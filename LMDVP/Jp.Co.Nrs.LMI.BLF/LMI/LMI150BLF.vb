' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI150  : 請求データ編集 [デュポン用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI150BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI150BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI150BLC = New LMI150BLC()

    Private Const LMG550PRT_KAGAMI As String = "01"
    Private Const LMG550PRT_SHUKEI As String = "02"
    Private Const LMG550PRT_SHUKEIKEIRI As String = "03"
#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectData", ds)

    End Function

    ''' <summary>
    ''' NRS処理番号の最大値を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNrsProcNo(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectNrsProcNo", ds)

    End Function

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SimpleScopeStartEnd(ds, "InsertData")

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SimpleScopeStartEnd(ds, "UpdateData")

    End Function

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SimpleScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = MyBase.CallBLC(Me._Blc, actionStr, ds)

            'エラーがなければトランザクション終了
            If MyBase.IsMessageExist() = False Then
                MyBase.CommitTransaction(scope)
            End If

        End Using

        Return ds

    End Function

#End Region 'Method

End Class