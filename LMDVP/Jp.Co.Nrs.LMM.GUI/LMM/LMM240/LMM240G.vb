' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM240G : 帳票パターンマスタメンテ
'  作  成  者       :  平山
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
Imports GrapeCity.Win.Editors

''' <summary>
''' LMM240Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMM240G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM240F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM240F, ByVal g As LMMControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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
            .sprDetail.TabIndex = LMM240C.CtlTabIndex.DETAIL
            .cmbNrsBrCd.TabIndex = LMM240C.CtlTabIndex.NRSBRCD
            .cmbPtnId.TabIndex = LMM240C.CtlTabIndex.PTNID
            .chkStdFlg.TabIndex = LMM240C.CtlTabIndex.STANDARDFLAG
            .txtPtnCd.TabIndex = LMM240C.CtlTabIndex.PTNCD
            .txtPtnNm.TabIndex = LMM240C.CtlTabIndex.PTNNM
            .cmbRptNm.TabIndex = LMM240C.CtlTabIndex.RPTID
            .txtPtnCd2.TabIndex = LMM240C.CtlTabIndex.PTNCD2
            .numCopyNb1.TabIndex = LMM240C.CtlTabIndex.COPIESNB1
            .numCopyNb2.TabIndex = LMM240C.CtlTabIndex.COPIESNB2
            .cmbRptOut.TabIndex = LMM240C.CtlTabIndex.RPTOUTKB
            .cmbOutPut.TabIndex = LMM240C.CtlTabIndex.OUTPUTKB
            .cmbPrinterNm.TabIndex = LMM240C.CtlTabIndex.PRINTERNM
            .cmbJobId.TabIndex = LMM240C.CtlTabIndex.JOBID
            .cmbHstryFlg.TabIndex = LMM240C.CtlTabIndex.HISTORYFLAG
            .txtRemark.TabIndex = LMM240C.CtlTabIndex.REMARK
            .lblSituation.TabIndex = LMM240C.CtlTabIndex.SITUATION
            .lblCrtDate.TabIndex = LMM240C.CtlTabIndex.CRTDATE
            .lblCrtUser.TabIndex = LMM240C.CtlTabIndex.CRTUSER
            .lblUpdUser.TabIndex = LMM240C.CtlTabIndex.UPDUSER
            .lblUpdDate.TabIndex = LMM240C.CtlTabIndex.UPDDATE
            .lblUpdTime.TabIndex = LMM240C.CtlTabIndex.UPDTIME
            .lblSysDelFlg.TabIndex = LMM240C.CtlTabIndex.SYSDELFLG


        End With

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal eventType As LMM240C.EventShubetsu, ByVal recstatus As Object)

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
    ''' <remarks>ナンバー型・コンボボックスの設定など</remarks>
    Friend Sub SetControl()

        'コンボボックスの設定
        Call Me.CreateComboBox()

        '編集部の項目をクリア
        Call Me._LMMConG.ClearControl(Me._Frm)

        'numberCellの桁数を設定する
        Call Me.SetNumberControl()

        'number型初期値設定
        Call Me.SetDefaultNumber()

    End Sub

    ''' <summary>
    ''' ナンバー型の設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetNumberControl()

        With Me._Frm

            Dim d94 As Decimal = Convert.ToDecimal(9999)
            Dim d0 As Decimal = Convert.ToDecimal(0)

            'numberCellの桁数を設定する
            .numCopyNb1.SetInputFields("#,##0", , 4, 1, , 0, 0, , d94, d0)
            .numCopyNb2.SetInputFields("#,##0", , 4, 1, , 0, 0, , d94, d0)

        End With

    End Sub

    ''' <summary>
    ''' ナンバー型初期値設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDefaultNumber()

        With Me._Frm

            .numCopyNb1.Value = Nothing
            .numCopyNb2.Value = Nothing
            .numCopyNb1.Value = 0
            .numCopyNb2.Value = 0

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


            Select Case .lblSituation.DispMode

                Case DispMode.VIEW
                    Call Me.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, lock)

                Case DispMode.EDIT

                    Select Case .lblSituation.RecordStatus

                        '参照
                        Case RecordStatus.NOMAL_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._LMMConG.LockComb(.cmbPtnId, lock)
                            Call Me._LMMConG.LockText(.txtPtnCd, lock)

                            Call Me.ChangeLockControl(LMM240C.EventShubetsu.HENSHU)

                            '新規
                        Case RecordStatus.NEW_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)

                            Call Me._LMMConG.LockComb(.cmbPrinterNm, lock)

                            Call Me.SetcmbBox()

                            '複写
                        Case RecordStatus.COPY_REC
                            Call Me._LMMConG.SetLockControl(Me._Frm, unLock)
                            Call Me._LMMConG.LockComb(.cmbNrsBrCd, lock)
                            Call Me._LMMConG.LockComb(.cmbPtnId, lock)
                            'Call Me._LMMConG.LockCheckBox(.chkStdFlg, lock)  '検証結果(メモ)№74対応(2011.09.08)

                            Call Me.ChangeLockControl(LMM240C.EventShubetsu.HENSHU)

                            Call Me.ClearControlFukusha()

                    End Select

                Case DispMode.INIT
                    Call Me.ClearControl(Me._Frm)
                    Call Me._LMMConG.SetLockControl(Me._Frm, lock)

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
            .txtPtnNm.TextValue = String.Empty
            .txtRemark.TextValue = String.Empty
            .chkStdFlg.SetBinaryValue(LMConst.FLG.OFF) '検証結果(メモ)№74対応(2011.09.08)

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
    Friend Sub SetFoucus(ByVal eventType As LMM240C.EventShubetsu)

        With Me._Frm

            Select Case eventType
                Case LMM240C.EventShubetsu.MAIN, LMM240C.EventShubetsu.KENSAKU
                    .sprDetail.Focus()
                Case LMM240C.EventShubetsu.SHINKI
                    .cmbPtnId.Focus()
                Case LMM240C.EventShubetsu.HUKUSHA
                    .txtPtnCd.Focus()
                Case LMM240C.EventShubetsu.HENSHU
                    .chkStdFlg.Focus()
            End Select

        End With

    End Sub

    ''' <summary>
    ''' 項目のクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control, Optional ByVal flg As Boolean = True)

        Call Me._LMMConG.ClearControl(ctl)

        If flg = True Then

            'ナンバー型初期値設定
            Call Me.SetDefaultNumber()

        End If

    End Sub

    ''' <summary>
    ''' 画面の値に応じてのロック制御
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Friend Sub ChangeLockControl(ByVal actionType As LMM240C.EventShubetsu)

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(Me._Frm.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim lock As Boolean = True
            Dim unLock As Boolean = False
            Dim rptOut As String = .cmbRptOut.SelectedValue.ToString()

            '帳票出力先区分＝"03(荷札)"の場合、プリンタ名コンボボックスのロック解除
            If LMM240C.NIHUDA.Equals(rptOut) = True Then
                Call Me._LMMConG.SetLockControl(.cmbPrinterNm, unLock)
            Else
                Call Me.ClearControl(.cmbPrinterNm, False)
                Call Me._LMMConG.SetLockControl(.cmbPrinterNm, lock)

            End If

        End With

    End Sub

    ''' <summary>
    ''' レポートファイル名の値によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub RptChangeLockControl()

        With Me._Frm

            '参照モードの場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim lockFlg As Boolean = False
            If String.IsNullOrEmpty(.cmbRptNm.SelectedValue.ToString()) = True Then

                '値クリア処理
                .numCopyNb1.Value = 0
                Call Me._LMMConG.ClearControl(.txtPtnCd2)
                Call Me._LMMConG.ClearControl(.lblPtnNm)
                .numCopyNb2.Value = 0
                Call Me._LMMConG.ClearControl(.cmbRptOut)
                Call Me._LMMConG.ClearControl(.cmbOutPut)
                Call Me._LMMConG.ClearControl(.cmbPrinterNm)
                Call Me._LMMConG.ClearControl(.cmbJobId)
                Call Me._LMMConG.ClearControl(.cmbHstryFlg)

                'ロックフラグの設定
                lockFlg = True

            End If

            'ロック制御
            Call Me._LMMConG.SetLockControl(.numCopyNb1, lockFlg)
            Call Me._LMMConG.SetLockControl(.txtPtnCd2, lockFlg)
            Call Me._LMMConG.SetLockControl(.numCopyNb2, lockFlg)
            Call Me._LMMConG.SetLockControl(.cmbRptOut, lockFlg)
            Call Me._LMMConG.SetLockControl(.cmbOutPut, lockFlg)
            Call Me._LMMConG.SetLockControl(.cmbPrinterNm, lockFlg)
            Call Me._LMMConG.SetLockControl(.cmbJobId, lockFlg)
            Call Me._LMMConG.SetLockControl(.cmbHstryFlg, lockFlg)

        End With

    End Sub

    '検証結果(メモ)№74対応 (2011.09.08)-------
    ''' <summary>
    ''' 帳票種類IDの値によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub PtnIdChangeControl(Optional ByVal KbnCd As String = "")

        With Me._Frm

            .cmbRptNm.Items.Clear()

            Dim SearchFlg As Boolean = False
            Dim sort As String = "KBN_CD"
            Dim KbnNm As String = String.Empty

            If String.IsNullOrEmpty(KbnCd) = False Then

                SearchFlg = True

                '帳票種類ID（区分マスタ）            
                Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.PtnIdKbnString(SearchFlg, KbnCd), sort)
                Dim max As Integer = getDr.Count - 1

                For i As Integer = 0 To max
                    KbnNm = getDr(i).Item("KBN_NM3").ToString()
                Next

            End If

            ' レポートファイル名（区分マスタ）
            Dim cd2 As String = String.Empty
            Dim item2 As String = String.Empty
            Dim getDr2 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.RrdIdKbnString(SearchFlg, KbnNm), sort)

            Dim max2 As Integer = getDr2.Count - 1
            .cmbRptNm.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max2
                cd2 = getDr2(i).Item("KBN_CD").ToString()
                item2 = getDr2(i).Item("KBN_NM1").ToString()
                .cmbRptNm.Items.Add(New ListItem(New SubItem() {New SubItem(item2), New SubItem(cd2)}))
            Next

        End With

    End Sub

    ''' <summary>
    ''' 帳票種類ID
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CreateComboBox()

        With Me._Frm

            Dim sort As String = "KBN_CD"

            '帳票種類ID（区分マスタ）            
            Dim cd As String = String.Empty
            Dim item As String = String.Empty
            Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.PtnIdKbnString, sort)

            Dim max As Integer = getDr.Count - 1
            .cmbPtnId.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))

            For i As Integer = 0 To max
                cd = getDr(i).Item("KBN_CD").ToString()
                item = getDr(i).Item("KBN_NM1").ToString()
                .cmbPtnId.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
            Next

            '検証結果(メモ)№74対応(2011.09.08)
            'レポートファイル名（区分マスタ）            
            Call Me.PtnIdChangeControl()

        End With

    End Sub

    ''' <summary>
    ''' 帳票種類ID取得SELECT文(キャッシュ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function PtnIdKbnString(Optional ByVal SearchFlg As Boolean = False, Optional ByVal KbnCd As String = "") As String

        PtnIdKbnString = String.Empty

        PtnIdKbnString = String.Concat(PtnIdKbnString, "SYS_DEL_FLG = '0'")

        PtnIdKbnString = String.Concat(PtnIdKbnString, " AND ", "KBN_GROUP_CD = ", " '", LMM240C.PTNIDKBNT007, "' ")

        PtnIdKbnString = String.Concat(PtnIdKbnString, " AND ", "VALUE1 = '1.000'")

        '検証結果(メモ)№74対応(2011.09.08)
        If SearchFlg = True Then
            PtnIdKbnString = String.Concat(PtnIdKbnString, " AND ", "KBN_CD = ", " '", KbnCd, "' ")
        End If

        Return PtnIdKbnString

    End Function

    '検証結果(メモ)№74対応 (2011.09.08)START-------
    ''' <summary>
    ''' レポートファイル名取得SELECT文(キャッシュ)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function RrdIdKbnString(Optional ByVal SearchFlg As Boolean = False, Optional ByVal KbnNm As String = "") As String

        RrdIdKbnString = String.Empty

        RrdIdKbnString = String.Concat(RrdIdKbnString, "SYS_DEL_FLG = '0'")

        RrdIdKbnString = String.Concat(RrdIdKbnString, " AND ", "KBN_GROUP_CD = ", " '", LMM240C.PTNIDKBNR010, "' ")

        If SearchFlg = True Then
            RrdIdKbnString = String.Concat(RrdIdKbnString, " AND ", "KBN_NM3 = ", " '", KbnNm, "' ")
        End If

        Return RrdIdKbnString

    End Function
    '検証結果(メモ)№74対応 (2011.09.08)END-------

    ''' <summary>
    ''' SPREADデータをコントロールに設定
    ''' </summary>
    ''' <param name="row"></param>
    ''' <remarks></remarks>
    Friend Sub SetControlSpreadData(ByVal row As Integer)

        Dim stdFlg As String = String.Empty

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.NRS_BR_CD.ColNo).Text
            .cmbPtnId.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.PTN_ID.ColNo).Text

            stdFlg = Me.StdFlgStr(.sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.STANDARD_FLAG.ColNo).Text)
            .chkStdFlg.SetBinaryValue(stdFlg)

            .txtPtnCd.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.PTN_CD.ColNo).Text
            .txtPtnNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.PTN_NM.ColNo).Text
            .cmbRptNm.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.RPT_ID.ColNo).Text
            .txtPtnCd2.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.PTN_CD2.ColNo).Text
            .lblPtnNm.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.PTN_NM2.ColNo).Text
            .numCopyNb1.Value = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.COPIES_NB1.ColNo).Text
            .numCopyNb2.Value = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.COPIES_NB2.ColNo).Text
            .cmbRptOut.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.RPTOUT_KB.ColNo).Text
            .cmbOutPut.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.OUTPUT_KB.ColNo).Text
            .cmbPrinterNm.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.PRINTER_NM.ColNo).Text
            .cmbJobId.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.JOB_ID.ColNo).Text
            .cmbHstryFlg.SelectedValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.HISTORY_FLAG.ColNo).Text
            .txtRemark.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.REMARK.ColNo).Text

            .lblCrtUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.SYS_ENT_USER_NM.ColNo).Text
            .lblCrtDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.SYS_ENT_DATE.ColNo).Text)
            .lblUpdUser.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.SYS_UPD_USER_NM.ColNo).Text
            .lblUpdDate.TextValue = DateFormatUtility.EditSlash(.sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.SYS_UPD_DATE.ColNo).Text)
            .lblUpdTime.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.SYS_UPD_TIME.ColNo).Text
            .lblSysDelFlg.TextValue = .sprDetail.ActiveSheet.Cells(row, LMM240G.sprDetailDef.SYS_DEL_FLG.ColNo).Text

        End With

    End Sub


    ''' <summary>
    ''' 区分値下1桁目を取得
    ''' </summary>
    ''' <param name="flg"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function StdFlgStr(ByVal flg As String) As String

        StdFlgStr = String.Empty

        StdFlgStr = flg.Substring(1, 1)

        Return StdFlgStr

    End Function

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体(上部)
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定        
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SYS_DEL_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_DEL_NM, "状態", 60, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.NRS_BR_CD, "営業所コード", 60, False)
        Public Shared NRS_BR_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.NRS_BR_NM, "営業所", 275, True)
        Public Shared PTN_ID As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PTN_ID, "帳票種類ID区分", 50, False)
        Public Shared PTN_ID_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PTN_ID_NM, "帳票種類ID", 180, True)
        Public Shared STANDARD_FLAG As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.STANDARD_FLAG, "標準帳票フラグ", 50, False)
        Public Shared STANDARD_FLAG_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.STANDARD_FLAG_NM, "標準帳票", 80, True)
        Public Shared PTN_CD As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PTN_CD, "帳票" & vbCrLf & "パターン", 80, True)
        Public Shared PTN_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PTN_NM, "パターン名", 200, True)
        Public Shared RPT_ID As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.RPT_ID, "レポートファイル名(区分) ", 50, False)
        Public Shared RPT_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.RPT_NM, "レポート" & vbCrLf & "ファイル名", 130, True)
        Public Shared RPTOUT_KB As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.RPTOUT_KB, "帳票出力先区分", 50, False)
        Public Shared RPTOUT_KB_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.RPTOUT_KB_NM, "帳票出力先区分", 150, True)
        Public Shared COPIES_NB1 As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.COPIES_NB1, "部数1", 50, False)
        Public Shared PTN_CD2 As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PTN_CD2, " 帳票パターンコード2", 50, False)
        Public Shared PTN_NM2 As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PTN_NM2, "パターン名2", 50, False)
        Public Shared COPIES_NB2 As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.COPIES_NB2, "部数2", 50, False)
        Public Shared OUTPUT_KB As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.OUTPUT_KB, "出力形式区分", 50, False)
        Public Shared PRINTER_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.PRINTER_NM, "プリンタ名", 50, False)
        Public Shared JOB_ID As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.JOB_ID, "ジョブID", 50, False)
        Public Shared HISTORY_FLAG As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.HISTORY_FLAG, " 履歴残し有無", 50, False)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.REMARK, " 備考", 50, False)
        Public Shared SYS_ENT_DATE As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_ENT_DATE, "作成日", 60, False)
        Public Shared SYS_ENT_USER_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_ENT_USER_NM, "作成者", 60, False)
        Public Shared SYS_UPD_DATE As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_UPD_DATE, "更新日", 60, False)
        Public Shared SYS_UPD_USER_NM As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_UPD_USER_NM, "更新者", 60, False)
        Public Shared SYS_UPD_TIME As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_UPD_TIME, "更新時刻", 60, False)
        Public Shared SYS_DEL_FLG As SpreadColProperty = New SpreadColProperty(LMM240C.SprColumnIndex.SYS_DEL_FLG, "削除フラグ", 60, False)


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
            .sprDetail.ActiveSheet.ColumnCount = 29

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New LMM240G.sprDetailDef(), False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = sprDetailDef.DEF.ColNo + 1
            Dim lblStyle As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left)

            '列設定（上部）
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_DEL_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S051", False))
            'M_NRS_BR 【事業所M】のLOCK_FLGが"01"場合はコンボボックスはロックする
            dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.NRS_BR).Select(String.Concat("NRS_BR_CD='" & LMUserInfoManager.GetNrsBrCd) & "'")(0)

            If Not dr.Item("LOCK_FLG").ToString.Equals("") Then
                .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", True))
            Else
                .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.NRS_BR_NM.ColNo, LMSpreadUtility.GetComboCellMaster(.sprDetail, LMConst.CacheTBL.NRS_BR, "NRS_BR_CD", "NRS_BR_NM", False))
            End If
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.NRS_BR_CD.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PTN_ID.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PTN_ID_NM.ColNo, Me.StyleInfoPtnId(.sprDetail))
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.STANDARD_FLAG.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.STANDARD_FLAG_NM.ColNo, Me.StyleInfoUmu(.sprDetail))
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PTN_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PTN_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.RPT_ID.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.RPT_NM.ColNo, Me.StyleInfoRptNm(.sprDetail))
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.RPTOUT_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.RPTOUT_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "T008", False))
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.COPIES_NB1.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PTN_CD2.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PTN_NM2.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.COPIES_NB2.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.OUTPUT_KB.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.PRINTER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.JOB_ID.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.HISTORY_FLAG.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.REMARK.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_ENT_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_ENT_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_UPD_DATE.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_UPD_USER_NM.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_UPD_TIME.ColNo, lblStyle)
            .sprDetail.SetCellStyle(0, LMM240G.sprDetailDef.SYS_DEL_FLG.ColNo, lblStyle)

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMM240F)

        With frm.sprDetail

            .SetCellValue(0, LMM240G.sprDetailDef.DEF.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.NRS_BR_NM.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_DEL_NM.ColNo, LMConst.FLG.OFF)
            .SetCellValue(0, LMM240G.sprDetailDef.NRS_BR_CD.ColNo, LMUserInfoManager.GetNrsBrCd.ToString())
            .SetCellValue(0, LMM240G.sprDetailDef.PTN_ID.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.PTN_ID_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.STANDARD_FLAG.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.STANDARD_FLAG_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.PTN_CD.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.PTN_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.RPT_ID.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.RPT_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.RPTOUT_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.RPTOUT_KB_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.COPIES_NB1.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.PTN_CD2.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.PTN_NM2.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.COPIES_NB2.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.OUTPUT_KB.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.PRINTER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.JOB_ID.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.HISTORY_FLAG.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.REMARK.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_ENT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_ENT_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_UPD_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_UPD_USER_NM.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_UPD_TIME.ColNo, String.Empty)
            .SetCellValue(0, LMM240G.sprDetailDef.SYS_DEL_FLG.ColNo, String.Empty)

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
            Dim id As String = String.Empty

            sDEF.HorizontalAlignment = CellHorizontalAlignment.Center

            Dim dr As DataRow = Nothing

            For i As Integer = 1 To lngcnt

                dr = dt.Rows(i - 1)

                'セルスタイル設定

                .SetCellStyle(i, LMM240G.sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_DEL_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.NRS_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PTN_ID.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PTN_ID_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.STANDARD_FLAG.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.STANDARD_FLAG_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PTN_CD.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PTN_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.RPT_ID.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.RPT_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.RPTOUT_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.RPTOUT_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.COPIES_NB1.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PTN_CD2.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PTN_NM2.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.COPIES_NB2.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.OUTPUT_KB.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.PRINTER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.JOB_ID.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.HISTORY_FLAG.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_ENT_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_ENT_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_UPD_DATE.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_UPD_USER_NM.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_UPD_TIME.ColNo, sLabel)
                .SetCellStyle(i, LMM240G.sprDetailDef.SYS_DEL_FLG.ColNo, sLabel)


                'セルに値を設定
                .SetCellValue(i, LMM240G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_DEL_NM.ColNo, dr.Item("SYS_DEL_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.NRS_BR_CD.ColNo, dr.Item("NRS_BR_CD").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.NRS_BR_NM.ColNo, dr.Item("NRS_BR_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PTN_ID.ColNo, dr.Item("PTN_ID").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PTN_ID_NM.ColNo, dr.Item("PTN_ID_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.STANDARD_FLAG.ColNo, dr.Item("STANDARD_FLAG").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.STANDARD_FLAG_NM.ColNo, dr.Item("STANDARD_FLAG_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PTN_CD.ColNo, dr.Item("PTN_CD").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PTN_NM.ColNo, dr.Item("PTN_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.RPT_ID.ColNo, dr.Item("RPT_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.RPT_NM.ColNo, dr.Item("RPT_ID").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.RPTOUT_KB.ColNo, dr.Item("RPTOUT_KB").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.RPTOUT_KB_NM.ColNo, dr.Item("RPTOUT_KB_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.COPIES_NB1.ColNo, dr.Item("COPIES_NB1").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PTN_CD2.ColNo, dr.Item("PTN_CD2").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PTN_NM2.ColNo, dr.Item("PTN_NM2").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.COPIES_NB2.ColNo, dr.Item("COPIES_NB2").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.OUTPUT_KB.ColNo, dr.Item("OUTPUT_KB").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.PRINTER_NM.ColNo, dr.Item("PRINTER_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.JOB_ID.ColNo, dr.Item("JOB_ID").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.HISTORY_FLAG.ColNo, dr.Item("HISTORY_FLAG").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.REMARK.ColNo, dr.Item("REMARK").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_ENT_DATE.ColNo, dr.Item("SYS_ENT_DATE").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_ENT_USER_NM.ColNo, dr.Item("SYS_ENT_USER_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_UPD_DATE.ColNo, dr.Item("SYS_UPD_DATE").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_UPD_USER_NM.ColNo, dr.Item("SYS_UPD_USER_NM").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_UPD_TIME.ColNo, dr.Item("SYS_UPD_TIME").ToString())
                .SetCellValue(i, LMM240G.sprDetailDef.SYS_DEL_FLG.ColNo, dr.Item("SYS_DEL_FLG").ToString())

            Next

            .ResumeLayout(True)

        End With

    End Sub


    ''' <summary>
    ''' セルのプロパティを設定(有無フラグ)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoUmu(ByVal spr As LMSpread) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , LMM240C.KBNCD _
                                                  , LMM240C.KBNNM2 _
                                                  , False _
                                                  , New String() {LMM240C.KBNGROUPCD} _
                                                  , New String() {LMKbnConst.KBN_U009} _
                                                  )

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(レポートファイル名)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoRptNm(ByVal spr As LMSpread) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , LMM240C.KBNNM1 _
                                                  , LMM240C.KBNNM1 _
                                                  , False _
                                                  , New String() {LMM240C.KBNGROUPCD} _
                                                  , New String() {LMKbnConst.KBN_R010} _
                                                  )

    End Function

    ''' <summary>
    ''' セルのプロパティを設定(帳票種類ID)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Friend Function StyleInfoPtnId(ByVal spr As LMSpread) As StyleInfo


        Return LMSpreadUtility.GetComboCellMaster(spr _
                                                  , LMConst.CacheTBL.KBN _
                                                  , LMM240C.KBNCD _
                                                  , LMM240C.KBNNM1 _
                                                  , False _
                                                  , New String() {LMM240C.KBNGROUPCD, "VALUE1"} _
                                                  , New String() {LMKbnConst.KBN_T007, LMM240C.T007VALUE} _
                                                  , LMConst.JoinCondition.AND_WORD _
                                                  )

    End Function

#End Region 'Spread

#End Region

End Class
