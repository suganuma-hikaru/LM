' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF070BLC : 運賃試算比較
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMF070BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF070BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF070DAC = New LMF070DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        'メッセージコードの設定
        Dim count As Integer = ds.Tables("LMF070OUT").Rows.Count

        If count < 1 Then

            '0件の場合
            MyBase.SetMessage("G001")

        ElseIf count > MyBase.GetMaxResultCount() Then

            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
