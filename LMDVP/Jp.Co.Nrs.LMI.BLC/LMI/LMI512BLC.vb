' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI512BLC : 出荷予定表(JNC)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI512BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI512BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TABLE_NM
        Public Const PRT_IN As String = "LMI512IN"
        Public Const PRT_OUT As String = "LMI512OUT"
        Public Const M_RPT As String = "M_RPT"
    End Class

#End Region 'Const

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI512DAC = New LMI512DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '印刷条件0件チェック
        If ds.Tables(TABLE_NM.PRT_IN).Rows.Count = 0 Then
            MyBase.SetMessage("G039")
            Return ds
        End If

        'データ取得はDataSetのコピーを使用する
        Dim setDs As DataSet = ds.Copy()

        '印刷条件の繰り返し
        For dtIdx As Integer = 0 To ds.Tables(TABLE_NM.PRT_IN).Rows.Count - 1
            '印刷条件の設定
            setDs.Clear()
            setDs.Tables(TABLE_NM.PRT_IN).ImportRow(ds.Tables(TABLE_NM.PRT_IN).Rows(dtIdx))

            '帳票パターン取得
            If dtIdx = 0 Then
                '繰り返しの1回目のみ
                With Nothing
                    '取得
                    setDs = Me.SelectMPrt(setDs)

                    'メッセージが設定されていればエラーで抜ける
                    If MyBase.IsMessageExist() Then
                        Return ds
                    End If

                    'レポートファイル名が未設定なら抜ける
                    If setDs.Tables(TABLE_NM.M_RPT).Rows.Count < 1 Then
                        Return ds
                    ElseIf String.IsNullOrEmpty(setDs.Tables(TABLE_NM.M_RPT).Rows(0).Item("RPT_ID").ToString()) Then
                        Return ds
                    End If

                    ds.Tables(TABLE_NM.M_RPT).ImportRow(setDs.Tables(TABLE_NM.M_RPT).Rows(0))
                End With
            End If

            '印刷データ取得
            setDs = Me.SelectPrintData(setDs)
            For dtIdx2 As Integer = 0 To setDs.Tables(TABLE_NM.PRT_OUT).Rows.Count - 1
                ds.Tables(TABLE_NM.PRT_OUT).ImportRow(setDs.Tables(TABLE_NM.PRT_OUT).Rows(dtIdx2))
            Next
        Next

        '印刷対象0件チェック
        If ds.Tables(TABLE_NM.PRT_OUT).Rows.Count = 0 Then
            Return ds
        End If

        '帳票CSV出力
        For Each dr As DataRow In ds.Tables(TABLE_NM.M_RPT).Rows
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                    dr.Item("PTN_ID").ToString(),
                    dr.Item("PTN_CD").ToString(),
                    dr.Item("RPT_ID").ToString(),
                    ds.Tables(TABLE_NM.PRT_OUT),
                    ds.Tables(LMConst.RD))
        Next

        Return ds

    End Function

    ''' <summary>
    '''　帳票パターン取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

    ''' <summary>
    ''' プリントフラグ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdatePrtFlg(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdatePrtFlg", ds)

    End Function

#End Region 'Method

End Class
