' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷編集
'  プログラムID     :  LMC791BLC : ケースマーク
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC791BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC791BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC791DAC = New LMC791DAC()


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
        Dim tableNmIn As String = "LMC791IN"
        Dim tableNmOut As String = "LMC791OUT"
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

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectPrintData", setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
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
        Dim copyDs As DataSet
        'Dim prtNb As Integer = 0
        Dim outkaTtlNb As Integer = 0
        Dim caseNoFrom As Integer = 0

        Dim goodsDr As DataRow()

        For Each dr As DataRow In ds.Tables("M_RPT").Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMC791OUT"), dr.Item("RPT_ID").ToString())

            copyDs = prtDs.Copy()

            Dim wkNewRow As DataRow

            Dim preGoodsCdNrs As String = String.Empty
            Dim sumNb As Integer = 0
            Dim copyDr As DataRow
            Dim minusflg As Boolean = False
            Dim temStr As String()
            Dim prtNb As Integer = 0

            Dim loopCnt As Integer = 0

            Dim tempDs As DataSet = ds.Clone()

            For i As Integer = 0 To prtDs.Tables("LMC791OUT").Rows.Count - 1

                copyDs.Clear()
                copyDs.Tables("LMC791OUT").ImportRow(prtDs.Tables("LMC791OUT").Rows(i))
                outkaTtlNb = Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_TTL_NB").ToString())

                sumNb = sumNb + Convert.ToInt32(ds.Tables("LMC791OUT").Rows(i).Item("OUTKA_TTL_NB").ToString())

                caseNoFrom = Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())

                'If String.IsNullOrEmpty(preGoodsCdNrs) AndAlso preGoodsCdNrs.Equals(ds.Tables("LMC791OUT").Rows(i).Item("GOODS_CD_NRS").ToString()) = False Then
                If String.IsNullOrEmpty(preGoodsCdNrs) OrElse preGoodsCdNrs.Equals(ds.Tables("LMC791OUT").Rows(i).Item("GOODS_CD_NRS").ToString()) = False Then
                    '検索結果取得
                    setDs = MyBase.CallDAC(Me._Dac, "SelectGoodsDetails", setDs)
                End If

                '減算フラグリセット
                minusflg = False

                Do While outkaTtlNb > 0

                    goodsDr = setDs.Tables("GOODS_SIZE_WT").Select(String.Concat("IRISU = '", outkaTtlNb, "'"), " GOODS_CD_NRS_EDA DESC")

                    If goodsDr.Length = 0 Then

                        goodsDr = setDs.Tables("GOODS_SIZE_WT").Select(String.Concat("IRISU <= '", outkaTtlNb, "'"), " GOODS_CD_NRS_EDA DESC")

                        If goodsDr.Length > 0 Then

                            If minusflg = False AndAlso goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                                copyDr = copyDs.Tables("LMC791OUT").Rows(copyDs.Tables("LMC791OUT").Rows.Count - 1)

                                temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                                copyDr("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                outkaTtlNb = outkaTtlNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                                prtNb = Convert.ToInt32(temStr(6))
                                copyDr("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                                goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON
                                minusflg = True

                                For j As Integer = 1 To prtNb - 1

                                    wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                                    wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                    wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                    wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                    wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                    wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                    wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                    wkNewRow("CASE_NO_FROM") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString()
                                    wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                    wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                    wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                    copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                                Next

                            ElseIf goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                                wkNewRow = copyDs.Tables("LMC791OUT").NewRow()

                                temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                                wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                outkaTtlNb = outkaTtlNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                                prtNb = Convert.ToInt32(temStr(6))
                                wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                wkNewRow("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                                wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                                For j As Integer = 1 To prtNb - 1

                                    wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                                    wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                    wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                    wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                    wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                    wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                    wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                    wkNewRow("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                                    wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                    wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                    wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                    copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                                Next
                                goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON
                                minusflg = True
                            Else
                                outkaTtlNb = outkaTtlNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                                temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)
                                prtNb = Convert.ToInt32(temStr(6))

                                For j As Integer = 1 To prtNb

                                    wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                                    wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                    wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                    wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                    wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                    wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                    wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                    wkNewRow("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                                    wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                    wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                    wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                    copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                                Next

                            End If

                        Else
                            MyBase.SetMessage("E454", New String() {"サイズ登録", "梱包明細が出力", ds.Tables("LMC791OUT").Rows(i).Item("GOODS_NM").ToString()})
                            Return ds
                            Exit Function
                        End If

                    Else

                        If minusflg = False AndAlso goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                            copyDr = copyDs.Tables("LMC791OUT").Rows(copyDs.Tables("LMC791OUT").Rows.Count - 1)

                            temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                            copyDr("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                            outkaTtlNb = outkaTtlNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                            prtNb = Convert.ToInt32(temStr(6))
                            copyDr("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())

                            For j As Integer = 1 To prtNb - 1

                                wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                                wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                wkNewRow("CASE_NO_FROM") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString()
                                wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                            Next

                            goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON
                            minusflg = True
                        ElseIf goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                            wkNewRow = copyDs.Tables("LMC791OUT").NewRow()

                            temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                            wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                            outkaTtlNb = outkaTtlNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                            prtNb = Convert.ToInt32(temStr(6))
                            wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                            wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                            wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                            wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                            wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                            wkNewRow("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                            wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                            wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                            wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                            copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                            For j As Integer = 1 To prtNb - 1

                                wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                                wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                wkNewRow("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                                wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)

                            Next

                            goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON
                            minusflg = True
                        Else
                            outkaTtlNb = outkaTtlNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                            temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)
                            prtNb = Convert.ToInt32(temStr(6))

                            For j As Integer = 1 To prtNb
                                wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                                wkNewRow("OUTKA_TTL_NB") = goodsDr(0).Item("IRISU")
                                wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                                wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                                wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                                wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                                wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                                wkNewRow("CASE_NO_FROM") = loopCnt + Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                                wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                                wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                                wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                                copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)
                            Next

                        End If

                    End If

                    preGoodsCdNrs = ds.Tables("LMC791OUT").Rows(i).Item("GOODS_CD_NRS").ToString()
                    loopCnt = loopCnt + 1

                Loop

                'Dim tempDs As DataSet
                'tempDs = copyDs.Copy()
                'tempDs.Tables("LMC791OUT").Clear()

                '2015.08.18 協立化学　追加START
                For k As Integer = 1 To 3
                    wkNewRow = copyDs.Tables("LMC791OUT").NewRow()
                    wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC791OUT").Rows(0).Item("NRS_BR_CD").ToString()
                    wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC791OUT").Rows(0).Item("OUTKA_NO_L").ToString()
                    wkNewRow("RPT_ID") = copyDs.Tables("LMC791OUT").Rows(0).Item("RPT_ID").ToString()
                    wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_1").ToString()
                    wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_2").ToString()
                    wkNewRow("CASE_NO_FROM") = Convert.ToInt32(copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_FROM").ToString())
                    wkNewRow("CASE_NO_TO") = copyDs.Tables("LMC791OUT").Rows(0).Item("CASE_NO_TO").ToString()
                    wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_3").ToString()
                    wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC791OUT").Rows(0).Item("MARK_INFO_4").ToString()
                    copyDs.Tables("LMC791OUT").Rows.Add(wkNewRow)
                Next
                '2015.08.18 協立化学　追加END

                For k As Integer = 0 To copyDs.Tables("LMC791OUT").Rows.Count - 1
                    tempDs.Tables("LMC791OUT").ImportRow(copyDs.Tables("LMC791OUT").Rows(k))
                Next

                ''帳票CSV出力
                'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                '                      dr.Item("PTN_ID").ToString(), _
                '                      dr.Item("PTN_CD").ToString(), _
                '                      dr.Item("RPT_ID").ToString(), _
                '                      tempDs.Tables("LMC791OUT"), _
                '                      ds.Tables(LMConst.RD))

            Next

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  tempDs.Tables("LMC791OUT"), _
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

        Return ds

    End Function

#End Region

#End Region

End Class
