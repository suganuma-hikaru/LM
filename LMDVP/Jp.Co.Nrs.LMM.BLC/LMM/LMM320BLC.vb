' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM320BLC : 請求項目マスタ
'  作  成  者       :  笈川
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM320BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM320BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM320DAC = New LMM320DAC()

#End Region

#Region "Method"

#Region "検索処理"


    ''' <summary>
    ''' 請求項目マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistSeikyukoumokuM", ds)

        Return ds

    End Function


    ''' <summary>
    ''' 請求項目マスタ更新対象データ件数検索
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
    ''' 請求項目マスタ更新対象データ検索
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
    ''' 請求項目マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSeikyukoumokuM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 請求項目マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertSeikyukoumokuM", ds)

    End Function

    ''' <summary>
    ''' 請求項目マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateSeikyukoumokuM", ds)

    End Function

    ''' <summary>
    ''' 請求先マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSeikyukoumokuM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSeikyukoumokuM", ds)

    End Function
    ''' <summary>
    ''' 郵便マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckZipM(ByVal ds As DataSet) As DataSet

        If String.IsNullOrEmpty(ds.Tables("LMM050IN").Rows(0).Item("ZIP").ToString()) = False Then
            'DACクラス呼出
            ds = MyBase.CallDAC(Me._Dac, "CheckExistZipM", ds)
            '処理件数による判定
            If MyBase.GetResultCount() = 0 Then
                '0件の場合、存在なしエラーメッセージを設定する
                MyBase.SetMessage("E079", New String() {"郵便番号マスタ", "郵便番号"})
            End If
        End If


        Return ds

    End Function
#End Region

#End Region

End Class
