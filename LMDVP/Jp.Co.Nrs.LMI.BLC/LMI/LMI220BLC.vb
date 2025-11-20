' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI220  : 定期検査管理
'  作  成  者       :  [KIM]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI220BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI220BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI220DAC = New LMI220DAC()

#End Region

#Region "Method"

#Region "HaitaData"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaData(ByVal ds As DataSet) As DataSet

        '排他チェック処理
        ds = MyBase.CallDAC(Me._Dac, "HaitaData", ds)

        Return ds

    End Function

#End Region

#Region "SelectListData"

    ''' <summary>
    ''' 検索データ取得処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '検索データ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

        Return ds

    End Function

#End Region

#Region "IsDataExistChk"

    ''' <summary>
    ''' データ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function IsDataExistChk(ByVal ds As DataSet) As DataSet

        'データ存在チェック処理
        ds = MyBase.CallDAC(Me._Dac, "IsDataExistChk", ds)

        Return ds

    End Function

#End Region

#Region "InsertData"

    ''' <summary>
    ''' 保存（新規登録）処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '保存処理 
        ds = MyBase.CallDAC(Me._Dac, "InsertData", ds)

        Return ds

    End Function

#End Region

#Region "UpdateData"

    ''' <summary>
    ''' 保存（更新）処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As DataSet

        '保存処理 
        ds = MyBase.CallDAC(Me._Dac, "UpdateData", ds)

        Return ds

    End Function

#End Region

#Region "DeleteData"

    ''' <summary>
    ''' 削除処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '削除処理 
        ds = MyBase.CallDAC(Me._Dac, "DeleteData", ds)

        Return ds

    End Function

#End Region

#Region "ReviveData"

    ''' <summary>
    ''' 復活処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ReviveData(ByVal ds As DataSet) As DataSet

        '復活処理 
        ds = MyBase.CallDAC(Me._Dac, "ReviveData", ds)

        Return ds

    End Function

#End Region

#Region "ImportData"

    ''' <summary>
    ''' 取込更新処理 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ImportData(ByVal ds As DataSet) As DataSet

        '保存処理 
        ds = MyBase.CallDAC(Me._Dac, "ImportData", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
