' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI960    : 出荷データ確認（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI960DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI960DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI960IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMI960OUT"

    ''' <summary>
    ''' 実績作成対象テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SAKUSEI_TARGET As String = "LMI960SAKUSEI_TARGET"

    ''' <summary>
    ''' 処理制御データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_PROC_CTRL As String = "LMI960PROC_CTRL"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ元データテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_JISSEKI_DATA As String = "LMI960JISSEKI_DATA"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMI960DAC"

    ''' <summary>
    ''' 部門名称(cmbBumon選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbBumonItems
        Public Const Soko As String = "倉庫"
        Public Const ISO As String = "ISO"
        Public Const ChilledLorry As String = "Chilled Lorry"
    End Class

    ''' <summary>
    ''' 場所区分(cmbBashoKb選択肢)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbBashoKbItems
        Public Const Tsumikomi As String = "積込場"
        Public Const Nioroshi As String = "荷下場"
        Public Const NonyuYotei As String = "納入予定"
    End Class

    'ADD START 2019/03/27
    ''' <summary>
    ''' 一括変更項目(cmbIkkatsuChange選択肢)コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbIkkatsuChangeItems
        Public Const ShukkaDate As String = "01"
        Public Const NonyuDate As String = "02"
    End Class
    'ADD END  2019/03/27

    ''' <summary>
    ''' 遅延種別(cmbDelayShubetsu選択肢)コード
    ''' </summary>
    ''' <remarks></remarks>
    Public Class CmbDelayShubetsuItems
        Public Const Shukka As String = "01"
        Public Const Nonyu As String = "02"
    End Class

    ''' <summary>
    ''' 作成区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum JissekiSakuseiKb As Integer
        Arrival = 0
        Departure
    End Enum

    ''' <summary>
    ''' 進捗区分(配送ステータス)
    ''' </summary>
    ''' <remarks>
    ''' 未送信→ピック済→納入予定→荷下ろし済
    ''' </remarks>
    Public Enum ShinchokuKb As Integer
        Misoushin = 1
        PickZumi
        NioroshiZumi
        NonyuYotei
    End Enum

    'ADD S 2019/12/12 009741
    ''' <summary>
    ''' 進捗区分(受注ステータス)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ShinchokuKbJuchu As Integer
        Mishori = 1            '未処理
        JuchuOK                 '受注OK
        JuchuNG                 '受注NG
        NyuShukkaTourokuZumi    '入出荷/受注登録済  'ADD 2020/02/07 010901
        JissekiSakuseiZumi      '実績作成済         'ADD 2020/02/07 010901
        EdiTorikeshi            'EDI取消
    End Enum

    ''' <summary>
    ''' H_OUTKAEDI_HED_HWL.GYO
    ''' </summary>
    Public Enum HOutkaediHedHwlGyo As Integer
        OrderResponse990 = 0
        ArrivalTsumikomi = 1
        DepartureTsumikomi = 2
        ArrivalNioroshi = 3
        DepartureNioroshi = 4
        OrderResponse214 = 5          'ADD 2020/03/06 011377
        ShukkaDelay = 6
        NonyuDelay = 7
        NonyuYotei = 8
        OrderResponse990Cancel = 9
        OrderResponse214Cancel = 10
    End Enum
    'ADD E 2019/12/12 009741

    ''' <summary>
    ''' 入出荷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Class InOutKb
        Public Const Mitei As String = ""
        Public Const Inka As String = "1"
        Public Const Outka As String = "2"
        Public Const Unso As String = "3"
    End Class

#End Region '制御用

#Region "検索処理SQL"

    Private Const SQL_SELECT_SEARCH_COUNT_S As String =
          "SELECT COUNT(*) AS REC_CNT                    " & vbNewLine _
        & "  FROM (                                      " & vbNewLine

    Private Const SQL_SELECT_SEARCH_COUNT_E As String =
          " ) SUB                                        " & vbNewLine

    '▼メインSQL（倉庫/Chilled Lorry）
    Private Const SQL_SELECT_SEARCH_DATA_SOKO As String =
          "SELECT                                                                      " & vbNewLine _
        & "-- ADD S 2019/12/12 009741                                                  " & vbNewLine _
        & "       CASE HED.STATUS_KB                                                   " & vbNewLine _
        & "         WHEN '1' THEN ''                                                   " & vbNewLine _
        & "         WHEN '2' THEN '訂正'                                               " & vbNewLine _
        & "         WHEN '3' THEN '取消'                                               " & vbNewLine _
        & "         ELSE HED.STATUS_KB                                                 " & vbNewLine _
        & "       END STATUS_KB                                                        " & vbNewLine _
        & "-- ADD S 2020/02/07 010901                                                  " & vbNewLine _
        & "      ,CASE HED.DEL_KB                                                      " & vbNewLine _
        & "         WHEN '0' THEN '正常'                                               " & vbNewLine _
        & "         WHEN '3' THEN '保留'                                               " & vbNewLine _
        & "         ELSE HED.DEL_KB                                                    " & vbNewLine _
        & "       END DEL_KB                                                           " & vbNewLine _
        & "-- ADD E 2020/02/07 010901                                                  " & vbNewLine _
        & "      ,CASE HED.SHINCHOKU_KB_JUCHU                                          " & vbNewLine _
        & "         WHEN '1' THEN '未処理'                                             " & vbNewLine _
        & "         WHEN '2' THEN '受注OK'                                             " & vbNewLine _
        & "         WHEN '3' THEN '受注NG'                                             " & vbNewLine _
        & "         WHEN '4' THEN '入出荷輸送登録済'                                   " & vbNewLine _
        & "         WHEN '5' THEN '実績作成済'                                         " & vbNewLine _
        & "         WHEN '6' THEN 'EDI取消'                                            " & vbNewLine _
        & "         ELSE HED.SHINCHOKU_KB_JUCHU                                        " & vbNewLine _
        & "       END SHINCHOKU_KB_JUCHU                                               " & vbNewLine _
        & "-- ADD E 2019/12/12 009741                                                  " & vbNewLine _
        & "      ,CASE HED.SHINCHOKU_KB                                                " & vbNewLine _
        & "         WHEN '1' THEN '未送信'                                             " & vbNewLine _
        & "         WHEN '2' THEN 'ピック済'                                           " & vbNewLine _
        & "         WHEN '3' THEN '荷下ろし済'                                         " & vbNewLine _
        & "         WHEN '4' THEN '納入予定'                                           " & vbNewLine _
        & "         ELSE HED.SHINCHOKU_KB                                              " & vbNewLine _
        & "       END SHINCHOKU_KB                                                     " & vbNewLine _
        & "      ,CASE                                                                 " & vbNewLine _
        & "         WHEN HED.SHINCHOKU_KB = '1' AND SHUKKADELAY.CRT_DATE IS NOT NULL   " & vbNewLine _
        & "         THEN '出荷遅延'                                                    " & vbNewLine _
        & "         WHEN HED.SHINCHOKU_KB = '4' AND NONYUDELAY.CRT_DATE IS NOT NULL    " & vbNewLine _
        & "         THEN '納入遅延'                                                    " & vbNewLine _
        & "         ELSE ''                                                            " & vbNewLine _
        & "       END DELAY_STATUS                                                     " & vbNewLine _
        & "      ,HED.CYLINDER_SERIAL_NO                                               " & vbNewLine _
        & "      ,'' AS GOODS_CD                                                       " & vbNewLine _
        & "      ,'' AS GOODS_NM                                                       " & vbNewLine _
        & "      ,SPM.SHIPMENT_ID                                                      " & vbNewLine _
        & "      ,'' AS SAP_ORD_NO                                                     " & vbNewLine _
        & "      ,SPM.CON AS CUST_ORD_NO                                               " & vbNewLine _
        & "      ,CASE HED.INOUT_KB                                                    " & vbNewLine _
        & "         WHEN ''  THEN '未定'                                               " & vbNewLine _
        & "         WHEN '1' THEN '入荷'                                               " & vbNewLine _
        & "         WHEN '2' THEN '出荷'                                               " & vbNewLine _
        & "         WHEN '3' THEN '輸送'                                               " & vbNewLine _
        & "         ELSE HED.INOUT_KB                                                  " & vbNewLine _
        & "       END INOUT_KB                                                         " & vbNewLine _
        & "-- ADD S 2020/02/07 010901                                                  " & vbNewLine _
        & "      ,CASE                                                                 " & vbNewLine _
        & "         WHEN HED.OUTKA_CTL_NO <> ''                                        " & vbNewLine _
        & "              AND CASE                                                      " & vbNewLine _
        & "                     WHEN HED.INOUT_KB = '1' THEN INKA_L.INKA_NO_L          " & vbNewLine _
        & "                     WHEN HED.INOUT_KB = '2' THEN OUTKA_L.OUTKA_NO_L        " & vbNewLine _
        & "                     WHEN HED.INOUT_KB = '3' THEN UNSO_L.UNSO_NO_L          " & vbNewLine _
        & "                     ELSE 'X'                                               " & vbNewLine _
        & "                  END IS NULL                                               " & vbNewLine _
        & "         THEN '削除済'                                                      " & vbNewLine _
        & "         ELSE HED.OUTKA_CTL_NO                                              " & vbNewLine _
        & "       END OUTKA_CTL_NO  -- 入出荷管理番号                                  " & vbNewLine _
        & "-- ADD E 2020/02/07 010901                                                  " & vbNewLine _
        & "-- ADD S 2020/02/27 010901                                                  " & vbNewLine _
        & "      ,HED.CUST_CD_L                                                        " & vbNewLine _
        & "      ,HED.CUST_CD_M                                                        " & vbNewLine _
        & "-- ADD E 2020/02/27 010901                                                  " & vbNewLine _
        & "      ,STP1.REQUEST_START_DATE_TIME AS SHUKKA_DATE                          " & vbNewLine _
        & "      ,STP2.REQUEST_START_DATE_TIME AS NONYU_DATE                           " & vbNewLine _
        & "      ,STP1.LOCATION_ID             AS SHUKKA_MOTO_CD                       " & vbNewLine _
        & "      ,STP1.CITY + '  ' + STP1.NAME AS SHUKKA_MOTO  -- MOD 2020/03/25 011731" & vbNewLine _
        & "      ,STP2.LOCATION_ID             AS NONYU_SAKI_CD                        " & vbNewLine _
        & "      ,STP2.CITY + '  ' + STP2.NAME AS NONYU_SAKI   -- MOD 2020/03/25 011731" & vbNewLine _
        & "      ,SUM(CONVERT(DECIMAL(21,3),CMD.MAXIMUM_WEIGHT)) AS MAXIMUM_WEIGHT     " & vbNewLine _
        & "      ,HED.CRT_DATE AS HED_CRT_DATE                                         " & vbNewLine _
        & "      ,HED.FILE_NAME AS HED_FILE_NAME                                       " & vbNewLine _
        & "      ,HED.SYS_UPD_DATE AS HED_UPD_DATE                                     " & vbNewLine _
        & "      ,HED.SYS_UPD_TIME AS HED_UPD_TIME                                     " & vbNewLine _
        & "-- 2019/03/27 ADD START                                                     " & vbNewLine _
        & "      ,STP1.GYO AS STP1_GYO                                                 " & vbNewLine _
        & "      ,STP1.SYS_UPD_DATE AS STP1_UPD_DATE                                   " & vbNewLine _
        & "      ,STP1.SYS_UPD_TIME AS STP1_UPD_TIME                                   " & vbNewLine _
        & "      ,STP2.GYO AS STP2_GYO                                                 " & vbNewLine _
        & "      ,STP2.SYS_UPD_DATE AS STP2_UPD_DATE                                   " & vbNewLine _
        & "      ,STP2.SYS_UPD_TIME AS STP2_UPD_TIME                                   " & vbNewLine _
        & "-- 2019/03/27 ADD END                                                       " & vbNewLine _
        & "      ,STP1.STOP_NOTE AS P_STOP_NOTE                                        " & vbNewLine _
        & "      ,STP2.STOP_NOTE AS D_STOP_NOTE                -- ADD 2020/03/25 011731" & vbNewLine _
        & "      ,(SELECT ',' + SKU_NUMBER                                             " & vbNewLine _
        & "          FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD                             " & vbNewLine _
        & "         WHERE CRT_DATE = HED.CRT_DATE                                      " & vbNewLine _
        & "           AND FILE_NAME = HED.FILE_NAME                                    " & vbNewLine _
        & "           AND SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "           FOR XML PATH('')                                                 " & vbNewLine _
        & "       ) AS SKU_NUMBER                                                      " & vbNewLine _
        & "      ,(SELECT ',' + NUMBER_PIECES                                          " & vbNewLine _
        & "          FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD                             " & vbNewLine _
        & "         WHERE CRT_DATE = HED.CRT_DATE                                      " & vbNewLine _
        & "           AND FILE_NAME = HED.FILE_NAME                                    " & vbNewLine _
        & "           AND SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "           FOR XML PATH('')                                                 " & vbNewLine _
        & "       ) AS NUMBER_PIECES                                                   " & vbNewLine _
        & "      ,CASE                                                                 " & vbNewLine _
        & "         WHEN HED.OUTKA_CTL_NO <> ''                                        " & vbNewLine _
        & "              AND CASE                                                      " & vbNewLine _
        & "                     WHEN HED.INOUT_KB = '1' THEN INKA_L.INKA_NO_L          " & vbNewLine _
        & "                     WHEN HED.INOUT_KB = '2' THEN OUTKA_L.OUTKA_NO_L        " & vbNewLine _
        & "                     WHEN HED.INOUT_KB = '3' THEN UNSO_L.UNSO_NO_L          " & vbNewLine _
        & "                     ELSE 'X'                                               " & vbNewLine _
        & "                  END IS NULL                                               " & vbNewLine _
        & "         THEN HED.OUTKA_CTL_NO                                              " & vbNewLine _
        & "         ELSE ''                                                            " & vbNewLine _
        & "       END AS OUTKA_CTL_NO_DELETED                                          " & vbNewLine _
        & "      ,SPM.SEQ_DESC                                                         " & vbNewLine _
        & "      ,SPM.BUYID                                                            " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                                    " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
        & "-- INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD_BUMON                     " & vbNewLine _
        & "--    ON CMD_BUMON.CRT_DATE = HED.CRT_DATE                                    " & vbNewLine _
        & "--   AND CMD_BUMON.FILE_NAME = HED.FILE_NAME                                  " & vbNewLine _
        & "--   AND CMD_BUMON.GYO = '1' -- Commodityの1行目を判別に使用                  " & vbNewLine _
        & "--   AND CMD_BUMON.SYS_DEL_FLG = '0'                                          " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR                           " & vbNewLine _
        & "    ON CRR.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND CRR.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND CRR.GYO = '1' -- Carriersは常に1行                                   " & vbNewLine _
        & "   AND CRR.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine _
        & "  LEFT JOIN                                                                 " & vbNewLine _
        & "       (SELECT *                                                            " & vbNewLine _
        & "              ,ROW_NUMBER() OVER(PARTITION BY SHIPMENT_ID ORDER BY SYS_ENT_DATE DESC, SYS_ENT_TIME DESC) AS SEQ_DESC  " & vbNewLine _
        & "          FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM                             " & vbNewLine _
        & "         WHERE SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "       )  SPM                                                               " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                            " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1                          " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                                         " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                                       " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
        & "--   AND STP1.STOP_SEQ_NUM = '1' -- 1:出荷元                                  " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'  -- 出荷元                                      " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2                          " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                                         " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                                       " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
        & "--   AND STP2.STOP_SEQ_NUM = '2' -- 2:納入先                                  " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'  -- 納入先                                      " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD                           " & vbNewLine _
        & "    ON CMD.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND CMD.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND CMD.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "-- ADD S 2020/02/07 010901                                                  " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..C_OUTKA_L  OUTKA_L                                    " & vbNewLine _
        & "    ON OUTKA_L.NRS_BR_CD = @NRS_BR_CD                                       " & vbNewLine _
        & "   AND OUTKA_L.OUTKA_NO_L = HED.OUTKA_CTL_NO                                " & vbNewLine _
        & "   AND OUTKA_L.SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "-- ADD E 2020/02/07 010901                                                  " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..B_INKA_L  INKA_L                                      " & vbNewLine _
        & "    ON INKA_L.NRS_BR_CD = @NRS_BR_CD                                        " & vbNewLine _
        & "   AND INKA_L.INKA_NO_L = HED.OUTKA_CTL_NO  -- 入荷管理番号L                " & vbNewLine _
        & "   AND INKA_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..F_UNSO_L  UNSO_L                                      " & vbNewLine _
        & "    ON UNSO_L.UNSO_NO_L = HED.OUTKA_CTL_NO  -- 運送番号L                    " & vbNewLine _
        & "   AND UNSO_L.SYS_DEL_FLG = '0'                                             " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  SHUKKADELAY                         " & vbNewLine _
        & "    ON SHUKKADELAY.CRT_DATE = HED.CRT_DATE                                  " & vbNewLine _
        & "   AND SHUKKADELAY.FILE_NAME = HED.FILE_NAME                                " & vbNewLine _
        & "   AND SHUKKADELAY.GYO = '6'  -- 6:出荷遅延                                 " & vbNewLine _
        & "   AND SHUKKADELAY.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  NONYUDELAY                          " & vbNewLine _
        & "    ON NONYUDELAY.CRT_DATE = HED.CRT_DATE                                   " & vbNewLine _
        & "   AND NONYUDELAY.FILE_NAME = HED.FILE_NAME                                 " & vbNewLine _
        & "   AND NONYUDELAY.GYO = '7'  -- 7:納入遅延                                  " & vbNewLine _
        & "   AND NONYUDELAY.SYS_DEL_FLG = '0'                                         " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "   AND UPPER(                                                                                                                " & vbNewLine _
        & "         CASE WHEN HED.STATUS_KB <> '3'                                                                                        " & vbNewLine _
        & "              THEN CRR.CUSTOM_EQUIPMENT_TYPE                                                                                   " & vbNewLine _
        & "              ELSE ISNULL(                                                                                                     " & vbNewLine _
        & "                (SELECT TOP 1 CRR2.CUSTOM_EQUIPMENT_TYPE                                                                       " & vbNewLine _
        & "                   FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM2                                                                 " & vbNewLine _
        & "                  INNER JOIN $LM_TRN$..H_OUTKAEDI_HED_HWL  HED2                                                                " & vbNewLine _
        & "                     ON HED2.CRT_DATE = SPM2.CRT_DATE                                                                          " & vbNewLine _
        & "                    AND HED2.FILE_NAME = SPM2.FILE_NAME                                                                        " & vbNewLine _
        & "                  INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR2                                                            " & vbNewLine _
        & "                     ON CRR2.CRT_DATE = SPM2.CRT_DATE                                                                          " & vbNewLine _
        & "                    AND CRR2.FILE_NAME = SPM2.FILE_NAME                                                                        " & vbNewLine _
        & "                  WHERE SPM2.GYO = '1' -- ShipmentDetailsは常に1行                                                             " & vbNewLine _
        & "                    AND SPM2.SHIPMENT_ID = SPM.SHIPMENT_ID                                                                     " & vbNewLine _
        & "                    --- SPM2.SYS_DEL_FLGは見ない(新規データの出荷登録前に削除データが来た場合、新規データは論理削除されるため) " & vbNewLine _
        & "                    AND HED2.STATUS_KB <> '3'                                                                                  " & vbNewLine _
        & "                    AND CRR2.GYO = '1' -- Carriersは常に1行                                                                    " & vbNewLine _
        & "                    AND CRR2.SYS_ENT_DATE + CRR2.SYS_ENT_TIME < CRR.SYS_ENT_DATE + CRR.SYS_ENT_TIME                            " & vbNewLine _
        & "                    --- CRR2.SYS_DEL_FLGは見ない(新規データの出荷登録前に削除データが来た場合、新規データは論理削除されるため) " & vbNewLine _
        & "                  ORDER BY CRR2.SYS_ENT_DATE DESC                                                                              " & vbNewLine _
        & "                          ,CRR2.SYS_ENT_TIME DESC                                                                              " & vbNewLine _
        & "                ), '')                                                                                                         " & vbNewLine _
        & "         END                                                                                                                   " & vbNewLine _
        & "       ) NOT LIKE '%ISO%'                                                                                                      " & vbNewLine _
        & "   AND UPPER(                                                                                                                  " & vbNewLine _
        & "         CASE WHEN HED.STATUS_KB <> '3'                                                                                        " & vbNewLine _
        & "              THEN CRR.CUSTOM_EQUIPMENT_TYPE                                                                                   " & vbNewLine _
        & "              ELSE ISNULL(                                                                                                     " & vbNewLine _
        & "                (SELECT TOP 1 CRR2.CUSTOM_EQUIPMENT_TYPE                                                                       " & vbNewLine _
        & "                   FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM2                                                                 " & vbNewLine _
        & "                  INNER JOIN $LM_TRN$..H_OUTKAEDI_HED_HWL  HED2                                                                " & vbNewLine _
        & "                     ON HED2.CRT_DATE = SPM2.CRT_DATE                                                                          " & vbNewLine _
        & "                    AND HED2.FILE_NAME = SPM2.FILE_NAME                                                                        " & vbNewLine _
        & "                  INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR2                                                            " & vbNewLine _
        & "                     ON CRR2.CRT_DATE = SPM2.CRT_DATE                                                                          " & vbNewLine _
        & "                    AND CRR2.FILE_NAME = SPM2.FILE_NAME                                                                        " & vbNewLine _
        & "                  WHERE SPM2.GYO = '1' -- ShipmentDetailsは常に1行                                                             " & vbNewLine _
        & "                    AND SPM2.SHIPMENT_ID = SPM.SHIPMENT_ID                                                                     " & vbNewLine _
        & "                    --- SPM2.SYS_DEL_FLGは見ない(新規データの出荷登録前に削除データが来た場合、新規データは論理削除されるため) " & vbNewLine _
        & "                    AND HED2.STATUS_KB <> '3'                                                                                  " & vbNewLine _
        & "                    AND CRR2.GYO = '1' -- Carriersは常に1行                                                                    " & vbNewLine _
        & "                    AND CRR2.SYS_ENT_DATE + CRR2.SYS_ENT_TIME < CRR.SYS_ENT_DATE + CRR.SYS_ENT_TIME                            " & vbNewLine _
        & "                    --- CRR2.SYS_DEL_FLGは見ない(新規データの出荷登録前に削除データが来た場合、新規データは論理削除されるため) " & vbNewLine _
        & "                  ORDER BY CRR2.SYS_ENT_DATE DESC                                                                              " & vbNewLine _
        & "                          ,CRR2.SYS_ENT_TIME DESC                                                                              " & vbNewLine _
        & "                ), '')                                                                                                         " & vbNewLine _
        & "         END                                                                                                                   " & vbNewLine _
        & "       )                                                                                                                       " & vbNewLine
    'DEL S 2020/04/17 012230
    '   & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
    '   & "--   -- Commodityの1行目のCommodityDescriptionに'ISO'を含まない               " & vbNewLine _
    '   & "--   AND UPPER(ISNULL(CMD_BUMON.COMMODITY_DESCRIPTION,'')) NOT LIKE 'ISO %'   " & vbNewLine _
    '   & "--   AND UPPER(ISNULL(CMD_BUMON.COMMODITY_DESCRIPTION,'')) NOT LIKE '% ISO %' " & vbNewLine _
    '   & "--   AND UPPER(ISNULL(CMD_BUMON.COMMODITY_DESCRIPTION,'')) NOT LIKE '% ISO'   " & vbNewLine _
    '   & "   AND UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) NOT LIKE 'ISO %'                    " & vbNewLine _
    '   & "   AND UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) NOT LIKE '% ISO %'                  " & vbNewLine _
    '   & "   AND UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) NOT LIKE '% ISO'                    " & vbNewLine _
    '   & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine
    'DEL E 2020/04/17 012230
    '▲メインSQL（倉庫/Chilled Lorry）

    '▼メインSQL（倉庫2）
    Private Const SQL_SELECT_SEARCH_DATA_SOKO_2 As String = "" _
        & "         NOT LIKE '%CHILLED LORRY%'                                                                                            " & vbNewLine
    '▲メインSQL（倉庫2）

    '▼メインSQL（Chilled Lorry）
    Private Const SQL_SELECT_SEARCH_DATA_CHILLED_LORRY As String = "" _
        & "         LIKE '%CHILLED LORRY%'                                                                                                " & vbNewLine
    '▲メインSQL（Chilled Lorry）

    '▼追加WHERE句
    Private Const SQL_SELECT_SEARCH_DATA_COND_SHUKKA_DATE_FROM As String =
          "   AND LEFT(STP1.REQUEST_START_DATE_TIME,8) >= @SHUKKA_DATE_FROM            " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SHUKKA_DATE_TO As String =
          "   AND LEFT(STP1.REQUEST_START_DATE_TIME,8) <= @SHUKKA_DATE_TO              " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SHINCHOKU_KB_JUCHU As String =
          "   AND HED.SHINCHOKU_KB_JUCHU IN(@SHINCHOKU_KB_JUCHU)                       " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_STATUS_KB As String =
          "   AND HED.STATUS_KB = @STATUS_KB                                           " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_DEL_KB As String =
          "   AND HED.DEL_KB = @DEL_KB                                                 " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SHINCHOKU_KB As String =
          "   AND HED.SHINCHOKU_KB = @SHINCHOKU_KB                                     " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_DELAY_STATUS As String =
          "   AND CASE                                                                 " & vbNewLine _
        & "         WHEN HED.SHINCHOKU_KB = '1' AND SHUKKADELAY.CRT_DATE IS NOT NULL   " & vbNewLine _
        & "         THEN '01'                                                          " & vbNewLine _
        & "         WHEN HED.SHINCHOKU_KB = '4' AND NONYUDELAY.CRT_DATE IS NOT NULL    " & vbNewLine _
        & "         THEN '02'                                                          " & vbNewLine _
        & "         ELSE ''                                                            " & vbNewLine _
        & "       END = @DELAY_STATUS                                                  " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_CYLINDER_SERIAL_NO As String =
          "   AND HED.CYLINDER_SERIAL_NO LIKE @CYLINDER_SERIAL_NO                      " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_GOODS_CD As String =
          "   AND RIGHT(CMD.SKU_NUMBER,8) LIKE @GOODS_CD                               " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_GOODS_NM As String =
          "   AND Z_KBN_LANG.KBN_NM1 LIKE @GOODS_NM                                    " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SHIPMENT_ID As String =
          "   AND SPM.SHIPMENT_ID LIKE @SHIPMENT_ID                                    " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SAP_ORD_NO As String =
          "   AND LEFT(SPM.CON+',',CHARINDEX(',',SPM.CON+',')-1) LIKE @SAP_ORD_NO      " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_INOUT_KB As String =
          "   AND HED.INOUT_KB IN(@INOUT_KB)                                           " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_OUTKA_CTL_NO As String =
          "   AND HED.OUTKA_CTL_NO LIKE @OUTKA_CTL_NO                                  " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_CUST_ORD_NO As String =
          "   AND SPM.CON LIKE @CUST_ORD_NO                                            " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_CUST_CD_L As String =
          "   AND HED.CUST_CD_L LIKE @CUST_CD_L                                        " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_CUST_CD_M As String =
          "   AND HED.CUST_CD_M LIKE @CUST_CD_M                                        " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SHUKKA_MOTO_CD As String =
          "   AND STP1.LOCATION_ID LIKE @SHUKKA_MOTO_CD                                " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_SHUKKA_MOTO As String =
          "   AND STP1.CITY + '  ' + STP1.NAME LIKE @SHUKKA_MOTO                       " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_NONYU_SAKI_CD As String =
          "   AND STP2.LOCATION_ID LIKE @NONYU_SAKI_CD                                 " & vbNewLine

    Private Const SQL_SELECT_SEARCH_DATA_COND_NONYU_SAKI As String =
          "   AND STP2.CITY + '  ' + STP2.NAME LIKE @NONYU_SAKI                        " & vbNewLine
    '▲追加WHERE句

    '▼GROUP句（倉庫）
    Private Const SQL_GROUP_SEARCH_DATA_SOKO As String =
          " GROUP BY HED.CRT_DATE                                                      " & vbNewLine _
        & "         ,HED.FILE_NAME                                                     " & vbNewLine _
        & "-- ADD S 2019/12/12 009741                                                  " & vbNewLine _
        & "         ,HED.STATUS_KB                                                     " & vbNewLine _
        & "         ,HED.DEL_KB                    -- ADD 2020/02/07 010901            " & vbNewLine _
        & "         ,HED.SHINCHOKU_KB_JUCHU                                            " & vbNewLine _
        & "-- ADD E 2019/12/12 009741                                                  " & vbNewLine _
        & "         ,HED.SHINCHOKU_KB                                                  " & vbNewLine _
        & "         ,SHUKKADELAY.CRT_DATE                                              " & vbNewLine _
        & "         ,NONYUDELAY.CRT_DATE                                               " & vbNewLine _
        & "         ,HED.CYLINDER_SERIAL_NO                                            " & vbNewLine _
        & "         ,SPM.SHIPMENT_ID                                                   " & vbNewLine _
        & "         ,SPM.CON                                                           " & vbNewLine _
        & "         ,HED.INOUT_KB                                                      " & vbNewLine _
        & "         ,HED.OUTKA_CTL_NO              -- ADD 2020/02/07 010901            " & vbNewLine _
        & "         ,OUTKA_L.OUTKA_NO_L            -- ADD 2020/02/07 010901            " & vbNewLine _
        & "         ,INKA_L.INKA_NO_L                                                  " & vbNewLine _
        & "         ,UNSO_L.UNSO_NO_L                                                  " & vbNewLine _
        & "-- ADD S 2020/02/27 010901                                                  " & vbNewLine _
        & "         ,HED.CUST_CD_L                                                     " & vbNewLine _
        & "         ,HED.CUST_CD_M                                                     " & vbNewLine _
        & "-- ADD E 2020/02/27 010901                                                  " & vbNewLine _
        & "         ,STP1.REQUEST_START_DATE_TIME                                      " & vbNewLine _
        & "         ,STP2.REQUEST_START_DATE_TIME                                      " & vbNewLine _
        & "         ,STP1.LOCATION_ID                                                  " & vbNewLine _
        & "         ,STP1.CITY                     -- ADD 2020/03/25 011731            " & vbNewLine _
        & "         ,STP1.NAME                                                         " & vbNewLine _
        & "         ,STP2.LOCATION_ID                                                  " & vbNewLine _
        & "         ,STP2.CITY                     -- ADD 2020/03/25 011731            " & vbNewLine _
        & "         ,STP2.NAME                                                         " & vbNewLine _
        & "         ,HED.SYS_ENT_DATE              -- ADD 2020/02/07 010901            " & vbNewLine _
        & "         ,HED.SYS_ENT_TIME              -- ADD 2020/02/07 010901            " & vbNewLine _
        & "         ,HED.SYS_UPD_DATE                                                  " & vbNewLine _
        & "         ,HED.SYS_UPD_TIME                                                  " & vbNewLine _
        & "-- 2019/03/27 ADD START                                                     " & vbNewLine _
        & "         ,STP1.GYO                                                          " & vbNewLine _
        & "         ,STP1.SYS_UPD_DATE                                                 " & vbNewLine _
        & "         ,STP1.SYS_UPD_TIME                                                 " & vbNewLine _
        & "         ,STP2.GYO                                                          " & vbNewLine _
        & "         ,STP2.SYS_UPD_DATE                                                 " & vbNewLine _
        & "         ,STP2.SYS_UPD_TIME                                                 " & vbNewLine _
        & "-- 2019/03/27 ADD END                                                       " & vbNewLine _
        & "         ,STP1.STOP_NOTE                                                    " & vbNewLine _
        & "         ,STP2.STOP_NOTE                -- ADD 2020/03/25 011731            " & vbNewLine _
        & "--         ,CRR.CUSTOM_EQUIPMENT_TYPE                                         " & vbNewLine _
        & "--         ,CRR.SYS_ENT_DATE                                                  " & vbNewLine _
        & "--         ,CRR.SYS_ENT_TIME                                                  " & vbNewLine _
        & "         ,SPM.SEQ_DESC                                                      " & vbNewLine _
        & "         ,SPM.BUYID                                                         " & vbNewLine
    '▲GROUP句（倉庫）

    '▼メインSQL（ISO）
    Private Const SQL_SELECT_SEARCH_DATA_ISO As String =
          "SELECT                                                                      " & vbNewLine _
        & "-- ADD S 2019/12/12 009741                                                  " & vbNewLine _
        & "       CASE HED.STATUS_KB                                                   " & vbNewLine _
        & "         WHEN '1' THEN ''                                                   " & vbNewLine _
        & "         WHEN '2' THEN '訂正'                                               " & vbNewLine _
        & "         WHEN '3' THEN '取消'                                               " & vbNewLine _
        & "         ELSE HED.STATUS_KB                                                 " & vbNewLine _
        & "       END STATUS_KB                                                        " & vbNewLine _
        & "-- ADD S 2020/02/07 010901                                                  " & vbNewLine _
        & "      ,CASE HED.DEL_KB                                                      " & vbNewLine _
        & "         WHEN '0' THEN '正常'                                               " & vbNewLine _
        & "         WHEN '3' THEN '保留'                                               " & vbNewLine _
        & "         ELSE HED.DEL_KB                                                    " & vbNewLine _
        & "       END DEL_KB                                                           " & vbNewLine _
        & "-- ADD E 2020/02/07 010901                                                  " & vbNewLine _
        & "      ,CASE HED.SHINCHOKU_KB_JUCHU                                          " & vbNewLine _
        & "         WHEN '1' THEN '未処理'      -- MOD 2020/02/07 010901               " & vbNewLine _
        & "         WHEN '2' THEN '受注OK'                                             " & vbNewLine _
        & "         WHEN '3' THEN '受注NG'                                             " & vbNewLine _
        & "         WHEN '4' THEN '受注登録済'  -- ADD 2020/02/07 010901               " & vbNewLine _
        & "         WHEN '5' THEN '実績作成済'  -- ADD 2020/02/07 010901               " & vbNewLine _
        & "         WHEN '6' THEN 'EDI取消'                                            " & vbNewLine _
        & "         ELSE HED.SHINCHOKU_KB_JUCHU                                        " & vbNewLine _
        & "       END SHINCHOKU_KB_JUCHU                                               " & vbNewLine _
        & "-- ADD E 2019/12/12 009741                                                  " & vbNewLine _
        & "      ,CASE HED.SHINCHOKU_KB                                                " & vbNewLine _
        & "         WHEN '1' THEN '未送信'                                             " & vbNewLine _
        & "         WHEN '2' THEN 'ピック済'                                           " & vbNewLine _
        & "         WHEN '3' THEN '荷下ろし済'                                         " & vbNewLine _
        & "         WHEN '4' THEN '納入予定'                                           " & vbNewLine _
        & "         ELSE HED.SHINCHOKU_KB                                              " & vbNewLine _
        & "       END SHINCHOKU_KB                                                     " & vbNewLine _
        & "      ,CASE                                                                 " & vbNewLine _
        & "         WHEN HED.SHINCHOKU_KB = '1' AND SHUKKADELAY.CRT_DATE IS NOT NULL   " & vbNewLine _
        & "         THEN '出荷遅延'                                                    " & vbNewLine _
        & "         WHEN HED.SHINCHOKU_KB = '4' AND NONYUDELAY.CRT_DATE IS NOT NULL    " & vbNewLine _
        & "         THEN '納入遅延'                                                    " & vbNewLine _
        & "         ELSE ''                                                            " & vbNewLine _
        & "       END DELAY_STATUS                                                     " & vbNewLine _
        & "      ,'' AS CYLINDER_SERIAL_NO                                             " & vbNewLine _
        & "      ,RIGHT(CMD.SKU_NUMBER,8) AS GOODS_CD                                  " & vbNewLine _
        & "      ,Z_KBN_LANG.KBN_NM1      AS GOODS_NM                                  " & vbNewLine _
        & "      ,SPM.SHIPMENT_ID                                                      " & vbNewLine _
        & "      ,LEFT(SPM.CON+',',CHARINDEX(',',SPM.CON+',')-1) AS SAP_ORD_NO         " & vbNewLine _
        & "      ,'' AS CUST_ORD_NO                                                    " & vbNewLine _
        & "      ,'' AS INOUT_KB                                                       " & vbNewLine _
        & "-- ADD S 2020/02/07 010901                                                  " & vbNewLine _
        & "      ,CASE                                                                 " & vbNewLine _
        & "         WHEN HED.OUTKA_CTL_NO <> '' AND C_BASIS.JOB_NO IS NULL THEN '削除済'  " & vbNewLine _
        & "         ELSE HED.OUTKA_CTL_NO                                              " & vbNewLine _
        & "       END OUTKA_CTL_NO                                                     " & vbNewLine _
        & "-- ADD E 2020/02/07 010901                                                  " & vbNewLine _
        & "-- ADD S 2020/02/27 010901                                                  " & vbNewLine _
        & "      ,HED.CUST_CD_L                                                        " & vbNewLine _
        & "      ,HED.CUST_CD_M                                                        " & vbNewLine _
        & "-- ADD E 2020/02/27 010901                                                  " & vbNewLine _
        & "      ,STP1.REQUEST_START_DATE_TIME AS SHUKKA_DATE                          " & vbNewLine _
        & "      ,STP2.REQUEST_START_DATE_TIME AS NONYU_DATE                           " & vbNewLine _
        & "      ,STP1.LOCATION_ID             AS SHUKKA_MOTO_CD                       " & vbNewLine _
        & "      ,STP1.CITY + '  ' + STP1.NAME AS SHUKKA_MOTO  -- MOD 2020/03/25 011731" & vbNewLine _
        & "      ,STP2.LOCATION_ID             AS NONYU_SAKI_CD                        " & vbNewLine _
        & "      ,STP2.CITY + '  ' + STP2.NAME AS NONYU_SAKI   -- MOD 2020/03/25 011731" & vbNewLine _
        & "      ,CONVERT(DECIMAL(21,3),ISNULL(CMD.MAXIMUM_WEIGHT,0)) AS MAXIMUM_WEIGHT    " & vbNewLine _
        & "      ,HED.CRT_DATE AS HED_CRT_DATE                                         " & vbNewLine _
        & "      ,HED.FILE_NAME AS HED_FILE_NAME                                       " & vbNewLine _
        & "      ,HED.SYS_UPD_DATE AS HED_UPD_DATE                                     " & vbNewLine _
        & "      ,HED.SYS_UPD_TIME AS HED_UPD_TIME                                     " & vbNewLine _
        & "-- 2019/03/27 ADD START                                                     " & vbNewLine _
        & "      ,STP1.GYO AS STP1_GYO                                                 " & vbNewLine _
        & "      ,STP1.SYS_UPD_DATE AS STP1_UPD_DATE                                   " & vbNewLine _
        & "      ,STP1.SYS_UPD_TIME AS STP1_UPD_TIME                                   " & vbNewLine _
        & "      ,STP2.GYO AS STP2_GYO                                                 " & vbNewLine _
        & "      ,STP2.SYS_UPD_DATE AS STP2_UPD_DATE                                   " & vbNewLine _
        & "      ,STP2.SYS_UPD_TIME AS STP2_UPD_TIME                                   " & vbNewLine _
        & "-- 2019/03/27 ADD END                                                       " & vbNewLine _
        & "      ,STP1.STOP_NOTE AS P_STOP_NOTE                                        " & vbNewLine _
        & "      ,STP2.STOP_NOTE AS D_STOP_NOTE                -- ADD 2020/03/25 011731" & vbNewLine _
        & "      ,(SELECT ',' + SKU_NUMBER                                             " & vbNewLine _
        & "          FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD                             " & vbNewLine _
        & "         WHERE CRT_DATE = HED.CRT_DATE                                      " & vbNewLine _
        & "           AND FILE_NAME = HED.FILE_NAME                                    " & vbNewLine _
        & "           AND SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "           FOR XML PATH('')                                                 " & vbNewLine _
        & "       ) AS SKU_NUMBER                                                      " & vbNewLine _
        & "      ,(SELECT ',' + NUMBER_PIECES                                          " & vbNewLine _
        & "          FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD                             " & vbNewLine _
        & "         WHERE CRT_DATE = HED.CRT_DATE                                      " & vbNewLine _
        & "           AND FILE_NAME = HED.FILE_NAME                                    " & vbNewLine _
        & "           AND SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "           FOR XML PATH('')                                                 " & vbNewLine _
        & "       ) AS NUMBER_PIECES                                                   " & vbNewLine _
        & "      ,'' AS OUTKA_CTL_NO_DELETED                                           " & vbNewLine _
        & "      ,SPM.SEQ_DESC                                                         " & vbNewLine _
        & "      ,SPM.BUYID                                                            " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                                    " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
        & "-- INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD_BUMON                     " & vbNewLine _
        & "--    ON CMD_BUMON.CRT_DATE = HED.CRT_DATE                                    " & vbNewLine _
        & "--   AND CMD_BUMON.FILE_NAME = HED.FILE_NAME                                  " & vbNewLine _
        & "--   AND CMD_BUMON.GYO = '1' -- Commodityの1行目を判別に使用                  " & vbNewLine _
        & "--   AND CMD_BUMON.SYS_DEL_FLG = '0'                                          " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR                           " & vbNewLine _
        & "    ON CRR.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND CRR.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND CRR.GYO = '1' -- Carriersは常に1行                                   " & vbNewLine _
        & "   AND CRR.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine _
        & "  LEFT JOIN                                                                 " & vbNewLine _
        & "       (SELECT *                                                            " & vbNewLine _
        & "              ,ROW_NUMBER() OVER(PARTITION BY SHIPMENT_ID ORDER BY SYS_ENT_DATE DESC, SYS_ENT_TIME DESC) AS SEQ_DESC  " & vbNewLine _
        & "          FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM                             " & vbNewLine _
        & "         WHERE SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "       )  SPM                                                               " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                            " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1                          " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                                         " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                                       " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
        & "--   AND STP1.STOP_SEQ_NUM = '1' -- 1:出荷元                                  " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'  -- 出荷元                                      " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2                          " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                                         " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                                       " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
        & "--   AND STP2.STOP_SEQ_NUM = '2' -- 2:納入先                                  " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'  -- 納入先                                      " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD                           " & vbNewLine _
        & "    ON CMD.CRT_DATE = HED.CRT_DATE                                          " & vbNewLine _
        & "   AND CMD.FILE_NAME = HED.FILE_NAME                                        " & vbNewLine _
        & "   AND RIGHT(CMD.SKU_NUMBER,8) <> '10305599'                                " & vbNewLine _
        & "   AND CMD.SKU_NUMBER <> ''                                                 " & vbNewLine _
        & "   AND CMD.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "  LEFT JOIN MST_DB..Z_KBN_LANG                                              " & vbNewLine _
        & "    ON Z_KBN_LANG.KBN_GROUP_CD = 'H00018'                                   " & vbNewLine _
        & "   AND Z_KBN_LANG.KBN_NM4 = RIGHT(CMD.SKU_NUMBER,8)                         " & vbNewLine _
        & "   AND Z_KBN_LANG.KBN_LANG = 'en'                                           " & vbNewLine _
        & "   AND Z_KBN_LANG.SYS_DEL_FLG = '0'                                         " & vbNewLine _
        & "-- ADD S 2020/02/07 010901                                                  " & vbNewLine _
        & "  LEFT JOIN GL_DB..C_BASIS                                                  " & vbNewLine _
        & "    ON C_BASIS.JOB_NO = HED.OUTKA_CTL_NO                                    " & vbNewLine _
        & "   AND C_BASIS.SYS_DEL_FLG = '0'                                            " & vbNewLine _
        & "   AND C_BASIS.BKG_STAGE_KBN <> '00099'                                     " & vbNewLine _
        & "-- ADD E 2020/02/07 010901                                                  " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  SHUKKADELAY                         " & vbNewLine _
        & "    ON SHUKKADELAY.CRT_DATE = HED.CRT_DATE                                  " & vbNewLine _
        & "   AND SHUKKADELAY.FILE_NAME = HED.FILE_NAME                                " & vbNewLine _
        & "   AND SHUKKADELAY.GYO = '6'  -- 6:出荷遅延                                 " & vbNewLine _
        & "   AND SHUKKADELAY.SYS_DEL_FLG = '0'                                        " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  NONYUDELAY                          " & vbNewLine _
        & "    ON NONYUDELAY.CRT_DATE = HED.CRT_DATE                                   " & vbNewLine _
        & "   AND NONYUDELAY.FILE_NAME = HED.FILE_NAME                                 " & vbNewLine _
        & "   AND NONYUDELAY.GYO = '7'  -- 7:納入遅延                                  " & vbNewLine _
        & "   AND NONYUDELAY.SYS_DEL_FLG = '0'                                         " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                                " & vbNewLine _
        & "   AND UPPER(                                                                                                                  " & vbNewLine _
        & "         CASE WHEN HED.STATUS_KB <> '3'                                                                                        " & vbNewLine _
        & "              THEN CRR.CUSTOM_EQUIPMENT_TYPE                                                                                   " & vbNewLine _
        & "              ELSE ISNULL(                                                                                                     " & vbNewLine _
        & "                (SELECT TOP 1 CRR2.CUSTOM_EQUIPMENT_TYPE                                                                       " & vbNewLine _
        & "                   FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM2                                                                 " & vbNewLine _
        & "                  INNER JOIN $LM_TRN$..H_OUTKAEDI_HED_HWL  HED2                                                                " & vbNewLine _
        & "                     ON HED2.CRT_DATE = SPM2.CRT_DATE                                                                          " & vbNewLine _
        & "                    AND HED2.FILE_NAME = SPM2.FILE_NAME                                                                        " & vbNewLine _
        & "                  INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR2                                                            " & vbNewLine _
        & "                     ON CRR2.CRT_DATE = SPM2.CRT_DATE                                                                          " & vbNewLine _
        & "                    AND CRR2.FILE_NAME = SPM2.FILE_NAME                                                                        " & vbNewLine _
        & "                  WHERE SPM2.GYO = '1' -- ShipmentDetailsは常に1行                                                             " & vbNewLine _
        & "                    AND SPM2.SHIPMENT_ID = SPM.SHIPMENT_ID                                                                     " & vbNewLine _
        & "                    --- SPM2.SYS_DEL_FLGは見ない(新規データの出荷登録前に削除データが来た場合、新規データは論理削除されるため) " & vbNewLine _
        & "                    AND HED2.STATUS_KB <> '3'                                                                                  " & vbNewLine _
        & "                    AND CRR2.GYO = '1' -- Carriersは常に1行                                                                    " & vbNewLine _
        & "                    AND CRR2.SYS_ENT_DATE + CRR2.SYS_ENT_TIME < CRR.SYS_ENT_DATE + CRR.SYS_ENT_TIME                            " & vbNewLine _
        & "                    --- CRR2.SYS_DEL_FLGは見ない(新規データの出荷登録前に削除データが来た場合、新規データは論理削除されるため) " & vbNewLine _
        & "                  ORDER BY CRR2.SYS_ENT_DATE DESC                                                                              " & vbNewLine _
        & "                          ,CRR2.SYS_ENT_TIME DESC                                                                              " & vbNewLine _
        & "                ), '')                                                                                                         " & vbNewLine _
        & "         END                                                                                                                   " & vbNewLine _
        & "       ) LIKE '%ISO%'                                                                                                          " & vbNewLine _
    'DEL S 2020/04/17 012230
    '   & "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
    '   & "--   -- Commodityの1行目のCommodityDescriptionに'ISO'を含む                   " & vbNewLine _
    '   & "--   AND (   UPPER(CMD_BUMON.COMMODITY_DESCRIPTION) LIKE 'ISO %'              " & vbNewLine _
    '   & "--        OR UPPER(CMD_BUMON.COMMODITY_DESCRIPTION) LIKE '% ISO %'            " & vbNewLine _
    '   & "--        OR UPPER(CMD_BUMON.COMMODITY_DESCRIPTION) LIKE '% ISO')             " & vbNewLine _
    '   & "   AND (   UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) LIKE 'ISO %'                    " & vbNewLine _
    '   & "        OR UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) LIKE '% ISO %'                  " & vbNewLine _
    '   & "        OR UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) LIKE '% ISO')                   " & vbNewLine _
    '   & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine
    'DEL E 2020/04/17 012230
    '▲メインSQL（ISO）

    '▼ORDER句
    Private Const SQL_ORDER_SEARCH As String =
          " ORDER BY SPM.SHIPMENT_ID                                             " & vbNewLine _
        & "         ,HED.SYS_ENT_DATE    -- ADD 2020/02/07 010901            " & vbNewLine _
        & "         ,HED.SYS_ENT_TIME    -- ADD 2020/02/07 010901            " & vbNewLine
    '▲ORDER句

#End Region '検索処理SQL

#Region "実績作成処理SQL"

    Private Const SQL_SELECT_SHIPMENT_ID_COUNT As String =
          "SELECT COUNT(*) AS REC_CNT              " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM " & vbNewLine _
        & " WHERE SHIPMENT_ID = @SHIPMENT_ID       " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                " & vbNewLine

    Private Const SQL_SELECT_OUTKAEDI_DATA_TSUMIKOMI As String =
          "SELECT HED.CRT_DATE                                        " & vbNewLine _
        & "      ,HED.FILE_NAME                                       " & vbNewLine _
        & "      ,SPM.SHIPMENT_ID                                     " & vbNewLine _
        & "      ,HED.CYLINDER_SERIAL_NO AS SHIPMENT_REF_NUM_MISC     " & vbNewLine _
        & "      ,STP1.STOP_SEQ_NUM                                   " & vbNewLine _
        & "      ,STP1.LOCATION_ID                                    " & vbNewLine _
        & "      ,LEFT(STP1.REQUEST_START_DATE_TIME,8) AS EVENT_DATE  " & vbNewLine _
        & "      ,STP1.CITY                                           " & vbNewLine _
        & "      ,STP1.COUNTRY                                        " & vbNewLine _
        & "      ,STP1.ZIP_CODE                                       " & vbNewLine _
        & "      ,CASE WHEN HED.INOUT_KB = '1' THEN 'I'               " & vbNewLine _
        & "            WHEN HED.INOUT_KB = '2' THEN 'O'               " & vbNewLine _
        & "            WHEN HED.INOUT_KB = '3' THEN 'U'               " & vbNewLine _
        & "            ELSE ''                                        " & vbNewLine _
        & "       END + HED.OUTKA_CTL_NO AS TRACTOR_NUMBER            " & vbNewLine _
        & "      ,SEND.TRACTOR_NUMBER AS FIRST_TRACTOR_NUMBER         " & vbNewLine _
        & "      ,CRR.TRAILER_NUMBER                                  " & vbNewLine _
        & "      ,CRR.PRO_NUMBER                                      " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                   " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                 " & vbNewLine _
        & "-- INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD_BUMON    " & vbNewLine _
        & "--    ON CMD_BUMON.CRT_DATE = HED.CRT_DATE                   " & vbNewLine _
        & "--   AND CMD_BUMON.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
        & "--   AND CMD_BUMON.GYO = '1' -- Commodityの1行目を判別に使用 " & vbNewLine _
        & "--   AND CMD_BUMON.SYS_DEL_FLG = '0'                         " & vbNewLine _
        & "--  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR          " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR          " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                 " & vbNewLine _
        & "    ON CRR.CRT_DATE = HED.CRT_DATE                         " & vbNewLine _
        & "   AND CRR.FILE_NAME = HED.FILE_NAME                       " & vbNewLine _
        & "   AND CRR.GYO = '1' -- Carriersは常に1行                  " & vbNewLine _
        & "   AND CRR.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM          " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                         " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                       " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行           " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1         " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                 " & vbNewLine _
        & "--   AND STP1.STOP_SEQ_NUM = '1' -- 1:出荷元                 " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'  -- 出荷元                     " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                 " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  SEND               " & vbNewLine _
        & "    ON SEND.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND SEND.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "   AND SEND.GYO = '5' -- 5:注文応答(214)                   " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "   AND LEFT(STP1.REQUEST_START_DATE_TIME,8) = @SHUKKA_DATE " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                      " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                            " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                          " & vbNewLine _
        & "   AND HED.SHINCHOKU_KB = @SHINCHOKU_KB                    " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                    " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                    " & vbNewLine _
        & "-- 2019/03/27 ADD START                                    " & vbNewLine _
        & "   AND STP1.GYO = @STP_GYO                                 " & vbNewLine _
        & "   AND STP1.SYS_UPD_DATE = @STP_UPD_DATE                   " & vbNewLine _
        & "   AND STP1.SYS_UPD_TIME = @STP_UPD_TIME                   " & vbNewLine _
        & "-- 2019/03/27 ADD END                                      " & vbNewLine

    Private Const SQL_SELECT_OUTKAEDI_DATA_NIOROSHI As String =
          "SELECT HED.CRT_DATE                                        " & vbNewLine _
        & "      ,HED.FILE_NAME                                       " & vbNewLine _
        & "      ,SPM.SHIPMENT_ID                                     " & vbNewLine _
        & "      ,HED.CYLINDER_SERIAL_NO AS SHIPMENT_REF_NUM_MISC     " & vbNewLine _
        & "      ,STP2.STOP_SEQ_NUM                                   " & vbNewLine _
        & "      ,STP2.LOCATION_ID                                    " & vbNewLine _
        & "      ,LEFT(STP2.REQUEST_START_DATE_TIME,8) AS EVENT_DATE  " & vbNewLine _
        & "      ,STP2.CITY                                           " & vbNewLine _
        & "      ,STP2.COUNTRY                                        " & vbNewLine _
        & "      ,STP2.ZIP_CODE                                       " & vbNewLine _
        & "      ,CASE WHEN HED.INOUT_KB = '1' THEN 'I'               " & vbNewLine _
        & "            WHEN HED.INOUT_KB = '2' THEN 'O'               " & vbNewLine _
        & "            WHEN HED.INOUT_KB = '3' THEN 'U'               " & vbNewLine _
        & "            ELSE ''                                        " & vbNewLine _
        & "       END + HED.OUTKA_CTL_NO AS TRACTOR_NUMBER            " & vbNewLine _
        & "      ,SEND.TRACTOR_NUMBER AS FIRST_TRACTOR_NUMBER         " & vbNewLine _
        & "      ,CRR.TRAILER_NUMBER                                  " & vbNewLine _
        & "      ,CRR.PRO_NUMBER                                      " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                   " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                 " & vbNewLine _
        & "-- INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD_BUMON    " & vbNewLine _
        & "--    ON CMD_BUMON.CRT_DATE = HED.CRT_DATE                   " & vbNewLine _
        & "--   AND CMD_BUMON.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
        & "--   AND CMD_BUMON.GYO = '1' -- Commodityの1行目を判別に使用 " & vbNewLine _
        & "--   AND CMD_BUMON.SYS_DEL_FLG = '0'                         " & vbNewLine _
        & "--  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR          " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CRR  CRR          " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                 " & vbNewLine _
        & "    ON CRR.CRT_DATE = HED.CRT_DATE                         " & vbNewLine _
        & "   AND CRR.FILE_NAME = HED.FILE_NAME                       " & vbNewLine _
        & "   AND CRR.GYO = '1' -- Carriersは常に1行                  " & vbNewLine _
        & "   AND CRR.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM          " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                         " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                       " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行           " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1         " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                 " & vbNewLine _
        & "--   AND STP1.STOP_SEQ_NUM = '1' -- 1:出荷元                 " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'  -- 出荷元                     " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                 " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2         " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "-- MOD S 2020/03/25 011731                                 " & vbNewLine _
        & "--   AND STP2.STOP_SEQ_NUM = '2' -- 2:納入先                 " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'  -- 納入先                     " & vbNewLine _
        & "-- MOD E 2020/03/25 011731                                 " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  SEND               " & vbNewLine _
        & "    ON SEND.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND SEND.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "   AND SEND.GYO = '5' -- 5:注文応答(214)                   " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "   AND LEFT(STP1.REQUEST_START_DATE_TIME,8) = @SHUKKA_DATE " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                      " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                            " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                          " & vbNewLine _
        & "   AND HED.SHINCHOKU_KB = @SHINCHOKU_KB                    " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                    " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                    " & vbNewLine _
        & "-- 2019/03/27 ADD START                                    " & vbNewLine _
        & "   AND STP2.GYO = @STP_GYO                                 " & vbNewLine _
        & "   AND STP2.SYS_UPD_DATE = @STP_UPD_DATE                   " & vbNewLine _
        & "   AND STP2.SYS_UPD_TIME = @STP_UPD_TIME                   " & vbNewLine _
        & "-- 2019/03/27 ADD END                                      " & vbNewLine

    'DEL S 2020/04/17 012230
    'Private Const SQL_SELECT_OUTKAEDI_DATA_CONDITION_SOKO As String = _
    '      "-- MOD S 2020/03/25 011731                                                  " & vbNewLine _
    '    & "--   -- Commodityの1行目のCommodityDescriptionに'ISO'を含まない               " & vbNewLine _
    '    & "--   AND UPPER(ISNULL(CMD_BUMON.COMMODITY_DESCRIPTION,'')) NOT LIKE 'ISO %'   " & vbNewLine _
    '    & "--   AND UPPER(ISNULL(CMD_BUMON.COMMODITY_DESCRIPTION,'')) NOT LIKE '% ISO %' " & vbNewLine _
    '    & "--   AND UPPER(ISNULL(CMD_BUMON.COMMODITY_DESCRIPTION,'')) NOT LIKE '% ISO'   " & vbNewLine _
    '    & "   AND UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) NOT LIKE 'ISO %'                    " & vbNewLine _
    '    & "   AND UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) NOT LIKE '% ISO %'                  " & vbNewLine _
    '    & "   AND UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) NOT LIKE '% ISO'                    " & vbNewLine _
    '    & "-- MOD E 2020/03/25 011731                                                  " & vbNewLine

    'Private Const SQL_SELECT_OUTKAEDI_DATA_CONDITION_ISO As String = _
    '      "-- MOD S 2020/03/25 011731                                                 " & vbNewLine _
    '    & "--   -- Commodityの1行目のCommodityDescriptionに'ISO'を含む                  " & vbNewLine _
    '    & "--   AND (   UPPER(CMD_BUMON.COMMODITY_DESCRIPTION) LIKE 'ISO %'             " & vbNewLine _
    '    & "--        OR UPPER(CMD_BUMON.COMMODITY_DESCRIPTION) LIKE '% ISO %'           " & vbNewLine _
    '    & "--        OR UPPER(CMD_BUMON.COMMODITY_DESCRIPTION) LIKE '% ISO')            " & vbNewLine _
    '    & "   AND (   UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) LIKE 'ISO %'                   " & vbNewLine _
    '    & "        OR UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) LIKE '% ISO %'                 " & vbNewLine _
    '    & "        OR UPPER(CRR.CUSTOM_EQUIPMENT_TYPE) LIKE '% ISO')                  " & vbNewLine _
    '    & "-- MOD E 2020/03/25 011731                                                 " & vbNewLine
    'DEL E 2020/04/17 012230

    Private Const SQL_INSET_SENDOUTEDI As String =
          "INSERT INTO $LM_TRN$..H_SENDOUTEDI_HWL ( " & vbNewLine _
        & "     CRT_DATE                          " & vbNewLine _
        & "    ,FILE_NAME                         " & vbNewLine _
        & "    ,GYO                               " & vbNewLine _
        & "    ,CREAT_DATE_TIME                   " & vbNewLine _
        & "    ,SHIPMENT_ACCEPT                   " & vbNewLine _
        & "    ,SHIPMENT_DECLINE                  " & vbNewLine _
        & "    ,SHIPMENT_STATUS                   " & vbNewLine _
        & "    ,SHIPMENT_ID                       " & vbNewLine _
        & "    ,SHIPMENT_REF_NUM_MISC             " & vbNewLine _
        & "    ,STOP_SEQ_NUM                      " & vbNewLine _
        & "    ,LOCATION_ID                       " & vbNewLine _
        & "    ,EVENT_STATUS                      " & vbNewLine _
        & "    ,EVENT_REASON                      " & vbNewLine _
        & "    ,EVENT_DATE_TIME                   " & vbNewLine _
        & "    ,CITY                              " & vbNewLine _
        & "    ,COUNTRY                           " & vbNewLine _
        & "    ,ZIP_CODE                          " & vbNewLine _
        & "    ,EXPRESS_CARRIER_CODE              " & vbNewLine _
        & "    ,TRACTOR_NUMBER                    " & vbNewLine _
        & "    ,TRAILER_NUMBER                    " & vbNewLine _
        & "    ,PRO_NUMBER                        " & vbNewLine _
        & "    ,SHIPMENT_DECLINE_REASON           --ADD 2020/03/06 011377  " & vbNewLine _
        & "    ,JISSEKI_SHORI_FLG                 " & vbNewLine _
        & "    ,SYS_ENT_DATE                      " & vbNewLine _
        & "    ,SYS_ENT_TIME                      " & vbNewLine _
        & "    ,SYS_ENT_PGID                      " & vbNewLine _
        & "    ,SYS_ENT_USER                      " & vbNewLine _
        & "    ,SYS_UPD_DATE                      " & vbNewLine _
        & "    ,SYS_UPD_TIME                      " & vbNewLine _
        & "    ,SYS_UPD_PGID                      " & vbNewLine _
        & "    ,SYS_UPD_USER                      " & vbNewLine _
        & "    ,SYS_DEL_FLG                       " & vbNewLine _
        & ") VALUES (                             " & vbNewLine _
        & "     @CRT_DATE                         " & vbNewLine _
        & "    ,@FILE_NAME                        " & vbNewLine _
        & "    ,@GYO                              " & vbNewLine _
        & "    ,@CREAT_DATE_TIME                  " & vbNewLine _
        & "    ,@SHIPMENT_ACCEPT                  " & vbNewLine _
        & "    ,@SHIPMENT_DECLINE                 " & vbNewLine _
        & "    ,@SHIPMENT_STATUS                  " & vbNewLine _
        & "    ,@SHIPMENT_ID                      " & vbNewLine _
        & "    ,@SHIPMENT_REF_NUM_MISC            " & vbNewLine _
        & "    ,@STOP_SEQ_NUM                     " & vbNewLine _
        & "    ,@LOCATION_ID                      " & vbNewLine _
        & "    ,@EVENT_STATUS                     " & vbNewLine _
        & "    ,@EVENT_REASON                     " & vbNewLine _
        & "    ,@EVENT_DATE_TIME                  " & vbNewLine _
        & "    ,@CITY                             " & vbNewLine _
        & "    ,@COUNTRY                          " & vbNewLine _
        & "    ,@ZIP_CODE                         " & vbNewLine _
        & "    ,@EXPRESS_CARRIER_CODE             " & vbNewLine _
        & "    ,@TRACTOR_NUMBER                   " & vbNewLine _
        & "    ,@TRAILER_NUMBER                   " & vbNewLine _
        & "    ,@PRO_NUMBER                       " & vbNewLine _
        & "    ,@SHIPMENT_DECLINE_REASON          --ADD 2020/03/06 011377  " & vbNewLine _
        & "    ,@JISSEKI_SHORI_FLG                " & vbNewLine _
        & "    ,@SYS_ENT_DATE                     " & vbNewLine _
        & "    ,@SYS_ENT_TIME                     " & vbNewLine _
        & "    ,@SYS_ENT_PGID                     " & vbNewLine _
        & "    ,@SYS_ENT_USER                     " & vbNewLine _
        & "    ,@SYS_UPD_DATE                     " & vbNewLine _
        & "    ,@SYS_UPD_TIME                     " & vbNewLine _
        & "    ,@SYS_UPD_PGID                     " & vbNewLine _
        & "    ,@SYS_UPD_USER                     " & vbNewLine _
        & "    ,@SYS_DEL_FLG                      " & vbNewLine _
        & ")                                      " & vbNewLine

    Private Const SQL_UPDATE_OUTKAEDI_HED_SHINCHOKU As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL        " & vbNewLine _
        & "   SET SHINCHOKU_KB = @SHINCHOKU_KB_AFTER  " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID        " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER        " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME              " & vbNewLine _
        & "   AND SHINCHOKU_KB = @SHINCHOKU_KB_BEFORE " & vbNewLine _
        & "   AND SYS_UPD_DATE = @HED_UPD_DATE        " & vbNewLine _
        & "   AND SYS_UPD_TIME = @HED_UPD_TIME        " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                   " & vbNewLine

#End Region '実績作成処理SQL

    'ADD S 2019/12/12 009741
#Region "受注作成処理SQL"

    Private Const SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU As String =
          "SELECT COUNT(*) AS REC_CNT                                         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                           " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                  " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                 " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                               " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                   " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                              " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "-- DEL S 2020/03/06 011377                                         " & vbNewLine _
        & "--   AND (   HED.SHINCHOKU_KB_JUCHU = '1' -- 受注前                  " & vbNewLine _
        & "--        OR HED.SHINCHOKU_KB_JUCHU = ''  -- 未設定(改修前に受信)    " & vbNewLine _
        & "--       )                                                           " & vbNewLine _
        & "-- DEL E 2020/03/06 011377                                         " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine

    'ADD S 2020/03/06 011377
    Private Const SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_SOKO_OUTKA As String =
          "SELECT COUNT(*) AS REC_CNT                                         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                           " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                  " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                 " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                               " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                   " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & " INNER JOIN $LM_TRN$..C_OUTKA_L  OUTKA_L                           " & vbNewLine _
        & "    ON OUTKA_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
        & "   AND OUTKA_L.OUTKA_NO_L = HED.OUTKA_CTL_NO                       " & vbNewLine _
        & "   AND OUTKA_L.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                              " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "   AND HED.OUTKA_CTL_NO = @OUTKA_CTL_NO                            " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine

    Private Const SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_SOKO_INKA As String =
          "SELECT COUNT(*) AS REC_CNT                                         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                           " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                  " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                 " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                               " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                   " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & " INNER JOIN $LM_TRN$..B_INKA_L  INKA_L                           " & vbNewLine _
        & "    ON INKA_L.NRS_BR_CD = @NRS_BR_CD                              " & vbNewLine _
        & "   AND INKA_L.INKA_NO_L = HED.OUTKA_CTL_NO                       " & vbNewLine _
        & "   AND INKA_L.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                              " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "   AND HED.OUTKA_CTL_NO = @OUTKA_CTL_NO                            " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine

    Private Const SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_SOKO_UNSO As String =
          "SELECT COUNT(*) AS REC_CNT                                         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                           " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                  " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                 " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                               " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                   " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & " INNER JOIN $LM_TRN$..F_UNSO_L  UNSO_L                             " & vbNewLine _
        & "    ON UNSO_L.UNSO_NO_L = HED.OUTKA_CTL_NO                         " & vbNewLine _
        & "   AND UNSO_L.SYS_DEL_FLG = '0'                                    " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                              " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "   AND HED.OUTKA_CTL_NO = @OUTKA_CTL_NO                            " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine

    Private Const SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_ISO As String =
          "SELECT COUNT(*) AS REC_CNT                                         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                           " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                  " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                 " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                               " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                   " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & " INNER JOIN GL_DB..C_BASIS                                         " & vbNewLine _
        & "    ON C_BASIS.JOB_NO = HED.OUTKA_CTL_NO                           " & vbNewLine _
        & "   AND C_BASIS.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & "   AND C_BASIS.BKG_STAGE_KBN <> '00099'                            " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                              " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "   AND HED.OUTKA_CTL_NO = @OUTKA_CTL_NO                            " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine
    'ADD E 2020/03/06 011377

    Private Const SQL_UPDATE_OUTKAEDI_HED_SHINCHOKU_JUCHU As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL                            " & vbNewLine _
        & "   SET SHINCHOKU_KB_JUCHU = @SHINCHOKU_KB_JUCHU_AFTER          " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "-- DEL S 2020/02/07 010901                                     " & vbNewLine _
        & "--   AND (   SHINCHOKU_KB_JUCHU = '1' -- 受注前                  " & vbNewLine _
        & "--        OR SHINCHOKU_KB_JUCHU = ''  -- 未設定(改修前に受信)    " & vbNewLine _
        & "--       )                                                       " & vbNewLine _
        & "-- DEL E 2020/02/07 010901                                     " & vbNewLine _
        & "   AND SYS_UPD_DATE = @HED_UPD_DATE                            " & vbNewLine _
        & "   AND SYS_UPD_TIME = @HED_UPD_TIME                            " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                                       " & vbNewLine

#End Region '受注作成処理SQL
    'ADD E 2019/12/12 009741

#Region "遅延送信処理"

    Private Const SQL_SELECT_SENDOUTEDI As String =
          "SELECT CREAT_DATE_TIME         " & vbNewLine _
        & "  FROM $LM_TRN$..H_SENDOUTEDI_HWL        " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE    " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME  " & vbNewLine _
        & "   AND GYO = @GYO              " & vbNewLine

    Private Const SQL_INSET_SENDOUTEDI_2 As String =
          "INSERT INTO $LM_TRN$..H_SENDOUTEDI_HWL ( " & vbNewLine _
        & "     CRT_DATE                          " & vbNewLine _
        & "    ,FILE_NAME                         " & vbNewLine _
        & "    ,GYO                               " & vbNewLine _
        & "    ,CREAT_DATE_TIME                   " & vbNewLine _
        & "    ,SHIPMENT_ACCEPT                   " & vbNewLine _
        & "    ,SHIPMENT_DECLINE                  " & vbNewLine _
        & "    ,SHIPMENT_STATUS                   " & vbNewLine _
        & "    ,SHIPMENT_ID                       " & vbNewLine _
        & "    ,SHIPMENT_REF_NUM_MISC             " & vbNewLine _
        & "    ,STOP_SEQ_NUM                      " & vbNewLine _
        & "    ,LOCATION_ID                       " & vbNewLine _
        & "    ,EVENT_STATUS                      " & vbNewLine _
        & "    ,EVENT_REASON                      " & vbNewLine _
        & "    ,REASON_DESC                       " & vbNewLine _
        & "    ,EVENT_DATE_TIME                   " & vbNewLine _
        & "    ,CITY                              " & vbNewLine _
        & "    ,COUNTRY                           " & vbNewLine _
        & "    ,ZIP_CODE                          " & vbNewLine _
        & "    ,EXPRESS_CARRIER_CODE              " & vbNewLine _
        & "    ,TRACTOR_NUMBER                    " & vbNewLine _
        & "    ,TRAILER_NUMBER                    " & vbNewLine _
        & "    ,PRO_NUMBER                        " & vbNewLine _
        & "    ,SHIPMENT_DECLINE_REASON           " & vbNewLine _
        & "    ,JISSEKI_SHORI_FLG                 " & vbNewLine _
        & "    ,SYS_ENT_DATE                      " & vbNewLine _
        & "    ,SYS_ENT_TIME                      " & vbNewLine _
        & "    ,SYS_ENT_PGID                      " & vbNewLine _
        & "    ,SYS_ENT_USER                      " & vbNewLine _
        & "    ,SYS_UPD_DATE                      " & vbNewLine _
        & "    ,SYS_UPD_TIME                      " & vbNewLine _
        & "    ,SYS_UPD_PGID                      " & vbNewLine _
        & "    ,SYS_UPD_USER                      " & vbNewLine _
        & "    ,SYS_DEL_FLG                       " & vbNewLine _
        & ") VALUES (                             " & vbNewLine _
        & "     @CRT_DATE                         " & vbNewLine _
        & "    ,@FILE_NAME                        " & vbNewLine _
        & "    ,@GYO                              " & vbNewLine _
        & "    ,@CREAT_DATE_TIME                  " & vbNewLine _
        & "    ,@SHIPMENT_ACCEPT                  " & vbNewLine _
        & "    ,@SHIPMENT_DECLINE                 " & vbNewLine _
        & "    ,@SHIPMENT_STATUS                  " & vbNewLine _
        & "    ,@SHIPMENT_ID                      " & vbNewLine _
        & "    ,@SHIPMENT_REF_NUM_MISC            " & vbNewLine _
        & "    ,@STOP_SEQ_NUM                     " & vbNewLine _
        & "    ,@LOCATION_ID                      " & vbNewLine _
        & "    ,@EVENT_STATUS                     " & vbNewLine _
        & "    ,@EVENT_REASON                     " & vbNewLine _
        & "    ,@REASON_DESC                      " & vbNewLine _
        & "    ,@EVENT_DATE_TIME                  " & vbNewLine _
        & "    ,@CITY                             " & vbNewLine _
        & "    ,@COUNTRY                          " & vbNewLine _
        & "    ,@ZIP_CODE                         " & vbNewLine _
        & "    ,@EXPRESS_CARRIER_CODE             " & vbNewLine _
        & "    ,@TRACTOR_NUMBER                   " & vbNewLine _
        & "    ,@TRAILER_NUMBER                   " & vbNewLine _
        & "    ,@PRO_NUMBER                       " & vbNewLine _
        & "    ,@SHIPMENT_DECLINE_REASON          " & vbNewLine _
        & "    ,@JISSEKI_SHORI_FLG                " & vbNewLine _
        & "    ,@SYS_ENT_DATE                     " & vbNewLine _
        & "    ,@SYS_ENT_TIME                     " & vbNewLine _
        & "    ,@SYS_ENT_PGID                     " & vbNewLine _
        & "    ,@SYS_ENT_USER                     " & vbNewLine _
        & "    ,@SYS_UPD_DATE                     " & vbNewLine _
        & "    ,@SYS_UPD_TIME                     " & vbNewLine _
        & "    ,@SYS_UPD_PGID                     " & vbNewLine _
        & "    ,@SYS_UPD_USER                     " & vbNewLine _
        & "    ,@SYS_DEL_FLG                      " & vbNewLine _
        & ")                                      " & vbNewLine

#End Region

#Region "Booked解除送信処理"

    ''' <summary>
    ''' 処理対象範囲の行の H_OUTKAEDI_HED_HWL の SYS_ENT_DATE, SYS_ENT_TIME を取得する SQL
    ''' </summary>
    Private Const SQL_SELECT_HED_SPM_SENDOUTEDI_BY_PKEY As String = "" _
        & "SELECT                            " & vbNewLine _
        & "      CRT_DATE                    " & vbNewLine _
        & "    , FILE_NAME                   " & vbNewLine _
        & "    , SYS_ENT_DATE                " & vbNewLine _
        & "    , SYS_ENT_TIME                " & vbNewLine _
        & "FROM                              " & vbNewLine _
        & "    $LM_TRN$..H_OUTKAEDI_HED_HWL  " & vbNewLine _
        & "WHERE (                           " & vbNewLine _
        & "   (    CRT_DATE = @CRT_DATE      " & vbNewLine _
        & "    AND FILE_NAME = @FILE_NAME)   " & vbNewLine _
        & ")                                 " & vbNewLine _
        & "AND SYS_DEL_FLG = '0'             " & vbNewLine

    ''' <summary>
    ''' 対象行と 同一の Load Number で、対象行より後に受信し、かつ
    ''' 応答レコードがある H_OUTKAEDI_HED_HWL の CRT_DATE, FILE_NAME を取得する SQL
    ''' </summary>
    Private Const SQL_SELECT_HED_SPM_SENDOUTEDI_BY_SHIPMENT_ID As String = "" _
        & "SELECT                                                                         " & vbNewLine _
        & "      H_OUTKAEDI_HED_HWL.CRT_DATE                                              " & vbNewLine _
        & "    , H_OUTKAEDI_HED_HWL.FILE_NAME                                             " & vbNewLine _
        & "FROM                                                                           " & vbNewLine _
        & "    $LM_TRN$..H_OUTKAEDI_HED_HWL                                               " & vbNewLine _
        & "LEFT JOIN                                                                      " & vbNewLine _
        & "    $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM                                           " & vbNewLine _
        & "        ON  H_OUTKAEDI_DTL_HWL_SPM.CRT_DATE = H_OUTKAEDI_HED_HWL.CRT_DATE      " & vbNewLine _
        & "        AND H_OUTKAEDI_DTL_HWL_SPM.FILE_NAME = H_OUTKAEDI_HED_HWL.FILE_NAME    " & vbNewLine _
        & "        AND H_OUTKAEDI_DTL_HWL_SPM.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & "LEFT JOIN                                                                      " & vbNewLine _
        & "    $LM_TRN$..H_SENDOUTEDI_HWL                                                 " & vbNewLine _
        & "        ON  H_SENDOUTEDI_HWL.CRT_DATE = H_OUTKAEDI_HED_HWL.CRT_DATE            " & vbNewLine _
        & "        AND H_SENDOUTEDI_HWL.FILE_NAME = H_OUTKAEDI_HED_HWL.FILE_NAME          " & vbNewLine _
        & "        AND H_SENDOUTEDI_HWL.GYO = @GYO                                        " & vbNewLine _
        & "        AND H_SENDOUTEDI_HWL.SYS_DEL_FLG = '0'                                 " & vbNewLine _
        & "WHERE                                                                          " & vbNewLine _
        & "NOT(    H_OUTKAEDI_HED_HWL.CRT_DATE = @CRT_DATE                                " & vbNewLine _
        & "    AND H_OUTKAEDI_HED_HWL.FILE_NAME = @FILE_NAME)                             " & vbNewLine _
        & "AND H_OUTKAEDI_HED_HWL.SYS_ENT_DATE + H_OUTKAEDI_HED_HWL.SYS_ENT_TIME >=       " & vbNewLine _
        & "       (SELECT                                                                 " & vbNewLine _
        & "            ISNULL(MAX(SYS_ENT_DATE + SYS_ENT_TIME), '00000000' + '000000000') " & vbNewLine _
        & "        FROM                                                                   " & vbNewLine _
        & "            $LM_TRN$..H_OUTKAEDI_HED_HWL                                       " & vbNewLine _
        & "        WHERE                                                                  " & vbNewLine _
        & "            CRT_DATE = @CRT_DATE                                               " & vbNewLine _
        & "        AND FILE_NAME = @FILE_NAME)                                            " & vbNewLine _
        & "AND H_OUTKAEDI_HED_HWL.SYS_DEL_FLG = '0'                                       " & vbNewLine _
        & "AND H_OUTKAEDI_DTL_HWL_SPM.CRT_DATE IS NOT NULL                                " & vbNewLine _
        & "AND H_OUTKAEDI_DTL_HWL_SPM.SHIPMENT_ID = @SHIPMENT_ID                          " & vbNewLine _
        & "AND H_SENDOUTEDI_HWL.CRT_DATE IS NOT NULL                                      " & vbNewLine

    ''' <summary>
    ''' 対象行と 同一の Load Number で、対象行より前に受信し、かつ
    ''' 応答レコードがある納入日情報と、対象行の納入日情報の取得 SQL
    ''' </summary>
    Private Const SQL_SELECT_SENDOUTEDI_OUTKAEDI_HED_DTL_STP As String = "" _
        & "SELECT                                                                         " & vbNewLine _
        & "      H_SENDOUTEDI_HWL.CRT_DATE                                                " & vbNewLine _
        & "    , H_SENDOUTEDI_HWL.FILE_NAME                                               " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.SCHEDULE_START_DATE_TIME                          " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.SCHEDULE_END_DATE_TIME                            " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.REQUEST_START_DATE_TIME                           " & vbNewLine _
        & "    , H_OUTKAEDI_HED_HWL.SYS_ENT_DATE                                          " & vbNewLine _
        & "    , H_OUTKAEDI_HED_HWL.SYS_ENT_TIME                                          " & vbNewLine _
        & "FROM                                                                           " & vbNewLine _
        & "    $LM_TRN$..H_SENDOUTEDI_HWL                                                 " & vbNewLine _
        & "LEFT JOIN                                                                      " & vbNewLine _
        & "    $LM_TRN$..H_OUTKAEDI_HED_HWL                                               " & vbNewLine _
        & "        ON  H_OUTKAEDI_HED_HWL.CRT_DATE = H_SENDOUTEDI_HWL.CRT_DATE            " & vbNewLine _
        & "        AND H_OUTKAEDI_HED_HWL.FILE_NAME = H_SENDOUTEDI_HWL.FILE_NAME          " & vbNewLine _
        & "        AND H_OUTKAEDI_HED_HWL.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "LEFT JOIN                                                                      " & vbNewLine _
        & "    $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP                                           " & vbNewLine _
        & "        ON  H_OUTKAEDI_DTL_HWL_STP.CRT_DATE = H_SENDOUTEDI_HWL.CRT_DATE        " & vbNewLine _
        & "        AND H_OUTKAEDI_DTL_HWL_STP.FILE_NAME = H_SENDOUTEDI_HWL.FILE_NAME      " & vbNewLine _
        & "        AND H_OUTKAEDI_DTL_HWL_STP.STOP_TYPE = 'D'                             " & vbNewLine _
        & "        AND H_OUTKAEDI_DTL_HWL_STP.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & "WHERE                                                                          " & vbNewLine _
        & "    H_SENDOUTEDI_HWL.SHIPMENT_ID = @SHIPMENT_ID                                " & vbNewLine _
        & "AND H_SENDOUTEDI_HWL.GYO = @GYO                                                " & vbNewLine _
        & "AND H_SENDOUTEDI_HWL.SYS_DEL_FLG = '0'                                         " & vbNewLine _
        & "AND NOT(    H_OUTKAEDI_HED_HWL.CRT_DATE = @CRT_DATE                            " & vbNewLine _
        & "        AND H_OUTKAEDI_HED_HWL.FILE_NAME = @FILE_NAME)                         " & vbNewLine _
        & "AND H_OUTKAEDI_HED_HWL.SYS_ENT_DATE + H_OUTKAEDI_HED_HWL.SYS_ENT_TIME <=       " & vbNewLine _
        & "       (SELECT                                                                 " & vbNewLine _
        & "            ISNULL(MAX(SYS_ENT_DATE + SYS_ENT_TIME), '00000000' + '000000000') " & vbNewLine _
        & "        FROM                                                                   " & vbNewLine _
        & "            $LM_TRN$..H_OUTKAEDI_HED_HWL                                       " & vbNewLine _
        & "        WHERE                                                                  " & vbNewLine _
        & "            CRT_DATE = @CRT_DATE                                               " & vbNewLine _
        & "        AND FILE_NAME = @FILE_NAME)                                            " & vbNewLine _
        & "AND H_OUTKAEDI_HED_HWL.CRT_DATE IS NOT NULL                                    " & vbNewLine _
        & "AND H_OUTKAEDI_DTL_HWL_STP.CRT_DATE IS NOT NULL                                " & vbNewLine _
        & "GROUP BY                                                                       " & vbNewLine _
        & "      H_SENDOUTEDI_HWL.CRT_DATE                                                " & vbNewLine _
        & "    , H_SENDOUTEDI_HWL.FILE_NAME                                               " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.SCHEDULE_START_DATE_TIME                          " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.SCHEDULE_END_DATE_TIME                            " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.REQUEST_START_DATE_TIME                           " & vbNewLine _
        & "    , H_OUTKAEDI_HED_HWL.SYS_ENT_DATE                                          " & vbNewLine _
        & "    , H_OUTKAEDI_HED_HWL.SYS_ENT_TIME                                          " & vbNewLine _
        & "UNION ALL                                                                      " & vbNewLine _
        & "SELECT                                                                         " & vbNewLine _
        & "      @CRT_DATE AS CRT_DATE                                                    " & vbNewLine _
        & "    , @FILE_NAME AS FILE_NAME                                                  " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.SCHEDULE_START_DATE_TIME                          " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.SCHEDULE_END_DATE_TIME                            " & vbNewLine _
        & "    , H_OUTKAEDI_DTL_HWL_STP.REQUEST_START_DATE_TIME                           " & vbNewLine _
        & "    , FORMAT(GETDATE(), 'yyyyMMdd') AS SYS_ENT_DATE                            " & vbNewLine _
        & "    , FORMAT(GETDATE(), 'HHmmssfff') AS SYS_ENT_TIME                           " & vbNewLine _
        & "FROM                                                                           " & vbNewLine _
        & "    $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP                                           " & vbNewLine _
        & "WHERE                                                                          " & vbNewLine _
        & "    H_OUTKAEDI_DTL_HWL_STP.CRT_DATE = @CRT_DATE                                " & vbNewLine _
        & "AND H_OUTKAEDI_DTL_HWL_STP.FILE_NAME = @FILE_NAME                              " & vbNewLine _
        & "AND H_OUTKAEDI_DTL_HWL_STP.STOP_TYPE = 'D'                                     " & vbNewLine _
        & "AND H_OUTKAEDI_DTL_HWL_STP.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & "ORDER BY                                                                       " & vbNewLine _
        & "      CRT_DATE                                                                 " & vbNewLine _
        & "    , SYS_ENT_DATE                                                             " & vbNewLine _
        & "    , SYS_ENT_TIME                                                             " & vbNewLine

    ''' <summary>
    ''' ハネウェルＥＤＩ送信の(注文応答990取消または注文応答214(受注OK)取消))データ存在チェック
    ''' ハネウェルＥＤＩ送信の(注文応答990または注文応答214(受注OK))データ存在チェック
    ''' </summary>
    Private Const SQL_SELECT_SENDOUTEDI_BY_PKEY As String = "" _
        & "SELECT                          " & vbNewLine _
        & "      CRT_DATE                  " & vbNewLine _
        & "    , FILE_NAME                 " & vbNewLine _
        & "    , GYO                       " & vbNewLine _
        & "    , SHIPMENT_ACCEPT           " & vbNewLine _
        & "    , SHIPMENT_DECLINE          " & vbNewLine _
        & "    , SHIPMENT_DECLINE_REASON   " & vbNewLine _
        & "    , SHIPMENT_ID               " & vbNewLine _
        & "    , SHIPMENT_REF_NUM_MISC     " & vbNewLine _
        & "    , TRACTOR_NUMBER            " & vbNewLine _
        & "FROM                            " & vbNewLine _
        & "    $LM_TRN$..H_SENDOUTEDI_HWL  " & vbNewLine _
        & "WHERE                           " & vbNewLine _
        & "    CRT_DATE = @CRT_DATE        " & vbNewLine _
        & "AND FILE_NAME = @FILE_NAME      " & vbNewLine _
        & "AND(GYO = @GYO OR GYO = @GYO_2) " & vbNewLine _
        & "AND SYS_DEL_FLG = '0'           " & vbNewLine

