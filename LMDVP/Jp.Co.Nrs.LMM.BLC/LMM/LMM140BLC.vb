' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM140BLC : ZONEマスタ
'  作  成  者       :  菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM140BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM140BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM140DAC = New LMM140DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' ZONEマスタ更新対象データ件数検索
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
    ''' ZONEマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        ds = MyBase.CallDAC(Me._Dac, "SelectListData2", ds)

        ds = MyBase.CallDAC(Me._Dac, "SelectListData3", ds)

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' ZONEマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistZoneM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistZoneM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaZoneM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectZoneM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ZONEマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertZoneM(ByVal ds As DataSet) As DataSet

        'Return MyBase.CallDAC(Me._Dac, "InsertZoneM", ds)

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

        'ZONEマスタの新規登録
        Dim rtnResult As Boolean = Me.InsertZoneData(ds)

        'ZONEマスタ消防情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelZoneShoboData(ds)

        'ZONEマスタ消防情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertZoneShoboData(ds)

        '棟室ゾーンチェックマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

        '棟室ゾーンチェックマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituZoneChkData(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' ZONEマスタ 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertZoneData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertZoneM")

    End Function

    ''' <summary>
    ''' ZONEマスタ消防情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelZoneShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelZoneShoboM")

    End Function

    ''' <summary>
    ''' ZONEマスタ消防情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertZoneShoboData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM140_ZONE_SHOBO").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertZoneShoboM")
        End If

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouSituZoneChkData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTouSituZoneChkM")

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituZoneChkData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM140_TOU_SITU_ZONE_CHK").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTouSituZoneChkM")
        End If

    End Function

    ''' <summary>
    ''' ZONEマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateZoneM(ByVal ds As DataSet) As DataSet

        'Return MyBase.CallDAC(Me._Dac, "UpdateZoneM", ds)

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

        'ZONEマスタの更新登録
        Dim rtnResult As Boolean = Me.UpdateZoneData(ds)

        'ZONEマスタ消防情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelZoneShoboData(ds)

        'ZONEマスタ消防情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertZoneShoboData(ds)

        '棟室ゾーンチェックマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

        '棟室ゾーンチェックマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituZoneChkData(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' ZONEマスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateZoneData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateZoneM")

    End Function

    ''' <summary>
    ''' Zoneマスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteZoneM(ByVal ds As DataSet) As DataSet

        'Return MyBase.CallDAC(Me._Dac, "DeleteZoneM", ds)

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

        'ZONEマスタの論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteZoneM")

        'ZONEマスタ消防情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteZoneShoboM")

        '棟室ゾーンチェックマスタ情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTouSituZoneChkM")

        Return rtnResult

    End Function

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

#End Region

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

#End Region

End Class
