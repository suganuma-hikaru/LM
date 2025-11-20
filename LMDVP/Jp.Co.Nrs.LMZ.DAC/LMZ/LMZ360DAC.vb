' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ360DAC : Tra-Net連携共通処理
'  作  成  者       :  kumakura
' ==========================================================================
Imports System.Data.SqlClient
Imports System.Net.Http
Imports System.Text
Imports System.Web.Script.Serialization
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMZ360DACクラス
''' </summary>
Public Class LMZ360DAC
    Inherits Jp.Co.Nrs.LM.Base.DAC.LMBaseDAC

#Region "Const"

#Region "制御用"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const IN_TBL_NM As String = "LMZ360IN"

    ''' <summary>
    ''' 運送LLテーブル ABHB910
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ABHB910_UNSO_LL As String = "ABHB910IN_UNSO_LL"

    ''' <summary>
    ''' 運送Lテーブル ABHB910
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ABHB910_UNSO_L As String = "ABHB910IN_UNSO_L"

    ''' <summary>
    ''' 運送Mテーブル ABHB910
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ABHB910_UNSO_M As String = "ABHB910IN_UNSO_M"

    ''' <summary>
    ''' 支払運賃テーブル ABHB910
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ABHB910_SHIHARAI_TRS As String = "ABHB910IN_SHIHARAI_TRS"

    ''' <summary>
    ''' 入出荷棟室テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_INOUTKA_TOUSITU As String = "INOUTKA_TOUSITU"

#End Region '制御用

#Region "SQL"

#Region "データ取得用"

    Private Const SQL_SELECT_UNSO_LL_DATA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        ISNULL(CNV.NRS_BR_CD, UNSO_LL.NRS_BR_CD)  AS NRS_BR_CD           " & vbNewLine _
        & "      , UNSO_LL.TRIP_NO                                                  " & vbNewLine _
        & "      , UNSO_LL.UNSOCO_CD                                                " & vbNewLine _
        & "      , UNSO_LL.UNSOCO_BR_CD                                             " & vbNewLine _
        & "      , UNSO_LL.JSHA_KB                                                  " & vbNewLine _
        & "      , UNSO_LL.BIN_KB                                                   " & vbNewLine _
        & "      , VCLE.CAR_NO AS CAR_KEY                                           " & vbNewLine _
        & "      , UNSO_LL.UNSO_ONDO                                                " & vbNewLine _
        & "      , UNSO_LL.DRIVER_CD                                                " & vbNewLine _
        & "      , UNSO_LL.TRIP_DATE                                                " & vbNewLine _
        & "      , UNSO_LL.PAY_UNCHIN                                               " & vbNewLine _
        & "      , UNSO_LL.PAY_TARIFF_CD                                            " & vbNewLine _
        & "      , UNSO_LL.HAISO_KB                                                 " & vbNewLine _
        & "      , UNSO_LL.REMARK                                                   " & vbNewLine _
        & "      , UNSO_LL.SHIHARAI_TARIFF_CD                                       " & vbNewLine _
        & "      , UNSO_LL.SHIHARAI_ETARIFF_CD                                      " & vbNewLine _
        & "      , UNSO_LL.SHIHARAI_UNSO_WT                                         " & vbNewLine _
        & "      , UNSO_LL.SHIHARAI_COUNT                                           " & vbNewLine _
        & "      , UNSO_LL.SHIHARAI_UNCHIN                                          " & vbNewLine _
        & "      , UNSO_LL.SHIHARAI_TARIFF_BUNRUI_KB                                " & vbNewLine _
        & "   FROM $LM_TRN$..F_UNSO_LL UNSO_LL                                      " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_VCLE VCLE                                            " & vbNewLine _
        & "     ON VCLE.NRS_BR_CD                   = UNSO_LL.NRS_BR_CD             " & vbNewLine _
        & "    AND VCLE.CAR_KEY                     = UNSO_LL.CAR_KEY               " & vbNewLine _
        & "    AND VCLE.SYS_DEL_FLG                 = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..S_USER  S_USER                                         " & vbNewLine _
        & "     ON S_USER.USER_CD                   = @USER_CD                      " & vbNewLine _
        & "    AND S_USER.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_NRS_BR  CNV                                        " & vbNewLine _
        & "     ON CNV.NRS_BR_CD_LM                 = UNSO_LL.NRS_BR_CD             " & vbNewLine _
        & "    AND CNV.WH_CD                        = S_USER.WH_CD                  " & vbNewLine _
        & "    AND CNV.SYS_DEL_FLG                  = '0'                           " & vbNewLine _
        & "  WHERE UNSO_LL.NRS_BR_CD                = @NRS_BR_CD                    " & vbNewLine _
        & "    AND UNSO_LL.SYS_DEL_FLG              = '0'                           " & vbNewLine

    Private Const SQL_ORDER_UNSO_LL_DATA As String _
        = "  ORDER BY                                                               " & vbNewLine _
        & "        UNSO_LL.TRIP_NO                                                  " & vbNewLine

    Private Const SQL_SELECT_UNSO_L_DATA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        ISNULL(CNV_SEND.CNV_NRS_BR_CD, ISNULL(CNV.NRS_BR_CD, UNSO_L.NRS_BR_CD))  AS NRS_BR_CD  " & vbNewLine _
        & "      , UNSO_L.UNSO_NO_L                                                 " & vbNewLine _
        & "      , ISNULL(CNV_SEND.CNV_NRS_BR_CD, ISNULL(CNV.NRS_BR_CD, UNSO_L.YUSO_BR_CD))  AS YUSO_BR_CD  " & vbNewLine _
        & "      , UNSO_L.INOUTKA_NO_L                                              " & vbNewLine _
        & "      , UNSO_L.TRIP_NO                                                   " & vbNewLine _
        & "      , ISNULL(CNV_SEND.CNV_UNSO_CD, ISNULL(UNSO_LL.UNSOCO_CD, UNSO_L.UNSO_CD))  AS UNSO_CD  " & vbNewLine _
        & "      , ISNULL(CNV_SEND.CNV_UNSO_BR_CD, ISNULL(UNSO_LL.UNSOCO_BR_CD, UNSO_L.UNSO_BR_CD))  AS UNSO_BR_CD  " & vbNewLine _
        & "      , L_UNCHIN_TRS.SEIQTO_CD                                           " & vbNewLine _
        & "      , SEIQTO.NRS_KEIRI_CD2                     AS UNSO_BP_CD           " & vbNewLine _
        & "      , UNSO_L.BIN_KB                                                    " & vbNewLine _
        & "      , UNSO_L.JIYU_KB                                                   " & vbNewLine _
        & "      , UNSO_L.DENP_NO                                                   " & vbNewLine _
        & "      , UNSO_L.AUTO_DENP_KBN                                             " & vbNewLine _
        & "      , UNSO_L.AUTO_DENP_NO                                              " & vbNewLine _
        & "      , UNSO_L.OUTKA_PLAN_DATE                                           " & vbNewLine _
        & "      , UNSO_L.OUTKA_PLAN_TIME                                           " & vbNewLine _
        & "      , UNSO_L.ARR_PLAN_DATE                                             " & vbNewLine _
        & "      , UNSO_L.ARR_PLAN_TIME                                             " & vbNewLine _
        & "      , UNSO_L.ARR_ACT_TIME                                              " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN UNSO_L.CUST_CD_L                                   " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS CUST_CD_L                                                " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN UNSO_L.CUST_CD_M                                   " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS CUST_CD_M                                                " & vbNewLine _
        & "      , CUST.CUST_NM_L                           AS CUST_NM_L            " & vbNewLine _
        & "      , CUST.PRODUCT_SEG_CD                                              " & vbNewLine _
        & "      , CUST.TCUST_BPCD                                                  " & vbNewLine _
        & "      , UNSO_L.CUST_REF_NO                                               " & vbNewLine _
        & "      , UNSO_L.SHIP_CD                                                   " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN UNSO_L.ORIG_CD                                     " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS ORIG_CD                                                  " & vbNewLine _
        & "      , ISNULL(ORIG.DEST_NM, Z_ORIG.DEST_NM)     AS ORIG_NM              " & vbNewLine _
        & "      , ISNULL(ORIG.KANA_NM, Z_ORIG.KANA_NM)     AS ORIG_KANA_NM         " & vbNewLine _
        & "      , ISNULL(ORIG.ZIP, Z_ORIG.ZIP)             AS ORIG_ZIP             " & vbNewLine _
        & "      , ISNULL(ORIG.AD_1, Z_ORIG.AD_1)           AS ORIG_AD_1            " & vbNewLine _
        & "      , ISNULL(ORIG.AD_2, Z_ORIG.AD_2)           AS ORIG_AD_2            " & vbNewLine _
        & "      , ISNULL(ORIG.AD_3, Z_ORIG.AD_3)           AS ORIG_AD_3            " & vbNewLine _
        & "      , ISNULL(ORIG.TEL, Z_ORIG.TEL)             AS ORIG_TEL             " & vbNewLine _
        & "      , ISNULL(ORIG.FAX, Z_ORIG.FAX)             AS ORIG_FAX             " & vbNewLine _
        & "      , ISNULL(ORIG_JIS.KEN, Z_ORIG_JIS.KEN)     AS ORIG_KEN             " & vbNewLine _
        & "      , ISNULL(ORIG_JIS.SHI, Z_ORIG_JIS.SHI)     AS ORIG_SHI             " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN UNSO_L.DEST_CD                                     " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS DEST_CD                                                  " & vbNewLine _
        & "      , ISNULL(DEST.DEST_NM, Z_DEST.DEST_NM)     AS DEST_NM              " & vbNewLine _
        & "      , ISNULL(DEST.KANA_NM, Z_DEST.KANA_NM)     AS DEST_KANA_NM         " & vbNewLine _
        & "      , ISNULL(DEST.ZIP, Z_DEST.ZIP)             AS DEST_ZIP             " & vbNewLine _
        & "      , ISNULL(DEST.AD_1, Z_DEST.AD_1)           AS DEST_AD_1            " & vbNewLine _
        & "      , ISNULL(DEST.AD_2, Z_DEST.AD_2)           AS DEST_AD_2            " & vbNewLine _
        & "      , ISNULL(DEST.AD_3, Z_DEST.AD_3)           AS DEST_AD_3            " & vbNewLine _
        & "      , ISNULL(DEST.TEL, Z_DEST.TEL)             AS DEST_TEL             " & vbNewLine _
        & "      , ISNULL(DEST.FAX, Z_DEST.FAX)             AS DEST_FAX             " & vbNewLine _
        & "      , ISNULL(DEST_JIS.KEN, Z_DEST_JIS.KEN)     AS DEST_KEN             " & vbNewLine _
        & "      , ISNULL(DEST_JIS.SHI, Z_DEST_JIS.SHI)     AS DEST_SHI             " & vbNewLine _
        & "      , UNSO_L.UNSO_PKG_NB                                               " & vbNewLine _
        & "      , ABKBN_NB_UT.KBN_CD                       AS NB_UT                " & vbNewLine _
        & "      , UNSO_L.UNSO_WT                                                   " & vbNewLine _
        & "      , ABKBN_ONDO_KB.KBN_CD                     AS UNSO_ONDO_KB         " & vbNewLine _
        & "      , UNSO_L.PC_KB                                                     " & vbNewLine _
        & "      , UNSO_L.TARIFF_BUNRUI_KB                                          " & vbNewLine _
        & "      , ABKBN_VCLE_KB.KBN_CD                     AS VCLE_KB              " & vbNewLine _
        & "      , UNSO_L.MOTO_DATA_KB                                              " & vbNewLine _
        & "      , UNSO_L.TAX_KB                                                    " & vbNewLine _
        & "      , UNSO_L.REMARK                                                    " & vbNewLine _
        & "      , UNSO_L.SEIQ_TARIFF_CD                                            " & vbNewLine _
        & "      , UNSO_L.SEIQ_ETARIFF_CD                                           " & vbNewLine _
        & "      , UNSO_L.AD_3                                                      " & vbNewLine _
        & "      , UNSO_L.UNSO_TEHAI_KB                                             " & vbNewLine _
        & "      , UNSO_L.BUY_CHU_NO                                                " & vbNewLine _
        & "      , UNSO_L.AREA_CD                                                   " & vbNewLine _
        & "      , UNSO_L.TYUKEI_HAISO_FLG                                          " & vbNewLine _
        & "      , UNSO_L.SYUKA_TYUKEI_CD                                           " & vbNewLine _
        & "      , UNSO_L.HAIKA_TYUKEI_CD                                           " & vbNewLine _
        & "      , UNSO_L.TRIP_NO_SYUKA                                             " & vbNewLine _
        & "      , UNSO_L.TRIP_NO_TYUKEI                                            " & vbNewLine _
        & "      , UNSO_L.TRIP_NO_HAIKA                                             " & vbNewLine _
        & "      , UNSO_L.SHIHARAI_TARIFF_CD                                        " & vbNewLine _
        & "      , UNSO_L.SHIHARAI_ETARIFF_CD                                       " & vbNewLine _
        & "      , UNSO_L.MAIN_DELI_KB                                              " & vbNewLine _
        & "      , UNSO_L.HAISO_PF_SEND_FLG                                         " & vbNewLine _
        & "      , UNSO_L.NHS_REMARK                                                " & vbNewLine _
        & "      , UNSO_L.SYS_UPD_DATE                                              " & vbNewLine _
        & "      , UNSO_L.SYS_UPD_TIME                                              " & vbNewLine _
        & "   FROM $LM_TRN$..F_UNSO_L UNSO_L                                        " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..F_UNSO_LL UNSO_LL                                      " & vbNewLine _
        & "     ON UNSO_LL.TRIP_NO                  = UNSO_L.TRIP_NO                " & vbNewLine _
        & "    AND UNSO_LL.SYS_DEL_FLG              = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_CUST CUST                                            " & vbNewLine _
        & "     ON CUST.NRS_BR_CD                   = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND CUST.CUST_CD_L                   = UNSO_L.CUST_CD_L              " & vbNewLine _
        & "    AND CUST.CUST_CD_M                   = UNSO_L.CUST_CD_M              " & vbNewLine _
        & "    AND CUST.CUST_CD_S                   = '00'                          " & vbNewLine _
        & "    AND CUST.CUST_CD_SS                  = '00'                          " & vbNewLine _
        & "    AND CUST.SYS_DEL_FLG                 = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_DEST ORIG                                            " & vbNewLine _
        & "     ON ORIG.NRS_BR_CD                   = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND ORIG.CUST_CD_L                   = UNSO_L.CUST_CD_L              " & vbNewLine _
        & "    AND ORIG.DEST_CD                     = UNSO_L.ORIG_CD                " & vbNewLine _
        & "    AND ORIG.SYS_DEL_FLG                 = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_DEST Z_ORIG                                          " & vbNewLine _
        & "     ON Z_ORIG.NRS_BR_CD                 = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND Z_ORIG.CUST_CD_L                 = 'ZZZZZ'                       " & vbNewLine _
        & "    AND Z_ORIG.DEST_CD                   = UNSO_L.ORIG_CD                " & vbNewLine _
        & "    AND Z_ORIG.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_DEST DEST                                            " & vbNewLine _
        & "     ON DEST.NRS_BR_CD                   = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND DEST.CUST_CD_L                   = UNSO_L.CUST_CD_L              " & vbNewLine _
        & "    AND DEST.DEST_CD                     = UNSO_L.DEST_CD                " & vbNewLine _
        & "    AND DEST.SYS_DEL_FLG                 = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_DEST Z_DEST                                          " & vbNewLine _
        & "     ON Z_DEST.NRS_BR_CD                 = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND Z_DEST.CUST_CD_L                 = 'ZZZZZ'                       " & vbNewLine _
        & "    AND Z_DEST.DEST_CD                   = UNSO_L.DEST_CD                " & vbNewLine _
        & "    AND Z_DEST.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_JIS ORIG_JIS                                         " & vbNewLine _
        & "     ON ORIG_JIS.JIS_CD                  = ORIG.JIS                      " & vbNewLine _
        & "    AND ORIG_JIS.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_JIS Z_ORIG_JIS                                       " & vbNewLine _
        & "     ON Z_ORIG_JIS.JIS_CD                = Z_ORIG.JIS                    " & vbNewLine _
        & "    AND Z_ORIG_JIS.SYS_DEL_FLG           = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_JIS DEST_JIS                                         " & vbNewLine _
        & "     ON DEST_JIS.JIS_CD                  = DEST.JIS                      " & vbNewLine _
        & "    AND DEST_JIS.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_JIS Z_DEST_JIS                                       " & vbNewLine _
        & "     ON Z_DEST_JIS.JIS_CD                = Z_DEST.JIS                    " & vbNewLine _
        & "    AND Z_DEST_JIS.SYS_DEL_FLG           = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_NB_UT                                        " & vbNewLine _
        & "     ON ABKBN_NB_UT.KBN_GROUP_CD         = 'N10014'                      " & vbNewLine _
        & "    AND ABKBN_NB_UT.KBN_LANG             = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_NB_UT.KBN_CD_REM           = UNSO_L.NB_UT                  " & vbNewLine _
        & "    AND ABKBN_NB_UT.SYS_DEL_FLG          = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_ONDO_KB                                      " & vbNewLine _
        & "     ON ABKBN_ONDO_KB.KBN_GROUP_CD       = 'U10009'                      " & vbNewLine _
        & "    AND ABKBN_ONDO_KB.KBN_LANG           = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_ONDO_KB.KBN_CD_REM         = UNSO_L.UNSO_ONDO_KB           " & vbNewLine _
        & "    AND ABKBN_ONDO_KB.SYS_DEL_FLG        = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_VCLE_KB                                      " & vbNewLine _
        & "     ON ABKBN_VCLE_KB.KBN_GROUP_CD       = 'S10091'                      " & vbNewLine _
        & "    AND ABKBN_VCLE_KB.KBN_LANG           = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_VCLE_KB.KBN_CD_REM         = UNSO_L.VCLE_KB                " & vbNewLine _
        & "    AND ABKBN_VCLE_KB.SYS_DEL_FLG        = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        (                                                                " & vbNewLine _
        & "          SELECT UNCHIN.UNSO_NO_L                                        " & vbNewLine _
        & "               , MAX(UNCHIN.SEIQTO_CD)           AS SEIQTO_CD            " & vbNewLine _
        & "            FROM $LM_TRN$..F_UNCHIN_TRS UNCHIN                           " & vbNewLine _
        & "           WHERE UNCHIN.NRS_BR_CD        = @NRS_BR_CD                    " & vbNewLine _
        & "             AND UNCHIN.SYS_DEL_FLG      = '0'                           " & vbNewLine _
        & "           GROUP BY                                                      " & vbNewLine _
        & "                 UNCHIN.UNSO_NO_L                                        " & vbNewLine _
        & "        ) L_UNCHIN_TRS                                                   " & vbNewLine _
        & "     ON L_UNCHIN_TRS.UNSO_NO_L           = UNSO_L.UNSO_NO_L              " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_SEIQTO SEIQTO                                        " & vbNewLine _
        & "     ON SEIQTO.NRS_BR_CD                 = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND SEIQTO.SEIQTO_CD                 = L_UNCHIN_TRS.SEIQTO_CD        " & vbNewLine _
        & "    AND SEIQTO.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..S_USER  S_USER                                         " & vbNewLine _
        & "     ON S_USER.USER_CD                   = @USER_CD                      " & vbNewLine _
        & "    AND S_USER.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_NRS_BR  CNV                                        " & vbNewLine _
        & "     ON CNV.NRS_BR_CD_LM                 = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND CNV.WH_CD                        = S_USER.WH_CD                  " & vbNewLine _
        & "    AND CNV.SYS_DEL_FLG                  = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_SEND_NRS_BR  CNV_SEND                              " & vbNewLine _
        & "     ON CNV_SEND.NRS_BR_CD               = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND CNV_SEND.CUST_CD_L               = UNSO_L.CUST_CD_L              " & vbNewLine _
        & "    AND CNV_SEND.CUST_CD_M               = UNSO_L.CUST_CD_M              " & vbNewLine _
        & "    AND CNV_SEND.UNSO_CD                 = UNSO_L.UNSO_CD                " & vbNewLine _
        & "    AND CNV_SEND.UNSO_BR_CD              = UNSO_L.UNSO_BR_CD             " & vbNewLine _
        & "    AND UNSO_L.TRIP_NO                   = ''                            " & vbNewLine _
        & "    AND CNV_SEND.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE UNSO_L.NRS_BR_CD                 = @NRS_BR_CD                    " & vbNewLine _
        & "    AND UNSO_L.SYS_DEL_FLG               = '0'                           " & vbNewLine

    Private Const SQL_ORDER_UNSO_L_DATA As String _
        = "  ORDER BY                                                               " & vbNewLine _
        & "        UNSO_L.UNSO_NO_L                                                 " & vbNewLine

    Private Const SQL_SELECT_UNSO_M_DATA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        ISNULL(CNV_SEND.CNV_NRS_BR_CD, ISNULL(CNV.NRS_BR_CD, UNSO_M.NRS_BR_CD))  AS NRS_BR_CD  " & vbNewLine _
        & "      , UNSO_M.UNSO_NO_L                                                 " & vbNewLine _
        & "      , UNSO_M.UNSO_NO_M                                                 " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN UNSO_M.GOODS_CD_NRS                                " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS GOODS_CD_NRS                                             " & vbNewLine _
        & "      , UNSO_M.GOODS_NM                                                  " & vbNewLine _
        & "      , KBN_SHOBO.KBN_NM1 + SHOBO.HINMEI         AS SHOBO_NM             " & vbNewLine _
        & "      , KBN_DOKU.KBN_NM1                         AS DOKU_NM              " & vbNewLine _
        & "      , UNSO_M.UNSO_TTL_NB                                               " & vbNewLine _
        & "      , ABKBN_NB_UT.KBN_CD                       AS NB_UT                " & vbNewLine _
        & "      , UNSO_M.UNSO_TTL_QT                                               " & vbNewLine _
        & "      , ABKBN_QT_UT.KBN_CD                       AS QT_UT                " & vbNewLine _
        & "      , UNSO_M.HASU                                                      " & vbNewLine _
        & "      , UNSO_M.ZAI_REC_NO                                                " & vbNewLine _
        & "      , ABKBN_ONDO_KB.KBN_CD                     AS UNSO_ONDO_KB         " & vbNewLine _
        & "      , UNSO_M.IRIME                                                     " & vbNewLine _
        & "      , ABKBN_IRIME_UT.KBN_CD                    AS IRIME_UT             " & vbNewLine _
        & "      , UNSO_M.BETU_WT                                                   " & vbNewLine _
        & "      , UNSO_M.SIZE_KB                                                   " & vbNewLine _
        & "      , UNSO_M.ZBUKA_CD                                                  " & vbNewLine _
        & "      , UNSO_M.ABUKA_CD                                                  " & vbNewLine _
        & "      , UNSO_M.PKG_NB                                                    " & vbNewLine _
        & "      , UNSO_M.LOT_NO                                                    " & vbNewLine _
        & "      , UNSO_M.REMARK                                                    " & vbNewLine _
        & "      , UNSO_M.UNSO_HOKEN_UM                                             " & vbNewLine _
        & "      , UNSO_M.KITAKU_GOODS_UP                                           " & vbNewLine _
        & "   FROM $LM_TRN$..F_UNSO_M UNSO_M                                        " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_GOODS GOODS                                          " & vbNewLine _
        & "     ON GOODS.NRS_BR_CD                  = UNSO_M.NRS_BR_CD              " & vbNewLine _
        & "    AND GOODS.GOODS_CD_NRS               = UNSO_M.GOODS_CD_NRS           " & vbNewLine _
        & "    AND GOODS.SYS_DEL_FLG                = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..M_SHOBO SHOBO                                          " & vbNewLine _
        & "     ON SHOBO.SHOBO_CD                   = GOODS.SHOBO_CD                " & vbNewLine _
        & "    AND SHOBO.SYS_DEL_FLG                = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..Z_KBN KBN_SHOBO                                        " & vbNewLine _
        & "     ON KBN_SHOBO.KBN_GROUP_CD           = 'S004'                        " & vbNewLine _
        & "    AND KBN_SHOBO.KBN_CD                 = SHOBO.RUI                     " & vbNewLine _
        & "    AND KBN_SHOBO.SYS_DEL_FLG            = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..Z_KBN KBN_DOKU                                         " & vbNewLine _
        & "     ON KBN_DOKU.KBN_GROUP_CD            = 'G001'                        " & vbNewLine _
        & "    AND KBN_DOKU.KBN_CD                  = GOODS.DOKU_KB                 " & vbNewLine _
        & "    AND KBN_DOKU.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_NB_UT                                        " & vbNewLine _
        & "     ON ABKBN_NB_UT.KBN_GROUP_CD         = 'K10205'                      " & vbNewLine _
        & "    AND ABKBN_NB_UT.KBN_LANG             = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_NB_UT.KBN_CD_REM           = UNSO_M.NB_UT                  " & vbNewLine _
        & "    AND ABKBN_NB_UT.SYS_DEL_FLG          = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_QT_UT                                        " & vbNewLine _
        & "     ON ABKBN_QT_UT.KBN_GROUP_CD         = 'N10014'                      " & vbNewLine _
        & "    AND ABKBN_QT_UT.KBN_LANG             = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_QT_UT.KBN_CD_REM           = UNSO_M.QT_UT                  " & vbNewLine _
        & "    AND ABKBN_QT_UT.SYS_DEL_FLG          = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_ONDO_KB                                      " & vbNewLine _
        & "     ON ABKBN_ONDO_KB.KBN_GROUP_CD       = 'U10009'                      " & vbNewLine _
        & "    AND ABKBN_ONDO_KB.KBN_LANG           = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_ONDO_KB.KBN_CD_REM         = UNSO_M.UNSO_ONDO_KB           " & vbNewLine _
        & "    AND ABKBN_ONDO_KB.SYS_DEL_FLG        = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        ABM_DB..Z_KBN ABKBN_IRIME_UT                                     " & vbNewLine _
        & "     ON ABKBN_IRIME_UT.KBN_GROUP_CD      = 'I10019'                      " & vbNewLine _
        & "    AND ABKBN_IRIME_UT.KBN_LANG          = @KBN_LANG                     " & vbNewLine _
        & "    AND ABKBN_IRIME_UT.KBN_CD_REM        = UNSO_M.IRIME_UT               " & vbNewLine _
        & "    AND ABKBN_IRIME_UT.SYS_DEL_FLG       = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..S_USER  S_USER                                         " & vbNewLine _
        & "     ON S_USER.USER_CD                   = @USER_CD                      " & vbNewLine _
        & "    AND S_USER.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_NRS_BR  CNV                                        " & vbNewLine _
        & "     ON CNV.NRS_BR_CD_LM                 = UNSO_M.NRS_BR_CD              " & vbNewLine _
        & "    AND CNV.WH_CD                        = S_USER.WH_CD                  " & vbNewLine _
        & "    AND CNV.SYS_DEL_FLG                  = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..F_UNSO_L  UNSO_L                                       " & vbNewLine _
        & "     ON UNSO_L.UNSO_NO_L                 = UNSO_M.UNSO_NO_L              " & vbNewLine _
        & "    AND UNSO_L.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_SEND_NRS_BR  CNV_SEND                              " & vbNewLine _
        & "     ON CNV_SEND.NRS_BR_CD               = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND CNV_SEND.CUST_CD_L               = UNSO_L.CUST_CD_L              " & vbNewLine _
        & "    AND CNV_SEND.CUST_CD_M               = UNSO_L.CUST_CD_M              " & vbNewLine _
        & "    AND CNV_SEND.UNSO_CD                 = UNSO_L.UNSO_CD                " & vbNewLine _
        & "    AND CNV_SEND.UNSO_BR_CD              = UNSO_L.UNSO_BR_CD             " & vbNewLine _
        & "    AND UNSO_L.TRIP_NO                   = ''                            " & vbNewLine _
        & "    AND CNV_SEND.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE UNSO_M.NRS_BR_CD                 = @NRS_BR_CD                    " & vbNewLine _
        & "    AND UNSO_M.SYS_DEL_FLG               = '0'                           " & vbNewLine

    Private Const SQL_ORDER_UNSO_M_DATA As String _
        = "  ORDER BY                                                               " & vbNewLine _
        & "        UNSO_M.UNSO_NO_L                                                 " & vbNewLine _
        & "      , UNSO_M.UNSO_NO_M                                                 " & vbNewLine

    Private Const SQL_SELECT_SHIHARAI_DATA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        ISNULL(CNV_SEND.CNV_NRS_BR_CD, ISNULL(CNV.NRS_BR_CD, SHIHARAI.YUSO_BR_CD))  AS YUSO_BR_CD  " & vbNewLine _
        & "      , ISNULL(CNV_SEND.CNV_NRS_BR_CD, ISNULL(CNV.NRS_BR_CD, SHIHARAI.NRS_BR_CD))  AS NRS_BR_CD  " & vbNewLine _
        & "      , SHIHARAI.UNSO_NO_L                                               " & vbNewLine _
        & "      , SHIHARAI.UNSO_NO_M                                               " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN SHIHARAI.CUST_CD_L                                 " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS CUST_CD_L                                                " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN SHIHARAI.CUST_CD_M                                 " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS CUST_CD_M                                                " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN SHIHARAI.CUST_CD_S                                 " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS CUST_CD_S                                                " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN SHIHARAI.CUST_CD_SS                                " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS CUST_CD_SS                                               " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_GROUP_NO                                       " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_GROUP_NO_M                                     " & vbNewLine _
        & "      , CASE WHEN CNV_SEND.NRS_BR_CD IS NULL                             " & vbNewLine _
        & "                 THEN SHIHARAI.SHIHARAITO_CD                             " & vbNewLine _
        & "             ELSE ''                                                     " & vbNewLine _
        & "        END  AS SHIHARAITO_CD                                            " & vbNewLine _
        & "      , SHIHARAI.UNTIN_CALCULATION_KB                                    " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_SYARYO_KB                                      " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_PKG_UT                                         " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_NG_NB                                          " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_DANGER_KB                                      " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_TARIFF_BUNRUI_KB                               " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_TARIFF_CD                                      " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_ETARIFF_CD                                     " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_KYORI                                          " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_WT                                             " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_UNCHIN                                         " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_CITY_EXTC                                      " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_WINT_EXTC                                      " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_RELY_EXTC                                      " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_TOLL                                           " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_INSU                                           " & vbNewLine _
        & "      , SHIHARAI.SHIHARAI_FIXED_FLAG                                     " & vbNewLine _
        & "      , SHIHARAI.DECI_NG_NB                                              " & vbNewLine _
        & "      , SHIHARAI.DECI_KYORI                                              " & vbNewLine _
        & "      , SHIHARAI.DECI_WT                                                 " & vbNewLine _
        & "      , SHIHARAI.DECI_UNCHIN                                             " & vbNewLine _
        & "      , SHIHARAI.DECI_CITY_EXTC                                          " & vbNewLine _
        & "      , SHIHARAI.DECI_WINT_EXTC                                          " & vbNewLine _
        & "      , SHIHARAI.DECI_RELY_EXTC                                          " & vbNewLine _
        & "      , SHIHARAI.DECI_TOLL                                               " & vbNewLine _
        & "      , SHIHARAI.DECI_INSU                                               " & vbNewLine _
        & "      , SHIHARAI.KANRI_UNCHIN                                            " & vbNewLine _
        & "      , SHIHARAI.KANRI_CITY_EXTC                                         " & vbNewLine _
        & "      , SHIHARAI.KANRI_WINT_EXTC                                         " & vbNewLine _
        & "      , SHIHARAI.KANRI_RELY_EXTC                                         " & vbNewLine _
        & "      , SHIHARAI.KANRI_TOLL                                              " & vbNewLine _
        & "      , SHIHARAI.KANRI_INSU                                              " & vbNewLine _
        & "      , SHIHARAI.REMARK                                                  " & vbNewLine _
        & "      , SHIHARAI.SIZE_KB                                                 " & vbNewLine _
        & "      , SHIHARAI.TAX_KB                                                  " & vbNewLine _
        & "      , SHIHARAI.SAGYO_KANRI                                             " & vbNewLine _
        & "   FROM $LM_TRN$..F_SHIHARAI_TRS SHIHARAI                                " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_MST$..S_USER  S_USER                                         " & vbNewLine _
        & "     ON S_USER.USER_CD                   = @USER_CD                      " & vbNewLine _
        & "    AND S_USER.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_NRS_BR  CNV                                        " & vbNewLine _
        & "     ON CNV.NRS_BR_CD_LM                 = SHIHARAI.NRS_BR_CD            " & vbNewLine _
        & "    AND CNV.WH_CD                        = S_USER.WH_CD                  " & vbNewLine _
        & "    AND CNV.SYS_DEL_FLG                  = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..F_UNSO_L  UNSO_L                                       " & vbNewLine _
        & "     ON UNSO_L.UNSO_NO_L                 = SHIHARAI.UNSO_NO_L            " & vbNewLine _
        & "    AND UNSO_L.SYS_DEL_FLG               = '0'                           " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        AB_DB..HM_CNV_SEND_NRS_BR  CNV_SEND                              " & vbNewLine _
        & "     ON CNV_SEND.NRS_BR_CD               = UNSO_L.NRS_BR_CD              " & vbNewLine _
        & "    AND CNV_SEND.CUST_CD_L               = UNSO_L.CUST_CD_L              " & vbNewLine _
        & "    AND CNV_SEND.CUST_CD_M               = UNSO_L.CUST_CD_M              " & vbNewLine _
        & "    AND CNV_SEND.UNSO_CD                 = UNSO_L.UNSO_CD                " & vbNewLine _
        & "    AND CNV_SEND.UNSO_BR_CD              = UNSO_L.UNSO_BR_CD             " & vbNewLine _
        & "    AND UNSO_L.TRIP_NO                   = ''                            " & vbNewLine _
        & "    AND CNV_SEND.SYS_DEL_FLG             = '0'                           " & vbNewLine _
        & "  WHERE SHIHARAI.NRS_BR_CD               = @NRS_BR_CD                    " & vbNewLine _
        & "    AND SHIHARAI.SYS_DEL_FLG             = '0'                           " & vbNewLine

    Private Const SQL_ORDER_SHIHARAI_DATA As String _
        = "  ORDER BY                                                               " & vbNewLine _
        & "        SHIHARAI.UNSO_NO_L                                               " & vbNewLine _
        & "      , SHIHARAI.UNSO_NO_M                                               " & vbNewLine

    Private Const SQL_SELECT_OUTKA_TOUSITU_DATA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        OUTKA_L.OUTKA_NO_L                       AS INOUTKA_NO_L         " & vbNewLine _
        & "      , OUTKA_L.WH_CD                                                    " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_M                       AS INOUTKA_NO_M         " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_S                       AS INOUTKA_NO_S         " & vbNewLine _
        & "      , OUTKA_S.TOU_NO                                                   " & vbNewLine _
        & "      , OUTKA_S.SITU_NO                                                  " & vbNewLine _
        & "      , TOU_SITU.TOU_SITU_NM                                             " & vbNewLine _
        & "      , TOU_SITU.TASYA_WH_NM                                             " & vbNewLine _
        & "      , TOU_SITU.TASYA_ZIP                                               " & vbNewLine _
        & "      , TOU_SITU.TASYA_AD_1                                              " & vbNewLine _
        & "      , TOU_SITU.TASYA_AD_2                                              " & vbNewLine _
        & "      , TOU_SITU.TASYA_AD_3                                              " & vbNewLine _
        & "   FROM $LM_TRN$..C_OUTKA_L OUTKA_L                                      " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..C_OUTKA_S OUTKA_S                                      " & vbNewLine _
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
        & "    AND OUTKA_L.SYS_DEL_FLG              = '0'                           " & vbNewLine

    Private Const SQL_ORDER_OUTKA_TOUSITU_DATA As String _
        = "  ORDER BY                                                               " & vbNewLine _
        & "        OUTKA_L.OUTKA_NO_L                                               " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_M                                               " & vbNewLine _
        & "      , OUTKA_S.OUTKA_NO_S                                               " & vbNewLine

    Private Const SQL_SELECT_INKA_TOUSITU_DATA As String _
        = " SELECT                                                                  " & vbNewLine _
        & "        INKA_L.INKA_NO_L                         AS INOUTKA_NO_L         " & vbNewLine _
        & "      , INKA_L.WH_CD                                                     " & vbNewLine _
        & "      , INKA_S.INKA_NO_M                         AS INOUTKA_NO_M         " & vbNewLine _
        & "      , INKA_S.INKA_NO_S                         AS INOUTKA_NO_S         " & vbNewLine _
        & "      , INKA_S.TOU_NO                                                    " & vbNewLine _
        & "      , INKA_S.SITU_NO                                                   " & vbNewLine _
        & "      , TOU_SITU.TOU_SITU_NM                                             " & vbNewLine _
        & "      , TOU_SITU.TASYA_WH_NM                                             " & vbNewLine _
        & "      , TOU_SITU.TASYA_ZIP                                               " & vbNewLine _
        & "      , TOU_SITU.TASYA_AD_1                                              " & vbNewLine _
        & "      , TOU_SITU.TASYA_AD_2                                              " & vbNewLine _
        & "      , TOU_SITU.TASYA_AD_3                                              " & vbNewLine _
        & "   FROM $LM_TRN$..B_INKA_L INKA_L                                        " & vbNewLine _
        & "   LEFT JOIN                                                             " & vbNewLine _
        & "        $LM_TRN$..B_INKA_S INKA_S                                        " & vbNewLine _
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
        & "    AND INKA_L.SYS_DEL_FLG               = '0'                           " & vbNewLine

    Private Const SQL_ORDER_INKA_TOUSITU_DATA As String _
        = "  ORDER BY                                                               " & vbNewLine _
        & "        INKA_L.INKA_NO_L                                                 " & vbNewLine _
        & "      , INKA_S.INKA_NO_M                                                 " & vbNewLine _
        & "      , INKA_S.INKA_NO_S                                                 " & vbNewLine

#End Region

#Region "データ更新用"

    Private Const SQL_UPD_UNSO_L_SOSHIN_FLG As String _
        = " UPDATE $LM_TRN$..F_UNSO_L                               " & vbNewLine _
        & "    SET HAISO_PF_SEND_FLG    = @HAISO_PF_SEND_FLG        " & vbNewLine _
        & "      , SYS_UPD_DATE         = @SYS_UPD_DATE             " & vbNewLine _
        & "      , SYS_UPD_TIME         = @SYS_UPD_TIME             " & vbNewLine _
        & "      , SYS_UPD_PGID         = @SYS_UPD_PGID             " & vbNewLine _
        & "      , SYS_UPD_USER         = @SYS_UPD_USER             " & vbNewLine _
        & "  WHERE NRS_BR_CD            = @NRS_BR_CD                " & vbNewLine _
        & "    AND UNSO_NO_L            = @UNSO_NO_L                " & vbNewLine _
        & "    AND SYS_UPD_DATE         = @GUI_SYS_UPD_DATE         " & vbNewLine _
        & "    AND SYS_UPD_TIME         = @GUI_SYS_UPD_TIME         " & vbNewLine _
        & "    AND SYS_DEL_FLG          = '0'                       " & vbNewLine

#End Region

#End Region 'SQL

#End Region 'Const

#Region "Class"

    ''' <summary>
    ''' リクエストABHB910
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ReqABHB910

        Public Sub New()
            UnsoLL_List = New List(Of HbUnsoLLItem)
            UnsoL_List = New List(Of HbUnsoLItem)
            UnsoM_List = New List(Of HbUnsoMItem)
            Shiharai_List = New List(Of HbShiharaiItem)
        End Sub

        ''' <summary>
        ''' 運送(特大)リスト
        ''' </summary>
        ''' <returns></returns>
        Public Property UnsoLL_List As List(Of HbUnsoLLItem) = Nothing

        ''' <summary>
        ''' 運送(大)リスト
        ''' </summary>
        ''' <returns></returns>
        Public Property UnsoL_List As List(Of HbUnsoLItem) = Nothing

        ''' <summary>
        ''' 運送(中)リスト
        ''' </summary>
        ''' <returns></returns>
        Public Property UnsoM_List As List(Of HbUnsoMItem) = Nothing

        ''' <summary>
        ''' 支払運賃リスト
        ''' </summary>
        ''' <returns></returns>
        Public Property Shiharai_List As List(Of HbShiharaiItem) = Nothing

    End Class

    ''' <summary>
    ''' 運送(特大)項目
    ''' </summary>
    ''' <remarks></remarks>
    Private Class HbUnsoLLItem

        ''' <summary>
        ''' キャンセルフラグ
        ''' </summary>
        ''' <returns></returns>
        Public Property CANCEL_FLG As String = Nothing

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property NRS_BR_CD As String = Nothing

        ''' <summary>
        ''' 運行番号
        ''' </summary>
        ''' <returns></returns>
        Public Property TRIP_NO As String = Nothing

        ''' <summary>
        ''' 運送会社コード
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSOCO_CD As String = Nothing

        ''' <summary>
        ''' 運送会社支店コード
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSOCO_BR_CD As String = Nothing

        ''' <summary>
        ''' 自傭車区分
        ''' </summary>
        ''' <returns></returns>
        Public Property JSHA_KB As String = Nothing

        ''' <summary>
        ''' 便区分
        ''' </summary>
        ''' <returns></returns>
        Public Property BIN_KB As String = Nothing

        ''' <summary>
        ''' 車輌KEY
        ''' </summary>
        ''' <returns></returns>
        Public Property CAR_KEY As String = Nothing

        ''' <summary>
        ''' 運送温度
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_ONDO As String = Nothing

        ''' <summary>
        ''' 乗務員コード
        ''' </summary>
        ''' <returns></returns>
        Public Property DRIVER_CD As String = Nothing

        ''' <summary>
        ''' 運行日
        ''' </summary>
        ''' <returns></returns>
        Public Property TRIP_DATE As String = Nothing

        ''' <summary>
        ''' 下払金額
        ''' </summary>
        ''' <returns></returns>
        Public Property PAY_UNCHIN As String = Nothing

        ''' <summary>
        ''' 支払タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property PAY_TARIFF_CD As String = Nothing

        ''' <summary>
        ''' 配送区分
        ''' </summary>
        ''' <returns></returns>
        Public Property HAISO_KB As String = Nothing

        ''' <summary>
        ''' 備考
        ''' </summary>
        ''' <returns></returns>
        Public Property REMARK As String = Nothing

        ''' <summary>
        ''' 支払タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_TARIFF_CD As String = Nothing

        ''' <summary>
        ''' 支払割増タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_ETARIFF_CD As String = Nothing

        ''' <summary>
        ''' 支払運送重量
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_UNSO_WT As String = Nothing

        ''' <summary>
        ''' 支払件数
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_COUNT As String = Nothing

        ''' <summary>
        ''' 支払金額
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_UNCHIN As String = Nothing

        ''' <summary>
        ''' 支払タリフ分類区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_TARIFF_BUNRUI_KB As String = Nothing

    End Class

    ''' <summary>
    ''' 運送(大)項目
    ''' </summary>
    ''' <remarks></remarks>
    Private Class HbUnsoLItem

        ''' <summary>
        ''' キャンセルフラグ
        ''' </summary>
        ''' <returns></returns>
        Public Property CANCEL_FLG As String = Nothing

        ''' <summary>
        ''' 予約番号
        ''' </summary>
        ''' <returns></returns>
        Public Property YOYAKU_NO As String = Nothing

        ''' <summary>
        ''' 配送まとめ番号
        ''' </summary>
        ''' <returns></returns>
        Public Property HAISO_GROUP_NO As String = Nothing

        ''' <summary>
        ''' 製品セグメント
        ''' </summary>
        ''' <returns></returns>
        Public Property PRODUCT_SEG_CD As String = Nothing

        ''' <summary>
        ''' 真荷主BPCD
        ''' </summary>
        ''' <returns></returns>
        Public Property TCUST_BPCD As String = Nothing

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property NRS_BR_CD As String = Nothing

        ''' <summary>
        ''' 作成者
        ''' </summary>
        ''' <returns></returns>
        Public Property CRT_USER_NM As String = Nothing

        ''' <summary>
        ''' 運送番号L
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_NO_L As String = Nothing

        ''' <summary>
        ''' 輸送部営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property YUSO_BR_CD As String = Nothing

        ''' <summary>
        ''' 入出荷管理番号L
        ''' </summary>
        ''' <returns></returns>
        Public Property INOUTKA_NO_L As String = Nothing

        ''' <summary>
        ''' 運行番号
        ''' </summary>
        ''' <returns></returns>
        Public Property TRIP_NO As String = Nothing

        ''' <summary>
        ''' 運送会社コード
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_CD As String = Nothing

        ''' <summary>
        ''' 運送会社支店コード
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_BR_CD As String = Nothing

        ''' <summary>
        ''' 日陸経理コード(JDE)
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_BP_CD As String = Nothing

        ''' <summary>
        ''' 便区分
        ''' </summary>
        ''' <returns></returns>
        Public Property BIN_KB As String = Nothing

        ''' <summary>
        ''' 運送事由区分
        ''' </summary>
        ''' <returns></returns>
        Public Property JIYU_KB As String = Nothing

        ''' <summary>
        ''' 送状番号
        ''' </summary>
        ''' <returns></returns>
        Public Property DENP_NO As String = Nothing

        ''' <summary>
        ''' 自動送状区分
        ''' </summary>
        ''' <returns></returns>
        Public Property AUTO_DENP_KBN As String = Nothing

        ''' <summary>
        ''' 自動送状番号
        ''' </summary>
        ''' <returns></returns>
        Public Property AUTO_DENP_NO As String = Nothing

        ''' <summary>
        ''' 出荷予定日
        ''' </summary>
        ''' <returns></returns>
        Public Property OUTKA_PLAN_DATE As String = Nothing

        ''' <summary>
        ''' 出荷予定時刻
        ''' </summary>
        ''' <returns></returns>
        Public Property OUTKA_PLAN_TIME As String = Nothing

        ''' <summary>
        ''' 納入予定日
        ''' </summary>
        ''' <returns></returns>
        Public Property ARR_PLAN_DATE As String = Nothing

        ''' <summary>
        ''' 納入予定時刻
        ''' </summary>
        ''' <returns></returns>
        Public Property ARR_PLAN_TIME As String = Nothing

        ''' <summary>
        ''' 納入実時刻
        ''' </summary>
        ''' <returns></returns>
        Public Property ARR_ACT_TIME As String = Nothing

        ''' <summary>
        ''' 荷主コード (大)
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_CD_L As String = Nothing

        ''' <summary>
        ''' 荷主コード (中)
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_CD_M As String = Nothing

        ''' <summary>
        ''' 荷主名（大）
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_NM_L As String = Nothing

        ''' <summary>
        ''' 荷主参照番号
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_REF_NO As String = Nothing

        ''' <summary>
        ''' 荷送人コード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIP_CD As String = Nothing

        ''' <summary>
        ''' 発地コード
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_CD As String = Nothing

        ''' <summary>
        ''' 発地名称
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_NM As String = Nothing

        ''' <summary>
        ''' 発地カナ名
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_KANA_NM As String = Nothing

        ''' <summary>
        ''' 発地郵便番号
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_ZIP As String = Nothing

        ''' <summary>
        ''' 発地住所1
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_AD_1 As String = Nothing

        ''' <summary>
        ''' 発地住所2
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_AD_2 As String = Nothing

        ''' <summary>
        ''' 発地住所3
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_AD_3 As String = Nothing

        ''' <summary>
        ''' 発地電話番号
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_TEL As String = Nothing

        ''' <summary>
        ''' 発地FAX番号
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_FAX As String = Nothing

        ''' <summary>
        ''' 発地都道府県名
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_KEN As String = Nothing

        ''' <summary>
        ''' 発地市区町村名
        ''' </summary>
        ''' <returns></returns>
        Public Property ORIG_SHI As String = Nothing

        ''' <summary>
        ''' 届先コード
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_CD As String = Nothing

        ''' <summary>
        ''' 届先名称
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_NM As String = Nothing

        ''' <summary>
        ''' 届先カナ名
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_KANA_NM As String = Nothing

        ''' <summary>
        ''' 届先郵便番号
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_ZIP As String = Nothing

        ''' <summary>
        ''' 届先住所1
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_AD_1 As String = Nothing

        ''' <summary>
        ''' 届先住所2
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_AD_2 As String = Nothing

        ''' <summary>
        ''' 届先住所3
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_AD_3 As String = Nothing

        ''' <summary>
        ''' 届先電話番号
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_TEL As String = Nothing

        ''' <summary>
        ''' 届先FAX番号
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_FAX As String = Nothing

        ''' <summary>
        ''' 届先都道府県名
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_KEN As String = Nothing

        ''' <summary>
        ''' 届先市区町村名
        ''' </summary>
        ''' <returns></returns>
        Public Property DEST_SHI As String = Nothing

        ''' <summary>
        ''' 運送梱包個数
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_PKG_NB As String = Nothing

        ''' <summary>
        ''' 個数単位
        ''' </summary>
        ''' <returns></returns>
        Public Property NB_UT As String = Nothing

        ''' <summary>
        ''' 運送重量
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_WT As String = Nothing

        ''' <summary>
        ''' 運送温度区分
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_ONDO_KB As String = Nothing

        ''' <summary>
        ''' 元着払区分
        ''' </summary>
        ''' <returns></returns>
        Public Property PC_KB As String = Nothing

        ''' <summary>
        ''' タリフ分類区分
        ''' </summary>
        ''' <returns></returns>
        Public Property TARIFF_BUNRUI_KB As String = Nothing

        ''' <summary>
        ''' 車輌区分
        ''' </summary>
        ''' <returns></returns>
        Public Property VCLE_KB As String = Nothing

        ''' <summary>
        ''' 元データ区分
        ''' </summary>
        ''' <returns></returns>
        Public Property MOTO_DATA_KB As String = Nothing

        ''' <summary>
        ''' 課税区分
        ''' </summary>
        ''' <returns></returns>
        Public Property TAX_KB As String = Nothing

        ''' <summary>
        ''' 備考
        ''' </summary>
        ''' <returns></returns>
        Public Property REMARK As String = Nothing

        ''' <summary>
        ''' タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SEIQ_TARIFF_CD As String = Nothing

        ''' <summary>
        ''' 割増タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SEIQ_ETARIFF_CD As String = Nothing

        ''' <summary>
        ''' 届先住所3
        ''' </summary>
        ''' <returns></returns>
        Public Property AD_3 As String = Nothing

        ''' <summary>
        ''' 運送手配区分
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_TEHAI_KB As String = Nothing

        ''' <summary>
        ''' 買主注文番号
        ''' </summary>
        ''' <returns></returns>
        Public Property BUY_CHU_NO As String = Nothing

        ''' <summary>
        ''' 配送区域
        ''' </summary>
        ''' <returns></returns>
        Public Property AREA_CD As String = Nothing

        ''' <summary>
        ''' 中継配送フラグ
        ''' </summary>
        ''' <returns></returns>
        Public Property TYUKEI_HAISO_FLG As String = Nothing

        ''' <summary>
        ''' 集荷中継地
        ''' </summary>
        ''' <returns></returns>
        Public Property SYUKA_TYUKEI_CD As String = Nothing

        ''' <summary>
        ''' 配荷中継地
        ''' </summary>
        ''' <returns></returns>
        Public Property HAIKA_TYUKEI_CD As String = Nothing

        ''' <summary>
        ''' 運行番号（集荷）
        ''' </summary>
        ''' <returns></returns>
        Public Property TRIP_NO_SYUKA As String = Nothing

        ''' <summary>
        ''' 運行番号（中継）
        ''' </summary>
        ''' <returns></returns>
        Public Property TRIP_NO_TYUKEI As String = Nothing

        ''' <summary>
        ''' 運行番号（配荷）
        ''' </summary>
        ''' <returns></returns>
        Public Property TRIP_NO_HAIKA As String = Nothing

        ''' <summary>
        ''' 支払タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_TARIFF_CD As String = Nothing

        ''' <summary>
        ''' 支払割増タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_ETARIFF_CD As String = Nothing

        ''' <summary>
        ''' 幹線配達区分
        ''' </summary>
        ''' <returns></returns>
        Public Property MAIN_DELI_KB As String = Nothing

        ''' <summary>
        ''' 備考
        ''' </summary>
        ''' <returns></returns>
        Public Property NHS_REMARK As String = Nothing

    End Class

    ''' <summary>
    ''' 運送(中)項目
    ''' </summary>
    ''' <remarks></remarks>
    Private Class HbUnsoMItem

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property NRS_BR_CD As String = Nothing

        ''' <summary>
        ''' 運送番号L
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_NO_L As String = Nothing

        ''' <summary>
        ''' 運送番号M
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_NO_M As String = Nothing

        ''' <summary>
        ''' 商品KEY
        ''' </summary>
        ''' <returns></returns>
        Public Property GOODS_CD_NRS As String = Nothing

        ''' <summary>
        ''' 商品名
        ''' </summary>
        ''' <returns></returns>
        Public Property GOODS_NM As String = Nothing

        ''' <summary>
        ''' 消防コード名
        ''' </summary>
        ''' <returns></returns>
        Public Property SHOBO_NM As String = Nothing

        ''' <summary>
        ''' 毒劇区分名
        ''' </summary>
        ''' <returns></returns>
        Public Property DOKU_NM As String = Nothing

        ''' <summary>
        ''' 運送個数
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_TTL_NB As String = Nothing

        ''' <summary>
        ''' 個数単位コード
        ''' </summary>
        ''' <returns></returns>
        Public Property NB_UT As String = Nothing

        ''' <summary>
        ''' 運送数量
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_TTL_QT As String = Nothing

        ''' <summary>
        ''' 数量単位コード
        ''' </summary>
        ''' <returns></returns>
        Public Property QT_UT As String = Nothing

        ''' <summary>
        ''' 端数
        ''' </summary>
        ''' <returns></returns>
        Public Property HASU As String = Nothing

        ''' <summary>
        ''' 在庫レコード番号
        ''' </summary>
        ''' <returns></returns>
        Public Property ZAI_REC_NO As String = Nothing

        ''' <summary>
        ''' 運送温度区分
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_ONDO_KB As String = Nothing

        ''' <summary>
        ''' 入目
        ''' </summary>
        ''' <returns></returns>
        Public Property IRIME As String = Nothing

        ''' <summary>
        ''' 入目単位コード
        ''' </summary>
        ''' <returns></returns>
        Public Property IRIME_UT As String = Nothing

        ''' <summary>
        ''' 個別重量
        ''' </summary>
        ''' <returns></returns>
        Public Property BETU_WT As String = Nothing

        ''' <summary>
        ''' 宅急便サイズ区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SIZE_KB As String = Nothing

        ''' <summary>
        ''' 在庫部課コード
        ''' </summary>
        ''' <returns></returns>
        Public Property ZBUKA_CD As String = Nothing

        ''' <summary>
        ''' 扱い部課コード
        ''' </summary>
        ''' <returns></returns>
        Public Property ABUKA_CD As String = Nothing

        ''' <summary>
        ''' 梱包個数
        ''' </summary>
        ''' <returns></returns>
        Public Property PKG_NB As String = Nothing

        ''' <summary>
        ''' ロット№
        ''' </summary>
        ''' <returns></returns>
        Public Property LOT_NO As String = Nothing

        ''' <summary>
        ''' 備考
        ''' </summary>
        ''' <returns></returns>
        Public Property REMARK As String = Nothing

        ''' <summary>
        ''' 運送保険料有無
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_HOKEN_UM As String = Nothing

        ''' <summary>
        ''' 寄託商品単価
        ''' </summary>
        ''' <returns></returns>
        Public Property KITAKU_GOODS_UP As String = Nothing

    End Class

    ''' <summary>
    ''' 支払運賃項目
    ''' </summary>
    ''' <remarks></remarks>
    Private Class HbShiharaiItem

        ''' <summary>
        ''' 輸送部営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property YUSO_BR_CD As String = Nothing

        ''' <summary>
        ''' 営業所コード
        ''' </summary>
        ''' <returns></returns>
        Public Property NRS_BR_CD As String = Nothing

        ''' <summary>
        ''' 運送番号L
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_NO_L As String = Nothing

        ''' <summary>
        ''' 運送番号M
        ''' </summary>
        ''' <returns></returns>
        Public Property UNSO_NO_M As String = Nothing

        ''' <summary>
        ''' 荷主コードL
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_CD_L As String = Nothing

        ''' <summary>
        ''' 荷主コードM
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_CD_M As String = Nothing

        ''' <summary>
        ''' 荷主コードS
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_CD_S As String = Nothing

        ''' <summary>
        ''' 荷主コードSS
        ''' </summary>
        ''' <returns></returns>
        Public Property CUST_CD_SS As String = Nothing

        ''' <summary>
        ''' 支払グループ番号L
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_GROUP_NO As String = Nothing

        ''' <summary>
        ''' 支払グループ番号M
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_GROUP_NO_M As String = Nothing

        ''' <summary>
        ''' 支払先コード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAITO_CD As String = Nothing

        ''' <summary>
        ''' 運賃計算締日区分
        ''' </summary>
        ''' <returns></returns>
        Public Property UNTIN_CALCULATION_KB As String = Nothing

        ''' <summary>
        ''' 車輌区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_SYARYO_KB As String = Nothing

        ''' <summary>
        ''' 包装単位（荷姿）
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_PKG_UT As String = Nothing

        ''' <summary>
        ''' 荷姿個数
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_NG_NB As String = Nothing

        ''' <summary>
        ''' 危険区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_DANGER_KB As String = Nothing

        ''' <summary>
        ''' タリフ分類区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_TARIFF_BUNRUI_KB As String = Nothing

        ''' <summary>
        ''' タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_TARIFF_CD As String = Nothing

        ''' <summary>
        ''' 支払割増タリフコード
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_ETARIFF_CD As String = Nothing

        ''' <summary>
        ''' 支払適用距離
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_KYORI As String = Nothing

        ''' <summary>
        ''' 支払適用重量
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_WT As String = Nothing

        ''' <summary>
        ''' 支払運賃
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_UNCHIN As String = Nothing

        ''' <summary>
        ''' 支払都市割増
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_CITY_EXTC As String = Nothing

        ''' <summary>
        ''' 支払冬期割増
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_WINT_EXTC As String = Nothing

        ''' <summary>
        ''' 支払中継料
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_RELY_EXTC As String = Nothing

        ''' <summary>
        ''' 支払通行料
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_TOLL As String = Nothing

        ''' <summary>
        ''' 支払保険料
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_INSU As String = Nothing

        ''' <summary>
        ''' 支払料金確定フラグ
        ''' </summary>
        ''' <returns></returns>
        Public Property SHIHARAI_FIXED_FLAG As String = Nothing

        ''' <summary>
        ''' 確定荷姿個数
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_NG_NB As String = Nothing

        ''' <summary>
        ''' 確定適用距離
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_KYORI As String = Nothing

        ''' <summary>
        ''' 確定適用重量
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_WT As String = Nothing

        ''' <summary>
        ''' 確定支払運賃
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_UNCHIN As String = Nothing

        ''' <summary>
        ''' 確定支払都市割増
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_CITY_EXTC As String = Nothing

        ''' <summary>
        ''' 確定支払冬期割増
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_WINT_EXTC As String = Nothing

        ''' <summary>
        ''' 確定支払中継料
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_RELY_EXTC As String = Nothing

        ''' <summary>
        ''' 確定支払通行料
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_TOLL As String = Nothing

        ''' <summary>
        ''' 確定支払保険料
        ''' </summary>
        ''' <returns></returns>
        Public Property DECI_INSU As String = Nothing

        ''' <summary>
        ''' 管理用支払運賃
        ''' </summary>
        ''' <returns></returns>
        Public Property KANRI_UNCHIN As String = Nothing

        ''' <summary>
        ''' 管理用支払都市割増
        ''' </summary>
        ''' <returns></returns>
        Public Property KANRI_CITY_EXTC As String = Nothing

        ''' <summary>
        ''' 管理用支払冬期割増
        ''' </summary>
        ''' <returns></returns>
        Public Property KANRI_WINT_EXTC As String = Nothing

        ''' <summary>
        ''' 管理用支払中継料
        ''' </summary>
        ''' <returns></returns>
        Public Property KANRI_RELY_EXTC As String = Nothing

        ''' <summary>
        ''' 管理用支払通行料
        ''' </summary>
        ''' <returns></returns>
        Public Property KANRI_TOLL As String = Nothing

        ''' <summary>
        ''' 管理用支払保険料
        ''' </summary>
        ''' <returns></returns>
        Public Property KANRI_INSU As String = Nothing

        ''' <summary>
        ''' 備考（荷主注文番号）
        ''' </summary>
        ''' <returns></returns>
        Public Property REMARK As String = Nothing

        ''' <summary>
        ''' 宅急便サイズ区分
        ''' </summary>
        ''' <returns></returns>
        Public Property SIZE_KB As String = Nothing

        ''' <summary>
        ''' 課税区分
        ''' </summary>
        ''' <returns></returns>
        Public Property TAX_KB As String = Nothing

        ''' <summary>
        ''' 作業料管理用
        ''' </summary>
        ''' <returns></returns>
        Public Property SAGYO_KANRI As String = Nothing

    End Class

    ''' <summary>
    ''' レスポンスABHB910
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ResABHB910

        Public Sub New()
            Messages = New List(Of ResItem)
        End Sub

        ''' <summary>
        ''' メッセージモデルリスト
        ''' </summary>
        ''' <returns></returns>
        Public Property Messages As List(Of ResItem) = Nothing

        ''' <summary>
        ''' エラーメッセージ有
        ''' </summary>
        ''' <returns></returns>
        Public Property HasError As Boolean = False

        ''' <summary>
        ''' ワーニングメッセージ有
        ''' </summary>
        ''' <returns></returns>
        Public Property HasWarning As Boolean = False

        ''' <summary>
        ''' ガイドメッセージ有
        ''' </summary>
        ''' <returns></returns>
        Public Property HasGuide As Boolean = False

    End Class

    ''' <summary>
    ''' メッセージモデル項目
    ''' </summary>
    ''' <remarks></remarks>
    Private Class ResItem

        ''' <summary>
        ''' メッセージ
        ''' </summary>
        ''' <returns></returns>
        Public Property Message As String = Nothing

        ''' <summary>
        ''' メッセージエリア
        ''' </summary>
        ''' <returns></returns>
        Public Property Area As String = Nothing

    End Class

#End Region 'Class

#Region "Method"

#Region "共通"

    ''' <summary>
    ''' スキーマ名を設定
    ''' </summary>
    ''' <param name="sql">変換元SQL</param>
    ''' <param name="brCd">営業所コード</param>
    ''' <returns>変換後SQL</returns>
    ''' <remarks></remarks>
    Private Function SetSchemaNm(ByVal sql As String, ByVal brCd As String) As String

        'トラン系のスキーマ名を設定
        sql = sql.Replace("$LM_TRN$", MyBase.GetDatabaseName(brCd, DBKbn.TRN))

        'マスタ系のスキーマ名を設定
        sql = sql.Replace("$LM_MST$", MyBase.GetDatabaseName(brCd, DBKbn.MST))

        Return sql

    End Function

