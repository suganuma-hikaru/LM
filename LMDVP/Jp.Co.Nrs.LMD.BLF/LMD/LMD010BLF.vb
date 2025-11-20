' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD       : 在庫管理
'  プログラムID     :  LMD010BLC : 在庫振替入力
'  作  成  者       :  [高道]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMD010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMD010BLC = New LMD010BLC()

#End Region

#Region "Method"

#Region "日付検索処理"

    ''' <summary>
    ''' 移動、請求日取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function SelectChkIdoDate(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectChkIdoDate", ds)

    End Function

#End Region

#Region "荷主明細 BYKキープ品管理 有無 検索"

    ''' <summary>
    ''' 荷主明細 BYKキープ品管理 有無 検索
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function SelectIsBykKeepGoodsCd(ByVal ds As DataSet) As DataSet

        ' 検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectIsBykKeepGoodsCd", ds)

    End Function

#End Region

#Region "申請外の商品保管ルール検索処理"
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 申請外の商品保管ルール検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function getTouSituExp(ByVal ds As DataSet) As DataSet

        Return Me.BlcAccess(ds, "getTouSituExp")

    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end
#End Region

#Region "振替確定処理"

    ''' <summary>
    ''' データ検索処理(振替データ作成用)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>BLCのSelectDataメソッドに飛ぶ</remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        Return Me.ScopeStartEnd(ds, "InsertSaveAction")

    End Function

    ''' <summary>
    ''' 振替伝票印刷
    ''' </summary>
    ''' <param name="lmd600Ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PrintAction(ByVal lmd600Ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        lmd600Ds.Merge(New RdPrevInfoDS)

        Return MyBase.CallBLC(New LMD600BLC(), "DoPrint", lmd600Ds)

    End Function

#If True Then   'ADD 2018/12/15 
    ''' <summary>
    ''' 振替日取得
    ''' </summary>
    ''' <param name="lmd600Ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SelecFurikaebiGet(ByVal lmd600Ds As DataSet) As DataSet

        Dim rdPrevDs As New RdPrevInfoDS
        Dim rdPrevDt As DataTable = rdPrevDs.Tables(LMConst.RD).Clone

        lmd600Ds.Merge(New RdPrevInfoDS)

        Return MyBase.CallBLC(New LMD600BLC(), "SelecFurikaebiGet", lmd600Ds)

    End Function

#End If

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
            ds = Me.BlcAccess(ds, actionStr)

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
