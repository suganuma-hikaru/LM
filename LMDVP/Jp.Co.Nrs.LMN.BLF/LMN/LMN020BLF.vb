' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020BLF : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMN020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMN020BLC = New LMN020BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期表示データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 出荷データ詳細削除処理(INSERT_FLG = '0'/ステータス「00:未設定」「01:設定済み」時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataInsFlgOff(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()


            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShukkaDataInsFlgOff", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの論理削除
                ds = MyBase.CallBLC(Me._Blc, "DeleteShukkaDataInsFlgOff", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ詳細削除処理(INSERT_FLG = '0'/ステータス「02:倉庫指示済み」時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataInsFlgOffSoko(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShukkaDataInsFlgOff", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                '倉庫側DBの対象データの削除状態をチェックする
                ds = MyBase.CallBLC(Me._Blc, "CheckHOutkaEdiL", ds)
            End If

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの論理削除
                ds = MyBase.CallBLC(Me._Blc, "DeleteShukkaDataInsFlgOff", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ詳細削除処理(INSERT_FLG = '1'時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataInsFlgOn(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()


            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShukkaDataInsFlgOn", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの論理削除
                ds = MyBase.CallBLC(Me._Blc, "DeleteShukkaDataInsFlgOn", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#Region "初期化処理"

    ''' <summary>
    ''' 出荷データ詳細初期化処理(ステータス「00:未設定」「01:設定済み」時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InitData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaInitShukkaData", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの初期化
                ds = MyBase.CallBLC(Me._Blc, "InitShukkaData", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ詳細初期化処理(ステータス「02:倉庫指示済み」時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InitDataSokoChk(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaInitShukkaData", ds)

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                '倉庫側DBの対象データの削除状態をチェックする
                ds = MyBase.CallBLC(Me._Blc, "CheckHOutkaEdiL", ds)
            End If

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの初期化
                ds = MyBase.CallBLC(Me._Blc, "InitShukkaData", ds)
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
