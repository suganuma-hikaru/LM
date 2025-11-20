' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI540BLF : オフライン出荷検索(FFEM)
'  作  成  者       :  
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI540BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI540BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Method"

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Torikomi(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI540BLC

        ' 取込前チェック
        ds = MyBase.CallBLC(rtnBlc, "TorikomiChk", ds)

        Dim dtHed As DataTable = ds.Tables("LMI540_TORIKOMI_HED")
        If dtHed.Rows(0).Item("ERR_FLG").ToString() = "1" Then
            Return ds
        End If

        Dim setDs As DataSet = ds.Copy()
        Dim setDtHed As DataTable = setDs.Tables("LMI540_TORIKOMI_HED")

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            ' エラーフラグを立てる(更新中の例外が Throw された場合にエラー終了と判定するため)
            setDtHed.Rows(0).Item("ERR_FLG") = "1"

            ' 取込
            setDs = MyBase.CallBLC(rtnBlc, "Torikomi", setDs)

            ' 戻り値判定
            If setDtHed.Rows(0).Item("ERR_FLG").ToString() = "0" Then
                dtHed.Rows(0).Item("ERR_FLG") = "0"
                MyBase.CommitTransaction(scope)
            Else
                dtHed.Rows(0).Item("ERR_FLG") = "1"
            End If

        End Using

        Return ds

    End Function

#End Region ' "取込処理"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Search(ByVal ds As DataSet) As DataSet

        Dim rtnBlc As Base.BLC.LMBaseBLC = New LMI540BLC

        ' 強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
        If Not MyBase.GetForceOparation() Then
            ' 件数
            ds = MyBase.CallBLC(rtnBlc, "SearchCount", ds)

            ' メッセージの判定
            If MyBase.IsMessageExist() Then
                Return ds
            End If
        End If

        '取得
        Return MyBase.CallBLC(rtnBlc, "SearchSelect", ds)

    End Function

#End Region ' "検索処理"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function PrintAndUpdate(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet = ds.Copy

        ' 印刷プレビュー用DataTable
        If ds.Tables(LMConst.RD) Is Nothing Then
            ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        End If
        Dim rtnDt As DataTable = ds.Tables(LMConst.RD)
        rtnDt.Clear()

        ' 印刷処理
        Dim setRptDt As DataTable = Me.DoPrint(setDs).Tables(LMConst.RD)

        ' プレビュー情報を設定
        If setRptDt IsNot Nothing Then
            For dtIdx As Integer = 0 To setRptDt.Rows.Count - 1
                rtnDt.ImportRow(setRptDt.Rows(dtIdx))
            Next
        End If

        ' プレビュー情報がなければ抜ける
        If rtnDt.Rows.Count = 0 Then
            Return ds
        End If

        ' トランザクションを開始
        Using scope As TransactionScope = MyBase.BeginTransaction()
            Dim rtnBlc As Com.Base.BaseBLC = New LMI540BLC

            ' プリントフラグ更新
            setDs = MyBase.CallBLC(rtnBlc, "UpdatePrtFlg", setDs)

            ' エラーの場合、終了
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            ' トランザクションを確定
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim setDs As DataSet() = Nothing
        Dim prtBlc As Com.Base.BaseBLC() = Nothing

        ' 帳票種類による判定
        Select Case ds.Tables("LMI540IN_PRINT").Rows(0).Item("PRINT_SB").ToString()
            Case "99"
                ' 依頼書
                prtBlc = New Com.Base.BaseBLC() {New LMI541BLC()}
                setDs = New DataSet() {ds}

        End Select

        If prtBlc Is Nothing Then
            Return ds
        End If

        Dim rtnDs As DataSet = Nothing
        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        For dsIdx As Integer = 0 To prtBlc.Count - 1
            If setDs Is Nothing Then
                Continue For
            End If

            setDs(dsIdx).Merge(New RdPrevInfoDS)
            rtnDs = MyBase.CallBLC(prtBlc(dsIdx), "DoPrint", setDs(dsIdx))
            rdPrevDt.Merge(setDs(dsIdx).Tables(LMConst.RD))
        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function

#End Region '  "印刷処理"

#End Region ' "Method"

End Class
