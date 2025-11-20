' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMM     : マスタ
'  プログラムID   : LMM550G : 下払いタリフマスタメンテ
'  作  成  者     : matsumoto
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMM550Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM550G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM550F

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

    ''' <summary>
    ''' カウントの保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _BeCnt As Integer = 0

    ''' <summary>
    ''' カウントの保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _AfCnt As Integer = 0

    ''' <summary>
    ''' Spread部(下部)利用状況
    ''' </summary>
    ''' <remarks>
    ''' TypeA: JISコード(起点/着点)が絡まないタリフで使用するスプレッドシート [基本]
    ''' TypeB: JISコード(起点/着点)が絡むタリフで使用するスプレッドシート [特殊]
    ''' </remarks>    
    Friend _SpreadType As LMM550C.SpreadType = LMM550C.SpreadType.A

    ''' <summary>
    ''' Spread部(下部)利用状況がTypeBの際の計算種別
    ''' </summary>
    Friend _SpreadTypeSub As String = ""

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM550F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        'Gamen共通クラスの設定
        _ControlG = New LMMControlG(handlerClass, DirectCast(frm, Form))

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
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            '.F7ButtonName = LMMControlC.FUNCTION_F7_INPUTXLS
            '.F8ButtonName = LMMControlC.FUNCTION_F8_OUTPUTXLS
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
            '.F7ButtonEnabled = view
            '.F8ButtonEnabled = view
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

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

            .sprDetail.TabIndex = LMM550C.CtlTabIndex.SHIHARAI_TARIFF_HD
            '編集部
            .cmbNrsBrCd.TabIndex = LMM550C.CtlTabIndex.NRS_BR_CD
            .txtShiharaiTariffCd.TabIndex = LMM550C.CtlTabIndex.SHIHARAI_TARIFF_CD
            .imdStrDate.TabIndex = LMM550C.CtlTabIndex.STR_DATE
            .cmbDataTp.TabIndex = LMM550C.CtlTabIndex.DATA_TP
            .cmbTableTp.TabIndex = LMM550C.CtlTabIndex.TABLE_TP
            .txtShiharaiTariffRem.TabIndex = LMM550C.CtlTabIndex.SHIHARAI_TARIFF_REM
            .txtShiharaiTariffCd2.TabIndex = LMM550C.CtlTabIndex.SHIHARAI_TARIFF_CD2
            '運賃タリフ(距離刻み/運賃)スプレッド
            .btnRowAdd.TabIndex = LMM550C.CtlTabIndex.BTN_ADD
            .btnRowDel.TabIndex = LMM550C.CtlTabIndex.BTN_DEL
            .btnColAdd.TabIndex = LMM550C.CtlTabIndex.BTN_COL_ADD
            .btnColDel.TabIndex = LMM550C.CtlTabIndex.BTN_COL_DEL
            .btnLock.TabIndex = LMM550C.CtlTabIndex.BTN_LOCK
            .btnUnLock.TabIndex = LMM550C.CtlTabIndex.BTN_UNLOCK
            .sprDetail2.TabIndex = LMM550C.CtlTabIndex.SHIHARAI_TARIFF_DTL
            .sprDetail3.TabIndex = LMM550C.CtlTabIndex.SHIHARAI_TARIFF_DTL_B

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

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
                    .sprDetail.Focus()

                Case DispMode.EDIT
                    '新規、複写時⇒支払タリフコード
                    '編集時　　　⇒データタイプ or 備考
                    Select Case .lblSituation.RecordStatus
                        Case RecordStatus.NEW_REC _
                           , RecordStatus.COPY_REC
                            .txtShiharaiTariffCd.Focus()
                        Case RecordStatus.NOMAL_REC
                            If (LMMControlC.FLG_OFF).Equals(.cmbDataTp.SelectedValue) = True Then
                                .cmbDataTp.Focus()
                            Else
                                .txtShiharaiTariffRem.Focus()
                            End If

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

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet

            .cmbNrsBrCd.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.NRS_BR_CD.ColNo))
            .txtShiharaiTariffCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD.ColNo))            
            .imdStrDate.TextValue = DateFormatUtility.DeleteSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.STR_DATE.ColNo)))
            .cmbDataTp.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.DATA_TP.ColNo))
            .cmbTableTp.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.TABLE_TP.ColNo))
            .txtShiharaiTariffRem.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SHIHARAI_TARIFF_REM.ColNo))
            .txtShiharaiTariffCd2.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD2.ColNo))
            '共通項目
            .lblCrtUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SYS_ENT_USER_NM.ColNo))
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SYS_ENT_DATE.ColNo)))
            .lblUpdUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SYS_UPD_USER_NM.ColNo))
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SYS_UPD_DATE.ColNo)))
            '隠し項目                           
            .lblUpdTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SYS_UPD_TIME.ColNo))
            .lblSysDelFlg.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMM550G.sprDetailDef.SYS_DEL_FLG.ColNo))

        End With

    End Sub

    ''' <summary>
    ''' Excel取り込みデータを編集部と運賃タリフ(距離刻み/運賃)Spreadに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetExcelUnchinData(ByVal ds As DataSet, ByVal eventShubetsu As LMM550C.EventShubetsu)

        Dim dt As DataTable = ds.Tables(LMM550C.TABLE_NM_KYORI)
        Dim dr As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty

        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '編集部(ヘッダー情報)にDataSetのデータタイプ＝00の情報を設定

            If (dr("DATA_TP").ToString).Equals(LMM550C.DATA_TP_KBN_00) = True Then
                With Me._Frm
                    .cmbNrsBrCd.SelectedValue = dr("NRS_BR_CD")
                    .txtShiharaiTariffCd.TextValue = dr("SHIHARAI_TARIFF_CD").ToString
                    .imdStrDate.TextValue = dr("STR_DATE").ToString
                    .cmbDataTp.SelectedValue = dr("DATA_TP")
                    .cmbTableTp.SelectedValue = dr("TABLE_TP")
                    .txtShiharaiTariffRem.TextValue = dr("SHIHARAI_TARIFF_REM").ToString
                    .txtShiharaiTariffCd2.TextValue = dr("SHIHARAI_TARIFF_CD2").ToString
                    '共通項目
                    .lblCrtUser.TextValue = dr("SYS_ENT_USER").ToString
                    .lblCrtDate.TextValue = dr("SYS_ENT_DATE").ToString
                    .lblUpdDate.TextValue = dr("SYS_UPD_DATE").ToString
                    .lblUpdUser.TextValue = dr("SYS_UPD_USER").ToString
                    '隠し項目                           
                    .lblUpdTime.TextValue = dr("SYS_UPD_TIME").ToString
                    .lblSysDelFlg.TextValue = dr("SYS_DEL_FLG").ToString
                    'Key項目の設定
                    nrsBrCd = dr("NRS_BR_CD").ToString
                End With
            Else
                With Me._Frm
                    '隠し項目(データタイプ='00'以外)                           
                    .lblExcelDt.TextValue = dr("DATA_TP").ToString
                End With
            End If

        Next

        Call Me.SetSpread2(dt, eventShubetsu, nrsBrCd, , )

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm
            Me._ControlG.SetDateFormat(.imdStrDate, LMMControlC.DATE_FORMAT.YYYY_MM_DD)
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
                    '編集部の項目をクリア
                    Call Me.ClearControl(Me._Frm)
                    '現在の列数のクリア
                    _BeCnt = 0
                    '運賃タリフ明細(距離刻み/運賃)の初期化処理
                    Call Me.InitUnchinTariffDTLSpread()

                Case DispMode.VIEW
                    '現在の列数のクリア
                    _BeCnt = 0

                Case DispMode.EDIT

                    '行追加/行削除/列挿入/列削除/ロック/アンロックボタン活性化
                    Call Me._ControlG.LockButton(.btnRowAdd, unLock)
                    Call Me._ControlG.LockButton(.btnRowDel, unLock)
                    Call Me._ControlG.LockButton(.btnColAdd, unLock)
                    Call Me._ControlG.LockButton(.btnColDel, unLock)
                    Call Me._ControlG.LockButton(.btnLock, unLock)
                    Call Me._ControlG.LockButton(.btnUnLock, unLock)

                    '編集部の項目のロック解除
                    Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                    '常にロック項目ロック制御
                    Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)

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

                            '運賃タリフ明細情報Spreadの隠し項目である"更新区分"の設定
                            If LMM550C.SpreadType.A.Equals(Me._SpreadType) Then
                                'TypeA
                                Dim RowCnt As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                                For i As Integer = 1 To RowCnt
                                    Dim unchinCdEda As String = _ControlG.SetMaeZeroData(Convert.ToString(i - 1), 3)
                                    '支払タリフコード枝番："000"から連番で設定
                                    .sprDetail2.SetCellValue(i, sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, unchinCdEda)
                                    '更新区分："0"を設定
                                    .sprDetail2.SetCellValue(i, sprDetailDef2.UPD_FLG.ColNo, "0")
                                    '削除フラグ："0"を設定
                                    .sprDetail2.SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")
                                Next
                            Else
                                'TypeB
                                Dim RowCnt As Integer = .sprDetail3.ActiveSheet.Rows.Count - 1
                                For i As Integer = 1 To RowCnt
                                    Dim unchinCdEda As String = _ControlG.SetMaeZeroData(Convert.ToString(i - 1), 3)
                                    '支払タリフコード枝番："000"から連番で設定
                                    .sprDetail3.SetCellValue(i, sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, unchinCdEda)
                                    '更新区分："0"を設定
                                    .sprDetail3.SetCellValue(i, sprDetailDef3.UPD_FLG.ColNo, "0")
                                    '削除フラグ："0"を設定
                                    .sprDetail3.SetCellValue(i, sprDetailDef3.SYS_DEL_FLG_T.ColNo, "0")
                                Next
                            End If

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

            '編集部の項目をクリア
            Call Me.ClearControl(Me._Frm)
            '現在の列数のクリア
            _BeCnt = 0
            '運賃タリフ明細(距離刻み/運賃)の初期化処理
            Call Me.InitUnchinTariffDTLSpread()
            '初期値を設定
            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd
            .cmbDataTp.SelectedValue = LMMControlC.FLG_ON             '01(運賃)
            .cmbTableTp.SelectedValue = LMMControlC.FLG_OFF           '00(重量・距離)

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit()

        Dim lock As Boolean = True

        With Me._Frm

            Call Me._ControlG.LockText(.txtShiharaiTariffCd, lock)      '支払タリフコード
            Call Me._ControlG.LockDate(.imdStrDate, lock)               '適用開始日
            Call Me._ControlG.LockComb(.cmbTableTp, lock)               '計算種別

            'データタイプ＝"00"'(距離刻み)以外のデータを編集した場合、データタイプをロック
            If (LMMControlC.FLG_OFF).Equals(.cmbDataTp.SelectedValue) = False Then
                Call Me._ControlG.LockComb(.cmbDataTp, lock)            'データタイプ
            End If

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        Dim lock As Boolean = True

        With Me._Frm

            '複写時のロック項目
            Call Me._ControlG.LockComb(.cmbTableTp, lock)        '計算種別

            '複写しない項目は空を設定
            .txtShiharaiTariffCd.TextValue = String.Empty
            .imdStrDate.TextValue = String.Empty

            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッド(下部)_距離刻みのクリア処理を行う
    ''' </summary>
    ''' <param name="lockFlg">クリア処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub ClearSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2)

            Dim spr As LMSpread = Me._Frm.sprDetail2
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail2, 0.0, 999999999.999, lockFlg, 3, True, ",")
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left)
            Dim visColCnt As Integer = 5 + _BeCnt

            '新規・初期の場合のみ、距離刻み行の初期設定を行う
            If RecordStatus.NEW_REC.Equals(Me._Frm.lblSituation.RecordStatus) = True _
             OrElse RecordStatus.INIT.Equals(Me._Frm.lblSituation.RecordStatus) = True Then

                With spr
                    '① 距離刻み行の「距離」の初期設定
                    Dim vis As Boolean = False
                    For i As Integer = 0 To 90
                        If LMM550C.SprColumnIndex2.KYORI_1 <= i And i <= LMM550C.SprColumnIndex2.KYORI_70 Then
                            'セルスタイル設定
                            .SetCellStyle(1, i, sNumber2)
                            'セルに値を設定
                            .SetCellValue(1, i, 0.ToString())
                        End If
                    Next
                    '② 距離刻み行の「支払タリフコード枝番・更新区分・削除フラグ」の初期設定
                    'セルスタイル設定
                    .SetCellStyle(1, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, sLabel)
                    .SetCellStyle(1, LMM550G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle(1, LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)
                    'セルに値を設定
                    .SetCellValue(1, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, "000")
                    .SetCellValue(1, LMM550G.sprDetailDef2.UPD_FLG.ColNo, LMM550C.FLG.OFF)
                    .SetCellValue(1, LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMM550C.FLG.OFF)
                End With

            End If

        End With

        'TypeBも同じタイミングで実行
        Call Me.ClearSpreadControl_B(lockFlg)

    End Sub

    ''' <summary>
    ''' スプレッド(下部/TypeB)_重量or個数刻みのクリア処理を行う
    ''' </summary>
    ''' <param name="lockFlg">クリア処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub ClearSpreadControl_B(ByVal lockFlg As Boolean)

        With Me._Frm

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail3)

            Dim spr As LMSpread = Me._Frm.sprDetail3
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail3, 0.0, 999999999.999, lockFlg, 3, True, ",")
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail3, CellHorizontalAlignment.Left)
            Dim visColCnt As Integer = 5 + _BeCnt

            '新規・初期の場合のみ、重量or個数刻み行の初期設定を行う
            If RecordStatus.NEW_REC.Equals(Me._Frm.lblSituation.RecordStatus) = True _
             OrElse RecordStatus.INIT.Equals(Me._Frm.lblSituation.RecordStatus) = True Then

                With spr
                    '① 重量or個数刻み行の「重量or個数」の初期設定
                    Dim vis As Boolean = False
                    For i As Integer = 0 To 90
                        If LMM550C.SprColumnIndex3.KYORI_1 <= i And i <= LMM550C.SprColumnIndex3.KYORI_70 Then
                            'セルスタイル設定
                            .SetCellStyle(1, i, sNumber2)
                            'セルに値を設定
                            .SetCellValue(1, i, 0.ToString())
                        End If
                    Next
                    '② 重量or個数刻み行の「支払タリフコード枝番・更新区分・削除フラグ」の初期設定
                    'セルスタイル設定
                    .SetCellStyle(1, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, sLabel)
                    .SetCellStyle(1, LMM550G.sprDetailDef3.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle(1, LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, sLabel)
                    'セルに値を設定
                    .SetCellValue(1, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, "000")
                    .SetCellValue(1, LMM550G.sprDetailDef3.UPD_FLG.ColNo, LMM550C.FLG.OFF)
                    .SetCellValue(1, LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, LMM550C.FLG.OFF)
                End With

            End If

        End With

    End Sub

    ''' <summary>
    ''' スプレッド(下部)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockBottomSpreadControl(ByVal lockFlg As Boolean)

        'TypeBの場合
        If LMM550C.SpreadType.B.Equals(Me._SpreadType) Then
            Me.SetLockBottomSpreadControl_B(lockFlg)
            Return
        End If

        With Me._Frm

            'ラベルスタイルの設定
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2)

            Dim spr As LMSpread = Me._Frm.sprDetail2
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left)
            Dim sChk As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail2, lockFlg)
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail2, 0, 999999999, lockFlg, 0, True, ",")
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail2, 0.0, 999999999.999, lockFlg, 3, True, ",")
            Dim sCmbS As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail2, LMKbnConst.KBN_S012, lockFlg)
            Dim sCmbT As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail2, LMKbnConst.KBN_T010, lockFlg)

            Dim max As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1

            For i As Integer = 1 To max

                .sprDetail2.SetCellStyle(i, sprDetailDef2.KYORI_1.ColNo, sNumber2)
                .sprDetail2.SetCellStyle(i, sprDetailDef2.KYORI_2.ColNo, sNumber2)
                .sprDetail2.SetCellStyle(i, sprDetailDef2.KYORI_3.ColNo, sNumber2)
                .sprDetail2.SetCellStyle(i, sprDetailDef2.KYORI_4.ColNo, sNumber2)
                .sprDetail2.SetCellStyle(i, sprDetailDef2.KYORI_5.ColNo, sNumber2)

                If i <> 1 Then
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.DEF.ColNo, sChk)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.WT_LV.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.CAR_TP.ColNo, sCmbS)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.NB.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.QT.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.T_SIZE.ColNo, sCmbT)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.CITY_EXTC_A.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.CITY_EXTC_B.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.WINT_EXTC_A.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.WINT_EXTC_B.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.RELY_EXTC.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.INSU.ColNo, sNumber1)
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    '.sprDetail2.SetCellStyle(i, sprDetailDef2.FRRY_EXTC_10KG.ColNo, sNumber1)
                    .sprDetail2.SetCellStyle(i, sprDetailDef2.FRRY_EXTC_PART.ColNo, sNumber1)
                End If
            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッド(下部/TypeB)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockBottomSpreadControl_B(ByVal lockFlg As Boolean)

        With Me._Frm

            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail3, CellHorizontalAlignment.Left)
            Dim sChk As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail3, lockFlg)
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail3, 0, 999999999, lockFlg, 0, True, ",")
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail3, 0.0, 999999999.999, lockFlg, 3, True, ",")
            Dim sCmbS As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail3, LMKbnConst.KBN_S012, lockFlg)
            Dim sCmbT As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail3, LMKbnConst.KBN_T010, lockFlg)
            Dim sText7 As StyleInfo = LMSpreadUtility.GetTextCell(.sprDetail3, InputControl.HAN_NUM_ALPHA, 7, lockFlg)

            Dim max As Integer = .sprDetail3.ActiveSheet.Rows.Count - 1

            For i As Integer = 1 To max

                .sprDetail3.SetCellStyle(i, sprDetailDef3.KYORI_1.ColNo, sNumber2)
                .sprDetail3.SetCellStyle(i, sprDetailDef3.KYORI_2.ColNo, sNumber2)
                .sprDetail3.SetCellStyle(i, sprDetailDef3.KYORI_3.ColNo, sNumber2)
                .sprDetail3.SetCellStyle(i, sprDetailDef3.KYORI_4.ColNo, sNumber2)
                .sprDetail3.SetCellStyle(i, sprDetailDef3.KYORI_5.ColNo, sNumber2)

                If i <> 1 Then
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.DEF.ColNo, sChk)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.ORIG_KEN_N.ColNo, sLabel)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.ORIG_CITY_N.ColNo, sLabel)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.ORIG_JIS_CD.ColNo, sText7)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.DEST_KEN_N.ColNo, sLabel)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.DEST_CITY_N.ColNo, sLabel)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.DEST_JIS_CD.ColNo, sText7)
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    '.sprDetail3.SetCellStyle(i, sprDetailDef3.FRRY_EXTC_10KG.ColNo, sNumber1)
                    .sprDetail3.SetCellStyle(i, sprDetailDef3.FRRY_EXTC_PART.ColNo, sNumber1)
                End If
            Next

        End With

    End Sub

