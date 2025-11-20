' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC860BLC : BYK出荷報告作成
'  作  成  者       :  umano
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC860BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC860BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC860DAC = New LMC860DAC()


#End Region

#Region "Method"

#Region "報告対象データ取得処理"

    ''' <summary>
    ''' 報告対象データ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectOutkaHokokuメソッド呼出</remarks>
    Private Function OutkaHokoku(ByVal ds As DataSet) As DataSet

        Dim outkaNoL As String = String.Empty
        Dim preOutkaNoL As String = String.Empty

        '出荷報告対象データの取得
        Dim rtnDs As DataSet = Me.SelectOutkaHokoku(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC860OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E024")
            Return ds

        Else
            Dim rtnDt As DataTable = rtnDs.Tables("LMC860OUT")
            For i As Integer = 0 To rtnDt.Rows.Count - 1

                If (rtnDt.Rows(i).Item("CUST_CD_M").ToString().Equals("02") OrElse _
                    rtnDt.Rows(i).Item("CUST_CD_M").ToString().Equals("03")) AndAlso _
                    String.IsNullOrEmpty(rtnDt.Rows(i).Item("E1EDKA1_PARVW_LIFNR_WE").ToString()) = True AndAlso _
                    rtnDt.Rows(i).Item("SAMPLE_HOUKOKU_FLG").ToString().Equals("0") = True Then
                    outkaNoL = rtnDt.Rows(i).Item("OUTKA_CTL_NO").ToString()

                    If String.IsNullOrEmpty(preOutkaNoL) = False AndAlso preOutkaNoL.Equals(outkaNoL) = False Then
                        MyBase.SetMessageStore("00", "E454", New String() {"送状番号未入力", "実績作成", String.Concat("出荷管理番号: ", outkaNoL)})
                    ElseIf String.IsNullOrEmpty(preOutkaNoL) = True AndAlso String.IsNullOrEmpty(outkaNoL) = False Then
                        MyBase.SetMessageStore("00", "E454", New String() {"送状番号未入力", "実績作成", String.Concat("出荷管理番号: ", outkaNoL)})
                    End If
                    preOutkaNoL = outkaNoL
                    outkaNoL = String.Empty
                End If

            Next

        End If

        Return ds

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectOutkaHokoku(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC860IN")
        Dim outDt As DataTable = ds.Tables("LMC860OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC860IN")
        Dim setDt As DataTable = setDs.Tables("LMC860OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectOutkaHokoku")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' BYK出荷実績TBL(代理店用)追加処理（BYK出荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function InsertSendOutBykAgtData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "InsertSendOutBykAgtData", ds)

    End Function

    ''' <summary>
    ''' 出荷データ(大)更新（BYK出荷報告作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateOutkaData(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateOutkaData", ds)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

#End Region

#End Region

End Class
