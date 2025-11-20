' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LM     : 在庫サブシステム
'  プログラムID     :  LMD010G : 在庫振替入力
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMD010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMD010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD010F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConG As LMDControlG

#If True Then '要望番号2449対応 Added 20151120 INOUE
    ''' <summary>
    ''' アクションタイプを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _EditActionType As LMD010C.ActionType = LMD010C.ActionType.MAIN
#End If


#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD010F, ByVal g As LMDControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMDConG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal actionType As LMD010C.ActionType)

        Dim always As Boolean = True
        Dim allLock As Boolean = False
        Dim lock1 As Boolean = False
        Dim lock2 As Boolean = False
        Dim lock3 As Boolean = False
        Dim lock4 As Boolean = False
        Dim lock5 As Boolean = False


        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = "複　写"
            .F4ButtonName = String.Empty
            .F5ButtonName = "引　当"
            .F6ButtonName = "全　量"
            .F7ButtonName = String.Empty
            .F8ButtonName = "戻　る"
            .F9ButtonName = String.Empty
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "振替元確定"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.EDIT   '編集モード

#If True Then '要望番号2449対応 Added 20151120 INOUE
                    Me._EditActionType = actionType
#End If
                    Select Case actionType

                        Case LMD010C.ActionType.HENSHU _
                           , LMD010C.ActionType.FUKUSHA _
                           , LMD010C.ActionType.HIKIATE _
                           , LMD010C.ActionType.ZENRYO
                            lock1 = False
                            lock2 = True
                            lock3 = True
                            lock4 = True
                            .F11ButtonName = "振替元確定"
                            lock5 = False
                        Case LMD010C.ActionType.FURIKAEMOTOKAKUTEI
                            lock1 = False
                            lock2 = True
                            lock3 = False
                            lock4 = False
                            .F11ButtonName = "振替確定"
                            lock5 = True
                        Case LMD010C.ActionType.FURIKAEKAKUTEI
                            lock1 = False
                            lock2 = False
                            lock3 = False
                            lock4 = True
                            lock5 = False
                    End Select

            End Select

            .F1ButtonEnabled = allLock
            .F2ButtonEnabled = allLock
            .F3ButtonEnabled = lock4
            .F4ButtonEnabled = allLock
            .F5ButtonEnabled = lock3
            .F6ButtonEnabled = lock3
            .F7ButtonEnabled = allLock
            .F8ButtonEnabled = lock5
            .F9ButtonEnabled = allLock
            .F10ButtonEnabled = lock2
            .F11ButtonEnabled = lock2
            .F12ButtonEnabled = always

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With

    End Sub

