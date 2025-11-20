' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM190F : 距離マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM190定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM190C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM190IN"
    Public Const TABLE_NM_OUT As String = "LMM190OUT"

    'メッセージ用
    Public Const K_CD As String = "距離程コード"
    Public Const O_JIS_CD As String = "発地JISコード"
    Public Const D_JIS_CD As String = "届先JISコード"

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        KYORI_CD
        ORIG_JIS_CD
        ORIG_KEN
        ORIG_SHI
        DEST_JIS_CD
        DEST_KEN
        DEST_SHI
        KYORI
        KYORI_REM
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

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
        KYORICD
        ORIGJISCD
        ORIGKEN
        ORIGSHI
        DESTJISCD
        DESTKEN
        DESTSHI
        KYORI
        KYORIREM
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum

    ''' <summary>
    ''' マスタ参照用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum MasterRow

        JISCD
        KEN
        SHI


    End Enum

End Class
