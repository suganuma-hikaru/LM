' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI950    : 運賃データ確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI950DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI950DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI950IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMI950OUT"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SENDUNCHIN_TARGET As String = "LMI950SENDUNCHIN_TARGET"

    ''' <summary>
    ''' 処理制御データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_PROC_CTRL_DATA As String = "LMI950PROC_CTRL_DATA"

    ''' <summary>
    ''' 運賃明細送信データ元データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SENDUNCHIN_DATA As String = "LMI950SENDUNCHIN_DATA"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMI950DAC"

#End Region '制御用

#Region "検索処理SQL"

    Private Const SQL_SELECT_SEARCH_COUNT_S As String = _
          "SELECT COUNT(*) AS REC_CNT                    " & vbNewLine _
        & "  FROM (                                      " & vbNewLine

    Private Const SQL_SELECT_SEARCH_COUNT_E As String = _
          " ) SUB                                        " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA As String =
          "SELECT                                                                                                    " & vbNewLine _
        & "  CASE MAIN.SHORI_KB                                                                                      " & vbNewLine _
        & "       WHEN '1' THEN '新規'                                                                               " & vbNewLine _
        & "       WHEN '2' THEN '変更'                                                                               " & vbNewLine _
        & "       WHEN '3' THEN '削除'                                                                               " & vbNewLine _
        & "  END AS SHORI_KB                                                                                         " & vbNewLine _
        & " ,CASE WHEN MAIN.DUP_CNT > 0 THEN                                                                         " & vbNewLine _
        & "       CASE WHEN MAIN.DUP_ROWNUM = 1                                                                      " & vbNewLine _
        & "            THEN '正常'                                                                                   " & vbNewLine _
        & "            --▼複数行ある場合2行目以降は送信後に受信したデータ                                           " & vbNewLine _
        & "            ELSE '保留'                                                                                   " & vbNewLine _
        & "       END                                                                                                " & vbNewLine _
        & "  END AS HORYU                                                                                            " & vbNewLine _
        & " ,CASE MAIN.SEND                                                                                          " & vbNewLine _
        & "       WHEN 0 THEN '未'                                                                                   " & vbNewLine _
        & "       WHEN 1 THEN '済'                                                                                   " & vbNewLine _
        & "  END AS SEND                                                                                             " & vbNewLine _
        & " ,MAIN.OUTKA_NO_L                                                                                         " & vbNewLine _
        & " ,MAIN.DEST_NM                                                                                            " & vbNewLine _
        & " ,MAIN.DECI_KYORI                                                                                         " & vbNewLine _
        & " ,MAIN.DECI_WT                                                                                            " & vbNewLine _
        & " ,MAIN.DECI_UNCHIN                                                                                        " & vbNewLine _
        & " ,MAIN.HOUKOKU_UNCHIN                                                                                     " & vbNewLine _
        & " ,MAIN.SHUKKA_DATE                                                                                        " & vbNewLine _
        & " ,MAIN.KOJO_KANRI_NO                                                                                      " & vbNewLine _
        & " ,MAIN.CRT_DATE                                                                                           " & vbNewLine _
        & " ,MAIN.FILE_NAME                                                                                          " & vbNewLine _
        & " ,MAIN.REC_NO                                                                                             " & vbNewLine _
        & " ,MAIN.UNSO_NO_L                                                                                          " & vbNewLine _
        & " ,MAIN.UNSO_L_UPD_DATE                                                                                    " & vbNewLine _
        & " ,MAIN.UNSO_L_UPD_TIME                                                                                    " & vbNewLine _
        & " ,MAIN.UNCHIN_UPD_DATE                                                                                    " & vbNewLine _
        & " ,MAIN.UNCHIN_UPD_TIME                                                                                    " & vbNewLine _
        & "FROM (                                                                                                    " & vbNewLine _
        & "    SELECT                                                                                                " & vbNewLine _
        & "      OUTKAJOHO.SHORI_KB                                                                                  " & vbNewLine _
        & "      --▼削除フラグ=1かつ処理区分=3(削除)の同一工場管理番号のデータ件数                                  " & vbNewLine _
        & "     ,(SELECT COUNT(*)                                                                                    " & vbNewLine _
        & "         FROM $LM_TRN$..H_OUTKAJOHOEDI_NSN  OUTKAJOHO_DEL                                                 " & vbNewLine _
        & "        WHERE OUTKAJOHO_DEL.KOJO_KANRI_NO = OUTKA_L.CUST_ORD_NO                                           " & vbNewLine _
        & "          AND OUTKAJOHO_DEL.SHORI_KB = '3'                                                                " & vbNewLine _
        & "          AND OUTKAJOHO_DEL.SYS_DEL_FLG = '1') AS DEL_CNT                                                 " & vbNewLine _
        & "      --▼削除フラグ=0の同一工場管理番号のデータ件数                                                      " & vbNewLine _
        & "     ,(SELECT COUNT(*)                                                                                    " & vbNewLine _
        & "         FROM $LM_TRN$..H_OUTKAJOHOEDI_NSN OUTKAJOHO_CNT                                                  " & vbNewLine _
        & "        WHERE OUTKAJOHO_CNT.KOJO_KANRI_NO = OUTKAJOHO.KOJO_KANRI_NO                                       " & vbNewLine _
        & "          AND OUTKAJOHO_CNT.SYS_DEL_FLG = '0') AS DUP_CNT                                                 " & vbNewLine _
        & "      --▼同一工場管理番号のデータに古い順に連番を振る                                                    " & vbNewLine _
        & "     ,ROW_NUMBER() OVER(PARTITION BY OUTKA_L.OUTKA_NO_L, OUTKAJOHO.KOJO_KANRI_NO                          " & vbNewLine _
        & "                        ORDER BY OUTKAJOHO.CRT_DATE, OUTKAJOHO.FILE_NAME, OUTKAJOHO.REC_NO) AS DUP_ROWNUM " & vbNewLine _
        & "     ,CASE WHEN SENDUNCHIN.KOJO_KANRI_NO IS NULL                                                          " & vbNewLine _
        & "           THEN 0                                                                                         " & vbNewLine _
        & "           ELSE 1                                                                                         " & vbNewLine _
        & "      END AS SEND                                                                                         " & vbNewLine _
        & "     ,OUTKA_L.OUTKA_NO_L                                                                                  " & vbNewLine _
        & "     ,M_DEST.DEST_NM                                                                                      " & vbNewLine _
        & "     ,MAX(UNCHIN.DECI_KYORI) AS DECI_KYORI                                                                " & vbNewLine _
        & "     ,UNSO_L.UNSO_WT AS DECI_WT                                                                           " & vbNewLine _
        & "     ,ROUND(SUM(UNCHIN.DECI_UNCHIN                                                                        " & vbNewLine _
        & "              + UNCHIN.DECI_CITY_EXTC                                                                     " & vbNewLine _
        & "              + UNCHIN.DECI_WINT_EXTC                                                                     " & vbNewLine _
        & "              + UNCHIN.DECI_RELY_EXTC                                                                     " & vbNewLine _
        & "              + UNCHIN.DECI_TOLL                                                                          " & vbNewLine _
        & "              + UNCHIN.DECI_INSU), 0) AS DECI_UNCHIN                                                      " & vbNewLine _
        & "     ,ROUND(SUM(UNCHIN.KANRI_UNCHIN                                                                       " & vbNewLine _
        & "              + UNCHIN.KANRI_CITY_EXTC                                                                    " & vbNewLine _
        & "              + UNCHIN.KANRI_WINT_EXTC                                                                    " & vbNewLine _
        & "              + UNCHIN.KANRI_RELY_EXTC                                                                    " & vbNewLine _
        & "              + UNCHIN.KANRI_TOLL                                                                         " & vbNewLine _
        & "              + UNCHIN.KANRI_INSU), 0) AS HOUKOKU_UNCHIN                                                  " & vbNewLine _
        & "     ,OUTKAJOHO.SHUKKA_DATE                                                                               " & vbNewLine _
        & "     ,OUTKAJOHO.KOJO_KANRI_NO                                                                             " & vbNewLine _
        & "     ,OUTKAJOHO.CRT_DATE                                                                                  " & vbNewLine _
        & "     ,OUTKAJOHO.FILE_NAME                                                                                 " & vbNewLine _
        & "     ,OUTKAJOHO.REC_NO                                                                                    " & vbNewLine _
        & "     ,UNSO_L.UNSO_NO_L                                                                                    " & vbNewLine _
        & "     ,UNSO_L.SYS_UPD_DATE AS UNSO_L_UPD_DATE                                                              " & vbNewLine _
        & "     ,UNSO_L.SYS_UPD_TIME AS UNSO_L_UPD_TIME                                                              " & vbNewLine _
        & "     ,MAX(UNCHIN.SYS_UPD_DATE) AS UNCHIN_UPD_DATE                                                         " & vbNewLine _
        & "     ,MAX(UNCHIN.SYS_UPD_TIME) AS UNCHIN_UPD_TIME                                                         " & vbNewLine _
        & "      FROM $LM_TRN$..C_OUTKA_L  OUTKA_L                                                                   " & vbNewLine _
        & "      LEFT JOIN $LM_TRN$..H_OUTKAJOHOEDI_NSN  OUTKAJOHO                                                   " & vbNewLine _
        & "        ON OUTKAJOHO.KOJO_KANRI_NO = OUTKA_L.CUST_ORD_NO                                                  " & vbNewLine _
        & "       AND OUTKAJOHO.SYS_DEL_FLG = '0'                                                                    " & vbNewLine _
        & "     INNER JOIN $LM_TRN$..F_UNSO_L  UNSO_L                                                                " & vbNewLine _
        & "        ON UNSO_L.NRS_BR_CD = OUTKA_L.NRS_BR_CD                                                           " & vbNewLine _
        & "       AND UNSO_L.INOUTKA_NO_L = OUTKA_L.OUTKA_NO_L                                                       " & vbNewLine _
        & "       AND UNSO_L.MOTO_DATA_KB = '20' --20:出荷                                                           " & vbNewLine _
        & "       AND UNSO_L.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
        & "     INNER JOIN $LM_TRN$..F_UNCHIN_TRS  UNCHIN                                                            " & vbNewLine _
        & "        ON UNCHIN.NRS_BR_CD = UNSO_L.NRS_BR_CD                                                            " & vbNewLine _
        & "       AND UNCHIN.UNSO_NO_L = UNSO_L.UNSO_NO_L                                                            " & vbNewLine _
        & "       AND UNCHIN.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
        & "      LEFT JOIN $LM_TRN$..H_SENDUNCHINEDI_NSN SENDUNCHIN                                                  " & vbNewLine _
        & "        ON SENDUNCHIN.KOJO_KANRI_NO = OUTKAJOHO.KOJO_KANRI_NO                                             " & vbNewLine _
        & "       AND SENDUNCHIN.SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
        & "      LEFT JOIN $LM_MST$..M_DEST                                                                          " & vbNewLine _
        & "        ON M_DEST.NRS_BR_CD = UNSO_L.NRS_BR_CD                                                            " & vbNewLine _
        & "       AND M_DEST.CUST_CD_L = UNSO_L.CUST_CD_L                                                            " & vbNewLine _
        & "       AND M_DEST.DEST_CD = UNSO_L.DEST_CD                                                                " & vbNewLine _
        & "       AND M_DEST.SYS_DEL_FLG = '0'                                                                       " & vbNewLine _
        & "     WHERE OUTKA_L.SYS_DEL_FLG = '0'                                                                      " & vbNewLine _
        & "       AND OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                                                 " & vbNewLine _
        & "       AND OUTKA_L.CUST_CD_L = @CUST_CD_L                                                                 " & vbNewLine _
        & "       AND OUTKA_L.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE                                                     " & vbNewLine _
        & "     GROUP BY                                                                                             " & vbNewLine _
        & "      UNCHIN.UNSO_NO_L                                                                                    " & vbNewLine _
        & "     ,OUTKAJOHO.SHORI_KB                                                                                  " & vbNewLine _
        & "     ,OUTKA_L.CUST_ORD_NO                                                                                 " & vbNewLine _
        & "     ,SENDUNCHIN.KOJO_KANRI_NO                                                                            " & vbNewLine _
        & "     ,OUTKA_L.OUTKA_NO_L                                                                                  " & vbNewLine _
        & "     ,M_DEST.DEST_NM                                                                                      " & vbNewLine _
        & "     ,UNSO_L.UNSO_WT                                                                                      " & vbNewLine _
        & "     ,OUTKAJOHO.SHUKKA_DATE                                                                               " & vbNewLine _
        & "     ,OUTKAJOHO.KOJO_KANRI_NO                                                                             " & vbNewLine _
        & "     ,OUTKAJOHO.CRT_DATE                                                                                  " & vbNewLine _
        & "     ,OUTKAJOHO.FILE_NAME                                                                                 " & vbNewLine _
        & "     ,OUTKAJOHO.REC_NO                                                                                    " & vbNewLine _
        & "     ,UNSO_L.UNSO_NO_L                                                                                    " & vbNewLine _
        & "     ,UNSO_L.SYS_UPD_DATE                                                                                 " & vbNewLine _
        & "     ,UNSO_L.SYS_UPD_TIME                                                                                 " & vbNewLine _
        & " ) MAIN                                                                                                   " & vbNewLine _
        & "--▼同一工場管理番号の全データが削除フラグ=1、かつ、処理区分=3(削除)のデータが存在する場合は除外する      " & vbNewLine _
        & "WHERE MAIN.DUP_CNT > 0 OR MAIN.DEL_CNT = 0                                                                " & vbNewLine

    Private Const SQL_ORDER_SEARCH As String = _
          "ORDER BY                                                                                                  " & vbNewLine _
        & "  MAIN.SEND                                                                                               " & vbNewLine _
        & " ,MAIN.OUTKA_NO_L                                                                                         " & vbNewLine

