' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM200BLC : 車輌マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMM200BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM200BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM200DAC = New LMM200DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 車輌マスタ更新対象データ件数検索
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
    ''' 車輌マスタ更新対象データ検索
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
    ''' 車輌マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSyaryoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSyaryoM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 車輌マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSyaryoM(ByVal ds As DataSet) As DataSet

        Dim carKeydMax As String = Me.GetcarKey(ds)

        '採番
        Dim dr As DataRow = ds.Tables("LMM200IN").Rows(0)
        dr.Item("CAR_KEY") = carKeydMax
        ds.Tables("LMM200OUT").ImportRow(dr)

        Return MyBase.CallDAC(Me._Dac, "InsertSyaryoM", ds)

    End Function

    ''' <summary>
    ''' 車輌マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSyaryoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateSyaryoM", ds)

    End Function

    ''' <summary>
    ''' 車輌マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSyaryoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSyaryoM", ds)

    End Function


    ''' <summary>
    ''' 車輌キー(自動採番)を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>carKey</returns>
    ''' <remarks></remarks>
    Private Function GetcarKey(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.CAR_KEY, Me, ds.Tables("LMM200IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

#End Region

#End Region

End Class