#End Region

    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW, _
                                Optional ByVal recSts As String = RecordStatus.NOMAL_REC)

        Me._Frm.lblSituation.DispMode = dispMd
        Me._Frm.lblSituation.RecordStatus = recSts

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
            .lblFurikaeNo.TabIndex = LMD010C.CtlTabIndex.FURIKAENO
            .cmbNrsBrCd.TabIndex = LMD010C.CtlTabIndex.NRSBRCD
            .cmbSoko.TabIndex = LMD010C.CtlTabIndex.SOKO
            .cmbFurikaeKbn.TabIndex = LMD010C.CtlTabIndex.FURIKAEKBN
            .imdFurikaeDate.TabIndex = LMD010C.CtlTabIndex.FURIKAEDATE
            .chkYoukiChange.TabIndex = LMD010C.CtlTabIndex.YOUKICHANGE
            .cmbToukiHokanKbn.TabIndex = LMD010C.CtlTabIndex.TOUKIHOKANKBN
            .pnlFurikae.TabIndex = LMD010C.CtlTabIndex.FURIKAE
            .txtCustCdL.TabIndex = LMD010C.CtlTabIndex.CUSTCDL
            .lblCustNmL.TabIndex = LMD010C.CtlTabIndex.CUSTNML
            .txtCustCdM.TabIndex = LMD010C.CtlTabIndex.CUSTCDM
            .lblCustNmM.TabIndex = LMD010C.CtlTabIndex.CUSTNMM
            .txtOrderNo.TabIndex = LMD010C.CtlTabIndex.ORDERNO
            .txtGoodsCdCust.TabIndex = LMD010C.CtlTabIndex.GOODSCDCUST
            .txtGoodsNmCust.TabIndex = LMD010C.CtlTabIndex.GOODSNMCUST
            .txtLotNo.TabIndex = LMD010C.CtlTabIndex.LOTNO
            .txtSerialNo.TabIndex = LMD010C.CtlTabIndex.SERIALNO
            .numIrime.TabIndex = LMD010C.CtlTabIndex.IRIME
            .lblIrimeTanni.TabIndex = LMD010C.CtlTabIndex.IRIMETANNI
            .cmbNiyaku.TabIndex = LMD010C.CtlTabIndex.NIYAKU
            .cmbTaxKbn.TabIndex = LMD010C.CtlTabIndex.TAXKBN
            .pnlKosu.TabIndex = LMD010C.CtlTabIndex.KOSU
            .numKonsu.TabIndex = LMD010C.CtlTabIndex.KONSU
            .lblKonsuTanni.TabIndex = LMD010C.CtlTabIndex.KONSUTANNI
            .numCnt.TabIndex = LMD010C.CtlTabIndex.CNT
            .lblCntTani.TabIndex = LMD010C.CtlTabIndex.CNTTANI
            .lblIrisuCnt.TabIndex = LMD010C.CtlTabIndex.IRISUCNT
            .lblKosuCnt.TabIndex = LMD010C.CtlTabIndex.KOSUCNT
            .lblHikiSumiCnt.TabIndex = LMD010C.CtlTabIndex.HIKISUMICNT
            .lblHikiZanCnt.TabIndex = LMD010C.CtlTabIndex.HIKIZANCNT
            .pnlHutaiSagyo.TabIndex = LMD010C.CtlTabIndex.HUTAISAGYO
            .txtSagyoCdO1.TabIndex = LMD010C.CtlTabIndex.SAGYOCD1
            .lblSagyoNmO1.TabIndex = LMD010C.CtlTabIndex.SAGYONM1
            .txtSagyoCdO2.TabIndex = LMD010C.CtlTabIndex.SAGYOCD2
            .lblSagyoNmO2.TabIndex = LMD010C.CtlTabIndex.SAGYONM2
            .txtSagyoCdO3.TabIndex = LMD010C.CtlTabIndex.SAGYOCD3
            .lblSagyoNmO3.TabIndex = LMD010C.CtlTabIndex.SAGYONM3
            .pnlNyukaRemark.TabIndex = LMD010C.CtlTabIndex.PSYUKKAREMARK
            .txtSyukkaRemark.TabIndex = LMD010C.CtlTabIndex.SYUKKAREMARK
            .btnMotoDel.TabIndex = LMD010C.CtlTabIndex.DEL
            .spdDtl.TabIndex = LMD010C.CtlTabIndex.DETAIL
            .pnlFurikaeNew.TabIndex = LMD010C.CtlTabIndex.FURIKAENEW
            .txtCustCdLNew.TabIndex = LMD010C.CtlTabIndex.CUSTCDLNEW
            .lblCustNmLNew.TabIndex = LMD010C.CtlTabIndex.CUSTNMLNEW
            .txtCustCdMNew.TabIndex = LMD010C.CtlTabIndex.CUSTCDMNEW
            .lblCustNmMNew.TabIndex = LMD010C.CtlTabIndex.CUSTNMMNEW
            .txtDenpNo.TabIndex = LMD010C.CtlTabIndex.DENPNO
            .txtGoodsCdCustNew.TabIndex = LMD010C.CtlTabIndex.GOODSCDCUSTNEW
            .txtGoodsNmCustNew.TabIndex = LMD010C.CtlTabIndex.GOODSNMCUSTNEW
            .lblGoodsCdNrsNew.TabIndex = LMD010C.CtlTabIndex.GOODSCDNRSNEW
            .cmbNiyakuNew.TabIndex = LMD010C.CtlTabIndex.NIYAKUNEW
            .cmbTaxKbnNew.TabIndex = LMD010C.CtlTabIndex.TAXKBNNEW
            .chkInkoDateUmu.TabIndex = LMD010C.CtlTabIndex.INKODATEUMU
            .pnlHutaiSagyoNew.TabIndex = LMD010C.CtlTabIndex.HUTAISAGYONEW
            .txtSagyoCdN1.TabIndex = LMD010C.CtlTabIndex.SAGYOCD1NEW
            .lblSagyoNmN1.TabIndex = LMD010C.CtlTabIndex.SAGYONM1NEW
            .txtSagyoCdN2.TabIndex = LMD010C.CtlTabIndex.SAGYOCD2NEW
            .lblSagyoNmN2.TabIndex = LMD010C.CtlTabIndex.SAGYONM2NEW
            .txtSagyoCdN3.TabIndex = LMD010C.CtlTabIndex.SAGYOCD3NEW
            .lblSagyoNmN3.TabIndex = LMD010C.CtlTabIndex.SAGYONM3NEW
            .pnlNyukaRemark.TabIndex = LMD010C.CtlTabIndex.PNYUKAREMARK
            .txtNyukaRemark.TabIndex = LMD010C.CtlTabIndex.NYUKAREMARK
            .btnSakiAdd.TabIndex = LMD010C.CtlTabIndex.ADDNEW
            .btnSakiDel.TabIndex = LMD010C.CtlTabIndex.DELNEW
            .sprDtlNew.TabIndex = LMD010C.CtlTabIndex.DETAILNEW

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="actionType">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal actionType As LMD010C.ActionType)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.EDIT
                    Select Case actionType

                        '編集時（初期状態）
                        Case LMD010C.ActionType.HENSHU

                            '検索項目ロック
                            Me._LMDConG.SetLockControl(.cmbNrsBrCd, True)
                            Me._LMDConG.SetLockControl(.cmbSoko, False)
                            Me._LMDConG.SetLockControl(.cmbFurikaeKbn, False)
                            Me._LMDConG.SetLockControl(.imdFurikaeDate, False)
                            Me._LMDConG.SetLockControl(.chkYoukiChange, False)
                            Me._LMDConG.SetLockControl(.cmbToukiHokanKbn, False)

                            '振替元入力項目ロック解除
                            Me._LMDConG.SetLockControl(.txtCustCdL, False)
                            Me._LMDConG.SetLockControl(.txtCustCdM, False)
                            Me._LMDConG.SetLockControl(.txtOrderNo, False)
                            Me._LMDConG.SetLockControl(.txtGoodsCdCust, False)
                            Me._LMDConG.SetLockControl(.txtGoodsNmCust, False)
                            Me._LMDConG.SetLockControl(.txtLotNo, False)
                            Me._LMDConG.SetLockControl(.txtSerialNo, False)
                            Me._LMDConG.SetLockControl(.cmbNiyaku, False)
                            'Me._LMDConG.SetLockControl(.cmbTaxKbn, False)
                            Me._LMDConG.SetLockControl(.numKonsu, False)
                            Me._LMDConG.SetLockControl(.numCnt, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO1, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO2, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO3, False)
                            Me._LMDConG.SetLockControl(.txtSyukkaRemark, False)
                            Me._LMDConG.LockButton(.btnMotoDel, False)

                            Me._LMDConG.LockButton(.btnMotoDel, False)
                            'START YANAI No.105
                            'Me._LMDConG.LockButton(.btnSakiAdd, False)
                            'Me._LMDConG.LockButton(.btnSakiDel, False)
                            Me._LMDConG.LockButton(.btnSakiAdd, True)
                            Me._LMDConG.LockButton(.btnSakiDel, True)
                            'END YANAI No.105

                            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
                            ' 初期入荷日を引き継いで在庫を振替えるにチェック
                            .chkInkoDateUmu.Checked = True
                            ' 当期保管料負担区分に"振替元"をセット
                            .cmbToukiHokanKbn.SelectedValue = "10"
                            '荷役量有無(振替元)に"無"をセット
                            .cmbNiyaku.SelectedValue = "00"
                            '荷役量有無(振替先)に"無"をセット
                            .cmbNiyakuNew.SelectedValue = "00"

                            '引当、全量時
                        Case LMD010C.ActionType.HIKIATE, LMD010C.ActionType.ZENRYO
                            '引当、全量に使用する項目をロックする
                            If .spdDtl.ActiveSheet.Rows.Count <> 0 Then
                                Me._LMDConG.SetLockControl(.txtGoodsCdCust, True)
                                Me._LMDConG.SetLockControl(.txtGoodsNmCust, True)
                                Me._LMDConG.SetLockControl(.txtCustCdL, True)
                                Me._LMDConG.SetLockControl(.txtCustCdM, True)
                                Me._LMDConG.SetLockControl(.cmbSoko, True)
                            Else
                                Me._LMDConG.SetLockControl(.txtGoodsCdCust, False)
                                Me._LMDConG.SetLockControl(.txtGoodsNmCust, False)
                                Me._LMDConG.SetLockControl(.txtCustCdL, False)
                                Me._LMDConG.SetLockControl(.txtCustCdM, False)
                                Me._LMDConG.SetLockControl(.cmbSoko, False)
                            End If

                            '（小）スプレッドロック解除
                            If 0 < Me._Frm.spdDtl.ActiveSheet.RowCount Then
                                Me._Frm.spdDtl.ActiveSheet.Cells(0, LMD010C.SprColumnIndexSprDtl.DEF, Me._Frm.spdDtl.ActiveSheet.RowCount - 1, LMD010C.SprColumnIndexSprDtl.DEF).Locked = False
                            End If

                            '振替元確定時
                        Case LMD010C.ActionType.FURIKAEMOTOKAKUTEI

                            '検索項目ロック
                            Me._LMDConG.SetLockControl(.cmbSoko, True)
                            Me._LMDConG.SetLockControl(.cmbFurikaeKbn, True)
                            Me._LMDConG.SetLockControl(.imdFurikaeDate, True)
                            Me._LMDConG.SetLockControl(.chkYoukiChange, True)
                            Me._LMDConG.SetLockControl(.cmbToukiHokanKbn, True)

                            '振替元入力項目ロック
                            Me._LMDConG.SetLockControl(.txtCustCdL, True)
                            Me._LMDConG.SetLockControl(.txtCustCdM, True)
                            Me._LMDConG.SetLockControl(.txtOrderNo, True)
                            Me._LMDConG.SetLockControl(.txtGoodsCdCust, True)
                            Me._LMDConG.SetLockControl(.txtGoodsNmCust, True)
                            Me._LMDConG.SetLockControl(.txtLotNo, True)
                            Me._LMDConG.SetLockControl(.txtSerialNo, True)
                            Me._LMDConG.SetLockControl(.numIrime, True)
                            Me._LMDConG.SetLockControl(.cmbNiyaku, True)
                            Me._LMDConG.SetLockControl(.cmbTaxKbn, True)
                            Me._LMDConG.SetLockControl(.numKonsu, True)
                            Me._LMDConG.SetLockControl(.numCnt, True)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO1, True)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO2, True)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO3, True)
                            Me._LMDConG.SetLockControl(.txtSyukkaRemark, True)
                            Me._LMDConG.LockButton(.btnMotoDel, True)

                            Me._Frm.spdDtl.AllLock()

                            '振替先入力項目ロック解除
                            Me._LMDConG.LockButton(.btnSakiDel, False)
                            Me._LMDConG.SetLockControl(.txtDenpNo, False)
                            Me._LMDConG.SetLockControl(.cmbNiyakuNew, False)
                            Me._LMDConG.SetLockControl(.cmbTaxKbnNew, False)
                            Me._LMDConG.SetLockControl(.chkInkoDateUmu, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdN1, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdN2, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdN3, False)
                            Me._LMDConG.SetLockControl(.txtNyukaRemark, False)

                            .txtDenpNo.TextValue = .txtOrderNo.TextValue

#If False Then '要望番号2449対応 Changed 20151120 INOUE
                            Select Case .cmbFurikaeKbn.SelectedText

                                Case "荷主変更"
                                    Me._LMDConG.SetLockControl(.txtCustCdLNew, False)
                                    Me._LMDConG.SetLockControl(.lblCustNmLNew, False)
                                    Me._LMDConG.SetLockControl(.txtCustCdMNew, False)
                                    Me._LMDConG.SetLockControl(.lblCustNmMNew, False)
                                    Me._LMDConG.SetLockControl(.txtGoodsCdCustNew, False)
                                    Me._LMDConG.SetLockControl(.txtGoodsNmCustNew, False)

                                Case "商品コード変更"
                                    Me._LMDConG.SetLockControl(.txtGoodsCdCustNew, False)
                                    Me._LMDConG.SetLockControl(.txtGoodsNmCustNew, False)
                                    .txtCustCdLNew.TextValue = .txtCustCdL.TextValue
                                    .lblCustNmLNew.TextValue = .lblCustNmL.TextValue
                                    .txtCustCdMNew.TextValue = .txtCustCdM.TextValue
                                    .lblCustNmMNew.TextValue = .lblCustNmM.TextValue

                                Case "ロット番号変更"
                                    .txtCustCdLNew.TextValue = .txtCustCdL.TextValue
                                    .lblCustNmLNew.TextValue = .lblCustNmL.TextValue
                                    .txtCustCdMNew.TextValue = .txtCustCdM.TextValue
                                    .lblCustNmMNew.TextValue = .lblCustNmM.TextValue
                                    .txtGoodsCdCustNew.TextValue = .txtGoodsCdCust.TextValue
                                    .txtGoodsNmCustNew.TextValue = .txtGoodsNmCust.TextValue
                                    .lblGoodsCdNrsNew.TextValue = .lblGoodsCdNrs.TextValue

                                Case "簿外品"
                                    .txtCustCdLNew.TextValue = .txtCustCdL.TextValue
                                    .lblCustNmLNew.TextValue = .lblCustNmL.TextValue
                                    .txtCustCdMNew.TextValue = .txtCustCdM.TextValue
                                    .lblCustNmMNew.TextValue = .lblCustNmM.TextValue
                                    .txtGoodsCdCustNew.TextValue = .txtGoodsCdCust.TextValue
                                    .txtGoodsNmCustNew.TextValue = .txtGoodsNmCust.TextValue
                                    .lblGoodsCdNrsNew.TextValue = .lblGoodsCdNrs.TextValue

                            End Select
