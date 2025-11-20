' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI420  : 請求明細・鑑作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI420BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI420BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI420DAC = New LMI420DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "ワークテーブル削除処理"
    ''' <summary>
    ''' ワークテーブル削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteRec(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(Me._Dac, "DeleteExcelRec", ds)

        Return ds

    End Function
#End Region

#Region "ワークＤＢ登録処理"
    ''' <summary>
    ''' ワークデータ登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertRec(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(Me._Dac, "InsertExcelRec", ds)

        Return ds

    End Function
#End Region

#Region "ワークデータ突合せ"
    ''' <summary>
    ''' ワークデータ突合せ処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateRec(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(Me._Dac, "UpdateExcelRec", ds)

        Return ds

    End Function
#End Region

End Class
