' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM230C : エリアマスタメンテ
'  作  成  者       :  hirayama
' ==========================================================================

''' <summary>
''' LMM230定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM230C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM230IN"
    Public Const TABLE_NM_OUT As String = "LMM230OUT"
    Public Const TABLE_NM_JIS As String = "LMM230JIS"

    'メッセージ置換文字
    Public Const AREACD As String = "エリアコード"
    Public Const JISCD As String = "JISコード"

    '更新フラグ
    Public Const UPDFLG_ON As String = "1"
    Public Const UPDFLG_OFF As String = "0"


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

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        JISCD = 0
        KEN
        SHI
        DETAIL
        PNL
        NRSBRCD
        AREACD
        AREANM
        BIN
        AREAINFO
        ROWADD
        ROWDEL
        DETAIL2
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

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
        AREA_CD
        AREA_NM
        BIN_KB
        BIN_KB_NM
        AREA_INFO
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

    ''' <summary>
    ''' Spread部(下部)列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        DEF = 0
        JIS_CD
        KEN
        SHI

    End Enum

End Class
