' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF090G : 支払検索
'  作  成  者       :  YANAI
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMF090Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF090G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF090F

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF090V

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF090F, ByRef g As LMFControlG, ByRef v As LMF090V)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMFconG = g

        Me._V = v

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal eventType As LMF090C.EventShubetsu, ByVal renzokuFLG As Boolean)

        Dim always As Boolean = True
        Dim allLock As Boolean = False
        Dim lock1 As Boolean = False
        Dim lock2 As Boolean = False
        Dim lock3 As Boolean = False
        Dim lock4 As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMFControlC.FUNCTION_HENSHU
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = LMFControlC.FUNCTION_KAKUTEI
            .F6ButtonName = LMFControlC.FUNCTION_KAIJO
            .F7ButtonName = LMFControlC.FUNCTION_SKIP
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = LMFControlC.FUNCTION_HOZON
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE
            'ファンクションキーの制御
            Select Case Me._Frm.lblSituation.DispMode
                '参照モード
                Case DispMode.VIEW

                    lock1 = True
                    lock2 = True

                    '編集ボタン以外はロック解除
                    If LMF090C.EventShubetsu.HENSHU <> eventType Then
                        lock4 = True
                    End If

                    '編集モード
                Case DispMode.EDIT

                    lock3 = True

                    '参照モード(初期)
                Case DispMode.INIT

                    lock1 = True

                    '編集ボタン以外はロック解除
                    Select Case eventType
                        Case LMF090C.EventShubetsu.MAIN
                            lock2 = True
                            lock4 = True
                    End Select

            End Select

            .F1ButtonEnabled = allLock
            .F2ButtonEnabled = lock2
            .F3ButtonEnabled = allLock
            .F4ButtonEnabled = allLock
            .F5ButtonEnabled = lock2
            .F6ButtonEnabled = lock2
            If renzokuFLG = True Then
                '連続処理時
                .F7ButtonEnabled = always
            Else
                .F7ButtonEnabled = allLock
            End If
            .F8ButtonEnabled = allLock
            .F9ButtonEnabled = allLock
            .F10ButtonEnabled = lock3
            .F11ButtonEnabled = lock3
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
    Friend Sub SetModeAndStatus(Optional ByVal mode As String = DispMode.VIEW, _
                                Optional ByVal status As String = RecordStatus.NOMAL_REC)

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

            .sprDetail.TabIndex = LMF090C.CtlTabIndex.DETAIL
            .cmbEigyo.TabIndex = LMF090C.CtlTabIndex.Eigyo
            .imdShukka.TabIndex = LMF090C.CtlTabIndex.SHUKKA
            .imdArr.TabIndex = LMF090C.CtlTabIndex.NONYU
            .lblUnsoCd.TabIndex = LMF090C.CtlTabIndex.UNSOCDL
            .lblUnsoBrCd.TabIndex = LMF090C.CtlTabIndex.UNSOCDM
            .lblUnsoNm.TabIndex = LMF090C.CtlTabIndex.UNSONM
            .cmbTariffKbn.TabIndex = LMF090C.CtlTabIndex.TARIFFKBN
            .numKyori.TabIndex = LMF090C.CtlTabIndex.Kyori
            .txtTariffCd.TabIndex = LMF090C.CtlTabIndex.TariffCd
            .lblTariffNm.TabIndex = LMF090C.CtlTabIndex.TARIFFNM
            .cmbKiken.TabIndex = LMF090C.CtlTabIndex.Kiken
            .numJuryo.TabIndex = LMF090C.CtlTabIndex.Juryo
            .txtWarimashi.TabIndex = LMF090C.CtlTabIndex.WarimashiCd
            .lblWarimashi.TabIndex = LMF090C.CtlTabIndex.WarimashiNm
            .cmbShashu.TabIndex = LMF090C.CtlTabIndex.Shashu
            .numKosu.TabIndex = LMF090C.CtlTabIndex.KOSU
            .txtUnsoNo.TabIndex = LMF090C.CtlTabIndex.UNSONO
            .cmbNisugata.TabIndex = LMF090C.CtlTabIndex.NISUGATA
            .txtKanriNo.TabIndex = LMF090C.CtlTabIndex.KANRINO
            .btnKeisan.TabIndex = LMF090C.CtlTabIndex.BTNKEISAN

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d10 As Decimal = Convert.ToDecimal("9999999999")
            Dim d5 As Decimal = Convert.ToDecimal("99999")
            Dim d12d3 As Decimal = Convert.ToDecimal("999,999,999.999")

            .numKyori.SetInputFields("##,##0", ",", 5, 1, , 0, 0, , d5, 0)
            .numJuryo.SetInputFields("###,###,##0.000", , 9, 1, , , 3, , d12d3, 0)
            .numKosu.SetInputFields("#,###,###,##0", ",", 10, 1, , 0, 0, , d10, 0)

        End With

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbNrsBrCd()

        Me._Frm.cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

    End Sub

    ''' <summary>
    ''' 初期値設定
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <remarks></remarks>
    Friend Sub SetInitValu(ByVal dr As DataRow)

        With Me._Frm

            .lblUnsoNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE)
            .lblUnsoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
            .lblUnsoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
            .imdShukka.TextValue = dr.Item("OUTKA_PLAN_DATE").ToString()
            .imdArr.TextValue = dr.Item("ARR_PLAN_DATE").ToString()
            .cmbEigyo.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .cmbTariffKbn.SelectedValue = dr.Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString()
            .numKyori.Value = dr.Item("DECI_KYORI").ToString()
            .txtTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
            .lblTariffNm.TextValue = dr.Item("SHIHARAI_TARIFF_NM").ToString()
            .cmbKiken.SelectedValue = dr.Item("SHIHARAI_DANGER_KB").ToString()
            .numJuryo.Value = dr.Item("DECI_WT").ToString()
            .txtWarimashi.TextValue = dr.Item("SHIHARAI_ETARIFF_CD").ToString()
            .lblWarimashi.TextValue = dr.Item("SHIHARAI_ETARIFF_NM").ToString()
            .cmbShashu.SelectedValue = dr.Item("SHIHARAI_SYARYO_KB").ToString()
            .numKosu.Value = dr.Item("DECI_NG_NB").ToString()
            .cmbNisugata.SelectedValue = dr.Item("SHIHARAI_PKG_UT_KB").ToString()
            .txtUnsoNo.TextValue = dr.Item("UNSO_NO_L").ToString()
            .txtKanriNo.TextValue = dr.Item("INOUTKA_NO_L").ToString()

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Dim lock As Boolean = True
            Dim unLock As Boolean = False

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Me.LockControl(lock)
                    Me.SetLockControl(.cmbEigyo, lock)
                    Me.SetLockControl(.imdShukka, lock)
                    Me.SetLockControl(.btnKeisan, lock)

                Case DispMode.EDIT

                    Me.LockControl(lock)
                    Me.SetLockControl(.cmbEigyo, lock)
                    Me.SetLockControl(.imdShukka, lock)
                    Me.SetLockControl(.btnKeisan, unLock)
                    Me.SetLockControl(.cmbTariffKbn, unLock)

                Case DispMode.INIT
                    Me.LockControl(lock)
                    Me.SetLockControl(.cmbEigyo, lock)
                    Me.SetLockControl(.imdShukka, lock)

            End Select

            '入荷着払いの時はロック
            Call Me.Nyuka()

        End With

    End Sub

    ''' <summary>
    ''' タリフ分類区分値変更のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Locktairff()

        With Me._Frm

            Dim lockflg1 As Boolean = True
            Dim lockflg2 As Boolean = True
            Dim lockflg3 As Boolean = True
            Dim lockflg4 As Boolean = True
            Dim lockflg5 As Boolean = True
            Dim lockflg6 As Boolean = False

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            'タリフ分類区分の値
            Dim tariffbun As String = .cmbTariffKbn.SelectedValue.ToString()

            If LMFControlC.TARIFF_YOKO.Equals(tariffbun) = True Then
                lockflg1 = False
                lockflg2 = False
            Else
                lockflg5 = False
            End If

            If LMFControlC.TARIFF_KURUMA.Equals(tariffbun) = True Then
                lockflg2 = False
            Else
                lockflg3 = False
            End If

            Select Case tariffbun

                Case LMFControlC.TARIFF_YOKO, LMFControlC.TARIFF_INKA
                Case Else

                    lockflg4 = False

            End Select

            '横持ちの場合
            If LMFControlC.TARIFF_YOKO.Equals(tariffbun) Then

            End If

            'タリフ分類区分が空欄の場合
            If String.IsNullOrEmpty(tariffbun) = True Then

                '全てロック
                lockflg1 = True
                lockflg2 = True
                lockflg3 = True
                lockflg4 = True
                lockflg5 = True
                lockflg6 = True

            End If

            '横持ちの場合、活性化
            Call Me._LMFconG.SetLockInputMan(.cmbKiken, lockflg1)
            Call Me._LMFconG.SetLockInputMan(.cmbNisugata, lockflg1)

            '横持ち、車扱いの場合、活性化
            Call Me._LMFconG.SetLockInputMan(.cmbShashu, lockflg2)

            '車扱い以外、活性化
            Call Me._LMFconG.SetLockInputMan(.numJuryo, lockflg3)

            '横持ち、入荷着払い以外、活性化
            Call Me._LMFconG.SetLockInputMan(.txtWarimashi, lockflg4)
            If lockflg4 = True Then
                .lblWarimashi.TextValue = String.Empty
            End If
            '横持ちの以外、活性化
            Call Me._LMFconG.SetLockInputMan(.numKyori, lockflg5)

            'タリフ分類区分に値がある場合、活性化
            Call Me._LMFconG.SetLockInputMan(.numKosu, lockflg6)
            Call Me._LMFconG.SetLockInputMan(.txtTariffCd, lockflg6)
            If lockflg6 = True Then
                .lblTariffNm.TextValue = String.Empty
            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbTariffKbn.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbTariffKbn.TextValue = String.Empty
            .numKyori.Value = 0
            .txtTariffCd.TextValue = String.Empty
            .lblTariffNm.TextValue = String.Empty
            .cmbKiken.TextValue = String.Empty
            .numJuryo.Value = 0
            .txtWarimashi.TextValue = String.Empty
            .lblWarimashi.TextValue = String.Empty
            .cmbShashu.TextValue = String.Empty
            .numKosu.Value = 0
            .cmbNisugata.TextValue = String.Empty

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UNSO_TEHAI As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.UNSO_TEHAI, "タリフ分類", 100, True)
        Public Shared DEST_JIS As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.DEST_JIS, "届先JIS", 90, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.DEST_NM, "届先名", 90, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.CUST_CD_L, "荷主コード(大)", 0, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.CUST_CD_M, "荷主コード(中)", 0, True)
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.CUST_NM, "荷主名", 90, True)
        Public Shared TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.TARIFF_CD, "タリフ" & vbCrLf & "コード", 90, True)
        Public Shared SHIHARAITO_CD As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAITO_CD, "支払先コード", 90, True)
        Public Shared SHIHARAI_NM As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAI_NM, "支払先名", 90, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.JURYO, "重量", 120, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.KYORI, "距離", 80, True)
        Public Shared KOSU As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.KOSU, "個数", 110, True)
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.UNCHIN, "運賃", 110, True)
        Public Shared TOSHI As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.TOSHI, "都市割増", 110, True)
        Public Shared TOKI As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.TOKI, "冬期割増", 110, True)
        Public Shared TYUKEI As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.TYUKEI, "中継料", 110, True)
        Public Shared KOSO As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.KOSO, "通行・航送料", 110, True)
        Public Shared SONOTA As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SONOTA, "保険料他", 110, True)
        Public Shared ZEI_KB As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.ZEI_KB, "課税区分", 70, True)
        Public Shared WARIMASHI As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.WARIMASHI, "割増タリフ" & vbCrLf & "コード", 90, True)
        Public Shared GROUP As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.GROUP, "まとめ", 60, True)
        Public Shared GROUP_UNSO As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.GROUP_UNSO, "まとめM", 70, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.REMARK, "支払備考", 70, True)
        Public Shared UNSO_NO As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.UNSO_NO, "運送番号", 80, True)
        Public Shared UNSO_NO_EDA As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.UNSO_NO_EDA, "運送M", 60, True)
        Public Shared TRIP_NO As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.TRIP_NO, "運行番号", 80, True)
        Public Shared ONDO As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.ONDO, "輸送温度", 140, True)
        Public Shared NISUGATA As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.NISUGATA, "荷姿", 80, True)
        Public Shared SHASHU As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHASHU, "車輌", 80, True)
        Public Shared KIKEN As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.KIKEN, "危険", 80, True)

        '隠し項目
        Public Shared TARIF_BUN As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.TARIFF_KB, "タリフ分類区分", 0, False)
        Public Shared PKG_KB As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.PKG_KB, "荷姿区分", 0, False)
        Public Shared SYARYO_KB As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SYARYO_KB, "車種区分", 0, False)
        Public Shared DANGER_KB As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.DANGER_KB, "危険区分", 0, False)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.NRS_BR_CD, "営業所コード", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SYS_UPD_DATE, "更新日付", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SYS_UPD_TIME, "更新日時", 0, False)

        '請求鑑ヘッダ用
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷日", 0, False)
        Public Shared ARR_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.ARR_PLAN_DATE, "納入日", 0, False)

        Public Shared SHIHARAI_TARIFF_CD_LL As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAI_TARIFF_CD_LL, "支払タリフコード(運行)", 0, False)
        Public Shared SHIHARAI_ETARIFF_CD_LL As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAI_ETARIFF_CD_LL, "支払割増タリフコード(運行)", 0, False)
        Public Shared SHIHARAI_UNSO_WT_LL As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAI_UNSO_WT_LL, "支払運送重量(運行)", 0, False)
        Public Shared SHIHARAI_COUNT_LL As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAI_COUNT_LL, "支払件数(運行)", 0, False)
        Public Shared SHIHARAI_UNCHIN_LL As SpreadColProperty = New SpreadColProperty(LMF090C.SprColumnIndex.SHIHARAI_UNCHIN_LL, "支払金額(運行)", 0, False)

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
            .ActiveSheet.ColumnCount = LMF090C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMF090G.sprDetailDef())

            '列固定位置を設定します。(チェックボックスで固定)
            .ActiveSheet.FrozenColumnCount = LMF090G.sprDetailDef.DEF.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMF090C.TABLE_NM_OUT)

        With spr

            .SuspendLayout()

            'スプレッドの行をクリア
            .CrearSpread()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '参照モードの場合、
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Call Me.SetSprViewData(spr, dt)
            Else
                Call Me.SetSprEditData(spr, dt)
            End If

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 参照モードの場合の値設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="dt">DataTable</param>
    ''' <remarks></remarks>
    Private Sub SetSprViewData(ByVal spr As LMSpread, ByVal dt As DataTable)

        With spr

            Dim lock As Boolean = True
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum7 As StyleInfo = Me.StyleInfoNum7(spr, True)
            Dim sNum9d3 As StyleInfo = Me.StyleInfoNum9dec3(spr, True)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr, True)
            Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr, True)
            Dim sCom As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Z001, True)
            Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, True)
            Dim taxFlg As Boolean = False
            Dim max As Integer = dt.Rows.Count - 1

            '値設定
            For i As Integer = 0 To max

                .SetCellStyle(i, LMF090G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNSO_TEHAI.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.DEST_JIS.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.SHIHARAITO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.SHIHARAI_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.JURYO.ColNo, sNum9d3)
                .SetCellStyle(i, LMF090G.sprDetailDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF090G.sprDetailDef.KOSU.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNCHIN.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.TOSHI.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.TOKI.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.TYUKEI.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.KOSO.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.SONOTA.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.ZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.WARIMASHI.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.GROUP.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.GROUP_UNSO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.REMARK.ColNo, sTxt)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNSO_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.ONDO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.NISUGATA.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.SHASHU.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.KIKEN.ColNo, sLabel)

                Call Me.SetSprData(spr, dt.Rows(i), i, taxFlg)

            Next

        End With

    End Sub

    ''' <summary>
    ''' 編集モードの場合の値設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="dt">DataTable</param>
    ''' <remarks></remarks>
    Private Sub SetSprEditData(ByVal spr As LMSpread, ByVal dt As DataTable)

        With spr

            Dim lockFlg As Boolean = True
            Dim lock As Boolean = True

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum10e As StyleInfo = Me.StyleInfoNum10(spr, lockFlg)
            Dim sNum7 As StyleInfo = Me.StyleInfoNum7(spr, lockFlg)
            Dim sNum9d3 As StyleInfo = Me.StyleInfoNum9dec3(spr, lockFlg)
            Dim sNum5 As StyleInfo = Me.StyleInfoNum5(spr, lockFlg)
            Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr, lock)
            Dim sCom As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_Z001, False)
            Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, lockFlg)
            Dim max As Integer = dt.Rows.Count - 1
            Dim taxFlg As Boolean = True

            'タリフ分類区分によるロック制御
            Select Case dt.Rows(0).Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString()

                Case LMFControlC.TARIFF_YOKO

                    '横持ち
                    lockFlg = True
                    lock = False

                Case LMFControlC.TARIFF_INKA

                    '入荷着払い
                    lockFlg = True
                    lock = False

                Case Else

                    lockFlg = False
                    lock = False

            End Select

            '値設定
            For i As Integer = 0 To max

                '先頭が親レコード
                'それ以外は子レコードのためロック
                If 0 <> i Then

                    lockFlg = True
                    lock = True

                End If

                sNum9d3 = Me.StyleInfoNum9dec3(spr, lock)
                sNum5 = Me.StyleInfoNum5(spr, lock)
                sNum10e = Me.StyleInfoNum10(spr, lockFlg)
                sNum7 = Me.StyleInfoNum7(spr, lockFlg)
                sNum10 = Me.StyleInfoNum10(spr, lock)
                sTxt = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 100, lock)

                .SetCellStyle(i, LMF090G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNSO_TEHAI.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.DEST_JIS.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.CUST_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.SHIHARAITO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.SHIHARAI_NM.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.JURYO.ColNo, sNum9d3)
                .SetCellStyle(i, LMF090G.sprDetailDef.KYORI.ColNo, sNum5)
                .SetCellStyle(i, LMF090G.sprDetailDef.KOSU.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNCHIN.ColNo, sNum10)
                .SetCellStyle(i, LMF090G.sprDetailDef.TOSHI.ColNo, sNum10e)
                .SetCellStyle(i, LMF090G.sprDetailDef.TOKI.ColNo, sNum10e)
                .SetCellStyle(i, LMF090G.sprDetailDef.TYUKEI.ColNo, sNum10e)
                .SetCellStyle(i, LMF090G.sprDetailDef.KOSO.ColNo, sNum10e)
                .SetCellStyle(i, LMF090G.sprDetailDef.SONOTA.ColNo, sNum10e)
                If 0 <> i Then
                    .SetCellStyle(i, LMF090G.sprDetailDef.ZEI_KB.ColNo, sLabel)
                Else
                    .SetCellStyle(i, LMF090G.sprDetailDef.ZEI_KB.ColNo, sCom)
                End If
                .SetCellStyle(i, LMF090G.sprDetailDef.WARIMASHI.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.GROUP.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.GROUP_UNSO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.REMARK.ColNo, sTxt)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNSO_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.TRIP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.ONDO.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.NISUGATA.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.SHASHU.ColNo, sLabel)
                .SetCellStyle(i, LMF090G.sprDetailDef.KIKEN.ColNo, sLabel)

                'セルに値を設定
                Call Me.SetSprData(spr, dt.Rows(i), i, taxFlg)

            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドに値を設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="taxFlg">課税区分の設定内容を切り替える True:TAX_KB False:TAX_NM</param>
    ''' <remarks></remarks>
    Private Sub SetSprData(ByVal spr As LMSpread, ByVal dr As DataRow, ByVal rowNo As Integer, ByVal taxFlg As Boolean)

        With spr

            'セルに値を設定
            .SetCellValue(rowNo, LMF090G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(rowNo, LMF090G.sprDetailDef.UNSO_TEHAI.ColNo, dr.Item("SHIHARAI_TARIFF_BUNRUI_NM").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.DEST_JIS.ColNo, dr.Item("DEST_JIS_CD").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.DEST_NM.ColNo, dr.Item("DEST_NM").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.CUST_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE))
            .SetCellValue(rowNo, LMF090G.sprDetailDef.TARIFF_CD.ColNo, dr.Item("SHIHARAI_TARIFF_CD").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAITO_CD.ColNo, dr.Item("SHIHARAITO_CD").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAI_NM.ColNo, Me._LMFconG.EditConcatData(dr.Item("SHIHARAITO_NM").ToString(), dr.Item("SHIHARAITO_BUSYO_NM").ToString(), LMFControlC.ZENKAKU_SPACE))
            .SetCellValue(rowNo, LMF090G.sprDetailDef.JURYO.ColNo, dr.Item("DECI_WT").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.KYORI.ColNo, dr.Item("DECI_KYORI").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.KOSU.ColNo, dr.Item("DECI_NG_NB").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.UNCHIN.ColNo, dr.Item("DECI_UNCHIN").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.TOSHI.ColNo, dr.Item("DECI_CITY_EXTC").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.TOKI.ColNo, dr.Item("DECI_WINT_EXTC").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.TYUKEI.ColNo, dr.Item("DECI_RELY_EXTC").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.KOSO.ColNo, dr.Item("DECI_TOLL").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SONOTA.ColNo, dr.Item("DECI_INSU").ToString())
            If 0 <> rowNo OrElse taxFlg = False Then
                .SetCellValue(rowNo, LMF090G.sprDetailDef.ZEI_KB.ColNo, dr.Item("TAX_NM").ToString())
            Else
                .SetCellValue(rowNo, LMF090G.sprDetailDef.ZEI_KB.ColNo, dr.Item("TAX_KB").ToString())
            End If
            .SetCellValue(rowNo, LMF090G.sprDetailDef.WARIMASHI.ColNo, dr.Item("SHIHARAI_ETARIFF_CD").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.GROUP.ColNo, dr.Item("SHIHARAI_GROUP_NO").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.GROUP_UNSO.ColNo, dr.Item("SHIHARAI_GROUP_NO_M").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.UNSO_NO.ColNo, dr.Item("UNSO_NO_L").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.UNSO_NO_EDA.ColNo, dr.Item("UNSO_NO_M").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.TRIP_NO.ColNo, dr.Item("TRIP_NO").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.ONDO.ColNo, dr.Item("UNSO_ONDO_NM").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.NISUGATA.ColNo, dr.Item("SHIHARAI_PKG_UT_NM").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHASHU.ColNo, dr.Item("SHIHARAI_SYARYO_NM").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.KIKEN.ColNo, dr.Item("SHIHARAI_DANGER_NM").ToString())

            '隠し項目
            .SetCellValue(rowNo, LMF090G.sprDetailDef.TARIF_BUN.ColNo, dr.Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.PKG_KB.ColNo, dr.Item("SHIHARAI_PKG_UT_KB").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SYARYO_KB.ColNo, dr.Item("SHIHARAI_SYARYO_KB").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.DANGER_KB.ColNo, dr.Item("SHIHARAI_DANGER_KB").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.OUTKA_PLAN_DATE.ColNo, dr.Item("OUTKA_PLAN_DATE").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.ARR_PLAN_DATE.ColNo, dr.Item("ARR_PLAN_DATE").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAI_TARIFF_CD_LL.ColNo, dr.Item("SHIHARAI_TARIFF_CD_LL").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAI_ETARIFF_CD_LL.ColNo, dr.Item("SHIHARAI_ETARIFF_CD_LL").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAI_UNSO_WT_LL.ColNo, dr.Item("SHIHARAI_UNSO_WT_LL").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAI_COUNT_LL.ColNo, dr.Item("SHIHARAI_COUNT_LL").ToString())
            .SetCellValue(rowNo, LMF090G.sprDetailDef.SHIHARAI_UNCHIN_LL.ColNo, dr.Item("SHIHARAI_UNCHIN_LL").ToString())

        End With

    End Sub

    ''' <summary>
    ''' 入荷着払い
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub Nyuka()

        With Me._Frm

            '入荷着払いがレコードに存在している場合
            Dim tariffbunruiM As String = String.Empty

            Dim i As Integer = .sprDetail.ActiveSheet.RowCount - 1

            tariffbunruiM = Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF090G.sprDetailDef.TARIF_BUN.ColNo)).ToString()

            If LMFControlC.TARIFF_INKA.Equals(tariffbunruiM) = True Then
                Me.LockControl(True)
                Me.SetLockControl(.btnKeisan, True)
            End If

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
    ''' セルのプロパティを設定(英大数)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="length">桁数</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoTextEidaisu(ByVal spr As LMSpread, ByVal length As Integer) As StyleInfo

        Return LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA_U, length, False)

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
    ''' セルのプロパティを設定(Number 整数7桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum7(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, lock, 3, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, lock, 0, , ",")

    End Function

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbEigyo, lock)
            Me.SetLockControl(.imdShukka, lock)
            Me.SetLockControl(.lblUnsoCd, lock)
            Me.SetLockControl(.lblUnsoBrCd, lock)
            Me.SetLockControl(.lblUnsoNm, lock)
            Me.SetLockControl(.cmbTariffKbn, lock)
            Me.SetLockControl(.numKyori, lock)
            Me.SetLockControl(.txtTariffCd, lock)
            Me.SetLockControl(.lblTariffNm, lock)
            Me.SetLockControl(.cmbKiken, lock)
            Me.SetLockControl(.numJuryo, lock)
            Me.SetLockControl(.txtWarimashi, lock)
            Me.SetLockControl(.lblWarimashi, lock)
            Me.SetLockControl(.cmbShashu, lock)
            Me.SetLockControl(.numKosu, lock)
            Me.SetLockControl(.cmbNisugata, lock)
            Me.SetLockControl(.btnKeisan, lock)

        End With
    End Sub

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

#End Region

#End Region

#End Region

End Class
