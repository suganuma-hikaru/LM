' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF040    : 運賃検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF040DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF040DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF040IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF040OUT"

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

    ''' <summary>
    ''' 区分テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_KBN As String = "Z_KBN"

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
    Private Const CLASS_NM As String = "LMF040DAC"

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
    ''' タリフ区分(車扱い)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TARIFF_KURUMA As String = "20"

    ''' <summary>
    ''' 並び順(荷主 , 運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_CUSTTRIP As String = "01"

    ''' <summary>
    ''' 並び順(届先コード)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_DEST As String = "02"

    ''' <summary>
    ''' 並び順(届先JIS)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_DESTJIS As String = "03"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 並び順(日立物流用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ORDER_BY_DIC As String = "04"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    ''' <summary>
    ''' 確定
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIX_ACTION As String = "01"

    ''' <summary>
    ''' 確定解除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIX_CANCELL_ACTION As String = "02"

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GROUP_ACTION As String = "03"

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GROUP_CANCELL_ACTION As String = "04"

    ''' <summary>
    ''' 修正項目(請求先)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_SEIQTO As String = "05"

    ''' <summary>
    ''' 修正項目(タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_TARIFF As String = "06"

    ''' <summary>
    ''' 修正項目(横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_YOKO As String = "07"

    ''' <summary>
    ''' 修正項目(荷主)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_CUST As String = "08"

    'START YANAI 要望番号996
    ''' <summary>
    ''' 修正項目(割増タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_ETARIFF As String = "09"
    'END YANAI 要望番号996

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 修正項目(在庫部課)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_ZBUKACD As String = "10"

    ''' <summary>
    ''' 修正項目(扱い部課)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_ABUKACD As String = "11"
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    'START s.kobayashi 20140519 Notes.2186 距離再計算
    ''' <summary>
    ''' 修正項目(扱い部課)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SHUSEI_KYORI As String = "12"
    'End s.kobayashi 20140519 Notes.2186 距離再計算

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

    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(F04_01.UNSO_NO_L)                     AS REC_CNT       " & vbNewLine

    'START YANAI 要望番号376
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
    '                                        & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
    '                                        & ",F04_01.SEIQ_FIXED_FLAG          AS SEIQ_FIXED_FLAG      " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM8                  AS SEIQ_FIXED_NM        " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
    '                                        & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
    '                                        & ",F04_01.SEIQTO_CD                AS SEIQTO_CD            " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS SEIQTO_NM            " & vbNewLine _
    '                                        & ",F02_01.DEST_CD                  AS DEST_CD              " & vbNewLine _
    '                                        & ",M10_01.DEST_NM                  AS DEST_NM              " & vbNewLine _
    '                                        & ",M10_01.JIS                      AS DEST_JIS_CD          " & vbNewLine _
    '                                        & ",F02_01.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
    '                                        & ",F02_01.UNSO_BR_CD               AS UNSO_BR_CD           " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_CD                AS UNSOCO_CD            " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_BR_CD             AS UNSOCO_BR_CD         " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_NM                AS UNSOCO_NM            " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_BR_NM             AS UNSOCO_BR_NM         " & vbNewLine _
    '                                        & ",M12_01.KEN + M12_01.SHI         AS SYUKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",M12_02.KEN + M12_02.SHI         AS HAIKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_SYUKA            AS TRIP_NO_SYUKA        " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_TYUKEI           AS TRIP_NO_TYUKEI       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_HAIKA            AS TRIP_NO_HAIKA        " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_NM                AS UNSOCO_SYUKA         " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_BR_NM             AS UNSOCO_BR_SYUKA      " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_NM                AS UNSOCO_TYUKEI        " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_BR_NM             AS UNSOCO_BR_TYUKEI     " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_NM                AS UNSOCO_HAIKA         " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_BR_NM             AS UNSOCO_BR_HAIKA      " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_BUNRUI_KB    AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_CD           AS SEIQ_TARIFF_CD       " & vbNewLine _
    '                                        & ",F04_01.SEIQ_ETARIFF_CD          AS SEIQ_ETARIFF_CD      " & vbNewLine _
    '                                        & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
    '                                        & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
    '                                        & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_TOLL                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
    '                                        & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
    '                                        & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO            AS SEIQ_GROUP_NO        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO_M          AS SEIQ_GROUP_NO_M      " & vbNewLine _
    '                                        & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
    '                                        & ",F02_01.TYUKEI_HAISO_FLG         AS TYUKEI_HAISO_FLG     " & vbNewLine _
    '                                        & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
    '                                        & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
    '                                        & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
    '                                        & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
    '                                        & "                THEN '1'                                 " & vbNewLine _
    '                                        & "                ELSE '0'                                 " & vbNewLine _
    '                                        & " END                             AS      GROUP_FLG       " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(F04_01.SEIQ_GROUP_NO) = ''              " & vbNewLine _
    '                                        & "      THEN F04_01.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_01.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_01.DECI_INSU                           " & vbNewLine _
    '                                        & "      ELSE F04_02.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_02.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_02.DECI_INSU                           " & vbNewLine _
    '                                        & " END                             AS CHK_UNCHIN           " & vbNewLine
    'START YANAI 要望番号891
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
    '                                        & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
    '                                        & ",F04_01.SEIQ_FIXED_FLAG          AS SEIQ_FIXED_FLAG      " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM8                  AS SEIQ_FIXED_NM        " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
    '                                        & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
    '                                        & ",F04_01.SEIQTO_CD                AS SEIQTO_CD            " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS SEIQTO_NM            " & vbNewLine _
    '                                        & ",F02_01.DEST_CD                  AS DEST_CD              " & vbNewLine _
    '                                        & ",ISNULL(M10_01.DEST_NM,M10_02.DEST_NM) AS DEST_NM        " & vbNewLine _
    '                                        & ",ISNULL(M10_01.JIS,M10_02.JIS)   AS DEST_JIS_CD          " & vbNewLine _
    '                                        & ",F02_01.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
    '                                        & ",F02_01.UNSO_BR_CD               AS UNSO_BR_CD           " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_CD                AS UNSOCO_CD            " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_BR_CD             AS UNSOCO_BR_CD         " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_NM                AS UNSOCO_NM            " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_BR_NM             AS UNSOCO_BR_NM         " & vbNewLine _
    '                                        & ",M12_01.KEN + M12_01.SHI         AS SYUKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",M12_02.KEN + M12_02.SHI         AS HAIKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_SYUKA            AS TRIP_NO_SYUKA        " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_TYUKEI           AS TRIP_NO_TYUKEI       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_HAIKA            AS TRIP_NO_HAIKA        " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_NM                AS UNSOCO_SYUKA         " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_BR_NM             AS UNSOCO_BR_SYUKA      " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_NM                AS UNSOCO_TYUKEI        " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_BR_NM             AS UNSOCO_BR_TYUKEI     " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_NM                AS UNSOCO_HAIKA         " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_BR_NM             AS UNSOCO_BR_HAIKA      " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_BUNRUI_KB    AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_CD           AS SEIQ_TARIFF_CD       " & vbNewLine _
    '                                        & ",F04_01.SEIQ_ETARIFF_CD          AS SEIQ_ETARIFF_CD      " & vbNewLine _
    '                                        & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
    '                                        & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
    '                                        & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_TOLL                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
    '                                        & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
    '                                        & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO            AS SEIQ_GROUP_NO        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO_M          AS SEIQ_GROUP_NO_M      " & vbNewLine _
    '                                        & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
    '                                        & ",F02_01.TYUKEI_HAISO_FLG         AS TYUKEI_HAISO_FLG     " & vbNewLine _
    '                                        & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
    '                                        & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
    '                                        & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
    '                                        & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
    '                                        & "                THEN '1'                                 " & vbNewLine _
    '                                        & "                ELSE '0'                                 " & vbNewLine _
    '                                        & " END                             AS      GROUP_FLG       " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(F04_01.SEIQ_GROUP_NO) = ''              " & vbNewLine _
    '                                        & "      THEN F04_01.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_01.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_01.DECI_INSU                           " & vbNewLine _
    '                                        & "      ELSE F04_02.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_02.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_02.DECI_INSU                           " & vbNewLine _
    '                                        & " END                             AS CHK_UNCHIN           " & vbNewLine
    'START YANAI 要望番号974
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
    '                                        & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
    '                                        & ",F04_01.SEIQ_FIXED_FLAG          AS SEIQ_FIXED_FLAG      " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM8                  AS SEIQ_FIXED_NM        " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
    '                                        & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
    '                                        & ",F04_01.SEIQTO_CD                AS SEIQTO_CD            " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS SEIQTO_NM            " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN F02_01.ORIG_CD                                " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN F02_01.DEST_CD                                " & vbNewLine _
    '                                        & "      ELSE F02_01.DEST_CD                                " & vbNewLine _
    '                                        & " END                             AS DEST_CD              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.DEST_NM,M10_04.DEST_NM)         " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & " END                             AS DEST_NM              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.JIS,M10_04.JIS)                 " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & " END                             AS DEST_JIS_CD          " & vbNewLine _
    '                                        & ",F02_01.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
    '                                        & ",F02_01.UNSO_BR_CD               AS UNSO_BR_CD           " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_CD                AS UNSOCO_CD            " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_BR_CD             AS UNSOCO_BR_CD         " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_NM                AS UNSOCO_NM            " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_BR_NM             AS UNSOCO_BR_NM         " & vbNewLine _
    '                                        & ",M12_01.KEN + M12_01.SHI         AS SYUKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",M12_02.KEN + M12_02.SHI         AS HAIKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_SYUKA            AS TRIP_NO_SYUKA        " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_TYUKEI           AS TRIP_NO_TYUKEI       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_HAIKA            AS TRIP_NO_HAIKA        " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_NM                AS UNSOCO_SYUKA         " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_BR_NM             AS UNSOCO_BR_SYUKA      " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_NM                AS UNSOCO_TYUKEI        " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_BR_NM             AS UNSOCO_BR_TYUKEI     " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_NM                AS UNSOCO_HAIKA         " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_BR_NM             AS UNSOCO_BR_HAIKA      " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_BUNRUI_KB    AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_CD           AS SEIQ_TARIFF_CD       " & vbNewLine _
    '                                        & ",F04_01.SEIQ_ETARIFF_CD          AS SEIQ_ETARIFF_CD      " & vbNewLine _
    '                                        & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
    '                                        & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
    '                                        & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_TOLL                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
    '                                        & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
    '                                        & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO            AS SEIQ_GROUP_NO        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO_M          AS SEIQ_GROUP_NO_M      " & vbNewLine _
    '                                        & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
    '                                        & ",F02_01.TYUKEI_HAISO_FLG         AS TYUKEI_HAISO_FLG     " & vbNewLine _
    '                                        & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
    '                                        & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
    '                                        & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
    '                                        & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
    '                                        & "                THEN '1'                                 " & vbNewLine _
    '                                        & "                ELSE '0'                                 " & vbNewLine _
    '                                        & " END                             AS      GROUP_FLG       " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(F04_01.SEIQ_GROUP_NO) = ''              " & vbNewLine _
    '                                        & "      THEN F04_01.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_01.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_01.DECI_INSU                           " & vbNewLine _
    '                                        & "      ELSE F04_02.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_02.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_02.DECI_INSU                           " & vbNewLine _
    '                                        & " END                             AS CHK_UNCHIN           " & vbNewLine
    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
    '                                        & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
    '                                        & ",F04_01.SEIQ_FIXED_FLAG          AS SEIQ_FIXED_FLAG      " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM8                  AS SEIQ_FIXED_NM        " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
    '                                        & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
    '                                        & ",F04_01.SEIQTO_CD                AS SEIQTO_CD            " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS SEIQTO_NM            " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN F02_01.ORIG_CD                                " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN F02_01.DEST_CD                                " & vbNewLine _
    '                                        & "      ELSE F02_01.DEST_CD                                " & vbNewLine _
    '                                        & " END                             AS DEST_CD              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.DEST_NM,M10_04.DEST_NM)         " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & " END                             AS DEST_NM              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.JIS,M10_04.JIS)                 " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & " END                             AS DEST_JIS_CD          " & vbNewLine _
    '                                        & ",F02_01.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
    '                                        & ",F02_01.UNSO_BR_CD               AS UNSO_BR_CD           " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_CD                AS UNSOCO_CD            " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_BR_CD             AS UNSOCO_BR_CD         " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_NM                AS UNSOCO_NM            " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_BR_NM             AS UNSOCO_BR_NM         " & vbNewLine _
    '                                        & ",M12_01.KEN + M12_01.SHI         AS SYUKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",M12_02.KEN + M12_02.SHI         AS HAIKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_SYUKA            AS TRIP_NO_SYUKA        " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_TYUKEI           AS TRIP_NO_TYUKEI       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_HAIKA            AS TRIP_NO_HAIKA        " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_NM                AS UNSOCO_SYUKA         " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_BR_NM             AS UNSOCO_BR_SYUKA      " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_NM                AS UNSOCO_TYUKEI        " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_BR_NM             AS UNSOCO_BR_TYUKEI     " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_NM                AS UNSOCO_HAIKA         " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_BR_NM             AS UNSOCO_BR_HAIKA      " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_BUNRUI_KB    AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_CD           AS SEIQ_TARIFF_CD       " & vbNewLine _
    '                                        & ",F04_01.SEIQ_ETARIFF_CD          AS SEIQ_ETARIFF_CD      " & vbNewLine _
    '                                        & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
    '                                        & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
    '                                        & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_TOLL                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
    '                                        & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
    '                                        & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO            AS SEIQ_GROUP_NO        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO_M          AS SEIQ_GROUP_NO_M      " & vbNewLine _
    '                                        & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
    '                                        & ",F02_01.TYUKEI_HAISO_FLG         AS TYUKEI_HAISO_FLG     " & vbNewLine _
    '                                        & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
    '                                        & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
    '                                        & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
    '                                        & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
    '                                        & "                THEN '1'                                 " & vbNewLine _
    '                                        & "                ELSE '0'                                 " & vbNewLine _
    '                                        & " END                             AS      GROUP_FLG       " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(F04_01.SEIQ_GROUP_NO) = ''              " & vbNewLine _
    '                                        & "      THEN F04_01.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_01.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_01.DECI_INSU                           " & vbNewLine _
    '                                        & "      ELSE F04_02.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_02.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_02.DECI_INSU                           " & vbNewLine _
    '                                        & " END                             AS CHK_UNCHIN           " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_UNCHIN,0)    AS SEIQ_UNCHIN          " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_CITY_EXTC,0) AS SEIQ_CITY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_WINT_EXTC,0) AS SEIQ_WINT_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_RELY_EXTC,0) AS SEIQ_RELY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_TOLL,0)      AS SEIQ_TOLL            " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_INSU,0)      AS SEIQ_INSU            " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_UNCHIN,0)    AS DECI_UNCHIN          " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_CITY_EXTC,0) AS DECI_CITY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_WINT_EXTC,0) AS DECI_WINT_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_RELY_EXTC,0) AS DECI_RELY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_TOLL,0)      AS DECI_TOLL            " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_INSU,0)      AS DECI_INSU            " & vbNewLine
    'START YANAI 要望番号1199 運賃一覧画面の届先名称・住所取得元を変更
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
    '                                        & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
    '                                        & ",F04_01.SEIQ_FIXED_FLAG          AS SEIQ_FIXED_FLAG      " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM8                  AS SEIQ_FIXED_NM        " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
    '                                        & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
    '                                        & ",F04_01.SEIQTO_CD                AS SEIQTO_CD            " & vbNewLine _
    '                                        & ",M06_01.SEIQTO_NM                AS SEIQTO_NM            " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN F02_01.ORIG_CD                                " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN F02_01.DEST_CD                                " & vbNewLine _
    '                                        & "      ELSE F02_01.DEST_CD                                " & vbNewLine _
    '                                        & " END                             AS DEST_CD              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.DEST_NM,M10_04.DEST_NM)         " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & " END                             AS DEST_NM              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.JIS,M10_04.JIS)                 " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & " END                             AS DEST_JIS_CD          " & vbNewLine _
    '                                        & ",F02_01.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
    '                                        & ",F02_01.UNSO_BR_CD               AS UNSO_BR_CD           " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_CD                AS UNSOCO_CD            " & vbNewLine _
    '                                        & ",F01_01.UNSOCO_BR_CD             AS UNSOCO_BR_CD         " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_NM                AS UNSOCO_NM            " & vbNewLine _
    '                                        & ",M38_02.UNSOCO_BR_NM             AS UNSOCO_BR_NM         " & vbNewLine _
    '                                        & ",M12_01.KEN + M12_01.SHI         AS SYUKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",M12_02.KEN + M12_02.SHI         AS HAIKA_TYUKEI_NM      " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_SYUKA            AS TRIP_NO_SYUKA        " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_TYUKEI           AS TRIP_NO_TYUKEI       " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO_HAIKA            AS TRIP_NO_HAIKA        " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_NM                AS UNSOCO_SYUKA         " & vbNewLine _
    '                                        & ",M38_03.UNSOCO_BR_NM             AS UNSOCO_BR_SYUKA      " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_NM                AS UNSOCO_TYUKEI        " & vbNewLine _
    '                                        & ",M38_04.UNSOCO_BR_NM             AS UNSOCO_BR_TYUKEI     " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_NM                AS UNSOCO_HAIKA         " & vbNewLine _
    '                                        & ",M38_05.UNSOCO_BR_NM             AS UNSOCO_BR_HAIKA      " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_BUNRUI_KB    AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                        & ",F04_01.SEIQ_TARIFF_CD           AS SEIQ_TARIFF_CD       " & vbNewLine _
    '                                        & ",F04_01.SEIQ_ETARIFF_CD          AS SEIQ_ETARIFF_CD      " & vbNewLine _
    '                                        & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
    '                                        & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
    '                                        & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
    '                                        & " + F04_01.DECI_TOLL                                      " & vbNewLine _
    '                                        & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
    '                                        & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
    '                                        & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO            AS SEIQ_GROUP_NO        " & vbNewLine _
    '                                        & ",F04_01.SEIQ_GROUP_NO_M          AS SEIQ_GROUP_NO_M      " & vbNewLine _
    '                                        & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
    '                                        & ",F02_01.TYUKEI_HAISO_FLG         AS TYUKEI_HAISO_FLG     " & vbNewLine _
    '                                        & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
    '                                        & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
    '                                        & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
    '                                        & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
    '                                        & "                THEN '1'                                 " & vbNewLine _
    '                                        & "                ELSE '0'                                 " & vbNewLine _
    '                                        & " END                             AS      GROUP_FLG       " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(F04_01.SEIQ_GROUP_NO) = ''              " & vbNewLine _
    '                                        & "      THEN F04_01.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_01.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_01.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_01.DECI_INSU                           " & vbNewLine _
    '                                        & "      ELSE F04_02.DECI_UNCHIN                            " & vbNewLine _
    '                                        & "            + F04_02.DECI_CITY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_WINT_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_RELY_EXTC                      " & vbNewLine _
    '                                        & "            + F04_02.DECI_TOLL                           " & vbNewLine _
    '                                        & "            + F04_02.DECI_INSU                           " & vbNewLine _
    '                                        & " END                             AS CHK_UNCHIN           " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_UNCHIN,0)    AS SEIQ_UNCHIN          " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_CITY_EXTC,0) AS SEIQ_CITY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_WINT_EXTC,0) AS SEIQ_WINT_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_RELY_EXTC,0) AS SEIQ_RELY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_TOLL,0)      AS SEIQ_TOLL            " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SEIQ_INSU,0)      AS SEIQ_INSU            " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_UNCHIN,0)    AS DECI_UNCHIN          " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_CITY_EXTC,0) AS DECI_CITY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_WINT_EXTC,0) AS DECI_WINT_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_RELY_EXTC,0) AS DECI_RELY_EXTC       " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_TOLL,0)      AS DECI_TOLL            " & vbNewLine _
    '                                        & ",ISNULL(F04_01.DECI_INSU,0)      AS DECI_INSU            " & vbNewLine _
    '                                        & ",F02_01.CUST_REF_NO              AS CUST_REF_NO          " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN CASE WHEN M10_03.CUST_DEST_CD = ''            " & vbNewLine _
    '                                        & "                THEN F02_01.ORIG_CD                      " & vbNewLine _
    '                                        & "                ELSE M10_03.CUST_DEST_CD                 " & vbNewLine _
    '                                        & "                END                                      " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN CASE WHEN M10_01.CUST_DEST_CD = ''            " & vbNewLine _
    '                                        & "                THEN F02_01.DEST_CD                      " & vbNewLine _
    '                                        & "                ELSE M10_01.CUST_DEST_CD                 " & vbNewLine _
    '                                        & "                END                                      " & vbNewLine _
    '                                        & "      ELSE CASE WHEN M10_01.CUST_DEST_CD = ''            " & vbNewLine _
    '                                        & "                THEN F02_01.DEST_CD                      " & vbNewLine _
    '                                        & "                ELSE M10_01.CUST_DEST_CD                 " & vbNewLine _
    '                                        & "                END                                      " & vbNewLine _
    '                                        & " END                             AS MINASHI_DEST_CD      " & vbNewLine _
    '                                        & ",F02_01.BIN_KB                   AS BIN_KB               " & vbNewLine _
    '                                        & ",KBN_05.KBN_NM1                  AS BIN_NM               " & vbNewLine _
    '                                        & ",UNSOM.ZBUKA_CD                  AS ZBUKA_CD             " & vbNewLine _
    '                                        & ",UNSOM.ABUKA_CD                  AS ABUKA_CD             " & vbNewLine _
    '                                        & ",F04_01.DECI_NG_NB               AS DECI_NG_NB           " & vbNewLine _
    '                                        & ",F04_01.SEIQ_PKG_UT              AS SEIQ_PKG_UT          " & vbNewLine _
    '                                        & ",F04_01.SEIQ_SYARYO_KB           AS SEIQ_SYARYO_KB       " & vbNewLine _
    '                                        & ",F04_01.SEIQ_DANGER_KB           AS SEIQ_DANGER_KB       " & vbNewLine
    'Start s.kobayashi 要望番号2143 ABUKA_CD空白時の対応
    Private Const SQL_SELECT_DATA As String = "SET ARITHABORT ON                 " & vbNewLine _
                                            & "SET ARITHIGNORE ON                " & vbNewLine _
                                            & "SELECT                                                   " & vbNewLine _
                                            & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
                                            & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
                                            & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
                                            & ",F04_01.SEIQ_FIXED_FLAG          AS SEIQ_FIXED_FLAG      " & vbNewLine _
                                            & ",KBN_01.KBN_NM8                  AS SEIQ_FIXED_NM        " & vbNewLine _
                                            & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
                                            & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
                                            & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
                                            & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
                                            & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
                                            & ",F04_01.SEIQTO_CD                AS SEIQTO_CD            " & vbNewLine _
                                            & ",M06_01.SEIQTO_NM                AS SEIQTO_NM            " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
                                            & "      THEN F02_01.ORIG_CD                                " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                            & "      THEN F02_01.DEST_CD                                " & vbNewLine _
                                            & "      ELSE F02_01.DEST_CD                                " & vbNewLine _
                                            & " END                             AS DEST_CD              " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
                                            & "      THEN ISNULL(M10_03.DEST_NM,M10_04.DEST_NM)         " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20' AND OUTKAL.DEST_KB = '02' " & vbNewLine _
                                            & "      THEN OUTKAEDIL.DEST_NM                             " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                            & "      THEN ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
                                            & "      ELSE ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
                                            & " END                             AS DEST_NM              " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
                                            & "      THEN ISNULL(M10_03.JIS,M10_04.JIS)                 " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20' AND OUTKAL.DEST_KB = '02' " & vbNewLine _
                                            & "      THEN OUTKAEDIL.DEST_JIS_CD                         " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                            & "      THEN ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
                                            & "      ELSE ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
                                            & " END                             AS DEST_JIS_CD          " & vbNewLine _
                                            & ",F02_01.UNSO_CD                  AS UNSO_CD              " & vbNewLine _
                                            & ",F02_01.UNSO_BR_CD               AS UNSO_BR_CD           " & vbNewLine _
                                            & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
                                            & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
                                            & ",F01_01.UNSOCO_CD                AS UNSOCO_CD            " & vbNewLine _
                                            & ",F01_01.UNSOCO_BR_CD             AS UNSOCO_BR_CD         " & vbNewLine _
                                            & ",M38_02.UNSOCO_NM                AS UNSOCO_NM            " & vbNewLine _
                                            & ",M38_02.UNSOCO_BR_NM             AS UNSOCO_BR_NM         " & vbNewLine _
                                            & ",M12_01.KEN + M12_01.SHI         AS SYUKA_TYUKEI_NM      " & vbNewLine _
                                            & ",M12_02.KEN + M12_02.SHI         AS HAIKA_TYUKEI_NM      " & vbNewLine _
                                            & ",F02_01.TRIP_NO_SYUKA            AS TRIP_NO_SYUKA        " & vbNewLine _
                                            & ",F02_01.TRIP_NO_TYUKEI           AS TRIP_NO_TYUKEI       " & vbNewLine _
                                            & ",F02_01.TRIP_NO_HAIKA            AS TRIP_NO_HAIKA        " & vbNewLine _
                                            & ",M38_03.UNSOCO_NM                AS UNSOCO_SYUKA         " & vbNewLine _
                                            & ",M38_03.UNSOCO_BR_NM             AS UNSOCO_BR_SYUKA      " & vbNewLine _
                                            & ",M38_04.UNSOCO_NM                AS UNSOCO_TYUKEI        " & vbNewLine _
                                            & ",M38_04.UNSOCO_BR_NM             AS UNSOCO_BR_TYUKEI     " & vbNewLine _
                                            & ",M38_05.UNSOCO_NM                AS UNSOCO_HAIKA         " & vbNewLine _
                                            & ",M38_05.UNSOCO_BR_NM             AS UNSOCO_BR_HAIKA      " & vbNewLine _
                                            & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
                                            & ",F04_01.SEIQ_TARIFF_BUNRUI_KB    AS SEIQ_TARIFF_BUNRUI_KB" & vbNewLine _
                                            & ",F04_01.SEIQ_TARIFF_CD           AS SEIQ_TARIFF_CD       " & vbNewLine _
                                            & ",F04_01.SEIQ_ETARIFF_CD          AS SEIQ_ETARIFF_CD      " & vbNewLine _
                                            & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
                                            & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
                                            & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
                                            & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
                                            & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
                                            & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
                                            & " + F04_01.DECI_TOLL                                      " & vbNewLine _
                                            & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
                                            & ",CASE WHEN M07_01.ITEM_CURR_CD = '' THEN 'JPY' ELSE M07_01.ITEM_CURR_CD END AS ITEM_CURR_CD         " & vbNewLine _
                                            & ",M_CURR.ROUND_POS                AS ROUND_POS            " & vbNewLine _
                                            & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
                                            & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
                                            & ",F04_01.SEIQ_GROUP_NO            AS SEIQ_GROUP_NO        " & vbNewLine _
                                            & ",F04_01.SEIQ_GROUP_NO_M          AS SEIQ_GROUP_NO_M      " & vbNewLine _
                                            & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
                                            & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
                                            & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
                                            & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
                                            & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
                                            & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
                                            & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
                                            & ",F02_01.TYUKEI_HAISO_FLG         AS TYUKEI_HAISO_FLG     " & vbNewLine _
                                            & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
                                            & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
                                            & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
                                            & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
                                            & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
                                            & "                THEN '1'                                 " & vbNewLine _
                                            & "                ELSE '0'                                 " & vbNewLine _
                                            & " END                             AS      GROUP_FLG       " & vbNewLine _
                                            & ",CASE WHEN RTRIM(F04_01.SEIQ_GROUP_NO) = ''              " & vbNewLine _
                                            & "      THEN F04_01.DECI_UNCHIN                            " & vbNewLine _
                                            & "            + F04_01.DECI_CITY_EXTC                      " & vbNewLine _
                                            & "            + F04_01.DECI_WINT_EXTC                      " & vbNewLine _
                                            & "            + F04_01.DECI_RELY_EXTC                      " & vbNewLine _
                                            & "            + F04_01.DECI_TOLL                           " & vbNewLine _
                                            & "            + F04_01.DECI_INSU                           " & vbNewLine _
                                            & "      ELSE F04_02.DECI_UNCHIN                            " & vbNewLine _
                                            & "            + F04_02.DECI_CITY_EXTC                      " & vbNewLine _
                                            & "            + F04_02.DECI_WINT_EXTC                      " & vbNewLine _
                                            & "            + F04_02.DECI_RELY_EXTC                      " & vbNewLine _
                                            & "            + F04_02.DECI_TOLL                           " & vbNewLine _
                                            & "            + F04_02.DECI_INSU                           " & vbNewLine _
                                            & " END                             AS CHK_UNCHIN           " & vbNewLine _
                                            & ",ISNULL(F04_01.SEIQ_UNCHIN,0)    AS SEIQ_UNCHIN          " & vbNewLine _
                                            & ",ISNULL(F04_01.SEIQ_CITY_EXTC,0) AS SEIQ_CITY_EXTC       " & vbNewLine _
                                            & ",ISNULL(F04_01.SEIQ_WINT_EXTC,0) AS SEIQ_WINT_EXTC       " & vbNewLine _
                                            & ",ISNULL(F04_01.SEIQ_RELY_EXTC,0) AS SEIQ_RELY_EXTC       " & vbNewLine _
                                            & ",ISNULL(F04_01.SEIQ_TOLL,0)      AS SEIQ_TOLL            " & vbNewLine _
                                            & ",ISNULL(F04_01.SEIQ_INSU,0)      AS SEIQ_INSU            " & vbNewLine _
                                            & ",ISNULL(F04_01.DECI_UNCHIN,0)    AS DECI_UNCHIN          " & vbNewLine _
                                            & ",ISNULL(F04_01.DECI_CITY_EXTC,0) AS DECI_CITY_EXTC       " & vbNewLine _
                                            & ",ISNULL(F04_01.DECI_WINT_EXTC,0) AS DECI_WINT_EXTC       " & vbNewLine _
                                            & ",ISNULL(F04_01.DECI_RELY_EXTC,0) AS DECI_RELY_EXTC       " & vbNewLine _
                                            & ",ISNULL(F04_01.DECI_TOLL,0)      AS DECI_TOLL            " & vbNewLine _
                                            & ",ISNULL(F04_01.DECI_INSU,0)      AS DECI_INSU            " & vbNewLine _
                                            & ",F02_01.CUST_REF_NO              AS CUST_REF_NO          " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
                                            & "      THEN CASE WHEN M10_03.CUST_DEST_CD = ''            " & vbNewLine _
                                            & "                THEN F02_01.ORIG_CD                      " & vbNewLine _
                                            & "                ELSE M10_03.CUST_DEST_CD                 " & vbNewLine _
                                            & "                END                                      " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                            & "      THEN CASE WHEN M10_01.CUST_DEST_CD = ''            " & vbNewLine _
                                            & "                THEN F02_01.DEST_CD                      " & vbNewLine _
                                            & "                ELSE M10_01.CUST_DEST_CD                 " & vbNewLine _
                                            & "                END                                      " & vbNewLine _
                                            & "      ELSE CASE WHEN M10_01.CUST_DEST_CD = ''            " & vbNewLine _
                                            & "                THEN F02_01.DEST_CD                      " & vbNewLine _
                                            & "                ELSE M10_01.CUST_DEST_CD                 " & vbNewLine _
                                            & "                END                                      " & vbNewLine _
                                            & " END                             AS MINASHI_DEST_CD      " & vbNewLine _
                                            & ",F02_01.BIN_KB                   AS BIN_KB               " & vbNewLine _
                                            & ",KBN_05.KBN_NM1                  AS BIN_NM               " & vbNewLine _
                                            & ",UNSOM.ZBUKA_CD                  AS ZBUKA_CD             " & vbNewLine _
                                            & "--Start要望番号2143 ABUKA_CD空白時の対応                " & vbNewLine _
                                            & ",CASE WHEN F04_01.NRS_BR_CD = '10' THEN UNSOM.ABUKA_CD   " & vbNewLine _
                                            & "      ELSE CASE WHEN UNSOM.ABUKA_CD = '' OR UNSOM.ABUKA_CD IS NULL THEN 'ZZZZZZZ'       " & vbNewLine _
                                            & "           ELSE UNSOM.ABUKA_CD END                       " & vbNewLine _
                                            & "      END AS ABUKA_CD                                    " & vbNewLine _
                                            & "--,UNSOM.ABUKA_CD                  AS ABUKA_CD             " & vbNewLine _
                                            & "--End要望番号2143 ABUKA_CD空白時の対応             " & vbNewLine _
                                            & ",F04_01.DECI_NG_NB               AS DECI_NG_NB           " & vbNewLine _
                                            & ",F04_01.SEIQ_PKG_UT              AS SEIQ_PKG_UT          " & vbNewLine _
                                            & ",F04_01.SEIQ_SYARYO_KB           AS SEIQ_SYARYO_KB       " & vbNewLine _
                                            & ",F04_01.SEIQ_DANGER_KB           AS SEIQ_DANGER_KB       " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
                                            & "      THEN ISNULL(M10_03.AD_1,'')                        " & vbNewLine _
                                            & "         + ISNULL(M10_03.AD_2,'')                        " & vbNewLine _
                                            & "         + ISNULL(M10_03.AD_3,'')                        " & vbNewLine _
                                            & "      ELSE ISNULL(M10_01.AD_1,'')                        " & vbNewLine _
                                            & "         + ISNULL(M10_01.AD_2,'')                        " & vbNewLine _
                                            & "         + ISNULL(M10_01.AD_3,'')                        " & vbNewLine _
                                            & " END                             AS DEST_ADDR            " & vbNewLine

    'END s.kobayashi 要望番号2143 ABUKA_CD空白時の対応
    'END YANAI 要望番号1199 運賃一覧画面の届先名称・住所取得元を変更
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応
    'END YANAI 要望番号974
    'END YANAI 要望番号891
    'END YANAI 要望番号376

#End Region

#Region "From句"

    'START YANAI 要望番号376
    'Private Const SQL_FROM As String = " FROM      $LM_TRN$..F_UNCHIN_TRS    F04_01                " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
    '                                 & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_01                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_02                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_SYUKA         = F01_02.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_03                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_TYUKEI        = F01_03.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_04                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_HAIKA         = F01_04.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS    F04_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_GROUP_NO         = F04_02.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQ_GROUP_NO_M       = F04_02.UNSO_NO_M      " & vbNewLine _
    '                                 & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_CUST          M07_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M07_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M07_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_M             = M07_01.CUST_CD_M      " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_S             = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_SS            = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                " & vbNewLine _
    '                                 & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQTO_CD             = M06_01.SEIQTO_CD      " & vbNewLine _
    '                                 & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M38_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_CD               = M38_01.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_BR_CD            = M38_01.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_02                " & vbNewLine _
    '                                 & "   ON F01_01.NRS_BR_CD             = M38_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_CD             = M38_02.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_BR_CD          = M38_02.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_03                " & vbNewLine _
    '                                 & "   ON F01_02.NRS_BR_CD             = M38_03.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_CD             = M38_03.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_BR_CD          = M38_03.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_04                " & vbNewLine _
    '                                 & "   ON F01_03.NRS_BR_CD             = M38_04.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_CD             = M38_04.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_BR_CD          = M38_04.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_05                " & vbNewLine _
    '                                 & "   ON F01_04.NRS_BR_CD             = M38_05.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_CD             = M38_05.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_BR_CD          = M38_05.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_05.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
    '                                 & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
    '                                 & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_FIXED_FLAG       = KBN_01.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
    '                                 & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_TARIFF_BUNRUI_KB = KBN_02.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_02.KBN_GROUP_CD          = 'T015'                " & vbNewLine _
    '                                 & "  AND KBN_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                " & vbNewLine _
    '                                 & "   ON F04_01.TAX_KB                = KBN_03.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_03.KBN_GROUP_CD          = 'Z001'                " & vbNewLine _
    '                                 & "  AND KBN_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                " & vbNewLine _
    '                                 & "   ON F02_01.MOTO_DATA_KB          = KBN_04.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_04.KBN_GROUP_CD          = 'M004'                " & vbNewLine _
    '                                 & "  AND KBN_04.SYS_DEL_FLG           = '0'                   " & vbNewLine
    'START YANAI 要望番号891
    'Private Const SQL_FROM As String = " FROM      $LM_TRN$..F_UNCHIN_TRS    F04_01                " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
    '                                 & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_01                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_02                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_SYUKA         = F01_02.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_03                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_TYUKEI        = F01_03.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_04                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_HAIKA         = F01_04.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS    F04_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_GROUP_NO         = F04_02.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQ_GROUP_NO_M       = F04_02.UNSO_NO_M      " & vbNewLine _
    '                                 & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_CUST          M07_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M07_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M07_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_M             = M07_01.CUST_CD_M      " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_S             = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_SS            = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                " & vbNewLine _
    '                                 & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQTO_CD             = M06_01.SEIQTO_CD      " & vbNewLine _
    '                                 & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_02                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND 'ZZZZZ'                      = M10_02.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_02.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M38_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_CD               = M38_01.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_BR_CD            = M38_01.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_02                " & vbNewLine _
    '                                 & "   ON F01_01.NRS_BR_CD             = M38_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_CD             = M38_02.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_BR_CD          = M38_02.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_03                " & vbNewLine _
    '                                 & "   ON F01_02.NRS_BR_CD             = M38_03.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_CD             = M38_03.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_BR_CD          = M38_03.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_04                " & vbNewLine _
    '                                 & "   ON F01_03.NRS_BR_CD             = M38_04.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_CD             = M38_04.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_BR_CD          = M38_04.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_05                " & vbNewLine _
    '                                 & "   ON F01_04.NRS_BR_CD             = M38_05.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_CD             = M38_05.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_BR_CD          = M38_05.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_05.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
    '                                 & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
    '                                 & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_FIXED_FLAG       = KBN_01.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
    '                                 & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_TARIFF_BUNRUI_KB = KBN_02.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_02.KBN_GROUP_CD          = 'T015'                " & vbNewLine _
    '                                 & "  AND KBN_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                " & vbNewLine _
    '                                 & "   ON F04_01.TAX_KB                = KBN_03.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_03.KBN_GROUP_CD          = 'Z001'                " & vbNewLine _
    '                                 & "  AND KBN_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                " & vbNewLine _
    '                                 & "   ON F02_01.MOTO_DATA_KB          = KBN_04.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_04.KBN_GROUP_CD          = 'M004'                " & vbNewLine _
    '                                 & "  AND KBN_04.SYS_DEL_FLG           = '0'                   " & vbNewLine
    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    'Private Const SQL_FROM As String = " FROM      $LM_TRN$..F_UNCHIN_TRS    F04_01                " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
    '                                 & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_01                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_02                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_SYUKA         = F01_02.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_03                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_TYUKEI        = F01_03.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_04                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_HAIKA         = F01_04.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS    F04_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_GROUP_NO         = F04_02.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQ_GROUP_NO_M       = F04_02.UNSO_NO_M      " & vbNewLine _
    '                                 & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_CUST          M07_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M07_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M07_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_M             = M07_01.CUST_CD_M      " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_S             = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_SS            = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                " & vbNewLine _
    '                                 & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQTO_CD             = M06_01.SEIQTO_CD      " & vbNewLine _
    '                                 & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_02                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND 'ZZZZZ'                      = M10_02.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_02.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_03                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_03.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M10_03.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.ORIG_CD               = M10_03.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_04                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_04.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND 'ZZZZZ'                      = M10_04.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.ORIG_CD               = M10_04.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M38_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_CD               = M38_01.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_BR_CD            = M38_01.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_02                " & vbNewLine _
    '                                 & "   ON F01_01.NRS_BR_CD             = M38_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_CD             = M38_02.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_BR_CD          = M38_02.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_03                " & vbNewLine _
    '                                 & "   ON F01_02.NRS_BR_CD             = M38_03.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_CD             = M38_03.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_BR_CD          = M38_03.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_04                " & vbNewLine _
    '                                 & "   ON F01_03.NRS_BR_CD             = M38_04.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_CD             = M38_04.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_BR_CD          = M38_04.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_05                " & vbNewLine _
    '                                 & "   ON F01_04.NRS_BR_CD             = M38_05.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_CD             = M38_05.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_BR_CD          = M38_05.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_05.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
    '                                 & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
    '                                 & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_FIXED_FLAG       = KBN_01.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
    '                                 & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_TARIFF_BUNRUI_KB = KBN_02.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_02.KBN_GROUP_CD          = 'T015'                " & vbNewLine _
    '                                 & "  AND KBN_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                " & vbNewLine _
    '                                 & "   ON F04_01.TAX_KB                = KBN_03.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_03.KBN_GROUP_CD          = 'Z001'                " & vbNewLine _
    '                                 & "  AND KBN_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                " & vbNewLine _
    '                                 & "   ON F02_01.MOTO_DATA_KB          = KBN_04.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_04.KBN_GROUP_CD          = 'M004'                " & vbNewLine _
    '                                 & "  AND KBN_04.SYS_DEL_FLG           = '0'                   " & vbNewLine
    'START YANAI 要望番号1199 運賃一覧画面の届先名称・住所取得元を変更
    'Private Const SQL_FROM As String = " FROM      $LM_TRN$..F_UNCHIN_TRS    F04_01                " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
    '                                 & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_01                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_02                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_SYUKA         = F01_02.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_03                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_TYUKEI        = F01_03.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_04                " & vbNewLine _
    '                                 & "   ON F02_01.TRIP_NO_HAIKA         = F01_04.TRIP_NO        " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS    F04_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_GROUP_NO         = F04_02.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQ_GROUP_NO_M       = F04_02.UNSO_NO_M      " & vbNewLine _
    '                                 & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_TRN$..F_UNSO_M        UNSOM                 " & vbNewLine _
    '                                 & "   ON UNSOM.UNSO_NO_L              = F04_01.UNSO_NO_L      " & vbNewLine _
    '                                 & "  AND UNSOM.UNSO_NO_M              = F04_01.UNSO_NO_M      " & vbNewLine _
    '                                 & "  AND UNSOM.SYS_DEL_FLG            = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_CUST          M07_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M07_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M07_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_M             = M07_01.CUST_CD_M      " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_S             = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.CUST_CD_SS            = '00'                  " & vbNewLine _
    '                                 & "  AND M07_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                " & vbNewLine _
    '                                 & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F04_01.SEIQTO_CD             = M06_01.SEIQTO_CD      " & vbNewLine _
    '                                 & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_02                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND 'ZZZZZ'                      = M10_02.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.DEST_CD               = M10_02.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_03                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_03.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.CUST_CD_L             = M10_03.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.ORIG_CD               = M10_03.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_04                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M10_04.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND 'ZZZZZ'                      = M10_04.CUST_CD_L      " & vbNewLine _
    '                                 & "  AND F02_01.ORIG_CD               = M10_04.DEST_CD        " & vbNewLine _
    '                                 & "  AND M10_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_01                " & vbNewLine _
    '                                 & "   ON F02_01.NRS_BR_CD             = M38_01.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_CD               = M38_01.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F02_01.UNSO_BR_CD            = M38_01.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_02                " & vbNewLine _
    '                                 & "   ON F01_01.NRS_BR_CD             = M38_02.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_CD             = M38_02.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_01.UNSOCO_BR_CD          = M38_02.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_03                " & vbNewLine _
    '                                 & "   ON F01_02.NRS_BR_CD             = M38_03.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_CD             = M38_03.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_02.UNSOCO_BR_CD          = M38_03.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_04                " & vbNewLine _
    '                                 & "   ON F01_03.NRS_BR_CD             = M38_04.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_CD             = M38_04.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_03.UNSOCO_BR_CD          = M38_04.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_05                " & vbNewLine _
    '                                 & "   ON F01_04.NRS_BR_CD             = M38_05.NRS_BR_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_CD             = M38_05.UNSOCO_CD      " & vbNewLine _
    '                                 & "  AND F01_04.UNSOCO_BR_CD          = M38_05.UNSOCO_BR_CD   " & vbNewLine _
    '                                 & "  AND M38_05.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
    '                                 & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
    '                                 & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
    '                                 & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_FIXED_FLAG       = KBN_01.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
    '                                 & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
    '                                 & "   ON F04_01.SEIQ_TARIFF_BUNRUI_KB = KBN_02.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_02.KBN_GROUP_CD          = 'T015'                " & vbNewLine _
    '                                 & "  AND KBN_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                " & vbNewLine _
    '                                 & "   ON F04_01.TAX_KB                = KBN_03.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_03.KBN_GROUP_CD          = 'Z001'                " & vbNewLine _
    '                                 & "  AND KBN_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                " & vbNewLine _
    '                                 & "   ON F02_01.MOTO_DATA_KB          = KBN_04.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_04.KBN_GROUP_CD          = 'M004'                " & vbNewLine _
    '                                 & "  AND KBN_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                " & vbNewLine _
    '                                 & "   ON F02_01.BIN_KB                = KBN_05.KBN_CD         " & vbNewLine _
    '                                 & "  AND KBN_05.KBN_GROUP_CD          = 'U001'                " & vbNewLine _
    '                                 & "  AND KBN_05.SYS_DEL_FLG           = '0'                   " & vbNewLine
    Private Const SQL_FROM_1 As String = " FROM      $LM_TRN$..F_UNCHIN_TRS    F04_01                " & vbNewLine _
                                 & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
                                 & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
                                 & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN                                                 " & vbNewLine
    Private Const SQL_FROM_2 As String = "   F01_01                                            " & vbNewLine _
                                 & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
                                 & " LEFT JOIN                                                 " & vbNewLine
    Private Const SQL_FROM_3 As String = "   F01_02                " & vbNewLine _
                                  & "   ON F02_01.TRIP_NO_SYUKA         = F01_02.TRIP_NO        " & vbNewLine _
                                  & " LEFT JOIN                                                 " & vbNewLine
    Private Const SQL_FROM_4 As String = "   F01_03                " & vbNewLine _
                                  & "   ON F02_01.TRIP_NO_TYUKEI        = F01_03.TRIP_NO        " & vbNewLine _
                                  & " LEFT JOIN                                                 " & vbNewLine
    Private Const SQL_FROM_5 As String = "   F01_04                " & vbNewLine _
                                  & "   ON F02_01.TRIP_NO_HAIKA         = F01_04.TRIP_NO        " & vbNewLine _
                                  & " LEFT JOIN $LM_TRN$..F_UNCHIN_TRS    F04_02                " & vbNewLine _
                                  & "   ON F04_01.SEIQ_GROUP_NO         = F04_02.UNSO_NO_L      " & vbNewLine _
                                  & "  AND F04_01.SEIQ_GROUP_NO_M       = F04_02.UNSO_NO_M      " & vbNewLine _
                                  & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_TRN$..F_UNSO_M        UNSOM                 " & vbNewLine _
                                  & "   ON UNSOM.UNSO_NO_L              = F04_01.UNSO_NO_L      " & vbNewLine _
                                  & "  AND UNSOM.UNSO_NO_M              = F04_01.UNSO_NO_M      " & vbNewLine _
                                  & "  AND UNSOM.SYS_DEL_FLG            = '0'                   " & vbNewLine _
                                  & "    LEFT JOIN $LM_TRN$..C_OUTKA_L    OUTKAL                " & vbNewLine _
                                  & "  ON OUTKAL.NRS_BR_CD             = F02_01.NRS_BR_CD       " & vbNewLine _
                                  & " AND OUTKAL.OUTKA_NO_L            = F02_01.INOUTKA_NO_L    " & vbNewLine _
                                  & "  AND F02_01.MOTO_DATA_KB          = '20'                  " & vbNewLine _
                                  & "  AND OUTKAL.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & "   LEFT JOIN (SELECT NRS_BR_CD,MIN(EDI_CTL_NO) AS EDI_CTL_NO " & vbNewLine _
                                  & "           ,OUTKA_CTL_NO                                   " & vbNewLine _
                                  & "           FROM  $LM_TRN$..H_OUTKAEDI_L WHERE DEL_KB = '0' " & vbNewLine _
                                  & "          GROUP BY NRS_BR_CD,OUTKA_CTL_NO                  " & vbNewLine _
                                  & " )  OUTKAEDIL_SUM                                          " & vbNewLine _
                                  & "   ON OUTKAEDIL_SUM.NRS_BR_CD          = OUTKAL.NRS_BR_CD  " & vbNewLine _
                                  & "  AND OUTKAEDIL_SUM.OUTKA_CTL_NO       = OUTKAL.OUTKA_NO_L " & vbNewLine _
                                  & "    LEFT JOIN $LM_TRN$..H_OUTKAEDI_L AS OUTKAEDIL          " & vbNewLine _
                                  & " ON OUTKAEDIL_SUM.NRS_BR_CD          = OUTKAEDIL.NRS_BR_CD " & vbNewLine _
                                  & "  AND OUTKAEDIL_SUM.EDI_CTL_NO      = OUTKAEDIL.EDI_CTL_NO " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_CUST          M07_01                " & vbNewLine _
                                  & "   ON F02_01.NRS_BR_CD             = M07_01.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F02_01.CUST_CD_L             = M07_01.CUST_CD_L      " & vbNewLine _
                                  & "  AND F02_01.CUST_CD_M             = M07_01.CUST_CD_M      " & vbNewLine _
                                  & "  AND M07_01.CUST_CD_S             = '00'                  " & vbNewLine _
                                  & "  AND M07_01.CUST_CD_SS            = '00'                  " & vbNewLine _
                                  & "  AND M07_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN COM_DB..M_CURR          M_CURR                  " & vbNewLine _
                                  & "   ON M_CURR.BASE_CURR_CD          = (CASE WHEN M07_01.ITEM_CURR_CD = '' THEN 'JPY' ELSE M07_01.ITEM_CURR_CD END) " & vbNewLine _
                                  & "  AND M_CURR.CURR_CD               = (SELECT CASE WHEN SEIQ_CURR_CD = '' THEN 'JPY' ELSE SEIQ_CURR_CD END FROM $LM_MST$..M_SEIQTO WHERE NRS_BR_CD = @NRS_BR_CD AND SEIQTO_CD = M07_01.UNCHIN_SEIQTO_CD AND SYS_DEL_FLG = '0') " & vbNewLine _
                                  & "  AND M_CURR.UP_FLG                = '00000'               " & vbNewLine _
                                  & "  AND M_CURR.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_SEIQTO        M06_01                " & vbNewLine _
                                  & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F04_01.SEIQTO_CD             = M06_01.SEIQTO_CD      " & vbNewLine _
                                  & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_DEST          M10_01                " & vbNewLine _
                                  & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
                                  & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
                                  & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_DEST          M10_02                " & vbNewLine _
                                  & "   ON F02_01.NRS_BR_CD             = M10_02.NRS_BR_CD      " & vbNewLine _
                                  & "  AND 'ZZZZZ'                      = M10_02.CUST_CD_L      " & vbNewLine _
                                  & "  AND F02_01.DEST_CD               = M10_02.DEST_CD        " & vbNewLine _
                                  & "  AND M10_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_DEST          M10_03                " & vbNewLine _
                                  & "   ON F02_01.NRS_BR_CD             = M10_03.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F02_01.CUST_CD_L             = M10_03.CUST_CD_L      " & vbNewLine _
                                  & "  AND F02_01.ORIG_CD               = M10_03.DEST_CD        " & vbNewLine _
                                  & "  AND M10_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_DEST          M10_04                " & vbNewLine _
                                  & "   ON F02_01.NRS_BR_CD             = M10_04.NRS_BR_CD      " & vbNewLine _
                                  & "  AND 'ZZZZZ'                      = M10_04.CUST_CD_L      " & vbNewLine _
                                  & "  AND F02_01.ORIG_CD               = M10_04.DEST_CD        " & vbNewLine _
                                  & "  AND M10_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_01                " & vbNewLine _
                                  & "   ON F02_01.NRS_BR_CD             = M38_01.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F02_01.UNSO_CD               = M38_01.UNSOCO_CD      " & vbNewLine _
                                  & "  AND F02_01.UNSO_BR_CD            = M38_01.UNSOCO_BR_CD   " & vbNewLine _
                                  & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_02                " & vbNewLine _
                                  & "   ON F01_01.NRS_BR_CD             = M38_02.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F01_01.UNSOCO_CD             = M38_02.UNSOCO_CD      " & vbNewLine _
                                  & "  AND F01_01.UNSOCO_BR_CD          = M38_02.UNSOCO_BR_CD   " & vbNewLine _
                                  & "  AND M38_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_03                " & vbNewLine _
                                  & "   ON F01_02.NRS_BR_CD             = M38_03.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F01_02.UNSOCO_CD             = M38_03.UNSOCO_CD      " & vbNewLine _
                                  & "  AND F01_02.UNSOCO_BR_CD          = M38_03.UNSOCO_BR_CD   " & vbNewLine _
                                  & "  AND M38_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_04                " & vbNewLine _
                                  & "   ON F01_03.NRS_BR_CD             = M38_04.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F01_03.UNSOCO_CD             = M38_04.UNSOCO_CD      " & vbNewLine _
                                  & "  AND F01_03.UNSOCO_BR_CD          = M38_04.UNSOCO_BR_CD   " & vbNewLine _
                                  & "  AND M38_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_05                " & vbNewLine _
                                  & "   ON F01_04.NRS_BR_CD             = M38_05.NRS_BR_CD      " & vbNewLine _
                                  & "  AND F01_04.UNSOCO_CD             = M38_05.UNSOCO_CD      " & vbNewLine _
                                  & "  AND F01_04.UNSOCO_BR_CD          = M38_05.UNSOCO_BR_CD   " & vbNewLine _
                                  & "  AND M38_05.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
                                  & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
                                  & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
                                  & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
                                  & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
                                  & "   ON F04_01.SEIQ_FIXED_FLAG       = KBN_01.KBN_CD         " & vbNewLine _
                                  & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
                                  & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
                                  & "   ON F04_01.SEIQ_TARIFF_BUNRUI_KB = KBN_02.KBN_CD         " & vbNewLine _
                                  & "  AND KBN_02.KBN_GROUP_CD          = 'T015'                " & vbNewLine _
                                  & "  AND KBN_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                " & vbNewLine _
                                  & "   ON F04_01.TAX_KB                = KBN_03.KBN_CD         " & vbNewLine _
                                  & "  AND KBN_03.KBN_GROUP_CD          = 'Z001'                " & vbNewLine _
                                  & "  AND KBN_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                " & vbNewLine _
                                  & "   ON F02_01.MOTO_DATA_KB          = KBN_04.KBN_CD         " & vbNewLine _
                                  & "  AND KBN_04.KBN_GROUP_CD          = 'M004'                " & vbNewLine _
                                  & "  AND KBN_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                " & vbNewLine _
                                  & "   ON F02_01.BIN_KB                = KBN_05.KBN_CD         " & vbNewLine _
                                  & "  AND KBN_05.KBN_GROUP_CD          = 'U001'                " & vbNewLine _
                                  & "  AND KBN_05.SYS_DEL_FLG           = '0'                   " & vbNewLine
    'END YANAI 要望番号1199 運賃一覧画面の届先名称・住所取得元を変更
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応
    'END YANAI 要望番号891
    'END YANAI 要望番号376

#End Region

#Region "Where句"

    Private Const SQL_WHERE As String = "WHERE F02_01.NRS_BR_CD             = @NRS_BR_CD          " & vbNewLine _
                                      & "  AND F04_01.SYS_DEL_FLG           = '0'                 " & vbNewLine

#End Region

#Region "F_UNCHIN_TRS_01"

    'START YANAI 要望番号376
    'Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                                         " & vbNewLine _
    '                                          & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
    '                                          & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
    '                                          & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
    '                                          & ",F04_01.SEIQ_GROUP_NO                  AS      SEIQ_GROUP_NO                   " & vbNewLine _
    '                                          & ",F04_01.SEIQ_GROUP_NO_M                AS      SEIQ_GROUP_NO_M                 " & vbNewLine _
    '                                          & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                          & "                 THEN F02_01.SEIQ_TARIFF_CD                                    " & vbNewLine _
    '                                          & "                 ELSE F04_01.SEIQ_TARIFF_CD                                    " & vbNewLine _
    '                                          & " END                                   AS      SEIQ_TARIFF_CD                  " & vbNewLine _
    '                                          & ",F02_01.TAX_KB                         AS      TAX_KB                          " & vbNewLine _
    '                                          & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                          & "                 THEN F02_01.SEIQ_ETARIFF_CD                                   " & vbNewLine _
    '                                          & "                 ELSE F04_01.SEIQ_ETARIFF_CD                                   " & vbNewLine _
    '                                          & " END                                   AS      SEIQ_ETARIFF_CD                 " & vbNewLine _
    '                                          & ",F04_01.SEIQ_NG_NB                     AS      DECI_NG_NB                      " & vbNewLine _
    '                                          & ",F04_01.SEIQ_NG_NB                     AS      SEIQ_NG_NB                      " & vbNewLine _
    '                                          & ",F04_01.SEIQ_KYORI                     AS      SEIQ_KYORI                      " & vbNewLine _
    '                                          & ",F04_01.SEIQ_WT                        AS      SEIQ_WT                         " & vbNewLine _
    '                                          & ",F04_01.SEIQ_UNCHIN                    AS      SEIQ_UNCHIN                     " & vbNewLine _
    '                                          & ",F04_01.SEIQ_CITY_EXTC                 AS      SEIQ_CITY_EXTC                  " & vbNewLine _
    '                                          & ",F04_01.SEIQ_WINT_EXTC                 AS      SEIQ_WINT_EXTC                  " & vbNewLine _
    '                                          & ",F04_01.SEIQ_RELY_EXTC                 AS      SEIQ_RELY_EXTC                  " & vbNewLine _
    '                                          & ",F04_01.SEIQ_TOLL                      AS      SEIQ_TOLL                       " & vbNewLine _
    '                                          & ",F04_01.SEIQ_INSU                      AS      SEIQ_INSU                       " & vbNewLine _
    '                                          & ",F04_01.DECI_NG_NB                     AS      DECI_NG_NB                      " & vbNewLine _
    '                                          & ",F04_01.DECI_KYORI                     AS      DECI_KYORI                      " & vbNewLine _
    '                                          & ",F04_01.DECI_WT                        AS      DECI_WT                         " & vbNewLine _
    '                                          & ",F04_01.DECI_UNCHIN                    AS      DECI_UNCHIN                     " & vbNewLine _
    '                                          & ",F04_01.DECI_CITY_EXTC                 AS      DECI_CITY_EXTC                  " & vbNewLine _
    '                                          & ",F04_01.DECI_WINT_EXTC                 AS      DECI_WINT_EXTC                  " & vbNewLine _
    '                                          & ",F04_01.DECI_RELY_EXTC                 AS      DECI_RELY_EXTC                  " & vbNewLine _
    '                                          & ",F04_01.DECI_TOLL                      AS      DECI_TOLL                       " & vbNewLine _
    '                                          & ",F04_01.DECI_INSU                      AS      DECI_INSU                       " & vbNewLine _
    '                                          & ",F04_01.KANRI_UNCHIN                   AS      KANRI_UNCHIN                    " & vbNewLine _
    '                                          & ",F04_01.KANRI_CITY_EXTC                AS      KANRI_CITY_EXTC                 " & vbNewLine _
    '                                          & ",F04_01.KANRI_WINT_EXTC                AS      KANRI_WINT_EXTC                 " & vbNewLine _
    '                                          & ",F04_01.KANRI_RELY_EXTC                AS      KANRI_RELY_EXTC                 " & vbNewLine _
    '                                          & ",F04_01.KANRI_TOLL                     AS      KANRI_TOLL                      " & vbNewLine _
    '                                          & ",F04_01.KANRI_INSU                     AS      KANRI_INSU                      " & vbNewLine _
    '                                          & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                          & "                 THEN F02_01.VCLE_KB                                           " & vbNewLine _
    '                                          & "                 ELSE F04_01.SEIQ_SYARYO_KB                                    " & vbNewLine _
    '                                          & " END                                   AS      SEIQ_SYARYO_KB                  " & vbNewLine _
    '                                          & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                          & "                 THEN F02_01.NB_UT                                             " & vbNewLine _
    '                                          & "                 ELSE F04_01.SEIQ_PKG_UT                                       " & vbNewLine _
    '                                          & " END                                   AS      SEIQ_PKG_UT                     " & vbNewLine _
    '                                          & ",F04_01.SEIQ_DANGER_KB                 AS      SEIQ_DANGER_KB                  " & vbNewLine _
    '                                          & ",F04_01.REMARK                         AS      REMARK                          " & vbNewLine _
    '                                          & ",F02_01.SYS_UPD_DATE                   AS      SYS_UPD_DATE                    " & vbNewLine _
    '                                          & ",F02_01.SYS_UPD_TIME                   AS      SYS_UPD_TIME                    " & vbNewLine _
    '                                          & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
    '                                          & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
    '                                          & ",@CD_M                                 AS      CD_M                            " & vbNewLine _
    '                                          & ",@CD_S                                 AS      CD_S                            " & vbNewLine _
    '                                          & ",@CD_SS                                AS      CD_SS                           " & vbNewLine _
    '                                          & ",@SEIQ_FIXED_FLAG                      AS      SEIQ_FIXED_FLAG                 " & vbNewLine _
    '                                          & ",F02_01.OUTKA_PLAN_DATE                AS      OUTKA_PLAN_DATE                 " & vbNewLine _
    '                                          & ",F02_01.ARR_PLAN_DATE                  AS      ARR_PLAN_DATE                   " & vbNewLine _
    '                                          & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                          & "                 THEN F02_01.TARIFF_BUNRUI_KB                                  " & vbNewLine _
    '                                          & "                 ELSE F04_01.SEIQ_TARIFF_BUNRUI_KB                             " & vbNewLine _
    '                                          & " END                                   AS      SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
    '                                          & ",F02_01.MOTO_DATA_KB                   AS      MOTO_DATA_KB                    " & vbNewLine _
    '                                          & ",CASE @ITEM_DATA WHEN '08'                                                     " & vbNewLine _
    '                                          & "                 THEN @UNTIN_CALCULATION_KB                                    " & vbNewLine _
    '                                          & "                 ELSE F04_01.UNTIN_CALCULATION_KB                              " & vbNewLine _
    '                                          & " END                                   AS      UNTIN_CALCULATION_KB            " & vbNewLine _
    '                                          & ",F02_01.CUST_CD_L                      AS      CUST_CD_L                       " & vbNewLine _
    '                                          & ",F02_01.CUST_CD_M                      AS      CUST_CD_M                       " & vbNewLine _
    '                                          & ",F02_01.DEST_CD                        AS      DEST_CD                         " & vbNewLine _
    '                                          & ",M10_01.JIS                            AS      DEST_JIS                        " & vbNewLine _
    '                                          & ",F02_01.VCLE_KB                        AS      VCLE_KB                         " & vbNewLine _
    '                                          & ",F02_01.UNSO_ONDO_KB                   AS      UNSO_ONDO_KB                    " & vbNewLine _
    '                                          & ",F04_01.SIZE_KB                        AS      SIZE_KB                         " & vbNewLine _
    '                                          & ",F04_01.SEIQTO_CD                      AS      SEIQTO_CD                       " & vbNewLine _
    '                                          & ",F04_01.SEIQ_WT                        AS      SEIQ_WT                         " & vbNewLine _
    '                                          & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
    '                                          & " FROM       $LM_TRN$..F_UNCHIN_TRS F04_01                                      " & vbNewLine _
    '                                          & " LEFT  JOIN $LM_TRN$..F_UNSO_L     F02_01                                      " & vbNewLine _
    '                                          & "   ON  F04_01.UNSO_NO_L          = F02_01.UNSO_NO_L                            " & vbNewLine _
    '                                          & "  AND  F02_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                          & " LEFT  JOIN $LM_TRN$..B_INKA_L     B01_01                                      " & vbNewLine _
    '                                          & "   ON  F02_01.NRS_BR_CD          = B01_01.NRS_BR_CD                            " & vbNewLine _
    '                                          & "  AND  F02_01.INOUTKA_NO_L       = B01_01.INKA_NO_L                            " & vbNewLine _
    '                                          & "  AND  B01_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                          & " LEFT  JOIN $LM_TRN$..C_OUTKA_L    C01_01                                      " & vbNewLine _
    '                                          & "   ON  F02_01.NRS_BR_CD          = C01_01.NRS_BR_CD                            " & vbNewLine _
    '                                          & "  AND  F02_01.INOUTKA_NO_L       = C01_01.OUTKA_NO_L                           " & vbNewLine _
    '                                          & "  AND  C01_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                          & " LEFT  JOIN $LM_MST$..M_DEST       M10_01                                      " & vbNewLine _
    '                                          & "   ON  F02_01.NRS_BR_CD          = M10_01.NRS_BR_CD                            " & vbNewLine _
    '                                          & "  AND  F02_01.CUST_CD_L          = M10_01.CUST_CD_L                            " & vbNewLine _
    '                                          & "  AND  F02_01.DEST_CD            = M10_01.DEST_CD                              " & vbNewLine _
    '                                          & "  AND  M10_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                          & "WHERE  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine
    'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
    'Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                                         " & vbNewLine _
    '                                              & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
    '                                              & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
    '                                              & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
    '                                              & ",F04_01.SEIQ_GROUP_NO                  AS      SEIQ_GROUP_NO                   " & vbNewLine _
    '                                              & ",F04_01.SEIQ_GROUP_NO_M                AS      SEIQ_GROUP_NO_M                 " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.SEIQ_TARIFF_CD                                    " & vbNewLine _
    '                                              & "                 ELSE F04_01.SEIQ_TARIFF_CD                                    " & vbNewLine _
    '                                              & " END                                   AS      SEIQ_TARIFF_CD                  " & vbNewLine _
    '                                              & ",F02_01.TAX_KB                         AS      TAX_KB                          " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.SEIQ_ETARIFF_CD                                   " & vbNewLine _
    '                                              & "                 ELSE F04_01.SEIQ_ETARIFF_CD                                   " & vbNewLine _
    '                                              & " END                                   AS      SEIQ_ETARIFF_CD                 " & vbNewLine _
    '                                              & ",F04_01.SEIQ_NG_NB                     AS      DECI_NG_NB                      " & vbNewLine _
    '                                              & ",F04_01.SEIQ_NG_NB                     AS      SEIQ_NG_NB                      " & vbNewLine _
    '                                              & ",F04_01.SEIQ_KYORI                     AS      SEIQ_KYORI                      " & vbNewLine _
    '                                              & ",F04_01.SEIQ_WT                        AS      SEIQ_WT                         " & vbNewLine _
    '                                              & ",F04_01.SEIQ_UNCHIN                    AS      SEIQ_UNCHIN                     " & vbNewLine _
    '                                              & ",F04_01.SEIQ_CITY_EXTC                 AS      SEIQ_CITY_EXTC                  " & vbNewLine _
    '                                              & ",F04_01.SEIQ_WINT_EXTC                 AS      SEIQ_WINT_EXTC                  " & vbNewLine _
    '                                              & ",F04_01.SEIQ_RELY_EXTC                 AS      SEIQ_RELY_EXTC                  " & vbNewLine _
    '                                              & ",F04_01.SEIQ_TOLL                      AS      SEIQ_TOLL                       " & vbNewLine _
    '                                              & ",F04_01.SEIQ_INSU                      AS      SEIQ_INSU                       " & vbNewLine _
    '                                              & ",F04_01.DECI_NG_NB                     AS      DECI_NG_NB                      " & vbNewLine _
    '                                              & ",F04_01.DECI_KYORI                     AS      DECI_KYORI                      " & vbNewLine _
    '                                              & ",F04_01.DECI_WT                        AS      DECI_WT                         " & vbNewLine _
    '                                              & ",F04_01.DECI_UNCHIN                    AS      DECI_UNCHIN                     " & vbNewLine _
    '                                              & ",F04_01.DECI_CITY_EXTC                 AS      DECI_CITY_EXTC                  " & vbNewLine _
    '                                              & ",F04_01.DECI_WINT_EXTC                 AS      DECI_WINT_EXTC                  " & vbNewLine _
    '                                              & ",F04_01.DECI_RELY_EXTC                 AS      DECI_RELY_EXTC                  " & vbNewLine _
    '                                              & ",F04_01.DECI_TOLL                      AS      DECI_TOLL                       " & vbNewLine _
    '                                              & ",F04_01.DECI_INSU                      AS      DECI_INSU                       " & vbNewLine _
    '                                              & ",F04_01.KANRI_UNCHIN                   AS      KANRI_UNCHIN                    " & vbNewLine _
    '                                              & ",F04_01.KANRI_CITY_EXTC                AS      KANRI_CITY_EXTC                 " & vbNewLine _
    '                                              & ",F04_01.KANRI_WINT_EXTC                AS      KANRI_WINT_EXTC                 " & vbNewLine _
    '                                              & ",F04_01.KANRI_RELY_EXTC                AS      KANRI_RELY_EXTC                 " & vbNewLine _
    '                                              & ",F04_01.KANRI_TOLL                     AS      KANRI_TOLL                      " & vbNewLine _
    '                                              & ",F04_01.KANRI_INSU                     AS      KANRI_INSU                      " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.VCLE_KB                                           " & vbNewLine _
    '                                              & "                 ELSE F04_01.SEIQ_SYARYO_KB                                    " & vbNewLine _
    '                                              & " END                                   AS      SEIQ_SYARYO_KB                  " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.NB_UT                                             " & vbNewLine _
    '                                              & "                 ELSE F04_01.SEIQ_PKG_UT                                       " & vbNewLine _
    '                                              & " END                                   AS      SEIQ_PKG_UT                     " & vbNewLine _
    '                                              & ",F04_01.SEIQ_DANGER_KB                 AS      SEIQ_DANGER_KB                  " & vbNewLine _
    '                                              & ",F04_01.REMARK                         AS      REMARK                          " & vbNewLine _
    '                                              & ",F02_01.SYS_UPD_DATE                   AS      SYS_UPD_DATE                    " & vbNewLine _
    '                                              & ",F02_01.SYS_UPD_TIME                   AS      SYS_UPD_TIME                    " & vbNewLine _
    '                                              & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
    '                                              & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
    '                                              & ",@CD_M                                 AS      CD_M                            " & vbNewLine _
    '                                              & ",@CD_S                                 AS      CD_S                            " & vbNewLine _
    '                                              & ",@CD_SS                                AS      CD_SS                           " & vbNewLine _
    '                                              & ",@SEIQ_FIXED_FLAG                      AS      SEIQ_FIXED_FLAG                 " & vbNewLine _
    '                                              & ",F02_01.OUTKA_PLAN_DATE                AS      OUTKA_PLAN_DATE                 " & vbNewLine _
    '                                              & ",F02_01.ARR_PLAN_DATE                  AS      ARR_PLAN_DATE                   " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.TARIFF_BUNRUI_KB                                  " & vbNewLine _
    '                                              & "                 ELSE F04_01.SEIQ_TARIFF_BUNRUI_KB                             " & vbNewLine _
    '                                              & " END                                   AS      SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
    '                                              & ",F02_01.MOTO_DATA_KB                   AS      MOTO_DATA_KB                    " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '08'                                                     " & vbNewLine _
    '                                              & "                 THEN @UNTIN_CALCULATION_KB                                    " & vbNewLine _
    '                                              & "                 ELSE F04_01.UNTIN_CALCULATION_KB                              " & vbNewLine _
    '                                              & " END                                   AS      UNTIN_CALCULATION_KB            " & vbNewLine _
    '                                              & ",F02_01.CUST_CD_L                      AS      CUST_CD_L                       " & vbNewLine _
    '                                              & ",F02_01.CUST_CD_M                      AS      CUST_CD_M                       " & vbNewLine _
    '                                              & ",F02_01.DEST_CD                        AS      DEST_CD                         " & vbNewLine _
    '                                              & ",ISNULL(M10_01.JIS,M10_02.JIS)         AS      DEST_JIS                        " & vbNewLine _
    '                                              & ",F02_01.VCLE_KB                        AS      VCLE_KB                         " & vbNewLine _
    '                                              & ",F02_01.UNSO_ONDO_KB                   AS      UNSO_ONDO_KB                    " & vbNewLine _
    '                                              & ",F04_01.SIZE_KB                        AS      SIZE_KB                         " & vbNewLine _
    '                                              & ",F04_01.SEIQTO_CD                      AS      SEIQTO_CD                       " & vbNewLine _
    '                                              & ",F04_01.SEIQ_WT                        AS      SEIQ_WT                         " & vbNewLine _
    '                                              & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
    '                                              & " FROM       $LM_TRN$..F_UNCHIN_TRS F04_01                                      " & vbNewLine _
    '                                              & " LEFT  JOIN $LM_TRN$..F_UNSO_L     F02_01                                      " & vbNewLine _
    '                                              & "   ON  F04_01.UNSO_NO_L          = F02_01.UNSO_NO_L                            " & vbNewLine _
    '                                              & "  AND  F02_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                              & " LEFT  JOIN $LM_TRN$..B_INKA_L     B01_01                                      " & vbNewLine _
    '                                              & "   ON  F02_01.NRS_BR_CD          = B01_01.NRS_BR_CD                            " & vbNewLine _
    '                                              & "  AND  F02_01.INOUTKA_NO_L       = B01_01.INKA_NO_L                            " & vbNewLine _
    '                                              & "  AND  B01_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                              & " LEFT  JOIN $LM_TRN$..C_OUTKA_L    C01_01                                      " & vbNewLine _
    '                                              & "   ON  F02_01.NRS_BR_CD          = C01_01.NRS_BR_CD                            " & vbNewLine _
    '                                              & "  AND  F02_01.INOUTKA_NO_L       = C01_01.OUTKA_NO_L                           " & vbNewLine _
    '                                              & "  AND  C01_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                              & " LEFT  JOIN $LM_MST$..M_DEST       M10_01                                      " & vbNewLine _
    '                                              & "   ON  F02_01.NRS_BR_CD          = M10_01.NRS_BR_CD                            " & vbNewLine _
    '                                              & "  AND  F02_01.CUST_CD_L          = M10_01.CUST_CD_L                            " & vbNewLine _
    '                                              & "  AND  F02_01.DEST_CD            = M10_01.DEST_CD                              " & vbNewLine _
    '                                              & "  AND  M10_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                              & " LEFT  JOIN $LM_MST$..M_DEST       M10_02                                      " & vbNewLine _
    '                                              & "   ON  F02_01.NRS_BR_CD          = M10_02.NRS_BR_CD                            " & vbNewLine _
    '                                              & "  AND  'ZZZZZ'                   = M10_02.CUST_CD_L                            " & vbNewLine _
    '                                              & "  AND  F02_01.DEST_CD            = M10_02.DEST_CD                              " & vbNewLine _
    '                                              & "  AND  M10_02.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                              & "WHERE  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine
    Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                                         " & vbNewLine _
                                                  & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
                                                  & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
                                                  & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
                                                  & ",F04_01.SEIQ_GROUP_NO                  AS      SEIQ_GROUP_NO                   " & vbNewLine _
                                                  & ",F04_01.SEIQ_GROUP_NO_M                AS      SEIQ_GROUP_NO_M                 " & vbNewLine _
                                                  & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
                                                  & "                 THEN F02_01.SEIQ_TARIFF_CD                                    " & vbNewLine _
                                                  & "                 ELSE F04_01.SEIQ_TARIFF_CD                                    " & vbNewLine _
                                                  & " END                                   AS      SEIQ_TARIFF_CD                  " & vbNewLine _
                                                  & ",F02_01.TAX_KB                         AS      TAX_KB                          " & vbNewLine _
                                                  & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
                                                  & "                 THEN F02_01.SEIQ_ETARIFF_CD                                   " & vbNewLine _
                                                  & "                 ELSE F04_01.SEIQ_ETARIFF_CD                                   " & vbNewLine _
                                                  & " END                                   AS      SEIQ_ETARIFF_CD                 " & vbNewLine _
                                                  & ",F04_01.SEIQ_NG_NB                     AS      DECI_NG_NB                      " & vbNewLine _
                                                  & ",F04_01.SEIQ_NG_NB                     AS      SEIQ_NG_NB                      " & vbNewLine _
                                                  & ",F04_01.SEIQ_KYORI                     AS      SEIQ_KYORI                      " & vbNewLine _
                                                  & ",F04_01.SEIQ_WT                        AS      SEIQ_WT                         " & vbNewLine _
                                                  & ",F04_01.SEIQ_UNCHIN                    AS      SEIQ_UNCHIN                     " & vbNewLine _
                                                  & ",F04_01.SEIQ_CITY_EXTC                 AS      SEIQ_CITY_EXTC                  " & vbNewLine _
                                                  & ",F04_01.SEIQ_WINT_EXTC                 AS      SEIQ_WINT_EXTC                  " & vbNewLine _
                                                  & ",F04_01.SEIQ_RELY_EXTC                 AS      SEIQ_RELY_EXTC                  " & vbNewLine _
                                                  & ",F04_01.SEIQ_TOLL                      AS      SEIQ_TOLL                       " & vbNewLine _
                                                  & ",F04_01.SEIQ_INSU                      AS      SEIQ_INSU                       " & vbNewLine _
                                                  & ",F04_01.DECI_NG_NB                     AS      DECI_NG_NB                      " & vbNewLine _
                                                  & ",F04_01.DECI_KYORI                     AS      DECI_KYORI                      " & vbNewLine _
                                                  & ",F04_01.DECI_WT                        AS      DECI_WT                         " & vbNewLine _
                                                  & ",F04_01.DECI_UNCHIN                    AS      DECI_UNCHIN                     " & vbNewLine _
                                                  & ",F04_01.DECI_CITY_EXTC                 AS      DECI_CITY_EXTC                  " & vbNewLine _
                                                  & ",F04_01.DECI_WINT_EXTC                 AS      DECI_WINT_EXTC                  " & vbNewLine _
                                                  & ",F04_01.DECI_RELY_EXTC                 AS      DECI_RELY_EXTC                  " & vbNewLine _
                                                  & ",F04_01.DECI_TOLL                      AS      DECI_TOLL                       " & vbNewLine _
                                                  & ",F04_01.DECI_INSU                      AS      DECI_INSU                       " & vbNewLine _
                                                  & ",F04_01.KANRI_UNCHIN                   AS      KANRI_UNCHIN                    " & vbNewLine _
                                                  & ",F04_01.KANRI_CITY_EXTC                AS      KANRI_CITY_EXTC                 " & vbNewLine _
                                                  & ",F04_01.KANRI_WINT_EXTC                AS      KANRI_WINT_EXTC                 " & vbNewLine _
                                                  & ",F04_01.KANRI_RELY_EXTC                AS      KANRI_RELY_EXTC                 " & vbNewLine _
                                                  & ",F04_01.KANRI_TOLL                     AS      KANRI_TOLL                      " & vbNewLine _
                                                  & ",F04_01.KANRI_INSU                     AS      KANRI_INSU                      " & vbNewLine _
                                                  & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
                                                  & "                 THEN F02_01.VCLE_KB                                           " & vbNewLine _
                                                  & "                 ELSE F04_01.SEIQ_SYARYO_KB                                    " & vbNewLine _
                                                  & " END                                   AS      SEIQ_SYARYO_KB                  " & vbNewLine _
                                                  & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
                                                  & "                 THEN F02_01.NB_UT                                             " & vbNewLine _
                                                  & "                 ELSE F04_01.SEIQ_PKG_UT                                       " & vbNewLine _
                                                  & " END                                   AS      SEIQ_PKG_UT                     " & vbNewLine _
                                                  & ",F04_01.SEIQ_DANGER_KB                 AS      SEIQ_DANGER_KB                  " & vbNewLine _
                                                  & ",F04_01.REMARK                         AS      REMARK                          " & vbNewLine _
                                                  & ",F02_01.SYS_UPD_DATE                   AS      SYS_UPD_DATE                    " & vbNewLine _
                                                  & ",F02_01.SYS_UPD_TIME                   AS      SYS_UPD_TIME                    " & vbNewLine _
                                                  & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
                                                  & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
                                                  & ",@CD_M                                 AS      CD_M                            " & vbNewLine _
                                                  & ",@CD_S                                 AS      CD_S                            " & vbNewLine _
                                                  & ",@CD_SS                                AS      CD_SS                           " & vbNewLine _
                                                  & ",@SEIQ_FIXED_FLAG                      AS      SEIQ_FIXED_FLAG                 " & vbNewLine _
                                                  & ",F02_01.OUTKA_PLAN_DATE                AS      OUTKA_PLAN_DATE                 " & vbNewLine _
                                                  & ",F02_01.ARR_PLAN_DATE                  AS      ARR_PLAN_DATE                   " & vbNewLine _
                                                  & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
                                                  & "                 THEN F02_01.TARIFF_BUNRUI_KB                                  " & vbNewLine _
                                                  & "                 ELSE F04_01.SEIQ_TARIFF_BUNRUI_KB                             " & vbNewLine _
                                                  & " END                                   AS      SEIQ_TARIFF_BUNRUI_KB           " & vbNewLine _
                                                  & ",F02_01.MOTO_DATA_KB                   AS      MOTO_DATA_KB                    " & vbNewLine _
                                                  & ",CASE @ITEM_DATA WHEN '08'                                                     " & vbNewLine _
                                                  & "                 THEN @UNTIN_CALCULATION_KB                                    " & vbNewLine _
                                                  & "                 ELSE F04_01.UNTIN_CALCULATION_KB                              " & vbNewLine _
                                                  & " END                                   AS      UNTIN_CALCULATION_KB            " & vbNewLine _
                                                  & ",F02_01.CUST_CD_L                      AS      CUST_CD_L                       " & vbNewLine _
                                                  & ",F02_01.CUST_CD_M                      AS      CUST_CD_M                       " & vbNewLine _
                                                  & ",F02_01.DEST_CD                        AS      DEST_CD                         " & vbNewLine _
                                                  & ",ISNULL(M10_01.JIS,M10_02.JIS)         AS      DEST_JIS                        " & vbNewLine _
                                                  & ",F02_01.VCLE_KB                        AS      VCLE_KB                         " & vbNewLine _
                                                  & ",F02_01.UNSO_ONDO_KB                   AS      UNSO_ONDO_KB                    " & vbNewLine _
                                                  & ",F04_01.SIZE_KB                        AS      SIZE_KB                         " & vbNewLine _
                                                  & ",F04_01.SEIQTO_CD                      AS      SEIQTO_CD                       " & vbNewLine _
                                                  & ",F04_01.SEIQ_WT                        AS      SEIQ_WT                         " & vbNewLine _
                                                  & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
                                                  & ",@NEW_SYS_UPD_DATE                     AS      NEW_SYS_UPD_DATE                " & vbNewLine _
                                                  & ",@NEW_SYS_UPD_TIME                     AS      NEW_SYS_UPD_TIME                " & vbNewLine _
                                                  & ",@SYS_UPD_FLG                          AS      SYS_UPD_FLG                     " & vbNewLine _
                                                  & ",ISNULL(M99_01.TABLE_TP,'')            AS      TABLE_TP                        " & vbNewLine

    Private Const SQL_SELECT_UNCHIN_KYORI As String = ",CASE WHEN F02_01.MOTO_DATA_KB = '10' AND ISNULL(M10_03.NRS_BR_CD,'') <> '' THEN   " & vbNewLine _
                                                     & "	     --入荷の距離取得                                                              " & vbNewLine _
                                                     & "	        CASE WHEN (M10_03.KYORI > 0) THEN M10_03.KYORI                             " & vbNewLine _
                                                     & "    	     WHEN (M10_03.KYORI = 0 AND M10_03.JIS <= MSO.JIS_CD) THEN ISNULL(MK1.KYORI,0)  " & vbNewLine _
                                                     & "      	     WHEN (M10_03.KYORI = 0 AND MSO.JIS_CD <= M10_03.JIS) THEN ISNULL(MK2.KYORI,0)  " & vbNewLine _
                                                     & "      	     ELSE 0                                                                    " & vbNewLine _
                                                     & " 	END                                                                                " & vbNewLine _
                                                     & "      WHEN F02_01.MOTO_DATA_KB = '10' THEN                                             " & vbNewLine _
                                                     & "	        CASE WHEN (M10_04.KYORI > 0) THEN M10_04.KYORI                             " & vbNewLine _
                                                     & "    	     WHEN (M10_04.KYORI = 0 AND M10_04.JIS <= MSO.JIS_CD) THEN ISNULL(MK3.KYORI,0)  " & vbNewLine _
                                                     & "      	     WHEN (M10_04.KYORI = 0 AND MSO.JIS_CD <= M10_04.JIS) THEN ISNULL(MK4.KYORI,0)  " & vbNewLine _
                                                     & "      	     ELSE 0                                                                    " & vbNewLine _
                                                     & " 	END                                                                                " & vbNewLine _
                                                     & "      WHEN F02_01.MOTO_DATA_KB = '20' AND ISNULL(M10_01.NRS_BR_CD,'') <> '' THEN          " & vbNewLine _
                                                     & "	     --出荷の距離取得                                                              " & vbNewLine _
                                                     & "	        CASE WHEN (M10_01.KYORI > 0) THEN M10_01.KYORI                             " & vbNewLine _
                                                     & "    	     WHEN (M10_01.KYORI = 0 AND M10_01.JIS <= MSO.JIS_CD) THEN ISNULL(MK5.KYORI,0)  " & vbNewLine _
                                                     & "      	     WHEN (M10_01.KYORI = 0 AND MSO.JIS_CD <= M10_01.JIS) THEN ISNULL(MK6.KYORI,0)  " & vbNewLine _
                                                     & "      	     ELSE 0                                                                    " & vbNewLine _
                                                     & " 	END                                                                                " & vbNewLine _
                                                     & "      WHEN F02_01.MOTO_DATA_KB = '20' THEN                                             " & vbNewLine _
                                                     & "	        CASE WHEN (M10_02.KYORI > 0) THEN M10_02.KYORI                             " & vbNewLine _
                                                     & "    	     WHEN (M10_02.KYORI = 0 AND M10_02.JIS <= MSO.JIS_CD) THEN ISNULL(MK7.KYORI,0)  " & vbNewLine _
                                                     & "      	     WHEN (M10_02.KYORI = 0 AND MSO.JIS_CD <= M10_02.JIS) THEN ISNULL(MK8.KYORI,0)  " & vbNewLine _
                                                     & "      	     ELSE 0                                                                    " & vbNewLine _
                                                     & " 	END                                                                                " & vbNewLine _
                                                     & "      ELSE                                                                             " & vbNewLine _
                                                     & "	     --運送の距離取得                                                              " & vbNewLine _
                                                     & "          CASE WHEN ISNULL(M10_01.NRS_BR_CD,'') <> '' THEN                                " & vbNewLine _
                                                     & "              CASE WHEN ISNULL(M10_03.NRS_BR_CD,'') <> '' THEN                            " & vbNewLine _
                                                     & "                 CASE WHEN M10_01.KYORI > 0 THEN M10_01.KYORI                          " & vbNewLine _
                                                     & "                      WHEN (M10_01.KYORI = 0 AND M10_03.JIS <= M10_01.JIS) THEN ISNULL(MK9.KYORI,0)   " & vbNewLine _
                                                     & "                      WHEN (M10_01.KYORI = 0 AND M10_01.JIS <= M10_03.JIS) THEN ISNULL(MK10.KYORI,0)  " & vbNewLine _
                                                     & "                      ELSE 0                                                           " & vbNewLine _
                                                     & "                 END                                                                   " & vbNewLine _
                                                     & "              ELSE                                                                     " & vbNewLine _
                                                     & "                 CASE WHEN M10_01.KYORI > 0 THEN M10_01.KYORI                               " & vbNewLine _
                                                     & "                      WHEN (M10_01.KYORI = 0 AND M10_04.JIS <= M10_01.JIS) THEN ISNULL(MK11.KYORI,0)  " & vbNewLine _
                                                     & "                      WHEN (M10_01.KYORI = 0 AND M10_01.JIS <= M10_04.JIS) THEN ISNULL(MK12.KYORI,0)  " & vbNewLine _
                                                     & "                      ELSE 0                                                                " & vbNewLine _
                                                     & "                 END                                                                        " & vbNewLine _
                                                     & "              END                                                                           " & vbNewLine _
                                                     & "          ELSE                                                                              " & vbNewLine _
                                                     & "              CASE WHEN ISNULL(M10_03.NRS_BR_CD,'') <> '' THEN                                 " & vbNewLine _
                                                     & "                 CASE WHEN M10_02.KYORI > 0 THEN M10_02.KYORI                               " & vbNewLine _
                                                     & "                      WHEN (M10_02.KYORI = 0 AND M10_03.JIS <= M10_02.JIS) THEN ISNULL(MK13.KYORI,0)  " & vbNewLine _
                                                     & "                      WHEN (M10_02.KYORI = 0 AND M10_02.JIS <= M10_03.JIS) THEN ISNULL(MK14.KYORI,0)  " & vbNewLine _
                                                     & "                      ELSE 0                                                                " & vbNewLine _
                                                     & "                 END                                                                        " & vbNewLine _
                                                     & "              ELSE                                                                          " & vbNewLine _
                                                     & "                 CASE WHEN M10_02.KYORI > 0 THEN M10_02.KYORI                               " & vbNewLine _
                                                     & "                      WHEN (M10_02.KYORI = 0 AND M10_04.JIS <= M10_02.JIS) THEN ISNULL(MK15.KYORI,0)  " & vbNewLine _
                                                     & "                      WHEN (M10_02.KYORI = 0 AND M10_02.JIS <= M10_04.JIS) THEN ISNULL(MK16.KYORI,0)  " & vbNewLine _
                                                     & "                      ELSE 0                                                                " & vbNewLine _
                                                     & "                 END                                                                        " & vbNewLine _
                                                     & "              END                                                                           " & vbNewLine _
                                                     & "          END                                                                               " & vbNewLine _
                                                     & "      END                    AS CALC_KYORI                                                  " & vbNewLine

    Private Const SQL_FROM_UNCHIN As String = " FROM       $LM_TRN$..F_UNCHIN_TRS F04_01                                      " & vbNewLine _
                                            & " LEFT  JOIN $LM_TRN$..F_UNSO_L     F02_01                                      " & vbNewLine _
                                            & "   ON  F04_01.UNSO_NO_L          = F02_01.UNSO_NO_L                            " & vbNewLine _
                                            & "  AND  F02_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & " LEFT  JOIN $LM_TRN$..B_INKA_L     B01_01                                      " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = B01_01.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  F02_01.INOUTKA_NO_L       = B01_01.INKA_NO_L                            " & vbNewLine _
                                            & "  AND  B01_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & " LEFT  JOIN $LM_TRN$..C_OUTKA_L    C01_01                                      " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = C01_01.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  F02_01.INOUTKA_NO_L       = C01_01.OUTKA_NO_L                           " & vbNewLine _
                                            & "  AND  C01_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & " LEFT  JOIN $LM_MST$..M_DEST       M10_01                                      " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = M10_01.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  F02_01.CUST_CD_L          = M10_01.CUST_CD_L                            " & vbNewLine _
                                            & "  AND  F02_01.DEST_CD            = M10_01.DEST_CD                              " & vbNewLine _
                                            & "  AND  M10_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & " LEFT  JOIN $LM_MST$..M_DEST       M10_02                                      " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = M10_02.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  'ZZZZZ'                   = M10_02.CUST_CD_L                            " & vbNewLine _
                                            & "  AND  F02_01.DEST_CD            = M10_02.DEST_CD                              " & vbNewLine _
                                            & "  AND  M10_02.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & " LEFT  JOIN $LM_MST$..M_UNCHIN_TARIFF M99_01                                   " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = M99_01.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  (CASE @ITEM_DATA WHEN '04' THEN F02_01.SEIQ_TARIFF_CD ELSE F04_01.SEIQ_TARIFF_CD END) = M99_01.UNCHIN_TARIFF_CD " & vbNewLine _
                                            & "  AND  M99_01.DATA_TP            = '00'                                        " & vbNewLine _
                                            & "  AND  M99_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine

    Private Const SQL_FROM_UNCHIN_KYORI As String = " LEFT  JOIN $LM_MST$..M_DEST       M10_03                                " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = M10_03.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  F02_01.CUST_CD_L          = M10_03.CUST_CD_L                            " & vbNewLine _
                                            & "  AND  F02_01.ORIG_CD            = M10_03.DEST_CD                              " & vbNewLine _
                                            & "  AND  M10_03.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & " LEFT  JOIN $LM_MST$..M_DEST       M10_04                                      " & vbNewLine _
                                            & "   ON  F02_01.NRS_BR_CD          = M10_04.NRS_BR_CD                            " & vbNewLine _
                                            & "  AND  'ZZZZZ'                   = M10_04.CUST_CD_L                            " & vbNewLine _
                                            & "  AND  F02_01.ORIG_CD            = M10_04.DEST_CD                              " & vbNewLine _
                                            & "  AND  M10_04.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                            & "--倉庫M                                                                        " & vbNewLine _
                                            & "LEFT JOIN $LM_MST$..M_SOKO MSO                                                 " & vbNewLine _
                                            & "ON  MSO.NRS_BR_CD = CASE F02_01.MOTO_DATA_KB WHEN '10' THEN B01_01.NRS_BR_CD ELSE C01_01.NRS_BR_CD END " & vbNewLine _
                                            & "AND MSO.WH_CD     = CASE F02_01.MOTO_DATA_KB WHEN '10' THEN B01_01.WH_CD ELSE C01_01.WH_CD END         " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)入荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK1                                               " & vbNewLine _
                                            & " ON  MK1.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK1.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK1.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK1.ORIG_JIS_CD = M10_03.JIS                                              " & vbNewLine _
                                            & " AND MK1.DEST_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)入荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK2                                               " & vbNewLine _
                                            & " ON  MK2.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK2.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK2.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK2.ORIG_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & " AND MK2.DEST_JIS_CD = M10_03.JIS                                              " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)入荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK3                                               " & vbNewLine _
                                            & " ON  MK3.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK3.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK3.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK3.ORIG_JIS_CD = M10_04.JIS                                              " & vbNewLine _
                                            & " AND MK3.DEST_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)入荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK4                                               " & vbNewLine _
                                            & " ON  MK4.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK4.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK4.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK4.ORIG_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & " AND MK4.DEST_JIS_CD = M10_04.JIS                                              " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)出荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK5                                               " & vbNewLine _
                                            & " ON  MK5.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK5.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK5.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK5.ORIG_JIS_CD = M10_01.JIS                                              " & vbNewLine _
                                            & " AND MK5.DEST_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)出荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK6                                               " & vbNewLine _
                                            & " ON  MK6.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK6.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK6.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK6.ORIG_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & " AND MK6.DEST_JIS_CD = M10_01.JIS                                              " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)出荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK7                                               " & vbNewLine _
                                            & " ON  MK7.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK7.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK7.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK7.ORIG_JIS_CD = M10_02.JIS                                              " & vbNewLine _
                                            & " AND MK7.DEST_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)出荷・倉庫⇔届け先                                 " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK8                                               " & vbNewLine _
                                            & " ON  MK8.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK8.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK8.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK8.ORIG_JIS_CD = MSO.JIS_CD                                              " & vbNewLine _
                                            & " AND MK8.DEST_JIS_CD = M10_02.JIS                                              " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK9                                               " & vbNewLine _
                                            & " ON  MK9.SYS_DEL_FLG = '0'                                                     " & vbNewLine _
                                            & " AND MK9.NRS_BR_CD = F02_01.NRS_BR_CD                                           " & vbNewLine _
                                            & " AND MK9.KYORI_CD = @CD_L                                                  " & vbNewLine _
                                            & " AND MK9.ORIG_JIS_CD = M10_03.JIS                                              " & vbNewLine _
                                            & " AND MK9.DEST_JIS_CD = M10_01.JIS                                              " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK10                                              " & vbNewLine _
                                            & " ON  MK10.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK10.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK10.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK10.ORIG_JIS_CD = M10_01.JIS                                             " & vbNewLine _
                                            & " AND MK10.DEST_JIS_CD = M10_03.JIS                                             " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK11                                              " & vbNewLine _
                                            & " ON  MK11.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK11.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK11.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK11.ORIG_JIS_CD = M10_04.JIS                                             " & vbNewLine _
                                            & " AND MK11.DEST_JIS_CD = M10_01.JIS                                             " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK12                                              " & vbNewLine _
                                            & " ON  MK12.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK12.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK12.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK12.ORIG_JIS_CD = M10_01.JIS                                             " & vbNewLine _
                                            & " AND MK12.DEST_JIS_CD = M10_04.JIS                                             " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK13                                              " & vbNewLine _
                                            & " ON  MK13.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK13.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK13.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK13.ORIG_JIS_CD = M10_03.JIS                                             " & vbNewLine _
                                            & " AND MK13.DEST_JIS_CD = M10_02.JIS                                             " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK14                                              " & vbNewLine _
                                            & " ON  MK14.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK14.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK14.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK14.ORIG_JIS_CD = M10_02.JIS                                             " & vbNewLine _
                                            & " AND MK14.DEST_JIS_CD = M10_03.JIS                                             " & vbNewLine _
                                            & "--距離程M(ORIG_CD < DEST_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK15                                              " & vbNewLine _
                                            & " ON  MK15.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK15.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK15.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK15.ORIG_JIS_CD = M10_04.JIS                                             " & vbNewLine _
                                            & " AND MK15.DEST_JIS_CD = M10_02.JIS                                             " & vbNewLine _
                                            & "--距離程M(DEST_CD < ORIG_CD)運送                                               " & vbNewLine _
                                            & " LEFT JOIN $LM_MST$..M_KYORI MK16                                              " & vbNewLine _
                                            & " ON  MK16.SYS_DEL_FLG = '0'                                                    " & vbNewLine _
                                            & " AND MK16.NRS_BR_CD = F02_01.NRS_BR_CD                                          " & vbNewLine _
                                            & " AND MK16.KYORI_CD = @CD_L                                                 " & vbNewLine _
                                            & " AND MK16.ORIG_JIS_CD = M10_02.JIS                                             " & vbNewLine _
                                            & " AND MK16.DEST_JIS_CD = M10_04.JIS                                             " & vbNewLine

    Private Const SQL_WHERE_UNCHIN As String = "WHERE  F04_01.SYS_DEL_FLG        = '0'                                        " & vbNewLine
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
    'END YANAI 要望番号376

#End Region

#Region "F_UNCHIN_TRS_02"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
    'Private Const SQL_SELECT_INIT_UNCHIN As String = "SELECT                                                                         " & vbNewLine _
    '                                               & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
    '                                               & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
    '                                               & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
    '                                               & ",F04_01.SEIQ_GROUP_NO                  AS      SEIQ_GROUP_NO                   " & vbNewLine _
    '                                               & ",F04_01.SEIQ_GROUP_NO_M                AS      SEIQ_GROUP_NO_M                 " & vbNewLine _
    '                                               & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
    '                                               & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
    '                                               & ",@CD_M                                 AS      CD_M                            " & vbNewLine _
    '                                               & ",@CD_S                                 AS      CD_S                            " & vbNewLine _
    '                                               & ",@CD_SS                                AS      CD_SS                           " & vbNewLine _
    '                                               & ",@SEIQ_FIXED_FLAG                      AS      SEIQ_FIXED_FLAG                 " & vbNewLine _
    '                                               & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
    '                                               & " FROM       $LM_TRN$..F_UNCHIN_TRS F04_01                                      " & vbNewLine _
    '                                               & " LEFT  JOIN $LM_TRN$..F_UNSO_L     F02_01                                      " & vbNewLine _
    '                                               & "   ON  F04_01.UNSO_NO_L          = F02_01.UNSO_NO_L                            " & vbNewLine _
    '                                               & "  AND  F02_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                               & "WHERE  F04_01.UNSO_NO_L          = @UNSO_NO_L                                  " & vbNewLine _
    '                                               & "  AND  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                               & "ORDER BY F04_01.UNSO_NO_L , F04_01.UNSO_NO_M                                   " & vbNewLine
    Private Const SQL_SELECT_INIT_UNCHIN As String = "SELECT                                                                         " & vbNewLine _
                                                   & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
                                                   & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
                                                   & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
                                                   & ",F04_01.SEIQ_GROUP_NO                  AS      SEIQ_GROUP_NO                   " & vbNewLine _
                                                   & ",F04_01.SEIQ_GROUP_NO_M                AS      SEIQ_GROUP_NO_M                 " & vbNewLine _
                                                   & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
                                                   & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
                                                   & ",@CD_M                                 AS      CD_M                            " & vbNewLine _
                                                   & ",@CD_S                                 AS      CD_S                            " & vbNewLine _
                                                   & ",@CD_SS                                AS      CD_SS                           " & vbNewLine _
                                                   & ",@SEIQ_FIXED_FLAG                      AS      SEIQ_FIXED_FLAG                 " & vbNewLine _
                                                   & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
                                                   & ",@NEW_SYS_UPD_DATE                     AS      NEW_SYS_UPD_DATE                " & vbNewLine _
                                                   & ",@NEW_SYS_UPD_TIME                     AS      NEW_SYS_UPD_TIME                " & vbNewLine _
                                                   & ",@SYS_UPD_FLG                          AS      SYS_UPD_FLG                     " & vbNewLine _
                                                   & " FROM       $LM_TRN$..F_UNCHIN_TRS F04_01                                      " & vbNewLine _
                                                   & " LEFT  JOIN $LM_TRN$..F_UNSO_L     F02_01                                      " & vbNewLine _
                                                   & "   ON  F04_01.UNSO_NO_L          = F02_01.UNSO_NO_L                            " & vbNewLine _
                                                   & "  AND  F02_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                                   & "WHERE  F04_01.UNSO_NO_L          = @UNSO_NO_L                                  " & vbNewLine _
                                                   & "  AND  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                                   & "ORDER BY F04_01.UNSO_NO_L , F04_01.UNSO_NO_M                                   " & vbNewLine
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)


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

#Region "F_UNSO_L"

    Private Const SQL_UPDATE_UNSO_L_SYS_DATETIME As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                           & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                           & "  AND  SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                           & "  AND  SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

    'START YANAI 要望番号936
    Private Const SQL_UPDATE_UNSO_L_TARIFF As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                           & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "      ,SEIQ_TARIFF_CD = @SEIQ_TARIFF_CD" & vbNewLine _
                                                           & "      ,TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                           & "  AND  SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                           & "  AND  SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine
    'END YANAI 要望番号936

    'START YANAI 要望番号996
    Private Const SQL_UPDATE_UNSO_L_ETARIFF As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                      & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                      & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                      & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                      & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                      & "      ,SEIQ_ETARIFF_CD = @SEIQ_ETARIFF_CD" & vbNewLine _
                                                      & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                      & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                      & "  AND  SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                      & "  AND  SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine
    'END YANAI 要望番号996

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    Private Const SQL_UPDATE_UNSO_M_ZBUKACD As String = "UPDATE $LM_TRN$..F_UNSO_M SET      " & vbNewLine _
                                                      & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                      & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                      & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                      & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                      & "      ,ZBUKA_CD     = @ZBUKA_CD    " & vbNewLine _
                                                      & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                      & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine

    Private Const SQL_UPDATE_UNSO_M_ABUKACD As String = "UPDATE $LM_TRN$..F_UNSO_M SET      " & vbNewLine _
                                                      & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                      & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                      & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                      & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                      & "      ,ABUKA_CD     = @ABUKA_CD    " & vbNewLine _
                                                      & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                      & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
    Private Const SQL_UPDATE_HAITA As String = "  AND SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                             & "  AND SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

#End Region

#Region "F_UNCHIN_TRS"

    Private Const SQL_UPDATE_UNCHIN_SYS_DATETIME As String = "      ,SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                           & "  AND  UNSO_NO_M    = @UNSO_NO_M   " & vbNewLine

#End Region

#Region "F_UNCHIN_TRS(再計算時)"

    ''' <summary>
    ''' F_UNSO_L(再計算時) SQL SELECT句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_SELECT_COUNT_SAIKEISAN As String = " SELECT COUNT(UNSOL.UNSO_NO_L)          AS REC_CNT              " & vbNewLine _
                                                        & "FROM $LM_TRN$..F_UNSO_L UNSOL                                  " & vbNewLine _
                                                        & "WHERE UNSOL.NRS_BR_CD = @NRS_BR_CD                             " & vbNewLine _
                                                        & "  AND UNSOL.UNSO_NO_L = @UNSO_NO_L                             " & vbNewLine _
                                                        & "  AND UNSOL.SYS_UPD_DATE = @SYS_UPD_DATE                       " & vbNewLine _
                                                        & "  AND UNSOL.SYS_UPD_TIME = @SYS_UPD_TIME                       " & vbNewLine

    ''' <summary>
    ''' F_UNCHIN_TRS(再計算時) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_UNCHIN_SAIKEISAN As String = "UPDATE $LM_TRN$..F_UNCHIN_TRS SET                              " & vbNewLine _
                                                        & "                  DECI_UNCHIN          = @DECI_UNCHIN          " & vbNewLine _
                                                        & "                 ,DECI_CITY_EXTC       = @DECI_CITY_EXTC       " & vbNewLine _
                                                        & "                 ,DECI_WINT_EXTC       = @DECI_WINT_EXTC       " & vbNewLine _
                                                        & "                 ,DECI_RELY_EXTC       = @DECI_RELY_EXTC       " & vbNewLine _
                                                        & "                 ,DECI_TOLL            = @DECI_TOLL            " & vbNewLine _
                                                        & "                 ,DECI_INSU            = @DECI_INSU            " & vbNewLine _
                                                        & "                 ,SYS_UPD_DATE         = @SYS_UPD_DATE         " & vbNewLine _
                                                        & "                 ,SYS_UPD_TIME         = @SYS_UPD_TIME         " & vbNewLine _
                                                        & "                 ,SYS_UPD_PGID         = @SYS_UPD_PGID         " & vbNewLine _
                                                        & "                 ,SYS_UPD_USER         = @SYS_UPD_USER         " & vbNewLine _
                                                        & "                 WHERE                                         " & vbNewLine _
                                                        & "                       NRS_BR_CD       = @NRS_BR_CD            " & vbNewLine _
                                                        & "                   AND UNSO_NO_L       = @UNSO_NO_L            " & vbNewLine _
                                                        & "                   AND UNSO_NO_M       = @UNSO_NO_M            " & vbNewLine

#End Region

    ''' <summary>
    ''' 運賃タリフ承認チェック
    ''' </summary>
    Private Const SQL_SELECT_CHK_APPROVAL As String = "" _
        & " SELECT                                          " & vbNewLine _
        & "    NRS_BR_CD                                    " & vbNewLine _
        & "   ,UNCHIN_TARIFF_CD                             " & vbNewLine _
        & " FROM                                            " & vbNewLine _
        & "   $LM_MST$..M_UNCHIN_TARIFF                     " & vbNewLine _
        & " WHERE                                           " & vbNewLine _
        & "       NRS_BR_CD           =  @NRS_BR_CD         " & vbNewLine _
        & "   AND UNCHIN_TARIFF_CD    =  @UNCHIN_TARIFF_CD  " & vbNewLine _
        & "   AND APPROVAL_CD         <> '01'               " & vbNewLine _
        & "   AND SYS_DEL_FLG         =  '0'                " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_IN)
        Dim fromUnsoLL As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        fromUnsoLL = Me.GetUnsoLLSchema(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF040DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_1)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_2)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_3)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_4)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_5)

        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_IN)
        Dim fromUnsoLL As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        fromUnsoLL = Me.GetUnsoLLSchema(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF040DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_1)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_2)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_3)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_4)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMF040DAC.SQL_FROM_5)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Call Me.SetOrderBySQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_BY", Me.SetWhereData(Me._Row.Item("ORDER_BY").ToString(), LMF040DAC.ConditionPattern.OTHER), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        map.Add("SEIQ_FIXED_NM", "SEIQ_FIXED_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("SEIQTO_NM", "SEIQTO_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("UNSOCO_CD", "UNSOCO_CD")
        map.Add("UNSOCO_BR_CD", "UNSOCO_BR_CD")
        map.Add("UNSOCO_NM", "UNSOCO_NM")
        map.Add("UNSOCO_BR_NM", "UNSOCO_BR_NM")
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
        map.Add("SEIQ_TARIFF_BUNRUI_KB", "SEIQ_TARIFF_BUNRUI_KB")
        map.Add("TARIFF_BUNRUI", "TARIFF_BUNRUI")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("UNCHIN", "UNCHIN")
        map.Add("ITEM_CURR_CD", "ITEM_CURR_CD")
        map.Add("ROUND_POS", "ROUND_POS")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_NM", "TAX_NM")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQ_GROUP_NO_M", "SEIQ_GROUP_NO_M")
        map.Add("REMARK", "REMARK")
        map.Add("INOUTKA_NO_L", "INOUTKA_NO_L")
        map.Add("TRIP_NO", "TRIP_NO")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("MOTO_DATA_NM", "MOTO_DATA_NM")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("TYUKEI_HAISO_FLG", "TYUKEI_HAISO_FLG")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("GROUP_FLG", "GROUP_FLG")
        map.Add("CHK_UNCHIN", "CHK_UNCHIN")
        'START YANAI 要望番号974
        map.Add("SEIQ_UNCHIN", "SEIQ_UNCHIN")
        map.Add("SEIQ_CITY_EXTC", "SEIQ_CITY_EXTC")
        map.Add("SEIQ_WINT_EXTC", "SEIQ_WINT_EXTC")
        map.Add("SEIQ_RELY_EXTC", "SEIQ_RELY_EXTC")
        map.Add("SEIQ_TOLL", "SEIQ_TOLL")
        map.Add("SEIQ_INSU", "SEIQ_INSU")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        'END YANAI 要望番号974
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("MINASHI_DEST_CD", "MINASHI_DEST_CD")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("ZBUKA_CD", "ZBUKA_CD")
        map.Add("ABUKA_CD", "ABUKA_CD")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("SEIQ_SYARYO_KB", "SEIQ_SYARYO_KB")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応
        map.Add("DEST_ADDR", "DEST_ADDR")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF040DAC.TABLE_NM_OUT)

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

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

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("SKYU_DATE", "SKYU_DATE")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF040DAC.TABLE_NM_G_HED)

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
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_G_HED_CHK)

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

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_G_HED_CHK)

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

        If Me._Row.Item("SEIQ_TARIFF_BUNRUI_KB").ToString() = LMF040DAC.SEIQ_TARIFF_BUNRUI_KB_YOKOMOCHI Then
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

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader.Item("SKYU_DATE_COUNT")))
        reader.Close()

        Return ds

    End Function
    '要望番号:1045 terakawa 2013.03.28 End


    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '画面項目の反映
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_DATA", Me._Row.Item("ITEM_DATA").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_L", Me._Row.Item("CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_M", Me._Row.Item("CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_S", Me._Row.Item("CD_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_SS", Me._Row.Item("CD_SS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", Me._Row.Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", Me._Row.Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_FLG", Me._Row.Item("SYS_UPD_FLG").ToString(), DBDataType.CHAR))
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF040DAC.SQL_SELECT_INIT_UNCHIN, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQ_GROUP_NO_M", "SEIQ_GROUP_NO_M")
        map.Add("ITEM_DATA", "ITEM_DATA")
        map.Add("CD_L", "CD_L")
        map.Add("CD_M", "CD_M")
        map.Add("CD_S", "CD_S")
        map.Add("CD_SS", "CD_SS")
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        map.Add("ROW_NO", "ROW_NO")
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        map.Add("NEW_SYS_UPD_DATE", "NEW_SYS_UPD_DATE")
        map.Add("NEW_SYS_UPD_TIME", "NEW_SYS_UPD_TIME")
        map.Add("SYS_UPD_FLG", "SYS_UPD_FLG")
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF040DAC.TABLE_NM_UNCHIN)

        '取得できない場合、排他エラー
        Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        'START s.kobayashi 20140519 Notes2186
        Me._StrSql.Append(LMF040DAC.SQL_SELECT_UNCHIN)
        If (LMF040DAC.SHUSEI_KYORI.Equals(Me._Row.Item("ITEM_DATA").ToString())) Then
            Me._StrSql.Append(LMF040DAC.SQL_SELECT_UNCHIN_KYORI)
        End If
        Me._StrSql.Append(LMF040DAC.SQL_FROM_UNCHIN)
        If (LMF040DAC.SHUSEI_KYORI.Equals(Me._Row.Item("ITEM_DATA").ToString())) Then
            Me._StrSql.Append(LMF040DAC.SQL_FROM_UNCHIN_KYORI)
        End If
        'End s.kobayashi 20140519 Notes2186
        Me._StrSql.Append(LMF040DAC.SQL_WHERE_UNCHIN)
        Me._StrSql.Append(vbNewLine)

        If String.IsNullOrEmpty(Me._Row.Item("SEIQ_GROUP_NO").ToString()) = True Then

            '通常レコードの場合、運送番号
            Me._StrSql.Append(" AND F04_01.UNSO_NO_L = @UNSO_NO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND F04_01.UNSO_NO_M = @UNSO_NO_M ")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me._Row.Item("UNSO_NO_M").ToString(), DBDataType.CHAR))

        Else

            'まとめの場合
            Me._StrSql.Append(" AND F04_01.SEIQ_GROUP_NO = @SEIQ_GROUP_NO ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND F04_01.SEIQ_GROUP_NO_M = @SEIQ_GROUP_NO_M ")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", Me._Row.Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", Me._Row.Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))

        End If

        '並び順の設定
        Me._StrSql.Append(" ORDER BY F04_01.UNSO_NO_L , F04_01.UNSO_NO_M ")

        '画面項目の反映
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_DATA", Me._Row.Item("ITEM_DATA").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_L", Me._Row.Item("CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_M", Me._Row.Item("CD_M").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_S", Me._Row.Item("CD_S").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_SS", Me._Row.Item("CD_SS").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", Me._Row.Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", Me._Row.Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_FLG", Me._Row.Item("SYS_UPD_FLG").ToString(), DBDataType.CHAR))
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SEIQ_GROUP_NO", "SEIQ_GROUP_NO")
        map.Add("SEIQ_GROUP_NO_M", "SEIQ_GROUP_NO_M")
        map.Add("SEIQ_TARIFF_CD", "SEIQ_TARIFF_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SEIQ_ETARIFF_CD", "SEIQ_ETARIFF_CD")
        map.Add("SEIQ_NG_NB", "SEIQ_NG_NB")
        map.Add("SEIQ_KYORI", "SEIQ_KYORI")
        map.Add("SEIQ_WT", "SEIQ_WT")
        map.Add("SEIQ_UNCHIN", "SEIQ_UNCHIN")
        map.Add("SEIQ_CITY_EXTC", "SEIQ_CITY_EXTC")
        map.Add("SEIQ_WINT_EXTC", "SEIQ_WINT_EXTC")
        map.Add("SEIQ_RELY_EXTC", "SEIQ_RELY_EXTC")
        map.Add("SEIQ_TOLL", "SEIQ_TOLL")
        map.Add("SEIQ_INSU", "SEIQ_INSU")
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
        map.Add("SEIQ_SYARYO_KB", "SEIQ_SYARYO_KB")
        map.Add("SEIQ_PKG_UT", "SEIQ_PKG_UT")
        map.Add("SEIQ_DANGER_KB", "SEIQ_DANGER_KB")
        map.Add("REMARK", "REMARK")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("ITEM_DATA", "ITEM_DATA")
        map.Add("CD_L", "CD_L")
        map.Add("CD_M", "CD_M")
        map.Add("CD_S", "CD_S")
        map.Add("CD_SS", "CD_SS")
        map.Add("SEIQ_FIXED_FLAG", "SEIQ_FIXED_FLAG")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("SEIQ_TARIFF_BUNRUI_KB", "SEIQ_TARIFF_BUNRUI_KB")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_JIS", "DEST_JIS")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("SEIQTO_CD", "SEIQTO_CD")
        map.Add("ROW_NO", "ROW_NO")
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        map.Add("NEW_SYS_UPD_DATE", "NEW_SYS_UPD_DATE")
        map.Add("NEW_SYS_UPD_TIME", "NEW_SYS_UPD_TIME")
        map.Add("SYS_UPD_FLG", "SYS_UPD_FLG")
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        'START s.kobayashi 20140519 Notes2186
        If (LMF040DAC.SHUSEI_KYORI.Equals(Me._Row.Item("ITEM_DATA").ToString())) Then
            map.Add("CALC_KYORI", "CALC_KYORI")
        End If
        'End s.kobayashi 20140519 Notes2186
        map.Add("TABLE_TP", "TABLE_TP")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF040DAC.TABLE_NM_UNCHIN)

        '取得できない場合、排他エラー
        Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフ承認チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectChkApproval(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("CHK_APPROVAL")

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me._Row.Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNCHIN_TARIFF_CD", Me._Row.Item("UNCHIN_TARIFF_CD").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF040DAC.SQL_SELECT_CHK_APPROVAL, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'レコードクリア
        ds.Tables("CHK_APPROVAL").Rows.Clear()

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNCHIN_TARIFF_CD", "UNCHIN_TARIFF_CD")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, "CHK_APPROVAL")

        Return ds

    End Function

#End Region

#Region "設定処理"

#Region "UNSO_L"

    ''' <summary>
    ''' 運送(大)テーブル更新(システム項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoLSysData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'START YANAI 要望番号936
        ''SQL文のコンパイル
        'Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF040DAC.SQL_UPDATE_UNSO_L_SYS_DATETIME, Me._Row.Item("NRS_BR_CD").ToString()))
        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        'If (LMF040DAC.SHUSEI_TARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
        '    (LMF040DAC.SHUSEI_YOKO).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
        '    cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF040DAC.SQL_UPDATE_UNSO_L_TARIFF, Me._Row.Item("NRS_BR_CD").ToString()))
        '    'START YANAI 要望番号996
        'ElseIf (LMF040DAC.SHUSEI_ETARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
        '    cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF040DAC.SQL_UPDATE_UNSO_L_ETARIFF, Me._Row.Item("NRS_BR_CD").ToString()))
        '    'END YANAI 要望番号996
        'Else
        '    cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF040DAC.SQL_UPDATE_UNSO_L_SYS_DATETIME, Me._Row.Item("NRS_BR_CD").ToString()))
        'End If
        ''END YANAI 要望番号936
        If (LMF040DAC.SHUSEI_TARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
            (LMF040DAC.SHUSEI_YOKO).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF040DAC.SQL_UPDATE_UNSO_L_TARIFF)
        ElseIf (LMF040DAC.SHUSEI_ETARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF040DAC.SQL_UPDATE_UNSO_L_ETARIFF)
        Else
            Me._StrSql.Append(LMF040DAC.SQL_UPDATE_UNSO_L_SYS_DATETIME)
        End If

        If ("1").Equals(Me._Row.Item("SYS_UPD_FLG").ToString) = True Then
            '２回目以降の一括変更時
            Me._StrSql.Append(LMF040DAC.SQL_UPDATE_HAITA)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        cmd = MyBase.CreateSqlCommand(sql)
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)
        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        'Call Me.SetSysdataParameter(Me._SqlPrmList)
        If String.IsNullOrEmpty(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Call Me.SetSysdataParameter(Me._SqlPrmList)
        Else
            Call Me.SetSysdataHenkoParameter(Me._SqlPrmList)
        End If
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)
        'START YANAI 要望番号936
        'START YANAI 要望番号996
        'If (LMF040DAC.SHUSEI_TARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
        '    (LMF040DAC.SHUSEI_YOKO).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
        If (LMF040DAC.SHUSEI_TARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
            (LMF040DAC.SHUSEI_YOKO).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
            (LMF040DAC.SHUSEI_ETARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            'END YANAI 要望番号996
            Call Me.SetUnsoLTariffParameter(Me._Row, Me._SqlPrmList)
        End If
        'END YANAI 要望番号936

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, LMF040DAC.GROUP_ACTION.Equals(Me._Row.Item("ITEM_DATA").ToString()))

        Return ds

    End Function

#End Region

#Region "UNSO_M"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運送(中)テーブル更新(システム項目)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnsoM(ByVal ds As DataSet) As DataSet

        If (LMF040DAC.SHUSEI_ZBUKACD).Equals(Me._Row.Item("ITEM_DATA").ToString()) = False AndAlso _
            (LMF040DAC.SHUSEI_ABUKACD).Equals(Me._Row.Item("ITEM_DATA").ToString()) = False Then
            '在庫部課、扱い部課以外は更新対象外
            Return ds
        End If

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing
        If (LMF040DAC.SHUSEI_ZBUKACD).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF040DAC.SQL_UPDATE_UNSO_M_ZBUKACD, Me._Row.Item("NRS_BR_CD").ToString()))
        ElseIf (LMF040DAC.SHUSEI_ABUKACD).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            cmd = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMF040DAC.SQL_UPDATE_UNSO_M_ABUKACD, Me._Row.Item("NRS_BR_CD").ToString()))
        End If

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        If (LMF040DAC.SHUSEI_ZBUKACD).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Call Me.SetUnsoMzbukaCdParameter(Me._Row, Me._SqlPrmList)
        ElseIf (LMF040DAC.SHUSEI_ABUKACD).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Call Me.SetUnsoMabukaCdParameter(Me._Row, Me._SqlPrmList)
        End If

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, LMF040DAC.GROUP_ACTION.Equals(Me._Row.Item("ITEM_DATA").ToString()))

        Return ds

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

