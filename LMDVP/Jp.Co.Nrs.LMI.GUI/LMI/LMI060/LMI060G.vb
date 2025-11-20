' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI      : 請求
'  プログラムID     :  LMI060G  : 三井化学ポリウレタン運賃計算「危険品一割増」処理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI060F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI060F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <param name="mode">Dispモード</param>
    ''' <param name="status">レコードステータス</param>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal mode As String, ByVal status As String)

        With Me._Frm

        End With

    End Sub

#End Region 'Mode&Status

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .imdDateFrom.TabIndex = LMI060C.CtlTabIndex.DATEFROM
            .imdDateTo.TabIndex = LMI060C.CtlTabIndex.DATETO
            .cmbMake.TabIndex = LMI060C.CtlTabIndex.CMBMAKE
            .btnMake.TabIndex = LMI060C.CtlTabIndex.BTNMAKE
            .cmbPrint.TabIndex = LMI060C.CtlTabIndex.CMBPRINT
            .btnPrint.TabIndex = LMI060C.CtlTabIndex.BTNPRINT
            .sprDetail.TabIndex = LMI060C.CtlTabIndex.SPRDETAILS

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .imdDateFrom.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal sysDate As String)

        With Me._Frm

            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            '前月の末日を求める
            Dim lastDate As String = Convert.ToString(DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), 1).AddDays(-1))
            lastDate = String.Concat( _
                                    Mid(lastDate, 1, 4), _
                                    Mid(lastDate, 6, 2), _
                                    Mid(lastDate, 9, 2) _
                                   )
            Dim firstDate As String = String.Concat( _
                                                    Mid(lastDate, 1, 6), _
                                                    "01" _
                                                   )
            .imdDateFrom.TextValue = firstDate
            .imdDateTo.TextValue = lastDate

            '要望番号:1482 KIM 2012.10.10 START
            '.txtCustCdL.TextValue = String.Empty
            '.lblCustNmL.TextValue = String.Empty
            '.txtCustCdM.TextValue = String.Empty
            '.lblCustNmM.TextValue = String.Empty
            '.txtCustCdS.TextValue = String.Empty
            '.lblCustNmS.TextValue = String.Empty
            '.txtCustCdSS.TextValue = String.Empty
            '.lblCustNmSS.TextValue = String.Empty
            '要望番号:1482 KIM 2012.10.10 END

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '******* 表示列 *******
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.DEF, "  ", 20, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_CD_L, "荷主コード(大)", 100, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_CD_M, "中", 30, True)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_CD_S, "小", 30, True)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_CD_SS, "極小", 30, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_NM, "荷主名", 250, True)
        Public Shared TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.TARIFF_CD, "タリフコード", 90, True)
        Public Shared TARIFF_KB As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.TARIFF_KB, "タリフ種別", 80, True)
        Public Shared WARIMASHI_NR As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.WARIMASHI_NR, "割増率", 80, True)
        Public Shared ROUND_KB As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.ROUND_KB, "丸め区分", 80, True)
        Public Shared ROUND_UT As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.ROUND_UT, "丸め単位", 80, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.REMARK, "コメント", 300, True)

        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 1, False)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_NM_M, "荷主名(中)", 1, False)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_NM_S, "荷主名(小)", 1, False)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.CUST_NM_SS, "荷主名(極小)", 1, False)
        Public Shared ROUND_UT_LEN As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.ROUND_UT_LEN, "丸め単位桁数", 1, False)
        Public Shared FREE_C01 As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.FREE_C01, "文字列01", 1, False)
        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
        Public Shared ROW_CNT As SpreadColProperty = New SpreadColProperty(LMI060C.SprColumnIndex.ROW_CNT, "ROW_CNT", 1, False)
        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999, False)
        Dim sCmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, Nothing, False)
        Dim sTxtlo As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            .ActiveSheet.Rows.Count = 0

            .SetColProperty(New LMI060G.sprDetailDef())

            .ActiveSheet.ColumnCount = LMI060C.SprColumnIndex.LAST

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sLabelLeft As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999, False)
        Dim sCmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, Nothing, False)
        Dim sTxtlo As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)
        Dim max As Integer = ds.Tables(LMI060C.TABLE_NM_OUT).Rows.Count - 1
        Dim custDr() As DataRow = Nothing
        Dim custWhere As String = String.Empty
        Dim strWK As String = String.Empty

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            .ActiveSheet.Rows.Count = 0

            sTxt.BackColor = Color.White
            sCmb.BackColor = Color.White

            .ActiveSheet.ColumnCount = LMI060C.SprColumnIndex.LAST

            For i As Integer = 0 To max
                .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

                'スプレッドの設定
                .SetCellStyle(i, LMI060G.sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_CD_L.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_CD_M.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_CD_S.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_CD_SS.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_NM.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.TARIFF_CD.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.TARIFF_KB.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.WARIMASHI_NR.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.ROUND_KB.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.ROUND_UT.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.REMARK.ColNo, sLabelLeft)

                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_NM_L.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_NM_M.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_NM_S.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.CUST_NM_SS.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.ROUND_UT_LEN.ColNo, sLabelLeft)
                .SetCellStyle(i, LMI060G.sprDetailDef.FREE_C01.ColNo, sLabelLeft)
                'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
                .SetCellStyle(i, LMI060G.sprDetailDef.ROW_CNT.ColNo, sLabelLeft)
                'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

                '値を設定
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_M").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_S").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_SS.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_SS").ToString)
                .SetCellValue(i, sprDetailDef.TARIFF_CD.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("TARIFF_CD").ToString)
                .SetCellValue(i, sprDetailDef.TARIFF_KB.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("TARIFF_KB").ToString)
                .SetCellValue(i, sprDetailDef.WARIMASHI_NR.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("WARIMASHI_NR").ToString)
                .SetCellValue(i, sprDetailDef.ROUND_KB.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("ROUND_KB").ToString)
                .SetCellValue(i, sprDetailDef.ROUND_UT.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("ROUND_UT").ToString)
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("REMARK").ToString)
                .SetCellValue(i, sprDetailDef.ROUND_UT_LEN.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("ROUND_UT_LEN").ToString)
                .SetCellValue(i, sprDetailDef.FREE_C01.ColNo, ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("FREE_C01").ToString)
                'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
                .SetCellValue(i, sprDetailDef.ROW_CNT.ColNo, Convert.ToString(i))
                'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

                '荷主名をキャッシュから取得
                custWhere = String.Concat("CUST_CD_L = '", ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_L").ToString, "' AND ", _
                                          "CUST_CD_M = '", ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_M").ToString, "' ")
                If String.IsNullOrEmpty(ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_S").ToString) = False Then
                    custWhere = String.Concat(custWhere, _
                                           "AND CUST_CD_S = '", ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_S").ToString, "' ")

                End If
                If String.IsNullOrEmpty(ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_SS").ToString) = False Then
                    custWhere = String.Concat(custWhere, _
                                           "AND CUST_CD_SS = '", ds.Tables(LMI060C.TABLE_NM_OUT).Rows(i).Item("CUST_CD_SS").ToString, "' ")

                End If
                custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(custWhere)
                If 0 < custDr.Length Then
                    strWK = String.Concat(custDr(0).Item("CUST_NM_L").ToString, _
                                          "", _
                                          custDr(0).Item("CUST_NM_M").ToString)
                    .SetCellValue(i, sprDetailDef.CUST_NM.ColNo, strWK)
                    .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, custDr(0).Item("CUST_NM_L").ToString)
                    .SetCellValue(i, sprDetailDef.CUST_NM_M.ColNo, custDr(0).Item("CUST_NM_M").ToString)
                    .SetCellValue(i, sprDetailDef.CUST_NM_S.ColNo, custDr(0).Item("CUST_NM_S").ToString)
                    .SetCellValue(i, sprDetailDef.CUST_NM_SS.ColNo, custDr(0).Item("CUST_NM_SS").ToString)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    '要望番号:1482 KIM 2012.10.10 START
    '''' <summary>
    '''' スプレッドの選択行の荷主設定処理
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SpreadCellClick(ByVal frm As LMI060F, ByVal row As Integer)

    '    With frm
    '        Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet

    '        .txtCustCdL.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_CD_L.ColNo))
    '        .txtCustCdM.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_CD_M.ColNo))
    '        .txtCustCdS.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_CD_S.ColNo))
    '        .txtCustCdSS.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_CD_SS.ColNo))

    '        .lblCustNmL.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_NM_L.ColNo))
    '        .lblCustNmM.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_NM_M.ColNo))
    '        .lblCustNmS.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_NM_S.ColNo))
    '        .lblCustNmSS.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.CUST_NM_SS.ColNo))

    '        .lblTariffCd.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.TARIFF_CD.ColNo))
    '        .lblWarimashi.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.WARIMASHI_NR.ColNo))
    '        .lblRoundKb.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.ROUND_KB.ColNo))
    '        .lblRoundUt.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.ROUND_UT.ColNo))
    '        .lblRoundUtLen.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.ROUND_UT_LEN.ColNo))
    '        .lblFreeC01.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.FREE_C01.ColNo))
    '        'START YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい
    '        .lblSpreadNo.TextValue = Me._LMIConG.GetCellValue(spr.Cells(row, LMI060G.sprDetailDef.ROW_CNT.ColNo))
    '        'END YANAI 要望番号1402 運賃計算危険品一割増処理を群馬00001で行うと値がおかしい

    '    End With

    'End Sub
    '要望番号:1482 KIM 2012.10.10 END

    ''' <summary>
    ''' スプレッドのチェックボックスをON(明細)notes 1916 2013/03/24 SHINOHARA
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub setChkSpread(ByVal k As Integer)

        'レイアウト用
        Dim spr As LMSpread = Me._Frm.sprDetail

        With spr

            .SetCellValue(Convert.ToInt32(k), sprDetailDef.DEF.ColNo, True.ToString())

        End With

    End Sub

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(CheckBox)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoChk(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetCheckBoxCell(spr, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

#End Region

#End Region

#End Region

End Class
