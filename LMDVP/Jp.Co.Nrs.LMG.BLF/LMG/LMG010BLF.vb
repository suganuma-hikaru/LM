' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG010BLF : 保管料/荷役料計算
'  作  成  者       :  [笈川]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMG010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMG010BLC = New LMG010BLC()

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

    ''' <summary>
    ''' 単価未承認チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectChkApprovalTanka(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectChkApprovalTanka", ds)

    End Function

    ''' <summary>
    ''' 変動保管料チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectChkVar(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectChkVar", ds)

    End Function

#End Region

#Region "前回計算取消処理"

    ''' <summary>
    ''' 前回計算取消処理（更新・削除）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub UpDateDel(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新・削除処理
            ds = MyBase.CallBLC(Me._Blc, "UpDateDel", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

    End Sub


#End Region

#Region "実行"

    ''' <summary>
    ''' 実行処理（登録）
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function InsertWorkHead(ByVal ds As DataSet) As DataSet


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新・削除処理
            ds = MyBase.CallBLC(Me._Blc, "InsertWorkHead", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function


    ''' <summary>
    ''' バッチ呼出処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub CalcBatch(ByVal ds As DataSet)

        MyBase.CallBLC(Me._Blc, "CalcBatch", ds)

    End Sub

#End Region

#Region "排他"

    ''' <summary>
    ''' 荷主排他
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaMCust(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaMCust", ds)
        Return ds
    End Function

#End Region

#End Region

End Class