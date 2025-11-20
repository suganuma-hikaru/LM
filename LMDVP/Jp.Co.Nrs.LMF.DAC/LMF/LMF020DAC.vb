' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF020    : 運送編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF020DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF020DAC
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
    Private Const TABLE_NM_IN As String = "LMF020IN"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    ''' <summary>
    ''' F_UNSO_Mテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_M As String = "F_UNSO_M"

    ''' <summary>
    ''' F_UNCHINテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNCHIN As String = "F_UNCHIN_TRS"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' F_SHIHARAIテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SHIHARAI_UNCHIN As String = "F_SHIHARAI_TRS"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' UNCHIN_INFOテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_INFO As String = "UNCHIN_INFO"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' SHIHARAI_INFOテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SHIHARAI As String = "SHIHARAI_INFO"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' G_HEDテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' G_HED_CHKテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMF020DAC"

    ''' <summary>
    ''' 納品書
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_NOUHIN As String = "01"

    ''' <summary>
    ''' 送状
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_OKURI As String = "02"

    ''' <summary>
    ''' 荷札
    ''' </summary>
    ''' <remarks></remarks>
    Private Const PRINT_NIFUDA As String = "03"

    ''' <summary>
    ''' フラグ(ON)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FLG_ON As String = "01"

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 請求タリフ分類区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SEIQ_TARIFF_BUNRUI_KB_YOKOMOCHI As String = "40"
    '要望番号:1045 terakawa 2013.03.28 End

#End Region

#Region "検索処理 SQL"

#Region "UNSO_L0"

    Private Const SQL_SELECT_L0 As String = "SELECT                                                                                                   " & vbNewLine _
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

#End Region

