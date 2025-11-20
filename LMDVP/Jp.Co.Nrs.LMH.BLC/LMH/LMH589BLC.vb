' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH589    : EDI納品書 日本合成化学
'  作  成  者       :  inoue
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH589BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH589BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH589DAC = New LMH589DAC()

#End Region 'Field

#Region "Const"

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

    ''' <summary>
    ''' 印刷種別
    ''' </summary>
    ''' <remarks></remarks>
    Private Class PRINT_TP

        ''' <summary>
        ''' EDI納品書 日本合成化学
        ''' </summary>
        ''' <remarks></remarks>
        Public Const EDI_DELIVERY_NOTE_NICHIGO  As String = "13"
    End Class


    ''' <summary>
    ''' 印刷出力テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const OUT_TABLE_NAME As String = LMH589DAC.TableNames.OUT_TABLE

    ''' <summary>
    ''' 一ページに表示できる最大詳細データ数
    ''' </summary>
    ''' <remarks></remarks>
    Public Const MAX_DETAIL_COUNT_BY_PAGE As Integer = 5


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
        Dim tableNmIn As String = LMH589DAC.TableNames.IN_TABLE
        Dim tableNmOut As String = LMH589DAC.TableNames.OUT_TABLE
        Dim tableNmRpt As String = LMH589DAC.TableNames.RPT

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

        MyBase.SetMessage(Nothing)

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G052", New String() {"EDI納品書"})
            MyBase.SetMessageStore(String.Empty, "G052", New String() {"EDI納品書"})
            Return ds
        End If

        ds.Tables(LMH589DAC.TableNames.EDI_PRINT).Clear()

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs, inTbl)

            If setDs.Tables(tableNmRpt).Rows.Count <> 0 Then

                '検索結果取得
                setDs = Me.SelectPrintData(setDs)

                '検索結果を詰め替え
                setDtOut = setDs.Tables(tableNmOut)
                setDtRpt = setDs.Tables(tableNmRpt)


                '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
                'OUT
                For j As Integer = 0 To setDtOut.Rows.Count - 1
                    dtOut.ImportRow(setDtOut.Rows(j))
                    ds = Me.SetHEdiPrint(dt.Rows(i), setDtOut.Rows(j), ds)
                Next

                'RPT(重複分を含めワーク用RPTテーブルに追加)
                For k As Integer = 0 To setDtRpt.Rows.Count - 1
                    workDtRpt.ImportRow(setDtRpt.Rows(k))
                Next

            End If


        Next


        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, LMH589BLC.NRS_BR_CD, LMH589BLC.PTN_ID, LMH589BLC.PTN_CD, LMH589BLC.RPT_ID)

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

        ' 印刷データソート順
        Dim sortOut As String = "OUTKA_DENP_NO, OUTKA_DENP_DTL_NO"

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
            prtDs = comPrt.CallDataSet(dtOut, dr.Item(LMH589BLC.RPT_ID).ToString(), sortOut)

            ' 帳票データ加工
            prtDs = Me.EditPrint(prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, _
                              dr.Item(LMH589BLC.NRS_BR_CD).ToString(), _
                              dr.Item(LMH589BLC.PTN_ID).ToString(), _
                              dr.Item(LMH589BLC.PTN_CD).ToString(), _
                              dr.Item(LMH589BLC.RPT_ID).ToString(), _
                              prtDs.Tables(tableNmOut), _
                              ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrint(ByVal ds As DataSet) As DataSet

        Dim newTable As DataTable = ds.Tables(LMH589DAC.TableNames.OUT_TABLE).Clone
        Dim newRow As DataRow = Nothing


        Dim pageNumbers As Dictionary(Of String, Integer) = New Dictionary(Of String, Integer)

        '印刷フォーマット数だけ印刷行を増やす(出荷指図書,送り状,荷物送附依頼書,荷物受取証)
        Dim pageNumber As Integer = 1
        For Each row As DataRow In ds.Tables(LMH589DAC.TableNames.OUT_TABLE).Select() _
            .OrderBy(Function(s) s.Item(LMH589DAC.OUT_TABLE_COLUMNS.EDI_CTL_NO)) _
            .ThenBy(Function(s) s.Item(LMH589DAC.OUT_TABLE_COLUMNS.EDI_CTL_NO_CHU))

            Dim values As Object() = row.ItemArray()

            For i As Integer = LMH589DAC.NICHIGO_PRINT_TYPE.SHIPMENT_INSTRUCTION To LMH589DAC.NICHIGO_PRINT_TYPE.RECEIPT
                newRow = newTable.NewRow()
                newRow.ItemArray = values
                newRow.Item(LMH589DAC.OUT_TABLE_COLUMNS.TYPE) = i
                newRow.Item(LMH589DAC.OUT_TABLE_COLUMNS.PAGE_NO) = pageNumber
                newTable.Rows.Add(newRow)
            Next
        Next

        ds.Tables(LMH589DAC.TableNames.OUT_TABLE).Clear()

        ' 並べ替え
        For Each row As DataRow In newTable.Select() _
            .OrderBy(Function(s) s.Item(LMH589DAC.OUT_TABLE_COLUMNS.EDI_CTL_NO)) _
            .ThenBy(Function(s) s.Item(LMH589DAC.OUT_TABLE_COLUMNS.OUTKA_DENP_NO)) _
            .ThenBy(Function(s) s.Item(LMH589DAC.OUT_TABLE_COLUMNS.TYPE)) _
            .ThenBy(Function(s) s.Item(LMH589DAC.OUT_TABLE_COLUMNS.OUTKA_DENP_DTL_NO))


            ds.Tables(LMH589DAC.TableNames.OUT_TABLE).ImportRow(row)
        Next


        Return ds


    End Function

    ''' <summary>
    '''  ＥＤＩ印刷対象テーブルINデータ設定(LMH589)
    ''' </summary>
    ''' <param name="inRow"></param>
    ''' <param name="outRow"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHEdiPrint(ByVal inRow As DataRow, ByVal outRow As DataRow, ByVal ds As DataSet) As DataSet

        ' 受信一覧表
        Dim table As DataTable = ds.Tables(LMH589DAC.TableNames.EDI_PRINT)


        If (table.Select.Where(Function(s) _
                                   s.Item("NRS_BR_CD").Equals(inRow.Item("NRS_BR_CD")) AndAlso _
                                   s.Item("EDI_CTL_NO").Equals(outRow.Item("EDI_CTL_NO")) AndAlso _
                                   s.Item("INOUT_KB").Equals(inRow.Item("INOUT_KB")) AndAlso _
                                   s.Item("PRINT_TP").Equals(PRINT_TP.EDI_DELIVERY_NOTE_NICHIGO) AndAlso _
                                   s.Item("DENPYO_NO").Equals(outRow.Item("OUTKA_DENP_NO"))).Count = 0) Then


            Dim newRow As DataRow = table.NewRow()

            newRow("NRS_BR_CD") = inRow.Item("NRS_BR_CD")
            newRow("EDI_CTL_NO") = inRow.Item("EDI_CTL_NO")
            newRow("INOUT_KB") = inRow.Item("INOUT_KB")
            newRow("CUST_CD_L") = inRow.Item("CUST_CD_L")
            newRow("CUST_CD_M") = inRow.Item("CUST_CD_M")
            newRow("PRINT_TP") = PRINT_TP.EDI_DELIVERY_NOTE_NICHIGO
            newRow("DENPYO_NO") = outRow.Item("OUTKA_DENP_NO")

            table.Rows.Add(newRow)
        End If

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

        ds = MyBase.CallDAC(Me._Dac, LMH589DAC.Functions.SELECT_MPRT, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then            
            If prtFlg.Equals(LMConst.FLG.ON) = True Then
                '出力済の場合、エラーメッセージはExcel出力
                MyBase.SetMessageStore("00", "G053", New String() {"　【EDI納品書】　"}, rowNo, "EDI管理番号", ediCtlNo)
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

        Return MyBase.CallDAC(Me._Dac, LMH589DAC.Functions.SELECT_PRT_DATA, ds)

    End Function

#End Region

#End Region

End Class
