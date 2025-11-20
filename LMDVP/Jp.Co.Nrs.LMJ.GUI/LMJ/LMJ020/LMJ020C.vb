' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ020C : 未使用荷主データ退避
'  作  成  者       :  s.kobayashi
' ==========================================================================

''' <summary>
''' LMJ020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMJ020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' 月末在庫情報取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_KENSAKU As String = "SelectGetuData"

    ''' <summary>
    ''' 実行時のデータ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_JIKKO_ESC As String = "ProcessEscape"

    ''' <summary>
    ''' 実行時のデータ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_JIKKO_MODOSHI As String = "ProcessDataModoshi"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMJ020IN"

    ''' <summary>
    ''' OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_OUT As String = "LMJ020OUT"

    ''' <summary>
    ''' TARGET_TBLテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_TARGETTBL As String = "TargetTables"
    ''' <summary>
    ''' SYS_DATETIMEテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"

    ''' <summary>
    ''' ERRテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' GETU_INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GETU_IN As String = "GETU_IN"

    ''' <summary>
    ''' GETU_OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GETU_OUT As String = "GETU_OUT"

    Public Const SHORI_ESCAPE As String = "00"

    Public Const SHORI_DATA_MODOSHI As String = "01"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        SHORI = 0
        EIGYO
        LastUpdDate

    End Enum

    ''' <summary>
    ''' アクションタイプ
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ActionType As Integer

        KENSAKU = 0
        JIKKOU_ESCAPE
        JIKKOU_MODOSHI
        CLOSE

    End Enum

    ''' <summary>
    ''' 行インデックス 
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        CUST_CD_L
        CUST_NM_L
        LAST_UPD_DATE
        TANTO_USER
        TAIHI_DATE
        TAIHI_USER
        NRS_BR_CD
        PROCESS_KB
    End Enum


    ''' <summary>
    ''' 移行テーブル（LM_MST)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TARGET_TABLES_MST
        M_COA = 1000
        M_COACONFIG
        M_CUSTCOND
        M_DEST_DETAILS
        M_DESTGOODS
        M_FURI_GOODS
        M_GOODS_DETAILS
        M_HANDY_CUST
        M_OKURIJO_CSV
        M_SAGYO
        M_TANKA
        M_UNCHIN_TARIFF_SET
        M_DEST
        M_GOODS

    End Enum

    ''' <summary>
    ''' 移行テーブル（LM_TRN)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum TARGET_TABLES_TRN
     
        H_SENDEDI_BP = 0
        H_SENDINEDI_DPN
        H_SENDINEDI_NCGO
        H_SENDINEDI_NIK
        H_SENDINEDI_NSN
        H_SENDINEDI_UTI
        H_SENDINOUTEDI_DOW
        H_SENDINOUTEDI_UKM
        H_SENDMONTHLY_SNZ
        H_SENDOUTEDI_ASH
        H_SENDOUTEDI_DPN
        H_SENDOUTEDI_NCGO
        H_SENDOUTEDI_NIK
        H_SENDOUTEDI_NSN
        H_SENDOUTEDI_SFJ
        H_SENDOUTEDI_SNK
        H_EDI_GOODSREP_TBL
        H_GOODS_EDI_BP
        H_INKAEDI_DTL_BP
        H_INKAEDI_DTL_DPN
        H_INKAEDI_DTL_NCGO
        H_INKAEDI_DTL_NIK
        H_INKAEDI_DTL_NISSAN
        H_INKAEDI_DTL_NSN
        H_INKAEDI_DTL_UKM
        H_INKAEDI_HED_BP
        H_INKAEDI_HED_DPN
        H_INKAEDI_HED_NCGO
        H_INKAEDI_HED_NSN
        H_INOUTKAEDI_DTL_DIC_NEW
        H_INOUTKAEDI_DTL_DOW
        H_INOUTKAEDI_DTL_FJF
        H_INOUTKAEDI_DTL_M3PL
        H_INOUTKAEDI_DTL_SMK
        H_INOUTKAEDI_DTL_TOHO
        H_INOUTKAEDI_HED_DIC_NEW
        H_INOUTKAEDI_HED_DOW
        H_INOUTKAEDI_HED_FJF
        H_INOUTKAEDI_HED_SMK
        H_INOUTKAEDI_HED_TOHO
        H_OUTKA_L_BP
        H_OUTKAEDI_DTL_ASH
        H_OUTKAEDI_DTL_BP
        H_OUTKAEDI_DTL_BYK
        H_OUTKAEDI_DTL_DIC
        H_OUTKAEDI_DTL_DNS
        H_OUTKAEDI_DTL_DPN
        H_OUTKAEDI_DTL_DSP
        H_OUTKAEDI_DTL_DSPAH
        H_OUTKAEDI_DTL_GODO
        H_OUTKAEDI_DTL_HON
        H_OUTKAEDI_DTL_JC
        H_OUTKAEDI_DTL_JT
        H_OUTKAEDI_DTL_KTK
        H_OUTKAEDI_DTL_LNZ
        H_OUTKAEDI_DTL_MHM
        H_OUTKAEDI_DTL_NCGO
        H_OUTKAEDI_DTL_NIK
        H_OUTKAEDI_DTL_NKS
        H_OUTKAEDI_DTL_NSN
        H_OUTKAEDI_DTL_OTK
        H_OUTKAEDI_DTL_SFJ
        H_OUTKAEDI_DTL_SNK
        H_OUTKAEDI_DTL_SNZ
        H_OUTKAEDI_DTL_TOR
        H_OUTKAEDI_DTL_UKM
        H_OUTKAEDI_HED_ASH
        H_OUTKAEDI_HED_BP
        H_OUTKAEDI_HED_DIC
        H_OUTKAEDI_HED_DNS
        H_OUTKAEDI_HED_DPN
        H_OUTKAEDI_HED_GODO
        H_OUTKAEDI_HED_HON
        H_OUTKAEDI_HED_JC
        H_OUTKAEDI_HED_NCGO
        H_OUTKAEDI_HED_NSN
        H_OUTKAEDI_HED_OTK
        H_OUTKAEDI_HED_SFJ
        H_OUTKAEDI_HED_TOR
        H_OUTKAEDI_HED_UKM
        H_OUTKAEDI_M_PRT_LNZ
        H_OUTKAEDI_L_PRT_LNZ
        H_UNSOCO_EDI
        H_ZAIKO_EDI_BP
        H_INKAEDI_M
        H_INKAEDI_L
        H_OUTKAEDI_M
        H_OUTKAEDI_L
        H_EDI_PRINT
        I_CONT_TRACK
        I_CONT_TRACK_LOG
        I_DOW_SEIQ_PRT
        I_HAISO_UNCHIN_TRS
        I_HIKITORI_UNCHIN_MEISAI
        I_HON_TEIKEN
        I_HONEY_ALBAS_CHG
        I_HONEY_SHIPTOCD_CHG
        I_MCPU_UNCHIN_CHK
        I_NRC_KAISHU_TBL
        I_UKI_BUNRUI_MST
        I_UKI_HOKOKU
        I_UKI_ZAIKO
        I_UKI_ZAIKO_SUM
        I_UKIMA_SEKY_MEISAI
        I_YOKO_UNCHIN_TRS
        I_YUSO_R
        I_YUSO_R_SUM
        M_BYK_GOODS
        M_CHOKUSO_NIK
        M_HINMOKU_FJF
        M_HINMOKU_TRM
        M_KOKYAKU_TRM
        M_SEHIN_NIK
        M_SET_GOODS_LNZ
        M_TOKUI_FJF
        M_TOKUI_NIK
        H_NRSBIN_DIC
        H_NRSBIN_TOR
        H_NRSCUST_DIC
        H_NRSGOODS_DIC
        H_NRSGOODS_DNS
        Q_WK_INOUT
        W_ZAI_SHOMEI
        B_INKA_S
        B_INKA_M
        B_INKA_L
        C_OUTKA_S
        C_OUTKA_M
        C_OUTKA_L
        D_IDO_HANDY
        D_WK_ZAI_PRT
        D_ZAI_SHOGOH
        D_ZAI_SHOGOH_CUST
        D_ZAI_SHOGOH_CUST_SUM
        D_ZAI_ZAN_JITSU
        D_IDO_TRS
        D_ZAI_TRS
        E_SAGYO_SIJI
        E_SAGYO
        F_UNSO_M
        F_UNSO_LL
        F_SHIHARAI_TRS
        F_UNCHIN_TRS
        F_UNSO_L
        G_DUPONT_INTERFACE_TRS
        G_DUPONT_SEKY_GL
        G_KAGAMI_DTL
        G_KAGAMI_HED
        G_SEKY_MEISAI
        G_SEKY_MEISAI_PRT
        G_SEKY_TBL
        G_TANKA_WK
        G_ZAIK_ZAN
    End Enum


End Class
