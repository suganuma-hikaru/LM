' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM040G : 届先マスタメンテナンス
'  作  成  者     : [金へスル]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.DSL
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李

''' <summary>
''' LMM040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM040F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM040V

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM040F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        'Validate共通クラスの設定
        _ControlV = New LMMControlV(handlerClass, DirectCast(frm, Form), _ControlG)

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim unLock As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LMMControlC.FUNCTION_F1_SHINKI
            .F2ButtonName = LMMControlC.FUNCTION_F2_HENSHU
            .F3ButtonName = LMMControlC.FUNCTION_F3_FUKUSHA
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = LMMControlC.FUNCTION_F5_DEST_IKKATU
            .F6ButtonName = String.Empty
            .F7ButtonName = LMMControlC.FUNCTION_F7_INSATSU
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能
            Dim F9lock As Boolean = unLock
            '初期モードで表示した時のロック制御
            If Me._Frm.lblModeFlg.TextValue = "1" Then
                F9lock = lock
            End If

            '常に使用不可キー
            .F6ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = F9lock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F5ButtonEnabled = view OrElse init
            'START EDIT 2013/09/10 KOBAYASHI WIT対応
            '.F7ButtonEnabled = lock
            .F7ButtonEnabled = view
            'END   EDIT 2013/09/10 KOBAYASHI WIT対応
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

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

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM040C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM040C.CtlTabIndex.NRSBRCD
            .txtCustCdL.TabIndex = LMM040C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMM040C.CtlTabIndex.CUSTNML
            .txtDestCd.TabIndex = LMM040C.CtlTabIndex.DESTCD
            .txtEDICd.TabIndex = LMM040C.CtlTabIndex.EDICD
            .txtDestNm.TabIndex = LMM040C.CtlTabIndex.DESTNM
            '要望番号:1330 terakawa 2012.08.09 Start
            .txtKanaNm.TabIndex = LMM040C.CtlTabIndex.KANANM
            '要望番号:1330 terakawa 2012.08.09 End
            .txtZip.TabIndex = LMM040C.CtlTabIndex.ZIP
            .txtAd1.TabIndex = LMM040C.CtlTabIndex.AD1
            .txtAd2.TabIndex = LMM040C.CtlTabIndex.AD2
            .txtAd3.TabIndex = LMM040C.CtlTabIndex.AD3
            .txtCustDestCd.TabIndex = LMM040C.CtlTabIndex.DICDESTCD
            '要望番号:1424② yamanaka 2012.09.20 Start
            .txtShiharaiAd.TabIndex = LMM040C.CtlTabIndex.SHIHARAI_AD
            '要望番号:1424② yamanaka 2012.09.20 End
            .txtTel.TabIndex = LMM040C.CtlTabIndex.TEL
            .txtJIS.TabIndex = LMM040C.CtlTabIndex.JIS
            .cmbSpNhsKb.TabIndex = LMM040C.CtlTabIndex.SPNHSKB
            .txtFax.TabIndex = LMM040C.CtlTabIndex.FAX
            .numKyori.TabIndex = LMM040C.CtlTabIndex.KYORI
            .cmbCoaYn.TabIndex = LMM040C.CtlTabIndex.COAYN
            .grpUnso.TabIndex = LMM040C.CtlTabIndex.UNSO
            .txtSpUnsoCd.TabIndex = LMM040C.CtlTabIndex.SPUNSOCD
            .txtSpUnsoBrCd.TabIndex = LMM040C.CtlTabIndex.SPUNSOBRCD
            .lblSpUnsoNm.TabIndex = LMM040C.CtlTabIndex.SPUNSONM
            .cmbPick.TabIndex = LMM040C.CtlTabIndex.PICK
            .cmbBin.TabIndex = LMM040C.CtlTabIndex.BIN
            .cmbMotoChakuKbn.TabIndex = LMM040C.CtlTabIndex.MOTOCHAKUKBN
            .txtCargoTimeLimit.TabIndex = LMM040C.CtlTabIndex.CARGOTIMELIMIT
            .cmbLargeCarYn.TabIndex = LMM040C.CtlTabIndex.LARGECARYN
            .txtDeliAtt.TabIndex = LMM040C.CtlTabIndex.DELIATT
            'START YANAI 要望番号881
            .txtRemark.TabIndex = LMM040C.CtlTabIndex.REMARK
            'END YANAI 要望番号881
            .txtSalesCd.TabIndex = LMM040C.CtlTabIndex.SALESCD
            .lblSalesNm.TabIndex = LMM040C.CtlTabIndex.SALESNM
            .txtUriageCd.TabIndex = LMM040C.CtlTabIndex.URIAGECD
            .lblUriageNm.TabIndex = LMM040C.CtlTabIndex.URIAGENM
            .txtUnchinSeiqtoCd.TabIndex = LMM040C.CtlTabIndex.UNCHINSEIQTOCD
            .lblUnchinSeiqtoNm.TabIndex = LMM040C.CtlTabIndex.UNCHINSEIQTONM
            .txtUnchinTariffCd1.TabIndex = LMM040C.CtlTabIndex.UNCHINTARIFFCD1
            .lblUnchinTariffRem1.TabIndex = LMM040C.CtlTabIndex.UNCHINTARIFFREM1
            .cmbTariffBunruiKbn.TabIndex = LMM040C.CtlTabIndex.TARIFFBUNRUIKBN
            .txtUnchinTariffCd2.TabIndex = LMM040C.CtlTabIndex.UNCHINTARIFFCD2
            .lblUnchinTariffRem2.TabIndex = LMM040C.CtlTabIndex.UNCHINTARIFFREM2
            .txtExtcTariffCd.TabIndex = LMM040C.CtlTabIndex.EXTCTARIFFCD
            .lblExtcTariffRem.TabIndex = LMM040C.CtlTabIndex.EXTCTARIFFREM
            .txtYokoTariffCd.TabIndex = LMM040C.CtlTabIndex.YOKOTARIFFCD
            .lblYokoTariffRem.TabIndex = LMM040C.CtlTabIndex.YOKOTARIFFREM
            .sprDetail2.TabIndex = LMM040C.CtlTabIndex.DETAIL2
            .lblSituation.TabIndex = LMM040C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM040C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM040C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM040C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM040C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM040C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM040C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' コンボボックス作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateComboBox()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        '区分マスタ検索処理
        Dim sort As String = "KBN_CD"
        Dim Value As String = "1"
        Dim delFlg As String = "0"
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_T015, "'"), sort)

        Dim max As Integer = getDr.Count - 2

        Me._Frm.cmbTariffBunruiKbn.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

        For i As Integer = 0 To max


            cd = getDr(i).Item("KBN_CD").ToString()

            '2017/09/25 修正 李↓
            item = getDr(i).Item(lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})).ToString()
            '2017/09/25 修正 李↑

            Me._Frm.cmbTariffBunruiKbn.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        With Me._Frm
            If (LMConst.FLG.ON).Equals(.lblModeFlg.TextValue) = False Then
                .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            End If
            .cmbLargeCarYn.SelectedValue = LMM040C.LARGECAR

        End With

    End Sub

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            '編集部
            .numKyori.SetInputFields("##,##0", , 5, 1, , 0, 0, , Convert.ToDecimal(99999), Convert.ToDecimal(0))

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    'タブ内の表示内容を初期化する
                    Call Me.ClearControl(Me._Frm)
                    Me._Frm.sprDetail2.CrearSpread()
                Case DispMode.VIEW
                    'スプレッド(下部)をロックする
                    Me.SetLockBottomSpreadControl(True)

                Case DispMode.EDIT

                    'ボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel, unLock)

                    '常にロック項目ロック制御
                    Call Me._ControlG.SetLockControl(.cmbNrsBrCd, lock)

                    Select Case .lblSituation.RecordStatus
                        '正常
                        Case RecordStatus.NOMAL_REC
                            '編集時ロック制御を行う
                            Call Me.LockControlEdit()

                        Case RecordStatus.NEW_REC
                            '新規時項目のクリアを行う
                            Call Me.ClearControlNew()

                        Case RecordStatus.COPY_REC
                            '複写時キー項目のクリアを行う
                            Call Me.ClearControlFukusha()

                            '届先明細情報Spreadの隠し項目である"届先コード枝番"と"更新区分"の設定
                            Dim RowCnt As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                Dim destCdEda As String = _ControlG.SetMaeZeroData(Convert.ToString(i), 2)
                                '届先コード枝番："00"から連番で設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.DEST_CD_EDA.ColNo, destCdEda)
                                '更新区分："0"を設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")

                            Next

                    End Select

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        Dim lock As Boolean = True
        Dim Unlock As Boolean = False

        '画面項目を全ロック解除する
        Call Me._ControlG.SetLockControl(Me._Frm, Unlock)

        '新規時はロックする
        Call Me._ControlG.SetLockControl(Me._Frm.cmbNrsBrCd, lock)

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit()

        Dim lock As Boolean = True
        Dim Unlock As Boolean = False

        '画面項目を全ロック解除する
        Call Me._ControlG.SetLockControl(Me._Frm, Unlock)
        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)

        With Me._Frm

            '編集時はロックする
            Call Me._ControlG.SetLockControl(.cmbNrsBrCd, lock)
            Call Me._ControlG.SetLockControl(.txtCustCdL, lock)
            Call Me._ControlG.SetLockControl(.txtDestCd, lock)
            '届先コード枝番を初期化する
            Me._Frm.lblMaxEda.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        Dim lock As Boolean = True
        Dim Unlock As Boolean = False

        '画面項目を全ロック解除する
        Call Me._ControlG.SetLockControl(Me._Frm, Unlock)
        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)
        '複写時はロックする
        Call Me._ControlG.SetLockControl(Me._Frm.cmbNrsBrCd, Lock)

        With Me._Frm

            '2011.08.25 検証結果一覧№28対応 START
            ''複写しない項目は空を設定
            '.txtDestCd.TextValue = String.Empty
            '.txtZip.TextValue = String.Empty
            '.txtAd1.TextValue = String.Empty
            '.txtAd2.TextValue = String.Empty
            '.txtAd3.TextValue = String.Empty
            '.txtTel.TextValue = String.Empty
            '.txtJIS.TextValue = String.Empty
            '.txtFax.TextValue = String.Empty
            '.numKyori.TextValue = String.Empty
            '2011.08.25 検証結果一覧№28対応 END
            .lblMaxEda.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 項目のクリア処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

        With Me._Frm

            '数値項目に初期値0を設定する            
            .numKyori.Value = 0
            'スプレッド(下部)のクリア
            .sprDetail2.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' スプレッド(下部)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockBottomSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            Dim max As Integer = .sprDetail2.ActiveSheet.Rows.Count
            For i As Integer = 1 To max
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, lockFlg))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.SUB_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail2, LMKbnConst.KBN_Y006, lockFlg))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.SET_NAIYO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail2, InputControl.ALL_MIX, 1000, lockFlg))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail2, InputControl.ALL_MIX, 100, lockFlg))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEST_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.UPD_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
                .sprDetail2.SetCellStyle((i - 1), LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            Next

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM040C.EventShubetsu)
        With Me._Frm
            Select Case eventType
                Case LMM040C.EventShubetsu.MAIN, LMM040C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM040C.EventShubetsu.SHINKI, LMM040C.EventShubetsu.HUKUSHA
                    .txtCustCdL.Focus()
                Case LMM040C.EventShubetsu.HENSHU
                    .txtEDICd.Focus()
                Case LMM040C.EventShubetsu.INS_T
                    .sprDetail2.Focus()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtCustCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.CUST_CD_L.ColNo).Text
            .lblCustNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.CUST_NM_L.ColNo).Text
            .txtDestCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.DEST_CD.ColNo).Text
            '要望番号:1330 terakawa 2012.08.09 Start
            .txtKanaNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.KANA_NM.ColNo).Text
            '要望番号:1330 terakawa 2012.08.09 End
            .txtEDICd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.EDI_CD.ColNo).Text
            .txtDestNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.DEST_NM.ColNo).Text
            .txtZip.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.ZIP.ColNo).Text
            .txtAd1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.AD.ColNo).Text
            .txtAd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.AD_2.ColNo).Text
            .txtAd3.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.AD_3.ColNo).Text
            .txtCustDestCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.CUST_DEST_CD.ColNo).Text
            '要望番号:1424② yamanaka 2012.09.20 Start
            .txtShiharaiAd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SHIHARAI_AD.ColNo).Text
            '要望番号:1424② yamanaka 2012.09.20 End
            .txtTel.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.TEL.ColNo).Text
            .txtJIS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.JIS.ColNo).Text
            .cmbSpNhsKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SP_NHS_KB.ColNo).Text
            .txtFax.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.FAX.ColNo).Text
            .numKyori.Value = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.KYORI.ColNo).Text
            .cmbCoaYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.COA_YN.ColNo).Text
            .txtSpUnsoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SP_UNSO_CD.ColNo).Text
            .txtSpUnsoBrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SP_UNSO_BR_CD.ColNo).Text
            .lblSpUnsoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SP_UNSO_BR_NM.ColNo).Text
            .cmbPick.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.PIC_KB.ColNo).Text
            .cmbBin.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.BIN_KB.ColNo).Text
            .cmbMotoChakuKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.MOTO_CHAKU_KB.ColNo).Text
            .txtCargoTimeLimit.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.CARGO_TIME_LIMIT.ColNo).Text
            .cmbLargeCarYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.LARGE_CAR_YN.ColNo).Text
            .txtDeliAtt.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.DELI_ATT.ColNo).Text
            'START YANAI 要望番号881
            .txtRemark.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.REMARK.ColNo).Text
            'END YANAI 要望番号881
            .txtSalesCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SALES_CD.ColNo).Text
            .lblSalesNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SALES_NM.ColNo).Text
            .txtUriageCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.URIAGE_CD.ColNo).Text
            .lblUriageNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.URIAGE_NM.ColNo).Text
            .txtUnchinSeiqtoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.UNCHIN_SEIQTO_CD.ColNo).Text
            .lblUnchinSeiqtoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.UNCHIN_SEIQTO_NM.ColNo).Text
            .txtUnchinTariffCd1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo).Text       '運賃ﾀﾘﾌｾｯﾄ情報
            .lblUnchinTariffRem1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM1.ColNo).Text      '運賃ﾀﾘﾌｾｯﾄ情報
            .cmbTariffBunruiKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.TARIFF_BUNRUI_KB.ColNo).Text    '運賃ﾀﾘﾌｾｯﾄ情報
            .txtUnchinTariffCd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo).Text       '運賃ﾀﾘﾌｾｯﾄ情報
            .lblUnchinTariffRem2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM2.ColNo).Text      '運賃ﾀﾘﾌｾｯﾄ情報
            .txtExtcTariffCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Text             '運賃ﾀﾘﾌｾｯﾄ情報
            .lblExtcTariffRem.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.EXTC_TARIFF_NM.ColNo).Text            '運賃ﾀﾘﾌｾｯﾄ情報
            .txtYokoTariffCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.YOKO_TARIFF_CD.ColNo).Text             '運賃ﾀﾘﾌｾｯﾄ情報
            .lblYokoTariffRem.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.YOKO_TARIFF_NM.ColNo).Text            '運賃ﾀﾘﾌｾｯﾄ情報
            .lblSysUpdDateT.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_UPD_DATE_T.ColNo).Text              '運賃ﾀﾘﾌｾｯﾄ情報
            .lblSysUpdTimeT.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_UPD_TIME_T.ColNo).Text              '運賃ﾀﾘﾌｾｯﾄ情報
            .lblSetKbn.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SET_KB.ColNo).Text                           '運賃ﾀﾘﾌｾｯﾄ情報
            .lblSetMstCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SET_MST_CD.ColNo).Text                     '運賃ﾀﾘﾌｾｯﾄ情報
            .lblCustCdM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.CUST_CD_M.ColNo).Text                       '運賃ﾀﾘﾌｾｯﾄ情報

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo).Text



        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds.Tables(LMM040C.TABLE_NM_OUT))

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)     '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)           '営業所名
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 100, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 190, True)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.DEST_CD, "届先コード", 100, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.DEST_NM, "届先名", 180, True)
        Public Shared AD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.AD, "住所", 140, True)
        Public Shared TEL As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.TEL, "電話番号", 120, True)

        '隠し項目
        Public Shared EDI_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.EDI_CD, "EDI届先コード", 60, False)
        '要望番号:1330 terakawa 2012.08.09 Start
        Public Shared KANA_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.KANA_NM, "カナ名", 60, False)
        '要望番号:1330 terakawa 2012.08.09 End

        Public Shared ZIP As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.ZIP, "郵便番号", 60, False)
        Public Shared AD_2 As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.AD_2, "住所2", 60, False)
        Public Shared AD_3 As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.AD_3, "住所3", 60, False)
        Public Shared CUST_DEST_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.CUST_DEST_CD, "顧客運賃纏めコード", 60, False)
        Public Shared SALES_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SALES_CD, " 納品書荷主名義コード", 60, False)
        Public Shared SALES_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SALES_NM, "納品書荷主名義名", 60, False)
        Public Shared SP_NHS_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SP_NHS_KB, "指定納品書添付区分", 60, False)
        Public Shared COA_YN As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.COA_YN, "分析表添付区分", 60, False)
        Public Shared SP_UNSO_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SP_UNSO_CD, "指定運送会社コード", 60, False)
        Public Shared SP_UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SP_UNSO_BR_CD, "指定運送会社支店コード", 60, False)
        Public Shared SP_UNSO_BR_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SP_UNSO_BR_NM, "指定運送会社名", 60, False)
        Public Shared DELI_ATT As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.DELI_ATT, "配送時注意事項", 60, False)
        'START YANAI 要望番号881
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.REMARK, "備考", 60, False)
        'END YANAI 要望番号881
        '要望番号:1424② yamanaka 2012.09.20 Start
        Public Shared SHIHARAI_AD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SHIHARAI_AD, "支払用住所", 60, False)
        '要望番号:1424② yamanaka 2012.09.20 End
        Public Shared CARGO_TIME_LIMIT As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.CARGO_TIME_LIMIT, "荷卸時間制限", 60, False)
        Public Shared LARGE_CAR_YN As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.LARGE_CAR_YN, "大型車輛可", 60, False)
        Public Shared FAX As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.FAX, "ファックス番号", 60, False)
        Public Shared UNCHIN_SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.UNCHIN_SEIQTO_CD, "運賃請求先コード", 60, False)
        Public Shared UNCHIN_SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.UNCHIN_SEIQTO_NM, "運賃請求先名", 60, False)
        Public Shared JIS As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.JIS, "JISコード", 60, False)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.KYORI, "指定距離", 60, False)
        Public Shared PIC_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.PIC_KB, "ピッキングリスト区分", 60, False)
        Public Shared BIN_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.BIN_KB, "便区分", 60, False)
        Public Shared MOTO_CHAKU_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.MOTO_CHAKU_KB, "元着払区分", 60, False)
        Public Shared URIAGE_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.URIAGE_CD, "売上先コード", 60, False)
        Public Shared URIAGE_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.URIAGE_NM, "売上先名", 60, False)
        Public Shared TARIFF_BUNRUI_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.TARIFF_BUNRUI_KB, "タリフ分類区分", 60, False)
        Public Shared UNCHIN_TARIFF_CD1 As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.UNCHIN_TARIFF_CD1, "運賃タリフコード１", 60, False)
        Public Shared UNCHIN_TARIFF_NM1 As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.UNCHIN_TARIFF_NM1, "運賃タリフ備考１", 60, False)
        Public Shared UNCHIN_TARIFF_CD2 As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.UNCHIN_TARIFF_CD2, "運賃タリフコード２", 60, False)
        Public Shared UNCHIN_TARIFF_NM2 As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.UNCHIN_TARIFF_NM2, "運賃タリフ備考２", 60, False)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.EXTC_TARIFF_CD, "割増運賃タリフコード", 60, False)
        Public Shared EXTC_TARIFF_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.EXTC_TARIFF_NM, "割増運賃タリフ備考", 60, False)
        Public Shared YOKO_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.YOKO_TARIFF_CD, "横持ち運賃タリフコード", 60, False)
        Public Shared YOKO_TARIFF_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.YOKO_TARIFF_NM, "横持ち運賃タリフ備考", 60, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.CUST_CD_M, "荷主コード(中)", 60, False)
        Public Shared SET_MST_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SET_MST_CD, "セットマスタコード", 60, False)
        Public Shared SET_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SET_KB, "セット区分", 60, False)
        Public Shared SYS_UPD_DATE_T As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_UPD_DATE_T, "更新日", 60, False)
        Public Shared SYS_UPD_TIME_T As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_UPD_TIME_T, "更新時刻", 60, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)


    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.DEF, " ", 20, True)
        Public Shared DEST_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.DEST_CD_EDA, "枝番", 60, True)
        Public Shared SUB_KB As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.SUB_KB, "用途区分", 150, True)
        Public Shared SET_NAIYO As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.SET_NAIYO, "設定値", 700, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.REMARK, "備考", 240, True)
        '隠し項目
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.DEST_CD, "届先コード", 150, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.CUST_CD_L, "荷主コード(大)", 150, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM040C.SprColumnIndex2.SYS_DEL_FLG_T, "削除フラグ", 150, False)


    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()
        Dim dr As DataRow
        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()
            .sprDetail2.CrearSpread()

            '列数設定
            '要望番号:1330 terakawa 2012.08.09 Start
            'START YANAI 要望番号881
            '.sprDetail.Sheets(0).ColumnCount = 55
            '.sprDetail.Sheets(0).ColumnCount = 56
            .sprDetail.Sheets(0).ColumnCount = LMM040C.SprColumnIndex.COLMUN_COUNT
            'END YANAI 要望番号881
            '要望番号:1330 terakawa 2012.08.09 End
            .sprDetail2.Sheets(0).ColumnCount = 9

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM040G.sprDetailDef())
            '.sprDetail2.SetColProperty(New LMM040G.sprDetailDef2())

            '20151026 tsunehira add 英語対応
            .sprDetail.SetColProperty(New LMM040G.sprDetailDef(), False)
            .sprDetail2.SetColProperty(New LMM040G.sprDetailDef2(), False)
            '20151026 tsunehira end 英語対応

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM040G.sprDetailDef.DEST_NM.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If

            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.DEST_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 15, False))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 80, False))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.AD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.TEL.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.EDI_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '要望番号:1330 terakawa 2012.08.09 Start
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.KANA_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '要望番号:1330 terakawa 2012.08.09 End
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.ZIP.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.AD_2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.AD_3.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.CUST_DEST_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SALES_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SALES_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SP_NHS_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.COA_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SP_UNSO_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SP_UNSO_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SP_UNSO_BR_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.DELI_ATT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'START YANAI 要望番号881
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'END YANAI 要望番号881
            '要望番号:1424② yamanaka 2012.09.20 Start
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SHIHARAI_AD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '要望番号:1424② yamanaka 2012.09.20 End
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.CARGO_TIME_LIMIT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.LARGE_CAR_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.FAX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.UNCHIN_SEIQTO_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.UNCHIN_SEIQTO_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.JIS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.KYORI.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.PIC_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.BIN_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.MOTO_CHAKU_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.URIAGE_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.URIAGE_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.TARIFF_BUNRUI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.EXTC_TARIFF_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.EXTC_TARIFF_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.YOKO_TARIFF_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.YOKO_TARIFF_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SET_MST_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SET_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_UPD_DATE_T.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_UPD_TIME_T.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            '列設定（下部）
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, False))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.SUB_KB.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail2, LMKbnConst.KBN_Y006, False))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.SET_NAIYO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail2, InputControl.ALL_MIX, 1000, False))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail2, InputControl.ALL_MIX, 100, False))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.DEST_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.UPD_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM040F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.NRS_BR_NM.ColNo).Value = LMUserInfoManager.GetNrsBrCd.ToString()
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.CUST_CD_L.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.CUST_NM_L.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.DEST_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.AD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.TEL.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.EDI_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.ZIP.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.AD_2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.AD_3.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.CUST_DEST_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SALES_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SALES_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SP_NHS_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.COA_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SP_UNSO_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SP_UNSO_BR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SP_UNSO_BR_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.DELI_ATT.ColNo).Value = String.Empty
            'START YANAI 要望番号881
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.REMARK.ColNo).Value = String.Empty
            'END YANAI 要望番号881
            '要望番号:1424② yamanaka 2012.09.20 Start
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SHIHARAI_AD.ColNo).Value = String.Empty
            '要望番号:1424② yamanaka 2012.09.20 End
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.CARGO_TIME_LIMIT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.LARGE_CAR_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.FAX.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.UNCHIN_SEIQTO_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.UNCHIN_SEIQTO_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.JIS.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.KYORI.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.PIC_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.BIN_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.MOTO_CHAKU_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.URIAGE_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.URIAGE_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.TARIFF_BUNRUI_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM2.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.EXTC_TARIFF_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.YOKO_TARIFF_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.YOKO_TARIFF_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.CUST_CD_M.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SET_MST_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SET_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_UPD_DATE_T.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_UPD_TIME_T.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM040G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.DEST_NM.ColNo, sLabel)
                '要望番号:1330 terakawa 2012.08.09 Start
                .SetCellStyle(i, LMM040G.sprDetailDef.KANA_NM.ColNo, sLabel)
                '要望番号:1330 terakawa 2012.08.09 End
                .SetCellStyle(i, LMM040G.sprDetailDef.AD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.TEL.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.EDI_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.ZIP.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.AD_2.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.AD_3.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.CUST_DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SALES_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SALES_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SP_NHS_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.COA_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SP_UNSO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SP_UNSO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SP_UNSO_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.DELI_ATT.ColNo, sLabel)
                'START YANAI 要望番号881
                .SetCellStyle(i, LMM040G.sprDetailDef.REMARK.ColNo, sLabel)
                'END YANAI 要望番号881
                '要望番号:1424② yamanaka 2012.09.20 Start
                .SetCellStyle(i, LMM040G.sprDetailDef.SHIHARAI_AD.ColNo, sLabel)
                '要望番号:1424② yamanaka 2012.09.20 End
                .SetCellStyle(i, LMM040G.sprDetailDef.CARGO_TIME_LIMIT.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.LARGE_CAR_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.FAX.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.UNCHIN_SEIQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.UNCHIN_SEIQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.JIS.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.KYORI.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.PIC_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.BIN_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.MOTO_CHAKU_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.URIAGE_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.URIAGE_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.TARIFF_BUNRUI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM1.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM2.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.EXTC_TARIFF_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.YOKO_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.YOKO_TARIFF_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SET_MST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SET_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_UPD_DATE_T.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_UPD_TIME_T.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM040G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                '要望番号:1330 terakawa 2012.08.09 Start
                .SetCellValue(i, LMM040G.sprDetailDef.KANA_NM.ColNo, dr.Item("KANA_NM").ToString())
                '要望番号:1330 terakawa 2012.08.09 End
                .SetCellValue(i, LMM040G.sprDetailDef.AD.ColNo, dr.Item("AD_1").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.TEL.ColNo, dr.Item("TEL").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.EDI_CD.ColNo, dr.Item("EDI_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.ZIP.ColNo, dr.Item("ZIP").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.AD_2.ColNo, dr.Item("AD_2").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.AD_3.ColNo, dr.Item("AD_3").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.CUST_DEST_CD.ColNo, dr.Item("CUST_DEST_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SALES_CD.ColNo, dr.Item("SALES_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SALES_NM.ColNo, dr.Item("SALES_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SP_NHS_KB.ColNo, dr.Item("SP_NHS_KB").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.COA_YN.ColNo, dr.Item("COA_YN").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SP_UNSO_CD.ColNo, dr.Item("SP_UNSO_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SP_UNSO_BR_CD.ColNo, dr.Item("SP_UNSO_BR_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SP_UNSO_BR_NM.ColNo, dr.Item("SP_UNSO_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.DELI_ATT.ColNo, dr.Item("DELI_ATT").ToString())
                'START YANAI 要望番号881
                .SetCellValue(i, LMM040G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                'END YANAI 要望番号881
                '要望番号:1424② yamanaka 2012.09.20 Start
                .SetCellValue(i, LMM040G.sprDetailDef.SHIHARAI_AD.ColNo, dr.Item("SHIHARAI_AD").ToString())
                '要望番号:1424② yamanaka 2012.09.20 End
                .SetCellValue(i, LMM040G.sprDetailDef.CARGO_TIME_LIMIT.ColNo, dr.Item("CARGO_TIME_LIMIT").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.LARGE_CAR_YN.ColNo, dr.Item("LARGE_CAR_YN").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.FAX.ColNo, dr.Item("FAX").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.UNCHIN_SEIQTO_CD.ColNo, dr.Item("UNCHIN_SEIQTO_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.UNCHIN_SEIQTO_NM.ColNo, dr.Item("UNCHIN_SEIQTO_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.JIS.ColNo, dr.Item("JIS").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.KYORI.ColNo, dr.Item("KYORI").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.PIC_KB.ColNo, dr.Item("PICK_KB").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.BIN_KB.ColNo, dr.Item("BIN_KB").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.MOTO_CHAKU_KB.ColNo, dr.Item("MOTO_CHAKU_KB").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.URIAGE_CD.ColNo, dr.Item("URIAGE_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.URIAGE_NM.ColNo, dr.Item("URIAGE_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.TARIFF_BUNRUI_KB.ColNo, dr.Item("TARIFF_BUNRUI_KB").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, dr.Item("UNCHIN_TARIFF_CD1").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM1.ColNo, dr.Item("UNCHIN_TARIFF_NM1").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, dr.Item("UNCHIN_TARIFF_CD2").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.UNCHIN_TARIFF_NM2.ColNo, dr.Item("UNCHIN_TARIFF_NM2").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.EXTC_TARIFF_CD.ColNo, dr.Item("EXTC_TARIFF_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.EXTC_TARIFF_NM.ColNo, dr.Item("EXTC_TARIFF_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.YOKO_TARIFF_CD.ColNo, dr.Item("YOKO_TARIFF_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.YOKO_TARIFF_NM.ColNo, dr.Item("YOKO_TARIFF_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SET_MST_CD.ColNo, dr.Item("SET_MST_CD").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SET_KB.ColNo, dr.Item("SET_KB").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_UPD_DATE_T.ColNo, dr.Item("SYS_UPD_DATE_T").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_UPD_TIME_T.ColNo, dr.Item("SYS_UPD_TIME_T").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2(ByVal dt As DataTable, ByRef nrsbrcd As String, ByRef custcd As String, ByRef destcd As String)

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        Dim tmpDatarow2() As DataRow = dt.Select(String.Concat("NRS_BR_CD = '", nrsbrcd, "' AND  ", "CUST_CD_L = '", custcd, "' AND  ", "DEST_CD = '", destcd, "'"), "DEST_CD ASC,CUST_CD_L ASC,DEST_CD_EDA ASC, SUB_KB ASC")

        With spr2

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = tmpDatarow2.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
            Dim stxt As StyleInfo = LMSpreadUtility.GetTextCell(spr2, InputControl.ALL_MIX, 100, True)
            Dim stxt1000 As StyleInfo = LMSpreadUtility.GetTextCell(spr2, InputControl.ALL_MIX, 1000, True)
            Dim scmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr2, LMKbnConst.KBN_Y006, True)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow2(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.SUB_KB.ColNo, scmb)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.SET_NAIYO.ColNo, stxt1000)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.REMARK.ColNo, stxt)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEST_CD.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, dr.Item("DEST_CD_EDA").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.SUB_KB.ColNo, dr.Item("SUB_KB").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.SET_NAIYO.ColNo, dr.Item("SET_NAIYO").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread2()

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        With spr2

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
            Dim stxt As StyleInfo = LMSpreadUtility.GetTextCell(spr2, InputControl.ALL_MIX, 100, False)
            Dim stxt1000 As StyleInfo = LMSpreadUtility.GetTextCell(spr2, InputControl.ALL_MIX, 1000, False)
            Dim scmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr2, LMKbnConst.KBN_Y006, False)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            '値設定(MAX届先コード枝番)
            Dim MaxDest As String = String.Empty
            MaxDest = Me._Frm.lblMaxEda.TextValue

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM040G.sprDetailDef2.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, sLabel)
            .SetCellStyle(row, LMM040G.sprDetailDef2.SUB_KB.ColNo, scmb)
            .SetCellStyle(row, LMM040G.sprDetailDef2.SET_NAIYO.ColNo, stxt1000)
            .SetCellStyle(row, LMM040G.sprDetailDef2.REMARK.ColNo, stxt)
            .SetCellStyle(row, LMM040G.sprDetailDef2.DEST_CD.ColNo, sLabel)
            .SetCellStyle(row, LMM040G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(row, LMM040G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM040G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, MaxDest)
            .SetCellValue(row, LMM040G.sprDetailDef2.SUB_KB.ColNo, String.Empty)
            .SetCellValue(row, LMM040G.sprDetailDef2.SET_NAIYO.ColNo, String.Empty)
            .SetCellValue(row, LMM040G.sprDetailDef2.REMARK.ColNo, String.Empty)
            .SetCellValue(row, LMM040G.sprDetailDef2.DEST_CD.ColNo, String.Empty)
            .SetCellValue(row, LMM040G.sprDetailDef2.CUST_CD_L.ColNo, String.Empty)
            .SetCellValue(row, LMM040G.sprDetailDef2.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_行削除時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReSetSpread(ByVal spr As LMSpread)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            Dim max As Integer = .ActiveSheet.Rows.Count

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim stxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False)
            Dim stxt1000 As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 1000, False)
            Dim scmb As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Y006, False)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            For i As Integer = 1 To max

                If i > max Then
                    Exit For
                End If

                '行削除処理で最初のスプレッドの行数が減った場合は、行削除後のスプレッドの行数を設定
                If max > .ActiveSheet.Rows.Count Then
                    i = i - 1
                    max = max - 1
                End If

                '行削除されていない行は再描画
                If (LMConst.FLG.OFF).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SYS_DEL_FLG_T.ColNo))) = True Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEF.ColNo, sDEF)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.SUB_KB.ColNo, scmb)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.SET_NAIYO.ColNo, stxt1000)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.REMARK.ColNo, stxt)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.DEST_CD.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.DEST_CD_EDA.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.SUB_KB.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SUB_KB.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.SET_NAIYO.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SET_NAIYO.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.REMARK.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.REMARK.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.DEST_CD.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.DEST_CD.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.CUST_CD_L.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.CUST_CD_L.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.UPD_FLG.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.UPD_FLG.ColNo)))
                    .SetCellValue((i - 1), LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, _ControlG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SYS_DEL_FLG_T.ColNo)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

