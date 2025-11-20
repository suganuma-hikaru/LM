' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI470  : 日本合成　物流費送信
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI470BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI470BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI470BLC = New LMI470BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' データチェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのUpdDataメソッドに飛ぶ</remarks>
    Private Function Butsuryuhi_Rtn(ByVal ds As DataSet) As DataSet

        Dim InDt As DataTable = ds.Tables("LMI470IN")
        Dim InDr As DataRow = Nothing
        InDr = InDt.Rows(0)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '物流費チェック
            ds = MyBase.CallBLC(Me._Blc, "Butsuryuhi_Rtn", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            If ("2").Equals(ds.Tables("LMI470IN").Rows(0).Item("SYORI_PTN").ToString) = True Then
                '物流費作成
                ds = MyBase.CallBLC(Me._Blc, "MakeData", ds)

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

End Class