' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI930  : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI930BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI930BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI930BLC = New LMI930BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' TXT出力データ検索/DBフラグ更新(EDI出荷・受信TBL)処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectTXTメソッドに飛ぶ</remarks>
    Private Function selectTxtFlgUpd(ByVal ds As DataSet) As DataSet

        '①実績作成対象データ検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectTXT", ds)

        'エラーの場合、終了
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '②各種TBLのフラグ更新処理
            ds = MyBase.CallBLC(Me._Blc, "updateTargetFlg", ds)

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