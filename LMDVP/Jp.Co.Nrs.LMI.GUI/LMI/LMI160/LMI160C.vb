' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI160C : 運賃請求印刷指示(ディック)
'  作  成  者       :  umano
' ==========================================================================

''' <summary>
''' LMI160定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI160C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN_BUKA As String = "LMI590IN"
    Public Const TABLE_NM_IN_HUTANKA As String = "LMI591IN"
    Public Const TABLE_NM_IN_NOUNYU As String = "LMI592IN"
    Public Const TABLE_NM_IN As String = "LMI160IN"

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const BUKA_UNCHIN_SEIKYU As String = "01"
    Public Const HUTANKA_UNCHIN_SEIKYU As String = "02"
    Public Const NOUNYU_UNCHIN_SEIKYU As String = "03"
    Public Const SYAATUKAI_UNCHIN_SEIKYU As String = "04"
    Public Const YAMATO_UNCHIN_SEIKYU As String = "05"

    ''' <summary>
    ''' 請求料金確定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MI_KAKUTEI As String = "00"
    Public Const KAKUTEI_ZUMI As String = "01"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const ACTION_ID_BUKA_UNCHIN_SEIKYU As String = "PrintBukaUnchin"
    Public Const ACTION_ID_HUTANKA_UNCHIN_SEIKYU As String = "PrintHutankaUnchin"
    Public Const ACTION_ID_NOUNYU_UNCHIN_SEIKYU As String = "PrintNounyuUnchin"

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
        optNotKakutei
        optKakuteiZumi
        optAll
        NonyuDateFrom
        NonyuDateTo

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
