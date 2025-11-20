' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LML       : 協力会社
'  プログラムID     :  LML010BLC : 協力会社
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LML010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LML010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LML010BLC = New LML010BLC()

#End Region

#Region "Method"

#Region "作業チェック　1:作業コードが設定されていないものがあったらNG (作業金額が0円以外が対象)"

    ''' <summary>
    ''' 作業コードチェック１処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function Sagyo_CHK1(ByVal ds As DataSet) As DataSet

        '検索結果 取得
        Return MyBase.CallBLC(Me._Blc, "Sagyo_CHK1", ds)

    End Function

#End Region

#Region "作業チェック　2:作業コードがダブっていたらＮＧ"

    ''' <summary>
    ''' 作業コードチェック２処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function Sagyo_CHK2(ByVal ds As DataSet) As DataSet

        '検索結果 取得
        Return MyBase.CallBLC(Me._Blc, "Sagyo_CHK2", ds)

    End Function

#End Region

#Region "データ作成処理"
    ''' <summary>
    ''' ダータ作成処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function Data_creat(ByVal ds As DataSet) As DataSet
        Dim rtnResult As Boolean = False

        ds = Me.BlcAccess(ds, "Data_creat")

        Return ds
    End Function
#End Region

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' トランザクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionStr">メソッド名</param>
    ''' <param name="selectFlg">再検索フラグ</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function ScopeStartEnd(ByVal ds As DataSet, ByVal actionStr As String, Optional ByVal selectFlg As Boolean = True) As DataSet

        Dim rtnResult As Boolean = False

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            'BLCアクセス
            ''rtnResult = Me.BlcAccess(ds, actionStr)

            'エラーがあるかを判定
            rtnResult = Not MyBase.IsMessageExist()

            If rtnResult = True Then

                'トランザクション終了
                MyBase.CommitTransaction(scope)

            End If

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

End Class
