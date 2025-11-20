' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM110BLC : 休日マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMM110BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM110BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM110DAC = New LMM110DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 休日マスタ更新対象データ件数検索
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
    ''' 休日マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function


    ''' <summary>
    ''' 休日マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCopyData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectCopyData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E310")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectUpdData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectUpdData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count > 0 Then
            '0件ではない場合
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ存在チェック(編集押下時)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectExistData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "CheckExistHolM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ存在チェック(新規登録時)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectExistDataIn(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "CheckExistHolMIn", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count > 0 Then
            '0件ではない場合
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


#End Region

#Region "設定処理"

    ''' <summary>
    ''' 休日マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertHolM(ByVal ds As DataSet) As DataSet

        '採番
        Dim dr As DataRow = ds.Tables("LMM110IN").Rows(0)
        ds.Tables("LMM110OUT").ImportRow(dr)

        Return MyBase.CallDAC(Me._Dac, "InsertHolM", ds)

    End Function

    ''' <summary>
    ''' 休日マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHolM(ByVal ds As DataSet) As DataSet

        '更新前データをDELETE
        ds = MyBase.CallDAC(Me._Dac, "UpdateHolM", ds)

        '更新(INSERT)
        ds = MyBase.CallDAC(Me._Dac, "InsertHolM", ds)

        Dim dr As DataRow = ds.Tables("LMM110IN").Rows(0)
        ds.Tables("LMM110OUT").ImportRow(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 休日マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteHolM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteHolM", ds)

    End Function

    ''' <summary>
    ''' 休日マスタ複写
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CopyHolM(ByVal ds As DataSet) As DataSet

        '複写先をDELETE
        ds = MyBase.CallDAC(Me._Dac, "CopyDelHolM", ds)

        '日付複写(SELECT/INSERT)
        ds = MyBase.CallDAC(Me._Dac, "CopyInHolM", ds)

        'OUTテーブルに設定
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        '暦上日チェック
        ds = MyBase.CallDAC(Me._Dac, "IsDateCheck", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
