' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM030BLC : 作業項目マスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMM030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM030DAC = New LMM030DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 作業項目マスタ更新対象データ件数検索
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
    ''' 作業項目マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 契約通貨コード初期値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectItemCurrCd(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectItemCurrCd", ds)

    End Function

    ''' <summary>
    ''' 協力会社作業の名称取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSagyoSub(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectSagyoSub", ds)

    End Function

    ''' <summary>
    ''' 協力会社作業の重複チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectExistSagyoSub(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectExistSagyoSub", ds)

    End Function


#End Region

#Region "設定処理"

    ''' <summary>
    ''' 作業項目マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSagyoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSagyoM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSagyoM(ByVal ds As DataSet) As DataSet

        Dim sagyoCdMax As String = Me.GetSagyoCd(ds)

        '採番
        Dim dr As DataRow = ds.Tables("LMM030IN").Rows(0)
        dr.Item("SAGYO_CD") = sagyoCdMax
        ds.Tables("LMM030OUT").ImportRow(dr)

        Return MyBase.CallDAC(Me._Dac, "InsertSagyoM", ds)

    End Function

    ''' <summary>
    ''' 作業項目マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSagyoM(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "UpdateSagyoM", ds)
        Dim dr As DataRow = ds.Tables("LMM030IN").Rows(0)
        ds.Tables("LMM030OUT").ImportRow(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 作業項目マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSagyoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSagyoM", ds)

    End Function

    ''' <summary>
    ''' 作業コード(自動採番)を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>sagyoCd</returns>
    ''' <remarks></remarks>
    Private Function GetSagyoCd(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.SAGYO_CD, Me, ds.Tables("LMM030IN").Rows(0).Item("NRS_BR_CD").ToString())

    End Function

#End Region

#End Region

End Class
