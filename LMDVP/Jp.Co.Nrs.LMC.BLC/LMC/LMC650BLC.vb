' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC650    : 詰め合わせ明細書
'  作  成  者       :  [SHINOHARA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC650BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC650BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC650DAC = New LMC650DAC()

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

        '部数の設定がない場合は初期値0を設定
        Dim prtNb As Integer = 0
        If String.IsNullOrEmpty(ds.Tables("LMC650INOUT").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMC650INOUT").Rows(0).Item("PRT_NB"))
        End If

        'IN条件0件チェック
        If ds.Tables("LMC650INOUT").Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        '使用帳票ID取得
        Dim rtnDs As DataSet = Me.SelectMPrt(ds)

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        'レポートIDの設定
        ds = Me.setRptId(ds, rtnDs)

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
        'レポートID分繰り返す
        Dim prtDs As DataSet
        '帳票種別用
        Dim rptId As String = String.Empty

        For Each dr As DataRow In ds.Tables("M_RPT").Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            prtDs = New DataSet
            'レポートID取得
            rptId = dr.Item("RPT_ID").ToString()

            '空まはたNULLの場合は処理を飛ばす
            If String.IsNullOrEmpty(rptId) = False Then
                '指定したレポートIDのデータを抽出する。
                prtDs = comPrt.CallDataSet(ds.Tables("LMC650INOUT"), dr.Item("RPT_ID").ToString())
                'データセットの編集(出力用テーブルに抽出データを設定)
                comPrt = New LMReportDesignerUtility

                '帳票CSV出力
                comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                      dr.Item("PTN_ID").ToString(), _
                                      dr.Item("PTN_CD").ToString(), _
                                      dr.Item("RPT_ID").ToString(), _
                                      prtDs.Tables("LMC650INOUT"), _
                                      ds.Tables(LMConst.RD), _
                                      String.Empty, _
                                      String.Empty, _
                                      prtNb)
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' RPT_IDの設定を行う。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function setRptId(ByVal ds As DataSet, ByVal rtnDs As DataSet) As DataSet

        If rtnDs.Tables("M_RPT").Rows.Count = 0 Then
            Return ds
        End If

        'SQLで取得したRPT_IDをINOUTに設定する
        Dim TableNm As String = "LMC650INOUT"
        Dim max As Integer = ds.Tables(TableNm).Rows.Count - 1

        For i As Integer = 0 To max
            ds.Tables(TableNm).Rows(i).Item("RPT_ID") = rtnDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString
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

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

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
