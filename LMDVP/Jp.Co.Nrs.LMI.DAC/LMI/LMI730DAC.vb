' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI730    : 運賃差分抽出（JXTG）
'  作  成  者       :  katagiri
' ==========================================================================
Imports System.Text
Imports System.Data.SqlClient
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI730DACクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI730DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI730IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMI730OUT"

    ''' <summary>
    ''' 運賃バックアップ更新用テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_BKUNCHIN As String = "UNCHIN_BACKUP"

    ''' <summary>
    ''' 区分テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_KBN As String = "Z_KBN"

    ''' <summary>
    ''' DAC名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CLASS_NM As String = "LMI730DAC"

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


#End Region

#Region "検索処理 SQL"

#Region "Select句"

    Private Const SQL_SELECT_COUNT As String = " SELECT COUNT(F04_01.UNSO_NO_L)                     AS REC_CNT       " & vbNewLine

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
                                            & " END                             AS DEST_ADDR            " & vbNewLine _
                                            & ",BKUNCHIN.TARIFF_CD              AS BK_TARIFF_CD         " & vbNewLine _
                                            & ",BKUNCHIN.UNCHIN                 AS BK_UNCHIN            " & vbNewLine



#End Region

#Region "From句"

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
                                  & "  AND KBN_05.SYS_DEL_FLG           = '0'                   " & vbNewLine _
                                  & " LEFT JOIN $LM_TRN$..I_UNCHIN_BKUP_JX BKUNCHIN             " & vbNewLine _
                                  & "   ON BKUNCHIN.NRS_BR_CD           = F02_01.NRS_BR_CD      " & vbNewLine _
                                  & "  AND BKUNCHIN.UNSO_NO_L           = F02_01.UNSO_NO_L      " & vbNewLine _
                                  & "                                                           " & vbNewLine



#End Region

#Region "Where句"

    Private Const SQL_WHERE As String = "WHERE F02_01.NRS_BR_CD             = @NRS_BR_CD          " & vbNewLine _
                                      & "  AND F04_01.SYS_DEL_FLG           = '0'                 " & vbNewLine

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

#Region "Insert SQL"

    Private Const SQL_INSET_I_UNCHIN_BKUP_JX As String = "INSERT INTO $LM_TRN$..I_UNCHIN_BKUP_JX  " & vbNewLine _
                                                       & "           (NRS_BR_CD                   " & vbNewLine _
                                                       & "           ,UNSO_NO_L                   " & vbNewLine _
                                                       & "           ,TARIFF_CD                   " & vbNewLine _
                                                       & "           ,UNCHIN                      " & vbNewLine _
                                                       & "           ,SYS_ENT_DATE                " & vbNewLine _
                                                       & "           ,SYS_ENT_TIME                " & vbNewLine _
                                                       & "           ,SYS_ENT_PGID                " & vbNewLine _
                                                       & "           ,SYS_ENT_USER                " & vbNewLine _
                                                       & "           ,SYS_UPD_DATE                " & vbNewLine _
                                                       & "           ,SYS_UPD_TIME                " & vbNewLine _
                                                       & "           ,SYS_UPD_PGID                " & vbNewLine _
                                                       & "           ,SYS_UPD_USER                " & vbNewLine _
                                                       & "           ,SYS_DEL_FLG)                " & vbNewLine _
                                                       & "     VALUES                             " & vbNewLine _
                                                       & "           (@NRS_BR_CD                  " & vbNewLine _
                                                       & "           ,@UNSO_NO_L                  " & vbNewLine _
                                                       & "           ,@TARIFF_CD                  " & vbNewLine _
                                                       & "           ,@UNCHIN                     " & vbNewLine _
                                                       & "           ,@SYS_ENT_DATE               " & vbNewLine _
                                                       & "           ,@SYS_ENT_TIME               " & vbNewLine _
                                                       & "           ,@SYS_ENT_PGID               " & vbNewLine _
                                                       & "           ,@SYS_ENT_USER               " & vbNewLine _
                                                       & "           ,@SYS_UPD_DATE               " & vbNewLine _
                                                       & "           ,@SYS_UPD_TIME               " & vbNewLine _
                                                       & "           ,@SYS_UPD_PGID               " & vbNewLine _
                                                       & "           ,@SYS_UPD_USER               " & vbNewLine _
                                                       & "           ,@SYS_DEL_FLG)               " & vbNewLine


#End Region

