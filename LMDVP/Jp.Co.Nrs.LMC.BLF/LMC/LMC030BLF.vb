' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC030BLF : 送状番号入力
'  作  成  者       :  nishikawa
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMC030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMC030BLC = New LMC030BLC()

#End Region

#Region "Method"

#Region "名称取得処理"

    ''' <summary>
    ''' 名称取得時(更新)データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HozonData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMC030IN"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC030IN")
        Dim outDt As DataTable = ds.Tables("LMC030OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC030IN")
        
        '******* 更新 処理を行う ***************
        For i As Integer = 0 To max

            '更新情報の設定
            inTbl.Clear()
            inTbl.ImportRow(dt.Rows(i))

            'データ取得
            'START YANAI メモ②No.13
            'Call Me.UpdateData(ds, "SelectUpdData")
            Call Me.UpdateData(setDs, "SelectUpdData")
            'END YANAI メモ②No.13

            'メッセージの判定
            If MyBase.IsMessageStoreExist(Convert.ToInt32(dt.Rows(i).Item("ROW_NO").ToString())) = True Then
                'エラーの場合、次レコード
                Continue For
            End If

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '更新処理を行う
                'START YANAI メモ②No.13
                'Call Me.UpdateData(ds, "UpdateOutkaL")
                Call Me.UpdateData(setDs, "UpdateOutkaL")
                'END YANAI メモ②No.13
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

                '更新処理を行う
                'START YANAI メモ②No.13
                'Call Me.UpdateData(ds, "UpdateUnsoL")
                Call Me.UpdateData(setDs, "UpdateUnsoL")
                'END YANAI メモ②No.13
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End Using

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet, ByVal methodNm As String)

        'データの更新
        ds = MyBase.CallBLC(Me._Blc, methodNm, ds)

    End Sub

#End Region

#End Region

End Class
