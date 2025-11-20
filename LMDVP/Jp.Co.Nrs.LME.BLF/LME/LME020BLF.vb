' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME020  : 作業料明細編集
'  作  成  者       :  [YANAI]
' ==========================================================================

Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LME020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LME020BLC = New LME020BLC()

#End Region

#Region "Method"

#Region "設定処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectData", ds)

    End Function

#End Region

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SaveComData(ds, "InsertSaveAction")

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SaveComData(ds, "UpdateSaveAction")

    End Function

    ''' <summary>
    ''' 削除登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteSaveAction(ByVal ds As DataSet) As DataSet

        '作業料取込チェック
        Dim rtnDs As DataSet = Me.ChkSeiqDateSagyo(ds)

        'エラーがあるかを判定
        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()

        If rtnResult = False Then
            'エラーがある場合次のレコード
        Else
            '削除処理
            Return Me.SaveComData(ds, "DeleteSaveAction")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 作業料取込チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyo(ByVal ds As DataSet) As DataSet

        '作業料取込チェック
        Call MyBase.CallBLC(Me._Blc, "ChkSeiqDateSagyo", ds)

        'エラーがあるかを判定
        Dim rtnResult As Boolean = Not MyBase.IsMessageExist()

        If rtnResult = False Then
            'エラーがある場合次のレコード
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理共通
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveComData(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        '保存処理
        ds = Me.ScopeStartEnd(ds, actionId)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ds = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            Else
                'エラーの場合はエラー処理
            End If

        End Using

        Return ds

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

#End Region

#End Region

End Class