#Region "UNSO_L1"

    'START YANAI 運送・運行・請求メモNo.41
    'Private Const SQL_SELECT_L1 As String = "SELECT                                                                 " & vbNewLine _
    '                                      & " F02_01.NRS_BR_CD                   AS      NRS_BR_CD                  " & vbNewLine _
    '                                      & ",F02_01.YUSO_BR_CD                  AS      YUSO_BR_CD                 " & vbNewLine _
    '                                      & ",F02_01.UNSO_NO_L                   AS      UNSO_NO_L                  " & vbNewLine _
    '                                      & ",F02_01.INOUTKA_NO_L                AS      INOUTKA_NO_L               " & vbNewLine _
    '                                      & ",F02_01.MOTO_DATA_KB                AS      MOTO_DATA_KB               " & vbNewLine _
    '                                      & ",F02_01.JIYU_KB                     AS      JIYU_KB                    " & vbNewLine _
    '                                      & ",F02_01.PC_KB                       AS      PC_KB                      " & vbNewLine _
    '                                      & ",F02_01.TAX_KB                      AS      TAX_KB                     " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO                     AS      TRIP_NO                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_TEHAI_KB               AS      UNSO_TEHAI_KB              " & vbNewLine _
    '                                      & ",F02_01.BIN_KB                      AS      BIN_KB                     " & vbNewLine _
    '                                      & ",F02_01.TARIFF_BUNRUI_KB            AS      TARIFF_BUNRUI_KB           " & vbNewLine _
    '                                      & ",F02_01.VCLE_KB                     AS      VCLE_KB                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_CD                     AS      UNSO_CD                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_BR_CD                  AS      UNSO_BR_CD                 " & vbNewLine _
    '                                      & ",M38_01.UNSOCO_NM                   AS      UNSO_NM                    " & vbNewLine _
    '                                      & ",M38_01.UNSOCO_BR_NM                AS      UNSO_BR_NM                 " & vbNewLine _
    '                                      & ",M38_01.TARE_YN                     AS      TARE_YN                    " & vbNewLine _
    '                                      & ",F02_01.DENP_NO                     AS      DENP_NO                    " & vbNewLine _
    '                                      & ",F02_01.CUST_CD_L                   AS      CUST_CD_L                  " & vbNewLine _
    '                                      & ",F02_01.CUST_CD_M                   AS      CUST_CD_M                  " & vbNewLine _
    '                                      & ",M07_01.CUST_NM_L                   AS      CUST_NM_L                  " & vbNewLine _
    '                                      & ",M07_01.CUST_NM_M                   AS      CUST_NM_M                  " & vbNewLine _
    '                                      & ",F02_01.CUST_REF_NO                 AS      CUST_REF_NO                " & vbNewLine _
    '                                      & ",F02_01.SHIP_CD                     AS      SHIP_CD                    " & vbNewLine _
    '                                      & ",M10_03.DEST_NM                     AS      SHIP_NM                    " & vbNewLine _
    '                                      & ",F02_01.BUY_CHU_NO                  AS      BUY_CHU_NO                 " & vbNewLine _
    '                                      & ",F02_01.SEIQ_TARIFF_CD              AS      SEIQ_TARIFF_CD             " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.TARIFF_BUNRUI_KB = '40' THEN M49_01.YOKO_REM         " & vbNewLine _
    '                                      & "                                          ELSE M47_01.UNCHIN_TARIFF_REM" & vbNewLine _
    '                                      & " END                                        TARIFF_REM                 " & vbNewLine _
    '                                      & ",F02_01.SEIQ_ETARIFF_CD             AS      SEIQ_ETARIFF_CD            " & vbNewLine _
    '                                      & ",M44_01.EXTC_TARIFF_REM             AS      EXTC_TARIFF_REM            " & vbNewLine _
    '                                      & ",F02_01.OUTKA_PLAN_DATE             AS      OUTKA_PLAN_DATE            " & vbNewLine _
    '                                      & ",F02_01.OUTKA_PLAN_TIME             AS      OUTKA_PLAN_TIME            " & vbNewLine _
    '                                      & ",F02_01.ORIG_CD                     AS      ORIG_CD                    " & vbNewLine _
    '                                      & ",M10_01.DEST_NM                     AS      ORIG_NM                    " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.MOTO_DATA_KB = '20' THEN M03_02.JIS_CD               " & vbNewLine _
    '                                      & "                                      ELSE M10_01.JIS                  " & vbNewLine _
    '                                      & " END                                        ORIG_JIS_CD                " & vbNewLine _
    '                                      & ",F02_01.ARR_PLAN_DATE               AS      ARR_PLAN_DATE              " & vbNewLine _
    '                                      & ",F02_01.ARR_PLAN_TIME               AS      ARR_PLAN_TIME              " & vbNewLine _
    '                                      & ",F02_01.ARR_ACT_TIME                AS      ARR_ACT_TIME               " & vbNewLine _
    '                                      & ",F02_01.DEST_CD                     AS      DEST_CD                    " & vbNewLine _
    '                                      & ",M10_02.DEST_NM                     AS      DEST_NM                    " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.MOTO_DATA_KB = '10' THEN M03_01.JIS_CD               " & vbNewLine _
    '                                      & "                                      ELSE M10_02.JIS                  " & vbNewLine _
    '                                      & " END                                        DEST_JIS_CD                " & vbNewLine _
    '                                      & ",M10_02.ZIP                         AS      ZIP                        " & vbNewLine _
    '                                      & ",M10_02.AD_1                        AS      AD_1                       " & vbNewLine _
    '                                      & ",M10_02.AD_2                        AS      AD_2                       " & vbNewLine _
    '                                      & ",F02_01.AD_3                        AS      AD_3                       " & vbNewLine _
    '                                      & ",F02_01.AREA_CD                     AS      AREA_CD                    " & vbNewLine _
    '                                      & ",M36_01.AREA_NM                     AS      AREA_NM                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_PKG_NB                 AS      UNSO_PKG_NB                " & vbNewLine _
    '                                      & ",F02_01.UNSO_WT                     AS      UNSO_WT                    " & vbNewLine _
    '                                      & ",F02_01.NB_UT                       AS      NB_UT                      " & vbNewLine _
    '                                      & ",F02_01.UNSO_ONDO_KB                AS      UNSO_ONDO_KB               " & vbNewLine _
    '                                      & ",F02_01.REMARK                      AS      REMARK                     " & vbNewLine _
    '                                      & ",F02_01.TYUKEI_HAISO_FLG            AS      TYUKEI_HAISO_FLG           " & vbNewLine _
    '                                      & ",F02_01.SYUKA_TYUKEI_CD             AS      SYUKA_TYUKEI_CD            " & vbNewLine _
    '                                      & ",F02_01.HAIKA_TYUKEI_CD             AS      HAIKA_TYUKEI_CD            " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO_SYUKA               AS      TRIP_NO_SYUKA              " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO_TYUKEI              AS      TRIP_NO_TYUKEI             " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO_HAIKA               AS      TRIP_NO_HAIKA              " & vbNewLine _
    '                                      & ",F02_01.SYS_UPD_DATE                AS      SYS_UPD_DATE               " & vbNewLine _
    '                                      & ",F02_01.SYS_UPD_TIME                AS      SYS_UPD_TIME               " & vbNewLine _
    '                                      & ",''                                 AS      PRINT_KB                   " & vbNewLine _
    '                                      & ",C01_01.OUTKA_STATE_KB              AS      OUTKA_STATE_KB             " & vbNewLine _
    '                                      & ",C01_01.SYS_UPD_DATE                AS      OUT_UPD_DATE               " & vbNewLine _
    '                                      & ",C01_01.SYS_UPD_TIME                AS      OUT_UPD_TIME               " & vbNewLine _
    '                                      & ",'0'                                AS      PRT_NB                     " & vbNewLine _
    '                                      & "FROM       $LM_TRN$..F_UNSO_L             F02_01                       " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..F_UNCHIN_TRS         F04_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = F04_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_NO_L                  = F04_01.UNSO_NO_L             " & vbNewLine _
    '                                      & " AND  F04_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..B_INKA_L             B01_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = B01_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.INOUTKA_NO_L               = B01_01.INKA_NO_L             " & vbNewLine _
    '                                      & " AND  B01_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..C_OUTKA_L            C01_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = C01_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.INOUTKA_NO_L               = C01_01.OUTKA_NO_L            " & vbNewLine _
    '                                      & " AND  C01_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_SOKO               M03_01                       " & vbNewLine _
    '                                      & "  ON  B01_01.WH_CD                      = M03_01.WH_CD                 " & vbNewLine _
    '                                      & " AND  M03_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_SOKO               M03_02                       " & vbNewLine _
    '                                      & "  ON  C01_01.WH_CD                      = M03_02.WH_CD                 " & vbNewLine _
    '                                      & " AND  M03_02.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_UNSOCO             M38_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M38_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_CD                    = M38_01.UNSOCO_CD             " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_BR_CD                 = M38_01.UNSOCO_BR_CD          " & vbNewLine _
    '                                      & " AND  M38_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_CUST               M07_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M07_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M07_01.CUST_CD_L             " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_M                  = M07_01.CUST_CD_M             " & vbNewLine _
    '                                      & " AND  M07_01.CUST_CD_S                  = '00'                         " & vbNewLine _
    '                                      & " AND  M07_01.CUST_CD_SS                 = '00'                         " & vbNewLine _
    '                                      & " AND  M07_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
    '                                      & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
    '                                      & "                      ,M47_02.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
    '                                      & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                                " & vbNewLine _
    '                                      & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                                 " & vbNewLine _
    '                                      & "                 INNER JOIN (                                                                                                           " & vbNewLine _
    '                                      & "                                    SELECT M47_02.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
    '                                      & "                                          ,M47_02.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
    '                                      & "                                          ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD                                         " & vbNewLine _
    '                                      & "                                          ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
    '                                      & "                                          ,M47_02.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
    '                                      & "                                      FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                             " & vbNewLine _
    '                                      & "                                INNER JOIN (                                                                                            " & vbNewLine _
    '                                      & "                                                    SELECT F02_11.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                          ,MAX(M47_01.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
    '                                      & "                                                      FROM $LM_TRN$..F_UNSO_L F02_11                                                    " & vbNewLine _
    '                                      & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_01                                                 " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD   = M07_01.NRS_BR_CD                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_L   = M07_01.CUST_CD_L                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_M   = M07_01.CUST_CD_M                                        " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_S   = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_SS  = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                      & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
    '                                      & "                                                                             SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
    '                                      & "                                                                               FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
    '                                      & "                                                                              WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                      & "                                                                           GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                                )                   M47_01                                              " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD       = M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                       AND F02_11.SEIQ_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                       AND CASE WHEN M07_01.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
    '                                      & "                                                                THEN F02_11.OUTKA_PLAN_DATE                                             " & vbNewLine _
    '                                      & "                                                                ELSE F02_11.ARR_PLAN_DATE                                               " & vbNewLine _
    '                                      & "                                                            END                  >= M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                     WHERE F02_11.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
    '                                      & "                                                  GROUP BY F02_11.NRS_BR_CD                                                             " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L                                                             " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
    '                                      & "                                           )                      M47_02                                                                " & vbNewLine _
    '                                      & "                                     ON M47_01.NRS_BR_CD        = M47_02.NRS_BR_CD                                                      " & vbNewLine _
    '                                      & "                                    AND M47_01.UNCHIN_TARIFF_CD = M47_02.UNCHIN_TARIFF_CD                                               " & vbNewLine _
    '                                      & "                                    AND M47_01.STR_DATE         = M47_02.STR_DATE                                                       " & vbNewLine _
    '                                      & "                                  WHERE M47_01.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
    '                                      & "                               GROUP BY M47_02.NRS_BR_CD                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_02.UNSO_NO_L                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_01.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
    '                                      & "                                       ,M47_02.STR_DATE                                                                                 " & vbNewLine _
    '                                      & "                            )                          M47_02                                                                           " & vbNewLine _
    '                                      & "                      ON M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                                                 " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
    '                                      & "                     AND M47_01.STR_DATE             = M47_02.STR_DATE                                                                  " & vbNewLine _
    '                                      & "                   WHERE M47_01.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
    '                                      & "          )                               M47_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M47_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_NO_L                  = M47_01.UNSO_NO_L     " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD     M49_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M49_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.SEIQ_TARIFF_CD             = M49_01.YOKO_TARIFF_CD" & vbNewLine _
    '                                      & " AND  M49_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                   " & vbNewLine _
    '                                      & "              SELECT NRS_BR_CD       AS NRS_BR_CD              " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD         " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM        " & vbNewLine _
    '                                      & "                FROM $LM_MST$..M_EXTC_UNCHIN                   " & vbNewLine _
    '                                      & "               WHERE JIS_CD      = '0000000'                   " & vbNewLine _
    '                                      & "                 AND SYS_DEL_FLG = '0'                         " & vbNewLine _
    '                                      & "            GROUP BY NRS_BR_CD                                 " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD                            " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM                           " & vbNewLine _
    '                                      & "           )                              M44_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M44_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.SEIQ_ETARIFF_CD            = M44_01.EXTC_TARIFF_CD" & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_DEST               M10_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M10_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M10_01.CUST_CD_L     " & vbNewLine _
    '                                      & " AND  F02_01.ORIG_CD                    = M10_01.DEST_CD       " & vbNewLine _
    '                                      & " AND  M10_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_DEST               M10_02               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M10_02.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M10_02.CUST_CD_L     " & vbNewLine _
    '                                      & " AND  F02_01.DEST_CD                    = M10_02.DEST_CD       " & vbNewLine _
    '                                      & " AND  M10_02.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_DEST               M10_03               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M10_03.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M10_03.CUST_CD_L     " & vbNewLine _
    '                                      & " AND  F02_01.SHIP_CD                    = M10_03.DEST_CD       " & vbNewLine _
    '                                      & " AND  M10_03.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_AREA               M36_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M36_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.AREA_CD                    = M36_01.AREA_CD       " & vbNewLine _
    '                                      & " AND  M10_02.JIS                        = M36_01.JIS_CD        " & vbNewLine _
    '                                      & " AND  M36_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "WHERE F02_01.NRS_BR_CD                  = @NRS_BR_CD           " & vbNewLine _
    '                                      & "  AND F02_01.UNSO_NO_L                  = @UNSO_NO_L           " & vbNewLine _
    '                                      & "  AND F02_01.SYS_DEL_FLG                = '0'                  " & vbNewLine
    'START YANAI 要望番号376
    'Private Const SQL_SELECT_L1 As String = "SELECT                                                                 " & vbNewLine _
    '                                      & " F02_01.NRS_BR_CD                   AS      NRS_BR_CD                  " & vbNewLine _
    '                                      & ",F02_01.YUSO_BR_CD                  AS      YUSO_BR_CD                 " & vbNewLine _
    '                                      & ",F02_01.UNSO_NO_L                   AS      UNSO_NO_L                  " & vbNewLine _
    '                                      & ",F02_01.INOUTKA_NO_L                AS      INOUTKA_NO_L               " & vbNewLine _
    '                                      & ",F02_01.MOTO_DATA_KB                AS      MOTO_DATA_KB               " & vbNewLine _
    '                                      & ",F02_01.JIYU_KB                     AS      JIYU_KB                    " & vbNewLine _
    '                                      & ",F02_01.PC_KB                       AS      PC_KB                      " & vbNewLine _
    '                                      & ",F02_01.TAX_KB                      AS      TAX_KB                     " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO                     AS      TRIP_NO                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_TEHAI_KB               AS      UNSO_TEHAI_KB              " & vbNewLine _
    '                                      & ",F02_01.BIN_KB                      AS      BIN_KB                     " & vbNewLine _
    '                                      & ",F02_01.TARIFF_BUNRUI_KB            AS      TARIFF_BUNRUI_KB           " & vbNewLine _
    '                                      & ",F02_01.VCLE_KB                     AS      VCLE_KB                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_CD                     AS      UNSO_CD                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_BR_CD                  AS      UNSO_BR_CD                 " & vbNewLine _
    '                                      & ",M38_01.UNSOCO_NM                   AS      UNSO_NM                    " & vbNewLine _
    '                                      & ",M38_01.UNSOCO_BR_NM                AS      UNSO_BR_NM                 " & vbNewLine _
    '                                      & ",M38_01.TARE_YN                     AS      TARE_YN                    " & vbNewLine _
    '                                      & ",F02_01.DENP_NO                     AS      DENP_NO                    " & vbNewLine _
    '                                      & ",F02_01.CUST_CD_L                   AS      CUST_CD_L                  " & vbNewLine _
    '                                      & ",F02_01.CUST_CD_M                   AS      CUST_CD_M                  " & vbNewLine _
    '                                      & ",M07_01.CUST_NM_L                   AS      CUST_NM_L                  " & vbNewLine _
    '                                      & ",M07_01.CUST_NM_M                   AS      CUST_NM_M                  " & vbNewLine _
    '                                      & ",F02_01.CUST_REF_NO                 AS      CUST_REF_NO                " & vbNewLine _
    '                                      & ",F02_01.SHIP_CD                     AS      SHIP_CD                    " & vbNewLine _
    '                                      & ",M10_03.DEST_NM                     AS      SHIP_NM                    " & vbNewLine _
    '                                      & ",F02_01.BUY_CHU_NO                  AS      BUY_CHU_NO                 " & vbNewLine _
    '                                      & ",F02_01.SEIQ_TARIFF_CD              AS      SEIQ_TARIFF_CD             " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.TARIFF_BUNRUI_KB = '40' THEN M49_01.YOKO_REM         " & vbNewLine _
    '                                      & "                                          ELSE M47_01.UNCHIN_TARIFF_REM" & vbNewLine _
    '                                      & " END                                        TARIFF_REM                 " & vbNewLine _
    '                                      & ",F02_01.SEIQ_ETARIFF_CD             AS      SEIQ_ETARIFF_CD            " & vbNewLine _
    '                                      & ",M44_01.EXTC_TARIFF_REM             AS      EXTC_TARIFF_REM            " & vbNewLine _
    '                                      & ",F02_01.OUTKA_PLAN_DATE             AS      OUTKA_PLAN_DATE            " & vbNewLine _
    '                                      & ",F02_01.OUTKA_PLAN_TIME             AS      OUTKA_PLAN_TIME            " & vbNewLine _
    '                                      & ",F02_01.ORIG_CD                     AS      ORIG_CD                    " & vbNewLine _
    '                                      & ",M10_01.DEST_NM                     AS      ORIG_NM                    " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.MOTO_DATA_KB = '20' THEN M03_02.JIS_CD               " & vbNewLine _
    '                                      & "                                      ELSE M10_01.JIS                  " & vbNewLine _
    '                                      & " END                                        ORIG_JIS_CD                " & vbNewLine _
    '                                      & ",F02_01.ARR_PLAN_DATE               AS      ARR_PLAN_DATE              " & vbNewLine _
    '                                      & ",F02_01.ARR_PLAN_TIME               AS      ARR_PLAN_TIME              " & vbNewLine _
    '                                      & ",F02_01.ARR_ACT_TIME                AS      ARR_ACT_TIME               " & vbNewLine _
    '                                      & ",F02_01.DEST_CD                     AS      DEST_CD                    " & vbNewLine _
    '                                      & ",M10_02.DEST_NM                     AS      DEST_NM                    " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.MOTO_DATA_KB = '10' THEN M03_01.JIS_CD               " & vbNewLine _
    '                                      & "                                      ELSE M10_02.JIS                  " & vbNewLine _
    '                                      & " END                                        DEST_JIS_CD                " & vbNewLine _
    '                                      & ",M10_02.ZIP                         AS      ZIP                        " & vbNewLine _
    '                                      & ",M10_02.AD_1                        AS      AD_1                       " & vbNewLine _
    '                                      & ",M10_02.AD_2                        AS      AD_2                       " & vbNewLine _
    '                                      & ",F02_01.AD_3                        AS      AD_3                       " & vbNewLine _
    '                                      & ",F02_01.AREA_CD                     AS      AREA_CD                    " & vbNewLine _
    '                                      & ",M36_01.AREA_NM                     AS      AREA_NM                    " & vbNewLine _
    '                                      & ",F02_01.UNSO_PKG_NB                 AS      UNSO_PKG_NB                " & vbNewLine _
    '                                      & ",F02_01.UNSO_WT                     AS      UNSO_WT                    " & vbNewLine _
    '                                      & ",F02_01.NB_UT                       AS      NB_UT                      " & vbNewLine _
    '                                      & ",F02_01.UNSO_ONDO_KB                AS      UNSO_ONDO_KB               " & vbNewLine _
    '                                      & ",F02_01.REMARK                      AS      REMARK                     " & vbNewLine _
    '                                      & ",F02_01.TYUKEI_HAISO_FLG            AS      TYUKEI_HAISO_FLG           " & vbNewLine _
    '                                      & ",F02_01.SYUKA_TYUKEI_CD             AS      SYUKA_TYUKEI_CD            " & vbNewLine _
    '                                      & ",F02_01.HAIKA_TYUKEI_CD             AS      HAIKA_TYUKEI_CD            " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO_SYUKA               AS      TRIP_NO_SYUKA              " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO_TYUKEI              AS      TRIP_NO_TYUKEI             " & vbNewLine _
    '                                      & ",F02_01.TRIP_NO_HAIKA               AS      TRIP_NO_HAIKA              " & vbNewLine _
    '                                      & ",F02_01.SYS_UPD_DATE                AS      SYS_UPD_DATE               " & vbNewLine _
    '                                      & ",F02_01.SYS_UPD_TIME                AS      SYS_UPD_TIME               " & vbNewLine _
    '                                      & ",''                                 AS      PRINT_KB                   " & vbNewLine _
    '                                      & ",C01_01.OUTKA_STATE_KB              AS      OUTKA_STATE_KB             " & vbNewLine _
    '                                      & ",C01_01.SYS_UPD_DATE                AS      OUT_UPD_DATE               " & vbNewLine _
    '                                      & ",C01_01.SYS_UPD_TIME                AS      OUT_UPD_TIME               " & vbNewLine _
    '                                      & ",'0'                                AS      PRT_NB                     " & vbNewLine _
    '                                      & ",CASE WHEN F02_01.MOTO_DATA_KB = '10' THEN M03_01.WH_CD                " & vbNewLine _
    '                                      & "                                      ELSE M03_02.WH_CD                " & vbNewLine _
    '                                      & " END                                        WH_CD                      " & vbNewLine _
    '                                      & "FROM       $LM_TRN$..F_UNSO_L             F02_01                       " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..F_UNCHIN_TRS         F04_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = F04_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_NO_L                  = F04_01.UNSO_NO_L             " & vbNewLine _
    '                                      & " AND  F04_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..B_INKA_L             B01_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = B01_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.INOUTKA_NO_L               = B01_01.INKA_NO_L             " & vbNewLine _
    '                                      & " AND  B01_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_TRN$..C_OUTKA_L            C01_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = C01_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.INOUTKA_NO_L               = C01_01.OUTKA_NO_L            " & vbNewLine _
    '                                      & " AND  C01_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_SOKO               M03_01                       " & vbNewLine _
    '                                      & "  ON  B01_01.WH_CD                      = M03_01.WH_CD                 " & vbNewLine _
    '                                      & " AND  M03_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_SOKO               M03_02                       " & vbNewLine _
    '                                      & "  ON  C01_01.WH_CD                      = M03_02.WH_CD                 " & vbNewLine _
    '                                      & " AND  M03_02.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_UNSOCO             M38_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M38_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_CD                    = M38_01.UNSOCO_CD             " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_BR_CD                 = M38_01.UNSOCO_BR_CD          " & vbNewLine _
    '                                      & " AND  M38_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_CUST               M07_01                       " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M07_01.NRS_BR_CD             " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M07_01.CUST_CD_L             " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_M                  = M07_01.CUST_CD_M             " & vbNewLine _
    '                                      & " AND  M07_01.CUST_CD_S                  = '00'                         " & vbNewLine _
    '                                      & " AND  M07_01.CUST_CD_SS                 = '00'                         " & vbNewLine _
    '                                      & " AND  M07_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
    '                                      & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
    '                                      & "                      ,M47_02.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
    '                                      & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                                " & vbNewLine _
    '                                      & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                                 " & vbNewLine _
    '                                      & "                 INNER JOIN (                                                                                                           " & vbNewLine _
    '                                      & "                                    SELECT M47_02.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
    '                                      & "                                          ,M47_02.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
    '                                      & "                                          ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD                                         " & vbNewLine _
    '                                      & "                                          ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
    '                                      & "                                          ,M47_02.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
    '                                      & "                                      FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                             " & vbNewLine _
    '                                      & "                                INNER JOIN (                                                                                            " & vbNewLine _
    '                                      & "                                                    SELECT F02_11.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                          ,MAX(M47_01.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
    '                                      & "                                                      FROM $LM_TRN$..F_UNSO_L F02_11                                                    " & vbNewLine _
    '                                      & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_01                                                 " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD   = M07_01.NRS_BR_CD                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_L   = M07_01.CUST_CD_L                                        " & vbNewLine _
    '                                      & "                                                       AND F02_11.CUST_CD_M   = M07_01.CUST_CD_M                                        " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_S   = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.CUST_CD_SS  = '00'                                                    " & vbNewLine _
    '                                      & "                                                       AND M07_01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
    '                                      & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
    '                                      & "                                                                             SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
    '                                      & "                                                                               FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
    '                                      & "                                                                              WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
    '                                      & "                                                                           GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                                                   ,M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                                )                   M47_01                                              " & vbNewLine _
    '                                      & "                                                        ON F02_11.NRS_BR_CD       = M47_01.NRS_BR_CD                                    " & vbNewLine _
    '                                      & "                                                       AND F02_11.SEIQ_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
    '                                      & "                                                       AND CASE WHEN M07_01.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
    '                                      & "                                                                THEN F02_11.OUTKA_PLAN_DATE                                             " & vbNewLine _
    '                                      & "                                                                ELSE F02_11.ARR_PLAN_DATE                                               " & vbNewLine _
    '                                      & "                                                            END                  >= M47_01.STR_DATE                                     " & vbNewLine _
    '                                      & "                                                     WHERE F02_11.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
    '                                      & "                                                  GROUP BY F02_11.NRS_BR_CD                                                             " & vbNewLine _
    '                                      & "                                                          ,F02_11.UNSO_NO_L                                                             " & vbNewLine _
    '                                      & "                                                          ,M47_01.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
    '                                      & "                                           )                      M47_02                                                                " & vbNewLine _
    '                                      & "                                     ON M47_01.NRS_BR_CD        = M47_02.NRS_BR_CD                                                      " & vbNewLine _
    '                                      & "                                    AND M47_01.UNCHIN_TARIFF_CD = M47_02.UNCHIN_TARIFF_CD                                               " & vbNewLine _
    '                                      & "                                    AND M47_01.STR_DATE         = M47_02.STR_DATE                                                       " & vbNewLine _
    '                                      & "                                  WHERE M47_01.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
    '                                      & "                               GROUP BY M47_02.NRS_BR_CD                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_02.UNSO_NO_L                                                                                " & vbNewLine _
    '                                      & "                                       ,M47_01.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
    '                                      & "                                       ,M47_02.STR_DATE                                                                                 " & vbNewLine _
    '                                      & "                            )                          M47_02                                                                           " & vbNewLine _
    '                                      & "                      ON M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                                                 " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
    '                                      & "                     AND M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
    '                                      & "                     AND M47_01.STR_DATE             = M47_02.STR_DATE                                                                  " & vbNewLine _
    '                                      & "                   WHERE M47_01.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
    '                                      & "          )                               M47_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M47_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.UNSO_NO_L                  = M47_01.UNSO_NO_L     " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD     M49_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M49_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.SEIQ_TARIFF_CD             = M49_01.YOKO_TARIFF_CD" & vbNewLine _
    '                                      & " AND  M49_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN (                                                   " & vbNewLine _
    '                                      & "              SELECT NRS_BR_CD       AS NRS_BR_CD              " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD         " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM        " & vbNewLine _
    '                                      & "                FROM $LM_MST$..M_EXTC_UNCHIN                   " & vbNewLine _
    '                                      & "               WHERE JIS_CD      = '0000000'                   " & vbNewLine _
    '                                      & "                 AND SYS_DEL_FLG = '0'                         " & vbNewLine _
    '                                      & "            GROUP BY NRS_BR_CD                                 " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_CD                            " & vbNewLine _
    '                                      & "                    ,EXTC_TARIFF_REM                           " & vbNewLine _
    '                                      & "           )                              M44_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M44_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.SEIQ_ETARIFF_CD            = M44_01.EXTC_TARIFF_CD" & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_DEST               M10_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M10_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M10_01.CUST_CD_L     " & vbNewLine _
    '                                      & " AND  F02_01.ORIG_CD                    = M10_01.DEST_CD       " & vbNewLine _
    '                                      & " AND  M10_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_DEST               M10_02               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M10_02.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M10_02.CUST_CD_L     " & vbNewLine _
    '                                      & " AND  F02_01.DEST_CD                    = M10_02.DEST_CD       " & vbNewLine _
    '                                      & " AND  M10_02.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_DEST               M10_03               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M10_03.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.CUST_CD_L                  = M10_03.CUST_CD_L     " & vbNewLine _
    '                                      & " AND  F02_01.SHIP_CD                    = M10_03.DEST_CD       " & vbNewLine _
    '                                      & " AND  M10_03.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "LEFT  JOIN $LM_MST$..M_AREA               M36_01               " & vbNewLine _
    '                                      & "  ON  F02_01.NRS_BR_CD                  = M36_01.NRS_BR_CD     " & vbNewLine _
    '                                      & " AND  F02_01.AREA_CD                    = M36_01.AREA_CD       " & vbNewLine _
    '                                      & " AND  M10_02.JIS                        = M36_01.JIS_CD        " & vbNewLine _
    '                                      & " AND  M36_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
    '                                      & "WHERE F02_01.NRS_BR_CD                  = @NRS_BR_CD           " & vbNewLine _
    '                                      & "  AND F02_01.UNSO_NO_L                  = @UNSO_NO_L           " & vbNewLine _
    '                                      & "  AND F02_01.SYS_DEL_FLG                = '0'                  " & vbNewLine
    Private Const SQL_SELECT_L1 As String = "SELECT                                                                 " & vbNewLine _
                                          & " F02_01.NRS_BR_CD                   AS      NRS_BR_CD                  " & vbNewLine _
                                          & ",F02_01.YUSO_BR_CD                  AS      YUSO_BR_CD                 " & vbNewLine _
                                          & ",F02_01.UNSO_NO_L                   AS      UNSO_NO_L                  " & vbNewLine _
                                          & ",F02_01.INOUTKA_NO_L                AS      INOUTKA_NO_L               " & vbNewLine _
                                          & ",F02_01.MOTO_DATA_KB                AS      MOTO_DATA_KB               " & vbNewLine _
                                          & ",F02_01.JIYU_KB                     AS      JIYU_KB                    " & vbNewLine _
                                          & ",F02_01.PC_KB                       AS      PC_KB                      " & vbNewLine _
                                          & ",F02_01.TAX_KB                      AS      TAX_KB                     " & vbNewLine _
                                          & ",F02_01.TRIP_NO                     AS      TRIP_NO                    " & vbNewLine _
                                          & ",F02_01.UNSO_TEHAI_KB               AS      UNSO_TEHAI_KB              " & vbNewLine _
                                          & ",F02_01.BIN_KB                      AS      BIN_KB                     " & vbNewLine _
                                          & ",F02_01.TARIFF_BUNRUI_KB            AS      TARIFF_BUNRUI_KB           " & vbNewLine _
                                          & ",F02_01.VCLE_KB                     AS      VCLE_KB                    " & vbNewLine _
                                          & ",F02_01.UNSO_CD                     AS      UNSO_CD                    " & vbNewLine _
                                          & ",F02_01.UNSO_BR_CD                  AS      UNSO_BR_CD                 " & vbNewLine _
                                          & ",M38_01.UNSOCO_NM                   AS      UNSO_NM                    " & vbNewLine _
                                          & ",M38_01.UNSOCO_BR_NM                AS      UNSO_BR_NM                 " & vbNewLine _
                                          & ",M38_01.TARE_YN                     AS      TARE_YN                    " & vbNewLine _
                                          & ",F02_01.DENP_NO                     AS      DENP_NO                    " & vbNewLine _
                                          & "--(2015.09.18)要望番号2408 追加START                                   " & vbNewLine _
                                          & ",F02_01.AUTO_DENP_KBN               AS AUTO_DENP_KBN                   " & vbNewLine _
                                          & ",F02_01.AUTO_DENP_NO                AS AUTO_DENP_NO                    " & vbNewLine _
                                          & "--(2015.09.18)要望番号2408 追加END                                     " & vbNewLine _
                                          & ",F02_01.CUST_CD_L                   AS      CUST_CD_L                  " & vbNewLine _
                                          & ",F02_01.CUST_CD_M                   AS      CUST_CD_M                  " & vbNewLine _
                                          & ",M07_01.CUST_NM_L                   AS      CUST_NM_L                  " & vbNewLine _
                                          & ",M07_01.CUST_NM_M                   AS      CUST_NM_M                  " & vbNewLine _
                                          & ",F02_01.CUST_REF_NO                 AS      CUST_REF_NO                " & vbNewLine _
                                          & ",F02_01.SHIP_CD                     AS      SHIP_CD                    " & vbNewLine _
                                          & ",ISNULL(M10_03.DEST_NM,M10_06.DEST_NM) AS      SHIP_NM                 " & vbNewLine _
                                          & ",F02_01.BUY_CHU_NO                  AS      BUY_CHU_NO                 " & vbNewLine _
                                          & ",F02_01.SEIQ_TARIFF_CD              AS      SEIQ_TARIFF_CD             " & vbNewLine _
                                          & ",CASE WHEN F02_01.TARIFF_BUNRUI_KB = '40' THEN M49_01.YOKO_REM         " & vbNewLine _
                                          & "                                          ELSE M47_01.UNCHIN_TARIFF_REM" & vbNewLine _
                                          & " END                                        TARIFF_REM                 " & vbNewLine _
                                          & ",F02_01.SEIQ_ETARIFF_CD             AS      SEIQ_ETARIFF_CD            " & vbNewLine _
                                          & ",M44_01.EXTC_TARIFF_REM             AS      EXTC_TARIFF_REM            " & vbNewLine _
                                          & "--'START UMANO 要望番号1302 支払運賃に伴う修正。                       " & vbNewLine _
                                          & ",F02_01.SHIHARAI_TARIFF_CD          AS      SHIHARAI_TARIFF_CD         " & vbNewLine _
                                          & ",CASE WHEN F02_01.TARIFF_BUNRUI_KB = '40' THEN M49_21.YOKO_REM         " & vbNewLine _
                                          & "                                          ELSE M47_21.SHIHARAI_TARIFF_REM" & vbNewLine _
                                          & " END                                        SHIHARAI_TARIFF_REM        " & vbNewLine _
                                          & ",F02_01.SHIHARAI_ETARIFF_CD         AS      SHIHARAI_ETARIFF_CD        " & vbNewLine _
                                          & ",M44_21.EXTC_TARIFF_REM             AS      SHIHARAI_EXTC_TARIFF_REM   " & vbNewLine _
                                          & "--'END UMANO 要望番号1302 支払運賃に伴う修正。                        " & vbNewLine _
                                          & ",F02_01.OUTKA_PLAN_DATE             AS      OUTKA_PLAN_DATE            " & vbNewLine _
                                          & ",F02_01.OUTKA_PLAN_TIME             AS      OUTKA_PLAN_TIME            " & vbNewLine _
                                          & ",F02_01.ORIG_CD                     AS      ORIG_CD                    " & vbNewLine _
                                          & ",ISNULL(M10_01.DEST_NM,M10_04.DEST_NM) AS      ORIG_NM                 " & vbNewLine _
                                          & ",CASE WHEN F02_01.MOTO_DATA_KB = '20' THEN M03_02.JIS_CD               " & vbNewLine _
                                          & "                                      ELSE ISNULL(M10_01.JIS,M10_04.JIS) " & vbNewLine _
                                          & " END                                        ORIG_JIS_CD                " & vbNewLine _
                                          & ",F02_01.ARR_PLAN_DATE               AS      ARR_PLAN_DATE              " & vbNewLine _
                                          & "-- ,M00_01.KBN_NM1                     AS      ARR_PLAN_TIME              " & vbNewLine _
                                          & ",F02_01.ARR_PLAN_TIME               AS      ARR_PLAN_TIME              " & vbNewLine _
                                          & ",F02_01.ARR_ACT_TIME                AS      ARR_ACT_TIME               " & vbNewLine _
                                          & ",F02_01.DEST_CD                     AS      DEST_CD                    " & vbNewLine _
                                          & ",ISNULL(M10_02.DEST_NM,M10_05.DEST_NM) AS      DEST_NM                 " & vbNewLine _
                                          & ",CASE WHEN F02_01.MOTO_DATA_KB = '10' THEN M03_01.JIS_CD               " & vbNewLine _
                                          & "                                      ELSE ISNULL(M10_02.JIS,M10_05.JIS) " & vbNewLine _
                                          & " END                                        DEST_JIS_CD                " & vbNewLine _
                                          & ",ISNULL(M10_02.ZIP,M10_05.ZIP)      AS      ZIP                        " & vbNewLine _
                                          & ",ISNULL(M10_02.AD_1,M10_05.AD_1)    AS      AD_1                       " & vbNewLine _
                                          & ",ISNULL(M10_02.AD_2,M10_05.AD_2)    AS      AD_2                       " & vbNewLine _
                                          & ",F02_01.AD_3                        AS      AD_3                       " & vbNewLine _
                                          & "--2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start " & vbNewLine _
                                          & ",ISNULL(M10_02.TEL,'')              AS      TEL                        " & vbNewLine _
                                          & "--2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end   " & vbNewLine _
                                          & ",F02_01.AREA_CD                     AS      AREA_CD                    " & vbNewLine _
                                          & ",M36_01.AREA_NM                     AS      AREA_NM                    " & vbNewLine _
                                          & ",F02_01.UNSO_PKG_NB                 AS      UNSO_PKG_NB                " & vbNewLine _
                                          & ",F02_01.UNSO_WT                     AS      UNSO_WT                    " & vbNewLine _
                                          & ",F02_01.NB_UT                       AS      NB_UT                      " & vbNewLine _
                                          & ",F02_01.UNSO_ONDO_KB                AS      UNSO_ONDO_KB               " & vbNewLine _
                                          & ",F02_01.REMARK                      AS      REMARK                     " & vbNewLine _
                                          & ",F02_01.TYUKEI_HAISO_FLG            AS      TYUKEI_HAISO_FLG           " & vbNewLine _
                                          & ",F02_01.SYUKA_TYUKEI_CD             AS      SYUKA_TYUKEI_CD            " & vbNewLine _
                                          & ",F02_01.HAIKA_TYUKEI_CD             AS      HAIKA_TYUKEI_CD            " & vbNewLine _
                                          & ",F02_01.TRIP_NO_SYUKA               AS      TRIP_NO_SYUKA              " & vbNewLine _
                                          & ",F02_01.TRIP_NO_TYUKEI              AS      TRIP_NO_TYUKEI             " & vbNewLine _
                                          & ",F02_01.TRIP_NO_HAIKA               AS      TRIP_NO_HAIKA              " & vbNewLine _
                                          & ",F02_01.SYS_UPD_DATE                AS      SYS_UPD_DATE               " & vbNewLine _
                                          & ",F02_01.SYS_UPD_TIME                AS      SYS_UPD_TIME               " & vbNewLine _
                                          & ",''                                 AS      PRINT_KB                   " & vbNewLine _
                                          & ",C01_01.OUTKA_STATE_KB              AS      OUTKA_STATE_KB             " & vbNewLine _
                                          & ",C01_01.SYS_UPD_DATE                AS      OUT_UPD_DATE               " & vbNewLine _
                                          & ",C01_01.SYS_UPD_TIME                AS      OUT_UPD_TIME               " & vbNewLine _
                                          & ",'0'                                AS      PRT_NB                     " & vbNewLine _
                                          & ",CASE WHEN F02_01.MOTO_DATA_KB = '10' THEN M03_01.WH_CD                " & vbNewLine _
                                          & "                                      ELSE M03_02.WH_CD                " & vbNewLine _
                                          & " END                                        WH_CD                      " & vbNewLine _
                                          & ",F02_01.NHS_REMARK                       AS      NHS_REMARK            " & vbNewLine _
                                          & "FROM       $LM_TRN$..F_UNSO_L             F02_01                       " & vbNewLine _
                                          & "LEFT  JOIN $LM_TRN$..F_UNCHIN_TRS         F04_01                       " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = F04_01.NRS_BR_CD             " & vbNewLine _
                                          & " AND  F02_01.UNSO_NO_L                  = F04_01.UNSO_NO_L             " & vbNewLine _
                                          & " AND  F04_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN $LM_TRN$..B_INKA_L             B01_01                       " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = B01_01.NRS_BR_CD             " & vbNewLine _
                                          & " AND  F02_01.INOUTKA_NO_L               = B01_01.INKA_NO_L             " & vbNewLine _
                                          & " AND  B01_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN $LM_TRN$..C_OUTKA_L            C01_01                       " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = C01_01.NRS_BR_CD             " & vbNewLine _
                                          & " AND  F02_01.INOUTKA_NO_L               = C01_01.OUTKA_NO_L            " & vbNewLine _
                                          & " AND  C01_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_SOKO               M03_01                       " & vbNewLine _
                                          & "  ON  B01_01.WH_CD                      = M03_01.WH_CD                 " & vbNewLine _
                                          & " AND  M03_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_SOKO               M03_02                       " & vbNewLine _
                                          & "  ON  C01_01.WH_CD                      = M03_02.WH_CD                 " & vbNewLine _
                                          & " AND  M03_02.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_UNSOCO             M38_01                       " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M38_01.NRS_BR_CD             " & vbNewLine _
                                          & " AND  F02_01.UNSO_CD                    = M38_01.UNSOCO_CD             " & vbNewLine _
                                          & " AND  F02_01.UNSO_BR_CD                 = M38_01.UNSOCO_BR_CD          " & vbNewLine _
                                          & " AND  M38_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_CUST               M07_01                       " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M07_01.NRS_BR_CD             " & vbNewLine _
                                          & " AND  F02_01.CUST_CD_L                  = M07_01.CUST_CD_L             " & vbNewLine _
                                          & " AND  F02_01.CUST_CD_M                  = M07_01.CUST_CD_M             " & vbNewLine _
                                          & " AND  M07_01.CUST_CD_S                  = '00'                         " & vbNewLine _
                                          & " AND  M07_01.CUST_CD_SS                 = '00'                         " & vbNewLine _
                                          & " AND  M07_01.SYS_DEL_FLG                = '0'                          " & vbNewLine _
                                          & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
                                          & "                SELECT M47_02.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
                                          & "                      ,M47_02.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
                                          & "                      ,M47_01.UNCHIN_TARIFF_REM     AS UNCHIN_TARIFF_REM                                                                " & vbNewLine _
                                          & "                  FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                                                 " & vbNewLine _
                                          & "                 INNER JOIN (                                                                                                           " & vbNewLine _
                                          & "                                    SELECT M47_02.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
                                          & "                                          ,M47_02.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
                                          & "                                          ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD                                         " & vbNewLine _
                                          & "                                          ,MIN(M47_01.UNCHIN_TARIFF_CD_EDA) AS UNCHIN_TARIFF_CD_EDA                                     " & vbNewLine _
                                          & "                                          ,M47_02.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
                                          & "                                      FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                                                             " & vbNewLine _
                                          & "                                INNER JOIN (                                                                                            " & vbNewLine _
                                          & "                                                    SELECT F02_11.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                          ,F02_11.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
                                          & "                                                          ,M47_01.UNCHIN_TARIFF_CD      AS UNCHIN_TARIFF_CD                             " & vbNewLine _
                                          & "                                                          ,MAX(M47_01.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
                                          & "                                                      FROM $LM_TRN$..F_UNSO_L F02_11                                                    " & vbNewLine _
                                          & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_01                                                 " & vbNewLine _
                                          & "                                                        ON F02_11.NRS_BR_CD   = M07_01.NRS_BR_CD                                        " & vbNewLine _
                                          & "                                                       AND F02_11.CUST_CD_L   = M07_01.CUST_CD_L                                        " & vbNewLine _
                                          & "                                                       AND F02_11.CUST_CD_M   = M07_01.CUST_CD_M                                        " & vbNewLine _
                                          & "                                                       AND M07_01.CUST_CD_S   = '00'                                                    " & vbNewLine _
                                          & "                                                       AND M07_01.CUST_CD_SS  = '00'                                                    " & vbNewLine _
                                          & "                                                       AND M07_01.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                          & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
                                          & "                                                                             SELECT M47_01.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
                                          & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD          AS UNCHIN_TARIFF_CD" & vbNewLine _
                                          & "                                                                                   ,M47_01.STR_DATE                  AS STR_DATE        " & vbNewLine _
                                          & "                                                                               FROM $LM_MST$..M_UNCHIN_TARIFF M47_01                    " & vbNewLine _
                                          & "                                                                              WHERE M47_01.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "                                                                           GROUP BY M47_01.NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                                                   ,M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
                                          & "                                                                                   ,M47_01.STR_DATE                                     " & vbNewLine _
                                          & "                                                                )                   M47_01                                              " & vbNewLine _
                                          & "                                                        ON F02_11.NRS_BR_CD       = M47_01.NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                       AND F02_11.SEIQ_TARIFF_CD  = M47_01.UNCHIN_TARIFF_CD                             " & vbNewLine _
                                          & "                                                       AND CASE WHEN M07_01.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
                                          & "                                                                THEN F02_11.OUTKA_PLAN_DATE                                             " & vbNewLine _
                                          & "                                                                ELSE F02_11.ARR_PLAN_DATE                                               " & vbNewLine _
                                          & "                                                            END                  >= M47_01.STR_DATE                                     " & vbNewLine _
                                          & "                                                     WHERE F02_11.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
                                          & "                                                  GROUP BY F02_11.NRS_BR_CD                                                             " & vbNewLine _
                                          & "                                                          ,F02_11.UNSO_NO_L                                                             " & vbNewLine _
                                          & "                                                          ,M47_01.UNCHIN_TARIFF_CD                                                      " & vbNewLine _
                                          & "                                           )                      M47_02                                                                " & vbNewLine _
                                          & "                                     ON M47_01.NRS_BR_CD        = M47_02.NRS_BR_CD                                                      " & vbNewLine _
                                          & "                                    AND M47_01.UNCHIN_TARIFF_CD = M47_02.UNCHIN_TARIFF_CD                                               " & vbNewLine _
                                          & "                                    AND M47_01.STR_DATE         = M47_02.STR_DATE                                                       " & vbNewLine _
                                          & "                                  WHERE M47_01.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
                                          & "                               GROUP BY M47_02.NRS_BR_CD                                                                                " & vbNewLine _
                                          & "                                       ,M47_02.UNSO_NO_L                                                                                " & vbNewLine _
                                          & "                                       ,M47_01.UNCHIN_TARIFF_CD                                                                         " & vbNewLine _
                                          & "                                       ,M47_02.STR_DATE                                                                                 " & vbNewLine _
                                          & "                            )                          M47_02                                                                           " & vbNewLine _
                                          & "                      ON M47_01.NRS_BR_CD            = M47_02.NRS_BR_CD                                                                 " & vbNewLine _
                                          & "                     AND M47_01.UNCHIN_TARIFF_CD     = M47_02.UNCHIN_TARIFF_CD                                                          " & vbNewLine _
                                          & "                     AND M47_01.UNCHIN_TARIFF_CD_EDA = M47_02.UNCHIN_TARIFF_CD_EDA                                                      " & vbNewLine _
                                          & "                     AND M47_01.STR_DATE             = M47_02.STR_DATE                                                                  " & vbNewLine _
                                          & "                   WHERE M47_01.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
                                          & "          )                               M47_01               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M47_01.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.UNSO_NO_L                  = M47_01.UNSO_NO_L     " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD     M49_01               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M49_01.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.SEIQ_TARIFF_CD             = M49_01.YOKO_TARIFF_CD" & vbNewLine _
                                          & " AND  M49_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN (                                                   " & vbNewLine _
                                          & "              SELECT NRS_BR_CD       AS NRS_BR_CD              " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD         " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM        " & vbNewLine _
                                          & "                FROM $LM_MST$..M_EXTC_UNCHIN                   " & vbNewLine _
                                          & "               WHERE JIS_CD      = '0000000'                   " & vbNewLine _
                                          & "                 AND SYS_DEL_FLG = '0'                         " & vbNewLine _
                                          & "            GROUP BY NRS_BR_CD                                 " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_CD                            " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_REM                           " & vbNewLine _
                                          & "           )                              M44_01               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M44_01.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.SEIQ_ETARIFF_CD            = M44_01.EXTC_TARIFF_CD" & vbNewLine _
                                          & "--'START UMANO 要望番号1302 支払運賃に伴う修正。               " & vbNewLine _
                                          & "LEFT  JOIN (                                                                                                                            " & vbNewLine _
                                          & "                SELECT M47_22.NRS_BR_CD             AS NRS_BR_CD                                                                        " & vbNewLine _
                                          & "                      ,M47_22.UNSO_NO_L             AS UNSO_NO_L                                                                        " & vbNewLine _
                                          & "                      ,M47_21.SHIHARAI_TARIFF_REM     AS SHIHARAI_TARIFF_REM                                                                " & vbNewLine _
                                          & "                  FROM $LM_MST$..M_SHIHARAI_TARIFF M47_21                                                                                 " & vbNewLine _
                                          & "                 INNER JOIN (                                                                                                           " & vbNewLine _
                                          & "                                    SELECT M47_22.NRS_BR_CD                 AS NRS_BR_CD                                                " & vbNewLine _
                                          & "                                          ,M47_22.UNSO_NO_L                 AS UNSO_NO_L                                                " & vbNewLine _
                                          & "                                          ,M47_21.SHIHARAI_TARIFF_CD          AS SHIHARAI_TARIFF_CD                                         " & vbNewLine _
                                          & "                                          ,MIN(M47_21.SHIHARAI_TARIFF_CD_EDA) AS SHIHARAI_TARIFF_CD_EDA                                     " & vbNewLine _
                                          & "                                          ,M47_22.STR_DATE                  AS STR_DATE                                                 " & vbNewLine _
                                          & "                                      FROM $LM_MST$..M_SHIHARAI_TARIFF M47_21                                                             " & vbNewLine _
                                          & "                                INNER JOIN (                                                                                            " & vbNewLine _
                                          & "                                                    SELECT F02_31.NRS_BR_CD             AS NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                          ,F02_31.UNSO_NO_L             AS UNSO_NO_L                                    " & vbNewLine _
                                          & "                                                          ,M47_21.SHIHARAI_TARIFF_CD    AS SHIHARAI_TARIFF_CD                             " & vbNewLine _
                                          & "                                                          ,MAX(M47_21.STR_DATE)         AS STR_DATE                                     " & vbNewLine _
                                          & "                                                      FROM $LM_TRN$..F_UNSO_L F02_31                                                    " & vbNewLine _
                                          & "                                                      LEFT JOIN $LM_MST$..M_CUST M07_21                                                 " & vbNewLine _
                                          & "                                                        ON F02_31.NRS_BR_CD   = M07_21.NRS_BR_CD                                        " & vbNewLine _
                                          & "                                                       AND F02_31.CUST_CD_L   = M07_21.CUST_CD_L                                        " & vbNewLine _
                                          & "                                                       AND F02_31.CUST_CD_M   = M07_21.CUST_CD_M                                        " & vbNewLine _
                                          & "                                                       AND M07_21.CUST_CD_S   = '00'                                                    " & vbNewLine _
                                          & "                                                       AND M07_21.CUST_CD_SS  = '00'                                                    " & vbNewLine _
                                          & "                                                       AND M07_21.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                          & "                                                      LEFT JOIN (                                                                       " & vbNewLine _
                                          & "                                                                             SELECT M47_21.NRS_BR_CD                 AS NRS_BR_CD       " & vbNewLine _
                                          & "                                                                                   ,M47_21.SHIHARAI_TARIFF_CD        AS SHIHARAI_TARIFF_CD" & vbNewLine _
                                          & "                                                                                   ,M47_21.STR_DATE                  AS STR_DATE        " & vbNewLine _
                                          & "                                                                               FROM $LM_MST$..M_SHIHARAI_TARIFF M47_21                    " & vbNewLine _
                                          & "                                                                              WHERE M47_21.SYS_DEL_FLG = '0'                            " & vbNewLine _
                                          & "                                                                           GROUP BY M47_21.NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                                                   ,M47_21.SHIHARAI_TARIFF_CD                             " & vbNewLine _
                                          & "                                                                                   ,M47_21.STR_DATE                                     " & vbNewLine _
                                          & "                                                                )                   M47_21                                              " & vbNewLine _
                                          & "                                                        ON F02_31.NRS_BR_CD       = M47_21.NRS_BR_CD                                    " & vbNewLine _
                                          & "                                                       AND F02_31.SHIHARAI_TARIFF_CD  = M47_21.SHIHARAI_TARIFF_CD                             " & vbNewLine _
                                          & "                                                       --AND CASE WHEN M07_21.UNTIN_CALCULATION_KB = '01'                                 " & vbNewLine _
                                          & "                                                       --         THEN F02_31.OUTKA_PLAN_DATE                                             " & vbNewLine _
                                          & "                                                       --         ELSE F02_31.ARR_PLAN_DATE                                               " & vbNewLine _
                                          & "                                                       --     END                  >= M47_21.STR_DATE                                     " & vbNewLine _
                                          & "                                                       AND F02_31.ARR_PLAN_DATE    >= M47_21.STR_DATE                                     " & vbNewLine _
                                          & "                                                     WHERE F02_31.SYS_DEL_FLG     = '0'                                                 " & vbNewLine _
                                          & "                                                  GROUP BY F02_31.NRS_BR_CD                                                             " & vbNewLine _
                                          & "                                                          ,F02_31.UNSO_NO_L                                                             " & vbNewLine _
                                          & "                                                          ,M47_21.SHIHARAI_TARIFF_CD                                                      " & vbNewLine _
                                          & "                                           )                      M47_22                                                                " & vbNewLine _
                                          & "                                     ON M47_21.NRS_BR_CD        = M47_22.NRS_BR_CD                                                      " & vbNewLine _
                                          & "                                    AND M47_21.SHIHARAI_TARIFF_CD = M47_22.SHIHARAI_TARIFF_CD                                               " & vbNewLine _
                                          & "                                    AND M47_21.STR_DATE         = M47_22.STR_DATE                                                       " & vbNewLine _
                                          & "                                  WHERE M47_21.SYS_DEL_FLG      = '0'                                                                   " & vbNewLine _
                                          & "                               GROUP BY M47_22.NRS_BR_CD                                                                                " & vbNewLine _
                                          & "                                       ,M47_22.UNSO_NO_L                                                                                " & vbNewLine _
                                          & "                                       ,M47_21.SHIHARAI_TARIFF_CD                                                                         " & vbNewLine _
                                          & "                                       ,M47_22.STR_DATE                                                                                 " & vbNewLine _
                                          & "                            )                          M47_22                                                                           " & vbNewLine _
                                          & "                      ON M47_21.NRS_BR_CD            = M47_22.NRS_BR_CD                                                                 " & vbNewLine _
                                          & "                     AND M47_21.SHIHARAI_TARIFF_CD     = M47_22.SHIHARAI_TARIFF_CD                                                          " & vbNewLine _
                                          & "                     AND M47_21.SHIHARAI_TARIFF_CD_EDA = M47_22.SHIHARAI_TARIFF_CD_EDA                                                      " & vbNewLine _
                                          & "                     AND M47_21.STR_DATE             = M47_22.STR_DATE                                                                  " & vbNewLine _
                                          & "                   WHERE M47_21.SYS_DEL_FLG          = '0'                                                                              " & vbNewLine _
                                          & "          )                               M47_21               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M47_21.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.UNSO_NO_L                  = M47_21.UNSO_NO_L     " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_YOKO_TARIFF_HD_SHIHARAI     M49_21      " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M49_21.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.SEIQ_TARIFF_CD             = M49_21.YOKO_TARIFF_CD" & vbNewLine _
                                          & " AND  M49_21.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN (                                                   " & vbNewLine _
                                          & "              SELECT NRS_BR_CD       AS NRS_BR_CD              " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_CD  AS EXTC_TARIFF_CD         " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_REM AS EXTC_TARIFF_REM        " & vbNewLine _
                                          & "                FROM $LM_MST$..M_EXTC_SHIHARAI                   " & vbNewLine _
                                          & "               WHERE JIS_CD      = '0000000'                   " & vbNewLine _
                                          & "                 AND SYS_DEL_FLG = '0'                         " & vbNewLine _
                                          & "            GROUP BY NRS_BR_CD                                 " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_CD                            " & vbNewLine _
                                          & "                    ,EXTC_TARIFF_REM                           " & vbNewLine _
                                          & "           )                              M44_21               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M44_21.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.SHIHARAI_ETARIFF_CD        = M44_21.EXTC_TARIFF_CD" & vbNewLine _
                                          & "--'END UMANO 要望番号1302 支払運賃に伴う修正。                 " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_DEST               M10_01               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M10_01.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.CUST_CD_L                  = M10_01.CUST_CD_L     " & vbNewLine _
                                          & " AND  F02_01.ORIG_CD                    = M10_01.DEST_CD       " & vbNewLine _
                                          & " AND  M10_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_DEST               M10_02               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M10_02.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.CUST_CD_L                  = M10_02.CUST_CD_L     " & vbNewLine _
                                          & " AND  F02_01.DEST_CD                    = M10_02.DEST_CD       " & vbNewLine _
                                          & " AND  M10_02.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_DEST               M10_03               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M10_03.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.CUST_CD_L                  = M10_03.CUST_CD_L     " & vbNewLine _
                                          & " AND  F02_01.SHIP_CD                    = M10_03.DEST_CD       " & vbNewLine _
                                          & " AND  M10_03.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_DEST               M10_04               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M10_04.NRS_BR_CD     " & vbNewLine _
                                          & " AND  'ZZZZZ'                           = M10_04.CUST_CD_L     " & vbNewLine _
                                          & " AND  F02_01.ORIG_CD                    = M10_04.DEST_CD       " & vbNewLine _
                                          & " AND  M10_04.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_DEST               M10_05               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M10_05.NRS_BR_CD     " & vbNewLine _
                                          & " AND  'ZZZZZ'                           = M10_05.CUST_CD_L     " & vbNewLine _
                                          & " AND  F02_01.DEST_CD                    = M10_05.DEST_CD       " & vbNewLine _
                                          & " AND  M10_05.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_DEST               M10_06               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M10_06.NRS_BR_CD     " & vbNewLine _
                                          & " AND  'ZZZZZ'                           = M10_06.CUST_CD_L     " & vbNewLine _
                                          & " AND  F02_01.SHIP_CD                    = M10_06.DEST_CD       " & vbNewLine _
                                          & " AND  M10_06.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "LEFT  JOIN $LM_MST$..M_AREA               M36_01               " & vbNewLine _
                                          & "  ON  F02_01.NRS_BR_CD                  = M36_01.NRS_BR_CD     " & vbNewLine _
                                          & " AND  F02_01.AREA_CD                    = M36_01.AREA_CD       " & vbNewLine _
                                          & " --要望番号1202 追加START(2012.07.02)                          " & vbNewLine _
                                          & " AND  F02_01.BIN_KB                     = M36_01.BIN_KB        " & vbNewLine _
                                          & " --要望番号1202 追加END  (2012.07.02)                          " & vbNewLine _
                                          & " AND  M10_02.JIS                        = M36_01.JIS_CD        " & vbNewLine _
                                          & " AND  M36_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "-- LEFT  JOIN $LM_MST$..Z_KBN                M00_01               " & vbNewLine _
                                          & "--   ON  'N010'                            = M00_01.KBN_GROUP_CD  " & vbNewLine _
                                          & "--  AND  F02_01.ARR_PLAN_TIME              = M00_01.KBN_CD        " & vbNewLine _
                                          & "--  AND  M00_01.SYS_DEL_FLG                = '0'                  " & vbNewLine _
                                          & "WHERE F02_01.NRS_BR_CD                  = @NRS_BR_CD           " & vbNewLine _
                                          & "  AND F02_01.UNSO_NO_L                  = @UNSO_NO_L           " & vbNewLine _
                                          & "  AND F02_01.SYS_DEL_FLG                = '0'                  " & vbNewLine
    'M_DESTのJOINは【M10_01とM10_04】、【M10_02とM10_05】、【M10_03とM10_06】がペア。
    'END YANAI 要望番号376
    'END YANAI 運送・運行・請求メモNo.41


#End Region

#Region "UNSO_M"

    Private Const SQL_SELECT_M As String = "SELECT                                               " & vbNewLine _
                                         & " F03_01.NRS_BR_CD                AS NRS_BR_CD        " & vbNewLine _
                                         & ",F03_01.UNSO_NO_L                AS UNSO_NO_L        " & vbNewLine _
                                         & ",F03_01.UNSO_NO_M                AS UNSO_NO_M        " & vbNewLine _
                                         & ",F03_01.GOODS_CD_NRS             AS GOODS_CD_NRS     " & vbNewLine _
                                         & ",M08_01.GOODS_CD_CUST            AS GOODS_CD_CUST    " & vbNewLine _
                                         & ",F03_01.GOODS_NM                 AS GOODS_NM         " & vbNewLine _
                                         & ",F03_01.LOT_NO                   AS LOT_NO           " & vbNewLine _
                                         & ",F03_01.BETU_WT                  AS BETU_WT          " & vbNewLine _
                                         & ",F03_01.UNSO_TTL_NB              AS UNSO_TTL_NB      " & vbNewLine _
                                         & ",F03_01.NB_UT                    AS NB_UT            " & vbNewLine _
                                         & ",F03_01.UNSO_TTL_QT              AS UNSO_TTL_QT      " & vbNewLine _
                                         & ",F03_01.QT_UT                    AS QT_UT            " & vbNewLine _
                                         & ",F03_01.HASU                     AS HASU             " & vbNewLine _
                                         & ",F03_01.ZAI_REC_NO               AS ZAI_REC_NO       " & vbNewLine _
                                         & ",F03_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB     " & vbNewLine _
                                         & ",F03_01.IRIME                    AS IRIME            " & vbNewLine _
                                         & ",F03_01.IRIME_UT                 AS IRIME_UT         " & vbNewLine _
                                         & ",F03_01.REMARK                   AS REMARK           " & vbNewLine _
                                         & ",F03_01.PRINT_SORT               AS PRINT_SORT   --ADD 2018/11/28      依頼番号 : 003400        " & vbNewLine _
                                         & ",F03_01.UNSO_HOKEN_UM         　 AS UNSO_HOKEN_UM   --ADD 2021/0121      依頼番号 : 026832        " & vbNewLine _
                                         & ",F03_01.KITAKU_GOODS_UP          AS KITAKU_GOODS_UP   --ADD 2021/01/12      依頼番号 : 026832        " & vbNewLine _
                                         & ",F03_01.PKG_NB                   AS PKG_NB           " & vbNewLine _
                                         & ",F03_01.SIZE_KB                  AS SIZE_KB          " & vbNewLine _
                                         & ",F03_01.ZBUKA_CD                 AS ZBUKA_CD         " & vbNewLine _
                                         & ",F03_01.ABUKA_CD                 AS ABUKA_CD         " & vbNewLine _
                                         & ",F03_01.SYS_UPD_DATE             AS SYS_UPD_DATE     " & vbNewLine _
                                         & ",F03_01.SYS_UPD_TIME             AS SYS_UPD_TIME     " & vbNewLine _
                                         & ",ISNULL(M08_01.STD_IRIME_NB,'0') AS STD_IRIME_NB     " & vbNewLine _
                                         & ",ISNULL(M08_01.STD_WT_KGS,'0')   AS STD_WT_KGS       " & vbNewLine _
                                         & ",ISNULL(M08_01.TARE_YN,'00')     AS TARE_YN          " & vbNewLine _
                                         & ",ISNULL(M08_01.CALC_FLG,'0')     AS CALC_FLG         " & vbNewLine _
                                         & "FROM       $LM_TRN$..F_UNSO_M F03_01                 " & vbNewLine _
                                         & "LEFT  JOIN (                                         " & vbNewLine _
                                         & "             SELECT NRS_BR_CD        AS NRS_BR_CD    " & vbNewLine _
                                         & "                   ,GOODS_CD_NRS     AS GOODS_CD_NRS " & vbNewLine _
                                         & "                   ,GOODS_CD_CUST    AS GOODS_CD_CUST" & vbNewLine _
                                         & "                   ,STD_IRIME_NB     AS STD_IRIME_NB " & vbNewLine _
                                         & "                   ,STD_WT_KGS       AS STD_WT_KGS   " & vbNewLine _
                                         & "                   ,TARE_YN          AS TARE_YN      " & vbNewLine _
                                         & "                   ,'1'              AS CALC_FLG     " & vbNewLine _
                                         & "               FROM $LM_MST$..M_GOODS                " & vbNewLine _
                                         & "              WHERE SYS_DEL_FLG = '0'                " & vbNewLine _
                                         & "            )                M08_01                  " & vbNewLine _
                                         & "   ON  F03_01.NRS_BR_CD    = M08_01.NRS_BR_CD        " & vbNewLine _
                                         & "  AND  F03_01.GOODS_CD_NRS = M08_01.GOODS_CD_NRS     " & vbNewLine _
                                         & "WHERE  F03_01.NRS_BR_CD    = @NRS_BR_CD              " & vbNewLine _
                                         & "  AND  F03_01.UNSO_NO_L    = @UNSO_NO_L              " & vbNewLine _
                                         & "  AND  F03_01.SYS_DEL_FLG  = '0'                     " & vbNewLine _
                                         & "ORDER BY F03_01.UNSO_NO_M                            " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                " & vbNewLine _
                                              & " F04_01.YUSO_BR_CD            AS YUSO_BR_CD           " & vbNewLine _
                                              & ",F04_01.NRS_BR_CD             AS NRS_BR_CD            " & vbNewLine _
                                              & ",F04_01.UNSO_NO_L             AS UNSO_NO_L            " & vbNewLine _
                                              & ",F04_01.UNSO_NO_M             AS UNSO_NO_M            " & vbNewLine _
                                              & ",F04_01.CUST_CD_L             AS CUST_CD_L            " & vbNewLine _
                                              & ",F04_01.CUST_CD_M             AS CUST_CD_M            " & vbNewLine _
                                              & ",F04_01.CUST_CD_S             AS CUST_CD_S            " & vbNewLine _
                                              & ",F04_01.CUST_CD_SS            AS CUST_CD_SS           " & vbNewLine _
                                              & ",F04_01.SEIQ_GROUP_NO         AS SEIQ_GROUP_NO        " & vbNewLine _
                                              & ",F04_01.SEIQ_GROUP_NO_M       AS SEIQ_GROUP_NO_M      " & vbNewLine _
                                              & ",F04_01.SEIQTO_CD             AS SEIQTO_CD            " & vbNewLine _
                                              & ",F04_01.UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB " & vbNewLine _
                                              & ",F04_01.SEIQ_SYARYO_KB        AS SEIQ_SYARYO_KB       " & vbNewLine _
                                              & ",F04_01.SEIQ_PKG_UT           AS SEIQ_PKG_UT          " & vbNewLine _
                                              & ",F04_01.SEIQ_NG_NB            AS SEIQ_NG_NB           " & vbNewLine _
                                              & ",F04_01.SEIQ_DANGER_KB        AS SEIQ_DANGER_KB       " & vbNewLine _
                                              & ",F04_01.SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
                                              & ",F04_01.SEIQ_TARIFF_CD        AS SEIQ_TARIFF_CD       " & vbNewLine _
                                              & ",F04_01.SEIQ_ETARIFF_CD       AS SEIQ_ETARIFF_CD      " & vbNewLine _
                                              & ",F04_01.SEIQ_KYORI            AS SEIQ_KYORI           " & vbNewLine _
                                              & ",F04_01.SEIQ_WT               AS SEIQ_WT              " & vbNewLine _
                                              & ",F04_01.SEIQ_UNCHIN           AS SEIQ_UNCHIN          " & vbNewLine _
                                              & ",F04_01.SEIQ_CITY_EXTC        AS SEIQ_CITY_EXTC       " & vbNewLine _
                                              & ",F04_01.SEIQ_WINT_EXTC        AS SEIQ_WINT_EXTC       " & vbNewLine _
                                              & ",F04_01.SEIQ_RELY_EXTC        AS SEIQ_RELY_EXTC       " & vbNewLine _
                                              & ",F04_01.SEIQ_TOLL             AS SEIQ_TOLL            " & vbNewLine _
                                              & ",F04_01.SEIQ_INSU             AS SEIQ_INSU            " & vbNewLine _
                                              & ",F04_01.SEIQ_FIXED_FLAG       AS SEIQ_FIXED_FLAG      " & vbNewLine _
                                              & ",F04_01.DECI_NG_NB            AS DECI_NG_NB           " & vbNewLine _
                                              & ",F04_01.DECI_KYORI            AS DECI_KYORI           " & vbNewLine _
                                              & ",F04_01.DECI_WT               AS DECI_WT              " & vbNewLine _
                                              & ",F04_01.DECI_UNCHIN           AS DECI_UNCHIN          " & vbNewLine _
                                              & ",F04_01.DECI_CITY_EXTC        AS DECI_CITY_EXTC       " & vbNewLine _
                                              & ",F04_01.DECI_WINT_EXTC        AS DECI_WINT_EXTC       " & vbNewLine _
                                              & ",F04_01.DECI_RELY_EXTC        AS DECI_RELY_EXTC       " & vbNewLine _
                                              & ",F04_01.DECI_TOLL             AS DECI_TOLL            " & vbNewLine _
                                              & ",F04_01.DECI_INSU             AS DECI_INSU            " & vbNewLine _
                                              & ",F04_01.KANRI_UNCHIN          AS KANRI_UNCHIN         " & vbNewLine _
                                              & ",F04_01.KANRI_CITY_EXTC       AS KANRI_CITY_EXTC      " & vbNewLine _
                                              & ",F04_01.KANRI_WINT_EXTC       AS KANRI_WINT_EXTC      " & vbNewLine _
                                              & ",F04_01.KANRI_RELY_EXTC       AS KANRI_RELY_EXTC      " & vbNewLine _
                                              & ",F04_01.KANRI_TOLL            AS KANRI_TOLL           " & vbNewLine _
                                              & ",F04_01.KANRI_INSU            AS KANRI_INSU           " & vbNewLine _
                                              & ",F04_01.REMARK                AS REMARK               " & vbNewLine _
                                              & ",F04_01.SIZE_KB               AS SIZE_KB              " & vbNewLine _
                                              & ",F04_01.TAX_KB                AS TAX_KB               " & vbNewLine _
                                              & ",F04_01.SAGYO_KANRI           AS SAGYO_KANRI          " & vbNewLine _
                                              & "FROM  $LM_TRN$..F_UNCHIN_TRS F04_01                   " & vbNewLine _
                                              & "WHERE F04_01.NRS_BR_CD   = @NRS_BR_CD                 " & vbNewLine _
                                              & "  AND F04_01.UNSO_NO_L   = @UNSO_NO_L                 " & vbNewLine _
                                              & "  AND F04_01.SYS_DEL_FLG = '0'                        " & vbNewLine


#End Region

#Region "INFO"

    'TODO:支払運賃テーブル未作成
    Private Const SQL_SELECT_INFO As String = " SELECT                                                               " & vbNewLine _
                                            & " F02_01.NRS_BR_CD                                                     " & vbNewLine _
                                            & ",F02_01.UNSO_NO_L                                                     " & vbNewLine _
                                            & ",( SELECT COUNT(F04_02.UNSO_NO_L)                                     " & vbNewLine _
                                            & "     FROM $LM_TRN$..F_UNCHIN_TRS    F04_02                            " & vbNewLine _
                                            & "    WHERE F02_01.NRS_BR_CD        = F04_02.NRS_BR_CD                  " & vbNewLine _
                                            & "      AND F02_01.UNSO_NO_L        = F04_02.UNSO_NO_L                  " & vbNewLine _
                                            & "      AND F04_02.SEIQ_FIXED_FLAG <> '00'                              " & vbNewLine _
                                            & "      AND F04_02.SYS_DEL_FLG      = '0'                               " & vbNewLine _
                                            & " )                                                   AS FLAG_CNT      " & vbNewLine _
                                            & ",( SELECT COUNT(F04_03.UNSO_NO_L)                                     " & vbNewLine _
                                            & "     FROM $LM_TRN$..F_UNCHIN_TRS    F04_03                            " & vbNewLine _
                                            & "    WHERE F02_01.NRS_BR_CD        = F04_03.NRS_BR_CD                  " & vbNewLine _
                                            & "      AND F02_01.UNSO_NO_L        = F04_03.UNSO_NO_L                  " & vbNewLine _
                                            & "      AND F04_03.SEIQ_GROUP_NO   <> ''                                " & vbNewLine _
                                            & "      AND F04_03.SYS_DEL_FLG      = '0'                               " & vbNewLine _
                                            & " )                                                   AS GROPU_CNT     " & vbNewLine _
                                            & ",F02_01.UNSO_WT                                      AS UNSO_WT       " & vbNewLine _
                                            & ",MAX(F04_01.DECI_KYORI)                              AS DECI_KYORI    " & vbNewLine _
                                            & ",SUM(F04_01.DECI_WT)                                 AS DECI_WT       " & vbNewLine _
                                            & ",SUM(F04_01.DECI_UNCHIN)                             AS DECI_UNCHIN   " & vbNewLine _
                                            & "--,SUM(F05_01.PAY_UNCHIN)                            AS PAY_UNCHIN    " & vbNewLine _
                                            & ",'0'                              AS PAY_UNCHIN                       " & vbNewLine _
                                            & ",SUM(F04_01.DECI_CITY_EXTC)                          AS DECI_CITY_EXTC" & vbNewLine _
                                            & ",SUM(F04_01.DECI_WINT_EXTC)                          AS DECI_WINT_EXTC" & vbNewLine _
                                            & ",SUM(F04_01.DECI_RELY_EXTC)                          AS DECI_RELY_EXTC" & vbNewLine _
                                            & ",SUM(F04_01.DECI_TOLL)                               AS DECI_TOLL     " & vbNewLine _
                                            & ",MAX(F04_01.DECI_INSU)                               AS DECI_INSU     " & vbNewLine _
                                            & " FROM      $LM_TRN$..F_UNSO_L         F02_01                          " & vbNewLine _
                                            & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS     F04_01                          " & vbNewLine _
                                            & "   ON F02_01.NRS_BR_CD              = F04_01.NRS_BR_CD                " & vbNewLine _
                                            & "  AND F02_01.UNSO_NO_L              = F04_01.UNSO_NO_L                " & vbNewLine _
                                            & "  AND F04_01.SYS_DEL_FLG            = '0'                             " & vbNewLine _
                                            & "-- LEFT JOIN $LM_TRN$..F_PAY_UNCHIN_TRS F05_01                        " & vbNewLine _
                                            & "--   ON F02_01.NRS_BR_CD              = F05_01.NRS_BR_CD              " & vbNewLine _
                                            & "--  AND F02_01.UNSO_NO_L              = F05_01.UNSO_NO_L              " & vbNewLine _
                                            & "--  AND F05_01.SYS_DEL_FLG            = '0'                           " & vbNewLine _
                                            & "WHERE F02_01.NRS_BR_CD              = @NRS_BR_CD                      " & vbNewLine _
                                            & "  AND F02_01.UNSO_NO_L              = @UNSO_NO_L                      " & vbNewLine _
                                            & "  AND F02_01.SYS_DEL_FLG            = '0'                             " & vbNewLine _
                                            & "GROUP BY F02_01.NRS_BR_CD                                             " & vbNewLine _
                                            & "        ,F02_01.UNSO_NO_L                                             " & vbNewLine _
                                            & "        ,F02_01.UNSO_WT                                               " & vbNewLine


    Private Const SQL_SELECT_SHIHARAI As String = " SELECT                                                       " & vbNewLine _
                                        & " F02_01.NRS_BR_CD                                                     " & vbNewLine _
                                        & ",F02_01.UNSO_NO_L                                                     " & vbNewLine _
                                        & ",( SELECT COUNT(F05_02.UNSO_NO_L)                                     " & vbNewLine _
                                        & "     FROM $LM_TRN$..F_SHIHARAI_TRS    F05_02                          " & vbNewLine _
                                        & "    WHERE F02_01.NRS_BR_CD        = F05_02.NRS_BR_CD                  " & vbNewLine _
                                        & "      AND F02_01.UNSO_NO_L        = F05_02.UNSO_NO_L                  " & vbNewLine _
                                        & "      AND F05_02.SHIHARAI_FIXED_FLAG <> '00'                          " & vbNewLine _
                                        & "      AND F05_02.SYS_DEL_FLG      = '0'                               " & vbNewLine _
                                        & " )                                                   AS FLAG_CNT      " & vbNewLine _
                                        & ",F02_01.UNSO_WT                                      AS UNSO_WT       " & vbNewLine _
                                        & ",MAX(F05_01.DECI_KYORI)                              AS DECI_KYORI    " & vbNewLine _
                                        & ",SUM(F05_01.DECI_WT)                                 AS DECI_WT       " & vbNewLine _
                                        & ",SUM(F05_01.DECI_UNCHIN)                             AS DECI_UNCHIN   " & vbNewLine _
                                        & ",SUM(F05_01.DECI_CITY_EXTC)                          AS DECI_CITY_EXTC" & vbNewLine _
                                        & ",SUM(F05_01.DECI_WINT_EXTC)                          AS DECI_WINT_EXTC" & vbNewLine _
                                        & ",SUM(F05_01.DECI_RELY_EXTC)                          AS DECI_RELY_EXTC" & vbNewLine _
                                        & ",SUM(F05_01.DECI_TOLL)                               AS DECI_TOLL     " & vbNewLine _
                                        & ",MAX(F05_01.DECI_INSU)                               AS DECI_INSU     " & vbNewLine _
                                        & " FROM      $LM_TRN$..F_UNSO_L         F02_01                          " & vbNewLine _
                                        & " LEFT JOIN $LM_TRN$..F_SHIHARAI_TRS   F05_01                          " & vbNewLine _
                                        & "   ON F02_01.NRS_BR_CD              = F05_01.NRS_BR_CD                " & vbNewLine _
                                        & "  AND F02_01.UNSO_NO_L              = F05_01.UNSO_NO_L                " & vbNewLine _
                                        & "  AND F05_01.SYS_DEL_FLG            = '0'                             " & vbNewLine _
                                        & "WHERE F02_01.NRS_BR_CD              = @NRS_BR_CD                      " & vbNewLine _
                                        & "  AND F02_01.UNSO_NO_L              = @UNSO_NO_L                      " & vbNewLine _
                                        & "  AND F02_01.SYS_DEL_FLG            = '0'                             " & vbNewLine _
                                        & "GROUP BY F02_01.NRS_BR_CD                                             " & vbNewLine _
                                        & "        ,F02_01.UNSO_NO_L                                             " & vbNewLine _
                                        & "        ,F02_01.UNSO_WT                                               " & vbNewLine


#End Region

#Region "COUNT"

    Private Const SQL_SELECT_COUNT As String = "SELECT COUNT(UNSO_NO_L) AS REC_CNT    " & vbNewLine _
                                             & "FROM $LM_TRN$..F_UNSO_L               " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD       " & vbNewLine _
                                             & "  AND UNSO_NO_L    = @UNSO_NO_L       " & vbNewLine _
                                             & "  AND SYS_DEL_FLG  = '0'              " & vbNewLine _
                                             & "  AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                             & "  AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine

#End Region

#End Region

#Region "設定処理 SQL"

#Region "Insert"

    'START UMANO 要望番号1286 支払運賃作成
#Region "UNSO_L"

    Private Const SQL_INSERT_UNSO_L As String = "INSERT INTO $LM_TRN$..F_UNSO_L" & vbNewLine _
                                              & "(                             " & vbNewLine _
                                              & " NRS_BR_CD                    " & vbNewLine _
                                              & ",UNSO_NO_L                    " & vbNewLine _
                                              & ",YUSO_BR_CD                   " & vbNewLine _
                                              & ",INOUTKA_NO_L                 " & vbNewLine _
                                              & ",TRIP_NO                      " & vbNewLine _
                                              & ",UNSO_CD                      " & vbNewLine _
                                              & ",UNSO_BR_CD                   " & vbNewLine _
                                              & ",BIN_KB                       " & vbNewLine _
                                              & ",JIYU_KB                      " & vbNewLine _
                                              & ",DENP_NO                      " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加START " & vbNewLine _
                                              & ",AUTO_DENP_KBN                " & vbNewLine _
                                              & ",AUTO_DENP_NO                 " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加END   " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE              " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME              " & vbNewLine _
                                              & ",ARR_PLAN_DATE                " & vbNewLine _
                                              & ",ARR_PLAN_TIME                " & vbNewLine _
                                              & ",ARR_ACT_TIME                 " & vbNewLine _
                                              & ",CUST_CD_L                    " & vbNewLine _
                                              & ",CUST_CD_M                    " & vbNewLine _
                                              & ",CUST_REF_NO                  " & vbNewLine _
                                              & ",SHIP_CD                      " & vbNewLine _
                                              & ",ORIG_CD                      " & vbNewLine _
                                              & ",DEST_CD                      " & vbNewLine _
                                              & ",UNSO_PKG_NB                  " & vbNewLine _
                                              & ",NB_UT                        " & vbNewLine _
                                              & ",UNSO_WT                      " & vbNewLine _
                                              & ",UNSO_ONDO_KB                 " & vbNewLine _
                                              & ",PC_KB                        " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB             " & vbNewLine _
                                              & ",VCLE_KB                      " & vbNewLine _
                                              & ",MOTO_DATA_KB                 " & vbNewLine _
                                              & ",TAX_KB                       " & vbNewLine _
                                              & ",REMARK                       " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD               " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD              " & vbNewLine _
                                              & ",AD_3                         " & vbNewLine _
                                              & ",UNSO_TEHAI_KB                " & vbNewLine _
                                              & ",BUY_CHU_NO                   " & vbNewLine _
                                              & ",AREA_CD                      " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG             " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD              " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD              " & vbNewLine _
                                              & ",TRIP_NO_SYUKA                " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI               " & vbNewLine _
                                              & ",TRIP_NO_HAIKA                " & vbNewLine _
                                              & ",SYS_ENT_DATE                 " & vbNewLine _
                                              & ",SYS_ENT_TIME                 " & vbNewLine _
                                              & ",SYS_ENT_PGID                 " & vbNewLine _
                                              & ",SYS_ENT_USER                 " & vbNewLine _
                                              & ",SYS_UPD_DATE                 " & vbNewLine _
                                              & ",SYS_UPD_TIME                 " & vbNewLine _
                                              & ",SYS_UPD_PGID                 " & vbNewLine _
                                              & ",SYS_UPD_USER                 " & vbNewLine _
                                              & ",SYS_DEL_FLG                  " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD           " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD          " & vbNewLine _
                                              & ",NHS_REMARK                   " & vbNewLine _
                                              & " )VALUES(                     " & vbNewLine _
                                              & " @NRS_BR_CD                   " & vbNewLine _
                                              & ",@UNSO_NO_L                   " & vbNewLine _
                                              & ",@YUSO_BR_CD                  " & vbNewLine _
                                              & ",@INOUTKA_NO_L                " & vbNewLine _
                                              & ",@TRIP_NO                     " & vbNewLine _
                                              & ",@UNSO_CD                     " & vbNewLine _
                                              & ",@UNSO_BR_CD                  " & vbNewLine _
                                              & ",@BIN_KB                      " & vbNewLine _
                                              & ",@JIYU_KB                     " & vbNewLine _
                                              & ",@DENP_NO                     " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加START " & vbNewLine _
                                              & ",@AUTO_DENP_KBN               " & vbNewLine _
                                              & ",@AUTO_DENP_NO                " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加END   " & vbNewLine _
                                              & ",@OUTKA_PLAN_DATE             " & vbNewLine _
                                              & ",@OUTKA_PLAN_TIME             " & vbNewLine _
                                              & ",@ARR_PLAN_DATE               " & vbNewLine _
                                              & ",@ARR_PLAN_TIME               " & vbNewLine _
                                              & ",@ARR_ACT_TIME                " & vbNewLine _
                                              & ",@CUST_CD_L                   " & vbNewLine _
                                              & ",@CUST_CD_M                   " & vbNewLine _
                                              & ",@CUST_REF_NO                 " & vbNewLine _
                                              & ",@SHIP_CD                     " & vbNewLine _
                                              & ",@ORIG_CD                     " & vbNewLine _
                                              & ",@DEST_CD                     " & vbNewLine _
                                              & ",@UNSO_PKG_NB                 " & vbNewLine _
                                              & ",@NB_UT                       " & vbNewLine _
                                              & ",@UNSO_WT                     " & vbNewLine _
                                              & ",@UNSO_ONDO_KB                " & vbNewLine _
                                              & ",@PC_KB                       " & vbNewLine _
                                              & ",@TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",@VCLE_KB                     " & vbNewLine _
                                              & ",@MOTO_DATA_KB                " & vbNewLine _
                                              & ",@TAX_KB                      " & vbNewLine _
                                              & ",@REMARK                      " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD              " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD             " & vbNewLine _
                                              & ",@AD_3                        " & vbNewLine _
                                              & ",@UNSO_TEHAI_KB               " & vbNewLine _
                                              & ",@BUY_CHU_NO                  " & vbNewLine _
                                              & ",@AREA_CD                     " & vbNewLine _
                                              & ",@TYUKEI_HAISO_FLG            " & vbNewLine _
                                              & ",@SYUKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@HAIKA_TYUKEI_CD             " & vbNewLine _
                                              & ",@TRIP_NO_SYUKA               " & vbNewLine _
                                              & ",@TRIP_NO_TYUKEI              " & vbNewLine _
                                              & ",@TRIP_NO_HAIKA               " & vbNewLine _
                                              & ",@SYS_ENT_DATE                " & vbNewLine _
                                              & ",@SYS_ENT_TIME                " & vbNewLine _
                                              & ",@SYS_ENT_PGID                " & vbNewLine _
                                              & ",@SYS_ENT_USER                " & vbNewLine _
                                              & ",@SYS_UPD_DATE                " & vbNewLine _
                                              & ",@SYS_UPD_TIME                " & vbNewLine _
                                              & ",@SYS_UPD_PGID                " & vbNewLine _
                                              & ",@SYS_UPD_USER                " & vbNewLine _
                                              & ",@SYS_DEL_FLG                 " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD          " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD         " & vbNewLine _
                                              & ",@NHS_REMARK                  " & vbNewLine _
                                              & ")                             " & vbNewLine

#End Region
    'END UMANO 要望番号1286 支払運賃作成

#Region "UNSO_M"

    Private Const SQL_INSERT_UNSO_M As String = "INSERT INTO $LM_TRN$..F_UNSO_M" & vbNewLine _
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
                                              & ",PRINT_SORT        --ADD 2018/11/28      依頼番号 : 003400  " & vbNewLine _
                                              & ",UNSO_HOKEN_UM   　--ADD 2021/01/21      依頼番号 : 026832  " & vbNewLine _
                                              & ",KITAKU_GOODS_UP   --ADD 2021/01/18      依頼番号 : 026832  " & vbNewLine _
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
                                              & ",@PRINT_SORT        --ADD 2018/11/28      依頼番号 : 003400  " & vbNewLine _
                                              & ",@UNSO_HOKEN_UM     --ADD 2021/01/21      依頼番号 : 026832  " & vbNewLine _
                                              & ",@KITAKU_GOODS_UP   --ADD 2021/01/18      依頼番号 : 026832  " & vbNewLine _
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

#End Region

#Region "UNCHIN"

    Private Const SQL_INSERT_UNCHIN As String = "INSERT INTO $LM_TRN$..F_UNCHIN_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SEIQ_GROUP_NO                    " & vbNewLine _
                                              & ",SEIQ_GROUP_NO_M                  " & vbNewLine _
                                              & ",SEIQTO_CD                        " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SEIQ_SYARYO_KB                   " & vbNewLine _
                                              & ",SEIQ_PKG_UT                      " & vbNewLine _
                                              & ",SEIQ_NG_NB                       " & vbNewLine _
                                              & ",SEIQ_DANGER_KB                   " & vbNewLine _
                                              & ",SEIQ_TARIFF_BUNRUI_KB            " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD                   " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD                  " & vbNewLine _
                                              & ",SEIQ_KYORI                       " & vbNewLine _
                                              & ",SEIQ_WT                          " & vbNewLine _
                                              & ",SEIQ_UNCHIN                      " & vbNewLine _
                                              & ",SEIQ_CITY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_WINT_EXTC                   " & vbNewLine _
                                              & ",SEIQ_RELY_EXTC                   " & vbNewLine _
                                              & ",SEIQ_TOLL                        " & vbNewLine _
                                              & ",SEIQ_INSU                        " & vbNewLine _
                                              & ",SEIQ_FIXED_FLAG                  " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO                   " & vbNewLine _
                                              & ",@SEIQ_GROUP_NO_M                 " & vbNewLine _
                                              & ",@SEIQTO_CD                       " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SEIQ_SYARYO_KB                  " & vbNewLine _
                                              & ",@SEIQ_PKG_UT                     " & vbNewLine _
                                              & ",@SEIQ_NG_NB                      " & vbNewLine _
                                              & ",@SEIQ_DANGER_KB                  " & vbNewLine _
                                              & ",@SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
                                              & ",@SEIQ_TARIFF_CD                  " & vbNewLine _
                                              & ",@SEIQ_ETARIFF_CD                 " & vbNewLine _
                                              & ",@SEIQ_KYORI                      " & vbNewLine _
                                              & ",@SEIQ_WT                         " & vbNewLine _
                                              & ",@SEIQ_UNCHIN                     " & vbNewLine _
                                              & ",@SEIQ_CITY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_WINT_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_RELY_EXTC                  " & vbNewLine _
                                              & ",@SEIQ_TOLL                       " & vbNewLine _
                                              & ",@SEIQ_INSU                       " & vbNewLine _
                                              & ",@SEIQ_FIXED_FLAG                 " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

#End Region

    'START UMANO 要望番号1286 支払運賃作成
#Region "SHIHARAI"

    ''' <summary>
    ''' SHIHARAI INSERT用
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_INSERT_SHIHARAI As String = "INSERT INTO $LM_TRN$..F_SHIHARAI_TRS" & vbNewLine _
                                              & "(                                 " & vbNewLine _
                                              & " YUSO_BR_CD                       " & vbNewLine _
                                              & ",NRS_BR_CD                        " & vbNewLine _
                                              & ",UNSO_NO_L                        " & vbNewLine _
                                              & ",UNSO_NO_M                        " & vbNewLine _
                                              & ",CUST_CD_L                        " & vbNewLine _
                                              & ",CUST_CD_M                        " & vbNewLine _
                                              & ",CUST_CD_S                        " & vbNewLine _
                                              & ",CUST_CD_SS                       " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO                " & vbNewLine _
                                              & ",SHIHARAI_GROUP_NO_M              " & vbNewLine _
                                              & ",SHIHARAITO_CD                    " & vbNewLine _
                                              & ",UNTIN_CALCULATION_KB             " & vbNewLine _
                                              & ",SHIHARAI_SYARYO_KB               " & vbNewLine _
                                              & ",SHIHARAI_PKG_UT                  " & vbNewLine _
                                              & ",SHIHARAI_NG_NB                   " & vbNewLine _
                                              & ",SHIHARAI_DANGER_KB               " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_BUNRUI_KB        " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD               " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD              " & vbNewLine _
                                              & ",SHIHARAI_KYORI                   " & vbNewLine _
                                              & ",SHIHARAI_WT                      " & vbNewLine _
                                              & ",SHIHARAI_UNCHIN                  " & vbNewLine _
                                              & ",SHIHARAI_CITY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_WINT_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_RELY_EXTC               " & vbNewLine _
                                              & ",SHIHARAI_TOLL                    " & vbNewLine _
                                              & ",SHIHARAI_INSU                    " & vbNewLine _
                                              & ",SHIHARAI_FIXED_FLAG              " & vbNewLine _
                                              & ",DECI_NG_NB                       " & vbNewLine _
                                              & ",DECI_KYORI                       " & vbNewLine _
                                              & ",DECI_WT                          " & vbNewLine _
                                              & ",DECI_UNCHIN                      " & vbNewLine _
                                              & ",DECI_CITY_EXTC                   " & vbNewLine _
                                              & ",DECI_WINT_EXTC                   " & vbNewLine _
                                              & ",DECI_RELY_EXTC                   " & vbNewLine _
                                              & ",DECI_TOLL                        " & vbNewLine _
                                              & ",DECI_INSU                        " & vbNewLine _
                                              & ",KANRI_UNCHIN                     " & vbNewLine _
                                              & ",KANRI_CITY_EXTC                  " & vbNewLine _
                                              & ",KANRI_WINT_EXTC                  " & vbNewLine _
                                              & ",KANRI_RELY_EXTC                  " & vbNewLine _
                                              & ",KANRI_TOLL                       " & vbNewLine _
                                              & ",KANRI_INSU                       " & vbNewLine _
                                              & ",REMARK                           " & vbNewLine _
                                              & ",SIZE_KB                          " & vbNewLine _
                                              & ",TAX_KB                           " & vbNewLine _
                                              & ",SAGYO_KANRI                      " & vbNewLine _
                                              & ",SYS_ENT_DATE                     " & vbNewLine _
                                              & ",SYS_ENT_TIME                     " & vbNewLine _
                                              & ",SYS_ENT_PGID                     " & vbNewLine _
                                              & ",SYS_ENT_USER                     " & vbNewLine _
                                              & ",SYS_UPD_DATE                     " & vbNewLine _
                                              & ",SYS_UPD_TIME                     " & vbNewLine _
                                              & ",SYS_UPD_PGID                     " & vbNewLine _
                                              & ",SYS_UPD_USER                     " & vbNewLine _
                                              & ",SYS_DEL_FLG                      " & vbNewLine _
                                              & " )VALUES(                         " & vbNewLine _
                                              & " @YUSO_BR_CD                      " & vbNewLine _
                                              & ",@NRS_BR_CD                       " & vbNewLine _
                                              & ",@UNSO_NO_L                       " & vbNewLine _
                                              & ",@UNSO_NO_M                       " & vbNewLine _
                                              & ",@CUST_CD_L                       " & vbNewLine _
                                              & ",@CUST_CD_M                       " & vbNewLine _
                                              & ",@CUST_CD_S                       " & vbNewLine _
                                              & ",@CUST_CD_SS                      " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO               " & vbNewLine _
                                              & ",@SHIHARAI_GROUP_NO_M             " & vbNewLine _
                                              & ",@SHIHARAITO_CD                   " & vbNewLine _
                                              & ",@UNTIN_CALCULATION_KB            " & vbNewLine _
                                              & ",@SHIHARAI_SYARYO_KB              " & vbNewLine _
                                              & ",@SHIHARAI_PKG_UT                 " & vbNewLine _
                                              & ",@SHIHARAI_NG_NB                  " & vbNewLine _
                                              & ",@SHIHARAI_DANGER_KB              " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_BUNRUI_KB       " & vbNewLine _
                                              & ",@SHIHARAI_TARIFF_CD              " & vbNewLine _
                                              & ",@SHIHARAI_ETARIFF_CD             " & vbNewLine _
                                              & ",@SHIHARAI_KYORI                  " & vbNewLine _
                                              & ",@SHIHARAI_WT                     " & vbNewLine _
                                              & ",@SHIHARAI_UNCHIN                 " & vbNewLine _
                                              & ",@SHIHARAI_CITY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_WINT_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_RELY_EXTC              " & vbNewLine _
                                              & ",@SHIHARAI_TOLL                   " & vbNewLine _
                                              & ",@SHIHARAI_INSU                   " & vbNewLine _
                                              & ",@SHIHARAI_FIXED_FLAG             " & vbNewLine _
                                              & ",@DECI_NG_NB                      " & vbNewLine _
                                              & ",@DECI_KYORI                      " & vbNewLine _
                                              & ",@DECI_WT                         " & vbNewLine _
                                              & ",@DECI_UNCHIN                     " & vbNewLine _
                                              & ",@DECI_CITY_EXTC                  " & vbNewLine _
                                              & ",@DECI_WINT_EXTC                  " & vbNewLine _
                                              & ",@DECI_RELY_EXTC                  " & vbNewLine _
                                              & ",@DECI_TOLL                       " & vbNewLine _
                                              & ",@DECI_INSU                       " & vbNewLine _
                                              & ",@KANRI_UNCHIN                    " & vbNewLine _
                                              & ",@KANRI_CITY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_WINT_EXTC                 " & vbNewLine _
                                              & ",@KANRI_RELY_EXTC                 " & vbNewLine _
                                              & ",@KANRI_TOLL                      " & vbNewLine _
                                              & ",@KANRI_INSU                      " & vbNewLine _
                                              & ",@REMARK                          " & vbNewLine _
                                              & ",@SIZE_KB                         " & vbNewLine _
                                              & ",@TAX_KB                          " & vbNewLine _
                                              & ",@SAGYO_KANRI                     " & vbNewLine _
                                              & ",@SYS_ENT_DATE                    " & vbNewLine _
                                              & ",@SYS_ENT_TIME                    " & vbNewLine _
                                              & ",@SYS_ENT_PGID                    " & vbNewLine _
                                              & ",@SYS_ENT_USER                    " & vbNewLine _
                                              & ",@SYS_UPD_DATE                    " & vbNewLine _
                                              & ",@SYS_UPD_TIME                    " & vbNewLine _
                                              & ",@SYS_UPD_PGID                    " & vbNewLine _
                                              & ",@SYS_UPD_USER                    " & vbNewLine _
                                              & ",@SYS_DEL_FLG                     " & vbNewLine _
                                              & ")                                 " & vbNewLine

#End Region
    'END UMANO 要望番号1286 支払運賃作成

#End Region

#Region "Update"

    'START UMANO 要望番号1286 支払運賃作成
#Region "UNSO_L"

    Private Const SQL_UPDATE_UNSO_L As String = "UPDATE $LM_TRN$..F_UNSO_L SET                " & vbNewLine _
                                              & " YUSO_BR_CD           = @YUSO_BR_CD          " & vbNewLine _
                                              & ",INOUTKA_NO_L         = @INOUTKA_NO_L        " & vbNewLine _
                                              & ",TRIP_NO              = @TRIP_NO             " & vbNewLine _
                                              & ",UNSO_CD              = @UNSO_CD             " & vbNewLine _
                                              & ",UNSO_BR_CD           = @UNSO_BR_CD          " & vbNewLine _
                                              & ",BIN_KB               = @BIN_KB              " & vbNewLine _
                                              & ",JIYU_KB              = @JIYU_KB             " & vbNewLine _
                                              & ",DENP_NO              = @DENP_NO             " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加START         " & vbNewLine _
                                              & ",AUTO_DENP_KBN        = @AUTO_DENP_KBN       " & vbNewLine _
                                              & ",AUTO_DENP_NO         = @AUTO_DENP_NO        " & vbNewLine _
                                              & "--(2015.09.18)要望番号2408 追加END           " & vbNewLine _
                                              & ",OUTKA_PLAN_DATE      = @OUTKA_PLAN_DATE     " & vbNewLine _
                                              & ",OUTKA_PLAN_TIME      = @OUTKA_PLAN_TIME     " & vbNewLine _
                                              & ",ARR_PLAN_DATE        = @ARR_PLAN_DATE       " & vbNewLine _
                                              & ",ARR_PLAN_TIME        = @ARR_PLAN_TIME       " & vbNewLine _
                                              & ",ARR_ACT_TIME         = @ARR_ACT_TIME        " & vbNewLine _
                                              & ",CUST_CD_L            = @CUST_CD_L           " & vbNewLine _
                                              & ",CUST_CD_M            = @CUST_CD_M           " & vbNewLine _
                                              & ",CUST_REF_NO          = @CUST_REF_NO         " & vbNewLine _
                                              & ",SHIP_CD              = @SHIP_CD             " & vbNewLine _
                                              & ",ORIG_CD              = @ORIG_CD             " & vbNewLine _
                                              & ",DEST_CD              = @DEST_CD             " & vbNewLine _
                                              & ",UNSO_PKG_NB          = @UNSO_PKG_NB         " & vbNewLine _
                                              & ",NB_UT                = @NB_UT               " & vbNewLine _
                                              & ",UNSO_WT              = @UNSO_WT             " & vbNewLine _
                                              & ",UNSO_ONDO_KB         = @UNSO_ONDO_KB        " & vbNewLine _
                                              & ",PC_KB                = @PC_KB               " & vbNewLine _
                                              & ",TARIFF_BUNRUI_KB     = @TARIFF_BUNRUI_KB    " & vbNewLine _
                                              & ",VCLE_KB              = @VCLE_KB             " & vbNewLine _
                                              & ",MOTO_DATA_KB         = @MOTO_DATA_KB        " & vbNewLine _
                                              & ",TAX_KB               = @TAX_KB              " & vbNewLine _
                                              & ",REMARK               = @REMARK              " & vbNewLine _
                                              & ",SEIQ_TARIFF_CD       = @SEIQ_TARIFF_CD      " & vbNewLine _
                                              & ",SEIQ_ETARIFF_CD      = @SEIQ_ETARIFF_CD     " & vbNewLine _
                                              & ",AD_3                 = @AD_3                " & vbNewLine _
                                              & ",UNSO_TEHAI_KB        = @UNSO_TEHAI_KB       " & vbNewLine _
                                              & ",BUY_CHU_NO           = @BUY_CHU_NO          " & vbNewLine _
                                              & ",AREA_CD              = @AREA_CD             " & vbNewLine _
                                              & ",TYUKEI_HAISO_FLG     = @TYUKEI_HAISO_FLG    " & vbNewLine _
                                              & ",SYUKA_TYUKEI_CD      = @SYUKA_TYUKEI_CD     " & vbNewLine _
                                              & ",HAIKA_TYUKEI_CD      = @HAIKA_TYUKEI_CD     " & vbNewLine _
                                              & ",TRIP_NO_SYUKA        = @TRIP_NO_SYUKA       " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI       = @TRIP_NO_TYUKEI      " & vbNewLine _
                                              & ",TRIP_NO_HAIKA        = @TRIP_NO_HAIKA       " & vbNewLine _
                                              & ",SYS_UPD_DATE         = @SYS_UPD_DATE        " & vbNewLine _
                                              & ",SYS_UPD_TIME         = @SYS_UPD_TIME        " & vbNewLine _
                                              & ",SYS_UPD_PGID         = @SYS_UPD_PGID        " & vbNewLine _
                                              & ",SYS_UPD_USER         = @SYS_UPD_USER        " & vbNewLine _
                                              & ",SHIHARAI_TARIFF_CD   = @SHIHARAI_TARIFF_CD  " & vbNewLine _
                                              & ",SHIHARAI_ETARIFF_CD  = @SHIHARAI_ETARIFF_CD " & vbNewLine _
                                              & ",NHS_REMARK           = @NHS_REMARK          " & vbNewLine _
                                              & "WHERE NRS_BR_CD       = @NRS_BR_CD           " & vbNewLine _
                                              & "  AND UNSO_NO_L       = @UNSO_NO_L           " & vbNewLine _
                                              & "  AND SYS_UPD_DATE    = @GUI_SYS_UPD_DATE    " & vbNewLine _
                                              & "  AND SYS_UPD_TIME    = @GUI_SYS_UPD_TIME    " & vbNewLine

#End Region
    'END UMANO 要望番号1286 支払運賃作成

#Region "UNCHIN_TRS"

    Private Const SQL_UPDATE_UNSO_U_SYS_DATETIME As String = "UPDATE $LM_TRN$..F_UNCHIN_TRS SET  " & vbNewLine _
                                                           & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                           & "  AND  SYS_DEL_FLG  = '0'          " & vbNewLine

#End Region

#Region "PAY_UNCHIN_TRS"

    Private Const SQL_UPDATE_UNSO_P_SYS_DATETIME As String = "UPDATE $LM_TRN$..F_PAY_UNCHIN_TRS SET" & vbNewLine _
                                                           & "       SYS_UPD_DATE = @SYS_UPD_DATE  " & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME  " & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID  " & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER  " & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD     " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L     " & vbNewLine _
                                                           & "  AND  SYS_DEL_FLG  = '0'            " & vbNewLine

#End Region

#End Region

#Region "Delete"

#Region "UNSO_L"

    Private Const SQL_DELETE_UNSO_L As String = " UPDATE $LM_TRN$..F_UNSO_L SET         " & vbNewLine _
                                              & "       SYS_UPD_DATE = @SYS_UPD_DATE    " & vbNewLine _
                                              & "      ,SYS_UPD_TIME = @SYS_UPD_TIME    " & vbNewLine _
                                              & "      ,SYS_UPD_PGID = @SYS_UPD_PGID    " & vbNewLine _
                                              & "      ,SYS_UPD_USER = @SYS_UPD_USER    " & vbNewLine _
                                              & "      ,SYS_DEL_FLG  = @SYS_DEL_FLG     " & vbNewLine _
                                              & " WHERE NRS_BR_CD    = @NRS_BR_CD       " & vbNewLine _
                                              & "   AND UNSO_NO_L    = @UNSO_NO_L       " & vbNewLine _
                                              & "   AND SYS_UPD_DATE = @GUI_SYS_UPD_DATE" & vbNewLine _
                                              & "   AND SYS_UPD_TIME = @GUI_SYS_UPD_TIME" & vbNewLine


#End Region

#Region "UNSO_M"

    Private Const SQL_DELETE_UNSO_M As String = "DELETE FROM $LM_TRN$..F_UNSO_M    " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD  " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L  " & vbNewLine

#End Region

#Region "UNCHIN"

    Private Const SQL_DELETE_UNCHIN As String = "DELETE FROM $LM_TRN$..F_UNCHIN_TRS " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L   " & vbNewLine

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    Private Const SQL_DELETE_SHIHARAI As String = "DELETE FROM $LM_TRN$..F_SHIHARAI_TRS " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L   " & vbNewLine

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "PAYUNCHIN"

    Private Const SQL_DELETE_PAYUNCHIN As String = "DELETE FROM $LM_TRN$..F_PAY_UNCHIN_TRS" & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD         " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L         " & vbNewLine

#End Region

#Region "M_DEST"
    Private Const SQL_UPDATE_M_DEST As String = "UPDATE $LM_MST$..M_DEST SET        " & vbNewLine _
                                              & "       TEL = @TEL                  " & vbNewLine _
                                              & "      ,SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                              & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                              & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                              & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                              & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND  CUST_CD_L    = @CUST_CD_L   " & vbNewLine _
                                              & "  AND  DEST_CD     = @DEST_CD      " & vbNewLine _
                                              & "  AND  SYS_DEL_FLG  = '0'          " & vbNewLine _
                                              & "  AND  TEL        <> @TEL          " & vbNewLine

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
    Private _StrSql As StringBuilder = New StringBuilder()

    ''' <summary>
    ''' パラメータ設定用
    ''' </summary>
    ''' <remarks></remarks>
    Private _SqlPrmList As ArrayList = New ArrayList()

#End Region

#Region "Method"

#Region "検索処理"

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectNewUnsoLData(ByVal ds As DataSet) As DataSet

        Return Me.SelectUnsoLData(ds, LMF020DAC.SQL_SELECT_L0, LMF020DAC.SelectCondition.PTN1)

    End Function

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLInitData(ByVal ds As DataSet) As DataSet

        Return Me.SelectUnsoLData(ds, LMF020DAC.SQL_SELECT_L1, LMF020DAC.SelectCondition.PTN2)

    End Function

    ''' <summary>
    ''' 運送(大)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">検索パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoLData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMF020DAC.SelectCondition) As DataSet

        'START YANAI 運送・運行・請求メモNo.41
        'Dim str As String() = New String() {"NRS_BR_CD" _
        '                                    , "YUSO_BR_CD" _
        '                                    , "UNSO_NO_L" _
        '                                    , "INOUTKA_NO_L" _
        '                                    , "MOTO_DATA_KB" _
        '                                    , "JIYU_KB" _
        '                                    , "PC_KB" _
        '                                    , "TAX_KB" _
        '                                    , "TRIP_NO" _
        '                                    , "UNSO_TEHAI_KB" _
        '                                    , "BIN_KB" _
        '                                    , "TARIFF_BUNRUI_KB" _
        '                                    , "VCLE_KB" _
        '                                    , "UNSO_CD" _
        '                                    , "UNSO_BR_CD" _
        '                                    , "UNSO_NM" _
        '                                    , "UNSO_BR_NM" _
        '                                    , "TARE_YN" _
        '                                    , "DENP_NO" _
        '                                    , "CUST_CD_L" _
        '                                    , "CUST_CD_M" _
        '                                    , "CUST_NM_L" _
        '                                    , "CUST_NM_M" _
        '                                    , "CUST_REF_NO" _
        '                                    , "SHIP_CD" _
        '                                    , "SHIP_NM" _
        '                                    , "BUY_CHU_NO" _
        '                                    , "SEIQ_TARIFF_CD" _
        '                                    , "TARIFF_REM" _
        '                                    , "SEIQ_ETARIFF_CD" _
        '                                    , "EXTC_TARIFF_REM" _
        '                                    , "OUTKA_PLAN_DATE" _
        '                                    , "OUTKA_PLAN_TIME" _
        '                                    , "ORIG_CD" _
        '                                    , "ORIG_NM" _
        '                                    , "ORIG_JIS_CD" _
        '                                    , "ARR_PLAN_DATE" _
        '                                    , "ARR_PLAN_TIME" _
        '                                    , "ARR_ACT_TIME" _
        '                                    , "DEST_CD" _
        '                                    , "DEST_NM" _
        '                                    , "DEST_JIS_CD" _
        '                                    , "ZIP" _
        '                                    , "AD_1" _
        '                                    , "AD_2" _
        '                                    , "AD_3" _
        '                                    , "AREA_CD" _
        '                                    , "AREA_NM" _
        '                                    , "UNSO_PKG_NB" _
        '                                    , "UNSO_WT" _
        '                                    , "NB_UT" _
        '                                    , "UNSO_ONDO_KB" _
        '                                    , "REMARK" _
        '                                    , "TYUKEI_HAISO_FLG" _
        '                                    , "SYUKA_TYUKEI_CD" _
        '                                    , "HAIKA_TYUKEI_CD" _
        '                                    , "TRIP_NO_SYUKA" _
        '                                    , "TRIP_NO_TYUKEI" _
        '                                    , "TRIP_NO_HAIKA" _
        '                                    , "SYS_UPD_DATE" _
        '                                    , "SYS_UPD_TIME" _
        '                                    , "PRINT_KB" _
        '                                    , "OUTKA_STATE_KB" _
        '                                    , "OUT_UPD_DATE" _
        '                                    , "OUT_UPD_TIME" _
        '                                    , "PRT_NB" _
        '                                    }
        'START UMANO 要望番号1302 支払運賃に伴う修正。
        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "YUSO_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "INOUTKA_NO_L" _
                                            , "MOTO_DATA_KB" _
                                            , "JIYU_KB" _
                                            , "PC_KB" _
                                            , "TAX_KB" _
                                            , "TRIP_NO" _
                                            , "UNSO_TEHAI_KB" _
                                            , "BIN_KB" _
                                            , "TARIFF_BUNRUI_KB" _
                                            , "VCLE_KB" _
                                            , "UNSO_CD" _
                                            , "UNSO_BR_CD" _
                                            , "UNSO_NM" _
                                            , "UNSO_BR_NM" _
                                            , "TARE_YN" _
                                            , "DENP_NO" _
                                            , "AUTO_DENP_KBN" _
                                            , "AUTO_DENP_NO" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_NM_L" _
                                            , "CUST_NM_M" _
                                            , "CUST_REF_NO" _
                                            , "SHIP_CD" _
                                            , "SHIP_NM" _
                                            , "BUY_CHU_NO" _
                                            , "SEIQ_TARIFF_CD" _
                                            , "TARIFF_REM" _
                                            , "SEIQ_ETARIFF_CD" _
                                            , "EXTC_TARIFF_REM" _
                                            , "SHIHARAI_TARIFF_CD" _
                                            , "SHIHARAI_TARIFF_REM" _
                                            , "SHIHARAI_ETARIFF_CD" _
                                            , "SHIHARAI_EXTC_TARIFF_REM" _
                                            , "OUTKA_PLAN_DATE" _
                                            , "OUTKA_PLAN_TIME" _
                                            , "ORIG_CD" _
                                            , "ORIG_NM" _
                                            , "ORIG_JIS_CD" _
                                            , "ARR_PLAN_DATE" _
                                            , "ARR_PLAN_TIME" _
                                            , "ARR_ACT_TIME" _
                                            , "DEST_CD" _
                                            , "DEST_NM" _
                                            , "DEST_JIS_CD" _
                                            , "ZIP" _
                                            , "AD_1" _
                                            , "AD_2" _
                                            , "AD_3" _
                                            , "TEL" _
                                            , "AREA_CD" _
                                            , "AREA_NM" _
                                            , "UNSO_PKG_NB" _
                                            , "UNSO_WT" _
                                            , "NB_UT" _
                                            , "UNSO_ONDO_KB" _
                                            , "REMARK" _
                                            , "TYUKEI_HAISO_FLG" _
                                            , "SYUKA_TYUKEI_CD" _
                                            , "HAIKA_TYUKEI_CD" _
                                            , "TRIP_NO_SYUKA" _
                                            , "TRIP_NO_TYUKEI" _
                                            , "TRIP_NO_HAIKA" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "PRINT_KB" _
                                            , "OUTKA_STATE_KB" _
                                            , "OUT_UPD_DATE" _
                                            , "OUT_UPD_TIME" _
                                            , "PRT_NB" _
                                            , "WH_CD" _
                                            , "NHS_REMARK" _
                                            }
        'END YANAI 運送・運行・請求メモNo.41
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        Return Me.SelectListData(ds, LMF020DAC.TABLE_NM_UNSO_L, sql, ptn, str)

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送(中)のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnsoMData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"NRS_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "UNSO_NO_M" _
                                            , "GOODS_CD_NRS" _
                                            , "GOODS_CD_CUST" _
                                            , "GOODS_NM" _
                                            , "LOT_NO" _
                                            , "BETU_WT" _
                                            , "UNSO_TTL_NB" _
                                            , "NB_UT" _
                                            , "UNSO_TTL_QT" _
                                            , "QT_UT" _
                                            , "HASU" _
                                            , "ZAI_REC_NO" _
                                            , "UNSO_ONDO_KB" _
                                            , "IRIME" _
                                            , "IRIME_UT" _
                                            , "REMARK" _
                                            , "PKG_NB" _
                                            , "SIZE_KB" _
                                            , "ZBUKA_CD" _
                                            , "ABUKA_CD" _
                                            , "SYS_UPD_DATE" _
                                            , "SYS_UPD_TIME" _
                                            , "STD_IRIME_NB" _
                                            , "STD_WT_KGS" _
                                            , "CALC_FLG" _
                                            , "TARE_YN" _
                                            , "PRINT_SORT" _
                                            , "UNSO_HOKEN_UM" _
                                            , "KITAKU_GOODS_UP"
                                            }

        Return Me.SelectListData(ds, LMF020DAC.TABLE_NM_UNSO_M, LMF020DAC.SQL_SELECT_M, LMF020DAC.SelectCondition.PTN2, str)

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の運賃のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"YUSO_BR_CD" _
                                            , "NRS_BR_CD" _
                                            , "UNSO_NO_L" _
                                            , "UNSO_NO_M" _
                                            , "CUST_CD_L" _
                                            , "CUST_CD_M" _
                                            , "CUST_CD_S" _
                                            , "CUST_CD_SS" _
                                            , "SEIQ_GROUP_NO" _
                                            , "SEIQ_GROUP_NO_M" _
                                            , "SEIQTO_CD" _
                                            , "UNTIN_CALCULATION_KB" _
                                            , "SEIQ_SYARYO_KB" _
                                            , "SEIQ_PKG_UT" _
                                            , "SEIQ_NG_NB" _
                                            , "SEIQ_DANGER_KB" _
                                            , "SEIQ_TARIFF_BUNRUI_KB" _
                                            , "SEIQ_TARIFF_CD" _
                                            , "SEIQ_ETARIFF_CD" _
                                            , "SEIQ_KYORI" _
                                            , "SEIQ_WT" _
                                            , "SEIQ_UNCHIN" _
                                            , "SEIQ_CITY_EXTC" _
                                            , "SEIQ_WINT_EXTC" _
                                            , "SEIQ_RELY_EXTC" _
                                            , "SEIQ_TOLL" _
                                            , "SEIQ_INSU" _
                                            , "SEIQ_FIXED_FLAG" _
                                            , "DECI_NG_NB" _
                                            , "DECI_KYORI" _
                                            , "DECI_WT" _
                                            , "DECI_UNCHIN" _
                                            , "DECI_CITY_EXTC" _
                                            , "DECI_WINT_EXTC" _
                                            , "DECI_RELY_EXTC" _
                                            , "DECI_TOLL" _
                                            , "DECI_INSU" _
                                            , "KANRI_UNCHIN" _
                                            , "KANRI_CITY_EXTC" _
                                            , "KANRI_WINT_EXTC" _
                                            , "KANRI_RELY_EXTC" _
                                            , "KANRI_TOLL" _
                                            , "KANRI_INSU" _
                                            , "REMARK" _
                                            , "SIZE_KB" _
                                            , "TAX_KB" _
                                            , "SAGYO_KANRI" _
                                            }

        Return Me.SelectListData(ds, LMF020DAC.TABLE_NM_UNCHIN, LMF020DAC.SQL_SELECT_UNCHIN, LMF020DAC.SelectCondition.PTN2, str)

    End Function

