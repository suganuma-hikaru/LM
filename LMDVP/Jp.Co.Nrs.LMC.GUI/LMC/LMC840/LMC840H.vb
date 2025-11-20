' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC840C : シグマ出荷CSV出力
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
''' LMC840ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC840H
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
        Me.UpdateSigmaCsv(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim strData As String = String.Empty
        Dim strDataSpace As String = Space(31)

        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMC840OUT").Select(Nothing, "OUTKA_NO_L")
        Dim max As Integer = 0

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = outDr.Length - 1
        For i As Integer = 0 To max
            With outDr(i)
                '出荷管理番号（後ろ7文字）
                setData.Append(Right(.Item("OUTKA_NO_L").ToString(), 7))
                '出荷日
                setData.Append(.Item("OUTKA_PLAN_DATE").ToString())
                '納入日
                setData.Append(.Item("ARR_PLAN_DATE").ToString())
                '梱包個数
                setData.Append(.Item("OUTKA_PKG_NB").ToString().PadRight(7, CChar(Space(1))))
                'EDI届け先コード
                setData.Append(Left(.Item("EDI_CD").ToString(), 12).PadRight(12, CChar(Space(1))))
                '32桁分半角スペース詰め
                setData.Append(strDataSpace)
                '
                setData.Append(.Item("CUST_CD_L").ToString())
                '15桁に足りない分、半角スペースを後詰する
                setData.Append(.Item("DEST_CD").ToString().PadRight(15, CChar(Space(1))))
                '
                '30桁に足りない分、空白を後詰する
                setData.Append(.Item("CUST_ORD_NO").ToString().PadRight(30, CChar(Space(1))))
                '
                'システム時間
                strData = String.Concat(Mid(.Item("SYS_UPD_TIME").ToString(), 1, 2), ":", _
                                        Mid(.Item("SYS_UPD_TIME").ToString(), 3, 2), ":", _
                                        Mid(.Item("SYS_UPD_TIME").ToString(), 5, 2))
                setData.Append(strData)
                setData.Append(vbNewLine)

            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC840OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC840OUT").Rows(0).Item("FILENAME").ToString, _
                                              ".txt")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC840OUT").Rows(0).Item("FILEPATH").ToString)
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
    Private Function UpdateSigmaCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC840BLF", "UpdateSigmaCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class