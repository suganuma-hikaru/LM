' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG080BLF : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMG080BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG080BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMG080BLC = New LMG080BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
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

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "予約取消"

    Private Function CancelUpDate(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '検索結果取得
            MyBase.CallBLC(Me._Blc, "CancelUpDate", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "排他処理"

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        '排他処理取得
        Return MyBase.CallBLC(Me._Blc, "CheckHaita", ds)

    End Function

#End Region

#Region "処理結果詳細"

    ''' <summary>
    ''' 処理結果詳細
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ProcessResults(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "ProcessResults", ds)

    End Function

#End Region

#Region "強制実行"

    ''' <summary>
    ''' 強制実行
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckExecute(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "CheckExecute", ds)

    End Function

#End Region

#Region "強制削除"

    ''' <summary>
    ''' 強制削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ForceDelete(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            MyBase.CallBLC(Me._Blc, "ForceDelete", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class