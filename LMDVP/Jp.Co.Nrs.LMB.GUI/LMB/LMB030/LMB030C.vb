' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB030C : 入荷報告書
'  作  成  者       :  kishi
' ==========================================================================

''' <summary>
''' LMB030定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB030C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMB030IN"
    Public Const TABLE_NM_PRT_IN As String = "LMB520IN"

    'モード別画面ロック用
    Public Const MODE_DEFAULT As String = "0"
    Public Const MODE_REF As String = "1"
    Public Const MODE_EDIT As String = "2"
    Public Const MODE_LOCK As String = "9"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_UPDATE As String = "UpdateData"

    ''' <summary>
    ''' 印刷アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT As String = "PrintAction"

    ''' <summary>
    ''' 印刷アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_UPDATE_INKA As String = "UpdateAction"


    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GETUJI As String = "01"
    Public Const NITIJI As String = "02"

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

        Print = 0
        Eigyo
        CustCDL
        CustNML
        CustCDM
        CustNMM
        NyukaDate
        Furikae
        DataInsDate

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

End Class
