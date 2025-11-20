' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH581    : EDI納品書(埼玉 BP オートバックス)
'  作  成  者       :  渡部　剛
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH581BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH581BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH581DAC = New LMH581DAC()

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
    Private Const TABLE_NM_IN As String = "LMH581IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMH581OUT"

    '2013.06.17 追加START
    ''' <summary>
    ''' CNTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_CNT As String = "CNT_TBL"
    '2013.06.17 追加END

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

    '2013.06.17 追加START
    ''' <summary>
    ''' 印刷件数取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_OUTPUTCNT As String = "SelectOutputData"
    '2013.06.17 追加START

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
        Dim tableNmIn As String = LMH581BLC.TABLE_NM_IN
        Dim tableNmOut As String = LMH581BLC.TABLE_NM_OUT
        Dim tableNmRpt As String = LMH581BLC.TABLE_NM_M_RPT
        '2013.06.17 追加START
        Dim tableNmCnt As String = LMH581BLC.TABLE_NM_CNT
        '2013.06.17 追加END

        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)
        '2013.06.17 追加START
        Dim dtCnt As DataTable = ds.Tables(tableNmCnt)
        '2013.06.17 追加END

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable
        '2013.06.17 追加START
        Dim setDtCnt As DataTable
        '2013.06.17 追加END

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

            '(2013.02.08)要望番号1822 メッセージ制御 -- START-- 
            'メッセージの判定
            'If MyBase.IsMessageExist() = True Then
            '    Return ds
            'End If

            If setDs.Tables(tableNmRpt).Rows.Count <> 0 Then

                '検索結果取得
                setDs = Me.SelectPrintData(setDs)

                '2013.06.17 追加START
                '出力件数取得
                setDs = Me.SelectOutputData(setDs)
                '2013.06.17 追加START

                '検索結果を詰め替え
                setDtOut = setDs.Tables(tableNmOut)
                setDtRpt = setDs.Tables(tableNmRpt)
                '2013.06.17 追加START
                setDtCnt = setDs.Tables(tableNmCnt)
                '2013.06.17 追加END

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

                '2013.06.17 追加START
                For l As Integer = 0 To setDtCnt.Rows.Count - 1
                    dtCnt.ImportRow(setDtCnt.Rows(l))
                Next
                '2013.06.17 追加END

            End If
            '(2013.02.08)要望番号1822 メッセージ制御 --  END -- 

        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, LMH581BLC.NRS_BR_CD, LMH581BLC.PTN_ID, LMH581BLC.PTN_CD, LMH581BLC.RPT_ID)

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
            'prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMH581BLC.RPT_ID).ToString())
            prtDs = comPrt.CallDataSet(dtOut, dr.Item(LMH581BLC.RPT_ID).ToString())
            '(2012.06.11) 要望番号1102 並び換えしたDateTableを設定 ---  END  ---

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMH581BLC.RPT_ID).ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, _
                              dr.Item(LMH581BLC.NRS_BR_CD).ToString(), _
                              dr.Item(LMH581BLC.PTN_ID).ToString(), _
                              dr.Item(LMH581BLC.PTN_CD).ToString(), _
                              dr.Item(LMH581BLC.RPT_ID).ToString(), _
                              prtDs.Tables(tableNmOut), _
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

        '要望管理1983 Start
        Dim outDt As DataTable = ds.Tables("LMH581OUT") 'データ取得
        Dim max As Integer = outDt.Rows.Count - 1       '抽出データ数取得

        ''2013.06.17 追加START
        'Dim preGyoNo As Integer = 1
        'Dim sumCnt As Integer = 0
        ''2013.06.17 追加END

        Dim drs As DataRow()

        For i As Integer = 0 To max

            If String.IsNullOrEmpty(outDt.Rows(i).Item("HED_OUTKA_PLAN_DATE").ToString) = True Then
                drs = outDt.Select(String.Concat("HED_CUST_ORD_NO ='", outDt.Rows(i).Item("HED_CUST_ORD_NO").ToString _
                                                                  , "' AND HED_OUTKA_PLAN_DATE <> ''"))
                If drs.Count <> 0 Then
                    outDt.Rows(i).Item("HED_OUTKA_PLAN_DATE") = drs(0).Item("HED_OUTKA_PLAN_DATE").ToString()
                End If

            End If

        Next

        '要望管理1983 End
        Return ds

    End Function

    ''' <summary>
    '''  ＥＤＩ印刷対象テーブルINデータ設定(LMH581)
    ''' </summary>
    ''' <param name="Indr"></param>
    ''' <param name="Outdr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHEdiPrint(ByVal Indr As DataRow, ByVal Outdr As DataRow, ByVal ds As DataSet) As DataSet

        ' 受信一覧表
        Dim PrmDt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH581DSのH_EDI_PRINTに値を設定
        PrmDr("NRS_BR_CD") = Indr.Item("NRS_BR_CD")
        PrmDr("EDI_CTL_NO") = Outdr.Item("EDI_CTL_NO")
        PrmDr("INOUT_KB") = Indr.Item("INOUT_KB")
        PrmDr("CUST_CD_L") = Indr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = Indr.Item("CUST_CD_M")
        PrmDr("INOUT_KB") = Indr.Item("INOUT_KB")
        PrmDr("PRINT_TP") = "07"
        PrmDr("DENPYO_NO") = Outdr.Item("DENPYO_NO")
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

        ds = MyBase.CallDAC(Me._Dac, LMH581BLC.ACTION_ID_SELECT_MPRT, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G053", New String() {"　【EDI納品書】　"})

            If prtFlg.Equals("1") = True Then
                '出力済の場合、エラーメッセージはExcel出力
                MyBase.SetMessageStore("00", "G053", New String() {"　【EDI納品書】　"}, rowNo, "EDI管理番号", ediCtlNo)
            Else
                If outType.Equals(ALL_PRINT) = True Then
                    '一括印刷時、エラーメッセージはExcel出力
                    MyBase.SetMessageStore("00", "G053", New String() {"　【EDI納品書】　"}, rowNo, "帳票名", "EDI納品書 BPｵｰﾄﾊﾞｯｸｽ")
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

        Return MyBase.CallDAC(Me._Dac, LMH581BLC.ACTION_ID_SELECT_PRT_DATA, ds)

    End Function

    ''' <summary>
    ''' 印刷出力枚数検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOutputData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, LMH581BLC.ACTION_ID_SELECT_OUTPUTCNT, ds)

    End Function

#End Region

#End Region

End Class
