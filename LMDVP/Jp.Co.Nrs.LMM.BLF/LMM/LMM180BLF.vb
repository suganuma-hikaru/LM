' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM180BLF : JISマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM180BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM180BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM180BLC = New LMM180BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' JISマスタ更新対象データ検索処理
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
    ''' 都道府県名データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "ComboData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' JISマスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistJisM", ds)

        'メッセージがある場合、終了
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertJisM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

                'コンボ用のデータ取得
                ds = Me.ComboData(ds)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' JISマスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateJisM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

                'コンボ用のデータ取得
                ds = Me.ComboData(ds)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' JISマスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        Return MyBase.CallBLC(Me._Blc, "CheckHaitaJisM", ds)

    End Function

    ''' <summary>
    ''' JISマスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaJisM", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteJisM", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

                'コンボ用のデータ取得
                ds = Me.ComboData(ds)

            End If

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