#Else


                            Select Case .cmbFurikaeKbn.SelectedValue.ToString()

                                Case LMD010C.FURIKAE_KBN_CUST
                                    Me._LMDConG.SetLockControl(.txtCustCdLNew, False)
                                    Me._LMDConG.SetLockControl(.lblCustNmLNew, False)
                                    Me._LMDConG.SetLockControl(.txtCustCdMNew, False)
                                    Me._LMDConG.SetLockControl(.lblCustNmMNew, False)
                                    Me._LMDConG.SetLockControl(.txtGoodsCdCustNew, False)
                                    Me._LMDConG.SetLockControl(.txtGoodsNmCustNew, False)

                                Case LMD010C.FURIKAE_KBN_GOODS
                                    Me._LMDConG.SetLockControl(.txtGoodsCdCustNew, False)
                                    Me._LMDConG.SetLockControl(.txtGoodsNmCustNew, False)
                                    .txtCustCdLNew.TextValue = .txtCustCdL.TextValue
                                    .lblCustNmLNew.TextValue = .lblCustNmL.TextValue
                                    .txtCustCdMNew.TextValue = .txtCustCdM.TextValue
                                    .lblCustNmMNew.TextValue = .lblCustNmM.TextValue

                                Case LMD010C.FURIKAE_KBN_LOT
                                    .txtCustCdLNew.TextValue = .txtCustCdL.TextValue
                                    .lblCustNmLNew.TextValue = .lblCustNmL.TextValue
                                    .txtCustCdMNew.TextValue = .txtCustCdM.TextValue
                                    .lblCustNmMNew.TextValue = .lblCustNmM.TextValue
                                    .txtGoodsCdCustNew.TextValue = .txtGoodsCdCust.TextValue
                                    .txtGoodsNmCustNew.TextValue = .txtGoodsNmCust.TextValue
                                    .lblGoodsCdNrsNew.TextValue = .lblGoodsCdNrs.TextValue

                                Case LMD010C.FURIKAE_KBN_HAKUGAIHIN
                                    .txtCustCdLNew.TextValue = .txtCustCdL.TextValue
                                    .lblCustNmLNew.TextValue = .lblCustNmL.TextValue
                                    .txtCustCdMNew.TextValue = .txtCustCdM.TextValue
                                    .lblCustNmMNew.TextValue = .lblCustNmM.TextValue
                                    .txtGoodsCdCustNew.TextValue = .txtGoodsCdCust.TextValue
                                    .txtGoodsNmCustNew.TextValue = .txtGoodsNmCust.TextValue
                                    .lblGoodsCdNrsNew.TextValue = .lblGoodsCdNrs.TextValue

                            End Select
#End If
                            '容器変更(有／無)がチェック時のみロック解除
                            If .chkYoukiChange.Checked = True Then
                                Me._LMDConG.LockButton(.btnSakiAdd, False)
                                'START YANAI No.105
                            Else
                                Me._LMDConG.LockButton(.btnSakiAdd, True)
                                'END YANAI No.105
                            End If

                            '振替元課税区分の値を振替先課税区分の値にセット
                            .cmbTaxKbnNew.SelectedValue = .cmbTaxKbn.SelectedValue

                            '複写
                        Case LMD010C.ActionType.FUKUSHA

                            '検索項目ロック解除
                            Me._LMDConG.SetLockControl(.cmbSoko, False)
                            Me._LMDConG.SetLockControl(.cmbFurikaeKbn, False)
                            Me._LMDConG.SetLockControl(.imdFurikaeDate, False)
                            Me._LMDConG.SetLockControl(.chkYoukiChange, False)
                            Me._LMDConG.SetLockControl(.cmbToukiHokanKbn, False)

                            '振替元入力項目ロック解除
                            Me._LMDConG.SetLockControl(.txtCustCdL, False)
                            Me._LMDConG.SetLockControl(.txtCustCdM, False)
                            Me._LMDConG.SetLockControl(.txtOrderNo, False)
                            Me._LMDConG.SetLockControl(.txtGoodsCdCust, False)
                            Me._LMDConG.SetLockControl(.txtGoodsNmCust, False)
                            Me._LMDConG.SetLockControl(.txtLotNo, False)
                            Me._LMDConG.SetLockControl(.txtSerialNo, False)
                            Me._LMDConG.SetLockControl(.cmbNiyaku, False)
                            'Me._LMDConG.SetLockControl(.cmbTaxKbn, False)
                            Me._LMDConG.SetLockControl(.numKonsu, False)
                            Me._LMDConG.SetLockControl(.numCnt, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO1, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO2, False)
                            Me._LMDConG.SetLockControl(.txtSagyoCdO3, False)
                            Me._LMDConG.SetLockControl(.txtSyukkaRemark, False)
                            Me._LMDConG.LockButton(.btnMotoDel, False)

                            '振替先入力項目ロック
                            Me._LMDConG.LockButton(.btnSakiAdd, True)
                            Me._LMDConG.LockButton(.btnSakiDel, True)
                            Me._LMDConG.SetLockControl(.txtCustCdLNew, True)
                            Me._LMDConG.SetLockControl(.lblCustNmLNew, True)
                            Me._LMDConG.SetLockControl(.txtCustCdMNew, True)
                            Me._LMDConG.SetLockControl(.lblCustNmMNew, True)
                            Me._LMDConG.SetLockControl(.txtDenpNo, True)
                            Me._LMDConG.SetLockControl(.txtGoodsCdCustNew, True)
                            Me._LMDConG.SetLockControl(.txtGoodsNmCustNew, True)
                            Me._LMDConG.SetLockControl(.cmbNiyakuNew, True)
                            Me._LMDConG.SetLockControl(.cmbTaxKbnNew, True)
                            Me._LMDConG.SetLockControl(.chkInkoDateUmu, True)
                            Me._LMDConG.SetLockControl(.txtSagyoCdN1, True)
                            Me._LMDConG.SetLockControl(.txtSagyoCdN2, True)
                            Me._LMDConG.SetLockControl(.txtSagyoCdN3, True)
                            Me._LMDConG.SetLockControl(.txtNyukaRemark, True)
                            If String.IsNullOrEmpty(.cmbFurikaeKbn.SelectedValue.ToString) Then
                                '振替区分で先頭空白が選択された：初期入荷日引き継ぎをチェックする
                                .chkInkoDateUmu.Checked = True

                            ElseIf "01".Equals(.cmbFurikaeKbn.SelectedValue.ToString) Then
                                '振替区分で[荷主変更]が選択された：初期入荷日引き継ぎのチェックを外す
                                .chkInkoDateUmu.Checked = False

                            Else
                                '振替区分で上記以外が選択された：初期入荷日引き継ぎをチェックする
                                .chkInkoDateUmu.Checked = True
                            End If
                            '荷役量有無(振替元)に"無"をセット
                            .cmbNiyaku.SelectedValue = "00"
                            '荷役量有無(振替先)に"無"をセット
                            .cmbNiyakuNew.SelectedValue = "00"

                            '振替確定
                        Case LMD010C.ActionType.FURIKAEKAKUTEI
                            '画面項目を全ロックする
                            Call Me._LMDConG.SetLockControl(Me._Frm, Lock)
                            Me._LMDConG.LockButton(.btnSakiAdd, True)
                            Me._LMDConG.LockButton(.btnSakiDel, True)
                            Me._Frm.spdDtl.AllLock()
                            Me._Frm.sprDtlNew.AllLock()

                    End Select

                Case DispMode.INIT

                    Me._LMDConG.SetLockControl(.cmbSoko, False)
                    Me._LMDConG.SetLockControl(.cmbFurikaeKbn, False)
                    Me._LMDConG.SetLockControl(.imdFurikaeDate, False)
                    Me._LMDConG.SetLockControl(.chkYoukiChange, False)
                    Me._LMDConG.SetLockControl(.cmbToukiHokanKbn, False)
                    Me._LMDConG.LockButton(.btnMotoDel, True)
                    Me._LMDConG.LockButton(.btnSakiAdd, True)
                    Me._LMDConG.LockButton(.btnSakiDel, True)
                    .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
                    ' 初期入荷日を引き継いで在庫を振替えるにチェック
                    .chkInkoDateUmu.Checked = True
                    ' 当期保管料負担区分に"振替元"をセット
                    .cmbToukiHokanKbn.SelectedValue = "10"
                    '荷役量有無(振替元)に"無"をセット
                    .cmbNiyaku.SelectedValue = "00"
                    '荷役量有無(振替先)に"無"をセット
                    .cmbNiyakuNew.SelectedValue = "00"

            End Select

        End With

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d4 As Decimal = Convert.ToDecimal("9999")
            Dim d6 As Decimal = Convert.ToDecimal("999999")
            Dim d8 As Decimal = Convert.ToDecimal("99999999")
            Dim d9 As Decimal = Convert.ToDecimal("999999999")
            Dim d9_3 As Decimal = Convert.ToDecimal("999999999.999")
            Dim d10 As Decimal = Convert.ToDecimal("9999999999")
            Dim d11 As Decimal = Convert.ToDecimal("99999999999")

            'numberCellの桁数を設定する
            .numKonsu.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .numCnt.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0)
            .lblIrisuCnt.SetInputFields("##,###,##0", , 8, 1, , 0, 0, , d8, 0)
            .lblKosuCnt.SetInputFields("##,###,###,##0", , 11, 1, , 0, 0, , d11, 0)
            .lblHikiZanCnt.SetInputFields("##,###,###,##0", , 11, 1, , 0, 0, , d11, 0)
            .numIrime.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)
            .numIrimeNew.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , d9_3, 0)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal actionType As LMD010C.ActionType)

        With Me._Frm
            Select Case actionType
                Case LMD010C.ActionType.MAIN
                    .cmbNrsBrCd.Focus()
                Case LMD010C.ActionType.HENSHU
                    .txtCustCdL.Focus()
                Case LMD010C.ActionType.FUKUSHA
                    .txtCustCdL.Focus()
                Case LMD010C.ActionType.HIKIATE, LMD010C.ActionType.ZENRYO
                    .spdDtl.Focus()
                Case LMD010C.ActionType.FURIKAEMOTOKAKUTEI

                    '振替区分の値によって、フォーカスセットする位置を変更
