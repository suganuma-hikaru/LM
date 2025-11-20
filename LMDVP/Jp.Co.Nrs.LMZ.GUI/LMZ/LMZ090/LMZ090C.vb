' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ090C : エリアマスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ090定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ090C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ090IN"
    Public Const TABLE_NM_OUT As String = "LMZ090OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        AREA_CD
        BIN_KB_NM
        BIN_KB
        AREA_NM
        AREA_INFO
        ROW_INDEX

    End Enum

End Class
