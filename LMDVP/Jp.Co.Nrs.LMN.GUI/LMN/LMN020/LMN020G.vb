' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN020G   : 出荷データ詳細
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMN020Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMN020G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN020F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN020F)

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
    Friend Sub SetFunctionKey(ByVal errorFlg As Boolean)

        Dim always As Boolean = True
        Dim errorCase As Boolean = True
        If errorFlg = True Then
            errorCase = False
        End If

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = "削除"
            .F5ButtonName = String.Empty
            .F6ButtonName = "初期化"
            .F7ButtonName = String.Empty
            .F8ButtonName = "在庫照会"
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = errorCase
            .F5ButtonEnabled = False
            .F6ButtonEnabled = errorCase
            .F7ButtonEnabled = False
            .F8ButtonEnabled = errorCase
            .F9ButtonEnabled = False
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

            .grpShukka_L.TabIndex = LMN020C.CtlTabIndex.GRP_SHUKKA
            .cmbWare.TabIndex = LMN020C.CtlTabIndex.SOKO
            .cmbStatus.TabIndex = LMN020C.CtlTabIndex.STASUS
            .cmbCustCd.TabIndex = LMN020C.CtlTabIndex.NINUSHI
            .lblOrderNO.TabIndex = LMN020C.CtlTabIndex.ORDER_NO
            .cmbMoushiOkuriKbn.TabIndex = LMN020C.CtlTabIndex.MOSHIOKURI
            .imdShukkaPlanDate.TabIndex = LMN020C.CtlTabIndex.SHUKKA_BI
            .imdNounyuDate.TabIndex = LMN020C.CtlTabIndex.NONYU_BI
            .lblEDITorikomiDate.TabIndex = LMN020C.CtlTabIndex.EDI_TORIKOMI
            .lblDest_Nm.TabIndex = LMN020C.CtlTabIndex.TODOKESAKI_NM
            .lblDEST_ZIP.TabIndex = LMN020C.CtlTabIndex.TODOKESAKI_ADD
            .lblDEST_AD.TabIndex = LMN020C.CtlTabIndex.TODOKESAKI_ADD_NM
            .lblDest_Tel.TabIndex = LMN020C.CtlTabIndex.TODOKESAKI_TEL
            .lblBiko.TabIndex = LMN020C.CtlTabIndex.BIKO
            .sprGoods.TabIndex = LMN020C.CtlTabIndex.SPR_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

        'コントロールの日付書式設定
        Call Me.SetDateControl()

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
        For i As Integer = 0 To max

            item = getDr(i).Item("KBN_NM3").ToString()
            cd = getDr(i).Item("KBN_NM1").ToString()

            Me._Frm.cmbCustCd.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        Next

    End Sub

    ''' <summary>
    ''' 取得データをHEADER部に表示
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHeader(ByVal dt As DataTable)

        Dim dr As DataRow = dt.Rows(0)

        'EDI取り込み日時設定
        Dim EDIDate As String = dr.Item("EDI_DATE").ToString
        If EDIDate.Length <> 8 Then
            EDIDate = String.Empty
        Else
            EDIDate = DateFormatUtility.EditSlash(EDIDate) 'スラッシュ編集
        End If
        dr.Item("EDI_DATETIME") = Me.EditConcatData(EDIDate, dr.Item("EDI_TIME").ToString, " ")
        '出荷日設定
        Dim ShukkaPDate As String = dr.Item("OUTKA_DATE").ToString()
        If ShukkaPDate.Length <> 8 Then
            ShukkaPDate = String.Empty
        End If
        '納入日設定
        Dim ArrDate As String = dr.Item("ARR_DATE").ToString()
        If ArrDate.Length <> 8 Then
            ArrDate = String.Empty
        End If

        With Me._Frm

            .cmbWare.SelectedValue = dr.Item("SOKO_CD")
            .cmbStatus.SelectedValue = dr.Item("STATUS_KBN")
            .cmbCustCd.SelectedValue = dr.Item("SCM_CUST_CD")
            .lblOrderNO.TextValue = dr.Item("CUST_ORD_NO_L").ToString()
            .cmbMoushiOkuriKbn.SelectedValue = dr.Item("MOUSHIOKURI_KBN")
            .imdShukkaPlanDate.TextValue = ShukkaPDate
            .imdNounyuDate.TextValue = ArrDate
            .lblEDITorikomiDate.TextValue = dr.Item("EDI_DATETIME").ToString
            .lblDest_Nm.TextValue = dr.Item("DEST_NM").ToString()
            .lblDEST_ZIP.TextValue = dr.Item("DEST_ZIP").ToString()
            .lblDEST_AD.TextValue = dr.Item("DEST_AD").ToString()
            .lblDest_Tel.TextValue = dr.Item("DEST_TEL").ToString()
            .lblBiko.TextValue = dr.Item("REMARK").ToString()

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(Optional ByVal errorFlg As Boolean = False)

        Call Me.SetFunctionKey(errorFlg)

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        Me._Frm.grpShukka_L.Focus()

    End Sub

    ''' <summary>
    ''' 画面項目をロックする
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetErrorLock()

        Call Me.SetLockControl(Me._Frm, True)

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbWare.SelectedValue = String.Empty
            .cmbStatus.SelectedValue = String.Empty
            .cmbCustCd.SelectedValue = String.Empty
            .lblOrderNO.TextValue = String.Empty
            .cmbMoushiOkuriKbn.SelectedValue = String.Empty
            .imdShukkaPlanDate.TextValue = String.Empty
            .imdNounyuDate.TextValue = String.Empty
            .lblEDITorikomiDate.TextValue = String.Empty
            .lblDest_Nm.TextValue = String.Empty
            .lblDEST_ZIP.TextValue = String.Empty
            .lblDEST_AD.TextValue = String.Empty
            .lblDest_Tel.TextValue = String.Empty
            .lblBiko.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Call Me.SetDateFormat(.imdShukkaPlanDate)
            Call Me.SetDateFormat(.imdNounyuDate)

        End With

    End Sub

