' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH552    : EDI出荷伝票(ゴードー溶剤用)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH552BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH552BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH552DAC = New LMH552DAC()

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
    Private Const TABLE_NM_IN As String = "LMH552IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMH552OUT"

    ''' <summary>
    ''' 帳票パターンアクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_MPRT As String = "SelectMPrintPattern"

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
        Dim tableNmIn As String = LMH552BLC.TABLE_NM_IN
        Dim tableNmOut As String = LMH552BLC.TABLE_NM_OUT
        Dim tableNmRpt As String = LMH552BLC.TABLE_NM_M_RPT

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

        'メッセージクリア
        MyBase.SetMessage(Nothing)

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            'MyBase.SetMessage("G039")
            MyBase.SetMessage("G052", New String() {"入出荷伝票"})
            MyBase.SetMessageStore(String.Empty, "G052", New String() {"入出荷伝票"})
            Return ds
        End If

        ds.Tables("H_EDI_PRINT").Clear()

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            'setDs = Me.SelectMPrt(setDs)
            setDs = Me.SelectMPrt(setDs, inTbl)

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
                ds = Me.SetHEdiPrint(dt.Rows(i), setDtOut.Rows(j), ds)
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, LMH552BLC.NRS_BR_CD, LMH552BLC.PTN_ID, LMH552BLC.PTN_CD, LMH552BLC.RPT_ID)

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
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMH552BLC.RPT_ID).ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMH552BLC.RPT_ID).ToString(), prtDs, ds)

            '帳票CSV出力
            comPrt.StartPrint(Me, _ 
                              dr.Item(LMH552BLC.NRS_BR_CD).ToString(), _
                              dr.Item(LMH552BLC.PTN_ID).ToString(), _
                              dr.Item(LMH552BLC.PTN_CD).ToString(), _
                              dr.Item(LMH552BLC.RPT_ID).ToString(), _
                              prtDs.Tables(tableNmOut), _
                              ds.Tables(LMConst.RD))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="prtDs"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal prtDs As DataSet, ByVal ds As DataSet) As DataSet

        Dim inDt As DataTable = ds.Tables("LMH552IN") 'データ取得
        Dim outDt As DataTable = prtDs.Tables("LMH552OUT") 'データ取得
        Dim max As Integer = outDt.Rows.Count - 1       '抽出データ数取得

        For i As Integer = 0 To max

            outDt.Rows(i).Item("SAI_PRINT_UMU") = inDt.Rows(0).Item("PRTFLG").ToString()

        Next

        Return prtDs

    End Function

    ''' <summary>
    ''' EDI印刷対象テーブルINデータ設定(LMH552)
    ''' </summary>
    ''' <param name="Indr"></param>
    ''' <param name="Outdr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHEdiPrint(ByVal Indr As DataRow, ByVal Outdr As DataRow, ByVal ds As DataSet) As DataSet

        ' 出荷伝票
        Dim PrmDt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH552DSのH_EDI_PRINTに値を設定
        PrmDr("NRS_BR_CD") = Indr.Item("NRS_BR_CD")
        PrmDr("EDI_CTL_NO") = Outdr.Item("EDI_CTL_NO")
        PrmDr("CUST_CD_L") = Indr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = Indr.Item("CUST_CD_M")
        PrmDr("INOUT_KB") = Indr.Item("INOUT_KB")
        PrmDr("PRINT_TP") = "01"
        PrmDr("DENPYO_NO") = Outdr.Item("DENP_NO")
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
        'Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = intbl.Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = intbl.Rows(0).Item("EDI_CTL_NO").ToString()
        Dim prtFlg As String = intbl.Rows(0).Item("PRTFLG").ToString()
        Dim outType As String = intbl.Rows(0).Item("OUTPUT_SHUBETU").ToString()

        ds = MyBase.CallDAC(Me._Dac, LMH552BLC.ACTION_ID_SELECT_MPRT, ds)

        'メッセージコードの設定ALL_PRINT
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            'MyBase.SetMessage("G021")                                                                  
            MyBase.SetMessage("G053", New String() {"　【入出荷伝票】　"})

            If prtFlg.Equals("1") = True Then
                '出力済の場合、エラーメッセージはExcel出力
                MyBase.SetMessageStore("00", "G053", New String() {"　【入出荷伝票】　"}, rowNo, "EDI管理番号", ediCtlNo)
            Else
                If outType.Equals(ALL_PRINT) = True Then
                    '一括印刷時、エラーメッセージはExcel出力
                    MyBase.SetMessageStore("00", "G053", New String() {"　【入出荷伝票】　"}, rowNo, "帳票名", "入出荷伝票")
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

        Return MyBase.CallDAC(Me._Dac, LMH552BLC.ACTION_ID_SELECT_PRT_DATA, ds)

    End Function

#End Region

#End Region

End Class
