' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA020G : メニュー
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMA020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMA020F


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMA020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim edit As Boolean = True
        Dim refer As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.MENU)
            .F9ButtonName = "パスワード変更"
            .F10ButtonName = String.Empty
            .F11ButtonName = "マスタ取得"
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F9ButtonEnabled = True
            .F10ButtonEnabled = False
            .F11ButtonEnabled = True
            .F12ButtonEnabled = True

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .menuLMB.TabIndex = LMA020C.CtlTabIndex.LMB
            .menuLMC.TabIndex = LMA020C.CtlTabIndex.LMC
            .menuLMD.TabIndex = LMA020C.CtlTabIndex.LMD
            .menuLME.TabIndex = LMA020C.CtlTabIndex.LME
            .menuLMF.TabIndex = LMA020C.CtlTabIndex.LMF
            .menuLMG.TabIndex = LMA020C.CtlTabIndex.LMG
            .menuLMH.TabIndex = LMA020C.CtlTabIndex.LMH
            .menuLMI.TabIndex = LMA020C.CtlTabIndex.LMI
            .menuLMJ.TabIndex = LMA020C.CtlTabIndex.LMJ
            .menuLMK.TabIndex = LMA020C.CtlTabIndex.LMK
            .menuLML.TabIndex = LMA020C.CtlTabIndex.LML
            .menuLMM1.TabIndex = LMA020C.CtlTabIndex.LMM1
            .menuLMM2.TabIndex = LMA020C.CtlTabIndex.LMM2
            .menuLMM3.TabIndex = LMA020C.CtlTabIndex.LMM3
            .menuLMN.TabIndex = LMA020C.CtlTabIndex.LMN
            .menuLMQ.TabIndex = LMA020C.CtlTabIndex.LMQ
            .menuLMR.TabIndex = LMA020C.CtlTabIndex.LMR
            .menuLMS.TabIndex = LMA020C.CtlTabIndex.LMS
            .menuLMT.TabIndex = LMA020C.CtlTabIndex.LMT
            .menuLMU.TabIndex = LMA020C.CtlTabIndex.LMU
            '.menuLMZ.TabIndex = LMA020C.CtlTabIndex.LMZ

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal appVersion As String)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim _langMgr As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        With Me._Frm
            'タイトルに営業所名を追記
            Dim drUs As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD='" & LM.Base.LMUserInfoManager.GetUserID.ToString & "'"))(0)
            Dim drKb As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD='D003' AND KBN_CD='" & drUs.Item("NRS_BR_CD").ToString & "'"))(0)
            .Text = String.Concat(.Text, "　　", drKb.Item("KBN_NM2").ToString)

            '2014/06/25 M_takahashi 
            '【営業所M】でLOCK＿FLGが"01"の場合は、ロックする。(一部のコントロールのみ)
            Dim dr As DataRow
            '【営業所M】キャッシュデータ取得
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() & "'"))(0)

            Dim whereKbnN018 As String =
                String.Concat(
                    "    KBN_GROUP_CD = 'N018'",
                    "AND KBN_NM5 = '", LM.Base.LMUserInfoManager.GetNrsBrCd().ToString(), "'",
                    "AND SYS_DEL_FLG = '0'")
            Dim drKbnN018 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(whereKbnN018)

            .lblVersion.Text = "Version : " & appVersion

            '2017/09/25 修正 李↓
            .LMTitleLabel.Text = _langMgr.Selector({"システム日付", "System Date", "시스템날짜", "中国語"})

            'メニューボタンの構築
            Dim langStr As String = String.Empty
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMB, LMA020C.MENU_TITLE_LMB_ENG, LMA020C.MENU_TITLE_LMB_KR, LMA020C.MENU_TITLE_LMB_ENG})
            .menuLMB.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMB, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMC, LMA020C.MENU_TITLE_LMC_ENG, LMA020C.MENU_TITLE_LMC_KR, LMA020C.MENU_TITLE_LMC_ENG})
            .menuLMC.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMC, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMD, LMA020C.MENU_TITLE_LMD_ENG, LMA020C.MENU_TITLE_LMD_KR, LMA020C.MENU_TITLE_LMD_ENG})
            .menuLMD.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMD, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LME, LMA020C.MENU_TITLE_LME_ENG, LMA020C.MENU_TITLE_LME_KR, LMA020C.MENU_TITLE_LME_ENG})
            .menuLME.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LME, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMF, LMA020C.MENU_TITLE_LMF_ENG, LMA020C.MENU_TITLE_LMF_KR, LMA020C.MENU_TITLE_LMF_ENG})
            .menuLMF.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMF, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMG, LMA020C.MENU_TITLE_LMG_ENG, LMA020C.MENU_TITLE_LMG_KR, LMA020C.MENU_TITLE_LMG_ENG})
            .menuLMG.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMG, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMH, LMA020C.MENU_TITLE_LMH_ENG, LMA020C.MENU_TITLE_LMH_KR, LMA020C.MENU_TITLE_LMH_ENG})
            .menuLMH.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMH, langStr, LMA020C.MENU_FLG_NOMAL))
            '特定荷主はLOCK＿FLGが"01"の場合、Z_KBN(KBN_GROUP_CD = 'N018' AND KBN_NM5 = 《ログインユーザーの営業所コード》) のレコードが存在しなければロックする。
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMI, LMA020C.MENU_TITLE_LMI_ENG, LMA020C.MENU_TITLE_LMI_KR, LMA020C.MENU_TITLE_LMI_ENG})
            If dr.Item("LOCK_FLG").ToString.Equals("01") Then
                If drKbnN018.Count = 0 Then
                    .menuLMI.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMI, langStr, LMA020C.MENU_FLG_OTHER))
                Else
                    .menuLMI.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMI, langStr, LMA020C.MENU_FLG_NOMAL))
                End If
            Else
                .menuLMI.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMI, langStr, LMA020C.MENU_FLG_NOMAL))
            End If
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMJ, LMA020C.MENU_TITLE_LMJ_ENG, LMA020C.MENU_TITLE_LMJ_KR, LMA020C.MENU_TITLE_LMJ_ENG})
            .menuLMJ.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMJ, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMK, LMA020C.MENU_TITLE_LMK_ENG, LMA020C.MENU_TITLE_LMK_KR, LMA020C.MENU_TITLE_LMK_ENG})
            .menuLMK.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMK, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LML, LMA020C.MENU_TITLE_LML_ENG, LMA020C.MENU_TITLE_LML_KR, LMA020C.MENU_TITLE_LML_ENG})
            .menuLML.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LML, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMM1, LMA020C.MENU_TITLE_LMM1_ENG, LMA020C.MENU_TITLE_LMM1_KR, LMA020C.MENU_TITLE_LMM1_ENG})
            .menuLMM1.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMM, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMM2, LMA020C.MENU_TITLE_LMM2_ENG, LMA020C.MENU_TITLE_LMM2_KR, LMA020C.MENU_TITLE_LMM2_ENG})
            .menuLMM2.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMM, langStr, LMA020C.MENU_FLG_OTHER))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMM3, LMA020C.MENU_TITLE_LMM3_ENG, LMA020C.MENU_TITLE_LMM3_KR, LMA020C.MENU_TITLE_LMM3_ENG})
            .menuLMM3.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMM, langStr, LMA020C.MENU_FLG_MAINT))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMN, LMA020C.MENU_TITLE_LMN_ENG, LMA020C.MENU_TITLE_LMN_KR, LMA020C.MENU_TITLE_LMN_ENG})
            .menuLMN.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMN, langStr, LMA020C.MENU_FLG_NOMAL))
            'データ抽出ははLOCK＿FLGが"01"の場合ロックする。
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMQ, LMA020C.MENU_TITLE_LMQ_ENG, LMA020C.MENU_TITLE_LMQ_KR, LMA020C.MENU_TITLE_LMQ_ENG})
            If dr.Item("LOCK_FLG").ToString.Equals("01") Then
                .menuLMQ.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMQ, langStr, LMA020C.MENU_FLG_OTHER))
            Else
                .menuLMQ.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMQ, langStr, LMA020C.MENU_FLG_NOMAL))
            End If
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMR, LMA020C.MENU_TITLE_LMR_ENG, LMA020C.MENU_TITLE_LMR_KR, LMA020C.MENU_TITLE_LMR_ENG})
            .menuLMR.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMR, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMS, LMA020C.MENU_TITLE_LMS_ENG, LMA020C.MENU_TITLE_LMS_KR, LMA020C.MENU_TITLE_LMS_ENG})
            .menuLMS.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMS, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMT, LMA020C.MENU_TITLE_LMT_ENG, LMA020C.MENU_TITLE_LMT_KR, LMA020C.MENU_TITLE_LMT_ENG})
            .menuLMT.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMT, langStr, LMA020C.MENU_FLG_NOMAL))
            langStr = _langMgr.Selector({LMA020C.MENU_TITLE_LMU, LMA020C.MENU_TITLE_LMU_ENG, LMA020C.MENU_TITLE_LMU_KR, LMA020C.MENU_TITLE_LMU_ENG})
            .menuLMU.Items.Add(Me.CreateToolStripMenu(LMA020C.SUB_SYSTEM_ID_LMU, langStr, LMA020C.MENU_FLG_NOMAL))
            '2017/09/25 修正 李↑

            '時刻を表示
            .lblTime.TextValue = Format(My.Computer.Clock.LocalTime, _langMgr.Selector({"yyyy/MM/dd hh:mm:ss", "dd/MM/yyyy hh:mm:ss", "yyyy/MM/dd hh:mm:ss", "dd/MM/yyyy hh:mm:ss"}))
            'タイマー設定(ミリ秒で設定)
            .timerMessage.Interval = 15 * 60000 '600000000(15分)

            'タイマースタート
            .timerMessage.Enabled = True



        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            '更にメニューに対する遷移先画面が存在しないものについては、ロック
            Call Me.SetMenuLockControl(.menuLMB)
            Call Me.SetMenuLockControl(.menuLMC)
            Call Me.SetMenuLockControl(.menuLMD)
            Call Me.SetMenuLockControl(.menuLME)
            Call Me.SetMenuLockControl(.menuLMF)
            Call Me.SetMenuLockControl(.menuLMG)
            Call Me.SetMenuLockControl(.menuLMH)
            Call Me.SetMenuLockControl(.menuLMI)
            Call Me.SetMenuLockControl(.menuLMJ)
            Call Me.SetMenuLockControl(.menuLMK)
            Call Me.SetMenuLockControl(.menuLML)
            Call Me.SetMenuLockControl(.menuLMM1)
            Call Me.SetMenuLockControl(.menuLMM2)
            Call Me.SetMenuLockControl(.menuLMM3)
            Call Me.SetMenuLockControl(.menuLMN)
            Call Me.SetMenuLockControl(.menuLMQ)
            Call Me.SetMenuLockControl(.menuLMR)
            Call Me.SetMenuLockControl(.menuLMS)
            Call Me.SetMenuLockControl(.menuLMT)
            Call Me.SetMenuLockControl(.menuLMU)
            'Call Me.SetMenuLockControl(.menuLMZ)

        End With

    End Sub

    ''' <summary>
    ''' ロック制御
    ''' </summary>
    ''' <param name="ctl">メニューコントロール</param>
    ''' <remarks></remarks>
    Private Sub SetMenuLockControl(ByVal ctl As System.Windows.Forms.MenuStrip)

        Dim lock As Boolean = True
        '何も設定されていない場合、ロック
        If String.IsNullOrEmpty(ctl.Items(0).Name) = True Then
            lock = False
        End If

        ctl.Enabled = lock

    End Sub

    ''' <summary>
    ''' メニューボタン生成
    ''' </summary>
    ''' <param name="subSystemId">サブシステムID</param>
    ''' <param name="menuFlg">メニューフラグ</param>
    ''' <remarks></remarks>
    Private Function CreateToolStripMenu(ByVal subSystemId As String, ByVal title As String, ByVal menuFlg As String) As ToolStripMenuItem

        'メニュー本体の生成
        Dim toolStripMenu As ToolStripMenuItem = New ToolStripMenuItem()
        toolStripMenu.Text = title
        'toolStripMenu.ForeColor = Color.White

        'メニューリスト生成
        Dim dropDownMenuList As ToolStripMenuItem() = Me.GetDropDownMenuList(subSystemId, menuFlg)

        'メニューリスト内の項目数 = 1 の場合リストは本体に付与せず、画面IDをメニュ本体に付け替える
        If dropDownMenuList.Length > 1 Then

            toolStripMenu.Name = subSystemId
            toolStripMenu.DropDownItems.AddRange(dropDownMenuList)

        ElseIf dropDownMenuList.Length = 1 Then

            toolStripMenu.Name = dropDownMenuList(0).Name
            AddHandler toolStripMenu.Click, AddressOf Me._Frm.ToolStripMenu_Click

        ElseIf dropDownMenuList.Length = 0 Then

            toolStripMenu.Name = String.Empty

        End If

        Return toolStripMenu

    End Function

    ''' <summary>
    ''' メニューリスト取得
    ''' </summary>
    ''' <param name="subSystemId">サブシステムID</param>
    ''' <param name="menuFlg">メニューフラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetDropDownMenuList(ByVal subSystemId As String, ByVal menuFlg As String) As ToolStripMenuItem()

        'キャッシュデータよりメニューリスト内容を取得
        Dim rows As DataRow() = Me.GetMenuListData(subSystemId, menuFlg)

        Dim menuList As ToolStripMenuItem() = New ToolStripMenuItem(rows.Length - 1) {}
        Dim i As Integer = 0
        Dim max As Integer = rows.Length - 1
        For i = 0 To max
            menuList(i) = New ToolStripMenuItem()
            menuList(i).Name = rows(i).Item("FORM_ID").ToString
            menuList(i).Text = rows(i).Item("FORM_NM").ToString
            AddHandler menuList(i).Click, AddressOf Me._Frm.DropDownMenuList_Click
            menuList(i).ImageTransparentColor = Color.Red
        Next

        Return menuList

    End Function

    ''' <summary>
    ''' メニューリストに表示するデータを抽出します
    ''' </summary>
    ''' <param name="subSystemId">サブシステムID</param>
    ''' <param name="menuFlg">メニューフラグ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMenuListData(ByVal subSystemId As String, ByVal menuFlg As String) As DataRow()

        Dim filterOption As String = ""
        If subSystemId = LMA020C.SUB_SYSTEM_ID_LMM AndAlso menuFlg = LMA020C.MENU_FLG_NOMAL Then
            If LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() <> "75" Then
                ' LMM121 単価マスタメンテ(セット料金) は営業所が熊本物流センターの場合のみ有効とする。
                ' ⇒営業所が熊本物流センター以外の場合は除外する
                filterOption = "AND FORM_ID <> 'LMM121' "
            End If
        End If

        Dim rows As DataRow() = Me.GetLMCachedDataTable(LMConst.CacheTBL.MENU).Select(String.Concat("SUB_SYSTEM_ID = '", subSystemId, "' AND MENU_DISP_FLG = '", menuFlg, "' ", filterOption), "FORM_ID")

        Return rows

    End Function

#End Region 'Form

#End Region 'Method

End Class
