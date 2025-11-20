' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060BLF : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMN060BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN060BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMN060BLC = New LMN060BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectDetail(ByVal ds As DataSet) As DataSet

        '検索結果取得
        Return MyBase.CallBLC(Me._Blc, "SelectDetail", ds)

    End Function

#End Region

#Region "在庫日数"

    ''' <summary>
    ''' 在庫日数算出、更新処理を行う
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdZaikoNissu(ByVal ds As DataSet) As DataSet

        'トランザクション開始

        'TODO:分散トランザクションのエラーのため、コメント
        'Using scope As TransactionScope = MyBase.BeginTransaction()

        '在庫日数算出、更新処理
        ds = MyBase.CallBLC(Me._Blc, "UpdZaikoNissu", ds)

        'TODO:分散トランザクションのエラーのため、コメント
        ''トランザクション終了
        'MyBase.CommitTransaction(scope)
        'End Using

        Return ds

    End Function

#End Region

#Region "LMN520プレビュー表示"

    ''' <summary>
    ''' 商品別過去実績推移プレビュー表示を行う
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function LMN520Preview(ByVal ds As DataSet) As DataSet

        '印刷用インスタンス作成
        Dim prtBlc As Com.Base.BaseBLC
        prtBlc = New LMN520BLC()
        Dim lmn520DS As DataSet = New LMN520DS
        Dim inDr As DataRow = ds.Tables("LMN060IN").Rows(0)

        'INデータ設定
        Dim dr As DataRow = lmn520DS.Tables("LMN520IN").NewRow

        dr.Item("NRS_BR_CD") = inDr.Item("BR_CD").ToString()
        dr.Item("WH_CD") = inDr.Item("SOKO_CD").ToString()
        dr.Item("SCM_CUST_CD") = inDr.Item("SCM_CUST_CD").ToString()
        dr.Item("GOODS_CD_CUST") = inDr.Item("CUST_GOODS_CD").ToString()
        dr.Item("IKO_FLAG") = inDr.Item("IKO_FLG").ToString()

        '出力開始日付の取得
        Dim fromDate As String = String.Empty
        '表示期間指定(6ヵ月)
        Dim months As Integer = 6
        '現在日付取得
        Dim nowDate As String = Me.GetSystemDate()
        '出力開始日付算出
        fromDate = Convert.ToDateTime(Date.Parse(Format(Convert.ToInt32(nowDate), "0000/00/00"))).AddMonths(-months).ToString("yyyyMMdd")
        '出力開始日付設定
        dr.Item("FROM_DATE") = fromDate

        ds = MyBase.CallBLC(prtBlc, "DoPrint", lmn520DS)

        Return ds

    End Function

#End Region


#End Region

End Class
