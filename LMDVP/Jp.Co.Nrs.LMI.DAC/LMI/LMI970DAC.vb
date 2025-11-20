' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI970    : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI970DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI970DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI970IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMI970OUT"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SENDUNCHIN_TARGET As String = "LMI970SENDUNCHIN_TARGET"

    ''' <summary>
    ''' 処理制御テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_PROC_CTRL As String = "LMI970PROC_CTRL"

    ''' <summary>
    ''' 運賃明細送信データ元データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SENDUNCHIN_DATA As String = "LMI970SENDUNCHIN_DATA"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMI970DAC"


    ''' <summary>
    ''' 検索日付区分(cmbSearchDateKb選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbSearchDateKbItems
        Public Const Blank As String = ""
        Public Const ShukkaDate As String = "出荷日"
        Public Const NonyuDate As String = "納入日"
    End Class

#End Region '制御用

#Region "検索処理SQL"

    Private Const SQL_SELECT_SEARCH_COUNT_S As String = _
          "SELECT COUNT(*) AS REC_CNT                    " & vbNewLine _
        & "  FROM (                                      " & vbNewLine

    Private Const SQL_SELECT_SEARCH_COUNT_E As String = _
          " ) SUB                                        " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA As String = _
          "SELECT                                                                            " & vbNewLine _
        & "       CASE YUSOIRAI.SHORI_KB                                                     " & vbNewLine _
        & "            WHEN '1' THEN '新規'                                                  " & vbNewLine _
        & "            WHEN '2' THEN '変更'                                                  " & vbNewLine _
        & "            WHEN '3' THEN '削除'                                                  " & vbNewLine _
        & "       END AS SHORI_KB                                                            " & vbNewLine _
        & "       --▼同一工場管理番号のデータに古い順に連番を振る                           " & vbNewLine _
        & "       --▼複数行ある場合2行目以降は送信後に受信したデータ                        " & vbNewLine _
        & "      ,CASE ROW_NUMBER() OVER(PARTITION BY YUSOIRAI.KOJO_KANRI_NO                 " & vbNewLine _
        & "                ORDER BY YUSOIRAI.CRT_DATE, YUSOIRAI.FILE_NAME, YUSOIRAI.REC_NO)  " & vbNewLine _
        & "            WHEN 1 THEN '正常'                                                    " & vbNewLine _
        & "            ELSE '保留'                                                           " & vbNewLine _
        & "       END AS HORYU_KB                                                            " & vbNewLine _
        & "-- ADD START 2019/05/30 要望管理006030                                            " & vbNewLine _
        & "      ,CASE YUSOIRAI.IRAISHO_PRINT_FLG                                            " & vbNewLine _
        & "            WHEN '00' THEN '未'                                                   " & vbNewLine _
        & "            WHEN '01' THEN '済'                                                   " & vbNewLine _
        & "       END AS PRINT_KB                                                            " & vbNewLine _
        & "-- ADD END   2019/05/30 要望管理006030                                            " & vbNewLine _
        & "      ,CASE WHEN SENDUNCHIN.KOJO_KANRI_NO IS NULL                                 " & vbNewLine _
        & "            THEN '未'                                                             " & vbNewLine _
        & "            ELSE '済'                                                             " & vbNewLine _
        & "       END AS SEND_KB                                                             " & vbNewLine _
        & "      ,YUSOIRAI.KOJO_KANRI_NO                                                     " & vbNewLine _
        & "      ,YUSOIRAI.SHUKKA_DATE                                                       " & vbNewLine _
        & "      ,YUSOIRAI.NONYU_DATE                                                        " & vbNewLine _
        & "      ,YUSOIRAI.DEST_KAISHA_NM                                                    " & vbNewLine _
        & "      ,YUSOIRAI.JUSHO_1 + YUSOIRAI.JUSHO_2 AS DEST_KAISHA_JUSHO                   " & vbNewLine _
        & "-- MOD START 2019/05/30 要望管理006030                                            " & vbNewLine _
        & "--      ,YUSOIRAI.HINMEI                                                            " & vbNewLine _
        & "      ,YUSOIRAI.NOUHINSHO_HINMEI AS HINMEI                                        " & vbNewLine _
        & "-- MOD END   2019/05/30 要望管理006030                                            " & vbNewLine _
        & "      ,YUSOIRAI.SURYO AS JURYO       -- ADD 2019/05/30 要望管理006030             " & vbNewLine _
        & "      ,YUSOIRAI.TANKA                -- ADD 2019/05/30 要望管理006030             " & vbNewLine _
        & "      ,YUSOIRAI.UNCHIN                                                            " & vbNewLine _
        & "      ,YUSOIRAI.CRT_DATE                                                          " & vbNewLine _
        & "      ,YUSOIRAI.FILE_NAME                                                         " & vbNewLine _
        & "      ,YUSOIRAI.REC_NO                                                            " & vbNewLine _
        & "      ,YUSOIRAI.SYS_UPD_DATE                                                      " & vbNewLine _
        & "      ,YUSOIRAI.SYS_UPD_TIME                                                      " & vbNewLine _
        & "  FROM $LM_TRN$..H_YUSOIRAIEDI_NSN  YUSOIRAI                                      " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDUNCHINEDI_NSN SENDUNCHIN                              " & vbNewLine _
        & "    ON SENDUNCHIN.KOJO_KANRI_NO = YUSOIRAI.KOJO_KANRI_NO                          " & vbNewLine _
        & "   AND SENDUNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & " WHERE YUSOIRAI.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
        & "   AND YUSOIRAI.KOSU = 0                                                          " & vbNewLine _
        & "   AND YUSOIRAI.UNSO_GYOSHA_CD = '712850C'    -- ADD 2019/05/30 要望管理006030    " & vbNewLine

    Private Const SQL_WHERE_SEARCH_DATA_TORIKOMI_DATE_FROM As String = _
          "   AND YUSOIRAI.CRT_DATE >= @TORIKOMI_DATE_FROM                                   " & vbNewLine

    Private Const SQL_WHERE_SEARCH_DATA_TORIKOMI_DATE_TO As String = _
          "   AND YUSOIRAI.CRT_DATE <= @TORIKOMI_DATE_TO                                     " & vbNewLine

    Private Const SQL_WHERE_SEARCH_DATA_SHUKKA_DATE_FROM As String = _
          "   AND YUSOIRAI.SHUKKA_DATE >= @SEARCH_DATE_FROM                                  " & vbNewLine

    Private Const SQL_WHERE_SEARCH_DATA_SHUKKA_DATE_TO As String = _
          "   AND YUSOIRAI.SHUKKA_DATE <= @SEARCH_DATE_TO                                    " & vbNewLine

    Private Const SQL_WHERE_SEARCH_DATA_NONYU_DATE_FROM As String = _
          "   AND YUSOIRAI.NONYU_DATE >= @SEARCH_DATE_FROM                                   " & vbNewLine

    Private Const SQL_WHERE_SEARCH_DATA_NONYU_DATE_TO As String = _
          "   AND YUSOIRAI.NONYU_DATE <= @SEARCH_DATE_TO                                     " & vbNewLine

    Private Const SQL_ORDER_SEARCH As String = _
          " ORDER BY                                                                         " & vbNewLine _
        & "       YUSOIRAI.SHUKKA_DATE          -- ADD 2019/05/30 要望管理006030             " & vbNewLine _
        & "      ,CASE WHEN SENDUNCHIN.KOJO_KANRI_NO IS NULL                                 " & vbNewLine _
        & "            THEN 0                                                                " & vbNewLine _
        & "            ELSE 1                                                                " & vbNewLine _
        & "       END                                                                        " & vbNewLine _
        & "      ,YUSOIRAI.KOJO_KANRI_NO                                                     " & vbNewLine _
        & "      ,YUSOIRAI.CRT_DATE                                                          " & vbNewLine _
        & "      ,YUSOIRAI.FILE_NAME                                                         " & vbNewLine _
        & "      ,YUSOIRAI.REC_NO                                                            " & vbNewLine

#End Region '検索処理SQL

#Region "保存処理SQL"

    Private Const SQL_UPDATE_YUSOIRAI As String = _
          "UPDATE $LM_TRN$..H_YUSOIRAIEDI_NSN          " & vbNewLine _
        & "   SET UNCHIN       = @UNCHIN               " & vbNewLine _
        & "      ,SURYO        = @SURYO                -- ADD 2019/05/30 要望管理006030 " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE         " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME         " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID         " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER         " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                 " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME               " & vbNewLine _
        & "   AND REC_NO = @REC_NO                     " & vbNewLine _
        & "   AND SYS_UPD_DATE = @SYS_UPD_DATE_BEFORE  " & vbNewLine _
        & "   AND SYS_UPD_TIME = @SYS_UPD_TIME_BEFORE  " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                    " & vbNewLine

#End Region

    'ADD START 2019/05/30 要望管理006030
#Region "単価更新処理"

    Private Const SQL_UPDATE_YUSOIRAI_TANKA As String = _
          "UPDATE $LM_TRN$..H_YUSOIRAIEDI_NSN                                                           " & vbNewLine _
        & "   SET TANKA = @TANKA                                                                        " & vbNewLine _
        & "      ,UNCHIN = ROUND(SURYO * @TANKA, 0)                                                     " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE                                                          " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                                                          " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                                                          " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                                                          " & vbNewLine _
        & " WHERE SYS_DEL_FLG = '0'                                                                     " & vbNewLine _
        & "   AND KOSU = 0                                                                              " & vbNewLine _
        & "   AND UNSO_GYOSHA_CD = '712850C'                                                            " & vbNewLine _
        & "   AND SHUKKA_DATE BETWEEN @SHUKKA_DATE_FROM AND @SHUKKA_DATE_TO                             " & vbNewLine _
        & "   AND NOT EXISTS(SELECT 'X'                                                                 " & vbNewLine _
        & "                    FROM $LM_TRN$..H_SENDUNCHINEDI_NSN                                       " & vbNewLine _
        & "                   WHERE H_SENDUNCHINEDI_NSN.KOJO_KANRI_NO = H_YUSOIRAIEDI_NSN.KOJO_KANRI_NO " & vbNewLine _
        & "                     AND H_SENDUNCHINEDI_NSN.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "                 )                                                                           " & vbNewLine

