' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM220C : 荷主別商品状態区分マスタメンテ
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMM220定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM220C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM220IN"
    Public Const TABLE_NM_OUT As String = "LMM220OUT"

    '荷主マスタ存在チェックデフォルトコード
    Public Const NINUSHI As String = "00"

    ''モード別画面ロック用
    'Public Const MODE_DEFAULT As String = "0"
    'Public Const MODE_REF As String = "1"
    'Public Const MODE_EDIT As String = "2"
    'Public Const MODE_LOCK As String = "9"

    ''' <summary>
    ''' Spread部列インデックス用列挙対
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        SYS_DEL_NM
        NRS_BR_CD
        NRS_BR_NM
        CUST_CD_L
        CUST_NM_L
        JOTAI_CD
        JOTAI_NM
        INFERIOR_GOODS_NM
        INFERIOR_GOODS_KB
        REMARK
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
        CUSTCDL
        CUSTNM
        JOTAICD
        JOTAINM
        INFERIORGOODSKB
        BIKO
        SITUATION
        CRTDATE
        CRTUSER
        UPDUSER
        UPDDATE
        UPDTIME
        SYSDELFLG

    End Enum


End Class
