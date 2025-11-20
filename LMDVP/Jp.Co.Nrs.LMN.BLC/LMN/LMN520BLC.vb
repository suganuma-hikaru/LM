' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : SCM
'  プログラムID     :  LMN520    : 商品別過去実績推移
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMN520BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN520BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMN520DAC = New LMN520DAC()


#End Region

#Region "Method"

#Region "印刷処理"


    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        Dim tableNmOut As String = "LMN520OUT"

        'IN条件0件チェック
        If ds.Tables("LMN520IN").Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds

        End If

        '使用帳票ID取得(直接指定)
        ds = Me.SelectMPrt(ds)

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        ds = Me.SelectPrintData(ds)

        '在庫数、凡例タイトル設定
        ds = Me.SetZaikoNb(ds)

        '出力開始日付以降のデータ取得
        Dim prtDs As DataSet = New DataSet
        prtDs = Me.SetFromDate(ds)

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
        Dim dr As DataRow = ds.Tables("M_PRT").Rows(0)
        '帳票CSV出力

        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                            dr.Item("PTN_ID").ToString(), _
                            dr.Item("PTN_CD").ToString(), _
                            dr.Item("RPT_ID").ToString(), _
                            prtDs.Tables(tableNmOut), _
                            ds.Tables(LMConst.RD))

        Return ds

    End Function


    ''' <summary>
    ''' 在庫数算出・設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetZaikoNb(ByVal ds As DataSet) As DataSet

        '編集データテーブル名取得
        Dim outTable As DataTable = ds.Tables("LMN520OUT")
        'レコード数取得
        Dim max As Integer = outTable.Rows.Count - 1
        '在庫数
        Dim zaikoNb As Integer = 0
        '入荷数
        Dim inkaNb As Integer = 0
        '出荷数
        Dim outkaNb As Integer = 0

        '各レコードの在庫数算出・設定
        For i As Integer = 0 To max
            '入荷数取得
            inkaNb = Convert.ToInt32(outTable.Rows(i).Item("INKA_NB").ToString())
            '出荷数取得
            outkaNb = Convert.ToInt32(outTable.Rows(i).Item("OUTKA_NB").ToString())
            '在庫数取得(在庫数は累積値)
            zaikoNb = zaikoNb + inkaNb - outkaNb

            '在庫数設定
            outTable.Rows(i).Item("ZAI_NB") = zaikoNb.ToString()
            '凡例タイトル(折れ線グラフ)設定
            outTable.Rows(i).Item("HANREI_TAITLE1") = "出荷個数"
            '凡例タイトル(棒グラフ)設定
            outTable.Rows(i).Item("HANREI_TAITLE2") = "在庫個数"
        Next

        Return ds

    End Function


    ''' <summary>
    ''' 出力開始日付以降のデータ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFromDate(ByVal ds As DataSet) As DataSet

        'リターンデータセット
        Dim prtDs As DataSet = New LMN520DS
        'IN情報より出力開始日付取得
        Dim fromDate As String = ds.Tables("LMN520IN").Rows(0).Item("FROM_DATE").ToString()

        '出力開始日付以降のデータ取得
        Dim selectDr As DataRow() = ds.Tables("LMN520OUT").Select(String.Concat("INOUTKA_DATE >= ", fromDate))

        'データ数取得
        Dim max As Integer = selectDr.Length - 1
        '取得データをリターンデータセットに格納
        For i As Integer = 0 To max
            prtDs.Tables("LMN520OUT").ImportRow(selectDr(i))
        Next

        Return prtDs

    End Function


    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        Select Case rptId
            Case ""

        End Select

        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理(直接指定)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        'IN情報
        Dim inDr As DataRow = ds.Tables("LMN520IN").Rows(0)

        '帳票パターンテーブルのデータ行作成
        Dim mprtDr As DataRow = ds.Tables("M_PRT").NewRow()

        '帳票パターンテーブル設定
        mprtDr.Item("NRS_BR_CD") = inDr.Item("NRS_BR_CD").ToString()
        mprtDr.Item("PTN_ID") = "70"        '商品別過去実績推移の帳票パターンID
        mprtDr.Item("PTN_CD") = "00"        '帳票パターンCD
        mprtDr.Item("RPT_ID") = "LMN520"    '商品別過去実績推移のレポートID

        '作成データを追加
        ds.Tables("M_PRT").Rows.Add(mprtDr)

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        '移行フラグ取得
        Dim ikoFlag As String = ds.Tables("LMN520IN").Rows(0).Item("IKO_FLAG").ToString()

        '移行フラグにより処理を分岐
        If ikoFlag = "00" Then  '移行前
            Return MyBase.CallDAC(Me._Dac, "SelectPrintDataLMSVer1", ds)
        ElseIf ikoFlag = "01" Then  '移行後
            Return MyBase.CallDAC(Me._Dac, "SelectPrintDataLMSVer2", ds)
        Else  'その他、とりあえず移行後の処理
            Return MyBase.CallDAC(Me._Dac, "SelectPrintDataLMSVer2", ds)
        End If

    End Function

#End Region

#End Region

End Class
