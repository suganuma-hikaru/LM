' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM050BLF : 請求先マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMM050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM050BLC = New LMM050BLC()

#End Region

#Region "Method"

#Region "起動時処理"

    ''' <summary>
    ''' JDE非必須ユーザ確認
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectBusyo(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectBusyo", ds)

    End Function

#End Region

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

    ''' <summary>
    ''' 変動保管料関連チェック用検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectVarStrage", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 請求先マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub InsertData(ByVal ds As DataSet)

        '2011.09.08 検証結果_導入時要望№1対応 START
        ''郵便番号存在チェック
        'ds = MyBase.CallBLC(Me._Blc, "CheckZipM", ds)
        '2011.09.08 検証結果_導入時要望№1対応 END

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            '存在チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckExistSeikyusakiM", ds)
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの新規登録
                ds = MyBase.CallBLC(Me._Blc, "InsertSeikyusakiM", ds)
            End If
            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

    End Sub

    ''' <summary>
    ''' 請求先マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)

        '2011.09.08 検証結果_導入時要望№1対応 START
        ''郵便番号存在チェック
        'ds = MyBase.CallBLC(Me._Blc, "CheckZipM", ds)
        '2011.09.08 検証結果_導入時要望№1対応 END

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSeikyusakiM", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

    End Sub

    ''' <summary>
    ''' 請求先マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSeikyusakiM", ds)

    End Sub

    '2011.09.08 検証結果_導入時要望№1対応 START
    '''' <summary>
    '''' 郵便番号存在チェック
    '''' </summary>
    '''' <param name="ds"></param>
    '''' <remarks></remarks>
    'Private Sub ExistZip(ByVal ds As DataSet)

    '    '存在チェック
    '    ds = MyBase.CallBLC(Me._Blc, "CheckZipM", ds)
    '    'メッセージの判定
    '    If MyBase.IsErrorMessageExist() = False Then

    '    End If

    'End Sub
    '2011.09.08 検証結果_導入時要望№1対応 END


    ''' <summary>
    ''' 請求先マスタ削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet(削除・復活用)</param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaSeikyusakiM", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "DeleteSeikyusakiM", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
    End Sub
#End Region


#Region "請求通貨コンボ取得"

    ''' <summary>
    ''' 契約通貨コンボ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectComboSeiqCurrCd(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectComboSeiqCurrCd", ds)

    End Function

#End Region

#End Region

End Class
