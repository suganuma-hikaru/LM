' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  TODO_LMG520H : 請求鑑 (値引表示有)（テスト用）
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

Public Class LMG520H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

    Public Sub Main(ByVal prm As LMFormData)

        Dim ds As DataSet

        ds = prm.ParamDataSet

        Dim rtnDs As DataSet = MyBase.CallWSA("LMG520BLF", "Kensaku", ds)

    End Sub

End Class
