' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM150H : 請求テンプレートマスタメンテ
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
'2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
Imports Jp.Co.Nrs.Win.Base
'2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end

''' <summary>
''' LMM150ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM150H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM150V
    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM150G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConV As LMMControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConH As LMMControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMM150F = New LMM150F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sForm, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Gamenクラスの設定
        Me._G = New LMM150G(Me, frm, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM150V(Me, frm, Me._LMMConV)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目ロック
        Call Me._G.LockControl(True)

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM150C.EventShubetsu.MAIN)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM150F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面全ロックの解除
        MyBase.UnLockedControls(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '画面項目のクリア
        Call Me._G.ClearControl()

        '初期設定
        Call Me._G.SetcmbValue()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM150C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM150F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtWhCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM150C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM150DS()
        Call SetDatasetUserItemData(frm, ds)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM150BLF", "HaitaData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._LMMConH.EndAction(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM150C.EventShubetsu.HENSHU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM150F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtWhCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM150C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '編集部の項目複写
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM150C.EventShubetsu.HUKUSHA)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM150F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtWhCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM150C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '2016.01.06 UMANO 英語化対応START
        Dim str As String() = Split(frm.FunctionKey.F4ButtonName, "･")
        '2016.01.06 UMANO 英語化対応END

        '削除フラグチェック
        If frm.lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) Then
            '2016.01.06 UMANO 英語化対応START
            'Select Case MyBase.ShowMessage(frm, "C001", New String() {"削除"})
            Select MyBase.ShowMessage(frm, "C001", New String() {Str(0)})
                '2016.01.06 UMANO 英語化対応END
                Case MsgBoxResult.Cancel '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        Else
            '2016.01.06 UMANO 英語化対応START
            'Select Case MyBase.ShowMessage(frm, "C001", New String() {"復活"})
            Select MyBase.ShowMessage(frm, "C001", New String() {str(1)})
                '2016.01.06 UMANO 英語化対応END
                Case MsgBoxResult.Cancel  '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM150DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM150BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SOKO)

        'メッセージ用請求先コード格納
        Dim WhCd As String = frm.txtWhCd.TextValue
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
        '                                      , String.Concat("[", LMM150C.SOKOMSG, " = ", WhCd, "]")})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName _
                                              , String.Concat("[", frm.lblTitleWh.Text(), " = ", WhCd, "]")})
        '2016.01.06 UMANO 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM150F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.KENSAKU) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '編集部クリアフラグ
        Dim clearFlg As Integer = 0

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                MyBase.ShowMessage(frm, "G003")
                Exit Sub
            Else      'OK押下
                clearFlg = 1
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm, clearFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM150C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()
    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM150F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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

        Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal frm As LMM150F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(frm, e.Row)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM150F, ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM150G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(LMM150C.EventShubetsu.SANSHO, frm.lblSituation.RecordStatus)

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMM150F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM150C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM150C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM150C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveItemData(ByVal frm As LMM150F, ByVal eventShubetsu As LMM150C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM150C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        '項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM150DS()
        Call Me.SetDatasetUserItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = Me.SaveWSAAction(frm, "LMM150BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = Me.SaveWSAAction(frm, "LMM150BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            '2011.09.08 検証結果_導入時要望№1対応 START
            If MyBase.GetMessageID().Equals("E079") = False Then
                MyBase.ShowMessage(frm)

                frm.txtWhCd.Focus()

                Call Me._LMMConH.EndAction(frm)  '終了処理

                Return False

                '    Dim Zip As String = frm.txtZip.TextValue
                '    MyBase.ShowMessage(frm, "E079", New String() {"郵便番号マスタ", Zip})
                '    Call Me._LMMConV.SetErrorControl(frm.txtZip)
                '    frm.txtZip.Focus()
                '    Call Me._LMMConH.EndAction(frm)  '終了処理
                '    Return False                
            End If

            '2011.09.08 検証結果_導入時要望№1対応 END
        End If

        'ワーニングキャンセル押下時
        If rtnDs Is Nothing = True Then

            Call Me._LMMConH.EndAction(frm)  '終了処理

            Return False

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SOKO)

        '処理結果メッセージ表示
        Dim whCd As String = frm.txtWhCd.TextValue
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
        '                                              , String.Concat("[", LMM150C.SOKOMSG, " = ", whCd, "]")})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                              , String.Concat("[", frm.lblTitleWh.Text(), " = ", whCd, "]")})
        '2016.01.06 UMANO 英語化対応END

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM150C.EventShubetsu.MAIN)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal frm As LMM150F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveItemData(frm, LMM150C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub


    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM150C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM150C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM150C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM150F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM150DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM150BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, _
        '                                         "LMM150BLF", "SelectListData", _FindDs _
        '                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
        '                                  , , Convert.ToInt32(Convert.ToDouble( _
        '                                     MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                     .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'ステータスの設定
        If clearflg = 1 Then
            Call Me._G.SetControlsStatus()
        End If

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMM150F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM150C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()
        '0件でないとき
        If Me._CntSelect.Equals(LMConst.FLG.OFF) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ステータスの設定
        Call Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' WSAクラス呼出(保存時)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="blf">BLFファイル名</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <remarks></remarks>
    Friend Function SaveWSAAction(ByVal frm As LMM150F _
                                  , ByVal blf As String _
                                  , ByVal actionId As String _
                                  , ByVal rtDs As DataSet _
                                  ) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, actionId, rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                Select Case MyBase.GetMessageID()

                    Case "W176"
                        'メッセージを表示し、戻り値により処理を分ける
                        '2016.01.06 UMANO 英語化対応START
                        'Select Case MyBase.ShowMessage(frm)
                        Select Case MyBase.ShowMessage(frm, "W176", New String() {frm.lblTitleZip.Text(), frm.lblTitleJis.Text()})
                            '2016.01.06 UMANO 英語化対応END
                            Case MsgBoxResult.Ok '「OK」押下時

                                '強制実行フラグの設定
                                MyBase.SetForceOparation(True)

                                'WSA呼出し
                                rtnDs = MyBase.CallWSA(blf, actionId, rtDs)

                                '成功時
                                Return rtnDs

                            Case MsgBoxResult.Cancel '「キャンセル」押下時

                                'メッセージエリアの設定
                                MyBase.ShowMessage(frm, "G003")

                                '画面解除
                                MyBase.UnLockedControls(frm)

                                Return Nothing

                        End Select

                    Case "W129"
                        'メッセージを表示し、戻り値により処理を分ける
                        '2016.01.06 UMANO 英語化対応START
                        'Select Case MyBase.ShowMessage(frm)
                        Select Case MyBase.ShowMessage(frm, "W129", New String() {frm.lblTitleJis.Text(), frm.lblTitleZip.Text()})
                            '2016.01.06 UMANO 英語化対応END
                            Case MsgBoxResult.Yes   '「はい」を選択：郵便番号マスタのJISコードで登録

                                '強制実行フラグの設定
                                MyBase.SetForceOparation(True)

                                '郵便番号マスタに登録されているJISコードで保存する
                                Dim dr As DataRow = rtnDs.Tables("LMM150JIS").Rows(0)
                                rtDs.Tables("LMM150IN").Rows(0).Item("JIS_CD") = dr.Item("JIS_CD")
                                'WSA呼出し
                                rtnDs = MyBase.CallWSA(blf, actionId, rtDs)

                                '成功時
                                Return rtnDs

                            Case MsgBoxResult.No   '「いいえ」を選択：画面のJISコードのまま登録

                                '強制実行フラグの設定
                                MyBase.SetForceOparation(True)

                                'WSA呼出し
                                rtnDs = MyBase.CallWSA(blf, actionId, rtDs)

                                '成功時
                                Return rtnDs

                            Case MsgBoxResult.Cancel   '「キャンセル」を選択：画面入力に戻る

                                'メッセージエリアの設定
                                MyBase.ShowMessage(frm, "G003")

                                '画面解除
                                MyBase.UnLockedControls(frm)

                                Return Nothing

                        End Select

                End Select

            Else      'Errorの場合

                ''メッセージエリアの設定
                'MyBase.ShowMessage(frm)

                '画面解除
                MyBase.UnLockedControls(frm)

                Return rtnDs

            End If

        Else

            '検索成功時
            Return rtnDs

        End If

        Return rtnDs

    End Function

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM150F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMM150F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM150F)

        Dim dt As DataTable = Me._FindDs.Tables(LMM150C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM150G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM150G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("WH_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM150G.sprDetailDef.WH_NM.ColNo))
            dr("TEL") = Me._LMMConV.GetCellValue(.Cells(0, LMM150G.sprDetailDef.TEL.ColNo))
            dr("FAX") = Me._LMMConV.GetCellValue(.Cells(0, LMM150G.sprDetailDef.FAX.ColNo))

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd
            dr("USER_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM150G.sprDetailDef.NRS_BR_CD.ColNo))

            dt.Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUserItemData(ByVal frm As LMM150F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM150C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("WH_CD") = .txtWhCd.TextValue
            dr("WH_NM") = .txtWhNm.TextValue
            dr("ZIP") = .txtZip.TextValue
            dr("AD_1") = .txtAd1.TextValue
            dr("AD_2") = .txtAd2.TextValue
            dr("AD_3") = .txtAd3.TextValue
            dr("WH_KB") = .cmbJtsFlg.SelectedValue
            dr("TEL") = .txtTel.TextValue
            dr("FAX") = .txtFax.TextValue
            dr("JIS_CD") = .txtJis.TextValue
            dr("NIHUDA_MX_CNT") = .numNihudaMxCnt.Value
            dr("INKA_YOTEI_YN") = .cmbInkaYotei.SelectedValue
            dr("INKA_UKE_PRT_YN") = .cmbInkaUkePrt.SelectedValue
            dr("INKA_KENPIN_YN") = .cmbInkaKenpin.SelectedValue
            dr("INKA_KAKUNIN_YN") = .cmbInkaKakunin.SelectedValue
            dr("INKA_INFO_YN") = .cmbInkaInfo.SelectedValue
            'START YANAI 要望番号394
            dr("OUTKA_YOTEI_YN") = .cmbOutkaYotei.SelectedValue
            'END YANAI 要望番号394
            dr("OUTKA_SASHIZU_PRT_YN") = .cmbOutkaSashizuPrt.SelectedValue
            dr("OUTOKA_KANRYO_YN") = .cmbOutkaKanryo.SelectedValue
            dr("OUTKA_KENPIN_YN") = .cmbOutkaKenpin.SelectedValue
            dr("OUTKA_INFO_YN") = .cmbOutkaInfo.SelectedValue
            dr("LOC_MANAGER_YN") = .cmbLocManager.SelectedValue
            dr("TOU_KANRI_YN") = .cmbTouKanri.SelectedValue
            dr("TOUHAN_SASHIZU_YN") = .cmbTouhanSashizu.SelectedValue
            'START KIM 2012/09/12 要望番号1404 
            dr("GOODSLOT_CHECK_YN") = .cmbGoodslotCheckYN.SelectedValue
            'END KIM 2012/09/12 要望番号1404 
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add start
            '言語区分（"0":日本語  "1":英語  "2"：韓国語  "3"：中国語）
            dr("LANG_KBN") = MessageManager.MessageLanguage
            '2018/03/28 001035 【LMS】倉庫マスタ_追加しても表示されない(SIN岩下)対応 Annen add end
        End With

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM150F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM150C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr("WH_CD") = .txtWhCd.TextValue
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd
            dr("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        dt.Rows.Add(dr)

    End Sub

#End Region

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMM150F, ByVal objNm As String, ByVal eventshubetsu As LMM150C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            Select Case objNm

                Case .txtZip.Name

                    Call Me.SetReturnZipPop(frm, objNm, eventshubetsu)

                Case .txtJis.Name

                    Call Me.SetReturnJisPop(frm, objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 郵便番号マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowZipPopup(ByVal frm As LMM150F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM150C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ060DS()
        Dim dt As DataTable = ds.Tables(LMZ060C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM150C.EventShubetsu.ENTER Then
                .Item("ZIP_NO") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ060", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 郵便番号Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnZipPop(ByVal frm As LMM150F, ByVal objNm As String, ByVal eventshubetsu As LMM150C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowZipPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ060C.TABLE_NM_OUT).Rows(0)

            '住所1(都道府県+市区町村名)
            Dim add1 As String = String.Concat(dr.Item("KEN_N").ToString(), dr.Item("CITY_N").ToString())

            ctl.TextValue = dr.Item("ZIP_NO").ToString()         '郵便番号

            If String.IsNullOrEmpty(frm.txtAd1.TextValue) _
            AndAlso String.IsNullOrEmpty(frm.txtAd2.TextValue) _
            AndAlso String.IsNullOrEmpty(frm.txtJis.TextValue) Then

                frm.txtAd1.TextValue = add1
                frm.txtAd2.TextValue = dr.Item("TOWN_N").ToString()    '住所2(町域名)
                frm.txtJis.TextValue = dr.Item("JIS_CD").ToString()
                frm.lblKen.TextValue = dr.Item("KEN_N").ToString()
                frm.lblShi.TextValue = dr.Item("CITY_N").ToString()

            End If

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' JISマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowJisPopup(ByVal frm As LMM150F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM150C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ070DS()
        Dim dt As DataTable = ds.Tables(LMZ070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM150C.EventShubetsu.ENTER Then
                .Item("JIS_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ070", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' JISPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnJisPop(ByVal frm As LMM150F, ByVal objNm As String, ByVal eventshubetsu As LMM150C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowJisPopup(frm, ctl, eventshubetsu)

        Dim item As String = String.Empty
        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)

            ctl.TextValue = dr.Item("JIS_CD").ToString()

            frm.lblKen.TextValue = dr.Item("KEN").ToString()
            frm.lblShi.TextValue = dr.Item("SHI").ToString()

            Return True

        End If

        Return False

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.NewDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveItemData")

        Me.SaveItemData(frm, LMM150C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM150F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM150F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM150F_KeyDown(ByVal frm As LMM150F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM150F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM150F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM150F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class