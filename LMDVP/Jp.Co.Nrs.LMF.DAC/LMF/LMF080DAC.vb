' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF080    : 支払検索
'  作  成  者       :  YANAI
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF080DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF080DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF080IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMF080OUT"

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
    ''' OUT_ANBUNテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT_ANBUN As String = "LMF080OUT_ANBUN"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMF080DAC"

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

    ''' <summary>
    ''' 確定
    ''' </summary>
    ''' <remarks></remarks>
    Private Const FIX_ACTION As String = "01"
    Private Const FIX_ACTION_ANBUN As String = "01A"

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
    ''' 修正項目(支払先)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_SHIHARAI As String = "05"

    ''' <summary>
    ''' 修正項目(支払先タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_TARIFF As String = "06"

    ''' <summary>
    ''' 修正項目(支払先横持ち)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_YOKO As String = "07"

    ''' <summary>
    ''' 修正項目(割増タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_ETARIFF As String = "08"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GUIDANCE_KBN As String = "00"

#End Region

#Region "検索処理 SQL"

#Region "Select句"

    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(F04_01.UNSO_NO_L)                     AS REC_CNT       " & vbNewLine

    'START YANAI 要望番号1424 支払処理
    'Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
    '                                        & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
    '                                        & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_FIXED_FLAG      AS SHIHARAI_FIXED_FLAG  " & vbNewLine _
    '                                        & ",KBN_01.KBN_NM8                  AS SHIHARAI_FIXED_NM    " & vbNewLine _
    '                                        & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
    '                                        & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
    '                                        & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
    '                                        & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
    '                                        & ",F04_01.SHIHARAITO_CD            AS SHIHARAITO_CD        " & vbNewLine _
    '                                        & ",M06_01.SHIHARAITO_NM            AS SHIHARAITO_NM        " & vbNewLine _
    '                                        & ",M06_01.SHIHARAITO_BUSYO_NM      AS SHIHARAITO_BUSYO_NM  " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN F02_01.ORIG_CD                                " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN F02_01.DEST_CD                                " & vbNewLine _
    '                                        & "      ELSE F02_01.DEST_CD                                " & vbNewLine _
    '                                        & " END                             AS DEST_CD              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.DEST_NM,M10_04.DEST_NM)         " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20' AND OUTKAL.DEST_KB = '02' " & vbNewLine _
    '                                        & "      THEN OUTKAEDIL.DEST_NM                             " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.DEST_NM,M10_02.DEST_NM)         " & vbNewLine _
    '                                        & " END                             AS DEST_NM              " & vbNewLine _
    '                                        & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_03.JIS,M10_04.JIS)                 " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20' AND OUTKAL.DEST_KB = '02' " & vbNewLine _
    '                                        & "      THEN OUTKAEDIL.DEST_JIS_CD                         " & vbNewLine _
    '                                        & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
    '                                        & "      THEN ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & "      ELSE ISNULL(M10_01.JIS,M10_02.JIS)                 " & vbNewLine _
    '                                        & " END                             AS DEST_JIS_CD          " & vbNewLine _
    '                                        & ",ISNULL(F01_01.UNSOCO_CD, F02_01.UNSO_CD) AS UNSO_CD     " & vbNewLine _
    '                                        & ",ISNULL(F01_01.UNSOCO_BR_CD, F02_01.UNSO_BR_CD) AS UNSO_BR_CD " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
    '                                        & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
    '                                        & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_TARIFF_BUNRUI_KB AS SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_TARIFF_CD       AS SHIHARAI_TARIFF_CD   " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_ETARIFF_CD      AS SHIHARAI_ETARIFF_CD  " & vbNewLine _
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
    '                                        & ",F04_01.SHIHARAI_GROUP_NO        AS SHIHARAI_GROUP_NO    " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_GROUP_NO_M      AS SHIHARAI_GROUP_NO_M  " & vbNewLine _
    '                                        & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
    '                                        & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
    '                                        & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
    '                                        & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
    '                                        & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
    '                                        & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
    '                                        & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
    '                                        & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
    '                                        & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
    '                                        & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
    '                                        & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
    '                                        & "                THEN '1'                                 " & vbNewLine _
    '                                        & "                ELSE '0'                                 " & vbNewLine _
    '                                        & " END                             AS      GROUP_FLG       " & vbNewLine _
    '                                        & ",CASE WHEN RTRIM(F04_01.SHIHARAI_GROUP_NO) = ''          " & vbNewLine _
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
    '                                        & ",ISNULL(F04_01.SHIHARAI_UNCHIN,0) AS SHIHARAI_UNCHIN     " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SHIHARAI_CITY_EXTC,0) AS SHIHARAI_CITY_EXTC " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SHIHARAI_WINT_EXTC,0) AS SHIHARAI_WINT_EXTC " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SHIHARAI_RELY_EXTC,0) AS SHIHARAI_RELY_EXTC " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SHIHARAI_TOLL,0)  AS SHIHARAI_TOLL        " & vbNewLine _
    '                                        & ",ISNULL(F04_01.SHIHARAI_INSU,0)  AS SHIHARAI_INSU        " & vbNewLine _
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
    '                                        & ",F04_01.DECI_NG_NB               AS DECI_NG_NB           " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_PKG_UT          AS SHIHARAI_PKG_UT      " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_SYARYO_KB       AS SHIHARAI_SYARYO_KB   " & vbNewLine _
    '                                        & ",F04_01.SHIHARAI_DANGER_KB       AS SHIHARAI_DANGER_KB   " & vbNewLine
    Private Const SQL_SELECT_DATA As String = "SELECT                                                   " & vbNewLine _
                                            & " F04_01.NRS_BR_CD                AS NRS_BR_CD            " & vbNewLine _
                                            & ",F04_01.UNSO_NO_L                AS UNSO_NO_L            " & vbNewLine _
                                            & ",F04_01.UNSO_NO_M                AS UNSO_NO_M            " & vbNewLine _
                                            & ",F04_01.SHIHARAI_FIXED_FLAG      AS SHIHARAI_FIXED_FLAG  " & vbNewLine _
                                            & ",KBN_01.KBN_NM8                  AS SHIHARAI_FIXED_NM    " & vbNewLine _
                                            & ",F02_01.OUTKA_PLAN_DATE          AS OUTKA_PLAN_DATE      " & vbNewLine _
                                            & ",F02_01.ARR_PLAN_DATE            AS ARR_PLAN_DATE        " & vbNewLine _
                                            & ",M07_01.CUST_NM_L                AS CUST_NM_L            " & vbNewLine _
                                            & ",M07_01.CUST_NM_M                AS CUST_NM_M            " & vbNewLine _
                                            & ",F04_01.UNTIN_CALCULATION_KB     AS UNTIN_CALCULATION_KB " & vbNewLine _
                                            & ",F04_01.SHIHARAITO_CD            AS SHIHARAITO_CD        " & vbNewLine _
                                            & ",M06_01.SHIHARAITO_NM            AS SHIHARAITO_NM        " & vbNewLine _
                                            & ",M06_01.SHIHARAITO_BUSYO_NM      AS SHIHARAITO_BUSYO_NM  " & vbNewLine _
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
                                            & ",ISNULL(F01_01.UNSOCO_CD, F02_01.UNSO_CD) AS UNSO_CD     " & vbNewLine _
                                            & ",ISNULL(F01_01.UNSOCO_BR_CD, F02_01.UNSO_BR_CD) AS UNSO_BR_CD " & vbNewLine _
                                            & ",M38_01.UNSOCO_NM                AS UNSO_NM              " & vbNewLine _
                                            & ",M38_01.UNSOCO_BR_NM             AS UNSO_BR_NM           " & vbNewLine _
                                            & ",KBN_02.KBN_NM1                  AS TARIFF_BUNRUI        " & vbNewLine _
                                            & ",F04_01.SHIHARAI_TARIFF_BUNRUI_KB AS SHIHARAI_TARIFF_BUNRUI_KB" & vbNewLine _
                                            & ",F04_01.SHIHARAI_TARIFF_CD       AS SHIHARAI_TARIFF_CD   " & vbNewLine _
                                            & ",F04_01.SHIHARAI_ETARIFF_CD      AS SHIHARAI_ETARIFF_CD  " & vbNewLine _
                                            & ",F04_01.DECI_WT                  AS DECI_WT              " & vbNewLine _
                                            & ",F04_01.DECI_KYORI               AS DECI_KYORI           " & vbNewLine _
                                            & ",F04_01.DECI_UNCHIN                                      " & vbNewLine _
                                            & " + F04_01.DECI_CITY_EXTC                                 " & vbNewLine _
                                            & " + F04_01.DECI_WINT_EXTC                                 " & vbNewLine _
                                            & " + F04_01.DECI_RELY_EXTC                                 " & vbNewLine _
                                            & " + F04_01.DECI_TOLL                                      " & vbNewLine _
                                            & " + F04_01.DECI_INSU              AS UNCHIN               " & vbNewLine _
                                            & ",F04_01.TAX_KB                   AS TAX_KB               " & vbNewLine _
                                            & ",KBN_03.KBN_NM1                  AS TAX_NM               " & vbNewLine _
                                            & ",F04_01.SHIHARAI_GROUP_NO        AS SHIHARAI_GROUP_NO    " & vbNewLine _
                                            & ",F04_01.SHIHARAI_GROUP_NO_M      AS SHIHARAI_GROUP_NO_M  " & vbNewLine _
                                            & ",F04_01.REMARK                   AS REMARK               " & vbNewLine _
                                            & ",F02_01.INOUTKA_NO_L             AS INOUTKA_NO_L         " & vbNewLine _
                                            & ",F02_01.TRIP_NO                  AS TRIP_NO              " & vbNewLine _
                                            & ",F02_01.MOTO_DATA_KB             AS MOTO_DATA_KB         " & vbNewLine _
                                            & ",KBN_04.KBN_NM1                  AS MOTO_DATA_NM         " & vbNewLine _
                                            & ",F02_01.CUST_CD_L                AS CUST_CD_L            " & vbNewLine _
                                            & ",F02_01.CUST_CD_M                AS CUST_CD_M            " & vbNewLine _
                                            & ",F02_01.VCLE_KB                  AS VCLE_KB              " & vbNewLine _
                                            & ",F02_01.UNSO_ONDO_KB             AS UNSO_ONDO_KB         " & vbNewLine _
                                            & ",F04_01.SIZE_KB                  AS SIZE_KB              " & vbNewLine _
                                            & ",F02_01.SYS_UPD_DATE             AS SYS_UPD_DATE         " & vbNewLine _
                                            & ",F02_01.SYS_UPD_TIME             AS SYS_UPD_TIME         " & vbNewLine _
                                            & ",CASE @ORDER_BY WHEN ''                                  " & vbNewLine _
                                            & "                THEN '1'                                 " & vbNewLine _
                                            & "                ELSE '0'                                 " & vbNewLine _
                                            & " END                             AS      GROUP_FLG       " & vbNewLine _
                                            & ",CASE WHEN RTRIM(F04_01.SHIHARAI_GROUP_NO) = ''          " & vbNewLine _
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
                                            & ",ISNULL(F04_01.SHIHARAI_UNCHIN,0) AS SHIHARAI_UNCHIN     " & vbNewLine _
                                            & ",ISNULL(F04_01.SHIHARAI_CITY_EXTC,0) AS SHIHARAI_CITY_EXTC " & vbNewLine _
                                            & ",ISNULL(F04_01.SHIHARAI_WINT_EXTC,0) AS SHIHARAI_WINT_EXTC " & vbNewLine _
                                            & ",ISNULL(F04_01.SHIHARAI_RELY_EXTC,0) AS SHIHARAI_RELY_EXTC " & vbNewLine _
                                            & ",ISNULL(F04_01.SHIHARAI_TOLL,0)  AS SHIHARAI_TOLL        " & vbNewLine _
                                            & ",ISNULL(F04_01.SHIHARAI_INSU,0)  AS SHIHARAI_INSU        " & vbNewLine _
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
                                            & ",F04_01.DECI_NG_NB               AS DECI_NG_NB           " & vbNewLine _
                                            & ",F04_01.SHIHARAI_PKG_UT          AS SHIHARAI_PKG_UT      " & vbNewLine _
                                            & ",F04_01.SHIHARAI_SYARYO_KB       AS SHIHARAI_SYARYO_KB   " & vbNewLine _
                                            & ",F04_01.SHIHARAI_DANGER_KB       AS SHIHARAI_DANGER_KB   " & vbNewLine _
                                            & ",CASE WHEN F02_01.MOTO_DATA_KB = '10'                    " & vbNewLine _
                                            & "      THEN M10_03.SHIHARAI_AD                            " & vbNewLine _
                                            & "      WHEN F02_01.MOTO_DATA_KB = '20'                    " & vbNewLine _
                                            & "      THEN M10_01.SHIHARAI_AD                            " & vbNewLine _
                                            & "      ELSE M10_01.SHIHARAI_AD                            " & vbNewLine _
                                            & " END                             AS DEST_AD              " & vbNewLine _
                                            & ",F01_01.SHIHARAI_UNCHIN          AS SHIHARAI_UNCHIN_UNSOLL " & vbNewLine

    'END YANAI 要望番号1424 支払処理

#End Region

#Region "From句"

    'START YANAI 要望番号1424 支払処理
    'Private Const SQL_FROM As String = " FROM      $LM_TRN$..F_SHIHARAI_TRS    F04_01          " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
    '                             & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
    '                             & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_01                " & vbNewLine _
    '                             & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
    '                             & "  AND F01_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_TRN$..F_SHIHARAI_TRS  F04_02                " & vbNewLine _
    '                             & "   ON F04_01.SHIHARAI_GROUP_NO     = F04_02.UNSO_NO_L      " & vbNewLine _
    '                             & "  AND F04_01.SHIHARAI_GROUP_NO_M   = F04_02.UNSO_NO_M      " & vbNewLine _
    '                             & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & "    LEFT JOIN $LM_TRN$..C_OUTKA_L    OUTKAL                " & vbNewLine _
    '                             & "  ON OUTKAL.NRS_BR_CD             = F02_01.NRS_BR_CD       " & vbNewLine _
    '                             & " AND OUTKAL.OUTKA_NO_L            = F02_01.INOUTKA_NO_L    " & vbNewLine _
    '                             & " AND F02_01.MOTO_DATA_KB          = '20'                   " & vbNewLine _
    '                             & " AND OUTKAL.SYS_DEL_FLG           = '0'                    " & vbNewLine _
    '                             & "   LEFT JOIN (SELECT NRS_BR_CD,MIN(EDI_CTL_NO) AS EDI_CTL_NO " & vbNewLine _
    '                             & "           ,OUTKA_CTL_NO                                   " & vbNewLine _
    '                             & "           FROM  $LM_TRN$..H_OUTKAEDI_L WHERE DEL_KB = '0' " & vbNewLine _
    '                             & "          GROUP BY NRS_BR_CD,OUTKA_CTL_NO                  " & vbNewLine _
    '                             & " )  OUTKAEDIL_SUM                                          " & vbNewLine _
    '                             & "   ON OUTKAEDIL_SUM.NRS_BR_CD          = OUTKAL.NRS_BR_CD  " & vbNewLine _
    '                             & "  AND OUTKAEDIL_SUM.OUTKA_CTL_NO       = OUTKAL.OUTKA_NO_L " & vbNewLine _
    '                             & "    LEFT JOIN $LM_TRN$..H_OUTKAEDI_L AS OUTKAEDIL          " & vbNewLine _
    '                             & " ON OUTKAEDIL_SUM.NRS_BR_CD          = OUTKAEDIL.NRS_BR_CD " & vbNewLine _
    '                             & "  AND OUTKAEDIL_SUM.EDI_CTL_NO      = OUTKAEDIL.EDI_CTL_NO " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_CUST          M07_01                " & vbNewLine _
    '                             & "   ON F02_01.NRS_BR_CD             = M07_01.NRS_BR_CD      " & vbNewLine _
    '                             & "  AND F02_01.CUST_CD_L             = M07_01.CUST_CD_L      " & vbNewLine _
    '                             & "  AND F02_01.CUST_CD_M             = M07_01.CUST_CD_M      " & vbNewLine _
    '                             & "  AND M07_01.CUST_CD_S             = '00'                  " & vbNewLine _
    '                             & "  AND M07_01.CUST_CD_SS            = '00'                  " & vbNewLine _
    '                             & "  AND M07_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_SHIHARAITO  M06_01                  " & vbNewLine _
    '                             & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
    '                             & "  AND F04_01.SHIHARAITO_CD         = M06_01.SHIHARAITO_CD  " & vbNewLine _
    '                             & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_DEST          M10_01                " & vbNewLine _
    '                             & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
    '                             & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
    '                             & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
    '                             & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_DEST          M10_02                " & vbNewLine _
    '                             & "   ON F02_01.NRS_BR_CD             = M10_02.NRS_BR_CD      " & vbNewLine _
    '                             & "  AND 'ZZZZZ'                      = M10_02.CUST_CD_L      " & vbNewLine _
    '                             & "  AND F02_01.DEST_CD               = M10_02.DEST_CD        " & vbNewLine _
    '                             & "  AND M10_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_DEST          M10_03                " & vbNewLine _
    '                             & "   ON F02_01.NRS_BR_CD             = M10_03.NRS_BR_CD      " & vbNewLine _
    '                             & "  AND F02_01.CUST_CD_L             = M10_03.CUST_CD_L      " & vbNewLine _
    '                             & "  AND F02_01.ORIG_CD               = M10_03.DEST_CD        " & vbNewLine _
    '                             & "  AND M10_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_DEST          M10_04                " & vbNewLine _
    '                             & "   ON F02_01.NRS_BR_CD             = M10_04.NRS_BR_CD      " & vbNewLine _
    '                             & "  AND 'ZZZZZ'                      = M10_04.CUST_CD_L      " & vbNewLine _
    '                             & "  AND F02_01.ORIG_CD               = M10_04.DEST_CD        " & vbNewLine _
    '                             & "  AND M10_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_UNSOCO        M38_01                " & vbNewLine _
    '                             & "   ON ISNULL(F01_01.NRS_BR_CD, F02_01.NRS_BR_CD) = M38_01.NRS_BR_CD " & vbNewLine _
    '                             & "  AND ISNULL(F01_01.UNSOCO_CD, F02_01.UNSO_CD) = M38_01.UNSOCO_CD " & vbNewLine _
    '                             & "  AND ISNULL(F01_01.UNSOCO_BR_CD, F02_01.UNSO_BR_CD) = M38_01.UNSOCO_BR_CD " & vbNewLine _
    '                             & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
    '                             & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
    '                             & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
    '                             & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
    '                             & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
    '                             & "   ON F04_01.SHIHARAI_FIXED_FLAG   = KBN_01.KBN_CD         " & vbNewLine _
    '                             & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
    '                             & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
    '                             & "   ON F04_01.SHIHARAI_TARIFF_BUNRUI_KB = KBN_02.KBN_CD     " & vbNewLine _
    '                             & "  AND KBN_02.KBN_GROUP_CD          = 'T015'                " & vbNewLine _
    '                             & "  AND KBN_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..Z_KBN           KBN_03                " & vbNewLine _
    '                             & "   ON F04_01.TAX_KB                = KBN_03.KBN_CD         " & vbNewLine _
    '                             & "  AND KBN_03.KBN_GROUP_CD          = 'Z001'                " & vbNewLine _
    '                             & "  AND KBN_03.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..Z_KBN           KBN_04                " & vbNewLine _
    '                             & "   ON F02_01.MOTO_DATA_KB          = KBN_04.KBN_CD         " & vbNewLine _
    '                             & "  AND KBN_04.KBN_GROUP_CD          = 'M004'                " & vbNewLine _
    '                             & "  AND KBN_04.SYS_DEL_FLG           = '0'                   " & vbNewLine _
    '                             & " LEFT JOIN $LM_MST$..Z_KBN           KBN_05                " & vbNewLine _
    '                             & "   ON F02_01.BIN_KB                = KBN_05.KBN_CD         " & vbNewLine _
    '                             & "  AND KBN_05.KBN_GROUP_CD          = 'U001'                " & vbNewLine _
    '                             & "  AND KBN_05.SYS_DEL_FLG           = '0'                   " & vbNewLine
    Private Const SQL_FROM As String = " FROM      $LM_TRN$..F_SHIHARAI_TRS    F04_01          " & vbNewLine _
                                 & " LEFT JOIN $LM_TRN$..F_UNSO_L        F02_01                " & vbNewLine _
                                 & "   ON F04_01.UNSO_NO_L             = F02_01.UNSO_NO_L      " & vbNewLine _
                                 & "  AND F02_01.TRIP_NO               <> ''                   " & vbNewLine _
                                 & "  AND F02_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_TRN$..F_UNSO_LL       F01_01                " & vbNewLine _
                                 & "   ON F02_01.TRIP_NO               = F01_01.TRIP_NO        " & vbNewLine _
                                 & "  AND F01_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_TRN$..F_SHIHARAI_TRS  F04_02                " & vbNewLine _
                                 & "   ON F04_01.SHIHARAI_GROUP_NO     = F04_02.UNSO_NO_L      " & vbNewLine _
                                 & "  AND F04_01.SHIHARAI_GROUP_NO_M   = F04_02.UNSO_NO_M      " & vbNewLine _
                                 & "  AND F04_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & "    LEFT JOIN $LM_TRN$..C_OUTKA_L    OUTKAL                " & vbNewLine _
                                 & "  ON OUTKAL.NRS_BR_CD             = F02_01.NRS_BR_CD       " & vbNewLine _
                                 & " AND OUTKAL.OUTKA_NO_L            = F02_01.INOUTKA_NO_L    " & vbNewLine _
                                 & " AND F02_01.MOTO_DATA_KB          = '20'                   " & vbNewLine _
                                 & " AND OUTKAL.SYS_DEL_FLG           = '0'                    " & vbNewLine _
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
                                 & " LEFT JOIN $LM_MST$..M_SHIHARAITO  M06_01                  " & vbNewLine _
                                 & "   ON F04_01.NRS_BR_CD             = M06_01.NRS_BR_CD      " & vbNewLine _
                                 & "  AND F04_01.SHIHARAITO_CD         = M06_01.SHIHARAITO_CD  " & vbNewLine _
                                 & "  AND M06_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_DEST        M10_01                  " & vbNewLine _
                                 & "   ON F02_01.NRS_BR_CD             = M10_01.NRS_BR_CD      " & vbNewLine _
                                 & "  AND F02_01.CUST_CD_L             = M10_01.CUST_CD_L      " & vbNewLine _
                                 & "  AND F02_01.DEST_CD               = M10_01.DEST_CD        " & vbNewLine _
                                 & "  AND M10_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_DEST          M10_02                " & vbNewLine _
                                 & "   ON F02_01.NRS_BR_CD             = M10_02.NRS_BR_CD      " & vbNewLine _
                                 & "  AND 'ZZZZZ'                      = M10_02.CUST_CD_L      " & vbNewLine _
                                 & "  AND F02_01.DEST_CD               = M10_02.DEST_CD        " & vbNewLine _
                                 & "  AND M10_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_DEST        M10_03                  " & vbNewLine _
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
                                 & "   ON ISNULL(F01_01.NRS_BR_CD, F02_01.NRS_BR_CD) = M38_01.NRS_BR_CD " & vbNewLine _
                                 & "  AND ISNULL(F01_01.UNSOCO_CD, F02_01.UNSO_CD) = M38_01.UNSOCO_CD " & vbNewLine _
                                 & "  AND ISNULL(F01_01.UNSOCO_BR_CD, F02_01.UNSO_BR_CD) = M38_01.UNSOCO_BR_CD " & vbNewLine _
                                 & "  AND M38_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_01                " & vbNewLine _
                                 & "   ON F02_01.SYUKA_TYUKEI_CD       = M12_01.JIS_CD         " & vbNewLine _
                                 & "  AND M12_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..M_JIS           M12_02                " & vbNewLine _
                                 & "   ON F02_01.HAIKA_TYUKEI_CD       = M12_02.JIS_CD         " & vbNewLine _
                                 & "  AND M12_02.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_01                " & vbNewLine _
                                 & "   ON F04_01.SHIHARAI_FIXED_FLAG   = KBN_01.KBN_CD         " & vbNewLine _
                                 & "  AND KBN_01.KBN_GROUP_CD          = 'U009'                " & vbNewLine _
                                 & "  AND KBN_01.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                 & " LEFT JOIN $LM_MST$..Z_KBN           KBN_02                " & vbNewLine _
                                 & "   ON F04_01.SHIHARAI_TARIFF_BUNRUI_KB = KBN_02.KBN_CD     " & vbNewLine _
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
    'END YANAI 要望番号1424 支払処理

#End Region

#Region "Where句"

    Private Const SQL_WHERE As String = "WHERE F02_01.NRS_BR_CD             = @NRS_BR_CD          " & vbNewLine _
                                      & "  AND F04_01.SYS_DEL_FLG           = '0'                 " & vbNewLine

#End Region

#Region "F_SHIHARAI_TRS_01"

    'START YANAI 要望番号1481 複数の支払いまとめを解除しようとしたらアベンド
    Private Const SQL_SELECT_UNCHIN_CNT As String = "SELECT COUNT(F02_01.NRS_BR_CD)         AS REC_CNT                              " & vbNewLine _
                                                  & " FROM       $LM_TRN$..F_SHIHARAI_TRS F04_01                                    " & vbNewLine _
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
                                                  & "WHERE  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine
    'END YANAI 要望番号1481 複数の支払いまとめを解除しようとしたらアベンド

    'START YANAI 要望番号1424 支払処理 まとめ解除時の再計算
    'Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                                             " & vbNewLine _
    '                                              & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
    '                                              & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
    '                                              & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_GROUP_NO              AS      SHIHARAI_GROUP_NO               " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_GROUP_NO_M            AS      SHIHARAI_GROUP_NO_M             " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.SHIHARAI_TARIFF_CD                                " & vbNewLine _
    '                                              & "                 ELSE F04_01.SHIHARAI_TARIFF_CD                                " & vbNewLine _
    '                                              & " END                                   AS      SHIHARAI_TARIFF_CD              " & vbNewLine _
    '                                              & ",F02_01.TAX_KB                         AS      TAX_KB                          " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.SHIHARAI_ETARIFF_CD                               " & vbNewLine _
    '                                              & "                 ELSE F04_01.SHIHARAI_ETARIFF_CD                               " & vbNewLine _
    '                                              & " END                                   AS      SHIHARAI_ETARIFF_CD             " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_NG_NB                 AS      DECI_NG_NB                      " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_NG_NB                 AS      SHIHARAI_NG_NB                  " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_KYORI                 AS      SHIHARAI_KYORI                  " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_WT                    AS      SHIHARAI_WT                     " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_UNCHIN                AS      SHIHARAI_UNCHIN                 " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_CITY_EXTC             AS      SHIHARAI_CITY_EXTC              " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_WINT_EXTC             AS      SHIHARAI_WINT_EXTC              " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_RELY_EXTC             AS      SHIHARAI_RELY_EXTC              " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_TOLL                  AS      SHIHARAI_TOLL                   " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_INSU                  AS      SHIHARAI_INSU                   " & vbNewLine _
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
    '                                              & "                 ELSE F04_01.SHIHARAI_SYARYO_KB                                " & vbNewLine _
    '                                              & " END                                   AS      SHIHARAI_SYARYO_KB              " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.NB_UT                                             " & vbNewLine _
    '                                              & "                 ELSE F04_01.SHIHARAI_PKG_UT                                   " & vbNewLine _
    '                                              & " END                                   AS      SHIHARAI_PKG_UT                 " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_DANGER_KB             AS      SHIHARAI_DANGER_KB              " & vbNewLine _
    '                                              & ",F04_01.REMARK                         AS      REMARK                          " & vbNewLine _
    '                                              & ",F02_01.SYS_UPD_DATE                   AS      SYS_UPD_DATE                    " & vbNewLine _
    '                                              & ",F02_01.SYS_UPD_TIME                   AS      SYS_UPD_TIME                    " & vbNewLine _
    '                                              & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
    '                                              & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
    '                                              & ",@SHIHARAI_FIXED_FLAG                  AS      SHIHARAI_FIXED_FLAG             " & vbNewLine _
    '                                              & ",F02_01.OUTKA_PLAN_DATE                AS      OUTKA_PLAN_DATE                 " & vbNewLine _
    '                                              & ",F02_01.ARR_PLAN_DATE                  AS      ARR_PLAN_DATE                   " & vbNewLine _
    '                                              & ",CASE @ITEM_DATA WHEN '04'                                                     " & vbNewLine _
    '                                              & "                 THEN F02_01.TARIFF_BUNRUI_KB                                  " & vbNewLine _
    '                                              & "                 ELSE F04_01.SHIHARAI_TARIFF_BUNRUI_KB                         " & vbNewLine _
    '                                              & " END                                   AS      SHIHARAI_TARIFF_BUNRUI_KB       " & vbNewLine _
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
    '                                              & ",F04_01.SHIHARAITO_CD                  AS      SHIHARAITO_CD                   " & vbNewLine _
    '                                              & ",F04_01.SHIHARAI_WT                    AS      SHIHARAI_WT                     " & vbNewLine _
    '                                              & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
    '                                              & ",@NEW_SYS_UPD_DATE                     AS      NEW_SYS_UPD_DATE                " & vbNewLine _
    '                                              & ",@NEW_SYS_UPD_TIME                     AS      NEW_SYS_UPD_TIME                " & vbNewLine _
    '                                              & ",@SYS_UPD_FLG                          AS      SYS_UPD_FLG                     " & vbNewLine _
    '                                              & ",F02_01.TRIP_NO                        AS      TRIP_NO                         " & vbNewLine _
    '                                              & " FROM       $LM_TRN$..F_SHIHARAI_TRS F04_01                                    " & vbNewLine _
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
    Private Const SQL_SELECT_UNCHIN As String = "SELECT                                                                             " & vbNewLine _
                                                  & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
                                                  & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
                                                  & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_GROUP_NO              AS      SHIHARAI_GROUP_NO               " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_GROUP_NO_M            AS      SHIHARAI_GROUP_NO_M             " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_TARIFF_CD             AS      SHIHARAI_TARIFF_CD              " & vbNewLine _
                                                  & ",F02_01.TAX_KB                         AS      TAX_KB                          " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_ETARIFF_CD            AS      SHIHARAI_ETARIFF_CD             " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_NG_NB                 AS      DECI_NG_NB                      " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_NG_NB                 AS      SHIHARAI_NG_NB                  " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_KYORI                 AS      SHIHARAI_KYORI                  " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_WT                    AS      SHIHARAI_WT                     " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_UNCHIN                AS      SHIHARAI_UNCHIN                 " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_CITY_EXTC             AS      SHIHARAI_CITY_EXTC              " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_WINT_EXTC             AS      SHIHARAI_WINT_EXTC              " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_RELY_EXTC             AS      SHIHARAI_RELY_EXTC              " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_TOLL                  AS      SHIHARAI_TOLL                   " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_INSU                  AS      SHIHARAI_INSU                   " & vbNewLine _
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
                                                  & ",F04_01.SHIHARAI_SYARYO_KB             AS      SHIHARAI_SYARYO_KB              " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_PKG_UT                AS      SHIHARAI_PKG_UT                 " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_DANGER_KB             AS      SHIHARAI_DANGER_KB              " & vbNewLine _
                                                  & ",F04_01.REMARK                         AS      REMARK                          " & vbNewLine _
                                                  & ",F02_01.SYS_UPD_DATE                   AS      SYS_UPD_DATE                    " & vbNewLine _
                                                  & ",F02_01.SYS_UPD_TIME                   AS      SYS_UPD_TIME                    " & vbNewLine _
                                                  & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
                                                  & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
                                                  & ",@SHIHARAI_FIXED_FLAG                  AS      SHIHARAI_FIXED_FLAG             " & vbNewLine _
                                                  & ",F02_01.OUTKA_PLAN_DATE                AS      OUTKA_PLAN_DATE                 " & vbNewLine _
                                                  & ",F02_01.ARR_PLAN_DATE                  AS      ARR_PLAN_DATE                   " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_TARIFF_BUNRUI_KB      AS      SHIHARAI_TARIFF_BUNRUI_KB       " & vbNewLine _
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
                                                  & ",F04_01.SHIHARAITO_CD                  AS      SHIHARAITO_CD                   " & vbNewLine _
                                                  & ",F04_01.SHIHARAI_WT                    AS      SHIHARAI_WT                     " & vbNewLine _
                                                  & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
                                                  & ",@NEW_SYS_UPD_DATE                     AS      NEW_SYS_UPD_DATE                " & vbNewLine _
                                                  & ",@NEW_SYS_UPD_TIME                     AS      NEW_SYS_UPD_TIME                " & vbNewLine _
                                                  & ",@SYS_UPD_FLG                          AS      SYS_UPD_FLG                     " & vbNewLine _
                                                  & ",F02_01.TRIP_NO                        AS      TRIP_NO                         " & vbNewLine _
                                                  & " FROM       $LM_TRN$..F_SHIHARAI_TRS F04_01                                    " & vbNewLine _
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
                                                  & "WHERE  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine
    'END YANAI 要望番号1424 支払処理 まとめ解除時の再計算

#End Region

#Region "F_SHIHARAI_TRS_02"

    Private Const SQL_SELECT_INIT_UNCHIN As String = "SELECT                                                                         " & vbNewLine _
                                                   & " F02_01.NRS_BR_CD                      AS      NRS_BR_CD                       " & vbNewLine _
                                                   & ",F02_01.UNSO_NO_L                      AS      UNSO_NO_L                       " & vbNewLine _
                                                   & ",F04_01.UNSO_NO_M                      AS      UNSO_NO_M                       " & vbNewLine _
                                                   & ",F04_01.SHIHARAI_GROUP_NO              AS      SHIHARAI_GROUP_NO               " & vbNewLine _
                                                   & ",F04_01.SHIHARAI_GROUP_NO_M            AS      SHIHARAI_GROUP_NO_M             " & vbNewLine _
                                                   & ",@ITEM_DATA                            AS      ITEM_DATA                       " & vbNewLine _
                                                   & ",@CD_L                                 AS      CD_L                            " & vbNewLine _
                                                   & ",@SHIHARAI_FIXED_FLAG                  AS      SHIHARAI_FIXED_FLAG             " & vbNewLine _
                                                   & ",@ROW_NO                               AS      ROW_NO                          " & vbNewLine _
                                                   & ",@NEW_SYS_UPD_DATE                     AS      NEW_SYS_UPD_DATE                " & vbNewLine _
                                                   & ",@NEW_SYS_UPD_TIME                     AS      NEW_SYS_UPD_TIME                " & vbNewLine _
                                                   & ",@SYS_UPD_FLG                          AS      SYS_UPD_FLG                     " & vbNewLine _
                                                   & ",@TRIP_NO                              AS      TRIP_NO                         " & vbNewLine _
                                                   & " FROM       $LM_TRN$..F_SHIHARAI_TRS F04_01                                    " & vbNewLine _
                                                   & " LEFT  JOIN $LM_TRN$..F_UNSO_L     F02_01                                      " & vbNewLine _
                                                   & "   ON  F04_01.UNSO_NO_L          = F02_01.UNSO_NO_L                            " & vbNewLine _
                                                   & "  AND  F02_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                                   & "WHERE  F04_01.UNSO_NO_L          = @UNSO_NO_L                                  " & vbNewLine _
                                                   & "  AND  F04_01.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                                   & "ORDER BY F04_01.UNSO_NO_L , F04_01.UNSO_NO_M                                   " & vbNewLine

#End Region

#Region "F_SHIHARAI_TRS_03"

    'START YANAI 要望番号1489 支払い確定できない
    'Private Const SQL_SELECT_ANBUN1 As String = "SELECT                                                                         " & vbNewLine _
    '                                         & " SHIHARAI.NRS_BR_CD                      AS      NRS_BR_CD                     " & vbNewLine _
    '                                         & ",SHIHARAI.UNSO_NO_L                      AS      UNSO_NO_L                     " & vbNewLine _
    '                                         & ",SHIHARAI.UNSO_NO_M                      AS      UNSO_NO_M                     " & vbNewLine _
    '                                         & ",SHIHARAI.SHIHARAI_GROUP_NO              AS      SHIHARAI_GROUP_NO             " & vbNewLine _
    '                                         & ",SHIHARAI.SHIHARAI_GROUP_NO_M            AS      SHIHARAI_GROUP_NO_M           " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_WT                        AS      DECI_WT                       " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_UNCHIN                    AS      DECI_UNCHIN                   " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_CITY_EXTC                 AS      DECI_CITY_EXTC                " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_WINT_EXTC                 AS      DECI_WINT_EXTC                " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_RELY_EXTC                 AS      DECI_RELY_EXTC                " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_TOLL                      AS      DECI_TOLL                     " & vbNewLine _
    '                                         & ",SHIHARAI.DECI_INSU                      AS      DECI_INSU                     " & vbNewLine _
    '                                         & ",SHIHARAI.SHIHARAI_WT                    AS      SHIHARAI_WT                   " & vbNewLine _
    '                                         & ",ISNULL(UNSOLL.SHIHARAI_UNCHIN,0)        AS      SHIHARAI_UNCHIN               " & vbNewLine _
    '                                         & ",UNSOL.SYS_UPD_DATE                      AS      SYS_UPD_DATE                  " & vbNewLine _
    '                                         & ",UNSOL.SYS_UPD_TIME                      AS      SYS_UPD_TIME                  " & vbNewLine _
    '                                         & ",@ROW_NO                                 AS      ROW_NO                        " & vbNewLine _
    '                                         & ",@NEW_SYS_UPD_DATE                       AS      NEW_SYS_UPD_DATE              " & vbNewLine _
    '                                         & ",@NEW_SYS_UPD_TIME                       AS      NEW_SYS_UPD_TIME              " & vbNewLine _
    '                                         & " FROM       $LM_TRN$..F_SHIHARAI_TRS SHIHARAI                                  " & vbNewLine _
    '                                         & " LEFT  JOIN $LM_TRN$..F_UNSO_L UNSOL                                           " & vbNewLine _
    '                                         & "   ON  UNSOL.TRIP_NO             = @TRIP_NO                                    " & vbNewLine _
    '                                         & "  AND  UNSOL.SYS_DEL_FLG         = '0'                                         " & vbNewLine _
    '                                         & " LEFT  JOIN $LM_TRN$..F_UNSO_LL UNSOLL                                         " & vbNewLine _
    '                                         & "   ON  UNSOLL.TRIP_NO            = UNSOL.TRIP_NO                               " & vbNewLine _
    '                                         & "  AND  UNSOLL.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
    '                                         & "WHERE  SHIHARAI.UNSO_NO_L        = UNSOL.UNSO_NO_L                             " & vbNewLine _
    '                                         & "  AND  SHIHARAI.SYS_DEL_FLG      = '0'                                         " & vbNewLine _
    '                                         & "ORDER BY SHIHARAI.UNSO_NO_L , SHIHARAI.UNSO_NO_M                               " & vbNewLine
    Private Const SQL_SELECT_ANBUN1 As String = "SELECT                                                                         " & vbNewLine _
                                             & " SHIHARAI.NRS_BR_CD                      AS      NRS_BR_CD                     " & vbNewLine _
                                             & ",SHIHARAI.UNSO_NO_L                      AS      UNSO_NO_L                     " & vbNewLine _
                                             & ",SHIHARAI.UNSO_NO_M                      AS      UNSO_NO_M                     " & vbNewLine _
                                             & ",SHIHARAI.SHIHARAI_GROUP_NO              AS      SHIHARAI_GROUP_NO             " & vbNewLine _
                                             & ",SHIHARAI.SHIHARAI_GROUP_NO_M            AS      SHIHARAI_GROUP_NO_M           " & vbNewLine _
                                             & ",SHIHARAI.DECI_WT                        AS      DECI_WT                       " & vbNewLine _
                                             & ",SHIHARAI.DECI_UNCHIN                    AS      DECI_UNCHIN                   " & vbNewLine _
                                             & ",SHIHARAI.DECI_CITY_EXTC                 AS      DECI_CITY_EXTC                " & vbNewLine _
                                             & ",SHIHARAI.DECI_WINT_EXTC                 AS      DECI_WINT_EXTC                " & vbNewLine _
                                             & ",SHIHARAI.DECI_RELY_EXTC                 AS      DECI_RELY_EXTC                " & vbNewLine _
                                             & ",SHIHARAI.DECI_TOLL                      AS      DECI_TOLL                     " & vbNewLine _
                                             & ",SHIHARAI.DECI_INSU                      AS      DECI_INSU                     " & vbNewLine _
                                             & ",SHIHARAI.SHIHARAI_WT                    AS      SHIHARAI_WT                   " & vbNewLine _
                                             & ",ISNULL(UNSOLL.SHIHARAI_UNCHIN,0)        AS      SHIHARAI_UNCHIN               " & vbNewLine _
                                             & ",UNSOL.SYS_UPD_DATE                      AS      SYS_UPD_DATE                  " & vbNewLine _
                                             & ",UNSOL.SYS_UPD_TIME                      AS      SYS_UPD_TIME                  " & vbNewLine _
                                             & ",@ROW_NO                                 AS      ROW_NO                        " & vbNewLine _
                                             & ",@NEW_SYS_UPD_DATE                       AS      NEW_SYS_UPD_DATE              " & vbNewLine _
                                             & ",@NEW_SYS_UPD_TIME                       AS      NEW_SYS_UPD_TIME              " & vbNewLine _
                                             & " FROM       $LM_TRN$..F_SHIHARAI_TRS SHIHARAI                                  " & vbNewLine _
                                             & " LEFT  JOIN $LM_TRN$..F_UNSO_L UNSOL                                           " & vbNewLine _
                                             & "   ON  UNSOL.UNSO_NO_L           = SHIHARAI.UNSO_NO_L                          " & vbNewLine _
                                             & "  AND  UNSOL.SYS_DEL_FLG         = '0'                                         " & vbNewLine _
                                             & " LEFT  JOIN $LM_TRN$..F_UNSO_LL UNSOLL                                         " & vbNewLine _
                                             & "   ON  UNSOLL.TRIP_NO            = UNSOL.TRIP_NO                               " & vbNewLine _
                                             & "  AND  UNSOLL.SYS_DEL_FLG        = '0'                                         " & vbNewLine _
                                             & "WHERE  SHIHARAI.SHIHARAI_GROUP_NO = @SHIHARAI_GROUP_NO                         " & vbNewLine _
                                             & "  AND  SHIHARAI.SHIHARAI_GROUP_NO_M = @SHIHARAI_GROUP_NO_M                     " & vbNewLine _
                                             & "  AND  SHIHARAI.SYS_DEL_FLG      = '0'                                         " & vbNewLine _
                                             & "ORDER BY SHIHARAI.UNSO_NO_L , SHIHARAI.UNSO_NO_M                               " & vbNewLine
    'END YANAI 要望番号1489 支払い確定できない

    Private Const SQL_SELECT_ANBUN2 As String = "SELECT                                                                         " & vbNewLine _
                                         & " SHIHARAI.NRS_BR_CD                      AS      NRS_BR_CD                     " & vbNewLine _
                                         & ",SHIHARAI.UNSO_NO_L                      AS      UNSO_NO_L                     " & vbNewLine _
                                         & ",SHIHARAI.UNSO_NO_M                      AS      UNSO_NO_M                     " & vbNewLine _
                                         & ",SHIHARAI.SHIHARAI_GROUP_NO              AS      SHIHARAI_GROUP_NO             " & vbNewLine _
                                         & ",SHIHARAI.SHIHARAI_GROUP_NO_M            AS      SHIHARAI_GROUP_NO_M           " & vbNewLine _
                                         & ",SHIHARAI.DECI_WT                        AS      DECI_WT                       " & vbNewLine _
                                         & ",SHIHARAI.DECI_UNCHIN                    AS      DECI_UNCHIN                   " & vbNewLine _
                                         & ",SHIHARAI.DECI_CITY_EXTC                 AS      DECI_CITY_EXTC                " & vbNewLine _
                                         & ",SHIHARAI.DECI_WINT_EXTC                 AS      DECI_WINT_EXTC                " & vbNewLine _
                                         & ",SHIHARAI.DECI_RELY_EXTC                 AS      DECI_RELY_EXTC                " & vbNewLine _
                                         & ",SHIHARAI.DECI_TOLL                      AS      DECI_TOLL                     " & vbNewLine _
                                         & ",SHIHARAI.DECI_INSU                      AS      DECI_INSU                     " & vbNewLine _
                                         & ",SHIHARAI.SHIHARAI_WT                    AS      SHIHARAI_WT                   " & vbNewLine _
                                         & ",0                                       AS      SHIHARAI_UNCHIN               " & vbNewLine _
                                         & ",UNSOL.SYS_UPD_DATE                      AS      SYS_UPD_DATE                  " & vbNewLine _
                                         & ",UNSOL.SYS_UPD_TIME                      AS      SYS_UPD_TIME                  " & vbNewLine _
                                         & ",@ROW_NO                                 AS      ROW_NO                        " & vbNewLine _
                                         & ",@NEW_SYS_UPD_DATE                       AS      NEW_SYS_UPD_DATE              " & vbNewLine _
                                         & ",@NEW_SYS_UPD_TIME                       AS      NEW_SYS_UPD_TIME              " & vbNewLine _
                                         & " FROM       $LM_TRN$..F_SHIHARAI_TRS SHIHARAI                                  " & vbNewLine _
                                         & " LEFT  JOIN $LM_TRN$..F_UNSO_L UNSOL                                           " & vbNewLine _
                                         & "   ON  UNSOL.UNSO_NO_L           = @UNSO_NO_L                                  " & vbNewLine _
                                         & "  AND  UNSOL.SYS_DEL_FLG         = '0'                                         " & vbNewLine _
                                         & "WHERE  SHIHARAI.UNSO_NO_L        = UNSOL.UNSO_NO_L                             " & vbNewLine _
                                         & "  AND  SHIHARAI.SYS_DEL_FLG      = '0'                                         " & vbNewLine _
                                         & "ORDER BY SHIHARAI.UNSO_NO_L , SHIHARAI.UNSO_NO_M                               " & vbNewLine

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

    Private Const SQL_UPDATE_UNSO_L_TARIFF1 As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                           & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "      ,SHIHARAI_TARIFF_CD = @SHIHARAI_TARIFF_CD" & vbNewLine _
                                                           & "      ,TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                           & "  AND  SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                           & "  AND  SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

    Private Const SQL_UPDATE_UNSO_L_TARIFF2 As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                       & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                       & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                       & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                       & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                       & "      ,SHIHARAI_TARIFF_CD = @SHIHARAI_TARIFF_CD" & vbNewLine _
                                                       & "      ,SHIHARAI_ETARIFF_CD = ''    " & vbNewLine _
                                                       & "      ,TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB" & vbNewLine _
                                                       & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                       & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                       & "  AND  SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                       & "  AND  SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

    Private Const SQL_UPDATE_UNSO_L_ETARIFF As String = "UPDATE $LM_TRN$..F_UNSO_L SET      " & vbNewLine _
                                                      & "       SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                      & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                      & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                      & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                      & "      ,SHIHARAI_ETARIFF_CD = @SHIHARAI_ETARIFF_CD" & vbNewLine _
                                                      & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                      & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                      & "  AND  SYS_UPD_DATE = @GUI_UPD_DATE" & vbNewLine _
                                                      & "  AND  SYS_UPD_TIME = @GUI_UPD_TIME" & vbNewLine

    Private Const SQL_UPDATE_HAITA As String = "  AND SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                             & "  AND SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine

#End Region

#Region "F_SHIHARAI_TRS"

    Private Const SQL_UPDATE_UNCHIN_SYS_DATETIME As String = "      ,SYS_UPD_DATE = @SYS_UPD_DATE" & vbNewLine _
                                                           & "      ,SYS_UPD_TIME = @SYS_UPD_TIME" & vbNewLine _
                                                           & "      ,SYS_UPD_PGID = @SYS_UPD_PGID" & vbNewLine _
                                                           & "      ,SYS_UPD_USER = @SYS_UPD_USER" & vbNewLine _
                                                           & "WHERE  NRS_BR_CD    = @NRS_BR_CD   " & vbNewLine _
                                                           & "  AND  UNSO_NO_L    = @UNSO_NO_L   " & vbNewLine _
                                                           & "  AND  UNSO_NO_M    = @UNSO_NO_M   " & vbNewLine

#End Region

#Region "F_SHIHARAI_TRS(再計算時)"

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
    ''' F_SHIHARAI_TRS(再計算時) SQL UPDATE句
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SQL_UPDATE_UNCHIN_SAIKEISAN As String = "UPDATE $LM_TRN$..F_SHIHARAI_TRS SET                            " & vbNewLine _
                                                        & "                  SHIHARAI_TARIFF_CD   = @SHIHARAI_TARIFF_CD   " & vbNewLine _
                                                        & "                 ,SHIHARAI_ETARIFF_CD  = @SHIHARAI_ETARIFF_CD  " & vbNewLine _
                                                        & "                 ,DECI_UNCHIN          = @DECI_UNCHIN          " & vbNewLine _
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
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF080DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMF080DAC.SQL_FROM)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF080DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMF080DAC.SQL_FROM)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Call Me.SetOrderBySQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_BY", Me.SetWhereData(Me._Row.Item("ORDER_BY").ToString(), LMF080DAC.ConditionPattern.OTHER), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        map.Add("SHIHARAI_FIXED_NM", "SHIHARAI_FIXED_NM")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("CUST_NM_L", "CUST_NM_L")
        map.Add("CUST_NM_M", "CUST_NM_M")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        map.Add("SHIHARAITO_NM", "SHIHARAITO_NM")
        map.Add("SHIHARAITO_BUSYO_NM", "SHIHARAITO_BUSYO_NM")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_NM", "DEST_NM")
        map.Add("DEST_JIS_CD", "DEST_JIS_CD")
        map.Add("UNSO_CD", "UNSO_CD")
        map.Add("UNSO_BR_CD", "UNSO_BR_CD")
        map.Add("UNSO_NM", "UNSO_NM")
        map.Add("UNSO_BR_NM", "UNSO_BR_NM")
        map.Add("SHIHARAI_TARIFF_BUNRUI_KB", "SHIHARAI_TARIFF_BUNRUI_KB")
        map.Add("TARIFF_BUNRUI", "TARIFF_BUNRUI")
        map.Add("SHIHARAI_TARIFF_CD", "SHIHARAI_TARIFF_CD")
        map.Add("SHIHARAI_ETARIFF_CD", "SHIHARAI_ETARIFF_CD")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_KYORI", "DECI_KYORI")
        map.Add("UNCHIN", "UNCHIN")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("TAX_NM", "TAX_NM")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
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
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("GROUP_FLG", "GROUP_FLG")
        map.Add("CHK_UNCHIN", "CHK_UNCHIN")
        map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
        map.Add("SHIHARAI_CITY_EXTC", "SHIHARAI_CITY_EXTC")
        map.Add("SHIHARAI_WINT_EXTC", "SHIHARAI_WINT_EXTC")
        map.Add("SHIHARAI_RELY_EXTC", "SHIHARAI_RELY_EXTC")
        map.Add("SHIHARAI_TOLL", "SHIHARAI_TOLL")
        map.Add("SHIHARAI_INSU", "SHIHARAI_INSU")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("CUST_REF_NO", "CUST_REF_NO")
        map.Add("MINASHI_DEST_CD", "MINASHI_DEST_CD")
        map.Add("BIN_KB", "BIN_KB")
        map.Add("BIN_NM", "BIN_NM")
        map.Add("DECI_NG_NB", "DECI_NG_NB")
        map.Add("SHIHARAI_PKG_UT", "SHIHARAI_PKG_UT")
        map.Add("SHIHARAI_SYARYO_KB", "SHIHARAI_SYARYO_KB")
        map.Add("SHIHARAI_DANGER_KB", "SHIHARAI_DANGER_KB")
        'START YANAI 要望番号1424 支払処理
        map.Add("DEST_AD", "DEST_AD")
        map.Add("SHIHARAI_UNCHIN_UNSOLL", "SHIHARAI_UNCHIN_UNSOLL")
        'END YANAI 要望番号1424 支払処理

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF080DAC.TABLE_NM_OUT)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '画面項目の反映
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_DATA", Me._Row.Item("ITEM_DATA").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_L", Me._Row.Item("CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", Me._Row.Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", Me._Row.Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_FLG", Me._Row.Item("SYS_UPD_FLG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me._Row.Item("TRIP_NO").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMF080DAC.SQL_SELECT_INIT_UNCHIN, Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
        map.Add("ITEM_DATA", "ITEM_DATA")
        map.Add("CD_L", "CD_L")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("NEW_SYS_UPD_DATE", "NEW_SYS_UPD_DATE")
        map.Add("NEW_SYS_UPD_TIME", "NEW_SYS_UPD_TIME")
        map.Add("SYS_UPD_FLG", "SYS_UPD_FLG")
        map.Add("TRIP_NO", "TRIP_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF080DAC.TABLE_NM_UNCHIN)

        '取得できない場合、排他エラー
        Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

        Return ds

    End Function

    'START YANAI 要望番号1481 複数の支払いまとめを解除しようとしたらアベンド
    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinDataCnt(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMF080DAC.SQL_SELECT_UNCHIN_CNT)
        Me._StrSql.Append(vbNewLine)

        If String.IsNullOrEmpty(Me._Row.Item("SHIHARAI_GROUP_NO").ToString()) = True Then

            '通常レコードの場合、運送番号
            Me._StrSql.Append(" AND F04_01.UNSO_NO_L = @UNSO_NO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND F04_01.UNSO_NO_M = @UNSO_NO_M ")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me._Row.Item("UNSO_NO_M").ToString(), DBDataType.CHAR))

        Else

            'まとめの場合
            Me._StrSql.Append(" AND F04_01.SHIHARAI_GROUP_NO = @SHIHARAI_GROUP_NO ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND F04_01.SHIHARAI_GROUP_NO_M = @SHIHARAI_GROUP_NO_M ")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", Me._Row.Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", Me._Row.Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))

        End If

        '画面項目の反映
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_DATA", Me._Row.Item("ITEM_DATA").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_L", Me._Row.Item("CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", Me._Row.Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", Me._Row.Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_FLG", Me._Row.Item("SYS_UPD_FLG").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        '処理件数の設定
        reader.Read()
        MyBase.SetResultCount(Convert.ToInt32(reader("REC_CNT")))
        reader.Close()

        '取得できない場合、排他エラー
        Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

        Return ds

    End Function
    'END YANAI 要望番号1481 複数の支払いまとめを解除しようとしたらアベンド

    ''' <summary>
    ''' 運賃情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(LMF080DAC.SQL_SELECT_UNCHIN)
        Me._StrSql.Append(vbNewLine)

        If String.IsNullOrEmpty(Me._Row.Item("SHIHARAI_GROUP_NO").ToString()) = True Then

            '通常レコードの場合、運送番号
            Me._StrSql.Append(" AND F04_01.UNSO_NO_L = @UNSO_NO_L ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND F04_01.UNSO_NO_M = @UNSO_NO_M ")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me._Row.Item("UNSO_NO_M").ToString(), DBDataType.CHAR))

        Else

            'まとめの場合
            Me._StrSql.Append(" AND F04_01.SHIHARAI_GROUP_NO = @SHIHARAI_GROUP_NO ")
            Me._StrSql.Append(vbNewLine)
            Me._StrSql.Append(" AND F04_01.SHIHARAI_GROUP_NO_M = @SHIHARAI_GROUP_NO_M ")
            Me._StrSql.Append(vbNewLine)

            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", Me._Row.Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", Me._Row.Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))

        End If

        '並び順の設定
        Me._StrSql.Append(" ORDER BY F04_01.UNSO_NO_L , F04_01.UNSO_NO_M ")

        '画面項目の反映
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ITEM_DATA", Me._Row.Item("ITEM_DATA").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@CD_L", Me._Row.Item("CD_L").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", Me._Row.Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNTIN_CALCULATION_KB", Me._Row.Item("UNTIN_CALCULATION_KB").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SYS_UPD_FLG", Me._Row.Item("SYS_UPD_FLG").ToString(), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
        map.Add("SHIHARAI_TARIFF_CD", "SHIHARAI_TARIFF_CD")
        map.Add("TAX_KB", "TAX_KB")
        map.Add("SHIHARAI_ETARIFF_CD", "SHIHARAI_ETARIFF_CD")
        map.Add("SHIHARAI_NG_NB", "SHIHARAI_NG_NB")
        map.Add("SHIHARAI_KYORI", "SHIHARAI_KYORI")
        map.Add("SHIHARAI_WT", "SHIHARAI_WT")
        map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
        map.Add("SHIHARAI_CITY_EXTC", "SHIHARAI_CITY_EXTC")
        map.Add("SHIHARAI_WINT_EXTC", "SHIHARAI_WINT_EXTC")
        map.Add("SHIHARAI_RELY_EXTC", "SHIHARAI_RELY_EXTC")
        map.Add("SHIHARAI_TOLL", "SHIHARAI_TOLL")
        map.Add("SHIHARAI_INSU", "SHIHARAI_INSU")
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
        map.Add("SHIHARAI_SYARYO_KB", "SHIHARAI_SYARYO_KB")
        map.Add("SHIHARAI_PKG_UT", "SHIHARAI_PKG_UT")
        map.Add("SHIHARAI_DANGER_KB", "SHIHARAI_DANGER_KB")
        map.Add("REMARK", "REMARK")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("ITEM_DATA", "ITEM_DATA")
        map.Add("CD_L", "CD_L")
        map.Add("SHIHARAI_FIXED_FLAG", "SHIHARAI_FIXED_FLAG")
        map.Add("OUTKA_PLAN_DATE", "OUTKA_PLAN_DATE")
        map.Add("ARR_PLAN_DATE", "ARR_PLAN_DATE")
        map.Add("SHIHARAI_TARIFF_BUNRUI_KB", "SHIHARAI_TARIFF_BUNRUI_KB")
        map.Add("MOTO_DATA_KB", "MOTO_DATA_KB")
        map.Add("UNTIN_CALCULATION_KB", "UNTIN_CALCULATION_KB")
        map.Add("CUST_CD_L", "CUST_CD_L")
        map.Add("CUST_CD_M", "CUST_CD_M")
        map.Add("DEST_CD", "DEST_CD")
        map.Add("DEST_JIS", "DEST_JIS")
        map.Add("VCLE_KB", "VCLE_KB")
        map.Add("UNSO_ONDO_KB", "UNSO_ONDO_KB")
        map.Add("SIZE_KB", "SIZE_KB")
        map.Add("SHIHARAITO_CD", "SHIHARAITO_CD")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("NEW_SYS_UPD_DATE", "NEW_SYS_UPD_DATE")
        map.Add("NEW_SYS_UPD_TIME", "NEW_SYS_UPD_TIME")
        map.Add("SYS_UPD_FLG", "SYS_UPD_FLG")
        map.Add("TRIP_NO", "TRIP_NO")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF080DAC.TABLE_NM_UNCHIN)

        '取得できない場合、排他エラー
        Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

        Return ds

    End Function

    'START YANAI 要望番号1424 支払処理 按分しないようにする
    '''' <summary>
    '''' 按分対象データ取得
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet</returns>
    '''' <remarks></remarks>
    'Private Function SelectAnbunData(ByVal ds As DataSet) As DataSet

    '    'DataSetのIN情報を取得
    '    Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

    '    'INTableの条件rowの格納
    '    Me._Row = inTbl.Rows(0)

    '    'SQLパラメータ初期化
    '    Me._SqlPrmList = New ArrayList()

    '    '画面項目の反映
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
    '    Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
    '    If String.IsNullOrEmpty(Me._Row.Item("TRIP_NO").ToString()) = False Then
    '        '運行データありの場合
    '        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me._Row.Item("TRIP_NO").ToString(), DBDataType.CHAR))
    '    Else
    '        '運行データなしの場合
    '        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
    '    End If

    '    'スキーマ名設定
    '    Dim sql As String = String.Empty
    '    If String.IsNullOrEmpty(Me._Row.Item("TRIP_NO").ToString()) = False Then
    '        '運行データありの場合
    '        sql = Me.SetSchemaNm(LMF080DAC.SQL_SELECT_ANBUN1, Me._Row.Item("NRS_BR_CD").ToString())
    '    Else
    '        '運行データなしの場合
    '        sql = Me.SetSchemaNm(LMF080DAC.SQL_SELECT_ANBUN2, Me._Row.Item("NRS_BR_CD").ToString())
    '    End If

    '    'SQL文のコンパイル
    '    Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

    '    'パラメータの反映
    '    For Each obj As Object In Me._SqlPrmList
    '        cmd.Parameters.Add(obj)
    '    Next

    '    MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

    '    'SQLの発行
    '    Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

    '    'DataReader→DataTableへの転記
    '    Dim map As Hashtable = New Hashtable()

    '    '取得データの格納先をマッピング
    '    map.Add("NRS_BR_CD", "NRS_BR_CD")
    '    map.Add("UNSO_NO_L", "UNSO_NO_L")
    '    map.Add("UNSO_NO_M", "UNSO_NO_M")
    '    map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
    '    map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
    '    map.Add("DECI_WT", "DECI_WT")
    '    map.Add("DECI_UNCHIN", "DECI_UNCHIN")
    '    map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
    '    map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
    '    map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
    '    map.Add("DECI_TOLL", "DECI_TOLL")
    '    map.Add("DECI_INSU", "DECI_INSU")
    '    map.Add("SHIHARAI_WT", "SHIHARAI_WT")
    '    map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
    '    map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
    '    map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
    '    map.Add("ROW_NO", "ROW_NO")
    '    map.Add("NEW_SYS_UPD_DATE", "NEW_SYS_UPD_DATE")
    '    map.Add("NEW_SYS_UPD_TIME", "NEW_SYS_UPD_TIME")

    '    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF080DAC.TABLE_NM_OUT_ANBUN)

    '    '取得できない場合、排他エラー
    '    Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

    '    Return ds

    'End Function
    ''' <summary>
    ''' 按分対象データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectAnbunData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        '画面項目の反映
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ROW_NO", Me._Row.Item("ROW_NO").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_DATE", Me._Row.Item("NEW_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NEW_SYS_UPD_TIME", Me._Row.Item("NEW_SYS_UPD_TIME").ToString(), DBDataType.CHAR))
        If String.IsNullOrEmpty(Me._Row.Item("SHIHARAI_GROUP_NO").ToString()) = False Then
            'まとめデータありの場合
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", Me._Row.Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", Me._Row.Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))
        Else
            'まとめデータなしの場合
            Me._SqlPrmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me._Row.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
        End If

        'スキーマ名設定
        Dim sql As String = String.Empty
        If String.IsNullOrEmpty(Me._Row.Item("SHIHARAI_GROUP_NO").ToString()) = False Then
            'まとめデータありの場合
            sql = Me.SetSchemaNm(LMF080DAC.SQL_SELECT_ANBUN1, Me._Row.Item("NRS_BR_CD").ToString())
        Else
            'まとめデータなしの場合
            sql = Me.SetSchemaNm(LMF080DAC.SQL_SELECT_ANBUN2, Me._Row.Item("NRS_BR_CD").ToString())
        End If

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Dim reader As SqlDataReader = MyBase.GetSelectResult(cmd)

        'DataReader→DataTableへの転記
        Dim map As Hashtable = New Hashtable()

        '取得データの格納先をマッピング
        map.Add("NRS_BR_CD", "NRS_BR_CD")
        map.Add("UNSO_NO_L", "UNSO_NO_L")
        map.Add("UNSO_NO_M", "UNSO_NO_M")
        map.Add("SHIHARAI_GROUP_NO", "SHIHARAI_GROUP_NO")
        map.Add("SHIHARAI_GROUP_NO_M", "SHIHARAI_GROUP_NO_M")
        map.Add("DECI_WT", "DECI_WT")
        map.Add("DECI_UNCHIN", "DECI_UNCHIN")
        map.Add("DECI_CITY_EXTC", "DECI_CITY_EXTC")
        map.Add("DECI_WINT_EXTC", "DECI_WINT_EXTC")
        map.Add("DECI_RELY_EXTC", "DECI_RELY_EXTC")
        map.Add("DECI_TOLL", "DECI_TOLL")
        map.Add("DECI_INSU", "DECI_INSU")
        map.Add("SHIHARAI_WT", "SHIHARAI_WT")
        map.Add("SHIHARAI_UNCHIN", "SHIHARAI_UNCHIN")
        map.Add("SYS_UPD_DATE", "SYS_UPD_DATE")
        map.Add("SYS_UPD_TIME", "SYS_UPD_TIME")
        map.Add("ROW_NO", "ROW_NO")
        map.Add("NEW_SYS_UPD_DATE", "NEW_SYS_UPD_DATE")
        map.Add("NEW_SYS_UPD_TIME", "NEW_SYS_UPD_TIME")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMF080DAC.TABLE_NM_OUT_ANBUN)

        '取得できない場合、排他エラー
        Call Me.UpdateResultChk(MyBase.GetResultCount(), False)

        Return ds

    End Function
    'END YANAI 要望番号1424 支払処理 按分しないようにする

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
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL文のコンパイル
        Dim cmd As SqlCommand = Nothing
        If (LMF080DAC.SHUSEI_TARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF080DAC.SQL_UPDATE_UNSO_L_TARIFF1)
        ElseIf (LMF080DAC.SHUSEI_YOKO).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF080DAC.SQL_UPDATE_UNSO_L_TARIFF2)
        ElseIf (LMF080DAC.SHUSEI_ETARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Me._StrSql.Append(LMF080DAC.SQL_UPDATE_UNSO_L_ETARIFF)
        Else
            Me._StrSql.Append(LMF080DAC.SQL_UPDATE_UNSO_L_SYS_DATETIME)
        End If

        If ("1").Equals(Me._Row.Item("SYS_UPD_FLG").ToString) = True Then
            '２回目以降の一括変更時
            Me._StrSql.Append(LMF080DAC.SQL_UPDATE_HAITA)
        End If

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        cmd = MyBase.CreateSqlCommand(sql)

        'パラメータ設定
        Call Me.SetUnsoLPkParameter(Me._Row, Me._SqlPrmList)
        If String.IsNullOrEmpty(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Call Me.SetSysdataParameter(Me._SqlPrmList)
        Else
            Call Me.SetSysdataHenkoParameter(Me._SqlPrmList)
        End If
        Call Me.SetGuiSysdataTimeParameter(Me._SqlPrmList, Me._Row)
        If (LMF080DAC.SHUSEI_TARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
            (LMF080DAC.SHUSEI_YOKO).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True OrElse _
            (LMF080DAC.SHUSEI_ETARIFF).Equals(Me._Row.Item("ITEM_DATA").ToString()) = True Then
            Call Me.SetUnsoLTariffParameter(Me._Row, Me._SqlPrmList)
        End If

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, LMF080DAC.GROUP_ACTION.Equals(Me._Row.Item("ITEM_DATA").ToString()))

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

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)
        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)
        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQL構築
        Me._StrSql.Append(" UPDATE $LM_TRN$..F_SHIHARAI_TRS SET ")
        Call Me.SetUpdateUnchinSQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Me._StrSql.Append(LMF080DAC.SQL_UPDATE_UNCHIN_SYS_DATETIME)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString()))

        'パラメータ設定
        Call Me.SetSysdataParameter(Me._SqlPrmList)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, False)

        Return ds

    End Function

