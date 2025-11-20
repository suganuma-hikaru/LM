' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH588    : テルモ仕切書(千葉)
'  作  成  者       :  篠田
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.Text

''' <summary>
''' LMH588BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH588BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH588DAC = New LMH588DAC()

#End Region 'Field

#Region "Const"

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMH588IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMH588OUT"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT_TEMP As String = "LMH588OUT_TEMP"

    ''' <summary>
    ''' 帳票パターンアクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_MPRT As String = "SelectMPrt"

    ''' <summary>
    ''' 印刷データ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_PRT_DATA As String = "SelectPrintData"

    ''' <summary>
    ''' RPTテーブルのカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD As String = "NRS_BR_CD"
    Private Const PTN_ID As String = "PTN_ID"
    Private Const PTN_CD As String = "PTN_CD"
    Private Const RPT_ID As String = "RPT_ID"

    ''' <summary>
    ''' 印刷種別：一括変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ALL_PRINT As String = "99"

#End Region 'Const

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
        Dim tableNmIn As String = LMH588BLC.TABLE_NM_IN
        Dim tableNmOut As String = LMH588BLC.TABLE_NM_OUT
        Dim tableNmOut_temp As String = LMH588BLC.TABLE_NM_OUT_TEMP
        Dim tableNmRpt As String = LMH588BLC.TABLE_NM_M_RPT

        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtOutTemp As DataTable = ds.Tables(tableNmOut_temp)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim printDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

        '【Notes】№1007/1008対応：メッセージクリア
        MyBase.SetMessage(Nothing)

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G052", New String() {"EDI納品書"})
            MyBase.SetMessageStore(String.Empty, "G052", New String() {"EDI納品書"})
            Return ds
        End If

        ds.Tables("H_EDI_PRINT").Clear()

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs, inTbl)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut_temp)
            setDtRpt = setDs.Tables(tableNmRpt)

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            'OUT
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOutTemp.ImportRow(setDtOut.Rows(j))
                ds = Me.SetHEdiPrint(dt.Rows(i), setDtOut.Rows(j), ds)
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, LMH588BLC.NRS_BR_CD, LMH588BLC.PTN_ID, LMH588BLC.PTN_CD, LMH588BLC.RPT_ID)

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
        For Each dr As DataRow In ds.Tables(tableNmRpt).Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            '指定したレポートIDのデータを抽出する。
            '(2012.06.11) 要望番号1102 並び換えしたDateTableを設定 --- START ---
            'prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMH584BLC.RPT_ID).ToString())
            prtDs = comPrt.CallDataSet(dtOutTemp, dr.Item(LMH588BLC.RPT_ID).ToString())
            '(2012.06.11) 要望番号1102 並び換えしたDateTableを設定 ---  END  ---

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMH588BLC.RPT_ID).ToString(), prtDs, printDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item(LMH588BLC.NRS_BR_CD).ToString(), _
                                dr.Item(LMH588BLC.PTN_ID).ToString(), _
                                dr.Item(LMH588BLC.PTN_CD).ToString(), _
                                dr.Item(LMH588BLC.RPT_ID).ToString(), _
                                printDs.Tables(tableNmOut), _
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
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet, ByVal ds2 As DataSet) As DataSet

        Dim PrmDt As DataTable = ds2.Tables(LMH588BLC.TABLE_NM_OUT)

        Dim wkCnt As Integer = 1
        Dim meisaiStr As String = String.Empty
        Dim PrmDr As DataRow = Nothing
        Dim cnt As Integer = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Rows.Count - 1
        'Dim rptDr As DataRow() = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Select(Nothing, "EDI_CTL_NO, EDI_CTL_NO_CHU,PRTFLG")

        For i As Integer = 0 To cnt


            Dim RcvData_Temp As String = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Rows(i).Item("RCV_DATA").ToString()
            Dim PrintID As String = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Rows(i).Item("PRINT_ID").ToString()

            If PrintID = "1" Then

                If wkCnt <> 1 Then
                    PrmDt.Rows.Add(PrmDr)
                End If

                PrmDr = PrmDt.NewRow()
                ClrOutRow(PrmDr)
                wkCnt = 1
                PrmDr("RPT_ID") = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Rows(i).Item("RPT_ID")
                PrmDr("CRT_DATE") = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Rows(i).Item("CRT_DATE")
                PrmDr("NRS_BR_CD") = ds.Tables(LMH588BLC.TABLE_NM_OUT_TEMP).Rows(i).Item("NRS_BR_CD")
                PrmDr("ID1_MESSAGE") = Trim(SubstringByte(RcvData_Temp, 1, 10))
                PrmDr("ID1_SHITEN_NM") = Trim(SubstringByte(RcvData_Temp, 11, 10))
                PrmDr("ID1_SHOGO_NM") = Trim(SubstringByte(RcvData_Temp, 21, 20))
                PrmDr("ID1_SHOGO_CD") = Trim(SubstringByte(RcvData_Temp, 41, 4))
                PrmDr("ID1_HAKKO_DATE") = Trim(SubstringByte(RcvData_Temp, 45, 6))
                PrmDr("ID1_SHIKIRI_NO") = Trim(SubstringByte(RcvData_Temp, 51, 9))
                PrmDr("ID1_TORIHIKI_KB") = Trim(SubstringByte(RcvData_Temp, 60, 3))
                PrmDr("ID1_DATA_CD") = Trim(SubstringByte(RcvData_Temp, 72, 4))
                PrmDr("ID1_ORD_NO") = Trim(SubstringByte(RcvData_Temp, 69, 3))
                PrmDr("ID1_MAKER_CD") = Trim(SubstringByte(RcvData_Temp, 66, 3))
                PrmDr("ID1_SEIBUTSU_KB") = Trim(SubstringByte(RcvData_Temp, 76, 10))

            ElseIf PrintID = "6" Then

                PrmDr("ID6_SOUHIN_NM") = Trim(SubstringByte(RcvData_Temp, 1, 20))
                PrmDr("ID6_SOUHIN_AD") = Trim(SubstringByte(RcvData_Temp, 21, 30))
                PrmDr("ID6_SOUHIN_CD") = Trim(SubstringByte(RcvData_Temp, 51, 8))
                PrmDr("ID6_SASIZU_NM") = Trim(SubstringByte(RcvData_Temp, 59, 20))
                PrmDr("ID6_SASIZU_CD") = Trim(SubstringByte(RcvData_Temp, 79, 8))

            ElseIf PrintID = "5" Then

                PrmDr("ID5_COMMENT") = Trim(SubstringByte(RcvData_Temp, 1, 56))
                PrmDr("ID5_TOTAL_KINGAKU") = Trim(SubstringByte(RcvData_Temp, 62, 9))
                PrmDr("ID5_TOTAL_TAX") = Trim(SubstringByte(RcvData_Temp, 71, 8))
                PrmDr("ID5_SOUKEI") = Trim(SubstringByte(RcvData_Temp, 79, 9))

            ElseIf PrintID = "7" Then

                PrmDr("ID7_BARA_MESSAGE") = Trim(SubstringByte(RcvData_Temp, 1, 70))

            ElseIf PrintID = "8" Then

                PrmDr("ID8_COMMENT") = Trim(SubstringByte(RcvData_Temp, 1, 70))
                PrmDr("ID8_TAX_COMMENT") = Trim(SubstringByte(RcvData_Temp, 71, 19))

            ElseIf PrintID = "2" OrElse _
                PrintID = "3" OrElse _
                PrintID = "4" Then

                'ID = 2 の時は、明細行の下段に表示する
                If PrintID = "2" AndAlso (wkCnt Mod 2 = 1) Then
                    wkCnt = wkCnt + 1
                End If

                meisaiStr = String.Empty

                Select Case wkCnt
                    Case 1
                        meisaiStr = "A0_"
                    Case 2
                        meisaiStr = "A1_"
                    Case 3
                        meisaiStr = "B0_"
                    Case 4
                        meisaiStr = "B1_"
                    Case 5
                        meisaiStr = "C0_"
                    Case 6
                        meisaiStr = "C1_"
                    Case 7
                        meisaiStr = "D0_"
                    Case 8
                        meisaiStr = "D1_"
                    Case 9
                        meisaiStr = "E0_"
                    Case 10
                        meisaiStr = "E1_"
                    Case Else

                End Select

                '商品コード（ID=2のみ有効）
                PrmDr(meisaiStr + "ITEM_CD") = IIf(PrintID = "2", Trim(SubstringByte(RcvData_Temp, 1, 6)), "")
                '品名（ID=2のみ有効）
                PrmDr(meisaiStr + "HINMEI") = IIf(PrintID = "2", Trim(SubstringByte(RcvData_Temp, 7, 16)), "")
                '規格（ID=2のみ有効）
                PrmDr(meisaiStr + "KIKAKU") = IIf(PrintID = "2", Trim(SubstringByte(RcvData_Temp, 23, 12)), "")
                '容量（ID=4以外有効）
                PrmDr(meisaiStr + "YORYO") = IIf(PrintID = "4", "", Trim(SubstringByte(RcvData_Temp, 35, 10)))
                '数量（ID=4以外有効）
                PrmDr(meisaiStr + "SURYO") = IIf(PrintID = "4", "", Trim(SubstringByte(RcvData_Temp, 45, 6)))
                '単価（ID=4以外有効）
                PrmDr(meisaiStr + "TANKA") = IIf(PrintID = "4", "", Trim(SubstringByte(RcvData_Temp, 51, 9)))
                '金額（ID=4以外有効）
                PrmDr(meisaiStr + "KINGAKU") = IIf(PrintID = "4", "", Trim(SubstringByte(RcvData_Temp, 61, 9)))
                '記帳ID（ID=4以外有効）
                PrmDr(meisaiStr + "KICHO_ID") = IIf(PrintID = "4", "", Trim(SubstringByte(RcvData_Temp, 70, 1)))
                '本ロット(ID=2.3.4全てに有効)
                PrmDr(meisaiStr + "HON_LOT") = Trim(SubstringByte(RcvData_Temp, 80, 10))
                'バラメッセージ(ID=2.3.4全てに有効)
                PrmDr(meisaiStr + "BARA_MSG") = Trim(SubstringByte(RcvData_Temp, 90, 8))
                '消費税非課税ID（ID=2のみ有効）
                PrmDr(meisaiStr + "HIKAZEI_ID") = IIf(PrintID = "2", Trim(SubstringByte(RcvData_Temp, 98, 1)), "")

                wkCnt = wkCnt + 1

            End If

            If i = cnt Then
                '最終行は最終オーダーを入れる
                PrmDt.Rows.Add(PrmDr)
            End If
        Next

        Return ds2

    End Function

    ''' <summary>
    ''' データロウの初期化をおこなう。
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Sub ClrOutRow(ByRef dr As DataRow)

        dr("RPT_ID") = ""
        dr("CRT_DATE") = ""
        dr("NRS_BR_CD") = ""
        dr("ID1_MESSAGE") = ""
        dr("ID1_SHITEN_NM") = ""
        dr("ID1_SHOGO_NM") = ""
        dr("ID1_SHOGO_CD") = ""
        dr("ID1_HAKKO_DATE") = ""
        dr("ID1_SHIKIRI_NO") = ""
        dr("ID1_TORIHIKI_KB") = ""
        dr("ID1_DATA_CD") = ""
        dr("ID1_ORD_NO") = ""
        dr("ID1_MAKER_CD") = ""
        dr("ID1_SEIBUTSU_KB") = ""
        dr("ID6_SOUHIN_NM") = ""
        dr("ID6_SOUHIN_AD") = ""
        dr("ID6_SOUHIN_CD") = ""
        dr("ID6_SASIZU_NM") = ""
        dr("ID6_SASIZU_CD") = ""
        dr("ID5_COMMENT") = ""
        dr("ID5_TOTAL_KINGAKU") = ""
        dr("ID5_TOTAL_TAX") = ""
        dr("ID5_SOUKEI") = ""
        dr("ID7_BARA_MESSAGE") = ""
        dr("ID8_COMMENT") = ""
        dr("ID8_TAX_COMMENT") = ""
        dr("A0_ITEM_CD") = ""
        dr("A0_HINMEI") = ""
        dr("A0_KIKAKU") = ""
        dr("A0_YORYO") = ""
        dr("A0_SURYO") = ""
        dr("A0_TANKA") = ""
        dr("A0_KINGAKU") = ""
        dr("A0_KICHO_ID") = ""
        dr("A0_HON_LOT") = ""
        dr("A0_BARA_MSG") = ""
        dr("A0_HIKAZEI_ID") = ""
        
        dr("A1_ITEM_CD") = ""
        dr("A1_HINMEI") = ""
        dr("A1_KIKAKU") = ""
        dr("A1_YORYO") = ""
        dr("A1_SURYO") = ""
        dr("A1_TANKA") = ""
        dr("A1_KINGAKU") = ""
        dr("A1_KICHO_ID") = ""
        dr("A1_HON_LOT") = ""
        dr("A1_BARA_MSG") = ""
        dr("A1_HIKAZEI_ID") = ""

        dr("B0_ITEM_CD") = ""
        dr("B0_HINMEI") = ""
        dr("B0_KIKAKU") = ""
        dr("B0_YORYO") = ""
        dr("B0_SURYO") = ""
        dr("B0_TANKA") = ""
        dr("B0_KINGAKU") = ""
        dr("B0_KICHO_ID") = ""
        dr("B0_HON_LOT") = ""
        dr("B0_BARA_MSG") = ""
        dr("B0_HIKAZEI_ID") = ""

        dr("B1_ITEM_CD") = ""
        dr("B1_HINMEI") = ""
        dr("B1_KIKAKU") = ""
        dr("B1_YORYO") = ""
        dr("B1_SURYO") = ""
        dr("B1_TANKA") = ""
        dr("B1_KINGAKU") = ""
        dr("B1_KICHO_ID") = ""
        dr("B1_HON_LOT") = ""
        dr("B1_BARA_MSG") = ""
        dr("B1_HIKAZEI_ID") = ""

        dr("C0_ITEM_CD") = ""
        dr("C0_HINMEI") = ""
        dr("C0_KIKAKU") = ""
        dr("C0_YORYO") = ""
        dr("C0_SURYO") = ""
        dr("C0_TANKA") = ""
        dr("C0_KINGAKU") = ""
        dr("C0_KICHO_ID") = ""
        dr("C0_HON_LOT") = ""
        dr("C0_BARA_MSG") = ""
        dr("C0_HIKAZEI_ID") = ""

        dr("C1_ITEM_CD") = ""
        dr("C1_HINMEI") = ""
        dr("C1_KIKAKU") = ""
        dr("C1_YORYO") = ""
        dr("C1_SURYO") = ""
        dr("C1_TANKA") = ""
        dr("C1_KINGAKU") = ""
        dr("C1_KICHO_ID") = ""
        dr("C1_HON_LOT") = ""
        dr("C1_BARA_MSG") = ""
        dr("C1_HIKAZEI_ID") = ""

        dr("D0_ITEM_CD") = ""
        dr("D0_HINMEI") = ""
        dr("D0_KIKAKU") = ""
        dr("D0_YORYO") = ""
        dr("D0_SURYO") = ""
        dr("D0_TANKA") = ""
        dr("D0_KINGAKU") = ""
        dr("D0_KICHO_ID") = ""
        dr("D0_HON_LOT") = ""
        dr("D0_BARA_MSG") = ""
        dr("D0_HIKAZEI_ID") = ""

        dr("D1_ITEM_CD") = ""
        dr("D1_HINMEI") = ""
        dr("D1_KIKAKU") = ""
        dr("D1_YORYO") = ""
        dr("D1_SURYO") = ""
        dr("D1_TANKA") = ""
        dr("D1_KINGAKU") = ""
        dr("D1_KICHO_ID") = ""
        dr("D1_HON_LOT") = ""
        dr("D1_BARA_MSG") = ""
        dr("D1_HIKAZEI_ID") = ""

        dr("E0_ITEM_CD") = ""
        dr("E0_HINMEI") = ""
        dr("E0_KIKAKU") = ""
        dr("E0_YORYO") = ""
        dr("E0_SURYO") = ""
        dr("E0_TANKA") = ""
        dr("E0_KINGAKU") = ""
        dr("E0_KICHO_ID") = ""
        dr("E0_HON_LOT") = ""
        dr("E0_BARA_MSG") = ""
        dr("E0_HIKAZEI_ID") = ""

        dr("E1_ITEM_CD") = ""
        dr("E1_HINMEI") = ""
        dr("E1_KIKAKU") = ""
        dr("E1_YORYO") = ""
        dr("E1_SURYO") = ""
        dr("E1_TANKA") = ""
        dr("E1_KINGAKU") = ""
        dr("E1_KICHO_ID") = ""
        dr("E1_HON_LOT") = ""
        dr("E1_BARA_MSG") = ""
        dr("E1_HIKAZEI_ID") = ""

    End Sub

    ''' <summary>
    '''  ＥＤＩ印刷対象テーブルINデータ設定(LMH588)
    ''' </summary>
    ''' <param name="Indr"></param>
    ''' <param name="Outdr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHEdiPrint(ByVal Indr As DataRow, ByVal Outdr As DataRow, ByVal ds As DataSet) As DataSet

        ' 受信一覧表
        Dim PrmDt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH588DSのH_EDI_PRINTに値を設定
        PrmDr("NRS_BR_CD") = Indr.Item("NRS_BR_CD")
        PrmDr("EDI_CTL_NO") = Outdr.Item("EDI_CTL_NO") '★2013.02.07修正
        PrmDr("INOUT_KB") = Indr.Item("INOUT_KB")
        PrmDr("CUST_CD_L") = Indr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = Indr.Item("CUST_CD_M")
        PrmDr("PRINT_TP") = "12"
        PrmDr("DENPYO_NO") = Outdr.Item("SIKIRI_NO") '★2013.02.07修正
        'PrmDr("DENPYO_NO") = Indr.Item("DENPYO_NO")   '★2013.02.07削除
        PrmDt.Rows.Add(PrmDr)

        Return ds

    End Function


    ''' <summary>
    ''' 出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>

    Private Function SelectMPrt(ByVal ds As DataSet, ByVal intbl As DataTable) As DataSet

        Dim rowNo As String = intbl.Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = intbl.Rows(0).Item("EDI_CTL_NO").ToString()
        Dim prtFlg As String = intbl.Rows(0).Item("PRTFLG").ToString()
        Dim outType As String = intbl.Rows(0).Item("OUTPUT_SHUBETU").ToString()

        ds = MyBase.CallDAC(Me._Dac, LMH588BLC.ACTION_ID_SELECT_MPRT, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G053", New String() {"　【EDI納品書】　"})

            If prtFlg.Equals("1") = True Then
                '出力済の場合、エラーメッセージはExcel出力
                MyBase.SetMessageStore("00", "G053", New String() {"　【仕切書】　"}, rowNo, "EDI管理番号", ediCtlNo)
            Else
                If outType.Equals(ALL_PRINT) = True Then
                    '一括印刷時、エラーメッセージはExcel出力
                    MyBase.SetMessageStore("00", "G053", New String() {"　【EDI納品書】　"}, rowNo, "帳票名", "受信一覧表")
                End If
            End If
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

        Return MyBase.CallDAC(Me._Dac, LMH588BLC.ACTION_ID_SELECT_PRT_DATA, ds)

    End Function

#End Region

#Region "SubstringByte"
    ''' <summary>
    ''' 文字列からバイト数を指定して部分文字列を取得する。
    ''' </summary>
    ''' <param name="value">対象文字列。</param>
    ''' <param name="startIndex">開始位置。（バイト数）</param>
    ''' <param name="length">長さ。（バイト数）</param>
    ''' <returns>部分文字列。</returns>
    ''' <remarks>文字列は <c>Shift_JIS</c> でエンコーディングして処理を行います。</remarks>
    Private Function SubstringByte(ByVal value As String, ByVal startIndex As Integer, ByVal length As Integer) As String
        Dim sjisEnc As Encoding = Encoding.GetEncoding("Shift_JIS")
        Dim byteArray() As Byte = sjisEnc.GetBytes(value)

        If byteArray.Length < startIndex + 1 Then
            Return ""
        End If

        If byteArray.Length < startIndex + length Then
            length = byteArray.Length - startIndex
        End If

        Dim cut As String = sjisEnc.GetString(byteArray, startIndex, length)

        ' 最初の文字が全角の途中で切れていた場合はカット
        Dim left As String = sjisEnc.GetString(byteArray, 0, startIndex + 1)
        Dim first As Char = value(left.Length - 1)
        If 0 < cut.Length AndAlso Not first = cut(0) Then
            cut = cut.Substring(1)
        End If

        ' 最後の文字が全角の途中で切れていた場合はカット
        left = sjisEnc.GetString(byteArray, 0, startIndex + length)

        Dim last As Char = value(left.Length - 1)
        If 0 < cut.Length AndAlso Not last = cut(cut.Length - 1) Then
            cut = cut.Substring(0, cut.Length - 1)
        End If

        Return cut
    End Function

#End Region

#End Region

End Class
