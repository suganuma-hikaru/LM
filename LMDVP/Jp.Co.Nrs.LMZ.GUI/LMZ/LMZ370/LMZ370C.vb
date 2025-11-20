' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ370C : 温度管理アラートチェック
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ370定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ370C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ' データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ370IN"
    Public Const TABLE_NM_OUT As String = "LMZ370OUT"

End Class
