' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM360C : 請求テンプレートマスタメンテ
'  作  成  者       :  [kisi]
' ==========================================================================

''' <summary>
''' LMM360定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM360C
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
        TCUST_BPCD
        TCUST_BPNM
        GROUPKB
        SEIQKMKCD
        SEIQKMKCD_S
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
        SEIQKMK_CD_S
        KEISAN_TLGK
        NEBIKI_RT
        NEBIKI_GK
        TEKIYO
        TCUST_BPCD
        TCUST_BPNM
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG
        SEIQKMK_NM

    End Enum

End Class
