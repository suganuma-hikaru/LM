' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMM     : マスタ
'  プログラムID   : LMM080G : 運送会社マスタメンテ
'  作  成  者     : hirayama
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
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports GrapeCity.Win.Editors

''' <summary>
''' LMM080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM080F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM080V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

#If True Then ' 名鉄対応(2499) 2016.1.29 added inoue
    ''' <summary>
    ''' コンボに設定する帳票種別を格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private ReadOnly _CmbPtnIdItems As Dictionary(Of String, String) = New Dictionary(Of String, String)

#End If

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM080F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMMConG = g

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
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit
            '行追加・行削除ボタン
            Me._Frm.btnRowAdd.Enabled = edit
            Me._Frm.btnRowDel.Enabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region 'FunctionKey

    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW,
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
            .sprDetail.TabIndex = LMM080C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM080C.CtlTabIndex.NRSBRCD
            .txtUnsocoCd.TabIndex = LMM080C.CtlTabIndex.UNSOCOCD
            .txtUnsocoNm.TabIndex = LMM080C.CtlTabIndex.UNSOCONM
            .txtUnsocoBrCd.TabIndex = LMM080C.CtlTabIndex.UNSOCOBRCD
            .txtUnsocoBrNm.TabIndex = LMM080C.CtlTabIndex.UNSOCOBRNM
            .cmbUnsocoKb.TabIndex = LMM080C.CtlTabIndex.UNSOCOKB
            .txtZip.TabIndex = LMM080C.CtlTabIndex.ZIP
            .txtAd1.TabIndex = LMM080C.CtlTabIndex.AD1
            .txtAd2.TabIndex = LMM080C.CtlTabIndex.AD2
            .txtAd3.TabIndex = LMM080C.CtlTabIndex.AD3
            .cmbMotoukeKb.TabIndex = LMM080C.CtlTabIndex.MOTOUKEKB
            .pnlCost.TabIndex = LMM080C.CtlTabIndex.PNLCOST
            '(2012.08.17)支払サブ機能対応 --- START ---
            .txtShiharaitoCd.TabIndex = LMM080C.CtlTabIndex.SHIHARAITOCD
            .lblShiharaitoNm.TabIndex = LMM080C.CtlTabIndex.SHIHARAITONM
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            .txtUnchinTariffCd.TabIndex = LMM080C.CtlTabIndex.UNCHINTARIFFCD
            .lblUnshinTariffRem.TabIndex = LMM080C.CtlTabIndex.UNCHINTARIFFREM
            .txtExtcTariffCd.TabIndex = LMM080C.CtlTabIndex.EXTCTARIFFCD
            .lblExtcTariffRem.TabIndex = LMM080C.CtlTabIndex.EXTCTARIFFREM
            .txtKyoriCd.TabIndex = LMM080C.CtlTabIndex.BETUKYORICD
            .lblKyoriRem.TabIndex = LMM080C.CtlTabIndex.BETUKYORIREM
            .sprDetail2.TabIndex = LMM080C.CtlTabIndex.DETAIL2
            .dtpLastPuTime.TabIndex = LMM080C.CtlTabIndex.LASTPUTIME
            .cmbNihudaYn.TabIndex = LMM080C.CtlTabIndex.NIHUDAYN
            .cmbTareYn.TabIndex = LMM080C.CtlTabIndex.TAREYN
            .txtTel.TabIndex = LMM080C.CtlTabIndex.TEL
#If True Then ' FFEM荷札検品対応 20160610 added inoue
            .cmbTagBcdKbn.TabIndex = LMM080C.CtlTabIndex.TAGBCDKBN
#End If
            .txtFax.TabIndex = LMM080C.CtlTabIndex.FAX
            .txtURL.TabIndex = LMM080C.CtlTabIndex.URL
            .txtPic.TabIndex = LMM080C.CtlTabIndex.PIC
            .txtNrsSbetuCd.TabIndex = LMM080C.CtlTabIndex.NRSSBETUCD
            '要望番号:1275 yamanaka 2012.07.13 Start
            .txtRyakumei.TabIndex = LMM080C.CtlTabIndex.CUST_UNSO_RYAKU_NM
            '要望番号:1275 yamanaka 2012.07.13 End
            '要望番号:2140 kobayashi 2013.12.24 Start
            .cmbPickGrpKbn.TabIndex = LMM080C.CtlTabIndex.PICKLIST_GRP_KBN
            '要望番号:2140 kobayashi 2013.12.24 End
            .cmbEDIUseKbn.TabIndex = LMM080C.CtlTabIndex.EDI_USED_KBN
            .lblSituation.TabIndex = LMM080C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM080C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM080C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM080C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM080C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM080C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM080C.CtlTabIndex.SYSDELFLG
            .cmbNifudaScanTabYn.TabIndex = LMM080C.CtlTabIndex.WH_NIFUDA_SCAN_YN
        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me._LMMConG.ClearControl(Me._Frm)

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

            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbNihudaYn.SelectedValue = LMM080C.KBN_01
            .cmbTareYn.SelectedValue = LMM080C.KBN_01

