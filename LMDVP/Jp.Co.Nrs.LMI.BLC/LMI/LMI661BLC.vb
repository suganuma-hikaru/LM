' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI661BLC : 請求鑑(DIC鑑種別：B(横持料除く))
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI661BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI661BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI661DAC = New LMI661DAC()


#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = "LMI660IN"
        Dim tableNmOut As String = "LMI661OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dtIn As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = dtIn.Rows.Count - 1
        Dim setDtOut As DataTable = Nothing
        Dim setDtRpt As DataTable = Nothing

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

        'IN条件0件チェック
        If dtIn.Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dtIn.Rows(i))

            '使用帳票ID取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectMPrt", setDs)

            '取得件数判定
            If setDs.Tables("M_RPT").Rows.Count = 0 Then
                Continue For
            End If

            '検索結果取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectPrintData", setDs)

            '取得件数判定
            If setDs.Tables(tableNmOut).Rows.Count = 0 Then
                Continue For
            End If

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOut.ImportRow(setDtOut.Rows(j))
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next

        '取得件数判定
        If dtOut.Rows.Count = 0 Then
            MyBase.SetMessageStore("00", "E078", New String() {"請求鑑(LMI661)"})
            Return ds
        End If

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
            prtDs = comPrt.CallDataSet(ds.Tables("LMI661OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables("LMI661OUT"), _
                                ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        Dim outDs As DataSet = ds.Copy
        Dim outDt As DataTable = outDs.Tables("LMI661OUT")
        Dim outDr As DataRow = Nothing
        Dim dr As DataRow() = ds.Tables("LMI661OUT").Select
        Dim max As Integer = dr.Length - 1

        outDt.Clear()
        outDr = outDt.NewRow

        '帳票ID
        outDr.Item("RPT_ID") = dr(0).Item("RPT_ID")

        '営業所コード
        outDr.Item("NRS_BR_CD") = dr(0).Item("NRS_BR_CD")

        '請求日
        outDr.Item("SKYU_DATE") = dr(0).Item("SKYU_DATE")

        '鑑区分
        outDr.Item("KAGAMI_KB") = dr(0).Item("KAGAMI_KB")

        '荷役保管料
        outDr.Item("HN_TTL") = 0

        '荷造料
        outDr.Item("NIZUKURI") = 0

        '横持料
        outDr.Item("YOKOMOCHI") = 0

        '合計を求める計算
        For i As Integer = 0 To max

            outDr.Item("HN_TTL") = Convert.ToDecimal(outDr.Item("HN_TTL")) + Convert.ToDecimal(dr(i).Item("HN_TTL"))
            outDr.Item("NIZUKURI") = Convert.ToDecimal(outDr.Item("NIZUKURI")) + Convert.ToDecimal(dr(i).Item("NIZUKURI"))
            outDr.Item("YOKOMOCHI") = Convert.ToDecimal(outDr.Item("YOKOMOCHI")) + Convert.ToDecimal(dr(i).Item("YOKOMOCHI"))

        Next

        '消費税
        outDr.Item("TAX") = dr(0).Item("TAX")

        '営業所名
        outDr.Item("NRS_BR_NM") = dr(0).Item("NRS_BR_NM")

        '住所
        outDr.Item("AD_1") = dr(0).Item("AD_1")
        outDr.Item("AD_2") = dr(0).Item("AD_2")

        outDt.Rows.Add(outDr)

        Return outDs

    End Function

#End Region

#End Region

End Class