#End Region '共通

#Region "運送(特大)データ取得"

    ''' <summary>
    ''' 運送(特大)データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function SelectUnsoLLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_SELECT_UNSO_LL_DATA, brCd))

        'Where句
        Dim whereTripNo = New StringBuilder()
        For Each dr As DataRow In inTbl.Rows

            '運行番号
            If String.IsNullOrEmpty(dr.Item("TRIP_NO").ToString()) = False Then
                '入力ありの場合
                If whereTripNo.Length > 0 Then
                    whereTripNo.Append(",")
                End If
                whereTripNo.Append("'")
                whereTripNo.Append(dr.Item("TRIP_NO").ToString())
                whereTripNo.Append("'")
            End If

        Next

        If whereTripNo.Length > 0 Then
            strSql.AppendLine("    AND UNSO_LL.TRIP_NO IN ( " & whereTripNo.ToString() & " )")
        Else
            '取得しない
            Return ds
        End If

        'Order句
        strSql.Append(SQL_ORDER_UNSO_LL_DATA)

        'SelectCommandの作成
        Dim sql As String = strSql.ToString()
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
            cmd.Parameters.Add(MyBase.GetSqlParameter("@USER_CD", MyBase.GetUserID(), DBDataType.CHAR))

            MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(TABLE_NM_ABHB910_UNSO_LL).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_ABHB910_UNSO_LL)
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region '運送(特大)データ取得

