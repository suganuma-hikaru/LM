' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC560    : 送り状
'  作  成  者       :  [shinohara]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC560BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC560BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC560DAC = New LMC560DAC()





#End Region

#Region "Const"

    ''' <summary>
    ''' RPTテーブルのカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const RPT_ID As String = "RPT_ID"
#End Region

#Region "Method"

#Region "印刷処理"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号1478 一括印刷が遅い
        Dim beforeSecond As String = String.Empty
        Dim nowSecond As String = String.Empty
        'END YANAI 要望番号1478 一括印刷が遅い

        '元のデータ
        Dim tableNmIn As String = "LMC560IN"
        Dim tableNmOut As String = "LMC560OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        'ADD 2018/07/31 依頼番号 1572
        Dim dtSelectAll As DataTable = ds.Tables(tableNmIn)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone

        'ADD Start 2022/12/28 アフトン別名出荷対応
        Dim setDsEdiGn As DataSet = ds.Copy()
        Dim dtEdiGn As DataTable
        'ADD End   2022/12/28 アフトン別名出荷対応

        ''DataSetのM_RPT情報を取得(岩槻浮間対応)
        'Dim mrptTbl As DataTable = ds.Tables("M_RPT")
        ''帳票ID用(岩槻浮間対応)
        'Dim mrptRow As Data.DataRow
        ''M_RPTTableの条件rowの格納(岩槻浮間対応)
        'mrptRow = mrptTbl.Rows(0)

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        'ADD Start 2022/12/28 アフトン別名出荷対応
        'セミEDI出荷指示取込時の商品名取得処理
        setDsEdiGn = Me.SelectOutkaediGoodsnm(setDsEdiGn)
        '取得結果を詰め替え
        dtEdiGn = setDsEdiGn.Tables("LMC560OUT_OUTKAEDI_GOODSNM")
        'ADD End   2022/12/28 アフトン別名出荷対応

        'Dim roopCnt As Integer = 0
        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'ADD 2018/07/31 依頼番号 1572
            setDs.Tables("LMC560IN_SELECT_ALL").Merge(dtSelectAll)


            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If
            ''20121214 BPCの場合

            'If setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC701" Or _
            '   setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC702" Or _
            '   setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC703" Or _
            '   setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC704" Or _
            '   setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC705" Or _
            '   setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC706" Then
            'Else
            '上記帳票はDACのSQLで処理するため、ここでは何もしない。(MATOME_FLGが1でも何も処理しない)
            '@(2012.06.05)纏め荷札対応 --- START ---
            ''(埼玉BPは処理しない：名鉄、オカケン、第一貨物、札幌、武蔵貨物、松岡満運輸、近物レックス、新潟運輸、王子運送)
            Select Case setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString
                Case "LMC701", "LMC702", "LMC703", "LMC704", "LMC705", "LMC706", "LMC708", "LMC716", "LMC717"
                    '何も処理させない。
                Case Else
                    If dt.Rows(i).Item("MATOME_FLG").Equals("1") = True Then

                        '纏め条件の取得
                        setDs = Me.SelectMatomeIN(setDs)

                        'メッセージの判定
                        If MyBase.IsMessageExist() = True Then
                            Return ds
                        End If

                        '纏め値(個数・重量)の取得
                        setDs = Me.SelectMatomeOUT(setDs)

                        'ADD 2018/08/29 マージ 纏め送状（選択）用
                        Dim sOUTKA_NO_L As String = setDs.Tables("LMC560OUT_MATOME").Rows(0).Item("OUTKA_NO_L").ToString
                        Dim sql As String = String.Empty
                        sql = String.Concat(" OUTKA_NO_L = '", sOUTKA_NO_L, "'")

                        Dim drs As DataRow() = ds.Tables("LMC560OUT_MATOME_SELECT").Select(sql)
                        If drs.Length = 0 Then
                            '同じ纏め出荷番号がないとき
                            ds.Tables("LMC560OUT_MATOME_SELECT").Merge(setDs.Tables("LMC560OUT_MATOME"))

                        End If

                        '=================================='
                        'Add Stt ma-takahashi
                        '=================================='

                        setDs = Me.SelectMatomeORDER(setDs)


                        'ADD 2018/09/27 マージ 纏め送状（選択）用
                        Dim maxODR As Integer = setDs.Tables("LMC560OUT_MATOME_ORD").Rows.Count - 1

                        For x As Integer = 0 To maxODR

                            Dim sOUTKA_NO_L_ORD As String = setDs.Tables("LMC560OUT_MATOME_ORD").Rows(x).Item("OUTKA_NO_L").ToString
                            'Dim sql2 As String = String.Empty
                            sql = String.Concat(" OUTKA_NO_L = '", sOUTKA_NO_L_ORD, "'")

                            Dim dr As DataRow() = ds.Tables("LMC560OUT_MATOME_ORD_SELECT").Select(sql)
                            If dr.Length = 0 Then
                                '同じ纏め出荷番号がないとき
                                ds.Tables("LMC560OUT_MATOME_ORD_SELECT").Merge(setDs.Tables("LMC560OUT_MATOME_ORD"))

                                '該当なしなので終了
                                Exit For

                            End If


                        Next
                        'ds.Tables("LMC560OUT_MATOME_ORD_SELECT").Merge(setDs.Tables("LMC560OUT_MATOME_ORD"))

                        '=================================='
                        'Add End ma-takahashi
                        '=================================='

                        'メッセージの判定
                        If MyBase.IsMessageExist() = True Then
                            Return ds
                        End If


                    End If
            End Select
            '@(2012.06.05)纏め荷札対応 ---  END  ---
            'End If
            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            'ADD Start 2022/12/28 アフトン別名出荷対応
            '商品名をセミEDI出荷指示取込時の値に置き換え
            If dtEdiGn.Rows.Count > 0 Then
                For j As Integer = 0 To setDtOut.Rows.Count - 1
                    Dim dr As DataRow() = dtEdiGn.Select(String.Concat(
                                            "NRS_BR_CD='", setDtOut.Rows(j).Item("NRS_BR_CD").ToString(), "' AND ",
                                            "OUTKA_NO_L='", setDtOut.Rows(j).Item("OUTKA_NO_L").ToString(), "'"))

                    If dr.Length > 0 AndAlso String.IsNullOrWhiteSpace(dr(0).Item("GOODS_NM").ToString()) = False Then
                        setDtOut.Rows(j).Item("GOODS_NM_1") = dr(0).Item("GOODS_NM").ToString()
                    End If
                Next
            End If
            'ADD End   2022/12/28 アフトン別名出荷対応

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            'OUT
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOut.ImportRow(setDtOut.Rows(j))
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next
#If True Then 'ADD 2018/08/29 まとめ結果を退避
        setDs.Tables("LMC560OUT_MATOME_SELECT").Clear()
        setDs.Tables("LMC560OUT_MATOME_SELECT").Merge(ds.Tables("LMC560OUT_MATOME_SELECT"))

        setDs.Tables("LMC560OUT_MATOME_ORD_SELECT").Clear()
        setDs.Tables("LMC560OUT_MATOME_ORD_SELECT").Merge(ds.Tables("LMC560OUT_MATOME_ORD_SELECT"))

