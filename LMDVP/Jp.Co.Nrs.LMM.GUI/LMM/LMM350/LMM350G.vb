' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタメンテ
'  プログラムID     :  LMM350G : 初期出荷元マスタ
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMM350Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMM350G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM350F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM350F)

        '親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付ける。
        MyBase.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

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

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

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

            .grpSearch.TabIndex = LMM350C.CtlTabIndex.GRP_KENSAKU
            .cmbCustCd.TabIndex = LMM350C.CtlTabIndex.CMB_NINUSHI
            .chkSoukoMisettei.TabIndex = LMM350C.CtlTabIndex.CHK_MISETTEI
            .txtDestZip.TabIndex = LMM350C.CtlTabIndex.TXT_YUBIN_BANGO
            .grpSokoSet.TabIndex = LMM350C.CtlTabIndex.GRP_SETTEI
            .cmbSoko.TabIndex = LMM350C.CtlTabIndex.CMB_SOKO
            .btnIkkatsu.TabIndex = LMM350C.CtlTabIndex.BTN_SETTEI
            .sprDetail.TabIndex = LMM350C.CtlTabIndex.SPR_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        '**********TODO:削除予定　日付型の設定はここで行う*******************
        ''コントロールの日付書式設定
        'Call Me.SetDateControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

        '**********TODO:削除予定　数値型の設定はここで行う*******************
        'numberCellの桁数を設定する
        ' Me._Frm.numExpPrimaryExRate.SetInputFields("##,##0.000000", , 5, 1, , 6, 6, , Convert.ToDecimal(99999.999999), Convert.ToDecimal(0))

    End Sub

    ''' <summary>
    ''' コンボボックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        '区分マスタ検索処理（荷主コンボ設定用）
        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim sort As String = "KBN_CD"
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S032, "' AND SYS_DEL_FLG = '0'"), sort)

        Dim max As Integer = getDr.Length - 1
        Me._Frm.cmbCustCd.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

        For i As Integer = 0 To max

            item = getDr(i).Item("KBN_NM3").ToString()
            cd = getDr(i).Item("KBN_NM1").ToString()

            Me._Frm.cmbCustCd.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        Next

        Me.CreateSokoComboBox()

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        Me._Frm.grpSearch.Focus()
        Me._Frm.cmbCustCd.Focus()

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbCustCd.SelectedValue = String.Empty
            .chkSoukoMisettei.Checked = False
            .txtDestZip.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 倉庫コンボボックス作成
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateSokoComboBox()

        Dim custCd As String = String.Empty
        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim whereBrCd As String = String.Empty
        Dim custCdselected As String = String.Empty

        Me._Frm.cmbSoko.Items.Clear()

        Me._Frm.cmbSoko.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))


        custCdselected = Me._Frm.cmbCustCd.SelectedValue.ToString()
        If String.IsNullOrEmpty(custCdselected) = True Then
            custCd = LMM350C.ScmCustCdBP
        Else
            custCd = custCdselected
        End If

        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S033, "' AND KBN_NM3 = '", custCd, "' AND SYS_DEL_FLG = '0'"))

        Dim max As Integer = getDr.Length - 1
        Dim whereOR As String = String.Empty
        For i As Integer = 0 To max
            If String.IsNullOrEmpty(whereBrCd) = False Then
                whereOR = " OR "
            End If
            whereBrCd = String.Concat(whereBrCd, whereOR, " NRS_BR_CD = '", getDr(i).Item("KBN_NM4").ToString(), "'")

        Next
        whereBrCd = String.Concat("(", whereBrCd, ")")


        '倉庫マスタ検索処理
        Dim sort As String = "WH_CD"
        getDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat(whereBrCd, " AND SYS_DEL_FLG = '0'"), sort)
        max = getDr.Count - 1
        For i As Integer = 0 To max

            cd = getDr(i).Item("WH_CD").ToString()
            item = getDr(i).Item("WH_NM").ToString()

            Me._Frm.cmbSoko.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

