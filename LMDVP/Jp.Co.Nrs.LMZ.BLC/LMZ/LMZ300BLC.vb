' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ300BLC : 支払割増運賃タリフマスタ照会
'  作  成  者       :  terakawa
' ==========================================================================

Option Explicit On

Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMZ300BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ300BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMZ300DAC = New LMZ300DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 割増運賃タリフマスタ/運賃タリフセットマスタデータ件数検索
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

#End Region


#Region "存在チェック"

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckUnsocoM(ByVal ds As DataSet) As DataSet

        If String.IsNullOrEmpty(ds.Tables("LMZ300IN").Rows(0).Item("UNSOCO_CD").ToString()) = False Then

            'DACクラス呼出
            ds = MyBase.CallDAC(Me._Dac, "CheckExistUnsocoM", ds)

            '処理件数による判定
            If MyBase.GetResultCount() = 0 Then
                '0件の場合、存在なしエラーメッセージを設定する
                MyBase.SetMessage("E079", New String() {"運送会社マスタ", "運送会社コード"})
            End If

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
