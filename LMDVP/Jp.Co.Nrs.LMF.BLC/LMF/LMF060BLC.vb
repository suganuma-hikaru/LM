' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF060BLC : 運行未登録運送検索
'  作  成  者       :  菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMF200BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF060DAC = New LMF060DAC()



#End Region

#Region "Const"
    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_IN As String = "M_KYORI"


#End Region

#Region "Method"

#Region "検索処理"
    ''' <summary>
    ''' 運送Lテーブルデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '2011.10.11 検証結果_その他(メモ)№47対応 START
        '画面の距離程コードの保持
        Dim dr As DataRow = ds.Tables(LMF060BLC.TABLE_NM_IN).Rows(0)
        Dim KyoriCd As String = dr.Item("KYORI_CD").ToString()
        '2011.10.11 検証結果_その他(メモ)№47対応 END

        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count < 1 Then
            '0件の場合
            'MyBase.SetMessage("G001")
            MyBase.SetMessage("E079", New String() {"距離程マスタ", KyoriCd}) '2011.10.11 検証結果_その他(メモ)№47対応
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
