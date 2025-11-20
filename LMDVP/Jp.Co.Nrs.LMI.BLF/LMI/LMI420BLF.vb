' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI420BLF : JX 請求運賃比較
'  作  成  者       :  daikoku
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI420BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI420BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI420BLC = New LMI420BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'Dim setDs As DataSet = ds.Copy
        'Dim setDt As DataTable = setDs.Tables("LMI420IN")
        Dim dt As DataTable = ds.Tables("LMI420IN")
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        'トランザクション開始
        Using scope As TransactionScope = Me.BeginTransaction()


            '同じユーザーIDでワークデータ削除
            'ds = Me.CallBLC(New JFB820BLC, "DeleteRec", ds)
            MyBase.CallBLC(Me._Blc, "DeleteRec", ds)

            If Me.IsErrorMessageExist = True Then Return ds
            'エクセルデータ書き込み
            'ds = Me.CallBLC(New JFB820BLC, "InsertRec", ds)
            'MyBase.CallBLC(Me._Blc, "InsertRec", ds)

            ds = Me.CallBLC(Me._Blc, "InsertRec", ds)
            If Me.IsErrorMessageExist = True Then Return ds

            '取込データと突合せ結果更新
            ds = Me.CallBLC(Me._Blc, "UpdateRec", ds)
            If Me.IsErrorMessageExist = True Then Return ds

            '該当データの抽出
            ds = Me.CallBLC(Me._Blc, "SelectListData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist Then
                Return ds
            End If

            'トランザクション終了
            Me.CommitTransaction(scope)
        End Using

        Return ds
    End Function


    ' ''' <summary>
    ' ''' 検索処理（1件ごとSelect版）
    ' ''' </summary>
    ' ''' <param name="ds">DataSet</param>
    ' ''' <returns>DataSet（プロキシ）</returns>
    ' ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    'Private Function SelectListData(ByVal ds As DataSet) As DataSet

    '    Dim setDs As DataSet = ds.Copy
    '    Dim setDt As DataTable = setDs.Tables("LMI420IN")
    '    Dim dt As DataTable = ds.Tables("LMI420IN")
    '    Dim max As Integer = dt.Rows.Count - 1

    '    'レコードがない場合、スルー
    '    If max < 0 Then
    '        Return ds
    '    End If


    '    For i As Integer = 0 To max

    '        '更新情報の設定
    '        setDt.Clear()
    '        setDt.ImportRow(dt.Rows(i))

    '        'データの更新
    '        MyBase.CallBLC(Me._Blc, "SelectListData", setDs)

    '        'メッセージの判定
    '        If MyBase.IsMessageExist Then
    '            Return ds
    '        End If

    '        'LMI420OUTにつめる
    '        ds.Tables("LMI420OUT").Merge(setDs.Tables("LMI420OUT"))

    '    Next

    '    Return ds
    'End Function

#End Region
End Class