#Region "運送(大)データ取得"

    ''' <summary>
    ''' 運送(大)データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function SelectUnsoLData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_SELECT_UNSO_L_DATA, brCd))

        'Where句
        Dim whereUnsoNoL = New StringBuilder()
        Dim whereTripNo = New StringBuilder()
        For Each dr As DataRow In inTbl.Rows

            '運送番号L
            If String.IsNullOrEmpty(dr.Item("UNSO_NO_L").ToString()) = False Then
                '入力ありの場合
                If whereUnsoNoL.Length > 0 Then
                    whereUnsoNoL.Append(",")
                End If
                whereUnsoNoL.Append("'")
                whereUnsoNoL.Append(dr.Item("UNSO_NO_L").ToString())
                whereUnsoNoL.Append("'")
            End If

            '運行番号
            If String.IsNullOrEmpty(dr.Item("TRIP_NO").ToString()) = False Then
                '入力ありの場合
                If whereTripNo.Length > 0 Then
                    whereTripNo.Append(",")
                End If
                whereTripNo.Append("'")
                whereTripNo.Append(dr.Item("TRIP_NO").ToString())
                whereTripNo.Append("'")
            End If

        Next

        strSql.AppendLine("    AND ( 0 = 1")

        '運送番号L
        If whereUnsoNoL.Length > 0 Then
            strSql.AppendLine("          OR UNSO_L.UNSO_NO_L IN ( " & whereUnsoNoL.ToString() & " )")
        End If

        '運行番号
        If whereTripNo.Length > 0 Then
            strSql.AppendLine("          OR UNSO_L.TRIP_NO IN ( " & whereTripNo.ToString() & " )")
        End If

        strSql.AppendLine("    ) ")

        'Order句
        strSql.Append(SQL_ORDER_UNSO_L_DATA)

        'SelectCommandの作成
        Dim sql As String = strSql.ToString()
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
            cmd.Parameters.Add(MyBase.GetSqlParameter("@KBN_LANG", inRow.Item("KBN_LANG").ToString(), DBDataType.CHAR))
            cmd.Parameters.Add(MyBase.GetSqlParameter("@USER_CD", MyBase.GetUserID(), DBDataType.CHAR))

            MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(TABLE_NM_ABHB910_UNSO_L).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_ABHB910_UNSO_L)
                End If

            End Using

        End Using

        '出荷棟室データ取得
        ds = SelectOutkaTouSituData(ds)

        '入荷棟室データ取得
        ds = SelectInkaTouSituData(ds)

        Return ds

    End Function

