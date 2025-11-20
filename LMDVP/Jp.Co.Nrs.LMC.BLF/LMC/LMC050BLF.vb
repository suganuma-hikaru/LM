' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC050BLF : 出荷印刷指示
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print590 As LMC590BLC = New LMC590BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print610 As LMC610BLC = New LMC610BLC()

    '(2012.11.02)千葉対応 --- START ---
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print720 As LMC720BLC = New LMC720BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print721 As LMC721BLC = New LMC721BLC()
    '(2012.11.02)千葉対応 ---  END  ---

    '2013.07.31 追加START
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print860 As LMC860BLC = New LMC860BLC()
    '2013.07.31 追加END

#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        '印刷種別がチェックリストか、出荷報告書を取得
        Dim dt As DataTable = ds.Tables("LMC050IN")
        Dim dr As DataRow = dt.Rows(0)
        Dim flg As String = String.Empty

        flg = dr.Item("PRINT_FLAG").ToString()

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        Select Case flg

            Case "01"
                'チェックリスト
                Dim Prmds As DataSet = New LMC590DS

                Dim PrmDt As DataTable = Prmds.Tables("LMC590IN")

                'LMC590INのデータテーブルをクリア
                PrmDt.Clear()

                'LMC050のテーブルにLMC590と同じ物があればデータセット
                PrmDt.ImportRow(ds.Tables("LMC050IN").Rows(0))

                Prmds.Merge(New RdPrevInfoDS)

                'LMC590の呼び出し
                ds = MyBase.CallBLC(Me._Print590, "DoPrint", Prmds)

                rdPrevDt.Merge(Prmds.Tables(LMConst.RD))

            Case "02", "03"

                '日別出荷報告書、出荷報告書(月次)

                Dim Prmds As DataSet = New LMC610DS

                Dim PrmDt As DataTable = Prmds.Tables("LMC610IN")

                'LMC610INのデータテーブルをクリア
                PrmDt.Clear()

                'LMC050のテーブルにLMC610と同じ物があればデータセット
                PrmDt.ImportRow(ds.Tables("LMC050IN").Rows(0))

                Prmds.Merge(New RdPrevInfoDS)

                'LMC610の呼び出し
                ds = MyBase.CallBLC(Me._Print610, "DoPrint", Prmds)

                rdPrevDt.Merge(Prmds.Tables(LMConst.RD))

                '(2012.11.02)千葉対応 追加 --- START ---
            Case "04"
                'JAL改定
                Dim Prmds As DataSet = New LMC720DS
                Dim PrmDt As DataTable = Prmds.Tables("LMC720IN")

                'LMC720INのデータテーブルをクリア
                PrmDt.Clear()

                'LMC050のテーブルにLMC720と同じ物があればデータセット
                PrmDt.ImportRow(ds.Tables("LMC050IN").Rows(0))

                Prmds.Merge(New RdPrevInfoDS)

                'LMC720の呼び出し
                ds = MyBase.CallBLC(Me._Print720, "DoPrint", Prmds)

                rdPrevDt.Merge(Prmds.Tables(LMConst.RD))

            Case "05"
                'ANA改定
                Dim Prmds As DataSet = New LMC721DS
                Dim PrmDt As DataTable = Prmds.Tables("LMC721IN")

                'LMC721INのデータテーブルをクリア
                PrmDt.Clear()

                'LMC050のテーブルにLMC721と同じ物があればデータセット
                PrmDt.ImportRow(ds.Tables("LMC050IN").Rows(0))

                Prmds.Merge(New RdPrevInfoDS)

                'LMC721の呼び出し
                ds = MyBase.CallBLC(Me._Print721, "DoPrint", Prmds)

                rdPrevDt.Merge(Prmds.Tables(LMConst.RD))

                '(2012.11.02)千葉対応 追加 ---  END  ---

            Case "06"
                'BYK出荷報告作成
                Dim Prmds As DataSet = New LMC860DS
                Dim PrmDt As DataTable = Prmds.Tables("LMC860IN")

                'LMC860INのデータテーブルをクリア
                PrmDt.Clear()

                'LMC050のテーブルにLMC860と同じ物があればデータセット
                PrmDt.ImportRow(ds.Tables("LMC050IN").Rows(0))

                'LMC860の呼び出し(出荷報告データ抽出)
                ds = MyBase.CallBLC(Me._Print860, "OutkaHokoku", Prmds)

                'LMC860の呼び出し(出荷報告データ作成・出荷ステージUP)
                ds = Me.UpdateStatus(ds)

                Return ds

        End Select

        ds.Tables(LMConst.RD).Clear()
        ds.Tables(LMConst.RD).Merge(rdPrevDt)

        Return ds

    End Function

#End Region

    '2013.07.31 追加START
#Region "Method"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateStatus(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMC860OUT"
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
            If Me.UpdateOutkaDataAction(setDs) = False Then
                Continue For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷報告作成時、更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaDataAction(ByVal ds As DataSet) As Boolean

        '更新対象テーブル名
        Dim tableNm As String = "LMC860OUT"
        Dim updDt As DataTable = ds.Tables(tableNm)

        '******* 更新 処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            If updDt.Rows.Count <> 0 Then

                Dim dr As DataRow = updDt.Rows(0)

                '出荷報告データの更新
                ds = MyBase.CallBLC(Me._Print860, "InsertSendOutBykAgtData", ds)

                '出荷データの更新
                ds = MyBase.CallBLC(Me._Print860, "UpdateOutkaData", ds)

                'メッセージの判定
                If MyBase.IsMessageStoreExist() = True Then
                    Return False
                End If

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return True

    End Function

#End Region
    '2013.07.31 追加END

#End Region

End Class