#End Region ' "Booked解除送信処理"

    'ADD S 2020/02/27 010901
#Region "荷主振り分け処理"

    Private Const SQL_SELECT_STP_CITY_FOR_SPECIFY_CUST As String =
          "SELECT STP.CITY                                                " & vbNewLine _
        & "      ,STP.ZIP_CODE                     --ADD 2020/03/25 011731" & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                       " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM              " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                             " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                           " & vbNewLine _
        & "   AND SPM.GYO = '1'                                           " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                          " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP              " & vbNewLine _
        & "    ON STP.CRT_DATE = HED.CRT_DATE                             " & vbNewLine _
        & "   AND STP.FILE_NAME = HED.FILE_NAME                           " & vbNewLine _
        & "   AND STP.STOP_TYPE = 'P'                                     " & vbNewLine _
        & "   AND STP.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " WHERE HED.CRT_DATE = @CRT_DATE                                " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                              " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                        " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                        " & vbNewLine _
        & "   AND HED.SYS_DEL_FLG = '0'                                   " & vbNewLine

    Private Const SQL_SELECT_CUST_CD_BY_SKU_NUMBER As String =
          "SELECT DISTINCT                                                " & vbNewLine _
        & "       MG.CUST_CD_L                                            " & vbNewLine _
        & "      ,MG.CUST_CD_M                                            " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                       " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD              " & vbNewLine _
        & "    ON CMD.CRT_DATE = HED.CRT_DATE                             " & vbNewLine _
        & "   AND CMD.FILE_NAME = HED.FILE_NAME                           " & vbNewLine _
        & "   AND CMD.SKU_NUMBER <> ''                                    " & vbNewLine _
        & "   AND CMD.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " INNER JOIN $LM_MST$..M_GOODS  MG                              " & vbNewLine _
        & "    ON MG.NRS_BR_CD = @NRS_BR_CD                               " & vbNewLine _
        & "   AND RIGHT(MG.SEARCH_KEY_2,8) = RIGHT(CMD.SKU_NUMBER,8)      " & vbNewLine _
        & "   AND (    MG.CUST_CD_L = '00630' AND MG.CUST_CD_M = '00'     " & vbNewLine _
        & "         OR MG.CUST_CD_L = '00632' AND MG.CUST_CD_M = '00'     " & vbNewLine _
        & "         OR MG.CUST_CD_L = '70630' AND MG.CUST_CD_M = '00'     " & vbNewLine _
        & "       )                                                       " & vbNewLine _
        & "   AND MG.CUST_CD_S = '00'                                     " & vbNewLine _
        & "   AND MG.CUST_CD_SS = '00'                                    " & vbNewLine _
        & "   AND MG.SYS_DEL_FLG = '0'                                    " & vbNewLine _
        & " WHERE HED.CRT_DATE = @CRT_DATE                                " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                              " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                        " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                        " & vbNewLine _
        & "   AND HED.SYS_DEL_FLG = '0'                                   " & vbNewLine

    Private Const SQL_UPDATE_OUTKAEDI_HED_CUST As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL                            " & vbNewLine _
        & "   SET CUST_CD_L = @CUST_CD_L                                  " & vbNewLine _
        & "      ,CUST_CD_M = @CUST_CD_M                                  " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "   AND SYS_UPD_DATE = @HED_UPD_DATE                            " & vbNewLine _
        & "   AND SYS_UPD_TIME = @HED_UPD_TIME                            " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                                       " & vbNewLine

