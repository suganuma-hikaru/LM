' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送サブシステム
'  プログラムID     :  LMF100C : 帳票印刷指示
'  作  成  者       :  篠原
' ==========================================================================

''' <summary>
''' LMF100定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMF100C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_KEN_BETSU As String = "LMF650IN"
    Public Const TABLE_NM_OUT_KEN_BETSU As String = "LMF650OUT"
    Public Const TABLE_NM_LMF100_KEN As String = "LMF100KEN"
    Public Const TABLE_NM_LMF100_KEN_IN As String = "LMF100KEN_IN"

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_KEN As String = "01"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_PRINT_KEN As String = "PrintKen"

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
        CmbKen
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

    ''' <summary>
    ''' キャッシュテーブル検索時使用
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum ConditionPattern As Integer

        equal
        pre
        all

    End Enum

End Class
