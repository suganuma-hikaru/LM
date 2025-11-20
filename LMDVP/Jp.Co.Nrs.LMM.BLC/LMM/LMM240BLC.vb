' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタメンテ
'  プログラムID     :  LMM240BLC : 帳票パターンマスタ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM240BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM240BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM240DAC = New LMM240DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 帳票パターンマスタ更新対象データ件数検索
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
    ''' 帳票パターンマスタ更新対象データ検索
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
    ''' 帳票パターンマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistRptM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistRptM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaRptM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectRptM", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertRptM(ByVal ds As DataSet) As DataSet

        Return Me.ServerDacAccess(ds, "InsertRptM")

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateRptM(ByVal ds As DataSet) As DataSet

        Return Me.ServerDacAccess(ds, "UpdateRptM")

    End Function

    ''' <summary>
    ''' 帳票パターンマスタ削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteRptM(ByVal ds As DataSet) As DataSet

        Return Me.ServerDacAccess(ds, "DeleteRptM")

    End Function

    ''' <summary>
    ''' DACアクセス
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="methodNm">メソッド名</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerDacAccess(ByVal ds As DataSet, ByVal methodNm As String) As DataSet

        Return MyBase.CallDAC(Me._dac, methodNm, ds)

    End Function

#End Region

#End Region

End Class
