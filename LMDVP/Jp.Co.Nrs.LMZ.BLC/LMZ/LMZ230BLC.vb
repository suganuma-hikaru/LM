' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ230BLC : 横持ちマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ230BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ230BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ230DAC = New LMZ230DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ/運賃タリフセットマスタデータ件数検索
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
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ/運賃タリフセットマスタデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function


    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' マイ運賃タリフ対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMyData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectData", ds)

    End Function
    '要望対応:1248 terakawa 2013.03.21 End

#End Region


#Region "存在チェック"

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckCustM(ByVal ds As DataSet) As DataSet

        If String.IsNullOrEmpty(ds.Tables("LMZ230IN").Rows(0).Item("CUST_CD_L").ToString()) = False Then

            'DACクラス呼出
            ds = MyBase.CallDAC(Me._Dac, "CheckExistCustM", ds)

            '処理件数による判定
            If MyBase.GetResultCount() = 0 Then
                '0件の場合、存在なしエラーメッセージを設定する
                '2016.01.06 UMANO 英語化対応START
                'MyBase.SetMessage("E079", New String() {"荷主マスタ", "荷主コード"})
                MyBase.SetMessage("E773")
                '2016.01.06 UMANO 英語化対応END
            End If

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
