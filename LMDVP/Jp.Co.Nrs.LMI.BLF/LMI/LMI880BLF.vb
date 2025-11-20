' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主
'  プログラムID     :  LMI880  : 篠崎運送月末在庫実績データ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI880BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI880BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI880BLC = New LMI880BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectCSVメソッドに飛ぶ</remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectCSV", ds)

    End Function

    ''' <summary>
    ''' CSV出力データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function UpdateCSV(ByVal ds As DataSet) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'メッセージの判定
            If MyBase.IsErrorMessageExist() = False Then
                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateCSV", ds)
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

#End Region

End Class