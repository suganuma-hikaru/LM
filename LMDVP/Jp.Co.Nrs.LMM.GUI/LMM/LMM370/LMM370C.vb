' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM370C : 庭先情報マスタメンテ
'  作  成  者       :  [wang]
' ==========================================================================

''' <summary>
''' LMM370定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM370C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM360IN"
    Public Const TABLE_NM_OUT As String = "LMM360OUT"

    '請求グループコード区分初期値
    Public Const GROUPKB As String = "01"

    'メッセージ置換文字
    Public Const SEIQTOMSG As String = "請求先コード"

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

    End Enum


    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        NRSBRCD
        SEIQTOCD
        PTNCD
        GROUPKB
        SEIQKMKCD
        KEISANTLGK
        NEBIKIRT
        NEBIKIGK
        TEKIYO
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
        SEIQTO_CD
        SEIQTO_NM
        PTN_CD
        GROUP_KB_NM
        GROUP_KB
        SEIQKMK_CD
        KEISAN_TLGK
        NEBIKI_RT
        NEBIKI_GK
        TEKIYO
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        SEIQKMK_NM

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex2

        KEISAI_DATE
        FILE_NAME
        FILE_PATH

    End Enum

End Class
