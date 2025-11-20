'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ070C : JISマスタ検索
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ070定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ070C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ070IN"
    Public Const TABLE_NM_OUT As String = "LMZ070OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        KEN
        SHI
        JIS_CD
        ROW_INDEX

    End Enum

End Class
