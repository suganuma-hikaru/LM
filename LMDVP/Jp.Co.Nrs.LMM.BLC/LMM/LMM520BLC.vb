' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM520    : 届先確認書
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM520BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM520BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM520DAC = New LMM520DAC()


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
        Dim tableNmIn As String = "LMM520IN"
        Dim tableNmOut As String = "LMM520OUT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        '結果DataTableの初期化
        dtOut.Clear()

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '対象結果をDataTableにセットする
            dtOut.ImportRow(setDs.Tables(tableNmOut).Rows(0))

        Next

        'ソート実行
        Dim rptDr As DataRow() = dtOut.Select(Nothing, "CUST_CD_L ASC, DEST_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtOut.Clone

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        'レポートID分繰り返す
        Dim prtDs As DataSet = New DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        With sortDtRpt.Rows(0)

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(sortDtRpt, .Item("RPT_ID").ToString())

            '帳票CSV出力
            comPrt.StartPrint(Me, .Item("NRS_BR_CD").ToString(), _
                                .Item("PTN_ID").ToString(), _
                                .Item("PTN_CD").ToString(), _
                                .Item("RPT_ID").ToString(), _
                                prtDs.Tables(tableNmOut), _
                                ds.Tables(LMConst.RD))

        End With


        
        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
