' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM010G : ユーザーマスタメンテナンス
'  作  成  者     : [金へスル]
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

''' <summary>
''' LMM010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM010F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM010V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMconV As LMMControlV

    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 一覧で選択した行がログインしているユーザーであるかどうかを確認するフラグ
    ''' True = 選択行がログインユーザーと同じ 選択行がログインユーザーと異なる。
    ''' </summary>
    ''' <remarks></remarks>
    Private _IsActiveUserFlg As Boolean
    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validateクラスの設定
        Me._V = New LMM010V(handlerClass, frm, _LMMconV)

        'Gamen共通クラスの設定
        _LMMConG = New LMMControlG(handlerClass, DirectCast(frm, Form))

        'Validate共通クラスの設定
        _LMMconV = New LMMControlV(handlerClass, DirectCast(frm, Form), _LMMConG)


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
            .F10ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F11ButtonEnabled = edit

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
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW, _
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
            .sprDetail.TabIndex = LMM010C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM010C.CtlTabIndex.NRSBRCD
            .txtUserId.TabIndex = LMM010C.CtlTabIndex.USERID
            .txtUserNm.TabIndex = LMM010C.CtlTabIndex.USERNM
            .txtEMail.TabIndex = LMM010C.CtlTabIndex.EMAIL
            .chkNoticeYn.TabIndex = LMM010C.CtlTabIndex.NOTICE
            .cmbAuthoLv.TabIndex = LMM010C.CtlTabIndex.AUTHOLV
            .cmbRiyoushaKbn.TabIndex = LMM010C.CtlTabIndex.RIYOUSHAKBN
            .cmbWare.TabIndex = LMM010C.CtlTabIndex.WARE
            .cmbSapLinkAutho.TabIndex = LMM010C.CtlTabIndex.SAP_LINK_AUTHO
            .cmbBusyoCd.TabIndex = LMM010C.CtlTabIndex.BUSYOCD
            .txtPw.TabIndex = LMM010C.CtlTabIndex.PW
            .cmbInkaDateInit.TabIndex = LMM010C.CtlTabIndex.INKADATEINIT
            .cmbOutkaDateInit.TabIndex = LMM010C.CtlTabIndex.OUTKADATEINIT
            .btnRowAdd.TabIndex = LMM010C.CtlTabIndex.ROWADD
            .btnRowDel.TabIndex = LMM010C.CtlTabIndex.ROWDEL
            .btnDefaultCust.TabIndex = LMM010C.CtlTabIndex.DEFAULTCUST
            .grpPrt.TabIndex = LMM010C.CtlTabIndex.PRT
            .cmbDefPrt1.TabIndex = LMM010C.CtlTabIndex.DEFPRT1
            .cmbDefPrt2.TabIndex = LMM010C.CtlTabIndex.DEFPRT2
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .cmbDefPrt3.TabIndex = LMM010C.CtlTabIndex.DEFPRT3
            .cmbDefPrt4.TabIndex = LMM010C.CtlTabIndex.DEFPRT4
            .cmbDefPrt5.TabIndex = LMM010C.CtlTabIndex.DEFPRT5
            .cmbDefPrt6.TabIndex = LMM010C.CtlTabIndex.DEFPRT6
            .cmbDefPrt7.TabIndex = LMM010C.CtlTabIndex.DEFPRT7
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .cmbDefPrt8.TabIndex = LMM010C.CtlTabIndex.DEFPRT8
            .cmbCoaPrt.TabIndex = LMM010C.CtlTabIndex.COAPRT
            .sprDetail2.TabIndex = LMM010C.CtlTabIndex.DETAIL2
            .lblSituation.TabIndex = LMM010C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM010C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM010C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM010C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM010C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM010C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM010C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

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
    ''' コンボボックス作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateComboBox()

        With Me._Frm
            .cmbCoaPrt.Items.Clear()
            .cmbCoaPrt.Items.Add(String.Empty)
            .cmbYellowCardPrt.Items.Clear()
            .cmbYellowCardPrt.Items.Add(String.Empty)
            For Each p As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
                .cmbCoaPrt.Items.Add(p)
                .cmbYellowCardPrt.Items.Add(p)
                Debug.WriteLine(p)
            Next

            'デフォルトプリンタ コンボ再設定 ADD 2022/02/25
            Call CreateComboBoxPrt()

        End With

    End Sub

    ''' <summary>
    ''' コンボボックス作成 デフォルトプリンタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBoxPrt(Optional ByVal dispMd As String = DispMode.VIEW, Optional ByVal kbnCD As String = "")

        With Me._Frm

            'デフォルトプリンタ 
            Dim row As DataRow
            Dim strSqlKbn As String = String.Empty
            Dim BR_CD As String = LMUserInfoManager.GetNrsBrCd

            'strSqlKbn = String.Concat("KBN_GROUP_CD = 'R008' AND KBN_NM3 = '", BR_CD, "' AND SYS_DEL_FLG = '0' ")
            Select Case dispMd
                Case DispMode.VIEW
                    '区分M R008すべて対象
                    strSqlKbn = String.Concat("KBN_GROUP_CD = 'R008' AND SYS_DEL_FLG = '0' ")

                Case DispMode.EDIT
                    '区分M R008のログインユーザーの営業所と現在登録しているものを対象
                    strSqlKbn = String.Concat("KBN_GROUP_CD = 'R008' AND (KBN_NM3 = '", BR_CD, "' OR KBN_CD IN(", kbnCD, ")) And SYS_DEL_FLG = '0' ")

                Case DispMode.INIT
                    '区分M R008のログインユーザーの営業所のみ
                    strSqlKbn = String.Concat("KBN_GROUP_CD = 'R008' AND KBN_NM3 = '", BR_CD, "' AND SYS_DEL_FLG = '0' ")
            End Select

            '-- TEST 2023/01/25
            Dim dtKbn As DataTable = Nothing

            Dim drKbnM As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(strSqlKbn)

            If drKbnM.Length = 0 Then

                'R008   より取得できなかった時
                'Me._Frm.cmbDefPrt1.Items.Clear()
                Me._Frm.cmbDefPrt1.Enabled = False
                Me._Frm.cmbDefPrt2.Enabled = False
                Me._Frm.cmbDefPrt3.Enabled = False
                Me._Frm.cmbDefPrt4.Enabled = False
                Me._Frm.cmbDefPrt5.Enabled = False
                Me._Frm.cmbDefPrt6.Enabled = False
                Me._Frm.cmbDefPrt7.Enabled = False
                Me._Frm.cmbDefPrt8.Enabled = False

                Exit Sub
            Else
                dtKbn = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(strSqlKbn).CopyToDataTable
            End If

            '--
            'Dim dtKbn As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(strSqlKbn).CopyToDataTable

            row = dtKbn.NewRow
            row("KBN_CD") = ""
            row("KBN_NM1") = ""
            dtKbn.Rows.InsertAt(row, 0)

            With Me._Frm.cmbDefPrt1
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt2
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt3
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt4
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt5
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt6
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt7
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

            With Me._Frm.cmbDefPrt8
                .DataSource = dtKbn
                .ValueMember = "KBN_CD"
                .DisplayMember = "KBN_NM1"
                .CreateComboBoxData()
            End With

        End With

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            .cmbInkaDateInit.SelectedValue = LMM010C.INKADATEINIT
            .cmbOutkaDateInit.SelectedValue = LMM010C.OUTKADATEINIT

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
            Call Me._LMMConG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.INIT

                    'タブ内の表示内容を初期化する
                    Call Me.ClearControl(Me._Frm)
                    Me._Frm.sprDetail2.CrearSpread()

                Case DispMode.VIEW

                    '要望番号:1248 yamanaka 2013.03.19 Start
                    'スプレッド(下部)をロックする
                    'Me.SetLockBottomSpreadControl(True)

                    Call Me._LMMConG.SetLockControl(.tabMyData, lock)
                    '要望番号:1248 yamanaka 2013.03.19 End


                Case DispMode.EDIT

                    'ボタン活性化
                    Call Me._LMMConG.LockButton(.btnRowAdd, unLock)
                    Call Me._LMMConG.LockButton(.btnRowDel, unLock)

                    '常にロック項目ロック制御
                    Call Me._LMMConG.SetLockControl(.cmbNrsBrCd, lock)
                    Select Case .lblSituation.RecordStatus
                        '正常
                        Case RecordStatus.NOMAL_REC
                            '編集時ロック制御を行う
                            Call Me.LockControlEdit()

                        Case RecordStatus.NEW_REC
                            '新規時項目のクリアを行う
                            Call Me.ClearControlNew()

                        Case RecordStatus.COPY_REC
                            '複写時キー項目のクリアを行う
                            Call Me.ClearControlFukusha()

                            'デフォルト荷主情報Spreadの隠し項目である"ユーザーコード枝番"と"更新区分"の設定
                            Dim RowCnt As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
                            For i As Integer = 0 To RowCnt
                                Dim userCdEda As String = _LMMConG.SetMaeZeroData(Convert.ToString(i), 2)
                                'ユーザーコード枝番："00"から連番で設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.USER_CD_EDA.ColNo, userCdEda)
                                '更新区分："0"を設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.UPD_FLG.ColNo, "0")
                                '削除フラグ："0"を設定
                                .sprDetail2.SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")
                            Next

                    End Select

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 新規時項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlNew()

        Dim lock As Boolean = True
        Dim Unlock As Boolean = False

        '画面項目を全ロック解除する
        Call Me._LMMConG.SetLockControl(Me._Frm, Unlock)

        '新規時はロックする
        Call Me._LMMConG.SetLockControl(Me._Frm.cmbNrsBrCd, lock)

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub LockControlEdit()

        Dim lock As Boolean = True
        Dim Unlock As Boolean = False

        '画面項目を全ロック解除する
        Call Me._LMMConG.SetLockControl(Me._Frm, Unlock)
        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)

        With Me._Frm

            '編集時はロックする
            Call Me._LMMConG.SetLockControl(.cmbNrsBrCd, lock)
            Call Me._LMMConG.SetLockControl(.txtUserId, lock)
            'ユーザーコード枝番を初期化する
            .lblMaxCustEda.TextValue = String.Empty

            '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
            '権限がセンター長、システム管理者以外、かつ、
            '一覧で選択された情報が自分のものの場合、
            '指名、権限区分、利用者区分、倉庫を非活性にする。
            Dim kengen As String = LMUserInfoManager.GetAuthoLv
            Dim activeUserCd As String = LMUserInfoManager.GetUserID
            _IsActiveUserFlg = .txtUserId.TextValue.Equals(activeUserCd)
            If Not kengen.Equals(LMConst.AuthoKBN.LEADER) AndAlso
               Not kengen.Equals(LMConst.AuthoKBN.MANAGER) AndAlso
               _IsActiveUserFlg Then
                Call Me._LMMConG.SetLockControl(.txtUserNm, lock)
                Call Me._LMMConG.SetLockControl(.cmbAuthoLv, lock)
                Call Me._LMMConG.SetLockControl(.cmbRiyoushaKbn, lock)
                '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end
            End If

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        Dim lock As Boolean = True
        Dim Unlock As Boolean = False

        '画面項目を全ロック解除する
        Call Me._LMMConG.SetLockControl(Me._Frm, Unlock)

        'スプレッド(下部)をロック解除する
        Me.SetLockBottomSpreadControl(False)

        '複写時はロックする
        Call Me._LMMConG.SetLockControl(Me._Frm.cmbNrsBrCd, lock)

        With Me._Frm

            '複写しない項目は空を設定
            .txtUserId.TextValue = String.Empty
            .txtUserNm.TextValue = String.Empty
            .txtEMail.TextValue = String.Empty
            .chkNoticeYn.Checked = False
            .txtPw.TextValue = String.Empty
            .lblMaxCustEda.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 項目のクリア処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._LMMConG.ClearControl(ctl)

        With Me._Frm

            'スプレッド(下部)のクリア
            .sprDetail2.CrearSpread()
            '要望番号:1248 yamanaka 2013.03.19 Start
            .sprMyUnsoco.CrearSpread()
            .sprMyTariff.CrearSpread()
            '要望番号:1248 yamanaka 2013.03.19 End

        End With

    End Sub

    ''' <summary>
    ''' スプレッド(下部)のロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockBottomSpreadControl(ByVal lockFlg As Boolean)

        With Me._Frm

            Dim max As Integer = .sprDetail2.ActiveSheet.Rows.Count
            For i As Integer = 1 To max
                .sprDetail2.SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, lockFlg))
            Next

        End With

    End Sub

    ''' <summary>
    ''' 編集部のボタンロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBtn()
        With Me._Frm
            Select Case .lblSituation.DispMode
                '参照モード
                Case DispMode.VIEW
                    .btnRowAdd.Enabled = False
                    .btnRowDel.Enabled = False
                    .btnDefaultCust.Enabled = False

                    '編集モード
                Case DispMode.EDIT
                    .btnRowAdd.Enabled = True
                    .btnRowDel.Enabled = True
                    .btnDefaultCust.Enabled = True

                    '参照モード(初期)
                Case DispMode.INIT
                    .btnRowAdd.Enabled = False
                    .btnRowDel.Enabled = False
                    .btnDefaultCust.Enabled = False

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM010C.EventShubetsu)
        With Me._Frm
            Select Case eventType
                Case LMM010C.EventShubetsu.MAIN, LMM010C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM010C.EventShubetsu.SHINKI, LMM010C.EventShubetsu.HUKUSHA
                    .txtUserId.Focus()
                Case LMM010C.EventShubetsu.HENSHU
                    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
                    If _IsActiveUserFlg Then
                        .cmbWare.Focus()
                    Else
                        .txtUserNm.Focus()
                    End If
                    '.txtUserNm.Focus()
                    '2017/10/30 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end
                Case LMM010C.EventShubetsu.INS_T
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

            'ADD 2022/02/26　027945 【LMS】ユーザマスタメンテ　デフォルトプリンタ絞込み
            '現在登録内容取得
            Dim sKBN_CD As String = String.Empty

            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT1.ColNo).Text) = False Then
                sKBN_CD = String.Concat("'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT1.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT2.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT2.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT3.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT3.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT4.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT4.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT5.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT5.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT6.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT6.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT7.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT7.ColNo).Text, "'")
            End If
            If String.IsNullOrEmpty(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT8.ColNo).Text) = False Then
                If String.IsNullOrEmpty(sKBN_CD) = False Then
                    sKBN_CD = String.Concat(sKBN_CD, ",")
                End If
                sKBN_CD = String.Concat(sKBN_CD, "'", .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT8.ColNo).Text, "'")
            End If
            'デフォルトプリンタ コンボ再設定 ADD 2022/02/25
            Call CreateComboBoxPrt(DispMode.EDIT, sKBN_CD)


            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtUserId.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.USER_ID.ColNo).Text
            .txtUserNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.USER_NM.ColNo).Text
            .cmbAuthoLv.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.AUTHO_LV.ColNo).Text
            .txtEMail.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.EMAIL.ColNo).Text
            If LMM010C.UMU_FLG_ON.Equals(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.NOTICE.ColNo).Text) Then
                .chkNoticeYn.Checked = True
            Else
                .chkNoticeYn.Checked = False
            End If
            .cmbRiyoushaKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.RIYOUSHA_KBN.ColNo).Text
            .cmbWare.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.WH_CD.ColNo).Text
            .cmbBusyoCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.BUSYO_CD.ColNo).Text
            '.txtPw.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.PW.ColNo).Text
            .cmbInkaDateInit.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.INKA_DATE_INIT.ColNo).Text
            .cmbOutkaDateInit.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.OUTKA_DATE_INIT.ColNo).Text
            .cmbDefPrt1.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT1.ColNo).Value
            .cmbDefPrt2.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT2.ColNo).Value
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .cmbDefPrt3.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT3.ColNo).Value
            .cmbDefPrt4.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT4.ColNo).Value
            .cmbDefPrt5.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT5.ColNo).Value
            .cmbDefPrt6.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT6.ColNo).Value
            .cmbDefPrt7.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT7.ColNo).Value
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .cmbDefPrt8.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.DEF_PRT8.ColNo).Value
            .cmbCoaPrt.SelectedText = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.COA_PRT.ColNo).Text
            .cmbYellowCardPrt.SelectedText = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.YELLOW_CARD_PRT.ColNo).Text
            .cmbSapLinkAutho.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SAP_LINK_AUTHO.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

            Dim filterExp As String = String.Empty
            Dim drUserM As DataRow() = Nothing
            Dim rtnName As String = String.Empty

            '検索条件設定
            filterExp = String.Concat(filterExp, " SYS_DEL_FLG = '0' ")
            filterExp = String.Concat(filterExp, " AND USER_CD = " & "'" & .sprDetail.ActiveSheet.Cells(row, LMM010G.sprDetailDef.USER_ID.ColNo).Text & "'")

            'キャッシュ参照 PASSWORD取得　　
            drUserM = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(filterExp)
            If drUserM.Length <> 0 Then
                .txtPw.TextValue = drUserM(0).Item("PW").ToString
            End If

        End With

    End Sub
