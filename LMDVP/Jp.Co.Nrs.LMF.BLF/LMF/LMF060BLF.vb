' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブシステム
'  プログラムID     :  LMF060BLF : 運行未登録運送検索
'  作  成  者       :  菱刈
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMF060BLC = New LMF060BLC()


    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc540 As LMF540BLC = New LMF540BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運送Lテーブルデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectListData", ds)

        Return ds
    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataPrint(ByVal ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        ds.Merge(New RdPrevInfoDS)

        'BLCクラスへ
        ds = MyBase.CallBLC(Me._Blc540, "DoPrint", ds)


        '距離、重量、金額の設定
        'ds = Me.SelectListDataKKyori(ds)
        Return ds

    End Function


    '''' <summary>
    '''' 距離、重量、金額の取得テーブルデータ検索処理
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function SelectListDataKKyori(ByVal ds As DataSet) As DataSet

    '    'BLCクラスへ
    '    ds = MyBase.CallBLC(Me._Blc540, "getKyoriRow", ds)


    '    '割増区分の取得
    '    ds = Me.GetWarimasi(ds)
    '    Return ds

    'End Function

    '''' <summary>
    '''' 割増区分の取得
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function GetWarimasi(ByVal ds As DataSet) As DataSet

    '    'BLCクラスへ
    '    ds = MyBase.CallBLC(Me._Blc540, "GetWarimasi", ds)

    '    '印刷実行の呼び出し
    '    ds = Me.DoPrintJikou(ds)

    '    Return ds

    'End Function


    '''' <summary>
    '''' 印刷実行
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <returns>DataSet（プロキシ）</returns>
    '''' <remarks></remarks>
    'Private Function DoPrintJikou(ByVal ds As DataSet) As DataSet

    '    'BLCクラスへ
    '    ds = MyBase.CallBLC(Me._Blc540, "DoPrintJikou", ds)

    '    Return ds

    'End Function

#End Region

#End Region

End Class
