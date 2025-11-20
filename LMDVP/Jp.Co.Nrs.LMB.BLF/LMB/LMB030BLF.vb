' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 入荷管理
'  プログラムID     :  LMB030BLF : 入荷印刷指示
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB030BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB030BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMB030BLC = New LMB030BLC()

#End Region

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal ds As DataSet) As DataSet

        '入荷L更新
        ds = Me.BlcAccess(ds, "UpdateInkaLPrintData")

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '印刷
        Return Me.DoPrint(ds)

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateAction(ByVal ds As DataSet) As DataSet

        '入荷L更新
        ds = Me.BlcAccess(ds, "UpdateInkaState")

        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

    End Function

    ''' <summary>
    ''' 印刷処理実行
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Me.SetPrtInDs(ds)
        Dim prtBlc As Com.Base.BaseBLC

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        '入荷報告書
        prtBlc = New LMB520BLC()

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs = MyBase.CallBLC(prtBlc, "DoPrint", rtnDs)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 印刷用データセット作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetPrtInDs(ByVal ds As DataSet) As DataSet

        Dim inDs As DataSet = New LMB520DS
        Dim dt As DataTable = inDs.Tables("LMB520IN")
        Dim dr As DataRow = dt.NewRow()

        'データセットにデータ格納
        With ds.Tables("LMB030IN").Rows(0)

            dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD")
            dr.Item("CUST_CD_L") = .Item("CUST_CD_L")
            dr.Item("CUST_CD_M") = .Item("CUST_CD_M")
            dr.Item("INKA_KB") = .Item("INKA_KB")
            dr.Item("INKA_DATE_FROM") = .Item("INKA_DATE_FROM")
            dr.Item("INKA_DATE_TO") = .Item("INKA_DATE_TO")
            dr.Item("SYS_ENT_DATE") = .Item("SYS_ENT_DATE")
            dr.Item("PGID") = MyBase.GetPGID

        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function

#End Region '印刷処理

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

#End Region 'ユーティリティ


#End Region

End Class
