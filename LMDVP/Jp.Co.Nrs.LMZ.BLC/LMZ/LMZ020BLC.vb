' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ020BLC : 商品マスタ照会
'  作  成  者       :  小林
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMZ020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ020DAC = New LMZ020DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region

#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 商品マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'EDI荷主明細取得
        If Not String.IsNullOrEmpty(ds.Tables("LMZ020IN").Rows(0).Item("EDI_INOUT_KB").ToString) Then
            ds = MyBase.CallDAC(Me._Dac, "SelectEdiCustData", ds)
        End If

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
    ''' 商品マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'EDI荷主明細取得
        If Not String.IsNullOrEmpty(ds.Tables("LMZ020IN").Rows(0).Item("EDI_INOUT_KB").ToString) Then
            ds = MyBase.CallDAC(Me._Dac, "SelectEdiCustData", ds)
        End If

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"


#End Region

#End Region

End Class
