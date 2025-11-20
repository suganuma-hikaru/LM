' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 在校
'  プログラムID     :  LMD070    : 在庫印刷指示
'  作  成  者       :  [菱刈]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD070BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD070BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD070DAC = New LMD070DAC()

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

    ''' <summary>
    ''' 完了件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataKanryou(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectDataKanryou", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count > 0 Then
            '0件以上の場合
            MyBase.SetMessage("W130")

        End If

        Return ds

    End Function


    ''' <summary>
    ''' 月末在庫データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataSeigo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectDataSeigo", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then

            Dim dt As DataTable = ds.Tables("LMD070IN")
            Dim dr As DataRow = dt.Rows(0)
            Dim Data As String = dr.Item("GETSUMATSU_ZAIKO").ToString()
            Dim DataSet As String = String.Empty
            DataSet = String.Concat(Data.Substring(0, 4), "/", Data.Substring(4, 2), "/", Data.Substring(6, 2))
            '0件の場合
            MyBase.SetMessage("E341", New String() {DataSet})

        End If

        Return ds

    End Function


#End Region
#End Region


End Class
