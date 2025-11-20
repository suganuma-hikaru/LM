' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ020    : 未使用荷主データ退避
'  作  成  者       :  [s_kobayashi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMJ020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    Private Const TBLIN As String = "LMJ020IN"
    Private Const TBLNM_J_DATA_ESC_LOG_HEAD As String = "LMJ020IN_J_DATA_ESC_LOG_HEAD"
    Private Const TBLCUSTUPD As String = "M_CUST_UPD"

    Private Const KBN_KEKKA_SUCCESS As String = "00"      '正常終了
    Private Const KBN_KEKKA_ERROR As String = "90"        '異常終了

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMJ020DAC = New LMJ020DAC()


#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 初期出荷マスタ更新対象データ件数検索
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
    ''' 初期出荷マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "実行時チェック"

#Region "基本チェック"

    ''' <summary>
    ''' データ退避処理（基本チェック）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function BasicCheck(ByVal ds As DataSet) As DataSet


        Return ds

    End Function


#End Region

#Region "個別チェック"

#Region "データ退避"

    ''' <summary>
    ''' データ退避処理（入力チェック）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InputCheckTaihi(ByVal ds As DataSet) As DataSet

        '削除されていない荷主かどうか
        ds = Me.InputCheckCust(ds)

        '在庫は０か
        ds = Me.InputCheckZAIKO(ds)

        '前回失敗時の残骸がある場合、削除
        ds = Me.DeletePreData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 可能荷主の存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InputCheckCust(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "InputCheckCUST", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        If count > 0 Then
            '0件の場合
            Me.SetErrorLogHead(ds)
            MyBase.SetMessageStore("00", "E545", New String() {Me.ConcatCustCdAndNmForErrorMsg(ds)})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 前回失敗時の残骸を削除
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DeletePreData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "DeletePreData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 在庫の存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InputCheckZAIKO(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "InputCheckZAIKO", ds)

        'メッセージコードの設定
        Dim poraQT As Integer = MyBase.GetResultCount()

        If poraQT > 0 Then
            '0ではない場合
            Me.SetErrorLogHead(ds)
            MyBase.SetMessageStore("00", "E546", New String() {Me.ConcatCustCdAndNmForErrorMsg(ds)})
        End If

        Return ds

    End Function
#End Region

#Region "データ戻し"

    ''' <summary>
    ''' データ退避処理（入力チェック）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InputCheckModoshi(ByVal ds As DataSet) As DataSet

        '削除されていない荷主かどうか
        ds = Me.InputCheckCust(ds)

        '残骸がある場合、削除

        Return ds

    End Function

#End Region

#End Region


#End Region

#Region "データ退避処理"

    ''' <summary>
    ''' データ退避処理（退避先への登録）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function EscapeData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "EscapeData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ退避処理（退避元の削除が可能かチェック）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataCheck(ByVal ds As DataSet) As DataSet

        '削除してよいか件数のチェック
        ds = MyBase.CallDAC(Me._Dac, "DeleteDataCheck", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ退避処理（退避元の削除）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '削除してよいか件数のチェック
        ds = MyBase.CallDAC(Me._Dac, "DeleteData", ds)

        Return ds

    End Function
#End Region

#Region "データ戻し"

    ''' <summary>
    ''' データ退避処理（退避先への登録）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ModoshiData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "ModoshiData", ds)

        Return ds

    End Function

    ''' <summary>
    ''' データ退避処理（退避元の削除）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteModoshiData(ByVal ds As DataSet) As DataSet

        '削除してよいか件数のチェック
        ds = MyBase.CallDAC(Me._Dac, "DeleteModoshiData", ds)

        Return ds

    End Function
#End Region

#Region "荷主マスタ更新"

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateM_CUST_DEL(ByVal ds As DataSet) As DataSet

        Me.SetDatasetM_CUST_DEL(ds)

        ds = MyBase.CallDAC(Me._Dac, "UpdateM_CUST", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除時のデータセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetM_CUST_DEL(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(TBLCUSTUPD).NewRow

        dr("NRS_BR_CD") = ds.Tables(TBLIN).Rows(0)("NRS_BR_CD").ToString()
        dr("CUST_CD_L") = ds.Tables(TBLIN).Rows(0)("CUST_CD_L").ToString()
        dr("BACKUP_FLG") = "01"
        dr("SYS_DEL_FLG") = "1"

        ds.Tables(TBLCUSTUPD).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 荷主マスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateM_CUST_REBORN(ByVal ds As DataSet) As DataSet

        Me.SetDatasetM_CUST_REBORN(ds)

        ds = MyBase.CallDAC(Me._Dac, "UpdateM_CUST", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 復活時のデータセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetM_CUST_REBORN(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(TBLCUSTUPD).NewRow

        dr("NRS_BR_CD") = ds.Tables(TBLIN).Rows(0)("NRS_BR_CD").ToString()
        dr("CUST_CD_L") = ds.Tables(TBLIN).Rows(0)("CUST_CD_L").ToString()
        dr("BACKUP_FLG") = "00"
        dr("SYS_DEL_FLG") = "0"

        ds.Tables(TBLCUSTUPD).Rows.Add(dr)

    End Sub

#End Region

#Region "Utility"

    ''' <summary>
    ''' スタートログの書き込み
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function StartInputLog(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "StartInputLog", ds)

        Return ds

    End Function

    ''' <summary>
    ''' スタートログの書き込み
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function EndInputLog(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "EndInputLog", ds)

        Return ds

    End Function

    ''' <summary>
    ''' ヘッダエラーのセット
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetErrorLogHead(ByVal ds As DataSet) As DataSet

        ds.Tables(TBLNM_J_DATA_ESC_LOG_HEAD).Rows(0)("EXEC_RESULT_KB") = KBN_KEKKA_ERROR

        Return ds

    End Function

    ''' <summary>
    ''' DsInのコード名称を：区切りで返却
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConcatCustCdAndNmForErrorMsg(ByVal ds As DataSet) As String

        Return String.Concat(ds.Tables(TBLIN).Rows(0).Item("CUST_CD_L").ToString(), ":", ds.Tables(TBLIN).Rows(0).Item("CUST_NM_L").ToString())

    End Function

#End Region

#End Region

End Class