#End Region '運送(大)データ取得

#Region "運送(中)データ取得"

    ''' <summary>
    ''' 運送(中)データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function SelectUnsoMData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)
        Dim sel_L_Tbl As DataTable = ds.Tables(TABLE_NM_ABHB910_UNSO_L)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_SELECT_UNSO_M_DATA, brCd))

        'Where句
        Dim whereUnsoNoL = New StringBuilder()
        For Each dr As DataRow In sel_L_Tbl.Rows

            '運送番号L
            If whereUnsoNoL.Length > 0 Then
                whereUnsoNoL.Append(",")
            End If
            whereUnsoNoL.Append("'")
            whereUnsoNoL.Append(dr.Item("UNSO_NO_L").ToString())
            whereUnsoNoL.Append("'")

        Next

        If whereUnsoNoL.Length > 0 Then
            strSql.AppendLine("    AND UNSO_M.UNSO_NO_L IN ( " & whereUnsoNoL.ToString() & " )")
        End If

        'Order句
        strSql.Append(SQL_ORDER_UNSO_M_DATA)

        'SelectCommandの作成
        Dim sql As String = strSql.ToString()
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
            cmd.Parameters.Add(MyBase.GetSqlParameter("@KBN_LANG", inRow.Item("KBN_LANG").ToString(), DBDataType.CHAR))
            cmd.Parameters.Add(MyBase.GetSqlParameter("@USER_CD", MyBase.GetUserID(), DBDataType.CHAR))

            MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(TABLE_NM_ABHB910_UNSO_M).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_ABHB910_UNSO_M)
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region '運送(中)データ取得