#End Region
    'ADD END   2019/05/30 要望管理006030

#Region "実績作成処理SQL"

    Private Const SQL_COUNT_KOJOKANRINO_OF_OUTKAJOHO As String = _
          "SELECT COUNT(*) AS REC_CNT                      " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAJOHOEDI_NSN  OUTKAJOHO  " & vbNewLine _
        & " WHERE OUTKAJOHO.KOJO_KANRI_NO = @KOJO_KANRI_NO " & vbNewLine _
        & "   AND OUTKAJOHO.SYS_DEL_FLG = '0'              " & vbNewLine

    Private Const SQL_COUNT_KOJOKANRINO_OF_YUSOIRAI As String = _
          "SELECT COUNT(*) AS REC_CNT                      " & vbNewLine _
        & "  FROM $LM_TRN$..H_YUSOIRAIEDI_NSN  YUSOIRAI    " & vbNewLine _
        & " WHERE YUSOIRAI.KOJO_KANRI_NO = @KOJO_KANRI_NO  " & vbNewLine _
        & "   AND YUSOIRAI.SYS_DEL_FLG = '0'               " & vbNewLine

    Private Const SQL_SELECT_SENDUNCHIN_DATA As String = _
          "SELECT                                                                            " & vbNewLine _
        & "     OUTKAJOHOEDI.DENSO_MOTO AS DENSO_SAKI                                        " & vbNewLine _
        & "    ,OUTKAJOHOEDI.DENSO_SAKI AS DENSO_MOTO                                        " & vbNewLine _
        & "    ,OUTKAJOHOEDI.TORIHIKI_KB                                                     " & vbNewLine _
        & "    ,OUTKAJOHOEDI.MOTOUKE_CD                                                      " & vbNewLine _
        & "    ,OUTKAJOHOEDI.CUST_CD                                                         " & vbNewLine _
        & "    ,OUTKAJOHOEDI.CUST_BUSHO                                                      " & vbNewLine _
        & "    ,OUTKAJOHOEDI.EIGYO_SASHIZU                                                   " & vbNewLine _
        & "    ,OUTKAJOHOEDI.KOJO_KANRI_NO                                                   " & vbNewLine _
        & "    ,OUTKAJOHOEDI.SHUKKA_DATE                                                     " & vbNewLine _
        & "    ,OUTKAJOHOEDI.NONYU_DATE                                                      " & vbNewLine _
        & "    ,CASE M_CUST.TAX_KB                                                           " & vbNewLine _
        & "       WHEN '01' THEN '1'                                                         " & vbNewLine _
        & "       WHEN '02' THEN '3'                                                         " & vbNewLine _
        & "       WHEN '03' THEN '2'                                                         " & vbNewLine _
        & "       ELSE ''                                                                    " & vbNewLine _
        & "     END AS TAX_KB                                                                " & vbNewLine _
        & "    ,CASE WHEN M_CUST.TAX_KB = '01'                                               " & vbNewLine _
        & "       THEN M_TAX.TAX_RATE * 100                                                  " & vbNewLine _
        & "       ELSE 0                                                                     " & vbNewLine _
        & "     END AS TAX_RATE                                                              " & vbNewLine _
        & "    ,CASE WHEN M_CUST.TAX_KB = '01'                                               " & vbNewLine _
        & "       THEN ROUND(YUSOIRAI.UNCHIN * M_TAX.TAX_RATE, 0)                            " & vbNewLine _
        & "       ELSE 0                                                                     " & vbNewLine _
        & "     END AS ZEI_GAKU_UNCHIN                                                       " & vbNewLine _
        & "    ,YUSOIRAI.UNCHIN                                                              " & vbNewLine _
        & "    ,OUTKAJOHOEDI.YUSO_CD                                                         " & vbNewLine _
        & "    ,OUTKAJOHOEDI.SEIKYU_SAKI                                                     " & vbNewLine _
        & "  FROM $LM_TRN$..H_YUSOIRAIEDI_NSN  YUSOIRAI                                      " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAJOHOEDI_NSN  OUTKAJOHOEDI                            " & vbNewLine _
        & "    ON OUTKAJOHOEDI.KOJO_KANRI_NO = YUSOIRAI.KOJO_KANRI_NO                        " & vbNewLine _
        & "   AND OUTKAJOHOEDI.SYS_DEL_FLG = '0'                                             " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDUNCHINEDI_NSN SENDUNCHIN                              " & vbNewLine _
        & "    ON SENDUNCHIN.KOJO_KANRI_NO = YUSOIRAI.KOJO_KANRI_NO                          " & vbNewLine _
        & "   AND SENDUNCHIN.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_CUST                                                      " & vbNewLine _
        & "    ON M_CUST.SYS_DEL_FLG = '0'                                                   " & vbNewLine _
        & "   AND M_CUST.NRS_BR_CD = @NRS_BR_CD                                              " & vbNewLine _
        & "   AND M_CUST.CUST_CD_L = @CUST_CD_L                                              " & vbNewLine _
        & "   AND M_CUST.CUST_CD_M = '00'                                                    " & vbNewLine _
        & "   AND M_CUST.CUST_CD_S = '00'                                                    " & vbNewLine _
        & "   AND M_CUST.CUST_CD_SS = '00'                                                   " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN  KBN_Z001                                             " & vbNewLine _
        & "    ON KBN_Z001.SYS_DEL_FLG  = '0'                                                " & vbNewLine _
        & "   AND KBN_Z001.KBN_GROUP_CD = 'Z001'                                             " & vbNewLine _
        & "   AND KBN_Z001.KBN_CD = M_CUST.TAX_KB                                            " & vbNewLine _
        & " LEFT JOIN $LM_MST$..M_TAX                                                        " & vbNewLine _
        & "   ON M_TAX.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
        & "  AND M_TAX.TAX_CD = KBN_Z001.KBN_NM3                                             " & vbNewLine _
        & "  AND M_TAX.START_DATE =                                                          " & vbNewLine _
        & "      (SELECT MAX(START_DATE)                                                     " & vbNewLine _
        & "         FROM $LM_MST$..M_TAX  TAX_S_DATE                                         " & vbNewLine _
        & "        WHERE TAX_S_DATE.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "          AND TAX_S_DATE.TAX_CD = KBN_Z001.KBN_NM3                                " & vbNewLine _
        & "          AND TAX_S_DATE.START_DATE <= CASE M_CUST.UNTIN_CALCULATION_KB           " & vbNewLine _
        & "                                         WHEN '01' THEN OUTKAJOHOEDI.SHUKKA_DATE  " & vbNewLine _
        & "                                         WHEN '02' THEN OUTKAJOHOEDI.NONYU_DATE   " & vbNewLine _
        & "                                       END                                        " & vbNewLine _
        & "      )                                                                           " & vbNewLine _
        & " WHERE YUSOIRAI.SYS_DEL_FLG = '0'                                                 " & vbNewLine _
        & "   AND YUSOIRAI.CRT_DATE = @CRT_DATE                                              " & vbNewLine _
        & "   AND YUSOIRAI.FILE_NAME = @FILE_NAME                                            " & vbNewLine _
        & "   AND YUSOIRAI.REC_NO = @REC_NO                                                  " & vbNewLine _
        & "   AND YUSOIRAI.SYS_UPD_DATE = @SYS_UPD_DATE                                      " & vbNewLine _
        & "   AND YUSOIRAI.SYS_UPD_TIME = @SYS_UPD_TIME                                      " & vbNewLine _
        & "   AND YUSOIRAI.KOSU = 0                                                          " & vbNewLine _
        & "   AND YUSOIRAI.UNSO_GYOSHA_CD = '712850C'    -- ADD 2019/05/30 要望管理006030    " & vbNewLine _
        & "   AND SENDUNCHIN.KOJO_KANRI_NO IS NULL                                           " & vbNewLine

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

    'ADD START 2019/05/30 要望管理006030