#End Region '荷主振り分け処理

#Region "出荷登録処理"

    Private Const SQL_SELECT_HED_SPM_STP_FOR_INS_INOUTKA As String =
          "SELECT HED.CUST_CD_L                                       " & vbNewLine _
        & "      ,HED.CUST_CD_M                                       " & vbNewLine _
        & "      ,HED.INOUT_KB                                        " & vbNewLine _
        & "      ,HED.OUTKA_CTL_NO                                    " & vbNewLine _
        & "      ,LEFT(STP1.SCHEDULE_START_DATE_TIME,8) AS OUTKA_PLAN_DATE    " & vbNewLine _
        & "      ,LEFT(STP2.SCHEDULE_END_DATE_TIME,8) AS ARR_PLAN_DATE        " & vbNewLine _
        & "      ,STP2.LOCATION_ID                                    " & vbNewLine _
        & "      ,SPM.CON                                             " & vbNewLine _
        & "      ,LEFT(STP2.REQUEST_START_DATE_TIME,8) AS INKA_INKA_DATE      " & vbNewLine _
        & "      ,STP1.LOCATION_ID AS INKA_ORIG_CD                            " & vbNewLine _
        & "      ,ISNULL(SEND.TRACTOR_NUMBER,'') AS TRACTOR_NUMBER    " & vbNewLine _
        & "      ,SPM.BUYID                                           " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                   " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM          " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                         " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                       " & vbNewLine _
        & "   AND SPM.GYO = '1'                                       " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                      " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                               " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1         " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'                                " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2         " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'                                " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                              " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_SENDOUTEDI_HWL  SEND               " & vbNewLine _
        & "    ON SEND.CRT_DATE = HED.CRT_DATE                        " & vbNewLine _
        & "   AND SEND.FILE_NAME = HED.FILE_NAME                      " & vbNewLine _
        & "   AND SEND.GYO = '5' -- 5:注文応答(214)                   " & vbNewLine _
        & " WHERE HED.CRT_DATE = @CRT_DATE                            " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                          " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE                    " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME                    " & vbNewLine _
        & "   AND HED.SYS_DEL_FLG = '0'                               " & vbNewLine

    Private Const SQL_SELECT_CMD_FOR_INS_OUTKA As String =
          "SELECT CMD.GYO                                       " & vbNewLine _
        & "      ,RIGHT(CMD.SKU_NUMBER,8) AS SKU_NUMBER         " & vbNewLine _
        & "      ,CMD.NUMBER_PIECES                             " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED             " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM    " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                   " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
        & "   AND SPM.GYO = '1'                                 " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                         " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD    " & vbNewLine _
        & "    ON CMD.CRT_DATE = HED.CRT_DATE                   " & vbNewLine _
        & "   AND CMD.FILE_NAME = HED.FILE_NAME                 " & vbNewLine _
        & "   AND CMD.SKU_NUMBER <> ''                          " & vbNewLine _
        & "   AND CMD.SYS_DEL_FLG = '0'                         " & vbNewLine _
        & " WHERE HED.CRT_DATE = @CRT_DATE                      " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                    " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @SYS_UPD_DATE              " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @SYS_UPD_TIME              " & vbNewLine _
        & "   AND HED.SYS_DEL_FLG = '0'                         " & vbNewLine

    Private Const SQL_SELECT_M_CUST_FOR_INS_INOUTKA As String =
          "SELECT M_CUST.CUST_CD_L                                " & vbNewLine _
        & "      ,M_CUST.CUST_CD_M                                " & vbNewLine _
        & "      ,CASE WHEN M_CUST.UNSO_TEHAI_KB = ''             " & vbNewLine _
        & "            THEN '90'  --未定                          " & vbNewLine _
        & "            ELSE M_CUST.UNSO_TEHAI_KB                  " & vbNewLine _
        & "       END AS UNSO_TEHAI_KB                            " & vbNewLine _
        & "      ,M_CUST.TAX_KB                                   " & vbNewLine _
        & "      ,M_CUST.HOKAN_FREE_KIKAN                         " & vbNewLine _
        & "      ,M_CUST.DEFAULT_SOKO_CD                          " & vbNewLine _
        & "      ,CASE WHEN Z_KBN.KBN_CD IS NULL                  " & vbNewLine _
        & "            THEN '00'                                  " & vbNewLine _
        & "            ELSE '01'                                  " & vbNewLine _
        & "       END  AS WH_TAB_YN                               " & vbNewLine _
        & "      ,ISNULL(M_SOKO.SOKO_DEST_CD,'') AS SOKO_DEST_CD  " & vbNewLine _
        & "      ,ISNULL(TARIF_SET.TARIFF_BUNRUI_KB,'') AS TARIFF_BUNRUI_KB     " & vbNewLine _
        & "  FROM $LM_MST$..M_CUST                                " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..Z_KBN                            " & vbNewLine _
        & "    ON Z_KBN.KBN_GROUP_CD = 'B007'                     " & vbNewLine _
        & "   AND Z_KBN.KBN_CD = M_CUST.NRS_BR_CD                 " & vbNewLine _
        & "   AND Z_KBN.VALUE1 = 1.0                              " & vbNewLine _
        & "   AND Z_KBN.SYS_DEL_FLG = '0'                         " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_SOKO                           " & vbNewLine _
        & "    ON M_SOKO.WH_CD = M_CUST.DEFAULT_SOKO_CD           " & vbNewLine _
        & "   AND M_SOKO.SYS_DEL_FLG = '0'                        " & vbNewLine _
        & "  LEFT JOIN $LM_MST$..M_UNCHIN_TARIFF_SET  TARIF_SET   " & vbNewLine _
        & "    ON TARIF_SET.NRS_BR_CD = M_CUST.NRS_BR_CD          " & vbNewLine _
        & "   AND TARIF_SET.CUST_CD_L = M_CUST.CUST_CD_L          " & vbNewLine _
        & "   AND TARIF_SET.CUST_CD_M = M_CUST.CUST_CD_M          " & vbNewLine _
        & "   AND TARIF_SET.SET_KB = '02'  -- 荷主（入荷）        " & vbNewLine _
        & "   AND TARIF_SET.DEST_CD = ''                          " & vbNewLine _
        & "   AND TARIF_SET.SYS_DEL_FLG = '0'                     " & vbNewLine _
        & " WHERE M_CUST.NRS_BR_CD = @NRS_BR_CD                   " & vbNewLine _
        & "   AND M_CUST.CUST_CD_L = @CUST_CD_L                   " & vbNewLine _
        & "   AND M_CUST.CUST_CD_M = @CUST_CD_M                   " & vbNewLine _
        & "   AND M_CUST.CUST_CD_S = @CUST_CD_S                   " & vbNewLine _
        & "   AND M_CUST.CUST_CD_SS = @CUST_CD_SS                 " & vbNewLine

    Private Const SQL_SELECT_M_DEST_FOR_INS_INOUTKA As String =
          "SELECT DEST_CD                            " & vbNewLine _
        & "      ,AD_3                               " & vbNewLine _
        & "      ,TEL                                " & vbNewLine _
        & "      ,SP_NHS_KB                          " & vbNewLine _
        & "      ,COA_YN                             " & vbNewLine _
        & "  FROM $LM_MST$..M_DEST                   " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD             " & vbNewLine _
        & "   AND CUST_CD_L = @CUST_CD_L             " & vbNewLine _
        & "   AND DEST_CD = @DEST_CD                 " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                  " & vbNewLine

    Private Const SQL_SELECT_M_GOODS_FOR_INS_OUTKA As String =
          "SELECT GOODS_CD_NRS                    " & vbNewLine _
        & "      ,GOODS_NM_1                      " & vbNewLine _
        & "      ,UNSO_ONDO_KB                    " & vbNewLine _
        & "      ,ALCTD_KB                        " & vbNewLine _
        & "      ,NB_UT                           " & vbNewLine _
        & "      ,PKG_NB                          " & vbNewLine _
        & "      ,PKG_UT                          " & vbNewLine _
        & "      ,STD_IRIME_NB                    " & vbNewLine _
        & "      ,STD_IRIME_UT                    " & vbNewLine _
        & "      ,STD_WT_KGS                      " & vbNewLine _
        & "      ,TARE_YN                         " & vbNewLine _
        & "      ,OUTKA_ATT                       " & vbNewLine _
        & "      ,SIZE_KB                         " & vbNewLine _
        & "      ,COA_YN                          " & vbNewLine _
        & "  FROM $LM_MST$..M_GOODS               " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD          " & vbNewLine _
        & "   AND CUST_CD_L = @CUST_CD_L          " & vbNewLine _
        & "   AND CUST_CD_M = @CUST_CD_M          " & vbNewLine _
        & "   AND RIGHT(SEARCH_KEY_2,8) = @SEARCH_KEY_2    " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'               " & vbNewLine

    Private Const SQL_UPDATE_OUTKAEDI_HED_FOR_INS_INOUTKA As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL                            " & vbNewLine _
        & "   SET DEL_KB = @DEL_KB                                        " & vbNewLine _
        & "      ,SHINCHOKU_KB_JUCHU = @SHINCHOKU_KB_JUCHU                " & vbNewLine _
        & "      ,INOUT_KB = @INOUT_KB                                    " & vbNewLine _
        & "      ,OUTKA_CTL_NO = @OUTKA_CTL_NO                            " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                                    " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME                                  " & vbNewLine _
        & "   AND SYS_UPD_DATE = @HED_UPD_DATE                            " & vbNewLine _
        & "   AND SYS_UPD_TIME = @HED_UPD_TIME                            " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                                       " & vbNewLine

    Private Const SQL_INSERT_C_OUTKA_L As String =
          "INSERT INTO $LM_TRN$..C_OUTKA_L(    " & vbNewLine _
        & "     NRS_BR_CD                      " & vbNewLine _
        & "    ,OUTKA_NO_L                     " & vbNewLine _
        & "    ,FURI_NO                        " & vbNewLine _
        & "    ,OUTKA_KB                       " & vbNewLine _
        & "    ,SYUBETU_KB                     " & vbNewLine _
        & "    ,OUTKA_STATE_KB                 " & vbNewLine _
        & "    ,OUTKAHOKOKU_YN                 " & vbNewLine _
        & "    ,PICK_KB                        " & vbNewLine _
        & "    ,DENP_NO                        " & vbNewLine _
        & "    ,ARR_KANRYO_INFO                " & vbNewLine _
        & "    ,WH_CD                          " & vbNewLine _
        & "    ,OUTKA_PLAN_DATE                " & vbNewLine _
        & "    ,OUTKO_DATE                     " & vbNewLine _
        & "    ,ARR_PLAN_DATE                  " & vbNewLine _
        & "    ,ARR_PLAN_TIME                  " & vbNewLine _
        & "    ,HOKOKU_DATE                    " & vbNewLine _
        & "    ,TOUKI_HOKAN_YN                 " & vbNewLine _
        & "    ,END_DATE                       " & vbNewLine _
        & "    ,CUST_CD_L                      " & vbNewLine _
        & "    ,CUST_CD_M                      " & vbNewLine _
        & "    ,SHIP_CD_L                      " & vbNewLine _
        & "    ,SHIP_CD_M                      " & vbNewLine _
        & "    ,DEST_CD                        " & vbNewLine _
        & "    ,DEST_AD_3                      " & vbNewLine _
        & "    ,DEST_TEL                       " & vbNewLine _
        & "    ,NHS_REMARK                     " & vbNewLine _
        & "    ,SP_NHS_KB                      " & vbNewLine _
        & "    ,COA_YN                         " & vbNewLine _
        & "    ,CUST_ORD_NO                    " & vbNewLine _
        & "    ,BUYER_ORD_NO                   " & vbNewLine _
        & "    ,REMARK                         " & vbNewLine _
        & "    ,OUTKA_PKG_NB                   " & vbNewLine _
        & "    ,DENP_YN                        " & vbNewLine _
        & "    ,PC_KB                          " & vbNewLine _
        & "    ,NIYAKU_YN                      " & vbNewLine _
        & "    ,DEST_KB                        " & vbNewLine _
        & "    ,DEST_NM                        " & vbNewLine _
        & "    ,DEST_AD_1                      " & vbNewLine _
        & "    ,DEST_AD_2                      " & vbNewLine _
        & "    ,ALL_PRINT_FLAG                 " & vbNewLine _
        & "    ,NIHUDA_FLAG                    " & vbNewLine _
        & "    ,NHS_FLAG                       " & vbNewLine _
        & "    ,DENP_FLAG                      " & vbNewLine _
        & "    ,COA_FLAG                       " & vbNewLine _
        & "    ,HOKOKU_FLAG                    " & vbNewLine _
        & "    ,MATOME_PICK_FLAG               " & vbNewLine _
        & "    ,MATOME_PRINT_DATE              " & vbNewLine _
        & "    ,MATOME_PRINT_TIME              " & vbNewLine _
        & "    ,LAST_PRINT_DATE                " & vbNewLine _
        & "    ,LAST_PRINT_TIME                " & vbNewLine _
        & "    ,SASZ_USER                      " & vbNewLine _
        & "    ,OUTKO_USER                     " & vbNewLine _
        & "    ,KEN_USER                       " & vbNewLine _
        & "    ,OUTKA_USER                     " & vbNewLine _
        & "    ,HOU_USER                       " & vbNewLine _
        & "    ,ORDER_TYPE                     " & vbNewLine _
        & "    ,WH_KENPIN_WK_STATUS            " & vbNewLine _
        & "    ,WH_TAB_STATUS                  " & vbNewLine _
        & "    ,WH_TAB_YN                      " & vbNewLine _
        & "    ,URGENT_YN                      " & vbNewLine _
        & "    ,WH_SIJI_REMARK                 " & vbNewLine _
        & "    ,WH_TAB_NO_SIJI_FLG             " & vbNewLine _
        & "    ,WH_TAB_HOKOKU_YN               " & vbNewLine _
        & "    ,WH_TAB_HOKOKU                  " & vbNewLine _
        & "    ,SYS_ENT_DATE                   " & vbNewLine _
        & "    ,SYS_ENT_TIME                   " & vbNewLine _
        & "    ,SYS_ENT_PGID                   " & vbNewLine _
        & "    ,SYS_ENT_USER                   " & vbNewLine _
        & "    ,SYS_UPD_DATE                   " & vbNewLine _
        & "    ,SYS_UPD_TIME                   " & vbNewLine _
        & "    ,SYS_UPD_PGID                   " & vbNewLine _
        & "    ,SYS_UPD_USER                   " & vbNewLine _
        & "    ,SYS_DEL_FLG                    " & vbNewLine _
        & ") VALUES (                          " & vbNewLine _
        & "     @NRS_BR_CD                     " & vbNewLine _
        & "    ,@OUTKA_NO_L                    " & vbNewLine _
        & "    ,@FURI_NO                       " & vbNewLine _
        & "    ,@OUTKA_KB                      " & vbNewLine _
        & "    ,@SYUBETU_KB                    " & vbNewLine _
        & "    ,@OUTKA_STATE_KB                " & vbNewLine _
        & "    ,@OUTKAHOKOKU_YN                " & vbNewLine _
        & "    ,@PICK_KB                       " & vbNewLine _
        & "    ,@DENP_NO                       " & vbNewLine _
        & "    ,@ARR_KANRYO_INFO               " & vbNewLine _
        & "    ,@WH_CD                         " & vbNewLine _
        & "    ,@OUTKA_PLAN_DATE               " & vbNewLine _
        & "    ,@OUTKO_DATE                    " & vbNewLine _
        & "    ,@ARR_PLAN_DATE                 " & vbNewLine _
        & "    ,@ARR_PLAN_TIME                 " & vbNewLine _
        & "    ,@HOKOKU_DATE                   " & vbNewLine _
        & "    ,@TOUKI_HOKAN_YN                " & vbNewLine _
        & "    ,@END_DATE                      " & vbNewLine _
        & "    ,@CUST_CD_L                     " & vbNewLine _
        & "    ,@CUST_CD_M                     " & vbNewLine _
        & "    ,@SHIP_CD_L                     " & vbNewLine _
        & "    ,@SHIP_CD_M                     " & vbNewLine _
        & "    ,@DEST_CD                       " & vbNewLine _
        & "    ,@DEST_AD_3                     " & vbNewLine _
        & "    ,@DEST_TEL                      " & vbNewLine _
        & "    ,@NHS_REMARK                    " & vbNewLine _
        & "    ,@SP_NHS_KB                     " & vbNewLine _
        & "    ,@COA_YN                        " & vbNewLine _
        & "    ,@CUST_ORD_NO                   " & vbNewLine _
        & "    ,@BUYER_ORD_NO                  " & vbNewLine _
        & "    ,@REMARK                        " & vbNewLine _
        & "    ,@OUTKA_PKG_NB                  " & vbNewLine _
        & "    ,@DENP_YN                       " & vbNewLine _
        & "    ,@PC_KB                         " & vbNewLine _
        & "    ,@NIYAKU_YN                     " & vbNewLine _
        & "    ,@DEST_KB                       " & vbNewLine _
        & "    ,@DEST_NM                       " & vbNewLine _
        & "    ,@DEST_AD_1                     " & vbNewLine _
        & "    ,@DEST_AD_2                     " & vbNewLine _
        & "    ,@ALL_PRINT_FLAG                " & vbNewLine _
        & "    ,@NIHUDA_FLAG                   " & vbNewLine _
        & "    ,@NHS_FLAG                      " & vbNewLine _
        & "    ,@DENP_FLAG                     " & vbNewLine _
        & "    ,@COA_FLAG                      " & vbNewLine _
        & "    ,@HOKOKU_FLAG                   " & vbNewLine _
        & "    ,@MATOME_PICK_FLAG              " & vbNewLine _
        & "    ,@MATOME_PRINT_DATE             " & vbNewLine _
        & "    ,@MATOME_PRINT_TIME             " & vbNewLine _
        & "    ,@LAST_PRINT_DATE               " & vbNewLine _
        & "    ,@LAST_PRINT_TIME               " & vbNewLine _
        & "    ,@SASZ_USER                     " & vbNewLine _
        & "    ,@OUTKO_USER                    " & vbNewLine _
        & "    ,@KEN_USER                      " & vbNewLine _
        & "    ,@OUTKA_USER                    " & vbNewLine _
        & "    ,@HOU_USER                      " & vbNewLine _
        & "    ,@ORDER_TYPE                    " & vbNewLine _
        & "    ,@WH_KENPIN_WK_STATUS           " & vbNewLine _
        & "    ,@WH_TAB_STATUS                 " & vbNewLine _
        & "    ,@WH_TAB_YN                     " & vbNewLine _
        & "    ,@URGENT_YN                     " & vbNewLine _
        & "    ,@WH_SIJI_REMARK                " & vbNewLine _
        & "    ,@WH_TAB_NO_SIJI_FLG            " & vbNewLine _
        & "    ,@WH_TAB_HOKOKU_YN              " & vbNewLine _
        & "    ,@WH_TAB_HOKOKU                 " & vbNewLine _
        & "    ,@SYS_ENT_DATE                  " & vbNewLine _
        & "    ,@SYS_ENT_TIME                  " & vbNewLine _
        & "    ,@SYS_ENT_PGID                  " & vbNewLine _
        & "    ,@SYS_ENT_USER                  " & vbNewLine _
        & "    ,@SYS_UPD_DATE                  " & vbNewLine _
        & "    ,@SYS_UPD_TIME                  " & vbNewLine _
        & "    ,@SYS_UPD_PGID                  " & vbNewLine _
        & "    ,@SYS_UPD_USER                  " & vbNewLine _
        & "    ,@SYS_DEL_FLG                   " & vbNewLine _
        & ");                                  " & vbNewLine

    Private Const SQL_INSERT_C_OUTKA_M As String =
          "INSERT INTO $LM_TRN$..C_OUTKA_M(    " & vbNewLine _
        & "     NRS_BR_CD                      " & vbNewLine _
        & "    ,OUTKA_NO_L                     " & vbNewLine _
        & "    ,OUTKA_NO_M                     " & vbNewLine _
        & "    ,EDI_SET_NO                     " & vbNewLine _
        & "    ,COA_YN                         " & vbNewLine _
        & "    ,CUST_ORD_NO_DTL                " & vbNewLine _
        & "    ,BUYER_ORD_NO_DTL               " & vbNewLine _
        & "    ,GOODS_CD_NRS                   " & vbNewLine _
        & "    ,RSV_NO                         " & vbNewLine _
        & "    ,LOT_NO                         " & vbNewLine _
        & "    ,SERIAL_NO                      " & vbNewLine _
        & "    ,ALCTD_KB                       " & vbNewLine _
        & "    ,OUTKA_PKG_NB                   " & vbNewLine _
        & "    ,OUTKA_HASU                     " & vbNewLine _
        & "    ,OUTKA_QT                       " & vbNewLine _
        & "    ,OUTKA_TTL_NB                   " & vbNewLine _
        & "    ,OUTKA_TTL_QT                   " & vbNewLine _
        & "    ,ALCTD_NB                       " & vbNewLine _
        & "    ,ALCTD_QT                       " & vbNewLine _
        & "    ,BACKLOG_NB                     " & vbNewLine _
        & "    ,BACKLOG_QT                     " & vbNewLine _
        & "    ,UNSO_ONDO_KB                   " & vbNewLine _
        & "    ,IRIME                          " & vbNewLine _
        & "    ,IRIME_UT                       " & vbNewLine _
        & "    ,OUTKA_M_PKG_NB                 " & vbNewLine _
        & "    ,REMARK                         " & vbNewLine _
        & "    ,SIZE_KB                        " & vbNewLine _
        & "    ,ZAIKO_KB                       " & vbNewLine _
        & "    ,SOURCE_CD                      " & vbNewLine _
        & "    ,YELLOW_CARD                    " & vbNewLine _
        & "    ,GOODS_CD_NRS_FROM              " & vbNewLine _
        & "    ,PRINT_SORT                     " & vbNewLine _
        & "    ,SYS_ENT_DATE                   " & vbNewLine _
        & "    ,SYS_ENT_TIME                   " & vbNewLine _
        & "    ,SYS_ENT_PGID                   " & vbNewLine _
        & "    ,SYS_ENT_USER                   " & vbNewLine _
        & "    ,SYS_UPD_DATE                   " & vbNewLine _
        & "    ,SYS_UPD_TIME                   " & vbNewLine _
        & "    ,SYS_UPD_PGID                   " & vbNewLine _
        & "    ,SYS_UPD_USER                   " & vbNewLine _
        & "    ,SYS_DEL_FLG                    " & vbNewLine _
        & ") VALUES (                          " & vbNewLine _
        & "     @NRS_BR_CD                     " & vbNewLine _
        & "    ,@OUTKA_NO_L                    " & vbNewLine _
        & "    ,@OUTKA_NO_M                    " & vbNewLine _
        & "    ,@EDI_SET_NO                    " & vbNewLine _
        & "    ,@COA_YN                        " & vbNewLine _
        & "    ,@CUST_ORD_NO_DTL               " & vbNewLine _
        & "    ,@BUYER_ORD_NO_DTL              " & vbNewLine _
        & "    ,@GOODS_CD_NRS                  " & vbNewLine _
        & "    ,@RSV_NO                        " & vbNewLine _
        & "    ,@LOT_NO                        " & vbNewLine _
        & "    ,@SERIAL_NO                     " & vbNewLine _
        & "    ,@ALCTD_KB                      " & vbNewLine _
        & "    ,@OUTKA_PKG_NB                  " & vbNewLine _
        & "    ,@OUTKA_HASU                    " & vbNewLine _
        & "    ,@OUTKA_QT                      " & vbNewLine _
        & "    ,@OUTKA_TTL_NB                  " & vbNewLine _
        & "    ,@OUTKA_TTL_QT                  " & vbNewLine _
        & "    ,@ALCTD_NB                      " & vbNewLine _
        & "    ,@ALCTD_QT                      " & vbNewLine _
        & "    ,@BACKLOG_NB                    " & vbNewLine _
        & "    ,@BACKLOG_QT                    " & vbNewLine _
        & "    ,@UNSO_ONDO_KB                  " & vbNewLine _
        & "    ,@IRIME                         " & vbNewLine _
        & "    ,@IRIME_UT                      " & vbNewLine _
        & "    ,@OUTKA_M_PKG_NB                " & vbNewLine _
        & "    ,@REMARK                        " & vbNewLine _
        & "    ,@SIZE_KB                       " & vbNewLine _
        & "    ,@ZAIKO_KB                      " & vbNewLine _
        & "    ,@SOURCE_CD                     " & vbNewLine _
        & "    ,@YELLOW_CARD                   " & vbNewLine _
        & "    ,@GOODS_CD_NRS_FROM             " & vbNewLine _
        & "    ,@PRINT_SORT                    " & vbNewLine _
        & "    ,@SYS_ENT_DATE                  " & vbNewLine _
        & "    ,@SYS_ENT_TIME                  " & vbNewLine _
        & "    ,@SYS_ENT_PGID                  " & vbNewLine _
        & "    ,@SYS_ENT_USER                  " & vbNewLine _
        & "    ,@SYS_UPD_DATE                  " & vbNewLine _
        & "    ,@SYS_UPD_TIME                  " & vbNewLine _
        & "    ,@SYS_UPD_PGID                  " & vbNewLine _
        & "    ,@SYS_UPD_USER                  " & vbNewLine _
        & "    ,@SYS_DEL_FLG                   " & vbNewLine _
        & ");                                  " & vbNewLine

