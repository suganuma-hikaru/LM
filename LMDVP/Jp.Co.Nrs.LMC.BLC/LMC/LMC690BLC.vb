' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC690BLC : 纏めピッキングリスト
'  作  成  者       :  YAMANAKA
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC690BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC690BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC690DAC = New LMC690DAC()

#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = "LMC690IN"
        Dim tableNmOut As String = "LMC690OUT"
        Dim tableNmRpt As String = "M_RPT"
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
        Dim workDtRpt As DataTable = dtRpt.Clone

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start
            'メッセージの判定
            If setDs.Tables("M_RPT").Rows.Count = 0 Then
                Continue For
            End If

            'If MyBase.IsMessageExist() = True Then
            '    Return ds
            'End If
            '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 End

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

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

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        'ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD")

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
        Dim comPrt As LMReportDesignerUtility
        Dim prtmax As Integer = 0

        '2012.11.13 yamanaka 千葉_東レ対応 Start
        '帳票ID格納用
        Dim rptId As String = String.Empty

        '作業用データテーブル
        Dim wkDt As DataTable = New DataTable
        wkDt.Merge(dtOut)
        wkDt.Clear()
        '2012.11.13 yamanaka 千葉_東レ対応 End

        For Each dr As DataRow In ds.Tables("M_RPT").Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            '印刷枚数指定
            Select Case dr.Item("RPT_ID").ToString
                Case "LMC691"
                    '千葉_DIC用
                    prtmax = 2
                    rptId = "LMC691"

                Case "LMC693"
                    '千葉_東レ用
                    prtmax = 3
                    rptId = "LMC693"

                    '埼玉BP・カストロール対応 yamanaka 2012.12.05 Start
                Case "LMC697"
                    rptId = "LMC697"
                    '埼玉BP・カストロール対応 yamanaka 2012.12.05 End

                    '九州　大塚倉庫用　まとめピック　追加START
                Case "LMC698"
                    rptId = "LMC698"
                    '九州　大塚倉庫用　まとめピック　追加END

                    '千葉　アクタス纏めピック対応START
                Case "LMC770"
                    '千葉_アクタス用
                    rptId = "LMC770"
                    '千葉　アクタス纏めピック対応END

                Case "LMC771"
                    '九州_ナフコ用
                    rptId = "LMC771"
                    '九州　ナフコ纏めピック対応END

                Case "LMC772"
                    '横浜　出荷日汎用(ストラタシス)
                    rptId = "LMC772"

                Case "LMC773"
                    '日医工(千葉・大阪)　出荷日汎用(ストラタシス)
                    rptId = "LMC773"

                Case Else
                    '群馬纏めピッキングリスト
                    rptId = "LMC690"

            End Select

            '印刷処理実行
            For j As Integer = 0 To prtmax

                comPrt = New LMReportDesignerUtility
                prtDs = New DataSet

                '指定したレポートIDのデータを抽出する。
                prtDs = comPrt.CallDataSet(dtOut, dr.Item("RPT_ID").ToString())

                '帳票ID変更
                Select Case dr.Item("RPT_ID").ToString
                    Case "LMC691"
                        '要望番号2516 20160205 tsunehira add start
                        Select Case j
                            Case 1
                                rptId = "LMC692"
                            Case 2
                                rptId = "LMC816"
                        End Select
                        '要望番号2516 20160205 tsunehira add end

                        'Case "LMC691"
                        'If j = 1 Then
                        '    rptId = "LMC692"
                        'End If
                        'End Select
 
                    Case "LMC693"
                        Select Case j
                            Case 1
                                rptId = "LMC694"

                            Case 2
                                rptId = "LMC695"

                            Case 3
                                For k As Integer = 0 To prtDs.Tables("LMC690OUT").Rows.Count - 1
                                    Select Case prtDs.Tables("LMC690OUT").Rows(k).Item("TOU_NO").ToString()
                                        Case "91", "92", "93", "94"
                                            wkDt.ImportRow(prtDs.Tables("LMC690OUT").Rows(k))

                                    End Select
                                Next

                                If wkDt.Rows.Count = 0 Then
                                    Continue For
                                End If

                                prtDs.Clear()
                                prtDs.Tables("LMC690OUT").Merge(wkDt)
                                rptId = "LMC696"

                        End Select
                End Select

                '帳票ごとの編集があるなら行う。
                prtDs = Me.EditPrintDataSet(rptId, prtDs)

                '帳票CSV出力
                comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      rptId, _
                      prtDs.Tables("LMC690OUT"), _
                      ds.Tables(LMConst.RD))
            Next

            '        '印刷処理実行
            '        comPrt = New LMReportDesignerUtility
            '        prtDs = New DataSet

            '        '指定したレポートIDのデータを抽出する。
            '        prtDs = comPrt.CallDataSet(dtOut, dr.Item("RPT_ID").ToString(), "TOU_NO, SITU_NO, " & _
            '                                              "CUST_NM_L, " & _
            '                                              "UNSO_CD, " & _
            '                                              "UNSOCO_NM, " & _
            '                                              "ZONE_CD, " & _
            '                                              "LOCA, " & _
            '                                              "GOODS_CD_CUST, " & _
            '                                              "LOT_NO, " & _
            '                                              "IRIME, " & _
            '                                              "ZAN_KOSU DESC, " & _
            '                                              "ZAN_HASU DESC")

            '        '帳票ごとの編集があるなら行う。
            '        prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)

            '        '帳票CSV出力
            '        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
            '                              dr.Item("PTN_ID").ToString(), _
            '                              dr.Item("PTN_CD").ToString(), _
            '                              dr.Item("RPT_ID").ToString(), _
            '                              prtDs.Tables("LMC690OUT"), _
            '                              ds.Tables(LMConst.RD))
        Next

        Return ds

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
            '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start
            MyBase.SetMessageStore("00", "E070", , ds.Tables("LMC690IN").Rows(0).Item("ROW_NO").ToString)
            'MyBase.SetMessage("G021")
            '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 End

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

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        Dim outDt As DataTable = ds.Tables("LMC690OUT") 'データ取得
        Dim max As Integer = outDt.Rows.Count - 1       '抽出データ数取得

        '2012.11.07 yamanaka Start
        Dim orderBy As String = String.Empty

        Select Case rptId
            Case "LMC691"
                '要望番号1809:(千葉　日立物流纏めピッキングリストのソート順変更 運送会社コード、商品名、入目、ロット番号) 2013/01/30 umano Start
                'orderBy = "OUTKO_DATE, UNSOCO_NM, TOU_NO, SITU_NO, ZONE_CD, LOCA, LOT_NO, OUTKA_NO_L"
                orderBy = "UNSO_CD, GOODS_NM_1, IRIME, LOT_NO "
                '要望番号1809:(千葉　日立物流纏めピッキングリストのソート順変更 運送会社コード、商品名、入目、ロット番号) 2013/01/30 umano End

            Case "LMC692", "LMC816"  'LMC816:要望番号2516 20160205 tsunehira add 
                '要望番号1809:(千葉　日立物流纏めピッキングリストのソート順変更 置き場、商品名、入目、ロット番号、残数) 2013/01/30 umano Start
                'orderBy = "OUTKO_DATE, TOU_NO, SITU_NO, ZONE_CD, LOCA, LOT_NO, OUTKA_NO_L"
                orderBy = "TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_NM_1, IRIME, LOT_NO, ZAN_KOSU_SORT DESC "
                '要望番号1809:(千葉　日立物流纏めピッキングリストのソート順変更 置き場、商品名、入目、ロット番号、残数) 2013/01/30 umano End

            Case "LMC693"
                orderBy = "OUTKO_DATE, EDI_CTL_NO, GOODS_NM_1, IRIME, LOT_NO"

            Case "LMC694"
                orderBy = "OUTKO_DATE, TOU_NO, SITU_NO, ZONE_CD, LOCA, LOT_NO, EDI_CTL_NO"

            Case "LMC695", "LMC696"
                orderBy = "OUTKO_DATE, KBN_NM1, EDI_CTL_NO, GOODS_NM_1, IRIME, LOT_NO"

                '要望番号:1914 yamanaka 2013.03.04 Start
                '要望対応:1874 yamanaka 2013.02.20 Start
                '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 Start
                '埼玉BP・カストロール対応 yamanaka 2012.12.05 Start
            Case "LMC697"
                'orderBy = String.Concat("TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_CD_CUST" _
                '                      , ", UNSOCO_NM, UNSOCO_BR_NM, INKO_DATE, OUTKA_NO_L")
                'orderBy = "TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, UNSOCO_NM, UNSOCO_BR_NM, ZAN_KOSU DESC, OUTKA_NO_L"
                'orderBy = "TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, INKO_DATE, UNSOCO_NM, UNSOCO_BR_NM, ZAN_KOSU DESC, OUTKA_NO_L"
                orderBy = "TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, UNSOCO_NM, UNSOCO_BR_NM, INKO_DATE, ZAN_KOSU DESC, OUTKA_NO_L"
                '埼玉BP・カストロール対応 yamanaka 2012.12.05 End
                '埼玉BP・カストロール修正対応 yamanaka 2013.01.21 End
                '要望対応:1874 yamanaka 2013.02.20 End
                '要望番号:1914 yamanaka 2013.03.04 End

            Case "LMC698"

                '千葉　アクタス纏めピック対応START
            Case "LMC770"
                orderBy = "OUTKO_DATE, TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_NM_1, OUTKA_NO_L"
                '千葉　アクタス纏めピック対応END

                '九州　ナフコ纏めピック対応START
            Case "LMC771"
                orderBy = "KEN_NM, TOU_NO, SITU_NO, ZONE_CD, LOCA, GOODS_NM_1, OUTKA_NO_L"
                '九州　ナフコ纏めピック対応END

            Case "LMC772"
                '横浜　ストラタシス　出庫日単位での出力
                orderBy = "OUTKO_DATE,TOU_NO, SITU_NO, UNSOCO_NM, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, IRIME, ZAN_KOSU_SORT DESC,ZAN_KOSU DESC "

            Case "LMC773"
                '日医工　届け先・商品単位での出力
                orderBy = "OUTKO_DATE,   DEST_NM,AD_1,AD_2,  GOODS_CD_CUST, LOT_NO, IRIME, ZAN_KOSU_SORT DESC "

            Case Else
                '要望番号1755:(群馬BC 出荷指示書 同一商品・同一置き場のレコードを残個数の降順で出力) 2013/01/09 本明 Start
                'orderBy = "TOU_NO, SITU_NO, UNSOCO_NM, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, IRIME"
                orderBy = "TOU_NO, SITU_NO, UNSOCO_NM, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, IRIME, INKO_DATE ,ZAN_KOSU_SORT DESC "
                '要望番号1755:(群馬BC 出荷指示書 同一商品・同一置き場のレコードを残個数の降順で出力) 2013/01/09 本明 End


        End Select

        'Dim orderBy As String = "TOU_NO, SITU_NO, UNSOCO_NM, ZONE_CD, LOCA, GOODS_CD_CUST, LOT_NO, IRIME"
        '2012.11.07 yamanaka End

        '並べ替えて入れなおす
        Dim tempDt As DataTable = ds.Clone.Tables("LMC690OUT")
        Dim outDrs As DataRow() = outDt.Select(Nothing, orderBy)
        Dim preDt As DataTable = ds.Clone.Tables("LMC690OUT")

        Dim rowIndex As Integer = 1
        For Each dr As DataRow In outDrs
            If preDt.Rows.Count = 0 OrElse Me.IsNewPage(dr, preDt.Rows(0), rptId) = True Then
                rowIndex = 1
            Else
                rowIndex = rowIndex + 1
            End If
            dr.Item("ROW_COUNT") = rowIndex.ToString()

            '埼玉BP・カストロール対応 yamanaka 2012.12.10 Start
            If rptId.Equals("LMC697") = True _
            AndAlso dr.Item("TOU_NO").ToString().Equals("03") Then
                dr.Item("LOCA_HED") = Left(dr.Item("LOCA").ToString(), 1)
            End If
            '埼玉BP・カストロール対応 yamanaka 2012.12.10 Start

            tempDt.ImportRow(dr)

            preDt.Rows.Clear()
            preDt.ImportRow(dr)
        Next

        outDt.Clear()
        For Each dr As DataRow In tempDt.Rows
            outDt.ImportRow(dr)
        Next
        Return ds

    End Function

    ''' <summary>
    ''' 改ページがされるかの判断
    ''' </summary>
    ''' <param name="nowDr"></param>
    ''' <param name="preDr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsNewPage(ByVal nowDr As DataRow, ByVal preDr As DataRow, ByVal rptId As String) As Boolean

        '2012.11.07 yamanaka Start
        'ヘッダ条件がひとつでも違ったら、Trueを返す
        Select Case rptId
            Case "LMC691"
                If (nowDr.Item("UNSOCO_NM").ToString().Equals(preDr.Item("UNSOCO_NM").ToString()) = True _
                    AndAlso nowDr.Item("OUTKO_DATE").ToString().Equals(preDr.Item("OUTKO_DATE").ToString()) = True) = False Then
                    Return True
                End If

            Case "LMC692", "LMC694", "LMC816" 'LMC816:要望番号2516 20160205 tsunehira add
                If (nowDr.Item("TOU_NO").ToString().Equals(preDr.Item("TOU_NO").ToString()) = True _
                    AndAlso nowDr.Item("OUTKO_DATE").ToString().Equals(preDr.Item("OUTKO_DATE").ToString()) = True) = False Then
                    Return True
                End If

            Case "LMC693", "LMC695", "LMC696"
                If nowDr.Item("OUTKO_DATE").ToString().Equals(preDr.Item("OUTKO_DATE").ToString()) = False Then
                    Return True
                End If

                '埼玉BP・カストロール対応 yamanaka 2012.12.05 Start
            Case "LMC697"
                If (nowDr.Item("OUTKA_PLAN_DATE").ToString().Equals(preDr.Item("OUTKA_PLAN_DATE").ToString()) = True _
                    AndAlso nowDr.Item("TOU_NO").ToString().Equals(preDr.Item("TOU_NO").ToString()) = True _
                ) = False Then
                    Return True
                End If
                '埼玉BP・カストロール対応 yamanaka 2012.12.05 End

                '横浜　ストラタシス　出庫日単位
            Case "LMC792"
                If (nowDr.Item("TOU_NO").ToString().Equals(preDr.Item("TOU_NO").ToString()) = True _
                    AndAlso nowDr.Item("SITU_NO").ToString().Equals(preDr.Item("SITU_NO").ToString()) = True _
                    AndAlso nowDr.Item("UNSOCO_NM").ToString().Equals(preDr.Item("UNSOCO_NM").ToString()) = True _
                    AndAlso nowDr.Item("CUST_NM_L").ToString().Equals(preDr.Item("CUST_NM_L").ToString()) = True _
                    AndAlso nowDr.Item("OUTKO_DATE").ToString().Equals(preDr.Item("OUTKO_DATE").ToString()) = True _
                       ) = False Then
                    Return True
                End If
                '横浜　ストラタシス　出庫日単位

            Case Else
                If (nowDr.Item("TOU_NO").ToString().Equals(preDr.Item("TOU_NO").ToString()) = True _
                    AndAlso nowDr.Item("SITU_NO").ToString().Equals(preDr.Item("SITU_NO").ToString()) = True _
                    AndAlso nowDr.Item("UNSOCO_NM").ToString().Equals(preDr.Item("UNSOCO_NM").ToString()) = True _
                    AndAlso nowDr.Item("CUST_NM_L").ToString().Equals(preDr.Item("CUST_NM_L").ToString()) = True _
                    AndAlso nowDr.Item("ARR_PLAN_DATE").ToString().Equals(preDr.Item("ARR_PLAN_DATE").ToString()) = True _
                       ) = False Then
                    Return True
                End If

        End Select

        Return False

        '    If (nowDr.Item("TOU_NO").ToString().Equals(preDr.Item("TOU_NO").ToString()) = True _
        '        AndAlso nowDr.Item("SITU_NO").ToString().Equals(preDr.Item("SITU_NO").ToString()) = True _
        '        AndAlso nowDr.Item("UNSOCO_NM").ToString().Equals(preDr.Item("UNSOCO_NM").ToString()) = True _
        '        AndAlso nowDr.Item("CUST_NM_L").ToString().Equals(preDr.Item("CUST_NM_L").ToString()) = True _
        '        AndAlso nowDr.Item("ARR_PLAN_DATE").ToString().Equals(preDr.Item("ARR_PLAN_DATE").ToString()) = True _
        '           ) = False Then

        '        Return True
        '    Else
        '        Return False
        '    End If
        '2012.11.07 yamanaka End

    End Function

#End Region

#End Region

End Class
