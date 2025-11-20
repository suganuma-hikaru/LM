' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : ＳＣＭ
'  プログラムID   : LMN010G : 出荷データ照会
'  作  成  者     : 佐川央
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports GrapeCity.Win.Editors

''' <summary>
''' LMN010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMN010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN010F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN010F)

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
            .F5ButtonName = "送信指示"
            .F6ButtonName = "欠品照会"
            .F7ButtonName = "欠品状態更新"
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
            .F5ButtonEnabled = always
            .F6ButtonEnabled = always
            .F7ButtonEnabled = always
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .grpSearch.TabIndex = 0
            .cmbStatus.TabIndex = 1
            .cmbCustCd.TabIndex = 2
            .imdEDITorikomiDate_From.TabIndex = 3
            .imdEDITorikomiDate_To.TabIndex = 4
            .imdShukkaDate_From.TabIndex = 5
            .imdShukkaDate_To.TabIndex = 6
            .imdNounyuDate_From.TabIndex = 7
            .imdNounyuDate_To.TabIndex = 8

            'GroupBox 
            .grpSokoSet.TabIndex = 9
            .cmbWare.TabIndex = 0
            .imdShukkaDate.TabIndex = 1
            .imdNounyuDate.TabIndex = 2

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

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
    ''' コントロールの入力制御
    ''' </summary>
    ''' <param name="lockFlg">モードによるロック制御を行う。省略時：初期モード</param>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(Optional ByVal lockFlg As String = LMN010C.MODE_DEFAULT)

        Dim noMnb As Boolean = True
        Dim dtTori As Boolean = True

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm


        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        With Me._Frm

        End With

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

        '区分マスタ検索処理（実績処理設定用）
        sort = "KBN_CD"
        getDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_J003, "' AND SYS_DEL_FLG = '0'"), sort)

        max = getDr.Length - 1
        For i As Integer = 0 To max

            item = getDr(i).Item("KBN_CD").ToString()
            cd = getDr(i).Item("KBN_NM1").ToString()

            Me._Frm.cmbJissekiAction.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        Next

        Me.CreateSokoComboBox()

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

        Me._Frm.cmbWare.Items.Clear()

        Me._Frm.cmbWare.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        custCdselected = Me._Frm.cmbCustCd.SelectedValue.ToString()
        If String.IsNullOrEmpty(custCdselected) = True Then
            custCd = LMN010C.ScmCustCdBP
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

            Me._Frm.cmbWare.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

#End Region

#Region "初期値設定"

    ''' <summary>
    ''' 初期値設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMN010F)

        'ヘッダー部初期値設定
        With Me._Frm

            .cmbStatus.SelectedValue = LMN010C.KbnCdMisettei
            .cmbCustCd.SelectedValue = LMN010C.ScmCustCdBP

        End With

        'Spread部初期値設定
        Call Me.SetSpreadInitValue(frm)

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
        Call Me.SetSpread(ds)

        'Dim getDt As DataTable = ds.Tables(LMN010C.TABLE_NM_OUT)

        'Me._Frm.txtMemno.TextValue = getDt.Rows(0).Item("KMMID").ToString

        'For index As Integer = 0 To (getDt.Rows().Count - 1)

        '    Dim strKNAPTA As String = getDt.Rows(index).Item("KNAPTA").ToString

        '    Select Case Mid(strKNAPTA, 1, 1)

        '        Case "1"
        '            '取得データをヘッダ部に表示
        '            Call SetHeaderData(strKNAPTA, "1")
        '        Case "2"
        '            '取得データをSpreadに表示
        '            Call Me.SetSpread(strKNAPTA)
        '        Case "8"
        '            '取得データをヘッダ部に表示
        '            Call SetHeaderData(strKNAPTA, "8")

        '    End Select
        'Next

    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

        With Me._Frm

            'Select Case strKBN
            '    Case "1"
            '        .txtType.TextValue = Get_Shubetu(Mid(strKNAPTA, 2, 2))      
            '        .txtHurikomi.TextValue = Trim(Mid(strKNAPTA, 2, 10))        
            '        .txtHurikomiNm.TextValue = Trim(Mid(strKNAPTA, 15, 40))     
            '        .txtTorikumi.TextValue = Trim(Mid(strKNAPTA, 55, 4))        
            '        .txtBankno.TextValue = Trim(Mid(strKNAPTA, 59, 4))          
            '        .txtBankNm.TextValue = Trim(Mid(strKNAPTA, 63, 15))         
            '        .txtShitenno.TextValue = Trim(Mid(strKNAPTA, 78, 3))        
            '        .txtShitenNm.TextValue = Trim(Mid(strKNAPTA, 81, 15))       
            '        .txtYokinsyu.TextValue = Get_Yokin(Mid(strKNAPTA, 96, 1))   
            '        .txtKozabango.TextValue = Trim(Mid(strKNAPTA, 97, 7))       
            '    Case "8"
            '        .txtTotalcnt.Value = CInt(Trim(Mid(strKNAPTA, 2, 6)))       
            '        .txtTotalKin.Value = CDec(Trim(Mid(strKNAPTA, 8, 12)))      
            'End Select

        End With

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(0, " ", 20, True)
        Public Shared SCM_CTL_NO_L As SpreadColProperty = New SpreadColProperty(1, "SCM受付番号L", 86, False)
        Public Shared SCM_CUST_CD As SpreadColProperty = New SpreadColProperty(2, "SCM荷主コード", 86, False)
        Public Shared KEPPIN_FLG As SpreadColProperty = New SpreadColProperty(3, "欠品", 30, True)
        Public Shared STATUS_KBN As SpreadColProperty = New SpreadColProperty(4, "ステータス", 100, True)
        Public Shared BR_CD As SpreadColProperty = New SpreadColProperty(5, "営業所コード", 86, False)
        Public Shared SOKO_NM As SpreadColProperty = New SpreadColProperty(6, "倉庫名", 180, True)
        Public Shared CUST_ORD_NO_L As SpreadColProperty = New SpreadColProperty(7, "オーダーＮＯ", 80, True)
        Public Shared MOUSHIOKURI_KBN As SpreadColProperty = New SpreadColProperty(8, "申送区分", 40, True)
        Public Shared OUTKA_DATE As SpreadColProperty = New SpreadColProperty(9, "出荷日", 90, True)
        Public Shared ARR_DATE As SpreadColProperty = New SpreadColProperty(10, "納入日", 90, True)
        Public Shared LMS_OUTKA_DATE As SpreadColProperty = New SpreadColProperty(11, "実出荷日", 90, True)
        Public Shared LMS_ARR_DATE As SpreadColProperty = New SpreadColProperty(12, "実納入日", 90, True)
        Public Shared DEST_NM As SpreadColProperty = New SpreadColProperty(13, "出荷先名", 200, True)
        Public Shared DEST_AD As SpreadColProperty = New SpreadColProperty(14, "出荷先住所", 300, True)
        Public Shared DEST_ZIP As SpreadColProperty = New SpreadColProperty(15, "出荷先〒", 70, True)
        Public Shared REMARK_1 As SpreadColProperty = New SpreadColProperty(16, "備考１", 200, True)
        Public Shared REMARK_2 As SpreadColProperty = New SpreadColProperty(17, "備考２", 200, True)
        Public Shared DTL_CNT As SpreadColProperty = New SpreadColProperty(18, "明細" & vbCrLf & "件数", 40, True)
        Public Shared EDI_DATETIME As SpreadColProperty = New SpreadColProperty(19, "ＥＤＩ取込日時", 150, True)
        Public Shared INSERT_FLG As SpreadColProperty = New SpreadColProperty(20, "新規取込フラグ", 86, False)
        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(21, "データ受信日", 86, False)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(22, "受信ファイル名", 200, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(23, "受信ファイル行数", 86, False)
        Public Shared HED_BP_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(24, "HED_BP排他用更新日", 86, False)
        Public Shared HED_BP_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(25, "HED_BP排他用更新時刻", 86, False)
        Public Shared L_SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(26, "L排他用更新日", 86, False)
        Public Shared L_SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(27, "L排他用更新時刻", 86, False)
        Public Shared SYS_ENT_USER As SpreadColProperty = New SpreadColProperty(28, "Created by", 86, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(29, "Created date", 86, False)
        Public Shared SYS_UPD_USER As SpreadColProperty = New SpreadColProperty(30, "Last edited by", 86, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(31, "Last edited date", 86, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(32, "Last edited time", 86, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.Sheets(0).ColumnCount = 33

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(納入日まで固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.ARR_DATE.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.DEF.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SCM_CTL_NO_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SCM_CUST_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.KEPPIN_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

            '2017/09/25 修正 李↓
            .sprDetail.SetCellStyle(0, sprDetailDef.STATUS_KBN.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                                                         lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                                                         False, "KBN_GROUP_CD", LM.Const.LMKbnConst.KBN_S031))
            '2017/09/25 修正 李↑

            .sprDetail.SetCellStyle(0, sprDetailDef.BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SOKO_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", False))
            .sprDetail.SetCellStyle(0, sprDetailDef.CUST_ORD_NO_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 30, False))

            '2017/09/25 修正 李↓
            .sprDetail.SetCellStyle(0, sprDetailDef.MOUSHIOKURI_KBN.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                                                              lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                                                              False, "KBN_GROUP_CD", LM.Const.LMKbnConst.KBN_M008))
            '2017/09/25 修正 李↑

            .sprDetail.SetCellStyle(0, sprDetailDef.OUTKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.ARR_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.LMS_OUTKA_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.LMS_ARR_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_AD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 80, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.DEST_ZIP.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 7, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.REMARK_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 30, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.REMARK_2.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 30, False))
            .sprDetail.SetCellStyle(0, sprDetailDef.DTL_CNT.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999, True, 0))
            .sprDetail.SetCellStyle(0, sprDetailDef.EDI_DATETIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.INSERT_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.CRT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.FILE_NAME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.REC_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.HED_BP_SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.HED_BP_SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.L_SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.L_SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_ENT_USER.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_USER.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

        End With

    End Sub

    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpreadInitValue(ByVal frm As LMN010F)

        With frm.sprDetail

            .Sheets(0).Cells(0, sprDetailDef.DEF.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SCM_CTL_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SCM_CUST_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.KEPPIN_FLG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.STATUS_KBN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.BR_CD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SOKO_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CUST_ORD_NO_L.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.MOUSHIOKURI_KBN.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.OUTKA_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.ARR_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LMS_OUTKA_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.LMS_ARR_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_NM.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_AD.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DEST_ZIP.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.REMARK_1.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.REMARK_2.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.DTL_CNT.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.EDI_DATETIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.INSERT_FLG.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.CRT_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.FILE_NAME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.REC_NO.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.HED_BP_SYS_UPD_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.HED_BP_SYS_UPD_TIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.L_SYS_UPD_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.L_SYS_UPD_TIME.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_ENT_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_USER.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .Sheets(0).Cells(0, sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            .Sheets(0).Rows.Count = 1
            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMN010C.TABLE_NM_OUT)
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = tbl.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.SCM_CTL_NO_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SCM_CUST_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KEPPIN_FLG.ColNo, sLabel)

                '2017/09/25 修正 李↓
                .SetCellStyle(i, sprDetailDef.STATUS_KBN.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                                                   lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                                                   True, "KBN_GROUP_CD", LM.Const.LMKbnConst.KBN_S031))
                '2017/09/25 修正 李↑

                .SetCellStyle(i, sprDetailDef.BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SOKO_NM.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.SOKO, "WH_CD", "WH_NM", True))
                .SetCellStyle(i, sprDetailDef.CUST_ORD_NO_L.ColNo, sLabel)

                '2017/09/25 修正 李↓
                .SetCellStyle(i, sprDetailDef.MOUSHIOKURI_KBN.ColNo, LMSpreadUtility.GetComboCellMaster(spr, LMConst.CacheTBL.KBN, "KBN_CD",
                                                                                                        lgm.Selector({"KBN_NM1", "KBN_NM11", "KBN_NM12", "KBN_NM13"}),
                                                                                                        True, "KBN_GROUP_CD", LM.Const.LMKbnConst.KBN_M008))
                '2017/09/25 修正 李↑

                .SetCellStyle(i, sprDetailDef.OUTKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ARR_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LMS_OUTKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LMS_ARR_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DEST_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DEST_AD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DEST_ZIP.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REMARK_1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REMARK_2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DTL_CNT.ColNo, LMSpreadUtility.GetNumberCell(spr, 0, 999, True, 0))
                .SetCellStyle(i, sprDetailDef.EDI_DATETIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INSERT_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FILE_NAME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REC_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HED_BP_SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HED_BP_SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.L_SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.L_SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_ENT_USER.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_USER.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMN010G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo, dRow.Item("SCM_CTL_NO_L").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.SCM_CUST_CD.ColNo, dRow.Item("SCM_CUST_CD").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.STATUS_KBN.ColNo, dRow.Item("STATUS_KBN").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.BR_CD.ColNo, dRow.Item("BR_CD").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.SOKO_NM.ColNo, dRow.Item("SOKO_CD").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.CUST_ORD_NO_L.ColNo, dRow.Item("CUST_ORD_NO_L").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.MOUSHIOKURI_KBN.ColNo, dRow.Item("MOUSHIOKURI_KBN").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.OUTKA_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("OUTKA_DATE").ToString()))
                '納入日に"000000"が設定されている場合、空を表示
                If dRow.Item("ARR_DATE").ToString() = "000000" Then
                    .SetCellValue(i, LMN010G.sprDetailDef.ARR_DATE.ColNo, String.Empty)
                Else
                    .SetCellValue(i, LMN010G.sprDetailDef.ARR_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("ARR_DATE").ToString()))
                End If
                .SetCellValue(i, LMN010G.sprDetailDef.LMS_OUTKA_DATE.ColNo, dRow.Item("LMS_OUTKA_DATE").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.LMS_ARR_DATE.ColNo, dRow.Item("LMS_ARR_DATE").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.DEST_NM.ColNo, dRow.Item("DEST_NM").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.DEST_AD.ColNo, dRow.Item("DEST_AD").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.DEST_ZIP.ColNo, dRow.Item("DEST_ZIP").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.REMARK_1.ColNo, dRow.Item("REMARK1").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.REMARK_2.ColNo, dRow.Item("REMARK2").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.DTL_CNT.ColNo, dRow.Item("DTL_CNT").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.EDI_DATETIME.ColNo, dRow.Item("EDI_DATETIME").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.INSERT_FLG.ColNo, dRow.Item("INSERT_FLG").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.CRT_DATE.ColNo, dRow.Item("CRT_DATE").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.FILE_NAME.ColNo, dRow.Item("FILE_NAME").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.REC_NO.ColNo, dRow.Item("REC_NO").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.HED_BP_SYS_UPD_DATE.ColNo, dRow.Item("HED_BP_SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.HED_BP_SYS_UPD_TIME.ColNo, dRow.Item("HED_BP_SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.L_SYS_UPD_DATE.ColNo, dRow.Item("L_SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMN010G.sprDetailDef.L_SYS_UPD_TIME.ColNo, dRow.Item("L_SYS_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドに欠品フラグを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadKeppinFlg(ByVal ds As DataSet)

        'データセットのOUT情報から欠品フラグを取得
        Dim outName As String = "LMN010OUT"
        Dim dt As DataTable = ds.Tables(outName)
        Dim outNum As Integer = dt.Rows.Count
        For i As Integer = 0 To outNum - 1
            Dim ScmCtlNoL As String = dt.Rows(i).Item("SCM_CTL_NO_L").ToString()
            Dim KeppinFlg As String = dt.Rows(i).Item("KEPPIN_FLG").ToString()
            Dim sprNum As Integer = Me._Frm.sprDetail.ActiveSheet.RowCount
            'Spred表示行をSCM管理番号で検索し、一致したレコードに欠品フラグを設定
            For j As Integer = 1 To sprNum - 1
                'Spread行のSCM管理番号を取得
                Dim SprScmCtlNoL As String = (Me._Frm.sprDetail.ActiveSheet.Cells(j, LMN010G.sprDetailDef.SCM_CTL_NO_L.ColNo).Value).ToString()
                If SprScmCtlNoL = ScmCtlNoL Then
                    '欠品フラグを設定
                    Me._Frm.sprDetail.SetCellValue(j, LMN010G.sprDetailDef.KEPPIN_FLG.ColNo, KeppinFlg)
                    Exit For
                End If
            Next
        Next

    End Sub


#End Region 'Spread

#End Region

End Class
