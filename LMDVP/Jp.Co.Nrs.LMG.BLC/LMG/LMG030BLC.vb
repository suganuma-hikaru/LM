' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG030BLC : 保管料荷役料明細編集
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMG030BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG030BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG030DAC = New LMG030DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理(変動保管料情報)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectVarStrage(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectVarStrage", ds)

    End Function

    ''' <summary>
    ''' 検索処理(件数取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 初期検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "更新処理"

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpDateTable(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpDateTable", ds)

    End Function

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IkkatuUpDateTable(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "IkkatuUpDateTable", ds)

    End Function
    'END YANAI 20111014 一括変更追加

#End Region

#Region "取込処理"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Acquisithion(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "Acquisithion", ds)

    End Function

    ''' <summary>
    ''' 取込排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function AcquisithionHaita(ByVal ds As DataSet) As DataSet

        ds = (MyBase.CallDAC(Me._Dac, "AcquisithionHaita", ds))

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("E257")
        End If

        Return ds

    End Function

#End Region

#Region "排他処理"

    ''' <summary>
    ''' 排他処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckHaita(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "CheckHaita", ds)

    End Function

#End Region

#End Region

End Class
