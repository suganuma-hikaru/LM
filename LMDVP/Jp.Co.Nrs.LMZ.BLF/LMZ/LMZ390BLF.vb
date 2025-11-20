' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ390BLF : Rapidus次回分納情報取得
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ390BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ390BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ390BLC = New LMZ390BLC()

#End Region ' "Field"

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 次回分納情報取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectJikaiBunnouInfo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectJikaiBunnouInfo", ds)

    End Function

#End Region ' "検索処理"

#End Region ' "Method"

End Class
