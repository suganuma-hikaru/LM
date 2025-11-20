' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI260H : 引取運賃明細入力
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI260ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI260H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI260F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI260V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI260G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

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
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' 印刷種別フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintFlg As String

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

        'フォームの作成
        Me._Frm = New LMI260F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH(MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI260V(Me, Me._Frm, Me._ControlV)

        'Gamenクラスの設定
        Me._G = New LMI260G(Me, Me._Frm, Me._ControlG, Me._V)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitDetailSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        '画面の入力項目
        Call Me._G.SetControlsStatus()
        Call Me._G.SetInitValue()

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
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '画面項目全クリア
        Call Me._G.ClearControl(Me._Frm)

        '画面初期値設定
        Call Me._G.DefaultSetControl(MyBase.GetSystemDateTime(0))

        'メッセージの表示
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
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim rtnds As DataSet = Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Call MyBase.CallWSA("LMI260BLF", "EditChk", rtnds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.FUKUSHA) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '明細NOの初期処理
        Call Me._G.SetMeisaiNo()

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

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行格納処理
        Me._ChkList = Me._V.GetCheckList()

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI260C.EventShubetsu.SAKUJO_HUKKATU, Me._ChkList) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行メッセージ表示
        If Me.ConfirmMsg(LMI260C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim rtnDs As DataSet = Me.SetDatasetDelData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理
        rtnDs = MyBase.CallWSA("LMI260BLF", "DeleteData", rtnDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        'メッセージ表示
        Call Me.SetCompleteMessage(LMI260C.EventShubetsu.SAKUJO_HUKKATU)

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        '画面項目全クリア
        Call Me._G.ClearControl(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI260C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                MyBase.ShowMessage(Me._Frm, "G003")
                Exit Sub
            End If
        End If

        'DataSet設定
        Dim rtnDs As DataSet = Me.SetDataSetInData()


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "SelectListData", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI260C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)
        End If

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
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMI260C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = True
        Me._ControlH.StartAction(Me._Frm)

        'Pop起動処理
        Call Me.ShowPopupControl(objNm, LMI260C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent() As Boolean

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目/関連チェック
        If Me._G.SetSumTotal(LMI260C.EventShubetsu.HOZON) = False OrElse Me._V.IsInputChk(LMI260C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        Dim rtnds As DataSet
        '更新の場合のみ排他チェック
        'DataSet設定(排他チェック)
        rtnds = Me.SetDataSetHaitaChk()

        If Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) _
           OrElse Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.DELETE_REC) Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

            '==========================
            'WSAクラス呼出
            '==========================
            Call MyBase.CallWSA("LMI260BLF", "EditChk", rtnds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

            'メッセージコードの判定
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
                Call Me._ControlH.EndAction(Me._Frm) '終了処理
                Exit Function
            End If

        End If

        'DataSetクリア
        rtnds.Clear()

        'DataSet設定
        rtnds = Me.SetDataSetSave()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '存在チェック
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        If Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
            '編集登録
            rtnds = MyBase.CallWSA(blf, "UpdateData", rtnds)
        Else
            '新規登録
            rtnds = MyBase.CallWSA(blf, "InsertData", rtnds)
        End If

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '完了メッセージ表示
        Call Me.SetCompleteMessage(LMI260C.EventShubetsu.HOZON)

        Return True

    End Function

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

                If Me.SaveEvent() = False Then

                    e.Cancel = True

                End If

            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnPrintClick()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.PRINT) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI260C.EventShubetsu.PRINT) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'データセット
        Dim rtnDs As DataSet = Me.SetPrintData()
        rtnDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        Select Case Me._PrintFlg

            Case LMI260C.PRINT_CHECK_LIST
                '引取運賃明細チェックリスト
                rtnDs = MyBase.CallWSA(blf, "ChkPrint", rtnDs)

            Case LMI260C.PRINT_MEISAI
                '引取運賃明細書
                rtnDs = MyBase.CallWSA(blf, "MeisaiPrint", rtnDs)

        End Select

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)

        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        '終了メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {"印刷", ""})

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
            If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            'カーソル位置チェック
            If Me._V.IsFocusChk(objNm, LMI260C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            '処理開始アクション
            Me._ControlH.StartAction(Me._Frm)

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(objNm, LMI260C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

    ''' <summary>
    ''' 合計計算処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SumTotal(Optional ByVal eventShubetsu As LMI260C.EventShubetsu = Nothing)

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '合計計算処理
        Call Me._G.SetSumTotal(eventShubetsu)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(Me._Frm.lblSituation.DispMode) = True Then
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
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMI260C.TABLE_NM_OUT)
        Call Me._G.SetSpread(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})


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

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '画面項目全クリア
        Call Me._G.ClearControl(Me._Frm)

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G001")

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI260C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(rowNo, LMI260G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal eventShubetu As LMI260C.EventShubetsu) As Boolean

        Select Case eventShubetu
            Case LMI260C.EventShubetsu.SAKUJO_HUKKATU
                '処理続行メッセージ表示
                Dim msg As String = String.Empty

                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.DELETE_REC
                        msg = "復活"
                    Case RecordStatus.NOMAL_REC
                        msg = "削除"
                End Select

                If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
                    Call Me._V.SetBaseMsg() 'メッセージエリアの設定
                    Exit Function
                End If

            Case LMI260C.EventShubetsu.HOZON
                '確認メッセージ
                If MyBase.ShowMessage(Me._Frm, "W003") = MsgBoxResult.Cancel Then
                    Call Me._V.SetBaseMsg()
                    Return False
                End If

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 処理完了メッセージ
    ''' </summary>
    ''' <param name="eventShubetu">イベント種別</param>
    ''' <remarks></remarks>
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMI260C.EventShubetsu)

        With Me._Frm

            Dim shoriMsg As String = String.Empty

            Select Case eventShubetu
                Case LMI260C.EventShubetsu.SAKUJO_HUKKATU
                    shoriMsg = "削除・復活"
                Case LMI260C.EventShubetsu.HOZON
                    shoriMsg = "保存"
            End Select

            Dim comMsg As String = String.Empty
            MyBase.ShowMessage(Me._Frm, "G002", New String() {shoriMsg, comMsg})

        End With

    End Sub

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal eventshubetsu As LMI260C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name _
                   , .txtCustCdM.Name _
                    '荷主マスタ参照POP起動
                    Call Me.SetReturnCustPop(objNm, eventshubetsu)

                Case .txtHikitoriCd.Name
                    '届け先マスタ参照POP起動
                    Call Me.SetReturnDestPop(objNm, eventshubetsu)

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
    Private Function SetReturnCustPop(ByVal objNm As String, ByVal eventshubetsu As LMI260C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(ctl, eventshubetsu)
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
    Private Function ShowCustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMI260C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbNrsBr.SelectedValue.ToString()
            If eventshubetsu = LMI260C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = Me._Frm.txtCustCdM.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "届け先マスタ"

    ''' <summary>
    ''' 届け先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal objNm As String, ByVal eventshubetsu As LMI260C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                .txtHikitoriCd.TextValue = dr.Item("DEST_CD").ToString()
                .lblHikitoriNm.TextValue = dr.Item("DEST_NM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届け先マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMI260C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbNrsBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            If eventshubetsu = LMI260C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = Me._Frm.txtHikitoriCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.OFF

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetInData() As DataSet

        Dim ds As DataSet = New LMI260DS
        Dim dr As DataRow = ds.Tables(LMI260C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("F_DATE") = .imdHikiDate_From.TextValue
            dr.Item("T_DATE") = .imdHikiDate_To.TextValue
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("HIKITORI_CD") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.HIKITORI_CD.ColNo))
            dr.Item("HIKITORI_NM") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.HIKITORI_NM.ColNo))
            dr.Item("HIN_CD") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.HIN_NM.ColNo))
            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.STATUS.ColNo))

            ds.Tables(LMI260C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetHaitaChk() As DataSet

        Dim ds As DataSet = New LMI260DS
        Dim dr As DataRow = ds.Tables(LMI260C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("HIKI_DATE") = .imdHikiDate.TextValue
            dr.Item("MEISAI_NO") = .numMeisaiNo.TextValue
            dr.Item("HIN_CD") = .cmbHinNm.SelectedValue
            dr.Item("HIKITORI_CD") = .txtHikitoriCd.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            ds.Tables(LMI260C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(削除・復活処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDatasetDelData() As DataSet

        Dim ds As DataSet = New LMI260DS
        Dim dr As DataRow = ds.Tables(LMI260C.TABLE_NM_IN).NewRow()

        With Me._Frm

            '削除/復活の切り替えを行う
            Dim delflg As String = String.Empty

            Select Case .lblSituation.RecordStatus

                Case RecordStatus.NOMAL_REC
                    delflg = LMConst.FLG.ON

                Case RecordStatus.DELETE_REC
                    delflg = LMConst.FLG.OFF

            End Select

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("HIKI_DATE") = .imdHikiDate.TextValue
            dr.Item("MEISAI_NO") = .numMeisaiNo.TextValue
            dr.Item("HIN_CD") = .cmbHinNm.SelectedValue
            dr.Item("HIKITORI_CD") = .txtHikitoriCd.TextValue
            dr.Item("SYS_DEL_FLG") = delflg

            ds.Tables(LMI260C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetSave() As DataSet

        Dim ds As DataSet = New LMI260DS
        Dim dr As DataRow = ds.Tables(LMI260C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("HIKI_DATE") = .imdHikiDate.TextValue
            dr.Item("MEISAI_NO") = .numMeisaiNo.Value
            dr.Item("HIN_CD") = .cmbHinNm.SelectedValue
            dr.Item("HIKITORI_CD") = .txtHikitoriCd.TextValue
            dr.Item("FC_NB") = .numFcNb.Value
            dr.Item("FC_TANKA") = .numFcTanka.Value
            dr.Item("FC_TOTAL") = .numFcTotal.Value
            dr.Item("DM_NB") = .numDmNb.Value
            dr.Item("DM_TANKA") = .numDmTanka.Value
            dr.Item("DM_TOTAL") = .numDmTotal.Value
            dr.Item("KISU") = .numKisu.Value
            dr.Item("SEIHIN") = .numSeihin.Value
            dr.Item("SUKURAP") = .numSukurap.Value
            dr.Item("WARIMASI") = .numWarimasi.Value
            dr.Item("SEIKEI") = .numSeikei.Value
            dr.Item("ROSEN") = .numRosen.Value
            dr.Item("KOUSOKU") = .numKousoku.Value
            dr.Item("SONOTA") = .numSonota.Value
            dr.Item("ALL_TOTAL") = .numAllTotal.Value
            dr.Item("REMARK") = .txtRemark.TextValue

            ds.Tables(LMI260C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(印刷処理)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function SetPrintData() As DataSet

        With Me._Frm

            Dim ds As DataSet = Nothing
            Dim dt As DataTable = Nothing
            Dim dr As DataRow = Nothing

            Select Case .cmbPrintShubetu.SelectedValue.ToString()

                Case LMI260C.PRINT_CHECK_LIST
                    ds = New LMI630DS
                    dt = ds.Tables(LMI260C.TABLE_NM_CHK)
                    Me._PrintFlg = "01"

                Case LMI260C.PRINT_MEISAI
                    ds = New LMI640DS
                    dt = ds.Tables(LMI260C.TABLE_NM_MEISAI)
                    Me._PrintFlg = "02"

            End Select

            dr = dt.NewRow()

            Select Case .cmbPrintHinNm.SelectedValue.ToString()

                Case LMI260C.PRINT_HIN_NM_ALL
                    dr.Item("HIN_CD") = ""

                Case LMI260C.PRINT_HIN_NM_HB
                    dr.Item("HIN_CD") = LMI260C.PRINT_HIN_NM_HB

                Case LMI260C.PRINT_HIN_NM_BTS
                    dr.Item("HIN_CD") = LMI260C.PRINT_HIN_NM_BTS

                Case LMI260C.PRINT_HIN_NM_KEMI
                    dr.Item("HIN_CD") = LMI260C.PRINT_HIN_NM_KEMI

            End Select

            dr.Item("F_DATE") = .imdHikiDate_From.TextValue
            dr.Item("T_DATE") = .imdHikiDate_To.TextValue
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("CUST_CD_L") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.CUST_CD_L.ColNo))
            dr.Item("CUST_CD_M") = Me._ControlV.GetCellValue(.sprDetail.ActiveSheet.Cells(0, LMI260G.sprDetailDef.CUST_CD_M.ColNo))
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            dt.Rows.Add(dr)

            Return ds

        End With

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(複写)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し(削除・復活)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除・復活処理
        Me.DeleteDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI260F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMI260F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Friend Sub LMI260FKeyDown(ByVal frm As LMI260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMI260F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMI260F_KeyDown")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetailLeaveCell(ByVal frm As LMI260F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetailLeaveCell")

        Call Me.SprFindLeaveCell(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetailLeaveCell")

    End Sub

    ''' <summary>
    ''' フレコンの合計値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numFcTotal_Leave(ByVal frm As LMI260F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FC_TOTAL")

        'フレコン合計処理
        Me.SumTotal(LMI260C.EventShubetsu.FC_TOTAL)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FC_TOTAL")

    End Sub

    ''' <summary>
    ''' ドラムの合計値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numDmTotal_Leave(ByVal frm As LMI260F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DM_TOTAL")

        'フレコン合計処理
        Me.SumTotal(LMI260C.EventShubetsu.DM_TOTAL)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DM_TOTAL")

    End Sub

    ''' <summary>
    ''' 総合計値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numAllTotal_Leave(ByVal frm As LMI260F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ALL_TOTAL")

        '総合計処理
        Me.SumTotal()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ALL_TOTAL")

    End Sub

    ''' <summary>
    ''' Printボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub PrintClick(ByVal frm As LMI260F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintClick")

        '印刷処理
        Call Me.BtnPrintClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintClick")

    End Sub

    ''========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class