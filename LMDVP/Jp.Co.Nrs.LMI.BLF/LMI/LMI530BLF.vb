' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI530BLF : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI530BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI530BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI530BLC = New LMI530BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 初期表示用データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "SelectInitData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのMakeDataメソッドに飛ぶ</remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        ' トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ' 作成処理
            ds = MyBase.CallBLC(Me._Blc, "UpdateData", ds)

            ' エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            ' トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

End Class
