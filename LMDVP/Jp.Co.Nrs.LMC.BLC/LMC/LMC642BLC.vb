' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC642    : 出荷チェックリスト印刷
'  作  成  者       :  [NAKAMURA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC642BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC642BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC642DAC = New LMC642DAC()
    Private _Dac643 As LMC643DAC = New LMC643DAC()

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
        If String.IsNullOrEmpty(ds.Tables("LMC642IN").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMC642IN").Rows(0).Item("PRT_NB"))
        End If
        If prtNb = 0 Then
            prtNb = 1
        End If
        'IN条件0件チェック
        If ds.Tables("LMC642IN").Rows.Count = 0 Then
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
        Dim prtCkMax As Integer = 0
        Dim tableName As String = "LMC642OUT"
        Dim tachiai_FLG As String = LMConst.FLG.OFF
        Dim FLG_40_00588 As String = LMConst.FLG.OFF

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
                prtDs = comPrt.CallDataSet(ds.Tables(tableName), dr.Item("RPT_ID").ToString())

                comPrt = New LMReportDesignerUtility

                prtDsMax = prtDs.Tables(tableName).Rows.Count - 1

#If False Then  'UPD 2019/11/11 007539   【LMS】出荷チェックリストは２枚印刷出来るようにしてほしい
                Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)

#Else
                '部数1設定確認
                Dim rpt_Cnt As Integer = 1
                If String.IsNullOrEmpty(dr.Item("COPIES_NB1").ToString()) = False Then
                    rpt_Cnt = CInt(dr.Item("COPIES_NB1").ToString())

                    If (0).Equals(rpt_Cnt) Then
                        Return ds
                    End If
                End If

                For i As Integer = 1 To rpt_Cnt
                    Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                Next
#End If
            End If
        Next

        Return ds

    End Function
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint2(ByVal ds As DataSet) As DataSet

        '部数の設定がない場合は初期値0を設定
        Dim prtNb As Integer = 0
        If String.IsNullOrEmpty(ds.Tables("LMC642IN").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMC642IN").Rows(0).Item("PRT_NB"))
        End If
        If prtNb = 0 Then
            prtNb = 1
        End If
        'IN条件0件チェック
        If ds.Tables("LMC642IN").Rows.Count = 0 Then
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
        Dim prtCkMax As Integer = 0
        Dim tableName As String = "LMC642OUT"

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
                prtDs = comPrt.CallDataSet(ds.Tables(tableName), dr.Item("RPT_ID").ToString())

                comPrt = New LMReportDesignerUtility

                prtDsMax = prtDs.Tables(tableName).Rows.Count - 1
#If False Then  'UPD 2019/11/11 007539   【LMS】出荷チェックリストは２枚印刷出来るようにしてほしい
                Call Me.CSV_OUT2(ds, dr, prtDs, prtNb, tableName)
#Else
                '部数1設定確認
                Dim rpt_Cnt As Integer = 1
                If String.IsNullOrEmpty(dr.Item("COPIES_NB1").ToString()) = False Then
                    rpt_Cnt = CInt(dr.Item("COPIES_NB1").ToString())

                    If (0).Equals(rpt_Cnt) Then
                        Return ds
                    End If

                End If

                For i As Integer = 1 To rpt_Cnt
                    Call Me.CSV_OUT2(ds, dr, prtDs, prtNb, tableName)
                Next
#End If
            End If
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

    ''' <summary>
    '''　棟番号取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTouNo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTouNo", ds)

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

        Dim rptId As String = ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString

        If rptId = "LMC643" Then
            Return MyBase.CallDAC(Me._Dac643, "SelectPrintData", ds)
        End If

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

    ''' <summary>
    ''' 棟別の帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function TOU_CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        '棟別の帳票CSV出力
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                              dr.Item("PTN_ID").ToString(), _
                              dr.Item("PTN_CD").ToString(), _
                              dr.Item("RPT_ID").ToString(), _
                              prtDs.Tables(tableName), _
                              ds.Tables(LMConst.RD), _
                              String.Empty, _
                              String.Empty, _
                              prtNb)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet
        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        Dim addFilePath As String = String.Empty
        Dim outkaNoL As String = String.Empty

        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      dr.Item("RPT_ID").ToString(), _
                      prtDs.Tables(tableName), _
                      ds.Tables(LMConst.RD), _
                      String.Empty, _
                      String.Empty, _
                      prtNb)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT2(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet
        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      dr.Item("RPT_ID").ToString(), _
                      prtDs.Tables(tableName), _
                      ds.Tables(LMConst.RD), _
                      String.Empty, _
                      String.Empty, _
                      prtNb)

        Return ds

    End Function
#End Region

#End Region

End Class
