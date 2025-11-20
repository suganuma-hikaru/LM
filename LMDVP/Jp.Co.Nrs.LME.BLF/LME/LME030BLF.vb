' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================

Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LME030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LME030BLC = New LME030BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectKensakuData(ByVal ds As DataSet) As DataSet

        '検索対象データの検索処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuData", ds)
        Return ds

    End Function

#End Region

End Class