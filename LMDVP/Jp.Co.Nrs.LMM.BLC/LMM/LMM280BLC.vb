' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM       : マスタ
'  プログラムID     :  LMM280BLC : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMM280BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM280BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM280DAC = New LMM280DAC()

#End Region

#Region "Method"

#Region "編集処理"

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function HaitaChk(ByVal ds As DataSet) As DataSet

        '横持ちタリフマスタ排他チェック
        ds = MyBase.CallDAC(Me._Dac, "HaitaChk", ds)

        Return ds

    End Function

#End Region

#Region "削除/復活処理"

    ''' <summary>
    ''' 横持ちタリフマスタ削除/復活処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As DataSet

        '横持ちタリフヘッダマスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteHedData", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '横持ちタリフ明細マスタ削除/復活
        ds = MyBase.CallDAC(Me._Dac, "DeleteDtlData", ds)

        Return ds

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 横持ちタリフマスタ検索処理(件数取得)
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
    '''初期検索を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>横持ちタリフヘッダマスタ、横持ちタリフ明細マスタ検索</remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '横持ちタリフヘッダ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectHedData", ds)

        '横持ちタリフ明細検索
        ds = MyBase.CallDAC(Me._Dac, "SelectDtlData", ds)

        Return ds

    End Function

#End Region

#Region "保存処理"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistYokoTariffHedM(ByVal ds As DataSet) As DataSet

        ' 横持ちタリフヘッダマスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "ExistYokoTariffHedM", ds)

    End Function

#Region "新規登録/更新"

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ、横持ちタリフ明細マスタ 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '横持ちタリフヘッダマスタ新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertYokoTariffHedM", ds)

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM280_YOKO_TARIFF_DTL").Rows.Count = 0 Then
            Return ds
        End If

        '横持ちタリフ明細マスタ登録
        ds = (MyBase.CallDAC(Me._Dac, "InsertYokoTariffDtlM", ds))

        Return ds

    End Function

    ''' <summary>
    ''' 横持ちタリフヘッダマスタ、横持ちタリフ明細マスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        '横持ちタリフヘッダマスタ更新登録
        ds = MyBase.CallDAC(Me._Dac, "UpdateYokoTariffHedM", ds)
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '横持ちタリフ明細マスタ物理削除
        ds = (MyBase.CallDAC(Me._Dac, "DeleteYokoTariffDtlM", ds))

        '更新登録する明細データがない場合、処理終了
        If ds.Tables("LMM280_YOKO_TARIFF_DTL").Rows.Count = 0 Then
            Return ds
        End If

        '横持ちタリフ明細マスタ登録
        ds = (MyBase.CallDAC(Me._Dac, "InsertYokoTariffDtlM", ds))

        Return ds

    End Function

#End Region

#End Region

#End Region

End Class
