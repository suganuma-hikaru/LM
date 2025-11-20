' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : ＳＣＭ
'  プログラムID     :  LMM180C : JISマスタメンテ
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
''' LMM180Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM180G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM180F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM180F, ByVal g As LMMControlG)

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
            .F3ButtonName = LMMControlC.FUNCTION_F3_FUKUSHA
            .F4ButtonName = LMMControlC.FUNCTION_F4_SAKUJO_HUKKATU
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = LMMControlC.FUNCTION_F9_KENSAKU
            '要望管理2166 SHINODA START
            '.F10ButtonName = String.Empty
            .F10ButtonName = LMMControlC.FUNCTION_F10_KENSAKU
            '要望管理2166 SHINODA END
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
            '要望管理2166 SHINODA START
            '.F10ButtonEnabled = lock
            .F10ButtonEnabled = unLock
            '要望管理2166 SHINODA END
            .F9ButtonEnabled = unLock
            .F12ButtonEnabled = unLock
            '画面入力モードによるロック制御
            .F1ButtonEnabled = view OrElse init
            .F2ButtonEnabled = view
            .F3ButtonEnabled = view
            .F4ButtonEnabled = view
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
            .sprDetail.TabIndex = LMM180C.CtlTabIndex.DETAIL

            .txtJisCd.TabIndex = LMM180C.CtlTabIndex.JISCD
            .cmbKenNm.TabIndex = LMM180C.CtlTabIndex.KEN
            .txtCityN.TabIndex = LMM180C.CtlTabIndex.SHI

            .lblSituation.TabIndex = LMM180C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM180C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM180C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM180C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM180C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM180C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM180C.CtlTabIndex.SYSDELFLG

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal ds As DataSet)

        '編集部の項目をクリア
        Call Me.ClearControl(Me._Frm)

        'コンボの設定
        Call Me.SetValue(ds)

    End Sub

    ''' <summary>
    ''' 新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub SetValue(ByVal ds As DataSet)

        With Me._Frm.cmbKenNm

            'リストのクリア 
            .Items.Clear()

            Dim cd As String = String.Empty
         
            '空行の設定
            .Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(cd)}))

            'マスタ検索処理
            Dim dt As DataTable = ds.Tables(LMM180C.TABLE_NM_KEN)
            Dim max As Integer = dt.Rows.Count - 1
            For i As Integer = 0 To max

                cd = dt.Rows(i).Item("KEN").ToString()
                .Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(cd)}))

            Next

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
            Call Me._ControlG.SetLockControl(Me._Frm, Lock)

            Select Case Me._Frm.lblSituation.DispMode
                Case DispMode.INIT
                    Me.ClearControl(Me._Frm)
                    Call Me._ControlG.SetLockControl(Me._Frm, Lock)

                Case DispMode.VIEW
                    Me.ClearControl(Me._Frm)
                    Call Me._ControlG.SetLockControl(Me._Frm, Lock)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
                            Call Me._ControlG.LockText(.txtJisCd, lock)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._ControlG.SetLockControl(Me._Frm, unLock)
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
    Friend Sub SetFoucus(ByVal eventType As LMM180C.EventShubetsu)

        With Me._Frm
            Select Case eventType
                Case LMM180C.EventShubetsu.MAIN, LMM180C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM180C.EventShubetsu.SHINKI
                    .txtJisCd.Focus()
                Case LMM180C.EventShubetsu.HUKUSHA, LMM180C.EventShubetsu.HENSHU
                    .cmbKenNm.Focus()
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

            .txtJisCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.JIS_CD.ColNo).Text
            .cmbKenNm.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.KEN.ColNo).Text
            .txtCityN.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SHI.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM180G.sprDetailDef.SYS_DEL_FLG.ColNo).Text


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

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared KEN As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.KEN, "都道府県名", 150, True)
        Public Shared SHI As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SHI, "市区町村名", 200, True)
        Public Shared JIS_CD As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.JIS_CD, "JISコード", 80, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM180C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal ds As DataSet)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = 11

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM180G.sprDetailDef())

            '列固定位置を設定します。
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM180G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            Call Me.SetKenComb(ds)
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SHI.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.JIS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 7, False))

            '隠し項目
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SYS_UPD_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue()
        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr


            .SetCellValue(0, LMM180G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM180G.sprDetailDef.KEN.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SHI.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.JIS_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM180G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)



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
                .SetCellStyle(i, LMM180G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.KEN.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SHI.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM180G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM180G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.KEN.ColNo, dr.Item("KEN").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SHI.ColNo, dr.Item("SHI").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.JIS_CD.ColNo, dr.Item("JIS_CD").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM180G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())


            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド都道府県コンボボックス設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetKenComb(ByVal ds As DataSet)

        Me._Frm.sprDetail.SetCellStyle(0, LMM180G.sprDetailDef.KEN.ColNo, LMSpreadUtility.GetComboCell(Me._Frm.sprDetail, New DataView(ds.Tables("LMM180KEN")), "KEN", "KEN", False))

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