#End Region

#Region "INFO"

    ''' <summary>
    ''' 料金情報のデータ取得(請求)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の入荷(大)のデータ取得SQLの構築・発行</remarks>
    Private Function SelectUnchinInfoData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"FLAG_CNT" _
                                            , "GROPU_CNT" _
                                            , "UNSO_WT" _
                                            , "DECI_KYORI" _
                                            , "DECI_WT" _
                                            , "DECI_UNCHIN" _
                                            , "PAY_UNCHIN" _
                                            , "DECI_CITY_EXTC" _
                                            , "DECI_WINT_EXTC" _
                                            , "DECI_RELY_EXTC" _
                                            , "DECI_TOLL" _
                                            , "DECI_INSU" _
                                            }

        Return Me.SelectListData(ds, LMF020DAC.TABLE_NM_INFO, LMF020DAC.SQL_SELECT_INFO, LMF020DAC.SelectCondition.PTN2, str)

    End Function


    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 料金情報のデータ取得(支払)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>初期表示時の支払データ取得SQLの構築・発行</remarks>
    Private Function SelectShiharaiInfoData(ByVal ds As DataSet) As DataSet

        Dim str As String() = New String() {"FLAG_CNT" _
                                            , "UNSO_WT" _
                                            , "DECI_KYORI" _
                                            , "DECI_WT" _
                                            , "DECI_UNCHIN" _
                                            , "DECI_CITY_EXTC" _
                                            , "DECI_WINT_EXTC" _
                                            , "DECI_RELY_EXTC" _
                                            , "DECI_TOLL" _
                                            , "DECI_INSU" _
                                            }

        Return Me.SelectListData(ds, LMF020DAC.TABLE_NM_SHIHARAI, LMF020DAC.SQL_SELECT_SHIHARAI, LMF020DAC.SelectCondition.PTN2, str)

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#End Region

