' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM120H : 単価マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMM120ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM120H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM120F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM120V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM120G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMMControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _InDs As DataSet

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' コンボボックス用データセット（製品セグメント）
    ''' </summary>
    Private _dsComboProductSeg As DataSet = Nothing

    ''' <summary>
    ''' 特定荷主フラグ（TSMC）
    ''' </summary>
    Protected _flgTSMC As Boolean = False

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        '特定荷主フラグの設定は、本クラスでは行わない。
        '特定荷主フラグの設定に特化して本クラスを継承した LMM121H よりの実行の場合のみ、
        '_flgTSMC = True となる

        'フォームの作成
        Me._Frm = New LMM120F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM120", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM120G(Me, Me._Frm, Me._ControlG)
        Me._G._flgTSMC = Me._flgTSMC

        'Validateクラスの設定
        Me._V = New LMM120V(Me, Me._Frm, Me._ControlV)
        Me._V._flgTSMC = Me._flgTSMC

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(Me._Frm)
        '2015.10.15 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベントの設定
        'Call Me._ControlH.SetEnterEvent(Me._Frm)
        '2011/08/11 福田 共通動作(右セル移動不可) エンド

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'コンボボックス用の値取得
        Me.GetComboData()

        'コンボボックス設定
        Call Me._G.SetComboControl(_dsComboProductSeg)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '外部倉庫用ABP対策
        Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))
        If drABP.Length > 0 Then
            '製品セグメントの必須を無効
            Me._Frm.cmbProductSegCd.HissuLabelVisible = False
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Me._InDs = New LMM120DS()
        Call Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM120BLF", "EditChk", Me._InDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
        Else
            '編集モード切り替え処理
            Call Me.ChangeEditMode()
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.FUKUSHA) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '複写ボタン押下時チェック
        If Me._V.IsFukushaChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent()

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '削除/復活ボタン押下時チェック
        If Me._V.IsSakujoHukkatuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        Select Case Me._Frm.lblSituation.RecordStatus
            Case RecordStatus.DELETE_REC
                ' 復活の場合
                ' 保存前チェック(現画面表示対象とは逆の期割区分のデータの存在有無判定)
                If GetOtherCondData() Then
                    Dim msg1 As String = ""
                    Dim str As String() = Split(Me._Frm.FunctionKey.F4ButtonName, If(Me._Frm.FunctionKey.F4ButtonName.IndexOf("･") >= 0, "･", "/"))
                    Dim msg2 As String = str(1)
                    If Me._flgTSMC Then
                        ' 単価マスタメンテ(セット料金) の場合、当該荷主にセット料金以外が登録済みの場合エラー
                        msg1 = lgm.Selector({"セット料金以外の単価を登録済みの荷主",
                                                "Shippers who have registered a unit price other than the set price",
                                                "세트 가격 이외의 단가를 등록한 화주",
                                                "Shippers who have registered a unit price other than the set price"})
                    Else
                        ' 通常の単価マスタメンテ の場合、当該荷主にセット料金がが登録済みの場合エラー
                        msg1 = lgm.Selector({"セット料金の単価を登録済みの荷主",
                                                "Shippers who have registered the unit price of the set price",
                                                "세트 가격 단가를 등록한 화주",
                                                "Shippers who have registered the unit price of the set price"})
                    End If
                    MyBase.ShowMessage(Me._Frm, "E320", New String() {msg1, msg2})
                    Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                    Exit Sub
                End If
        End Select

        '処理続行メッセージ表示
        If Me.ConfirmMsg(LMM120C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM120DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM120BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'キャッシュ最新化
        Call Me.GetNewCache()

        '完了メッセージ表示
        Call Me.SetCompleteMessage(LMM120C.EventShubetsu.SAKUJO_HUKKATU)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                Call Me._V.SetBaseMsg() '基本メッセージの表示
                Exit Sub
            End If
        End If

        '検索処理を行う
        Call Me.SelectData()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent()

        '背景色クリア
        Me._ControlG.SetBackColor(Me._Frm)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMMControlC.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction()

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(objNm, LMM120C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="eventShubetu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent(ByVal eventShubetu As LMM120C.EventShubetsu) As Boolean

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目/関連チェック
        If Me._V.IsSaveChk(MyBase.GetSystemDateTime(0)) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        ' 保存前チェック(現画面表示対象とは逆の期割区分のデータの存在有無判定)
        If GetOtherCondData() Then
            Dim msg1 As String = ""
            Dim msg2 As String = Me._Frm.FunctionKey.F11ButtonName
            If lgm.MessageLanguage() = "0" AndAlso msg2.IndexOf("　") >= 0 Then
                msg2 = msg2.Replace("　", "")
            End If
            If Me._flgTSMC Then
                ' 単価マスタメンテ(セット料金) の場合、当該荷主にセット料金以外が登録済みの場合エラー
                msg1 = lgm.Selector({"セット料金以外の単価を登録済みの荷主",
                                        "Shippers who have registered a unit price other than the set price",
                                        "세트 가격 이외의 단가를 등록한 화주",
                                        "Shippers who have registered a unit price other than the set price"})
            Else
                ' 通常の単価マスタメンテ の場合、当該荷主にセット料金がが登録済みの場合エラー
                msg1 = lgm.Selector({"セット料金の単価を登録済みの荷主",
                                        "Shippers who have registered the unit price of the set price",
                                        "세트 가격 단가를 등록한 화주",
                                        "Shippers who have registered the unit price of the set price"})
            End If
            MyBase.ShowMessage(Me._Frm, "E320", New String() {msg1, msg2})
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM120DS()
        Call Me.SetDataSetSave(ds)

#If True Then   'ADD 2020/12/23 017521　【LMS】単価マスタエラー通知仕様追加
        'ﾚｺｰﾄﾞNo.未設定時チェックする
        If Me._Frm.lblRecNo.TextValue = String.Empty Then

            'If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            ds = MyBase.CallWSA("LMM120BLF", "CheckTankaM_UP_GP_CD_1", ds)

            If MyBase.IsErrorMessageExist() = True Then
                Dim msgBoxResult As MsgBoxResult
                If lgm.MessageLanguage() = "0" OrElse lgm.MessageLanguage() = "2" Then
                    msgBoxResult = MyBase.ShowMessage(Me._Frm, "W134", New String() {Me._Frm.lblTitleTankaMstCd.Text})
                Else
                    ' メッセージ文字列が表示されない事象回避
                    msgBoxResult = MsgBox(
                        String.Concat(Me._Frm.lblTitleTankaMstCd.Text, " has already been registered. Are you sure you want to save it?"),
                        MsgBoxStyle.Exclamation Or MsgBoxStyle.OkCancel, "Warning")
                End If
                If msgBoxResult <> MsgBoxResult.Ok Then
                    Call Me._ControlH.EndAction(Me._Frm)  '終了処理
                    Exit Function
                End If
            End If
            'DataSet再設定
            ds = New LMM120DS()
            Call Me.SetDataSetSave(ds)

        End If
#End If
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '==========================
        'WSAクラス呼出
        '==========================
        '保存処理
        ds = MyBase.CallWSA("LMM120BLF", "SaveData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Function
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'キャッシュ最新化
        Call Me.GetNewCache()

        '完了メッセージ表示
        Call Me.SetCompleteMessage(LMM120C.EventShubetsu.HOZON)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return True

    End Function

    ''' <summary>
    ''' 承認処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="eventShubetu">申請、承認、差し戻し</param>
    Private Sub ApprovalEvent(ByVal frm As LMM120F, ByVal eventShubetu As LMM120C.EventShubetsu)

        '処理開始アクション
        Call Me.StartAction()

        '対象行を取得
        Dim arr As ArrayList = Me._ControlH.GetCheckList(frm.sprDetail.ActiveSheet, LMM120G.sprDtlDef.DEF.ColNo)
        If arr.Count = 0 Then
            MyBase.ShowMessage(frm, "E009")
            Call Me._ControlH.EndAction(frm)    '終了処理　
            Return
        End If

        'DataSet設定（エラーチェックも兼ねる）
        Dim ds As DataSet = New LMM120DS()
        Dim dt As DataTable = ds.Tables(LMM120C.TABLE_NM_IN)

        For i As Integer = 0 To arr.Count - 1
            Dim rowIdx As Integer = Convert.ToInt32(arr(i))
            Dim approvalCd As String = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.APPROVAL_CD.ColNo))

            '処理対象とならない行ならばエラー
            Select Case eventShubetu
                Case LMM120C.EventShubetsu.REQUEST
                    '申請：承認状況が未でなければエラー
                    If Not "00".Equals(approvalCd) Then
                        MyBase.ShowMessage(frm, "E01U", New String() {"承認状況が「未」以外のデータが選択されています。"})
                        Call Me._ControlH.EndAction(frm)    '終了処理　
                        Return
                    End If
                Case LMM120C.EventShubetsu.APPROVAL, LMM120C.EventShubetsu.REMAND
                    '承認／差し戻し：承認状況が申請中でなければエラー
                    If Not "09".Equals(approvalCd) Then
                        MyBase.ShowMessage(frm, "E01U", New String() {"承認状況が「申請中」以外のデータが選択されています。"})
                        Call Me._ControlH.EndAction(frm)    '終了処理　
                        Return
                    End If
            End Select

            'DataSet設定
            Dim dr As DataRow = dt.NewRow()

            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.BR_CD.ColNo))
            dr.Item("CUST_CD_L") = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.CUST_CD_L.ColNo))
            dr.Item("CUST_CD_M") = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.CUST_CD_M.ColNo))
            dr.Item("UP_GP_CD_1") = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.TANKA_MST_CD.ColNo))
            dr.Item("STR_DATE") = DateFormatUtility.DeleteSlash(Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.TEKIYO_START_DATE.ColNo)))
            dr.Item("SYS_UPD_DATE") = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.UPDATE_DATE.ColNo))
            dr.Item("SYS_UPD_TIME") = Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMM120G.sprDtlDef.UPDATE_TIME.ColNo))

            Select Case eventShubetu
                Case LMM120C.EventShubetsu.REQUEST
                    '申請：未 ⇒ 申請中
                    dr.Item("APPROVAL_CD") = "09"
                    dr.Item("APPROVAL_USER") = String.Empty
                    dr.Item("APPROVAL_DATE") = String.Empty
                    dr.Item("APPROVAL_TIME") = String.Empty
                Case LMM120C.EventShubetsu.APPROVAL
                    '承認：申請中 ⇒ 済
                    dr.Item("APPROVAL_CD") = "01"
                    dr.Item("APPROVAL_USER") = Me.GetUserID()
                    dr.Item("APPROVAL_DATE") = "DACで"
                    dr.Item("APPROVAL_TIME") = "DACで"
                Case LMM120C.EventShubetsu.REMAND
                    '差し戻し：申請中 ⇒ 未
                    dr.Item("APPROVAL_CD") = "00"
                    dr.Item("APPROVAL_USER") = String.Empty
                    dr.Item("APPROVAL_DATE") = String.Empty
                    dr.Item("APPROVAL_TIME") = String.Empty
            End Select

            dt.Rows.Add(dr)
        Next

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ApprovalData")

        '==========================
        'WSAクラス呼出
        '==========================
        '承認処理
        ds = MyBase.CallWSA("LMM120BLF", "ApprovalData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() Then
            MyBase.ShowMessage(frm)
            Call Me._ControlH.EndAction(frm)    '終了処理　
            Return
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ApprovalData")

        '終了処理
        Call Me._ControlH.EndAction(frm)

        '完了メッセージ表示
        With frm
            Dim shoriMsg As String = String.Empty
            Select Case eventShubetu
                Case LMM120C.EventShubetsu.REQUEST
                    shoriMsg = Me._Frm.FunctionKey.F5ButtonName
                Case LMM120C.EventShubetsu.APPROVAL
                    shoriMsg = Me._Frm.FunctionKey.F6ButtonName
                Case LMM120C.EventShubetsu.REMAND
                    shoriMsg = Me._Frm.btnRemand.Text
            End Select
            MyBase.ShowMessage(Me._Frm, "G002", New String() {shoriMsg, String.Empty})
        End With

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveEvent(LMM120C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    '''  Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm)  '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(e.Row)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            'カーソル位置チェック
            If Me._V.IsFocusChk(objNm, LMMControlC.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            '処理開始アクション
            Call Me.StartAction()

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(objNm, LMM120C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal frm As LMM120F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.NewRow
        If rowNo < 1 Then
            Exit Sub
        End If

        '同じ行の場合、スルー
        If e.Row = rowNo Then
            Exit Sub
        End If

        Call Me.RowSelection(rowNo)

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction()

        'マスタメンテ共通処理
        Me._ControlH.StartAction(Me._Frm)

        '背景色クリア
        Me._ControlG.SetBackColor(Me._Frm)

    End Sub

    ''' <summary>
    ''' 編集処理切り替え処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeEditMode()

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectData()

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Me._InDs = New LMM120DS()
        Call SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm, "LMM120BLF", "SelectListData", Me._InDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm _
        '                                                , "LMM120BLF" _
        '                                                , "SelectListData" _
        '                                                , Me._InDs _
        '                                                , Me._ControlH.GetLimitCount() _
        '                                                , , _
        '                                                Convert.ToInt32(Convert.ToDouble( _
        '                                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMM120C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMM120C.TABLE_NM_OUT)
        Call Me._G.SetSpread(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM120C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(rowNo, LMM120G.sprDtlDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G013")

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal eventShubetu As LMM120C.EventShubetsu) As Boolean

        Select Case eventShubetu
            Case LMM120C.EventShubetsu.SAKUJO_HUKKATU
                '処理続行メッセージ表示
                Dim msg As String = String.Empty

                '2016.01.06 UMANO 英語化対応START
                Dim str As String() = Split(Me._Frm.FunctionKey.F4ButtonName, If(Me._Frm.FunctionKey.F4ButtonName.IndexOf("･") >= 0, "･", "/"))
                '2016.01.06 UMANO 英語化対応END

                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.DELETE_REC
                        '2016.01.06 UMANO 英語化対応START
                        'msg = "復活"
                        msg = str(1)
                    Case RecordStatus.NOMAL_REC
                        'msg = "削除"
                        msg = str(0)
                        '2016.01.06 UMANO 英語化対応START
                End Select
                If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
                    Call Me._V.SetBaseMsg() 'メッセージエリアの設定
                    Exit Function
                End If
        End Select

        Return True

    End Function

    ''' <summary>
    ''' 処理完了メッセージ
    ''' </summary>
    ''' <param name="eventShubetu">イベント種別</param>
    ''' <remarks></remarks>
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMM120C.EventShubetsu)

        With Me._Frm

            Dim shoriMsg As String = String.Empty

            Select Case eventShubetu
                Case LMM120C.EventShubetsu.SAKUJO_HUKKATU
                    '2016.01.06 UMANO 英語化対応START
                    'shoriMsg = "削除・復活"
                    shoriMsg = Me._Frm.FunctionKey.F4ButtonName
                Case LMM120C.EventShubetsu.HOZON
                    'shoriMsg = "保存"
                    shoriMsg = Me._Frm.FunctionKey.F11ButtonName
                    '2016.01.06 UMANO 英語化対応END
                Case LMM120C.EventShubetsu.REQUEST
                    shoriMsg = Me._Frm.FunctionKey.F5ButtonName
                Case LMM120C.EventShubetsu.APPROVAL
                    shoriMsg = Me._Frm.FunctionKey.F6ButtonName
                Case LMM120C.EventShubetsu.REMAND
                    shoriMsg = Me._Frm.btnRemand.Text
            End Select

            '2016.01.06 UMANO 英語化対応START
            'Dim comMsg As String = String.Concat("[荷主コード = ", Me._Frm.txtCustCdL.TextValue, "-", Me._Frm.txtCustCdM.TextValue, "]")
            'comMsg = String.Concat(comMsg, "[単価マスタコード = ", Me._Frm.txtTankaMstCd.TextValue, "]")
            Dim comMsg As String = String.Concat(Me._Frm.txtCustCdL.TextValue, "-", Me._Frm.txtCustCdM.TextValue)
            comMsg = String.Concat(comMsg, Me._Frm.txtTankaMstCd.TextValue)
            '2016.01.06 UMANO 英語化対応END
            MyBase.ShowMessage(Me._Frm, "G002", New String() {shoriMsg, comMsg})

        End With

    End Sub

    ''' <summary>
    ''' キャッシュ最新化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetNewCache()

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TANKA)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TANKA_GRP)

    End Sub

    ''' <summary>
    ''' コンボボックス用の値取得
    ''' </summary>
    Private Sub GetComboData()

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        _dsComboProductSeg = New LMM120DS()

        '製品セグメント取得
        Dim dt As DataTable = _dsComboProductSeg.Tables("LMM120COMBO_SEIHINA")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
        dt.Rows.Add(dr)
        _dsComboProductSeg = MyBase.CallWSA("LMM120BLF", "SelectComboSeihin", _dsComboProductSeg)

    End Sub

    ''' <summary>
    ''' 保存前チェック時用・現画面表示対象とは逆の期割区分のデータの存在有無判定
    ''' </summary>
    ''' <returns></returns>
    Private Function GetOtherCondData() As Boolean

        Dim isExists As Boolean = False

        ' DataSet設定
        Dim rtDs As DataSet = New LMM120DS()
        Call SetDataSetInDataOtherCond(rtDs)


        ' 強制実行フラグの設定
        ' (存在チェックにつき強制実行なし)
        MyBase.SetForceOparation(False)

        ' 閾値の設定
        ' (存在チェックにつき 0)
        MyBase.SetLimitCount(0)

        ' 表示最大件数の設定
        ' (存在チェックにつき 0)
        MyBase.SetMaxResultCount(0)

        Dim rtnDs As DataSet = MyBase.CallWSA("LMM120BLF", "SelectListData", rtDs)

        ' メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.GetMessageID() <> "G001" OrElse MyBase.GetResultCount() > 0 Then
                ' 0件メッセージ以外のメッセージ、または 対象件数 0 より大 で戻れば存在あり
                isExists = True
            End If
            MyBase.ClearMessageData()
        Else
            If rtnDs IsNot Nothing _
            AndAlso rtnDs.Tables(LMM120C.TABLE_NM_OUT).Rows.Count > 0 Then
                isExists = True
            End If
        End If

        Return isExists

    End Function

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal eventShubetu As LMM120C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name _
                    , .txtCustCdM.Name

                    '荷主マスタ参照POP起動
                    Call Me.SetReturnCustPop(objNm, eventShubetu)

            End Select

        End With

        Return True

    End Function

#Region "荷主マスタ"

    ''' <summary>
    ''' 荷主マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal objNm As String, ByVal eventShubetu As LMM120C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(ctl, eventShubetu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With Me._Frm

                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventShubetu As LMM120C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventShubetu = LMM120C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = Me._Frm.txtCustCdM.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        Dim dt As DataTable = Me._InDs.Tables(LMM120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm.sprDetail.ActiveSheet

            '検索条件を設定
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.BR_NM.ColNo))
            dr.Item("CUST_CD_L") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.CUST_CD_L.ColNo))
            dr.Item("CUST_NM_L") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.CUST_NM_L.ColNo))
            dr.Item("CUST_CD_M") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.CUST_CD_M.ColNo))
            dr.Item("CUST_NM_M") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.CUST_NM_M.ColNo))
            dr.Item("UP_GP_CD_1") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.TANKA_MST_CD.ColNo))
            dr.Item("ALL_DATA_FLG") = Me._Frm.chkZenDataHyoji.GetBinaryValue()
            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.STATUS.ColNo))
            dr.Item("AVAL_YN") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.AVAL_YN_NM.ColNo))          'ADD 2019/04/18 依頼番号 : 004862
            dr.Item("APPROVAL_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.APPROVAL_NM.ColNo))
            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM120G.sprDtlDef.BR_CD.ColNo))
            dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")
            If Me._flgTSMC Then
                dr.Item("KIWARI_KB_COND") = " = '05'"
            Else
                dr.Item("KIWARI_KB_COND") = " <> '05'"
            End If

            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim dt As DataTable = Me._InDs.Tables(LMM120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("UP_GP_CD_1") = .txtTankaMstCd.TextValue
            dr.Item("STR_DATE") = .imdTekiyoStart.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()


            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(削除復活処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("UP_GP_CD_1") = .txtTankaMstCd.TextValue
            dr.Item("STR_DATE") = .imdTekiyoStart.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            '削除/復活の切り替えを行う
            Dim delflg As String = String.Empty
            Select Case .lblSituation.RecordStatus
                Case RecordStatus.NOMAL_REC
                    delflg = LMConst.FLG.ON
                Case RecordStatus.DELETE_REC
                    delflg = LMConst.FLG.OFF
            End Select
            dr.Item("SYS_DEL_FLG") = delflg

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()


            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(保存前チェック時用・現画面表示対象とは逆の期割区分検索用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInDataOtherCond(ByVal ds As DataSet)

        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        Dim dt As DataTable = ds.Tables(LMM120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("SYS_DEL_FLG") = "0"
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue
            dr.Item("KBN_LANG") = If("0".Equals(lgm.MessageLanguage()), "ja", "en")

            If Me._flgTSMC Then
                ' 単価マスタメンテ(セット料金) の場合はセット料金以外を対象とする。
                dr.Item("KIWARI_KB_COND") = " <> '05'"
            Else
                ' 通常の単価マスタメンテ の場合はセット料金を対象とする。
                dr.Item("KIWARI_KB_COND") = " = '05'"
            End If

            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetSave(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            '登録項目格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("REC_NO") = .lblRecNo.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("KIWARI_KB") = .cmbKiwariKbn.SelectedValue
            dr.Item("REMARK") = .txtTekiyo.TextValue
            dr.Item("UP_GP_CD_1") = .txtTankaMstCd.TextValue
            dr.Item("STR_DATE") = .imdTekiyoStart.TextValue

            If Me._flgTSMC Then
                '特定荷主（TSMC）の場合はパレット建で固定
                dr.Item("STORAGE_KB1") = "04"
                dr.Item("STORAGE_KB2") = "04"
            Else
                dr.Item("STORAGE_KB1") = .cmbHokanKbnNashi.SelectedValue
                dr.Item("STORAGE_KB2") = .cmbHokanKbnAri.SelectedValue
            End If

            dr.Item("STORAGE_1") = .numHokanryoNashi.Value
            dr.Item("STORAGE_2") = .numHokanryoAri.Value

            If Me._flgTSMC Then
                '特定荷主（TSMC）の場合はパレット建で固定
                dr.Item("HANDLING_IN_KB") = "04"
                dr.Item("HANDLING_OUT_KB") = "04"
            Else
                dr.Item("HANDLING_IN_KB") = .cmbNiyakuryoKbnNyuko.SelectedValue
                dr.Item("HANDLING_OUT_KB") = .cmbNiyakuryoKbnShukko.SelectedValue
            End If

            dr.Item("HANDLING_IN") = .numNiyakuryoNyuko.Value
            dr.Item("HANDLING_OUT") = .numNiyakuryoShukko.Value
            dr.Item("MINI_TEKI_IN_AMO") = .numMinHoshoNyuko.Value
            dr.Item("MINI_TEKI_OUT_AMO") = .numMinHoshoShukko.Value
            dr.Item("AVAL_YN") = .cmbAVAL_YN.SelectedValue          'ADD 2019/04/18 依頼番号 : 004862
            dr.Item("PRODUCT_SEG_CD") = .cmbProductSegCd.SelectedValue

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            '排他処理条件を格納
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            dt.Rows.Add(dr)

        End With

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.NewDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ApprovalEvent")

        '承認処理（申請）
        Me.ApprovalEvent(frm, LMM120C.EventShubetsu.REQUEST)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ApprovalEvent")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ApprovalEvent")

        '承認処理（承認）
        Me.ApprovalEvent(frm, LMM120C.EventShubetsu.APPROVAL)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ApprovalEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent(LMM120C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM120F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM120F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM120FKeyDown(ByVal frm As LMM120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM120F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM120F_KeyDown")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetailLeaveCell(ByVal frm As LMM120F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetailLeaveCell")

        Call Me.SprFindLeaveCell(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetailLeaveCell")

    End Sub

    ''' <summary>
    ''' 差し戻しボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnRemand_Click(ByVal frm As LMM120F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '承認処理（差し戻し）
        Me.ApprovalEvent(frm, LMM120C.EventShubetsu.REMAND)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class