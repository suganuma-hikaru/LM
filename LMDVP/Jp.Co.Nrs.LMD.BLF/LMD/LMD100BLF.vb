' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫
'  プログラムID     :  LMD100    : 在庫テーブル照会
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMD100BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD100BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD100BLC = New LMD100BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(_Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectListData", ds)

    End Function

    ''' <summary>
    ''' 振替商品検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectFuriGoodsData(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(_Blc, "SelectFuriGoodsData", ds)

    End Function

#End Region

#End Region

End Class
