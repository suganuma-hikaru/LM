' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM280G : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMM280Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMM280G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM280F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM280F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetFunctionKey()

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
            .F10ButtonName = String.Empty
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
            .F10ButtonEnabled = lock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F11ButtonEnabled = edit

        End With

    End Sub

#End Region

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal dispMode As String, ByVal recStatus As String)

        With Me._Frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recStatus
        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .sprYokomochiHed.TabIndex = LMM280C.CtlTabIndex.SPR_YOKO_HED
            .cmbBr.TabIndex = LMM280C.CtlTabIndex.CMB_BR
            .txtYokomochiTariff.TabIndex = LMM280C.CtlTabIndex.TXT_YOKOMOCHI_TARIFF_CD
            .txtBiko.TabIndex = LMM280C.CtlTabIndex.TXT_BIKO
            .cmbKeisanHoho.TabIndex = LMM280C.CtlTabIndex.CMB_KEISANHOHO
            .cmbMeisaiBunkatu.TabIndex = LMM280C.CtlTabIndex.CMB_MEISAI_BUNKATU
            .numMinHosho.TabIndex = LMM280C.CtlTabIndex.NUM_MIN_HOSHO
            .btnRowAdd.TabIndex = LMM280C.CtlTabIndex.BTN_ADD
            .btnRowDel.TabIndex = LMM280C.CtlTabIndex.BTN_DEL
            .sprYokomochiDtl.TabIndex = LMM280C.CtlTabIndex.SPR_YOKO_DTL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        '数値コントロールの書式設定
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            Select Case .lblSituation.DispMode
                Case DispMode.INIT _
                , DispMode.VIEW
                    .sprYokomochiHed.Focus()

                Case DispMode.EDIT
                    '新規、複写時⇒横持ちタリフコード
                    '編集時　　　⇒備考
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC _
                           , RecordStatus.COPY_REC
                            .txtYokomochiTariff.Focus()
                        Case RecordStatus.NOMAL_REC
                            .txtBiko.Focus()
                    End Select

            End Select

        End With

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
    ''' 項目のクリア処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

        With Me._Frm

            '数値項目に初期値0を設定する
            .numMinHosho.Value = 0

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprYokomochiHed.ActiveSheet

            .cmbBr.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.BR_CD.ColNo))
            .txtYokomochiTariff.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.YOKOMOCHI_TARIFF_CD.ColNo))
            .txtBiko.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.BIKO.ColNo))
            .cmbKeisanHoho.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.KEISAN_CD_KBN.ColNo))
            .cmbMeisaiBunkatu.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.MEISAI_BUNKATU_FLG.ColNo))
            .numMinHosho.Value = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.MIN_HOSHO.ColNo))

            '共通項目
            .lblCreateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.CREATE_USER.ColNo))
            .lblCreateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.CREATE_DATE.ColNo)))
            .lblUpdateUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.UPDATE_USER.ColNo))
            .lblUpdateDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.UPDATE_DATE.ColNo)))
            '隠し項目                           
            .lblUpdateTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM280G.sprHedDef.UPDATE_TIME.ColNo))

        End With

    End Sub

    ''' <summary>
    ''' 行追加/削除時計算方法コンボボックスのロック制御を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockKeisanHohoCombo()

        Dim lock As Boolean = True
        Dim unlock As Boolean = False
        Dim flg As Boolean = False

        With Me._Frm

            If .lblSituation.RecordStatus.Equals(RecordStatus.NEW_REC) Then
                If .sprYokomochiDtl.ActiveSheet.Rows.Count > 0 Then
                    flg = lock
                Else
                    flg = unlock
                End If
                Call Me._ControlG.LockComb(.cmbKeisanHoho, flg)           '計算方法
            End If

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 数値項目の書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            .numMinHosho.SetInputFields("###,###,##0.000", , 9, 1, , 3, 3, , Convert.ToDecimal(999999999.999), Convert.ToDecimal(0))

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
                    Call Me.ClearControl(Me._Frm)
                    Me._Frm.sprYokomochiDtl.CrearSpread()

                Case DispMode.EDIT

                    '画面項目を全ロック解除する
                    Call Me._ControlG.SetLockControl(Me._Frm, unLock)

                    '常にロック項目ロック制御
                    Call Me._ControlG.LockComb(.cmbBr, lock)

                    Select Case Me._Frm.lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC
                            '新規時項目のクリアを行う
                            Call Me.ClearControlNew()

                        Case RecordStatus.NOMAL_REC
                            '編集時ロック制御を行う
                            Call Me.LockControlEdit()

                        Case RecordStatus.COPY_REC
                            '複写時キー項目のクリアを行う
                            Call Me.ClearControlFukusha()

                    End Select
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        With Me._Frm

            '表示内容を初期化する
            Call Me.ClearControl(Me._Frm)
            .sprYokomochiDtl.CrearSpread()

            '初期値を設定
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd
            .cmbKeisanHoho.SelectedValue = LMM280C.KEISAN_CD_NISUGATA
            .cmbMeisaiBunkatu.SelectedValue = LMMControlC.FLG_OFF

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit()

        Dim lock As Boolean = True

        With Me._Frm

            Call Me._ControlG.LockText(.txtYokomochiTariff, lock)      '横持ちタリフコード
            Call Me._ControlG.LockComb(.cmbKeisanHoho, lock)           '計算方法

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        Dim lock As Boolean = True

        With Me._Frm

            '項目クリア
            .txtYokomochiTariff.TextValue = String.Empty
            .lblCreateDate.TextValue = String.Empty
            .lblCreateUser.TextValue = String.Empty
            .lblUpdateDate.TextValue = String.Empty
            .lblUpdateUser.TextValue = String.Empty

            '項目ロック
            Call Me._ControlG.LockComb(.cmbKeisanHoho, lock)           '計算方法

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(横持ちタリフヘッダSpread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprHedDef

        '******* 表示列 *******
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.STATUS, "状態", 60, True)
        Public Shared BR_NM As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.BR_NM, "営業所", 275, True)
        Public Shared YOKOMOCHI_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.YOKOMOCHI_TARIFF_CD, "横持ちタリフコード", 170, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.BIKO, "備考", 400, True)
        Public Shared KEISANHOHO As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.KEISANHOHO, "計算方法", 110, True)
        Public Shared MEISAI_BUNKATU As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.MEISAI_BUNKATU, "明細分割", 90, True)

        '******* 隠し列 *******
        Public Shared BR_CD As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.BR_CD, "", 50, False)
        Public Shared KEISAN_CD_KBN As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.KEISAN_CD_KBN, "", 50, False)
        Public Shared MEISAI_BUNKATU_FLG As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.MEISAI_BUNKATU_FLG, "", 50, False)
        Public Shared MIN_HOSHO As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.MIN_HOSHO, "", 50, False)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.CREATE_DATE, "", 50, False)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.CREATE_USER, "", 50, False)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.UPDATE_DATE, "", 50, False)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.UPDATE_USER, "", 50, False)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.UPDATE_TIME, "", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffColumnIndex.SYS_DEL_FLG, "", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(横持ちタリフ明細Spread)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDtlDef

        '******* 表示列 *******
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.DEF, " ", 20, True)
        Public Shared NISUGATA_KBN As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.NISUGATA_KBN, "荷姿", 150, True)
        Public Shared SHASHU As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.SHASHU, "車種", 100, True)
        Public Shared KUGIRI_JURYO As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.KUGIRI_JURYO, "区切重量", 90, True)
        Public Shared KIKENHIN As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.KIKENHIN, "危険品", 110, True)
        Public Shared TANKA_PER_KGS As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.TANKA_PER_KGS, "KGSあたり" & vbCrLf & "単価", 120, True)
        Public Shared TANKA As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.TANKA, "単価", 120, True)

        '******* 隠し列 *******
        Public Shared YOKO_TARIFF_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM280C.SprYokoTariffDtlColumnIndex.EDA_NO, "枝番", 80, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '横持ちタリフヘッダSpreadの初期化処理
        Call Me.InitHedSpread()

        '横持ちタリフ明細Spreadの初期化処理
        Call Me.InitDtlSpread()

    End Sub

    ''' <summary>
    ''' 検索結果を横持ちタリフヘッダSpreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprYokomochiHed

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprHedDef.DEF.ColNo, def)
                .SetCellStyle(i, sprHedDef.STATUS.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.BR_NM.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.YOKOMOCHI_TARIFF_CD.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.BIKO.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.KEISANHOHO.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.MEISAI_BUNKATU.ColNo, lbl)

                '**** 隠し列 ****
                .SetCellStyle(i, sprHedDef.BR_CD.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.KEISAN_CD_KBN.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.MEISAI_BUNKATU_FLG.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.MIN_HOSHO.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.CREATE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.CREATE_USER.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.UPDATE_DATE.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.UPDATE_USER.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.UPDATE_TIME.ColNo, lbl)
                .SetCellStyle(i, sprHedDef.SYS_DEL_FLG.ColNo, lbl)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprHedDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprHedDef.STATUS.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, sprHedDef.BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprHedDef.YOKOMOCHI_TARIFF_CD.ColNo, dr.Item("YOKO_TARIFF_CD").ToString())
                .SetCellValue(i, sprHedDef.BIKO.ColNo, dr.Item("YOKO_REM").ToString())
                .SetCellValue(i, sprHedDef.KEISANHOHO.ColNo, dr.Item("CALC_KB_NM").ToString())
                .SetCellValue(i, sprHedDef.MEISAI_BUNKATU.ColNo, dr.Item("SPLIT_FLG_NM").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprHedDef.BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprHedDef.KEISAN_CD_KBN.ColNo, dr.Item("CALC_KB").ToString())
                .SetCellValue(i, sprHedDef.MEISAI_BUNKATU_FLG.ColNo, dr.Item("SPLIT_FLG").ToString())
                .SetCellValue(i, sprHedDef.MIN_HOSHO.ColNo, dr.Item("YOKOMOCHI_MIN").ToString())
                .SetCellValue(i, sprHedDef.CREATE_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, sprHedDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, sprHedDef.UPDATE_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprHedDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, sprHedDef.UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprHedDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 表示内容を横持ちタリフ明細Spreadに表示
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadDtl(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprYokomochiDtl

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

            '枝番の初期化
            If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.COPY_REC
                        '複写データ時、枝番初期化
                        For i As Integer = 0 To lngcnt - 1
                            With dt.Rows(i)
                                .Item("YOKO_TARIFF_CD_EDA") = String.Empty
                            End With
                        Next
                End Select
            End If

            '列設定用変数
            Dim unlock As Boolean = False
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim nisugataKbn As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.NISUGATA)
            Dim shashuKbn As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.SHASHU)
            Dim kikenhinKbn As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.KIKENHIN)
            Dim numJuryo As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.KUGIRI_JURYO)
            Dim numTankaPerKgs As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.TANKA_PER_KGS)
            Dim numTanka As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.TANKA)
            Dim carKbn As String = String.Empty

            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString
                    Case LMM280C.KEISAN_CD_SHADATE
                        carKbn = dr.Item("CAR_KB").ToString()
                    Case Else
                        carKbn = String.Empty
                End Select

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprDtlDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDtlDef.NISUGATA_KBN.ColNo, nisugataKbn)
                .SetCellStyle(i, sprDtlDef.SHASHU.ColNo, shashuKbn)
                .SetCellStyle(i, sprDtlDef.KUGIRI_JURYO.ColNo, numJuryo)
                .SetCellStyle(i, sprDtlDef.KIKENHIN.ColNo, kikenhinKbn)
                .SetCellStyle(i, sprDtlDef.TANKA_PER_KGS.ColNo, numTankaPerKgs)
                .SetCellStyle(i, sprDtlDef.TANKA.ColNo, numTanka)

                '**** 隠し列 ****
                .SetCellStyle(i, sprDtlDef.YOKO_TARIFF_CD_EDA.ColNo, lbl)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprDtlDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDtlDef.NISUGATA_KBN.ColNo, dr.Item("CARGO_KB").ToString())
                .SetCellValue(i, sprDtlDef.SHASHU.ColNo, carKbn)
                .SetCellValue(i, sprDtlDef.KUGIRI_JURYO.ColNo, dr.Item("WT_LV").ToString())
                .SetCellValue(i, sprDtlDef.KIKENHIN.ColNo, dr.Item("DANGER_KB").ToString())
                .SetCellValue(i, sprDtlDef.TANKA_PER_KGS.ColNo, dr.Item("KGS_PRICE").ToString())
                .SetCellValue(i, sprDtlDef.TANKA.ColNo, dr.Item("UT_PRICE").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprDtlDef.YOKO_TARIFF_CD_EDA.ColNo, dr.Item("YOKO_TARIFF_CD_EDA").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 横持ちタリフ明細Spreadに一行追加する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddRow()

        Dim spr As LMSpread = Me._Frm.sprYokomochiDtl

        With spr

            .SuspendLayout()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim nisugataKbn As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.NISUGATA)
            Dim shashuKbn As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.SHASHU)
            Dim kikenhinKbn As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.KIKENHIN)
            Dim numJuryo As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.KUGIRI_JURYO)
            Dim numTankaPerKgs As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.TANKA_PER_KGS)
            Dim numTanka As StyleInfo = Me.SetDtlColStyle(LMM280C.SetDtlCol.TANKA)

            Dim addRow As Integer = .ActiveSheet.Rows.Count - 1

            'セルスタイル設定
            '**** 表示列 ****
            .SetCellStyle(addRow, sprDtlDef.DEF.ColNo, def)
            .SetCellStyle(addRow, sprDtlDef.NISUGATA_KBN.ColNo, nisugataKbn)
            .SetCellStyle(addRow, sprDtlDef.SHASHU.ColNo, shashuKbn)
            .SetCellStyle(addRow, sprDtlDef.KUGIRI_JURYO.ColNo, numJuryo)
            .SetCellStyle(addRow, sprDtlDef.KIKENHIN.ColNo, kikenhinKbn)
            .SetCellStyle(addRow, sprDtlDef.TANKA_PER_KGS.ColNo, numTankaPerKgs)
            .SetCellStyle(addRow, sprDtlDef.TANKA.ColNo, numTanka)

            '**** 隠し列 ****
            .SetCellStyle(addRow, sprDtlDef.YOKO_TARIFF_CD_EDA.ColNo, lbl)

            'セル値設定
            '**** 表示列 ****
            .SetCellValue(addRow, sprDtlDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(addRow, sprDtlDef.NISUGATA_KBN.ColNo, String.Empty)
            .SetCellValue(addRow, sprDtlDef.SHASHU.ColNo, String.Empty)
            .SetCellValue(addRow, sprDtlDef.KUGIRI_JURYO.ColNo, "0")
            .SetCellValue(addRow, sprDtlDef.KIKENHIN.ColNo, String.Empty)
            .SetCellValue(addRow, sprDtlDef.TANKA_PER_KGS.ColNo, "0")
            .SetCellValue(addRow, sprDtlDef.TANKA.ColNo, "0")

            '**** 隠し列 ****
            .SetCellValue(addRow, sprDtlDef.YOKO_TARIFF_CD_EDA.ColNo, String.Empty)

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 横持ちタリフ明細Spreadの行削除を行う
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DelateDtl(ByVal list As ArrayList)

        Dim listMax As Integer = list.Count - 1
        For i As Integer = listMax To 0 Step -1
            Me._Frm.sprYokomochiDtl.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
        Next

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 横持ちタリフスプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitHedSpread()

        '横持ちタリフヘッダSpreadの初期値設定
        Dim sprHed As LMSpread = Me._Frm.sprYokomochiHed
        Dim dr As DataRow
        With sprHed

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 17

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprHedDef)

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprHed)

            '**** 表示列 ****
            .SetCellStyle(0, sprHedDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(sprHed, LMKbnConst.KBN_S051, False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .SetCellStyle(0, sprHedDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprHed, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprHedDef.BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprHed, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .SetCellStyle(0, sprHedDef.YOKOMOCHI_TARIFF_CD.ColNo, LMSpreadUtility.GetTextCell(sprHed, InputControl.ALL_HANKAKU, 10, False))
            .SetCellStyle(0, sprHedDef.BIKO.ColNo, LMSpreadUtility.GetTextCell(sprHed, InputControl.ALL_MIX, 100, False))
            .SetCellStyle(0, sprHedDef.KEISANHOHO.ColNo, LMSpreadUtility.GetComboCellKbn(sprHed, LMKbnConst.KBN_K012, False))
            .SetCellStyle(0, sprHedDef.MEISAI_BUNKATU.ColNo, LMSpreadUtility.GetComboCellKbn(sprHed, LMKbnConst.KBN_U009, False))

            '**** 隠し列 ****
            .SetCellStyle(0, sprHedDef.BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.KEISAN_CD_KBN.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.MEISAI_BUNKATU_FLG.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.MIN_HOSHO.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.UPDATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprHedDef.SYS_DEL_FLG.ColNo, lbl)

            '初期値設定
            Call Me._ControlG.ClearControl(sprHed)
            .SetCellValue(0, sprHedDef.STATUS.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprHedDef.BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())

        End With

    End Sub

    ''' <summary>
    ''' 横持ちタリフ明細スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitDtlSpread()

        '横持ちタリフ明細Spreadの初期値設定
        Dim sprDtl As LMSpread = Me._Frm.sprYokomochiDtl

        With sprDtl

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 8

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprDtlDef)

        End With

    End Sub

    ''' <summary>
    ''' 表示内容を横持ちタリフ明細Spreadに表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDtlColStyle(ByVal setCol As LMM280C.SetDtlCol) As StyleInfo

        Dim spr As LMSpread = Me._Frm.sprYokomochiDtl
        Dim rtnStyle As StyleInfo = Nothing

        With spr

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True
            Dim edit As Boolean = unlock

            If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                edit = unlock
            Else
                edit = lock
            End If

            Dim nisugata As Boolean = edit
            Dim shashu As Boolean = edit
            Dim kugiriJuryo As Boolean = edit
            Dim kikenhin As Boolean = edit
            Dim tankaPerKgs As Boolean = edit
            Dim tanka As Boolean = edit

            If edit = unlock Then
                Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString()
                    Case LMM280C.KEISAN_CD_NISUGATA
                        shashu = lock
                        kugiriJuryo = lock
                        tankaPerKgs = lock
                    Case LMM280C.KEISAN_CD_SHADATE
                        nisugata = lock
                        kugiriJuryo = lock
                        tankaPerKgs = lock
                    Case LMM280C.KEISAN_CD_TEIZO_UNCHIN
                        nisugata = lock
                        shashu = lock
                        tankaPerKgs = lock
                    Case LMM280C.KEISAN_CD_JURYO
                        nisugata = lock
                        shashu = lock
                        kugiriJuryo = lock
                        tanka = lock
                End Select
            End If

            Dim nisugataKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_N001, nisugata)
            Dim shashuKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_S012, shashu)
            Dim kikenhinKbn As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, LMKbnConst.KBN_K008, kikenhin)
            Dim numJuryo As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, kugiriJuryo, , , ",")
            Dim numTankaPerKgs As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999.999, tankaPerKgs, 3, , ",")
            Dim numTanka As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0.0, 999999999.999, tanka, 3, , ",")

            Select Case setCol
                Case LMM280C.SetDtlCol.NISUGATA
                    rtnStyle = nisugataKbn
                Case LMM280C.SetDtlCol.SHASHU
                    rtnStyle = shashuKbn
                Case LMM280C.SetDtlCol.KUGIRI_JURYO
                    rtnStyle = numJuryo
                Case LMM280C.SetDtlCol.KIKENHIN
                    rtnStyle = kikenhinKbn
                Case LMM280C.SetDtlCol.TANKA_PER_KGS
                    rtnStyle = numTankaPerKgs
                Case LMM280C.SetDtlCol.TANKA
                    rtnStyle = numTanka
            End Select

        End With

        Return rtnStyle

    End Function

#End Region

#End Region

#End Region

#End Region

End Class
