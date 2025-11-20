' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI350C : 保管荷役明細(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================

''' <summary>
''' LMI350定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI350C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI680IN"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        Print = 0
        CmbBr
        CustCdL
        CustNmL
        CustCdM
        CustNmM
        CustCdS
        CustNmS
        SeiqCd
        SeiqNm
        cmbSearchDate
        SeiqDate

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
