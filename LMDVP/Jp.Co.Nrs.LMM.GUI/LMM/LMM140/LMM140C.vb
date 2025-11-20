' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM140C : ZONEマスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM140定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM140C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM140IN"
    Public Const TABLE_NM_OUT As String = "LMM140OUT"
    Friend Const TABLE_NM_ZONE_SHOBO As String = "LMM140_ZONE_SHOBO"
    Friend Const TABLE_NM_TOU_SITU_ZONE_CHK As String = "LMM140_TOU_SITU_ZONE_CHK"

    'コンボボックス初期値
    Public Const TUBO As String = "01"
    Public Const JOUON As String = "01"
    Public Const TEION As String = "02"
    Public Const ONDOMIKANRI As String = "00"
    Public Const FUTUUKANRI As String = "01"
    Public Const NONE As String = "00"
    Public Const ARI As String = "01"

    'メッセージ置換文字
    Public Const TOU As String = "棟番号"
    Public Const SHITU As String = "室番号"
    Public Const ZONECD As String = "ZONEコード"

    '棟室ゾーンチェックマスタ 区分グループコード
    Public Const M_Z_KBN_DOKUGEKI As String = "G001"
    Public Const M_Z_KBN_KOUATHUGAS As String = "G012"
    Public Const M_Z_KBN_YAKUZIHO As String = "G201"

    '棟室ゾーンチェックマスタ 区分 無し区分値
    Public Const M_Z_KBN_DOKUGEKI_NONE As String = "01"
    Public Const M_Z_KBN_KOUATHUGAS_NONE As String = "00"
    Public Const M_Z_KBN_YAKUZIHO_NONE As String = "00"

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
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
        VALUECHANGED

        INS_T           '消防情報行追加
        DEL_T           '消防情報行削除

        INS_DOKU        '毒劇情報行追加
        DEL_DOKU        '毒劇情報行削除
        INS_KOUATHUGAS  '高圧ガス情報行追加
        DEL_KOUATHUGAS  '高圧ガス情報行削除
        INS_YAKUZIHO    '薬事法情報行追加
        DEL_YAKUZIHO    '薬事法情報行削除

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        SOKO
        TOUNO
        SITUNO
        TOUSITUNM
        ZONECD
        ZONENM
        ZONEKB
        HEATCTLKB
        NOWSETHEAT
        MAXHEATLIM
        MINHEATLOW
        HEATCTL
        BONDCTLKB
        PHARKB
        PFLKB
        GASCTLKB
        SEIQCD
        RENTMONTHLY
        SHOBO_JOHO
        BTN_ADD
        BTN_DEL
        SPR_ZONE_SHOBO
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
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        WH_CD
        WH_NM
        TOU_NO
        SITU_NO
        TOU_SITU_NM
        ZONE_CD
        ZONE_NM
        HOZEI_KB
        HOZEI_KB_NM
        ONDO_CTL_KB
        ONDO_CTL_KB_NM
        ONDO_CTL_FLG
        ONDO_CTL_FLG_NM
        ONDO
        YAKUJI_YN
        YAKUJI_YN_NM
        DOKU_YN
        DOKU_YN_NM
        GASS_YN
        GASS_YN_NM
        ZONE_KB
        MAX_ONDO_UP
        MINI_ONDO_DOWN
        SEIQTO_CD
        SEIQTO_NM
        TSUBO_AM
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

    ''' <summary>
    ''' Spread部(消防情報)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexShobo

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

    ''' <summary>
    ''' Spread部(毒劇情報)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndexDoku

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
    Public Enum SprColumnIndexKouathuGas

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
    Public Enum SprColumnIndexYakuziho

        DEF = 0
        YAKUZIHO_KB
        UPD_FLG
        SYS_DEL_FLG_T
        LAST

    End Enum

End Class
