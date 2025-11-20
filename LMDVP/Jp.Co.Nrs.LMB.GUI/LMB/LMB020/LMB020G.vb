' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB020G : 入荷データ編集
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMB020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMB020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB020F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconG As LMBControlG

    ''' <summary>
    ''' ハンドラ共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconH As LMBControlH

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB020F, ByVal g As LMBControlG, ByVal h As LMBControlH)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        '共通クラスの設定
        Me._LMBconG = g
        Me._LMBconH = h

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal actionType As LMB020C.ActionType)

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim f2 As Boolean = False
        Dim f9 As Boolean = False
        Dim lock As Boolean = False

        'モード判定
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then

            '参照モード時、活性化
            view = True
            f2 = True

        Else

            '編集モード時、活性化
            edit = True
            f9 = edit

            '特殊編集モードの場合
            Select Case actionType

                Case LMB020C.ActionType.UNSOEDIT, LMB020C.ActionType.DATEEDIT

                    f2 = True
                    f9 = False
            End Select

        End If

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新　規"
            .F2ButtonName = "編　集"
            .F3ButtonName = "複　写"
            .F4ButtonName = "削　除"
            .F5ButtonName = "検品取込"
            .F6ButtonName = "取　込"
            .F7ButtonName = "運送修正"
            .F8ButtonName = "起算日修正"
            .F9ButtonName = "追加(中)"
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = view     '(F1) 新規
            .F2ButtonEnabled = f2       '(F2) 編集
            .F3ButtonEnabled = view     '(F3) 複写
            .F4ButtonEnabled = view     '(F4) 削除
            .F5ButtonEnabled = edit     '(F5) 検品取込
            .F6ButtonEnabled = edit     '(F6) CSV取込
            .F7ButtonEnabled = view     '(F7) 運送修正
            .F8ButtonEnabled = view     '(F8) 起算日修正
            .F9ButtonEnabled = f9      '(F9) 追加(中)
            .F10ButtonEnabled = edit  '(F10)マスタ参照
            .F11ButtonEnabled = edit  '(F11)保存
            .F12ButtonEnabled = always  '(F12)閉じる

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーの制御(F2)
    ''' </summary>
    ''' <param name="f2"></param>
    Friend Sub SetEditBtnEnabled(ByVal f2 As Boolean)

        With Me._Frm.FunctionKey
            'ファンクションキーの制御
            .F2ButtonEnabled = f2       '(F2) 編集

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
            .cmbJikkou.TabIndex = LMB020C.CtlTabIndex.JIKKOU
            .btnJikkou.TabIndex = LMB020C.CtlTabIndex.BTN_JIKKOU
            .cmbPrint.TabIndex = LMB020C.CtlTabIndex.PRINT
            .cmbLabelTYpe.TabIndex = LMB020C.CtlTabIndex.LABEL_TYPE         'ADD 2017/08/04
            .btnPrint.TabIndex = LMB020C.CtlTabIndex.BTN_PRINT
            .pnlNyukaL.TabIndex = LMB020C.CtlTabIndex.NYUKAL
            .lblKanriNoL.TabIndex = LMB020C.CtlTabIndex.KANRINOL
            .cmbEigyo.TabIndex = LMB020C.CtlTabIndex.EIGYO
            .cmbSoko.TabIndex = LMB020C.CtlTabIndex.SOKO
            .cmbNyukaType.TabIndex = LMB020C.CtlTabIndex.NYUKATYPE
            .cmbWhWkStatus.TabIndex = LMB020C.CtlTabIndex.WH_WK_STATUS
            .cmbWHSagyoSijiStatus.TabIndex = LMB020C.CtlTabIndex.WH_TAB_SIJI_STATUS
            .cmbWHSagyoStatus.TabIndex = LMB020C.CtlTabIndex.WH_TAB_SAGYO_STATUS
            .lblNyukaKbn.TabIndex = LMB020C.CtlTabIndex.NYUKAKBN
            .lblShinshokuKbn.TabIndex = LMB020C.CtlTabIndex.SHINSHOKUKBN
            .imdNyukaDate.TabIndex = LMB020C.CtlTabIndex.NYUKADATE
#If True Then 'インターコンチ　総保入期限切れ防止対応
            .imdStorageDueDate.TabIndex = LMB020C.CtlTabIndex.STORAGE_DUE_DATE     'ADD 2017/07/10
#End If
            .txtHuriKanriNo.TabIndex = LMB020C.CtlTabIndex.HURIKANRINO
            .txtBuyerOrdNo.TabIndex = LMB020C.CtlTabIndex.BUYERORDNO
            .txtOrderNo.TabIndex = LMB020C.CtlTabIndex.ORDERNO
            .numFreeKikan.TabIndex = LMB020C.CtlTabIndex.FREEKIKAN
            .imdHokanStrDate.TabIndex = LMB020C.CtlTabIndex.HOKANSTRDATE
            .chkNoSiji.TabIndex = LMB020C.CtlTabIndex.WH_TAB_NO_SIJI
            .txtCustCdL.TabIndex = LMB020C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMB020C.CtlTabIndex.CUSTCDM
            .lblCustNm.TabIndex = LMB020C.CtlTabIndex.CUSTNM
            .cmbKazeiKbn.TabIndex = LMB020C.CtlTabIndex.KAZEIKBN
            .cmbToukiHokanUmu.TabIndex = LMB020C.CtlTabIndex.TOUKIHOKANUMU
            .cmbZenkiHokanUmu.TabIndex = LMB020C.CtlTabIndex.ZENKIHOKANUMU
            .cmbNiyakuUmu.TabIndex = LMB020C.CtlTabIndex.NIYAKUUMU
            .chkStopAlloc.TabIndex = LMB020C.CtlTabIndex.CHKSTOPALLOC   'ADD 2019/08/01 要望管理005237
            .numPlanQT.TabIndex = LMB020C.CtlTabIndex.PLANQT
            .cmbPlanQtUt.TabIndex = LMB020C.CtlTabIndex.PLANQTUT
            .numNyukaCnt.TabIndex = LMB020C.CtlTabIndex.NYUKACNT
            .txtNyubanL.TabIndex = LMB020C.CtlTabIndex.NYUBANL
            .txtNyukaComment.TabIndex = LMB020C.CtlTabIndex.NYUKACOMMENT
            .tabMiddle.TabIndex = LMB020C.CtlTabIndex.MIDDLE
            .tabGoods.TabIndex = LMB020C.CtlTabIndex.GOODS
            .btnRowAddM.TabIndex = LMB020C.CtlTabIndex.ROWADDM
            .btnRowDelM.TabIndex = LMB020C.CtlTabIndex.ROWDELM
            .pnlList.TabIndex = LMB020C.CtlTabIndex.LIST
            .txtSerchGoodsCd.TabIndex = LMB020C.CtlTabIndex.SERCHGOODSCD
            .txtSerchGoodsNm.TabIndex = LMB020C.CtlTabIndex.SERCHGOODSNM
            .pnlHenko.TabIndex = LMB020C.CtlTabIndex.PNL_HENKO
            .numHenkoInjun.TabIndex = LMB020C.CtlTabIndex.HENKO_INJUN
            .btnHenko.TabIndex = LMB020C.CtlTabIndex.BTN_HENKO
            .txtTouNo.TabIndex = LMB020C.CtlTabIndex.TOUNO
            .txtSituNo.TabIndex = LMB020C.CtlTabIndex.SITUNO
            .txtZoneCd.TabIndex = LMB020C.CtlTabIndex.ZONECD
            .txtLocation.TabIndex = LMB020C.CtlTabIndex.LOCATION
            .btnAllChange.TabIndex = LMB020C.CtlTabIndex.BTN_ALLCHANGE
            .sprGoodsDef.TabIndex = LMB020C.CtlTabIndex.GOODSDEF
            .lblKanriNoM.TabIndex = LMB020C.CtlTabIndex.KANRINOM
            .lblGoodsCd.TabIndex = LMB020C.CtlTabIndex.GOODSCD
            .lblGoodsNm.TabIndex = LMB020C.CtlTabIndex.GOODSNM
            .numSort.TabIndex = LMB020C.CtlTabIndex.SORT
            .lblHikiate.TabIndex = LMB020C.CtlTabIndex.HIKIATE
            .cmbOndo.TabIndex = LMB020C.CtlTabIndex.ONDO
            .numSumCnt.TabIndex = LMB020C.CtlTabIndex.SUMCNT
            .numSuryo.TabIndex = LMB020C.CtlTabIndex.SURYO
            .numIrisu.TabIndex = LMB020C.CtlTabIndex.IRISU
            .numStdIrime.TabIndex = LMB020C.CtlTabIndex.STDIRIME
            .numTare.TabIndex = LMB020C.CtlTabIndex.TARE
            .numEdiKosu.TabIndex = LMB020C.CtlTabIndex.EDIKOSU
            .numEdiSuryo.TabIndex = LMB020C.CtlTabIndex.EDISURYO
            .txtOrderNoM.TabIndex = LMB020C.CtlTabIndex.ORDERNOM
            .txtBuyerOrdNoM.TabIndex = LMB020C.CtlTabIndex.BUYERORDNOM
            .txtGoodsComment.TabIndex = LMB020C.CtlTabIndex.GOODSCOMMENT
            .txtSagyoCdM1.TabIndex = LMB020C.CtlTabIndex.SAGYOCDM1
            .lblSagyoNmM1.TabIndex = LMB020C.CtlTabIndex.SAGYONMM1
            .lblSagyoFlgM1.TabIndex = LMB020C.CtlTabIndex.SAGYOFLGM1
            .txtSagyoRemarkM1.TabIndex = LMB020C.CtlTabIndex.SAGYORMKM1
            .txtSagyoCdM2.TabIndex = LMB020C.CtlTabIndex.SAGYOCDM2
            .lblSagyoNmM2.TabIndex = LMB020C.CtlTabIndex.SAGYONMM2
            .lblSagyoFlgM2.TabIndex = LMB020C.CtlTabIndex.SAGYOFLGM2
            .txtSagyoRemarkM2.TabIndex = LMB020C.CtlTabIndex.SAGYORMKM2
            .txtSagyoCdM3.TabIndex = LMB020C.CtlTabIndex.SAGYOCDM3
            .lblSagyoNmM3.TabIndex = LMB020C.CtlTabIndex.SAGYONMM3
            .lblSagyoFlgM3.TabIndex = LMB020C.CtlTabIndex.SAGYOFLGM3
            .txtSagyoRemarkM3.TabIndex = LMB020C.CtlTabIndex.SAGYORMKM3
            .txtSagyoCdM4.TabIndex = LMB020C.CtlTabIndex.SAGYOCDM4
            .lblSagyoNmM4.TabIndex = LMB020C.CtlTabIndex.SAGYONMM4
            .lblSagyoFlgM4.TabIndex = LMB020C.CtlTabIndex.SAGYOFLGM4
            .txtSagyoRemarkM4.TabIndex = LMB020C.CtlTabIndex.SAGYORMKM4
            .txtSagyoCdM5.TabIndex = LMB020C.CtlTabIndex.SAGYOCDM5
            .lblSagyoNmM5.TabIndex = LMB020C.CtlTabIndex.SAGYONMM5
            .lblSagyoFlgM5.TabIndex = LMB020C.CtlTabIndex.SAGYOFLGM5
            .txtSagyoRemarkM5.TabIndex = LMB020C.CtlTabIndex.SAGYORMKM5
            .tabUnso.TabIndex = LMB020C.CtlTabIndex.TAB_UNSO
            .txtUnsoNo.TabIndex = LMB020C.CtlTabIndex.UNSONO
            .cmbUnchinUmu.TabIndex = LMB020C.CtlTabIndex.UNCHINUMU
            .cmbUnchinKbn.TabIndex = LMB020C.CtlTabIndex.UNCHINKBN
            .cmbSharyoKbn.TabIndex = LMB020C.CtlTabIndex.SHARYOKBN
            .cmbTrnThermoKbn.TabIndex = LMB020C.CtlTabIndex.TRNTHERMOKBN
            .txtUnsoCd.TabIndex = LMB020C.CtlTabIndex.UNSOCD
            .txtTrnBrCD.TabIndex = LMB020C.CtlTabIndex.TRNBRCD
            .lblTrnNM.TabIndex = LMB020C.CtlTabIndex.TRNNM
            .numUnchin.TabIndex = LMB020C.CtlTabIndex.UNCHIN
            .txtUnsoTariffCD.TabIndex = LMB020C.CtlTabIndex.UNSOTARIFFCD
            .lblUnsoTariffNM.TabIndex = LMB020C.CtlTabIndex.UNSOTARIFFNM
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            .txtShiharaiTariffCD.TabIndex = LMB020C.CtlTabIndex.SHIHARAITARIFFCD
            .lblShiharaiTariffNM.TabIndex = LMB020C.CtlTabIndex.SHIHARAITARIFFNM
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
            .numUnsoJuryo.TabIndex = LMB020C.CtlTabIndex.UNSOJURYO
            .txtShukkaMotoCD.TabIndex = LMB020C.CtlTabIndex.SHUKKAMOTOCD
            .lblShukkaMotoNM.TabIndex = LMB020C.CtlTabIndex.SHUKKAMOTONM
            .numKyori.TabIndex = LMB020C.CtlTabIndex.KYORI
            .txtUnchinComment.TabIndex = LMB020C.CtlTabIndex.UNCHINCOMMENT
            .cmbTax.TabIndex = LMB020C.CtlTabIndex.UNSOKAZEIKBN
            .cmbYusoBrCd.TabIndex = LMB020C.CtlTabIndex.YUSONRS
            '.pnlSagyoL.TabIndex = LMB020C.CtlTabIndex.SAGYOL
            .txtSagyoCdL1.TabIndex = LMB020C.CtlTabIndex.SAGYOCD_L1
            .lblSagyoNmL1.TabIndex = LMB020C.CtlTabIndex.SAGYONM_L1
            .lblSagyoFlgL1.TabIndex = LMB020C.CtlTabIndex.SAGYOFLG_L1
            .txtSagyoRemarkL1.TabIndex = LMB020C.CtlTabIndex.SAGYORMK_L1
            .txtSagyoCdL2.TabIndex = LMB020C.CtlTabIndex.SAGYOCD_L2
            .lblSagyoNmL2.TabIndex = LMB020C.CtlTabIndex.SAGYONM_L2
            .lblSagyoFlgL2.TabIndex = LMB020C.CtlTabIndex.SAGYOFLG_L2
            .txtSagyoRemarkL2.TabIndex = LMB020C.CtlTabIndex.SAGYORMK_L2
            .txtSagyoCdL3.TabIndex = LMB020C.CtlTabIndex.SAGYOCD_L3
            .lblSagyoNmL3.TabIndex = LMB020C.CtlTabIndex.SAGYONM_L3
            .lblSagyoFlgL3.TabIndex = LMB020C.CtlTabIndex.SAGYOFLG_L3
            .txtSagyoRemarkL3.TabIndex = LMB020C.CtlTabIndex.SAGYORMK_L3
            .txtSagyoCdL4.TabIndex = LMB020C.CtlTabIndex.SAGYOCD_L4
            .lblSagyoNmL4.TabIndex = LMB020C.CtlTabIndex.SAGYONM_L4
            .lblSagyoFlgL4.TabIndex = LMB020C.CtlTabIndex.SAGYOFLG_L4
            .txtSagyoRemarkL4.TabIndex = LMB020C.CtlTabIndex.SAGYORMK_L4
            .txtSagyoCdL5.TabIndex = LMB020C.CtlTabIndex.SAGYOCD_L5
            .lblSagyoNmL5.TabIndex = LMB020C.CtlTabIndex.SAGYONM_L5
            .lblSagyoFlgL5.TabIndex = LMB020C.CtlTabIndex.SAGYOFLG_L5
            .txtSagyoRemarkL5.TabIndex = LMB020C.CtlTabIndex.SAGYORMK_L5
            .btnRowAddS.TabIndex = LMB020C.CtlTabIndex.ROWADDS
            .btnRowDelS.TabIndex = LMB020C.CtlTabIndex.ROWDELS
            .btnRowCopyS.TabIndex = LMB020C.CtlTabIndex.ROWCOPYS
            'START YANAI 要望番号557
            .numRowCopyScnt.TabIndex = LMB020C.CtlTabIndex.ROWCOPYSCNT
            'END YANAI 要望番号557
            .numEntryCnt.TabIndex = LMB020C.CtlTabIndex.ENTRYCNT
            .btnPhotoSel.TabIndex = LMB020C.CtlTabIndex.BTN_PHOTOSEL    'ADD 2022/11/07 倉庫写真アプリ対応
            .btnImgAdd.TabIndex = LMB020C.CtlTabIndex.BTN_IMGADD
            .btnCoaAdd.TabIndex = LMB020C.CtlTabIndex.BTN_COAADD
            .btnYCardAdd.TabIndex = LMB020C.CtlTabIndex.BTN_YCARDADD
            .sprDetail.TabIndex = LMB020C.CtlTabIndex.DETAIL
            .btnWHSagyoTorikomi.TabIndex = LMB020C.CtlTabIndex.BTN_TAB_TORIKOMI

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

