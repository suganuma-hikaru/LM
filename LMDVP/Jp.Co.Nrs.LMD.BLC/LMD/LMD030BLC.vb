' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 在庫管理
'  プログラムID     :  LMD030    : 在庫履歴
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMD030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD030DAC = New LMD030DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ件数検索
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
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region '検索処理

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '請求日チェック
        If Me.ChkDate(ds.Tables("LMD030IN_ZAI_DEL").Rows(0).Item("IDO_DATE").ToString(), Me.SelectGheaderData(ds), "削除") = False Then
            Return ds
        End If

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "DeleteDataControl", ds)

        Return ds

    End Function

#End Region '削除処理

#Region "チェック"

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As String

        ds = MyBase.CallDAC(Me._Dac, "SelectGheaderData", ds)

        Dim dt As DataTable = ds.Tables("G_HED")
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal value1 As String, ByVal value2 As String, ByVal msg As String) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            MyBase.SetMessage("E232", New String() {"経理取込", msg})
            Return False

        End If

        Return True

    End Function

#End Region

#End Region

End Class
