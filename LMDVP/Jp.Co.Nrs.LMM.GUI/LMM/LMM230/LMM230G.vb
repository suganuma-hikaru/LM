' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM230G : ユーザーマスタメンテナンス
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

''' <summary>
''' LMM230Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM230G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM230F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM230V

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM230F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMMConG = g

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
            .F3ButtonName = String.Empty
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
            .F11ButtonName = LMMControlC.FUNCTION_F11_HOZON
            .F12ButtonName = LMMControlC.FUNCTION_F12_TOJIRU

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
            .F10ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F4ButtonEnabled = view
            .F11ButtonEnabled = edit
            '行追加・行削除ボタン
            Me._Frm.btnRowAdd.Enabled = edit
            Me._Frm.btnRowDel.Enabled = edit


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
            .txtJis.TabIndex = LMM230C.CtlTabIndex.JISCD
            .lblKen.TabIndex = LMM230C.CtlTabIndex.KEN
            .lblShi.TabIndex = LMM230C.CtlTabIndex.SHI
            .sprDetail.TabIndex = LMM230C.CtlTabIndex.DETAIL
            .pnlEdit.TabIndex = LMM230C.CtlTabIndex.PNL
            .cmbNrsBrCd.TabIndex = LMM230C.CtlTabIndex.NRSBRCD
            .txtAreaCd.TabIndex = LMM230C.CtlTabIndex.AREACD
            .txtAreaNm.TabIndex = LMM230C.CtlTabIndex.AREANM
            .cmbBin.TabIndex = LMM230C.CtlTabIndex.BIN
            .txtAreaInfo.TabIndex = LMM230C.CtlTabIndex.AREAINFO
            .btnRowAdd.TabIndex = LMM230C.CtlTabIndex.ROWADD
            .btnRowDel.TabIndex = LMM230C.CtlTabIndex.ROWDEL
            .sprDetail2.TabIndex = LMM230C.CtlTabIndex.DETAIL2
            .lblSituation.TabIndex = LMM230C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM230C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM230C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM230C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM230C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM230C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM230C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me._LMMConG.ClearControl(Me._Frm)

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
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
            
        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Me._LMMConG.ClearControl(.pnlEdit)
                    Me._LMMConG.SetLockControl(.pnlEdit, True)
                    
                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Me._LMMConG.SetLockControl(.pnlEdit, False)
                            Me._LMMConG.SetLockControl(.cmbNrsBrCd, True)
                            Me._LMMConG.SetLockControl(.txtAreaCd, True)
                            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
                            Me._LMMConG.SetLockControl(.cmbBin, True)
                            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


                            '新規
                        Case RecordStatus.NEW_REC
                            Me._LMMConG.SetLockControl(.pnlEdit, False)
                            Me._LMMConG.SetLockControl(.cmbNrsBrCd, True)
                            Me._LMMConG.ClearControl(.pnlEdit)
                            .sprDetail2.CrearSpread()

                    End Select

                Case DispMode.INIT
                    Me._LMMConG.ClearControl(.pnlEdit)
                    Me._LMMConG.SetLockControl(.pnlEdit, True)
                    .sprDetail2.CrearSpread()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM230C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM230C.EventShubetsu.MAIN, LMM230C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM230C.EventShubetsu.SHINKI
                    .txtAreaCd.Focus()
                Case LMM230C.EventShubetsu.HENSHU
                    .txtAreaNm.Focus()
                Case LMM230C.EventShubetsu.DEL_T, LMM230C.EventShubetsu.INS_T
                    .sprDetail2.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtAreaCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.AREA_CD.ColNo).Text
            .txtAreaNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.AREA_NM.ColNo).Text
            .cmbBin.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.BIN_KB.ColNo).Text
            .txtAreaInfo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.AREA_INFO.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared AREA_CD As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.AREA_CD, "エリアコード", 100, True)
        Public Shared AREA_NM As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.AREA_NM, "エリア名", 150, True)
        Public Shared BIN_KB As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.BIN_KB, "便区分名", 50, False)
        Public Shared BIN_KB_NM As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.BIN_KB_NM, "便区分", 90, True)
        Public Shared AREA_INFO As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.AREA_INFO, "備考", 400, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)

    End Class

    ''' <summary>
    ''' スプレッド列定義体(下部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef2

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex2.DEF, " ", 20, True)
        Public Shared JIS_CD As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex2.JIS_CD, "JISコード", 100, True)
        Public Shared KEN As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex2.KEN, "都道府県名", 100, True)
        Public Shared SHI As SpreadColProperty = New SpreadColProperty(LMM230C.SprColumnIndex2.SHI, "市区町村名", 300, True)

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
            .sprDetail2.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 15
            .sprDetail2.ActiveSheet.ColumnCount = 4

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM230G.sprDetailDef())
            .sprDetail2.SetColProperty(New LMM230G.sprDetailDef2())

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM230G.sprDetailDef.DEF.ColNo + 1

            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)
            Dim lblStyle2 As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail2, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.NRS_BR_CD.ColNo, lblStyle)
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.AREA_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.AREA_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.BIN_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.BIN_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "U001", False))
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.AREA_INFO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)

            '列設定（下部）
            .sprDetail2.SetCellStyle(-1, LMM230G.sprDetailDef2.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(.sprDetail2, False))
            .sprDetail2.SetCellStyle(-1, LMM230G.sprDetailDef2.JIS_CD.ColNo, lblStyle2)
            .sprDetail2.SetCellStyle(-1, LMM230G.sprDetailDef2.KEN.ColNo, lblStyle2)
            .sprDetail2.SetCellStyle(-1, LMM230G.sprDetailDef2.SHI.ColNo, lblStyle2)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM230F)

        With frm.sprDetail

            .SetCellValue(0, LMM230G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM230G.sprDetailDef.AREA_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.AREA_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.BIN_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.BIN_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.AREA_INFO.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo, LMConst.FLG.OFF)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

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
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM230G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.AREA_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.AREA_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.BIN_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.BIN_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.AREA_INFO.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM230G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.AREA_CD.ColNo, dr.Item("AREA_CD").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.AREA_NM.ColNo, dr.Item("AREA_NM").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.BIN_KB.ColNo, dr.Item("BIN_KB").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.BIN_KB_NM.ColNo, dr.Item("BIN_KB_NM").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.AREA_INFO.ColNo, dr.Item("AREA_INFO").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM230G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定_SPREADダブルクリック時(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread2(ByVal dt As DataTable, ByVal areaCd As String, ByVal binKb As String)

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start(関数に引数追加)
        'Friend Sub SetSpread2(ByVal dt As DataTable, ByVal areaCd As String)
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        Dim spr2 As LMSpread = Me._Frm.sprDetail2
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start(関数に引数追加)
        'Dim setDrs As DataRow() = dt.Select(Me.SetJisSql(areaCd), "JIS_CD")
        Dim setDrs As DataRow() = dt.Select(Me.SetJisSql(areaCd, binKb), "JIS_CD")
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End


        Dim max As Integer = setDrs.Length - 1

        'セルに設定するスタイルの取得
        Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr2, False)
        Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr2, CellHorizontalAlignment.Left)

        sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

        Dim dr As DataRow = Nothing
        Dim rowCnt As Integer = 0

        With spr2

            .SuspendLayout()
            'スプレッドの行をクリア
            .CrearSpread()

            For i As Integer = 0 To max

                dr = setDrs(i)

                '編集モードでSYS_DEL_FLGが'1'のものは表示しない
                If DispMode.EDIT.Equals(Me._Frm.lblSituation.DispMode) = True _
                    AndAlso LMConst.FLG.ON.Equals(dr.Item("SYS_DEL_FLG").ToString()) = True Then

                    Continue For

                End If

                '設定する行数を設定
                rowCnt = .ActiveSheet.Rows.Count

                '行追加
                .ActiveSheet.AddRows(rowCnt, 1)

                'セルスタイル設定
                .SetCellStyle(rowCnt, LMM230G.sprDetailDef2.DEF.ColNo, sDEF)
                .SetCellStyle(rowCnt, LMM230G.sprDetailDef2.JIS_CD.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMM230G.sprDetailDef2.KEN.ColNo, sLabel)
                .SetCellStyle(rowCnt, LMM230G.sprDetailDef2.SHI.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(rowCnt, LMM230G.sprDetailDef2.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(rowCnt, LMM230G.sprDetailDef2.JIS_CD.ColNo, dr.Item("JIS_CD").ToString())
                .SetCellValue(rowCnt, LMM230G.sprDetailDef2.KEN.ColNo, dr.Item("KEN").ToString())
                .SetCellValue(rowCnt, LMM230G.sprDetailDef2.SHI.ColNo, dr.Item("SHI").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

  
    ''' <summary>
    ''' エリアコードにひもづくJISコード取得SQL構築
    ''' </summary>
    ''' <param name="areaCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetJisSql(ByVal areaCd As String, ByVal binKb As String) As String

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start(関数に引数追加)
        'Friend Function SetJisSql(ByVal areaCd As String) As String
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
        'Return String.Concat("AREA_CD = '", areaCd, "' ")
        Return String.Concat("AREA_CD = '", areaCd, "' ", " AND ", "BIN_KB = '", binKb, "' ")
        '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

    End Function



#End Region

#End Region

End Class
