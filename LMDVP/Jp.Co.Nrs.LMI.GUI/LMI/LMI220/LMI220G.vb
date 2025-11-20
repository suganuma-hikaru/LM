' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMI220G : 届先マスタメンテナンス
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
Imports GrapeCity.Win.Editors

''' <summary>
''' LMI220Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI220G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI220F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI220V

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI220F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        'Validate共通クラスの設定
        _ControlV = New LMIControlV(handlerClass, DirectCast(frm, Form), _ControlG)

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
        Dim editF1 As Boolean = False
        Dim editF2 As Boolean = False
        Dim editF4 As Boolean = False
        Dim editF7 As Boolean = False
        Dim editF11 As Boolean = False

        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LMIControlC.FUNCTION_SINKI
            .F2ButtonName = LMIControlC.FUNCTION_EDIT
            .F3ButtonName = String.Empty
            .F4ButtonName = LMIControlC.FUNCTION_DEL_REV
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = LMIControlC.FUNCTION_IMPORT_DATA
            .F8ButtonName = String.Empty
            .F9ButtonName = LMIControlC.FUNCTION_KENSAKU
            .F10ButtonName = String.Empty
            .F11ButtonName = LMIControlC.FUNCTION_HOZON
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.INIT '初期モード時
                    editF1 = True
                    editF2 = False
                    editF4 = False
                    editF7 = True
                    editF11 = False
                Case DispMode.VIEW '参照モード時
                    editF1 = True
                    Select Case Me._Frm.lblSituation.RecordStatus
                        Case RecordStatus.DELETE_REC
                            editF2 = False
                        Case Else
                            editF2 = True
                    End Select
                    editF4 = True
                    editF7 = True
                    editF11 = False
                Case DispMode.EDIT '編集モード時
                    editF1 = False
                    editF2 = False
                    editF4 = False
                    editF7 = False
                    editF11 = True
            End Select

            'ファンクションキーの制御
            .F1ButtonEnabled = editF1
            .F2ButtonEnabled = editF2
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = editF4
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = editF7
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = always
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = editF11
            .F12ButtonEnabled = always

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

    ''' <summary>
    ''' 検索条件部(Spread)に初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitSpreadValue()

        With Me._Frm.sprDetail

            '状態
            .SetCellValue(0, LMI220C.SprColumnIndex.SYS_DEL_NM,lmconst.FLG.OFF )
            'シリンダ番号
            .SetCellValue(0, LMI220C.SprColumnIndex.SERIAL_NO, String.Empty)
            'サイズ
            .SetCellValue(0, LMI220C.SprColumnIndex.SIZE_NM, String.Empty)
            '製造日
            .SetCellValue(0, LMI220C.SprColumnIndex.PROD_DATE, String.Empty)
            '再検査日
            .SetCellValue(0, LMI220C.SprColumnIndex.LAST_TEST_DATE, String.Empty)
            '次回定検日
            .SetCellValue(0, LMI220C.SprColumnIndex.NEXT_TEST_DATE, String.Empty)

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

            .sprDetail.TabIndex = LMI220C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMI220C.CtlTabIndex.NRS_BR_CD
            .txtSerialNo.TabIndex = LMI220C.CtlTabIndex.SERIAL_NO
            .imdProdDate.TabIndex = LMI220C.CtlTabIndex.PROD_DATE
            .imdLastTestDate.TabIndex = LMI220C.CtlTabIndex.LAST_TEST_DATE
            .txtNextTestDate.TabIndex = LMI220C.CtlTabIndex.NEXT_TEST_DATE

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFormControlsStatus()

        'ファンクションキー設定
        Call Me.SetFunctionKey()

        '画面コントロールの入力制御
        Call Me.SetControlsStatus()

    End Sub


    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.VIEW

                    '編集部のロック
                    Call Me.LockControlEdit(lock)

                Case DispMode.EDIT

                    '編集部のロック解除
                    Call Me.LockControlEdit(unLock)

                    Select Case Me._Frm.lblSituation.RecordStatus
                        Case RecordStatus.NOMAL_REC

                            '編集モードの場合は「シリンダ番号」をロックする
                            Call Me._ControlG.SetLockInputMan(.txtSerialNo, lock, False)

                    End Select

                Case Else

                    Call Me.LockControlEdit(lock)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 編集時ロック制御
    ''' </summary>
    ''' <remarks>ロック時：true ロック解除時：false</remarks>
    Private Sub LockControlEdit(ByVal lock As Boolean)

        Dim clearFlg As Boolean = False

        With Me._Frm

            '編集時はロックする
            Call Me.LockEditControl(.cmbSize, lock)
            Call Me._ControlG.SetLockInputMan(.txtSerialNo, lock, clearFlg)
            Call Me._ControlG.SetLockInputMan(.imdProdDate, lock, clearFlg)
            Call Me._ControlG.SetLockInputMan(.imdLastTestDate, lock, clearFlg)

            '常にロック項目ロック制御
            Call Me.LockEditControl(.cmbNrsBrCd, True)

        End With

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockEditControl(ByVal ctl As Nrs.Win.GUI.Win.Interface.IEditableControl, ByVal lockFlg As Boolean)
        ctl.ReadOnlyStatus = lockFlg
    End Sub

    ''' <summary>
    ''' 項目のクリア処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            'コントロールクリア
            Call Me.ClearEditControl()

            'スプレッドクリア
            .sprDetail.CrearSpread()

            'スプレッド初期化
            Call Me.InitSpread()

        End With

    End Sub

    ''' <summary>
    ''' 編集部クリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearEditControl()

        With Me._Frm

            'コントロールクリア
            .cmbNrsBrCd.SelectedIndex = 0
            .txtSerialNo.TextValue = String.Empty
            .cmbSize.SelectedIndex = -1
            .imdProdDate.TextValue = String.Empty
            .imdLastTestDate.TextValue = String.Empty
            .txtNextTestDate.TextValue = String.Empty
            
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMI220C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMI220C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMI220C.EventShubetsu.SHINKI
                    .txtSerialNo.Focus()
                Case LMI220C.EventShubetsu.HENSHU
                    .cmbSize.Focus()
                Case Else

            End Select

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.NRS_BR_CD))
            .txtSerialNo.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SERIAL_NO))
            .cmbSize.SelectedValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SIZE))
            .imdProdDate.TextValue = DateFormatUtility.DeleteSlash(Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.PROD_DATE)))
            .imdLastTestDate.TextValue = DateFormatUtility.DeleteSlash(Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.LAST_TEST_DATE)))
            .txtNextTestDate.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.NEXT_TEST_DATE))

            .lblCrtDate.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SYS_ENT_DATE))
            .lblCrtUser.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SYS_ENT_USER_NM))
            .lblUpdDate.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SYS_UPD_DATE))
            .lblUpdTime.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SYS_UPD_TIME))
            .lblUpdUser.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SYS_UPD_USER_NM))
            .lblSysDelFlg.TextValue = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(row, LMI220C.SprColumnIndex.SYS_DEL_FLG))

            'スラッシュ編集
            .lblCrtDate.TextValue = Me.getSlashStr(.lblCrtDate.TextValue)
            .lblUpdDate.TextValue = Me.getSlashStr(.lblUpdDate.TextValue)

        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared SERIAL_NO As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SERIAL_NO, "シリンダ番号", 200, True)
        Public Shared SIZE_NM As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SIZE_NM, "サイズ", 100, True)
        Public Shared PROD_DATE As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.PROD_DATE, "製造日", 100, True)
        Public Shared LAST_TEST_DATE As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.LAST_TEST_DATE, "再検査日", 100, True)
        Public Shared NEXT_TEST_DATE As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.NEXT_TEST_DATE, "次回定検日", 100, True)

        '隠し項目
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.NRS_BR_CD, "営業所コード", 0, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_ENT_DATE, "作成日", 0, False)
        Public Shared SYS_ENT_TIME As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_ENT_TIME, "作成時刻", 0, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 0, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_UPD_DATE, "更新日", 0, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 0, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 0, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 0, False)
        Public Shared SIZE As SpreadColProperty = New SpreadColProperty(LMI220C.SprColumnIndex.SIZE, "サイズ", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            'スプレッドの列数設定
            .sprDetail.Sheets(0).ColumnCount = LMI220C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMI220G.sprDetailDef())

            Dim sCheck As StyleInfo = LMSpreadUtility.GetCheckBoxCell(.sprDetail, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定（検索条件部）
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SERIAL_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 40, False))
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SIZE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S085", False))
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.PROD_DATE.ColNo, LMSpreadUtility.GetDateTimeFormatCell(.sprDetail, False, "yyyy/MM/dd"))
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.LAST_TEST_DATE.ColNo, LMSpreadUtility.GetDateTimeFormatCell(.sprDetail, False, "yyyy/MM/dd"))
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.NEXT_TEST_DATE.ColNo, LMSpreadUtility.GetDateTimeFormatCell(.sprDetail, False, "yyyy/MM/dd"))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_ENT_TIME.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
            .sprDetail.SetCellStyle(0, LMI220G.sprDetailDef.SIZE.ColNo, sLabel)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMI220F)

        With frm.sprDetail

            '.ActiveSheet.Cells(0, LMI220G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SERIAL_NO.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SIZE_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.PROD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.LAST_TEST_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.NEXT_TEST_DATE.ColNo).Value = String.Empty

            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.NRS_BR_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SYS_DEL_FLG.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMI220G.sprDetailDef.SIZE.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定 
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI220C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count
        Dim henkangoStr As String = String.Empty

        With Me._Frm.sprDetail

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(Me._Frm.sprDetail, False)
            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(Me._Frm.sprDetail, CellHorizontalAlignment.Left)
            Dim sLabelRight As StyleInfo = LMSpreadUtility.GetLabelCell(Me._Frm.sprDetail, CellHorizontalAlignment.Right)

            Dim dr As DataRow = Nothing
            Dim rowCnt As Integer = 0

            For i As Integer = 1 To max

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SERIAL_NO.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SIZE_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.PROD_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.LAST_TEST_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.NEXT_TEST_DATE.ColNo, sLabel)

                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_ENT_TIME.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMI220G.sprDetailDef.SIZE.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SERIAL_NO.ColNo, dr.Item("SERIAL_NO").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SIZE_NM.ColNo, dr.Item("SIZE_NM").ToString())
                .SetCellValue(rowCnt, sprDetailDef.PROD_DATE.ColNo, Me.getSlashStr(dr.Item("PROD_DATE").ToString()))
                .SetCellValue(rowCnt, sprDetailDef.LAST_TEST_DATE.ColNo, Me.getSlashStr(dr.Item("LAST_TEST_DATE").ToString()))
                .SetCellValue(rowCnt, sprDetailDef.NEXT_TEST_DATE.ColNo, Me.getSlashStr(dr.Item("NEXT_TEST_DATE").ToString()))

                .SetCellValue(rowCnt, sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_ENT_TIME.ColNo, dr.Item("SYS_ENT_TIME").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(rowCnt, sprDetailDef.SIZE.ColNo, dr.Item("SIZE").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

    Friend Function getSlashStr(ByVal str As String) As String

        If str.Length <> 8 Then
            Return str
        End If

        Dim rtnStr As String = String.Empty
        rtnStr = String.Concat(str.Substring(0, 4), "/", str.Substring(4, 2), "/", str.Substring(6, 2))

        Return rtnStr

    End Function

#End Region

End Class
