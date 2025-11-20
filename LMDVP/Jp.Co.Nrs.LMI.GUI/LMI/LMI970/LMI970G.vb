' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI970G : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors.Fields

''' <summary>
''' LMI970Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI970G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI970F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI970F, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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
    Friend Sub SetFunctionKey(ByVal mode As String)

        Dim usable As Boolean = True
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "実績作成"
            .F2ButtonName = LMFControlC.FUNCTION_HENSHU
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMFControlC.FUNCTION_KENSAKU
            .F10ButtonName = LMFControlC.FUNCTION_POP
            .F11ButtonName = LMFControlC.FUNCTION_HOZON
            .F12ButtonName = LMFControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            Select Case mode
                Case DispMode.INIT
                    .F1ButtonEnabled = usable
                    .F2ButtonEnabled = lock
                    .F3ButtonEnabled = lock
                    .F4ButtonEnabled = lock
                    .F5ButtonEnabled = lock
                    .F6ButtonEnabled = lock
                    .F7ButtonEnabled = lock
                    .F8ButtonEnabled = lock
                    .F9ButtonEnabled = usable
                    .F10ButtonEnabled = usable
                    .F11ButtonEnabled = lock
                    .F12ButtonEnabled = usable

                Case DispMode.VIEW
                    .F1ButtonEnabled = usable
                    .F2ButtonEnabled = usable
                    .F3ButtonEnabled = lock
                    .F4ButtonEnabled = lock
                    .F5ButtonEnabled = lock
                    .F6ButtonEnabled = lock
                    .F7ButtonEnabled = lock
                    .F8ButtonEnabled = lock
                    .F9ButtonEnabled = usable
                    .F10ButtonEnabled = usable
                    .F11ButtonEnabled = lock
                    .F12ButtonEnabled = usable

                Case DispMode.EDIT
                    .F1ButtonEnabled = lock
                    .F2ButtonEnabled = lock
                    .F3ButtonEnabled = lock
                    .F4ButtonEnabled = lock
                    .F5ButtonEnabled = lock
                    .F6ButtonEnabled = lock
                    .F7ButtonEnabled = lock
                    .F8ButtonEnabled = lock
                    .F9ButtonEnabled = usable
                    .F10ButtonEnabled = lock
                    .F11ButtonEnabled = usable
                    .F12ButtonEnabled = usable

            End Select

            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)

        End With

    End Sub

