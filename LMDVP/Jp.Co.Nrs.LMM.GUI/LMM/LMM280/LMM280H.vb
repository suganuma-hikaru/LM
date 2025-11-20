' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM280H : 横持ちマスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility

''' <summary>
''' LMM280ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM280H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM280F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM280V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM280G

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
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

    ''' <summary>
    '''最大枝番格納フィールド(チェック時使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxEdaNo As Integer

    ''' <summary>
    '''最大枝番格納フィールド(設定時使用)
    ''' </summary>
    ''' <remarks></remarks>
    Private _MaxEdaNoSet As Integer

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
        Me._Frm = New LMM280F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM280", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM280G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMM280V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

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

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

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
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '枝番の設定
        Me._MaxEdaNo = -1
        Me._MaxEdaNoSet = -1

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
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Me._InDs = New LMM280DS()
        Call Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM280BLF", "EditChk", Me._InDs)

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
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.FUKUSHA) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '複写ボタン押下時チェック
        If Me._V.IsFukushaChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '枝番の設定
        Me._MaxEdaNo = Me._Frm.sprYokomochiDtl.ActiveSheet.Rows.Count - 1
        Me._MaxEdaNoSet = -1

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '横持ちタリフ明細マスタ情報をSpreadに設定
        Call Me._G.SetSpreadDtl(Me._DispDt)

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

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '削除/復活ボタン押下時チェック
        If Me._V.IsSakujoHukkatuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行メッセージ表示
        If Me.ConfirmMsg(LMM280C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM280DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM280BLF", "DeleteData", ds)

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
        Call Me.SetCompleteMessage(LMM280C.EventShubetsu.SAKUJO_HUKKATU)

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
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.KENSAKU) = False Then
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
    ''' 保存処理
    ''' </summary>
    ''' <param name="eventShubetu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent(ByVal eventShubetu As LMM280C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目/関連チェック
        If Me._V.IsSaveChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM280DS()
        Call Me.SetDataSetSave(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '==========================
        'WSAクラス呼出
        '==========================
        If Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
            '更新処理
            ds = MyBase.CallWSA("LMM280BLF", "UpdateData", ds)
        Else
            '新規登録処理
            ds = MyBase.CallWSA("LMM280BLF", "InsertData", ds)
        End If

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
        Call Me.SetCompleteMessage(LMM280C.EventShubetsu.HOZON)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

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

                If Me.SaveEvent(LMM280C.EventShubetsu.TOJIRU) = False Then

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
    ''' 行追加処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowAdd()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.ADD_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsAddRowChk(Me._MaxEdaNo) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '採番データを採番用に編集する。
        Me._MaxEdaNo = Me._MaxEdaNo + 1

        '行追加処理を行う
        Call Me._G.AddRow()

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        '計算方法コンボボックスの入力制御を行う
        Call Me._G.LockKeisanHohoCombo()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowDel()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.DEL_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprYokomochiDtl.ActiveSheet, LMM280G.sprDtlDef.DEF.ColNo)

        '単項目/関連チェック
        If Me._V.IsDelRowChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行削除
        Call Me._G.DelateDtl(list)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        '計算方法コンボボックスの入力制御を行う
        Call Me._G.LockKeisanHohoCombo()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        'フォーカス移動処理
        Call Me._ControlH.NextFocusedControl(Me._Frm, True)

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal frm As LMM280F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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

        '最大枝番の設定
        Me._MaxEdaNo = -1
        Dim max As Integer = Me._Frm.sprYokomochiDtl.ActiveSheet.Rows.Count - 1
        Dim edaNo As Integer = 0
        For i As Integer = 0 To max
            edaNo = Convert.ToInt32(Me._ControlV.GetCellValue(Me._Frm.sprYokomochiDtl.ActiveSheet.Cells(i, LMM280G.sprDtlDef.YOKO_TARIFF_CD_EDA.ColNo)))
            If Me._MaxEdaNo < edaNo Then
                Me._MaxEdaNo = edaNo
            End If
        Next
        Me._MaxEdaNoSet = Me._MaxEdaNo

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '商品明細マスタ情報表示設定
        Call Me._G.SetSpreadDtl(Me._DispDt)

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
        Me._InDs = New LMM280DS()
        Call SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm, "LMM280BLF", "SelectListData", Me._InDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm _
        '                                                , "LMM280BLF" _
        '                                                , "SelectListData" _
        '                                                , Me._InDs _
        '                                                , Me._ControlH.GetLimitCount() _
        '                                                 , , _
        '                                                Convert.ToInt32(Convert.ToDouble( _
        '                                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                               .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))


        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMM280C.TABLE_NM_YOKO_HED).Rows.Count > 0 Then
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
        Me._Frm.sprYokomochiHed.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMM280C.TABLE_NM_YOKO_HED)
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
        Me._Frm.sprYokomochiHed.CrearSpread()

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
        If Me._V.IsAuthorityChk(LMM280C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprYokomochiHed.ActiveSheet.Cells(rowNo, LMM280G.sprHedDef.SYS_DEL_FLG.ColNo))) = True Then
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

        '横持ちタリフ明細マスタ情報をSpreadに設定
        Call Me.GetYokoTariffDtlDisplayData()
        Call Me._G.SetSpreadDtl(Me._DispDt)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G013")

    End Sub

    ''' <summary>
    ''' 横持ちタリフ明細マスタSpread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetYokoTariffDtlDisplayData()

        Dim dt As DataTable = Me._OutDs.Tables(LMM280C.TABLE_NM_YOKO_DTL)

        '表示対象データを取得
        Me._DispDt = Nothing
        Dim filter As String = String.Empty
        filter = String.Concat(filter, "NRS_BR_CD = '", Me._Frm.cmbBr.SelectedValue, "'")
        filter = String.Concat(filter, " AND YOKO_TARIFF_CD = '", Me._Frm.txtYokomochiTariff.TextValue, "'")
        Dim orderBy As String = String.Empty
        Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString
            Case LMM280C.KEISAN_CD_NISUGATA
                orderBy = "CARGO_KB,DANGER_KB"  '荷姿区分、危険品区分
            Case LMM280C.KEISAN_CD_SHADATE
                orderBy = "CAR_KB,DANGER_KB"    '車種区分、危険品区分
            Case LMM280C.KEISAN_CD_TEIZO_UNCHIN
                orderBy = "WT_LV,DANGER_KB"     '区切り重量、危険品区分
            Case LMM280C.KEISAN_CD_JURYO
                orderBy = "DANGER_KB"           '危険品区分
        End Select
        Dim selectDr As DataRow() = dt.Select(filter, orderBy)
        Dim max As Integer = selectDr.Length - 1
        Dim setDS As DataSet = New LMM280DS()
        Me._DispDt = setDS.Tables(LMM280C.TABLE_NM_YOKO_DTL)

        For i As Integer = 0 To max
            Me._DispDt.ImportRow(selectDr(i))
        Next

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal eventShubetu As LMM280C.EventShubetsu) As Boolean

        Select Case eventShubetu
            Case LMM280C.EventShubetsu.SAKUJO_HUKKATU
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

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 処理完了メッセージ
    ''' </summary>
    ''' <param name="eventShubetu">イベント種別</param>
    ''' <remarks></remarks>
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMM280C.EventShubetsu)

        With Me._Frm

            Dim shoriMsg As String = String.Empty

            Select Case eventShubetu
                Case LMM280C.EventShubetsu.SAKUJO_HUKKATU
                    shoriMsg = "削除・復活"
                Case LMM280C.EventShubetsu.HOZON
                    shoriMsg = "保存"
            End Select

            Dim comMsg As String = String.Concat("[横持ちタリフコード = ", Me._Frm.txtYokomochiTariff.TextValue, "]")
            MyBase.ShowMessage(Me._Frm, "G002", New String() {shoriMsg, comMsg})

        End With

    End Sub

    ''' <summary>
    ''' キャッシュ最新化処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetNewCache()

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.YOKO_TARIFF_HD)
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.YOKO_TARIFF_DTL)

    End Sub

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dt As DataTable = Me._InDs.Tables(LMM280C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm.sprYokomochiHed.ActiveSheet

            '検索条件を設定
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.BR_NM.ColNo))
            dr.Item("YOKO_TARIFF_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.YOKOMOCHI_TARIFF_CD.ColNo))
            dr.Item("YOKO_REM") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.BIKO.ColNo))
            dr.Item("CALC_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.KEISANHOHO.ColNo))
            dr.Item("SPLIT_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.MEISAI_BUNKATU.ColNo))
            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.STATUS.ColNo))
            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM280G.sprHedDef.BR_CD.ColNo))

            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim dt As DataTable = Me._InDs.Tables(LMM280C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("YOKO_TARIFF_CD") = .txtYokomochiTariff.TextValue
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

        Dim dt As DataTable = ds.Tables(LMM280C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("YOKO_TARIFF_CD") = .txtYokomochiTariff.TextValue
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
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetSave(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM280C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            '登録項目格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("YOKO_TARIFF_CD") = .txtYokomochiTariff.TextValue
            dr.Item("YOKO_REM") = .txtBiko.TextValue
            dr.Item("CALC_KB") = .cmbKeisanHoho.SelectedValue
            dr.Item("SPLIT_FLG") = .cmbMeisaiBunkatu.SelectedValue
            dr.Item("YOKOMOCHI_MIN") = .numMinHosho.Value

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            '排他処理条件を格納
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            dt.Rows.Add(dr)

            '横持ちタリフ明細内容をDataSet荷格納する
            Call Me.SetDataSetDtlSave(ds)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(保存処理(横持ちタリフ明細))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetDtlSave(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM280C.TABLE_NM_YOKO_DTL)
        Dim max As Integer = Me._Frm.sprYokomochiDtl.ActiveSheet.Rows.Count - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To max

            With Me._Frm.sprYokomochiDtl.ActiveSheet
                Dim edaNo As String = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.YOKO_TARIFF_CD_EDA.ColNo))
                Dim cargoKbn As String = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.NISUGATA_KBN.ColNo))
                Dim carKbn As String = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.SHASHU.ColNo))

                dr = dt.NewRow()
                '枝番設定
                If String.IsNullOrEmpty(edaNo) Then
                    Me._MaxEdaNoSet = Me._MaxEdaNoSet + 1
                    edaNo = Me._MaxEdaNoSet.ToString().PadLeft(3, Convert.ToChar("0"))                    
                End If
                dr.Item("YOKO_TARIFF_CD_EDA") = edaNo
                dr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                dr.Item("YOKO_TARIFF_CD") = Me._Frm.txtYokomochiTariff.TextValue
                Select Case Me._Frm.cmbKeisanHoho.SelectedValue.ToString
                    Case LMM280C.KEISAN_CD_NISUGATA
                        carKbn = "00"
                    Case LMM280C.KEISAN_CD_SHADATE
                        cargoKbn = "00"
                    Case LMM280C.KEISAN_CD_TEIZO_UNCHIN
                        cargoKbn = "00"
                        carKbn = "00"
                    Case LMM280C.KEISAN_CD_JURYO
                        cargoKbn = "00"
                        carKbn = "00"
                End Select
                dr.Item("CARGO_KB") = cargoKbn
                dr.Item("CAR_KB") = carKbn
                dr.Item("WT_LV") = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KUGIRI_JURYO.ColNo))
                dr.Item("DANGER_KB") = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.KIKENHIN.ColNo))
                dr.Item("UT_PRICE") = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.TANKA.ColNo))
                dr.Item("KGS_PRICE") = Me._ControlV.GetCellValue(.Cells(i, LMM280G.sprDtlDef.TANKA_PER_KGS.ColNo))

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'スキーマ名取得用
                'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                dr.Item("USER_BR_CD") = Me._Frm.cmbBr.SelectedValue

                dt.Rows.Add(dr)

            End With
        Next

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
    Friend Sub FunctionKey1Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent(LMM280C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM280F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM280F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Friend Sub LMM280FKeyDown(ByVal frm As LMM280F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM280F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM280F_KeyDown")

    End Sub

    ''' <summary>
    ''' Addボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub AddClick(ByVal frm As LMM280F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "AddClick")

        '行追加処理
        Call Me.RowAdd()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "AddClick")

    End Sub

    ''' <summary>
    ''' Deleteボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DelClick(ByVal frm As LMM280F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelClick")

        '行削除処理
        Call Me.RowDel()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelClick")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprYokomochiHedLeaveCell(ByVal frm As LMM280F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprYokomochiHedLeaveCell")

        Call Me.SprFindLeaveCell(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprYokomochiHedLeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class