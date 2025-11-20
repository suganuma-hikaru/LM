' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMH060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH060BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH060DAC = New LMH060DAC()

    ''' <summary>
    ''' データ作成判定フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ErrFlg As Boolean = False

#End Region

#Region "Method"

    ''' <summary>
    ''' 検索対象データのデータ件数検索処理
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
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索対象データの検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectKensakuData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectKensakuData", ds)

    End Function

    ''' <summary>
    ''' 荷主セット処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateNinushiSet(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        '荷主セット処理
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdateNinushiSet", ds)

        Return ds

    End Function

    ''' <summary>
    ''' キャンセル処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateCancel(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing

        'キャンセル処理
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdateCancel", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 登録処理(DIC荷主の場合)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateHozonDic(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Nothing
        Dim rtnResult As Boolean = False

        'DIC出荷EDI受信データヘッダ更新処理
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdateHozonDicHed", ds)
        'エラーがあるかを判定
        rtnResult = Not MyBase.IsMessageExist()
        If rtnResult = False Then
            'エラーがある場合は終了
            Return ds
        End If


        'DIC出荷EDI受信データ明細更新処理
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdateHozonDicDtl", ds)
        'エラーがあるかを判定
        rtnResult = Not MyBase.IsMessageExist()
        If rtnResult = False Then
            'エラーがある場合は終了
            Return ds
        End If


        'EDI出荷データ更新処理
        rtnDs = MyBase.CallDAC(Me._Dac, "UpdateHozonDic", ds)
        'エラーがあるかを判定
        rtnResult = Not MyBase.IsMessageExist()
        If rtnResult = False Then
            'エラーがある場合は終了
            Return ds
        End If


        'DIC日陸荷主テーブル新規追加処理
        rtnDs = MyBase.CallDAC(Me._Dac, "InsertHozonDicCust", ds)
        'エラーがあるかを判定
        rtnResult = Not MyBase.IsMessageExist()
        If rtnResult = False Then
            'エラーがある場合は終了
            Return ds
        End If

        Return ds

    End Function

#End Region

End Class
