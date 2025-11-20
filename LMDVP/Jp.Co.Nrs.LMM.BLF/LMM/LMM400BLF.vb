' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM400BLF : 西濃マスタ
'  作  成  者       :  adachi
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM400BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM400BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM400BLC = New LMM400BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 西濃マスタ更新対象データ検索処理
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
    ''' 西濃マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistSeinoM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then

            Return ds

        End If


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertSeinoM", ds)

            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ更新処理+排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        '==========================
        'WSA 排他チェッククラス呼出 3
        '==========================
        ds = Me.HaitaData(ds)


        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then

            Return ds

        End If

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistSeinoM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then

            Return ds

        End If



        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateSeinoM", ds)
            'メッセージの判定

            If MyBase.IsErrorMessageExist() = False Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ' ''' <summary>
    ' ''' 必須項目チェック 2015/3/17 AM10確認中
    ' ''' </summary>
    ' ''' <param name="ds"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function SaveCheck(ByVal ds As DataSet) As DataSet

    '    '必須項目チェック
    '    'ds = MyBase.CallBLC(Me._Blc, "CheckHissuItem", ds)

    '    Return ds

    'End Function


    ''' <summary>
    ''' 西濃マスタ排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSeinoM", ds)

        Return ds

    End Function


    ''' <summary>
    ''' 西濃マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet


        '存在チェック
        '復活のみ3/18
        ds = MyBase.CallBLC(Me._Blc, "CheckExistSeinoM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then

            Return ds

        End If


        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteSeinoM", ds)

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
