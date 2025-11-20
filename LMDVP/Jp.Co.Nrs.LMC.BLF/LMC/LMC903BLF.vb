' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC903    : 佐川e飛伝Ⅲ CSV出力
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMC903BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC903BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMC903BLC = New LMC903BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 佐川e飛伝ⅢCSV作成時、更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateSagawaEHiden3Csv(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMC903OUT"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        For i As Integer = 0 To max

            '更新情報の設定
            setDt.Clear()
            setDt.ImportRow(dt.Rows(i))

            '更新処理
            If Me.UpdateSagawaEHiden3CsvDataAction(setDs) = False Then
                Continue For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 佐川e飛伝ⅢCSV作成時、更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagawaEHiden3CsvDataAction(ByVal ds As DataSet) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "LMC903OUT"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                'データの更新
                ds = MyBase.CallBLC(Me._Blc, "UpdateSagawaEHiden3Csv", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist(Convert.ToInt32(dr.Item("ROW_NO").ToString())) = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function

#End Region

End Class
