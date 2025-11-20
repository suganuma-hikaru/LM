' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC789    : 名鉄・荷札
'  作  成  者       :  tsunehira
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC789BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC789BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC789DAC = New LMC789DAC()

#End Region

#Region "Method"

#Region "荷札データ取得処理"

    ''' <summary>
    ''' 送り状データ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのMeitetuCsvメソッド呼出</remarks>
    Private Function MeitetuTag(ByVal ds As DataSet) As DataSet

        '印刷対象データの取得
        Dim rtnDs As DataSet = Me.getMeitetuTag(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables("LMC789OUT").Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E070")
            Return ds
        End If

        Return rtnDs

    End Function

    ' ''' <summary>
    ' ''' データ検索
    ' ''' </summary>
    ' ''' <param name="ds">DataSet</param>
    ' ''' <returns>DataSet（プロキシ）</returns>
    ' ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    'Private Function getMeitetuTag(ByVal ds As DataSet) As DataSet

    '    '元のデータ
    '    Dim dt As DataTable = ds.Tables("LMC789IN")
    '    Dim outDt As DataTable = ds.Tables("LMC789OUT")
    '    Dim max As Integer = dt.Rows.Count - 1

    '    '別インスタンス
    '    Dim setDs As DataSet = ds.Copy()
    '    Dim inTbl As DataTable = setDs.Tables("LMC789IN")
    '    Dim setDt As DataTable = setDs.Tables("LMC789OUT")
    '    Dim count As Integer = 0

    '    Dim rtnResult As Boolean = True

    '    For i As Integer = 0 To max

    '        '値のクリア
    '        setDs.Clear()

    '        '条件の設定
    '        inTbl.ImportRow(dt.Rows(i))

    '        'データの抽出
    '        rtnResult = Me.ServerChkJudge(setDs, "SelectMeitetuTag")

    '        count = MyBase.GetResultCount()

    '        '0件の場合は次のデータへ
    '        If count = 0 Then
    '            Continue For
    '        End If

    '        '値設定
    '        For j As Integer = 0 To count - 1
    '            outDt.ImportRow(setDt.Rows(j))
    '        Next

    '    Next

    '    Return ds

    'End Function

    ''' <summary>
    ''' 出荷データ（大）更新（名鉄送り状作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuTag(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateMeitetuTag", ds)

    End Function

#End Region
#End Region

#Region "チェック"

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    Private Function ToValidateData(ByVal ds As DataSet) As DataSet


        Return ds

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function getMeitetuTag(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = "LMC789IN"
        Dim tableNmOut As String = "LMC789OUT"
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
        Dim workDtOut As DataTable = dtOut.Clone
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
            workDtOut.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            setDs = Me.SelectMPrt_Tag(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectMeitetuTag(setDs)

            '郵便番号のハイフン除去
            setDs.Tables("LMC789OUT").Rows(0).Item("NIUKENIN_ZIP") = setDs.Tables("LMC789OUT").Rows(0).Item("NIUKENIN_ZIP").ToString.Replace("-", "")

            '送り状でQRコードが正しく取得できなかったものは荷札を出力しない
            If String.IsNullOrEmpty(setDs.Tables("LMC789OUT").Rows(0).Item("AUTO_DENP_NO").ToString) = True _
                OrElse String.IsNullOrEmpty(setDs.Tables("LMC789OUT").Rows(0).Item("KOSU").ToString) _
                OrElse setDs.Tables("LMC789OUT").Rows(0).Item("KOSU").ToString = "0" _
                OrElse String.IsNullOrEmpty(setDs.Tables("LMC789OUT").Rows(0).Item("JYURYO").ToString) = True _
                OrElse setDs.Tables("LMC789OUT").Rows(0).Item("JYURYO").ToString = "0" _
                OrElse String.IsNullOrEmpty(setDs.Tables("LMC789OUT").Rows(0).Item("NIUKENIN_ZIP").ToString) = True Then

                Continue For
            Else
                '帳票パターンを格納する
                setDs.Tables("LMC789OUT").Rows(0).Item("RPT_ID") = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID")
            End If

            '問い合わせ番号が最大になった場合は限界になったことをシステム部に通知して、処理を行わない
            If setDs.Tables("LMC789OUT").Rows(0).Item("AUTO_DENP_NO").ToString >= "3250969993" Then
                MyBase.SetMessageStore("00", "E815", , dt.Rows(0).Item("OUTKA_NO_L").ToString)
                setDs.Tables("LMC789OUT").Rows(0).Item("RPT_ID") = String.Empty
            End If

            '名鉄の着店等を取得
            setDs = Me.SelectMeitetuZip(setDs)

            '区分値には発店別の郵便番号パターンを格納
            Dim ZipPattern As String = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM1").ToString
            '群馬BP用。荷主詳細Mに登録すれば、登録した郵便番号パターンを使用する
            Dim setZip As String = setDs.Tables("LMC789OUT").Rows(0).Item("ZIP_PATTERN").ToString

            '群馬BP以外は基本的にはNullが入っている
            If String.IsNullOrEmpty(setZip) = False Then
                ZipPattern = setZip
                'もし郵便番号パターンが取れないときの処理。区分名３には１が格納されている
            ElseIf String.IsNullOrEmpty(ZipPattern) = True Then
                ZipPattern = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM3").ToString
            End If

            '取得したパターンで仕訳番号が空の場合は、パターン１を使用する
            If String.IsNullOrEmpty(setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern)).ToString) = True Then
                ZipPattern = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM3").ToString
            End If
            setDs.Tables("LMC789OUT").Rows(0).Item("CHAKU_CD") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTI_CD_", ZipPattern))
            setDs.Tables("LMC789OUT").Rows(0).Item("CYAKU_NM") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTEN_NM_", ZipPattern))
            setDs.Tables("LMC789OUT").Rows(0).Item("SIWAKE_NO") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern))

            'For j As Integer = 1 To Convert.ToInt32(setDs.Tables(tableNmOut).Rows(0).Item("PAGES")) - 1

            '    setDs.Tables(tableNmOut).ImportRow(setDs.Tables(tableNmOut).Rows(0))

            'Next

            '荷札を編集する処理
            workDtOut.ImportRow(setDs.Tables("LMC789OUT").Rows(0))
            setDs.Tables("LMC789OUT").Clear()

            For k As Integer = 1 To Convert.ToInt32(workDtOut.Rows(0).Item("KOSU"))

                Dim Kosu As Integer = Convert.ToInt32(workDtOut.Rows(0).Item("KOSU")) + 1

                '100個口以上の表示するときに使用する項目。基本的に空欄
                workDtOut.Rows(0).Item("PAGES_2") = ""
                If Kosu >= 100 Then
                    Select Case k
                        Case 1
                            workDtOut.Rows(0).Item("PAGES") = "B" + workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString + "B"
                        Case Else
                            workDtOut.Rows(0).Item("PAGES") = ""
                            Dim OkuriNo As String = workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString
                            workDtOut.Rows(0).Item("PAGES_2") = Mid(OkuriNo, 1, 2) & "-" & Mid(OkuriNo, 3, 4) & "-" & Mid(OkuriNo, 7, 4)
                    End Select
                Else
                    '100個以下の時の処理
                    workDtOut.Rows(0).Item("PAGES") = "B" + workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString + Right(String.Concat("0", k), 2) + "B"
                End If

                workDtOut.Rows(0).Item("ROW_NO") = k
                setDs.Tables("LMC789OUT").ImportRow(workDtOut.Rows(0))
            Next

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

                '        '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                '        'キー更新
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
            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMC789OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC789OUT"), _
                                  ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="prtId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal prtId As String, ByVal ds As DataSet) As DataSet

        Select Case prtId
            Case ""
            Case Else

        End Select

        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt_Tag(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt_Tag", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function


    ''' <summary>
    '''　出力対象帳票パターン運送取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt_TagUnso(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt_TagUnso", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    '2015.10.01 tsunehira add
    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMeitetuTag(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectMeitetuTag", ds)

    End Function

    '2015.11.12 tsunehira add
    ''' <summary>
    ''' 郵便番号取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMeitetuZip(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectMeitetuZip", ds)

    End Function


#If True Then ' 名鉄対応(2499) 2016.2.3 added
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>getMeitetuTagから印刷処理のみ分離</remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNameInData As String = "LMC789OUT"
        Dim tableNmRpt As String = "M_RPT"

        Dim dtOut As DataTable = ds.Tables(tableNameInData)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Clone()


        Dim inTbl As DataTable = setDs.Tables(tableNameInData)
        Dim max As Integer = ds.Tables(tableNameInData).Rows.Count - 1
        Dim setDtOut As DataTable = Nothing
        Dim setDtRpt As DataTable = Nothing

        Dim cutStr As String() = {}

        'ADD 2017/07/20 出荷編集画面の枚数指定時対応 Start
        Dim printCNT As String = String.Empty
        Dim printCNTFrom As String = String.Empty
        Dim printCNTTo As String = String.Empty
        If ds.Tables("LMC794IN") Is Nothing = False Then
            printCNT = ds.Tables("LMC794IN").Rows(0).Item("PRT_NB").ToString.Trim
            printCNTFrom = ds.Tables("LMC794IN").Rows(0).Item("PRT_NB_FROM").ToString.Trim
            printCNTTo = ds.Tables("LMC794IN").Rows(0).Item("PRT_NB_TO").ToString.Trim
        End If

        Dim wkCNTFrom As String = String.Empty
        Dim wkCNTTo As String = String.Empty
        'ADD 2017/07/20 出荷編集画面の枚数指定時対応 End

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone
        Dim workDtOut As DataTable = inTbl.Clone

        'IN条件0件チェック
        If ds.Tables(tableNameInData).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        Dim outTable As DataTable = ds.Tables(tableNameInData).Copy()
        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()
            workDtOut.Clear()

            '条件の設定
            setDs.Tables(tableNameInData).ImportRow(outTable.Rows(i))

            setDs = Me.SelectMPrt_Tag(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '送り状でQRコードが正しく取得できなかったものは荷札を出力しない
            If String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("AUTO_DENP_NO").ToString) = True _
                OrElse String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("KOSU").ToString) _
                OrElse setDs.Tables(tableNameInData).Rows(0).Item("KOSU").ToString = "0" _
                OrElse String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("JYURYO").ToString) = True _
                OrElse setDs.Tables(tableNameInData).Rows(0).Item("JYURYO").ToString = "0" _
                OrElse String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ZIP").ToString) = True Then

                Continue For
            Else
                '帳票パターンを格納する
                setDs.Tables(tableNameInData).Rows(0).Item("RPT_ID") = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID")
            End If


            '名鉄の着店等を取得
            setDs = Me.SelectMeitetuZip(setDs)

            '20160701 要番2583 tsunehira add start
            '名鉄郵便番号マスタに郵便番号がなかった場合の処理
            If setDs.Tables("MEITETSU_ZIP").Rows.Count = 0 Then
                MyBase.SetMessageStore("00", "E428", New String() {"郵便番号が不正の", "送状を出力", "届先マスタもしくはEDIデータの郵便番号を修正してください"}, setDs.Tables("LMC789OUT").Rows(0).Item("OUTKA_NO_L").ToString)
                Continue For
            End If
            '20160701 tsunehira add end

            '区分値には発店別の郵便番号パターンを格納
            Dim ZipPattern As String = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM1").ToString
            '群馬BP用。荷主詳細Mに登録すれば、登録した郵便番号パターンを使用する
            Dim setZip As String = setDs.Tables(tableNameInData).Rows(0).Item("ZIP_PATTERN").ToString

            '群馬BP以外は基本的にはNullが入っている
            If String.IsNullOrEmpty(setZip) = False Then
                ZipPattern = setZip
                'もし郵便番号パターンが取れないときの処理。区分名３には１が格納されている
            ElseIf String.IsNullOrEmpty(ZipPattern) = True Then
                ZipPattern = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM3").ToString
            End If

            '取得したパターンで仕訳番号が空の場合は、パターン１を使用する
            If String.IsNullOrEmpty(setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern)).ToString) = True Then
                ZipPattern = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM3").ToString
            End If
            setDs.Tables(tableNameInData).Rows(0).Item("CHAKU_CD") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTI_CD_", ZipPattern))
            setDs.Tables(tableNameInData).Rows(0).Item("CYAKU_NM") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTEN_NM_", ZipPattern))
            setDs.Tables(tableNameInData).Rows(0).Item("SIWAKE_NO") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern))