#Region "Spread(ADD/DEL)"

    ''' <summary>
    ''' MAX届先コード枝番の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetDestCdEdaDataSet(ByVal ds As DataSet, ByVal eventShubetsu As LMM040C.EventShubetsu) As Boolean

        '新規/複写の場合
        If ds Is Nothing Then
            ds = New LMM040DS()
        End If

        With Me._Frm

            '新規/複写の場合
            Dim max As Integer = ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).Rows.Count
            Dim insMRows As DataRow = ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).NewRow

            '複写の場合
            If (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True Then
                Dim RowCnt As Integer = Me._Frm.sprDetail2.ActiveSheet.Rows.Count - 1
                If -1 < RowCnt Then
                    .lblMaxEda.TextValue = Me._Frm.sprDetail2.ActiveSheet.Cells(RowCnt, LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo).Text
                End If
            End If

            '届先コード枝番の最大値を求める
            Dim oldMaxDestCd As String = String.Empty
            If (0).Equals(max) = True Then
                If String.IsNullOrEmpty(.lblMaxEda.TextValue) = True Then
                    oldMaxDestCd = "0"
                Else
                    oldMaxDestCd = .lblMaxEda.TextValue
                End If
            Else
                If ("").Equals(ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).Rows(max - 1).Item("DEST_MAXCD_EDA").ToString()) = True Then
                    oldMaxDestCd = "0"
                Else
                    oldMaxDestCd = ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).Rows(max - 1).Item("DEST_MAXCD_EDA").ToString()
                End If
            End If

            Dim newMaxDestCd As String = String.Empty
            If ("0").Equals(oldMaxDestCd) = True Then
                newMaxDestCd = _ControlG.SetMaeZeroData(oldMaxDestCd, 2)
            Else
                '現在のMAX届先コード枝番がMAX値を超えている場合は採番後の桁数を3桁にする
                If ("99").Equals(oldMaxDestCd) = False Then
                    newMaxDestCd = _ControlG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxDestCd) + 1), 2)
                Else
                    newMaxDestCd = _ControlG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxDestCd) + 1), 3)
                End If
            End If

            '枝番の限界値、チェック
            If Me._ControlV.IsMaxChk(Convert.ToInt32(newMaxDestCd), 99, "届先コード枝番") = False Then
                '処理終了アクション
                Return False
            End If

            '届先コード枝番の更新
            insMRows("DEST_MAXCD_EDA") = newMaxDestCd

            'データセットに追加
            ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).Rows.Add(insMRows)

            '画面のMAX届先コード枝番に設定
            .lblMaxEda.TextValue = newMaxDestCd

        End With

        Return True

    End Function

    ''' <summary>
    ''' 届先明細Spreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelDestDetails(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            If ("sprDetail2").Equals(spr.Name) = True Then
                For i As Integer = 0 To max

                    If i > max Then
                        Exit For
                    End If

                    If (LMConst.FLG.ON).Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.DEF.ColNo))) = True Then

                        If ("1").Equals(_ControlG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.UPD_FLG.ColNo))) = True Then
                            '既に登録済みのデータの場合は、削除フラグを"1"に変更
                            .SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMConst.FLG.ON)
                        Else
                            '上記以外(新規追加データ)の場合は、スプレッドから行削除
                            .ActiveSheet.RemoveRows(i, 1)

                            '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
                            i = i - 1
                            max = max - 1
                        End If

                    End If
                Next

            End If

        End With

    End Sub

#End Region

#End Region 'Spread

#End Region

End Class
