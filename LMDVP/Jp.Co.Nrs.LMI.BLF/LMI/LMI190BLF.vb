' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI190BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI190BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI190BLC = New LMI190BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetDataMain(ByVal ds As DataSet) As DataSet

        '荷主明細データ取得
        ds = MyBase.CallBLC(Me._Blc, "SelectCustDetailsData", ds)

        '削除対象データ取得
        ds = MyBase.CallBLC(Me._Blc, "SelectDeleteData", ds)

        '入荷データ取得
        ds = MyBase.CallBLC(Me._Blc, "SelectInkaData", ds)

        '出荷データ取得
        ds = MyBase.CallBLC(Me._Blc, "SelectOutkaData", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '取得した削除対象データを元に削除
            ds = MyBase.CallBLC(Me._Blc, "DeleteContTrackData", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '取得した入荷データを元に削除
            ds = MyBase.CallBLC(Me._Blc, "DeleteContTrackInkaData", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '取得した出荷データを元に削除
            ds = MyBase.CallBLC(Me._Blc, "DeleteContTrackOutkaData", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '取得した入荷データを元に追加
            ds = MyBase.CallBLC(Me._Blc, "InsertContTrackInkaData", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '取得した出荷データを元に追加
            ds = MyBase.CallBLC(Me._Blc, "InsertContTrackOutkaData", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'ハネウェル用データ管理取得ログ作成
            ds = MyBase.CallBLC(Me._Blc, "InsertLogData", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' シリンダタイプコンボの値を取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCylinder(ByVal ds As DataSet) As DataSet

        'シリンダタイプコンボの値を取得
        ds = MyBase.CallBLC(Me._Blc, "SelectCylinder", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫場所コンボの値を取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectToFromNm(ByVal ds As DataSet) As DataSet

        '在庫場所コンボの値を取得
        ds = MyBase.CallBLC(Me._Blc, "SelectToFromNm", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountZaiko(ByVal ds As DataSet) As DataSet

        '在庫検索データ件数取得処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuCountZaiko", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuZaikoData(ByVal ds As DataSet) As DataSet

        '在庫検索データ取得処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuZaikoData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 履歴検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountRireki(ByVal ds As DataSet) As DataSet

        '履歴検索データ件数取得処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuCountRireki", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 履歴検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuRirekiData(ByVal ds As DataSet) As DataSet

        '履歴検索データ取得処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuRirekiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄済検索データ件数取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuCountHaiki(ByVal ds As DataSet) As DataSet

        '廃棄済検索データ取得処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuCountHaiki", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄済検索データ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuHaikiData(ByVal ds As DataSet) As DataSet

        '廃棄済検索データ取得処理
        ds = MyBase.CallBLC(Me._Blc, "SelectKensakuHaikiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '廃棄済検索データ取得処理
            ds = MyBase.CallBLC(Me._Blc, "UpdateHaikiData", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 廃棄解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHaikiKaijoData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '廃棄解除処理
            ds = MyBase.CallBLC(Me._Blc, "UpdateHaikiKaijoData", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function



    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonData(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '保存処理
            ds = MyBase.CallBLC(Me._Blc, "UpdateHozonData", ds)

            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' N40コード取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ImportN40Code(ByVal ds As DataSet) As DataSet

        '処理の性質上、トランザクション制御が必須ではなく、また、
        'トランザクションがタイムアウトすることがあるのでトランザクション制御を行わない

        ds = MyBase.CallBLC(Me._Blc, "ImportN40Code", ds)

        Return ds

    End Function

#End Region

End Class