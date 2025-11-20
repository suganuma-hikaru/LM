' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 支払
'  プログラムID     :  LMK060BLF : 支払印刷指示
'  作  成  者       :  yamanaka
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMK060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMK060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print600 As LMF600BLC = New LMF600BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print610 As LMF610BLC = New LMF610BLC()

#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷処理(支払運賃明細)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintMeisai(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print600, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(支払運賃チェックリスト)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintCheck(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print610, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
