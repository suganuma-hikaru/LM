' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 請求
'  プログラムID     :  LMF100BLF : 請求印刷指示
'  作  成  者       :  篠原
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF100BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF100BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Print100 As LMF100BLC = New LMF100BLC()
    Private _Print650 As LMF650BLC = New LMF650BLC()





#End Region

#Region "Method"

#Region "検索"
    ''' <summary>
    ''' 都道府県名データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Print100, "ComboData", ds)

    End Function

#End Region

#Region "印刷"

    ''' <summary>
    ''' 印刷処理(都道府県別運送情報一覧)
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function PrintKen(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        ds = MyBase.CallBLC(Me._Print650, "DoPrint", ds)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            Return ds

        End If

        Return ds

    End Function

#End Region

#End Region

End Class
