' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG020BLC : 保管料/荷役料計算
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMG020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG020DAC = New LMG020DAC()

#End Region

#Region "Method"

#Region "検索処理"

    '''' <summary>
    '''' 検索処理(件数取得)
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
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
    ''' 検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索実行
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        ' ''SBS菱刈　レスポンス対応（件数取得処理回避）のため、当メソッド内にてメッセージをセット
        'Dim dt As DataTable = ds.Tables("LMG020OUT")
        'Dim count As Integer = dt.Rows.Count
        'If count < 1 Then
        '    '0件の場合
        '    MyBase.SetMessage("G001")

        'ElseIf count > MyBase.GetLimitCount() Then
        '    '閾値以上の場合	
        '    MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})

        'End If
        Return ds

    End Function

#End Region

#Region "請求存在チェック"

    ''' <summary>
    ''' 請求存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiq(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectSeiq", ds)

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables("LMG030IN")

        'INTableの条件rowの格納
        Dim Row As DataRow = inTbl.Rows(0)



        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E079", New String() {"請求元在庫データ", String.Concat("JOB_NO:", Row.Item("JOB_NO").ToString())})
        End If
        Return ds

    End Function

#End Region

#End Region

End Class
