' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM400BLC : 西濃マスタ
'  作  成  者       :  adachi
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM400BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM400BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM400DAC = New LMM400DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 西濃マスタ更新対象データ件数検索
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
    ''' 西濃マスタ更新対象データ検索
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
    ''' 西濃マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistSeinoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistSeinoM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E022", New String() {"郵便番号：" + ds.Tables("LMM400IN").Rows(0).Item("ZIP_NO").ToString})
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 必須項目存在チェック 3/17 未使用
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckAllItem(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables("LMM400IN").Rows(0)

        ds = MyBase.CallDAC(Me._Dac, "CheckAllItem", ds)

        Return ds

    End Function



    ''' <summary>
    ''' 西濃マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaSeinoM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckHaitaSeinoM", ds)

        '処理件数による判定
        'If MyBase.GetResultCount() < 1 Then
        '    '1件ＨＩＴしなかった場合、他者更新メッセージを設定する
        '    MyBase.SetMessage("E011")
        'End If

        Return ds

    End Function

    ''' <summary>
    ''' 西濃マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSeinoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertSeinoM", ds)

    End Function

    ''' <summary>
    ''' 西濃マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSeinoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateSeinoM", ds)

    End Function

    ''' <summary>
    ''' 西濃マスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteSeinoM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteSeinoM", ds)

    End Function

#End Region

#End Region

End Class
