' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD040C : 在庫履歴照会
'  作  成  者       :  
' ==========================================================================

''' <summary>
''' LMD040定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD040C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMD040IN"
    Public Const TABLE_NM_OUT As String = "LMD040OUT"
    Public Const TABLE_NM_GENZAIKO As String = "GENZAIKO"
    Public Const TABLE_NM_RIREKI As String = "LMD040_RIREKI"
    Public Const TABLE_NM_RIREKI_ZAI As String = "LMD040_RIREKIZAI"
    Public Const TABLE_NM_OKIBA_PRINT As String = "LMD500IN"
    Public Const TABLE_NM_TANAOROSI_PRINT As String = "LMD510IN"
    Public Const TABLE_NM_TANAOROSI_SYANAI_PRINT As String = "LMD515IN"
    Public Const TABLE_NM_TANAOROSI_CFS_PRINT As String = "LMD519IN"    '#(2012.07.24)コンソリ業務対応
    Public Const TABLE_NM_GOODS_PRINT As String = "LMD520IN"
    Public Const TABLE_NM_RIREKI_LOT_PRINT As String = "LMD540IN"
    Public Const TABLE_NM_RIREKI_GOODS_PRINT As String = "LMD541IN"
    Public Const TABLE_NM_YOTEI_TANAOROSI_PRINT As String = "LMD610IN"  '#(2012.11.07)千葉対応
    Public Const TABLE_NM_SHOBO_ZAI_PRINT As String = "LMD640IN"  '2015.03.24 消防類別・在庫一覧表

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    '選択タブ区分(業務処理用)
    Public Const TAB_SONOTA As String = "1"
    Public Const TAB_INKA As String = "2"
    Public Const TAB_ZAIK As String = "3"
    Public Const TAB_HOKA As String = "4"

    '選択タブ区分(帳票用)
    Public Const TAB_FLG_INKA As String = "1"
    Public Const TAB_FLG_ZAIK As String = "2"

    '数区分(帳票用)
    Public Const KAZU_KOSU As String = "0"
    Public Const KAZU_SURYO As String = "1"

    '帳票ID(帳票用)
    Public Const PTN_ID_LOT As String = "25"
    Public Const PTN_ID_GOODS As String = "26"

    '帳票種別
    Public Const PRINT_ZAIKO_RIREKI_LOT As String = "01"            '在庫履歴帳票（LOT別）
    Public Const PRINT_ZAIKO_RIREKI_GOODS As String = "02"          '在庫履歴帳票（商品別）
    Public Const PRINT_OKIBA_ZAIKOICHIRAN As String = "03"          '置場別・在庫一覧表
    Public Const PRINT_GOODS_ZAIKOICHIRAN As String = "04"          '商品別・在庫一覧表
    Public Const PRINT_TANAOROSI_ICHIRAN As String = "05"           '棚卸し一覧表
    Public Const PRINT_TANAOROSI_ICHIRAN_SYANAI As String = "06"    '棚卸し一覧表(社内)
    Public Const PRINT_TANAOROSI_ICHIRAN_CFS As String = "07"       '棚卸し一覧表(CFS業務用)    '#(2012.07.24) コンソリ業務対応
    Public Const PRINT_YOTEI_TANAOROSI_ICHIRAN As String = "08"       '予定棚卸し一覧表           '#(2012.11.06) 千葉対応
    Public Const PRINT_SHOBO_ZAIKOICHIRAN As String = "09"          '消防類別・在庫一覧表       '2015.03.24

    'ADD START 2019/8/27 依頼番号:007116,007119
    '実行種別
    Public Const EXECUTION_EMPTY_RACK_REF As String = "01"  '空棚参照
    Public Const EXECUTION_ZAIKO_DIFF_LIST As String = "02" '在庫差異リスト
    Public Const EXECUTION_EMPTY_RACK_LIST As String = "03" '空棚リスト

    '在庫差異リストファイル
    Public Const ZAIKO_DIFF_PATH As String = "C:\LMUSER\在庫差異リスト\"   '出力パス
    Public Const ZAIKO_DIFF_NAME As String = "在庫差異YYYYMMDDhhmmss.xlsx" 'ファイル名

    '空棚リストファイル
    Public Const EMPTY_RACK_PATH As String = "C:\LMUSER\空棚リスト\"   '出力パス
    Public Const EMPTY_RACK_NAME As String = "空棚YYYYMMDDhhmmss.xlsx" 'ファイル名
    'ADD END 2019/8/27 依頼番号:007116,007119

    '検索種別
    Public Enum KensakuTp

        KENSAKU_GOODS = 0
        KENSAKU_GOODS_LOT
        KENSAKU_GOODS_OKIBA
        KENSAKU_OKIBA
        KENSAKU_DETAIL

    End Enum

    '画面ID
    Public Const PGID_LMC020 As String = "LMC020"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DATAIL = 0
        EIGYO
        SOKO
        PRINT
        PRINT_BTN
        NYUKAFROM
        NYUKATO
        ZEROZAIKO
        CUST_CD_L
        CUST_CD_M
        CUST_CD_S
        CUST_CD_SS
        YOTEI
        JIKKOU
        ALL
        HYOUJIFROM
        HYOUJITO
        SYOUSAI
        SYUKKATORIKESHI
        KOSU
        KOSUZAIKO

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu

        DATAIL = 0
        HENSHU
        KENSAKU
        MASTER
        SHOKININUSHI
        TOJIRU
        PRINT
        ENTER
        EXECUTION 'ADD 2019/8/27 依頼番号:007116,007119

    End Enum

    ''' <summary>
    '''  在庫スプレッドインデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        YOJITU
        OKIBA
        GOODS_CD_CUST
        GOODS_NM
        CUST_CATEGORY_1
        INKO_DATE
        LOT_NO
        IRIME
        NB_UT
        ZAI_QT
        ZAI_QT_ANOTHER  '残数表記（梱数　端数/入数）
        ZAI_UT
        ZAN_SUURYOU
        ALCTD_NB
        ALCTD_NB_ANOTHER  '引当中表記（梱数　端数/入数）
        HIKIATE_SURYOU
        JITU_QT
        JITU_UT
        REMARK
        REMARK_OUT
        LT_DATE
        SERIAL_NO
        GOODS_COND_NM_1
        GOODS_COND_NM_2
        GOODS_COND_NM_3
        IRISU
        UT
        OFB_KB
        OFB_NM
        SPD_KB
        SPD_NM
        CUST_KANJYO_CD_1
        CUST_KANJYO_CD_2
        CUST_CATEGORY_2
        CUST_NM
        CUST_CD
        INKA_NO
        INKA_NO_L
        INKA_NO_M
        INKA_NO_S
        GOODS_CD_NRS
        ZAI_REC_NO
        WARIATE
        WARIATE_NM
        DEST_CD_NM
        SYOUBOU_CD
        SYOUBOU_NM
        ZEI_KB
        ZEI_KB_NM
        DOKUGEKI
        DOKUGEKI_NM
        ONDO
        ONDO_NM
        INKA_IRIME
        NRS_BR_CD
        NRS_BR_NM
        NRS_CR_NM
        NRS_CR_CD
        CD_NRS_TO
        CUST_CD_L
        CUST_CD_M
        LAST

    End Enum

    ''' <summary>
    ''' 入出荷（入荷ごと）インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum sprNyukaNIndex

        DEF = 0
        YOJITU_N
        SYUBETU_N
        GOODS_NM_N
        PLAN_DATE_N
        LOT_NO_N
        IRIME_N
        INKA_KOSU_N
        OUTKA_KOSU_N
        ZAN_KOSU_N
        NB_UT_N
        INKA_SURYO_N
        OUTKA_SURYO_N
        ZAN_SURYO_N
        STD_IRIME_UT_N
        OKIBA_N
        KANRI_NO_N
        ZAI_REC_NO_N
        ZAI_TRS_UPD_USER_NM    ' 1888_WIT_ロケーション変更強化対応 20160822
        DEST_NM_N
        ORD_NO_N
        BUYER_ORD_NO_N
        UNSOCO_NM_N
        REMARK_N
        REMARK_OUT_N
        GOODS_COND_NM_1_N
        GOODS_COND_NM_2_N
        GOODS_COND_NM_3_N
        OFB_KB_NM_N
        SPD_KB_NM_N
        ALLOC_PRIORITY_NM_N
        DEST_CD_NM_N
        RSV_NO_N
        KANRI_NO_L
        KANRI_NO_M
        KANRI_NO_S
        LAST_N

    End Enum

    ''' <summary>
    ''' 入出荷（在庫ごと）インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum sprNyukaZIndex

        DEF = 0
        YOJITU_Z
        SYUBETU_Z
        GOODS_NM_Z
        PLAN_DATE_Z
        LOT_NO_Z
        IRIME_Z
        INKA_KOSU_Z
        OUTKA_KOSU_Z
        ZAN_KOSU_Z
        NB_UT_Z
        INKA_SURYO_Z
        OUTKA_SURYO_Z
        ZAN_SURYO_Z
        STD_IRIME_UT_Z
        OKIBA_Z
        KANRI_NO_Z
        ZAI_REC_NO_Z
        ZAI_TRS_UPD_USER_NM    ' 1888_WIT_ロケーション変更強化対応 20160822
        DEST_NM_Z
        ORD_NO_Z
        BUYER_ORD_NO_Z
        UNSOCO_NM_Z
        REMARK_Z
        REMARK_OUT_Z
        GOODS_COND_NM_1_Z
        GOODS_COND_NM_2_Z
        GOODS_COND_NM_3_Z
        SPD_KB_NM_Z
        OFB_KB_NM_Z
        ALLOC_PRIORITY_NM_Z
        DEST_CD_NM_Z
        RSV_NO_Z
        INKA_NO_L_Z
        INKA_NO_M_Z
        INKA_NO_S_Z
        LAST_Z

    End Enum


End Class
