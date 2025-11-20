' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG030G : 保管料荷役料明細編集
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMG030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMG030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG030F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMGControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG030F, ByVal g As LMGControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <param name="State"></param>
    ''' <param name="varStrageFlg">変動保管料フラグ</param>
    Friend Sub SetFunctionKey(ByVal State As Boolean, Optional ByVal varStrageFlg As String = "0")

        Dim always As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMGControlC.FUNCTION_HENSHU
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = LMGControlC.FUNCTION_TORIKOMI
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMGControlC.FUNCTION_KENSAKU
            .F10ButtonName = String.Empty
            .F11ButtonName = LMGControlC.FUNCTION_HOZON
            .F12ButtonName = LMGControlC.FUNCTION_TOJIRU

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
            .F12ButtonEnabled = lock

            If State = True Then
                .F2ButtonEnabled = always
                .F5ButtonEnabled = always
                .F9ButtonEnabled = always
                .F12ButtonEnabled = always
            Else
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
            End If

            '変動保管料対象荷主ならば編集ボタンは常に使用不可
            If "1".Equals(varStrageFlg) Then
                .F2ButtonEnabled = lock
            End If

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpShuturyoku.TabIndex = LMG030C.CtlTabIndex.GRP_SHUTURYOKU
            .lblJobNo.TabIndex = LMG030C.CtlTabIndex.LBL_JOBNO
            .lblCustCdL.TabIndex = LMG030C.CtlTabIndex.LBL_CUST_L
            .lblCustCdM.TabIndex = LMG030C.CtlTabIndex.LBL_CUST_M
            .lblCustCdS.TabIndex = LMG030C.CtlTabIndex.LBL_CUST_S
            .lblCustCdSS.TabIndex = LMG030C.CtlTabIndex.LBL_CUST_SS
            .lblCustNm.TabIndex = LMG030C.CtlTabIndex.LBL_CUST_NM
            .cmbPrint.TabIndex = LMG030C.CtlTabIndex.CMB_PRINT
            .btnPrint.TabIndex = LMG030C.CtlTabIndex.BTN_PRINT
            'START YANAI 20111014 一括変更追加
            .cmbIkkatu.TabIndex = LMG030C.CtlTabIndex.CMB_IKKATU
            .numIkkatu.TabIndex = LMG030C.CtlTabIndex.TXT_IKKATU
            .btnIkkatu.TabIndex = LMG030C.CtlTabIndex.BTN_IKKATU
            'END YANAI 20111014 一括変更追加
            .lblGoodsCdCust.TabIndex = LMG030C.CtlTabIndex.LBL_GOODS_CD_CUST
            .lblGoodsNm1.TabIndex = LMG030C.CtlTabIndex.LBL_GOODS_NM1
            .lblGoodsCdNrs.TabIndex = LMG030C.CtlTabIndex.LBL_GOODS_CD_NRS
            .lblLotNo.TabIndex = LMG030C.CtlTabIndex.LBL_LOTNO
            .lblSerialNo.TabIndex = LMG030C.CtlTabIndex.LBL_SERIALNO
            .cmbNbUt.TabIndex = LMG030C.CtlTabIndex.CMB_NB_UT
            .lblIrimeNb.TabIndex = LMG030C.CtlTabIndex.LBL_IRIME_NB
            .cmbIrimeUt.TabIndex = LMG030C.CtlTabIndex.CMB_IRIME_UT
            .cmbTaxKb.TabIndex = LMG030C.CtlTabIndex.CMB_TAX_KB
            .numSekiNb1.TabIndex = LMG030C.CtlTabIndex.TXT_SEKI_NB1
            .numSekiNb2.TabIndex = LMG030C.CtlTabIndex.TXT_SEKI_NB2
            .numSekiNb3.TabIndex = LMG030C.CtlTabIndex.TXT_SEKI_NB3
            .numHokanTnk1.TabIndex = LMG030C.CtlTabIndex.TXT_HOKAN_TNK1
            .numHokanTnk2.TabIndex = LMG030C.CtlTabIndex.TXT_HOKAN_TNK2
            .numHokanTnk3.TabIndex = LMG030C.CtlTabIndex.TXT_HOKAN_TNK3
            'START YANAI 20111013 保管料自動計算廃止
            '.lblHokanAmt.TabIndex = LMG030C.CtlTabIndex.TXT_HOKAN_AMT
            .numHokanAmt.TabIndex = LMG030C.CtlTabIndex.TXT_HOKAN_AMT
            'END YANAI 20111013 保管料自動計算廃止
            .numVarHokanAmt.TabIndex = LMG030C.CtlTabIndex.TXT_VAR_HOKAN_AMT
            .numInNb.TabIndex = LMG030C.CtlTabIndex.TXT_IN_NB
            .numNiyakuInTnk1.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_IN_TNK1
            .numNiyakuInTnk2.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_IN_TNK2
            .numNiyakuInTnk3.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_IN_TNK3
            .numOutNb.TabIndex = LMG030C.CtlTabIndex.TXT_OUT_NB
            .numNiyakuOutTnk1.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_OUT_TNK1
            .numNiyakuOutTnk2.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_OUT_TNK2
            .numNiyakuOutTnk3.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_OUT_TNK3
            .numNiyakuAmt.TabIndex = LMG030C.CtlTabIndex.TXT_NIYAKU_AMT

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByVal ds As DataSet)

        Dim CustNm As DataRow() = Nothing

        Dim dt As DataTable = ds.Tables(LMG030C.TABLE_NM_IN)
        Dim dr As DataRow = dt.Rows(0)

        Dim NrsBrCd As String = dr.Item("NRS_BR_CD").ToString()
        Dim InvDateTo As String = dr.Item("INV_DATE_TO").ToString()
        Dim jobno As String = dr.Item("JOB_NO").ToString()
        Dim CustCdL As String = dr.Item("CUST_CD_L").ToString()
        Dim CustCdM As String = dr.Item("CUST_CD_M").ToString()
        Dim CustCdLM As String = dr.Item("CUST_NM_L_M").ToString()
        Dim CustNms As String = String.Empty

        Dim d9 As Decimal = Convert.ToDecimal("999999999")
        Dim d9_3 As Decimal = Convert.ToDecimal("999999999.999")
        Dim d6_3 As Decimal = Convert.ToDecimal("999999.999")

        With Me._Frm
            .lblNrsBrCd.TextValue = NrsBrCd
            .lblInvDateTo.TextValue = InvDateTo
            .lblJobNo.TextValue = jobno
            .lblCustCdL.TextValue = CustCdL
            .lblCustCdM.TextValue = CustCdM
            .lblCustNm.TextValue = CustCdLM

            .lblIrimeNb.SetInputFields("###,##0.000", , 6, 1, , 3, 3, , d6_3, 0)
            .numHokanTnk1.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numHokanTnk2.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numHokanTnk3.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numInNb.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numNiyakuAmt.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0)
            .numNiyakuInTnk1.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numNiyakuInTnk2.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numNiyakuInTnk3.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numNiyakuOutTnk1.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numNiyakuOutTnk2.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numNiyakuOutTnk3.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numOutNb.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numSekiNb1.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numSekiNb2.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numSekiNb3.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            'START YANAI 20111013 保管料自動計算廃止
            .numHokanAmt.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0)
            'END YANAI 20111013 保管料自動計算廃止
            .numVarHokanAmt.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0)
            'START YANAI 20111014 一括変更追加
            .numIkkatu.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0)
            'END YANAI 20111014 一括変更追加

            .cmbPrint.SelectedValue = "01"

        End With

    End Sub

    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW, _
                                Optional ByVal recSts As String = RecordStatus.NOMAL_REC)

        With Me._Frm
            .lblSituation.DispMode = dispMd
            .lblSituation.RecordStatus = recSts
        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal dispmode As Boolean)

        With Me._Frm

            'START YANAI 20111013 保管料自動計算廃止
            '.lblHokanAmt.ReadOnly = True
            'END YANAI 20111013 保管料自動計算廃止
            'START YANAI 20111014 ロック時ReadOnly
            'If dispmode = True Then

            '    .btnPrint.Enabled = False
            '    .cmbPrint.Enabled = False
            '    .numSekiNb1.Enabled = True
            '    .numSekiNb2.Enabled = True
            '    .numSekiNb3.Enabled = True
            '    .numHokanTnk1.Enabled = True
            '    .numHokanTnk2.Enabled = True
            '    .numHokanTnk3.Enabled = True
            '    .numInNb.Enabled = True
            '    .numNiyakuInTnk1.Enabled = True
            '    .numNiyakuInTnk2.Enabled = True
            '    .numNiyakuInTnk3.Enabled = True
            '    .numOutNb.Enabled = True
            '    .numNiyakuOutTnk1.Enabled = True
            '    .numNiyakuOutTnk2.Enabled = True
            '    .numNiyakuOutTnk3.Enabled = True
            '    .numNiyakuAmt.Enabled = True
            '    'START YANAI 20111013 保管料自動計算廃止
            '    .numHokanAmt.Enabled = True
            '    'END YANAI 20111013 保管料自動計算廃止
            '    'START YANAI 20111014 一括変更追加
            '    .cmbIkkatu.Enabled = False
            '    .numIkkatu.Enabled = False
            '    .btnIkkatu.Enabled = False
            '    'END YANAI 20111014 一括変更追加

            'ElseIf dispmode = False Then

            '    .btnPrint.Enabled = True
            '    .cmbPrint.Enabled = True
            '    .numSekiNb1.Enabled = False
            '    .numSekiNb2.Enabled = False
            '    .numSekiNb3.Enabled = False
            '    .numHokanTnk1.Enabled = False
            '    .numHokanTnk2.Enabled = False
            '    .numHokanTnk3.Enabled = False
            '    .numInNb.Enabled = False
            '    .numNiyakuInTnk1.Enabled = False
            '    .numNiyakuInTnk2.Enabled = False
            '    .numNiyakuInTnk3.Enabled = False
            '    .numOutNb.Enabled = False
            '    .numNiyakuOutTnk1.Enabled = False
            '    .numNiyakuOutTnk2.Enabled = False
            '    .numNiyakuOutTnk3.Enabled = False
            '    .numNiyakuAmt.Enabled = False
            '    'START YANAI 20111013 保管料自動計算廃止
            '    .numHokanAmt.Enabled = False
            '    'END YANAI 20111013 保管料自動計算廃止
            '    'START YANAI 20111014 一括変更追加
            '    .cmbIkkatu.Enabled = True
            '    .numIkkatu.Enabled = True
            '    .btnIkkatu.Enabled = True
            '    'END YANAI 20111014 一括変更追加

            'End If
            If dispmode = True Then

                .btnPrint.Enabled = False
                .cmbPrint.Enabled = False
                .numSekiNb1.ReadOnly = False
                .numSekiNb2.ReadOnly = False
                .numSekiNb3.ReadOnly = False
                .numHokanTnk1.ReadOnly = False
                .numHokanTnk2.ReadOnly = False
                .numHokanTnk3.ReadOnly = False
                .numInNb.ReadOnly = False
                .numNiyakuInTnk1.ReadOnly = False
                .numNiyakuInTnk2.ReadOnly = False
                .numNiyakuInTnk3.ReadOnly = False
                .numOutNb.ReadOnly = False
                .numNiyakuOutTnk1.ReadOnly = False
                .numNiyakuOutTnk2.ReadOnly = False
                .numNiyakuOutTnk3.ReadOnly = False
                .numNiyakuAmt.ReadOnly = False
                'START YANAI 20111013 保管料自動計算廃止
                .numHokanAmt.ReadOnly = False
                'END YANAI 20111013 保管料自動計算廃止
                .numVarHokanAmt.ReadOnly = False
                'START YANAI 20111014 一括変更追加
                .cmbIkkatu.Enabled = False
                .numIkkatu.ReadOnly = True
                .btnIkkatu.Enabled = False
                'END YANAI 20111014 一括変更追加

            ElseIf dispmode = False Then

                .btnPrint.Enabled = True
                .cmbPrint.Enabled = True
                .numSekiNb1.ReadOnly = True
                .numSekiNb2.ReadOnly = True
                .numSekiNb3.ReadOnly = True
                .numHokanTnk1.ReadOnly = True
                .numHokanTnk2.ReadOnly = True
                .numHokanTnk3.ReadOnly = True
                .numInNb.ReadOnly = True
                .numNiyakuInTnk1.ReadOnly = True
                .numNiyakuInTnk2.ReadOnly = True
                .numNiyakuInTnk3.ReadOnly = True
                .numOutNb.ReadOnly = True
                .numNiyakuOutTnk1.ReadOnly = True
                .numNiyakuOutTnk2.ReadOnly = True
                .numNiyakuOutTnk3.ReadOnly = True
                .numNiyakuAmt.ReadOnly = True
                'START YANAI 20111013 保管料自動計算廃止
                .numHokanAmt.ReadOnly = True
                'END YANAI 20111013 保管料自動計算廃止
                .numVarHokanAmt.ReadOnly = True
                'START YANAI 20111014 一括変更追加
                .cmbIkkatu.Enabled = True
                .numIkkatu.ReadOnly = False
                .btnIkkatu.Enabled = True
                'END YANAI 20111014 一括変更追加

            End If
            'END YANAI 20111014 ロック時ReadOnly

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm

            .grpShuturyoku.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロールの背景色の初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal frm As LMG030F)

        With Me._Frm
            Me._ControlG.SetBackColor(.cmbPrint)
            Me._ControlG.SetBackColor(.numSekiNb1)
            Me._ControlG.SetBackColor(.numSekiNb2)
            Me._ControlG.SetBackColor(.numSekiNb3)
            Me._ControlG.SetBackColor(.numHokanTnk1)
            Me._ControlG.SetBackColor(.numHokanTnk2)
            Me._ControlG.SetBackColor(.numHokanTnk3)
            'START YANAI 20111013 保管料自動計算廃止
            'Me._ControlG.SetBackColor(.lblHokanAmt)
            Me._ControlG.SetBackColor(.numHokanAmt)
            'END YANAI 20111013 保管料自動計算廃止
            Me._ControlG.SetBackColor(.numVarHokanAmt)
            Me._ControlG.SetBackColor(.numInNb)
            Me._ControlG.SetBackColor(.numNiyakuInTnk1)
            Me._ControlG.SetBackColor(.numNiyakuInTnk2)
            Me._ControlG.SetBackColor(.numNiyakuInTnk3)
            Me._ControlG.SetBackColor(.numOutNb)
            Me._ControlG.SetBackColor(.numNiyakuOutTnk1)
            Me._ControlG.SetBackColor(.numNiyakuOutTnk2)
            Me._ControlG.SetBackColor(.numNiyakuOutTnk3)
            Me._ControlG.SetBackColor(.numNiyakuAmt)
            'START YANAI 20111014 一括変更追加
            Me._ControlG.SetBackColor(.cmbIkkatu)
            Me._ControlG.SetBackColor(.numIkkatu)
            'END YANAI 20111014 一括変更追加

        End With
    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="Row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal Row As Integer, ByVal V As LMGControlV)
        Dim MaxValue As Decimal = Convert.ToDecimal(999999999.999)
        Dim NiyakuValue As Decimal = 0
        Dim hokanamt As String = String.Empty
        '2011/08/16 菱刈 引数にRowの設定をしたためコメント化 スタート
        'Dim Row As Integer = e.Row()
        '2011/08/16 菱刈 引数にRowの設定をしたためコメント化 エンド
        With Me._Frm.sprMeisaiPrt.ActiveSheet
            NiyakuValue = Convert.ToDecimal(V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_RYO.ColNo)))
            If MaxValue <= NiyakuValue = True Then
                MaxValue = 999999999
            Else
                MaxValue = NiyakuValue
            End If
            'START YANAI 20111013 保管料自動計算廃止
            'Me._Frm.lblHokanAmt.Value = ToHalfAdjust(Convert.ToDecimal( _
            '                                         V.GetCellValue( _
            '                                         .Cells(Row, LMG030G.sprMeisaiPrtDef.HOKAN_RYO.ColNo))))               ' 保管料
            Me._Frm.numHokanAmt.Value = ToHalfAdjust(Convert.ToDecimal( _
                                                     V.GetCellValue( _
                                                     .Cells(Row, LMG030G.sprMeisaiPrtDef.HOKAN_RYO.ColNo))))               ' 保管料
            'END YANAI 20111013 保管料自動計算廃止
            Me._Frm.numVarHokanAmt.Value = ToHalfAdjust(Convert.ToDecimal(
                                                     V.GetCellValue(
                                                     .Cells(Row, LMG030G.sprMeisaiPrtDef.VAR_HOKAN_RYO.ColNo))))           ' 変動保管料
            Me._Frm.lblGoodsCdCust.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_GOODS_CD.ColNo))    ' 商品コード
            Me._Frm.lblGoodsNm1.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_GOODS_NM.ColNo))       ' 商品名
            Me._Frm.lblGoodsCdNrs.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NRS_GOODS.ColNo))         ' 商品KEY
            Me._Frm.lblLotNo.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.LOT_NO.ColNo))                 ' ロット№
            Me._Frm.lblSerialNo.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SERIAL_NO.ColNo))           ' シリアル№
            Me._Frm.cmbNbUt.SelectedValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NB_UT.ColNo))               ' 個数単位
            Me._Frm.lblIrimeNb.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.IRIME_NB.ColNo))                 ' 入目　数量
            Me._Frm.cmbIrimeUt.SelectedValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.IRIME_UT.ColNo))         ' 入目単位（コード）
            Me._Frm.cmbTaxKb.SelectedValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.TAX_KB.ColNo))             ' 課税区分
            Me._Frm.numSekiNb1.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SEKI_NB_1.ColNo))                ' 積数　1期
            Me._Frm.numSekiNb2.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SEKI_NB_2.ColNo))                ' 積数　2期
            Me._Frm.numSekiNb3.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SEKI_NB_3.ColNo))                ' 積数　3期
            Me._Frm.numHokanTnk1.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.HOKAN_1.ColNo))                ' 保管　1期単価
            Me._Frm.numHokanTnk2.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.HOKAN_2.ColNo))                ' 保管　2期単価
            Me._Frm.numHokanTnk3.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.HOKAN_3.ColNo))                ' 保管　3期単価
            Me._Frm.numInNb.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NYUKODAKA.ColNo))                   ' 入庫高
            Me._Frm.numNiyakuInTnk1.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_1.ColNo))         ' 荷役（入庫）　1期単価
            Me._Frm.numNiyakuInTnk2.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_2.ColNo))         ' 荷役（入庫）　2期単価
            Me._Frm.numNiyakuInTnk3.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_3.ColNo))         ' 荷役（入庫）　3期単価
            Me._Frm.numOutNb.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SYUKKODAKA.ColNo))                 ' 出庫高
            Me._Frm.numNiyakuOutTnk1.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo))       ' 荷役（出庫）　1期単価
            Me._Frm.numNiyakuOutTnk2.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_2.ColNo))       ' 荷役（出庫）　2期単価
            Me._Frm.numNiyakuOutTnk3.Value = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_3.ColNo))       ' 荷役（出庫）　3期単価
            Me._Frm.numNiyakuAmt.Value = MaxValue                                                                          ' 荷役料
            Me._Frm.lblCtlNo.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CTL_NO.ColNo))                 ' レコード番号
            Me._Frm.lblSysUpdDate.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SYS_UPD_DATE.ColNo))      ' 更新日付
            Me._Frm.lblSysUpdTime.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SYS_UPD_TIME.ColNo))      ' 更新時刻
            Me._Frm.lblCustCdL2.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_CD_L.ColNo))           ' 荷主コード（大）
            Me._Frm.lblCustCdM2.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_CD_M.ColNo))           ' 荷主コード（中）
            Me._Frm.lblCustCdS.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_CD_S.ColNo))            ' 荷主コード（小）
            Me._Frm.lblCustCdSS.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_CD_SS.ColNo))          ' 荷主コード（極小）
            Me._Frm.lblCustNm2.TextValue = String.Concat(Me._Frm.lblCustNm.TextValue _
                                                         , V.GetCellValue( _
                                                         .Cells(Row, LMG030G.sprMeisaiPrtDef.CUST_NM_S_SS.ColNo)))         ' 荷主名
            Me._Frm.lblInkaNo.TextValue = V.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.INKA_NO_L.ColNo))             ' 入荷管理番号

        End With

    End Sub

    ''' <summary>
    ''' 編集部のクリア
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub ClearFormData(ByVal frm As LMG030F)

        With Me._Frm.sprMeisaiPrt.ActiveSheet
            Me._Frm.lblGoodsCdCust.TextValue = String.Empty    ' 商品コード
            Me._Frm.lblGoodsNm1.TextValue = String.Empty       ' 商品名
            Me._Frm.lblGoodsCdNrs.TextValue = String.Empty     ' 商品KEY
            Me._Frm.lblLotNo.TextValue = String.Empty          ' ロット№
            Me._Frm.lblSerialNo.TextValue = String.Empty       ' シリアル№
            Me._Frm.cmbNbUt.SelectedValue = String.Empty       ' 個数単位
            Me._Frm.lblIrimeNb.Value = 0                       ' 入目　数量
            Me._Frm.cmbIrimeUt.SelectedValue = String.Empty    ' 入目単位（コード）
            Me._Frm.cmbTaxKb.SelectedValue = String.Empty      ' 課税区分
            Me._Frm.numSekiNb1.Value = 0                       ' 積数　1期
            Me._Frm.numSekiNb2.Value = 0                       ' 積数　2期
            Me._Frm.numSekiNb3.Value = 0                       ' 積数　3期
            Me._Frm.numHokanTnk1.Value = 0                     ' 保管　1期単価
            Me._Frm.numHokanTnk2.Value = 0                     ' 保管　2期単価
            Me._Frm.numHokanTnk3.Value = 0                     ' 保管　3期単価
            'START YANAI 20111013 保管料自動計算廃止
            'Me._Frm.lblHokanAmt.Value = 0                      ' 保管料
            Me._Frm.numHokanAmt.Value = 0                      ' 保管料
            'END YANAI 20111013 保管料自動計算廃止
            Me._Frm.numVarHokanAmt.Value = 0                   ' 変動保管料
            Me._Frm.numInNb.Value = 0                          ' 入庫高
            Me._Frm.numNiyakuInTnk1.Value = 0                  ' 荷役（入庫）　1期単価
            Me._Frm.numNiyakuInTnk2.Value = 0                  ' 荷役（入庫）　2期単価
            Me._Frm.numNiyakuInTnk3.Value = 0                  ' 荷役（入庫）　3期単価
            Me._Frm.numOutNb.Value = 0                         ' 出庫高
            Me._Frm.numNiyakuOutTnk1.Value = 0                 ' 荷役（出庫）　1期単価
            Me._Frm.numNiyakuOutTnk2.Value = 0                 ' 荷役（出庫）　2期単価
            Me._Frm.numNiyakuOutTnk3.Value = 0                 ' 荷役（出庫）　3期単価
            Me._Frm.numNiyakuAmt.Value = 0                     ' 荷役料
            Me._Frm.lblCtlNo.TextValue = String.Empty          'レコード番号
            Me._Frm.lblSysUpdDate.TextValue = String.Empty     '更新日付
            Me._Frm.lblSysUpdTime.TextValue = String.Empty     '更新時刻
            Me._Frm.lblCustCdL2.TextValue = String.Empty       '荷主コード（大）
            Me._Frm.lblCustCdM2.TextValue = String.Empty       '荷主コード（中）
            Me._Frm.lblCustCdS.TextValue = String.Empty        '荷主コード（小）
            Me._Frm.lblCustCdSS.TextValue = String.Empty       '荷主コード（極小）
            Me._Frm.lblCustNm2.TextValue = String.Empty        '荷主名
            Me._Frm.lblInkaNo.TextValue = String.Empty         '入荷管理番号
            'START YANAI 20111014 一括変更追加
            Me._Frm.numIkkatu.Value = 0                        ' 一括変更値
            'END YANAI 20111014 一括変更追加

        End With

    End Sub

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 一括変更部のクリア
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub ClearFormIkkatuData(ByVal frm As LMG030F)

        Dim d9 As Decimal = Convert.ToDecimal("999999999")
        Dim d9_3 As Decimal = Convert.ToDecimal("999999999.999")

        '一括変更値の入力形式変更
        If ("07").Equals(frm.cmbIkkatu.SelectedValue) = True Then
            '保管料の場合
            frm.numIkkatu.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0)
        Else
            '保管料以外の場合
            frm.numIkkatu.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
        End If

        '一括変更値のクリア
        frm.numIkkatu.Value = 0

    End Sub
    'END YANAI 20111014 一括変更追加

#End Region

#End Region

#Region "Spread"
    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMeisaiPrtDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.GOODS_CD, "商品コード", 100, True)
        Public Shared CUST_GOODS_NM As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.GOODS_NM, "商品名", 150, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.LOT_NO, "ロット№", 110, True)
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SERIAL_NO, "シリアル№", 110, True)
        Public Shared TANI As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.TANI, "単位", 40, True)
        Public Shared IRIME_NB As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.IRIME_SURYO, "入目", 90, True)
        Public Shared IRIME_UT_NM As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.IRIME_TANI, " ", 90, True)
        Public Shared TAX_KBN As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.ZEIKUBUN, String.Concat("課税", vbNewLine, "区分"), 40, True)
        Public Shared INKA_DATE As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.INKA_DATE, "入荷日", 80, True)
        Public Shared INKA_NO_L As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.INKA_NO_L, "入荷管理番号L", 110, True)
        Public Shared SEKI_NB_1 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SEKI_NB_1, "積数", 100, True)
        Public Shared SEKI_NB_2 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SEKI_NB_2, "", 100, True)
        Public Shared SEKI_NB_3 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SEKI_NB_3, "", 100, True)
        Public Shared NYUKODAKA As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NYUUKODAKA, "入庫高", 110, True)
        Public Shared SYUKKODAKA As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SYUKKODAKA, "出庫高", 110, True)
        Public Shared HOKAN_1 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.HOKAN_1, "保管", 200, True)
        Public Shared HOKAN_2 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.HOKAN_2, " ", 200, True)
        Public Shared HOKAN_3 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.HOKAN_3, " ", 200, True)
        Public Shared HOKAN_RYO As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.HOKANRYO, "保管料", 110, True)
        Public Shared VAR_RATE As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.VAR_RATE, "変動倍率", 60, True)
        Public Shared VAR_HOKAN_RYO As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.VAR_HOKANRYO, "変動保管料", 110, True)
        Public Shared NIYAKU_IN_1 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKU_IN_1, "荷役(入庫)", 200, True)
        Public Shared NIYAKU_IN_2 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKU_IN_2, " ", 200, True)
        Public Shared NIYAKU_IN_3 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKU_IN_3, " ", 200, True)
        Public Shared NIYAKU_OUT_1 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKU_OUT_1, "荷役(出庫)", 200, True)
        Public Shared NIYAKU_OUT_2 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKU_OUT_2, " ", 200, True)
        Public Shared NIYAKU_OUT_3 As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKU_OUT_3, " ", 200, True)
        Public Shared NIYAKU_RYO As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NIYAKURYO, "荷役料", 110, True)
        Public Shared NRS_GOODS As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NRSGOODS_CD, "商品KEY", 110, True)
        Public Shared CUST_NM_S_SS As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.CUST_NM_S_SS, "荷主名小・極小", 110, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.CUST_CD_L, "荷主コードL", 110, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.CUST_CD_M, "荷主コードM", 110, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.CUST_CD_S, "荷主コードS", 110, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.CUST_CD_SS, "荷主コードSS", 110, False)
        Public Shared CTL_NO As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.CTL_NO, "レコード番号", 110, False)
        Public Shared IRIME_UT As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.IRIME_UT, "入目単位（コード）", 110, False)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.TAX_KB, "課税区分", 110, False)
        Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.NB_UT, "個数単位", 110, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SYS_UPD_DATE, "更新日付", 110, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMG030C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 110, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprMeisaiPrt

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 41

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprSagyo.SetColProperty(New sprDetailDef)
            .SetColProperty(New LMG030G.sprMeisaiPrtDef(), False)
            '2015.10.15 英語化対応END

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.SetColProperty(New sprMeisaiPrtDef)

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprMeisaiPrtDef.TAX_KBN.ColNo + 1

            'ヘッダの連結
            .ActiveSheet.ColumnHeaderRowCount = 2
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.DEF.ColNo, 2, 1)             'チェックボックス
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_GOODS_CD.ColNo, 2, 1)   '荷主商品コード
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_GOODS_NM.ColNo, 2, 1)   '商品名
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.LOT_NO.ColNo, 2, 1)          'ロット№
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.SERIAL_NO.ColNo, 2, 1)       'シリアル№
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.IRIME_NB.ColNo, 1, 2)        '入目
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.TANI.ColNo, 2, 1)            '単位
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.TAX_KBN.ColNo, 2, 1)         '税区分
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.INKA_DATE.ColNo, 2, 1)       '入荷日
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.INKA_NO_L.ColNo, 2, 1)       '入荷管理番号L
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.SEKI_NB_1.ColNo, 1, 3)       '積数
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.NYUKODAKA.ColNo, 2, 1)       '入庫高
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.SYUKKODAKA.ColNo, 2, 1)      '出庫高
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.HOKAN_1.ColNo, 1, 3)         '保管
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.HOKAN_RYO.ColNo, 2, 1)       '保管料
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.VAR_RATE.ColNo, 2, 1)        '変動倍率
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.VAR_HOKAN_RYO.ColNo, 2, 1)   '変動保管料
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_1.ColNo, 1, 3)     '荷役(IN)
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo, 1, 3)    '荷役(OUT)
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.NIYAKU_RYO.ColNo, 2, 1)      '荷役料
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.NRS_GOODS.ColNo, 2, 1)       'NRS商品コード
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_NM_S_SS.ColNo, 2, 1)    '荷主名（小・極小）
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_CD_L.ColNo, 2, 1)       '荷主コードL
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_CD_M.ColNo, 2, 1)       '荷主コードM
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_CD_S.ColNo, 2, 1)       '荷主コードS
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CUST_CD_SS.ColNo, 2, 1)      '荷主コードSS
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.CTL_NO.ColNo, 2, 1)          'レコード番号
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.IRIME_UT.ColNo, 2, 1)        '入力単位（レコード）
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.TAX_KB.ColNo, 2, 1)          '課税区分
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.NB_UT.ColNo, 2, 1)           '個数単位
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.SYS_UPD_DATE.ColNo, 2, 1)    '更新日付
            .ActiveSheet.AddColumnHeaderSpanCell(0, LMG030G.sprMeisaiPrtDef.SYS_UPD_TIME.ColNo, 2, 1)    '更新時刻

            'ヘッダ(2行目)に列タイトル設定
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.IRIME_NB.ColNo).Text = "数量"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.IRIME_UT_NM.ColNo).Text = "単位"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.SEKI_NB_1.ColNo).Text = "1期"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.SEKI_NB_2.ColNo).Text = "2期"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.SEKI_NB_3.ColNo).Text = "3期"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.HOKAN_1.ColNo).Text = "1期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.HOKAN_2.ColNo).Text = "2期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.HOKAN_3.ColNo).Text = "3期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_1.ColNo).Text = "1期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_2.ColNo).Text = "2期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_3.ColNo).Text = "3期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo).Text = "1期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_2.ColNo).Text = "2期単価"
            '.ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_3.ColNo).Text = "3期単価"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.IRIME_NB.ColNo).Text = "数量(Q'ty)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.IRIME_UT_NM.ColNo).Text = "単位(Unit)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.SEKI_NB_1.ColNo).Text = "1期(1Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.SEKI_NB_2.ColNo).Text = "2期(2Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.SEKI_NB_3.ColNo).Text = "3期(3Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.HOKAN_1.ColNo).Text = "1期単価(UnitPrice 1Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.HOKAN_2.ColNo).Text = "2期単価(UnitPrice 2Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.HOKAN_3.ColNo).Text = "3期単価(UnitPrice 3Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_1.ColNo).Text = "1期単価(UnitPrice 1Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_2.ColNo).Text = "2期単価(UnitPrice 2Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_IN_3.ColNo).Text = "3期単価(UnitPrice 3Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo).Text = "1期単価(UnitPrice 1Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_2.ColNo).Text = "2期単価(UnitPrice 2Term)"
            .ActiveSheet.ColumnHeader.Cells(1, LMG030G.sprMeisaiPrtDef.NIYAKU_OUT_3.ColNo).Text = "3期単価(UnitPrice 3Term)"

            .ActiveSheet.Rows.Count = 1

            'セルに設定するスタイルの取得
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim num1_1 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9.9, True, 1, , ",")
            Dim num6_3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")
            Dim num9_3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")
            Dim num9 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")
            Dim numIrime As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, True, 3, , ",")

            'スプレッド検索行の制御
            .SetCellStyle(0, sprMeisaiPrtDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 20, False))
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            .SetCellStyle(0, sprMeisaiPrtDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprMeisaiPrtDef.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 40, False))
            .SetCellStyle(0, sprMeisaiPrtDef.TANI.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.IRIME_NB.ColNo, numIrime)
            .SetCellStyle(0, sprMeisaiPrtDef.IRIME_UT_NM.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.TAX_KBN.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.INKA_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.INKA_NO_L.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.SEKI_NB_1.ColNo, num6_3)
            .SetCellStyle(0, sprMeisaiPrtDef.SEKI_NB_2.ColNo, num6_3)
            .SetCellStyle(0, sprMeisaiPrtDef.SEKI_NB_3.ColNo, num6_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NYUKODAKA.ColNo, num6_3)
            .SetCellStyle(0, sprMeisaiPrtDef.SYUKKODAKA.ColNo, num6_3)
            .SetCellStyle(0, sprMeisaiPrtDef.HOKAN_1.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.HOKAN_2.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.HOKAN_3.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.HOKAN_RYO.ColNo, num9)
            .SetCellStyle(0, sprMeisaiPrtDef.VAR_RATE.ColNo, num1_1)
            .SetCellStyle(0, sprMeisaiPrtDef.VAR_HOKAN_RYO.ColNo, num9)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_IN_1.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_IN_2.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_IN_3.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_OUT_2.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_OUT_3.ColNo, num9_3)
            .SetCellStyle(0, sprMeisaiPrtDef.NIYAKU_RYO.ColNo, num9)
            .SetCellStyle(0, sprMeisaiPrtDef.NRS_GOODS.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_NM_S_SS.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 120, False))
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_CD_L.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_CD_M.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_CD_S.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.CUST_CD_SS.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.CTL_NO.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.IRIME_UT.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.TAX_KB.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.NB_UT.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprMeisaiPrtDef.SYS_UPD_TIME.ColNo, lbl)

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMG030F)

        With frm.sprMeisaiPrt

            .SetCellValue(0, sprMeisaiPrtDef.CUST_GOODS_CD.ColNo, String.Empty)
            .SetCellValue(0, sprMeisaiPrtDef.CUST_GOODS_NM.ColNo, String.Empty)
            .SetCellValue(0, sprMeisaiPrtDef.LOT_NO.ColNo, String.Empty)
            .SetCellValue(0, sprMeisaiPrtDef.SERIAL_NO.ColNo, String.Empty)
            .SetCellValue(0, sprMeisaiPrtDef.NRS_GOODS.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSelectListData(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprMeisaiPrt
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMG030C.TABLE_NM_OUT)
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            Dim dRow As DataRow
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sLabelm As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Center)
            Dim sLabelr As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999.999, 9999999999.999, True, 3, , ",")
            Dim number As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, , ",")
            Dim num1_1 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9.9, 9.9, True, 1, , ",")

            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            'Dim dRow As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = tbl.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprMeisaiPrtDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_GOODS_NM.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprMeisaiPrtDef.SERIAL_NO.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.TANI.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.IRIME_NB.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.IRIME_UT_NM.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.TAX_KBN.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.INKA_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.INKA_NO_L.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.SEKI_NB_1.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.SEKI_NB_2.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.SEKI_NB_3.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NYUKODAKA.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.SYUKKODAKA.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.HOKAN_1.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.HOKAN_2.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.HOKAN_3.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.HOKAN_RYO.ColNo, number)
                .SetCellStyle(i, sprMeisaiPrtDef.VAR_RATE.ColNo, num1_1)
                .SetCellStyle(i, sprMeisaiPrtDef.VAR_HOKAN_RYO.ColNo, number)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_IN_1.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_IN_2.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_IN_3.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_OUT_2.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_OUT_3.ColNo, num)
                .SetCellStyle(i, sprMeisaiPrtDef.NIYAKU_RYO.ColNo, number)
                .SetCellStyle(i, sprMeisaiPrtDef.NRS_GOODS.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_NM_S_SS.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_CD_L.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_CD_M.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_CD_S.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.CUST_CD_SS.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.CTL_NO.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.IRIME_UT.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.TAX_KB.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.NB_UT.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.SYS_UPD_DATE.ColNo, lbl)
                .SetCellStyle(i, sprMeisaiPrtDef.SYS_UPD_TIME.ColNo, lbl)



                'セルに値を設定
                .SetCellValue(i, sprMeisaiPrtDef.CUST_GOODS_CD.ColNo, dRow.Item("GOODS_CD_CUST").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CUST_GOODS_NM.ColNo, dRow.Item("GOODS_NM_1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SERIAL_NO.ColNo, dRow.Item("SERIAL_NO").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.TANI.ColNo, dRow.Item("NB_UT_NM").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.IRIME_NB.ColNo, dRow.Item("IRIME").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.IRIME_UT_NM.ColNo, dRow.Item("IRIME_UT_NM").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.TAX_KBN.ColNo, dRow.Item("TAX_KB_NM").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.INKA_DATE.ColNo, dRow.Item("INKA_DATE").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.INKA_NO_L.ColNo, dRow.Item("INKA_NO_L").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SEKI_NB_1.ColNo, dRow.Item("SEKI_ARI_NB1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SEKI_NB_2.ColNo, dRow.Item("SEKI_ARI_NB2").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SEKI_NB_3.ColNo, dRow.Item("SEKI_ARI_NB3").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NYUKODAKA.ColNo, dRow.Item("INKO_NB_TTL1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SYUKKODAKA.ColNo, dRow.Item("OUTKO_NB_TTL1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.HOKAN_1.ColNo, dRow.Item("STORAGE1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.HOKAN_2.ColNo, dRow.Item("STORAGE2").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.HOKAN_3.ColNo, dRow.Item("STORAGE3").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.HOKAN_RYO.ColNo, dRow.Item("STRAGE_HENDO_NASHI_AMO_TTL").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.VAR_RATE.ColNo, dRow.Item("VAR_RATE").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.VAR_HOKAN_RYO.ColNo, dRow.Item("STORAGE_AMO_TTL").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_IN_1.ColNo, dRow.Item("HANDLING_IN1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_IN_2.ColNo, dRow.Item("HANDLING_IN2").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_IN_3.ColNo, dRow.Item("HANDLING_IN3").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_OUT_1.ColNo, dRow.Item("HANDLING_OUT1").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_OUT_2.ColNo, dRow.Item("HANDLING_OUT2").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_OUT_3.ColNo, dRow.Item("HANDLING_OUT3").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NIYAKU_RYO.ColNo, dRow.Item("HANDLING_AMO_TTL").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NRS_GOODS.ColNo, dRow.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CUST_NM_S_SS.ColNo, dRow.Item("CUST_NM_S_SS").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CUST_CD_L.ColNo, dRow.Item("CUST_CD_L").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CUST_CD_M.ColNo, dRow.Item("CUST_CD_M").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CUST_CD_S.ColNo, dRow.Item("CUST_CD_S").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CUST_CD_SS.ColNo, dRow.Item("CUST_CD_SS").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.CTL_NO.ColNo, dRow.Item("CTL_NO").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.IRIME_UT.ColNo, dRow.Item("IRIME_UT").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.TAX_KB.ColNo, dRow.Item("TAX_KB").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.NB_UT.ColNo, dRow.Item("NB_UT").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SYS_UPD_DATE.ColNo, dRow.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprMeisaiPrtDef.SYS_UPD_TIME.ColNo, dRow.Item("SYS_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "内部処理"

    ''' <summary>
    ''' 四捨五入処理
    ''' </summary>
    ''' <param name="Amt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToHalfAdjust(ByVal Amt As Decimal) As String

        Dim dCoef As Decimal = Convert.ToDecimal(System.Math.Pow(10, 0))

        Dim i As Decimal = 0

        If Amt > 0 Then
            i = Convert.ToDecimal(System.Math.Floor((Amt * dCoef) + 0.5) / dCoef)
        Else
            i = Convert.ToDecimal(System.Math.Ceiling((Amt * dCoef) - 0.5) / dCoef)
        End If

        Return Convert.ToString(i)

    End Function
#End Region

#End Region

End Class
