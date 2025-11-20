' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI280C : 最低荷役保障料・差額明細書印刷指示(日産物流)
'  作  成  者       :  nakamura
' ==========================================================================

''' <summary>
''' LMI280定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI280C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI280IN"

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
    Public Const ACTION_ID_PrintData As String = "PrintData"

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
        cmbSearchDate
        DateFrom
        DateTo

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
