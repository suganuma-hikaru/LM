' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM070BLF : 割増運賃マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM070BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM070BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM070BLC = New LMM070BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 都道府県名データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "ComboData", ds)

    End Function

    ''' <summary>
    ''' 割増運賃マスタ更新対象データ検索処理
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
    ''' 割増運賃マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallBLC(Me._Blc, "CheckExistwarimashiM", ds)
        If MyBase.IsErrorMessageExist() = True Then
            Return ds
        End If
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertwarimashiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)


            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdatewarimashiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)


            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        Return MyBase.CallBLC(Me._Blc, "CheckHaitawarimashiM", ds)

    End Function

    ''' <summary>
    ''' 割増運賃マスタ削除処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除用)</param>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet


        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistDeleteM", ds)
        If MyBase.IsErrorMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeletewarimashiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)


            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 割増運賃マスタ復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(復活用)</param>
    ''' <remarks></remarks>
    Private Function RevivalData(ByVal ds As DataSet) As DataSet


        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistRivivalM", ds)
        If MyBase.IsErrorMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeletewarimashiM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)


            End If

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
