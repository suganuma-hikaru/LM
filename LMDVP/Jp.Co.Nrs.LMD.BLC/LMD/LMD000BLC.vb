' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD000    : 在庫共通（最終請求日取得）
'  作  成  者       :  [金]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD000BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD000BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD000DAC = New LMD000DAC()

    'データセット名称
    Private Const TABLE_NM_IN As String = "LMD000IN"
    Private Const TABLE_NM_OUT As String = "LMD000OUT"

#End Region

#Region "Method"

#Region "日付検索処理"

    ''' <summary>
    ''' 最終請求日検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectChkDate(ByVal ds As DataSet) As DataSet

        Dim chkDate As String = String.Empty
        Dim dr As DataRow = Nothing

        'データの抽出
        ds = Me.DacAccess(ds, "SelectSeiqDate")

        Dim dt As DataTable = ds.Tables(LMD000BLC.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        '請求日判定
        For i As Integer = 0 To max
            dr = dt.Rows(i)
            chkDate = dr.Item("CHK_DATE").ToString()
            If String.IsNullOrEmpty(chkDate) = False AndAlso _
               chkDate <= dt.Rows(i).Item("HOKAN_NIYAKU_CALCULATION").ToString() = True Then
                MyBase.SetMessage("E375", New String() {dt.Rows(i).Item("REPLACE_STR1").ToString(), dt.Rows(i).Item("REPLACE_STR2").ToString()})
                Return ds
            End If
        Next

        Return ds

    End Function

#End Region

#End Region

#Region "チェック"

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


    ''' <summary>
    ''' 全ての行にValueの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="value">設定したい値</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetValueData(ByVal ds As DataSet, ByVal tblNm As String, ByVal colNm As String(), ByVal value As String()) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim count As Integer = value.Length - 1

        For i As Integer = 0 To max


            For j As Integer = 0 To count

                dt.Rows(i).Item(colNm(j)) = value(j)

            Next

        Next

        Return ds

    End Function

#End Region

End Class