#If True Then ' 名鉄対応(2499) 2016.1.29 added inoue
            .cmbAddPtnId.SelectedValue = LMM080C.INVOICE_PTNID
#End If

        End With

    End Sub


#If True Then ' 名鉄対応(2499) 2016.1.29 added inoue

    ''' <summary>
    ''' コンボボックスのItems設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCmbItems()
        Me.SetPtnIdCmbItems()
    End Sub


    ''' <summary>
    ''' 帳票種類コンボのItems設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetPtnIdCmbItems()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        ' ToDo: constへ移動
        Dim filterFormat As String = "KBN_GROUP_CD = '{0}' AND KBN_NM4 = '{1}' "
        Dim KeyColumnName As String = "KBN_CD"

        '2017/09/25 修正 李↓
        Dim ValueColumnName As String = lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"})
        '2017/09/25 修正 李↑

        Dim PtnIdKbnGroupCd As String = "T007"
        Dim UseUnsoCustValue As String = LMConst.FLG.ON

        _CmbPtnIdItems.Clear()

        With Me._Frm.cmbAddPtnId

            For Each row As DataRow In MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Format(filterFormat, PtnIdKbnGroupCd, UseUnsoCustValue), KeyColumnName)

                _CmbPtnIdItems.Add(row.Item(KeyColumnName).ToString(), row.Item(ValueColumnName).ToString())

                .Items.Add(New ListItem(New SubItem() {New SubItem(row.Item(ValueColumnName).ToString()) _
                                                     , New SubItem(row.Item(KeyColumnName).ToString())}))
            Next
        End With
    End Sub

#End If



    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Call Me._LMMConG.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, True)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, False)
                            Call Me._LMMConG.SetLockControl(.cmbNrsBrCd, True)
                            Call Me._LMMConG.SetLockControl(.txtUnsocoCd, True)
                            Call Me._LMMConG.SetLockControl(.txtUnsocoBrCd, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, False)
                            Call Me._LMMConG.SetLockControl(.cmbNrsBrCd, True)
                            Call Me._LMMConG.ClearControl(Me._Frm)
                            .sprDetail2.CrearSpread()
                            Call Me.SetcmbValue()
                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, False)
                            Call Me._LMMConG.SetLockControl(.cmbNrsBrCd, True)
                            Call Me._LMMConG.SetLockControl(.txtUnsocoCd, True)
                            Call Me.ClearControlFukusha()


                    End Select

                Case DispMode.INIT
                    Call Me._LMMConG.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, True)
                    .sprDetail2.CrearSpread()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtUnsocoBrCd.TextValue = String.Empty
            .txtUnsocoBrNm.TextValue = String.Empty

            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM080C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM080C.EventShubetsu.MAIN, LMM080C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM080C.EventShubetsu.SHINKI
                    .txtUnsocoCd.Focus()
                Case LMM080C.EventShubetsu.HENSHU
                    .txtUnsocoNm.Focus()
                Case LMM080C.EventShubetsu.DEL_T, LMM080C.EventShubetsu.INS_T
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

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtUnsocoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNSOCO_CD.ColNo).Text
            .txtUnsocoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNSOCO_NM.ColNo).Text
            .txtUnsocoBrCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo).Text
            .txtUnsocoBrNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo).Text
            .cmbUnsocoKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNSOCO_KB.ColNo).Text
            .txtZip.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.ZIP.ColNo).Text
            .txtAd1.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.AD_1.ColNo).Text
            .txtAd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.AD_2.ColNo).Text
            .txtAd3.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.AD_3.ColNo).Text
            .cmbMotoukeKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.MOTOUKE_KB.ColNo).Text
            '(2012.08.17)支払サブ機能対応 --- START ---
            .txtShiharaitoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SHIHARAITO_CD.ColNo).Text
            .lblShiharaitoNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SHIHARAITO_NM.ColNo).Text
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            .txtUnchinTariffCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo).Text
            .lblUnshinTariffRem.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo).Text
            .txtExtcTariffCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Text
            .lblExtcTariffRem.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.EXTC_TARIFF_REM.ColNo).Text
            .txtKyoriCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.BETU_KYORI_CD.ColNo).Text
            .lblKyoriRem.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.BETU_KYORI_REM.ColNo).Text
            .dtpLastPuTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.LAST_PU_TIME.ColNo).Text
            .cmbNihudaYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.NIHUDA_YN.ColNo).Text
            .cmbTareYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.TARE_YN.ColNo).Text
            .txtTel.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.TEL.ColNo).Text
            .txtFax.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.FAX.ColNo).Text
            .txtURL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.URL.ColNo).Text
            .txtPic.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.PIC.ColNo).Text
            .txtNrsSbetuCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.NRS_SBETU_CD.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.SYS_DEL_FLG.ColNo).Text
            '要望番号:1275 yamanaka 2012.07.13 Start
            .txtRyakumei.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.CUST_UNSO_RYAKU_NM.ColNo).Text
            '要望番号:1275 yamanaka 2012.07.13 End
            '要望番号:2140 kobayashi 2013.12.24 Start
            .cmbPickGrpKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.PICKLIST_GRP_KBN.ColNo).Text
            '要望番号:2140 kobayashi 2013.12.24 End
            .cmbEDIUseKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.EDI_USED_KBN.ColNo).Text
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
            .cmbNifudaScanYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.NIFUDA_SCAN_YN.ColNo).Text
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 

            '要望番号:2408 2015.09.17 追加START
            If .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.AUTO_DENP_NO_FLG.ColNo).Text.Equals(LMConst.FLG.ON) = True Then
                .chkAutoDenp.Checked = True
            Else
                .chkAutoDenp.Checked = False
            End If
            .cmbAutoDenpKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.AUTO_DENP_NO_KBN.ColNo).Text
            '要望番号:2408 2015.09.17 追加END
