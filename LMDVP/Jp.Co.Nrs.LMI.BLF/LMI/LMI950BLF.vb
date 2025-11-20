' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI950  : 運賃データ確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI950BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI950BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI950BLC = New LMI950BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 対象データ件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT As String = "SelectData"

    ''' <summary>
    ''' 実績作成アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SENDUNCHIN As String = "InsertSendUnchin"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.BlcAccess(ds, LMI950BLF.ACTION_ID_COUNT)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 実績作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function InsertSendUnchin(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI950BLF.ACTION_ID_INSERT_SENDUNCHIN)

            If (MyBase.IsMessageExist() = True) Then
                Return ds

            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)

        End Using

        Return ds

    End Function

    ''' <summary>
    ''' BLCクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function BlcAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallBLC(Me._Blc, actionId, ds)

    End Function

#End Region


#End Region

End Class
