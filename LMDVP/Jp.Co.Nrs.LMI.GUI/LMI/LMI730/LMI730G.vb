' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI730G : 運賃差分抽出（JXTG）
'  作  成  者       :  katagiri
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
''' LMI730Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI730G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI730F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI730F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = LMFControlC.FUNCTION_BACKUP
            .F9ButtonName = LMFControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = always
            .F9ButtonEnabled = always
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

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

            .cmbEigyo.TabIndex = LMI730C.CtlTabIndex.EIGYO
            .pnlCondition.TabIndex = LMI730C.CtlTabIndex.CONDITION
            .cmbDateKb.TabIndex = LMI730C.CtlTabIndex.DATEKB
            .imdFrom.TabIndex = LMI730C.CtlTabIndex.FROM_DATA
            .imdTo.TabIndex = LMI730C.CtlTabIndex.TO_DATA
            .txtTariffCd.TabIndex = LMI730C.CtlTabIndex.TARIFFCD
            .lblTariffNm.TabIndex = LMI730C.CtlTabIndex.TARIFFNM
            .txtCustCdL.TabIndex = LMI730C.CtlTabIndex.CUSTCDL
            .txtCustCdM.TabIndex = LMI730C.CtlTabIndex.CUSTCDM
            .lblCustNm.TabIndex = LMI730C.CtlTabIndex.CUSTNM
            .txtDestCd.TabIndex = LMI730C.CtlTabIndex.DESTCD
            .lblDestNm.TabIndex = LMI730C.CtlTabIndex.DESTNM
            .pnlConditionKbn.TabIndex = LMI730C.CtlTabIndex.CONDITIONKBN
            .pnlRev.TabIndex = LMI730C.CtlTabIndex.REV
            .optRevMi.TabIndex = LMI730C.CtlTabIndex.REVMI
            .optRevKaku.TabIndex = LMI730C.CtlTabIndex.REVKAKU
            .optRevRyoho.TabIndex = LMI730C.CtlTabIndex.REVRYOHO
            .pnlHenko.TabIndex = LMI730C.CtlTabIndex.PNL_HENKO
            .cmbShusei.TabIndex = LMI730C.CtlTabIndex.SHUSEI
            .txtShuseiL.TabIndex = LMI730C.CtlTabIndex.SHUSEIL
            .txtShuseiM.TabIndex = LMI730C.CtlTabIndex.SHUSEIM
            .txtShuseiS.TabIndex = LMI730C.CtlTabIndex.SHUSEIS
            .txtShuseiSS.TabIndex = LMI730C.CtlTabIndex.SHUSEISS
            .btnHenko.TabIndex = LMI730C.CtlTabIndex.HENKO
            .numSokeithi.TabIndex = LMI730C.CtlTabIndex.SOKEITHI
            .sprDetail.TabIndex = LMI730C.CtlTabIndex.DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        'START YANAI 要望番号652
        '編集部の項目をクリア
        Call Me.ClearControl()
        'END YANAI 要望番号652

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
            .cmbDateKb.SelectedValue = LMI730C.SYUKKA_DATE
            .imdFrom.TextValue = String.Empty
            .imdTo.TextValue = String.Empty
            .txtTariffCd.TextValue = String.Empty
            .lblTariffNm.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .txtDestCd.TextValue = String.Empty
            .lblDestNm.TextValue = String.Empty
            .cmbShusei.SelectedValue = Nothing
            .txtShuseiL.TextValue = String.Empty
            .txtShuseiM.TextValue = String.Empty
            .txtShuseiS.TextValue = String.Empty
            .txtShuseiSS.TextValue = String.Empty
            .numSokeithi.Value = 0
            Dim chk As Boolean = True
            Dim unChk As Boolean = False
            .optRevMi.Checked = unChk
            .optRevKaku.Checked = unChk
            .optRevRyoho.Checked = chk

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
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            Dim custCdL As String = String.Empty
            Dim custCdM As String = String.Empty
            If 0 < drs.Length Then

                custCdL = drs(0).Item("CUST_CD_L").ToString()
                custCdM = drs(0).Item("CUST_CD_M").ToString()

            End If

            drs = Me._LMFconG.SelectCustListDataRow(brCd, custCdL, custCdM)
            Dim custNm As String = String.Empty
            If 0 < drs.Length Then

                custNm = Me._LMFconG.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)

            End If

            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            Dim nrsDr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd().ToString()) & "'")(0)

            If Not nrsDr.Item("LOCK_FLG").ToString.Equals("") Then
                Me._Frm.cmbEigyo.ReadOnly = True
            Else
                Me._Frm.cmbEigyo.ReadOnly = False
            End If

            .txtCustCdL.TextValue = custCdL
            .txtCustCdM.TextValue = custCdM
            .lblCustNm.TextValue = custNm

            'ラジオボタンの設定
            .optRevRyoho.Checked = True

            'スプレッドの列の表示・非表示設定
            Call Me.SetSpreadVisible()

        End With

    End Sub


    ''' <summary>
    ''' 修正項目の変更によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockHenkoChangeControl()

        With Me._Frm

            Dim ptn1 As Boolean = True
            Dim ptn2 As Boolean = True

            Select Case .cmbShusei.SelectedValue.ToString()

                Case LMI730C.SHUSEI_CUST

                    ptn1 = False
                    ptn2 = False

                Case LMI730C.SHUSEI_SEIQTO, LMI730C.SHUSEI_TARIFF, LMI730C.SHUSEI_YOKO, LMI730C.SHUSEI_ETARIFF, LMI730C.SHUSEI_ZBUKACD, LMI730C.SHUSEI_ABUKACD, LMI730C.SHUSEI_KYORI

                    ptn1 = False

            End Select

            'ロック制御
            Me._LMFconG.SetLockInputMan(.txtShuseiL, ptn1)
            Me._LMFconG.SetLockInputMan(.txtShuseiM, ptn2)
            Me._LMFconG.SetLockInputMan(.txtShuseiS, ptn2)
            Me._LMFconG.SetLockInputMan(.txtShuseiSS, ptn2)

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared KAKUTEI_FLG As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.KAKUTEI_FLG, "確定フラグ", 0, False)
        Public Shared KAKUTEI As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.KAKUTEI, "確定", 40, True)
        Public Shared SHUKKA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SHUKKA, "出荷日", 80, True)
        Public Shared NONYU As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.NONYU, "納入日", 80, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.CUST_NM, "荷主名", 140, False)
        Public Shared CUST_REF_NO As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.CUST_REF_NO, "伝票№", 50, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQTO_CD, "請求先" & vbCrLf & "コード", 60, False)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQTO_NM, "請求先名", 80, False)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DEST_NM, "届先名", 140, True)
        Public Shared UNSO_NM As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSO_NM, "運送会社名(1次)", 110, True)
        Public Shared BIN_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.BIN_KB, "便区分コード", 0, False)         '常に表示しない(隠しスプレッド)
        Public Shared BIN_NM As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.BIN_NM, "便区分", 90, False)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSOCO_NM, "運送会社名(2次)", 0, False)
        Public Shared TARIFF_KBN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TARIFF_KBN, "タリフ分類区分", 0, False)
        Public Shared BUNRUI As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.BUNRUI, "タリフ分類", 0, False)
        Public Shared TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TARIFF_CD, "ﾀﾘﾌ" & vbCrLf & "ｺｰﾄﾞ", 55, True)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.EXTC_TARIFF_CD, "割増ﾀﾘﾌ" & vbCrLf & "ｺｰﾄﾞ", 55, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.JURYO, "重量", 80, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.KYORI, "距離", 80, True)
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNCHIN, "運賃計", 80, True)
        Public Shared ITEM_CURR_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.ITEM_CURR_CD, "契約通貨" & vbCrLf & "コード", 80, False)
        Public Shared ZBUKA_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.ZBUKA_CD, "在庫部課", 80, True)
        Public Shared ABUKA_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.ABUKA_CD, "扱い部課", 80, True)
        Public Shared ZEI As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.ZEI, "税", 0, False)
        Public Shared ZEI_KBN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.ZEI_KBN, "課税区分", 80, False)
        Public Shared GROUP As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.GROUP, "まとめ", 80, False)
        Public Shared GROUP_M As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.GROUP_M, "まとめM", 70, False)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.REMARK, "運賃備考", 120, False)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.KANRI_NO, "管理番号", 80, True)
        Public Shared UNSO_NO As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSO_NO, "運送番号", 80, True)
        Public Shared UNSO_NO_EDA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSO_NO_EDA, "番号M", 60, False)
        Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TRIP_NO, "運行番号", 80, False)
        Public Shared MOTO_DATA_KBN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.MOTO_DATA_KBN, "(隠し)元データ区分", 0, False)
        Public Shared MOTO_DATA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.MOTO_DATA, "元データ" & vbCrLf & "区分", 80, False)
        Public Shared SHUKA_RELY_POINT As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SHUKA_RELY_POINT, "集荷中継地", 100, False)
        Public Shared HAIKA_RELY_POINT As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.HAIKA_RELY_POINT, "配荷中継地", 100, False)
        Public Shared TRIP_NO_SHUKA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TRIP_NO_SHUKA, "運行番号(集荷)", 130, False)
        Public Shared TRIP_NO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TRIP_NO_CHUKEI, "運行番号(中継)", 130, False)
        Public Shared TRIP_NO_HAIKA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TRIP_NO_HAIKA, "運行番号(配荷)", 130, False)
        Public Shared UNSOCO_SHUKA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSOCO_SHUKA, "運送会社(集荷)", 130, False)
        Public Shared UNSOCO_CHUKEI As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSOCO_CHUKEI, "運送会社(中継)", 130, False)
        Public Shared UNSOCO_HAIKA As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSOCO_HAIKA, "運送会社(配荷)", 130, False)
        Public Shared DEST_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DEST_CD, "届先コード", 110, True)
        Public Shared MINASHI_DEST_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.MINASHI_DEST_CD, "みなし届先", 110, False)
        Public Shared DEST_JIS_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DEST_JIS_CD, "届先JIS", 60, True)
        Public Shared CUST_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.CUST_CD, "荷主" & vbCrLf & "コード", 60, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.CUST_CD_L, "荷主(大)コード", 0, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.CUST_CD_M, "荷主(中)コード", 0, False)
        Public Shared UNSO_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSO_CD, "運送会社(1次)" & vbCrLf & "コード", 110, False)
        Public Shared UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSO_BR_CD, "運送会社(1次)" & vbCrLf & "支店コード", 120, False)
        Public Shared UNSOCO_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSOCO_CD, "運送会社(2次)" & vbCrLf & "コード", 110, False)
        Public Shared UNSOCO_BR_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSOCO_BR_CD, "運送会社(2次)" & vbCrLf & "支店コード", 120, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.NRS_BR_CD, "営業所", 0, False)
        Public Shared TYUKEI_HAISO_FLG As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.TYUKEI_HAISO_FLG, "中継配送フラグ", 0, False)
        Public Shared UNTIN_CALCULATION_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNTIN_CALCULATION_KB, "絞め日基準", 0, False)
        Public Shared VCLE_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.VCLE_KB, "車輌区分", 0, False)
        Public Shared UNSO_ONDO_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.UNSO_ONDO_KB, "温度区分", 0, False)
        Public Shared SIZE_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SIZE_KB, "サイズ区分", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SYS_UPD_TIME, "更新時間", 0, False)
        Public Shared CHK_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.CHK_UNCHIN, "チェック用運賃", 0, False)
        Public Shared SEIQ_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_UNCHIN, "請求運賃", 0, False)
        Public Shared SEIQ_CITY_EXTC As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_CITY_EXTC, "請求都市割増", 0, False)
        Public Shared SEIQ_WINT_EXTC As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_WINT_EXTC, "請求冬期割増", 0, False)
        Public Shared SEIQ_RELY_EXTC As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_RELY_EXTC, "請求中継料", 0, False)
        Public Shared SEIQ_TOLL As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_TOLL, "請求通行料", 0, False)
        Public Shared SEIQ_INSU As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_INSU, "請求保険料", 0, False)
        Public Shared DECI_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_UNCHIN, "確定運賃", 0, False)
        Public Shared DECI_CITY_EXTC As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_CITY_EXTC, "確定都市割増", 0, False)
        Public Shared DECI_WINT_EXTC As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_WINT_EXTC, "確定冬期割増", 0, False)
        Public Shared DECI_RELY_EXTC As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_RELY_EXTC, "確定中継料", 0, False)
        Public Shared DECI_TOLL As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_TOLL, "確定通行料", 0, False)
        Public Shared DECI_INSU As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_INSU, "確定保険料", 0, False)
        Public Shared DECI_NG_NB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DECI_NG_NB, "個数", 0, False)
        Public Shared SEIQ_PKG_UT As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_PKG_UT, "荷姿", 0, False)
        Public Shared SEIQ_SYARYO_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_SYARYO_KB, "車両区分(運賃)", 0, False)
        Public Shared SEIQ_DANGER_KB As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SEIQ_DANGER_KB, "危険区分", 0, False)
        Public Shared SYS_UPD_FLG As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.SYS_UPD_FLG, "更新フラグ", 0, False)
        Public Shared DEST_ADDR As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.DEST_ADDR, "届先住所", 300, True)
        Public Shared BEFORE_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.BEFORE_TARIFF_CD, "元" & vbCrLf & "ﾀﾘﾌｺｰﾄﾞ", 70, True)
        Public Shared BEFORE_UNCHIN As SpreadColProperty = New SpreadColProperty(LMI730C.SprColumnIndex.BEFORE_UNCHIN, "元運賃計", 80, True)

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
            .ActiveSheet.ColumnCount = LMI730C.SprColumnIndex.LAST

            .SetColProperty(New LMI730G.sprDetailDef(), False)

            '列固定位置を設定します。(ex.納入予定で固定)
            .ActiveSheet.FrozenColumnCount = LMI730G.sprDetailDef.CUST_NM.ColNo + 1

            '列設定
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sEidaisu3 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 3)
            Dim sEidaisu5 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 5)
            Dim sEidaisu7 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 7)
            Dim sEidaisu9 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 9)
            Dim sEidaisu10 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 10)
            Dim sEidaisu30 As StyleInfo = Me.StyleInfoTextEidaisu(spr, 30)
            Dim sHan15 As StyleInfo = Me.StyleInfoTextHan(spr, 15)
            Dim sMix50 As StyleInfo = Me.StyleInfoTextMix(spr, 50)
            Dim sMix60 As StyleInfo = Me.StyleInfoTextMix(spr, 60)
            Dim sMix80 As StyleInfo = Me.StyleInfoTextMix(spr, 80)
            Dim sMix100 As StyleInfo = Me.StyleInfoTextMix(spr, 100)
            Dim sMix122 As StyleInfo = Me.StyleInfoTextMix(spr, 122)

            .SetCellStyle(0, LMI730G.sprDetailDef.KAKUTEI_FLG.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.KAKUTEI.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SHUKKA.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.NONYU.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.CUST_NM.ColNo, sMix122)
            .SetCellStyle(0, LMI730G.sprDetailDef.CUST_REF_NO.ColNo, sEidaisu30)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQTO_CD.ColNo, sEidaisu7)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQTO_NM.ColNo, sMix60)
            .SetCellStyle(0, LMI730G.sprDetailDef.DEST_NM.ColNo, sMix80)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSO_NM.ColNo, sMix122)
            .SetCellStyle(0, LMI730G.sprDetailDef.BIN_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.BIN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_U001, False))
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSOCO_NM.ColNo, sMix122)
            .SetCellStyle(0, LMI730G.sprDetailDef.TARIFF_KBN.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.BUNRUI.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.TARIFF_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.JURYO.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.KYORI.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.ITEM_CURR_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.ZBUKA_CD.ColNo, sEidaisu7)
            .SetCellStyle(0, LMI730G.sprDetailDef.ABUKA_CD.ColNo, sEidaisu7)
            .SetCellStyle(0, LMI730G.sprDetailDef.ZEI.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.ZEI_KBN.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Z001, False))
            .SetCellStyle(0, LMI730G.sprDetailDef.GROUP.ColNo, sEidaisu9)
            .SetCellStyle(0, LMI730G.sprDetailDef.GROUP_M.ColNo, sEidaisu3)
            .SetCellStyle(0, LMI730G.sprDetailDef.REMARK.ColNo, sMix100)
            .SetCellStyle(0, LMI730G.sprDetailDef.KANRI_NO.ColNo, sEidaisu9)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSO_NO.ColNo, sEidaisu9)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSO_NO_EDA.ColNo, sEidaisu3)
            .SetCellStyle(0, LMI730G.sprDetailDef.TRIP_NO.ColNo, sEidaisu10)
            .SetCellStyle(0, LMI730G.sprDetailDef.MOTO_DATA_KBN.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.MOTO_DATA.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SHUKA_RELY_POINT.ColNo, sMix50)
            .SetCellStyle(0, LMI730G.sprDetailDef.HAIKA_RELY_POINT.ColNo, sMix50)
            .SetCellStyle(0, LMI730G.sprDetailDef.TRIP_NO_SHUKA.ColNo, sEidaisu10)
            .SetCellStyle(0, LMI730G.sprDetailDef.TRIP_NO_CHUKEI.ColNo, sEidaisu10)
            .SetCellStyle(0, LMI730G.sprDetailDef.TRIP_NO_HAIKA.ColNo, sEidaisu10)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSOCO_SHUKA.ColNo, sMix122)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSOCO_CHUKEI.ColNo, sMix122)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSOCO_HAIKA.ColNo, sMix122)
            .SetCellStyle(0, LMI730G.sprDetailDef.DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.MINASHI_DEST_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DEST_JIS_CD.ColNo, sEidaisu7)
            .SetCellStyle(0, LMI730G.sprDetailDef.CUST_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSO_CD.ColNo, sEidaisu5)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSO_BR_CD.ColNo, sEidaisu3)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSOCO_CD.ColNo, sEidaisu5)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSOCO_BR_CD.ColNo, sEidaisu3)
            .SetCellStyle(0, LMI730G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.VCLE_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.UNSO_ONDO_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SIZE_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.CHK_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_CITY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_WINT_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_RELY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_TOLL.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_INSU.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_UNCHIN.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_CITY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_WINT_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_RELY_EXTC.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_TOLL.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_INSU.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DECI_NG_NB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_PKG_UT.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_SYARYO_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SEIQ_DANGER_KB.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.SYS_UPD_FLG.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.DEST_ADDR.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.BEFORE_TARIFF_CD.ColNo, sLabel)
            .SetCellStyle(0, LMI730G.sprDetailDef.BEFORE_UNCHIN.ColNo, sLabel)


            Dim max As Integer = spr.ActiveSheet.Columns.Count - 1
            For i As Integer = 1 To max
                .SetCellValue(0, i, String.Empty)
            Next

            '初期荷主から値取得
            Dim drs As DataRow() = Me._LMFconG.SelectTCustListDataRow(LMUserInfoManager.GetUserID())
            If 0 < drs.Length Then

                Dim brCd As String = LMUserInfoManager.GetNrsBrCd() '20160928 要番2622 tsunehira add
                Dim custCdL As String = drs(0).Item("CUST_CD_L").ToString()
                Dim custCdM As String = drs(0).Item("CUST_CD_M").ToString()
                Me._Frm.txtCustCdL.TextValue = custCdL
                Me._Frm.txtCustCdL.TextValue = custCdM
                drs = Me._LMFconG.SelectCustListDataRow(brCd, custCdL, custCdM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF) '20160928 要番2622 tsunehira add
                If 0 < drs.Length Then
                    Me._Frm.lblCustNm.TextValue = Me._LMFconG.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)
                End If

            End If

        End With

    End Sub

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

                If dr("UNCHIN").ToString().Equals("0.00") And dr("SEIQ_GROUP_NO").ToString().Equals(String.Empty) Then
                    '運賃=\0 かつ　まとめ番号が空白の場合は、赤文字
                    .ActiveSheet.Rows(i).ForeColor = Color.Red
                End If

            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet) As Boolean

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMI730C.TABLE_NM_OUT)
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

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum3 As StyleInfo = Me.StyleInfoNum3(spr)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr)
            Dim sNum9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr)
            Dim sNum14 As StyleInfo = Me.StyleInfoNumMax(spr)
            Dim sNum12dec2 As StyleInfo = Me.StyleInfoNum12dec2(spr)
            Dim unchin As String = String.Empty
            Dim unchinData As Decimal = 0
            Dim chk As Boolean = True
            Dim maxData As Decimal = Convert.ToDecimal(LMFControlC.MAX_KETA)

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMI730G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMI730G.sprDetailDef.KAKUTEI_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.KAKUTEI.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SHUKKA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.NONYU.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.CUST_REF_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.BIN_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.BIN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TARIFF_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.BUNRUI.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.JURYO.ColNo, sNum9dec3)
                .SetCellStyle(i, LMI730G.sprDetailDef.KYORI.ColNo, sNum5)
                If dr.Item("ROUND_POS").ToString() = "2" Then
                    .SetCellStyle(i, LMI730G.sprDetailDef.UNCHIN.ColNo, sNum12dec2)
                Else
                    .SetCellStyle(i, LMI730G.sprDetailDef.UNCHIN.ColNo, sNum14)
                End If
                .SetCellStyle(i, LMI730G.sprDetailDef.ITEM_CURR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.ZBUKA_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.ABUKA_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.ZEI.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.ZEI_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.GROUP.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.GROUP_M.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.KANRI_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSO_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSO_NO_EDA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.MOTO_DATA_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.MOTO_DATA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SHUKA_RELY_POINT.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.HAIKA_RELY_POINT.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TRIP_NO_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TRIP_NO_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TRIP_NO_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSOCO_SHUKA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSOCO_CHUKEI.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSOCO_HAIKA.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.MINASHI_DEST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DEST_JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.CUST_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSOCO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSOCO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.VCLE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.UNSO_ONDO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SIZE_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.CHK_UNCHIN.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_UNCHIN.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_CITY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_WINT_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_RELY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_TOLL.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_INSU.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_UNCHIN.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_CITY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_WINT_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_RELY_EXTC.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_TOLL.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_INSU.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DECI_NG_NB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_SYARYO_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SEIQ_DANGER_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.SYS_UPD_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.DEST_ADDR.ColNo, sLabel)
                .SetCellStyle(i, LMI730G.sprDetailDef.BEFORE_TARIFF_CD.ColNo, sLabel)
                If dr.Item("ROUND_POS").ToString() = "2" Then
                    .SetCellStyle(i, LMI730G.sprDetailDef.BEFORE_UNCHIN.ColNo, sNum12dec2)
                Else
                    .SetCellStyle(i, LMI730G.sprDetailDef.BEFORE_UNCHIN.ColNo, sNum14)
                End If
                

                'セルに値を設定
                .SetCellValue(i, LMI730G.sprDetailDef.DEF.ColNo, False.ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.KAKUTEI.ColNo, dr.Item("SEIQ_FIXED_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.KAKUTEI_FLG.ColNo, dr.Item("SEIQ_FIXED_FLAG").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SHUKKA.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(i, LMI730G.sprDetailDef.NONYU.ColNo, DateFormatUtility.EditSlash(dr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(i, LMI730G.sprDetailDef.CUST_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMI730G.sprDetailDef.CUST_REF_NO.ColNo, dr.Item("CUST_REF_NO").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSO_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSO_NM").ToString(), dr.Item("UNSO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMI730G.sprDetailDef.BIN_KB.ColNo, dr.Item("BIN_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.BIN_NM.ColNo, dr.Item("BIN_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSOCO_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMI730G.sprDetailDef.TARIFF_KBN.ColNo, dr.Item("SEIQ_TARIFF_BUNRUI_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.BUNRUI.ColNo, dr.Item("TARIFF_BUNRUI").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.TARIFF_CD.ColNo, dr.Item("SEIQ_TARIFF_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.EXTC_TARIFF_CD.ColNo, dr.Item("SEIQ_ETARIFF_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.JURYO.ColNo, dr.Item("DECI_WT").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.KYORI.ColNo, dr.Item("DECI_KYORI").ToString())
                unchin = dr.Item("UNCHIN").ToString()
                .SetCellValue(i, LMI730G.sprDetailDef.UNCHIN.ColNo, unchin)
                .SetCellValue(i, LMI730G.sprDetailDef.ITEM_CURR_CD.ColNo, dr.Item("ITEM_CURR_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.ZBUKA_CD.ColNo, dr.Item("ZBUKA_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.ABUKA_CD.ColNo, dr.Item("ABUKA_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.ZEI.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.ZEI_KBN.ColNo, dr.Item("TAX_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.GROUP.ColNo, dr.Item("SEIQ_GROUP_NO").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.GROUP_M.ColNo, dr.Item("SEIQ_GROUP_NO_M").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.KANRI_NO.ColNo, dr.Item("INOUTKA_NO_L").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSO_NO.ColNo, dr.Item("UNSO_NO_L").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSO_NO_EDA.ColNo, dr.Item("UNSO_NO_M").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.MOTO_DATA_KBN.ColNo, dr.Item("MOTO_DATA_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.MOTO_DATA.ColNo, dr.Item("MOTO_DATA_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SHUKA_RELY_POINT.ColNo, dr.Item("SYUKA_TYUKEI_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.HAIKA_RELY_POINT.ColNo, dr.Item("HAIKA_TYUKEI_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.TRIP_NO_SHUKA.ColNo, dr.Item("TRIP_NO_SYUKA").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.TRIP_NO_CHUKEI.ColNo, dr.Item("TRIP_NO_TYUKEI").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.TRIP_NO_HAIKA.ColNo, dr.Item("TRIP_NO_HAIKA").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSOCO_SHUKA.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_SYUKA").ToString(), dr.Item("UNSOCO_BR_SYUKA").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMI730G.sprDetailDef.UNSOCO_CHUKEI.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_TYUKEI").ToString(), dr.Item("UNSOCO_BR_TYUKEI").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMI730G.sprDetailDef.UNSOCO_HAIKA.ColNo, Me._LMFconG.EditConcatData(dr.Item("UNSOCO_HAIKA").ToString(), dr.Item("UNSOCO_BR_HAIKA").ToString(), LMFControlC.ZENKAKU_SPACE))
                .SetCellValue(i, LMI730G.sprDetailDef.DEST_CD.ColNo, dr.Item("DEST_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.MINASHI_DEST_CD.ColNo, dr.Item("MINASHI_DEST_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.BIN_KB.ColNo, dr.Item("BIN_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.BIN_NM.ColNo, dr.Item("BIN_NM").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DEST_JIS_CD.ColNo, dr.Item("DEST_JIS_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.CUST_CD.ColNo, Me._LMFconG.EditConcatData(dr.Item("CUST_CD_L").ToString(), dr.Item("CUST_CD_M").ToString(), "-"))
                .SetCellValue(i, LMI730G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSO_CD.ColNo, dr.Item("UNSO_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSO_BR_CD.ColNo, dr.Item("UNSO_BR_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSOCO_CD.ColNo, dr.Item("UNSOCO_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSOCO_BR_CD.ColNo, dr.Item("UNSOCO_BR_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.TYUKEI_HAISO_FLG.ColNo, dr.Item("TYUKEI_HAISO_FLG").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, dr.Item("UNTIN_CALCULATION_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.VCLE_KB.ColNo, dr.Item("VCLE_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.UNSO_ONDO_KB.ColNo, dr.Item("UNSO_ONDO_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SIZE_KB.ColNo, dr.Item("SIZE_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.CHK_UNCHIN.ColNo, dr.Item("CHK_UNCHIN").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_UNCHIN.ColNo, dr.Item("SEIQ_UNCHIN").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_CITY_EXTC.ColNo, dr.Item("SEIQ_CITY_EXTC").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_WINT_EXTC.ColNo, dr.Item("SEIQ_WINT_EXTC").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_RELY_EXTC.ColNo, dr.Item("SEIQ_RELY_EXTC").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_TOLL.ColNo, dr.Item("SEIQ_TOLL").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_INSU.ColNo, dr.Item("SEIQ_INSU").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DECI_UNCHIN.ColNo, dr.Item("DECI_UNCHIN").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DECI_CITY_EXTC.ColNo, dr.Item("DECI_CITY_EXTC").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DECI_WINT_EXTC.ColNo, dr.Item("DECI_WINT_EXTC").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DECI_RELY_EXTC.ColNo, dr.Item("DECI_RELY_EXTC").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DECI_TOLL.ColNo, dr.Item("DECI_TOLL").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.DECI_NG_NB.ColNo, dr.Item("DECI_NG_NB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_PKG_UT.ColNo, dr.Item("SEIQ_PKG_UT").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_SYARYO_KB.ColNo, dr.Item("SEIQ_SYARYO_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SEIQ_DANGER_KB.ColNo, dr.Item("SEIQ_DANGER_KB").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.SYS_UPD_FLG.ColNo, String.Empty)
                .SetCellValue(i, LMI730G.sprDetailDef.DEST_ADDR.ColNo, dr.Item("DEST_ADDR").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.BEFORE_TARIFF_CD.ColNo, dr.Item("BK_TARIFF_CD").ToString())
                .SetCellValue(i, LMI730G.sprDetailDef.BEFORE_UNCHIN.ColNo, dr.Item("BK_UNCHIN").ToString())

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
    Friend Function SetUpdSpread(ByVal frm As LMI730F, ByVal arr As ArrayList) As Boolean

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0
        With spr

            .SuspendLayout()

            '----データ挿入----'
            '値設定
            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                'セルに値を設定(更新日付・時間・フラグの更新)
                .SetCellValue(rowNo, LMI730G.sprDetailDef.SYS_UPD_DATE.ColNo, frm.lblSysUpdDate.TextValue)
                .SetCellValue(rowNo, LMI730G.sprDetailDef.SYS_UPD_TIME.ColNo, frm.lblSysUpdTime.TextValue)
                .SetCellValue(rowNo, LMI730G.sprDetailDef.SYS_UPD_FLG.ColNo, LMConst.FLG.ON)

                If (LMI730C.SHUSEI_SEIQTO).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '請求先コード
                    .SetCellValue(rowNo, LMI730G.sprDetailDef.SEIQTO_CD.ColNo, frm.txtShuseiL.TextValue)
                ElseIf (LMI730C.SHUSEI_TARIFF).Equals(frm.cmbShusei.SelectedValue) = True OrElse _
                        (LMI730C.SHUSEI_YOKO).Equals(frm.cmbShusei.SelectedValue) = True Then
                    'タリフコード、横持タリフコード
                    .SetCellValue(rowNo, LMI730G.sprDetailDef.TARIFF_CD.ColNo, frm.txtShuseiL.TextValue)
                ElseIf (LMI730C.SHUSEI_CUST).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '荷主コード
                    .SetCellValue(rowNo, LMI730G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo, frm.lblCalcKbn.TextValue)
                ElseIf (LMI730C.SHUSEI_ETARIFF).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '割増タリフコード
                    .SetCellValue(rowNo, LMI730G.sprDetailDef.EXTC_TARIFF_CD.ColNo, frm.txtShuseiL.TextValue)
                ElseIf (LMI730C.SHUSEI_ZBUKACD).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '在庫部課コード
                    .SetCellValue(rowNo, LMI730G.sprDetailDef.ZBUKA_CD.ColNo, frm.txtShuseiL.TextValue)
                ElseIf (LMI730C.SHUSEI_ABUKACD).Equals(frm.cmbShusei.SelectedValue) = True Then
                    '扱い部課コード
                    .SetCellValue(rowNo, LMI730G.sprDetailDef.ABUKA_CD.ColNo, frm.txtShuseiL.TextValue)
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
        '標準
        visibleFlg = False

        With Me._Frm.sprDetail

            .SuspendLayout()

            .ActiveSheet.Columns(LMI730G.sprDetailDef.CUST_REF_NO.ColNo).Visible = visibleFlg
            .ActiveSheet.Columns(LMI730G.sprDetailDef.ZBUKA_CD.ColNo).Visible = visibleFlg
            .ActiveSheet.Columns(LMI730G.sprDetailDef.ABUKA_CD.ColNo).Visible = visibleFlg
            .ActiveSheet.Columns(LMI730G.sprDetailDef.MINASHI_DEST_CD.ColNo).Visible = visibleFlg

            .ResumeLayout(True)

        End With

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
    ''' セルのプロパティを設定(Number 整数12桁 小数2桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec2(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.99, True, 2, , ",")

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
