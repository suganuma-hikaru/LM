' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM190G : 距離程マスタメンテ
'  作  成  者       :  平山
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
''' LMM190Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM190G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM190F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM190F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

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
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = view
            .F10ButtonEnabled = edit
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
            .sprDetail.TabIndex = LMM190C.CtlTabIndex.DETAIL

            .cmbNrsBrCd.TabIndex = LMM190C.CtlTabIndex.NRSBRCD
            .txtKyoriCd.TabIndex = LMM190C.CtlTabIndex.KYORICD
            .txtOrigJisCd.TabIndex = LMM190C.CtlTabIndex.ORIGJISCD
            .lblOrigKenNm.TabIndex = LMM190C.CtlTabIndex.ORIGKEN
            .lblOrigShiNm.TabIndex = LMM190C.CtlTabIndex.ORIGSHI
            .txtDestJisCd.TabIndex = LMM190C.CtlTabIndex.DESTJISCD
            .lblDestKenNm.TabIndex = LMM190C.CtlTabIndex.DESTKEN
            .lblDestShiNm.TabIndex = LMM190C.CtlTabIndex.DESTSHI
            .numKyori.TabIndex = LMM190C.CtlTabIndex.KYORI
            .txtKyoriRem.TabIndex = LMM190C.CtlTabIndex.KYORIREM

            .lblSituation.TabIndex = LMM190C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM190C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM190C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM190C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM190C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM190C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM190C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

        With Me._Frm
            .numKyori.Value = 0
        End With

    End Sub

    ''' <summary>
    ''' 新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetValue()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm


            'numberCellの桁数を設定する
            .numKyori.SetInputFields("#,##0", , 4, 1, , 0, 0, , 9999, 0)

        End With

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
                            Call Me._ControlG.LockText(.txtKyoriCd, lock)
                            Call Me._ControlG.LockText(.txtOrigJisCd, lock)
                            Call Me._ControlG.LockText(.lblOrigKenNm, lock)
                            Call Me._ControlG.LockText(.lblOrigShiNm, lock)
                            Call Me._ControlG.LockText(.txtDestJisCd, lock)
                            Call Me._ControlG.LockText(.lblDestKenNm, lock)
                            Call Me._ControlG.LockText(.lblDestShiNm, lock)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)

                    End Select

            End Select

        End With

    End Sub


    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM190C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM190C.EventShubetsu.MAIN, LMM190C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM190C.EventShubetsu.SHINKI
                    .txtKyoriCd.Focus()
                Case LMM190C.EventShubetsu.HENSHU
                    .numKyori.Focus()
            End Select
        End With

    End Sub


    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtKyoriCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.KYORI_CD.ColNo).Text
            .txtOrigJisCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo).Text
            .lblOrigKenNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.ORIG_KEN.ColNo).Text
            .lblOrigShiNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.ORIG_SHI.ColNo).Text
            .txtDestJisCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo).Text
            .lblDestKenNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.DEST_KEN.ColNo).Text
            .lblDestShiNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.DEST_SHI.ColNo).Text
            .numKyori.Value = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.KYORI.ColNo).Text
            .txtKyoriRem.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.KYORI_REM.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM190G.sprDetailDef.SYS_DEL_FLG.ColNo).Text


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

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)           '営業所コード（隠し項目）        
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared KYORI_CD As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.KYORI_CD, "距離程コード", 100, True)
        Public Shared ORIG_JIS_CD As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.ORIG_JIS_CD, "発地JISコード", 120, True)
        Public Shared ORIG_KEN As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.ORIG_KEN, "発地都道府県名", 60, False)       '都道府県名(隠し項目)
        Public Shared ORIG_SHI As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.ORIG_SHI, "発地市区町村名", 200, True)
        Public Shared DEST_JIS_CD As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.DEST_JIS_CD, "届先JISコード", 120, True)
        Public Shared DEST_KEN As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.DEST_KEN, "届先都道府県名", 60, False)               '都道府県名(隠し項目)
        Public Shared DEST_SHI As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.DEST_SHI, "届先市区町村名", 200, True)
        Public Shared KYORI As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.KYORI, "距離", 60, False)                       '距離(隠し項目)
        Public Shared KYORI_REM As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.KYORI_REM, "備考", 300, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM190C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)

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
            .sprDetail.ActiveSheet.ColumnCount = 19

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM190G.sprDetailDef())

            '列固定位置を設定します。
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM190G.sprDetailDef.ORIG_SHI.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.KYORI_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.ORIG_SHI.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.DEST_SHI.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.KYORI_REM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.KYORI.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.ORIG_KEN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.DEST_KEN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM190G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()
        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr


            .SetCellValue(0, LMM190G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM190G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM190G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM190G.sprDetailDef.KYORI_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.ORIG_SHI.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.DEST_SHI.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.KYORI_REM.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.KYORI.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.ORIG_KEN.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.DEST_KEN.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM190G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)



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
                .SetCellStyle(i, LMM190G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.KYORI_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.ORIG_KEN.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.ORIG_SHI.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.DEST_KEN.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.DEST_SHI.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.KYORI.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.KYORI_REM.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM190G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM190G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.KYORI_CD.ColNo, dr.Item("KYORI_CD").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.ORIG_JIS_CD.ColNo, dr.Item("ORIG_JIS_CD").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.ORIG_KEN.ColNo, dr.Item("ORIG_KEN").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.ORIG_SHI.ColNo, dr.Item("ORIG_SHI").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.DEST_JIS_CD.ColNo, dr.Item("DEST_JIS_CD").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.DEST_KEN.ColNo, dr.Item("DEST_KEN").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.DEST_SHI.ColNo, dr.Item("DEST_SHI").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.KYORI.ColNo, dr.Item("KYORI").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.KYORI_REM.ColNo, dr.Item("KYORI_REM").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM190G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())


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

End Class
