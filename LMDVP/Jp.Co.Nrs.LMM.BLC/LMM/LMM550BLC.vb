' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM550BLC : 下払いタリフマスタメンテ
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMM550BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM550BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC    

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM550DAC = New LMM550DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 支払運賃タリフマスタ更新対象データ件数検索
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
    ''' 支払運賃タリフマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '支払運賃タリフマスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '支払運賃タリフマスタ(距離刻み/運賃)データ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        Return ds

    End Function

    ''' <summary>
    ''' 支払運賃タリフマスタExcelデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExcelDataMake(ByVal ds As DataSet) As DataSet

        '支払運賃タリフマスタExcelデータ取得
        ds = Me.DacAccess(ds, "SelectListExcelData")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 支払運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistShiharaiM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistShiharaiM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 支払運賃タリフマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaShiharaiM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectShiharaiM", ds)

        'MAX支払タリフコード枝番のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMaxShiharaiCdEdaData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 支払タリフコード・2次タリフコード(世代チェック_①)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistShiharaiSedai1(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistShiharaiSedai1", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E205", New String() {"支払タリフコード", "2次タリフコード"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 支払タリフコード・2次タリフコード(世代チェック_②)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistShiharaiSedai2(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistShiharaiSedai2", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、メッセージを設定する
            MyBase.SetMessage("E205", New String() {"2次タリフコード", "支払タリフコード"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 支払タリフコード・2次タリフコード(整合性チェック)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistShiharaiSeigo(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistShiharaiSeigo", ds)

        '処理件数による判定
        If MyBase.GetResultCount() < 1 Then
            '0件の場合、メッセージを設定する
            MyBase.SetMessage("E268", New String() {"2次タリフコード"})
        End If

        Return ds

    End Function

#Region "新規登録"

    ''' <summary>
    ''' 支払運賃タリフマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '新規登録
        Call Me.InsertData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As Boolean

        '支払運賃タリフ情報の新規登録
        Dim rtnResult As Boolean = Me.InsertShiharaiData(ds)

        Return rtnResult

    End Function

#End Region

#Region "更新登録"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Call Me.UpdateData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As Boolean

        '支払運賃タリフ情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateShiharaiData(ds)

        Return rtnResult

    End Function

#End Region

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '削除処理
        Call Me.DeleteData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        '支払運賃タリフ情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteShiharaiM")

        Return rtnResult

    End Function

#End Region

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

#End Region

#Region "共通更新"

    ''' <summary>
    ''' 支払運賃タリフ 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertShiharaiData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertShiharaiTariffM")

    End Function

    ''' <summary>
    ''' 支払運賃タリフ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateShiharaiData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateShiharaiTariffM")

    End Function


    ''' <summary>
    ''' 支払運賃タリフ 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelShiharaiData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelShiharaiTariffM")

    End Function

#End Region

#End Region

End Class
