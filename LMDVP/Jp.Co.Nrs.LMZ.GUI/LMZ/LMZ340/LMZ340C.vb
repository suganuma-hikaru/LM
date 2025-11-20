' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ340C : 入荷棟室ZONEチェック処理
'  作  成  者       :  asatsuma
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ340定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ340C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ340IN"
    Public Const TABLE_NM_OUT_CHECK_FLG As String = "LMZ340OUT_CHECK_FLG"
    Public Const TABLE_NM_OUT_CHECK_CAPA As String = "LMZ340OUT_CHECK_CAPA"
    Public Const TABLE_NM_OUT_CALC_QTY As String = "LMZ340OUT_CALC_QTY"
    Public Const TABLE_NM_OUT_CHECK_ATTR As String = "LMZ340OUT_CHECK_ATTR"

End Class