#End Region '出荷登録処理
    'ADD E 2020/02/27 010901

#Region "入荷登録処理"

    Private Const SQL_INSERT_B_INKA_L As String =
          "INSERT INTO $LM_TRN$..B_INKA_L(    " & vbNewLine _
        & "     NRS_BR_CD                     " & vbNewLine _
        & "    ,INKA_NO_L                     " & vbNewLine _
        & "    ,FURI_NO                       " & vbNewLine _
        & "    ,INKA_TP                       " & vbNewLine _
        & "    ,INKA_KB                       " & vbNewLine _
        & "    ,INKA_STATE_KB                 " & vbNewLine _
        & "    ,INKA_DATE                     " & vbNewLine _
        & "    ,STORAGE_DUE_DATE              " & vbNewLine _
        & "    ,WH_CD                         " & vbNewLine _
        & "    ,CUST_CD_L                     " & vbNewLine _
        & "    ,CUST_CD_M                     " & vbNewLine _
        & "    ,INKA_PLAN_QT                  " & vbNewLine _
        & "    ,INKA_PLAN_QT_UT               " & vbNewLine _
        & "    ,INKA_TTL_NB                   " & vbNewLine _
        & "    ,BUYER_ORD_NO_L                " & vbNewLine _
        & "    ,OUTKA_FROM_ORD_NO_L           " & vbNewLine _
        & "    ,SEIQTO_CD                     " & vbNewLine _
        & "    ,TOUKI_HOKAN_YN                " & vbNewLine _
        & "    ,HOKAN_YN                      " & vbNewLine _
        & "    ,HOKAN_FREE_KIKAN              " & vbNewLine _
        & "    ,HOKAN_STR_DATE                " & vbNewLine _
        & "    ,NIYAKU_YN                     " & vbNewLine _
        & "    ,TAX_KB                        " & vbNewLine _
        & "    ,REMARK                        " & vbNewLine _
        & "    ,REMARK_OUT                    " & vbNewLine _
        & "    ,CHECKLIST_PRT_DATE            " & vbNewLine _
        & "    ,CHECKLIST_PRT_USER            " & vbNewLine _
        & "    ,UKETSUKELIST_PRT_DATE         " & vbNewLine _
        & "    ,UKETSUKELIST_PRT_USER         " & vbNewLine _
        & "    ,UKETSUKE_DATE                 " & vbNewLine _
        & "    ,UKETSUKE_USER                 " & vbNewLine _
        & "    ,KEN_DATE                      " & vbNewLine _
        & "    ,KEN_USER                      " & vbNewLine _
        & "    ,INKO_DATE                     " & vbNewLine _
        & "    ,INKO_USER                     " & vbNewLine _
        & "    ,HOUKOKUSYO_PR_DATE            " & vbNewLine _
        & "    ,HOUKOKUSYO_PR_USER            " & vbNewLine _
        & "    ,UNCHIN_TP                     " & vbNewLine _
        & "    ,UNCHIN_KB                     " & vbNewLine _
        & "    ,WH_KENPIN_WK_STATUS           " & vbNewLine _
        & "    ,WH_TAB_STATUS                 " & vbNewLine _
        & "    ,WH_TAB_YN                     " & vbNewLine _
        & "    ,WH_TAB_IMP_YN                 " & vbNewLine _
        & "    ,STOP_ALLOC                    " & vbNewLine _
        & "    ,WH_TAB_NO_SIJI_FLG            " & vbNewLine _
        & "    ,SYS_ENT_DATE                  " & vbNewLine _
        & "    ,SYS_ENT_TIME                  " & vbNewLine _
        & "    ,SYS_ENT_PGID                  " & vbNewLine _
        & "    ,SYS_ENT_USER                  " & vbNewLine _
        & "    ,SYS_UPD_DATE                  " & vbNewLine _
        & "    ,SYS_UPD_TIME                  " & vbNewLine _
        & "    ,SYS_UPD_PGID                  " & vbNewLine _
        & "    ,SYS_UPD_USER                  " & vbNewLine _
        & "    ,SYS_DEL_FLG                   " & vbNewLine _
        & ") VALUES (                         " & vbNewLine _
        & "     @NRS_BR_CD                    " & vbNewLine _
        & "    ,@INKA_NO_L                    " & vbNewLine _
        & "    ,@FURI_NO                      " & vbNewLine _
        & "    ,@INKA_TP                      " & vbNewLine _
        & "    ,@INKA_KB                      " & vbNewLine _
        & "    ,@INKA_STATE_KB                " & vbNewLine _
        & "    ,@INKA_DATE                    " & vbNewLine _
        & "    ,@STORAGE_DUE_DATE             " & vbNewLine _
        & "    ,@WH_CD                        " & vbNewLine _
        & "    ,@CUST_CD_L                    " & vbNewLine _
        & "    ,@CUST_CD_M                    " & vbNewLine _
        & "    ,@INKA_PLAN_QT                 " & vbNewLine _
        & "    ,@INKA_PLAN_QT_UT              " & vbNewLine _
        & "    ,@INKA_TTL_NB                  " & vbNewLine _
        & "    ,@BUYER_ORD_NO_L               " & vbNewLine _
        & "    ,@OUTKA_FROM_ORD_NO_L          " & vbNewLine _
        & "    ,@SEIQTO_CD                    " & vbNewLine _
        & "    ,@TOUKI_HOKAN_YN               " & vbNewLine _
        & "    ,@HOKAN_YN                     " & vbNewLine _
        & "    ,@HOKAN_FREE_KIKAN             " & vbNewLine _
        & "    ,@HOKAN_STR_DATE               " & vbNewLine _
        & "    ,@NIYAKU_YN                    " & vbNewLine _
        & "    ,@TAX_KB                       " & vbNewLine _
        & "    ,@REMARK                       " & vbNewLine _
        & "    ,@REMARK_OUT                   " & vbNewLine _
        & "    ,@CHECKLIST_PRT_DATE           " & vbNewLine _
        & "    ,@CHECKLIST_PRT_USER           " & vbNewLine _
        & "    ,@UKETSUKELIST_PRT_DATE        " & vbNewLine _
        & "    ,@UKETSUKELIST_PRT_USER        " & vbNewLine _
        & "    ,@UKETSUKE_DATE                " & vbNewLine _
        & "    ,@UKETSUKE_USER                " & vbNewLine _
        & "    ,@KEN_DATE                     " & vbNewLine _
        & "    ,@KEN_USER                     " & vbNewLine _
        & "    ,@INKO_DATE                    " & vbNewLine _
        & "    ,@INKO_USER                    " & vbNewLine _
        & "    ,@HOUKOKUSYO_PR_DATE           " & vbNewLine _
        & "    ,@HOUKOKUSYO_PR_USER           " & vbNewLine _
        & "    ,@UNCHIN_TP                    " & vbNewLine _
        & "    ,@UNCHIN_KB                    " & vbNewLine _
        & "    ,@WH_KENPIN_WK_STATUS          " & vbNewLine _
        & "    ,@WH_TAB_STATUS                " & vbNewLine _
        & "    ,@WH_TAB_YN                    " & vbNewLine _
        & "    ,@WH_TAB_IMP_YN                " & vbNewLine _
        & "    ,@STOP_ALLOC                   " & vbNewLine _
        & "    ,@WH_TAB_NO_SIJI_FLG           " & vbNewLine _
        & "    ,@SYS_ENT_DATE                 " & vbNewLine _
        & "    ,@SYS_ENT_TIME                 " & vbNewLine _
        & "    ,@SYS_ENT_PGID                 " & vbNewLine _
        & "    ,@SYS_ENT_USER                 " & vbNewLine _
        & "    ,@SYS_UPD_DATE                 " & vbNewLine _
        & "    ,@SYS_UPD_TIME                 " & vbNewLine _
        & "    ,@SYS_UPD_PGID                 " & vbNewLine _
        & "    ,@SYS_UPD_USER                 " & vbNewLine _
        & "    ,@SYS_DEL_FLG                  " & vbNewLine _
        & ")                                  " & vbNewLine

    Private Const SQL_INSERT_F_UNSO_L As String =
          "INSERT INTO $LM_TRN$..F_UNSO_L(    " & vbNewLine _
        & "     NRS_BR_CD                     " & vbNewLine _
        & "    ,UNSO_NO_L                     " & vbNewLine _
        & "    ,YUSO_BR_CD                    " & vbNewLine _
        & "    ,INOUTKA_NO_L                  " & vbNewLine _
        & "    ,TRIP_NO                       " & vbNewLine _
        & "    ,UNSO_CD                       " & vbNewLine _
        & "    ,UNSO_BR_CD                    " & vbNewLine _
        & "    ,BIN_KB                        " & vbNewLine _
        & "    ,JIYU_KB                       " & vbNewLine _
        & "    ,DENP_NO                       " & vbNewLine _
        & "    ,AUTO_DENP_KBN                 " & vbNewLine _
        & "    ,AUTO_DENP_NO                  " & vbNewLine _
        & "    ,OUTKA_PLAN_DATE               " & vbNewLine _
        & "    ,OUTKA_PLAN_TIME               " & vbNewLine _
        & "    ,ARR_PLAN_DATE                 " & vbNewLine _
        & "    ,ARR_PLAN_TIME                 " & vbNewLine _
        & "    ,ARR_ACT_TIME                  " & vbNewLine _
        & "    ,CUST_CD_L                     " & vbNewLine _
        & "    ,CUST_CD_M                     " & vbNewLine _
        & "    ,CUST_REF_NO                   " & vbNewLine _
        & "    ,SHIP_CD                       " & vbNewLine _
        & "    ,ORIG_CD                       " & vbNewLine _
        & "    ,DEST_CD                       " & vbNewLine _
        & "    ,UNSO_PKG_NB                   " & vbNewLine _
        & "    ,NB_UT                         " & vbNewLine _
        & "    ,UNSO_WT                       " & vbNewLine _
        & "    ,UNSO_ONDO_KB                  " & vbNewLine _
        & "    ,PC_KB                         " & vbNewLine _
        & "    ,TARIFF_BUNRUI_KB              " & vbNewLine _
        & "    ,VCLE_KB                       " & vbNewLine _
        & "    ,MOTO_DATA_KB                  " & vbNewLine _
        & "    ,TAX_KB                        " & vbNewLine _
        & "    ,REMARK                        " & vbNewLine _
        & "    ,SEIQ_TARIFF_CD                " & vbNewLine _
        & "    ,SEIQ_ETARIFF_CD               " & vbNewLine _
        & "    ,AD_3                          " & vbNewLine _
        & "    ,UNSO_TEHAI_KB                 " & vbNewLine _
        & "    ,BUY_CHU_NO                    " & vbNewLine _
        & "    ,AREA_CD                       " & vbNewLine _
        & "    ,TYUKEI_HAISO_FLG              " & vbNewLine _
        & "    ,SYUKA_TYUKEI_CD               " & vbNewLine _
        & "    ,HAIKA_TYUKEI_CD               " & vbNewLine _
        & "    ,TRIP_NO_SYUKA                 " & vbNewLine _
        & "    ,TRIP_NO_TYUKEI                " & vbNewLine _
        & "    ,TRIP_NO_HAIKA                 " & vbNewLine _
        & "    ,SHIHARAI_TARIFF_CD            " & vbNewLine _
        & "    ,SHIHARAI_ETARIFF_CD           " & vbNewLine _
        & "    ,MAIN_DELI_KB                  " & vbNewLine _
        & "    ,NHS_REMARK                    " & vbNewLine _
        & "    ,SYS_ENT_DATE                  " & vbNewLine _
        & "    ,SYS_ENT_TIME                  " & vbNewLine _
        & "    ,SYS_ENT_PGID                  " & vbNewLine _
        & "    ,SYS_ENT_USER                  " & vbNewLine _
        & "    ,SYS_UPD_DATE                  " & vbNewLine _
        & "    ,SYS_UPD_TIME                  " & vbNewLine _
        & "    ,SYS_UPD_PGID                  " & vbNewLine _
        & "    ,SYS_UPD_USER                  " & vbNewLine _
        & "    ,SYS_DEL_FLG                   " & vbNewLine _
        & ") VALUES (                         " & vbNewLine _
        & "     @NRS_BR_CD                    " & vbNewLine _
        & "    ,@UNSO_NO_L                    " & vbNewLine _
        & "    ,@YUSO_BR_CD                   " & vbNewLine _
        & "    ,@INOUTKA_NO_L                 " & vbNewLine _
        & "    ,@TRIP_NO                      " & vbNewLine _
        & "    ,@UNSO_CD                      " & vbNewLine _
        & "    ,@UNSO_BR_CD                   " & vbNewLine _
        & "    ,@BIN_KB                       " & vbNewLine _
        & "    ,@JIYU_KB                      " & vbNewLine _
        & "    ,@DENP_NO                      " & vbNewLine _
        & "    ,@AUTO_DENP_KBN                " & vbNewLine _
        & "    ,@AUTO_DENP_NO                 " & vbNewLine _
        & "    ,@OUTKA_PLAN_DATE              " & vbNewLine _
        & "    ,@OUTKA_PLAN_TIME              " & vbNewLine _
        & "    ,@ARR_PLAN_DATE                " & vbNewLine _
        & "    ,@ARR_PLAN_TIME                " & vbNewLine _
        & "    ,@ARR_ACT_TIME                 " & vbNewLine _
        & "    ,@CUST_CD_L                    " & vbNewLine _
        & "    ,@CUST_CD_M                    " & vbNewLine _
        & "    ,@CUST_REF_NO                  " & vbNewLine _
        & "    ,@SHIP_CD                      " & vbNewLine _
        & "    ,@ORIG_CD                      " & vbNewLine _
        & "    ,@DEST_CD                      " & vbNewLine _
        & "    ,@UNSO_PKG_NB                  " & vbNewLine _
        & "    ,@NB_UT                        " & vbNewLine _
        & "    ,@UNSO_WT                      " & vbNewLine _
        & "    ,@UNSO_ONDO_KB                 " & vbNewLine _
        & "    ,@PC_KB                        " & vbNewLine _
        & "    ,@TARIFF_BUNRUI_KB             " & vbNewLine _
        & "    ,@VCLE_KB                      " & vbNewLine _
        & "    ,@MOTO_DATA_KB                 " & vbNewLine _
        & "    ,@TAX_KB                       " & vbNewLine _
        & "    ,@REMARK                       " & vbNewLine _
        & "    ,@SEIQ_TARIFF_CD               " & vbNewLine _
        & "    ,@SEIQ_ETARIFF_CD              " & vbNewLine _
        & "    ,@AD_3                         " & vbNewLine _
        & "    ,@UNSO_TEHAI_KB                " & vbNewLine _
        & "    ,@BUY_CHU_NO                   " & vbNewLine _
        & "    ,@AREA_CD                      " & vbNewLine _
        & "    ,@TYUKEI_HAISO_FLG             " & vbNewLine _
        & "    ,@SYUKA_TYUKEI_CD              " & vbNewLine _
        & "    ,@HAIKA_TYUKEI_CD              " & vbNewLine _
        & "    ,@TRIP_NO_SYUKA                " & vbNewLine _
        & "    ,@TRIP_NO_TYUKEI               " & vbNewLine _
        & "    ,@TRIP_NO_HAIKA                " & vbNewLine _
        & "    ,@SHIHARAI_TARIFF_CD           " & vbNewLine _
        & "    ,@SHIHARAI_ETARIFF_CD          " & vbNewLine _
        & "    ,@MAIN_DELI_KB                 " & vbNewLine _
        & "    ,@NHS_REMARK                   " & vbNewLine _
        & "    ,@SYS_ENT_DATE                 " & vbNewLine _
        & "    ,@SYS_ENT_TIME                 " & vbNewLine _
        & "    ,@SYS_ENT_PGID                 " & vbNewLine _
        & "    ,@SYS_ENT_USER                 " & vbNewLine _
        & "    ,@SYS_UPD_DATE                 " & vbNewLine _
        & "    ,@SYS_UPD_TIME                 " & vbNewLine _
        & "    ,@SYS_UPD_PGID                 " & vbNewLine _
        & "    ,@SYS_UPD_USER                 " & vbNewLine _
        & "    ,@SYS_DEL_FLG                  " & vbNewLine _
        & ")                                  " & vbNewLine