#End Region 'FunctionKey

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbEigyo.TabIndex = LMI970C.CtlTabIndex.NRS_BR_CD
            .txtCustCdL.TabIndex = LMI970C.CtlTabIndex.CUST_CD_L
            .lblCustNm.TabIndex = LMI970C.CtlTabIndex.CUST_NM
            .imdTorikomiDateFrom.TabIndex = LMI970C.CtlTabIndex.TORIKOMI_DATE_FROM
            .imdTorikomiDateTo.TabIndex = LMI970C.CtlTabIndex.TORIKOMI_DATE_TO
            .cmbSearchDateKb.TabIndex = LMI970C.CtlTabIndex.SEARCH_DATE_KB
            .imdSearchDateFrom.TabIndex = LMI970C.CtlTabIndex.SEARCH_DATE_FROM
            .imdSearchDateTo.TabIndex = LMI970C.CtlTabIndex.SEARCH_DATE_TO
            .cmbPrint.TabIndex = LMI970C.CtlTabIndex.CMB_PRINT
            .btnPrint.TabIndex = LMI970C.CtlTabIndex.BTN_PRINT
            'ADD START 2019/05/30 要望管理006030
            .pnlUpdateTanka.TabIndex = LMI970C.CtlTabIndex.PNL_UPDATE_TANKA
            .imdTargetYM.TabIndex = LMI970C.CtlTabIndex.TARGET_YM
            .numTanka.TabIndex = LMI970C.CtlTabIndex.TANKA
            .btnUpdateTanka.TabIndex = LMI970C.CtlTabIndex.BTN_UPDATE_TANKA
            'ADD END   2019/05/30 要望管理006030
            .sprDetail.TabIndex = LMI970C.CtlTabIndex.DETAIL

        End With

    End Sub

    ''' <summary>
    ''' ヘッダの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetHeaderControls(ByVal mode As String)

        With Me._frm

            'ボタンの制御
            Select Case mode
                Case DispMode.INIT, DispMode.VIEW
                    .cmbEigyo.Enabled = True
                    .txtCustCdL.Enabled = True
                    .imdTorikomiDateFrom.Enabled = True
                    .imdTorikomiDateTo.Enabled = True
                    .cmbSearchDateKb.Enabled = True
                    .imdSearchDateFrom.Enabled = True
                    .imdSearchDateTo.Enabled = True
                    .cmbPrint.Enabled = True
                    .btnPrint.Enabled = True
                    .pnlUpdateTanka.Enabled = True      'ADD 2019/05/30 要望管理006030

                Case DispMode.EDIT
                    .cmbEigyo.Enabled = False
                    .txtCustCdL.Enabled = False
                    .imdTorikomiDateFrom.Enabled = False
                    .imdTorikomiDateTo.Enabled = False
                    .cmbSearchDateKb.Enabled = False
                    .imdSearchDateFrom.Enabled = False
                    .imdSearchDateTo.Enabled = False
                    .cmbPrint.Enabled = False
                    .btnPrint.Enabled = False
                    .pnlUpdateTanka.Enabled = False     'ADD 2019/05/30 要望管理006030

            End Select

        End With

    End Sub
    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'ADD START 2019/05/30 要望管理006030
        '単価一括変更 -> 対象年月 の書式設定
        Me._Frm.imdTargetYM.Format = DateFieldsBuilder.BuildFields("yyyyMM")
        Me._Frm.imdTargetYM.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

        '単価一括変更 -> 単価 の書式設定
        Me._Frm.numTanka.SetInputFields("##0.00", , 3, 1, , 2, 2, , CDec(999.99), CDec(0.0))
        'ADD END   2019/05/30 要望管理006030

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal mode As String)

        With Me._Frm

            .Focus()

            Select Case mode
                Case DispMode.INIT, DispMode.VIEW
                    .imdTorikomiDateFrom.Focus()

                Case DispMode.EDIT
                    .sprDetail.Focus()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbEigyo.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .lblCustNm.TextValue = String.Empty
            .imdTorikomiDateFrom.TextValue = String.Empty
            .imdTorikomiDateTo.TextValue = String.Empty
            .cmbSearchDateKb.TextValue = String.Empty
            .imdSearchDateFrom.TextValue = String.Empty
            .imdSearchDateTo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm

            '営業所の値を設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd
            Me._Frm.cmbEigyo.ReadOnly = True

            '初期荷主の値を設定
            Dim custCdL As String = "00145"
            .txtCustCdL.TextValue = custCdL

            Dim drsCust As DataRow() = Me._LMFconG.SelectCustListDataRow(brCd, custCdL)

            If 0 < drsCust.Length Then
                .lblCustNm.TextValue = drsCust(0).Item("CUST_NM_L").ToString()
            End If

            'cmbSearchDateKbの値を設定
            .cmbSearchDateKb.Items.Add(LMI970C.CmbSearchDateKbItems.Blank)
            .cmbSearchDateKb.Items.Add(LMI970C.CmbSearchDateKbItems.ShukkaDate)
            .cmbSearchDateKb.Items.Add(LMI970C.CmbSearchDateKbItems.NonyuDate)
            .cmbSearchDateKb.SelectedIndex = 0

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SHORI_KB As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.SHORI_KB, "処理", 40, True)
        Public Shared HORYU_KB As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.HORYU_KB, "保留", 40, True)
        Public Shared PRINT_KB As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.PRINT_KB, "印刷", 40, True)    'ADD 2019/05/30 要望管理006030
        Public Shared SEND_KB As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.SEND_KB, "送信", 40, True)
        Public Shared KOJO_KANRI_NO As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.KOJO_KANRI_NO, "工場管理番号", 100, True)
        Public Shared SHUKKA_DATE As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.SHUKKA_DATE, "出荷日", 80, True)
        Public Shared NONYU_DATE As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.NONYU_DATE, "納入日", 80, True)
        Public Shared DEST_KAISHA_NM As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.DEST_KAISHA_NM, "届先名", 200, True)            'MOD 2019/05/30 列幅変更
        Public Shared DEST_KAISHA_JUSHO As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.DEST_KAISHA_JUSHO, "届先住所", 195, True)    'MOD 2019/05/30 列幅変更
        Public Shared HINMEI As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.HINMEI, "品名", 110, True)       'MOD 2019/05/30 列幅変更
        Public Shared JURYO As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.JURYO, "重量", 80, True)          'ADD 2019/05/30 要望管理006030
        Public Shared TANKA As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.TANKA, "単価", 80, True)          'ADD 2019/05/30 要望管理006030
        Public Shared UNCHIN As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.UNCHIN, "運賃", 80, True)

        Public Shared CRT_DATE As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.CRT_DATE, "CRT_DATE", 0, False)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.FILE_NAME, "FILE_NAME", 0, False)
        Public Shared REC_NO As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.REC_NO, "REC_NO", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.SYS_UPD_DATE, "SYS_UPD_DATE", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI970C.SprColumnIndex.SYS_UPD_TIME, "SYS_UPD_TIME", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim spr As LMSpread = Me._Frm.sprDetail
        With spr

            'スプレッドの行をクリア
            .CrearSpread()
            .ActiveSheet.Rows.Count = 0

            '列数設定
            .ActiveSheet.ColumnCount = LMI970C.SprColumnIndex.LAST

            .SetColProperty(New LMI970G.sprDetailDef(), False)

            '列固定位置を設定
            .ActiveSheet.FrozenColumnCount = LMI970G.sprDetailDef.KOJO_KANRI_NO.ColNo + 1

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの文字色設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpreadColor()

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sht As FarPoint.Win.Spread.SheetView = spr.ActiveSheet
        Dim max As Integer = sht.Rows.Count - 1

        For i As Integer = 0 To max

            If Me._LMFconG.GetCellValue(sht.Cells(i, LMI970G.sprDetailDef.HORYU_KB.ColNo)) = "保留" Then
                '保留の場合
                sht.Rows(i).ForeColor = Color.Blue
            End If

            If CDec(Me._LMFconG.GetCellValue(sht.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo))) = CDec(0) Then
                '運賃0円の場合
                sht.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo).ForeColor = Color.Red
            End If

        Next


    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Function SetSpread(ByVal ds As DataSet) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMI970C.TABLE_NM_OUT)

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()
            .ActiveSheet.Rows.Count = 0

            .SuspendLayout()

            '----データ挿入----'
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Return True
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Me.StyleInfoChk(spr)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)
            Dim sNum12 As StyleInfo = Me.StyleInfoNum12(spr)
            sNum12.HorizontalAlignment = CellHorizontalAlignment.Right
            'ADD START 2019/05/30 要望管理006030
            Dim sNum9 As StyleInfo = Me.StyleInfoNum9(spr)
            sNum9.HorizontalAlignment = CellHorizontalAlignment.Right
            Dim sNum3Dec2 As StyleInfo = Me.StyleInfoNum3Dec2(spr)
            sNum3Dec2.HorizontalAlignment = CellHorizontalAlignment.Right
            'ADD END   2019/05/30 要望管理006030

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                .SetCellStyle(i, LMI970G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMI970G.sprDetailDef.SHORI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.HORYU_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.PRINT_KB.ColNo, sLabel)   'ADD 2019/05/30 要望管理006030
                .SetCellStyle(i, LMI970G.sprDetailDef.SEND_KB.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.KOJO_KANRI_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.SHUKKA_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.NONYU_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.DEST_KAISHA_NM.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.DEST_KAISHA_JUSHO.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.HINMEI.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.JURYO.ColNo, sNum12)      'ADD 2019/05/30 要望管理006030
                .SetCellStyle(i, LMI970G.sprDetailDef.TANKA.ColNo, sNum3Dec2) 'ADD 2019/05/30 要望管理006030
                .SetCellStyle(i, LMI970G.sprDetailDef.UNCHIN.ColNo, sNum9)      'MOD 2019/05/30 sNum12→sNum9

                .SetCellStyle(i, LMI970G.sprDetailDef.CRT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.FILE_NAME.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.REC_NO.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMI970G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMI970G.sprDetailDef.DEF.ColNo, False.ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.SHORI_KB.ColNo, dr.Item("SHORI_KB").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.HORYU_KB.ColNo, dr.Item("HORYU_KB").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.PRINT_KB.ColNo, dr.Item("PRINT_KB").ToString())   'ADD 2019/05/30 要望管理006030
                .SetCellValue(i, LMI970G.sprDetailDef.SEND_KB.ColNo, dr.Item("SEND_KB").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.KOJO_KANRI_NO.ColNo, dr.Item("KOJO_KANRI_NO").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.SHUKKA_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SHUKKA_DATE").ToString()))
                .SetCellValue(i, LMI970G.sprDetailDef.NONYU_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("NONYU_DATE").ToString()))
                .SetCellValue(i, LMI970G.sprDetailDef.DEST_KAISHA_NM.ColNo, dr.Item("DEST_KAISHA_NM").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.DEST_KAISHA_JUSHO.ColNo, dr.Item("DEST_KAISHA_JUSHO").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.HINMEI.ColNo, dr.Item("HINMEI").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.JURYO.ColNo, dr.Item("JURYO").ToString())         'ADD 2019/05/30 要望管理006030
                .SetCellValue(i, LMI970G.sprDetailDef.TANKA.ColNo, dr.Item("TANKA").ToString())         'ADD 2019/05/30 要望管理006030
                .SetCellValue(i, LMI970G.sprDetailDef.UNCHIN.ColNo, dr.Item("UNCHIN").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.CRT_DATE.ColNo, dr.Item("CRT_DATE").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.FILE_NAME.ColNo, dr.Item("FILE_NAME").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.REC_NO.ColNo, dr.Item("REC_NO").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMI970G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

    ''' <summary>
    ''' スプレッドのロック設定(明細)
    ''' </summary>
    ''' <param name="locked"></param>
    ''' <remarks></remarks>
    Friend Function SetSpreadLock(ByVal locked As Boolean) As Boolean

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim sht As SheetView = spr.ActiveSheet

        With spr

            .SuspendLayout()

            '値設定
            For i As Integer = 0 To sht.Rows.Count - 1

                'セルスタイル設定
                If Me._LMFconG.GetCellValue(sht.Cells(i, LMI970G.sprDetailDef.SEND_KB.ColNo)) = LMI970C.SendKbName.SoushinZumi Then
                    .ActiveSheet.Cells(i, LMI970G.sprDetailDef.JURYO.ColNo).Locked = True       'ADD 2019/05/30 要望管理006030
                    .ActiveSheet.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo).Locked = True

                    .ActiveSheet.Cells(i, LMI970G.sprDetailDef.JURYO.ColNo).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()     'ADD 2019/05/30 不具合修正
                    .ActiveSheet.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()    'ADD 2019/05/30 不具合修正
                Else
                    .ActiveSheet.Cells(i, LMI970G.sprDetailDef.JURYO.ColNo).Locked = locked     'ADD 2019/05/30 要望管理006030
                    .ActiveSheet.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo).Locked = locked
                    If locked = False Then
                        .ActiveSheet.Cells(i, LMI970G.sprDetailDef.JURYO.ColNo).BackColor = Color.White     'ADD 2019/05/30 要望管理006030
                        .ActiveSheet.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo).BackColor = Color.White    'MOD 2019/05/30 GetSpreadInputBackColor()→White
                    Else
                        .ActiveSheet.Cells(i, LMI970G.sprDetailDef.JURYO.ColNo).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()     'ADD 2019/05/30 要望管理006030
                        .ActiveSheet.Cells(i, LMI970G.sprDetailDef.UNCHIN.ColNo).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
                    End If
                End If
            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

    ''' <summary>
    ''' スプレッドのデータを更新
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="arr"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <remarks></remarks>
    Friend Function SetUpdSpread(ByVal frm As LMI970F, ByVal arr As ArrayList, ByVal eventShubetsu As LMI970C.EventShubetsu) As Boolean  'MOD 2019/05/30 引数eventShubetsu追加

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0
        With spr

            .SuspendLayout()

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                'ADD START 2019/05/30 要望管理006030
                Select Case eventShubetsu
                    Case LMI970C.EventShubetsu.JISSEKI_SAKUSEI
                        'ADD END   2019/05/30 要望管理006030

                        'セルに値を設定(送信)
                        .SetCellValue(rowNo, LMI970G.sprDetailDef.SEND_KB.ColNo, LMI970C.SendKbName.SoushinZumi)

                        'ADD START 2019/05/30 要望管理006030
                    Case LMI970C.EventShubetsu.PRINT

                        'セルに値を設定(印刷)
                        .SetCellValue(rowNo, LMI970G.sprDetailDef.PRINT_KB.ColNo, LMI970C.PrintKbName.Sumi)

                End Select
                'ADD END   2019/05/30 要望管理006030

            Next

            .ResumeLayout(True)

            Return True

        End With

    End Function

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

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum3Dec2(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999.99, True, 2, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")

    End Function
    'ADD END   2019/05/30 要望管理006030

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999, True, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数5桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum5(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 99999, True, 0, , ",")

    End Function

#End Region

#End Region

#End Region

End Class
