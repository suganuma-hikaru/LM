' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF080G : 支払検索
'  作  成  者       :  YANAI
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF080Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF080G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF080F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF080F, ByVal g As LMFControlG)

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
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = LMFControlC.FUNCTION_PRINT
            .F4ButtonName = LMFControlC.FUNCTION_RENNYU
            .F5ButtonName = LMFControlC.FUNCTION_KAKUTEI
            .F6ButtonName = LMFControlC.FUNCTION_KAIJO
            .F7ButtonName = LMFControlC.FUNCTION_MATOMESHIJI
            .F8ButtonName = LMFControlC.FUNCTION_MATOMEKAIJO
            .F9ButtonName = LMFControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = LMFControlC.FUNCTION_SAIKEI
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = always
            .F4ButtonEnabled = always
            .F5ButtonEnabled = always
            .F6ButtonEnabled = always
            .F7ButtonEnabled = always
            .F8ButtonEnabled = always
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = always
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbEigyo.TabIndex = LMF080C.CtlTabIndex.EIGYO
            .pnlCondition.TabIndex = LMF080C.CtlTabIndex.CONDITION
            .cmbDateKb.TabIndex = LMF080C.CtlTabIndex.DATEKB
            .imdFrom.TabIndex = LMF080C.CtlTabIndex.FROM_DATA
            .imdTo.TabIndex = LMF080C.CtlTabIndex.TO_DATA
            .cmbTariffKbn.TabIndex = LMF080C.CtlTabIndex.TARIFFKBN
            .txtTariffCd.TabIndex = LMF080C.CtlTabIndex.TARIFFCD
            .lblTariffNm.TabIndex = LMF080C.CtlTabIndex.TARIFFNM
            .txtExtcCd.TabIndex = LMF080C.CtlTabIndex.EXTCCD
            .lblExtcNm.TabIndex = LMF080C.CtlTabIndex.EXTCNM
            .txtUnsocoCd.TabIndex = LMF080C.CtlTabIndex.UNSOCD
            .txtUnsocoBrCd.TabIndex = LMF080C.CtlTabIndex.UNSOBRCD
            .lblUnsocoNm.TabIndex = LMF080C.CtlTabIndex.UNSONM
            .txtCustCdL.TabIndex = LMF080C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMF080C.CtlTabIndex.CUSTCDM
            .lblCustNm.TabIndex = LMF080C.CtlTabIndex.CUSTNM
            .txtDestCd.TabIndex = LMF080C.CtlTabIndex.DESTCD
            .lblDestNm.TabIndex = LMF080C.CtlTabIndex.DESTNM
            .cmbGroup.TabIndex = LMF080C.CtlTabIndex.CMB_GROUP
            .pnlKey.TabIndex = LMF080C.CtlTabIndex.KEY
            .optNomal.TabIndex = LMF080C.CtlTabIndex.NOMAL
            .optGroup.TabIndex = LMF080C.CtlTabIndex.GROUP
            .pnlConditionKbn.TabIndex = LMF080C.CtlTabIndex.CONDITIONKBN
            .pnlUnchin.TabIndex = LMF080C.CtlTabIndex.UNCHIN
            .optShaDate.TabIndex = LMF080C.CtlTabIndex.SHADATE
            .optTonKiro.TabIndex = LMF080C.CtlTabIndex.TONKIRO
            .optUnchinRyoho.TabIndex = LMF080C.CtlTabIndex.UNCHINRYOHO
            .pnlGroupNo.TabIndex = LMF080C.CtlTabIndex.GROUPNO
            .optGroupMi.TabIndex = LMF080C.CtlTabIndex.GROUPMI
            .optGroupSumi.TabIndex = LMF080C.CtlTabIndex.GROUPSUMI
            .optGroupRyoho.TabIndex = LMF080C.CtlTabIndex.GROUPRYOHO
            .pnlRev.TabIndex = LMF080C.CtlTabIndex.REV
            .optRevMi.TabIndex = LMF080C.CtlTabIndex.REVMI
            .optRevKaku.TabIndex = LMF080C.CtlTabIndex.REVKAKU
            .optRevRyoho.TabIndex = LMF080C.CtlTabIndex.REVRYOHO
            .pnlMoto.TabIndex = LMF080C.CtlTabIndex.MOTO
            .optIn.TabIndex = LMF080C.CtlTabIndex.IN_DATA
            .optOut.TabIndex = LMF080C.CtlTabIndex.OUT
            .optUnso.TabIndex = LMF080C.CtlTabIndex.UNSO
            .optAll.TabIndex = LMF080C.CtlTabIndex.ALL
            'START YANAI 要望番号1424 支払処理
            .optShiharaiNormal.TabIndex = LMF080C.CtlTabIndex.SHIHARAI_NOMARL
            .optShiharaiUnco.TabIndex = LMF080C.CtlTabIndex.SHIHARAI_UNCO
            'END YANAI 要望番号1424 支払処理
            .pnlHenko.TabIndex = LMF080C.CtlTabIndex.PNL_HENKO
            .cmbShusei.TabIndex = LMF080C.CtlTabIndex.SHUSEI
            .txtShuseiL.TabIndex = LMF080C.CtlTabIndex.SHUSEIL
            .btnHenko.TabIndex = LMF080C.CtlTabIndex.HENKO
            .numSokeithi.TabIndex = LMF080C.CtlTabIndex.SOKEITHI
            .sprDetail.TabIndex = LMF080C.CtlTabIndex.DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '数値コントロール設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim dMax As Decimal = Convert.ToDecimal(LMFControlC.MAX_KETA)
            .numSokeithi.SetInputFields(LMFControlC.MAX_18, , 18, , , 0, , , dMax)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()
            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.SelectedValue = Nothing
            .cmbDateKb.SelectedValue = LMF080C.SYUKKA_DATE
            .imdFrom.TextValue = String.Empty
            .imdTo.TextValue = String.Empty
            .txtTariffCd.TextValue = String.Empty
            .lblTariffNm.TextValue = String.Empty
            .txtUnsocoCd.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            .lblUnsocoNm.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtDestCd.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty
            .cmbGroup.SelectedValue = Nothing
            .cmbShusei.SelectedValue = Nothing
            .txtShuseiL.TextValue = String.Empty
            .numSokeithi.Value = 0
            Dim chk As Boolean = True
            Dim unChk As Boolean = False
            .optNomal.Checked = chk
            .optGroup.Checked = unChk
            .optShaDate.Checked = unChk
            .optTonKiro.Checked = unChk
            .optUnchinRyoho.Checked = chk
            .optGroupMi.Checked = unChk
            .optGroupSumi.Checked = unChk
            .optGroupRyoho.Checked = chk
            .optRevMi.Checked = unChk
            .optRevKaku.Checked = unChk
            .optRevRyoho.Checked = chk
            .optIn.Checked = unChk
            .optOut.Checked = unChk
            .optUnso.Checked = unChk
            .optAll.Checked = chk
            'START YANAI 要望番号1424 支払処理
            .optShiharaiNormal.Checked = chk
            .optShiharaiUnco.Checked = unChk
            'END YANAI 要望番号1424 支払処理

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            '初期荷主の値を設定
            Dim drs As DataRow() = Me._LMFconG.SelectTCustListDataRow(LMUserInfoManager.GetUserID())
            Dim brCd As String = String.Empty '20160928 要番2622 tsunehira add
            Dim custCdL As String = String.Empty
            Dim custCdM As String = String.Empty
            If 0 < drs.Length Then
                brCd = LMUserInfoManager.GetNrsBrCd() '20160928 要番2622 tsunehira add
                custCdL = drs(0).Item("CUST_CD_L").ToString()
                custCdM = drs(0).Item("CUST_CD_M").ToString()

            End If

            'drs = Me._LMFconG.SelectCustListDataRow(custCdL, custCdM)
            drs = Me._LMFconG.SelectCustListDataRow(brCd, custCdL, custCdM) '20160928 要番2622 tsunehira add
            Dim custNm As String = String.Empty
            If 0 < drs.Length Then

                custNm = Me._LMFconG.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)

            End If

            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '2014.08.04 FFEM高取対応 START
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                Me._Frm.cmbEigyo.ReadOnly = True
            Else
                Me._Frm.cmbEigyo.ReadOnly = False
            End If
            '2014.08.04 FFEM高取対応 END

            .txtCustCdL.TextValue = custCdL
            .txtCustCdM.TextValue = custCdM
            .lblCustNm.TextValue = custNm

            'ラジオボタンの設定
            .optUnchinRyoho.Checked = True
            .optGroupRyoho.Checked = True
            .optRevRyoho.Checked = True
            .optAll.Checked = True

            'スプレッドの列の表示・非表示設定
            Call Me.SetSpreadVisible()

        End With

    End Sub

    ''' <summary>
    ''' 検索処理成功後の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetOrderByData()

        With Me._Frm

            Dim orderBy As String = .cmbGroup.SelectedValue.ToString()

            .lblOrderBy.TextValue = orderBy

            Dim chk As Boolean = True

            '何も選択していない場合、
            If String.IsNullOrEmpty(orderBy) = True Then

                '通常検索
                .optNomal.Checked = chk

            Else

                'まとめ検索
                .optGroup.Checked = chk

            End If

        End With

    End Sub

    ''' <summary>
    ''' 修正項目の変更によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockHenkoChangeControl()

        With Me._Frm

            Dim ptn1 As Boolean = True

            Select Case .cmbShusei.SelectedValue.ToString()

                Case LMF080C.SHUSEI_SHIHARAI, LMF080C.SHUSEI_TARIFF, LMF080C.SHUSEI_YOKO, LMF080C.SHUSEI_ETARIFF

                    ptn1 = False

            End Select

            'ロック制御
            Me._LMFconG.SetLockInputMan(.txtShuseiL, ptn1)

        End With

    End Sub

    ''' <summary>
    ''' まとめ候補変更による検索キー変更
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SelectGroupOpt()

        With Me._Frm

            Dim chk As Boolean = True
            Dim kbnDr() As DataRow = Nothing

            'まとめ候補が空の場合、通常を選択
            If String.IsNullOrEmpty(.cmbGroup.SelectedValue.ToString()) = chk Then
                Dim unChk As Boolean = False
                Me._LMFconG.SetLockControl(.pnlGroupNo, unChk)
                Me._LMFconG.SetLockControl(.pnlRev, unChk)
                .optGroupRyoho.Checked = chk
                .optRevRyoho.Checked = chk
                .optNomal.Checked = chk
            Else
                Me._LMFconG.SetLockControl(.pnlGroupNo, chk)
                Me._LMFconG.SetLockControl(.pnlRev, chk)
                .optGroup.Checked = chk
                .optGroupMi.Checked = chk
                .optRevMi.Checked = chk
                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'M015' AND ", _
                                                                                               "KBN_CD = '", .cmbGroup.SelectedValue, "'"))
                If kbnDr.Length > 0 Then
                    .lblTitleTyuki.TextValue = kbnDr(0).Item("KBN_NM2").ToString
                End If
            End If

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared KAKUTEI_FLG As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.KAKUTEI_FLG, "確定フラグ", 0, False)
        Public Shared KAKUTEI As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.KAKUTEI, "確定", 40, True)
        Public Shared SHUKKA As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHUKKA, "出荷日", 80, True)
        Public Shared NONYU As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.NONYU, "納入日", 80, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.CUST_NM, "荷主名", 140, True)
        Public Shared CUST_REF_NO As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.CUST_REF_NO, "伝票№", 50, True)
        Public Shared SHIHARAITO_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAITO_CD, "支払先" & vbCrLf & "コード", 80, True)
        Public Shared SHIHARAI_NM As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_NM, "支払先名", 80, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DEST_NM, "届先名", 140, True)
        'START YANAI 要望番号1424 支払処理
        Public Shared DEST_AD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DEST_AD, "届先住所", 110, True)
        'END YANAI 要望番号1424 支払処理
        Public Shared UNSO_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNSO_CD, "運送会社" & vbCrLf & "コード", 110, True)
        Public Shared UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNSO_BR_CD, "運送会社" & vbCrLf & "支店コード", 120, True)
        Public Shared UNSO_NM As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNSO_NM, "運送会社名", 110, True)
        Public Shared TARIFF_KBN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.TARIFF_KBN, "タリフ分類区分", 0, False)
        Public Shared BUNRUI As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.BUNRUI, "タリフ分類", 100, True)
        Public Shared TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.TARIFF_CD, "支払タリフ" & vbCrLf & "コード", 100, True)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.EXTC_TARIFF_CD, "支払割増" & vbCrLf & "タリフコード", 120, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.JURYO, "重量", 80, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.KYORI, "距離", 80, True)
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNCHIN, "運賃計", 80, True)
        Public Shared ZEI As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.ZEI, "税", 0, False)
        Public Shared ZEI_KBN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.ZEI_KBN, "課税区分", 80, True)
        Public Shared GROUP As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.GROUP, "まとめ", 80, True)
        Public Shared GROUP_M As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.GROUP_M, "まとめM", 70, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.REMARK, "支払備考", 120, True)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.KANRI_NO, "管理番号", 80, True)
        Public Shared UNSO_NO As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNSO_NO, "運送番号", 80, True)
        Public Shared UNSO_NO_EDA As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNSO_NO_EDA, "番号M", 60, True)
        Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.TRIP_NO, "運行番号", 80, True)
        Public Shared MOTO_DATA_KBN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.MOTO_DATA_KBN, "(隠し)元データ区分", 0, False)
        Public Shared MOTO_DATA As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.MOTO_DATA, "元データ" & vbCrLf & "区分", 80, True)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DEST_CD, "届先コード", 110, True)
        Public Shared MINASHI_DEST_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.MINASHI_DEST_CD, "みなし届先", 110, True)
        Public Shared BIN_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.BIN_KB, "便区分コード", 0, False)
        Public Shared BIN_NM As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.BIN_NM, "便区分", 90, True)
        Public Shared DEST_JIS_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DEST_JIS_CD, "届先JIS", 60, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.CUST_CD, "荷主" & vbCrLf & "コード", 60, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.CUST_CD_L, "荷主(大)コード", 0, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.CUST_CD_M, "荷主(中)コード", 0, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.NRS_BR_CD, "営業所", 0, False)
        Public Shared UNTIN_CALCULATION_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNTIN_CALCULATION_KB, "絞め日基準", 0, False)
        Public Shared VCLE_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.VCLE_KB, "車輌区分", 0, False)
        Public Shared UNSO_ONDO_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.UNSO_ONDO_KB, "温度区分", 0, False)
        Public Shared SIZE_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SIZE_KB, "サイズ区分", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SYS_UPD_TIME, "更新時間", 0, False)
        Public Shared CHK_UNCHIN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.CHK_UNCHIN, "チェック用運賃", 0, False)
        Public Shared SHIHARAI_UNCHIN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_UNCHIN, "支払運賃", 0, False)
        Public Shared SHIHARAI_CITY_EXTC As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_CITY_EXTC, "支払都市割増", 0, False)
        Public Shared SHIHARAI_WINT_EXTC As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_WINT_EXTC, "支払冬期割増", 0, False)
        Public Shared SHIHARAI_RELY_EXTC As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_RELY_EXTC, "支払中継料", 0, False)
        Public Shared SHIHARAI_TOLL As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_TOLL, "支払通行料", 0, False)
        Public Shared SHIHARAI_INSU As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_INSU, "支払保険料", 0, False)
        Public Shared DECI_UNCHIN As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_UNCHIN, "確定運賃", 0, False)
        Public Shared DECI_CITY_EXTC As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_CITY_EXTC, "確定都市割増", 0, False)
        Public Shared DECI_WINT_EXTC As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_WINT_EXTC, "確定冬期割増", 0, False)
        Public Shared DECI_RELY_EXTC As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_RELY_EXTC, "確定中継料", 0, False)
        Public Shared DECI_TOLL As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_TOLL, "確定通行料", 0, False)
        Public Shared DECI_INSU As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_INSU, "確定保険料", 0, False)
        Public Shared DECI_NG_NB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.DECI_NG_NB, "個数", 0, False)
        Public Shared SHIHARAI_PKG_UT As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_PKG_UT, "荷姿", 0, False)
        Public Shared SHIHARAI_SYARYO_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_SYARYO_KB, "車両区分(運賃)", 0, False)
        Public Shared SHIHARAI_DANGER_KB As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_DANGER_KB, "危険区分", 0, False)
        Public Shared SYS_UPD_FLG As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SYS_UPD_FLG, "更新フラグ", 0, False)
        'START YANAI 要望番号1424 支払処理
        Public Shared SHIHARAI_UNCHIN_UNSOLL As SpreadColProperty = New SpreadColProperty(LMF080C.SprColumnIndex.SHIHARAI_UNCHIN_UNSOLL, "支払運賃(運行)", 0, False)
        'END YANAI 要望番号1424 支払処理

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprDetail
        With spr

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMF080C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMF080G.sprDetailDef())

            '列固定位置を設定します。(ex.納入予定で固定)
            .ActiveSheet.FrozenColumnCount = LMF080G.sprDetailDef.CUST_NM.ColNo + 1

            '列設定
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sEidaisu3 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 3)
            Dim sEidaisu5 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 5)
            Dim sEidaisu7 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 7)
            Dim sEidaisu8 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 8)
            Dim sEidaisu9 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 9)
            Dim sEidaisu10 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 10)
            Dim sEidaisu30 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 30)
            Dim sHan15 As StyleInfo = Me.StyleInfoTextHan(spr, 15)
            Dim sMix50 As StyleInfo = Me.StyleInfoTextMix(spr, 50)
            Dim sMix60 As StyleInfo = Me.StyleInfoTextMix(spr, 60)
            Dim sMix80 As StyleInfo = Me.StyleInfoTextMix(spr, 80)
            Dim sMix100 As StyleInfo = Me.StyleInfoTextMix(spr, 100)
            Dim sMix122 As StyleInfo = Me.StyleInfoTextMix(spr, 122)
            'START YANAI 要望番号1424 支払処理
            Dim sMix120 As StyleInfo = Me.StyleInfoTextMix(spr, 120)
            'END YANAI 要望番号1424 支払処理

            .SetCellStyle(0, LMF080G.sprDetailDef.KAKUTEI_FLG.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.KAKUTEI.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHUKKA.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.NONYU.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.CUST_NM.ColNo, sMix122)
            .SetCellStyle(0, LMF080G.sprDetailDef.CUST_REF_NO.ColNo, sEidaisu30)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo, sEidaisu8)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_NM.ColNo, sMix122)
            .SetCellStyle(0, LMF080G.sprDetailDef.DEST_NM.ColNo, sMix80)
            'START YANAI 要望番号1424 支払処理
            .SetCellStyle(0, LMF080G.sprDetailDef.DEST_AD.ColNo, sMix120)
            'END YANAI 要望番号1424 支払処理
            .SetCellStyle(0, LMF080G.sprDetailDef.UNSO_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNSO_BR_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNSO_NM.ColNo, sMix122)
            .SetCellStyle(0, LMF080G.sprDetailDef.TARIFF_KBN.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.BUNRUI.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.TARIFF_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.JURYO.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.KYORI.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.ZEI.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.ZEI_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Z001, False))
            .SetCellStyle(0, LMF080G.sprDetailDef.GROUP.ColNo, sEidaisu9)
            .SetCellStyle(0, LMF080G.sprDetailDef.GROUP_M.ColNo, sEidaisu3)
            .SetCellStyle(0, LMF080G.sprDetailDef.REMARK.ColNo, sMix100)
            .SetCellStyle(0, LMF080G.sprDetailDef.KANRI_NO.ColNo, sEidaisu9)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNSO_NO.ColNo, sEidaisu9)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNSO_NO_EDA.ColNo, sEidaisu3)
            .SetCellStyle(0, LMF080G.sprDetailDef.TRIP_NO.ColNo, sEidaisu10)
            .SetCellStyle(0, LMF080G.sprDetailDef.MOTO_DATA_KBN.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.MOTO_DATA.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.MINASHI_DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.BIN_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.BIN_NM.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DEST_JIS_CD.ColNo, sEidaisu7)
            .SetCellStyle(0, LMF080G.sprDetailDef.CUST_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.VCLE_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.UNSO_ONDO_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SIZE_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.CHK_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_CITY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_WINT_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_RELY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_TOLL.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_INSU.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_CITY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_WINT_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_RELY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_TOLL.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_INSU.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.DECI_NG_NB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_PKG_UT.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_SYARYO_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_DANGER_KB.ColNo, sLabel)
            .SetCellStyle(0, LMF080G.sprDetailDef.SYS_UPD_FLG.ColNo, sLabel)
            'START YANAI 要望番号1424 支払処理
            .SetCellStyle(0, LMF080G.sprDetailDef.SHIHARAI_UNCHIN_UNSOLL.ColNo, sLabel)
            'END YANAI 要望番号1424 支払処理

            Dim max As Integer = spr.ActiveSheet.Columns.Count - 1
            For i As Integer = 1 To max
                .SetCellValue(0, i, String.Empty)
            Next

            '初期荷主から値取得
            Dim drs As DataRow() = Me._LMFconG.SelectTCustListDataRow(LMUserInfoManager.GetUserID())
            If 0 < drs.Length Then

                Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
                Dim custCdL As String = drs(0).Item("CUST_CD_L").ToString()
                Dim custCdM As String = drs(0).Item("CUST_CD_M").ToString()
                Me._Frm.txtCustCdL.TextValue = custCdL
                Me._Frm.txtCustCdL.TextValue = custCdM
                'drs = Me._LMFconG.SelectCustListDataRow(custCdL, custCdM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)
                '20160928 要番2622 tsunehira add
                drs = Me._LMFconG.SelectCustListDataRow(brCd, custCdL, custCdM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)
                If 0 < drs.Length Then
                    Me._Frm.lblCustNm.TextValue = Me._LMFconG.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)
                End If

            End If

        End With

    End Sub

    '(2017.08.07)要望番号1577の支払版対応 -- START --
    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks>2012.11.09 追加 要望番号1577</remarks>
    Friend Sub SetSpreadColor(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dr As DataRow
        Dim lngcnt As Integer = dt.Rows.Count()

        With spr

            If lngcnt = 0 Then
                Exit Sub
            End If

            For i As Integer = 1 To lngcnt
                dr = dt.Rows(i - 1)

                '要望番号:1791(運賃画面：0円行は赤くしているが、まとめた分は外す) 2013/01/24 本明 Start
                'If dr("UNCHIN").ToString().Equals("0.00") Then
                If dr("UNCHIN").ToString().Equals("0.00") And dr("SHIHARAI_GROUP_NO").ToString().Equals(String.Empty) Then
                    '要望番号:1791(運賃画面：0円行は赤くしているが、まとめた分は外す) 2013/01/24 本明 End
                    '運賃=\0 かつ　まとめ番号が空白の場合は、赤文字
                    .ActiveSheet.Rows(i).ForeColor = Color.Red
                End If

            Next

        End With

    End Sub
    '(2017.08.07)要望番号1577の支払版対応 -- E N D --

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet) As Boolean

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMF080C.TABLE_NM_OUT)
        Dim sokei As Decimal = 0

        '合計欄の初期化
        Me._Frm.numSokeithi.Value = 0

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Return True
            End If

            '検索に成功した場合、並び順の値を設定
            Call Me.SetOrderByData()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum3 As StyleInfo = Me.StyleInfoNum3(spr)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr)
            Dim sNum9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr)
            Dim sNum14 As StyleInfo = Me.StyleInfoNumMax(spr)
            Dim unchin As String = String.Empty
            Dim unchinData As Decimal = 0
            Dim chk As Boolean = True
            Dim maxData As Decimal = Convert.ToDecimal(LMFControlC.MAX_KETA)

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMF080G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF080G.sprDetailDef.KAKUTEI_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.KAKUTEI.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHUKKA.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.NONYU.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.CUST_REF_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DEST_NM.ColNo, sLabel)
                'START YANAI 要望番号1424 支払処理
                .SetCellStyle(i, LMF080G.sprDetailDef.DEST_AD.ColNo, sLabel)
                'END YANAI 要望番号1424 支払処理
                .SetCellStyle(i, LMF080G.sprDetailDef.UNSO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNSO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNSO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.TARIFF_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.BUNRUI.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.JURYO.ColNo, sNum9dec3)
                .SetCellStyle(i, LMF080G.sprDetailDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNCHIN.ColNo, sNum14)
                .SetCellStyle(i, LMF080G.sprDetailDef.ZEI.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.ZEI_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.GROUP.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.GROUP_M.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.KANRI_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNSO_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNSO_NO_EDA.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.MOTO_DATA_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.MOTO_DATA.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.MINASHI_DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.BIN_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.BIN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DEST_JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.CUST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.VCLE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.UNSO_ONDO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SIZE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.CHK_UNCHIN.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_UNCHIN.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_CITY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_WINT_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_RELY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_TOLL.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_INSU.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_UNCHIN.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_CITY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_WINT_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_RELY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_TOLL.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_INSU.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.DECI_NG_NB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_SYARYO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_DANGER_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF080G.sprDetailDef.SYS_UPD_FLG.ColNo, sLabel)
                'START YANAI 要望番号1424 支払処理
                .SetCellStyle(i, LMF080G.sprDetailDef.SHIHARAI_UNCHIN_UNSOLL.ColNo, sLabel)
                'END YANAI 要望番号1424 支払処理

                'セルに値を設定
                .SetCellValue(i, LMF080G.sprDetailDef.DEF.ColNo, False.ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.KAKUTEI.ColNo, dr.Item("SHIHARAI_FIXED_NM").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.KAKUTEI_FLG.ColNo, dr.Item("SHIHARAI_FIXED_FLAG").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHUKKA.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, LMF080G.sprDetailDef.NONYU.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(i, LMF080G.sprDetailDef.CUST_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMF080G.sprDetailDef.CUST_REF_NO.ColNo, dr.Item("CUST_REF_NO").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo, dr.Item("SHIHARAITO_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("SHIHARAITO_NM").ToString(), dr.Item("SHIHARAITO_BUSYO_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMF080G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                'START YANAI 要望番号1424 支払処理
                .SetCellValue(i, LMF080G.sprDetailDef.DEST_AD.ColNo, dr.Item("DEST_AD").ToString())
                'END YANAI 要望番号1424 支払処理
                .SetCellValue(i, LMF080G.sprDetailDef.UNSO_CD.ColNo, dr.Item("UNSO_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.UNSO_BR_CD.ColNo, dr.Item("UNSO_BR_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.UNSO_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSO_NM").ToString(), dr.Item("UNSO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMF080G.sprDetailDef.TARIFF_KBN.ColNo, dr.Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.BUNRUI.ColNo, dr.Item("TARIFF_BUNRUI").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.TARIFF_CD.ColNo, dr.Item("SHIHARAI_TARIFF_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, dr.Item("SHIHARAI_ETARIFF_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.JURYO.ColNo, dr.Item("DECI_WT").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.KYORI.ColNo, dr.Item("DECI_KYORI").ToString())
                unchin = dr.Item("UNCHIN").ToString()
                .SetCellValue(i, LMF080G.sprDetailDef.UNCHIN.ColNo, unchin)
                .SetCellValue(i, LMF080G.sprDetailDef.ZEI.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.ZEI_KBN.ColNo, dr.Item("TAX_NM").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.GROUP.ColNo, dr.Item("SHIHARAI_GROUP_NO").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.GROUP_M.ColNo, dr.Item("SHIHARAI_GROUP_NO_M").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.KANRI_NO.ColNo, dr.Item("INOUTKA_NO_L").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.UNSO_NO.ColNo, dr.Item("UNSO_NO_L").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.UNSO_NO_EDA.ColNo, dr.Item("UNSO_NO_M").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.MOTO_DATA_KBN.ColNo, dr.Item("MOTO_DATA_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.MOTO_DATA.ColNo, dr.Item("MOTO_DATA_NM").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.MINASHI_DEST_CD.ColNo, dr.Item("MINASHI_DEST_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.BIN_KB.ColNo, dr.Item("BIN_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.BIN_NM.ColNo, dr.Item("BIN_NM").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DEST_JIS_CD.ColNo, dr.Item("DEST_JIS_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.CUST_CD.ColNo, Me._LMFconG.EditConcatData(dr.Item("CUST_CD_L").ToString(), dr.Item("CUST_CD_M").ToString(), "-"))
                .SetCellValue(i, LMF080G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, dr.Item("UNTIN_CALCULATION_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.VCLE_KB.ColNo, dr.Item("VCLE_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.UNSO_ONDO_KB.ColNo, dr.Item("UNSO_ONDO_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SIZE_KB.ColNo, dr.Item("SIZE_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.CHK_UNCHIN.ColNo, dr.Item("CHK_UNCHIN").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_UNCHIN.ColNo, dr.Item("SHIHARAI_UNCHIN").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_CITY_EXTC.ColNo, dr.Item("SHIHARAI_CITY_EXTC").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_WINT_EXTC.ColNo, dr.Item("SHIHARAI_WINT_EXTC").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_RELY_EXTC.ColNo, dr.Item("SHIHARAI_RELY_EXTC").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_TOLL.ColNo, dr.Item("SHIHARAI_TOLL").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_INSU.ColNo, dr.Item("SHIHARAI_INSU").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_UNCHIN.ColNo, dr.Item("DECI_UNCHIN").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_CITY_EXTC.ColNo, dr.Item("DECI_CITY_EXTC").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_WINT_EXTC.ColNo, dr.Item("DECI_WINT_EXTC").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_RELY_EXTC.ColNo, dr.Item("DECI_RELY_EXTC").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_TOLL.ColNo, dr.Item("DECI_TOLL").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_INSU.ColNo, dr.Item("DECI_INSU").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.DECI_NG_NB.ColNo, dr.Item("DECI_NG_NB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_PKG_UT.ColNo, dr.Item("SHIHARAI_PKG_UT").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_SYARYO_KB.ColNo, dr.Item("SHIHARAI_SYARYO_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_DANGER_KB.ColNo, dr.Item("SHIHARAI_DANGER_KB").ToString())
                .SetCellValue(i, LMF080G.sprDetailDef.SYS_UPD_FLG.ColNo, String.Empty)
                'START YANAI 要望番号1424 支払処理
                .SetCellValue(i, LMF080G.sprDetailDef.SHIHARAI_UNCHIN_UNSOLL.ColNo, dr.Item("SHIHARAI_UNCHIN_UNSOLL").ToString())
                'END YANAI 要望番号1424 支払処理

                '合計値を設定
                unchinData = Convert.ToDecimal(Me._LMFconG.FormatNumValue(unchin))
                chk = chk AndAlso Me.IsCalc(sokei + unchinData, maxData)
                If chk = True Then
                    sokei += unchinData
                Else
                    sokei = maxData
                End If

            Next

            .ResumeLayout(True)

            Me._Frm.numSokeithi.Value = sokei

            'スプレッドの列の表示・非表示設定
            Call Me.SetSpreadVisible()

            Return chk

        End With

    End Function

    ''' <summary>
    ''' スプレッドのデータを更新
    ''' </summary>
    ''' <param name="frm">frm</param>
    ''' <param name="arr">arr</param>
    ''' <remarks></remarks>
    Friend Function SetUpdSpread(ByVal frm As LMF080F, ByVal arr As ArrayList) As Boolean

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0
        Dim dr() As DataRow = Nothing
        With spr

            .SuspendLayout()

            '----データ挿入----'
            '値設定
            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                'セルに値を設定(更新日付・時間・フラグの更新)
                .SetCellValue(rowNo, LMF080G.sprDetailDef.SYS_UPD_DATE.ColNo, frm.lblSysUpdDate.TextValue)
                .SetCellValue(rowNo, LMF080G.sprDetailDef.SYS_UPD_TIME.ColNo, frm.lblSysUpdTime.TextValue)
                .SetCellValue(rowNo, LMF080G.sprDetailDef.SYS_UPD_FLG.ColNo, LMConst.FLG.ON)

                If (LMF080C.SHUSEI_SHIHARAI).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '支払先コード
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.SHIHARAITO_CD.ColNo, frm.txtShuseiL.TextValue)
                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAITO).Select(String.Concat("SHIHARAITO_CD = '", frm.txtShuseiL.TextValue, "'"))
                    If dr.Length > 0 Then
                        .SetCellValue(rowNo, LMF080G.sprDetailDef.SHIHARAI_NM.ColNo, String.Concat(dr(0).Item("SHIHARAITO_NM").ToString(), "　", dr(0).Item("SHIHARAITO_BUSYO_NM").ToString()))
                    End If

                ElseIf (LMF080C.SHUSEI_TARIFF).Equals(frm.cmbShusei.SelectedValue) = True Then
                    'タリフコード
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.TARIFF_CD.ColNo, frm.txtShuseiL.TextValue)
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.TARIFF_KBN.ColNo, "10")
                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T015' AND ", _
                                                                                                "KBN_CD = '10'"))
                    If dr.Length > 0 Then
                        .SetCellValue(rowNo, LMF080G.sprDetailDef.BUNRUI.ColNo, dr(0).Item("KBN_NM1").ToString)
                    End If

                ElseIf (LMF080C.SHUSEI_YOKO).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '横持タリフコード
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.TARIFF_CD.ColNo, frm.txtShuseiL.TextValue)
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.TARIFF_KBN.ColNo, "40")
                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T015' AND ", _
                                                                                                "KBN_CD = '40'"))
                    If dr.Length > 0 Then
                        .SetCellValue(rowNo, LMF080G.sprDetailDef.BUNRUI.ColNo, dr(0).Item("KBN_NM1").ToString)
                    End If

                    '割増タリフコードを空にする
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, String.Empty)

                ElseIf (LMF080C.SHUSEI_ETARIFF).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '割増タリフコード
                    .SetCellValue(rowNo, LMF080G.sprDetailDef.EXTC_TARIFF_CD.ColNo, frm.txtShuseiL.TextValue)
                End If

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

    ''' <summary>
    ''' 総計チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="max">最大値</param>
    ''' <returns>上限オーバーしている場合、False</returns>
    ''' <remarks></remarks>
    Private Function IsCalc(ByVal value As Decimal, ByVal max As Decimal) As Boolean

        If max < value Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの列の表示・非表示を設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadVisible()

        Dim visibleFlg As Boolean = False

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

    ''' <summary>
    ''' セルのプロパティを設定(半角英数)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextEidaisu(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(MIX)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextMix(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(半角)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextHan(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, length, False)

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁 小数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999, True, 0, , ",")

    End Function
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数最大桁[14])
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNumMax(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, LMFControlC.MAX_KETA_SPR, True, 0, , ",")

    End Function

#End Region

#End Region

#End Region

End Class