#End If


        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        'ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtRpt.Clone

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next
        '20130123 BPまとめ用データ並び換え制御の追加 --- START ---
        'ソート実行
        'BPの帳票で、まとめのもの。
        'For i As Integer = 0 To max
        Select Case setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString
            Case "LMC701", "LMC702", "LMC703", "LMC704", "LMC705", "LMC706", "LMC708", "LMC716", "LMC717"

                If dt.Rows(0).Item("MATOME_FLG").Equals("1") = True Then

                    '要望番号1961　修正START 纏め条件を届先CD⇒顧客運賃纏めコードに変更
                    'Dim outDr As DataRow() = dtOut.Select(Nothing, "NRS_BR_CD ASC, CUST_CD_L ASC, CUST_CD_M ASC, OUTKA_PLAN_DATE ASC, ARR_PLAN_DATE ASC ,DEST_CD ASC")
                    Dim outDr As DataRow() = dtOut.Select(Nothing, "NRS_BR_CD ASC, CUST_CD_L ASC, CUST_CD_M ASC, OUTKA_PLAN_DATE ASC, ARR_PLAN_DATE ASC ,CUST_DEST_CD ASC")
                    '要望番号1961　修正END 纏め条件を届先CD⇒顧客運賃纏めコードに変更

                    'ソート実行後データ格納データセット作成
                    dtOut = dtOut.Clone

                    'ソート済みデータ格納
                    For Each rowOut As DataRow In outDr
                        dtOut.ImportRow(rowOut)
                    Next
                End If
        End Select

        'Next

        '20130123 BPまとめ用データ並び換え制御の追加 --- END ---
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
        For Each dr As DataRow In ds.Tables("M_RPT").Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            Select Case setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString
                Case "LMC701", "LMC702", "LMC703", "LMC704", "LMC705", "LMC706", "LMC708", "LMC716", "LMC717"
                    If dt.Rows(0).Item("MATOME_FLG").Equals("1") = True Then
                        '並び換えしたDateTableを設定 --- START ---
                        'prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMH573BLC.RPT_ID).ToString())
                        prtDs = comPrt.CallDataSet(dtOut, dr.Item(LMC560BLC.RPT_ID).ToString())
                        '(2012.06.11) 要望番号1102 並び換えしたDateTableを設定 ---  END  ---
                    Else
                        '指定したレポートIDのデータを抽出する。(並び替えしてないBP)
                        prtDs = comPrt.CallDataSet(ds.Tables("LMC560OUT"), dr.Item("RPT_ID").ToString())
                    End If
                Case Else

                    '指定したレポートIDのデータを抽出する。
                    prtDs = comPrt.CallDataSet(ds.Tables("LMC560OUT"), dr.Item("RPT_ID").ToString())
            End Select



            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs, setDs)

            'START YANAI 要望番号1478 一括印刷が遅い
            ''TODO 開発元の回答により対応
            ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
            ''1秒（1000ミリ秒）待機する
            'System.Threading.Thread.Sleep(1000)
            ''★★★ END
            'nowSecond = Mid(MyBase.GetSystemTime, 5, 2) '現在の時間の秒を設定

            'If (nowSecond).Equals(beforeSecond) = True Then
            '    '秒が同じ値の場合
            '    '1000ミリ秒 - 現在のミリ秒、待機する
            '    System.Threading.Thread.Sleep(1000 - Convert.ToInt32(Mid(MyBase.GetSystemTime, 7, 3)))
            'Else
            '    '1秒（1000ミリ秒）待機する
            '    System.Threading.Thread.Sleep(1000)
            'End If
            'beforeSecond = nowSecond
            ''END YANAI 要望番号1478 一括印刷が遅い


            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC560OUT"), _
                                  ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function
    ' ''' <summary>
    ' ''' データセットの編集を行う。
    ' ''' </summary>
    ' ''' <param name="prtId"></param>
    ' ''' <param name="ds"></param>
    ' ''' <returns></returns>
    ' ''' <remarks></remarks>
    'Private Function EditPrintDataSet2(ByVal prtId As String, ByVal ds As DataSet, ByVal setDs As DataSet) As DataSet
    '    Dim rtnDs As DataSet
    '    Dim outDt As DataTable = ds.Tables("LMC560OUT")             '送り状データ取得
    '    Dim inDt As DataTable = setDs.Tables("LMC560IN")            '纏めデータ取得(個数･重量)
    '    Dim matDt As DataTable = setDs.Tables("LMC560OUT_MATOME")   '纏めデータ取得(個数･重量)
    '    Dim ordDt As DataTable = setDs.Tables("LMC560OUT_MATOME_ORD") '纏めデータ取得(オーダー番号取得)
    '    Dim max As Integer = ordDt.Rows.Count - 1
    '    Dim rptDs As DataSet

    '    Dim BETU_WT As Double = 0
    '    Dim UNSO_WT As Double = 0
    '    Dim UNSO_PKG_NB As Double = 0
    '    Dim OUTKA_PKG_NB As Double = 0



    '    '20121214 BPCの場合
    '    Dim tableNmRpt As String = "M_RPT"
    '    Dim tableNmOut As String = "LMC560OUT"

    '    '帳票種別用
    '    Dim rptId As String = String.Empty

    '    'キーブレイク用(NRS_BR_CD,PTN_ID,RPT_ID)
    '    Dim keyNrsBrCd As String = String.Empty
    '    Dim keyCustCdL As String = String.Empty
    '    Dim keyCustCdM As String = String.Empty
    '    Dim keyOutkaPlanDate As String = String.Empty
    '    Dim keyArrPlanDate As String = String.Empty
    '    Dim keyDestCd As String = String.Empty

    '    Dim oldkeyNrsBrCd As String = String.Empty
    '    Dim oldkeyCustCdL As String = String.Empty
    '    Dim oldkeyCustCdM As String = String.Empty
    '    Dim oldkeyOutkaPlanDate As String = String.Empty
    '    Dim oldkeyArrPlanDate As String = String.Empty
    '    Dim oldDestCd As String = String.Empty

    '    Dim OrdCnt As Integer = 0 'A
    '    Dim RecCnt As Integer = 0 'S

    '    'レポートID取得
    '    rptId = setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString

    '    If (rptId).Equals("LMC701") = True OrElse _
    '       (rptId).Equals("LMC702") = True OrElse _
    '       (rptId).Equals("LMC703") = True OrElse _
    '       (rptId).Equals("LMC704") = True OrElse _
    '       (rptId).Equals("LMC705") = True OrElse _
    '       (rptId).Equals("LMC706") = True OrElse _
    '       (rptId).Equals("LMC706") = True OrElse _
    '       (rptId).Equals("LMC708") = True OrElse _
    '       (rptId).Equals("LMC716") = True OrElse _
    '       (rptId).Equals("LMC717") = True OrElse _
    '       (rptId).Equals("LMC664") = True Then

    '        'If Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC701" Or _
    '        '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC702" Or _
    '        '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC703" Or _
    '        '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC704" Or _
    '        '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC705" Or _
    '        '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC706" Then
    '        'SQLの都合上、OUTKA_PKG_を省いています。(20130126OUTKA_PKG_NB追加)
    '        If inDt.Rows(0).Item("MATOME_FLG").Equals("1") = True Then
    '            rptDs = ds.Copy
    '            rptDs.Tables(tableNmOut).Clear()
    '            'OrdCnt(オーダーカウント)を1(行目)に設定 A
    '            'OrdCnt = 1
    '            'RecCnt(レコードカウント1(行目)に設定    S
    '            'RecCnt = 1

    '            'NewKeyとOldKeyを空白に
    '            keyNrsBrCd = ""
    '            keyCustCdL = ""
    '            keyCustCdM = ""
    '            keyOutkaPlanDate = ""
    '            keyArrPlanDate = ""
    '            keyDestCd = ""

    '            oldkeyNrsBrCd = ""
    '            oldkeyCustCdL = ""
    '            oldkeyCustCdM = ""
    '            oldkeyOutkaPlanDate = ""
    '            oldkeyArrPlanDate = ""
    '            oldDestCd = ""

    '            '纏め送り状
    '            For i As Integer = 0 To max 'MAXを、纏めるレコード件数分にする。
    '                '纏め送り状の場合、合算された
    '                'outDt.Rows(i).Item("BETU_WT") = matDt.Rows(0).Item("BETU_WT").ToString()
    '                'outDt.Rows(i).Item("UNSO_WT") = matDt.Rows(0).Item("UNSO_WT").ToString()
    '                'outDt.Rows(i).Item("UNSO_PKG_NB") = matDt.Rows(i).Item("UNSO_PKG_NB").ToString()
    '                'outDt.Rows(i).Item("OUTKA_PKG_NB") = matDt.Rows(0).Item("OUTKA_PKG_NB").ToString()

    '                keyNrsBrCd = ordDt.Rows(i).Item("NRS_BR_CD").ToString()
    '                keyCustCdL = ordDt.Rows(i).Item("CUST_CD_L").ToString()
    '                keyCustCdM = ordDt.Rows(i).Item("CUST_CD_M").ToString()
    '                keyOutkaPlanDate = ordDt.Rows(i).Item("OUTKA_PLAN_DATE").ToString()
    '                keyArrPlanDate = ordDt.Rows(i).Item("ARR_PLAN_DATE").ToString()
    '                keyDestCd = ordDt.Rows(i).Item("DEST_CD").ToString

    '                If i = 0 Then
    '                    rptDs.Tables(tableNmOut).ImportRow(outDt.Rows(0))
    '                End If
    '                If String.IsNullOrEmpty(oldkeyNrsBrCd) = True OrElse _
    '                   (keyNrsBrCd = oldkeyNrsBrCd AndAlso _
    '                   keyCustCdL = oldkeyCustCdL AndAlso _
    '                   keyCustCdM = oldkeyCustCdM AndAlso _
    '                   keyOutkaPlanDate = oldkeyOutkaPlanDate AndAlso _
    '                   keyArrPlanDate = oldkeyArrPlanDate AndAlso _
    '                   keyDestCd = oldDestCd) Then

    '                    '加算する(仮)
    '                    BETU_WT = BETU_WT + Convert.ToDouble(outDt.Rows(RecCnt).Item("BETU_WT").ToString())

    '                    '要望番号:1937（予定入力済の送状印刷でアベンド）対応　 2013/03/13 本明Start
    '                    'UNSO_WT = UNSO_WT + Convert.ToDouble(outDt.Rows(i).Item("UNSO_WT").ToString())
    '                    If IsNumeric(outDt.Rows(RecCnt).Item("UNSO_WT").ToString()) Then
    '                        UNSO_WT = UNSO_WT + Convert.ToDouble(outDt.Rows(i).Item("UNSO_WT").ToString())
    '                    End If
    '                    '要望番号:1937（予定入力済の送状印刷でアベンド）対応　 2013/03/13 本明End

    '                    UNSO_PKG_NB = UNSO_PKG_NB + Convert.ToDouble(outDt.Rows(RecCnt).Item("UNSO_PKG_NB").ToString())
    '                    OUTKA_PKG_NB = OUTKA_PKG_NB + Convert.ToDouble(outDt.Rows(RecCnt).Item("OUTKA_PKG_NB").ToString())

    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("BETU_WT") = BETU_WT
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB
    '                    'outDt.Rows(RecCnt).Item("BETU_WT") = BETU_WT
    '                    'outDt.Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
    '                    'outDt.Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
    '                    'outDt.Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB


    '                    'CUST_ORD_NOの設定
    '                    Select Case OrdCnt
    '                        Case 0
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 1
    '                            If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
    '                                outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
    '                            Else
    '                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            End If

    '                        Case 2
    '                            If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
    '                                outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
    '                            Else
    '                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            End If
    '                        Case 3
    '                            If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
    '                                outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
    '                            Else
    '                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            End If
    '                        Case 4
    '                            If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
    '                                outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
    '                            Else
    '                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            End If
    '                        Case 5
    '                            If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
    '                                outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
    '                            Else
    '                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            End If
    '                        Case 6
    '                            If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
    '                                outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
    '                            Else
    '                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            End If
    '                        Case Else
    '                    End Select
    '                    If String.IsNullOrEmpty(outDt.Rows(i).Item("CUST_ORD_NO").ToString()) = False Then
    '                        OrdCnt = OrdCnt + 1
    '                    End If
    '                Else
    '                    'RowAdd
    '                    rptDs.Tables(tableNmOut).ImportRow(outDt.Rows(i))
    '                    '加算しなおす
    '                    BETU_WT = 0
    '                    UNSO_WT = 0
    '                    UNSO_PKG_NB = 0
    '                    OUTKA_PKG_NB = 0

    '                    OrdCnt = 0

    '                    BETU_WT = BETU_WT + Convert.ToDouble(outDt.Rows(i).Item("BETU_WT").ToString())
    '                    UNSO_WT = UNSO_WT + Convert.ToDouble(outDt.Rows(i).Item("UNSO_WT").ToString())
    '                    UNSO_PKG_NB = UNSO_PKG_NB + Convert.ToDouble(outDt.Rows(i).Item("UNSO_PKG_NB").ToString())
    '                    OUTKA_PKG_NB = OUTKA_PKG_NB + Convert.ToDouble(outDt.Rows(i).Item("OUTKA_PKG_NB").ToString())

    '                    'outDt.Rows(RecCnt).Item("BETU_WT") = BETU_WT
    '                    'outDt.Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
    '                    'outDt.Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
    '                    'outDt.Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("BETU_WT") = BETU_WT
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
    '                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB

    '                    'CUST_ORD_NOの設定
    '                    Select Case OrdCnt
    '                        Case 0
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 1
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 2
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 3
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 4
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 5
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case 6
    '                            'outDt.Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                            rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
    '                        Case Else
    '                    End Select

    '                    RecCnt = RecCnt + 1
    '                    If String.IsNullOrEmpty(outDt.Rows(i).Item("CUST_ORD_NO").ToString()) = False Then
    '                        OrdCnt = OrdCnt + 1
    '                    End If

    '                End If

    '                oldkeyNrsBrCd = keyNrsBrCd
    '                oldkeyCustCdL = keyCustCdL
    '                oldkeyCustCdM = keyCustCdM
    '                oldkeyOutkaPlanDate = keyOutkaPlanDate
    '                oldkeyArrPlanDate = keyArrPlanDate


    '            Next
    '            rtnDs = rptDs
    '        Else
    '            rtnDs = ds

    '        End If

    '    Else
    '        If inDt.Rows(0).Item("MATOME_FLG").Equals("1") = True Then
    '            '纏め送り状
    '            For i As Integer = 0 To max
    '                '纏め送り状の場合、合算された
    '                outDt.Rows(i).Item("BETU_WT") = matDt.Rows(0).Item("BETU_WT").ToString()
    '                outDt.Rows(i).Item("UNSO_WT") = matDt.Rows(0).Item("UNSO_WT").ToString()
    '                outDt.Rows(i).Item("UNSO_PKG_NB") = matDt.Rows(0).Item("UNSO_PKG_NB").ToString()
    '                outDt.Rows(i).Item("OUTKA_PKG_NB") = matDt.Rows(0).Item("OUTKA_PKG_NB").ToString()

    '            Next
    '        End If
    '        rtnDs = ds
    '    End If

    '    Return rtnDs

    'End Function
    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="prtId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal prtId As String, ByVal ds As DataSet, ByVal setDs As DataSet) As DataSet

        Dim rtnDs As DataSet
        Dim outDt As DataTable = ds.Tables("LMC560OUT")             '送り状データ取得
        Dim inDt As DataTable = setDs.Tables("LMC560IN")            '纏めデータ取得(個数･重量)
        Dim matDt As DataTable = setDs.Tables("LMC560OUT_MATOME")   '纏めデータ取得(個数･重量)
        Dim ordDt As DataTable = setDs.Tables("LMC560OUT_MATOME_ORD") '纏めデータ取得(オーダー番号取得)
        Dim max As Integer = outDt.Rows.Count - 1
        Dim rptDs As DataSet

        Dim BETU_WT As Double = 0
        Dim UNSO_WT As Double = 0
        Dim UNSO_PKG_NB As Double = 0
        Dim OUTKA_PKG_NB As Double = 0



        '20121214 BPCの場合
        Dim tableNmRpt As String = "M_RPT"
        Dim tableNmOut As String = "LMC560OUT"

        '帳票種別用
        Dim rptId As String = String.Empty

        'キーブレイク用(NRS_BR_CD,PTN_ID,RPT_ID)
        Dim keyNrsBrCd As String = String.Empty
        Dim keyCustCdL As String = String.Empty
        Dim keyCustCdM As String = String.Empty
        Dim keyOutkaPlanDate As String = String.Empty
        Dim keyArrPlanDate As String = String.Empty

        Dim oldkeyNrsBrCd As String = String.Empty
        Dim oldkeyCustCdL As String = String.Empty
        Dim oldkeyCustCdM As String = String.Empty
        Dim oldkeyOutkaPlanDate As String = String.Empty
        Dim oldkeyArrPlanDate As String = String.Empty

        Dim OrdCnt As Integer = 0 'A
        Dim RecCnt As Integer = 0 'S

        Dim chkCUST_ORD_NO As String = String.Empty        'ADD 2018/10/16 依頼番号 002601 

        'レポートID取得
        rptId = setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString

        If (rptId).Equals("LMC837") = True Then
            ' FFEM用送状
            For rowIdx As Integer = 0 To outDt.Rows.Count - 1
                Dim dataRow As DataRow = outDt.Rows(rowIdx)

                ' 重量
                Dim unsoWt As String = dataRow.Item("UNSO_WT").ToString()
                Dim decUnsoWt As Decimal = 0
                If String.IsNullOrEmpty(unsoWt) Then
                    dataRow.Item("UNSO_WT") = "0"
                ElseIf Decimal.TryParse(unsoWt, decUnsoWt) Then
                    decUnsoWt = Me.ToRound(decUnsoWt, 0) ' 四捨五入
                    dataRow.Item("UNSO_WT") = decUnsoWt.ToString()
                End If
            Next
        End If

        If (rptId).Equals("LMC701") = True OrElse _
           (rptId).Equals("LMC702") = True OrElse _
           (rptId).Equals("LMC703") = True OrElse _
           (rptId).Equals("LMC704") = True OrElse _
           (rptId).Equals("LMC705") = True OrElse _
           (rptId).Equals("LMC706") = True OrElse _
           (rptId).Equals("LMC706") = True OrElse _
           (rptId).Equals("LMC708") = True OrElse _
           (rptId).Equals("LMC716") = True OrElse _
           (rptId).Equals("LMC717") = True  Then

            'If Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC701" Or _
            '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC702" Or _
            '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC703" Or _
            '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC704" Or _
            '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC705" Or _
            '   Not setDs.Tables(tableNmRpt).Rows(0).Item("RPT_ID").ToString = "LMC706" Then
            'SQLの都合上、OUTKA_PKG_を省いています。(20130126OUTKA_PKG_NB追加)
            If inDt.Rows(0).Item("MATOME_FLG").Equals("1") = True Then
                rptDs = ds.Copy
                rptDs.Tables(tableNmOut).Clear()
                'OrdCnt(オーダーカウント)を1(行目)に設定 A
                'OrdCnt = 1
                'RecCnt(レコードカウント1(行目)に設定    S
                'RecCnt = 1

                'NewKeyとOldKeyを空白に
                keyNrsBrCd = ""
                keyCustCdL = ""
                keyCustCdM = ""
                keyOutkaPlanDate = ""
                keyArrPlanDate = ""

                oldkeyNrsBrCd = ""
                oldkeyCustCdL = ""
                oldkeyCustCdM = ""
                oldkeyOutkaPlanDate = ""
                oldkeyArrPlanDate = ""

                '纏め送り状
                For i As Integer = 0 To max 'MAXを、纏めるレコード件数分にする。
                    '纏め送り状の場合、合算された
                    'outDt.Rows(i).Item("BETU_WT") = matDt.Rows(0).Item("BETU_WT").ToString()
                    'outDt.Rows(i).Item("UNSO_WT") = matDt.Rows(0).Item("UNSO_WT").ToString()
                    'outDt.Rows(i).Item("UNSO_PKG_NB") = matDt.Rows(i).Item("UNSO_PKG_NB").ToString()
                    'outDt.Rows(i).Item("OUTKA_PKG_NB") = matDt.Rows(0).Item("OUTKA_PKG_NB").ToString()

                    keyNrsBrCd = outDt.Rows(i).Item("NRS_BR_CD").ToString()
                    keyCustCdL = outDt.Rows(i).Item("CUST_CD_L").ToString()
                    keyCustCdM = outDt.Rows(i).Item("CUST_CD_M").ToString()
                    keyOutkaPlanDate = outDt.Rows(i).Item("OUTKA_PLAN_DATE").ToString()
                    keyArrPlanDate = outDt.Rows(i).Item("ARR_PLAN_DATE").ToString()
                    If i = 0 Then
                        rptDs.Tables(tableNmOut).ImportRow(outDt.Rows(i))
                    End If
                    If String.IsNullOrEmpty(oldkeyNrsBrCd) = True OrElse _
                       (keyNrsBrCd = oldkeyNrsBrCd AndAlso _
                       keyCustCdL = oldkeyCustCdL AndAlso _
                       keyCustCdM = oldkeyCustCdM AndAlso _
                       keyOutkaPlanDate = oldkeyOutkaPlanDate AndAlso _
                       keyArrPlanDate = oldkeyArrPlanDate) Then

                        '加算する(仮)
                        BETU_WT = BETU_WT + Convert.ToDouble(outDt.Rows(i).Item("BETU_WT").ToString())

                        '要望番号:1937（予定入力済の送状印刷でアベンド）対応　 2013/03/13 本明Start
                        'UNSO_WT = UNSO_WT + Convert.ToDouble(outDt.Rows(i).Item("UNSO_WT").ToString())
                        If IsNumeric(outDt.Rows(i).Item("UNSO_WT").ToString()) Then
                            UNSO_WT = UNSO_WT + Convert.ToDouble(outDt.Rows(i).Item("UNSO_WT").ToString())
                        End If
                        '要望番号:1937（予定入力済の送状印刷でアベンド）対応　 2013/03/13 本明End

                        UNSO_PKG_NB = UNSO_PKG_NB + Convert.ToDouble(outDt.Rows(i).Item("UNSO_PKG_NB").ToString())
                        OUTKA_PKG_NB = OUTKA_PKG_NB + Convert.ToDouble(outDt.Rows(i).Item("OUTKA_PKG_NB").ToString())

                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("BETU_WT") = BETU_WT
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB
                        'outDt.Rows(RecCnt).Item("BETU_WT") = BETU_WT
                        'outDt.Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
                        'outDt.Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
                        'outDt.Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB


                        'CUST_ORD_NOの設定
                        Select Case OrdCnt
                            Case 0
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                            Case 1
                                If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
                                    outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
                                Else
                                    'outDt.Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                End If

                            Case 2
                                If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
                                    outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
                                Else
                                    'outDt.Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                End If
                            Case 3
                                If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
                                    outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
                                Else
                                    'outDt.Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                End If
                            Case 4
                                If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
                                    outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
                                Else
                                    'outDt.Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                End If
                            Case 5
                                If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
                                    outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
                                Else
                                    'outDt.Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                End If
                            Case 6
                                If outDt.Rows(i).Item("CUST_ORD_NO").ToString().Equals(outDt.Rows(i - 1).Item("CUST_ORD_NO").ToString()) = True Then
                                    outDt.Rows(i).Item("CUST_ORD_NO") = String.Empty
                                Else
                                    'outDt.Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                    rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                End If
                            Case Else
                        End Select
                        If String.IsNullOrEmpty(outDt.Rows(i).Item("CUST_ORD_NO").ToString()) = False Then
                            OrdCnt = OrdCnt + 1
                        End If
                    Else
                        'RowAdd
                        rptDs.Tables(tableNmOut).ImportRow(outDt.Rows(i))
                        '加算しなおす
                        BETU_WT = 0
                        UNSO_WT = 0
                        UNSO_PKG_NB = 0
                        OUTKA_PKG_NB = 0

                        OrdCnt = 0

                        BETU_WT = BETU_WT + Convert.ToDouble(outDt.Rows(i).Item("BETU_WT").ToString())
                        UNSO_WT = UNSO_WT + Convert.ToDouble(outDt.Rows(i).Item("UNSO_WT").ToString())
                        UNSO_PKG_NB = UNSO_PKG_NB + Convert.ToDouble(outDt.Rows(i).Item("UNSO_PKG_NB").ToString())
                        OUTKA_PKG_NB = OUTKA_PKG_NB + Convert.ToDouble(outDt.Rows(i).Item("OUTKA_PKG_NB").ToString())

                        'outDt.Rows(RecCnt).Item("BETU_WT") = BETU_WT
                        'outDt.Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
                        'outDt.Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
                        'outDt.Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("BETU_WT") = BETU_WT
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_WT") = UNSO_WT
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("UNSO_PKG_NB") = UNSO_PKG_NB
                        rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_PKG_NB") = OUTKA_PKG_NB

                        'CUST_ORD_NOの設定
                        Select Case OrdCnt
                            Case 0
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_1") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_1") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case 1
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_2") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_2") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case 2
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_3") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_3") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case 3
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_4") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_4") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case 4
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_5") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_5") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case 5
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_6") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_6") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case 6
                                'outDt.Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("CUST_ORD_NO_7") = outDt.Rows(i).Item("CUST_ORD_NO").ToString()
                                rptDs.Tables(tableNmOut).Rows(RecCnt).Item("OUTKA_NO_L_7") = outDt.Rows(i).Item("OUTKA_NO_L").ToString()        'ADD 2018/09/26 
                            Case Else
                        End Select

                        RecCnt = RecCnt + 1
                        If String.IsNullOrEmpty(outDt.Rows(i).Item("CUST_ORD_NO").ToString()) = False Then
                            OrdCnt = OrdCnt + 1
                        End If

                    End If

                    oldkeyNrsBrCd = keyNrsBrCd
                    oldkeyCustCdL = keyCustCdL
                    oldkeyCustCdM = keyCustCdM
                    oldkeyOutkaPlanDate = keyOutkaPlanDate
                    oldkeyArrPlanDate = keyArrPlanDate


                Next
                rtnDs = rptDs
            Else
                rtnDs = ds

            End If

        Else
            If inDt.Rows(0).Item("MATOME_FLG").Equals("1") = True Then
                rptDs = ds.Copy
                rptDs.Tables(tableNmOut).Clear()
                '纏め送り状
                'UPD 2018/09/25 依頼番号 : 002487  最初の１件でけで処理（まとめの１件だけを出力）
                For i As Integer = 0 To max
                    'For i As Integer = 0 To 0

                    '纏め送り状の場合、合算された
                    outDt.Rows(i).Item("BETU_WT") = matDt.Rows(0).Item("BETU_WT").ToString()
                    outDt.Rows(i).Item("UNSO_WT") = matDt.Rows(0).Item("UNSO_WT").ToString()
                    '日医工用仕様でオーダー番号の一番若いデータに手打ちで総個数（荷札分）を入力している。
                    If (rptId).Equals("LMC664") = False Then
