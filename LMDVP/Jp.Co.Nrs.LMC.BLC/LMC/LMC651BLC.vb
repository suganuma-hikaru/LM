' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC651BLC : 梱包明細
'  作  成  者       :  inoue
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC651BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC651BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC651DAC = New LMC651DAC()


#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = "LMC651IN"
        Dim tableNmOut As String = "LMC651OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            If LMConst.FLG.ON.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) _
                OrElse LMConst.FLG.OFF.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) Then
                'PRINT_FLGを設定しているのはLMC020のみ
                inTbl.Merge(dt)
            Else
                inTbl.ImportRow(dt.Rows(i))
            End If

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            'OUT
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOut.ImportRow(setDtOut.Rows(j))
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

            'LMC020のときは1回だけ
            If LMConst.FLG.ON.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) _
                OrElse LMConst.FLG.OFF.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) Then
                Exit For
            End If
        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        'ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtRpt.Clone

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        'キーブレイク用(NRS_BR_CD,PTN_ID,RPT_ID)
        Dim keyBrCd As String = String.Empty
        Dim keyPtnId As String = String.Empty
        Dim keyRptId As String = String.Empty

        '重複分を除外したワーク用RPTテーブルを帳票出力に使用するDSにセットする)
        For l As Integer = 0 To sortDtRpt.Rows.Count - 1

            '営業所コード、パターンID、レポートIDが一致するレコードは除外する
            If keyBrCd <> sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString() OrElse _
               keyPtnId <> sortDtRpt.Rows(l).Item("PTN_ID").ToString() OrElse _
               keyRptId <> sortDtRpt.Rows(l).Item("RPT_ID").ToString() Then

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                'キー更新
                keyBrCd = sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(l).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(l).Item("RPT_ID").ToString()

            End If

        Next

        'レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables("M_RPT").Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMC651OUT"), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC651OUT"), _
                                  ds.Tables(LMConst.RD))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="prtId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal prtId As String, ByVal ds As DataSet) As DataSet
        Return ds
    End Function

    ''' <summary>
    ''' 出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

#End Region

#End Region

End Class