#Region "印刷フラグ更新処理"

    Private Const SQL_UPDATE_IRAISHO_PRINT_FLG As String = _
          "UPDATE $LM_TRN$..H_YUSOIRAIEDI_NSN  " & vbNewLine _
        & "   SET IRAISHO_PRINT_FLG = '01'     " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER " & vbNewLine _
        & " WHERE SYS_DEL_FLG = '0'            " & vbNewLine _
        & "   AND CRT_DATE = @CRT_DATE         " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME       " & vbNewLine _
        & "   AND REC_NO = @REC_NO             " & vbNewLine

#End Region
    'ADD END   2019/05/30 要望管理006030

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
        Dim inTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI970DAC.SQL_SELECT_SEARCH_COUNT_S)

        Me._StrSql.Append(LMI970DAC.SQL_SELECT_SEARCH_DATA)

        If Not String.IsNullOrEmpty(_Row.Item("TORIKOMI_DATE_FROM").ToString()) Then
            'EDI取込日From
            Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_TORIKOMI_DATE_FROM)
        End If
        If Not String.IsNullOrEmpty(_Row.Item("TORIKOMI_DATE_TO").ToString()) Then
            'EDI取込日To
            Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_TORIKOMI_DATE_TO)
        End If

        If _Row.Item("SEARCH_DATE_KB").ToString() <> CmbSearchDateKbItems.Blank Then
            If Not String.IsNullOrEmpty(_Row.Item("SEARCH_DATE_FROM").ToString()) Then
                Select Case _Row.Item("SEARCH_DATE_KB").ToString()
                    Case CmbSearchDateKbItems.ShukkaDate
                        '出荷日From
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_SHUKKA_DATE_FROM)
                    Case CmbSearchDateKbItems.NonyuDate
                        '納入日From
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_NONYU_DATE_FROM)
                End Select
            End If
            If Not String.IsNullOrEmpty(_Row.Item("SEARCH_DATE_TO").ToString()) Then
                Select Case _Row.Item("SEARCH_DATE_KB").ToString()
                    Case CmbSearchDateKbItems.ShukkaDate
                        '出荷日To
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_SHUKKA_DATE_TO)
                    Case CmbSearchDateKbItems.NonyuDate
                        '納入日To
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_NONYU_DATE_TO)
                End Select
            End If
        End If

        Me._StrSql.Append(LMI970DAC.SQL_SELECT_SEARCH_COUNT_E)

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

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI970DAC.SQL_SELECT_SEARCH_DATA)

        If Not String.IsNullOrEmpty(_Row.Item("TORIKOMI_DATE_FROM").ToString()) Then
            'EDI取込日From
            Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_TORIKOMI_DATE_FROM)
        End If
        If Not String.IsNullOrEmpty(_Row.Item("TORIKOMI_DATE_TO").ToString()) Then
            'EDI取込日To
            Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_TORIKOMI_DATE_TO)
        End If

        If _Row.Item("SEARCH_DATE_KB").ToString() <> CmbSearchDateKbItems.Blank Then
            If Not String.IsNullOrEmpty(_Row.Item("SEARCH_DATE_FROM").ToString()) Then
                Select Case _Row.Item("SEARCH_DATE_KB").ToString()
                    Case CmbSearchDateKbItems.ShukkaDate
                        '出荷日From
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_SHUKKA_DATE_FROM)
                    Case CmbSearchDateKbItems.NonyuDate
                        '納入日From
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_NONYU_DATE_FROM)
                End Select
            End If
            If Not String.IsNullOrEmpty(_Row.Item("SEARCH_DATE_TO").ToString()) Then
                Select Case _Row.Item("SEARCH_DATE_KB").ToString()
                    Case CmbSearchDateKbItems.ShukkaDate
                        '出荷日To
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_SHUKKA_DATE_TO)
                    Case CmbSearchDateKbItems.NonyuDate
                        '納入日To
                        Me._StrSql.Append(LMI970DAC.SQL_WHERE_SEARCH_DATA_NONYU_DATE_TO)
                End Select
            End If
        End If

        Me._StrSql.Append(LMI970DAC.SQL_ORDER_SEARCH)

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

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SHORI_KB", "SHORI_KB")
        map.Add("HORYU_KB", "HORYU_KB")
        map.Add("PRINT_KB", "PRINT_KB")     'ADD 2019/05/30 要望管理006030
        map.Add("SEND_KB", "SEND_KB")
        map.Add("KOJO_KANRI_NO", "KOJO_KANRI_NO")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("DEST_KAISHA_NM", "DEST_KAISHA_NM")
        map.Add("DEST_KAISHA_JUSHO", "DEST_KAISHA_JUSHO")
        map.Add("HINMEI", "HINMEI")
        map.Add("JURYO", "JURYO")           'ADD 2019/05/30 要望管理006030
        map.Add("TANKA", "TANKA")           'ADD 2019/05/30 要望管理006030
        map.Add("UNCHIN", "UNCHIN")
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("REC_NO", "REC_NO")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI970DAC.TABLE_NM_OUT)

        Return ds

    End Function