#If False Then '要望番号2449対応 Changed 20151120 INOUE
                    Select Case .cmbFurikaeKbn.SelectedText
                        Case "荷主変更"
#Else
                    Select Case .cmbFurikaeKbn.SelectedValue.ToString()
                        Case LMD010C.FURIKAE_KBN_CUST
#End If
                            .txtCustCdLNew.Focus()
                        Case Else
                            .txtDenpNo.Focus()

                    End Select

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 戻る処理時の画面制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetCancelControl()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

#If False Then '要望番号2449対応 Changed 20151120 INOUE
        Dim key As String = Me._Frm.FunctionKey.F11ButtonName()
        Select Case key
            Case "振替確定"
#Else
        Select Case Me._EditActionType
            Case LMD010C.ActionType.FURIKAEMOTOKAKUTEI
#End If
                '振替先入力部クリア
                Call Me.ClearControlSaki()

                'ロック制御
                Call Me.SetModeAndStatus(DispMode.EDIT, RecordStatus.INIT)
                Call Me._LMDConG.SetLockControl(Me._Frm, lock)
                Call Me.SetControlsStatus(LMD010C.ActionType.FUKUSHA)
                Call Me.SetControlsStatus(LMD010C.ActionType.HENSHU)
                Call Me.SetFunctionKey(LMD010C.ActionType.HENSHU)
                Call Me.SetControlsStatus(LMD010C.ActionType.HIKIATE)

                'Focus設定
                Me._Frm.txtCustCdL.Focus()

        End Select



    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            '共通
            .lblFurikaeNo.TextValue = String.Empty
            .cmbNrsBrCd.SelectedValue = String.Empty
            .cmbSoko.SelectedValue = String.Empty

        End With

        Call Me.ClearControlHd()
        Call Me.ClearControlMoto()
        Call Me.ClearControlSaki()

    End Sub

    ''' <summary>
    ''' コントロール値のクリア(ヘッダ部)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlHd()

        With Me._Frm

            '初期
            .cmbFurikaeKbn.SelectedValue = String.Empty
            .imdFurikaeDate.TextValue = String.Empty
            .chkYoukiChange.Checked = False
            .cmbToukiHokanKbn.SelectedValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア(振替元入力部)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlMoto()

        With Me._Frm

            '編集
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .txtOrderNo.TextValue = String.Empty
            .txtGoodsCdCust.TextValue = String.Empty
            .txtGoodsNmCust.TextValue = String.Empty
            .lblGoodsCdNrs.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .txtSerialNo.TextValue = String.Empty
            .numIrime.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            .lblIrimeTanni.TextValue = String.Empty
            .lblIrimeTanniKB.TextValue = String.Empty
#Else
            .lblIrimeTanni.KbnValue = String.Empty
#End If
            .cmbNiyaku.SelectedValue = String.Empty
            .cmbTaxKbn.SelectedValue = String.Empty
            .numKonsu.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            .lblKonsuTanni.TextValue = String.Empty
            .lblKonsuTanniKB.TextValue = String.Empty
#Else
            .lblKonsuTanni.KbnValue = String.Empty
#End If
            .numCnt.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            .lblCntTani.TextValue = String.Empty
            .lblCntTaniKB.TextValue = String.Empty
#Else
            .lblCntTani.KbnValue = String.Empty
#End If
            .lblIrisuCnt.TextValue = String.Empty
            .lblKosuCnt.TextValue = String.Empty
            .lblHikiSumiCnt.TextValue = String.Empty
            .lblHikiZanCnt.TextValue = String.Empty
            .txtSagyoCdO1.TextValue = String.Empty
            .lblSagyoNmO1.TextValue = String.Empty
            .txtSagyoCdO2.TextValue = String.Empty
            .lblSagyoNmO2.TextValue = String.Empty
            .txtSagyoCdO3.TextValue = String.Empty
            .lblSagyoNmO3.TextValue = String.Empty
            .lblSagyoInNmO1.TextValue = String.Empty
            .lblSagyoInNmO2.TextValue = String.Empty
            .lblSagyoInNmO3.TextValue = String.Empty
            .txtSyukkaRemark.TextValue = String.Empty
            .spdDtl.Sheets(0).RowCount = 0

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア(振替先入力部)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlSaki()

        With Me._Frm

            '振替元確定後
            .txtCustCdLNew.TextValue = String.Empty
            .lblCustNmLNew.TextValue = String.Empty
            .txtCustCdMNew.TextValue = String.Empty
            .lblCustNmMNew.TextValue = String.Empty
            .txtDenpNo.TextValue = String.Empty
            .txtGoodsCdCustNew.TextValue = String.Empty
            .txtGoodsNmCustNew.TextValue = String.Empty
            .lblGoodsCdNrsNew.TextValue = String.Empty
            .cmbNiyakuNew.SelectedValue = String.Empty
            .cmbTaxKbnNew.SelectedValue = String.Empty
            .chkInkoDateUmu.Checked = False
            .txtSagyoCdN1.TextValue = String.Empty
            .lblSagyoNmN1.TextValue = String.Empty
            .txtSagyoCdN2.TextValue = String.Empty
            .lblSagyoNmN2.TextValue = String.Empty
            .txtSagyoCdN3.TextValue = String.Empty
            .lblSagyoNmN3.TextValue = String.Empty
            .lblSagyoInNmN1.TextValue = String.Empty
            .lblSagyoInNmN2.TextValue = String.Empty
            .lblSagyoInNmN3.TextValue = String.Empty
            .txtNyukaRemark.TextValue = String.Empty
            .sprDtlNew.Sheets(0).RowCount = 0

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア(複写時)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControlFukusha()

        With Me._Frm

            .lblFurikaeNo.TextValue = String.Empty
            .txtOrderNo.TextValue = String.Empty
            .txtGoodsCdCust.TextValue = String.Empty
            .txtGoodsNmCust.TextValue = String.Empty
            .lblGoodsCdNrs.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .txtSerialNo.TextValue = String.Empty
            .numIrime.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            .lblIrimeTanni.TextValue = String.Empty
            .lblIrimeTanniKB.TextValue = String.Empty
#Else
            .lblIrimeTanni.KbnValue = String.Empty

#End If
            .cmbNiyaku.SelectedValue = String.Empty
            .cmbTaxKbn.SelectedValue = String.Empty
            .numKonsu.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            .lblKonsuTanni.TextValue = String.Empty
            .lblKonsuTanniKB.TextValue = String.Empty
#Else
            .lblKonsuTanni.KbnValue = String.Empty
#End If
            .numCnt.Value = 0
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            .lblCntTani.TextValue = String.Empty
            .lblCntTaniKB.TextValue = String.Empty
#Else
            .lblCntTani.KbnValue = String.Empty
