' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI550G : TSMC在庫照会
'  作  成  者       :  
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMH511Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI550G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI550F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _H As LMI550H

    ''' <summary>
    ''' スプレッド列定義体を格納するフィールド(列移動許可のため)
    ''' </summary>
    ''' <remarks></remarks>
    Friend objSprDef As Object = Nothing
    Friend sprDetailDef As sprDetailDefault


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI550F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Form"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm
            ' ヘッダ部
            .cmbEigyo.TabIndex = LMI550C.CtlTabIndex_MAIN.CMBEIGYO
            .cmbWare.TabIndex = LMI550C.CtlTabIndex_MAIN.CMBWARE
            .cmbSearchDate.TabIndex = LMI550C.CtlTabIndex_MAIN.CMBSEARCHDATE
            .imdSearchDateFrom.TabIndex = LMI550C.CtlTabIndex_MAIN.IMDSEARCHDATEFROM
            .imdSearchDateTo.TabIndex = LMI550C.CtlTabIndex_MAIN.IMDSEARCHDATETO
            .chkMiSeikyu.TabIndex = LMI550C.CtlTabIndex_MAIN.CHKMISEIKYU
            .txtCustCdL.TabIndex = LMI550C.CtlTabIndex_MAIN.TXTCUSTCDL
            .txtCustCdM.TabIndex = LMI550C.CtlTabIndex_MAIN.TXTCUSTCDM
            .chkZai.TabIndex = LMI550C.CtlTabIndex_MAIN.CHKZAI
            .chkOutKa.TabIndex = LMI550C.CtlTabIndex_MAIN.CHKOUTKA
            .chkReZai.TabIndex = LMI550C.CtlTabIndex_MAIN.CHKREZAI
            .chkReOutKa.TabIndex = LMI550C.CtlTabIndex_MAIN.CHKREOUTKA
            .chkHenpinOutKa.TabIndex = LMI550C.CtlTabIndex_MAIN.CHKHENPINOUTKA
            .numKeikaFrom.TabIndex = LMI550C.CtlTabIndex_MAIN.NUMKEIKAFROM
            .numKeikaTo.TabIndex = LMI550C.CtlTabIndex_MAIN.NUMKEIKATO

            ' 明細部
            .sprDetail.TabIndex = LMI550C.CtlTabIndex_MAIN.SPRDETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String, ByRef frm As LMI550F, ByVal sysdate As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールに初期値設定
        Call Me.SetInitControl(id, frm, sysdate)

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .imdSearchDateFrom.TextValue = String.Empty
            .imdSearchDateTo.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロールに初期値設定
    ''' </summary>
    ''' <param name="id"></param>
    ''' <param name="frm"></param>
    ''' <param name="sysdate"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitControl(ByVal id As String, ByRef frm As LMI550F, ByVal sysdate As String)

        'コントロールに初期値を設定
        With frm
            ' 営業所
            .cmbEigyo.SelectedValue() = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()
            .cmbEigyo.ReadOnly = True

            ' 倉庫
            .cmbWare.SelectedValue() = LM.Base.LMUserInfoManager.GetWhCd().ToString()

            ' 日付FROM
            .imdSearchDateFrom.TextValue = String.Empty

            ' 日付TO
            .imdSearchDateTo.TextValue = String.Empty

            ' 未請求
            .chkMiSeikyu.Checked = False

            ' 荷主コード(大)
            .txtCustCdL.TextValue = String.Empty

            ' 荷主コード(中)
            .txtCustCdM.TextValue = String.Empty

            ' 在庫
            .chkZai.Checked = True

            ' 出荷
            .chkOutKa.Checked = True

            ' 空在庫
            .chkReZai.Checked = True

            ' 空出荷
            .chkReOutKa.Checked = True

            ' 返品出荷
            .chkHenpinOutKa.Checked = True

            ' 経過日数FROM
            .numKeikaFrom.TextValue = String.Empty

            ' 経過日数TO
            .numKeikaTo.TextValue = String.Empty

        End With

        ' 初期荷主
        SetInitControlCust(frm)

    End Sub

    ''' <summary>
    ''' 荷主コード/荷主名 (大/中) 初期値設定
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub SetInitControlCust(ByRef frm As LMI550F)

        SetInitControlCust(frm, "", "")

    End Sub

    ''' <summary>
    ''' 荷主コード/荷主名 (大/中) 初期値設定
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Function SetInitControlCust(ByRef frm As LMI550F, ByVal custCdL As String, ByVal custCdM As String) As Boolean

        Dim ret As Boolean = False

        '初期荷主情報取得
        If custCdL.Trim().Length = 0 OrElse custCdM.Trim().Length = 0 Then
            Dim initCust As DataRow =
            MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST).AsEnumerable() _
            .Where(Function(r)
                       Return _
                       r.Item("SYS_DEL_FLG").Equals(LMConst.FLG.OFF) AndAlso
                       r.Item("USER_CD").Equals(LM.Base.LMUserInfoManager.GetUserID()) AndAlso
                       r.Item("DEFAULT_CUST_YN").Equals("01")
                   End Function) _
            .FirstOrDefault()
            If Not initCust Is Nothing Then
                custCdL = initCust.Item("CUST_CD_L").ToString()
                custCdM = initCust.Item("CUST_CD_M").ToString()
            End If
        End If

        Dim drCust As DataRow()
        Dim where As New Text.StringBuilder()
        If True Then
            where.Append(String.Concat("    NRS_BR_CD = '" & LM.Base.LMUserInfoManager.GetNrsBrCd().ToString() & "' "))
        End If
        If custCdL.Trim().Length > 0 Then
            where.Append(String.Concat("AND CUST_CD_L = '" & custCdL & "' "))
        End If
        If custCdM.Trim().Length > 0 Then
            where.Append(String.Concat("AND CUST_CD_M = '" & custCdM & "' "))
        End If

        drCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(where.ToString())
        With Me._Frm
            If drCust.Count > 0 Then
                .txtCustCdL.TextValue = drCust(0).Item("CUST_CD_L").ToString()
                .txtCustCdL.BackColorDef() = LMGUIUtility.GetSystemInputBackColor()
                .txtCustCdM.TextValue = drCust(0).Item("CUST_CD_M").ToString()
                .txtCustCdM.BackColorDef() = LMGUIUtility.GetSystemInputBackColor()
                .txtCustNm.TextValue = drCust(0).Item("CUST_NM_L").ToString() & "  " & drCust(0).Item("CUST_NM_M").ToString()
                ret = True
            Else
                .txtCustNm.TextValue = ""
            End If
        End With

        Return ret

    End Function

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(mode As Integer)

        Call Me.SetFunctionKey(mode)
        Call Me.SetControlsStatus(mode)

    End Sub

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal mode As Integer)

        With Me._Frm.FunctionKey
            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            '表示名を設定
            .F1ButtonName = ""
            If Me.IsEditableUser() Then
                .F2ButtonName = "編集"
            Else
                .F2ButtonName = ""
            End If
            .F3ButtonName = ""
            .F4ButtonName = ""
            .F5ButtonName = ""
            .F6ButtonName = ""
            .F7ButtonName = ""
            .F8ButtonName = ""
            .F9ButtonName = "検索"
            .F10ButtonName = ""
            If Me.IsEditableUser() Then
                .F11ButtonName = "保存"
            Else
                .F11ButtonName = ""
            End If
            .F12ButtonName = "閉じる"

            ' 使用状態の設定
            .Enabled = True
            Select Case mode
                Case LMI550C.Mode.INT
                    ' 初期モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = True

                Case LMI550C.Mode.REF
                    ' 参照モード
                    .F1ButtonEnabled = False
                    If Me.IsEditableUser() Then
                        .F2ButtonEnabled = True
                    Else
                        .F2ButtonEnabled = False
                    End If
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = True
                    .F10ButtonEnabled = False
                    .F11ButtonEnabled = False
                    .F12ButtonEnabled = True

                Case LMI550C.Mode.EDT
                    ' 編集モード
                    .F1ButtonEnabled = False
                    .F2ButtonEnabled = False
                    .F3ButtonEnabled = False
                    .F4ButtonEnabled = False
                    .F5ButtonEnabled = False
                    .F6ButtonEnabled = False
                    .F7ButtonEnabled = False
                    .F8ButtonEnabled = False
                    .F9ButtonEnabled = True
                    .F10ButtonEnabled = False
                    If Me.IsEditableUser() Then
                        .F11ButtonEnabled = True
                    Else
                        .F11ButtonEnabled = False
                    End If
                    .F12ButtonEnabled = True

            End Select
        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal mode As Integer)

        With Me._Frm
            Select Case mode
                Case LMI550C.Mode.INT
                    ' 初期モード

                Case LMI550C.Mode.REF
                    ' 参照モード

                Case LMI550C.Mode.EDT
                    ' 編集モード

            End Select
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(ヘッダー部の先頭)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocusHeader()

        With Me._Frm
            .cmbEigyo.Focus()
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(明細部の先頭)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFocusDetail()

        With Me._Frm
            .sprDetail.Focus()
        End With

    End Sub

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDefault

        ' 見出し/幅/表示状態
        Public CUST_GOODS_CD As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.CUST_GOODS_CD, "商品コード", 120, True)
        Public GOODS_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.GOODS_NM, "商品名", 200, True)
        Public LVL1_UT As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LVL1_UT, "荷姿", 60, True)
        Public LOT_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LOT_NO, "ロット№", 120, True)
        Public STATUS_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.STATUS_NM, "ステータス", 70, True)
        Public INKA_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.INKA_DATE, "入荷日", 80, True)
        Public OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.OUTKA_PLAN_DATE, "出荷日", 80, True)
        Public RTN_INKA_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.RTN_INKA_DATE, "空入荷日", 80, True)
        Public RTN_OUTKA_PLAN_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.RTN_OUTKA_PLAN_DATE, "空出荷日", 80, True)
        Public LAST_INV_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LAST_INV_DATE, "最終請求日", 80, True)
        Public UP_FLG_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.UP_FLG_NM, "最新区分", 70, True)
        Public RETURN_FLAG_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.RETURN_FLAG_NM, "回収区分", 70, True)
        Public SUPPLY_CD As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.SUPPLY_CD, "サプライヤーコード", 120, True)
        Public ASN_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.ASN_NO, "ASN No.", 120, True)
        Public TSMC_REC_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.TSMC_REC_NO, "TSMC在庫番号", 120, True)
        Public GRLVL1_PPNID As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.GRLVL1_PPNID, "個品ラベル", 120, True)
        Public PLT_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.PLT_NO, "パレットNo.", 120, True)
        Public DEPLT_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.DEPLT_NO, "出庫パレットNo.", 120, True)
        Public LV2_SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LV2_SERIAL_NO, "シリアルNo.", 120, True)
        Public CYLINDER_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.CYLINDER_NO, "容器番号", 120, True)
        Public STOCK_TYPE_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.STOCK_TYPE_NM, "検査状態", 120, True)
        Public KEIKA_DAYS As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.KEIKA_DAYS, "経過日数", 80, True)
        Public LT_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LT_DATE, "使用期限", 80, True)
        Public LVL1_CHECK As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LVL1_CHECK, "検査番号1", 120, True)
        Public LVL2_CHECK As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LVL2_CHECK, "検査番号2", 120, True)
        Public LAST_CLC_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LAST_CLC_DATE, "最終セット" & vbCrLf & "料金計算日", 80, True)
        Public WH_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.WH_NM, "倉庫", 80, True)
        Public JISSEKI_SHORI_FLG_IN_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.JISSEKI_SHORI_FLG_IN_NM, "入荷送信実績", 130, True)
        Public JISSEKI_SHORI_FLG_OUT_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.JISSEKI_SHORI_FLG_OUT_NM, "出荷送信実績", 130, True)
        Public TOU_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.TOU_NO, "棟", 30, True)
        Public SITU_NO As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.SITU_NO, "室", 30, True)
        Public TOU_SITU_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.TOU_SITU_NM, "棟室名", 200, True)
        Public ZONE_CD As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.ZONE_CD, "ZONE", 30, True)
        Public ZONE_NM As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.ZONE_NM, "ZONE名", 60, True)
        Public LOCA As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.LOCA, "ロケーション", 80, True)

        '**** 隠し列 ****
        Public NRS_WH_CD As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.NRS_WH_CD, "倉庫(コード)", 0, False)
        Public STATUS As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.STATUS, "ステータス(コード)", 0, False)
        Public STOCK_TYPE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.STOCK_TYPE, "検査状態(コード)", 0, False)
        Public UP_FLG As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.UP_FLG, "最新区分(コード)", 0, False)
        Public RETURN_FLAG As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.RETURN_FLAG, "回収区分(コード)", 0, False)
        Public JISSEKI_SHORI_FLG_IN As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.JISSEKI_SHORI_FLG_IN, "入荷送信実績(コード)", 0, False)
        Public JISSEKI_SHORI_FLG_OUT As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.JISSEKI_SHORI_FLG_OUT, "出荷送信実績(コード)", 0, False)
        Public SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI550C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        Dim dtJissekiShoriFlg As New DataTable
        dtJissekiShoriFlg.Columns.Add("JISSEKI_SHORI_FLG", GetType(String))
        dtJissekiShoriFlg.Columns.Add("JISSEKI_SHORI_FLG_NM", GetType(String))
        Dim drJissekiShoriFlg As DataRow
        drJissekiShoriFlg = dtJissekiShoriFlg.NewRow()
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG") = "X"
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG_NM") = "未登録"
        dtJissekiShoriFlg.Rows.Add(drJissekiShoriFlg)
        drJissekiShoriFlg = dtJissekiShoriFlg.NewRow()
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG") = "0"
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG_NM") = "送信対象外"
        dtJissekiShoriFlg.Rows.Add(drJissekiShoriFlg)
        drJissekiShoriFlg = dtJissekiShoriFlg.NewRow()
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG") = "1"
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG_NM") = "送信対象"
        dtJissekiShoriFlg.Rows.Add(drJissekiShoriFlg)
        drJissekiShoriFlg = dtJissekiShoriFlg.NewRow()
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG") = "2"
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG_NM") = "実績送信エラー"
        dtJissekiShoriFlg.Rows.Add(drJissekiShoriFlg)
        drJissekiShoriFlg = dtJissekiShoriFlg.NewRow()
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG") = "3"
        drJissekiShoriFlg.Item("JISSEKI_SHORI_FLG_NM") = "実績送信済"
        dtJissekiShoriFlg.Rows.Add(drJissekiShoriFlg)

        With spr
            ' スプレッドをクリア
            .CrearSpread()

            ' 列数設定
            .ActiveSheet.ColumnCount = LMI550C.SprColumnIndex.LAST

            ' スプレッドの列設定（見出し／幅／表示状態／列移動許可）
            objSprDef = New sprDetailDefault
            .SetColProperty(objSprDef, True)
            sprDetailDef = DirectCast(objSprDef, sprDetailDefault)

            ' 列固定位置を設定(ex.荷主名で固定)
            .ActiveSheet.FrozenColumnCount = sprDetailDef.LOT_NO.ColNo + 1

            ' 先頭検索行のセルスタイル設定
            .SetCellStyle(0, sprDetailDef.CUST_GOODS_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, sprDetailDef.GOODS_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 60, False))
            .SetCellStyle(0, sprDetailDef.LVL1_UT.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprDetailDef.STATUS_NM.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.INKA_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.OUTKA_PLAN_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.RTN_INKA_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.RTN_OUTKA_PLAN_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.LAST_INV_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.UP_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "A008", False))
            .SetCellStyle(0, sprDetailDef.RETURN_FLAG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "U009", False))
            .SetCellStyle(0, sprDetailDef.SUPPLY_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 10, False))
            .SetCellStyle(0, sprDetailDef.ASN_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 40, False))
            .SetCellStyle(0, sprDetailDef.TSMC_REC_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 10, False))
            .SetCellStyle(0, sprDetailDef.GRLVL1_PPNID.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 100, False))
            .SetCellStyle(0, sprDetailDef.PLT_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprDetailDef.DEPLT_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprDetailDef.LV2_SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False))
            .SetCellStyle(0, sprDetailDef.CYLINDER_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 40, False))
            .SetCellStyle(0, sprDetailDef.STOCK_TYPE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "T037", False))
            .SetCellStyle(0, sprDetailDef.KEIKA_DAYS.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.LT_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.LVL1_CHECK.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 80, False))
            .SetCellStyle(0, sprDetailDef.LVL2_CHECK.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 80, False))
            .SetCellStyle(0, sprDetailDef.LAST_CLC_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.WH_NM.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.JISSEKI_SHORI_FLG_IN_NM.ColNo, LMSpreadUtility.GetComboCell(spr, New DataView(dtJissekiShoriFlg), "JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG_NM", False))
            .SetCellStyle(0, sprDetailDef.JISSEKI_SHORI_FLG_OUT_NM.ColNo, LMSpreadUtility.GetComboCell(spr, New DataView(dtJissekiShoriFlg), "JISSEKI_SHORI_FLG", "JISSEKI_SHORI_FLG_NM", False))
            .SetCellStyle(0, sprDetailDef.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 2, False))
            .SetCellStyle(0, sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 2, False))
            .SetCellStyle(0, sprDetailDef.TOU_SITU_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX, 60, False))
            .SetCellStyle(0, sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_HANKAKU, 2, False))
            .SetCellStyle(0, sprDetailDef.ZONE_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 60, False))
            .SetCellStyle(0, sprDetailDef.LOCA.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 10, False))

            '**** 隠し列 ****
            .SetCellStyle(0, sprDetailDef.NRS_WH_CD.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.STATUS.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.STOCK_TYPE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.UP_FLG.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.RETURN_FLAG.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.JISSEKI_SHORI_FLG_IN.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.JISSEKI_SHORI_FLG_OUT.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
            .SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
        End With

    End Sub

    ''' <summary>
    ''' スプレッドに取得データをセット
    ''' </summary>
    ''' <param name="dt">DataTable</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        '取得データ件数
        Dim dataCnt As Integer = dt.Rows.Count()

        '取得データが0件なら抜ける
        If dataCnt = 0 Then
            Exit Sub
        End If

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim sCheck As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
        Dim sLabelR As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
        Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, True, 0, True, ",")
        Dim sDate As StyleInfo = LMSpreadUtility.GetDateTimeCell(spr, True, CellType.DateTimeFormat.ShortDate)

        With spr
            '描画中断
            .SuspendLayout()

            'スプレッドのカレント行
            Dim spIdx As Integer = 0

            ' 取得データのループ
            For dtIdx As Integer = 1 To dataCnt
                Dim dr As DataRow = dt.Rows(dtIdx - 1)

                .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)
                spIdx += 1

                ' 追加行のセルスタイル設定
                .SetCellStyle(spIdx, sprDetailDef.CUST_GOODS_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LVL1_UT.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.STATUS_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.INKA_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.OUTKA_PLAN_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.RTN_INKA_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.RTN_OUTKA_PLAN_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LAST_INV_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.UP_FLG_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.RETURN_FLAG_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SUPPLY_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.ASN_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.TSMC_REC_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.GRLVL1_PPNID.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.PLT_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.DEPLT_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LV2_SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.CYLINDER_NO.ColNo, sLabel)
                If Me.IsEditableUser() Then
                    .SetCellStyle(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(spr, "T037", False))
                Else
                    .SetCellStyle(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo, sLabel)
                End If
                .SetCellStyle(spIdx, sprDetailDef.KEIKA_DAYS.ColNo, sNum)
                .SetCellStyle(spIdx, sprDetailDef.LT_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LVL1_CHECK.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LVL2_CHECK.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LAST_CLC_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.WH_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SITU_NO.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.TOU_SITU_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.ZONE_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.ZONE_NM.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.LOCA.ColNo, sLabel)

                '**** 隠し列 ****
                .SetCellStyle(spIdx, sprDetailDef.NRS_WH_CD.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.STATUS.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.STOCK_TYPE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.UP_FLG.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.RETURN_FLAG.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(spIdx, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)

                ' セル値の設定
                .SetCellValue(spIdx, sprDetailDef.CUST_GOODS_CD.ColNo, dr.Item("CUST_GOODS_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.GOODS_NM.ColNo, dr.Item("GOODS_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.LVL1_UT.ColNo, dr.Item("LVL1_UT").ToString())
                .SetCellValue(spIdx, sprDetailDef.LOT_NO.ColNo, dr.Item("LOT_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.STATUS_NM.ColNo, dr.Item("STATUS_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.INKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("INKA_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.RTN_INKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("RTN_INKA_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.RTN_OUTKA_PLAN_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("RTN_OUTKA_PLAN_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.LAST_INV_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LAST_INV_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.UP_FLG_NM.ColNo, dr.Item("UP_FLG_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.RETURN_FLAG_NM.ColNo, dr.Item("RETURN_FLAG_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.SUPPLY_CD.ColNo, dr.Item("SUPPLY_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.ASN_NO.ColNo, dr.Item("ASN_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.TSMC_REC_NO.ColNo, dr.Item("TSMC_REC_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.GRLVL1_PPNID.ColNo, dr.Item("GRLVL1_PPNID").ToString())
                .SetCellValue(spIdx, sprDetailDef.PLT_NO.ColNo, dr.Item("PLT_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.DEPLT_NO.ColNo, dr.Item("DEPLT_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.LV2_SERIAL_NO.ColNo, dr.Item("LV2_SERIAL_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.CYLINDER_NO.ColNo, dr.Item("CYLINDER_NO").ToString())
                If Me.IsEditableUser() Then
                    .SetCellValue(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo, dr.Item("STOCK_TYPE").ToString())
                Else
                    .SetCellValue(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo, dr.Item("STOCK_TYPE_NM").ToString())
                End If
                .SetCellValue(spIdx, sprDetailDef.KEIKA_DAYS.ColNo, dr.Item("KEIKA_DAYS").ToString())
                .SetCellValue(spIdx, sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LT_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.LVL1_CHECK.ColNo, dr.Item("LVL1_CHECK").ToString())
                .SetCellValue(spIdx, sprDetailDef.LVL2_CHECK.ColNo, dr.Item("LVL2_CHECK").ToString())
                .SetCellValue(spIdx, sprDetailDef.LAST_CLC_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("LAST_CLC_DATE").ToString()))
                .SetCellValue(spIdx, sprDetailDef.WH_NM.ColNo, dr.Item("WH_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN_NM.ColNo, dr.Item("JISSEKI_SHORI_FLG_IN_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT_NM.ColNo, dr.Item("JISSEKI_SHORI_FLG_OUT_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.TOU_NO.ColNo, dr.Item("TOU_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.SITU_NO.ColNo, dr.Item("SITU_NO").ToString())
                .SetCellValue(spIdx, sprDetailDef.TOU_SITU_NM.ColNo, dr.Item("TOU_SITU_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.ZONE_CD.ColNo, dr.Item("ZONE_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.ZONE_NM.ColNo, dr.Item("ZONE_NM").ToString())
                .SetCellValue(spIdx, sprDetailDef.LOCA.ColNo, dr.Item("LOCA").ToString())

                '**** 隠し列 ****
                .SetCellValue(spIdx, sprDetailDef.NRS_WH_CD.ColNo, dr.Item("NRS_WH_CD").ToString())
                .SetCellValue(spIdx, sprDetailDef.STATUS.ColNo, dr.Item("STATUS").ToString())
                .SetCellValue(spIdx, sprDetailDef.STOCK_TYPE.ColNo, dr.Item("STOCK_TYPE").ToString())
                .SetCellValue(spIdx, sprDetailDef.UP_FLG.ColNo, dr.Item("UP_FLG").ToString())
                .SetCellValue(spIdx, sprDetailDef.RETURN_FLAG.ColNo, dr.Item("RETURN_FLAG").ToString())
                .SetCellValue(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN.ColNo, dr.Item("JISSEKI_SHORI_FLG_IN").ToString())
                .SetCellValue(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT.ColNo, dr.Item("JISSEKI_SHORI_FLG_OUT").ToString())
                .SetCellValue(spIdx, sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(spIdx, sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
            Next

            '描画再開
            .ResumeLayout(True)
        End With

    End Sub

    ''' <summary>
    ''' スプレッドの編集状態を制御
    ''' </summary>
    ''' <param name="mode">処理モード</param>
    ''' <param name="spIdx">対象行番号</param>
    ''' <param name="endAfter">True:終了アクション後,False:通常時</param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadEdit(ByVal mode As Integer, ByVal spIdx As Integer, Optional ByVal endAfter As Boolean = False)

        With Me._Frm.sprDetail
            ' 描画中断
            .SuspendLayout()

            Dim readOnlyBackColor As System.Drawing.Color = Utility.LMGUIUtility.GetReadOnlyBackColor
            Dim inputBackColor As System.Drawing.Color = Color.White

            Select Case mode
                Case LMI550C.Mode.REF, LMI550C.Mode.EDT
                    ' 参照モード、編集モード
                    .ActiveSheet.Cells(spIdx, sprDetailDef.CUST_GOODS_CD.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.CUST_GOODS_CD.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.GOODS_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.GOODS_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LVL1_UT.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LVL1_UT.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LOT_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LOT_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.STATUS_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.STATUS_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.INKA_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.INKA_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.OUTKA_PLAN_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.OUTKA_PLAN_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RTN_INKA_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RTN_INKA_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RTN_OUTKA_PLAN_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RTN_OUTKA_PLAN_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LAST_INV_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LAST_INV_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.UP_FLG_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.UP_FLG_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RETURN_FLAG_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RETURN_FLAG_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SUPPLY_CD.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SUPPLY_CD.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ASN_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ASN_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TSMC_REC_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TSMC_REC_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.GRLVL1_PPNID.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.GRLVL1_PPNID.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.PLT_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.PLT_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.DEPLT_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.DEPLT_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LV2_SERIAL_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LV2_SERIAL_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.CYLINDER_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.CYLINDER_NO.ColNo).BackColor = readOnlyBackColor
                    If mode = LMI550C.Mode.EDT Then
                        .ActiveSheet.Cells(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo).Locked = False
                        .ActiveSheet.Cells(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo).BackColor = inputBackColor
                    Else
                        .ActiveSheet.Cells(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo).Locked = True
                        .ActiveSheet.Cells(spIdx, sprDetailDef.STOCK_TYPE_NM.ColNo).BackColor = readOnlyBackColor
                    End If
                    .ActiveSheet.Cells(spIdx, sprDetailDef.KEIKA_DAYS.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.KEIKA_DAYS.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LT_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LT_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LVL1_CHECK.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LVL1_CHECK.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LVL2_CHECK.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LVL2_CHECK.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LAST_CLC_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LAST_CLC_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.WH_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.WH_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TOU_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TOU_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SITU_NO.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SITU_NO.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TOU_SITU_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.TOU_SITU_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ZONE_CD.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ZONE_CD.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ZONE_NM.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.ZONE_NM.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LOCA.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.LOCA.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.NRS_WH_CD.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.NRS_WH_CD.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.STATUS.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.STATUS.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.STOCK_TYPE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.STOCK_TYPE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.UP_FLG.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.UP_FLG.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RETURN_FLAG.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.RETURN_FLAG.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_IN.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.JISSEKI_SHORI_FLG_OUT.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SYS_UPD_DATE.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SYS_UPD_DATE.ColNo).BackColor = readOnlyBackColor
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SYS_UPD_TIME.ColNo).Locked = True
                    .ActiveSheet.Cells(spIdx, sprDetailDef.SYS_UPD_TIME.ColNo).BackColor = readOnlyBackColor

            End Select

            '描画再開
            .ResumeLayout(True)
        End With

    End Sub

#End Region 'Form

#Region "共用メソッド"

    Private Function IsEditableUser() As Boolean

        Dim kengen As String = Jp.Co.Nrs.LM.Base.LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = False

        Select Case kengen
            Case LMConst.AuthoKBN.VIEW : kengenFlg = False
            Case LMConst.AuthoKBN.EDIT : kengenFlg = True
            Case LMConst.AuthoKBN.EDIT_UP : kengenFlg = True
            Case LMConst.AuthoKBN.LEADER : kengenFlg = True
            Case LMConst.AuthoKBN.MANAGER : kengenFlg = True
            Case LMConst.AuthoKBN.AGENT : kengenFlg = False
        End Select

        Return kengenFlg

    End Function

#End Region ' "共用メソッド"

End Class
