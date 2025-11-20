' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM070G : 割増運賃マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMM070Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM070G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM070F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM070F, ByVal g As LMMControlG)

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
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .sprDetail.TabIndex = LMM070C.CtlTabIndex.DETAIL

            .cmbNrsBrCd.TabIndex = LMM070C.CtlTabIndex.NRSBRCD
            .txtExtcTariffCd.TabIndex = LMM070C.CtlTabIndex.EXTCTARIFFCD
            .cmbDataType.TabIndex = LMM070C.CtlTabIndex.DATATYPE
            .txtExtcTariffRem.TabIndex = LMM070C.CtlTabIndex.EXTCTARIFFREM
            .txtJisCd.TabIndex = LMM070C.CtlTabIndex.JISCD
            .cmbWintExtcYn.TabIndex = LMM070C.CtlTabIndex.WINTEXTCYN
            .imdWintKikanFrom.TabIndex = LMM070C.CtlTabIndex.WINTKIKANFROM
            .imdWintKikanTo.TabIndex = LMM070C.CtlTabIndex.WINTKIKANTO
            .cmbCityExtcYn.TabIndex = LMM070C.CtlTabIndex.CITYEXTCYN
            .cmbRelyExtcYn.TabIndex = LMM070C.CtlTabIndex.RELYEXTCYN
            .cmbFrryExtcYn.TabIndex = LMM070C.CtlTabIndex.FRRYEXTCYN
            'START YANAI 要望番号377
            .numFrryExtc10kg.TabIndex = LMM070C.CtlTabIndex.FRRYEXTC10KG
            'END YANAI 要望番号377
            .lblSituation.TabIndex = LMM070C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM070C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM070C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM070C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM070C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM070C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM070C.CtlTabIndex.SYSDELFLG
            .lblOyaDate.TabIndex = LMM070C.CtlTabIndex.OYADATE
            .lblOyaTime.TabIndex = LMM070C.CtlTabIndex.OYATIME
            .lblOyaSysDelFlg.TabIndex = LMM070C.CtlTabIndex.OYASYSDELFLG


        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        'コントロールの日付書式設定
        Call Me.SetDateControl()

        'START YANAI 要望番号377
        '数値コントロールの書式設定
        Call Me.SetNumberControl()
        'END YANAI 要望番号377

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        '数値項目以外のクリアを行う
        Call Me._ControlG.ClearControl(ctl)

        'START YANAI 要望番号377
        With Me._Frm
            '数値項目に初期値0を設定する            
            .numFrryExtc10kg.Value = 0
        End With
        'END YANAI 要望番号377

    End Sub

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateControl()

        With Me._Frm

            Me._ControlG.SetDateFormat(.imdWintKikanFrom, LMMControlC.DATE_FORMAT.MM_DD)
            Me._ControlG.SetDateFormat(.imdWintKikanTo, LMMControlC.DATE_FORMAT.MM_DD)

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM070C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM070C.EventShubetsu.MAIN, LMM070C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM070C.EventShubetsu.SHINKI
                    .txtExtcTariffCd.Focus()
                Case LMM070C.EventShubetsu.HUKUSHA, LMM070C.EventShubetsu.HENSHU
                    .txtExtcTariffRem.Focus()
            End Select
        End With

    End Sub

    'START YANAI 要望番号377
    ''' <summary>
    ''' 数値コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetNumberControl()

        With Me._frm

            Dim d9 As Decimal = Convert.ToDecimal(LMM070C.NB_MAX_9)
            Dim sharp9 As String = "###,###,##0"

            .numFrryExtc10kg.SetInputFields(sharp9, , 9, 1, , 0, 0, , d9, 0)

        End With

    End Sub
    'END YANAI 要望番号377

    ''' <summary>
    ''' 新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetValue()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbDataType.SelectedValue = " "
        Me._Frm.cmbWintExtcYn.SelectedValue = "00"
        Me._Frm.cmbCityExtcYn.SelectedValue = "00"
        Me._Frm.cmbRelyExtcYn.SelectedValue = "00"
        Me._Frm.cmbFrryExtcYn.SelectedValue = "00"


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
                            Call Me._ControlG.LockText(.txtExtcTariffCd, lock)
                            Call Me._ControlG.LockComb(.cmbDataType, lock)
                            Call Me._ControlG.LockText(.txtJisCd, lock)


                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockComb(.cmbDataType, lock)

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._ControlG.LockText(.txtExtcTariffCd, lock)
                            Call Me._ControlG.LockComb(.cmbDataType, lock)
                            '複写時キー項目のクリアを行う。
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

            .txtJisCd.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty


        End With

    End Sub

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .txtExtcTariffCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Text
            .txtExtcTariffRem.TextValue = .sprDetail.ActiveSheet.Cells( row, LMM070G.sprDetailDef.EXTC_TARIFF_REM.ColNo).Text
            .cmbDataType.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.DATA_TYPE.ColNo).Text
            .txtJisCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.JIS_CD.ColNo).Text
            .cmbWintExtcYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.WINT_EXTC_YN.ColNo).Text
            .imdWintKikanFrom.TextValue = DateFormatUtility.DeleteSlash(.sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.WINT_KIKAN_FROM.ColNo).Text)
            .imdWintKikanTo.TextValue = DateFormatUtility.DeleteSlash(.sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.WINT_KIKAN_TO.ColNo).Text)
            .cmbCityExtcYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.CITY_EXTC_YN.ColNo).Text
            .cmbRelyExtcYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.RELY_EXTC_YN.ColNo).Text
            .cmbFrryExtcYn.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.FRRY_EXTC_YN.ColNo).Text
            'START YANAI 要望番号377
            .numFrryExtc10kg.Value = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.FRRY_EXTC_10KG.ColNo).Text
            'END YANAI 要望番号377

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.SYS_DEL_FLG.ColNo).Text
            .lblOyaDate.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.OYA_DATE.ColNo).Text
            .lblOyaTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.OYA_TIME.ColNo).Text
            .lblOyaSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM070G.sprDetailDef.OYA_SYS_DEL_FLG.ColNo).Text


        End With

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    ''' 
    Public Class sprDetailDef
        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)           '営業所コード（隠し項目）        
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.EXTC_TARIFF_CD, "割増タリフ" & vbCrLf & "コード", 100, True)
        Public Shared EXTC_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.EXTC_TARIFF_REM, "割増タリフ備考", 120, True)
        Public Shared KEN As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.KEN, "都道府県名", 120, True)
        Public Shared SHI As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SHI, "市区町村名", 150, True)
        Public Shared DATA_TYPE As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.DATA_TYPE, "データタイプ" & vbCrLf & "コード", 60, False)
        Public Shared DATA_TYPE_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.DATA_TYPE_NM, "データタイプ", 100, True)
        Public Shared JIS_CD As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.JIS_CD, "到着JISコード", 120, True)
        Public Shared WINT_KIKAN_FROM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.WINT_KIKAN_FROM, "冬期期間FROM", 100, True)
        Public Shared WINT_KIKAN_TO As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.WINT_KIKAN_TO, "冬期期間TO", 100, True)
        Public Shared WINT_EXTC_YN As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.WINT_EXTC_YN, "冬期割増有無", 70, False)
        Public Shared WINT_EXTC_YN_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.WINT_EXTC_YN_NM, "冬期割増", 100, True)
        Public Shared CITY_EXTC_YN As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.CITY_EXTC_YN, "都市割増有無", 80, False)
        Public Shared CITY_EXTC_YN_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.CITY_EXTC_YN_NM, "都市割増", 100, True)
        Public Shared RELY_EXTC_YN As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.RELY_EXTC_YN, "中継割増有無", 80, False)
        Public Shared RELY_EXTC_YN_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.RELY_EXTC_YN_NM, "中継割増", 100, True)
        Public Shared FRRY_EXTC_YN As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.FRRY_EXTC_YN, "航送割増有無", 80, False)
        Public Shared FRRY_EXTC_YN_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.FRRY_EXTC_YN_NM, "航送割増", 100, True)
        'START YANAI 要望番号377
        Public Shared FRRY_EXTC_10KG As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.FRRY_EXTC_10KG, "10kgあたりの航送料", 100, True)
        'END YANAI 要望番号377

        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)
        Public Shared OYA_DATE As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.OYA_DATE, "親更新日", 60, False)
        Public Shared OYA_TIME As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.OYA_TIME, "親更新時刻", 60, False)
        Public Shared OYA_SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM070C.SprColumnIndex.OYA_SYS_DEL_FLG, "親削除フラグ", 60, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal ds As DataSet)
        Dim dr As DataRow
        With Me._Frm

            'START YANAI 要望番号377
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999999999, True, 0, True, ",")
            'END YANAI 要望番号377

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            'START YANAI 要望番号377
            '.sprDetail.Sheets(0).ColumnCount = 30
            .sprDetail.Sheets(0).ColumnCount = 31
            'END YANAI 要望番号377

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM070G.sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM070G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.EXTC_TARIFF_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 10, False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.EXTC_TARIFF_REM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            Call Me.SetKenComb(ds)
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SHI.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.DATA_TYPE_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "D008", False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.JIS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.WINT_KIKAN_FROM.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.WINT_KIKAN_TO.ColNo, LMSpreadUtility.GetDateTimeCell(.sprDetail, True))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.WINT_EXTC_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "W002", False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.CITY_EXTC_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "W003", False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.RELY_EXTC_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "W004", False))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.FRRY_EXTC_YN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "W005", False))
            'START YANAI 要望番号377
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.FRRY_EXTC_10KG.ColNo, sNumber1)
            'END YANAI 要望番号377

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.NRS_BR_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.DATA_TYPE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.WINT_EXTC_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.CITY_EXTC_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.RELY_EXTC_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.FRRY_EXTC_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.OYA_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.OYA_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.OYA_SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

 
    ''' <summary>
    ''' スプレッド都道府県コンボボックス設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetKenComb(ByVal ds As DataSet)

        Me._Frm.sprDetail.SetCellStyle(0, LMM070G.sprDetailDef.KEN.ColNo, LMSpreadUtility.GetComboCell(Me._Frm.sprDetail, New DataView(ds.Tables("LMM070KEN")), "KEN", "KEN", False))

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()
        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr


            .SetCellValue(0, LMM070G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM070G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM070G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM070G.sprDetailDef.EXTC_TARIFF_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.EXTC_TARIFF_REM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.KEN.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SHI.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.DATA_TYPE.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.DATA_TYPE_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.JIS_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.WINT_KIKAN_FROM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.WINT_KIKAN_TO.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.WINT_EXTC_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.WINT_EXTC_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.CITY_EXTC_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.CITY_EXTC_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.RELY_EXTC_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.RELY_EXTC_YN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.FRRY_EXTC_YN.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.FRRY_EXTC_YN_NM.ColNo, String.Empty)
            'START YANAI 要望番号377
            .SetCellValue(0, LMM070G.sprDetailDef.FRRY_EXTC_10KG.ColNo, String.Empty)
            'END YANAI 要望番号377

            .SetCellValue(0, LMM070G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)

            .SetCellValue(0, LMM070G.sprDetailDef.OYA_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.OYA_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM070G.sprDetailDef.OYA_SYS_DEL_FLG.ColNo, String.Empty)



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
            'START YANAI 要望番号377
            Dim sNumber1 As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, True, ",")
            'END YANAI 要望番号377

            Dim dr As DataRow = Nothing

            Dim winFrom As String = String.Empty
            Dim winTo As String = String.Empty


            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)


                'セルスタイル設定
                .SetCellStyle(i, LMM070G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.EXTC_TARIFF_REM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.KEN.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.SHI.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.DATA_TYPE.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.DATA_TYPE_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.WINT_KIKAN_FROM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.WINT_KIKAN_TO.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.WINT_EXTC_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.WINT_EXTC_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.CITY_EXTC_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.CITY_EXTC_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.RELY_EXTC_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.RELY_EXTC_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.FRRY_EXTC_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.FRRY_EXTC_YN_NM.ColNo, sLabel)
                'START YANAI 要望番号377
                .SetCellStyle(i, LMM070G.sprDetailDef.FRRY_EXTC_10KG.ColNo, sNumber1)
                'END YANAI 要望番号377

                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                .SetCellStyle(i, LMM070G.sprDetailDef.OYA_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.OYA_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM070G.sprDetailDef.OYA_SYS_DEL_FLG.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM070G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM070G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.EXTC_TARIFF_CD.ColNo, dr.Item("EXTC_TARIFF_CD").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.EXTC_TARIFF_REM.ColNo, dr.Item("EXTC_TARIFF_REM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.KEN.ColNo, dr.Item("KEN").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.SHI.ColNo, dr.Item("SHI").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.DATA_TYPE.ColNo, dr.Item("DATA_TYPE").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.DATA_TYPE_NM.ColNo, dr.Item("DATA_TYPE_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.JIS_CD.ColNo, dr.Item("JIS_CD").ToString())
                winFrom = DateFormatUtility.EditSlash(dr.Item("WINT_KIKAN_FROM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.WINT_KIKAN_FROM.ColNo, winFrom)
                winTo = DateFormatUtility.EditSlash(dr.Item("WINT_KIKAN_TO").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.WINT_KIKAN_TO.ColNo, winTo)
                .SetCellValue(i, LMM070G.sprDetailDef.WINT_EXTC_YN.ColNo, dr.Item("WINT_EXTC_YN").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.WINT_EXTC_YN_NM.ColNo, dr.Item("WINT_EXTC_YN_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.CITY_EXTC_YN.ColNo, dr.Item("CITY_EXTC_YN").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.CITY_EXTC_YN_NM.ColNo, dr.Item("CITY_EXTC_YN_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.RELY_EXTC_YN.ColNo, dr.Item("RELY_EXTC_YN").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.RELY_EXTC_YN_NM.ColNo, dr.Item("RELY_EXTC_YN_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.FRRY_EXTC_YN.ColNo, dr.Item("FRRY_EXTC_YN").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.FRRY_EXTC_YN_NM.ColNo, dr.Item("FRRY_EXTC_YN_NM").ToString())
                'START YANAI 要望番号377
                .SetCellValue(i, LMM070G.sprDetailDef.FRRY_EXTC_10KG.ColNo, dr.Item("FRRY_EXTC_10KG").ToString())
                'END YANAI 要望番号377

                .SetCellValue(i, LMM070G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

                .SetCellValue(i, LMM070G.sprDetailDef.OYA_DATE.ColNo, dr.Item("OYA_DATE").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.OYA_TIME.ColNo, dr.Item("OYA_TIME").ToString())
                .SetCellValue(i, LMM070G.sprDetailDef.OYA_SYS_DEL_FLG.ColNo, dr.Item("OYA_SYS_DEL_FLG").ToString())


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
    ''' 画面の値に応じてのロック制御-冬季割増区分
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl(ByVal actionType As LMM070C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            '冬季割増区分が有の場合はロック解除
            If .cmbWintExtcYn.SelectedValue.ToString() = "01" _
            Or .cmbWintExtcYn.SelectedValue.ToString() = "02" Then
                Call Me._ControlG.LockDate(.imdWintKikanFrom, unLock)
                Call Me._ControlG.LockDate(.imdWintKikanTo, unLock)

                Exit Sub

                '冬季割増区分が無の場合はロック
            End If
            Call Me._ControlG.LockDate(.imdWintKikanFrom, lock)
            Call Me._ControlG.LockDate(.imdWintKikanTo, lock)

            'ロックする場合は値をクリア
            .imdWintKikanFrom.TextValue = String.Empty
            .imdWintKikanTo.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' 画面の値に応じてのロック制御-データタイプ
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl2(ByVal actionType As LMM070C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm


            'データタイプが「代表」だった場合はロック
            If .cmbDataType.SelectedValue.ToString() = "00" Then
                'JISコードを親コードに設定
                .txtJisCd.TextValue = "0000000"
                'ロックする場合は値をクリア
                .cmbWintExtcYn.SelectedValue = String.Empty
                .imdWintKikanFrom.TextValue = String.Empty
                .imdWintKikanTo.TextValue = String.Empty
                .cmbCityExtcYn.SelectedValue = String.Empty
                .cmbRelyExtcYn.SelectedValue = String.Empty
                .cmbFrryExtcYn.SelectedValue = String.Empty
                'START YANAI 要望番号377
                .numFrryExtc10kg.Value = 0
                'END YANAI 要望番号377
                Call Me._ControlG.LockText(.txtJisCd, lock)
                Call Me._ControlG.LockComb(.cmbWintExtcYn, lock)
                Call Me._ControlG.LockComb(.cmbCityExtcYn, lock)
                Call Me._ControlG.LockComb(.cmbRelyExtcYn, lock)
                Call Me._ControlG.LockComb(.cmbFrryExtcYn, lock)
                '2011.08.26 まとめ検証結果(LMM_画面)№41対応 START
                Call Me._ControlG.LockDate(.imdWintKikanFrom, lock)
                Call Me._ControlG.LockDate(.imdWintKikanTo, lock)
                '2011.08.26 まとめ検証結果(LMM_画面)№41対応 END
                'START YANAI 要望番号377
                Call Me._ControlG.LockNumber(.numFrryExtc10kg, lock)
                'END YANAI 要望番号377

                Exit Sub

                'データタイプが「明細」だった場合はロックしない
            End If


        End With

    End Sub

    ''' <summary>
    ''' 画面の値に応じてのロック制御-Jisコード
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl3(ByVal actionType As LMM070C.EventShubetsu)

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        With Me._Frm


            '参照モードの場合、スルー
            'If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
            '    Exit Sub
            'End If

            Select Case Me._Frm.lblSituation.RecordStatus
                Case RecordStatus.NEW_REC, RecordStatus.COPY_REC

                    'JISコードが親コードだった場合はロック
                    If .txtJisCd.TextValue.ToString() = "0000000" Then
                        'ロックする場合は値をクリア
                        .cmbWintExtcYn.SelectedValue = String.Empty
                        .imdWintKikanFrom.TextValue = String.Empty
                        .imdWintKikanTo.TextValue = String.Empty
                        .cmbCityExtcYn.SelectedValue = String.Empty
                        .cmbRelyExtcYn.SelectedValue = String.Empty
                        .cmbFrryExtcYn.SelectedValue = String.Empty
                        'START YANAI 要望番号377
                        .numFrryExtc10kg.Value = 0
                        'END YANAI 要望番号377
                        'Call Me._ControlG.LockText(.txtJisCd, lock)
                        Call Me._ControlG.LockComb(.cmbWintExtcYn, lock)
                        Call Me._ControlG.LockComb(.cmbCityExtcYn, lock)
                        Call Me._ControlG.LockComb(.cmbRelyExtcYn, lock)
                        Call Me._ControlG.LockComb(.cmbFrryExtcYn, lock)
                        'START YANAI 要望番号377
                        Call Me._ControlG.LockNumber(.numFrryExtc10kg, lock)
                        'END YANAI 要望番号377

                        Exit Sub

                        'JISコードが子コードだった場合はロック解除
                    End If                    
                    'Call Me.SetValue() '2011.08.25 まとめ検証結果(LMM_画面)№37対応
                    Call Me._ControlG.LockComb(.cmbWintExtcYn, unLock)
                    Call Me._ControlG.LockComb(.cmbCityExtcYn, unLock)
                    Call Me._ControlG.LockComb(.cmbRelyExtcYn, unLock)
                    Call Me._ControlG.LockComb(.cmbFrryExtcYn, unLock)
                    'START YANAI 要望番号377
                    Call Me._ControlG.LockNumber(.numFrryExtc10kg, unLock)
                    'END YANAI 要望番号377

                Case Else
            End Select
        End With

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
