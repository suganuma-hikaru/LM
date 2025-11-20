' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB070BLC : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMB070BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB070BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB070DAC = New LMB070DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 写真選択対象データ件数検索
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
    ''' 写真選択対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 入荷写真データ追加処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertInkaPhoto(ByVal ds As DataSet) As DataSet

        'データの更新
        MyBase.CallDAC(Me._Dac, "InsertInkaPhoto", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 写真データ追加処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateNpPhoto(ByVal ds As DataSet) As DataSet

        'データの更新
        MyBase.CallDAC(Me._Dac, "UpdateNpPhoto", ds)

        Return ds

    End Function


#End Region

#Region "削除処理"

    ''' <summary>
    ''' 入荷写真データ削除処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteInkaPhoto(ByVal ds As DataSet) As DataSet

        'データの更新
        MyBase.CallDAC(Me._Dac, "DeleteInkaPhoto", ds)

        Return ds

    End Function


#End Region


#End Region

End Class