#End Region '検索処理SQL

#Region "実績作成処理SQL"

    Private Const SQL_SELECT_KOJO_KANRI_NO_COUNT As String = _
          "SELECT COUNT(*) AS REC_CNT                      " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAJOHOEDI_NSN  OUTKAJOHO  " & vbNewLine _
        & " WHERE OUTKAJOHO.KOJO_KANRI_NO = @KOJO_KANRI_NO " & vbNewLine _
        & "   AND OUTKAJOHO.SYS_DEL_FLG = '0'              " & vbNewLine

    Private Const SQL_SELECT_SENDUNCHIN_DATA As String =
          "SELECT                                                      " & vbNewLine _
        & "  OUTKA_L.OUTKA_NO_L                                        " & vbNewLine _
        & " ,OUTKAJOHO.DENSO_MOTO AS DENSO_SAKI                        " & vbNewLine _
        & " ,OUTKAJOHO.DENSO_SAKI AS DENSO_MOTO                        " & vbNewLine _
        & " ,OUTKAJOHO.SHORI_KB                                        " & vbNewLine _
        & " ,OUTKAJOHO.TORIHIKI_KB                                     " & vbNewLine _
        & " ,OUTKAJOHO.MOTOUKE_CD                                      " & vbNewLine _
        & " ,OUTKAJOHO.CUST_CD                                         " & vbNewLine _
        & " ,OUTKAJOHO.CUST_BUSHO                                      " & vbNewLine _
        & " ,OUTKAJOHO.EIGYO_SASHIZU                                   " & vbNewLine _
        & " ,OUTKAJOHO.KOJO_KANRI_NO                                   " & vbNewLine _
        & " ,OUTKAJOHO.SHUKKA_DATE                                     " & vbNewLine _
        & " ,OUTKAJOHO.NONYU_DATE                                      " & vbNewLine _
        & " ,OUTKAJOHO.YUSO_CD                                         " & vbNewLine _
        & " ,OUTKAJOHO.SEIKYU_SAKI                                     " & vbNewLine _
        & " ,CASE MIN(UNCHIN.TAX_KB)                                   " & vbNewLine _
        & "    WHEN '01' THEN '1'                                      " & vbNewLine _
        & "    WHEN '02' THEN '3'                                      " & vbNewLine _
        & "    WHEN '03' THEN '2'                                      " & vbNewLine _
        & "    ELSE ''                                                 " & vbNewLine _
        & "  END AS TAX_KB                                             " & vbNewLine _
        & " ,CASE WHEN MIN(UNCHIN.TAX_KB) = '01'                       " & vbNewLine _
        & "    THEN M_TAX.TAX_RATE * 100                               " & vbNewLine _
        & "    ELSE 0                                                  " & vbNewLine _
        & "  END AS TAX_RATE                                           " & vbNewLine _
        & " ,CASE WHEN MIN(UNCHIN.TAX_KB) = '01'                       " & vbNewLine _
        & "    THEN ROUND(SUM(UNCHIN.DECI_UNCHIN                       " & vbNewLine _
        & "    + UNCHIN.DECI_CITY_EXTC                                 " & vbNewLine _
        & "    + UNCHIN.DECI_WINT_EXTC                                 " & vbNewLine _
        & "    + UNCHIN.DECI_RELY_EXTC                                 " & vbNewLine _
        & "    + UNCHIN.DECI_TOLL                                      " & vbNewLine _
        & "    + UNCHIN.DECI_INSU) * M_TAX.TAX_RATE, 0)                " & vbNewLine _
        & "    ELSE 0                                                  " & vbNewLine _
        & "  END AS ZEI_GAKU_UNCHIN                                    " & vbNewLine _
        & " ,ROUND(SUM(UNCHIN.DECI_UNCHIN                              " & vbNewLine _
        & "    + UNCHIN.DECI_CITY_EXTC                                 " & vbNewLine _
        & "    + UNCHIN.DECI_WINT_EXTC                                 " & vbNewLine _
        & "    + UNCHIN.DECI_RELY_EXTC                                 " & vbNewLine _
        & "    + UNCHIN.DECI_TOLL                                      " & vbNewLine _
        & "    + UNCHIN.DECI_INSU), 0) AS DECI_UNCHIN                  " & vbNewLine _
        & " ,UNSO_L.SYS_UPD_DATE AS UNSO_L_UPD_DATE                    " & vbNewLine _
        & " ,UNSO_L.SYS_UPD_TIME AS UNSO_L_UPD_TIME                    " & vbNewLine _
        & " ,MAX(UNCHIN.SYS_UPD_DATE) AS UNCHIN_UPD_DATE               " & vbNewLine _
        & " ,MAX(UNCHIN.SYS_UPD_TIME) AS UNCHIN_UPD_TIME               " & vbNewLine _
        & "  FROM $LM_TRN$..C_OUTKA_L  OUTKA_L                         " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAJOHOEDI_NSN  OUTKAJOHO         " & vbNewLine _
        & "    ON OUTKAJOHO.KOJO_KANRI_NO = OUTKA_L.CUST_ORD_NO        " & vbNewLine _
        & "   AND OUTKAJOHO.SYS_DEL_FLG = '0'                          " & vbNewLine _
        & " INNER JOIN $LM_TRN$..F_UNSO_L  UNSO_L                      " & vbNewLine _
        & "    ON UNSO_L.NRS_BR_CD = OUTKA_L.NRS_BR_CD                 " & vbNewLine _
        & "   AND UNSO_L.INOUTKA_NO_L = OUTKA_L.OUTKA_NO_L             " & vbNewLine _
        & "   AND UNSO_L.MOTO_DATA_KB = '20' --20:出荷                 " & vbNewLine _
        & "   AND UNSO_L.SYS_DEL_FLG = '0'                             " & vbNewLine _
        & " INNER JOIN $LM_TRN$..F_UNCHIN_TRS  UNCHIN                  " & vbNewLine _
        & "    ON UNCHIN.NRS_BR_CD = UNSO_L.NRS_BR_CD                  " & vbNewLine _
        & "   AND UNCHIN.UNSO_NO_L = UNSO_L.UNSO_NO_L                  " & vbNewLine _
        & "   AND UNCHIN.SYS_DEL_FLG = '0'                             " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN KBN_Z001                        " & vbNewLine _
        & "    ON KBN_Z001.KBN_GROUP_CD = 'Z001'                       " & vbNewLine _
        & "   AND KBN_Z001.KBN_CD = '01'                               " & vbNewLine _
        & "   AND KBN_Z001.SYS_DEL_FLG  = '0'                          " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_TAX                                 " & vbNewLine _
        & "    ON M_TAX.TAX_CD = KBN_Z001.KBN_NM3                      " & vbNewLine _
        & "   AND M_TAX.START_DATE =                                   " & vbNewLine _
        & "       (SELECT MAX(START_DATE)                              " & vbNewLine _
        & "          FROM $LM_MST$..M_TAX TAX_S_DATE                   " & vbNewLine _
        & "         WHERE TAX_S_DATE.TAX_CD = KBN_Z001.KBN_NM3         " & vbNewLine _
        & "           AND TAX_S_DATE.START_DATE <= CASE UNCHIN.UNTIN_CALCULATION_KB           " & vbNewLine _
        & "                                             WHEN '01' THEN UNSO_L.OUTKA_PLAN_DATE " & vbNewLine _
        & "                                             WHEN '02' THEN UNSO_L.ARR_PLAN_DATE   " & vbNewLine _
        & "                                        END                 " & vbNewLine _
        & "           AND TAX_S_DATE.SYS_DEL_FLG = '0'                 " & vbNewLine _
        & "       )                                                    " & vbNewLine _
        & "   AND M_TAX.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDUNCHINEDI_NSN SENDUNCHIN        " & vbNewLine _
        & "    ON SENDUNCHIN.KOJO_KANRI_NO = OUTKAJOHO.KOJO_KANRI_NO   " & vbNewLine _
        & "   AND SENDUNCHIN.SYS_DEL_FLG = '0'                         " & vbNewLine _
        & " WHERE OUTKA_L.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & "   AND OUTKA_L.NRS_BR_CD = @NRS_BR_CD                       " & vbNewLine _
        & "   AND OUTKA_L.CUST_CD_L = @CUST_CD_L                       " & vbNewLine _
        & "   AND OUTKA_L.OUTKA_PLAN_DATE = @OUTKA_PLAN_DATE           " & vbNewLine _
        & "   AND OUTKA_L.OUTKA_NO_L = @OUTKA_NO_L                     " & vbNewLine _
        & "   AND OUTKAJOHO.CRT_DATE = @CRT_DATE                       " & vbNewLine _
        & "   AND OUTKAJOHO.FILE_NAME = @FILE_NAME                     " & vbNewLine _
        & "   AND OUTKAJOHO.REC_NO = @REC_NO                           " & vbNewLine _
        & "   AND OUTKAJOHO.KOJO_KANRI_NO = @KOJO_KANRI_NO             " & vbNewLine _
        & "   AND UNSO_L.UNSO_NO_L = @UNSO_NO_L                        " & vbNewLine _
        & "   AND SENDUNCHIN.KOJO_KANRI_NO IS NULL                     " & vbNewLine _
        & " GROUP BY                                                   " & vbNewLine _
        & "  UNCHIN.UNSO_NO_L                                          " & vbNewLine _
        & " ,OUTKA_L.OUTKA_NO_L                                        " & vbNewLine _
        & " ,OUTKAJOHO.DENSO_MOTO                                      " & vbNewLine _
        & " ,OUTKAJOHO.DENSO_SAKI                                      " & vbNewLine _
        & " ,OUTKAJOHO.SHORI_KB                                        " & vbNewLine _
        & " ,OUTKAJOHO.TORIHIKI_KB                                     " & vbNewLine _
        & " ,OUTKAJOHO.MOTOUKE_CD                                      " & vbNewLine _
        & " ,OUTKAJOHO.CUST_CD                                         " & vbNewLine _
        & " ,OUTKAJOHO.CUST_BUSHO                                      " & vbNewLine _
        & " ,OUTKAJOHO.EIGYO_SASHIZU                                   " & vbNewLine _
        & " ,OUTKAJOHO.KOJO_KANRI_NO                                   " & vbNewLine _
        & " ,OUTKAJOHO.SHUKKA_DATE                                     " & vbNewLine _
        & " ,OUTKAJOHO.NONYU_DATE                                      " & vbNewLine _
        & " ,OUTKAJOHO.YUSO_CD                                         " & vbNewLine _
        & " ,OUTKAJOHO.SEIKYU_SAKI                                     " & vbNewLine _
        & " ,M_TAX.TAX_RATE                                            " & vbNewLine _
        & " ,UNSO_L.SYS_UPD_DATE                                       " & vbNewLine _
        & " ,UNSO_L.SYS_UPD_TIME                                       " & vbNewLine _
        & " ORDER BY                                                   " & vbNewLine _
        & "  OUTKA_L.OUTKA_NO_L                                        " & vbNewLine

    Private Const SQL_INSET_SENDUNCHIN As String = _
          "INSERT INTO $LM_TRN$..H_SENDUNCHINEDI_NSN( " & vbNewLine _
        & " OUTKA_CTL_NO                              " & vbNewLine _
        & ",DATA_SHURUI                               " & vbNewLine _
        & ",SAKUSEI_DATE                              " & vbNewLine _
        & ",SAKUSEI_TIME                              " & vbNewLine _
        & ",DENSO_SAKI                                " & vbNewLine _
        & ",DENSO_MOTO                                " & vbNewLine _
        & ",SHORI_KB                                  " & vbNewLine _
        & ",TORIHIKI_KB                               " & vbNewLine _
        & ",MOTOUKE_CD                                " & vbNewLine _
        & ",CUST_CD                                   " & vbNewLine _
        & ",CUST_BUSHO                                " & vbNewLine _
        & ",EIGYO_SASHIZU                             " & vbNewLine _
        & ",KOJO_KANRI_NO                             " & vbNewLine _
        & ",SHUKKA_DATE                               " & vbNewLine _
        & ",NONYU_DATE                                " & vbNewLine _
        & ",ZEI_KB_UNCHIN                             " & vbNewLine _
        & ",ZEI_RITSU_UNCHIN                          " & vbNewLine _
        & ",ZEI_GAKU_UNCHIN                           " & vbNewLine _
        & ",KINGAKU_UNCHIN                            " & vbNewLine _
        & ",YURYODORO_GAKU                            " & vbNewLine _
        & ",ZEI_KB_YUSOHOKEN                          " & vbNewLine _
        & ",ZEI_RITSU_YUSOHOKEN                       " & vbNewLine _
        & ",ZEI_GAKU_YUSOHOKEN                        " & vbNewLine _
        & ",KINGAKU_YUSOHOKEN                         " & vbNewLine _
        & ",ZEI_KB_KAIJOHOKEN                         " & vbNewLine _
        & ",ZEI_RITSU_KAIJOHOKEN                      " & vbNewLine _
        & ",ZEI_GAKU_KAIJOHOKEN                       " & vbNewLine _
        & ",KINGAKU_KAIJOHOKEN                        " & vbNewLine _
        & ",ZEI_KB_YUSHUTSU_KAZEI                     " & vbNewLine _
        & ",ZEI_RITSU_YUSHUTSU_KAZEI                  " & vbNewLine _
        & ",ZEI_GAKU_YUSHUTSU_KAZEI                   " & vbNewLine _
        & ",KINGAKU_YUSHUTSU_KAZEI                    " & vbNewLine _
        & ",ZEI_KB_YUSHUTSU_HIKAZEI                   " & vbNewLine _
        & ",ZEI_RITSU_YUSHUTSU_HIKAZEI                " & vbNewLine _
        & ",ZEI_GAKU_YUSHUTSU_HIKAZEI                 " & vbNewLine _
        & ",KINGAKU_YUSHUTSU_HIKAZEI                  " & vbNewLine _
        & ",BUTSURYU_MOTOUKE_NO                       " & vbNewLine _
        & ",UNSO_GYOSHA_CD                            " & vbNewLine _
        & ",SEIKYU_SAKI_BUSHO                         " & vbNewLine _
        & ",YOBI                                      " & vbNewLine _
        & ",JISSEKI_SHORI_FLG                         " & vbNewLine _
        & ",SYS_ENT_DATE                              " & vbNewLine _
        & ",SYS_ENT_TIME                              " & vbNewLine _
        & ",SYS_ENT_PGID                              " & vbNewLine _
        & ",SYS_ENT_USER                              " & vbNewLine _
        & ",SYS_UPD_DATE                              " & vbNewLine _
        & ",SYS_UPD_TIME                              " & vbNewLine _
        & ",SYS_UPD_PGID                              " & vbNewLine _
        & ",SYS_UPD_USER                              " & vbNewLine _
        & ",SYS_DEL_FLG                               " & vbNewLine _
        & ") VALUES (                                 " & vbNewLine _
        & " @OUTKA_CTL_NO                             " & vbNewLine _
        & ",@DATA_SHURUI                              " & vbNewLine _
        & ",@SAKUSEI_DATE                             " & vbNewLine _
        & ",@SAKUSEI_TIME                             " & vbNewLine _
        & ",@DENSO_SAKI                               " & vbNewLine _
        & ",@DENSO_MOTO                               " & vbNewLine _
        & ",@SHORI_KB                                 " & vbNewLine _
        & ",@TORIHIKI_KB                              " & vbNewLine _
        & ",@MOTOUKE_CD                               " & vbNewLine _
        & ",@CUST_CD                                  " & vbNewLine _
        & ",@CUST_BUSHO                               " & vbNewLine _
        & ",@EIGYO_SASHIZU                            " & vbNewLine _
        & ",@KOJO_KANRI_NO                            " & vbNewLine _
        & ",@SHUKKA_DATE                              " & vbNewLine _
        & ",@NONYU_DATE                               " & vbNewLine _
        & ",@ZEI_KB_UNCHIN                            " & vbNewLine _
        & ",@ZEI_RITSU_UNCHIN                         " & vbNewLine _
        & ",@ZEI_GAKU_UNCHIN                          " & vbNewLine _
        & ",@KINGAKU_UNCHIN                           " & vbNewLine _
        & ",@YURYODORO_GAKU                           " & vbNewLine _
        & ",@ZEI_KB_YUSOHOKEN                         " & vbNewLine _
        & ",@ZEI_RITSU_YUSOHOKEN                      " & vbNewLine _
        & ",@ZEI_GAKU_YUSOHOKEN                       " & vbNewLine _
        & ",@KINGAKU_YUSOHOKEN                        " & vbNewLine _
        & ",@ZEI_KB_KAIJOHOKEN                        " & vbNewLine _
        & ",@ZEI_RITSU_KAIJOHOKEN                     " & vbNewLine _
        & ",@ZEI_GAKU_KAIJOHOKEN                      " & vbNewLine _
        & ",@KINGAKU_KAIJOHOKEN                       " & vbNewLine _
        & ",@ZEI_KB_YUSHUTSU_KAZEI                    " & vbNewLine _
        & ",@ZEI_RITSU_YUSHUTSU_KAZEI                 " & vbNewLine _
        & ",@ZEI_GAKU_YUSHUTSU_KAZEI                  " & vbNewLine _
        & ",@KINGAKU_YUSHUTSU_KAZEI                   " & vbNewLine _
        & ",@ZEI_KB_YUSHUTSU_HIKAZEI                  " & vbNewLine _
        & ",@ZEI_RITSU_YUSHUTSU_HIKAZEI               " & vbNewLine _
        & ",@ZEI_GAKU_YUSHUTSU_HIKAZEI                " & vbNewLine _
        & ",@KINGAKU_YUSHUTSU_HIKAZEI                 " & vbNewLine _
        & ",@BUTSURYU_MOTOUKE_NO                      " & vbNewLine _
        & ",@UNSO_GYOSHA_CD                           " & vbNewLine _
        & ",@SEIKYU_SAKI_BUSHO                        " & vbNewLine _
        & ",@YOBI                                     " & vbNewLine _
        & ",@JISSEKI_SHORI_FLG                        " & vbNewLine _
        & ",@SYS_ENT_DATE                             " & vbNewLine _
        & ",@SYS_ENT_TIME                             " & vbNewLine _
        & ",@SYS_ENT_PGID                             " & vbNewLine _
        & ",@SYS_ENT_USER                             " & vbNewLine _
        & ",@SYS_UPD_DATE                             " & vbNewLine _
        & ",@SYS_UPD_TIME                             " & vbNewLine _
        & ",@SYS_UPD_PGID                             " & vbNewLine _
        & ",@SYS_UPD_USER                             " & vbNewLine _
        & ",@SYS_DEL_FLG                              " & vbNewLine _
        & ")                                          " & vbNewLine

