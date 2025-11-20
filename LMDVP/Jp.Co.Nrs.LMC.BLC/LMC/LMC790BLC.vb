' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷編集
'  プログラムID     :  LMC790BLC : 梱包明細
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC790BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC790BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC790DAC = New LMC790DAC()


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
        Dim tableNmIn As String = "LMC790IN"
        Dim tableNmOut As String = "LMC790OUT"
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
        For Each dr As DataRow In ds.Tables("M_RPT").Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMC790OUT"), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs, ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables("LMC790OUT"), _
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
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet, ByVal motoDs As DataSet) As DataSet

        '抽出データテーブル取得
        Dim outDt As DataTable = ds.Tables("LMC790OUT")

        Dim goodsDt As DataTable = motoDs.Tables("GOODS_SIZE_WT")

        Dim copyDs As DataSet = ds.Copy()

        Dim alctdNb As Integer = 0
        Dim goodsDr As DataRow()

        '返却用のデータセット※編集対象のOUTテーブルのみを事前クリア
        Dim tempDs As DataSet = ds.Copy()
        tempDs.Tables("LMC790OUT").Clear()

        Dim tempDr As DataRow

        Dim temStr As String()

        Dim preGoodsCdNrs As String = String.Empty
        Dim minusflg As Boolean = False
        Dim wkNewRow As DataRow = Nothing
        Dim sumNb As Integer = 0

        '抽出データ数
        Dim max As Integer = 0

        '抽出データ数設定
        max = outDt.Rows.Count - 1

        '■□■□■□■□■□ 明細作成 ■□■□■□■□■□
        For i As Integer = 0 To max

            copyDs.Tables("LMC790OUT").Clear()
            copyDs.Tables("LMC790OUT").ImportRow(ds.Tables("LMC790OUT").Rows(i))
            alctdNb = Convert.ToInt32(ds.Tables("LMC790OUT").Rows(i).Item("ALCTD_NB").ToString())

            sumNb = sumNb + Convert.ToInt32(ds.Tables("LMC790OUT").Rows(i).Item("ALCTD_NB").ToString())

            outDt.Rows(i).Item("LOT_NO_NB") = Replace(outDt.Rows(i).Item("LOT_NO_NB").ToString(), ",", vbCrLf)

            'If String.IsNullOrEmpty(preGoodsCdNrs) AndAlso preGoodsCdNrs.Equals(ds.Tables("LMC790OUT").Rows(i).Item("GOODS_CD_NRS").ToString()) = False Then
            If String.IsNullOrEmpty(preGoodsCdNrs) OrElse preGoodsCdNrs.Equals(ds.Tables("LMC790OUT").Rows(i).Item("GOODS_CD_NRS").ToString()) = False Then
                '検索結果取得
                motoDs = MyBase.CallDAC(Me._Dac, "SelectGoodsDetails", motoDs)
            End If

            'Copyしたテーブルを返却テーブルにマージ
            tempDs.Tables("LMC790OUT").Merge(copyDs.Tables("LMC790OUT"))

            '引算開始フラグリセット
            minusflg = False

            Do While alctdNb > 0

                goodsDr = motoDs.Tables("GOODS_SIZE_WT").Select(String.Concat("IRISU = '", alctdNb, "'"), " GOODS_CD_NRS_EDA DESC")

                If goodsDr.Length = 0 Then

                    goodsDr = motoDs.Tables("GOODS_SIZE_WT").Select(String.Concat("IRISU <= '", alctdNb, "'"), " GOODS_CD_NRS_EDA DESC")

                    If goodsDr.Length > 0 Then

                        If minusflg = False AndAlso goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                            '返却DSに直接書込
                            tempDr = tempDs.Tables("LMC790OUT").Rows(tempDs.Tables("LMC790OUT").Rows.Count - 1)

                            '商品のサイズ情報取得
                            temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                            'サイズ情報設定
                            tempDr("ALCTD_NB") = goodsDr(0).Item("IRISU")
                            alctdNb = alctdNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                            tempDr("NET_WEIGHT") = temStr(0)
                            tempDr("GROSS_WEIGHT") = temStr(1)
                            tempDr("STD_CBM") = temStr(2)
                            tempDr("SIZE_L") = temStr(3)
                            tempDr("SIZE_W") = temStr(4)
                            tempDr("SIZE_H") = temStr(5)
                            tempDr("LOT_NO_NB") = Replace(tempDr("LOT_NO_NB").ToString(), ",", vbCrLf)

                            'ケース№情報初期化
                            tempDr("CASE_NO_FROM") = 0
                            tempDr("CASE_NO_TO") = 1

                            'サイズ情報登録済フラグ設定
                            goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON

                            '引算開始フラグ設定
                            minusflg = True

                        ElseIf goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                            '返却DSに追加する行の生成
                            wkNewRow = tempDs.Tables("LMC790OUT").NewRow()

                            '商品のサイズ情報取得
                            temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                            'サイズ情報設定
                            wkNewRow("ALCTD_NB") = goodsDr(0).Item("IRISU")
                            alctdNb = alctdNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                            wkNewRow("NET_WEIGHT") = temStr(0)
                            wkNewRow("GROSS_WEIGHT") = temStr(1)
                            wkNewRow("STD_CBM") = temStr(2)
                            wkNewRow("SIZE_L") = temStr(3)
                            wkNewRow("SIZE_W") = temStr(4)
                            wkNewRow("SIZE_H") = temStr(5)

                            '共通パラメータ設定
                            wkNewRow("RPT_ID") = copyDs.Tables("LMC790OUT").Rows(0).Item("RPT_ID")
                            wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC790OUT").Rows(0).Item("NRS_BR_CD")
                            wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC790OUT").Rows(0).Item("OUTKA_NO_L")
                            wkNewRow("JYUCYU_NM") = copyDs.Tables("LMC790OUT").Rows(0).Item("JYUCYU_NM")
                            wkNewRow("CRT_DATE") = copyDs.Tables("LMC790OUT").Rows(0).Item("CRT_DATE")
                            wkNewRow("CUST_ORD_NO") = copyDs.Tables("LMC790OUT").Rows(0).Item("CUST_ORD_NO")
                            wkNewRow("ARR_PLAN_DATE") = copyDs.Tables("LMC790OUT").Rows(0).Item("ARR_PLAN_DATE")
                            wkNewRow("DEST_NM") = copyDs.Tables("LMC790OUT").Rows(0).Item("DEST_NM")
                            wkNewRow("CASE_NO") = copyDs.Tables("LMC790OUT").Rows(0).Item("CASE_NO")
                            wkNewRow("GOODS_NM") = copyDs.Tables("LMC790OUT").Rows(0).Item("GOODS_NM")
                            wkNewRow("PKG_NB") = copyDs.Tables("LMC790OUT").Rows(0).Item("PKG_NB")
                            wkNewRow("PLT_PER_PKG_UT") = copyDs.Tables("LMC790OUT").Rows(0).Item("PLT_PER_PKG_UT")
                            wkNewRow("LOT_NO_NB") = Replace(copyDs.Tables("LMC790OUT").Rows(0).Item("LOT_NO_NB").ToString(), ",", vbCrLf)
                            wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_1")
                            wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_2")
                            wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_3")
                            wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_4")
                            wkNewRow("CASE_NO_FROM") = 0
                            wkNewRow("CASE_NO_TO") = 1
                            wkNewRow("CUST_NM_L") = copyDs.Tables("LMC790OUT").Rows(0).Item("CUST_NM_L")
                            wkNewRow("GOODS_CD_NRS") = copyDs.Tables("LMC790OUT").Rows(0).Item("GOODS_CD_NRS")

                            'サイズ情報登録済フラグ設定
                            goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON

                            '引算開始フラグ設定
                            minusflg = True

                            '返却DSに行追加
                            tempDs.Tables("LMC790OUT").Rows.Add(wkNewRow)

                        Else

                            '個数マイナス(規定入数分)
                            alctdNb = alctdNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))

                            '対象サイズを持つ入数のケース№ Toにプラス
                            tempDr = tempDs.Tables("LMC790OUT").Select(String.Concat("ALCTD_NB = '", goodsDr(0).Item("IRISU").ToString(), "'"))(0)
                            tempDr.Item("CASE_NO_TO") = Convert.ToInt32(tempDr.Item("CASE_NO_TO").ToString()) + 1

                        End If

                    Else
                        MyBase.SetMessage("E454", New String() {"サイズ登録", "梱包明細が出力", ds.Tables("LMC790OUT").Rows(i).Item("GOODS_NM").ToString()})
                        Return ds
                        Exit Function
                    End If

                Else

                    If minusflg = False AndAlso goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                        '返却DSに直接書込
                        tempDr = tempDs.Tables("LMC790OUT").Rows(tempDs.Tables("LMC790OUT").Rows.Count - 1)

                        '商品のサイズ情報取得
                        temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                        'サイズ情報設定
                        tempDr("ALCTD_NB") = goodsDr(0).Item("IRISU")
                        alctdNb = alctdNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                        tempDr("NET_WEIGHT") = temStr(0)
                        tempDr("GROSS_WEIGHT") = temStr(1)
                        tempDr("STD_CBM") = temStr(2)
                        tempDr("SIZE_L") = temStr(3)
                        tempDr("SIZE_W") = temStr(4)
                        tempDr("SIZE_H") = temStr(5)
                        tempDr("LOT_NO_NB") = Replace(tempDr("LOT_NO_NB").ToString(), ",", vbCrLf)

                        'ケース№情報初期化
                        tempDr("CASE_NO_FROM") = 0
                        tempDr("CASE_NO_TO") = 1

                        'サイズ情報登録済フラグ設定
                        goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON

                        '引算開始フラグ設定
                        minusflg = True

                    ElseIf goodsDr(0).Item("DISP_FLG").Equals(LMConst.FLG.OFF) = True Then

                        '返却DSに追加する行の生成
                        wkNewRow = tempDs.Tables("LMC790OUT").NewRow()

                        '商品のサイズ情報取得
                        temStr = goodsDr(0).Item("SIZE_WT").ToString().Trim().Split("-"c)

                        'サイズ情報設定
                        wkNewRow("ALCTD_NB") = goodsDr(0).Item("IRISU")
                        alctdNb = alctdNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))
                        wkNewRow("NET_WEIGHT") = temStr(0)
                        wkNewRow("GROSS_WEIGHT") = temStr(1)
                        wkNewRow("STD_CBM") = temStr(2)
                        wkNewRow("SIZE_L") = temStr(3)
                        wkNewRow("SIZE_W") = temStr(4)
                        wkNewRow("SIZE_H") = temStr(5)

                        '共通パラメータ設定
                        wkNewRow("RPT_ID") = copyDs.Tables("LMC790OUT").Rows(0).Item("RPT_ID")
                        wkNewRow("NRS_BR_CD") = copyDs.Tables("LMC790OUT").Rows(0).Item("NRS_BR_CD")
                        wkNewRow("OUTKA_NO_L") = copyDs.Tables("LMC790OUT").Rows(0).Item("OUTKA_NO_L")
                        wkNewRow("JYUCYU_NM") = copyDs.Tables("LMC790OUT").Rows(0).Item("JYUCYU_NM")
                        wkNewRow("CRT_DATE") = copyDs.Tables("LMC790OUT").Rows(0).Item("CRT_DATE")
                        wkNewRow("CUST_ORD_NO") = copyDs.Tables("LMC790OUT").Rows(0).Item("CUST_ORD_NO")
                        wkNewRow("ARR_PLAN_DATE") = copyDs.Tables("LMC790OUT").Rows(0).Item("ARR_PLAN_DATE")
                        wkNewRow("DEST_NM") = copyDs.Tables("LMC790OUT").Rows(0).Item("DEST_NM")
                        wkNewRow("CASE_NO") = copyDs.Tables("LMC790OUT").Rows(0).Item("CASE_NO")
                        wkNewRow("GOODS_NM") = copyDs.Tables("LMC790OUT").Rows(0).Item("GOODS_NM")
                        wkNewRow("PKG_NB") = copyDs.Tables("LMC790OUT").Rows(0).Item("PKG_NB")
                        wkNewRow("PLT_PER_PKG_UT") = copyDs.Tables("LMC790OUT").Rows(0).Item("PLT_PER_PKG_UT")
                        wkNewRow("LOT_NO_NB") = Replace(copyDs.Tables("LMC790OUT").Rows(0).Item("LOT_NO_NB").ToString(), ",", vbCrLf)
                        wkNewRow("MARK_INFO_1") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_1")
                        wkNewRow("MARK_INFO_2") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_2")
                        wkNewRow("MARK_INFO_3") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_3")
                        wkNewRow("MARK_INFO_4") = copyDs.Tables("LMC790OUT").Rows(0).Item("MARK_INFO_4")
                        wkNewRow("CASE_NO_FROM") = 0
                        wkNewRow("CASE_NO_TO") = 1
                        wkNewRow("CUST_NM_L") = copyDs.Tables("LMC790OUT").Rows(0).Item("CUST_NM_L")
                        wkNewRow("GOODS_CD_NRS") = copyDs.Tables("LMC790OUT").Rows(0).Item("GOODS_CD_NRS")

                        'サイズ情報登録済フラグ設定
                        goodsDr(0).Item("DISP_FLG") = LMConst.FLG.ON

                        '引算開始フラグ設定
                        minusflg = True

                        '返却DSに行追加
                        tempDs.Tables("LMC790OUT").Rows.Add(wkNewRow)

                    Else

                        '個数マイナス(規定入数分)
                        alctdNb = alctdNb - Convert.ToInt32(goodsDr(0).Item("IRISU"))

                        '対象サイズを持つ入数のケース№ Toにプラス
                        tempDr = tempDs.Tables("LMC790OUT").Select(String.Concat("ALCTD_NB = '", goodsDr(0).Item("IRISU").ToString(), "'"))(0)
                        tempDr.Item("CASE_NO_TO") = Convert.ToInt32(tempDr.Item("CASE_NO_TO").ToString()) + 1

                    End If

                End If

                preGoodsCdNrs = ds.Tables("LMC790OUT").Rows(i).Item("GOODS_CD_NRS").ToString()

            Loop

        Next

        '■□■□■□■□■□ ケース№の調整 ■□■□■□■□■□
        Dim maxCaseFromCnt As Integer = Convert.ToInt32(outDt.Rows(0).Item("CASE_NO_FROM"))
        Dim maxCaseToCnt As Integer = Convert.ToInt32(outDt.Rows(0).Item("CASE_NO_TO"))

        '==ケース№の調整==
        '現在のケース№の最大始数
        Dim cntFrom As Integer = maxCaseFromCnt
        '現在のケースNo.の最大終数
        Dim cntTo As Integer = maxCaseFromCnt - 1
        'サイズ毎の商品数(パレット、段、箱のそれぞれの数)
        Dim goodsCnt As Integer = 0
        For Each row As DataRow In tempDs.Tables("LMC790OUT").Rows

            '商品数の設定
            goodsCnt = Convert.ToInt32(row.Item("CASE_NO_TO").ToString())

            '開始の設定
            row.Item("CASE_NO_FROM") = cntFrom

            '終了の設定
            If cntTo >= maxCaseToCnt Then

                cntTo = maxCaseToCnt
                cntFrom = maxCaseToCnt

            Else

                cntTo = cntTo + goodsCnt

                If cntTo >= maxCaseToCnt = False Then
                    cntFrom = cntTo + 1
                End If

            End If

            row.Item("CASE_NO_TO") = cntTo

            Next

        '■□■□■□■□■□  合計行の作成  ■□■□■□■□■□
        Dim tmpMax As Integer = tempDs.Tables("LMC790OUT").Rows.Count - 1

        wkNewRow = tempDs.Tables("LMC790OUT").NewRow()

        wkNewRow("RPT_ID") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("RPT_ID")
        wkNewRow("NRS_BR_CD") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("NRS_BR_CD")
        wkNewRow("OUTKA_NO_L") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("OUTKA_NO_L")
        wkNewRow("JYUCYU_NM") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("JYUCYU_NM")
        wkNewRow("CRT_DATE") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("CRT_DATE")
        wkNewRow("CUST_ORD_NO") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("CUST_ORD_NO")
        wkNewRow("ARR_PLAN_DATE") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("ARR_PLAN_DATE")
        wkNewRow("DEST_NM") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("DEST_NM")
        wkNewRow("ALCTD_NB") = sumNb
        wkNewRow("LOT_NO_NB") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("LOT_NO_NB").ToString()
        wkNewRow("MARK_INFO_1") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("MARK_INFO_1")
        wkNewRow("MARK_INFO_2") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("MARK_INFO_2")
        wkNewRow("MARK_INFO_3") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("MARK_INFO_3")
        wkNewRow("MARK_INFO_4") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("MARK_INFO_4")
        wkNewRow("CASE_NO_FROM") = maxCaseFromCnt
        wkNewRow("CASE_NO_TO") = maxCaseToCnt
        'wkNewRow("CASE_NO_FROM") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("CASE_NO_FROM")
        'wkNewRow("CASE_NO_TO") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("CASE_NO_TO")
        wkNewRow("CUST_NM_L") = tempDs.Tables("LMC790OUT").Rows(tmpMax).Item("CUST_NM_L")
        tempDs.Tables("LMC790OUT").Rows.Add(wkNewRow)

        Return tempDs

    End Function

#End Region

#End Region

End Class
