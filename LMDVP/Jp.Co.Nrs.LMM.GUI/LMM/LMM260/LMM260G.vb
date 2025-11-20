' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM260G : 個人・用途別注意書マスタメンテ
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMM260Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM260G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM260F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM260F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        Me._ControlG = g

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

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = LMMControlC.FUNCTION_F1_SHINKI
            .F2ButtonName = LMMControlC.FUNCTION_F2_HENSHU
            .F3ButtonName = LMMControlC.FUNCTION_F3_FUKUSHA
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = String.Empty
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

            'ロック制御変数
            Dim edit As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) '編集モード時使用可能
            Dim view As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.VIEW) '参照モード時使用可能
            Dim init As Boolean = Me._Frm.lblSituation.DispMode.Equals(DispMode.INIT) '初期モード時使用可能

            '常に使用不可キー
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view

            .F10ButtonEnabled = lock
            .F11ButtonEnabled = edit


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

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM260C.CtlTabIndex.DETAIL

            .cmbNrsBrCd.TabIndex = LMM260C.CtlTabIndex.NRSBRCD
            .lblUserCd.TabIndex = LMM260C.CtlTabIndex.USERID
            .lblUserNm.TabIndex = LMM260C.CtlTabIndex.USERNM
            .cmbYoto.TabIndex = LMM260C.CtlTabIndex.YOTO
            .txtChui.TabIndex = LMM260C.CtlTabIndex.CHUI

            .lblSituation.TabIndex = LMM260C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM260C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM260C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM260C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM260C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM260C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM260C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetValue()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.lblUserCd.TextValue = LMUserInfoManager.GetUserID()
        Me._Frm.lblUserNm.TextValue = LMUserInfoManager.GetUserName()
        Me._Frm.cmbYoto.SelectedValue = LMM260C.YOTO

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '画面項目を全ロックする
            Call Me._ControlG.SetLockControl(Me._Frm, lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    Me.ClearControl(Me._Frm)
                    Call Me._ControlG.SetLockControl(Me._Frm, lock)

                Case DispMode.VIEW
                    Me.ClearControl(Me._Frm)
                    Call Me._ControlG.SetLockControl(Me._Frm, lock)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.lblUserCd, lock)
                            Call Me._ControlG.LockText(.lblUserNm, lock)
                            Call Me._ControlG.LockComb(.cmbYoto, lock)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.lblUserCd, lock)
                            Call Me._ControlG.LockText(.lblUserNm, lock)

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.lblUserCd, lock)
                            Call Me._ControlG.LockText(.lblUserNm, lock)
                            Call Me._ControlG.LockComb(.cmbYoto, lock)
                            Call Me.ClearControlFukusha()
                            

                    End Select


            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .lblUserCd.TextValue = LMUserInfoManager.GetUserID()
            .lblUserNm.TextValue = LMUserInfoManager.GetUserName()
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty


        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM260C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM260C.EventShubetsu.MAIN, LMM260C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM260C.EventShubetsu.SHINKI
                    .cmbYoto.Focus()
                Case LMM260C.EventShubetsu.HUKUSHA, LMM260C.EventShubetsu.HENSHU
                    .txtChui.Focus()
            End Select
        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm
            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .lblUserCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.USER_CD.ColNo).Text
            .lblUserNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.USER_NM.ColNo).Text
            .cmbYoto.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SUB_KB.ColNo).Text
            .txtChui.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.REMARK.ColNo).Text
            .txtRemno.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.REM_NO.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM260G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared USER_CD As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.USER_CD, "ユーザー" & vbCrLf & "コード", 80, True)
        Public Shared USER_NM As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.USER_NM, "ユーザー名", 150, True)
        Public Shared SUB_KB As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SUB_KB, "用途区分コード", 60, False)
        Public Shared SUB_KB_NM As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SUB_KB_NM, "用途区分", 80, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.REMARK, "注意書", 400, True)
        Public Shared REM_NO As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.REM_NO, "注意書番号", 60, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM260C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)




    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        Dim dr As DataRow

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 16

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM260G.sprDetailDef())

            '列固定位置を設定します。
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM260G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.USER_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 5, False))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.USER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SUB_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "Y005", False))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))



            '隠し項目
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SUB_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.REM_NO.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM260G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>

    Friend Sub SetInitValue()

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        With spr


            .SetCellValue(0, LMM260G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM260G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM260G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM260G.sprDetailDef.USER_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SUB_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SUB_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.REMARK.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.REM_NO.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM260G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)



            .ResumeLayout(True)

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()
        Dim lock As Boolean = True
        With spr

            .SuspendLayout()
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = Me.StyleInfoLabel(spr)

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)


                'セルスタイル設定
                .SetCellStyle(i, LMM260G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.USER_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SUB_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SUB_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.REM_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM260G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM260G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.USER_CD.ColNo, dr.Item("USER_CD").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.USER_NM.ColNo, dr.Item("USER_NM").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SUB_KB.ColNo, dr.Item("SUB_KB").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SUB_KB_NM.ColNo, dr.Item("SUB_KB_NM").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.REM_NO.ColNo, dr.Item("REM_NO").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM260G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())


            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "プロパティ"

    ''' <summary>
    ''' セルのプロパティを設定(Label)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StyleInfoLabel(ByVal spr As LMSpread) As StyleInfo

        Return LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

    End Function

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

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
