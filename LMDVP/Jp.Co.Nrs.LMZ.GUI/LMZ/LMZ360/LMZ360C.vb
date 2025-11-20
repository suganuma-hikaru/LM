' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ360C : Tra-Net連携共通処理
'  作  成  者       :  kumakura
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ360定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ360C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    'データセットテーブル名
    Public Const TABLE_NM_IN As String = "LMZ360IN"
    Public Const TABLE_NM_ABHB910_UNSO_L As String = "ABHB910IN_UNSO_L"

End Class
