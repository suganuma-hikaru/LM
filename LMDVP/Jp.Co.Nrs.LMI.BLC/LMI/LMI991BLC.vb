' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI991BLC : サーテック　運賃明細作成
'  作  成  者       :  sia-minagawa
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI991BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI991BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI991DAC = New LMI991DAC()


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
        Dim tableNmIn As String = "LMI991IN"
        Dim tableNmOut As String = "LMI991OUT_PRT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dtIn As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        'IN条件0件チェック
        If dtIn.Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")   '印刷条件が設定されていません。
            Return ds
        End If

        '取得件数判定
        If dtOut.Rows.Count = 0 Then
            MyBase.SetMessage("E070")   '印刷対象データがありませんでした。
            Return ds
        End If

        '使用帳票ID取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        '取得件数判定
        If dtRpt.Rows.Count = 0 Then
            MyBase.SetMessage("E078", {"帳票パターンマスタ"})   '[%1]に該当データが存在しません。
            Return ds
        End If

        'レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In dtRpt.Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                                dr.Item("PTN_ID").ToString(),
                                dr.Item("PTN_CD").ToString(),
                                dr.Item("RPT_ID").ToString(),
                                dtOut,
                                ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function

#End Region

#End Region

End Class
