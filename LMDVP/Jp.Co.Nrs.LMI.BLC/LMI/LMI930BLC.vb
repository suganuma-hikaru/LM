' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI930  : 住化カラー実績報告データ作成
'  作  成  者       :  [umano]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI930BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI930BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI930DAC = New LMI930DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' TXT出力データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectTXTメソッド呼出</remarks>
    Private Function SelectTXT(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTXT", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessage("E320", New String() {"実績送信対象データが存在しない", "ファイル作成"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' DBフラグ更新(EDI出荷・受信TBL)処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのupdateTargetFlgメソッド呼出</remarks>
    Private Function updateTargetFlg(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMI930_SMKJISSEKI_TXT")
        Dim max As Integer = dt.Rows.Count - 1

        ''EDI管理番号ソートでDATATABLE内並び替え
        'dt.Select(String.Empty, " EDI_CTL_NO ASC, EDI_CTL_NO_CHU ASC")   'ソート順

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim setDt As DataTable = setDs.Tables("LMI930_SMKJISSEKI_TXT")

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDt.ImportRow(dt.Rows(i))

            '①住化受信TBL(HED)更新処理
            setDs = MyBase.CallDAC(Me._Dac, "updateInOutkaSmkHed", setDs)

            '②住化受信TBL(DTL)更新処理
            setDs = MyBase.CallDAC(Me._Dac, "updateInOutkaSmkDtl", setDs)

            If setDt.Rows(0).Item("INOUT_KB").ToString().Equals("0") = True Then

                '③EDI出荷(大)更新処理
                setDs = MyBase.CallDAC(Me._Dac, "updateOutkaEdiL", setDs)

                '④EDI出荷(中)更新処理
                setDs = MyBase.CallDAC(Me._Dac, "updateOutkaEdiM", setDs)


            ElseIf setDt.Rows(0).Item("INOUT_KB").ToString().Equals("1") = True Then

                '⑤EDI入荷(大)更新処理
                setDs = MyBase.CallDAC(Me._Dac, "updateInkaEdiL", setDs)

                '⑥EDI入荷(中)更新処理
                setDs = MyBase.CallDAC(Me._Dac, "updateInkaEdiM", setDs)

            End If

        Next

        Return ds

    End Function

#End Region

End Class
