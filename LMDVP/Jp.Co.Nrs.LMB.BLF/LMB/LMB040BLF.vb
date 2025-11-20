' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB040BLF : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB040BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB040BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB040BLC = New LMB040BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 入荷検品選択対象データ検索処理
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
    ''' 入荷取込フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaToriFlg(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMB040IN_UPDATE"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To max

                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))

                'データの更新
                MyBase.CallBLC(Me._Blc, "UpdateInkaToriFlg", setDs)

                'メッセージの判定
                If MyBase.IsMessageExist Then
                    Return ds
                End If
            Next

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 入荷取込フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMB040IN_DELETE"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            For i As Integer = 0 To max

                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))

                'データの更新
                MyBase.CallBLC(Me._Blc, "DeleteAction", setDs)

                'メッセージの判定
                If MyBase.IsMessageExist Then
                    Return ds
                End If
            Next

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
