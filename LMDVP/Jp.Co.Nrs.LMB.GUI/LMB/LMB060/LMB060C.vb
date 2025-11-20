' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB060C : 入庫連絡票
'  作  成  者       :  hojo
' ==========================================================================

''' <summary>
''' LMB060定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB060IN"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    ''' <summary>
    ''' 印刷アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT As String = "PrintAction"

    ''' <summary>
    ''' 日付
    ''' </summary>
    ''' <remarks></remarks>
    Public Const DATE_YYYYMMDD As String = "yyyyMMdd"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        PRINT = 0
        NRS_BR_CD
        WH_CD
        CUST_CD_L
        CUST_NM_L
        CUST_CD_M
        CUST_NM_M
        INKA_DATE

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        MASTEROPEN
        TOJIRU
        PRINT
        ENTER

    End Enum

    Public Enum SysData As Integer
        YYYYMMDD = 0
        HHMMSSsss
    End Enum

End Class
