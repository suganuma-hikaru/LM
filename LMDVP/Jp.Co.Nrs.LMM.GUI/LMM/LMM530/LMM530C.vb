' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM530C : イエローカード管理マスタメンテ
'  作  成  者       :  hori
' ==========================================================================

''' <summary>
''' LMM530定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
'''
''' </histry>
Public Class LMM530C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMM530IN"
    Public Const TABLE_NM_OUT As String = "LMM530OUT"
    Public Const TABLE_NM_TIME As String = "LMM530TIME"

    '区分マスタ
    Public Const KBN_NM1 As String = "KBN_NM1"
    Public Const KBN_NM3 As String = "KBN_NM3"

    'ダイアログ設定
    Public Const ALL_FILE As Integer = 2
    Public Const CDRIVE As String = "C:\"
    Public Const DEFADRIVE As String = "C:\LM\YELLOWBACK"
    Public Const FILETYPE As String = "すべてのファイル(*.*)|*.*"
    Public Const DLGTITLE As String = "ファイルを選択してください"

    'メッセージ置換文字
    Public Const GOODSCDMSG As String = "商品コード"
    Public Const SHOBOCDMSG As String = "消防コード"

    'プログラムID
    Public Const PGID_LMB020 As String = "LMB020"       '入荷データ編集

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
        ADD
        CLEAR
        OPEN

    End Enum

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        DETAIL = 0
        CUSTCDL
        CUSTCDM
        GOODSCD
        LOTNO
        SHOBOCD
        BTNADD
        BTNCLEAR
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
        CUST_CD_L
        CUST_NM_L
        CUST_CD_M
        CUST_NM_M
        GOODS_CD_CUST
        GOODS_NM_1
        GOODS_CD_NRS
        LOT_NO
        SHOBO_CD
        SHOBO_NM
        ZAIKO
        SYS_ENT_DATE
        YCARD_LINK
        YCARD_NAME
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

End Class
