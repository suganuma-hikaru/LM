' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB040BLC : 入荷検品選択
'  作  成  者       :  小林
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMB040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB040DAC = New LMB040DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 入荷検品選択対象データ件数検索
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
    ''' 入荷検品選択対象データ検索
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
    ''' 入荷取込フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateInkaToriFlg(ByVal ds As DataSet) As DataSet

        'データの更新
        MyBase.CallDAC(Me._Dac, "UpdateInkaToriFlg", ds)

        '更新件数が0の場合は排他エラー
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '2015.10.26 tusnehira add
            '英語化対応
            MyBase.SetMessage("E775")
            'MyBase.SetMessage("E256", New String() {"入荷検品ワーク"})
        End If

        Return ds

    End Function


#End Region

#Region "削除処理"

    ''' <summary>
    ''' 入荷取込フラグ更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        'データの更新
        MyBase.CallDAC(Me._Dac, "DeleteAction", ds)

        '更新件数が0の場合は排他エラー
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            MyBase.SetMessage("E256", New String() {"入荷検品ワーク"})
        End If

        Return ds

    End Function


#End Region


#End Region

End Class
