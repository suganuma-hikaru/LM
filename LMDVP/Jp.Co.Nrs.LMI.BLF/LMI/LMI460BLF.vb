' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI460  : ローム　請求先コード変更
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI460BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI460BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI460BLC = New LMI460BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのUpdDataメソッドに飛ぶ</remarks>
    Private Function UpdDataUnchin(ByVal ds As DataSet) As DataSet

        Dim InDt As DataTable = ds.Tables("LMI460IN")
        Dim InDr As DataRow = Nothing
        InDr = InDt.Rows(0)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '作成処理
            ds = MyBase.CallBLC(Me._Blc, "UpdDataUnchin", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function


#End Region

End Class