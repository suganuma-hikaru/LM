' ==========================================================================
'  システム名     : LM　　　: 倉庫システム
'  サブシステム名 : LMS     : マスタ
'  プログラムID   : LMM320G : 請求項目マスタメンテナンス
'  作  成  者     : 金ヘスル
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMM320Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM320G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM320F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM320F)

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
            .F10ButtonEnabled = lock
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

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbGroupCd.TabIndex = LMM320C.CtlTabindex.GROUP_KB
            .txtSeiqkmkCd.TabIndex = LMM320C.CtlTabindex.SEIQKMK_CD
            .txtSeiqkmkCdS.TabIndex = LMM320C.CtlTabindex.SEIQKMK_CD_S
            .txtSeiqknkNm.TabIndex = LMM320C.CtlTabindex.SEIQKMK_NM
            .cmbTaxKb.TabIndex = LMM320C.CtlTabindex.TAX_KB
            .cmbKeiriCd.TabIndex = LMM320C.CtlTabindex.KEIRI_KB
            .txtComment.TabIndex = LMM320C.CtlTabindex.REMARK
            .numInJyun.TabIndex = LMM320C.CtlTabindex.PRINT_SORT

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
    ''' コントロールの入力制御
    ''' </summary>
    ''' モードによるロック制御を行う。省略時：初期モード
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            Select Case .lblSituation.DispMode

                '初期設定時
                Case DispMode.VIEW

                    'コントロールをクリアする
                    Me.ClearControl()

                    'コントロールをロックする
                    Me.LockedControls(True)

                Case DispMode.EDIT
                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC

                            'コントロールのロックを解除する
                            Me.LockedControls(False)
                            Call Me.SetLockControl(.cmbGroupCd, True)
                            Call Me.SetLockControl(.txtSeiqkmkCd, True)
                            Call Me.SetLockControl(.txtSeiqkmkCdS, True)


                            '新規
                        Case RecordStatus.NEW_REC

                            'コントロールをクリアする
                            Me.ClearControl()
                            'コントロールのロックを解除する
                            Me.LockedControls(False)

                            '複写
                        Case RecordStatus.COPY_REC
                            'コントロールのロックを解除する
                            Me.LockedControls(False)
                            Call Me.ClearControlFukusha()


                    End Select

                Case DispMode.INIT
                    Me.ClearControl()

                    'コントロールをロックする
                    Me.LockedControls(True)
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 複写時キー項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControlFukusha()

        With Me._Frm

            .txtSeiqkmkCd.TextValue = String.Empty
            .txtSeiqkmkCdS.TextValue = String.Empty
            .txtComment.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblCrtDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM320C.EventShubetsu, ByVal recstatus As Object)

        Call Me.SetFunctionKey()
        Call Me.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal eventType As LMM320C.EventShubetsu)
        With Me._Frm
            Select Case eventType
                Case LMM320C.EventShubetsu.MAIN, LMM320C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM320C.EventShubetsu.SHINKI, LMM320C.EventShubetsu.HUKUSHA
                    .cmbGroupCd.Focus()
                Case LMM320C.EventShubetsu.HENSHU
                    .txtSeiqknkNm.Focus()

            End Select

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm
            .cmbGroupCd.SelectedValue = String.Empty
            .txtSeiqkmkCd.TextValue = String.Empty
            .txtSeiqkmkCdS.TextValue = String.Empty
            .txtSeiqknkNm.TextValue = String.Empty
            .cmbTaxKb.SelectedValue = String.Empty
            .cmbKeiriCd.SelectedValue = String.Empty
            .txtComment.TextValue = String.Empty
            .numInJyun.Value = 0
            .lblCrtDate.TextValue = String.Empty
            .lblCrtUser.TextValue = String.Empty
            .lblUpdDate.TextValue = String.Empty
            .lblUpdUser.TextValue = String.Empty
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
            .cmbGroupCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.GROUP_KB.ColNo).Text
            .txtSeiqkmkCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo).Text
            .txtSeiqkmkCdS.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SEIQKMK_CD_S.ColNo).Text
            .txtSeiqknkNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo).Text
            .cmbTaxKb.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.TAX_KB.ColNo).Text
            .cmbKeiriCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.KEIRI_KB.ColNo).Text
            .txtComment.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.REMARK.ColNo).Text
            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SYS_UPD_USER_TIME.ColNo).Text
            .numInJyun.Value = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.PRINT_SORT.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM320G.sprDetailDef.SYS_DEL_FLG.ColNo).Text
        End With

    End Sub

#End Region

#Region "検索結果表示"

    ''' <summary>
    ''' 検索結果表示
    ''' </summary>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Public Sub SetSelectListData(ByVal ds As DataTable)

        '参考値の設定
        Call Me.SetSpread(ds)

    End Sub

    ''' <summary>
    ''' 検索結果ヘッダー部表示
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHeaderData(ByVal strKNAPTA As String, ByVal strKBN As String)

        With Me._Frm

            'Select Case strKBN
            '    Case "1"
            '        .txtType.TextValue = Get_Shubetu(Mid(strKNAPTA, 2, 2))      
            '        .txtHurikomi.TextValue = Trim(Mid(strKNAPTA, 2, 10))        
            '        .txtHurikomiNm.TextValue = Trim(Mid(strKNAPTA, 15, 40))     
            '        .txtTorikumi.TextValue = Trim(Mid(strKNAPTA, 55, 4))        
            '        .txtBankno.TextValue = Trim(Mid(strKNAPTA, 59, 4))          
            '        .txtBankNm.TextValue = Trim(Mid(strKNAPTA, 63, 15))         
            '        .txtShitenno.TextValue = Trim(Mid(strKNAPTA, 78, 3))        
            '        .txtShitenNm.TextValue = Trim(Mid(strKNAPTA, 81, 15))       
            '        .txtYokinsyu.TextValue = Get_Yokin(Mid(strKNAPTA, 96, 1))   
            '        .txtKozabango.TextValue = Trim(Mid(strKNAPTA, 97, 7))       
            '    Case "8"
            '        .txtTotalcnt.Value = CInt(Trim(Mid(strKNAPTA, 2, 6)))       
            '        .txtTotalKin.Value = CDec(Trim(Mid(strKNAPTA, 8, 12)))      
            'End Select

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.DEF, " ", 20, True)

        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared PRINT_SORT As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.PRINT_SORT, "印順", 60, True)
        Public Shared GROUP_KB As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.GROUP_KB, "請求グループコード", 60, False)                     '隠し項目
        Public Shared GROUP_KB_NM As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.GROUP_KB_NM, "請求グループ" & vbCrLf & "コード", 100, True)
        Public Shared SEIQKMK_CD As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SEIQKMK_CD, "請求項目" & vbCrLf & "コード", 100, True)
        Public Shared SEIQKMK_CD_S As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SEIQKMK_CD_S, "請求項目" & vbCrLf & "CD小", 60, True)
        Public Shared SEIQKMK_NM As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SEIQKMK_NM, "請求項目名", 180, True)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.TAX_KB, "課税区分", 60, False)                                      '隠し項目
        Public Shared TAX As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.TAX_KB_NM, "課税区分", 100, True)
        Public Shared KEIRI_KB As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.KEIRI_KB, "経理科目コード", 60, False)                           '隠し項目
        Public Shared KEIRI As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.KEIRI_KB_NM, "経理科目" & vbCrLf & "コード", 150, True)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.REMARK, "備考", 300, True)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_ENT_DATE, " 作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_USER_TIME As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM320C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread()

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.Sheets(0).ColumnCount = 19

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New LMM320G.sprDetailDef())
            Dim rowCount As Integer = 0

            '列固定位置を設定します。
            .sprDetail.ActiveSheet.FrozenColumnCount = LMM320G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.PRINT_SORT.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.GROUP_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.GROUP_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S024", False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SEIQKMK_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.TAX_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.TAX.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "Z001", False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.KEIRI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.KEIRI.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "K016", False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_ENT_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_ENT_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_UPD_DATE.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_UPD_USER_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_UPD_USER_TIME.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(rowCount, LMM320G.sprDetailDef.SYS_DEL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM320F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_DEL_NM.ColNo).Value = LMConst.FLG.OFF
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.PRINT_SORT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.GROUP_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.GROUP_KB_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SEIQKMK_CD_S.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.TAX_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.TAX.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.KEIRI_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.KEIRI.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.REMARK.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_UPD_USER_TIME.ColNo).Value = String.Empty
        End With

    End Sub
    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetClearValue(ByVal frm As LMM320F)

        With frm.sprDetail

            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.PRINT_SORT.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.GROUP_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.GROUP_KB_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SEIQKMK_CD_S.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.TAX_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.TAX.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.KEIRI_KB.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.KEIRI.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.REMARK.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_ENT_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_UPD_DATE.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Value = String.Empty
            .ActiveSheet.Cells(0, LMM320G.sprDetailDef.SYS_UPD_USER_TIME.ColNo).Value = String.Empty
        End With

    End Sub

 

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As New DataSet

        With spr

            .SuspendLayout()
            'データ挿入
            '行数設定
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            .Sheets(0).AddRows(.Sheets(0).Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim sCombo As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "S024", False, LMSpreadUtility.DISP_MEMBERS.KBN_NM1)
            Dim kCombo As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "K016", False, LMSpreadUtility.DISP_MEMBERS.KBN_NM1)
            Dim zCombo As StyleInfo = LMSpreadUtility.GetComboCellKbn(spr, "Z001", False, LMSpreadUtility.DISP_MEMBERS.KBN_NM1)
            'Dim nLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            Dim dr As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)


                ''セルスタイル設定
                .SetCellStyle(i, LMM320G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.PRINT_SORT.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.GROUP_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.GROUP_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SEIQKMK_CD_S.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.TAX_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.TAX.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.KEIRI_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.KEIRI.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_UPD_USER_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM320G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)

                ''セルに値を設定
                .SetCellValue(i, LMM320G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.PRINT_SORT.ColNo, dr.Item("PRINT_SORT").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.GROUP_KB.ColNo, dr.Item("GROUP_KB").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.GROUP_KB_NM.ColNo, dr.Item("GROUP_KB_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SEIQKMK_CD.ColNo, dr.Item("SEIQKMK_CD").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SEIQKMK_CD_S.ColNo, dr.Item("SEIQKMK_CD_S").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SEIQKMK_NM.ColNo, dr.Item("SEIQKMK_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.TAX_KB.ColNo, dr.Item("TAX_KB").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.TAX.ColNo, dr.Item("TAX_KB_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.KEIRI_KB.ColNo, dr.Item("KEIRI_KB").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.KEIRI.ColNo, dr.Item("KEIRI_KB_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_UPD_USER_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM320G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread


#End Region

    ''' <summary>
    ''' 画面コントロールのロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub LockedControls(ByVal lock As Boolean)

        With _Frm
            Me.SetLockControl(.cmbGroupCd, lock)    '請求グループコード
            Me.SetLockControl(.txtSeiqkmkCd, lock)  '請求項目コード
            Me.SetLockControl(.txtSeiqkmkCdS, lock) '請求項目コード小分類
            Me.SetLockControl(.txtSeiqknkNm, lock)  '請求項目名
            Me.SetLockControl(.cmbTaxKb, lock)      '課税区分
            Me.SetLockControl(.cmbKeiriCd, lock)    '経理科目コード
            Me.SetLockControl(.txtComment, lock)    '備考
            Me.SetLockControl(.numInJyun, lock)     '印順
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
        Call Me.GetTarget(Of Win.InputMan.LMComboKubun)(arr, ctl)
        For Each arrCtl As Win.InputMan.LMComboKubun In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockGroupBox(arrCtl, lockFlg)
        Next

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

            '指定されたクラスかその継承クラスを設定
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockGroupBox(ByVal ctl As Win.InputMan.LMComboKubun, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

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
    '''新規押下時コンボボックスの設定 
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetcmbValue()

        With Me._Frm
            .cmbGroupCd.SelectedValue = LMM320C.COMBO_BOX_DEFAULT
            .cmbTaxKb.SelectedValue = LMM320C.COMBO_BOX_DEFAULT
            .cmbKeiriCd.SelectedValue = LMM320C.COMBO_BOX_DEFAULT
            .numInJyun.Value = 99
        End With
        
    End Sub

    ''' <summary>
    ''' 画面編集部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbGroupCd, lock)
            Me.SetLockControl(.txtSeiqkmkCd, lock)
            Me.SetLockControl(.txtSeiqkmkCdS, lock)
            Me.SetLockControl(.txtSeiqknkNm, lock)
            Me.SetLockControl(.cmbTaxKb, lock)
            Me.SetLockControl(.cmbKeiriCd, lock)
            Me.SetLockControl(.txtComment, lock)
            Me.SetLockControl(.numInJyun, lock)

        End With

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d2 As Decimal = Convert.ToDecimal("99")

            'numberCellの桁数を設定する
            .numInJyun.SetInputFields("#0", , 2, 1, , 0, 0, , d2, 0)
        End With

    End Sub

End Class