#Region "UPDATE SQL"

    Private Const SQL_UPDATE_UNCHIN_BKUP_JX As String = "UPDATE $LM_TRN$..I_UNCHIN_BKUP_JX SET   " & vbNewLine _
                                                      & "      TARIFF_CD     = @TARIFF_CD        " & vbNewLine _
                                                      & "      ,UNCHIN       = @UNCHIN           " & vbNewLine _
                                                      & "      ,SYS_UPD_DATE = @SYS_UPD_DATE     " & vbNewLine _
                                                      & "      ,SYS_UPD_TIME = @SYS_UPD_TIME     " & vbNewLine _
                                                      & "      ,SYS_UPD_PGID = @SYS_UPD_PGID     " & vbNewLine _
                                                      & "      ,SYS_UPD_USER = @SYS_UPD_USER     " & vbNewLine _
                                                      & " WHERE                                  " & vbNewLine _
                                                      & "          NRS_BR_CD = @NRS_BR_CD        " & vbNewLine _
                                                      & "       AND UNSO_NO_L = @UNSO_NO_L       " & vbNewLine

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
        Dim inTbl As DataTable = ds.Tables(LMI730DAC.TABLE_NM_IN)
        Dim fromUnsoLL As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        fromUnsoLL = Me.GetUnsoLLSchema(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI730DAC.SQL_SELECT_COUNT)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_1)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_2)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_3)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_4)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_5)

        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI730DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        Dim inTbl As DataTable = ds.Tables(LMI730DAC.TABLE_NM_IN)
        Dim fromUnsoLL As String = String.Empty

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        fromUnsoLL = Me.GetUnsoLLSchema(ds)

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'SQL作成
        Me._StrSql.Append(LMI730DAC.SQL_SELECT_DATA)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_1)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_2)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_3)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_4)
        Me._StrSql.Append(fromUnsoLL)
        Me._StrSql.Append(LMI730DAC.SQL_FROM_5)
        Call Me.SetConditionMasterSQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Call Me.SetOrderBySQL(Me._SqlPrmList, Me._Row, Me._StrSql)
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@ORDER_BY", Me.SetWhereData(Me._Row.Item("ORDER_BY").ToString(), LMI730DAC.ConditionPattern.OTHER), DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(Me._StrSql.ToString(), Me._Row.Item("NRS_BR_CD").ToString())

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI730DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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
        map.Add("DEST_ADDR", "DEST_ADDR")
        map.Add("BK_TARIFF_CD", "BK_TARIFF_CD")
        map.Add("BK_UNCHIN", "BK_UNCHIN")

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI730DAC.TABLE_NM_OUT)

        Return ds

    End Function
#End Region

