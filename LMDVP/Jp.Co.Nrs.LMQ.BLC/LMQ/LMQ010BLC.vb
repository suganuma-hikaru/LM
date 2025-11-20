' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMQ010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMQ010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMQ010DAC = New LMQ010DAC()


#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "HaitaChk", ds)

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 更新対象データ件数検索
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
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' データ抽出SQLマスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SaveData", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011", New String() {"保存"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ抽出SQLマスタ 削除・復活
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DelRevSQL(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "DelRevSQL", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then
            '0件の場合、論理排他メッセージを設定する
            MyBase.SetMessage("E011", New String() {"削除・復活"})
        Else
            MyBase.SetMessage(Nothing)
        End If

        Return ds

    End Function

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

    ''' <summary>
    ''' データ抽出SQLマスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SaveExcelData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SaveExcelData", ds)

        '処理件数による判定
        If MyBase.GetResultCount() = 0 Then

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
