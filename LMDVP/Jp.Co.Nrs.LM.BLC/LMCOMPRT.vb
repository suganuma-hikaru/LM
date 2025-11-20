' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMCOM       : 印刷共通処理（仮）
'  プログラムID     :  LMCOMPRT    : 印刷共通処理（仮）最終的には部品
'  作  成  者       :  [kobayashi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports RDEPS_JobManagerAPI

''' <summary>
''' LMBPRTBLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMCOMPRT
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Const"

    Private Const RDSVIP As String = "10.1.1.38"
    Private Const RDSVPORT As Integer = 7080
    Private Const ENC_SJIS As String = "SHIFT-JIS"
#End Region

#Region "va"

    Private rdJobId As String = "TESTPRINT"

#End Region

#Region "Method"

#Region "該当レイアウトの帳票を抽出"


    ''' <summary>
    ''' 該当レイアウトの帳票を抽出
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="PrtId">PrtId</param>
    ''' <param name="outPutTblName">outPutTblName</param>
    ''' <returns>DataSet</returns>
    ''' <remarks>同一レイアウトの新しいデータセットを返します</remarks>
    Friend Function CallDataSet(ByVal ds As DataSet _
                                , ByVal prtId As String _
                                , ByVal outPutTblName As String, Optional ByVal sort As String = "") As DataSet

        Dim outDs As DataSet = New DataSet
        Dim outDt As DataTable = ds.Tables(outPutTblName).Clone
        outDs.Tables.Add(outDt)

        Dim drs As DataRow() = ds.Tables(outPutTblName).Select("RPT_ID = '" & prtId & "'", sort)

        For Each dr As DataRow In drs

            outDt.ImportRow(dr)

        Next

        Return outDs

    End Function


#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="brCd">brCd</param>
    ''' <param name="ptnId">ptnId</param>
    ''' <param name="ptnCd">ptnCd</param>
    ''' <param name="rptId">rptId</param>
    ''' <param name="printTblName">printTblName</param>
    ''' <param name="ds">ds</param>
    ''' <remarks>同一レイアウトの新しいデータセットを返します</remarks>
    Friend Function DoPrint(ByVal brCd As String _
                    , ByVal ptnId As String _
                    , ByVal ptnCd As String _
                    , ByVal rptId As String _
                    , ByVal printTblName As String _
                    , ByVal ds As DataSet _
                    , Optional ByRef prevFileNm As String = "") As Boolean

        Dim rtnResult As Boolean = False

        '出力文字列
        Dim printString As String = String.Empty
        For i As Integer = 0 To ds.Tables(printTblName).Columns.Count - 1
            printString = String.Concat(printString, Me.SetQuot(ds.Tables(printTblName).Columns(i).ColumnName.ToString()), ",")
        Next
        printString = String.Concat(printString, vbCrLf)

        'データ部出力
        For Each row As DataRow In ds.Tables(printTblName).Rows
            For i As Integer = 0 To ds.Tables(printTblName).Columns.Count - 1
                printString = String.Concat(printString, Me.SetQuot(row(i).ToString))
                If i <> ds.Tables(printTblName).Columns.Count - 1 Then
                    printString = String.Concat(printString, ",")
                Else
                    printString = String.Concat(printString, ",", vbCrLf)
                End If
            Next
        Next

        'ヘッダメタ部出力
        Dim headCSV As String = Me.SetHeaderTab(brCd, ptnId, ptnCd, rptId)
        printString = String.Concat(headCSV, printString)

        '出力
        Dim fileNm As String = Me.CreateCSV(brCd, ptnId, ptnCd, rptId, printString)

        'API呼出
        '出力区分が直接印刷・スプールの場合、API呼出
        Dim OUTPUT_KBN As String = "02"
        If OUTPUT_KBN.Equals("02") = True Then
            'テストはすべてスプール
            rtnResult = Me.CallRdAPI(fileNm)
        Else
            'プレビューの場合、ファイル名を返す
            prevFileNm = fileNm
            rtnResult = True
        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' ダブルクォーテーションで囲み返却
    ''' </summary>
    ''' <param name="para"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetQuot(ByVal para As String) As String

        '文字列ダブルクォーテーションは""に変換する。ここだけ見るともはやカオス。
        Return String.Concat("""", para.Replace("""", """"""), """")

    End Function


    ''' <summary>
    ''' ヘッダメタデータ作成
    ''' </summary>
    ''' <param name="brCd"></param>
    ''' <param name="ptnId"></param>
    ''' <param name="ptnCd"></param>
    ''' <param name="rptId"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetHeaderTab(ByVal brCd As String _
                    , ByVal ptnId As String _
                    , ByVal ptnCd As String _
                    , ByVal rptId As String) As String
        Dim headCSV As String = String.Empty
        '帳票パターンマスタ情報を取得

        '帳票パターンマスタから設定。なんだけどテストなのでとりあえず固定
        headCSV &= "<rdstart>" & vbCrLf
        headCSV &= "RDSetForm=/LM/" & rptId & ".mrd" & vbCrLf
        headCSV &= "RDSetOutputMode=SPOOL" & vbCrLf
        '実際はMybase.UserId 落ちた。
        headCSV &= "RDSetUserName=sbs" & vbCrLf
        headCSV &= "RDSetComputer=sbsPC" & vbCrLf
        headCSV &= "RDSetDocName=" & rptId & vbCrLf
        '実際は出力形式区分より設定。直接印刷⇒1 スプール⇒0
        headCSV &= "RDSetAutoPrintMode=0" & vbCrLf
        headCSV &= "<rdend>" & vbCrLf

        Return headCSV

    End Function

