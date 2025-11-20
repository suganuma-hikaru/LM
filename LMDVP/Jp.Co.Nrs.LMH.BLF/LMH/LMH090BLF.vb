' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブシステム
'  プログラムID     :  LMH090BLF : 現品票印刷
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH090BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH090BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH090BLC = New LMH090BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 印刷対象データ検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '対象データ取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds.Merge(New RdPrevInfoDS)

            '現品票印刷処理（LMH503の呼び出し）
            ds = MyBase.CallBLC(New LMH503BLC(), "DoPrint", ds)

            If MyBase.IsMessageExist = False AndAlso MyBase.IsMessageStoreExist = False Then
                'エラーが無ければCommit
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region

End Class
