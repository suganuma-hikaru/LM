' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF030G : 運行情報入力
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF030Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMF030G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF030F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF030F, ByVal g As LMFControlG)

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
            .F1ButtonName = String.Empty
            .F2ButtonName = LMFControlC.FUNCTION_HENSHU
            .F3ButtonName = String.Empty
            .F4ButtonName = LMFControlC.FUNCTION_SAKUJO
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = LMFControlC.FUNCTION_HOZON
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = view
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = view
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            '.F10ButtonEnabled = edit
            .F10ButtonEnabled = always
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
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

            .pnlUnso.TabIndex = LMF030C.CtlTabIndex.UNSO
            .cmbEigyo.TabIndex = LMF030C.CtlTabIndex.EIGYO
            .lblTripNo.TabIndex = LMF030C.CtlTabIndex.TRIPNO
            .lblTripDate.TabIndex = LMF030C.CtlTabIndex.TRIPDATE
            .cmbBinKbn.TabIndex = LMF030C.CtlTabIndex.BINKBN
            .txtCarNo.TabIndex = LMF030C.CtlTabIndex.CARNO
            .lblCarType.TabIndex = LMF030C.CtlTabIndex.CARTYPE
            .lblCarKey.TabIndex = LMF030C.CtlTabIndex.CARKEY
            .numOndoMx.TabIndex = LMF030C.CtlTabIndex.ONDOMM
            .numOndoMm.TabIndex = LMF030C.CtlTabIndex.ONDOMX
            .txtUnsocoCd.TabIndex = LMF030C.CtlTabIndex.UNSOCOCD
            .txtUnsocoBrCd.TabIndex = LMF030C.CtlTabIndex.UNSOCOBRCD
            .lblUnsocoNm.TabIndex = LMF030C.CtlTabIndex.UNSOCONM
            .txtDriverCd.TabIndex = LMF030C.CtlTabIndex.DRIVERCD
            .lblDriverNm.TabIndex = LMF030C.CtlTabIndex.DRIVERNM
            .cmbJshaKbn.TabIndex = LMF030C.CtlTabIndex.JSHAKBN
            .cmbHaiso.TabIndex = LMF030C.CtlTabIndex.HAISO
            .txtRem.TabIndex = LMF030C.CtlTabIndex.REMARK
            .numLoadWt.TabIndex = LMF030C.CtlTabIndex.LOADWT
            .numUnsoOndo.TabIndex = LMF030C.CtlTabIndex.UNSOONDO
            .numPayAmt.TabIndex = LMF030C.CtlTabIndex.PAYAMT
            .numUnsoNb.TabIndex = LMF030C.CtlTabIndex.UNSONB
            .numTripWt.TabIndex = LMF030C.CtlTabIndex.TRIPWT
            .numRevUnchin.TabIndex = LMF030C.CtlTabIndex.REVUNCHIN
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .cmbShiharai.TabIndex = LMF030C.CtlTabIndex.SHIHARAI
            .numKingaku.TabIndex = LMF030C.CtlTabIndex.SHIHARAIKINGAKU
            .numShiharaiWt.TabIndex = LMF030C.CtlTabIndex.SHIHARAIWT
            .numKensu.TabIndex = LMF030C.CtlTabIndex.SHIHARAIKENSU
            .cmbTariffKbn.TabIndex = LMF030C.CtlTabIndex.SHIHARAITARIFFKB
            .txtTariffCd.TabIndex = LMF030C.CtlTabIndex.SHIHARAITARIFFCD
            .txtExtcCd.TabIndex = LMF030C.CtlTabIndex.SHIHARAIEXTCCD
            .btnShiharaiKeisan.TabIndex = LMF030C.CtlTabIndex.SHIHARAIKEISAN
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .btnRowAdd.TabIndex = LMF030C.CtlTabIndex.ROWADD
            .btnRowDel.TabIndex = LMF030C.CtlTabIndex.ROWDEL
            .sprDetail.TabIndex = LMF030C.CtlTabIndex.DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '数値コントロール設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Dim edit As Boolean = True
            Dim lock As Boolean = True
            Dim clearFlg As Boolean = False
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            Dim view As Boolean = True
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            'モード判定
            If DispMode.EDIT.Equals(.lblSituation.DispMode) = True Then
                edit = False
            End If

            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                view = False
            End If
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            '常にロック
            Call Me._LMFconG.SetLockInputMan(.cmbEigyo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblTripNo, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblTripDate, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblCarKey, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.lblDriverNm, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numLoadWt, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numPayAmt, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numUnsoNb, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numTripWt, lock, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numRevUnchin, lock, clearFlg)

            '編集時に解除
            Call Me._LMFconG.SetLockInputMan(.cmbBinKbn, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtCarNo, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtUnsocoCd, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtUnsocoBrCd, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtDriverCd, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbJshaKbn, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbHaiso, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtRem, edit, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numUnsoOndo, edit, clearFlg)
            Call Me._LMFconG.SetLockControl(.btnRowAdd, edit)
            Call Me._LMFconG.SetLockControl(.btnRowDel, edit)

            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            '参照時に解除
            Call Me._LMFconG.SetLockInputMan(.cmbShiharai, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.cmbTariffKbn, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numKingaku, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numShiharaiWt, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.numKensu, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtTariffCd, view, clearFlg)
            Call Me._LMFconG.SetLockInputMan(.txtExtcCd, view, clearFlg)
            Call Me._LMFconG.SetLockControl(.btnShiharaiKeisan, view)

            If ("10").Equals(.cmbTariffKbn.SelectedValue) = False AndAlso _
                ("30").Equals(.cmbTariffKbn.SelectedValue) = False AndAlso _
                ("").Equals(.cmbTariffKbn.SelectedValue) = False Then
                Call Me._LMFconG.SetLockInputMan(.txtExtcCd, lock, clearFlg)
            End If
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()
            .cmbBinKbn.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.SelectedValue = Nothing
            .lblTripNo.TextValue = String.Empty
            .lblTripDate.TextValue = String.Empty
            .cmbBinKbn.SelectedValue = Nothing
            .txtCarNo.TextValue = String.Empty
            .lblCarType.TextValue = String.Empty
            .lblCarKey.TextValue = String.Empty
            .numOndoMx.Value = Nothing
            .numOndoMm.Value = Nothing
            .lblSyakenTruck.TextValue = String.Empty
            .lblSyakenTrailer.TextValue = String.Empty
            .txtUnsocoCd.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            .lblUnsocoNm.TextValue = String.Empty
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .txtUnsocoCdOld.TextValue = String.Empty
            .txtUnsocoBrCdOld.TextValue = String.Empty
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .txtDriverCd.TextValue = String.Empty
            .lblDriverNm.TextValue = String.Empty
            .cmbJshaKbn.SelectedValue = Nothing
            .cmbHaiso.SelectedValue = Nothing
            .txtRem.TextValue = String.Empty
            .numLoadWt.Value = 0
            .numUnsoOndo.Value = 0
            .numPayAmt.Value = 0
            .numUnsoNb.Value = 0
            .numTripWt.Value = 0
            .numRevUnchin.Value = 0
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .cmbShiharai.SelectedValue = Nothing
            .cmbTariffKbn.SelectedValue = Nothing
            .numKingaku.Value = 0
            .numShiharaiWt.Value = 0
            .numKensu.Value = 0
            .txtTariffCd.TextValue = String.Empty
            .lblTariffNm.TextValue = String.Empty
            .txtExtcCd.TextValue = String.Empty
            .lblExtcNm.TextValue = String.Empty
            .numShiharaiKingaku.Value = 0
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        End With

    End Sub

    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d9dec3 As Decimal = Convert.ToDecimal(LMF030C.MAX_9_3)
            Dim d2 As Decimal = Convert.ToDecimal(LMF030C.MAX_2)
            Dim md2 As Decimal = Convert.ToDecimal(LMF030C.MIN_2)
            Dim dMax As Decimal = Convert.ToDecimal(LMFControlC.MAX_KETA)
            Dim dMaxdec As Decimal = Convert.ToDecimal(LMFControlC.MAX_KETA_DEC)
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            Dim d3 As Decimal = Convert.ToDecimal(LMF030C.MAX_3)
            Dim d10 As Decimal = Convert.ToDecimal(LMF030C.MAX_10)
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            .numOndoMx.SetInputFields(LMF030C.SHARP2, , 2, , , 0, , , d2, md2)
            .numOndoMm.SetInputFields(LMF030C.SHARP2, , 2, , , 0, , , d2, md2)
            .numUnsoOndo.SetInputFields(LMF030C.SHARP2, , 2, , , 0, , , d2, md2)
            .numLoadWt.SetInputFields(LMF030C.SHARP9_3, , 9, , , 3, 3, , d9dec3)

            .numUnsoNb.SetInputFields(LMFControlC.MAX_18, , 18, , , 0, , , dMax)
            .numRevUnchin.SetInputFields(LMFControlC.MAX_18, , 18, , , 0, , , dMax)
            .numPayAmt.SetInputFields(LMFControlC.MAX_18, , 18, , , 0, , , dMaxdec)
            .numTripWt.SetInputFields(LMFControlC.MAX_15_3, , 15, , , 3, 3, , dMaxdec)

            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .numKingaku.SetInputFields(LMF030C.SHARP10, , 10, , , 0, , , d10)
            .numShiharaiWt.SetInputFields(LMF030C.SHARP9_3, , 9, , , 3, 3, , d9dec3)
            .numKensu.SetInputFields(LMF030C.SHARP3, , 3, , , 0, , , d3)
            .numShiharaiKingaku.SetInputFields(LMF030C.SHARP10, , 10, , , 0, , , d10)
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ds As DataSet)

        'ヘッダ項目の値設定
        Call Me.SetHeaderData(ds)

        '明細の値設定
        Call Me.SetSpread(ds)

    End Sub

    ''' <summary>
    ''' ヘッダ項目の値設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMF030C.TABLE_NM_UNSO_LL).Rows(0)

        With Me._Frm

            .cmbEigyo.SelectedValue = dr.Item("NRS_BR_CD").ToString()
            .lblTripNo.TextValue = dr.Item("TRIP_NO").ToString()
            .lblTripDate.TextValue = DateFormatUtility.EditSlash(dr.Item("TRIP_DATE").ToString())
            .cmbBinKbn.SelectedValue = dr.Item("BIN_KB").ToString()
            .txtCarNo.TextValue = dr.Item("CAR_NO").ToString()
            .lblCarType.TextValue = dr.Item("CAR_TP_NM").ToString()
            .lblCarKey.TextValue = dr.Item("CAR_KEY").ToString()
            .numOndoMx.Value = Me._LMFconG.FormatNumValue(dr.Item("ONDO_MX").ToString())
            .numOndoMm.Value = Me._LMFconG.FormatNumValue(dr.Item("ONDO_MM").ToString())
            .lblSyakenTruck.TextValue = dr.Item("INSPC_DATE_TRUCK").ToString()
            .lblSyakenTrailer.TextValue = dr.Item("INSPC_DATE_TRAILER").ToString()
            .txtUnsocoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
            .txtUnsocoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .txtUnsocoCdOld.TextValue = dr.Item("UNSOCO_CD").ToString()
            .txtUnsocoBrCdOld.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .lblUnsocoNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))
            .txtDriverCd.TextValue = dr.Item("DRIVER_CD").ToString()
            .lblDriverNm.TextValue = dr.Item("DRIVER_NM").ToString()
            .cmbJshaKbn.SelectedValue = dr.Item("JSHA_KB").ToString()
            .cmbHaiso.SelectedValue = dr.Item("HAISO_KB").ToString()
            .txtRem.TextValue = dr.Item("REMARK").ToString()
            .numLoadWt.Value = Me._LMFconG.FormatNumValue(dr.Item("LOAD_WT").ToString())
            .numUnsoOndo.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_ONDO").ToString())
            .numPayAmt.Value = Me._LMFconG.FormatNumValue(dr.Item("PAY_UNCHIN").ToString())
            .numUnsoNb.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_PKG_NB").ToString())
            .numTripWt.Value = Me._LMFconG.FormatNumValue(dr.Item("UNSO_WT").ToString())
            .numRevUnchin.Value = Me._LMFconG.FormatNumValue(dr.Item("DECI_UNCHIN").ToString())

            '要望番号2063 追加START 2015.05.27
            .lblTehaisyubetsu.TextValue = dr.Item("TEHAI_SYUBETSU").ToString()
            '要望番号2063 追加END 2015.05.27

            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            .numShiharaiWt.Value = Me._LMFconG.FormatNumValue(dr.Item("SHIHARAI_UNSO_WT").ToString())
            .numKensu.Value = Me._LMFconG.FormatNumValue(dr.Item("SHIHARAI_COUNT").ToString())
            .cmbTariffKbn.SelectedValue = dr.Item("SHIHARAI_TARIFF_BUNRUI_KB").ToString()
            .txtTariffCd.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()
            .lblTariffNm.TextValue = String.Empty
            .txtExtcCd.TextValue = dr.Item("SHIHARAI_ETARIFF_CD").ToString()
            .lblExtcNm.TextValue = String.Empty
            .numShiharaiKingaku.Value = Me._LMFconG.FormatNumValue(dr.Item("SHIHARAI_UNCHIN").ToString())

            '運行テーブルの支払先情報が設定されていない場合のみ、運送会社マスタに設定されているタリフコードを設定する
            Dim unsoDr() As DataRow = Nothing
            If String.IsNullOrEmpty(.txtTariffCd.TextValue) = True Then
                'unsoDr = Me._LMFconG.SelectUnsocoListDataRow(.txtUnsocoCd.TextValue, .txtUnsocoBrCd.TextValue)
                '20160928 要番2622 tsunehira add
                unsoDr = Me._LMFconG.SelectUnsocoListDataRow(.cmbEigyo.SelectedValue.ToString, .txtUnsocoCd.TextValue, .txtUnsocoBrCd.TextValue)
                If unsoDr.Length > 0 Then
                    'マスタの値を設定
                    .txtTariffCd.TextValue = unsoDr(0).Item("UNCHIN_TARIFF_CD").ToString()
                    .txtExtcCd.TextValue = unsoDr(0).Item("EXTC_TARIFF_CD").ToString()
                End If
            End If

            Dim tariffRow() As DataRow = Nothing
            '支払タリフ名取得
            If ("40").Equals(.cmbTariffKbn.SelectedValue) = False Then
                '横持以外の場合
                tariffRow = Me._LMFconG.SelectShiharaiTariffListDataRow(.txtTariffCd.TextValue)
                If tariffRow.Length > 0 Then
                    .lblTariffNm.TextValue = tariffRow(0).Item("SHIHARAI_TARIFF_REM").ToString()
                End If
            ElseIf ("40").Equals(.cmbTariffKbn.SelectedValue) = True Then
                '横持の場合
                tariffRow = Me._LMFconG.SelectShiharaiYokoTariffListDataRow(Convert.ToString(.cmbEigyo.SelectedValue), .txtTariffCd.TextValue)
                If tariffRow.Length > 0 Then
                    .lblTariffNm.TextValue = tariffRow(0).Item("YOKO_REM").ToString()
                End If
            End If

            '支払割増タリフ名取得
            tariffRow = Me._LMFconG.SelectExtcShiharaiListDataRow(Convert.ToString(.cmbEigyo.SelectedValue), .txtExtcCd.TextValue, String.Empty)
            If tariffRow.Length > 0 Then
                .lblExtcNm.TextValue = tariffRow(0).Item("EXTC_TARIFF_REM").ToString()
            End If
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等


            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 Start
            'ぎょうへんしゅうふらぐをオフにせってい
            .txtIsEditRowFlg.TextValue = "0"
            '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう) 2012/08/29 本明 End

        End With

    End Sub

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' タリフ分類区分のロック設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTariffKbn()

        With Me._Frm

            If ("10").Equals(.cmbTariffKbn.SelectedValue) = False AndAlso _
                ("30").Equals(.cmbTariffKbn.SelectedValue) = False AndAlso _
                ("").Equals(.cmbTariffKbn.SelectedValue) = False Then
                '運送手配：車扱い、横持の場合
                .txtExtcCd.ReadOnly = True
                .txtExtcCd.TextValue = String.Empty
                .lblExtcNm.TextValue = String.Empty
            Else
                '運送手配：混載、特便の場合
                .txtExtcCd.ReadOnly = False
            End If

        End With

    End Sub
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared BINKBN As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.BINKBN, "便区分", 90, True)
        Public Shared AREA As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.AREA, "エリア名", 120, True)
        Public Shared NONYUDATE As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.NONYUDATE, "納入予定", 100, True)
        Public Shared ORIG_NM As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.ORIG_NM, "発地名", 120, True)
        Public Shared ORIG_ADD As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.ORIG_ADD, "発地住所", 120, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.DEST_NM, "届先名", 120, True)
        Public Shared DEST_ADD As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.DEST_ADD, "届先住所", 120, True)
        Public Shared TASYA_WH_NM As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.TASYA_WH_NM, "製品置き場" & vbCrLf & "（他社倉庫名称）", 140, True)
        Public Shared KOSU As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.KOSU, "総個数", 80, True)
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.JURYO, "重量", 80, True)
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.UNCHIN, "運賃", 80, True)
        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        Public Shared SHIHARAIUNCHIN As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.SHIHARAIUNCHIN, "支払運賃", 80, True)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        Public Shared CUST_NM As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.CUST_NM, "荷主名", 80, True)
        Public Shared CUST_REF_NO As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.CUST_REF_NO, "荷主参照番号", 120, True)
        Public Shared UNSO_NO As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.UNSO_NO, "運送番号", 80, True)
        Public Shared KANRI_NO As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.KANRI_NO, "管理番号", 80, True)
        Public Shared MOTO_DATA_KBN As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.MOTO_DATA_KBN, "元データ" & vbCrLf & "区分", 80, True)
        Public Shared UNSO_REM As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.UNSO_REM, "備考", 150, True)
        Public Shared UNSO_TEHAI_KBN As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.UNSO_TEHAI_KBN, "タリフ分類", 90, True)
        Public Shared ONKAN As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.ONKAN, "温管", 140, True)
        Public Shared UNSO_CD As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.UNSO_CD, "運送会社コード", 0, False)
        Public Shared UNSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.UNSO_BR_CD, "運送会社支店コード", 0, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.REC_NO, "レコード番号", 0, False)
        'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
        Public Shared OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMF030C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷予定日", 0, False)
        'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    End Class

    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
            ''列数設定
            '.sprDetail.ActiveSheet.ColumnCount = 22
            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = LMF030C.SprColumnIndex.LAST
            'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMF030G.sprDetailDef())
            .sprDetail.SetColProperty(New LMF030G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.納入予定で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMF030G.sprDetailDef.NONYUDATE.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetail

        'セルに設定するスタイルの取得
        Dim sNum10 As StyleInfo = Me.StyleInfoNum10(spr)
        Dim sNum9dec3 As StyleInfo = Me.StyleInfoNum9dec3(spr)
        Dim sNumMax As StyleInfo = Me.StyleInfoNumMax(spr)
        Dim sCheck As StyleInfo = Me.StyleInfoChk(spr)
        Dim sLbl As StyleInfo = Me.StyleInfoLabel(spr)

        '値設定用の変数
        Dim rowCnt As Integer = 0
        Dim setDt As DataTable = ds.Tables(LMF030C.TABLE_NM_UNSO_L)
        Dim setDr As DataRow = Nothing
        Dim max As Integer = setDt.Rows.Count - 1

        With spr

            '行クリア
            .CrearSpread()

            .SuspendLayout()

            Dim ketasu As Integer = (max + 1).ToString().Length
            Dim keta As String = Me._LMFconG.GetZeroData(ketasu)

            For i As Integer = 0 To max

                '設定する行
                setDr = setDt.Rows(i)

                '削除されているレコードの場合、スルー
                If LMConst.FLG.ON.Equals(setDr.Item("SYS_DEL_FLG").ToString()) = True Then
                    Continue For
                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.DEF.ColNo, sCheck)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.BINKBN.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.AREA.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.NONYUDATE.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.ORIG_NM.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.ORIG_ADD.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.DEST_NM.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.DEST_ADD.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.TASYA_WH_NM.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.KOSU.ColNo, sNum10)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.JURYO.ColNo, sNum9dec3)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.UNCHIN.ColNo, sNumMax)
                'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.SHIHARAIUNCHIN.ColNo, sNumMax)
                'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.CUST_NM.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.CUST_REF_NO.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.UNSO_NO.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.KANRI_NO.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.MOTO_DATA_KBN.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.UNSO_REM.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.UNSO_TEHAI_KBN.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.ONKAN.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.UNSO_CD.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.UNSO_BR_CD.ColNo, sLbl)
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.REC_NO.ColNo, sLbl)
                'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                .SetCellStyle(rowCnt, LMF030G.sprDetailDef.OUTKA_PLAN_DATE.ColNo, sLbl)
                'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

                '値設定
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.DEF.ColNo, False.ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.BINKBN.ColNo, setDr.Item("BIN_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.AREA.ColNo, setDr.Item("AREA_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.NONYUDATE.ColNo, DateFormatUtility.EditSlash(setDr.Item("ARR_PLAN_DATE").ToString()))
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.ORIG_NM.ColNo, setDr.Item("ORIG_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.ORIG_ADD.ColNo, setDr.Item("ORIG_AD_1").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.DEST_NM.ColNo, setDr.Item("DEST_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.DEST_ADD.ColNo, setDr.Item("DEST_AD_1").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.TASYA_WH_NM.ColNo, setDr.Item("TASYA_WH_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.KOSU.ColNo, setDr.Item("UNSO_PKG_NB").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.JURYO.ColNo, setDr.Item("UNSO_WT").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.UNCHIN.ColNo, setDr.Item("UNCHIN").ToString())
                'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.SHIHARAIUNCHIN.ColNo, setDr.Item("SHIHARAI_UNCHIN").ToString())
                'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.CUST_NM.ColNo, Me._LMFconG.EditConcatData(setDr.Item("CUST_NM_L").ToString(), setDr.Item("CUST_NM_M").ToString(), Space(1)))
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.CUST_REF_NO.ColNo, setDr.Item("CUST_REF_NO").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.UNSO_NO.ColNo, setDr.Item("UNSO_NO_L").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.KANRI_NO.ColNo, setDr.Item("INOUTKA_NO_L").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.MOTO_DATA_KBN.ColNo, setDr.Item("MOTO_DATA_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.UNSO_REM.ColNo, setDr.Item("REMARK").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.UNSO_TEHAI_KBN.ColNo, setDr.Item("TARIFF_BUNRUI_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.ONKAN.ColNo, setDr.Item("UNSO_ONDO_NM").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.UNSO_CD.ColNo, setDr.Item("UNSO_CD").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.UNSO_BR_CD.ColNo, setDr.Item("UNSO_BR_CD").ToString())
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.REC_NO.ColNo, Me._LMFconG.SetMaeZeroData(i.ToString(), ketasu, keta))
                'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
                .SetCellValue(rowCnt, LMF030G.sprDetailDef.OUTKA_PLAN_DATE.ColNo, setDr.Item("OUTKA_PLAN_DATE").ToString())
                'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

            Next

            .ResumeLayout(True)

            '行がない場合、運行日をクリア
            If .ActiveSheet.Rows.Count < 1 Then
                Me._Frm.lblTripDate.TextValue = String.Empty
            End If

        End With

    End Sub

    ''' <summary>
    ''' 並び替え処理
    ''' </summary>
    ''' <param name="spr">スプレッドシート</param>
    ''' <param name="colNo">ソート列</param>
    ''' <remarks></remarks>
    Friend Sub sprSortColumnCommand(ByVal spr As LMSpread, ByVal colNo As Integer)

        spr.ActiveSheet.SortRows(colNo, True, False)

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
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9dec3(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, True, 3, , ",")

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
