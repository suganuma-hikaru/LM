' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG010DAC : 保管料/荷役料計算
'  作  成  者       :  [笈川]
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMG010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "検索処理 SQL"

    ''' <summary>
    ''' 荷主マスタ検索処理(件数取得)用Start
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_START As String = "SET ARITHABORT ON SET ARITHIGNORE ON SELECT COUNT(CUST_CD_L) AS SELECT_CNT FROM(                                                          " & vbNewLine

    ''' <summary>
    ''' 荷主マスタ検索処理(件数取得)用End
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_END As String = ")CNT"

    'START YANAI 要望番号871
    '''' <summary>
    '''' 荷主マスタ検索処理(データ取得)用
    '''' </summary>
    '''' <remarks>
    ''''   2011/10/17 修正 須賀
    ''''      単価マスタ取得時、削除フラグを抽出条件から除外
    '''' </remarks>
    'Private Const SQL_SELECT_DATA As String = "SELECT DISTINCT                                                                                            " & vbNewLine _
    '                                             & "   CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,                                         " & vbNewLine _
    '                                             & "   KBN1.KBN_NM1 AS CLOSE_NM,                                                                          " & vbNewLine _
    '                                             & "   CUST.CUST_NM_L + ' ' + CUST.CUST_NM_M + ' ' + CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS AS CUST_NM,   " & vbNewLine _
    '                                             & "   CUST.CUST_NM_L AS CUST_NM_L,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_NM_M AS CUST_NM_M,                                                                       " & vbNewLine _
    '                                             & "   CUST.HOKAN_NIYAKU_CALCULATION AS INV_DATE_TO,                                                      " & vbNewLine _
    '                                             & "   KBN2.KBN_NM1 AS KIWARI_NM,                                                                         " & vbNewLine _
    '                                             & "   CUST.SYS_UPD_DATE AS UPD_DATE_M_CUST,                                                              " & vbNewLine _
    '                                             & "   CUST.SYS_UPD_TIME AS UPD_TIME_M_CUST,                                                              " & vbNewLine _
    '                                             & "   CUST.NEW_JOB_NO AS JOB_NO,                                                                         " & vbNewLine _
    '                                             & "   CUST.CUST_CD_L AS CUST_CD_L,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_CD_M AS CUST_CD_M,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_CD_S AS CUST_CD_S,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_CD_SS AS CUST_CD_SS,                                                                     " & vbNewLine _
    '                                             & "   SEIQTO.CLOSE_KB AS CLOSE_KB,                                                                       " & vbNewLine _
    '                                             & "   GOODS.KIWARI_KB AS KIWARI_KB,                                                                      " & vbNewLine _
    '                                             & "   CUST.NRS_BR_CD AS NRS_BR_CD                                                                        " & vbNewLine _
    '                                             & "FROM $LM_MST$..M_CUST AS CUST                                                                         " & vbNewLine _
    '                                             & " INNER JOIN                                                                                           " & vbNewLine _
    '                                             & "  (SELECT DISTINCT                                                                                    " & vbNewLine _
    '                                             & "     GOODS.NRS_BR_CD,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_L,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_M,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_S,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_SS,                                                                                " & vbNewLine _
    '                                             & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
    '                                             & "   FROM $LM_MST$..M_GOODS AS GOODS,                                                                   " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "      TANKA.NRS_BR_CD,                                                                                " & vbNewLine _
    '                                             & "      TANKA.CUST_CD_L,                                                                                " & vbNewLine _
    '                                             & "      TANKA.CUST_CD_M,                                                                                " & vbNewLine _
    '                                             & "      TANKA.UP_GP_CD_1,                                                                               " & vbNewLine _
    '                                             & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
    '                                             & "   FROM $LM_MST$..M_TANKA AS TANKA                                                                    " & vbNewLine _
    '                                             & "   WHERE @NRS_BR_CD= TANKA.NRS_BR_CD                                                                  " & vbNewLine _
    '                                             & "  )TANKA                                                                                              " & vbNewLine _
    '                                             & "   WHERE GOODS.NRS_BR_CD   = TANKA.NRS_BR_CD                                                          " & vbNewLine _
    '                                             & "     AND GOODS.CUST_CD_L   = TANKA.CUST_CD_L                                                          " & vbNewLine _
    '                                             & "     AND GOODS.CUST_CD_M   = TANKA.CUST_CD_M                                                          " & vbNewLine _
    '                                             & "     AND GOODS.UP_GP_CD_1 = TANKA.UP_GP_CD_1                                                          " & vbNewLine _
    '                                             & "     )GOODS                                                                                           " & vbNewLine _
    '                                             & "     ON CUST.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_L = GOODS.CUST_CD_L                                                             " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_M = GOODS.CUST_CD_M                                                             " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_S = GOODS.CUST_CD_S                                                             " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                                           " & vbNewLine _
    '                                             & " LEFT OUTER JOIN $LM_MST$..M_TCUST AS TCUST                                                           " & vbNewLine _
    '                                             & "  ON CUST.CUST_CD_L = TCUST.CUST_CD_L                                                                 " & vbNewLine _
    '                                             & "  AND CUST.CUST_CD_M = TCUST.CUST_CD_M                                                                " & vbNewLine _
    '                                             & " LEFT OUTER JOIN $LM_MST$..M_SEIQTO AS SEIQTO                                                         " & vbNewLine _
    '                                             & "  ON CUST.NRS_BR_CD = SEIQTO.NRS_BR_CD                                                                " & vbNewLine _
    '                                             & "  AND (CUST.HOKAN_SEIQTO_CD = SEIQTO.SEIQTO_CD                                                        " & vbNewLine _
    '                                             & "   OR  CUST.NIYAKU_SEIQTO_CD = SEIQTO.SEIQTO_CD)                                                      " & vbNewLine _
    '                                             & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "     KBN1.KBN_CD,                                                                                     " & vbNewLine _
    '                                             & "     KBN1.KBN_NM1                                                                                     " & vbNewLine _
    '                                             & "   FROM $LM_MST$..Z_KBN AS KBN1                                                                       " & vbNewLine _
    '                                             & "   WHERE KBN1.KBN_GROUP_CD = 'S008')KBN1                                                              " & vbNewLine _
    '                                             & "  ON KBN1.KBN_CD = SEIQTO.CLOSE_KB                                                                    " & vbNewLine _
    '                                             & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "     KBN2.KBN_CD,                                                                                     " & vbNewLine _
    '                                             & "     KBN2.KBN_NM1                                                                                     " & vbNewLine _
    '                                             & "   FROM $LM_MST$..Z_KBN AS KBN2                                                                       " & vbNewLine _
    '                                             & "   WHERE KBN2.KBN_GROUP_CD = 'K003')KBN2                                                              " & vbNewLine _
    '                                             & "  ON KBN2.KBN_CD = GOODS.KIWARI_KB                                                                    " & vbNewLine _
    '                                             & "WHERE CUST.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
    '                                             & "  AND CUST.HOKAN_NIYAKU_KEISAN_YN = '01'                                                                          " & vbNewLine
    'START YANAI 要望番号824
    '''' <summary>
    '''' 荷主マスタ検索処理(データ取得)用
    '''' </summary>
    '''' <remarks>
    ''''      単価マスタ取得時、削除フラグを抽出条件から除外
    '''' </remarks>
    'Private Const SQL_SELECT_DATA As String = "SELECT DISTINCT                                                                                            " & vbNewLine _
    '                                             & "   CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,                                         " & vbNewLine _
    '                                             & "   KBN1.KBN_NM1 AS CLOSE_NM,                                                                          " & vbNewLine _
    '                                             & "   CUST.CUST_NM_L + ' ' + CUST.CUST_NM_M + ' ' + CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS AS CUST_NM,   " & vbNewLine _
    '                                             & "   CUST.CUST_NM_L AS CUST_NM_L,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_NM_M AS CUST_NM_M,                                                                       " & vbNewLine _
    '                                             & "   CUST.HOKAN_NIYAKU_CALCULATION AS INV_DATE_TO,                                                      " & vbNewLine _
    '                                             & "   KBN2.KBN_NM1 AS KIWARI_NM,                                                                         " & vbNewLine _
    '                                             & "   CUST.SYS_UPD_DATE AS UPD_DATE_M_CUST,                                                              " & vbNewLine _
    '                                             & "   CUST.SYS_UPD_TIME AS UPD_TIME_M_CUST,                                                              " & vbNewLine _
    '                                             & "   CUST.NEW_JOB_NO AS JOB_NO,                                                                         " & vbNewLine _
    '                                             & "   CUST.CUST_CD_L AS CUST_CD_L,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_CD_M AS CUST_CD_M,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_CD_S AS CUST_CD_S,                                                                       " & vbNewLine _
    '                                             & "   CUST.CUST_CD_SS AS CUST_CD_SS,                                                                     " & vbNewLine _
    '                                             & "   SEIQTO.CLOSE_KB AS CLOSE_KB,                                                                       " & vbNewLine _
    '                                             & "   GOODS.KIWARI_KB AS KIWARI_KB,                                                                      " & vbNewLine _
    '                                             & "   CUST.NRS_BR_CD AS NRS_BR_CD                                                                        " & vbNewLine _
    '                                             & "FROM $LM_MST$..M_CUST AS CUST                                                                         " & vbNewLine _
    '                                             & " INNER JOIN                                                                                           " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "     GOODS.NRS_BR_CD,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_L,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_M,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_S,                                                                                 " & vbNewLine _
    '                                             & "     GOODS.CUST_CD_SS,                                                                                " & vbNewLine _
    '                                             & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
    '                                             & "   FROM $LM_MST$..M_GOODS AS GOODS,                                                                   " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "      TANKA.NRS_BR_CD,                                                                                " & vbNewLine _
    '                                             & "      TANKA.CUST_CD_L,                                                                                " & vbNewLine _
    '                                             & "      TANKA.CUST_CD_M,                                                                                " & vbNewLine _
    '                                             & "      TANKA.UP_GP_CD_1,                                                                               " & vbNewLine _
    '                                             & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
    '                                             & "   FROM $LM_MST$..M_TANKA AS TANKA                                                                    " & vbNewLine _
    '                                             & "   WHERE @NRS_BR_CD= TANKA.NRS_BR_CD                                                                  " & vbNewLine _
    '                                             & "  )TANKA                                                                                              " & vbNewLine _
    '                                             & "   WHERE GOODS.NRS_BR_CD   = TANKA.NRS_BR_CD                                                          " & vbNewLine _
    '                                             & "     AND GOODS.CUST_CD_L   = TANKA.CUST_CD_L                                                          " & vbNewLine _
    '                                             & "     AND GOODS.CUST_CD_M   = TANKA.CUST_CD_M                                                          " & vbNewLine _
    '                                             & "     AND GOODS.UP_GP_CD_1 = TANKA.UP_GP_CD_1                                                          " & vbNewLine _
    '                                             & "     )GOODS                                                                                           " & vbNewLine _
    '                                             & "     ON CUST.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_L = GOODS.CUST_CD_L                                                             " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_M = GOODS.CUST_CD_M                                                             " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_S = GOODS.CUST_CD_S                                                             " & vbNewLine _
    '                                             & "     AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                                           " & vbNewLine _
    '                                             & " LEFT OUTER JOIN $LM_MST$..M_TCUST AS TCUST                                                           " & vbNewLine _
    '                                             & "  ON CUST.CUST_CD_L = TCUST.CUST_CD_L                                                                 " & vbNewLine _
    '                                             & "  AND CUST.CUST_CD_M = TCUST.CUST_CD_M                                                                " & vbNewLine _
    '                                             & " LEFT OUTER JOIN $LM_MST$..M_SEIQTO AS SEIQTO                                                         " & vbNewLine _
    '                                             & "  ON CUST.NRS_BR_CD = SEIQTO.NRS_BR_CD                                                                " & vbNewLine _
    '                                             & "  AND (CUST.HOKAN_SEIQTO_CD = SEIQTO.SEIQTO_CD                                                        " & vbNewLine _
    '                                             & "   OR  CUST.NIYAKU_SEIQTO_CD = SEIQTO.SEIQTO_CD)                                                      " & vbNewLine _
    '                                             & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "     KBN1.KBN_CD,                                                                                     " & vbNewLine _
    '                                             & "     KBN1.KBN_NM1                                                                                     " & vbNewLine _
    '                                             & "   FROM $LM_MST$..Z_KBN AS KBN1                                                                       " & vbNewLine _
    '                                             & "   WHERE KBN1.KBN_GROUP_CD = 'S008')KBN1                                                              " & vbNewLine _
    '                                             & "  ON KBN1.KBN_CD = SEIQTO.CLOSE_KB                                                                    " & vbNewLine _
    '                                             & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
    '                                             & "  (SELECT                                                                                             " & vbNewLine _
    '                                             & "     KBN2.KBN_CD,                                                                                     " & vbNewLine _
    '                                             & "     KBN2.KBN_NM1                                                                                     " & vbNewLine _
    '                                             & "   FROM $LM_MST$..Z_KBN AS KBN2                                                                       " & vbNewLine _
    '                                             & "   WHERE KBN2.KBN_GROUP_CD = 'K003')KBN2                                                              " & vbNewLine _
    '                                             & "  ON KBN2.KBN_CD = GOODS.KIWARI_KB                                                                    " & vbNewLine _
    '                                             & "WHERE CUST.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
    '                                             & "  AND CUST.HOKAN_NIYAKU_KEISAN_YN = '01'                                                                          " & vbNewLine
    ''' <summary>
    ''' 荷主マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks>
    '''      単価マスタ取得時、削除フラグを抽出条件から除外
    ''' </remarks>
    Private Const SQL_SELECT_DATA As String = "SET ARITHABORT ON SET ARITHIGNORE ON SELECT DISTINCT                                                                                            " & vbNewLine _
                                                 & "   CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,                                         " & vbNewLine _
                                                 & "   KBN1.KBN_NM1 AS CLOSE_NM,                                                                          " & vbNewLine _
                                                 & "   CUST.CUST_NM_L + ' ' + CUST.CUST_NM_M + ' ' + CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS AS CUST_NM,   " & vbNewLine _
                                                 & "   CUST.CUST_NM_L AS CUST_NM_L,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_NM_M AS CUST_NM_M,                                                                       " & vbNewLine _
                                                 & "   CUST.HOKAN_NIYAKU_CALCULATION AS INV_DATE_TO,                                                      " & vbNewLine _
                                                 & "   KBN2.KBN_NM1 AS KIWARI_NM,                                                                         " & vbNewLine _
                                                 & "   CUST.SYS_UPD_DATE AS UPD_DATE_M_CUST,                                                              " & vbNewLine _
                                                 & "   CUST.SYS_UPD_TIME AS UPD_TIME_M_CUST,                                                              " & vbNewLine _
                                                 & "   CUST.NEW_JOB_NO AS JOB_NO,                                                                         " & vbNewLine _
                                                 & "   CUST.CUST_CD_L AS CUST_CD_L,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_CD_M AS CUST_CD_M,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_CD_S AS CUST_CD_S,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_CD_SS AS CUST_CD_SS,                                                                     " & vbNewLine _
                                                 & "   SEIQTO.CLOSE_KB AS CLOSE_KB,                                                                       " & vbNewLine _
                                                 & "   GOODS.KIWARI_KB AS KIWARI_KB,                                                                      " & vbNewLine _
                                                 & "   CUST.NRS_BR_CD AS NRS_BR_CD                                                                        " & vbNewLine _
                                                 & "FROM $LM_MST$..M_CUST AS CUST                                                                         " & vbNewLine _
                                                 & " INNER JOIN                                                                                           " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "     GOODS.NRS_BR_CD,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_L,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_M,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_S,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_SS,                                                                                " & vbNewLine _
                                                 & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
                                                 & "   FROM $LM_MST$..M_GOODS AS GOODS,                                                                   " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "      TANKA.NRS_BR_CD,                                                                                " & vbNewLine _
                                                 & "      TANKA.CUST_CD_L,                                                                                " & vbNewLine _
                                                 & "      TANKA.CUST_CD_M,                                                                                " & vbNewLine _
                                                 & "      TANKA.UP_GP_CD_1,                                                                               " & vbNewLine _
                                                 & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
                                                 & "   FROM $LM_MST$..M_TANKA AS TANKA                                                                    " & vbNewLine _
                                                 & "   WHERE @NRS_BR_CD= TANKA.NRS_BR_CD                                                                  " & vbNewLine _
                                                 & "   --(2012.11.08)要望番号1231 SYS_DEL_FLG='0'制御 -- START --                                         " & vbNewLine _
                                                 & "     AND TANKA.SYS_DEL_FLG='0'                                                                        " & vbNewLine _
                                                 & "   --(2012.11.08)要望番号1231 SYS_DEL_FLG='0'制御 --  END  --                                         " & vbNewLine _
                                                 & "  )TANKA                                                                                              " & vbNewLine _
                                                 & "   WHERE GOODS.NRS_BR_CD   = TANKA.NRS_BR_CD                                                          " & vbNewLine _
                                                 & "     AND GOODS.CUST_CD_L   = TANKA.CUST_CD_L                                                          " & vbNewLine _
                                                 & "     AND GOODS.CUST_CD_M   = TANKA.CUST_CD_M                                                          " & vbNewLine _
                                                 & "     AND GOODS.UP_GP_CD_1 = TANKA.UP_GP_CD_1                                                          " & vbNewLine _
                                                 & "     )GOODS                                                                                           " & vbNewLine _
                                                 & "     ON CUST.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_L = GOODS.CUST_CD_L                                                             " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_M = GOODS.CUST_CD_M                                                             " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_S = GOODS.CUST_CD_S                                                             " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                                           " & vbNewLine _
                                                 & " LEFT OUTER JOIN $LM_MST$..M_SEIQTO AS SEIQTO                                                         " & vbNewLine _
                                                 & "  ON CUST.NRS_BR_CD = SEIQTO.NRS_BR_CD                                                                " & vbNewLine _
                                                 & "  AND (RTRIM(CUST.HOKAN_SEIQTO_CD) = RTRIM(SEIQTO.SEIQTO_CD)                                                        " & vbNewLine _
                                                 & "   OR  RTRIM(CUST.NIYAKU_SEIQTO_CD) = RTRIM(SEIQTO.SEIQTO_CD))                                                      " & vbNewLine _
                                                 & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "     KBN1.KBN_CD,                                                                                     " & vbNewLine _
                                                 & "     KBN1.#KBN# AS KBN_NM1                                                                            " & vbNewLine _
                                                 & "   FROM $LM_MST$..Z_KBN AS KBN1                                                                       " & vbNewLine _
                                                 & "   WHERE KBN1.KBN_GROUP_CD = 'S008')KBN1                                                              " & vbNewLine _
                                                 & "  ON KBN1.KBN_CD = SEIQTO.CLOSE_KB                                                                    " & vbNewLine _
                                                 & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "     KBN2.KBN_CD,                                                                                     " & vbNewLine _
                                                 & "     KBN2.#KBN# AS KBN_NM1                                                                            " & vbNewLine _
                                                 & "   FROM $LM_MST$..Z_KBN AS KBN2                                                                       " & vbNewLine _
                                                 & "   WHERE KBN2.KBN_GROUP_CD = 'K003')KBN2                                                              " & vbNewLine _
                                                 & "  ON KBN2.KBN_CD = GOODS.KIWARI_KB                                                                    " & vbNewLine _
                                                 & "WHERE CUST.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
                                                 & "  AND CUST.HOKAN_NIYAKU_KEISAN_YN = '01'                                                                          " & vbNewLine

    ''' <summary>
    ''' 荷主マスタ検索処理(データ取得)用
    ''' </summary>
    ''' <remarks>
    '''      単価マスタ取得時、削除フラグを抽出条件から除外
    ''' </remarks>
    Private Const SQL_SELECT_DATA_CNT As String = "SELECT DISTINCT                                                                                            " & vbNewLine _
                                                 & "   CUST.HOKAN_NIYAKU_CALCULATION AS HOKAN_NIYAKU_CALCULATION,                                         " & vbNewLine _
                                                 & "   KBN1.KBN_NM1 AS CLOSE_NM,                                                                          " & vbNewLine _
                                                 & "   CUST.CUST_NM_L + ' ' + CUST.CUST_NM_M + ' ' + CUST.CUST_NM_S + ' ' + CUST.CUST_NM_SS AS CUST_NM,   " & vbNewLine _
                                                 & "   CUST.CUST_NM_L AS CUST_NM_L,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_NM_M AS CUST_NM_M,                                                                       " & vbNewLine _
                                                 & "   CUST.HOKAN_NIYAKU_CALCULATION AS INV_DATE_TO,                                                      " & vbNewLine _
                                                 & "   KBN2.KBN_NM1 AS KIWARI_NM,                                                                         " & vbNewLine _
                                                 & "   CUST.SYS_UPD_DATE AS UPD_DATE_M_CUST,                                                              " & vbNewLine _
                                                 & "   CUST.SYS_UPD_TIME AS UPD_TIME_M_CUST,                                                              " & vbNewLine _
                                                 & "   CUST.NEW_JOB_NO AS JOB_NO,                                                                         " & vbNewLine _
                                                 & "   CUST.CUST_CD_L AS CUST_CD_L,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_CD_M AS CUST_CD_M,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_CD_S AS CUST_CD_S,                                                                       " & vbNewLine _
                                                 & "   CUST.CUST_CD_SS AS CUST_CD_SS,                                                                     " & vbNewLine _
                                                 & "   SEIQTO.CLOSE_KB AS CLOSE_KB,                                                                       " & vbNewLine _
                                                 & "   GOODS.KIWARI_KB AS KIWARI_KB,                                                                      " & vbNewLine _
                                                 & "   CUST.NRS_BR_CD AS NRS_BR_CD                                                                        " & vbNewLine _
                                                 & "FROM $LM_MST$..M_CUST AS CUST                                                                         " & vbNewLine _
                                                 & " INNER JOIN                                                                                           " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "     GOODS.NRS_BR_CD,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_L,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_M,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_S,                                                                                 " & vbNewLine _
                                                 & "     GOODS.CUST_CD_SS,                                                                                " & vbNewLine _
                                                 & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
                                                 & "   FROM $LM_MST$..M_GOODS AS GOODS,                                                                   " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "      TANKA.NRS_BR_CD,                                                                                " & vbNewLine _
                                                 & "      TANKA.CUST_CD_L,                                                                                " & vbNewLine _
                                                 & "      TANKA.CUST_CD_M,                                                                                " & vbNewLine _
                                                 & "      TANKA.UP_GP_CD_1,                                                                               " & vbNewLine _
                                                 & "      TANKA.KIWARI_KB                                                                                 " & vbNewLine _
                                                 & "   FROM $LM_MST$..M_TANKA AS TANKA                                                                    " & vbNewLine _
                                                 & "   WHERE @NRS_BR_CD= TANKA.NRS_BR_CD                                                                  " & vbNewLine _
                                                 & "   --(2012.11.08)要望番号1231 SYS_DEL_FLG='0'制御 -- START --                                         " & vbNewLine _
                                                 & "     AND TANKA.SYS_DEL_FLG='0'                                                                        " & vbNewLine _
                                                 & "   --(2012.11.08)要望番号1231 SYS_DEL_FLG='0'制御 --  END  --                                         " & vbNewLine _
                                                 & "  )TANKA                                                                                              " & vbNewLine _
                                                 & "   WHERE GOODS.NRS_BR_CD   = TANKA.NRS_BR_CD                                                          " & vbNewLine _
                                                 & "     AND GOODS.CUST_CD_L   = TANKA.CUST_CD_L                                                          " & vbNewLine _
                                                 & "     AND GOODS.CUST_CD_M   = TANKA.CUST_CD_M                                                          " & vbNewLine _
                                                 & "     AND GOODS.UP_GP_CD_1 = TANKA.UP_GP_CD_1                                                          " & vbNewLine _
                                                 & "     )GOODS                                                                                           " & vbNewLine _
                                                 & "     ON CUST.NRS_BR_CD = GOODS.NRS_BR_CD                                                              " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_L = GOODS.CUST_CD_L                                                             " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_M = GOODS.CUST_CD_M                                                             " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_S = GOODS.CUST_CD_S                                                             " & vbNewLine _
                                                 & "     AND CUST.CUST_CD_SS = GOODS.CUST_CD_SS                                                           " & vbNewLine _
                                                 & " LEFT OUTER JOIN $LM_MST$..M_SEIQTO AS SEIQTO                                                         " & vbNewLine _
                                                 & "  ON CUST.NRS_BR_CD = SEIQTO.NRS_BR_CD                                                                " & vbNewLine _
                                                 & "  AND (RTRIM(CUST.HOKAN_SEIQTO_CD) = RTRIM(SEIQTO.SEIQTO_CD)                                                        " & vbNewLine _
                                                 & "   OR  RTRIM(CUST.NIYAKU_SEIQTO_CD) = RTRIM(SEIQTO.SEIQTO_CD))                                                      " & vbNewLine _
                                                 & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "     KBN1.KBN_CD,                                                                                     " & vbNewLine _
                                                 & "     KBN1.KBN_NM1                                                                                     " & vbNewLine _
                                                 & "   FROM $LM_MST$..Z_KBN AS KBN1                                                                       " & vbNewLine _
                                                 & "   WHERE KBN1.KBN_GROUP_CD = 'S008')KBN1                                                              " & vbNewLine _
                                                 & "  ON KBN1.KBN_CD = SEIQTO.CLOSE_KB                                                                    " & vbNewLine _
                                                 & " LEFT OUTER JOIN                                                                                      " & vbNewLine _
                                                 & "  (SELECT                                                                                             " & vbNewLine _
                                                 & "     KBN2.KBN_CD,                                                                                     " & vbNewLine _
                                                 & "     KBN2.KBN_NM1                                                                                     " & vbNewLine _
                                                 & "   FROM $LM_MST$..Z_KBN AS KBN2                                                                       " & vbNewLine _
                                                 & "   WHERE KBN2.KBN_GROUP_CD = 'K003')KBN2                                                              " & vbNewLine _
                                                 & "  ON KBN2.KBN_CD = GOODS.KIWARI_KB                                                                    " & vbNewLine _
                                                 & "WHERE CUST.SYS_DEL_FLG = '0'                                                                          " & vbNewLine _
                                                 & "  AND CUST.HOKAN_NIYAKU_KEISAN_YN = '01'                                                                          " & vbNewLine

    'END YANAI 要望番号824
    'END YANAI 要望番号871

    ''' <summary>
    ''' ORDER_BY句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_ORDER_BY As String = "ORDER BY CUST.HOKAN_NIYAKU_CALCULATION DESC , CUST.CUST_CD_L , CUST.CUST_CD_M , CUST.CUST_CD_S , CUST.CUST_CD_SS"

    ''' <summary>
    ''' 単価未承認チェック
    ''' </summary>
    Private Const SQL_SELECT_CHK_APPROVAL_TANKA As String = "" _
        & " SELECT                                          " & vbNewLine _
        & "    @ROW_NO AS ROW_NO                            " & vbNewLine _
        & "   ,MTAN.NRS_BR_CD                               " & vbNewLine _
        & "   ,MTAN.CUST_CD_L                               " & vbNewLine _
        & "   ,MTAN.CUST_CD_M                               " & vbNewLine _
        & " FROM                                            " & vbNewLine _
        & "   $LM_MST$..M_TANKA   AS  MTAN                  " & vbNewLine _
        & " WHERE                                           " & vbNewLine _
        & "       MTAN.NRS_BR_CD      =  @NRS_BR_CD         " & vbNewLine _
        & "   AND MTAN.CUST_CD_L      =  @CUST_CD_L         " & vbNewLine _
        & "   AND MTAN.CUST_CD_M      =  @CUST_CD_M         " & vbNewLine _
        & "   AND MTAN.APPROVAL_CD    <> '01'               " & vbNewLine _
        & "   AND MTAN.SYS_DEL_FLG    =  '0'                " & vbNewLine

    ''' <summary>
    ''' 変動保管料チェック
    ''' </summary>
    Private Const SQL_SELECT_CHK_VAR As String = "" _
        & " SELECT                                          " & vbNewLine _
        & "    @ROW_NO AS ROW_NO                            " & vbNewLine _
        & "   ,MCUS.NRS_BR_CD                               " & vbNewLine _
        & "   ,MCUS.CUST_CD_L                               " & vbNewLine _
        & "   ,MCUS.CUST_CD_M                               " & vbNewLine _
        & "   ,MCUS.CUST_CD_S                               " & vbNewLine _
        & "   ,MCUS.CUST_CD_SS                              " & vbNewLine _
        & "   ,MCUS.SAITEI_HAN_KB                           " & vbNewLine _
        & "   ,KBN_KEIKA_MONTH.VALUE1 AS KBN_KEIKA_MONTH_VALUE1 " & vbNewLine _
        & "   ,KBN_KEIKA_MONTH.VALUE2 AS KBN_KEIKA_MONTH_VALUE2 " & vbNewLine _
        & " FROM                                            " & vbNewLine _
        & "   $LM_MST$..M_CUST   AS  MCUS                   " & vbNewLine _
        & " LEFT JOIN                                       " & vbNewLine _
        & "   $LM_MST$..M_SEIQTO AS  MSEI                   " & vbNewLine _
        & "   ON  MSEI.NRS_BR_CD = MCUS.NRS_BR_CD           " & vbNewLine _
        & "   AND MSEI.SEIQTO_CD = MCUS.HOKAN_SEIQTO_CD     " & vbNewLine _
        & " LEFT JOIN                                       " & vbNewLine _
        & "   $LM_MST$..Z_KBN    AS  KBN_KEIKA_MONTH        " & vbNewLine _
        & "   ON  KBN_KEIKA_MONTH.KBN_GROUP_CD = 'G022'     " & vbNewLine _
        & "   AND KBN_KEIKA_MONTH.KBN_NM1 = MSEI.NRS_BR_CD  " & vbNewLine _
        & "   AND KBN_KEIKA_MONTH.KBN_NM2 = MSEI.SEIQTO_CD  " & vbNewLine _
        & "   AND KBN_KEIKA_MONTH.SYS_DEL_FLG = '0'         " & vbNewLine _
        & " WHERE                                           " & vbNewLine _
        & "       MCUS.NRS_BR_CD      =  @NRS_BR_CD         " & vbNewLine _
        & "   AND MCUS.CUST_CD_L      =  @CUST_CD_L         " & vbNewLine _
        & "   AND MCUS.CUST_CD_M      =  @CUST_CD_M         " & vbNewLine _
        & "   AND MCUS.CUST_CD_S      =  @CUST_CD_S         " & vbNewLine _
        & "   AND MCUS.CUST_CD_SS     =  @CUST_CD_SS        " & vbNewLine _
        & "   AND MCUS.SYS_DEL_FLG    =  '0'                " & vbNewLine _
        & "   AND MSEI.VAR_STRAGE_FLG =  '1'                " & vbNewLine

#End Region

#Region "前回計算取消 SQL"

#Region "データ取得"

    ''' <summary>
    ''' 前回計算取消排他SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA As String = "SET ARITHABORT ON " & vbNewLine _
                                        & "SET ARITHIGNORE ON " & vbNewLine _
                                        & "SELECT COUNT(CUST.NRS_BR_CD) AS SELECT_CNT                          " & vbNewLine _
                                       & "FROM $LM_MST$..M_CUST AS CUST                                      " & vbNewLine _
                                       & "WHERE  CUST.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                       & "   AND CUST.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                       & "   AND CUST.CUST_CD_M = @CUST_CD_M                                 " & vbNewLine _
                                       & "   AND CUST.CUST_CD_S = @CUST_CD_S                                 " & vbNewLine _
                                       & "   AND CUST.CUST_CD_SS = @CUST_CD_SS                               " & vbNewLine _
                                       & "   AND CUST.SYS_UPD_DATE = @UPD_DATE_M_CUST                        " & vbNewLine _
                                       & "   AND CUST.SYS_UPD_TIME = @UPD_TIME_M_CUST                        " & vbNewLine

    ''' <summary>
    ''' 最終計算日判定用SQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INV_DATE_MAX As String = "SET ARITHABORT ON " & vbNewLine _
                                   & "SET ARITHIGNORE ON " & vbNewLine _
                                   & "SELECT                                                       " & vbNewLine _
                                   & " (CASE WHEN ISNULL(CUST.HOKAN_NIYAKU_CALCULATION,'1') <" _
                                   & " ISNULL(SEKY.INV_DATE_TO,CUST.HOKAN_NIYAKU_CALCULATION) THEN 'True' ELSE 'False' END) AS ANSWER  " & vbNewLine _
                                   & "FROM                                                                   " & vbNewLine _
                                   & " (SELECT                                                               " & vbNewLine _
                                   & "   HOKAN_NIYAKU_CALCULATION                                            " & vbNewLine _
                                   & "  FROM $LM_MST$..M_CUST AS CUST                                        " & vbNewLine _
                                   & "  WHERE CUST.NRS_BR_CD = @NRS_BR_CD                                    " & vbNewLine _
                                   & "    AND CUST.CUST_CD_L = @CUST_CD_L                                    " & vbNewLine _
                                   & "    AND CUST.CUST_CD_M = @CUST_CD_M                                    " & vbNewLine _
                                   & "    AND CUST.CUST_CD_S = @CUST_CD_S                                    " & vbNewLine _
                                   & "    AND CUST.CUST_CD_SS = @CUST_CD_SS)AS CUST,                         " & vbNewLine _
                                   & " (SELECT                                                               " & vbNewLine _
                                   & "   MAX(SEKY.INV_DATE_TO) AS INV_DATE_TO                                " & vbNewLine _
                                   & "  FROM                                                                 " & vbNewLine _
                                   & "   (SELECT                                                             " & vbNewLine _
                                   & "     MAX(SEKY.INV_DATE_TO) AS INV_DATE_TO                              " & vbNewLine _
                                   & "    FROM $LM_MST$..M_GOODS AS GOODS                                    " & vbNewLine _
                                   & "     INNER JOIN $LM_TRN$..G_SEKY_TBL AS SEKY                           " & vbNewLine _
                                   & "      ON SEKY.NRS_BR_CD = GOODS.NRS_BR_CD                              " & vbNewLine _
                                   & "      AND SEKY.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                       " & vbNewLine _
                                   & "      AND SEKY.SEKY_FLG ='00'                                          " & vbNewLine _
                                   & "    WHERE GOODS.NRS_BR_CD = @NRS_BR_CD                                 " & vbNewLine _
                                   & "      AND GOODS.CUST_CD_L = @CUST_CD_L                                 " & vbNewLine _
                                   & "      AND GOODS.CUST_CD_M = @CUST_CD_M                                 " & vbNewLine _
                                   & "      AND GOODS.CUST_CD_S = @CUST_CD_S                                 " & vbNewLine _
                                   & "      AND GOODS.CUST_CD_SS =@CUST_CD_SS                                " & vbNewLine _
                                   & "    GROUP BY SEKY.INV_DATE_TO)SEKY)SEKY                                " & vbNewLine



    '''' <summary>
    '''' 請求元在庫テーブルデータ取得
    '''' </summary>
    '''' <remarks></remarks>
    'Private Const SQL_SEKY As String = "SELECT DISTINCT                                                  " & vbNewLine _
    '                                 & "     ISNULL(SEKY.INV_DATE_TO,MC.HOKAN_NIYAKU_CALCULATION_OLD) AS INV_DATE_TO " & vbNewLine _
    '                                 & "    ,ISNULL(SEKY.JOB_NO,MC.OLD_JOB_NO)                        AS JOB_NO      " & vbNewLine _
    '                                 & "FROM $LM_MST$..M_GOODS AS GOODS                                  " & vbNewLine _
    '                                 & "     LEFT OUTER JOIN $LM_TRN$..G_SEKY_TBL AS SEKY                " & vbNewLine _
    '                                 & "       ON SEKY.NRS_BR_CD = GOODS.NRS_BR_CD	                     " & vbNewLine _
    '                                 & "      AND SEKY.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                 " & vbNewLine _
    '                                 & "      AND SEKY.SEKY_FLG ='00'	                                 " & vbNewLine _
    '                                 & "    ,$LM_MST$..M_CUST AS MC                                      " & vbNewLine _
    '                                 & "WHERE                                                            " & vbNewLine _
    '                                 & "     GOODS.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_S = @CUST_CD_S                                " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_SS = @CUST_CD_SS	                             " & vbNewLine _
    '                                 & " AND GOODS.NRS_BR_CD = MC.NRS_BR_CD	                             " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_L = MC.CUST_CD_L	                             " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_M = MC.CUST_CD_M	                             " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_S = MC.CUST_CD_S	                             " & vbNewLine _
    '                                 & " AND GOODS.CUST_CD_SS = MC.CUST_CD_SS	                         " & vbNewLine _
    '                                 & "ORDER BY INV_DATE_TO DESC                                        " & vbNewLine
    ' ''' <summary>
    ' ''' 請求元在庫テーブルデータ取得
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private Const SQL_SEKY As String = "SELECT DISTINCT                                                  " & vbNewLine _
    '                                     & "     ISNULL(SEKY.INV_DATE_TO,MC.HOKAN_NIYAKU_CALCULATION_OLD) AS INV_DATE_TO " & vbNewLine _
    '                                     & "    ,ISNULL(SEKY.JOB_NO,MC.OLD_JOB_NO)                        AS JOB_NO      " & vbNewLine _
    '                                     & "FROM                                   " & vbNewLine _
    '                                     & "     (SELECT * FROM $LM_MST$..M_GOODS AS GOODS                                  " & vbNewLine _
    '                                     & " WHERE    GOODS.NRS_BR_CD = @NRS_BR_CD                                " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_S = @CUST_CD_S                                " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_SS = @CUST_CD_SS ) AS GOODS	                             " & vbNewLine _
    '                                     & "     LEFT OUTER JOIN $LM_TRN$..G_SEKY_TBL AS SEKY                " & vbNewLine _
    '                                     & "       ON SEKY.NRS_BR_CD = GOODS.NRS_BR_CD	                     " & vbNewLine _
    '                                     & "      AND SEKY.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                 " & vbNewLine _
    '                                     & "      AND SEKY.SEKY_FLG ='00'	                                 " & vbNewLine _
    '                                     & "     LEFT OUTER JOIN $LM_MST$..M_CUST AS MC                                      " & vbNewLine _
    '                                     & " ON GOODS.NRS_BR_CD = MC.NRS_BR_CD	                             " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_L = MC.CUST_CD_L	                             " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_M = MC.CUST_CD_M	                             " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_S = MC.CUST_CD_S	                             " & vbNewLine _
    '                                     & " AND GOODS.CUST_CD_SS = MC.CUST_CD_SS	                         " & vbNewLine _
    '                                     & "ORDER BY INV_DATE_TO DESC                                        " & vbNewLine

    ''' <summary>
    ''' Delete用データ取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DELETE_KEY As String = "SET ARITHABORT ON " & vbNewLine _
                                        & "SET ARITHIGNORE ON " & vbNewLine _
                                        & "SELECT        	                                             " & vbNewLine _
                                       & "    CUST.NRS_BR_CD                AS NRS_BR_CD,      	             " & vbNewLine _
                                       & "    CUST.CUST_CD_L                AS CUST_CD_L,      	             " & vbNewLine _
                                       & "    CUST.CUST_CD_M                AS CUST_CD_M,      	             " & vbNewLine _
                                       & "    CUST.CUST_CD_S                AS CUST_CD_S,      	             " & vbNewLine _
                                       & "    CUST.CUST_CD_SS               AS CUST_CD_SS,      	         " & vbNewLine _
                                       & "    CUST.HOKAN_NIYAKU_CALCULATION AS INV_DATE_TO	                 " & vbNewLine _
                                       & "  FROM $LM_MST$..M_CUST  AS CUST	                                 " & vbNewLine _
                                       & "   WHERE CUST.NRS_BR_CD = @NRS_BR_CD	                             " & vbNewLine _
                                       & "    AND CUST.CUST_CD_L  = @CUST_CD_L	                             " & vbNewLine _
                                       & "    AND CUST.CUST_CD_M  = @CUST_CD_M	                             " & vbNewLine _
                                       & "    AND CUST.CUST_CD_S  = @CUST_CD_S	                             " & vbNewLine _
                                       & "    AND CUST.CUST_CD_SS = @CUST_CD_SS	                             " & vbNewLine