#End Region

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '*****表示列*****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.CHECK, " ", 20, True)
        Public Shared JIS_CD As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.JIS_CD, "JISコード", 80, True)
        Public Shared JIS_NM As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.JIS_NM, "JIS名", 705, True)
        Public Shared SOKO_NM As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.SOKO_NM, "倉庫名", 300, True)
        '*****隠し列*****
        Public Shared SOKO_CD As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.SOKO_CD, "", 0, False)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.UPD_FLG, "", 86, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.UPD_DATE, "", 86, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM350C.SprColumnIndex.UPD_TIME, "", 86, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 8

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM350G.sprDetailDef())

            ''列固定位置を設定する。
            '.sprDetail.ActiveSheet.FrozenColumnCount = LMM350G.sprDetailDef.DEF.ColNo + 1

            '列設定用変数
            Dim spr As LMSpreadSearch = .sprDetail
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            '*****表示列*****
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.DEF.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.JIS_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.JIS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 50, False))
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.SOKO_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False))
            '*****隠し列*****
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.SOKO_CD.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.UPD_FLG.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
            .sprDetail.SetCellStyle(0, LMM350G.sprDetailDef.SYS_UPD_TIME.ColNo, lbl)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM350F)

        With frm.sprDetail.ActiveSheet

            '*****表示列*****
            .Cells(0, LMM350G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMM350G.sprDetailDef.JIS_CD.ColNo).Value = String.Empty
            .Cells(0, LMM350G.sprDetailDef.JIS_NM.ColNo).Value = String.Empty
            .Cells(0, LMM350G.sprDetailDef.SOKO_NM.ColNo).Value = String.Empty
            '*****隠し列*****
            .Cells(0, LMM350G.sprDetailDef.SOKO_CD.ColNo).Value = String.Empty
            .Cells(0, LMM350G.sprDetailDef.UPD_FLG.ColNo).Value = String.Empty
            .Cells(0, LMM350G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .Cells(0, LMM350G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

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
            Dim check As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '*****表示列*****
                .SetCellStyle(i, LMM350G.sprDetailDef.DEF.ColNo, check)
                .SetCellStyle(i, LMM350G.sprDetailDef.JIS_CD.ColNo, lbl)
                .SetCellStyle(i, LMM350G.sprDetailDef.JIS_NM.ColNo, lbl)
                .SetCellStyle(i, LMM350G.sprDetailDef.SOKO_NM.ColNo, lbl)
                '*****隠し列*****
                .SetCellStyle(i, LMM350G.sprDetailDef.SOKO_CD.ColNo, lbl)
                .SetCellStyle(i, LMM350G.sprDetailDef.UPD_FLG.ColNo, lbl)
                .SetCellStyle(i, LMM350G.sprDetailDef.SYS_UPD_DATE.ColNo, lbl)
                .SetCellStyle(i, LMM350G.sprDetailDef.SYS_UPD_TIME.ColNo, lbl)

                'セル値設定
                '*****表示列*****
                .SetCellValue(i, LMM350G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM350G.sprDetailDef.JIS_CD.ColNo, dr.Item("JIS_CD").ToString())
                .SetCellValue(i, LMM350G.sprDetailDef.JIS_NM.ColNo, dr.Item("JIS_NM").ToString())
                .SetCellValue(i, LMM350G.sprDetailDef.SOKO_NM.ColNo, dr.Item("WH_NM").ToString())
                '*****隠し列*****
                .SetCellValue(i, LMM350G.sprDetailDef.SOKO_CD.ColNo, dr.Item("WH_CD").ToString())
                .SetCellValue(i, LMM350G.sprDetailDef.UPD_FLG.ColNo, dr.Item("UPD_FLG").ToString())
                .SetCellValue(i, LMM350G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM350G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">書式設定を行うコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate)

        '**********TODO:削除予定　日付コントロールの型設定がある場合のみ使用*********************
        'EX)
        'ctl.Format = DateFieldsBuilder.BuildFields("ddMMMyyyy")
        'ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("dd/MMM/yyyy")

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

End Class
