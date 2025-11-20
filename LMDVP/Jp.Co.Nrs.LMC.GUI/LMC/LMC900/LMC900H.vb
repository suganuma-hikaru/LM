' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC900  : 佐川e飛伝 CSV出力
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
''' LMC900ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC900H
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
        Me.UpdateYamatoB2Csv(Me._PrmDs)

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
        Dim setOutDt As DataTable = setDs.Tables("LMC900OUT")
        setOutDt.Clear()

        '並び替えをする
        'Dim outDr As DataRow() = prmDs.Tables("LMC900OUT").Select(Nothing, "CUST_CD_L,JIS,NIUKENIN_CD,SYUKKABI,HAISOBI,DENPYO_NO")
        Dim outDr As DataRow() = prmDs.Tables("LMC900OUT").Select(Nothing, "BINSYU_HINMEI,HINMEI1")

        'まとめ処理、編集処理
        Dim kiji7 As String = String.Empty
        Dim kiji8 As String = String.Empty
        Dim kosu As Decimal = 0
        Dim juryo As Decimal = 0
        'Dim cutStr() As String
        Dim matomeCnt As Integer = 0
        Dim max As Integer = outDr.Length - 1
        Dim STRwK As String = String.Empty
        'DEL 2016/07/15 e飛伝側で分割処理しているのでやめる
        For i As Integer = 0 To max
            '    'お客様住所１にはDEST_AD_1 + DEST_AD_2 を設定、e飛伝があふれた分は自動でADD1/ADD2/ADD3に設定する
            '    ''お客様住所１の分割処理
            '    'ReDim cutStr(1)
            '    'cutStr = Me.stringCut(outDr(i).Item("DEST_AD_1").ToString, 32, 2)
            '    'outDr(i).Item("DEST_AD_1") = cutStr(0).Trim
            '    'outDr(i).Item("DEST_AD_2") = cutStr(1).Trim

            '    'お届け先名称の分割処理
            '    ReDim cutStr(1)
            '    cutStr = Me.stringCut(outDr(i).Item("DEST_NM_1").ToString, 32, 2)
            '    outDr(i).Item("DEST_NM_1") = cutStr(0).Trim
            '    outDr(i).Item("DEST_NM_2") = cutStr(1).Trim

            '    '部署・担当者の分割処理
            '    ReDim cutStr(0)
            '    cutStr = Me.stringCut(outDr(i).Item("BUSYO_TANTOUSYA").ToString, 32, 1)
            '    outDr(i).Item("BUSYO_TANTOUSYA") = cutStr(0).Trim

            '    'ご依頼人住所１の分割処理
            '    ReDim cutStr(0)
            '    cutStr = Me.stringCut(outDr(i).Item("GOIRAINUSHI_AD1").ToString, 32, 1)
            '    outDr(i).Item("GOIRAINUSHI_AD1") = cutStr(0).Trim

            '    'ご依頼人住所２の分割処理
            '    ReDim cutStr(0)
            '    cutStr = Me.stringCut(outDr(i).Item("GOIRAINUSHI_AD2").ToString, 32, 1)
            '    outDr(i).Item("GOIRAINUSHI_AD2") = cutStr(0).Trim

            '    'ご依頼人名称１の分割処理
            '    ReDim cutStr(0)
            '    cutStr = Me.stringCut(outDr(i).Item("GOIRAINUSHI_AD2").ToString, 32, 1)
            '    outDr(i).Item("GOIRAINUSHI_AD2") = cutStr(0).Trim

            '    '荷受人名の分割処理
            '    ReDim cutStr(0)
            '    'cutStr = Me.stringCut(outDr(i).Item("GOIRAINUSH_NM1").ToString, 32, 1)
            '    STRwK = StrConv(outDr(i).Item("GOIRAINUSH_NM1").ToString, VbStrConv.Narrow)   '全角　⇒　半角対応
            '    cutStr = Me.stringCut(STRwK, 32, 1)
            '    outDr(i).Item("GOIRAINUSH_NM1") = cutStr(0).Trim

            setOutDt.ImportRow(outDr(i))
        Next

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("JYUSYOROKU_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ZIP").ToString()))
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
                setData.Append(SetDblQuotation(.Item("OKYAKU_KANRI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OKYAKU_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BUSYO_TANTOUSYA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIOKURININ_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_AD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSHI_AD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSH_NM1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOIRAINUSH_NM2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NISUGATA_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI4").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HINMEI5").ToString()))
                setData.Append(",")
                setData.Append(.Item("SYUKKA_KOSU").ToString())
                setData.Append(",")
                setData.Append(.Item("BINSYU_SPEED").ToString())
                setData.Append(",")
                setData.Append(.Item("BINSYU_HINMEI").ToString())
                setData.Append(",")
                setData.Append(.Item("HAITATSU_BI").ToString())
                setData.Append(",")
                setData.Append(.Item("HAITATSU_JIKANTAI").ToString())
                setData.Append(",")
                setData.Append(.Item("HAITATSU_JIKAN").ToString())
                setData.Append(",")
                setData.Append(.Item("DAIBIKI_KINGAKU").ToString())
                setData.Append(",")
                setData.Append(.Item("TAX").ToString())
                setData.Append(",")
                setData.Append(.Item("KESSAI_SYUBETSU").ToString())
                setData.Append(",")
                setData.Append(.Item("HOKEN_KINGAKU").ToString())
                setData.Append(",")
                setData.Append(.Item("HOKEN_KINGAKU_INJI").ToString())
                setData.Append(",")
                setData.Append(.Item("SEAL1").ToString())
                setData.Append(",")
                setData.Append(.Item("SEAL2").ToString())
                setData.Append(",")
                setData.Append(.Item("SEAL3").ToString())
                setData.Append(",")
                setData.Append(.Item("EIGYOTENDOME").ToString())
                setData.Append(",")
                setData.Append(.Item("SRC_KBN").ToString())
                setData.Append(",")
                setData.Append(.Item("EIGYOTEN_CD").ToString())
                setData.Append(",")
                setData.Append(.Item("MOTOCHAKU_KBN").ToString())
                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC900OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC900OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC900OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC900OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC900OUT").Rows(0).Item("FILEPATH").ToString)
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
    Private Function UpdateYamatoB2Csv(ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC900BLF", "UpdateSagawaEHidenCsv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class