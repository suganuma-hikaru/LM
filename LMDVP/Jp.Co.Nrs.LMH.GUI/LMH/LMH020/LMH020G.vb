' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH020G : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMH020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH020F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH020F, ByVal g As LMHControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMHconG = g

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

        'モード判定
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then

            view = True

        Else

            edit = True

        End If

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMHControlC.FUNCTION_EDIT
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMHControlC.FUNCTION_POP
            .F11ButtonName = LMHControlC.FUNCTION_SAVE
            .F12ButtonName = LMHControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = view
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit
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
            .lblSituation.DispMode = mode
            .lblSituation.RecordStatus = status
        End With

    End Sub

#End Region 'Mode&Status

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .tabInka.TabIndex = LMH020C.CtlTabIndex.INKA
            .tabNyuka.TabIndex = LMH020C.CtlTabIndex.NYUKA
            .lblEdiKanriNo.TabIndex = LMH020C.CtlTabIndex.EDIKANRINO
            .cmbStatus.TabIndex = LMH020C.CtlTabIndex.STATUS
            .lblKanriNoL.TabIndex = LMH020C.CtlTabIndex.KANRINOL
            .cmbEigyo.TabIndex = LMH020C.CtlTabIndex.EIGYO
            .cmbWh.TabIndex = LMH020C.CtlTabIndex.WH
            .cmbNyukaType.TabIndex = LMH020C.CtlTabIndex.NYUKATYPE
            .cmbNyukaKbn.TabIndex = LMH020C.CtlTabIndex.NYUKAKBN
            .cmbShinshokuKbn.TabIndex = LMH020C.CtlTabIndex.SHINSHOKUKBN
            .imdNyukaDate.TabIndex = LMH020C.CtlTabIndex.NYUKADATE
            .txtBuyerOrdNo.TabIndex = LMH020C.CtlTabIndex.BUYERORDNO
            .txtOrderNo.TabIndex = LMH020C.CtlTabIndex.ORDERNO
            .numFree.TabIndex = LMH020C.CtlTabIndex.FREE
            .imdHokanDate.TabIndex = LMH020C.CtlTabIndex.HOKANDATE
            .txtCustCdL.TabIndex = LMH020C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMH020C.CtlTabIndex.CUSTCDM
            .lblCustNm.TabIndex = LMH020C.CtlTabIndex.CUSTNM
            .cmbTax.TabIndex = LMH020C.CtlTabIndex.TAX
            .cmbToukiHo.TabIndex = LMH020C.CtlTabIndex.TOUKIHO
            .cmbZenkiHo.TabIndex = LMH020C.CtlTabIndex.ZENKIHO
            .cmbNiyakuUmu.TabIndex = LMH020C.CtlTabIndex.NIYAKUUMU
            .numPlanQt.TabIndex = LMH020C.CtlTabIndex.PLANQT
            .cmbPlanQtUt.TabIndex = LMH020C.CtlTabIndex.PLANQTUT
            .numNyukaCnt.TabIndex = LMH020C.CtlTabIndex.NYUKACNT
            .txtNyubanL.TabIndex = LMH020C.CtlTabIndex.NYUBANL
            .txtNyukaComment.TabIndex = LMH020C.CtlTabIndex.NYUKACOMMENT
            .tabMiddle.TabIndex = LMH020C.CtlTabIndex.MIDDLE
            .tabGoods.TabIndex = LMH020C.CtlTabIndex.GOODS
            .btnRevival.TabIndex = LMH020C.CtlTabIndex.REVIVAL
            .btnDel.TabIndex = LMH020C.CtlTabIndex.DEL
            .sprGoodsDef.TabIndex = LMH020C.CtlTabIndex.GOODSDEF
            .lblKanriNoM.TabIndex = LMH020C.CtlTabIndex.KANRINOM
            .txtGoodsCd.TabIndex = LMH020C.CtlTabIndex.GOODSCD
            .lblGoodsNm.TabIndex = LMH020C.CtlTabIndex.GOODSNM
            .lblGoodsKey.TabIndex = LMH020C.CtlTabIndex.GOODSKEY
            .numKosu.TabIndex = LMH020C.CtlTabIndex.KOSU
            .numHasu.TabIndex = LMH020C.CtlTabIndex.HASU
            .numIrisu.TabIndex = LMH020C.CtlTabIndex.IRISU
            .numStdIrime.TabIndex = LMH020C.CtlTabIndex.STDIRIME
            .cmbOndo.TabIndex = LMH020C.CtlTabIndex.ONDO
            .numSumAnt.TabIndex = LMH020C.CtlTabIndex.SUMANT
            .numSumCnt.TabIndex = LMH020C.CtlTabIndex.SUMCNT
            .numTare.TabIndex = LMH020C.CtlTabIndex.TARE
            .numIrime.TabIndex = LMH020C.CtlTabIndex.IRIME
            .txtOrderNoM.TabIndex = LMH020C.CtlTabIndex.ORDERNOM
            .txtBuyerOrdNoM.TabIndex = LMH020C.CtlTabIndex.BUYERORDNOM
            .txtLot.TabIndex = LMH020C.CtlTabIndex.LOT
            .txtGoodsComment.TabIndex = LMH020C.CtlTabIndex.GOODSCOMMENT
            .txtSerial.TabIndex = LMH020C.CtlTabIndex.SERIAL
            .tabUnso.TabIndex = LMH020C.CtlTabIndex.UNSO
            .pnlUnso.TabIndex = LMH020C.CtlTabIndex.PNL_UNSO
            .cmbUnchinTehai.TabIndex = LMH020C.CtlTabIndex.UNCHINTEHAI
            .cmbTariffKbn.TabIndex = LMH020C.CtlTabIndex.TARIFFKBN
            .cmbSharyoKbn.TabIndex = LMH020C.CtlTabIndex.SHARYOKBN
            .cmbOnkan.TabIndex = LMH020C.CtlTabIndex.ONKAN
            .numUnchin.TabIndex = LMH020C.CtlTabIndex.UNCHIN
            .txtUnsoCd.TabIndex = LMH020C.CtlTabIndex.UNSOCD
            .txtUnsoBrCd.TabIndex = LMH020C.CtlTabIndex.UNSOBRCD
            .lblUnsoNm.TabIndex = LMH020C.CtlTabIndex.UNSONM
            .txtTariffCd.TabIndex = LMH020C.CtlTabIndex.TARIFFCD
            .lblTariffNm.TabIndex = LMH020C.CtlTabIndex.TARIFFNM
            .txtShukkaMotoCd.TabIndex = LMH020C.CtlTabIndex.SHUKKAMOTOCD
            .lblShukkaMotoNm.TabIndex = LMH020C.CtlTabIndex.SHUKKAMOTONM
            .tabFreeL.TabIndex = LMH020C.CtlTabIndex.FREEL
            .sprFreeL.TabIndex = LMH020C.CtlTabIndex.FREEINPUTSL
            .tabFree.TabIndex = LMH020C.CtlTabIndex.TAB_FREE
            .tabFreeM.TabIndex = LMH020C.CtlTabIndex.FREEM
            .sprFreeM.TabIndex = LMH020C.CtlTabIndex.FREEINPUTSM

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '日付コントロールの書式設定
        Call Me.SetDateFormat()

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

        'コンボボックスの生成
        Call Me.CreateComboBox()

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateFormat()

        With Me._Frm

            Me._LMHconG.SetDateFormat(.imdNyukaDate)
            Me._LMHconG.SetDateFormat(.imdHokanDate)

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d3 As Decimal = Convert.ToDecimal(LMHControlC.MAX_3)
            Dim sharp3 As String = "##0"
            Dim d6 As Decimal = Convert.ToDecimal(LMHControlC.MAX_6)
            Dim sharp6 As String = "###,##0"
            Dim d6dec3 As Decimal = Convert.ToDecimal(LMHControlC.MAX_6_3)
            Dim sharp6dec3 As String = "###,##0.000"
            Dim d9dec3 As Decimal = Convert.ToDecimal(LMHControlC.MAX_9_3)
            Dim sharp9dec3 As String = "###,###,##0.000"
            Dim d10 As Decimal = Convert.ToDecimal(LMHControlC.MAX_10)
            Dim sharp10 As String = "#,###,###,##0"

            '▼▼▼(マイナスデータ)
            Dim d9dec3M As Decimal = Convert.ToDecimal(String.Concat("-",LMHControlC.MAX_9_3))
            Dim d10M As Decimal = Convert.ToDecimal(String.Concat("-", LMHControlC.MAX_10))
            '▲▲▲(マイナスデータ)

            .numFree.SetInputFields(sharp3, , 3, 1, , 0, , , d3, 0)
            .numPlanQt.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, 0)
            .numNyukaCnt.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)
            '▼▼▼(マイナスデータ)
            '.numKosu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)
            '.numHasu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)
            .numKosu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, d10M)
            .numHasu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, d10M)
            '▲▲▲(マイナスデータ)
            .numIrisu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)
            .numStdIrime.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, 0)
            .numIrime.SetInputFields(sharp6dec3, , 6, 1, , 3, 3, , d6dec3, 0)
            '▼▼▼(マイナスデータ)
            '.numSumAnt.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, 0)
            '.numSumCnt.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)
            .numSumAnt.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, d9dec3M)
            .numSumCnt.SetInputFields(sharp10, , 10, 1, , 0, , , d10, d10M)
            '▲▲▲(マイナスデータ)
            .numTare.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, 0)
            .numUnchin.SetInputFields(sharp6, , 6, 1, , 0, , , d6, 0)

        End With

    End Sub

    ''' <summary>
    ''' コンボボックスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateComboBox()

        With Me._Frm

            Dim sql As String = String.Concat(" KBN_GROUP_CD = '", LMKbnConst.KBN_U009, "' ")
            Dim sort As String = "KBN_CD"
            Dim cdNm As String() = New String() {"KBN_NM10"}
            Dim itemNm As String() = New String() {"KBN_NM1"}

            Me._LMHconG.CreateComboBox(.cmbToukiHo, LMConst.CacheTBL.KBN, cdNm, itemNm, sql, sort)
            Me._LMHconG.CreateComboBox(.cmbZenkiHo, LMConst.CacheTBL.KBN, cdNm, itemNm, sql, sort)
            Me._LMHconG.CreateComboBox(.cmbNiyakuUmu, LMConst.CacheTBL.KBN, cdNm, itemNm, sql, sort)

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlgM As Boolean = False)

        With Me._Frm

            Dim edit As Boolean = True
            Dim lock As Boolean = True
            Dim clearFlg As Boolean = False

            '編集モード時、活性化
            If DispMode.EDIT.Equals(.lblSituation.DispMode) = True Then
                edit = False
            End If

            '常にロックの項目
            Me._LMHconG.SetLockInputMan(.lblEdiKanriNo, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbStatus, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbWh, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbShinshokuKbn, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblKanriNoL, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbEigyo, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtCustCdL, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtCustCdM, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblCustNm, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblKanriNoM, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblGoodsNm, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblGoodsKey, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.numIrisu, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.numStdIrime, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.numIrime, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.numSumAnt, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.numTare, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbOndo, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblUnsoNm, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblTariffNm, lock, clearFlg)
            Me._LMHconG.SetLockInputMan(.lblShukkaMotoNm, lock, clearFlg)

            '運送情報は常にロックして後で活性化
            Call Me._LMHconG.SetLockInputMan(.cmbUnchinTehai, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.cmbTariffKbn, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.txtUnsoCd, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.txtUnsoBrCd, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.txtShukkaMotoCd, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.cmbSharyoKbn, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.cmbOnkan, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.numUnchin, lock, clearFlg)
            Call Me._LMHconG.SetLockInputMan(.txtTariffCd, lock, clearFlg)

            '編集時に活性化
            Me._LMHconG.SetLockInputMan(.cmbNyukaType, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbNyukaKbn, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.imdNyukaDate, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtBuyerOrdNo, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtOrderNo, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.numFree, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.imdHokanDate, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbTax, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbToukiHo, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbZenkiHo, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbNiyakuUmu, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.numPlanQt, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.cmbPlanQtUt, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.numNyukaCnt, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtNyubanL, edit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtNyukaComment, edit, clearFlg)
            Me._LMHconG.SetLockControl(.btnRevival, edit)
            Me._LMHconG.SetLockControl(.btnDel, edit)

            '中情報のロック制御
            Call Me.SetIrisuLockControl(lockFlgM)

            '運送情報のロック制御
            Call Me.SetTariffKbnLockControl()

        End With

    End Sub

    ''' <summary>
    ''' 中情報のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetIrisuLockControl(Optional ByVal lockFlgM As Boolean = False)

        With Me._Frm

            Dim mEdit As Boolean = True
            Dim mNum1 As Boolean = True '梱数
            Dim mNum2 As Boolean = True '端数
            Dim mNum3 As Boolean = True '個数
            Dim clearFlg As Boolean = Not DispMode.EDIT.Equals(.lblSituation.DispMode)

            If lockFlgM = False Then

                '中情報を選択している 且つ 通常レコード場合にロック解除
                If String.IsNullOrEmpty(.lblKanriNoM.TextValue) = False _
                    AndAlso LMConst.FLG.OFF.Equals(.lblJotai.TextValue) = True _
                    Then

                    mEdit = False

                    If 1 <> Convert.ToDecimal(Me._LMHconG.FormatNumValue(.numIrisu.TextValue)) Then
                        mNum1 = False
                        mNum2 = False
                    Else
                        mNum2 = False
                    End If

                End If
            End If

            Me._LMHconG.SetLockInputMan(.txtGoodsCd, mEdit, clearFlg)
            Me._LMHconG.SetLockInputMan(.numIrime, mEdit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtLot, mEdit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtOrderNoM, mEdit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtBuyerOrdNoM, mEdit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtSerial, mEdit, clearFlg)
            Me._LMHconG.SetLockInputMan(.txtGoodsComment, mEdit, clearFlg)

            '入数によるロック制御
            Me._LMHconG.SetLockInputMan(.numKosu, mNum1, clearFlg)
            Me._LMHconG.SetLockInputMan(.numHasu, mNum2, clearFlg)
            Me._LMHconG.SetLockInputMan(.numSumCnt, mNum3, clearFlg)

        End With

    End Sub

    ''' <summary>
    ''' 運送情報のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTariffKbnLockControl()

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim ptn1 As Boolean = True
            Dim ptn2 As Boolean = True
            Dim ptn3 As Boolean = True
            Dim ptn4 As Boolean = True
            Dim ptn5 As Boolean = True
            Dim lock As Boolean = True
            Dim edit As Boolean = False

            Dim unsoUmu As String = .cmbUnchinTehai.SelectedValue.ToString()
            Dim unchinKbn As String = .cmbTariffKbn.SelectedValue.ToString()

            '日陸手配の場合、活性化
            If LMH020C.TEHAI_NRS.Equals(unsoUmu) = True Then

                ptn1 = False

                '車扱の場合、活性化
                If LMHControlC.TARIFF_KURUMA.Equals(unchinKbn) = True Then

                    ptn2 = False
                    ptn3 = False
                    ptn5 = False

                End If

                '横持ちの場合、活性化
                If LMHControlC.TARIFF_YOKO.Equals(unchinKbn) = True Then

                    ptn2 = False
                    ptn5 = False

                End If

                '混載 または 特便の場合、活性化
                If LMHControlC.TARIFF_KONSAI.Equals(unchinKbn) = True _
                    OrElse LMHControlC.TARIFF_TOKUBIN.Equals(unchinKbn) = True _
                    Then

                    ptn3 = False
                    ptn5 = False

                End If

                '路線(入荷着払い)の場合、活性化
                If LMHControlC.TARIFF_ROSEN.Equals(unchinKbn) = True Then

                    ptn4 = False

                End If

            End If

            'ロック制御
            '編集モード時にロック解除
            Call Me._LMHconG.SetLockInputMan(.cmbUnchinTehai, edit)

            'ロック制御パターン1
            Call Me._LMHconG.SetLockInputMan(.cmbTariffKbn, ptn1)
            Call Me._LMHconG.SetLockInputMan(.txtUnsoCd, ptn1)
            Call Me._LMHconG.SetLockInputMan(.txtUnsoBrCd, ptn1)
            Call Me._LMHconG.SetLockInputMan(.lblUnsoNm, lock, ptn1)
            Call Me._LMHconG.SetLockInputMan(.txtShukkaMotoCd, ptn1)
            Call Me._LMHconG.SetLockInputMan(.lblShukkaMotoNm, lock, ptn1)

            'ロック制御パターン2
            Call Me._LMHconG.SetLockInputMan(.cmbSharyoKbn, ptn2)

            'ロック制御パターン3
            Call Me._LMHconG.SetLockInputMan(.cmbOnkan, ptn3)

            'ロック制御パターン4
            Call Me._LMHconG.SetLockInputMan(.numUnchin, ptn4)

            'ロック制御パターン5
            Call Me._LMHconG.SetLockInputMan(.txtTariffCd, ptn5)
            Call Me._LMHconG.SetLockInputMan(.lblTariffNm, lock, ptn5)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカスの初期化
            .Focus()

            '編集モードの場合
            If DispMode.EDIT.Equals(.lblSituation.DispMode) = True Then
                .cmbNyukaType.Focus()
            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .lblEdiKanriNo.TextValue = String.Empty
            .cmbStatus.SelectedValue = Nothing
            .lblKanriNoL.TextValue = String.Empty
            .cmbEigyo.SelectedValue = Nothing
            .cmbWh.SelectedValue = Nothing
            .cmbShinshokuKbn.SelectedValue = Nothing
            .imdNyukaDate.TextValue = String.Empty
            .txtBuyerOrdNo.TextValue = String.Empty
            .txtOrderNo.TextValue = String.Empty
            .numFree.Value = 0
            .imdHokanDate.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .cmbTax.SelectedValue = Nothing
            .cmbToukiHo.SelectedValue = Nothing
            .cmbZenkiHo.SelectedValue = Nothing
            .cmbNiyakuUmu.SelectedValue = Nothing
            .numPlanQt.Value = 0
            .cmbPlanQtUt.SelectedValue = Nothing
            .numNyukaCnt.Value = 0
            .txtNyubanL.TextValue = String.Empty
            .txtNyukaComment.TextValue = String.Empty

            '中情報のクリア
            Call Me.ClearMControl()

            .cmbUnchinTehai.SelectedValue = Nothing
            .cmbTariffKbn.SelectedValue = Nothing
            .cmbSharyoKbn.SelectedValue = Nothing
            .cmbOnkan.SelectedValue = Nothing
            .numUnchin.Value = 0
            .txtUnsoCd.TextValue = String.Empty
            .txtUnsoBrCd.TextValue = String.Empty
            .lblUnsoNm.TextValue = String.Empty
            .txtTariffCd.TextValue = String.Empty
            .lblTariffNm.TextValue = String.Empty
            .txtShukkaMotoCd.TextValue = String.Empty
            .lblShukkaMotoNm.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 中情報のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearMControl()

        With Me._Frm

            .lblKanriNoM.TextValue = String.Empty
            .txtGoodsCd.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            .lblGoodsKey.TextValue = String.Empty
            .numKosu.Value = 0
            .numHasu.Value = 0
            .numIrisu.Value = 0
            .numStdIrime.Value = 0
            .numIrime.Value = 0
            .numSumAnt.Value = 0
            .numSumCnt.Value = 0
            .numTare.Value = 0
            .cmbOndo.SelectedValue = String.Empty
            .txtLot.TextValue = String.Empty
            .txtOrderNoM.TextValue = String.Empty
            .txtBuyerOrdNoM.TextValue = String.Empty
            .txtSerial.TextValue = String.Empty
            .txtGoodsComment.TextValue = String.Empty

            '単位名のクリア
            .lblTitleKonsuTani.Text = String.Empty
            .lblTitleHasuTani.Text = String.Empty
            .lblStdIrimeTani.Text = String.Empty
            .lblTitleIrimeTani.Text = String.Empty
            .lblSumAntTani.Text = String.Empty
            .lblSumCntTani.Text = String.Empty

            '隠し項目のクリア
            .lblJotai.TextValue = String.Empty

            'フリー(M)スプレッドをクリア
            .sprFreeM.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ds As DataSet)

        'ヘッダ項目の値設定
        Call Me.SetHeaderData(ds)

        '明細の値設定
        Call Me.SetInkaMData(ds)
        Call Me.SetFreeLData(ds)

    End Sub

    ''' <summary>
    ''' ヘッダ項目の値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMH020C.TABLE_NM_L).Rows(0)

        With Me._Frm

            .lblEdiKanriNo.TextValue = dr.Item("EDI_CTL_NO").ToString()
            .cmbStatus.SelectedValue = dr.Item("EDI_STATE_KB").ToString()
            .lblKanriNoL.TextValue = dr.Item("INKA_CTL_NO_L").ToString()
            .cmbEigyo.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .cmbWh.SelectedValue = dr.Item("NRS_WH_CD").ToString()
            .cmbShinshokuKbn.SelectedValue = dr.Item("INKA_STATE_KB").ToString()
            .imdNyukaDate.TextValue = dr.Item("INKA_DATE").ToString()
            .cmbNyukaType.SelectedValue = dr.Item("INKA_TP").ToString()
            .cmbNyukaKbn.SelectedValue = dr.Item("INKA_KB").ToString()
            .txtBuyerOrdNo.TextValue = dr.Item("BUYER_ORD_NO").ToString()
            .txtOrderNo.TextValue = dr.Item("OUTKA_FROM_ORD_NO").ToString()
            .numFree.Value = Me._LMHconG.FormatNumValue(dr.Item("HOKAN_FREE_KIKAN").ToString())
            .imdHokanDate.TextValue = dr.Item("HOKAN_STR_DATE").ToString()
            .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
            .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
            .lblCustNm.TextValue = Me._LMHconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMHControlC.ZENKAKU_SPACE)
            .cmbTax.SelectedValue = dr.Item("TAX_KB").ToString()
            .cmbToukiHo.SelectedValue = dr.Item("TOUKI_HOKAN_YN").ToString()
            .cmbZenkiHo.SelectedValue = dr.Item("HOKAN_YN").ToString()
            .cmbNiyakuUmu.SelectedValue = dr.Item("NIYAKU_YN").ToString()
            .numPlanQt.Value = Me._LMHconG.FormatNumValue(dr.Item("INKA_PLAN_QT").ToString())
            .cmbPlanQtUt.SelectedValue = dr.Item("INKA_PLAN_QT_UT").ToString()
            .numNyukaCnt.Value = Me._LMHconG.FormatNumValue(dr.Item("INKA_TTL_NB").ToString())
            .txtNyubanL.TextValue = dr.Item("NYUBAN_L").ToString()
            .txtNyukaComment.TextValue = dr.Item("REMARK").ToString()
            .cmbUnchinTehai.SelectedValue = dr.Item("UNCHIN_TP").ToString()
            .cmbTariffKbn.SelectedValue = dr.Item("UNCHIN_KB").ToString()
            .cmbSharyoKbn.SelectedValue = dr.Item("SYARYO_KB").ToString()
            .cmbOnkan.SelectedValue = dr.Item("UNSO_ONDO_KB").ToString()
            .numUnchin.Value = Me._LMHconG.FormatNumValue(dr.Item("UNCHIN").ToString())
            .txtUnsoCd.TextValue = dr.Item("UNSO_CD").ToString()
            .txtUnsoBrCd.TextValue = dr.Item("UNSO_BR_CD").ToString()
            .lblUnsoNm.TextValue = Me._LMHconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), LMHControlC.ZENKAKU_SPACE)
            .txtTariffCd.TextValue = dr.Item("YOKO_TARIFF_CD").ToString()
            .lblTariffNm.TextValue = dr.Item("TARIFF_REM").ToString()
            .txtShukkaMotoCd.TextValue = dr.Item("OUTKA_MOTO").ToString()
            .lblShukkaMotoNm.TextValue = dr.Item("OUTKA_MOTO_NM").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 中の詳細情報を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rowNo">スプレッドの行番号　-1の場合、画面の中番号を使用</param>
    ''' <remarks></remarks>
    Friend Sub SetInkaMHeaderData(ByVal ds As DataSet, ByVal rowNo As Integer)

        With Me._Frm

            Dim setDr As DataRow = Nothing
            Dim oldIrisu As String = String.Empty

            If -1 < rowNo Then
                setDr = ds.Tables(LMH020C.TABLE_NM_M).Rows(Convert.ToInt32(Me._LMHconG.GetCellValue(.sprGoodsDef.ActiveSheet.Cells(rowNo, LMH020G.sprGoodsDef.RECNO.ColNo))))
            Else
                setDr = Me.GetInkaMDataRow(ds)
            End If

            '元の入数を格納
            oldIrisu = .numIrisu.TextValue

            '値の設定
            .lblKanriNoM.TextValue = setDr.Item("EDI_CTL_NO_CHU").ToString()
            .txtGoodsCd.TextValue = setDr.Item("CUST_GOODS_CD").ToString()
            .lblGoodsNm.TextValue = setDr.Item("GOODS_NM").ToString()
            .lblGoodsKey.TextValue = setDr.Item("NRS_GOODS_CD").ToString()

            '入数が0の場合は梱数に0を設定、入数が1で端数が0の場合は端数に梱数を設定し梱数を0に設定。
            '入数が1で端数が0で無い場合は梱数に0を設定。
            If Convert.ToInt32(Me._LMHconG.FormatNumValue(setDr.Item("PKG_NB").ToString())) = 0 Then
                .numHasu.Value = Me._LMHconG.FormatNumValue(setDr.Item("HASU").ToString())
                .numKosu.Value = 0
            ElseIf Convert.ToInt32(Me._LMHconG.FormatNumValue(setDr.Item("PKG_NB").ToString())) = 1 Then

                If Me._LMHconG.FormatNumValue(setDr.Item("HASU").ToString()) = "0" Then
                    .numHasu.Value = Me._LMHconG.FormatNumValue(setDr.Item("INKA_PKG_NB").ToString())
                    .numKosu.Value = 0
                Else
                    .numHasu.Value = Me._LMHconG.FormatNumValue(setDr.Item("HASU").ToString())
                    .numKosu.Value = 0
                End If
            Else
                .numHasu.Value = Me._LMHconG.FormatNumValue(setDr.Item("HASU").ToString())
                .numKosu.Value = Me._LMHconG.FormatNumValue(setDr.Item("INKA_PKG_NB").ToString())
            End If

            '.numHasu.Value = Me._LMHconG.FormatNumValue(setDr.Item("HASU").ToString())
            .numIrisu.Value = Me._LMHconG.FormatNumValue(setDr.Item("PKG_NB").ToString())
            .numStdIrime.Value = Me._LMHconG.FormatNumValue(setDr.Item("STD_IRIME").ToString())
            .numIrime.Value = Me._LMHconG.FormatNumValue(setDr.Item("IRIME").ToString())
            .numSumAnt.Value = Me.SetSuryoData(setDr.Item("SURYO").ToString())
            .numSumCnt.Value = Me._LMHconG.FormatNumValue(setDr.Item("NB").ToString())
            .numTare.Value = setDr.Item("BETU_WT").ToString()
            .cmbOndo.SelectedValue = setDr.Item("ONDO_KB").ToString()
            .txtLot.TextValue = setDr.Item("LOT_NO").ToString()
            .txtOrderNoM.TextValue = setDr.Item("OUTKA_FROM_ORD_NO").ToString()
            .txtBuyerOrdNoM.TextValue = setDr.Item("BUYER_ORD_NO").ToString()
            .txtSerial.TextValue = setDr.Item("SERIAL_NO").ToString()
            .txtGoodsComment.TextValue = setDr.Item("REMARK").ToString()
            .lblJotai.TextValue = setDr.Item("JYOTAI").ToString()

            .lblTitleKonsuTani.Text = setDr.Item("PKG_UT").ToString()
            .lblTitleHasuTani.Text = setDr.Item("NB_UT").ToString()
            .lblStdIrimeTani.Text = setDr.Item("STD_IRIME_UT").ToString()
            .lblTitleIrimeTani.Text = setDr.Item("IRIME_UT").ToString()
            .lblSumAntTani.Text = setDr.Item("IRIME_UT").ToString()
            .lblSumCntTani.Text = setDr.Item("NB_UT").ToString()


            'フリー(M)スプレッドに値設定
            Call Me.SetFreeMData(ds, setDr)

        End With

    End Sub

    ''' <summary>
    ''' 数量の最大値を判定して設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>値</returns>
    ''' <remarks></remarks>
    Private Function SetSuryoData(ByVal value As String) As String

        SetSuryoData = Me._LMHconG.FormatNumValue(value)

        If Convert.ToDecimal(LMHControlC.MAX_9_3) < Convert.ToDecimal(SetSuryoData) Then
            SetSuryoData = LMHControlC.MAX_9_3
        End If

        Return SetSuryoData

    End Function

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGoodsDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.DEF, " ", 20, True)
        Public Shared JYOTAI_NM As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.JYOTAI_NM, "状態", 40, True)
        Public Shared EDI_CTL_NO_CHU As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.EDI_CTL_NO_CHU, "EDI管理番号" & vbCrLf & "(中)", 100, True)
        Public Shared CUST_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.CUST_GOODS_CD, "商品コード", 120, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.GOODS_NM, "商品名", 220, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.NB, "個数", 120, True)
        Public Shared SURYO As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.SURYO, "数量", 120, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.REMARK, "商品別注意事項", 240, True)
        Public Shared OUTKA_FROM_ORD_NO As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.OUTKA_FROM_ORD_NO, "オーダー番号", 180, True)
        Public Shared RECNO As SpreadColProperty = New SpreadColProperty(LMH020C.SprInkaMColumnIndex.RECNO, "", 0, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(フリー項目)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprFreeDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH020C.SprFREEColumnIndex.DEF, " ", 20, True)
        Public Shared TITLE As SpreadColProperty = New SpreadColProperty(LMH020C.SprFREEColumnIndex.TITLE, "タイトル", 360, True)
        Public Shared FREE As SpreadColProperty = New SpreadColProperty(LMH020C.SprFREEColumnIndex.FREE, "入力", 780, True)
        Public Shared COLNM As SpreadColProperty = New SpreadColProperty(LMH020C.SprFREEColumnIndex.COLNM, "", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            '入荷(中)スプレッドの設定
            Call Me.InitSpread(.sprGoodsDef, New LMH020G.sprGoodsDef(), 10)

            'フリー(大)スプレッドの設定
            Call Me.InitSpread(.sprFreeL, New LMH020G.sprFreeDef(), 4)

            'フリー(中)スプレッドの設定
            Call Me.InitSpread(.sprFreeM, New LMH020G.sprFreeDef(), 4)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="columnClass">スプレッド列定義体</param>
    ''' <param name="columnCnt">列数</param>
    ''' <remarks></remarks>
    Private Sub InitSpread(ByVal spr As LMSpread, ByVal columnClass As Object, ByVal columnCnt As Integer)

        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = columnCnt

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(columnClass)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInkaMData(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprGoodsDef

        'SPREAD(表示行)初期化
        spr.CrearSpread()

        'セルに設定するスタイルの取得
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr, True)
        Dim sNum12dec3 As StyleInfo = Me.StyleInfoNum12dec3(spr, True)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)

        Dim rowCnt As Integer = 0
        Dim setDt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
        Dim max As Integer = setDt.Rows.Count - 1

        Dim dr As DataRow = Nothing     'ADD 2017/05/11

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To max

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.JYOTAI_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.EDI_CTL_NO_CHU.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.CUST_GOODS_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.NB.ColNo, sNum10)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.SURYO.ColNo, sNum12dec3)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.REMARK.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.OUTKA_FROM_ORD_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprGoodsDef.RECNO.ColNo, sLabel)

                '値設定
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.JYOTAI_NM.ColNo, setDt.Rows(i).Item("JYOTAI_NM").ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.EDI_CTL_NO_CHU.ColNo, setDt.Rows(i).Item("EDI_CTL_NO_CHU").ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.CUST_GOODS_CD.ColNo, setDt.Rows(i).Item("CUST_GOODS_CD").ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.GOODS_NM.ColNo, setDt.Rows(i).Item("GOODS_NM").ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.NB.ColNo, Me._LMHconG.FormatNumValue(setDt.Rows(i).Item("NB").ToString()))
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.SURYO.ColNo, Me.SetSuryoData(setDt.Rows(i).Item("SURYO").ToString()))
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.REMARK.ColNo, setDt.Rows(i).Item("REMARK").ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.OUTKA_FROM_ORD_NO.ColNo, setDt.Rows(i).Item("OUTKA_FROM_ORD_NO").ToString())
                .SetCellValue(rowCnt, LMH020G.sprGoodsDef.RECNO.ColNo, Me.SetZeroData(i.ToString(), LMH020C.MAEZERO))

                '色設定 ADD 2017/05/09 アクサルタ対応
                dr = setDt.Rows(i)

                If SetColor(dr) = True Then
                    .ActiveSheet.Rows(rowCnt).ForeColor = Color.Blue
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 色設定 アクサルタ　ADD 2017/05/11
    ''' </summary>
    ''' <param name="dr">値</param>
    ''' <returns>設定値</returns>
    ''' <remarks></remarks>
    Friend Function SetColor(ByVal dr As DataRow) As Boolean

        If String.Empty.Equals(dr.Item("INKA_CTL_NO_L").ToString.Trim) = True _
            And "1".ToString.Equals(dr.Item("NB").ToString.Trim) = True _
            And "KOSU0".ToString.Equals(dr.Item("FREE_C30").ToString.Trim) = True _
            And "0".ToString.Equals(dr.Item("SYS_DEL_FLG").ToString.Trim) = True Then

            Dim dSURYO As Decimal = Convert.ToDecimal(dr.Item("SURYO").ToString)
            Dim dIREME As Decimal = Convert.ToDecimal(dr.Item("STD_IRIME").ToString)
            Dim dNB As Decimal = Convert.ToDecimal(dr.Item("NB").ToString)

            If dIREME <> 0 Then
                Dim fixSURO As Decimal = Fix(Convert.ToDecimal(dSURYO / dIREME))
                Dim wkSURO As Decimal = Convert.ToDecimal(dSURYO / dIREME)

                If fixSURO <> wkSURO Then
                    Return True

                End If

            End If
        End If

        Return False

    End Function

    ''' <summary>
    ''' 前ゼロ設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="keta">前につけるゼロ</param>
    ''' <returns>設定値</returns>
    ''' <remarks></remarks>
    Friend Function SetZeroData(ByVal value As String, ByVal keta As String) As String

        SetZeroData = String.Concat(keta, value)

        Dim ketasu As Integer = keta.Length

        Return SetZeroData.Substring(SetZeroData.Length - ketasu, ketasu)

    End Function

    ''' <summary>
    ''' スプレッドにデータを設定(フリーL)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetFreeLData(ByVal ds As DataSet)

        '値のクリア
        Call Me.ClearMControl()

        'フリースプレッド(L)に値設定
        Call Me.SetFreeData(ds, Me._Frm.sprFreeL, ds.Tables(LMH020C.TABLE_NM_L).Rows(0), LMH020C.DATA_KB_L, True)

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(フリーM)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Friend Sub SetFreeMData(ByVal ds As DataSet, ByVal dr As DataRow)

        'フリースプレッド(M)に値設定
        Call Me.SetFreeData(ds, Me._Frm.sprFreeM, dr, LMH020C.DATA_KB_M, LMConst.FLG.OFF.Equals(Me._Frm.lblJotai.TextValue))

    End Sub

    'START YANAI EDIメモNo.43
    ''' <summary>
    ''' スプレッドにデータを設定(フリーL)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetFreeLData2(ByVal ds As DataSet)

        'フリースプレッド(L)に値設定
        Call Me.SetFreeData(ds, Me._Frm.sprFreeL, ds.Tables(LMH020C.TABLE_NM_L).Rows(0), LMH020C.DATA_KB_L, True)

    End Sub
    'END YANAI EDIメモNo.43

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="setDr">画面に設定するDataRow</param>
    ''' <param name="dataKb">データ区分</param>
    ''' <param name="editFlg">編集可能フラグ</param>
    ''' <remarks></remarks>
    Private Sub SetFreeData(ByVal ds As DataSet _
                            , ByVal spr As LMSpread _
                            , ByVal setDr As DataRow _
                            , ByVal dataKb As String _
                            , ByVal editFlg As Boolean _
                            )

        'SPREAD(表示行)初期化
        spr.CrearSpread()

        'セルに設定するスタイルの取得
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)

        Dim rowCnt As Integer = 0
        Dim colDrs As DataRow() = Me.GetFreeDrs(ds, dataKb)
        Dim max As Integer = colDrs.Length - 1
        editFlg = editFlg AndAlso DispMode.EDIT.Equals(Me._Frm.lblSituation.DispMode)

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            Dim colNm As String = String.Empty

            For i As Integer = 0 To max

                '非表示項目の場合、スルー
                If LMHControlC.FLG_OFF.Equals(colDrs(i).Item("ROW_VISIBLE_FLAG").ToString()) = True Then
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMH020G.sprFreeDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMH020G.sprFreeDef.TITLE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMH020G.sprFreeDef.FREE.ColNo, Me.StyleInfoFreeCol(spr, colDrs(i), editFlg))
                .SetCellStyle(rowCnt, LMH020G.sprFreeDef.COLNM.ColNo, sLabel)

                '値設定
                .SetCellValue(rowCnt, LMH020G.sprFreeDef.DEF.ColNo, False.ToString())
                colNm = colDrs(i).Item("DB_COL_NM").ToString()
                .SetCellValue(rowCnt, LMH020G.sprFreeDef.TITLE.ColNo, Me._LMHconG.EditConcatData(colNm, colDrs(i).Item("FIELD_NM").ToString(), Space(1)))
                .SetCellValue(rowCnt, LMH020G.sprFreeDef.FREE.ColNo, setDr.Item(colNm).ToString())
                .SetCellValue(rowCnt, LMH020G.sprFreeDef.COLNM.ColNo, colNm)

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' フリーテーブルの情報を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dataKb">データ区分</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function GetFreeDrs(ByVal ds As DataSet, ByVal dataKb As String) As DataRow()
        Return ds.Tables(LMH020C.TABLE_NM_FREE).Select(String.Concat(" DATA_KB = '", dataKb, "' "), "SORT_NO")
    End Function

#End Region 'Spread

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
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        '▼▼▼(マイナスデータ)
        'Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, lock, 0, , ",")
        Return LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, lock, 0, , ",")
        '▲▲▲(マイナスデータ)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        '▼▼▼(マイナスデータ)
        'Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, lock, 3, , ",")
        Return LMSpreadUtility.GetNumberCell(spr, -999999999999.999, 999999999999.999, lock, 3, , ",")
        '▲▲▲(マイナスデータ)

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

    ''' <summary>
    ''' フリー項目のスタイルを設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="editFlg">編集フラグ</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoFreeCol(ByVal spr As LMSpread, ByVal dr As DataRow, ByVal editFlg As Boolean) As StyleInfo

        '数値型の場合
        If LMH020C.INPUT_NUMBER.Equals(dr.Item("INPUT_MANAGE_KB").ToString()) = True Then

            '▼▼▼(マイナスデータ)
            'Return LMSpreadUtility.GetNumberCell(spr, 0, Convert.ToDouble(Me.GetNumberByte(dr)), Me.GetInputEditFlg(dr, editFlg), Convert.ToInt32(Me._LMHconG.FormatNumValue(dr.Item("NUM_DIGITS_DEC").ToString())), , ",")
            Return LMSpreadUtility.GetNumberCell(spr, Convert.ToDouble(String.Concat("-", Me.GetNumberByte(dr))), Convert.ToDouble(Me.GetNumberByte(dr)), Me.GetInputEditFlg(dr, editFlg), Convert.ToInt32(Me._LMHconG.FormatNumValue(dr.Item("NUM_DIGITS_DEC").ToString())), , ",")
            '▲▲▲(マイナスデータ)

        End If

        Return LMSpreadUtility.GetTextCell(spr, Me.GetInputManageKbn(dr), Convert.ToInt32(Me._LMHconG.FormatNumValue(dr.Item("NUM_DIGITS_INT").ToString())), Me.GetInputEditFlg(dr, editFlg))

    End Function

    ''' <summary>
    ''' 入力制御の設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>InputControl</returns>
    ''' <remarks></remarks>
    Private Function GetInputManageKbn(ByVal dr As DataRow) As InputControl

        GetInputManageKbn = Nothing

        Select Case dr.Item("INPUT_MANAGE_KB").ToString()

            Case LMH020C.INPUT_ALL_HANKAKU

                GetInputManageKbn = InputControl.ALL_HANKAKU

            Case LMH020C.INPUT_ALL_MIX

                GetInputManageKbn = InputControl.ALL_MIX

            Case LMH020C.INPUT_ALL_MIX_IME_OFF

                GetInputManageKbn = InputControl.ALL_MIX_IME_OFF

            Case LMH020C.INPUT_ALL_ZENKAKU

                GetInputManageKbn = InputControl.ALL_ZENKAKU

            Case LMH020C.INPUT_HAN_ALPHA

                GetInputManageKbn = InputControl.HAN_ALPHA

            Case LMH020C.INPUT_HAN_ALPHA_L

                GetInputManageKbn = InputControl.HAN_ALPHA_L

            Case LMH020C.INPUT_HAN_ALPHA_U

                GetInputManageKbn = InputControl.HAN_ALPHA_U

            Case LMH020C.INPUT_HAN_KANA

                GetInputManageKbn = InputControl.HAN_KANA

            Case LMH020C.INPUT_HAN_NUM_ALPHA

                GetInputManageKbn = InputControl.HAN_NUM_ALPHA

            Case LMH020C.INPUT_HAN_NUM_ALPHA_L

                GetInputManageKbn = InputControl.HAN_NUM_ALPHA_L

            Case LMH020C.INPUT_HAN_NUM_ALPHA_U

                GetInputManageKbn = InputControl.HAN_NUM_ALPHA_U

        End Select

        Return GetInputManageKbn

    End Function

    ''' <summary>
    ''' ナンバー型の桁数を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>桁数</returns>
    ''' <remarks></remarks>
    Private Function GetNumberByte(ByVal dr As DataRow) As String

        Dim dec As Double = Convert.ToDouble(Me._LMHconG.FormatNumValue(dr.Item("NUM_DIGITS_DEC").ToString()))

        'X整数部
        'Y小数部
        '10の(X)乗 - 1
        GetNumberByte = Me.PowData(Convert.ToDouble(Me._LMHconG.FormatNumValue(dr.Item("NUM_DIGITS_INT").ToString()))).ToString()

        '0以外の場合、小数部の設定
        If 0 <> dec Then

            '10の(Y)乗 - 1
            dec = Me.PowData(dec)

            GetNumberByte = String.Concat(GetNumberByte, ".", dec.ToString())

        End If

        Return GetNumberByte

    End Function

    ''' <summary>
    ''' 指数計算
    ''' </summary>
    ''' <param name="value">指数</param>
    ''' <returns>計算値</returns>
    ''' <remarks></remarks>
    Private Function PowData(ByVal value As Double) As Double

        Return System.Math.Pow(10, value) - 1

    End Function

    ''' <summary>
    ''' 編集可否の判定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="editFlg">編集フラグ</param>
    ''' <returns>True：編集不可　False：編集可能</returns>
    ''' <remarks></remarks>
    Private Function GetInputEditFlg(ByVal dr As DataRow, ByVal editFlg As Boolean) As Boolean

        '編集モード以外、ロック
        If editFlg = False Then
            Return True
        End If

        '編集不可の場合、True
        If LMHControlC.FLG_OFF.Equals(dr.Item("EDIT_ABLE_FLAG").ToString()) = True Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 中番号を元データを特定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Friend Function GetInkaMDataRow(ByVal ds As DataSet) As DataRow

        Return ds.Tables(LMH020C.TABLE_NM_M).Select(String.Concat(" EDI_CTL_NO_CHU = '", Me._Frm.lblKanriNoM.TextValue, "' "))(0)

    End Function

#End Region

#End Region

End Class
