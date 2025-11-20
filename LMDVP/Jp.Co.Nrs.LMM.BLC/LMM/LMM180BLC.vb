' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM180BLC : JISマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM180BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM180BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM180DAC = New LMM180DAC()



#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' JISマスタ更新対象データ件数検索
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
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 都道府県名データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ComboData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "ComboData", ds)


    End Function

    ''' <summary>
    ''' JISマスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListData", ds)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' JISマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistJisM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistJisM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' JISマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaJisM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectJisM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' JISマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertJisM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "InsertJisM", ds)

    End Function

    ''' <summary>
    ''' JISマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateJisM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "UpdateJisM", ds)

    End Function

    ''' <summary>
    ''' JISマスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteJisM(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "DeleteJisM", ds)

    End Function

#End Region

#End Region

End Class