#If True Then ' FFEM荷札検品対応 20160610 added inoue
            .cmbTagBcdKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.TAG_BARCD_KBN.ColNo).Text

#End If
            .cmbNifudaScanTabYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM080G.sprDetailDef.WH_NIFUDA_SCAN_YN.ColNo).Text
        End With

    End Sub

#End Region

    ''' <summary>
    ''' Spreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelCustRpt(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            If ("sprDetail2").Equals(spr.Name) = True Then

                For i As Integer = 0 To max

                    If i > max Then
                        Exit For
                    End If

                    If (LMConst.FLG.ON).Equals(_LMMConG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.DEF.ColNo))) = True Then

                        '上記以外(新規追加データ)の場合は、スプレッドから行削除
                        .ActiveSheet.RemoveRows(i, 1)

                        '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
                        i = i - 1
                        max = max - 1

                    End If

                Next

            End If

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared UNSOCO_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNSOCO_CD, "運送会社" & vbCrLf & "コード", 100, True)
        Public Shared UNSOCO_BR_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNSOCO_BR_CD, "支店" & vbCrLf & "コード", 80, True)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNSOCO_NM, "運送会社名", 200, True)
        Public Shared UNSOCO_BR_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNSOCO_BR_NM, "支店名", 200, True)
        Public Shared MOTOUKE_KB As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.MOTOUKE_KB, "元請区分コード", 50, False)
        Public Shared MOTOUKE_KB_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.MOTOUKE_KB_NM, "元請区分", 80, True)
        Public Shared UNSOCO_KB As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNSOCO_KB, "運送会社区分", 50, False)
        Public Shared ZIP As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.ZIP, "郵便番号", 50, False)
        Public Shared AD_1 As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.AD_1, "住所1", 50, False)
        Public Shared AD_2 As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.AD_2, "住所2", 50, False)
        Public Shared AD_3 As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.AD_3, "住所3", 50, False)
        Public Shared TEL As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.TEL, "電話番号", 50, False)
        Public Shared FAX As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.FAX, "ファックス", 50, False)
        Public Shared URL As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.URL, "問合せURL", 50, False)
        Public Shared PIC As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.PIC, "担当者氏名", 50, False)
        Public Shared NRS_SBETU_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.NRS_SBETU_CD, "日陸識別コード", 50, False)
        Public Shared NIHUDA_YN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.NIHUDA_YN, "荷札有無フラグ", 50, False)
        Public Shared TARE_YN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.TARE_YN, "風袋加算フラグ", 50, False)
        '(2012.08.17)支払サブ機能対応 --- START ---
        Public Shared SHIHARAITO_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SHIHARAITO_CD, "支払先コード", 8, False)
        Public Shared SHIHARAITO_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SHIHARAITO_NM, "支払先名", 200, False)
        '(2012.08.17)支払サブ機能対応 ---  END  ---
        Public Shared UNCHIN_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNCHIN_TARIFF_CD, "運賃タリフコード", 50, False)
        Public Shared UNCHIN_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.UNCHIN_TARIFF_REM, "運賃タリフ備考", 50, False)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.EXTC_TARIFF_CD, "割増運賃タリフコード", 50, False)
        Public Shared EXTC_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.EXTC_TARIFF_REM, "割増運賃タリフ備考", 50, False)
        Public Shared BETU_KYORI_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.BETU_KYORI_CD, "距離程マスタコード", 50, False)
        Public Shared BETU_KYORI_REM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.BETU_KYORI_REM, "距離程マスタ備考", 50, False)
        Public Shared LAST_PU_TIME As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.LAST_PU_TIME, "最終集荷時間", 50, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_ENT_DATE, "作成日", 50, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 50, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_UPD_DATE, "更新日", 50, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 50, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)
        '要望番号:1275 yamanaka 2012.07.13 Start
        Public Shared CUST_UNSO_RYAKU_NM As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.CUST_UNSO_RYAKU_NM, "運送会社略名", 50, False)
        '要望番号:1275 yamanaka 2012.07.13 End
        '要望番号:2140 kobayashi 2013.12.24 Start
        Public Shared PICKLIST_GRP_KBN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.PICKLIST_GRP_KBN, "ピッキングリストグループ区分", 50, False)
        '要望番号:2140 kobayashi 2013.12.24 End
        Public Shared EDI_USED_KBN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.EDI_USED_KBN, "EDI連携区分", 50, False)
        '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
        Public Shared NIFUDA_SCAN_YN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.NIFUDA_SCAN_YN, "EDI連携区分", 50, False)
        '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 
        '要望番号:2408 2015.09.17 追加START
        Public Shared AUTO_DENP_NO_FLG As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.AUTO_DENP_NO_FLG, " 自動送状番号フラグ", 50, False)
        Public Shared AUTO_DENP_NO_KBN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.AUTO_DENP_NO_KBN, " 自動送状番号区分", 50, False)
        '要望番号:2408 2015.09.17 追加START
