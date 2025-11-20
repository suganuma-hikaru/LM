' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI490C : ローム　棚卸対象商品リスト
'  作  成  者       :  kido
' ==========================================================================

''' <summary>
''' LMI490定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI490C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI490IN"
    Public Const TABLE_NM_IN_CUST As String = "LMI490IN_CUST"
    Public Const TABLE_NM_IN_EXCEL As String = "LMI490IN_EXCEL"
    Public Const TABLE_NM_OUT As String = "LMI490OUT"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SELECT_DATA As String = "SelectData"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        CMBEIGYO = 0
        BTN_MAKE

    End Enum

    ''' <summary>
    ''' イベント種別
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum EventShubetsu As Integer

        MAIN = 0
        TOJIRU
        SAKUSEI

    End Enum

    ''' <summary>
    ''' ガイダンス区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

End Class
