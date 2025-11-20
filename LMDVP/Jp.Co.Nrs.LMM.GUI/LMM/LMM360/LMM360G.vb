' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM360G : 請求テンプレートマスタメンテ
'  作  成  者     : [kishi]
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

''' <summary>
''' LMM360Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM360G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM360F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM360F)

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
            .F10ButtonName = LMMControlC.FUNCTION_F10_MST_SANSHO
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
            .F10ButtonEnabled = edit
            .F11ButtonEnabled = edit

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

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
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM360C.CtlTabIndex.DETAIL

            .cmbNrsBrCd.TabIndex = LMM360C.CtlTabIndex.NRSBRCD
            .txtSeiqtoCd.TabIndex = LMM360C.CtlTabIndex.SEIQTOCD
            .txtPtnCd.TabIndex = LMM360C.CtlTabIndex.PTNCD
            .txtTcustBpCd.TabIndex = LMM360C.CtlTabIndex.TCUST_BPCD
            .lblTcustBpNm.TabIndex = LMM360C.CtlTabIndex.TCUST_BPNM
            .cmbGroupKbn.TabIndex = LMM360C.CtlTabIndex.GROUPKB
            .txtSeiqkmkCd.TabIndex = LMM360C.CtlTabIndex.SEIQKMKCD
            .txtSeiqkmkCdS.TabIndex = LMM360C.CtlTabIndex.SEIQKMKCD_S
            .numkeisanTlgk.TabIndex = LMM360C.CtlTabIndex.KEISANTLGK
            .numNebikiRt.TabIndex = LMM360C.CtlTabIndex.NEBIKIRT
            .numNebikiGk.TabIndex = LMM360C.CtlTabIndex.NEBIKIGK
            .txtTekiyo.TabIndex = LMM360C.CtlTabIndex.TEKIYO

            .lblSituation.TabIndex = LMM360C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM360C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM360C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM360C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM360C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM360C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM360C.CtlTabIndex.SYSDELFLG


        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM360C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbBox()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl()

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d10 As Decimal = Convert.ToDecimal(9999999999)
            Dim d100 As Decimal = Convert.ToDecimal(100.0)
            Dim d9 As Decimal = Convert.ToDecimal(999999999)
          
            'numberCellの桁数を設定する
            .numkeisanTlgk.SetInputFields("#,###,###,##0", , 10, 1, , 0, 0, , d10, 0, , , )
            .numNebikiRt.SetInputFields("##0.00", , 3, 1, , 2, 2, , d100, 0)
            .numNebikiGk.SetInputFields("###,###,##0", , 9, 1, , 0, 0, , d9, 0, , , )

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
                    Me.ClearControl()
                    Me.LockControl(True)

                Case DispMode.EDIT

                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Me.SetLockControl(.txtSeiqtoCd, True)
                            Me.SetLockControl(.txtPtnCd, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)

                            '複写
                        Case RecordStatus.COPY_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Call Me.ClearControlFukusha()

                    End Select

                Case DispMode.INIT
                    Me.ClearControl()
                    Me.LockControl(True)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtPtnCd.TextValue = String.Empty
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
    Friend Sub SetFoucus(ByVal eventType As LMM360C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM360C.EventShubetsu.MAIN, LMM360C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM360C.EventShubetsu.SHINKI, LMM360C.EventShubetsu.HUKUSHA
                    .txtSeiqtoCd.Focus()
                Case LMM360C.EventShubetsu.HENSHU
                    .txtTcustBpCd.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = String.Empty
            .txtSeiqtoCd.TextValue = String.Empty
            .lblSeiqNm.TextValue = String.Empty
            .txtPtnCd.TextValue = String.Empty
            .txtTcustBpCd.TextValue = String.Empty
            .lblTcustBpNm.TextValue = String.Empty
            .cmbGroupKbn.SelectedValue = String.Empty
            .txtSeiqkmkCd.TextValue = String.Empty
            .txtSeiqkmkCdS.TextValue = String.Empty
            .lblSeiqkmkNm.TextValue = String.Empty
            .numkeisanTlgk.Value = 0
            .numNebikiRt.Value = 0
            .numNebikiGk.Value = 0
            .txtTekiyo.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdTime.TextValue = String.Empty
            .lblSysDelFlg.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtSeiqtoCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SEIQTO_CD.ColNo).Text
            .lblSeiqNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SEIQTO_NM.ColNo).Text
            .txtPtnCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.PTN_CD.ColNo).Text
            .txtTcustBpCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.TCUST_BPCD.ColNo).Text
            .lblTcustBpNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.TCUST_BPNM.ColNo).Text
            .cmbGroupKbn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.GROUP_KB.ColNo).Text
            .txtSeiqkmkCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SEIQKMK_CD.ColNo).Text
            .txtSeiqkmkCdS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SEIQKMK_CD_S.ColNo).Text
            .lblSeiqkmkNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SEIQKMK_NM.ColNo).Text
            .numkeisanTlgk.Value = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.KEISAN_TLGK.ColNo).Text
            .numNebikiRt.Value = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.NEBIKI_RT.ColNo).Text
            .numNebikiGk.Value = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.NEBIKI_GK.ColNo).Text
            .txtTekiyo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.TEKIYO.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM360G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)              '営業所コード(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared SEIQTO_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQTO_CD, "請求先" & vbCrLf & "コード", 70, True)
        Public Shared SEIQTO_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQTO_NM, "請求先名", 250, True)
        Public Shared PTN_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.PTN_CD, "請求パターン" & vbCrLf & "コード", 120, True)
        Public Shared GROUP_KB_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.GROUP_KB_NM, "請求グループ" & vbCrLf & "コード区分", 100, True)
        Public Shared GROUP_KB As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.GROUP_KB, "請求グループコード区分", 100, False)
        Public Shared SEIQKMK_CD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQKMK_CD, "請求項目" & vbCrLf & "コード", 100, True)
        Public Shared SEIQKMK_CD_S As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQKMK_CD_S, "請求項目" & vbCrLf & "CD小", 60, True)
        Public Shared KEISAN_TLGK As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.KEISAN_TLGK, "計算額", 120, True)
        Public Shared NEBIKI_RT As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NEBIKI_RT, "値引率", 100, True)
        Public Shared NEBIKI_GK As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.NEBIKI_GK, "固定値引額", 100, True)
        Public Shared TEKIYO As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.TEKIYO, "摘要", 300, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared SEIQKMK_NM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.SEIQKMK_NM, "請求項目名", 60, False)
        Public Shared TCUST_BPCD As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.TCUST_BPCD, "真荷主コード", 50, False)
        Public Shared TCUST_BPNM As SpreadColProperty = New SpreadColProperty(LMM360C.SprColumnIndex.TCUST_BPNM, "真荷主名", 50, False)

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
            .sprDetail.ActiveSheet.ColumnCount = 24

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.SEIQTO_NM.ColNo + 1
            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SEIQTO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SEIQTO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 122, False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.PTN_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.GROUP_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S024", False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.GROUP_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SEIQKMK_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SEIQKMK_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.KEISAN_TLGK.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, InputControl.HAN_NUMBER, 10, True))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.NEBIKI_RT.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, InputControl.HAN_NUMBER, 5, True))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.NEBIKI_GK.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, InputControl.HAN_NUMBER, 9, True))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.TEKIYO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.SEIQKMK_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.TCUST_BPCD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM360G.sprDetailDef.TCUST_BPNM.ColNo, lblStyle)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM360F)

        With frm.sprDetail

            .SetCellValue(0, LMM360G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM360G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM360G.sprDetailDef.SEIQTO_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SEIQTO_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.PTN_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.GROUP_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.GROUP_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SEIQKMK_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SEIQKMK_CD_S.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.KEISAN_TLGK.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.NEBIKI_RT.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.NEBIKI_GK.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.TEKIYO.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.SEIQKMK_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.TCUST_BPCD.ColNo, String.Empty)
            .SetCellValue(0, LMM360G.sprDetailDef.TCUST_BPNM.ColNo, String.Empty)

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet
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
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sNumber As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim sTlgk As StyleInfo = Me.StyleInfoNum10(spr, lock)
            Dim sRt As StyleInfo = Me.StyleInfoNum3dec2(spr, lock)
            Dim sGk As StyleInfo = Me.StyleInfoNum9(spr, lock)
            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM360G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SEIQTO_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SEIQTO_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.PTN_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.GROUP_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.GROUP_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SEIQKMK_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SEIQKMK_CD_S.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.KEISAN_TLGK.ColNo, sTlgk)
                .SetCellStyle(i, LMM360G.sprDetailDef.NEBIKI_RT.ColNo, sRt)
                .SetCellStyle(i, LMM360G.sprDetailDef.NEBIKI_GK.ColNo, sGk)
                .SetCellStyle(i, LMM360G.sprDetailDef.TEKIYO.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.SEIQKMK_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.TCUST_BPCD.ColNo, sLabel)
                .SetCellStyle(i, LMM360G.sprDetailDef.TCUST_BPNM.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, LMM360G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM360G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SEIQTO_CD.ColNo, dr.Item("SEIQTO_CD").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SEIQTO_NM.ColNo, dr.Item("SEIQTO_NM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.TCUST_BPCD.ColNo, dr.Item("TCUST_BPCD").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.TCUST_BPNM.ColNo, dr.Item("TCUST_BPNM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.PTN_CD.ColNo, dr.Item("PTN_CD").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.GROUP_KB_NM.ColNo, dr.Item("GROUP_KB_NM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.GROUP_KB.ColNo, dr.Item("GROUP_KB").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SEIQKMK_CD.ColNo, dr.Item("SEIQKMK_CD").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SEIQKMK_CD_S.ColNo, dr.Item("SEIQKMK_CD_S").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.KEISAN_TLGK.ColNo, dr.Item("KEISAN_TLGK").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.NEBIKI_RT.ColNo, dr.Item("NEBIKI_RT").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.NEBIKI_GK.ColNo, dr.Item("NEBIKI_GK").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.TEKIYO.ColNo, dr.Item("TEKIYO").ToString())

                .SetCellValue(i, LMM360G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())
                .SetCellValue(i, LMM360G.sprDetailDef.SEIQKMK_NM.ColNo, dr.Item("SEIQKMK_NM").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

#Region "部品化検討中"
    ''' <summary>
    ''' セルのプロパティを設定(Number 整数10桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum10(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -9999999999, 9999999999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数9桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum9(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, -999999999, 999999999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数3桁　少数2桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum3dec2(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999.99, lock, 2, , ",")

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, lock, 3, , ",")

    End Function
    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.txtSeiqtoCd, lock)
            Me.SetLockControl(.txtTcustBpCd, lock)
            Me.SetLockControl(.txtPtnCd, lock)
            Me.SetLockControl(.cmbGroupKbn, lock)
            Me.SetLockControl(.txtSeiqkmkCd, lock)
            Me.SetLockControl(.txtSeiqkmkCdS, lock)
            Me.SetLockControl(.numkeisanTlgk, lock)
            Me.SetLockControl(.numNebikiRt, lock)
            Me.SetLockControl(.numNebikiGk, lock)
            Me.SetLockControl(.txtTekiyo, lock)

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーロック処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey()

        Me.SetLockControl(Me._Frm.FunctionKey, True)

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



#End Region

End Class