#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds.Tables(LMM010C.TABLE_NM_OUT))

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)     '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)           '営業所名
        Public Shared USER_ID As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.USER_ID, "ユーザー" & vbCrLf & "コード", 100, True)
        Public Shared USER_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.USER_NM, "氏名", 150, True)
        Public Shared AUTHO_LV As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.AUTHO_LV, "権限", 120, False)                '区分コード(隠し項目)
        Public Shared AUTHO_LV_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.AUTHO_LV_NM, "権限レベル", 150, True)     '区分名称

        '隠し項目
        Public Shared EMAIL As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.EMAIL, "メールアドレス", 150, False)
        Public Shared NOTICE As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.NOTICE, "通知対象", 150, False)
        Public Shared PW As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.PW, "パスワード", 150, False)
        Public Shared RIYOUSHA_KBN As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.RIYOUSHA_KBN, "利用者区", 150, False)
        Public Shared RIYOUSHA_KBN_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.RIYOUSHA_KBN_NM, "利用者区分名", 150, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.WH_CD, "倉庫コード", 150, False)
        Public Shared WH_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.WH_NM, "倉庫名", 150, False)
        Public Shared BUSYO_CD As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.BUSYO_CD, "部署コード", 150, False)
        Public Shared BUSYO_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.BUSYO_NM, "部署名", 150, False)
        Public Shared INKA_DATE_INIT As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.INKA_DATE_INIT, "入荷の日付初期値", 150, False)
        Public Shared INKA_DATE_INIT_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.INKA_DATE_INIT_NM, "入荷の日付初期値名", 150, False)
        Public Shared OUTKA_DATE_INIT As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.OUTKA_DATE_INIT, "出荷の日付初期値", 150, False)
        Public Shared OUTKA_DATE_INIT_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.OUTKA_DATE_INIT_NM, "出荷の日付初期値名", 150, False)
        Public Shared DEF_PRT1 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT1, "デフォルトプリンタ1", 150, False)
        Public Shared DEF_PRT2 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT2, "デフォルトプリンタ2", 150, False)
        'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
        Public Shared DEF_PRT3 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT3, "デフォルトプリンタ3", 150, False)
        Public Shared DEF_PRT4 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT4, "デフォルトプリンタ4", 150, False)
        Public Shared DEF_PRT5 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT5, "デフォルトプリンタ5", 150, False)
        Public Shared DEF_PRT6 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT6, "デフォルトプリンタ6", 150, False)
        Public Shared DEF_PRT7 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT7, "デフォルトプリンタ7", 150, False)
        'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
        Public Shared DEF_PRT8 As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.DEF_PRT8, "デフォルトプリンタ8", 150, False)
        Public Shared COA_PRT As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.COA_PRT, "分析票用プリンタ", 150, False)
        Public Shared YELLOW_CARD_PRT As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.YELLOW_CARD_PRT, "イエローカード用プリンタ", 150, False)
        Public Shared SAP_LINK_AUTHO As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SAP_LINK_AUTHO, "SAP連携権限", 150, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)


    End Class

    ''' <summary>
    ''' スプレッド列定義体(初期荷主)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.DEF, " ", 20, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 90, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 90, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.CUST_NM_L, "荷主名(大)", 150, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.CUST_NM_M, "荷主名(中)", 150, True)
        Public Shared DEF_CUST As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.DEF_CUST, "初期荷主", 100, True)
        '隠し項目
        Public Shared DEFAULT_CUST_YN As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.DEFAULT_CUST_YN, "初期荷主該当フラグ", 150, False)
        Public Shared USER_CD_T As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.USER_CD_T, "ユーザーコード", 150, False)
        Public Shared USER_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.USER_CD_EDA, "ユーザーコード枝番", 150, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex2.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    '要望番号:1248 yamanaka 2013.03.19 Start
    ''' <summary>
    ''' スプレッド列定義体(My運送会社)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMyUnsocoDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.DEF, " ", 20, True)
        Public Shared UNSOCO_CD As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.UNSOCO_CD, "運送会社" & vbCrLf & "コード", 90, True)
        Public Shared UNSOCO_BR_CD As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.UNSOCO_BR_CD, "運送会社支店" & vbCrLf & "コード", 90, True)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.UNSOCO_NM, "運送会社名", 200, True)
        Public Shared UNSOCO_BR_NM As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.UNSOCO_BR_NM, "運送会社支店名", 200, True)

        '隠し項目
        Public Shared USER_CD_T As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.USER_CD_T, "ユーザーコード", 150, False)
        Public Shared USER_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.USER_CD_EDA, "ユーザーコード枝番", 150, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex3.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(My運賃タリフ)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprMyTariffDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.DEF, " ", 20, True)
        Public Shared UNCHIN_TARIFF As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.UNCHIN_TARIFF, "運賃タリフコード", 150, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.REMARK, "備考", 430, True)

        '隠し項目
        Public Shared USER_CD_T As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.USER_CD_T, "ユーザーコード", 150, False)
        Public Shared USER_CD_EDA As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.USER_CD_EDA, "ユーザーコード枝番", 150, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.UPD_FLG, "更新区分", 150, False)
        Public Shared SYS_DEL_FLG_T As SpreadColProperty = New SpreadColProperty(LMM010C.SprColumnIndex4.SYS_DEL_FLG_T, "削除フラグ", 150, False)

    End Class
    '要望番号:1248 yamanaka 2013.03.19 End

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
            '要望番号:1248 yamanaka 2013.03.19 Start
            .sprMyUnsoco.CrearSpread()
            .sprMyTariff.CrearSpread()
            '要望番号:1248 yamanaka 2013.03.19 End

            '列数設定
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            '.sprDetail.ActiveSheet.ColumnCount = 26
            '.sprDetail2.ActiveSheet.ColumnCount = 11
            .sprDetail.ActiveSheet.ColumnCount = LMM010C.SprColumnIndex.LAST
            .sprDetail2.ActiveSheet.ColumnCount = LMM010C.SprColumnIndex2.LAST
            '要望番号:1248 yamanaka 2013.03.19 Start
            .sprMyUnsoco.ActiveSheet.ColumnCount = LMM010C.SprColumnIndex3.LAST
            .sprMyTariff.ActiveSheet.ColumnCount = LMM010C.SprColumnIndex4.LAST
            '要望番号:1248 yamanaka 2013.03.19 End
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM010G.sprDetailDef())
            '.sprDetail2.SetColProperty(New LMM010G.sprDetailDef2())
            ''要望番号:1248 yamanaka 2013.03.19 Start
            '.sprMyUnsoco.SetColProperty(New LMM010G.sprMyUnsocoDef())
            '.sprMyTariff.SetColProperty(New LMM010G.sprMyTariffDef())
            ''要望番号:1248 yamanaka 2013.03.19 End
            .sprDetail.SetColProperty(New LMM010G.sprDetailDef(), False)
            .sprDetail2.SetColProperty(New LMM010G.sprDetailDef2(), False)
            .sprMyUnsoco.SetColProperty(New LMM010G.sprMyUnsocoDef(), False)
            .sprMyTariff.SetColProperty(New LMM010G.sprMyTariffDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM010G.sprDetailDef.DEF.ColNo + 1

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If

            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.USER_ID.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.AUTHO_LV.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.AUTHO_LV_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, LMKbnConst.KBN_K010, False))

            '隠し項目（上部）
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.EMAIL.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.NOTICE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.PW.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left, , Format("***************")))
            '.sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.PW.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.RIYOUSHA_KBN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.RIYOUSHA_KBN_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.WH_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.WH_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.BUSYO_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.BUSYO_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.INKA_DATE_INIT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.INKA_DATE_INIT_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.OUTKA_DATE_INIT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.OUTKA_DATE_INIT_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT3.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT4.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT5.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT6.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT7.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.DEF_PRT8.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.COA_PRT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.YELLOW_CARD_PRT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SAP_LINK_AUTHO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM010G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            '列設定（下部）
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, False))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.CUST_NM_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.CUST_NM_M.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.DEF_CUST.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.USER_CD_T.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.UPD_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))
            .sprDetail2.SetCellStyle(-1, LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left))

            '要望番号:1248 yamanaka 2013.03.19 Start
            'My運送会社・My運賃タリフ初期設定
            Call Me.InitMySpr()
            '要望番号:1248 yamanaka 2013.03.19 End

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの初期設定(My運送会社・My運賃タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitMySpr()

        With Me._Frm

            '列設定（My運送会社）
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprMyUnsoco, False))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.UNSOCO_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.UNSOCO_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.UNSOCO_BR_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.USER_CD_T.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.USER_CD_EDA.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.UPD_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))
            .sprMyUnsoco.SetCellStyle(-1, LMM010G.sprMyUnsocoDef.SYS_DEL_FLG_T.ColNo, LMSpreadUtility.GetLabelCell(.sprMyUnsoco, CellHorizontalAlignment.Left))

            '列設定（My運賃タリフ）
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprMyTariff, False))
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.UNCHIN_TARIFF.ColNo, LMSpreadUtility.GetLabelCell(.sprMyTariff, CellHorizontalAlignment.Left))
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.REMARK.ColNo, LMSpreadUtility.GetLabelCell(.sprMyTariff, CellHorizontalAlignment.Left))
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.USER_CD_T.ColNo, LMSpreadUtility.GetLabelCell(.sprMyTariff, CellHorizontalAlignment.Left))
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.USER_CD_EDA.ColNo, LMSpreadUtility.GetLabelCell(.sprMyTariff, CellHorizontalAlignment.Left))
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.UPD_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprMyTariff, CellHorizontalAlignment.Left))
            .sprMyTariff.SetCellStyle(-1, LMM010G.sprMyTariffDef.SYS_DEL_FLG_T.ColNo, LMSpreadUtility.GetLabelCell(.sprMyTariff, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM010F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.NRS_BR_NM.ColNo).Value = LMUserInfoManager.GetNrsBrCd.ToString()
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.USER_ID.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.AUTHO_LV_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.EMAIL.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.NOTICE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.PW.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.RIYOUSHA_KBN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.RIYOUSHA_KBN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.WH_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.WH_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.BUSYO_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.BUSYO_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.INKA_DATE_INIT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.INKA_DATE_INIT_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.OUTKA_DATE_INIT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.OUTKA_DATE_INIT_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT1.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT2.ColNo).Value = String.Empty
            'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT3.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT4.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT5.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT6.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT7.ColNo).Value = String.Empty
            'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.DEF_PRT8.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.COA_PRT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.YELLOW_CARD_PRT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SAP_LINK_AUTHO.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM010G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

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
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.USER_ID.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.AUTHO_LV.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.AUTHO_LV_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.EMAIL.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.NOTICE.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.PW.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.RIYOUSHA_KBN.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.RIYOUSHA_KBN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.WH_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.WH_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.BUSYO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.BUSYO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.INKA_DATE_INIT.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.INKA_DATE_INIT_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.OUTKA_DATE_INIT.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.OUTKA_DATE_INIT_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT1.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT2.ColNo, sLabel)
                'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT3.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT4.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT5.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT6.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT7.ColNo, sLabel)
                'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
                .SetCellStyle(i, LMM010G.sprDetailDef.DEF_PRT8.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.COA_PRT.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.YELLOW_CARD_PRT.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SAP_LINK_AUTHO.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM010G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.USER_ID.ColNo, dr.Item("USER_CD").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.USER_NM.ColNo, dr.Item("USER_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.AUTHO_LV.ColNo, dr.Item("AUTHO_LV").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.AUTHO_LV_NM.ColNo, dr.Item("AUTHO_LV_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.EMAIL.ColNo, dr.Item("EMAIL").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.NOTICE.ColNo, dr.Item("NOTICE_YN").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.PW.ColNo, String.Empty)
                .SetCellValue(i, LMM010G.sprDetailDef.RIYOUSHA_KBN.ColNo, dr.Item("RIYOUSHA_KBN").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.RIYOUSHA_KBN_NM.ColNo, dr.Item("RIYOUSHA_KBN_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.WH_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.WH_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.BUSYO_CD.ColNo, dr.Item("BUSYO_CD").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.BUSYO_NM.ColNo, dr.Item("BUSYO_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.INKA_DATE_INIT.ColNo, dr.Item("INKA_DATE_INIT").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.INKA_DATE_INIT_NM.ColNo, dr.Item("INKA_DATE_INIT_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.OUTKA_DATE_INIT.ColNo, dr.Item("OUTKA_DATE_INIT").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.OUTKA_DATE_INIT_NM.ColNo, dr.Item("OUTKA_DATE_INIT_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT1.ColNo, dr.Item("DEF_PRT1").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT2.ColNo, dr.Item("DEF_PRT2").ToString())
                'START YANAI 要望番号675 プリンタの設定を個人別を可能にする
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT3.ColNo, dr.Item("DEF_PRT3").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT4.ColNo, dr.Item("DEF_PRT4").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT5.ColNo, dr.Item("DEF_PRT5").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT6.ColNo, dr.Item("DEF_PRT6").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT7.ColNo, dr.Item("DEF_PRT7").ToString())
                'END YANAI 要望番号675 プリンタの設定を個人別を可能にする
                .SetCellValue(i, LMM010G.sprDetailDef.DEF_PRT8.ColNo, dr.Item("DEF_PRT8").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.COA_PRT.ColNo, dr.Item("COA_PRT").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.YELLOW_CARD_PRT.ColNo, dr.Item("YELLOW_CARD_PRT").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SAP_LINK_AUTHO.ColNo, dr.Item("SAP_LINK_AUTHO").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM010G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2(ByVal dt As DataTable, ByRef userid As String)

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        Dim tmpDatarow2() As DataRow = dt.Select(String.Concat("USER_CD = '", userid, "' "), "CUST_CD_L ASC, CUST_CD_M ASC")

        With spr2

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = tmpDatarow2.Length

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow2(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEF_CUST.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.USER_CD_T.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.DEF_CUST.ColNo, dr.Item("DEFAULT_CUST_YN_NM").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, dr.Item("DEFAULT_CUST_YN").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.USER_CD_T.ColNo, dr.Item("USER_CD").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, dr.Item("USER_CD_EDA").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(My運送会社)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetMyUnsocoSpread(ByVal dt As DataTable, ByRef userid As String)

        Dim spr As LMSpread = Me._Frm.sprMyUnsoco
        Dim dtOut As New DataSet

        Dim tmpDatarow2() As DataRow = dt.Select(String.Concat("USER_CD = '", userid, "' "), "UNSOCO_CD, UNSOCO_BR_CD")

        With spr

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = tmpDatarow2.Length

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow2(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_CD.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_BR_NM.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.USER_CD_T.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.USER_CD_EDA.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyUnsocoDef.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_CD.ColNo, dr.Item("UNSOCO_CD").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD.ColNo, dr.Item("UNSOCO_BR_CD").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.UNSOCO_BR_NM.ColNo, dr.Item("UNSOCO_BR_NM").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.USER_CD_T.ColNo, dr.Item("USER_CD").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.USER_CD_EDA.ColNo, dr.Item("USER_CD_EDA").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyUnsocoDef.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(My運賃タリフ)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetMyTariffSpread(ByVal dt As DataTable, ByRef userid As String)

        Dim spr As LMSpread = Me._Frm.sprMyTariff
        Dim dtOut As New DataSet

        Dim tmpDatarow2() As DataRow = dt.Select(String.Concat("USER_CD = '", userid, "' "), "UNCHIN_TARIFF_CD")

        With spr

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = tmpDatarow2.Length

            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = tmpDatarow2(i - 1)

                'セルスタイル設定
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.DEF.ColNo, sDEF)
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.UNCHIN_TARIFF.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.REMARK.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.USER_CD_T.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.USER_CD_EDA.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.UPD_FLG.ColNo, sLabel)
                .SetCellStyle((i - 1), LMM010G.sprMyTariffDef.SYS_DEL_FLG_T.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.UNCHIN_TARIFF.ColNo, dr.Item("UNCHIN_TARIFF_CD").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.REMARK.ColNo, dr.Item("UNCHIN_TARIFF_REM").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.USER_CD_T.ColNo, dr.Item("USER_CD").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.USER_CD_EDA.ColNo, dr.Item("USER_CD_EDA").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue((i - 1), LMM010G.sprMyTariffDef.SYS_DEL_FLG_T.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub
    '要望番号:1248 yamanaka 2013.03.21 End

    ''' <summary>
    ''' スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetSpread2(ByVal dt As DataTable)

        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        Dim dtOut As New DataSet

        With spr2

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定(荷主マスタ照会POP)
            dr = dt.Rows(0)

            '値設定(MAXユーザーコード枝番)
            Dim MaxUser As String = String.Empty
            MaxUser = Me._Frm.lblMaxCustEda.TextValue

            '区分マスタキャッシュ取得（有無フラグ＝"00"(無)の区分名２を取得）
            Dim NKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'U009' AND ", _
                                                                                      "KBN_CD ='00'"))

            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM010G.sprDetailDef2.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM010G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.CUST_CD_M.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.CUST_NM_L.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.CUST_NM_M.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.DEF_CUST.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.USER_CD_T.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM010G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM010G.sprDetailDef2.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
            .SetCellValue(row, LMM010G.sprDetailDef2.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
            .SetCellValue(row, LMM010G.sprDetailDef2.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
            .SetCellValue(row, LMM010G.sprDetailDef2.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
            .SetCellValue(row, LMM010G.sprDetailDef2.DEF_CUST.ColNo, NKbn(0).Item("KBN_NM2").ToString)
            .SetCellValue(row, LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, "00")
            .SetCellValue(row, LMM010G.sprDetailDef2.USER_CD_T.ColNo, String.Empty)
            .SetCellValue(row, LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, MaxUser)
            .SetCellValue(row, LMM010G.sprDetailDef2.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_行削除/初期荷主時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ReSetSpread(ByVal spr As LMSpread)

        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            Dim max As Integer = .ActiveSheet.Rows.Count

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            For i As Integer = 1 To max

                If i > max Then
                    Exit For
                End If

                '行削除処理で最初のスプレッドの行数が減った場合は、行削除後のスプレッドの行数を設定
                If max > .ActiveSheet.Rows.Count Then
                    i = i - 1
                    max = max - 1
                End If

                '行削除されていない行は再描画
                If (LMConst.FLG.OFF).Equals(_LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SYS_DEL_FLG_T.ColNo))) = True Then

                    'セルスタイル設定
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEF.ColNo, sDEF)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_CD_L.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_CD_M.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_NM_L.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.CUST_NM_M.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEF_CUST.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.USER_CD_T.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.UPD_FLG.ColNo, sLabel)
                    .SetCellStyle((i - 1), LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, sLabel)

                    'セルに値を設定
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_CD_L.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.CUST_CD_L.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_CD_M.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.CUST_CD_M.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_NM_L.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.CUST_NM_L.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.CUST_NM_M.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.CUST_NM_M.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.DEF_CUST.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.DEF_CUST.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.DEFAULT_CUST_YN.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.DEFAULT_CUST_YN.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.USER_CD_T.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.USER_CD_T.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.USER_CD_EDA.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.USER_CD_EDA.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.UPD_FLG.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.UPD_FLG.ColNo)))
                    .SetCellValue((i - 1), LMM010G.sprDetailDef2.SYS_DEL_FLG_T.ColNo, _LMMConG.GetCellValue(spr.ActiveSheet.Cells((i - 1), sprDetailDef2.SYS_DEL_FLG_T.ColNo)))

                Else
                    '行削除された行は非表示
                    spr.ActiveSheet.RemoveRows((i - 1), 1)
                End If

            Next

            .ResumeLayout(True)

        End With

    End Sub

    '要望番号:1248 yamanaka 2013.03.19 Start
    ''' <summary>
    ''' スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetMyUnsoco(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprMyUnsoco
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            dr = dt.Rows(0)

            '値設定(MAXユーザーコード枝番)
            Dim MaxUser As String = Me._Frm.lblMaxUnsocoEda.TextValue
            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.UNSOCO_CD.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.UNSOCO_NM.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.UNSOCO_BR_NM.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.USER_CD_T.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.USER_CD_EDA.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyUnsocoDef.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.UNSOCO_CD.ColNo, dr.Item("UNSOCO_CD").ToString())
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.UNSOCO_BR_CD.ColNo, dr.Item("UNSOCO_BR_CD").ToString())
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.UNSOCO_NM.ColNo, dr.Item("UNSOCO_NM").ToString())
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.UNSOCO_BR_NM.ColNo, dr.Item("UNSOCO_BR_NM").ToString())
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.USER_CD_T.ColNo, String.Empty)
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.USER_CD_EDA.ColNo, MaxUser)
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM010G.sprMyUnsocoDef.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_行追加時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddSetMyTariff(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprMyTariff
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, 1)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            dr = dt.Rows(0)

            '値設定(MAXユーザーコード枝番)
            Dim MaxUser As String = Me._Frm.lblMaxTariffEda.TextValue
            Dim row As Integer = .Sheets(0).Rows.Count - 1

            'セルスタイル設定
            .SetCellStyle(row, LMM010G.sprMyTariffDef.DEF.ColNo, sDEF)
            .SetCellStyle(row, LMM010G.sprMyTariffDef.UNCHIN_TARIFF.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyTariffDef.REMARK.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyTariffDef.USER_CD_T.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyTariffDef.USER_CD_EDA.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyTariffDef.UPD_FLG.ColNo, sLabel)
            .SetCellStyle(row, LMM010G.sprMyTariffDef.SYS_DEL_FLG_T.ColNo, sLabel)

            'セルに値を設定
            .SetCellValue(row, LMM010G.sprMyTariffDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(row, LMM010G.sprMyTariffDef.UNCHIN_TARIFF.ColNo, dr.Item("UNCHIN_TARIFF_CD").ToString())
            .SetCellValue(row, LMM010G.sprMyTariffDef.REMARK.ColNo, dr.Item("UNCHIN_TARIFF_REM").ToString())
            .SetCellValue(row, LMM010G.sprMyTariffDef.USER_CD_T.ColNo, String.Empty)
            .SetCellValue(row, LMM010G.sprMyTariffDef.USER_CD_EDA.ColNo, MaxUser)
            .SetCellValue(row, LMM010G.sprMyTariffDef.UPD_FLG.ColNo, "0")
            .SetCellValue(row, LMM010G.sprMyTariffDef.SYS_DEL_FLG_T.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub
    '要望番号:1248 yamanaka 2013.03.19 End

#Region "Spread(ADD/DEL)"

    '要望番号:1248 yamanaka 2013.03.21 Start
    ''' <summary>
    ''' MAXユーザーコード枝番の値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function SetUserCdEdaDataSet(ByVal pageNm As String) As Boolean

        With Me._Frm

            Dim spr As SheetView = Nothing
            Dim userCdEda As SpreadColProperty = Nothing
            Dim lblNm As LMImTextBox = Nothing

            Select Case pageNm

                Case Me._Frm.tpgMyCust.Name

                    spr = .sprDetail2.ActiveSheet
                    userCdEda = LMM010G.sprDetailDef2.USER_CD_EDA
                    lblNm = .lblMaxCustEda

                Case Me._Frm.tpgMyUnsoco.Name

                    spr = .sprMyUnsoco.ActiveSheet
                    userCdEda = LMM010G.sprMyUnsocoDef.USER_CD_EDA
                    lblNm = .lblMaxUnsocoEda

                Case Me._Frm.tpgMyTariff.Name

                    spr = .sprMyTariff.ActiveSheet
                    userCdEda = LMM010G.sprMyTariffDef.USER_CD_EDA
                    lblNm = .lblMaxTariffEda

            End Select

            '複写の場合
            Dim RowCnt As Integer = spr.Rows.Count - 1

            If (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True _
            OrElse (RecordStatus.NOMAL_REC).Equals(.lblSituation.RecordStatus) = True Then

                If RowCnt > -1 Then
                    '要望管理2025Start 2013.4.26 s.kobayashi
                    Dim maxNo As Integer = 0
                    For i As Integer = 0 To RowCnt
                        If maxNo <= Convert.ToInt32(spr.Cells(i, userCdEda.ColNo).Text) Then
                            maxNo = Convert.ToInt32(spr.Cells(i, userCdEda.ColNo).Text)
                        End If
                    Next
                    lblNm.TextValue = maxNo.ToString()
                    'lblNm.TextValue = spr.Cells(RowCnt, userCdEda.ColNo).Text
                    '要望管理2025End 2013.4.26 s.kobayashi 
                Else

                    lblNm.TextValue = ""

                End If

            End If

            'ユーザーコード枝番の最大値を求める
            Dim oldMaxUserCd As String = String.Empty
            Dim newMaxUserCd As String = String.Empty
            Dim maxLimit As Integer = 0

            If String.IsNullOrEmpty(lblNm.TextValue) = True Then
                oldMaxUserCd = "0"
            Else
                oldMaxUserCd = lblNm.TextValue
            End If

            Select Case pageNm

                Case .tpgMyCust.Name

                    '限界値設定
                    maxLimit = 99

                    'If (oldMaxUserCd).Equals("0") = True Then
                    '        newMaxUserCd = Me._LMMConG.SetMaeZeroData(oldMaxUserCd, 2)
                    'Else
                    '    現在のMAXユーザーコード枝番がMAX値を超えている場合は採番後の桁数を3桁にする
                    '    If (oldMaxUserCd).Equals(maxLimit.ToString()) = True Then
                    '        newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 3)
                    '    Else
                    '        newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 2)
                    '    End If
                    'End If

                    If (oldMaxUserCd).Equals("0") = True _
                    And (RecordStatus.NEW_REC).Equals(.lblSituation.RecordStatus) = True Then
                        newMaxUserCd = Me._LMMConG.SetMaeZeroData(oldMaxUserCd, 2)
                    Else
                        '現在のMAXユーザーコード枝番がMAX値を超えている場合は採番後の桁数を3桁にする
                        If (oldMaxUserCd).Equals(maxLimit.ToString()) = True Then
                            newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 3)
                        Else
                            newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 2)
                        End If
                    End If

                Case .tpgMyUnsoco.Name, .tpgMyTariff.Name

                    '限界値設定
                    maxLimit = 999

                    'If (oldMaxUserCd).Equals("0") = True Then
                    '    newMaxUserCd = Me._LMMConG.SetMaeZeroData(oldMaxUserCd, 3)
                    'Else
                    '    '現在のMAXユーザーコード枝番がMAX値を超えている場合は採番後の桁数を3桁にする
                    '    If (oldMaxUserCd).Equals(maxLimit.ToString()) = True Then
                    '        newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 4)
                    '    Else
                    '        newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 3)
                    '    End If
                    'End If

                    If (oldMaxUserCd).Equals("0") = True _
                    And (RecordStatus.NEW_REC).Equals(.lblSituation.RecordStatus) = True Then
                        newMaxUserCd = Me._LMMConG.SetMaeZeroData(oldMaxUserCd, 3)
                    Else
                        '現在のMAXユーザーコード枝番がMAX値を超えている場合は採番後の桁数を3桁にする
                        If (oldMaxUserCd).Equals(maxLimit.ToString()) = True Then
                            newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 4)
                        Else
                            newMaxUserCd = Me._LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 3)
                        End If
                    End If

            End Select

            '枝番の限界値、チェック
            If Me._LMMconV.IsMaxChk(Convert.ToInt32(newMaxUserCd), maxLimit, "ユーザーコード枝番") = False Then
                '処理終了アクション
                Return False
            End If

            '画面のMAXユーザーコード枝番に設定
            lblNm.TextValue = newMaxUserCd

        End With

        Return True

    End Function

    '''' <summary>
    '''' MAXユーザーコード枝番の値を設定
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <remarks></remarks>
    'Friend Function SetUserCdEdaDataSet(ByVal ds As DataSet, ByVal eventShubetsu As LMM010C.EventShubetsu) As Boolean

    '    '新規/複写の場合
    '    If ds Is Nothing Then
    '        ds = New LMM010DS()
    '    End If

    '    With Me._Frm

    '        '新規/複写の場合
    '        Dim max As Integer = ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).Rows.Count
    '        Dim insMRows As DataRow = ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).NewRow

    '        '複写の場合
    '        If (RecordStatus.COPY_REC).Equals(.lblSituation.RecordStatus) = True Then
    '            Dim RowCnt As Integer = Me._Frm.sprDetail2.ActiveSheet.Rows.Count - 1
    '            If -1 < RowCnt Then
    '                .lblMaxCustEda.TextValue = Me._Frm.sprDetail2.ActiveSheet.Cells(RowCnt, LMM010G.sprDetailDef2.USER_CD_EDA.ColNo).Text
    '            End If
    '        End If

    '        'ユーザーコード枝番の最大値を求める
    '        Dim oldMaxUserCd As String = String.Empty
    '        If (0).Equals(max) = True Then
    '            If String.IsNullOrEmpty(.lblMaxCustEda.TextValue) = True Then
    '                oldMaxUserCd = "0"
    '            Else
    '                oldMaxUserCd = .lblMaxCustEda.TextValue
    '            End If
    '        Else
    '            If ("").Equals(ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).Rows(max - 1).Item("USER_CD_MAXEDA").ToString()) = True Then
    '                oldMaxUserCd = "0"
    '            Else
    '                oldMaxUserCd = ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).Rows(max - 1).Item("USER_CD_MAXEDA").ToString()
    '            End If
    '        End If

    '        Dim newMaxUserCd As String = String.Empty
    '        If ("0").Equals(oldMaxUserCd) = True Then
    '            newMaxUserCd = _LMMConG.SetMaeZeroData(oldMaxUserCd, 2)
    '            '現在のMAXユーザーコード枝番がMAX値を超えている場合は採番後の桁数を3桁にする
    '        Else
    '            If ("99").Equals(oldMaxUserCd) = False Then
    '                newMaxUserCd = _LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 2)
    '            Else
    '                newMaxUserCd = _LMMConG.SetMaeZeroData(Convert.ToString(Convert.ToInt32(oldMaxUserCd) + 1), 3)
    '            End If
    '        End If

    '        '枝番の限界値、チェック
    '        If Me._LMMconV.IsMaxChk(Convert.ToInt32(newMaxUserCd), 99, "ユーザーコード枝番") = False Then
    '            '処理終了アクション
    '            Return False
    '        End If

    '        'ユーザーコード枝番の更新
    '        insMRows("USER_CD_MAXEDA") = newMaxUserCd

    '        'データセットに追加
    '        ds.Tables(LMM010C.TABLE_NM_TCUST_MAXEDA).Rows.Add(insMRows)

    '        '画面のMAXユーザーコード枝番に設定
    '        .lblMaxCustEda.TextValue = newMaxUserCd

    '    End With

    '    Return True

    'End Function
    '要望番号:1248 yamanaka 2013.03.21 End

    ''' <summary>
    ''' Spreadの行削除
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DelTcust(ByVal spr As LMSpread, ByVal list As ArrayList)

        With spr

            '要望番号:1248 yamanaka 2013.03.19 Start
            Dim max As Integer = list.Count - 1
            'Dim max As Integer = .ActiveSheet.Rows.Count - 1

            For i As Integer = max To 0 Step -1
                .ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)
            Next

            'If ("sprDetail2").Equals(spr.Name) = True Then
            '    For i As Integer = 0 To max

            '        If i > max Then
            '            Exit For
            '        End If

            '        If (LMConst.FLG.ON).Equals(_LMMConG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.DEF.ColNo))) = True Then

            '            If ("1").Equals(_LMMConG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.UPD_FLG.ColNo))) = True Then
            '                '既に登録済みのデータの場合は、削除フラグを"1"に変更
            '                .SetCellValue(i, sprDetailDef2.SYS_DEL_FLG_T.ColNo, LMConst.FLG.ON)
            '            Else
            '                '上記以外(新規追加データ)の場合は、スプレッドから行削除
            '                .ActiveSheet.RemoveRows(i, 1)

            '                '行削除処理で最初のスプレッドの行数が減った場合は、最大スプレッド行数とカウントを減らす
            '                i = i - 1
            '                max = max - 1
            '            End If

            '        End If
            '    Next

            'End If
            '要望番号:1248 yamanaka 2013.03.19 End

        End With

    End Sub

    ''' <summary>
    ''' デフォルト荷主Spreadの初期荷主設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub DefTcust(ByVal spr As LMSpread)

        With spr

            Dim max As Integer = .ActiveSheet.Rows.Count - 1

            '初期荷主該当フラグの設定
            Dim defaultFlgOn As String = "01"
            Dim defaultFlgOff As String = "00"

            '区分マスタキャッシュ取得（有無フラグ＝"00"(無)の区分名２を取得）
            Dim NKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'U009' AND ", _
                                                                                      "KBN_CD ='00'"))

            '区分マスタキャッシュ取得（有無フラグ＝"01"(有)の区分名２を取得）
            Dim YKbn As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'U009' AND ", _
                                                                                      "KBN_CD ='01'"))

            If ("sprDetail2").Equals(spr.Name) = True Then
                For i As Integer = 0 To max

                    If i > max Then
                        Exit For
                    End If

                    If (LMConst.FLG.ON).Equals(_LMMConG.GetCellValue(spr.ActiveSheet.Cells(i, sprDetailDef2.DEF.ColNo))) = True Then
                        '選択行の場合は、初期荷主該当フラグを "01"（初期荷主対象）に変更
                        .SetCellValue(i, sprDetailDef2.DEFAULT_CUST_YN.ColNo, defaultFlgOn)
                        .SetCellValue(i, sprDetailDef2.DEF_CUST.ColNo, YKbn(0).Item("KBN_NM2").ToString)
                    Else
                        '上記以外(非選択行)の場合は、初期荷主該当フラグを "00"（初期荷主対象外）に変更
                        .SetCellValue(i, sprDetailDef2.DEFAULT_CUST_YN.ColNo, defaultFlgOff)
                        .SetCellValue(i, sprDetailDef2.DEF_CUST.ColNo, NKbn(0).Item("KBN_NM2").ToString)
                    End If
                Next

            End If

        End With

    End Sub

#End Region


#End Region

#End Region

End Class
