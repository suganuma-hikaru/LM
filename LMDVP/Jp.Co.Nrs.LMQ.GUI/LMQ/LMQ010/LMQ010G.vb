' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMQ010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMQ010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMQ010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMQconG As LMQControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal handlerClass As LMBaseGUIHandler, ByVal frm As LMQ010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        MyForm = frm

        _Frm = frm

        'Gamen共通クラスの設定
        _LMQconG = New LMQControlG(DirectCast(frm, Form))

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


        With _Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = "新  規"
            .F2ButtonName = "編　集"
            .F3ButtonName = String.Empty
            .F4ButtonName = "削除・復活"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = "Excel作成"
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"

            'ロック制御変数
            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F3ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F4ButtonEnabled = view
            .F10ButtonEnabled = view
            .F11ButtonEnabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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

        With _Frm

            'Main
            .sprPattern.TabIndex = 0
            .txtPatternID.TabIndex = 1
            .cmbSyubetu.TabIndex = 2
            .txtExcelName.TabIndex = 3
            .txtExcelTitleName.TabIndex = 4
            .txtTyusyutu.TabIndex = 5
            .txtSql.TabIndex = 6
            .sprParam.TabIndex = 7

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Me.ClearControl()

    End Sub

    ''' <summary>
    ''' コンボボックス作成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim defMST As String = "LM_MST"
        Dim defTRN As String = "LM_TRN"
        Dim defBrs As String = String.Empty
        Dim defBrsNM As String = String.Empty
        Dim loginBrIndex As Integer = -1
        Dim idx As Integer = 0

        '区分マスタ検索処理
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_D003, "'"), "KBN_NM3,KBN_NM4,KBN_NM1")

        Dim max As Integer = getDr.Count - 1

        For i As Integer = 0 To max

            If defMST.Equals(getDr(i).Item("KBN_NM3").ToString) = True AndAlso defTRN.Equals(getDr(i).Item("KBN_NM4").ToString) = True Then
                If String.IsNullOrEmpty(defBrs) = True Then
                    defBrs = getDr(i).Item("KBN_NM1").ToString()
                    If "00".Equals(getDr(i).Item("KBN_NM1").ToString()) = False Then
                        defBrsNM = getDr(i).Item("KBN_NM2").ToString()
                    End If
                Else
                    defBrs = String.Concat(defBrs, ",", getDr(i).Item("KBN_NM1").ToString())
                    If "00".Equals(getDr(i).Item("KBN_NM1").ToString()) = True Then
                        '本社の場合、名前を設定しない
                    ElseIf String.IsNullOrEmpty(defBrsNM) = True Then
                        defBrsNM = getDr(i).Item("KBN_NM2").ToString()
                    Else
                        defBrsNM = String.Concat(defBrsNM, ",", getDr(i).Item("KBN_NM2").ToString())
                    End If
                End If
                Continue For
            End If

            cd = getDr(i).Item("KBN_NM1").ToString()
            item = getDr(i).Item("KBN_NM2").ToString()

            Me._Frm.cmbConnection.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
            If LM.Base.LMUserInfoManager.GetNrsBrCd().Equals(getDr(i).Item("KBN_NM1").ToString()) = True Then
                loginBrIndex = idx
            End If
            idx = idx + 1

        Next

        Me._Frm.cmbConnection.Items.Add(New ListItem(New SubItem() {New SubItem(defBrsNM), New SubItem(defBrs)}))

        '初期値設定
        If loginBrIndex = -1 Then
            loginBrIndex = Me._Frm.cmbConnection.Items.Count - 1
        End If

        Me._Frm.cmbConnection.SelectedIndex = loginBrIndex

    End Sub

    ''' <summary>
    ''' 画面コントロールのロック設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetLockControl()

        With _Frm

            '編集部の項目をロック
            .sprPattern.Enabled = True
            .txtPatternID.ReadOnly = True
            .cmbSyubetu.ReadOnly = True
            .txtExcelName.ReadOnly = True
            .txtExcelTitleName.ReadOnly = True
            .txtTyusyutu.ReadOnly = True
            .txtSql.ReadOnly = True
            .sprParam.Enabled = True

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        'With _Frm

        '    Select Case mode
        '        Case DispMode.VIEW  '参照モード
        '            .sprPattern.Enabled = True
        '            .txtPatternID.ReadOnly = True
        '            .cmbSyubetu.Enabled = False
        '            .txtExcelName.ReadOnly = True
        '            .txtTyusyutu.ReadOnly = True
        '            .txtSql.ReadOnly = True
        '            .sprParam.Enabled = True
        '        Case DispMode.EDIT  '編集モード
        '            .sprPattern.Enabled = True
        '            If RecordStatus.NOMAL_REC.Equals(status) = True Then
        '                .txtPatternID.ReadOnly = True
        '            Else
        '                .txtPatternID.ReadOnly = False
        '            End If
        '            .cmbSyubetu.Enabled = True
        '            .txtExcelName.ReadOnly = False
        '            .txtTyusyutu.ReadOnly = False
        '            .txtSql.ReadOnly = False
        '            .sprParam.Enabled = False
        '    End Select

        'End With

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Me.ClearControl()
                    Me.LockControl(True)
                    .sprPattern.Enabled = True
                    .sprParam.Enabled = True

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照/新規
                        Case RecordStatus.NOMAL_REC, RecordStatus.NEW_REC
                            Me.LockControl(False)
                            .sprParam.Enabled = False


                    End Select

                Case DispMode.INIT
                    Me.ClearControl()
                    Me.LockControl(True)
                    .sprPattern.Enabled = True
                    .sprParam.Enabled = True

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMQ010C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMQ010C.EventShubetsu.MAIN, LMQ010C.EventShubetsu.KENSAKU
                    .sprPattern.Focus()
                Case LMQ010C.EventShubetsu.SINKI, LMQ010C.EventShubetsu.HENSYU
                    .txtPatternID.Focus()
            End Select
        End With


    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With _Frm

            .txtPatternID.TextValue = String.Empty
            .cmbSyubetu.SelectedValue = String.Empty
            .txtExcelName.TextValue = String.Empty
            .txtExcelTitleName.TextValue = String.Empty
            .txtTyusyutu.TextValue = String.Empty
            .txtSql.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' シチュエーションラベルの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSituation(Optional ByVal dispMode As String = DispMode.VIEW, _
                            Optional ByVal recordStatus As String = RecordStatus.NOMAL_REC)

        '編集部の項目をクリア
        With _Frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recordStatus
        End With
    End Sub