#End Region

#Region "運賃更新処理(再計算時)"

    ''' <summary>
    ''' 運送(大)データ件数検索(再計算時の排他用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>運送(大)件数取得SQLの構築・発行</remarks>
    Private Function SelectDataSaikeisan(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF080DAC.SQL_SELECT_COUNT_SAIKEISAN)
        Call Me.SetSelectUnsoLParameter(inTbl.Rows(0), Me._SqlPrmList)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMF080DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMF080DAC.TABLE_NM_UNCHIN)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMF080DAC.SQL_UPDATE_UNCHIN_SAIKEISAN) 'SQL構築(UPDATE句)

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

        MyBase.Logger.WriteSQLLog("LMF080DAC", "UpdUnchinData", cmd)

        'SQLの発行
        Me.UpdateResultChk(cmd, False)

        Return ds

    End Function

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
        With dr

            'Whereの固定値を設定
            sql.Append(LMF080DAC.SQL_WHERE)

            '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.SetWhereData(.Item("NRS_BR_CD").ToString(), LMF080DAC.ConditionPattern.OTHER), DBDataType.CHAR))

            '日付絞込
            Call Me.SetConditionDateSQL(prmList, dr, sql)

            '支払タリフコード
            whereStr = .Item("SHIHARAI_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAI_TARIFF_CD LIKE @SHIHARAI_TARIFF_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '支払割増タリフコード
            whereStr = .Item("SHIHARAI_ETARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAI_ETARIFF_CD LIKE @SHIHARAI_ETARIFF_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送会社コード
            whereStr = .Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND ISNULL(F01_01.UNSOCO_CD, F02_01.UNSO_CD) LIKE @UNSO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送会社支店コード
            whereStr = .Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND ISNULL(F01_01.UNSOCO_BR_CD, F02_01.UNSO_BR_CD) LIKE @UNSO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送会社名
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M38_01.UNSOCO_NM + '　' + M38_01.UNSOCO_BR_NM LIKE @UNSO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '荷主(大)コード
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_L = @CUST_CD_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主(中)コード
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_M = @CUST_CD_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M07_01.CUST_NM_L + '　' + M07_01.CUST_NM_M LIKE @CUST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_NM", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '運賃区分
            whereStr = .Item("UNCHIN_KBN").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                '運賃区分 = '1'の場合トンキロ選択
                If LMConst.FLG.ON.Equals(whereStr) = True Then

                    sql.Append(" AND F04_01.SHIHARAI_TARIFF_BUNRUI_KB <> @UNCHIN_KBN")

                Else

                    sql.Append(" AND F04_01.SHIHARAI_TARIFF_BUNRUI_KB = @UNCHIN_KBN")

                End If

                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KBN", Me.SetWhereData(LMF080DAC.TARIFF_KURUMA, LMF080DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            'まとめ番号
            whereStr = .Item("GROUP_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                'まとめ番号 = '1'の場合まとめ済みを表示
                If LMConst.FLG.ON.Equals(whereStr) = True Then

                    sql.Append(" AND F04_01.SHIHARAI_GROUP_NO <> '' ")

                Else

                    sql.Append(" AND F04_01.SHIHARAI_GROUP_NO = '' ")

                End If

                sql.Append(vbNewLine)

            End If

            '支払運賃確定
            whereStr = .Item("KAKUTEI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAI_FIXED_FLAG = @KAKUTEI_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@KAKUTEI_KB", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))
            End If

            '元データ区分
            whereStr = .Item("MOTO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.MOTO_DATA_KB = @MOTO_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@MOTO_KB", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))
            End If

            '支払先コード
            whereStr = .Item("SHIHARAITO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAITO_CD LIKE @SHIHARAITO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '支払先名
            whereStr = .Item("SHIHARAI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M06_01.SHIHARAITO_NM + '　' + M06_01.SHIHARAITO_BUSYO_NM LIKE @SHIHARAI_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_NM", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '届先名
            whereStr = .Item("DEST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M10_01.DEST_NM LIKE @DEST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            'タリフ分類
            whereStr = .Item("TARIFF_BUNRUI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAI_TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))
            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.TAX_KB = @TAX_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))
            End If

            'まとめ番号
            whereStr = .Item("SHIHARAI_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAI_GROUP_NO LIKE @SHIHARAI_GROUP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            'まとめ番号M
            whereStr = .Item("SHIHARAI_GROUP_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SHIHARAI_GROUP_NO_M LIKE @SHIHARAI_GROUP_NO_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '備考
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.REMARK LIKE @REMARK")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
            End If

            '管理番号
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.INOUTKA_NO_L LIKE @INOUTKA_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送番号L
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.UNSO_NO_L LIKE @UNSO_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運送番号M
            whereStr = .Item("UNSO_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.UNSO_NO_M LIKE @UNSO_NO_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.TRIP_NO LIKE @TRIP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '届先コード
            whereStr = .Item("DEST_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.DEST_CD LIKE @DEST_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '届先JIS
            whereStr = .Item("DEST_JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M10_01.JIS LIKE @DEST_JIS_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '伝票№
            whereStr = .Item("CUST_REF_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_REF_NO LIKE @CUST_REF_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            'START YANAI 要望番号1424 支払処理
            '届先住所
            whereStr = .Item("DEST_AD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND (M10_01.SHIHARAI_AD LIKE @DEST_AD")
                sql.Append("  OR  M10_03.SHIHARAI_AD LIKE @DEST_AD)")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_AD", Me.SetWhereData(whereStr, LMF080DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '支払区分
            whereStr = .Item("SHIHARAI_KB").ToString()
            If ("01").Equals(whereStr) = True Then
                sql.Append(" AND F01_01.SHIHARAI_UNCHIN = '0'")
                sql.Append(vbNewLine)
            ElseIf ("02").Equals(whereStr) = True Then
                sql.Append(" AND F01_01.SHIHARAI_UNCHIN <> '0'")
                sql.Append(vbNewLine)
            End If
            'END YANAI 要望番号1424 支払処理

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

                Case LMF080DAC.DATE_KBN_NONYU

                    fromCondition = " AND F02_01.ARR_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.ARR_PLAN_DATE <= @TO_DATE "

                Case LMF080DAC.DATE_KBN_SHUKKA

                    fromCondition = " AND F02_01.OUTKA_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.OUTKA_PLAN_DATE <= @TO_DATE "

            End Select

            If String.IsNullOrEmpty(fromDate) = False Then
                sql.Append(fromCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@FROM_DATE", Me.SetWhereData(fromDate, LMF080DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            If String.IsNullOrEmpty(toDate) = False Then
                sql.Append(toCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TO_DATE", Me.SetWhereData(toDate, LMF080DAC.ConditionPattern.OTHER), DBDataType.CHAR))
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
                sql.Append("          ,F04_01.SHIHARAITO_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.ARR_PLAN_DATE ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.DEST_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SHIHARAI_GROUP_NO ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SHIHARAI_GROUP_NO_M ")
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
                sql.Append("          ,F02_01.ARR_PLAN_DATE ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.CUST_CD_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.CUST_CD_S ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.CUST_CD_SS ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SHIHARAITO_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F02_01.DEST_CD ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SHIHARAI_GROUP_NO ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.SHIHARAI_GROUP_NO_M ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.UNSO_NO_L ")
                sql.Append(vbNewLine)
                sql.Append("          ,F04_01.UNSO_NO_M ")
                sql.Append(vbNewLine)
            End If

            Exit Sub

        End If

        '並び順を選択している場合、まとめ候補順に設定
        sql.Append(" ORDER BY  F02_01.ARR_PLAN_DATE ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.SHIHARAITO_CD ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.SHIHARAI_TARIFF_CD ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.SHIHARAI_ETARIFF_CD ")
        sql.Append(vbNewLine)
        sql.Append("          ,F04_01.TAX_KB ")
        sql.Append(vbNewLine)

    End Sub

    ''' <summary>
    ''' 抽出条件設定
    ''' </summary>
    ''' <param name="whereStr">条件の文字</param>
    ''' <param name="ptn">抽出パターン</param>
    ''' <returns>文字</returns>
    ''' <remarks></remarks>
    Private Function SetWhereData(ByVal whereStr As String, ByVal ptn As LMF080DAC.ConditionPattern) As String

        SetWhereData = String.Empty

        Select Case ptn

            Case LMF080DAC.ConditionPattern.PRE

                SetWhereData = String.Concat(whereStr, "%")

            Case LMF080DAC.ConditionPattern.ALL

                SetWhereData = String.Concat("%", whereStr, "%")

            Case LMF080DAC.ConditionPattern.OTHER

                SetWhereData = whereStr

        End Select

        Return SetWhereData

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

    ''' <summary>
    ''' 運送(大)タリフコード
    ''' </summary>
    ''' <param name="conditionRow">DataRow</param>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLTariffParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow

            If (LMF080DAC.SHUSEI_TARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", "10", DBDataType.CHAR))
            ElseIf (LMF080DAC.SHUSEI_YOKO).Equals(.Item("ITEM_DATA").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", "40", DBDataType.CHAR))
            ElseIf (LMF080DAC.SHUSEI_ETARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
                prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))
            End If

        End With

    End Sub

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
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
            prmList.Add(MyBase.GetSqlParameter("@DECI_UNCHIN", .Item("DECI_UNCHIN").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_CITY_EXTC", .Item("DECI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_WINT_EXTC", .Item("DECI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_RELY_EXTC", .Item("DECI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_TOLL", .Item("DECI_TOLL").ToString(), DBDataType.NUMERIC))
            prmList.Add(MyBase.GetSqlParameter("@DECI_INSU", .Item("DECI_INSU").ToString(), DBDataType.NUMERIC))

        End With

    End Sub

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
                MyBase.SetMessageStore(LMF080DAC.GUIDANCE_KBN, "E011", , Convert.ToInt32(Me._Row.Item("ROW_NO")).ToString())
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

            sql.Append(vbNewLine)

            Select Case .Item("ITEM_DATA").ToString()

                Case LMF080DAC.FIX_ACTION, LMF080DAC.FIX_CANCELL_ACTION

                    sql.Append(" SHIHARAI_FIXED_FLAG        = @SHIHARAI_FIXED_FLAG ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", .Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))

                Case LMF080DAC.FIX_ACTION_ANBUN

                    sql.Append(" SHIHARAI_FIXED_FLAG  = @SHIHARAI_FIXED_FLAG ")
                    sql.Append(vbNewLine)
                    sql.Append(",KANRI_UNCHIN         = @KANRI_UNCHIN ")
                    sql.Append(vbNewLine)
                    sql.Append(",KANRI_CITY_EXTC      = @KANRI_CITY_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",KANRI_WINT_EXTC      = @KANRI_WINT_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",KANRI_RELY_EXTC      = @KANRI_RELY_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",KANRI_TOLL           = @KANRI_TOLL ")
                    sql.Append(vbNewLine)
                    sql.Append(",KANRI_INSU           = @KANRI_INSU ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_UNCHIN          = @KANRI_UNCHIN ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_CITY_EXTC       = @KANRI_CITY_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_WINT_EXTC       = @KANRI_WINT_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_RELY_EXTC       = @KANRI_RELY_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_TOLL            = @KANRI_TOLL ")
                    sql.Append(vbNewLine)
                    sql.Append(",DECI_INSU            = @KANRI_INSU ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_FIXED_FLAG", .Item("SHIHARAI_FIXED_FLAG").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@KANRI_UNCHIN", .Item("KANRI_UNCHIN").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@KANRI_CITY_EXTC", .Item("KANRI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@KANRI_WINT_EXTC", .Item("KANRI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@KANRI_RELY_EXTC", .Item("KANRI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@KANRI_TOLL", .Item("KANRI_TOLL").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@KANRI_INSU", .Item("KANRI_INSU").ToString(), DBDataType.NUMERIC))

                Case LMF080DAC.GROUP_ACTION

                    sql.Append(" SHIHARAI_GROUP_NO      = @SHIHARAI_GROUP_NO ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_GROUP_NO_M    = @SHIHARAI_GROUP_NO_M ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", .Item("SHIHARAI_GROUP_NO").ToString(), DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", .Item("SHIHARAI_GROUP_NO_M").ToString(), DBDataType.CHAR))

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)

                    '備考の設定
                    sql.Append(",REMARK                 = @REMARK ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))

                Case LMF080DAC.GROUP_CANCELL_ACTION

                    sql.Append(" SHIHARAI_GROUP_NO          = @SHIHARAI_GROUP_NO ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_GROUP_NO_M        = @SHIHARAI_GROUP_NO_M ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO", String.Empty, DBDataType.CHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_GROUP_NO_M", String.Empty, DBDataType.CHAR))

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)
                    sql.Append(",SHIHARAI_UNCHIN           = @SHIHARAI_UNCHIN ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_CITY_EXTC        = @SHIHARAI_CITY_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_WINT_EXTC        = @SHIHARAI_WINT_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_RELY_EXTC        = @SHIHARAI_RELY_EXTC ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_TOLL             = @SHIHARAI_TOLL ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_INSU             = @SHIHARAI_INSU ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_UNCHIN", .Item("SHIHARAI_UNCHIN").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_CITY_EXTC", .Item("SHIHARAI_CITY_EXTC").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_WINT_EXTC", .Item("SHIHARAI_WINT_EXTC").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_RELY_EXTC", .Item("SHIHARAI_RELY_EXTC").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TOLL", .Item("SHIHARAI_TOLL").ToString(), DBDataType.NUMERIC))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_INSU", .Item("SHIHARAI_INSU").ToString(), DBDataType.NUMERIC))

                    'その他の設定
                    sql.Append(",SHIHARAI_TARIFF_CD         = @SHIHARAI_TARIFF_CD ")
                    sql.Append(vbNewLine)
                    sql.Append(",SHIHARAI_ETARIFF_CD        = @SHIHARAI_ETARIFF_CD ")
                    sql.Append(vbNewLine)
                    sql.Append(",REMARK                 = @REMARK ")
                    sql.Append(vbNewLine)
                    sql.Append(",TAX_KB                 = @TAX_KB ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("SHIHARAI_TARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("SHIHARAI_ETARIFF_CD").ToString(), DBDataType.NVARCHAR))
                    Dim groupL As String = .Item("SHIHARAI_GROUP_NO").ToString()
                    Dim groupM As String = .Item("SHIHARAI_GROUP_NO_M").ToString()

                    '親レコードの場合、初期化
                    If .Item("SHIHARAI_GROUP_NO").ToString().Equals(.Item("UNSO_NO_L").ToString()) = True _
                        AndAlso .Item("SHIHARAI_GROUP_NO_M").ToString().Equals(.Item("UNSO_NO_M").ToString()) = True _
                        Then
                        prmList.Add(MyBase.GetSqlParameter("@REMARK", String.Empty, DBDataType.NVARCHAR))
                    Else
                        prmList.Add(MyBase.GetSqlParameter("@REMARK", .Item("REMARK").ToString(), DBDataType.NVARCHAR))
                    End If

                    prmList.Add(MyBase.GetSqlParameter("@TAX_KB", .Item("TAX_KB").ToString(), DBDataType.CHAR))

                Case LMF080DAC.SHUSEI_SHIHARAI

                    sql.Append(" SHIHARAITO_CD              = @SHIHARAITO_CD ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAITO_CD", .Item("CD_L").ToString(), DBDataType.CHAR))

                Case LMF080DAC.SHUSEI_TARIFF, LMF080DAC.SHUSEI_YOKO

                    sql.Append(" SHIHARAI_TARIFF_CD         = @SHIHARAI_TARIFF_CD ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_TARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))

                    If (LMF080DAC.SHUSEI_TARIFF).Equals(.Item("ITEM_DATA").ToString()) = True Then
                        sql.Append(" ,SHIHARAI_TARIFF_BUNRUI_KB         = '10' ")
                        sql.Append(vbNewLine)
                    ElseIf (LMF080DAC.SHUSEI_YOKO).Equals(.Item("ITEM_DATA").ToString()) = True Then
                        sql.Append(" ,SHIHARAI_TARIFF_BUNRUI_KB         = '40' ")
                        sql.Append(vbNewLine)
                        sql.Append(" ,SHIHARAI_ETARIFF_CD               = '' ")
                        sql.Append(vbNewLine)
                    End If

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)

                Case LMF080DAC.SHUSEI_ETARIFF

                    sql.Append(" SHIHARAI_ETARIFF_CD        = @SHIHARAI_ETARIFF_CD ")
                    sql.Append(vbNewLine)
                    prmList.Add(MyBase.GetSqlParameter("@SHIHARAI_ETARIFF_CD", .Item("CD_L").ToString(), DBDataType.CHAR))

                    '数値系の設定
                    Call Me.SetUpdateKingakuSQL(prmList, dr, sql)

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
