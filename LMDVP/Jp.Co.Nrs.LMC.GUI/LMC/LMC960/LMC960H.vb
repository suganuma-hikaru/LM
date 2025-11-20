' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷管理
'  プログラムID     :  LMC960  : トールCSV出力
'  作  成  者       :  []
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
''' LMC960ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC960H
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
        Me.UpdateToll(Me._PrmDs)

    End Sub

#End Region '初期処理

#Region "Method"

    ''' <summary>
    ''' CSV作成
    ''' </summary>
    ''' <param name="prmDs">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsv(ByVal prmDs As DataSet)

        Dim max As Integer = prmDs.Tables("LMC960OUT").Rows.Count - 1
        Dim setData As StringBuilder = New StringBuilder()

        For i As Integer = 0 To max
            With prmDs.Tables("LMC960OUT").Rows(i)
                setData.Append(NullStringToSpace(.Item("RENBAN").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("OUT_YMD").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("USER_ID").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI1_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI1_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI1_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SYUKKA_DATE").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DELIV_PREF_DATE").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DELIV_PREF_TIME").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SYUKKA_NO").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("JURYO").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("TANI").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("KOSU").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("NIFUDA_CNT").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HOKEN_GAKU").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DAIKIN").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI2_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI2_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI2_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_NM_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_NM_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_NM_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_NM_KANA").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_TEL").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_ZIP").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_AD_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("DEST_AD_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_3_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_3_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_CD").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_NM_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_NM_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_NM_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_TEL").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_ZIP").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_AD_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("SENDER_AD_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_4_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_4_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_4_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("TOKUSEI_NM").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_5").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_1").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_2").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_3").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_4").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_4").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_5").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_5").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_6").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_6").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_7").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_7").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("YOBI_6_8").ToString()))
                setData.Append(",")
                setData.Append(NullStringToSpace(.Item("HIN_NM_8").ToString()))
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC960OUT").Rows(0).Item("FILEPATH").ToString,
                                              prmDs.Tables("LMC960OUT").Rows(0).Item("FILENAME").ToString,
                                               "_",
                                              prmDs.Tables("LMC960OUT").Rows(0).Item("SYS_DATE").ToString,
                                               "_",
                                              prmDs.Tables("LMC960OUT").Rows(0).Item("USER_ID").ToString,
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC960OUT").Rows(0).Item("FILEPATH").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

    End Sub

    ''' <summary>
    ''' 空文字からスペースへの変換
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NullStringToSpace(ByVal val As String) As String

        If val.Length = 0 Then
            Return " "
        Else
            Return val
        End If

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function UpdateToll(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC960BLF", "UpdateToll", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class