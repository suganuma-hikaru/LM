' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM030BLF : 作業項目マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM030BLC = New LMM030BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 作業項目マスタ更新対象データ検索処理
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
    ''' 協力会社作業の名称取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSagyoSub(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectSagyoSub", ds)

    End Function

    ''' <summary>
    ''' 協力会社作業の重複チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectExistSagyoSub(ByVal ds As DataSet) As DataSet

        Return MyBase.CallBLC(Me._Blc, "SelectExistSagyoSub", ds)

    End Function

#End Region

#Region "契約通貨初期値取得"

    ''' <summary>
    ''' 契約通貨初期値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectItemCurrCd(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectItemCurrCd", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 作業項目マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertSagyoM", ds)

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSagyoM", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSagyoM", ds)

    End Sub

    ''' <summary>
    ''' 作業項目マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSagyoM", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "DeleteSagyoM", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub
#End Region

#End Region

End Class
