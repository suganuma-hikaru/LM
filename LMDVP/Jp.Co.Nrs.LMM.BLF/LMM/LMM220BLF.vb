' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM220BLF : 商品状態区分マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM220BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM220BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM220BLC = New LMM220BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品状態区分マスタ更新対象データ検索処理
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
    ''' 商品状態区分マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistShohinKbnM", ds)

        'メッセージがある場合、終了
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertShohinKbnM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 商品状態区分マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateShohinKbnM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End If



        End Using

    End Sub

    ''' <summary>
    ''' 商品状態区分マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShohinKbnM", ds)

    End Sub

    ''' <summary>
    ''' 商品状態区分マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShohinKbnM", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteShohinKbnM", ds)

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