#Region "排他チェック"

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectHaitaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, LMF020DAC.SelectCondition.PTN2)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_SELECT_COUNT, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

#End Region

#Region "G_HED"

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))	'要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        'START YANAI 要望番号827
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        'END YANAI 要望番号827

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMG000DAC.SQL_SELECT_KEIRI_CHK_DATE, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF020DAC.TABLE_NM_G_HED)

        Return ds

    End Function

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 新黒存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function NewKuroExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_G_HED_CHK)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMG000DAC.SQL_SELECT_NEW_KURO_COUNT, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function


    ''' <summary>
    ''' '請求期間内チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InSkyuDateChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_G_HED_CHK)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_MONTH", String.Concat(Mid(Me._Row.Item("SKYU_DATE").ToString(), 1, 6), "%"), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SKYU_DATE", Me._Row.Item("SKYU_DATE").ToString(), DBDataType.CHAR))

        Dim motoSql As String = String.Empty

        If Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString() = LMF020DAC.SEIQ_TARIFF_BUNRUI_KB_YOKOMOCHI Then
            motoSql = LMG000DAC.SQL_SELECT_IN_SKYU_DATE_YOKO
        Else
            motoSql = LMG000DAC.SQL_SELECT_IN_SKYU_DATE
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(motoSql, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function
    '要望番号:1045 terakawa 2013.03.28 End


#End Region

#End Region

#Region "設定処理"

#Region "Insert"

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_INSERT_UNSO_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Dim sysDateTime As String() = Me.SetDataInsertParameter(Me._SqlPrmList)
        Call Me.SetUnsoLComParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetInsertResult(cmd)

        '更新日を設定
        Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
        Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送(中)テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル新規登録SQLの構築・発行</remarks>
    Private Function InsertUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_M)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_INSERT_UNSO_M _
                                                                       , ds.Tables(LMF020DAC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnsoMComParameter(Me._SqlPrmList, Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' (請求)運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNCHIN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_INSERT_UNCHIN _
                                                                       , ds.Tables(LMF020DAC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)
            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetUnchinComParameter(Me._SqlPrmList, Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    ''' <summary>
    ''' (支払)運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_SHIHARAI_UNCHIN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_INSERT_SHIHARAI _
                                                                       , ds.Tables(LMF020DAC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)
            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetShiharaiComParameter(Me._SqlPrmList, Me._Row)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#End Region

#Region "Update"

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_UPDATE_UNSO_L, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetUnsoLComParameter(Me._SqlPrmList, Me._Row)
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then

            Me._Row.Item("SYS_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("SYS_UPD_TIME") = sysDateTime(1)

        End If

        Return ds

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinData(ByVal ds As DataSet) As DataSet

        Return Me.UpdateComData(ds, LMF020DAC.SQL_UPDATE_UNSO_U_SYS_DATETIME, LMF020DAC.SelectCondition.PTN2)

    End Function

#End Region

#Region "PAYUNCHIN"

    ''' <summary>
    ''' 支払運賃テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>支払運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdatePayUnchinData(ByVal ds As DataSet) As DataSet

        'TODO:支払テーブル未作成
        'Return Me.UpdateComData(ds, LMF020DAC.SQL_UPDATE_UNSO_P_SYS_DATETIME, LMF020DAC.SelectCondition.PTN2)
        Return ds

    End Function

#End Region

    '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
#Region "M_DEST"
    ''' <summary>
    ''' 届先マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateDestMasterData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(SQL_UPDATE_M_DEST, Me._Row.Item("NRS_BR_CD").ToString()))

        With Me._SqlPrmList
            'パラメータ設定
            .Add(MyBase.GetSqlParameter("@TEL", Me._Row.Item("TEL").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@CUST_CD_L", Me._Row.Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            .Add(MyBase.GetSqlParameter("@DEST_CD", Me._Row.Item("DEST_CD").ToString(), DBDataType.CHAR))
        End With

        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim result As Integer = MyBase.GetUpdateResult(cmd)

        Return ds

    End Function
#End Region
    '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start


#Region "COM"

    ''' <summary>
    ''' テーブル更新(システム日付)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <param name="ptn">抽出パターン</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateComData(ByVal ds As DataSet, ByVal sql As String, ByVal ptn As LMF020DAC.SelectCondition) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)
        Dim sysDateTime As String() = Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        If Me.UpdateResultChk(cmd) = True Then

            Me._Row.Item("UPD_UPD_DATE") = sysDateTime(0)
            Me._Row.Item("UPD_UPD_TIME") = sysDateTime(1)

        End If

        Return ds

    End Function

#End Region

#End Region

#Region "Delete"

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)の論理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF020DAC.SQL_DELETE_UNSO_L _
                                                                       , Me._Row.Item("NRS_BR_CD").ToString()))

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetUpdateDelFlgParameter(Me._SqlPrmList)
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, LMF020DAC.SelectCondition.PTN2)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Call Me.UpdateResultChk(cmd)

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    ''' <summary>
    ''' 運送(中)テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnsoMData(ByVal ds As DataSet) As DataSet

        Return Me.DeleteComData(ds, LMF020DAC.SQL_DELETE_UNSO_M)

    End Function

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' (請求)運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteUnchinData(ByVal ds As DataSet) As DataSet

        Return Me.DeleteComData(ds, LMF020DAC.SQL_DELETE_UNCHIN)

    End Function

