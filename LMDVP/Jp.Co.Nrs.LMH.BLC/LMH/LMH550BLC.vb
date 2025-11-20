' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH550    : EDI出荷伝票(浮間合成用)
'  作  成  者       :  大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH550BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH550BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH550DAC = New LMH550DAC()

#End Region 'Field

#Region "Const"

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMH550IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMH550OUT"

    ''' <summary>
    ''' 帳票パターンアクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_MPRT As String = "SelectMPrintPattern"

    ''' <summary>
    ''' 印刷データ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_PRT_DATA As String = "SelectPrintData"

    ''' <summary>
    ''' RPTテーブルのカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD As String = "NRS_BR_CD"
    Private Const PTN_ID As String = "PTN_ID"
    Private Const PTN_CD As String = "PTN_CD"
    Private Const RPT_ID As String = "RPT_ID"

    ''' <summary>
    ''' 印刷種別：一括変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ALL_PRINT As String = "99"

#End Region 'Const

#Region "Method"

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = LMH550BLC.TABLE_NM_IN
        Dim tableNmOut As String = LMH550BLC.TABLE_NM_OUT
        Dim tableNmRpt As String = LMH550BLC.TABLE_NM_M_RPT

        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

        '【Notes】№1007/1008対応：メッセージクリア
        MyBase.SetMessage(Nothing)

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            'MyBase.SetMessage("G039")                                                  '【Notes】№1007/1008対応：G039→G052に変更
            MyBase.SetMessage("G052", New String() {"入出荷伝票"})                      '【Notes】№1007/1008対応：G039→G052に変更
            MyBase.SetMessageStore(String.Empty, "G052", New String() {"入出荷伝票"})   '【Notes】№1007/1008対応：G039→G052に変更
            Return ds
        End If

        '要望番号1007 2012.05.08 追加START
        ds.Tables("H_EDI_PRINT").Clear()
        '要望番号1007 2012.05.08 追加END

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            'setDs = Me.SelectMPrt(setDs)       '【Notes】№1007/1008対応
            setDs = Me.SelectMPrt(setDs, inTbl) '【Notes】№1007/1008対応

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            'OUT
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOut.ImportRow(setDtOut.Rows(j))
                '要望番号1007 2012.05.08 追加START
                ds = Me.SetHEdiPrint(dt.Rows(i), setDtOut.Rows(j), ds)
                '要望番号1007 2012.05.08 追加END
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, LMH550BLC.NRS_BR_CD, LMH550BLC.PTN_ID, LMH550BLC.PTN_CD, LMH550BLC.RPT_ID)

        'ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtRpt.Clone

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        'キーブレイク用(NRS_BR_CD,PTN_ID,RPT_ID)
        Dim keyBrCd As String = String.Empty
        Dim keyPtnId As String = String.Empty
        Dim keyRptId As String = String.Empty

        '重複分を除外したワーク用RPTテーブルを帳票出力に使用するDSにセットする)
        For l As Integer = 0 To sortDtRpt.Rows.Count - 1
            '営業所コード、パターンID、レポートIDが一致するレコードは除外する
            If keyBrCd <> sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString() OrElse _
               keyPtnId <> sortDtRpt.Rows(l).Item("PTN_ID").ToString() OrElse _
               keyRptId <> sortDtRpt.Rows(l).Item("RPT_ID").ToString() Then

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                'キー更新
                keyBrCd = sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(l).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(l).Item("RPT_ID").ToString()

            End If

        Next


        'レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables(tableNmRpt).Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMH550BLC.RPT_ID).ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMH550BLC.RPT_ID).ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item(LMH550BLC.NRS_BR_CD).ToString(), _
                                dr.Item(LMH550BLC.PTN_ID).ToString(), _
                                dr.Item(LMH550BLC.PTN_CD).ToString(), _
                                dr.Item(LMH550BLC.RPT_ID).ToString(), _
                                prtDs.Tables(tableNmOut), _
                                ds.Tables(LMConst.RD))
        Next


        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        Dim outDt As DataTable = ds.Tables("LMH550OUT") 'データ取得
        Dim max As Integer = outDt.Rows.Count - 1       '抽出データ数取得
        Dim count As Integer = 0                        '項番(初期値0)

        '編集処理
        For i As Integer = 0 To max

            '[1]郵便番号
            'outDt.Rows(i).Item("YUBIN_NO_B") = KANJI_HENKAN(outDt.Rows(i).Item("YUBIN_NO_B").ToString())

            '[2]電話番号
            outDt.Rows(i).Item("TEL_NO_B") = KANJI_HENKAN(Replace(Replace(outDt.Rows(i).Item("TEL_NO_B").ToString(), "(", ""), ")", ""))

            '[3]品名
            outDt.Rows(i).Item("HINMEI_B") = KANJI_HENKAN(outDt.Rows(i).Item("HINMEI_B").ToString())

            '[4]ロット№
            outDt.Rows(i).Item("LOT_NO_B") = KANJI_HENKAN(outDt.Rows(i).Item("LOT_NO_B").ToString())

            '[5]イエローカード
            If Trim(outDt.Rows(i).Item("YELLOW_CARD_NO_B").ToString()) <> "" Then
                outDt.Rows(i).Item("YELLOW_CARD_NO_B") = KANJI_HENKAN(outDt.Rows(i).Item("YELLOW_CARD_NO_B").ToString())
            End If

            '[6]指針番号
            Select Case Trim(outDt.Rows(i).Item("SHISHIN_NO_B").ToString())
                Case "0"
                Case "1"
                    outDt.Rows(i).Item("SHISHIN_NO_B") = "ナシ"
                Case Else
                    outDt.Rows(i).Item("SHISHIN_NO_B") = KANJI_HENKAN(String.Format(outDt.Rows(i).Item("SHISHIN_NO_B").ToString, "@@@@"))
            End Select

            '[7]国連番号
            Select Case Trim(outDt.Rows(i).Item("UN_NO_B").ToString())
                Case "0"
                Case "1"
                    outDt.Rows(i).Item("UN_NO_B") = "ナシ"
                Case Else
                    outDt.Rows(i).Item("UN_NO_B") = KANJI_HENKAN(String.Format(outDt.Rows(i).Item("UN_NO_B").ToString, "@@@@"))
            End Select

            '[8]発行№
            If Trim(outDt.Rows(i).Item("HAKKO_NO_B").ToString()) <> "" Then
                outDt.Rows(i).Item("HAKKO_NO_B") = KANJI_HENKAN(outDt.Rows(i).Item("HAKKO_NO_B").ToString())
            End If

            '[9]数量
            outDt.Rows(i).Item("SURYO_B") = CDbl(outDt.Rows(i).Item("YOURYO").ToString) * CDbl(outDt.Rows(i).Item("KOSU").ToString)
            outDt.Rows(i).Item("SURYO_B") = KANJI_HENKAN(String.Format(outDt.Rows(i).Item("SURYO_B").ToString, "###,###,###,##0.000"))


            '[10]出荷日(YYMMDD)
            Dim OUTKA_BI_B As String = Left(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 2) & "-"

            '→ 月に前ZEROがあれば、SPACEに変換する
            If Mid(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 3, 1) = "0" Then
                OUTKA_BI_B = OUTKA_BI_B & " "
            Else
                OUTKA_BI_B = OUTKA_BI_B & Mid(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 3, 1)
            End If
            OUTKA_BI_B = OUTKA_BI_B & Mid(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 4, 1) & "-"

            '→ 日に前ZEROがあれば、SPACEに変換する
            If Mid(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 5, 1) = "0" Then
                OUTKA_BI_B = OUTKA_BI_B & " "
            Else
                OUTKA_BI_B = OUTKA_BI_B & Mid(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 5, 1)
            End If
            OUTKA_BI_B = OUTKA_BI_B & Mid(outDt.Rows(i).Item("OUTKA_BI_B").ToString(), 6, 1)

            outDt.Rows(i).Item("OUTKA_BI_B") = KANJI_HENKAN(OUTKA_BI_B)

            '[11]バーコード用項目(左側)
            outDt.Rows(i).Item("BARCODE_LEFT") = String.Format(outDt.Rows(i).Item("KOSU").ToString, "###0")


            '[12]バーコード用項目(右側)
            Dim wkStr As String
            wkStr = "000" & String.Format(outDt.Rows(i).Item("YOURYO").ToString, "##########0.000")
            outDt.Rows(i).Item("BARCODE_RIGHT") = outDt.Rows(i).Item("SAKUIN_CD").ToString & _
                                                  Mid(outDt.Rows(i).Item("LOT_NO").ToString, 2, 2) & _
                                                  Mid(outDt.Rows(i).Item("LOT_NO").ToString, 5, 3) & _
                                                  Format(CSng(Mid(wkStr, InStr(wkStr, ".") - 3, 5)) * 10, "0000")

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 漢字変換処理
    ''' </summary>
    ''' <param name="argStr">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function KANJI_HENKAN(ByVal argStr As String) As String

        Dim Code_ASC As Integer
        Dim Code_ASC_OLD As Integer = 0
        Dim Code_UNICODE As Integer
        Dim Str_KANJI As String = ""
        Dim i As Integer

        '戻り値初期化
        KANJI_HENKAN = ""

        '変換処理
        For i = 1 To Len(argStr)

            Code_ASC = Asc(Mid(argStr, i, 1))

            Select Case Code_ASC
                Case 222
                    'カナ濁点
                    Select Case Code_ASC_OLD
                        Case 182 To 196, 202 To 206
                            Code_UNICODE = Code_UNICODE + 1
                            Code_ASC_OLD = 0
                    End Select

                Case 223
                    'カナ半濁点
                    Select Case Code_ASC_OLD
                        Case 202 To 206
                            Code_UNICODE = Code_UNICODE + 2
                            Code_ASC_OLD = 0
                    End Select

                Case Else
                    KANJI_HENKAN = KANJI_HENKAN & Str_KANJI
                    Str_KANJI = ""
                    Code_ASC_OLD = Code_ASC

                    Select Case Code_ASC
                        Case 32
                            'スペース
                            Code_UNICODE = 12288
                        Case 33 To 126
                            '記号・数字・アルファベット
                            Code_UNICODE = CInt(IIf(Code_ASC = 92, -27, Code_ASC - 288))
                        Case 161
                            'カナ記号（。）
                            Code_UNICODE = 12290
                        Case 162, 163
                            'カナ記号（「」）
                            Code_UNICODE = Code_ASC + 12138
                        Case 164
                            'カナ記号（、）
                            Code_UNICODE = 12289
                        Case 165
                            'カナ記号（・）
                            Code_UNICODE = 12539
                        Case 166
                            'カナ（ヲ）
                            Code_UNICODE = 12530
                        Case 167 To 171
                            'カナ（ァ行）
                            Code_UNICODE = Code_ASC * 2 + 12115
                        Case 172 To 174
                            'カナ（ャ行）
                            Code_UNICODE = Code_ASC * 2 + 12171
                        Case 175
                            'カナ（ッ）
                            Code_UNICODE = 12483
                        Case 176
                            'カナ記号（ー）
                            Code_UNICODE = 12540
                        Case 177 To 181
                            'カナ（ア行）
                            Code_UNICODE = Code_ASC * 2 + 12096
                        Case 182 To 196
                            'カナ（カ行～タ行）
                            Code_UNICODE = CInt(IIf(Code_ASC < 194, Code_ASC * 2 + 12095, Code_ASC * 2 + 12096))
                        Case 197 To 201
                            'カナ（ナ行）
                            Code_UNICODE = Code_ASC + 12293
                        Case 202 To 206
                            'カナ（ハ行）
                            Code_UNICODE = Code_ASC * 3 + 11889
                        Case 207 To 211
                            'カナ（マ行）
                            Code_UNICODE = Code_ASC + 12303
                        Case 212 To 214
                            'カナ（ヤ行）
                            Code_UNICODE = Code_ASC * 2 + 12092
                        Case 215 To 219
                            'カナ（ラ行）
                            Code_UNICODE = Code_ASC + 12306
                        Case 220
                            'カナ（ワ）
                            Code_UNICODE = 12527
                        Case 221
                            'カナ（ン）
                            Code_UNICODE = 12531
                    End Select
            End Select

            Str_KANJI = ChrW(Code_UNICODE)

        Next i

        '戻り値設定
        KANJI_HENKAN = KANJI_HENKAN & Str_KANJI

    End Function

    '要望番号1007 2012.05.08 追加START
    ''' <summary>
    '''  ＥＤＩ印刷対象テーブルINデータ設定(LMH550)
    ''' </summary>
    ''' <param name="Indr"></param>
    ''' <param name="Outdr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHEdiPrint(ByVal Indr As DataRow, ByVal Outdr As DataRow, ByVal ds As DataSet) As DataSet

        ' 出荷伝票
        Dim PrmDt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH550DSのH_EDI_PRINTに値を設定
        PrmDr("NRS_BR_CD") = Indr.Item("NRS_BR_CD")
        PrmDr("EDI_CTL_NO") = Outdr.Item("OUTKAEDI_NO_L")
        PrmDr("CUST_CD_L") = Indr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = Indr.Item("CUST_CD_M")
        PrmDr("INOUT_KB") = Indr.Item("INOUT_KB")
        PrmDr("PRINT_TP") = "01"
        PrmDr("DENPYO_NO") = Outdr.Item("DENPYO_NO")    '要望番号1077 2012.05.29 伝票№追加
        PrmDt.Rows.Add(PrmDr)

        Return ds

    End Function
    '要望番号1007 2012.05.08 追加END

    ''' <summary>
    ''' 出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>

    Private Function SelectMPrt(ByVal ds As DataSet, ByVal intbl As DataTable) As DataSet
        'Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        Dim rowNo As String = intbl.Rows(0).Item("ROW_NO").ToString()
        Dim ediCtlNo As String = intbl.Rows(0).Item("EDI_CTL_NO").ToString()
        Dim prtFlg As String = intbl.Rows(0).Item("PRTFLG").ToString()
        Dim outType As String = intbl.Rows(0).Item("OUTPUT_SHUBETU").ToString()

        ds = MyBase.CallDAC(Me._Dac, LMH550BLC.ACTION_ID_SELECT_MPRT, ds)

        'メッセージコードの設定ALL_PRINT
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            '【Notes】№1007/1008対応：G021→G053に変更 --- START ---
            'MyBase.SetMessage("G021")                                                                  
            MyBase.SetMessage("G053", New String() {"　【入出荷伝票】　"})

            If prtFlg.Equals("1") = True Then
                '出力済の場合、エラーメッセージはExcel出力
                MyBase.SetMessageStore("00", "G053", New String() {"　【入出荷伝票】　"}, rowNo, "EDI管理番号", ediCtlNo)
            Else
                If outType.Equals(ALL_PRINT) = True Then
                    '一括印刷時、エラーメッセージはExcel出力
                    MyBase.SetMessageStore("00", "G053", New String() {"　【入出荷伝票】　"}, rowNo, "帳票名", "入出荷伝票")
                End If
            End If
            '【Notes】№1007/1008対応：G021→G053に変更 ---  END ---
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, LMH550BLC.ACTION_ID_SELECT_PRT_DATA, ds)

    End Function

#End Region

#End Region

End Class
