' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME       : 作業
'  プログラムID     :  LME050    : 作業個数引当
'  作  成  者       :  [矢内正之]
' ==========================================================================

Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LME050BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LME050BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LME050BLC = New LME050BLC()

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

        '強制実行フラグがオフ時のみ（オンになるのは閾値オーバーでも表示する時）
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

#End Region

#Region "設定処理"

#End Region

#End Region

End Class
