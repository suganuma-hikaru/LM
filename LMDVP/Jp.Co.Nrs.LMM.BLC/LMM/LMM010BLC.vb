' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM010BLC : ユーザーマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM010DAC = New LMM010DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' ユーザーマスタ更新対象データ件数検索
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
    ''' ユーザーマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        'ユーザーマスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '担当者別荷主マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        '要望番号:1248 yamanaka 2013.03.21 Start
        '担当者別運送会社マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListDataTunsoco")

        '担当者別運賃タリフマスタデータ取得
        ds = Me.DacAccess(ds, "SelectListDataTtariff")
        '要望番号:1248 yamanaka 2013.03.21 End

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' ユーザーマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistUserM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistUserM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' ユーザーマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaUserM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectUserM", ds)

        'MAXユーザーコード枝番のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMaxUserCdEdaData", ds)

        Return ds

    End Function

#Region "新規登録"

    ''' <summary>
    ''' ユーザーマスタ新規登録
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

        'ユーザー情報の新規登録
        Dim rtnResult As Boolean = Me.InsertUserData(ds)

        '担当者荷主別情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTcustData(ds)

        '担当者荷主別情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTcustData(ds)

        '要望番号:1248 yamanaka 2013.03.22 Start
        '担当者運送会社情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTunsocoData(ds)

        '担当者運送会社情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTunsocoData(ds)

        '担当者運賃タリフ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTtariffData(ds)

        '担当者運賃タリフ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTtariffData(ds)
        '要望番号:1248 yamanaka 2013.03.22 End

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

        'ユーザー情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateUserData(ds)

        '担当者荷主別情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTcustData(ds)

        '担当者荷主別情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTcustData(ds)

        '要望番号:1248 yamanaka 2013.03.22 Start
        '担当者運送会社情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTunsocoData(ds)

        '担当者運送会社情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTunsocoData(ds)

        '担当者運賃タリフ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTtariffData(ds)

        '担当者運賃タリフ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTtariffData(ds)
        '要望番号:1248 yamanaka 2013.03.22 End
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

        'ユーザー情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUserM")

        '担当者荷主別情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTcustM")

        '要望番号:1248 yamanaka 2013.03.22 Start
        '担当者荷主別情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTunsocoM")

        '担当者荷主別情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTtariffM")
        '要望番号:1248 yamanaka 2013.03.22 End

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
    ''' ユーザー 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertUserData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertUserM")

    End Function

    ''' <summary>
    ''' ユーザー 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateUserData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateUserM")

    End Function

    ''' <summary>
    ''' 担当者別荷主 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTcustData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTcustM")

    End Function

    ''' <summary>
    ''' 担当者別荷主 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTcustData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM010_TCUST").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTcustM")
        End If

    End Function

    '要望番号:1248 yamanaka 2013.03.22 Start
    ''' <summary>
    ''' 担当者別運送会社 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTunsocoData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTunsocoM")

    End Function

    ''' <summary>
    ''' 担当者別運送会社 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTunsocoData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM010_TUNSOCO").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTunsocoM")
        End If

    End Function

    ''' <summary>
    ''' 担当者別運賃タリフ 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTtariffData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTtariffM")

    End Function

    ''' <summary>
    ''' 担当者別運賃タリフ 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTtariffData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM010_TUNCHIN_TARIFF").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTtariffM")
        End If

    End Function
    '要望番号:1248 yamanaka 2013.03.22 End

#End Region

#End Region

End Class
