' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG520BLC : 請求鑑 (値引表示有)
'  作  成  者       :  [笈川]
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

Public Class LMG520BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC


#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG520DAC = New LMG520DAC()

    Private midashi As ArrayList = New ArrayList

    Private midashisort As ArrayList = New ArrayList

#End Region

#Region "Const"
    '★ ADD START 2011/09/06 SUGA

    ''' <summary>
    ''' 印刷順番(PRINT_SORT)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const INJUN_TAX_BETSU_TAISYO_GK As String = "100"
    Private Const INJUN_TAX_GK As String = "101"
    Private Const INJUN_SKYU_ALL_GK As String = "102"

    '2014.08.21 追加START 多通貨対応
    Private Const SEIKYUSYO_LMG523 As String = "LMG523"
    Private Const SEIKYUSYO_LMG571 As String = "LMG571"
    Private Const SEIKYUSYO_LMG572 As String = "LMG572"
    Private Const CURR_NM_JPY As String = "円"
    Private Const CURR_RATE As Double = 1.0
    '2014.08.21 追加END 多通貨対応

    '★ ADD E N D 2011/09/06 SUGA


#If True Then ' 鑑作成区分名表示 20161025 added inoue

    ''' <summary>
    ''' 見出し(正)
    ''' </summary>
    Private Const SEI_NM As String = "正"

    ''' <summary>
    ''' 見出し(副)
    ''' </summary>
    Private Const HUKU_NM As String = "副"

    ''' <summary>
    ''' 見出し(控)
    ''' </summary>
    Private Const HIKAE_NM As String = "控"

    ''' <summary>
    ''' 見出し(経理部)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const KEIRIHIKAE_NM As String = "経理部控"


    ''' <summary>
    ''' 鑑明細作成種別区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class MAKE_SYU_KB

        ''' <summary>
        ''' 自動
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AUTO As String = "00"

        ''' <summary>
        ''' 追加
        ''' </summary>
        ''' <remarks></remarks>
        Public Const ADD As String = "01"
    End Class

    ''' <summary>
    ''' 鑑明細テンプレート取込区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class TEMPLATE_IMP_FLG

        ''' <summary>
        ''' その他
        ''' </summary>
        ''' <remarks></remarks>
        Public Const OTHER As String = "00"

        ''' <summary>
        ''' テンプレート(IMPORT)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TEMPLATE As String = "01"
    End Class

    ''' <summary>
    ''' 鑑作成区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class KABAMI_CRT_KB

        ''' <summary>
        ''' 自動取込請求書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AUTO As String = "00"

        ''' <summary>
        ''' 手書き請求書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MANUAL As String = "01"

    End Class


    ''' <summary>
    ''' 鑑作成区分
    ''' </summary>
    ''' <remarks></remarks>
    Private Class KAGAMI_CRT_NM

        ''' <summary>
        ''' 自動取込請求書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const AUTO As String = "自動請求書"

        ''' <summary>
        ''' 手書き請求書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MANUAL As String = "手書き請求書"

        ''' <summary>
        ''' 半自動請求書
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEMI_AUTO As String = "半自動請求書"

    End Class

#End If


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

        '初期化
        midashi = New ArrayList
        midashisort = New ArrayList

        '元のデータ
        Dim tableNmIn As String = "LMG520IN"
        Dim tableNmOut As String = "LMG520OUT"
        Dim tableNmOutMado As String = "LMG520OUT_MADO" 'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        Dim tableNmSet As String = "LMG520SET"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtSet As DataTable = ds.Tables(tableNmSet)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)
        Dim dtOutMado As DataTable = ds.Tables(tableNmOutMado)   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

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
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '2014.08.21 追加START 多通貨対応
            '出力データセットへ設定
            If Convert.ToDecimal(setDs.Tables("LMG520SET").Rows(0).Item("EX_RATE")) = 1.0 AndAlso _
               (setDs.Tables("LMG520SET").Rows(0).Item("SEIQ_CURR_CD")).Equals(LMG520BLC.CURR_NM_JPY) = True Then
                setDs = dataChange(ds, setDs)
            Else
                setDs = dataChangeExchange(ds, setDs)
            End If
            '2014.08.21 追加END 多通貨対応

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
        ''For Each dr As DataRow In ds.Tables(tableNmRpt).Rows
        ''    'レポートIDが空の場合は処理を飛ばす
        ''    If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
        ''        Continue For
        ''    End If
        ''    '印刷処理実行
        ''    Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
        ''    prtDs = New DataSet
        ''    '指定したレポートIDのデータを抽出する。

        ''    'If ds.Tables(tableNmOut).Rows(0).Item("RPT_ID").ToString().Equals(LMG520BLC.SEIKYUSYO_LMG571) = True Then
        ''    '    dr.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG571
        ''    'ElseIf ds.Tables(tableNmOut).Rows(0).Item("RPT_ID").ToString().Equals(LMG520BLC.SEIKYUSYO_LMG571) = True Then
        ''    '    dr.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG572
        ''    'ElseIf ds.Tables(tableNmOut).Rows(0).Item("RPT_ID").ToString().Equals(LMG520BLC.SEIKYUSYO_LMG523) = True Then
        ''    '    dr.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG523
        ''    'End If

        ''    '2014.08.21 追加START 多通貨対応
        ''    prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item("RPT_ID").ToString())
        ''    '2014.08.21 追加END 多通貨対応

        ''    '帳票CSV出力
        ''    comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
        ''                        dr.Item("PTN_ID").ToString(), _
        ''                        dr.Item("PTN_CD").ToString(), _
        ''                        dr.Item("RPT_ID").ToString(), _
        ''                        prtDs.Tables(tableNmOut), _
        ''                        ds.Tables(LMConst.RD))

        ''Next
#If True Then   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
        Dim sDOC_SEI_YN As String = ds.Tables("LMG520IN").Rows(0).Item("SEI").ToString.Trim
        Dim sDOCDOC_DEST_YN_SEI_YN As String = ds.Tables("LMG520OUT").Rows(0).Item("DOC_DEST_YN").ToString.Trim
        Dim sATENA_RPR_ID As String = ds.Tables("LMG520OUT").Rows(0).Item("ATENA_RPR_ID").ToString.Trim

        If ("1").Equals(sDOC_SEI_YN) = True _
            AndAlso ("01").Equals(sDOCDOC_DEST_YN_SEI_YN) = True _
            AndAlso String.IsNullOrEmpty(sATENA_RPR_ID) = False Then
            '正印刷で請求マスタの宛先有のとき
            ds.Tables("LMG520OUTMADO").Clear()

            ds.Tables("LMG520OUTMADO").ImportRow(ds.Tables("LMG520OUT").Rows(0))
            ds.Tables("LMG520OUTMADO").Rows(0).Item("RPT_ID") = sATENA_RPR_ID

            For Each dr As DataRow In ds.Tables(tableNmRpt).Rows
                '印刷処理実行
                Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
                prtDs = New DataSet

                '帳票CSV出力  PTN_ID.PTN_CD　鑑のまま使用
                comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                               dr.Item("PTN_ID").ToString(),
                                dr.Item("PTN_CD").ToString(),
                                sATENA_RPR_ID,
                                ds.Tables("LMG520OUTMADO"),
                                ds.Tables(LMConst.RD))

            Next

        End If
#End If
        '↑から移動
        'レポートID分繰り返す
        'Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables(tableNmRpt).Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet
            '指定したレポートIDのデータを抽出する。

            'If ds.Tables(tableNmOut).Rows(0).Item("RPT_ID").ToString().Equals(LMG520BLC.SEIKYUSYO_LMG571) = True Then
            '    dr.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG571
            'ElseIf ds.Tables(tableNmOut).Rows(0).Item("RPT_ID").ToString().Equals(LMG520BLC.SEIKYUSYO_LMG571) = True Then
            '    dr.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG572
            'ElseIf ds.Tables(tableNmOut).Rows(0).Item("RPT_ID").ToString().Equals(LMG520BLC.SEIKYUSYO_LMG523) = True Then
            '    dr.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG523
            'End If

            '2014.08.21 追加START 多通貨対応
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item("RPT_ID").ToString())
            '2014.08.21 追加END 多通貨対応

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                                dr.Item("PTN_ID").ToString(),
                                dr.Item("PTN_CD").ToString(),
                                dr.Item("RPT_ID").ToString(),
                                prtDs.Tables(tableNmOut),
                                ds.Tables(LMConst.RD))

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

#End Region

#Region "内部処理"

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' データセット設定処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function dataChangeExchange(ByVal ds As DataSet, ByVal SetDs As DataSet) As DataSet

        'インプットデータセット設定
        Dim dt As DataTable = ds.Tables("LMG520IN")
        Dim drin As DataRow = dt.Rows(0)

        'DBデータ取得データセット設定
        Dim dtSet As DataTable = SetDs.Tables("LMG520SET")
        Dim drSetcount As Integer = dtSet.Rows.Count
        Dim drSet As DataRow = Nothing
        Dim datas As DataSet = ds.Copy

        '出力データ設定
        Dim NewDs As DataSet = SetDs.Copy()
        Dim tableNewout As DataTable = NewDs.Tables("LMG520OUT")
        Dim dr As DataRow = Nothing

        '請求書種判定用
        Dim sSei As String = SEI_NM
        Dim sHuku As String = HUKU_NM

        '帳票出力用固定文言
        Dim asta As String = "*"
        Dim taisyo As String = "対象額"
        Dim MIDASHI As String = String.Empty

        Dim ZeiInJun As String = String.Empty    'プリントソート


        '正・副Sumデータ用
        Dim keisanGk As Decimal = 0              '値引前額
        Dim koteiGk As Decimal = 0               '固定値引額
        Dim NebikiGk As Decimal = 0              '値引額
        Dim NebikigoGk As Decimal = 0            '金額
        Dim Nebiki_Rt As Decimal = 0

        '課税データ設定用
        Dim sKazei As String = String.Empty      '課税区分名称
        Dim kazeinebikirt As Decimal = 0         '課税全体値引率（％）
        Dim kaKoNebiki As Decimal = 0            '固定値引額
        Dim kaNebikigaku As Decimal = 0          '値引額
        Dim kaZei As Decimal = 0                 '金額
        Dim ka As Boolean = False                '課税出力判定

        '免税データ設定用
        Dim sMenzei As String = String.Empty     '課税区分名称
        Dim menzeinebikirt As Decimal = 0        '免税全体値引率（％）
        Dim menKoNebiki As Decimal = 0           '固定値引額
        Dim menNebikigaku As Decimal = 0         '値引額
        Dim menZei As Decimal = 0                '金額
        Dim men As Boolean = False               '免税出力判定

        '非課税データ設定用
        Dim sHikazei As String = String.Empty    '課税区分名称
        Dim hikaZei As Decimal = 0               '金額
        Dim hi As Boolean = False                '非課税出力判定

#If True Then       'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
        '不課税データ設定用
        Dim sFukazei As String = String.Empty    '課税区分名称
        Dim fukaZei As Decimal = 0               '金額
        Dim fu As Boolean = False                '不課税出力判定

        Dim fukaZeiSeiq As Decimal = 0

        '不課税(立替金)データ設定用
        Dim sFukazeiTatekae As String = String.Empty    '課税区分名称
        Dim fukaZeiTatekae As Decimal = 0               '金額
        Dim fuTatekae As Boolean = False                '不課税出力判定

        Dim fukaZeiTatekaeSeiq As Decimal = 0

#End If
        '内税設定用
        Dim sUchizei As String = String.Empty    '課税区分名称
        Dim uchiZei As Decimal = 0               '金額
        Dim uchi As Boolean = False              '内税出力判定

        Dim SumGk As Decimal = 0                 '帳票金額合計用

        Dim kaZeiSeiq As Decimal = 0
        Dim menZeiSeiq As Decimal = 0
        Dim hikaZeiSeiq As Decimal = 0
        Dim uchiZeiSeiq As Decimal = 0
        Dim menNebikigakuSeiq As Decimal = 0
        Dim menzeinebikirtSeiq As Decimal = 0
        Dim menKoNebikiSeiq As Decimal = 0
        Dim kaNebikigakuSeiq As Decimal = 0
        Dim kazeinebikirtSeiq As Decimal = 0
        Dim kaKoNebikiSeiq As Decimal = 0
        Dim SumGkSeiq As Decimal = 0

        Dim keisanGkSeiq As Decimal = 0
        Dim NebikiGkSeiq As Decimal = 0
        Dim NebikigoGkSeiq As Decimal = 0

        '請求書種設定
        Dim sei As String = drin.Item("SEI").ToString()
        Dim huku As String = drin.Item("HUKU").ToString()
        Dim keirihikae As String = drin.Item("KEIRIHIKAE").ToString()
        Dim hikae As String = drin.Item("HIKAE").ToString()

        '枚数・請求書種設定
        Dim maisu As Integer = Me.MidashiHantei(sei, huku, keirihikae, hikae)

        '共通項目設定用リスト
        Dim kyotu As ArrayList = New ArrayList

        'Bkデータ
        Dim BkBusho As String = String.Empty
        Dim BkTaxKb As String = String.Empty
        Dim BkSeiqGroup As String = String.Empty
        Dim BkSeiqCd As String = String.Empty
        Dim BkSeiqNm As String = String.Empty
        Dim BkMidashi As String = String.Empty
        Dim BkTaxKbNm As String = String.Empty
        Dim BkTekiyo As String = String.Empty
        Dim BkPrintSo As String = String.Empty
        '2014.08.21 追加START 多通貨対応
        Dim BkItemCurrCd As String = String.Empty
        Dim BkRoundPos As Decimal = 0
        '2014.08.21 追加END 多通貨対応

        ' 2011/10/05 DEL START SBS)SUGA
        'Dim BkMakeSyuKb As String = String.Empty
        ' 2011/10/05 DEL E N D SBS)SUGA

        '★ ADD START 2011/09/06 SUGA
        Dim BkNebikiRt As String = String.Empty
        '★ ADD E N D 2011/09/06 SUGA

#If True Then ' 鑑作成区分名表示 20161025 added inoue
        Dim kagamiCreateName As String = Me.GetKagamiCreateName(dtSet.Select())
#End If

        ' 請求書出力内容変更 適用年月 (変更後帳票定義ファイル 使用開始年月) 以降の請求であるか否かの判定
        Dim isPrtChg As Boolean = False
        Dim isOutNewZeiMidashi As Boolean = False
        Dim isOutUchizeiTaisyoGyo As Boolean = False
        If drSetcount > 0 AndAlso
            dtSet.Rows(0).Item("SKYU_DATE").ToString().Substring(0, 6) >= drin.Item("RPT_CHG_START_YM").ToString() Then
            isPrtChg = True
            If dtSet.Rows(0).Item("RPT_ID").ToString().Substring(0, 6).ToUpper() = "LMG571" Then
                ' 特定の RPT_ID のみ、国内営業所請求書(円請求)同様の、課税対象額/税額 見出し変更を行う。
                ' 国外営業所用 RPT_ID は、RD 内部の式で見出し文字列の変換を行っているので、上記の対象 RPT_ID に加える場合は、
                ' 以下のの見出し文字列変換を、RD 内部の式に追加する必要がある。
                ' ("対象額" については既に変換を行っている RDは必要ないが、
                '  当該変換より優先する判定で、"内税対象額" の変換を行う必要がある)
                ' "nn.n%内税対象額" の "内税対象額"
                ' "nn.n%対象額" の "対象額"
                ' "nn.n%消費税" の "消費税"
                isOutNewZeiMidashi = True
            End If
            If dtSet.Rows(0).Item("RPT_ID").ToString().Substring(0, 6).ToUpper() = "LMG571" Then
                ' 特定の RPT_ID のみ、国内営業所請求書(円請求)同様の、内税額行出力を行う。
                ' 国外営業所用 RPT_ID は、RD 内部の式で見出し文字列の変換を行っているので、上記の対象 RPT_ID に加える場合は、
                ' "nn.n%内税額" の "内税額" の部分の見出し文字列変換を、RD 内部の式に追加する必要がある。
                isOutUchizeiTaisyoGyo = True
            End If
        End If

        For i As Integer = 0 To drSetcount - 1
            drSet = dtSet.Rows(i)

            drSet.Item("PRINT_SORT") = drSet.Item("PRINT_SORT").ToString().PadLeft(3, Convert.ToChar("0"))

            ''2014.08.21 追加START 多通貨対応
            'Dim drnebiki() As DataRow = dtSet.Select("NEBIKI_GK <> '0.000'")
            'If drnebiki.Length > 0 Then
            '    drSet.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG572
            '    'ElseIf drSet.Item("SEIQ_CURR_CD").ToString().Equals(LMG520BLC.CURR_NM_JPY) = True Then
            'ElseIf Convert.ToDecimal(drSet.Item("EX_RATE")) = 1.0 Then
            '    drSet.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG523
            'Else
            '    drSet.Item("RPT_ID") = LMG520BLC.SEIKYUSYO_LMG571
            'End If
            ''2014.08.21 追加END 多通貨対応

            '課税区分判定
            Select Case drSet.Item("TAX_KB").ToString()

                '2014.09.10 追加START 多通貨対応
                Case "01", "05", "09", "13"                   '課税(韓国,US含む)
                    '2014.09.10 追加END 多通貨対応
                    If isPrtChg AndAlso isOutNewZeiMidashi Then
                        Dim Setei As String = Convert.ToString(
                                                  Convert.ToDecimal(
                                                   drSet.Item("TAX_RATE").ToString) * 100)
                        sKazei = String.Concat(Setei.Substring(0, 4), "%")
                    Else
                        sKazei = drSet.Item("TAX_KB_NM").ToString()
                    End If
                    kazeinebikirt = Convert.ToDecimal(drSet.Item("NEBIKI_RT1"))
                    kaKoNebiki = Convert.ToDecimal(drSet.Item("NEBIKI_GK1"))
                    kaZei = kaZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    '2014.08.21 追加START 多通貨対応
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        'kaZeiSeiq = kaZeiSeiq + Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))
                        kaZeiSeiq = kaZeiSeiq + System.Math.Round(Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS")))
                    End If
                    '2014.08.21 追加END 多通貨対応
                    '★ DEL START 2011/09/06 SUGA
                    'ZeiInJun = "100"
                    '★ DEL E N D 2011/09/06 SUGA
                    ka = True
                Case "02", "06", "10", "14"                   '免税(韓国,US含む)
                    sMenzei = drSet.Item("TAX_KB_NM").ToString()
                    menzeinebikirt = Convert.ToDecimal(drSet.Item("NEBIKI_RT2"))
                    menKoNebiki = Convert.ToDecimal(drSet.Item("NEBIKI_GK2"))
                    menZei = menZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    '2014.08.21 追加START 多通貨対応
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        'menZeiSeiq = menZeiSeiq + Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))
                        menZeiSeiq = menZeiSeiq + System.Math.Round(Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS")))
                    End If
                    '2014.08.21 追加END 多通貨対応
                    men = True
                Case "03", "07", "11", "15"                   '非課税(韓国,US含む)
                    sHikazei = drSet.Item("TAX_KB_NM").ToString()
                    '2014.08.21 追加START 多通貨対応
                    hikaZei = hikaZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        'hikaZeiSeiq = hikaZeiSeiq + Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))
                        hikaZeiSeiq = hikaZeiSeiq + System.Math.Round(Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS")))
                    End If
                    '2014.08.21 追加END 多通貨対応
                    hi = True
#If True Then   'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
                Case "17"                  '不課税
                    sFukazei = drSet.Item("TAX_KB_NM").ToString()
                    fukaZei = fukaZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        fukaZeiSeiq = fukaZeiSeiq + System.Math.Round(Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS")))
                    End If
                    fu = True
                Case "16"                   '不課税(立替金)
                    sFukazeiTatekae = drSet.Item("TAX_KB_NM").ToString()
                    fukaZeiTatekae = fukaZeiTatekae + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        fukaZeiTatekaeSeiq = fukaZeiTatekaeSeiq + System.Math.Round(Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS")))
                    End If
                    fuTatekae = True


#End If
                Case "04", "08", "12"                    '内税(韓国,US含む)
                    If isPrtChg AndAlso isOutNewZeiMidashi Then
                        Dim Setei As String = Convert.ToString(
                                                  Convert.ToDecimal(
                                                   drSet.Item("TAX_RATE").ToString) * 100)
                        sUchizei = String.Concat(Setei.Substring(0, 4), "%", "内税")
                    Else
                        sUchizei = drSet.Item("TAX_KB_NM").ToString()
                    End If
                    uchiZei = uchiZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    '2014.08.21 追加START 多通貨対応
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        'uchiZeiSeiq = uchiZeiSeiq + Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))
                        uchiZeiSeiq = uchiZeiSeiq + System.Math.Round(Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS")))
                    End If
                    '2014.08.21 追加END 多通貨対応
                    uchi = True
            End Select
        Next

        menNebikigaku = ToRoundDown(menzeinebikirt / 100 * menZei) + menKoNebiki
        kaNebikigaku = ToRoundDown(kazeinebikirt / 100 * kaZei) + kaKoNebiki
        '2014.08.21 追加START 多通貨対応
        If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
            'menNebikigakuSeiq = ToRoundDown(menzeinebikirtSeiq / 100 * menZeiSeiq) + menKoNebikiSeiq
            'kaNebikigakuSeiq = ToRoundDown(kazeinebikirtSeiq / 100 * kaZeiSeiq) + kaKoNebikiSeiq
            menNebikigakuSeiq = ToRoundDown(menzeinebikirt / 100 * menZeiSeiq) + menKoNebiki
            kaNebikigakuSeiq = ToRoundDown(kazeinebikirt / 100 * kaZeiSeiq) + kaKoNebiki
        End If
        '2014.08.21 追加END 多通貨対応



        '合計額の計算
#If False Then  'UPD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
         SumGk = kaZei + menZei + hikaZei + uchiZei + (Convert.ToDecimal(drSet.Item("TAX_GK1"))
#End If
        SumGk = kaZei + menZei + hikaZei + uchiZei + fukaZei + fukaZeiTatekae + (Convert.ToDecimal(drSet.Item("TAX_GK1")) _
                                  + Convert.ToDecimal(drSet.Item("TAX_HASU_GK1"))) _
                                  - kaNebikigaku - menNebikigaku
        '2014.08.21 追加START 多通貨対応
        If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
            'SumGkSeiq = kaZeiSeiq + menZeiSeiq + hikaZeiSeiq + uchiZeiSeiq + (Convert.ToDecimal(drSet.Item("TAX_GK1_SEIQ")) _
            '                      + Convert.ToDecimal(drSet.Item("TAX_HASU_GK1_SEIQ"))) _
            '                      - kaNebikigakuSeiq - menNebikigakuSeiq

            SumGkSeiq = System.Math.Round(kaZeiSeiq, CInt(drSet.Item("SEIQ_ROUND_POS"))) +
                        System.Math.Round(menZeiSeiq, CInt(drSet.Item("SEIQ_ROUND_POS"))) +
                        System.Math.Round(hikaZeiSeiq, CInt(drSet.Item("SEIQ_ROUND_POS"))) +
                        System.Math.Round(fukaZeiSeiq, CInt(drSet.Item("SEIQ_ROUND_POS"))) +
                        System.Math.Round(uchiZeiSeiq, CInt(drSet.Item("SEIQ_ROUND_POS"))) +
                        System.Math.Round(Convert.ToDecimal(drSet.Item("TAX_GK1_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS"))) +
                        System.Math.Round(Convert.ToDecimal(drSet.Item("TAX_HASU_GK1_SEIQ")), CInt(drSet.Item("SEIQ_ROUND_POS"))) -
                        System.Math.Round(kaNebikigakuSeiq, CInt(drSet.Item("SEIQ_ROUND_POS"))) -
                        System.Math.Round(menNebikigakuSeiq, CInt(drSet.Item("SEIQ_ROUND_POS")))


        End If
        '2014.08.21 追加END 多通貨対応

        '請求書種用For文
        For i As Integer = 0 To maisu - 1

            'DBデータ用For文
            For j As Integer = 0 To drSetcount - 1

                '出力用データロウ設定
                dr = tableNewout.NewRow()

                drSet = dtSet.Rows(j)

                If j = 0 Then
                    '共通項目の設定
                    '2014.08.21 追加START 多通貨対応
                    'Setkyotu(kyotu, drSet, SumGkSeiq, i)
                    SetkyotuExchange(kyotu, drSet, SumGk, i)
                    '2014.08.21 追加END 多通貨対応
                End If
                '請求書種が正・副の場合
                If (sSei.Equals(Me.midashi(i).ToString()) = True _
                Or sHuku.Equals(Me.midashi(i).ToString())) = True Then
                    If j <> 0 Then
                        'Bk見出しと見出しリストのi番目判定
                        If BkMidashi.Equals(Me.midashi(i)) Then

                            'Bkと課税区分・請求項目名称が等しい場合
                            ' 2011/10/05 UPD START SBS)SUGA
                            'If BkTaxKb.Equals(drSet.Item("TAX_KB").ToString) = True _
                            'AndAlso BkSeiqGroup.Equals(drSet.Item("GROUP_KB").ToString) = True _
                            'AndAlso BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True _
                            'AndAlso BkMakeSyuKb.Equals(drSet.Item("MAKE_SYU_KB").ToString) = True _
                            'AndAlso BkNebikiRt.Equals(drSet.Item("NEBIKI_RT").ToString) = True _
                            'AndAlso "01".Equals(BkMakeSyuKb) = False Then
                            '2011/11/21 UPD START SBS)SAGAWA 摘要をグループ化項目に加える
                            'If BkTaxKb.Equals(drSet.Item("TAX_KB").ToString) = True _
                            'AndAlso BkSeiqGroup.Equals(drSet.Item("GROUP_KB").ToString) = True _
                            'AndAlso BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True _
                            'AndAlso BkNebikiRt.Equals(drSet.Item("NEBIKI_RT").ToString) = True _
                            'AndAlso BkPrintSo.Equals(drSet.Item("PRINT_SORT").ToString) = True Then
                            ' 2011/10/05 UPD E N D SBS)SUGA
                            If BkTaxKb.Equals(drSet.Item("TAX_KB").ToString) = True _
                            AndAlso BkSeiqGroup.Equals(drSet.Item("GROUP_KB").ToString) = True _
                            AndAlso BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True _
                            AndAlso BkNebikiRt.Equals(drSet.Item("NEBIKI_RT").ToString) = True _
                            AndAlso BkPrintSo.Equals(drSet.Item("PRINT_SORT").ToString) = True _
                            AndAlso BkTekiyo.Equals(drSet.Item("TEKIYO").ToString) Then
                                ' 2011/11/21 UPD E N D SBS)SAGAWA

                                '変数へデータ設定
                                koteiGk = koteiGk + Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                                keisanGk = keisanGk + Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                                NebikiGk = NebikiGk + Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                                NebikigoGk = NebikigoGk + Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                                '2014.08.21 追加START 多通貨対応
                                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                    keisanGkSeiq = keisanGkSeiq + Convert.ToDecimal(drSet.Item("KEISAN_TLGK_SEIQ"))    '値引前額
                                    NebikiGkSeiq = NebikiGkSeiq + Convert.ToDecimal(drSet.Item("NEBIKI_GK_SEIQ"))      '値引額
                                    NebikigoGkSeiq = NebikigoGkSeiq + Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))    '金額
                                End If
                                '2014.08.21 追加END 多通貨対応

                                ' 2011/10/05 DEL START SBS)SUGA
                                'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                                ' 2011/10/05 DEL START SBS)SUGA
                            Else
                                '共通項目データセット設定
                                kyotuSetDataExchange(kyotu, dr)

                                'DETAIL
                                '2011/08/15 菱刈 残No11 スタート
                                dr.Item("GROUP_KB") = BkSeiqGroup
                                '2011/08/15 菱刈 残No11 エンド
                                dr.Item("SEIQKMK_NM") = BkSeiqNm
                                dr.Item("TAX_KB") = BkTaxKb
                                dr.Item("TAX_KB_NM") = BkTaxKbNm
                                dr.Item("TEKIYO") = BkTekiyo
                                '★ ADD START 2011/09/06 SUGA
                                dr.Item("NEBIKI_RT") = BkNebikiRt
                                '★ ADD E N D 2011/09/06 SUGA

                                dr.Item("PRINT_SORT") = BkPrintSo

                                '2014.08.21 追加START 多通貨対応
                                dr.Item("ITEM_CURR_CD") = BkItemCurrCd
                                dr.Item("ITEM_ROUND_POS") = BkRoundPos
                                '2014.08.21 追加END 多通貨対応

                                'DETAIL見出し設定処理
                                If sHuku.Equals(kyotu(num.SKYU_SYOSYU)) Then

                                    '請求書種が副の場合
                                    dr.Item("DETAIL_MIDASHI") = "行番"
                                Else
                                    '請求書種が副以外の場合
                                    dr.Item("DETAIL_MIDASHI") = "コード"
                                End If

                                '部署コードは常にブランクを設定
                                dr.Item("BUSYO_CD") = String.Empty

                                '請求書合計額の設定
                                dr.Item("SKYU_KINGAKU") = SumGk
                                '2014.08.21 追加START 多通貨対応
                                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                                End If
                                '2014.08.21 追加END 多通貨対応

                                '値引き前金額の設定
                                dr.Item("KEISAN_TLGK") = keisanGk
                                '2014.08.21 追加START 多通貨対応
                                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                    dr.Item("KEISAN_TLGK_SEIQ") = keisanGkSeiq
                                End If
                                '2014.08.21 追加END 多通貨対応

                                '計算額が０の場合のシステムエラー回避用
                                If keisanGk < 1 = False Then
                                    'dr.Item("NEBIKI_RT") = (NebikiGk - koteiGk) / keisanGk * 100
                                    '2011/08/15 菱刈 No16 スタート
                                    dr.Item("NEBIKI_RT") = Nebiki_Rt
                                    '2011/08/15 菱刈 No16 エンド
                                Else
                                    dr.Item("NEBIKI_RT") = 0
                                End If

                                '固定・値引額をマイナスに変更する
                                dr.Item("KOTEI_NEBIKI_GK") = koteiGk * -1
                                dr.Item("NEBIKI_GK") = NebikiGk * -1
                                '値引き後金額
                                dr.Item("KINGAKU") = NebikigoGk
                                '2014.08.21 追加START 多通貨対応
                                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                    dr.Item("NEBIKI_GK_SEIQ") = NebikiGkSeiq * -1
                                    '値引き後金額
                                    dr.Item("KINGAKU_SEIQ") = NebikigoGkSeiq
                                End If
                                '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                                ' 029595 【LMS】請求書（インボイス）対応　
                                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                                'データの設定
                                tableNewout.Rows.Add(dr)

                                '変数へデータ設定
                                BkPrintSo = drSet.Item("PRINT_SORT").ToString()
                                BkTekiyo = drSet.Item("TEKIYO").ToString()
                                koteiGk = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                                keisanGk = Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                                NebikiGk = Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                                NebikigoGk = Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                                '2014.08.21 追加START 多通貨対応
                                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                    keisanGkSeiq = Convert.ToDecimal(drSet.Item("KEISAN_TLGK_SEIQ"))    '値引前額
                                    NebikiGkSeiq = Convert.ToDecimal(drSet.Item("NEBIKI_GK_SEIQ"))      '値引額
                                    NebikigoGkSeiq = Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))    '金額
                                End If
                                '2014.08.21 追加END 多通貨対応

                                ' 2011/10/05 DEL START SBS)SUGA
                                'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                                ' 2011/10/05 DEL E N D SBS)SUGA
                                Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))
                                '2014.08.21 追加START 多通貨対応
                                BkItemCurrCd = drSet.Item("ITEM_CURR_CD").ToString()
                                BkRoundPos = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                                '2014.08.21 追加END 多通貨対応

                            End If
                        Else

                            '共通項目データセット設定
                            kyotuSetDataExchange(kyotu, dr)
                            'DETAIL
                            '2011/08/15 菱刈 残No11 スタート
                            dr.Item("GROUP_KB") = BkSeiqGroup
                            '2011/08/15 菱刈 残No11 エンド
                            dr.Item("SEIQKMK_NM") = BkSeiqNm
                            dr.Item("TAX_KB") = BkTaxKb
                            dr.Item("TAX_KB_NM") = BkTaxKbNm
                            dr.Item("TEKIYO") = BkTekiyo
                            '★ ADD START 2011/09/06 SUGA
                            dr.Item("NEBIKI_RT") = BkNebikiRt
                            '★ ADD E N D 2011/09/06 SUGA

                            dr.Item("PRINT_SORT") = BkPrintSo

                            '2014.08.21 追加START 多通貨対応
                            dr.Item("ITEM_CURR_CD") = BkItemCurrCd
                            dr.Item("ITEM_ROUND_POS") = BkRoundPos
                            '2014.08.21 追加END 多通貨対応

                            'DETAIL見出し設定処理
                            If sHuku.Equals(kyotu(num.SKYU_SYOSYU)) Then
                                '請求書種が副の場合
                                dr.Item("DETAIL_MIDASHI") = "行番"
                            Else
                                '請求書種が副以外の場合
                                dr.Item("DETAIL_MIDASHI") = "コード"
                            End If

                            '部署コードは常にブランクを設定
                            dr.Item("BUSYO_CD") = String.Empty

                            '請求書合計額の設定
                            dr.Item("SKYU_KINGAKU") = SumGk
                            '2014.08.21 追加START 多通貨対応
                            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                dr.Item("SKYU_KINGAKU") = SumGkSeiq
                            End If
                            '2014.08.21 追加END 多通貨対応

                            '値引き前金額の設定
                            dr.Item("KEISAN_TLGK") = keisanGk
                            '2014.08.21 追加START 多通貨対応
                            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                dr.Item("KEISAN_TLGK_SEIQ") = keisanGkSeiq
                            End If
                            '2014.08.21 追加END 多通貨対応

                            '計算額が０の場合のシステムエラー回避用
                            If keisanGk < 1 = False Then
                                'dr.Item("NEBIKI_RT") = (NebikiGk - koteiGk) / keisanGk * 100
                                '2011/08/15 菱刈 No16 スタート
                                dr.Item("NEBIKI_RT") = Nebiki_Rt
                                '2011/08/15 菱刈 No16 エンド
                            Else
                                dr.Item("NEBIKI_RT") = 0
                            End If

                            '固定・値引額をマイナスに変更する
                            dr.Item("KOTEI_NEBIKI_GK") = koteiGk * -1
                            dr.Item("NEBIKI_GK") = NebikiGk * -1
                            '2014.08.21 追加START 多通貨対応
                            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                dr.Item("NEBIKI_GK_SEIQ") = NebikiGkSeiq * -1
                            End If
                            '2014.08.21 追加END 多通貨対応

                            '値引き後金額
                            dr.Item("KINGAKU") = NebikigoGk
                            '2014.08.21 追加START 多通貨対応
                            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                dr.Item("KINGAKU_SEIQ") = NebikigoGkSeiq
                            End If
                            '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                            dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                            dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                            dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                            dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                            dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                            dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                            dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                            dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                            dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                            dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                            dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                            dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                            dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                            ' 029595 【LMS】請求書（インボイス）対応　
                            dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                            dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                            dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                            dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                            dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                            dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                            dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                            dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                            dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                            dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                            dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                            dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                            'データの設定
                            tableNewout.Rows.Add(dr)

                            '変数へデータ設定
                            BkTekiyo = drSet.Item("TEKIYO").ToString()
                            keisanGk = Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                            NebikiGk = Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                            NebikigoGk = Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                            '2014.08.21 追加START 多通貨対応
                            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                                keisanGkSeiq = Convert.ToDecimal(drSet.Item("KEISAN_TLGK_SEIQ"))    '値引前額
                                NebikiGkSeiq = Convert.ToDecimal(drSet.Item("NEBIKI_GK_SEIQ"))      '値引額
                                NebikigoGkSeiq = Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))    '金額
                            End If
                            '2014.08.21 追加END 多通貨対応
                            koteiGk = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                            ' 2011/10/05 DEL START SBS)SUGA
                            'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                            ' 2011/10/05 DEL E N D SBS)SUGA
                            Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))

                            '2014.08.21 追加START 多通貨対応
                            BkItemCurrCd = drSet.Item("ITEM_CURR_CD").ToString()
                            BkRoundPos = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                            '2014.08.21 追加END 多通貨対応

                        End If
                    Else

                        '変数へデータ設定
                        keisanGk = keisanGk + Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                        NebikiGk = NebikiGk + Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                        NebikigoGk = NebikigoGk + Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                        '2014.08.21 追加START 多通貨対応
                        If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                            keisanGkSeiq = keisanGkSeiq + Convert.ToDecimal(drSet.Item("KEISAN_TLGK_SEIQ"))    '値引前額
                            NebikiGkSeiq = NebikiGkSeiq + Convert.ToDecimal(drSet.Item("NEBIKI_GK_SEIQ"))      '値引額
                            NebikigoGkSeiq = NebikigoGkSeiq + Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))    '金額
                        End If
                        koteiGk = koteiGk + Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                        '2014.08.21 追加END 多通貨対応

                        ' 2011/10/05 DEL START SBS)SUGA
                        'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                        ' 2011/10/05 DEL E N D SBS)SUGA

                        BkTekiyo = drSet.Item("TEKIYO").ToString()
                        BkPrintSo = drSet.Item("PRINT_SORT").ToString()
                        Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))

                        '2014.08.21 追加START 多通貨対応
                        BkItemCurrCd = drSet.Item("ITEM_CURR_CD").ToString()
                        BkRoundPos = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                        '2014.08.21 追加END 多通貨対応

                    End If
                Else
                    '請求諸種が控・経理部控の場合

                    '共通項目データセット設定
                    kyotuSetDataExchange(kyotu, dr)

                    'DETAIL
                    dr.Item("DETAIL_MIDASHI") = "コード"
                    dr.Item("BUSYO_CD") = drSet.Item("BUSYO_CD").ToString
                    dr.Item("SKYU_KINGAKU") = SumGk
                    '2014.08.21 追加START 多通貨対応
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        dr.Item("SKYU_KINGAKU") = SumGkSeiq
                    End If
                    '2014.08.21 追加END 多通貨対応

                    '2011/08/15 菱刈 残No11 スタート
                    dr.Item("GROUP_KB") = drSet.Item("GROUP_KB").ToString
                    '2011/08/15 菱刈 残No11 エンド

                    dr.Item("BUSYO_CD") = drSet.Item("BUSYO_CD").ToString
                    dr.Item("SEIQKMK_NM") = drSet.Item("SEIQKMK_NM").ToString
                    dr.Item("KEISAN_TLGK") = drSet.Item("KEISAN_TLGK").ToString
                    dr.Item("NEBIKI_GK") = Convert.ToDecimal(drSet.Item("NEBIKI_GK")) * -1
                    dr.Item("KINGAKU") = drSet.Item("KINGAKU")
                    '2014.08.21 追加START 多通貨対応
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        dr.Item("KEISAN_TLGK_SEIQ") = drSet.Item("KEISAN_TLGK_SEIQ").ToString
                        dr.Item("NEBIKI_GK_SEIQ") = Convert.ToDecimal(drSet.Item("NEBIKI_GK_SEIQ")) * -1
                        dr.Item("KINGAKU_SEIQ") = drSet.Item("KINGAKU_SEIQ")
                    End If
                    '2014.08.21 追加END 多通貨対応
                    dr.Item("NEBIKI_RT") = dtSet.Rows(j).Item("NEBIKI_RT").ToString
                    dr.Item("KOTEI_NEBIKI_GK") = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK")) * -1

                    dr.Item("TAX_KB") = drSet.Item("TAX_KB").ToString
                    dr.Item("TAX_KB_NM") = drSet.Item("TAX_KB_NM").ToString
                    dr.Item("TEKIYO") = drSet.Item("TEKIYO").ToString

                    dr.Item("PRINT_SORT") = drSet.Item("PRINT_SORT").ToString

                    '2014.08.21 追加START 多通貨対応
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString()
                    dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                    '2014.08.21 追加END 多通貨対応

                    '20160707 経理部控　勘定区分対応
                    'GROUP_KBがastariskの場合は表示しない
                    dr.Item("KANJO") = drSet.Item("KANJO").ToString()
                    '20160707 経理部控　勘定区分対応
                    dr.Item("KANJO_NM") = drSet.Item("KANJO_NM").ToString()

                    'If (Me.IsKeiriHikae(Me.midashi(i).ToString())) Then
                    '    dr.Item("MAKE_SYU_KB") = drSet.Item("MAKE_SYU_KB").ToString
                    '    dr.Item("TEMPLATE_IMP_FLG") = drSet.Item("TEMPLATE_IMP_FLG").ToString
                    'End If
                    '後の集計処理の都合上、控であっても値はセットする（集計処理の最後でクリアする）
                    dr.Item("MAKE_SYU_KB") = drSet.Item("MAKE_SYU_KB").ToString
                    dr.Item("TEMPLATE_IMP_FLG") = drSet.Item("TEMPLATE_IMP_FLG").ToString

#If True Then ' 作成者名表示対応 200170420 added by inoue
                    dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                    dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                    dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                    dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                    dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                    dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                    dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                    dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                    dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                    dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                    dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                    dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                    dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                    ' 029595 【LMS】請求書（インボイス）対応　
                    dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                    dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                    dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                    dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                    dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                    dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                    dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                    dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                    dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                    dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                    dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                    dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                    'データの設定
                    tableNewout.Rows.Add(dr)

                    '変数
                    '2014.08.21 追加START 多通貨対応
                    keisanGk = Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                    NebikiGk = Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                    NebikigoGk = Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                    If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                        keisanGkSeiq = Convert.ToDecimal(drSet.Item("KEISAN_TLGK_SEIQ"))    '値引前額
                        NebikiGkSeiq = Convert.ToDecimal(drSet.Item("NEBIKI_GK_SEIQ"))      '値引額
                        NebikigoGkSeiq = Convert.ToDecimal(drSet.Item("KINGAKU_SEIQ"))    '金額
                    End If
                    '2014.08.21 追加END 多通貨対応
                    koteiGk = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                    Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))

                    '2014.08.21 追加START 多通貨対応
                    BkItemCurrCd = drSet.Item("ITEM_CURR_CD").ToString()
                    BkRoundPos = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                    '2014.08.21 追加END 多通貨対応

                End If

                '退避用データの設定
                BkMidashi = Me.midashi(i).ToString()
                BkSeiqGroup = drSet.Item("GROUP_KB").ToString()
                BkSeiqCd = drSet.Item("SEIQKMK_CD").ToString()
                BkSeiqNm = drSet.Item("SEIQKMK_NM").ToString()
                BkBusho = drSet.Item("BUSYO_CD").ToString()
                BkTaxKb = drSet.Item("TAX_KB").ToString()
                BkTaxKbNm = drSet.Item("TAX_KB_NM").ToString()
                '★ ADD START 2011/09/06 SUGA
                BkNebikiRt = drSet.Item("NEBIKI_RT").ToString()
                '★ ADD E N D 2011/09/06 SUGA
                ' 2011/10/05 DEL START SBS)SUGA
                BkPrintSo = drSet.Item("PRINT_SORT").ToString()
                ' 2011/10/05 DEL E N D SBS)SUGA
            Next

            '出力用データロウ設定
            dr = tableNewout.NewRow()

            '請求書種が正・副の場合はデータを設定
            If sSei.Equals(Me.midashi(i).ToString()) _
            Or sHuku.Equals(Me.midashi(i).ToString()) Then

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                '2014.08.21 追加END 多通貨対応
                'DETAIL
                '2011/08/15 菱刈 残No11 スタート
                dr.Item("GROUP_KB") = BkSeiqGroup
                '2011/08/15 菱刈 残No11 エンド
                dr.Item("SEIQKMK_NM") = BkSeiqNm
                dr.Item("TAX_KB") = BkTaxKb
                dr.Item("TAX_KB_NM") = BkTaxKbNm
                dr.Item("TEKIYO") = BkTekiyo
                '★ ADD START 2011/09/06 SUGA
                dr.Item("NEBIKI_RT") = BkNebikiRt
                '★ ADD E N D 2011/09/06 SUGA

                dr.Item("PRINT_SORT") = drSet.Item("PRINT_SORT").ToString

                '2014.08.21 追加START 多通貨対応
                dr.Item("ITEM_CURR_CD") = BkItemCurrCd
                dr.Item("ITEM_ROUND_POS") = BkRoundPos
                '2014.08.21 追加END 多通貨対応


                '見出し・部署コード設定
                If sHuku.Equals(kyotu(num.SKYU_SYOSYU)) Then

                    dr.Item("DETAIL_MIDASHI") = "行番"
                Else
                    dr.Item("DETAIL_MIDASHI") = "コード"
                End If
                dr.Item("BUSYO_CD") = String.Empty

                dr.Item("KEISAN_TLGK") = keisanGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = keisanGkSeiq
                End If
                '2014.08.21 追加END 多通貨対応

                If keisanGk < 1 = False Then
                    'dr.Item("NEBIKI_RT") = (NebikiGk - koteiGk) / keisanGk * 100
                    '2011/08/15 菱刈 No16 スタート
                    dr.Item("NEBIKI_RT") = Nebiki_Rt
                    '2011/08/15 菱刈 No16 エンド
                End If

                dr.Item("KOTEI_NEBIKI_GK") = koteiGk * -1
                dr.Item("NEBIKI_GK") = NebikiGk * -1
                dr.Item("KINGAKU") = NebikigoGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("NEBIKI_GK_SEIQ") = NebikiGk * -1
                    dr.Item("KINGAKU_SEIQ") = NebikigoGkSeiq
                End If
                '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '請求書種が控の場合、セグメントで分割された明細を集計する（ABP連携によりセグメントが追加される前のデータ構成を再現）
            Dim nowMidashi As String = Me.midashi(i).ToString()
            If HIKAE_NM.Equals(nowMidashi) Then
                'データ内容を一旦退避してクリア
                Dim cpyTableNewout As DataTable = tableNewout.Copy()
                tableNewout.Clear()

                '退避データのループ
                For iCpy As Integer = 0 To cpyTableNewout.Rows.Count - 1
                    Dim drCopy As DataRow = cpyTableNewout.Rows(iCpy)

                    '集計が必要なデータか判定
                    '　請求書種が処理中と一致
                    '　手動追加ではない
                    '　テンプレートではない
                    '　合計行ではない
                    If (nowMidashi.Equals(drCopy.Item("SKYU_SYOSYU").ToString)) _
                            AndAlso (Not MAKE_SYU_KB.ADD.Equals(drCopy.Item("MAKE_SYU_KB").ToString)) _
                            AndAlso (Not TEMPLATE_IMP_FLG.TEMPLATE.Equals(drCopy.Item("TEMPLATE_IMP_FLG").ToString)) _
                            AndAlso (Not asta.Equals(drCopy.Item("GROUP_KB").ToString)) Then
                        '集計が必要
                        '集計キーに一致するデータがすでに戻されているか調べる
                        Dim sCpyKey As String = String.Concat(
                                drCopy.Item("SKYU_SYOSYU").ToString,
                                drCopy.Item("GROUP_KB").ToString,
                                drCopy.Item("TAX_KB").ToString,
                                drCopy.Item("KANJO").ToString,
                                drCopy.Item("BUSYO_CD").ToString,
                                drCopy.Item("NEBIKI_RT").ToString,
                                drCopy.Item("TEKIYO").ToString
                                )
                        Dim iIdx As Integer = -1

                        For iNew As Integer = 0 To tableNewout.Rows.Count - 1
                            Dim drNew As DataRow = tableNewout.Rows(iNew)
                            Dim sNewKey As String = String.Concat(
                                    drNew.Item("SKYU_SYOSYU").ToString,
                                    drNew.Item("GROUP_KB").ToString,
                                    drNew.Item("TAX_KB").ToString,
                                    drNew.Item("KANJO").ToString,
                                    drNew.Item("BUSYO_CD").ToString,
                                    drNew.Item("NEBIKI_RT").ToString,
                                    drNew.Item("TEKIYO").ToString
                                    )

                            If sNewKey.Equals(sCpyKey) Then
                                '集計キーに一致するデータがあった
                                iIdx = iNew
                                Exit For
                            End If
                        Next

                        '集計判定
                        If iIdx = -1 Then
                            '今回の集計キーデータは初なのでそのまま戻す
                            tableNewout.ImportRow(drCopy)
                        Else
                            'すでに戻されている同集計キーのデータに加算する（符号反転している項目は一旦戻してから加算）
                            tableNewout.Rows(iIdx).Item("KEISAN_TLGK") = Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KEISAN_TLGK")) + Convert.ToDecimal(drCopy.Item("KEISAN_TLGK"))
                            tableNewout.Rows(iIdx).Item("KOTEI_NEBIKI_GK") = (Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KOTEI_NEBIKI_GK")) * -1 + Convert.ToDecimal(drCopy.Item("KOTEI_NEBIKI_GK"))) * -1
                            tableNewout.Rows(iIdx).Item("NEBIKI_GK") = (Convert.ToDecimal(tableNewout.Rows(iIdx).Item("NEBIKI_GK")) * -1 + Convert.ToDecimal(drCopy.Item("NEBIKI_GK"))) * -1
                            tableNewout.Rows(iIdx).Item("KINGAKU") = Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KINGAKU")) + Convert.ToDecimal(drCopy.Item("KINGAKU"))
                            If Convert.ToDecimal(drCopy.Item("EX_RATE")) <> 1.0 Then
                                tableNewout.Rows(iIdx).Item("KEISAN_TLGK_SEIQ") = Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KEISAN_TLGK_SEIQ")) + Convert.ToDecimal(drCopy.Item("KEISAN_TLGK_SEIQ"))
                                tableNewout.Rows(iIdx).Item("NEBIKI_GK_SEIQ") = (Convert.ToDecimal(tableNewout.Rows(iIdx).Item("NEBIKI_GK_SEIQ")) * -1 + Convert.ToDecimal(drCopy.Item("NEBIKI_GK_SEIQ"))) * -1
                                tableNewout.Rows(iIdx).Item("KINGAKU_SEIQ") = Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KINGAKU_SEIQ")) + Convert.ToDecimal(drCopy.Item("KINGAKU_SEIQ"))
                            End If
                        End If
                    Else
                        '集計は不要
                        'データをそのまま戻す
                        tableNewout.ImportRow(drCopy)
                    End If
                Next

                'この集計処理の都合でセットしてあった項目値をクリアする
                For iNew As Integer = 0 To tableNewout.Rows.Count - 1
                    Dim drNew As DataRow = tableNewout.Rows(iNew)

                    If HIKAE_NM.Equals(drNew.Item("SKYU_SYOSYU").ToString) Then
                        drNew.Item("MAKE_SYU_KB") = String.Empty
                        drNew.Item("TEMPLATE_IMP_FLG") = String.Empty
                    End If
                Next
            End If

            If sHuku.Equals(Me.midashi(i).ToString()) = True OrElse
            sSei.Equals(Me.midashi(i).ToString()) = True Then
                MIDASHI = "行番"
            Else
                MIDASHI = "コード"
            End If

            If isPrtChg Then
                For Each drNewout As DataRow In tableNewout.Rows()
                    drNewout.Item("SKYU_SUB_NO") = ""
                Next
            End If

            '課税設定（課税レコードが存在する場合設定）
            If ka = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                '2014.08.21 追加END 多通貨対応
                dr.Item("REMARK") = drSet.Item("REMARK").ToString
                dr.Item("DETAIL_MIDASHI") = MIDASHI
                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sKazei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = kaNebikigaku * -1
                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = kaZei + (kaNebikigaku * -1)
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = kaNebikigakuSeiq * -1
                    dr.Item("KINGAKU_SEIQ") = kaZeiSeiq + (kaNebikigakuSeiq * -1)
                End If
                dr.Item("NEBIKI_RT") = kazeinebikirt
                dr.Item("KOTEI_NEBIKI_GK") = kaKoNebiki * -1
                dr.Item("TAX_KB") = "01"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = String.Empty
                If isPrtChg Then
                    ' 請求書出力内容変更 適用年月 以降の場合

                    ' 課税(と後述の 内税・内税額) は、
                    ' 免税・非課税・不課税・不課税(立替金) より後(従来の税額 に続く位置) の出力とする。
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
                    ' 上記で従来の税額の出力位置とした一連の行は、
                    ' 課税、税額、内税、内税額 の出力順とする。
                    ' (課税、内税　 の SKYU_SUB_NO は TAX_KB  と同じ値に、
                    '  税額、内税額 の SKYU_SUB_NO はそれぞれ課税、内税 の TAX_KB と同じ値とし、
                    '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                    dr.Item("SKYU_SUB_NO") = dr.Item("TAX_KB")
                Else

                    '★ UPD START 2011/09/06 SUGA
                    'dr.Item("PRINT_SORT") = ZeiInJun
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                    '★ UPD E N D 2011/09/06 SUGA
                End If

                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '免税設定（免税レコードが存在する場合設定）
            If men = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI
                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sMenzei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = menNebikigaku * -1
                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = menZei + (menNebikigaku * -1)
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = menNebikigakuSeiq * -1
                    dr.Item("KINGAKU_SEIQ") = menZeiSeiq + (menNebikigakuSeiq * -1)
                End If
                '2014.08.21 追加END 多通貨対応
                dr.Item("NEBIKI_RT") = menzeinebikirt
                dr.Item("KOTEI_NEBIKI_GK") = menKoNebiki * -1
                dr.Item("TAX_KB") = "02"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = String.Empty
                '★ UPD START 2011/09/06 SUGA
                'dr.Item("PRINT_SORT") = ZeiInJun
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                '★ UPD E N D 2011/09/06 SUGA

                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '非課税（非課税レコードが存在する場合設定）
            If hi = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sHikazei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = hikaZei
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                    dr.Item("KINGAKU_SEIQ") = hikaZeiSeiq
                End If
                '2014.08.21 追加END 多通貨対応
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("TAX_KB") = "03"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                '★ UPD START 2011/09/06 SUGA
                'dr.Item("PRINT_SORT") = ZeiInJun
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                '★ UPD E N D 2011/09/06 SUGA

                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

#If True Then   'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
            '不課税（不課税レコードが存在する場合設定）
            If fu = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sFukazei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = fukaZei
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                    dr.Item("KINGAKU_SEIQ") = fukaZeiSeiq
                End If
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("TAX_KB") = "18"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()

                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If
            '不課税（立替金）
            If fuTatekae = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sFukazeiTatekae, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = fukaZeiTatekae
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                    dr.Item("KINGAKU_SEIQ") = fukaZeiSeiq
                End If
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("TAX_KB") = "18"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()

                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

#End If
            '内税（内税レコードが存在する場合設定）
            If uchi = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                '2014.08.21 追加START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                '2014.08.21 追加END 多通貨対応
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sUchizei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = uchiZei
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                    dr.Item("KINGAKU_SEIQ") = uchiZeiSeiq

                End If
                '2014.08.21 追加END 多通貨対応
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("TAX_KB") = "03"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                If isPrtChg Then
                    ' 請求書出力内容変更 適用年月 以降の場合

                    ' (前出の 課税および) 内税(と後出の 内税額) は、
                    ' 免税・非課税・不課税・不課税(立替金) より後(従来の税額 に続く位置) の出力とする。
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
                    ' 上記で従来の税額の出力位置とした一連の行は、
                    ' 課税、税額、内税、内税額 の出力順とする。
                    ' (課税、内税　 の SKYU_SUB_NO は TAX_KB  と同じ値に、
                    '  税額、内税額 の SKYU_SUB_NO はそれぞれ課税、内税 の TAX_KB と同じ値とし、
                    '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                    dr.Item("SKYU_SUB_NO") = dr.Item("TAX_KB")
                Else
                    '★ UPD START 2011/09/06 SUGA
                    'dr.Item("PRINT_SORT") = ZeiInJun
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                    '★ UPD E N D 2011/09/06 SUGA
                End If

                '2014.08.21 追加START 多通貨対応
                'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '税額レコードの作成
            dr = tableNewout.NewRow()

            '共通項目データセット設定
            kyotuSetDataExchange(kyotu, dr)
            dr.Item("SKYU_KINGAKU") = SumGk
            '2014.08.21 追加START 多通貨対応
            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                dr.Item("SKYU_KINGAKU") = SumGkSeiq
            End If
            '2014.08.21 追加END 多通貨対応
            dr.Item("REMARK") = drSet.Item("REMARK").ToString
            dr.Item("DETAIL_MIDASHI") = MIDASHI

            dr.Item("GROUP_KB") = asta
            'dr.Item("SEIQKMK_NM") = String.Concat("税額（", Convert.ToString( _
            'Convert.ToDecimal( _
            'drSet.Item("TAX_RATE").ToString) * 100), "%)")
            Dim Setei As String = Convert.ToString(
                                                  Convert.ToDecimal(
                                                   drSet.Item("TAX_RATE").ToString) * 100)

            If isPrtChg AndAlso isOutNewZeiMidashi Then
                dr.Item("SEIQKMK_NM") = String.Concat(Setei.Substring(0, 4), "%", "消費税")
            Else
                dr.Item("SEIQKMK_NM") = String.Concat("税額（", Setei.Substring(0, 4), "%)")
            End If
            dr.Item("BUSYO_CD") = String.Empty
            dr.Item("KEISAN_TLGK") = String.Empty
            dr.Item("NEBIKI_GK") = String.Empty
            '2014.08.21 追加START 多通貨対応
            'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
            If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                dr.Item("KINGAKU") = String.Empty
            Else
                dr.Item("KINGAKU") = Convert.ToDecimal(drSet.Item("TAX_GK1")) _
                                   + Convert.ToDecimal(drSet.Item("TAX_HASU_GK1"))
            End If
            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                dr.Item("KINGAKU_SEIQ") = Convert.ToDecimal(drSet.Item("TAX_GK1_SEIQ")) _
                                   + Convert.ToDecimal(drSet.Item("TAX_HASU_GK1_SEIQ"))

            End If
            dr.Item("NEBIKI_RT") = String.Empty
            dr.Item("KOTEI_NEBIKI_GK") = String.Empty
            If isPrtChg Then
                ' 請求書出力内容変更 適用年月 以降の場合

                ' 設定値については、下記 SKYU_SUB_NO 設定箇所のコメント参照
                dr.Item("TAX_KB") = "99"
            Else
                '2014.08.21 追加END 多通貨対応
                dr.Item("TAX_KB") = String.Empty
            End If
            dr.Item("TAX_KB_NM") = String.Empty
            dr.Item("TEKIYO") = String.Empty
            If isPrtChg Then
                ' 請求書出力内容変更 適用年月 以降の場合

                ' 前出の 課税・内税および後出の 内税額 は、
                ' 税額(本レコード) に続く位置 の出力とするよう、PRINT_SORT は税額(本レコード) と同値としてある。
                ' この一連の行は、
                ' 課税、税額、内税、内税額 の出力順とする。
                ' (課税、内税 　の SKYU_SUB_NO は TAX_KB  と同じ値に、
                '  税額、内税額 の SKYU_SUB_NO はそれぞれ 課税、内税 の TAX_KB と同じ値とし、
                '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                dr.Item("SKYU_SUB_NO") = "01"
            End If
            '★ UPD START 2011/09/06 SUGA
            'dr.Item("PRINT_SORT") = "101"
            dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
            '★ UPD START 2011/09/06 SUGA

            '2014.08.21 追加START 多通貨対応
            'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
            If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                dr.Item("ITEM_CURR_CD") = String.Empty
            Else
                dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
            End If
            dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
            '2014.08.21 追加END 多通貨対応

#If True Then ' 作成者名表示対応 200170420 added by inoue
            dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
            dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
            dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
            dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
            dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
            dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
            dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
            dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
            dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29014230   【LMS】請求書送付時の窓付き封筒の導入 
            dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
            dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

            dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

            ' 029595 【LMS】請求書（インボイス）対応　
            dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
            dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
            dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
            dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
            dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
            dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
            dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
            dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
            dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
            dr.Item("T_NO") = drSet.Item("T_NO").ToString()
            dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
            dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

            'データの設定（税額レコード）
            tableNewout.Rows.Add(dr)

            If isPrtChg AndAlso isOutUchizeiTaisyoGyo AndAlso uchi Then
                ' 内税額レコードの作成
                dr = tableNewout.NewRow()

                Dim taxRate As Decimal = Convert.ToDecimal(drSet.Item("TAX_RATE").ToString())

                ' 共通項目データセット設定
                kyotuSetDataExchange(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                ' START 多通貨対応
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("SKYU_KINGAKU") = SumGkSeiq
                End If
                ' END 多通貨対応
                dr.Item("REMARK") = drSet.Item("REMARK").ToString
                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                Setei = Convert.ToString(taxRate * 100)
                dr.Item("SEIQKMK_NM") = String.Concat(Setei.Substring(0, 4), "%", "内税額")
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                ' START 多通貨対応
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("KINGAKU") = String.Empty
                Else
                    dr.Item("KINGAKU") = Math.Truncate(uchiZei * taxRate / (taxRate + 1))
                End If
                If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                    dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                    dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                    dr.Item("KINGAKU_SEIQ") = Math.Truncate(uchiZeiSeiq * taxRate / (taxRate + 1))
                End If
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                ' END 多通貨対応
                ' 設定値については、下記 PRINT_SORT, SKYU_SUB_NO 設定箇所のコメント参照
                dr.Item("TAX_KB") = "99"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = String.Empty
                ' (前出の課税・内税および) 内税額 は、
                ' 請求書出力内容変更 適用年月 以降の場合、
                ' 税額 に続く位置 の出力とするよう、PRINT_SORT は税額 と同値としてある。
                ' この一連の行は、
                ' 課税、税額、内税、内税額 の出力順とする。
                ' (課税、内税 　の SKYU_SUB_NO は TAX_KB  と同じ値に、
                '  税額、内税額 の SKYU_SUB_NO はそれぞれ 課税、内税 の TAX_KB と同じ値とし、
                '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
                dr.Item("SKYU_SUB_NO") = "03"

                ' START 多通貨対応
                If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                    dr.Item("ITEM_CURR_CD") = String.Empty
                Else
                    dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
                End If
                dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
                ' END 多通貨対応

                ' 作成者名表示対応
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()

                ' QR対応
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()

                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   ' 振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   ' 014230   【LMS】請求書送付時の窓付き封筒の導入 
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString() ' 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()       ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()       ' 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                'データの設定（内税額レコード）
                tableNewout.Rows.Add(dr)
            End If

            '請求総額レコードの作成
            dr = tableNewout.NewRow()

            '共通項目データセット設定
            kyotuSetDataExchange(kyotu, dr)
            dr.Item("SKYU_KINGAKU") = SumGk
            '2014.08.21 追加START 多通貨対応
            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                dr.Item("SKYU_KINGAKU") = SumGkSeiq
            End If
            '2014.08.21 追加END 多通貨対応
            dr.Item("REMARK") = drSet.Item("REMARK").ToString
            dr.Item("DETAIL_MIDASHI") = MIDASHI

            dr.Item("GROUP_KB") = asta
            dr.Item("SEIQKMK_NM") = "請求総額"
            dr.Item("BUSYO_CD") = String.Empty
            dr.Item("KEISAN_TLGK") = String.Empty
            dr.Item("NEBIKI_GK") = String.Empty
            '2014.08.21 追加START 多通貨対応
            'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
            If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                dr.Item("KINGAKU") = String.Empty
            Else
                dr.Item("KINGAKU") = SumGk
            End If
            '
            If Convert.ToDecimal(drSet.Item("EX_RATE")) <> 1.0 Then
                dr.Item("KEISAN_TLGK_SEIQ") = String.Empty
                dr.Item("NEBIKI_GK_SEIQ") = String.Empty
                dr.Item("KINGAKU_SEIQ") = SumGkSeiq
            End If
            '2014.08.21 追加END 多通貨対応
            dr.Item("NEBIKI_RT") = String.Empty
            dr.Item("KOTEI_NEBIKI_GK") = String.Empty
            dr.Item("TAX_KB") = String.Empty
            dr.Item("TAX_KB_NM") = String.Empty
            dr.Item("TEKIYO") = String.Empty
            '★ UPD START 2011/09/06 SUGA
            'dr.Item("PRINT_SORT") = "102"
            dr.Item("PRINT_SORT") = LMG520BLC.INJUN_SKYU_ALL_GK
            '★ UPD E N D 2011/09/06 SUGA
            If isPrtChg Then
                dr.Item("SKYU_SUB_NO") = dr.Item("TAX_KB")
            End If

            '2014.08.21 追加START 多通貨対応
            'If drSet.Item("ITEM_CURR_CD").ToString.Equals(LMG520BLC.CURR_NM_JPY) = True Then
            If drSet.Item("ITEM_CURR_CD").ToString.Equals(drSet.Item("SEIQ_CURR_CD").ToString) = False Then
                dr.Item("ITEM_CURR_CD") = String.Empty
            Else
                dr.Item("ITEM_CURR_CD") = drSet.Item("ITEM_CURR_CD").ToString
            End If
            dr.Item("ITEM_ROUND_POS") = Convert.ToDecimal(drSet.Item("ITEM_ROUND_POS"))
            '2014.08.21 追加END 多通貨対応

#If True Then ' 鑑作成区分名表示 20161025 added inoue
            dr.Item("MAKE_SYU_KB") = String.Empty
            dr.Item("TEMPLATE_IMP_FLG") = String.Empty
            If (Me.IsKeiriHikae(Me.midashi(i).ToString())) Then
                dr.Item("KAGAMI_CRT_NM") = kagamiCreateName
            End If
#End If

#If True Then ' 作成者名表示対応 200170420 added by inoue
            dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
            dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
            dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
            dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
            dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
            dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
            dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
            dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
            dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
            dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

            dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

            ' 029595 【LMS】請求書（インボイス）対応　
            dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
            dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
            dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
            dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
            dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
            dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
            dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
            dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
            dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
            dr.Item("T_NO") = drSet.Item("T_NO").ToString()
            dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
            dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

            'データの設定（総額レコード）
            tableNewout.Rows.Add(dr)

            '請求書種の対比
            BkMidashi = Me.midashi(i).ToString()

            '合計額等金額のクリア
            keisanGk = 0
            koteiGk = 0
            NebikiGk = 0
            NebikigoGk = 0

            '2014.08.21 追加START 多通貨対応
            keisanGkSeiq = 0
            NebikiGkSeiq = 0
            NebikigoGkSeiq = 0
            '2014.08.21 追加END 多通貨対応

            kyotu.Clear()
        Next
        Dim outds As DataSet = NewDs.Copy()
        Dim drOut As DataRow() = outds.Tables("LMG520OUT").Select("PRINT_SORT IS NOT NULL", String.Concat("SKYU_SYOSYU_SORT,PRINT_SORT,", If(isPrtChg, "SKYU_SUB_NO,", ""), "TAX_KB"))
        tableNewout.Rows.Clear()
        For i As Integer = 0 To drOut.Length - 1
            tableNewout.ImportRow(drOut(i))
        Next

        Return NewDs

    End Function
    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' データセット設定処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function dataChange(ByVal ds As DataSet, ByVal SetDs As DataSet) As DataSet

        'インプットデータセット設定
        Dim dt As DataTable = ds.Tables("LMG520IN")
        Dim drin As DataRow = dt.Rows(0)

        'DBデータ取得データセット設定
        Dim dtSet As DataTable = SetDs.Tables("LMG520SET")
        Dim drSetcount As Integer = dtSet.Rows.Count
        Dim drSet As DataRow = Nothing
        Dim datas As DataSet = ds.Copy

        'ADD Start 2016/11/25 GROUP_KBを修正（自社・他社・追加データを１件にするため（正・副のため））
        For i As Integer = 0 To SetDs.Tables("LMG520SET").Rows.Count - 1

            If dtSet.Rows(i).Item("GROUP_KB").ToString.Trim = "08" Then
                dtSet.Rows(i).Item("GROUP_KB") = "01"
            ElseIf dtSet.Rows(i).Item("GROUP_KB").ToString.Trim = "09" Then
                dtSet.Rows(i).Item("GROUP_KB") = "02"
            ElseIf dtSet.Rows(i).Item("GROUP_KB").ToString.Trim = "10" Then
                dtSet.Rows(i).Item("GROUP_KB") = "03"
            ElseIf dtSet.Rows(i).Item("GROUP_KB").ToString.Trim = "11" Then
                dtSet.Rows(i).Item("GROUP_KB") = "04"
            ElseIf dtSet.Rows(i).Item("GROUP_KB").ToString.Trim = "12" Then
                dtSet.Rows(i).Item("GROUP_KB") = "05"

            End If

        Next

        Dim dtSetWK As DataTable = SetDs.Tables("LMG520SET").Copy

        Dim drSetWK As DataRow() = SetDs.Tables("LMG520SET").Copy.Select("", "PRINT_SORT,GROUP_KB,TAX_KB,TEKIYO,NEBIKI_RT")
        SetDs.Tables("LMG520SET").Clear()

        For x As Integer = 0 To drSetWK.Length - 1

            SetDs.Tables("LMG520SET").ImportRow(drSetWK(x))

        Next
        'ADD END 2016/11/25

        '出力データ設定
        Dim NewDs As DataSet = SetDs.Copy()
        Dim tableNewout As DataTable = NewDs.Tables("LMG520OUT")
        Dim dr As DataRow = Nothing

        '請求書種判定用
        Dim sSei As String = SEI_NM
        Dim sHuku As String = HUKU_NM

        '帳票出力用固定文言
        Dim asta As String = "*"
        Dim taisyo As String = "対象額"
        Dim MIDASHI As String = String.Empty

        Dim ZeiInJun As String = String.Empty    'プリントソート


        '正・副Sumデータ用
        Dim keisanGk As Decimal = 0              '値引前額
        Dim koteiGk As Decimal = 0               '固定値引額
        Dim NebikiGk As Decimal = 0              '値引額
        Dim NebikigoGk As Decimal = 0            '金額
        Dim Nebiki_Rt As Decimal = 0

        '課税データ設定用
        Dim sKazei As String = String.Empty      '課税区分名称
        Dim kazeinebikirt As Decimal = 0         '課税全体値引率（％）
        Dim kaKoNebiki As Decimal = 0            '固定値引額
        Dim kaNebikigaku As Decimal = 0          '値引額
        Dim kaZei As Decimal = 0                 '金額
        Dim ka As Boolean = False                '課税出力判定

        '免税データ設定用
        Dim sMenzei As String = String.Empty     '課税区分名称
        Dim menzeinebikirt As Decimal = 0        '免税全体値引率（％）
        Dim menKoNebiki As Decimal = 0           '固定値引額
        Dim menNebikigaku As Decimal = 0         '値引額
        Dim menZei As Decimal = 0                '金額
        Dim men As Boolean = False               '免税出力判定

        '非課税データ設定用
        Dim sHikazei As String = String.Empty    '課税区分名称
        Dim hikaZei As Decimal = 0               '金額
        Dim hi As Boolean = False                '非課税出力判定

#If True Then   'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
        '不課税データ設定用
        Dim sFukazei As String = String.Empty    '課税区分名称
        Dim fukaZei As Decimal = 0               '金額
        Dim fu As Boolean = False                '不課税出力判定

        '不課税(立替金)データ設定用
        Dim sFukazeiTatekae As String = String.Empty    '課税区分名称
        Dim fukaZeiTatekae As Decimal = 0               '金額
        Dim fuTatekae As Boolean = False                '不課税出力判定

#End If
        '内税設定用
        Dim sUchizei As String = String.Empty    '課税区分名称
        Dim uchiZei As Decimal = 0               '金額
        Dim uchi As Boolean = False              '内税出力判定

        Dim SumGk As Decimal = 0                 '帳票金額合計用

        '20161213 請求表示用
        Dim seikyuSubNo As String = String.Empty


        '請求書種設定
        Dim sei As String = drin.Item("SEI").ToString()
        Dim huku As String = drin.Item("HUKU").ToString()
        Dim keirihikae As String = drin.Item("KEIRIHIKAE").ToString()
        Dim hikae As String = drin.Item("HIKAE").ToString()

        '枚数・請求書種設定
        Dim maisu As Integer = Me.MidashiHantei(sei, huku, keirihikae, hikae)

        '共通項目設定用リスト
        Dim kyotu As ArrayList = New ArrayList

        'Bkデータ
        Dim BkBusho As String = String.Empty
        Dim BkTaxKb As String = String.Empty
        Dim BkSeiqGroup As String = String.Empty
        Dim BkSeiqCd As String = String.Empty
        Dim BkSeiqNm As String = String.Empty
        Dim BkMidashi As String = String.Empty
        Dim BkTaxKbNm As String = String.Empty
        Dim BkTekiyo As String = String.Empty
        Dim BkPrintSo As String = String.Empty
        ' 2011/10/05 DEL START SBS)SUGA
        'Dim BkMakeSyuKb As String = String.Empty
        ' 2011/10/05 DEL E N D SBS)SUGA

        '20161213 請求サブNo追加(表示順用)
        Dim BkSeikyuSubNo As String = String.Empty

        '★ ADD START 2011/09/06 SUGA
        Dim BkNebikiRt As String = String.Empty
        '★ ADD E N D 2011/09/06 SUGA

#If True Then  ' 鑑作成区分名表示 20161025 added inoue
        Dim kagamiCreateName As String = Me.GetKagamiCreateName(dtSet.Select())
#End If

        ' 請求書出力内容変更 適用年月 (変更後帳票定義ファイル 使用開始年月) 以降の請求であるか否かの判定
        Dim isPrtChg As Boolean = False
        If drSetcount > 0 AndAlso
            dtSet.Rows(0).Item("SKYU_DATE").ToString().Substring(0, 6) >= drin.Item("RPT_CHG_START_YM").ToString() Then
            isPrtChg = True
        End If

        For i As Integer = 0 To drSetcount - 1
            drSet = dtSet.Rows(i)

            drSet.Item("PRINT_SORT") = drSet.Item("PRINT_SORT").ToString().PadLeft(3, Convert.ToChar("0"))

            '課税区分判定
            Select Case drSet.Item("TAX_KB").ToString()

                Case "01"                    '課税
                    If isPrtChg Then
                        Dim Setei As String = Convert.ToString(
                                                  Convert.ToDecimal(
                                                   drSet.Item("TAX_RATE").ToString) * 100)
                        sKazei = String.Concat(Setei.Substring(0, 4), "%")
                    Else
                        sKazei = drSet.Item("TAX_KB_NM").ToString()
                    End If
                    kazeinebikirt = Convert.ToDecimal(drSet.Item("NEBIKI_RT1"))
                    kaKoNebiki = Convert.ToDecimal(drSet.Item("NEBIKI_GK1"))
                    kaZei = kaZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    '★ DEL START 2011/09/06 SUGA
                    'ZeiInJun = "100"
                    '★ DEL E N D 2011/09/06 SUGA
                    ka = True
                Case "02"                    '免税
                    sMenzei = drSet.Item("TAX_KB_NM").ToString()
                    menzeinebikirt = Convert.ToDecimal(drSet.Item("NEBIKI_RT2"))
                    menKoNebiki = Convert.ToDecimal(drSet.Item("NEBIKI_GK2"))
                    menZei = menZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    men = True
                Case "03"                    '非課税
                    sHikazei = drSet.Item("TAX_KB_NM").ToString()
                    hikaZei = hikaZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    hi = True
                Case "04"                    '内税
                    If isPrtChg Then
                        Dim Setei As String = Convert.ToString(
                                                  Convert.ToDecimal(
                                                   drSet.Item("TAX_RATE").ToString) * 100)
                        sUchizei = String.Concat(Setei.Substring(0, 4), "%", "内税")
                    Else
                        sUchizei = drSet.Item("TAX_KB_NM").ToString()
                    End If
                    uchiZei = uchiZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    uchi = True
#If True Then   'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
                Case "17"                   '不課税"
                    sFukazei = drSet.Item("TAX_KB_NM").ToString()
                    fukaZei = fukaZei + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    fu = True
                Case "16"                   '不課税 立替金"
                    sFukazeiTatekae = drSet.Item("TAX_KB_NM").ToString()
                    fukaZeiTatekae = fukaZeiTatekae + Convert.ToDecimal(drSet.Item("KINGAKU"))
                    fuTatekae = True

#End If
            End Select
        Next

        menNebikigaku = ToRoundDown(menzeinebikirt / 100 * menZei) + menKoNebiki
        kaNebikigaku = ToRoundDown(kazeinebikirt / 100 * kaZei) + kaKoNebiki

        '合計額の計算

#If False Then   'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
                SumGk = kaZei + menZei + hikaZei + uchiZei + (Convert.ToDecimal(drSet.Item("TAX_GK1")) 

#End If
        SumGk = kaZei + menZei + hikaZei + uchiZei + fukaZei + fukaZeiTatekae + (Convert.ToDecimal(drSet.Item("TAX_GK1")) _
                                                      + Convert.ToDecimal(drSet.Item("TAX_HASU_GK1"))) _
                                                      - kaNebikigaku - menNebikigaku

        '請求書種用For文
        For i As Integer = 0 To maisu - 1

            'DBデータ用For文
            For j As Integer = 0 To drSetcount - 1

                '出力用データロウ設定
                dr = tableNewout.NewRow()

                drSet = dtSet.Rows(j)

                If j = 0 Then
                    '共通項目の設定
                    Setkyotu(kyotu, drSet, SumGk, i)
                End If
                '請求書種が正・副の場合
                If (sSei.Equals(Me.midashi(i).ToString()) = True _
                Or sHuku.Equals(Me.midashi(i).ToString())) = True Then
                    If j <> 0 Then
                        'Bk見出しと見出しリストのi番目判定
                        If BkMidashi.Equals(Me.midashi(i)) Then

                            'Bkと課税区分・請求項目名称が等しい場合
                            ' 2011/10/05 UPD START SBS)SUGA
                            'If BkTaxKb.Equals(drSet.Item("TAX_KB").ToString) = True _
                            'AndAlso BkSeiqGroup.Equals(drSet.Item("GROUP_KB").ToString) = True _
                            'AndAlso BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True _
                            'AndAlso BkMakeSyuKb.Equals(drSet.Item("MAKE_SYU_KB").ToString) = True _
                            'AndAlso BkNebikiRt.Equals(drSet.Item("NEBIKI_RT").ToString) = True _
                            'AndAlso "01".Equals(BkMakeSyuKb) = False Then
                            '2011/11/21 UPD START SBS)SAGAWA 摘要をグループ化項目に加える
                            'If BkTaxKb.Equals(drSet.Item("TAX_KB").ToString) = True _
                            'AndAlso BkSeiqGroup.Equals(drSet.Item("GROUP_KB").ToString) = True _
                            'AndAlso BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True _
                            'AndAlso BkNebikiRt.Equals(drSet.Item("NEBIKI_RT").ToString) = True _
                            'AndAlso BkPrintSo.Equals(drSet.Item("PRINT_SORT").ToString) = True Then
                            ' 2011/10/05 UPD E N D SBS)SUGA

                            'DEL 2016/11/16 SEIQKMK_CDの条件をとる（自社・他社を1件で処理するため）
                            ''AndAlso BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True _

                            If BkTaxKb.Equals(drSet.Item("TAX_KB").ToString) = True _
                            AndAlso BkSeiqGroup.Equals(drSet.Item("GROUP_KB").ToString) = True _
                            AndAlso ((drSet.Item("GROUP_KB").ToString <= "05") _
                                 Or (drSet.Item("GROUP_KB").ToString > "05" And BkSeiqCd.Equals(drSet.Item("SEIQKMK_CD").ToString) = True)) _
                            AndAlso BkNebikiRt.Equals(drSet.Item("NEBIKI_RT").ToString) = True _
                            AndAlso BkPrintSo.Equals(drSet.Item("PRINT_SORT").ToString) = True _
                            AndAlso BkTekiyo.Equals(drSet.Item("TEKIYO").ToString) Then
                                ' 2011/11/21 UPD E N D SBS)SAGAWA

                                '変数へデータ設定
                                keisanGk = keisanGk + Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                                koteiGk = koteiGk + Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                                NebikiGk = NebikiGk + Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                                NebikigoGk = NebikigoGk + Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                                ' 2011/10/05 DEL START SBS)SUGA
                                'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                                ' 2011/10/05 DEL START SBS)SUGA
                            Else
                                '共通項目データセット設定
                                kyotuSetData(kyotu, dr)

                                'DETAIL
                                '2011/08/15 菱刈 残No11 スタート
                                dr.Item("GROUP_KB") = BkSeiqGroup
                                '2011/08/15 菱刈 残No11 エンド
                                dr.Item("SEIQKMK_NM") = BkSeiqNm
                                dr.Item("TAX_KB") = BkTaxKb
                                dr.Item("TAX_KB_NM") = BkTaxKbNm
                                dr.Item("TEKIYO") = BkTekiyo
                                '★ ADD START 2011/09/06 SUGA
                                dr.Item("NEBIKI_RT") = BkNebikiRt
                                '★ ADD E N D 2011/09/06 SUGA

                                dr.Item("PRINT_SORT") = BkPrintSo

                                'DETAIL見出し設定処理
                                If sHuku.Equals(kyotu(num.SKYU_SYOSYU)) Then

                                    '請求書種が副の場合
                                    dr.Item("DETAIL_MIDASHI") = "行番"
                                Else
                                    '請求書種が副以外の場合
                                    dr.Item("DETAIL_MIDASHI") = "コード"
                                End If

                                '部署コードは常にブランクを設定
                                dr.Item("BUSYO_CD") = String.Empty

                                '請求書合計額の設定
                                dr.Item("SKYU_KINGAKU") = SumGk

                                '値引き前金額の設定
                                dr.Item("KEISAN_TLGK") = keisanGk

                                '計算額が０の場合のシステムエラー回避用
                                If keisanGk < 1 = False Then
                                    'dr.Item("NEBIKI_RT") = (NebikiGk - koteiGk) / keisanGk * 100
                                    '2011/08/15 菱刈 No16 スタート
                                    dr.Item("NEBIKI_RT") = Nebiki_Rt
                                    '2011/08/15 菱刈 No16 エンド
                                Else
                                    dr.Item("NEBIKI_RT") = 0
                                End If

                                '固定・値引額をマイナスに変更する
                                dr.Item("KOTEI_NEBIKI_GK") = koteiGk * -1
                                dr.Item("NEBIKI_GK") = NebikiGk * -1

                                '値引き後金額
                                dr.Item("KINGAKU") = NebikigoGk

                                '表示順用 20161213
                                dr.Item("SKYU_SUB_NO") = BkSeikyuSubNo

#If True Then ' 作成者名表示対応 200170420 added by inoue
                                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If

#If True Then ' QR対応 200170531 added by daikoku
                                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

                                ' 029595 【LMS】請求書（インボイス）対応　
                                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                                'データの設定
                                tableNewout.Rows.Add(dr)

                                '変数へデータ設定
                                BkPrintSo = drSet.Item("PRINT_SORT").ToString()
                                BkTekiyo = drSet.Item("TEKIYO").ToString()
                                keisanGk = Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                                koteiGk = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                                NebikiGk = Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                                NebikigoGk = Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                                ' 2011/10/05 DEL START SBS)SUGA
                                'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                                ' 2011/10/05 DEL E N D SBS)SUGA
                                '20161213
                                BkSeikyuSubNo = drSet.Item("SKYU_SUB_NO").ToString()


                                Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))
                            End If
                        Else

                            '共通項目データセット設定
                            kyotuSetData(kyotu, dr)
                            'DETAIL
                            '2011/08/15 菱刈 残No11 スタート
                            dr.Item("GROUP_KB") = BkSeiqGroup
                            '2011/08/15 菱刈 残No11 エンド
                            dr.Item("SEIQKMK_NM") = BkSeiqNm
                            dr.Item("TAX_KB") = BkTaxKb
                            dr.Item("TAX_KB_NM") = BkTaxKbNm
                            dr.Item("TEKIYO") = BkTekiyo
                            '★ ADD START 2011/09/06 SUGA
                            dr.Item("NEBIKI_RT") = BkNebikiRt
                            '★ ADD E N D 2011/09/06 SUGA

                            dr.Item("PRINT_SORT") = BkPrintSo

                            'DETAIL見出し設定処理
                            If sHuku.Equals(kyotu(num.SKYU_SYOSYU)) Then
                                '請求書種が副の場合
                                dr.Item("DETAIL_MIDASHI") = "行番"
                            Else
                                '請求書種が副以外の場合
                                dr.Item("DETAIL_MIDASHI") = "コード"
                            End If

                            '部署コードは常にブランクを設定
                            dr.Item("BUSYO_CD") = String.Empty

                            '請求書合計額の設定
                            dr.Item("SKYU_KINGAKU") = SumGk

                            '値引き前金額の設定
                            dr.Item("KEISAN_TLGK") = keisanGk

                            '計算額が０の場合のシステムエラー回避用
                            If keisanGk < 1 = False Then
                                'dr.Item("NEBIKI_RT") = (NebikiGk - koteiGk) / keisanGk * 100
                                '2011/08/15 菱刈 No16 スタート
                                dr.Item("NEBIKI_RT") = Nebiki_Rt
                                '2011/08/15 菱刈 No16 エンド
                            Else
                                dr.Item("NEBIKI_RT") = 0
                            End If

                            '固定・値引額をマイナスに変更する
                            dr.Item("KOTEI_NEBIKI_GK") = koteiGk * -1
                            dr.Item("NEBIKI_GK") = NebikiGk * -1

                            '値引き後金額
                            dr.Item("KINGAKU") = NebikigoGk

                            '表示順用 20161213
                            dr.Item("SKYU_SUB_NO") = BkSeikyuSubNo

#If True Then ' 作成者名表示対応 200170420 added by inoue
                            dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If

#If True Then ' QR対応 200170531 added by daikoku
                            dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                            dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                            dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                            dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                            dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                            dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                            dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                            dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                            dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                            dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                            dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                            dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                            dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                            dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                            dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                            dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                            dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                            dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                            dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                            dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                            dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                            dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                            dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                            dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                            dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                            'データの設定
                            tableNewout.Rows.Add(dr)

                            '変数へデータ設定
                            BkTekiyo = drSet.Item("TEKIYO").ToString()
                            keisanGk = Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                            koteiGk = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                            NebikiGk = Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                            NebikigoGk = Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                            ' 2011/10/05 DEL START SBS)SUGA
                            'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                            ' 2011/10/05 DEL E N D SBS)SUGA
                            Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))

                            '20161213
                            BkSeikyuSubNo = drSet.Item("SKYU_SUB_NO").ToString()

                        End If
                    Else

                        '変数へデータ設定
                        keisanGk = keisanGk + Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                        koteiGk = koteiGk + Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                        NebikiGk = NebikiGk + Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                        NebikigoGk = NebikigoGk + Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                        ' 2011/10/05 DEL START SBS)SUGA
                        'BkMakeSyuKb = drSet.Item("MAKE_SYU_KB").ToString
                        ' 2011/10/05 DEL E N D SBS)SUGA

                        BkTekiyo = drSet.Item("TEKIYO").ToString()
                        BkPrintSo = drSet.Item("PRINT_SORT").ToString()
                        Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))
                        '20161213
                        BkSeikyuSubNo = drSet.Item("SKYU_SUB_NO").ToString()

                    End If
                Else
                    '請求諸種が控・経理部控の場合

                    '共通項目データセット設定
                    kyotuSetData(kyotu, dr)


                    'DETAIL
                    dr.Item("DETAIL_MIDASHI") = "コード"
                    dr.Item("BUSYO_CD") = drSet.Item("BUSYO_CD").ToString
                    dr.Item("SKYU_KINGAKU") = SumGk

                    '20160707 経理部控　勘定区分対応
                    'GROUP_KBがastariskの場合は表示しない
                    dr.Item("KANJO") = drSet.Item("KANJO").ToString()
                    '20160707 経理部控　勘定区分対応
                    dr.Item("KANJO_NM") = drSet.Item("KANJO_NM").ToString()

                    '2011/08/15 菱刈 残No11 スタート
                    dr.Item("GROUP_KB") = drSet.Item("GROUP_KB").ToString
                    '2011/08/15 菱刈 残No11 エンド

                    dr.Item("BUSYO_CD") = drSet.Item("BUSYO_CD").ToString
                    dr.Item("SEIQKMK_NM") = drSet.Item("SEIQKMK_NM").ToString
                    dr.Item("KEISAN_TLGK") = drSet.Item("KEISAN_TLGK").ToString
                    dr.Item("NEBIKI_RT") = dtSet.Rows(j).Item("NEBIKI_RT").ToString
                    dr.Item("KOTEI_NEBIKI_GK") = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK")) * -1
                    dr.Item("NEBIKI_GK") = Convert.ToDecimal(drSet.Item("NEBIKI_GK")) * -1
                    dr.Item("KINGAKU") = drSet.Item("KINGAKU")
                    dr.Item("TAX_KB") = drSet.Item("TAX_KB").ToString
                    dr.Item("TAX_KB_NM") = drSet.Item("TAX_KB_NM").ToString
                    dr.Item("TEKIYO") = drSet.Item("TEKIYO").ToString

                    dr.Item("PRINT_SORT") = drSet.Item("PRINT_SORT").ToString
                    '20161213
                    dr.Item("SKYU_SUB_NO") = drSet.Item("SKYU_SUB_NO").ToString

                    'If (Me.IsKeiriHikae(Me.midashi(i).ToString())) Then
                    '    dr.Item("MAKE_SYU_KB") = drSet.Item("MAKE_SYU_KB").ToString
                    '    dr.Item("TEMPLATE_IMP_FLG") = drSet.Item("TEMPLATE_IMP_FLG").ToString
                    'End If
                    '後の集計処理の都合上、控であっても値はセットする（集計処理の最後でクリアする）
                    dr.Item("MAKE_SYU_KB") = drSet.Item("MAKE_SYU_KB").ToString
                    dr.Item("TEMPLATE_IMP_FLG") = drSet.Item("TEMPLATE_IMP_FLG").ToString

#If True Then ' 作成者名表示対応 200170420 added by inoue
                    dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                    dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                    dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                    dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                    dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                    dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                    dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                    dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                    dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                    dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                    dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                    dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                    dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                    dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                    dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                    dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                    dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                    dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                    dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                    dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                    dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                    dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                    dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                    dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                    dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                    dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                    'データの設定
                    tableNewout.Rows.Add(dr)

                    '変数
                    keisanGk = Convert.ToDecimal(drSet.Item("KEISAN_TLGK"))    '値引前額
                    koteiGk = Convert.ToDecimal(drSet.Item("KOTEI_NEBIKI_GK"))  '固定値引額
                    NebikiGk = Convert.ToDecimal(drSet.Item("NEBIKI_GK"))      '値引額
                    NebikigoGk = Convert.ToDecimal(drSet.Item("KINGAKU"))    '金額
                    Nebiki_Rt = Convert.ToDecimal(drSet.Item("NEBIKI_RT"))

                End If

                '退避用データの設定
                BkMidashi = Me.midashi(i).ToString()
                BkSeiqGroup = drSet.Item("GROUP_KB").ToString()
                BkSeiqCd = drSet.Item("SEIQKMK_CD").ToString()
                BkSeiqNm = drSet.Item("SEIQKMK_NM").ToString()
                BkBusho = drSet.Item("BUSYO_CD").ToString()
                BkTaxKb = drSet.Item("TAX_KB").ToString()
                BkTaxKbNm = drSet.Item("TAX_KB_NM").ToString()
                '★ ADD START 2011/09/06 SUGA
                BkNebikiRt = drSet.Item("NEBIKI_RT").ToString()
                '★ ADD E N D 2011/09/06 SUGA
                ' 2011/10/05 DEL START SBS)SUGA
                BkPrintSo = drSet.Item("PRINT_SORT").ToString()
                ' 2011/10/05 DEL E N D SBS)SUGA
            Next

            '出力用データロウ設定
            dr = tableNewout.NewRow()

            '請求書種が正・副の場合はデータを設定
            If sSei.Equals(Me.midashi(i).ToString()) _
            Or sHuku.Equals(Me.midashi(i).ToString()) Then

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                'DETAIL
                '2011/08/15 菱刈 残No11 スタート
                dr.Item("GROUP_KB") = BkSeiqGroup
                '2011/08/15 菱刈 残No11 エンド
                dr.Item("SEIQKMK_NM") = BkSeiqNm
                dr.Item("TAX_KB") = BkTaxKb
                dr.Item("TAX_KB_NM") = BkTaxKbNm
                dr.Item("TEKIYO") = BkTekiyo
                '★ ADD START 2011/09/06 SUGA
                dr.Item("NEBIKI_RT") = BkNebikiRt
                '★ ADD E N D 2011/09/06 SUGA

                dr.Item("PRINT_SORT") = drSet.Item("PRINT_SORT").ToString


                '見出し・部署コード設定
                If sHuku.Equals(kyotu(num.SKYU_SYOSYU)) Then

                    dr.Item("DETAIL_MIDASHI") = "行番"
                Else
                    dr.Item("DETAIL_MIDASHI") = "コード"
                End If
                dr.Item("BUSYO_CD") = String.Empty

                dr.Item("KEISAN_TLGK") = keisanGk

                If keisanGk < 1 = False Then
                    'dr.Item("NEBIKI_RT") = (NebikiGk - koteiGk) / keisanGk * 100
                    '2011/08/15 菱刈 No16 スタート
                    dr.Item("NEBIKI_RT") = Nebiki_Rt
                    '2011/08/15 菱刈 No16 エンド
                End If

                dr.Item("KOTEI_NEBIKI_GK") = koteiGk * -1
                dr.Item("NEBIKI_GK") = NebikiGk * -1
                dr.Item("KINGAKU") = NebikigoGk

                '20161213
                dr.Item("SKYU_SUB_NO") = BkSeikyuSubNo

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

#End If

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '請求書種が控の場合、セグメントで分割された明細を集計する（ABP連携によりセグメントが追加される前のデータ構成を再現）
            Dim nowMidashi As String = Me.midashi(i).ToString()
            If HIKAE_NM.Equals(nowMidashi) Then
                'データ内容を一旦退避してクリア
                Dim cpyTableNewout As DataTable = tableNewout.Copy()
                tableNewout.Clear()

                '退避データのループ
                For iCpy As Integer = 0 To cpyTableNewout.Rows.Count - 1
                    Dim drCopy As DataRow = cpyTableNewout.Rows(iCpy)

                    '集計が必要なデータか判定
                    '　請求書種が処理中と一致
                    '　手動追加ではない
                    '　テンプレートではない
                    '　合計行ではない
                    If (nowMidashi.Equals(drCopy.Item("SKYU_SYOSYU").ToString)) _
                            AndAlso (Not MAKE_SYU_KB.ADD.Equals(drCopy.Item("MAKE_SYU_KB").ToString)) _
                            AndAlso (Not TEMPLATE_IMP_FLG.TEMPLATE.Equals(drCopy.Item("TEMPLATE_IMP_FLG").ToString)) _
                            AndAlso (Not asta.Equals(drCopy.Item("GROUP_KB").ToString)) Then
                        '集計が必要
                        '集計キーに一致するデータがすでに戻されているか調べる
                        Dim sCpyKey As String = String.Concat(
                                drCopy.Item("SKYU_SYOSYU").ToString,
                                drCopy.Item("GROUP_KB").ToString,
                                drCopy.Item("TAX_KB").ToString,
                                drCopy.Item("KANJO").ToString,
                                drCopy.Item("BUSYO_CD").ToString,
                                drCopy.Item("NEBIKI_RT").ToString,
                                drCopy.Item("TEKIYO").ToString
                                )
                        Dim iIdx As Integer = -1

                        For iNew As Integer = 0 To tableNewout.Rows.Count - 1
                            Dim drNew As DataRow = tableNewout.Rows(iNew)
                            Dim sNewKey As String = String.Concat(
                                    drNew.Item("SKYU_SYOSYU").ToString,
                                    drNew.Item("GROUP_KB").ToString,
                                    drNew.Item("TAX_KB").ToString,
                                    drNew.Item("KANJO").ToString,
                                    drNew.Item("BUSYO_CD").ToString,
                                    drNew.Item("NEBIKI_RT").ToString,
                                    drNew.Item("TEKIYO").ToString
                                    )

                            If sNewKey.Equals(sCpyKey) Then
                                '集計キーに一致するデータがあった
                                iIdx = iNew
                                Exit For
                            End If
                        Next

                        '集計判定
                        If iIdx = -1 Then
                            '今回の集計キーデータは初なのでそのまま戻す
                            tableNewout.ImportRow(drCopy)
                        Else
                            'すでに戻されている同集計キーのデータに加算する（符号反転している項目は一旦戻してから加算）
                            tableNewout.Rows(iIdx).Item("KEISAN_TLGK") = Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KEISAN_TLGK")) + Convert.ToDecimal(drCopy.Item("KEISAN_TLGK"))
                            tableNewout.Rows(iIdx).Item("KOTEI_NEBIKI_GK") = (Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KOTEI_NEBIKI_GK")) * -1 + Convert.ToDecimal(drCopy.Item("KOTEI_NEBIKI_GK"))) * -1
                            tableNewout.Rows(iIdx).Item("NEBIKI_GK") = (Convert.ToDecimal(tableNewout.Rows(iIdx).Item("NEBIKI_GK")) * -1 + Convert.ToDecimal(drCopy.Item("NEBIKI_GK"))) * -1
                            tableNewout.Rows(iIdx).Item("KINGAKU") = Convert.ToDecimal(tableNewout.Rows(iIdx).Item("KINGAKU")) + Convert.ToDecimal(drCopy.Item("KINGAKU"))
                        End If
                    Else
                        '集計は不要
                        'データをそのまま戻す
                        tableNewout.ImportRow(drCopy)
                    End If
                Next

                'この集計処理の都合でセットしてあった項目値をクリアする
                For iNew As Integer = 0 To tableNewout.Rows.Count - 1
                    Dim drNew As DataRow = tableNewout.Rows(iNew)

                    If HIKAE_NM.Equals(drNew.Item("SKYU_SYOSYU").ToString) Then
                        drNew.Item("MAKE_SYU_KB") = String.Empty
                        drNew.Item("TEMPLATE_IMP_FLG") = String.Empty
                    End If
                Next
            End If

            If sHuku.Equals(Me.midashi(i).ToString()) = True OrElse
            sSei.Equals(Me.midashi(i).ToString()) = True Then
                MIDASHI = "行番"
            Else
                MIDASHI = "コード"
            End If

            '課税設定（課税レコードが存在する場合設定）
            If ka = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString
                dr.Item("DETAIL_MIDASHI") = MIDASHI
                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sKazei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = kazeinebikirt
                dr.Item("KOTEI_NEBIKI_GK") = kaKoNebiki * -1
                dr.Item("NEBIKI_GK") = kaNebikigaku * -1
                dr.Item("KINGAKU") = kaZei + (kaNebikigaku * -1)
                dr.Item("TAX_KB") = "01"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = String.Empty
                If isPrtChg Then
                    ' 請求書出力内容変更 適用年月 以降の場合

                    ' 課税(と後出の 内税・内税額) は、
                    ' 免税・非課税・不課税・不課税(立替金) より後(従来の税額 に続く位置) の出力とする。
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
                    ' 上記で従来の税額の出力位置とした一連の行は、
                    ' 課税、税額、内税、内税額 の出力順とする。
                    ' (課税、内税　 の SKYU_SUB_NO は TAX_KB  と同じ値に、
                    '  税額、内税額 の SKYU_SUB_NO はそれぞれ 課税、内税 の TAX_KB と同じ値とし、
                    '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                    dr.Item("SKYU_SUB_NO") = dr.Item("TAX_KB")
                Else
                    '★ UPD START 2011/09/06 SUGA
                    'dr.Item("PRINT_SORT") = ZeiInJun
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                    '★ UPD E N D 2011/09/06 SUGA
                    '20161213
                    dr.Item("SKYU_SUB_NO") = String.Empty
                End If

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '免税設定（免税レコードが存在する場合設定）
            If men = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI
                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sMenzei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = menzeinebikirt
                dr.Item("KOTEI_NEBIKI_GK") = menKoNebiki * -1
                dr.Item("NEBIKI_GK") = menNebikigaku * -1
                dr.Item("KINGAKU") = menZei + (menNebikigaku * -1)
                dr.Item("TAX_KB") = "02"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = String.Empty
                '★ UPD START 2011/09/06 SUGA
                'dr.Item("PRINT_SORT") = ZeiInJun
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                '★ UPD E N D 2011/09/06 SUGA

                '20161213
                dr.Item("SKYU_SUB_NO") = String.Empty

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

#End If

                tableNewout.Rows.Add(dr)

            End If

            '非課税（非課税レコードが存在する場合設定）
            If hi = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sHikazei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                dr.Item("KINGAKU") = hikaZei
                dr.Item("TAX_KB") = "03"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                '★ UPD START 2011/09/06 SUGA
                'dr.Item("PRINT_SORT") = ZeiInJun
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                '★ UPD E N D 2011/09/06 SUGA

                '20161213
                dr.Item("SKYU_SUB_NO") = String.Empty

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

#If True Then   'ADD 2020/01/29 009561【LMS】【JDE】調査_請求_立替金社外・不課税で登録できるようにしたい
            '不課税（不課税レコードが存在する場合設定）
            If fu = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sFukazei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                dr.Item("KINGAKU") = fukaZei
                dr.Item("TAX_KB") = "18"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK

                dr.Item("SKYU_SUB_NO") = String.Empty

                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼
#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '不課税（立替金）
            If fuTatekae = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sFukazeiTatekae, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                dr.Item("KINGAKU") = fukaZeiTatekae
                dr.Item("TAX_KB") = "18"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK

                dr.Item("SKYU_SUB_NO") = String.Empty

                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

#End If
            '内税（内税レコードが存在する場合設定）
            If uchi = True Then
                dr = tableNewout.NewRow()

                '共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString

                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta
                dr.Item("SEIQKMK_NM") = String.Concat(sUchizei, taisyo)
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                dr.Item("KINGAKU") = uchiZei
                dr.Item("TAX_KB") = "03"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = ""
                If isPrtChg Then
                    ' 請求書出力内容変更 適用年月 以降の場合

                    ' (前出の 課税および) 内税(と後出の 内税額) は、
                    ' 免税・非課税・不課税・不課税(立替金) より後(従来の税額 に続く位置) の出力とする。
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
                    ' 上記で従来の税額の出力位置とした一連の行は、
                    ' 課税、税額、内税、内税額 の出力順とする。
                    ' (課税、内税　 の SKYU_SUB_NO は TAX_KB  と同じ値に、
                    '  税額、内税額 の SKYU_SUB_NO はそれぞれ課税、内税 の TAX_KB と同じ値とし、
                    '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                    dr.Item("SKYU_SUB_NO") = dr.Item("TAX_KB")
                Else
                    '★ UPD START 2011/09/06 SUGA
                    'dr.Item("PRINT_SORT") = ZeiInJun
                    dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_BETSU_TAISYO_GK
                    '★ UPD E N D 2011/09/06 SUGA

                    '20161213
                    dr.Item("SKYU_SUB_NO") = String.Empty
                End If

#If True Then ' 作成者名表示対応 200170420 added by inoue
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

                'データの設定
                tableNewout.Rows.Add(dr)

            End If

            '税額レコードの作成
            dr = tableNewout.NewRow()

            '共通項目データセット設定
            kyotuSetData(kyotu, dr)
            dr.Item("SKYU_KINGAKU") = SumGk
            dr.Item("REMARK") = drSet.Item("REMARK").ToString
            dr.Item("DETAIL_MIDASHI") = MIDASHI

            dr.Item("GROUP_KB") = asta
            'dr.Item("SEIQKMK_NM") = String.Concat("税額（", Convert.ToString( _
            'Convert.ToDecimal( _
            'drSet.Item("TAX_RATE").ToString) * 100), "%)")
            Dim Setei As String = Convert.ToString(
                                                  Convert.ToDecimal(
                                                   drSet.Item("TAX_RATE").ToString) * 100)

            If isPrtChg Then
                dr.Item("SEIQKMK_NM") = String.Concat(Setei.Substring(0, 4), "%", "消費税")
            Else
                dr.Item("SEIQKMK_NM") = String.Concat("税額（", Setei.Substring(0, 4), "%)")
            End If
            dr.Item("BUSYO_CD") = String.Empty
            dr.Item("KEISAN_TLGK") = String.Empty
            dr.Item("NEBIKI_RT") = String.Empty
            dr.Item("KOTEI_NEBIKI_GK") = String.Empty
            dr.Item("NEBIKI_GK") = String.Empty
            dr.Item("KINGAKU") = Convert.ToDecimal(drSet.Item("TAX_GK1")) _
                               + Convert.ToDecimal(drSet.Item("TAX_HASU_GK1"))
            If isPrtChg Then
                ' 請求書出力内容変更 適用年月 以降の場合

                ' 設定値については、下記 SKYU_SUB_NO 設定箇所のコメント参照
                dr.Item("TAX_KB") = "99"
            Else
                dr.Item("TAX_KB") = String.Empty
            End If
            dr.Item("TAX_KB_NM") = String.Empty
            dr.Item("TEKIYO") = String.Empty
            '★ UPD START 2011/09/06 SUGA
            'dr.Item("PRINT_SORT") = "101"
            dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
            '★ UPD START 2011/09/06 SUGA

            If isPrtChg Then
                ' 請求書出力内容変更 適用年月 以降の場合

                ' 前出の 課税・内税および後出の 内税額 は、
                ' 税額(本レコード) に続く位置 の出力とするよう、PRINT_SORT は税額(本レコード) と同値としてある。
                ' この一連の行は、
                ' 課税、税額、内税、内税額 の出力順とする。
                ' (課税、内税 　の SKYU_SUB_NO は TAX_KB  と同じ値に、
                '  税額、内税額 の SKYU_SUB_NO はそれぞれ 課税、内税 の TAX_KB と同じ値とし、
                '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                dr.Item("SKYU_SUB_NO") = "01"
            Else
                '20161213
                dr.Item("SKYU_SUB_NO") = String.Empty
            End If

#If True Then ' 作成者名表示対応 200170420 added by inoue
            dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
            dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
            dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
            dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
            dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
            dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
            dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
            dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
            dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
            dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

            dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
            dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
            dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
            dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
            dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
            dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
            dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
            dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
            dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
            dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
            dr.Item("T_NO") = drSet.Item("T_NO").ToString()
            dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
            dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

            'データの設定（税額レコード）
            tableNewout.Rows.Add(dr)

            If isPrtChg AndAlso uchi Then
                ' 内税額レコードの作成
                dr = tableNewout.NewRow()

                Dim taxRate As Decimal = Convert.ToDecimal(drSet.Item("TAX_RATE").ToString())

                ' 共通項目データセット設定
                kyotuSetData(kyotu, dr)
                dr.Item("SKYU_KINGAKU") = SumGk
                dr.Item("REMARK") = drSet.Item("REMARK").ToString
                dr.Item("DETAIL_MIDASHI") = MIDASHI

                dr.Item("GROUP_KB") = asta

                Setei = Convert.ToString(taxRate * 100)
                dr.Item("SEIQKMK_NM") = String.Concat(Setei.Substring(0, 4), "%", "内税額")
                dr.Item("BUSYO_CD") = String.Empty
                dr.Item("KEISAN_TLGK") = String.Empty
                dr.Item("NEBIKI_RT") = String.Empty
                dr.Item("KOTEI_NEBIKI_GK") = String.Empty
                dr.Item("NEBIKI_GK") = String.Empty
                dr.Item("KINGAKU") = Math.Truncate(uchiZei * taxRate / (taxRate + 1))
                ' 設定値については、下記 PRINT_SORT, SKYU_SUB_NO 設定箇所のコメント参照
                dr.Item("TAX_KB") = "99"
                dr.Item("TAX_KB_NM") = String.Empty
                dr.Item("TEKIYO") = String.Empty
                ' (前出の課税・内税および) 内税額 は、
                ' 請求書出力内容変更 適用年月 以降の場合、
                ' 税額 に続く位置 の出力とするよう、PRINT_SORT は税額 と同値としてある。
                ' この一連の行は、
                ' 課税、税額、内税、内税額 の出力順とする。
                ' (課税、内税 　の SKYU_SUB_NO は TAX_KB  と同じ値に、
                '  税額、内税額 の SKYU_SUB_NO はそれぞれ 課税、内税 の TAX_KB と同じ値とし、
                '  税額、内税額 の TAX_KB は "99" として、「課税、税額」「内税、内税額」の順の出力となるようにする)
                dr.Item("PRINT_SORT") = LMG520BLC.INJUN_TAX_GK
                dr.Item("SKYU_SUB_NO") = "03"

                ' 作成者名表示対応
                dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()

                ' QR対応
                dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
                dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
                dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
                dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
                dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
                dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()

                dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()       ' 振込手数料を含む記載
                dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()       ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()     ' 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
                dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

                dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()           ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入
                dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()         ' 014230   【LMS】請求書送付時の窓付き封筒の導入

                dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()           ' 023615   【LMS_RT】請求書に部署名の追加依頼

                ' 029595 【LMS】請求書（インボイス）対応　
                dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
                dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
                dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
                dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
                dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
                dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
                dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
                dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
                dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
                dr.Item("T_NO") = drSet.Item("T_NO").ToString()
                dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
                dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()

                ' データの設定（内税額レコード）
                tableNewout.Rows.Add(dr)
            End If

            '請求総額レコードの作成
            dr = tableNewout.NewRow()

            '共通項目データセット設定
            kyotuSetData(kyotu, dr)
            dr.Item("SKYU_KINGAKU") = SumGk
            dr.Item("REMARK") = drSet.Item("REMARK").ToString
            dr.Item("DETAIL_MIDASHI") = MIDASHI

            dr.Item("GROUP_KB") = asta
            dr.Item("SEIQKMK_NM") = "請求総額"
            dr.Item("BUSYO_CD") = String.Empty
            dr.Item("KEISAN_TLGK") = String.Empty
            dr.Item("NEBIKI_RT") = String.Empty
            dr.Item("KOTEI_NEBIKI_GK") = String.Empty
            dr.Item("NEBIKI_GK") = String.Empty
            dr.Item("KINGAKU") = SumGk
            dr.Item("TAX_KB") = String.Empty
            dr.Item("TAX_KB_NM") = String.Empty
            dr.Item("TEKIYO") = String.Empty
            '★ UPD START 2011/09/06 SUGA
            'dr.Item("PRINT_SORT") = "102"
            dr.Item("PRINT_SORT") = LMG520BLC.INJUN_SKYU_ALL_GK
            '★ UPD E N D 2011/09/06 SUGA

            '20161213
            dr.Item("SKYU_SUB_NO") = String.Empty

#If True Then ' 鑑作成区分名表示 20161025 added inoue

            dr.Item("MAKE_SYU_KB") = String.Empty

            If (Me.IsKeiriHikae(Me.midashi(i).ToString())) Then
                dr.Item("KAGAMI_CRT_NM") = kagamiCreateName
            End If
#End If

#If True Then ' 作成者名表示対応 200170420 added by inoue
            dr.Item("KAGAMI_ENT_USER_NM") = drSet.Item("KAGAMI_ENT_USER_NM").ToString()
#End If
#If True Then ' QR対応 200170531 added by daikoku
            dr.Item("JDE_CD") = drSet.Item("JDE_CD").ToString()
            dr.Item("QR_SYSTEM_ID") = drSet.Item("QR_SYSTEM_ID").ToString()
            dr.Item("QR_SYSTEM_ID_NM") = drSet.Item("QR_SYSTEM_ID_NM").ToString()
            dr.Item("QR_REC_TYP") = drSet.Item("QR_REC_TYP").ToString()
            dr.Item("QR_REC_TYP_NM") = drSet.Item("QR_REC_TYP_NM").ToString()
            dr.Item("QR_FOLDER") = drSet.Item("QR_FOLDER").ToString()
#End If
            dr.Item("KBNG014_FLG") = drSet.Item("KBNG014_FLG").ToString()   'ADD 2020/09/15振込手数料を含む記載
            dr.Item("DOC_DEST_YN") = drSet.Item("DOC_DEST_YN").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("ATENA_RPR_ID") = drSet.Item("ATENA_RPR_ID").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("SEIQTO_ZIP") = drSet.Item("SEIQTO_ZIP").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD1") = drSet.Item("SEIQTO_AD1").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD2") = drSet.Item("SEIQTO_AD2").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_AD3") = drSet.Item("SEIQTO_AD3").ToString()   'ADD 2020/09/29 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("SEIQTO_BUSYO_NM") = drSet.Item("SEIQTO_BUSYO_NM").ToString()
            dr.Item("SEIQTO_OYA_PIC") = drSet.Item("SEIQTO_OYA_PIC").ToString()

            dr.Item("NRS_BR_NM") = drSet.Item("NRS_BR_NM").ToString()     'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_ZIP") = drSet.Item("NRS_BR_ZIP").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD1") = drSet.Item("NRS_BR_AD1").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD2") = drSet.Item("NRS_BR_AD2").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入
            dr.Item("NRS_BR_AD3") = drSet.Item("NRS_BR_AD3").ToString()   'ADD 2020/09/30 014230   【LMS】請求書送付時の窓付き封筒の導入

            dr.Item("NRS_BR_CD") = drSet.Item("NRS_BR_CD").ToString()   'ADD 2021/09/03 023615   【LMS_RT】請求書に部署名の追加依頼

#If True Then   'ADD 2022/07/01 029595 【LMS】請求書（インボイス）対応　
            dr.Item("BANK_NM1") = drSet.Item("BANK_NM1").ToString()
            dr.Item("BANK_BR1") = drSet.Item("BANK_BR1").ToString()
            dr.Item("BANK_ACC1") = drSet.Item("BANK_ACC1").ToString()
            dr.Item("BANK_NM2") = drSet.Item("BANK_NM2").ToString()
            dr.Item("BANK_BR2") = drSet.Item("BANK_BR2").ToString()
            dr.Item("BANK_ACC2") = drSet.Item("BANK_ACC2").ToString()
            dr.Item("BANK_NM3") = drSet.Item("BANK_NM3").ToString()
            dr.Item("BANK_BR3") = drSet.Item("BANK_BR3").ToString()
            dr.Item("BANK_ACC3") = drSet.Item("BANK_ACC3").ToString()
            dr.Item("T_NO") = drSet.Item("T_NO").ToString()
            dr.Item("BANK__NM0") = drSet.Item("BANK__NM0").ToString()
            dr.Item("FURIKOMI_MEMO") = drSet.Item("FURIKOMI_MEMO").ToString()
#End If

            'データの設定（総額レコード）
            tableNewout.Rows.Add(dr)

            '請求書種の対比
            BkMidashi = Me.midashi(i).ToString()

            '合計額等金額のクリア
            keisanGk = 0
            koteiGk = 0
            NebikiGk = 0
            NebikigoGk = 0

            kyotu.Clear()
        Next
        Dim outds As DataSet = NewDs.Copy()
        Dim drOut As DataRow() = outds.Tables("LMG520OUT").Select("PRINT_SORT IS NOT NULL", "SKYU_SYOSYU_SORT,PRINT_SORT,SKYU_SUB_NO,TAX_KB")
        tableNewout.Rows.Clear()
        For i As Integer = 0 To drOut.Length - 1
            tableNewout.ImportRow(drOut(i))
        Next

        Return NewDs

    End Function


#If True Then ' 鑑作成区分名表示 20161025 added inoue

    ''' <summary>
    ''' 鑑作成区分名取得
    ''' </summary>
    ''' <param name="rows"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKagamiCreateName(ByVal rows As DataRow()) As String

        Dim createName As String = String.Empty

        If (rows IsNot Nothing AndAlso rows.Count > 0) Then

            If (KABAMI_CRT_KB.MANUAL.Equals(rows(0).Item("CRT_KB"))) Then
                ' 手書き請求書
                createName = KAGAMI_CRT_NM.MANUAL
            Else

                If ((rows.Where(Function(row) MAKE_SYU_KB.ADD.Equals(row.Item("MAKE_SYU_KB"))).Count > 0)) Then

                    ' 半自動
                    createName = KAGAMI_CRT_NM.SEMI_AUTO
                    '20161206 テンプレート使用時は半自動表記 
                ElseIf ((rows.Where(Function(row) TEMPLATE_IMP_FLG.TEMPLATE.Equals(row.Item("TEMPLATE_IMP_FLG"))).Count > 0)) Then
                    ' 半自動
                    createName = KAGAMI_CRT_NM.SEMI_AUTO

                    '20161206 end
                Else

                    ' 全自動
                    createName = KAGAMI_CRT_NM.AUTO
                End If

            End If
        End If
        Return createName
    End Function


    Private Function IsKeiriHikae(ByVal midashi As String) As Boolean
        Return KEIRIHIKAE_NM.Equals(midashi)
    End Function


#End If


    ''' <summary>
    ''' 切捨処理
    ''' </summary>
    ''' <param name="dValue"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToRoundDown(ByVal dValue As Double) As Decimal
        Dim decima As Decimal = 0

        Dim dCoef As Double = System.Math.Pow(10, 0)

        If dValue > 0 Then
            decima = Convert.ToDecimal(System.Math.Floor(dValue * dCoef) / dCoef)
        Else
            decima = Convert.ToDecimal(System.Math.Ceiling(dValue * dCoef) / dCoef)
        End If

        Return decima
    End Function


    ''' <summary>
    ''' 見出し判定用
    ''' </summary>
    ''' <param name="sei"></param>
    ''' <param name="huku"></param>
    ''' <param name="keirihikae"></param>
    ''' <param name="hikae"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function MidashiHantei(ByVal sei As String _
                                   , ByVal huku As String _
                                   , ByVal keirihikae As String _
                                   , ByVal hikae As String) As Integer

        If "1".Equals(sei) = True Then
            Me.midashi.Add(SEI_NM)
        End If

        If "1".Equals(huku) = True Then
            Me.midashi.Add(HUKU_NM)
        End If

        If "1".Equals(hikae) = True Then
            Me.midashi.Add(HIKAE_NM)
        End If

        If "1".Equals(keirihikae) = True Then
            Me.midashi.Add(KEIRIHIKAE_NM)
        End If


        '帳票枚数判定
        Dim maisu As Integer = Convert.ToInt32(sei) + Convert.ToInt32(huku) + _
                              Convert.ToInt32(hikae) + Convert.ToInt32(keirihikae)

        Return maisu

    End Function

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 帳票内共通項目設定
    ''' </summary>
    ''' <param name="kyotu"></param>
    ''' <param name="drSet"></param>
    ''' <param name="SumGk"></param>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetkyotuExchange(ByVal kyotu As ArrayList, ByVal drSet As DataRow, ByVal SumGk As Decimal, ByVal i As Integer) As ArrayList

        kyotu.Add(drSet.Item("RPT_ID").ToString())
        kyotu.Add(drSet.Item("SEIQTO_NM").ToString())
        kyotu.Add(drSet.Item("SEIQTO_PIC").ToString())
        kyotu.Add(drSet.Item("SKYU_NO").ToString())
        kyotu.Add(drSet.Item("SKYU_DATE").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_NM").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_ADD1").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_ADD2").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_TEL").ToString())
        kyotu.Add(i)
        kyotu.Add(Me.midashi(i).ToString())
        kyotu.Add(drSet.Item("SKYU_MONTH").ToString())
        kyotu.Add(SumGk)
        kyotu.Add(drSet.Item("REMARK").ToString())
        kyotu.Add(drSet.Item("BANK_NM").ToString())
        kyotu.Add(drSet.Item("MEIGI_NM").ToString())
        kyotu.Add(drSet.Item("YOKIN_INFO").ToString())
        kyotu.Add(drSet.Item("TAX_HASU_GK1").ToString())
        '2014.08.21 追加START 多通貨対応
        kyotu.Add(Convert.ToDecimal(drSet.Item("EX_RATE")))
        kyotu.Add(drSet.Item("EX_MOTO_CURR_CD").ToString())
        kyotu.Add(drSet.Item("EX_SAKI_CURR_CD").ToString())
        kyotu.Add(drSet.Item("SEIQ_CURR_CD").ToString())
        kyotu.Add(Convert.ToDecimal(drSet.Item("SEIQ_ROUND_POS")))
        '2014.08.21 追加END 多通貨対応

        Return kyotu

    End Function
    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' 帳票内共通項目設定
    ''' </summary>
    ''' <param name="kyotu"></param>
    ''' <param name="drSet"></param>
    ''' <param name="SumGk"></param>
    ''' <param name="i"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function Setkyotu(ByVal kyotu As ArrayList, ByVal drSet As DataRow, ByVal SumGk As Decimal, ByVal i As Integer) As ArrayList

        kyotu.Add(drSet.Item("RPT_ID").ToString())
        kyotu.Add(drSet.Item("SEIQTO_NM").ToString())
        kyotu.Add(drSet.Item("SEIQTO_PIC").ToString())
        kyotu.Add(drSet.Item("SKYU_NO").ToString())
        kyotu.Add(drSet.Item("SKYU_DATE").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_NM").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_ADD1").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_ADD2").ToString())
        kyotu.Add(drSet.Item("HAKKO_MOTO_TEL").ToString())
        kyotu.Add(i)
        kyotu.Add(Me.midashi(i).ToString())
        kyotu.Add(drSet.Item("SKYU_MONTH").ToString())
        kyotu.Add(SumGk)
        kyotu.Add(drSet.Item("REMARK").ToString())
        kyotu.Add(drSet.Item("BANK_NM").ToString())
        kyotu.Add(drSet.Item("MEIGI_NM").ToString())
        kyotu.Add(drSet.Item("YOKIN_INFO").ToString())
        kyotu.Add(drSet.Item("TAX_HASU_GK1").ToString())

        Return kyotu

    End Function

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 共通項目のデータセット設定
    ''' </summary>
    ''' <param name="kyotu"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Function kyotuSetDataExchange(ByVal kyotu As ArrayList, ByVal dr As DataRow) As DataRow

        dr.Item("RPT_ID") = kyotu(numExchange.RPT_ID)
        'PAGEHEADER
        dr.Item("SEIQTO_NM") = kyotu(numExchange.SEIQTO_NM)
        dr.Item("SEIQTO_PIC") = kyotu(numExchange.SEIQTO_PIC)
        dr.Item("SKYU_NO") = kyotu(numExchange.SKYU_NO)
        dr.Item("SKYU_DATE") = kyotu(numExchange.SKYU_DATE)
        dr.Item("HAKKO_MOTO_NM") = kyotu(numExchange.HAKKO_MOTO_NM)
        dr.Item("HAKKO_MOTO_ADD1") = kyotu(numExchange.HAKKO_MOTO_ADD1)
        dr.Item("HAKKO_MOTO_ADD2") = kyotu(numExchange.HAKKO_MOTO_ADD2)
        dr.Item("HAKKO_MOTO_TEL") = kyotu(numExchange.HAKKO_MOTO_TEL)
        dr.Item("SKYU_SYOSYU_SORT") = kyotu(numExchange.SKYU_SYOSYU_SORT)
        dr.Item("SKYU_SYOSYU") = kyotu(numExchange.SKYU_SYOSYU)
        dr.Item("SKYU_MONTH") = kyotu(numExchange.SKYU_MONTH)
        '2014.08.21 追加START 多通貨対応
        dr.Item("EX_RATE") = kyotu(numExchange.EX_RATE)
        dr.Item("EX_MOTO_CURR_CD") = kyotu(numExchange.EX_MOTO_CURR_CD)
        dr.Item("EX_SAKI_CURR_CD") = kyotu(numExchange.EX_SAKI_CURR_CD)
        dr.Item("SEIQ_CURR_CD") = kyotu(numExchange.SEIQ_CURR_CD)
        dr.Item("SEIQ_ROUND_POS") = kyotu(numExchange.SEIQ_ROUND_POS)
        '2014.08.21 追加END 多通貨対応

        'GROUPHEADER
        dr.Item("SKYU_KINGAKU") = kyotu(numExchange.SKYU_KINGAKU)
        dr.Item("REMARK") = kyotu(numExchange.REMARK)

        'GROUPFOOTER
        dr.Item("BANK_NM") = kyotu(numExchange.BANK_NM)
        dr.Item("MEIGI_NM") = kyotu(numExchange.MEIGI_NM)
        dr.Item("YOKIN_INFO") = kyotu(numExchange.YOKIN_INFO)

        dr.Item("TAX_HASU_GK1") = kyotu(numExchange.TAX_HASU_GK1)

        Return dr
    End Function
    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' 共通項目のデータセット設定
    ''' </summary>
    ''' <param name="kyotu"></param>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Private Function kyotuSetData(ByVal kyotu As ArrayList, ByVal dr As DataRow) As DataRow

        dr.Item("RPT_ID") = kyotu(num.RPT_ID)
        'PAGEHEADER
        dr.Item("SEIQTO_NM") = kyotu(num.SEIQTO_NM)
        dr.Item("SEIQTO_PIC") = kyotu(num.SEIQTO_PIC)
        dr.Item("SKYU_NO") = kyotu(num.SKYU_NO)
        dr.Item("SKYU_DATE") = kyotu(num.SKYU_DATE)
        dr.Item("HAKKO_MOTO_NM") = kyotu(num.HAKKO_MOTO_NM)
        dr.Item("HAKKO_MOTO_ADD1") = kyotu(num.HAKKO_MOTO_ADD1)
        dr.Item("HAKKO_MOTO_ADD2") = kyotu(num.HAKKO_MOTO_ADD2)
        dr.Item("HAKKO_MOTO_TEL") = kyotu(num.HAKKO_MOTO_TEL)
        dr.Item("SKYU_SYOSYU_SORT") = kyotu(num.SKYU_SYOSYU_SORT)
        dr.Item("SKYU_SYOSYU") = kyotu(num.SKYU_SYOSYU)
        dr.Item("SKYU_MONTH") = kyotu(num.SKYU_MONTH)

        'GROUPHEADER
        dr.Item("SKYU_KINGAKU") = kyotu(num.SKYU_KINGAKU)
        dr.Item("REMARK") = kyotu(num.REMARK)

        'GROUPFOOTER
        dr.Item("BANK_NM") = kyotu(num.BANK_NM)
        dr.Item("MEIGI_NM") = kyotu(num.MEIGI_NM)
        dr.Item("YOKIN_INFO") = kyotu(num.YOKIN_INFO)

        dr.Item("TAX_HASU_GK1") = kyotu(num.TAX_HASU_GK1)


        Return dr
    End Function

    '2014.08.21 追加START 多通貨対応
    ''' <summary>
    ''' 共通項目ナンバー
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum numExchange As Integer

        'PAGEHEADER
        RPT_ID = 0
        SEIQTO_NM
        SEIQTO_PIC
        SKYU_NO
        SKYU_DATE
        HAKKO_MOTO_NM
        HAKKO_MOTO_ADD1
        HAKKO_MOTO_ADD2
        HAKKO_MOTO_TEL
        SKYU_SYOSYU_SORT
        SKYU_SYOSYU
        SKYU_MONTH

        'GROUPHEADER
        SKYU_KINGAKU
        REMARK

        'GROUPFOOTER
        BANK_NM
        MEIGI_NM
        YOKIN_INFO

        TAX_HASU_GK1
        EX_RATE
        EX_MOTO_CURR_CD
        EX_SAKI_CURR_CD
        SEIQ_CURR_CD
        SEIQ_ROUND_POS

    End Enum
    '2014.08.21 追加END 多通貨対応

    ''' <summary>
    ''' 共通項目ナンバー
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum num As Integer

        'PAGEHEADER
        RPT_ID = 0
        SEIQTO_NM
        SEIQTO_PIC
        SKYU_NO
        SKYU_DATE
        HAKKO_MOTO_NM
        HAKKO_MOTO_ADD1
        HAKKO_MOTO_ADD2
        HAKKO_MOTO_TEL
        SKYU_SYOSYU_SORT
        SKYU_SYOSYU
        SKYU_MONTH

        'GROUPHEADER
        SKYU_KINGAKU
        REMARK

        'GROUPFOOTER
        BANK_NM
        MEIGI_NM
        YOKIN_INFO

        TAX_HASU_GK1

    End Enum

#End Region

#End Region

End Class
