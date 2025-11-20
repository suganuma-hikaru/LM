' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ360BLF : Tra-Net連携共通処理
'  作  成  者       :  kumakura
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ360BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ360BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _Blc As LMZ360BLC = New LMZ360BLC()

#End Region

#Region "Method"

#Region "データ送信処理"

    ''' <summary>
    '''データ追加
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DataAdd(ByVal ds As DataSet) As DataSet

        'データ取得
        ds = MyBase.CallBLC(Me._Blc, "DataGet", ds)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '送信フラグ更新
            ds = MyBase.CallBLC(Me._Blc, "UpdSoshinFlg", ds)
            If MyBase.IsMessageStoreExist() Then
                Return ds 'ロールバック
            End If

            'データ送信
            ds = MyBase.CallBLC(Me._Blc, "DataSend", ds)
            If MyBase.IsMessageStoreExist() Then
                Return ds 'ロールバック
            End If

            'コミット
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
