' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ080G : 距離呈マスタ照会
'  作  成  者       :  平山
' ==========================================================================

''' <summary>
''' LMZ080定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ080C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ080IN"
    Public Const TABLE_NM_OUT As String = "LMZ080OUT"

    ''' <summary>
    ''' 行インデックス
    ''' </summary>
    ''' <remarks></remarks>
    Public Enum SprColumnIndex

        DEF = 0
        KYORI_CD
        KYORI_REM
        ROW_INDEX

    End Enum
End Class