#End Region '実績作成処理SQL

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

#End Region 'Field

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI950DAC.SQL_SELECT_SEARCH_COUNT_S)
        Me._StrSql.Append(LMI950DAC.SQL_SELECT_SEARCH_DATA)
        Me._StrSql.Append(LMI950DAC.SQL_SELECT_SEARCH_COUNT_E)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSearchParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI950DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>対象データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI950DAC.SQL_SELECT_SEARCH_DATA)
        Me._StrSql.Append(LMI950DAC.SQL_ORDER_SEARCH)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSearchParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI950DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SHORI_KB", "SHORI_KB")
        map.Add("HORYU", "HORYU")
        map.Add("SEND", "SEND")
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("HOUKOKU_UNCHIN", "HOUKOKU_UNCHIN")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("KOJO_KANRI_NO", "KOJO_KANRI_NO")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_L_UPD_DATE", "UNSO_L_UPD_DATE")
        map.Add("UNSO_L_UPD_TIME", "UNSO_L_UPD_TIME")
        map.Add("UNCHIN_UPD_DATE", "UNCHIN_UPD_DATE")
        map.Add("UNCHIN_UPD_TIME", "UNCHIN_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI950DAC.TABLE_NM_OUT)

        Return ds

    End Function

#End Region '検索処理

#Region "実績作成処理"

    ''' <summary>
    ''' 同一工場管理番号データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>件数取得SQLの構築・発行</remarks>
    Private Function SelectCntKojoKanriNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI950DAC.TABLE_NM_PROC_CTRL_DATA).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_IN)
        Dim unchinTargetTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'INTableの条件rowの格納
        Me._Row = unchinTargetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI950DAC.SQL_SELECT_KOJO_KANRI_NO_COUNT)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetKojoKanriNoParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI950DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 対象運賃データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>対象データ取得SQLの構築・発行</remarks>
    Private Function SelectSendUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI950DAC.TABLE_NM_PROC_CTRL_DATA).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_IN)
        Dim unchinTargetTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'INTableの条件rowの格納
        Me._Row = unchinTargetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI950DAC.SQL_SELECT_SENDUNCHIN_DATA)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSearchParameter(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetSelectSendUnchinParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI950DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("OUTKA_NO_L", "OUTKA_NO_L")
        map.Add("DENSO_MOTO", "DENSO_MOTO")
        map.Add("DENSO_SAKI", "DENSO_SAKI")
        map.Add("SHORI_KB", "SHORI_KB")
        map.Add("TORIHIKI_KB", "TORIHIKI_KB")
        map.Add("MOTOUKE_CD", "MOTOUKE_CD")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_BUSHO", "CUST_BUSHO")
        map.Add("EIGYO_SASHIZU", "EIGYO_SASHIZU")
        map.Add("KOJO_KANRI_NO", "KOJO_KANRI_NO")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("YUSO_CD", "YUSO_CD")
        map.Add("SEIKYU_SAKI", "SEIKYU_SAKI")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_RATE", "TAX_RATE")
        map.Add("ZEI_GAKU_UNCHIN", "ZEI_GAKU_UNCHIN")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("UNSO_L_UPD_DATE", "UNSO_L_UPD_DATE")
        map.Add("UNSO_L_UPD_TIME", "UNSO_L_UPD_TIME")
        map.Add("UNCHIN_UPD_DATE", "UNCHIN_UPD_DATE")
        map.Add("UNCHIN_UPD_TIME", "UNCHIN_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI950DAC.TABLE_NM_SENDUNCHIN_DATA)

        Return ds

    End Function

    ''' <summary>
    ''' 日産物流運賃明細送信データ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendUnchin(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim procCtrlTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_PROC_CTRL_DATA)
        Dim unchinDataTbl As DataTable = ds.Tables(LMI950DAC.TABLE_NM_SENDUNCHIN_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI950DAC.SQL_INSET_SENDUNCHIN _
                                                                       , ds.Tables(LMI950DAC.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = unchinDataTbl.Rows(0)

        'パラメータ設定
        Call Me.SetInsertSendUnchinParameter(Me._Row, procCtrlTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetDataInsertParameter(procCtrlTbl.Rows(0), Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI950DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds
    End Function

#End Region '実績作成処理

#Region "パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = conditionRow.Item("SYSTEM_DATE").ToString()
        Dim systemTime As String = conditionRow.Item("SYSTEM_TIME").ToString()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", systemUserID, DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetDataUpdateParameter(conditionRow, prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataUpdateParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = conditionRow.Item("SYSTEM_DATE").ToString()
        Dim systemTime As String = conditionRow.Item("SYSTEM_TIME").ToString()
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 検索処理のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_DATE").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    '''  同一工場管理番号データ件数検索のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetKojoKanriNoParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@KOJO_KANRI_NO", .Item("KOJO_KANRI_NO").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 対象運賃データ検索のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectSendUnchinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOJO_KANRI_NO", .Item("KOJO_KANRI_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 日産物流運賃明細送信データ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="conditionRow2"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendUnchinParameter(ByVal conditionRow As DataRow, ByVal conditionRow2 As DataRow, ByVal prmList As ArrayList)

        Const zero5 As String = "00000"
        Const zero9 As String = "000000000"

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_NO_L").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_SHURUI", "15", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAKUSEI_DATE", conditionRow2.Item("SYSTEM_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAKUSEI_TIME", conditionRow2.Item("SYSTEM_TIME").ToString().Substring(0, 4), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENSO_SAKI", .Item("DENSO_SAKI").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENSO_MOTO", .Item("DENSO_MOTO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHORI_KB", "1", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TORIHIKI_KB", .Item("TORIHIKI_KB").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@MOTOUKE_CD", .Item("MOTOUKE_CD").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD", .Item("CUST_CD").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_BUSHO", .Item("CUST_BUSHO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EIGYO_SASHIZU", .Item("EIGYO_SASHIZU").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KOJO_KANRI_NO", .Item("KOJO_KANRI_NO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_DATE", .Item("SHUKKA_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NONYU_DATE", .Item("NONYU_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_KB_UNCHIN", .Item("TAX_KB").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_RITSU_UNCHIN", CLng(.Item("TAX_RATE")).ToString("000.00").Replace(".", ""), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_GAKU_UNCHIN", CLng(.Item("ZEI_GAKU_UNCHIN")).ToString().PadLeft(9, "0"c), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KINGAKU_UNCHIN", CLng(.Item("DECI_UNCHIN")).ToString().PadLeft(9, "0"c), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YURYODORO_GAKU", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_KB_YUSOHOKEN", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_RITSU_YUSOHOKEN", zero5, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_GAKU_YUSOHOKEN", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KINGAKU_YUSOHOKEN", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_KB_KAIJOHOKEN", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_RITSU_KAIJOHOKEN", zero5, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_GAKU_KAIJOHOKEN", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KINGAKU_KAIJOHOKEN", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_KB_YUSHUTSU_KAZEI", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_RITSU_YUSHUTSU_KAZEI", zero5, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_GAKU_YUSHUTSU_KAZEI", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KINGAKU_YUSHUTSU_KAZEI", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_KB_YUSHUTSU_HIKAZEI", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_RITSU_YUSHUTSU_HIKAZEI", zero5, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZEI_GAKU_YUSHUTSU_HIKAZEI", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@KINGAKU_YUSHUTSU_HIKAZEI", zero9, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUTSURYU_MOTOUKE_NO", .Item("KOJO_KANRI_NO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_GYOSHA_CD", .Item("YUSO_CD").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIKYU_SAKI_BUSHO", .Item("SEIKYU_SAKI").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YOBI", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

#End Region 'パラメータ設定

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
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd))

    End Function

    ''' <summary>
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cnt">件数</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        'SQLの発行
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

#End Region 'ユーティリティ

#End Region 'Method

End Class
