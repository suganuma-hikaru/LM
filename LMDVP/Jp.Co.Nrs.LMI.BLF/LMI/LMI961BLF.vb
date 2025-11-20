' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI961  : GLIS見積情報照会（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.GL.DSL

''' <summary>
''' LMI961BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI961BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI961BLC = New LMI961BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_KENSAKU_IN As String = "LMI961KENSAKU_IN"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Public Const TABLE_NM_GAMEN_IN As String = "LMI961GAMEN_IN"

#End Region

#Region "Method"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        If (MyBase.IsMessageExist) Then
            Return ds
        End If

        If ds.Tables("GLZ9300IN_EST_LIST").Rows.Count <> 1 Then
            MyBase.SetMessage("E334", New String() {"[Load Number = " & ds.Tables(TABLE_NM_GAMEN_IN).Rows(0).Item("SHIPMENT_ID").ToString() & "]"})
            Return ds
        End If

        'GLZ9300BLF.SelectEstimateListForHwlの入力パラメタの設定
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("EXP_FLG") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("EXP_FLG").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("IMP_FLG") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("IMP_FLG").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("LOCAL_FLG") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("LOCAL_FLG").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("EST_NO") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("EST_NO").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("EST_NO_EDA") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("EST_NO_EDA").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("FWD_USER_NM") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("FWD_USER_NM").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("EST_MAKE_USER_NM") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("EST_MAKE_USER_NM").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("GOODS_NM") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("GOODS_NM").ToString
        ds.Tables("GLZ9300IN_EST_LIST").Rows(0).Item("SEARCH_REM") = ds.Tables(TABLE_NM_KENSAKU_IN).Rows(0).Item("SEARCH_REM").ToString

        'GLISのBLF呼び出し
        Dim retDs As DataSet = MyBase.CallGLWSA("GLZ9300BLF", "SelectEstimateListForHwl", ds)

        Return retDs

    End Function

    ''' <summary>
    ''' 受注作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JuchuSakusei(ByVal ds As DataSet) As DataSet

        ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        If (MyBase.IsMessageExist) Then
            Return ds
        End If

        If ds.Tables("GLZ9300IN_BOOKING_DATA").Rows.Count <> 1 Then
            MyBase.SetMessage("E334", New String() {"[Load Number = " & ds.Tables(TABLE_NM_GAMEN_IN).Rows(0).Item("SHIPMENT_ID").ToString() & "]"})
            Return ds
        End If

        'GLISのBLF呼び出し
        Dim retDs As DataSet = MyBase.CallGLWSA("GLZ9300BLF", "InsertProvBookingForHwl", ds)

        Return retDs

    End Function

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

#End Region

End Class
