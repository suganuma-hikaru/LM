' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF090BLF : 支払編集
'  作  成  者       :  YANAI
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF090BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF090BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMF090BLC = New LMF090BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'データ件数取得
        ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)
        
        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ排他処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        ''請求鑑ヘッダ
        'ds = MyBase.CallBLC(Me._Blc, "HedChcik", ds)

        ''請求鑑ヘッダー時にエラーがあった場合
        'If MyBase.IsMessageExist() = True Then

        '    'メッセージの再セット
        '    MyBase.SetMessage("E307", New String() {"鑑取込"})

        'End If

        If MyBase.IsMessageExist() = False Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnchinM", ds)

        End If

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運賃マスタ更新処理(保存)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateUnchinM", ds)
            End If

            'メッセージがなかったらtrue
            rtnResult = Not MyBase.IsMessageExist()

            'リターンフラグでメッセージ判定trueのときはトランザクション終了
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新処理(確定)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateDataKakutei(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnchinM", ds)
        End If

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateUnchinKakuteiM", ds)
            End If

            'エラーメッセージがなければtrue
            rtnResult = Not MyBase.IsMessageExist()

            'フラグでエラーメッセージがtrueの場合トランザクション終了
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 運賃マスタ更新処理(確定解除)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateDataKakuteikaijo(ByVal ds As DataSet) As DataSet

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            '排他チェック
            ds = MyBase.CallBLC(Me._Blc, "CheckHaitaUnchinM", ds)
        End If

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateUnchinKakuteiKijoM", ds)
            End If

            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
