' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN080BLC : 欠品警告
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN060BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN080BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN080DAC = New LMN080DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    '''  対象倉庫コード、今回引当出荷オーダー数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetSOKO_CD_LIST(ByVal ds As DataSet) As DataSet

        '取得結果格納用データセット
        Dim rtnDs As DataSet = New LMN080DS()
        '取得結果格納テーブル名
        Dim outNm As String = "LMN080OUT_L"
        '列ヘッダ設定内容取得
        ds = MyBase.CallDAC(Me._Dac, "GetSOKO_CD_LIST", ds)

        Return ds

    End Function

    ''' <summary>
    '''  出荷予定品目数取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetPLAN_HINMOKU_NB(ByVal ds As DataSet) As DataSet

        'IN情報テーブル名取得
        Dim inName As String = "LMN080OUT_L"
        '倉庫毎の出荷予定品目数を取得
        Dim sokoNum As Integer = ds.Tables(inName).Rows.Count
        If sokoNum > 0 Then
            For i As Integer = 0 To sokoNum - 1
                'IN情報格納用データセット
                Dim inDs As DataSet = New LMN080DS()
                'IN情報を設定
                Dim inDr As DataRow = inDs.Tables(inName).NewRow()
                inDr("SCM_CUST_CD") = ds.Tables("LMN080IN").Rows(0).Item("SCM_CUST_CD").ToString()
                inDr("SOKO_CD") = ds.Tables(inName).Rows(i).Item("SOKO_CD").ToString()
                inDs.Tables(inName).Rows.Add(inDr)
                '出荷予定品目数取得
                inDs = MyBase.CallDAC(Me._Dac, "GetPLAN_HINMOKU_NB", inDs)
                ds.Tables(inName).Rows(i).Item("PLAN_HINMOKU_NB") = MyBase.GetResultCount().ToString()
            Next
        End If

        Return ds

    End Function

    ''' <summary>
    '''  明細データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function GetLMN080OUT_M(ByVal ds As DataSet) As DataSet

        '明細データ取得
        ds = MyBase.CallDAC(Me._Dac, "GetLMN080OUT_M", ds)

        Return ds

    End Function

    ''' <summary>
    '''  欠品危惧チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckPreKeppin(ByVal ds As DataSet) As DataSet

        '欠品危惧チェック
        ds = MyBase.CallDAC(Me._Dac, "CheckPreKeppin", ds)

        Return ds

    End Function

#End Region

#End Region

End Class