#Region "支払運賃データ取得"

    ''' <summary>
    ''' 支払運賃データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function SelectShiharaiData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)
        Dim sel_L_Tbl As DataTable = ds.Tables(TABLE_NM_ABHB910_UNSO_L)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_SELECT_SHIHARAI_DATA, brCd))

        'Where句
        Dim whereUnsoNoL = New StringBuilder()
        For Each dr As DataRow In sel_L_Tbl.Rows

            '運送番号L
            If whereUnsoNoL.Length > 0 Then
                whereUnsoNoL.Append(",")
            End If
            whereUnsoNoL.Append("'")
            whereUnsoNoL.Append(dr.Item("UNSO_NO_L").ToString())
            whereUnsoNoL.Append("'")

        Next

        If whereUnsoNoL.Length > 0 Then
            strSql.AppendLine("    AND SHIHARAI.UNSO_NO_L IN ( " & whereUnsoNoL.ToString() & " )")
        End If

        'Order句
        strSql.Append(SQL_ORDER_SHIHARAI_DATA)

        'SelectCommandの作成
        Dim sql As String = strSql.ToString()
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
            cmd.Parameters.Add(MyBase.GetSqlParameter("@USER_CD", MyBase.GetUserID(), DBDataType.CHAR))

            MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(TABLE_NM_ABHB910_SHIHARAI_TRS).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_ABHB910_SHIHARAI_TRS)
                End If

            End Using

        End Using

        Return ds

    End Function

