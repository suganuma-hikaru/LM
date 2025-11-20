' ==========================================================================
'  システム名       : LM
'  サブシステム名   : LMD       : 在庫管理
'  プログラムID     : LMD670    : 強制出庫在庫一覧
'  作  成  者       : hori
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD670BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD670BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD670DAC = New LMD670DAC()

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

        'DataTable名
        Dim tableNmIn As String = "LMD670IN"
        Dim tableNmOut As String = "LMD670OUT"
        Dim tableNmRpt As String = "M_RPT"

        '印刷条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            MyBase.SetMessage("G039")
            Return ds
        End If

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()

        For dtIdx As Integer = 0 To ds.Tables(tableNmIn).Rows.Count - 1
            '条件の設定
            setDs.Clear()
            setDs.Tables(tableNmIn).ImportRow(ds.Tables(tableNmIn).Rows(dtIdx))

            '使用帳票ID取得
            If dtIdx = 0 Then
                '繰り返しの1回目のみ
                setDs = Me.SelectMPrt(setDs)

                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

                'レポートファイル名が未設定なら抜ける
                If setDs.Tables(tableNmRpt).Rows.Count < 1 Then
                    Return ds
                ElseIf String.IsNullOrEmpty(setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString()) Then
                    Return ds
                End If

                ds.Tables(tableNmRpt).ImportRow(setDs.Tables(tableNmRpt).Rows(0))
            End If

            '印刷データ取得
            setDs = Me.SelectPrintData(setDs)
            For dtIdx2 As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1
                ds.Tables(tableNmOut).ImportRow(setDs.Tables(tableNmOut).Rows(dtIdx2))
            Next
        Next

        '印刷対象0件チェック
        If ds.Tables(tableNmOut).Rows.Count = 0 Then
            Return ds
        End If

        '帳票CSV出力
        For Each dr As DataRow In ds.Tables(tableNmRpt).Rows
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                    dr.Item("PTN_ID").ToString(),
                    dr.Item("PTN_CD").ToString(),
                    dr.Item("RPT_ID").ToString(),
                    ds.Tables(tableNmOut),
                    ds.Tables(LMConst.RD))
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
