' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH040G : EDI出荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMH040Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMH040G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH040F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH040V

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconH As LMHControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    ''' 検索条件保存データセット
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

    ''' <summary>
    ''' 保存DataRow
    ''' </summary>
    ''' <remarks></remarks>
    Private _Row As DataRow

    ''' <summary>
    ''' FreeC30行番号
    ''' </summary>
    ''' <remarks></remarks>
    Private _FreeC30Row As Integer

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH040F, ByVal g As LMHControlG, ByVal v As LMHControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm
        Me._Frm = frm
        Me._LMHconG = g
        Me._LMHconV = v

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

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = LMHControlC.FUNCTION_EDIT   '編集
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMHControlC.FUNCTION_POP   'マスタ参照
            .F11ButtonName = LMHControlC.FUNCTION_SAVE  '保　存
            .F12ButtonName = LMHControlC.FUNCTION_CLOSE '閉じる

            'ファンクションキーの制御
            .F1ButtonEnabled = False    '(F1)
            .F2ButtonEnabled = True     '(F2) 編集
            .F3ButtonEnabled = False    '(F3)
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = always  '(F10)マスタ参照
            .F11ButtonEnabled = always  '(F11)保存
            .F12ButtonEnabled = always  '(F12)閉じる

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

#Region "外部メソッド"

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '項目をクリア
        Call Me.ClearControl()

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

        '初期値設定
        Call Me.SetInitData()

        'ラジオボタンの初期値
        Call Me.OptChk()

        'ロック制御
        Call Me.EventLockControlL(False)
        Call Me.EventLockControlM(False)

    End Sub

    ''' <summary>
    ''' タリフ表示コントロール
    ''' </summary>
    ''' <remarks>タリフ分類区分により制御（横持ち：'40'）</remarks>
    Friend Sub SetTariffView()

        Dim lock As Boolean = True
        Dim unlock As Boolean = False

        With Me._Frm

            '参照の場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim tariffKbn As String = .cmbUnsoTehaiKB.SelectedValue.ToString.Trim()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffKbn) = True Then
                Exit Sub
            End If

            'ロック制御
            Call Me.UnsoLockControl()

            'タリフセットマスタから値取得
            Dim drs As DataRow() = Me.GetTariffSetDataRow(Me._Frm, tariffKbn)
                                        
            Dim tariffCd As String = String.Empty
            Dim extcCd As String = String.Empty

            If 0 < drs.Length Then

                Select Case tariffKbn

                    Case LMHControlC.TARIFF_YOKO  '横持ち

                        tariffCd = drs(0).Item("YOKO_TARIFF_CD").ToString()

                    Case LMHControlC.TARIFF_KONSAI  '混在

                        tariffCd = drs(0).Item("UNCHIN_TARIFF_CD1").ToString()
                        extcCd = drs(0).Item("EXTC_TARIFF_CD").ToString()

                    Case LMHControlC.TARIFF_KURUMA    '車扱い

                        tariffCd = drs(0).Item("UNCHIN_TARIFF_CD2").ToString()
                        extcCd = drs(0).Item("EXTC_TARIFF_CD").ToString()

                End Select

            End If

            'タリフコードの初期値設定
            .txtUntinTariffCD.TextValue = tariffCd
            .txtExtcTariff.TextValue = extcCd

            'タリフ表示制御
            Select Case tariffKbn
                Case LMHControlC.TARIFF_YOKO
                    'タリフ分類区分が横持ち
                    .lblTariff.TextValue = "横持ちタリフ"
                    Call Me.SetYokoTariffRem()
                Case Else
                    'タリフ分類区分が横持ち以外
                    .lblTariff.TextValue = "運賃タリフ"
                    Call Me.SetUnchinTariffRem()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' ロック制御（入数）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub IrisuLockControl()

        Dim lock As Boolean = True
        Dim unlock As Boolean = False

        With Me._Frm

            '運送時Mタブ固定対応 terakawa 2012.06.15 Start
            '運送データなら制御なし
            Dim freeC30 As String = Me._LMHconV.GetCellValue( _
                                    .sprFreeInputsL.ActiveSheet.Cells(Me._FreeC30Row, LMH040G.sprFreeLDef.INPUT.ColNo)).ToString()

            If Left(freeC30, 2).ToString.Equals(LMH040C.UNSO_DATA) Then
                Exit Sub
            End If
            '運送時Mタブ固定対応 terakawa 2012.06.15 End

            '参照モードなら制御なし
            If .lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
                Exit Sub
            End If

            If .optCnt.Checked = True Then
                .numOutkaPkgNB.ReadOnly = unlock
                .numOutkaHasu.ReadOnly = unlock
                '入数が１以下の場合、梱数をロック
                If Convert.ToInt32(.numPkgNB.TextValue.Trim()) <= 1 Then
                    .numOutkaPkgNB.ReadOnly = lock
                Else
                    .numOutkaPkgNB.ReadOnly = unlock
                End If
            Else
                .numOutkaPkgNB.ReadOnly = lock
                .numOutkaHasu.ReadOnly = lock
            End If

        End With

    End Sub

    ''' <summary>
    ''' ロック制御（出荷単位）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub OutkaLockControl()

        Dim lock As Boolean = True
        Dim unlock As Boolean = False

        With Me._Frm

            '運送時Mタブ固定対応 terakawa 2012.06.15 Start
            '運送データなら制御なし
            Dim freeC30 As String = Me._LMHconV.GetCellValue( _
                                    .sprFreeInputsL.ActiveSheet.Cells(Me._FreeC30Row, LMH040G.sprFreeLDef.INPUT.ColNo)).ToString()

            If Left(freeC30, 2).ToString.Equals(LMH040C.UNSO_DATA) Then
                Exit Sub
            End If
            '運送時Mタブ固定対応 terakawa 2012.06.15 End

            '参照モードなら制御なし
            If .lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
                Exit Sub
            End If

            '出荷単位チェックによるロック
            If .optCnt.Checked = True Then         '個数

                .numOutkaPkgNB.ReadOnly = unlock     '梱数
                .numOutkaHasu.ReadOnly = unlock      '端数
                .numOutkaTtlQT.ReadOnly = lock       '数量

                '入数チェック
                Call Me.IrisuLockControl()

            ElseIf .optAmt.Checked = True OrElse .optSample.Checked = True Then  '数量、サンプル
                .numOutkaPkgNB.ReadOnly = lock
                .numOutkaHasu.ReadOnly = lock
                .numOutkaTtlQT.ReadOnly = unlock

            End If

        End With

    End Sub

    ''' <summary>
    ''' ロック制御（運送情報）
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnsoLockControl()

        Dim lock As Boolean = True
        Dim unlock As Boolean = False

        With Me._Frm

            '参照モードなら制御なし
            If .lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
                Exit Sub
            End If

            Dim key As String = .cmbUnsoMotoKB.SelectedValue.ToString.Trim()
            Select Case key

                Case LMH040C.UNSO_MOTO_KB_NRS
                    Call Me.SetUnsoLock(unlock)

                Case LMH040C.UNSO_MOTO_KB_CUST, LMH040C.UNSO_MOTO_KB_UNFIXED
                    Call Me.SetUnsoLock(lock)
                    Exit Sub

            End Select

            Dim key2 As String = .cmbUnsoTehaiKB.SelectedValue.ToString()
            If .cmbUnsoMotoKB.SelectedValue.ToString.Equals(LMH040C.UNSO_MOTO_KB_NRS) Then
                Select Case key2

                    Case LMHControlC.TARIFF_KONSAI, LMHControlC.TARIFF_TOKUBIN
                        .cmbSharyoKB.ReadOnly = lock
                        .txtExtcTariff.ReadOnly = unlock

                    Case LMHControlC.TARIFF_KURUMA
                        .cmbSharyoKB.ReadOnly = unlock
                        .txtExtcTariff.ReadOnly = lock

                    Case LMHControlC.TARIFF_YOKO
                        .cmbSharyoKB.ReadOnly = lock
                        .txtExtcTariff.ReadOnly = lock

                End Select

            End If

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal sta As String, Optional ByVal lockFlg As String = LMH040C.MODE_DEFAULT, Optional ByVal ds As DataSet = Nothing)

        With Me._Frm

            Select Case lockFlg

                Case LMH040C.MODE_DEFAULT, LMH040C.MODE_REF  '初期、参照モード

                    .FunctionKey.F2ButtonEnabled = True     '編集
                    .FunctionKey.F10ButtonEnabled = False   'マスタ参照
                    .FunctionKey.F11ButtonEnabled = False   '保存
                    .btnDestSinki.Enabled = False           '新規（届先）
                    .btnRowReM.Enabled = False              '行復活（中）
                    .btnRowDelM.Enabled = False             '行削除（中）

                    Call Me.SetModeAndStatus(DispMode.VIEW, sta)

                    '編集項目制御
                    Call Me.EventLockControlL(True)
                    Call Me.EventLockControlM(True)

                Case LMH040C.MODE_EDIT       '編集モード

                    .FunctionKey.F2ButtonEnabled = False   '編集
                    .FunctionKey.F10ButtonEnabled = True   'マスタ参照
                    If Me.IsFFEM_MaterialPlantTransfer(ds) Then
                        ' FFEM原料プラント間転送の場合
                        ' 保存ボタンの無効化
                        .FunctionKey.F11ButtonEnabled = False
                    Else
                        .FunctionKey.F11ButtonEnabled = True   '保存
                    End If
                    .btnDestSinki.Enabled = True           '新規（届先）
                    .btnRowReM.Enabled = True              '行復活（中）
                    .btnRowDelM.Enabled = True             '行削除（中）

                    Call Me.SetModeAndStatus(DispMode.EDIT, sta)

                    '編集項目制御
                    Call Me.EventLockControlL(False)
                    Call Me.EventLockControlM(False)
                    Call Me.SetGooDsLock(True)
                    Call Me.UnsoLockControl()

            End Select

        End With

    End Sub

#End Region '外部メソッド

#Region "内部メソッド"

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d4 As Decimal = Convert.ToDecimal(LMHControlC.MAX_4)
            Dim sharp4 As String = "#,##0"

            Dim d5 As Decimal = Convert.ToDecimal(LMHControlC.MAX_5)
            Dim sharp5 As String = "##,##0"

            Dim d6dec3 As Decimal = Convert.ToDecimal(LMHControlC.MAX_6_3)
            Dim sharp6dec3 As String = "###,##0.000"

            Dim d9dec3 As Decimal = Convert.ToDecimal(LMHControlC.MAX_9_3)
            Dim sharp9dec3 As String = "###,###,##0.000"

            Dim d10 As Decimal = Convert.ToDecimal(LMHControlC.MAX_10)
            Dim sharp10 As String = "#,###,###,##0"

            '▼▼▼(マイナスデータ)
            Dim d9dec3M As Decimal = Convert.ToDecimal(String.Concat("-", LMHControlC.MAX_9_3))
            Dim d10M As Decimal = Convert.ToDecimal(String.Concat("-", LMHControlC.MAX_10))
            '▲▲▲(マイナスデータ)

            .numIrime.SetInputFields(sharp6dec3, , 6, 1, , 3, 3, , d6dec3, 0)      '6_3
            '▼▼▼(マイナスデータ)
            '.numOutkaHasu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)         '10_0
            '.numOutkaPkgNB.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)        '10_0
            '.numOutkaTtlNB.SetInputFields(sharp10, , 10, 1, , 0, , , d10, 0)        '10_0
            '.numOutkaTtlQT.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, 0) '9_3
            .numOutkaHasu.SetInputFields(sharp10, , 10, 1, , 0, , , d10, d10M)         '10_0
            .numOutkaPkgNB.SetInputFields(sharp10, , 10, 1, , 0, , , d10, d10M)        '10_0
            .numOutkaTtlNB.SetInputFields(sharp10, , 10, 1, , 0, , , d10, d10M)        '10_0
            .numOutkaTtlQT.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, d9dec3M) '9_3
            '▲▲▲(マイナスデータ)
            '要望番号1145 2012.06.12 Start
            .lblBetuWT.SetInputFields(sharp9dec3, , 9, 1, , 3, 3, , d9dec3, d9dec3M) '9_3
            '要望番号1145 2012.06.12 End

        End With

    End Sub

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .imdOutkoDate.TabIndex = LMH040C.CtlTabIndex.IMDOUTKODATE
            .imdOutkaPlanDate.TabIndex = LMH040C.CtlTabIndex.IMDOUTKAPLANDATE
            .imdArrPlanDate.TabIndex = LMH040C.CtlTabIndex.IMDARRPLANDATE
            .cmbArrPlanTime.TabIndex = LMH040C.CtlTabIndex.CMBARRPLANTIME
            .imdHokokuDate.TabIndex = LMH040C.CtlTabIndex.IMDHOKOKUDATE
            .cmbOutkaKB.TabIndex = LMH040C.CtlTabIndex.CMBOUTKAKB
            .cmbSyubetuKB.TabIndex = LMH040C.CtlTabIndex.CMBSYUBETUKB
            .cmbOutkaStateKB.TabIndex = LMH040C.CtlTabIndex.CMBOUTKASTATEKB
            .txtCustOrdNO.TabIndex = LMH040C.CtlTabIndex.TXTCUSTORDNO
            .txtBuyerOrdNO.TabIndex = LMH040C.CtlTabIndex.TXTBUYERORDNO
            .cmbPickKB.TabIndex = LMH040C.CtlTabIndex.CMBPICKKB
            .cmbOutkaHokokuYN.TabIndex = LMH040C.CtlTabIndex.CMBOUTKAHOKOKUYN
            .cmbToukiHokanYN.TabIndex = LMH040C.CtlTabIndex.CMBTOUKIHOKANYN
            .cmbNiyakuYN.TabIndex = LMH040C.CtlTabIndex.CMBNIYAKUYN
            .txtShipCDL.TabIndex = LMH040C.CtlTabIndex.TXTSHIPCDL
            .cmbDenpYN.TabIndex = LMH040C.CtlTabIndex.CMBDENPYN
            .btnDestSinki.TabIndex = LMH040C.CtlTabIndex.BTNDESTSINKI
            .txtDestCD.TabIndex = LMH040C.CtlTabIndex.TXTDESTCD
            .cmbSpNhsKB.TabIndex = LMH040C.CtlTabIndex.CMBSPNHSKB
            .cmbCoaYN.TabIndex = LMH040C.CtlTabIndex.CMBCOAYN
            .txtEDIDestCD.TabIndex = LMH040C.CtlTabIndex.TXTEDIDESTCD
            .txtDestZip.TabIndex = LMH040C.CtlTabIndex.TXTDESTZIP
            .txtDestJisCD.TabIndex = LMH040C.CtlTabIndex.TXTDESTJISCD
            .txtDestTel.TabIndex = LMH040C.CtlTabIndex.TXTDESTTEL
            .txtDestFax.TabIndex = LMH040C.CtlTabIndex.TXTDESTFAX
            .txtDestEmail.TabIndex = LMH040C.CtlTabIndex.TXTDESTEMAIL
            .txtDestAd3.TabIndex = LMH040C.CtlTabIndex.TXTDESTAD3
            .txtRemark.TabIndex = LMH040C.CtlTabIndex.TXTREMARK
            .txtUnsoAtt.TabIndex = LMH040C.CtlTabIndex.TXTUNSOATT
            .btnRowReM.TabIndex = LMH040C.CtlTabIndex.BTNROWREM
            .btnRowDelM.TabIndex = LMH040C.CtlTabIndex.BTNROWDELM
            .sprGoodsDef.TabIndex = LMH040C.CtlTabIndex.SPRGOODSDEF
            .txtCustGoodsCD.TabIndex = LMH040C.CtlTabIndex.TXTCUSTGOODSCD
            .txtCustOrdNoDtl.TabIndex = LMH040C.CtlTabIndex.TXTCUSTORDNODTL
            .txtRsvNO.TabIndex = LMH040C.CtlTabIndex.TXTRSVNO
            .txtSerialNO.TabIndex = LMH040C.CtlTabIndex.TXTSERIALNO
            .txtBuyerOrdNoDtl.TabIndex = LMH040C.CtlTabIndex.TXTBUYERORDNODTL
            .optTempY.TabIndex = LMH040C.CtlTabIndex.OPTTEMPY
            .optTempN.TabIndex = LMH040C.CtlTabIndex.OPTTEMPN
            .optCnt.TabIndex = LMH040C.CtlTabIndex.OPTCNT
            .optAmt.TabIndex = LMH040C.CtlTabIndex.OPTAMT
            .optSample.TabIndex = LMH040C.CtlTabIndex.OPTSAMPLE
            .numOutkaTtlNB.TabIndex = LMH040C.CtlTabIndex.NUMOUTKATTLNB
            .numOutkaPkgNB.TabIndex = LMH040C.CtlTabIndex.NUMOUTKAPKGNB
            .numOutkaHasu.TabIndex = LMH040C.CtlTabIndex.NUMOUTKAHASU
            .numOutkaTtlQT.TabIndex = LMH040C.CtlTabIndex.NUMOUTKATTLQT
            .txtGoodsRemark.TabIndex = LMH040C.CtlTabIndex.TXTGOODSREMARK
            .cmbUnsoMotoKB.TabIndex = LMH040C.CtlTabIndex.CMBUNSOMOTOKB
            .cmbUnsoTehaiKB.TabIndex = LMH040C.CtlTabIndex.CMBUNSOTEHAIKB
            .cmbSharyoKB.TabIndex = LMH040C.CtlTabIndex.CMBSHARYOKB
            .cmbBinKB.TabIndex = LMH040C.CtlTabIndex.CMBBINKB
            .cmbPcKB.TabIndex = LMH040C.CtlTabIndex.CMBPCKB
            .txtUnsoCD.TabIndex = LMH040C.CtlTabIndex.TXTUNSOCD
            .txtUnsoBrCD.TabIndex = LMH040C.CtlTabIndex.TXTUNSOBRCD
            .txtUntinTariffCD.TabIndex = LMH040C.CtlTabIndex.TXTUNTINTARIFFCD
            .txtExtcTariff.TabIndex = LMH040C.CtlTabIndex.TXTEXTCTARIFF
            .sprFreeInputsL.TabIndex = LMH040C.CtlTabIndex.SPRFREEINPUTSL
            .sprFreeInputsM.TabIndex = LMH040C.CtlTabIndex.SPRFREEINPUTSM

        End With

    End Sub

    ''' <summary>
    ''' 初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInitData()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' ラジオボタンの初期値
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OptChk()

        With Me._Frm

            .optCnt.Checked = True

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            'フォーカス位置初期化
            .Focus()
            .tabLarge.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            '.cmbEigyo.SelectedValue = Nothing
            .txtEDICtlNOChu.TextValue() = String.Empty
            .txtCustGoodsCD.TextValue() = String.Empty
            .lblNrsGoodsCD.TextValue() = String.Empty
            .lblGoodsNM.TextValue() = String.Empty
            .txtLotNo.TextValue() = String.Empty
            .txtCustOrdNoDtl.TextValue() = String.Empty
            .txtRsvNO.TextValue() = String.Empty
            .txtSerialNO.TextValue() = String.Empty
            .txtBuyerOrdNoDtl.TextValue() = String.Empty
            .optTempN.Checked = False
            .optTempY.Checked = False
            .numIrime.Value = Nothing
            .lblIrimeUtNm.TextValue() = String.Empty
            .lblBetuWT.TextValue() = String.Empty
            .cmbOndoKB.SelectedValue() = Nothing
            .cmbUnsoOndoKB.SelectedValue() = Nothing
            .optCnt.Checked = False
            .optAmt.Checked = False
            .optSample.Checked = False
            .numPkgNB.Value = Nothing
            .numOutkaTtlNB.Value = Nothing
            .numOutkaPkgNB.Value = Nothing
            .lblPkgUtNm.TextValue = String.Empty
            .numOutkaHasu.TextValue() = Nothing
            .lblKbUtNm.TextValue() = String.Empty
            .numOutkaTtlQT.Value = Nothing
            .lblQtUtNm.TextValue() = String.Empty
            .txtGoodsRemark.TextValue() = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 適用日取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetStrDate() As String

        Dim strdate As String = String.Empty

        With Me._Frm

            Dim custCdL As String = .txtCustCDL.TextValue.Trim()
            Dim custCdM As String = .txtCustCDM.TextValue.Trim()
            Dim custCdS As String = "00"
            Dim custCdSS As String = "00"

            '======= 商品マスタから荷主コード取得 TODO: 適用日取得仕様変更可能性あり==========
            'whereCust = "NRS_BR_CD = '" & .cmbEigyo.SelectedValue.ToString() & "' "
            'whereCust = whereCust & " AND GOODS_CD_NRS = '" & .lblNrsGoodsCD.TextValue & "' "
            'Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(whereCust)
            'If dr.Count() > 0 Then
            '    custCdL = dr(0).Item("CUST_CD_L").ToString()
            '    custCdM = dr(0).Item("CUST_CD_M").ToString()
            '    custCdS = dr(0).Item("CUST_CD_S").ToString()
            '    custCdSS = dr(0).Item("CUST_CD_SS").ToString()
            'End If

            Dim tmp As String = String.Empty

            'UNTIN_CALCULATION_KB取得
            '2016.02.18 要望番号2491 修正START
            'Dim dr As DataRow() = Me._LMHconG.SelectCustListDataRow(custCdL, custCdM, custCdS, custCdSS)
            Dim dr As DataRow() = Me._LMHconG.SelectCustListDataRow(Convert.ToString(.cmbEigyo.SelectedValue()), custCdL, custCdM, custCdS, custCdSS)
            '2016.02.18 要望番号2491 修正END

            If 0 < dr.Count() Then
                tmp = dr(0)("UNTIN_CALCULATION_KB").ToString()
            End If

            Select Case tmp
                Case LMHControlC.UNTIN_CALCULATION_KB_OUTKA
                    'OUTKA_PLAN_DATE
                    strdate = .imdOutkaPlanDate.TextValue()
                Case LMHControlC.UNTIN_CALCULATION_KB_ARR
                    'ARR_PLAN_DATE
                    strdate = .imdArrPlanDate.TextValue()
            End Select

        End With

        Return strdate

    End Function

    ''' <summary>
    ''' 運送タリフ名取得
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetUnchinTariffRem() As DataRow

        With Me._Frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString.Trim()
            Dim tariffCd As String = .txtUntinTariffCD.TextValue.Trim()
            Dim startDate As String = Me.GetStrDate()
            Dim dataTp As String = "00"

            '運送タリフ名取得
            Dim getDr As DataRow() = Me._LMHconG.SelectUnchinTariffListDataRow(tariffCd, "", startDate, dataTp)
            If 0 < getDr.Count() Then
                .lblUntinTariffREM.TextValue = getDr(0)("UNCHIN_TARIFF_REM").ToString()
                Return getDr(0)
            Else
                .lblUntinTariffREM.TextValue = String.Empty
            End If

        End With

        Return Nothing

    End Function

    ''' <summary>
    ''' タリフセットマスタから値取得
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetTariffSetDataRow(ByVal frm As LMH040F, ByVal tariffKbn As String) As DataRow()

        With frm

            'タリフセットマスタから値取得
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString.Trim()
            Dim custLCd As String = .txtCustCDL.TextValue.Trim()
            Dim custMCd As String = .txtCustCDM.TextValue.Trim()
            Dim destCd As String = .txtDestCD.TextValue.Trim()
            Dim setCd As String = String.Empty

            If String.IsNullOrEmpty(destCd) = True Then
                setCd = LMHControlC.SET_KB_CUST
            Else
                setCd = LMHControlC.SET_KB_DEST
            End If

            Dim drs As DataRow() = Me._LMHconG.SelectTariffSetListDataRow(brCd, custLCd, custMCd, tariffKbn, "", "", "", setCd, destCd)

            Return drs

        End With

    End Function

    ''' <summary>
    ''' 横持ちタリフ名取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetYokoTariffRem()

        With Me._Frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString.Trim()
            Dim tariffCd As String = .txtUntinTariffCD.TextValue.Trim()
            Dim yokoRem As String = String.Empty
            
            '横持ちタリフ名取得
            Dim getDr As DataRow() = Me._LMHconG.SelectYokoTariffListDataRow(brCd, tariffCd)
            If 0 < getDr.Count() Then
                yokoRem = getDr(0)("YOKO_REM").ToString()
            End If

            '横持ちタリフ名ラベルに名称セット
            Me._Frm.lblUntinTariffREM.TextValue = yokoRem

        End With

    End Sub

    ''' <summary>
    ''' 商品情報セット
    ''' </summary>
    ''' <remarks>商品コードテキストボックスからFocusが外された時</remarks>
    Friend Sub SetGoodsData()

        With Me._Frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString.Trim()
            Dim goodsCd As String = .lblNrsGoodsCD.TextValue.Trim()
            Dim goodsKey As String = .txtCustGoodsCD.TextValue.Trim()
            Dim custCdL As String = .txtCustCDL.TextValue.Trim()
            Dim custCdM As String = .txtCustCDM.TextValue.Trim()

            Dim dr As DataRow() = Me._LMHconG.SelectGoodsListDataRow(brCd, goodsKey, goodsCd, custCdL, custCdM)
            If dr.Length < 1 Then
                .lblNrsGoodsCD.TextValue = String.Empty
                .lblGoodsNM.TextValue = String.Empty
                Exit Sub
            End If

            Call Me.SetGoodsDetailData(dr(0))

        End With

    End Sub

    ''' <summary>
    ''' 商品関連詳細データセット
    ''' </summary>
    ''' <param name="dr"></param>
    ''' <remarks></remarks>
    Friend Sub SetGoodsDetailData(ByVal dr As DataRow)

        With Me._Frm

            .txtCustGoodsCD.TextValue = dr.Item("GOODS_CD_CUST").ToString()                               '商品コード
            .lblGoodsNM.TextValue = dr.Item("GOODS_NM_1").ToString()                                      '商品名
            .lblNrsGoodsCD.TextValue = dr.Item("GOODS_CD_NRS").ToString()                                 '商品KEY
            .numIrime.Value = dr.Item("STD_IRIME_NB")                                                     '入目
            .lblIrimeUt.TextValue = dr.Item("STD_IRIME_UT").ToString()                                    '入目単位（コード）
            .lblIrimeUtNm.TextValue = dr.Item("STD_IRIME_UT").ToString()                                  '入目単位（名称）
            .lblBetuWT.Value = dr.Item("STD_WT_KGS").ToString()                                           '重量
            .cmbOndoKB.SelectedValue = dr.Item("ONDO_KB").ToString()                                      '保管温度
            .cmbUnsoOndoKB.SelectedValue = dr.Item("UNSO_ONDO_KB").ToString()                             '運送温度
            .numPkgNB.Value = dr.Item("PKG_NB")                                                           '入数
            .lblPkgUt.TextValue = dr.Item("PKG_UT").ToString()                                            '梱数単位（コード）
            .lblPkgUtNm.TextValue = dr.Item("PKG_UT").ToString()                                          '梱数単位（名称）
            .lblKbUt.TextValue = dr.Item("NB_UT").ToString()                                              '端数単位（コード）
            .lblKbUtNm.TextValue = dr.Item("NB_UT").ToString()                                            '端数単位（名称）
            .lblQtUt.TextValue = dr.Item("STD_IRIME_UT").ToString()                                       '数量単位（コード）
            .lblQtUtNm.TextValue = dr.Item("STD_IRIME_UT").ToString()                                     '数量単位（名称）

            '入数が１以下の場合、梱数を0にする、端数に個数を入れる。
            If Convert.ToInt32(.numPkgNB.TextValue) <= 1 Then
                .numOutkaHasu.TextValue = .numOutkaTtlNB.TextValue
                .numOutkaPkgNB.TextValue = "0"
            End If

        End With

    End Sub

    ''' <summary>
    ''' 運送会社名セット
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetUnsoNm()

        Dim unsoNM As String = String.Empty
        Dim unsoBrNM As String = String.Empty

        With Me._Frm

            Dim unsoCd As String = .txtUnsoCD.TextValue.Trim()
            Dim unsoBrCd As String = .txtUnsoBrCD.TextValue.Trim()
            Dim dr As DataRow() = Me._LMHconG.SelectUnsocoListDataRow(unsoCd, unsoBrCd)

            If 0 < dr.Length Then
                unsoNM = dr(0).Item("UNSOCO_NM").ToString
                unsoBrNM = dr(0).Item("UNSOCO_BR_NM").ToString
            End If

            '名称セット
            .lblUnsoNM.TextValue = Me._LMHconG.EditConcatData(unsoNM, unsoBrNM, Space(1))

            'データセットに名称設定
            Call Me.SetDataSetUnsoNm(unsoNM, unsoBrNM)

        End With

    End Sub

    ''' <summary>
    ''' モードによる編集項目ロック制御（出荷L）
    ''' </summary>
    ''' <param name="lock"></param>
    ''' <remarks>lock = True：参照モード lock = False：編集モード</remarks>
    Friend Sub EventLockControlL(ByVal lock As Boolean)

        With _Frm

            .imdOutkoDate.ReadOnly = lock
            .imdOutkaPlanDate.ReadOnly = lock
            .imdArrPlanDate.ReadOnly = lock
            .cmbArrPlanTime.ReadOnly = lock
            .imdHokokuDate.ReadOnly = lock
            .cmbOutkaKB.ReadOnly = lock
            .cmbSyubetuKB.ReadOnly = lock
            .cmbOutkaStateKB.ReadOnly = lock
            .txtCustOrdNO.ReadOnly = lock
            .txtBuyerOrdNO.ReadOnly = lock
            .cmbPickKB.ReadOnly = lock
            .cmbOutkaHokokuYN.ReadOnly = lock
            .cmbToukiHokanYN.ReadOnly = lock
            .cmbNiyakuYN.ReadOnly = lock
            .txtShipCDL.ReadOnly = lock
            .cmbDenpYN.ReadOnly = lock
            .txtDestCD.ReadOnly = lock
            .cmbSpNhsKB.ReadOnly = lock
            .cmbCoaYN.ReadOnly = lock
            .txtEDIDestCD.ReadOnly = lock
            .txtDestZip.ReadOnly = lock
            .txtDestJisCD.ReadOnly = lock
            .txtDestTel.ReadOnly = lock
            .txtDestFax.ReadOnly = lock
            .txtDestEmail.ReadOnly = lock
            .txtDestAd3.ReadOnly = lock
            .txtRemark.ReadOnly = lock
            .txtUnsoAtt.ReadOnly = lock
            '▼▼▼(ソート対応)
            '.sprGoodsDef.Locked = Not lock
            '▲▲▲(ソート対応)
            .sprFreeInputsL.Locked = Not lock
            .sprFreeInputsM.Locked = Not lock

        End With

    End Sub

    ''' <summary>
    ''' モードによる編集項目ロック制御（出荷M）
    ''' </summary>
    ''' <param name="lock"></param>
    ''' <remarks>lock = True：参照モード lock = False：編集モード</remarks>
    Friend Sub EventLockControlM(ByVal lock As Boolean)

        With _Frm

            '運送時Mタブ固定対応 terakawa 2012.06.15 Start
            '運送データかつ編集モードの場合、読み取り専用
            Dim freeC30 As String = Me._LMHconV.GetCellValue( _
                                    .sprFreeInputsL.ActiveSheet.Cells(Me._FreeC30Row, LMH040G.sprFreeLDef.INPUT.ColNo)).ToString()

            If Left(freeC30, 2).ToString.Equals(LMH040C.UNSO_DATA) AndAlso lock = False Then
                Exit Sub
            End If
            '運送時Mタブ固定対応 terakawa 2012.06.15 End

            .txtCustGoodsCD.ReadOnly = lock
            '2012.03.13 大阪対応START
            .txtLotNo.ReadOnly = lock
            '2012.03.13 大阪対応END
            .txtCustOrdNoDtl.ReadOnly = lock
            .txtRsvNO.ReadOnly = lock
            .txtSerialNO.ReadOnly = lock
            .txtBuyerOrdNoDtl.ReadOnly = lock
            .numIrime.ReadOnly = lock
            .numOutkaPkgNB.ReadOnly = lock
            .numOutkaHasu.ReadOnly = lock
            .numOutkaTtlQT.ReadOnly = lock
            .txtGoodsRemark.ReadOnly = lock
            .cmbUnsoMotoKB.ReadOnly = lock
            .cmbUnsoTehaiKB.ReadOnly = lock
            .cmbSharyoKB.ReadOnly = lock
            .cmbBinKB.ReadOnly = lock
            .cmbPcKB.ReadOnly = lock
            .txtUnsoCD.ReadOnly = lock
            .txtUnsoBrCD.ReadOnly = lock
            .txtUntinTariffCD.ReadOnly = lock
            .txtExtcTariff.ReadOnly = lock

            .btnDestSinki.Enabled = Not lock
            .btnRowReM.Enabled = Not lock
            .btnRowDelM.Enabled = Not lock

            .optTempY.Enabled = Not lock
            .optTempN.Enabled = Not lock
            .optCnt.Enabled = Not lock
            .optAmt.Enabled = Not lock
            .optSample.Enabled = Not lock

            .sprFreeInputsM.Locked = Not lock

        End With

    End Sub

    ''' <summary>
    ''' 商品別情報項目ロック設定
    ''' </summary>
    ''' <param name="lockflg"></param>
    ''' <remarks></remarks>
    Friend Sub SetGooDsLock(ByVal lockflg As Boolean)

        With Me._Frm

            '運送時Mタブ固定対応 terakawa 2012.06.15 Start
            '運送データかつ編集モードの場合、読み取り専用
            Dim freeC30 As String = Me._LMHconV.GetCellValue( _
                                    .sprFreeInputsL.ActiveSheet.Cells(Me._FreeC30Row, LMH040G.sprFreeLDef.INPUT.ColNo)).ToString()

            '運送データかつ編集モードの場合、読み取り専用を解除する荷主 ADD 2017/05/30
            Dim cust_cd_lm As String = String.Concat(Me._Frm.txtCustCDL.TextValue.Trim, Me._Frm.txtCustCDM.TextValue.Trim)

            If Left(freeC30, 2).ToString.Equals(LMH040C.UNSO_DATA) AndAlso lockflg = False Then
                If cust_cd_lm.ToString.Equals(LMH040C.UNSO_CUST_CD_LM) = False AndAlso lockflg = False Then
                    Exit Sub
                End If
            End If
            '運送時Mタブ固定対応 terakawa 2012.06.15 End

            .txtCustGoodsCD.ReadOnly = lockflg
            '2012.03.13 大阪対応START
            .txtLotNo.ReadOnly = lockflg
            '2012.03.13 大阪対応END
            .txtCustOrdNoDtl.ReadOnly = lockflg
            .txtRsvNO.ReadOnly = lockflg
            .txtSerialNO.ReadOnly = lockflg
            .txtBuyerOrdNoDtl.ReadOnly = lockflg
            .numOutkaPkgNB.ReadOnly = lockflg
            .numOutkaHasu.ReadOnly = lockflg
            .numOutkaTtlQT.ReadOnly = lockflg
            .txtGoodsRemark.ReadOnly = lockflg
            .optTempY.Enabled = Not lockflg
            .optTempN.Enabled = Not lockflg
            .optCnt.Enabled = Not lockflg
            .optAmt.Enabled = Not lockflg
            .optSample.Enabled = Not lockflg
        End With

    End Sub

    ''' <summary>
    ''' 運送情報項目ロック設定
    ''' </summary>
    ''' <param name="lockflg"></param>
    ''' <remarks></remarks>
    Friend Sub SetUnsoLock(ByVal lockflg As Boolean)

        With Me._Frm
            .cmbUnsoTehaiKB.ReadOnly = lockflg
            .cmbSharyoKB.ReadOnly = lockflg
            .cmbBinKB.ReadOnly = lockflg
            '2012.04.25 要望番号976　修正START
            '.cmbPcKB.ReadOnly = lockflg
            '.txtUnsoCD.ReadOnly = lockflg
            '.txtUnsoBrCD.ReadOnly = lockflg
            '↓下記３項目は手配区分が変更になってもロック制御をしない
            .cmbPcKB.ReadOnly = False
            .txtUnsoCD.ReadOnly = False
            .txtUnsoBrCD.ReadOnly = False
            '2012.04.25 要望番号976　修正END
            .txtUntinTariffCD.ReadOnly = lockflg
            .txtExtcTariff.ReadOnly = lockflg
        End With

    End Sub

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

    ''' <summary>
    ''' 計算処理（個数、数量）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetCalData() As Boolean

        With Me._Frm

            Dim hasu As Decimal = Convert.ToDecimal(.numOutkaHasu.Value)
            Dim konsu As Decimal = Convert.ToDecimal(.numOutkaPkgNB.Value)
            Dim irisu As Decimal = Convert.ToDecimal(.numPkgNB.Value)
            Dim irime As Decimal = Convert.ToDecimal(.numIrime.Value)
            Dim suryo As Decimal = Convert.ToDecimal(.numOutkaTtlQT.Value)
            Dim temp As Decimal = 0

            '○個数再計算処理
            '梱数 * 入数 + 端数 → 個数にセット
            temp = konsu * irisu + hasu
            '▼▼▼要望番号:466
            'If Me._LMHconV.IsBoundsChk(temp, 9999999999, 0, "出荷個数") = False Then
            If Me._LMHconV.IsBoundsChk(temp, LMHControlC.MAX_10, LMHControlC.MIN_0, "個数") = False Then
                '▲▲▲要望番号:466
                Return False
            End If
            .numOutkaTtlNB.Value = temp
            'LMHControlC.MAX_10
            '○数量再計算処理
            '個数 * 入目　→　数量にセット
            Dim kosu As Decimal = Convert.ToDecimal(.numOutkaTtlNB.Value)

            temp = kosu * irime
            '▼▼▼要望番号:466
            'If Me._LMHconV.IsBoundsChk(temp, 999999999.999, 0, "出荷数量") = False Then
            If Me._LMHconV.IsBoundsChk(temp, LMHControlC.MAX_9_3, LMHControlC.MIN_0, "数量") = False Then
                '▲▲▲要望番号:466
                Return False
            End If
            .numOutkaTtlQT.Value = temp
        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷単位数量時の再計算処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetCalDataQT() As Boolean

        With Me._Frm

            Dim hasu As Decimal = Convert.ToDecimal(.numOutkaHasu.Value)
            Dim konsu As Decimal = Convert.ToDecimal(.numOutkaPkgNB.Value)
            Dim irisu As Decimal = Convert.ToDecimal(.numPkgNB.Value)
            Dim irime As Decimal = Convert.ToDecimal(.numIrime.Value)
            Dim suryo As Decimal = Convert.ToDecimal(.numOutkaTtlQT.Value)

            .numOutkaHasu.Value = suryo / irime
            .numOutkaTtlNB.Value = .numOutkaHasu.Value

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷単位変更時の計算処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetCalDataOptChange() As Boolean

        With Me._Frm

            Dim hasu As Decimal = Convert.ToDecimal(.numOutkaHasu.Value)
            Dim konsu As Decimal = Convert.ToDecimal(.numOutkaPkgNB.Value)
            Dim irisu As Decimal = Convert.ToDecimal(.numPkgNB.Value)

            '数量・サンプルが選択された場合、梱数を０にして端数に足す。
            If .optAmt.Checked = True OrElse .optSample.Checked = True Then
                .numOutkaHasu.Value = hasu + (konsu * irisu)
                .numOutkaPkgNB.Value = 0
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' データセットに運送会社名セット
    ''' </summary>
    ''' <param name="unsoNm"></param>
    ''' <param name="unsoBrNm"></param>
    ''' <remarks></remarks>
    Friend Sub SetDataSetUnsoNm(ByVal unsoNm As String, ByVal unsoBrNm As String)
        'データセットに直接名称値を入力
        Me._Ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_NM") = unsoNm
        Me._Ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_BR_NM") = unsoBrNm

    End Sub

    ''' <summary>
    ''' 単位名称取得
    ''' </summary>
    ''' <param name="gCd">KBN_GROUP_CD</param>
    ''' <param name="kbCd">KBN_CD</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetKbnNm(ByVal gCd As String, ByVal kbCd As String) As String

        Dim getDr As DataRow() = Me._LMHconG.SelectKbnListDataRow(kbCd, gCd)
        If 0 < getDr.Count Then
            Return getDr(0)("KBN_NM1").ToString()
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' FFEM原料プラント間転送か否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Friend Function IsFFEM_MaterialPlantTransfer(ByVal ds As DataSet) As Boolean

        If (Not (ds Is Nothing)) AndAlso
            ds.Tables.Contains(LMH040C.TABLE_NM_INOUTKAEDI_HED_FJF) AndAlso ds.Tables(LMH040C.TABLE_NM_INOUTKAEDI_HED_FJF).Rows.Count() > 0 AndAlso
            ds.Tables.Contains(LMH040C.TABLE_NM_OUT_M) AndAlso ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows.Count() > 0 AndAlso
            ds.Tables(LMH040C.TABLE_NM_INOUTKAEDI_HED_FJF).Rows(0).Item("ZFVYHKKBN").ToString() = "2" AndAlso
            ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(0).Item("CUST_GOODS_CD").ToString().StartsWith("243") AndAlso
            ds.Tables(LMH040C.TABLE_NM_INOUTKAEDI_HED_FJF).Rows(0).Item("ZFVYDENTYP").ToString() = "ZUB1" Then
            ' 引当計上予実区分(ZFVYHKKBN) = '2'(出荷予定) かつ
            ' 品目コード(CUST_GOODS_CD[設定元元:MATNR]) の左 3桁が '243'(原料) かつ
            ' 伝票タイプ区分(ZFVYDENTYP) = 'ZUB1'(在庫転送オーダー) の場合
            ' FFEM原料プラント間転送である
            Return True
        End If

        Return False

    End Function

#End Region

#End Region

#Region "データ表示＆格納"

    ''' <summary>
    ''' 検索結果ヘッダー部表示（出荷データ（大））
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHeaderData(ByVal ds As DataSet)

        With Me._Frm
            Me._Row = ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)

            '出荷情報
            .lblEDICtlNO.TextValue = Me._Row("EDI_CTL_NO").ToString()
            .cmbEDIStateKB.SelectedValue = Me._Row("EDI_STATE_KB").ToString()
            .lblOutkaCtlNO.TextValue = Me._Row("OUTKA_CTL_NO").ToString()
            .cmbEigyo.SelectedValue = Me._Row("NRS_BR_CD").ToString()
            .cmbSoko.SelectedValue = Me._Row("WH_CD").ToString()
            .imdOutkoDate.TextValue = Me._Row("OUTKO_DATE").ToString()
            .imdOutkaPlanDate.TextValue = Me._Row("OUTKA_PLAN_DATE").ToString()
            .imdArrPlanDate.TextValue = Me._Row("ARR_PLAN_DATE").ToString()
            .cmbArrPlanTime.SelectedValue = Me._Row("ARR_PLAN_TIME").ToString()
            .imdHokokuDate.TextValue = Me._Row("HOKOKU_DATE").ToString()
            .cmbOutkaKB.SelectedValue = Me._Row("OUTKA_KB").ToString()
            .cmbSyubetuKB.SelectedValue = Me._Row("SYUBETU_KB").ToString()
            .cmbOutkaStateKB.SelectedValue = Me._Row("OUTKA_STATE_KB").ToString()
            .txtCustOrdNO.TextValue = Me._Row("CUST_ORD_NO").ToString()
            .txtBuyerOrdNO.TextValue = Me._Row("BUYER_ORD_NO").ToString()
            .cmbPickKB.SelectedValue = Me._Row("PICK_KB").ToString()
            .cmbOutkaHokokuYN.SelectedValue = String.Concat("0", Me._Row("OUTKAHOKOKU_YN").ToString())
            .cmbToukiHokanYN.SelectedValue = String.Concat("0", Me._Row("TOUKI_HOKAN_YN").ToString())
            .txtCustCDL.TextValue = Me._Row("CUST_CD_L").ToString()
            .txtCustCDM.TextValue = Me._Row("CUST_CD_M").ToString()
            .lblCustNM.TextValue = String.Concat(Me._Row("CUST_NM_L").ToString(), " ", Me._Row("CUST_NM_M").ToString())
            .cmbNiyakuYN.SelectedValue = String.Concat("0", Me._Row("NIYAKU_YN").ToString())
            .txtShipCDL.TextValue = Me._Row("SHIP_CD_L").ToString()
            .lblShipNM.TextValue = String.Concat(Me._Row("SHIP_NM_L").ToString(), " ", Me._Row("SHIP_NM_M").ToString())
            .cmbDenpYN.SelectedValue = String.Concat("0", Me._Row("DENP_YN").ToString())
            .txtDestCD.TextValue = Me._Row("DEST_CD").ToString()
            .lblDestNM.TextValue = Me._Row("DEST_NM").ToString()
            .cmbSpNhsKB.SelectedValue = Me._Row("SP_NHS_KB").ToString()
            .cmbCoaYN.SelectedValue = String.Concat("0", Me._Row("COA_YN").ToString())
            .txtDestAd1.TextValue = Me._Row("DEST_AD_1").ToString()
            .txtDestAd2.TextValue = Me._Row("DEST_AD_2").ToString()
            .txtDestAd3.TextValue = Me._Row("DEST_AD_3").ToString()
            .txtDestAd4.TextValue = Me._Row("DEST_AD_4").ToString()
            .txtDestAd5.TextValue = Me._Row("DEST_AD_5").ToString()
            .txtEDIDestCD.TextValue = Me._Row("EDI_DEST_CD").ToString()
            .txtDestZip.TextValue = Me._Row("DEST_ZIP").ToString()
            .txtDestJisCD.TextValue = Me._Row("DEST_JIS_CD").ToString()
            .txtDestTel.TextValue = Me._Row("DEST_TEL").ToString()
            .txtDestFax.TextValue = Me._Row("DEST_FAX").ToString()
            .txtDestEmail.TextValue = Me._Row("DEST_MAIL").ToString()
            .txtRemark.TextValue = Me._Row("REMARK").ToString()
            .txtUnsoAtt.TextValue = Me._Row("UNSO_ATT").ToString()

            'その他、手配情報
            .cmbUnsoMotoKB.SelectedValue = Me._Row("UNSO_MOTO_KB").ToString()
            .cmbUnsoTehaiKB.SelectedValue = Me._Row("UNSO_TEHAI_KB").ToString()
            .cmbSharyoKB.SelectedValue = Me._Row("SYARYO_KB").ToString()
            .cmbBinKB.SelectedValue = Me._Row("BIN_KB").ToString()
            .cmbPcKB.SelectedValue = Me._Row("PC_KB").ToString()
            .txtUnsoCD.TextValue = Me._Row("UNSO_CD").ToString()
            .txtUnsoBrCD.TextValue = Me._Row("UNSO_BR_CD").ToString()
            .lblUnsoNM.TextValue = String.Concat(Me._Row("UNSO_NM").ToString(), " ", Me._Row("UNSO_BR_NM").ToString())
            .txtUntinTariffCD.TextValue = Me._Row("UNCHIN_TARIFF_CD").ToString()
            .txtExtcTariff.TextValue = Me._Row("EXTC_TARIFF_CD").ToString()

            'タリフ表示
            Call Me.SetTariffView()

        End With

    End Sub

    ''' <summary>
    ''' 中スプレッドダブルクリック時、商品別データ表示（出荷データ（中））
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetDetailData(ByVal ds As DataSet)

        With Me._Frm
            '▼▼▼(ソート)
            'Dim rownum As Integer = .sprGoodsDef.ActiveSheet.ActiveRowIndex()
            'Me._Row = ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)
            Dim rowNoM As String = GetSelectRowMNo()
            Me._Row = GetOutkaMDataRow(ds, rowNoM)
            '▲▲▲(ソート)

            .txtEDICtlNOChu.TextValue = Me._Row("EDI_CTL_NO_CHU").ToString()
            .txtCustGoodsCD.TextValue = Me._Row("CUST_GOODS_CD").ToString()
            .lblGoodsNM.TextValue = Me._Row("GOODS_NM").ToString()
            .txtLotNo.TextValue = Me._Row("LOT_NO").ToString()
            .txtCustOrdNoDtl.TextValue = Me._Row("CUST_ORD_NO_DTL").ToString()
            .txtRsvNO.TextValue = Me._Row("RSV_NO").ToString()
            .txtSerialNO.TextValue = Me._Row("SERIAL_NO").ToString()
            .txtBuyerOrdNoDtl.TextValue = Me._Row("BUYER_ORD_NO_DTL").ToString()

            '分析表添付
            Select Case Me._Row("COA_YN").ToString()
                Case LMH040C.COA_N   'なし
                    .optTempN.Checked = True
                Case LMH040C.COA_Y   'あり
                    .optTempY.Checked = True
            End Select

            .numIrime.Value = Me._Row("IRIME")
            '.lblIrimeUtNm.TextValue = Me._Row("IRIME_UT_NM").ToString()
            .lblIrimeUtNm.TextValue = Me._Row("IRIME_UT").ToString()
            '要望対応1145 2012.06.12 Start
            '.lblBetuWT.TextValue = Me._Row("BETU_WT").ToString()
            .lblBetuWT.Value = Me._Row("BETU_WT")
            '要望対応1145 2012.06.12 End


            .cmbOndoKB.SelectedValue = Me._Row("ONDO_KB").ToString()
            .cmbUnsoOndoKB.SelectedValue = Me._Row("UNSO_ONDO_KB").ToString()

            '出荷単位
            Select Case Me._Row("ALCTD_KB").ToString()
                Case LMH040C.ALCTD_KB_KOSU    '個数
                    .optCnt.Checked = True
                Case LMH040C.ALCTD_KB_SURYO   '数量
                    .optAmt.Checked = True
                Case LMH040C.ALCTD_KB_SAMPLE  'サンプル
                    .optSample.Checked = True
            End Select

            .numPkgNB.Value = Me._Row("PKG_NB")
            .numOutkaTtlNB.Value = Me._Row("OUTKA_TTL_NB")

            '入数が0の場合は梱数に0を設定、入数が1の場合は端数に梱数を足し梱数を0に設定する。
            '.numOutkaPkgNB.Value = Me._Row("OUTKA_PKG_NB")

            If Convert.ToInt64(.numPkgNB.Value) = 0 Then
                '2013.10.11 要望番号2113 修正START
                If ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0).Item("FREE_C30").ToString().Length > 2 AndAlso _
                   ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0).Item("FREE_C30").ToString().Substring(0, 2) = "01" Then
                    .numOutkaPkgNB.Value = 0
                    .numPkgNB.Value = 1
                    .numOutkaHasu.Value = Convert.ToInt64(Me._Row("OUTKA_HASU")) + Convert.ToInt64(Me._Row("OUTKA_PKG_NB"))
                Else
                    ''千葉アクタス対応（NRS商品CDが取込時空になる）
                    'Dim whereStr As String = String.Empty
                    'whereStr = "NRS_BR_CD = '"
                    'whereStr = whereStr & _Frm.cmbEigyo.SelectedValue.ToString & "'"
                    'whereStr = whereStr & " AND CUST_CD = '" & String.Concat(_Frm.txtCustCDL.TextValue.ToString ) & "'"
                    'whereStr = whereStr & " AND SUB_KB = '80' "

                    'Dim custdtlDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(whereStr)

                    'アクタスの場合、そのまま掘り込む
                    'If custdtlDr.Length > 0 Then
                    .numOutkaPkgNB.Value = 0
                    .numOutkaHasu.Value = Me._Row("OUTKA_PKG_NB")

                    'Else

                    .numOutkaPkgNB.Value = 0
                    .numOutkaHasu.Value = Me._Row("OUTKA_HASU")

                    'End If

                End If
            '2013.10.11 要望番号2113 修正END
            ElseIf Convert.ToInt64(.numPkgNB.Value) = 1 Then
            .numOutkaPkgNB.Value = 0
            .numOutkaHasu.Value = Convert.ToInt64(Me._Row("OUTKA_HASU")) + Convert.ToInt64(Me._Row("OUTKA_PKG_NB"))
            Else
            .numOutkaPkgNB.Value = Me._Row("OUTKA_PKG_NB")
            .numOutkaHasu.Value = Me._Row("OUTKA_HASU")
            End If

            '.lblPkgUtNm.TextValue = Me._Row("PKG_UT_NM").ToString()
            .lblPkgUtNm.TextValue = Me._Row("PKG_UT").ToString()

            '.lblKbUtNm.TextValue = Me._Row("KB_UT_NM").ToString()
            .lblKbUtNm.TextValue = Me._Row("KB_UT").ToString()
            .numOutkaTtlQT.Value = Me._Row("OUTKA_TTL_QT")
            '.lblQtUtNm.TextValue = Me._Row("QT_UT_NM").ToString()
            .lblQtUtNm.TextValue = Me._Row("QT_UT").ToString()
            .txtGoodsRemark.TextValue = Me._Row("REMARK").ToString()

            '隠し項目
            .lblNrsGoodsCD.TextValue = Me._Row("NRS_GOODS_CD").ToString()
            .lblIrimeUt.TextValue = Me._Row("IRIME_UT").ToString()
            .lblPkgUt.TextValue = Me._Row("PKG_UT").ToString()
            .lblKbUt.TextValue = Me._Row("KB_UT").ToString()
            .lblQtUt.TextValue = Me._Row("QT_UT").ToString()

            '自由項目再設定
            Call Me.SetFreeMSpread(ds)

        End With

    End Sub

    ''' <summary>
    ''' 編集中のEDI出荷(中)データをデータセットに格納・スプレッド出力(自由項目以外)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="rownum">編集中データの行番号</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SaveEditData(ByVal ds As DataSet, ByVal rownum As Integer) As DataSet

        With Me._Frm

            '▼▼▼(ソート)
            Dim ediNoM As String = Me._Frm.txtEDICtlNOChu.TextValue
            Dim dsRow As DataRow = Me.GetOutkaMDataRow(ds, ediNoM)
            Dim sprRow As Integer = Me.GetSprRowM
            '▲▲▲(ソート)

            '▼▼▼(ソート)
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("EDI_CTL_NO_CHU") = .txtEDICtlNOChu.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("CUST_GOODS_CD") = .txtCustGoodsCD.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("GOODS_NM") = .lblGoodsNM.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("LOT_NO") = .txtLotNo.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("CUST_ORD_NO_DTL") = .txtCustOrdNoDtl.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("RSV_NO") = .txtRsvNO.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("SERIAL_NO") = .txtSerialNO.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("BUYER_ORD_NO_DTL") = .txtBuyerOrdNoDtl.TextValue()

            dsRow("EDI_CTL_NO_CHU") = .txtEDICtlNOChu.TextValue()
            dsRow("CUST_GOODS_CD") = .txtCustGoodsCD.TextValue()
            dsRow("GOODS_NM") = .lblGoodsNM.TextValue()
            dsRow("LOT_NO") = .txtLotNo.TextValue()
            dsRow("CUST_ORD_NO_DTL") = .txtCustOrdNoDtl.TextValue()
            dsRow("RSV_NO") = .txtRsvNO.TextValue()
            dsRow("SERIAL_NO") = .txtSerialNO.TextValue()
            dsRow("BUYER_ORD_NO_DTL") = .txtBuyerOrdNoDtl.TextValue()
            '▲▲▲(ソート)

            '▼▼▼(ソート)
            '分析表添付
            'If .optTempN.Checked = True Then
            '    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("COA_YN") = LMH040C.COA_N
            'ElseIf .optTempY.Checked = True Then
            '    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("COA_YN") = LMH040C.COA_Y
            'End If

            If .optTempN.Checked = True Then
                dsRow("COA_YN") = LMH040C.COA_N
            ElseIf .optTempY.Checked = True Then
                dsRow("COA_YN") = LMH040C.COA_Y
            End If
            '▲▲▲(ソート)

            '▼▼▼(ソート)
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("IRIME") = .numIrime.Value()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("IRIME_UT_NM") = .lblIrimeUtNm.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("BETU_WT") = .lblBetuWT.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("ONDO_KB") = .cmbOndoKB.SelectedValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("UNSO_ONDO_KB") = .cmbUnsoOndoKB.SelectedValue()
            dsRow("IRIME") = .numIrime.Value()
            dsRow("IRIME_UT_NM") = .lblIrimeUtNm.TextValue()
            dsRow("BETU_WT") = .lblBetuWT.TextValue()
            dsRow("ONDO_KB") = .cmbOndoKB.SelectedValue()
            dsRow("UNSO_ONDO_KB") = .cmbUnsoOndoKB.SelectedValue()
            '▲▲▲(ソート)

            '▼▼▼(ソート)
            '出荷単位
            'If .optCnt.Checked = True Then
            '    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("ALCTD_KB") = LMH040C.ALCTD_KB_KOSU
            '    .sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "個数")
            'ElseIf .optAmt.Checked = True Then
            '    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("ALCTD_KB") = LMH040C.ALCTD_KB_SURYO
            '    .sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "数量")
            'ElseIf .optSample.Checked = True Then
            '    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("ALCTD_KB") = LMH040C.ALCTD_KB_SAMPLE
            '    .sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "サンプル")
            'End If
            If .optCnt.Checked = True Then
                dsRow("ALCTD_KB") = LMH040C.ALCTD_KB_KOSU
                .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "個数")
            ElseIf .optAmt.Checked = True Then
                dsRow("ALCTD_KB") = LMH040C.ALCTD_KB_SURYO
                .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "数量")
            ElseIf .optSample.Checked = True Then
                dsRow("ALCTD_KB") = LMH040C.ALCTD_KB_SAMPLE
                .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "サンプル")
            End If
            '▲▲▲(ソート)

            '▼▼▼(ソート)
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("PKG_NB") = .numPkgNB.Value()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("OUTKA_TTL_NB") = .numOutkaTtlNB.Value()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("OUTKA_PKG_NB") = .numOutkaPkgNB.Value()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("PKG_UT_NM") = .lblPkgUtNm.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("OUTKA_HASU") = .numOutkaHasu.Value()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("KB_UT_NM") = .lblKbUtNm.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("OUTKA_TTL_QT") = .numOutkaTtlQT.Value()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("QT_UT_NM") = .lblQtUtNm.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("REMARK") = .txtGoodsRemark.TextValue()

            ''隠し項目
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("NRS_GOODS_CD") = .lblNrsGoodsCD.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("IRIME_UT") = .lblIrimeUt.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("PKG_UT") = .lblPkgUt.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("KB_UT") = .lblKbUt.TextValue()
            'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)("QT_UT") = .lblQtUt.TextValue()

            dsRow("PKG_NB") = .numPkgNB.Value()
            dsRow("OUTKA_TTL_NB") = .numOutkaTtlNB.Value()
            dsRow("OUTKA_PKG_NB") = .numOutkaPkgNB.Value()
            dsRow("PKG_UT_NM") = .lblPkgUtNm.TextValue()
            dsRow("OUTKA_HASU") = .numOutkaHasu.Value()
            dsRow("KB_UT_NM") = .lblKbUtNm.TextValue()
            dsRow("OUTKA_TTL_QT") = .numOutkaTtlQT.Value()
            dsRow("QT_UT_NM") = .lblQtUtNm.TextValue()
            dsRow("REMARK") = .txtGoodsRemark.TextValue()

            '隠し項目
            dsRow("NRS_GOODS_CD") = .lblNrsGoodsCD.TextValue()
            dsRow("IRIME_UT") = .lblIrimeUt.TextValue()
            dsRow("PKG_UT") = .lblPkgUt.TextValue()
            dsRow("KB_UT") = .lblKbUt.TextValue()
            dsRow("QT_UT") = .lblQtUt.TextValue()

            '▲▲▲(ソート)

            '▼▼▼(ソート)
            'スプレッドに格納結果反映（出荷単位以外）
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.EDI_CTL_NO_CHU, .txtEDICtlNOChu.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.CUST_GOODS_CD, .txtCustGoodsCD.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.M_GOODS_NM, .lblGoodsNM.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.IRIME, .numIrime.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.NB, .numOutkaTtlNB.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.OUTKA_TTL_QT, .numOutkaTtlQT.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.REMARK, .txtGoodsRemark.TextValue())
            '.sprGoodsDef.SetCellValue(rownum, LMH040C.SprMainColumnIndex.CUST_ORD_NO_DTL, .txtCustOrdNoDtl.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.EDI_CTL_NO_CHU, .txtEDICtlNOChu.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.CUST_GOODS_CD, .txtCustGoodsCD.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.M_GOODS_NM, .lblGoodsNM.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.IRIME, .numIrime.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.NB, .numOutkaTtlNB.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.OUTKA_TTL_QT, .numOutkaTtlQT.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.REMARK, .txtGoodsRemark.TextValue())
            .sprGoodsDef.SetCellValue(sprRow, LMH040C.SprMainColumnIndex.CUST_ORD_NO_DTL, .txtCustOrdNoDtl.TextValue())
            '▲▲▲(ソート)

        End With

        Return ds

    End Function

    '▼▼▼(ソート)
    ''' <summary>
    ''' 中番号から元データを特定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataRow</returns>
    ''' <remarks></remarks>
    Friend Function GetOutkaMDataRow(ByVal ds As DataSet, ByVal ediNoM As String) As DataRow

        Return ds.Tables(LMH040C.TABLE_NM_OUT_M).Select(String.Concat(" EDI_CTL_NO_CHU = '", ediNoM, "' "))(0)

    End Function

    ''' <summary>
    ''' 中番号からスプレッド行を特定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSprRowM() As Integer

        With Me._Frm.sprGoodsDef.Sheets(0)

            For i As Integer = 0 To .RowCount - 1

                If .Cells(i, LMH040C.SprMainColumnIndex.EDI_CTL_NO_CHU).Value.ToString() = Me._Frm.txtEDICtlNOChu.TextValue Then
                    Return i
                End If

            Next

        End With

        Return 0

    End Function

    ''' <summary>
    ''' 選択行から中番号を取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSelectRowMNo() As String

        Dim selectRow As Integer = Me._Frm.sprGoodsDef.ActiveSheet.ActiveRowIndex()

        Return Me._Frm.sprGoodsDef.Sheets(0).Cells(selectRow, LMH040C.SprMainColumnIndex.EDI_CTL_NO_CHU).Value.ToString()

    End Function
    '▲▲▲(ソート)

    ''' <summary>
    ''' 編集中データをデータセットに格納（自由項目（中））
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="rownum"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SaveEditMSprDate(ByVal ds As DataSet, ByVal rownum As Integer) As DataSet

        Dim colNm As String = String.Empty
        Dim input As String = String.Empty

        '▼▼▼(ソート)
        Dim ediNoM As String = Me._Frm.txtEDICtlNOChu.TextValue
        Dim dsRow As DataRow = Me.GetOutkaMDataRow(ds, ediNoM)
        '▲▲▲(ソート)

        '自由項目（中）設定
        With Me._Frm.sprFreeInputsM.Sheets(0)
            For i As Integer = 0 To .RowCount - 1

                colNm = .Cells(i, LMH040C.SprFreeColumnIndex.COLNM).Value.ToString()
                input = NothingConvertString(.Cells(i, LMH040C.SprFreeColumnIndex.INPUT).Value).ToString()
                '▼▼▼(ソート)
                'ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(rownum)(colNm) = input
                dsRow(colNm) = input
                '▲▲▲(ソート)
            Next
        End With

        Return ds
    End Function

    ''' <summary>
    ''' Nothing変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NothingConvertString(ByVal value As Object) As Object

        If value Is Nothing Then
            value = String.Empty
        End If

        Return value

    End Function

    ''' <summary>
    ''' 編集中データをデータセットに格納（自由項目（大））
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SaveEditLSprDate(ByVal ds As DataSet) As DataSet

        Dim colNm As String = String.Empty
        Dim input As String = String.Empty

        '自由項目（大）設定
        With Me._Frm.sprFreeInputsL.Sheets(0)
            For i As Integer = 0 To .RowCount - 1

                colNm = .Cells(i, LMH040C.SprFreeColumnIndex.COLNM).Value.ToString()
                input = .Cells(i, LMH040C.SprFreeColumnIndex.INPUT).Value.ToString()
                ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)(colNm) = input

            Next
        End With

        Return ds

    End Function

    ''' <summary>
    ''' チェックした行の「状態」項目を更新
    ''' </summary>
    ''' <param name="ds">更新対象データセット</param>
    ''' <param name="cd">DEL_KBコード</param>
    ''' <param name="nm">DEL_KB名称（スプレッドの「状態」に表示される）</param>
    ''' <remarks></remarks>
    Friend Sub SetDelKbData(ByRef ds As DataSet, ByVal cd As String, ByVal nm As String)

        With Me._Frm.sprGoodsDef.Sheets(0)

            For i As Integer = 0 To .RowCount - 1
                If .Cells(i, LMH040G.sprGoodsDef.DEF.ColNo).Value IsNot Nothing AndAlso _
                   .Cells(i, LMH040G.sprGoodsDef.DEF.ColNo).Value.ToString = True.ToString Then

                    '状態表示変更
                    .SetValue(i, LMH040C.SprMainColumnIndex.DEL, nm)

                    'データセットのDEL_KB、SYS_DEL_FLG変更
                    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(i).Item("DEL_KB") = cd
                    ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows(i).Item("SYS_DEL_FLG") = cd

                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' 2byte区分コードをEDIテーブル用1byteコードに変換
    ''' </summary>
    ''' <param name="cd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function getEdiKbCd(ByVal cd As String) As String

        If 0 < cd.Length Then
            Return cd.Substring(1, 1)
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' ヘッダー部データ格納（出荷データ（大））
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetHeaderDataSet(ByVal ds As DataSet) As DataSet

        With Me._Frm

            '出荷情報
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("OUTKO_DATE") = .imdOutkoDate.TextValue.Replace("/", "")
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("OUTKA_PLAN_DATE") = .imdOutkaPlanDate.TextValue.Replace("/", "")
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("ARR_PLAN_DATE") = .imdArrPlanDate.TextValue.Replace("/", "")
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("ARR_PLAN_TIME") = .cmbArrPlanTime.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("HOKOKU_DATE") = .imdHokokuDate.TextValue.Replace("/", "")
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("OUTKA_KB") = .cmbOutkaKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("SYUBETU_KB") = .cmbSyubetuKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("OUTKA_STATE_KB") = .cmbOutkaStateKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("CUST_ORD_NO") = .txtCustOrdNO.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("BUYER_ORD_NO") = .txtBuyerOrdNO.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("PICK_KB") = .cmbPickKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("OUTKAHOKOKU_YN") = Me.getEdiKbCd(.cmbOutkaHokokuYN.SelectedValue.ToString)
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("TOUKI_HOKAN_YN") = Me.getEdiKbCd(.cmbToukiHokanYN.SelectedValue.ToString)
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("NIYAKU_YN") = Me.getEdiKbCd(.cmbNiyakuYN.SelectedValue.ToString)
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("SHIP_CD_L") = .txtShipCDL.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("SHIP_NM_L") = .lblShipNM.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DENP_YN") = Me.getEdiKbCd(.cmbDenpYN.SelectedValue.ToString)
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_CD") = .txtDestCD.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_NM") = .lblDestNM.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("SP_NHS_KB") = .cmbSpNhsKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("COA_YN") = Me.getEdiKbCd(.cmbCoaYN.SelectedValue.ToString)
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_AD_1") = .txtDestAd1.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_AD_2") = .txtDestAd2.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_AD_3") = .txtDestAd3.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_AD_4") = .txtDestAd4.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_AD_5") = .txtDestAd5.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("EDI_DEST_CD") = .txtEDIDestCD.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_ZIP") = .txtDestZip.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_JIS_CD") = .txtDestJisCD.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_TEL") = .txtDestTel.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_FAX") = .txtDestFax.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("DEST_MAIL") = .txtDestEmail.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("REMARK") = .txtRemark.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_ATT") = .txtUnsoAtt.TextValue

            'その他、手配情報
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_MOTO_KB") = .cmbUnsoMotoKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_TEHAI_KB") = .cmbUnsoTehaiKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("SYARYO_KB") = .cmbSharyoKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("BIN_KB") = .cmbBinKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("PC_KB") = .cmbPcKB.SelectedValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_CD") = .txtUnsoCD.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNSO_BR_CD") = .txtUnsoBrCD.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("UNCHIN_TARIFF_CD") = .txtUntinTariffCD.TextValue
            ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("EXTC_TARIFF_CD") = .txtExtcTariff.TextValue

            '運送会社名称セット
            Dim unsoNM As String = String.Empty
            Dim unsoBrNM As String = String.Empty

            Dim drs As DataRow() = Nothing
            If Me._LMHconV.SelectUnsocoListDataRow(drs, .txtUnsoCD.TextValue, .txtUnsoBrCD.TextValue) = True Then
                unsoNM = drs(0).Item("UNSOCO_NM").ToString
                unsoBrNM = drs(0).Item("UNSOCO_BR_NM").ToString
            End If

            '編集不可能項目なので格納必要なし
            'ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("EDI_CTL_NO") = .lblEDICtlNO.TextValue
            'ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("EDI_STATE_KB") = .cmbEDIStateKB.SelectedValue
            'ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0)("OUTKA_CTL_NO") = .lblOutkaCtlNO.TextValue

        End With

        Return ds

    End Function

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(商品別情報)
    ''' </summary>
    ''' <remarks></remarks>
    Public Class sprGoodsDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.DEF, " ", 20, True)
        Public Shared DEL As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.DEL, "状態", 50, True)
        Public Shared EDI_CTL_NO_CHU As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.EDI_CTL_NO_CHU, "EDI管理番号(中)", 170, True)
        Public Shared CUST_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.CUST_GOODS_CD, "商品コード", 120, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.M_GOODS_NM, "商品名", 140, True)
        Public Shared ALCTD_KB_NM As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.ALCTD_KB_NM, "出荷単位", 80, True)
        Public Shared IRIME As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.IRIME, "入目", 120, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.NB, "個数", 120, True)
        Public Shared OUTKA_TTL_QT As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.OUTKA_TTL_QT, "数量", 120, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.REMARK, "商品別注意事項", 140, True)
        Public Shared CUST_ORD_NO_DTL As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.CUST_ORD_NO_DTL, "オーダー番号", 120, True)

        'invisible
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.SYS_UPD_DATE, "Last edited date", 86, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMH040C.SprMainColumnIndex.SYS_UPD_TIME, "Last edited time", 86, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(自由設定項目)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprFreeLDef

        'スプレッド(タイトル列)の設定
        Public Shared TITLE As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.TITLE, "タイトル", 350, True)
        Public Shared INPUT As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.INPUT, "入力", 750, True)
        'invisible
        Public Shared DB_COL_NM As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.COLNM, "DB列名", 0, False)
        Public Shared EDIT_ABLE_FLAG As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.EDIT_ABLE_FLAG, "編集可能有無", 0, False)
        Public Shared ROW_VISIBLE_FLAG As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.ROW_VISIBLE_FLAG, "表示有無", 0, False)
        Public Shared ROW_NUM As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.ROW_NUM, "行番号", 0, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(自由設定項目（中）)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprFreeMDef

        'スプレッド(タイトル列)の設定
        Public Shared TITLE As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.TITLE, "タイトル", 350, True)
        Public Shared INPUT As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.INPUT, "入力", 750, True)
        'invisible
        Public Shared DB_COL_NM As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.COLNM, "DB列名", 0, False)
        Public Shared EDIT_ABLE_FLAG As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.EDIT_ABLE_FLAG, "編集可能有無", 0, False)
        Public Shared ROW_VISIBLE_FLAG As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.ROW_VISIBLE_FLAG, "表示有無", 0, False)
        Public Shared ROW_NUM As SpreadColProperty = New SpreadColProperty(LMH040C.SprFreeColumnIndex.ROW_NUM, "行番号", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドに商品情報データを設定
    ''' </summary>
    ''' <remarks></remarks>0
    Friend Sub SetGoodsSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprGoodsDef
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            '行数設定
            Dim tbl As DataTable = ds.Tables(LMH040C.TABLE_NM_OUT_M)

            Dim lngcnt As Integer = tbl.Rows.Count()
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            '▼▼▼(マイナスデータ)
            'Dim nLabel As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999.999, True, 3, False, ",")
            'Dim nLabelInt As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, False, ",")
            Dim nLabel As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999.999, 9999999999.999, True, 3, False, ",")
            Dim nLabelInt As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, False, ",")
            '▲▲▲(マイナスデータ)

            Dim dRow As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dRow = tbl.Rows(i)

                'セルに値を設定
                .SetCellValue(i, sprGoodsDef.DEF.ColNo, LMConst.FLG.OFF)
                Select Case dRow("SYS_DEL_FLG").ToString()
                    Case LMH040C.DEL_KB_OK  '正常
                        .SetCellValue(i, sprGoodsDef.DEL.ColNo, LMH040C.DEL_KB_OK_NM)
                    Case LMH040C.DEL_KB_NG  '削除
                        .SetCellValue(i, sprGoodsDef.DEL.ColNo, LMH040C.DEL_KB_NG_NM)
                End Select
                .SetCellValue(i, sprGoodsDef.EDI_CTL_NO_CHU.ColNo, dRow("EDI_CTL_NO_CHU").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_GOODS_CD.ColNo, dRow("CUST_GOODS_CD").ToString())
                .SetCellValue(i, sprGoodsDef.GOODS_NM.ColNo, dRow("GOODS_NM").ToString())
                .SetCellValue(i, sprGoodsDef.ALCTD_KB_NM.ColNo, dRow("ALCTD_KB_NM").ToString())
                'Select Case dRow("ALCTD_KB_NM").ToString()
                '    Case "01"
                '        .SetCellValue(i, sprGoodsDef.ALCTD_KB.ColNo, "個数")
                '    Case "02"
                '        .SetCellValue(i, sprGoodsDef.ALCTD_KB.ColNo, "数量")
                '    Case "04"
                '        .SetCellValue(i, sprGoodsDef.ALCTD_KB.ColNo, "サンプル")
                'End Select
                .SetCellValue(i, sprGoodsDef.IRIME.ColNo, dRow("IRIME").ToString())
                .SetCellValue(i, sprGoodsDef.NB.ColNo, dRow("OUTKA_TTL_NB").ToString())
                .SetCellValue(i, sprGoodsDef.OUTKA_TTL_QT.ColNo, dRow("OUTKA_TTL_QT").ToString())
                .SetCellValue(i, sprGoodsDef.REMARK.ColNo, dRow("REMARK").ToString())
                .SetCellValue(i, sprGoodsDef.CUST_ORD_NO_DTL.ColNo, dRow("CUST_ORD_NO_DTL").ToString())


                .SetCellStyle(i, sprGoodsDef.DEF.ColNo, sDEF)
                '.SetCellStyle(i, sprGoodsDef.DEL.ColNo, LMSpreadUtility.GetComboCellKbn(spr, LM.Const.LMKbnConst.KBN_S051, True))
                .SetCellStyle(i, sprGoodsDef.DEL.ColNo, sLabel)
                .SetCellStyle(i, sprGoodsDef.EDI_CTL_NO_CHU.ColNo, sLabel)
                .SetCellStyle(i, sprGoodsDef.CUST_GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, sprGoodsDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGoodsDef.ALCTD_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprGoodsDef.IRIME.ColNo, nLabel)
                .SetCellStyle(i, sprGoodsDef.NB.ColNo, nLabelInt)
                .SetCellStyle(i, sprGoodsDef.OUTKA_TTL_QT.ColNo, nLabel)
                .SetCellStyle(i, sprGoodsDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprGoodsDef.CUST_ORD_NO_DTL.ColNo, sLabel)

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドに自由項目（大）データを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFreeLSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprFreeInputsL
        Dim dtOut As New DataSet

        With spr

            'スプレッド初期化
            .CrearSpread()

            .SuspendLayout()

            'データ行設定
            Me._Row = Me._Ds.Tables(LMH040C.TABLE_NM_OUT_L)(0)

            '行数設定
            Dim tbl As DataTable = ds.Tables(LMH040C.TABLE_NM_FREE_L)
            Dim lngcnt As Integer = tbl.Rows.Count

            'スプレッド設定
            Call Me.SetFreeSpreadDtl(spr, tbl, lngcnt)
 
            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドに自由項目（中）データを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFreeMSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprFreeInputsM
        Dim dtOut As New DataSet

        If ds.Tables(LMH040C.TABLE_NM_OUT_M).Rows.Count = 0 Then
            Exit Sub
        End If

        With spr

            'スプレッド初期化
            .CrearSpread()

            .SuspendLayout()

            'クリックしたEDI（中）データ取得
            '▼▼▼(ソート)
            'Dim rownum As Integer = Me._Frm.sprGoodsDef.ActiveSheet.ActiveRowIndex()
            'Me._Row = ds.Tables(LMH040C.TABLE_NM_OUT_M)(rownum)
            Dim rowNoM As String = GetSelectRowMNo()
            Me._Row = GetOutkaMDataRow(ds, rowNoM)
            '▲▲▲(ソート)

            '行数設定
            Dim tbl As DataTable = ds.Tables(LMH040C.TABLE_NM_FREE_M)
            Dim lngcnt As Integer = tbl.Rows.Count

            'スプレッド設定
            Call Me.SetFreeSpreadDtl(spr, tbl, lngcnt)

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 自由項目スプレッドレイアウト設定
    ''' </summary>
    ''' <param name="spr"></param>
    ''' <param name="tbl"></param>
    ''' <param name="lngcnt"></param>
    ''' <remarks></remarks>
    Private Sub SetFreeSpreadDtl(ByRef spr As LMSpread, ByVal tbl As DataTable, ByVal lngcnt As Integer)

        With spr

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            '▼▼▼(マイナスデータ)
            'Dim nCell As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, False, 3, True, ",")
            Dim nCell As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -999999999.999, 999999999.999, False, 3, True, ",")
            '▲▲▲(マイナスデータ)
            Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim tCell As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 200, False)

            Dim dRow As DataRow
            Dim dbColNm As String = String.Empty

            '表示№格納変数
            Dim rowCnt As Integer = 0

            Dim j As Integer = 0

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dRow = tbl.Rows(i)
                dbColNm = dRow("DB_COL_NM").ToString()

                '非表示項目の場合、スルー
                If LMHControlC.FLG_OFF.Equals(dRow("ROW_VISIBLE_FLAG").ToString()) = True Then
                    j = j + 1
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルに値を設定
                .SetCellValue(rowCnt, sprFreeLDef.TITLE.ColNo, Me._LMHconG.EditConcatData(dbColNm, dRow("FIELD_NM").ToString(), Space(1)))
                '.SetCellValue(rowCnt, sprFreeLDef.TITLE.ColNo, dRow("FIELD_NM").ToString())
                .SetCellValue(rowCnt, sprFreeLDef.DB_COL_NM.ColNo, dbColNm)
                .SetCellValue(rowCnt, sprFreeLDef.INPUT.ColNo, Me._Row(dbColNm).ToString())
                .SetCellValue(rowCnt, sprFreeLDef.EDIT_ABLE_FLAG.ColNo, dRow("EDIT_ABLE_FLAG").ToString())
                .SetCellValue(rowCnt, sprFreeLDef.ROW_VISIBLE_FLAG.ColNo, dRow("ROW_VISIBLE_FLAG").ToString())
                .SetCellValue(rowCnt, sprFreeLDef.ROW_NUM.ColNo, i.ToString())

                '運送時Mタブ固定対応 terakawa 2012.06.15 Start
                'FREE_C30の行番号を取得
                If spr.Name.Equals(Me._Frm.sprFreeInputsL.Name) Then
                    If LMH040C.COLMUN_NM_FREE_C30.Equals(dbColNm) = True Then
                        Me._FreeC30Row = i - j
                    End If
                End If
                '運送時Mタブ固定対応 terakawa 2012.06.15 End

                'セルスタイル設定
                .SetCellStyle(rowCnt, sprFreeLDef.TITLE.ColNo, sLabel)
                .SetCellStyle(rowCnt, sprFreeLDef.DB_COL_NM.ColNo, sLabel)

                'セルの編集制御フラグ（'00'：不可、'01'：可）
                'START EDI
                Dim editFlg As String = dRow("EDIT_ABLE_FLAG").ToString()
                'Dim editFlg As String = "01"
                'END EDI

                '運送時Mタブ固定対応 terakawa 2012.06.15 Start
                '運送データの場合、自由設定項目(中)のスプレッドは読み取り専用
                If spr.Name.Equals(Me._Frm.sprFreeInputsM.Name) Then
                    Dim freeC30 As String = Me._LMHconV.GetCellValue(Me._Frm.sprFreeInputsL.ActiveSheet _
                                            .Cells(Me._FreeC30Row, LMH040G.sprFreeLDef.INPUT.ColNo)).ToString()

                    If Left(freeC30, 2).ToString.Equals(LMH040C.UNSO_DATA) Then
                        editFlg = LMH040C.EDIT_FLG_DEFAULT
                    End If
                End If
                '運送時Mタブ固定対応 terakawa 2012.06.15 End

                If dbColNm.Substring(0, 6).Equals("FREE_C") Then
                    Select Case editFlg
                        Case "00"
                            .SetCellStyle(rowCnt, sprFreeLDef.INPUT.ColNo, sLabel)
                        Case "01"
                            .SetCellStyle(rowCnt, sprFreeLDef.INPUT.ColNo, tCell)
                    End Select
                Else
                    Select Case editFlg
                        Case "00"
                            .SetCellStyle(rowCnt, sprFreeLDef.INPUT.ColNo, nLabel)
                            Dim input As Double = Convert.ToDouble(.Sheets(0).Cells(rowCnt, sprFreeLDef.INPUT.ColNo).Text)
                            Dim val As String = System.Math.Floor(input).ToString("#,0")
                            val = String.Concat(val, ".", input.ToString.Substring(input.ToString.IndexOf(".") + 1))
                            .SetCellValue(rowCnt, sprFreeLDef.INPUT.ColNo, val)
                        Case "01"
                            .SetCellStyle(rowCnt, sprFreeLDef.INPUT.ColNo, nCell)
                    End Select
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        'データセット格納
        Me._Ds = ds

        With Me._Frm

            'スプレッドの行をクリア
            .sprGoodsDef.CrearSpread()
            .sprFreeInputsL.CrearSpread()
            .sprFreeInputsM.CrearSpread()

            '列数設定
            .sprGoodsDef.Sheets(0).ColumnCount = LMH040C.SprMainColumnIndex.LAST      '(中)一覧
            .sprFreeInputsL.Sheets(0).ColumnCount = LMH040C.SprFreeColumnIndex.LAST   '自由設定項目(大)一覧
            .sprFreeInputsM.Sheets(0).ColumnCount = LMH040C.SprFreeColumnIndex.LAST   '自由設定項目(中)一覧

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprGoodsDef.SetColProperty(New sprGoodsDef)
            .sprFreeInputsL.SetColProperty(New sprFreeLDef)
            .sprFreeInputsM.SetColProperty(New sprFreeMDef)

            '列固定位置を設定します。(ex.商品コードで固定)
            '.sprGoodsDef.Sheets(0).FrozenColumnCount = sprGoodsDef.CUST_GOODS_CD.ColNo + 1

            '列設定 + データ設定
            Call Me.SetGoodsSpread(ds)
            Call Me.SetFreeLSpread(ds)
            Call Me.SetFreeMSpread(ds)

            Me._Frm.sprFreeInputsM.Sheets(0).RowCount = 0

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