#End Region

#Region "検索結果表示"

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprPattern

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(0, " ", 20, True)
        Public Shared SYS_DEL_FLG_NM As SpreadColProperty = New SpreadColProperty(1, "状態", 60, True)          '状態
        Public Shared PATTERN_ID As SpreadColProperty = New SpreadColProperty(2, "パターンID", 110, True)       'パターンID
        Public Shared EX_TYPE_NM As SpreadColProperty = New SpreadColProperty(3, "印刷種別", 235, True)         '抽出種別
        Public Shared FILE_NM As SpreadColProperty = New SpreadColProperty(4, "Excel名称", 80, True)            'Excel名称
        Public Shared FILE_TITLE_NM As SpreadColProperty = New SpreadColProperty(5, "Excelﾌｧｲﾙ名", 100, True)   'Excelﾌｧｲﾙ名
        Public Shared EX_CONTENTS As SpreadColProperty = New SpreadColProperty(6, "抽出内容", 380, True)        '抽出内容
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(7, "作成日", 100, True)         '作成日
        Public Shared LAST_ACTION_DATE As SpreadColProperty = New SpreadColProperty(8, "最終実行日", 100, True) '最終実行日
        '隠し項目
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(9, "削除フラグ", 1, False)       '状態

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(Optional ByVal eventShubetu As LMQ010C.EventShubetsu = LMQ010C.EventShubetsu.SINKI)

        With _Frm.sprPattern

            If LMQ010C.EventShubetsu.HENSYU.Equals(eventShubetu) = False Then
                '編集以外の時はパターン一覧をクリアする

                'スプレッドの行をクリア
                .CrearSpread()

                '列数設定
                .Sheets(0).ColumnCount = 10

                '2015.10.15 英語化対応START
                'スプレッドの列設定（列名、列幅、列の表示・非表示）
                '.SetColProperty(New sprPattern)
                .SetColProperty(New LMQ010G.sprPattern(), False)
                '2015.10.15 英語化対応END

                '列設定
                .SetCellStyle(0, sprPattern.SYS_DEL_FLG_NM.ColNo, LMSpreadUtility.GetComboCellKbn(Me._Frm.sprPattern, "S051", False))
                .SetCellStyle(0, sprPattern.PATTERN_ID.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprPattern, InputControl.HAN_NUM_ALPHA, 10, False))   'パターンID
                .SetCellStyle(0, sprPattern.EX_TYPE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(Me._Frm.sprPattern, "E001", False))                       '抽出種別
                .SetCellStyle(0, sprPattern.FILE_NM.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprPattern, InputControl.ALL_MIX, 20, False))            'Excel作成
                .SetCellStyle(0, sprPattern.FILE_TITLE_NM.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprPattern, InputControl.ALL_MIX, 60, False))      'Excelﾌｧｲﾙ名
                .SetCellStyle(0, sprPattern.EX_CONTENTS.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprPattern, InputControl.ALL_MIX, 100, False))       '抽出内容
                .SetCellStyle(0, sprPattern.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(Me._Frm.sprPattern, True))
                .SetCellStyle(0, sprPattern.LAST_ACTION_DATE.ColNo, LMSpreadUtility.GetDateTimeCell(Me._Frm.sprPattern, True))
                .SetCellStyle(0, sprPattern.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetTextCell(Me._Frm.sprPattern, InputControl.ALL_MIX, 10, False))        '削除フラグ

            End If
            'スプレッドの行の初期値設定
            Me._Frm.sprParam.CrearSpread()

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMQ010F)

        With frm.sprPattern.ActiveSheet

            .SetValue(0, sprPattern.SYS_DEL_FLG_NM.ColNo, LMConst.FLG.OFF)     '状態
            .SetValue(0, sprPattern.PATTERN_ID.ColNo, String.Empty)            'パターンID
            .SetValue(0, sprPattern.EX_TYPE_NM.ColNo, String.Empty)            '抽出区分
            .SetValue(0, sprPattern.FILE_NM.ColNo, String.Empty)               'Excel名称
            .SetValue(0, sprPattern.FILE_TITLE_NM.ColNo, String.Empty)         'Excelﾌｧｲﾙ名
            .SetValue(0, sprPattern.EX_CONTENTS.ColNo, String.Empty)           '抽出内容
            .SetValue(0, sprPattern.SYS_ENT_DATE.ColNo, String.Empty)          '作成日
            .SetValue(0, sprPattern.LAST_ACTION_DATE.ColNo, String.Empty)      '最終実行日

            .SetValue(0, sprPattern.SYS_DEL_FLG.ColNo, String.Empty)           '削除フラグ            

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = _Frm.sprPattern

        With spr

            .SuspendLayout()

            '----データ挿入----'

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count()
            If 0 = lngcnt Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count(), lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dr As DataRow = dt.NewRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '*****表示列*****
                .SetCellStyle(i, sprPattern.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprPattern.SYS_DEL_FLG_NM.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.PATTERN_ID.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.EX_TYPE_NM.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.FILE_NM.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.FILE_TITLE_NM.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.EX_CONTENTS.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprPattern.LAST_ACTION_DATE.ColNo, sLabel)

                '*****隠し列*****
                .SetCellStyle(i, sprPattern.SYS_DEL_FLG.ColNo, sLabel)

                'セル値設定
                '*****表示列*****
                .SetCellValue(i, sprPattern.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprPattern.SYS_DEL_FLG_NM.ColNo, dr.Item("SYS_DEL_FLG_NM").ToString())
                .SetCellValue(i, sprPattern.PATTERN_ID.ColNo, dr.Item("PATTERN_ID").ToString())
                .SetCellValue(i, sprPattern.EX_TYPE_NM.ColNo, dr.Item("EX_TYPE_NM").ToString())
                .SetCellValue(i, sprPattern.FILE_NM.ColNo, dr.Item("FILE_NM").ToString())
                .SetCellValue(i, sprPattern.FILE_TITLE_NM.ColNo, dr.Item("FILE_TITLE_NM").ToString())
                .SetCellValue(i, sprPattern.EX_CONTENTS.ColNo, dr.Item("EX_CONTENTS").ToString())
                .SetCellValue(i, sprPattern.SYS_ENT_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprPattern.LAST_ACTION_DATE.ColNo, Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(dr.Item("LAST_ACTION_DATE").ToString()))

                '*****隠し列*****
                .SetCellValue(i, sprPattern.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "詳細表示"

    ''' <summary>
    ''' 詳細表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDetails(ByVal index As Integer, ByVal dsPattern As DataTable)

        Dim patternID As String = String.Empty
        '選択行の値を詳細に設定する


        With _Frm.sprPattern.ActiveSheet
            patternID = _LMQconG.GetCellValue(.Cells(index, sprPattern.PATTERN_ID.ColNo))

            _Frm.txtPatternID.TextValue = _LMQconG.GetCellValue(.Cells(index, sprPattern.PATTERN_ID.ColNo))

            Dim setRows() As DataRow = dsPattern.Select(String.Concat("PATTERN_ID = '", patternID, "'"))

            _Frm.cmbSyubetu.SelectedValue = setRows(0)(LMQ010C.DsPatternColumnIndex.EX_TYPE_KB)
            _Frm.txtExcelName.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.FILE_NM).ToString
            _Frm.txtExcelTitleName.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.FILE_TITLE_NM).ToString
            _Frm.txtTyusyutu.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.EX_CONTENTS).ToString
            _Frm.txtSql.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.EX_SQL).ToString
            _Frm.lblCrtUser.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_ENT_USER_NM).ToString
            _Frm.lblCrtDate.TextValue = Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_ENT_DATE).ToString)
            _Frm.lblUpdUser.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_UPD_USER_NM).ToString
            _Frm.lblUpdDate.TextValue = Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_UPD_DATE).ToString)
            _Frm.lblUpdTime.TextValue = Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_UPD_TIME).ToString)
            _Frm.lblSysDelFlg.TextValue = setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_DEL_FLG).ToString

            'スプレッドの行をクリア
            _Frm.sprParam.CrearSpread()
            '列数設定
            _Frm.sprParam.Sheets(0).ColumnCount = 0

            If "0".Equals(setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_DEL_FLG).ToString) = True Then
                '削除データの時は対象外

                'パラメータ一覧の設定
                Dim sqlArray As String() = setRows(0)(LMQ010C.DsPatternColumnIndex.EX_SQL).ToString.Replace("@", "@@").Split("@"c)
                Dim sqlArrayCnt As Integer = sqlArray.Length - 1
                Dim paramFlg As String = "0"
                Dim colCnt As Integer = 0
                Dim colNM() As String = Nothing
                Dim chkCnt As Integer = 0

                For i As Integer = 0 To sqlArrayCnt
                    If String.IsNullOrEmpty(sqlArray(i)) = True Then
                        If "0".Equals(paramFlg) = True Then
                            paramFlg = "1"
                        ElseIf "2".Equals(paramFlg) = True Then
                            paramFlg = "0"
                        End If
                    Else
                        If "1".Equals(paramFlg) = True Then
                            '既に同じ値が設定されていたらスルー
                            For chkCnt = 0 To colCnt - 1
                                If (colNM(chkCnt).ToString).Equals(sqlArray(i).ToString) = True Then
                                    Exit For
                                End If
                            Next

                            If chkCnt.Equals(colCnt) = True Then
                                'SPREADにパラメータ追加
                                ReDim Preserve colNM(colCnt)
                                colNM(colCnt) = sqlArray(i).ToString 'セル名を設定
                                colCnt = colCnt + 1
                            End If

                            paramFlg = "2"
                        End If
                    End If
                Next

                If 0 < colCnt Then
                    '列数設定
                    _Frm.sprParam.Sheets(0).ColumnCount = colCnt
                    _Frm.sprParam.Sheets(0).RowCount = 1

                    ''スプレッドの列設定（列名、列幅、列の表示・非表示）
                    '_Frm.sprParam.SetColProperty(colNM)
                    colCnt = colCnt - 1
                    For i As Integer = 0 To colCnt
                        '列設定
                        _Frm.sprParam.Sheets(0).SetColumnWidth(i, 150)
                        _Frm.sprParam.Sheets(0).SetColumnLabel(0, i, colNM(i))
                        'START YANAI 要望番号332
                        '_Frm.sprParam.SetCellStyle(0, i, LMSpreadUtility.GetTextCell(_Frm.sprParam, InputControl.ALL_MIX_IME, 1000, False))    '列の設定
                        _Frm.sprParam.SetCellStyle(0, i, LMSpreadUtility.GetTextCell(_Frm.sprParam, InputControl.ALL_MIX_IME_OFF, 1000, False))    '列の設定
                        'END YANAI 要望番号332
                    Next
                End If
            End If

        End With

    End Sub

#End Region

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        Me.SetLockControl(Me._Frm.txtPatternID, lock)
        Me.SetLockControl(Me._Frm.cmbSyubetu, lock)
        Me.SetLockControl(Me._Frm.txtExcelName, lock)
        Me.SetLockControl(Me._Frm.txtExcelTitleName, lock)
        Me.SetLockControl(Me._Frm.txtTyusyutu, lock)
        Me.SetLockControl(Me._Frm.txtSql, lock)

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

End Class
