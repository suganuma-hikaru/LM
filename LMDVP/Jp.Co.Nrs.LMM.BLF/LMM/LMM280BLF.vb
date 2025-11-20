' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM280BLF : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM280BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM280BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM280BLC = New LMM280BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''編集時チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "HaitaChk", ds)
        If MyBase.IsMessageExist() Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "DeleteData", ds)
            If IsMessageExist() = True Then
                Exit Sub
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 横持ちタリフマスタ検索処理
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

#Region "保存処理"

    ''' <summary>
    ''' 新規登録処理(横持ちタリフヘッダマスタ、横持ちタリフ明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '横持ちタリフヘッダマスタ存在チェック
            ds = MyBase.CallBLC(Me._Blc, "ExistYokoTariffHedM", ds)
            If IsMessageExist() = True Then
                Return ds
            End If

            '新規登録処理を行う
            ds = MyBase.CallBLC(Me._Blc, "InsertData", ds)
            If IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理(横持ちタリフヘッダマスタ、横持ちタリフ明細マスタ)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateSaveData", ds)
            If IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