#End Region

#Region "更新"

    ''' <summary>
    ''' 荷主マスタUPDATESQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_CUST As String = "UPDATE $LM_MST$..M_CUST                                       " & vbNewLine _
                                       & "SET                                                                " & vbNewLine _
                                       & "  SYS_UPD_DATE = @SYS_UPD_DATE                                     " & vbNewLine _
                                       & "  ,SYS_UPD_TIME = @SYS_UPD_TIME                                    " & vbNewLine _
                                       & "  ,SYS_UPD_PGID = @SYS_UPD_PGID                                    " & vbNewLine _
                                       & "  ,SYS_UPD_USER = @SYS_UPD_USER                                    " & vbNewLine _
                                       & "  ,HOKAN_NIYAKU_CALCULATION = @HOKAN_NIYAKU_CALCULATION            " & vbNewLine _
                                       & "  ,HOKAN_NIYAKU_CALCULATION_OLD = @HOKAN_NIYAKU_CALCULATION_OLD    " & vbNewLine _
                                       & "  ,NEW_JOB_NO = @NEW_JOB_NO                                        " & vbNewLine _
                                       & "  ,OLD_JOB_NO = @OLD_JOB_NO                                        " & vbNewLine _
                                       & "WHERE                                                              " & vbNewLine _
                                       & "  NRS_BR_CD = @NRS_BR_CD                                           " & vbNewLine _
                                       & "  AND CUST_CD_L = @CUST_CD_L                                       " & vbNewLine _
                                       & "  AND CUST_CD_M = @CUST_CD_M                                       " & vbNewLine _
                                       & "  AND CUST_CD_S = @CUST_CD_S                                       " & vbNewLine _
                                       & "  AND CUST_CD_SS = @CUST_CD_SS                                     " & vbNewLine _
                                       & "  AND SYS_UPD_DATE = @UPD_DATE_M_CUST                              " & vbNewLine _
                                       & "  AND SYS_UPD_TIME = @UPD_TIME_M_CUST                              " & vbNewLine

    ''' <summary>
    ''' 請求明細データテーブル削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_SEQMEISAI As String = "DELETE FROM                            " & vbNewLine _
                                       & "     $LM_TRN$..G_SEKY_MEISAI                  " & vbNewLine _
                                       & "FROM $LM_TRN$..G_SEKY_MEISAI        AS TR     " & vbNewLine _
                                       & "     INNER JOIN $LM_MST$..M_GOODS   AS MG     " & vbNewLine _
                                       & "        ON TR.NRS_BR_CD    = MG.NRS_BR_CD     " & vbNewLine _
                                       & "       AND TR.GOODS_CD_NRS = MG.GOODS_CD_NRS  " & vbNewLine _
                                       & "WHERE                                         " & vbNewLine _
                                       & "      TR.NRS_BR_CD    = @NRS_BR_CD            " & vbNewLine _
                                       & "  AND TR.INV_DATE_TO  = @INV_DATE_TO          " & vbNewLine _
                                       & "  AND TR.SEKY_FLG     = '00'                  " & vbNewLine _
                                       & "  AND MG.NRS_BR_CD    = TR.NRS_BR_CD          " & vbNewLine _
                                       & "  AND MG.CUST_CD_L    = @CUST_CD_L            " & vbNewLine _
                                       & "  AND MG.CUST_CD_M    = @CUST_CD_M            " & vbNewLine _
                                       & "  AND MG.CUST_CD_S    = @CUST_CD_S            " & vbNewLine _
                                       & "  AND MG.CUST_CD_SS   = @CUST_CD_SS           " & vbNewLine

    ''' <summary>
    ''' 請求元在庫データ削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_SEQZAI As String = "DELETE FROM                               " & vbNewLine _
                                       & "     $LM_TRN$..G_SEKY_TBL                     " & vbNewLine _
                                       & "FROM $LM_TRN$..G_SEKY_TBL           AS TR     " & vbNewLine _
                                       & "     INNER JOIN $LM_MST$..M_GOODS   AS MG     " & vbNewLine _
                                       & "        ON TR.NRS_BR_CD    = MG.NRS_BR_CD     " & vbNewLine _
                                       & "       AND TR.GOODS_CD_NRS = MG.GOODS_CD_NRS  " & vbNewLine _
                                       & "WHERE                                         " & vbNewLine _
                                       & "      TR.NRS_BR_CD    = @NRS_BR_CD            " & vbNewLine _
                                       & "  AND TR.INV_DATE_TO  = @INV_DATE_TO          " & vbNewLine _
                                       & "  AND TR.SEKY_FLG     = '00'                  " & vbNewLine _
                                       & "  AND MG.NRS_BR_CD    = TR.NRS_BR_CD          " & vbNewLine _
                                       & "  AND MG.CUST_CD_L    = @CUST_CD_L            " & vbNewLine _
                                       & "  AND MG.CUST_CD_M    = @CUST_CD_M            " & vbNewLine _
                                       & "  AND MG.CUST_CD_S    = @CUST_CD_S            " & vbNewLine _
                                       & "  AND MG.CUST_CD_SS   = @CUST_CD_SS           " & vbNewLine

    ''' <summary>
    ''' 月末在庫データ削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_ZAIKZAN As String = "DELETE FROM                              " & vbNewLine _
                                       & "     $LM_TRN$..G_ZAIK_ZAN                     " & vbNewLine _
                                       & "FROM $LM_TRN$..G_ZAIK_ZAN           AS TR     " & vbNewLine _
                                       & "WHERE                                         " & vbNewLine _
                                       & "      TR.NRS_BR_CD    = @NRS_BR_CD            " & vbNewLine _
                                       & "  AND TR.INV_DATE_TO  = @INV_DATE_TO          " & vbNewLine _
                                       & "  AND TR.CUST_CD_L    = @CUST_CD_L            " & vbNewLine _
                                       & "  AND TR.CUST_CD_M    = @CUST_CD_M            " & vbNewLine _
                                       & "  AND TR.CUST_CD_S    = @CUST_CD_S            " & vbNewLine _
                                       & "  AND TR.CUST_CD_SS   = @CUST_CD_SS           " & vbNewLine
    'Private Const SQL_DEL_ZAIKZAN As String = "DELETE FROM                              " & vbNewLine _
    '                                   & "     $LM_TRN$..G_ZAIK_ZAN                     " & vbNewLine _
    '                                   & "FROM $LM_TRN$..G_ZAIK_ZAN           AS TR     " & vbNewLine _
    '                                   & "     INNER JOIN $LM_MST$..M_GOODS   AS MG     " & vbNewLine _
    '                                   & "        ON TR.NRS_BR_CD    = MG.NRS_BR_CD     " & vbNewLine _
    '                                   & "       AND TR.GOODS_CD_NRS = MG.GOODS_CD_NRS  " & vbNewLine _
    '                                   & "WHERE                                         " & vbNewLine _
    '                                   & "      TR.NRS_BR_CD    = @NRS_BR_CD            " & vbNewLine _
    '                                   & "  AND TR.INV_DATE_TO  = @INV_DATE_TO          " & vbNewLine _
    '                                   & "  AND MG.NRS_BR_CD    = TR.NRS_BR_CD          " & vbNewLine _
    '                                   & "  AND MG.CUST_CD_L    = @CUST_CD_L            " & vbNewLine _
    '                                   & "  AND MG.CUST_CD_M    = @CUST_CD_M            " & vbNewLine _
    '                                   & "  AND MG.CUST_CD_S    = @CUST_CD_S            " & vbNewLine _
    '                                   & "  AND MG.CUST_CD_SS   = @CUST_CD_SS           " & vbNewLine
    ''' <summary>
    ''' 保管荷役明細印刷テーブル削除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_DEL_SEKMEISAIPRT As String = "DELETE FROM                         " & vbNewLine _
                                       & "     $LM_TRN$..G_SEKY_MEISAI_PRT              " & vbNewLine _
                                       & "FROM $LM_TRN$..G_SEKY_MEISAI_PRT    AS TR     " & vbNewLine _
                                       & "     INNER JOIN $LM_MST$..M_GOODS   AS MG     " & vbNewLine _
                                       & "        ON TR.NRS_BR_CD    = MG.NRS_BR_CD     " & vbNewLine _
                                       & "       AND TR.GOODS_CD_NRS = MG.GOODS_CD_NRS  " & vbNewLine _
                                       & "WHERE                                         " & vbNewLine _
                                       & "      TR.NRS_BR_CD    = @NRS_BR_CD            " & vbNewLine _
                                       & "  AND TR.INV_DATE_TO  = @INV_DATE_TO          " & vbNewLine _
                                       & "  AND TR.SEKY_FLG     = '00'                  " & vbNewLine _
                                       & "  AND MG.NRS_BR_CD    = TR.NRS_BR_CD          " & vbNewLine _
                                       & "  AND MG.CUST_CD_L    = @CUST_CD_L            " & vbNewLine _
                                       & "  AND MG.CUST_CD_M    = @CUST_CD_M            " & vbNewLine _
                                       & "  AND MG.CUST_CD_S    = @CUST_CD_S            " & vbNewLine _
                                       & "  AND MG.CUST_CD_SS   = @CUST_CD_SS           " & vbNewLine

