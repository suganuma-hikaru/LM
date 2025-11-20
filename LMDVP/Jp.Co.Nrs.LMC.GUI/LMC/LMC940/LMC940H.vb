' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC940  : カンガルーマジックCSV(大黒)出力
'  作  成  者       :  daikoku
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
''' LMC940ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC940H
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
        Me.UpdateKangarooCsv(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim setDs As DataSet = prmDs.Copy()
        Dim setOutDt As DataTable = setDs.Tables("LMC940OUT")
        setOutDt.Clear()
        '並び替えをする
        Dim outDr As DataRow() = prmDs.Tables("LMC940OUT").Select(Nothing, "OUTKA_DATE")

        Dim max As Integer = outDr.Length - 1
        Dim strWk As String = String.Empty

        For i As Integer = 0 To max
            setOutDt.ImportRow(outDr(i))
        Next

        'CSV出力処理
        Dim sWk(0) As String

        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("NRS_BR_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_NO_L").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KOSU").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("JURYO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL").ToString().Replace("-", "")))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ZIP").ToString().Replace("-", "")))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_AD3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI5").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("COMMENT1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("COMMENT2").ToString()))
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC940OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC940OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC940OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC940OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC940OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub

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
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateKangarooCsv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC940BLF", "UpdateKangarooCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class