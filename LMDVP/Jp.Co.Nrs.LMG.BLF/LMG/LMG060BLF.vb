' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 請求
'  プログラムID     :  LMG060BLF : 請求印刷指示
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print510 As LMF510BLC = New LMF510BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print520 As LMF520BLC = New LMF520BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print530 As LMF530BLC = New LMF530BLC()

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print590 As LMF590BLC = New LMF590BLC()

    '(2012.09.25) 追加START 運賃請求明細書(出荷)
    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print630 As LMF630BLC = New LMF630BLC()
    '(2012.09.25) 追加END 運賃請求明細書(出荷)

#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷処理(運賃請求明細)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintSeikyu(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print510, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(運賃請求明細(タリフ))
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintSeikyuTariff(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print520, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(運賃チェックリスト)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintCheck(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print530, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷処理(運賃請求明細(入荷))
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintSeikyuInka(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print590, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

    '(2012.09.25) 追加START 運賃請求明細書(出荷)
    ''' <summary>
    ''' 印刷処理(運賃請求明細(出荷))
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintSeikyuOutka(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print630, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function
    '(2012.09.25) 追加END 運賃請求明細書(出荷)

    '(2013.02.26)要望番号1835 --  START  --
    ''' <summary>
    ''' 印刷処理(運賃請求明細(連続))
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintSeikyuRenzoku(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim inDs As DataSet = ds.Copy
        Dim inDt As DataTable = inDs.Tables("LMF510IN")
        Dim outDt As DataTable = inDs.Tables("LMF510OUT")
        Dim rptDt As DataTable = inDs.Tables("M_PRT")
        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        inDs.Merge(New RdPrevInfoDS)

        For i As Integer = 0 To ds.Tables("LMF510IN").Rows.Count - 1

            inDs.Clear()
            inDt.ImportRow(ds.Tables("LMF510IN").Rows(i))

            rtnDs = MyBase.CallBLC(Me._Print510, "DoPrint", inDs)

            'メッセージ判定
            If MyBase.IsMessageExist = True Then
                'EXCELにメッセージを格納
                MyBase.SetMessageStore("00", "E070", , inDs.Tables("LMF510IN").Rows(0).Item("ROW_NO").ToString())
                'メッセージクリア
                MyBase.SetMessage(Nothing)
                Continue For
            End If

            rdPrevDt.Merge(inDs.Tables(LMConst.RD))

        Next

        rtnDs.Tables(LMConst.RD).Clear()
        rtnDs.Tables(LMConst.RD).Merge(rdPrevDt)

        Return rtnDs

    End Function
    '(2013.02.26)要望番号1835 --  END  --

#End Region

#End Region

End Class
