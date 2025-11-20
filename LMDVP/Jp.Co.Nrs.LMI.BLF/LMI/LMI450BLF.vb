' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI450  : 
'  作  成  者       :  [hojo]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI450BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI450BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI450BLC = New LMI450BLC()

#End Region

#Region "Method"


    ''' <summary>
    ''' EDIデータ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetEdiData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = MyBase.CallBLC(Me._Blc _
                                , LMI450BLC.FUNCTION_NAME.GetEdiData _
                                , ds)

            'エラーがあるかを判定
            If MyBase.IsMessageStoreExist() = True Then
                Return ds
            End If

            Return ds

        End Using

    End Function


#End Region

End Class