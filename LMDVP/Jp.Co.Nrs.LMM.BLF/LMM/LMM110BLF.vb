' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM110BLF : 休日マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM110BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM110BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM110BLC = New LMM110BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 休日マスタ更新対象データ検索処理
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
    ''' 存在チェック(編集押下時)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExistData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectExistData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 休日マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "SelectExistDataIn", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If MyBase.IsErrorMessageExist() = False Then

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "InsertHolM", ds)

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If


        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'データの更新
        ds = MyBase.CallBLC(Me._Blc, "SelectUpdData", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateHolM", ds)

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ日付複写処理
    ''' </summary>
    ''' <param name="ds">DataSet(複写用)</param>
    ''' <remarks></remarks>
    Private Function CopyData(ByVal ds As DataSet) As DataSet


        'データ件数取得
        ds = MyBase.CallBLC(Me._Blc, "SelectCopyData", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "CopyHolM", ds)

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function


    ''' <summary>
    ''' 休日マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "SelectExistData", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "DeleteHolM", ds)

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If
        End Using

    End Sub

#End Region

#End Region

End Class
