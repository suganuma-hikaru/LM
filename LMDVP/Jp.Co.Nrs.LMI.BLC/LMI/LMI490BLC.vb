' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI490  : ローム　棚卸対象商品リスト
'  作  成  者       :  kido
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI490BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI490BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI490DAC = New LMI490DAC()

#End Region

#Region "Const"

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        'ADD START 2018/11/27 要望管理002837
        Dim dr As DataRow = ds.Tables("LMI490IN").Rows(0)
        dr.Item("CRT_DATE") = MyBase.GetSystemDate

        '取込済のExcelかチェック(ワークテーブルの主キー存在チェック)
        ds = MyBase.CallDAC(Me._Dac, "SelectExcelDataCnt", ds)

        If MyBase.GetResultCount() > 0 Then
            MyBase.SetMessage("E305", New String() {"ワークテーブルへのExcelデータ", "このファイルは処理済みです。"})
            Return ds
        End If

        'Excelデータをワークテーブルに登録
        ds = MyBase.CallDAC(Me._Dac, "InsertExcelData", ds)

        If MyBase.GetResultCount() < 1 Then
            MyBase.SetMessage("E305", New String() {"ワークテーブルへのExcelデータ", ""})
            Return ds
        End If
        'ADD END   2018/11/27 要望管理002837

        '棚卸対象商品リスト検索
        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'ADD START 2018/11/27 要望管理002837
        'ワークテーブルのステータスフラグを更新
        ds = MyBase.CallDAC(Me._Dac, "UpdateExcelDataStatus", ds)
        'ADD END   2018/11/27 要望管理002837

        Return ds

    End Function

#End Region

End Class
