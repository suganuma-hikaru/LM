' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数と日陸在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD080BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD080BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD080BLC = New LMD080BLC()

#End Region

#Region "Method"

    ''' <summary>
    ''' チェック処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function CheckData(ByVal ds As DataSet) As DataSet

        'チェック処理
        ds = MyBase.CallBLC(Me._Blc, "CheckData", ds)
        Return ds

    End Function

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function TorikomiData(ByVal ds As DataSet) As DataSet

        'チェック処理
        ds = MyBase.CallBLC(Me._Blc, "TorikomiData", ds)
        Return ds

    End Function

    ''' <summary>
    ''' 集計処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function ShukeiData(ByVal ds As DataSet) As DataSet

        '集計処理
        ds = MyBase.CallBLC(Me._Blc, "ShukeiData", ds)
        Return ds

    End Function

    ''' <summary>
    ''' 照合処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function ShogohData(ByVal ds As DataSet) As DataSet

        '照合処理
        ds = MyBase.CallBLC(Me._Blc, "ShogohData", ds)
        Return ds

    End Function

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタ取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function ShogohMstData(ByVal ds As DataSet) As DataSet

        '荷主在庫数データ取込制御マスタ取得処理
        ds = MyBase.CallBLC(Me._Blc, "ShogohMstData", ds)
        Return ds

    End Function

#End Region

End Class