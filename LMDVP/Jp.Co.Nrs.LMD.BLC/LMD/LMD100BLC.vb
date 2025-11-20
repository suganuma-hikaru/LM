' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD100    : 在庫テーブル照会
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMD100BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD100BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMD100DAC = New LMD100DAC()

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

    ''' <summary>
    ''' 在庫テーブルデータ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        '商品情報
        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 在庫テーブル対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(_Dac, "SelectListData", ds)

        '小分けフラグ編集
        Dim outDtName As String = "LMD100OUT"
        Dim outNum As Integer = ds.Tables(outDtName).Rows.Count - 1
        For i As Integer = 0 To outNum
            If ds.Tables(outDtName).Rows(i).Item("SMPL_FLAG").ToString() = "00" Then
                '小分けフラグ = '00'の場合、半角スペースを設定
                ds.Tables(outDtName).Rows(i).Item("SMPL") = " "

            ElseIf ds.Tables(outDtName).Rows(i).Item("SMPL_FLAG").ToString() = "01" Then
                '小分けフラグ = '01'の場合、'*'を設定
                ds.Tables(outDtName).Rows(i).Item("SMPL") = "*"

            Else
                '上記以外の場合
                ds.Tables(outDtName).Rows(i).Item("SMPL") = " "

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 振替商品の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectFuriGoodsData(ByVal ds As DataSet) As DataSet

        '振替商品のデータ取得
        ds = MyBase.CallDAC(Me._Dac, "SelectFuriGoodsData", ds)

        Return ds

    End Function


#End Region

#End Region

End Class