#End Region

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)     '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)           '営業所名
        Public Shared SHIHARAI_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SHIHARAI_TARIFF_CD, "支払タリフコード", 150, True)
        Public Shared DATA_TP_NM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.DATA_TP_NM, "データタイプ", 100, True)
        Public Shared TABLE_TP_NM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.TABLE_TP_NM, "計算種別", 200, True)
        Public Shared STR_DATE As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.STR_DATE, "適用開始日", 100, True)

        '隠し項目
        Public Shared SHIHARAI_TARIFF_CD2 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SHIHARAI_TARIFF_CD2, "2次タリフコード", 80, False)
        Public Shared SHIHARAI_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SHIHARAI_TARIFF_REM, "支払タリフコード備考", 60, False)
        Public Shared DATA_TP As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.DATA_TP, "データタイプ", 60, False)
        Public Shared TABLE_TP As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.TABLE_TP, "テーブルタイプ", 60, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部/TypeA)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.DEF, " ", 20, True)
        Public Shared WT_LV As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.WT_LV, "重量", 100, True)
        Public Shared CAR_TP As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.CAR_TP, "車種", 80, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.NB, "個数", 100, True)
        Public Shared QT As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.QT, "数量", 100, True)
        Public Shared T_SIZE As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.T_SIZE, "宅急便ｻｲｽﾞ", 80, True)
        Public Shared KYORI_1 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_1, "", 100, True)
        Public Shared KYORI_2 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_2, "", 100, True)
        Public Shared KYORI_3 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_3, "", 100, True)
        Public Shared KYORI_4 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_4, "", 100, True)
        Public Shared KYORI_5 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_5, "", 100, True)
        Public Shared KYORI_6 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_6, "", 100, False)
        Public Shared KYORI_7 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_7, "", 100, False)
        Public Shared KYORI_8 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_8, "", 100, False)
        Public Shared KYORI_9 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_9, "", 100, False)
        Public Shared KYORI_10 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_10, "", 100, False)
        Public Shared KYORI_11 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_11, "", 100, False)
        Public Shared KYORI_12 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_12, "", 100, False)
        Public Shared KYORI_13 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_13, "", 100, False)
        Public Shared KYORI_14 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_14, "", 100, False)
        Public Shared KYORI_15 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_15, "", 100, False)
        Public Shared KYORI_16 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_16, "", 100, False)
        Public Shared KYORI_17 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_17, "", 100, False)
        Public Shared KYORI_18 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_18, "", 100, False)
        Public Shared KYORI_19 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_19, "", 100, False)
        Public Shared KYORI_20 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_20, "", 100, False)
        Public Shared KYORI_21 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_21, "", 100, False)
        Public Shared KYORI_22 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_22, "", 100, False)
        Public Shared KYORI_23 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_23, "", 100, False)
        Public Shared KYORI_24 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_24, "", 100, False)
        Public Shared KYORI_25 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_25, "", 100, False)
        Public Shared KYORI_26 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_26, "", 100, False)
        Public Shared KYORI_27 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_27, "", 100, False)
        Public Shared KYORI_28 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_28, "", 100, False)
        Public Shared KYORI_29 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_29, "", 100, False)
        Public Shared KYORI_30 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_30, "", 100, False)
        Public Shared KYORI_31 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_31, "", 100, False)
        Public Shared KYORI_32 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_32, "", 100, False)
        Public Shared KYORI_33 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_33, "", 100, False)
        Public Shared KYORI_34 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_34, "", 100, False)
        Public Shared KYORI_35 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_35, "", 100, False)
        Public Shared KYORI_36 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_36, "", 100, False)
        Public Shared KYORI_37 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_37, "", 100, False)
        Public Shared KYORI_38 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_38, "", 100, False)
        Public Shared KYORI_39 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_39, "", 100, False)
        Public Shared KYORI_40 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_40, "", 100, False)
        Public Shared KYORI_41 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_41, "", 100, False)
        Public Shared KYORI_42 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_42, "", 100, False)
        Public Shared KYORI_43 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_43, "", 100, False)
        Public Shared KYORI_44 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_44, "", 100, False)
        Public Shared KYORI_45 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_45, "", 100, False)
        Public Shared KYORI_46 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_46, "", 100, False)
        Public Shared KYORI_47 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_47, "", 100, False)
        Public Shared KYORI_48 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_48, "", 100, False)
        Public Shared KYORI_49 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_49, "", 100, False)
        Public Shared KYORI_50 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_50, "", 100, False)
        Public Shared KYORI_51 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_51, "", 100, False)
        Public Shared KYORI_52 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_52, "", 100, False)
        Public Shared KYORI_53 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_53, "", 100, False)
        Public Shared KYORI_54 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_54, "", 100, False)
        Public Shared KYORI_55 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_55, "", 100, False)
        Public Shared KYORI_56 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_56, "", 100, False)
        Public Shared KYORI_57 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_57, "", 100, False)
        Public Shared KYORI_58 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_58, "", 100, False)
        Public Shared KYORI_59 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_59, "", 100, False)
        Public Shared KYORI_60 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_60, "", 100, False)
        Public Shared KYORI_61 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_61, "", 100, False)
        Public Shared KYORI_62 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_62, "", 100, False)
        Public Shared KYORI_63 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_63, "", 100, False)
        Public Shared KYORI_64 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_64, "", 100, False)
        Public Shared KYORI_65 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_65, "", 100, False)
        Public Shared KYORI_66 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_66, "", 100, False)
        Public Shared KYORI_67 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_67, "", 100, False)
        Public Shared KYORI_68 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_68, "", 100, False)
        Public Shared KYORI_69 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_69, "", 100, False)
        Public Shared KYORI_70 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.KYORI_70, "", 100, False)
        Public Shared CITY_EXTC_A As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.CITY_EXTC_A, "都市割増A", 100, True)
        Public Shared CITY_EXTC_B As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.CITY_EXTC_B, "都市割増B", 100, True)
        Public Shared WINT_EXTC_A As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.WINT_EXTC_A, "冬期割増A", 100, True)
        Public Shared WINT_EXTC_B As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.WINT_EXTC_B, "冬期割増B", 100, True)
        Public Shared RELY_EXTC As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.RELY_EXTC, "中継料", 100, True)
        Public Shared INSU As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.INSU, "保険料", 100, True)
        '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
        'Public Shared FRRY_EXTC_10KG As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.FRRY_EXTC_10KG, "10㎏あたりの" & vbCrLf & "航送料", 100, True)

        '隠し項目
        Public Shared FRRY_EXTC_PART As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.FRRY_EXTC_PART, "1件あたりの" & vbCrLf & "航送料", 100, False)
        Public Shared SHIHARAI_TARIFF_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.SHIHARAI_TARIFF_CD_EDA, "支払タリフコード枝番", 100, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.UPD_FLG, "更新区分", 100, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.SYS_DEL_FLG_T, "削除フラグ", 100, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.SYS_UPD_DATE, "更新日", 100, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.SYS_UPD_TIME, "更新時刻", 100, False)
        Public Shared RECNO As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex2.RECNO, "RECNO", 30, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部/TypeB)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef3

        'スプレッド(タイトル列)の設定

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.DEF, " ", 20, True)
        Public Shared ORIG_KEN_N As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.ORIG_KEN_N, "(起点)都道府県名", 120, True)
        Public Shared ORIG_CITY_N As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.ORIG_CITY_N, "" & vbCrLf & "市区町村名", 100, True)
        Public Shared ORIG_JIS_CD As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.ORIG_JIS_CD, "" & vbCrLf & "JISコード", 80, True)
        Public Shared DEST_KEN_N As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.DEST_KEN_N, "(着点)都道府県名", 120, True)
        Public Shared DEST_CITY_N As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.DEST_CITY_N, "" & vbCrLf & "市区町村名", 100, True)
        Public Shared DEST_JIS_CD As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.DEST_JIS_CD, "" & vbCrLf & "JISコード", 80, True)
        Public Shared KYORI_1 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_1, "", 100, True)
        Public Shared KYORI_2 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_2, "", 100, True)
        Public Shared KYORI_3 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_3, "", 100, True)
        Public Shared KYORI_4 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_4, "", 100, True)
        Public Shared KYORI_5 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_5, "", 100, True)
        Public Shared KYORI_6 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_6, "", 100, False)
        Public Shared KYORI_7 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_7, "", 100, False)
        Public Shared KYORI_8 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_8, "", 100, False)
        Public Shared KYORI_9 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_9, "", 100, False)
        Public Shared KYORI_10 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_10, "", 100, False)
        Public Shared KYORI_11 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_11, "", 100, False)
        Public Shared KYORI_12 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_12, "", 100, False)
        Public Shared KYORI_13 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_13, "", 100, False)
        Public Shared KYORI_14 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_14, "", 100, False)
        Public Shared KYORI_15 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_15, "", 100, False)
        Public Shared KYORI_16 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_16, "", 100, False)
        Public Shared KYORI_17 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_17, "", 100, False)
        Public Shared KYORI_18 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_18, "", 100, False)
        Public Shared KYORI_19 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_19, "", 100, False)
        Public Shared KYORI_20 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_20, "", 100, False)
        Public Shared KYORI_21 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_21, "", 100, False)
        Public Shared KYORI_22 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_22, "", 100, False)
        Public Shared KYORI_23 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_23, "", 100, False)
        Public Shared KYORI_24 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_24, "", 100, False)
        Public Shared KYORI_25 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_25, "", 100, False)
        Public Shared KYORI_26 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_26, "", 100, False)
        Public Shared KYORI_27 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_27, "", 100, False)
        Public Shared KYORI_28 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_28, "", 100, False)
        Public Shared KYORI_29 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_29, "", 100, False)
        Public Shared KYORI_30 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_30, "", 100, False)
        Public Shared KYORI_31 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_31, "", 100, False)
        Public Shared KYORI_32 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_32, "", 100, False)
        Public Shared KYORI_33 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_33, "", 100, False)
        Public Shared KYORI_34 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_34, "", 100, False)
        Public Shared KYORI_35 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_35, "", 100, False)
        Public Shared KYORI_36 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_36, "", 100, False)
        Public Shared KYORI_37 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_37, "", 100, False)
        Public Shared KYORI_38 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_38, "", 100, False)
        Public Shared KYORI_39 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_39, "", 100, False)
        Public Shared KYORI_40 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_40, "", 100, False)
        Public Shared KYORI_41 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_41, "", 100, False)
        Public Shared KYORI_42 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_42, "", 100, False)
        Public Shared KYORI_43 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_43, "", 100, False)
        Public Shared KYORI_44 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_44, "", 100, False)
        Public Shared KYORI_45 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_45, "", 100, False)
        Public Shared KYORI_46 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_46, "", 100, False)
        Public Shared KYORI_47 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_47, "", 100, False)
        Public Shared KYORI_48 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_48, "", 100, False)
        Public Shared KYORI_49 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_49, "", 100, False)
        Public Shared KYORI_50 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_50, "", 100, False)
        Public Shared KYORI_51 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_51, "", 100, False)
        Public Shared KYORI_52 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_52, "", 100, False)
        Public Shared KYORI_53 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_53, "", 100, False)
        Public Shared KYORI_54 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_54, "", 100, False)
        Public Shared KYORI_55 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_55, "", 100, False)
        Public Shared KYORI_56 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_56, "", 100, False)
        Public Shared KYORI_57 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_57, "", 100, False)
        Public Shared KYORI_58 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_58, "", 100, False)
        Public Shared KYORI_59 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_59, "", 100, False)
        Public Shared KYORI_60 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_60, "", 100, False)
        Public Shared KYORI_61 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_61, "", 100, False)
        Public Shared KYORI_62 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_62, "", 100, False)
        Public Shared KYORI_63 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_63, "", 100, False)
        Public Shared KYORI_64 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_64, "", 100, False)
        Public Shared KYORI_65 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_65, "", 100, False)
        Public Shared KYORI_66 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_66, "", 100, False)
        Public Shared KYORI_67 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_67, "", 100, False)
        Public Shared KYORI_68 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_68, "", 100, False)
        Public Shared KYORI_69 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_69, "", 100, False)
        Public Shared KYORI_70 As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.KYORI_70, "", 100, False)
        '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
        'Public Shared FRRY_EXTC_10KG As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.FRRY_EXTC_10KG, "10㎏あたりの" & vbCrLf & "航送料", 100, True)

        '隠し項目
        Public Shared FRRY_EXTC_PART As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.FRRY_EXTC_PART, "1件あたりの" & vbCrLf & "航送料", 100, False)
        Public Shared SHIHARAI_TARIFF_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.SHIHARAI_TARIFF_CD_EDA, "支払タリフコード枝番", 100, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.UPD_FLG, "更新区分", 100, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.SYS_DEL_FLG_T, "削除フラグ", 100, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.SYS_UPD_DATE, "更新日", 100, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.SYS_UPD_TIME, "更新時刻", 100, False)
        Public Shared RECNO As SpreadColProperty = New SpreadColProperty(LMM550C.SprColumnIndex3.RECNO, "RECNO", 30, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '配置調整
        With Me._Frm.sprDetail3
            .Location = Me._Frm.sprDetail2.Location
            .Size = Me._Frm.sprDetail2.Size
            .Visible = False
        End With

        If (DispMode.INIT).Equals(Me._Frm.lblSituation.DispMode) = True Then
            '運賃タリフSpreadの初期化処理
            Call Me.InitUnchinTariffHDSpread()
        End If

        '運賃タリフ明細(距離刻み/運賃)の初期化処理
        Call Me.InitUnchinTariffDTLSpread()

    End Sub

    ''' <summary>
    ''' 運賃タリフスプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InitUnchinTariffHDSpread()        

        '運賃タリフSpreadの初期値設定
        Dim sprDetail As LMSpread = Me._Frm.sprDetail
        Dim dr As DataRow
        With sprDetail

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 18

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New LMM550G.sprDetailDef(), False)

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprDetailDef.DEF.ColNo + 1

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail)

            '**** 表示列 ****
            .SetCellStyle(0, sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, "S051", False))
            .SetCellStyle(0, sprDetailDef.NRS_BR_CD.ColNo, lbl)
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .SetCellStyle(0, sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
             .SetCellStyle(0, sprDetailDef.SHIHARAI_TARIFF_CD.ColNo, LMSpreadUtility.GetTextCell(sprDetail, InputControl.ALL_HANKAKU, 10, False))
            .SetCellStyle(0, sprDetailDef.DATA_TP_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_U010, False))
            .SetCellStyle(0, sprDetailDef.TABLE_TP_NM.ColNo, LMSpreadUtility.GetComboCellKbn(sprDetail, LMKbnConst.KBN_U011, False))
            .SetCellStyle(0, sprDetailDef.STR_DATE.ColNo, lbl)

            '**** 隠し列 ****
            .SetCellStyle(0, sprDetailDef.SHIHARAI_TARIFF_CD2.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SHIHARAI_TARIFF_REM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.DATA_TP.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.TABLE_TP.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_ENT_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_ENT_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_USER_NM.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, lbl)
            .SetCellStyle(0, sprDetailDef.SYS_DEL_FLG.ColNo, lbl)

            '初期値設定
            Call Me._ControlG.ClearControl(sprDetail)
            .SetCellValue(0, sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, sprDetailDef.DATA_TP_NM.ColNo, LMM550C.DATA_TP_KBN_01)

        End With

    End Sub

    ''' <summary>
    ''' 運賃タリフ明細(距離刻み/運賃/TypeA)スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitUnchinTariffDTLSpread(Optional ByVal EventShubetsu As Integer = 0)

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        '運賃タリフ明細(距離刻み/運賃)Spreadの初期値設定
        Dim sprDetail2 As LMSpread = Me._Frm.sprDetail2


        With sprDetail2

            If _BeCnt = 0 Then
                'スプレッドの行をクリア
                .CrearSpread()
                '列数設定
                .ActiveSheet.ColumnCount = 89

                .SetColProperty(New LMM550G.sprDetailDef2(), False)
                .SetColProperty(New sprDetailDef2(), lgm.Selector({"距離(km)", "Distance(km)", "거리(km)", "Distance(km)"}), 6, 5)

                '距離刻みの値クリア
                Call Me.ClearSpreadControl(True)
            Else

                .CrearSpread()

                .SetColProperty(New LMM550G.sprDetailDef2(), False)
                .SetColProperty(New sprDetailDef2(), lgm.Selector({"距離(km)", "Distance(km)", "거리(km)", "Distance(km)"}), 6, (_BeCnt))

            End If

            Dim vis As Boolean = False
            Dim visColCnt As Integer = 0

            If _BeCnt = 0 Then
                visColCnt = 5
                Me._Frm.lblDefAddCnt.TextValue = visColCnt.ToString
            Else
                visColCnt = _BeCnt
            End If

            For i As Integer = 0 To 90
                If LMM550C.SprColumnIndex2.KYORI_1 <= i And i <= LMM550C.SprColumnIndex2.KYORI_70 Then
                    If i <= LMM550C.SprColumnIndex2.T_SIZE + visColCnt Then
                        .ActiveSheet.Columns(i).Visible = True
                    Else
                        .ActiveSheet.Columns(i).Visible = False
                    End If

                End If

            Next

            '行番号の再設定
            .ReRowNumber()

        End With

        'TypeBも同じタイミングで初期設定
        Call Me.InitUnchinTariffDTLSpread_B()

    End Sub

    ''' <summary>
    ''' 運賃タリフ明細(距離刻み/運賃/TypeB)スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitUnchinTariffDTLSpread_B(Optional ByVal EventShubetsu As Integer = 0)

        '運賃タリフ明細(距離刻み/運賃)Spreadの初期値設定
        Dim sprDetail3 As LMSpread = Me._Frm.sprDetail3

        With sprDetail3

            Dim title As String = "重量or個数"
            If LMM550C.TABLE_TP_KBN_08.Equals(Me._SpreadTypeSub) Then
                title = "重量"
            ElseIf LMM550C.TABLE_TP_KBN_09.Equals(Me._SpreadTypeSub) Then
                title = "個数"
            End If

            If _BeCnt = 0 Then
                'スプレッドの行をクリア
                .CrearSpread()
                '列数設定
                .ActiveSheet.ColumnCount = 84

                .SetColProperty(New LMM550G.sprDetailDef3(), False)
                .SetColProperty(New sprDetailDef3(), title, 7, 5)

                '距離刻みの値クリア
                Call Me.ClearSpreadControl(True)
            Else
                .CrearSpread()

                .SetColProperty(New LMM550G.sprDetailDef3(), False)
                .SetColProperty(New sprDetailDef3(), title, 7, (_BeCnt))
            End If

            Dim vis As Boolean = False
            Dim visColCnt As Integer = 0

            If _BeCnt = 0 Then
                visColCnt = 5
                Me._Frm.lblDefAddCnt.TextValue = visColCnt.ToString
            Else
                visColCnt = _BeCnt
            End If

            For i As Integer = 0 To 90
                If LMM550C.SprColumnIndex3.KYORI_1 <= i And i <= LMM550C.SprColumnIndex3.KYORI_70 Then
                    If i <= LMM550C.SprColumnIndex3.DEST_JIS_CD + visColCnt Then
                        .ActiveSheet.Columns(i).Visible = True
                    Else
                        .ActiveSheet.Columns(i).Visible = False
                    End If
                End If
            Next

            '行番号の再設定
            .ReRowNumber()

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM550F)

        With frm.sprDetail

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
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
            Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                ''セルスタイル設定
                .SetCellStyle(i, LMM550G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.DATA_TP_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.TABLE_TP_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.STR_DATE.ColNo, sDate)
                '隠し項目
                .SetCellStyle(i, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD2.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SHIHARAI_TARIFF_REM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.DATA_TP.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.TABLE_TP.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM550G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM550G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD.ColNo, dr.Item("SHIHARAI_TARIFF_CD").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.DATA_TP_NM.ColNo, dr.Item("DATA_TP_NM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.TABLE_TP_NM.ColNo, dr.Item("TABLE_TP_NM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.STR_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("STR_DATE").ToString()))
                .SetCellValue(i, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD2.ColNo, dr.Item("SHIHARAI_TARIFF_CD2").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SHIHARAI_TARIFF_REM.ColNo, dr.Item("SHIHARAI_TARIFF_REM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.DATA_TP.ColNo, dr.Item("DATA_TP").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.TABLE_TP.ColNo, dr.Item("TABLE_TP").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM550G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With


    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2(ByVal dt As DataTable, ByVal eventShubetsu As LMM550C.EventShubetsu, Optional ByVal NrsBrcd As String = "", Optional ByVal TariffCd As String = "", _
                          Optional ByVal DataTp As String = "", Optional ByVal StrDate As String = "", Optional ByVal TableTp As String = "")

        'TypeBの場合
        If LMM550C.SpreadType.B.Equals(Me._SpreadType) Then
            Me.SetSpread2_B(dt, eventShubetsu, NrsBrcd, TariffCd, DataTp, StrDate, TableTp)
            Return
        End If

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet
        Dim sort As String = String.Empty
        Dim sql As String = String.Empty
        Dim copydt As New DataTable

        '重量のデータ型変換
        copydt = dt.Copy
        copydt.Columns.Add("WT_LV_NUM", Type.GetType("System.Decimal"), "Convert(WT_LV,'System.Decimal')")

        'イベント種別によってソートと抽出条件を変える
        '①ダブルクリック時
        If eventShubetsu.Equals(LMM550C.EventShubetsu.DCLICK) = True Then
            sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                          , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                          , "AND STR_DATE = '", StrDate, "' " _
                                          , "AND (DATA_TP = '", DataTp, "' " _
                                          , "OR  DATA_TP = '", LMM550C.TABLE_TP_KBN_00, "') " _
                                          )
            Select Case TableTp
                Case LMM550C.TABLE_TP_KBN_00, LMM550C.TABLE_TP_KBN_02, LMM550C.TABLE_TP_KBN_03, _
                     LMM550C.TABLE_TP_KBN_04, LMM550C.TABLE_TP_KBN_05, LMM550C.TABLE_TP_KBN_07
                    sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,WT_LV_NUM ASC,CAR_TP ASC,SHIHARAI_TARIFF_CD_EDA ASC"
                Case LMM550C.TABLE_TP_KBN_01, LMM550C.TABLE_TP_KBN_06
                    sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,CAR_TP ASC,WT_LV_NUM ASC,SHIHARAI_TARIFF_CD_EDA ASC"
            End Select
            '②列挿入・列削除・行追加・行削除時
        ElseIf eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
            OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True _
            OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.DEL_T) = True _
            OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.INS_T) = True Then
            sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "'")
            '③その他
        Else
            sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "'")
            Select Case TableTp
                Case LMM550C.TABLE_TP_KBN_00, LMM550C.TABLE_TP_KBN_02, LMM550C.TABLE_TP_KBN_03, _
                     LMM550C.TABLE_TP_KBN_04, LMM550C.TABLE_TP_KBN_05, LMM550C.TABLE_TP_KBN_07
                    sort = "NRS_BR_CD ASC,DATA_TP ASC,TABLE_TP ASC,WT_LV_NUM ASC,CAR_TP ASC,SHIHARAI_TARIFF_CD_EDA ASC"
                Case LMM550C.TABLE_TP_KBN_01, LMM550C.TABLE_TP_KBN_06
                    sort = "NRS_BR_CD ASC,DATA_TP ASC,TABLE_TP ASC,CAR_TP ASC,WT_LV_NUM ASC,SHIHARAI_TARIFF_CD_EDA ASC"
            End Select
        End If

        Dim tmpDatarow2() As DataRow = copydt.Select(sql, sort)

        With spr2

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = tmpDatarow2.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'イベント種別によって追加列数の初期化を行う
            If eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
               OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True Then
                _BeCnt = CType(Me._Frm.lblAddCnt.TextValue, Integer)
            Else
                _BeCnt = 0
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, True)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
            Dim sChk As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, True)
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(spr2, 0, 999999999, True, 0, True, ",")
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(spr2, 0.0, 999999999.999, True, 3, True, ",")
            Dim sCmbS As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr2, LMKbnConst.KBN_S012, True)
            Dim sCmbT As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr2, LMKbnConst.KBN_T010, True)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            'イベント種別によって追加列数のカウントを行う
            If eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
               OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True Then
            ElseIf eventShubetsu.Equals(LMM550C.EventShubetsu.DCLICK) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                For t As Integer = 1 To 70
                    If String.IsNullOrEmpty(tmpDatarow2(0).Item("KYORI_" & t).ToString()) = False _
                       AndAlso CType("0", Integer).Equals(CType(tmpDatarow2(0).Item("KYORI_" & t), Integer)) = False _
                       AndAlso CType("0.000", Integer).Equals(CType(tmpDatarow2(0).Item("KYORI_" & t), Integer)) = False Then
                        _BeCnt = t
                    End If
                Next
                '距離刻みが全て0の場合はデフォルトで５列表示
                If _BeCnt = 0 Then
                    _BeCnt = _BeCnt + 5
                End If
            Else
                For i As Integer = 0 To 90
                    If LMM550C.SprColumnIndex2.KYORI_1 <= i And i <= LMM550C.SprColumnIndex2.KYORI_70 Then
                        If .ActiveSheet.Columns(i).Visible = True Then
                            _BeCnt = _BeCnt + 1
                        End If
                    End If
                Next
            End If

            '運賃タリフ明細(距離刻み/運賃)の初期化処理
            Call Me.InitUnchinTariffDTLSpread()

            Dim rowCnt As Integer = 0
            For i As Integer = 1 To lngcnt

                dr = tmpDatarow2(i - 1)

                '削除データでない場合のみ、各レコードのSYS_DEL_FLGによって表示・非表示を行う。
                If (RecordStatus.DELETE_REC).Equals(Me._Frm.lblSituation.RecordStatus) = False Then
                    'SYS_DEL_FLGが'1'のものは表示しない
                    If LMConst.FLG.ON.Equals(dr.Item("SYS_DEL_FLG").ToString()) = True Then
                        Continue For
                    End If
                End If

                '距離刻みのデータ(データタイプ='00')の場合、行追加なし
                If (LMM550C.DATA_TP_KBN_00).Equals(dr.Item("DATA_TP").ToString()) = True Then
                    '設定する行数を設定
                    rowCnt = 1
                Else
                    '設定する行数を設定
                    rowCnt = .ActiveSheet.Rows.Count
                    '行追加
                    .ActiveSheet.AddRows(rowCnt, 1)
                End If

                If rowCnt = 1 Then

                    'セルスタイル設定
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_TIME.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.RECNO.ColNo, sLabel)
                    'セルに値を設定
                    For t As Integer = 1 To _BeCnt
                        .SetCellStyle(rowCnt, (5 + t), sNumber2)
                        .SetCellValue(rowCnt, (5 + t), dr.Item("KYORI_" & t).ToString())
                    Next
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, dr.Item("SHIHARAI_TARIFF_CD_EDA").ToString())
                    'Excelから取り込んだデータの場合、更新区分="0"(新規)を設定
                    If eventShubetsu.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.UPD_FLG.ColNo, LMM550C.FLG.OFF)
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                    End If
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.RECNO.ColNo, Me.SetZeroData(rowCnt.ToString(), LMM550C.MAEZERO))

                End If

                If rowCnt <> 1 Then

                    'セルスタイル設定
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.DEF.ColNo, sDEF)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.WT_LV.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.CAR_TP.ColNo, sCmbS)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.NB.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.QT.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.T_SIZE.ColNo, sCmbT)
                    For t As Integer = 1 To _BeCnt
                        .SetCellStyle(rowCnt, (5 + t), sNumber2)
                    Next
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.CITY_EXTC_A.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.CITY_EXTC_B.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.WINT_EXTC_A.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.WINT_EXTC_B.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.RELY_EXTC.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.INSU.ColNo, sNumber1)
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    '.SetCellStyle(rowCnt, LMM550G.sprDetailDef2.FRRY_EXTC_10KG.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.FRRY_EXTC_PART.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_TIME.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef2.RECNO.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)

                    '重量
                    If (LMM550C.TABLE_TP_KBN_00).Equals(Me._Frm.cmbTableTp.SelectedValue) = True _
                       OrElse (LMM550C.TABLE_TP_KBN_03).Equals(Me._Frm.cmbTableTp.SelectedValue) = True _
                       OrElse (LMM550C.TABLE_TP_KBN_07).Equals(Me._Frm.cmbTableTp.SelectedValue) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.WT_LV.ColNo, dr.Item("WT_LV").ToString())
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.WT_LV.ColNo, 0.ToString())
                    End If

                    '車種
                    If (LMM550C.TABLE_TP_KBN_01).Equals(Me._Frm.cmbTableTp.SelectedValue) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.CAR_TP.ColNo, dr.Item("CAR_TP").ToString())
                    End If

                    '数量
                    If (LMM550C.TABLE_TP_KBN_02).Equals(Me._Frm.cmbTableTp.SelectedValue) = True _
                       OrElse (LMM550C.TABLE_TP_KBN_05).Equals(Me._Frm.cmbTableTp.SelectedValue) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.NB.ColNo, dr.Item("WT_LV").ToString())
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.NB.ColNo, 0.ToString())
                    End If

                    '個数
                    If (LMM550C.TABLE_TP_KBN_04).Equals(Me._Frm.cmbTableTp.SelectedValue) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.QT.ColNo, dr.Item("WT_LV").ToString())
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.QT.ColNo, 0.ToString())
                    End If

                    '宅急便サイズ
                    If (LMM550C.TABLE_TP_KBN_06).Equals(Me._Frm.cmbTableTp.SelectedValue) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.T_SIZE.ColNo, dr.Item("CAR_TP").ToString())
                    End If
                    For t As Integer = 1 To _BeCnt
                        .SetCellValue(rowCnt, (5 + t), dr.Item("KYORI_" & t).ToString())
                    Next
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.CITY_EXTC_A.ColNo, dr.Item("CITY_EXTC_A").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.CITY_EXTC_B.ColNo, dr.Item("CITY_EXTC_B").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.WINT_EXTC_A.ColNo, dr.Item("WINT_EXTC_A").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.WINT_EXTC_B.ColNo, dr.Item("WINT_EXTC_B").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.RELY_EXTC.ColNo, dr.Item("RELY_EXTC").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.INSU.ColNo, dr.Item("INSU").ToString())
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    '.SetCellValue(rowCnt, LMM550G.sprDetailDef2.FRRY_EXTC_10KG.ColNo, dr.Item("FRRY_EXTC_10KG").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.FRRY_EXTC_PART.ColNo, dr.Item("FRRY_EXTC_PART").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo, dr.Item("SHIHARAI_TARIFF_CD_EDA").ToString())
                    'Excelから取り込んだデータの場合、更新区分="0"(新規)を設定
                    If eventShubetsu.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.UPD_FLG.ColNo, LMM550C.FLG.OFF)
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef2.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                    End If
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef2.RECNO.ColNo, Me.SetZeroData(rowCnt.ToString(), LMM550C.MAEZERO))

                End If

            Next

            '前の列数の保持
            _AfCnt = _BeCnt

            '列挿入・列削除後の列数を保持
            Me._Frm.lblDefAddCnt.TextValue = _BeCnt.ToString

            '行番号の再設定
            .ReRowNumber()

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細/TypeB)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2_B(ByVal dt As DataTable, ByVal eventShubetsu As LMM550C.EventShubetsu, Optional ByVal NrsBrcd As String = "", Optional ByVal TariffCd As String = "",
                          Optional ByVal DataTp As String = "", Optional ByVal StrDate As String = "", Optional ByVal TableTp As String = "")

        Dim spr3 As LMSpread = Me._Frm.sprDetail3
        Dim dtOut As New DataSet
        Dim sort As String = String.Empty
        Dim sql As String = String.Empty
        Dim copydt As New DataTable

        '重量のデータ型変換
        copydt = dt.Copy
        ''''copydt.Columns.Add("WT_LV_NUM", Type.GetType("System.Decimal"), "Convert(WT_LV,'System.Decimal')")

        'イベント種別によってソートと抽出条件を変える
        '①ダブルクリック時
        If eventShubetsu.Equals(LMM550C.EventShubetsu.DCLICK) = True Then
            sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                          , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                          , "AND STR_DATE = '", StrDate, "' " _
                                          , "AND (DATA_TP = '", DataTp, "' " _
                                          , "OR  DATA_TP = '", LMM550C.TABLE_TP_KBN_00, "') "
                                          )
            sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,ORIG_JIS_CD ASC,SHIHARAI_TARIFF_CD_EDA ASC"
            '②列挿入・列削除・行追加・行削除時
        ElseIf eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
            OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True _
            OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.DEL_T) = True _
            OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.INS_T) = True Then
            sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "'")
            '③その他
        Else
            sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "'")
            sort = "NRS_BR_CD ASC,DATA_TP ASC,TABLE_TP ASC,ORIG_JIS_CD ASC,DEST_JIS_CD ASC,SHIHARAI_TARIFF_CD_EDA ASC"
        End If

        Dim tmpDatarow3() As DataRow = copydt.Select(sql, sort)

        With spr3

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = tmpDatarow3.Length
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'イベント種別によって追加列数の初期化を行う
            If eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
               OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True Then
                _BeCnt = CType(Me._Frm.lblAddCnt.TextValue, Integer)
            Else
                _BeCnt = 0
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr3, True)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr3, CellHorizontalAlignment.Left)
            Dim sChk As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr3, True)
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(spr3, 0, 999999999, True, 0, True, ",")
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(spr3, 0.0, 999999999.999, True, 3, True, ",")
            Dim sCmbS As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr3, LMKbnConst.KBN_S012, True)
            Dim sCmbT As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr3, LMKbnConst.KBN_T010, True)
            Dim sText7 As StyleInfo = LMSpreadUtility.GetTextCell(spr3, InputControl.HAN_NUM_ALPHA, 7, True)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            'イベント種別によって追加列数のカウントを行う
            If eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
               OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True Then
            ElseIf eventShubetsu.Equals(LMM550C.EventShubetsu.DCLICK) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                For t As Integer = 1 To 70
                    If String.IsNullOrEmpty(tmpDatarow3(0).Item("KYORI_" & t).ToString()) = False _
                       AndAlso CType("0", Integer).Equals(CType(tmpDatarow3(0).Item("KYORI_" & t), Integer)) = False _
                       AndAlso CType("0.000", Integer).Equals(CType(tmpDatarow3(0).Item("KYORI_" & t), Integer)) = False Then
                        _BeCnt = t
                    End If
                Next
                '重量or個数刻みが全て0の場合はデフォルトで５列表示
                If _BeCnt = 0 Then
                    _BeCnt = _BeCnt + 5
                End If
            Else
                For i As Integer = 0 To 90
                    If LMM550C.SprColumnIndex3.KYORI_1 <= i And i <= LMM550C.SprColumnIndex3.KYORI_70 Then
                        If .ActiveSheet.Columns(i).Visible = True Then
                            _BeCnt = _BeCnt + 1
                        End If
                    End If
                Next
            End If

            '運賃タリフ明細(距離刻み/運賃)の初期化処理
            Call Me.InitUnchinTariffDTLSpread()

            Dim rowCnt As Integer = 0
            For i As Integer = 1 To lngcnt

                dr = tmpDatarow3(i - 1)

                '削除データでない場合のみ、各レコードのSYS_DEL_FLGによって表示・非表示を行う。
                If (RecordStatus.DELETE_REC).Equals(Me._Frm.lblSituation.RecordStatus) = False Then
                    'SYS_DEL_FLGが'1'のものは表示しない
                    If LMConst.FLG.ON.Equals(dr.Item("SYS_DEL_FLG").ToString()) = True Then
                        Continue For
                    End If
                End If

                '重量or個数刻みのデータ(データタイプ='00')の場合、行追加なし
                If (LMM550C.DATA_TP_KBN_00).Equals(dr.Item("DATA_TP").ToString()) = True Then
                    '設定する行数を設定
                    rowCnt = 1
                Else
                    '設定する行数を設定
                    rowCnt = .ActiveSheet.Rows.Count
                    '行追加
                    .ActiveSheet.AddRows(rowCnt, 1)
                End If

                If rowCnt = 1 Then

                    'セルスタイル設定
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_TIME.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.RECNO.ColNo, sLabel)
                    'セルに値を設定
                    For t As Integer = 1 To _BeCnt
                        .SetCellStyle(rowCnt, (6 + t), sNumber2)
                        .SetCellValue(rowCnt, (6 + t), dr.Item("KYORI_" & t).ToString())
                    Next
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, dr.Item("SHIHARAI_TARIFF_CD_EDA").ToString())
                    'Excelから取り込んだデータの場合、更新区分="0"(新規)を設定
                    If eventShubetsu.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef3.UPD_FLG.ColNo, LMM550C.FLG.OFF)
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef3.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                    End If
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.RECNO.ColNo, Me.SetZeroData(rowCnt.ToString(), LMM550C.MAEZERO))

                End If

                If rowCnt <> 1 Then

                    'セルスタイル設定
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.DEF.ColNo, sDEF)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.ORIG_KEN_N.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.ORIG_CITY_N.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.ORIG_JIS_CD.ColNo, sText7)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.DEST_KEN_N.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.DEST_CITY_N.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.DEST_JIS_CD.ColNo, sText7)
                    For t As Integer = 1 To _BeCnt
                        .SetCellStyle(rowCnt, (6 + t), sNumber2)
                    Next
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    '.SetCellStyle(rowCnt, LMM550G.sprDetailDef3.FRRY_EXTC_10KG.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.FRRY_EXTC_PART.ColNo, sNumber1)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_DATE.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_TIME.ColNo, sLabel)
                    .SetCellStyle(rowCnt, LMM550G.sprDetailDef3.RECNO.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.DEF.ColNo, LMConst.FLG.OFF)

                    '起点/都道府県名
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.ORIG_KEN_N.ColNo, dr.Item("ORIG_KEN_N").ToString())

                    '起点/市区町村名
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.ORIG_CITY_N.ColNo, dr.Item("ORIG_CITY_N").ToString())

                    '起点/JISコード
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.ORIG_JIS_CD.ColNo, dr.Item("ORIG_JIS_CD").ToString())

                    '着点/都道府県名
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.DEST_KEN_N.ColNo, dr.Item("DEST_KEN_N").ToString())

                    '着点/市区町村名
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.DEST_CITY_N.ColNo, dr.Item("DEST_CITY_N").ToString())

                    '着点/JISコード
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.DEST_JIS_CD.ColNo, dr.Item("DEST_JIS_CD").ToString())

                    For t As Integer = 1 To _BeCnt
                        .SetCellValue(rowCnt, (6 + t), dr.Item("KYORI_" & t).ToString())
                    Next

                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    '.SetCellValue(rowCnt, LMM550G.sprDetailDef3.FRRY_EXTC_10KG.ColNo, dr.Item("FRRY_EXTC_10KG").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.FRRY_EXTC_PART.ColNo, dr.Item("FRRY_EXTC_PART").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo, dr.Item("SHIHARAI_TARIFF_CD_EDA").ToString())
                    'Excelから取り込んだデータの場合、更新区分="0"(新規)を設定
                    If eventShubetsu.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef3.UPD_FLG.ColNo, LMM550C.FLG.OFF)
                    Else
                        .SetCellValue(rowCnt, LMM550G.sprDetailDef3.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                    End If
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                    .SetCellValue(rowCnt, LMM550G.sprDetailDef3.RECNO.ColNo, Me.SetZeroData(rowCnt.ToString(), LMM550C.MAEZERO))

                End If

            Next

            '前の列数の保持
            _AfCnt = _BeCnt

            '列挿入・列削除後の列数を保持
            Me._Frm.lblDefAddCnt.TextValue = _BeCnt.ToString

            '行番号の再設定
            .ReRowNumber()

            .ResumeLayout(True)

        End With

    End Sub

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

        If LMM550C.SpreadType.A.Equals(Me._SpreadType) Then
            'TypeA
            Me.sprSortColumnCommand(Me._Frm.sprDetail2, LMM550G.sprDetailDef2.RECNO.ColNo)
        Else
            'TypeB
            Me.sprSortColumnCommand(Me._Frm.sprDetail3, LMM550G.sprDetailDef3.RECNO.ColNo)
        End If

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

#Region "Spread(ADD/DEL)"

    ''' <summary>
    ''' MAX支払タリフコード枝番の値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetUnchinCdEdaDataSet(ByVal ds As DataSet, ByVal eventShubetsu As LMM550C.EventShubetsu) As Boolean

        '新規/複写の場合
        If ds Is Nothing Then
            ds = New LMM550DS()
        End If

        With Me._Frm

            '新規/複写の場合
            Dim max As Integer = ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).Rows.Count
            Dim insMRows As DataRow = ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).NewRow

            '複写の場合
            If LMM550C.SpreadType.A.Equals(Me._SpreadType) Then
                'TypeA
                If (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True Then
                    Dim RowCnt As Integer = Me._Frm.sprDetail2.ActiveSheet.Rows.Count - 1
                    If 1 < RowCnt Then
                        .lblMaxEda.TextValue = Me._Frm.sprDetail2.ActiveSheet.Cells(RowCnt, LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo).Text
                    End If
                End If
            Else
                'TypeB
                If (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True Then
                    Dim RowCnt As Integer = Me._Frm.sprDetail3.ActiveSheet.Rows.Count - 1
                    If 1 < RowCnt Then
                        .lblMaxEda.TextValue = Me._Frm.sprDetail3.ActiveSheet.Cells(RowCnt, LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo).Text
                    End If
                End If
            End If

            '支払タリフコード枝番の最大値を求める
            Dim oldMaxUnchinCd As String = String.Empty
            If (0).Equals(max) = True Then
                If String.IsNullOrEmpty(.lblMaxEda.TextValue) = True Then
                    oldMaxUnchinCd = "0"
                Else
                    oldMaxUnchinCd = .lblMaxEda.TextValue
                End If
            Else
                If ("").Equals(ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).Rows(max - 1).Item("SHIHARAI_TARIFF_MAXEDA").ToString()) = True Then
                    oldMaxUnchinCd = "0"
                Else
                    oldMaxUnchinCd = ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).Rows(max - 1).Item("SHIHARAI_TARIFF_MAXEDA").ToString()
                End If
            End If

            Dim newMaxUnchinCd As String = String.Empty
            '現在のMAX支払タリフコード枝番がMAX値を超えている場合は採番後の桁数を4桁にする
            If ("999").Equals(oldMaxUnchinCd) = False Then
                newMaxUnchinCd = _ControlG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUnchinCd) + 1), 3)
            Else
                newMaxUnchinCd = _ControlG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUnchinCd) + 1), 4)
            End If

            '枝番の限界値、チェック
            If Me._ControlV.IsMaxChk(Convert.ToInt32(newMaxUnchinCd), 999, "支払タリフコード枝番") = False Then
                '処理終了アクション
                Return False
            End If

            '支払タリフコード枝番の更新
            insMRows("SHIHARAI_TARIFF_MAXEDA") = newMaxUnchinCd

            'データセットに追加
            ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).Rows.Add(insMRows)

            '画面のMAX支払タリフコード枝番に設定
            .lblMaxEda.TextValue = newMaxUnchinCd

        End With

        Return True

    End Function

#End Region

#End Region 'Spread

#Region "その他画面制御"

    ''' <summary>
    ''' 運賃明細Spreadのロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks>運賃明細Spread列の活性化・非活性化の制御を行う。</remarks>
    Friend Sub ChangeLockControl1(ByVal actionType As LMM550C.EventShubetsu)

        'TypeBの場合
        If LMM550C.SpreadType.B.Equals(Me._SpreadType) Then
            Me.ChangeLockControl1_B(actionType)
            Return
        End If

        Dim lock As Boolean = True
        Dim unLock As Boolean = False
        Dim WtLv As String = String.Empty
        Dim CarTp As String = String.Empty
        Dim NB As String = String.Empty
        Dim QT As String = String.Empty
        Dim Tsize As String = String.Empty

        With Me._Frm

            'スプレッド(下部)の重量・車種・個数・数量・宅急便サイズを全てロックする。
            Me.SetLockBottomSpreadControl(True)

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '①計算種別が"00"(重量・距離)/"03"(重量建て)/"07"(重量・県)の場合、重量をロック解除
            If .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_00 _
               OrElse .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_03 _
                OrElse .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_07 Then
                WtLv = LMConst.FLG.ON
            End If

            '②計算種別が"01"(車種・距離)の場合、車種をロック解除
            If .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_01 Then
                CarTp = LMConst.FLG.ON
            End If

            '③計算種別が"02"(個数建て)/"05"(個数/・県)の場合、個数をロック解除
            If .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_02 _
                OrElse .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_05 Then
                NB = LMConst.FLG.ON
            End If

            '④計算種別が"04"(数量建て)の場合、数量をロック解除
            If .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_04 Then                
                QT = LMConst.FLG.ON
            End If

            '⑤計算種別が"06"(宅急便サイズ・県)の場合、数量をロック解除
            If .cmbTableTp.SelectedValue.ToString() = LMM550C.TABLE_TP_KBN_06 Then
                Tsize = LMConst.FLG.ON
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail2, unLock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left)
            Dim sChk As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail2, unLock)
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail2, 0, 999999999, unLock, 0, True, ",")
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail2, 0.0, 999999999.999, unLock, 3, True, ",")
            Dim sCmbS As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail2, LMKbnConst.KBN_S012, unLock)
            Dim sCmbT As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail2, LMKbnConst.KBN_T010, unLock)

            Dim colmax As Integer = _AfCnt
            Dim max As Integer = .sprDetail2.ActiveSheet.Rows.Count
            Dim UpdFlg As String = String.Empty

            '①新規モードで行削除ボタン押下時 or ②Excel取込ボタン押下時、運賃明細Spreadの明細行の有無によって計算種別のロック制御を行う。
            If (RecordStatus.NEW_REC.Equals(Me._Frm.lblSituation.RecordStatus) = True _
               AndAlso actionType.Equals(LMM550C.EventShubetsu.DEL_T) = True) _
               OrElse actionType.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                If max = 2 Then
                    Call Me._ControlG.LockComb(.cmbTableTp, unLock)      '運賃明細Spreadの明細行がない場合、計算種別のロック解除。
                Else
                    Call Me._ControlG.LockComb(.cmbTableTp, lock)        '運賃明細Spreadの明細行がある場合、計算種別のロック。
                End If
            End If

            For i As Integer = 3 To max

                '①正常モード以外の場合 or ②正常モードかつデータタイプ="00"(距離刻み)の場合 or ③行追加ボタン押下の場合 or ④行削除ボタン押下の場合 or ⑤列削除ボタン押下の場合 or ⑥列挿入ボタン押下の場合、
                '重量・車種・個数・数量・宅急便サイズのロック制御をする
                If RecordStatus.NOMAL_REC.Equals(Me._Frm.lblSituation.RecordStatus) = False _
                   OrElse (RecordStatus.NOMAL_REC.Equals(Me._Frm.lblSituation.RecordStatus) = True _
                   AndAlso (LMM550C.DATA_TP_KBN_00).Equals(.cmbDataTp.SelectedValue.ToString())) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.INS_T) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.DEL_T) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.COLADD) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.COLDEL) = True Then

                    UpdFlg = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells((i - 1), LMM550C.SprColumnIndex2.UPD_FLG))
                    '対象行の更新区分＝'0'(新規)レコードの場合のみロック解除
                    If LMM550C.FLG.OFF.Equals(UpdFlg) = True Then
                        If (LMConst.FLG.ON).Equals(WtLv) = True Then
                            .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.WT_LV.ColNo, sNumber1)
                        End If
                        If (LMConst.FLG.ON).Equals(CarTp) = True Then
                            .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.CAR_TP.ColNo, sCmbS)
                        End If
                        If (LMConst.FLG.ON).Equals(NB) = True Then
                            .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.NB.ColNo, sNumber1)
                        End If
                        If (LMConst.FLG.ON).Equals(QT) = True Then
                            .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.QT.ColNo, sNumber1)
                        End If
                        If (LMConst.FLG.ON).Equals(Tsize) = True Then
                            .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.T_SIZE.ColNo, sCmbT)
                        End If
                    End If
                End If

                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.DEF.ColNo, sDEF)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.KYORI_1.ColNo, sNumber2)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.KYORI_2.ColNo, sNumber2)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.KYORI_3.ColNo, sNumber2)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.KYORI_4.ColNo, sNumber2)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.KYORI_5.ColNo, sNumber2)
                If colmax <> 0 Then
                    For t As Integer = 1 To colmax
                        .sprDetail2.SetCellStyle((i - 1), (10 + t), sNumber2)
                    Next
                End If
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.CITY_EXTC_A.ColNo, sNumber1)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.CITY_EXTC_B.ColNo, sNumber1)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.WINT_EXTC_A.ColNo, sNumber1)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.WINT_EXTC_B.ColNo, sNumber1)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.RELY_EXTC.ColNo, sNumber1)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.INSU.ColNo, sNumber1)
                '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                '.sprDetail2.SetCellStyle((i - 1), sprDetailDef2.FRRY_EXTC_10KG.ColNo, sNumber1)
                .sprDetail2.SetCellStyle((i - 1), sprDetailDef2.FRRY_EXTC_PART.ColNo, sNumber1)

            Next

            Exit Sub

        End With

    End Sub

    ''' <summary>
    ''' 運賃明細Spread(TypeB)のロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks>運賃明細Spread列の活性化・非活性化の制御を行う。</remarks>
    Friend Sub ChangeLockControl1_B(ByVal actionType As LMM550C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False
        Dim Orig As String = String.Empty
        Dim Dest As String = String.Empty

        With Me._Frm

            'スプレッド(下部)を全てロックする
            Me.SetLockBottomSpreadControl(True)

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '起点/JISコード・着点/JISコードをロック解除
            Orig = LMConst.FLG.ON
            Dest = LMConst.FLG.ON

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail3, unLock)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail3, CellHorizontalAlignment.Left)
            Dim sChk As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail3, unLock)
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail3, 0, 999999999, unLock, 0, True, ",")
            Dim sNumber2 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail3, 0.0, 999999999.999, unLock, 3, True, ",")
            Dim sCmbS As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail3, LMKbnConst.KBN_S012, unLock)
            Dim sCmbT As StyleInfo = LMSpreadUtility.GetComboCellKbn(.sprDetail3, LMKbnConst.KBN_T010, unLock)
            Dim sText7 As StyleInfo = LMSpreadUtility.GetTextCell(.sprDetail3, InputControl.HAN_NUM_ALPHA, 7, unLock)

            Dim colmax As Integer = _AfCnt
            Dim max As Integer = .sprDetail3.ActiveSheet.Rows.Count
            Dim UpdFlg As String = String.Empty

            '①新規モードで行削除ボタン押下時 or ②Excel取込ボタン押下時、運賃明細Spreadの明細行の有無によって計算種別のロック制御を行う。
            If (RecordStatus.NEW_REC.Equals(Me._Frm.lblSituation.RecordStatus) = True _
               AndAlso actionType.Equals(LMM550C.EventShubetsu.DEL_T) = True) _
               OrElse actionType.Equals(LMM550C.EventShubetsu.EXCELINPUT) = True Then
                If max = 2 Then
                    Call Me._ControlG.LockComb(.cmbTableTp, unLock)      '運賃明細Spreadの明細行がない場合、計算種別のロック解除。
                Else
                    Call Me._ControlG.LockComb(.cmbTableTp, lock)        '運賃明細Spreadの明細行がある場合、計算種別のロック。
                End If
            End If

            For i As Integer = 3 To max

                '①正常モード以外の場合 or ②正常モードかつデータタイプ="00"(距離刻み)の場合 or ③行追加ボタン押下の場合 or ④行削除ボタン押下の場合 or ⑤列削除ボタン押下の場合 or ⑥列挿入ボタン押下の場合、
                '起点/JISコード・着点/JISコードのロック制御をする
                If RecordStatus.NOMAL_REC.Equals(Me._Frm.lblSituation.RecordStatus) = False _
                   OrElse (RecordStatus.NOMAL_REC.Equals(Me._Frm.lblSituation.RecordStatus) = True _
                   AndAlso (LMM550C.DATA_TP_KBN_00).Equals(.cmbDataTp.SelectedValue.ToString())) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.INS_T) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.DEL_T) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.COLADD) = True _
                   OrElse actionType.Equals(LMM550C.EventShubetsu.COLDEL) = True Then

                    UpdFlg = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells((i - 1), LMM550C.SprColumnIndex3.UPD_FLG))
                    '対象行の更新区分＝'0'(新規)レコードの場合のみロック解除
                    If LMM550C.FLG.OFF.Equals(UpdFlg) = True Then
                        If (LMConst.FLG.ON).Equals(Orig) = True Then
                            .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.ORIG_JIS_CD.ColNo, sText7)
                        End If
                        If (LMConst.FLG.ON).Equals(Dest) = True Then
                            .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.DEST_JIS_CD.ColNo, sText7)
                        End If
                    End If
                End If

                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.DEF.ColNo, sDEF)
                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.KYORI_1.ColNo, sNumber2)
                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.KYORI_2.ColNo, sNumber2)
                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.KYORI_3.ColNo, sNumber2)
                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.KYORI_4.ColNo, sNumber2)
                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.KYORI_5.ColNo, sNumber2)
                If colmax <> 0 Then
                    For t As Integer = 1 To colmax
                        .sprDetail3.SetCellStyle((i - 1), (11 + t), sNumber2)
                    Next
                End If
                '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                '.sprDetail3.SetCellStyle((i - 1), sprDetailDef3.FRRY_EXTC_10KG.ColNo, sNumber1)
                .sprDetail3.SetCellStyle((i - 1), sprDetailDef3.FRRY_EXTC_PART.ColNo, sNumber1)

            Next

            Exit Sub

        End With

    End Sub

#End Region

#End Region

End Class
