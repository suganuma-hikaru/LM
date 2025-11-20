' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ390C : Rapidus次回分納情報取得
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMZ390定数定義クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ390C
    Inherits Jp.Co.Nrs.LM.Const.LMConst

    ' 分納 2回目以降の H_OUTKAEDI_DTL_RAPI.FILE_NAME に付与する文字列の前半・後半
    ' ( "n回目" 前後のカッコは [] が SQL Server の LIKE 演算子のワイルドカードだったため {} に変更する)
    Public Const TEMPLATE_PREFIX As String = "(分納自動生成データ{"
    Public Const TEMPLATE_SUFFIX As String = "回目})"

End Class