#End Region '支払運賃データ取得

#Region "送信フラグ更新"

    ''' <summary>
    ''' 送信フラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function UpdSoshinFlg(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)
        Dim sel_L_Tbl As DataTable = ds.Tables(TABLE_NM_ABHB910_UNSO_L)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_UPD_UNSO_L_SOSHIN_FLG, brCd))

        For Each dr As DataRow In inTbl.Rows

            'SelectCommandの作成
            Dim sql As String = strSql.ToString()
            Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

                'パラメータ設定
                cmd.Parameters.Add(MyBase.GetSqlParameter("@HAISO_PF_SEND_FLG", "01", DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@UNSO_NO_L", dr.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("GUI_SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("GUI_SYS_UPD_TIME").ToString(), DBDataType.CHAR))

                'パラメータ設定（更新）
                Me.SetParamCommonSystemUpd(cmd.Parameters)

                MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                'SQLの発行
                Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

                If cnt < 1 Then
                    '排他エラー
                    MyBase.SetMessageStore("00", "E011", Nothing, String.Empty, "運送番号", dr.Item("UNSO_NO_L").ToString())
                End If

            End Using

        Next

        'IN情報以外に、運行に紐づく運送情報があれば、更新
        For Each dr As DataRow In sel_L_Tbl.Rows

            'Update済みはスキップ
            Dim rows As DataRow() = inTbl.Select($"UNSO_NO_L = '{dr.Item("UNSO_NO_L").ToString()}'")
            If rows.Length <> 0 Then
                Continue For
            End If

            'SelectCommandの作成
            Dim sql As String = strSql.ToString()
            Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

                'パラメータ設定
                cmd.Parameters.Add(MyBase.GetSqlParameter("@HAISO_PF_SEND_FLG", "01", DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@UNSO_NO_L", dr.Item("UNSO_NO_L").ToString(), DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_DATE", dr.Item("SYS_UPD_DATE").ToString(), DBDataType.CHAR))
                cmd.Parameters.Add(MyBase.GetSqlParameter("@GUI_SYS_UPD_TIME", dr.Item("SYS_UPD_TIME").ToString(), DBDataType.CHAR))

                'パラメータ設定（更新）
                Me.SetParamCommonSystemUpd(cmd.Parameters)

                MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

                'SQLの発行
                Dim cnt As Integer = MyBase.GetUpdateResult(cmd)

                If cnt < 1 Then
                    '排他エラー
                    MyBase.SetMessageStore("00", "E011", Nothing, String.Empty, "運送番号", dr.Item("UNSO_NO_L").ToString())
                End If

            End Using

        Next

        Return ds

    End Function

#End Region '送信フラグ更新

#Region "データ送信"

    ''' <summary>
    ''' データ送信
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Public Function DataSend(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'リクエストデータ設定
        Dim req As ReqABHB910 = SetReqABHB910(ds)

        'APIサーバーURL
        Dim apiUrl As String = $"{inRow("API_SERVER_URL").ToString()}/api/ABHB910"

        'APIキーヘッダ
        Dim apiKeyHeader As String = inRow("API_KEY_HEADER").ToString()

        'APIキー
        Dim apiKey As String = inRow("API_KEY").ToString()

        'ユーザー言語区分
        Dim language As String = inRow("API_LANGUAGE").ToString()

        'Httpリクエスト 生成
        Dim client As HttpClient = New HttpClient()
        Dim reqMsg As New HttpRequestMessage(HttpMethod.Post, apiUrl)

        'リクエストヘッダーを設定
        reqMsg.Headers.Add("Accept-Language", language)         'ユーザー言語区分
        reqMsg.Headers.Add(apiKeyHeader, apiKey)                'APIキー

        'リクエスト内容を設定
        Dim reqJson As String = New JavaScriptSerializer().Serialize(req)
        Dim content As New StringContent(reqJson, System.Text.Encoding.UTF8, "application/json")
        reqMsg.Content = content

        'Http送信
        Dim response As HttpResponseMessage = client.SendAsync(reqMsg).GetAwaiter().GetResult()

        'レスポンス取得
        Dim resStr As String = response.Content.ReadAsStringAsync().GetAwaiter().GetResult()
        Dim res As ResABHB910 = New JavaScriptSerializer().Deserialize(Of ResABHB910)(resStr)

        'エラーメッセージ設定
        If res.HasError Then
            For Each item As ResItem In res.Messages
                MyBase.SetMessageStore("00", "E01U", New String() {item.Message})
            Next
        End If

        Return ds

    End Function

#End Region 'データ送信

#Region "内部処理"

#Region "出荷棟室データ取得"

    ''' <summary>
    ''' 出荷棟室データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaTouSituData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)
        Dim sel_L_Tbl As DataTable = ds.Tables(TABLE_NM_ABHB910_UNSO_L)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_SELECT_OUTKA_TOUSITU_DATA, brCd))

        'Where句
        Dim whereInoutkaNoL = New StringBuilder()
        For Each dr As DataRow In sel_L_Tbl.Rows

            '入出荷管理番号L
            If String.IsNullOrEmpty(dr.Item("INOUTKA_NO_L").ToString()) = False Then
                '入力ありの場合
                If whereInoutkaNoL.Length > 0 Then
                    whereInoutkaNoL.Append(",")
                End If
                whereInoutkaNoL.Append("'")
                whereInoutkaNoL.Append(dr.Item("INOUTKA_NO_L").ToString())
                whereInoutkaNoL.Append("'")
            End If

        Next

        '入出荷管理番号L
        If whereInoutkaNoL.Length > 0 Then
            strSql.AppendLine("    AND OUTKA_L.OUTKA_NO_L IN ( " & whereInoutkaNoL.ToString() & " )")
        Else
            '取得しない
            Return ds
        End If

        'Order句
        strSql.Append(SQL_ORDER_OUTKA_TOUSITU_DATA)

        'SelectCommandの作成
        Dim sql As String = strSql.ToString()
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

            MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(TABLE_NM_INOUTKA_TOUSITU).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_INOUTKA_TOUSITU)
                End If

            End Using

        End Using

        For Each dr As DataRow In sel_L_Tbl.Rows

            If dr.Item("ORIG_CD").ToString() <> "999999999999999" Then
                Continue For
            End If

            '入出荷管理番号L
            Dim rows As DataRow() = ds.Tables(TABLE_NM_INOUTKA_TOUSITU) _
                .Select($"INOUTKA_NO_L = '{dr.Item("INOUTKA_NO_L").ToString()}'", "INOUTKA_NO_M, INOUTKA_NO_S")

            If rows.Length < 1 Then
                Continue For
            End If

            dr.Item("ORIG_NM") = rows(0).Item("TASYA_WH_NM").ToString()
            dr.Item("ORIG_KANA_NM") = String.Empty
            dr.Item("ORIG_ZIP") = rows(0).Item("TASYA_ZIP").ToString()
            dr.Item("ORIG_AD_1") = rows(0).Item("TASYA_AD_1").ToString()
            dr.Item("ORIG_AD_2") = rows(0).Item("TASYA_AD_2").ToString()
            dr.Item("ORIG_AD_3") = rows(0).Item("TASYA_AD_3").ToString()
            dr.Item("ORIG_TEL") = String.Empty
            dr.Item("ORIG_FAX") = String.Empty
            dr.Item("ORIG_KEN") = String.Empty
            dr.Item("ORIG_SHI") = String.Empty
        Next

        'テーブルクリア
        ds.Tables(TABLE_NM_INOUTKA_TOUSITU).Rows.Clear()

        Return ds

    End Function

