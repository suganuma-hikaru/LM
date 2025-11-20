' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH
'  プログラムID     :  LMH820BLC : 入荷確認データ取込(UTI)
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH820BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH820BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH820DAC = New LMH820DAC()

#End Region

#Region "Method"

#Region "更新処理"

    ''' <summary>
    ''' UTI入荷確認EDIデータ(HED)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertHInkaEdiHedUti(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertHInkaEdiHedUti", ds)

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertHInkaEdiDtlUti(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertHInkaEdiDtlUti", ds)

    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' UTI入荷確認EDIデータ(HED)削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteHInkaEdiHedUti(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteHInkaEdiHedUti", ds)

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ(DTL)削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteHInkaEdiDtlUti(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteHInkaEdiDtlUti", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' EDIフォルダパスマスタより取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetEdiFolderPass(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetEdiFolderPass", ds)

        Return ds

    End Function

    ''' <summary>
    ''' UTI入荷確認EDIデータ存在チェック(Delivery №)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetHinkaEdiHedCheck(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "GetHinkaEdiHedCheck", ds)

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

#End Region

End Class
