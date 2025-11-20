' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU010G   : 文書管理画面
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.DSL
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Com.Utility.DateFormatUtility
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMU010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMU010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMU010F

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks></remarks>
    Friend objSprDef As Object = Nothing
    Private sprDocDef As sprDataDef


    '検索条件保持用
    Friend KEY_TYPE_USED As String = String.Empty
    Friend KEY_NM_USED As String = String.Empty

    'イベント種別
    Private Const EVENT_EDIT As String = "編集"
    Private Const EVENT_SAVE As String = "保存"
    Private Const EVENT_CLOSE As String = "閉じる"

    '遷移有無フラグ
    Private Const ACT_FLG_MENU As String = "0"
    Private Const ACT_FLG_ETC As String = "1"




#Region "タブインデックス用列挙体(コントロール)"

    ''' <summary>
    ''' タブインデックス用列挙体(コントロール)
    ''' </summary>
    ''' <remarks></remarks>
    Private Enum CtlTabIndex

        SystemID = 0
        CTRtypekbn
        KeyType
        KeyNo
        Add
        Delete
        CTRNo
        sprData

    End Enum

#End Region 'タブインデックス用列挙体(コントロール)

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMU010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

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
    Friend Sub SetFunctionKey(ByVal mode As String)

        Dim always As Boolean = False
        Dim lock As Boolean = False
        Dim edit1 As Boolean = False
        Dim edit2 As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            'モードによる制御
            If mode = DispMode.INIT Then
                '初期モード
                always = True

            ElseIf mode = DispMode.VIEW Then
                '照会モード
                always = True
                edit1 = True

            ElseIf mode = DispMode.EDIT Then
                '編集モード
                always = True
                edit2 = True

            Else
                '画面をロックする場合
                '全てFalse
            End If

            .Enabled = True
            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = EVENT_EDIT
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = String.Empty
            .F11ButtonName = EVENT_SAVE
            .F12ButtonName = EVENT_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = edit1
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = edit2
            .F12ButtonEnabled = always

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

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbSystemID.TabIndex = CtlTabIndex.SystemID
            .cmbKeyType.TabIndex = CtlTabIndex.KeyType
            .txtKeyNo.TabIndex = CtlTabIndex.KeyNo
            .btnAdd.TabIndex = CtlTabIndex.Add
            .btnDelete.TabIndex = CtlTabIndex.Delete
            .sprData.TabIndex = CtlTabIndex.sprData


        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()



    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal mode As String, Optional ByVal paramDS As DataSet = Nothing)

        With Me._Frm

            '画面項目を全ロック解除する
            CType(.cmbSystemID, Nrs.Win.GUI.Win.Interface.IEditableControl).ReadOnlyStatus = True
            CType(.cmbKeyType, Nrs.Win.GUI.Win.Interface.IEditableControl).ReadOnlyStatus = True
            CType(.txtKeyNo, Nrs.Win.GUI.Win.Interface.IEditableControl).ReadOnlyStatus = True
            CType(.cmbSystemID, Nrs.Win.GUI.Win.Interface.IEditableControl).EnableStatus = False
            CType(.cmbKeyType, Nrs.Win.GUI.Win.Interface.IEditableControl).EnableStatus = False
            CType(.txtKeyNo, Nrs.Win.GUI.Win.Interface.IEditableControl).EnableStatus = False

            .btnAdd.Enabled = False
            .btnDelete.Enabled = False

            .cmbSystemID.SelectedValue = String.Empty
            .cmbKeyType.SelectedValue = String.Empty
            .txtKeyNo.TextValue = String.Empty

            If paramDS Is Nothing = False AndAlso _
               paramDS.Tables(LMControlC.LMU010C_TABLE_NM_IN) Is Nothing = False AndAlso _
               paramDS.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows.Count > 0 Then

                If (DispMode.VIEW.Equals(mode) = True) Then
                    '参照モード時制御

                    If paramDS.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows(0).Item("ACT_FLG").Equals(ACT_FLG_ETC) = False Then
                        CType(.cmbSystemID, Nrs.Win.GUI.Win.Interface.IEditableControl).ReadOnlyStatus = False
                        CType(.cmbKeyType, Nrs.Win.GUI.Win.Interface.IEditableControl).ReadOnlyStatus = False
                        CType(.txtKeyNo, Nrs.Win.GUI.Win.Interface.IEditableControl).ReadOnlyStatus = False
                        CType(.cmbSystemID, Nrs.Win.GUI.Win.Interface.IEditableControl).EnableStatus = True
                        CType(.cmbKeyType, Nrs.Win.GUI.Win.Interface.IEditableControl).EnableStatus = True
                        CType(.txtKeyNo, Nrs.Win.GUI.Win.Interface.IEditableControl).EnableStatus = True
                    End If

                    '使用可能Buttonのロックを解除する
                    .btnAdd.Enabled = True
                    .btnDelete.Enabled = True

                End If

                .cmbSystemID.SelectedValue = paramDS.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows(0).Item("ENT_SYSID_KBN").ToString
                .cmbKeyType.SelectedValue = paramDS.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows(0).Item("KEY_TYPE_KBN").ToString
                .txtKeyNo.TextValue = paramDS.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows(0).Item("KEY_NO").ToString

            End If

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        Me._Frm.cmbSystemID.Focus()

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .cmbSystemID.TextValue = String.Empty
            .cmbKeyType.TextValue = String.Empty
            .txtKeyNo.TextValue = String.Empty
        End With

        With Me._Frm.sprData.Sheets(0)
            .Cells(0, sprDataDef.DEF.ColNo).Value = String.Empty
            .Cells(0, sprDataDef.FILE_TYPE.ColNo).Value = String.Empty
            .Cells(0, sprDataDef.FILE_NAME.ColNo).Value = String.Empty
            .Cells(0, sprDataDef.REMARK.ColNo).Value = String.Empty
            .Cells(0, sprDataDef.FILE_PATH.ColNo).Value = String.Empty
            .Cells(0, sprDataDef.SYS_ENT_USER.ColNo).Value = String.Empty
            .Cells(0, sprDataDef.SYS_ENT_DATE.ColNo).Value = String.Empty
        End With

    End Sub