#End Region

#Region "UNCHIN"

    ''' <summary>
    ''' 運賃テーブル更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運賃テーブル更新SQLの構築・発行</remarks>
    Private Function UpdateUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..F_UNCHIN_TRS SET ")
        Call Me.SetUpdateUnchinSQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Me._StrSql.Append(LMF040DAC.SQL_UPDATE_UNCHIN_SYS_DATETIME)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)
        
        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, False)

        Return ds

    End Function

#End Region

#Region "運賃更新処理(再計算時)"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運送(大)データ件数検索(再計算時の排他用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)件数取得SQLの構築・発行</remarks>
    Private Function SelectDataSaikeisan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF040DAC.SQL_SELECT_COUNT_SAIKEISAN)
        Call Me.SetSelectUnsoLParameter(inTbl.Rows(0), Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()
        Return ds

    End Function

    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_UNCHIN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF040DAC.SQL_UPDATE_UNCHIN_SAIKEISAN) 'SQL構築(UPDATE句)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), inTbl.Rows(0).Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの初期化
        cmd.Parameters.Clear()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQLパラメータ（個別項目）設定
        Call Me.SetUpdUnchinParameter(inTbl.Rows(0), Me._SqlPrmList)

        'SQLパラメータ（システム項目）設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog("LMF040DAC", "UpdUnchinData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, False)

        Return ds

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