#End Region '出荷棟室データ取得

#Region "入荷棟室データ取得"

    ''' <summary>
    ''' 入荷棟室データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInkaTouSituData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(IN_TBL_NM)
        Dim sel_L_Tbl As DataTable = ds.Tables(TABLE_NM_ABHB910_UNSO_L)

        'INTableの条件rowの格納
        Dim inRow As DataRow = inTbl.Rows(0)

        'SQL格納変数の初期化
        Dim strSql As New StringBuilder()

        'スキーマ名設定
        Dim brCd As String = inRow.Item("NRS_BR_CD").ToString()
        strSql.Append(Me.SetSchemaNm(SQL_SELECT_INKA_TOUSITU_DATA, brCd))

        'Where句
        Dim whereInoutkaNoL = New StringBuilder()
        For Each dr As DataRow In sel_L_Tbl.Rows

            '入出荷管理番号L
            If String.IsNullOrEmpty(dr.Item("INOUTKA_NO_L").ToString()) = False Then
                '入力ありの場合
                If whereInoutkaNoL.Length > 0 Then
                    whereInoutkaNoL.Append(",")
                End If
                whereInoutkaNoL.Append("'")
                whereInoutkaNoL.Append(dr.Item("INOUTKA_NO_L").ToString())
                whereInoutkaNoL.Append("'")
            End If

        Next

        '入出荷管理番号L
        If whereInoutkaNoL.Length > 0 Then
            strSql.AppendLine("    AND INKA_L.INKA_NO_L IN ( " & whereInoutkaNoL.ToString() & " )")
        Else
            '取得しない
            Return ds
        End If

        'Order句
        strSql.Append(SQL_ORDER_INKA_TOUSITU_DATA)

        'SelectCommandの作成
        Dim sql As String = strSql.ToString()
        Using cmd As SqlCommand = Me.CreateSqlCommand(sql)

            'パラメータの反映
            cmd.Parameters.Add(MyBase.GetSqlParameter("@NRS_BR_CD", brCd, DBDataType.CHAR))

            MyBase.Logger.WriteSQLLog("LMZ360DAC", System.Reflection.MethodBase.GetCurrentMethod.Name, cmd)

            'SQLの発行
            Using reader As SqlDataReader = MyBase.GetSelectResult(cmd)

                If reader.HasRows Then
                    '取得データの格納先をマッピング
                    Dim map As Hashtable = New Hashtable()
                    For Each item As String In Enumerable.Range(0, reader.FieldCount).Select(Function(i) reader.GetName(i))
                        If (ds.Tables(TABLE_NM_INOUTKA_TOUSITU).Columns.Contains(item)) Then
                            map.Add(item, item)
                        End If
                    Next

                    'DataReader→DataTableへの転記
                    ds = MyBase.SetSelectResultToDataSet(map, ds, reader, TABLE_NM_INOUTKA_TOUSITU)
                End If

            End Using

        End Using

        For Each dr As DataRow In sel_L_Tbl.Rows

            If dr.Item("DEST_CD").ToString() <> "999999999999999" Then
                Continue For
            End If

            '入出荷管理番号L
            Dim rows As DataRow() = ds.Tables(TABLE_NM_INOUTKA_TOUSITU) _
                .Select($"INOUTKA_NO_L = '{dr.Item("INOUTKA_NO_L").ToString()}'", "INOUTKA_NO_M, INOUTKA_NO_S")

            If rows.Length < 1 Then
                Continue For
            End If

            dr.Item("DEST_NM") = rows(0).Item("TASYA_WH_NM").ToString()
            dr.Item("DEST_KANA_NM") = String.Empty
            dr.Item("DEST_ZIP") = rows(0).Item("TASYA_ZIP").ToString()
            dr.Item("DEST_AD_1") = rows(0).Item("TASYA_AD_1").ToString()
            dr.Item("DEST_AD_2") = rows(0).Item("TASYA_AD_2").ToString()
            dr.Item("DEST_AD_3") = rows(0).Item("TASYA_AD_3").ToString()
            dr.Item("DEST_TEL") = String.Empty
            dr.Item("DEST_FAX") = String.Empty
            dr.Item("DEST_KEN") = String.Empty
            dr.Item("DEST_SHI") = String.Empty
        Next

        'テーブルクリア
        ds.Tables(TABLE_NM_INOUTKA_TOUSITU).Rows.Clear()

        Return ds

    End Function

#End Region '入荷棟室データ取得

