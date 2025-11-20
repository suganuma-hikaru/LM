' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM130H : 棟室マスタメンテナンス
'  作  成  者       :  [kishi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM130BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM130BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM130DAC = New LMM130DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 棟室マスタ更新対象データ件数検索
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
    ''' 棟室マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '棟室マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '棟室マスタ消防データ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

        '申請外の商品保管ルールデータ取得
        ds = Me.DacAccess(ds, "SelectListData3")

        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        '棟室ゾーンチェックマスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData4")

        Return ds

    End Function

    '2017/11/01 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    Private Function SelectListData3(ByVal ds As DataSet) As DataSet
        '申請外の商品保管ルールデータ取得
        ds = Me.DacAccess(ds, "SelectListData3")
        Return ds
    End Function
    '2017/11/01 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 倉庫自社他社判定用データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectSokoJT(ByVal ds As DataSet) As DataSet
        ds = Me.DacAccess(ds, "SelectSokoJT")
        Return ds
    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 棟室マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistTouSituM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistTouSituM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 棟室マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaTouSituM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectTouSituM", ds)

        Return ds

    End Function

#Region "新規登録"

    ''' <summary>
    ''' 棟室マスタ新規登録
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

        '棟室情報の新規登録
        Dim rtnResult As Boolean = Me.InsertTouSituData(ds)

        '棟室マスタ消防情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituShoboData(ds)

        '棟室マスタ消防情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituShoboData(ds)

        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        '申請外の商品保管許可ルールマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituExpData(ds)

        '申請外の商品保管許可ルールマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituExpData(ds)
        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add END

        '棟室ゾーンチェックマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

        '棟室ゾーンチェックマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituZoneChkData(ds)

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

        '棟室情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateTouSituData(ds)

        '棟室マスタ消防情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituShoboData(ds)

        '棟室マスタ消防情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituShoboData(ds)

        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

        rtnResult = rtnResult AndAlso Me.DelTouSituExpData(ds)

        rtnResult = rtnResult AndAlso Me.InsertTouSituExpData(ds)

        '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        '棟室ゾーンチェックマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

        '棟室ゾーンチェックマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouSituZoneChkData(ds)

        Return rtnResult

    End Function

#End Region
#Region "一括登録"
    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private Function IkkatuTourokuExpAction(ByVal ds As DataSet) As DataSet

        Return DacAccess(ds, "IkkatuTourokuExpAction")

    End Function
    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end
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

        '棟室情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteTouSituM")

        '棟室マスタ消防情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTouSituShoboM")

        '棟室ゾーンチェックマスタ情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTouSituZoneChkM")

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
    ''' 棟室マスタ 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertTouSituM")

    End Function

    ''' <summary>
    ''' 棟室マスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateTouSituData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateTouSituM")

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouSituShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTouSituShoboM")

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 申請外の商品保管許可ルールマスタ報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouSituExpData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTouSituExpM")

    End Function
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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
    ''' 棟室マスタ消防情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituShoboData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM130_TOU_SITU_SHOBO").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTouSituShoboM")
        End If

    End Function

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 申請外の商品保管許可ルール情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituExpData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM130_TOU_SITU_EXP").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTouSituExpM")
        End If

    End Function
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituZoneChkData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM130_TOU_SITU_ZONE_CHK").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTouSituZoneChkM")
        End If

    End Function

#End Region

#End Region

End Class