#If True Then ' FFEM荷札検品対応 20160610 added inoue
        Public Shared TAG_BARCD_KBN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.TAG_BARCD_KBN, "荷札バーコード区分", 50, False)
#End If
        Public Shared WH_NIFUDA_SCAN_YN As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex.WH_NIFUDA_SCAN_YN, "タブレット荷札スキャン", 50, False)
    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.DEF, " ", 20, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 100, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 100, True)
        Public Shared MOTO_TYAKU_KB As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.MOTO_TYAKU_KB, "元着払区分", 100, True)
#If False Then ' 名鉄対応 2016.1.27 changed inoue
        Public Shared PTN_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.PTN_CD, "パターン名", 250, True)
        Public Shared PTN_ID As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.PTN_ID, "帳票パターンID", 50, False)
#Else
        Public Shared PTN_ID As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.PTN_ID, "帳票種類", 90, True)
        Public Shared PTN_CD As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.PTN_CD, "パターン名", 250, True)
#End If
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMM080C.SprColumnIndex2.ROW_INDEX, "行番号", 10, False)

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
            '要望番号:1275 yamanaka 2012.07.13 Start
            .sprDetail.ActiveSheet.ColumnCount = LMM080C.SprColumnIndex.CLMCNT
            '要望番号:1275 yamanaka 2012.07.13 End
#If False Then ' 名鉄対応(2499) 2016.1.27 changed inoue
            .sprDetail2.ActiveSheet.ColumnCount = 7
#Else
            .sprDetail2.ActiveSheet.ColumnCount = LMM080C.SprColumnIndex2.COLUMN_COUNT