#End Region

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "UNCHIN"

    ''' <summary>
    ''' (支払)運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>支払運賃テーブル削除SQLの構築・発行</remarks>
    Private Function DeleteShiharaiData(ByVal ds As DataSet) As DataSet

        Return Me.DeleteComData(ds, LMF020DAC.SQL_DELETE_SHIHARAI)

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#Region "PAYUNCHIN"

    ''' <summary>
    ''' 支払運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル更新SQLの構築・発行</remarks>
    Private Function DeletePayUnchinData(ByVal ds As DataSet) As DataSet

        'TODO:支払テーブル未作成
        'Return Me.DeleteComData(ds, LMF020DAC.SQL_DELETE_PAYUNCHIN)
        Return ds

    End Function

#End Region

#Region "COM"

    ''' <summary>
    ''' 物理削除共通処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(中)テーブル更新SQLの構築・発行</remarks>
    Private Function DeleteComData(ByVal ds As DataSet, ByVal sql As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, LMF020DAC.SelectCondition.PTN2)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

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
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet, ByVal tblNm As String, ByVal sql As String, ByVal ptn As LMF020DAC.SelectCondition, ByVal str As String()) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF020DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql.Length = 0

        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row, ptn)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF020DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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

#End Region

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(検索)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="ptn">取得条件の切り替え</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal ptn As LMF020DAC.SelectCondition)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))

            Select Case ptn

                Case LMF020DAC.SelectCondition.PTN1

                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SYS_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))

                Case LMF020DAC.SelectCondition.PTN2

                    prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

                Case LMF020DAC.SelectCondition.PTN3

                    prmList.Add(MyBase.GetSqlParameter("@OUTKA_NO_L", .Item("INOUTKA_NO_L").ToString(), DBDataType.CHAR))

            End Select


        End With

    End Sub

    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetDataInsertParameter(ByVal prmList As ArrayList) As String()


        '更新日時
        Dim sysDateTime As String() = Me.SetSysdataParameter(prmList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", sysDateTime(1), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Return sysDateTime

    End Function

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetSysdataParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

        Return sysDateTime

    End Function

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
    ''' <param name="update">更新日の列名</param>
    ''' <param name="uptime">更新時間の列名</param>
    ''' <remarks></remarks>
    Private Sub SetGuiSysdataTimeParameter(ByVal prmList As ArrayList _
                                           , ByVal dr As DataRow _
                                           , Optional ByVal update As String = "SYS_UPD_DATE" _
                                           , Optional ByVal uptime As String = "SYS_UPD_TIME" _
                                           )

        With dr

            '更新日時
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", .Item(update).ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", .Item(uptime).ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

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
            '要望番号:2408 2015.09.17 追加START
            prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_KBN", .Item("AUTO_DENP_KBN").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO", .Item("AUTO_DENP_NO").ToString(), DBDataType.NVARCHAR))
            '要望番号:2408 2015.09.17 追加END
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
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            prmList.Add(MyBase.GetSqlParameter("@NHS_REMARK", .Item("NHS_REMARK").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(中)の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

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
            prmList.Add(MyBase.GetSqlParameter("@PRINT_SORT", .Item("PRINT_SORT").ToString(), DBDataType.NUMERIC))     'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
            prmList.Add(MyBase.GetSqlParameter("@KITAKU_GOODS_UP", .Item("KITAKU_GOODS_UP").ToString(), DBDataType.NUMERIC))     'ADD 20122/01/12 依頼番号 : 026832 
            prmList.Add(MyBase.GetSqlParameter("@UNSO_HOKEN_UM", .Item("UNSO_HOKEN_UM").ToString(), DBDataType.CHAR))     'ADD 20122/01/21 依頼番号 : 026832 

        End With

    End Sub

    ''' <summary>
    ''' (請求)運賃の更新パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="conditionRow">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR))   '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_SYARYO_KB", .Item("SEIQ_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_PKG_UT", .Item("SEIQ_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_NG_NB", .Item("SEIQ_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_DANGER_KB", .Item("SEIQ_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_BUNRUI_KB", .Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_KYORI", .Item("SEIQ_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WT", .Item("SEIQ_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_UNCHIN", .Item("SEIQ_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_CITY_EXTC", .Item("SEIQ_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_WINT_EXTC", .Item("SEIQ_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_RELY_EXTC", .Item("SEIQ_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_TOLL", .Item("SEIQ_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_INSU", .Item("SEIQ_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", .Item("DECI_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", .Item("DECI_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", .Item("DECI_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", .Item("KANRI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", .Item("KANRI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", .Item("KANRI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", .Item("KANRI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", .Item("KANRI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", .Item("KANRI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払運賃の更新パラメータ
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetShiharaiComParameter(ByVal prmList As ArrayList, ByVal conditionRow As DataRow)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", .Item("YUSO_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", .Item("CUST_CD_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", .Item("CUST_CD_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CUST_CD_S").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CUST_CD_SS").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", .Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", .Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("SHIHARAITO_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_SYARYO_KB", .Item("SHIHARAI_SYARYO_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_PKG_UT", .Item("SHIHARAI_PKG_UT").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_NG_NB", .Item("SHIHARAI_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_DANGER_KB", .Item("SHIHARAI_DANGER_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_BUNRUI_KB", .Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_KYORI", .Item("SHIHARAI_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WT", .Item("SHIHARAI_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNCHIN", .Item("SHIHARAI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_CITY_EXTC", .Item("SHIHARAI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WINT_EXTC", .Item("SHIHARAI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_RELY_EXTC", .Item("SHIHARAI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TOLL", .Item("SHIHARAI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_INSU", .Item("SHIHARAI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", .Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_NG_NB", .Item("DECI_NG_NB").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_KYORI", .Item("DECI_KYORI").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WT", .Item("DECI_WT").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", .Item("KANRI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", .Item("KANRI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", .Item("KANRI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", .Item("KANRI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", .Item("KANRI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", .Item("KANRI_INSU").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SIZE_KB", .Item("SIZE_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SAGYO_KANRI", .Item("SAGYO_KANRI").ToString(), DBDataType.CHAR))

        End With

    End Sub

    'END UMANO 要望番号1302 支払運賃に伴う修正。

#End Region

#End Region

End Class