#End If
            .lblIrisuCnt.TextValue = String.Empty
            .lblKosuCnt.TextValue = String.Empty
            .lblHikiSumiCnt.TextValue = String.Empty
            .lblHikiZanCnt.TextValue = String.Empty
            .txtSagyoCdO1.TextValue = String.Empty
            .lblSagyoNmO1.TextValue = String.Empty
            .txtSagyoCdO2.TextValue = String.Empty
            .lblSagyoNmO2.TextValue = String.Empty
            .txtSagyoCdO3.TextValue = String.Empty
            .lblSagyoNmO3.TextValue = String.Empty
            .lblSagyoInNmO1.TextValue = String.Empty
            .lblSagyoInNmO2.TextValue = String.Empty
            .lblSagyoInNmO3.TextValue = String.Empty
            .txtSyukkaRemark.TextValue = String.Empty
            .txtCustCdLNew.TextValue = String.Empty
            .lblCustNmLNew.TextValue = String.Empty
            .txtCustCdMNew.TextValue = String.Empty
            .lblCustNmMNew.TextValue = String.Empty
            .txtDenpNo.TextValue = String.Empty
            .txtGoodsCdCustNew.TextValue = String.Empty
            .txtGoodsNmCustNew.TextValue = String.Empty
            .lblGoodsCdNrsNew.TextValue = String.Empty
            .cmbNiyakuNew.SelectedValue = String.Empty
            .cmbTaxKbnNew.SelectedValue = String.Empty

            If String.IsNullOrEmpty(.cmbFurikaeKbn.SelectedValue.ToString) Then
                '振替区分で先頭空白が選択された：初期入荷日引き継ぎをチェックする
                .chkInkoDateUmu.Checked = True

            ElseIf "01".Equals(.cmbFurikaeKbn.SelectedValue.ToString) Then
                '振替区分で[荷主変更]が選択された：初期入荷日引き継ぎのチェックを外す
                .chkInkoDateUmu.Checked = False

            Else
                '振替区分で上記以外が選択された：初期入荷日引き継ぎをチェックする
                .chkInkoDateUmu.Checked = True
            End If

            .txtSagyoCdN1.TextValue = String.Empty
            .lblSagyoNmN1.TextValue = String.Empty
            .txtSagyoCdN2.TextValue = String.Empty
            .lblSagyoNmN2.TextValue = String.Empty
            .txtSagyoCdN3.TextValue = String.Empty
            .lblSagyoNmN3.TextValue = String.Empty
            .lblSagyoInNmN1.TextValue = String.Empty
            .lblSagyoInNmN2.TextValue = String.Empty
            .lblSagyoInNmN3.TextValue = String.Empty
            .txtNyukaRemark.TextValue = String.Empty
            .spdDtl.CrearSpread()
            .sprDtlNew.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' 振替先のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFurikaeSakiLock()

        With Me._Frm

            Dim unlock As Boolean = False
            Dim lock As Boolean = True

            If .chkYoukiChange.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                If .sprDtlNew.ActiveSheet.Rows.Count = 0 Then
                    '商品コードロック解除
                    Me._LMDConG.LockText(.txtGoodsCdCustNew, unlock)
                    Me._LMDConG.LockText(.txtGoodsNmCustNew, unlock)
                ElseIf .sprDtlNew.ActiveSheet.Rows.Count > 0 Then
                    '商品コードロック
                    Me._LMDConG.LockText(.txtGoodsCdCustNew, lock)
                    Me._LMDConG.LockText(.txtGoodsNmCustNew, lock)
                End If
            End If

        End With

    End Sub


