' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF700BLC : 運送チェックリスト
'  作  成  者       :  hori
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF700BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF700BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF700DAC = New LMF700DAC()

#End Region 'Field

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '定数
        Const TBL_NM_IN As String = "LMF700IN"
        Const TBL_NM_OUT As String = "LMF700OUT"
        Const TBL_NM_RPT As String = "M_RPT"

        'IN条件0件チェック
        If ds.Tables(TBL_NM_IN).Rows.Count = 0 Then
            MyBase.SetMessage("G039")
            Return ds
        End If

        'IN条件のループ
        For i As Integer = 0 To ds.Tables(TBL_NM_IN).Rows.Count - 1
            '検索用のDataSetを用意
            Dim setDs As DataSet = ds.Clone()
            Dim inTbl As DataTable = setDs.Tables(TBL_NM_IN)
            inTbl.ImportRow(ds.Tables(TBL_NM_IN).Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            If MyBase.GetResultCount() < 1 Then
                '0件の場合                
                MyBase.SetMessageStore("00", "E078", New String() {String.Concat("運送チェックリスト ", inTbl.Rows(0).Item("UNSO_NO_L").ToString)})
                Continue For
            End If

            '印刷データ検索
            setDs = Me.SelectPrintData(setDs)

            '検索結果を元のDataSetに追加
            ds.Tables(TBL_NM_RPT).Merge(setDs.Tables(TBL_NM_RPT))
            ds.Tables(TBL_NM_OUT).Merge(setDs.Tables(TBL_NM_OUT))
        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(ds.Tables(TBL_NM_RPT))
        Dim rptDt As DataTable = view.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        'ソート実行
        Dim rptDr As DataRow() = rptDt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = ds.Tables(TBL_NM_RPT).Clone()

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        '重複分を除外したワーク用RPTテーブルを帳票出力に使用するDSにセットする)
        ds.Tables(TBL_NM_RPT).Clear()

        Dim keyBrCd As String = String.Empty
        Dim keyPtnId As String = String.Empty
        Dim keyRptId As String = String.Empty

        For i As Integer = 0 To sortDtRpt.Rows.Count - 1
            '営業所コード、パターンID、レポートIDが一致するレコードは除外する
            If keyBrCd <> sortDtRpt.Rows(i).Item("NRS_BR_CD").ToString() OrElse
                    keyPtnId <> sortDtRpt.Rows(i).Item("PTN_ID").ToString() OrElse
                    keyRptId <> sortDtRpt.Rows(i).Item("RPT_ID").ToString() Then

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                ds.Tables(TBL_NM_RPT).ImportRow(sortDtRpt.Rows(i))

                'キー更新
                keyBrCd = sortDtRpt.Rows(i).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(i).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(i).Item("RPT_ID").ToString()
            End If
        Next

        '部数の決定
        Dim prtNb As Integer = 0
        If Not String.IsNullOrEmpty(ds.Tables(TBL_NM_IN).Rows(0).Item("PRT_NB").ToString) Then
            prtNb = Convert.ToInt32(ds.Tables(TBL_NM_IN).Rows(0).Item("PRT_NB").ToString)
        End If
        If prtNb = 0 Then
            prtNb = 1
        End If

        'レポートID分繰り返す
        For Each drRpt As DataRow In ds.Tables(TBL_NM_RPT).Rows
            'レポートIDが空の場合はスキップ
            If String.IsNullOrEmpty(drRpt.Item("RPT_ID").ToString) Then
                Continue For
            End If

            '指定したレポートIDのデータを抽出
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            Dim prtDs As DataSet = New DataSet
            prtDs = comPrt.CallDataSet(ds.Tables(TBL_NM_OUT), drRpt.Item("RPT_ID").ToString())

            '帳票CSV出力
            comPrt.StartPrint( _
                    Me,
                    drRpt.Item("NRS_BR_CD").ToString(),
                    drRpt.Item("PTN_ID").ToString(),
                    drRpt.Item("PTN_CD").ToString(),
                    drRpt.Item("RPT_ID").ToString(),
                    prtDs.Tables(TBL_NM_OUT),
                    ds.Tables(LMConst.RD),
                    String.Empty,
                    String.Empty,
                    prtNb)
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

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

#End Region '印刷処理

#End Region 'Method

End Class
