' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM090C : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================

''' <summary>
''' LMM090定数定義クラス
''' </summary>
''' <remarks></remarks>
Friend Class LMM090C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Friend Const TABLE_NM_IN As String = "LMM090IN"
    Friend Const TABLE_NM_CUST As String = "LMM090OUT"
    Friend Const TABLE_NM_CUST_PRT As String = "LMM090_CUST_RPT"
    Friend Const TABLE_NM_TARIFF As String = "LMM090_TARIFF_SET"
    '要望番号:349 yamanaka 2012.07.10 Start
    Friend Const TABLE_NM_CUST_DETAILS As String = "LMM090_CUST_DETAILS"
    '要望番号:349 yamanaka 2012.07.10 End
    Friend Const TABLE_NM_VAR_STRAGE As String = "LMM090_VAR_STRAGE"

    ''' <summary>
    ''' 最低保証適用単位区分【区分マスタ：S018】
    ''' </summary>
    ''' <remarks>00:無し、01:ロット入り目、02:置場も分割、03:入荷毎に分割</remarks>
    Friend Const HOSHO_MIN_KBN_NASHI As String = "00"
    Friend Const HOSHO_MIN_KBN_LOT As String = "01"
    Friend Const HOSHO_MIN_KBN_OKIBA As String = "02"
    Friend Const HOSHO_MIN_KBN_BUNKATU As String = "03"

    ''' <summary>
    ''' 請求時簿外品取扱区分【区分マスタ：S071】
    ''' </summary>
    ''' <remarks>01:設定不要、02:入荷時の 簿外品区分 を設定、03:常に 簿外品 を設定</remarks>
    Friend Const SEIQ_HAKUGAIHIN_KBN_HUYO As String = "01"
    Friend Const SEIQ_HAKUGAIHIN_KBN_NYUKA As String = "02"
    Friend Const SEIQ_HAKUGAIHIN_KBN_HAKUGAIHIN As String = "03"

    ''' <summary>
    ''' セットマスタコード
    ''' </summary>
    ''' <remarks>000：出荷、001：入荷</remarks>
    ''' '(2012.06.22)要望番号1178 セットマスタコードを3桁⇒4桁
    Friend Const SET_MST_CD_SHUKKA As String = "0000"
    Friend Const SET_MST_CD_NYUKA As String = "0001"
    'Friend Const SET_MST_CD_SHUKKA As String = "000"
    'Friend Const SET_MST_CD_NYUKA As String = "001"

    ''' <summary>
    ''' セット区分
    ''' </summary>
    ''' <remarks>00：出荷、02：入荷</remarks>
    Friend Const SET_KBN_SHUKKA As String = "00"
    Friend Const SET_KBN_NYUKA As String = "02"

    '要望番号:349 yamanaka 2012.07.11 Start
    ''' <summary>
    ''' 荷主階層
    ''' </summary>
    ''' <remarks>00：大、01：中、02：小、03：極小</remarks>
    Friend Const CUST_CLASS_L As String = "00"
    Friend Const CUST_CLASS_M As String = "01"
    Friend Const CUST_CLASS_S As String = "02"
    Friend Const CUST_CLASS_SS As String = "03"

    ''' <summary>
    ''' 最大枝番
    ''' </summary>
    ''' <remarks>固定</remarks>
    Friend Const MAX_EDA_NO As String = "99"
    '要望番号:349 yamanaka 2012.07.11 End

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum EventShubetsu As Integer

        SHINKI = 0
        HENSHU
        FUKUSHA
        SAKUJO_HUKKATU
        TANKA_IKKATU_HENKO
        KENSAKU
        MASTEROPEN
        HOZON
        TOJIRU
        DOUBLE_CLICK
        ADD_ROW
        DEL_ROW
        ENTER

    End Enum

    ''' <summary>
    ''' 編集、複写ボタンクリック対象を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum ClickObject As Integer

        INIT = 0
        CUST_L
        CUST_M
        CUST_S
        CUST_SS

    End Enum

    '要望番号:349 yamanaka 2012.07.24 Start
    ''' <summary>
    ''' 明細スプレッド対象を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprDetailObject As Integer

        INIT = 0
        CUST_L
        CUST_M
        CUST_S
        CUST_SS

    End Enum

    ''' <summary>
    ''' 複写判定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum HukushaObject As Integer

        INIT = 0
        HUKUSHA_M
        HUKUSHA_S
        HUKUSHA_SS

    End Enum
    '要望番号:349 yamanaka 2012.07.24 End

    ''' <summary>
    ''' テーブルタイプ(車建 or トンキロ建)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum TableType As Integer

        SHA_DATE = 0
        TON_KIRO

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex

        SPR_CUST = 0
        CMB_BR

        TAB_CUST
        TPG_CUST_L
        TXT_CUST_CD_L
        TXT_CUST_NM_L
        TXT_MAIN_CUST
        LBL_MAIN_CUST_NM
        CMB_UNSO_TEHAI
        TXT_SAMPLE_KBN
        LBL_SAMPLE_NM
        CMB_PRODUCT_SEG_CD
        'ADD START 2018/11/14 要望番号001939
        OPT_COA_INKA_DATE_N
        OPT_COA_INKA_DATE_Y
        'ADD END   2018/11/14 要望番号001939
        '要望番号:349 yamanaka 2012.07.24 Start
        BTN_DETAIL_ADD_L
        BTN_DETAIL_DEL_L
        SPR_CUST_DETAIL_L
        '要望番号:349 yamanaka 2012.07.24 End
        BTN_EDIT_L

        TPG_CUST_M
        LBL_CUST_CD_M
        TXT_CUST_NM_M
        'START YANAI 要望番号824
        TXT_TANTO_CD
        TXT_TANTO_NM
        'END YANAI 要望番号824
        TXT_ZIP
        TXT_ADD1
        TXT_ADD2
        TXT_ADD3
        '要望番号:349 yamanaka 2012.07.24 Start
        CMB_ITEM_CURR_CD
        CMB_KAZEI
        CMB_SOKO
        CMB_HOSHO_MIN_KBN
        UNSO_HOKEN_AUTO_KBN      'ADD 2018/10/10 002400
        TXT_CUST_KYORI_CD
        TXT_CUST_KYORI_NM
        REMARK                  'ADD 2019/07/10 002520
        'CMB_HOSHO_MIN_KBN
        'CMB_KAZEI
        'TXT_CUST_KYORI_CD
        'TXT_CUST_KYORI_NM
        'CMB_SOKO
        '要望番号:349 yamanaka 2012.07.24 End
        PNL_NYUKA
        CMB_TARIF_KBN_NYUKA
        TXT_UNCHIN_TARIF_TON_NYUKA
        LBL_UNCHIN_TARIF_TON_NYUKA
        TXT_UNCHIN_TARIF_SHADATE_NYUKA
        LBL_UNCHIN_TARIF_SHADATE_NYUKA
        TXT_WARIMASHI_TARIF_NYUKA
        LBL_WARIMASHI_TARIF_NYUKA
        TXT_YOKOMOCHI_TARIF_NYUKA
        LBL_YOKOMOCHI_TARIF_NYUKA
        INIT_INKA_PLAN_DATE_KB                 'ADD 2018/10/30 002192
        PNL_SHUKKA
        CMB_TARIF_KBN_SHUKKA
        TXT_UNCHIN_TARIF_TON_SHUKKA
        LBL_UNCHIN_TARIF_TON_SHUKKA
        TXT_UNCHIN_TARIF_SHADATE_SHUKKA
        LBL_UNCHIN_TARIF_SHADATE_SHUKKA
        TXT_WARIMASHI_TARIF_SHUKKA
        LBL_WARIMASHI_TARIF_SHUKKA
        TXT_YOKOMOCHI_TARIF_SHUKKA
        LBL_YOKOMOCHI_TARIF_SHUKKA
        INIT_OUTKA_PLAN_DATE_KB                 'ADD 2018/10/30 002192
        TXT_SHITEI_UNSO_COMP
        TXT_SHITEI_UNSO_SHISHA
        LBL_SHITEI_UNSO
        INKA_ORIG_CD    'ADD 2018/10/25 要望番号001820
        NUM_HOKAN_FREE
        CMB_NYUKA_HOKOKU
        CMB_ZAIKO_HOKOKU
        CMB_SHUKKA_HOKOKU
        '要望番号:349 yamanaka 2012.07.24 Start
        BTN_DETAIL_ADD_M
        BTN_DETAIL_DEL_M
        SPR_CUST_DETAIL_M
        '要望番号:349 yamanaka 2012.07.24 End
        BTN_FUKUSHA_M
        BTN_EDIT_M

        TPG_CUST_S
        LBL_CUST_CD_S
        TXT_CUST_NM_S
        TXT_CUST_BETU_NM
        CMB_SHIME_KBN
        TXT_TCUST_BPCD
        LBL_TCUST_BPNM
        TXT_SEIQ_CD
        LBL_SEIQ_NM
        TXT_HOKAN_SEIQ_CD
        LBL_HOKAN_SEIQ_NM
        TXT_NIYAKU_SEIQ_CD
        LBL_NIYAKU_SEIQ_NM
        TXT_UNCHIN_SEIQ_CD
        LBL_UNCHIN_SEIQ_NM
        TXT_SAGYO_SEIQ_CD
        LBL_SAGYO_SEIQ_NM
        '要望番号:349 yamanaka 2012.07.24 Start
        CMB_UNCHIN_CALC
        CMB_PIKKING
        'CMB_PIKKING
        'CMB_UNCHIN_CALC
        '要望番号:349 yamanaka 2012.07.24 End
        BTN_ADD_ROW
        BTN_DEL_ROW
        SPR_CUST_PRT
        '要望番号:349 yamanaka 2012.07.24 Start
        BTN_DETAIL_ADD_S
        BTN_DETAIL_DEL_S
        SPR_CUST_DETAIL_S
        '要望番号:349 yamanaka 2012.07.24 End
        BTN_FUKUSHA_S
        BTN_EDIT_S

        TPG_CUST_SS
        LBL_CUST_CD_SS
        TXT_CUST_NM_SS
        TXT_MAIN_TANTO
        TXT_SUB_TANTO
        TXT_TEL
        TXT_FAX
        TXT_EMAIL
        CMB_SOKAE_SHORI
        IMD_LAST_CALK
        IMD_BEFORE_CALK
        LBL_LAST_JOB
        LBL_BEFORE_JOB
        CMB_CALK
        CMB_SEIQ_HAKUGAIHIN_KBN
        '要望番号:349 yamanaka 2012.07.24 Start
        BTN_DETAIL_ADD_SS
        BTN_DETAIL_DEL_SS
        SPR_CUST_DETAIL_SS
        '要望番号:349 yamanaka 2012.07.24 End
        BTN_FUKUSHA_SS
        BTN_EDIT_SS

    End Enum

    ''' <summary>
    ''' Spread(Cust)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprCustColumnIndex

        DEF = 0
        STATUS
        BR_CD
        BR_NM
        CUST_CD
        CUST_NM_L
        CUST_NM_M
        CUST_NM_S
        CUST_NM_SS
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        MAIN_CUST_CD
        MAIN_CUST_NM
        ZIP
        ADD1
        ADD2
        ADD3
        MAIN_TANTO
        SUB_TANTO
        TEL
        FAX
        EMAIL
        HOSHO_MIN_KBN
        UNSO_HOKEN_AUTO_KBN         'ADD 2018/10/10  002400
        INIT_INKA_PLAN_DATE_KB      'ADD 2018/10/31  002192
        INIT_OUTKA_PLAN_DATE_KB     'ADD 2018/10/31  002192
        SEIQ_CD
        SEIQ_NM
        HOKAN_SEIQ_CD
        HOKAN_SEIQ_NM
        NIYAKU_SEIQ_CD
        NIYAKU_SEIQ_NM
        UNCHIN_SEIQ_CD
        UNCHIN_SEIQ_NM
        SAGYO_SEIQ_CD
        SAGYO_SEIQ_NM
        NYUKA_HOKOKU
        SHUKKA_HOKOKU
        ZAIKO_HOKOKU
        UNSO_TEHAI
        SHITEI_UNSO_COMP
        SHITEI_UNSO_SHISHA
        SHITEI_UNSO_NM
        CUST_KYORI_CD
        CUST_KYORI_NM
        KAZEI
        HOKAN_FREE
        SAMPLE_KBN
        SAMPLE_NM
        PRODUCT_SEG_CD
        PRODUCT_SEG_NM_L
        PRODUCT_SEG_NM_M
        TCUST_BPCD
        TCUST_BPNM
        LAST_CALK
        BEFORE_CALK
        LAST_JOB
        BEFORE_JOB
        CALK_UMU
        UNCHIN_CALC
        CUST_BETU_NM
        SOKAE_SHORI
        SOKO
        PIKKING
        SEIQ_HAKUGAIHIN_KBN
        SHIME_KBN
        CREATE_DATE
        CREATE_USER
        UPDATE_DATE
        UPDATE_USER
        UPDATE_TIME
        SYS_DEL_FLG
        'START YANAI 要望番号824
        TANTO_CD
        TANTO_NM
        'END YANAI 要望番号824
        'START OU 要望番号2229
        ITEM_CURR_CD
        'END OU 要望番号2229
        INKA_ORIG_CD    'ADD 2018/10/25 要望番号001820
        INKA_ORIG_NM    'ADD 2018/10/25 要望番号001820
        COA_INKA_DATE_FLG   'ADD 2018/11/14 要望番号001939
        INTEG_WEB_FLG       'ADD 2018/12/28 依頼番号 : 003453   【LMS】IntegWeb利用状況を、荷主マスタに表示
        REMARK              'ADD 2019/07/10 002520
    End Enum

    ''' <summary>
    ''' Spread(CustPrt)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum SprCustPrtColumnIndex

        DEF = 0
        PTN_ID
        PTN_NM

    End Enum

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' Spread(CustDetail)部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum sprCustDetailColumnIndex

        DEF = 0
        EDA_NO
        YOTO_KBN
        SETTEI_VALUE
        SETTEI_VALUE2
        SETTEI_VALUE3
        BIKO
        CUST_CD
        CUST_CLASS
        UPD_FLG
        SYS_DEL_FLG
        MAX_EDA_NO
        ClmNm

    End Enum
    '要望番号:349 yamanaka 2012.07.10 End

End Class