#Region "リクエスト設定"

    ''' <summary>
    ''' リクエスト設定 ABHB910
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function SetReqABHB910(ByVal ds As DataSet) As ReqABHB910

        'リクエスト生成
        Dim req As New ReqABHB910()

        '運送（特大）
        For Each dr As DataRow In ds.Tables(TABLE_NM_ABHB910_UNSO_LL).Rows
            Dim unsoLLItem As New HbUnsoLLItem()
            With unsoLLItem

                ' キャンセルフラグ
                .CANCEL_FLG = "00000"

                ' 営業所コード
                .NRS_BR_CD = dr.Item("NRS_BR_CD").ToString()

                ' 運行番号
                .TRIP_NO = dr.Item("TRIP_NO").ToString()

                ' 運送会社コード
                .UNSOCO_CD = dr.Item("UNSOCO_CD").ToString()

                ' 運送会社支店コード
                .UNSOCO_BR_CD = dr.Item("UNSOCO_BR_CD").ToString()

                ' 自傭車区分
                .JSHA_KB = dr.Item("JSHA_KB").ToString()

                ' 便区分
                .BIN_KB = dr.Item("BIN_KB").ToString()

                ' 車輌KEY
                .CAR_KEY = dr.Item("CAR_KEY").ToString()

                ' 運送温度
                .UNSO_ONDO = dr.Item("UNSO_ONDO").ToString()

                ' 乗務員コード
                .DRIVER_CD = dr.Item("DRIVER_CD").ToString()

                ' 運行日
                .TRIP_DATE = dr.Item("TRIP_DATE").ToString()

                ' 下払金額
                .PAY_UNCHIN = dr.Item("PAY_UNCHIN").ToString()

                ' 支払タリフコード
                .PAY_TARIFF_CD = dr.Item("PAY_TARIFF_CD").ToString()

                ' 配送区分
                .HAISO_KB = dr.Item("HAISO_KB").ToString()

                ' 備考
                .REMARK = dr.Item("REMARK").ToString()

                ' 支払タリフコード
                .SHIHARAI_TARIFF_CD = dr.Item("SHIHARAI_TARIFF_CD").ToString()

                ' 支払割増タリフコード
                .SHIHARAI_ETARIFF_CD = dr.Item("SHIHARAI_ETARIFF_CD").ToString()

                ' 支払運送重量
                .SHIHARAI_UNSO_WT = dr.Item("SHIHARAI_UNSO_WT").ToString()

                ' 支払件数
                .SHIHARAI_COUNT = dr.Item("SHIHARAI_COUNT").ToString()

                ' 支払金額
                .SHIHARAI_UNCHIN = dr.Item("SHIHARAI_UNCHIN").ToString()

                ' 支払タリフ分類区分
                .SHIHARAI_TARIFF_BUNRUI_KB = dr.Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString()

            End With
            req.UnsoLL_List.Add(unsoLLItem)
        Next

        '運送（大）
        For Each dr As DataRow In ds.Tables(TABLE_NM_ABHB910_UNSO_L).Rows
            Dim unsoLItem As New HbUnsoLItem()
            With unsoLItem

                ' キャンセルフラグ
                .CANCEL_FLG = "00000"

                ' 予約番号
                .YOYAKU_NO = String.Empty

                ' 配送まとめ番号
                .HAISO_GROUP_NO = String.Empty

                ' 製品セグメント
                .PRODUCT_SEG_CD = dr.Item("PRODUCT_SEG_CD").ToString()

                ' 真荷主BPCD
                .TCUST_BPCD = dr.Item("TCUST_BPCD").ToString()

                ' 営業所コード
                .NRS_BR_CD = dr.Item("NRS_BR_CD").ToString()

                ' 作成者
                .CRT_USER_NM = MyBase.GetUserName()

                ' 運送番号L
                .UNSO_NO_L = dr.Item("UNSO_NO_L").ToString()

                ' 輸送部営業所コード
                .YUSO_BR_CD = dr.Item("YUSO_BR_CD").ToString()

                ' 入出荷管理番号L
                .INOUTKA_NO_L = dr.Item("INOUTKA_NO_L").ToString()

                ' 運行番号
                .TRIP_NO = dr.Item("TRIP_NO").ToString()

                ' 運送会社コード
                .UNSO_CD = dr.Item("UNSO_CD").ToString()

                ' 運送会社支店コード
                .UNSO_BR_CD = dr.Item("UNSO_BR_CD").ToString()

                ' 日陸経理コード(JDE)
                .UNSO_BP_CD = dr.Item("UNSO_BP_CD").ToString()

                ' 便区分
                .BIN_KB = dr.Item("BIN_KB").ToString()

                ' 運送事由区分
                .JIYU_KB = dr.Item("JIYU_KB").ToString()

                ' 送状番号
                .DENP_NO = dr.Item("DENP_NO").ToString()

                ' 自動送状区分
                .AUTO_DENP_KBN = dr.Item("AUTO_DENP_KBN").ToString()

                ' 自動送状番号
                .AUTO_DENP_NO = dr.Item("AUTO_DENP_NO").ToString()

                ' 出荷予定日
                .OUTKA_PLAN_DATE = dr.Item("OUTKA_PLAN_DATE").ToString()

                ' 出荷予定時刻
                .OUTKA_PLAN_TIME = dr.Item("OUTKA_PLAN_TIME").ToString()

                ' 納入予定日
                .ARR_PLAN_DATE = dr.Item("ARR_PLAN_DATE").ToString()

                ' 納入予定時刻
                .ARR_PLAN_TIME = dr.Item("ARR_PLAN_TIME").ToString()

                ' 納入実時刻
                .ARR_ACT_TIME = dr.Item("ARR_ACT_TIME").ToString()

                ' 荷主コード (大)
                .CUST_CD_L = dr.Item("CUST_CD_L").ToString()

                ' 荷主コード (中)
                .CUST_CD_M = dr.Item("CUST_CD_M").ToString()

                ' 荷主名（大）
                .CUST_NM_L = dr.Item("CUST_NM_L").ToString()

                ' 荷主参照番号
                .CUST_REF_NO = dr.Item("CUST_REF_NO").ToString()

                ' 荷送人コード
                .SHIP_CD = dr.Item("SHIP_CD").ToString()

                ' 発地コード
                .ORIG_CD = dr.Item("ORIG_CD").ToString()

                ' 発地名称
                .ORIG_NM = dr.Item("ORIG_NM").ToString()

                ' 発地カナ名
                .ORIG_KANA_NM = dr.Item("ORIG_KANA_NM").ToString()

                ' 発地郵便番号
                .ORIG_ZIP = dr.Item("ORIG_ZIP").ToString()

                ' 発地住所1
                .ORIG_AD_1 = dr.Item("ORIG_AD_1").ToString()

                ' 発地住所2
                .ORIG_AD_2 = dr.Item("ORIG_AD_2").ToString()

                ' 発地住所3
                .ORIG_AD_3 = dr.Item("ORIG_AD_3").ToString()

                ' 発地電話番号
                .ORIG_TEL = dr.Item("ORIG_TEL").ToString()

                ' 発地FAX番号
                .ORIG_FAX = dr.Item("ORIG_FAX").ToString()

                ' 発地都道府県名
                .ORIG_KEN = dr.Item("ORIG_KEN").ToString()

                ' 発地市区町村名
                .ORIG_SHI = dr.Item("ORIG_SHI").ToString()

                ' 届先コード
                .DEST_CD = dr.Item("DEST_CD").ToString()

                ' 届先名称
                .DEST_NM = dr.Item("DEST_NM").ToString()

                ' 届先カナ名
                .DEST_KANA_NM = dr.Item("DEST_KANA_NM").ToString()

                ' 届先郵便番号
                .DEST_ZIP = dr.Item("DEST_ZIP").ToString()

                ' 届先住所1
                .DEST_AD_1 = dr.Item("DEST_AD_1").ToString()

                ' 届先住所2
                .DEST_AD_2 = dr.Item("DEST_AD_2").ToString()

                ' 届先住所3
                .DEST_AD_3 = dr.Item("DEST_AD_3").ToString()

                ' 届先電話番号
                .DEST_TEL = dr.Item("DEST_TEL").ToString()

                ' 届先FAX番号
                .DEST_FAX = dr.Item("DEST_FAX").ToString()

                ' 届先都道府県名
                .DEST_KEN = dr.Item("DEST_KEN").ToString()

                ' 届先市区町村名
                .DEST_SHI = dr.Item("DEST_SHI").ToString()

                ' 運送梱包個数
                .UNSO_PKG_NB = dr.Item("UNSO_PKG_NB").ToString()

                ' 個数単位
                .NB_UT = dr.Item("NB_UT").ToString()

                ' 運送重量
                .UNSO_WT = dr.Item("UNSO_WT").ToString()

                ' 運送温度区分
                .UNSO_ONDO_KB = dr.Item("UNSO_ONDO_KB").ToString()

                ' 元着払区分
                .PC_KB = dr.Item("PC_KB").ToString()

                ' タリフ分類区分
                .TARIFF_BUNRUI_KB = dr.Item("TARIFF_BUNRUI_KB").ToString()

                ' 車輌区分
                .VCLE_KB = dr.Item("VCLE_KB").ToString()

                ' 元データ区分
                .MOTO_DATA_KB = dr.Item("MOTO_DATA_KB").ToString()

                ' 課税区分
                .TAX_KB = dr.Item("TAX_KB").ToString()

                ' 備考
                .REMARK = dr.Item("REMARK").ToString()

                ' タリフコード
                .SEIQ_TARIFF_CD = dr.Item("SEIQ_TARIFF_CD").ToString()

                ' 割増タリフコード
                .SEIQ_ETARIFF_CD = dr.Item("SEIQ_ETARIFF_CD").ToString()

                ' 届先住所3
                .AD_3 = dr.Item("AD_3").ToString()

                ' 運送手配区分
                .UNSO_TEHAI_KB = dr.Item("UNSO_TEHAI_KB").ToString()

                ' 買主注文番号
                .BUY_CHU_NO = dr.Item("BUY_CHU_NO").ToString()

                ' 配送区域
                .AREA_CD = dr.Item("AREA_CD").ToString()

                ' 中継配送フラグ
                .TYUKEI_HAISO_FLG = dr.Item("TYUKEI_HAISO_FLG").ToString()

                ' 集荷中継地
                .SYUKA_TYUKEI_CD = dr.Item("SYUKA_TYUKEI_CD").ToString()

                ' 配荷中継地
                .HAIKA_TYUKEI_CD = dr.Item("HAIKA_TYUKEI_CD").ToString()

                ' 運行番号（集荷）
                .TRIP_NO_SYUKA = dr.Item("TRIP_NO_SYUKA").ToString()

                ' 運行番号（中継）
                .TRIP_NO_TYUKEI = dr.Item("TRIP_NO_TYUKEI").ToString()

                ' 運行番号（配荷）
                .TRIP_NO_HAIKA = dr.Item("TRIP_NO_HAIKA").ToString()

                ' 支払タリフコード
                .SHIHARAI_TARIFF_CD = dr.Item("SHIHARAI_TARIFF_CD").ToString()

                ' 支払割増タリフコード
                .SHIHARAI_ETARIFF_CD = dr.Item("SHIHARAI_ETARIFF_CD").ToString()

                ' 幹線配達区分
                .MAIN_DELI_KB = dr.Item("MAIN_DELI_KB").ToString()

                ' 備考
                .NHS_REMARK = dr.Item("NHS_REMARK").ToString()

            End With
            req.UnsoL_List.Add(unsoLItem)
        Next

        '運送（中）
        For Each dr As DataRow In ds.Tables(TABLE_NM_ABHB910_UNSO_M).Rows
            Dim unsoMItem As New HbUnsoMItem()
            With unsoMItem

                ' 営業所コード
                .NRS_BR_CD = dr.Item("NRS_BR_CD").ToString()

                ' 運送番号L
                .UNSO_NO_L = dr.Item("UNSO_NO_L").ToString()

                ' 運送番号M
                .UNSO_NO_M = dr.Item("UNSO_NO_M").ToString()

                ' 商品KEY
                .GOODS_CD_NRS = dr.Item("GOODS_CD_NRS").ToString()

                ' 商品名
                .GOODS_NM = dr.Item("GOODS_NM").ToString()

                ' 消防コード名
                .SHOBO_NM = dr.Item("SHOBO_NM").ToString()

                ' 毒劇区分名
                .DOKU_NM = dr.Item("DOKU_NM").ToString()

                ' 運送個数
                .UNSO_TTL_NB = dr.Item("UNSO_TTL_NB").ToString()

                ' 個数単位コード
                .NB_UT = dr.Item("NB_UT").ToString()

                ' 運送数量
                .UNSO_TTL_QT = dr.Item("UNSO_TTL_QT").ToString()

                ' 数量単位コード
                .QT_UT = dr.Item("QT_UT").ToString()

                ' 端数
                .HASU = dr.Item("HASU").ToString()

                ' 在庫レコード番号
                .ZAI_REC_NO = dr.Item("ZAI_REC_NO").ToString()

                ' 運送温度区分
                .UNSO_ONDO_KB = dr.Item("UNSO_ONDO_KB").ToString()

                ' 入目
                .IRIME = dr.Item("IRIME").ToString()

                ' 入目単位コード
                .IRIME_UT = dr.Item("IRIME_UT").ToString()

                ' 個別重量
                .BETU_WT = dr.Item("BETU_WT").ToString()

                ' 宅急便サイズ区分
                .SIZE_KB = dr.Item("SIZE_KB").ToString()

                ' 在庫部課コード
                .ZBUKA_CD = dr.Item("ZBUKA_CD").ToString()

                ' 扱い部課コード
                .ABUKA_CD = dr.Item("ABUKA_CD").ToString()

                ' 梱包個数
                .PKG_NB = dr.Item("PKG_NB").ToString()

                ' ロット№
                .LOT_NO = dr.Item("LOT_NO").ToString()

                ' 備考
                .REMARK = dr.Item("REMARK").ToString()

                ' 運送保険料有無
                .UNSO_HOKEN_UM = dr.Item("UNSO_HOKEN_UM").ToString()

                ' 寄託商品単価
                .KITAKU_GOODS_UP = dr.Item("KITAKU_GOODS_UP").ToString()

            End With
            req.UnsoM_List.Add(unsoMItem)
        Next

        '支払運賃
        For Each dr As DataRow In ds.Tables(TABLE_NM_ABHB910_SHIHARAI_TRS).Rows
            Dim shiharaiItem As New HbShiharaiItem()
            With shiharaiItem

                ' 輸送部営業所コード
                .YUSO_BR_CD = dr.Item("YUSO_BR_CD").ToString()

                ' 営業所コード
                .NRS_BR_CD = dr.Item("NRS_BR_CD").ToString()

                ' 運送番号L
                .UNSO_NO_L = dr.Item("UNSO_NO_L").ToString()

                ' 運送番号M
                .UNSO_NO_M = dr.Item("UNSO_NO_M").ToString()

                ' 荷主コードL
                .CUST_CD_L = dr.Item("CUST_CD_L").ToString()

                ' 荷主コードM
                .CUST_CD_M = dr.Item("CUST_CD_M").ToString()

                ' 荷主コードS
                .CUST_CD_S = dr.Item("CUST_CD_S").ToString()

                ' 荷主コードSS
                .CUST_CD_SS = dr.Item("CUST_CD_SS").ToString()

                ' 支払グループ番号L
                .SHIHARAI_GROUP_NO = dr.Item("SHIHARAI_GROUP_NO").ToString()

                ' 支払グループ番号M
                .SHIHARAI_GROUP_NO_M = dr.Item("SHIHARAI_GROUP_NO_M").ToString()

                ' 支払先コード
                .SHIHARAITO_CD = dr.Item("SHIHARAITO_CD").ToString()

                ' 運賃計算締日区分
                .UNTIN_CALCULATION_KB = dr.Item("UNTIN_CALCULATION_KB").ToString()

                ' 車輌区分
                .SHIHARAI_SYARYO_KB = dr.Item("SHIHARAI_SYARYO_KB").ToString()

                ' 包装単位（荷姿）
                .SHIHARAI_PKG_UT = dr.Item("SHIHARAI_PKG_UT").ToString()

                ' 荷姿個数
                .SHIHARAI_NG_NB = dr.Item("SHIHARAI_NG_NB").ToString()

                ' 危険区分
                .SHIHARAI_DANGER_KB = dr.Item("SHIHARAI_DANGER_KB").ToString()

                ' タリフ分類区分
                .SHIHARAI_TARIFF_BUNRUI_KB = dr.Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString()

                ' タリフコード
                .SHIHARAI_TARIFF_CD = dr.Item("SHIHARAI_TARIFF_CD").ToString()

                ' 支払割増タリフコード
                .SHIHARAI_ETARIFF_CD = dr.Item("SHIHARAI_ETARIFF_CD").ToString()

                ' 支払適用距離
                .SHIHARAI_KYORI = dr.Item("SHIHARAI_KYORI").ToString()

                ' 支払適用重量
                .SHIHARAI_WT = dr.Item("SHIHARAI_WT").ToString()

                ' 支払運賃
                .SHIHARAI_UNCHIN = dr.Item("SHIHARAI_UNCHIN").ToString()

                ' 支払都市割増
                .SHIHARAI_CITY_EXTC = dr.Item("SHIHARAI_CITY_EXTC").ToString()

                ' 支払冬期割増
                .SHIHARAI_WINT_EXTC = dr.Item("SHIHARAI_WINT_EXTC").ToString()

                ' 支払中継料
                .SHIHARAI_RELY_EXTC = dr.Item("SHIHARAI_RELY_EXTC").ToString()

                ' 支払通行料
                .SHIHARAI_TOLL = dr.Item("SHIHARAI_TOLL").ToString()

                ' 支払保険料
                .SHIHARAI_INSU = dr.Item("SHIHARAI_INSU").ToString()

                ' 支払料金確定フラグ
                .SHIHARAI_FIXED_FLAG = dr.Item("SHIHARAI_FIXED_FLAG").ToString()

                ' 確定荷姿個数
                .DECI_NG_NB = dr.Item("DECI_NG_NB").ToString()

                ' 確定適用距離
                .DECI_KYORI = dr.Item("DECI_KYORI").ToString()

                ' 確定適用重量
                .DECI_WT = dr.Item("DECI_WT").ToString()

                ' 確定支払運賃
                .DECI_UNCHIN = dr.Item("DECI_UNCHIN").ToString()

                ' 確定支払都市割増
                .DECI_CITY_EXTC = dr.Item("DECI_CITY_EXTC").ToString()

                ' 確定支払冬期割増
                .DECI_WINT_EXTC = dr.Item("DECI_WINT_EXTC").ToString()

                ' 確定支払中継料
                .DECI_RELY_EXTC = dr.Item("DECI_RELY_EXTC").ToString()

                ' 確定支払通行料
                .DECI_TOLL = dr.Item("DECI_TOLL").ToString()

                ' 確定支払保険料
                .DECI_INSU = dr.Item("DECI_INSU").ToString()

                ' 管理用支払運賃
                .KANRI_UNCHIN = dr.Item("KANRI_UNCHIN").ToString()

                ' 管理用支払都市割増
                .KANRI_CITY_EXTC = dr.Item("KANRI_CITY_EXTC").ToString()

                ' 管理用支払冬期割増
                .KANRI_WINT_EXTC = dr.Item("KANRI_WINT_EXTC").ToString()

                ' 管理用支払中継料
                .KANRI_RELY_EXTC = dr.Item("KANRI_RELY_EXTC").ToString()

                ' 管理用支払通行料
                .KANRI_TOLL = dr.Item("KANRI_TOLL").ToString()

                ' 管理用支払保険料
                .KANRI_INSU = dr.Item("KANRI_INSU").ToString()

                ' 備考（荷主注文番号）
                .REMARK = dr.Item("REMARK").ToString()

                ' 宅急便サイズ区分
                .SIZE_KB = dr.Item("SIZE_KB").ToString()

                ' 課税区分
                .TAX_KB = dr.Item("TAX_KB").ToString()

                ' 作業料管理用
                .SAGYO_KANRI = dr.Item("SAGYO_KANRI").ToString()

            End With
            req.Shiharai_List.Add(shiharaiItem)
        Next

        Return req

    End Function

#End Region 'リクエスト設定

#Region "パラメータ設定"

    ''' <summary>
    ''' パラメータ設定モジュール(システム共通項目(更新時))
    ''' </summary>
    ''' <param name="params">SQLパラメータ</param>
    ''' <remarks></remarks>
    Private Sub SetParamCommonSystemUpd(ByVal params As SqlParameterCollection)

        'パラメータ設定
        params.Add(MyBase.GetSqlParameter("@SYS_UPD_DATE", MyBase.GetSystemDate(), DBDataType.CHAR))
        params.Add(MyBase.GetSqlParameter("@SYS_UPD_TIME", MyBase.GetSystemTime(), DBDataType.CHAR))
        params.Add(MyBase.GetSqlParameter("@SYS_UPD_PGID", MyBase.GetPGID(), DBDataType.CHAR))
        params.Add(MyBase.GetSqlParameter("@SYS_UPD_USER", MyBase.GetUserID(), DBDataType.CHAR))

    End Sub

#End Region 'パラメータ設定

#End Region '内部処理

#End Region 'Method

End Class
