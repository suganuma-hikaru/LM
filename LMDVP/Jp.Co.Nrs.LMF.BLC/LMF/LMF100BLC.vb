' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 請求
'  プログラムID     :  LMF100BLF : 請求印刷指示
'  作  成  者       :  篠原
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMF100BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF100BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF100DAC = New LMF100DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 都道府県名データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ComboData", ds)


    End Function

#End Region


#End Region

End Class
