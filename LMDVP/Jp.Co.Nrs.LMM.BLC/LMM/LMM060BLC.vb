' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM060BLC : 運賃タリフマスタ
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const

''' <summary>
''' LMM060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC    

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM060DAC = New LMM060DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運賃タリフマスタ更新対象データ件数検索
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
    ''' 運賃タリフマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '運賃タリフマスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '運賃タリフマスタ(距離刻み/運賃)データ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        Return ds

    End Function

    ''' <summary>
    ''' 運賃タリフマスタExcelデータ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExcelDataMake(ByVal ds As DataSet) As DataSet

        '運賃タリフマスタExcelデータ取得
        ds = Me.DacAccess(ds, "SelectListExcelData")

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistUnchinM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistUnchinM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 運賃タリフマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaUnchinM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectUnchinM", ds)

        'MAX運賃タリフコード枝番のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMaxUnchinCdEdaData", ds)

        Return ds

    End Function

#Region "新規登録"

    ''' <summary>
    ''' 運賃タリフマスタ新規登録
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

        '運賃タリフ情報の新規登録
        Dim rtnResult As Boolean = Me.InsertUnchinData(ds)

        ''担当者荷主別情報の物理削除
        'rtnResult = rtnResult AndAlso Me.DelTcustData(ds)

        ''担当者荷主別情報の新規登録
        'rtnResult = rtnResult AndAlso Me.InsertTcustData(ds)

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

        '運賃タリフ情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateUnchinData(ds)

        Return rtnResult

        ''運賃タリフ情報の更新登録
        'Dim rtnResult As Boolean = Me.UpdateUnchinData(ds)

        ''運賃タリフ情報の物理削除
        'rtnResult = rtnResult AndAlso Me.DelUnchinData(ds)

        ''担当者荷主別情報の新規登録
        'rtnResult = rtnResult AndAlso Me.InsertTcustData(ds)

        'Return rtnResult

    End Function

    '''' <summary>
    '''' 更新登録
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function UpdateData(ByVal ds As DataSet) As Boolean

    '    Dim inTbl As DataTable = ds.Tables("LMM060_KYORI")
    '    Dim max As Integer = inTbl.Rows.Count - 1

    '    Dim rtnResult As Boolean = False

    '    For i As Integer = 0 To max

    '        'INTableの更新区分によって新規登録か更新処理を行う。
    '        If inTbl.Rows(i).Item("UPD_FLG").ToString = LMConst.FLG.OFF Then
    '            '運賃タリフ情報の新規登録(更新区分＝'0'の場合)
    '            rtnResult = Me.InsertUnchinData(ds)
    '        Else
    '            '運賃タリフ情報の更新登録(更新区分≠'0'の場合)
    '            'INTableの削除フラグによって物理削除か更新処理を行う。
    '            If inTbl.Rows(i).Item("SYS_DEL_FLG").ToString = LMConst.FLG.ON Then
    '                '運賃タリフ情報の物理削除(削除フラグ＝'1'の場合)
    '                rtnResult = Me.DelUnchinData(ds)
    '            Else
    '                '運賃タリフ情報の更新登録(削除フラグ≠'1'の場合)
    '                rtnResult = Me.UpdateUnchinData(ds)
    '            End If

    '        End If

    '    Next

    '    Return rtnResult

    '    ''運賃タリフ情報の更新登録
    '    'Dim rtnResult As Boolean = Me.UpdateUnchinData(ds)

    '    ''運賃タリフ情報の物理削除
    '    'rtnResult = rtnResult AndAlso Me.DelUnchinData(ds)

    '    ''担当者荷主別情報の新規登録
    '    'rtnResult = rtnResult AndAlso Me.InsertTcustData(ds)

    '    'Return rtnResult

    'End Function

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

        '運賃タリフ情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteUnchinM")

        ''担当者荷主別情報の論理削除
        'rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTcustM")

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
    ''' 運賃タリフ 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertUnchinData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertUnchinTariffM")

    End Function

    ''' <summary>
    ''' 運賃タリフ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnchinData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateUnchinTariffM")

    End Function


    ''' <summary>
    ''' 運賃タリフ 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelUnchinData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelUnchinTariffM")

    End Function

    '''' <summary>
    '''' 担当者別荷主 新規
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function InsertTcustData(ByVal ds As DataSet) As Boolean

    '    '新規登録する明細データがない場合、処理終了
    '    If ds.Tables("LMM060_TCUST").Rows.Count = 0 Then
    '        Return True
    '    Else
    '        Return Me.ServerChkJudge(ds, "InsertTcustM")
    '    End If

    'End Function

#End Region

#Region "承認処理"

    ''' <summary>
    ''' 承認処理（申請、承認、差し戻し）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ApprovalData(ByVal ds As DataSet) As DataSet

        '承認
        ds = MyBase.CallDAC(Me._Dac, "ApprovalData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