#End Region

#End Region 'Form

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDataDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Def, " ", 20, True)
        Public Shared FILE_NO As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Fileno, "", 0, False)
        Public Shared SYSTEM As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.System, "", 0, False)
        Public Shared SYSTEM_NM As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.SystemNm, "System", 60, False)
        Public Shared FILE_TYPE As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Filetype, "ファイルタイプ", 140, True)
        Public Shared FILE_NAME As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Filename, "ファイル名", 240, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Remark, "備考", 300, True)
        Public Shared FILE_PATH As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Filepath, "File path", 0, False)
        Public Shared SYS_ENT_USER As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Sysentuser, "作成者", 120, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Sysentdate, "作成日", 90, True)
        Public Shared UPD_FLG As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Updflg, "", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Sysupddate, "", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.Sysupdtime, "", 0, False)
        Public Shared RECORDNO As SpreadColProperty = New SpreadColProperty(LMU010C.SprColNoIndex.RecordNo, "", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprData.CrearSpread()

            '列数設定
            .sprData.Sheets(0).ColumnCount = 14

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprData.SetColProperty(New sprDataDef)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet, ByVal comboDs As DataSet, ByVal mode As String, Optional ByVal arr As ArrayList = Nothing, Optional ByVal flg As Boolean = False)

        Dim spr As LMSpread = Me._Frm.sprData

        With spr

            .SuspendLayout()

            'データ挿入
            '行数設定
            Dim tbl As DataTable = ds.Tables(LMU010C.TABLE_NM_OUT)
            Dim lngcnt As Integer = tbl.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If
            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = Spread.LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Spread.LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sTextBox As StyleInfo = Spread.LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 40, False)
            Dim sComboBox As StyleInfo = Spread.LMSpreadUtility.GetComboCellKbn(spr, "F014", False)
            Dim sCombo As StyleInfo = Me.StyleInfoFileType(comboDs, spr)
            Dim sDate As StyleInfo = Spread.LMSpreadUtility.GetDateTimeCell(spr, True)

            Dim dRow As DataRow
            Dim defValue As String = "False"
            Dim filetypeValueNM As String = "FILE_TYPE_NM"

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dRow = tbl.Rows(i)

                'セルスタイル設定
                If mode.Equals(DispMode.EDIT) Then

                    sDEF.Locked = True

                    If arr.Contains(i) Then                                            '編集対象
                        defValue = "True"
                        If flg = False Then
                            filetypeValueNM = "FILE_TYPE_KBN"
                            .SetCellStyle(i, sprDataDef.FILE_TYPE.ColNo, sCombo)
                            .SetCellStyle(i, sprDataDef.REMARK.ColNo, sTextBox)
                            .SetCellStyle(i, sprDataDef.FILE_NAME.ColNo, sLabel)
                        Else
                            filetypeValueNM = "FILE_TYPE_NM"
                            .SetCellStyle(i, sprDataDef.FILE_TYPE.ColNo, sLabel)
                            .SetCellStyle(i, sprDataDef.REMARK.ColNo, sLabel)
                            .SetCellStyle(i, sprDataDef.FILE_NAME.ColNo, sLabel)
                        End If
                    Else                                                               '編集対象外
                        defValue = "False"
                        filetypeValueNM = "FILE_TYPE_NM"
                        .SetCellStyle(i, sprDataDef.FILE_TYPE.ColNo, sLabel)
                        .SetCellStyle(i, sprDataDef.REMARK.ColNo, sLabel)
                        .SetCellStyle(i, sprDataDef.FILE_NAME.ColNo, sLabel)
                    End If
                Else                                                                   '参照モード時
                    .SetCellStyle(i, sprDataDef.FILE_TYPE.ColNo, sLabel)
                    .SetCellStyle(i, sprDataDef.REMARK.ColNo, sLabel)
                    .SetCellStyle(i, sprDataDef.FILE_NAME.ColNo, sLabel)
                End If

                .SetCellStyle(i, sprDataDef.DEF.ColNo, sDEF)                       'DEF
                .SetCellStyle(i, sprDataDef.FILE_PATH.ColNo, sLabel)               'FILE_PATH
                .SetCellStyle(i, sprDataDef.SYS_ENT_USER.ColNo, sLabel)            'SYS_ENT_USER
                .SetCellStyle(i, sprDataDef.SYS_ENT_DATE.ColNo, sDate)             'SYS_ENT_DATE

                .SetCellStyle(i, sprDataDef.SYSTEM.ColNo, sLabel)
                .SetCellStyle(i, sprDataDef.SYSTEM_NM.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDataDef.DEF.ColNo, defValue)                                                     'checkbox
                .SetCellValue(i, sprDataDef.SYSTEM.ColNo, dRow.Item("ENT_SYSID_KBN").ToString)                              'SYSTEM
                .SetCellValue(i, sprDataDef.SYSTEM_NM.ColNo, dRow.Item("ENT_SYSID_NM").ToString)                              'SYSTEM
                .SetCellValue(i, sprDataDef.FILE_TYPE.ColNo, dRow.Item(filetypeValueNM).ToString)                    'FILE_TYPE
                .SetCellValue(i, sprDataDef.FILE_NAME.ColNo, dRow.Item("FILE_NM").ToString)                    'FILE_NAME
                .SetCellValue(i, sprDataDef.REMARK.ColNo, dRow.Item("REMARK").ToString)                              'REMARK
                .SetCellValue(i, sprDataDef.FILE_PATH.ColNo, dRow.Item("FILE_PATH").ToString & _
                                                             dRow.Item("FILE_NM").ToString)                          'FILE_PATH
                .SetCellValue(i, sprDataDef.UPD_FLG.ColNo, dRow.Item("UPD_FLG").ToString)                            'UPD_FLG
                .SetCellValue(i, sprDataDef.SYS_ENT_USER.ColNo, dRow.Item("SYS_ENT_USER_NM").ToString)               'SYS_ENT_USER
                .SetCellValue(i, sprDataDef.SYS_ENT_DATE.ColNo, EditSlashEUR(dRow.Item("SYS_ENT_DATE").ToString))    'SYS_ENT_DATE

                .SetCellStyle(i, sprDataDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDataDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, sprDataDef.FILE_NO.ColNo, sLabel)
                .SetCellValue(i, sprDataDef.SYS_UPD_DATE.ColNo, dRow.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, sprDataDef.SYS_UPD_TIME.ColNo, dRow.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprDataDef.FILE_NO.ColNo, dRow.Item("FILE_NO").ToString())

                .SetCellStyle(i, sprDataDef.RECORDNO.ColNo, sLabel)
                .SetCellValue(i, sprDataDef.RECORDNO.ColNo, i.ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、0 or 1を返却
            If Me.changeBooleanCheckBox(aCell.Text) = True Then
                GetCellValue = BaseConst.FLG.ON
            Else
                GetCellValue = BaseConst.FLG.OFF
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType Then

            '日付の場合、Value値を yyyyMMdd に変換して返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = Convert.ToDateTime(aCell.Value).ToString("yyyyMMdd")
            End If

        Else

            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

    ''' <summary>
    ''' チェックボックスの値をString型からBoolean型に変換する
    ''' </summary>
    ''' <param name="textValue">obj.text(0:チェック無し,1:チェック有り)</param>
    ''' <returns>True:チェック有り,False:チェック無し</returns>
    ''' <remarks></remarks>
    Friend Function changeBooleanCheckBox(ByVal textValue As String) As Boolean

        '"1"の場合Trueを返却
        If (LMConst.FLG.ON.Equals(textValue) = True) _
            OrElse True.ToString().Equals(textValue) = True Then
            Return True
        End If

        '"0"の場合Falseを返却
        Return False

    End Function

#End Region 'Spread

#Region "ユーティリティ"

    ''' <summary>
    ''' LMU010のコンボボックスの構築をします。 2015/1/6 ｱﾙﾍﾞ対応
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateCombo(ByVal ds As DataSet)

        Dim Dt As DataTable = ds.Tables(LMU010C.TABLE_NM_COMBO)  'システムID

        Dim dr As DataRow = Dt.NewRow()
        dr(LMU010C.KBN_CD) = String.Empty
        dr(LMU010C.KBN_NM) = String.Empty
        Dt.Rows.InsertAt(dr, 0)

        With Me._Frm.cmbSystemID
            .DataSource = Dt
            .ValueMember = LMU010C.KBN_CD
            .DisplayMember = LMU010C.KBN_NM
            .CreateComboBoxData()
        End With

        Dim kDt As DataTable = ds.Tables(LMU010C.TABLE_NM_KANRITYPE)  '管理タイプ

        Dim kdr As DataRow = kDt.NewRow()
        kdr(LMU010C.KBN_KCD) = String.Empty
        kdr(LMU010C.KBN_KNM) = String.Empty
        kDt.Rows.InsertAt(kdr, 0)

        With Me._Frm.cmbKeyType
            .DataSource = kDt
            .ValueMember = LMU010C.KBN_KCD
            .DisplayMember = LMU010C.KBN_KNM
            .CreateComboBoxData()
        End With

    End Sub

#End Region 'ユーティリティ

    ''' <summary>
    ''' セルのプロパティを設定(Container)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoFileType(ByVal ds As DataSet, ByVal spr As LMSpread) As StyleInfo

        Dim fDt As DataTable = ds.Tables(LMU010C.TABLE_NM_FILETYPE).Copy
        Dim dv As DataView = fDt.DefaultView

        Return LMSpreadUtility.GetComboCell(spr _
                                          , dv _
                                          , LMU010C.KBN_FCD _
                                          , LMU010C.KBN_FNM, False)

    End Function

#End Region 'Method

End Class
