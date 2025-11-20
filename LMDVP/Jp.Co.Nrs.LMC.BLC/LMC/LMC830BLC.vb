' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC830    : 日立物流出荷音声データCSV作成
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC830BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC830BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC830DAC = New LMC830DAC()

#End Region

#Region "Method"

#Region "CSVデータ取得処理"

    ''' <summary>
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのDoPrintメソッド呼出</remarks>
    Private Function SelectCSV(ByVal ds As DataSet) As DataSet

        '発行ナンバーに採番
        Dim num As New NumberMasterUtility
        Dim hakoNo As String = String.Empty
        Dim tableNm As String = String.Empty
        Dim max As Integer = 0

        '要望番号:1339 yamanaka 2012.09.28 Start
        If ds.Tables("LMC830IN").Rows(0).Item("RPT_FLG").Equals("05") = True Then
            '検索処理
            ds = MyBase.CallDAC(Me._Dac, "SelectCSV", ds)
            tableNm = "LMC830OUT_CSV"

        Else
            '出荷取消検索処理
            ds = MyBase.CallDAC(Me._Dac, "SelectDelCSV", ds)
            tableNm = "LMC830OUT_CSV_DEL"

        End If

        If ds.Tables(tableNm).Rows.Count > 0 Then
            hakoNo = num.GetAutoCode(NumberMasterUtility.NumberKbn.HAKO_NO, Me, String.Empty).ToString()
            max = ds.Tables(tableNm).Rows.Count - 1
            For i As Integer = 0 To max
                ds.Tables(tableNm).Rows(i).Item("HAKO_NO") = hakoNo
            Next
        End If
        '要望番号:1339 yamanaka 2012.09.28 End

        Return ds

    End Function

#End Region

#End Region

End Class