#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
        '隠し項目を非表示
        _Frm.txtShinshokuKbnKbn.Visible = False
#End If

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal actionType As LMB020C.ActionType, ByVal ds As DataSet)

        With Me._Frm

            '特殊モードの場合、スルー
            If String.IsNullOrEmpty(.lblEdit.TextValue) = False Then
                Exit Sub
            End If

            Dim view As Boolean = True
            Dim edit As Boolean = True
            Dim inkaM As Boolean = True
            Dim inkaMBtn As Boolean = True
            Dim dateEdit As Boolean = True
            Dim unsoEdit As Boolean = True
            Dim unsoEdit1 As Boolean = True
            Dim unsoEdit2 As Boolean = True
            Dim soko As Boolean = True
            Dim lock As Boolean = True
            'START YANAI 運送・運行・請求メモNo.44
            Dim edit2 As Boolean = True
            'END YANAI 運送・運行・請求メモNo.44
            Dim hotoSel As Boolean = True   'ADD 2022/11/07 倉庫写真アプリ対応

            'ADD 2017/07/11 総保入期限処理判定
#If True Then 'インターコンチ　総保入期限切れ防止対応
            Dim edit3 As Boolean = StorageDueDateControl(Me._Frm, ds)
#Else
            Dim edit3 As Boolean = True     '処理対象外
#End If
            Dim edit4 As Boolean = True

            'モード切替
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then

                '参照の場合、活性化
                view = False
                edit3 = True   'ADD 2017/07/11
                hotoSel = False 'ADD 2022/11/07 倉庫写真アプリ対応
            Else

                '運行紐付けしているかを判定
                unsoEdit = Me.LockTripControl(ds)

                Select Case actionType

                    Case LMB020C.ActionType.DATEEDIT

                        '起算日修正の場合
                        dateEdit = False

                    Case LMB020C.ActionType.UNSOEDIT

                        '運送修正の場合
                        unsoEdit1 = False

                        '運行紐付けされていない場合、ロック解除
                        If unsoEdit = True Then
                            unsoEdit2 = False
                        End If

                    Case Else

                        '新規の場合
                        If RecordStatus.NEW_REC.Equals(.lblSituation.RecordStatus) = True Then

                            soko = False

                        End If

                        '引当済みレコードがある場合、入荷(大)はロック
                        Dim inkaMDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
                        Dim sql As String = String.Concat("SYS_DEL_FLG = '0' AND HIKIATE = '", LMB020C.HIKIATE_ARI, "' ")

                        If inkaMDt.Select(sql).Length < 1 Then

                            '編集の場合、活性化
                            edit = False
                            unsoEdit1 = False

                            '運行紐付けされていない場合、ロック解除
                            If unsoEdit = True Then
                                unsoEdit2 = False
                            End If

                        End If

                        '入荷(中)番号がある 且つ 引当されていない場合、ロック解除
                        Dim inkaNoM As String = .lblKanriNoM.TextValue

                        If String.IsNullOrEmpty(inkaNoM) = False _
                            AndAlso inkaMDt.Select(String.Concat(sql, " AND INKA_NO_M = '", inkaNoM, "' ")).Length < 1 Then
                            inkaM = False
                        End If

                        '行追加、行削除(中)のロック解除
                        inkaMBtn = False

                        'START YANAI 運送・運行・請求メモNo.44
                        '編集の場合、常に活性化
                        edit2 = False
                        dateEdit = False
                        'END YANAI 運送・運行・請求メモNo.44

                        '編集で進捗区分が入荷済より前であれば活性化
                        If .lblShinshokuKbn.KbnValue < LMB020C.STATE_NYUKOZUMI Then
                            edit4 = False
                        End If

                        hotoSel = False 'ADD 2022/11/07 倉庫写真アプリ対応

                End Select

            End If

            'クリアフラグ
            Dim clearFlg As Boolean = False

            '参照モードで活性化
            Call Me._LMBconG.SetLockInputMan(.cmbJikkou, view, clearFlg)
            Call Me._LMBconG.SetLockControl(.btnJikkou, view)

            '常に活性化
            Call Me._LMBconG.SetLockInputMan(.cmbLabelTYpe, view, clearFlg) 'ADD 2017/08/04 GHSラベル対応
            Call Me._LMBconG.SetLockInputMan(.cmbPrint, view, clearFlg)
            Call Me._LMBconG.SetLockControl(.btnPrint, view)

            '常にロック
            Call Me._LMBconG.SetLockInputMan(.txtCustCdL, lock, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtCustCdM, lock, clearFlg)

            '編集(紐付けなし)
            Call Me._LMBconG.SetLockInputMan(.cmbNyukaType, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.imdNyukaDate, edit, clearFlg)
#If True Then 'インターコンチ　総保入期限切れ防止対応
            Call Me._LMBconG.SetLockInputMan(.imdStorageDueDate, edit3, clearFlg)     'ADD 2017/07/10
#End If
            Call Me._LMBconG.SetLockInputMan(.txtBuyerOrdNo, edit, clearFlg)
#If False Then  'UPD 2019/01/31 依頼番号 : 002334   【LMS】ダウケミカル_入荷/出荷のオーダー番号を出荷後に入れたい
            Call Me._LMBconG.SetLockInputMan(.txtOrderNo, edit, clearFlg)
#Else
            Call Me._LMBconG.SetLockInputMan(.txtOrderNo, edit2, clearFlg)