#If False Then     'UPD 2018/10/16 依頼番号 : 002601   【LMS】日医工_まとめ送状(選択)_送り状番号更新 ＋ オーダー番号最小の個数のみ送り状表示(千葉BC柴田) 
                        outDt.Rows(i).Item("UNSO_PKG_NB") = matDt.Rows(0).Item("UNSO_PKG_NB").ToString()
                        outDt.Rows(i).Item("OUTKA_PKG_NB") = matDt.Rows(0).Item("OUTKA_PKG_NB").ToString()

#Else
                        outDt.Rows(i).Item("UNSO_PKG_NB") = outDt.Rows(0).Item("UNSO_PKG_NB").ToString()
                        outDt.Rows(i).Item("OUTKA_PKG_NB") = outDt.Rows(0).Item("OUTKA_PKG_NB").ToString()

#End If
                    End If

                    If inDt.Rows(0).Item("MATOME_SELECT_FLG").Equals("1") = True Then
#If True Then       'ADD 2018/08/07 依頼番号 : 001572  

                        '纏め送り状（選択）

                        Dim sNRS_BR_CD As String = outDt.Rows(i).Item("NRS_BR_CD").ToString()
                        Dim sCUST_CD_L As String = outDt.Rows(i).Item("CUST_CD_L").ToString()
                        Dim sCUST_CD_M As String = outDt.Rows(i).Item("CUST_CD_M").ToString()
                        Dim sOUTKA_PLAN_DATE As String = outDt.Rows(i).Item("OUTKA_PLAN_DATE").ToString()
                        Dim sARR_PLAN_DATE As String = outDt.Rows(i).Item("ARR_PLAN_DATE").ToString()
                        'Dim sUNSO_CD As String = outDt.Rows(i).Item("UNSO_CD").ToString()
                        'Dim sUNSO_BR_CD As String = outDt.Rows(i).Item("UNSO_BR_CD").ToString()
                        Dim sDEST_CD As String = outDt.Rows(i).Item("DEST_CD").ToString()

                        Dim sql As String = String.Empty
                        sql = String.Concat(" NRS_BR_CD = '", sNRS_BR_CD, "' AND CUST_CD_L = '", sCUST_CD_L, "' AND OUTKA_PLAN_DATE = '", sOUTKA_PLAN_DATE, "' AND ARR_PLAN_DATE = '", sARR_PLAN_DATE _
                                            , "' AND DEST_CD = '", sDEST_CD, "'")


                        Dim drs As DataRow() = setDs.Tables("LMC560OUT_MATOME_SELECT").Select(sql)
                        If drs.Length > 0 Then
                            outDt.Rows(i).Item("UNSO_PKG_NB") = drs(0).Item("UNSO_PKG_NB").ToString()
                            'outDt.Rows(i).Item("OUTKA_PKG_NB") = drs(0).Item("OUTKA_PKG_NB").ToString()        'DEL 2018/10/16 依頼番号 : 002601   【LMS】日医工_まとめ送状(選択)_送り状番号更新 ＋ オーダー番号最小の個数のみ送り状表示(千葉BC柴田) 
                            outDt.Rows(i).Item("OUTKA_NO_L") = drs(0).Item("OUTKA_NO_L").ToString()
                            outDt.Rows(i).Item("UNSO_WT") = drs(0).Item("UNSO_WT").ToString()
                        End If

                        Dim drsORD As DataRow() = setDs.Tables("LMC560OUT_MATOME_ORD_SELECT").Select(sql)
                        If drsORD.Length > 0 Then
                            Dim outDtlmax As Integer = drsORD.Length - 1
                            For j As Integer = 0 To outDtlmax
                                Select Case j
                                    Case 0
                                        outDt.Rows(i).Item("CUST_ORD_NO_1") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_1") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                        outDt.Rows(i).Item("OUTKA_NO_L_2") = String.Empty                                                     'ADD 2018/09/26 
                                        outDt.Rows(i).Item("OUTKA_NO_L_3") = String.Empty                                                     'ADD 2018/09/26 
                                        outDt.Rows(i).Item("OUTKA_NO_L_4") = String.Empty                                                     'ADD 2018/09/26 
                                        outDt.Rows(i).Item("OUTKA_NO_L_5") = String.Empty                                                     'ADD 2018/09/26 
                                        outDt.Rows(i).Item("OUTKA_NO_L_6") = String.Empty                                                     'ADD 2018/09/26 
                                        outDt.Rows(i).Item("OUTKA_NO_L_2") = String.Empty                                                     'ADD 2018/09/26 
                                    Case 1
                                        outDt.Rows(i).Item("CUST_ORD_NO_2") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_2") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                    Case 2
                                        outDt.Rows(i).Item("CUST_ORD_NO_3") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_3") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                    Case 3
                                        outDt.Rows(i).Item("CUST_ORD_NO_4") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_4") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                    Case 4
                                        outDt.Rows(i).Item("CUST_ORD_NO_5") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_5") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                    Case 5
                                        outDt.Rows(i).Item("CUST_ORD_NO_6") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_6") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                    Case 6
                                        outDt.Rows(i).Item("CUST_ORD_NO_7") = drsORD(j).Item("CUST_ORD_NO").ToString
                                        outDt.Rows(i).Item("OUTKA_NO_L_7") = drsORD(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                End Select
                            Next

                            '纏めの１件だけ対象にする
                            sql = String.Concat(" OUTKA_NO_L = '", outDt.Rows(i).Item("OUTKA_NO_L").ToString(), "'")

                            Dim drOUT As DataRow() = rptDs.Tables(tableNmOut).Select(sql)
                            If drOUT.Length = 0 Then

                                rptDs.Tables(tableNmOut).ImportRow(outDt.Rows(i))

                            End If

                        End If
                    Else
                        Dim outDtlmax As Integer = ordDt.Rows.Count - 1
                        For j As Integer = 0 To outDtlmax
                            Select Case j
                                Case 0
                                    outDt.Rows(i).Item("CUST_ORD_NO_1") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_1") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                    outDt.Rows(i).Item("OUTKA_NO_L_2") = String.Empty                                                     'ADD 2018/09/26 
                                    outDt.Rows(i).Item("OUTKA_NO_L_3") = String.Empty                                                     'ADD 2018/09/26 
                                    outDt.Rows(i).Item("OUTKA_NO_L_4") = String.Empty                                                     'ADD 2018/09/26 
                                    outDt.Rows(i).Item("OUTKA_NO_L_5") = String.Empty                                                     'ADD 2018/09/26 
                                    outDt.Rows(i).Item("OUTKA_NO_L_6") = String.Empty                                                     'ADD 2018/09/26 
                                    outDt.Rows(i).Item("OUTKA_NO_L_2") = String.Empty                                                     'ADD 2018/09/26 
                                Case 1
                                    outDt.Rows(i).Item("CUST_ORD_NO_2") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_2") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                Case 2
                                    outDt.Rows(i).Item("CUST_ORD_NO_3") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_3") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                Case 3
                                    outDt.Rows(i).Item("CUST_ORD_NO_4") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_4") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                Case 4
                                    outDt.Rows(i).Item("CUST_ORD_NO_5") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_5") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                Case 5
                                    outDt.Rows(i).Item("CUST_ORD_NO_6") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_6") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                                Case 6
                                    outDt.Rows(i).Item("CUST_ORD_NO_7") = ordDt.Rows(j).Item("CUST_ORD_NO").ToString
                                    outDt.Rows(i).Item("OUTKA_NO_L_7") = ordDt.Rows(j).Item("OUTKA_NO_L").ToString                        'ADD 2018/09/26 
                            End Select
                        Next

                        rptDs.Tables(tableNmOut).ImportRow(outDt.Rows(i))

                    End If

                Next

                rtnDs = rptDs
#End If

            Else
                rtnDs = ds
            End If
        End If

        Return rtnDs

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
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

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

    '@(2012.06.05) 纏め送り状対応 --- START ---
    ''' <summary>
    ''' 纏め送り状抽出条件取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeIN(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatomeIN", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 纏め送り状抽出条件取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeOUT(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatomeOUT", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    '@(2012.06.05) 纏め送り状対応 ---  END  ---
    ''' <summary>
    ''' 纏め送り状オーダー番号取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMatomeORDER(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatomeORDER", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    '@(2012.06.05) 纏め送り状対応 ---  END  ---

    'ADD Start 2022/12/28 アフトン別名出荷対応
    ''' <summary>
    ''' セミEDI出荷指示取込時の商品名取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOutkaediGoodsnm(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectOutkaediGoodsnm", ds)

    End Function
    'ADD End   2022/12/28 アフトン別名出荷対応

    ''' <summary>
    ''' 数値の四捨五入
    ''' </summary>
    ''' <param name="value">四捨五入を行う数値</param>
    ''' <param name="value2">四捨五入後の数値の有効小数桁数</param>
    ''' <returns>四捨五入した数値</returns>
    ''' <remarks></remarks>
    Private Function ToRound(ByVal value As Decimal, ByVal value2 As Integer) As Decimal

        Dim maxLength As Decimal = Convert.ToDecimal(System.Math.Pow(10, value2))

        If value > 0 Then
            'value値が0より大きい場合は、Floor を使用して四捨五入
            Return Convert.ToDecimal(System.Math.Floor((value * maxLength) + 0.5) / maxLength)
        Else
            'value値が0以下の場合は、Ceiling を使用して四捨五入
            Return Convert.ToDecimal(System.Math.Ceiling((value * maxLength) - 0.5) / maxLength)
        End If

    End Function
#End Region

#End Region

End Class
