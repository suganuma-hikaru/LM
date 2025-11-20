' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM400C : 西濃マスタメンテ
'  作  成  者       :  阿達
' ==========================================================================

''' <summary>
''' LMM400定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM400C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM400IN"
    Public Const TABLE_NM_OUT As String = "LMM400OUT"

    Public Const JITASHAFLG As String = "01"
    Public Const UMUFLG As String = "00"

    Public Const SEINOMSG As String = "西濃着点番号"

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
        CUST_NM_L
        CUST_NM_M
        CUST_CD_L
        CUST_CD_M
        ZIP_NO
        KEN_K
        CITY_K
        SHIWAKE_CD
        CHAKU_CD
        CHAKU_NM
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG
        SEINOKEY

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
        SEINO_KEY
        CUST_NM_L
        CUST_NM_M
        CUST_CD_L
        CUST_CD_M
        ZIP_NO
        KEN_K
        CITY_K
        SHIWAKE_CD
        CHAKU_CD
        CHAKU_NM
        SYS_ENT_DATE
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

End Class
