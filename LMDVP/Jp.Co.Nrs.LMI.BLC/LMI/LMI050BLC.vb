' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI050BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI050BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI050DAC = New LMI050DAC()

#End Region

#Region "Method"

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMonthlySnz(ByVal ds As DataSet) As DataSet

        '篠崎運送専用の月末在庫テーブルからデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectMonthlySnz", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 月末在庫データの検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectZaiZanSnz(ByVal ds As DataSet) As DataSet

        '月末在庫データの検索
        ds = MyBase.CallDAC(Me._Dac, "SelectZaiZanSnz", ds)

        If ds.Tables("LMI050INOUT_ZAIZAN_SNZ").Rows.Count = 0 Then
            MyBase.SetMessage("E483", New String() {"実行"})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteDataSNZ(ByVal ds As DataSet) As DataSet

        '篠崎運送専用の月末在庫テーブル削除処理
        ds = MyBase.CallDAC(Me._Dac, "DeleteDataSNZ", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertDataSNZ(ByVal ds As DataSet) As DataSet

        '篠崎運送専用の月末在庫テーブル作成処理
        ds = MyBase.CallDAC(Me._Dac, "InsertDataSNZ", ds)

        Return ds

    End Function

#End Region

End Class
