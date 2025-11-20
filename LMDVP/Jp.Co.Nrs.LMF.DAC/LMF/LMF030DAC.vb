' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF030    : 運行編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF030DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF030DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF030IN"

    '2022.09.06 追加START
    ''' <summary>
    ''' INテーブル車輌マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_CAR As String = "LMF030IN_CAR"

    ''' <summary>
    ''' OUTテーブル車輌マスタ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT_CAR As String = "LMF030OUT_CAR"
    '2022.09.06 追加END

    ''' <summary>
    ''' F_UNSO_LLテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_LL As String = "F_UNSO_LL"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' F_SHIHARAI_TRSテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SHIHARAI As String = "F_SHIHARAI_TRS"

    ''' <summary>
    ''' IN_KEISANテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN_KEISAN As String = "LMF030IN_KEISAN"
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    Private Const TABLE_NM_KBN As String = "Z_KBN"

    Private Const TABLE_NM_TASYA_WH_NM As String = "TASYA_WH_NM"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMF030DAC"

    '要望番号1269 2012.07.12 追加START umano
    ''' <summary>
    ''' F_UNSO_LL(配送区分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NORMAL As Integer = 0
    Private Const SYUKA As Integer = 1
    Private Const TYUKEI As Integer = 2
    Private Const HAIKA As Integer = 3
    Private Const HAISOKBN_CNT As Integer = 3       '配送区分カウント数(空白は含まない)
    '要望番号1269 2012.07.12 追加END umano

    ''' <summary>
    ''' 検索パターン
    ''' </summary>
    ''' <remarks>
    ''' パターン1：通常検索
    ''' パターン2：新規(運行)
    ''' パターン3：新規(運送)⇒パラメータ設定無し
    ''' パターン4：乗務員取得(運行)
    ''' </remarks>
    Private Enum SelectCondition As Integer
        PTN1
        PTN2
        PTN3
        '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
        PTN4
        '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 End
    End Enum

#End Region

#Region "検索処理 SQL"

#Region "UNSO_LL0"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    'Private Const SQL_SELECT_LL0 As String = "SELECT                                              " & vbNewLine _
    '                                       & " F01_01.NRS_BR_CD         AS      NRS_BR_CD         " & vbNewLine _
    '                                       & ",''                       AS      TRIP_NO           " & vbNewLine _
    '                                       & ",F01_01.TRIP_DATE         AS      TRIP_DATE         " & vbNewLine _
    '                                       & ",F01_01.BIN_KB            AS      BIN_KB            " & vbNewLine _
    '                                       & ",''                       AS      CAR_KEY           " & vbNewLine _
    '                                       & ",''                       AS      CAR_NO            " & vbNewLine _
    '                                       & ",''                       AS      CAR_TP_KB         " & vbNewLine _
    '                                       & ",0                        AS      ONDO_MM           " & vbNewLine _
    '                                       & ",0                        AS      ONDO_MX           " & vbNewLine _
    '                                       & ",0                        AS      LOAD_WT           " & vbNewLine _
    '                                       & ",''                       AS      INSPC_DATE_TRUCK  " & vbNewLine _
    '                                       & ",''                       AS      INSPC_DATE_TRAILER" & vbNewLine _
    '                                       & ",''                       AS      CAR_TP_NM         " & vbNewLine _
    '                                       & ",KBN_01.KBN_NM3           AS      UNSOCO_CD         " & vbNewLine _
    '                                       & ",KBN_01.KBN_NM4           AS      UNSOCO_BR_CD      " & vbNewLine _
    '                                       & ",M38_01.UNSOCO_NM         AS      UNSOCO_NM         " & vbNewLine _
    '                                       & ",M38_01.UNSOCO_BR_NM      AS      UNSOCO_BR_NM      " & vbNewLine _
    '                                       & ",''                       AS      JSHA_KB           " & vbNewLine _
    '                                       & ",''                       AS      HAISO_KB          " & vbNewLine _
    '                                       & ",''                       AS      DRIVER_CD         " & vbNewLine _
    '                                       & ",''                       AS      DRIVER_NM         " & vbNewLine _
    '                                       & ",''                       AS      REMARK            " & vbNewLine _
    '                                       & ",0                        AS      UNSO_ONDO         " & vbNewLine _
    '                                       & ",0                        AS      PAY_UNCHIN        " & vbNewLine _
    '                                       & ",0                        AS      UNSO_PKG_NB       " & vbNewLine _
    '                                       & ",0                        AS      UNSO_WT           " & vbNewLine _
    '                                       & ",0                        AS      DECI_UNCHIN       " & vbNewLine _
    '                                       & ",''                       AS      PAY_TARIFF_CD     " & vbNewLine _
    '                                       & ",''                       AS      SYS_UPD_DATE      " & vbNewLine _
    '                                       & ",''                       AS      SYS_UPD_TIME      " & vbNewLine _
    '                                       & "FROM (                                              " & vbNewLine _
    '                                       & "        SELECT                                      " & vbNewLine _
    '                                       & "               @NRS_BR_CD     AS NRS_BR_CD          " & vbNewLine _
    '                                       & "              ,@ARR_PLAN_DATE AS TRIP_DATE          " & vbNewLine _
    '                                       & "              ,@BIN_KB        AS BIN_KB             " & vbNewLine _
    '                                       & "      )                       F01_01                " & vbNewLine _
    '                                       & "LEFT  JOIN $LM_MST$..Z_KBN    KBN_01                " & vbNewLine _
    '                                       & "  ON  F01_01.NRS_BR_CD      = KBN_01.KBN_CD         " & vbNewLine _
    '                                       & " AND  KBN_01.KBN_GROUP_CD   = 'N017'                " & vbNewLine _
    '                                       & " AND  KBN_01.SYS_DEL_FLG    = '0'                   " & vbNewLine _
    '                                       & "LEFT  JOIN $LM_MST$..M_UNSOCO M38_01                " & vbNewLine _
    '                                       & "  ON  F01_01.NRS_BR_CD      = M38_01.NRS_BR_CD      " & vbNewLine _
    '                                       & " AND  KBN_01.KBN_NM3        = M38_01.UNSOCO_CD      " & vbNewLine _
    '                                       & " AND  KBN_01.KBN_NM4        = M38_01.UNSOCO_BR_CD   " & vbNewLine _
    '                                       & " AND  M38_01.SYS_DEL_FLG    = '0'                   " & vbNewLine
    Private Const SQL_SELECT_LL0 As String = "SELECT                                              " & vbNewLine _
                                           & " F01_01.NRS_BR_CD         AS      NRS_BR_CD         " & vbNewLine _
                                           & ",''                       AS      TRIP_NO           " & vbNewLine _
                                           & ",F01_01.TRIP_DATE         AS      TRIP_DATE         " & vbNewLine _
                                           & ",F01_01.BIN_KB            AS      BIN_KB            " & vbNewLine _
                                           & ",''                       AS      CAR_KEY           " & vbNewLine _
                                           & ",''                       AS      CAR_NO            " & vbNewLine _
                                           & ",''                       AS      CAR_TP_KB         " & vbNewLine _
                                           & ",0                        AS      ONDO_MM           " & vbNewLine _
                                           & ",0                        AS      ONDO_MX           " & vbNewLine _
                                           & ",0                        AS      LOAD_WT           " & vbNewLine _
                                           & ",''                       AS      INSPC_DATE_TRUCK  " & vbNewLine _
                                           & ",''                       AS      INSPC_DATE_TRAILER" & vbNewLine _
                                           & ",''                       AS      CAR_TP_NM         " & vbNewLine _
                                           & ",KBN_01.KBN_NM3           AS      UNSOCO_CD         " & vbNewLine _
                                           & ",KBN_01.KBN_NM4           AS      UNSOCO_BR_CD      " & vbNewLine _
                                           & ",M38_01.UNSOCO_NM         AS      UNSOCO_NM         " & vbNewLine _
                                           & ",M38_01.UNSOCO_BR_NM      AS      UNSOCO_BR_NM      " & vbNewLine _
                                           & ",''                       AS      JSHA_KB           " & vbNewLine _
                                           & ",''                       AS      HAISO_KB          " & vbNewLine _
                                           & ",''                       AS      DRIVER_CD         " & vbNewLine _
                                           & ",''                       AS      DRIVER_NM         " & vbNewLine _
                                           & ",''                       AS      REMARK            " & vbNewLine _
                                           & ",0                        AS      UNSO_ONDO         " & vbNewLine _
                                           & ",0                        AS      PAY_UNCHIN        " & vbNewLine _
                                           & ",0                        AS      UNSO_PKG_NB       " & vbNewLine _
                                           & ",0                        AS      UNSO_WT           " & vbNewLine _
                                           & ",0                        AS      DECI_UNCHIN       " & vbNewLine _
                                           & ",''                       AS      PAY_TARIFF_CD     " & vbNewLine _
                                           & ",''                       AS      SYS_UPD_DATE      " & vbNewLine _
                                           & ",''                       AS      SYS_UPD_TIME      " & vbNewLine _
                                           & ",''                       AS      SHIHARAI_TARIFF_CD" & vbNewLine _
                                           & ",''                       AS      SHIHARAI_ETARIFF_CD" & vbNewLine _
                                           & ",0                        AS      SHIHARAI_UNSO_WT  " & vbNewLine _
                                           & ",0                        AS      SHIHARAI_COUNT    " & vbNewLine _
                                           & ",0                        AS      SHIHARAI_UNCHIN   " & vbNewLine _
                                           & ",''                       AS      SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加START              " & vbNewLine _
                                           & ",''                       AS      TEHAI_SYUBETSU    " & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加END                " & vbNewLine _
                                           & "FROM (                                              " & vbNewLine _
                                           & "        SELECT                                      " & vbNewLine _
                                           & "               @NRS_BR_CD     AS NRS_BR_CD          " & vbNewLine _
                                           & "              ,@ARR_PLAN_DATE AS TRIP_DATE          " & vbNewLine _
                                           & "              ,@BIN_KB        AS BIN_KB             " & vbNewLine _
                                           & "      )                       F01_01                " & vbNewLine _
                                           & "LEFT  JOIN $LM_MST$..Z_KBN    KBN_01                " & vbNewLine _
                                           & "  ON  F01_01.NRS_BR_CD      = KBN_01.KBN_CD         " & vbNewLine _
                                           & " AND  KBN_01.KBN_GROUP_CD   = 'N017'                " & vbNewLine _
                                           & " AND  KBN_01.SYS_DEL_FLG    = '0'                   " & vbNewLine _
                                           & "LEFT  JOIN $LM_MST$..M_UNSOCO M38_01                " & vbNewLine _
                                           & "  ON  F01_01.NRS_BR_CD      = M38_01.NRS_BR_CD      " & vbNewLine _
                                           & " AND  KBN_01.KBN_NM3        = M38_01.UNSOCO_CD      " & vbNewLine _
                                           & " AND  KBN_01.KBN_NM4        = M38_01.UNSOCO_BR_CD   " & vbNewLine _
                                           & " AND  M38_01.SYS_DEL_FLG    = '0'                   " & vbNewLine
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "UNSO_LL1"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    'Private Const SQL_SELECT_LL1 As String = "SELECT                                                " & vbNewLine _
    '                                       & " F01_01.NRS_BR_CD           AS      NRS_BR_CD         " & vbNewLine _
    '                                       & ",F01_01.TRIP_NO             AS      TRIP_NO           " & vbNewLine _
    '                                       & ",F01_01.TRIP_DATE           AS      TRIP_DATE         " & vbNewLine _
    '                                       & ",F01_01.BIN_KB              AS      BIN_KB            " & vbNewLine _
    '                                       & ",F01_01.CAR_KEY             AS      CAR_KEY           " & vbNewLine _
    '                                       & ",M39_01.CAR_NO              AS      CAR_NO            " & vbNewLine _
    '                                       & ",M39_01.CAR_TP_KB           AS      CAR_TP_KB         " & vbNewLine _
    '                                       & ",M39_01.ONDO_MM             AS      ONDO_MM           " & vbNewLine _
    '                                       & ",M39_01.ONDO_MX             AS      ONDO_MX           " & vbNewLine _
    '                                       & ",M39_01.LOAD_WT             AS      LOAD_WT           " & vbNewLine _
    '                                       & ",M39_01.INSPC_DATE_TRUCK    AS      INSPC_DATE_TRUCK  " & vbNewLine _
    '                                       & ",M39_01.INSPC_DATE_TRAILER  AS      INSPC_DATE_TRAILER" & vbNewLine _
    '                                       & ",KBN_01.KBN_NM1             AS      CAR_TP_NM         " & vbNewLine _
    '                                       & ",F01_01.UNSOCO_CD           AS      UNSOCO_CD         " & vbNewLine _
    '                                       & ",F01_01.UNSOCO_BR_CD        AS      UNSOCO_BR_CD      " & vbNewLine _
    '                                       & ",M38_01.UNSOCO_NM           AS      UNSOCO_NM         " & vbNewLine _
    '                                       & ",M38_01.UNSOCO_BR_NM        AS      UNSOCO_BR_NM      " & vbNewLine _
    '                                       & ",F01_01.JSHA_KB             AS      JSHA_KB           " & vbNewLine _
    '                                       & ",F01_01.HAISO_KB            AS      HAISO_KB          " & vbNewLine _
    '                                       & ",F01_01.DRIVER_CD           AS      DRIVER_CD         " & vbNewLine _
    '                                       & ",M37_01.DRIVER_NM           AS      DRIVER_NM         " & vbNewLine _
    '                                       & ",F01_01.REMARK              AS      REMARK            " & vbNewLine _
    '                                       & ",F01_01.UNSO_ONDO           AS      UNSO_ONDO         " & vbNewLine _
    '                                       & ",F01_01.PAY_UNCHIN          AS      PAY_UNCHIN        " & vbNewLine _
    '                                       & ",'0'                        AS      UNSO_PKG_NB       " & vbNewLine _
    '                                       & ",'0'                        AS      UNSO_WT           " & vbNewLine _
    '                                       & ",'0'                        AS      DECI_UNCHIN       " & vbNewLine _
    '                                       & ",F01_01.PAY_TARIFF_CD       AS      PAY_TARIFF_CD     " & vbNewLine _
    '                                       & ",F01_01.SYS_UPD_DATE        AS      SYS_UPD_DATE      " & vbNewLine _
    '                                       & ",F01_01.SYS_UPD_TIME        AS      SYS_UPD_TIME      " & vbNewLine _
    '                                       & " FROM      $LM_TRN$..F_UNSO_LL F01_01                 " & vbNewLine _
    '                                       & " LEFT JOIN $LM_MST$..M_VCLE    M39_01                 " & vbNewLine _
    '                                       & "   ON F01_01.CAR_KEY         = M39_01.CAR_KEY         " & vbNewLine _
    '                                       & "  AND M39_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
    '                                       & " LEFT JOIN $LM_MST$..M_UNSOCO  M38_01                 " & vbNewLine _
    '                                       & "   ON F01_01.NRS_BR_CD       = M38_01.NRS_BR_CD       " & vbNewLine _
    '                                       & "  AND F01_01.UNSOCO_CD       = M38_01.UNSOCO_CD       " & vbNewLine _
    '                                       & "  AND F01_01.UNSOCO_BR_CD    = M38_01.UNSOCO_BR_CD    " & vbNewLine _
    '                                       & "  AND M38_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
    '                                       & " LEFT JOIN $LM_MST$..M_DRIVER  M37_01                 " & vbNewLine _
    '                                       & "   ON F01_01.DRIVER_CD       = M37_01.DRIVER_CD       " & vbNewLine _
    '                                       & "  AND M37_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
    '                                       & " LEFT JOIN $LM_MST$..Z_KBN     KBN_01                 " & vbNewLine _
    '                                       & "   ON M39_01.CAR_TP_KB       = KBN_01.KBN_CD          " & vbNewLine _
    '                                       & "  AND KBN_01.KBN_GROUP_CD    = 'S023'                 " & vbNewLine _
    '                                       & "  AND KBN_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
    '                                       & "WHERE F01_01.TRIP_NO         = @TRIP_NO               " & vbNewLine _
    '                                       & "  AND F01_01.SYS_DEL_FLG     = '0'                    " & vbNewLine
    Private Const SQL_SELECT_LL1_1 As String = "SELECT                                                " & vbNewLine _
                                           & " F01_01.NRS_BR_CD           AS      NRS_BR_CD         " & vbNewLine _
                                           & ",F01_01.TRIP_NO             AS      TRIP_NO           " & vbNewLine _
                                           & ",F01_01.TRIP_DATE           AS      TRIP_DATE         " & vbNewLine _
                                           & ",F01_01.BIN_KB              AS      BIN_KB            " & vbNewLine _
                                           & ",F01_01.CAR_KEY             AS      CAR_KEY           " & vbNewLine _
                                           & ",M39_01.CAR_NO              AS      CAR_NO            " & vbNewLine _
                                           & ",M39_01.CAR_TP_KB           AS      CAR_TP_KB         " & vbNewLine _
                                           & ",M39_01.ONDO_MM             AS      ONDO_MM           " & vbNewLine _
                                           & ",M39_01.ONDO_MX             AS      ONDO_MX           " & vbNewLine _
                                           & ",M39_01.LOAD_WT             AS      LOAD_WT           " & vbNewLine _
                                           & ",M39_01.INSPC_DATE_TRUCK    AS      INSPC_DATE_TRUCK  " & vbNewLine _
                                           & ",M39_01.INSPC_DATE_TRAILER  AS      INSPC_DATE_TRAILER" & vbNewLine _
                                           & ",KBN_01.KBN_NM1             AS      CAR_TP_NM         " & vbNewLine _
                                           & ",F01_01.UNSOCO_CD           AS      UNSOCO_CD         " & vbNewLine _
                                           & ",F01_01.UNSOCO_BR_CD        AS      UNSOCO_BR_CD      " & vbNewLine _
                                           & ",M38_01.UNSOCO_NM           AS      UNSOCO_NM         " & vbNewLine _
                                           & ",M38_01.UNSOCO_BR_NM        AS      UNSOCO_BR_NM      " & vbNewLine _
                                           & ",F01_01.JSHA_KB             AS      JSHA_KB           " & vbNewLine _
                                           & ",F01_01.HAISO_KB            AS      HAISO_KB          " & vbNewLine _
                                           & ",F01_01.DRIVER_CD           AS      DRIVER_CD         " & vbNewLine _
                                           & ",M37_01.DRIVER_NM           AS      DRIVER_NM         " & vbNewLine _
                                           & ",F01_01.REMARK              AS      REMARK            " & vbNewLine _
                                           & ",F01_01.UNSO_ONDO           AS      UNSO_ONDO         " & vbNewLine _
                                           & ",F01_01.PAY_UNCHIN          AS      PAY_UNCHIN        " & vbNewLine _
                                           & ",'0'                        AS      UNSO_PKG_NB       " & vbNewLine _
                                           & ",'0'                        AS      UNSO_WT           " & vbNewLine _
                                           & ",'0'                        AS      DECI_UNCHIN       " & vbNewLine _
                                           & ",F01_01.PAY_TARIFF_CD       AS      PAY_TARIFF_CD     " & vbNewLine _
                                           & ",F01_01.SYS_UPD_DATE        AS      SYS_UPD_DATE      " & vbNewLine _
                                           & ",F01_01.SYS_UPD_TIME        AS      SYS_UPD_TIME      " & vbNewLine _
                                           & ",F01_01.SHIHARAI_TARIFF_CD  AS      SHIHARAI_TARIFF_CD" & vbNewLine _
                                           & ",F01_01.SHIHARAI_ETARIFF_CD AS      SHIHARAI_ETARIFF_CD" & vbNewLine _
                                           & ",F01_01.SHIHARAI_UNSO_WT    AS      SHIHARAI_UNSO_WT  " & vbNewLine _
                                           & ",F01_01.SHIHARAI_COUNT      AS      SHIHARAI_COUNT    " & vbNewLine _
                                           & ",F01_01.SHIHARAI_UNCHIN     AS      SHIHARAI_UNCHIN   " & vbNewLine _
                                           & ",F01_01.SHIHARAI_TARIFF_BUNRUI_KB AS SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加START                " & vbNewLine _
                                           & ",ISNULL(MAIN.TEHAI_SYUBETSU,'')   AS TEHAI_SYUBETSU   " & vbNewLine _
                                           & "  --要望番号2063(2013.10.21) 追加END                  " & vbNewLine _
                                           & " FROM                                                 " & vbNewLine
    Private Const SQL_SELECT_LL1_2 As String = " F01_01                 " & vbNewLine _
                                           & " LEFT JOIN $LM_MST$..M_VCLE    M39_01                 " & vbNewLine _
                                           & "   ON F01_01.CAR_KEY         = M39_01.CAR_KEY         " & vbNewLine _
                                           & "  AND M39_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
                                           & " LEFT JOIN $LM_MST$..M_UNSOCO  M38_01                 " & vbNewLine _
                                           & "   ON F01_01.NRS_BR_CD       = M38_01.NRS_BR_CD       " & vbNewLine _
                                           & "  AND F01_01.UNSOCO_CD       = M38_01.UNSOCO_CD       " & vbNewLine _
                                           & "  AND F01_01.UNSOCO_BR_CD    = M38_01.UNSOCO_BR_CD    " & vbNewLine _
                                           & "  AND M38_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
                                           & " LEFT JOIN $LM_MST$..M_DRIVER  M37_01                 " & vbNewLine _
                                           & "   ON F01_01.DRIVER_CD       = M37_01.DRIVER_CD       " & vbNewLine _
                                           & "  AND M37_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
                                           & " LEFT JOIN $LM_MST$..Z_KBN     KBN_01                 " & vbNewLine _
                                           & "   ON M39_01.CAR_TP_KB       = KBN_01.KBN_CD          " & vbNewLine _
                                           & "  AND KBN_01.KBN_GROUP_CD    = 'S023'                 " & vbNewLine _
                                           & "  AND KBN_01.SYS_DEL_FLG     = '0'                    " & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加START                " & vbNewLine _
                                           & " LEFT JOIN                                                                           " & vbNewLine _
                                           & " (SELECT                                                                             " & vbNewLine _
                                           & "  MAIN_TEHAI.NRS_BR_CD                                 AS NRS_BR_CD                  " & vbNewLine _
                                           & " ,MAIN_TEHAI.TRIP_NO                                   AS TRIP_NO                    " & vbNewLine _
                                           & " ,MAIN_TEHAI.TEHAI_SYUBETSU                            AS TEHAI_SYUBETSU             " & vbNewLine _
                                           & " ,MAIN_TEHAI.SYS_UPD_DATE + MAIN_TEHAI.SYS_UPD_TIME    AS SYS_UPD_MAX                " & vbNewLine _
                                           & "  FROM                                                                               " & vbNewLine _
                                           & " $LM_TRN$..H_TEHAIINFO_TBL MAIN_TEHAI                                                " & vbNewLine _
                                           & " ,(SELECT                                                                            " & vbNewLine _
                                           & "    NRS_BR_CD                                                                        " & vbNewLine _
                                           & "   ,TRIP_NO                                                                          " & vbNewLine _
                                           & "   ,MAX(SYS_UPD_DATE + SYS_UPD_TIME) AS SYS_UPD_MAX                                  " & vbNewLine _
                                           & "    FROM                                                                             " & vbNewLine _
                                           & "    $LM_TRN$..H_TEHAIINFO_TBL                                                        " & vbNewLine _
                                           & "    WHERE                                                                            " & vbNewLine _
                                           & "    TRIP_NO = @TRIP_NO                                                               " & vbNewLine _
                                           & "    -- AND SYS_DEL_FLG = '0'                                                         " & vbNewLine _
                                           & "    GROUP BY                                                                         " & vbNewLine _
                                           & "    NRS_BR_CD                                                                        " & vbNewLine _
                                           & "   ,TRIP_NO                                                                          " & vbNewLine _
                                           & "   )THI_TBL                                                                          " & vbNewLine _
                                           & "   WHERE                                                                             " & vbNewLine _
                                           & "       MAIN_TEHAI.NRS_BR_CD = THI_TBL.NRS_BR_CD                                      " & vbNewLine _
                                           & "   AND MAIN_TEHAI.TRIP_NO   = THI_TBL.TRIP_NO                                        " & vbNewLine _
                                           & "   AND MAIN_TEHAI.SYS_UPD_DATE + MAIN_TEHAI.SYS_UPD_TIME = THI_TBL.SYS_UPD_MAX       " & vbNewLine _
                                           & "  ) MAIN                                                                             " & vbNewLine _
                                           & "  ON F01_01.NRS_BR_CD       = MAIN.NRS_BR_CD                                         " & vbNewLine _
                                           & " AND F01_01.TRIP_NO         = MAIN.TRIP_NO                                           " & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加END                " & vbNewLine _
                                           & "WHERE F01_01.TRIP_NO         = @TRIP_NO               " & vbNewLine _
                                           & "  AND F01_01.SYS_DEL_FLG     = '0'                    " & vbNewLine
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "UNSO_LL2"

    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''車番から直近の乗務員を取得する
    'Private Const SQL_SELECT_LL2 As String = "SELECT                                                    " & vbNewLine _
    '                                       & " F01_01.NRS_BR_CD             AS      NRS_BR_CD           " & vbNewLine _
    '                                       & ",F01_01.TRIP_NO               AS      TRIP_NO             " & vbNewLine _
    '                                       & ",F01_01.TRIP_DATE             AS      TRIP_DATE           " & vbNewLine _
    '                                       & ",F01_01.BIN_KB                AS      BIN_KB              " & vbNewLine _
    '                                       & ",F01_01.CAR_KEY               AS      CAR_KEY             " & vbNewLine _
    '                                       & ",''                           AS      CAR_NO              " & vbNewLine _
    '                                       & ",''                           AS      CAR_TP_KB           " & vbNewLine _
    '                                       & ",''                           AS      ONDO_MM             " & vbNewLine _
    '                                       & ",''                           AS      ONDO_MX             " & vbNewLine _
    '                                       & ",''                           AS      LOAD_WT             " & vbNewLine _
    '                                       & ",''                           AS      INSPC_DATE_TRUCK    " & vbNewLine _
    '                                       & ",''                           AS      INSPC_DATE_TRAILER  " & vbNewLine _
    '                                       & ",''                           AS      CAR_TP_NM           " & vbNewLine _
    '                                       & ",F01_01.UNSOCO_CD             AS      UNSOCO_CD           " & vbNewLine _
    '                                       & ",F01_01.UNSOCO_BR_CD          AS      UNSOCO_BR_CD        " & vbNewLine _
    '                                       & ",''                           AS      UNSOCO_NM           " & vbNewLine _
    '                                       & ",''                           AS      UNSOCO_BR_NM        " & vbNewLine _
    '                                       & ",F01_01.JSHA_KB               AS      JSHA_KB             " & vbNewLine _
    '                                       & ",F01_01.HAISO_KB              AS      HAISO_KB            " & vbNewLine _
    '                                       & ",F01_01.DRIVER_CD             AS      DRIVER_CD           " & vbNewLine _
    '                                       & ",M37_01.DRIVER_NM             AS      DRIVER_NM           " & vbNewLine _
    '                                       & ",F01_01.REMARK                AS      REMARK              " & vbNewLine _
    '                                       & ",F01_01.UNSO_ONDO             AS      UNSO_ONDO           " & vbNewLine _
    '                                       & ",F01_01.PAY_UNCHIN            AS      PAY_UNCHIN          " & vbNewLine _
    '                                       & ",'0'                          AS      UNSO_PKG_NB         " & vbNewLine _
    '                                       & ",'0'                          AS      UNSO_WT             " & vbNewLine _
    '                                       & ",'0'                          AS      DECI_UNCHIN         " & vbNewLine _
    '                                       & ",F01_01.PAY_TARIFF_CD         AS      PAY_TARIFF_CD       " & vbNewLine _
    '                                       & ",F01_01.SYS_UPD_DATE          AS      SYS_UPD_DATE        " & vbNewLine _
    '                                       & ",F01_01.SYS_UPD_TIME          AS      SYS_UPD_TIME        " & vbNewLine _
    '                                       & " FROM      $LM_TRN$..F_UNSO_LL F01_01                     " & vbNewLine _
    '                                       & " LEFT JOIN $LM_MST$..M_DRIVER  M37_01                     " & vbNewLine _
    '                                       & "   ON F01_01.DRIVER_CD        = M37_01.DRIVER_CD          " & vbNewLine _
    '                                       & "  AND M37_01.SYS_DEL_FLG      = '0'                       " & vbNewLine _
    '                                       & "WHERE F01_01.NRS_BR_CD        = @NRS_BR_CD                " & vbNewLine _
    '                                       & "  AND F01_01.CAR_KEY          = @CAR_KEY                  " & vbNewLine _
    '                                       & "  AND F01_01.SYS_DEL_FLG      = '0'                       " & vbNewLine _
    '                                       & "ORDER BY F01_01.TRIP_NO DESC                              " & vbNewLine
    '車番から直近の乗務員を取得する
    Private Const SQL_SELECT_LL2_1 As String = "SELECT                                                    " & vbNewLine _
                                           & " F01_01.NRS_BR_CD             AS      NRS_BR_CD           " & vbNewLine _
                                           & ",F01_01.TRIP_NO               AS      TRIP_NO             " & vbNewLine _
                                           & ",F01_01.TRIP_DATE             AS      TRIP_DATE           " & vbNewLine _
                                           & ",F01_01.BIN_KB                AS      BIN_KB              " & vbNewLine _
                                           & ",F01_01.CAR_KEY               AS      CAR_KEY             " & vbNewLine _
                                           & ",''                           AS      CAR_NO              " & vbNewLine _
                                           & ",''                           AS      CAR_TP_KB           " & vbNewLine _
                                           & ",''                           AS      ONDO_MM             " & vbNewLine _
                                           & ",''                           AS      ONDO_MX             " & vbNewLine _
                                           & ",''                           AS      LOAD_WT             " & vbNewLine _
                                           & ",''                           AS      INSPC_DATE_TRUCK    " & vbNewLine _
                                           & ",''                           AS      INSPC_DATE_TRAILER  " & vbNewLine _
                                           & ",''                           AS      CAR_TP_NM           " & vbNewLine _
                                           & ",F01_01.UNSOCO_CD             AS      UNSOCO_CD           " & vbNewLine _
                                           & ",F01_01.UNSOCO_BR_CD          AS      UNSOCO_BR_CD        " & vbNewLine _
                                           & ",''                           AS      UNSOCO_NM           " & vbNewLine _
                                           & ",''                           AS      UNSOCO_BR_NM        " & vbNewLine _
                                           & ",F01_01.JSHA_KB               AS      JSHA_KB             " & vbNewLine _
                                           & ",F01_01.HAISO_KB              AS      HAISO_KB            " & vbNewLine _
                                           & ",F01_01.DRIVER_CD             AS      DRIVER_CD           " & vbNewLine _
                                           & ",M37_01.DRIVER_NM             AS      DRIVER_NM           " & vbNewLine _
                                           & ",F01_01.REMARK                AS      REMARK              " & vbNewLine _
                                           & ",F01_01.UNSO_ONDO             AS      UNSO_ONDO           " & vbNewLine _
                                           & ",F01_01.PAY_UNCHIN            AS      PAY_UNCHIN          " & vbNewLine _
                                           & ",'0'                          AS      UNSO_PKG_NB         " & vbNewLine _
                                           & ",'0'                          AS      UNSO_WT             " & vbNewLine _
                                           & ",'0'                          AS      DECI_UNCHIN         " & vbNewLine _
                                           & ",F01_01.PAY_TARIFF_CD         AS      PAY_TARIFF_CD       " & vbNewLine _
                                           & ",F01_01.SYS_UPD_DATE          AS      SYS_UPD_DATE        " & vbNewLine _
                                           & ",F01_01.SYS_UPD_TIME          AS      SYS_UPD_TIME        " & vbNewLine _
                                           & ",F01_01.SHIHARAI_TARIFF_CD    AS      SHIHARAI_TARIFF_CD  " & vbNewLine _
                                           & ",F01_01.SHIHARAI_ETARIFF_CD   AS      SHIHARAI_ETARIFF_CD " & vbNewLine _
                                           & ",F01_01.SHIHARAI_UNSO_WT      AS      SHIHARAI_UNSO_WT    " & vbNewLine _
                                           & ",F01_01.SHIHARAI_COUNT        AS      SHIHARAI_COUNT      " & vbNewLine _
                                           & ",F01_01.SHIHARAI_UNCHIN       AS      SHIHARAI_UNCHIN     " & vbNewLine _
                                           & ",F01_01.SHIHARAI_TARIFF_BUNRUI_KB AS SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加START                    " & vbNewLine _
                                           & ",''                           AS      TEHAI_SYUBETSU      " & vbNewLine _
                                           & "  --要望番号2063(2015.05.27) 追加END                      " & vbNewLine _
                                           & " FROM                                                 " & vbNewLine
    Private Const SQL_SELECT_LL2_2 As String = " F01_01                 " & vbNewLine _
                                           & " LEFT JOIN $LM_MST$..M_DRIVER  M37_01                     " & vbNewLine _
                                           & "   ON F01_01.DRIVER_CD        = M37_01.DRIVER_CD          " & vbNewLine _
                                           & "  AND M37_01.SYS_DEL_FLG      = '0'                       " & vbNewLine _
                                           & "WHERE F01_01.NRS_BR_CD        = @NRS_BR_CD                " & vbNewLine _
                                           & "  AND F01_01.CAR_KEY          = @CAR_KEY                  " & vbNewLine _
                                           & "  AND F01_01.SYS_DEL_FLG      = '0'                       " & vbNewLine _
                                           & "ORDER BY F01_01.TRIP_NO DESC                              " & vbNewLine
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 End

#End Region

#Region "UNSO_L"

    'START YANAI 要望番号376
    'Private Const SQL_SELECT_L As String = "SELECT                                                           " & vbNewLine _
    '                                     & " F02_01.NRS_BR_CD             AS      NRS_BR_CD                  " & vbNewLine _
    '                                     & ",F02_01.UNSO_NO_L             AS      UNSO_NO_L                  " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO_SYUKA         AS      TRIP_NO_SYUKA              " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO_TYUKEI        AS      TRIP_NO_TYUKEI             " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO_HAIKA         AS      TRIP_NO_HAIKA              " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO               AS      TRIP_NO                    " & vbNewLine _
    '                                     & ",F02_01.BIN_KB                AS      BIN_KB                     " & vbNewLine _
    '                                     & ",KBN_01.KBN_NM1               AS      BIN_NM                     " & vbNewLine _
    '                                     & ",F02_01.AREA_CD               AS      AREA_CD                    " & vbNewLine _
    '                                     & ",M36_01.AREA_NM               AS      AREA_NM                    " & vbNewLine _
    '                                     & ",F02_01.ARR_PLAN_DATE         AS      ARR_PLAN_DATE              " & vbNewLine _
    '                                     & ",F02_01.ORIG_CD               AS      ORIG_CD                    " & vbNewLine _
    '                                     & ",M10_01.DEST_NM               AS      ORIG_NM                    " & vbNewLine _
    '                                     & ",M10_01.JIS                   AS      ORIG_JIS_CD                " & vbNewLine _
    '                                     & ",M10_01.AD_1                  AS      ORIG_AD_1                  " & vbNewLine _
    '                                     & ",F02_01.DEST_CD               AS      DEST_CD                    " & vbNewLine _
    '                                     & ",M10_02.DEST_NM               AS      DEST_NM                    " & vbNewLine _
    '                                     & ",M10_02.JIS                   AS      DEST_JIS_CD                " & vbNewLine _
    '                                     & ",M10_02.AD_1                  AS      DEST_AD_1                  " & vbNewLine _
    '                                     & ",F02_01.UNSO_PKG_NB           AS      UNSO_PKG_NB                " & vbNewLine _
    '                                     & ",F02_01.UNSO_WT               AS      UNSO_WT                    " & vbNewLine _
    '                                     & ",ISNULL(F04_01.DECI_UNCHIN,0) AS      UNCHIN                     " & vbNewLine _
    '                                     & ",F02_01.CUST_CD_L             AS      CUST_CD_L                  " & vbNewLine _
    '                                     & ",F02_01.CUST_CD_M             AS      CUST_CD_M                  " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_L             AS      CUST_NM_L                  " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_M             AS      CUST_NM_M                  " & vbNewLine _
    '                                     & ",F02_01.CUST_REF_NO           AS      CUST_REF_NO                " & vbNewLine _
    '                                     & ",F02_01.INOUTKA_NO_L          AS      INOUTKA_NO_L               " & vbNewLine _
    '                                     & ",F02_01.MOTO_DATA_KB          AS      MOTO_DATA_KB               " & vbNewLine _
    '                                     & ",KBN_02.KBN_NM1               AS      MOTO_DATA_NM               " & vbNewLine _
    '                                     & ",F02_01.REMARK                AS      REMARK                     " & vbNewLine _
    '                                     & ",F02_01.TARIFF_BUNRUI_KB      AS      TARIFF_BUNRUI_KB           " & vbNewLine _
    '                                     & ",KBN_03.KBN_NM1               AS      TARIFF_BUNRUI_NM           " & vbNewLine _
    '                                     & ",F02_01.UNSO_CD               AS      UNSO_CD                    " & vbNewLine _
    '                                     & ",F02_01.UNSO_BR_CD            AS      UNSO_BR_CD                 " & vbNewLine _
    '                                     & ",M38_01.UNSOCO_NM             AS      UNSO_NM                    " & vbNewLine _
    '                                     & ",M38_01.UNSOCO_BR_NM          AS      UNSO_BR_NM                 " & vbNewLine _
    '                                     & ",F02_01.UNSO_ONDO_KB          AS      UNSO_ONDO_KB               " & vbNewLine _
    '                                     & ",KBN_04.KBN_NM1               AS      UNSO_ONDO_NM               " & vbNewLine _
    '                                     & ",F02_01.TYUKEI_HAISO_FLG      AS      TYUKEI_HAISO_FLG           " & vbNewLine _
    '                                     & ",F02_01.SYS_UPD_DATE          AS      SYS_UPD_DATE               " & vbNewLine _
    '                                     & ",F02_01.SYS_UPD_TIME          AS      SYS_UPD_TIME               " & vbNewLine _
    '                                     & ",@UP_KBN                      AS      UP_KBN                     " & vbNewLine _
    '                                     & ",F02_01.SYS_DEL_FLG           AS      SYS_DEL_FLG                " & vbNewLine _
    '                                     & " FROM       $LM_TRN$..F_UNSO_L     F02_01                        " & vbNewLine _
    '                                     & " LEFT  JOIN (                                                    " & vbNewLine _
    '                                     & "                SELECT                                           " & vbNewLine _
    '                                     & "                        SUM(    F04_01.DECI_UNCHIN               " & vbNewLine _
    '                                     & "                              + F04_01.DECI_CITY_EXTC            " & vbNewLine _
    '                                     & "                              + F04_01.DECI_WINT_EXTC            " & vbNewLine _
    '                                     & "                              + F04_01.DECI_RELY_EXTC            " & vbNewLine _
    '                                     & "                              + F04_01.DECI_TOLL                 " & vbNewLine _
    '                                     & "                              + F04_01.DECI_INSU                 " & vbNewLine _
    '                                     & "                            )                      AS DECI_UNCHIN" & vbNewLine _
    '                                     & "                               ,F04_01.NRS_BR_CD   AS NRS_BR_CD  " & vbNewLine _
    '                                     & "                               ,F04_01.UNSO_NO_L   AS UNSO_NO_L  " & vbNewLine _
    '                                     & "                  FROM $LM_TRN$..F_UNCHIN_TRS F04_01             " & vbNewLine _
    '                                     & "                 GROUP BY F04_01.NRS_BR_CD                       " & vbNewLine _
    '                                     & "                         ,F04_01.UNSO_NO_L                       " & vbNewLine _
    '                                     & "       )                           F04_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = F04_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.UNSO_NO_L          = F04_01.UNSO_NO_L              " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_DEST       M10_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M10_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M10_01.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.ORIG_CD            = M10_01.DEST_CD                " & vbNewLine _
    '                                     & "  AND  M10_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_DEST       M10_02                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M10_02.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M10_02.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.DEST_CD            = M10_02.DEST_CD                " & vbNewLine _
    '                                     & "  AND  M10_02.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_AREA       M36_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M36_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.AREA_CD            = M36_01.AREA_CD                " & vbNewLine _
    '                                     & "  AND  M10_02.JIS                = M36_01.JIS_CD                 " & vbNewLine _
    '                                     & "  AND  M36_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_CUST       M07_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M07_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M07_01.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_M          = M07_01.CUST_CD_M              " & vbNewLine _
    '                                     & "  AND  M07_01.CUST_CD_S          = '00'                          " & vbNewLine _
    '                                     & "  AND  M07_01.CUST_CD_SS         = '00'                          " & vbNewLine _
    '                                     & "  AND  M07_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_UNSOCO     M38_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M38_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.UNSO_CD            = M38_01.UNSOCO_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.UNSO_BR_CD         = M38_01.UNSOCO_BR_CD           " & vbNewLine _
    '                                     & "  AND  M38_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.BIN_KB             = KBN_01.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_01.KBN_GROUP_CD       = 'U001'                        " & vbNewLine _
    '                                     & "  AND  KBN_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_02                        " & vbNewLine _
    '                                     & "   ON  F02_01.MOTO_DATA_KB       = KBN_02.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_02.KBN_GROUP_CD       = 'M004'                        " & vbNewLine _
    '                                     & "  AND  KBN_02.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_03                        " & vbNewLine _
    '                                     & "   ON  F02_01.TARIFF_BUNRUI_KB   = KBN_03.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_03.KBN_GROUP_CD       = 'T015'                        " & vbNewLine _
    '                                     & "  AND  KBN_03.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_04                        " & vbNewLine _
    '                                     & "   ON  F02_01.UNSO_ONDO_KB       = KBN_04.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_04.KBN_GROUP_CD       = 'U006'                        " & vbNewLine _
    '                                     & "  AND  KBN_04.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & "WHERE  F02_01.SYS_DEL_FLG        = '0'                           " & vbNewLine
    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    'Private Const SQL_SELECT_L As String = "SELECT                                                           " & vbNewLine _
    '                                     & " F02_01.NRS_BR_CD             AS      NRS_BR_CD                  " & vbNewLine _
    '                                     & ",F02_01.UNSO_NO_L             AS      UNSO_NO_L                  " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO_SYUKA         AS      TRIP_NO_SYUKA              " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO_TYUKEI        AS      TRIP_NO_TYUKEI             " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO_HAIKA         AS      TRIP_NO_HAIKA              " & vbNewLine _
    '                                     & ",F02_01.TRIP_NO               AS      TRIP_NO                    " & vbNewLine _
    '                                     & ",F02_01.BIN_KB                AS      BIN_KB                     " & vbNewLine _
    '                                     & ",KBN_01.KBN_NM1               AS      BIN_NM                     " & vbNewLine _
    '                                     & ",F02_01.AREA_CD               AS      AREA_CD                    " & vbNewLine _
    '                                     & ",M36_01.AREA_NM               AS      AREA_NM                    " & vbNewLine _
    '                                     & ",F02_01.ARR_PLAN_DATE         AS      ARR_PLAN_DATE              " & vbNewLine _
    '                                     & ",F02_01.ORIG_CD               AS      ORIG_CD                    " & vbNewLine _
    '                                     & ",ISNULL(M10_01.DEST_NM,M10_03.DEST_NM) AS      ORIG_NM           " & vbNewLine _
    '                                     & ",ISNULL(M10_01.JIS,M10_03.JIS) AS      ORIG_JIS_CD               " & vbNewLine _
    '                                     & ",ISNULL(M10_01.AD_1,M10_03.AD_1) AS      ORIG_AD_1               " & vbNewLine _
    '                                     & ",F02_01.DEST_CD               AS      DEST_CD                    " & vbNewLine _
    '                                     & ",ISNULL(M10_02.DEST_NM,M10_04.DEST_NM) AS      DEST_NM           " & vbNewLine _
    '                                     & ",ISNULL(M10_02.JIS,M10_04.JIS) AS      DEST_JIS_CD               " & vbNewLine _
    '                                     & ",ISNULL(M10_02.AD_1,M10_04.AD_1) AS      DEST_AD_1               " & vbNewLine _
    '                                     & ",F02_01.UNSO_PKG_NB           AS      UNSO_PKG_NB                " & vbNewLine _
    '                                     & ",F02_01.UNSO_WT               AS      UNSO_WT                    " & vbNewLine _
    '                                     & ",ISNULL(F04_01.DECI_UNCHIN,0) AS      UNCHIN                     " & vbNewLine _
    '                                     & ",F02_01.CUST_CD_L             AS      CUST_CD_L                  " & vbNewLine _
    '                                     & ",F02_01.CUST_CD_M             AS      CUST_CD_M                  " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_L             AS      CUST_NM_L                  " & vbNewLine _
    '                                     & ",M07_01.CUST_NM_M             AS      CUST_NM_M                  " & vbNewLine _
    '                                     & ",F02_01.CUST_REF_NO           AS      CUST_REF_NO                " & vbNewLine _
    '                                     & ",F02_01.INOUTKA_NO_L          AS      INOUTKA_NO_L               " & vbNewLine _
    '                                     & ",F02_01.MOTO_DATA_KB          AS      MOTO_DATA_KB               " & vbNewLine _
    '                                     & ",KBN_02.KBN_NM1               AS      MOTO_DATA_NM               " & vbNewLine _
    '                                     & ",F02_01.REMARK                AS      REMARK                     " & vbNewLine _
    '                                     & ",F02_01.TARIFF_BUNRUI_KB      AS      TARIFF_BUNRUI_KB           " & vbNewLine _
    '                                     & ",KBN_03.KBN_NM1               AS      TARIFF_BUNRUI_NM           " & vbNewLine _
    '                                     & ",F02_01.UNSO_CD               AS      UNSO_CD                    " & vbNewLine _
    '                                     & ",F02_01.UNSO_BR_CD            AS      UNSO_BR_CD                 " & vbNewLine _
    '                                     & ",M38_01.UNSOCO_NM             AS      UNSO_NM                    " & vbNewLine _
    '                                     & ",M38_01.UNSOCO_BR_NM          AS      UNSO_BR_NM                 " & vbNewLine _
    '                                     & ",F02_01.UNSO_ONDO_KB          AS      UNSO_ONDO_KB               " & vbNewLine _
    '                                     & ",KBN_04.KBN_NM1               AS      UNSO_ONDO_NM               " & vbNewLine _
    '                                     & ",F02_01.TYUKEI_HAISO_FLG      AS      TYUKEI_HAISO_FLG           " & vbNewLine _
    '                                     & ",F02_01.SYS_UPD_DATE          AS      SYS_UPD_DATE               " & vbNewLine _
    '                                     & ",F02_01.SYS_UPD_TIME          AS      SYS_UPD_TIME               " & vbNewLine _
    '                                     & ",@UP_KBN                      AS      UP_KBN                     " & vbNewLine _
    '                                     & ",F02_01.SYS_DEL_FLG           AS      SYS_DEL_FLG                " & vbNewLine _
    '                                     & " FROM       $LM_TRN$..F_UNSO_L     F02_01                        " & vbNewLine _
    '                                     & " LEFT  JOIN (                                                    " & vbNewLine _
    '                                     & "                SELECT                                           " & vbNewLine _
    '                                     & "                        SUM(    F04_01.DECI_UNCHIN               " & vbNewLine _
    '                                     & "                              + F04_01.DECI_CITY_EXTC            " & vbNewLine _
    '                                     & "                              + F04_01.DECI_WINT_EXTC            " & vbNewLine _
    '                                     & "                              + F04_01.DECI_RELY_EXTC            " & vbNewLine _
    '                                     & "                              + F04_01.DECI_TOLL                 " & vbNewLine _
    '                                     & "                              + F04_01.DECI_INSU                 " & vbNewLine _
    '                                     & "                            )                      AS DECI_UNCHIN" & vbNewLine _
    '                                     & "                               ,F04_01.NRS_BR_CD   AS NRS_BR_CD  " & vbNewLine _
    '                                     & "                               ,F04_01.UNSO_NO_L   AS UNSO_NO_L  " & vbNewLine _
    '                                     & "                  FROM $LM_TRN$..F_UNCHIN_TRS F04_01             " & vbNewLine _
    '                                     & "                 GROUP BY F04_01.NRS_BR_CD                       " & vbNewLine _
    '                                     & "                         ,F04_01.UNSO_NO_L                       " & vbNewLine _
    '                                     & "       )                           F04_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = F04_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.UNSO_NO_L          = F04_01.UNSO_NO_L              " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_DEST       M10_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M10_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M10_01.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.ORIG_CD            = M10_01.DEST_CD                " & vbNewLine _
    '                                     & "  AND  M10_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_DEST       M10_02                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M10_02.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M10_02.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.DEST_CD            = M10_02.DEST_CD                " & vbNewLine _
    '                                     & "  AND  M10_02.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_DEST       M10_03                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M10_03.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M10_03.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.ORIG_CD            = M10_03.DEST_CD                " & vbNewLine _
    '                                     & "  AND  M10_03.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_DEST       M10_04                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M10_04.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M10_04.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.DEST_CD            = M10_04.DEST_CD                " & vbNewLine _
    '                                     & "  AND  M10_04.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_AREA       M36_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M36_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.AREA_CD            = M36_01.AREA_CD                " & vbNewLine _
    '                                     & " --要望番号1202 追加START(2012.07.02)                            " & vbNewLine _
    '                                     & "  AND  F02_01.BIN_KB             = M36_01.BIN_KB                 " & vbNewLine _
    '                                     & " --要望番号1202 追加END  (2012.07.02)                            " & vbNewLine _
    '                                     & "  AND  M10_02.JIS                = M36_01.JIS_CD                 " & vbNewLine _
    '                                     & "  AND  M36_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_CUST       M07_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M07_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_L          = M07_01.CUST_CD_L              " & vbNewLine _
    '                                     & "  AND  F02_01.CUST_CD_M          = M07_01.CUST_CD_M              " & vbNewLine _
    '                                     & "  AND  M07_01.CUST_CD_S          = '00'                          " & vbNewLine _
    '                                     & "  AND  M07_01.CUST_CD_SS         = '00'                          " & vbNewLine _
    '                                     & "  AND  M07_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..M_UNSOCO     M38_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.NRS_BR_CD          = M38_01.NRS_BR_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.UNSO_CD            = M38_01.UNSOCO_CD              " & vbNewLine _
    '                                     & "  AND  F02_01.UNSO_BR_CD         = M38_01.UNSOCO_BR_CD           " & vbNewLine _
    '                                     & "  AND  M38_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_01                        " & vbNewLine _
    '                                     & "   ON  F02_01.BIN_KB             = KBN_01.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_01.KBN_GROUP_CD       = 'U001'                        " & vbNewLine _
    '                                     & "  AND  KBN_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_02                        " & vbNewLine _
    '                                     & "   ON  F02_01.MOTO_DATA_KB       = KBN_02.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_02.KBN_GROUP_CD       = 'M004'                        " & vbNewLine _
    '                                     & "  AND  KBN_02.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_03                        " & vbNewLine _
    '                                     & "   ON  F02_01.TARIFF_BUNRUI_KB   = KBN_03.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_03.KBN_GROUP_CD       = 'T015'                        " & vbNewLine _
    '                                     & "  AND  KBN_03.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_04                        " & vbNewLine _
    '                                     & "   ON  F02_01.UNSO_ONDO_KB       = KBN_04.KBN_CD                 " & vbNewLine _
    '                                     & "  AND  KBN_04.KBN_GROUP_CD       = 'U006'                        " & vbNewLine _
    '                                     & "  AND  KBN_04.SYS_DEL_FLG        = '0'                           " & vbNewLine _
    '                                     & "WHERE  F02_01.SYS_DEL_FLG        = '0'                           " & vbNewLine
    Private Const SQL_SELECT_L1 As String = "SELECT                                                           " & vbNewLine _
                                         & " F02_01.NRS_BR_CD             AS      NRS_BR_CD                  " & vbNewLine _
                                         & ",F02_01.UNSO_NO_L             AS      UNSO_NO_L                  " & vbNewLine _
                                         & ",F02_01.TRIP_NO_SYUKA         AS      TRIP_NO_SYUKA              " & vbNewLine _
                                         & ",F02_01.TRIP_NO_TYUKEI        AS      TRIP_NO_TYUKEI             " & vbNewLine _
                                         & ",F02_01.TRIP_NO_HAIKA         AS      TRIP_NO_HAIKA              " & vbNewLine _
                                         & ",F02_01.TRIP_NO               AS      TRIP_NO                    " & vbNewLine _
                                         & ",F02_01.BIN_KB                AS      BIN_KB                     " & vbNewLine _
                                         & ",KBN_01.KBN_NM1               AS      BIN_NM                     " & vbNewLine _
                                         & ",F02_01.AREA_CD               AS      AREA_CD                    " & vbNewLine _
                                         & ",M36_01.AREA_NM               AS      AREA_NM                    " & vbNewLine _
                                         & ",F02_01.ARR_PLAN_DATE         AS      ARR_PLAN_DATE              " & vbNewLine _
                                         & ",F02_01.ORIG_CD               AS      ORIG_CD                    " & vbNewLine _
                                         & ",ISNULL(M10_01.DEST_NM,M10_03.DEST_NM) AS      ORIG_NM           " & vbNewLine _
                                         & ",ISNULL(M10_01.JIS,M10_03.JIS) AS      ORIG_JIS_CD               " & vbNewLine _
                                         & ",ISNULL(M10_01.AD_1,M10_03.AD_1) AS      ORIG_AD_1               " & vbNewLine _
                                         & ",F02_01.DEST_CD               AS      DEST_CD                    " & vbNewLine _
                                         & ",ISNULL(M10_02.DEST_NM,M10_04.DEST_NM) AS      DEST_NM           " & vbNewLine _
                                         & ",ISNULL(M10_02.JIS,M10_04.JIS) AS      DEST_JIS_CD               " & vbNewLine _
                                         & ",ISNULL(M10_02.AD_1,M10_04.AD_1) AS      DEST_AD_1               " & vbNewLine _
                                         & ",F02_01.UNSO_PKG_NB           AS      UNSO_PKG_NB                " & vbNewLine _
                                         & ",F02_01.UNSO_WT               AS      UNSO_WT                    " & vbNewLine _
                                         & ",ISNULL(F04_01.DECI_UNCHIN,0) AS      UNCHIN                     " & vbNewLine _
                                         & ",F02_01.CUST_CD_L             AS      CUST_CD_L                  " & vbNewLine _
                                         & ",F02_01.CUST_CD_M             AS      CUST_CD_M                  " & vbNewLine _
                                         & ",M07_01.CUST_NM_L             AS      CUST_NM_L                  " & vbNewLine _
                                         & ",M07_01.CUST_NM_M             AS      CUST_NM_M                  " & vbNewLine _
                                         & ",F02_01.CUST_REF_NO           AS      CUST_REF_NO                " & vbNewLine _
                                         & ",F02_01.INOUTKA_NO_L          AS      INOUTKA_NO_L               " & vbNewLine _
                                         & ",F02_01.MOTO_DATA_KB          AS      MOTO_DATA_KB               " & vbNewLine _
                                         & ",KBN_02.KBN_NM1               AS      MOTO_DATA_NM               " & vbNewLine _
                                         & ",F02_01.REMARK                AS      REMARK                     " & vbNewLine _
                                         & ",F02_01.TARIFF_BUNRUI_KB      AS      TARIFF_BUNRUI_KB           " & vbNewLine _
                                         & ",KBN_03.KBN_NM1               AS      TARIFF_BUNRUI_NM           " & vbNewLine _
                                         & ",F02_01.UNSO_CD               AS      UNSO_CD                    " & vbNewLine _
                                         & ",F02_01.UNSO_BR_CD            AS      UNSO_BR_CD                 " & vbNewLine _
                                         & ",M38_01.UNSOCO_NM             AS      UNSO_NM                    " & vbNewLine _
                                         & ",M38_01.UNSOCO_BR_NM          AS      UNSO_BR_NM                 " & vbNewLine _
                                         & ",F02_01.UNSO_ONDO_KB          AS      UNSO_ONDO_KB               " & vbNewLine _
                                         & ",KBN_04.KBN_NM1               AS      UNSO_ONDO_NM               " & vbNewLine _
                                         & ",F02_01.TYUKEI_HAISO_FLG      AS      TYUKEI_HAISO_FLG           " & vbNewLine _
                                         & ",F02_01.SYS_UPD_DATE          AS      SYS_UPD_DATE               " & vbNewLine _
                                         & ",F02_01.SYS_UPD_TIME          AS      SYS_UPD_TIME               " & vbNewLine _
                                         & ",@UP_KBN                      AS      UP_KBN                     " & vbNewLine _
                                         & ",F02_01.SYS_DEL_FLG           AS      SYS_DEL_FLG                " & vbNewLine _
                                         & ",SHIHARAI.SHIHARAI_UNCHIN     AS      SHIHARAI_UNCHIN            " & vbNewLine _
                                         & ",F02_01.OUTKA_PLAN_DATE       AS      OUTKA_PLAN_DATE            " & vbNewLine _
                                         & " FROM                                                            " & vbNewLine

    Private Const SQL_SELECT_L2 As String = " F02_01                                                        " & vbNewLine _
                                         & "LEFT  JOIN (                                                     " & vbNewLine _
                                         & "                SELECT                                           " & vbNewLine _
                                         & "                        SUM(    F04_01.DECI_UNCHIN               " & vbNewLine _
                                         & "                              + F04_01.DECI_CITY_EXTC            " & vbNewLine _
                                         & "                              + F04_01.DECI_WINT_EXTC            " & vbNewLine _
                                         & "                              + F04_01.DECI_RELY_EXTC            " & vbNewLine _
                                         & "                              + F04_01.DECI_TOLL                 " & vbNewLine _
                                         & "                              + F04_01.DECI_INSU                 " & vbNewLine _
                                         & "                            )                      AS DECI_UNCHIN" & vbNewLine _
                                         & "                               ,F04_01.NRS_BR_CD   AS NRS_BR_CD  " & vbNewLine _
                                         & "                               ,F04_01.UNSO_NO_L   AS UNSO_NO_L  " & vbNewLine _
                                         & "                  FROM                                           " & vbNewLine

    Private Const SQL_SELECT_L3 As String = "                F04_01                                          " & vbNewLine _
                                         & "                 GROUP BY F04_01.NRS_BR_CD                       " & vbNewLine _
                                         & "                         ,F04_01.UNSO_NO_L                       " & vbNewLine _
                                         & "       )                           F04_01                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = F04_01.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.UNSO_NO_L          = F04_01.UNSO_NO_L              " & vbNewLine _
                                         & " LEFT  JOIN (                                                    " & vbNewLine _
                                         & "                SELECT                                           " & vbNewLine _
                                         & "                        SUM(    SHIHARAI.DECI_UNCHIN             " & vbNewLine _
                                         & "                              + SHIHARAI.DECI_CITY_EXTC          " & vbNewLine _
                                         & "                              + SHIHARAI.DECI_WINT_EXTC          " & vbNewLine _
                                         & "                              + SHIHARAI.DECI_RELY_EXTC          " & vbNewLine _
                                         & "                              + SHIHARAI.DECI_TOLL               " & vbNewLine _
                                         & "                              + SHIHARAI.DECI_INSU               " & vbNewLine _
                                         & "                            )                  AS SHIHARAI_UNCHIN" & vbNewLine _
                                         & "                               ,SHIHARAI.NRS_BR_CD AS NRS_BR_CD  " & vbNewLine _
                                         & "                               ,SHIHARAI.UNSO_NO_L AS UNSO_NO_L  " & vbNewLine _
                                         & "                  FROM                                           " & vbNewLine

    Private Const SQL_SELECT_L4 As String = "                 SHIHARAI                                       " & vbNewLine _
                                         & "                 GROUP BY SHIHARAI.NRS_BR_CD                     " & vbNewLine _
                                         & "                         ,SHIHARAI.UNSO_NO_L                     " & vbNewLine _
                                         & "       )                           SHIHARAI                      " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = SHIHARAI.NRS_BR_CD            " & vbNewLine _
                                         & "  AND  F02_01.UNSO_NO_L          = SHIHARAI.UNSO_NO_L            " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_DEST       M10_01                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M10_01.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.CUST_CD_L          = M10_01.CUST_CD_L              " & vbNewLine _
                                         & "  AND  F02_01.ORIG_CD            = M10_01.DEST_CD                " & vbNewLine _
                                         & "  AND  M10_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_DEST       M10_02                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M10_02.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.CUST_CD_L          = M10_02.CUST_CD_L              " & vbNewLine _
                                         & "  AND  F02_01.DEST_CD            = M10_02.DEST_CD                " & vbNewLine _
                                         & "  AND  M10_02.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_DEST       M10_03                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M10_03.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.CUST_CD_L          = M10_03.CUST_CD_L              " & vbNewLine _
                                         & "  AND  F02_01.ORIG_CD            = M10_03.DEST_CD                " & vbNewLine _
                                         & "  AND  M10_03.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_DEST       M10_04                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M10_04.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.CUST_CD_L          = M10_04.CUST_CD_L              " & vbNewLine _
                                         & "  AND  F02_01.DEST_CD            = M10_04.DEST_CD                " & vbNewLine _
                                         & "  AND  M10_04.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_AREA       M36_01                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M36_01.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.AREA_CD            = M36_01.AREA_CD                " & vbNewLine _
                                         & " --要望番号1202 追加START(2012.07.02)                            " & vbNewLine _
                                         & "  AND  F02_01.BIN_KB             = M36_01.BIN_KB                 " & vbNewLine _
                                         & " --要望番号1202 追加END  (2012.07.02)                            " & vbNewLine _
                                         & "  AND  M10_02.JIS                = M36_01.JIS_CD                 " & vbNewLine _
                                         & "  AND  M36_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_CUST       M07_01                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M07_01.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.CUST_CD_L          = M07_01.CUST_CD_L              " & vbNewLine _
                                         & "  AND  F02_01.CUST_CD_M          = M07_01.CUST_CD_M              " & vbNewLine _
                                         & "  AND  M07_01.CUST_CD_S          = '00'                          " & vbNewLine _
                                         & "  AND  M07_01.CUST_CD_SS         = '00'                          " & vbNewLine _
                                         & "  AND  M07_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..M_UNSOCO     M38_01                        " & vbNewLine _
                                         & "   ON  F02_01.NRS_BR_CD          = M38_01.NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F02_01.UNSO_CD            = M38_01.UNSOCO_CD              " & vbNewLine _
                                         & "  AND  F02_01.UNSO_BR_CD         = M38_01.UNSOCO_BR_CD           " & vbNewLine _
                                         & "  AND  M38_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_01                        " & vbNewLine _
                                         & "   ON  F02_01.BIN_KB             = KBN_01.KBN_CD                 " & vbNewLine _
                                         & "  AND  KBN_01.KBN_GROUP_CD       = 'U001'                        " & vbNewLine _
                                         & "  AND  KBN_01.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_02                        " & vbNewLine _
                                         & "   ON  F02_01.MOTO_DATA_KB       = KBN_02.KBN_CD                 " & vbNewLine _
                                         & "  AND  KBN_02.KBN_GROUP_CD       = 'M004'                        " & vbNewLine _
                                         & "  AND  KBN_02.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_03                        " & vbNewLine _
                                         & "   ON  F02_01.TARIFF_BUNRUI_KB   = KBN_03.KBN_CD                 " & vbNewLine _
                                         & "  AND  KBN_03.KBN_GROUP_CD       = 'T015'                        " & vbNewLine _
                                         & "  AND  KBN_03.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & " LEFT  JOIN $LM_MST$..Z_KBN        KBN_04                        " & vbNewLine _
                                         & "   ON  F02_01.UNSO_ONDO_KB       = KBN_04.KBN_CD                 " & vbNewLine _
                                         & "  AND  KBN_04.KBN_GROUP_CD       = 'U006'                        " & vbNewLine _
                                         & "  AND  KBN_04.SYS_DEL_FLG        = '0'                           " & vbNewLine _
                                         & "WHERE  F02_01.SYS_DEL_FLG        = '0'                           " & vbNewLine
    'M_DESTは【M10_01とM10_03】、【M10_02と04】がペア。
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    'END YANAI 要望番号376

    ''' <summary>
    ''' 他社倉庫名称の取得（出荷）
    ''' </summary>
    Private Const SQL_SELECT_TASYA_WH_NM_OUTKA_L1 As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        TOU_SITU.TASYA_WH_NM                                             " & vbNewLine _
        & "   FROM                                                                  " & vbNewLine

    Private Const SQL_SELECT_TASYA_WH_NM_OUTKA_L2 As String _
        = "     OUTKA_L                                                             " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine

    Private Const SQL_SELECT_TASYA_WH_NM_OUTKA_L3 As String _
        = "     OUTKA_S                                                             " & vbNewLine _
        & "     ON OUTKA_L.NRS_BR_CD                = OUTKA_S.NRS_BR_CD             " & vbNewLine _
        & "    AND OUTKA_L.OUTKA_NO_L               = OUTKA_S.OUTKA_NO_L            " & vbNewLine _
        & "    AND OUTKA_S.SYS_DEL_FLG              = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_TOU_SITU TOU_SITU                                    " & vbNewLine _
        & "     ON OUTKA_L.NRS_BR_CD                = TOU_SITU.NRS_BR_CD            " & vbNewLine _
        & "    AND OUTKA_L.WH_CD                    = TOU_SITU.WH_CD                " & vbNewLine _
        & "    AND OUTKA_S.TOU_NO                   = TOU_SITU.TOU_NO               " & vbNewLine _
        & "    AND OUTKA_S.SITU_NO                  = TOU_SITU.SITU_NO              " & vbNewLine _
        & "    AND TOU_SITU.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE OUTKA_L.NRS_BR_CD                = @NRS_BR_CD                    " & vbNewLine _
        & "    AND OUTKA_L.OUTKA_NO_L               = @INOUTKA_NO_L                 " & vbNewLine _
        & "    AND OUTKA_L.SYS_DEL_FLG              = '0'                           " & vbNewLine _
        & "  ORDER BY                                                               " & vbNewLine _
        & "        OUTKA_L.OUTKA_NO_L                                               " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_M                                               " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_S                                               " & vbNewLine

    ''' <summary>
    ''' 他社倉庫名称の取得（入荷）
    ''' </summary>
    Private Const SQL_SELECT_TASYA_WH_NM_INKA_L1 As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        TOU_SITU.TASYA_WH_NM                                             " & vbNewLine _
        & "   FROM                                                                  " & vbNewLine

    Private Const SQL_SELECT_TASYA_WH_NM_INKA_L2 As String _
        = "     INKA_L                                                              " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine

    Private Const SQL_SELECT_TASYA_WH_NM_INKA_L3 As String _
        = "     INKA_S                                                              " & vbNewLine _
        & "     ON INKA_L.NRS_BR_CD                 = INKA_S.NRS_BR_CD              " & vbNewLine _
        & "    AND INKA_L.INKA_NO_L                 = INKA_S.INKA_NO_L              " & vbNewLine _
        & "    AND INKA_S.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_TOU_SITU TOU_SITU                                    " & vbNewLine _
        & "     ON INKA_L.NRS_BR_CD                 = TOU_SITU.NRS_BR_CD            " & vbNewLine _
        & "    AND INKA_L.WH_CD                     = TOU_SITU.WH_CD                " & vbNewLine _
        & "    AND INKA_S.TOU_NO                    = TOU_SITU.TOU_NO               " & vbNewLine _
        & "    AND INKA_S.SITU_NO                   = TOU_SITU.SITU_NO              " & vbNewLine _
        & "    AND TOU_SITU.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE INKA_L.NRS_BR_CD                 = @NRS_BR_CD                    " & vbNewLine _
        & "    AND INKA_L.INKA_NO_L                 = @INOUTKA_NO_L                 " & vbNewLine _
        & "    AND INKA_L.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "  ORDER BY                                                               " & vbNewLine _
        & "        INKA_L.INKA_NO_L                                                 " & vbNewLine _
        & "      , INKA_S.INKA_NO_M                                                 " & vbNewLine _
        & "      , INKA_S.INKA_NO_S                                                 " & vbNewLine

#End Region

#Region "SHIHARAI_TRS"
    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
#Region "支払運賃データの検索 SQL SELECT句"

    ''' <summary>
    ''' 支払運賃データの検索 SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_SHIHARAI As String = " SELECT                                                                   " & vbNewLine _
                                                & " SHIHARAI.YUSO_BR_CD                     AS YUSO_BR_CD                    " & vbNewLine _
                                                & ",SHIHARAI.NRS_BR_CD                      AS NRS_BR_CD                     " & vbNewLine _
                                                & ",SHIHARAI.UNSO_NO_L                      AS UNSO_NO_L                     " & vbNewLine _
                                                & ",SHIHARAI.UNSO_NO_M                      AS UNSO_NO_M                     " & vbNewLine _
                                                & ",SHIHARAI.CUST_CD_L                      AS CUST_CD_L                     " & vbNewLine _
                                                & ",SHIHARAI.CUST_CD_M                      AS CUST_CD_M                     " & vbNewLine _
                                                & ",SHIHARAI.CUST_CD_S                      AS CUST_CD_S                     " & vbNewLine _
                                                & ",SHIHARAI.CUST_CD_SS                     AS CUST_CD_SS                    " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_GROUP_NO              AS SHIHARAI_GROUP_NO             " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_GROUP_NO_M            AS SHIHARAI_GROUP_NO_M           " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAITO_CD                  AS SHIHARAITO_CD                 " & vbNewLine _
                                                & ",SHIHARAI.UNTIN_CALCULATION_KB           AS UNTIN_CALCULATION_KB          " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_SYARYO_KB             AS SHIHARAI_SYARYO_KB            " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_PKG_UT                AS SHIHARAI_PKG_UT               " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_NG_NB                 AS SHIHARAI_NG_NB                " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_DANGER_KB             AS SHIHARAI_DANGER_KB            " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_TARIFF_BUNRUI_KB      AS SHIHARAI_TARIFF_BUNRUI_KB     " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_TARIFF_CD             AS SHIHARAI_TARIFF_CD            " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_ETARIFF_CD            AS SHIHARAI_ETARIFF_CD           " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_KYORI                 AS SHIHARAI_KYORI                " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_WT                    AS SHIHARAI_WT                   " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_UNCHIN                AS SHIHARAI_UNCHIN               " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_CITY_EXTC             AS SHIHARAI_CITY_EXTC            " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_WINT_EXTC             AS SHIHARAI_WINT_EXTC            " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_RELY_EXTC             AS SHIHARAI_RELY_EXTC            " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_TOLL                  AS SHIHARAI_TOLL                 " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_INSU                  AS SHIHARAI_INSU                 " & vbNewLine _
                                                & ",SHIHARAI.SHIHARAI_FIXED_FLAG            AS SHIHARAI_FIXED_FLAG           " & vbNewLine _
                                                & ",SHIHARAI.DECI_NG_NB                     AS DECI_NG_NB                    " & vbNewLine _
                                                & ",SHIHARAI.DECI_KYORI                     AS DECI_KYORI                    " & vbNewLine _
                                                & ",SHIHARAI.DECI_WT                        AS DECI_WT                       " & vbNewLine _
                                                & ",SHIHARAI.DECI_UNCHIN                    AS DECI_UNCHIN                   " & vbNewLine _
                                                & ",SHIHARAI.DECI_CITY_EXTC                 AS DECI_CITY_EXTC                " & vbNewLine _
                                                & ",SHIHARAI.DECI_WINT_EXTC                 AS DECI_WINT_EXTC                " & vbNewLine _
                                                & ",SHIHARAI.DECI_RELY_EXTC                 AS DECI_RELY_EXTC                " & vbNewLine _
                                                & ",SHIHARAI.DECI_TOLL                      AS DECI_TOLL                     " & vbNewLine _
                                                & ",SHIHARAI.DECI_INSU                      AS DECI_INSU                     " & vbNewLine _
                                                & ",SHIHARAI.KANRI_UNCHIN                   AS KANRI_UNCHIN                  " & vbNewLine _
                                                & ",SHIHARAI.KANRI_CITY_EXTC                AS KANRI_CITY_EXTC               " & vbNewLine _
                                                & ",SHIHARAI.KANRI_WINT_EXTC                AS KANRI_WINT_EXTC               " & vbNewLine _
                                                & ",SHIHARAI.KANRI_RELY_EXTC                AS KANRI_RELY_EXTC               " & vbNewLine _
                                                & ",SHIHARAI.KANRI_TOLL                     AS KANRI_TOLL                    " & vbNewLine _
                                                & ",SHIHARAI.KANRI_INSU                     AS KANRI_INSU                    " & vbNewLine _
                                                & ",SHIHARAI.REMARK                         AS REMARK                        " & vbNewLine _
                                                & ",SHIHARAI.SIZE_KB                        AS SIZE_KB                       " & vbNewLine _
                                                & ",SHIHARAI.TAX_KB                         AS TAX_KB                        " & vbNewLine _
                                                & ",SHIHARAI.SAGYO_KANRI                    AS SAGYO_KANRI                   " & vbNewLine

#End Region

#Region "支払運賃データの検索 SQL FROM句"

    ''' <summary>
    ''' 支払運賃データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SHIHARAI As String = "FROM                                                               " & vbNewLine _
                                                     & "$LM_TRN$..F_SHIHARAI_TRS SHIHARAI                                  " & vbNewLine _
                                                     & "INNER JOIN                                                         " & vbNewLine _
                                                     & "$LM_TRN$..F_UNSO_L UNSOL                                           " & vbNewLine _
                                                     & "ON                                                                 " & vbNewLine _
                                                     & "UNSOL.NRS_BR_CD = SHIHARAI.NRS_BR_CD                               " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "UNSOL.TRIP_NO = @TRIP_NO                                           " & vbNewLine _
                                                     & "AND                                                                " & vbNewLine _
                                                     & "UNSOL.SYS_DEL_FLG = '0'                                            " & vbNewLine

#End Region

#Region "支払運賃データの検索 SQL WHERE句"

    ''' <summary>
    ''' 支払運賃データの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SHIHARAI As String = "WHERE                                                               " & vbNewLine _
                                                     & "SHIHARAI.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                                     & "AND                                                                  " & vbNewLine _
                                                     & "SHIHARAI.UNSO_NO_L = UNSOL.UNSO_NO_L                                 " & vbNewLine _
                                                     & "AND                                                                  " & vbNewLine _
                                                     & "SHIHARAI.SYS_DEL_FLG = '0'                                           " & vbNewLine

#End Region

#Region "支払運賃データの検索 SQL ORDER句"

    ''' <summary>
    ''' 支払運賃データの検索 SQL ORDER句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_ORDER_SHIHARAI As String = "ORDER BY                                                            " & vbNewLine _
                                                     & " SHIHARAI.UNSO_NO_L                                                  " & vbNewLine _
                                                     & ",SHIHARAI.UNSO_NO_M                                                  " & vbNewLine

#End Region

    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
#Region "支払運賃データの検索 SQL FROM句"

    ''' <summary>
    ''' 支払運賃データの検索 SQL FROM句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_FROM_SHIHARAI2 As String = "FROM                                                               " & vbNewLine _
                                                     & "$LM_TRN$..F_SHIHARAI_TRS SHIHARAI                                   " & vbNewLine

#End Region

#Region "支払運賃データの検索 SQL WHERE句"

    ''' <summary>
    ''' 支払運賃データの検索 SQL WHERE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_WHERE_SHIHARAI2 As String = "WHERE                                                               " & vbNewLine _
                                                     & "SHIHARAI.NRS_BR_CD = @NRS_BR_CD                                      " & vbNewLine _
                                                     & "AND                                                                  " & vbNewLine _
                                                     & "SHIHARAI.SYS_DEL_FLG = '0'                                           " & vbNewLine

#End Region
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End




#End Region

#Region "区分マスタ用"

    Private Const SQL_SELECT_KBN_DATA As String = "SELECT                          " & vbNewLine _
                                                & " KBN_GROUP_CD                   " & vbNewLine _
                                                & ",KBN_CD                         " & vbNewLine _
                                                & ",KBN_KEYWORD                    " & vbNewLine _
                                                & ",KBN_NM1                        " & vbNewLine _
                                                & ",KBN_NM2                        " & vbNewLine _
                                                & ",KBN_NM3                        " & vbNewLine _
                                                & ",KBN_NM4                        " & vbNewLine _
                                                & ",KBN_NM5                        " & vbNewLine _
                                                & ",KBN_NM6                        " & vbNewLine _
                                                & ",KBN_NM7                        " & vbNewLine _
                                                & ",KBN_NM8                        " & vbNewLine _
                                                & ",KBN_NM9                        " & vbNewLine _
                                                & ",KBN_NM10                       " & vbNewLine _
                                                & ",VALUE1                         " & vbNewLine _
                                                & ",VALUE2                         " & vbNewLine _
                                                & ",VALUE3                         " & vbNewLine _
                                                & ",SORT                           " & vbNewLine _
                                                & ",REM                            " & vbNewLine _
                                                & " FROM $LM_MST$..Z_KBN           " & vbNewLine _
                                                & "WHERE KBN_GROUP_CD  = @KBN_GROUP_CD" & vbNewLine _
                                                & "  AND KBN_NM1       = @NRS_BR_CD" & vbNewLine _
                                                & "  AND SYS_DEL_FLG   = '0'       " & vbNewLine _
                                                & "ORDER BY KBN_GROUP_CD,SORT,KBN_CD               " & vbNewLine



#End Region

#Region "車輌マスタ用"

    '2022.09.06 追加START
    Private Const SQL_SELECT_CAR_DATA As String = "SELECT                               " & vbNewLine _
                                                & "      CAR_KEY                        " & vbNewLine _
                                                & "     ,DAY_UNCHIN                     " & vbNewLine _
                                                & " FROM $LM_MST$..M_VCLE               " & vbNewLine _
                                                & "WHERE NRS_BR_CD = @NRS_BR_CD         " & vbNewLine _
                                                & "  AND CAR_KEY = @CAR_KEY             " & vbNewLine _
                                                & "  AND SYS_DEL_FLG = '0'              " & vbNewLine
    '2022.09.06 追加END

#End Region

#End Region

#Region "設定処理 SQL"

#Region "Insert"

#Region "UNSO_LL"

    Private Const SQL_INSERT_UNSO_LL As String = "INSERT INTO $LM_TRN$..F_UNSO_LL" & vbNewLine _
                                               & "(                              " & vbNewLine _
                                               & " NRS_BR_CD                     " & vbNewLine _
                                               & ",TRIP_NO                       " & vbNewLine _
                                               & ",UNSOCO_CD                     " & vbNewLine _
                                               & ",UNSOCO_BR_CD                  " & vbNewLine _
                                               & ",JSHA_KB                       " & vbNewLine _
                                               & ",BIN_KB                        " & vbNewLine _
                                               & ",CAR_KEY                       " & vbNewLine _
                                               & ",UNSO_ONDO                     " & vbNewLine _
                                               & ",DRIVER_CD                     " & vbNewLine _
                                               & ",TRIP_DATE                     " & vbNewLine _
                                               & ",PAY_UNCHIN                    " & vbNewLine _
                                               & ",PAY_TARIFF_CD                 " & vbNewLine _
                                               & ",HAISO_KB                      " & vbNewLine _
                                               & ",REMARK                        " & vbNewLine _
                                               & ",SYS_ENT_DATE                  " & vbNewLine _
                                               & ",SYS_ENT_TIME                  " & vbNewLine _
                                               & ",SYS_ENT_PGID                  " & vbNewLine _
                                               & ",SYS_ENT_USER                  " & vbNewLine _
                                               & ",SYS_UPD_DATE                  " & vbNewLine _
                                               & ",SYS_UPD_TIME                  " & vbNewLine _
                                               & ",SYS_UPD_PGID                  " & vbNewLine _
                                               & ",SYS_UPD_USER                  " & vbNewLine _
                                               & ",SYS_DEL_FLG                   " & vbNewLine _
                                               & " )VALUES(                      " & vbNewLine _
                                               & " @NRS_BR_CD                    " & vbNewLine _
                                               & ",@TRIP_NO                      " & vbNewLine _
                                               & ",@UNSOCO_CD                    " & vbNewLine _
                                               & ",@UNSOCO_BR_CD                 " & vbNewLine _
                                               & ",@JSHA_KB                      " & vbNewLine _
                                               & ",@BIN_KB                       " & vbNewLine _
                                               & ",@CAR_KEY                      " & vbNewLine _
                                               & ",@UNSO_ONDO                    " & vbNewLine _
                                               & ",@DRIVER_CD                    " & vbNewLine _
                                               & ",@TRIP_DATE                    " & vbNewLine _
                                               & ",@PAY_UNCHIN                   " & vbNewLine _
                                               & ",@PAY_TARIFF_CD                " & vbNewLine _
                                               & ",@HAISO_KB                     " & vbNewLine _
                                               & ",@REMARK                       " & vbNewLine _
                                               & ",@SYS_ENT_DATE                 " & vbNewLine _
                                               & ",@SYS_ENT_TIME                 " & vbNewLine _
                                               & ",@SYS_ENT_PGID                 " & vbNewLine _
                                               & ",@SYS_ENT_USER                 " & vbNewLine _
                                               & ",@SYS_UPD_DATE                 " & vbNewLine _
                                               & ",@SYS_UPD_TIME                 " & vbNewLine _
                                               & ",@SYS_UPD_PGID                 " & vbNewLine _
                                               & ",@SYS_UPD_USER                 " & vbNewLine _
                                               & ",@SYS_DEL_FLG                  " & vbNewLine _
                                               & ")                              " & vbNewLine

#End Region

#End Region

#Region "Update"

#Region "UNSO_LL"

    Private Const SQL_UPDATE_UNSO_LL As String = "UPDATE $LM_TRN$..F_UNSO_LL SET        " & vbNewLine _
                                               & " NRS_BR_CD            = @NRS_BR_CD    " & vbNewLine _
                                               & ",TRIP_NO              = @TRIP_NO      " & vbNewLine _
                                               & ",UNSOCO_CD            = @UNSOCO_CD    " & vbNewLine _
                                               & ",UNSOCO_BR_CD         = @UNSOCO_BR_CD " & vbNewLine _
                                               & ",JSHA_KB              = @JSHA_KB      " & vbNewLine _
                                               & ",BIN_KB               = @BIN_KB       " & vbNewLine _
                                               & ",CAR_KEY              = @CAR_KEY      " & vbNewLine _
                                               & ",UNSO_ONDO            = @UNSO_ONDO    " & vbNewLine _
                                               & ",DRIVER_CD            = @DRIVER_CD    " & vbNewLine _
                                               & ",TRIP_DATE            = @TRIP_DATE    " & vbNewLine _
                                               & ",PAY_UNCHIN           = @PAY_UNCHIN   " & vbNewLine _
                                               & ",PAY_TARIFF_CD        = @PAY_TARIFF_CD" & vbNewLine _
                                               & ",HAISO_KB             = @HAISO_KB     " & vbNewLine _
                                               & ",REMARK               = @REMARK       " & vbNewLine _
                                               & ",SYS_UPD_DATE         = @SYS_UPD_DATE " & vbNewLine _
                                               & ",SYS_UPD_TIME         = @SYS_UPD_TIME " & vbNewLine _
                                               & ",SYS_UPD_PGID         = @SYS_UPD_PGID " & vbNewLine _
                                               & ",SYS_UPD_USER         = @SYS_UPD_USER " & vbNewLine _
                                               & "WHERE NRS_BR_CD       = @NRS_BR_CD    " & vbNewLine _
                                               & "  AND TRIP_NO         = @TRIP_NO      " & vbNewLine _
                                               & "  AND SYS_UPD_DATE    = @GUI_UPD_DATE " & vbNewLine _
                                               & "  AND SYS_UPD_TIME    = @GUI_UPD_TIME " & vbNewLine


#End Region

    '要望番号1269 2012.07.12 追加START umano
#Region "UNSO_L(運送項目初期値設定)"

    Private Const SQL_UPDATE_UNSO_DEFAULT_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET " & vbNewLine
    Private Const SQL_UPDATE_UNSO_DEFAULT_NORMAL As String = " TRIP_NO  = ''           " & vbNewLine
    Private Const SQL_UPDATE_UNSO_DEFAULT_SYUKA As String = "TRIP_NO_SYUKA = ''        " & vbNewLine
    Private Const SQL_UPDATE_UNSO_DEFAULT_TYUKEI As String = "TRIP_NO_TYUKEI = ''      " & vbNewLine
    Private Const SQL_UPDATE_UNSO_DEFAULT_HAIKA As String = "TRIP_NO_HAIKA = ''        " & vbNewLine

    Private Const SQL_UPDATE_UNSO_WHERE_NORMAL As String = "WHERE NRS_BR_CD  = @NRS_BR_CD    " & vbNewLine _
                                                         & "  AND TRIP_NO   = @TRIP_NO       " & vbNewLine

    Private Const SQL_UPDATE_UNSO_WHERE_SYUKA As String = "WHERE NRS_BR_CD  = @NRS_BR_CD     " & vbNewLine _
                                                         & "  AND TRIP_NO_SYUKA   = @TRIP_NO " & vbNewLine

    Private Const SQL_UPDATE_UNSO_WHERE_TYUKEI As String = "WHERE NRS_BR_CD  = @NRS_BR_CD     " & vbNewLine _
                                                         & "  AND TRIP_NO_TYUKEI   = @TRIP_NO " & vbNewLine

    Private Const SQL_UPDATE_UNSO_WHERE_HAIKA As String = "WHERE NRS_BR_CD  = @NRS_BR_CD      " & vbNewLine _
                                                        & "  AND TRIP_NO_HAIKA   = @TRIP_NO      " & vbNewLine

    Private Const SQL_UPDATE_UNSO_DEFAULT_L2 As String = "UPDATE $LM_TRN$..F_UNSO_L SET " & vbNewLine _
                                                       & "  TRIP_NO        = ''         " & vbNewLine _
                                                       & " ,TRIP_NO_SYUKA  = ''         " & vbNewLine _
                                                       & " ,TRIP_NO_TYUKEI = ''         " & vbNewLine _
                                                       & " ,TRIP_NO_HAIKA  = ''         " & vbNewLine

    Private Const SQL_UPDATE_UNSO_WHERE As String = "WHERE NRS_BR_CD  = @NRS_BR_CD      " & vbNewLine _
                                                  & "  AND @TRIP_NO IN ( TRIP_NO , TRIP_NO_SYUKA , TRIP_NO_TYUKEI , TRIP_NO_HAIKA ) " & vbNewLine

#End Region
    '要望番号1269 2012.07.12 追加END umano

#Region "UNSO_L"

    'START YANAI 要望番号1362 運行情報入力 の保存時に、運送画面の運送会社を更新しない
    'Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET          " & vbNewLine _
    '                                           & " TRIP_NO             = @TRIP_NO       " & vbNewLine _
    '                                           & ",TRIP_NO_SYUKA       = @TRIP_NO_SYUKA " & vbNewLine _
    '                                           & ",TRIP_NO_TYUKEI      = @TRIP_NO_TYUKEI" & vbNewLine _
    '                                           & ",TRIP_NO_HAIKA       = @TRIP_NO_HAIKA " & vbNewLine _
    '                                           & "--要望番号:1242 terakawa 2012.07.05 Start" & vbNewLine _
    '                                           & ",BIN_KB              = @BIN_KB        " & vbNewLine _
    '                                           & ",UNSO_CD             = @UNSO_CD       " & vbNewLine _
    '                                           & ",UNSO_BR_CD          = @UNSO_BR_CD    " & vbNewLine _
    '                                           & "--要望番号:1242 terakawa 2012.07.05 End" & vbNewLine _
    '                                           & ",SYS_UPD_DATE        = @SYS_UPD_DATE  " & vbNewLine _
    '                                           & ",SYS_UPD_TIME        = @SYS_UPD_TIME  " & vbNewLine _
    '                                           & ",SYS_UPD_PGID        = @SYS_UPD_PGID  " & vbNewLine _
    '                                           & ",SYS_UPD_USER        = @SYS_UPD_USER  " & vbNewLine _
    '                                           & "WHERE NRS_BR_CD      = @NRS_BR_CD     " & vbNewLine _
    '                                           & "  AND UNSO_NO_L      = @UNSO_NO_L     " & vbNewLine _
    '                                           & "  AND SYS_UPD_DATE   = @GUI_UPD_DATE  " & vbNewLine _
    '                                           & "  AND SYS_UPD_TIME   = @GUI_UPD_TIME  " & vbNewLine
    Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET          " & vbNewLine _
                                               & " TRIP_NO             = @TRIP_NO       " & vbNewLine _
                                               & ",TRIP_NO_SYUKA       = @TRIP_NO_SYUKA " & vbNewLine _
                                               & ",TRIP_NO_TYUKEI      = @TRIP_NO_TYUKEI" & vbNewLine _
                                               & ",TRIP_NO_HAIKA       = @TRIP_NO_HAIKA " & vbNewLine _
                                               & "--'要望番号:1567（運行編集画面：保存時、UNSO_Lの便区分を更新しない） 2012/11/06 本明 Start" & vbNewLine _
                                               & "--,BIN_KB              = @BIN_KB        " & vbNewLine _
                                               & "--'要望番号:1567（運行編集画面：保存時、UNSO_Lの便区分を更新しない） 2012/11/06 本明 End  " & vbNewLine _
                                               & "--'要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start" & vbNewLine _
                                               & ",SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD    " & vbNewLine _
                                               & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD   " & vbNewLine _
                                               & "--'要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End  " & vbNewLine _
                                               & ",SYS_UPD_DATE        = @SYS_UPD_DATE  " & vbNewLine _
                                               & ",SYS_UPD_TIME        = @SYS_UPD_TIME  " & vbNewLine _
                                               & ",SYS_UPD_PGID        = @SYS_UPD_PGID  " & vbNewLine _
                                               & ",SYS_UPD_USER        = @SYS_UPD_USER  " & vbNewLine _
                                               & "WHERE NRS_BR_CD      = @NRS_BR_CD     " & vbNewLine _
                                               & "  AND UNSO_NO_L      = @UNSO_NO_L     " & vbNewLine _
                                               & "  AND SYS_UPD_DATE   = @GUI_UPD_DATE  " & vbNewLine _
                                               & "  AND SYS_UPD_TIME   = @GUI_UPD_TIME  " & vbNewLine
    'END YANAI 要望番号1362 運行情報入力 の保存時に、運送画面の運送会社を更新しない


#End Region

#Region "UNSO_L(計算時)"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    Private Const SQL_UPDATE_UNSO_L_KEISAN1 As String = "UPDATE $LM_TRN$..F_UNSO_L SET                 " & vbNewLine _
                                                      & " SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD    " & vbNewLine _
                                                      & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD   " & vbNewLine _
                                                      & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                      & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                      & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                      & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                      & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                      & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine _
                                                      & "  AND SYS_UPD_DATE   = @GUI_UPD_DATE          " & vbNewLine _
                                                      & "  AND SYS_UPD_TIME   = @GUI_UPD_TIME          " & vbNewLine

    Private Const SQL_UPDATE_UNSO_L_KEISAN2 As String = "UPDATE $LM_TRN$..F_UNSO_L SET                 " & vbNewLine _
                                                      & " SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                      & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                      & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                      & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                      & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                      & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine _
                                                      & "  AND SYS_UPD_DATE   = @GUI_UPD_DATE          " & vbNewLine _
                                                      & "  AND SYS_UPD_TIME   = @GUI_UPD_TIME          " & vbNewLine

    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "SHIHARAI_TRS(計算時)"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    Private Const SQL_UPDATE_SHIHARAI_KEISAN1 As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET          " & vbNewLine _
                                                       & " SHIHARAI_TARIFF_BUNRUI_KB  = @SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
                                                       & ",SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD    " & vbNewLine _
                                                       & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD   " & vbNewLine _
                                                       & ",SHIHARAITO_CD       = @SHIHARAITO_CD         " & vbNewLine _
                                                       & ",DECI_UNCHIN         = @DECI_UNCHIN           " & vbNewLine _
                                                       & ",DECI_CITY_EXTC      = 0                      " & vbNewLine _
                                                       & ",DECI_WINT_EXTC      = 0                      " & vbNewLine _
                                                       & ",DECI_RELY_EXTC      = 0                      " & vbNewLine _
                                                       & ",DECI_TOLL           = 0                      " & vbNewLine _
                                                       & ",DECI_INSU           = 0                      " & vbNewLine _
                                                       & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                       & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                       & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                       & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                       & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                       & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine

    Private Const SQL_UPDATE_SHIHARAI_KEISAN2 As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET          " & vbNewLine _
                                                       & " DECI_UNCHIN         = SHIHARAI_UNCHIN        " & vbNewLine _
                                                       & ",DECI_CITY_EXTC      = SHIHARAI_CITY_EXTC     " & vbNewLine _
                                                       & ",DECI_WINT_EXTC      = SHIHARAI_WINT_EXTC     " & vbNewLine _
                                                       & ",DECI_RELY_EXTC      = SHIHARAI_RELY_EXTC     " & vbNewLine _
                                                       & ",DECI_TOLL           = SHIHARAI_TOLL          " & vbNewLine _
                                                       & ",DECI_INSU           = SHIHARAI_INSU          " & vbNewLine _
                                                       & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                       & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                       & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                       & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                       & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                       & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine

    'START YANAI 要望番号1424 支払処理
    'Private Const SQL_UPDATE_SHIHARAI_KEISAN3 As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET          " & vbNewLine _
    '                                                   & " SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
    '                                                   & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
    '                                                   & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
    '                                                   & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
    '                                                   & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
    '                                                   & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine
    Private Const SQL_UPDATE_SHIHARAI_KEISAN3 As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET          " & vbNewLine _
                                                       & " SHIHARAI_TARIFF_BUNRUI_KB  = @SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
                                                       & ",SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD    " & vbNewLine _
                                                       & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD   " & vbNewLine _
                                                       & ",DECI_UNCHIN         = @DECI_UNCHIN           " & vbNewLine _
                                                       & ",DECI_CITY_EXTC      = 0                      " & vbNewLine _
                                                       & ",DECI_WINT_EXTC      = 0                      " & vbNewLine _
                                                       & ",DECI_RELY_EXTC      = 0                      " & vbNewLine _
                                                       & ",DECI_TOLL           = 0                      " & vbNewLine _
                                                       & ",DECI_INSU           = 0                      " & vbNewLine _
                                                       & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                       & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                       & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                       & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                       & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                       & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine
    'END YANAI 要望番号1424 支払処理
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "UNSO_LL(計算時)"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    Private Const SQL_UPDATE_UNSO_LL_KEISAN As String = "UPDATE $LM_TRN$..F_UNSO_LL SET                " & vbNewLine _
                                                      & " SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD    " & vbNewLine _
                                                      & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD   " & vbNewLine _
                                                      & ",SHIHARAI_UNSO_WT    = @SHIHARAI_UNSO_WT      " & vbNewLine _
                                                      & ",SHIHARAI_COUNT      = @SHIHARAI_COUNT        " & vbNewLine _
                                                      & ",SHIHARAI_UNCHIN     = @SHIHARAI_UNCHIN       " & vbNewLine _
                                                      & ",SHIHARAI_TARIFF_BUNRUI_KB = @SHIHARAI_TARIFF_BUNRUI_KB " & vbNewLine _
                                                      & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                      & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                      & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                      & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                      & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                      & "  AND TRIP_NO        = @TRIP_NO               " & vbNewLine

    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#Region "SHIHARAI_TRS(保存)"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    Private Const SQL_UPDATE_SHIHARAI_KEISAN As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET           " & vbNewLine _
                                                       & " SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD    " & vbNewLine _
                                                       & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD   " & vbNewLine _
                                                       & ",DECI_UNCHIN         = @DECI_UNCHIN           " & vbNewLine _
                                                       & ",DECI_CITY_EXTC      = @DECI_CITY_EXTC        " & vbNewLine _
                                                       & ",DECI_WINT_EXTC      = @DECI_WINT_EXTC        " & vbNewLine _
                                                       & ",DECI_RELY_EXTC      = @DECI_RELY_EXTC        " & vbNewLine _
                                                       & ",DECI_TOLL           = @DECI_TOLL             " & vbNewLine _
                                                       & ",DECI_INSU           = @DECI_INSU             " & vbNewLine _
                                                       & ",DECI_NG_NB          = @DECI_NG_NB            " & vbNewLine _
                                                       & ",DECI_KYORI          = @DECI_KYORI            " & vbNewLine _
                                                       & ",DECI_WT             = @DECI_WT               " & vbNewLine _
                                                       & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                       & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                       & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                       & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                       & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                       & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine

    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#End Region

    'START KIM 要望番号1485 支払い関連修正
#Region "F_SHIHARAI_TRS（支払先）"
    ''' <summary>
    ''' F_SHIHARAI_TRS（支払先）更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_SHIHARAI_HOZON As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET           " & vbNewLine _
                                                      & " SHIHARAITO_CD       = @SHIHARAITO_CD         " & vbNewLine _
                                                      & ",SYS_UPD_DATE        = @SYS_UPD_DATE          " & vbNewLine _
                                                      & ",SYS_UPD_TIME        = @SYS_UPD_TIME          " & vbNewLine _
                                                      & ",SYS_UPD_PGID        = @SYS_UPD_PGID          " & vbNewLine _
                                                      & ",SYS_UPD_USER        = @SYS_UPD_USER          " & vbNewLine _
                                                      & "WHERE NRS_BR_CD      = @NRS_BR_CD             " & vbNewLine _
                                                      & "  AND UNSO_NO_L      = @UNSO_NO_L             " & vbNewLine
#End Region
    'END KIM 要望番号1485 支払い関連修正

#End Region

#Region "Delete"

#Region "UNSO_LL"

    Private Const SQL_DELETE_UNSO_LL As String = " UPDATE $LM_TRN$..F_UNSO_LL SET    " & vbNewLine _
                                               & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                               & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                               & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                               & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                               & "      ,SYS_DEL_FLG  = @SYS_DEL_FLG " & vbNewLine _
                                               & " WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                               & "   AND TRIP_NO      = @TRIP_NO     " & vbNewLine _
                                               & "   AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                               & "   AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine


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

#Region "UNSO_LL"

    ''' <summary>
    ''' 運送(特大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(特大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectInitUnsoLLData(ByVal ds As DataSet) As DataSet

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_LL1_1)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_UNSO_LL"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_LL1_2)
        Me._StrSql.Append(vbNewLine)

        Return Me.SelectUnsoLLData(ds, Me._StrSql.ToString(), LMF030DAC.SelectCondition.PTN1)

    End Function

    ''' <summary>
    ''' 運送(特大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(特大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectNewUnsoLLData(ByVal ds As DataSet) As DataSet

        Return Me.SelectUnsoLLData(ds, LMF030DAC.SQL_SELECT_LL0, LMF030DAC.SelectCondition.PTN2)

    End Function

    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
    ''' <summary>
    ''' 運送(特大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(特大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectDriverUnsoLLData(ByVal ds As DataSet) As DataSet

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_LL2_1)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_UNSO_LL"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_LL2_2)
        Me._StrSql.Append(vbNewLine)

        Return Me.SelectUnsoLLData(ds, Me._StrSql.ToString(), LMF030DAC.SelectCondition.PTN4)

    End Function
    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start



    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
    ''' <summary>
    ''' 支払データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>登録時の支払データ取得SQLの構築・発行</remarks>
    Private Function SelectShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_L)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '０件はスルーする
        If inTbl.Rows.Count < 1 Then
            Return ds
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End


        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'Wherer文作成
        Dim max As Integer = inTbl.Rows.Count - 1
        Dim sSql As String = String.Empty
        For i As Integer = 0 To max
            If i = 0 Then
                sSql = String.Concat(" AND ( UNSO_NO_L = ", "'", inTbl.Rows(i).Item("UNSO_NO_L"), "'")
            Else
                sSql = String.Concat(sSql, " OR UNSO_NO_L = ", "'", inTbl.Rows(i).Item("UNSO_NO_L"), "'")
            End If
        Next

        If String.IsNullOrEmpty(sSql) = False Then
            '閉じかっこをつける
            sSql = String.Concat(sSql, " ) ")
        End If

        'SQL作成
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_SHIHARAI)                'SQL構築 SELECT句
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_FROM_SHIHARAI2)          'SQL構築 FROM句
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_WHERE_SHIHARAI2 & sSql)  'SQL構築 WHERE句

        'パラメータの設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", inTbl.Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF030DAC", "SelectShiharaiData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SHIHARAI_SYARYO_KB", "SHIHARAI_SYARYO_KB")
        map.Add("SHIHARAI_PKG_UT", "SHIHARAI_PKG_UT")
        map.Add("SHIHARAI_NG_NB", "SHIHARAI_NG_NB")
        map.Add("SHIHARAI_DANGER_KB", "SHIHARAI_DANGER_KB")
        map.Add("SHIHARAI_TARIFF_BUNRUI_KB", "SHIHARAI_TARIFF_BUNRUI_KB")
        map.Add("SHIHARAI_TARIFF_CD", "SHIHARAI_TARIFF_CD")
        map.Add("SHIHARAI_ETARIFF_CD", "SHIHARAI_ETARIFF_CD")
        map.Add("SHIHARAI_KYORI", "SHIHARAI_KYORI")
        map.Add("SHIHARAI_WT", "SHIHARAI_WT")
        map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
        map.Add("SHIHARAI_CITY_EXTC", "SHIHARAI_CITY_EXTC")
        map.Add("SHIHARAI_WINT_EXTC", "SHIHARAI_WINT_EXTC")
        map.Add("SHIHARAI_RELY_EXTC", "SHIHARAI_RELY_EXTC")
        map.Add("SHIHARAI_TOLL", "SHIHARAI_TOLL")
        map.Add("SHIHARAI_INSU", "SHIHARAI_INSU")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("KANRI_UNCHIN", "KANRI_UNCHIN")
        map.Add("KANRI_CITY_EXTC", "KANRI_CITY_EXTC")
        map.Add("KANRI_WINT_EXTC", "KANRI_WINT_EXTC")
        map.Add("KANRI_RELY_EXTC", "KANRI_RELY_EXTC")
        map.Add("KANRI_TOLL", "KANRI_TOLL")
        map.Add("KANRI_INSU", "KANRI_INSU")
        map.Add("REMARK", "REMARK")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SAGYO_KANRI", "SAGYO_KANRI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF030DAC.TABLE_NM_SHIHARAI)

        reader.Close()

        Return ds

    End Function
    '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End



    ''' <summary>
    ''' 運送(特大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(特大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLLData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMF030DAC.SelectCondition) As DataSet

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "TRIP_NO" _
        '                                    , "TRIP_DATE" _
        '                                    , "BIN_KB" _
        '                                    , "CAR_KEY" _
        '                                    , "CAR_NO" _
        '                                    , "CAR_TP_KB" _
        '                                    , "ONDO_MM" _
        '                                    , "ONDO_MX" _
        '                                    , "LOAD_WT" _
        '                                    , "INSPC_DATE_TRUCK" _
        '                                    , "INSPC_DATE_TRAILER" _
        '                                    , "CAR_TP_NM" _
        '                                    , "UNSOCO_CD" _
        '                                    , "UNSOCO_BR_CD" _
        '                                    , "UNSOCO_NM" _
        '                                    , "UNSOCO_BR_NM" _
        '                                    , "JSHA_KB" _
        '                                    , "HAISO_KB" _
        '                                    , "DRIVER_CD" _
        '                                    , "DRIVER_NM" _
        '                                    , "REMARK" _
        '                                    , "UNSO_ONDO" _
        '                                    , "PAY_UNCHIN" _
        '                                    , "UNSO_PKG_NB" _
        '                                    , "UNSO_WT" _
        '                                    , "DECI_UNCHIN" _
        '                                    , "PAY_TARIFF_CD" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    }
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "TRIP_NO" _
                                            , "TRIP_DATE" _
                                            , "BIN_KB" _
                                            , "CAR_KEY" _
                                            , "CAR_NO" _
                                            , "CAR_TP_KB" _
                                            , "ONDO_MM" _
                                            , "ONDO_MX" _
                                            , "LOAD_WT" _
                                            , "INSPC_DATE_TRUCK" _
                                            , "INSPC_DATE_TRAILER" _
                                            , "CAR_TP_NM" _
                                            , "UNSOCO_CD" _
                                            , "UNSOCO_BR_CD" _
                                            , "UNSOCO_NM" _
                                            , "UNSOCO_BR_NM" _
                                            , "JSHA_KB" _
                                            , "HAISO_KB" _
                                            , "DRIVER_CD" _
                                            , "DRIVER_NM" _
                                            , "REMARK" _
                                            , "UNSO_ONDO" _
                                            , "PAY_UNCHIN" _
                                            , "UNSO_PKG_NB" _
                                            , "UNSO_WT" _
                                            , "DECI_UNCHIN" _
                                            , "PAY_TARIFF_CD" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "SHIHARAI_TARIFF_CD" _
                                            , "SHIHARAI_ETARIFF_CD" _
                                            , "SHIHARAI_UNSO_WT" _
                                            , "SHIHARAI_COUNT" _
                                            , "SHIHARAI_UNCHIN" _
                                            , "SHIHARAI_TARIFF_BUNRUI_KB" _
                                            , "TEHAI_SYUBETSU" _
                                            }
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        Return Me.SelectListData(ds, LMF030DAC.TABLE_NM_UNSO_LL, sql, ptn, str)

    End Function

#End Region

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLInitData(ByVal ds As DataSet) As DataSet

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L1)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_UNSO_L"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L2)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_UNCHIN_TRS"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L3)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_SHIHARAI_TRS"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L4)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append("  AND @TRIP_NO IN ( F02_01.TRIP_NO , F02_01.TRIP_NO_SYUKA , F02_01.TRIP_NO_TYUKEI , F02_01.TRIP_NO_HAIKA ) ")
        Me._StrSql.Append(vbNewLine)

        Return Me.SelectUnsoLData(ds, Me._StrSql.ToString(), LMF030DAC.SelectCondition.PTN1, LMConst.FLG.OFF)

    End Function

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運送(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectNewUnsoLData(ByVal ds As DataSet) As DataSet

        Me._StrSql = New StringBuilder()
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L1)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_UNSO_L"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L2)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_UNCHIN_TRS"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L3)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(Me.GetUnsoLSchema(ds, "F_SHIHARAI_TRS"))
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_L4)
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(ds.Tables(LMF030DAC.TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString())
        Me._StrSql.Append(vbNewLine)

        Return Me.SelectUnsoLData(ds, Me._StrSql.ToString(), LMF030DAC.SelectCondition.PTN3, LMConst.FLG.OFF)

    End Function

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <param name="upKbn">UP_KBN</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet _
                                     , ByVal sql As String _
                                     , ByVal ptn As LMF030DAC.SelectCondition _
                                     , ByVal upKbn As String _
                                     ) As DataSet

        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "UNSO_NO_L" _
        '                                    , "TRIP_NO_SYUKA" _
        '                                    , "TRIP_NO_TYUKEI" _
        '                                    , "TRIP_NO_HAIKA" _
        '                                    , "TRIP_NO" _
        '                                    , "BIN_KB" _
        '                                    , "BIN_NM" _
        '                                    , "AREA_CD" _
        '                                    , "AREA_NM" _
        '                                    , "ARR_PLAN_DATE" _
        '                                    , "ORIG_CD" _
        '                                    , "ORIG_NM" _
        '                                    , "ORIG_JIS_CD" _
        '                                    , "ORIG_AD_1" _
        '                                    , "DEST_CD" _
        '                                    , "DEST_NM" _
        '                                    , "DEST_JIS_CD" _
        '                                    , "DEST_AD_1" _
        '                                    , "UNSO_PKG_NB" _
        '                                    , "UNSO_WT" _
        '                                    , "UNCHIN" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "CUST_NM_L" _
        '                                    , "CUST_NM_M" _
        '                                    , "CUST_REF_NO" _
        '                                    , "INOUTKA_NO_L" _
        '                                    , "MOTO_DATA_KB" _
        '                                    , "MOTO_DATA_NM" _
        '                                    , "REMARK" _
        '                                    , "TARIFF_BUNRUI_KB" _
        '                                    , "TARIFF_BUNRUI_NM" _
        '                                    , "UNSO_CD" _
        '                                    , "UNSO_BR_CD" _
        '                                    , "UNSO_NM" _
        '                                    , "UNSO_BR_NM" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "UNSO_ONDO_NM" _
        '                                    , "TYUKEI_HAISO_FLG" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "UP_KBN" _
        '                                    , "SYS_DEL_FLG" _
        '                                    }
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "TRIP_NO_SYUKA" _
                                            , "TRIP_NO_TYUKEI" _
                                            , "TRIP_NO_HAIKA" _
                                            , "TRIP_NO" _
                                            , "BIN_KB" _
                                            , "BIN_NM" _
                                            , "AREA_CD" _
                                            , "AREA_NM" _
                                            , "ARR_PLAN_DATE" _
                                            , "ORIG_CD" _
                                            , "ORIG_NM" _
                                            , "ORIG_JIS_CD" _
                                            , "ORIG_AD_1" _
                                            , "DEST_CD" _
                                            , "DEST_NM" _
                                            , "DEST_JIS_CD" _
                                            , "DEST_AD_1" _
                                            , "UNSO_PKG_NB" _
                                            , "UNSO_WT" _
                                            , "UNCHIN" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "CUST_REF_NO" _
                                            , "INOUTKA_NO_L" _
                                            , "MOTO_DATA_KB" _
                                            , "MOTO_DATA_NM" _
                                            , "REMARK" _
                                            , "TARIFF_BUNRUI_KB" _
                                            , "TARIFF_BUNRUI_NM" _
                                            , "UNSO_CD" _
                                            , "UNSO_BR_CD" _
                                            , "UNSO_NM" _
                                            , "UNSO_BR_NM" _
                                            , "UNSO_ONDO_KB" _
                                            , "UNSO_ONDO_NM" _
                                            , "TYUKEI_HAISO_FLG" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "UP_KBN" _
                                            , "SYS_DEL_FLG" _
                                            , "SHIHARAI_UNCHIN" _
                                            , "OUTKA_PLAN_DATE" _
                                            }
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        '並び順設定
        sql = String.Concat(sql, "ORDER BY F02_01.NRS_BR_CD , F02_01.UNSO_NO_L ")

        ds = Me.SelectListData(ds, LMF030DAC.TABLE_NM_UNSO_L, sql, ptn, str, upKbn)

        '他社倉庫名称の設定
        ds = SelectTasyaWhNm(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)レコード存在チェック用検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL構築
        Me._StrSql.Append(" SELECT COUNT(NRS_BR_CD) AS REC_CNT ")
        Me._StrSql.Append(" FROM $LM_TRN$..F_UNSO_L ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" WHERE NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND UNSO_NO_L = @UNSO_NO_L ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "SHIHARAI_TRS"

#Region "支払運賃の検索"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃の検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiInitData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_SHIHARAI)       'SQL構築 SELECT句
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_FROM_SHIHARAI)  'SQL構築 FROM句
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_WHERE_SHIHARAI) 'SQL構築 WHERE句
        Me._StrSql.Append(LMF030DAC.SQL_SELECT_ORDER_SHIHARAI) 'SQL構築 ORDER句

        'パラメータの設定
        Call SetSelectShiharaiParam(inTbl.Rows(0))             '条件設定

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF030DAC", "SelectShiharaiInitData", cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("YUSO_BR_CD", "YUSO_BR_CD")
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("CUST_CD_S", "CUST_CD_S")
        map.Add("CUST_CD_SS", "CUST_CD_SS")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SHIHARAI_SYARYO_KB", "SHIHARAI_SYARYO_KB")
        map.Add("SHIHARAI_PKG_UT", "SHIHARAI_PKG_UT")
        map.Add("SHIHARAI_NG_NB", "SHIHARAI_NG_NB")
        map.Add("SHIHARAI_DANGER_KB", "SHIHARAI_DANGER_KB")
        map.Add("SHIHARAI_TARIFF_BUNRUI_KB", "SHIHARAI_TARIFF_BUNRUI_KB")
        map.Add("SHIHARAI_TARIFF_CD", "SHIHARAI_TARIFF_CD")
        map.Add("SHIHARAI_ETARIFF_CD", "SHIHARAI_ETARIFF_CD")
        map.Add("SHIHARAI_KYORI", "SHIHARAI_KYORI")
        map.Add("SHIHARAI_WT", "SHIHARAI_WT")
        map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
        map.Add("SHIHARAI_CITY_EXTC", "SHIHARAI_CITY_EXTC")
        map.Add("SHIHARAI_WINT_EXTC", "SHIHARAI_WINT_EXTC")
        map.Add("SHIHARAI_RELY_EXTC", "SHIHARAI_RELY_EXTC")
        map.Add("SHIHARAI_TOLL", "SHIHARAI_TOLL")
        map.Add("SHIHARAI_INSU", "SHIHARAI_INSU")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("KANRI_UNCHIN", "KANRI_UNCHIN")
        map.Add("KANRI_CITY_EXTC", "KANRI_CITY_EXTC")
        map.Add("KANRI_WINT_EXTC", "KANRI_WINT_EXTC")
        map.Add("KANRI_RELY_EXTC", "KANRI_RELY_EXTC")
        map.Add("KANRI_TOLL", "KANRI_TOLL")
        map.Add("KANRI_INSU", "KANRI_INSU")
        map.Add("REMARK", "REMARK")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SAGYO_KANRI", "SAGYO_KANRI")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF030DAC.TABLE_NM_SHIHARAI)

        reader.Close()

        Return ds

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#End Region

#Region "排他チェック"

    ''' <summary>
    ''' 運送(特大)の排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaUnsoLLData(ByVal ds As DataSet) As DataSet

        Return Me.SelectHaitaData(ds, LMF030DAC.TABLE_NM_UNSO_LL)

    End Function

    ''' <summary>
    ''' 運送(大)の排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaUnsoLData(ByVal ds As DataSet) As DataSet

        Return Me.SelectHaitaData(ds, LMF030DAC.TABLE_NM_UNSO_L)

    End Function

    ''' <summary>
    ''' 排他検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaData(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(tblNm)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL構築
        Me._StrSql.Append(" SELECT COUNT(NRS_BR_CD) AS REC_CNT ")
        Me._StrSql.Append(vbNewLine)
        If LMF030DAC.TABLE_NM_UNSO_L.Equals(tblNm) = True Then

            Me._StrSql.Append(" FROM $LM_TRN$..F_UNSO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" WHERE UNSO_NO_L = @UNSO_NO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        Else

            Me._StrSql.Append(" FROM $LM_TRN$..F_UNSO_LL ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" WHERE TRIP_NO = @TRIP_NO ")
            Me._StrSql.Append(vbNewLine)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me._Row.Item("TRIP_NO").ToString(), DBDataType.CHAR))

        End If

        Me._StrSql.Append(" AND NRS_BR_CD = @NRS_BR_CD ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND SYS_DEL_FLG = '0' ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND SYS_UPD_DATE = @GUI_UPD_DATE ")
        Me._StrSql.Append(vbNewLine)
        Me._StrSql.Append(" AND SYS_UPD_TIME = @GUI_UPD_TIME ")
        Me._StrSql.Append(vbNewLine)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

        'パラメータ設定
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "SelectKbnData"

    ''' <summary>
    ''' 区分マスタ取得
    ''' </summary>
    ''' <param name="kbnGrpCd">String</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnData(ByVal ds As DataSet, ByVal kbnGrpCd As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", kbnGrpCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF030DAC.SQL_SELECT_KBN_DATA, brCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        map.Add("VALUE1", "VALUE1")
        map.Add("VALUE2", "VALUE2")
        map.Add("VALUE3", "VALUE3")
        map.Add("SORT", "SORT")
        map.Add("REM", "REM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF030DAC.TABLE_NM_KBN)

        Return ds

    End Function

#End Region

#Region "車輌マスタ"

    '2022.09.06 追加START
    ''' <summary>
    ''' 車輌マスタ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCarData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN_CAR)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim carKey As String = Me._Row.Item("CAR_KEY").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CAR_KEY", carKey, DBDataType.CHAR))

        'スキーマ名設定
        Me._StrSql.AppendLine(Me.SetSchemaNm(LMF030DAC.SQL_SELECT_CAR_DATA, brCd))

        'SQL文のコンパイル
        Dim sql As String = Me._StrSql.ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CAR_KEY", "CAR_KEY")
        map.Add("DAY_UNCHIN", "DAY_UNCHIN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF030DAC.TABLE_NM_OUT_CAR)

        Return ds

    End Function
    '2022.09.06 追加END

#End Region

#End Region

#Region "設定処理"

#Region "Insert"

#Region "UNSO_LL"

    ''' <summary>
    ''' 運送(特大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(特大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoLLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_LL)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF030DAC.SQL_INSERT_UNSO_LL, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetUnsoLLComParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim rtn As Integer = MyBase.GetInsertResult(cmd)

        Return ds

    End Function

#End Region

#End Region

#Region "Update"

#Region "UNSO_LL"

    ''' <summary>
    ''' 運送(特大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(特大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_LL)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF030DAC.SQL_UPDATE_UNSO_LL, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLLComParameter(Me._SqlPrmList, Me._Row)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

    '要望番号1269 2012.07.12 追加START umano
#Region "UNSO_L(該当区分運行番号初期値更新)"

    ''' <summary>
    ''' 運送(大)テーブル更新(該当区分運行番号初期値更新)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLDefaultData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_LL)

        'For i As Integer = 0 To LMF030DAC.HAISOKBN_CNT

        '    'SQL文のコンパイル
        '    Dim cmd As SqlCommand = Nothing

        '    '更新するレコードを設定
        '    Me._Row = inTbl.Rows(0)

        '    'SQLパラメータ初期化
        '    Me._SqlPrmList = New ArrayList()

        '    'スキーマが違うため都度SQL構築
        '    Select Case i

        '        Case LMF030DAC.NORMAL
        '            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_L, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_NORMAL, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_WHERE_NORMAL), _
        '                                                       Me._Row.Item("NRS_BR_CD").ToString()))


        '        Case LMF030DAC.SYUKA
        '            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_L, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_SYUKA, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_WHERE_SYUKA), _
        '                                                       Me._Row.Item("NRS_BR_CD").ToString()))

        '        Case LMF030DAC.TYUKEI
        '            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_L, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_TYUKEI, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_WHERE_TYUKEI), _
        '                                                       Me._Row.Item("NRS_BR_CD").ToString()))

        '        Case LMF030DAC.HAIKA
        '            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_L, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_HAIKA, _
        '                                                       LMF030DAC.SQL_UPDATE_UNSO_WHERE_HAIKA), _
        '                                                       Me._Row.Item("NRS_BR_CD").ToString()))

        '    End Select


        '    'パラメータの初期化
        '    cmd.Parameters.Clear()

        '    'パラメータ設定
        '    Call Me.SetUnsoLLComParameter(Me._SqlPrmList, Me._Row)

        '    'パラメータの反映
        '    For Each obj As Object In Me._SqlPrmList
        '        cmd.Parameters.Add(obj)
        '    Next

        '    MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        '    'SQLの発行
        '    '(初期化設定なので)排他チェックは行わない
        '    'If Me.UpdateResultChk(cmd) = False Then
        '    '    Return ds
        '    'End If
        '    MyBase.GetUpdateResult(cmd)

        'Next

        '■■■速度改善
        '上記のやりかたの場合、TRIP_NO、TRIP_NO_SYUKA、TRIP_NO_TYUKEI、TRIP_NO_HAIKAの初期化を1個ずつUPDATE、計4回行っていた。
        'なので、1回のUPDATEでTRIP_NO、TRIP_NO_SYUKA、TRIP_NO_TYUKEI、TRIP_NO_HAIKAの4項目すべてを初期化するように変更
        'データによるが、最大で約30秒の速度改善
        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing

        '更新するレコードを設定
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'スキーマが違うため都度SQL構築
        cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(String.Concat(LMF030DAC.SQL_UPDATE_UNSO_DEFAULT_L2, _
                                                                   LMF030DAC.SQL_UPDATE_UNSO_WHERE), _
                                                                   Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの初期化
        cmd.Parameters.Clear()

        'パラメータ設定
        Call Me.SetUnsoLLComParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        '(初期化設定なので)排他チェックは行わない
        'If Me.UpdateResultChk(cmd) = False Then
        '    Return ds
        'End If
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region
    '要望番号1269 2012.07.12 追加END umano

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_L)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing

        '行数分更新
        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max


            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
            '削除レコードでも支払タリフの書き換えがあるので以下をコメント化
            ''START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            'If (LMConst.FLG.ON).Equals(inTbl.Rows(i).Item("SYS_DEL_FLG").ToString) = True Then
            '    '削除レコード時は処理を飛ばす
            '    Continue For
            'End If
            ''END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

            '更新するレコードを設定
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'スキーマが違うため都度SQL構築
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF030DAC.SQL_UPDATE_UNSO_L, Me._Row.Item("NRS_BR_CD").ToString()))

            'パラメータの初期化
            cmd.Parameters.Clear()

            'パラメータ設定
            Call Me.SetUnsoLComParameter(Me._SqlPrmList, Me._Row)
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

#End Region

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateShiharai(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_SHIHARAI)

        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
        '０件はスルーする
        If inTbl.Rows.Count < 1 Then
            Return ds
        End If
        '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF030DAC.SQL_UPDATE_SHIHARAI_KEISAN)         'SQL構築(UPDATE句)
        
        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ設定
            Call Me.SetUpdShiharaiParam(inTbl.Rows(i))
            Call Me.SetSysdataParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF030DAC", "UpdateShiharai", cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    'START KIM 要望番号1485 支払い関連修正
    ''' <summary>
    ''' 支払先更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShiharaisakiSaveAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_SHIHARAI)

        '０件はスルーする
        If inTbl.Rows.Count < 1 Then
            Return ds
        End If

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF030DAC.SQL_UPDATE_SHIHARAI_HOZON)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", Me._Row.Item("SHIHARAITO_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Call Me.SetSysdataParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF030DAC", "ShiharaisakiSaveAction", cmd)

            'START KIM 要望番号1524
            'SQLの発行
            'If Me.UpdateResultChk(cmd) = False Then
            '    Return ds
            'End If
            MyBase.GetUpdateResult(cmd)
            'END KIM 要望番号1524

        Next

        Return ds

    End Function
    'END KIM 要望番号1485 支払い関連修正


#Region "計算時の更新"

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 運送(大)テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoLKeisan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN_KEISAN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        If ("01").Equals(inTbl.Rows(0).Item("SHIHARAI_KEISAN_KB").ToString) = True Then
            Me._StrSql.Append(LMF030DAC.SQL_UPDATE_UNSO_L_KEISAN1)         'SQL構築(UPDATE句)
        Else
            Me._StrSql.Append(LMF030DAC.SQL_UPDATE_UNSO_L_KEISAN2)         'SQL構築(UPDATE句)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ設定
            Call Me.SetUpdUnsoLKeisanParam(inTbl.Rows(i))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF030DAC", "UpdateUnsoLKeisan", cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 支払運賃テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateShiharaiKeisan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN_KEISAN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        If ("01").Equals(inTbl.Rows(0).Item("SHIHARAI_KEISAN_KB").ToString) = True Then
            If String.IsNullOrEmpty(inTbl.Rows(0).Item("SHIHARAITO_CD").ToString()) = False Then
                Me._StrSql.Append(LMF030DAC.SQL_UPDATE_SHIHARAI_KEISAN1)         'SQL構築(UPDATE句)
            Else
                Me._StrSql.Append(LMF030DAC.SQL_UPDATE_SHIHARAI_KEISAN3)         'SQL構築(UPDATE句)
            End If
        Else
            Me._StrSql.Append(LMF030DAC.SQL_UPDATE_SHIHARAI_KEISAN2)         'SQL構築(UPDATE句)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'パラメータの初期化
            cmd.Parameters.Clear()

            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'SQLパラメータ設定
            Call Me.SetUpdShiharaiKeisanParam(inTbl.Rows(i))
            Call Me.SetSysdataParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog("LMF030DAC", "UpdateShiharaiKeisan", cmd)

            'SQLの発行
            If Me.UpdateResultChk(cmd) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送(特大)テーブルの更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoLLKeisan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN_KEISAN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF030DAC.SQL_UPDATE_UNSO_LL_KEISAN)         'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)


        'パラメータの初期化
        cmd.Parameters.Clear()

        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ設定
        Call Me.SetUpdUnsoLLKeisanParam(inTbl.Rows(0))
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF030DAC", "UpdateUnsoLLKeisan", cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = False Then
            Return ds
        End If

        Return ds

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#End Region

#Region "Delete"

#Region "UNSO_LL"

    ''' <summary>
    ''' 運送(特大)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_LL)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF030DAC.SQL_DELETE_UNSO_LL _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList)
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, LMF030DAC.SelectCondition.PTN1)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#End Region

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
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">抽出パターン</param>
    ''' <param name="str">列名配列</param>
    ''' <param name="upKbn">UP_KBN</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet _
                                    , ByVal tblNm As String _
                                    , ByVal sql As String _
                                    , ByVal ptn As LMF030DAC.SelectCondition _
                                    , ByVal str As String() _
                                    , Optional ByVal upKbn As String = "" _
                                    ) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        If LMF030DAC.SelectCondition.PTN3 <> ptn Then

            'パラメータ設定
            Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        End If

        '運送検索の場合
        If String.IsNullOrEmpty(upKbn) = False Then
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UP_KBN", upKbn, DBDataType.CHAR))
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me.GetSchemaBr(Me._Row, ptn)))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
    ''' 他社倉庫名称の設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectTasyaWhNm(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF030DAC.TABLE_NM_UNSO_L)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        For Each dr As DataRow In inTbl.Rows
            If Not String.IsNullOrEmpty(dr.Item("INOUTKA_NO_L").ToString()) Then
                '入出荷管理番号Lに値がある
                If dr.Item("ORIG_CD").ToString() = "999999999999999" Then
                    '出荷から取得
                    strSql.Append(LMF030DAC.SQL_SELECT_TASYA_WH_NM_OUTKA_L1)
                    strSql.Append(vbNewLine)
                    strSql.Append(Me.GetUnsoLSchema(ds, "C_OUTKA_L"))
                    strSql.Append(vbNewLine)
                    strSql.Append(LMF030DAC.SQL_SELECT_TASYA_WH_NM_OUTKA_L2)
                    strSql.Append(vbNewLine)
                    strSql.Append(Me.GetUnsoLSchema(ds, "C_OUTKA_S"))
                    strSql.Append(vbNewLine)
                    strSql.Append(LMF030DAC.SQL_SELECT_TASYA_WH_NM_OUTKA_L3)
                ElseIf dr.Item("DEST_CD").ToString() = "999999999999999" Then
                    '入荷から取得
                    strSql.Append(LMF030DAC.SQL_SELECT_TASYA_WH_NM_INKA_L1)
                    strSql.Append(vbNewLine)
                    strSql.Append(Me.GetUnsoLSchema(ds, "B_INKA_L"))
                    strSql.Append(vbNewLine)
                    strSql.Append(LMF030DAC.SQL_SELECT_TASYA_WH_NM_INKA_L2)
                    strSql.Append(vbNewLine)
                    strSql.Append(Me.GetUnsoLSchema(ds, "B_INKA_S"))
                    strSql.Append(vbNewLine)
                    strSql.Append(LMF030DAC.SQL_SELECT_TASYA_WH_NM_INKA_L3)
                End If

                'SQLが生成された場合
                If strSql.Length > 0 Then
                    'SQLパラメータ設定
                    Me._SqlPrmList = New ArrayList()
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", dr.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", dr.Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

                    'SQL文のコンパイル
                    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(strSql.ToString(), dr.Item("NRS_BR_CD").ToString()))

                    'パラメータの反映
                    For Each obj As Object In Me._SqlPrmList
                        cmd.Parameters.Add(obj)
                    Next

                    MyBase.Logger.WriteSQLLog(LMF030DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                    'テーブルクリア
                    ds.Tables(TABLE_NM_TASYA_WH_NM).Rows.Clear()

                    'SQLの発行
                    Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)
                        If reader.HasRows Then
                            '取得データの格納先をマッピング
                            Dim map As Hashtable = New Hashtable()
                            For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                                If (ds.Tables(TABLE_NM_TASYA_WH_NM).Columns.Contains(item)) Then
                                    map.Add(item, item)
                                End If
                            Next

                            'DataReader→DataTableへの転記
                            ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_TASYA_WH_NM)
                        End If
                    End Using

                    If ds.Tables(TABLE_NM_TASYA_WH_NM).Rows.Count > 0 Then
                        dr.Item("TASYA_WH_NM") = ds.Tables(TABLE_NM_TASYA_WH_NM).Rows(0).Item("TASYA_WH_NM").ToString()
                    End If
                End If
            End If

        Next

        Return ds

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
    ''' 排他チェック
    ''' </summary>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer) As Boolean

        '判定
        If cnt < 1 Then
            MyBase.SetMessage("E011")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' スキーマ用営業所取得
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <param name="ptn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetSchemaBr(ByVal dr As DataRow, ByVal ptn As LMF030DAC.SelectCondition) As String

        Dim rtnBr As String = Me._Row.Item("NRS_BR_CD").ToString()
        Select Case ptn

            Case LMF030DAC.SelectCondition.PTN1, LMF030DAC.SelectCondition.PTN2, LMF030DAC.SelectCondition.PTN4

                rtnBr = dr.Item("NRS_BR_CD").ToString()

        End Select

        Return rtnBr

    End Function

    ''' <summary>
    ''' 運行データの営業所またぎ対応
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnsoLSchema(ByVal ds As DataSet, ByVal tblNM As String) As String

        Dim rtnFrom As String = String.Empty
        '区分マスタ(U026)の取得
        ds = Me.SelectKbnData(ds, "U026")

        '件数０件の場合、自営業所のみ
        If ds.Tables(LMF030DAC.TABLE_NM_KBN).Rows.Count = 0 Then
            rtnFrom = String.Concat("$LM_TRN$..", tblNM)
            Return rtnFrom
        End If

        '０件ではない場合、またぎ営業所のスキーマを設定
        Dim matagiBr As String = String.Empty
        rtnFrom = String.Concat("(SELECT * FROM $LM_TRN$..", tblNM, " ")
        For i As Integer = 0 To ds.Tables(LMF030DAC.TABLE_NM_KBN).Rows.Count - 1
            matagiBr = ds.Tables(LMF030DAC.TABLE_NM_KBN).Rows(i).Item("KBN_NM2").ToString()
            rtnFrom = String.Concat(rtnFrom, " UNION SELECT * FROM ", MyBase.GetDatabaseName(matagiBr, DBKbn.TRN), "..", tblNM)
        Next
        rtnFrom = String.Concat(rtnFrom, ")")

        Return rtnFrom

    End Function
