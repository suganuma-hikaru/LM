' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC820    : 名鉄CSV作成(大阪)
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC820BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC820BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC820DAC = New LMC820DAC()


#End Region

#Region "Method"

#Region "CSVデータ取得処理"

    ''' <summary>
    ''' CSVデータ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのMeitetuCsvメソッド呼出</remarks>
    Private Function MeitetuCsv(ByVal ds As DataSet) As DataSet

        '印刷対象データの取得
        Dim rtnDs As DataSet = Me.SelectMeitetuCsv(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC820OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectMeitetuCsv(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC820IN")
        Dim outDt As DataTable = ds.Tables("LMC820OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC820IN")
        Dim setDt As DataTable = setDs.Tables("LMC820OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectMeitetuCsv")

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
    ''' 出荷データ（大）更新（名鉄CSV作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuCsv(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateMeitetuCsv", ds)

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