#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(振替元)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.DEF, " ", 20, True)
        Public Shared MOTO_INKA_NO_S As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_INKA_NO_S, "出荷管理番号" & vbCrLf & "(小)", 100, True)
        Public Shared MOTO_TOU_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_TOU_NO, "棟", 50, True)
        Public Shared MOTO_SITU_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_SITU_NO, "室", 40, True)
        Public Shared MOTO_ZONE_CD As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_ZONE_CD, "ZONE", 40, True)
        Public Shared MOTO_LOCA As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_LOCA, "ロケーション", 100, True)
        Public Shared MOTO_LOT_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_LOT_NO, "ロット№", 100, True)
        Public Shared MOTO_HURIKAE_KOSU As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_HURIKAE_KOSU, "振替個数", 80, True)
        Public Shared MOTO_ZAN_KOSU As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_ZAN_KOSU, "残個数", 80, True)
        Public Shared MOTO_IRIME As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_IRIME, "入目", 60, True)
        Public Shared MOTO_HURIKAE_SURYO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_HURIKAE_SURYO, "振替数量", 80, True)
        Public Shared MOTO_ZAN_SURYO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_ZAN_SURYO, "残数量", 80, True)
        Public Shared MOTO_ZAI_REC As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_ZAI_REC, "在庫レコード" & vbCrLf & "番号", 100, True)
        Public Shared MOTO_LT_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_LT_DATE, "賞味期限", 80, True)
        Public Shared MOTO_GOODS_COND_NM_1 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_COND_NM_1, "状態 中身", 80, True)
        Public Shared MOTO_GOODS_COND_NM_2 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_COND_NM_2, "状態 外観", 80, True)
        Public Shared MOTO_GOODS_COND_NM_3 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_COND_NM_3, "状態 荷主", 80, True)
        Public Shared MOTO_OFB_NM As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_OFB_NM, "薄外品", 80, True)
        Public Shared MOTO_SPD_NM As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_SPD_NM, "保留品", 80, True)
        Public Shared KEEP_GOODS_NM As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.KEEP_GOODS_NM, "キープ品", 115, False)
        Public Shared MOTO_SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_SERIAL_NO, "シリアル№", 100, True)
        Public Shared MOTO_GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_CRT_DATE, "製造日", 80, True)
        Public Shared MOTO_DEST_CD As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_DEST_CD, "届先コード", 100, True)
        Public Shared MOTO_ALLOC_PRIORITY_NM As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_ALLOC_PRIORITY_NM, "割当優先", 80, True)
        Public Shared MOTO_BUYER_ORD_NO_L As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_BUYER_ORD_NO_L, "注文番号", 100, True)
        Public Shared MOTO_INKA_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_INKA_DATE, "入荷日", 80, True)
        Public Shared MOTO_INKA_YOTEI_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_INKA_YOTEI_DATE, "入荷予定日", 90, True)

        '非表示項目
        Public Shared MOTO_UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_UPDATE_DATE, "更新日付", 80, False)
        Public Shared MOTO_UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_UPDATE_TIME, "更新日時", 90, False)
        Public Shared MOTO_PORA_ZAI_QT As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_PORA_ZAI_QT, "実予在庫数量", 90, False)
        Public Shared MOTO_HOKAN_YN As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_HOKAN_YN, "保管料有無", 90, False)
        Public Shared MOTO_GOODS_COND_KB_1 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_COND_KB_1, "状態 中身区分", 80, False)
        Public Shared MOTO_GOODS_COND_KB_2 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_COND_KB_2, "状態 外観区分", 80, False)
        Public Shared MOTO_GOODS_COND_KB_3 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_GOODS_COND_KB_3, "状態 荷主区分", 80, False)
        Public Shared MOTO_OFB_KB As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_OFB_KB, "薄外品区分", 80, False)
        Public Shared MOTO_SPD_KB As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_SPD_KB, "保留品区分", 80, False)
        Public Shared MOTO_REMARK As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_REMARK, "備考(社内)", 80, False)
        Public Shared MOTO_REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_REMARK_OUT, "備考(社外)", 80, False)
        Public Shared MOTO_ALLOC_PRIORITY As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_ALLOC_PRIORITY, "割当優先区分", 80, False)
        Public Shared MOTO_BYK_KEEP_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtl.MOTO_BYK_KEEP_GOODS_CD, "BYKキープ品コード", 150, False)


    End Class

    ''' <summary>
    ''' スプレッド列定義体(振替先)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailNewDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.DEF, " ", 20, True)
        Public Shared SAKI_INKA_NO_S As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_INKA_NO_S, "入荷管理番号" & vbCrLf & "(小)", 100, True)
        Public Shared SAKI_TOU_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_TOU_NO, "棟", 50, True)
        Public Shared SAKI_SITU_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_SITU_NO, "室", 40, True)
        Public Shared SAKI_ZONE_CD As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_ZONE_CD, "ZONE", 40, True)
        Public Shared SAKI_LOCA As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_LOCA, "ロケーション", 100, True)
        Public Shared SAKI_LOT_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_LOT_NO, "ロット№", 100, True)
        Public Shared SAKI_HURIKAE_KOSU As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_HURIKAE_KOSU, "振替個数", 80, True)
        Public Shared SAKI_HURIKAE_TANNI As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_HURIKAE_TANNI, "個数単位", 80, True)
        Public Shared SAKI_IRIME As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_IRIME, "入目", 60, True)
        Public Shared SAKI_SURYO_TANNI As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_SURYO_TANNI, "入目単位", 80, True)
        Public Shared SAKI_HURIKAE_SURYO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_HURIKAE_SURYO, "振替数量", 80, True)
        Public Shared SAKI_REMARK As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_REMARK, "備考小(社内)", 120, True)
        Public Shared SAKI_LT_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_LT_DATE, "賞味期限", 80, True)
        Public Shared SAKI_REMARK_OUT As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_REMARK_OUT, "備考小(社外)", 120, True)
        Public Shared SAKI_GOODS_COND_KB_1 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_GOODS_COND_KB_1, "状態 中身", 80, True)
        Public Shared SAKI_GOODS_COND_KB_2 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_GOODS_COND_KB_2, "状態 外観", 80, True)
        Public Shared SAKI_GOODS_COND_KB_3 As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_GOODS_COND_KB_3, "状態 荷主", 80, True)
        Public Shared SAKI_OFB_KB As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_OFB_KB, "薄外品", 80, True)
        Public Shared SAKI_SPD_KB As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_SPD_KB, "保留品", 80, True)
        Public Shared SAKI_BYK_KEEP_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_BYK_KEEP_GOODS_CD, "キープ品", 115, False)
        Public Shared SAKI_SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_SERIAL_NO, "シリアル№", 100, True)
        Public Shared SAKI_GOODS_CRT_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_GOODS_CRT_DATE, "製造日", 80, True)
        Public Shared SAKI_DEST_CD As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_DEST_CD, "届先コード", 100, True)
        Public Shared SAKI_ALLOC_PRIORITY As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_ALLOC_PRIORITY, "割当優先", 80, True)
        Public Shared SAKI_INKA_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_INKA_DATE, "入荷日", 80, True)
        Public Shared SAKI_INKA_YOTEI_DATE As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_INKA_YOTEI_DATE, "入荷予定日", 90, True)

        '非表示項目
        Public Shared SAKI_ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMD010C.SprColumnIndexSprDtlNew.SAKI_ZAI_REC_NO, "在庫レコード" & vbCrLf & "番号", 100, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .spdDtl.CrearSpread()
            .sprDtlNew.CrearSpread()

            '列数設定
            .spdDtl.Sheets(0).ColumnCount = 40
            .sprDtlNew.Sheets(0).ColumnCount = 28

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.spdDtl.SetColProperty(New sprDetailDef)
            '.sprDtlNew.SetColProperty(New sprDetailNewDef)
            .spdDtl.SetColProperty(New sprDetailDef, False)
            .sprDtlNew.SetColProperty(New sprDetailNewDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(チェック列で固定)
            .spdDtl.ActiveSheet.FrozenColumnCount = LMD010G.sprDetailDef.DEF.ColNo + 1
            .sprDtlNew.ActiveSheet.FrozenColumnCount = LMD010G.sprDetailNewDef.DEF.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(振替元の明細)
    ''' </summary>
    ''' <param name="hikiZenFlg">引当時⇒0 全量時⇒1 </param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable, ByVal hikiZenFlg As Integer)

        Dim spr As LMSpread = Me._Frm.spdDtl

        With spr

            .SuspendLayout()

            .CrearSpread()

            Dim lngcnt As Integer = dt.Rows.Count - 1

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim number9 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ",")
            Dim number9to3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.999, True, 3, , ",")

            Dim dr As DataRow = Nothing
            Dim rowCnt As Integer = 0

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            '値設定
            For i As Integer = 0 To lngcnt

                dr = dt.Rows(i)

                If i = 0 Then
                    Dim isVisibleKeepGoods As Boolean = False
                    If dr.Item("IS_BYK_KEEP_GOODS_CD").ToString() = "1" Then
                        isVisibleKeepGoods = True
                    End If
                    .ActiveSheet.Columns(LMD010G.sprDetailDef.KEEP_GOODS_NM.ColNo).Visible = isVisibleKeepGoods
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                '残個数の計算
                Dim allocCanNb As Decimal = Convert.ToDecimal(dr.Item("ALLOC_CAN_NB_HOZON").ToString())
                Dim alctdKosu As Decimal = Convert.ToDecimal(dr.Item("HIKI_KOSU").ToString())
                Dim zanKosu As Decimal = allocCanNb - alctdKosu
                '残数量の計算
                Dim allocCanQt As Decimal = Convert.ToDecimal(dr.Item("ALLOC_CAN_QT_HOZON").ToString())
                Dim alctdSuryo As Decimal = Convert.ToDecimal(dr.Item("HIKI_SURYO").ToString())
                Dim zanSuryo As Decimal = allocCanQt - alctdSuryo

                'セルスタイル設定
                .SetCellStyle(i, LMD010G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_INKA_NO_S.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_ZONE_CD.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_LOCA.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, number9)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_ZAN_KOSU.ColNo, number9)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_IRIME.ColNo, number9to3)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, number9to3)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_ZAN_SURYO.ColNo, number9to3)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_LT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_1.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_2.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_3.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_OFB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_SPD_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.KEEP_GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY_NM.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_BUYER_ORD_NO_L.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_INKA_YOTEI_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_UPDATE_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_UPDATE_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_PORA_ZAI_QT.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_HOKAN_YN.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_1.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_2.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_3.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_OFB_KB.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_SPD_KB.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_REMARK_OUT.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY.ColNo, sLabel)
                .SetCellStyle(i, LMD010G.sprDetailDef.MOTO_BYK_KEEP_GOODS_CD.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMD010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_INKA_NO_S.ColNo, String.Empty)
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_LOCA.ColNo, dr.Item("LOCA").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_ZAN_KOSU.ColNo, zanKosu.ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_IRIME.ColNo, dr.Item("IRIME").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_ZAN_SURYO.ColNo, zanSuryo.ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo, dr.Item("ZAI_REC_NO").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_LT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_1.ColNo, dr.Item("GOODS_COND_NM_1").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_2.ColNo, dr.Item("GOODS_COND_NM_2").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_3.ColNo, dr.Item("GOODS_COND_NM_3").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_OFB_NM.ColNo, dr.Item("OFB_KB_NM").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_SPD_NM.ColNo, dr.Item("SPD_KB_NM").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.KEEP_GOODS_NM.ColNo, dr.Item("KEEP_GOODS_NM").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("GOODS_CRT_DATE").ToString()))
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_DEST_CD.ColNo, dr.Item("DEST_CD_P").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY_NM.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_BUYER_ORD_NO_L.ColNo, dr.Item("BUYER_ORD_NO_DTL").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKO_DATE").ToString()))
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_INKA_YOTEI_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKO_PLAN_DATE").ToString()))
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_UPDATE_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_PORA_ZAI_QT.ColNo, dr.Item("PORA_ZAI_QT").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HOKAN_YN.ColNo, dr.Item("HOKAN_YN").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_1.ColNo, dr.Item("GOODS_COND_KB_1").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_2.ColNo, dr.Item("GOODS_COND_KB_2").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_3.ColNo, dr.Item("GOODS_COND_KB_3").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_OFB_KB.ColNo, dr.Item("OFB_KB").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_SPD_KB.ColNo, dr.Item("SPD_KB").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_REMARK_OUT.ColNo, dr.Item("REMARK_OUT").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY.ColNo, dr.Item("ALLOC_PRIORITY").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_BYK_KEEP_GOODS_CD.ColNo, dr.Item("BYK_KEEP_GOODS_CD").ToString())

                'Select Case hikiZenFlg
                '    Case 0 '引当
                '        .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, dr.Item("HIKI_KOSU").ToString())
                '        .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, Convert.ToDecimal(dr.Item("HIKI_SURYO").ToString()).ToString())
                '    Case 1 '全量
                '        .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, zanKosu.ToString())
                '        .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, zanSuryo.ToString())
                'End Select
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, dr.Item("HIKI_KOSU").ToString())
                .SetCellValue(i, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, Convert.ToDecimal(dr.Item("HIKI_SURYO").ToString()).ToString())

            Next

            With _Frm

                If hikiZenFlg = 0 Then
                    '引当時のみセット
                    .txtLotNo.TextValue = dr.Item("LOT_NO_L").ToString()
                    .txtSerialNo.TextValue = dr.Item("SERIAL_NO").ToString()
                    .numIrime.Value = dr.Item("IRIME_L").ToString()
                    .numKonsu.Value = Convert.ToDecimal(dr.Item("KONSU").ToString())
                    .numCnt.Value = Convert.ToDecimal(dr.Item("HASU").ToString())
                End If

            End With

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(振替先の明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSakiSpread(ByVal arr As ArrayList, ByVal rowAdd As String, ByVal calcSuryo As Decimal, ByVal calcKosu As Decimal)

        Dim spr As LMSpread = Me._Frm.sprDtlNew
        Dim rowCnt As Integer = 0

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            If LMConst.FLG.ON.Equals(rowAdd) = False Then
                .CrearSpread()
            End If

            'セルに設定するスタイルの取得
            Dim unlock As Boolean = False
            Dim lock As Boolean = True
            Dim furikaeKosuLock As Boolean = lock
            Dim numIrimeLock As Boolean = lock
            If Me._Frm.chkYoukiChange.GetBinaryValue().Equals(LMConst.FLG.ON) Then
                furikaeKosuLock = unlock
                numIrimeLock = unlock
            End If
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim number9 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, lock, 0, , ",")
            Dim numFurikaeKosu As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, furikaeKosuLock, 0, , ",")
            Dim number9to3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.999, lock, 3, , ",")
            Dim numIrime As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999.999, numIrimeLock, 3, , ",")

            Dim dr As DataRow = Nothing
            Dim sCustM As StyleInfo = Me.StyleInfoCustCond(spr)
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim loca As String = String.Empty
            Dim lotNo As String = String.Empty
            Dim furikaeKosu As String = String.Empty
            Dim furikaeSuryo As String = String.Empty
            Dim irime As String = String.Empty
            Dim remark As String = String.Empty
            Dim remarkOut As String = String.Empty
            Dim ltDate As String = String.Empty
            Dim goodsCondKb1 As String = String.Empty
            Dim goodsCondKb2 As String = String.Empty
            Dim goodsCondKb3 As String = String.Empty
            Dim ofbKb As String = String.Empty
            Dim spdKb As String = String.Empty
            Dim bykKeepGoodsCd As String = String.Empty
            Dim serialNo As String = String.Empty
            Dim goodsCrtDate As String = String.Empty
            Dim destCd As String = String.Empty
            Dim allocPriority As String = String.Empty
            Dim inkaDate As String = String.Empty
            Dim inkaYoteiDate As String = String.Empty
            Dim zaiRecNo As String = String.Empty
            Dim zanKosu As Decimal = 0
            Dim zanSuryo As Decimal = 0

            If LMConst.FLG.ON.Equals(rowAdd) = False Then
                .ActiveSheet.Columns(LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo).Visible =
                    Me._Frm.spdDtl.ActiveSheet.Columns(LMD010G.sprDetailDef.KEEP_GOODS_NM.ColNo).Visible
            End If

            '振替元のスプレッドでチェックされている明細のみをコピー
            For i As Integer = 0 To arr.Count - 1

                sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

                touNo = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_TOU_NO.ColNo)).ToString()
                situNo = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_SITU_NO.ColNo)).ToString()
                zoneCd = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_ZONE_CD.ColNo)).ToString()
                loca = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_LOCA.ColNo)).ToString()
                lotNo = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo)).ToString()
                furikaeKosu = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo)).ToString()
                furikaeSuryo = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo)).ToString()
                irime = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_IRIME.ColNo)).ToString()
                remark = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_REMARK.ColNo)).ToString()
                remarkOut = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_REMARK_OUT.ColNo)).ToString()
                ltDate = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_LT_DATE.ColNo)).ToString()
                goodsCondKb1 = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_1.ColNo)).ToString()
                goodsCondKb2 = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_2.ColNo)).ToString()
                goodsCondKb3 = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_3.ColNo)).ToString()
                ofbKb = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_OFB_KB.ColNo)).ToString()
                spdKb = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_SPD_KB.ColNo)).ToString()
                bykKeepGoodsCd = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_BYK_KEEP_GOODS_CD.ColNo)).ToString()
                serialNo = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_SERIAL_NO.ColNo)).ToString()
                goodsCrtDate = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_GOODS_CRT_DATE.ColNo)).ToString()
                destCd = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_DEST_CD.ColNo)).ToString()
                allocPriority = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY.ColNo)).ToString()
                inkaDate = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo)).ToString()
                inkaYoteiDate = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_INKA_YOTEI_DATE.ColNo)).ToString()
                zaiRecNo = Me._LMDConG.GetCellValue(Me._Frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo)).ToString()

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.DEF.ColNo, sDEF)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_INKA_NO_S.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 2, False))
                'START YANAI 要望番号705
                '.SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 1, False))
                '.SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 1, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 2, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 2, False))
                'END YANAI 要望番号705
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_LOCA.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 10, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_LOT_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo, numFurikaeKosu)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_TANNI.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo, numIrime)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_SURYO_TANNI.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo, number9to3)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_REMARK.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_LT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(spr, False, CellType.DateTimeFormat.ShortDate))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_REMARK_OUT.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 15, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_1.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_S005, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_2.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_S006, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_3.ColNo, sCustM)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_OFB_KB.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_B002, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_SPD_KB.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_H003, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMD010C.KbnConst.BYK_KEEP_GOODS_CD, False))
                'START YANAI 要望番号992
                '.SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 40, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 40, False))
                'END YANAI 要望番号992
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_CRT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(spr, False, CellType.DateTimeFormat.ShortDate))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_DEST_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 15, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_ALLOC_PRIORITY.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_W001, False))
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_INKA_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_INKA_YOTEI_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMD010G.sprDetailNewDef.SAKI_ZAI_REC_NO.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_INKA_NO_S.ColNo, String.Empty)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo, touNo)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, situNo)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo, zoneCd)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_LOCA.ColNo, loca)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_LOT_NO.ColNo, lotNo)

                If LMConst.FLG.ON.Equals(rowAdd) = True Then
                    '行追加ボタン押下時は振替数量、振替個数を計算してセット
                    If 0 < Convert.ToDecimal(furikaeKosu) - calcKosu Then
                        .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo, (Convert.ToDecimal(furikaeKosu) - calcKosu).ToString())
                        .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo, (Convert.ToDecimal(furikaeSuryo) - calcSuryo).ToString())
                    Else
                        .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo, furikaeKosu.ToString())
                        .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo, furikaeSuryo.ToString())
                    End If
                Else
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo, furikaeKosu)
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo, furikaeSuryo)
                End If

                '.SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_TANNI.ColNo, Me._Frm.lblCntTani.TextValue)

                '容器変更有の場合は商品マスタ参照にて、保持しておいた入目をセットする
                If Me._Frm.chkYoukiChange.Checked = True Then
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo, Me._Frm.numIrimeNew.Value.ToString())
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_SURYO_TANNI.ColNo, Me._Frm.lblIrimeTanniNew.TextValue)
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_TANNI.ColNo, Me._Frm.lblKosuTanniNew.TextValue)

                Else
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo, irime)
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_SURYO_TANNI.ColNo, Me._Frm.lblIrimeTanni.TextValue)
                    .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_HURIKAE_TANNI.ColNo, Me._Frm.lblCntTani.TextValue)
                End If


                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_REMARK.ColNo, remark)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_LT_DATE.ColNo, DateFormatUtility.EditSlash(ltDate))
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_REMARK_OUT.ColNo, remarkOut)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_1.ColNo, goodsCondKb1)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_2.ColNo, goodsCondKb2)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_3.ColNo, goodsCondKb3)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_OFB_KB.ColNo, ofbKb)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_SPD_KB.ColNo, spdKb)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo, bykKeepGoodsCd)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_SERIAL_NO.ColNo, serialNo)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(goodsCrtDate))
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_DEST_CD.ColNo, destCd)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_ALLOC_PRIORITY.ColNo, allocPriority)
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_INKA_DATE.ColNo, DateFormatUtility.EditSlash(inkaDate))
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_INKA_YOTEI_DATE.ColNo, DateFormatUtility.EditSlash(inkaYoteiDate))
                .SetCellValue(rowCnt, LMD010G.sprDetailNewDef.SAKI_ZAI_REC_NO.ColNo, zaiRecNo)

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにコンボボックスを再生成(振替先の明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub setCustCond(ByVal custCdL As String)

        Dim spr As LMSpread = Me._Frm.sprDtlNew

        With spr

            Dim sCustM As StyleInfo = Me.StyleInfoSakiCustCond(spr, custCdL)

            '振替先のスプレッド分ループを行い、状態荷主コンボの再生成を行う
            For i As Integer = 0 To .ActiveSheet.Rows.Count - 1
                .SetCellStyle(i, LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_3.ColNo, sCustM)
            Next

        End With

    End Sub

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.CUSTCOND _
                                                  , "JOTAI_CD" _
                                                  , "JOTAI_NM" _
                                                  , False _
                                                  , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                                                  , New String() {Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), Me._Frm.txtCustCdL.TextValue} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

    ''' <summary>
    ''' 振替先のセルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoSakiCustCond(ByVal spr As LMSpread, ByVal custCdL As String) As StyleInfo

        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.CUSTCOND _
                                                  , "JOTAI_CD" _
                                                  , "JOTAI_NM" _
                                                  , False _
                                                  , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                                                  , New String() {Me._Frm.cmbNrsBrCd.SelectedValue.ToString(), custCdL} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