#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMF030DAC.SelectCondition)

        With dr

            Select Case ptn

                Case LMF030DAC.SelectCondition.PTN1

                    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))

                Case LMF030DAC.SelectCondition.PTN2

                    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@ARR_PLAN_DATE", .Item("ARR_PLAN_DATE").ToString(), DBDataType.CHAR))

                    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 Start
                Case LMF030DAC.SelectCondition.PTN4
                    prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CAR_KEY", .Item("CAR_KEY").ToString(), DBDataType.CHAR))
                    '要望番号:1205(車番入力後、各種情報を取得する) 2012/06/29 本明 End

            End Select

        End With

    End Sub

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
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 論理削除時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateDelFlgParameter(ByVal prmList As ArrayList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.ON, DBDataType.CHAR))
        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 排他の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <remarks></remarks>
    Private Sub SetGuiSysdataTimeParameter(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            '更新日時
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(特大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLLComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", .Item("UNSOCO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", .Item("UNSOCO_BR_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@JSHA_KB", .Item("JSHA_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CAR_KEY", .Item("CAR_KEY").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO", .Item("UNSO_ONDO").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", .Item("DRIVER_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_DATE", .Item("TRIP_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@PAY_UNCHIN", .Item("PAY_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@PAY_TARIFF_CD", .Item("PAY_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@HAISO_KB", .Item("HAISO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        '運送(大)は運行番号のみ
        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            '要望番号:1242 terakawa 2012.07.05 Start
            prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))
            'START YANAI 要望番号1362 運行情報入力 の保存時に、運送画面の運送会社を更新しない
            'prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.CHAR))
            'prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.CHAR))
            'END YANAI 要望番号1362 運行情報入力 の保存時に、運送画面の運送会社を更新しない
            '要望番号:1242 terakawa 2012.07.05 End
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", .Item("TRIP_NO_SYUKA").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", .Item("TRIP_NO_TYUKEI").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", .Item("TRIP_NO_HAIKA").ToString(), DBDataType.CHAR))

            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        End With

    End Sub

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払運賃の検索パラメータ設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSelectShiharaiParam(ByVal dr As DataRow)

        With dr

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)の検索パラメータ設定(計算時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUpdUnsoLKeisanParam(ByVal dr As DataRow)

        With dr

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@GUI_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

            If ("01").Equals(.Item("SHIHARAI_KEISAN_KB").ToString()) = True Then
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 支払運賃の検索パラメータ設定(計算時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUpdShiharaiKeisanParam(ByVal dr As DataRow)

        With dr

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

            If ("01").Equals(.Item("SHIHARAI_KEISAN_KB").ToString()) = True Then
                'START YANAI 要望番号1424 支払処理
                'If String.IsNullOrEmpty(.Item("SHIHARAITO_CD").ToString()) = False Then
                '    '支払先コードに値が設定されている場合は更新
                '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.NVARCHAR))
                '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
                'End If
                If String.IsNullOrEmpty(.Item("SHIHARAITO_CD").ToString()) = False Then
                    '支払先コードに値が設定されている場合は更新
                    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.NVARCHAR))
                End If
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
                'END YANAI 要望番号1424 支払処理
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_BUNRUI_KB", .Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 運送(特大)の検索パラメータ設定(計算時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUpdUnsoLLKeisanParam(ByVal dr As DataRow)

        With dr

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNSO_WT", .Item("SHIHARAI_UNSO_WT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_COUNT", .Item("SHIHARAI_COUNT").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNCHIN", .Item("SHIHARAI_UNCHIN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_BUNRUI_KB", .Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 支払運賃の検索パラメータ設定(保存時)
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUpdShiharaiParam(ByVal dr As DataRow)

        With dr

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", .Item("DECI_NG_NB").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", .Item("DECI_KYORI").ToString(), DBDataType.NUMERIC))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@DECI_WT", .Item("DECI_WT").ToString(), DBDataType.NUMERIC))

        End With

    End Sub
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#End Region

End Class
