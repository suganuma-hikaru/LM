' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI730  : 運賃差分抽出（JXTG）
'  作  成  者       :  katagiri
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI730BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI730BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMI730BLC = New LMI730BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 運賃検索対象データ件数検索アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_COUNT As String = "SelectData"

    ''' <summary>
    ''' バックアップ運賃登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_BKUNCHIN As String = "InsertBackupUnchin"

    ''' <summary>
    ''' バックアップ運賃更新アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_BKUNCHIN As String = "UpdateBackupUnchin"

    ''' <summary>
    ''' 運賃バックアップテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_BKUNCHIN As String = "UNCHIN_BACKUP"

    ''' <summary>
    ''' 運賃バックアップ登録用テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI730IN"

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 運送(大)検索対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = Me.BlcAccess(ds, LMI730BLF.ACTION_ID_COUNT)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運賃バックアップ作成/更新
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BackupUnchin(ByVal ds As DataSet) As DataSet

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            ds = Me.BlcAccess(ds, LMI730BLF.ACTION_ID_INSERT_BKUNCHIN)
            ds = Me.BlcAccess(ds, LMI730BLF.ACTION_ID_UPDATE_BKUNCHIN)

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
