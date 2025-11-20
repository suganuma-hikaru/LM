' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 入荷管理
'  プログラムID     :  LMB060BLF : 入庫連絡票
'  作  成  者       :  hojo
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB060BLC = New LMB060BLC()

#End Region

#Region "Method"

#Region "メイン処理"
    ''' <summary>
    ''' 新規入荷L作成・印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal ds As DataSet) As DataSet

        ds = Me.ScopeStartEnd(ds, "InsertAction")

        '印刷処理
        Call Me.DoPrint(ds)

        Return ds

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理実行
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim prtBlc As Com.Base.BaseBLC

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        '入庫連絡票
        Dim lmb550ds As DataSet = Me.BlcAccess(ds, "SetDataSetLMB550InData")
        prtBlc = New LMB550BLC()
        lmb550ds.Merge(New RdPrevInfoDS)
        rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", lmb550ds)
        rdPrevDt.Merge(rtnDs.Tables(LMConst.RD))

        ds.Tables(LMConst.RD).Merge(rdPrevDt)

        Return ds

    End Function


#End Region '印刷処理

#Region "ユーティリティ"
    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet _
                                   , ByVal actionId As String _
                                   ) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新処理
            rtnResult = Me.SetItemData(ds, actionId)
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetItemData(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        '更新処理
        ds = Me.BlcAccess(ds, actionId)

        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()

        Return rtnResult

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#End Region 'ユーティリティ

#End Region

End Class