#End Region '入荷登録処理

#Region "運送登録処理"

    ''' <summary>
    ''' 運送Lの初期値取得SQL
    ''' </summary>
    ''' <remarks>LMF020DACのSQL_SELECT_L0を移植</remarks>
    Private Const SQL_SELECT_UNSO_L_SOURCE As String =
          "SELECT                                                                                                   " & vbNewLine _
        & " F02_01.NRS_BR_CD                                                 AS NRS_BR_CD                           " & vbNewLine _
        & ",F02_01.NRS_BR_CD                                                 AS YUSO_BR_CD                          " & vbNewLine _
        & ",''                                                               AS UNSO_NO_L                           " & vbNewLine _
        & ",''                                                               AS INOUTKA_NO_L                        " & vbNewLine _
        & ",'40'                                                             AS MOTO_DATA_KB                        " & vbNewLine _
        & ",'01'                                                             AS JIYU_KB                             " & vbNewLine _
        & ",'01'                                                             AS PC_KB                               " & vbNewLine _
        & ",M07_01.TAX_KB                                                    AS TAX_KB                              " & vbNewLine _
        & ",''                                                               AS TRIP_NO                             " & vbNewLine _
        & ",'10'                                                             AS UNSO_TEHAI_KB                       " & vbNewLine _
        & ",'01'                                                             AS BIN_KB                              " & vbNewLine _
        & ",F02_01.TARIFF_BUNRUI_KB                                          AS TARIFF_BUNRUI_KB                    " & vbNewLine _
        & ",''                                                               AS VCLE_KB                             " & vbNewLine _
        & ",''                                                               AS UNSO_CD                             " & vbNewLine _
        & ",''                                                               AS UNSO_BR_CD                          " & vbNewLine _
        & ",''                                                               AS UNSO_NM                             " & vbNewLine _
        & ",''                                                               AS UNSO_BR_NM                          " & vbNewLine _
        & ",'00'                                                             AS TARE_YN                             " & vbNewLine _
        & ",''                                                               AS DENP_NO                             " & vbNewLine _
        & "--(2015.09.18)要望番号2408 追加START                                                                     " & vbNewLine _
        & ",''                                                               AS AUTO_DENP_KBN                       " & vbNewLine _
        & ",''                                                               AS AUTO_DENP_NO                        " & vbNewLine _
        & "--(2015.09.18)要望番号2408 追加START                                                                     " & vbNewLine _
        & ",F02_01.CUST_CD_L                                                 AS CUST_CD_L                           " & vbNewLine _
        & ",F02_01.CUST_CD_M                                                 AS CUST_CD_M                           " & vbNewLine _
        & ",M07_01.CUST_NM_L                                                 AS CUST_NM_L                           " & vbNewLine _
        & ",M07_01.CUST_NM_M                                                 AS CUST_NM_M                           " & vbNewLine _
        & ",''                                                               AS CUST_REF_NO                         " & vbNewLine _
        & ",''                                                               AS SHIP_CD                             " & vbNewLine _
        & ",''                                                               AS SHIP_NM                             " & vbNewLine _
        & ",''                                                               AS BUY_CHU_NO                          " & vbNewLine _
        & ",F02_01.SEIQ_TARIFF_CD                                            AS SEIQ_TARIFF_CD                      " & vbNewLine _
        & ",CASE WHEN F02_01.TARIFF_BUNRUI_KB = '40'                                                                " & vbNewLine _
        & "      THEN M44_01.EXTC_TARIFF_REM                                                                        " & vbNewLine _
        & "      ELSE M47_01.UNCHIN_TARIFF_REM                                                                      " & vbNewLine _
        & " END                                                              AS TARIFF_REM                          " & vbNewLine _
        & ",F02_01.EXTC_TARIFF_CD                                            AS SEIQ_ETARIFF_CD                     " & vbNewLine _
        & ",M44_01.EXTC_TARIFF_REM                                           AS EXTC_TARIFF_REM                     " & vbNewLine _
        & ",F02_01.OUTKA_PLAN_DATE                                           AS OUTKA_PLAN_DATE                     " & vbNewLine _
        & ",''                                                               AS OUTKA_PLAN_TIME                     " & vbNewLine _
        & "-- UPD 20181213 ,''                                                               AS ORIG_CD                             " & vbNewLine _
        & ",ISNULL(MCD.SET_NAIYO_2,'')                                       AS ORIG_CD      -- UPD 20181213        " & vbNewLine _
        & "-- UPD 20181213 .''                                                               AS ORIG_NM                             " & vbNewLine _
        & ",ISNULL(M10_01.DEST_NM,'')                                        AS ORIG_NM                             " & vbNewLine _
        & ",''                                                               AS ORIG_JIS_CD  -- UPD 20181213        " & vbNewLine _
        & ",CONVERT(VARCHAR(8)                                                                                      " & vbNewLine _
        & "       , DATEADD( DAY , 1                                                                                " & vbNewLine _
        & "       , CONVERT(DATETIME , F02_01.OUTKA_PLAN_DATE , 101)) , 112) AS ARR_PLAN_DATE                       " & vbNewLine _
        & ",''                                                               AS ARR_PLAN_TIME                       " & vbNewLine _
        & ",''                                                               AS ARR_ACT_TIME                        " & vbNewLine _
        & ",''                                                               AS DEST_CD                             " & vbNewLine _
        & ",''                                                               AS DEST_NM                             " & vbNewLine _
        & ",''                                                               AS DEST_JIS_CD                         " & vbNewLine _
        & ",''                                                               AS ZIP                                 " & vbNewLine _
        & ",''                                                               AS AD_1                                " & vbNewLine _
        & ",''                                                               AS AD_2                                " & vbNewLine _
        & ",''                                                               AS AD_3                                " & vbNewLine _
        & "--2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start       " & vbNewLine _
        & ",''                                                               AS TEL                                 " & vbNewLine _
        & "--2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end         " & vbNewLine _
        & ",''                                                               AS AREA_CD                             " & vbNewLine _
        & ",''                                                               AS AREA_NM                             " & vbNewLine _
        & ",'0'                                                              AS UNSO_PKG_NB                         " & vbNewLine _
        & ",'0'                                                              AS UNSO_WT                             " & vbNewLine _
        & ",''                                                               AS NB_UT                               " & vbNewLine _
        & ",'90'                                                             AS UNSO_ONDO_KB                        " & vbNewLine _
        & ",''                                                               AS REMARK                              " & vbNewLine _
        & ",'00'                                                             AS TYUKEI_HAISO_FLG                    " & vbNewLine _
        & ",''                                                               AS SYUKA_TYUKEI_CD                     " & vbNewLine _
        & ",''                                                               AS HAIKA_TYUKEI_CD                     " & vbNewLine _
        & ",''                                                               AS TRIP_NO_SYUKA                       " & vbNewLine _
        & ",''                                                               AS TRIP_NO_TYUKEI                      " & vbNewLine _
        & ",''                                                               AS TRIP_NO_HAIKA                       " & vbNewLine _
        & ",''                                                               AS SYS_UPD_DATE                        " & vbNewLine _
        & ",''                                                               AS SYS_UPD_TIME                        " & vbNewLine _
        & ",''                                                               AS PRINT_KB                            " & vbNewLine _
        & ",''                                                               AS OUTKA_STATE_KB                      " & vbNewLine _
        & ",''                                                               AS OUT_UPD_DATE                        " & vbNewLine _
        & ",''                                                               AS OUT_UPD_TIME                        " & vbNewLine _
        & ",'0'                                                              AS PRT_NB                              " & vbNewLine _
        & ",''                                                               AS WH_CD                               " & vbNewLine _
        & "--'START UMANO 要望番号1302 支払運賃に伴う修正。                                                         " & vbNewLine _
        & "--,''                                                               AS SHIHARAI_UNCHIN                     " & vbNewLine _
        & ",''                                                               AS SHIHARAI_TARIFF_CD                  " & vbNewLine _
        & ",''                                                               AS SHIHARAI_TARIFF_REM                 " & vbNewLine _
        & ",''                                                               AS SHIHARAI_ETARIFF_CD                 " & vbNewLine _
        & ",''                                                               AS SHIHARAI_EXTC_TARIFF_REM            " & vbNewLine _
        & ",''                                                               AS NHS_REMARK                          " & vbNewLine _
        & "--'END UMANO 要望番号1302 支払運賃に伴う修正。                                                           " & vbNewLine _
        & "FROM                                                                                                     " & vbNewLine _
        & "     (                                                                                                   " & vbNewLine _
        & "                                                                                                         " & vbNewLine _
        & "             SELECT F02_01.NRS_BR_CD                 AS NRS_BR_CD                                        " & vbNewLine _
        & "                   ,F02_01.CUST_CD_L                 AS CUST_CD_L                                        " & vbNewLine _
        & "                   ,F02_01.CUST_CD_M                 AS CUST_CD_M                                        " & vbNewLine _
        & "                   ,F02_01.OUTKA_PLAN_DATE           AS OUTKA_PLAN_DATE                                  " & vbNewLine _
        & "                   ,F02_01.TARIFF_BUNRUI_KB          AS TARIFF_BUNRUI_KB                                 " & vbNewLine _
        & "                   ,F02_01.SEIQ_TARIFF_CD            AS SEIQ_TARIFF_CD                                   " & vbNewLine _
        & "                   ,F02_01.EXTC_TARIFF_CD            AS EXTC_TARIFF_CD                                   " & vbNewLine _
        & "                   ,F02_01.STR_DATE                  AS STR_DATE                                         " & vbNewLine _
        & "                   ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                             " & vbNewLine _
        & "               FROM                                                                                      " & vbNewLine _
        & "                    (                                                                                    " & vbNewLine _
        & "                            SELECT F02_01.NRS_BR_CD             AS NRS_BR_CD                             " & vbNewLine _
        & "                                  ,F02_01.CUST_CD_L             AS CUST_CD_L                             " & vbNewLine _
        & "                                  ,F02_01.CUST_CD_M             AS CUST_CD_M                             " & vbNewLine _
        & "                                  ,F02_01.OUTKA_PLAN_DATE       AS OUTKA_PLAN_DATE                       " & vbNewLine _
        & "                                  ,M48_01.TARIFF_BUNRUI_KB      AS TARIFF_BUNRUI_KB                      " & vbNewLine _
        & "                                  ,CASE WHEN M48_01.TARIFF_BUNRUI_KB = '20'                              " & vbNewLine _
        & "                                        THEN M48_01.UNCHIN_TARIFF_CD2                                    " & vbNewLine _
        & "                                        WHEN M48_01.TARIFF_BUNRUI_KB = '40'                              " & vbNewLine _
        & "                                        THEN M48_01.YOKO_TARIFF_CD                                       " & vbNewLine _
        & "                                        ELSE M48_01.UNCHIN_TARIFF_CD1                                    " & vbNewLine _
        & "                                   END                          AS SEIQ_TARIFF_CD                        " & vbNewLine _
        & "                                  ,M48_01.EXTC_TARIFF_CD        AS EXTC_TARIFF_CD                        " & vbNewLine _
        & "                                  ,MAX(M47_01.STR_DATE)         AS STR_DATE                              " & vbNewLine _
        & "                            FROM (                                                                       " & vbNewLine _
        & "                                   SELECT                                                                " & vbNewLine _
        & "                                    @NRS_BR_CD    AS NRS_BR_CD                                           " & vbNewLine _
        & "                                   ,@CUST_CD_L    AS CUST_CD_L                                           " & vbNewLine _
        & "                                   ,@CUST_CD_M    AS CUST_CD_M                                           " & vbNewLine _
        & "                                   ,@SYS_DATE     AS OUTKA_PLAN_DATE                                     " & vbNewLine _
        & "                                 ) F02_01                                                                " & vbNewLine _
        & "                            LEFT JOIN $LM_MST$..M_UNCHIN_TARIFF_SET M48_01                               " & vbNewLine _
        & "                              ON F02_01.NRS_BR_CD                 = M48_01.NRS_BR_CD                     " & vbNewLine _
        & "                             AND F02_01.CUST_CD_L                 = M48_01.CUST_CD_L                     " & vbNewLine _
        & "                             AND F02_01.CUST_CD_M                 = M48_01.CUST_CD_M                     " & vbNewLine _
        & "                             AND M48_01.SET_KB                    = '00'                                 " & vbNewLine _
        & "                             AND M48_01.SYS_DEL_FLG               = '0'                                  " & vbNewLine _
        & "                            LEFT JOIN (                                                                  " & vbNewLine _
        & "                                              SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
        & "                                                    ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
        & "                                                    ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
        & "                                                FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
        & "                                               WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & "                                            GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
        & "                                                    ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
        & "                                                    ,M47_01.STR_DATE                                     " & vbNewLine _
        & "                                      )                    M47_01                                        " & vbNewLine _
        & "                              ON F02_01.NRS_BR_CD        = M47_01.NRS_BR_CD                              " & vbNewLine _
        & "                             AND CASE WHEN M48_01.TARIFF_BUNRUI_KB = '20'                                " & vbNewLine _
        & "                                      THEN M48_01.UNCHIN_TARIFF_CD2                                      " & vbNewLine _
        & "                                      ELSE M48_01.UNCHIN_TARIFF_CD1                                      " & vbNewLine _
        & "                                       END               = M47_01.UNCHIN_TARIFF_CD                       " & vbNewLine _
        & "                             AND F02_01.OUTKA_PLAN_DATE >= M47_01.STR_DATE                               " & vbNewLine _
        & "                        GROUP BY  F02_01.NRS_BR_CD                                                       " & vbNewLine _
        & "                                 ,F02_01.CUST_CD_L                                                       " & vbNewLine _
        & "                                 ,F02_01.CUST_CD_M                                                       " & vbNewLine _
        & "                                 ,F02_01.OUTKA_PLAN_DATE                                                 " & vbNewLine _
        & "                                 ,M48_01.TARIFF_BUNRUI_KB                                                " & vbNewLine _
        & "                                 ,M48_01.UNCHIN_TARIFF_CD1                                               " & vbNewLine _
        & "                                 ,M48_01.UNCHIN_TARIFF_CD2                                               " & vbNewLine _
        & "                                 ,M48_01.YOKO_TARIFF_CD                                                  " & vbNewLine _
        & "                                 ,M48_01.EXTC_TARIFF_CD                                                  " & vbNewLine _
        & "                    )    F02_01                                                                          " & vbNewLine _
        & "                LEFT JOIN $LM_MST$..M_UNCHIN_TARIFF M47_01                                               " & vbNewLine _
        & "                  ON F02_01.NRS_BR_CD             = M47_01.NRS_BR_CD                                     " & vbNewLine _
        & "                 AND F02_01.SEIQ_TARIFF_CD        = M47_01.UNCHIN_TARIFF_CD                              " & vbNewLine _
        & "                 AND F02_01.STR_DATE              = M47_01.STR_DATE                                      " & vbNewLine _
        & "                 AND M47_01.SYS_DEL_FLG           = '0'                                                  " & vbNewLine _
        & "            GROUP BY F02_01.NRS_BR_CD                                                                    " & vbNewLine _
        & "                    ,F02_01.CUST_CD_L                                                                    " & vbNewLine _
        & "                    ,F02_01.CUST_CD_M                                                                    " & vbNewLine _
        & "                    ,F02_01.OUTKA_PLAN_DATE                                                              " & vbNewLine _
        & "                    ,F02_01.TARIFF_BUNRUI_KB                                                             " & vbNewLine _
        & "                    ,F02_01.SEIQ_TARIFF_CD                                                               " & vbNewLine _
        & "                    ,F02_01.EXTC_TARIFF_CD                                                               " & vbNewLine _
        & "                    ,F02_01.STR_DATE                                                                     " & vbNewLine _
        & "     )    F02_01                                                                                         " & vbNewLine _
        & "LEFT  JOIN $LM_MST$..M_CUST           M07_01                                                             " & vbNewLine _
        & "  ON  F02_01.NRS_BR_CD              = M07_01.NRS_BR_CD                                                   " & vbNewLine _
        & " AND  F02_01.CUST_CD_L              = M07_01.CUST_CD_L                                                   " & vbNewLine _
        & " AND  F02_01.CUST_CD_M              = M07_01.CUST_CD_M                                                   " & vbNewLine _
        & " AND  M07_01.CUST_CD_S              = '00'                                                               " & vbNewLine _
        & " AND  M07_01.CUST_CD_SS             = '00'                                                               " & vbNewLine _
        & " AND  M07_01.SYS_DEL_FLG            = '0'                                                                " & vbNewLine _
        & "LEFT  JOIN $LM_MST$..M_UNCHIN_TARIFF  M47_01                                                             " & vbNewLine _
        & "  ON  F02_01.NRS_BR_CD              = M47_01.NRS_BR_CD                                                   " & vbNewLine _
        & " AND  F02_01.SEIQ_TARIFF_CD         = M47_01.UNCHIN_TARIFF_CD                                            " & vbNewLine _
        & " AND  F02_01.UNCHIN_TARIFF_CD_EDA   = M47_01.UNCHIN_TARIFF_CD_EDA                                        " & vbNewLine _
        & " AND  F02_01.STR_DATE               = M47_01.STR_DATE                                                    " & vbNewLine _
        & " AND  M47_01.SYS_DEL_FLG            = '0'                                                                " & vbNewLine _
        & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD M49_01                                                             " & vbNewLine _
        & "  ON  F02_01.NRS_BR_CD              = M49_01.NRS_BR_CD                                                   " & vbNewLine _
        & " AND  F02_01.SEIQ_TARIFF_CD         = M49_01.YOKO_TARIFF_CD                                              " & vbNewLine _
        & " AND  M49_01.SYS_DEL_FLG            = '0'                                                                " & vbNewLine _
        & "LEFT  JOIN (                                                                                             " & vbNewLine _
        & "              SELECT NRS_BR_CD       AS NRS_BR_CD                                                        " & vbNewLine _
        & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD                                                   " & vbNewLine _
        & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM                                                  " & vbNewLine _
        & "                FROM $LM_MST$..M_EXTC_UNCHIN                                                             " & vbNewLine _
        & "               WHERE SYS_DEL_FLG = '0'                                                                   " & vbNewLine _
        & "            GROUP BY NRS_BR_CD                                                                           " & vbNewLine _
        & "                    ,EXTC_TARIFF_CD                                                                      " & vbNewLine _
        & "                    ,EXTC_TARIFF_REM                                                                     " & vbNewLine _
        & "           )                          M44_01                                                             " & vbNewLine _
        & "  ON  F02_01.NRS_BR_CD              = M44_01.NRS_BR_CD                                                   " & vbNewLine _
        & " AND  F02_01.EXTC_TARIFF_CD         = M44_01.EXTC_TARIFF_CD                                              " & vbNewLine _
        & "----ADD Start 2018/12/12 依頼番号 : 003455   【LMS】運送新規を押したときに積込先を自動入力               " & vbNewLine _
        & "LEFT JOIN  $LM_MST$..M_CUST_DETAILS MCD                                                                  " & vbNewLine _
        & "  ON  MCD.NRS_BR_CD =  F02_01.NRS_BR_CD                                                                  " & vbNewLine _
        & " AND  MCD.CUST_CD   =  F02_01.CUST_CD_L + F02_01.CUST_CD_M                                               " & vbNewLine _
        & " AND  MCD.SUB_KB    = '9O'    --運送新規積込先                                                           " & vbNewLine _
        & " AND  MCD.SET_NAIYO = '1'     --設定する                                                                 " & vbNewLine _
        & "LEFT  JOIN  $LM_MST$..M_DEST   M10_01                                                                    " & vbNewLine _
        & "  ON  M10_01.NRS_BR_CD      = F02_01.NRS_BR_CD                                                           " & vbNewLine _
        & " AND  M10_01.CUST_CD_L      = F02_01.CUST_CD_L                                                           " & vbNewLine _
        & " AND  M10_01.DEST_CD        = MCD.SET_NAIYO_2                                                            " & vbNewLine _
        & " AND  M10_01.SYS_DEL_FLG    = '0'                                                                        " & vbNewLine _
        & "----ADD End   2018/12/12 依頼番号 : 003455   【LMS】運送新規を押したときに積込先を自動入力               " & vbNewLine

    ''' <summary>
    ''' 商品明細マスタ取得SQL
    ''' </summary>
    Private Const SQL_SELECT_M_GOODS_DETAILS As String =
          "SELECT NRS_BR_CD                               " & vbNewLine _
        & "      ,GOODS_CD_NRS                            " & vbNewLine _
        & "      ,SUB_KB                                  " & vbNewLine _
        & "      ,SET_NAIYO                               " & vbNewLine _
        & "  FROM $LM_MST$..M_GOODS_DETAILS               " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD                  " & vbNewLine _
        & "   AND GOODS_CD_NRS = @GOODS_CD_NRS            " & vbNewLine _
        & "   AND SUB_KB = @SUB_KB                        " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                       " & vbNewLine

    ''' <summary>
    ''' 区分マスタ取得SQL（汎用）
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_Z_KBN_HANYO As String =
          "SELECT                                 " & vbNewLine _
        & "       KBN_GROUP_CD                    " & vbNewLine _
        & "      ,KBN_CD                          " & vbNewLine _
        & "      ,KBN_KEYWORD                     " & vbNewLine _
        & "      ,KBN_NM1                         " & vbNewLine _
        & "      ,KBN_NM2                         " & vbNewLine _
        & "      ,KBN_NM3                         " & vbNewLine _
        & "      ,KBN_NM4                         " & vbNewLine _
        & "      ,KBN_NM5                         " & vbNewLine _
        & "      ,KBN_NM6                         " & vbNewLine _
        & "      ,KBN_NM7                         " & vbNewLine _
        & "      ,KBN_NM8                         " & vbNewLine _
        & "      ,KBN_NM9                         " & vbNewLine _
        & "      ,KBN_NM10                        " & vbNewLine _
        & "      ,KBN_NM11                        " & vbNewLine _
        & "      ,KBN_NM12                        " & vbNewLine _
        & "      ,KBN_NM13                        " & vbNewLine _
        & "      ,VALUE1                          " & vbNewLine _
        & "      ,VALUE2                          " & vbNewLine _
        & "      ,VALUE3                          " & vbNewLine _
        & "      ,SORT                            " & vbNewLine _
        & "      ,REM                             " & vbNewLine _
        & "  FROM $LM_MST$..Z_KBN                 " & vbNewLine _
        & " WHERE SYS_DEL_FLG = '0'               " & vbNewLine _
        & "   AND KBN_GROUP_CD = @KBN_GROUP_CD    " & vbNewLine

    ''' <summary>
    ''' 運送Ｍ登録SQL
    ''' </summary>
    Private Const SQL_INSERT_F_UNSO_M As String =
          "INSERT INTO $LM_TRN$..F_UNSO_M" & vbNewLine _
        & "(                             " & vbNewLine _
        & " NRS_BR_CD                    " & vbNewLine _
        & ",UNSO_NO_L                    " & vbNewLine _
        & ",UNSO_NO_M                    " & vbNewLine _
        & ",GOODS_CD_NRS                 " & vbNewLine _
        & ",GOODS_NM                     " & vbNewLine _
        & ",UNSO_TTL_NB                  " & vbNewLine _
        & ",NB_UT                        " & vbNewLine _
        & ",UNSO_TTL_QT                  " & vbNewLine _
        & ",QT_UT                        " & vbNewLine _
        & ",HASU                         " & vbNewLine _
        & ",ZAI_REC_NO                   " & vbNewLine _
        & ",UNSO_ONDO_KB                 " & vbNewLine _
        & ",IRIME                        " & vbNewLine _
        & ",IRIME_UT                     " & vbNewLine _
        & ",BETU_WT                      " & vbNewLine _
        & ",SIZE_KB                      " & vbNewLine _
        & ",ZBUKA_CD                     " & vbNewLine _
        & ",ABUKA_CD                     " & vbNewLine _
        & ",PKG_NB                       " & vbNewLine _
        & ",LOT_NO                       " & vbNewLine _
        & ",REMARK                       " & vbNewLine _
        & ",PRINT_SORT                   " & vbNewLine _
        & ",SYS_ENT_DATE                 " & vbNewLine _
        & ",SYS_ENT_TIME                 " & vbNewLine _
        & ",SYS_ENT_PGID                 " & vbNewLine _
        & ",SYS_ENT_USER                 " & vbNewLine _
        & ",SYS_UPD_DATE                 " & vbNewLine _
        & ",SYS_UPD_TIME                 " & vbNewLine _
        & ",SYS_UPD_PGID                 " & vbNewLine _
        & ",SYS_UPD_USER                 " & vbNewLine _
        & ",SYS_DEL_FLG                  " & vbNewLine _
        & " )VALUES(                     " & vbNewLine _
        & " @NRS_BR_CD                   " & vbNewLine _
        & ",@UNSO_NO_L                   " & vbNewLine _
        & ",@UNSO_NO_M                   " & vbNewLine _
        & ",@GOODS_CD_NRS                " & vbNewLine _
        & ",@GOODS_NM                    " & vbNewLine _
        & ",@UNSO_TTL_NB                 " & vbNewLine _
        & ",@NB_UT                       " & vbNewLine _
        & ",@UNSO_TTL_QT                 " & vbNewLine _
        & ",@QT_UT                       " & vbNewLine _
        & ",@HASU                        " & vbNewLine _
        & ",@ZAI_REC_NO                  " & vbNewLine _
        & ",@UNSO_ONDO_KB                " & vbNewLine _
        & ",@IRIME                       " & vbNewLine _
        & ",@IRIME_UT                    " & vbNewLine _
        & ",@BETU_WT                     " & vbNewLine _
        & ",@SIZE_KB                     " & vbNewLine _
        & ",@ZBUKA_CD                    " & vbNewLine _
        & ",@ABUKA_CD                    " & vbNewLine _
        & ",@PKG_NB                      " & vbNewLine _
        & ",@LOT_NO                      " & vbNewLine _
        & ",@REMARK                      " & vbNewLine _
        & ",@PRINT_SORT                  " & vbNewLine _
        & ",@SYS_ENT_DATE                " & vbNewLine _
        & ",@SYS_ENT_TIME                " & vbNewLine _
        & ",@SYS_ENT_PGID                " & vbNewLine _
        & ",@SYS_ENT_USER                " & vbNewLine _
        & ",@SYS_UPD_DATE                " & vbNewLine _
        & ",@SYS_UPD_TIME                " & vbNewLine _
        & ",@SYS_UPD_PGID                " & vbNewLine _
        & ",@SYS_UPD_USER                " & vbNewLine _
        & ",@SYS_DEL_FLG                 " & vbNewLine _
        & ")                             " & vbNewLine

    ''' <summary>
    ''' 運賃タリフセットマスタ取得SQL
    ''' </summary>
    Private Const SQL_SELECT_UNCHIN_TARIFF_SET As String =
          "SELECT NRS_BR_CD                        " & vbNewLine _
        & "      ,CUST_CD_L                        " & vbNewLine _
        & "      ,CUST_CD_M                        " & vbNewLine _
        & "      ,SET_MST_CD                       " & vbNewLine _
        & "      ,DEST_CD                          " & vbNewLine _
        & "      ,SET_KB                           " & vbNewLine _
        & "      ,TARIFF_BUNRUI_KB                 " & vbNewLine _
        & "      ,UNCHIN_TARIFF_CD1                " & vbNewLine _
        & "      ,UNCHIN_TARIFF_CD2                " & vbNewLine _
        & "      ,EXTC_TARIFF_CD                   " & vbNewLine _
        & "      ,YOKO_TARIFF_CD                   " & vbNewLine _
        & "  FROM $LM_MST$..M_UNCHIN_TARIFF_SET    " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD           " & vbNewLine _
        & "   AND CUST_CD_L = @CUST_CD_L           " & vbNewLine _
        & "   AND CUST_CD_M = @CUST_CD_M           " & vbNewLine _
        & "   AND SET_KB = @SET_KB                 " & vbNewLine _
        & "   AND DEST_CD = @DEST_CD               " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                " & vbNewLine

#End Region '運送登録処理

    'ADD S 2020/02/07 010901
#Region "GLIS受注登録・削除処理"

    Private Const SQL_SELECT_GLZ9300IN_BOOK_UPD_KEY As String =
          "SELECT TOP 1                                           " & vbNewLine _
        & "       SPM.SHIPMENT_ID                                 " & vbNewLine _
        & "      ,HED.CRT_DATE                                    " & vbNewLine _
        & "      ,HED.FILE_NAME                                   " & vbNewLine _
        & "      ,CB.JOB_NO                                       " & vbNewLine _
        & "      ,ISNULL(BB.EST_NO,'') AS EST_NO                  " & vbNewLine _
        & "      ,ISNULL(BB.EST_NO_EDA,'') AS EST_NO_EDA          " & vbNewLine _
        & "      ,CB.SYS_UPD_DATE AS JOB_SYS_UPD_DATE             " & vbNewLine _
        & "      ,CB.SYS_UPD_TIME AS JOB_SYS_UPD_TIME             " & vbNewLine _
        & "      ,ISNULL(BB.SYS_UPD_DATE,'') AS EST_SYS_UPD_DATE  " & vbNewLine _
        & "      ,ISNULL(BB.SYS_UPD_TIME,'') AS EST_SYS_UPD_TIME  " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED               " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM      " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                     " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                   " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行       " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & " INNER JOIN GL_DB..C_BASIS  CB                         " & vbNewLine _
        & "    ON CB.JOB_NO = HED.OUTKA_CTL_NO                    " & vbNewLine _
        & "   AND CB.BKG_STAGE_KBN <> '00099'                     " & vbNewLine _
        & "   AND CB.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & "  LEFT JOIN GL_DB..B_BASIS  BB                         " & vbNewLine _
        & "    ON BB.EST_NO = CB.EST_NO                           " & vbNewLine _
        & "   AND BB.EST_NO_EDA = CB.EST_NO_EDA                   " & vbNewLine _
        & "   AND BB.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & "   AND HED.STATUS_KB <> '3'                            " & vbNewLine _
        & "   AND HED.OUTKA_CTL_NO <> ''                          " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                  " & vbNewLine _
        & " ORDER BY HED.SYS_ENT_DATE DESC                        " & vbNewLine _
        & "         ,HED.SYS_ENT_TIME DESC                        " & vbNewLine

    Private Const SQL_SELECT_GLZ9300IN_BOOKING_DATA As String =
          "SELECT                                                                             " & vbNewLine _
        & "       SPM.SHIPMENT_ID                                   AS CUST_REF_NO            " & vbNewLine _
        & "      ,ISNULL(RIGHT(CMD1.SKU_NUMBER,8),'')               AS GOODS_CD_HWL_SAP       " & vbNewLine _
        & "      ,ISNULL(CMD1.MAXIMUM_WEIGHT,'')                    AS GROSS_WEIGHT           " & vbNewLine _
        & "      ,ISNULL(CMD2.NUMBER_PIECES,'')                     AS ORAP_CNT               " & vbNewLine _
        & "      ,ISNULL(STP1.LOCATION_ID,'')                       AS PLACE_CD_A_HWL_SAP     " & vbNewLine _
        & "      ,ISNULL(LEFT(STP1.SCHEDULE_START_DATE_TIME,8),'')  AS TRUCK_DATE_A           " & vbNewLine _
        & "      ,ISNULL(STP2.LOCATION_ID,'')                       AS STAR_PLACE_CD_HWL_SAP  " & vbNewLine _
        & "      ,ISNULL(LEFT(STP2.SCHEDULE_START_DATE_TIME,8),'')  AS STAR_DATE              " & vbNewLine _
        & "      ,LEFT(SPM.CON+',',CHARINDEX(',',SPM.CON+',')-1)    AS STAR_REM               " & vbNewLine _
        & "      ,LEFT(SPM.CON+',',CHARINDEX(',',SPM.CON+',')-1)    AS TRN_ORD_NO             " & vbNewLine _
        & "      ,ISNULL(STP2.STOP_NOTE,'')                         AS TRUCK_ARRG_REM         " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                                           " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM                                  " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                                                 " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                                               " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行                                   " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP1                                 " & vbNewLine _
        & "    ON STP1.CRT_DATE = HED.CRT_DATE                                                " & vbNewLine _
        & "   AND STP1.FILE_NAME = HED.FILE_NAME                                              " & vbNewLine _
        & "   AND STP1.STOP_TYPE = 'P'                                                        " & vbNewLine _
        & "   AND STP1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP2                                 " & vbNewLine _
        & "    ON STP2.CRT_DATE = HED.CRT_DATE                                                " & vbNewLine _
        & "   AND STP2.FILE_NAME = HED.FILE_NAME                                              " & vbNewLine _
        & "   AND STP2.STOP_TYPE = 'D'                                                        " & vbNewLine _
        & "   AND STP2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD1                                 " & vbNewLine _
        & "    ON CMD1.CRT_DATE = HED.CRT_DATE                                                " & vbNewLine _
        & "   AND CMD1.FILE_NAME = HED.FILE_NAME                                              " & vbNewLine _
        & "   AND RIGHT(CMD1.SKU_NUMBER,8) <> '10305599'                                      " & vbNewLine _
        & "   AND CMD1.SKU_NUMBER <> ''                                                       " & vbNewLine _
        & "   AND CMD1.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & "  LEFT JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_CMD  CMD2                                 " & vbNewLine _
        & "    ON CMD2.CRT_DATE = HED.CRT_DATE                                                " & vbNewLine _
        & "   AND CMD2.FILE_NAME = HED.FILE_NAME                                              " & vbNewLine _
        & "   AND (RIGHT(CMD2.SKU_NUMBER,8) = '10305599' OR CMD2.SKU_NUMBER = '')             " & vbNewLine _
        & "   AND CMD2.SYS_DEL_FLG = '0'                                                      " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                                                       " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                                    " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                                                  " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                                              " & vbNewLine

    Private Const SQL_SELECT_GLZ9300IN_BOOK_DEL_KEY As String =
          "SELECT                                               " & vbNewLine _
        & "       SPM.SHIPMENT_ID                                 " & vbNewLine _
        & "      ,HED.CRT_DATE                                    " & vbNewLine _
        & "      ,HED.FILE_NAME                                   " & vbNewLine _
        & "      ,CB.JOB_NO                                       " & vbNewLine _
        & "      ,ISNULL(BB.EST_NO,'') AS EST_NO                  " & vbNewLine _
        & "      ,ISNULL(BB.EST_NO_EDA,'') AS EST_NO_EDA          " & vbNewLine _
        & "      ,CB.SYS_UPD_DATE AS JOB_SYS_UPD_DATE             " & vbNewLine _
        & "      ,CB.SYS_UPD_TIME AS JOB_SYS_UPD_TIME             " & vbNewLine _
        & "      ,ISNULL(BB.SYS_UPD_DATE,'') AS EST_SYS_UPD_DATE  " & vbNewLine _
        & "      ,ISNULL(BB.SYS_UPD_TIME,'') AS EST_SYS_UPD_TIME  " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED               " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM      " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                     " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                   " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行       " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & " INNER JOIN GL_DB..C_BASIS  CB                         " & vbNewLine _
        & "    ON CB.JOB_NO = HED.OUTKA_CTL_NO                    " & vbNewLine _
        & "   AND CB.BKG_STAGE_KBN <> '00099'                     " & vbNewLine _
        & "   AND CB.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & "  LEFT JOIN GL_DB..B_BASIS  BB                         " & vbNewLine _
        & "    ON BB.EST_NO = CB.EST_NO                           " & vbNewLine _
        & "   AND BB.EST_NO_EDA = CB.EST_NO_EDA                   " & vbNewLine _
        & "   AND BB.SYS_DEL_FLG = '0'                            " & vbNewLine _
        & " WHERE HED.SYS_DEL_FLG = '0'                           " & vbNewLine _
        & "   AND HED.OUTKA_CTL_NO <> ''                          " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                        " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                      " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @HED_UPD_DATE                " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @HED_UPD_TIME                " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                  " & vbNewLine

#End Region 'GLIS受注登録・削除処理
    'ADD E 2020/02/07 010901

    'ADD START 2019/03/27
#Region "一括変更処理SQL"

    Private Const SQL_UPDATE_OUTKAEDI_DTL_STP_REQ_START_DATE_TIME As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP                        " & vbNewLine _
        & "   SET REQUEST_START_DATE_TIME = @CHANGE_VALUE                 " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP                   " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                  " & vbNewLine _
        & "    ON HED.CRT_DATE = STP.CRT_DATE                             " & vbNewLine _
        & "   AND HED.FILE_NAME = STP.FILE_NAME                           " & vbNewLine _
        & "   AND HED.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM              " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                             " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                           " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行               " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " WHERE STP.STOP_SEQ_NUM = @STOP_SEQ_NUM -- 1:出荷元 2:納入先   " & vbNewLine _
        & "   AND STP.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                          " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                              " & vbNewLine _
        & "   AND HED.SYS_UPD_DATE = @HED_UPD_DATE                        " & vbNewLine _
        & "   AND HED.SYS_UPD_TIME = @HED_UPD_TIME                        " & vbNewLine _
        & "   AND STP.GYO = @STP_GYO                                      " & vbNewLine _
        & "   AND STP.SYS_UPD_DATE = @STP_UPD_DATE                        " & vbNewLine _
        & "   AND STP.SYS_UPD_TIME = @STP_UPD_TIME                        " & vbNewLine

    Private Const SQL_UPDATE_HED_WHEN_UPD_STP_REQ_START_DATE_TIME As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL                            " & vbNewLine _
        & "   SET SYS_UPD_DATE = @SYS_UPD_DATE                            " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                            " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                            " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                            " & vbNewLine _
        & "  FROM $LM_TRN$..H_OUTKAEDI_DTL_HWL_STP  STP                   " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_HED_HWL  HED                  " & vbNewLine _
        & "    ON HED.CRT_DATE = STP.CRT_DATE                             " & vbNewLine _
        & "   AND HED.FILE_NAME = STP.FILE_NAME                           " & vbNewLine _
        & "   AND HED.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " INNER JOIN $LM_TRN$..H_OUTKAEDI_DTL_HWL_SPM  SPM              " & vbNewLine _
        & "    ON SPM.CRT_DATE = HED.CRT_DATE                             " & vbNewLine _
        & "   AND SPM.FILE_NAME = HED.FILE_NAME                           " & vbNewLine _
        & "   AND SPM.GYO = '1' -- ShipmentDetailsは常に1行               " & vbNewLine _
        & "   AND SPM.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & " WHERE STP.STOP_SEQ_NUM = @STOP_SEQ_NUM -- 1:出荷元 2:納入先   " & vbNewLine _
        & "   AND STP.SYS_DEL_FLG = '0'                                   " & vbNewLine _
        & "   AND SPM.SHIPMENT_ID = @SHIPMENT_ID                          " & vbNewLine _
        & "   AND HED.CRT_DATE = @CRT_DATE                                " & vbNewLine _
        & "   AND HED.FILE_NAME = @FILE_NAME                              " & vbNewLine _
        & "   AND STP.GYO = @STP_GYO                                      " & vbNewLine _
        & "   AND STP.SYS_UPD_DATE = @STP_UPD_DATE                        " & vbNewLine _
        & "   AND STP.SYS_UPD_TIME = @STP_UPD_TIME                        " & vbNewLine

#End Region '一括変更処理SQL
    'ADD END   2019/03/27

#Region "受注ステータス戻し処理"

    Private Const SQL_UPDATE_HED_FOR_ROLLBACK_JUCHU_STATUS As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL           " & vbNewLine _
        & "   SET SHINCHOKU_KB_JUCHU = '1'               " & vbNewLine _
        & "      ,CUST_CD_L = ''                         " & vbNewLine _
        & "      ,CUST_CD_M = ''                         " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE           " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME           " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID           " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER           " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                   " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME                 " & vbNewLine _
        & "   AND SYS_UPD_DATE = @SYS_UPD_DATE_BEFORE    " & vbNewLine _
        & "   AND SYS_UPD_TIME = @SYS_UPD_TIME_BEFORE    " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                      " & vbNewLine

    Private Const SQL_DELETE_OUTKA_L As String =
          "DELETE $LM_TRN$..C_OUTKA_L         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L    " & vbNewLine _
        & "   AND SYS_DEL_FLG = '1'           " & vbNewLine

    Private Const SQL_DELETE_OUTKA_M As String =
          "DELETE $LM_TRN$..C_OUTKA_M         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L    " & vbNewLine

    Private Const SQL_DELETE_OUTKA_S As String =
          "DELETE $LM_TRN$..C_OUTKA_S          " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L     " & vbNewLine

    Private Const SQL_DELETE_C_EXPORT_L As String =
          "DELETE $LM_TRN$..C_EXPORT_L         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L     " & vbNewLine

    Private Const SQL_DELETE_C_MARK_HED As String =
          "DELETE $LM_TRN$..C_MARK_HED         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L     " & vbNewLine

    Private Const SQL_DELETE_C_MARK_DTL As String =
          "DELETE $LM_TRN$..C_MARK_DTL         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "   AND OUTKA_NO_L = @OUTKA_NO_L     " & vbNewLine

    Private Const SQL_DELETE_INKA_L As String =
          "DELETE $LM_TRN$..B_INKA_L         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
        & "   AND INKA_NO_L = @INKA_NO_L    " & vbNewLine _
        & "   AND SYS_DEL_FLG = '1'           " & vbNewLine

    Private Const SQL_DELETE_INKA_M As String =
          "DELETE $LM_TRN$..B_INKA_M         " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD      " & vbNewLine _
        & "   AND INKA_NO_L = @INKA_NO_L    " & vbNewLine

    Private Const SQL_DELETE_INKA_S As String =
          "DELETE $LM_TRN$..B_INKA_S          " & vbNewLine _
        & " WHERE NRS_BR_CD = @NRS_BR_CD       " & vbNewLine _
        & "   AND INKA_NO_L = @INKA_NO_L     " & vbNewLine

    Private Const SQL_DELETE_UNSO_L As String =
          "DELETE $LM_TRN$..F_UNSO_L          " & vbNewLine _
        & " WHERE UNSO_NO_L = @UNSO_NO_L     " & vbNewLine

#End Region '受注ステータス戻し処理

#Region "シリンダー取込処理"

    Private Const SQL_UPDATE_OUTKAEDI_HED_CYLINDER_SERIAL_NO As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL                " & vbNewLine _
        & "   SET CYLINDER_SERIAL_NO = @CYLINDER_SERIAL_NO    " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE                " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME                " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID                " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER                " & vbNewLine _
        & " WHERE CRT_DATE = @CRT_DATE                        " & vbNewLine _
        & "   AND FILE_NAME = @FILE_NAME                      " & vbNewLine _
        & "   AND SYS_UPD_DATE = @SYS_UPD_DATE_BEFORE         " & vbNewLine _
        & "   AND SYS_UPD_TIME = @SYS_UPD_TIME_BEFORE         " & vbNewLine _
        & "   AND SYS_DEL_FLG = '0'                           " & vbNewLine

#End Region 'シリンダー取込処理

#Region "JOB NO変更処理"

    Private Const SQL_UPDATE_OUTKAEDI_HED_OUTKA_CTL_NO As String =
          "UPDATE $LM_TRN$..H_OUTKAEDI_HED_HWL        " & vbNewLine _
        & "   SET OUTKA_CTL_NO = @OUTKA_CTL_NO        " & vbNewLine _
        & "      ,SYS_UPD_DATE = @SYS_UPD_DATE        " & vbNewLine _
        & "      ,SYS_UPD_TIME = @SYS_UPD_TIME        " & vbNewLine _
        & "      ,SYS_UPD_PGID = @SYS_UPD_PGID        " & vbNewLine _
        & "      ,SYS_UPD_USER = @SYS_UPD_USER        " & vbNewLine _
        & " WHERE OUTKA_CTL_NO = (SELECT HED.OUTKA_CTL_NO                   " & vbNewLine _
        & "                         FROM $LM_TRN$..H_OUTKAEDI_HED_HWL  HED  " & vbNewLine _
        & "                        WHERE HED.CRT_DATE = @CRT_DATE           " & vbNewLine _
        & "                          AND HED.FILE_NAME = @FILE_NAME         " & vbNewLine _
        & "                          AND HED.SYS_UPD_DATE = @HED_UPD_DATE   " & vbNewLine _
        & "                          AND HED.SYS_UPD_TIME = @HED_UPD_TIME   " & vbNewLine _
        & "                          AND HED.SYS_DEL_FLG = '0')             " & vbNewLine

#End Region 'JOB NO変更処理

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
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_COUNT_S)

        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko Then
            '倉庫
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_SOKO)
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_SOKO_2)
        ElseIf Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            ' Chilled Lorry
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_SOKO)
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_CHILLED_LORRY)
        ElseIf Me._Row.Item("BUMON").ToString() = CmbBumonItems.ISO Then
            'ISO
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_ISO)
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("SHUKKA_DATE_FROM").ToString()) Then
            '出荷日From
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_COND_SHUKKA_DATE_FROM)
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("SHUKKA_DATE_TO").ToString()) Then
            '出荷日To
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_COND_SHUKKA_DATE_TO)
        End If


        'Where条件追加
        Call Me.AppendWhereClauseSearch()   'ADD 2020/02/07 010901


        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
            Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            '倉庫
            'または Chilled Lorry
            Me._StrSql.Append(LMI960DAC.SQL_GROUP_SEARCH_DATA_SOKO)
        End If

        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_COUNT_E)

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

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko Then
            '倉庫
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_SOKO)
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_SOKO_2)
        ElseIf Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            ' Chilled Lorry
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_SOKO)
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_CHILLED_LORRY)
        ElseIf Me._Row.Item("BUMON").ToString() = CmbBumonItems.ISO Then
            'ISO
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_ISO)
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("SHUKKA_DATE_FROM").ToString()) Then
            '出荷日From
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_COND_SHUKKA_DATE_FROM)
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("SHUKKA_DATE_TO").ToString()) Then
            '出荷日To
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_SEARCH_DATA_COND_SHUKKA_DATE_TO)
        End If


        'Where条件追加
        Call Me.AppendWhereClauseSearch()   'ADD 2020/02/07 010901


        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
            Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            '倉庫
            'または Chilled Lorry
            Me._StrSql.Append(LMI960DAC.SQL_GROUP_SEARCH_DATA_SOKO)
        End If

        Me._StrSql.Append(LMI960DAC.SQL_ORDER_SEARCH)

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

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        'ADD S 2019/12/12 009741
        map.Add("STATUS_KB", "STATUS_KB")
        map.Add("DEL_KB", "DEL_KB")                             'ADD 2020/02/07 010901
        map.Add("SHINCHOKU_KB_JUCHU", "SHINCHOKU_KB_JUCHU")
        'ADD E 2019/12/12 009741
        map.Add("SHINCHOKU_KB", "SHINCHOKU_KB")
        map.Add("DELAY_STATUS", "DELAY_STATUS")
        map.Add("CYLINDER_SERIAL_NO", "CYLINDER_SERIAL_NO")
        map.Add("GOODS_CD", "GOODS_CD")
        map.Add("GOODS_NM", "GOODS_NM")
        map.Add("SHIPMENT_ID", "SHIPMENT_ID")
        map.Add("SAP_ORD_NO", "SAP_ORD_NO")
        map.Add("CUST_ORD_NO", "CUST_ORD_NO")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")                 'ADD 2020/02/07 010901
        map.Add("CUST_CD_L", "CUST_CD_L")                       'ADD 2020/02/27 010901
        map.Add("CUST_CD_M", "CUST_CD_M")                       'ADD 2020/02/27 010901
        map.Add("SHUKKA_DATE", "SHUKKA_DATE")
        map.Add("NONYU_DATE", "NONYU_DATE")
        map.Add("SHUKKA_MOTO_CD", "SHUKKA_MOTO_CD")
        map.Add("SHUKKA_MOTO", "SHUKKA_MOTO")
        map.Add("NONYU_SAKI_CD", "NONYU_SAKI_CD")
        map.Add("NONYU_SAKI", "NONYU_SAKI")
        map.Add("MAXIMUM_WEIGHT", "MAXIMUM_WEIGHT")
        map.Add("HED_CRT_DATE", "HED_CRT_DATE")
        map.Add("HED_FILE_NAME", "HED_FILE_NAME")
        map.Add("HED_UPD_DATE", "HED_UPD_DATE")
        map.Add("HED_UPD_TIME", "HED_UPD_TIME")
        'ADD START 2019/03/27
        map.Add("STP1_GYO", "STP1_GYO")
        map.Add("STP1_UPD_DATE", "STP1_UPD_DATE")
        map.Add("STP1_UPD_TIME", "STP1_UPD_TIME")
        map.Add("STP2_GYO", "STP2_GYO")
        map.Add("STP2_UPD_DATE", "STP2_UPD_DATE")
        map.Add("STP2_UPD_TIME", "STP2_UPD_TIME")
        'ADD END   2019/03/27
        map.Add("P_STOP_NOTE", "P_STOP_NOTE")
        map.Add("D_STOP_NOTE", "D_STOP_NOTE")
        map.Add("SKU_NUMBER", "SKU_NUMBER")
        map.Add("NUMBER_PIECES", "NUMBER_PIECES")
        map.Add("OUTKA_CTL_NO_DELETED", "OUTKA_CTL_NO_DELETED")
        map.Add("SEQ_DESC", "SEQ_DESC")
        map.Add("BUYID", "BUYID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI960DAC.TABLE_NM_OUT)

        Return ds

    End Function

#End Region '検索処理

#Region "実績作成処理"

    ''' <summary>
    ''' ShipmentIDデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>件数取得SQLの構築・発行</remarks>
    Private Function SelectCntShipmentID(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SHIPMENT_ID_COUNT)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetShipmentIDParameter(Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ハネウェルＥＤＩ受信データ取得SQLの構築・発行</remarks>
    Private Function SelectOutkaEdiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Select Case inTbl.Rows(0).Item("BASHO_KB").ToString()
            Case CmbBashoKbItems.Tsumikomi
                Me._StrSql.Append(LMI960DAC.SQL_SELECT_OUTKAEDI_DATA_TSUMIKOMI)
            Case CmbBashoKbItems.Nioroshi, CmbBashoKbItems.NonyuYotei
                Me._StrSql.Append(LMI960DAC.SQL_SELECT_OUTKAEDI_DATA_NIOROSHI)
        End Select

        Dim bumon As String = inTbl.Rows(0).Item("BUMON").ToString()
        'DEL S 2020/04/17 012230
        'If bumon = CmbBumonItems.Soko Then
        '    '倉庫
        '    Me._StrSql.Append(LMI960DAC.SQL_SELECT_OUTKAEDI_DATA_CONDITION_SOKO)
        'ElseIf bumon = CmbBumonItems.ISO Then
        '    'ISO
        '    Me._StrSql.Append(LMI960DAC.SQL_SELECT_OUTKAEDI_DATA_CONDITION_ISO)
        'End If
        'DEL E 2020/04/17 012230

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectOutkaEdiDataParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CRT_DATE", "CRT_DATE")
        map.Add("FILE_NAME", "FILE_NAME")
        map.Add("SHIPMENT_ID", "SHIPMENT_ID")
        map.Add("SHIPMENT_REF_NUM_MISC", "SHIPMENT_REF_NUM_MISC")
        map.Add("STOP_SEQ_NUM", "STOP_SEQ_NUM")
        map.Add("LOCATION_ID", "LOCATION_ID")
        map.Add("EVENT_DATE", "EVENT_DATE")
        map.Add("CITY", "CITY")
        map.Add("COUNTRY", "COUNTRY")
        map.Add("ZIP_CODE", "ZIP_CODE")
        map.Add("TRACTOR_NUMBER", "TRACTOR_NUMBER")
        map.Add("TRAILER_NUMBER", "TRAILER_NUMBER")
        map.Add("PRO_NUMBER", "PRO_NUMBER")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI960DAC.TABLE_NM_JISSEKI_DATA)

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ登録(積込場・荷下場用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdi(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim jissekiDataTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_JISSEKI_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        Me._Row = jissekiDataTbl.Rows(0)

        For sakuseiKbn As Integer = JissekiSakuseiKb.Arrival To JissekiSakuseiKb.Departure

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetInsertSendOutEdiParameter(sakuseiKbn, Me._Row, inTblRow, Me._SqlPrmList)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds
    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ登録(納入予定用)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdi2(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim jissekiDataTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_JISSEKI_DATA)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        Me._Row = jissekiDataTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetInsertSendOutEdi2Parameter(Me._Row, inTblRow, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)進捗区分更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedShinchoku(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_HED_SHINCHOKU _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedShinchokuParameter(inTblRow, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds
    End Function

#End Region '実績作成処理

    'ADD S 2019/12/12 009741
#Region "受注作成処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ハネウェルＥＤＩ送信(受注可否)データ存在チェックSQLの構築・発行</remarks>
    Private Function SelectSendJuchuData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SENDOUTEDI)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectCountSendJuchuDataParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CREAT_DATE_TIME", "CREAT_DATE_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960H_SENDOUTEDI_HWL")

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ検索(受注作成用存在チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ハネウェルＥＤＩ受信データ取得SQLの構築・発行</remarks>
    Private Function SelectCountOutkaEdiDataJuchu(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectCountOutkaEdiDataJuchuParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function

    'ADD S 2020/03/06 011377
    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ検索(受注OK作成用存在チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ハネウェルＥＤＩ受信データ取得SQLの構築・発行</remarks>
    Private Function SelectCountOutkaEdiDataJuchuOK(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()

        Dim bumon As String = inTbl.Rows(0).Item("BUMON").ToString()
        If bumon = CmbBumonItems.Soko OrElse
            bumon = CmbBumonItems.ChilledLorry Then
            '倉庫
            'または Chilled Lorry
            If Me._Row.Item("INOUT_KB").ToString = LMI960DAC.InOutKb.Inka Then
                '入荷
                Me._StrSql.Append(LMI960DAC.SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_SOKO_INKA)
            ElseIf Me._Row.Item("INOUT_KB").ToString = LMI960DAC.InOutKb.Unso Then
                '運送
                Me._StrSql.Append(LMI960DAC.SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_SOKO_UNSO)
            Else
                '出荷
                Me._StrSql.Append(LMI960DAC.SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_SOKO_OUTKA)
            End If
        ElseIf bumon = CmbBumonItems.ISO Then
            'ISO
            Me._StrSql.Append(LMI960DAC.SQL_SELECT_COUNT_OUTKAEDI_DATA_JUCHU_OK_ISO)
        End If

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectCountOutkaEdiDataJuchuParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        Return ds

    End Function
    'ADD E 2020/03/06 011377

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(990)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiJuchu(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetInsertSendOutEdiJuchuParameter(Me._Row, inTblRow, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    'ADD S 2020/03/06 011377
    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(214)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiJuchu214(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetInsertSendOutEdiJuchuParameter214(Me._Row, inTblRow, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function
    'ADD E 2020/03/06 011377

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)進捗区分(受注ステータス)更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedShinchokuJuchu(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_HED_SHINCHOKU_JUCHU _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedShinchokuJuchuParameter(inTblRow, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds
    End Function

#End Region '受注作成処理
    'ADD E 2019/12/12 009741

#Region "遅延送信処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(遅延理由)データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>ハネウェルＥＤＩ送信(遅延理由)データ存在チェックSQLの構築・発行</remarks>
    Private Function SelectSendDelayData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SENDOUTEDI)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectCountSendDelayDataParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CREAT_DATE_TIME", "CREAT_DATE_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960H_SENDOUTEDI_HWL")

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(遅延理由)データ登録(214)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiDelay(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim jissekiDataTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_JISSEKI_DATA)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        '条件rowの格納
        Me._Row = jissekiDataTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI_2 _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetInsertSendOutEdiDelayParameter(Me._Row, inTblRow, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#Region "Booked解除送信処理"

    ''' <summary>
    ''' 処理対象範囲の行の H_OUTKAEDI_HED_HWL の SYS_ENT_DATE, SYS_ENT_TIME の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectHedSpmSendoutediByPkey(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        Me._StrSql = New StringBuilder()
        Me._SqlPrmList = New ArrayList()

        Dim where As New StringBuilder
        For rowNo = 0 To targetTbl.Rows.Count - 1
            If rowNo = 0 Then
                where.Append(String.Concat("   (    CRT_DATE = @CRT_DATE", "_", rowNo.ToString(), "      " & vbNewLine))
                where.Append(String.Concat("    AND FILE_NAME = @FILE_NAME", "_", rowNo.ToString(), ")   " & vbNewLine))
            Else
                where.Append(String.Concat("OR (    CRT_DATE = @CRT_DATE", "_", rowNo.ToString(), "      " & vbNewLine))
                where.Append(String.Concat("    AND FILE_NAME = @FILE_NAME", "_", rowNo.ToString(), ")   " & vbNewLine))
            End If

            ' INTableの条件rowの格納
            Me._Row = targetTbl.Rows(rowNo)

            ' SQLパラメータ
            Call Me.SetSelectHedSpmSendoutediByPkeyParameter(Me._Row, rowNo, Me._SqlPrmList)
        Next

        ' SQL作成
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_HED_SPM_SENDOUTEDI_BY_PKEY.Replace("" _
                            & "   (    CRT_DATE = @CRT_DATE      " & vbNewLine _
                            & "    AND FILE_NAME = @FILE_NAME)   " & vbNewLine,
                            where.ToString()))

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            cmd.Parameters.AddRange(_SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
                map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960_OUTKAEDI_HED")

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 対象行と 同一の Load Number で、対象行より後に受信し、かつ
    ''' 応答レコードがある H_OUTKAEDI_HED_HWL の CRT_DATE, FILE_NAME の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectHedSpmSendoutediByShipmentId(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        ' INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        ' SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_HED_SPM_SENDOUTEDI_BY_SHIPMENT_ID)

        ' SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectHedSpmSendoutediByShipmentIdParameter(Me._Row, Me._SqlPrmList)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            cmd.Parameters.AddRange(_SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960_OUTKAEDI_HED")

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 対象行と 同一の Load Number で、対象行より前に受信し、かつ
    ''' 応答レコードがある納入日情報と、対象行の納入日情報の取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSendoutediOutkaediHedDtlStp(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        ' INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        ' SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SENDOUTEDI_OUTKAEDI_HED_DTL_STP)

        ' SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectSendoutediOutkaediHedDtlStpParameter(Me._Row, Me._SqlPrmList)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            cmd.Parameters.AddRange(_SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("SCHEDULE_START_DATE_TIME", "SCHEDULE_START_DATE_TIME")
                map.Add("SCHEDULE_END_DATE_TIME", "SCHEDULE_END_DATE_TIME")
                map.Add("REQUEST_START_DATE_TIME", "REQUEST_START_DATE_TIME")
                map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
                map.Add("SYS_ENT_TIME", "SYS_ENT_TIME")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960_SENDOUTEDI_OUTKAEDI_DTL_STP")

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信の(注文応答990取消または注文応答214(受注OK)取消))データ存在チェック
    ''' 検索条件: PKEY, 検索結果: 件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSendoutedi990CancelOr214CancelByPkey(ByVal ds As DataSet) As DataSet

        Return SelectSendoutediByPkey(ds,
                    HOutkaediHedHwlGyo.OrderResponse990Cancel, HOutkaediHedHwlGyo.OrderResponse214Cancel)

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信の(注文応答990または注文応答214(受注OK))データ存在チェック
    ''' 検索条件: PKEY, 検索結果: 件数
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSendoutedi990Or214ByPkey(ByVal ds As DataSet) As DataSet

        Return SelectSendoutediByPkey(ds,
                    HOutkaediHedHwlGyo.OrderResponse990, HOutkaediHedHwlGyo.OrderResponse214)

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信の(注文応答990取消または注文応答214(受注OK)取消))データ存在チェック
    ''' ハネウェルＥＤＩ送信の(注文応答990または注文応答214(受注OK))データ存在チェック
    ''' 検索条件: PKEY
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="gyo">SQLパラメータ @GYO への設定値</param>
    ''' <param name="gyo2">SQLパラメータ @GYO_2 への設定値</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSendoutediByPkey(ByVal ds As DataSet,
                                                    ByVal gyo As HOutkaediHedHwlGyo, ByVal gyo2 As HOutkaediHedHwlGyo) As DataSet

        ' DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables("LMI960_SELECT_SENDOUTEDI_BY_PKEY_IN")

        ' INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        ' SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_SENDOUTEDI_BY_PKEY)

        ' SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectSendoutediCountByPkeyParameter(Me._Row, Me._SqlPrmList, gyo, gyo2)

        ' スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        ' SQL文のコンパイル
        Using cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

            ' パラメータの反映
            cmd.Parameters.AddRange(_SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            ' SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                ' DataReader→DataTableへの転記
                Dim map As Hashtable = New Hashtable()

                ' 取得データの格納先をマッピング
                map.Add("CRT_DATE", "CRT_DATE")
                map.Add("FILE_NAME", "FILE_NAME")
                map.Add("GYO", "GYO")
                map.Add("SHIPMENT_ACCEPT", "SHIPMENT_ACCEPT")
                map.Add("SHIPMENT_DECLINE", "SHIPMENT_DECLINE")
                map.Add("SHIPMENT_DECLINE_REASON", "SHIPMENT_DECLINE_REASON")
                map.Add("SHIPMENT_ID", "SHIPMENT_ID")
                map.Add("SHIPMENT_REF_NUM_MISC", "SHIPMENT_REF_NUM_MISC")
                map.Add("TRACTOR_NUMBER", "TRACTOR_NUMBER")

                ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960_SENDOUTEDI")

            End Using

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(990 取消)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiJuchuCancel(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables("LMI960_SENDOUTEDI")

        ' 条件rowの格納
        Dim where As String = String.Concat("GYO = '", CStr(HOutkaediHedHwlGyo.OrderResponse990), "'")
        If targetTbl.Select(where).Count > 0 Then
            Me._Row = targetTbl.Select(where)(0)
        Else
            ' 以下、理論上ないはずだが念のため
            Me._Row = targetTbl.NewRow()
            Me._Row.Item("CRT_DATE") = targetTbl.Rows(0).Item("CRT_DATE").ToString()
            Me._Row.Item("FILE_NAME") = targetTbl.Rows(0).Item("FILE_NAME").ToString()
            Me._Row.Item("SHIPMENT_ID") = targetTbl.Rows(0).Item("SHIPMENT_ID").ToString()
            Me._Row.Item("SHIPMENT_ACCEPT") = ""
            Me._Row.Item("SHIPMENT_DECLINE") = ""
            Me._Row.Item("SHIPMENT_DECLINE_REASON") = ""
        End If

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' パラメータの初期化
        cmd.Parameters.Clear()

        ' パラメータ設定
        Call Me.SetInsertSendOutEdiJuchuCancelParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        ' パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        ' SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(214 取消)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutEdiJuchu214Cancel(ByVal ds As DataSet) As DataSet

        ' DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables("LMI960_SENDOUTEDI")

        ' 条件rowの格納
        Dim where As String = String.Concat("GYO = '", CStr(HOutkaediHedHwlGyo.OrderResponse214), "'")
        If targetTbl.Select(where).Count > 0 Then
            Me._Row = targetTbl.Select(where)(0)
        Else
            ' 以下、理論上ないはずだが念のため
            Me._Row = targetTbl.NewRow()
            Me._Row.Item("CRT_DATE") = targetTbl.Rows(0).Item("CRT_DATE").ToString()
            Me._Row.Item("FILE_NAME") = targetTbl.Rows(0).Item("FILE_NAME").ToString()
            Me._Row.Item("SHIPMENT_ID") = targetTbl.Rows(0).Item("SHIPMENT_ID").ToString()
            Me._Row.Item("SHIPMENT_REF_NUM_MISC") = ""
            Me._Row.Item("TRACTOR_NUMBER") = ""
        End If

        ' SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSET_SENDOUTEDI _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        ' SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        ' パラメータの初期化
        cmd.Parameters.Clear()

        ' パラメータ設定
        Call Me.SetInsertSendOutEdiJuchu214CancelParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetDataInsertParameter(Me._SqlPrmList)

        ' パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        ' SQLの発行
        MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region ' "Booked解除送信処理"

    'ADD S 2020/02/27 010901
#Region "荷主振り分け処理"

    ''' <summary>
    ''' 荷主振り分け用Stop->City取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主振り分け用Stop->City取得SQLの構築・発行</remarks>
    Private Function SelectStpCityForSpecifyCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_STP_CITY_FOR_SPECIFY_CUST)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectStpCityForSpecifyCustParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CITY", "CITY")
        map.Add("ZIP_CODE", "ZIP_CODE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960STP_FOR_SPECIFY_CUST")

        Return ds

    End Function

    ''' <summary>
    ''' 荷主振り分け用荷主コード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>荷主振り分け用荷主コード取得SQLの構築・発行</remarks>
    Private Function SelectCustCdBySkuNumber(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_CUST_CD_BY_SKU_NUMBER)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectCustCdBySkuNumberParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960M_CUST")

        Return ds

    End Function

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)荷主コード更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedCust(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim custTblRow As DataRow = ds.Tables("LMI960M_CUST").Rows(0)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_HED_CUST _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedCustParameter(custTblRow, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region '荷主振り分け処理

#Region "出荷登録処理"

    ''' <summary>
    ''' 入出荷登録の元情報（Header、ShipmentDetails、Stops）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入出荷登録の元情報（Header、ShipmentDetails、Stops）取得SQLの構築・発行</remarks>
    Private Function SelectHedSpmStpForInsInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_HED_SPM_STP_FOR_INS_INOUTKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectHedSpmStpForInsInOutkaParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("INOUT_KB", "INOUT_KB")
        map.Add("OUTKA_CTL_NO", "OUTKA_CTL_NO")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("LOCATION_ID", "LOCATION_ID")
        map.Add("CON", "CON")
        map.Add("INKA_INKA_DATE", "INKA_INKA_DATE")
        map.Add("INKA_ORIG_CD", "INKA_ORIG_CD")
        map.Add("TRACTOR_NUMBER", "TRACTOR_NUMBER")
        map.Add("BUYID", "BUYID")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        Return ds

    End Function

    ''' <summary>
    ''' 入出荷登録の元情報（Commodity）取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入出荷登録の元情報（Commodity）取得SQLの構築・発行</remarks>
    Private Function SelectCmdForInsInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_CMD_FOR_INS_OUTKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectCmdForInsInOutkaParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CMD_GYO", "GYO")
        map.Add("SKU_NUMBER", "SKU_NUMBER")
        map.Add("NUMBER_PIECES", "NUMBER_PIECES")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960CMD_FOR_INS_INOUTKA")

        Return ds

    End Function

    ''' <summary>
    ''' 入出荷登録用荷主情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入出荷登録用荷主情報取得SQLの構築・発行</remarks>
    Private Function SelectMCustForInsInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_M_CUST_FOR_INS_INOUTKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectMCustForInsInOutkaParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("HOKAN_FREE_KIKAN", "HOKAN_FREE_KIKAN")
        map.Add("DEFAULT_SOKO_CD", "DEFAULT_SOKO_CD")
        map.Add("WH_TAB_YN", "WH_TAB_YN")
        map.Add("SOKO_DEST_CD", "SOKO_DEST_CD")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960M_CUST")

        Return ds

    End Function

    ''' <summary>
    ''' 入出荷登録用届先情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入出荷登録用届先情報取得SQLの構築・発行</remarks>
    Private Function SelectMDestForInsInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_M_DEST_FOR_INS_INOUTKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectMDestForInsInOutkaParameter(ds, inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEST_CD", "DEST_CD")
        map.Add("AD_3", "AD_3")
        map.Add("TEL", "TEL")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960M_DEST")

        Return ds

    End Function

    ''' <summary>
    ''' 出荷登録用売上先(届先)情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>出荷登録用売上先(届先)情報取得SQLの構築・発行</remarks>
    Private Function SelectMDestShipForInsOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables("LMI960HED_SPM_STP_FOR_INS_INOUTKA")

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_M_DEST_FOR_INS_INOUTKA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        _SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        _SqlPrmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
        _SqlPrmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("BUYID").ToString(), DBDataType.VARCHAR))

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("DEST_CD", "DEST_CD")
        map.Add("AD_3", "AD_3")
        map.Add("TEL", "TEL")
        map.Add("SP_NHS_KB", "SP_NHS_KB")
        map.Add("COA_YN", "COA_YN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960M_DEST")

        Return ds

    End Function

    ''' <summary>
    ''' 入出荷登録用商品情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>入出荷登録用商品情報取得SQLの構築・発行</remarks>
    Private Function SelectMGoodsForInsInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO_CMD"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables("LMI960CMD_FOR_INS_INOUTKA")

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_M_GOODS_FOR_INS_OUTKA)

        If Not String.IsNullOrEmpty(Me._Row.Item("GOODS_CD_NRS").ToString()) Then
            Me._StrSql.Append("   AND GOODS_CD_NRS = @GOODS_CD_NRS    " & vbNewLine)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectMGoodsForInsInOutkaParameter(ds, Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("GOODS_NM_1", "GOODS_NM_1")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("ALCTD_KB", "ALCTD_KB")
        map.Add("NB_UT", "NB_UT")
        map.Add("PKG_NB", "PKG_NB")
        map.Add("PKG_UT", "PKG_UT")
        map.Add("STD_IRIME_NB", "STD_IRIME_NB")
        map.Add("STD_IRIME_UT", "STD_IRIME_UT")
        map.Add("STD_WT_KGS", "STD_WT_KGS")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("OUTKA_ATT", "OUTKA_ATT")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("COA_YN", "COA_YN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960M_GOODS")

        Return ds

    End Function

    ''' <summary>
    ''' 入出荷登録用ハネウェルＥＤＩ受信データ(Header)更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedForInsInOutka(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_HED_FOR_INS_INOUTKA _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedForInsInOutkaParameter(ds, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds
    End Function

    ''' <summary>
    ''' 出荷データＬ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertCOutkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim insDataTbl As DataTable = ds.Tables("LMI960IN_OUTKA_L")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSERT_C_OUTKA_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        For Each Me._Row In insDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetInsertCOutkaLParameter(Me._Row, inTblRow, Me._SqlPrmList)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データＭ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertCOutkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim insDataTbl As DataTable = ds.Tables("LMI960IN_OUTKA_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSERT_C_OUTKA_M _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        For Each Me._Row In insDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetInsertCOutkaMParameter(Me._Row, inTblRow, Me._SqlPrmList)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region
    'ADD E 2020/02/27 010901

#Region "入荷登録処理"

    ''' <summary>
    ''' 入荷データＬ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertBInkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim insDataTbl As DataTable = ds.Tables("LMI960IN_INKA_L")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSERT_B_INKA_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        For Each Me._Row In insDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetInsertBInkaLParameter(Me._Row, inTblRow, Me._SqlPrmList)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送Ｌ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertFUnsoL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim insDataTbl As DataTable = Nothing
        '処理制御データテーブル
        Dim drProcCtrlData As DataRow = ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0)
        Select Case drProcCtrlData("INOUT_KB").ToString
            Case LMI960DAC.InOutKb.Inka
                insDataTbl = ds.Tables("LMI960IN_UNSO_L")
            Case LMI960DAC.InOutKb.Unso
                insDataTbl = ds.Tables("F_UNSO_L")
        End Select

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSERT_F_UNSO_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        For Each Me._Row In insDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetInsertFUnsoLParameter(Me._Row, drProcCtrlData, Me._SqlPrmList)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

#End Region '入荷登録処理

#Region "運送登録処理"

    ''' <summary>
    ''' 運送L初期値取得（運送登録用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送L初期値取得（運送登録用）SQLの構築・発行</remarks>
    Private Function SelectUnsoLSource(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_UNSO_L_SOURCE)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectUnsoLSourceParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("JIYU_KB", "JIYU_KB")
        map.Add("PC_KB", "PC_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("TARE_YN", "TARE_YN")
        map.Add("DENP_NO", "DENP_NO")
        map.Add("AUTO_DENP_KBN", "AUTO_DENP_KBN")
        map.Add("AUTO_DENP_NO", "AUTO_DENP_NO")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("SHIP_CD", "SHIP_CD")
        map.Add("SHIP_NM", "SHIP_NM")
        map.Add("BUY_CHU_NO", "BUY_CHU_NO")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("TARIFF_REM", "TARIFF_REM")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("EXTC_TARIFF_REM", "EXTC_TARIFF_REM")
        map.Add("SHIHARAI_TARIFF_CD", "SHIHARAI_TARIFF_CD")
        map.Add("SHIHARAI_TARIFF_REM", "SHIHARAI_TARIFF_REM")
        map.Add("SHIHARAI_ETARIFF_CD", "SHIHARAI_ETARIFF_CD")
        map.Add("SHIHARAI_EXTC_TARIFF_REM", "SHIHARAI_EXTC_TARIFF_REM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("OUTKA_PLAN_TIME", "OUTKA_PLAN_TIME")
        map.Add("ORIG_CD", "ORIG_CD")
        map.Add("ORIG_NM", "ORIG_NM")
        map.Add("ORIG_JIS_CD", "ORIG_JIS_CD")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("ARR_PLAN_TIME", "ARR_PLAN_TIME")
        map.Add("ARR_ACT_TIME", "ARR_ACT_TIME")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("ZIP", "ZIP")
        map.Add("AD_1", "AD_1")
        map.Add("AD_2", "AD_2")
        map.Add("AD_3", "AD_3")
        map.Add("TEL", "TEL")
        map.Add("AREA_CD", "AREA_CD")
        map.Add("AREA_NM", "AREA_NM")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("NB_UT", "NB_UT")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("REMARK", "REMARK")
        map.Add("TYUKEI_HAISO_FLG", "TYUKEI_HAISO_FLG")
        map.Add("SYUKA_TYUKEI_CD", "SYUKA_TYUKEI_CD")
        map.Add("HAIKA_TYUKEI_CD", "HAIKA_TYUKEI_CD")
        map.Add("TRIP_NO_SYUKA", "TRIP_NO_SYUKA")
        map.Add("TRIP_NO_TYUKEI", "TRIP_NO_TYUKEI")
        map.Add("TRIP_NO_HAIKA", "TRIP_NO_HAIKA")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("PRINT_KB", "PRINT_KB")
        map.Add("OUTKA_STATE_KB", "OUTKA_STATE_KB")
        map.Add("OUT_UPD_DATE", "OUT_UPD_DATE")
        map.Add("OUT_UPD_TIME", "OUT_UPD_TIME")
        map.Add("PRT_NB", "PRT_NB")
        map.Add("WH_CD", "WH_CD")
        map.Add("NHS_REMARK", "NHS_REMARK")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "F_UNSO_L")

        Return ds

    End Function

    ''' <summary>
    ''' 商品明細マスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>商品明細マスタ取得SQLの構築・発行</remarks>
    Private Function SelectMGoodsDetails(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inOutTbl As DataTable = ds.Tables("LMI960INOUT_M_GOODS_DETAILS")

        'INTableの条件rowの格納
        Me._Row = inOutTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_M_GOODS_DETAILS)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectMGoodsDetailsParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("GOODS_CD_NRS", "GOODS_CD_NRS")
        map.Add("SUB_KB", "SUB_KB")
        map.Add("SET_NAIYO", "SET_NAIYO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960INOUT_M_GOODS_DETAILS")

        Return ds

    End Function

    ''' <summary>
    ''' 区分マスタ取得（汎用）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    Private Function SelectZKbnHanyo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMI960INOUT_Z_KBN")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL作成
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_Z_KBN_HANYO)
        If Me._Row.Item("KBN_CD") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_CD").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_CD = @KBN_CD                " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM1") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM1").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM1 = @KBN_NM1              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM2") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM2").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM2 = @KBN_NM2              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM3") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM3").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM3 = @KBN_NM3              " & vbNewLine)
        End If
        If Me._Row.Item("KBN_NM4") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM4").ToString() <> "" Then
            Me._StrSql.Append("   AND KBN_NM4 = @KBN_NM4              " & vbNewLine)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), ds.Tables("LMI960IN").Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの設定
        Me._SqlPrmList = New ArrayList()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", Me._Row.Item("KBN_GROUP_CD").ToString(), DBDataType.CHAR))
        If Me._Row.Item("KBN_CD") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_CD").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_CD", Me._Row.Item("KBN_CD").ToString(), DBDataType.CHAR))
        End If
        If Me._Row.Item("KBN_NM1") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM1").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM1", Me._Row.Item("KBN_NM1").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM2") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM2").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM2", Me._Row.Item("KBN_NM2").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM3") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM3").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM3", Me._Row.Item("KBN_NM3").ToString(), DBDataType.NVARCHAR))
        End If
        If Me._Row.Item("KBN_NM4") IsNot DBNull.Value AndAlso Me._Row.Item("KBN_NM4").ToString() <> "" Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_NM4", Me._Row.Item("KBN_NM4").ToString(), DBDataType.NVARCHAR))
        End If

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("KBN_GROUP_CD", "KBN_GROUP_CD")
        map.Add("KBN_CD", "KBN_CD")
        map.Add("KBN_KEYWORD", "KBN_KEYWORD")
        map.Add("KBN_NM1", "KBN_NM1")
        map.Add("KBN_NM2", "KBN_NM2")
        map.Add("KBN_NM3", "KBN_NM3")
        map.Add("KBN_NM4", "KBN_NM4")
        map.Add("KBN_NM5", "KBN_NM5")
        map.Add("KBN_NM6", "KBN_NM6")
        map.Add("KBN_NM7", "KBN_NM7")
        map.Add("KBN_NM8", "KBN_NM8")
        map.Add("KBN_NM9", "KBN_NM9")
        map.Add("KBN_NM10", "KBN_NM10")
        map.Add("KBN_NM11", "KBN_NM11")
        map.Add("KBN_NM12", "KBN_NM12")
        map.Add("KBN_NM13", "KBN_NM13")
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960INOUT_Z_KBN")

        Return ds

    End Function

    ''' <summary>
    ''' 運送Ｍ登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertFUnsoM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim insDataTbl As DataTable = ds.Tables("F_UNSO_M")

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_INSERT_F_UNSO_M,
                                                                       inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        For Each Me._Row In insDataTbl.Rows

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetInsertFUnsoMParameter(Me._Row, Me._SqlPrmList)
            Call Me.SetDataInsertParameter(Me._SqlPrmList)

            'パラメータの反映
            cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

            MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃タリフセットマスタ取得SQLの構築・発行</remarks>
    Private Function SelectUnchinTariffSet(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inOutTbl As DataTable = ds.Tables("LMI960INOUT_M_UNCHIN_TARIFF_SET")

        'INTableの条件rowの格納
        Me._Row = inOutTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_UNCHIN_TARIFF_SET)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Call Me.SetSelectUnchinTariffSetParameter(Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("SET_MST_CD", "SET_MST_CD")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("SET_KB", "SET_KB")
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("UNCHIN_TARIFF_CD1", "UNCHIN_TARIFF_CD1")
        map.Add("UNCHIN_TARIFF_CD2", "UNCHIN_TARIFF_CD2")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("YOKO_TARIFF_CD", "YOKO_TARIFF_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "LMI960INOUT_M_UNCHIN_TARIFF_SET")

        Return ds

    End Function

#End Region

    'ADD S 2020/02/07 010901
#Region "GLIS受注登録・削除処理"

    ''' <summary>
    ''' 受注参照キーデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>受注参照キーデータ取得SQLの構築・発行</remarks>
    Private Function SelectGLZ9300InBookUpdKey(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_GLZ9300IN_BOOK_UPD_KEY)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Me.SetSelectGLZ9300InBookUpdKeyParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JOB_NO", "JOB_NO")
        map.Add("EST_NO", "EST_NO")
        map.Add("EST_NO_EDA", "EST_NO_EDA")
        map.Add("JOB_SYS_UPD_DATE", "JOB_SYS_UPD_DATE")
        map.Add("JOB_SYS_UPD_TIME", "JOB_SYS_UPD_TIME")
        map.Add("EST_SYS_UPD_DATE", "EST_SYS_UPD_DATE")
        map.Add("EST_SYS_UPD_TIME", "EST_SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GLZ9300IN_BOOK_UPD_KEY")

        Return ds

    End Function

    ''' <summary>
    ''' 受注更新用HWLデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>受注更新用HWLデータ取得SQLの構築・発行</remarks>
    Private Function SelectGLZ9300InBookingData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_GLZ9300IN_BOOKING_DATA)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Me.SetSelectGLZ9300InBookingDataParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("LOAD_NO", "CUST_REF_NO")
        map.Add("GOODS_CD_HWL_SAP", "GOODS_CD_HWL_SAP")
        map.Add("GROSS_WEIGHT", "GROSS_WEIGHT")
        map.Add("ORAP_CNT", "ORAP_CNT")
        map.Add("FROM_CD_HWL_SAP", "PLACE_CD_A_HWL_SAP")
        map.Add("FROM_DATE", "TRUCK_DATE_A")
        map.Add("TO_CD_HWL_SAP", "STAR_PLACE_CD_HWL_SAP")
        map.Add("TO_DATE", "STAR_DATE")
        map.Add("STAR_REM", "STAR_REM")
        map.Add("TRN_ORD_NO", "TRN_ORD_NO")
        map.Add("TRUCK_ARRG_REM", "TRUCK_ARRG_REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GLZ9300IN_BOOKING_DATA")

        Return ds

    End Function

    ''' <summary>
    ''' 受注削除キーデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>受注削除キーデータ取得SQLの構築・発行</remarks>
    Private Function SelectGLZ9300InBookDelKey(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_IN)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'INTableの条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL作成
        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMI960DAC.SQL_SELECT_GLZ9300IN_BOOK_DEL_KEY)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'SQLパラメータ
        Me._SqlPrmList = New ArrayList()
        Me.SetSelectGLZ9300InBookDelKeyParameter(inTbl.Rows(0), Me._Row, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("JOB_NO", "JOB_NO")
        map.Add("EST_NO", "EST_NO")
        map.Add("EST_NO_EDA", "EST_NO_EDA")
        map.Add("JOB_SYS_UPD_DATE", "JOB_SYS_UPD_DATE")
        map.Add("JOB_SYS_UPD_TIME", "JOB_SYS_UPD_TIME")
        map.Add("EST_SYS_UPD_DATE", "EST_SYS_UPD_DATE")
        map.Add("EST_SYS_UPD_TIME", "EST_SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "GLZ9300IN_BOOK_DEL_KEY")

        Return ds

    End Function

#End Region 'GLIS受注登録・削除処理
    'ADD E 2020/02/07 010901

#Region "シリンダー取込処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)シリンダーシリアルNo更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedCylinderSerial(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_HED_CYLINDER_SERIAL_NO _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedCylinderSerial(Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

    'ADD START 2019/03/27
#Region "一括変更"
    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Stops) RequestedStartDateTime 更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiDtlStpReqStartDateTime(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_DTL_STP_REQ_START_DATE_TIME _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiDtlStpReqStartDateTimeParameter(inTblRow, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim cnt As Integer = MyBase.GetUpdateResult(cmd)
        MyBase.SetResultCount(cnt)

        Return ds

    End Function

    'ADD S 2020/02/27 010901
    ''' <summary>
    ''' RequestedStartDateTime更新時のHeader更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks>排他制御で使用しているHeaderの更新日時を更新する</remarks>
    Private Function UpdateOutkaEdiHedWhenUpdStpReqStartDateTime(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_HED_WHEN_UPD_STP_REQ_START_DATE_TIME _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedWhenUpdStpReqStartDateTimeParameter(inTblRow, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds
    End Function
    'ADD S 2020/02/27 010901

#Region "JOB NO変更処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)出荷管理番号(JOB NO)更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedOutkaCtlNo(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_OUTKAEDI_HED_OUTKA_CTL_NO _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedOutkaCtlNo(inTblRow, Me._Row, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(_SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region

#End Region
    'ADD END   2019/03/27

#Region "受注ステータス戻し処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)更新(受注ステータス戻し処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaEdiHedForRollback(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_UPDATE_HED_FOR_ROLLBACK_JUCHU_STATUS _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateOutkaEdiHedForRollbackParameter(Me._Row, inTblRow, Me._SqlPrmList)
        Call Me.SetDataUpdateParameter(Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データL物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_OUTKA_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteOutkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データM物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_OUTKA_M _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteOutkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データS物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteOutkaS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_OUTKA_S _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteOutkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 輸出データL物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteCExportL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_C_EXPORT_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteOutkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' シッピングマークHED物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteCMarkHed(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_C_MARK_HED _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteOutkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' シッピングマークDTL物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteCMarkDtl(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_C_MARK_DTL _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteOutkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データL物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_INKA_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteInkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データM物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaM(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_INKA_M _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteInkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷データS物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaS(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_INKA_S _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteInkaParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 運送L物理削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteUnsoL(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTblRow As DataRow = ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
        Dim rowNo As Integer = CInt(ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("ROW_NO"))
        Dim targetTbl As DataTable = ds.Tables(LMI960DAC.TABLE_NM_SAKUSEI_TARGET)

        '条件rowの格納
        Me._Row = targetTbl.Rows(rowNo)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI960DAC.SQL_DELETE_UNSO_L _
                                                                       , inTblRow.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetDeleteUnsoParameter(Me._Row, inTblRow, Me._SqlPrmList)

        'パラメータの反映
        cmd.Parameters.AddRange(Me._SqlPrmList.ToArray)

        MyBase.Logger.WriteSQLLog(LMI960DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetDeleteResult(cmd)

        Return ds

    End Function

#End Region '受注ステータス戻し処理

    'ADD S 2020/02/07 010901
#Region "条件句組み立て"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理のWhere条件追加
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub AppendWhereClauseSearch()

        Dim juchuStatusCodes As StringBuilder = New StringBuilder()

        If Not String.IsNullOrEmpty(Me._Row.Item("CHK_MISHORI").ToString()) Then
            '未処理(受注前)
            juchuStatusCodes.Append(",'").Append(CStr(ShinchokuKbJuchu.Mishori)).Append("'")
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("CHK_JUCHU_OK").ToString()) Then
            '受注OK
            juchuStatusCodes.Append(",'").Append(CStr(ShinchokuKbJuchu.JuchuOK)).Append("'")
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("CHK_JUCHU_NG").ToString()) Then
            '受注NG
            juchuStatusCodes.Append(",'").Append(CStr(ShinchokuKbJuchu.JuchuNG)).Append("'")
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("CHK_SHUKKA_TOUROKU_ZUMI").ToString()) Then
            '入出荷登録済
            juchuStatusCodes.Append(",'").Append(CStr(ShinchokuKbJuchu.NyuShukkaTourokuZumi)).Append("'")
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("CHK_JISSEKI_SAKUSEI_ZUMI").ToString()) Then
            '実績作成済
            juchuStatusCodes.Append(",'").Append(CStr(ShinchokuKbJuchu.JissekiSakuseiZumi)).Append("'")
        End If
        If Not String.IsNullOrEmpty(Me._Row.Item("CHK_TORIKESHI").ToString()) Then
            'EDI取消
            juchuStatusCodes.Append(",'").Append(CStr(ShinchokuKbJuchu.EdiTorikeshi)).Append("'")
        End If

        If juchuStatusCodes.Length > 0 Then
            Dim juchuStatusInClause As String = juchuStatusCodes.ToString().Substring(1)
            '進捗区分(受注ステータス)
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_SHINCHOKU_KB_JUCHU.Replace("@SHINCHOKU_KB_JUCHU", juchuStatusInClause))
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("STATUS_KB").ToString()) Then
            'ステータス区分(TMC取消)
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_STATUS_KB)
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("DEL_KB").ToString()) Then
            '削除区分(EDI保留区分)
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_DEL_KB)
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("SHINCHOKU_KB").ToString()) Then
            '進捗区分(配送ステータス)
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_SHINCHOKU_KB)
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("DELAY_STATUS").ToString()) Then
            '遅延ステータス
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_DELAY_STATUS)
        End If

        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
            Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            '倉庫の場合
            'または Chilled Lorry の場合

            If Not String.IsNullOrEmpty(Me._Row.Item("CYLINDER_SERIAL_NO").ToString()) Then
                'シリンダーシリアルNo
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_CYLINDER_SERIAL_NO)
            End If
        End If

        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.ISO Then
            'ISOの場合

            If Not String.IsNullOrEmpty(Me._Row.Item("GOODS_CD").ToString()) Then
                '商品コード
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_GOODS_CD)
            End If

            If Not String.IsNullOrEmpty(Me._Row.Item("GOODS_NM").ToString()) Then
                '商品
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_GOODS_NM)
            End If
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("SHIPMENT_ID").ToString()) Then
            'ShipmentID(Load Number)
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_SHIPMENT_ID)
        End If

        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
            Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            '倉庫の場合
            'または Chilled Lorry の場合

            Dim inOutKbCodes As StringBuilder = New StringBuilder()

            If Not String.IsNullOrEmpty(Me._Row.Item("CHK_MITEI").ToString()) Then
                '未定
                inOutKbCodes.Append(",'").Append(InOutKb.Mitei).Append("'")
            End If
            If Not String.IsNullOrEmpty(Me._Row.Item("CHK_INKA").ToString()) Then
                '入荷
                inOutKbCodes.Append(",'").Append(InOutKb.Inka).Append("'")
            End If
            If Not String.IsNullOrEmpty(Me._Row.Item("CHK_OUTKA").ToString()) Then
                '出荷
                inOutKbCodes.Append(",'").Append(InOutKb.Outka).Append("'")
            End If
            If Not String.IsNullOrEmpty(Me._Row.Item("CHK_UNSO").ToString()) Then
                '運送
                inOutKbCodes.Append(",'").Append(InOutKb.Unso).Append("'")
            End If

            If inOutKbCodes.Length > 0 Then
                Dim inOutKbInClause As String = inOutKbCodes.ToString().Substring(1)
                '入出荷区分
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_INOUT_KB.Replace("@INOUT_KB", inOutKbInClause))
            End If
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("OUTKA_CTL_NO").ToString()) Then
            '入出荷管理番号/JOB NO
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_OUTKA_CTL_NO)
        End If

        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.ISO Then
            'ISOの場合

            If Not String.IsNullOrEmpty(Me._Row.Item("SAP_ORD_NO").ToString()) Then
                'HNW SAP Order No
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_SAP_ORD_NO)
            End If
        End If

        'ADD S 2020/02/27 010901
        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
            Me._Row.Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
            '倉庫の場合
            'または Chilled Lorry の場合

            If Not String.IsNullOrEmpty(Me._Row.Item("CUST_ORD_NO").ToString()) Then
                'オーダー番号
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_CUST_ORD_NO)
            End If

            If Not String.IsNullOrEmpty(Me._Row.Item("CUST_CD_L").ToString()) Then
                '荷主コード(大)
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_CUST_CD_L)
            End If

            If Not String.IsNullOrEmpty(Me._Row.Item("CUST_CD_M").ToString()) Then
                '荷主コード(中)
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_CUST_CD_M)
            End If
        End If
        'ADD E 2020/02/27 010901
        If Not String.IsNullOrEmpty(Me._Row.Item("SHUKKA_MOTO").ToString()) Then
            '出荷元
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_SHUKKA_MOTO)
        End If

        If Not String.IsNullOrEmpty(Me._Row.Item("NONYU_SAKI").ToString()) Then
            '納入先
            Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_NONYU_SAKI)
        End If

        If Me._Row.Item("BUMON").ToString() = CmbBumonItems.ISO Then
            'ISOの場合

            If Not String.IsNullOrEmpty(Me._Row.Item("SHUKKA_MOTO_CD").ToString()) Then
                '出荷元コード
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_SHUKKA_MOTO_CD)
            End If

            If Not String.IsNullOrEmpty(Me._Row.Item("NONYU_SAKI_CD").ToString()) Then
                '納入先コード
                Me._StrSql.Append(SQL_SELECT_SEARCH_DATA_COND_NONYU_SAKI_CD)
            End If
        End If

    End Sub

