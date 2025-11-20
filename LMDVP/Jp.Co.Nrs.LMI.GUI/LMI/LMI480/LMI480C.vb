' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI480C : 運賃請求印刷指示(ディック)
'  作  成  者       :  umano
' ==========================================================================

''' <summary>
''' LMI480定数定義クラス
''' </summary>
''' <remarks></remarks>
Public Class LMI480C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "LMI480IN"
    Public Const TABLE_NM_OUT_0101 As String = "LMI480OUT_0101"          'DICG関係請求 神奈川配送分横持
    Public Const TABLE_NM_OUT_0102 As String = "LMI480OUT_0102"          'DICG関係請求 神奈川配送分横持(聖亘提出用)
    Public Const TABLE_NM_OUT_0103 As String = "LMI480OUT_0103"          'DICG関係請求 神奈川地区固定車
    Public Const TABLE_NM_OUT_0104 As String = "LMI480OUT_0104"          'DICG関係請求 栃木地区最低保証

    ''' <summary>
    ''' 抽出区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SEIKYU_DICG As String = "01"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SELECT_DATA As String = "SelectData"

    ''' <summary>
    ''' データ検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Public Const PRINT_DoPrint As String = "PRINT_DoPrint"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum CtlTabIndex

        SELECT_KB = 0
        KIKAN_YM
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

End Class
