' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI210  : ハネウェル管理
'  作  成  者       :  [KIM]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI210BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI210BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI210BLC = New LMI210BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索データ取得処理（出荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuOutkaData(ByVal ds As DataSet) As DataSet

        '検索データ取得処理（出荷）
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuOutkaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 検索データ取得処理（返却）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuInkaData(ByVal ds As DataSet) As DataSet

        '検索データ取得処理（返却）
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuInkaData", ds)

        Return ds

    End Function

#End Region

End Class