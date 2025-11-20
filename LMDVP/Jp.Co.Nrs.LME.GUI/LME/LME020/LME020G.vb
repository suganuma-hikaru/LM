' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME      : 作業
'  プログラムID     :  LME020G  : 作業料明細編集
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
''' LME020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LME020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME020F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LME020V

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

    Private ROUND_POS_FLG As String

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._LMEconV = New LMEControlV(handlerClass, DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LME020V(handlerClass, frm, Me._LMEconV)


    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal errFlg As Boolean, ByVal renzokuFlg As String)

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新　規"
            .F2ButtonName = "編　集"
            .F3ButtonName = "複　写"
            .F4ButtonName = "削　除"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = "スキップ"
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = "マスタ参照"
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            If (DispMode.VIEW).Equals(Me._Frm.lblSituation.DispMode) = True Then
                '参照モード時
                .F1ButtonEnabled = always
                .F2ButtonEnabled = always
                .F3ButtonEnabled = always
                .F4ButtonEnabled = always
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = lock
                .F11ButtonEnabled = lock
                .F12ButtonEnabled = always
            Else
                '編集モード時
                .F1ButtonEnabled = lock
                .F2ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F4ButtonEnabled = lock
                .F5ButtonEnabled = lock
                .F6ButtonEnabled = lock
                .F7ButtonEnabled = lock
                .F8ButtonEnabled = lock
                .F9ButtonEnabled = lock
                .F10ButtonEnabled = always
                .F11ButtonEnabled = always
                .F12ButtonEnabled = always
            End If

            If errFlg = True Then
                '編集モード時
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
            End If

            If ("01").Equals(renzokuFlg) = True Then
                '連続入力時のみ
                .F1ButtonEnabled = lock
                .F3ButtonEnabled = lock
                .F7ButtonEnabled = always
            End If

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

        End With

    End Sub

#End Region 'Mode&Status

#End Region

#Region "設定・制御"

    ''' <summary>
    ''' シチュエーションラベルの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSituation(ByVal dispMode As String, ByVal recordStatus As String)

        '編集部の項目をクリア
        With Me._frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recordStatus
        End With

    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'START YANAI 要望番号875
            .imdSagyoCompDate.TabIndex = LME020C.CtlTabIndex.SAGYOCOMPDATE
            'END YANAI 要望番号875
            .cmbIozsKb.TabIndex = LME020C.CtlTabIndex.IOZSKB
            .txtSagyoNm.TabIndex = LME020C.CtlTabIndex.SAGYONM
            .txtCustCdL.TabIndex = LME020C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LME020C.CtlTabIndex.CUSTCDm
            .txtDestCd.TabIndex = LME020C.CtlTabIndex.DESTCD
            .txtDestNm.TabIndex = LME020C.CtlTabIndex.DESTNM
            'START YANAI 要望番号875
            '.cmbDestSagyoFlg.TabIndex = LME020C.CtlTabIndex.DESTSAGYOFLG
            'END YANAI 要望番号875
            .txtGoodsCdCust.TabIndex = LME020C.CtlTabIndex.GOODSCDCUST
            .txtGoodsNm.TabIndex = LME020C.CtlTabIndex.GOODSNM
            .txtLotNo.TabIndex = LME020C.CtlTabIndex.LOTNO
            .txtSeiqtoCd.TabIndex = LME020C.CtlTabIndex.SEIQTOCD
            .cmbTaxKb.TabIndex = LME020C.CtlTabIndex.TAXKB
            .numSagyoNb.TabIndex = LME020C.CtlTabIndex.SAGYONB
            .numSagyoUp.TabIndex = LME020C.CtlTabIndex.SAGYOUP
            .numSagyoGk.TabIndex = LME020C.CtlTabIndex.SAGYOGK
            .cmbInvTani.TabIndex = LME020C.CtlTabIndex.INVTANI
            .txtRemarkZai.TabIndex = LME020C.CtlTabIndex.REMARKZAI
            .txtRemarkSkyu.TabIndex = LME020C.CtlTabIndex.REMARKSKYU

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        With Me._Frm

            If (RecordStatus.NEW_REC).Equals(.lblSituation.RecordStatus) = True Then
                '新規モード
                .cmbIozsKb.ReadOnly = False
                .txtCustCdL.ReadOnly = False
                .txtCustCdM.ReadOnly = False
                .txtGoodsCdCust.ReadOnly = False
            ElseIf (RecordStatus.NOMAL_REC).Equals(.lblSituation.RecordStatus) = True Then
                '編集モード
                .cmbIozsKb.ReadOnly = True
                .txtCustCdL.ReadOnly = True
                .txtCustCdM.ReadOnly = True
                .txtGoodsCdCust.ReadOnly = True
            ElseIf (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True Then
                'コピーモード
                .cmbIozsKb.ReadOnly = False
                .txtCustCdL.ReadOnly = False
                .txtCustCdM.ReadOnly = False
                .txtGoodsCdCust.ReadOnly = False
            End If


            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = True Then
                '参照モード
                'START YANAI 要望番号875
                .imdSagyoCompDate.ReadOnly = True
                'END YANAI 要望番号875
                .cmbIozsKb.ReadOnly = True
                .txtSagyoNm.ReadOnly = True
                .txtCustCdL.ReadOnly = True
                .txtCustCdM.ReadOnly = True
                .txtDestCd.ReadOnly = True
                .txtDestNm.ReadOnly = True
                'START YANAI 要望番号875
                '.cmbDestSagyoFlg.ReadOnly = True
                'END YANAI 要望番号875
                .txtGoodsCdCust.ReadOnly = True
                .txtGoodsNm.ReadOnly = True
                .txtLotNo.ReadOnly = True
                .txtSeiqtoCd.ReadOnly = True
                .cmbTaxKb.ReadOnly = True
                .numSagyoNb.ReadOnly = True
                .numSagyoUp.ReadOnly = True
                .numSagyoGk.ReadOnly = True
                .cmbInvTani.ReadOnly = True
                .txtRemarkZai.ReadOnly = True
                .txtRemarkSkyu.ReadOnly = True
            Else
                '編集モード
                'START YANAI 要望番号875
                .imdSagyoCompDate.ReadOnly = False
                'END YANAI 要望番号875
                .txtSagyoNm.ReadOnly = False
                .txtDestCd.ReadOnly = False
                .txtDestNm.ReadOnly = False
                'START YANAI 要望番号875
                '.cmbDestSagyoFlg.ReadOnly = False
                'END YANAI 要望番号875
                .txtGoodsNm.ReadOnly = False
                .txtLotNo.ReadOnly = False
                .txtSeiqtoCd.ReadOnly = False
                .cmbTaxKb.ReadOnly = False
                .numSagyoNb.ReadOnly = False
                .numSagyoUp.ReadOnly = False
                .numSagyoGk.ReadOnly = False
                .cmbInvTani.ReadOnly = False
                .txtRemarkZai.ReadOnly = False
                .txtRemarkSkyu.ReadOnly = False
            End If

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定(LME020HのMain部でのみ使用)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlFromMain()

        With Me._Frm

            .txtCustCdL.ReadOnly = True
            .txtCustCdM.ReadOnly = True

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._frm
            Dim d9_3 As Decimal = Convert.ToDecimal(LME020C.MAX_9_3)
            Dim sharp9_3 As String = "###,###,##0.000"
            Dim d10 As Decimal = Convert.ToDecimal(LME020C.MAX_10)
            Dim sharp10 As String = "#,###,###,##0"

            'START WANG 2014/10/23 要望2229対応
            Dim d9_2 As Decimal = Convert.ToDecimal(LME020C.MAX_9_2)
            Dim sharp9_2 As String = "###,###,##0.00"
            If ROUND_POS_FLG = "2" Then
                .numSagyoUp.SetInputFields(sharp9_2, , 9, 1, , 2, 2, , d9_2, 0)
                .numSagyoGk.SetInputFields(sharp9_2, , 9, 1, , 2, 2, , d9_2, 0)
            Else
                .numSagyoUp.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
                .numSagyoGk.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)
            End If
            'END WANG 2014/10/23 要望2229対応
            .numSagyoNb.SetInputFields(sharp10, , 10, 1, , 0, 0, , d10, 0)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .txtSagyoRecNo.TextValue = String.Empty
            .cmbSagyoCompKb.SelectedValue = "00"
            .cmbSkyuChk.SelectedValue = "00"
            .imdSagyoCompDate.TextValue = String.Empty
            .txtSagyoCompCd.TextValue = String.Empty
            .txtSagyoCompNm.TextValue = String.Empty
            .txtSagyoSijiNo.TextValue = String.Empty
            'START YANAI 要望番号875
            '.cmbIozsKb.SelectedValue = Nothing
            .cmbIozsKb.SelectedValue = "50"
            'END YANAI 要望番号875
            .txtInOutkaNoLM.TextValue = String.Empty
            .txtSagyoCd.TextValue = String.Empty
            .txtSagyoNm.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .txtCustNm.TextValue = String.Empty
            .txtDestCd.TextValue = String.Empty
            .txtDestNm.TextValue = String.Empty
            'START YANAI 要望番号875
            '.cmbDestSagyoFlg.SelectedValue = Nothing
            'END YANAI 要望番号875
            .txtGoodsCdCust.TextValue = String.Empty
            .txtGoodsNm.TextValue = String.Empty
            .txtLotNo.TextValue = String.Empty
            .txtSeiqtoCd.TextValue = String.Empty
            .txtSeiqtoNm.TextValue = String.Empty
            .cmbTaxKb.TextValue = String.Empty
            .numSagyoNb.Value = 0
            .numSagyoUp.Value = 0
            .numSagyoGk.Value = 0
            .lblSagyoUpCurr.TextValue = String.Empty
            .lblSagyoGkCurr.TextValue = String.Empty
            .cmbInvTani.SelectedValue = Nothing
            .txtRemarkZai.TextValue = String.Empty
            .txtRemarkSkyu.TextValue = String.Empty
            .txtGoodsCdCustHide.TextValue = String.Empty
            .txtGoodsCdKey.TextValue = String.Empty
            .txtUpdDate.TextValue = String.Empty
            .txtUpdTime.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア(コピー時)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CopyClearControl()

        With Me._Frm

            .txtSagyoRecNo.TextValue = String.Empty
            .cmbSagyoCompKb.SelectedValue = "00"
            .cmbSkyuChk.SelectedValue = "00"
            'START YANAI 要望番号875
            '.imdSagyoCompDate.TextValue = String.Empty
            'END YANAI 要望番号875
            .txtSagyoCompCd.TextValue = String.Empty
            .txtSagyoCompNm.TextValue = String.Empty
            .txtSagyoSijiNo.TextValue = String.Empty
            .txtInOutkaNoLM.TextValue = String.Empty
            .txtUpdDate.TextValue = String.Empty
            .txtUpdTime.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 遷移元画面で設定された値を画面に設定(編集時)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlEdit(ByVal ds As DataSet)

        '遷移元画面で設定された値を画面に設定(編集時)
        With Me._Frm
            .cmbEigyo.SelectedValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString
            .cmbSoko.SelectedValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("WH_CD").ToString
            .txtSagyoRecNo.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("SAGYO_REC_NO").ToString
            .txtSagyoCd.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("SAGYO_CD").ToString
            .txtSagyoNm.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("SAGYO_NM").ToString
            'SHINODA 要望管理2168
            If ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString.Length = 7 Then
                .txtCustCdL.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString.Substring(0, 5)
                .txtCustCdM.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString.Substring(5, 2)
            Else
                .txtCustCdL.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString
                .txtCustCdM.TextValue = "00"
            End If
            'SHINODA 要望管理2168
            '荷主名の取得
            '20160621 tsunehira 要番2491 add start
            Dim custDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                                                               "NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                                               "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                               "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))

            '20160621 tsunehira 要番2491 add end
            'Dim custDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
            '                                                                                                  "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
            If 0 < custDr.Length Then
                .txtCustNm.TextValue = custDr(0).Item("CUST_NM_L").ToString()      '荷主名（大）
                'START YANAI 要望番号875
                .txtSeiqtoCd.TextValue = custDr(0).Item("SAGYO_SEIQTO_CD").ToString()      '請求先コード
                '請求先名の取得
                Dim seiqtoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("SEIQTO_CD = '", .txtSeiqtoCd.TextValue, "'"))
                If 0 < seiqtoDr.Length Then
                    .txtSeiqtoNm.TextValue = seiqtoDr(0).Item("SEIQTO_NM").ToString()      '請求先名
                End If
                'END YANAI 要望番号875
            End If
            'START YANAI 要望番号875
            If String.IsNullOrEmpty(ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("SAGYO_UP").ToString) = False Then
                'START YANAI 要望番号1104 作業マスタの単価が４桁以上の場合作成時アベンド
                '.numSagyoUp.TextValue = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("SAGYO_UP").ToString
                .numSagyoUp.Value = ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("SAGYO_UP").ToString
                'END YANAI 要望番号1104 作業マスタの単価が４桁以上の場合作成時アベンド
            End If
            'END YANAI 要望番号875

        End With
    End Sub

    ''' <summary>
    ''' DBから取得した値を画面に設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlServerData(ByVal ds As DataSet)

        'DBから取得した値を画面に設定
        With Me._Frm
            .cmbEigyo.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("NRS_BR_CD").ToString
            .cmbSoko.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("WH_CD").ToString
            .txtSagyoRecNo.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_REC_NO").ToString
            .cmbSagyoCompKb.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_COMP").ToString
            .cmbSkyuChk.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SKYU_CHK").ToString
            .imdSagyoCompDate.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_COMP_DATE").ToString
            .txtSagyoCompCd.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_COMP_CD").ToString
            .txtSagyoCompNm.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_COMP_CD_NM").ToString
            .txtSagyoSijiNo.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_SIJI_NO").ToString
            .cmbIozsKb.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("IOZS_KB").ToString
            If String.IsNullOrEmpty(ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("INOUTKA_NO_LM").ToString) = False Then
                .txtInOutkaNoLM.TextValue = String.Concat(Mid(ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("INOUTKA_NO_LM").ToString, 1, 9), _
                                                          "-", _
                                                          Mid(ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("INOUTKA_NO_LM").ToString, 10, 3))
            Else
                .txtInOutkaNoLM.TextValue = String.Empty
            End If
            .txtSagyoCd.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_CD").ToString
            .txtSagyoNm.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_NM").ToString
            .txtCustCdL.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("CUST_CD_L").ToString
            .txtCustCdM.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("CUST_CD_M").ToString
            .txtCustNm.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("CUST_NM").ToString
            .txtDestCd.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("DEST_CD").ToString
            .txtDestNm.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("DEST_NM").ToString
            'START YANAI 要望番号875
            '.cmbDestSagyoFlg.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("DEST_SAGYO_FLG").ToString
            'END YANAI 要望番号875
            .txtGoodsCdCust.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("GOODS_CD_CUST").ToString
            .txtGoodsNm.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("GOODS_NM_NRS").ToString
            .txtLotNo.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("LOT_NO").ToString
            .txtSeiqtoCd.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SEIQTO_CD").ToString
            .txtSeiqtoNm.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SEIQTO_NM").ToString
            .cmbTaxKb.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("TAX_KB").ToString
            .numSagyoNb.Value = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_NB").ToString
            'START WANG 2014/10/23 要望2229対応
            ROUND_POS_FLG = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("ROUND_POS").ToString()
            'END WANG 2014/10/23 要望2229対応
            .numSagyoUp.Value = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_UP").ToString
            .numSagyoGk.Value = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SAGYO_GK").ToString
            .lblSagyoUpCurr.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("ITEM_CURR_CD").ToString
            .lblSagyoGkCurr.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("ITEM_CURR_CD").ToString
            .cmbInvTani.SelectedValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("INV_TANI").ToString
            .txtRemarkZai.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("REMARK_ZAI").ToString
            .txtRemarkSkyu.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("REMARK_SKYU").ToString

            .txtGoodsCdCustHide.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("GOODS_CD_CUST").ToString
            .txtGoodsCdKey.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("GOODS_CD_NRS").ToString
            .txtUpdDate.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SYS_UPD_DATE").ToString
            .txtUpdTime.TextValue = ds.Tables(LME020C.TABLE_NM_INOUT).Rows(0).Item("SYS_UPD_TIME").ToString

        End With
    End Sub

    ''' <summary>
    ''' 作業レコードの値を設定
    ''' </summary>
    ''' <param name="frm">frm</param>
    ''' <remarks></remarks>
    Friend Function SetDataSet(ByVal frm As LME020F, ByVal ds As DataSet) As DataSet

        With frm

            ds.Tables(LME020C.TABLE_NM_INOUT).Clear()
            Dim row As DataRow = ds.Tables(LME020C.TABLE_NM_INOUT).NewRow
            Dim dr As DataRow() = Nothing

            row.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            row.Item("SAGYO_REC_NO") = .txtSagyoRecNo.TextValue
            row.Item("SAGYO_COMP") = .cmbSagyoCompKb.SelectedValue
            row.Item("SKYU_CHK") = .cmbSkyuChk.SelectedValue
            row.Item("SAGYO_SIJI_NO") = .txtSagyoSijiNo.TextValue
            row.Item("INOUTKA_NO_LM") = (.txtInOutkaNoLM.TextValue).Replace("-", "")
            row.Item("WH_CD") = .cmbSoko.SelectedValue
            row.Item("IOZS_KB") = .cmbIozsKb.SelectedValue
            row.Item("SAGYO_CD") = .txtSagyoCd.TextValue
            row.Item("SAGYO_NM") = .txtSagyoNm.TextValue
            row.Item("CUST_CD_L") = .txtCustCdL.TextValue
            row.Item("CUST_CD_M") = .txtCustCdM.TextValue
            row.Item("DEST_CD") = .txtDestCd.TextValue
            row.Item("DEST_NM") = .txtDestNm.TextValue
            row.Item("GOODS_CD_NRS") = .txtGoodsCdKey.TextValue
            row.Item("GOODS_NM_NRS") = .txtGoodsNm.TextValue
            row.Item("LOT_NO") = .txtLotNo.TextValue
            row.Item("INV_TANI") = .cmbInvTani.SelectedValue
            row.Item("SAGYO_NB") = .numSagyoNb.Value
            row.Item("SAGYO_UP") = .numSagyoUp.Value
            row.Item("SAGYO_GK") = .numSagyoGk.Value
            row.Item("TAX_KB") = .cmbTaxKb.SelectedValue
            row.Item("SEIQTO_CD") = .txtSeiqtoCd.TextValue
            row.Item("REMARK_ZAI") = .txtRemarkZai.TextValue
            row.Item("REMARK_SKYU") = .txtRemarkSkyu.TextValue
            row.Item("SAGYO_COMP_CD") = .txtSagyoCompCd.TextValue
            row.Item("SAGYO_COMP_DATE") = .imdSagyoCompDate.TextValue
            'START YANAI 要望番号875
            'row.Item("DEST_SAGYO_FLG") = .cmbDestSagyoFlg.SelectedValue
            row.Item("DEST_SAGYO_FLG") = "00"
            'END YANAI 要望番号875

            row.Item("SAGYO_COMP_CD_NM") = .txtSagyoCompNm.TextValue
            row.Item("CUST_NM") = .txtCustNm.TextValue
            row.Item("SEIQTO_NM") = .txtSeiqtoNm.TextValue
            row.Item("GOODS_CD_CUST") = .txtGoodsCdCust.TextValue

            ds.Tables(LME020C.TABLE_NM_INOUT).Rows.Add(row)

        End With

        Return ds

    End Function

    'START YANAI 要望番号875
    ''' <summary>
    ''' 作業金額の計算
    ''' </summary>
    ''' <param name="frm">frm</param>
    ''' <remarks></remarks>
    Friend Function SAGYOKINGAKU(ByVal frm As LME020F) As Boolean

        Dim sagyoGk As Decimal
        With frm

            '作業金額の計算を行う
            '(2012.12.17)要望番号1695 0でなくても計算する -- START --

            'If Convert.ToDecimal(.numSagyoGk.Value) = 0 Then
            sagyoGk = Convert.ToDecimal(.numSagyoNb.Value) * Convert.ToDecimal(.numSagyoUp.Value)
            '入力チェック
            If Me._V.IsSagyoKingakuCheck(sagyoGk) = False Then
                '処理終了アクション
                Return False
            End If
            .numSagyoGk.Value = sagyoGk

            'End If
            '(2012.12.17)要望番号1695 0でなくても計算する --  END  --

        End With

        Return True

    End Function

    'END YANAI 要望番号875

#End Region

#Region "ユーティリティ"

#Region "プロパティ"

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#End Region

#End Region

End Class