#If True Then ' 名鉄対応(2499) 20160426 added inoue

            '荷送人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_MEI1").ToString, 34, 2)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_MEI1") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_MEI2") = cutStr(1).Trim

            '荷送人住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD1").ToString, 34, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '荷送人住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD2").ToString, 34, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '荷送人住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD3").ToString, 34, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD3") = cutStr(0).Trim


            '荷受人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_MEI1").ToString, 34, 2)
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_MEI1") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_MEI2") = cutStr(1).Trim

            '荷受人住所の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD1").ToString, 34, 3)
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '記事1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_1").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_1") = cutStr(0).Trim

            '記事2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_2").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_2") = cutStr(0).Trim

            '記事3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_3").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_3") = cutStr(0).Trim


            '記事4の再設定
            If (String.IsNullOrWhiteSpace(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4").ToString()) = True) Then
                If String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("SHIP_NM_L").ToString) = False Then
                    setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables(tableNameInData).Rows(0).Item("SHIP_NM_L").ToString, "様扱い")
                ElseIf String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("DENPYO_NM").ToString) = False Then
                    setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables(tableNameInData).Rows(0).Item("DENPYO_NM").ToString, "様扱い")
                Else
                    setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables(tableNameInData).Rows(0).Item("CUST_NM_L").ToString, "様扱い")
                End If
            End If

            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = cutStr(0).Trim

            '記事5、6の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_5").ToString, 40, 2)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_5") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_6") = cutStr(1).Trim

