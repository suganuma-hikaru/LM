' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU010BLC : 文書管理画面
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

Public Class LMU010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMU010DAC = New LMU010DAC()

#End Region

    ''' <summary>
    ''' データ情報検索件数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataCount(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(New LMU010DAC, "SelectDataCount", ds)

        'メッセージコードの設定
        Dim count As Integer = Me.GetResultCount
        Dim limitCnt As Integer = Me.GetLimitCount()

        Debug.Print(CStr(count))

        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
            'ElseIf MyBase.GetMaxResultCount < count Then
            '    '最大値以上の場合
            '    MyBase.SetMessage("G009", New String() {MyBase.GetMaxResultCount.ToString(), count.ToString()})
            'ElseIf MyBase.GetLimitCount() < count Then
            '    '閾値以上の場合
            '    MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ情報取得(BLC)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectDataList(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = Me.CallDAC(New LMU010DAC, "SelectDataList", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function chkDataListHaita(ByVal ds As DataSet) As DataSet

        'InTableの更新日・更新時刻格納
        Dim inTbl As DataTable = ds.Tables("M_FILE")
        Dim guiDateTime As String = Trim(CStr(IIf(String.IsNullOrEmpty(inTbl.Rows(0)("SYS_UPD_DATE").ToString), "", inTbl.Rows(0)("SYS_UPD_DATE")))) & _
                                    Trim(CStr(IIf(String.IsNullOrEmpty(inTbl.Rows(0)("SYS_UPD_TIME").ToString), "", inTbl.Rows(0)("SYS_UPD_TIME"))))

        'DACクラス呼出
        Dim dsTmp As DataSet = Me.CallDAC(New LMU010DAC, "SelectDataListForChkHaita", ds)

        'OutTableの更新日・更新時刻格納
        Dim outTbl As DataTable = dsTmp.Tables("M_FILE")

        Dim dacDateTime As String = String.Empty
        If outTbl IsNot Nothing _
           AndAlso outTbl.Rows.Count <> 0 Then
            dacDateTime = Trim(CStr(IIf(String.IsNullOrEmpty(outTbl.Rows(0)("SYS_UPD_DATE").ToString), "", outTbl.Rows(0)("SYS_UPD_DATE")))) & _
                                        Trim(CStr(IIf(String.IsNullOrEmpty(outTbl.Rows(0)("SYS_UPD_TIME").ToString), "", outTbl.Rows(0)("SYS_UPD_TIME"))))
        End If
        '論理排他チェック
        If guiDateTime.Equals(dacDateTime) = False Then

            Me.SetMessage("E011")

        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(New LMU010DAC, "InsertData", ds)

        '新規登録件数確認
        Dim recCnt As Integer = Me.GetResultCount()

        Debug.Print(CStr(recCnt))

        Return ds

    End Function

    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(New LMU010DAC, "UpdateData", ds)

        '更新件数確認
        Dim recCnt As Integer = Me.GetResultCount

        Debug.Print(CStr(recCnt))

        If recCnt = 0 Then
            '0件の場合
            Me.SetMessage("E011")
        End If

        Return ds

    End Function
    ''' <summary>
    ''' データ更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateWebData(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(New LMU010DAC, "UpdateWebData", ds)

        Return ds

    End Function
    ''' <summary>
    ''' データ論理削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        ds = Me.CallDAC(New LMU010DAC, "DeleteData", ds)

        '更新件数確認
        Dim recCnt As Integer = Me.GetResultCount

        Debug.Print(CStr(recCnt))

        If recCnt = 0 Then
            '0件の場合
            Me.SetMessage("E011")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ論理削除処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function DeleteListData(ByVal ds As DataSet) As DataSet

        '排他チェック
        ds = Me.chkDataListHaita(ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '更新処理
        ds = Me.DeleteData(ds)

        Return ds

    End Function

    ''' <summary>
    '''コンボ作成データ取得　2015/1/6 大野ｱﾙﾍﾞ対応
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '区分マスタ(COM_DB)検索
        ds = MyBase.CallDAC(Me._Dac, "SelectListDataSysID", ds)
        ds = MyBase.CallDAC(Me._Dac, "SelectListDataKanriType", ds)
        ds = MyBase.CallDAC(Me._Dac, "SelectListDataFileType", ds)

        Return ds

    End Function

End Class