#End Region '検索処理

    'ADD START 2019/05/30 要望管理006030
#Region "単価更新処理"

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ単価一括更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateYusoIraiTanka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_IN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI970DAC.SQL_UPDATE_YUSOIRAI_TANKA _
                                                                       , inTbl.Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = inTbl.Rows(0)

        'パラメータ設定
        Call Me.SetUpdateYusoIraiTankaParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = False Then
            Return ds
        End If

        Return ds

    End Function

#End Region
    'ADD START 2019/05/30 要望管理006030

#Region "保存処理"

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateYusoIrai(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unchinDataTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI970DAC.SQL_UPDATE_YUSOIRAI _
                                                                       , ds.Tables(LMI970DAC.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()))

        For Each unchinRow As DataRow In unchinDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = unchinRow

            'パラメータ設定
            Call Me.SetUpdateYusoIraiParameter(Me._Row, Me._SqlPrmList)
            Call Me.SetDataUpdateParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "実績作成処理"

    ''' <summary>
    ''' 同一工場管理番号データ件数検索（日産物流出荷情報ＥＤＩ受信データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>件数取得SQLの構築・発行</remarks>
    Private Function SelectCntKojoKanriNoOfOutkaJoho(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI970DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_IDX"))
        Dim inTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_IN)
        Dim unchinTargetTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'INTableの条件rowの格納
        Me._Row = unchinTargetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI970DAC.SQL_COUNT_KOJOKANRINO_OF_OUTKAJOHO)

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

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 同一工場管理番号データ件数検索（日産物流輸送依頼ＥＤＩ受信データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>件数取得SQLの構築・発行</remarks>
    Private Function SelectCntKojoKanriNoOfYusoIrai(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI970DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_IDX"))
        Dim inTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_IN)
        Dim unchinTargetTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'INTableの条件rowの格納
        Me._Row = unchinTargetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI970DAC.SQL_COUNT_KOJOKANRINO_OF_YUSOIRAI)

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

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim rowNo As Integer = CInt(ds.Tables(LMI970DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_IDX"))
        Dim inTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_IN)
        Dim unchinTargetTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'INTableの条件rowの格納
        Me._Row = unchinTargetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI970DAC.SQL_SELECT_SENDUNCHIN_DATA)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSearchParameter(inTbl.Rows(0), Me._SqlPrmList)
        Call Me.SetSelectSendUnchinParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DENSO_MOTO", "DENSO_MOTO")
        map.Add("DENSO_SAKI", "DENSO_SAKI")
        map.Add("TORIHIKI_KB", "TORIHIKI_KB")
        map.Add("MOTOUKE_CD", "MOTOUKE_CD")
        map.Add("CUST_CD", "CUST_CD")
        map.Add("CUST_BUSHO", "CUST_BUSHO")
        map.Add("EIGYO_SASHIZU", "EIGYO_SASHIZU")
        map.Add("KOJO_KANRI_NO", "KOJO_KANRI_NO")
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_RATE", "TAX_RATE")
        map.Add("ZEI_GAKU_UNCHIN", "ZEI_GAKU_UNCHIN")
        map.Add("UNCHIN", "UNCHIN")
        map.Add("YUSO_CD", "YUSO_CD")
        map.Add("SEIKYU_SAKI", "SEIKYU_SAKI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI970DAC.TABLE_NM_SENDUNCHIN_DATA)

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
        Dim procCtrlTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_PROC_CTRL)
        Dim unchinDataTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_SENDUNCHIN_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI970DAC.SQL_INSET_SENDUNCHIN _
                                                                       , ds.Tables(LMI970DAC.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '条件rowの格納
        Me._Row = unchinDataTbl.Rows(0)

        'パラメータ設定
        Call Me.SetInsertSendUnchinParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        'パラメータの初期化
        cmd.Parameters.Clear()

        Return ds
    End Function

#End Region '実績作成処理

    'ADD START 2019/05/30 要望管理006030
#Region "印刷フラグ更新処理"

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ運送依頼書印刷フラグ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateIraishoPrintFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim unchinDataTbl As DataTable = ds.Tables(LMI970DAC.TABLE_NM_SENDUNCHIN_TARGET)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI970DAC.SQL_UPDATE_IRAISHO_PRINT_FLG _
                                                                       , ds.Tables(LMI970DAC.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()))

        For Each unchinRow As DataRow In unchinDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            '条件rowの格納
            Me._Row = unchinRow

            'パラメータ設定
            Call Me.SetUpdateIraishoPrintFlgParameter(Me._Row, Me._SqlPrmList)
            Call Me.SetDataUpdateParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMI970DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region
    'ADD END   2019/05/30 要望管理006030