#End If

#If True Then    ' 20160509 名鉄対応(2499) 曜日識別データ設定 added inoue
            '日付処理
            Dim arrPlanDate As String = setDs.Tables(tableNameInData).Rows(0).Item("ARR_PLAN_DATE").ToString

            If String.IsNullOrEmpty(arrPlanDate) = False Then

                'スラッシュ追加編集
                arrPlanDate = Mid(arrPlanDate, 1, 4) & "/" & Mid(arrPlanDate, 5, 2) & "/" & Mid(arrPlanDate, 7, 2)

                '配送日から曜日を取得。送り状の日付判断に使用
                setDs.Tables(tableNameInData).Rows(0).Item("DAY") = Weekday(CDate(arrPlanDate))
            Else
                setDs.Tables(tableNameInData).Rows(0).Item("DAY") = String.Empty
            End If
#End If


            '荷札を編集する処理
            workDtOut.ImportRow(setDs.Tables(tableNameInData).Rows(0))
            setDs.Tables(tableNameInData).Clear()

            '出荷編集画面より設定のため修正 ADD 2017/07/20 Start
            If printCNT.ToString.Trim = "" Then
                wkCNTFrom = "1"
                wkCNTTo = workDtOut.Rows(0).Item("KOSU").ToString.Trim
            Else
                wkCNTFrom = printCNTFrom.ToString.Trim
                wkCNTTo = printCNTTo.ToString.Trim
            End If
            'UPD 2017/07/20 
            'For k As Integer = 1 To Convert.ToInt32(workDtOut.Rows(0).Item("KOSU"))
            For k As Integer = Convert.ToInt32(wkCNTFrom.ToString) To Convert.ToInt32(wkCNTTo.ToString)

                Dim Kosu As Integer = Convert.ToInt32(workDtOut.Rows(0).Item("KOSU")) + 1

                '100個口以上の表示するときに使用する項目。基本的に空欄
                workDtOut.Rows(0).Item("PAGES_2") = ""
                If Kosu >= 100 Then
                    Select Case k
                        Case Convert.ToInt32(wkCNTFrom.ToString)
                            workDtOut.Rows(0).Item("PAGES") = "B" + workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString + "B"
                        Case Else
                            workDtOut.Rows(0).Item("PAGES") = ""
                            Dim OkuriNo As String = workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString
                            workDtOut.Rows(0).Item("PAGES_2") = Mid(OkuriNo, 1, 2) & "-" & Mid(OkuriNo, 3, 4) & "-" & Mid(OkuriNo, 7, 4)
                    End Select
                Else
                    '100個以下の時の処理
                    workDtOut.Rows(0).Item("PAGES") = "B" + workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString + Right(String.Concat("0", k), 2) + "B"
                End If

                workDtOut.Rows(0).Item("ROW_NO") = k
                setDs.Tables(tableNameInData).ImportRow(workDtOut.Rows(0))
            Next

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNameInData)

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

                '        '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                '        'キー更新
                keyBrCd = sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(l).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(l).Item("RPT_ID").ToString()

            End If

        Next

        Dim rdTmp As New DataSet()
        rdTmp.Merge(New RdPrevInfoDS)

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
            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables(tableNameInData), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC789OUT"), _
                                  rdTmp.Tables(LMConst.RD))

        Next

        ds.Merge(rdTmp)

        Return ds

    End Function
