' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB070BLF : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB070BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB070BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB070BLC = New LMB070BLC()

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
    ''' 入荷写真データ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaPhoto(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMB070OUT_INKA_PHOTO"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim tableNmDel As String = "LMB070IN"
        Dim setDtDel As DataTable = setDs.Tables(tableNmDel)
        Dim dtDel As DataTable = ds.Tables(tableNmDel)

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '削除
            If dtDel.Rows.Count > 0 Then

                '更新情報の設定
                setDtDel.Clear()
                setDtDel.ImportRow(dtDel.Rows(0))

                'データの更新
                MyBase.CallBLC(Me._Blc, "DeleteInkaPhoto", setDs)

                'メッセージの判定
                If MyBase.IsMessageExist Then
                    Return ds
                End If
            End If

            '追加
            For i As Integer = 0 To max

                '更新情報の設定
                setDt.Clear()
                setDt.ImportRow(dt.Rows(i))

                'データの更新
                MyBase.CallBLC(Me._Blc, "InsertInkaPhoto", setDs)

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

    ''' <summary>
    ''' 写真データ変更
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateNpPhoto(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'データの更新
            ds = MyBase.CallBLC(Me._Blc, "UpdateNpPhoto", ds)

            'メッセージの判定
            If MyBase.IsMessageExist Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

#End Region

#End Region

End Class
