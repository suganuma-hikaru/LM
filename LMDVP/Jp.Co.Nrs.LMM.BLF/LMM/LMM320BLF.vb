' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM320BLF : 請求項目マスタ
'  作  成  者       :  笈川
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

Public Class LMM320BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM320BLC = New LMM320BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 請求先マスタ更新対象データ検索処理
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

#Region "設定処理"

    ''' <summary>
    ''' 請求項目マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "InsertSeikyukoumokuM", "CheckExistSeikyukoumokuM")

    End Function

    ''' <summary>
    ''' 請求項目マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'データの更新
        Return Me.ScopeStartEnd(ds, "UpdateSeikyukoumokuM")

    End Function

    ''' <summary>
    ''' 請求項目マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSeikyukoumokuM", ds)

    End Sub

    ''' <summary>
    ''' 請求項目マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSeikyukoumokuM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            'データの更新
            ds = Me.ScopeStartEnd(ds, "DeleteSeikyukoumokuM")

        End If

    End Sub

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="chkActionId">チェックメソッド名</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionId As String, Optional ByVal chkActionId As String = "") As DataSet

        '排他チェック
        If String.IsNullOrEmpty(chkActionId) = False Then

            'アクションIDが存在する場合排他チェックを行う
            ds = MyBase.CallBLC(Me._Blc, chkActionId, ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = True Then
                Return ds
            End If

        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, actionId, ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
