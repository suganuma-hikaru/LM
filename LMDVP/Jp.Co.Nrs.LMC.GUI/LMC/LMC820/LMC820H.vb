' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC820C : 名鉄CSV出力(大阪)
'  作  成  者       :  YANAI
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports System.Text
Imports System.IO

''' <summary>
''' LMC820ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC820H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

#End Region 'Field

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'CSV出力処理
        Me.MakeCSV(Me._PrmDs)

        '出荷(大)の更新
        Me.UpdateMeitetuCsv(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMC820OUT").Select(Nothing, "NRS_BR_CD,DEST_CD,CUST_CD_L")
        Dim max As Integer = 0

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = outDr.Length - 1
        For i As Integer = 0 To max
            With outDr(i)
                setData.Append(SetDblQuotation(.Item("DEST_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CUST_CD_L").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("AD_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("AD_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("AD_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CUST_NM_L").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CUST_NM_M").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OKURIJO_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DENPYO_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_PLAN_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("PRT_CNT").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SUM_KOSU").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SUM_WT").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SUM_YOSEKI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ARR_PLAN_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAITATSU_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAITATSU_TIME_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_5").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_6").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI_7").ToString()))
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        'START YANAI 要望番号957
        'Dim csvPath As String = String.Concat(prmDs.Tables("LMC820OUT").Rows(0).Item("FILEPATH").ToString, _
        '                                      prmDs.Tables("LMC820OUT").Rows(0).Item("FILENAME").ToString, _
        '                                      prmDs.Tables("LMC820OUT").Rows(0).Item("SYS_DATE").ToString, _
        '                                       "-", _
        '                                      Left(prmDs.Tables("LMC820OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
        '                                      ".csv")
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC820OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC820OUT").Rows(0).Item("FILENAME").ToString, _
                                              ".csv")
        'END YANAI 要望番号957

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC820OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub

    ''' <summary>
    ''' 文字分割
    ''' </summary>
    ''' <param name="inStr">分割対象文字</param>
    ''' <param name="inByte">分割単位バイト数</param>
    ''' <param name="inCnt">分割する数</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Function stringCut(ByVal inStr As String, ByVal inByte As Integer, ByVal inCnt As Integer) As String()

        Dim newCnt As Integer = inCnt - 1
        Dim newByte As Integer = inByte - 1
        Dim oldStr(newCnt) As String
        Dim newStr(newCnt) As String
        Dim byteCnt As Integer = 1

        For i As Integer = 0 To newCnt
            For j As Integer = 0 To newByte
                oldStr(i) = String.Concat(oldStr(i), Mid(inStr, byteCnt, 1))
                If System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(oldStr(i)) <= newByte + 1 Then
                    newStr(i) = oldStr(i)
                    byteCnt = byteCnt + 1
                Else
                    Exit For
                End If
            Next
        Next

        Return newStr

    End Function

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC820BLF", "UpdateMeitetuCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class