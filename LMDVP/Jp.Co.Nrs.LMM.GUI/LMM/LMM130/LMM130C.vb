' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM130C : 棟室マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================

''' <summary>
''' LMM130定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM130C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM130IN"
    Public Const TABLE_NM_OUT As String = "LMM130OUT"
    Friend Const TABLE_NM_TOU_SITU_SHOBO As String = "LMM130_TOU_SITU_SHOBO"
    Friend Const TABLE_NM_TOU_SITU_EXP As String = "LMM130_TOU_SITU_EXP"
    Friend Const TABLE_NM_TOU_SITU_ZONE_CHK As String = "LMM130_TOU_SITU_ZONE_CHK"

    'コンボボックス初期値
    Public Const SOKO_KB As String = "11"

    'メッセージ置換文字
    Public Const TOU As String = "棟番号"
    Public Const SHITU As String = "室番号"

    '温度管理区分値(常温・定温)
    Public Const ONDO_J As String = "01"
    Public Const ONDO_T As String = "02"

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    '自社他社区分値
    Public Const JISYA As String = "01"
    Public Const TASYA As String = "02"
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    '棟室ゾーンチェックマスタ 区分グループコード
    Public Const M_Z_KBN_DOKUGEKI As String = "G001"
    Public Const M_Z_KBN_KOUATHUGAS As String = "G012"
    Public Const M_Z_KBN_YAKUZIHO As String = "G201"

    '区分マスタ 毒劇区分(G001) 区分値
    Public Const M_Z_KBN_DOKUGEKI_NASI As String = "01" '無し
    Public Const M_Z_KBN_DOKUGEKI_DOKU As String = "02" '毒物
    Public Const M_Z_KBN_DOKUGEKI_GEKI As String = "03" '劇物
    Public Const M_Z_KBN_DOKUGEKI_TOKU As String = "04" '特定毒物

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        INIT
        KENSAKU
        SANSHO
        SHINKI
        HENSHU
        HUKUSHA
        SAKUJO
        HOZON
        MASTEROPEN
        ENTER
        TOJIRU
        DCLICK
        INS_T           '行追加
        DEL_T           '行削除
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        MASTER
        INS_EXP_T       '行追加
        DEL_EXP_T       '行削除
        IKKATU_TOUROKU   '一括変更

        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
        INS_DOKU
        DEL_DOKU
        INS_KOUATHUGAS
        DEL_KOUATHUGAS
        INS_YAKUZIHO
        DEL_YAKUZIHO
        WARE_CHANGE
        JISYA_TASYA_CHANGE

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex
        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd start 
        APL_DATE_FROM = 0
        APL_DATE_TO
        IKKATU_TOUROKU
        CUST_CD
        TOU_SITU
        'TOU_SITU = 0
        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen upd end 
        TAB_TOU_SITU
        NRS_BR_CD
        WH_CD
        TOU_NO
        SITU_NO
        TOU_SITU_NM
        SOKO_KB
        HOZEI_KB
        CHOZO_MAX_QTY
        CHOZO_MAX_BAISU
        ONDO_CTL_KB
        MAX_ONDO_UP
        MINI_ONDO_DOWN
        ONDO
        ONDO_CTL_FLG
        MAX_WT
        CBM
        AREA
        HAN
        MX_PLT_QT
        RACK_YN
        SHOKA_KB
        JISYA_TASYA_KB
        FCT_MGR
        FCT_MGR_NM
        USER_CD
        USER_CD_SUB
        TASYA_WH_NM
        TASYA_ZIP
        TASYA_AD_1
        TASYA_AD_2
        TASYA_AD_3
        AREA_RENT_HOKAN_AMO
        DOKU_KB
        GAS_KANRI_KB
        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        EXP_JOHO
        BTN_EXP_ADD
        BTN_EXP_DLL
        SPR_TOU_SITU_EXP
        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 
        SHOBO_JOHO
        BTN_ADD
        BTN_DETAIL
        SPR_TOU_SITU_SHOBO
        DOKU_JOHO
        BTN_DOKU_ADD
        BTN_DOKU_DEL
        SPR_TOU_SITU_ZONE_CHK_DOKU
        KOUATHUGAS_JOHO
        BTN_KOUATHUGAS_ADD
        BTN_KOUATHUGAS_DEL
        SPR_TOU_SITU_ZONE_CHK_KOUATHUGAS
        YAKUZIHO_JOHO
        BTN_YAKUZIHO_ADD
        BTN_YAKUZIHO_DEL
        SPR_TOU_SITU_ZONE_CHK_YAKUZIHO

    End Enum

    ''' <summary>
    ''' Spread部(上部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        SOKO
        TOU_NO
        SITU_NO
        TOU_SITU_NM
        HOZEI_KB_NM
        ONDO_CTL_KB_NM
        ONDO_CTL_FLG_NM
        ONDO
        WH_CD
        SOKO_KB
        HOZEI_KB
        CHOZO_MAX_QTY
        CHOZO_MAX_BAISU
        ONDO_CTL_KB
        MAX_ONDO_UP
        MINI_ONDO_DOWN
        ONDO_CTL_FLG
        HAN
        CBM
        AREA
        MX_PLT_QT
        RACK_YN
        FCT_MGR
        FCT_MGR_NM
        SHOKA_KB
        '要望番号：674 yamanaka 2012.7.5 Start
        JISYATASYA_KB
        '要望番号：674 yamanaka 2012.7.5 End
        DOKU_KB
        GAS_KANRI_KB
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        ONDO_JITU
        MAX_WT
        USER_CD
        USER_NM
        USER_CD_SUB
        USER_NM_SUB
        WH_KB
        TASYA_WH_NM
        TASYA_ZIP
        TASYA_AD_1
        TASYA_AD_2
        TASYA_AD_3
        AREA_RENT_HOKAN_AMO
        ClmNm

    End Enum

    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        SHOBO_CD
        HINMEI
        KIKEN_TOKYU
        KIKEN_SYU
        BAISU
        WH_KYOKA_DATE
        UPD_FLG
        SYS_DEL_FLG_T

    End Enum

    '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex3

        DEF = 0
        APPLICATION_DATE_FROM
        APPLICATION_DATE_TO
        CUST_CD
        CUST_NM
        SERIAL_NO
        UPD_FLG
        SYS_DEL_FLG_T

    End Enum
    '2017/10/20 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' Spread部(毒劇情報)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex4

        DEF = 0
        DOKU_KB
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

    ''' <summary>
    ''' Spread部(高圧ガス情報)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex5

        DEF = 0
        KOUATHUGAS_KB
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

    ''' <summary>
    ''' Spread部(薬事法情報)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex6

        DEF = 0
        YAKUZIHO_KB
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

End Class
