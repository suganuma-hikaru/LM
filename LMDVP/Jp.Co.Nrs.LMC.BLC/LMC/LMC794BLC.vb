' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC794    : 名鉄・送り状
'  作  成  者       :  tsunehira
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.IO

''' <summary>
''' LMC794BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC794BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC794DAC = New LMC794DAC()

    ''' <summary>
    ''' ロック用
    ''' </summary>
    ''' <remarks></remarks>
    Private _LockObject As Object = New Object()


    ''' <summary>
    ''' こぐま提供メソッドSUCCESSコード
    ''' </summary>
    ''' <remarks></remarks>
    Private Const KOGUMA_SUCCESS_CODE As String = "00"


    Private Const IN_TABLE_NAME As String = "LMC794IN"
    Private Const OUT_TABLE_NAME As String = "LMC794OUT"
    Private Const UPDATE_TABLE_NAME As String = "LMC794IN_UPDATE_UNSO_L"

    Private Const RPT_TABLE_NAME As String = "M_RPT"
#End Region

#Region "Method"

#Region "送り状データ取得処理"

    ''' <summary>
    ''' 送り状データ取得処理メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのMeitetuCsvメソッド呼出</remarks>
    Private Function MeitetuLabel(ByVal ds As DataSet) As DataSet

#If False Then
        '印刷対象データの取得
        Dim rtnDs As DataSet = Me.getMeitetuLabel(ds)
#Else
        Dim rtnDs As DataSet = Me.SelectMeitetuLabel(ds)
