' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : データ管理サブ
'  プログラムID     :  LMI500BLF : デュポン在庫報告
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI500BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI500BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI500BLC = New LMI500BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 日次在庫報告用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNitijiData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' <summary>
    ''' 在庫証明書作成データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' <summary>
    ''' SFTPデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSftpData(ByVal ds As DataSet) As DataSet
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#End Region

#End Region

End Class
