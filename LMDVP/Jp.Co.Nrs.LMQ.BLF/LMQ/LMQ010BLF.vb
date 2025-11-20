' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMQ010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMQ010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMQ010BLC = New LMQ010BLC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    '''編集時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function EditChk(ByVal ds As DataSet) As DataSet

        '排他チェック
        Return MyBase.CallBLC(Me._Blc, "HaitaChk", ds)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(_Blc, "SelectData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMQ010INOUT"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then
                '更新処理を行う
                Call Me.UpdateData(ds, "SaveData")
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DelRevData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMQ010INOUT"
        Dim updDt As DataTable = ds.Tables(tableNm)
        Dim dr As DataRow = Nothing
        Dim max As Integer = updDt.Rows.Count - 1
        Dim saveCnt As Integer = 0

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LMQ010INOUT")

        '******* 更新 処理を行う ***************

        For i As Integer = 0 To max
            dr = updDt.Rows(i)
            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '値のクリア
                setDs.Clear()

                '条件の設定
                setDt.ImportRow(dr)

                '更新処理を行う
                Call Me.UpdateData(setDs, "DelRevSQL")
                'メッセージの判定
                If MyBase.IsMessageExist() = False Then
                    'トランザクション終了
                    MyBase.CommitTransaction(scope)
                    saveCnt = saveCnt + 1
                End If

            End Using

        Next

        Dim insRows As DataRow = ds.Tables("LMQ010_SAVECNT").NewRow
        insRows.Item("SAVECNT") = saveCnt
        ds.Tables("LMQ010_SAVECNT").Rows.Add(insRows)

        Return ds

    End Function

    ''' <summary>
    ''' Excel作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExcelMake(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMQ010_EXCEL"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        'Using scope As TransactionScope = MyBase.BeginTransaction()

        If updDt.Rows.Count <> 0 Then
            'データ件数取得
            ds = MyBase.CallBLC(_Blc, "ExcelMake", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        'トランザクション終了
        'MyBase.CommitTransaction(scope)
        'End Using

        Return ds

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveExcelData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMQ010INOUT"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then
                '更新処理を行う
                Call Me.UpdateData(ds, "SaveExcelData")
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

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

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = True Then
            Exit Sub
        End If

    End Sub

#End Region

#End Region

End Class
