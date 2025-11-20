' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330  : 納品データ選択&編集
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI330BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI330BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI330DAC = New LMI330DAC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 編集時排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub HaitaChk(ByVal ds As DataSet)

        'DACクラス呼出
        Call MyBase.CallDAC(Me._Dac, "HaitaChk", ds)

    End Sub

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 閾値件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectExistData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectExistData", ds)

    End Function

    ''' <summary>
    ''' 編集登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        'EDIロンザ専用テーブル(大)更新
        Call MyBase.CallDAC(Me._Dac, "UpdateDataL", ds)

        'EDIロンザ専用テーブル(中)更新
        Dim inDs As DataSet = New LMI330DS
        Dim inDt As DataTable = inDs.Tables("LMI330IN")

        For i As Integer = 0 To ds.Tables("LMI330IN").Rows.Count - 1

            inDt.Clear()
            inDt.ImportRow(ds.Tables("LMI330IN").Rows(i))

            Call MyBase.CallDAC(Me._Dac, "UpdateDataM", inDs)

        Next

        Return ds

    End Function

#End Region

#End Region

End Class