#End If

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM080G.sprDetailDef())
            '.sprDetail2.SetColProperty(New LMM080G.sprDetailDef2())
            .sprDetail.SetColProperty(New LMM080G.sprDetailDef(), False)
            .sprDetail2.SetColProperty(New LMM080G.sprDetailDef2(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM080G.sprDetailDef.DEF.ColNo + 1

            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)
            Dim lblStyle2 As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.NRS_BR_CD.ColNo, lblStyle)
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNSOCO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNSOCO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.MOTOUKE_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.MOTOUKE_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "M006", False))
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNSOCO_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.ZIP.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.AD_1.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.AD_2.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.AD_3.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.TEL.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.FAX.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.URL.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.PIC.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.NRS_SBETU_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.NIHUDA_YN.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.TARE_YN.ColNo, lblStyle)
            '(2012.08.17)支払サブ機能対応 --- STATR ---
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SHIHARAITO_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SHIHARAITO_NM.ColNo, lblStyle)
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.EXTC_TARIFF_REM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.BETU_KYORI_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.BETU_KYORI_REM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.LAST_PU_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)
            '要望番号:1275 yamanaka 2012.07.13 Start
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.CUST_UNSO_RYAKU_NM.ColNo, lblStyle)
            '要望番号:1275 yamanaka 2012.07.13 End
            '要望番号:2140 kobayashi 2013.12.24 Start
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.PICKLIST_GRP_KBN.ColNo, lblStyle)
            '要望番号:2140 kobayashi 2013.12.24 End
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.EDI_USED_KBN.ColNo, lblStyle)
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.NIFUDA_SCAN_YN.ColNo, lblStyle)
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 
            '要望番号:2408 2015.09.17 追加START
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.AUTO_DENP_NO_FLG.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.AUTO_DENP_NO_KBN.ColNo, lblStyle)
            '要望番号:2408 2015.09.17 追加END
#If True Then ' FFEM荷札検品対応 20166010 added inoue
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.TAG_BARCD_KBN.ColNo, lblStyle)
#End If
            .sprDetail.SetCellStyle(0, LMM080G.sprDetailDef.WH_NIFUDA_SCAN_YN.ColNo, lblStyle)
        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM080F)

        With frm.sprDetail

            .SetCellValue(0, LMM080G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM080G.sprDetailDef.UNSOCO_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.UNSOCO_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.MOTOUKE_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.MOTOUKE_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.UNSOCO_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.ZIP.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.AD_1.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.AD_2.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.AD_3.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.TEL.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.FAX.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.URL.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.PIC.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.NRS_SBETU_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.NIHUDA_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.TARE_YN.ColNo, String.Empty)
            '(2012.08.17)支払サブ機能対応 --- STATR ---
            .SetCellValue(0, LMM080G.sprDetailDef.SHIHARAITO_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SHIHARAITO_NM.ColNo, String.Empty)
            '(2012.08.17)支払サブ機能対応 ---  END  ---
            .SetCellValue(0, LMM080G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.EXTC_TARIFF_REM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.BETU_KYORI_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.BETU_KYORI_REM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.LAST_PU_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.SYS_DEL_FLG.ColNo, LMConst.FLG.OFF)
            '要望番号:1275 yamanaka 2012.07.13 Start
            .SetCellValue(0, LMM080G.sprDetailDef.CUST_UNSO_RYAKU_NM.ColNo, String.Empty)
            '要望番号:1275 yamanaka 2012.07.13 End
            '要望番号:2140 kobayashi 2013.12.24 Start
            .SetCellValue(0, LMM080G.sprDetailDef.PICKLIST_GRP_KBN.ColNo, String.Empty)
            '要望番号:2140 kobayashi 2013.12.24 End
            .SetCellValue(0, LMM080G.sprDetailDef.EDI_USED_KBN.ColNo, String.Empty)
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
            .SetCellValue(0, LMM080G.sprDetailDef.NIFUDA_SCAN_YN.ColNo, String.Empty)
            '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 

            '要望番号:2408 2015.09.17 追加START
            .SetCellValue(0, LMM080G.sprDetailDef.AUTO_DENP_NO_FLG.ColNo, String.Empty)
            .SetCellValue(0, LMM080G.sprDetailDef.AUTO_DENP_NO_KBN.ColNo, String.Empty)
            '要望番号:2408 2015.09.17 追加END

#If True Then ' FFEM荷札検品対応 20166010 added inoue
            .SetCellValue(0, LMM080G.sprDetailDef.TAG_BARCD_KBN.ColNo, String.Empty)
