' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI050BLC = New LMI050BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectMonthlySnz(ByVal ds As DataSet) As DataSet

        '篠崎運送専用の月末在庫テーブルからデータ取得
        ds = MyBase.CallBLC(Me._Blc, "SelectMonthlySnz", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function MakeDataSNZ(ByVal ds As DataSet) As DataSet

        '月末在庫データの検索
        ds = MyBase.CallBLC(Me._Blc, "SelectZaiZanSnz", ds)
        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '篠崎運送専用の月末在庫テーブル削除処理
            ds = MyBase.CallBLC(Me._Blc, "DeleteDataSNZ", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '篠崎運送専用の月末在庫テーブル作成処理
            ds = MyBase.CallBLC(Me._Blc, "InsertDataSNZ", ds)
            'エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

End Class