#End Region

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
        '要望管理2244対応
        Dim strMotoDataKbn As String = String.Empty
        With dr

            'Whereの固定値を設定
            sql.Append(LMF040DAC.SQL_WHERE)

            '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.SetWhereData(.Item("NRS_BR_CD").ToString(), LMF040DAC.ConditionPattern.OTHER), DBDataType.CHAR))

            '日付絞込
            Call Me.SetConditionDateSQL(prmList, dr, sql)

            'タリフコード
            whereStr = .Item("SEIQ_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SEIQ_TARIFF_CD LIKE @SEIQ_TARIFF_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '割増タリフコード
            whereStr = .Item("SEIQ_ETARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SEIQ_ETARIFF_CD LIKE @SEIQ_ETARIFF_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '乗務員コード
            whereStr = .Item("DRIVER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND ( F01_01.DRIVER_CD LIKE @DRIVER_CD OR F01_02.DRIVER_CD LIKE @DRIVER_CD OR F01_03.DRIVER_CD LIKE @DRIVER_CD OR F01_04.DRIVER_CD LIKE @DRIVER_CD )")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '荷主(大)コード
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_L = @CUST_CD_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主(中)コード
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_M = @CUST_CD_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M07_01.CUST_NM_L + '　' + M07_01.CUST_NM_M LIKE @CUST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '運賃区分
            whereStr = .Item("UNCHIN_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                '運賃区分 = '1'の場合トンキロ選択
                If LMConst.FLG.ON.Equals(whereStr) = True Then

                    sql.Append(" AND F04_01.SEIQ_TARIFF_BUNRUI_KB <> @UNCHIN_KBN")

                Else

                    sql.Append(" AND F04_01.SEIQ_TARIFF_BUNRUI_KB = @UNCHIN_KBN")

                End If

                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KBN", Me.SetWhereData(LMF040DAC.TARIFF_KURUMA, LMF040DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            'まとめ番号
            whereStr = .Item("GROUP_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                'まとめ番号 = '1'の場合まとめ済みを表示
                If LMConst.FLG.ON.Equals(whereStr) = True Then

                    sql.Append(" AND F04_01.SEIQ_GROUP_NO <> '' ")

                Else

                    sql.Append(" AND F04_01.SEIQ_GROUP_NO = '' ")

                End If

                sql.Append(vbNewLine)

            End If

            '請求運賃確定
            whereStr = .Item("KAKUTEI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_FIXED_FLAG = @KAKUTEI_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@KAKUTEI_KB", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            '元データ区分
            whereStr = .Item("MOTO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.MOTO_DATA_KB = @MOTO_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@MOTO_KB", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If
            '要望管理2244対応
            strMotoDataKbn = whereStr

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQTO_CD LIKE @SEIQTO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '請求先名
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M06_01.SEIQTO_NM LIKE @SEIQTO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                '要望管理2244対応
                If strMotoDataKbn = String.Empty Then
                    sql.Append(" AND (M10_01.DEST_NM LIKE @DEST_NM OR M10_03.DEST_NM LIKE @DEST_NM) ")
                ElseIf strMotoDataKbn = "10" Then
                    sql.Append(" AND M10_03.DEST_NM LIKE @DEST_NM")
                ElseIf strMotoDataKbn = "20" Then
                    sql.Append(" AND M10_01.DEST_NM LIKE @DEST_NM")
                Else
                    sql.Append(" AND M10_01.DEST_NM LIKE @DEST_NM")
                End If

                'sql.Append(" AND M10_01.DEST_NM LIKE @DEST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社名(1次)
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_01.UNSOCO_NM + '　' + M38_01.UNSOCO_BR_NM LIKE @UNSO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '2013.02.27 / Notes1897開始
            '便区分
            whereStr = .Item("BIN_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                Me._StrSql.Append(" AND KBN_05.KBN_CD = @BIN_KB")
                Me._StrSql.Append(vbNewLine)
                Me._SqlPrmList.Add(MyBase.GetSqlParameter("@BIN_KB", whereStr, DBDataType.CHAR))
            End If
            '2013.02.27 / Notes1897終了

            '運送会社名(2次)
            whereStr = .Item("UNSOCO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_02.UNSOCO_NM + '　' + M38_02.UNSOCO_BR_NM LIKE @UNSOCO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            'タリフ分類
            whereStr = .Item("TARIFF_BUNRUI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.TAX_KB = @TAX_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            'まとめ番号
            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_GROUP_NO LIKE @SEIQ_GROUP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            'まとめ番号M
            whereStr = .Item("SEIQ_GROUP_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_GROUP_NO_M LIKE @SEIQ_GROUP_NO_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '備考
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.REMARK LIKE @REMARK")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '管理番号
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.INOUTKA_NO_L LIKE @INOUTKA_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送番号L
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.UNSO_NO_L LIKE @UNSO_NO_L")
                sql.Append(vbNewLine)
                '要望番号1636:(運賃検索から明細ダブルクリック時、タイムアウトが発生) 2012/11/29 本明 START
                'prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.CHAR))
                '要望番号1636:(運賃検索から明細ダブルクリック時、タイムアウトが発生) 2012/11/29 本明 END


            End If

            '運送番号M
            whereStr = .Item("UNSO_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.UNSO_NO_M LIKE @UNSO_NO_M")
                sql.Append(vbNewLine)
                '要望番号1636:(運賃検索から明細ダブルクリック時、タイムアウトが発生) 2012/11/29 本明 START
                'prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.CHAR))
                '要望番号1636:(運賃検索から明細ダブルクリック時、タイムアウトが発生) 2012/11/29 本明 END

            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO LIKE @TRIP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '集荷中継地
            whereStr = .Item("SYUKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M12_01.KEN + M12_01.SHI LIKE @SYUKA_TYUKEI_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '配荷中継地
            whereStr = .Item("HAIKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M12_02.KEN + M12_02.SHI LIKE @HAIKA_TYUKEI_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_NM", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運行番号(集荷)
            whereStr = .Item("TRIP_NO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO_SYUKA LIKE @TRIP_NO_SYUKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運行番号(中継)
            whereStr = .Item("TRIP_NO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO_TYUKEI LIKE @TRIP_NO_TYUKEI")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運行番号(配荷)
            whereStr = .Item("TRIP_NO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO_HAIKA LIKE @TRIP_NO_HAIKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社名(集荷)
            whereStr = .Item("UNSOCO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_03.UNSOCO_NM + '　' + M38_03.UNSOCO_BR_NM LIKE @UNSOCO_SYUKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_SYUKA", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社名(中継)
            whereStr = .Item("UNSOCO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_04.UNSOCO_NM + '　' + M38_04.UNSOCO_BR_NM LIKE @UNSOCO_TYUKEI")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_TYUKEI", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社名(集荷)
            whereStr = .Item("UNSOCO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_05.UNSOCO_NM + '　' + M38_05.UNSOCO_BR_NM LIKE @UNSOCO_HAIKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_HAIKA", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                '要望管理2244対応
                If strMotoDataKbn = String.Empty Then
                    sql.Append(" AND (F02_01.DEST_CD LIKE @DEST_CD OR F02_01.ORIG_CD LIKE @DEST_CD) ")
                ElseIf strMotoDataKbn = "10" Then
                    sql.Append(" AND F02_01.ORIG_CD LIKE @DEST_CD")
                ElseIf strMotoDataKbn = "20" Then
                    sql.Append(" AND F02_01.DEST_CD LIKE @DEST_CD")
                Else
                    sql.Append(" AND F02_01.DEST_CD LIKE @DEST_CD")
                End If
                'sql.Append(" AND F02_01.DEST_CD LIKE @DEST_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '届先JIS
            whereStr = .Item("DEST_JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M10_01.JIS LIKE @DEST_JIS_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社コード(1次)
            whereStr = .Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.UNSO_CD LIKE @UNSO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社支店コード(1次)
            whereStr = .Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.UNSO_BR_CD LIKE @UNSO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If



            '運送会社コード(2次)
            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F01_01.UNSOCO_CD LIKE @UNSOCO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社支店コード(2次)
            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F01_01.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            'START YANAI 20120622 DIC運賃まとめ及び再計算対応
            '伝票№
            whereStr = .Item("CUST_REF_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.CUST_REF_NO LIKE @CUST_REF_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '在庫部課
            whereStr = .Item("ZBUKA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND UNSOM.ZBUKA_CD LIKE @ZBUKA_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '扱い部課
            whereStr = .Item("ABUKA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND UNSOM.ABUKA_CD LIKE @ABUKA_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", Me.SetWhereData(whereStr, LMF040DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If
            'END YANAI 20120622 DIC運賃まとめ及び再計算対応
                
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

                Case LMF040DAC.DATE_KBN_NONYU

                    fromCondition = " AND F02_01.ARR_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.ARR_PLAN_DATE <= @TO_DATE "

                Case LMF040DAC.DATE_KBN_SHUKKA

                    fromCondition = " AND F02_01.OUTKA_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.OUTKA_PLAN_DATE <= @TO_DATE "

            End Select

            If String.IsNullOrEmpty(fromDate) = False Then
                sql.Append(fromCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@FROM_DATE", Me.SetWhereData(fromDate, LMF040DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            If String.IsNullOrEmpty(toDate) = False Then
                sql.Append(toCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TO_DATE", Me.SetWhereData(toDate, LMF040DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

        End With

    End Sub

    ''' <summary>
    ''' まとめ条件の設定
    ''' </summary>
    ''' <param name="sql">SQL</param>
    ''' <param name="value">追加する文</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="max">リストの件数</param>
    ''' <remarks></remarks>
    Private Sub SetGroupData(ByVal sql As StringBuilder, ByVal value As String, ByVal arr As ArrayList, ByVal max As Integer)

        sql.Append(value)
        For i As Integer = 0 To max
            sql.Append(arr(i).ToString())
        Next

    End Sub

    ''' <summary>
    ''' 条件文・パラメータ設定モジュール
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetOrderBySQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        Dim orderBy As String = dr.Item("ORDER_BY").ToString()
        If String.IsNullOrEmpty(orderBy) = True Then

            'START YANAI 要望番号641
            ''並び順を選択していない場合、標準の並び順
            'sql.Append(" ORDER BY  F04_01.NRS_BR_CD ")
            'sql.Append(vbNewLine)
            ''START YANAI 要望番号632
            ''sql.Append("          ,F04_01.SEIQTO_CD ")
            ''sql.Append(vbNewLine)
            'sql.Append("          ,F02_01.MOTO_DATA_KB ")
            'sql.Append(vbNewLine)
            ''END YANAI 要望番号632
            'sql.Append("          ,F02_01.CUST_CD_L ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F02_01.CUST_CD_M ")
            ''START YANAI 要望番号632
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.CUST_CD_S ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.CUST_CD_SS ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.SEIQTO_CD ")
            ''END YANAI 要望番号632
            ''START YANAI 要望番号576
            'sql.Append(vbNewLine)
            'sql.Append("          ,F02_01.OUTKA_PLAN_DATE ")
            ''END YANAI 要望番号576
            'sql.Append(vbNewLine)
            'sql.Append("          ,F02_01.DEST_CD ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.SEIQ_GROUP_NO ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.SEIQ_GROUP_NO_M ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.UNSO_NO_L ")
            'sql.Append(vbNewLine)
            'sql.Append("          ,F04_01.UNSO_NO_M ")
            'sql.Append(vbNewLine)

            '並び順を選択していない場合、標準の並び順
            If String.IsNullOrEmpty(dr.Item("CUST_CD_M").ToString()) = False Then
                '荷主コード(中)が設定されている場合
                sql.Append(" ORDER BY  F04_01.NRS_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.MOTO_DATA_KB ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.CUST_CD_S ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.CUST_CD_SS ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SEIQTO_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.OUTKA_PLAN_DATE ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.DEST_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SEIQ_GROUP_NO ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SEIQ_GROUP_NO_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.UNSO_NO_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.UNSO_NO_M ")
                sql.Append(vbNewLine)
            Else
                '荷主コード(中)が設定されていない場合
                sql.Append(" ORDER BY  F04_01.NRS_BR_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.MOTO_DATA_KB ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.OUTKA_PLAN_DATE ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.CUST_CD_S ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.CUST_CD_SS ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SEIQTO_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.DEST_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SEIQ_GROUP_NO ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SEIQ_GROUP_NO_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.UNSO_NO_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.UNSO_NO_M ")
                sql.Append(vbNewLine)
            End If
            'END YANAI 要望番号641

            Exit Sub

        End If

        '並び順を選択している場合、まとめ候補順に設定
        sql.Append(" ORDER BY  F02_01.OUTKA_PLAN_DATE ")
        sql.Append(vbNewLine)
        sql.Append("          ,F02_01.ARR_PLAN_DATE ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.SEIQTO_CD ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.SEIQ_TARIFF_CD ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.SEIQ_ETARIFF_CD ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.TAX_KB ")
        sql.Append(vbNewLine)

        Select Case dr.Item("ORDER_BY").ToString()

            Case LMF040DAC.ORDER_BY_CUSTTRIP

                sql.Append("          ,F02_01.CUST_CD_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.TYUKEI_HAISO_FLG ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.TRIP_NO ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.TRIP_NO_SYUKA ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.TRIP_NO_TYUKEI ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.TRIP_NO_HAIKA ")
                sql.Append(vbNewLine)

            Case LMF040DAC.ORDER_BY_DEST

                sql.Append("          ,F02_01.DEST_CD ")
                sql.Append(vbNewLine)

            Case LMF040DAC.ORDER_BY_DESTJIS

                sql.Append("          ,M10_01.JIS ")
                sql.Append(vbNewLine)

        End Select

    End Sub

    ''' <summary>
    ''' 抽出条件設定
    ''' </summary>
    ''' <param name="whereStr">条件の文字</param>
    ''' <param name="ptn">抽出パターン</param>
    ''' <returns>文字</returns>
    ''' <remarks></remarks>
    Private Function SetWhereData(ByVal whereStr As String, ByVal ptn As LMF040DAC.ConditionPattern) As String

        SetWhereData = String.Empty

        Select Case ptn

            Case LMF040DAC.ConditionPattern.PRE

                SetWhereData = String.Concat(whereStr, "%")

            Case LMF040DAC.ConditionPattern.ALL

                SetWhereData = String.Concat("%", whereStr, "%")

            Case LMF040DAC.ConditionPattern.OTHER

                SetWhereData = whereStr

        End Select

        Return SetWhereData

    End Function

    ''' <summary>
    ''' 区分マスタ取得
    ''' </summary>
    ''' <param name="kbnGrpCd">String</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnData(ByVal ds As DataSet, ByVal kbnGrpCd As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF040DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", kbnGrpCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF040DAC.SQL_SELECT_KBN_DATA, brCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF040DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF040DAC.TABLE_NM_KBN)

        Return ds

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
        If ds.Tables(LMF040DAC.TABLE_NM_KBN).Rows.Count = 0 Then
            rtnFrom = "$LM_TRN$..F_UNSO_LL"
            Return rtnFrom
        End If

        '０件ではない場合、またぎ営業所のスキーマを設定
        Dim matagiBr As String = String.Empty
        rtnFrom = "(SELECT * FROM $LM_TRN$..F_UNSO_LL "
        For i As Integer = 0 To ds.Tables(LMF040DAC.TABLE_NM_KBN).Rows.Count - 1
            matagiBr = ds.Tables(LMF040DAC.TABLE_NM_KBN).Rows(i).Item("KBN_NM2").ToString()
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
    ''' 運送(大)PK
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLPkParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))

        End With

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

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
    ''' <summary>
    ''' 更新時の共通パラメータ設定(一括変更時)
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataHenkoParameter(ByVal prmList As ArrayList)

        'システム項目
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.NVARCHAR))

    End Sub
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

    'START YANAI 要望番号936
    ''' <summary>
    ''' 運送(大)タリフコード
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLTariffParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            'START YANAI 要望番号996
            'prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
            'If (LMF040DAC.SHUSEI_TARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", "10", DBDataType.CHAR))
            'ElseIf (LMF040DAC.SHUSEI_YOKO).Equals(.Item("ITEM_DATA").ToString()) = True Then
            '    prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", "40", DBDataType.CHAR))
            'End If
            If (LMF040DAC.SHUSEI_TARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", "10", DBDataType.CHAR))
            ElseIf (LMF040DAC.SHUSEI_YOKO).Equals(.Item("ITEM_DATA").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", "40", DBDataType.CHAR))
            ElseIf (LMF040DAC.SHUSEI_ETARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
            End If
            'END YANAI 要望番号996

        End With

    End Sub
    'END YANAI 要望番号936

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運送(中)在庫部課
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMzbukaCdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", .Item("CD_L").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運送(中)扱い部課
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoMabukaCdParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", .Item("CD_L").ToString(), DBDataType.NVARCHAR))

        End With

    End Sub
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSelectUnsoLParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", .Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", .Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUpdUnchinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))

        End With

    End Sub
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

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
    ''' <param name="setFlg">エラーセットフラグ　True:通常のメッセージセット　False:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cmd As SqlCommand, ByVal setFlg As Boolean) As Boolean

        Return Me.UpdateResultChk(MyBase.GetUpdateResult(cmd), setFlg)

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="cnt">カウント</param>
    ''' <param name="setFlg">エラーセットフラグ　True:通常のメッセージセット　False:一括更新のメッセージセット</param>
    ''' <returns>True:エラーなし,OK False:排他エラー(E011)</returns>
    ''' <remarks></remarks>
    Private Function UpdateResultChk(ByVal cnt As Integer, ByVal setFlg As Boolean) As Boolean

        '判定
        If cnt < 1 Then
            If setFlg = True Then
                MyBase.SetMessage("E011")
            Else
                '前ゼロ対応
                MyBase.SetMessageStore(LMF040DAC.GUIDANCE_KBN, "E011", , Convert.ToInt32(Me._Row.Item("ROW_NO")).ToString())
            End If
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 運賃更新のSQL構築
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateUnchinSQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        With dr
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
            Dim sMATOME_REMARK_UPNG_FLG As String = .Item("MATOME_REMARK_UPNG_FLG").ToString

#End If
            sql.Append(vbNewLine)

            Select Case .Item("ITEM_DATA").ToString()

                Case LMF040DAC.FIX_ACTION, LMF040DAC.FIX_CANCELL_ACTION

                    sql.Append(" SEIQ_FIXED_FLAG        = @SEIQ_FIXED_FLAG ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_FIXED_FLAG", .Item("SEIQ_FIXED_FLAG").ToString(), DBDataType.CHAR))

                Case LMF040DAC.GROUP_ACTION

                    sql.Append(" SEIQ_GROUP_NO          = @SEIQ_GROUP_NO ")
                    sql.Append(vbNewLine)
                    sql.Append(",SEIQ_GROUP_NO_M        = @SEIQ_GROUP_NO_M ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", .Item("SEIQ_GROUP_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", .Item("SEIQ_GROUP_NO_M").ToString(), DBDataType.CHAR))

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
                    If ("1").Equals(sMATOME_REMARK_UPNG_FLG) = False Then
                        '備考の設定
                        sql.Append(",REMARK                 = @REMARK ")
                        sql.Append(vbNewLine)
                        prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

                    End If
#End If

                Case LMF040DAC.GROUP_CANCELL_ACTION

                    sql.Append(" SEIQ_GROUP_NO          = @SEIQ_GROUP_NO ")
                    sql.Append(vbNewLine)
                    sql.Append(",SEIQ_GROUP_NO_M        = @SEIQ_GROUP_NO_M ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", String.Empty, DBDataType.CHAR))

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)

                    'その他の設定
                    sql.Append(",SEIQ_TARIFF_CD         = @SEIQ_TARIFF_CD ")
                    sql.Append(vbNewLine)
                    sql.Append(",SEIQ_ETARIFF_CD        = @SEIQ_ETARIFF_CD ")
                    sql.Append(vbNewLine)
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
                    If ("1").Equals(sMATOME_REMARK_UPNG_FLG) = False Then
                        sql.Append(",REMARK                 = @REMARK ")
                        sql.Append(vbNewLine)
                    End If
#End If
                    sql.Append(",TAX_KB                 = @TAX_KB ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("SEIQ_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("SEIQ_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    Dim groupL As String = .Item("SEIQ_GROUP_NO").ToString()
                    Dim groupM As String = .Item("SEIQ_GROUP_NO_M").ToString()
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
                    If ("1").Equals(sMATOME_REMARK_UPNG_FLG) = False Then
                        '親レコードの場合、初期化
                        If .Item("SEIQ_GROUP_NO").ToString().Equals(.Item("UNSO_NO_L").ToString()) = True _
                        AndAlso .Item("SEIQ_GROUP_NO_M").ToString().Equals(.Item("UNSO_NO_M").ToString()) = True _
                        Then
                            prmList.Add(MyBase.GetSqlParameter("@REMARK", String.Empty, DBDataType.NVARCHAR))
                        Else
                            prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
                        End If

                    End If
#End If

                    prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))

                Case LMF040DAC.SHUSEI_SEIQTO

                    sql.Append(" SEIQTO_CD              = @SEIQTO_CD ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", .Item("CD_L").ToString(), DBDataType.CHAR))

                Case LMF040DAC.SHUSEI_TARIFF, LMF040DAC.SHUSEI_YOKO

                    sql.Append(" SEIQ_TARIFF_CD         = @SEIQ_TARIFF_CD ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))

                    'START YANAI 要望番号936
                    If (LMF040DAC.SHUSEI_TARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
                        sql.Append(" ,SEIQ_TARIFF_BUNRUI_KB         = '10' ")
                        sql.Append(vbNewLine)
                    ElseIf (LMF040DAC.SHUSEI_YOKO).Equals(.Item("ITEM_DATA").ToString()) = True Then
                        sql.Append(" ,SEIQ_TARIFF_BUNRUI_KB         = '40' ")
                        sql.Append(vbNewLine)
                    End If
                    'END YANAI 要望番号936

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)

                    'START YANAI 要望番号996
                Case LMF040DAC.SHUSEI_ETARIFF

                    sql.Append(" SEIQ_ETARIFF_CD        = @SEIQ_ETARIFF_CD ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)
                    'END YANAI 要望番号996

                Case LMF040DAC.SHUSEI_CUST

                    sql.Append(" CUST_CD_S              = @CUST_CD_S ")
                    sql.Append(vbNewLine)
                    sql.Append(",CUST_CD_SS             = @CUST_CD_SS ")
                    sql.Append(vbNewLine)
                    sql.Append(",UNTIN_CALCULATION_KB   = @UNTIN_CALCULATION_KB ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_S", .Item("CD_S").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@CUST_CD_SS", .Item("CD_SS").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", .Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))

                    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
                Case LMF040DAC.SHUSEI_ZBUKACD, LMF040DAC.SHUSEI_ABUKACD
                    sql.Append(" SYS_DEL_FLG = SYS_DEL_FLG ") '更新する項目がないけど、何かを設定しなければならないので、削除フラグを設定。
                    sql.Append(vbNewLine)
                    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

                    'START s.kobayashi 20140519 距離再取得
                Case LMF040DAC.SHUSEI_KYORI
                    sql.Append(" SEIQ_KYORI        = @CALC_KYORI ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_KYORI        = @CALC_KYORI ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@CALC_KYORI", .Item("CALC_KYORI").ToString(), DBDataType.CHAR))

                    '距離のみ変更で運賃の再計算は行わない
                    'End s.kobayashi 20140519 距離再取得

            End Select

            'PKの設定
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", .Item("UNSO_NO_M").ToString(), DBDataType.CHAR))

        End With

    End Sub

    ''' <summary>
    ''' 数値系の設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub SetUpdateKingakuSQL(ByVal prmList As ArrayList, ByVal dr As DataRow, ByVal sql As StringBuilder)

        sql.Append(",DECI_NG_NB             = @DECI_NG_NB ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_KYORI             = @DECI_KYORI ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_WT                = @DECI_WT ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_UNCHIN            = @DECI_UNCHIN ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_CITY_EXTC         = @DECI_CITY_EXTC ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_WINT_EXTC         = @DECI_WINT_EXTC ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_RELY_EXTC         = @DECI_RELY_EXTC ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_TOLL              = @DECI_TOLL ")
        sql.Append(vbNewLine)
        sql.Append(",DECI_INSU              = @DECI_INSU ")
        sql.Append(vbNewLine)
        sql.Append(",KANRI_UNCHIN           = @KANRI_UNCHIN ")
        sql.Append(vbNewLine)
        sql.Append(",KANRI_CITY_EXTC        = @KANRI_CITY_EXTC ")
        sql.Append(vbNewLine)
        sql.Append(",KANRI_WINT_EXTC        = @KANRI_WINT_EXTC ")
        sql.Append(vbNewLine)
        sql.Append(",KANRI_RELY_EXTC        = @KANRI_RELY_EXTC ")
        sql.Append(vbNewLine)
        sql.Append(",KANRI_TOLL             = @KANRI_TOLL ")
        sql.Append(vbNewLine)
        sql.Append(",KANRI_INSU             = @KANRI_INSU ")
        sql.Append(vbNewLine)

        With dr

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

        End With

    End Sub

#End Region

#End Region

End Class
