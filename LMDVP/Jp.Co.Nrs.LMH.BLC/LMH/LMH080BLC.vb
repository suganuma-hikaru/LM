' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH080    : 
'  作  成  者       :  s.kobayashi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH080BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH080BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Public Const GUIDANCE_KBN As String = "00"

    ''' <summary>
    ''' EXCEL用COLUMタイトル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const EXCEL_COLTITLE As String = "Delivery №"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH080DAC = New LMH080DAC()

#End Region

#Region "Method"

#Region "検索処理"

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
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateDelUTI(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = ds.Tables("LMH080IN_DEL").Rows(0).Item("ROW_NO").ToString()
        Dim delivery_No As String = ds.Tables("LMH080IN_DEL").Rows(0).Item("DELIVERY_NO").ToString()

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "DeleteDtlUTI", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH080BLC.GUIDANCE_KBN, "E011", , rowNo, LMH080BLC.EXCEL_COLTITLE, delivery_No)
            Return ds
        End If

        ds = MyBase.CallDAC(Me._Dac, "DeleteHedUTI", ds)
        If MyBase.GetResultCount = 0 Then
            MyBase.SetMessageStore(LMH080BLC.GUIDANCE_KBN, "E011", , rowNo, LMH080BLC.EXCEL_COLTITLE, delivery_No)
            Return ds
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
