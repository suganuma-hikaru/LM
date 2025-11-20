' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400G : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI400Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI400G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI400F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 初期処理か判断するフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShokiFlg As Boolean = True

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI400V

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI400F, ByVal g As LMIControlG, ByVal v As LMI400V)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

        Me._V = v

    End Sub

#End Region

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
            .F1ButtonName = "新　規"
            .F2ButtonName = "編　集"
            .F3ButtonName = String.Empty
            .F4ButtonName = "削除・復活"
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検　索"
            .F10ButtonName = String.Empty
            .F11ButtonName = "保　存"
            .F12ButtonName = "閉じる"


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
            .F10ButtonEnabled = lock

            '常に使用可能キー
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock

            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F4ButtonEnabled = view
            .F11ButtonEnabled = edit

        End With

    End Sub

#End Region

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal dispMode As String, ByVal recStatus As String)

        With Me._Frm
            .lblSituation.DispMode = dispMode
            .lblSituation.RecordStatus = recStatus
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

            '******************* ヘッダー部 *****************************
            .sprSearch.TabIndex = LMI400C.CtlTabIndex.SPR_SEARCH
            '******************* 編集部 *****************************
            .cmbNrsBr.TabIndex = LMI400C.CtlTabIndex.CMB_NRS_BR
            .txtOyaCd.TabIndex = LMI400C.CtlTabIndex.TXT_OYA_CD
            .txtOyaNm.TabIndex = LMI400C.CtlTabIndex.TXT_OYA_NM
            .btnAdd.TabIndex = LMI400C.CtlTabIndex.BTN_ADD
            .btnDel.TabIndex = LMI400C.CtlTabIndex.BTN_DEL
            .sprDetail.TabIndex = LMI400C.CtlTabIndex.SPR_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '画面項目クリア処理
        Call Me.ClearControl()

        '初期設定
        Call Me.SetDateControl()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.INIT, DispMode.VIEW
                    .sprSearch.Focus()

                Case DispMode.EDIT

                    Select Case Me._Frm.lblSituation.RecordStatus

                        Case RecordStatus.NEW_REC
                            .txtOyaCd.Focus()

                        Case RecordStatus.NOMAL_REC
                            .sprDetail.Focus()

                    End Select

            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case Me._Frm.lblSituation.DispMode

                Case DispMode.EDIT

                    Select Case Me._Frm.lblSituation.RecordStatus

                        Case RecordStatus.NEW_REC

                            '新規モード
                            .cmbNrsBr.ReadOnly = True
                            .txtOyaCd.ReadOnly = False
                            .txtOyaNm.ReadOnly = False
                            .btnAdd.Enabled = True
                            .btnDel.Enabled = True
                            .lblCrtUser.ReadOnly = True
                            .lblCrtDate.ReadOnly = True
                            .lblUpdUser.ReadOnly = True
                            .lblUpdDate.ReadOnly = True
                            .lblUpdateTime.ReadOnly = True

                        Case RecordStatus.NOMAL_REC

                            '編集モード
                            .cmbNrsBr.ReadOnly = True
                            .txtOyaCd.ReadOnly = True
                            .txtOyaNm.ReadOnly = False
                            .btnAdd.Enabled = True
                            .btnDel.Enabled = True
                            .lblCrtUser.ReadOnly = True
                            .lblCrtDate.ReadOnly = True
                            .lblUpdUser.ReadOnly = True
                            .lblUpdDate.ReadOnly = True
                            .lblUpdateTime.ReadOnly = True

                    End Select



                Case DispMode.INIT, DispMode.VIEW

                    '参照モード時
                    .sprDetail.CrearSpread()
                    .cmbNrsBr.ReadOnly = True
                    .txtOyaCd.ReadOnly = True
                    .txtOyaNm.ReadOnly = True
                    .btnAdd.Enabled = False
                    .btnDel.Enabled = False
                    .lblCrtUser.ReadOnly = True
                    .lblCrtDate.ReadOnly = True
                    .lblUpdUser.ReadOnly = True
                    .lblUpdDate.ReadOnly = True
                    .lblUpdateTime.ReadOnly = True

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 画面項目クリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbNrsBr.SelectedValue = String.Empty
            .txtOyaCd.TextValue = String.Empty
            .txtOyaNm.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
            .sprDetail.CrearSpread()

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 初期設定(新規)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            .cmbNrsBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

        End With

    End Sub

    ''' <summary>
    ''' 明細Spreadに一行追加する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub AddRow(ByVal ds As DataSet)

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_OUT)

        With spr

            .SuspendLayout()

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 1)

            '列設定用変数
            Dim unlock As Boolean = False
            Dim lock As Boolean = True

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, unlock)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999, unlock, 0, , ",")
            Dim koCd As StyleInfo = LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 15, unlock)
            Dim addRow As Integer = .ActiveSheet.Rows.Count - 1

            'セルスタイル設定
            '**** 表示列 ****
            .SetCellStyle(addRow, sprDetailDef.DEF.ColNo, def)
            .SetCellStyle(addRow, sprDetailDef.KO_CD.ColNo, koCd)
            .SetCellStyle(addRow, sprDetailDef.KO_NM.ColNo, lbl)
            .SetCellStyle(addRow, sprDetailDef.KOSU.ColNo, num)

            'セル値設定
            '**** 表示列 ****
            .SetCellValue(addRow, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
            .SetCellValue(addRow, sprDetailDef.KO_CD.ColNo, dt.Rows(0).Item("GOODS_CD_CUST").ToString())
            .SetCellValue(addRow, sprDetailDef.KO_NM.ColNo, dt.Rows(0).Item("GOODS_NM_1").ToString())
            .SetCellValue(addRow, sprDetailDef.KOSU.ColNo, "0")

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 明細Spreadの行削除を行う
    ''' </summary>
    ''' <param name="list">チェック行格納配列</param>
    ''' <remarks></remarks>
    Friend Sub DelateDtl(ByVal list As ArrayList)

        Dim listMax As Integer = list.Count - 1

        For i As Integer = listMax To 0 Step -1
            Me._Frm.sprDetail.ActiveSheet.Rows.Remove(Convert.ToInt32(list(i)), 1)

        Next

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprSearchDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.DEF, " ", 20, True)
        Public Shared STATUS As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.STATUS, "状態", 60, True)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.NRS_BR_NM, "営業所", 230, True)
        Public Shared OYA_CD As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.OYA_CD, "親コード", 150, True)
        Public Shared OYA_NM As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.OYA_NM, "親名称", 300, True)

        '**** 隠し列 ****
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.NRS_BR_CD, "営業所コード", 50, False)
        Public Shared CREATE_USER As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.CREATE_USER, "作成者", 50, False)
        Public Shared CREATE_DATE As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.CREATE_DATE, "作成日", 50, False)
        Public Shared UPDATE_USER As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.UPDATE_USER, "更新者", 50, False)
        Public Shared UPDATE_DATE As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.UPDATE_DATE, "更新日", 50, False)
        Public Shared UPDATE_TIME As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.UPDATE_TIME, "更新時間", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(明細)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        '**** 表示列 ****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI400C.SprSearchColumnIndex.DEF, " ", 20, True)
        Public Shared KO_CD As SpreadColProperty = New SpreadColProperty(LMI400C.SprDetailColumnIndex.KO_CD, "子コード", 150, True)
        Public Shared KO_NM As SpreadColProperty = New SpreadColProperty(LMI400C.SprDetailColumnIndex.KO_NM, "子名称", 300, True)
        Public Shared KOSU As SpreadColProperty = New SpreadColProperty(LMI400C.SprDetailColumnIndex.KOSU, "個数", 100, True)

    End Class

    ''' <summary>
    ''' SPREADの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitDetailSpread()

        '検索スプレッド初期化
        Call SetSprSearch()

        '明細スプレッド初期化
        Call SetSprDetail()

    End Sub

    ''' <summary>
    ''' SPREADのコントロール設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSprSearch()

        'Spreadの初期値設定
        Dim sprSearch As LMSpread = Me._Frm.sprSearch

        With sprSearch

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI400C.SprSearchColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprSearchDef)

            '列固定位置を設定します。
            .ActiveSheet.FrozenColumnCount = sprSearchDef.OYA_NM.ColNo + 1

            '検索行の設定を行う
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprSearch)
            Dim oyaCd As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_MIX, 30, False)
            Dim custCd As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.HAN_NUMBER, 5, False)
            Dim text As StyleInfo = LMSpreadUtility.GetTextCell(sprSearch, InputControl.ALL_MIX, 60, False)

            '**** 表示列 ****
            .SetCellStyle(0, sprSearchDef.DEF.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.STATUS.ColNo, LMSpreadUtility.GetComboCellKbn(sprSearch, LMKbnConst.KBN_S051, False))
            .SetCellStyle(0, sprSearchDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(sprSearch, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            .SetCellStyle(0, sprSearchDef.OYA_CD.ColNo, oyaCd)
            .SetCellStyle(0, sprSearchDef.OYA_NM.ColNo, text)

            '**** 隠し列 ****
            .SetCellStyle(0, sprSearchDef.NRS_BR_CD.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.CREATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.CREATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UPDATE_USER.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UPDATE_DATE.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.UPDATE_TIME.ColNo, lbl)
            .SetCellStyle(0, sprSearchDef.SYS_DEL_FLG.ColNo, lbl)

            '初期値設定
            .SetCellValue(0, sprSearchDef.STATUS.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, sprSearchDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())

        End With

    End Sub

    ''' <summary>
    ''' SPREADのコントロール設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSprDetail()

        'Spreadの初期値設定
        Dim sprDetail As LMSpread = Me._Frm.sprDetail

        With sprDetail

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = LMI400C.SprDetailColumnIndex.CLM_NM

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .SetColProperty(New sprDetailDef)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSprSearch(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprSearch

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim lblL As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
            Dim numNb As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")

            Dim dr As DataRow

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprSearchDef.DEF.ColNo, def)
                .SetCellStyle(i, sprSearchDef.STATUS.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.NRS_BR_NM.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.OYA_CD.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.OYA_NM.ColNo, lblL)

                '**** 隠し列 ****
                .SetCellStyle(i, sprSearchDef.NRS_BR_CD.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.CREATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.CREATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.UPDATE_USER.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.UPDATE_DATE.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.UPDATE_TIME.ColNo, lblL)
                .SetCellStyle(i, sprSearchDef.SYS_DEL_FLG.ColNo, lblL)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprSearchDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprSearchDef.STATUS.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, sprSearchDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, sprSearchDef.OYA_CD.ColNo, dr.Item("OYA_CD").ToString())
                .SetCellValue(i, sprSearchDef.OYA_NM.ColNo, dr.Item("OYA_NM").ToString())

                '**** 隠し列 ****
                .SetCellValue(i, sprSearchDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, sprSearchDef.CREATE_USER.ColNo, dr.Item("SYS_ENT_USER").ToString())
                .SetCellValue(i, sprSearchDef.CREATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_ENT_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.UPDATE_USER.ColNo, dr.Item("SYS_UPD_USER").ToString())
                .SetCellValue(i, sprSearchDef.UPDATE_DATE.ColNo, DateFormatUtility.EditSlash(dr.Item("SYS_UPD_DATE").ToString()))
                .SetCellValue(i, sprSearchDef.UPDATE_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, sprSearchDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            Dim spr As FarPoint.Win.Spread.SheetView = .sprSearch.ActiveSheet

            '明細部
            .cmbNrsBr.SelectedValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.NRS_BR_CD.ColNo))
            .txtOyaCd.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.OYA_CD.ColNo))
            .txtOyaNm.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.OYA_NM.ColNo))

            '共通項目
            .lblCrtUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.CREATE_USER.ColNo))
            .lblCrtDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.CREATE_DATE.ColNo))
            .lblUpdUser.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.UPDATE_USER.ColNo))
            .lblUpdDate.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.UPDATE_DATE.ColNo))
            .lblUpdTime.TextValue = Me._ControlG.GetCellValue(spr.Cells(row, LMI400G.sprSearchDef.UPDATE_TIME.ColNo))

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="dt">スプレッドの表示するデータテーブル</param>
    ''' <remarks></remarks>
    Friend Sub SetSprDetail(ByVal dt As DataTable)

        Dim sprDetail As LMSpread = Me._Frm.sprDetail

        With sprDetail

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()

            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            Dim edit As Boolean = False

            If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
                edit = True
            End If


            '列設定用変数
            Dim def As StyleInfo = LMSpreadUtility.GetCheckBoxCell(sprDetail, edit)
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(sprDetail)
            Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(sprDetail, 0, 99999, edit, 0, , ",")
            Dim koCd As StyleInfo = LMSpreadUtility.GetTextCell(sprDetail, InputControl.HAN_NUM_ALPHA, 15, edit)
            Dim dr As DataRow

            '値設定
            For i As Integer = 0 To lngcnt - 1

                dr = dt.Rows(i)

                'セルスタイル設定
                '**** 表示列 ****
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, def)
                .SetCellStyle(i, sprDetailDef.KO_CD.ColNo, koCd)
                .SetCellStyle(i, sprDetailDef.KO_NM.ColNo, lbl)
                .SetCellStyle(i, sprDetailDef.KOSU.ColNo, num)

                'セル値設定
                '**** 表示列 ****
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.KO_CD.ColNo, dr.Item("KO_CD").ToString())
                .SetCellValue(i, sprDetailDef.KO_NM.ColNo, dr.Item("GOODS_NM_1").ToString())
                .SetCellValue(i, sprDetailDef.KOSU.ColNo, dr.Item("SET_KOSU").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region

#End Region

#End Region

End Class