#End If
            'START YANAI 運送・運行・請求メモNo.44
            'Call Me._LMBconG.SetLockInputMan(.numFreeKikan, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.numFreeKikan, edit2, clearFlg)
            'END YANAI 運送・運行・請求メモNo.44
            Call Me._LMBconG.SetLockInputMan(.cmbZenkiHokanUmu, edit, clearFlg)
            'START YANAI 運送・運行・請求メモNo.44
            'Call Me._LMBconG.SetLockInputMan(.cmbKazeiKbn, edit, clearFlg)
            'Call Me._LMBconG.SetLockInputMan(.cmbToukiHokanUmu, edit, clearFlg)
            'Call Me._LMBconG.SetLockInputMan(.cmbZenkiHokanUmu, edit, clearFlg)
            'Call Me._LMBconG.SetLockInputMan(.cmbNiyakuUmu, edit, clearFlg)
            '要望管理2271 SHINODA START
            'Call Me._LMBconG.SetLockInputMan(.cmbKazeiKbn, edit2, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbKazeiKbn, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbToukiHokanUmu, edit2, clearFlg)
            'Call Me._LMBconG.SetLockInputMan(.cmbZenkiHokanUmu, edit2, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbZenkiHokanUmu, edit, clearFlg)
            '要望管理2271 SHINODA END
            Call Me._LMBconG.SetLockInputMan(.cmbNiyakuUmu, edit2, clearFlg)
            'END YANAI 運送・運行・請求メモNo.44
            Call Me._LMBconG.SetLockControl(.chkStopAlloc, edit2)    'ADD 2019/08/01 要望管理005237
            Call Me._LMBconG.SetLockInputMan(.numPlanQT, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbPlanQtUt, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.numNyukaCnt, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtNyubanL, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtNyukaComment, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSerchGoodsCd, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSerchGoodsNm, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdL1, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdL2, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdL3, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdL4, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdL5, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkL1, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkL2, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkL3, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkL4, edit, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkL5, edit, clearFlg)
            Call Me._LMBconG.SetLockControl(.btnHenko, edit)
            Call Me._LMBconG.SetLockControl(.btnAllChange, edit)

            Call Me._LMBconG.SetLockInputMan(.cmbWhWkStatus, edit, clearFlg)


            Call Me._LMBconG.SetLockInputMan(.cmbWHSagyoSijiStatus, lock, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbWHSagyoStatus, lock, clearFlg)
            If LMB020C.WH_TAB_SIJI_00.Equals(.cmbWHSagyoSijiStatus.SelectedValue) Then
                Call Me._LMBconG.SetLockControl(.chkTablet, edit2)
            Else
                Call Me._LMBconG.SetLockControl(.chkTablet, lock)
            End If

            Call Me._LMBconG.SetLockControl(.btnWHSagyoTorikomi, edit4)
            Call Me._LMBconG.SetLockControl(.btnImgAdd, view)
            Call Me._LMBconG.SetLockControl(.chkNoSiji, edit2)
            Call Me._LMBconG.SetLockControl(.btnPhotoSel, hotoSel)  'ADD 2022/11/07 倉庫写真アプリ対応

            '編集
            Call Me._LMBconG.SetLockControl(.btnRowDelM, inkaMBtn)
            Call Me._LMBconG.SetLockControl(.btnRowAddM, inkaMBtn)

            '新規の場合のみ活性化
            Call Me._LMBconG.SetLockInputMan(.cmbSoko, soko, clearFlg)

            '入荷(中)のロック制御
            Call Me._LMBconG.SetLockInputMan(.numSort, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtOrderNoM, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtBuyerOrdNoM, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtGoodsComment, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdM1, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdM2, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdM3, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdM4, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoCdM5, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkM1, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkM2, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkM3, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkM4, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSagyoRemarkM5, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.numHenkoInjun, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtTouNo, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtSituNo, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtZoneCd, inkaM, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtLocation, inkaM, clearFlg)
            Call Me._LMBconG.SetLockControl(.btnRowDelS, inkaM)
            Call Me._LMBconG.SetLockControl(.btnRowAddS, inkaM)
            Call Me._LMBconG.SetLockControl(.btnRowCopyS, inkaM)

            'START YANAI 要望番号557
            '入荷(小)のロック制御
            Call Me._LMBconG.SetLockInputMan(.numRowCopyScnt, edit, clearFlg)
            'END YANAI 要望番号557

            '起算日
            Call Me._LMBconG.SetLockInputMan(.imdHokanStrDate, dateEdit, clearFlg)

            '運送項目
            Call Me._LMBconG.SetLockInputMan(.cmbUnchinUmu, unsoEdit2, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbYusoBrCd, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbUnchinKbn, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbSharyoKbn, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbTrnThermoKbn, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtUnsoCd, unsoEdit2, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtTrnBrCD, unsoEdit2, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.numUnchin, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtUnsoTariffCD, unsoEdit1, clearFlg)
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
            'Call Me._LMBconG.SetLockInputMan(.txtShiharaiTariffCD, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtShiharaiTariffCD, unsoEdit2, clearFlg)
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
            Call Me._LMBconG.SetLockInputMan(.txtShukkaMotoCD, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.cmbTax, unsoEdit1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtUnchinComment, unsoEdit1, clearFlg)

            '値によるロック制御
            Call Me.SetLockControl(actionType, unsoEdit)

        End With

    End Sub

    ''' <summary>
    ''' 運行紐付け判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>紐付いている場合、False　その他、True</returns>
    ''' <remarks></remarks>
    Friend Function LockTripControl(ByVal ds As DataSet) As Boolean

        '運送がない場合、True
        If ds Is Nothing = True Then
            Return True
        End If

        Dim dt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_L)
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '運行紐付いている場合、False
        Dim dr As DataRow = dt.Rows(0)
        If String.IsNullOrEmpty(dr.Item("TRIP_NO").ToString()) = False _
            OrElse String.IsNullOrEmpty(dr.Item("TRIP_NO_SYUKA").ToString()) = False _
            OrElse String.IsNullOrEmpty(dr.Item("TRIP_NO_TYUKEI").ToString()) = False _
            OrElse String.IsNullOrEmpty(dr.Item("TRIP_NO_HAIKA").ToString()) = False _
            Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateFormat()

        With Me._Frm

            Me._LMBconG.SetDateFormat(.imdNyukaDate)

#If True Then 'インターコンチ　総保入期限切れ防止対応

            Me._LMBconG.SetDateFormat(.imdStorageDueDate)     'ADD 2017/07/10
#Else
            .imdStorageDueDate.Visible = False
            .lblTitleStorageDueDate.Visible = False
#End If

            Me._LMBconG.SetDateFormat(.imdHokanStrDate)

            Me._LMBconG.SetDateFormat2(.lblOndoFrom)
            Me._LMBconG.SetDateFormat2(.lblOndoTo)

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d2 As Decimal = Convert.ToDecimal(LMB020C.SORT_MAX)
            Dim sharp2 As String = "#0"
            Dim d3 As Decimal = Convert.ToDecimal(LMB020C.FREE_MAX)
            Dim sharp3 As String = "##0"
            Dim d5 As Decimal = Convert.ToDecimal(LMB020C.KYORI_MAX)
            Dim sharp5 As String = "##,##0"
            Dim d8 As Decimal = Convert.ToDecimal(LMB020C.IRISU_MAX)
            Dim sharp8 As String = "##,###,##0"
            Dim d9 As Decimal = Convert.ToDecimal(LMB020C.UNCHIN_MAX)
            Dim sharp9 As String = "###,###,##0"
            Dim d9_3 As Decimal = Convert.ToDecimal(LMB020C.NB_MAX)
            Dim sharp9_3 As String = "###,###,##0.000"
            Dim d10 As Decimal = Convert.ToDecimal(LMB020C.NB_MAX)
            Dim sharp10 As String = "#,###,###,##0"
            Dim d10_2 As Decimal = Convert.ToDecimal(LMB020C.UNCHIN_MAX)
            Dim sharp10_2 As String = "#,###,###,##0.00"
            Dim d12_3 As Decimal = Convert.ToDecimal(LMB020C.SURYO_MAX)
            Dim sharp12_3 As String = "###,###,###,##0.000"

            .numFreeKikan.SetInputFields(sharp3, , 3, 1, , 0, 0, , d3, 0)
            .numPlanQT.SetInputFields(sharp9_3, , 9, 1, , 3, 3, , d9_3, 0)
            .numNyukaCnt.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numSort.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            .numSumCnt.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numSuryo.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numIrisu.SetInputFields(sharp8, , 8, 1, , 0, 0, , d8, 0)
            .numStdIrime.SetInputFields(sharp9_3, , 9, 1, , 3, 3, , d9_3, 0)
            .numTare.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numEdiKosu.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numEdiSuryo.SetInputFields(sharp12_3, , 12, 1, , 3, 3, , d12_3, 0)
            .numUnchin.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numUnsoJuryo.SetInputFields(sharp9, , 9, 1, , 0, 0, , d9, 0)
            .numKyori.SetInputFields(sharp10, , 5, 1, , 0, 0, , d5, 0)
            .numEntryCnt.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            .numHenkoInjun.SetInputFields(sharp2, , 2, 1, , 0, 0, , d2, 0)
            'START YANAI 要望番号557
            .numRowCopyScnt.SetInputFields(sharp3, , 3, 1, , 0, 0, , d3, 0)
            'END YANAI 要望番号557


        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal actionType As LMB020C.ActionType)

        With Me._Frm

            'フォーカス位置の初期化
            .Focus()

            '参照モードの場合
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then

                .cmbPrint.Focus()

                Exit Sub

            End If

            '編集モードの場合
            Select Case actionType

                Case LMB020C.ActionType.DATEEDIT

                    '起算日にフォーカス
                    .imdHokanStrDate.Focus()

                Case LMB020C.ActionType.UNSOEDIT

                    'タブの設定
                    .tabMiddle.SelectedTab = .tabUnso

                    '運送有無にフォーカス
                    .cmbUnchinUmu.Focus()

                Case Else

                    '倉庫コンボにフォーカス
                    .cmbSoko.Focus()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .cmbJikkou.SelectedValue = Nothing
            .lblEdit.TextValue = String.Empty
            .cmbLabelTYpe.SelectedValue = Nothing       'ADD 2017/08/04 GHSラベル対応
            .cmbPrint.SelectedValue = Nothing
            .lblKanriNoL.TextValue = String.Empty
            .cmbEigyo.SelectedValue = Nothing
            .cmbSoko.SelectedValue = Nothing
            .cmbNyukaType.SelectedValue = Nothing
#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .txtNyukaKbn.TextValue = String.Empty
            .txtShinshokuKbn.TextValue = String.Empty
            .txtShinshokuKbnKbn.TextValue = String.Empty
#Else
            .lblNyukaKbn.KbnValue = ""
            .lblShinshokuKbn.KbnValue = ""
#End If

            .imdNyukaDate.TextValue = String.Empty
            .imdStorageDueDate.TextValue = String.Empty   'ADD 2017/07/10
            .txtHuriKanriNo.TextValue = String.Empty
            .txtBuyerOrdNo.TextValue = String.Empty
            .txtOrderNo.TextValue = String.Empty
            .numFreeKikan.Value = 0
            .cmbKazeiKbn.SelectedValue = Nothing
            .cmbToukiHokanUmu.SelectedValue = Nothing
            .cmbZenkiHokanUmu.SelectedValue = Nothing
            .cmbNiyakuUmu.SelectedValue = Nothing
            .chkStopAlloc.Checked = False   'ADD 2019/08/01 要望管理005237
            .numPlanQT.Value = 0
            .cmbPlanQtUt.SelectedValue = Nothing
            .numNyukaCnt.Value = 0
            .txtNyubanL.TextValue = String.Empty
            .txtNyukaComment.TextValue = String.Empty
            .txtSerchGoodsCd.TextValue = String.Empty
            .txtSerchGoodsNm.TextValue = String.Empty
            .numHenkoInjun.Value = 0
            .txtTouNo.TextValue = String.Empty
            .txtSituNo.TextValue = String.Empty
            .txtZoneCd.TextValue = String.Empty
            .txtLocation.TextValue = String.Empty

            '入荷(中)の詳細情報は別メソッド
            Call Me.ClearInkaMControl()

            .txtUnsoNo.TextValue = String.Empty
            .cmbUnchinUmu.SelectedValue = Nothing
            .cmbUnchinKbn.SelectedValue = Nothing
            .cmbSharyoKbn.SelectedValue = Nothing
            .cmbTrnThermoKbn.SelectedValue = Nothing
            .txtUnsoCd.TextValue = String.Empty
            .txtTrnBrCD.TextValue = String.Empty
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsoCdOld.TextValue = String.Empty
            .txtTrnBrCDOld.TextValue = String.Empty
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .lblTrnNM.TextValue = String.Empty
            .lblUnsoNm.TextValue = String.Empty
            .lblUnsoBrNm.TextValue = String.Empty
            .lblTareYn.TextValue = String.Empty
            .numUnchin.Value = 0
            .txtUnsoTariffCD.TextValue = String.Empty
            .lblUnsoTariffNM.TextValue = String.Empty
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            .txtShiharaiTariffCD.TextValue = String.Empty
            .lblShiharaiTariffNM.TextValue = String.Empty
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End
            .numUnsoJuryo.Value = 0
            .txtShukkaMotoCD.TextValue = String.Empty
            .lblShukkaMotoNM.TextValue = String.Empty
            .numKyori.Value = 0
            .txtUnchinComment.TextValue = String.Empty
            .lblRecNoL1.TextValue = String.Empty
            .txtSagyoCdL1.TextValue = String.Empty
            .lblSagyoNmL1.TextValue = String.Empty
            .lblSagyoFlgL1.TextValue = String.Empty
            .lblAddFlgL1.TextValue = String.Empty
            .txtSagyoRemarkL1.TextValue = String.Empty
            .lblRecNoL2.TextValue = String.Empty
            .txtSagyoCdL2.TextValue = String.Empty
            .lblSagyoNmL2.TextValue = String.Empty
            .lblSagyoFlgL2.TextValue = String.Empty
            .lblAddFlgL2.TextValue = String.Empty
            .txtSagyoRemarkL2.TextValue = String.Empty
            .lblRecNoL3.TextValue = String.Empty
            .txtSagyoCdL3.TextValue = String.Empty
            .lblSagyoNmL3.TextValue = String.Empty
            .lblSagyoFlgL3.TextValue = String.Empty
            .lblAddFlgL3.TextValue = String.Empty
            .txtSagyoRemarkL3.TextValue = String.Empty
            .lblRecNoL4.TextValue = String.Empty
            .txtSagyoCdL4.TextValue = String.Empty
            .lblSagyoNmL4.TextValue = String.Empty
            .lblSagyoFlgL4.TextValue = String.Empty
            .lblAddFlgL4.TextValue = String.Empty
            .txtSagyoRemarkL4.TextValue = String.Empty
            .lblRecNoL5.TextValue = String.Empty
            .txtSagyoCdL5.TextValue = String.Empty
            .lblSagyoNmL5.TextValue = String.Empty
            .lblSagyoFlgL5.TextValue = String.Empty
            .lblAddFlgL5.TextValue = String.Empty
            .txtSagyoRemarkL5.TextValue = String.Empty
            .numEntryCnt.Value = 0
            .cmbTax.SelectedValue = Nothing
            .cmbYusoBrCd.SelectedValue = Nothing
            'START YANAI 要望番号557
            .numRowCopyScnt.Value = 1
            'END YANAI 要望番号557

            .cmbWhWkStatus.SelectedValue = Nothing
            .txtWhWorkImpFlg.TextValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' 入荷(中)詳細情報クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearInkaMControl()

        With Me._Frm

            .lblKanriNoM.TextValue = String.Empty
            .lblGoodsCd.TextValue = String.Empty
            .lblGoodsNm.TextValue = String.Empty
            .lblGoodsCdNrs.TextValue = String.Empty
            .numSort.Value = 0
            .lblHikiate.TextValue = String.Empty
            .cmbOndo.SelectedValue = Nothing
            'START YANAI No.73
            '.lblOndoFrom.Text = String.Empty
            '.lblOndoTo.Text = String.Empty
            .lblOndoFrom.TextValue = String.Empty
            .lblOndoTo.TextValue = String.Empty
            'END YANAI No.73
            .numSumCnt.Value = 0
            .lblSumCntTani.Text = String.Empty
            .numSuryo.Value = 0
            .lblSumAntTani.Text = String.Empty
            .numIrisu.Value = 0
            .lblIrisuTani1.Text = String.Empty
            .lblIrisuTani2.Text = String.Empty
            .numStdIrime.Value = 0
            .lblStdIrimeTani.Text = String.Empty
            .numTare.Value = 0
            .numEdiKosu.Value = 0
            .lblEdiKosuTani.Text = String.Empty
            .numEdiSuryo.Value = 0
            .lblEdiSuryoTani.Text = String.Empty
            .txtOrderNoM.TextValue = String.Empty
            .txtBuyerOrdNoM.TextValue = String.Empty
            .txtGoodsComment.TextValue = String.Empty
            .lblRecNoM1.TextValue = String.Empty
            .txtSagyoCdM1.TextValue = String.Empty
            .lblSagyoNmM1.TextValue = String.Empty
            .lblSagyoFlgM1.TextValue = String.Empty
            .lblAddFlgM1.TextValue = String.Empty
            .txtSagyoRemarkM1.TextValue = String.Empty
            .lblRecNoM2.TextValue = String.Empty
            .txtSagyoCdM2.TextValue = String.Empty
            .lblSagyoNmM2.TextValue = String.Empty
            .lblSagyoFlgM2.TextValue = String.Empty
            .lblAddFlgM2.TextValue = String.Empty
            .txtSagyoRemarkM2.TextValue = String.Empty
            .lblRecNoM3.TextValue = String.Empty
            .txtSagyoCdM3.TextValue = String.Empty
            .lblSagyoNmM3.TextValue = String.Empty
            .lblSagyoFlgM3.TextValue = String.Empty
            .lblAddFlgM3.TextValue = String.Empty
            .txtSagyoRemarkM3.TextValue = String.Empty
            .lblRecNoM4.TextValue = String.Empty
            .txtSagyoCdM4.TextValue = String.Empty
            .lblSagyoNmM4.TextValue = String.Empty
            .lblSagyoFlgM4.TextValue = String.Empty
            .lblAddFlgM4.TextValue = String.Empty
            .txtSagyoRemarkM4.TextValue = String.Empty
            .lblRecNoM5.TextValue = String.Empty
            .txtSagyoCdM5.TextValue = String.Empty
            .lblSagyoNmM5.TextValue = String.Empty
            .lblSagyoFlgM5.TextValue = String.Empty
            .lblAddFlgM5.TextValue = String.Empty
            .txtSagyoRemarkM5.TextValue = String.Empty

            'タイトルを初期化
            'START YANAI No.73
            '.lblTitleKara.Text = String.Empty
            'END YANAI No.73
            .lblTitleS.Text = String.Empty

        End With

    End Sub

    '要望番号:1724 terakawa 2013.01.16 Start
    ''' <summary>
    ''' コントロール値のクリア（運送関連のみ）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearUnsoControl()

        With Me._Frm

            .txtUnsoNo.TextValue = String.Empty
            .cmbUnchinUmu.SelectedValue = Nothing
            .cmbUnchinKbn.SelectedValue = Nothing
            .cmbSharyoKbn.SelectedValue = Nothing
            .cmbTrnThermoKbn.SelectedValue = Nothing
            .txtUnsoCd.TextValue = String.Empty
            .txtTrnBrCD.TextValue = String.Empty
            .txtUnsoCdOld.TextValue = String.Empty
            .txtTrnBrCDOld.TextValue = String.Empty
            .lblTrnNM.TextValue = String.Empty
            .lblUnsoNm.TextValue = String.Empty
            .lblUnsoBrNm.TextValue = String.Empty
            .lblTareYn.TextValue = String.Empty
            .numUnchin.Value = 0
            .txtUnsoTariffCD.TextValue = String.Empty
            .lblUnsoTariffNM.TextValue = String.Empty
            .txtShiharaiTariffCD.TextValue = String.Empty
            .lblShiharaiTariffNM.TextValue = String.Empty
            .numUnsoJuryo.Value = 0
            .txtShukkaMotoCD.TextValue = String.Empty
            .lblShukkaMotoNM.TextValue = String.Empty
            .numKyori.Value = 0
            .txtUnchinComment.TextValue = String.Empty
            .cmbTax.SelectedValue = Nothing
            .cmbYusoBrCd.SelectedValue = Nothing

        End With

    End Sub
    '要望番号:1724 terakawa 2013.01.16 End

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ds As DataSet)

        '入荷(大)の情報を設定
        Call Me.SetInitControl(ds)

        '運送(大)の情報を設定
        Call Me.SetUnsoInforData(ds)

        '作業(大)の情報を設定
        Call Me.SetSagyoLInforData(ds)

        '入荷(中)の情報を設定
        Call Me.SetInkaMData(ds)

    End Sub

    ''' <summary>
    ''' ヘッダ項目に値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal ds As DataSet)

        With Me._Frm

            Dim dr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)

            .lblKanriNoL.TextValue = dr.Item("INKA_NO_L").ToString()
            .cmbEigyo.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .cmbSoko.SelectedValue = dr.Item("WH_CD").ToString()
            .cmbNyukaType.SelectedValue = dr.Item("INKA_TP").ToString()
#If False Then '区分タイトルラベル対応 Changed 20151116 INOUE
            .txtNyukaKbn.TextValue = dr.Item("INKA_KB_NM").ToString()
            .txtShinshokuKbn.TextValue = dr.Item("INKA_STATE_KB_NM").ToString()
            .txtShinshokuKbnKbn.TextValue = dr.Item("INKA_STATE_KB").ToString()
#Else
            .lblNyukaKbn.KbnValue = dr.Item("INKA_KB").ToString()
            .lblShinshokuKbn.KbnValue = dr.Item("INKA_STATE_KB").ToString()
#End If

            .imdNyukaDate.TextValue = dr.Item("INKA_DATE").ToString()
#If True Then 'インターコンチ　総保入期限切れ防止対応
            .imdStorageDueDate.TextValue = dr.Item("STORAGE_DUE_DATE").ToString()    'ADD 2017/07/10
#End If
            .txtHuriKanriNo.TextValue = dr.Item("FURI_NO").ToString()
            .txtBuyerOrdNo.TextValue = dr.Item("BUYER_ORD_NO_L").ToString()
            .txtOrderNo.TextValue = dr.Item("OUTKA_FROM_ORD_NO_L").ToString()
            .numFreeKikan.Value = dr.Item("HOKAN_FREE_KIKAN").ToString()
            .imdHokanStrDate.TextValue = dr.Item("HOKAN_STR_DATE").ToString()
            .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
            .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
            .lblCustNm.TextValue = dr.Item("CUST_NM").ToString()
            .cmbKazeiKbn.SelectedValue = dr.Item("TAX_KB").ToString()
            .cmbToukiHokanUmu.SelectedValue = dr.Item("TOUKI_HOKAN_YN").ToString()
            .cmbZenkiHokanUmu.SelectedValue = dr.Item("HOKAN_YN").ToString()
            .cmbNiyakuUmu.SelectedValue = dr.Item("NIYAKU_YN").ToString()
            .numPlanQT.Value = dr.Item("INKA_PLAN_QT").ToString()
            .cmbPlanQtUt.SelectedValue = dr.Item("INKA_PLAN_QT_UT").ToString()
            .numNyukaCnt.Value = dr.Item("INKA_TTL_NB").ToString()
            .txtNyubanL.TextValue = dr.Item("REMARK_OUT").ToString()
            .txtNyukaComment.TextValue = dr.Item("REMARK").ToString()

            .cmbWhWkStatus.SelectedValue = dr.Item("WH_KENPIN_WK_STATUS").ToString()

            .cmbWHSagyoSijiStatus.SelectedValue = dr.Item("WH_TAB_SAGYO_SIJI_STATUS").ToString()
            .cmbWHSagyoStatus.SelectedValue = dr.Item("WH_TAB_SAGYO_STATUS").ToString()
            If LMB020C.WH_TAB_YN_01.Equals(dr.Item("WH_TAB_YN").ToString()) Then
                .chkTablet.Checked = True
            Else
                .chkTablet.Checked = False
            End If
            'Del 2019/10/09 要望管理007373  .chkStopAlloc.Checked = If(dr.Item("STOP_ALLOC").ToString = LMB020C.StopAllocYN.Yes, True, False)   'ADD 2019/08/01 要望管理005237
            'Add Start 2019/10/09 要望管理007373
            If ds.Tables(LMB020C.TABLE_NM_INKA_S).Select("SPD_KB = '" & LMB020C.SPD_KB.Unpermitted & "' AND SYS_DEL_FLG = '0'").Length > 0 Then
                .chkStopAlloc.Checked = True
            Else
                .chkStopAlloc.Checked = False
            End If
            'Add End   2019/10/09 要望管理007373
            If LMB020C.WH_TAB_NO_SIJI_YN_01.Equals(dr.Item("WH_TAB_NO_SIJI_FLG").ToString()) Then
                .chkNoSiji.Checked = True
            Else
                .chkNoSiji.Checked = False
            End If
            If RecordStatus.COPY_REC.Equals(.lblSituation.RecordStatus) Then
                .chkNoSiji.Checked = False
            End If


        End With

    End Sub

    ''' <summary>
    ''' 入荷(中)の詳細情報表示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="recNo">データテーブルの行番号</param>
    ''' <param name="inkaMNo">入荷中番号</param>
    ''' <remarks></remarks>
    Friend Sub SetInkaMInforData(ByVal ds As DataSet, ByVal recNo As Integer, Optional ByVal inkaMNo As String = "")

        With Me._Frm

            '入荷(中)の情報を設定
            Dim inkaMDr As DataRow = Nothing
            If String.IsNullOrEmpty(inkaMNo) = True Then
                inkaMDr = ds.Tables(LMB020C.TABLE_NM_INKA_M).Rows(recNo)
            Else
                inkaMDr = ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(String.Concat("INKA_NO_M = '", inkaMNo, "' "))(0)
            End If

            .lblKanriNoM.TextValue = inkaMDr.Item("INKA_NO_M").ToString()
            .lblGoodsCd.TextValue = inkaMDr.Item("GOODS_CD_CUST").ToString()
            .lblGoodsNm.TextValue = inkaMDr.Item("GOODS_NM").ToString()
            .lblGoodsCdNrs.TextValue = inkaMDr.Item("GOODS_CD_NRS").ToString()
            .numSort.Value = inkaMDr.Item("PRINT_SORT").ToString()
            .lblHikiate.TextValue = inkaMDr.Item("HIKIATE").ToString()

            Dim onKb As String = inkaMDr.Item("ONDO_KB").ToString()
            .cmbOndo.SelectedValue = onKb
            Dim from As String = String.Empty
            Dim toData As String = String.Empty
            Dim kara As String = String.Empty
            If LMB020C.ONKAN_TEION.Equals(onKb) = True Then
                from = inkaMDr.Item("ONDO_STR_DATE").ToString()
                toData = inkaMDr.Item("ONDO_END_DATE").ToString()
                kara = "～"
            End If

            'START YANAI No.73
            '.lblOndoFrom.Text = from
            '.lblOndoTo.Text = toData
            '.lblTitleKara.Text = kara
            .lblOndoFrom.TextValue = from
            .lblOndoTo.TextValue = toData
            'END YANAI No.73
            .numSumCnt.Value = inkaMDr.Item("SUM_KOSU").ToString()
            .lblSumCntTani.Text = inkaMDr.Item("NB_UT").ToString()
            .numSuryo.Value = inkaMDr.Item("SUM_SURYO_M").ToString()
            .lblSumAntTani.Text = inkaMDr.Item("STD_IRIME_UT").ToString()
            .numIrisu.Value = Me._LMBconG.FormatNumValue(inkaMDr.Item("PKG_NB").ToString())
            Dim pkgNbUt As String = inkaMDr.Item("PKG_NB_UT1").ToString()
            .lblIrisuTani1.Text = pkgNbUt
            .lblIrisuTani2.Text = inkaMDr.Item("PKG_NB_UT2").ToString()
            .lblTitleS.Text = "/"
            .numStdIrime.Value = Me._LMBconG.FormatNumValue(inkaMDr.Item("STD_IRIME_NB_M").ToString())
            .lblStdIrimeTani.Text = inkaMDr.Item("STD_IRIME_UT").ToString()

            .numEdiKosu.Value = inkaMDr.Item("EDI_KOSU").ToString()
            .numEdiSuryo.Value = inkaMDr.Item("EDI_SURYO").ToString()

            .numTare.Value = Me._LMBconG.FormatNumValue(inkaMDr.Item("SUM_JURYO_M").ToString())

            If .txtCustCdL.TextValue = "00135" AndAlso .txtCustCdM.TextValue = "00" Then
                .lblEdiKosuTani.Text = inkaMDr.Item("EDI_NB_UT").ToString()
            Else
                .lblEdiKosuTani.Text = inkaMDr.Item("NB_UT").ToString()
            End If
            .lblEdiSuryoTani.Text = inkaMDr.Item("STD_IRIME_UT").ToString()
            .txtOrderNoM.TextValue = inkaMDr.Item("OUTKA_FROM_ORD_NO_M").ToString()
            .txtBuyerOrdNoM.TextValue = inkaMDr.Item("BUYER_ORD_NO_M").ToString()
            .txtGoodsComment.TextValue = inkaMDr.Item("REMARK").ToString()

            .numEntryCnt.Value = inkaMDr.Item("SUM_KOSU").ToString()

            .lblEntryCntTani.Text = pkgNbUt

            '作業の情報を設定
            Call Me.SetSagyoInforData(ds, inkaMDr, inkaMDr.Item("INKA_NO_M").ToString(), LMB020C.SagyoData.M)

        End With

    End Sub

    ''' <summary>
    ''' 作業(大)の情報を表示
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetSagyoLInforData(ByVal ds As DataSet)

        '作業の情報を設定
        Call Me.SetSagyoInforData(ds, ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0), LMB020C.MAEZERO, LMB020C.SagyoData.L)

    End Sub

    ''' <summary>
    ''' 作業の情報を表示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <param name="type">タイプ</param>
    ''' <remarks></remarks>
    Private Sub SetSagyoInforData(ByVal ds As DataSet, ByVal dr As DataRow, ByVal inkaMNo As String, ByVal type As LMB020C.SagyoData)

        Dim sagyoDr As DataRow() = ds.Tables(LMB020C.TABLE_NM_SAGYO).Select(Me.SetSagyoSql(dr, inkaMNo), "SAGYO_REC_NO")
        Dim max As Integer = sagyoDr.Length - 1

        'データがない場合、スルー
        If max < 0 Then
            Exit Sub
        End If

        With Me._Frm

            '設定するコントロールの文字
            Dim typeStr As String = type.ToString()
            Dim sagyoStr As String = String.Concat(LMB020C.SAGYO_PK, typeStr)
            Dim txtStr As String = String.Concat(LMB020C.SAGYO_CD, typeStr)
            Dim lblStr As String = String.Concat(LMB020C.SAGYO_NM, typeStr)
            Dim flgStr As String = String.Concat(LMB020C.SAGYO_FL, typeStr)
            Dim upFlgStr As String = String.Concat(LMB020C.SAGYO_UP, typeStr)
            Dim rmkSijiStr As String = String.Concat(LMB020C.SAGYO_RMK_SIJI, typeStr)
            Dim recNo As String = String.Empty

            For i As Integer = 0 To max
                recNo = (i + 1).ToString()
                Me.GetTextControl(String.Concat(sagyoStr, recNo)).TextValue = sagyoDr(i).Item("INOUTKA_NO_LM").ToString()
                Me.GetTextControl(String.Concat(txtStr, recNo)).TextValue = sagyoDr(i).Item("SAGYO_CD").ToString()
                Me.GetTextControl(String.Concat(lblStr, recNo)).TextValue = sagyoDr(i).Item("SAGYO_RYAK").ToString()
                Me.GetTextControl(String.Concat(flgStr, recNo)).TextValue = sagyoDr(i).Item("SAGYO_COMP_NM").ToString()
                Me.GetTextControl(String.Concat(upFlgStr, recNo)).TextValue = sagyoDr(i).Item("UP_KBN").ToString()
                Me.GetTextControl(String.Concat(rmkSijiStr, recNo)).TextValue = sagyoDr(i).Item("REMARK_SIJI").ToString()
            Next

        End With

    End Sub

    ''' <summary>
    ''' 運送(大)の情報を表示
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetUnsoInforData(ByVal ds As DataSet)

        With Me._Frm

            'INKA_Lの情報を設定
            Dim inkaLDr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)
            Dim tariff As String = inkaLDr.Item("UNCHIN_TP").ToString()
            Dim tariffKbn As String = inkaLDr.Item("UNCHIN_KB").ToString()
            .cmbUnchinUmu.SelectedValue = tariff
            .cmbUnchinKbn.SelectedValue = tariffKbn

            'データがない場合、スルー
            Dim unsoLDt As DataTable = ds.Tables(LMB020C.TABLE_NM_UNSO_L)
            If unsoLDt.Rows.Count < 1 Then
                Exit Sub
            End If

            '日陸手配でない場合、スルー
            If LMB020C.TEHAI_NRS.Equals(tariff) = False Then
                Exit Sub
            End If

            Dim unsoDr As DataRow = unsoLDt.Rows(0)
            .txtUnsoNo.TextValue = unsoDr.Item("UNSO_NO_L").ToString()
            .cmbSharyoKbn.SelectedValue = unsoDr.Item("VCLE_KB").ToString()
            .cmbTrnThermoKbn.SelectedValue = unsoDr.Item("UNSO_ONDO_KB").ToString()
            .txtUnsoCd.TextValue = unsoDr.Item("UNSO_CD").ToString()
            .txtTrnBrCD.TextValue = unsoDr.Item("UNSO_BR_CD").ToString()
            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            .txtUnsoCdOld.TextValue = unsoDr.Item("UNSO_CD").ToString()
            .txtTrnBrCDOld.TextValue = unsoDr.Item("UNSO_BR_CD").ToString()
            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬
            Dim unsoNm As String = unsoDr.Item("UNSOCO_NM").ToString()
            Dim unsoBrNm As String = unsoDr.Item("UNSOCO_BR_NM").ToString()
            .lblTrnNM.TextValue = String.Concat(unsoNm, unsoBrNm)
            .lblUnsoNm.TextValue = unsoNm
            .lblUnsoBrNm.TextValue = unsoBrNm
            .lblTareYn.TextValue = unsoDr.Item("TARE_YN").ToString()
            .numUnchin.Value = Me._LMBconG.FormatNumValue(unsoDr.Item("SEIQ_UNCHIN").ToString())
            .txtUnsoTariffCD.TextValue = unsoDr.Item("SEIQ_TARIFF_CD").ToString()

            'タリフ区分で名称の設定内容を変更
            Dim remStr As String = unsoDr.Item("UNCHIN_TARIFF_REM").ToString()
            If LMB020C.TARIFF_YOKO.Equals(tariffKbn) = True Then
                remStr = unsoDr.Item("YOKO_REM").ToString()
            End If
            .lblUnsoTariffNM.TextValue = remStr

            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            .txtShiharaiTariffCD.TextValue = unsoDr.Item("SHIHARAI_TARIFF_CD").ToString()
            'タリフ区分で名称の設定内容を変更
            Dim remShiharai As String = unsoDr.Item("SHIHARAI_TARIFF_REM").ToString()
            If LMB020C.TARIFF_YOKO.Equals(tariffKbn) = True Then
                '横持の場合は横持タリフの名称を表示する
                remShiharai = unsoDr.Item("SHIHARAI_YOKO_REM").ToString()
            End If
            .lblShiharaiTariffNM.TextValue = remShiharai
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            .txtShukkaMotoCD.TextValue = unsoDr.Item("ORIG_CD").ToString()
            .lblShukkaMotoNM.TextValue = unsoDr.Item("ORIG_CD_NM").ToString()
            .txtUnchinComment.TextValue = unsoDr.Item("REMARK").ToString()
            .numUnsoJuryo.Value = unsoDr.Item("UNSO_WT").ToString()
            .numKyori.Value = Me._LMBconG.FormatNumValue(unsoDr.Item("KYORI").ToString())
            .cmbTax.SelectedValue = unsoDr.Item("TAX_KB").ToString()
            .cmbYusoBrCd.SelectedValue = unsoDr.Item("YUSO_BR_CD").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 作業情報取得のSQL
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="value">INKA_NO_M</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Friend Function SetSagyoSql(ByVal dr As DataRow, ByVal value As String) As String

        Dim iokaNoL As String = String.Concat(dr.Item("INKA_NO_L").ToString(), value)
        Return String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString(), "' " _
                                          , "AND INOUTKA_NO_LM = '", iokaNoL, "' " _
                                          , "AND SYS_DEL_FLG = '0' " _
                                          )

    End Function

    ''' <summary>
    ''' 画面の値に応じてのロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="unsoEdit">運行紐付けフラグ</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal actionType As LMB020C.ActionType, ByVal unsoEdit As Boolean)

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '特殊編集モードの場合、スルー
            Select Case actionType

                Case LMB020C.ActionType.DATEEDIT

                    Exit Sub

            End Select

            Dim ptn1 As Boolean = True
            Dim ptn2 As Boolean = True
            Dim ptn3 As Boolean = True
            Dim ptn4 As Boolean = True
            Dim ptn5 As Boolean = True
            Dim edit As Boolean = Not unsoEdit

            Dim unsoUmu As String = .cmbUnchinUmu.SelectedValue.ToString()
            Dim unchinKbn As String = .cmbUnchinKbn.SelectedValue.ToString()

            '日陸手配の場合、活性化
            If LMB020C.TEHAI_NRS.Equals(unsoUmu) = True Then

                ptn1 = False

                '車扱の場合、活性化
                If LMB020C.TARIFF_KURUMA.Equals(unchinKbn) = True Then

                    ptn2 = False
                    ptn3 = False
                    ptn5 = False

                End If

                '横持ちの場合、活性化
                If LMB020C.TARIFF_YOKO.Equals(unchinKbn) = True Then

                    ptn2 = False
                    ptn5 = False

                End If

                '混載 または 特便の場合、活性化
                If LMB020C.TARIFF_KONSAI.Equals(unchinKbn) = True _
                    OrElse LMB020C.TARIFF_TOKUBIN.Equals(unchinKbn) = True _
                    Then

                    ptn3 = False
                    ptn5 = False

                End If

                '路線の場合、活性化
                If LMB020C.TARIFF_ROSEN.Equals(unchinKbn) = True Then

                    ptn4 = False

                End If

            End If

            'ロック制御

            'ロック制御パターン1
            Call Me._LMBconG.SetLockInputMan(.cmbUnchinKbn, ptn1)
            Call Me._LMBconG.SetLockInputMan(.txtShukkaMotoCD, ptn1)
            Call Me._LMBconG.SetLockInputMan(.txtUnchinComment, ptn1)
            Call Me._LMBconG.SetLockInputMan(.cmbTax, ptn1)
            Call Me._LMBconG.SetLockInputMan(.cmbYusoBrCd, ptn1)

            '運行紐付けされている場合、ロック
            Dim clearFlg As Boolean = True
            If unsoEdit = False Then
                ptn1 = True
                clearFlg = False
            End If

            '編集モード時にロック解除
            Call Me._LMBconG.SetLockInputMan(.cmbUnchinUmu, edit, clearFlg)

            Call Me._LMBconG.SetLockInputMan(.txtUnsoCd, ptn1, clearFlg)
            Call Me._LMBconG.SetLockInputMan(.txtTrnBrCD, ptn1, clearFlg)

            'ロック制御パターン2
            Call Me._LMBconG.SetLockInputMan(.cmbSharyoKbn, ptn2)

            'ロック制御パターン3
            Call Me._LMBconG.SetLockInputMan(.cmbTrnThermoKbn, ptn3)

            'ロック制御パターン4
            Call Me._LMBconG.SetLockInputMan(.numUnchin, ptn4)

            'ロック制御パターン5
            Call Me._LMBconG.SetLockInputMan(.txtUnsoTariffCD, ptn5)
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
            'Call Me._LMBconG.SetLockInputMan(.txtShiharaiTariffCD, ptn5)
            Call Me._LMBconG.SetLockInputMan(.txtShiharaiTariffCD, ptn1, clearFlg)
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End  
            '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            'コンボボックスのテキストだけ空になって、SelectedValueは初期化されてない不具合対応
            If String.IsNullOrEmpty(.cmbUnchinUmu.TextValue) = True Then
                .cmbUnchinUmu.SelectedValue = Nothing
            End If
            If String.IsNullOrEmpty(.cmbUnchinKbn.TextValue) = True Then
                .cmbUnchinKbn.SelectedValue = Nothing
            End If
            If String.IsNullOrEmpty(.cmbTax.TextValue) = True Then
                .cmbTax.SelectedValue = Nothing
            End If
            If String.IsNullOrEmpty(.cmbSharyoKbn.TextValue) = True Then
                .cmbSharyoKbn.SelectedValue = Nothing
            End If
            If String.IsNullOrEmpty(.cmbTrnThermoKbn.TextValue) = True Then
                .cmbTrnThermoKbn.SelectedValue = Nothing
            End If

            '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 Start
            If String.IsNullOrEmpty(.cmbYusoBrCd.TextValue) = True Then
                .cmbYusoBrCd.SelectedValue = Nothing
            End If
            '要望番号1357:(輸送営業所に初期値設定し、必須チェックを入れる) 2012/08/22 本明 End


            '運送会社、出荷元がロックの場合、名称をクリア
            If ptn1 = True AndAlso String.IsNullOrEmpty(.txtUnsoCd.TextValue) = True Then
                .lblTrnNM.TextValue = String.Empty
                .lblShukkaMotoNM.TextValue = String.Empty
            End If

            'タリフがロックの場合、名称をクリア
            If ptn5 = True Then
                .lblUnsoTariffNM.TextValue = String.Empty

                '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 Start
                .lblShiharaiTariffNM.TextValue = String.Empty
                '要望番号1302:(支払運賃に伴う修正) 2012/08/15 本明 End

            End If

        End With

    End Sub

    ''' <summary>
    ''' 運送のデフォルト値を、運賃タリフセットから取得し、設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetUnsoTariffSet()

        With Me._Frm

            '運賃タリフセットマスタからタリフコードを取得し、設定
            Dim tariffSet As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                              "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                                              "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND ", _
                                                                                                                              "SET_KB = '02' AND DEST_CD = '' "))
            'START YANAI 要望番号691
            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                              "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                              "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
            'END YANAI 要望番号691

            'START YANAI 要望番号691
            'If 0 < tariffSet.Length = True Then
            If 0 < tariffSet.Length = True AndAlso 0 < custDr.Length Then

                'END YANAI 要望番号691
                'START YANAI 要望番号691
                '.cmbUnchinKbn.SelectedValue = tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()

                'If ("90").Equals(.cmbUnchinUmu.SelectedValue) = False AndAlso _
                'String.IsNullOrEmpty(.cmbUnchinKbn.SelectedValue.ToString) = True Then
                '    '手配区分が"未定"以外で、タリフ分類区分が未設定の時は、手配区分を"未定"にする
                '    .cmbUnchinUmu.SelectedValue = "90"
                'End If
                If String.IsNullOrEmpty(custDr(0).Item("UNSO_TEHAI_KB").ToString()) = True OrElse _
                    ("90").Equals(custDr(0).Item("UNSO_TEHAI_KB").ToString()) = True Then
                    .cmbUnchinUmu.SelectedValue = "90"
                ElseIf ("90").Equals(custDr(0).Item("UNSO_TEHAI_KB").ToString()) = False AndAlso _
                    String.IsNullOrEmpty(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                    .cmbUnchinUmu.SelectedValue = "90"
                Else
                    .cmbUnchinUmu.SelectedValue = custDr(0).Item("UNSO_TEHAI_KB").ToString()
                End If
                'END YANAI 要望番号691
                '2013.02.05 タリフ分類区分が設定されない修正START
                If .txtUnsoTariffCD.ReadOnly = False OrElse ("10").Equals(.cmbUnchinUmu.SelectedValue) = True Then
                    .cmbUnchinKbn.SelectedValue = tariffSet(0)("TARIFF_BUNRUI_KB")
                    '2013.02.05 タリフ分類区分が設定されない修正END
                    If ("40").Equals(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                        '横持ちの場合は横持ちタリフコードをセット
                        .txtUnsoTariffCD.TextValue = tariffSet(0).Item("YOKO_TARIFF_CD").ToString()
                        Dim yokodr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                              "YOKO_TARIFF_CD = '", .txtUnsoTariffCD.TextValue, "' "))

                        If 0 < yokodr.Length Then
                            .lblUnsoTariffNM.TextValue = yokodr(0).Item("YOKO_REM").ToString()
                        End If

                    ElseIf ("20").Equals(tariffSet(0).Item("TARIFF_BUNRUI_KB").ToString()) = True Then
                        '車扱いの場合は運賃タリフコード２をセット
                        .txtUnsoTariffCD.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_CD2").ToString()
                        .lblUnsoTariffNM.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_REM2").ToString()
                    Else
                        '横持ち以外の場合は運賃タリフコードをセット
                        .txtUnsoTariffCD.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_CD1").ToString()
                        .lblUnsoTariffNM.TextValue = tariffSet(0).Item("UNCHIN_TARIFF_REM1").ToString()
                    End If
                    '.txtExtc.TextValue = tariffSet(0).Item("EXTC_TARIFF_CD").ToString()
                End If

            Else

                '手配区分が"未定"以外で、タリフ分類区分が未設定の時は、手配区分を"未定"にするのだが、
                'タリフセットからデータが取得できない時点でタリフ分類は空白確定なので、"未定"にする。
                .cmbUnchinUmu.SelectedValue = "90"

            End If

        End With

    End Sub

    ''' <summary>
    ''' 運送会社をキャッシュより取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub GetUnsoCompany()

        With Me._Frm

            '荷主マスタより運賃請求先マスタコードを取得
            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                                     "NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND " _
                                                                                     , "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
                                                                                     , "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND " _
                                                                                     , "CUST_CD_S = '00' AND " _
                                                                                     , "CUST_CD_SS = '00'"))
            If custDr.Length = 0 Then
                '荷主がヒットしない場合はこれ以上やりようがないので、処理を終わる
                Exit Sub
            End If

            '運賃請求先マスタコードを元に、届先マスタより指定運送会社コードを取得
            '---↓
            'Dim destDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat( _
            '                                                                         "NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND " _
            '                                                                         , "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
            '                                                                         , "DEST_CD = '", custDr(0).Item("UNCHIN_SEIQTO_CD").ToString, "'"))

            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            destMstDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            destMstDr.Item("DEST_CD") = custDr(0).Item("UNCHIN_SEIQTO_CD").ToString
            destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            Dim destDr As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If destDr.Length > 0 Then
                '届先マスタの運送会社コードを画面に設定
                .txtUnsoCd.TextValue = destDr(0).Item("SP_UNSO_CD").ToString
                .txtTrnBrCD.TextValue = destDr(0).Item("SP_UNSO_BR_CD").ToString
            End If

            If String.IsNullOrEmpty(.txtUnsoCd.TextValue) = True AndAlso _
                String.IsNullOrEmpty(.txtTrnBrCD.TextValue) = True Then
                '届先マスタの運送会社コードが未設定の場合は、荷主マスタの運送会社コードを設定する。
                .txtUnsoCd.TextValue = custDr(0).Item("SP_UNSO_CD").ToString
                .txtTrnBrCD.TextValue = custDr(0).Item("SP_UNSO_BR_CD").ToString
            End If

            If String.IsNullOrEmpty(.txtUnsoCd.TextValue) = True AndAlso _
                String.IsNullOrEmpty(.txtTrnBrCD.TextValue) = True Then
                '運送会社コード・支店コードの両方が空の場合は、この先の検索処理でヒットしないのがわかっているので、処理を終わる
                Exit Sub
            End If

            '指定運送会社コードを元に、運送会社マスタより運送会社名を取得
            Dim unsoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat( _
                                                                                      "NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND " _
                                                                                    , "UNSOCO_CD = '", .txtUnsoCd.TextValue, "' AND " _
                                                                                    , "UNSOCO_BR_CD = '", .txtTrnBrCD.TextValue, "'"))
            If unsoDr.Length = 0 Then
                Exit Sub
            End If

            '運送会社名を画面に設定
            .lblTrnNM.TextValue = String.Concat(unsoDr(0).Item("UNSOCO_NM").ToString, unsoDr(0).Item("UNSOCO_BR_NM").ToString)

        End With

    End Sub

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードOLD設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub SetUnsoCdOld(ByVal frm As LMB020F)

        frm.txtUnsoCdOld.TextValue = frm.txtUnsoCd.TextValue
        frm.txtTrnBrCDOld.TextValue = frm.txtTrnBrCD.TextValue

    End Sub

    ''' <summary>
    ''' 運賃タリフを運賃タリフセットマスタキャッシュより取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub GetUnchinTariffSet(ByVal frm As LMB020F, ByVal tariffBunruiFlg As Boolean)

        Dim updateFlg As Boolean = False

        With frm
            Dim tariffSetDr() As DataRow = Nothing

            If tariffBunruiFlg = False Then
                tariffSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET_UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                          "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                                          "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsoCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtTrnBrCD.TextValue, "' AND ", _
                                                                                                                          "UNSO_TEHAI_KB = '", .cmbUnchinUmu.SelectedValue, "'"))
            Else
                tariffSetDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET_UNSOCO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                          "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                                          "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_CD = '", .txtUnsoCd.TextValue, "' AND ", _
                                                                                                                          "UNSOCO_BR_CD = '", .txtTrnBrCD.TextValue, "' AND ", _
                                                                                                                          "UNSO_TEHAI_KB = '", .cmbUnchinUmu.SelectedValue, "' AND ", _
                                                                                                                          "TARIFF_BUNRUI_KB = '", .cmbUnchinKbn.SelectedValue, "'"))

            End If

            If 0 < tariffSetDr.Length Then
                .txtUnsoTariffCD.TextValue = String.Empty
                .lblUnsoTariffNM.TextValue = String.Empty
                .cmbUnchinKbn.SelectedValue = tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString

                If ("10").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '混載の場合
                    .txtUnsoTariffCD.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD").ToString
                    .lblUnsoTariffNM.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD_NM").ToString
                ElseIf ("20").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '車扱いの場合
                    .txtUnsoTariffCD.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD").ToString
                    .lblUnsoTariffNM.TextValue = tariffSetDr(0).Item("UNCHIN_TARIFF_CD_NM").ToString
                ElseIf ("30").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '特便の場合
                ElseIf ("40").Equals(tariffSetDr(0).Item("TARIFF_BUNRUI_KB").ToString) = True Then
                    '横持ちの場合
                End If

            End If

        End With

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬


    ''' <summary>
    ''' 総保入期限処理判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>設定されている場合、False　その他、True</returns>
    ''' <remarks></remarks>
    Friend Function StorageDueDateControl(ByVal frm As LMB020F, ByVal ds As DataSet) As Boolean

        '荷主詳細  SUB_KB = '0X'より、総保入期限対象か
        Dim sSql As String = "NRS_BR_CD = '" & frm.cmbEigyo.SelectedValue.ToString() & "' AND CUST_CD = '" & frm.txtCustCdL.TextValue.ToString() & frm.txtCustCdM.TextValue.ToString() & "' " _
                             & " AND SUB_KB = '0X' AND SET_NAIYO = '1'"
        Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sSql)
        If dr.Length > 0 Then
            '総保入期限処理対象
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' タブレット項目の初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetWHTabletControl()
        With Me._Frm

            Dim nrsBrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd
            Dim kbnDr() As DataRow = Nothing
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'B007' AND ", _
                                                                                           "KBN_CD = '", nrsBrCd, "' AND ", _
                                                                                           "VALUE1 = '1.000'"))
            If kbnDr.Length > 0 Then
                .chkTablet.Checked = True
            End If

        End With
    End Sub

#End Region '設定・制御

#End Region 'Form

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(INKA_M)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGoodsDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.DEF, " ", 20, True)
        Public Shared SORT_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.SORT_NO, "印順", 30, True)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.KANRI_NO, "入荷管理番号" & vbCrLf & "(中)", 100, True)
        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.GOODS_CD, "商品コード", 100, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.GOODS_NM, "商品名", 150, True)
        Public Shared ALL_KOSU As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.ALL_KOSU, "合計個数", 100, True)
        Public Shared ALL_SURYO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.ALL_SURYO, "合計数量", 120, True)
        Public Shared HIKIATE_STATE As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.HIKIATE_STATE, "引当状況", 150, True)
        Public Shared GOODS_COMMENT As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.GOODS_COMMENT, "商品コメント", 150, True)
        Public Shared ORDER_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.ORDER_NO, "オーダー番号", 150, True)
        Public Shared SAGYO_UMU As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.SAGYO_UMU, "作業有無", 80, True)
        Public Shared RECNO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaMColumnIndex.RECNO, "", 0, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(INKA_S)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.DEF, " ", 20, True)
        Public Shared KANRI_NO_S As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.KANRI_NO_S, "入荷管理番号" & vbCrLf & "(小)", 95, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.LOT_NO, "ロット№", 100, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.TOU_NO, "棟", 30, True)
        Public Shared SHITSU_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.SHITSU_NO, "室", 30, True)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.ZONE_CD, "ZONE", 40, True)
        Public Shared LOCA As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.LOCA, "ロケーション", 90, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.NB, "梱数", 70, True)
        Public Shared HASU As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.HASU, "端数", 70, True)
        Public Shared SUM As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.SUM, "個数", 70, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.IRIME, "入目", 70, True)
        Public Shared TANI As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.TANI, "単位", 50, True)
        Public Shared SURYO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.SURYO, "数量", 70, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.JURYO, "正味重量", 70, True)
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.LT_DATE, "賞味期限", 100, True)
        Public Shared ENT_PHOTO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.ENT_PHOTO, "登録済み画像", 100, True)    'ADD 2022/11/07 倉庫写真アプリ対応
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.REMARK, "備考小(社内)", 100, True)
        Public Shared REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.REMARK_OUT, "備考小(社外)", 100, True)
        Public Shared GOODS_COND_KB_1 As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.GOODS_COND_KB_1, "状態 中身", 100, True)
        Public Shared GOODS_COND_KB_2 As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.GOODS_COND_KB_2, "状態 外装", 120, True)
        Public Shared GOODS_COND_KB_3 As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.GOODS_COND_KB_3, "状態 荷主", 100, True)
        Public Shared OFB_KBN As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.OFB_KBN, "簿外品", 65, True)
        Public Shared SPD_KBN_S As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.SPD_KBN_S, "保留品", 100, True)
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.SERIAL_NO, "シリアル№", 150, True)
        Public Shared GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.GOODS_CRT_DATE, "製造日", 100, True)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.DEST_CD, "届先コード", 100, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.DEST_NM, "届先名", 150, True)
        Public Shared ALLOC_PRIORITY As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.ALLOC_PRIORITY, "割当優先", 85, True)
        Public Shared LOT_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.LOT_CTL_KB, "", 0, False)
        Public Shared LT_DATE_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.LT_DATE_CTL_KB, "", 0, False)
        Public Shared CRT_DATE_CTL_KB As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.CRT_DATE_CTL_KB, "", 0, False)
        Public Shared RECNO As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.RECNO, "", 0, False)
        Public Shared STD_WT As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.STD_WT, "", 0, False)
        Public Shared IMG_YN As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.IMG_YN, "画像", 40, True)
        Public Shared BUG_YN As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.BUG_YN, "不具合", 60, True)

        ''2013.07.16 追加START①
        'Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.GOODS_CD_NRS, "商品キー", 100, True)
        'Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMB020C.SprInkaSColumnIndex.GOODS_CD_CUST, "商品コード", 100, True)
        ''2013.07.16 追加END①

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            '入荷(中)スプレッドの設定
            Call Me.InitSpread(.sprGoodsDef, New LMB020G.sprGoodsDef(), 12)

            '入荷(小)スプレッドの設定
            'UPD 2022/11/07 倉庫写真アプリ対応 START
            'Call Me.InitSpread(.sprDetail, New LMB020G.sprDetailDef(), 34)
            Call Me.InitSpread(.sprDetail, New LMB020G.sprDetailDef(), 35)
            'UPD 2022/11/07 倉庫写真アプリ対応 END


            '列固定位置を設定します。(運送会社（2次）で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMB020G.sprDetailDef.LOCA.ColNo + 1


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
            .SetColProperty(columnClass, False)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInkaMData(ByVal ds As DataSet)

        '値のクリア
        Call Me.ClearInkaMControl()

        Dim spr As LMSpread = Me._Frm.sprGoodsDef

        'SPREAD(表示行)初期化
        spr.CrearSpread()

        'セルに設定するスタイルの取得
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sNum As StyleInfo = Me.StyleInfoNum9dec3(spr, True)
        Dim sKosu As StyleInfo = Me.StyleInfoNum10(spr, True)
        Dim sSuryo As StyleInfo = Me.StyleInfoNum12dec3(spr, True)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
        Dim nLabel As StyleInfo = Me.StyleInfoLabel(spr, CellHorizontalAlignment.Right)

        Dim rowCnt As Integer = 0
        Dim setDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim max As Integer = setDt.Rows.Count - 1

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To max

                'SYS_DEL_FLGが'1'のものは表示しない
                If LMConst.FLG.ON.Equals(setDt.Rows(i).Item("SYS_DEL_FLG").ToString()) = True Then
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.SORT_NO.ColNo, nLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.KANRI_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.GOODS_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.ALL_KOSU.ColNo, sKosu)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.ALL_SURYO.ColNo, sSuryo)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.GOODS_COMMENT.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.ORDER_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.SAGYO_UMU.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.RECNO.ColNo, sLabel)

                '値設定
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.SORT_NO.ColNo, setDt.Rows(i).Item("PRINT_SORT").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.KANRI_NO.ColNo, setDt.Rows(i).Item("INKA_NO_M").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.GOODS_CD.ColNo, setDt.Rows(i).Item("GOODS_CD_CUST").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.GOODS_NM.ColNo, setDt.Rows(i).Item("GOODS_NM").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.ALL_KOSU.ColNo, setDt.Rows(i).Item("SUM_KOSU").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.ALL_SURYO.ColNo, setDt.Rows(i).Item("SUM_SURYO_M").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo, setDt.Rows(i).Item("HIKIATE").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.GOODS_COMMENT.ColNo, setDt.Rows(i).Item("REMARK").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.ORDER_NO.ColNo, setDt.Rows(i).Item("OUTKA_FROM_ORD_NO_M").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.SAGYO_UMU.ColNo, setDt.Rows(i).Item("SAGYO_UMU").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.RECNO.ColNo, Me.SetZeroData(i.ToString(), LMB020C.MAEZERO))

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <remarks></remarks>
    Friend Sub SetInkaSData(ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType, ByVal inkaMNo As String)

        Dim spr As LMSpread = Me._Frm.sprDetail

        'ロック制御
        Dim lock As Boolean = True

        Dim sql As String = String.Concat("SYS_DEL_FLG = '0' AND HIKIATE = '", LMB020C.HIKIATE_ARI, "' AND INKA_NO_M = '", inkaMNo, "' ")
        Dim editMode As String = Me._Frm.lblEdit.TextValue

        '参照以外 且つ 引当未 且つ 特殊編集モード以外、ロック解除
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = False _
            AndAlso ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql).Length < 1 _
            AndAlso LMB020C.ActionType.DATEEDIT.ToString().Equals(editMode) = False _
            AndAlso LMB020C.ActionType.UNSOEDIT.ToString().Equals(editMode) = False _
            Then
            lock = False

        End If

        'セルに設定するスタイルの取得
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
        Dim sLot As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 40, lock)
        Dim sTou As StyleInfo = Me.StyleInfoTextHankaku(spr, 2, lock)
        'START YANAI 要望番号705
        'Dim sShituZone As StyleInfo = Me.StyleInfoTextHankaku(spr, 1, lock)
        Dim sShituZone As StyleInfo = Me.StyleInfoTextHankaku(spr, 2, lock)
        'END YANAI 要望番号705
        Dim sLoc As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 10, lock)
        Dim sKon As StyleInfo = Me.StyleInfoNum10(spr, lock)
        Dim sHasu As StyleInfo = Me.StyleInfoNum10(spr, lock)

        '入数が1の場合、端数をロック
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = False AndAlso Me._Frm.numIrisu.TextValue.Equals("1") = True Then
            sHasu = Me.StyleInfoNum10(spr, True)
        End If

        Dim sKosu As StyleInfo = Me.StyleInfoNum10(spr, True)
        Dim sIrime As StyleInfo = Me.StyleInfoNum6dec3(spr, lock)
        Dim s9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr, True)

        '(2012.09.28)要望場号1463 IME設定をALL-MIXに変更 -- START --
        'Dim sMix As StyleInfo = Me.StyleInfoTextHankaku(spr, 100, lock)
        'Dim sTxt As StyleInfo = Me.StyleInfoTextHankaku(spr, 15, lock)
        Dim sMix As StyleInfo = Me.StyleInfoTextMix(spr, 100, lock)
        Dim sTxt As StyleInfo = Me.StyleInfoTextMix(spr, 15, lock)
        '(2012.09.28)要望場号1463 IME設定をALL-MIXに変更 --  END  --

        Dim sCmbGoodsIn As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_S005, lock)
        Dim sCmbGoodsOut As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_S006, lock)
        Dim sCmbBogai As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_B002, lock)
        Dim sCmbHoryu As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_H003, lock)
        Dim sCmbYusen As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_W001, lock)
        Dim sCustM As StyleInfo = Me.StyleInfoCustCond(spr, ds, lock)
        Dim sDate As StyleInfo = Me.StyleInfoDate(spr, lock)
        Dim sCmbImg As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_U009, True)
        Dim sCmbBug As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_U009, lock)

        Dim rowCnt As Integer = 0
        Dim setDrs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(Me.SetInkaSSql(inkaMNo), "INKA_NO_S")
        Dim max As Integer = setDrs.Length - 1
        Dim errDt As DataTable = ds.Tables(LMB020C.TABLE_NM_ERR)
        Dim errNo As String = String.Empty
        If 0 < errDt.Rows.Count Then
            errNo = errDt(0).Item("INKA_S_NO").ToString()
        End If
        Dim inkaSNo As String = String.Empty

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To max

                'SYS_DEL_FLGが'1'のものは表示しない
                If LMConst.FLG.ON.Equals(setDrs(i).Item("SYS_DEL_FLG").ToString()) = True Then
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LOT_NO.ColNo, sLot)
#If True Then ' JT物流入荷検品対応 20160726 added inoue
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, sDate)
#End If
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.TOU_NO.ColNo, sTou)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, sShituZone)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, sShituZone)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LOCA.ColNo, sLoc)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.NB.ColNo, sKon)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.HASU.ColNo, sHasu)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SUM.ColNo, sKosu)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.IRIME.ColNo, sIrime)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.TANI.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SURYO.ColNo, s9dec3)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, s9dec3)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LT_DATE.ColNo, sDate)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, sLabel) 'ADD 2022/11/07 倉庫写真アプリ対応
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.REMARK.ColNo, sMix)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.REMARK_OUT.ColNo, sTxt)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_1.ColNo, sCmbGoodsIn)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_2.ColNo, sCmbGoodsOut)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_3.ColNo, sCustM)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.OFB_KBN.ColNo, sCmbBogai)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SPD_KBN_S.ColNo, sCmbHoryu)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SERIAL_NO.ColNo, sLot)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, sDate)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.DEST_CD.ColNo, sTxt)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo, sCmbYusen)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.RECNO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.STD_WT.ColNo, s9dec3)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.IMG_YN.ColNo, sCmbImg)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.BUG_YN.ColNo, sCmbBug)
                ''2013.07.16 追加START①
                '.SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                '.SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                ''2013.07.16 追加END①

                '値設定
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.DEF.ColNo, False.ToString())
                inkaSNo = setDrs(i).Item("INKA_NO_S").ToString()
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, inkaSNo)
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.LOT_NO.ColNo, setDrs(i).Item("LOT_NO").ToString())
#If True Then ' JT物流入荷検品対応 20160726 added inoue
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(setDrs(i).Item("GOODS_CRT_DATE").ToString()))
#End If
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.TOU_NO.ColNo, setDrs(i).Item("TOU_NO").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, setDrs(i).Item("SITU_NO").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, setDrs(i).Item("ZONE_CD").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.LOCA.ColNo, setDrs(i).Item("LOCA").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.NB.ColNo, setDrs(i).Item("KONSU").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.HASU.ColNo, Me._LMBconG.FormatNumValue(setDrs(i).Item("HASU").ToString()))
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SUM.ColNo, Me._LMBconG.FormatNumValue(setDrs(i).Item("KOSU_S").ToString()))
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.IRIME.ColNo, setDrs(i).Item("IRIME").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.TANI.ColNo, setDrs(i).Item("STD_IRIME_NM").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SURYO.ColNo, setDrs(i).Item("SURYO_S").ToString())
                'START YANAI 運送・運行・請求メモNo.48
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, Me.RoundDown(setDrs(i).Item("JURYO_S").ToString(), LMB020C.JURYO_ROUND_POS))
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, Convert.ToString(Me.ToRound(Convert.ToDecimal(setDrs(i).Item("JURYO_S").ToString()), LMB020C.JURYO_ROUND_POS)))
                'END YANAI 運送・運行・請求メモNo.48
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(setDrs(i).Item("LT_DATE").ToString()))

                'ADD 2022/11/07 倉庫写真アプリ対応 START
                If LMB020C.FLG_ON.Equals(setDrs(i).Item("PHOTO_YN").ToString()) Then
                    .SetCellValue(rowCnt, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, LMB020C.PHOTO_YN_NM)
                Else
                    .SetCellValue(rowCnt, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, String.Empty)
                End If
                'ADD 2022/11/07 倉庫写真アプリ対応 END

                .SetCellValue(rowCnt, LMB020G.sprDetailDef.REMARK.ColNo, setDrs(i).Item("REMARK").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.REMARK_OUT.ColNo, setDrs(i).Item("REMARK_OUT").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_1.ColNo, setDrs(i).Item("GOODS_COND_KB_1").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_2.ColNo, setDrs(i).Item("GOODS_COND_KB_2").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_3.ColNo, setDrs(i).Item("GOODS_COND_KB_3").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.OFB_KBN.ColNo, setDrs(i).Item("OFB_KB").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SPD_KBN_S.ColNo, setDrs(i).Item("SPD_KB").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SERIAL_NO.ColNo, setDrs(i).Item("SERIAL_NO").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(setDrs(i).Item("GOODS_CRT_DATE").ToString()))
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.DEST_CD.ColNo, setDrs(i).Item("DEST_CD").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.DEST_NM.ColNo, setDrs(i).Item("DEST_NM").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo, setDrs(i).Item("ALLOC_PRIORITY").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo, setDrs(i).Item("LOT_CTL_KB").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo, setDrs(i).Item("LT_DATE_CTL_KB").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo, setDrs(i).Item("CRT_DATE_CTL_KB").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.RECNO.ColNo, Me.SetZeroData(i.ToString(), LMB020C.MAEZERO))
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.STD_WT.ColNo, setDrs(i).Item("STD_WT_KGS").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.IMG_YN.ColNo, setDrs(i).Item("IMG_YN").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.BUG_YN.ColNo, setDrs(i).Item("BUG_YN").ToString())
                ''2013.07.16 追加START①
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CD_NRS.ColNo, setDrs(i).Item("GOODS_CD_NRS").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CD_CUST.ColNo, setDrs(i).Item("GOODS_CD_CUST").ToString())
                ''2013.07.16 追加END①

                'エラー設定
                Call Me.SetErrCell(spr, errDt, inkaSNo, errNo, rowCnt)

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにCSVデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="inkaMNo">入荷中番</param>
    ''' <remarks></remarks>
    Friend Sub SetInkaSData_CSV(ByVal ds As DataSet, ByVal actionType As LMB020C.ActionType, ByVal inkaMNo As String)

        Dim spr As LMSpread = Me._Frm.sprDetail

        'ロック制御
        Dim lock As Boolean = True

        Dim sql As String = String.Concat("SYS_DEL_FLG = '0' AND HIKIATE = '", LMB020C.HIKIATE_ARI, "' AND INKA_NO_M = '", inkaMNo, "' ")
        Dim editMode As String = Me._Frm.lblEdit.TextValue

        '参照以外 且つ 引当未 且つ 特殊編集モード以外、ロック解除
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = False _
            AndAlso ds.Tables(LMB020C.TABLE_NM_INKA_M).Select(sql).Length < 1 _
            AndAlso LMB020C.ActionType.DATEEDIT.ToString().Equals(editMode) = False _
            AndAlso LMB020C.ActionType.UNSOEDIT.ToString().Equals(editMode) = False _
            Then
            lock = False

        End If

        'セルに設定するスタイルの取得
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
        Dim sLot As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 40, lock)
        Dim sTou As StyleInfo = Me.StyleInfoTextHankaku(spr, 2, lock)
        'START YANAI 要望番号705
        'Dim sShituZone As StyleInfo = Me.StyleInfoTextHankaku(spr, 1, lock)
        Dim sShituZone As StyleInfo = Me.StyleInfoTextHankaku(spr, 2, lock)
        'END YANAI 要望番号705
        Dim sLoc As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 10, lock)
        Dim sKon As StyleInfo = Me.StyleInfoNum10(spr, lock)
        Dim sHasu As StyleInfo = Me.StyleInfoNum10(spr, lock)

        '入数が1の場合、端数をロック
        If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = False AndAlso Me._Frm.numIrisu.TextValue.Equals("1") = True Then
            sHasu = Me.StyleInfoNum10(spr, True)
        End If

        Dim sKosu As StyleInfo = Me.StyleInfoNum10(spr, True)
        Dim sIrime As StyleInfo = Me.StyleInfoNum6dec3(spr, lock)
        Dim s9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr, True)

        '(2012.09.28)要望場号1463 IME設定をALL-MIXに変更 -- START --
        'Dim sMix As StyleInfo = Me.StyleInfoTextHankaku(spr, 100, lock)
        'Dim sTxt As StyleInfo = Me.StyleInfoTextHankaku(spr, 15, lock)
        Dim sMix As StyleInfo = Me.StyleInfoTextMix(spr, 100, lock)
        Dim sTxt As StyleInfo = Me.StyleInfoTextMix(spr, 15, lock)
        '(2012.09.28)要望場号1463 IME設定をALL-MIXに変更 --  END  --

        Dim sCmbGoodsIn As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_S005, lock)
        Dim sCmbGoodsOut As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_S006, lock)
        Dim sCmbBogai As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_B002, lock)
        Dim sCmbHoryu As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_H003, lock)
        Dim sCmbYusen As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_W001, lock)
        Dim sCustM As StyleInfo = Me.StyleInfoCustCond(spr, ds, lock)
        Dim sDate As StyleInfo = Me.StyleInfoDate(spr, lock)

        Dim rowCnt As Integer = 0
        Dim setDrs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(Me.SetInkaSSql(inkaMNo), "INKA_NO_S")
        Dim max As Integer = setDrs.Length - 1
        Dim errDt As DataTable = ds.Tables(LMB020C.TABLE_NM_ERR)
        Dim errNo As String = String.Empty
        If 0 < errDt.Rows.Count Then
            errNo = errDt(0).Item("INKA_S_NO").ToString()
        End If
        Dim inkaSNo As String = String.Empty


        'csv対応
        Dim csvDt As DataTable = ds.Tables(LMB020C.TABLE_NM_CSV_DATA)

        Dim csvMax As Integer = csvDt.Rows.Count
        Dim intSno As Integer = Convert.ToInt32("000")
        Dim strSno As String

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To csvMax - 1

                ''SYS_DEL_FLGが'1'のものは表示しない
                'If LMConst.FLG.ON.Equals(setDrs(i).Item("SYS_DEL_FLG").ToString()) = True Then
                '    Continue For
                'End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                '.ActiveSheet.AddRows(rowCnt, csvMax)
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LOT_NO.ColNo, sLot)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.TOU_NO.ColNo, sTou)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, sShituZone)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, sShituZone)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LOCA.ColNo, sLoc)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.NB.ColNo, sKon)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.HASU.ColNo, sHasu)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SUM.ColNo, sKosu)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.IRIME.ColNo, sIrime)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.TANI.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SURYO.ColNo, s9dec3)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, s9dec3)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LT_DATE.ColNo, sDate)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, sLabel) 'ADD 2022/11/07 倉庫写真アプリ対応
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.REMARK.ColNo, sMix)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.REMARK_OUT.ColNo, sTxt)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_1.ColNo, sCmbGoodsIn)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_2.ColNo, sCmbGoodsOut)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_3.ColNo, sCustM)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.OFB_KBN.ColNo, sCmbBogai)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SPD_KBN_S.ColNo, sCmbHoryu)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.SERIAL_NO.ColNo, sLot)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, sDate)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.DEST_CD.ColNo, sTxt)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo, sCmbYusen)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.RECNO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.STD_WT.ColNo, s9dec3)

                ''2013.07.16 追加START①
                '.SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                '.SetCellStyle(rowCnt, LMB020G.sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                ''2013.07.16 追加END①


                'CSVデータ追加
                intSno = intSno + 1
                strSno = intSno.ToString()
                Select Case strSno.Length
                    Case 1
                        strSno = "00" + strSno
                    Case 2
                        strSno = "0" + strSno
                End Select

                .SetCellValue(rowCnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, strSno)
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.REMARK_OUT.ColNo, csvDt.Rows(i).Item("REMARK_OUT").ToString())
                'dr.Item("REMARK_OUT") = csvDt.Rows(i).Item("入番").ToString()
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.LOT_NO.ColNo, csvDt.Rows(i).Item("LOT_NO").ToString())
                'dr.Item("LOT_NO") = csvDt.Rows(i).Item("ロット").ToString()
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SERIAL_NO.ColNo, csvDt.Rows(i).Item("SERIAL_NO").ToString())
                'dr.Item("SERIAL_NO") = csvDt.Rows(i).Item("ｼﾘﾝﾀﾞｰ").ToString()
                'dr.Item("REMARK") = csvDt.Rows(i).Item("コメント").ToString()
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.REMARK.ColNo, csvDt.Rows(i).Item("REMARK").ToString())
                'dr.Item("TOU_NO") = csvDt.Rows(i).Item("置場").ToString().Substring(0, 2)
                'dr.Item("SITU_NO") = csvDt.Rows(i).Item("置場").ToString().Substring(2, 1)
                'dr.Item("ZONE_CD") = csvDt.Rows(i).Item("置場").ToString().Substring(3, 1)    'ZONE_CDとの見分けが困難、ゾーンCDのほとんどが1バイトのための対応
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.TOU_NO.ColNo, csvDt.Rows(i).Item("LOC").ToString().Substring(0, 2))
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, csvDt.Rows(i).Item("LOC").ToString().Substring(2, 1)) 'ZONE_CDとの見分けが困難、室NOのほとんどが1バイトのための対応
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, csvDt.Rows(i).Item("LOC").ToString().Substring(3, 1)) 'SHITSU_NOとの見分けが困難、室NOのほとんどが1バイトのための対応



                .SetCellValue(rowCnt, LMB020G.sprDetailDef.DEF.ColNo, False.ToString())
                'inkaSNo = setDrs(i).Item("INKA_NO_S").ToString()
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, inkaSNo)
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.LOT_NO.ColNo, setDrs(i).Item("LOT_NO").ToString())
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.TOU_NO.ColNo, setDrs(i).Item("TOU_NO").ToString())
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, setDrs(i).Item("SITU_NO").ToString())
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, setDrs(i).Item("ZONE_CD").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.LOCA.ColNo, setDrs(i).Item("LOCA").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.NB.ColNo, setDrs(i).Item("KONSU").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.HASU.ColNo, Me._LMBconG.FormatNumValue(setDrs(i).Item("HASU").ToString()))
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.SUM.ColNo, Me._LMBconG.FormatNumValue(setDrs(i).Item("KOSU_S").ToString()))
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.IRIME.ColNo, setDrs(i).Item("IRIME").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.TANI.ColNo, setDrs(i).Item("STD_IRIME_NM").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.SURYO.ColNo, setDrs(i).Item("SURYO_S").ToString())
                ''START YANAI 運送・運行・請求メモNo.48
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, Me.RoundDown(setDrs(i).Item("JURYO_S").ToString(), LMB020C.JURYO_ROUND_POS))
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, Convert.ToString(Me.ToRound(Convert.ToDecimal(setDrs(i).Item("JURYO_S").ToString()), LMB020C.JURYO_ROUND_POS)))
                ''END YANAI 運送・運行・請求メモNo.48
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(setDrs(i).Item("LT_DATE").ToString()))
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.REMARK.ColNo, setDrs(i).Item("REMARK").ToString())
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.REMARK_OUT.ColNo, setDrs(i).Item("REMARK_OUT").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_1.ColNo, setDrs(i).Item("GOODS_COND_KB_1").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_2.ColNo, setDrs(i).Item("GOODS_COND_KB_2").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_COND_KB_3.ColNo, setDrs(i).Item("GOODS_COND_KB_3").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.OFB_KBN.ColNo, setDrs(i).Item("OFB_KB").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.SPD_KBN_S.ColNo, setDrs(i).Item("SPD_KB").ToString())
                ''.SetCellValue(rowCnt, LMB020G.sprDetailDef.SERIAL_NO.ColNo, setDrs(i).Item("SERIAL_NO").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(setDrs(i).Item("GOODS_CRT_DATE").ToString()))
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.DEST_CD.ColNo, setDrs(i).Item("DEST_CD").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.DEST_NM.ColNo, setDrs(i).Item("DEST_NM").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo, setDrs(i).Item("ALLOC_PRIORITY").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo, setDrs(i).Item("LOT_CTL_KB").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo, setDrs(i).Item("LT_DATE_CTL_KB").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo, setDrs(i).Item("CRT_DATE_CTL_KB").ToString())
                .SetCellValue(rowCnt, LMB020G.sprDetailDef.RECNO.ColNo, Me.SetZeroData(i.ToString(), LMB020C.MAEZERO))
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.STD_WT.ColNo, setDrs(i).Item("STD_WT_KGS").ToString())

                ''2013.07.16 追加START①
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CD_NRS.ColNo, setDrs(i).Item("GOODS_CD_NRS").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.GOODS_CD_CUST.ColNo, setDrs(i).Item("GOODS_CD_CUST").ToString())
                ''2013.07.16 追加END①

                'エラー設定
                Call Me.SetErrCell(spr, errDt, inkaSNo, errNo, rowCnt)

            Next

            .ResumeLayout(True)

        End With

    End Sub

