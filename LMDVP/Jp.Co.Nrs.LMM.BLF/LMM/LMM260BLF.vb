' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM260BLF : 注意書マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM260BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM260BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM260BLC = New LMM260BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 注意書マスタ更新対象データ検索処理
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
    ''' 注意書マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub InsertData(ByVal ds As DataSet)

        ds = MyBase.CallBLC(Me._Blc, "CountM", ds)

        'エラーがある場合、終了
        If MyBase.IsMessageExist() = True Then
            Exit Sub
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertChuigakiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End If
            

        End Using

    End Sub

    ''' <summary>
    ''' 注意書マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateChuigakiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End If



        End Using

    End Sub

    ''' <summary>
    ''' 注意書マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaChuigakiM", ds)

    End Sub

    ''' <summary>
    ''' 注意書マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaChuigakiM", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteChuigakiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End If

        End Using
    End Sub
#End Region

#End Region

End Class
