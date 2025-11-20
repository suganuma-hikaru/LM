' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF020G : 運送入力
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMF020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF020F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF020F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMFconG = g

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
            .F1ButtonName = LMFControlC.FUNCTION_UNSONEW    'ADD 2018/06/25
            .F2ButtonName = LMFControlC.FUNCTION_HENSHU
            .F3ButtonName = LMFControlC.FUNCTION_FUKUSHA
            .F4ButtonName = LMFControlC.FUNCTION_SAKUJO
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = LMFControlC.FUNCTION_DESTSAVE
            .F9ButtonName = String.Empty
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = LMFControlC.FUNCTION_HOZON
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = view    'UPD 2018/06/25 lock → view
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = always
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbPrint.TabIndex = LMF020C.CtlTabIndex.PRINT
            .numPrtCnt.TabIndex = LMF020C.CtlTabIndex.PRTCNT
            .numPrtCnt_From.TabIndex = LMF020C.CtlTabIndex.PRTCNT_FROM
            .numPrtCnt_To.TabIndex = LMF020C.CtlTabIndex.PRTCNT_TO
            .btnPrint.TabIndex = LMF020C.CtlTabIndex.BTNPRINT
            .pnlUnso.TabIndex = LMF020C.CtlTabIndex.UNSO
            .cmbEigyo.TabIndex = LMF020C.CtlTabIndex.EIGYO
            .cmbYosoEigyo.TabIndex = LMF020C.CtlTabIndex.YUSOEIGYO
            .lblUnsoNo.TabIndex = LMF020C.CtlTabIndex.UNSONO
            .cmbMotoDataKbn.TabIndex = LMF020C.CtlTabIndex.MOTODATAKBN
            .cmbUnsoJiyuKbn.TabIndex = LMF020C.CtlTabIndex.UNSOJIYUKBN
            .cmbPcKbn.TabIndex = LMF020C.CtlTabIndex.PCKBN
            .cmbTax.TabIndex = LMF020C.CtlTabIndex.TAXKB
            .lblUnkoNo.TabIndex = LMF020C.CtlTabIndex.UNKONO
            .cmbTehaiKbn.TabIndex = LMF020C.CtlTabIndex.TEHAIKBN
            .cmbBinKbn.TabIndex = LMF020C.CtlTabIndex.BINKBN
            .cmbTariffKbn.TabIndex = LMF020C.CtlTabIndex.TARIFFKBN
            .cmbSharyoKbn.TabIndex = LMF020C.CtlTabIndex.SHARYOKBN
            .lblKanriNo.TabIndex = LMF020C.CtlTabIndex.KANRINO
            .txtUnsocoCd.TabIndex = LMF020C.CtlTabIndex.UNSOCOCD
            .txtUnsocoBrCd.TabIndex = LMF020C.CtlTabIndex.UNSOCOBRCD
            .lblUnsocoNm.TabIndex = LMF020C.CtlTabIndex.UNSOCONM
            .txtOkuriNo.TabIndex = LMF020C.CtlTabIndex.OKURINO
            .txtCustCdL.TabIndex = LMF020C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMF020C.CtlTabIndex.CUSTCDM
            .lblCustNm.TabIndex = LMF020C.CtlTabIndex.CUSTNM
            .txtOrdNo.TabIndex = LMF020C.CtlTabIndex.ORDNO
            .txtShipCd.TabIndex = LMF020C.CtlTabIndex.SHIPCD
            .lblShipNm.TabIndex = LMF020C.CtlTabIndex.SHIPNM
            .txtBuyerOrdNo.TabIndex = LMF020C.CtlTabIndex.BUYERORDNO
            .txtTariffCd.TabIndex = LMF020C.CtlTabIndex.TARIFFCD
            .lblTariffRem.TabIndex = LMF020C.CtlTabIndex.TARIFFREM
            .txtExtcTariffCd.TabIndex = LMF020C.CtlTabIndex.EXTCTARIFFCD
            .lblExtcTariffRem.TabIndex = LMF020C.CtlTabIndex.EXTCTARIFFREM
            .txtPayTariffCd.TabIndex = LMF020C.CtlTabIndex.PAYTARIFFCD              'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .lblPayTariffRem.TabIndex = LMF020C.CtlTabIndex.PAYTARIFFREM            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayExtcTariffCd.TabIndex = LMF020C.CtlTabIndex.PAYEXTCTARIFFCD      'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .lblPayExtcTariffRem.TabIndex = LMF020C.CtlTabIndex.PAYEXTCTARIFFREM    'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .pnlDestOrigin.TabIndex = LMF020C.CtlTabIndex.ORIGDEST
            .imdOrigDate.TabIndex = LMF020C.CtlTabIndex.ORIGDATE
            .txtOrigTime.TabIndex = LMF020C.CtlTabIndex.ORIGTIME
            .txtOrigCd.TabIndex = LMF020C.CtlTabIndex.ORIGCD
            .lblOrigNm.TabIndex = LMF020C.CtlTabIndex.ORIGNM
            .lblOrigJisCd.TabIndex = LMF020C.CtlTabIndex.ORIGJISCD
            .imdDestDate.TabIndex = LMF020C.CtlTabIndex.DESTDATE
            .txtDestTime.TabIndex = LMF020C.CtlTabIndex.DESTTIME
            .txtJiDestTime.TabIndex = LMF020C.CtlTabIndex.DESTJITIME
            '要望番号:2408 2015.09.17 追加START
            .cmbAutoDenpKbn.TabIndex = LMF020C.CtlTabIndex.AUTO_DENP_KBN
            .lblAutoDenpNo.TabIndex = LMF020C.CtlTabIndex.AUTO_DENP_NO
            '要望番号:2408 2015.09.17 追加END
            .txtDestCd.TabIndex = LMF020C.CtlTabIndex.DESTCD
            .lblDestNm.TabIndex = LMF020C.CtlTabIndex.DESTNM
            .lblDestJisCd.TabIndex = LMF020C.CtlTabIndex.DESTJISCD
            .lblZipNo.TabIndex = LMF020C.CtlTabIndex.ZIPNO
            .lblDestAdd1.TabIndex = LMF020C.CtlTabIndex.DESTADD1
            .lblDestAdd2.TabIndex = LMF020C.CtlTabIndex.DESTADD2
            .txtDestAdd3.TabIndex = LMF020C.CtlTabIndex.DESTADD3
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            .txtTel.TabIndex = LMF020C.CtlTabIndex.TEL_NO
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
            .txtAreaCd.TabIndex = LMF020C.CtlTabIndex.AREACD
            .lblAreaNm.TabIndex = LMF020C.CtlTabIndex.AREANM
            .numUnsoPkgCnt.TabIndex = LMF020C.CtlTabIndex.UNSOPKGCNT
            .numUnsoWtL.TabIndex = LMF020C.CtlTabIndex.UNSOWT_L
            .cmbUnsoCntUt.TabIndex = LMF020C.CtlTabIndex.UNSOCNT_UT
            .cmbThermoKbn.TabIndex = LMF020C.CtlTabIndex.THERMOKBN
            .btnKeisan.TabIndex = LMF020C.CtlTabIndex.KEISAN
            .txtUnsoComment.TabIndex = LMF020C.CtlTabIndex.UNSOCOMMENT
            .pnlCargo.TabIndex = LMF020C.CtlTabIndex.CARGO
            .btnAdd.TabIndex = LMF020C.CtlTabIndex.ADD
            .btnDel.TabIndex = LMF020C.CtlTabIndex.DEL
            .sprDetail.TabIndex = LMF020C.CtlTabIndex.DETAIL
            .numUnsoWt.TabIndex = LMF020C.CtlTabIndex.UNSOWT
            .numSeiqTariffDes.TabIndex = LMF020C.CtlTabIndex.SEIQTARIFFDES
            .numSeiqUnchin.TabIndex = LMF020C.CtlTabIndex.SEIQUNCHIN
            .numPayUnchin.TabIndex = LMF020C.CtlTabIndex.PAYUNCHIN
            .numCityExtc.TabIndex = LMF020C.CtlTabIndex.CITYEXTC
            .numWintExtc.TabIndex = LMF020C.CtlTabIndex.WINTEXTC
            .numRelyExtc.TabIndex = LMF020C.CtlTabIndex.RELYEXTC
            .numPassExtc.TabIndex = LMF020C.CtlTabIndex.PASSEXTC
            .numInsurExtc.TabIndex = LMF020C.CtlTabIndex.INSUREXTC
            .numPayUnsoWt.TabIndex = LMF020C.CtlTabIndex.PAYUNSOWT                  'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPaySeiqTariffDes.TabIndex = LMF020C.CtlTabIndex.PAYSEIQTARIFFDES    'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayPayUnchin.TabIndex = LMF020C.CtlTabIndex.PAYPAYUNCHIN            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayCityExtc.TabIndex = LMF020C.CtlTabIndex.PAYCITYEXTC              'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayWintExtc.TabIndex = LMF020C.CtlTabIndex.PAYWINTEXTC              'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayRelyExtc.TabIndex = LMF020C.CtlTabIndex.PAYRELYEXTC              'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayPassExtc.TabIndex = LMF020C.CtlTabIndex.PAYPASSEXTC              'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayInsurExtc.TabIndex = LMF020C.CtlTabIndex.PAYINSUREXTC            'ADD UMANO 要望番号1302 支払運賃に伴う修正。
            .txtRemark.TabIndex = LMF020C.CtlTabIndex.REMARK

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="clearFlg">クリアフラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal clearFlg As Boolean = True)

        Dim flg As Boolean() = Me.LockControlFlg()
        Dim view As Boolean = flg(LMF020C.Lock.REPT)
        Dim edit As Boolean = flg(LMF020C.Lock.EDIT)
        Dim unso As Boolean = flg(LMF020C.Lock.UNSO)
        Dim lock As Boolean = flg(LMF020C.Lock.LOCK)
        Dim trip As Boolean = flg(LMF020C.Lock.TRIP)
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
        Dim cust As Boolean = flg(LMF020C.Lock.CUST)
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

        With Me._Frm

            '常にロック
            Call Me._LMFconG.SetLockInputMan(.cmbEigyo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblUnsoNo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblKanriNo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbMotoDataKbn, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblUnkoNo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbTariffKbn, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblUnsocoNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtCustCdL, lock, clearFlg)
            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
            'Call Me._LMFconG.SetLockInputMan(.txtCustCdM, lock, clearFlg)
            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End
            Call Me._LMFconG.SetLockInputMan(.lblCustNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblShipNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblTariffRem, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblExtcTariffRem, lock, clearFlg)
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            Call Me._LMFconG.SetLockInputMan(.lblPayTariffRem, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblPayExtcTariffRem, lock, clearFlg)
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            Call Me._LMFconG.SetLockInputMan(.lblOrigNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblOrigJisCd, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblDestNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblDestJisCd, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblZipNo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblDestAdd1, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblDestAdd2, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblAreaNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numUnsoWt, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numSeiqTariffDes, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numSeiqUnchin, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayUnchin, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numCityExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numWintExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numRelyExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPassExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numInsurExtc, lock, clearFlg)
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            Call Me._LMFconG.SetLockInputMan(.numPayUnsoWt, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPaySeiqTariffDes, lock, clearFlg)
            'Call Me._LMFconG.SetLockInputMan(.numPayPayUnchin, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayCityExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayWintExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayRelyExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayPassExtc, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayInsurExtc, lock, clearFlg)
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            '参照 且つ 元データ = 運送の場合、活性化
            Call Me._LMFconG.SetLockInputMan(.cmbPrint, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPrtCnt, view, clearFlg)
            Call Me._LMFconG.SetLockControl(.btnPrint, view)

            '編集の場合、活性化
            Call Me._LMFconG.SetLockInputMan(.txtUnsoComment, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbUnsoJiyuKbn, edit, clearFlg)

            '元データ = 出荷の場合電話番号入力可　　'ADD 2018/05/14 依頼番号 001545 
            If LMFControlC.MOTO_DATA_SHUKKA.Equals(.cmbMotoDataKbn.SelectedValue.ToString()) = True Then
                Call Me._LMFconG.SetLockInputMan(.txtTel, edit, clearFlg)
            Else
                Call Me._LMFconG.SetLockInputMan(.txtTel, unso, clearFlg)
            End If


            '元データ区分 = 運送の場合、活性化
            Call Me._LMFconG.SetLockInputMan(.cmbPcKbn, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbTax, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbYosoEigyo, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbTariffKbn, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbSharyoKbn, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtOkuriNo, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtOrdNo, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtShipCd, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtBuyerOrdNo, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtTariffCd, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtExtcTariffCd, unso, clearFlg)
            ''START UMANO 要望番号1302 支払運賃に伴う修正。
            'Call Me._LMFconG.SetLockInputMan(.txtPayTariffCd, unso, clearFlg)
            'Call Me._LMFconG.SetLockInputMan(.txtPayExtcTariffCd, unso, clearFlg)
            ''END UMANO 要望番号1302 支払運賃に伴う修正。
            Call Me._LMFconG.SetLockInputMan(.imdOrigDate, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtOrigTime, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtOrigCd, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtDestTime, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtJiDestTime, unso, clearFlg)
            '要望番号:2408 2015.09.17 追加START
            Call Me._LMFconG.SetLockInputMan(.cmbAutoDenpKbn, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblAutoDenpNo, lock, clearFlg)
            '要望番号:2408 2015.09.17 追加END
            Call Me._LMFconG.SetLockInputMan(.txtDestCd, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtDestAdd3, unso, clearFlg)
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            'Call Me._LMFconG.SetLockInputMan(.txtTel, unso, clearFlg)     'DEL 2018/05/14 依頼番号 001545 
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
            Call Me._LMFconG.SetLockInputMan(.txtAreaCd, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numUnsoPkgCnt, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numUnsoWtL, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbUnsoCntUt, unso, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbThermoKbn, unso, clearFlg)
            Call Me._LMFconG.SetLockControl(.btnKeisan, unso)
            Call Me._LMFconG.SetLockControl(.btnAdd, unso)
            Call Me._LMFconG.SetLockControl(.btnDel, unso)
            Call Me._LMFconG.SetLockInputMan(.numPayPayUnchin, unso, clearFlg)

            '運行紐付けしていない場合、活性化
            Call Me._LMFconG.SetLockInputMan(.cmbBinKbn, trip, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd, trip, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd, trip, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.imdDestDate, trip, clearFlg)

            'START UMANO 要望番号1369 運行が紐づいている場合はロック
            Call Me._LMFconG.SetLockInputMan(.txtPayTariffCd, trip, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtPayExtcTariffCd, trip, clearFlg)
            'END UMANO 要望番号1369 運行が紐づいている場合はロック


            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
            '荷主明細マスタに指定されている荷主の場合、活性化
            Call Me._LMFconG.SetLockInputMan(.txtCustCdM, cust, clearFlg)
            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End
            Call Me._LMFconG.SetLockInputMan(.txtRemark, unso, clearFlg)


        End With

        'タリフ分類区分によるロック制御
        Call Me.TariffKbnLockControl()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()

            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then

                '参照モード時は印刷コンボ
                .cmbPrint.Focus()

            Else

                '元データ = 運送の場合
                If LMFControlC.MOTO_DATA_UNSO.Equals(.cmbMotoDataKbn.SelectedValue.ToString()) = True Then

                    '別営業所
                    .cmbYosoEigyo.Focus()

                Else

                    '運送事由
                    .cmbUnsoJiyuKbn.Focus()

                End If

            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '日付コントロール設定
        Call Me.SetDateFormat()

        '数値コントロール設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateFormat()

        With Me._Frm

            Me._LMFconG.SetDateFormat(.imdOrigDate)
            Me._LMFconG.SetDateFormat(.imdDestDate)

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d9 As Decimal = Convert.ToDecimal(LMF020C.MAX_9)
            Dim d10 As Decimal = Convert.ToDecimal(LMF020C.MAX_10)
            Dim dMax As Decimal = Convert.ToDecimal(LMFControlC.MAX_KETA)
            'START 要望番号1243 赤データの表示・・EDI検索
            Dim d9Minus As Decimal = Convert.ToDecimal(LMF020C.MIN_9)
            'END 要望番号1243 赤データの表示・・EDI検索

            '要望対応:1816 yamanaka 2013.02.22 Start
            .numPrtCnt.SetInputFields(LMF020C.SHARP2, , 3, 1, , 0, 0, , Convert.ToDecimal(LMF020C.MAX_3), 0)
            .numPrtCnt_From.SetInputFields(LMF020C.SHARP2, , 3, 1, , 0, 0, , Convert.ToDecimal(LMF020C.MAX_3), 0)
            .numPrtCnt_To.SetInputFields(LMF020C.SHARP2, , 3, 1, , 0, 0, , Convert.ToDecimal(LMF020C.MAX_3), 0)
            '.numPrtCnt.SetInputFields(LMF020C.SHARP2, , 2, 1, , 0, 0, , Convert.ToDecimal(LMF020C.MAX_2), 0)
            '要望対応:1816 yamanaka 2013.02.22 End

            .numUnsoPkgCnt.SetInputFields(LMF020C.SHARP10, , 10, 1, , 0, 0, , d10, 0)
            'START 要望番号1243 赤データの表示・・EDI検索
            '.numUnsoWtL.SetInputFields(LMF020C.SHARP9, , 9, 1, , 0, , , d9, 0)
            '.numUnsoWt.SetInputFields(LMF020C.SHARP9, , 9, 1, , 0, , , d9, 0)
            .numUnsoWtL.SetInputFields(LMF020C.SHARP9, , 9, 1, , 0, , , d9, d9Minus)
            .numUnsoWt.SetInputFields(LMF020C.SHARP9, , 9, 1, , 0, , , d9, d9Minus)
            'END 要望番号1243 赤データの表示・・EDI検索
            .numSeiqTariffDes.SetInputFields(LMF020C.SHARP5, , 5, 1, , 0, 0, , Convert.ToDecimal(LMF020C.MAX_5), 0)
            .numSeiqUnchin.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPayUnchin.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numCityExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numWintExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numRelyExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPassExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numInsurExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayUnsoWt.SetInputFields(LMF020C.SHARP9, , 9, 1, , 0, , , d9, 0)
            .numPaySeiqTariffDes.SetInputFields(LMF020C.SHARP5, , 5, 1, , 0, 0, , Convert.ToDecimal(LMF020C.MAX_5), 0)
            .numPayPayUnchin.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPayCityExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPayWintExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPayRelyExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPayPassExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            .numPayInsurExtc.SetInputFields(LMFControlC.MAX_18, , 15, 1, , 0, 0, , dMax, 0)
            'END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbPrint.SelectedValue = Nothing
            .numPrtCnt.Value = 0
            .numPrtCnt_From.Value = 0
            .numPrtCnt_To.Value = 0
            .cmbEigyo.SelectedValue = Nothing
            .cmbYosoEigyo.SelectedValue = Nothing
            .lblUnsoNo.TextValue = String.Empty
            .lblKanriNo.TextValue = String.Empty
            .cmbMotoDataKbn.SelectedValue = Nothing
            .cmbUnsoJiyuKbn.SelectedValue = Nothing
            .cmbPcKbn.SelectedValue = Nothing
            .cmbTax.SelectedValue = Nothing
            .lblUnkoNo.TextValue = String.Empty
            .cmbTehaiKbn.SelectedValue = Nothing
            .cmbBinKbn.SelectedValue = Nothing
            .cmbTariffKbn.SelectedValue = Nothing
            .cmbSharyoKbn.SelectedValue = Nothing
            .txtUnsocoCd.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsocoCdOld.TextValue = String.Empty
            .txtUnsocoBrCdOld.TextValue = String.Empty
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .lblUnsocoNm.TextValue = String.Empty
            .lblUnsoNm.TextValue = String.Empty
            .lblUnsoBrNm.TextValue = String.Empty
            .lblTareYn.TextValue = String.Empty
            .txtOkuriNo.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtOrdNo.TextValue = String.Empty
            .txtShipCd.TextValue = String.Empty
            .lblShipNm.TextValue = String.Empty
            .txtBuyerOrdNo.TextValue = String.Empty
            .txtTariffCd.TextValue = String.Empty
            .lblTariffRem.TextValue = String.Empty
            .txtExtcTariffCd.TextValue = String.Empty
            .lblExtcTariffRem.TextValue = String.Empty
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayTariffCd.TextValue = String.Empty
            .lblPayTariffRem.TextValue = String.Empty
            .txtPayExtcTariffCd.TextValue = String.Empty
            .lblPayExtcTariffRem.TextValue = String.Empty
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .imdDestDate.TextValue = String.Empty
            .imdOrigDate.TextValue = String.Empty
            .txtOrigTime.TextValue = String.Empty
            .txtOrigCd.TextValue = String.Empty
            .lblOrigNm.TextValue = String.Empty
            .lblOrigJisCd.TextValue = String.Empty
            .txtDestTime.TextValue = String.Empty
            .txtJiDestTime.TextValue = String.Empty
            '要望番号:2408 2015.09.17 追加START
            .cmbAutoDenpKbn.SelectedValue = String.Empty
            .lblAutoDenpNo.TextValue = String.Empty
            '要望番号:2408 2015.09.17 追加END
            .txtDestCd.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty
            .lblDestJisCd.TextValue = String.Empty
            .lblZipNo.TextValue = String.Empty
            .lblDestAdd1.TextValue = String.Empty
            .lblDestAdd2.TextValue = String.Empty
            .txtDestAdd3.TextValue = String.Empty
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            .txtTel.TextValue = String.Empty
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
            .txtAreaCd.TextValue = String.Empty
            .lblAreaNm.TextValue = String.Empty
            .numUnsoPkgCnt.Value = 0
            .numUnsoWtL.Value = 0
            .cmbUnsoCntUt.SelectedValue = Nothing
            .cmbThermoKbn.SelectedValue = Nothing
            .txtUnsoComment.TextValue = String.Empty
            .numUnsoWt.Value = 0
            .numSeiqTariffDes.Value = 0
            .numSeiqUnchin.Value = 0
            .numPayUnchin.Value = 0
            .numCityExtc.Value = 0
            .numWintExtc.Value = 0
            .numRelyExtc.Value = 0
            .numPassExtc.Value = 0
            .numInsurExtc.Value = 0
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .numPayUnsoWt.Value = 0
            .numPaySeiqTariffDes.Value = 0
            .numPayPayUnchin.Value = 0
            .numPayCityExtc.Value = 0
            .numPayWintExtc.Value = 0
            .numPayRelyExtc.Value = 0
            .numPayPassExtc.Value = 0
            .numPayInsurExtc.Value = 0
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .txtRemark.TextValue = String.Empty
            .numPrtCnt_From.Visible = False
            .numPrtCnt_To.Visible = False
            .lblTitlePrtCntFromTo.Visible = False

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ds As DataSet)

        '運送(大)データ設定
        Call Me.SetUnsoLData(ds)

        '運送(中)データ設定
        Call Me.SetSpread(ds)

        '請求運賃情報の設定
        Call Me.SetUnchinInfo(ds)

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '支払運賃情報の設定
        Call Me.SetShiharaiInfo(ds)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

    End Sub

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    ''' <summary>
    ''' 初期値を設定します(複写時)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetCopyInitValue(ByVal ds As DataSet)

        With Me._Frm
            .imdOrigDate.TextValue = ds.Tables(LMF020C.TABLE_NM_IN).Rows(0).Item("OUTKA_PLAN_DATE").ToString()
            .imdDestDate.TextValue = ds.Tables(LMF020C.TABLE_NM_IN).Rows(0).Item("ARR_PLAN_DATE").ToString()
        End With

    End Sub
    'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    ''' <summary>
    ''' 運送(大)データ設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetUnsoLData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMF020C.TABLE_NM_UNSO_L).Rows(0)
        With Me._Frm

            .cmbEigyo.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .cmbYosoEigyo.SelectedValue = dr.Item("YUSO_BR_CD").ToString()
            .lblUnsoNo.TextValue = dr.Item("UNSO_NO_L").ToString()
            .lblKanriNo.TextValue = dr.Item("INOUTKA_NO_L").ToString()
            .cmbMotoDataKbn.SelectedValue = dr.Item("MOTO_DATA_KB").ToString()
            .cmbUnsoJiyuKbn.SelectedValue = dr.Item("JIYU_KB").ToString()
            .cmbPcKbn.SelectedValue = dr.Item("PC_KB").ToString()
            .cmbTax.SelectedValue = dr.Item("TAX_KB").ToString()
            Dim tripNo As String = dr.Item("TRIP_NO").ToString()
            If LMFControlC.FLG_OFF.Equals(dr.Item("TYUKEI_HAISO_FLG").ToString()) = False Then
                tripNo = dr.Item("TRIP_NO_TYUKEI").ToString()
            End If
            .lblUnkoNo.TextValue = tripNo
            .cmbTehaiKbn.SelectedValue = dr.Item("UNSO_TEHAI_KB").ToString()
            .cmbBinKbn.SelectedValue = dr.Item("BIN_KB").ToString()
            .cmbTariffKbn.SelectedValue = dr.Item("TARIFF_BUNRUI_KB").ToString()
            .cmbSharyoKbn.SelectedValue = dr.Item("VCLE_KB").ToString()
            .txtUnsocoCd.TextValue = dr.Item("UNSO_CD").ToString()
            .txtUnsocoBrCd.TextValue = dr.Item("UNSO_BR_CD").ToString()
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsocoCdOld.TextValue = dr.Item("UNSO_CD").ToString()
            .txtUnsocoBrCdOld.TextValue = dr.Item("UNSO_BR_CD").ToString()
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            Dim unsoNm As String = dr.Item("UNSO_NM").ToString()
            Dim unsoBrNm As String = dr.Item("UNSO_BR_NM").ToString()
            .lblUnsocoNm.TextValue = Me._LMFconG.EditConcatData(unsoNm, unsoBrNm, Space(1))
            .lblUnsoNm.TextValue = unsoNm
            .lblUnsoBrNm.TextValue = unsoBrNm
            .lblTareYn.TextValue = dr.Item("TARE_YN").ToString()
            .txtOkuriNo.TextValue = dr.Item("DENP_NO").ToString()
            .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
            .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
            .lblCustNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), Space(1))
            .txtOrdNo.TextValue = dr.Item("CUST_REF_NO").ToString()
            .txtShipCd.TextValue = dr.Item("SHIP_CD").ToString()
            .lblShipNm.TextValue = dr.Item("SHIP_NM").ToString()
            .txtBuyerOrdNo.TextValue = dr.Item("BUY_CHU_NO").ToString()
            .txtTariffCd.TextValue = dr.Item("SEIQ_TARIFF_CD").ToString()
            .lblTariffRem.TextValue = dr.Item("TARIFF_REM").ToString()
            .txtExtcTariffCd.TextValue = dr.Item("SEIQ_ETARIFF_CD").ToString()
            .lblExtcTariffRem.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
            'START UMANO 要望番号1302 支払運賃に伴う修正。
            .txtPayTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
            .lblPayTariffRem.TextValue = dr.Item("SHIHARAI_TARIFF_REM").ToString()
            .txtPayExtcTariffCd.TextValue = dr.Item("SHIHARAI_ETARIFF_CD").ToString()
            .lblPayExtcTariffRem.TextValue = dr.Item("SHIHARAI_EXTC_TARIFF_REM").ToString()
            'END UMANO 要望番号1302 支払運賃に伴う修正。
            .imdOrigDate.TextValue = dr.Item("OUTKA_PLAN_DATE").ToString()
            .txtOrigTime.TextValue = dr.Item("OUTKA_PLAN_TIME").ToString()
            .txtOrigCd.TextValue = dr.Item("ORIG_CD").ToString()
            .lblOrigNm.TextValue = dr.Item("ORIG_NM").ToString()
            .lblOrigJisCd.TextValue = dr.Item("ORIG_JIS_CD").ToString()
            .imdDestDate.TextValue = dr.Item("ARR_PLAN_DATE").ToString()
            .txtDestTime.TextValue = dr.Item("ARR_PLAN_TIME").ToString()
            .txtJiDestTime.TextValue = dr.Item("ARR_ACT_TIME").ToString()
            '要望番号:2408 2015.09.17 追加START
            .beforeAutoDenpKbn.SelectedValue = dr.Item("AUTO_DENP_KBN").ToString()
            .cmbAutoDenpKbn.SelectedValue = dr.Item("AUTO_DENP_KBN").ToString()
            .lblAutoDenpNo.TextValue = dr.Item("AUTO_DENP_NO").ToString()
            '要望番号:2408 2015.09.17 追加END
            .txtDestCd.TextValue = dr.Item("DEST_CD").ToString()
            .lblDestNm.TextValue = dr.Item("DEST_NM").ToString()
            .lblDestJisCd.TextValue = dr.Item("DEST_JIS_CD").ToString()
            .lblZipNo.TextValue = dr.Item("ZIP").ToString()
            .lblDestAdd1.TextValue = dr.Item("AD_1").ToString()
            .lblDestAdd2.TextValue = dr.Item("AD_2").ToString()
            .txtDestAdd3.TextValue = dr.Item("AD_3").ToString()
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            .txtTel.TextValue = dr.Item("TEL").ToString
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
            .txtAreaCd.TextValue = dr.Item("AREA_CD").ToString()
            .lblAreaNm.TextValue = dr.Item("AREA_NM").ToString()
            .numUnsoPkgCnt.Value = dr.Item("UNSO_PKG_NB").ToString()
            .numUnsoWtL.Value = dr.Item("UNSO_WT").ToString()
            .cmbUnsoCntUt.SelectedValue = dr.Item("NB_UT").ToString()
            .cmbThermoKbn.SelectedValue = dr.Item("UNSO_ONDO_KB").ToString()
            .txtUnsoComment.TextValue = dr.Item("REMARK").ToString()
            .txtRemark.TextValue = dr.Item("NHS_REMARK").ToString()
        End With

    End Sub

    ''' <summary>
    ''' 料金情報の設定(請求運賃)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinInfo(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_INFO)

        'データが無い場合、スルー
        If dt.Rows.Count < 1 Then
            Exit Sub
        End If

        Dim dr As DataRow = dt.Rows(0)
        With Me._Frm

            'SHINODA MOD 2017/4/25 要望管理2696対応 Start

            '.numUnsoWt.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_WT").ToString())
            '.numSeiqTariffDes.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_KYORI").ToString())
            '.numSeiqUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_UNCHIN").ToString())
            '.numPayUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("PAY_UNCHIN").ToString())
            '.numCityExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_CITY_EXTC").ToString())
            '.numWintExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_WINT_EXTC").ToString())
            '.numRelyExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_RELY_EXTC").ToString())
            '.numPassExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_TOLL").ToString())
            '.numInsurExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_INSU").ToString())

            If (LMConst.AuthoKBN.AGENT).Equals(LMUserInfoManager.GetAuthoLv()) = True Then
                .numUnsoWt.Value = 0
                .numSeiqTariffDes.Value = 0
                .numSeiqUnchin.Value = 0
                .numPayUnchin.Value = 0
                .numCityExtc.Value = 0
                .numWintExtc.Value = 0
                .numRelyExtc.Value = 0
                .numPassExtc.Value = 0
                .numInsurExtc.Value = 0
            Else
                .numUnsoWt.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_WT").ToString())
                .numSeiqTariffDes.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_KYORI").ToString())
                .numSeiqUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_UNCHIN").ToString())
                .numPayUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("PAY_UNCHIN").ToString())
                .numCityExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_CITY_EXTC").ToString())
                .numWintExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_WINT_EXTC").ToString())
                .numRelyExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_RELY_EXTC").ToString())
                .numPassExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_TOLL").ToString())
                .numInsurExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_INSU").ToString())
            End If
            
            'SHINODA MOD 2017/4/25 要望管理2696対応 END

        End With

    End Sub

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 料金情報の設定(支払運賃)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetShiharaiInfo(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_SHIHARAI)

        'データが無い場合、スルー
        If dt.Rows.Count < 1 Then
            Exit Sub
        End If

        Dim dr As DataRow = dt.Rows(0)
        With Me._Frm

            'SHINODA MOD 2017/4/25 要望管理2696対応 Start
            '.numPayUnsoWt.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_WT").ToString())
            '.numPaySeiqTariffDes.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_KYORI").ToString())
            '.numPayPayUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_UNCHIN").ToString())
            '.numPayCityExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_CITY_EXTC").ToString())
            '.numPayWintExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_WINT_EXTC").ToString())
            '.numPayRelyExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_RELY_EXTC").ToString())
            '.numPayPassExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_TOLL").ToString())
            '.numPayInsurExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_INSU").ToString())

            If (LMConst.AuthoKBN.AGENT).Equals(LMUserInfoManager.GetAuthoLv()) = True Then
                .numPayUnsoWt.Value = 0
                .numPaySeiqTariffDes.Value = 0
                .numPayPayUnchin.Value = 0
                .numPayCityExtc.Value = 0
                .numPayWintExtc.Value = 0
                .numPayRelyExtc.Value = 0
                .numPayPassExtc.Value = 0
                .numPayInsurExtc.Value = 0
            Else
                .numPayUnsoWt.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_WT").ToString())
                .numPaySeiqTariffDes.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_KYORI").ToString())
                .numPayPayUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_UNCHIN").ToString())
                .numPayCityExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_CITY_EXTC").ToString())
                .numPayWintExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_WINT_EXTC").ToString())
                .numPayRelyExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_RELY_EXTC").ToString())
                .numPayPassExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_TOLL").ToString())
                .numPayInsurExtc.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_INSU").ToString())
            End If
            'SHINODA MOD 2017/4/25 要望管理2696対応 END

        End With

    End Sub
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' タリフ分類区分によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TariffKbnLockControl()

        Dim kurumaFlg As Boolean = True
        Dim extcFlg As Boolean = False

        With Me._Frm

            '参照の場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '元データ区分 <> 運送の場合、スルー
            If LMFControlC.MOTO_DATA_UNSO.Equals(.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
                Exit Sub
            End If

            Select Case .cmbTariffKbn.SelectedValue.ToString()

                Case LMFControlC.TARIFF_KURUMA

                    '車扱いの場合、活性化
                    kurumaFlg = False

                Case LMFControlC.TARIFF_YOKO

                    '車扱いの場合、活性化
                    kurumaFlg = False

                    '横持ちの場合、ロック
                    extcFlg = True

                    'START UMANO 要望番号1369 支払運賃に伴う修正。
                    Call Me._LMFconG.SetLockInputMan(.txtPayExtcTariffCd, extcFlg)
                    'END UMANO 要望番号1369 支払運賃に伴う修正。

                    '備考をクリア
                    .lblExtcTariffRem.TextValue = String.Empty

            End Select

            Call Me._LMFconG.SetLockInputMan(.cmbSharyoKbn, kurumaFlg)
            Call Me._LMFconG.SetLockInputMan(.txtExtcTariffCd, extcFlg)
            ''START UMANO 要望番号1302 支払運賃に伴う修正。
            'Call Me._LMFconG.SetLockInputMan(.txtPayExtcTariffCd, extcFlg)
            ''END UMANO 要望番号1302 支払運賃に伴う修正。

        End With

    End Sub

    ''' <summary>
    ''' タリフセットマスタの値を設定
    ''' </summary>
    ''' <param name="shimeKbn">締め日基準</param>
    ''' <returns>True:セットマスタに存在する False:セットマスタに存在しない</returns>
    ''' <remarks></remarks>
    Friend Function SetTariffSetData(ByVal shimeKbn As String) As Boolean

        With Me._Frm

            'タリフセットから値取得
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim destCd As String = .txtDestCd.TextValue
            Dim drs As DataRow() = Me._LMFconG.SelectTariffSetListDataRow(brCd _
                                                                      , .txtCustCdL.TextValue _
                                                                      , .txtCustCdM.TextValue _
                                                                      , String.Empty _
                                                                      , String.Empty _
                                                                      , String.Empty _
                                                                      , String.Empty _
                                                                      , LMF020C.SET_KBN_DEST _
                                                                      , destCd _
                                                                      )

            '取得できていない場合、スルー
            If drs.Length < 1 Then
                Return False
            End If

            Dim dr As DataRow = drs(0)
            Dim tariffKbn As String = dr.Item("TARIFF_BUNRUI_KB").ToString()
            Dim tariffCd As String = Me.GetTariffSetCd(dr, tariffKbn)

            'タリフ備考設定
            Call Me.SelectTariffMst(tariffCd, tariffKbn, shimeKbn)

            .txtTariffCd.TextValue = tariffCd
            .cmbTariffKbn.SelectedValue = tariffKbn
            .txtExtcTariffCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' タリフコードを取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <returns>タリフコードを</returns>
    ''' <remarks></remarks>
    Private Function GetTariffSetCd(ByVal dr As DataRow, ByVal tariffKbn As String) As String

        GetTariffSetCd = String.Empty

        Select Case tariffKbn

            Case LMFControlC.TARIFF_KURUMA

                GetTariffSetCd = dr.Item("UNCHIN_TARIFF_CD2").ToString()

            Case LMFControlC.TARIFF_YOKO

                GetTariffSetCd = dr.Item("YOKO_TARIFF_CD").ToString()

            Case Else

                GetTariffSetCd = dr.Item("UNCHIN_TARIFF_CD1").ToString()

        End Select

        Return GetTariffSetCd

    End Function

    ''' <summary>
    ''' タリフ備考を設定
    ''' </summary>
    ''' <param name="tariffCd">タリフコード</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <param name="shimeKbn">締め日基準</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SelectTariffMst(ByVal tariffCd As String, ByVal tariffKbn As String, ByVal shimeKbn As String) As Boolean

        With Me._Frm

            '値のクリア
            .lblTakSize.TextValue = String.Empty
            .lblCalcKbn.TextValue = String.Empty
            .lblSplitFlg.TextValue = String.Empty

            'タリフ備考を取得
            Dim drs As DataRow() = Nothing
            Dim colNm As String = String.Empty
            Dim code As String = String.Empty
            Dim calcKbn As String = String.Empty
            Dim splitFlg As String = String.Empty
            Dim tbType As String = String.Empty
            Select Case tariffKbn

                Case LMFControlC.TARIFF_YOKO

                    drs = Me._LMFconG.SelectYokoTariffListDataRow(.cmbEigyo.SelectedValue.ToString(), tariffCd)
                    code = "YOKO_TARIFF_CD"
                    colNm = "YOKO_REM"
                    calcKbn = "CALC_KB"
                    splitFlg = "SPLIT_FLG"

                Case Else

                    Dim chkDate As String = .imdOrigDate.TextValue
                    If LMFControlC.CALC_NYUKA.Equals(shimeKbn) = True Then
                        chkDate = .imdDestDate.TextValue
                    End If

                    drs = Me._LMFconG.SelectUnchinTariffListDataRow(tariffCd, String.Empty, Me.GetUnchinTariffDate(shimeKbn))
                    code = "UNCHIN_TARIFF_CD"
                    colNm = "UNCHIN_TARIFF_REM"
                    tbType = "TABLE_TP"

            End Select

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Return False
            End If

            'マスタの値を設定
            .txtTariffCd.TextValue = drs(0).Item(code).ToString()
            .lblTariffRem.TextValue = drs(0).Item(colNm).ToString()

            'チェック用のコントロールに値を設定
            If String.IsNullOrEmpty(calcKbn) = False Then
                .lblCalcKbn.TextValue = drs(0).Item(calcKbn).ToString()
                .lblSplitFlg.TextValue = drs(0).Item(splitFlg).ToString()
            End If
            If String.IsNullOrEmpty(tbType) = False Then
                .lblTakSize.TextValue = drs(0).Item(tbType).ToString()
            End If

            Return True

        End With

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払タリフ備考を設定
    ''' </summary>
    ''' <param name="payTariffCd">支払タリフコード</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <param name="shimeKbn">締め日基準</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiTariffMst(ByVal payTariffCd As String, ByVal tariffKbn As String, ByVal shimeKbn As String) As Boolean

        With Me._Frm

            '値のクリア
            .lblTakSize.TextValue = String.Empty
            .lblCalcKbn.TextValue = String.Empty
            .lblSplitFlg.TextValue = String.Empty

            'タリフ備考を取得
            Dim drs As DataRow() = Nothing
            Dim colNm As String = String.Empty
            Dim code As String = String.Empty
            Dim calcKbn As String = String.Empty
            Dim splitFlg As String = String.Empty
            Dim tbType As String = String.Empty
            Select Case tariffKbn

                Case LMFControlC.TARIFF_YOKO

                    drs = Me._LMFconG.SelectShiharaiYokoTariffListDataRow(.cmbEigyo.SelectedValue.ToString(), payTariffCd)
                    code = "YOKO_TARIFF_CD"
                    colNm = "YOKO_REM"
                    calcKbn = "CALC_KB"
                    splitFlg = "SPLIT_FLG"

                Case Else

                    Dim chkDate As String = .imdOrigDate.TextValue
                    If LMFControlC.CALC_NYUKA.Equals(shimeKbn) = True Then
                        chkDate = .imdDestDate.TextValue
                    End If

                    drs = Me._LMFconG.SelectShiharaiTariffListDataRow(payTariffCd, String.Empty, Me.GetShiharaiTariffDate(shimeKbn))
                    code = "SHIHARAI_TARIFF_CD"
                    colNm = "SHIHARAI_TARIFF_REM"
                    tbType = "TABLE_TP"

            End Select

            '取得できない場合、スルー
            If drs.Length < 1 Then
                Return False
            End If

            'マスタの値を設定
            .txtPayTariffCd.TextValue = drs(0).Item(code).ToString()
            .lblPayTariffRem.TextValue = drs(0).Item(colNm).ToString()

            ''チェックが必要なのかペンディング
            ''↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
            ''チェック用のコントロールに値を設定
            'If String.IsNullOrEmpty(calcKbn) = False Then
            '    .lblPayCalcKbn.TextValue = drs(0).Item(calcKbn).ToString()
            '    .lblPaySplitFlg.TextValue = drs(0).Item(splitFlg).ToString()
            'End If
            'If String.IsNullOrEmpty(tbType) = False Then
            '    .lblTakSize.TextValue = drs(0).Item(tbType).ToString()
            'End If
            ''↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑↑

            Return True

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 運賃タリフの適応開始日(抽出条件)を取得
    ''' </summary>
    ''' <param name="shimeKbn">締め日基準</param>
    ''' <returns>適応開始日</returns>
    ''' <remarks></remarks>
    Friend Function GetShiharaiTariffDate(ByVal shimeKbn As String) As String

        With Me._Frm

            '適用開始日は納入予定日とする
            Dim chkDate As String = .imdDestDate.TextValue

            Return chkDate

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 運賃タリフの適応開始日(抽出条件)を取得
    ''' </summary>
    ''' <param name="shimeKbn">締め日基準</param>
    ''' <returns>適応開始日</returns>
    ''' <remarks></remarks>
    Friend Function GetUnchinTariffDate(ByVal shimeKbn As String) As String

        With Me._Frm

            Dim chkDate As String = .imdOrigDate.TextValue
            If LMFControlC.CALC_NYUKA.Equals(shimeKbn) = True Then
                chkDate = .imdDestDate.TextValue
            End If

            Return chkDate

        End With

    End Function

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードOLD設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub SetUnsoCdOld(ByVal frm As LMF020F)

        frm.txtUnsocoCdOld.TextValue = frm.txtUnsocoCd.TextValue
        frm.txtUnsocoBrCdOld.TextValue = frm.txtUnsocoBrCd.TextValue

    End Sub

    ''' <summary>
    ''' 運賃タリフを運賃タリフセットマスタキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetUnchinTariffSet(ByVal frm As LMF020F, ByVal tariffBunruiFlg As Boolean)

        Dim updateFlg As Boolean = False

        With frm
            Dim tariffSetDr() As DataRow = Nothing

            If tariffBunruiFlg = False Then
                tariffSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET_UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                          "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                                          "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsocoCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtUnsocoBrCd.TextValue, "' AND ", _
                                                                                                                          "UNSO_TEHAI_KB = '", .cmbTehaiKbn.SelectedValue, "'"))
            Else
                tariffSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET_UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                          "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                                          "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsocoCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtUnsocoBrCd.TextValue, "' AND ", _
                                                                                                                          "UNSO_TEHAI_KB = '", .cmbTehaiKbn.SelectedValue, "' AND ", _
                                                                                                                          "TARIFF_BUNRUI_KB = '", .cmbTariffKbn.SelectedValue, "'"))

            End If

            If 0 < tariffSetDr.Length Then
                .txtTariffCd.TextValue = String.Empty
                .lblTariffRem.TextValue = String.Empty
                .txtExtcTariffCd.TextValue = String.Empty
                .lblExtcTariffRem.TextValue = String.Empty
                .cmbTariffKbn.SelectedValue = tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString

                If ("10").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '混載の場合
                    .txtTariffCd.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD").ToString
                    .lblTariffRem.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD_NM").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                ElseIf ("20").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '車扱いの場合
                    .txtTariffCd.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD").ToString
                    .lblTariffRem.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD_NM").ToString
                    .txtExtcTariffCd.TextValue = tariffSetDr(0).Item("EXTC_TARIFF_CD").ToString
                ElseIf ("30").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '特便の場合
                ElseIf ("40").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '横持ちの場合
                End If

            End If

        End With

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    '要望対応:1816 yamanaka 2013.02.22 Start
    ''' <summary>
    ''' 印刷種別による値の設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub SetCmbPrint(ByVal frm As LMF020F)

        With frm

            Dim Busu As String = "0"

            Select Case .cmbPrint.SelectedValue.ToString()

                Case LMF020C.PRINT_NOUHIN, LMF020C.PRINT_OKURI, LMF020C.PRINT_HIKITORI, LMF020C.PRINT_KONPOU, LMF020C.PRINT_ALL     'UPD 2018/11/21　依頼番号 : 002743    LMF020C.PRINT_ALL追加
                    Busu = "1"

                Case LMF020C.PRINT_NIFUDA
                    frm.numPrtCnt_From.TextValue = "1"
                    frm.numPrtCnt_To.TextValue = .numUnsoPkgCnt.TextValue

            End Select

            frm.numPrtCnt.TextValue = Busu

        End With

    End Sub
    '要望対応:1816 yamanaka 2013.02.22 End

    ''' <summary>
    ''' 部数範囲の設定（印刷種別変更時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub PrintCntSetControl(ByVal frm As LMF020F)

        With frm

            .numPrtCnt.Visible = False
            .numPrtCnt_From.Visible = False
            .numPrtCnt_To.Visible = False
            .lblTitleBu.Visible = False
            .lblTitlePrtCntFromTo.Visible = False

            If (LMF020C.PRINT_NIFUDA).Equals(.cmbPrint.SelectedValue) = True Then
                .numPrtCnt_From.Visible = True
                .numPrtCnt_To.Visible = True
                .lblTitlePrtCntFromTo.Visible = True
            Else
                .numPrtCnt.Visible = True
                .lblTitleBu.Visible = True
            End If

        End With

    End Sub

    '要望番号:2408 2015.09.17 追加START
    ''' <summary>
    ''' 自動送状区分を運送会社マスタキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetAutoDenpSet(ByVal frm As LMF020F)

        Dim updateFlg As Boolean = False

        With frm
            Dim unsoSetDr() As DataRow = Nothing

            'unsoSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
            '                                                                                                              "UNSOCO_CD = '", .txtUnsocoCd.TextValue, "' AND ", _
            '                                                                                                              "UNSOCO_BR_CD = '", .txtUnsocoBrCd.TextValue, "'"))
            '20160930 要番2622 tsunehira add
            unsoSetDr = Me._LMFconG.SelectUnsocoListDataRow(.cmbEigyo.SelectedValue.ToString(), .txtUnsocoCd.TextValue, .txtUnsocoBrCd.TextValue)

            If 0 < unsoSetDr.Length Then
                If unsoSetDr(0).Item("AUTO_DENP_NO_FLG").ToString.Equals(LMConst.FLG.ON) = True Then
                    .cmbAutoDenpKbn.SelectedValue = unsoSetDr(0).Item("AUTO_DENP_NO_KBN").ToString
                Else
                    .cmbAutoDenpKbn.SelectedValue = String.Empty
                End If
                If .cmbAutoDenpKbn.SelectedValue.Equals(.beforeAutoDenpKbn.SelectedValue) = False Then
                    .lblAutoDenpNo.TextValue = String.Empty
                End If

                '要望番号:2408 2015.09.17 追加START
                If unsoSetDr(0).Item("AUTO_DENP_NO_FLG").ToString.Equals(LMConst.FLG.ON) = True Then
                    .cmbAutoDenpKbn.SelectedValue = unsoSetDr(0).Item("AUTO_DENP_NO_KBN").ToString
                    If .cmbAutoDenpKbn.SelectedValue.Equals(.beforeAutoDenpKbn.SelectedValue) = False Then
                        .lblAutoDenpNo.TextValue = String.Empty
                    End If
                End If
                '要望番号:2408 2015.09.17 追加END

            End If

        End With

    End Sub
    '要望番号:2408 2015.09.17 追加END

    '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
    ''' <summary>
    ''' トールの送状番号先頭の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetTollOkurijyoTblDataSet(ByRef ds As DataSet) As Boolean

        With Me._frm

            'DataSet初期化
            ds.Tables(LMF020C.TABLE_NM_OKURIJYO_WK).Clear()

            '区分マスタより取得
            Dim kbnDetailDr() As DataRow = Nothing
            kbnDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T029' AND ", _
                                                                                               "KBN_NM1 = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                               "KBN_NM2 = '", .txtUnsocoCd.TextValue, "' AND ", _
                                                                                               "KBN_NM3 = '", .txtUnsocoBrCd.TextValue, "'"))

            If kbnDetailDr.Count > 0 Then
                Dim row As DataRow = ds.Tables(LMF020C.TABLE_NM_OKURIJYO_WK).NewRow
                Row("OKURIJYO_HEAD") = kbnDetailDr(0).Item("KBN_NM4")

                ds.Tables(LMF020C.TABLE_NM_OKURIJYO_WK).Rows.Add(row)
            End If

            Return True
        End With
    End Function
    '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared PRT_ORDER As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.PRT_ORDER, "印順", 40, True)  'ADD 2018/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.GOODS_CD, "商品KEY", 170, True)
        '要望対応:1816 yamanaka 2013.02.22 Start
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.GOODS_CD_CUST, "商品CD", 170, True)
        '要望対応:1816 yamanaka 2013.02.22 End
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.GOODS_NM, "商品名", 170, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.LOT_NO, "ロット№", 90, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.JURYO, "個別重量", 120, True)
        Public Shared UNSO_KOSU As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.UNSO_KOSU, "運送個数", 100, True)
        Public Shared KOSU_TANI As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.KOSU_TANI, "個数単位", 80, True)
        Public Shared UNSO_SURYO As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.UNSO_SURYO, "運送数量", 120, True)
        Public Shared SURYO_TANI As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.SURYO_TANI, "数量単位", 80, True)
        Public Shared HASU As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.HASU, "端数", 100, True)
        Public Shared ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.ZAI_REC_NO, "在庫" & vbCrLf & "レコード番号", 120, True)
        Public Shared ONDO_KANRI As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.ONDO_KANRI, "温度管理", 130, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.IRIME, "入目", 100, True)
        Public Shared IRIME_TANI As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.IRIME_TANI, "入目単位", 80, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.REMARK, "備考", 120, True)
        Public Shared SIZE As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.SIZE, "宅急便サイズ区分", 130, True)
        Public Shared KONPO_KOSU As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.KONPO_KOSU, "梱包個数", 100, True)
        Public Shared ZAI_BUKA As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.ZAI_BUKA, "在庫部課", 70, True)
        Public Shared HOKA_BUKA As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.HOKA_BUKA, "扱い部課", 70, True)
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.STD_IRIME_NB, "標準入目", 0, False)
        Public Shared STD_WT_KGS As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.STD_WT_KGS, "標準重量", 0, False)
        Public Shared CALC_FLG As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.CALC_FLG, "計算フラグ", 0, False)
        Public Shared TARE_YN As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.TARE_YN, "風袋計算フラグ", 0, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.REC_NO, "レコード番号", 0, False)
        Public Shared UNSO_HOKEN_UM As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.UNSO_HOKEN_UM, "運送保険料" & vbCrLf & "有無", 80, True)     'ADD 2022/01/24 026832
        Public Shared KITAKU_GOODS_UP As SpreadColProperty = New SpreadColProperty(LMF020C.SprColumnIndex.KITAKU_GOODS_UP, "寄託商品単価", 120, True)     'ADD 2022/01/12 026832
    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = LMF020C.SprColumnIndex.CLM_NUM

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMF020G.sprDetailDef())
            .sprDetail.SetColProperty(New LMF020G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.納入予定で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMF020G.sprDetailDef.GOODS_NM.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        'ロック制御用
        Dim flg As Boolean() = Me.LockControlFlg()
        Dim edit As Boolean = flg(LMF020C.Lock.EDIT)
        Dim unso As Boolean = flg(LMF020C.Lock.UNSO)

        'セルに設定するスタイルの取得
        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sMix As StyleInfo = Me.StyleInfoTextMix(spr, 100, edit)
        Dim sHan40 As StyleInfo = Me.StyleInfoTextHankaku(spr, 40, unso)
        Dim sEidaisu7 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 7, unso)
        Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr, unso)
        Dim sNum6dec3 As StyleInfo = Me.StyleInfoNum6dec3(spr, unso)
        Dim sNum9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr, unso)
        Dim sNum9 As StyleInfo = Me.StyleInfoNum9(spr, unso)     'ADD 2022/01/19 
        Dim sOndo As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_U006, unso)
        Dim sSize As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_T010, unso)
        Dim sLbl As StyleInfo = Me.StyleInfoLabel(spr)
        Dim sNisugata As StyleInfo = Nothing
        Dim sNum2 As StyleInfo = Me.StyleInfoNum2(spr, unso)   'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
        Dim sUNSO_HOKEN_UM As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_U009, unso)   'ADD 2022/01/24

        '値設定用の変数
        Dim rowCnt As Integer = 0
        Dim setDt As DataTable = ds.Tables(LMF020C.TABLE_NM_UNSO_M)
        Dim setDr As DataRow = Nothing
        Dim max As Integer = setDt.Rows.Count - 1
        Dim calcData As String = String.Empty
        Dim calcFlg As Boolean = True

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To max

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                '設定する行
                setDr = setDt.Rows(i)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.PRT_ORDER.ColNo, sNum2)   'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
                '(2012.11.13)要望番号1575 商品名はロックしない。 -- START --
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.GOODS_NM.ColNo, sMix)
                '(2012.11.13)要望番号1575 商品名はロックしない。 --  END  --
                '要望対応:1816 yamanaka 2013.02.22 Start
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.GOODS_CD_CUST.ColNo, sLbl)
                '要望対応:1816 yamanaka 2013.02.22 End
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.LOT_NO.ColNo, sHan40)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.UNSO_KOSU.ColNo, sNum10)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.HASU.ColNo, sNum10)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.ZAI_REC_NO.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.ONDO_KANRI.ColNo, sOndo)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.IRIME.ColNo, sNum6dec3)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.REMARK.ColNo, sMix)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.SIZE.ColNo, sSize)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.KONPO_KOSU.ColNo, sNum10)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.ZAI_BUKA.ColNo, sEidaisu7)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.HOKA_BUKA.ColNo, sEidaisu7)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.STD_IRIME_NB.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.STD_WT_KGS.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.CALC_FLG.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.TARE_YN.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.REC_NO.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.UNSO_HOKEN_UM.ColNo, sUNSO_HOKEN_UM)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.KITAKU_GOODS_UP.ColNo, sLbl)     'ADD 2022/01/12 026832

                '計算フラグによるロック制御
                calcData = setDr.Item("CALC_FLG").ToString()
                calcFlg = Me.LockCalcControl(calcData)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.GOODS_CD.ColNo, Me.StyleInfoTextHankaku(spr, 20, calcFlg))
                '(2012.11.13)要望番号1575 商品名はロックしない。 -- START --
                '.SetCellStyle(rowCnt, LMF020G.sprDetailDef.GOODS_NM.ColNo, Me.StyleInfoTextMixImeOFF(spr, 60, calcFlg)) '検証結果_導入時要望 №62対応(2011.09.13)
                '(2012.11.13)要望番号1575 商品名はロックしない。 --  END  --
                'START 要望番号1243 赤データの表示・・EDI検索
                '.SetCellStyle(rowCnt, LMF020G.sprDetailDef.JURYO.ColNo, Me.StyleInfoNum9dec3(spr, calcFlg))
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.JURYO.ColNo, Me.StyleInfoNum9dec3Minus(spr, calcFlg))

                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.UNSO_HOKEN_UM.ColNo, sUNSO_HOKEN_UM)
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.KITAKU_GOODS_UP.ColNo, Me.StyleInfoNum9(spr, calcFlg))
                'END 要望番号1243 赤データの表示・・EDI検索
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.KOSU_TANI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K002, calcFlg))
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.UNSO_SURYO.ColNo, Me.StyleInfoNum9dec3(spr, calcFlg))
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.SURYO_TANI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_N001, calcFlg))
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.IRIME_TANI.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_I001, calcFlg))
                .SetCellStyle(rowCnt, LMF020G.sprDetailDef.UNSO_HOKEN_UM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_U009, calcFlg))  'ADD 2022/01/25

                '値設定
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.GOODS_CD.ColNo, setDr.Item("GOODS_CD_NRS").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.PRT_ORDER.ColNo, setDr.Item("PRINT_SORT").ToString())     'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
                '要望対応:1816 yamanaka 2013.02.22 Start
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.GOODS_CD_CUST.ColNo, setDr.Item("GOODS_CD_CUST").ToString())
                '要望対応:1816 yamanaka 2013.02.22 End
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.GOODS_NM.ColNo, setDr.Item("GOODS_NM").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.LOT_NO.ColNo, setDr.Item("LOT_NO").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.JURYO.ColNo, setDr.Item("BETU_WT").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.UNSO_KOSU.ColNo, setDr.Item("UNSO_TTL_NB").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.KOSU_TANI.ColNo, setDr.Item("NB_UT").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.UNSO_SURYO.ColNo, setDr.Item("UNSO_TTL_QT").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.SURYO_TANI.ColNo, setDr.Item("QT_UT").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.HASU.ColNo, setDr.Item("HASU").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.ZAI_REC_NO.ColNo, setDr.Item("ZAI_REC_NO").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.ONDO_KANRI.ColNo, setDr.Item("UNSO_ONDO_KB").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.IRIME.ColNo, setDr.Item("IRIME").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.IRIME_TANI.ColNo, setDr.Item("IRIME_UT").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.REMARK.ColNo, setDr.Item("REMARK").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.SIZE.ColNo, setDr.Item("SIZE_KB").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.KONPO_KOSU.ColNo, setDr.Item("PKG_NB").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.ZAI_BUKA.ColNo, setDr.Item("ZBUKA_CD").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.HOKA_BUKA.ColNo, setDr.Item("ABUKA_CD").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.STD_IRIME_NB.ColNo, setDr.Item("STD_IRIME_NB").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.STD_WT_KGS.ColNo, setDr.Item("STD_WT_KGS").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.CALC_FLG.ColNo, calcData)
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.TARE_YN.ColNo, setDr.Item("TARE_YN").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.REC_NO.ColNo, Me._LMFconG.SetMaeZeroData(i.ToString(), 3))

                .SetCellValue(rowCnt, LMF020G.sprDetailDef.KITAKU_GOODS_UP.ColNo, setDr.Item("KITAKU_GOODS_UP").ToString())
                .SetCellValue(rowCnt, LMF020G.sprDetailDef.UNSO_HOKEN_UM.ColNo, setDr.Item("UNSO_HOKEN_UM").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 計算フラグによるロック判定
    ''' </summary>
    ''' <param name="calcFlg">計算フラグ</param>
    ''' <returns>
    ''' True:ロック
    ''' True:ロック解除
    ''' </returns>
    ''' <remarks></remarks>
    Private Function LockCalcControl(ByVal calcFlg As String) As Boolean

        '参照モードの場合、ロック
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
            Return True
        End If

        '元データ区分 <> 運送の場合、ロック
        If LMFControlC.MOTO_DATA_UNSO.Equals(Me._Frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
            Return True
        End If

        '計算フラグ = 1の場合、ロック解除
        If LMConst.FLG.ON.Equals(calcFlg) = True Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 並び替え処理
    ''' </summary>
    ''' <param name="spr">スプレッドシート</param>
    ''' <param name="colNo">ソート列</param>
    ''' <remarks></remarks>
    Friend Sub sprSortColumnCommand(ByVal spr As LMSpread, ByVal colNo As Integer)

        spr.ActiveSheet.SortRows(colNo, True, False)

    End Sub

    ''' <summary>
    ''' 商品Pop選択時のロック制御 + 値設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Friend Sub SetGoodsInfo(ByVal dr As DataRow, ByVal rowNo As Integer)

        Dim spr As LMSpread = Me._Frm.sprDetail

        With spr

            '値設定
            .SetCellValue(rowNo, LMF020G.sprDetailDef.GOODS_CD.ColNo, dr.Item("GOODS_CD_NRS").ToString())
            '要望対応:1816 yamanaka 2013.02.22 Start
            .SetCellValue(rowNo, LMF020G.sprDetailDef.GOODS_CD_CUST.ColNo, dr.Item("GOODS_CD_CUST").ToString())
            '要望対応:1816 yamanaka 2013.02.22 End
            .SetCellValue(rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM_1").ToString())
            Dim wt As String = dr.Item("STD_WT_KGS").ToString()
            .SetCellValue(rowNo, LMF020G.sprDetailDef.JURYO.ColNo, wt)
            .SetCellValue(rowNo, LMF020G.sprDetailDef.KOSU_TANI.ColNo, dr.Item("NB_UT").ToString())
            .SetCellValue(rowNo, LMF020G.sprDetailDef.SURYO_TANI.ColNo, dr.Item("PKG_UT").ToString())
            Dim irime As String = dr.Item("STD_IRIME_NB").ToString()
            .SetCellValue(rowNo, LMF020G.sprDetailDef.IRIME.ColNo, irime)
            .SetCellValue(rowNo, LMF020G.sprDetailDef.IRIME_TANI.ColNo, dr.Item("STD_IRIME_UT").ToString())
            .SetCellValue(rowNo, LMF020G.sprDetailDef.KONPO_KOSU.ColNo, dr.Item("PKG_NB").ToString())
            .SetCellValue(rowNo, LMF020G.sprDetailDef.STD_IRIME_NB.ColNo, irime)
            .SetCellValue(rowNo, LMF020G.sprDetailDef.STD_WT_KGS.ColNo, wt)
            .SetCellValue(rowNo, LMF020G.sprDetailDef.TARE_YN.ColNo, dr.Item("TARE_YN").ToString())
            .SetCellValue(rowNo, LMF020G.sprDetailDef.CALC_FLG.ColNo, LMConst.FLG.ON)
            Dim KITAKU As String = dr.Item("KITAKU_GOODS_UP").ToString()
            .SetCellValue(rowNo, LMF020G.sprDetailDef.KITAKU_GOODS_UP.ColNo, KITAKU)

            'ロック制御
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.GOODS_CD.ColNo)
            '(2012.11.09)要望番号1575 商品名はロックしない。 -- START --
            'Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo)
            '(2012.11.09)要望番号1575 商品名はロックしない。 --  END  --
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.JURYO.ColNo)
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.UNSO_SURYO.ColNo)
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.KOSU_TANI.ColNo)
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.SURYO_TANI.ColNo)
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.IRIME_TANI.ColNo)
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.KITAKU_GOODS_UP.ColNo)            'ADD 2022/01/12 026832
            Call Me._LMFconG.SetLockCell(spr, rowNo, LMF020G.sprDetailDef.UNSO_HOKEN_UM.ColNo)            'ADD 2022/01/24 026832

        End With

    End Sub

#End Region

#Region "ユーティリティ"

#Region "ロック判定"

    ''' <summary>
    ''' ロック判定
    ''' </summary>
    ''' <returns>Boolean配列</returns>
    ''' <remarks>
    ''' ①:参照モード
    ''' ②:編集モード
    ''' ③:データ区分 = 運送
    ''' ④:常にロック
    ''' ⑤:運行紐付けによるロック
    ''' </remarks>
    Private Function LockControlFlg() As Boolean()

        Dim view As Boolean = True
        Dim edit As Boolean = True
        Dim unso As Boolean = True
        Dim lock As Boolean = True
        Dim trip As Boolean = True
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
        Dim cust As Boolean = True
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

        Dim unLock As Boolean = False

        With Me._Frm

            Dim moto As String = .cmbMotoDataKbn.SelectedValue.ToString()

            'モード切替
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then

                '要望番号:1931(●ハネ●運送画面より、出力したい) 2013/03/06 本明 Start
                ''参照 且つ 元データ = 運送の場合、活性化
                'If LMFControlC.MOTO_DATA_UNSO.Equals(moto) = True Then
                '参照 且つ 元データ = 運送または出荷の場合、活性化
                If LMFControlC.MOTO_DATA_UNSO.Equals(moto) = True Or LMFControlC.MOTO_DATA_SHUKKA.Equals(moto) = True Then
                    '要望番号:1931(●ハネ●運送画面より、出力したい) 2013/03/06 本明 End
                    view = unLock
                End If

            Else

                '編集の場合、活性化
                edit = unLock

                '元データ区分 = 運送の場合、活性化
                If LMFControlC.MOTO_DATA_UNSO.Equals(moto) = True Then
                    unso = unLock

                    '運行紐付け無しの場合、活性化
                    If String.IsNullOrEmpty(.lblUnkoNo.TextValue) = True Then
                        trip = unLock
                    End If

                    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
                    '荷主明細マスタに指定されている荷主の場合、活性化
                    '要望番号1253:(荷主明細マスタのキャッシュに営業所コードを保持していない) 2012/07/13 本明 Start
                    'Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._Frm.txtCustCdL.TextValue, "' AND SUB_KB = '35'"))
                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue.ToString(), "' AND CUST_CD = '", Me._Frm.txtCustCdL.TextValue, "' AND SUB_KB = '35'"))
                    '要望番号1253:(荷主明細マスタのキャッシュに営業所コードを保持していない) 2012/07/13 本明 End
                    If custDetailsDr.Length > 0 Then
                        cust = unLock
                    End If
                    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

                End If

            End If

        End With
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
        'Return New Boolean() {edit, unso, view, lock, trip}
        Return New Boolean() {edit, unso, view, lock, trip, cust}
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

    End Function

#End Region

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
    ''' セルのプロパティを設定(全半角)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, lock)

    End Function

    '検証結果_導入時要望 №62対応(2011.09.13)START---------
    ''' <summary>
    ''' セルのプロパティを設定(IMEOFF)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMixImeOFF(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, length, lock)

    End Function
    '検証結果_導入時要望 №62対応(2011.09.13)END---------

    ''' <summary>
    ''' セルのプロパティを設定(半角)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextHankaku(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(英大数)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextEidaisu(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, lock, 3, , ",")

    End Function

#If True Then       'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加

    Private Function StyleInfoNum2(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99, lock, 0, , ",")

    End Function
#End If

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数6桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum6dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, lock, 3, , ",")

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

    'START 要望番号1243 赤データの表示・・EDI検索
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁 マイナス値有り)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3Minus(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -999999999.999, 999999999.999, lock, 3, , ",")

    End Function
    'END 要望番号1243 赤データの表示・・EDI検索

#If True Then   ' ADD 2022/02/19 026832
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　 マイナス値有り)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -999999999, 999999999, lock, 0, , ",")
    End Function
#End If

#End Region

#End Region

#End Region

End Class