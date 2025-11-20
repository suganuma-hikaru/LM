' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM150BLF : 倉庫マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM150BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM150BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM150BLC = New LMM150BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 倉庫マスタ更新対象データ検索処理
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
    ''' 倉庫マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet


        If MyBase.GetForceOparation() = False Then

            '郵便番号マスタ関連チェック
            ds = Me.SaveCheck(ds)

            If MyBase.IsMessageExist = True Then

                Return ds

            End If

        End If

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistSokoM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = true Then

            Return ds

        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertSokoM", ds)

            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 倉庫マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        '強制実行フラグ
        If MyBase.GetForceOparation() = False Then

            '郵便番号マスタ関連チェック
            ds = Me.SaveCheck(ds)

            If MyBase.IsMessageExist = True Then

                Return ds

            End If

        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateSokoM", ds)
            'メッセージの判定

            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 保存時郵便番号チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveCheck(ByVal ds As DataSet) As DataSet

        ''2011.09.08 検証結果_導入時要望№1対応 START
        ''郵便番号存在チェック
        'ds = MyBase.CallBLC(Me._Blc, "CheckZipM", ds)


        'メッセージの判定
        'If MyBase.IsErrorMessageExist() = False Then

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckJisWng", ds)

        'End If
        '2011.09.08 検証結果_導入時要望№1対応 END

        Return ds

    End Function


    ''' <summary>
    ''' 倉庫マスタ排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSokoM", ds)

        Return ds

    End Function


    ''' <summary>
    ''' 倉庫マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteSokoM", ds)

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
