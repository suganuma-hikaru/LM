' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 入荷管理
'  プログラムID     :  LMB030BLC : 入荷印刷指示
'  作  成  者       :  菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMB030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB030DAC = New LMB030DAC()



#End Region

#Region "Method"

#Region "更新"

    ''' <summary>
    ''' 入荷L更新(印刷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaLPrintData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateInkaLPrintData", ds)

    End Function

    ''' <summary>
    ''' 入荷L更新(印刷)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaState(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateInkaState", ds)

    End Function


#End Region

#End Region

End Class
