' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : データ管理サブ
'  プログラムID     :  LMI020BLF : デュポン在庫報告
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI020BLC = New LMI020BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名(SYS_DATETIMEテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_SYS_DATETIME As String = "SYS_DATETIME"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' コンボ用のデータ取得(初期表示用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectLoadData(ByVal ds As DataSet) As DataSet
        'START YANAI 要望番号410
        'ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        'END YANAI 要望番号410
        Return Me.SetSysDateTime(ds)
    End Function

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal ds As DataSet) As DataSet
        'START YANAI 要望番号410
        'ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        'END YANAI 要望番号410
        Return Me.SetSysDateTime(ds)
    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

    ''' <summary>
    ''' システム日時を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetSysDateTime(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMI020BLF.TABLE_NM_SYS_DATETIME)
        dt.Clear()
        Dim dr As DataRow = dt.NewRow()
        dr.Item("SYS_DATE") = MyBase.GetSystemDate()
        dr.Item("SYS_TIME") = MyBase.GetSystemTime()
        dt.Rows.Add(dr)
        Return ds

    End Function

#End Region

#End Region

End Class
