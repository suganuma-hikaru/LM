' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI513BLC : JNC_運賃照合作成
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI513BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI513BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI513DAC = New LMI513DAC()


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
        Dim tableNmIn As String = "LMI513IN"
        Dim tableNmOut As String = "LMI513OUT_PRT"
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


#Region "Excel出力"

    ''' <summary>
    ''' Excel出力データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExcelMake(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "ExcelMake", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If (-1).Equals(count) = True Then
            'エラー時
            MyBase.SetMessage("S001", New String() {"Excel作成"})
        Else
            'メッセージエリアの設定
            MyBase.SetMessage("G002", New String() {"Excel作成", String.Empty})
        End If

        Return ds

    End Function
#End Region

#End Region

End Class
