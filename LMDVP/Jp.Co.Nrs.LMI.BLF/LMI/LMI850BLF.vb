' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI850  : 日医工在庫照合データCSV作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI850BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI850BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI850BLC = New LMI850BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectCSVメソッドに飛ぶ</remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCSV", ds)

    End Function

#End Region

End Class