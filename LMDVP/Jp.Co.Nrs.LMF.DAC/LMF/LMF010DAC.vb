' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF010    : 運送検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF010DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF010DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF010IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF010OUT"

    ''' <summary>
    ''' ITEMテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ITEM As String = "ITEM"

    ''' <summary>
    ''' UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "UNSO_L"

    ''' <summary>
    ''' G_HEDテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    ''' <summary>
    ''' UNCHINテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNCHIN As String = "UNCHIN"

    'START UMANO 要望番号1369 支払運賃に伴う修正。
    ''' <summary>
    ''' UNSOCOマスタ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSOCO As String = "UNSOCO"
    'END UMANO 要望番号1369 支払運賃に伴う修正。

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' G_HED_CHKテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End

    ''' <summary>
    ''' CMBテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_CMB As String = "CMB"

    ''' <summary>
    ''' 区分テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_KBN As String = "Z_KBN"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMF010DAC"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' F_SHIHARAIテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SHIHARAI_UNCHIN As String = "F_SHIHARAI_TRS"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 条件設定
    ''' </summary>
    ''' <remarks>
    ''' PRE  ：前方一致
    ''' ALL  ：部分一致
    ''' OTHER：その他
    ''' </remarks>
    Private Enum ConditionPattern As Integer

        PRE
        ALL
        OTHER

    End Enum

    ''' <summary>
    ''' 日付絞込(納入日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_NONYU As String = "01"

    ''' <summary>
    ''' 日付絞込(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_SHUKKA As String = "02"

    ''' <summary>
    ''' 日付絞込(運行日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_LL As String = "03"

    ''' <summary>
    ''' 日付絞込(作成日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const DATE_KBN_ENT As String = "04"

    ''' <summary>
    ''' 修正項目(運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_TRIP As String = "01"

    ''' <summary>
    ''' 修正項目(便区分)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_BIN As String = "02"

    ''' <summary>
    ''' 修正項目(運送会社)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_UNSOCO As String = "03"

    ''' <summary>
    ''' 修正項目(中継配送)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_CHUKEI As String = "04"

    ''' <summary>
    ''' 中継配送有
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TYUKEI_HAISO_FLG_ON As String = "01"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GUIDANCE_KBN As String = "00"

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 請求タリフ分類区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SEIQ_TARIFF_BUNRUI_KB_YOKOMOCHI As String = "40"
    '要望番号:1045 terakawa 2013.03.28 End




#End Region



#Region "検索処理 SQL"

#Region "Select句"

    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(F02_01.UNSO_NO_L)                     AS REC_CNT       " & vbNewLine

    'START YANAI 要望番号376
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                               " & vbNewLine _
    '                                        & " F02_01.NRS_BR_CD                        AS      NRS_BR_CD           " & vbNewLine _
    '                                        & ",F02_01.UNSO_NO_L                        AS      UNSO_NO_L           " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM1                          AS      BIN                 " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                          AS      BUNRUI              " & vbNewLine _
    '                                        & ",F02_01.TARIFF_BUNRUI_KB                 AS      TARIFF_BUNRUI_KB    " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_CD                        AS      UNSOCO_CD           " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_BR_CD                     AS      UNSOCO_BR_CD        " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_NM                        AS      UNSOCO_NM           " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_BR_NM                     AS      UNSOCO_BR_NM        " & vbNewLine _
    '                                        & ",F02_01.CUST_REF_NO                      AS      CUST_REF_NO         " & vbNewLine _
    '                                        & ",F02_01.ORIG_CD                          AS      ORIG_CD             " & vbNewLine _
    '                                        & ",M10_01.DEST_NM                          AS      ORIG_NM             " & vbNewLine _
    '                                        & ",F02_01.DEST_CD                          AS      DEST_CD             " & vbNewLine _
    '                                        & ",M10_02.DEST_NM                          AS      DEST_NM             " & vbNewLine _
    '                                        & ",M10_02.AD_1                             AS      DEST_ADD            " & vbNewLine _
    '                                        & ",M36_01.AREA_NM                          AS      AREA                " & vbNewLine _
    '                                        & ",F02_01.UNSO_PKG_NB                      AS      UNSO_PKG_NB         " & vbNewLine _
    '                                        & ",F02_01.UNSO_WT                          AS      UNSO_WT             " & vbNewLine _
    '                                        & ",F03_01.STD_WT_KGS                       AS      SHOMI_JURYO         " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L                     AS      KANRI_NO            " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE                  AS      OUTKA_PLAN_DATE     " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE                    AS      ARR_PLAN_DATE       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                          AS      TRIP_NO             " & vbNewLine _
    '                                        & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
    '                                        & "                              THEN M37_01.DRIVER_NM                  " & vbNewLine _
    '                                        & "                              ELSE M37_02.DRIVER_NM                  " & vbNewLine _
    '                                        & " END                                     AS      DRIVER_NM           " & vbNewLine _
    '                                        & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
    '                                        & "                              THEN F01_01.TRIP_DATE                  " & vbNewLine _
    '                                        & "                              ELSE F01_04.TRIP_DATE                  " & vbNewLine _
    '                                        & " END                                     AS      TRIP_DATE           " & vbNewLine _
    '                                        & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
    '                                        & "                              THEN KBN_05.KBN_NM1                    " & vbNewLine _
    '                                        & "                              ELSE KBN_06.KBN_NM1                    " & vbNewLine _
    '                                        & " END                                     AS      CAR_TP              " & vbNewLine _
    '                                        & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
    '                                        & "                              THEN M39_01.CAR_NO                     " & vbNewLine _
    '                                        & "                              ELSE M39_02.CAR_NO                     " & vbNewLine _
    '                                        & " END                                     AS      CAR_NO              " & vbNewLine _
    '                                        & ",F02_01.UNSO_CD                          AS      UNSO_CD             " & vbNewLine _
    '                                        & ",F02_01.UNSO_BR_CD                       AS      UNSO_BR_CD          " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                        AS      UNSO_NM             " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM                     AS      UNSO_BR_NM          " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                        AS      CUST_NM_L           " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                        AS      CUST_NM_M           " & vbNewLine _
    '                                        & ",F02_01.REMARK                           AS      REMARK              " & vbNewLine _
    '                                        & ",F04_01.UNCHIN                           AS      UNCHIN              " & vbNewLine _
    '                                        & ",F04_01.KYORI                            AS      KYORI               " & vbNewLine _
    '                                        & ",F04_01.GROUP_NO                         AS      GROUP_NO            " & vbNewLine _
    '                                        & ",KBN_03.KBN_NM1                          AS      UNSO_ONDO           " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB                     AS      MOTO_DATA_KB        " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                          AS      MOTO_DATA           " & vbNewLine _
    '                                        & ",M12_01.KEN + M12_01.SHI                 AS      SYUKA_TYUKEI_NM     " & vbNewLine _
    '                                        & ",M12_02.KEN + M12_02.SHI                 AS      HAIKA_TYUKEI_NM     " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_SYUKA                    AS      TRIP_NO_SYUKA       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_TYUKEI                   AS      TRIP_NO_TYUKEI      " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_HAIKA                    AS      TRIP_NO_HAIKA       " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_NM                        AS      UNSOCO_SYUKA        " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_BR_NM                     AS      UNSOCO_BR_SYUKA     " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_NM                        AS      UNSOCO_TYUKEI       " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_BR_NM                     AS      UNSOCO_BR_TYUKEI    " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_NM                        AS      UNSOCO_HAIKA        " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_BR_NM                     AS      UNSOCO_BR_HAIKA     " & vbNewLine _
    '                                        & ",F02_01.TYUKEI_HAISO_FLG                 AS      TYUKEI_HAISO_FLG    " & vbNewLine _
    '                                        & ",F02_01.UNSO_TEHAI_KB                    AS      UNSO_TEHAI_KB       " & vbNewLine _
    '                                        & ",S01_01.USER_NM                          AS      SYS_ENT_NM          " & vbNewLine _
    '                                        & ",F02_01.SYS_ENT_DATE                     AS      SYS_ENT_DATE        " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE                     AS      SYS_UPD_DATE        " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME                     AS      SYS_UPD_TIME        " & vbNewLine _
    '                                        & ",''                                      AS      C_SELECT            " & vbNewLine
    Private Const SQL_SELECT_DATA As String = "SELECT                                                               " & vbNewLine _
                                            & " F02_01.NRS_BR_CD                        AS      NRS_BR_CD           " & vbNewLine _
                                            & ",F02_01.UNSO_NO_L                        AS      UNSO_NO_L           " & vbNewLine _
                                            & ",KBN_01.KBN_NM1                          AS      BIN                 " & vbNewLine _
                                            & ",ISNULL(KBN_07.KBN_NM1,'')               AS      BIN_UNSO_LL         --ADD 2018/12/19 要望管理000880 " & vbNewLine _
                                            & ",KBN_02.KBN_NM1                          AS      BUNRUI              " & vbNewLine _
                                            & ",F02_01.TARIFF_BUNRUI_KB                 AS      TARIFF_BUNRUI_KB    " & vbNewLine _
                                            & ",F01_01.UNSOCO_CD                        AS      UNSOCO_CD           " & vbNewLine _
                                            & ",F01_01.UNSOCO_BR_CD                     AS      UNSOCO_BR_CD        " & vbNewLine _
                                            & ",M38_02.UNSOCO_NM                        AS      UNSOCO_NM           " & vbNewLine _
                                            & ",M38_02.UNSOCO_BR_NM                     AS      UNSOCO_BR_NM        " & vbNewLine _
                                            & ",F02_01.CUST_REF_NO                      AS      CUST_REF_NO         " & vbNewLine _
                                            & ",F02_01.ORIG_CD                          AS      ORIG_CD             " & vbNewLine _
                                            & ",ISNULL(M10_01.DEST_NM,M10_03.DEST_NM)   AS      ORIG_NM             " & vbNewLine _
                                            & ",F02_01.DEST_CD                          AS      DEST_CD             " & vbNewLine _
                                            & ",ISNULL(M10_02.DEST_NM,M10_04.DEST_NM)   AS      DEST_NM             " & vbNewLine _
                                            & ",ISNULL(M10_02.AD_1,M10_04.AD_1)         AS      DEST_ADD            " & vbNewLine _
                                            & ",CASE WHEN F02_01.INOUTKA_NO_L IS NOT NULL OR F02_01.INOUTKA_NO_L <> ''  " & vbNewLine _
                                            & "          THEN CASE WHEN F02_01.ORIG_CD = '999999999999999'          " & vbNewLine _
                                            & "                        THEN OUTKA_TOU_SITU.TASYA_WH_NM              " & vbNewLine _
                                            & "                    WHEN F02_01.DEST_CD = '999999999999999'          " & vbNewLine _
                                            & "                        THEN INKA_TOU_SITU.TASYA_WH_NM               " & vbNewLine _
                                            & "                    ELSE ''                                          " & vbNewLine _
                                            & "               END                                                   " & vbNewLine _
                                            & "      ELSE ''                                                        " & vbNewLine _
                                            & " END                                     AS      TASYA_WH_NM         " & vbNewLine _
                                            & ",ISNULL(M10_02_JIS.KEN + M10_02_JIS.SHI                              " & vbNewLine _
                                            & ",M10_04_JIS.KEN + M10_04_JIS.SHI)        AS      DEST_ADD2           " & vbNewLine _
                                            & ",M36_01.AREA_NM                          AS      AREA                " & vbNewLine _
                                            & ",F02_01.UNSO_PKG_NB                      AS      UNSO_PKG_NB         " & vbNewLine _
                                            & ",F02_01.UNSO_WT                          AS      UNSO_WT             " & vbNewLine _
                                            & ",F03_01.STD_WT_KGS                       AS      SHOMI_JURYO         " & vbNewLine _
                                            & ",F02_01.INOUTKA_NO_L                     AS      KANRI_NO            " & vbNewLine _
                                            & "--  ADD 2019/08/05 005193                                            " & vbNewLine _
                                            & ",CASE WHEN C01_01.WH_CD IS NOT NULL THEN  C01_01.WH_CD               " & vbNewLine _
                                            & "      WHEN B01_01.WH_CD IS NOT NULL THEN  C01_01.WH_CD               " & vbNewLine _
                                            & "      ELSE ''                             END WH_CD                  " & vbNewLine _
                                            & ",F02_01.OUTKA_PLAN_DATE                  AS      OUTKA_PLAN_DATE     " & vbNewLine _
                                            & ",F02_01.ARR_PLAN_DATE                    AS      ARR_PLAN_DATE       " & vbNewLine _
                                            & ",F02_01.TRIP_NO                          AS      TRIP_NO             " & vbNewLine _
                                            & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
                                            & "                              THEN M37_01.DRIVER_NM                  " & vbNewLine _
                                            & "                              ELSE M37_02.DRIVER_NM                  " & vbNewLine _
                                            & " END                                     AS      DRIVER_NM           " & vbNewLine _
                                            & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
                                            & "                              THEN F01_01.TRIP_DATE                  " & vbNewLine _
                                            & "                              ELSE F01_04.TRIP_DATE                  " & vbNewLine _
                                            & " END                                     AS      TRIP_DATE           " & vbNewLine _
                                            & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
                                            & "                              THEN KBN_05.KBN_NM1                    " & vbNewLine _
                                            & "                              ELSE KBN_06.KBN_NM1                    " & vbNewLine _
                                            & " END                                     AS      CAR_TP              " & vbNewLine _
                                            & ",CASE F02_01.TYUKEI_HAISO_FLG WHEN '00'                              " & vbNewLine _
                                            & "                              THEN M39_01.CAR_NO                     " & vbNewLine _
                                            & "                              ELSE M39_02.CAR_NO                     " & vbNewLine _
                                            & " END                                     AS      CAR_NO              " & vbNewLine _
                                            & ",F02_01.UNSO_CD                          AS      UNSO_CD             " & vbNewLine _
                                            & ",F02_01.UNSO_BR_CD                       AS      UNSO_BR_CD          " & vbNewLine _
                                            & ",M38_01.UNSOCO_NM                        AS      UNSO_NM             " & vbNewLine _
                                            & ",M38_01.UNSOCO_BR_NM                     AS      UNSO_BR_NM          " & vbNewLine _
                                            & ",M07_01.CUST_NM_L                        AS      CUST_NM_L           " & vbNewLine _
                                            & ",M07_01.CUST_NM_M                        AS      CUST_NM_M           " & vbNewLine _
                                            & ",F02_01.REMARK                           AS      REMARK              " & vbNewLine _
                                            & ",F04_01.UNCHIN                           AS      UNCHIN              " & vbNewLine _
                                            & "--START UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSの項目追加) " & vbNewLine _
                                            & ",F05_01.SHIHARAI_UNCHIN                  AS      SHIHARAI_UNCHIN     " & vbNewLine _
                                            & ",F05_01.SHIHARAI_FIXED_FLAG              AS      SHIHARAI_FIXED_FLAG " & vbNewLine _
                                            & "--END UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSの項目追加) " & vbNewLine _
                                            & ",F04_01.KYORI                            AS      KYORI               " & vbNewLine _
                                            & ",F04_01.GROUP_NO                         AS      GROUP_NO            " & vbNewLine _
                                            & ",KBN_03.KBN_NM1                          AS      UNSO_ONDO           " & vbNewLine _
                                            & ",F02_01.MOTO_DATA_KB                     AS      MOTO_DATA_KB        " & vbNewLine _
                                            & ",KBN_04.KBN_NM1                          AS      MOTO_DATA           " & vbNewLine _
                                            & ",M12_01.KEN + M12_01.SHI                 AS      SYUKA_TYUKEI_NM     " & vbNewLine _
                                            & ",M12_02.KEN + M12_02.SHI                 AS      HAIKA_TYUKEI_NM     " & vbNewLine _
                                            & ",F02_01.TRIP_NO_SYUKA                    AS      TRIP_NO_SYUKA       " & vbNewLine _
                                            & ",F02_01.TRIP_NO_TYUKEI                   AS      TRIP_NO_TYUKEI      " & vbNewLine _
                                            & ",F02_01.TRIP_NO_HAIKA                    AS      TRIP_NO_HAIKA       " & vbNewLine _
                                            & ",M38_03.UNSOCO_NM                        AS      UNSOCO_SYUKA        " & vbNewLine _
                                            & ",M38_03.UNSOCO_BR_NM                     AS      UNSOCO_BR_SYUKA     " & vbNewLine _
                                            & ",M38_04.UNSOCO_NM                        AS      UNSOCO_TYUKEI       " & vbNewLine _
                                            & ",M38_04.UNSOCO_BR_NM                     AS      UNSOCO_BR_TYUKEI    " & vbNewLine _
                                            & ",M38_05.UNSOCO_NM                        AS      UNSOCO_HAIKA        " & vbNewLine _
                                            & ",M38_05.UNSOCO_BR_NM                     AS      UNSOCO_BR_HAIKA     " & vbNewLine _
                                            & ",F02_01.TYUKEI_HAISO_FLG                 AS      TYUKEI_HAISO_FLG    " & vbNewLine _
                                            & ",F02_01.UNSO_TEHAI_KB                    AS      UNSO_TEHAI_KB       " & vbNewLine _
                                            & ",S01_01.USER_NM                          AS      SYS_ENT_NM          " & vbNewLine _
                                            & ",F02_01.SYS_ENT_DATE                     AS      SYS_ENT_DATE        " & vbNewLine _
                                            & ",F02_01.SYS_UPD_DATE                     AS      SYS_UPD_DATE        " & vbNewLine _
                                            & ",F02_01.SYS_UPD_TIME                     AS      SYS_UPD_TIME        " & vbNewLine _
                                            & ",''                                      AS      C_SELECT            " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '20' THEN                           " & vbNewLine _
                                            & "ISNULL((SELECT KBN_NM1 FROM $LM_MST$..Z_KBN WHERE KBN_GROUP_CD = 'H025' AND KBN_CD = (  " & vbNewLine _
                                            & "     CASE WHEN C01_01.OUTKA_STATE_KB < '50' THEN                     " & vbNewLine _
                                            & "    '00'                                                             " & vbNewLine _
                                            & "     ELSE '01' END)),'')                                             " & vbNewLine _
                                            & "ELSE '' END AS ALCTD_STS                                             " & vbNewLine _
                                            & " --要望番号2063 (2015.05.27) 追加START                               " & vbNewLine _
                                            & ",CASE WHEN H_TEHAI.SYS_UPD_DATE IS NULL  THEN '未'                   " & vbNewLine _
                                            & "      WHEN H_TEHAI.SYS_UPD_DATE IS NOT NULL AND H_TEHAI.SYS_UPD_DATE + H_TEHAI.SYS_UPD_TIME >= F02_01.SYS_UPD_DATE + F02_01.SYS_UPD_TIME THEN '最新'        " & vbNewLine _
                                            & "      ELSE '旧'                          END TEHAI_JYOKYO            " & vbNewLine _
                                            & ",ISNULL(KBN_27.KBN_NM1,'')               AS      TEHAI_SYUBETSU      " & vbNewLine _
                                            & " --要望番号2063 (2015.05.27) 追加END                                 " & vbNewLine _
                                            & " -- 西濃自動送り状番号対応                                           " & vbNewLine _
                                            & ",F02_01.AUTO_DENP_KBN                    AS AUTO_DENP_KBN            " & vbNewLine _
                                            & ",F02_01.AUTO_DENP_NO                     AS AUTO_DENP_NO             " & vbNewLine _
                                            & ",ISNULL(NULLIF(AUTO_DENP_NO, ''), F02_01.DENP_NO)   AS DENP_NO       " & vbNewLine _
                                            & ",F02_01.NHS_REMARK                       AS      NHS_REMARK          " & vbNewLine _
                                            & ",F02_01.HAISO_PF_SEND_FLG                AS      PF_SOSHIN           --ADD 2022.08.22 " & vbNewLine _
                                            & ",KBN_08.KBN_NM8                          AS      PF_SOSHIN_NM        --ADD 2022.08.22 " & vbNewLine _
                                            & ",KBN_9.KBN_NM8                           AS      SEIQ_FIXED_NM       " & vbNewLine
    'END YANAI 要望番号376

#End Region

#Region "From句"

    'START YANAI 要望番号376
    'Private Const SQL_FROM As String = "FROM       $LM_TRN$..F_UNSO_L           F02_01                           " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.TRIP_NO                  = F01_01.TRIP_NO                   " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_02                           " & vbNewLine _
    '                                 & "  ON  F02_01.TRIP_NO_SYUKA            = F01_02.TRIP_NO                   " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_03                           " & vbNewLine _
    '                                 & "  ON  F02_01.TRIP_NO_TYUKEI           = F01_03.TRIP_NO                   " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_TRN$..F_UNSO_LL          F01_04                           " & vbNewLine _
    '                                 & "  ON  F02_01.TRIP_NO_HAIKA            = F01_04.TRIP_NO                   " & vbNewLine _
    '                                 & "LEFT  JOIN (                                                             " & vbNewLine _
    '                                 & "                  SELECT F03_01.NRS_BR_CD                                " & vbNewLine _
    '                                 & "                        ,F03_01.UNSO_NO_L                                " & vbNewLine _
    '                                 & "                        ,SUM(ISNULL(M08_01.STD_WT_KGS,'0')) AS STD_WT_KGS" & vbNewLine _
    '                                 & "                    FROM $LM_TRN$..F_UNSO_M        F03_01                " & vbNewLine _
    '                                 & "               LEFT JOIN $LM_MST$..M_GOODS         M08_01                " & vbNewLine _
    '                                 & "                      ON F03_01.NRS_BR_CD        = M08_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "                     AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS   " & vbNewLine _
    '                                 & "                     AND M08_01.SYS_DEL_FLG      = '0'                   " & vbNewLine _
    '                                 & "                   WHERE F03_01.SYS_DEL_FLG      = '0'                   " & vbNewLine _
    '                                 & "                GROUP BY F03_01.NRS_BR_CD                                " & vbNewLine _
    '                                 & "                        ,F03_01.UNSO_NO_L                                " & vbNewLine _
    '                                 & "            )                           F03_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = F03_01.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.UNSO_NO_L                = F03_01.UNSO_NO_L                 " & vbNewLine _
    '                                 & "LEFT  JOIN (                                                             " & vbNewLine _
    '                                 & "                  SELECT NRS_BR_CD                                       " & vbNewLine _
    '                                 & "                        ,UNSO_NO_L                                       " & vbNewLine _
    '                                 & "                        ,MIN(F04_01.SEIQ_GROUP_NO)        AS GROUP_NO    " & vbNewLine _
    '                                 & "                        ,SUM(   F04_01.DECI_UNCHIN                       " & vbNewLine _
    '                                 & "                              + F04_01.DECI_CITY_EXTC                    " & vbNewLine _
    '                                 & "                              + F04_01.DECI_WINT_EXTC                    " & vbNewLine _
    '                                 & "                              + F04_01.DECI_RELY_EXTC                    " & vbNewLine _
    '                                 & "                              + F04_01.DECI_TOLL                         " & vbNewLine _
    '                                 & "                              + F04_01.DECI_INSU                         " & vbNewLine _
    '                                 & "                             )                            AS UNCHIN      " & vbNewLine _
    '                                 & "                        ,MAX(F04_01.SEIQ_KYORI)           AS KYORI       " & vbNewLine _
    '                                 & "                    FROM $LM_TRN$..F_UNCHIN_TRS       F04_01             " & vbNewLine _
    '                                 & "                   WHERE SYS_DEL_FLG = '0'                               " & vbNewLine _
    '                                 & "                GROUP BY NRS_BR_CD                                       " & vbNewLine _
    '                                 & "                        ,UNSO_NO_L                                       " & vbNewLine _
    '                                 & "            )                           F04_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = F04_01.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.UNSO_NO_L                = F04_01.UNSO_NO_L                 " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_CUST             M07_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = M07_01.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.CUST_CD_L                = M07_01.CUST_CD_L                 " & vbNewLine _
    '                                 & " AND  F02_01.CUST_CD_M                = M07_01.CUST_CD_M                 " & vbNewLine _
    '                                 & " AND  M07_01.CUST_CD_S                = '00'                             " & vbNewLine _
    '                                 & " AND  M07_01.CUST_CD_SS               = '00'                             " & vbNewLine _
    '                                 & " AND  M07_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = M38_01.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.UNSO_CD                  = M38_01.UNSOCO_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.UNSO_BR_CD               = M38_01.UNSOCO_BR_CD              " & vbNewLine _
    '                                 & " AND  M38_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_02                           " & vbNewLine _
    '                                 & "  ON  F01_01.NRS_BR_CD                = M38_02.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F01_01.UNSOCO_CD                = M38_02.UNSOCO_CD                 " & vbNewLine _
    '                                 & " AND  F01_01.UNSOCO_BR_CD             = M38_02.UNSOCO_BR_CD              " & vbNewLine _
    '                                 & " AND  M38_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_03                           " & vbNewLine _
    '                                 & "  ON  F01_02.NRS_BR_CD                = M38_03.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F01_02.UNSOCO_CD                = M38_03.UNSOCO_CD                 " & vbNewLine _
    '                                 & " AND  F01_02.UNSOCO_BR_CD             = M38_03.UNSOCO_BR_CD              " & vbNewLine _
    '                                 & " AND  M38_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_04                           " & vbNewLine _
    '                                 & "  ON  F01_03.NRS_BR_CD                = M38_04.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F01_03.UNSOCO_CD                = M38_04.UNSOCO_CD                 " & vbNewLine _
    '                                 & " AND  F01_03.UNSOCO_BR_CD             = M38_04.UNSOCO_BR_CD              " & vbNewLine _
    '                                 & " AND  M38_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_05                           " & vbNewLine _
    '                                 & "  ON  F01_04.NRS_BR_CD                = M38_05.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F01_04.UNSOCO_CD                = M38_05.UNSOCO_CD                 " & vbNewLine _
    '                                 & " AND  F01_04.UNSOCO_BR_CD             = M38_05.UNSOCO_BR_CD              " & vbNewLine _
    '                                 & " AND  M38_05.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = M10_01.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.CUST_CD_L                = M10_01.CUST_CD_L                 " & vbNewLine _
    '                                 & " AND  F02_01.ORIG_CD                  = M10_01.DEST_CD                   " & vbNewLine _
    '                                 & " AND  M10_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_DEST             M10_02                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = M10_02.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.CUST_CD_L                = M10_02.CUST_CD_L                 " & vbNewLine _
    '                                 & " AND  F02_01.DEST_CD                  = M10_02.DEST_CD                   " & vbNewLine _
    '                                 & " AND  M10_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.SYUKA_TYUKEI_CD          = M12_01.JIS_CD                    " & vbNewLine _
    '                                 & " AND  M12_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_JIS              M12_02                           " & vbNewLine _
    '                                 & "  ON  F02_01.HAIKA_TYUKEI_CD          = M12_02.JIS_CD                    " & vbNewLine _
    '                                 & " AND  M12_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_AREA             M36_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.NRS_BR_CD                = M36_01.NRS_BR_CD                 " & vbNewLine _
    '                                 & " AND  F02_01.AREA_CD                  = M36_01.AREA_CD                   " & vbNewLine _
    '                                 & " AND  M10_02.JIS                      = M36_01.JIS_CD                    " & vbNewLine _
    '                                 & " AND  M36_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_01                           " & vbNewLine _
    '                                 & "  ON  F01_01.CAR_KEY                  = M39_01.CAR_KEY                   " & vbNewLine _
    '                                 & " AND  M39_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_VCLE             M39_02                           " & vbNewLine _
    '                                 & "  ON  F01_04.CAR_KEY                  = M39_02.CAR_KEY                   " & vbNewLine _
    '                                 & " AND  M39_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_01                           " & vbNewLine _
    '                                 & "  ON  F01_01.DRIVER_CD                = M37_01.DRIVER_CD                 " & vbNewLine _
    '                                 & " AND  M37_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_02                           " & vbNewLine _
    '                                 & "  ON  F01_04.DRIVER_CD                = M37_02.DRIVER_CD                 " & vbNewLine _
    '                                 & " AND  M37_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.BIN_KB                   = KBN_01.KBN_CD                    " & vbNewLine _
    '                                 & " AND  KBN_01.KBN_GROUP_CD             = 'U001'                           " & vbNewLine _
    '                                 & " AND  KBN_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_02                           " & vbNewLine _
    '                                 & "  ON  F02_01.TARIFF_BUNRUI_KB         = KBN_02.KBN_CD                    " & vbNewLine _
    '                                 & " AND  KBN_02.KBN_GROUP_CD             = 'T015'                           " & vbNewLine _
    '                                 & " AND  KBN_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_03                           " & vbNewLine _
    '                                 & "  ON  F02_01.UNSO_ONDO_KB             = KBN_03.KBN_CD                    " & vbNewLine _
    '                                 & " AND  KBN_03.KBN_GROUP_CD             = 'U006'                           " & vbNewLine _
    '                                 & " AND  KBN_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_04                           " & vbNewLine _
    '                                 & "  ON  F02_01.MOTO_DATA_KB             = KBN_04.KBN_CD                    " & vbNewLine _
    '                                 & " AND  KBN_04.KBN_GROUP_CD             = 'M004'                           " & vbNewLine _
    '                                 & " AND  KBN_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_05                           " & vbNewLine _
    '                                 & "  ON  M39_01.CAR_TP_KB                = KBN_05.KBN_CD                    " & vbNewLine _
    '                                 & " AND  KBN_05.KBN_GROUP_CD             = 'S023'                           " & vbNewLine _
    '                                 & " AND  KBN_05.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_06                           " & vbNewLine _
    '                                 & "  ON  M39_02.CAR_TP_KB                = KBN_06.KBN_CD                    " & vbNewLine _
    '                                 & " AND  KBN_06.KBN_GROUP_CD             = 'S023'                           " & vbNewLine _
    '                                 & " AND  KBN_06.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "LEFT  JOIN $LM_MST$..S_USER             S01_01                           " & vbNewLine _
    '                                 & "  ON  F02_01.SYS_ENT_USER             = S01_01.USER_CD                   " & vbNewLine _
    '                                 & " AND  S01_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
    '                                 & "WHERE F02_01.NRS_BR_CD                = @NRS_BR_CD                       " & vbNewLine _
    '                                 & "  AND F02_01.SYS_DEL_FLG              = '0'                              " & vbNewLine
    Private Const SQL_FROM_1 As String = "FROM       $LM_TRN$..F_UNSO_L           F02_01                           " & vbNewLine _
                                     & "LEFT  JOIN                                      " & vbNewLine
    Private Const SQL_FROM_2 As String = " F01_01 ON  F02_01.TRIP_NO                  = F01_01.TRIP_NO                   " & vbNewLine _
                                     & "LEFT  JOIN                                      " & vbNewLine
    Private Const SQL_FROM_3 As String = " F01_02 ON  F02_01.TRIP_NO_SYUKA            = F01_02.TRIP_NO                   " & vbNewLine _
                                     & "LEFT  JOIN                                      " & vbNewLine
    Private Const SQL_FROM_4 As String = " F01_03 ON  F02_01.TRIP_NO_TYUKEI           = F01_03.TRIP_NO                   " & vbNewLine _
                                     & "LEFT  JOIN                                      " & vbNewLine
    Private Const SQL_FROM_5 As String = " F01_04 ON  F02_01.TRIP_NO_HAIKA            = F01_04.TRIP_NO                   " & vbNewLine _
                                     & "LEFT  JOIN (                                                             " & vbNewLine _
                                     & "                  SELECT F03_01.NRS_BR_CD                                " & vbNewLine _
                                     & "                        ,F03_01.UNSO_NO_L                                " & vbNewLine _
                                     & "                        ,SUM(ISNULL(M08_01.STD_WT_KGS,'0')) AS STD_WT_KGS" & vbNewLine _
                                     & "                    FROM $LM_TRN$..F_UNSO_M        F03_01                " & vbNewLine _
                                     & "               LEFT JOIN $LM_MST$..M_GOODS         M08_01                " & vbNewLine _
                                     & "                      ON F03_01.NRS_BR_CD        = M08_01.NRS_BR_CD      " & vbNewLine _
                                     & "                     AND F03_01.GOODS_CD_NRS     = M08_01.GOODS_CD_NRS   " & vbNewLine _
                                     & "                     AND M08_01.SYS_DEL_FLG      = '0'                   " & vbNewLine _
                                     & "                   WHERE F03_01.SYS_DEL_FLG      = '0'                   " & vbNewLine _
                                     & "                GROUP BY F03_01.NRS_BR_CD                                " & vbNewLine _
                                     & "                        ,F03_01.UNSO_NO_L                                " & vbNewLine _
                                     & "            )                           F03_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = F03_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.UNSO_NO_L                = F03_01.UNSO_NO_L                 " & vbNewLine _
                                     & "LEFT  JOIN (                                                             " & vbNewLine _
                                     & "                  SELECT NRS_BR_CD                                       " & vbNewLine _
                                     & "                        ,UNSO_NO_L                                       " & vbNewLine _
                                     & "                        ,MIN(F04_01.SEIQ_GROUP_NO)        AS GROUP_NO    " & vbNewLine _
                                     & "                        ,SUM(   F04_01.DECI_UNCHIN                       " & vbNewLine _
                                     & "                              + F04_01.DECI_CITY_EXTC                    " & vbNewLine _
                                     & "                              + F04_01.DECI_WINT_EXTC                    " & vbNewLine _
                                     & "                              + F04_01.DECI_RELY_EXTC                    " & vbNewLine _
                                     & "                              + F04_01.DECI_TOLL                         " & vbNewLine _
                                     & "                              + F04_01.DECI_INSU                         " & vbNewLine _
                                     & "                             )                            AS UNCHIN      " & vbNewLine _
                                     & "                        ,MAX(F04_01.SEIQ_KYORI)           AS KYORI       " & vbNewLine _
                                     & "                        ,MIN(F04_01.SEIQ_FIXED_FLAG)      AS SEIQ_FIXED_FLAG  " & vbNewLine _
                                     & "                    FROM $LM_TRN$..F_UNCHIN_TRS       F04_01             " & vbNewLine _
                                     & "                   WHERE SYS_DEL_FLG = '0'                               " & vbNewLine _
                                     & "                GROUP BY NRS_BR_CD                                       " & vbNewLine _
                                     & "                        ,UNSO_NO_L                                       " & vbNewLine _
                                     & "            )                           F04_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = F04_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.UNSO_NO_L                = F04_01.UNSO_NO_L                 " & vbNewLine _
                                     & "--START UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加) " & vbNewLine _
                                     & "LEFT  JOIN (                                                             " & vbNewLine _
                                     & "                  SELECT NRS_BR_CD                                       " & vbNewLine _
                                     & "                        ,UNSO_NO_L                                       " & vbNewLine _
                                     & "                        ,MIN(F05_01.SHIHARAI_GROUP_NO)    AS GROUP_NO    " & vbNewLine _
                                     & "                        ,SUM(   F05_01.DECI_UNCHIN                       " & vbNewLine _
                                     & "                              + F05_01.DECI_CITY_EXTC                    " & vbNewLine _
                                     & "                              + F05_01.DECI_WINT_EXTC                    " & vbNewLine _
                                     & "                              + F05_01.DECI_RELY_EXTC                    " & vbNewLine _
                                     & "                              + F05_01.DECI_TOLL                         " & vbNewLine _
                                     & "                              + F05_01.DECI_INSU                         " & vbNewLine _
                                     & "                             )                   AS SHIHARAI_UNCHIN      " & vbNewLine _
                                     & "                        ,MAX(F05_01.SHIHARAI_KYORI)           AS KYORI   " & vbNewLine _
                                     & "                        ,MAX(F05_01.SHIHARAI_FIXED_FLAG)  AS SHIHARAI_FIXED_FLAG       " & vbNewLine _
                                     & "                    FROM $LM_TRN$..F_SHIHARAI_TRS       F05_01           " & vbNewLine _
                                     & "                   WHERE SYS_DEL_FLG = '0'                               " & vbNewLine _
                                     & "                GROUP BY NRS_BR_CD                                       " & vbNewLine _
                                     & "                        ,UNSO_NO_L                                       " & vbNewLine _
                                     & "            )                           F05_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = F05_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.UNSO_NO_L                = F05_01.UNSO_NO_L                 " & vbNewLine _
                                     & "--END UMANO 要望番号1302 支払運賃に伴う修正。(支払運賃TRSのJOINを追加)   " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_CUST             M07_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M07_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.CUST_CD_L                = M07_01.CUST_CD_L                 " & vbNewLine _
                                     & " AND  F02_01.CUST_CD_M                = M07_01.CUST_CD_M                 " & vbNewLine _
                                     & " AND  M07_01.CUST_CD_S                = '00'                             " & vbNewLine _
                                     & " AND  M07_01.CUST_CD_SS               = '00'                             " & vbNewLine _
                                     & " AND  M07_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M38_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.UNSO_CD                  = M38_01.UNSOCO_CD                 " & vbNewLine _
                                     & " AND  F02_01.UNSO_BR_CD               = M38_01.UNSOCO_BR_CD              " & vbNewLine _
                                     & " AND  M38_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_02                           " & vbNewLine _
                                     & "  ON  F01_01.NRS_BR_CD                = M38_02.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F01_01.UNSOCO_CD                = M38_02.UNSOCO_CD                 " & vbNewLine _
                                     & " AND  F01_01.UNSOCO_BR_CD             = M38_02.UNSOCO_BR_CD              " & vbNewLine _
                                     & " AND  M38_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_03                           " & vbNewLine _
                                     & "  ON  F01_02.NRS_BR_CD                = M38_03.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F01_02.UNSOCO_CD                = M38_03.UNSOCO_CD                 " & vbNewLine _
                                     & " AND  F01_02.UNSOCO_BR_CD             = M38_03.UNSOCO_BR_CD              " & vbNewLine _
                                     & " AND  M38_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_04                           " & vbNewLine _
                                     & "  ON  F01_03.NRS_BR_CD                = M38_04.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F01_03.UNSOCO_CD                = M38_04.UNSOCO_CD                 " & vbNewLine _
                                     & " AND  F01_03.UNSOCO_BR_CD             = M38_04.UNSOCO_BR_CD              " & vbNewLine _
                                     & " AND  M38_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_UNSOCO           M38_05                           " & vbNewLine _
                                     & "  ON  F01_04.NRS_BR_CD                = M38_05.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F01_04.UNSOCO_CD                = M38_05.UNSOCO_CD                 " & vbNewLine _
                                     & " AND  F01_04.UNSOCO_BR_CD             = M38_05.UNSOCO_BR_CD              " & vbNewLine _
                                     & " AND  M38_05.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_DEST             M10_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M10_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.CUST_CD_L                = M10_01.CUST_CD_L                 " & vbNewLine _
                                     & " AND  F02_01.ORIG_CD                  = M10_01.DEST_CD                   " & vbNewLine _
                                     & " AND  M10_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_DEST             M10_02                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M10_02.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.CUST_CD_L                = M10_02.CUST_CD_L                 " & vbNewLine _
                                     & " AND  F02_01.DEST_CD                  = M10_02.DEST_CD                   " & vbNewLine _
                                     & " AND  M10_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_JIS              M10_02_JIS                       " & vbNewLine _
                                     & "  ON  M10_02.JIS                      = M10_02_JIS.JIS_CD                " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_DEST             M10_03                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M10_03.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  'ZZZZZ'                         = M10_03.CUST_CD_L                 " & vbNewLine _
                                     & " AND  F02_01.ORIG_CD                  = M10_03.DEST_CD                   " & vbNewLine _
                                     & " AND  M10_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_DEST             M10_04                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M10_04.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  'ZZZZZ'                         = M10_04.CUST_CD_L                 " & vbNewLine _
                                     & " AND  F02_01.DEST_CD                  = M10_04.DEST_CD                   " & vbNewLine _
                                     & " AND  M10_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_JIS              M10_04_JIS                       " & vbNewLine _
                                     & "  ON  M10_04.JIS                      = M10_04_JIS.JIS_CD                " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_JIS              M12_01                           " & vbNewLine _
                                     & "  ON  F02_01.SYUKA_TYUKEI_CD          = M12_01.JIS_CD                    " & vbNewLine _
                                     & " AND  M12_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_JIS              M12_02                           " & vbNewLine _
                                     & "  ON  F02_01.HAIKA_TYUKEI_CD          = M12_02.JIS_CD                    " & vbNewLine _
                                     & " AND  M12_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_AREA             M36_01                           " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = M36_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.AREA_CD                  = M36_01.AREA_CD                   " & vbNewLine _
                                     & " --要望番号1202 追加START(2012.07.02)                                    " & vbNewLine _
                                     & " AND  F02_01.BIN_KB                   = M36_01.BIN_KB                    " & vbNewLine _
                                     & " --要望番号1202 追加END  (2012.07.02)                                    " & vbNewLine _
                                     & " AND  M10_02.JIS                      = M36_01.JIS_CD                    " & vbNewLine _
                                     & " AND  M36_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_VCLE             M39_01                           " & vbNewLine _
                                     & "  ON  F01_01.CAR_KEY                  = M39_01.CAR_KEY                   " & vbNewLine _
                                     & " AND  M39_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_VCLE             M39_02                           " & vbNewLine _
                                     & "  ON  F01_04.CAR_KEY                  = M39_02.CAR_KEY                   " & vbNewLine _
                                     & " AND  M39_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_01                           " & vbNewLine _
                                     & "  ON  F01_01.DRIVER_CD                = M37_01.DRIVER_CD                 " & vbNewLine _
                                     & " AND  M37_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..M_DRIVER           M37_02                           " & vbNewLine _
                                     & "  ON  F01_04.DRIVER_CD                = M37_02.DRIVER_CD                 " & vbNewLine _
                                     & " AND  M37_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_01                           " & vbNewLine _
                                     & "  ON  F02_01.BIN_KB                   = KBN_01.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_01.KBN_GROUP_CD             = 'U001'                           " & vbNewLine _
                                     & " AND  KBN_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_02                           " & vbNewLine _
                                     & "  ON  F02_01.TARIFF_BUNRUI_KB         = KBN_02.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_02.KBN_GROUP_CD             = 'T015'                           " & vbNewLine _
                                     & " AND  KBN_02.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_03                           " & vbNewLine _
                                     & "  ON  F02_01.UNSO_ONDO_KB             = KBN_03.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_03.KBN_GROUP_CD             = 'U006'                           " & vbNewLine _
                                     & " AND  KBN_03.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_04                           " & vbNewLine _
                                     & "  ON  F02_01.MOTO_DATA_KB             = KBN_04.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_04.KBN_GROUP_CD             = 'M004'                           " & vbNewLine _
                                     & " AND  KBN_04.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_05                           " & vbNewLine _
                                     & "  ON  M39_01.CAR_TP_KB                = KBN_05.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_05.KBN_GROUP_CD             = 'S023'                           " & vbNewLine _
                                     & " AND  KBN_05.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_06                           " & vbNewLine _
                                     & "  ON  M39_02.CAR_TP_KB                = KBN_06.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_06.KBN_GROUP_CD             = 'S023'                           " & vbNewLine _
                                     & " AND  KBN_06.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "--ADD 2018/12/19 START 要望管理000880                                    " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_07                           " & vbNewLine _
                                     & "  ON  F01_01.BIN_KB                   = KBN_07.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_07.KBN_GROUP_CD             = 'U001'                           " & vbNewLine _
                                     & " AND  KBN_07.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_08                           --ADD 2022.08.22 " & vbNewLine _
                                     & "  ON  F02_01.HAISO_PF_SEND_FLG        = KBN_08.KBN_CD                    --ADD 2022.08.22 " & vbNewLine _
                                     & " AND  KBN_08.KBN_GROUP_CD             = 'U009'                           --ADD 2022.08.22 " & vbNewLine _
                                     & " AND  KBN_08.SYS_DEL_FLG              = '0'                              --ADD 2022.08.22 " & vbNewLine _
                                     & "--ADD 2018/12/19 END   要望管理000880                                    " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..S_USER             S01_01                           " & vbNewLine _
                                     & "  ON  F02_01.SYS_ENT_USER             = S01_01.USER_CD                   " & vbNewLine _
                                     & " AND  S01_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & " --要望番号2140 (2013.12.25) 追加START                                   " & vbNewLine _
                                     & "LEFT  JOIN $LM_TRN$..C_OUTKA_L        C01_01                             " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = C01_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.INOUTKA_NO_L             = C01_01.OUTKA_NO_L                " & vbNewLine _
                                     & " AND  F02_01.MOTO_DATA_KB             = '20'                             " & vbNewLine _
                                     & " AND  C01_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                  " & vbNewLine _
                                     & " --要望番号005193 ADD 2019/08/05 START                                   " & vbNewLine _
                                     & "LEFT  JOIN $LM_TRN$..B_INKA_L         B01_01                             " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = B01_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  F02_01.INOUTKA_NO_L             = B01_01.INKA_NO_L                 " & vbNewLine _
                                     & " AND  F02_01.MOTO_DATA_KB             = '10'                             " & vbNewLine _
                                     & " AND  C01_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & " --要望番号005193 ADD 2019/08/05 end                                     " & vbNewLine _
                                     & " --要望番号2063 (2015.05.27) 追加START                                   " & vbNewLine _
                                     & "LEFT  JOIN $LM_TRN$..H_TEHAIINFO_TBL    H_TEHAI                          " & vbNewLine _
                                     & "  ON  F02_01.NRS_BR_CD                = H_TEHAI.NRS_BR_CD                " & vbNewLine _
                                     & " AND  F02_01.TRIP_NO                  = H_TEHAI.TRIP_NO                  " & vbNewLine _
                                     & " AND  F02_01.UNSO_NO_L                = H_TEHAI.UNSO_NO_L                " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_27                           " & vbNewLine _
                                     & "  ON  H_TEHAI.TEHAI_SYUBETSU          = KBN_27.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_27.KBN_GROUP_CD             = 'U027'                           " & vbNewLine _
                                     & " AND  KBN_27.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & " --要望番号2140 (2013.12.25) 追加END                                     " & vbNewLine _
                                     & "LEFT JOIN  $LM_TRN$..C_OUTKA_L          OUTKA_L                          " & vbNewLine _
                                     & "  ON  OUTKA_L.NRS_BR_CD               = F02_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  OUTKA_L.OUTKA_NO_L              = F02_01.INOUTKA_NO_L              " & vbNewLine _
                                     & " AND  OUTKA_L.SYS_DEL_FLG             = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN (                                                             " & vbNewLine _
                                     & "            SELECT                                                       " & vbNewLine _
                                     & "               OUTKA_NO_S.NRS_BR_CD                                      " & vbNewLine _
                                     & "              ,OUTKA_NO_S.OUTKA_NO_L                                     " & vbNewLine _
                                     & "              ,OUTKA_NO_M.OUTKA_NO_M                                     " & vbNewLine _
                                     & "              ,MIN(OUTKA_NO_S.OUTKA_NO_S)  AS OUTKA_NO_S                 " & vbNewLine _
                                     & "            FROM $LM_TRN$..C_OUTKA_S  OUTKA_NO_S                         " & vbNewLine _
                                     & "            LEFT  JOIN (                                                 " & vbNewLine _
                                     & "                        SELECT                                           " & vbNewLine _
                                     & "                           NRS_BR_CD                                     " & vbNewLine _
                                     & "                          ,OUTKA_NO_L                                    " & vbNewLine _
                                     & "                          ,MIN(OUTKA_NO_M)  AS OUTKA_NO_M                " & vbNewLine _
                                     & "                        FROM $LM_TRN$..C_OUTKA_S  OUTKA_NO_M             " & vbNewLine _
                                     & "                        WHERE OUTKA_NO_M.SYS_DEL_FLG = 0                 " & vbNewLine _
                                     & "                        GROUP BY NRS_BR_CD, OUTKA_NO_L                   " & vbNewLine _
                                     & "                        )  AS OUTKA_NO_M                                 " & vbNewLine _
                                     & "                         ON OUTKA_NO_M.NRS_BR_CD = OUTKA_NO_S.NRS_BR_CD  " & vbNewLine _
                                     & "                        AND OUTKA_NO_M.OUTKA_NO_L = OUTKA_NO_S.OUTKA_NO_L" & vbNewLine _
                                     & "                        AND OUTKA_NO_M.OUTKA_NO_M = OUTKA_NO_S.OUTKA_NO_M" & vbNewLine _
                                     & "            WHERE OUTKA_NO_S.SYS_DEL_FLG = 0                             " & vbNewLine _
                                     & "              AND OUTKA_NO_M.OUTKA_NO_L IS NOT NULL                      " & vbNewLine _
                                     & "            GROUP BY OUTKA_NO_S.NRS_BR_CD, OUTKA_NO_S.OUTKA_NO_L, OUTKA_NO_M.OUTKA_NO_M  " & vbNewLine _
                                     & "            )  OUTKA_GRP_S                                               " & vbNewLine _
                                     & "  ON  OUTKA_GRP_S.NRS_BR_CD               = OUTKA_L.NRS_BR_CD            " & vbNewLine _
                                     & " AND  OUTKA_GRP_S.OUTKA_NO_L              = OUTKA_L.OUTKA_NO_L           " & vbNewLine _
                                     & "LEFT JOIN  $LM_TRN$..C_OUTKA_S          OUTKA_S                          " & vbNewLine _
                                     & "  ON  OUTKA_S.NRS_BR_CD               = OUTKA_GRP_S.NRS_BR_CD            " & vbNewLine _
                                     & " AND  OUTKA_S.OUTKA_NO_L              = OUTKA_GRP_S.OUTKA_NO_L           " & vbNewLine _
                                     & " AND  OUTKA_S.OUTKA_NO_M              = OUTKA_GRP_S.OUTKA_NO_M           " & vbNewLine _
                                     & " AND  OUTKA_S.OUTKA_NO_S              = OUTKA_GRP_S.OUTKA_NO_S           " & vbNewLine _
                                     & " AND  OUTKA_S.SYS_DEL_FLG             = '0'                              " & vbNewLine _
                                     & "LEFT JOIN  $LM_MST$..M_TOU_SITU         OUTKA_TOU_SITU                   " & vbNewLine _
                                     & "  ON  OUTKA_TOU_SITU.NRS_BR_CD        = OUTKA_L.NRS_BR_CD                " & vbNewLine _
                                     & " AND  OUTKA_TOU_SITU.WH_CD            = OUTKA_L.WH_CD                    " & vbNewLine _
                                     & " AND  OUTKA_TOU_SITU.TOU_NO           = OUTKA_S.TOU_NO                   " & vbNewLine _
                                     & " AND  OUTKA_TOU_SITU.SITU_NO          = OUTKA_S.SITU_NO                  " & vbNewLine _
                                     & " AND  OUTKA_TOU_SITU.SYS_DEL_FLG      = '0'                              " & vbNewLine _
                                     & "LEFT JOIN  $LM_TRN$..B_INKA_L           INKA_L                           " & vbNewLine _
                                     & "  ON  INKA_L.NRS_BR_CD                = F02_01.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  INKA_L.INKA_NO_L                = F02_01.INOUTKA_NO_L              " & vbNewLine _
                                     & " AND  INKA_L.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN (                                                             " & vbNewLine _
                                     & "            SELECT                                                       " & vbNewLine _
                                     & "               INKA_NO_S.NRS_BR_CD                                       " & vbNewLine _
                                     & "              ,INKA_NO_S.INKA_NO_L                                       " & vbNewLine _
                                     & "              ,INKA_NO_M.INKA_NO_M                                       " & vbNewLine _
                                     & "              ,MIN(INKA_NO_S.INKA_NO_S)  AS INKA_NO_S                    " & vbNewLine _
                                     & "            FROM $LM_TRN$..B_INKA_S  INKA_NO_S                           " & vbNewLine _
                                     & "            LEFT  JOIN (                                                 " & vbNewLine _
                                     & "                        SELECT                                           " & vbNewLine _
                                     & "                           NRS_BR_CD                                     " & vbNewLine _
                                     & "                          ,INKA_NO_L                                     " & vbNewLine _
                                     & "                          ,MIN(INKA_NO_M)  AS INKA_NO_M                  " & vbNewLine _
                                     & "                        FROM $LM_TRN$..B_INKA_S  INKA_NO_M               " & vbNewLine _
                                     & "                        WHERE INKA_NO_M.SYS_DEL_FLG = 0                  " & vbNewLine _
                                     & "                        GROUP BY NRS_BR_CD, INKA_NO_L                    " & vbNewLine _
                                     & "                        )  AS INKA_NO_M                                  " & vbNewLine _
                                     & "                         ON INKA_NO_M.NRS_BR_CD = INKA_NO_S.NRS_BR_CD    " & vbNewLine _
                                     & "                        AND INKA_NO_M.INKA_NO_L = INKA_NO_S.INKA_NO_L    " & vbNewLine _
                                     & "                        AND INKA_NO_M.INKA_NO_M = INKA_NO_S.INKA_NO_M    " & vbNewLine _
                                     & "            WHERE INKA_NO_S.SYS_DEL_FLG = 0                              " & vbNewLine _
                                     & "              AND INKA_NO_M.INKA_NO_L IS NOT NULL                        " & vbNewLine _
                                     & "            GROUP BY INKA_NO_S.NRS_BR_CD, INKA_NO_S.INKA_NO_L, INKA_NO_M.INKA_NO_M  " & vbNewLine _
                                     & "            )  INKA_GRP_S                                                " & vbNewLine _
                                     & "  ON  INKA_GRP_S.NRS_BR_CD           = INKA_L.NRS_BR_CD                  " & vbNewLine _
                                     & " AND  INKA_GRP_S.INKA_NO_L           = OUTKA_L.OUTKA_NO_L                " & vbNewLine _
                                     & "LEFT JOIN  $LM_TRN$..B_INKA_S          INKA_S                            " & vbNewLine _
                                     & "  ON  INKA_S.NRS_BR_CD               = INKA_GRP_S.NRS_BR_CD              " & vbNewLine _
                                     & " AND  INKA_S.INKA_NO_L               = INKA_GRP_S.INKA_NO_L              " & vbNewLine _
                                     & " AND  INKA_S.INKA_NO_M               = INKA_GRP_S.INKA_NO_M              " & vbNewLine _
                                     & " AND  INKA_S.INKA_NO_S               = INKA_GRP_S.INKA_NO_S              " & vbNewLine _
                                     & " AND  INKA_S.SYS_DEL_FLG             = '0'                               " & vbNewLine _
                                     & "LEFT JOIN  $LM_MST$..M_TOU_SITU         INKA_TOU_SITU                    " & vbNewLine _
                                     & "  ON  INKA_TOU_SITU.NRS_BR_CD         = INKA_L.NRS_BR_CD                 " & vbNewLine _
                                     & " AND  INKA_TOU_SITU.WH_CD             = INKA_L.WH_CD                     " & vbNewLine _
                                     & " AND  INKA_TOU_SITU.TOU_NO            = INKA_S.TOU_NO                    " & vbNewLine _
                                     & " AND  INKA_TOU_SITU.SITU_NO           = INKA_S.SITU_NO                   " & vbNewLine _
                                     & " AND  INKA_TOU_SITU.SYS_DEL_FLG       = '0'                              " & vbNewLine _
                                     & "LEFT  JOIN $LM_MST$..Z_KBN              KBN_9                            " & vbNewLine _
                                     & "  ON  F04_01.SEIQ_FIXED_FLAG          = KBN_9.KBN_CD                    " & vbNewLine _
                                     & " AND  KBN_9.KBN_GROUP_CD              = 'U009'                           " & vbNewLine _
                                     & " AND  KBN_9.SYS_DEL_FLG               = '0'                              " & vbNewLine _
                                     & "WHERE F02_01.NRS_BR_CD                = @NRS_BR_CD                       " & vbNewLine _
                                     & "  AND F02_01.SYS_DEL_FLG              = '0'                              " & vbNewLine _
                                     & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 -- START --                  " & vbNewLine _
                                     & "  AND F02_01.UNSO_TEHAI_KB            = '10'                             " & vbNewLine _
                                     & "--(2013.01.25)要望番号1503 日陸手配のみ抽出 --  END  --                  " & vbNewLine

#End Region

#Region "排他"

    Private Const SQL_SELECT_HAITA As String = "SELECT COUNT(UNSO_NO_L) AS REC_CNT" & vbNewLine _
                                             & "FROM $LM_TRN$..F_UNSO_L           " & vbNewLine _
                                             & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                             & "  AND UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                             & "  AND SYS_DEL_FLG  = '0'          " & vbNewLine _
                                             & "  AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                             & "  AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

#End Region

#Region "キャンセル"

    Private Const SQL_SELECT_CANCEL As String = "SELECT COUNT(LL.TRIP_NO) AS REC_CNT                                " & vbNewLine _
                                             & "FROM AB_DB..HB_LMS_UNSO_LL LL                                       " & vbNewLine _
                                             & "LEFT  JOIN (                                                        " & vbNewLine _
                                             & "                  SELECT MAX(INFO_A.RECEIVE_NO)  AS KEY_RECEIVE_NO  " & vbNewLine _
                                             & "                        ,TRIP_NO  AS TRIP_NO                        " & vbNewLine _
                                             & "                        ,NRS_BR_CD  AS NRS_BR_CD                    " & vbNewLine _
                                             & "                        ,UNSOCO_CD  AS UNSOCO_CD                    " & vbNewLine _
                                             & "                        ,UNSOCO_BR_CD  AS UNSOCO_BR_CD              " & vbNewLine _
                                             & "                    FROM AB_DB..HB_LMS_UNSO_LL  INFO_A              " & vbNewLine _
                                             & "                   WHERE INFO_A.SYS_DEL_FLG = '0'                   " & vbNewLine _
                                             & "                GROUP BY TRIP_NO                                    " & vbNewLine _
                                             & "                        ,NRS_BR_CD                                  " & vbNewLine _
                                             & "                        ,UNSOCO_CD                                  " & vbNewLine _
                                             & "                        ,UNSOCO_BR_CD                               " & vbNewLine _
                                             & "            )                           KEY_DATE                    " & vbNewLine _
                                             & "  ON  KEY_DATE.KEY_RECEIVE_NO = LL.RECEIVE_NO                       " & vbNewLine _
                                             & " AND  KEY_DATE.TRIP_NO = LL.TRIP_NO                                 " & vbNewLine _
                                             & " AND  KEY_DATE.NRS_BR_CD = LL.NRS_BR_CD                             " & vbNewLine _
                                             & " AND  KEY_DATE.UNSOCO_CD = LL.UNSOCO_CD                             " & vbNewLine _
                                             & " AND  KEY_DATE.UNSOCO_BR_CD = LL.UNSOCO_BR_CD                       " & vbNewLine _
                                             & "WHERE LL.TRIP_NO = @TRIP_NO                                         " & vbNewLine _
                                             & "  AND KEY_DATE.KEY_RECEIVE_NO IS NOT NULL                           " & vbNewLine _
                                             & "  AND LL.CANCEL_FLG = '00000'                                       " & vbNewLine _
                                             & "  AND LL.SYS_DEL_FLG  = '0'                                         " & vbNewLine _

#End Region


#Region "F_UNSO_LL"

    Private Const SQL_SELECT_COUNT_LL As String = " SELECT COUNT(F01_01.TRIP_NO) AS REC_CNT " & vbNewLine _
                                                & "   FROM $LM_TRN$..F_UNSO_LL F01_01       " & vbNewLine _
                                                & "  WHERE F01_01.TRIP_NO     = @TRIP_NO    " & vbNewLine

#End Region

#Region "F_UNCHIN_TRS"

    Private Const SQL_SELECT_UNCHIN As String = " SELECT SEIQTO_CD             AS SEIQTO_CD            " & vbNewLine _
                                              & "       ,UNTIN_CALCULATION_KB  AS UNTIN_CALCULATION_KB " & vbNewLine _
                                              & "       ,SEIQ_TARIFF_BUNRUI_KB AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
                                              & "   FROM $LM_TRN$..F_UNCHIN_TRS F04_01                 " & vbNewLine _
                                              & "  WHERE F04_01.NRS_BR_CD   = @NRS_BR_CD               " & vbNewLine _
                                              & "    AND F04_01.UNSO_NO_L   = @UNSO_NO_L               " & vbNewLine _
                                              & "    AND F04_01.SYS_DEL_FLG = '0'                      " & vbNewLine

#End Region

    'START UMANO 要望番号1369 運行紐付け対応
#Region "M_UNSOCO"

    Private Const SQL_SELECT_UNSOCO As String = " SELECT                                              " & vbNewLine _
                                                & " M38.UNCHIN_TARIFF_CD      AS  UNCHIN_TARIFF_CD    " & vbNewLine _
                                                & ",M38.EXTC_TARIFF_CD        AS  EXTC_TARIFF_CD      " & vbNewLine _
                                                & " ,CASE WHEN M38.AUTO_DENP_NO_KBN IS NULL THEN ''   " & vbNewLine _
                                                & "       WHEN M38.AUTO_DENP_NO_FLG = '1' THEN M38.AUTO_DENP_NO_KBN " & vbNewLine _
                                                & "       ELSE ''             END AUTO_DENP_KBN       " & vbNewLine _
                                                & "   FROM $LM_MST$..M_UNSOCO M38                     " & vbNewLine _
                                                & "  WHERE M38.NRS_BR_CD     = @NRS_BR_CD             " & vbNewLine _
                                                & "  AND   M38.UNSOCO_CD     = @UNSO_CD             " & vbNewLine _
                                                & "  AND   M38.UNSOCO_BR_CD  = @UNSO_BR_CD          " & vbNewLine

#End Region
    'END UMANO 要望番号1369 運行紐付け対応

    'START KIM   要望番号1485 支払い関連修正
    'START UMANO 要望番号1369 運行紐付け対応
#Region "F_UNSO_LL(支払運賃タリフ取得)"

    'Private Const SQL_SELECT_UNCO As String = " SELECT                                               " & vbNewLine _
    '                                        & "  F01.SHIHARAI_TARIFF_CD     AS UNCHIN_TARIFF_CD      " & vbNewLine _
    '                                        & " ,F01.SHIHARAI_ETARIFF_CD    AS EXTC_TARIFF_CD        " & vbNewLine _
    '                                        & " FROM $LM_TRN$..F_UNSO_LL F01                         " & vbNewLine _
    '                                        & "  WHERE F01.TRIP_NO     = @TRIP_NO                    " & vbNewLine

    Private Const SQL_SELECT_UNCO As String = " SELECT                                              " & vbNewLine _
                                           & "  F01.SHIHARAI_TARIFF_CD     AS UNCHIN_TARIFF_CD      " & vbNewLine _
                                           & " ,F01.SHIHARAI_ETARIFF_CD    AS EXTC_TARIFF_CD        " & vbNewLine _
                                           & " ,ISNULL(UNSOCO.SHIHARAITO_CD,'') AS SHIHARAITO_CD    " & vbNewLine _
                                           & " FROM $LM_TRN$..F_UNSO_LL F01                         " & vbNewLine _
                                           & " LEFT OUTER JOIN                                      " & vbNewLine _
                                           & "     $LM_MST$..M_UNSOCO UNSOCO                        " & vbNewLine _
                                           & "   ON UNSOCO.NRS_BR_CD    = F01.NRS_BR_CD             " & vbNewLine _
                                           & "  AND UNSOCO.UNSOCO_CD    = F01.UNSOCO_CD             " & vbNewLine _
                                           & "  AND UNSOCO.UNSOCO_BR_CD = F01.UNSOCO_BR_CD          " & vbNewLine _
                                           & "  AND UNSOCO.SYS_DEL_FLG  = '0'                       " & vbNewLine _
                                           & "  WHERE F01.TRIP_NO     = @TRIP_NO                    " & vbNewLine


#End Region
    'END UMANO 要望番号1369 運行紐付け対応
    'END KIM   要望番号1485 支払い関連修正

#Region "コンボ用"

    Private Const SQL_SELECT_CMB_DATA As String = "SELECT                          " & vbNewLine _
                                                & " DISTINCT                       " & vbNewLine _
                                                & " A.JIS_CD AS CD                 " & vbNewLine _
                                                & ",B.KEN + B.SHI AS NM            " & vbNewLine _
                                                & " FROM $LM_MST$..M_SOKO A        " & vbNewLine _
                                                & " LEFT JOIN $LM_MST$..M_JIS B    " & vbNewLine _
                                                & "   ON A.JIS_CD = B.JIS_CD       " & vbNewLine _
                                                & "  AND B.SYS_DEL_FLG = 0         " & vbNewLine _
                                                & "WHERE A.NRS_BR_CD   = @NRS_BR_CD" & vbNewLine _
                                                & "  AND A.WH_KB       = '01'      " & vbNewLine _
                                                & "  AND A.JIS_CD     <> ''        " & vbNewLine _
                                                & "ORDER BY A.JIS_CD               " & vbNewLine



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

#End Region

#Region "設定処理 SQL"

#Region "解除"

    Private Const SQL_UPDATE_CANCEL As String = "UPDATE $LM_TRN$..F_UNSO_L SET " & vbNewLine _
                                              & " TRIP_NO           = ''       " & vbNewLine _
                                              & ",TRIP_NO_SYUKA     = ''       " & vbNewLine _
                                              & ",TRIP_NO_TYUKEI    = ''       " & vbNewLine _
                                              & ",TRIP_NO_HAIKA     = ''       " & vbNewLine

    Private Const SQL_UPDATE_CANCEL_TYUKEI As String = ",TYUKEI_HAISO_FLG  = '00'     " & vbNewLine _
                                                 & ",SYUKA_TYUKEI_CD   = ''       " & vbNewLine _
                                                 & ",HAIKA_TYUKEI_CD   = ''       " & vbNewLine

    'START UMANO 要望番号1369 運行紐付け対応
    Private Const SQL_UPDATE_CANCEL_STARIFF As String = ",SHIHARAI_TARIFF_CD  = @SHIHARAI_TARIFF_CD     " & vbNewLine _
                                                      & ",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD    " & vbNewLine
    'END UMANO 要望番号1369 運行紐付け対応


#End Region

#Region "更新共通"

    Private Const SQL_UPDATE As String = ",SYS_UPD_DATE      = @SYS_UPD_DATE" & vbNewLine _
                                       & ",SYS_UPD_TIME      = @SYS_UPD_TIME" & vbNewLine _
                                       & ",SYS_UPD_PGID      = @SYS_UPD_PGID" & vbNewLine _
                                       & ",SYS_UPD_USER      = @SYS_UPD_USER" & vbNewLine _
                                       & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                       & "  AND UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                       & "  AND SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                       & "  AND SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

#End Region

#Region "F_UNSO_LL"

    Private Const SQL_UPDATE_LL As String = "UPDATE $LM_TRN$..F_UNSO_LL SET    " & vbNewLine _
                                          & " SYS_UPD_DATE      = @SYS_UPD_DATE" & vbNewLine _
                                          & ",SYS_UPD_TIME      = @SYS_UPD_TIME" & vbNewLine _
                                          & ",SYS_UPD_PGID      = @SYS_UPD_PGID" & vbNewLine _
                                          & ",SYS_UPD_USER      = @SYS_UPD_USER" & vbNewLine _
                                          & "WHERE NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                          & "  AND TRIP_NO      = @TRIP_NO     " & vbNewLine

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

    'START UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "SHIHARAI"

    Private Const SQL_DELETE_SHIHARAI As String = "DELETE FROM $LM_TRN$..F_SHIHARAI_TRS " & vbNewLine _
                                              & "WHERE   NRS_BR_CD   = @NRS_BR_CD   " & vbNewLine _
                                              & "  AND   UNSO_NO_L   = @UNSO_NO_L   " & vbNewLine

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
#Region "UNCHIN"

    ''' <summary>
    ''' (支払)運賃テーブルの物理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>支払運賃テーブル削除SQLの構築・発行</remarks>
    Private Function DeleteShiharaiData(ByVal ds As DataSet) As DataSet

        Return Me.DeleteComData(ds, LMF010DAC.SQL_DELETE_SHIHARAI)

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
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
        'Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_SHIHARAI_UNCHIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList.Clear()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(sql, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSelectParam(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        MyBase.GetUpdateResult(cmd)

        Return ds

    End Function

#End Region
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START KIM 要望番号1485 支払い関連修正。
#Region "SHIHARAI"

    Private Const SQL_UPDATE_SHIHARAI As String = " UPDATE $LM_TRN$..F_SHIHARAI_TRS SET     " & vbNewLine _
                                                & "        SHIHARAITO_CD = @SHIHARAITO_CD   " & vbNewLine _
                                                & "       ,SYS_UPD_DATE  = @SYS_UPD_DATE    " & vbNewLine _
                                                & "       ,SYS_UPD_TIME  = @SYS_UPD_TIME    " & vbNewLine _
                                                & "       ,SYS_UPD_PGID  = @SYS_UPD_PGID    " & vbNewLine _
                                                & "       ,SYS_UPD_USER  = @SYS_UPD_USER    " & vbNewLine _
                                                & "WHERE   NRS_BR_CD     = @NRS_BR_CD       " & vbNewLine _
                                                & "  AND   UNSO_NO_L     = @UNSO_NO_L       " & vbNewLine

#End Region
    'END KIM 要望番号1485 支払い関連修正。

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
    ''' 運送(大)検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)件数取得SQLの構築・発行</remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_IN)
        Dim fromUnsoLL As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        fromUnsoLL = Me.GetUnsoLLSchema(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF010DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_1)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_2)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_3)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_4)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_5)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)検索対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)対象データ取得SQLの構築・発行</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_IN)
        Dim fromUnsoLL As String = String.Empty
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        fromUnsoLL = Me.GetUnsoLLSchema(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF010DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_1)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_2)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_3)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_4)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF010DAC.SQL_FROM_5)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Me._StrSql.Append(" ORDER BY F02_01.UNSO_NO_L ")

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("BIN", "BIN")
        map.Add("BIN_UNSO_LL", "BIN_UNSO_LL")   'ADD 2018/12/19 要望管理000880
        map.Add("TARIFF_BUNRUI_KB", "TARIFF_BUNRUI_KB")
        map.Add("BUNRUI", "BUNRUI")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("ORIG_CD", "ORIG_CD")
        map.Add("ORIG_NM", "ORIG_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_ADD", "DEST_ADD")
        map.Add("TASYA_WH_NM", "TASYA_WH_NM")
        map.Add("AREA", "AREA")
        map.Add("UNSO_PKG_NB", "UNSO_PKG_NB")
        map.Add("UNSO_WT", "UNSO_WT")
        map.Add("SHOMI_JURYO", "SHOMI_JURYO")
        map.Add("KANRI_NO", "KANRI_NO")
        map.Add("WH_CD", "WH_CD")                       'ADD 2019/08/05 005193
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("DRIVER_NM", "DRIVER_NM")
        map.Add("TRIP_DATE", "TRIP_DATE")
        map.Add("CAR_NO", "CAR_NO")
        map.Add("CAR_TP", "CAR_TP")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("REMARK", "REMARK")
        map.Add("UNCHIN", "UNCHIN")
        'START UMANO 要望番号1302 支払運賃に伴う修正。
        map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        'END UMANO 要望番号1302 支払運賃に伴う修正。
        map.Add("KYORI", "KYORI")
        map.Add("GROUP_NO", "GROUP_NO")
        map.Add("UNSO_ONDO", "UNSO_ONDO")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("MOTO_DATA", "MOTO_DATA")
        map.Add("SYUKA_TYUKEI_NM", "SYUKA_TYUKEI_NM")
        map.Add("HAIKA_TYUKEI_NM", "HAIKA_TYUKEI_NM")
        map.Add("TRIP_NO_SYUKA", "TRIP_NO_SYUKA")
        map.Add("TRIP_NO_TYUKEI", "TRIP_NO_TYUKEI")
        map.Add("TRIP_NO_HAIKA", "TRIP_NO_HAIKA")
        map.Add("UNSOCO_SYUKA", "UNSOCO_SYUKA")
        map.Add("UNSOCO_BR_SYUKA", "UNSOCO_BR_SYUKA")
        map.Add("UNSOCO_TYUKEI", "UNSOCO_TYUKEI")
        map.Add("UNSOCO_BR_TYUKEI", "UNSOCO_BR_TYUKEI")
        map.Add("UNSOCO_HAIKA", "UNSOCO_HAIKA")
        map.Add("UNSOCO_BR_HAIKA", "UNSOCO_BR_HAIKA")
        map.Add("TYUKEI_HAISO_FLG", "TYUKEI_HAISO_FLG")
        map.Add("UNSO_TEHAI_KB", "UNSO_TEHAI_KB")
        map.Add("DEST_ADD2", "DEST_ADD2")
        '要望番号2140 追加START 2013.12.25
        map.Add("ALCTD_STS", "ALCTD_STS")
        '要望番号2140 追加END 2013.12.25
        map.Add("SYS_ENT_NM", "SYS_ENT_NM")
        map.Add("SYS_ENT_DATE", "SYS_ENT_DATE")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("C_SELECT", "C_SELECT")
        '要望番号2063 追加START 2015.05.27
        map.Add("TEHAI_JYOKYO", "TEHAI_JYOKYO")
        map.Add("TEHAI_SYUBETSU", "TEHAI_SYUBETSU")
        '要望番号2063 追加END 2015.05.27

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
        map.Add("AUTO_DENP_KBN", "AUTO_DENP_KBN")
        map.Add("AUTO_DENP_NO", "AUTO_DENP_NO")
#End If
        map.Add("DENP_NO", "DENP_NO")
        map.Add("NHS_REMARK", "NHS_REMARK")
        '2022.08.22 追加START
        map.Add("PF_SOSHIN", "PF_SOSHIN")
        map.Add("PF_SOSHIN_NM", "PF_SOSHIN_NM")
        '2022.08.22 追加END
        map.Add("SEIQ_FIXED_NM", "SEIQ_FIXED_NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_OUT)

        Return ds

    End Function

    ''' <summary>
    ''' 更新日付を入れた検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectHaitaData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsoPkParameter(Me._SqlPrmList, Me._Row)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF010DAC.SQL_SELECT_HAITA, Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        Call Me.UpdateResultChk(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 対象データキャンセルチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>データ取得SQLの構築・発行</remarks>
    Private Function SelectCancelData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_ITEM)

        For Each row As DataRow In inTbl.Rows

            'INTableの条件rowの格納
            Me._Row = row

            'SQL格納変数の初期化
            Me._StrSql = New StringBuilder()

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()

            'パラメータ設定
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", row.Item("TRIP_NO").ToString(), DBDataType.CHAR))

            'SQL文のコンパイル
            Dim cmd As SqlCommand = MyBase.CreateSqlCommand(LMF010DAC.SQL_SELECT_CANCEL)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

            '処理件数の設定
            reader.Read()
            Call Me.CancelResultChk(Convert.ToInt32(reader("REC_CNT")))
            reader.Close()

            If MyBase.IsMessageExist() Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送(特大)検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(特大)件数取得SQLの構築・発行</remarks>
    Private Function SelectLLCountData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_ITEM)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me.SetWhereData(Me._Row.Item("TRIP_NO").ToString(), LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF010DAC.SQL_SELECT_COUNT_LL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃テーブル取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsoPkParameter(Me._SqlPrmList, Me._Row)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF010DAC.SQL_SELECT_UNCHIN, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SEIQ_TARIFF_BUNRUI_KB", "SEIQ_TARIFF_BUNRUI_KB")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_UNCHIN)

        Return ds

    End Function

    'START UMANO 要望番号1369 支払運賃に伴う修正。
    ''' <summary>
    ''' 運送会社マスタ支払運賃タリフの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UnsoLShiharaiEdit(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得(登録・解除でINTBLの切替)
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_ITEM)
        '解除(運行番号の場合は運送LをINTBLにする)
        If inTbl.Rows(0).Item("ITEM_DATA").ToString().Equals(LMF010DAC.SHUSEI_TRIP) = True Then
            inTbl = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)
        End If

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUnsoEditParameter(Me._SqlPrmList, Me._Row)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF010DAC.SQL_SELECT_UNSOCO, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        map.Add("AUTO_DENP_KBN", "AUTO_DENP_KBN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_UNSOCO)

        Return ds

    End Function
    'END UMANO 要望番号1369 支払運賃に伴う修正。

    'START UMANO 要望番号1369 支払運賃に伴う修正。
    ''' <summary>
    ''' 運行データの支払運賃タリフコード取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUncodataTariff(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_ITEM)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Call Me.SetUncoParameter(Me._SqlPrmList, Me._Row)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF010DAC.SQL_SELECT_UNCO, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")
        map.Add("EXTC_TARIFF_CD", "EXTC_TARIFF_CD")
        'START KIM   要望番号1485 支払い関連修正
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        'END KIM   要望番号1485 支払い関連修正

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_UNSOCO)

        Return ds

    End Function
    'END UMANO 要望番号1369 支払運賃に伴う修正。

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me._Row.Item("SEIQTO_CD").ToString(), DBDataType.NVARCHAR)) '要望番号1664:(SEIQTO_CD DACの型) 2012/12/05 本明 VARCHAR,CHAR→NVARCHARに変更
        'START YANAI 要望番号827
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", ds.Tables(LMF010DAC.TABLE_NM_ITEM).Rows(0).Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        'END YANAI 要望番号827

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMG000DAC.SQL_SELECT_KEIRI_CHK_DATE, ds.Tables(LMF010DAC.TABLE_NM_ITEM).Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_G_HED)

        Return ds

    End Function

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCombData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF010DAC.SQL_SELECT_CMB_DATA, brCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("CD", "CD")
        map.Add("NM", "NM")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_CMB)

        Return ds

    End Function

    ''' <summary>
    ''' 区分マスタ取得
    ''' </summary>
    ''' <param name="kbnGrpCd">String</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnData(ByVal ds As DataSet, ByVal kbnGrpCd As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", kbnGrpCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF010DAC.SQL_SELECT_KBN_DATA, brCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF010DAC.TABLE_NM_KBN)

        Return ds

    End Function
#End Region

#Region "設定処理"

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append("UPDATE $LM_TRN$..F_UNSO_L SET")
        Me._StrSql.Append(vbNewLine)

        '修正項目によるSQL構築
        Call Me.SetUpdateData(Me._SqlPrmList, ds, Me._StrSql)

        '共通項目
        Me._StrSql.Append(LMF010DAC.SQL_UPDATE)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)
        Call Me.SetUnsoPkParameter(Me._SqlPrmList, Me._Row)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function RemovedAction(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()
        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMF010DAC.SQL_UPDATE_CANCEL)

        'START UMANO 要望番号1369 運行紐付け対応
        '運行番号の場合(⇒運送会社コード(1次)の支払タリフで更新)
        If LMF010DAC.SHUSEI_TRIP.Equals(ds.Tables(LMF010DAC.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF010DAC.SQL_UPDATE_CANCEL_STARIFF)
        End If
        'END UMANO 要望番号1369 運行紐付け対応

        '中継解除の場合
        If LMF010DAC.SHUSEI_CHUKEI.Equals(ds.Tables(LMF010DAC.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF010DAC.SQL_UPDATE_CANCEL_TYUKEI)
        End If

        '共通項目
        Me._StrSql.Append(LMF010DAC.SQL_UPDATE)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)
        Call Me.SetUnsoPkParameter(Me._SqlPrmList, Me._Row)

        'START UMANO 要望番号1369 運行紐付け対応
        If LMF010DAC.SHUSEI_TRIP.Equals(ds.Tables(LMF010DAC.TABLE_NM_ITEM).Rows(0).Item("ITEM_DATA").ToString()) = True Then
            Call Me.SetUnsoStariffParameter(Me._SqlPrmList, Me._Row)
        End If
        'END UMANO 要望番号1369 運行紐付け対応

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    ''' <summary>
    ''' 運送(特大)テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(特大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_ITEM)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL文のコンパイル
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF010DAC.SQL_UPDATE_LL, brCd))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me._Row.Item("TRIP_NO").ToString(), DBDataType.CHAR))

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd)

        Return ds

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃テーブル新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃新規登録SQLの構築・発行</remarks>
    Private Function InsertShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_SHIHARAI_UNCHIN)

        'SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF010DAC.SQL_INSERT_SHIHARAI _
        '                                                               , ds.Tables(LMF010DAC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString()))
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF010DAC.SQL_INSERT_SHIHARAI _
                                                               , ds.Tables(LMF010DAC.TABLE_NM_SHIHARAI_UNCHIN).Rows(0).Item("NRS_BR_CD").ToString()))


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

            MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' パラメータ設定モジュール(検索)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetSelectParam(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            'パラメータ設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START KIM 要望番号1485 支払い関連修正。
    ''' <summary>
    ''' (支払)運賃テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃更新SQLの構築・発行</remarks>
    Private Function UpdateShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L)
        Dim unsoLLTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_UNSOCO)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF010DAC.SQL_UPDATE_SHIHARAI _
                                                                     , ds.Tables(LMF010DAC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            'INTableの条件rowの格納
            Me._Row = inTbl.Rows(i)

            'SQLパラメータ初期化
            Me._SqlPrmList.Clear()

            'パラメータ設定
            Call Me.SetSysdataParameter(Me._SqlPrmList)
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", unsoLLTbl.Rows(0).Item("SHIHARAITO_CD").ToString(), DBDataType.NVARCHAR))

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetUpdateResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds

    End Function
    'END KIM 要望番号1485 支払い関連修正。

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' 新黒存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function NewKuroExistChk(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_G_HED_CHK)

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

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMF010DAC.TABLE_NM_G_HED_CHK)

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

        If Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString() = LMF010DAC.SEIQ_TARIFF_BUNRUI_KB_YOKOMOCHI Then
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

        MyBase.Logger.WriteSQLLog(LMF010DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
    ''' Update文の発行
    ''' </summary>
    ''' <param name="cmd">SQLコマンド</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), True)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = False Then
                MyBase.SetMessage("E011")
            Else
                MyBase.SetMessageStore(LMF010DAC.GUIDANCE_KBN, "E011", , Me._Row.Item("ROW_NO").ToString())
            End If
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' キャンセルチェック
    ''' </summary>
    ''' <param name="setFlg">セットフラグ　False:通常のメッセージセット　True:一括更新のメッセージセット</param>
    ''' <param name="cnt">カウント</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function CancelResultChk(ByVal cnt As Integer, Optional ByVal setFlg As Boolean = False) As Boolean

        '判定
        If cnt > 0 Then
            If setFlg = False Then
                MyBase.SetMessage("E02W", {Me._Row.Item("TRIP_NO").ToString()})
            End If
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' 運行データの営業所またぎ対応
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetUnsoLLSchema(ByVal ds As DataSet) As String

        Dim rtnFrom As String = String.Empty
        '区分マスタ(U026)の取得
        ds = Me.SelectKbnData(ds, "U026")

        '件数０件の場合、自営業所のみ
        If ds.Tables(LMF010DAC.TABLE_NM_KBN).Rows.Count = 0 Then
            rtnFrom = "$LM_TRN$..F_UNSO_LL"
            Return rtnFrom
        End If

        '０件ではない場合、またぎ営業所のスキーマを設定
        Dim matagiBr As String = String.Empty
        rtnFrom = "(SELECT * FROM $LM_TRN$..F_UNSO_LL "
        For i As Integer = 0 To ds.Tables(LMF010DAC.TABLE_NM_KBN).Rows.Count - 1
            matagiBr = ds.Tables(LMF010DAC.TABLE_NM_KBN).Rows(i).Item("KBN_NM2").ToString()
            rtnFrom = String.Concat(rtnFrom, " UNION SELECT * FROM ", MyBase.GetDatabaseName(matagiBr, DBKbn.TRN), "..F_UNSO_LL ")
        Next
        rtnFrom = String.Concat(rtnFrom, ")")

        Return rtnFrom

    End Function

#End Region

#Region "パラメータ設定"

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
    ''' PKのパラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoPkParameter(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送会社コードパラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoEditParameter(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    'START UMANO 要望番号1302 運行紐付け対応
    ''' <summary>
    ''' 運行レコードパラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <remarks></remarks>
    Private Sub SetUncoParameter(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))

        End With

    End Sub
    'END UMANO 要望番号1302 運行紐付け対応

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

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetDataInsertParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = Me.SetInsSysdataParameter(prmList)

        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_DATE", sysDateTime(0), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_TIME", sysDateTime(1), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_DEL_FLG", BaseConst.FLG.OFF, DBDataType.CHAR))

        Return sysDateTime

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Function SetInsSysdataParameter(ByVal prmList As ArrayList) As String()

        '更新日時
        Dim sysDateTime As String() = New String() {MyBase.GetSystemDate(), MyBase.GetSystemTime()}

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

        Return sysDateTime

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1369 運行紐付け対応
    ''' <summary>
    ''' 運行レコードパラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">dr</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoStariffParameter(ByVal prmList As ArrayList, ByVal dr As DataRow)

        With dr

            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub
    'END UMANO 要望番号1369 運行紐付け対応

    ''' <summary>
    ''' 登録処理時のSQL構築
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateData(ByVal prmList As ArrayList, ByVal ds As DataSet, ByVal sql As StringBuilder)

        Dim setDr As DataRow = ds.Tables(LMF010DAC.TABLE_NM_ITEM).Rows(0)

        With setDr

            Select Case setDr.Item("ITEM_DATA").ToString()

                Case LMF010DAC.SHUSEI_TRIP

                    sql.Append(" TRIP_NO = @TRIP_NO")
                    sql.Append(vbNewLine)
                    'START UMANO 要望番号1369 支払運賃に伴う修正。
                    sql.Append(",SHIHARAI_TARIFF_CD = @SHIHARAI_TARIFF_CD")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", .Item("TRIP_NO").ToString(), DBDataType.CHAR))
                    'START UMANO 要望番号1369 支払運賃に伴う修正。
                    If ds.Tables("UNSOCO").Rows.Count <> 0 Then
                        prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", ds.Tables("UNSOCO").Rows(0).Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                        prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", ds.Tables("UNSOCO").Rows(0).Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    ElseIf ds.Tables("UNSOCO").Rows.Count = 0 Then
                        prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", String.Empty, DBDataType.NVARCHAR))
                        prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", String.Empty, DBDataType.NVARCHAR))
                    End If
                    'END UMANO 要望番号1369 支払運賃に伴う修正。

                Case LMF010DAC.SHUSEI_BIN


                    sql.Append(" BIN_KB = @BIN_KB")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@BIN_KB", .Item("BIN_KB").ToString(), DBDataType.CHAR))

                Case LMF010DAC.SHUSEI_UNSOCO

                    sql.Append(" UNSO_CD = @UNSO_CD")
                    sql.Append(vbNewLine)
                    sql.Append(",UNSO_BR_CD = @UNSO_BR_CD")
                    sql.Append(vbNewLine)
                    'START UMANO 要望番号1369 支払運賃に伴う修正。
                    sql.Append(",SHIHARAI_TARIFF_CD = @SHIHARAI_TARIFF_CD")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD")
                    sql.Append(vbNewLine)
                    'END UMANO 要望番号1369 支払運賃に伴う修正。

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
                    sql.Append(",AUTO_DENP_KBN = @AUTO_DENP_KBN")
                    sql.Append(vbNewLine)
                    sql.Append(",AUTO_DENP_NO = @AUTO_DENP_NO")
                    sql.Append(vbNewLine)
#End If

                    prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", .Item("UNSO_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", .Item("UNSO_BR_CD").ToString(), DBDataType.CHAR))
                    'START UMANO 要望番号1369 支払運賃に伴う修正。
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", ds.Tables("UNSOCO").Rows(0).Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", ds.Tables("UNSOCO").Rows(0).Item("EXTC_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    'END UMANO 要望番号1369 支払運賃に伴う修正。

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
                    Dim unsoLRow As DataRow = ds.Tables(LMF010DAC.TABLE_NM_UNSO_L).Rows(0)
                    prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_KBN", unsoLRow.Item("AUTO_DENP_KBN").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@AUTO_DENP_NO", unsoLRow.Item("AUTO_DENP_NO").ToString(), DBDataType.NVARCHAR))
#End If
                Case LMF010DAC.SHUSEI_CHUKEI

                    sql.Append(" TYUKEI_HAISO_FLG = '01'")
                    sql.Append(vbNewLine)
                    sql.Append(",SYUKA_TYUKEI_CD = @SYUKA_TYUKEI_CD")
                    sql.Append(vbNewLine)
                    sql.Append(",HAIKA_TYUKEI_CD = @HAIKA_TYUKEI_CD")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_CD", .Item("SYUKA_TYUKEI_CD").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_CD", .Item("HAIKA_TYUKEI_CD").ToString(), DBDataType.CHAR))

            End Select

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

#Region "抽出条件"

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetConditionMasterSQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        '検索条件部に入力された条件とパラメータ設定
        Dim whereStr As String = String.Empty
        With dr

            '営業所
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.SetWhereData(.Item("NRS_BR_CD").ToString(), LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))

            '輸送部営業所
            whereStr = .Item("YUSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.YUSO_BR_CD = @YUSO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@YUSO_BR_CD", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '運送会社(1次)コード
            whereStr = .Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.UNSO_CD LIKE @UNSO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送会社支店(1次)コード
            whereStr = .Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.UNSO_BR_CD LIKE @UNSO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送会社(1次)名
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M38_01.UNSOCO_NM + '　' + M38_01.UNSOCO_BR_NM LIKE @UNSO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '荷主(大)コード
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_L LIKE @CUST_CD_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '荷主(中)コード
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_M LIKE @CUST_CD_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M07_01.CUST_NM_L + '　' + M07_01.CUST_NM_M LIKE @CUST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '日付絞込条件
            Call Me.SetConditionDateSQL(prmList, dr, sql)

            '作成者コード
            whereStr = .Item("SYS_ENT_USER").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.SYS_ENT_USER LIKE @SYS_ENT_USER")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '作成者名
            whereStr = .Item("SYS_ENT_USER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND S01_01.USER_NM LIKE @SYS_ENT_USER_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SYS_ENT_USER_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '運行紐付け
            whereStr = .Item("UNCO_ARI_NASHI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                If LMF010DAC.TYUKEI_HAISO_FLG_ON.Equals(whereStr) = True Then

                    '運行番号のどれかに値がある場合、紐付け済み
                    sql.Append(" AND  ( '' <> F02_01.TRIP_NO OR '' <> F02_01.TRIP_NO_SYUKA OR '' <> F02_01.TRIP_NO_TYUKEI OR '' <> F02_01.TRIP_NO_HAIKA ) ")
                    sql.Append(vbNewLine)

                Else

                    '運行番号の全てに値がない場合、紐付け未
                    sql.Append(" AND F02_01.TRIP_NO = '' ")
                    sql.Append(vbNewLine)
                    sql.Append(" AND F02_01.TRIP_NO_SYUKA = '' ")
                    sql.Append(vbNewLine)
                    sql.Append(" AND F02_01.TRIP_NO_TYUKEI = '' ")
                    sql.Append(vbNewLine)
                    sql.Append(" AND F02_01.TRIP_NO_HAIKA = '' ")
                    sql.Append(vbNewLine)

                End If

            End If

            '中継配送フラグ
            whereStr = .Item("TYUKEI_HAISO_FLG").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.TYUKEI_HAISO_FLG = @TYUKEI_HAISO_FLG")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TYUKEI_HAISO_FLG", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '運送番号
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.UNSO_NO_L LIKE @UNSO_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '便区分
            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.BIN_KB = @BIN_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@BIN_KB", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            'タリフ分類
            whereStr = .Item("TARIFF_BUNRUI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主参照番号
            whereStr = .Item("CUST_REF_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_REF_NO LIKE @CUST_REF_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '発地名
            whereStr = .Item("ORIG_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M10_01.DEST_NM LIKE @ORIG_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@ORIG_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M10_02.DEST_NM LIKE @DEST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '届先住所
            whereStr = .Item("DEST_ADD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M10_02.AD_1 LIKE @DEST_ADD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_ADD", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '届先JIS住所
            whereStr = .Item("DEST_ADD2").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M10_02_JIS.KEN + M10_02_JIS.SHI LIKE @DEST_ADD2")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_ADD2", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            'エリア名
            whereStr = .Item("AREA_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M36_01.AREA_NM LIKE @AREA_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@AREA_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '管理番号
            whereStr = .Item("KANRI_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.INOUTKA_NO_L LIKE @KANRI_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@KANRI_NO_L", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '備考
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.REMARK LIKE @REMARK")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            'まとめ番号
            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.GROUP_NO LIKE @SEIQ_GROUP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '運送温度区分
            whereStr = .Item("UNSO_ONDO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.UNSO_ONDO_KB = @UNSO_ONDO_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_ONDO_KB", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '元データ区分
            whereStr = .Item("MOTO_DATA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.MOTO_DATA_KB = @MOTO_DATA_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@MOTO_DATA_KB", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '引当状況
            whereStr = .Item("ALCTD_STS").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                If whereStr.Equals("00") Then
                    '未引当の場合
                    sql.Append(" AND C01_01.OUTKA_STATE_KB < '50'")
                ElseIf whereStr.Equals("01") Then
                    '引当済の場合
                    sql.Append(" AND C01_01.OUTKA_STATE_KB >= '50'")
                End If
                sql.Append(vbNewLine)
            End If

            '送り状番号
            whereStr = .Item("DENP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.AUTO_DENP_NO LIKE @DENP_NO OR F02_01.DENP_NO LIKE @DENP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DENP_NO", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))
            End If

            '中継配送フラグごとのSQL構築
            Call Me.SetHaishoWhereData(prmList, dr, sql)

        End With

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(日付絞込)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetConditionDateSQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        With dr

            '日付絞込がない場合、スルー
            Dim dateKbn As String = .Item("DATE_KBN").ToString()
            If String.IsNullOrEmpty(dateKbn) = True Then
                Exit Sub
            End If

            Dim fromDate As String = .Item("DATE_FROM").ToString()
            Dim toDate As String = .Item("DATE_TO").ToString()

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(fromDate) = True _
                AndAlso String.IsNullOrEmpty(toDate) = True _
                Then
                Exit Sub
            End If

            Dim fromCondition As String = String.Empty
            Dim toCondition As String = String.Empty

            Select Case dateKbn

                Case LMF010DAC.DATE_KBN_NONYU

                    fromCondition = " AND F02_01.ARR_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.ARR_PLAN_DATE <= @TO_DATE "

                Case LMF010DAC.DATE_KBN_SHUKKA

                    fromCondition = " AND F02_01.OUTKA_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.OUTKA_PLAN_DATE <= @TO_DATE "

                Case LMF010DAC.DATE_KBN_LL

                    fromCondition = " AND ( F01_01.TRIP_DATE >= @FROM_DATE OR F01_04.TRIP_DATE >= @FROM_DATE ) "
                    toCondition = " AND ( F01_01.TRIP_DATE <= @TO_DATE OR F01_04.TRIP_DATE <= @TO_DATE ) "

                Case LMF010DAC.DATE_KBN_ENT

                    fromCondition = " AND F02_01.SYS_ENT_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.SYS_ENT_DATE <= @TO_DATE "

            End Select

            If String.IsNullOrEmpty(fromDate) = False Then
                sql.Append(fromCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@FROM_DATE", Me.SetWhereData(fromDate, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            If String.IsNullOrEmpty(toDate) = False Then
                sql.Append(toCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TO_DATE", Me.SetWhereData(toDate, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' 抽出条件設定
    ''' </summary>
    ''' <param name="whereStr">条件の文字</param>
    ''' <param name="ptn">抽出パターン</param>
    ''' <returns>文字</returns>
    ''' <remarks></remarks>
    Private Function SetWhereData(ByVal whereStr As String, ByVal ptn As LMF010DAC.ConditionPattern) As String

        SetWhereData = String.Empty

        Select Case ptn

            Case LMF010DAC.ConditionPattern.PRE

                SetWhereData = String.Concat(whereStr, "%")

            Case LMF010DAC.ConditionPattern.ALL

                SetWhereData = String.Concat("%", whereStr, "%")

            Case LMF010DAC.ConditionPattern.OTHER

                SetWhereData = whereStr

        End Select

        Return SetWhereData

    End Function

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール(中継配送有 Or 無)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetHaishoWhereData(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        Dim whereStr As String = String.Empty

        With dr

            '運送会社コード
            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    F01_01.UNSOCO_CD LIKE @UNSOCO_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_02.UNSOCO_CD LIKE @UNSOCO_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_03.UNSOCO_CD LIKE @UNSOCO_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_04.UNSOCO_CD LIKE @UNSOCO_CD ) ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社支店コード
            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    F01_01.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_02.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_03.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("       OR F01_04.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社名
            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_02.UNSOCO_NM + '　' + M38_02.UNSOCO_BR_NM LIKE @UNSOCO_NM ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO LIKE @TRIP_NO ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))

            End If

            '乗務員名
            whereStr = .Item("DRIVER_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M37_01.DRIVER_NM LIKE @DRIVER_NM ")
                sql.Append(vbNewLine)
                sql.Append("       OR M37_02.DRIVER_NM LIKE @DRIVER_NM ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@DRIVER_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '車種
            whereStr = .Item("CAR_TP_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M39_01.CAR_TP_KB LIKE @CAR_TP_KB ")
                sql.Append(vbNewLine)
                sql.Append("       OR M39_02.CAR_TP_KB LIKE @CAR_TP_KB ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@CAR_TP_KB", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))

            End If

            '車番
            whereStr = .Item("CAR_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M39_01.CAR_NO LIKE @CAR_NO ")
                sql.Append(vbNewLine)
                sql.Append("       OR M39_02.CAR_NO LIKE @CAR_NO ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@CAR_NO", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '自傭区分
            whereStr = .Item("JSHA_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND (    M39_01.JSHA_KB LIKE @JSHA_KB ")
                sql.Append(vbNewLine)
                sql.Append("       OR M39_02.JSHA_KB LIKE @JSHA_KB ) ")
                sql.Append(vbNewLine)

                prmList.Add(MyBase.GetSqlParameter("@JSHA_KB", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.OTHER), DBDataType.CHAR))

            End If

            '配荷中継地
            whereStr = .Item("HAIKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M12_01.KEN + M12_01.SHI LIKE @HAIKA_TYUKEI_NM ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '集荷中継地
            whereStr = .Item("SYUKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M12_02.KEN + M12_02.SHI LIKE @SYUKA_TYUKEI_NM ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_NM", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運行番号(集荷)
            whereStr = .Item("TRIP_NO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO_SYUKA LIKE @TRIP_NO_SYUKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))

            End If

            '運行番号(中継)
            whereStr = .Item("TRIP_NO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO_TYUKEI LIKE @TRIP_NO_TYUKEI ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))

            End If

            '運行番号(配荷)
            whereStr = .Item("TRIP_NO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      F02_01.TRIP_NO_HAIKA LIKE @TRIP_NO_HAIKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.PRE), DBDataType.CHAR))

            End If

            '運送会社(集荷)
            whereStr = .Item("UNSOCO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_03.UNSOCO_NM + '　' + M38_03.UNSOCO_BR_NM LIKE @UNSOCO_SYUKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_SYUKA", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社(中継)
            whereStr = .Item("UNSOCO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_04.UNSOCO_NM + '　' + M38_04.UNSOCO_BR_NM LIKE @UNSOCO_TYUKEI ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_TYUKEI", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社(配荷)
            whereStr = .Item("UNSOCO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND      M38_05.UNSOCO_NM + '　' + M38_05.UNSOCO_BR_NM LIKE @UNSOCO_HAIKA ")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_HAIKA", Me.SetWhereData(whereStr, LMF010DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

        End With

    End Sub

#End Region

#End Region

End Class
