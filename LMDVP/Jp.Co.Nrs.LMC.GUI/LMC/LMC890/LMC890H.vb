' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC890C : ヤマトB2 CSV出力
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
''' LMC890ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC890H
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
        Dim setOutDt As DataTable = setDs.Tables("LMC890OUT")
        setOutDt.Clear()
        '並び替えをする
        'Dim outDr As DataRow() = prmDs.Tables("LMC890OUT").Select(Nothing, "CUST_CD_L,JIS,NIUKENIN_CD,SYUKKABI,HAISOBI,DENPYO_NO")
        Dim outDr As DataRow() = prmDs.Tables("LMC890OUT").Select(Nothing, "COOL_KBN,GOODS_NM1")

        'まとめ処理、編集処理
        Dim kiji7 As String = String.Empty
        Dim kiji8 As String = String.Empty
        Dim kosu As Decimal = 0
        Dim juryo As Decimal = 0
        Dim cutStr() As String
        Dim matomeCnt As Integer = 0
        Dim max As Integer = outDr.Length - 1
        Dim strWk As String = String.Empty

        For i As Integer = 0 To max

            '出荷予定日編集(YYYY/MM/DD)
            If String.IsNullOrEmpty(outDr(i).Item("OUTKA_PLAN_DATE").ToString) = False Then

                outDr(i).Item("OUTKA_PLAN_DATE") = Left(CStr(Convert.ToDateTime((Convert.ToInt32(outDr(i).Item("OUTKA_PLAN_DATE").ToString)).ToString("0000/00/00"))), 10)
            End If

            'お届け予定日編集(YYYY/MM/DD)
            If String.IsNullOrEmpty(outDr(i).Item("ARR_PLAN_DATE").ToString) = False Then

                outDr(i).Item("ARR_PLAN_DATE") = Left(CStr(Convert.ToDateTime((Convert.ToInt32(outDr(i).Item("ARR_PLAN_DATE").ToString)).ToString("0000/00/00"))), 10)
            End If

            'お届け先住所の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("DEST_ADD_1").ToString, 64, 1)
            outDr(i).Item("DEST_ADD_1") = cutStr(0).Trim

            'お届け先会社・部門１の分割処理
            ReDim cutStr(1)
            'UPD 2016/07/14 全角　⇒　半角
            'cutStr = Me.stringCut(outDr(i).Item("DEST_BUMON1").ToString, 50, 2)
            strWk = StrConv(outDr(i).Item("DEST_BUMON1").ToString, VbStrConv.Narrow)    '全角　⇒　半角対応
            cutStr = Me.stringCut(strWk, 50, 2)

            outDr(i).Item("DEST_BUMON1") = cutStr(0).Trim

            strWk = StrConv(outDr(i).Item("DEST_BUMON2").ToString, VbStrConv.Narrow)    '全角　⇒　半角対応
            outDr(i).Item("DEST_BUMON2") = cutStr(1).Trim + strWk

            'お届け先名称
            ReDim cutStr(0)
            strWk = StrConv(outDr(i).Item("DEST_NM").ToString, VbStrConv.Narrow)        '全角　⇒　半角対応
            cutStr = Me.stringCut(strWk, 32, 1)
            outDr(i).Item("DEST_NM") = cutStr(0).Trim

            'ご依頼主住所の分割処理
            ReDim cutStr(0)
            cutStr = Me.stringCut(outDr(i).Item("IRAINUSHI_ADD").ToString, 64, 1)
            outDr(i).Item("IRAINUSHI_ADD") = cutStr(0).Trim

            'ご依頼主名の分割処理
            ReDim cutStr(0)
            'UPD 2016/07/14 全角　⇒　半角
            'cutStr = Me.stringCut(outDr(i).Item("IRAINUSHI_NM").ToString, 32, 1)
            strWk = StrConv(outDr(i).Item("IRAINUSHI_NM").ToString, VbStrConv.Narrow)   '全角　⇒　半角対応
            cutStr = Me.stringCut(strWk, 32, 1)

            outDr(i).Item("IRAINUSHI_NM") = cutStr(0).Trim

            setOutDt.ImportRow(outDr(i))
        Next

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()
        max = setOutDt.Rows.Count - 1
        For i As Integer = 0 To max
            With setOutDt.Rows(i)
                setData.Append(SetDblQuotation(.Item("OKYAKU_KANRI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OKURIJYO_PTN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("COOL_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DENPYO_BANGO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("OUTKA_PLAN_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("ARR_PLAN_DATE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("HAITATSU_TIME").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TEL_EDA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_ADD_1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_TATEMONO_NM").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_BUMON1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_BUMON2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DEST_NM").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("DET_NM_KANA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KEISYO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_TEL_EDA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_ADD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_TATEMONO_NM").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_NM").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("IRAINUSHI_NM_KANA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOODS_CD1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOODS_NM1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOODS_CD2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("GOODS_NM2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIATSUKAI1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("NIATSUKAI2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KIJI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("COLLECT_AMT").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("COLLECT_TAX").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("TOMEOKI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("BR_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("PRINT_CNT").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KOSU_DISP_FLG").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SEIKYUSAKI_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SEIKYUSAKI_BUNRUI_CD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("UNCHIN_KANRI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CARD_HARAI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CARD_KAMEI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CARD_NO1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CARD_NO2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("CARD_NO3").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EMAIL_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EMAIL_ADDRESS").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EMAIL_IN_TYPE").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("EMAIL_MSG").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KANRYO_EMAIL_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KANRYO_EMAIL_ADDRESS").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("KANRYO_EMAIL_MSG").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_KBN").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("YOBI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_AMT").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_TAX").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_ADD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_TATEMONO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_BUMON1").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_BUMON2").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_NM_KNJI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_SKY_NM_KANA").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_TOI_NM_KANJI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_TOI_NM_ZIP").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_TOI_ADD").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_TOI_TATEMONO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_TOI_TEL").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_DAI_KANRI_NO").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_DAI_HINMEI").ToString()))
                setData.Append(",")
                setData.Append(SetDblQuotation(.Item("SYUNOU_DAI_BIKOU").ToString()))

                setData.Append(vbNewLine)
            End With
        Next

        '保存先のCSVファイルのパス
        Dim csvPath As String = String.Concat(prmDs.Tables("LMC890OUT").Rows(0).Item("FILEPATH").ToString, _
                                              prmDs.Tables("LMC890OUT").Rows(0).Item("FILENAME").ToString, _
                                              prmDs.Tables("LMC890OUT").Rows(0).Item("SYS_DATE").ToString, _
                                               "-", _
                                              Left(prmDs.Tables("LMC890OUT").Rows(0).Item("SYS_TIME").ToString, 6), _
                                              ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(prmDs.Tables("LMC890OUT").Rows(0).Item("FILEPATH").ToString)
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
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC890BLF", "UpdateYamatoB2Csv", ds)

        Return rtnDs

    End Function

#End Region 'Method

End Class