#Region "追加・更新"
    Private Function InsertBackupUnchin(ByVal ds As DataSet) As DataSet
        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI730DAC.TABLE_NM_BKUNCHIN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI730DAC.SQL_INSET_I_UNCHIN_BKUP_JX _
                                                                       , ds.Tables(LMI730DAC.TABLE_NM_BKUNCHIN).Rows(0).Item("NRS_BR_CD").ToString()))

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '更新（UPDATE）用データを除外する（後続のUpdateBackupUnchinで処理）
            If (Convert.ToBoolean(inTbl.Rows(i).Item("UPD_FLG")) = True) Then
                Continue For
            End If

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            'パラメータ設定
            Call Me.SetDataInsertParameter(Me._SqlPrmList)
            Call Me.SetBackUnchinParameter(Me._Row, Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMI730DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            MyBase.GetInsertResult(cmd)

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds
    End Function

    Private Function UpdateBackupUnchin(ByVal ds As DataSet) As DataSet
        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI730DAC.TABLE_NM_BKUNCHIN)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(Me.SetSchemaNm(LMI730DAC.SQL_UPDATE_UNCHIN_BKUP_JX _
                                                                       , ds.Tables(LMI730DAC.TABLE_NM_BKUNCHIN).Rows(0).Item("NRS_BR_CD").ToString()))

        'SQL格納変数の初期化
        Me._StrSql = New StringBuilder()

        Dim max As Integer = inTbl.Rows.Count - 1
        For i As Integer = 0 To max

            '追加用データを除外する
            If (Convert.ToBoolean(inTbl.Rows(i).Item("UPD_FLG")) = False) Then
                Continue For
            End If

            'SQLパラメータ初期化
            Me._SqlPrmList = New ArrayList()
            '条件rowの格納
            Me._Row = inTbl.Rows(i)

            Call Me.SetBackUnchinParameter(Me._Row, Me._SqlPrmList)
            Call Me.SetSysdataParameter(Me._SqlPrmList)

            'パラメータの反映
            For Each obj As Object In Me._SqlPrmList
                cmd.Parameters.Add(obj)
            Next

            MyBase.Logger.WriteSQLLog(LMI730DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            If (Me.UpdateResultChk(cmd) = False) Then
                Return ds
            End If

            'パラメータの初期化
            cmd.Parameters.Clear()

        Next

        Return ds
    End Function
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
            sql.Append(LMI730DAC.SQL_WHERE)

            '営業所コード
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", Me.SetWhereData(.Item("NRS_BR_CD").ToString(), LMI730DAC.ConditionPattern.OTHER), DBDataType.CHAR))

            '日付絞込
            Call Me.SetConditionDateSQL(prmList, dr, sql)

            'タリフコード
            whereStr = .Item("SEIQ_TARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SEIQ_TARIFF_CD LIKE @SEIQ_TARIFF_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_TARIFF_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '割増タリフコード
            whereStr = .Item("SEIQ_ETARIFF_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F04_01.SEIQ_ETARIFF_CD LIKE @SEIQ_ETARIFF_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_ETARIFF_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '乗務員コード
            whereStr = .Item("DRIVER_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND ( F01_01.DRIVER_CD LIKE @DRIVER_CD OR F01_02.DRIVER_CD LIKE @DRIVER_CD OR F01_03.DRIVER_CD LIKE @DRIVER_CD OR F01_04.DRIVER_CD LIKE @DRIVER_CD )")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DRIVER_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))
            End If

            '荷主(大)コード
            whereStr = .Item("CUST_CD_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_L = @CUST_CD_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_L", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主(中)コード
            whereStr = .Item("CUST_CD_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND F02_01.CUST_CD_M = @CUST_CD_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_CD_M", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            '荷主名
            whereStr = .Item("CUST_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then
                sql.Append(" AND M07_01.CUST_NM_L + '　' + M07_01.CUST_NM_M LIKE @CUST_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))
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
                prmList.Add(MyBase.GetSqlParameter("@UNCHIN_KBN", Me.SetWhereData(LMI730DAC.TARIFF_KURUMA, LMI730DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

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
                prmList.Add(MyBase.GetSqlParameter("@KAKUTEI_KB", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            '元データ区分
            whereStr = .Item("MOTO_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.MOTO_DATA_KB = @MOTO_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@MOTO_KB", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If
            '要望管理2244対応
            strMotoDataKbn = whereStr

            '請求先コード
            whereStr = .Item("SEIQTO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQTO_CD LIKE @SEIQTO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQTO_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '請求先名
            whereStr = .Item("SEIQTO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M06_01.SEIQTO_NM LIKE @SEIQTO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQTO_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

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
                prmList.Add(MyBase.GetSqlParameter("@DEST_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社名(1次)
            whereStr = .Item("UNSO_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_01.UNSOCO_NM + '　' + M38_01.UNSOCO_BR_NM LIKE @UNSO_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

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
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            'タリフ分類
            whereStr = .Item("TARIFF_BUNRUI_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_TARIFF_BUNRUI_KB = @TARIFF_BUNRUI_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TARIFF_BUNRUI_KB", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            '課税区分
            whereStr = .Item("TAX_KB").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.TAX_KB = @TAX_KB")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TAX_KB", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.OTHER), DBDataType.NVARCHAR))

            End If

            'まとめ番号
            whereStr = .Item("SEIQ_GROUP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_GROUP_NO LIKE @SEIQ_GROUP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            'まとめ番号M
            whereStr = .Item("SEIQ_GROUP_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.SEIQ_GROUP_NO_M LIKE @SEIQ_GROUP_NO_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SEIQ_GROUP_NO_M", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '備考
            whereStr = .Item("REMARK").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.REMARK LIKE @REMARK")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@REMARK", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '管理番号
            whereStr = .Item("INOUTKA_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.INOUTKA_NO_L LIKE @INOUTKA_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@INOUTKA_NO_L", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送番号L
            whereStr = .Item("UNSO_NO_L").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.UNSO_NO_L LIKE @UNSO_NO_L")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.CHAR))


            End If

            '運送番号M
            whereStr = .Item("UNSO_NO_M").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F04_01.UNSO_NO_M LIKE @UNSO_NO_M")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_M", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.CHAR))
                
            End If

            '運行番号
            whereStr = .Item("TRIP_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO LIKE @TRIP_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '集荷中継地
            whereStr = .Item("SYUKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M12_01.KEN + M12_01.SHI LIKE @SYUKA_TYUKEI_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@SYUKA_TYUKEI_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '配荷中継地
            whereStr = .Item("HAIKA_TYUKEI_NM").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M12_02.KEN + M12_02.SHI LIKE @HAIKA_TYUKEI_NM")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@HAIKA_TYUKEI_NM", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運行番号(集荷)
            whereStr = .Item("TRIP_NO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO_SYUKA LIKE @TRIP_NO_SYUKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_SYUKA", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運行番号(中継)
            whereStr = .Item("TRIP_NO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO_TYUKEI LIKE @TRIP_NO_TYUKEI")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_TYUKEI", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運行番号(配荷)
            whereStr = .Item("TRIP_NO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.TRIP_NO_HAIKA LIKE @TRIP_NO_HAIKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TRIP_NO_HAIKA", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社名(集荷)
            whereStr = .Item("UNSOCO_SYUKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_03.UNSOCO_NM + '　' + M38_03.UNSOCO_BR_NM LIKE @UNSOCO_SYUKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_SYUKA", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社名(中継)
            whereStr = .Item("UNSOCO_TYUKEI").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_04.UNSOCO_NM + '　' + M38_04.UNSOCO_BR_NM LIKE @UNSOCO_TYUKEI")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_TYUKEI", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

            End If

            '運送会社名(集荷)
            whereStr = .Item("UNSOCO_HAIKA").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M38_05.UNSOCO_NM + '　' + M38_05.UNSOCO_BR_NM LIKE @UNSOCO_HAIKA")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_HAIKA", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.ALL), DBDataType.NVARCHAR))

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
                prmList.Add(MyBase.GetSqlParameter("@DEST_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '届先JIS
            whereStr = .Item("DEST_JIS_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND M10_01.JIS LIKE @DEST_JIS_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@DEST_JIS_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社コード(1次)
            whereStr = .Item("UNSO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.UNSO_CD LIKE @UNSO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社支店コード(1次)
            whereStr = .Item("UNSO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.UNSO_BR_CD LIKE @UNSO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSO_BR_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If



            '運送会社コード(2次)
            whereStr = .Item("UNSOCO_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F01_01.UNSOCO_CD LIKE @UNSOCO_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '運送会社支店コード(2次)
            whereStr = .Item("UNSOCO_BR_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F01_01.UNSOCO_BR_CD LIKE @UNSOCO_BR_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@UNSOCO_BR_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            'START YANAI 20120622 DIC運賃まとめ及び再計算対応
            '伝票№
            whereStr = .Item("CUST_REF_NO").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND F02_01.CUST_REF_NO LIKE @CUST_REF_NO")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@CUST_REF_NO", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '在庫部課
            whereStr = .Item("ZBUKA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND UNSOM.ZBUKA_CD LIKE @ZBUKA_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@ZBUKA_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

            End If

            '扱い部課
            whereStr = .Item("ABUKA_CD").ToString()
            If String.IsNullOrEmpty(whereStr) = False Then

                sql.Append(" AND UNSOM.ABUKA_CD LIKE @ABUKA_CD")
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@ABUKA_CD", Me.SetWhereData(whereStr, LMI730DAC.ConditionPattern.PRE), DBDataType.NVARCHAR))

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

                Case LMI730DAC.DATE_KBN_NONYU

                    fromCondition = " AND F02_01.ARR_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.ARR_PLAN_DATE <= @TO_DATE "

                Case LMI730DAC.DATE_KBN_SHUKKA

                    fromCondition = " AND F02_01.OUTKA_PLAN_DATE >= @FROM_DATE "
                    toCondition = " AND F02_01.OUTKA_PLAN_DATE <= @TO_DATE "

            End Select

            If String.IsNullOrEmpty(fromDate) = False Then
                sql.Append(fromCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@FROM_DATE", Me.SetWhereData(fromDate, LMI730DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

            If String.IsNullOrEmpty(toDate) = False Then
                sql.Append(toCondition)
                sql.Append(vbNewLine)
                prmList.Add(MyBase.GetSqlParameter("@TO_DATE", Me.SetWhereData(toDate, LMI730DAC.ConditionPattern.OTHER), DBDataType.CHAR))
            End If

        End With

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

            Case LMI730DAC.ORDER_BY_CUSTTRIP

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

            Case LMI730DAC.ORDER_BY_DEST

                sql.Append("          ,F02_01.DEST_CD ")
                sql.Append(vbNewLine)

            Case LMI730DAC.ORDER_BY_DESTJIS

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
    Private Function SetWhereData(ByVal whereStr As String, ByVal ptn As LMI730DAC.ConditionPattern) As String

        SetWhereData = String.Empty

        Select Case ptn

            Case LMI730DAC.ConditionPattern.PRE

                SetWhereData = String.Concat(whereStr, "%")

            Case LMI730DAC.ConditionPattern.ALL

                SetWhereData = String.Concat("%", whereStr, "%")

            Case LMI730DAC.ConditionPattern.OTHER

                SetWhereData = whereStr

        End Select

        Return SetWhereData

    End Function

#End Region

#Region "設定処理"

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
        If ds.Tables(LMI730DAC.TABLE_NM_KBN).Rows.Count = 0 Then
            rtnFrom = "$LM_TRN$..F_UNSO_LL"
            Return rtnFrom
        End If

        '０件ではない場合、またぎ営業所のスキーマを設定
        Dim matagiBr As String = String.Empty
        rtnFrom = "(SELECT * FROM $LM_TRN$..F_UNSO_LL "
        For i As Integer = 0 To ds.Tables(LMI730DAC.TABLE_NM_KBN).Rows.Count - 1
            matagiBr = ds.Tables(LMI730DAC.TABLE_NM_KBN).Rows(i).Item("KBN_NM2").ToString()
            rtnFrom = String.Concat(rtnFrom, " UNION SELECT * FROM ", MyBase.GetDatabaseName(matagiBr, DBKbn.TRN), "..F_UNSO_LL ")
        Next
        rtnFrom = String.Concat(rtnFrom, ")")

        Return rtnFrom

    End Function


    ''' <summary>
    ''' 区分マスタ取得
    ''' </summary>
    ''' <param name="kbnGrpCd">String</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnData(ByVal ds As DataSet, ByVal kbnGrpCd As String) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMI730DAC.TABLE_NM_IN)

        'INTableの条件rowの格納
        Me._Row = inTbl.Rows(0)

        'SQLパラメータ初期化
        Me._SqlPrmList = New ArrayList()

        'パラメータ設定
        Dim brCd As String = Me._Row.Item("NRS_BR_CD").ToString()
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@KBN_GROUP_CD", kbnGrpCd, DBDataType.CHAR))
        Me._SqlPrmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

        'スキーマ名設定
        Dim sql As String = Me.SetSchemaNm(LMI730DAC.SQL_SELECT_KBN_DATA, brCd)

        'SQL文のコンパイル
        Dim cmd As SqlCommand = MyBase.CreateSqlCommand(sql)

        'パラメータの反映
        For Each obj As Object In Me._SqlPrmList
            cmd.Parameters.Add(obj)
        Next

        MyBase.Logger.WriteSQLLog(LMI730DAC.CLASS_NM, System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

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

        ds = MyBase.SetSelectResultToDataSet(map, ds, reader, LMI730DAC.TABLE_NM_KBN)

        Return ds

    End Function

#End Region


#Region "パラメータ設定"
    ''' <summary>
    ''' 新規登録の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetDataInsertParameter(ByVal prmList As ArrayList)

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

        Call Me.SetSysdataParameter(prmList)

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemPGID As String = MyBase.GetPGID()
        Dim systemUserID As String = MyBase.GetUserID()

        Call Me.SetSysdataTimeParameter(prmList)
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", systemPGID, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", systemUserID, DBDataType.NVARCHAR))

    End Sub

    ''' <summary>
    ''' 更新時の共通パラメータ設定
    ''' </summary>
    ''' <param name="prmList">パラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetSysdataTimeParameter(ByVal prmList As ArrayList)

        'システム項目
        Dim systemDate As String = MyBase.GetSystemDate()
        Dim systemTime As String = MyBase.GetSystemTime()

        '更新日時
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", systemDate, DBDataType.CHAR))
        prmList.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", systemTime, DBDataType.CHAR))

    End Sub

    Private Sub SetBackUnchinParameter(ByVal conditionRow As DataRow, ByVal prmList As ArrayList)

        With conditionRow
            prmList.Add(MyBase.GetSqlParameter("@NRS_BR_CD", .Item("NRS_BR_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNSO_NO_L", .Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@TARIFF_CD", .Item("TARIFF_CD").ToString(), DBDataType.CHAR))
            prmList.Add(MyBase.GetSqlParameter("@UNCHIN", Me.FormatNumValue(.Item("UNCHIN").ToString()), DBDataType.NUMERIC))

        End With

    End Sub

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

#End Region

#End Region

End Class
