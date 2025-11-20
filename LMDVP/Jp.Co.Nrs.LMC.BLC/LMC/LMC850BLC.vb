' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC850BLC : 名鉄CSV作成(埼玉)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports System.IO

''' <summary>
''' LMC850BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC850BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC850DAC = New LMC850DAC()


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
        If rtnDs.Tables("LMC850OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E430")
            Return ds
        End If

        Dim prtDs As DataSet
        prtDs = New DataSet

        '纏め処理
        prtDs = Me.EditCsvDataSet(rtnDs)

        Return prtDs

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectMeitetuCsv(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim dt As DataTable = ds.Tables("LMC850IN")
        Dim outDt As DataTable = ds.Tables("LMC850OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC850IN")
        Dim setDt As DataTable = setDs.Tables("LMC850OUT")
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
    ''' 出荷データ(大)更新（名鉄CSV埼玉作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuCsv(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateMeitetuCsv", ds)

    End Function


    ''' <summary>
    ''' CSVデータまとめ処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function EditCsvDataSet(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet
        Dim outDt As DataTable = ds.Tables("LMC850OUT")
        Dim max As Integer = outDt.Rows.Count - 1       'DataSet Max行数格納
        Dim RecCnt As Integer = 0
        Dim KOSU As Double = 0
        Dim JYURYO As Double = 0
        Dim matomeCnt As Integer = 0

        'キーブレイク用
        Dim NewKey As String = String.Empty
        Dim OldKey As String = String.Empty

        rtnDs = ds.Copy
        rtnDs.Tables("LMC850OUT").Clear()

        'まとめ処理：以下の4項目で纏め処理を行う。
        '①荷主コード、②届先コード、③出荷日、④納入日

        For i As Integer = 0 To max 'MAXを、纏めるレコード件数分にする。

            'NEWキーに値を設定
            NewKey = outDt.Rows(i).Item("CUST_CD_L").ToString().Trim
            NewKey = NewKey & outDt.Rows(i).Item("DEST_CD").ToString().Trim
            NewKey = NewKey & outDt.Rows(i).Item("OUTKA_PLAN_DATE").ToString().Trim
            NewKey = NewKey & outDt.Rows(i).Item("ARR_PLAN_DATE").ToString().Trim

            If String.IsNullOrEmpty(NewKey) = True OrElse NewKey = OldKey Then

                'NewKey、OldKeyが同じ場合は纏め処理を行う
                matomeCnt = matomeCnt + 1

                'オーダー番号
                If matomeCnt <= 4 Then
                    rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KIJI_7") = String.Concat(rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KIJI_7").ToString, _
                                                                                          ",", outDt.Rows(i).Item("KIJI_7").ToString)
                ElseIf matomeCnt = 5 Then
                    'まとめの5件目の場合
                    rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KIJI_8") = outDt.Rows(i).Item("KIJI_7").ToString
                ElseIf 6 <= matomeCnt AndAlso matomeCnt <= 8 Then
                    'まとめの6～8件目までの場合
                    rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KIJI_8") = String.Concat(rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KIJI_8").ToString, _
                                                                                          ",", outDt.Rows(i).Item("KIJI_7").ToString)
                End If

                '個数・重量                        
                KOSU = KOSU + Convert.ToDouble(outDt.Rows(i).Item("KOSU").ToString())
                JYURYO = JYURYO + Convert.ToDouble(outDt.Rows(i).Item("JYURYO").ToString())

                rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KOSU") = KOSU
                rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("JYURYO") = JYURYO

            Else
                'NewKey、OldKeyが違う場合は、rtnDsへ格納する
                rtnDs.Tables("LMC850OUT").ImportRow(outDt.Rows(i))

                KOSU = 0
                JYURYO = 0
                matomeCnt = 1

                KOSU = KOSU + Convert.ToDouble(outDt.Rows(i).Item("KOSU").ToString())
                JYURYO = JYURYO + Convert.ToDouble(outDt.Rows(i).Item("JYURYO").ToString())

                If i <> 0 Then
                    '初回はカウントUpしない
                    RecCnt = RecCnt + 1
                End If

                rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("KOSU") = KOSU
                rtnDs.Tables("LMC850OUT").Rows(RecCnt).Item("JYURYO") = JYURYO

            End If

                'OldKeyへ入れ換え
                OldKey = NewKey

        Next

        Return rtnDs

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
