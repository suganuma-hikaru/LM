' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ       : システム管理
'  プログラムID     :  LMJ010BLC : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMJ010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMJ010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' LMJ010INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMJ010IN"

    ''' <summary>
    ''' ERRテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' GETU_INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_GETU_IN As String = "GETU_IN"

    ''' <summary>
    ''' GETU_OUTテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_GETU_OUT As String = "GETU_OUT"

    ''' <summary>
    ''' 処理内容 = 指定された荷主のみチェックする
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHORI_SONOTA As String = "99"

    ''' <summary>
    ''' 請求在庫存在エラー
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ERR_SEIQ_DATA As String = "01"

    ''' <summary>
    ''' 月末在庫存在エラー
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ERR_GETSU_DATA As String = "02"

    ''' <summary>
    ''' 初期在庫
    ''' </summary>
    ''' <remarks></remarks>
    Private Const START_DATE As String = "00000000"

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMJ010DAC = New LMJ010DAC()

#End Region

#Region "検索処理"

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 締め荷主の取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectShimeCust(ByVal ds As DataSet) As DataSet

        Dim closeKb As String = ds.Tables(LMJ010BLC.TABLE_NM_IN).Rows(0).Item("CLOSE_KB").ToString()

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Dim msg As String = "該当締め日の荷主"

        '指定荷主の場合
        If LMJ010BLC.SHORI_SONOTA.Equals(closeKb) = True Then
            msg = "指定荷主"
        End If

        Call Me.ChkSelectCount(ds, msg, String.Empty)

        Return ds

    End Function

    ''' <summary>
    ''' 請求在庫の荷主コードを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqZaikoData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function


#End Region

#Region "チェック"

    ''' <summary>
    ''' 件数チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkSelectCountData(ByVal ds As DataSet) As DataSet

        '請求在庫チェック
        Dim chk As Boolean = Me.SelectSeiqZaikoCount(ds)

        '月末在庫チェック
        chk = chk AndAlso Me.SelectGetsuZaikoCount(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 請求在庫チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqZaikoCount(ByVal ds As DataSet) As Boolean

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return Me.ChkSelectCount(ds, "請求在庫データ", LMJ010BLC.ERR_SEIQ_DATA)

    End Function

    ''' <summary>
    ''' 月末在庫チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectGetsuZaikoCount(ByVal ds As DataSet) As Boolean

        '初期在庫の場合、チェックをしない
        If LMJ010BLC.START_DATE.Equals(ds.Tables(LMJ010BLC.TABLE_NM_IN).Rows(0).Item("RIREKI_DATE").ToString()) = True Then
            Return True
        End If

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Return Me.ChkSelectCount(ds, "月末在庫履歴データ", LMJ010BLC.ERR_GETSU_DATA)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 件数チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="kbn">エラー区分</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSelectCount(ByVal ds As DataSet, ByVal msg As String, ByVal kbn As String) As Boolean

        '0件の場合、エラー
        If MyBase.GetResultCount() < 1 Then
            Dim errDt As DataTable = ds.Tables(LMJ010BLC.TABLE_NM_ERR)
            Dim errDr As DataRow = errDt.NewRow()
            errDr.Item("CHK") = kbn
            errDr.Item("ID") = "E223"
            errDr.Item("MSG") = msg
            errDt.Rows.Add(errDr)
            Return False
        End If

        Return True

    End Function

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

End Class
