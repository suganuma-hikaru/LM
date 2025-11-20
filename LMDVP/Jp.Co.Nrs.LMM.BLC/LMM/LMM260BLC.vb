' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM260BLC : 注意書マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM260BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM260BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM260DAC = New LMM260DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 注意書マスタ更新対象データ件数検索
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
    ''' 注意書マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 注意書マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CountM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CountM", ds)

        '処理件数による判定
        If 999 < Convert.ToInt32(ds.Tables("LMM260MAX").Rows(0).Item("REM_NO").ToString()) Then
            '1000以上の場合、エラーメッセージを設定する
            MyBase.SetMessage("E062", New String() {"注意書番号"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 注意書マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaChuigakiM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectChuigakiM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 注意書マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertChuigakiM(ByVal ds As DataSet) As DataSet

        'テーブルの値を移し変える
        ds.Tables("LMM260IN").Rows(0).Item("REM_NO") = ds.Tables("LMM260MAX").Rows(0).Item("REM_NO")

        Return MyBase.CallDAC(Me._Dac, "InsertChuigakiM", ds)

    End Function

    ''' <summary>
    ''' 注意書マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateChuigakiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateChuigakiM", ds)

    End Function

    ''' <summary>
    ''' 注意書マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteChuigakiM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteChuigakiM", ds)

    End Function

#End Region

#End Region

End Class
