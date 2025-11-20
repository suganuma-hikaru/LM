' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM210C : 乗務員マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM210定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM210C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM210IN"
    Public Const TABLE_NM_OUT As String = "LMM210OUT"

    'コンボボックスの初期値
    Public Const Combo As String = "00"

    'メッセージ置換文字
    Public Const CREWCDMSG As String = "乗務員コード"

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

    End Enum

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        YUSO_BR_CD
        YUSO_BR_NM
        DRIVER_CD
        DRIVER_NM
        AVAL_YN
        LCAR_LICENSE_YN
        LCAR_LICENSE_YN_NM
        TRAILER_LICENSE_YN
        TRAILER_LICENSE_YN_NM
        OTSU1_YN
        OTSU1_YN_NM
        OTSU2_YN
        OTSU2_YN_NM
        OTSU3_YN
        OTSU3_YN_NM
        OTSU4_YN
        OTSU4_YN_NM
        OTSU5_YN
        OTSU5_YN_NM
        OTSU6_YN
        OTSU6_YN_NM
        HICOMPGAS_YN
        HICOMPGAS_YN_NM
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_TIME
        SYS_UPD_USER_NM
        SYS_DEL_FLG
        
    End Enum

    Public Enum CtlTabIndex

        DETAIL = 0
        SYSDELNM
        YUSOBRCD
        YUSOBRNM
        DRIVECD
        DRIVENM
        AVALYN
        LCARLICENSEYN
        LCARLICENSEYNNM
        TRAILERLICENSEYN
        TRAILERLICENSEYNNM
        OTSU1YN
        OTSU1YNNM
        OTSU2YN
        OTSU2YNNM
        OTSU3YN
        OTSU3YNNM
        OTSU4YN
        OTSU4YNNM
        OTSU5YN
        OTSU5YNNM
        OTSU6YN
        OTSU6YNNM
        HICOMPGASYN
        HICOMPGASYNNM
        SYSENTDATE
        SYSENTUSERNM
        SYSUPDDATE
        SYSUPDDATEUSERNM
        SYSUPDTIME
        SYSUPDUSERNM
        SYSDELFLG

    End Enum
End Class