#Region "スプレッド(明細)にCSV設定"

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInkaMData_CSV(ByVal ds As DataSet)

        '値のクリア
        Call Me.ClearInkaMControl()

        Dim spr As LMSpread = Me._Frm.sprGoodsDef

        'SPREAD(表示行)初期化
        spr.CrearSpread()

        'セルに設定するスタイルの取得
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sNum As StyleInfo = Me.StyleInfoNum9dec3(spr, True)
        Dim sKosu As StyleInfo = Me.StyleInfoNum10(spr, True)
        Dim sSuryo As StyleInfo = Me.StyleInfoNum12dec3(spr, True)
        Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
        Dim nLabel As StyleInfo = Me.StyleInfoLabel(spr, CellHorizontalAlignment.Right)

        Dim rowCnt As Integer = 0
        Dim setDt As DataTable = ds.Tables(LMB020C.TABLE_NM_INKA_M)
        Dim max As Integer = setDt.Rows.Count - 1

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To max

                'SYS_DEL_FLGが'1'のものは表示しない
                If LMConst.FLG.ON.Equals(setDt.Rows(i).Item("SYS_DEL_FLG").ToString()) = True Then
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.SORT_NO.ColNo, nLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.KANRI_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.GOODS_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.ALL_KOSU.ColNo, sKosu)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.ALL_SURYO.ColNo, sSuryo)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.GOODS_COMMENT.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.ORDER_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.SAGYO_UMU.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMB020G.sprGoodsDef.RECNO.ColNo, sLabel)

                '値設定
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.DEF.ColNo, False.ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.SORT_NO.ColNo, setDt.Rows(i).Item("PRINT_SORT").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.KANRI_NO.ColNo, setDt.Rows(i).Item("INKA_NO_M").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.GOODS_CD.ColNo, setDt.Rows(i).Item("GOODS_CD_CUST").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.GOODS_NM.ColNo, setDt.Rows(i).Item("GOODS_NM").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.ALL_KOSU.ColNo, setDt.Rows(i).Item("SUM_KOSU").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.ALL_SURYO.ColNo, setDt.Rows(i).Item("SUM_SURYO_M").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.HIKIATE_STATE.ColNo, setDt.Rows(i).Item("HIKIATE").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.GOODS_COMMENT.ColNo, setDt.Rows(i).Item("REMARK").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.ORDER_NO.ColNo, setDt.Rows(i).Item("OUTKA_FROM_ORD_NO_M").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprGoodsDef.SAGYO_UMU.ColNo, setDt.Rows(i).Item("SAGYO_UMU").ToString())
                .SetCellValue(rowCnt, LMB020G.sprGoodsDef.RECNO.ColNo, Me.SetZeroData(i.ToString(), LMB020C.MAEZERO))

            Next

            .ResumeLayout(True)

        End With

    End Sub


    ''' <summary>
    ''' 明細Sのスプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInkaSData_CSV(ByVal dt As DataTable, ByVal ds As DataSet, ByVal inkaMNo As String)

        'Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        'Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim spr As LMSpread = Me._Frm.sprDetail
        'Dim spr2 As LMSpreadSearch = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        'ロック制御
        Dim lock As Boolean = True

        'csv対応
        Dim csvDt As DataTable = ds.Tables(LMB020C.TABLE_NM_CSV_DATA)

        Dim csvMax As Integer = csvDt.Rows.Count
        Dim intSno As Integer = Convert.ToInt32("000")
        Dim strSno As String


        With spr

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = csvDt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            'Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True)

            '追加20130215
            'セルに設定するスタイルの取得
            Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLot As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 40, True)
            Dim sTou As StyleInfo = Me.StyleInfoTextHankaku(spr, 2, lock)
            'START YANAI 要望番号705
            'Dim sShituZone As StyleInfo = Me.StyleInfoTextHankaku(spr, 1, lock)
            Dim sShituZone As StyleInfo = Me.StyleInfoTextHankaku(spr, 2, lock)
            'END YANAI 要望番号705
            Dim sLoc As StyleInfo = Me.StyleInfoTextMixImeOff(spr, 10, lock)
            Dim sKon As StyleInfo = Me.StyleInfoNum10(spr, lock)
            Dim sHasu As StyleInfo = Me.StyleInfoNum10(spr, lock)

            '入数が1の場合、端数をロック
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = False AndAlso Me._Frm.numIrisu.TextValue.Equals("1") = True Then
                sHasu = Me.StyleInfoNum10(spr, True)
            End If

            Dim sKosu As StyleInfo = Me.StyleInfoNum10(spr, True)
            Dim sIrime As StyleInfo = Me.StyleInfoNum6dec3(spr, lock)
            Dim s9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr, True)

            '(2012.09.28)要望場号1463 IME設定をALL-MIXに変更 -- START --
            'Dim sMix As StyleInfo = Me.StyleInfoTextHankaku(spr, 100, lock)
            'Dim sTxt As StyleInfo = Me.StyleInfoTextHankaku(spr, 15, lock)
            Dim sMix As StyleInfo = Me.StyleInfoTextMix(spr, 100, lock)
            Dim sTxt As StyleInfo = Me.StyleInfoTextMix(spr, 15, lock)
            '(2012.09.28)要望場号1463 IME設定をALL-MIXに変更 --  END  --

            Dim sCmbGoodsIn As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_S005, lock)
            Dim sCmbGoodsOut As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_S006, lock)
            Dim sCmbBogai As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_B002, lock)
            Dim sCmbHoryu As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_H003, lock)
            Dim sCmbYusen As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_W001, lock)
            Dim sCustM As StyleInfo = Me.StyleInfoCustCond(spr, ds, lock)
            Dim sDate As StyleInfo = Me.StyleInfoDate(spr, lock)

            Dim rowCnt As Integer = 0
            'Dim setDrs As DataRow() = ds.Tables(LMB020C.TABLE_NM_INKA_S).Select(Me.SetInkaSSql(inkaMNo), "INKA_NO_S")
            Dim setDrs As DataRow() = ds.Tables(LMB020C.TABLE_NM_CSV_DATA).Select '(Me.SetInkaSSql(inkaMNo), "INKA_NO_S")
            'Dim max As Integer = setDrs.Length - 1
            Dim max As Integer = setDrs.Length - 1
            Dim errDt As DataTable = ds.Tables(LMB020C.TABLE_NM_ERR)
            Dim errNo As String = String.Empty
            If 0 < errDt.Rows.Count Then
                errNo = errDt(0).Item("INKA_S_NO").ToString()
            End If
            Dim inkaSNo As String = String.Empty

            Dim okiba As String = String.Empty
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty


            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            'For i As Integer = 1 To lngcnt
            For i As Integer = 0 To max

                'dr = dt.Rows(i - 1)

                ''セルスタイル設定
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.LOT_NO.ColNo, sLot)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.TOU_NO.ColNo, sTou)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, sShituZone)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, sShituZone)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.LOCA.ColNo, sLoc)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.NB.ColNo, sKon)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.HASU.ColNo, sHasu)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.SUM.ColNo, sKosu)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.IRIME.ColNo, sIrime)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.TANI.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.SURYO.ColNo, s9dec3)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.JURYO.ColNo, s9dec3)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.LT_DATE.ColNo, sDate)
                .SetCellStyle(rowCnt, LMB020G.sprDetailDef.ENT_PHOTO.ColNo, sLabel) 'ADD 2022/11/07 倉庫写真アプリ対応
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.REMARK.ColNo, sMix)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.REMARK_OUT.ColNo, sTxt)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.GOODS_COND_KB_1.ColNo, sCmbGoodsIn)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.GOODS_COND_KB_2.ColNo, sCmbGoodsOut)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.GOODS_COND_KB_3.ColNo, sCustM)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.OFB_KBN.ColNo, sCmbBogai)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.SPD_KBN_S.ColNo, sCmbHoryu)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.SERIAL_NO.ColNo, sLot)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.GOODS_CRT_DATE.ColNo, sDate)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.DEST_CD.ColNo, sTxt)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo, sCmbYusen)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.LOT_CTL_KB.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.LT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.CRT_DATE_CTL_KB.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.RECNO.ColNo, sLabel)
                .SetCellStyle(lngcnt, LMB020G.sprDetailDef.STD_WT.ColNo, s9dec3)

                'セルに値を設定
                '値設定
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.DEF.ColNo, False.ToString())
                'inkaSNo = setDrs(i).Item("INKA_NO_S").ToString()

                'CSVデータ追加
                intSno = intSno + 1
                strSno = intSno.ToString()
                Select Case strSno.Length
                    Case 1
                        strSno = "00" + strSno
                    Case 2
                        strSno = "0" + strSno
                End Select


                '置場の棟室ゾーンの分離は暫定
                okiba = setDrs(i).Item("LOC").ToString()
                touNo = okiba.Substring(0, 2)
                situNo = okiba.Substring(1, 1)
                zoneCd = okiba.Substring(2, 1)
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, inkaSNo)
                .SetCellValue(i, LMB020G.sprDetailDef.KANRI_NO_S.ColNo, strSno)
                .SetCellValue(i, LMB020G.sprDetailDef.LOT_NO.ColNo, setDrs(i).Item("LOT_NO").ToString())
                '置場(OKIBA)ここから
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.TOU_NO.ColNo, setDrs(i).Item("TOU_NO").ToString())
                .SetCellValue(i, LMB020G.sprDetailDef.TOU_NO.ColNo, touNo)
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.SHITSU_NO.ColNo, setDrs(i).Item("SITU_NO").ToString())
                .SetCellValue(i, LMB020G.sprDetailDef.SHITSU_NO.ColNo, situNo)
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.ZONE_CD.ColNo, setDrs(i).Item("ZONE_CD").ToString())
                .SetCellValue(i, LMB020G.sprDetailDef.ZONE_CD.ColNo, zoneCd)
                '置場(OKIBA)ここまで
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.LOCA.ColNo, setDrs(i).Item("LOCA").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.NB.ColNo, setDrs(i).Item("KONSU").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.HASU.ColNo, Me._LMBconG.FormatNumValue(setDrs(i).Item("HASU").ToString()))
                .SetCellValue(i, LMB020G.sprDetailDef.HASU.ColNo, "0".ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.SUM.ColNo, Me._LMBconG.FormatNumValue(setDrs(i).Item("KOSU_S").ToString()))
                .SetCellValue(i, LMB020G.sprDetailDef.SUM.ColNo, "0".ToString())
                .SetCellValue(i, LMB020G.sprDetailDef.IRIME.ColNo, setDrs(i).Item("IRIME").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.TANI.ColNo, setDrs(i).Item("STD_IRIME_NM").ToString())
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.SURYO.ColNo, setDrs(i).Item("SURYO_S").ToString())
                'START YANAI 運送・運行・請求メモNo.48
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, Me.RoundDown(setDrs(i).Item("JURYO_S").ToString(), LMB020C.JURYO_ROUND_POS))
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.JURYO.ColNo, Convert.ToString(Me.ToRound(Convert.ToDecimal(setDrs(i).Item("JURYO_S").ToString()), LMB020C.JURYO_ROUND_POS)))
                .SetCellValue(i, LMB020G.sprDetailDef.JURYO.ColNo, "0".ToString())
                'END YANAI 運送・運行・請求メモNo.48
                '.SetCellValue(rowCnt, LMB020G.sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(setDrs(i).Item("LT_DATE").ToString()))
                .SetCellValue(i, LMB020G.sprDetailDef.REMARK.ColNo, setDrs(i).Item("REMARK").ToString()) '☆
                .SetCellValue(i, LMB020G.sprDetailDef.REMARK_OUT.ColNo, setDrs(i).Item("REMARK_OUT").ToString()) '☆
                .SetCellValue(i, LMB020G.sprDetailDef.SERIAL_NO.ColNo, setDrs(i).Item("SERIAL_NO").ToString())
                .SetCellValue(i, LMB020G.sprDetailDef.RECNO.ColNo, Me.SetZeroData(i.ToString(), LMB020C.MAEZERO))

                .SetCellValue(i, LMB020G.sprDetailDef.OFB_KBN.ColNo, "01".ToString()) '簿外品
                .SetCellValue(i, LMB020G.sprDetailDef.SPD_KBN_S.ColNo, "01".ToString()) '保留品
                .SetCellValue(i, LMB020G.sprDetailDef.ALLOC_PRIORITY.ColNo, "10".ToString()) '割当優先
                '.SetCellValue(i, LMM060G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, dr.Item("UNCHIN_TARIFF_CD").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.DATA_TP_NM.ColNo, dr.Item("DATA_TP_NM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.TABLE_TP_NM.ColNo, dr.Item("TABLE_TP_NM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.STR_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("STR_DATE").ToString()))
                '.SetCellValue(i, LMM060G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, dr.Item("UNCHIN_TARIFF_CD2").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, dr.Item("UNCHIN_TARIFF_REM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.DATA_TP.ColNo, dr.Item("DATA_TP").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.TABLE_TP.ColNo, dr.Item("TABLE_TP").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                '.SetCellValue(i, LMM060G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With


    End Sub
#End Region '"スプレッド(明細)にCSV設定"

    ''' <summary>
    ''' エラーフォーカス設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="dt">DataTable</param>
    ''' <param name="inkaSNo">入荷(小)番号</param>
    ''' <param name="errNo">エラーの入荷(小)番号</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub SetErrCell(ByVal spr As LMSpread, ByVal dt As DataTable, ByVal inkaSNo As String, ByVal errNo As String, ByVal rowNo As Integer)

        'エラー情報がない場合、スルー
        If String.IsNullOrEmpty(errNo) = True Then
            Exit Sub
        End If

        'PKが違う場合、スルー
        If errNo.Equals(inkaSNo) = False Then
            Exit Sub
        End If

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            Call Me.SetErrorControl(spr, rowNo, Convert.ToInt32(dt.Rows(i).Item("COLNO").ToString()))

        Next

    End Sub

    ''' <summary>
    ''' INKA_Sの値取得SQL構築
    ''' </summary>
    ''' <param name="inkaNoM">INKA_NO_M</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Friend Function SetInkaSSql(ByVal inkaNoM As String) As String

        Return String.Concat("INKA_NO_M = '", inkaNoM, "' ")

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
    ''' 並び替え処理(Goods)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub sprGoodsSortColumnCommand()

        Me.sprSortColumnCommand(Me._Frm.sprGoodsDef, LMB020G.sprGoodsDef.RECNO.ColNo)

    End Sub

    ''' <summary>
    ''' 並び替え処理(Detail)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub sprDetailSortColumnCommand()

        Me.sprSortColumnCommand(Me._Frm.sprDetail, LMB020G.sprDetailDef.RECNO.ColNo)

    End Sub

    ''' <summary>
    ''' 並び替え処理
    ''' </summary>
    ''' <param name="spr">スプレッドシート</param>
    ''' <param name="colNo">ソート列</param>
    ''' <remarks></remarks>
    Private Sub sprSortColumnCommand(ByVal spr As LMSpread, ByVal colNo As Integer)

        spr.ActiveSheet.SortRows(colNo, True, False)

    End Sub

    Private Function RoundDown(ByVal strVal As String, ByVal iDigits As Integer) As String

        Dim pos As Integer = strVal.IndexOf(".")

        If pos < 0 Then
            Return strVal
        End If

        Return strVal.Substring(0, pos + 1).PadRight(pos + 1 + iDigits, "0"c)

    End Function

    'START YANAI 運送・運行・請求メモNo.48
    ''' <summary>
    ''' 数値の四捨五入
    ''' </summary>
    ''' <param name="value">四捨五入を行う数値</param>
    ''' <param name="value2">四捨五入後の数値の有効小数桁数</param>
    ''' <returns>四捨五入た数値</returns>
    ''' <remarks></remarks>
    Friend Function ToRound(ByVal value As Decimal, ByVal value2 As Integer) As Decimal

        Dim maxLength As Decimal = Convert.ToDecimal(System.Math.Pow(10, value2))

        If value > 0 Then
            'value値が0より大きい場合は、Ceilingを使用して四捨五入
            Return Convert.ToDecimal(System.Math.Floor((value * maxLength) + 0.5) / maxLength)
        Else
            'value値が0以下の場合は、Floorを使用して四捨五入
            Return Convert.ToDecimal(System.Math.Ceiling((value * maxLength) - 0.5) / maxLength)
        End If

    End Function
    'END YANAI 運送・運行・請求メモNo.48

#End Region 'Spread

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
    ''' セルのプロパティを設定(Date)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoDate(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        '日付スタイルを設定
        StyleInfoDate = LMSpreadUtility.GetDateTimeCell(spr, lock)

        '配置左に設定 
        StyleInfoDate.HorizontalAlignment = CellHorizontalAlignment.Left

        Return StyleInfoDate

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(TextHankaku)
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
    ''' セルのプロパティを設定(TextHankaku)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMixImeOff(ByVal spr As LMSpread, ByVal length As Integer, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, length, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数4桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum4(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数6桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum6(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999, lock, 0, , ",")

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
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, lock, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, lock, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(RecType)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoCombKbn(ByVal spr As LMSpread, ByVal kbn As String, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetComboCellKbn(spr, kbn, lock)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread, ByVal ds As DataSet, ByVal lock As Boolean) As StyleInfo

        Dim dr As DataRow = ds.Tables(LMB020C.TABLE_NM_INKA_L).Rows(0)

        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.CUSTCOND _
                                                  , "JOTAI_CD" _
                                                  , "JOTAI_NM" _
                                                  , lock _
                                                  , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                                                  , New String() {dr.Item("NRS_BR_CD").ToString(), dr.Item("CUST_CD_L").ToString()} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread, Optional ByVal alignment As CellHorizontalAlignment = CellHorizontalAlignment.Left) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, alignment)

    End Function

#End Region 'プロパティ

#Region "フォーカス設定"

    ''' <summary>
    ''' エラーコントロール設定(明細用)
    ''' </summary>
    ''' <param name="spr">スプレッドシート</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal spr As LMSpread, ByVal rowNo As Integer, ByVal colNo As Integer)

        With spr.ActiveSheet

            Me.MyForm.ActiveControl = spr
            spr.Focus()
            .SetActiveCell(rowNo, colNo)
            .Cells(rowNo, colNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()

        End With

    End Sub

#End Region 'フォーカス設定

#Region "コントロール取得"

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal objNm As String) As Win.InputMan.LMImTextBox

        Return DirectCast(Me._Frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)

    End Function

#End Region 'コントロール取得

#End Region '"ユーティリティ

#End Region '"Method"

End Class
