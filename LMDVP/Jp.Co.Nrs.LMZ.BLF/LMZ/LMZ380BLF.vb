' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ380    : 物産アニマルヘルス在庫選択
'  作  成  者       :  HORI
' ==========================================================================

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC

''' <summary>
''' LMZ380BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMZ380BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMZ380BLC = New LMZ380BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then
            '件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
        End If

        'データ取得
        Return MyBase.CallBLC(Me._Blc, "SelectListData", ds)

    End Function

#End Region

#End Region

End Class
