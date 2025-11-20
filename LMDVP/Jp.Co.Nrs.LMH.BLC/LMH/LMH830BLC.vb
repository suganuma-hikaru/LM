' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH
'  プログラムID     :  LMH830BLC : 未着・早着ファイル作成(UTI)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH830BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH830BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH830DAC = New LMH830DAC()

#End Region

#Region "Method"

#Region "設定処理"

    ''' <summary>
    ''' 入荷・受信合致(不整合なし)データの取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetAgreeDataSql(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetAgreeDataSql", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 未着対象データの取得(UTI入荷確認EDIデータ：有、入荷データ：無)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetMicyakuDataFile(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetMicyakuDataFile", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 早着対象データの取得(UTI入荷確認EDIデータ：無、入荷データ：有)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetSoucyakuDataFile(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetSoucyakuDataFile", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 区分マスタより送信ファイル格納パス取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetSendFolderPass(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetSendFolderPass", ds)

        Return ds

    End Function

    Private Function CopyAndDelete(ByVal tgtFile As String, ByVal CopyTOFolder As String) As Boolean

        Try
            '上書きOKとしてコピー可能
            System.IO.File.Copy(tgtFile, String.Concat(CopyTOFolder, "\", IO.Path.GetFileName(tgtFile)), True)
            'System.IO.File.Delete(tgtFile)

        Catch ex As Exception
            Me.SetMessage("S002")
        End Try

    End Function

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 入荷データ(B_INKA_L)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateBInkaL(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateBInkaL", ds)

    End Function

    ''' <summary>
    ''' 在庫データ(D_ZAI_TRS)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateZaiTrs(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateZaiTrs", ds)

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(HED/DTL)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHInkaEdiHedUti(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateHInkaEdiHedUti", ds)

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHInkaEdiDtlUti(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateHInkaEdiDtlUti", ds)

    End Function

#End Region

#End Region

End Class
