' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM400G : 西濃着点マスタメンテ
'  作  成  者     : adachi
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
''' LMM400Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM400G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM400F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM400F, ByVal g As LMMControlG)

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
            .sprDetail.TabIndex = LMM400C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM400C.CtlTabIndex.NRSBRCD
            .txtCustCdL.TabIndex = LMM400C.CtlTabIndex.CUST_CD_L
            .lblCustNmL.TabIndex = LMM400C.CtlTabIndex.CUST_NM_L
            .txtCustCdM.TabIndex = LMM400C.CtlTabIndex.CUST_CD_M
            .lblCustNmM.TabIndex = LMM400C.CtlTabIndex.CUST_NM_M
            .txtZipNo.TabIndex = LMM400C.CtlTabIndex.ZIP_NO
            .txtKenK.TabIndex = LMM400C.CtlTabIndex.KEN_K
            .txtCityK.TabIndex = LMM400C.CtlTabIndex.CITY_K
            .txtShiwakeCd.TabIndex = LMM400C.CtlTabIndex.SHIWAKE_CD
            .txtChakuCd.TabIndex = LMM400C.CtlTabIndex.CHAKU_CD
            .txtChakuNm.TabIndex = LMM400C.CtlTabIndex.CHAKU_NM
            .lblCrtUser.TabIndex = LMM400C.CtlTabIndex.CRTUSER
            .lblCrtDate.TabIndex = LMM400C.CtlTabIndex.CRTDATE
            .lblUpdUser.TabIndex = LMM400C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM400C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM400C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM400C.CtlTabIndex.SYSDELFLG
            .lblSeinoKey.TabIndex = LMM400C.CtlTabIndex.SEINOKEY

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM400C.EventShubetsu, ByVal recstatus As Object)

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

            Dim d999 As Decimal = Convert.ToDecimal(999)

            ''numberの桁数を設定する
            '.numNihudaMxCnt.SetInputFields("##0", , 3, 1, , 0, 0, , d999, 0, , , , )

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

                        '参照henshu

                        Case RecordStatus.NOMAL_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Me.SetLockControl(.txtZipNo, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)


                            '複写
                        Case RecordStatus.COPY_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            '2015/3/19 入力必要なため入力可能に
                            'Me.SetLockControl(.txtZipNo, True)
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

            .lblSeinoKey.TextValue = String.Empty
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
    Friend Sub SetFoucus(ByVal eventType As LMM400C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM400C.EventShubetsu.MAIN, LMM400C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM400C.EventShubetsu.SHINKI, LMM400C.EventShubetsu.HUKUSHA
                    .txtCustCdL.Focus()
                Case LMM400C.EventShubetsu.HENSHU
                    .txtCustCdL.Focus()
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
            .lblSeinoKey.TextValue = String.Empty
            .txtCustCdL.TextValue = String.Empty
            .txtCustCdM.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .lblCustNmM.TextValue = String.Empty
            .txtZipNo.TextValue = String.Empty
            .txtKenK.TextValue = String.Empty
            .txtCityK.TextValue = String.Empty
            .txtShiwakeCd.TextValue = String.Empty
            .txtChakuCd.TextValue = String.Empty
            .txtChakuNm.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
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

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .lblSeinoKey.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SEINO_KEY.ColNo).Text
            .lblCustNmL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CUST_NM_L.ColNo).Text
            .lblCustNmM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CUST_NM_M.ColNo).Text
            .txtCustCdL.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CUST_CD_L.ColNo).Text
            .txtCustCdM.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CUST_CD_M.ColNo).Text
            .txtZipNo.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.ZIP_NO.ColNo).Text
            .txtKenK.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.KEN_K.ColNo).Text
            .txtCityK.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CITY_K.ColNo).Text
            .txtShiwakeCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SHIWAKE_CD.ColNo).Text
            .txtChakuCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CHAKU_CD.ColNo).Text
            .txtChakuNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.CHAKU_NM.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM400G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.NRS_BR_CD, "営業所コード", 50, False)              '営業所コード(隠し項目)
        Public Shared SEINO_KEY As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SEINO_KEY, "SEINOKEY", 60, False)              '西濃キー(隠し項目)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.NRS_BR_NM, "営業所", 250, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CUST_NM_L, "荷主名（大）", 190, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CUST_NM_M, "荷主名（中）", 80, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CUST_CD_L, "荷主コード（大）", 80, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CUST_CD_M, "荷主コード（中）", 80, True)
        Public Shared ZIP_NO As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.ZIP_NO, "郵便番号", 60, True)
        Public Shared KEN_K As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.KEN_K, "都道府県名", 60, True)
        Public Shared CITY_K As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CITY_K, "市区町村名", 100, True)
        Public Shared SHIWAKE_CD As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SHIWAKE_CD, "仕分コード", 60, True)
        Public Shared CHAKU_CD As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CHAKU_CD, "着点番号", 60, True)
        Public Shared CHAKU_NM As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.CHAKU_NM, "着点名称", 100, True)

        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM400C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)


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
            .sprDetail.ActiveSheet.ColumnCount = 21


            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.ZIP_NO.ColNo + 1
            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.NRS_BR_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SEINO_KEY.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.ZIP_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.KEN_K.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_ZENKAKU, 10, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CITY_K.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_ZENKAKU, 40, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SHIWAKE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CHAKU_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 4, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.CHAKU_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_ZENKAKU, 40, False))
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM400G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()

        With Me._Frm.sprDetail

            .SetCellValue(0, LMM400G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM400G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM400G.sprDetailDef.SEINO_KEY.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CUST_NM_L.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CUST_NM_M.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CUST_CD_L.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CUST_CD_M.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.ZIP_NO.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.KEN_K.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CITY_K.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.SHIWAKE_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CHAKU_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.CHAKU_NM.ColNo, String.Empty)
            
            .SetCellValue(0, LMM400G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM400G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)


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
            Dim sMxCnt As StyleInfo = Me.StyleInfoNum3(spr, lock)

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM400G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SEINO_KEY.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.ZIP_NO.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.KEN_K.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CITY_K.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SHIWAKE_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CHAKU_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.CHAKU_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM400G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)



                'セルに値を設定
                .SetCellValue(i, LMM400G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SEINO_KEY.ColNo, dr.Item("SEINO_KEY").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CUST_NM_L.ColNo, dr.Item("CUST_NM_L").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CUST_NM_M.ColNo, dr.Item("CUST_NM_M").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CUST_CD_L.ColNo, dr.Item("CUST_CD_L").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CUST_CD_M.ColNo, dr.Item("CUST_CD_M").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.ZIP_NO.ColNo, dr.Item("ZIP_NO").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.KEN_K.ColNo, dr.Item("KEN_K").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CITY_K.ColNo, dr.Item("CITY_K").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SHIWAKE_CD.ColNo, dr.Item("SHIWAKE_CD").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CHAKU_CD.ColNo, dr.Item("CHAKU_CD").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.CHAKU_NM.ColNo, dr.Item("CHAKU_NM").ToString())
                
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM400G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())


            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999, lock, 0, , ",")

    End Function

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.txtCustCdL, lock)
            Me.SetLockControl(.txtCustCdM, lock)
            Me.SetLockControl(.txtZipNo, lock)
            Me.SetLockControl(.txtKenK, lock)
            Me.SetLockControl(.txtCityK, lock)
            Me.SetLockControl(.txtShiwakeCd, lock)
            Me.SetLockControl(.txtChakuCd, lock)
            Me.SetLockControl(.txtChakuNm, lock)
            

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
