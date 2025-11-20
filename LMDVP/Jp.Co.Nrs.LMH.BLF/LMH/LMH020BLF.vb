' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDIサブ
'  プログラムID     :  LMH020BLC : EDI入荷編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF020BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH020BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMH020BLC = New LMH020BLC()

#End Region

#Region "Const"

    ''' <summary>
    ''' INテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMH020IN"

    ''' <summary>
    ''' INKAEDI_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_L As String = "INKAEDI_L"

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveItemData(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, System.Reflection.MethodBase.GetCurrentMethod.Name, True, True)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 編集処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectEditData(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet _
                                   , ByVal actionId As String _
                                   , ByVal selectFlg As Boolean _
                                   , ByVal updateUnchin As Boolean _
                                   ) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '更新処理
            ds = Me.BlcAccess(ds, actionId)

            'エラー判定
            rtnResult = Not MyBase.IsMessageExist()
            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

        End Using

        '更新成功の場合
        If selectFlg = True AndAlso rtnResult = True Then

            '登録しいた情報で再検索
            Dim inTbl As DataTable = ds.Tables(LMH020BLF.TABLE_NM_IN)
            inTbl.Clear()
            inTbl.ImportRow(ds.Tables(LMH020BLF.TABLE_NM_L).Rows(0))
            ds = Me.SelectData(ds)

        End If

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

End Class
