' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF600BLC : 支払運賃明細書
'  作  成  者       :  kurihara
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF600BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF600BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF600DAC = New LMF600DAC()


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
        Dim tableNmIn As String = "LMF600IN"
        Dim tableNmOut As String = "LMF600OUT"
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
            inTbl.ImportRow(dt.Rows(i))

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
            prtDs = comPrt.CallDataSet(ds.Tables("LMF600OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables("LMF600OUT"), _
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

        'Select Case prtId
        '    Case ""
        '    Case Else

        'End Select

        '(2012.08.28) 要望番号1373 支払グループまとめ処理 --- START ----
        '支払グループのまとめ編集
        Dim outDt As DataTable = ds.Tables("LMF600OUT")  'データ取得
        Dim cloneDt As DataTable = outDt.Clone
        Dim wkCUST_CD_L As String = String.Empty
        Dim wkSHIHARAI_GROUP_NO As String = String.Empty
        Dim preCust As String = String.Empty
        '支払グループのまとめ
        For Each row As DataRow In outDt.Rows

            If String.IsNullOrEmpty(row.Item("SHIHARAI_GROUP_NO").ToString) = True OrElse row.Item("UNSO_NO_L").ToString.Equals(row.Item("SHIHARAI_GROUP_NO").ToString) = True Then
                'まとめではない場合、まとめ処理を抜かす
                If String.IsNullOrEmpty(row.Item("SHIHARAI_GROUP_NO").ToString) = False Then

                    wkSHIHARAI_GROUP_NO = row.Item("SHIHARAI_GROUP_NO").ToString
                    'まとめレコードの取得
                    For Each tmpRow As DataRow In outDt.Select(String.Concat("SHIHARAI_GROUP_NO = '", wkSHIHARAI_GROUP_NO, "'"))
                        If preCust.Equals(tmpRow.Item("CUST_CD_L").ToString) = False Then
                            'onaji ninushi ha hyoji shinai
                            wkCUST_CD_L = String.Concat(tmpRow.Item("CUST_CD_L").ToString, ",", wkCUST_CD_L)
                        End If
                        preCust = tmpRow.Item("CUST_CD_L").ToString
                    Next

                    If String.IsNullOrEmpty(wkCUST_CD_L) = False Then
                        wkCUST_CD_L = Mid(wkCUST_CD_L, 1, Len(wkCUST_CD_L) - 1)
                    End If

                    row.Item("CUST_CD_L") = wkCUST_CD_L
                End If

                cloneDt.ImportRow(row)

                wkSHIHARAI_GROUP_NO = String.Empty
                wkCUST_CD_L = String.Empty
            End If

        Next

        '元データクリア
        outDt.Clear()

        '集約データを元データへ
        For Each cloneRow As DataRow In cloneDt.Rows
            Dim wkNewRow As DataRow = ds.Tables("LMF600OUT").NewRow

            wkNewRow("RPT_ID") = cloneRow("RPT_ID")
            wkNewRow("SHIHARAI_GROUP_NO") = cloneRow("SHIHARAI_GROUP_NO")
            wkNewRow("SHIHARAITO_CD") = cloneRow("SHIHARAITO_CD")
            wkNewRow("SHIHARAITO_NM") = cloneRow("SHIHARAITO_NM")
            wkNewRow("UNTIN_CALCULATION_KB") = cloneRow("UNTIN_CALCULATION_KB")
            wkNewRow("CUST_CD_L") = cloneRow("CUST_CD_L")
            wkNewRow("CUST_CD_M") = cloneRow("CUST_CD_M")
            wkNewRow("CUST_CD_S") = cloneRow("CUST_CD_S")
            wkNewRow("CUST_CD_SS") = cloneRow("CUST_CD_SS")
            wkNewRow("CUST_NM_L") = cloneRow("CUST_NM_L")
            wkNewRow("CUST_NM_M") = cloneRow("CUST_NM_M")
            wkNewRow("CUST_NM_S") = cloneRow("CUST_NM_S")
            wkNewRow("CUST_NM_SS") = cloneRow("CUST_NM_SS")
            wkNewRow("T_DATE") = cloneRow("T_DATE")
            wkNewRow("NRS_BR_NM") = cloneRow("NRS_BR_NM")
            wkNewRow("INOUTKA_NO_L") = cloneRow("INOUTKA_NO_L")
            wkNewRow("MOTO_DATA_KB") = cloneRow("MOTO_DATA_KB")
            wkNewRow("OUTKA_PLAN_DATE") = cloneRow("OUTKA_PLAN_DATE")
            wkNewRow("DEST_CD") = cloneRow("DEST_CD")
            wkNewRow("DEST_NM") = cloneRow("DEST_NM")
            wkNewRow("AD_1") = cloneRow("AD_1")
            wkNewRow("GOODS_NM") = cloneRow("GOODS_NM")
            wkNewRow("DECI_NG_NB") = cloneRow("DECI_NG_NB")
            wkNewRow("DECI_KYORI") = cloneRow("DECI_KYORI")
            wkNewRow("DECI_WT") = cloneRow("DECI_WT")
            wkNewRow("DECI_UNCHIN") = cloneRow("DECI_UNCHIN")
            wkNewRow("DECI_CITY_EXTC") = cloneRow("DECI_CITY_EXTC")
            wkNewRow("DECI_WINT_EXTC") = cloneRow("DECI_WINT_EXTC")
            wkNewRow("DECI_RELY_EXTC") = cloneRow("DECI_RELY_EXTC")
            wkNewRow("DECI_TOLL") = cloneRow("DECI_TOLL")
            wkNewRow("DECI_INSU") = cloneRow("DECI_INSU")
            wkNewRow("REMARK") = cloneRow("REMARK")
            wkNewRow("SEARCH_KEY_1") = cloneRow("SEARCH_KEY_1")
            wkNewRow("SHI") = cloneRow("SHI")
            wkNewRow("SHIHARAI_TARIFF_CD") = cloneRow("SHIHARAI_TARIFF_CD")
            wkNewRow("SHIHARAI_FIXED_FLAG") = cloneRow("SHIHARAI_FIXED_FLAG")
            wkNewRow("UNSO_NO_L") = cloneRow("UNSO_NO_L")
            wkNewRow("ARR_PLAN_DATE") = cloneRow("ARR_PLAN_DATE")
            wkNewRow("UNSO_CD") = cloneRow("UNSO_CD")
            wkNewRow("UNSO_BR_CD") = cloneRow("UNSO_BR_CD")
            wkNewRow("UNSOCO_NM") = cloneRow("UNSOCO_NM")
            wkNewRow("UNSOCO_BR_NM") = cloneRow("UNSOCO_BR_NM")

            ds.Tables("LMF600OUT").Rows.Add(wkNewRow)
        Next
        '(2012.08.28) 要望番号1373 支払グループまとめ処理 --- END ----

        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
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
