' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI970  : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI970BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI970BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI970BLC = New LMI970BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 対象データ件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT As String = "SelectData"

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SENDUNCHIN As String = "InsertSendUnchin"

#End Region

#Region "Method"

#Region "処理"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.BlcAccess(ds, LMI970BLF.ACTION_ID_COUNT)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ単価一括更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateYusoIraiTanka(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function
    'ADD END   2019/05/30 要望管理006030

    ''' <summary>
    ''' 日産物流輸送依頼ＥＤＩ受信データ更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateYusoIrai(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 実績作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendUnchin(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI970BLF.ACTION_ID_INSERT_SENDUNCHIN)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function PrintData(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "LMI970SENDUNCHIN_TARGET"
        Dim setDs As DataSet = ds.Copy
        Dim setDt As DataTable = setDs.Tables(tableNm)
        Dim dt As DataTable = ds.Tables(tableNm)
        Dim max As Integer = dt.Rows.Count - 1

        'レコードがない場合、スルー
        If max < 0 Then
            Return ds
        End If

        Dim printShubetu As String = ds.Tables("LMI970IN").Rows(0)("PRINT_SHUBETU").ToString()

        'プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing = True Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If

        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        Dim setRptDt As DataTable = Nothing
        Dim cnt As Integer = 0

        '通常の印刷  --(INデータマージなし)
        For i As Integer = 0 To max

            '更新情報の設定
            setDt.Clear()
            setDt.ImportRow(dt.Rows(i))

            '印刷処理
            setRptDt = Me.DoPrint(setDs, printShubetu).Tables(LMConst.RD)

            If MyBase.IsMessageExist() Then
                Return ds
            End If

            'プレビュー情報を設定
            If setRptDt Is Nothing = False Then
                cnt = setRptDt.Rows.Count - 1
                For j As Integer = 0 To cnt
                    rtnDt.ImportRow(setRptDt.Rows(j))
                Next
            End If
        Next

        'ADD START 2019/05/30 要望管理006030
        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, "UpdateIraishoPrintFlg")

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using
        'ADD END   2019/05/30 要望管理006030

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet, ByVal printShubetu As String) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        Select Case printShubetu

            Case "01" '請求明細書

                prtBlc = New Com.Base.BaseBLC() {New LMI980BLC()}
                setDs = New DataSet() {Me.SetDataSetLMI980InData(ds)}

        End Select

        Dim rtnDs As DataSet = Nothing
        Dim max As Integer = prtBlc.Count - 1
        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For i As Integer = 0 To max

            setDs(i).Merge(New RdPrevInfoDS)

            rtnDs = MyBase.CallBLC(prtBlc(i), "DoPrint", setDs(i))

            If MyBase.IsMessageExist() Then
                Return rtnDs
            End If

            rdPrevDt.Merge(setDs(i).Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

    ''' <summary>
    ''' LMI980DSを生成
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMI980InData(ByVal ds As DataSet) As DataSet

        'コピー元:LMI970DS
        Dim lmi970inDr As DataRow = ds.Tables("LMI970IN").Rows(0)
        Dim targetDrs As DataRow() = ds.Tables("LMI970SENDUNCHIN_TARGET").Select
        Dim max As Integer = targetDrs.Length - 1

        'コピー先:LMI980DS
        Dim lmi980Ds As DataSet = New LMI980DS


        'LMI970DS.LMI970SENDUNCHIN_TARGET → LMI980DS.LMI980IN
        Dim lmi980inDt As DataTable = lmi980Ds.Tables("LMI980IN")
        Dim lmi980inDr As DataRow = Nothing

        For i As Integer = 0 To max

            lmi980inDr = lmi980inDt.NewRow
            lmi980inDr.Item("NRS_BR_CD") = lmi970inDr.Item("NRS_BR_CD").ToString()
            lmi980inDr.Item("CUST_CD_L") = lmi970inDr.Item("CUST_CD_L").ToString()
            lmi980inDr.Item("CRT_DATE") = targetDrs(i).Item("CRT_DATE").ToString()
            lmi980inDr.Item("FILE_NAME") = targetDrs(i).Item("FILE_NAME").ToString()
            lmi980inDr.Item("REC_NO") = targetDrs(i).Item("REC_NO").ToString()
            lmi980inDr.Item("SYS_UPD_DATE") = targetDrs(i).Item("SYS_UPD_DATE").ToString()
            lmi980inDr.Item("SYS_UPD_TIME") = targetDrs(i).Item("SYS_UPD_TIME").ToString()
            lmi980inDt.Rows.Add(lmi980inDr)

        Next

        Return lmi980Ds

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

#End Region

End Class
