' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK     : 支払サブシステム
'  プログラムID     :  LMK060C : 支払印刷指示
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMK060定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMK060C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ' ''' <summary>
    ' ''' データセットテーブル名
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Public Const TABLE_NM_IN As String = "LMK060IN"
    Public Const TABLE_NM_LMF600IN As String = "LMF600IN"
    Public Const TABLE_NM_LMF610IN As String = "LMF610IN"

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_SHIHARAI_MEISAI As String = "01"
    Public Const PRINT_SHIHARAI_CHECK As String = "02"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT_MEISAI As String = "PrintMeisai"
    Public Const ACTION_ID_PRINT_CHECK As String = "PrintCheck"

    Public Const PGID_LMF080 As String = "LMF080"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        Print = 0
        Br
        CustCdL
        CustNmL
        CustCdM
        CustNmM
        ShiharaiCd
        ShiharaiNm
        OutkaDateFrom
        OutkaDateTo

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
