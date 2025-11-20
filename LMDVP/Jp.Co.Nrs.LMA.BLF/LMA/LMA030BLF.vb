' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMA       : メニュー
'  プログラムID     :  LMA030BLF : 荷主選択
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMA030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMA030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMA030BLC = New LMA030BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectComboData(ByVal ds As DataSet) As DataSet

        ''検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectComboData", ds)


    End Function

#End Region

#End Region

End Class
