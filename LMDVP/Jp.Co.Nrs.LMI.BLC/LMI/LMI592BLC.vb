' ===========================================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI592    : 運賃請求明細書（DIC在庫部課、負担課、納入日、納 入先別）
'                                                  1便・2便、車扱い・特便共通
'  作  成  者       :  kurihara
' ===========================================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI592BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI592BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI592DAC = New LMI592DAC()

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
    Private Const TABLE_NM_IN As String = "LMI592IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMI592OUT"

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
        Dim tableNmIn As String = LMI592BLC.TABLE_NM_IN
        Dim tableNmOut As String = LMI592BLC.TABLE_NM_OUT
        Dim tableNmRpt As String = LMI592BLC.TABLE_NM_M_RPT

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
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

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

        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, LMI592BLC.NRS_BR_CD, LMI592BLC.PTN_ID, LMI592BLC.PTN_CD, LMI592BLC.RPT_ID)

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
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMI592BLC.RPT_ID).ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMI592BLC.RPT_ID).ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item(LMI592BLC.NRS_BR_CD).ToString(), _
                                dr.Item(LMI592BLC.PTN_ID).ToString(), _
                                dr.Item(LMI592BLC.PTN_CD).ToString(), _
                                dr.Item(LMI592BLC.RPT_ID).ToString(), _
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

        'Select Case rptId
        '    Case ""
        'End Select

        'Return ds

        '伝票№のまとめ編集
        Dim outDt As DataTable = ds.Tables("LMI592OUT")  'データ取得
        Dim cloneDt As DataTable = outDt.Clone
        Dim wkCUST_REF_NO As String = String.Empty
        Dim wkSEIQ_GROUP_NO As String = String.Empty

        '正常に伝票のまとめができていなかったので修正 20120723 
        '伝票№のまとめ
        For Each row As DataRow In outDt.Rows
            'Sortがまとめ番号で連続していないので、修正。20120730
            If String.IsNullOrEmpty(row.Item("SEIQ_GROUP_NO").ToString) = True OrElse row.Item("UNSO_NO_L").ToString.Equals(row.Item("SEIQ_GROUP_NO").ToString) = True Then
                'まとめではない場合、まとめ処理を抜かす
                If String.IsNullOrEmpty(row.Item("SEIQ_GROUP_NO").ToString) = False Then
                    wkSEIQ_GROUP_NO = row.Item("SEIQ_GROUP_NO").ToString
                    'まとめレコードの取得
                    '(2012.08.31) 要望番号1304 修正 --- START ---
                    'For Each tmpRow As DataRow In outDt.Select(String.Concat("SEIQ_GROUP_NO = '", wkSEIQ_GROUP_NO, "'"))
                    '    wkCUST_REF_NO = String.Concat(tmpRow.Item("CUST_REF_NO").ToString, " ", wkCUST_REF_NO)
                    'Next

                    For Each tmpRow As DataRow In outDt.Select(String.Concat("SEIQ_GROUP_NO = '", wkSEIQ_GROUP_NO, "'"), "CUST_REF_NO ASC")
                        wkCUST_REF_NO = String.Concat(wkCUST_REF_NO, tmpRow.Item("CUST_REF_NO").ToString, " ")
                    Next
                    '(2012.08.31) 要望番号1304 修正 --- END ---

                    row.Item("CUST_REF_NO") = wkCUST_REF_NO
                End If

                cloneDt.ImportRow(row)

                wkSEIQ_GROUP_NO = String.Empty
                wkCUST_REF_NO = String.Empty
            End If

        Next
        'End 正常に伝票のまとめができていなかったので修正 20120723

        '元データクリア
        outDt.Clear()

        '伝票№集約データを元データへ
        For Each cloneRow As DataRow In cloneDt.Rows
            Dim wkNewRow As DataRow = ds.Tables("LMI592OUT").NewRow

            wkNewRow("RPT_ID") = cloneRow("RPT_ID")
            wkNewRow("NRS_BR_CD") = cloneRow("NRS_BR_CD")
            wkNewRow("NRS_BR_NM") = cloneRow("NRS_BR_NM")
            wkNewRow("CUST_CD_L") = cloneRow("CUST_CD_L")
            wkNewRow("CUST_NM_L") = cloneRow("CUST_NM_L")
            wkNewRow("CUST_CD_M") = cloneRow("CUST_CD_M")
            wkNewRow("CUST_NM_M") = cloneRow("CUST_NM_M")
            wkNewRow("ZBUKA_CD") = cloneRow("ZBUKA_CD")
            wkNewRow("ABUKA_CD") = cloneRow("ABUKA_CD")
            wkNewRow("DEST_CD") = cloneRow("DEST_CD")
            wkNewRow("DEST_NM") = cloneRow("DEST_NM")
            wkNewRow("ARR_PLAN_DATE") = cloneRow("ARR_PLAN_DATE")
            wkNewRow("SEIQ_KYORI") = cloneRow("SEIQ_KYORI")
            wkNewRow("SEIQ_WT") = cloneRow("SEIQ_WT")
            wkNewRow("DECI_UNCHIN") = cloneRow("DECI_UNCHIN")
            wkNewRow("CUST_REF_NO") = cloneRow("CUST_REF_NO")
            wkNewRow("UNSO_NO_L") = cloneRow("UNSO_NO_L")
            wkNewRow("SEIQ_GROUP_NO") = cloneRow("SEIQ_GROUP_NO")
            wkNewRow("BIN_KB") = cloneRow("BIN_KB")
            wkNewRow("BIN_NM") = cloneRow("BIN_NM")
            wkNewRow("F_DATE") = cloneRow("F_DATE")
            wkNewRow("T_DATE") = cloneRow("T_DATE")
            '(2012.07.12)要望番号1275 --- START ---
            wkNewRow("UNSOCO_CD") = cloneRow("UNSOCO_CD")
            wkNewRow("UNSOCO_BR_CD") = cloneRow("UNSOCO_BR_CD")
            wkNewRow("UNSOCO_NM") = cloneRow("UNSOCO_NM")
            wkNewRow("UNSOCO_BR_NM") = cloneRow("UNSOCO_BR_NM")
            wkNewRow("CUST_UNSO_RYAKU_NM") = cloneRow("CUST_UNSO_RYAKU_NM")
            '(2012.07.12)要望番号1275 ---  END  ---

            ds.Tables("LMI592OUT").Rows.Add(wkNewRow)
        Next

        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, LMI592BLC.ACTION_ID_SELECT_MPRT, ds)

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

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, LMI592BLC.ACTION_ID_SELECT_PRT_DATA, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return rtnDs

    End Function

#End Region

#End Region

End Class