#End If
            .SetCellValue(0, LMM080G.sprDetailDef.WH_NIFUDA_SCAN_YN.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

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

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM080G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.UNSOCO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.MOTOUKE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.MOTOUKE_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.UNSOCO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.ZIP.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.AD_1.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.AD_2.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.AD_3.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.TEL.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.FAX.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.URL.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.PIC.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.NRS_SBETU_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.NIHUDA_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.TARE_YN.ColNo, sLabel)
                '(2012.08.17)支払サブ機能対応 --- START ---
                .SetCellStyle(i, LMM080G.sprDetailDef.SHIHARAITO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SHIHARAITO_NM.ColNo, sLabel)
                '(2012.08.17)支払サブ機能対応 ---  END  ---
                .SetCellStyle(i, LMM080G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.EXTC_TARIFF_REM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.BETU_KYORI_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.BETU_KYORI_REM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.LAST_PU_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                '要望番号:1275 yamanaka 2012.07.13 Start
                .SetCellStyle(i, LMM080G.sprDetailDef.CUST_UNSO_RYAKU_NM.ColNo, sLabel)
                '要望番号:1275 yamanaka 2012.07.13 End
                '要望番号:2140 kobayashi 2013.12.24 Start
                .SetCellStyle(i, LMM080G.sprDetailDef.PICKLIST_GRP_KBN.ColNo, sLabel)
                '要望番号:2140 kobayashi 2013.12.24 End
                .SetCellStyle(i, LMM080G.sprDetailDef.EDI_USED_KBN.ColNo, sLabel)

                '要望番号:2408 2015.09.17 追加START
                .SetCellStyle(i, LMM080G.sprDetailDef.AUTO_DENP_NO_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef.AUTO_DENP_NO_KBN.ColNo, sLabel)
                '要望番号:2408 2015.09.17 追加END
                'セルに値を設定
                .SetCellValue(i, LMM080G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.UNSOCO_CD.ColNo, dr.Item("UNSOCO_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo, dr.Item("UNSOCO_BR_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.UNSOCO_BR_NM.ColNo, dr.Item("UNSOCO_BR_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.MOTOUKE_KB.ColNo, dr.Item("MOTOUKE_KB").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.MOTOUKE_KB_NM.ColNo, dr.Item("MOTOUKE_KB_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.UNSOCO_KB.ColNo, dr.Item("UNSOCO_KB").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.ZIP.ColNo, dr.Item("ZIP").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.AD_1.ColNo, dr.Item("AD_1").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.AD_2.ColNo, dr.Item("AD_2").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.AD_3.ColNo, dr.Item("AD_3").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.TEL.ColNo, dr.Item("TEL").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.FAX.ColNo, dr.Item("FAX").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.URL.ColNo, dr.Item("URL").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.PIC.ColNo, dr.Item("PIC").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.NRS_SBETU_CD.ColNo, dr.Item("NRS_SBETU_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.NIHUDA_YN.ColNo, dr.Item("NIHUDA_YN").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.TARE_YN.ColNo, dr.Item("TARE_YN").ToString())
                '(2012.08.17)支払サブ機能対応 --- START ---
                .SetCellValue(i, LMM080G.sprDetailDef.SHIHARAITO_CD.ColNo, dr.Item("SHIHARAITO_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SHIHARAITO_NM.ColNo, dr.Item("SHIHARAITO_NM").ToString())
                '(2012.08.17)支払サブ機能対応 ---  END  ---
                .SetCellValue(i, LMM080G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo, dr.Item("UNCHIN_TARIFF_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo, dr.Item("UNCHIN_TARIFF_REM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, dr.Item("EXTC_TARIFF_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.EXTC_TARIFF_REM.ColNo, dr.Item("EXTC_TARIFF_REM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.BETU_KYORI_CD.ColNo, dr.Item("BETU_KYORI_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.BETU_KYORI_REM.ColNo, dr.Item("BETU_KYORI_REM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.LAST_PU_TIME.ColNo, dr.Item("LAST_PU_TIME").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                '要望番号:1275 yamanaka 2012.07.13 Start
                .SetCellValue(i, LMM080G.sprDetailDef.CUST_UNSO_RYAKU_NM.ColNo, dr.Item("CUST_UNSO_RYAKU_NM").ToString())
                '要望番号:1275 yamanaka 2012.07.13 End
                '要望番号:2140 kobayashi 2013.12.24 Start
                .SetCellValue(i, LMM080G.sprDetailDef.PICKLIST_GRP_KBN.ColNo, dr.Item("PICKLIST_GRP_KBN").ToString())
                '要望番号:2140 kobayashi 2013.12.24 End
                .SetCellValue(i, LMM080G.sprDetailDef.EDI_USED_KBN.ColNo, dr.Item("EDI_USED_KBN").ToString())
                '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正開始 --- 
                .SetCellValue(i, LMM080G.sprDetailDef.NIFUDA_SCAN_YN.ColNo, dr.Item("NIFUDA_SCAN_YN").ToString())
                '(2015.03.03)WIT 出荷検品 荷札スキャン機能対応 --- 修正終了 --- 

                '要望番号:2408 2015.09.17 追加START
                .SetCellValue(i, LMM080G.sprDetailDef.AUTO_DENP_NO_FLG.ColNo, dr.Item("AUTO_DENP_NO_FLG").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef.AUTO_DENP_NO_KBN.ColNo, dr.Item("AUTO_DENP_NO_KBN").ToString())
                '要望番号:2408 2015.09.17 追加SEND

#If True Then ' FFEM荷札検品対応 20166010 added inoue
                .SetCellValue(i, LMM080G.sprDetailDef.TAG_BARCD_KBN.ColNo, dr.Item("TAG_BARCD_KBN").ToString())
#End If
                .SetCellValue(i, LMM080G.sprDetailDef.WH_NIFUDA_SCAN_YN.ColNo, dr.Item("WH_NIFUDA_SCAN_YN").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 明細スプレッド表示
    ''' </summary>
    ''' <param name="dt"></param>    
    ''' <remarks></remarks>
    Friend Sub SetSpreadDetail(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprDetail2

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True
            Dim edit As Boolean = unlock

            If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                edit = unlock
            Else
                edit = lock
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sCmbMototyak As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_M001, lock)
#If False Then ' 名鉄対応(2499) 2016.1.27 changed inoue
            Dim sCmbPtn As StyleInfo = Me.StyleInfoPtnNm(LMM080C.INVOICE_PTNID, edit)
#End If
            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                .SetCellStyle(i, LMM080G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM080G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef2.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo, sCmbMototyak)
#If False Then ' 名鉄対応(2499) 2016.1.27 added inoue
                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_CD.ColNo, sCmbPtn)
                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_ID.ColNo, sLabel)
#Else
                Dim sCmbPtnId As StyleInfo = Me.StyleInfoPtnIdNm(dr.Item("PTN_ID").ToString(), lock)
                Dim sCmbPtn As StyleInfo = Me.StyleInfoPtnNm(dr.Item("PTN_ID").ToString(), edit)

                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_CD.ColNo, sCmbPtn)
                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_ID.ColNo, sCmbPtnId)
#End If
                .SetCellStyle(i, LMM080G.sprDetailDef2.ROW_INDEX.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM080G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM080G.sprDetailDef2.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo, dr.Item("MOTO_TYAKU_KB").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.PTN_CD.ColNo, dr.Item("PTN_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.PTN_ID.ColNo, dr.Item("PTN_ID").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.ROW_INDEX.ColNo, Me.SetZeroData(i.ToString(), LMM080C.MAEZERO))

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 明細Spreadに一行追加する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddRow(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprDetail2

        With spr

            .SuspendLayout()

            Dim startRow As Integer = .ActiveSheet.Rows.Count

            .ActiveSheet.AddRows(startRow, dt.Rows.Count)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sCmbMototyak As StyleInfo = Me.StyleInfoCombKbn(spr, LMKbnConst.KBN_M001, lock)
#If False Then ' 名鉄対応(2499) 2016.1.29 changed inoue
            Dim sCmbPtn As StyleInfo = Me.StyleInfoPtnNm(LMM080C.INVOICE_PTNID, unlock)

#Else
            Dim sCmbPtnId As StyleInfo = Me.StyleInfoPtnIdNm(_Frm.cmbAddPtnId.SelectedValue.ToString(), lock)
            Dim sCmbPtn As StyleInfo = Me.StyleInfoPtnNm(_Frm.cmbAddPtnId.SelectedValue.ToString(), unlock)


#End If
            Dim max As Integer = startRow + dt.Rows.Count - 1
            Dim dr As DataRow

            '値設定
            For i As Integer = startRow To max

                dr = dt.Rows(i - startRow)

                'セルスタイル設定
                .SetCellStyle(i, LMM080G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM080G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef2.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo, sCmbMototyak)
                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_CD.ColNo, sCmbPtn)
#If False Then ' 名鉄対応(2499) 2016.1.29 added inoue
                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_ID.ColNo, sLabel)
#Else
                .SetCellStyle(i, LMM080G.sprDetailDef2.PTN_ID.ColNo, sCmbPtnId)
#End If
                .SetCellStyle(i, LMM080G.sprDetailDef2.ROW_INDEX.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM080G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM080G.sprDetailDef2.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.MOTO_TYAKU_KB.ColNo, dr.Item("MOTO_TYAKU_KB").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.PTN_CD.ColNo, dr.Item("PTN_CD").ToString())
                .SetCellValue(i, LMM080G.sprDetailDef2.PTN_ID.ColNo, dr.Item("PTN_ID").ToString())

            Next

            '並び順再設定
            max = .ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max
                .SetCellValue(i, LMM080G.sprDetailDef2.ROW_INDEX.ColNo, Me.SetZeroData(i.ToString(), LMM080C.MAEZERO))
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 明細Spreadの行削除を行う
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DelateDtl(ByVal list As ArrayList)

        Dim listMax As Integer = list.Count - 1
        For i As Integer = listMax To 0 Step -1
            Me._Frm.sprDetail2.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
        Next

    End Sub

    ''' <summary>
    ''' 運送会社マスタにひもづく運送会社荷主別マスタデータ取得SQL構築
    ''' </summary>
    ''' <param name="nrsBrCD"></param>
    ''' <param name="unsoCd"></param>
    ''' <param name="unsoBrCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetCustRptSql(ByVal nrsBrCD As String _
                                      , ByVal unsoCd As String _
                                      , ByVal unsoBrCd As String _
                                      ) As String

        SetCustRptSql = String.Concat("NRS_BR_CD = '", nrsBrCD, "'")

        If String.IsNullOrEmpty(unsoCd) = False Then

            SetCustRptSql = String.Concat(SetCustRptSql, " AND UNSOCO_CD = '", unsoCd, "'")

        End If

        If String.IsNullOrEmpty(unsoBrCd) = False Then

            SetCustRptSql = String.Concat(SetCustRptSql, " AND UNSOCO_BR_CD = '", unsoBrCd, "'")

        End If

        Return SetCustRptSql

    End Function

    ''' <summary>
    ''' ソート設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetDataTableSort() As String

#If False Then ' 名鉄対応(4299) 2016.2.1 changed inoue
        SetDataTableSort = "CUST_CD_L,CUST_CD_M,MOTO_TYAKU_KB,PTN_CD"
#Else
        SetDataTableSort = "CUST_CD_L,CUST_CD_M,PTN_ID,MOTO_TYAKU_KB,PTN_CD"
#End If
        Return SetDataTableSort

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
    ''' 並び替え処理(Detail)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub sprDetailSortColumnCommand()

        Call Me.sprSortColumnCommand(Me._Frm.sprDetail2, LMM080G.sprDetailDef2.ROW_INDEX.ColNo)

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
    ''' セルのプロパティを設定(帳票パターン名)
    ''' </summary>
    ''' <param name="ptnId">帳票種別ID</param>
    ''' <param name="lock">ロック制御</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoPtnNm(ByVal ptnId As String _
                                  , ByVal lock As Boolean _
                                  ) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(Me._Frm.sprDetail2 _
                                                  , LMConst.CacheTBL.RPT _
                                                  , "PTN_CD" _
                                                  , "PTN_NM" _
                                                  , lock _
                                                  , New String() {"NRS_BR_CD", "PTN_ID"} _
                                                  , New String() {LMUserInfoManager.GetNrsBrCd(), ptnId} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

#If True Then ' 名鉄対応(2499) 2016.1.29 added inoue
    Private Function StyleInfoPtnIdNm(ByVal ptnId As String _
                                    , ByVal lock As Boolean _
                                     ) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(Me._Frm.sprDetail2 _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM1" _
                                                  , lock _
                                                  , New String() {"KBN_GROUP_CD", "KBN_CD"} _
                                                  , New String() {"T007", ptnId} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function
#End If


#End Region

#End Region

#Region "キー項目変数セット"

    Friend Function NrsBrCdSet() As String

        Dim nrsBrCd As String = LMUserInfoManager.GetNrsBrCd

        Return nrsBrCd

    End Function

    Friend Function UnsocoCdSet() As String

        Dim unsoCd As String = Me._Frm.txtUnsocoCd.TextValue

        Return unsoCd

    End Function

    Friend Function UnsocoBrCdSet() As String

        Dim unsoBrCd As String = Me._Frm.txtUnsocoBrCd.TextValue

        Return unsoBrCd

    End Function

    Friend Function NrsBrCdSetFromSpread(ByVal eRow As Integer) As String

        Dim nrsBrCd As String = Me._Frm.sprDetail.ActiveSheet.Cells(eRow, LMM080G.sprDetailDef.NRS_BR_CD.ColNo).Text()

        Return nrsBrCd

    End Function

    Friend Function UnsocoCdSetFromSpread(ByVal eRow As Integer) As String

        Dim unsoCd As String = Me._Frm.sprDetail.ActiveSheet.Cells(eRow, LMM080G.sprDetailDef.UNSOCO_CD.ColNo).Text()

        Return unsoCd

    End Function

    Friend Function UnsocoBrCdSetFromSpread(ByVal eRow As Integer) As String

        Dim unsoBrCd As String = Me._Frm.sprDetail.ActiveSheet.Cells(eRow, LMM080G.sprDetailDef.UNSOCO_BR_CD.ColNo).Text()

        Return unsoBrCd

    End Function

#End Region

End Class