#End Region

#End Region
    'ADD E 2020/02/07 010901

#Region "パラメータ設定"

#Region "システム共通項目"

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.VARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Call Me.SetDataUpdateParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataUpdateParameter(ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.VARCHAR))

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))    'ADD 2020/02/07 010901
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_DATE_FROM", .Item("SHUKKA_DATE_FROM").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_DATE_TO", .Item("SHUKKA_DATE_TO").ToString(), DBDataType.VARCHAR))

            'ADD S 2020/02/07 010901
            If Not String.IsNullOrEmpty(.Item("STATUS_KB").ToString()) Then
                'ステータス区分(TMC取消)
                prmList.Add(MyBase.GetSqlParameter("@STATUS_KB", .Item("STATUS_KB").ToString(), DBDataType.VARCHAR))
            End If

            If Not String.IsNullOrEmpty(.Item("DEL_KB").ToString()) Then
                '削除区分(EDI保留区分)
                prmList.Add(MyBase.GetSqlParameter("@DEL_KB", .Item("DEL_KB").ToString(), DBDataType.VARCHAR))
            End If

            If Not String.IsNullOrEmpty(.Item("SHINCHOKU_KB").ToString()) Then
                '進捗区分(配送ステータス)
                prmList.Add(MyBase.GetSqlParameter("@SHINCHOKU_KB", .Item("SHINCHOKU_KB").ToString(), DBDataType.VARCHAR))
            End If

            If Not String.IsNullOrEmpty(.Item("DELAY_STATUS").ToString()) Then
                '遅延ステータス
                prmList.Add(MyBase.GetSqlParameter("@DELAY_STATUS", .Item("DELAY_STATUS").ToString(), DBDataType.VARCHAR))
            End If

            If .Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
                .Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
                '倉庫の場合
                'または Chilled Lorry の場合

                If Not String.IsNullOrEmpty(.Item("CYLINDER_SERIAL_NO").ToString()) Then
                    'シリンダーシリアルNo
                    prmList.Add(MyBase.GetSqlParameter("@CYLINDER_SERIAL_NO", String.Concat("%", .Item("CYLINDER_SERIAL_NO").ToString(), "%"), DBDataType.VARCHAR))
                End If
            End If

            If .Item("BUMON").ToString() = CmbBumonItems.ISO Then
                'ISOの場合

                If Not String.IsNullOrEmpty(.Item("GOODS_CD").ToString()) Then
                    '商品コード
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_CD", String.Concat(.Item("GOODS_CD").ToString(), "%"), DBDataType.VARCHAR))
                End If

                If Not String.IsNullOrEmpty(.Item("GOODS_NM").ToString()) Then
                    '商品
                    prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", String.Concat("%", .Item("GOODS_NM").ToString(), "%"), DBDataType.VARCHAR))
                End If
            End If

            If Not String.IsNullOrEmpty(.Item("SHIPMENT_ID").ToString()) Then
                'ShipmentID(Load Number)
                prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", String.Concat(.Item("SHIPMENT_ID").ToString(), "%"), DBDataType.VARCHAR))
            End If

            If .Item("BUMON").ToString() = CmbBumonItems.ISO Then
                'ISOの場合

                If Not String.IsNullOrEmpty(.Item("SAP_ORD_NO").ToString()) Then
                    'HNW SAP Order No
                    prmList.Add(MyBase.GetSqlParameter("@SAP_ORD_NO", String.Concat(.Item("SAP_ORD_NO").ToString(), "%"), DBDataType.VARCHAR))
                End If
            End If

            If Not String.IsNullOrEmpty(.Item("OUTKA_CTL_NO").ToString()) Then
                '入出荷管理番号/JOB NO
                prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", String.Concat(.Item("OUTKA_CTL_NO").ToString(), "%"), DBDataType.VARCHAR))
            End If

            'ADD S 2020/02/27 010901
            If .Item("BUMON").ToString() = CmbBumonItems.Soko OrElse
                .Item("BUMON").ToString() = CmbBumonItems.ChilledLorry Then
                '倉庫の場合
                'または Chilled Lorry の場合

                If Not String.IsNullOrEmpty(.Item("CUST_ORD_NO").ToString()) Then
                    'オーダー番号
                    prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", String.Concat("%", .Item("CUST_ORD_NO").ToString(), "%"), DBDataType.VARCHAR))
                End If

                If Not String.IsNullOrEmpty(.Item("CUST_CD_L").ToString()) Then
                    '荷主コード(大)
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", String.Concat(.Item("CUST_CD_L").ToString(), "%"), DBDataType.VARCHAR))
                End If

                If Not String.IsNullOrEmpty(.Item("CUST_CD_M").ToString()) Then
                    '荷主コード(中)
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", String.Concat(.Item("CUST_CD_M").ToString(), "%"), DBDataType.VARCHAR))
                End If
            End If
            'ADD E 2020/02/27 010901

            If Not String.IsNullOrEmpty(.Item("SHUKKA_MOTO").ToString()) Then
                '出荷元
                prmList.Add(MyBase.GetSqlParameter("@SHUKKA_MOTO", String.Concat("%", .Item("SHUKKA_MOTO").ToString(), "%"), DBDataType.VARCHAR))
            End If

            If Not String.IsNullOrEmpty(.Item("NONYU_SAKI").ToString()) Then
                '納入先
                prmList.Add(MyBase.GetSqlParameter("@NONYU_SAKI", String.Concat("%", .Item("NONYU_SAKI").ToString(), "%"), DBDataType.VARCHAR))
            End If
            'ADD E 2020/02/07 010901

            If .Item("BUMON").ToString() = CmbBumonItems.ISO Then
                'ISOの場合

                If Not String.IsNullOrEmpty(.Item("SHUKKA_MOTO_CD").ToString()) Then
                    '出荷元コード
                    prmList.Add(MyBase.GetSqlParameter("@SHUKKA_MOTO_CD", String.Concat(.Item("SHUKKA_MOTO_CD").ToString(), "%"), DBDataType.VARCHAR))
                End If

                If Not String.IsNullOrEmpty(.Item("NONYU_SAKI_CD").ToString()) Then
                    '納入先コード
                    prmList.Add(MyBase.GetSqlParameter("@NONYU_SAKI_CD", String.Concat(.Item("NONYU_SAKI_CD").ToString(), "%"), DBDataType.VARCHAR))
                End If
            End If
        End With

    End Sub

