' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM540C : 棟マスタメンテナンス
'  作  成  者       :  [narita]
' ==========================================================================

''' <summary>
''' LMM540定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM540C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM540IN"
    Public Const TABLE_NM_OUT As String = "LMM540OUT"
    Friend Const TABLE_NM_TOU_SHOBO As String = "LMM540_TOU_SHOBO"
    Friend Const TABLE_NM_TOU_CHK As String = "LMM540_TOU_CHK"
    Friend Const TABLE_NM_IN_HAIKA_CHECK As String = "LMM540IN_HAIKA_CHECK"
    Friend Const TABLE_NM_HAIKA_TOU_SITU As String = "LMM540_HAIKA_TOU_SITU"
    Friend Const TABLE_NM_HAIKA_ZONE As String = "LMM540_HAIKA_ZONE"
    Friend Const TABLE_NM_HAIKA_TOU_SITU_ZONE_CHK As String = "LMM540_HAIKA_TOU_SITU_ZONE_CHK"
    Friend Const TABLE_NM_TOU_SITU_SHOBO As String = "LMM540_TOU_SITU_SHOBO"
    Friend Const TABLE_NM_ZONE_SHOBO As String = "LMM540_ZONE_SHOBO"
    Friend Const TABLE_NM_TOU_SITU_CHK As String = "LMM540_TOU_SITU_CHK"
    Friend Const TABLE_NM_ZONE_CHK As String = "LMM540_ZONE_CHK"
    Friend Const TABLE_NM_SUM As String = "LMM540_SUM"

    'コンボボックス初期値
    Public Const SOKO_KB As String = "11"
    Public Const HOZEI_KB As String = "11"

    'メッセージ置換文字
    Public Const TOU As String = "棟番号"

    '自社他社区分値
    Public Const JISYA As String = "01"
    Public Const TASYA As String = "02"

    '棟チェックマスタ 区分グループコード
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
        MASTER
        INS_EXP_T       '行追加
        DEL_EXP_T       '行削除
        IKKATU_TOUROKU   '一括変更
        INS_DOKU
        DEL_DOKU
        INS_KOUATHUGAS
        DEL_KOUATHUGAS
        INS_YAKUZIHO    'YAKKIHO
        DEL_YAKUZIHO    'YAKKIHO

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Enum CtlTabIndex
        TOU = 0
        NRS_BR_CD
        WH_CD
        TOU_NO
        TOU_NM
        SOKO_KB
        HOZEI_KB
        CHOZO_MAX_QTY
        CHOZO_MAX_BAISU
        FCT_MGR
        FCT_MGR_NM
        ONDO_CTL_KB
        AREA
        JISYATASYA_KB
        EXP_JOHO
        BTN_EXP_ADD
        BTN_EXP_DLL
        SPR_TOU_EXP
        SHOBO_JOHO
        BTN_ADD
        BTN_DETAIL
        SHOBO_HAIKA_CHK
        SPR_TOU_SHOBO
        DOKU_JOHO
        BTN_DOKU_ADD
        BTN_DOKU_DEL
        DOKU_HAIKA_CHK
        SPR_TOU_CHK_DOKU
        KOUATHUGAS_JOHO
        BTN_KOUATHUGAS_ADD
        BTN_KOUATHUGAS_DEL
        KOUATHUGAS_HAIKA_CHK
        SPR_TOU_CHK_KOUATHUGAS
        YAKUZIHO_JOHO       'YAKKIHO
        BTN_YAKUZIHO_ADD    'YAKKIHO
        BTN_YAKUZIHO_DEL    'YAKKIHO
        YAKKIHO_HAIKA_CHK
        SPR_TOU_CHK_YAKUZIHO  'YAKKIHO

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
        TOU_NM
        SOKO_KB
        SOKO_KB_NM
        HOZEI_KB
        HOZEI_KB_NM
        HOKAN_KANO_M3
        HOKAN_KANO_KG
        ONDO_CTL_KB_NM
        WH_CD
        CHOZO_MAX_QTY
        CHOZO_MAX_BAISU
        ONDO_CTL_KB
        AREA
        FCT_MGR
        FCT_MGR_NM
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        ONDO_JITU
        MAX_WT
        ClmNm

    End Enum

    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        SHOBO_CD
        BAISU
        WH_KYOKA_DATE
        HINMEI
        KIKEN_TOKYU
        KIKEN_SYU
        UPD_FLG
        SYS_DEL_FLG_T

    End Enum

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
        UPD_FLG
        SYS_DEL_FLG_T

    End Enum

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
    ''' Spread部(薬機法情報)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex6

        DEF = 0
        YAKUZIHO_KB    'YAKKIHO
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

End Class
