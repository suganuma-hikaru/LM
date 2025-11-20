' ==========================================================================
'  システム名     : LM      : 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM020C : 分析票マスタメンテ
'  作  成  者     : hirayama
' ==========================================================================

''' <summary>
''' LMM020定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM020C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    'Public Const TABLE_NM_IN As String = "LMM020IN"
    Public Const TABLE_NM_OUT As String = "LMM020OUT"
    Public Const TABLE_NM_TIME As String = "LMM020TIME"

    '区分マスタ
    Public Const KBN_NM1 As String = "KBN_NM1"
    Public Const KBN_NM3 As String = "KBN_NM3"

    'ダイアログ設定
    Public Const ALL_FILE As Integer = 2
    Public Const CDRIVE As String = "C:\"
    Public Const DEFADRIVE As String = "C:\LM\COABACK"
    Public Const FILETYPE As String = "すべてのファイル(*.*)|*.*"
    Public Const DLGTITLE As String = "ファイルを選択してください"

    'メッセージ置換文字
    Public Const GOODSCDMSG As String = "商品コード"

    'プログラムID
    Public Const PGID_LMB020 As String = "LMB020"

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

        CHKREC = 0
        DETAIL
        CUSTCDL
        CUSTCDM
        GOODSCD
        LOTNO
        DESTCD
        'ADD START 2018/11/14 要望番号001939
        INKADATE
        INKADATEVERSFLG
        'ADD END   2018/11/14 要望番号001939
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
        GOODS_CD_CUST
        GOODS_NM_1
        GOODS_CD_NRS
        LOT_NO
        'ADD START 2018/11/14 要望番号001939
        INKA_DATE
        INKA_DATE_VERS_FLG
        'ADD END   2018/11/14 要望番号001939
        ZAIKO
        DEST_NM
        SYS_ENT_DATE
        COA_LINK
        COA_NAME
        CUST_CD_M
        CUST_NM_M
        DEST_CD
        SYS_ENT_USER_NM
        SYS_UPD_DATE
        SYS_UPD_USER_NM
        SYS_UPD_TIME
        SYS_DEL_FLG

    End Enum

End Class
