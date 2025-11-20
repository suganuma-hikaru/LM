' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM
'  プログラムID     :  LMM210G : 乗務員マスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMM210Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM210G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM210F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM210F)

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
            .F10ButtonEnabled = lock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
            .F5ButtonEnabled = lock
            .F11ButtonEnabled = edit

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

        End With


    End Sub

#End Region 'FunctionKey


    ''' <summary>
    ''' ステータス設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(Optional ByVal dispMd As String = DispMode.VIEW, _
                                Optional ByVal recSts As String = RecordStatus.NOMAL_REC)

        Me._Frm.lblSituation.DispMode = dispMd
        Me._Frm.lblSituation.RecordStatus = recSts



    End Sub

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .sprDetail.TabIndex = LMM210C.CtlTabIndex.DETAIL
            .txtCrewCd.TabIndex = LMM210C.CtlTabIndex.YUSOBRCD
            .txtCrewNm.TabIndex = LMM210C.CtlTabIndex.YUSOBRNM
            .cmbWorkPossible.TabIndex = LMM210C.CtlTabIndex.AVALYN
            .cmbLarge.TabIndex = LMM210C.CtlTabIndex.LCARLICENSEYNNM
            .cmbTraction.TabIndex = LMM210C.CtlTabIndex.TRAILERLICENSEYNNM
            .cmbOtu1.TabIndex = LMM210C.CtlTabIndex.OTSU1YNNM
            .cmbOtu2.TabIndex = LMM210C.CtlTabIndex.OTSU2YNNM
            .cmbOtu3.TabIndex = LMM210C.CtlTabIndex.OTSU3YNNM
            .cmbOtu4.TabIndex = LMM210C.CtlTabIndex.OTSU4YNNM
            .cmbOtu5.TabIndex = LMM210C.CtlTabIndex.OTSU5YNNM
            .cmbOtu6.TabIndex = LMM210C.CtlTabIndex.OTSU6YNNM
            .cmbMoveKeepWatch.TabIndex = LMM210C.CtlTabIndex.HICOMPGASYNNM
            .lblSituation.TabIndex = LMM210C.CtlTabIndex.SYSENTDATE
            .lblCrtDate.TabIndex = LMM210C.CtlTabIndex.SYSENTUSERNM
            .lblCrtUser.TabIndex = LMM210C.CtlTabIndex.SYSUPDDATE
            .lblUpdUser.TabIndex = LMM210C.CtlTabIndex.SYSUPDDATEUSERNM
            .lblUpdDate.TabIndex = LMM210C.CtlTabIndex.SYSUPDDATE
            .lblUpdTime.TabIndex = LMM210C.CtlTabIndex.SYSUPDTIME
            .lblSysDelFlg.TabIndex = LMM210C.CtlTabIndex.SYSDELFLG
          

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal id As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

    End Sub
    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM210C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' コンボボックスの初期設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetcmbNrsBrCd()

        Me._Frm.cmbNrsBrCd.SelectedValue = String.Empty
        Me._Frm.cmbWorkPossible.SelectedValue = String.Empty
        Me._Frm.cmbLarge.SelectedValue = String.Empty
        Me._Frm.cmbTraction.SelectedValue = String.Empty
        Me._Frm.cmbOtu1.SelectedValue = String.Empty
        Me._Frm.cmbOtu2.SelectedValue = String.Empty
        Me._Frm.cmbOtu3.SelectedValue = String.Empty
        Me._Frm.cmbOtu4.SelectedValue = String.Empty
        Me._Frm.cmbOtu5.SelectedValue = String.Empty
        Me._Frm.cmbOtu6.SelectedValue = String.Empty
        Me._Frm.cmbMoveKeepWatch.SelectedValue = String.Empty
      
    End Sub

    ''' <summary>
    '''コンボボックスの設定(新規ボタン押下時) 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetCmbBox()

        Me._Frm.cmbNrsBrCd.SelectedValue = LMUserInfoManager.GetNrsBrCd()
        Me._Frm.cmbWorkPossible.SelectedValue = LMM210C.combo
        Me._Frm.cmbLarge.SelectedValue = LMM210C.combo
        Me._Frm.cmbTraction.SelectedValue = LMM210C.combo
        Me._Frm.cmbOtu1.SelectedValue = LMM210C.combo
        Me._Frm.cmbOtu2.SelectedValue = LMM210C.combo
        Me._Frm.cmbOtu3.SelectedValue = LMM210C.combo
        Me._Frm.cmbOtu4.SelectedValue = LMM210C.combo
        Me._Frm.cmbOtu5.SelectedValue = LMM210C.combo
        Me._Frm.cmbOtu6.SelectedValue = LMM210C.combo
        Me._Frm.cmbMoveKeepWatch.SelectedValue = LMM210C.combo

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
                            Me.SetLockControl(.txtCrewCd, True)

                            '新規
                        Case RecordStatus.NEW_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)

                            '複写
                        Case RecordStatus.COPY_REC
                            Me.LockControl(False)
                            Me.SetLockControl(.cmbNrsBrCd, True)
                            Me._Frm.txtCrewCd.TextValue = String.Empty
                            Me._Frm.txtCrewNm.TextValue = String.Empty

                    End Select

                Case DispMode.INIT
                    Me.ClearControl()
                    Me.LockControl(True)

            End Select

        End With
    End Sub


    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM210C.EventShubetsu)
        
        With Me._Frm
            Select Case eventType
                Case LMM210C.EventShubetsu.MAIN, LMM210C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM210C.EventShubetsu.SHINKI, LMM210C.EventShubetsu.HUKUSHA
                    .txtCrewCd.Focus()
                Case LMM210C.EventShubetsu.HENSHU
                    .txtCrewNm.Focus()

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
            .txtCrewCd.TextValue = String.Empty
            .txtCrewNm.TextValue = String.Empty
            .cmbWorkPossible.SelectedValue = String.Empty
            .cmbLarge.SelectedValue = String.Empty
            .cmbTraction.SelectedValue = String.Empty
            .cmbOtu1.SelectedValue = String.Empty
            .cmbOtu2.SelectedValue = String.Empty
            .cmbOtu3.SelectedValue = String.Empty
            .cmbOtu4.SelectedValue = String.Empty
            .cmbOtu5.SelectedValue = String.Empty
            .cmbOtu6.SelectedValue = String.Empty
            .cmbMoveKeepWatch.SelectedValue = String.Empty
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
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)
        
        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.YUSO_BR_CD.ColNo).Text
            .txtCrewCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.DRIVER_CD.ColNo).Text
            .txtCrewNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.DRIVER_NM.ColNo).Text
            .cmbWorkPossible.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.AVAL_YN.ColNo).Text
            .cmbLarge.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.LCAR_LICENSE_YN.ColNo).Text
            .cmbTraction.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.TRAILER_LICENSE_YN.ColNo).Text
            .cmbOtu1.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.OTSU1_YN.ColNo).Text
            .cmbOtu2.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.OTSU2_YN.ColNo).Text
            .cmbOtu3.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.OTSU3_YN.ColNo).Text
            .cmbOtu4.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.OTSU4_YN.ColNo).Text
            .cmbOtu5.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.OTSU5_YN.ColNo).Text
            .cmbOtu6.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.OTSU6_YN.ColNo).Text
            .cmbMoveKeepWatch.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.HICOMPGAS_YN.ColNo).Text


            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.SYS_ENT_DATA.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.SYS_UPD_DATA.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM210G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataSet)

        '参考値の設定
        Call Me.SetSpread(ds.Tables(LMM210C.TABLE_NM_OUT))

    End Sub

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared YUSO_BR_CD As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.YUSO_BR_CD, "営業所コード", 50, False)
        Public Shared YUSO_BR_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.YUSO_BR_NM, "営業所", 275, True)
        Public Shared DRIVER_CD As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.DRIVER_CD, "乗務員" & vbCrLf & "コード", 77, True)
        Public Shared DRIVER_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.DRIVER_NM, "乗務員氏名", 150, True)
        Public Shared AVAL_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.AVAL_YN, "使用可能フラグ", 30, False)
        Public Shared LCAR_LICENSE_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.LCAR_LICENSE_YN, "大型免許の有無", 30, False)
        Public Shared LCAR_LICENSE_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.LCAR_LICENSE_YN_NM, "大型免許", 80, True)
        Public Shared TRAILER_LICENSE_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.TRAILER_LICENSE_YN, "けん引免許の有無", 60, False)
        Public Shared TRAILER_LICENSE_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.TRAILER_LICENSE_YN_NM, "けん引", 80, True)
        Public Shared OTSU1_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU1_YN, "危険物乙1類の有無", 50, False)
        Public Shared OTSU1_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU1_YN_NM, "乙種1類", 60, True)
        Public Shared OTSU2_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU2_YN, "危険物乙2類の有無", 50, False)
        Public Shared OTSU2_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU2_YN_NM, "乙種2類", 60, True)
        Public Shared OTSU3_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU3_YN, "危険物乙3類の有無", 50, False)
        Public Shared OTSU3_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU3_YN_NM, "乙種3類", 60, True)
        Public Shared OTSU4_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU4_YN, "危険物乙4類の有無", 80, False)
        Public Shared OTSU4_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU4_YN_NM, "乙種4類", 60, True)
        Public Shared OTSU5_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU5_YN, "危険物乙5類の有無", 45, False)
        Public Shared OTSU5_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU5_YN_NM, "乙種5類", 60, True)
        Public Shared OTSU6_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU6_YN, "危険物乙6類の有無", 45, False)
        Public Shared OTSU6_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.OTSU6_YN_NM, "乙種6類", 60, True)
        Public Shared HICOMPGAS_YN As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.HICOMPGAS_YN, "高圧ガス移動監視者の有無", 45, False)
        Public Shared HICOMPGAS_YN_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.HICOMPGAS_YN_NM, "移動" & vbCrLf & "監視者", 80, True)
        Public Shared SYS_ENT_DATA As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_ENT_DATE, "作成日", 180, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 50, False)
        Public Shared SYS_UPD_DATA As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_UPD_DATE, "更新日", 50, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 50, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 50, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM210C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 50, False)

    End Class

    ''' <summary>
    ''' セルのプロパティを設定(CUSTCOND)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoCustCond(ByVal spr As LMSpread) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , "KBN_CD" _
                                                  , "KBN_NM2" _
                                                  , False _
                                                  , New String() {"KBN_GROUP_CD"} _
                                                  , New String() {LMKbnConst.KBN_U009} _
                                                  )

    End Function

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
            .sprDetail.ActiveSheet.ColumnCount = 31

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New LMM210G.sprDetailDef)
            .sprDetail.SetColProperty(New LMM210G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM210G.sprDetailDef.DRIVER_NM.ColNo + 1

            Dim umuStyle As StyleInfo = Me.StyleInfoCustCond(.sprDetail)

            '列設定
            'TODO:区分コンスト待ち(S051)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.YUSO_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.YUSO_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.DRIVER_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.DRIVER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.LCAR_LICENSE_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.TRAILER_LICENSE_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU1_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU2_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU3_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU4_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU5_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU6_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.HICOMPGAS_YN_NM.ColNo, umuStyle)
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.AVAL_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.LCAR_LICENSE_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.TRAILER_LICENSE_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU1_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU2_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU3_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU4_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU5_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.OTSU6_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.HICOMPGAS_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_ENT_DATA.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_UPD_DATA.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM210G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM210F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.YUSO_BR_NM.ColNo).Value = LMUserInfoManager.GetNrsBrCd.ToString()
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.DRIVER_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.LCAR_LICENSE_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.TRAILER_LICENSE_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU1_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU2_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU3_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU4_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU5_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU6_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.HICOMPGAS_YN_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.AVAL_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.LCAR_LICENSE_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.TRAILER_LICENSE_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU1_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU2_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU3_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU4_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU5_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.OTSU6_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.HICOMPGAS_YN.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.SYS_UPD_DATA.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.SYS_UPD_TIME.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM210G.sprDetailDef.SYS_DEL_FLG.ColNo).Value = String.Empty


        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

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
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)


            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, LMM210G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.YUSO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.YUSO_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.AVAL_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.DRIVER_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.DRIVER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.LCAR_LICENSE_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.LCAR_LICENSE_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.TRAILER_LICENSE_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.TRAILER_LICENSE_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU1_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU1_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU2_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU2_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU3_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU3_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU4_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU4_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU5_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU5_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU6_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.OTSU6_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.HICOMPGAS_YN.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.HICOMPGAS_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_ENT_DATA.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_UPD_DATA.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM210G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)



                'セルに値を設定
                .SetCellValue(i, LMM210G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM210G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.YUSO_BR_NM.ColNo, dr.Item("YUSO_BR_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.AVAL_YN.ColNo, dr.Item("AVAL_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.DRIVER_CD.ColNo, dr.Item("DRIVER_CD").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.DRIVER_NM.ColNo, dr.Item("DRIVER_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.LCAR_LICENSE_YN_NM.ColNo, dr.Item("LCAR_LICENSE_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.TRAILER_LICENSE_YN_NM.ColNo, dr.Item("TRAILER_LICENSE_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU1_YN_NM.ColNo, dr.Item("OTSU1_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU2_YN_NM.ColNo, dr.Item("OTSU2_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU3_YN_NM.ColNo, dr.Item("OTSU3_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU4_YN_NM.ColNo, dr.Item("OTSU4_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU5_YN_NM.ColNo, dr.Item("OTSU5_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU6_YN_NM.ColNo, dr.Item("OTSU6_YN_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.HICOMPGAS_YN_NM.ColNo, dr.Item("HICOMPGAS_YN_NM").ToString())

                .SetCellValue(i, LMM210G.sprDetailDef.YUSO_BR_CD.ColNo, dr.Item("YUSO_BR_CD").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.LCAR_LICENSE_YN.ColNo, dr.Item("LCAR_LICENSE_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.TRAILER_LICENSE_YN.ColNo, dr.Item("TRAILER_LICENSE_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU1_YN.ColNo, dr.Item("OTSU1_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU2_YN.ColNo, dr.Item("OTSU2_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU3_YN.ColNo, dr.Item("OTSU3_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU4_YN.ColNo, dr.Item("OTSU4_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU5_YN.ColNo, dr.Item("OTSU5_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.OTSU6_YN.ColNo, dr.Item("OTSU6_YN").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.HICOMPGAS_YN.ColNo, dr.Item("HICOMPGAS_YN").ToString())

                .SetCellValue(i, LMM210G.sprDetailDef.SYS_ENT_DATA.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.SYS_UPD_DATA.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM210G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With


    End Sub

#End Region 'Spread

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm
            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.txtCrewCd, lock)
            Me.SetLockControl(.txtCrewNm, lock)
            Me.SetLockControl(.cmbWorkPossible, lock)
            Me.SetLockControl(.cmbLarge, lock)
            Me.SetLockControl(.cmbTraction, lock)
            Me.SetLockControl(.cmbOtu1, lock)
            Me.SetLockControl(.cmbOtu2, lock)
            Me.SetLockControl(.cmbOtu3, lock)
            Me.SetLockControl(.cmbOtu4, lock)
            Me.SetLockControl(.cmbOtu5, lock)
            Me.SetLockControl(.cmbOtu6, lock)
            Me.SetLockControl(.cmbMoveKeepWatch, lock)
          
        End With
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
