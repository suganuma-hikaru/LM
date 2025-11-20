' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI440  : 
'  作  成  者       :  [inoue]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI440BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI440BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class TABLE_NAME

        ''' <summary>
        ''' 入力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const INPUT As String = LMI440DAC.TABLE_NAME.INPUT

        ''' <summary>
        ''' 出力テーブル
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OUTPUT As String = LMI440DAC.TABLE_NAME.OUTPUT

        ''' <summary>
        ''' 入力テーブル(輸送データ)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const IN_TRANSPORT As String = LMI440DAC.TABLE_NAME.IN_TRANSPORT


    End Class


    ''' <summary>
    ''' 関数名
    ''' </summary>
    ''' <remarks></remarks>
    Public Class FUNCTION_NAME

        ''' <summary>
        ''' 検品データ検索
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SelectData As String = "SelectData"

    End Class

#End Region

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI440DAC = New LMI440DAC()

#End Region

#Region "Method"

    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ' 一時テーブル作成
        MyBase.CallDAC(Me._Dac, LMI440DAC.FUNCTION_NAME.CreateTempTable, ds)

        ' 取込ファイルデータ登録
        Dim wkData As DataSet = ds.Clone
        For Each transportRow As DataRow In ds.Tables(TABLE_NAME.IN_TRANSPORT).Rows

            wkData.Clear()
            wkData.Tables(TABLE_NAME.IN_TRANSPORT).ImportRow(transportRow)
            MyBase.CallDAC(Me._Dac, LMI440DAC.FUNCTION_NAME.InsertTempTable, wkData)
        Next

        ' 出荷テーブル検索(荷主コード別)
        For Each inRow As DataRow In ds.Tables(TABLE_NAME.INPUT).Rows
            wkData.Clear()
            wkData.Tables(TABLE_NAME.INPUT).ImportRow(inRow)
            wkData = MyBase.CallDAC(Me._Dac, LMI440DAC.FUNCTION_NAME.SelectUnsoData, wkData)

            ' 出荷検索結果マージ
            ds.Tables(TABLE_NAME.OUTPUT).Merge(wkData.Tables(TABLE_NAME.OUTPUT))
        Next

        ' 輸送テーブル検索
        wkData.Clear()
        wkData.Tables(TABLE_NAME.INPUT).Merge(ds.Tables(TABLE_NAME.INPUT))
        wkData = MyBase.CallDAC(Me._Dac, LMI440DAC.FUNCTION_NAME.SelectTransportData, wkData)

        ' 輸送検索結果マージ
        ds.Tables(TABLE_NAME.OUTPUT).Merge(wkData.Tables(TABLE_NAME.OUTPUT))

        Return ds

    End Function

#End Region

End Class
