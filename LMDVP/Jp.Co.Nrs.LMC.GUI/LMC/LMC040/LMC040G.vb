' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC040C : 在庫引当
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMC040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMC040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC040F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC040V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal handlerClass As LMBaseGUIHandler, ByVal frm As LMC040F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validateクラスの設定
        _V = New LMC040V(handlerClass, frm)

        'Gamen共通クラスの設定
        _LMCconG = New LMCControlG(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    'START YANAI 要望番号507
    '''' <summary>
    '''' ファンクションキーの設定
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetFunctionKey(ByVal taninusiFlg As String)
    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal taninusiFlg As String, ByVal outkaSCnt As Integer)
        'END YANAI 要望番号507
        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = "他荷主"
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = "選　択"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            'START YANAI 要望番号507
            'If ("00").Equals(taninusiFlg) = True Then
            '    .F5ButtonEnabled = False
            'Else
            '    .F5ButtonEnabled = always
            'End If
            If ("01").Equals(taninusiFlg) = True AndAlso _
                outkaSCnt = 0 Then
                .F5ButtonEnabled = always
            Else
                .F5ButtonEnabled = False
            End If
            'END YANAI 要望番号507
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .frmAmount.TabIndex = LMC040C.CtlTabIndex.FRMAMOUNT
            .frmCnt.TabIndex = LMC040C.CtlTabIndex.FRMCNT
            .frmShukaTani.TabIndex = LMC040C.CtlTabIndex.FRMSHUKATANI
            .sprZaiko.TabIndex = LMC040C.CtlTabIndex.SPRZAIKO
            .txtSerialNO.TabIndex = LMC040C.CtlTabIndex.TXTSERIALNO
            .txtRsvNO.TabIndex = LMC040C.CtlTabIndex.TXTRSVNO
            .txtLotNO.TabIndex = LMC040C.CtlTabIndex.TXTLOTNO
            .numIrime.TabIndex = LMC040C.CtlTabIndex.NUMIRIME
            .optCnt.TabIndex = LMC040C.CtlTabIndex.OPTCNT
            .optAmt.TabIndex = LMC040C.CtlTabIndex.OPTAMT
            .optKowake.TabIndex = LMC040C.CtlTabIndex.OPTKOWAKE
            .optSample.TabIndex = LMC040C.CtlTabIndex.OPTSAMPLE
            .numSyukkaKosu.TabIndex = LMC040C.CtlTabIndex.NUMSYUKKAKOSU
            .numSyukkaHasu.TabIndex = LMC040C.CtlTabIndex.NUMSYUKKAHASU
            .numSyukkaSouAmt.TabIndex = LMC040C.CtlTabIndex.NUMSYUKKASOUAMT

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal addRecFlg As String = "")
        'Friend Sub SetControlsStatus()

        With _Frm

            Dim kLock As Boolean = True '個数のロック制御
            Dim sLock As Boolean = True '数量のロック制御

            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, _
                                                             "' AND CUST_CD = '", .lblCustCD_L.TextValue, _
                                                             "' AND SUB_KB = '80'"))

            If .optCnt.Checked = True Then
                kLock = False
                sLock = True
                'START YANAI 20110906 サンプル対応
                'ElseIf .optSample.Checked = True Then
                '    kLock = True
                '    sLock = True
                'END YANAI 20110906 サンプル対応

                '2014.09.11 修正START
                If custDetailsDr.Length > 0 AndAlso String.IsNullOrEmpty(.lblGoodsNM.TextValue) = True _
                    AndAlso addRecFlg.Equals("01") = False Then
                    kLock = True
                End If
                '2014.09.11 修正END

            Else
                kLock = True
                sLock = False
            End If

            '梱数・端数・数量のロック制御
            .numSyukkaKosu.ReadOnly = kLock
            .numSyukkaHasu.ReadOnly = kLock
            .numSyukkaSouAmt.ReadOnly = sLock

            '一覧の引当個数・引当数量のロック制御
            Dim max As Integer = .sprZaiko.ActiveSheet.Rows.Count - 1

            For i As Integer = 1 To max

                If custDetailsDr.Length > 0 AndAlso String.IsNullOrEmpty(.lblGoodsNM.TextValue) = True AndAlso addRecFlg.Equals("01") = False Then
                    kLock = False
                End If

                .sprZaiko.SetCellStyle(i, sprZaiko.HIKI_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 9999999999, kLock, 0, True, ","))        '引当個数
                .sprZaiko.SetCellStyle(i, sprZaiko.HIKI_AMT.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 999999999.999, sLock, 3, True, ","))     '引当数量
            Next

            If Convert.ToDecimal(.numIrisu.Value) = 1 Then
                .numSyukkaHasu.ReadOnly = True
            End If

            '.optKowake.Enabled = False
            '.optSample.Enabled = False

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d2 As Decimal = Convert.ToDecimal("99")
            Dim d3 As Decimal = Convert.ToDecimal("999")
            Dim d5 As Decimal = Convert.ToDecimal("99999")
            Dim d6_3 As Decimal = Convert.ToDecimal("999999.999")
            Dim d9_3 As Decimal = Convert.ToDecimal("999999999.999")
            Dim d10 As Decimal = Convert.ToDecimal("9999999999")
            Dim d12_3 As Decimal = Convert.ToDecimal("999999999999.999")

            .numIrime.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , d6_3, 0)
            .numIrisu.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numHikiSumiCnt.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numHikiZanCnt.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numSyukkaKosu.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numSyukkaHasu.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numSyukkaSouCnt.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numHikiCntSum.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numSyukkaSouAmt.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , d12_3, 0)
            .numHikiSumiAmt.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , d12_3, 0)
            .numHikiZanAmt.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , d12_3, 0)
            .numHikiAmtSum.SetInputFields("###,###,###,##0.000", , 12, 1, , 3, 3, , d12_3, 0)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            '初期 
            .numSyukkaKosu.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .lblCustCD_L.TextValue = String.Empty
            .lblCustCD_M.TextValue = String.Empty
            .lblCustNM_L.TextValue = String.Empty
            .lblCustNM_M.TextValue = String.Empty
            .lblGoodsCD.TextValue = String.Empty
            .lblGoodsNM.TextValue = String.Empty
            .txtSerialNO.TextValue = String.Empty
            .txtRsvNO.TextValue = String.Empty
            .txtLotNO.TextValue = String.Empty
            .numIrime.Value = 0
            .optCnt.Checked = True
            .numIrisu.Value = 0
            .numHikiSumiCnt.Value = 0
            .numHikiZanCnt.Value = 0
            .numSyukkaKosu.TextValue = String.Empty
            .numSyukkaHasu.TextValue = String.Empty
            .numSyukkaSouCnt.Value = 0
            .numHikiCntSum.Value = 0
            .numSyukkaSouAmt.TextValue = String.Empty
            .numHikiSumiAmt.Value = 0
            .numHikiZanAmt.Value = 0
            .numHikiAmtSum.Value = "0"

            .lblGoodsNRS.TextValue = "0"
            .lblConsDate.TextValue = "0"

            'START YANAI 20111003 一括引当対応
            .txtOutkaPlanDate.TextValue = String.Empty
            'END YANAI 20111003 一括引当対応

        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' INのデータセットの値を画面に表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitForm(ByVal frm As LMC040F, ByVal prmDs As DataSet, Optional ByVal addRecFlg As String = "")

        Dim custNm As String = String.Empty
        Dim strSqlCust As String = String.Empty

        With prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
            frm.cmbEigyo.SelectedValue = .Rows(0)("NRS_BR_CD").ToString()
            frm.cmbSoko.SelectedValue = .Rows(0)("WH_CD").ToString()
            frm.lblCustCD_L.TextValue = .Rows(0)("CUST_CD_L").ToString()
            frm.lblCustCD_M.TextValue = .Rows(0)("CUST_CD_M").ToString()
            frm.lblCustNM_L.TextValue = .Rows(0)("CUST_NM_L").ToString()
            frm.lblCustNM_M.TextValue = .Rows(0)("CUST_NM_M").ToString() '20160926 tsunehira 荷主名大から荷主名中に変更
            frm.lblGoodsCD.TextValue = .Rows(0)("GOODS_CD_CUST").ToString()
            frm.lblGoodsNM.TextValue = .Rows(0)("GOODS_NM").ToString()
            frm.txtSerialNO.TextValue = .Rows(0)("SERIAL_NO").ToString()
            frm.txtRsvNO.TextValue = .Rows(0)("RSV_NO").ToString()
            frm.txtLotNO.TextValue = .Rows(0)("LOT_NO").ToString()
            frm.numIrime.Value = .Rows(0)("IRIME").ToString()
            frm.lblIrimeTani.TextValue = .Rows(0)("IRIME_UT").ToString()
            Select Case .Rows(0)("ALCTD_KB").ToString()
                Case "01"   '個数
                    frm.optCnt.Checked = True
                Case "02"   '数量
                    frm.optAmt.Checked = True
                Case "03"   '小分け
                    frm.optKowake.Checked = True
                Case "04"   'サンプル
                    frm.optSample.Checked = True
            End Select
            frm.numIrisu.Value = .Rows(0)("PKG_NB").ToString()
            frm.numHikiSumiCnt.Value = .Rows(0)("ALCTD_NB").ToString()
            frm.numHikiZanCnt.Value = .Rows(0)("BACKLOG_NB").ToString()
            frm.numSyukkaKosu.Value = .Rows(0)("KONSU").ToString()
            frm.txtSyukkaKosu.TextValue = .Rows(0)("NB_UT").ToString()
            frm.numSyukkaHasu.Value = .Rows(0)("HASU").ToString()
            frm.txtSyukkaHasu.TextValue = .Rows(0)("NB_UT").ToString()
            frm.numSyukkaSouCnt.Value = .Rows(0)("KOSU").ToString()
            frm.numSyukkaSouAmt.Value = .Rows(0)("SURYO").ToString()
            frm.lblSyukkaSouAmt.TextValue = .Rows(0)("STD_IRIME_UT").ToString()
            frm.numHikiSumiAmt.Value = .Rows(0)("ALCTD_QT").ToString()
            frm.numHikiZanAmt.Value = .Rows(0)("BACKLOG_QT").ToString()
            frm.numHikiCntSum.Value = frm.numHikiSumiCnt.Value
            frm.numHikiAmtSum.Value = frm.numHikiSumiAmt.Value
            frm.lblGoodsNRS.TextValue = .Rows(0)("GOODS_CD_NRS").ToString()

            frm.sprZaiko.SetCellValue(0, sprZaiko.REMARK.ColNo, .Rows(0)("REMARK").ToString())
            frm.sprZaiko.SetCellValue(0, sprZaiko.REMARK_OUT.ColNo, .Rows(0)("REMARK_OUT").ToString())
            frm.sprZaiko.SetCellValue(0, sprZaiko.TAX_KB.ColNo, .Rows(0)("TAX_KB").ToString())
            frm.sprZaiko.SetCellValue(0, sprZaiko.HIKIATE_ALERT_NM.ColNo, .Rows(0)("HIKIATE_ALERT_YN").ToString())
            frm.sprZaiko.SetCellValue(0, sprZaiko.DEST_NM.ColNo, .Rows(0)("DEST_NM").ToString())

            'START YANAI 20111003 一括引当対応
            frm.txtOutkaPlanDate.TextValue = .Rows(0)("OUTKA_PLAN_DATE").ToString()
            'END YANAI 20111003 一括引当対応

            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue, _
                                                             "' AND CUST_CD = '", frm.lblCustCD_L.TextValue, _
                                                             "' AND SUB_KB = '80'"))

            If custDetailsDr.Length > 0 AndAlso String.IsNullOrEmpty(.Rows(0)("GOODS_CD_NRS").ToString()) = True AndAlso addRecFlg.Equals("01") = False Then
                frm.numHikiZanAmt.Value = Convert.ToDecimal(.Rows(0)("BACKLOG_NB").ToString()) * Convert.ToDecimal(.Rows(0)("IRIME").ToString())
            Else
                '個数の計算をする
                Dim calsumFlg As Boolean = Me.SetCalSum(LMC040C.EventShubetsu.SYOKI)
            End If


        End With

    End Sub

    ''' <summary>
    ''' INのデータセットの値を画面に表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitFormTaninusi(ByVal frm As LMC040F, ByVal dt As DataTable)

        With dt
            If 0 < dt.Rows.Count Then
                frm.numIrime.Value = .Rows(0)("IRIME").ToString()
                frm.lblIrimeTani.TextValue = .Rows(0)("STD_IRIME_UT").ToString()
                frm.numIrisu.Value = .Rows(0)("PKG_NB").ToString()
                frm.txtSyukkaKosu.TextValue = .Rows(0)("NB_UT").ToString()
                frm.txtSyukkaHasu.TextValue = .Rows(0)("NB_UT").ToString()
                frm.lblSyukkaSouAmt.TextValue = .Rows(0)("STD_IRIME_UT").ToString()
                frm.lblGoodsNM.TextValue = .Rows(0)("GOODS_NM_1").ToString()

            End If

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprZaiko

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.DEF, " ", 20, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.LOT_NO, "ロット№", 70, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.IRIME, "入目", 70, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.TOU_NO, "棟", 25, True)
        Public Shared SHITSU_NO As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.SHITSU_NO, "室", 25, True)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.ZONE_CD, "ZONE", 55, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.LOCA, "ロケーション", 100, True)
        Public Shared HIKI_CNT As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.HIKI_CNT, "引当個数", 100, True)
        Public Shared HIKI_AMT As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.HIKI_AMT, "引当数量", 100, True)
        Public Shared HIKI_KANO_CNT As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.HIKI_KANO_CNT, "引当可能個数", 100, True)
        Public Shared ZAIKOHIKI_KANO_CNTHASU As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.HIKI_KANO_UT, " ", 40, True)
        Public Shared HIKI_KANO_AMT As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.HIKI_KANO_AMT, "引当可能数量", 100, True)
        Public Shared NAKAMI As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.NAKAMI, "状態 中身", 80, True)
        Public Shared GAIKAN As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.GAIKAN, "状態 外装", 80, True)
        Public Shared CUST_STATUS As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.CUST_STATUS, "状態 荷主", 80, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.REMARK, "備考小" & vbCrLf & "（社内）", 70, True)
        Public Shared INKO_DATE As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.INKO_DATE, "入荷日", 80, True)
        Public Shared OFB_KBN As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.OFB_KBN, "簿外品", 65, True)
        Public Shared SPD_KBN_S As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.SPD_KBN_S, "保留品", 65, True)
        Public Shared KEEP_GOODS_NM As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.KEEP_GOODS_NM, "キープ品", 115, False)
        Public Shared YOYAKU_NO As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.YOYAKU_NO, "予約番号", 80, False)
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.SERIAL_NO, "シリアル№", 80, True)
        Public Shared GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.GOODS_CRT_DATE, "製造日", 80, True)
        Public Shared ALLOC_PRIORITY As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.ALLOC_PRIORITY, "割当優先", 80, True)
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.REMARK_OUT, "備考小" & vbCrLf & "（社外）", 80, True)
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.LT_DATE, "賞味有効期限", 80, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.DEST_NM, "届先名", 300, True)
        Public Shared PORA_ZAI_NB As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.PORA_ZAI_NB, "実予在庫個数", 120, True)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.TAX_KB, "課税区分", 70, True)                                    '課税区分
        Public Shared HIKIATE_ALERT_NM As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.HIKIATE_ALERT_NM, "引当注意品", 80, True)
        Public Shared ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.ZAI_REC_NO, "在庫" & vbCrLf & "レコード番号", 120, True)

        'invisible
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.NRS_BR_CD, "営業所コード", 1, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.WH_CD, "倉庫コード", 1, False)
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.OUTKA_PLAN_DATE, "出庫日", 1, False)
        Public Shared SMPL_FLG As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.SMPL_FLG, "小分けフラグ", 1, False)
        Public Shared GOODS_CD_NRS_FROM As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.GOODS_CD_NRS_FROM, "振替元商品キー", 1, False)
        'START YANAI 要望番号776
        Public Shared INKO_DATE_ZAI As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.INKO_DATE_ZAI, "入庫日", 1, False)
        'END YANAI 要望番号776
        'START YANAI 要望番号780
        Public Shared INKA_DATE_KANRI_KB As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.INKA_DATE_KANRI_KB, "入庫日管理区分", 1, False)
        'END YANAI 要望番号780
        '(2013.03.12)要望番号1229 -- START --
        Public Shared INKA_STATE_KB As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.INKA_STATE_KB, "入荷作業進捗区分", 1, False)
        '(2013.03.12)要望番号1229 --  END  --

        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.GOODS_CD_NRS, "商品キー", 1, False)

        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.SHOBO_CD, "消防CD", 1, False)
        Public Shared SHOBO_NM As SpreadColProperty = New SpreadColProperty(LMC040C.SprZaikoColumnIndex.SHOBO_NM, "消防NM", 1, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprZaiko.CrearSpread()

            '列数設定
            .sprZaiko.Sheets(0).ColumnCount = LMC040C.SprZaikoColCount

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprZaiko.SetColProperty(New LMC040G.sprZaiko())
            '.sprZaiko.SetColProperty(New LMC040G.sprZaiko(), False)
            '2015.10.15 英語化対応END
            ' 030509【LMS】BYK商品6桁対応
            .sprZaiko.SetColProperty(New LMC040G.sprZaiko(), True)

            '列固定位置を設定します。(ロケーションで固定)
            .sprZaiko.Sheets(0).FrozenColumnCount = LMC040G.sprZaiko.LOCA.ColNo + 1

            '列設定
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX_IME_OFF, 40, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.IRIME.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 999999999, True, 3))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.HAN_NUM_ALPHA, 2, False))              '棟
            'START YANAI 要望番号705
            '.sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SHITSU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.HAN_NUM_ALPHA, 1, False))           '室
            '.sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.HAN_NUM_ALPHA, 1, False))             'ゾーン
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SHITSU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.HAN_NUM_ALPHA, 2, False))           '室
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.HAN_NUM_ALPHA, 2, False))             'ゾーン
            'END YANAI 要望番号705
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.LOCA.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX_IME_OFF, 10, False))               'ロケーション
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.HIKI_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 9999999999, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.HIKI_AMT.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 999999999999, True, 3))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.HIKI_KANO_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 9999999999, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.ZAIKOHIKI_KANO_CNTHASU.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX_IME_OFF, 31, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 999999999999, True, 3))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.NAKAMI.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, LMKbnConst.KBN_S005, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.GAIKAN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, LMKbnConst.KBN_S006, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.CUST_STATUS.ColNo, Me.SetComboJyotai())
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 100, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.INKO_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprZaiko, True))                                         '入庫日
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.OFB_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, LMKbnConst.KBN_B002, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SPD_KBN_S.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, LMKbnConst.KBN_H003, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.KEEP_GOODS_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, LMC040C.KbnConst.BYK_KEEP_GOODS_CD, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.YOYAKU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX_IME_OFF, 10, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX_IME_OFF, 40, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.GOODS_CRT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprZaiko, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.ALLOC_PRIORITY.ColNo, LMSpreadUtility.GetLabelCell(.sprZaiko, CellHorizontalAlignment.Left))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.REMARK_OUT.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 15, False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.LT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprZaiko, True))                                   '賞味有効期限
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX_IME_OFF, 80, False))    '届先名
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.PORA_ZAI_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprZaiko, 0, 9999999999, True))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.TAX_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, "Z001", False))                           '課税区分
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.HIKIATE_ALERT_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprZaiko, "U009", False))
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.ZAI_REC_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 10, False))

            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.NRS_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 8, False))       '営業所コード
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.WH_CD.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 8, False))           '倉庫コード
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.OUTKA_PLAN_DATE.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 8, False)) '出庫日
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SMPL_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 2, False))        '小分けフラグ
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SHOBO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 2, False))        '消防CD
            .sprZaiko.SetCellStyle(0, LMC040G.sprZaiko.SHOBO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprZaiko, InputControl.ALL_MIX, 2, False))        '消防NM
        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMC040F)

        With frm.sprZaiko

            .Sheets(0).Cells(0, sprZaiko.LOT_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.IRIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.TOU_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.SHITSU_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.ZONE_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.LOCA.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.HIKI_CNT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.HIKI_AMT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.HIKI_KANO_CNT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.ZAIKOHIKI_KANO_CNTHASU.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.HIKI_KANO_AMT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.NAKAMI.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.GAIKAN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.CUST_STATUS.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.REMARK.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.INKO_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.OFB_KBN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.SPD_KBN_S.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.KEEP_GOODS_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.YOYAKU_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.SERIAL_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.GOODS_CRT_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.ALLOC_PRIORITY.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.REMARK_OUT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.LT_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.PORA_ZAI_NB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.TAX_KB.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.HIKIATE_ALERT_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.ZAI_REC_NO.ColNo).Value = String.Empty

            .Sheets(0).Cells(0, sprZaiko.NRS_BR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.WH_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.OUTKA_PLAN_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.SMPL_FLG.ColNo).Value = String.Empty

            .Sheets(0).Cells(0, sprZaiko.SHOBO_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprZaiko.SHOBO_NM.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal frm As LMC040F, ByVal dt As DataTable, ByVal prmDs As DataSet)

        Dim spr As LMSpreadSearch = Me._Frm.sprZaiko

        With spr

            .SuspendLayout()

            '----データ挿入----'

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If 0 = lngcnt Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count(), lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dr As DataRow = dt.NewRow
            Dim zaiDr() As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                If i = 1 Then
                    Dim isVisibleKeepGoods As Boolean = False
                    If dr.Item("IS_BYK_KEEP_GOODS_CD").ToString() = "1" Then
                        isVisibleKeepGoods = True
                    End If
                    .ActiveSheet.Columns(sprZaiko.KEEP_GOODS_NM.ColNo).Visible = isVisibleKeepGoods
                End If

                'セルスタイル設定
                .SetCellStyle(i, sprZaiko.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprZaiko.LOT_NO.ColNo, sLabel)                                                                          'ロット№
                .SetCellStyle(i, sprZaiko.IRIME.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, True, ","))            '入目
                .SetCellStyle(i, sprZaiko.TOU_NO.ColNo, sLabel)                                                                          '棟
                .SetCellStyle(i, sprZaiko.SHITSU_NO.ColNo, sLabel)                                                                       '室
                .SetCellStyle(i, sprZaiko.ZONE_CD.ColNo, sLabel)                                                                         'ZONE
                .SetCellStyle(i, sprZaiko.LOCA.ColNo, sLabel)                                                                            'LOC
                .SetCellStyle(i, sprZaiko.HIKI_CNT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))        '引当個数
                .SetCellStyle(i, sprZaiko.HIKI_AMT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, True, ","))      '引当数量
                .SetCellStyle(i, sprZaiko.HIKI_KANO_CNT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))    '引当可能個数
                .SetCellStyle(i, sprZaiko.ZAIKOHIKI_KANO_CNTHASU.ColNo, sLabel)                                                          '在庫引当可能梱数/端数
                .SetCellStyle(i, sprZaiko.HIKI_KANO_AMT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, True, ",")) '引当可能数量
                .SetCellStyle(i, sprZaiko.NAKAMI.ColNo, sLabel)                                                                          '中身
                .SetCellStyle(i, sprZaiko.GAIKAN.ColNo, sLabel)                                                                          '外観
                .SetCellStyle(i, sprZaiko.CUST_STATUS.ColNo, sLabel)                                                                     '荷主状態
                .SetCellStyle(i, sprZaiko.REMARK.ColNo, sLabel)                                                                          'REMARK
                .SetCellStyle(i, sprZaiko.INKO_DATE.ColNo, sLabel)                                                                         '入庫日
                .SetCellStyle(i, sprZaiko.OFB_KBN.ColNo, sLabel)                                                                         '保留品
                .SetCellStyle(i, sprZaiko.SPD_KBN_S.ColNo, sLabel)                                                                       '簿外品
                .SetCellStyle(i, sprZaiko.KEEP_GOODS_NM.ColNo, sLabel)                                                                   'キープ品
                .SetCellStyle(i, sprZaiko.YOYAKU_NO.ColNo, sLabel)                                                                       '予約番号
                .SetCellStyle(i, sprZaiko.SERIAL_NO.ColNo, sLabel)                                                                       'シリアル№
                .SetCellStyle(i, sprZaiko.GOODS_CRT_DATE.ColNo, sLabel)                                                                  '製造日
                .SetCellStyle(i, sprZaiko.ALLOC_PRIORITY.ColNo, sLabel)                                                                  '割当優先
                .SetCellStyle(i, sprZaiko.REMARK_OUT.ColNo, sLabel)                                                                        '入番(小)
                .SetCellStyle(i, sprZaiko.LT_DATE.ColNo, sLabel)                                                                         '賞味有効期限
                .SetCellStyle(i, sprZaiko.DEST_NM.ColNo, sLabel)                                                                         '届先名
                .SetCellStyle(i, sprZaiko.PORA_ZAI_NB.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, True, ","))    '実予在庫個数
                .SetCellStyle(i, sprZaiko.TAX_KB.ColNo, sLabel)                                                                         '課税区分
                .SetCellStyle(i, sprZaiko.HIKIATE_ALERT_NM.ColNo, sLabel)                                                                      '引当注意品
                .SetCellStyle(i, sprZaiko.ZAI_REC_NO.ColNo, sLabel)                                                                      '在庫レコード番号

                .SetCellStyle(i, sprZaiko.NRS_BR_CD.ColNo, sLabel)                                                                         '営業所コード
                .SetCellStyle(i, sprZaiko.WH_CD.ColNo, sLabel)                                                                         '倉庫コード
                .SetCellStyle(i, sprZaiko.OUTKA_PLAN_DATE.ColNo, sLabel)                                                                         '出荷日
                .SetCellStyle(i, sprZaiko.SMPL_FLG.ColNo, sLabel)                                                                         '小分けフラグ
                .SetCellStyle(i, sprZaiko.SHOBO_CD.ColNo, sLabel)                                                                         '消防CD
                .SetCellStyle(i, sprZaiko.SHOBO_NM.ColNo, sLabel)                                                                         '消防NM

                'セルに値を設定
                .SetCellValue(i, sprZaiko.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprZaiko.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())                    'ロット№
                .SetCellValue(i, sprZaiko.IRIME.ColNo, dr.Item("IRIME").ToString())                      '入目
                .SetCellValue(i, sprZaiko.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())                    '棟
                .SetCellValue(i, sprZaiko.SHITSU_NO.ColNo, dr.Item("SITU_NO").ToString())                '室
                .SetCellValue(i, sprZaiko.ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())                  'ZONE
                .SetCellValue(i, sprZaiko.LOCA.ColNo, dr.Item("LOCA").ToString())                        'LOC
                .SetCellValue(i, sprZaiko.HIKI_CNT.ColNo, "0")                                           '引当個数
                .SetCellValue(i, sprZaiko.HIKI_AMT.ColNo, "0")                                           '引当数量
                .SetCellValue(i, sprZaiko.HIKI_KANO_CNT.ColNo, dr.Item("ALLOC_CAN_NB").ToString())       '引当可能個数
                .SetCellValue(i, sprZaiko.ZAIKOHIKI_KANO_CNTHASU.ColNo, _Frm.txtSyukkaKosu.TextValue)    '引当可能個数単位
                .SetCellValue(i, sprZaiko.HIKI_KANO_AMT.ColNo, dr.Item("ALLOC_CAN_QT").ToString())       '引当可能数量
                .SetCellValue(i, sprZaiko.NAKAMI.ColNo, dr.Item("GOODS_COND_NM_1").ToString())           '中身
                .SetCellValue(i, sprZaiko.GAIKAN.ColNo, dr.Item("GOODS_COND_NM_2").ToString())           '外観
                .SetCellValue(i, sprZaiko.CUST_STATUS.ColNo, dr.Item("GOODS_COND_NM_3").ToString())      '荷主状態
                .SetCellValue(i, sprZaiko.REMARK.ColNo, dr.Item("REMARK").ToString())                    'REMARK
                'START YANAI 要望番号926
                '.SetCellValue(i, sprZaiko.INKO_DATE.ColNo, _
                '              Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("INKA_DATE").ToString()))                '入庫日
                .SetCellValue(i, sprZaiko.INKO_DATE.ColNo,
                              Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("INKA_DATE2").ToString()))                '入庫日
                'END YANAI 要望番号926
                .SetCellValue(i, sprZaiko.OFB_KBN.ColNo, dr.Item("OFB_KB_NM").ToString())                '保留品
                .SetCellValue(i, sprZaiko.SPD_KBN_S.ColNo, dr.Item("SPD_KB_NM").ToString())              '簿外品
                .SetCellValue(i, sprZaiko.KEEP_GOODS_NM.ColNo, dr.Item("KEEP_GOODS_NM").ToString())      'キープ品
                .SetCellValue(i, sprZaiko.YOYAKU_NO.ColNo, dr.Item("RSV_NO").ToString())                 '予約番号
                .SetCellValue(i, sprZaiko.SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())              'シリアル№
                .SetCellValue(i, sprZaiko.GOODS_CRT_DATE.ColNo,
                              Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("GOODS_CRT_DATE").ToString())) '製造日
                .SetCellValue(i, sprZaiko.ALLOC_PRIORITY.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())     '割当優先
                .SetCellValue(i, sprZaiko.REMARK_OUT.ColNo, dr.Item("REMARK_OUT").ToString())                '入番(小)
                .SetCellValue(i, sprZaiko.LT_DATE.ColNo,
                               Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))                  '賞味有効期限
                .SetCellValue(i, sprZaiko.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())                          '届先名
                .SetCellValue(i, sprZaiko.PORA_ZAI_NB.ColNo, dr.Item("PORA_ZAI_NB").ToString())                  '実予在庫個数
                .SetCellValue(i, sprZaiko.TAX_KB.ColNo, dr.Item("TAX_KB_NM").ToString())                         '課税区分
                .SetCellValue(i, sprZaiko.HIKIATE_ALERT_NM.ColNo, dr.Item("HIKIATE_ALERT_NM").ToString())        '引当注意品
                .SetCellValue(i, sprZaiko.ZAI_REC_NO.ColNo, dr.Item("ZAI_REC_NO").ToString())                    '在庫レコード番号

                .SetCellValue(i, sprZaiko.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())              '営業所コード
                .SetCellValue(i, sprZaiko.WH_CD.ColNo, dr.Item("WH_CD").ToString())                      '倉庫コード
                .SetCellValue(i, sprZaiko.OUTKA_PLAN_DATE.ColNo,
                              Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))          '出荷日
                .SetCellValue(i, sprZaiko.SMPL_FLG.ColNo, dr.Item("SMPL_FLAG").ToString())                '小分けフラグ
                .SetCellValue(i, sprZaiko.GOODS_CD_NRS_FROM.ColNo, dr.Item("GOODS_CD_NRS_FROM").ToString())                '振替元商品キー
                'START YANAI 要望番号776
                .SetCellValue(i, sprZaiko.INKO_DATE_ZAI.ColNo, dr.Item("INKO_DATE").ToString())                '入庫日
                'END YANAI 要望番号776
                'START YANAI 要望番号780
                .SetCellValue(i, sprZaiko.INKA_DATE_KANRI_KB.ColNo, dr.Item("INKA_DATE_KANRI_KB").ToString())                '入庫日管理区分
                'END YANAI 要望番号780
                '(2013.03.12)要望番号1229 -- START --
                .SetCellValue(i, sprZaiko.INKA_STATE_KB.ColNo, dr.Item("INKA_STATE_KB").ToString())             '入荷作業進捗区分
                '(2013.03.12)要望番号1229 --  END  --

                frm.lblConsDate.TextValue = dr.Item("CONSUME_PERIOD_DATE").ToString()                      '消費期限事前禁止日

                .SetCellValue(i, sprZaiko.GOODS_CD_NRS.ColNo, dr.Item("GOODS_CD_NRS").ToString())                '商品キー

                .SetCellValue(i, sprZaiko.SHOBO_CD.ColNo, dr.Item("SHOBO_CD").ToString())                '消防CD
                .SetCellValue(i, sprZaiko.SHOBO_NM.ColNo, dr.Item("SHOBO_NM").ToString())                '消防NM


                '引当可能個数・数量の上書き
                If 0 < prmDs.Tables(LMC040C.TABLE_NM_ZAI).Rows.Count Then
                    zaiDr = prmDs.Tables(LMC040C.TABLE_NM_ZAI).Select(String.Concat("ZAI_REC_NO = '", dr.Item("ZAI_REC_NO").ToString(), "'"))
                    If 0 < zaiDr.Length Then
                        .SetCellValue(i, sprZaiko.HIKI_KANO_CNT.ColNo, zaiDr(0).Item("ALLOC_CAN_NB").ToString)
                        .SetCellValue(i, sprZaiko.HIKI_KANO_AMT.ColNo, zaiDr(0).Item("ALLOC_CAN_QT").ToString)
                    End If
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 個数・数量を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetCalSum(ByVal eventShubetsu As LMC040C.EventShubetsu) As Boolean

        Dim souAmt As Decimal = 0
        Dim souCnt As Decimal = 0
        Dim kosu As Decimal = 0
        Dim hasu As Decimal = 0

        With Me._Frm
            '値をチェック
            If _V.IrimeCheck() = False Then
                Return False
            End If
            If (LMC040C.EventShubetsu.CAL_KONSU).Equals(eventShubetsu) = True Then
                '梱数・端数変更時

                '数量計算
                souAmt = Convert.ToDecimal( _
                        (Convert.ToDecimal(.numSyukkaKosu.Value) _
                       * Convert.ToDecimal(.numIrisu.Value) _
                       + Convert.ToDecimal(.numSyukkaHasu.Value)) _
                       * Convert.ToDecimal(.numIrime.Value))
            ElseIf (LMC040C.EventShubetsu.CAL_SURYO).Equals(eventShubetsu) = True Then
                '数量変更時
                '個数計算
                If .optAmt.Checked = True Then
                    '数量選択時
                    kosu = System.Math.Floor( _
                             Me.CalcData(Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                           , Convert.ToDecimal(.numIrime.Value)))

                    '梱数計算
                    souCnt = System.Math.Floor(Me.CalcData( _
                             Me.CalcData(Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                           , Convert.ToDecimal(.numIrisu.Value)) _
                           , Convert.ToDecimal(.numIrime.Value)))

                    '端数計算
                    hasu = System.Math.Floor(Me.CalcData( _
                             Me.CalcDataMod(Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                           , Convert.ToDecimal(.numIrisu.Value)) _
                           , Convert.ToDecimal(.numIrime.Value)))

                Else
                    '個数、梱数選択時
                    kosu = 1
                    souCnt = 1
                End If
                'START YANAI 20111027 入り目対応
                'ElseIf (LMC040C.EventShubetsu.CAL_IRIME).Equals(eventShubetsu) = True Then
                '    '入目変更時
                '    If .optCnt.Checked = True Then
                '        '個数選択時

                '        kosu = System.Math.Floor( _
                '                 Me.CalcData(Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                '               , Convert.ToDecimal(.numIrime.Value)))

                '        '梱数計算
                '        souCnt = System.Math.Floor(Me.CalcData( _
                '                 Me.CalcData(Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                '               , Convert.ToDecimal(.numIrisu.Value)) _
                '               , Convert.ToDecimal(.numIrime.Value)))

                '        '端数計算
                '        hasu = System.Math.Floor(Me.CalcData( _
                '                 Me.CalcDataMod(Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                '               , Convert.ToDecimal(.numIrisu.Value)) _
                '               , Convert.ToDecimal(.numIrime.Value)))

                '        '数量計算
                '        souAmt = kosu _
                '               * Convert.ToDecimal(.numIrime.Value)

                '    ElseIf .optAmt.Checked = True Then
                '        '数量選択時

                '        '数量計算
                '        souAmt = Convert.ToDecimal( _
                '                (Convert.ToDecimal(.numSyukkaKosu.Value) _
                '               * Convert.ToDecimal(.numIrisu.Value) _
                '               + Convert.ToDecimal(.numSyukkaHasu.Value)) _
                '               * Convert.ToDecimal(.numIrime.Value))

                '        kosu = System.Math.Floor( _
                '                 Me.CalcData(souAmt _
                '               , Convert.ToDecimal(.numIrime.Value)))

                '        '梱数計算
                '        souCnt = System.Math.Floor(Me.CalcData( _
                '                 Me.CalcData(souAmt _
                '               , Convert.ToDecimal(.numIrisu.Value)) _
                '               , Convert.ToDecimal(.numIrime.Value)))

                '        '端数計算
                '        hasu = System.Math.Floor(Me.CalcData( _
                '                 Me.CalcDataMod(souAmt _
                '               , Convert.ToDecimal(.numIrisu.Value)) _
                '               , Convert.ToDecimal(.numIrime.Value)))

                '    Else
                '        '個数、梱数以外選択時
                '        kosu = 1
                '        souCnt = 1
                '    End If
                'END YANAI 20111027 入り目対応
            End If

            If kosu = 0 Then
                '出荷個数計算
                kosu = Convert.ToDecimal( _
                       Convert.ToDecimal(.numSyukkaKosu.Value) _
                     * Convert.ToDecimal(.numIrisu.Value) _
                     + Convert.ToDecimal(.numSyukkaHasu.Value))
            End If

            '値をチェック
            If _V.CalSumCheck(kosu, souCnt, souAmt) = False Then
                Return False
            End If

            '値設定
            If souAmt <> 0 Then
                .numSyukkaSouAmt.Value = souAmt
            End If

            If souCnt <> 0 OrElse hasu <> 0 Then
                .numSyukkaKosu.Value = souCnt
                .numSyukkaHasu.Value = hasu
            ElseIf (LMC040C.EventShubetsu.KENSAKU).Equals(eventShubetsu) = False AndAlso _
                souCnt = 0 AndAlso _
                (.optKowake.Checked = True OrElse .optSample.Checked = True) Then
                .numSyukkaKosu.Value = 1
                .numSyukkaHasu.Value = 0
                kosu = 1
            End If

            If .optSample.Checked = True Then  'サンプルチェック時
                'START YANAI 20110906 サンプル対応
                '.sprZaiko.SetCellValue(.sprZaiko.ActiveSheet.ActiveRowIndex, sprZaiko.HIKI_CNT.ColNo, "0")  '引当個数
                'END YANAI 20110906 サンプル対応
                .numSyukkaKosu.Value = 0
                .numSyukkaHasu.Value = 0
                kosu = 0
            End If

            .numHikiZanCnt.Value = kosu - Convert.ToDecimal(.numHikiSumiCnt.Value)
            .numHikiZanAmt.Value = Convert.ToDecimal(.numSyukkaSouAmt.Value) - Convert.ToDecimal(.numHikiSumiAmt.Value)
            .numSyukkaSouCnt.Value = kosu
            Return True

        End With

    End Function

    ''' <summary>
    ''' 引当個数・引当数量・引当個数合計・引当数量合計を求める
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHikiSum(ByVal eventShubetsu As LMC040C.EventShubetsu)

        Dim kosu As Decimal = 0
        Dim suryo As Decimal = 0
        Dim amari As Decimal = 0
        Dim suryo2 As Decimal = 0

        Dim kosuFlg As Boolean = True

        With Me._Frm

            Dim rowNo As Integer = .sprZaiko.ActiveSheet.ActiveRowIndex
            Dim colNo As Integer = .sprZaiko.ActiveSheet.ActiveColumnIndex

            If rowNo = 0 Then
                Exit Sub
            End If

            If .optCnt.Checked = True Then
                '引当個数変更時
                If ("00").Equals(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.SMPL_FLG.ColNo)).ToString()) = True Then
                    '小分け在庫以外の時
                    suryo = Convert.ToDecimal( _
                            Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_CNT.ColNo)).ToString()) _
                          * Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.IRIME.ColNo)).ToString()))
                    suryo2 = Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo)).ToString())
                    If suryo > suryo2 Then
                        suryo = suryo2
                    End If
                Else
                    If ("0").Equals(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_CNT.ColNo)).ToString()) = False Then
                        suryo = Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo)).ToString())
                    End If

                End If

                kosuFlg = True

            Else

                '引当数量変更時
                If .optAmt.Checked = True Then
                    '数量選択時

                    If ("00").Equals(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.SMPL_FLG.ColNo)).ToString()) = True Then
                        '小分け在庫以外の時
                        kosu = Me.CalcData( _
                               Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_AMT.ColNo))) _
                             , Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.IRIME.ColNo))))

                        amari = Me.CalcDataMod( _
                                Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_AMT.ColNo))) _
                                , Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.IRIME.ColNo))))
                    Else
                        kosu = Me.CalcData( _
                               Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_AMT.ColNo))) _
                                , Convert.ToDecimal(.numIrime.Value))

                        amari = Me.CalcDataMod( _
                                Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_AMT.ColNo))) _
                                , Convert.ToDecimal(.numIrime.Value))

                    End If

                ElseIf .optKowake.Checked = True Then
                    '小分け選択時
                    kosu = 0
                    amari = 0
                ElseIf .optSample.Checked = True Then
                    'サンプル選択時
                    kosu = 0
                    amari = 0
                End If

                kosuFlg = False

            End If

            '値をチェック
            If _V.CalHikiCheck(kosu, suryo, amari) = False Then
                Exit Sub
            End If

            If .optSample.Checked = False AndAlso .optKowake.Checked = False Then 'サンプル、小分け以外チェック時

                'START YANAI 要望番号821
                'If kosuFlg = False AndAlso _
                '    Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_CNT.ColNo))) = 0 Then
                '    .sprZaiko.SetCellValue(rowNo, sprZaiko.HIKI_CNT.ColNo, Convert.ToString(kosu))  '引当個数
                'End If
                If kosuFlg = False  Then
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.HIKI_CNT.ColNo, Convert.ToString(kosu))  '引当個数
                End If
                'END YANAI 要望番号821

                'START YANAI 要望番号821
                'If kosuFlg = True AndAlso _
                '    Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_AMT.ColNo))) = 0 Then
                '    .sprZaiko.SetCellValue(rowNo, sprZaiko.HIKI_AMT.ColNo, Convert.ToString(suryo)) '引当数量
                'End If
                If kosuFlg = True  Then
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.HIKI_AMT.ColNo, Convert.ToString(suryo)) '引当数量
                End If
                'END YANAI 要望番号821

            ElseIf .optSample.Checked = True Then  'サンプルチェック時
                .sprZaiko.SetCellValue(rowNo, sprZaiko.HIKI_CNT.ColNo, "0")  '引当個数
                'START YANAI 20110906 サンプル対応
                '.sprZaiko.SetCellValue(rowNo, sprZaiko.HIKI_AMT.ColNo, "0")  '引当数量
                'END YANAI 20110906 サンプル対応
                .numSyukkaKosu.Value = 0
                .numSyukkaHasu.Value = 0

            End If


            '引当個数合計、引当数量合計を求める
            Dim max As Integer = .sprZaiko.ActiveSheet.Rows.Count - 1
            kosu = 0
            suryo = 0

            For i As Integer = 1 To max

                If Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.DEF.ColNo)).Equals(LMConst.FLG.ON) = True Then
                    '引当個数加算
                    kosu = kosu + _
                           Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_CNT.ColNo)))

                    '引当数量加算
                    suryo = suryo + _
                            Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo)))

                    If (LMC040C.EventShubetsu.SENTAKU).Equals(eventShubetsu) = True AndAlso _
                        .optKowake.Checked = True AndAlso _
                        Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_CNT.ColNo))) = 0 AndAlso _
                        Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo))) = Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo))) Then
                        'ここに来るパターンは、
                        '「選択」ボタン押下時、出荷単位が小分けで、スプレッドの引当数量を自分で入力した時の全量出荷の場合のみ。
                        'チェックをオンにして、引当数量を自動で設定する場合はここにはこない。
                        .sprZaiko.SetCellValue(i, sprZaiko.HIKI_CNT.ColNo, "1")
                        kosu = kosu + 1
                    End If
                End If

            Next i

            '値をチェック
            If _V.CalHikiGokeiCheck(kosu, suryo) = False Then
                Exit Sub
            End If

            'START YANAI 20110906 サンプル対応
            'If .optSample.Checked = False Then  
            '    .numHikiCntSum.Value = kosu + Convert.ToDecimal(.numHikiSumiCnt.Value)
            '    .numHikiAmtSum.Value = suryo + Convert.ToDecimal(.numHikiSumiAmt.Value)
            'Else
            '    .numHikiCntSum.Value = 0
            '    .numHikiAmtSum.Value = 0
            'End If
            If .optSample.Checked = False Then
                .numHikiCntSum.Value = kosu + Convert.ToDecimal(.numHikiSumiCnt.Value)
            Else
                .numHikiCntSum.Value = 0
            End If
            .numHikiAmtSum.Value = suryo + Convert.ToDecimal(.numHikiSumiAmt.Value)
            'END YANAI 20110906 サンプル対応

        End With

    End Sub

    ''' <summary>
    ''' 個数・数量変更時、チェックボックスのオンオフ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCheckOnOff()

        Dim kosu As String = String.Empty
        Dim suryo As String = String.Empty

        With Me._Frm

            Dim rowNo As Integer = .sprZaiko.ActiveSheet.ActiveRowIndex
            Dim colNo As Integer = .sprZaiko.ActiveSheet.ActiveColumnIndex
            If rowNo = 0 OrElse colNo = 0 Then
                Exit Sub
            End If

            kosu = Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_CNT.ColNo)).ToString()
            suryo = Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(rowNo, LMC040G.sprZaiko.HIKI_AMT.ColNo)).ToString()

            If (LMC040G.sprZaiko.HIKI_CNT.ColNo).Equals(colNo) = True Then
                '引当個数にフォーカスが合っている場合
                If ("0").Equals(kosu) = False Then
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.DEF.ColNo, "True")
                Else

                    If ("0").Equals(.numSyukkaKosu.Value.ToString) = False Then

                    Else
                        .sprZaiko.SetCellValue(rowNo, sprZaiko.DEF.ColNo, "False")
                    End If

                End If

            ElseIf (LMC040G.sprZaiko.HIKI_AMT.ColNo).Equals(colNo) = True Then
                '引当数量にフォーカスが合っている場合
                If ("0").Equals(suryo) = False Then
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.DEF.ColNo, "True")
                Else
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.DEF.ColNo, "False")
                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' AllZero時、引当個数・数量を自動で設定する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetAllZero()

        With Me._Frm
            Dim kosu As Decimal = Convert.ToDecimal(.numSyukkaSouCnt.Value) '出荷個数
            Dim sumiKosu As Decimal = Convert.ToDecimal(.numHikiSumiCnt.Value) '引当済個数
            Dim hikiKosu As Decimal = sumiKosu  '引当個数合計
            Dim wkosu As Decimal = kosu - hikiKosu

            Dim suryo As Decimal = Convert.ToDecimal(.numSyukkaSouAmt.Value) '出荷数量
            Dim sumiSuryo As Decimal = Convert.ToDecimal(.numHikiSumiAmt.Value) '引当済数量
            Dim hikiSuryo As Decimal = sumiSuryo  '引当数量合計
            Dim wsuryo As Decimal = suryo - hikiSuryo

            Dim kanoKosu As Decimal = 0  '引当可能個数(各レコード）
            Dim kanoSuryo As Decimal = 0  '引当可能数量(各レコード）
            Dim sumkanoSuryo As Decimal = 0  '引当可能数量(各レコード）

            Dim irime As Decimal = Convert.ToDecimal(.numIrime.Value)  '入目

            Dim endFlg As Boolean = False

            'チェックリスト格納変数
            Dim list As ArrayList = New ArrayList()
            list = Me._V.getCheckList()
            Dim max As Integer = list.Count() - 1

            For i As Integer = 0 To max
                kanoKosu = Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMC040G.sprZaiko.HIKI_KANO_CNT.ColNo)).ToString())
                kanoSuryo = Convert.ToDecimal(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo)).ToString())
                If .optKowake.Checked = False AndAlso .optSample.Checked = False Then
                    If endFlg = False AndAlso kanoKosu < wkosu Then
                        '対象列の引当可能個数すべてを引当てる
                        '引当個数
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_CNT.ColNo, kanoKosu.ToString())
                        '引当数量
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, kanoSuryo.ToString())

                        hikiKosu = hikiKosu + kanoKosu

                        wkosu = wkosu - kanoKosu

                    ElseIf endFlg = False AndAlso kanoKosu >= wkosu Then
                        '対象列の引当可能個数の内、必要な分を引当てる
                        '引当個数
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_CNT.ColNo, wkosu.ToString())

                        If ("00").Equals(Me._LMCconG.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMC040G.sprZaiko.SMPL_FLG.ColNo)).ToString()) = True Then
                            '小分け以外の場合
                            '引当数量
                            If kanoSuryo >= wkosu * irime Then
                                .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, (wkosu * irime).ToString())
                            Else
                                .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, (kanoSuryo).ToString())
                            End If
                            
                        Else
                            '小分けの場合
                            .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, kanoSuryo.ToString())

                        End If

                        hikiKosu = kosu

                        wkosu = 0

                        endFlg = True

                    ElseIf endFlg = True Then
                        '引当個数
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_CNT.ColNo, "0")
                        '引当数量
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, "0")
                        'チェックをオフにする
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.DEF.ColNo, LMConst.FLG.OFF)
                    End If

                ElseIf .optKowake.Checked = True Then
                    If i = 0 Then
                        '対象列の引当可能個数すべてを引当てる
                        '引当個数
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_CNT.ColNo, "1")
                        '引当数量
                        If kanoSuryo < suryo Then
                            suryo = kanoSuryo
                        End If
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, suryo.ToString())
                        sumkanoSuryo = sumkanoSuryo + suryo
                    Else

                        'チェックをオフにする
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.DEF.ColNo, LMConst.FLG.OFF)
                    End If

                ElseIf .optSample.Checked = True Then
                    If i = 0 Then
                        '対象列の引当可能個数すべてを引当てる
                        '引当個数
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_CNT.ColNo, "0")
                        '引当数量
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.HIKI_AMT.ColNo, suryo.ToString())
                    Else

                        'チェックをオフにする
                        .sprZaiko.SetCellValue(Convert.ToInt32(list(i)), sprZaiko.DEF.ColNo, LMConst.FLG.OFF)
                    End If
                End If

            Next

            If .optKowake.Checked = False AndAlso .optSample.Checked = False Then

                .numHikiCntSum.Value = hikiKosu '引当個数合計
                .numHikiZanCnt.Value = Convert.ToDecimal(.numSyukkaSouCnt.Value) - Convert.ToDecimal(.numHikiSumiCnt.Value)  '引当残個数

                .numHikiAmtSum.Value = Convert.ToDecimal(.numHikiCntSum.Value) * irime   '引当数量合計
                .numHikiZanAmt.Value = Convert.ToDecimal(.numHikiZanCnt.Value) * irime   '引当残数量

            ElseIf .optKowake.Checked = True Then

                .numHikiCntSum.Value = 1 '引当個数合計
                .numHikiZanCnt.Value = 1  '引当残個数

                .numHikiAmtSum.Value = sumkanoSuryo   '引当数量合計
                .numHikiZanAmt.Value = suryo   '引当残数量

            ElseIf .optSample.Checked = True Then

                .numHikiCntSum.Value = 0 '引当個数合計
                .numHikiZanCnt.Value = 0  '引当残個数

                .numHikiAmtSum.Value = suryo   '引当数量合計
                .numHikiZanAmt.Value = suryo   '引当残数量

            End If

        End With

    End Sub

    ''' <summary>
    ''' スプレッド 状態 荷主 コンボボックス設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetComboJyotai() As StyleInfo

        Dim sort As String = "JOTAI_CD"
        Dim getDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUSTCOND)
        getDt.Rows.Clear()
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUSTCOND).Select( _
                                                                                String.Concat( _
                                                                                "NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, "' ", _
                                                                                "AND CUST_CD_L = '", Me._Frm.lblCustCD_L.TextValue, "' ", _
                                                                                "AND SYS_DEL_FLG = '0'"), _
                                                                                sort)

        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max
            getDt.ImportRow(getDr(i))
        Next

        Dim cmb As StyleInfo = LMSpreadUtility.GetComboCell(Me._Frm.sprZaiko, New DataView(getDt), "JOTAI_CD", "JOTAI_NM", False)

        Return cmb

    End Function

    ''' <summary>
    ''' 商品キーを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetGoodsNrs()

        '商品キーをキャッシュから取得し、画面項目に設定
        Me._Frm.lblGoodsNRS.TextValue = GetCachedGoods(Me._Frm)

    End Sub

    ''' <summary>
    ''' 商品キャッシュから商品キー取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedGoods(ByVal frm As LMC040F) As String

        With frm

            Dim dr As DataRow() = Nothing

            '商品キー
            '---↓
            'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat( _
            '                                                                   "NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND " _
            '                                                                 , "GOODS_CD_CUST = '", .lblGoodsCD.TextValue, "' AND " _
            '                                                                 , "CUST_CD_L = '", .lblCustCD_L.TextValue, "' AND " _
            '                                                                 , "CUST_CD_M = '", .lblCustCD_M.TextValue, "' AND " _
            '                                                                 , "SYS_DEL_FLG = '0'"))

            Dim goodsDs As MGoodsDS = New MGoodsDS
            Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
            goodsDr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            goodsDr.Item("GOODS_CD_CUST") = .lblGoodsCD.TextValue
            goodsDr.Item("CUST_CD_L") = .lblCustCD_L.TextValue
            goodsDr.Item("CUST_CD_M") = .lblCustCD_M.TextValue
            goodsDr.Item("SYS_DEL_FLG") = "0"
            goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
            Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
            dr = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
            '---↑

            If 0 < dr.Length Then
                Return dr(0).Item("GOODS_CD_NRS").ToString
            End If

            Return String.Empty

        End With

    End Function

    'START YANAI 要望番号389
    ''' <summary>
    ''' 他荷主判定処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function chkTaninusi(ByVal frm As LMC040F) As Boolean

        Dim max As Integer = frm.sprZaiko.ActiveSheet.Rows.Count - 1
        Dim allZeroFlg As Boolean = True

        For i As Integer = 1 To max

            If ("0").Equals(Me._LMCconG.GetCellValue(frm.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_KANO_CNT.ColNo)).ToString()) = False OrElse _
                ("0").Equals(Me._LMCconG.GetCellValue(frm.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo)).ToString()) Then
                allZeroFlg = False
            Else
                frm.sprZaiko.ActiveSheet.Rows(i).Remove()
                i = i - 1
                max = max - 1
            End If
            If i = max Then
                Exit For
            End If

        Next

        Return allZeroFlg

    End Function
    'END YANAI 要望番号389

    ''' <summary>
    ''' ゼロ割回避処理
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcData(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 / value2

    End Function

    ''' <summary>
    ''' ゼロ割回避処理(あまり値を返却)
    ''' </summary>
    ''' <param name="value1">分子の値</param>
    ''' <param name="value2">分母の値</param>
    ''' <returns>分母が0の場合、0を返却</returns>
    ''' <remarks></remarks>
    Private Function CalcDataMod(ByVal value1 As Decimal, ByVal value2 As Decimal) As Decimal

        If value2 = 0 Then
            Return 0
        End If

        Return value1 Mod value2

    End Function

    ''' <summary>
    ''' 10キー「+」ボタン押下時、チェックボックスのオンオフ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCheckOnOffKeysAdd(ByVal frm As LMC040F)

        With Me._Frm

            Dim rowNo As Integer = .sprZaiko.ActiveSheet.ActiveRowIndex
            Dim colNo As Integer = .sprZaiko.ActiveSheet.ActiveColumnIndex
            If rowNo = 0 OrElse colNo = 0 Then
                Exit Sub
            End If

            If (LMC040G.sprZaiko.HIKI_CNT.ColNo).Equals(colNo) = True Then

                'If .sprZaiko.EditMode = True Then
                '    Console.Write("[G1342] ①mode=true→false" & vbNewLine)
                '    '編集を完了し、データモデルを一時的に更新します
                '    .sprZaiko.EditMode = False

                '    Console.Write("[G1346] ①mode=false→true" & vbNewLine)
                '    'セルを編集状態に戻します
                '    .sprZaiko.EditMode = True
                'End If

                Dim chk As String = .sprZaiko.ActiveSheet.GetValue(rowNo, LMC040G.sprZaiko.DEF.ColNo).ToString


                '引当個数にフォーカスが合っている場合
                If (True.ToString).Equals(chk) = True Then
                    Console.Write("[G1356] チェック=off" & vbNewLine)
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.DEF.ColNo, "False")
                Else
                    Console.Write("[G1359] チェック=on" & vbNewLine)
                    .sprZaiko.SetCellValue(rowNo, sprZaiko.DEF.ColNo, "True")
                End If

                'If .sprZaiko.EditMode = True Then
                '    Console.Write("[G1364] ②mode=true→false" & vbNewLine)
                '    '編集を完了し、データモデルを一時的に更新します
                '    .sprZaiko.EditMode = False

                '    Console.Write("[G1368] ②mode=false→true" & vbNewLine)
                '    'セルを編集状態に戻します
                '    .sprZaiko.EditMode = True
                'End If

            End If

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
