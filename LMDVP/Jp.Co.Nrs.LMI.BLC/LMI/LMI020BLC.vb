' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : データ管理サブ
'  プログラムID     :  LMI020BLC : デュポン在庫報告
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

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

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI020DAC = New LMI020DAC()

#End Region

#Region "検索処理"

    ''' <summary>
    ''' コンボ用のデータ取得(初期表示用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectLoadData(ByVal ds As DataSet) As DataSet

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMI020BLC.TABLE_NM_GETU_IN)
        Dim inDt As DataTable = ds.Tables(LMI020BLC.TABLE_NM_GETU_IN)
        Dim max As Integer = inDt.Rows.Count - 1
        Dim cnt As Integer = 0
        Dim dt As DataTable = Nothing
        Dim rtnDt As DataTable = ds.Tables(LMI020BLC.TABLE_NM_GETU_OUT)
        For i As Integer = 0 To max

            '検索条件の設定
            selectDt.Clear()
            selectDt.ImportRow(inDt.Rows(i))

            '検索結果の反映
            selectDs = Me.SelectGetuData(selectDs)
            dt = selectDs.Tables(LMI020BLC.TABLE_NM_GETU_OUT)
            cnt = dt.Rows.Count - 1
            For j As Integer = 0 To cnt
                rtnDt.ImportRow(dt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As DataSet
        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
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

End Class
