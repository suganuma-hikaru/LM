' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI890  : NRC出荷／回収情報Excel作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI890BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI890BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI890BLC = New LMI890BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' Excel出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectCSVメソッドに飛ぶ</remarks>
    Private Function SelectExcel(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectExcel", ds)

    End Function

#End Region

End Class