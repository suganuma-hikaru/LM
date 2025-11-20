' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LME030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LME030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LME030DAC = New LME030DAC()

    ''' <summary>
    ''' データ作成判定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ErrFlg As Boolean = False

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuData(ByVal ds As DataSet) As DataSet

        '検索対象データの検索処理
        ds = MyBase.CallDAC(Me._Dac, "SelectKensakuData", ds)

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '閾値の判定の仕方が他とは異なります。
        '理由は、COUNTのSQLがGROUP BY する必要があり、単純なSQLではできなく、
        '本来のSQLの結果に対してCOUNTをかける必要があるため、結局二度同じSQLを発行することになるため、
        '最初から本来必要なデータを取得しておいて、閾値判定をし、表示するかしないかをGUI側で行っている。

        'メッセージコードの設定
        Dim count As Integer = ds.Tables("LME030OUT").Rows.Count
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

#End Region

End Class