#Region "パラメータ設定"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetDataUpdateParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataUpdateParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 検索処理のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_FROM", .Item("TORIKOMI_DATE_FROM").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TORIKOMI_DATE_TO", .Item("TORIKOMI_DATE_TO").ToString(), DBDataType.CHAR))
            If .Item("SEARCH_DATE_KB").ToString() <> CmbSearchDateKbItems.Blank Then
                If Not String.IsNullOrEmpty(.Item("SEARCH_DATE_FROM").ToString()) Then
                    '検索日From
                    prmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_FROM", .Item("SEARCH_DATE_FROM").ToString(), DBDataType.CHAR))
                End If
                If Not String.IsNullOrEmpty(.Item("SEARCH_DATE_TO").ToString()) Then
                    '検索日To
                    prmList.Add(MyBase.GetSqlParameter("@SEARCH_DATE_TO", .Item("SEARCH_DATE_TO").ToString(), DBDataType.CHAR))
                End If
            End If
        End With

    End Sub

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ単価一括更新のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateYusoIraiTankaParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            Dim sTargetYM As String = .Item("TARGET_YM").ToString()

            prmList.Add(MyBase.GetSqlParameter("@TANKA", .Item("TANKA").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_DATE_FROM", sTargetYM + "01", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_DATE_TO", sTargetYM + "31".ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ印刷フラグ更新のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateIraishoPrintFlgParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
        End With

    End Sub
    'ADD END   2019/05/30 要望管理006030

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ更新のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateYusoIraiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SURYO", .Item("JURYO").ToString(), DBDataType.NUMERIC))    'ADD 2019/05/30 要望管理006030
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN", .Item("UNCHIN").ToString(), DBDataType.NUMERIC))  'MOD 2019/05/30 CHAR→NUMERIC
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 同一工場管理番号データ件数検索のパラメータ設定
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
    ''' <param name="targetRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectSendUnchinParameter(ByVal inRow As DataRow, ByVal targetRow As DataRow, ByVal prmList As ArrayList)

        With inRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
        End With

        With targetRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REC_NO", .Item("REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 日産物流運賃明細送信データ登録のパラメータ設定
    ''' </summary>
    ''' <param name="dataRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendUnchinParameter(ByVal dataRow As DataRow, ByVal prmList As ArrayList)

        Const zero5 As String = "00000"
        Const zero9 As String = "000000000"

        With dataRow
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DATA_SHURUI", "15", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAKUSEI_DATE", MyBase.GetSystemDate(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAKUSEI_TIME", MyBase.GetSystemTime().Substring(0, 4), DBDataType.VARCHAR))
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
            prmList.Add(MyBase.GetSqlParameter("@KINGAKU_UNCHIN", CLng(.Item("UNCHIN")).ToString().PadLeft(9, "0"c), DBDataType.VARCHAR))
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