#End Region

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

#End Region

#End Region

#Region "削除予定（仕様変更）"

    ' ''' <summary>
    ' ''' 引当実行2回目以降の場合の設定(行追加Ver)
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend Sub SetHikiSpread(ByVal dt As DataTable, ByVal arr As ArrayList)

    '    Dim spr As LMSpread = Me._Frm.spdDtl
    '    Dim rowCnt As Integer = 0

    '    With spr

    '        .SuspendLayout()

    '        Dim lngcnt As Integer = arr.Count - 1

    '        'セルに設定するスタイルの取得
    '        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
    '        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
    '        Dim number9 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ",")
    '        Dim number9to3 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.999, True, 3, , ",")

    '        Dim dr As DataRow = Nothing
    '        Dim setFlg As Boolean = False

    '        sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

    '        '値設定
    '        For i As Integer = 0 To lngcnt

    '            dr = dt.Rows(i)
    '            '設定する行数を設定
    '            rowCnt = .ActiveSheet.Rows.Count

    '            '行追加
    '            .ActiveSheet.AddRows(rowCnt, 1)

    '            Dim allocCanNb As Decimal = Convert.ToDecimal(dr.Item("ALLOC_CAN_NB_HOZON").ToString())
    '            Dim alctdKosu As Decimal = Convert.ToDecimal(dr.Item("HIKI_KOSU").ToString())
    '            Dim zanKosu As Decimal = allocCanNb - alctdKosu
    '            '残数量の計算
    '            Dim allocCanQt As Decimal = Convert.ToDecimal(dr.Item("ALLOC_CAN_QT_HOZON").ToString())
    '            Dim alctdSuryo As Decimal = Convert.ToDecimal(dr.Item("HIKI_SURYO").ToString())
    '            Dim zanSuryo As Decimal = allocCanQt - alctdSuryo

    '            'セルスタイル設定
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.DEF.ColNo, sDEF)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_INKA_NO_S.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_TOU_NO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_SITU_NO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_ZONE_CD.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_LOCA.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, number9)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_ZAN_KOSU.ColNo, number9)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_IRIME.ColNo, number9to3)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, number9to3)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_ZAN_SURYO.ColNo, number9to3)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_LT_DATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_1.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_2.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_3.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_OFB_NM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_SPD_NM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_SERIAL_NO.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_CRT_DATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_DEST_CD.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY_NM.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_BUYER_ORD_NO_L.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_INKA_YOTEI_DATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_UPDATE_DATE.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_UPDATE_TIME.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_PORA_ZAI_QT.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_HOKAN_YN.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_1.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_2.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_3.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_OFB_KB.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_SPD_KB.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_REMARK.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_REMARK_OUT.ColNo, sLabel)
    '            .SetCellStyle(rowCnt, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY.ColNo, sLabel)


    '            'セルに値を設定
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_INKA_NO_S.ColNo, String.Empty)
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_LOCA.ColNo, dr.Item("LOCA").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, dr.Item("ALCTD_NB").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_ZAN_KOSU.ColNo, zanKosu.ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_IRIME.ColNo, dr.Item("IRIME").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, Convert.ToDecimal(dr.Item("ALCTD_QT").ToString()).ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_ZAN_SURYO.ColNo, zanSuryo.ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo, dr.Item("ZAI_REC_NO").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_LT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_1.ColNo, dr.Item("GOODS_COND_NM_1").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_2.ColNo, dr.Item("GOODS_COND_NM_2").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_NM_3.ColNo, dr.Item("GOODS_COND_NM_3").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_OFB_NM.ColNo, dr.Item("OFB_KB_NM").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_SPD_NM.ColNo, dr.Item("SPD_KB_NM").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_CRT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("GOODS_CRT_DATE").ToString()))
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_DEST_CD.ColNo, dr.Item("DEST_CD_P").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY_NM.ColNo, dr.Item("ALLOC_PRIORITY_NM").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_BUYER_ORD_NO_L.ColNo, dr.Item("BUYER_ORD_NO_DTL").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_INKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKO_DATE").ToString()))
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_INKA_YOTEI_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKO_PLAN_DATE").ToString()))
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_UPDATE_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_PORA_ZAI_QT.ColNo, dr.Item("PORA_ZAI_QT").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_HOKAN_YN.ColNo, dr.Item("HOKAN_YN").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_1.ColNo, dr.Item("GOODS_COND_KB_1").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_2.ColNo, dr.Item("GOODS_COND_KB_2").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_GOODS_COND_KB_3.ColNo, dr.Item("GOODS_COND_KB_3").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_OFB_KB.ColNo, dr.Item("OFB_KB").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_SPD_KB.ColNo, dr.Item("SPD_KB").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_REMARK.ColNo, dr.Item("REMARK").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_REMARK_OUT.ColNo, dr.Item("REMARK_OUT").ToString())
    '            .SetCellValue(rowCnt, LMD010G.sprDetailDef.MOTO_ALLOC_PRIORITY.ColNo, dr.Item("ALLOC_PRIORITY").ToString())

    '            '画面項目セットの識別用
    '            setFlg = True
    '        Next

    '        With _Frm

    '            'drが存在する場合のみ画面項目へセットを行う
    '            If setFlg = True Then
    '                .txtLotNo.TextValue = dr.Item("LOT_NO_L").ToString()
    '                .txtSerialNo.TextValue = dr.Item("SERIAL_NO").ToString()
    '                .numIrime.Value = dr.Item("IRIME_L").ToString()
    '                .numKonsu.Value = Convert.ToDecimal(dr.Item("KONSU").ToString())
    '                .numCnt.Value = Convert.ToDecimal(dr.Item("HASU").ToString())

    '            End If

    '        End With

    '        .ResumeLayout(True)

    '    End With

    'End Sub

    '''' <summary>
    '''' 引当実行2回目以降の場合の設定(振替数量、振替個数再計算Ver)
    '''' </summary>
    '''' <remarks></remarks>
    'Friend Sub SetHikiCalcSpread(ByVal dt As DataTable, ByVal arr As ArrayList, ByVal arrDt As ArrayList)

    '    Dim spr As LMSpread = Me._Frm.spdDtl
    '    Dim rowCnt As Integer = 0

    '    With spr

    '        .SuspendLayout()

    '        Dim lngcnt As Integer = arr.Count - 1

    '        'セルに設定するスタイルの取得
    '        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
    '        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
    '        Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

    '        Dim dr As DataRow = Nothing

    '        sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

    '        '値設定
    '        For i As Integer = 0 To lngcnt

    '            Dim j As Integer = Convert.ToInt32(arr(i))

    '            dr = dt.Rows(Convert.ToInt32(arrDt(i)))

    '            '設定する行数を設定
    '            rowCnt = .ActiveSheet.Rows.Count

    '            Dim furikaeKosu As String = String.Empty
    '            Dim furikaeSuryo As String = String.Empty
    '            Dim hikifurikaeKosu As Decimal = 0
    '            Dim hikifurikaeSuryo As Decimal = 0

    '            Dim zanHurikaeKosu As String = String.Empty
    '            Dim zanHurikaeSuryo As String = String.Empty
    '            Dim zanHikifurikaeKosu As Decimal = 0
    '            Dim zanHikifurikaeSuryo As Decimal = 0


    '            '既に引当を行っている場合は加算処理を行う為スプレッドから取得
    '            furikaeKosu = Me._LMDConG.GetCellValue(.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo))
    '            furikaeSuryo = Me._LMDConG.GetCellValue(.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo))
    '            zanHurikaeKosu = Me._LMDConG.GetCellValue(.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_ZAN_KOSU.ColNo))
    '            zanHurikaeSuryo = Me._LMDConG.GetCellValue(.ActiveSheet.Cells(j, LMD010G.sprDetailDef.MOTO_ZAN_SURYO.ColNo))

    '            hikifurikaeKosu = Convert.ToDecimal(furikaeKosu) + Convert.ToDecimal(dr.Item("ALCTD_NB").ToString())
    '            hikifurikaeSuryo = Convert.ToDecimal(furikaeSuryo) + Convert.ToDecimal(dr.Item("ALCTD_QT").ToString())

    '            zanHikifurikaeKosu = Convert.ToDecimal(zanHurikaeKosu) - Convert.ToDecimal(dr.Item("ALCTD_NB").ToString())
    '            zanHikifurikaeSuryo = Convert.ToDecimal(zanHurikaeSuryo) - Convert.ToDecimal(dr.Item("ALCTD_QT").ToString())

    '            .SetCellValue(j, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo, hikifurikaeKosu.ToString())
    '            .SetCellValue(j, LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo, hikifurikaeSuryo.ToString())
    '            .SetCellValue(j, LMD010G.sprDetailDef.MOTO_ZAN_KOSU.ColNo, zanHikifurikaeKosu.ToString())
    '            .SetCellValue(j, LMD010G.sprDetailDef.MOTO_ZAN_SURYO.ColNo, zanHikifurikaeSuryo.ToString())

    '        Next

    '        With _Frm

    '            'drが存在する場合のみ画面項目へセットを行う
    '            If lngcnt >= 0 Then
    '                .numKonsu.Value = Convert.ToDecimal(dr.Item("KONSU").ToString())
    '                .numCnt.Value = Convert.ToDecimal(dr.Item("HASU").ToString())
    '            End If

    '        End With

    '        .ResumeLayout(True)

    '    End With

    'End Sub

#End Region '削除予定（仕様変更）

End Class
