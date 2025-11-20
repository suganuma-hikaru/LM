' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI880  : 篠崎運送月末在庫実績データ作成
'  作  成  者       :  [YANAI]
' ==========================================================================

''' <summary>
''' LMI880定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI880C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMI880IN"
    Public Const TABLE_NM_INOUT_CSV As String = "LMI880INOUT_CSV"

End Class
