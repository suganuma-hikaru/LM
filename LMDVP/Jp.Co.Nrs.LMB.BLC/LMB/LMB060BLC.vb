' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 入荷管理
'  プログラムID     :  LMB060BLC : 入庫連絡票
'  作  成  者       :  hojo
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMB060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB060DAC = New LMB060DAC()



#End Region

#Region "Method"

#Region "登録処理"

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertAction(ByVal ds As DataSet) As DataSet

        '採番
        Dim inkaNoL As String = Me.GetInkaNoL(ds)
        Dim value As String() = New String() {inkaNoL}
        Dim colNm As String() = New String() {"INKA_NO_L"}

        '入荷(大)に採番した値を設定
        ds = Me.SetValueData(ds, "LMB060IN", colNm, value)

        '入荷(大)新規登録
        Call Me.InsertInkaLData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 入荷(大)新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertInkaLData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertInkaLData")

    End Function
#End Region

#Region "印刷処理"
    ''' <summary>
    ''' 入荷受付表のインパラメータ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB550InData(ByVal ds As DataSet) As DataSet

        Dim inDs As DSL.LMB550DS = New DSL.LMB550DS
        Dim dt As DataTable = inDs.Tables("LMB550IN")
        Dim dr As DataRow = dt.NewRow()
        With ds.Tables("LMB060IN").Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            dr.Item("INKA_NO_L") = .Item("INKA_NO_L").ToString()
            dr.Item("USER_CD") = ""

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

#End Region

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
    ''' INKA_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>InkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetInkaNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, ds.Tables("LMB060IN").Rows(0).Item("NRS_BR_CD").ToString())

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

End Class
