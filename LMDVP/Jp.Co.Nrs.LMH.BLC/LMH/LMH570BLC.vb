' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH       : EDI
'  プログラムID     :  LMH570    : 出荷EDI受信一覧表(ＤＩＣ用)
'  作  成  者       :  篠原将文
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMH570BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMH570BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMH570DAC = New LMH570DAC()

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
    Private Const TABLE_NM_IN As String = "LMH570IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT As String = "LMH570OUT"

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
        Dim tableNmIn As String = LMH570BLC.TABLE_NM_IN
        Dim tableNmOut As String = LMH570BLC.TABLE_NM_OUT
        Dim tableNmRpt As String = LMH570BLC.TABLE_NM_M_RPT

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
            MyBase.SetMessage("G052", New String() {"受信一覧表"})                      '【Notes】№1007/1008対応：G039→G052に変更
            MyBase.SetMessageStore(String.Empty, "G052", New String() {"受信一覧表"})   '【Notes】№1007/1008対応：G039→G052に変更
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

            '使用帳票ID取得 dr.Item("CHOICE_KB").ToString()
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
        workDtRpt = view.ToTable(True, LMH570BLC.NRS_BR_CD, LMH570BLC.PTN_ID, LMH570BLC.PTN_CD, LMH570BLC.RPT_ID)

        'ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtRpt.Clone

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        '(2012.06.11) 要望番号1102 データ並び換え制御の追加 --- START ---
        'ソート実行
        Dim outDr As DataRow() = dtOut.Select(Nothing, "DENPYO_NO ASC, OUTKAEDI_NO_L ASC, CANCEL_FLG DESC, REC_NO ASC,GYO ASC")

        'ソート実行後データ格納データセット作成
        dtOut = dtOut.Clone

        'ソート済みデータ格納
        For Each rowOut As DataRow In outDr
            dtOut.ImportRow(rowOut)
        Next
        '(2012.06.11) 要望番号1102 データ並び換え制御の追加 ---  END  ---

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
            '(2012.06.11) 要望番号1102 並び換えしたDateTableを設定 --- START ---
            'prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMH570BLC.RPT_ID).ToString())
            prtDs = comPrt.CallDataSet(dtOut, dr.Item(LMH570BLC.RPT_ID).ToString())
            '(2012.06.11) 要望番号1102 並び換えしたDateTableを設定 ---  END  ---

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMH570BLC.RPT_ID).ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item(LMH570BLC.NRS_BR_CD).ToString(), _
                                dr.Item(LMH570BLC.PTN_ID).ToString(), _
                                dr.Item(LMH570BLC.PTN_CD).ToString(), _
                                dr.Item(LMH570BLC.RPT_ID).ToString(), _
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

        '編集データテーブル取得
        Dim outTable As DataTable = ds.Tables("LMH570OUT")

        'OUTデータ数取得
        Dim max As Integer = outTable.Rows.Count - 1

        'DEL_KBとCANCEL_FLG
        Dim msg_flg As String

        'レコードの項目設定用
        Dim countOutkaL As String = String.Empty
        '比較用キー設定用
        Dim keycountOutkaL As String = String.Empty
        'MESSAGE_01保管用
        Dim MESSAGE_01 As String = String.Empty
        'PRTFLG保管用
        Dim prt_flg As String = String.Empty

        '項番(初期値01)
        Dim count As Integer = 0

        Select Case rptId
            Case "LMH570"
                'OUTデータを1行ずつ編集
                For i As Integer = 0 To max

                    'EDI出荷管理番号L
                    countOutkaL = String.Empty
                    countOutkaL = outTable.Rows(i).Item("OUTKAEDI_NO_L").ToString()
                    'キャンセルメッセージ表示フラグ
                    msg_flg = String.Empty
                    msg_flg = outTable.Rows(i).Item("L_DEL_KB").ToString() + outTable.Rows(i).Item("CANCEL_FLG").ToString()
                    '印刷フラグ(HED)
                    prt_flg = String.Empty
                    prt_flg = outTable.Rows(i).Item("PRTFLG").ToString()

                    '比較用キーとレコード項目の一致判定(空白は除く)
                    If keycountOutkaL <> countOutkaL Then
                        '一致しない場合
                        '01にする
                        count = 1
                        '項番設定
                        outTable.Rows(i).Item("GYO") = "1"
                        '比較用キー更新
                        keycountOutkaL = countOutkaL
                    Else

                        '一致する場合
                        '項番をカウントアップ
                        count = count + 1
                        '項番設定
                        outTable.Rows(i).Item("GYO") = count

                    End If

                    Select Case msg_flg
                        Case "00"
                            outTable.Rows(i).Item("MESSAGE_01") = String.Empty
                        Case "01"
                            If outTable.Rows(i).Item("OUTKAEDI_NO_L").ToString() = "" Then
                                outTable.Rows(i).Item("MESSAGE_01") = "＜データ不正でキャンセル対象の登録済み出荷データが特定できません＞"
                            Else
                                outTable.Rows(i).Item("MESSAGE_01") = "＜キャンセル対象の登録済み出荷データを削除してください。管理番号=" _
                                + outTable.Rows(i).Item("OUTKA_NO_L").ToString() _
                                + "-" _
                                + outTable.Rows(i).Item("OUTKA_NO_M").ToString() _
                                + "EDI番号=" _
                                 + outTable.Rows(i).Item("OUTKAEDI_NO_L").ToString() _
                                + "-" _
                                + outTable.Rows(i).Item("OUTKAEDI_NO_M").ToString() _
                                + ")＞"
                            End If
                        Case "10"
                            If outTable.Rows(i).Item("OUTKAEDI_NO_L").ToString() = "" Then
                                outTable.Rows(i).Item("MESSAGE_01") = "＜キー項目が一致しないのでキャンセル対象が特定できません。＞"
                            Else
                                Select prt_flg
                                    Case "0"
                                        outTable.Rows(i).Item("MESSAGE_01") = "＜同一受信済内のキャンセル対象EDIデータを自動削除しました。EDI番号=" _
                                        + outTable.Rows(i).Item("OUTKAEDI_NO_L").ToString() _
                                        + "-" _
                                        + outTable.Rows(i).Item("OUTKAEDI_NO_M").ToString() _
                                        + ")＞"
                                    Case "2"
                                        outTable.Rows(i).Item("MESSAGE_01") = "＜受信済のキャンセル対象EDIデータを自動削除しました。EDI番号=" _
                                        + outTable.Rows(i).Item("OUTKAEDI_NO_L").ToString() _
                                        + "-" _
                                        + outTable.Rows(i).Item("OUTKAEDI_NO_M").ToString() _
                                        + ")＞"
                                End Select
                                    End If
                        Case "11"
                            outTable.Rows(i).Item("MESSAGE_01") = "＜キー項目が一致したので自動キャンセルされました。＞"
                        Case Else
                            outTable.Rows(i).Item("MESSAGE_01") = ""
                    End Select
                Next

        End Select

        Return ds

    End Function

    '要望番号1007 2012.05.08 追加START
    ''' <summary>
    '''  ＥＤＩ印刷対象テーブルINデータ設定(LMH570)
    ''' </summary>
    ''' <param name="Indr"></param>
    ''' <param name="Outdr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetHEdiPrint(ByVal Indr As DataRow, ByVal Outdr As DataRow, ByVal ds As DataSet) As DataSet

        ' 受信一覧表
        Dim PrmDt As DataTable = ds.Tables("H_EDI_PRINT")
        Dim PrmDr As DataRow = PrmDt.NewRow()

        'LMH570DSのH_EDI_PRINTに値を設定
        PrmDr("NRS_BR_CD") = Indr.Item("NRS_BR_CD")
        PrmDr("EDI_CTL_NO") = Outdr.Item("OUTKAEDI_NO_L")
        PrmDr("CUST_CD_L") = Indr.Item("CUST_CD_L")
        PrmDr("CUST_CD_M") = Indr.Item("CUST_CD_M")
        PrmDr("INOUT_KB") = Indr.Item("INOUT_KB")
        PrmDr("PRINT_TP") = "03"
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

        ds = MyBase.CallDAC(Me._Dac, LMH570BLC.ACTION_ID_SELECT_MPRT, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            '【Notes】№1007/1008対応：G021→G053に変更 --- START ---
            'MyBase.SetMessage("G021")                                                                  
            MyBase.SetMessage("G053", New String() {"　【受信一覧表】　"})

            If prtFlg.Equals("1") = True Then
                '出力済の場合、エラーメッセージはExcel出力
                MyBase.SetMessageStore("00", "G053", New String() {"　【受信一覧表】　"}, rowNo, "EDI管理番号", ediCtlNo)
            Else
                If outType.Equals(ALL_PRINT) = True Then
                    '一括印刷時、エラーメッセージはExcel出力
                    MyBase.SetMessageStore("00", "G053", New String() {"　【受信一覧表】　"}, rowNo, "帳票名", "受信一覧表")
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

        Return MyBase.CallDAC(Me._Dac, LMH570BLC.ACTION_ID_SELECT_PRT_DATA, ds)

    End Function

#End Region

#End Region

End Class