#End Region

#Region "実績作成処理"

    ''' <summary>
    '''  同一ShipmentIDデータ件数検索のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetShipmentIDParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ検索のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectOutkaEdiDataParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim shinchokuKb As String = String.Empty
        Select Case inTblRow.Item("BASHO_KB").ToString()
            Case CmbBashoKbItems.Tsumikomi
                shinchokuKb = CStr(LMI960DAC.ShinchokuKb.Misoushin)
            Case CmbBashoKbItems.NonyuYotei
                shinchokuKb = CStr(LMI960DAC.ShinchokuKb.PickZumi)
            Case CmbBashoKbItems.Nioroshi
                shinchokuKb = CStr(LMI960DAC.ShinchokuKb.NonyuYotei)
        End Select

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHUKKA_DATE", .Item("SHUKKA_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHINCHOKU_KB", shinchokuKb, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
            'ADD START 2019/03/27
            prmList.Add(MyBase.GetSqlParameter("@STP_GYO", .Item("STP_GYO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_UPD_DATE", .Item("STP_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_UPD_TIME", .Item("STP_UPD_TIME").ToString(), DBDataType.CHAR))
            'ADD END   2019/03/27

        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ登録(積込場・荷下場用)のパラメータ設定
    ''' </summary>
    ''' <param name="sakuseiKb"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdiParameter(ByVal sakuseiKb As Integer, ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        Dim gyo As String
        Dim eventStatus As String
        Dim eventDateTime As String

        If sakuseiKb = JissekiSakuseiKb.Arrival Then
            '到着
            Select Case inTblRow.Item("BASHO_KB").ToString()
                Case CmbBashoKbItems.Tsumikomi
                    gyo = CStr(CInt(HOutkaediHedHwlGyo.ArrivalTsumikomi))   '"1"
                    eventStatus = "X3"
                Case CmbBashoKbItems.Nioroshi
                    gyo = CStr(CInt(HOutkaediHedHwlGyo.ArrivalNioroshi))    '"3"
                    eventStatus = "X1"
                Case Else
                    Return
            End Select
            eventDateTime = conditionRow.Item("EVENT_DATE").ToString() & inTblRow.Item("ARRIVAL_TIME").ToString() & "00"
        ElseIf sakuseiKb = JissekiSakuseiKb.Departure Then
            '出発
            Select Case inTblRow.Item("BASHO_KB").ToString()
                Case CmbBashoKbItems.Tsumikomi
                    gyo = CStr(CInt(HOutkaediHedHwlGyo.DepartureTsumikomi)) '"2"
                    eventStatus = "AF"
                Case CmbBashoKbItems.Nioroshi
                    gyo = CStr(CInt(HOutkaediHedHwlGyo.DepartureNioroshi))  '"4"
                    eventStatus = "D1"
                Case Else
                    Return
            End Select
            eventDateTime = conditionRow.Item("EVENT_DATE").ToString() & inTblRow.Item("DEPARTURE_TIME").ToString() & "00"
        Else
            Return
        End If

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", gyo, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "1", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", .Item("SHIPMENT_REF_NUM_MISC").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", .Item("STOP_SEQ_NUM").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", .Item("LOCATION_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", eventStatus, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", "NS", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", eventDateTime, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", .Item("CITY").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", .Item("COUNTRY").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", .Item("ZIP_CODE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", .Item("TRACTOR_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", .Item("TRAILER_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", .Item("PRO_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", "", DBDataType.VARCHAR))  'ADD 2020/03/06 011377
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(出荷ピック)データ登録(納入予定用)のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdi2Parameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        Dim eventDateTime As String = conditionRow.Item("EVENT_DATE").ToString() & inTblRow.Item("ARRIVAL_TIME").ToString() & "00"

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(CInt(HOutkaediHedHwlGyo.NonyuYotei)), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "1", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", .Item("SHIPMENT_REF_NUM_MISC").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", .Item("STOP_SEQ_NUM").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", .Item("LOCATION_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", "AB", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", "NS", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", eventDateTime, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", .Item("CITY").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", .Item("COUNTRY").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", .Item("ZIP_CODE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", .Item("TRACTOR_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", .Item("TRAILER_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", .Item("PRO_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", "", DBDataType.VARCHAR))  'ADD 2020/03/06 011377
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)進捗区分更新のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedShinchokuParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim shinchokuKbAfter As String
        Dim shinchokuKbBefore As String

        Select Case inTblRow.Item("BASHO_KB").ToString()
            Case CmbBashoKbItems.Tsumikomi
                shinchokuKbBefore = CStr(ShinchokuKb.Misoushin)
                shinchokuKbAfter = CStr(ShinchokuKb.PickZumi)
            Case CmbBashoKbItems.NonyuYotei
                shinchokuKbBefore = CStr(ShinchokuKb.PickZumi)
                shinchokuKbAfter = CStr(ShinchokuKb.NonyuYotei)
            Case CmbBashoKbItems.Nioroshi
                shinchokuKbBefore = CStr(ShinchokuKb.NonyuYotei)
                shinchokuKbAfter = CStr(ShinchokuKb.NioroshiZumi)
            Case Else
                Return
        End Select

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHINCHOKU_KB_AFTER", shinchokuKbAfter, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHINCHOKU_KB_BEFORE", shinchokuKbBefore, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

    'ADD S 2019/12/12 009741
#Region "受注作成処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ存在チェックのパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectCountSendJuchuDataParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(CInt(HOutkaediHedHwlGyo.OrderResponse990)), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ検索(受注作成用存在チェック)のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectCountOutkaEdiDataJuchuParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("OUTKA_CTL_NO").ToString(), DBDataType.VARCHAR))  'ADD 2020/03/06 011377
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdiJuchuParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        Dim accept As String
        Dim decline As String
        If inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString = CStr(CInt(ShinchokuKbJuchu.JuchuOK)) Then
            accept = "1"
            decline = "0"
        Else
            accept = "0"
            decline = "1"
        End If

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(CInt(HOutkaediHedHwlGyo.OrderResponse990)), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", accept, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", decline, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", inTblRow.Item("SHIPMENT_DECLINE_REASON").ToString, DBDataType.VARCHAR))  'ADD 2020/03/06 011377
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

    'ADD S 2020/03/06 011377
    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(214)のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdiJuchuParameter214(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            Dim tractorNumPrefix As String = ""
            Select Case .Item("INOUT_KB").ToString
                Case LMI960DAC.InOutKb.Inka
                    tractorNumPrefix = "I"
                Case LMI960DAC.InOutKb.Outka
                    tractorNumPrefix = "O"
                Case LMI960DAC.InOutKb.Unso
                    tractorNumPrefix = "U"
            End Select

            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(CInt(HOutkaediHedHwlGyo.OrderResponse214)), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "1", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", .Item("CYLINDER_SERIAL_NO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", tractorNumPrefix & .Item("OUTKA_CTL_NO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub
    'ADD E 2020/03/06 011377

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)進捗区分(受注ステータス)更新のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedShinchokuJuchuParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHINCHOKU_KB_JUCHU_AFTER", inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            'ADD S 2020/02/07 010901
            If inTblRow.Item("SHINCHOKU_KB_JUCHU").ToString = CStr(CInt(ShinchokuKbJuchu.JissekiSakuseiZumi)) _
            AndAlso inTblRow.Item("BASHO_KB").ToString() = CmbBashoKbItems.Nioroshi Then
                prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
            Else
                'ADD E 2020/02/07 010901
                prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
            End If  'ADD 2020/02/07 010901
        End With

    End Sub

#End Region
    'ADD E 2019/12/12 009741

#Region "遅延送信処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(遅延理由)データ存在チェックのパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectCountSendDelayDataParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim gyo As String
        If inTblRow.Item("SHIPMENT_DELAY_SHUBETSU").ToString = CmbDelayShubetsuItems.Shukka Then
            gyo = CStr(CInt(HOutkaediHedHwlGyo.ShukkaDelay))
        Else
            gyo = CStr(CInt(HOutkaediHedHwlGyo.NonyuDelay))
        End If

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", gyo, DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(遅延理由)データ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdiDelayParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        Dim gyo As String
        If inTblRow.Item("SHIPMENT_DELAY_SHUBETSU").ToString = CmbDelayShubetsuItems.Shukka Then
            gyo = CStr(CInt(HOutkaediHedHwlGyo.ShukkaDelay))
        Else
            gyo = CStr(CInt(HOutkaediHedHwlGyo.NonyuDelay))
        End If

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", .Item("SHIPMENT_DELAY_REASON"), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REASON_DESC", .Item("SHIPMENT_DELAY_HOSOKU"), DBDataType.VARCHAR))
        End With

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", gyo, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "1", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", .Item("SHIPMENT_REF_NUM_MISC").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", .Item("STOP_SEQ_NUM").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", .Item("LOCATION_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", "SD", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", .Item("CITY").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", .Item("COUNTRY").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", .Item("ZIP_CODE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", .Item("TRACTOR_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", .Item("TRAILER_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", .Item("PRO_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

#End Region

#Region "Booked解除送信処理"

    ''' <summary>
    '''     ''' 処理対象範囲の行の H_OUTKAEDI_HED_HWL の SYS_ENT_DATE, SYS_ENT_TIME を取得する SQL のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="row"></param>
    ''' <param name="prmList"></param>
    Private Sub SetSelectHedSpmSendoutediByPkeyParameter(ByVal conditionRow As DataRow, ByVal row As Integer, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter(String.Concat("@CRT_DATE", "_", row.ToString()), .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter(String.Concat("@FILE_NAME", "_", row.ToString()), .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 対象行と 同一の Load Number で、対象行より後に受信し、かつ
    ''' 応答レコードがある H_OUTKAEDI_HED_HWL の CRT_DATE, FILE_NAME を取得する SQL のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    Private Sub SetSelectHedSpmSendoutediByShipmentIdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(HOutkaediHedHwlGyo.OrderResponse990), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(同一 LoadNumber(SHIPMENT_ID) の(注文応答990または注文応答214(受注OK)))データ存在チェック SQL のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectSendoutediCountByShipmentIdGyo990Or214MiParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(HOutkaediHedHwlGyo.OrderResponse990), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO_2", CStr(HOutkaediHedHwlGyo.OrderResponse214), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 対象行と 同一の Load Number で、対象行より前に受信し、かつ
    ''' 応答レコードがある納入日情報と、対象行の納入日情報の取得 SQL
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    Private Sub SetSelectSendoutediOutkaediHedDtlStpParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(HOutkaediHedHwlGyo.OrderResponse990), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
        End With

    End Sub
    ''' <summary>
    ''' ハネウェルＥＤＩ送信の(注文応答990取消または注文応答214(受注OK)取消))データ存在チェック(件数取得) SQL のパラメータ設定
    ''' </summary>
    Private Sub SetSelectSendoutediCountByPkeyParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList,
                                                        ByVal gyo As HOutkaediHedHwlGyo, ByVal gyo2 As HOutkaediHedHwlGyo)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(gyo), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO_2", CStr(gyo2), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(990 取消)のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdiJuchuCancelParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(HOutkaediHedHwlGyo.OrderResponse990Cancel), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", .Item("SHIPMENT_ACCEPT").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", .Item("SHIPMENT_DECLINE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", .Item("SHIPMENT_DECLINE_REASON").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ送信(受注可否)データ登録(214 取消)のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertSendOutEdiJuchu214CancelParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GYO", CStr(CInt(HOutkaediHedHwlGyo.OrderResponse214Cancel)), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CREAT_DATE_TIME", Left(MyBase.GetSystemDate() & MyBase.GetSystemTime(), 14), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ACCEPT", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE", "0", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_STATUS", "1", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_REF_NUM_MISC", .Item("SHIPMENT_REF_NUM_MISC").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOCATION_ID", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_STATUS", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EVENT_DATE_TIME", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CITY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@COUNTRY", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZIP_CODE", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@EXPRESS_CARRIER_CODE", "T5355162", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRACTOR_NUMBER", .Item("TRACTOR_NUMBER").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRAILER_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRO_NUMBER", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_DECLINE_REASON", "", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JISSEKI_SHORI_FLG", "2", DBDataType.VARCHAR))
        End With

    End Sub

#End Region ' "Booked解除送信処理"

    'ADD S 2020/02/27 010901
#Region "荷主振り分け処理"

    ''' <summary>
    ''' 荷主振り分け用Stop->City取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectStpCityForSpecifyCustParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 荷主振り分け用荷主コード取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectCustCdBySkuNumberParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)荷主コード更新のパラメータ設定
    ''' </summary>
    ''' <param name="custTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedCustParameter(ByVal custTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With custTblRow
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.VARCHAR))
        End With
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region '荷主振り分け処理

#Region "出荷登録処理"

    ''' <summary>
    ''' 入出荷登録の元情報（Header、ShipmentDetails、Stops）取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectHedSpmStpForInsInOutkaParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 入出荷登録の元情報（Commodity）取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectCmdForInsInOutkaParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 入出荷登録用荷主情報取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectMCustForInsInOutkaParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", "00", DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", "00", DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 入出荷登録用届先情報取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectMDestForInsInOutkaParameter(ByVal ds As DataSet, ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim inoutKb As String = ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("INOUT_KB").ToString

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
            Select Case inoutKb
                Case LMI960DAC.InOutKb.Inka
                    prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("INKA_ORIG_CD").ToString(), DBDataType.VARCHAR))
                Case LMI960DAC.InOutKb.Outka
                    prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("LOCATION_ID").ToString(), DBDataType.VARCHAR))
            End Select
        End With

    End Sub

    ''' <summary>
    ''' 入出荷登録用商品情報取得のパラメータ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectMGoodsForInsInOutkaParameter(ByVal ds As DataSet, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SEARCH_KEY_2", .Item("SKU_NUMBER").ToString(), DBDataType.VARCHAR))
            If Not String.IsNullOrEmpty(.Item("GOODS_CD_NRS").ToString()) Then
                prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.VARCHAR))
            End If
        End With
        With ds.Tables("LMI960M_CUST").Rows(0)
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 入出荷登録用ハネウェルＥＤＩ受信データ(Header)更新のパラメータ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedForInsInOutkaParameter(ByVal ds As DataSet, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        Dim inoutKb As String = ds.Tables(LMI960DAC.TABLE_NM_PROC_CTRL).Rows(0).Item("INOUT_KB").ToString

        prmList.Add(MyBase.GetSqlParameter("@DEL_KB", "0", DBDataType.VARCHAR)) '0:正常
        With ds.Tables(LMI960DAC.TABLE_NM_IN).Rows(0)
            prmList.Add(MyBase.GetSqlParameter("@SHINCHOKU_KB_JUCHU", .Item("SHINCHOKU_KB_JUCHU").ToString, DBDataType.VARCHAR))
        End With
        prmList.Add(MyBase.GetSqlParameter("@INOUT_KB", inoutKb, DBDataType.VARCHAR))
        Select Case inoutKb
            Case LMI960DAC.InOutKb.Inka
                prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", ds.Tables("LMI960IN_INKA_L").Rows(0).Item("INKA_NO_L").ToString, DBDataType.VARCHAR))
            Case LMI960DAC.InOutKb.Outka
                prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", ds.Tables("LMI960IN_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString, DBDataType.VARCHAR))
            Case LMI960DAC.InOutKb.Unso
                prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", ds.Tables("F_UNSO_L").Rows(0).Item("UNSO_NO_L").ToString, DBDataType.VARCHAR))
        End Select
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 出荷データＬ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertCOutkaLParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_KB", "10", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYUBETU_KB", "10", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_STATE_KB", "10", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKAHOKOKU_YN", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PICK_KB", "01", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_KANRYO_INFO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_DATE", .Item("OUTKO_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", "01", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@END_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_L", .Item("SHIP_CD_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIP_CD_M", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_3", .Item("DEST_AD_3").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_TEL", .Item("DEST_TEL").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SP_NHS_KB", .Item("SP_NHS_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO", .Item("CUST_ORD_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Item("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DENP_YN", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PC_KB", "01", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", "01", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_KB", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_NM", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_1", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_AD_2", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALL_PRINT_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIHUDA_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NHS_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DENP_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKOKU_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PICK_FLAG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PRINT_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@MATOME_PRINT_TIME", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@LAST_PRINT_TIME", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SASZ_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKO_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOU_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ORDER_TYPE", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KENPIN_WK_STATUS", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", .Item("WH_TAB_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@URGENT_YN", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_SIJI_REMARK", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_NO_SIJI_FLG", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_HOKOKU_YN", "00", DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_HOKOKU", String.Empty, DBDataType.NVARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 出荷データＭ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertCOutkaMParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_M", .Item("OUTKA_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@EDI_SET_NO", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@COA_YN", .Item("COA_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_ORD_NO_DTL", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_DTL", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@RSV_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SERIAL_NO", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_KB", .Item("ALCTD_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_PKG_NB", .Item("OUTKA_PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_HASU", .Item("OUTKA_HASU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_QT", .Item("OUTKA_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_NB", .Item("OUTKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_TTL_QT", .Item("OUTKA_TTL_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_NB", "0", DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ALCTD_QT", "0", DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_NB", .Item("BACKLOG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BACKLOG_QT", .Item("BACKLOG_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_M_PKG_NB", .Item("OUTKA_M_PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZAIKO_KB", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SOURCE_CD", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@YELLOW_CARD", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS_FROM", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", "99", DBDataType.NUMERIC))
        End With

    End Sub

#End Region
    'ADD E 2020/02/27 010901

#Region "入荷登録処理"

    ''' <summary>
    ''' 入荷データＬ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertBInkaLParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("INKA_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@FURI_NO", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TP", .Item("INKA_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_KB", .Item("INKA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_STATE_KB", .Item("INKA_STATE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_DATE", .Item("INKA_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STORAGE_DUE_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_CD", .Item("WH_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT", .Item("INKA_PLAN_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@INKA_PLAN_QT_UT", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKA_TTL_NB", .Item("INKA_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@BUYER_ORD_NO_L", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_FROM_ORD_NO_L", .Item("OUTKA_FROM_ORD_NO_L").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TOUKI_HOKAN_YN", .Item("TOUKI_HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_YN", .Item("HOKAN_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_FREE_KIKAN", .Item("HOKAN_FREE_KIKAN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@HOKAN_STR_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NIYAKU_YN", .Item("NIYAKU_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK_OUT", String.Empty, DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHECKLIST_PRT_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKELIST_PRT_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKELIST_PRT_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKE_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UKETSUKE_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@KEN_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@INKO_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_DATE", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HOUKOKUSYO_PR_USER", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_TP", .Item("UNCHIN_TP").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KB", .Item("UNCHIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_KENPIN_WK_STATUS", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_STATUS", .Item("WH_TAB_STATUS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_YN", .Item("WH_TAB_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_IMP_YN", .Item("WH_TAB_IMP_YN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STOP_ALLOC", String.Empty, DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@WH_TAB_NO_SIJI_FLG", .Item("WH_TAB_NO_SIJI_FLG").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 運送Ｌ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="drProcCtrlData"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertFUnsoLParameter(ByVal conditionRow As DataRow, ByVal drProcCtrlData As DataRow, ByVal prmList As ArrayList)

        Select Case drProcCtrlData("INOUT_KB").ToString
            Case LMI960DAC.InOutKb.Inka
                With conditionRow
                    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DENP_NO", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_KBN", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", .Item("UNSO_PKG_NB").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@NB_UT", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@REMARK", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AD_3", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AREA_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", String.Empty, DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@MAIN_DELI_KB", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", String.Empty, DBDataType.NVARCHAR))
                End With

            Case LMI960DAC.InOutKb.Unso
                With conditionRow
                    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@JIYU_KB", .Item("JIYU_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DENP_NO", .Item("DENP_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_KBN", .Item("AUTO_DENP_KBN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO", .Item("AUTO_DENP_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_DATE", .Item("OUTKA_PLAN_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_PLAN_TIME", .Item("OUTKA_PLAN_TIME").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_TIME", .Item("ARR_PLAN_TIME").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_ACT_TIME", .Item("ARR_ACT_TIME").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", .Item("CUST_REF_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIP_CD", .Item("SHIP_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ORIG_CD", .Item("ORIG_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_PKG_NB", .Item("UNSO_PKG_NB").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_WT", .Item("UNSO_WT").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@PC_KB", .Item("PC_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", .Item("TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@VCLE_KB", .Item("VCLE_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", .Item("MOTO_DATA_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AD_3", .Item("AD_3").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_TEHAI_KB", .Item("UNSO_TEHAI_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BUY_CHU_NO", .Item("BUY_CHU_NO").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AREA_CD", .Item("AREA_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", .Item("TYUKEI_HAISO_FLG").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@MAIN_DELI_KB", "", DBDataType.NVARCHAR))
                End With

        End Select

    End Sub

#End Region

#Region "運送登録処理"

    ''' <summary>
    ''' 運送L初期値取得（運送登録用）のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectUnsoLSourceParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTblRow.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_DATE", .Item("SHUKKA_DATE").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 商品明細マスタ取得のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectMGoodsDetailsParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SUB_KB", .Item("SUB_KB").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 運送Ｍ登録のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetInsertFUnsoMParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_CD_NRS", .Item("GOODS_CD_NRS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GOODS_NM", .Item("GOODS_NM").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_NB", .Item("UNSO_TTL_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@NB_UT", .Item("NB_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_TTL_QT", .Item("UNSO_TTL_QT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@QT_UT", .Item("QT_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HASU", .Item("HASU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@ZAI_REC_NO", .Item("ZAI_REC_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", .Item("UNSO_ONDO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@IRIME", .Item("IRIME").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@IRIME_UT", .Item("IRIME_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BETU_WT", .Item("BETU_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("ZBUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("ABUKA_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PKG_NB", .Item("PKG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@LOT_NO", .Item("LOT_NO").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))
        End With

    End Sub

    ''' <summary>
    ''' 運賃タリフセットマスタ取得のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectUnchinTariffSetParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SET_KB", .Item("SET_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DEST_CD", .Item("DEST_CD").ToString(), DBDataType.NVARCHAR))
        End With

    End Sub

#End Region

    'ADD S 2020/02/07 010901
#Region "GLIS受注登録・削除処理"

    ''' <summary>
    ''' 受注参照キーデータ取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectGLZ9300InBookUpdKeyParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 受注更新用HWLデータ取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectGLZ9300InBookingDataParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 受注削除キーデータ取得のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectGLZ9300InBookDelKeyParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region 'GLIS受注登録・削除処理
    'ADD E 2020/02/07 010901

#Region "JOB NO変更処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)OUTKA_CTL_NO(JOB NO)更新のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedCylinderSerial(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CYLINDER_SERIAL_NO", .Item("CYLINDER_SERIAL_NO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

#Region "一括変更処理"
    'ADD START 2019/03/27
    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Stops) RequestedStartDateTime 更新のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiDtlStpReqStartDateTimeParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            Dim stopSeqNum As String
            Dim changeValue As String
            Select Case .Item("IKKATSU_CHANGE_ITEM").ToString()
                Case CmbIkkatsuChangeItems.ShukkaDate
                    stopSeqNum = "1"
                    changeValue = .Item("CHANGE_VALUE").ToString() & "000000"
                Case CmbIkkatsuChangeItems.NonyuDate
                    stopSeqNum = "2"
                    changeValue = .Item("CHANGE_VALUE").ToString() & "000000"
                Case Else
                    Return
            End Select

            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", stopSeqNum, DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CHANGE_VALUE", changeValue, DBDataType.VARCHAR))
        End With

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_GYO", .Item("STP_GYO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_UPD_DATE", .Item("STP_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_UPD_TIME", .Item("STP_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub
    'ADD END   2019/03/27
    'ADD S 2020/02/27 010901
    ''' <summary>
    ''' RequestedStartDateTime更新時のHeader更新のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedWhenUpdStpReqStartDateTimeParameter(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            Dim stopSeqNum As String
            Select Case .Item("IKKATSU_CHANGE_ITEM").ToString()
                Case CmbIkkatsuChangeItems.ShukkaDate
                    stopSeqNum = "1"
                Case CmbIkkatsuChangeItems.NonyuDate
                    stopSeqNum = "2"
                Case Else
                    Return
            End Select

            prmList.Add(MyBase.GetSqlParameter("@STOP_SEQ_NUM", stopSeqNum, DBDataType.VARCHAR))
        End With

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@SHIPMENT_ID", .Item("SHIPMENT_ID").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_GYO", .Item("STP_GYO").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@STP_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        End With

    End Sub
    'ADD E 2020/02/27 010901

#End Region

#Region "受注ステータス戻し処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)更新(受注ステータス戻し処理)のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedForRollbackParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE_BEFORE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME_BEFORE", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

    ''' <summary>
    ''' 出荷データ物理削除のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetDeleteOutkaParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("OUTKA_CTL_NO_DELETED").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 入荷データ物理削除のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetDeleteInkaParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.VARCHAR))
        End With

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@INKA_NO_L", .Item("OUTKA_CTL_NO_DELETED").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

    ''' <summary>
    ''' 運送データ物理削除のパラメータ設定
    ''' </summary>
    ''' <param name="conditionRow"></param>
    ''' <param name="inTblRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetDeleteUnsoParameter(ByVal conditionRow As DataRow, ByVal inTblRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("OUTKA_CTL_NO_DELETED").ToString(), DBDataType.VARCHAR))
        End With

    End Sub

#End Region '受注ステータス戻し処理

#Region "JOB NO変更処理"

    ''' <summary>
    ''' ハネウェルＥＤＩ受信データ(Header)OUTKA_CTL_NO(JOB NO)更新のパラメータ設定
    ''' </summary>
    ''' <param name="inTblRow"></param>
    ''' <param name="conditionRow"></param>
    ''' <param name="prmList"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateOutkaEdiHedOutkaCtlNo(ByVal inTblRow As DataRow, ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With inTblRow
            prmList.Add(MyBase.GetSqlParameter("@OUTKA_CTL_NO", .Item("CHANGE_VALUE").ToString(), DBDataType.VARCHAR))
        End With

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@CRT_DATE", .Item("HED_CRT_DATE").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@FILE_NAME", .Item("HED_FILE_NAME").ToString(), DBDataType.VARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_DATE", .Item("HED_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@HED_UPD_TIME", .Item("HED_UPD_TIME").ToString(), DBDataType.CHAR))
        End With

    End Sub

#End Region

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