#End Region

#Region "テスト（DS出力）"

    ''' <summary>
    ''' テストDS出力
    ''' </summary>
    ''' <param name="brCd">brCd</param>
    ''' <param name="ptnId">ptnId</param>
    ''' <param name="ptnCd">ptnCd</param>
    ''' <param name="rptId">rptId</param>
    ''' <param name="prtString"></param>
    ''' <returns>出力ファイル名</returns>
    ''' <remarks></remarks>
    Friend Function CreateCSV(ByVal brCd As String _
                    , ByVal ptnId As String _
                    , ByVal ptnCd As String _
                    , ByVal rptId As String _
                    , ByVal prtString As String) As String

        'テストのため、出力先は固定
        Dim aDate As Date = Now
        'サーバに変更
        'Dim outputFileNm As String = String.Concat("C:\LMUSER\REPORT\", rptId, Format(aDate, "yyyyMMdd_HHmmss"), aDate.Millisecond.ToString.PadLeft(3, CChar("0")), ".csv")
        Dim outputFileNm As String = String.Concat("\\SVTYO106s\project\LMS_NET\rpt\", rptId, "_", Format(aDate, "yyyyMMddHHmmss"), aDate.Millisecond.ToString.PadLeft(3, CChar("0")), ".csv")

        'Shift JISで書き込む
        Dim sw As New System.IO.StreamWriter(outputFileNm, _
                        False, _
                        System.Text.Encoding.GetEncoding("shift_jis"))

        'TextBox1.Textの内容を書き込む
        sw.Write(prtString)
        '閉じる
        sw.Close()

        Return outputFileNm

    End Function

#End Region

#Region "RDAPI"

    ''' <summary>
    ''' ReportDesignerAPI実行
    ''' </summary>
    ''' <param name="fileNm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CallRdAPI(ByVal fileNm As String) As Boolean

        Dim rtnResult As Boolean = False

        Dim jmAPI As JobManagerAPIClient = New JobManagerAPIClient
        jmAPI.SetJobManager(RDSVIP, RDSVPORT)
        jmAPI.SetEncoding(ENC_SJIS)
        If jmAPI.RequestReportByFile(rdJobId, fileNm) = True Then
            rtnResult = True
        End If

        Return rtnResult

    End Function
#End Region

#End Region

End Class