#End Region

#End Region

#Region "実行 SQL"

    ''' <summary>
    ''' 実行時INSERTSQL
    ''' </summary>
    ''' <remarks></remarks>
    Private Const EXECUTE_Insert_SQL As String = "INSERT INTO $LM_MST$..G_HN_CALC_WK_HEAD                                                                 " & vbNewLine _
                                                 & "(                                                                                                     " & vbNewLine _
                                                 & "SEKY_FLG,                                                                                             " & vbNewLine _
                                                 & "BATCH_NO,                                                                                             " & vbNewLine _
                                                 & "NRS_BR_CD,                                                                                            " & vbNewLine _
                                                 & "OPE_USER_CD,                                                                                          " & vbNewLine _
                                                 & "REC_NO,                                                                                               " & vbNewLine _
                                                 & "MIKAN_INC_FLG,                                                                                        " & vbNewLine _
                                                 & "SEKY_DATE,                                                                                            " & vbNewLine _
                                                 & "JOB_NO,                                                                                               " & vbNewLine _
                                                 & "CUST_CD_L,                                                                                            " & vbNewLine _
                                                 & "CUST_CD_M,                                                                                            " & vbNewLine _
                                                 & "CUST_CD_S,                                                                                            " & vbNewLine _
                                                 & "CUST_CD_SS,                                                                                           " & vbNewLine _
                                                 & "EXEC_STATE_KB,                                                                                        " & vbNewLine _
                                                 & "EXEC_RESULT_KB,                                                                                       " & vbNewLine _
                                                 & "HOKAN_NIYAKU_CALCULATION,                                                                             " & vbNewLine _
                                                 & "CLOSE_KB,                                                                                             " & vbNewLine _
                                                 & "INV_DATE_TO,                                                                                          " & vbNewLine _
                                                 & "MESSAGE_ID,                                                                                           " & vbNewLine _
                                                 & "EXEC_TIMING_KB,                                                                                       " & vbNewLine _
                                                 & "KIWARI_KB,                                                                                            " & vbNewLine _
                                                 & "SYS_ENT_DATE,                                                                                         " & vbNewLine _
                                                 & "SYS_ENT_TIME,                                                                                         " & vbNewLine _
                                                 & "SYS_ENT_PGID,                                                                                         " & vbNewLine _
                                                 & "SYS_ENT_USER,                                                                                         " & vbNewLine _
                                                 & "SYS_UPD_DATE,                                                                                         " & vbNewLine _
                                                 & "SYS_UPD_TIME,                                                                                         " & vbNewLine _
                                                 & "SYS_UPD_PGID,                                                                                         " & vbNewLine _
                                                 & "SYS_UPD_USER,                                                                                         " & vbNewLine _
                                                 & "SYS_DEL_FLG                                                                                           " & vbNewLine _
                                                 & ")VALUES(                                                                                              " & vbNewLine _
                                                 & "@SEKY_FLG,                                                                                            " & vbNewLine _
                                                 & "@BATCH_NO,                                                                                            " & vbNewLine _
                                                 & "@NRS_BR_CD,                                                                                           " & vbNewLine _
                                                 & "@OPE_USER_CD,                                                                                         " & vbNewLine _
                                                 & "@REC_NO,                                                                                              " & vbNewLine _
                                                 & "@MIKAN_INC_FLG,                                                                                       " & vbNewLine _
                                                 & "@SEKY_DATE,                                                                                           " & vbNewLine _
                                                 & "@JOB_NO,                                                                                              " & vbNewLine _
                                                 & "@CUST_CD_L,                                                                                           " & vbNewLine _
                                                 & "@CUST_CD_M,                                                                                           " & vbNewLine _
                                                 & "@CUST_CD_S,                                                                                           " & vbNewLine _
                                                 & "@CUST_CD_SS,                                                                                          " & vbNewLine _
                                                 & "@EXEC_STATE_KB,                                                                                       " & vbNewLine _
                                                 & "@EXEC_RESULT_KB,                                                                                      " & vbNewLine _
                                                 & "@HOKAN_NIYAKU_CALCULATION,                                                                            " & vbNewLine _
                                                 & "@CLOSE_KB,                                                                                            " & vbNewLine _
                                                 & "@INV_DATE_TO,                                                                                         " & vbNewLine _
                                                 & "@MESSAGE_ID,                                                                                          " & vbNewLine _
                                                 & "@EXEC_TIMING_KB,                                                                                      " & vbNewLine _
                                                 & "@KIWARI_KB,                                                                                           " & vbNewLine _
                                                 & "@SYS_ENT_DATE,                                                                                        " & vbNewLine _
                                                 & "@SYS_ENT_TIME,                                                                                        " & vbNewLine _
                                                 & "@SYS_ENT_PGID,                                                                                        " & vbNewLine _
                                                 & "@SYS_ENT_USER,                                                                                        " & vbNewLine _
                                                 & "@SYS_UPD_DATE,                                                                                        " & vbNewLine _
                                                 & "@SYS_UPD_TIME,                                                                                        " & vbNewLine _
                                                 & "@SYS_UPD_PGID,                                                                                        " & vbNewLine _
                                                 & "@SYS_UPD_USER,                                                                                        " & vbNewLine _
                                                 & "@SYS_DEL_FLG)"

    ''' <summary>
    ''' 実行時排他チェック
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_HAITA_HEAD As String = "SELECT                                                         " & vbNewLine _
                                                 & "  COUNT(HEAD.SEKY_FLG) AS SELECT_CNT                               " & vbNewLine _
                                                 & "FROM                                                               " & vbNewLine _
                                                 & "$LM_MST$..G_HN_CALC_WK_HEAD AS HEAD                                " & vbNewLine _
                                                 & "WHERE HEAD.SEKY_FLG = '00'                                         " & vbNewLine _
                                                 & "  AND HEAD.SEKY_FLG = '@SEKY_FLG'                                  " & vbNewLine _
                                                 & "  AND HEAD.NRS_BR_CD = '@NRS_BR_CD'                                " & vbNewLine _
                                                 & "  AND HEAD.INV_DATE_TO = '@INV_DATE_TO'                            " & vbNewLine _
                                                 & "  AND HEAD.CUST_CD_L = '@CUST_CD_L'                                " & vbNewLine _
                                                 & "  AND HEAD.CUST_CD_M = '@CUST_CD_M'                                " & vbNewLine _
                                                 & "  AND HEAD.CUST_CD_S = '@CUST_CD_S'                                " & vbNewLine _
                                                 & "  AND HEAD.CUST_CD_SS = '@CUST_CD_SS'                              " & vbNewLine _
                                                 & "  AND (HEAD.EXEC_STATE_KB = '00'                                   " & vbNewLine _
                                                 & "       OR HEAD.EXEC_STATE_KB = '02')                               " & vbNewLine

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
    ''' 退避用データセット
    ''' </summary>
    ''' <remarks></remarks>
    Private DsBk As DataSet = New DataSet

    ''' <summary>
    ''' バッチ番号
    ''' </summary>
    ''' <remarks></remarks>
    Private BatchNo As String

    ''' <summary>
    ''' デリゲート（非同期実行用）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Delegate Function WriteStringAsyncDelegate( _
         ByVal cmd As SqlCommand) As Integer

    ''' <summary>
    ''' 排他エラー件数
    ''' </summary>
    ''' <remarks></remarks>
    Private _ErrCnt As Integer

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(件数取得)SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_SELECT_COUNT_START)     'SQL構築(カウント用SelectCountStart句)

        With Me._Row
            Me._StrSql.Append(LMG010DAC.SQL_SELECT_DATA_CNT.Replace("@SEKY_DATE", String.Concat("'", .Item("SEKY_DATE").ToString(), "'")))
        End With


        Call Me.SetConditionMasterSQL()                         'パラメータ条件設定
        Me._StrSql.Append(LMG010DAC.SQL_SELECT_COUNT_END)       'SQL構築(カウント用SelectCountEnd句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'タイムアウトの設定
        cmd.CommandTimeout = 60

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG010DAC", "SelectData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SELECT_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>検索処理(データ取得)SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '20210628 ベトナム対応Add
        Dim kbnNm As String = Me.SelectLangSet(ds)
        '20210628 ベトナム対応Add

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQL作成
        With Me._Row
            'SQL構築(データ抽出用Select句)
            Me._StrSql.Append(LMG010DAC.SQL_SELECT_DATA.Replace("@SEKY_DATE", String.Concat("'", .Item("SEKY_DATE").ToString(), "'")))
        End With
        Call Me.SetConditionMasterSQL()                         'パラメータ条件設定
        Me._StrSql.Append(LMG010DAC.SQL_ORDER_BY)               'SQL構築(データ抽出用ORDER BY句)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetKbnNm(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()), kbnNm))

        'タイムアウトの設定
        cmd.CommandTimeout = 60

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG010DAC", "SelectListData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("HOKAN_NIYAKU_CALCULATION", "HOKAN_NIYAKU_CALCULATION")
        map.Add("CLOSE_NM", "CLOSE_NM")
        map.Add("CUST_NM", "CUST_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("KIWARI_NM", "KIWARI_NM")
        map.Add("UPD_DATE_M_CUST", "UPD_DATE_M_CUST")
        map.Add("UPD_TIME_M_CUST", "UPD_TIME_M_CUST")
        map.Add("JOB_NO", "JOB_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("CLOSE_KB", "CLOSE_KB")
        map.Add("KIWARI_KB", "KIWARI_KB")
        map.Add("NRS_BR_CD", "NRS_BR_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG010OUT")

        Return ds

    End Function

    ''' <summary>
    ''' 単価未承認チェック(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectChkApprovalTanka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010CHK_APPROVAL_TANKA")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG010DAC.SQL_SELECT_CHK_APPROVAL_TANKA, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG010DAC", "SelectChkApprovalTanka", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'レコードクリア
        ds.Tables("LMG010CHK_APPROVAL_TANKA").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ROW_NO", "ROW_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG010CHK_APPROVAL_TANKA")

        Return ds

    End Function

    ''' <summary>
    ''' 変動保管料チェック(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectChkVar(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010CHK_VAR")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMG010DAC.SQL_SELECT_CHK_VAR, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.NUMERIC))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", Me._Row.Item("CUST_CD_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", Me._Row.Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG010DAC", "SelectChkVar", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'レコードクリア
        ds.Tables("LMG010CHK_VAR").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("ROW_NO", "ROW_NO")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("SAITEI_HAN_KB", "SAITEI_HAN_KB")
        map.Add("KBN_KEIKA_MONTH_VALUE1", "KBN_KEIKA_MONTH_VALUE1")
        map.Add("KBN_KEIKA_MONTH_VALUE2", "KBN_KEIKA_MONTH_VALUE2")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG010CHK_VAR")

        Return ds

    End Function

#End Region

#Region "前回計算取消"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010INOUT_DEL")

        'DataSetの件数を取得
        Dim inno As Integer = inTbl.Rows.Count - 1

        For i As Integer = 0 To inno
            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(LMG010DAC.SQL_HAITA)     'SQL構築(カウント用Select句)
            Call Me.SetZenkaiKeisanUpSQL()               '条件設定

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                            Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'ログメッセージの設定
            MyBase.Logger.WriteSQLLog("LMG010DAC", String.Concat("CheckHaita", i), cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
            '処理件数の設定
            reader.Read()
            If Convert.ToInt32(reader.Item("SELECT_CNT")) < 1 Then
                MyBase.SetMessage("E262", New String() {String.Concat("該当行の荷主コード ", Me._Row.Item("CUST_CD_L").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_M").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_S").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_SS").ToString())})

                reader.Close()

                Return ds
            End If

            reader.Close()
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 前回計算取消処理（削除取得・更新・削除）呼出
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpDateDelTable(ByVal ds As DataSet) As DataSet

        '請求元在庫・荷主マスタ同一判定処理
        If Me.CheckInvDate(ds) = True Then
            Return ds
        End If

        '画面より取得したデータを退避
        DsBk = ds.Copy()

        'Delete用データの作成
        Call Me.SelectDel(ds)
        '★ UPD START 2011/09/06 SUGA **削除処理順 修正

        ''請求明細データテーブル削除処理
        'Call Me.DelSEQMEISAI(ds)

        ''請求元在庫データ削除処理
        'Call Me.DelSEQZAI(ds)

        ''月末在庫データ削除処理
        'Call Me.DelZAIKZAN(ds)

        ''保管荷役明細印刷テーブル削除処理
        'Call Me.DelSEKMEISAIPRT(ds)

        '請求元在庫データ削除処理
        Call Me.DelSEQZAI(ds)

        '月末在庫データ削除処理
        Call Me.DelZAIKZAN(ds)

        '請求明細データテーブル削除処理
        Call Me.DelSEQMEISAI(ds)

        '保管荷役明細印刷テーブル削除処理
        Call Me.DelSEKMEISAIPRT(ds)
        '★ UPD E N D 2011/09/06 SUGA

        '更新用データ取得
        ds = Me.UpDateDelSelectTable(ds)

        '荷主マスタ更新処理
        Call Me.UpdtMCUST(Me.SetDataset(ds, DsBk))

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日同一判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckInvDate(ByVal ds As DataSet) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010INOUT_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_INV_DATE_MAX)      'SQL構築(データ抽出用Select句)
        Call Me.SetZenkaiKeisanUpSQL()                    '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))
                                                        
        ' 2020/03/04 KAMIZONO ADD START 
        'シミュレーション用タイムアウトの設定
        cmd.CommandTimeout = 600
        ' 2020/03/04 KAMIZONO ADD E N D 

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "CheckInvDate", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        reader.Read()
        Dim answer As Boolean = Convert.ToBoolean(reader.Item("ANSWER"))
        reader.Close()
        If answer = True Then
            MyBase.SetMessage("E371")

        End If

        Return answer

    End Function

    ''' <summary>
    ''' 削除用キー取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectDel(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010INOUT_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_DELETE_KEY)      'SQL構築(データ抽出用)
        Call Me.SetZenkaiKeisanUpSQL()                    '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))
        
        '2021/03/08 KAMIZONO ADD START 
        'シミュレーション用タイムアウトの設定
        cmd.CommandTimeout = 600
        '2021/03/08 KAMIZONO ADD E N D 
        
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "SelectDel", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("INV_DATE_TO", "INV_DATE_TO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMG010DEL")

        reader.Close()

        Return ds

    End Function

    ''' <summary>
    ''' 請求明細データテーブル削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelSEQMEISAI(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010DEL")

        Dim reader As Integer = 0

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_DEL_SEQMEISAI)      'SQL構築(データ削除用)

        Call Me.SetZenkaiKeisanDelSQL()                        '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        'タイムアウトの設定
        cmd.CommandTimeout = 600

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "DelSEQMEISAI", cmd)

        'SQLの発行
        reader = MyBase.GetDeleteResult(cmd)

        Return reader

    End Function

    ''' <summary>
    ''' 請求元在庫データ削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelSEQZAI(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010DEL")

        Dim reader As Integer = 0

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_DEL_SEQZAI)      'SQL構築(データ削除用)

        Call Me.SetZenkaiKeisanDelSQL()                     '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        ' 2020/04/30 KAMIZONO ADD START 
        '群馬保管料荷役料取消タイムアウト緊急対応
        cmd.CommandTimeout = 600
        ' 2020/04/30 KAMIZONO ADD E N D 

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "DelSEQZAI", cmd)

        'SQLの発行
        reader = MyBase.GetDeleteResult(cmd)

        Return reader

    End Function

    ''' <summary>
    ''' 月末在庫データ削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelZAIKZAN(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010DEL")

        Dim reader As Integer = 0

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_DEL_ZAIKZAN)      'SQL構築(データ削除用)

        Call Me.SetZenkaiKeisanDelSQL()                     '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "DelZAIKZAN", cmd)

        'SQLの発行
        reader = MyBase.GetUpdateResult(cmd)

        Return reader

    End Function

    ''' <summary>
    ''' 保管荷役明細印刷テーブル削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DelSEKMEISAIPRT(ByVal ds As DataSet) As Integer

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010DEL")

        Dim reader As Integer = 0

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_DEL_SEKMEISAIPRT)      'SQL構築(データ削除用)

        Call Me.SetZenkaiKeisanDelSQL()                           '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "DelSEKMEISAIPRT", cmd)

        'SQLの発行
        reader = MyBase.GetDeleteResult(cmd)

        Return reader

    End Function

    ''' <summary>
    ''' 前回計算取消処理（取得）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpDateDelSelectTable(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010INOUT_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim sCd As String = Me._Row.Item("CUST_CD_S").ToString
        Dim ssCd As String = Me._Row.Item("CUST_CD_SS").ToString

        'SHINODA 保管荷役前回計算タイムアウト改修 START（まずカウントをとる）
        Dim cnt As Integer = 0

        Me._StrSql.Append("SELECT COUNT(*)  AS SELECT_CNT             " & vbNewLine _
                                     & "  FROM( " & vbNewLine _
                                     & "  SELECT DISTINCT  " & vbNewLine _
                                     & "  ISNULL(SEKY.INV_DATE_TO,MC.HOKAN_NIYAKU_CALCULATION_OLD) AS INV_DATE_TO " & vbNewLine _
                                     & " ,ISNULL(SEKY.JOB_NO,MC.OLD_JOB_NO)                        AS JOB_NO      " & vbNewLine _
                                     & " FROM $LM_TRN$..G_SEKY_TBL AS SEKY " & vbNewLine _
                                     & " LEFT JOIN $LM_MST$..M_GOODS AS GOODS ON " & vbNewLine _
                                     & " GOODS.NRS_BR_CD = SEKY.NRS_BR_CD " & vbNewLine _
                                     & " AND GOODS.GOODS_CD_NRS = SEKY.GOODS_CD_NRS " & vbNewLine _
                                     & " LEFT OUTER JOIN $LM_MST$..M_CUST AS MC ON " & vbNewLine _
                                     & " GOODS.NRS_BR_CD = MC.NRS_BR_CD " & vbNewLine _
                                     & " AND GOODS.CUST_CD_L = MC.CUST_CD_L " & vbNewLine _
                                     & " AND GOODS.CUST_CD_M = MC.CUST_CD_M " & vbNewLine _
                                     & " AND GOODS.CUST_CD_S = MC.CUST_CD_S " & vbNewLine _
                                     & " AND GOODS.CUST_CD_SS = MC.CUST_CD_SS " & vbNewLine _
                                     & " WHERE " & vbNewLine _
                                     & " SEKY.SEKY_FLG ='00' " & vbNewLine _
                                     & " AND SEKY.NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
                                     & " AND GOODS.CUST_CD_L = @CUST_CD_L " & vbNewLine _
                                     & " AND GOODS.CUST_CD_M = @CUST_CD_M " & vbNewLine _
                                     & " AND GOODS.CUST_CD_S = '" & sCd & "' " & vbNewLine _
                                     & " AND GOODS.CUST_CD_SS = '" & ssCd & "' " & vbNewLine _
                                     & "  ) CNT_BASE                                        ")

        Call Me.SetZenkaiKeisanUpSQL()             '条件設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        '2021/03/08 KAMIZONO ADD START 
        'シミュレーション用タイムアウトの設定
        cmd.CommandTimeout = 600
        '2021/03/08 KAMIZONO ADD E N D 

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "UpDateDelSelectTableCount", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
        '処理件数の設定
        reader.Read()
        cnt = Convert.ToInt32(reader.Item("SELECT_CNT"))
        reader.Close()

        If cnt <= 0 Then
            'DataSet設定
            Dim inTbl2 As DataTable = ds.Tables("LMG010INOUT_DEL")
            inTbl2.Clear()
            Dim row As DataRow = inTbl2.NewRow()
            row("HOKAN_NIYAKU_CALCULATION") = ""
            row("HOKAN_NIYAKU_CALCULATION_OLD") = ""
            row("NEW_JOB_NO") = ""
            row("OLD_JOB_NO") = ""
            ds.Tables("LMG010INOUT_DEL").Rows.Add(row)
        Else
            Me._StrSql = New StringBuilder()

            'SQL作成
            'Me._StrSql.Append(LMG010DAC.SQL_SEKY)      'SQL構築(データ抽出用Select句) 2013/10/01 タイムアウト速度UP
            'Me._StrSql.Append("SELECT DISTINCT                                                  " & vbNewLine _
            '                             & "     ISNULL(SEKY.INV_DATE_TO,MC.HOKAN_NIYAKU_CALCULATION_OLD) AS INV_DATE_TO " & vbNewLine _
            '                             & "    ,ISNULL(SEKY.JOB_NO,MC.OLD_JOB_NO)                        AS JOB_NO      " & vbNewLine _
            '                             & "FROM                                   " & vbNewLine _
            '                             & "     (SELECT * FROM $LM_MST$..M_GOODS AS GOODS                   " & vbNewLine _
            '                             & " WHERE    GOODS.NRS_BR_CD = @NRS_BR_CD                           " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_L = @CUST_CD_L                                " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_M = @CUST_CD_M                                " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_S = '" & sCd & "'                               " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_SS = '" & ssCd & "' ) AS GOODS	                 " & vbNewLine _
            '                             & "     LEFT OUTER JOIN $LM_TRN$..G_SEKY_TBL AS SEKY                " & vbNewLine _
            '                             & "       ON SEKY.NRS_BR_CD = GOODS.NRS_BR_CD	                     " & vbNewLine _
            '                             & "      AND SEKY.GOODS_CD_NRS = GOODS.GOODS_CD_NRS                 " & vbNewLine _
            '                             & "      AND SEKY.SEKY_FLG ='00'	                                 " & vbNewLine _
            '                             & "     LEFT OUTER JOIN $LM_MST$..M_CUST AS MC                                      " & vbNewLine _
            '                             & " ON GOODS.NRS_BR_CD = MC.NRS_BR_CD	                             " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_L = MC.CUST_CD_L	                             " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_M = MC.CUST_CD_M	                             " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_S = MC.CUST_CD_S	                             " & vbNewLine _
            '                             & " AND GOODS.CUST_CD_SS = MC.CUST_CD_SS	                         " & vbNewLine _
            '                             & "ORDER BY INV_DATE_TO DESC                                        ")

            Me._StrSql.Append("SELECT DISTINCT                                                  " & vbNewLine _
                                         & "  ISNULL(SEKY.INV_DATE_TO,MC.HOKAN_NIYAKU_CALCULATION_OLD) AS INV_DATE_TO " & vbNewLine _
                                         & " ,ISNULL(SEKY.JOB_NO,MC.OLD_JOB_NO)                        AS JOB_NO      " & vbNewLine _
                                         & " FROM $LM_TRN$..G_SEKY_TBL AS SEKY " & vbNewLine _
                                         & " LEFT JOIN $LM_MST$..M_GOODS AS GOODS ON " & vbNewLine _
                                         & " GOODS.NRS_BR_CD = SEKY.NRS_BR_CD " & vbNewLine _
                                         & " AND GOODS.GOODS_CD_NRS = SEKY.GOODS_CD_NRS " & vbNewLine _
                                         & " LEFT OUTER JOIN $LM_MST$..M_CUST AS MC ON " & vbNewLine _
                                         & " GOODS.NRS_BR_CD = MC.NRS_BR_CD " & vbNewLine _
                                         & " AND GOODS.CUST_CD_L = MC.CUST_CD_L " & vbNewLine _
                                         & " AND GOODS.CUST_CD_M = MC.CUST_CD_M " & vbNewLine _
                                         & " AND GOODS.CUST_CD_S = MC.CUST_CD_S " & vbNewLine _
                                         & " AND GOODS.CUST_CD_SS = MC.CUST_CD_SS " & vbNewLine _
                                         & " WHERE " & vbNewLine _
                                         & " SEKY.SEKY_FLG ='00' " & vbNewLine _
                                         & " AND SEKY.NRS_BR_CD = @NRS_BR_CD " & vbNewLine _
                                         & " AND GOODS.CUST_CD_L = @CUST_CD_L " & vbNewLine _
                                         & " AND GOODS.CUST_CD_M = @CUST_CD_M " & vbNewLine _
                                         & " AND GOODS.CUST_CD_S = '" & sCd & "' " & vbNewLine _
                                         & " AND GOODS.CUST_CD_SS = '" & ssCd & "' " & vbNewLine _
                                         & " ORDER BY INV_DATE_TO DESC                                        ")

            Call Me.SetZenkaiKeisanUpSQL()             '条件設定

            'SQL文のコンパイル
            Dim cmd2 As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                            Me._StrSql.ToString(), _
                                                            Me._Row.Item("NRS_BR_CD").ToString()))

            '2021/03/08 KAMIZONO ADD START 
            'シミュレーション用タイムアウトの設定
            cmd2.CommandTimeout = 600
            '2021/03/08 KAMIZONO ADD E N D 

            '' 2011/0/31 SBS)SUGA ADD START 
            ''タイムアウトの設定
            'cmd.CommandTimeout = 1200
            '' 2011/08/31 SBS)SUGA ADD E N D 

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd2.Parameters.Add(obj)
            Next

            'ログメッセージの設定
            MyBase.Logger.WriteSQLLog("LMG010DAC", "UpDateDelSelectTable", cmd2)

            'SQLの発行
            Dim reader2 As SqlDataReader = MyBase.GetSelectResult(cmd2)

            'DataReader→DataTableへの転記
            Dim map As Hashtable = New Hashtable()

            '取得データの格納先をマッピング
            map.Add("HOKAN_NIYAKU_CALCULATION", "INV_DATE_TO")
            map.Add("HOKAN_NIYAKU_CALCULATION_OLD", "INV_DATE_TO")
            map.Add("NEW_JOB_NO", "JOB_NO")
            map.Add("OLD_JOB_NO", "JOB_NO")

            ds = MyBase.SetSelectResultToDataSet(map, ds, reader2, "LMG010INOUT_DEL")

            reader.Close()

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 荷主マスタ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdtMCUST(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010INOUT_DEL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.SQL_UPDATE_CUST)      'SQL構築(データ更新用Update句)
        Call Me.SetZenkaiKeisanUpSQL()                      '条件設定
        Call Me.SetParamCommonSystemUpd()                 'DB共通（更新項目）設定

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                        Me._StrSql.ToString(), _
                                                        Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", "UpdtMCUST", cmd)

        'SQLの発行
        If MyBase.GetUpdateResult(cmd) < 1 Then
            MyBase.SetMessage("E262", New String() {String.Concat("該当行の荷主コード ", Me._Row.Item("CUST_CD_L").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_M").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_S").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_SS").ToString())})

        End If

        Return ds

    End Function

#End Region

#Region "実行処理"

    ''' <summary>
    ''' 排他チェック（荷主）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaCUST(ByVal ds As DataSet) As DataSet

        'バッチ番号の作成
        Me.BatchNo = String.Concat(MyBase.GetSystemDate(), MyBase.GetSystemTime())

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010INOUT_DEL")

        Dim incTbl As DataTable = ds.Tables("LMG010IN_CALC")

        Dim NewDs As DataSet = ds.Copy()
        Dim outTbl As DataTable = NewDs.Tables("LMG010IN_CALC_BATCH")

        Dim drout As DataRow = incTbl.Rows(0)
        Dim dr As DataRow = Nothing
        'DataSetの件数を取得
        Dim inno As Integer = inTbl.Rows.Count - 1

        ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt START 
        Me._ErrCnt = 0
        ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt END 

        For i As Integer = 0 To inno
            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQL作成
            Me._StrSql.Append(LMG010DAC.SQL_HAITA)     'SQL構築(カウント用Select句)
            Call Me.SetZenkaiKeisanUpSQL()               '条件設定

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm( _
                                                            Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))
            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            'ログメッセージの設定
            MyBase.Logger.WriteSQLLog("LMG010DAC", String.Concat("CheckHaita", i), cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)
            '処理件数の設定
            reader.Read()
            If Convert.ToInt32(reader.Item("SELECT_CNT")) < 1 Then
                '201//08/16 菱刈 検証一覧No8 スタート
                reader.Close()
                '201//08/16 菱刈 検証一覧No8 エンド
                MyBase.SetMessageStore("00" _
                                       , "E262" _
                                       , New String() {String.Concat("該当行の荷主コード ", Me._Row.Item("CUST_CD_L").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_M").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_S").ToString(), "-", _
                                                                      Me._Row.Item("CUST_CD_SS").ToString())} _
                                       , Me._Row.Item("ROW_NO").ToString() _
                                       , "JOB番号" _
                                       , Me._Row.Item("JOB_NO").ToString())
                '201//08/16 菱刈 検証一覧No8 スタート
                'reader.Close()
                '201//08/16 菱刈 検証一覧No8 エンド
                ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt START 
                Me._ErrCnt += 1
                ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt END
            Else
                reader.Close()
                'DataSetのIN情報を取得
                ds = CheckHaitaGHEAD(ds, i)
            End If

        Next

        drout = incTbl.Rows(0)

        dr = outTbl.NewRow()
        dr.Item("SEKY_FLG") = drout.Item("SEKY_FLG").ToString()
        dr.Item("NRS_BR_CD") = drout.Item("NRS_BR_CD").ToString()
        dr.Item("OPE_USER_CD") = drout.Item("OPE_USER_CD").ToString()
        dr.Item("BATCH_NO") = BatchNo

        outTbl.Rows.Add(dr)

        ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt START
        '選択行すべてが排他チェックにひっかかった場合、メッセージを設定
        If inTbl.Rows.Count() = Me._ErrCnt Then
            MyBase.SetMessage("E410", New String() {})
        End If
        ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt END

        Return NewDs

    End Function

    ''' <summary>
    ''' ワークヘッダ排他チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaGHEAD(ByVal ds As DataSet, ByVal row As Integer) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010IN_CALC")

        Me.BatchNo = String.Concat(MyBase.GetSystemDate(), MyBase.GetSystemTime())

        'DataSetの件数を取得
        Dim inno As Integer = inTbl.Rows.Count - 1

        Dim sql As String = String.Empty

        Dim cmd As SqlCommand

        Dim dsout As DataSet = Nothing

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(row)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        sql = String.Empty
        sql = LMG010DAC.SQL_HAITA_HEAD.Replace("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString())
        sql = sql.Replace("@SEKY_FLG", Me._Row.Item("SEKY_FLG").ToString())
        sql = sql.Replace("@INV_DATE_TO", Me._Row.Item("INV_DATE_TO").ToString())
        sql = sql.Replace("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString())
        sql = sql.Replace("@CUST_CD_M", Me._Row.Item("CUST_CD_M").ToString())
        sql = sql.Replace("@CUST_CD_SS", Me._Row.Item("CUST_CD_SS").ToString())
        sql = sql.Replace("@CUST_CD_S", Me._Row.Item("CUST_CD_S").ToString())
        ' 2011/08/31 SBS)SUGA DEL START 
        'sql = sql.Replace("@BATCH_NO", BatchNo)
        ' 2011/08/31 SBS)SUGA DEL START 

        Me._StrSql.Append(sql)     'SQL構築(カウント用Select句)

        'SQL文のコンパイル
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                     , Me._Row.Item("NRS_BR_CD").ToString()))

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", String.Concat("CheckHaitaGHEAD", row), cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        If Convert.ToInt32(reader.Item("SELECT_CNT")) >= 1 Then
            '201//08/16 菱刈 検証一覧No8 スタート
            reader.Close()
            '201//08/16 菱刈 検証一覧No8 エンド
            MyBase.SetMessageStore("00" _
                                   , "E383" _
                                   , New String() {String.Concat("該当行の荷主コード ", Me._Row.Item("CUST_CD_L").ToString(), "-", _
                                                                  Me._Row.Item("CUST_CD_M").ToString(), "-", _
                                                                  Me._Row.Item("CUST_CD_S").ToString(), "-", _
                                                                  Me._Row.Item("CUST_CD_SS").ToString())} _
                                   , Me._Row.Item("ROW_NO").ToString() _
                                   , "JOB番号" _
                                   , Me._Row.Item("JOB_NO").ToString())
            '  reader.Close()
            ' 2011/08/31 SBS)SUGA DEL START 
            dsout = ds
            ' 2011/08/31 SBS)SUGA DEL E N D
            ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt START 
            Me._ErrCnt += 1
            ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt END 
        Else
            reader.Close()

            dsout = InsertWorkHead(ds, row)

        End If

        Return dsout

    End Function

    ''' <summary>
    ''' 実行処理（登録処理）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertWorkHead(ByVal ds As DataSet, ByVal row As Integer) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG010IN_CALC")

        'DataSetの件数を取得
        Dim inno As Integer = inTbl.Rows.Count - 1

        Dim cmd As SqlCommand


        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(row)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMG010DAC.EXECUTE_Insert_SQL)     'SQL構築(Insert句)
        Call Me.SetExecuteSQL(row)                            '条件設定
        Call Me.SetParamCommonSystemIns()

        'SQL文のコンパイル
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        'ログメッセージの設定
        MyBase.Logger.WriteSQLLog("LMG010DAC", String.Concat("InsertWorkHead", row), cmd)

        'SQLの発行
        Dim reader As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 保管料・荷役料計算コントロール呼出処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CtlCalcHokanNiyaku(ByVal ds As DataSet)

        Dim inTbl As DataTable = ds.Tables("LMG010IN_CALC_BATCH")

        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Me._StrSql.Append("$LM_TRN$..CTL_CALC_HOKAN_NIYAKU")

        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString() _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandTimeout = 12000            '20分

        'インプット項目の設定
        '請求フラグ
        Dim SEKY_FLG As SqlParameter = cmd.Parameters.AddWithValue("@IN_SEKY_FLG", DBDataType.CHAR)
        SEKY_FLG.Direction = ParameterDirection.Input
        SEKY_FLG.Value = Me._Row.Item("SEKY_FLG")

        'バッチ番号
        Dim BATCH_NO As SqlParameter = cmd.Parameters.AddWithValue("@IN_BATCH_NO", DBDataType.CHAR)
        BATCH_NO.Direction = ParameterDirection.Input
        BATCH_NO.Value = Me._Row.Item("BATCH_NO")

        '営業所コード
        Dim NRS_BR_CD As SqlParameter = cmd.Parameters.AddWithValue("@IN_NRS_BR_CD", DBDataType.CHAR)
        NRS_BR_CD.Direction = ParameterDirection.Input
        NRS_BR_CD.Value = Me._Row.Item("NRS_BR_CD")

        '実行ユーザーコード
        Dim OPE_USER_CD As SqlParameter = cmd.Parameters.AddWithValue("@IN_OPE_USER_CD", DBDataType.CHAR)
        OPE_USER_CD.Direction = ParameterDirection.Input
        OPE_USER_CD.Value = Me._Row.Item("OPE_USER_CD")

        'パラメータ設定（出力（処理結果））
        Dim OT_RTN_CD As SqlParameter = cmd.Parameters.AddWithValue("@OT_RTN_CD", DBDataType.NVARCHAR)
        OT_RTN_CD.Direction = ParameterDirection.Output
        OT_RTN_CD.Value = String.Empty

        Me.GetSelectResult(cmd)

    End Sub

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", Me.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", Me.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(登録時))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemIns()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", LMConst.FLG.OFF, DBDataType.CHAR))

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール（検索時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '実行ユーザコード判定用
        Dim TANTO_USER_FLG As String = Me._Row.Item("TANTO_USER_FLG").ToString

        ' 対象荷主と営業所コード・荷主コード(大)・(中) で紐付き、
        ' 期割り区分が “セット料金” の単価マスタ非削除レコードが存在する場合は
        ' 請求計算を特定荷主機能で行う荷主として抽出対象から除外する。
        ' (熊本 TSMC 念頭)
        Me._StrSql.Append(String.Concat("AND(NOT EXISTS (                                          ", vbNewLine))
        Me._StrSql.Append(String.Concat("            SELECT                                        ", vbNewLine))
        Me._StrSql.Append(String.Concat("                'X'                                       ", vbNewLine))
        Me._StrSql.Append(String.Concat("            FROM                                          ", vbNewLine))
        Me._StrSql.Append(String.Concat("                $LM_MST$..M_TANKA                         ", vbNewLine))
        Me._StrSql.Append(String.Concat("            WHERE                                         ", vbNewLine))
        Me._StrSql.Append(String.Concat("                M_TANKA.NRS_BR_CD = CUST.NRS_BR_CD        ", vbNewLine))
        Me._StrSql.Append(String.Concat("            AND M_TANKA.CUST_CD_L = CUST.CUST_CD_L        ", vbNewLine))
        Me._StrSql.Append(String.Concat("            AND M_TANKA.CUST_CD_M = CUST.CUST_CD_M        ", vbNewLine))
        Me._StrSql.Append(String.Concat("            AND M_TANKA.KIWARI_KB = '05'    -- セット料金 ", vbNewLine))
        Me._StrSql.Append(String.Concat("            AND M_TANKA.SYS_DEL_FLG = '0'))               ", vbNewLine))

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With Me._Row

            '営業所コード
            whereStr = .Item("NRS_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.NRS_BR_CD = @NRS_BR_CD")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", whereStr, DBDataType.CHAR))
            End If

            '実行ユーザコード
            If "1".Equals(TANTO_USER_FLG) = True Then
                whereStr = .Item("USER_CD").ToString()
                If String.IsNullOrEmpty(whereStr) = False Then
                    'START YANAI 要望番号824
                    'Me._StrSql.Append(" AND TCUST.USER_CD = @USER_CD")
                    Me._StrSql.Append(" AND CUST.TANTO_CD = @USER_CD")
                    'END YANAI 要望番号824
                    Me._StrSql.Append(vbNewLine)
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@USER_CD", whereStr, DBDataType.CHAR))
                End If
            End If

            '今回請求日
            whereStr = .Item("SEKY_DATE").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.HOKAN_NIYAKU_CALCULATION < @SEKY_DATE")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_DATE", whereStr, DBDataType.CHAR))
            End If

            '荷主コード（大）
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_CD_L = @CUST_CD_L ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", whereStr, DBDataType.CHAR))
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '荷主コード（中）
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_CD_M = @CUST_CD_M ")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", whereStr, DBDataType.CHAR))
                'Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(whereStr, "%"), DBDataType.CHAR))
            End If

            '締日区分
            whereStr = .Item("CLOSE_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND SEIQTO.CLOSE_KB = @CLOSE_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", whereStr, DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND CUST.CUST_NM_L + CUST.CUST_NM_M + CUST.CUST_NM_S + CUST.CUST_NM_SS LIKE @CUST_NM")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_NM", String.Concat("%", whereStr, "%"), DBDataType.NVARCHAR))
            End If

            '期割区分
            whereStr = .Item("KIWARI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND GOODS.KIWARI_KB = @KIWARI_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIWARI_KB", whereStr, DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 前回計算取消用パラメータ設定（Select・Update用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZenkaiKeisanUpSQL()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_DATE_M_CUST", .Item("UPD_DATE_M_CUST").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UPD_TIME_M_CUST", .Item("UPD_TIME_M_CUST").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", .Item("JOB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", .Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION", .Item("HOKAN_NIYAKU_CALCULATION").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION_OLD", .Item("HOKAN_NIYAKU_CALCULATION_OLD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_JOB_NO", .Item("NEW_JOB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OLD_JOB_NO", .Item("OLD_JOB_NO").ToString(), DBDataType.CHAR))

        End With
    End Sub

    ''' <summary>
    ''' 前回計算取消用パラメータ設定（Delete用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetZenkaiKeisanDelSQL()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", .Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))

        End With
    End Sub

    ''' <summary>
    ''' 実行用パラメータ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetExecuteSQL(ByVal i As Integer)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        With Me._Row

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_FLG", .Item("SEKY_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BATCH_NO", Me.BatchNo, DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@OPE_USER_CD", .Item("OPE_USER_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@REC_NO", Convert.ToString(i + 1).PadLeft(9, Convert.ToChar("0")), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MIKAN_INC_FLG", .Item("MIKAN_INC_FLG").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEKY_DATE", .Item("SEKY_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@JOB_NO", .Item("JOB_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXEC_STATE_KB", .Item("EXEC_STATE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXEC_RESULT_KB", .Item("EXEC_RESULT_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@HOKAN_NIYAKU_CALCULATION", .Item("HOKAN_NIYAKU_CALCULATION").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CLOSE_KB", .Item("CLOSE_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INV_DATE_TO", .Item("INV_DATE_TO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@MESSAGE_ID", .Item("MESSAGE_ID").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@EXEC_TIMING_KB", .Item("EXEC_TIMING_KB").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KIWARI_KB", .Item("KIWARI_KB").ToString(), DBDataType.CHAR))

        End With
    End Sub

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
    ''' 画面取得DS・SQLDSをデータセットへ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="dsbk"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataset(ByVal ds As DataSet, ByVal dsbk As DataSet) As DataSet

        Dim newds As DataSet = dsbk.Copy()
        newds.Clear()
        Dim dt As DataTable = newds.Tables("LMG010INOUT_DEL")
        Dim dr As DataRow = dt.NewRow()
        Dim getdr As DataRow = Nothing
        Dim getHokanNiyakuCalcDate As String = String.Empty

        Dim drbk As DataRow = Nothing

        For i As Integer = 0 To ds.Tables("LMG010INOUT_DEL").Rows.Count - 1
            getdr = ds.Tables("LMG010INOUT_DEL").Rows(i)
            If i = 0 Then
                drbk = dsbk.Tables("LMG010INOUT_DEL").Rows(0)
                dr.Item("NRS_BR_CD") = drbk.Item("NRS_BR_CD").ToString()
                dr.Item("JOB_NO") = drbk.Item("JOB_NO")
                dr.Item("CUST_CD_L") = drbk.Item("CUST_CD_L")
                dr.Item("CUST_CD_M") = drbk.Item("CUST_CD_M")
                dr.Item("CUST_CD_S") = drbk.Item("CUST_CD_S")
                dr.Item("CUST_CD_SS") = drbk.Item("CUST_CD_SS")
                dr.Item("INV_DATE_TO") = drbk.Item("INV_DATE_TO")
                dr.Item("UPD_DATE_M_CUST") = drbk.Item("UPD_DATE_M_CUST")
                dr.Item("UPD_TIME_M_CUST") = drbk.Item("UPD_TIME_M_CUST")
                ' 2011/08/31 SBS)SUGA ADD START 
                getHokanNiyakuCalcDate = getdr.Item("HOKAN_NIYAKU_CALCULATION").ToString
                If String.IsNullOrEmpty(getHokanNiyakuCalcDate) Then
                    ' 一度も計算されていないデータに対して処理された場合、
                    ' (荷主マスタ)最終計算日(※) - 1ヶ月 に締日区分 を考慮した日にち(GUIにて設定されたもの)を設定する
                    ' (※)今回取消対象となる最終計算日
                    dr.Item("HOKAN_NIYAKU_CALCULATION") = drbk.Item("HOKAN_NIYAKU_CALCULATION_YOBI")
                Else
                    dr.Item("HOKAN_NIYAKU_CALCULATION") = getHokanNiyakuCalcDate
                End If
                ' 2011/08/31 SBS)SUGA ADD E N D
                ' 2011/08/31 SBS)SUGA UPD START
                'dr.Item("HOKAN_NIYAKU_CALCULATION_OLD") = getdr.Item("HOKAN_NIYAKU_CALCULATION_OLD")
                dr.Item("HOKAN_NIYAKU_CALCULATION_OLD") = String.Empty
                ' 2011/08/31 SBS)SUGA UPD E N D
                dr.Item("NEW_JOB_NO") = getdr.Item("NEW_JOB_NO")
                ' 2011/08/31 SBS)SUGA UPD START
                'dr.Item("OLD_JOB_NO") = getdr.Item("OLD_JOB_NO")
                dr.Item("OLD_JOB_NO") = String.Empty
                ' 2011/08/31 SBS)SUGA UPD E N D
            ElseIf i = 1 Then
                dr.Item("HOKAN_NIYAKU_CALCULATION_OLD") = getdr.Item("HOKAN_NIYAKU_CALCULATION_OLD")
                dr.Item("OLD_JOB_NO") = getdr.Item("OLD_JOB_NO")
                Exit For
            End If

        Next

        dt.Rows.Add(dr)

        Return newds
    End Function

