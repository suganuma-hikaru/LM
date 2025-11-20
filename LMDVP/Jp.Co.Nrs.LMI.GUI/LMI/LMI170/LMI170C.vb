' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI170C : 運賃請求印刷指示(ゴードー)
'  作  成  者       :  umano
' ==========================================================================

''' <summary>
''' LMI170定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI170C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI170IN"

    ''' <summary>
    ''' 印刷区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEQ_20_NOT950C As String = "01"
    Public Const SEQ_20_950C_101 As String = "02"
    Public Const SEQ_20_950C_NOT101_A0020 As String = "03"
    Public Const SEQ_20_950C_NOT101_A0021 As String = "04"
    Public Const SEQ_30 As String = "05"

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
    Public Const ACTION_ID_Close20 As String = "PrintClose20"
    Public Const ACTION_ID_CloseMatsu As String = "PrintCloseMatsu"

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
