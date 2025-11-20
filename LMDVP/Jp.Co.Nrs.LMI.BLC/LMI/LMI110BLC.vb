' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特殊荷主機能
'  プログラムID     :  LMI110BLC : 日医工製品マスタ登録
'  作  成  者       :  [寺川徹]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMI110BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI110BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI110DAC = New LMI110DAC()

#End Region

#Region "Method"


#Region "検索処理"

    ''' <summary>
    ''' 日医工製品マスタ検索処理(件数取得)
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
    '''日医工製品マスタ検索処理(データ取得)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '商品マスタ検索
        ds = MyBase.CallDAC(Me._Dac, "SelectListData", ds)

       Return ds

    End Function

#End Region

#Region "保存処理"

#Region "チェック"


    ''' <summary>
    ''' 商品マスタ重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ExistGoodsM(ByVal ds As DataSet) As DataSet

        '商品マスタ存在チェック
        Return MyBase.CallDAC(Me._Dac, "ExistGoodsM", ds)

    End Function


 

#End Region

#Region "新規登録/更新"

    ''' <summary>
    ''' 商品マスタ/明細 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As DataSet

        '商品マスタ新規登録
        ds = MyBase.CallDAC(Me._Dac, "InsertGoodsM", ds)

        ' ''新規登録する明細データがない場合、処理終了
        ''If ds.Tables("LMM100_GOODS_DETAILS").Rows.Count = 0 Then
        ''    Return ds
        ''End If

        '要望番号:1250 terakawa 2012.07.12 Start
        '商品明細新規登録
        ds = (MyBase.CallDAC(Me._Dac, "InsertGoodsMDtl", ds))
        '要望番号:1250 terakawa 2012.07.12 End

        Return ds

    End Function

    ''' <summary>
    ''' 商品マスタ/明細 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveData(ByVal ds As DataSet) As DataSet

        '商品マスタ更新登録
        ds = MyBase.CallDAC(Me._Dac, "UpdateGoodsM", ds)

        ''If MyBase.GetResultCount() > 0 Then

        ''    '商品明細物理削除
        ''    ds = (MyBase.CallDAC(Me._Dac, "DeleteGoodsMDtl", ds))

        ''    '更新登録する明細データがない場合、処理終了
        ''    If ds.Tables("LMM100_GOODS_DETAILS").Rows.Count = 0 Then
        ''        Return ds
        ''    End If

        ''    '商品明細新規登録
        ''    ds = (MyBase.CallDAC(Me._Dac, "InsertGoodsMDtl", ds))

        ''End If

        Return ds

    End Function

    ''' <summary>
    ''' 製品マスタ（日医工） 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSehinM(ByVal ds As DataSet) As DataSet

        '製品マスタ（日医工）更新登録
        ds = MyBase.CallDAC(Me._Dac, "UpdateSehinM", ds)

        Return ds

    End Function


#End Region

#End Region

#End Region

End Class