#End Region

#Region "言語取得"
    '20210628 ベトナム対応 add start
    ''' <summary>
    ''' 言語の取得(区分マスタの区分項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectLangSet(ByVal ds As DataSet) As String

        'DataSetのIN情報を取得
        Dim inTbl As DataTable
        inTbl = ds.Tables("LMG010IN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL作成

        'SQL構築
        Me._StrSql.AppendLine("SELECT                                    ")
        Me._StrSql.AppendLine(" CASE WHEN KBN_NM1 = ''    THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      WHEN KBN_NM1 IS NULL THEN 'KBN_NM1' ")
        Me._StrSql.AppendLine("      ELSE KBN_NM1 END      AS KBN_NM     ")
        Me._StrSql.AppendLine("FROM $LM_MST$..Z_KBN                      ")
        Me._StrSql.AppendLine("WHERE KBN_GROUP_CD = 'K025'               ")
        Me._StrSql.AppendLine("  AND RIGHT(KBN_CD,1 ) = @LANG            ")
        Me._StrSql.AppendLine("  AND SYS_DEL_FLG  = '0'                  ")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        Me._SqlPrmList.Add(GetSqlParameter("@LANG", Me._Row.Item("LANG_FLG").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMG010DAC", "SelectLangset", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        Dim str As String = "KBN_NM1"

        If reader.Read() = True Then
            str = Convert.ToString(reader("KBN_NM"))
        End If
        reader.Close()

        Return str

    End Function

    ''' <summary>
    ''' 区分項目設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetKbnNm(ByVal sql As String, ByVal kbnNm As String) As String

        '区分項目変換設定
        sql = sql.Replace("#KBN#", kbnNm)

        Return sql

    End Function
    '20210628 ベトナム対応 add End

#End Region

#End Region

End Class
