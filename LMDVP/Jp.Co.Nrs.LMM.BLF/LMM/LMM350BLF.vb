' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM350BLF : 初期出荷元マスタ
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMM350BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM350BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMM350BLC = New LMM350BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 初期出荷マスタ設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMM350IN_UPDATE"

        '******* 新規登録データ / 更新データを作成 ********

        '新規登録データ/更新データ 格納用
        Dim insDs As DataSet = New LMM350DS()
        Dim insDt As DataTable = insDs.Tables(tableNm)
        '新規登録用データ抽出
        Dim updDt As DataTable = ds.Tables(tableNm)
        Dim insdr As DataRow() = updDt.Select("UPD_FLG = '0'")
        Dim max As Integer = insdr.Length - 1
        For i As Integer = 0 To max
            '新規登録用データ設定
            insDt.ImportRow(insdr(i))
            '更新用データから新規登録対象データを削除
            updDt.Rows.Remove(insdr(i))
        Next

        '******* 新規登録 / 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If insDt.Rows.Count <> 0 Then
                '新規登録処理を行う
                Call Me.InsertData(insDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return insDs
                End If
            End If

            If updDt.Rows.Count <> 0 Then
                '更新処理を行う
                Call Me.UpdateData(ds)
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
    ''' 初期出荷マスタ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Sub InsertData(ByVal ds As DataSet)

        '存在チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckExistShokiShukkaM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            'データの新規登録
            ds = MyBase.CallBLC(Me._Blc, "InsertShokiShukkaM", ds)
        End If

    End Sub

    ''' <summary>
    ''' 初期出荷マスタ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Sub UpdateData(ByVal ds As DataSet)

        '排他チェック
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaShokiShukkaM", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateShokiShukkaM", ds)
        End If

    End Sub

#End Region

#End Region

End Class