#End Region

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprGOODSDef

        'スプレッド(タイトル列)の設定
        '*****表示列*****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMN020C.SprColumnIndex.CHECK, " ", 20, True)
        Public Shared SHOUHIN_CD As SpreadColProperty = New SpreadColProperty(LMN020C.SprColumnIndex.SHOHIN_CD, "顧客商品コード", 120, True)
        Public Shared SHOUHIN_NM As SpreadColProperty = New SpreadColProperty(LMN020C.SprColumnIndex.SHOHIN_NM, "商品名", 300, True)
        Public Shared SHUKKA_KOSU As SpreadColProperty = New SpreadColProperty(LMN020C.SprColumnIndex.SHUKKA_KOSU, "出荷個数", 120, True)
        Public Shared ZAIKO_KOSU As SpreadColProperty = New SpreadColProperty(LMN020C.SprColumnIndex.ZAIKO_KOSU, "在庫個数", 120, True)
        Public Shared BIKO As SpreadColProperty = New SpreadColProperty(LMN020C.SprColumnIndex.BIKO, "備考", 250, True)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprGoods.CrearSpread()

            '列数設定
            .sprGoods.ActiveSheet.ColumnCount = 6

            'スプレッドの列設定
            .sprGoods.SetColProperty(New LMN020G.sprGOODSDef())

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprGoods

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim check As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numShukka As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999, True, , , ",")
            Dim numZaiko As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 9999999999, True, , , ",")

            Dim dr As DataRow

            '値設定
            Dim max As Integer = lngcnt - 1
            For i As Integer = 0 To max

                dr = dt.Rows(i)

                'セルスタイル設定
                '*****表示列*****
                .SetCellStyle(i, LMN020G.sprGOODSDef.DEF.ColNo, check)
                .SetCellStyle(i, LMN020G.sprGOODSDef.SHOUHIN_CD.ColNo, lbl)
                .SetCellStyle(i, LMN020G.sprGOODSDef.SHOUHIN_NM.ColNo, lbl)
                .SetCellStyle(i, LMN020G.sprGOODSDef.SHUKKA_KOSU.ColNo, numShukka)
                .SetCellStyle(i, LMN020G.sprGOODSDef.ZAIKO_KOSU.ColNo, numZaiko)
                .SetCellStyle(i, LMN020G.sprGOODSDef.BIKO.ColNo, lbl)

                'セル値設定
                '*****表示列*****
                .SetCellValue(i, LMN020G.sprGOODSDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMN020G.sprGOODSDef.SHOUHIN_CD.ColNo, dr.Item("CUST_GOODS_CD").ToString())
                .SetCellValue(i, LMN020G.sprGOODSDef.SHOUHIN_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(i, LMN020G.sprGOODSDef.SHUKKA_KOSU.ColNo, dr.Item("OUTKA_TTL_NB").ToString())
                .SetCellValue(i, LMN020G.sprGOODSDef.ZAIKO_KOSU.ColNo, dr.Item("PORA_ZAI_NB").ToString())
                .SetCellValue(i, LMN020G.sprGOODSDef.BIKO.ColNo, dr.Item("BIKO_DTL").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl"></param>
    ''' <remarks></remarks>
    Private Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMMdd")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM/dd")

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

    ''' <summary>
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Private Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = " - ") As String

        EditConcatData = value1
        If String.IsNullOrEmpty(EditConcatData) = True Then

            EditConcatData = value2

        Else

            If String.IsNullOrEmpty(value2) = False Then

                EditConcatData = String.Concat(EditConcatData, str, value2)

            End If

        End If

        Return EditConcatData

    End Function

#End Region

#End Region

End Class
