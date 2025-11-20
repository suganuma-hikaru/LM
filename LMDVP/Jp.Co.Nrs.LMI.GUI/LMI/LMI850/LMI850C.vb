' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI850C : 日医工在庫照合データCSV作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI850定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI850C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI850IN"
    Public Const TABLE_NM_OUT_CSV As String = "LMI850OUT_CSV"

End Class