#End If
        '出力対象データが0件の場合は終了
        If rtnDs.Tables(OUT_TABLE_NAME).Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E070")
            Return ds
        End If

        '妥当性チェック
        rtnDs = Me.ToValidateData(rtnDs)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 送り状運送データ取得処理メイン ADD 2017/03/01
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのMeitetuCsvメソッド呼出</remarks>
    Private Function MeitetuLabelUnso(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Me.SelectMeitetuLabelUnso(ds)

        '出力対象データが0件の場合は終了
        If rtnDs.Tables(OUT_TABLE_NAME).Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.SetMessage("E070")
            Return ds
        End If

        '妥当性チェック
        rtnDs = Me.ToValidateData(rtnDs)

        Return rtnDs

    End Function

    Private Function MeitetuLabelWithGrouping(ByVal ds As DataSet) As DataSet

        Dim rtnDs As DataSet = Me.MeitetuLabel(ds)

        If (MyBase.IsMessageStoreExist = False) Then
            'まとめ
            rtnDs = Me.GroupingPrintDataSet(rtnDs)
        End If

        Return rtnDs

    End Function


    Private Function ToValidateData(ByVal ds As DataSet) As DataSet

        For i As Int32 = 0 To ds.Tables(OUT_TABLE_NAME).Rows.Count - 1


            'お問合せ番号
            If String.IsNullOrEmpty(ds.Tables("LMC794OUT").Rows(0).Item("AUTO_DENP_NO").ToString) = True Then
                MyBase.SetMessageStore("00", "E454", New String() {"お問い合わせ番号が空欄", "出力", "お問い合わせ番号の取得を行ってください。"}, ds.Tables("LMC794OUT").Rows(0).Item("OUTKA_NO_L").ToString)
            End If

            '個数
            If String.IsNullOrEmpty(ds.Tables("LMC794OUT").Rows(0).Item("KOSU").ToString) = True OrElse ds.Tables("LMC794OUT").Rows(0).Item("KOSU").ToString = "0" Then
                MyBase.SetMessageStore("00", "E454", New String() {"個数が空欄もしくは0", "出力", "1以上999以下の正しい値を入力をしてください。"}, ds.Tables("LMC794OUT").Rows(0).Item("OUTKA_NO_L").ToString)
            End If

            '重量
            If String.IsNullOrEmpty(ds.Tables("LMC794OUT").Rows(0).Item("JYURYO").ToString) = True OrElse ds.Tables("LMC794OUT").Rows(0).Item("JYURYO").ToString = "0" Then
                MyBase.SetMessageStore("00", "E454", New String() {"重量が空欄もしくは0", "出力", "1以上99999以下の正しい値を入力をしてください。"}, ds.Tables("LMC794OUT").Rows(0).Item("OUTKA_NO_L").ToString)
            End If

            ' 郵便番号
            If String.IsNullOrEmpty(ds.Tables("LMC794OUT").Rows(i).Item("NIUKENIN_ZIP").ToString) = True Then
                MyBase.SetMessageStore("00", "E454", New String() {"郵便番号が空欄もしくは不正", "出力", "正しく入力をしてください。"}, ds.Tables(OUT_TABLE_NAME).Rows(i).Item("OUTKA_NO_L").ToString)
            Else
                'Zipパターン取得のための郵便番号のハイフン除去
                ds.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ZIP") = ds.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ZIP").ToString.Replace("-", "")
            End If
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="value1"></param>
    ''' <param name="value2"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MinAutoDenpNo(ByVal value1 As String, ByVal value2 As String) As String


        If (String.IsNullOrEmpty(value1) = False AndAlso String.IsNullOrEmpty(value2) = True) Then
            Return value1
        ElseIf (String.IsNullOrEmpty(value1) = True AndAlso String.IsNullOrEmpty(value2) = False) Then
            Return value2
        ElseIf (String.IsNullOrEmpty(value1) = False AndAlso String.IsNullOrEmpty(value2) = True) Then
            Return value1
        End If

        Dim val1 As UInt64 = 0
        Dim val2 As UInt64 = 0

        If (UInt64.TryParse(value2, val2) = False) Then
            Return value1
        End If

        If (UInt64.TryParse(value1, val1) = False) Then
            Return value2
        End If

        If (val2 > val1) Then
            Return value1
        Else
            Return value2
        End If

    End Function

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet


        Dim dtOut As DataTable = ds.Tables(OUT_TABLE_NAME)
        Dim dtRpt As DataTable = ds.Tables(RPT_TABLE_NAME)

        '別インスタンス
        Dim setDs As DataSet = ds.Clone()

        Dim setDtOut As DataTable = Nothing
        Dim setDtRpt As DataTable = Nothing

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone

        Dim cutStr As String() = {}

        'IN条件0件チェック
        If ds.Tables(OUT_TABLE_NAME).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        Dim outTable As DataTable = ds.Tables(OUT_TABLE_NAME).Copy()
        For i As Integer = 0 To outTable.Rows.Count - 1

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDs.Tables(OUT_TABLE_NAME).ImportRow(outTable.Rows(i))

            '帳票パターンを取得
            setDs = Me.SelectMPrt_Label(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If


            '帳票パターンを格納する
            setDs.Tables("LMC794OUT").Rows(0).Item("RPT_ID") = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID")

            '名鉄運輸サイドの着店等を取得
            setDs = Me.SelectMeitetuZip(setDs)

            '20160701 要番2583 tsunehira add start
            '名鉄郵便番号マスタに郵便番号がなかった場合の処理
            If setDs.Tables("MEITETSU_ZIP").Rows.Count = 0 Then
                MyBase.SetMessageStore("00", "E428", New String() {"郵便番号が不正の", "送状を出力", "届先マスタもしくはEDIデータの郵便番号を修正してください"}, setDs.Tables("LMC794OUT").Rows(0).Item("OUTKA_NO_L").ToString)
                Continue For
            End If
            '20160701 tsunehira add end

            '区分値には発店別の郵便番号パターンを格納
            Dim ZipPattern As String = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM1").ToString
            '群馬BP用。荷主詳細Mに登録すれば、登録した郵便番号パターンを使用する
            Dim setZip As String = setDs.Tables("LMC794OUT").Rows(0).Item("ZIP_PATTERN").ToString

            '群馬BP以外は基本的にはNullが入ることとなる
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
            setDs.Tables("LMC794OUT").Rows(0).Item("CHAKU_CD") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTI_CD_", ZipPattern))
            setDs.Tables("LMC794OUT").Rows(0).Item("CYAKU_NM") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTEN_NM_", ZipPattern))
            setDs.Tables("LMC794OUT").Rows(0).Item("SIWAKE_NO") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern))

            'QRコード生成
            Me.getKogumaQRCode(setDs)

            'QRコード生成に失敗したら送り状の出力を行わない
            If String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("KOGUMA_QR").ToString) = True Then
                Continue For
            End If

            '日付処理
            Dim Toutyaku As String = setDs.Tables("LMC794OUT").Rows(0).Item("ARR_PLAN_DATE").ToString

            If String.IsNullOrEmpty(Toutyaku) = False Then

                'スラッシュ追加編集
                Dim ConvertDate As String = Mid(Toutyaku, 1, 4) & "/" & Mid(Toutyaku, 5, 2) & "/" & Mid(Toutyaku, 7, 2)
                '配送日から曜日を取得。送り状の日付判断に使用
                setDs.Tables("LMC794OUT").Rows(0).Item("DAY") = Weekday(CDate(ConvertDate))

                '月日追加編集
                Dim Month As String = String.Concat(Mid(Toutyaku, 5, 2), "月")
                Dim Day As String = String.Concat(Mid(Toutyaku, 7, 2), "日")

                setDs.Tables("LMC794OUT").Rows(0).Item("ARR_PLAN_DATE") = String.Concat(Month, Day)

            End If

#If True Then ' 名鉄対応(2499) 20160426 added inoue

            '荷送人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_MEI1").ToString, 34, 2)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_MEI1") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_MEI2") = cutStr(1).Trim

            '荷送人住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD1").ToString, 34, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '荷送人住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD2").ToString, 34, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '荷送人住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD3").ToString, 34, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD3") = cutStr(0).Trim

            '荷受人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_MEI1").ToString, 34, 2)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_MEI1") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_MEI2") = cutStr(1).Trim

            '荷受人住所の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD1").ToString, 34, 3)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '記事1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_1").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_1") = cutStr(0).Trim

            '記事2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_2").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_2") = cutStr(0).Trim

            '記事3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_3").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_3") = cutStr(0).Trim


            '記事4の再設定
            If (String.IsNullOrWhiteSpace(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4").ToString()) = True) Then
                If String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("SHIP_NM_L").ToString) = False Then
                    setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables("LMC794OUT").Rows(0).Item("SHIP_NM_L").ToString, "様扱い")
                ElseIf String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("DENPYO_NM").ToString) = False Then
                    setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables("LMC794OUT").Rows(0).Item("DENPYO_NM").ToString, "様扱い")
                Else
                    setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables("LMC794OUT").Rows(0).Item("CUST_NM_L").ToString, "様扱い")
                End If
            End If

            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = cutStr(0).Trim

            '記事5、6の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_5").ToString, 40, 2)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_5") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_6") = cutStr(1).Trim

#End If
            '検索結果を詰め替え
            setDtOut = setDs.Tables(OUT_TABLE_NAME)
            setDtRpt = setDs.Tables(RPT_TABLE_NAME)

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

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                'キー更新
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMC794OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC794OUT"), _
                                  rdTmp.Tables(LMConst.RD))
        Next

        ds.Merge(rdTmp)

        Return ds


    End Function


    ''' <summary>
    ''' 印刷(運送)　　ADD 2017/03/01
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function DoPrintUnso(ByVal ds As DataSet) As DataSet


        Dim dtOut As DataTable = ds.Tables(OUT_TABLE_NAME)
        Dim dtRpt As DataTable = ds.Tables(RPT_TABLE_NAME)

        '別インスタンス
        Dim setDs As DataSet = ds.Clone()

        Dim setDtOut As DataTable = Nothing
        Dim setDtRpt As DataTable = Nothing

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone

        Dim cutStr As String() = {}

        'IN条件0件チェック
        If ds.Tables(OUT_TABLE_NAME).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        Dim outTable As DataTable = ds.Tables(OUT_TABLE_NAME).Copy()
        For i As Integer = 0 To outTable.Rows.Count - 1

            '値のクリア
            setDs.Clear()

            '条件の設定
            setDs.Tables(OUT_TABLE_NAME).ImportRow(outTable.Rows(i))

            '帳票パターンを取得
            setDs = Me.SelectMPrt_LabelUnso(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If


            '帳票パターンを格納する
            setDs.Tables("LMC794OUT").Rows(0).Item("RPT_ID") = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID")

            '名鉄運輸サイドの着店等を取得
            setDs = Me.SelectMeitetuZip(setDs)

            '20160701 要番2583 tsunehira add start
            '名鉄郵便番号マスタに郵便番号がなかった場合の処理
            If setDs.Tables("MEITETSU_ZIP").Rows.Count = 0 Then
                MyBase.SetMessageStore("00", "E428", New String() {"郵便番号が不正の", "送状を出力", "届先マスタの郵便番号を修正してください"}, setDs.Tables("LMC794OUT").Rows(0).Item("OUTKA_NO_L").ToString)
                Continue For
            End If
            '20160701 tsunehira add end

            '区分値には発店別の郵便番号パターンを格納
            Dim ZipPattern As String = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM1").ToString
            '群馬BP用。荷主詳細Mに登録すれば、登録した郵便番号パターンを使用する
            Dim setZip As String = setDs.Tables("LMC794OUT").Rows(0).Item("ZIP_PATTERN").ToString

            '群馬BP以外は基本的にはNullが入ることとなる
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
            setDs.Tables("LMC794OUT").Rows(0).Item("CHAKU_CD") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTI_CD_", ZipPattern))
            setDs.Tables("LMC794OUT").Rows(0).Item("CYAKU_NM") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTEN_NM_", ZipPattern))
            setDs.Tables("LMC794OUT").Rows(0).Item("SIWAKE_NO") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern))

            'QRコード生成
            Me.getKogumaQRCode(setDs)

            'QRコード生成に失敗したら送り状の出力を行わない
            If String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("KOGUMA_QR").ToString) = True Then
                Continue For
            End If

            '日付処理
            Dim Toutyaku As String = setDs.Tables("LMC794OUT").Rows(0).Item("ARR_PLAN_DATE").ToString

            If String.IsNullOrEmpty(Toutyaku) = False Then

                'スラッシュ追加編集
                Dim ConvertDate As String = Mid(Toutyaku, 1, 4) & "/" & Mid(Toutyaku, 5, 2) & "/" & Mid(Toutyaku, 7, 2)
                '配送日から曜日を取得。送り状の日付判断に使用
                setDs.Tables("LMC794OUT").Rows(0).Item("DAY") = Weekday(CDate(ConvertDate))

                '月日追加編集
                Dim Month As String = String.Concat(Mid(Toutyaku, 5, 2), "月")
                Dim Day As String = String.Concat(Mid(Toutyaku, 7, 2), "日")

                setDs.Tables("LMC794OUT").Rows(0).Item("ARR_PLAN_DATE") = String.Concat(Month, Day)

            End If

#If True Then ' 名鉄対応(2499) 20160426 added inoue

            '荷送人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_MEI1").ToString, 34, 2)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_MEI1") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_MEI2") = cutStr(1).Trim

            '荷送人住所1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD1").ToString, 34, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD1") = cutStr(0).Trim

            '荷送人住所2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD2").ToString, 34, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD2") = cutStr(0).Trim

            '荷送人住所3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD3").ToString, 34, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIOKURININ_ADD3") = cutStr(0).Trim

            '荷受人名の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_MEI1").ToString, 34, 2)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_MEI1") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_MEI2") = cutStr(1).Trim

            '荷受人住所の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD1").ToString, 34, 3)
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD1") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD2") = cutStr(1).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ADD3") = cutStr(2).Trim

            '記事1の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_1").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_1") = cutStr(0).Trim

            '記事2の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_2").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_2") = cutStr(0).Trim

            '記事3の分割処理
            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_3").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_3") = cutStr(0).Trim


            '記事4の再設定
            If (String.IsNullOrWhiteSpace(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4").ToString()) = True) Then
                If String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("SHIP_NM_L").ToString) = False Then
                    setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables("LMC794OUT").Rows(0).Item("SHIP_NM_L").ToString, "様扱い")
                ElseIf String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("DENPYO_NM").ToString) = False Then
                    setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables("LMC794OUT").Rows(0).Item("DENPYO_NM").ToString, "様扱い")
                Else
                    setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = String.Concat(setDs.Tables("LMC794OUT").Rows(0).Item("CUST_NM_L").ToString, "様扱い")
                End If
            End If

            ReDim cutStr(0)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4").ToString, 40, 1)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_4") = cutStr(0).Trim

            '記事5、6の分割処理
            ReDim cutStr(1)
            cutStr = Me.StringCut(setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_5").ToString, 40, 2)
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_5") = cutStr(0).Trim
            setDs.Tables("LMC794OUT").Rows(0).Item("KIJI_6") = cutStr(1).Trim

#End If
            '検索結果を詰め替え
            setDtOut = setDs.Tables(OUT_TABLE_NAME)
            setDtRpt = setDs.Tables(RPT_TABLE_NAME)

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

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                'キー更新
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMC794OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC794OUT"), _
                                  rdTmp.Tables(LMConst.RD))
        Next

        ds.Merge(rdTmp)

        Return ds


    End Function

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GroupingPrintDataSet(ByVal ds As DataSet) As DataSet

        Dim response As DataSet = ds.Copy()
        response.Tables(OUT_TABLE_NAME).Clear()
        Dim groupedTable As DataTable = response.Tables(OUT_TABLE_NAME)

        Dim outTable As DataTable = ds.Tables(OUT_TABLE_NAME)


        Dim groupingMasterRow As DataRow = Nothing
        Dim groupingCount As Integer = 0

        Dim KOSU As Double = 0
        Dim JYURYO As Double = 0

        'キーブレイク用
        Dim NewKey As String = String.Empty
        Dim OldKey As String = String.Empty


        Dim autoDenpNo As String = String.Empty
        Dim groupingList As New Dictionary(Of String, LMC794DS.LMC794OUTDataTable)()
        Dim groupingRowList As New LMC794DS.LMC794OUTDataTable()


        'ソート
        Dim qry As IEnumerable(Of DataRow) = From row In outTable.Select.AsQueryable() _
                                             Select row _
                                             Order By row.Item("CUST_CD_L") _
                                                    , row.Item("NIUKENIN_CD") _
                                                    , row.Item("OUTKA_PLAN_DATE") _
                                                    , row.Item("ARR_PLAN_DATE") _
                                                    , row.Item("AUTO_DENP_NO")
        ' まとめ処理
        For i As Integer = 0 To qry.Count - 1


            'NEWキーに値を設定
            NewKey = String.Format("{0}{1}{2}{3}" _
                                   , qry.ElementAt(i).Item("CUST_CD_L").ToString().Trim _
                                   , qry.ElementAt(i).Item("NIUKENIN_CD").ToString().Trim _
                                   , qry.ElementAt(i).Item("OUTKA_PLAN_DATE").ToString().Trim _
                                   , qry.ElementAt(i).Item("ARR_PLAN_DATE").ToString().Trim)



            If String.IsNullOrEmpty(NewKey) = True _
                OrElse NewKey = OldKey Then

                'NewKey、OldKeyが同じ場合は纏め処理を行う
                groupingCount += 1

                ' 親子関係紐付
                groupingRowList.ImportRow(qry.ElementAt(i))

                ' 最小のお問い合わせ番号取得
                autoDenpNo = Me.MinAutoDenpNo(autoDenpNo _
                                            , qry.ElementAt(i).Item("AUTO_DENP_NO").ToString().Trim)


                'オーダー番号
                If groupingCount <= 4 Then
                    groupingMasterRow.Item("KIJI_7") = String.Concat(groupingMasterRow.Item("KIJI_7").ToString, _
                                                                                          ",", qry.ElementAt(i).Item("KIJI_7").ToString)
                ElseIf groupingCount = 5 Then
                    'まとめの5件目の場合
                    groupingMasterRow.Item("KIJI_8") = qry.ElementAt(i).Item("KIJI_7").ToString
                ElseIf 6 <= groupingCount AndAlso groupingCount <= 8 Then
                    'まとめの6～8件目までの場合
                    groupingMasterRow.Item("KIJI_8") = String.Concat(groupingMasterRow.Item("KIJI_8").ToString, _
                                                                                          ",", qry.ElementAt(i).Item("KIJI_7").ToString)
                End If

                '個数・重量                        
                KOSU = KOSU + Convert.ToDouble(qry.ElementAt(i).Item("KOSU").ToString())
                JYURYO = JYURYO + Convert.ToDouble(qry.ElementAt(i).Item("JYURYO").ToString())

                groupingMasterRow.Item("KOSU") = KOSU
                groupingMasterRow.Item("JYURYO") = JYURYO


            Else
                ' 新規グループ開始

                If (groupingMasterRow IsNot Nothing) Then
                    ' 次キーの処理へ行く前に前のデータを格納

                    groupingList.Add(autoDenpNo, groupingRowList)
                    groupingRowList = New LMC794DS.LMC794OUTDataTable()


                    groupingMasterRow.Item("AUTO_DENP_NO") = autoDenpNo
                    autoDenpNo = String.Empty

                    ' 最終結果に設定
                    groupedTable.ImportRow(groupingMasterRow)
                    groupingMasterRow = Nothing

                End If

                '新しい親行を設定
                groupingMasterRow = qry.ElementAt(i)
                groupingRowList.ImportRow(qry.ElementAt(i))

                ' お問い合わせ番号初期化
                autoDenpNo = groupingMasterRow.Item("AUTO_DENP_NO").ToString()

                ' 加算値クリア
                KOSU = Convert.ToDouble(qry.ElementAt(i).Item("KOSU").ToString())
                JYURYO = Convert.ToDouble(qry.ElementAt(i).Item("JYURYO").ToString())
                groupingCount = 1

                groupingMasterRow.Item("KOSU") = KOSU
                groupingMasterRow.Item("JYURYO") = JYURYO

            End If

            OldKey = NewKey

        Next

        If (groupingMasterRow IsNot Nothing) Then

            ' お問い合わせ更新用リスト追加
            groupingList.Add(autoDenpNo, groupingRowList)
            groupingRowList = New LMC794DS.LMC794OUTDataTable()

            ' 最小お問い合わせ番号設定
            groupingMasterRow.Item("AUTO_DENP_NO") = autoDenpNo
            autoDenpNo = String.Empty

            ' まとめ結果格納
            groupedTable.ImportRow(groupingMasterRow)
            groupingMasterRow = Nothing
        End If

        response.Tables(OUT_TABLE_NAME).Merge(groupedTable)

        ' お問い合わせ番号更新用リスト生成
        For Each data As KeyValuePair(Of String, LMC794DS.LMC794OUTDataTable) In groupingList
            For i As Int32 = 0 To data.Value.Rows.Count - 1
                Dim updateRow As DataRow = response.Tables(UPDATE_TABLE_NAME).NewRow
                updateRow.Item("AUTO_DENP_NO") = data.Key
                updateRow.Item("NRS_BR_CD") = data.Value.Rows(i).Item("NRS_BR_CD")
                updateRow.Item("OUTKA_NO_L") = data.Value.Rows(i).Item("OUTKA_NO_L")
                updateRow.Item("UNSO_NO_L") = data.Value.Rows(i).Item("UNSO_NO_L")
                updateRow.Item("SYS_UPD_DATE") = data.Value.Rows(i).Item("SYS_UPD_DATE")
                updateRow.Item("SYS_UPD_TIME") = data.Value.Rows(i).Item("SYS_UPD_TIME")
                updateRow.Item("UNSO_SYS_UPD_DATE") = data.Value.Rows(i).Item("UNSO_SYS_UPD_DATE")
                updateRow.Item("UNSO_SYS_UPD_TIME") = data.Value.Rows(i).Item("UNSO_SYS_UPD_TIME")
                response.Tables(UPDATE_TABLE_NAME).Rows.Add(updateRow)
            Next
        Next

        Return response

    End Function

    ''' <summary>
    ''' データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectMeitetuLabel(ByVal ds As DataSet) As DataSet

        '元のデータ-
        Dim dt As DataTable = ds.Tables("LMC794IN")
        Dim outDt As DataTable = ds.Tables("LMC794OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC794IN")
        Dim setDt As DataTable = setDs.Tables("LMC794OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectMeitetuLabel")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 運送データ検索 ADD 2017/03/01
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks>DACのSelectMeitetuCsvメソッド呼出</remarks>
    Private Function SelectMeitetuLabelUnso(ByVal ds As DataSet) As DataSet

        '元のデータ-
        Dim dt As DataTable = ds.Tables("LMC794IN")
        Dim outDt As DataTable = ds.Tables("LMC794OUT")
        Dim max As Integer = dt.Rows.Count - 1

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables("LMC794IN")
        Dim setDt As DataTable = setDs.Tables("LMC794OUT")
        Dim count As Integer = 0

        Dim rtnResult As Boolean = True

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            'データの抽出
            rtnResult = Me.ServerChkJudge(setDs, "SelectMeitetuLabelUnso")

            count = MyBase.GetResultCount()

            '0件の場合は次のデータへ
            If count = 0 Then
                Continue For
            End If

            '値設定
            For j As Integer = 0 To count - 1
                outDt.ImportRow(setDt.Rows(j))
            Next

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 出荷データ（大）更新（名鉄送り状作成時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateMeitetuLabel(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateMeitetuLabel", ds)

    End Function


    Private Function UpdateUnsoL(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        Return MyBase.CallDAC(Me._Dac, "UpdateUnsoL", ds)

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

#Region "名鉄QRコード"

    Private Function getKogumaQRCode(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables("LMC794OUT")

        'ライブラリ呼び出し
        Dim KogumaQR As New CreateKoguma10QRString.CreateKoguma10QRString

        '文字列格納用
        Dim KogumaQRerr As String

        'お問合せ番号
        If String.IsNullOrEmpty(dt.Rows(0).Item("AUTO_DENP_NO").ToString) = True Then
            MyBase.SetMessageStore("00", "E454", New String() {"お問い合わせ番号が空欄", "出力", "お問い合わせ番号の取得を行ってください。"}, dt.Rows(0).Item("OUTKA_NO_L").ToString)
        End If
        Dim okurijoNo As String = dt.Rows(0).Item("AUTO_DENP_NO").ToString.Replace("-", "")



#If False Then ' 20160322 deleted inoue 保存時にS_NUMBERへの書き込みによって事前に確認されているので削除
        '問い合わせ番号が最大になった場合は限界になったことをシステム部に通知して、処理を行わない
        If okurijoNo >= "3250969993" Then
            MyBase.SetMessageStore("00", "E815", , dt.Rows(0).Item("OUTKA_NO_L").ToString)
            okurijoNo = String.Empty
        End If
#End If
        '支払人コード
        Dim siharaiCd As String = dt.Rows(0).Item("SHIHARAININ_CD").ToString.Replace("-", "")

        '個数
        If String.IsNullOrEmpty(dt.Rows(0).Item("KOSU").ToString) = True OrElse dt.Rows(0).Item("KOSU").ToString = "0" Then
            MyBase.SetMessageStore("00", "E454", New String() {"個数が空欄もしくは0", "出力", "1以上999以下の正しい値を入力をしてください。"}, dt.Rows(0).Item("OUTKA_NO_L").ToString)
            'Return ds
        End If
        Dim kosu As String = dt.Rows(0).Item("KOSU").ToString

        '重量
        If String.IsNullOrEmpty(dt.Rows(0).Item("JYURYO").ToString) = True OrElse dt.Rows(0).Item("JYURYO").ToString = "0" Then
            MyBase.SetMessageStore("00", "E454", New String() {"重量が空欄もしくは0", "出力", "1以上99999以下の正しい値を入力をしてください。"}, dt.Rows(0).Item("OUTKA_NO_L").ToString)
        End If
        Dim juryo As String = CDbl(dt.Rows(0).Item("JYURYO")).ToString

        '容積（使用しないため0）
        Dim yoseki As String = "0"
        '合計運賃（使用しないため0）
        Dim unchin As String = "0"
        '路線着地コード
        Dim rosenChakuchiCd As String
        If dt.Rows(0).Item("CHAKU_CD").ToString.Length = 5 Then
            rosenChakuchiCd = dt.Rows(0).Item("CHAKU_CD").ToString
        Else
            rosenChakuchiCd = "00000"
        End If

        '荷主伝票番号(自由項目。営業所＋運送管理番号（大）＋00（後続処理のため0を詰める）)
        Dim ninusiDenpyoNo As String = String.Concat(dt.Rows(0).Item("NRS_BR_CD").ToString, dt.Rows(0).Item("UNSO_NO_L").ToString, "00")

        '荷受人郵便番号
        If String.IsNullOrEmpty(dt.Rows(0).Item("NIUKENIN_ZIP").ToString) = True Then
            MyBase.SetMessageStore("00", "E454", New String() {"郵便番号が空欄もしくは不正", "出力", "正しく入力をしてください。"}, dt.Rows(0).Item("OUTKA_NO_L").ToString)
        End If
        Dim yubinNo As String = dt.Rows(0).Item("NIUKENIN_ZIP").ToString.Replace("-", "")

        '荷受人TEL
        Dim niukeTel As String = dt.Rows(0).Item("NIUKENIN_TEL").ToString.Replace("-", "")
        '荷受人名１
        Dim niukeNm1 As String = dt.Rows(0).Item("NIUKENIN_MEI1").ToString
        '荷受人名２
        Dim niukeNm2 As String = dt.Rows(0).Item("NIUKENIN_MEI2").ToString
        'QRコード用文字列(エラーがない場合に処理された文字列が格納される)
        Dim reQRCode As String = ""


#If False Then ' 名鉄側でログ出力の停止は対応されないため、カレントフォルダを変更して出力先を変更する。changed 2016.01.06 inoue
        KogumaQRerr = KogumaQR.GetMeiunQRCode(okurijoNo, siharaiCd, kosu, juryo, yoseki, unchin, rosenChakuchiCd, _
                                               ninusiDenpyoNo, yubinNo, niukeTel, niukeNm1, niukeNm2, reQRCode)
#Else

        ' GetMeiunQRCodeメソッドは、実行するアプリケーションのカレントディレクトリにログを出力する。
        ' 現在、カレントディレクトリは、iisのアプリケーションプールのデフォルトになっているおり、
        ' 書込み禁止となっているためエラーが発生する。
        ' 名鉄より提供されたライブラリであり、変更する設定がないため、一時的にカレントフォルダを変更する。
        ' プロセスをまたぐ排他処理は実装しない
        SyncLock _LockObject

            Dim currentDir As String = System.Environment.CurrentDirectory
            Dim logOutFolder As String = Path.Combine(Path.GetTempPath, String.Format("{0}_{1}", DateTime.Now.ToString("yyyyMMddHHmmssfff"), Guid.NewGuid.ToString("N")))

            ' 一時出力用フォルダ作成
            Directory.CreateDirectory(logOutFolder)

            '　カレントフォルダを一時変更
            System.Environment.CurrentDirectory = logOutFolder

            KogumaQRerr = KogumaQR.GetMeiunQRCode(okurijoNo, siharaiCd, kosu, juryo, yoseki, unchin, rosenChakuchiCd, _
                                                   ninusiDenpyoNo, yubinNo, niukeTel, niukeNm1, niukeNm2, reQRCode)

            ' カレントを戻す
            System.Environment.CurrentDirectory = currentDir

            ' ログ削除
            Directory.Delete(logOutFolder, True)

        End SyncLock

#End If

        '00の場合、問題なし。それ以外はエラー
        dt.Rows(0).Item("KOGUMA_QR") = reQRCode
        'If (KogumaQRerr = "00") Then
        '    dt.Rows(0).Item("KOGUMA_QR") = reQRCode
        'Else
        '    MyBase.SetMessageStore("00", "E454", New String() {"QRコード出力に必要な項目が誤り", "出力", ""}, dt.Rows(0).Item("OUTKA_NO_L").ToString)
        'End If

        Return ds

    End Function

#End Region

#Region "印刷処理"


    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function getMeitetuLabel(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = "LMC794IN"
        Dim tableNmOut As String = "LMC794OUT"
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

            setDs = Me.SelectMPrt_Label(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectMeitetuLabel(setDs)

            '帳票パターンを格納する
            setDs.Tables("LMC794OUT").Rows(0).Item("RPT_ID") = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID")


            If String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ZIP").ToString) = True Then
                MyBase.SetMessageStore("00", "E454", New String() {"郵便番号が空欄もしくは不正", "出力", "正しく入力をしてください。"}, dt.Rows(0).Item("OUTKA_NO_L").ToString)
                Continue For
            Else
                'Zipパターン取得のための郵便番号のハイフン除去
                setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ZIP") = setDs.Tables("LMC794OUT").Rows(0).Item("NIUKENIN_ZIP").ToString.Replace("-", "")
            End If

            '名鉄運輸サイドの着店等を取得
            setDs = Me.SelectMeitetuZip(setDs)

            '区分値には発店別の郵便番号パターンを格納
            Dim ZipPattern As String = setDs.Tables("MEITETSU_ZIP").Rows(0).Item("KBN_NM1").ToString
            '群馬BP用。荷主詳細Mに登録すれば、登録した郵便番号パターンを使用する
            Dim setZip As String = setDs.Tables("LMC794OUT").Rows(0).Item("ZIP_PATTERN").ToString

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
            setDs.Tables("LMC794OUT").Rows(0).Item("CHAKU_CD") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTI_CD_", ZipPattern))
            setDs.Tables("LMC794OUT").Rows(0).Item("CYAKU_NM") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("CHAKUTEN_NM_", ZipPattern))
            setDs.Tables("LMC794OUT").Rows(0).Item("SIWAKE_NO") = setDs.Tables("MEITETSU_ZIP").Rows(0).Item(String.Concat("SHIWAKE_NO_", ZipPattern))

            'QRコード生成
            Me.getKogumaQRCode(setDs)

            'QRコード生成に失敗したら送り状の出力を行わない
            If String.IsNullOrEmpty(setDs.Tables("LMC794OUT").Rows(0).Item("KOGUMA_QR").ToString) = True Then
                Continue For
            End If

            '日付処理
            Dim Toutyaku As String = setDs.Tables("LMC794OUT").Rows(0).Item("ARR_PLAN_DATE").ToString

            If String.IsNullOrEmpty(Toutyaku) = False Then

                'スラッシュ追加編集
                Dim ConvertDate As String = Mid(Toutyaku, 1, 4) & "/" & Mid(Toutyaku, 5, 2) & "/" & Mid(Toutyaku, 7, 2)
                '配送日から曜日を取得。送り状の日付判断に使用
                setDs.Tables("LMC794OUT").Rows(0).Item("DAY") = Weekday(CDate(ConvertDate))

                '月日追加編集
                Dim Month As String = String.Concat(Mid(Toutyaku, 5, 2), "月")
                Dim Day As String = String.Concat(Mid(Toutyaku, 7, 2), "日")

                setDs.Tables("LMC794OUT").Rows(0).Item("ARR_PLAN_DATE") = String.Concat(Month, Day)

            End If

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
            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMC794OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC794OUT"), _
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
    Private Function SelectMPrt_Label(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt_Label", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function


    ''' <summary>
    '''　出力対象帳票パターン運送取得処理  ADD 2017/03/01
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt_LabelUnso(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt_LabelUnso", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

#If False Then
    '2015.10.01 tsunehira add
    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMeitetuLabel(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(Me._Dac, "SelectMeitetuLabel", ds)

    End Function
#End If

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