#End If

    ''' <summary>
    ''' 印刷処理(運送より)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>getMeitetuTagから印刷処理のみ分離</remarks>
    Private Function DoPrintUnso(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNameInData As String = "LMC789OUT"
        Dim tableNmRpt As String = "M_RPT"

        Dim dtOut As DataTable = ds.Tables(tableNameInData)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Clone()


        Dim inTbl As DataTable = setDs.Tables(tableNameInData)
        Dim max As Integer = ds.Tables(tableNameInData).Rows.Count - 1
        Dim setDtOut As DataTable = Nothing
        Dim setDtRpt As DataTable = Nothing

        Dim cutStr As String() = {}

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone
        Dim workDtOut As DataTable = inTbl.Clone

        'IN条件0件チェック
        If ds.Tables(tableNameInData).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        Dim outTable As DataTable = ds.Tables(tableNameInData).Copy()
        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()
            workDtOut.Clear()

            '条件の設定
            setDs.Tables(tableNameInData).ImportRow(outTable.Rows(i))

            setDs = Me.SelectMPrt_TagUnso(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '送り状でQRコードが正しく取得できなかったものは荷札を出力しない
            If String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("AUTO_DENP_NO").ToString) = True _
                OrElse String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("KOSU").ToString) _
                OrElse setDs.Tables(tableNameInData).Rows(0).Item("KOSU").ToString = "0" _
                OrElse String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("JYURYO").ToString) = True _
                OrElse setDs.Tables(tableNameInData).Rows(0).Item("JYURYO").ToString = "0" _
                OrElse String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ZIP").ToString) = True Then

                Continue For
            Else
                '帳票パターンを格納する
                setDs.Tables(tableNameInData).Rows(0).Item("RPT_ID") = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID")
            End If


            '名鉄の着店等を取得
            setDs = Me.SelectMeitetuZip(setDs)

            '20160701 要番2583 tsunehira add start
            '名鉄郵便番号マスタに郵便番号がなかった場合の処理
            If setDs.Tables("MEITETSU_ZIP").Rows.Count = 0 Then
                MyBase.SetMessageStore("00", "E428", New String() {"郵便番号が不正の", "荷札を出力", "届先マスタの郵便番号を修正してください"}, setDs.Tables("LMC789OUT").Rows(0).Item("OUTKA_NO_L").ToString)
                Continue For
            End If
            '20160701 tsunehira add end

            '区分値には発店別の郵便番号パターンを格納
            Dim ZipPattern As String = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM1").ToString
            '群馬BP用。荷主詳細Mに登録すれば、登録した郵便番号パターンを使用する
            Dim setZip As String = setDs.Tables(tableNameInData).Rows(0).Item("ZIP_PATTERN").ToString

            '群馬BP以外は基本的にはNullが入っている
            If String.IsNullOrEmpty(setZip) = False Then
                ZipPattern = setZip
                'もし郵便番号パターンが取れないときの処理。区分名３には１が格納されている
            ElseIf String.IsNullOrEmpty(ZipPattern) = True Then
                ZipPattern = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM3").ToString
            End If

            '取得したパターンで仕訳番号が空の場合は、パターン１を使用する
            If String.IsNullOrEmpty(setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern)).ToString) = True Then
                ZipPattern = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM3").ToString
            End If
            setDs.Tables(tableNameInData).Rows(0).Item("CHAKU_CD") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTI_CD_", ZipPattern))
            setDs.Tables(tableNameInData).Rows(0).Item("CYAKU_NM") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTEN_NM_", ZipPattern))
            setDs.Tables(tableNameInData).Rows(0).Item("SIWAKE_NO") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern))

            '荷送人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_MEI1").ToString, 34, 2)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_MEI1") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_MEI2") = cutStr(1).Trim

            '荷送人住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD1").ToString, 34, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '荷送人住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD2").ToString, 34, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '荷送人住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD3").ToString, 34, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("NIOKURININ_ADD3") = cutStr(0).Trim


            '荷受人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_MEI1").ToString, 34, 2)
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_MEI1") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_MEI2") = cutStr(1).Trim

            '荷受人住所の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD1").ToString, 34, 3)
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '記事1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_1").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_1") = cutStr(0).Trim

            '記事2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_2").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_2") = cutStr(0).Trim

            '記事3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_3").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_3") = cutStr(0).Trim


            '記事4の再設定
            If (String.IsNullOrWhiteSpace(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4").ToString()) = True) Then
                If String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("SHIP_NM_L").ToString) = False Then
                    setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables(tableNameInData).Rows(0).Item("SHIP_NM_L").ToString, "様扱い")
                ElseIf String.IsNullOrEmpty(setDs.Tables(tableNameInData).Rows(0).Item("DENPYO_NM").ToString) = False Then
                    setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables(tableNameInData).Rows(0).Item("DENPYO_NM").ToString, "様扱い")
                Else
                    setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables(tableNameInData).Rows(0).Item("CUST_NM_L").ToString, "様扱い")
                End If
            End If

            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4").ToString, 40, 1)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_4") = cutStr(0).Trim

            '記事5、6の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables(tableNameInData).Rows(0).Item("KIJI_5").ToString, 40, 2)
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_5") = cutStr(0).Trim
            setDs.Tables(tableNameInData).Rows(0).Item("KIJI_6") = cutStr(1).Trim

            '日付処理
            Dim arrPlanDate As String = setDs.Tables(tableNameInData).Rows(0).Item("ARR_PLAN_DATE").ToString

            If String.IsNullOrEmpty(arrPlanDate) = False Then

                'スラッシュ追加編集
                arrPlanDate = Mid(arrPlanDate, 1, 4) & "/" & Mid(arrPlanDate, 5, 2) & "/" & Mid(arrPlanDate, 7, 2)

                '配送日から曜日を取得。送り状の日付判断に使用
                setDs.Tables(tableNameInData).Rows(0).Item("DAY") = Weekday(CDate(arrPlanDate))
            Else
                setDs.Tables(tableNameInData).Rows(0).Item("DAY") = String.Empty
            End If

            '荷札を編集する処理
            workDtOut.ImportRow(setDs.Tables(tableNameInData).Rows(0))
            setDs.Tables(tableNameInData).Clear()

            For k As Integer = 1 To Convert.ToInt32(workDtOut.Rows(0).Item("KOSU"))

                Dim Kosu As Integer = Convert.ToInt32(workDtOut.Rows(0).Item("KOSU")) + 1

                '100個口以上の表示するときに使用する項目。基本的に空欄
                workDtOut.Rows(0).Item("PAGES_2") = ""
                If Kosu >= 100 Then
                    Select Case k
                        Case 1
                            workDtOut.Rows(0).Item("PAGES") = "B" + workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString + "B"
                        Case Else
                            workDtOut.Rows(0).Item("PAGES") = ""
                            Dim OkuriNo As String = workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString
                            workDtOut.Rows(0).Item("PAGES_2") = Mid(OkuriNo, 1, 2) & "-" & Mid(OkuriNo, 3, 4) & "-" & Mid(OkuriNo, 7, 4)
                    End Select
                Else
                    '100個以下の時の処理
                    workDtOut.Rows(0).Item("PAGES") = "B" + workDtOut.Rows(0).Item("AUTO_DENP_NO").ToString + Right(String.Concat("0", k), 2) + "B"
                End If

                workDtOut.Rows(0).Item("ROW_NO") = k
                setDs.Tables(tableNameInData).ImportRow(workDtOut.Rows(0))
            Next

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNameInData)

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

                '        '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                '        'キー更新
                keyBrCd = sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(l).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(l).Item("RPT_ID").ToString()

            End If

        Next

        Dim rdTmp As New DataSet()
        rdTmp.Merge(New RdPrevInfoDS)

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
            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables(tableNameInData), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC789OUT"), _
                                  rdTmp.Tables(LMConst.RD))

        Next

        ds.Merge(rdTmp)

        Return ds

    End Function



    ''' <summary>
    ''' 文字分割
    ''' </summary>
    ''' <param name="inStr">分割対象文字</param>
    ''' <param name="inByte">分割単位バイト数</param>
    ''' <param name="inCnt">分割する数</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Function StringCut(ByVal inStr As String, ByVal inByte As Integer, ByVal inCnt As Integer) As String()

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
#End Region

End Class
