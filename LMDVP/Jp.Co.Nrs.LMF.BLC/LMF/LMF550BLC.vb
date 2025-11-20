' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF550    : 物品引取書
'  作  成  者       :  大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF550BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF550BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF550DAC = New LMF550DAC()


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
        If String.IsNullOrEmpty(ds.Tables("LMF550IN").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMF550IN").Rows(0).Item("PRT_NB"))
        End If

        'IN条件0件チェック
        If ds.Tables("LMF550IN").Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds

        End If

        '使用帳票ID取得
        ds = Me.SelectMPrt(ds)

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        ds = Me.SelectPrintData(ds)

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        'レポートID分繰り返す
        Dim prtDs As DataSet

        '帳票種別用
        Dim rptId As String = String.Empty
        Dim prtmax As Integer = 0
        Dim prtDsMax As Integer = 0

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
                prtDs = comPrt.CallDataSet(ds.Tables("LMF550OUT"), dr.Item("RPT_ID").ToString())

                'データセットの編集(出力用テーブルに抽出データを設定)
                prtDs = Me.EditPrintDataSet(rptId, prtDs)

                comPrt = New LMReportDesignerUtility

                prtDsMax = prtDs.Tables("LMF550OUT").Rows.Count - 1
                prtmax = 3

                For j As Integer = 0 To prtmax

                    Select Case j
                        Case 0
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables("LMF550OUT").Rows(k).Item("PRT_KBN") = "01"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb)

                        Case 1
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables("LMF550OUT").Rows(k).Item("PRT_KBN") = "02"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb)

                        Case 2
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables("LMF550OUT").Rows(k).Item("PRT_KBN") = "03"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb)

                        Case 3
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables("LMF550OUT").Rows(k).Item("PRT_KBN") = "04"
                            Next
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb)

                    End Select

                Next

            End If

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

        Select Case rptId
            Case ""
        End Select

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

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer) As DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility
        'System.Threading.Thread.Sleep(1000)

		'CSV出力
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                          dr.Item("PTN_ID").ToString(), _
                          dr.Item("PTN_CD").ToString(), _
                          dr.Item("RPT_ID").ToString(), _
                          prtDs.Tables("LMF550OUT"), _
                          ds.Tables(LMConst.RD), _
                          String.Empty, _
                          String.Empty, _
                          prtNb)

        Return ds

    End Function

#End Region

#End Region

End Class
