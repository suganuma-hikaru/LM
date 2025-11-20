' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 在庫管理
'  プログラムID     :  LMD040    : 在庫履歴照会
'  作  成  者       :  [高道]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMD040BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD040BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD040DAC = New LMD040DAC()

    '2017/09/25 修正 李↓
    ''20151106 tsunehira add
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = MessageManager.MessageLanguage
    '2017/09/25 修正 李↑

#End Region

#Region "Const"

    '2015.11.02 tusnehira add
    '英語化対応
    Private Const MESSEGE_LANGUAGE_JAPANESE As String = "0"
    Private Const MESSEGE_LANGUAGE_ENGLISH As String = "1"

#End Region

#Region "Method"

#Region "検索処理"

#Region "データ件数検索"

    ''' <summary>
    ''' データ件数検索（商品）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoods(ByVal ds As DataSet) As DataSet

        Return Me.SelectCount(ds, "SelectDataGoods")

    End Function

    ''' <summary>
    ''' データ件数検索（商品・ロット・入目）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataGoodsLot(ByVal ds As DataSet) As DataSet

        Return Me.SelectCount(ds, "SelectDataGoodsLot")

    End Function

    ''' <summary>
    ''' データ件数検索（商品・ロット・置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataOkibaLot(ByVal ds As DataSet) As DataSet

        Return Me.SelectCount(ds, "SelectDataOkibaLot")

    End Function

    ''' <summary>
    ''' データ件数検索（置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataOkiba(ByVal ds As DataSet) As DataSet

        Return Me.SelectCount(ds, "SelectDataOkiba")

    End Function

    ''' <summary>
    ''' データ件数検索（詳細）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDataAll(ByVal ds As DataSet) As DataSet

        Return Me.SelectCount(ds, "SelectDataAll")

    End Function

    ''' <summary>
    ''' データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        Return Me.SelectCount(ds, "SelectData")

    End Function

    ''' <summary>
    ''' データ件数共通DACコールクラス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCount(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        ds = MyBase.CallDAC(Me._Dac, actionId, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        '件数判定処理をコール
        Me.CountHandling(count)

        Return ds

    End Function

    ''' <summary>
    ''' データ件数判定処理
    ''' </summary>
    ''' <param name="count">Integer</param>
    ''' <remarks></remarks>
    Private Sub CountHandling(ByVal count As Integer)
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
    End Sub

#End Region
#Region "データ検索"

    ''' <summary>
    ''' データ検索（商品）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataGoods(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataGoods", ds)

    End Function

    ''' <summary>
    ''' データ検索（商品・ロット・入目）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataGoodsLot(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataGoodsLot", ds)

    End Function

    ''' <summary>
    ''' データ検索（商品・ロット・置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataOkibaLot(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataOkibaLot", ds)

    End Function

    ''' <summary>
    ''' データ検索（商品・ロット・置場）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataOkiba(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataOkiba", ds)

    End Function

    ''' <summary>
    ''' データ検索（詳細）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataAll(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataAll", ds)

    End Function

    ''' <summary>
    ''' データ検索（入荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataInka(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataInka", ds)

    End Function

    ''' <summary>
    ''' データ検索（在庫）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectListDataメソッド呼出</remarks>
    Private Function SelectListDataZaiko(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectListDataZaiko", ds)

    End Function

    'ADD START 2019/8/27 依頼番号:007116,007119
    ''' <summary>
    ''' 空棚参照のデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecutionEmptyRack(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectEmptyRack", ds)

    End Function

    ''' <summary>
    ''' 在庫差異リストのデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ExecutionZaikoDiff(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectZaikoDiff", ds)

    End Function
    'ADD END 2019/8/27 依頼番号:007116,007119

#End Region

#End Region '検索処理
#End Region

End Class
