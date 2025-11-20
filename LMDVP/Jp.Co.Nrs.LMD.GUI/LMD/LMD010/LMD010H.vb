' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD010H : 在庫振替入力
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMD010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD010G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConH As LMDControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConG As LMDControlG

    ''' <summary>
    '''引当済みかどうか判定するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _HikiateFlg As String

    ''' <summary>
    ''' PopUp画面表示フラグを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private _Frm As LMD010F
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑


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
        Dim frm As LMD010F = New LMD010F(Me)

        'Validate共通クラスの設定
        Me._LMDConV = New LMDControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMDConH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMDConG = New LMDControlG(Me, DirectCast(frm, Form))

        'Gamenクラスの設定
        Me._G = New LMD010G(Me, frm, Me._LMDConG)

        'Validateクラスの設定
        Me._V = New LMD010V(Me, frm)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbNrsBrCd, frm.cmbSoko)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目ロック
        Dim lock As Boolean = True
        Dim unLock As Boolean = False
        Call Me._LMDConG.SetLockControl(frm, lock)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.HENSHU)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.HENSHU)

        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        _Frm = frm
        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.MAIN)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 複写処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditCopyEvent(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMD010C.ActionType.FUKUSHA) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())  '終了処理
            Exit Sub
        End If

        '複写項目を除いて初期化
        Call Me._G.ClearControlFukusha()

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.FUKUSHA)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.FUKUSHA)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.FUKUSHA)

        '引当実行フラグを元に戻す
        _HikiateFlg = LMConst.FLG.OFF

    End Sub

    ''' <summary>
    ''' 引当処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenHikiatePop(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMD010C.ActionType.HIKIATE) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())  '終了処理
            Exit Sub
        End If

        '項目チェック
        Dim hikiZenFlg As Integer = 0
        If Me._V.IsFunctionInputChk(hikiZenFlg) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '在庫引当画面呼び出し
        Call SetReturnHikiatePop(frm, hikiZenFlg)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.HIKIATE)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.HIKIATE)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.HIKIATE)

        '出荷管理番号の採番
        Me.sprAddNewNo(frm.spdDtl)

    End Sub

    ''' <summary>
    ''' 全量処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OpenZenryoPop(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMD010C.ActionType.HIKIATE) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())  '終了処理
            Exit Sub
        End If

        '項目チェック
        Dim hikiZenFlg As Integer = 1
        If Me._V.IsFunctionInputChk(hikiZenFlg) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '在庫引当画面呼び出し
        Call Me.SetReturnHikiatePop(frm, hikiZenFlg)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.ZENRYO)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.ZENRYO)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.ZENRYO)

        '出荷管理番号の採番
        Me.sprAddNewNo(frm.spdDtl)

    End Sub

#If True Then       'ADD 2018/12/20 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい

    ''' <summary>
    ''' 再印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SaiPrintINIT(ByVal frm As LMD010F)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.MASTER)

        '処理開始アクション
        'Call Me._LMDConH.StartAction(frm)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.PRINT_INIT)


        frm.lblFurikaeNo.Enabled = True
        frm.lblFurikaeNo.ReadOnly = False

        frm.FunctionKey.F1ButtonName = "印　刷"

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.PRINT_INIT)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G014", New String() {"振替管理番号"})

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.PRINT_INIT)

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SaiPrintRTN(ByVal frm As LMD010F)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.MASTER)

        If PrintEvent(frm) = False Then      'ADD 2018/12/20
            If IsMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            End If

            Exit Sub

        End If

        frm.lblFurikaeNo.Enabled = True
        frm.lblFurikaeNo.ReadOnly = True

        frm.FunctionKey.F1ButtonName = "再印刷"
        frm.lblFurikaeNo.TextValue = String.Empty


        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.HENSHU)


        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

    End Sub
#End If

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMD010F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.MASTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD010C.ActionType.MASTER)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '項目チェック：１件時表示あり
        Me._PopupSkipFlg = True
        Me.ShowPopupControl(frm, objNm, LMD010C.ActionType.MASTER)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

    End Sub

    ''' <summary>
    ''' 行削除(振替元)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowsMoto(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.COLDEL)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMDConH.GetCheckList(frm.spdDtl.ActiveSheet, LMD010G.sprDetailDef.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsSelectChk(arr.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        For i As Integer = arr.Count - 1 To 0 Step -1

            '選択された行を物理削除
            frm.spdDtl.ActiveSheet.Rows(Convert.ToInt32(arr(i))).Remove()

        Next

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.HIKIATE)

        '振替数量、振替個数の再計算を行う
        Call Me.CalcHikiSumiCnt(frm, 0)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        '出荷管理番号の採番
        Me.sprAddNewNo(frm.spdDtl)

    End Sub

    ''' <summary>
    ''' 行追加(振替先)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub AddRowsSaki(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.COLADDNEW)

        '振替元のスプレッドチェック状態を取得
        Dim arrOld As ArrayList = Me._LMDConH.GetList(frm.spdDtl.ActiveSheet, LMD010G.sprDetailDef.DEF.ColNo)
        '振替先のスプレッドチェック状態を取得
        Dim arrNew As ArrayList = Me._LMDConH.GetList(frm.sprDtlNew.ActiveSheet, LMD010G.sprDetailNewDef.DEF.ColNo)

        '行追加(振替先)時項目チェック
        rtnResult = rtnResult AndAlso Me._V.IsAddFurikaesakiChk(arrNew, arrOld)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        Dim calcSuryo As Decimal = 0
        Dim calcKosu As Decimal = 0

        '行追加時に計算する数量の取得
        calcSuryo = Me.rowAddCalc(frm, 0)

        '行追加時に計算する個数の取得
        calcKosu = Me.rowAddCalc(frm, 1)

        '振替先のスプレッドにコピー
        Dim arr As ArrayList = New ArrayList()
        arr.Add(0)
        Me._G.SetSakiSpread(arr, LMConst.FLG.ON, calcSuryo, calcKosu)

        '入荷管理番号の採番
        Me.sprAddNewNo(frm.sprDtlNew)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        '画面項目のロック制御
        Me._G.SetFurikaeSakiLock()

    End Sub

    ''' <summary>
    ''' 行削除(振替先)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowsSaki(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.COLDELNEW)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMDConH.GetCheckList(frm.sprDtlNew.ActiveSheet, LMD010G.sprDetailNewDef.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsSelectChk(arr.Count)

        '全削除チェック
        rtnResult = rtnResult AndAlso Me._V.IsAllDeleteChk(arr.Count, frm.sprDtlNew.ActiveSheet.Rows.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        For i As Integer = arr.Count - 1 To 0 Step -1

            '選択された行を物理削除
            frm.sprDtlNew.ActiveSheet.Rows(Convert.ToInt32(arr(i))).Remove()

        Next

        '入荷管理番号の採番
        Me.sprAddNewNo(frm.sprDtlNew)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        '画面項目のロック制御
        Me._G.SetFurikaeSakiLock()

    End Sub

    ''' <summary>
    ''' 振替元確定処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FurikaeMotoKakuteiEvent(ByVal frm As LMD010F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.FURIKAEMOTOKAKUTEI)

        '単項目チェック
        rtnResult = rtnResult AndAlso Me._V.IsEditMotoInputChk()

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '★★★保管・荷役料最終計算日大小チェック
        Dim chkDs As DataSet = Me.SetDataSetSeiqData(frm, "MOTO")
        chkDs = MyBase.CallWSA("LMD000BLF", "SelectChkIdoDate", chkDs)

        If MyBase.IsMessageExist() = True Then
            '処理終了アクション
            Me.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, "") '終了処理
            Exit Sub
        End If
        '★★★

        'ヘッダー情報、振替元ヘッダー、振替元一覧情報のコピー
        Dim arr As ArrayList = Nothing

        '振替元のスプレッドチェック状態を取得
        arr = Me._LMDConH.GetList(frm.spdDtl.ActiveSheet, LMD010G.sprDetailDef.DEF.ColNo)

        Dim chkDateDs As DataSet = New LMD010DS()

        '請求日検索条件のデータセットを行う
        Call Me.SetDataSetMotoSeiqData(frm, chkDateDs)

        '移動日検索条件のデータセットを行う
        Call Me.SetDataSetIdoData(frm, chkDateDs, arr)

        '作業データ取得
        chkDateDs = Me.SetSagyoInForDateChk(frm, chkDateDs, LMConst.FLG.OFF)

        '移動日を取得
        chkDateDs = MyBase.CallWSA("LMD010BLF", "SelectChkIdoDate", chkDateDs)

        '入力チェック、関連チェック
        rtnResult = rtnResult AndAlso Me._V.IsFurikaeInputChk(arr, chkDateDs)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '振替先のスプレッドにコピー
        '要望番号1313 容器変更にチェックを入れたときも、振替先データをスプレッドに表示 START
        'If frm.chkYoukiChange.GetBinaryValue().Equals(LMConst.FLG.OFF) Then
        '    Me._G.SetSakiSpread(arr, LMConst.FLG.OFF, 0, 0)
        'End If

        Me._G.SetSakiSpread(arr, LMConst.FLG.OFF, 0, 0)

        '要望番号1313 容器変更にチェックを入れたときも、振替先データをスプレッドに表示 END

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        '入荷管理番号の採番
        Me.sprAddNewNo(frm.sprDtlNew)

        'キャッシュから名称取得
        Call SetCachedName(frm, LMD010C.ActionType.FURIKAEMOTOKAKUTEI)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.FURIKAEMOTOKAKUTEI)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.FURIKAEMOTOKAKUTEI)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.FURIKAEMOTOKAKUTEI)

    End Sub

    ''' <summary>
    ''' 振替確定処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function FurikaeKakuteiEvent(ByVal frm As LMD010F) As Boolean

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.FURIKAEKAKUTEI)

        '全行計算処理
        rtnResult = rtnResult AndAlso Me.AllCalculation(frm)

        Dim chkDateDs As DataSet = New LMD010DS()

        '振替先のチェックボックス一覧
        Dim arrNew As ArrayList = Nothing

        '振替先のチェックボックス一覧
        Dim arrOld As ArrayList = Nothing

        '振替元のスプレッドチェック状態を取得
        arrOld = Me._LMDConH.GetList(frm.spdDtl.ActiveSheet, LMD010G.sprDetailDef.DEF.ColNo)

        '振替先のスプレッドチェック状態を取得
        arrNew = Me._LMDConH.GetList(frm.sprDtlNew.ActiveSheet, LMD010G.sprDetailNewDef.DEF.ColNo)

        '請求日検索条件のデータセットを行う
        Call Me.SetDataSetMotoSeiqData(frm, chkDateDs)

        '作業データ取得
        chkDateDs = Me.SetSagyoInForDateChk(frm, chkDateDs, LMConst.FLG.ON)

        '移動日を取得
        chkDateDs = MyBase.CallWSA("LMD010BLF", "SelectChkIdoDate", chkDateDs)

        '入力チェック、関連チェック
        rtnResult = rtnResult AndAlso Me._V.IsFurikaeKakuteiInputChk(arrNew, arrOld, chkDateDs)

        '棟 + 室 + ZONE（置き場情報）温度管理チェック
        rtnResult = rtnResult AndAlso Me._V.IsOndoCheck(arrNew)

        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        '申請外の商品保管ルール情報取得
        Dim expDs As DataSet = New LMD010DS()
        Dim inRow As DataRow = expDs.Tables("LMD010IN").NewRow
        inRow.Item("NRS_BR_CD") = _Frm.cmbNrsBrCd.SelectedValue
        inRow.Item("WH_CD") = _Frm.cmbSoko.SelectedValue
        inRow.Item("SAKI_CUST_CD_L") = _Frm.txtCustCdLNew.TextValue
        inRow.Item("TRANSFER_DATE") = _Frm.imdFurikaeDate.TextValue
        expDs.Tables("LMD010IN").Rows.Add(inRow)
        expDs = MyBase.CallWSA("LMD010BLF", "getTouSituExp", expDs)

        '依頼番号:013987 棟室マスタ、ZONEマスタチェック処理改修
        'rtnResult = rtnResult AndAlso Me._V.IsDangerousGoodsCheck(frm, arrNew, LMConst.FLG.OFF, expDs)
        '新規入荷チェック
        rtnResult = rtnResult AndAlso Me.IsTouSituZoneCheck(frm, arrNew, LMConst.FLG.OFF, expDs)
        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If

        '★★★保管・荷役料最終計算日大小チェック
        Dim chkDs As DataSet = Me.SetDataSetSeiqData(frm, "SAKI")
        chkDs = MyBase.CallWSA("LMD000BLF", "SelectChkIdoDate", chkDs)

        If MyBase.IsMessageExist() = True Then
            '処理終了アクション
            Call Me.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, "") '終了処理
            Exit Function
        End If
        '★★★

        ' 振替先 キープ品列表示制御
        Dim isVisibleKeepGoods As Boolean = IsBykKeepGoodsCd()
        frm.sprDtlNew.ActiveSheet.Columns(LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo).Visible = isVisibleKeepGoods

        'コード値をキャッシュより再取得
        Me.SetDataSetInCd(frm)

        '振替データ作成のデータ取得
        'DataSet設定
        Dim furikaeKakuteDs As DataSet = New LMD010DS()

        '全てのデータセットを行うクラスを呼び出す
        Call Me.SetDatasetAll(frm, furikaeKakuteDs, arrNew, arrOld)

        '==========================
        'WSAクラス呼出
        '========================== 
        furikaeKakuteDs = MyBase.CallWSA("LMD010BLF", "InsertSaveAction", furikaeKakuteDs)

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())  '終了処理
            Return False

        End If

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'キャッシュから名称取得
        Call SetCachedName(frm, LMD010C.ActionType.FURIKAEKAKUTEI)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD010C.ActionType.FURIKAEKAKUTEI)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.FURIKAEKAKUTEI)

        Dim drInkaL As DataRow = furikaeKakuteDs.Tables(LMD010C.TABLE_NM_INKA_L)(0)

        Dim furikaeNo As String = String.Empty

        furikaeNo = drInkaL.Item("FURI_NO").ToString()

        frm.lblFurikaeNo.TextValue = furikaeNo

        '処理終了メッセージの表示
        '2015.10.22 tusnehira add
        '英語化対応
        MyBase.ShowMessage(frm, "G075", New String() {String.Concat("[", frm.lblTitleFurikaeKnariNo.Text, " = ", furikaeNo, "]")})
        'MyBase.ShowMessage(frm, "G002", New String() {"振替確定", String.Concat("[", frm.lblTitleFurikaeKnariNo.Text, " = ", furikaeNo, "]")})

        '印刷処理（振替伝票）
        Dim rtDs As DataSet = Me.SetLMD600InDataSet(frm)
        rtDs.Merge(New RdPrevInfoDS)
        Dim lmd600Ds As DataSet = MyBase.CallWSA("LMD010BLF", "PrintAction", rtDs)

        If IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If

        'プレビュー判定 
        Dim prevDt As DataTable = lmd600Ds.Tables(LMConst.RD)
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

        Return True

    End Function

#If True Then   'ADD 2018/12/20 依頼番号 : 003904   【LMS】在庫振替入力時の印刷プレビューを再度閲覧したい
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintEvent(ByVal frm As LMD010F) As Boolean

        ' ''処理開始アクション
        ''Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD010C.ActionType.FURIKAEKAKUTEI)

        '単項目チェック
        rtnResult = Me._V.IsPrintInputChk()

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If

        '印刷処理）
        Dim rtDs As DataSet = Me.SetLMD600InDataSet(frm, "Print")

        'SelecFurikaebiGet
        Dim lmd600Ds As DataSet = MyBase.CallWSA("LMD010BLF", "SelecFurikaebiGet", rtDs)

        Dim cntGet As Integer = lmd600Ds.Tables("LMD600_FURIKAE").Rows.Count

        If cntGet = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("E483", New String() {"振替管理番号では"})
            Return False

        Else
            Dim furikaebi As String = lmd600Ds.Tables("LMD600_FURIKAE").Rows(0).Item("FURIKAEBI").ToString

            rtDs = Me.SetLMD600InDataSet(frm, "Print")

            rtDs.Tables("LMD600IN").Rows(0).Item("FURIKAEBI") = furikaebi
        End If

        rtDs.Merge(New RdPrevInfoDS)
        lmd600Ds = MyBase.CallWSA("LMD010BLF", "PrintAction", rtDs)

        If IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If

        'プレビュー判定 
        Dim prevDt As DataTable = lmd600Ds.Tables(LMConst.RD)
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

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(LMD010C.ActionType.HENSHU)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD010C.ActionType.MAIN)

        Return True

    End Function

#End If

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Function CloseFormEvent(ByVal frm As LMD010F, ByVal e As FormClosingEventArgs) As Boolean



        'ディスプレイモードが編集の場合
        If RecordStatus.NEW_REC.Equals(frm.lblSituation.RecordStatus) = True Then
            'メッセージ表示
            Select Case MyBase.ShowMessage(frm, "W002")
                Case MsgBoxResult.Yes  '「はい」押下時
                    If Me.FurikaeKakuteiEvent(frm) = True Then
                        Return True
                    Else '保存失敗時
                        e.Cancel = True
                        Return False
                    End If

                Case MsgBoxResult.No   '「いいえ」押下時
                    Return True
                Case Else                   '「キャンセル」押下時
                    e.Cancel = True
                    Return False
            End Select

        End If

        Return True

    End Function

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Call Me.EnterAction(frm, e.KeyCode = Keys.Enter)

    End Sub

    ''' <summary>
    ''' スプレッドのチェンジイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprChangeAction(ByVal frm As LMD010F, ByVal e As FarPoint.Win.Spread.ChangeEventArgs)

        '1行目は
        Dim rowNo As Integer = e.Row
        If rowNo < 0 Then
            Exit Sub
        End If

        '個数、入目以外、スルー
        Select Case e.Column

            Case LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo _
                , LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo
            Case Else
                Exit Sub

        End Select

        '計算処理
        Call Me.Calculation(frm, rowNo)

    End Sub

    ''' <summary>
    ''' 戻る処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CancelAction(ByVal frm As LMD010F)

        Call Me._G.SetCancelControl()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "ユーティリティ"

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">処理を行う場合 True</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD010F, ByVal eventFlg As Boolean)

        '参照の場合、Tab移動して終了
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()


        '権限チェック
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD010C.ActionType.ENTER)

        'Popを表示するかを判定
        rtnResult = rtnResult AndAlso Me.ChkOpenEnterAction(frm, objNm)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '項目チェック：１件時表示なし
        Me._PopupSkipFlg = False
        Me.ShowPopupControl(frm, objNm, LMD010C.ActionType.ENTER)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' Enter処理の特殊フォーカス移動
    ''' </summary>
    ''' <param name="eventFlg">Enterの場合、True</param>
    ''' <remarks></remarks>
    Private Sub NextFocusedControl(ByVal frm As LMD010F, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' Enter処理時にPopを表示するかを判定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:参照する False:参照しない</returns>
    ''' <remarks></remarks>
    Private Function ChkOpenEnterAction(ByVal frm As LMD010F, ByVal objNm As String) As Boolean

        With frm

            Select Case objNm

                Case .spdDtl.Name

                    Return False

                Case .sprDtlNew.Name

                    Return False

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        Return False
                    End If

                    Return True

                Case .txtGoodsCdCust.Name, .txtGoodsNmCust.Name

                    If String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtGoodsNmCust.TextValue) = True Then
                        Return False
                    End If

                    Return True

                Case .txtCustCdLNew.Name, .txtCustCdMNew.Name

                    If String.IsNullOrEmpty(.txtCustCdLNew.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtCustCdMNew.TextValue) = True Then
                        .lblGoodsCdNrsNew.TextValue = String.Empty
                        Return False
                    End If

                    Return True

                Case .txtGoodsCdCustNew.Name, .txtGoodsNmCustNew.Name

                    If String.IsNullOrEmpty(.txtGoodsCdCustNew.TextValue) = True _
                       AndAlso String.IsNullOrEmpty(.txtGoodsNmCustNew.TextValue) = True Then
                        .lblGoodsCdNrsNew.TextValue = String.Empty
                        Return False
                    End If

                    Return True

            End Select

            Return Not String.IsNullOrEmpty(DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox).TextValue)

        End With


    End Function

    ''' <summary>
    ''' スプレッドの入荷、出荷番号の採番メソッド
    ''' </summary>
    ''' <param name="spr">対象スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub sprAddNewNo(ByVal spr As Win.Spread.LMSpread)

        Dim countUp As Integer = 1

        For i As Integer = 0 To spr.ActiveSheet.Rows.Count - 1

            spr.SetCellValue(i, LMD010G.sprDetailNewDef.SAKI_INKA_NO_S.ColNo, Format(countUp, "000"))

            countUp += 1

        Next


    End Sub

    ''' <summary>
    ''' 行追加時の計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function rowAddCalc(ByVal frm As LMD010F, ByVal flg As Integer) As Decimal


        '返却する変数
        Dim rtnValue As Decimal = 0
        Dim motoZaiRecNo As String = String.Empty
        Dim sakiZaiRecNo As String = String.Empty

        motoZaiRecNo = Me._LMDConV.GetCellValue(frm.spdDtl.ActiveSheet.Cells(0, LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo)).ToString()

        'スプレッドシートのチェックの分だけ検索条件をセット
        For i As Integer = 0 To frm.sprDtlNew.ActiveSheet.Rows.Count - 1

            sakiZaiRecNo = Me._LMDConV.GetCellValue(frm.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_ZAI_REC_NO.ColNo))

            '在庫レコード番号が合致するかチェック
            If motoZaiRecNo.Equals(sakiZaiRecNo) = True Then

                'フラグによって、数量、個数の計算を可変させる
                If flg = 0 Then
                    '数量の計算
                    rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo)))
                Else
                    '個数の計算
                    rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)))
                End If


            End If


        Next

        Return rtnValue

    End Function

    ''' <summary>
    ''' ガイダンスメッセージIDを返却
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String

        Return "G003"

    End Function

    ''' <summary>
    ''' コード値をキャッシュより再取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetInCd(ByVal frm As LMD010F) As Boolean

        '荷主コード再設定
        Dim rtnResult As Boolean = Me.SetCustCd(frm)

        '商品コード再設定
        rtnResult = rtnResult AndAlso Me.SetGoodsCd(frm)

        '棟・室・ゾーン再設定
        rtnResult = rtnResult AndAlso Me.SetTouCd(frm)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主コード再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetCustCd(ByVal frm As LMD010F) As Boolean

        With frm
            Dim custDr As DataRow() = Nothing
            '振替元
            custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                             "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND ", _
                                                                                             "CUST_CD_S = '00' AND ", _
                                                                                             "CUST_CD_SS = '00' AND ", _
                                                                                             "SYS_DEL_FLG = '0'"))
            If 0 < custDr.Length Then
                .txtCustCdL.TextValue = custDr(0).Item("CUST_CD_L").ToString
                .txtCustCdM.TextValue = custDr(0).Item("CUST_CD_M").ToString
            End If

            '振替先
            custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdLNew.TextValue, "' AND ", _
                                                                                             "CUST_CD_M = '", .txtCustCdMNew.TextValue, "' AND ", _
                                                                                             "CUST_CD_S = '00' AND ", _
                                                                                             "CUST_CD_SS = '00' AND ", _
                                                                                             "SYS_DEL_FLG = '0'"))
            If 0 < custDr.Length Then
                .txtCustCdLNew.TextValue = custDr(0).Item("CUST_CD_L").ToString
                .txtCustCdMNew.TextValue = custDr(0).Item("CUST_CD_M").ToString
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 商品コード再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetGoodsCd(ByVal frm As LMD010F) As Boolean

        With frm
            Dim custDr As DataRow() = Nothing
            '振替元
            '---↓
            'custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = '", .lblGoodsCdNrs.TextValue, "' AND ", _
            '                                                                                  "SYS_DEL_FLG = '0'"))

            Dim goodsDs As MGoodsDS = New MGoodsDS
            Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
            goodsDr.Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
            goodsDr.Item("SYS_DEL_FLG") = "0"
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
            goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
#End If
            goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
            Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
            custDr = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
            '---↑

            If 0 < custDr.Length Then
                .txtGoodsCdCust.TextValue = custDr(0).Item("GOODS_CD_CUST").ToString
            End If

            '振替先
            '---↓
            'custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = '", .lblGoodsCdNrsNew.TextValue, "' AND ", _
            '                                                                                  "SYS_DEL_FLG = '0'"))

            goodsDs.Clear()
            goodsDr = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
            goodsDr.Item("GOODS_CD_NRS") = .lblGoodsCdNrsNew.TextValue
            goodsDr.Item("SYS_DEL_FLG") = "0"
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
            goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
#End If

            goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
            rtnDs = MyBase.GetGoodsMasterData(goodsDs)
            custDr = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
            '---↑

            If 0 < custDr.Length Then
                .txtGoodsCdCustNew.TextValue = custDr(0).Item("GOODS_CD_CUST").ToString
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟・室・ゾーン再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTouCd(ByVal frm As LMD010F) As Boolean

        With frm
            Dim touDr As DataRow() = Nothing
            Dim max As Integer = .sprDtlNew.ActiveSheet.Rows.Count - 1
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty

            For i As Integer = 0 To max
                touNo = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo))
                situNo = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo))
                zoneCd = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo))

                If String.IsNullOrEmpty(touNo) = False AndAlso _
                    String.IsNullOrEmpty(situNo) = False AndAlso _
                    String.IsNullOrEmpty(zoneCd) = False Then
                    touDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(String.Concat("WH_CD = '", Convert.ToString(.cmbSoko.SelectedValue), "' AND ", _
                                                                                                             "TOU_NO = '", touNo, "' AND ", _
                                                                                                             "SITU_NO = '", situNo, "' AND ", _
                                                                                                             "ZONE_CD = '", zoneCd, "'"))

                    If 0 < touDr.Length Then
                        .sprDtlNew.SetCellValue(i, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo, touDr(0).Item("TOU_NO").ToString())
                        .sprDtlNew.SetCellValue(i, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, touDr(0).Item("SITU_NO").ToString())
                        .sprDtlNew.SetCellValue(i, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo, touDr(0).Item("ZONE_CD").ToString())
                    End If

                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Private Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#End Region

#Region "POPUP"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMD010F, ByVal objNm As String, ByVal actionType As LMD010C.ActionType) As Boolean

        With frm

            Select Case objNm

                Case .sprDtlNew.Name

                    'スプレッド項目（振替先）POP呼出&戻り値設定
                    Return Me.ShowPopupSpreadSaki(frm, objNm)

                    '振替元の荷主マスタ参照時
                Case .txtCustCdL.Name, .txtCustCdM.Name
                    Dim motosakiFlg As Integer = 0
                    Call Me.SetReturnCustPop(frm, motosakiFlg, actionType)

                    '振替先の荷主マスタ参照時
                Case .txtCustCdLNew.Name, .txtCustCdMNew.Name
                    Dim motosakiFlg As Integer = 1
                    Call Me.SetReturnCustPop(frm, motosakiFlg, actionType)

                    '振替元の在庫テーブル照会時
                Case .txtGoodsCdCust.Name, .txtGoodsNmCust.Name
                    '在庫テーブル参照時、荷主コードは必須。なので荷主コードの必須チェックを行う。
                    If Me._V.IsZaikoPopChk() = False Then
                        Return False
                    End If
                    Call Me.SetReturnZaiTablesPop(frm)

                    '振替先の商品マスタ照会時
                Case .txtGoodsCdCustNew.Name, .txtGoodsNmCustNew.Name
                    Call Me.SetReturnGoodsPop(frm, actionType)

                Case Else

                    '作業コードの場合
                    Select Case objNm.Substring(0, objNm.Length - 2)

                        '作業項目マスタ参照時
                        Case LMD010C.SAGYO_CD

                            Call Me.SetReturnSagyoPop(frm, objNm, objNm.Substring(objNm.Length - 2, 2), actionType)

                    End Select

            End Select

        End With

        MyBase.ShowMessage(frm, "G003")

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="motosakiFlg">振替元時⇒0 振替先時⇒1</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMD010F, ByVal motosakiFlg As Integer, ByVal actionType As LMD010C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '振替元の荷主マスタ参照時
            If 0 = motosakiFlg Then
                .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
                'START SHINOHARA 要望番号513
                If actionType = LMD010C.ActionType.ENTER Then
                    .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                    .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                End If
                'END SHINOHARA 要望番号513
                .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            Else
                '振替先の荷主マスタ参照時
                .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
                'START SHINOHARA 要望番号513
                If actionType = LMD010C.ActionType.ENTER Then
                    .Item("CUST_CD_L") = frm.txtCustCdLNew.TextValue
                    .Item("CUST_CD_M") = frm.txtCustCdMNew.TextValue
                End If
                'END SHINOHARA 要望番号513
                .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            End If

            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S

        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="motosakiFlg">振替元時⇒0 振替先時⇒1</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMD010F, ByVal motosakiFlg As Integer, ByVal actionType As LMD010C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, motosakiFlg, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                '振替元の荷主マスタ参照時
                If 0 = motosakiFlg Then

                    .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                    .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                    .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                    .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()

                Else
                    '振替先の荷主マスタ参照時
                    .txtCustCdLNew.TextValue = dr.Item("CUST_CD_L").ToString()
                    .txtCustCdMNew.TextValue = dr.Item("CUST_CD_M").ToString()
                    .lblCustNmLNew.TextValue = dr.Item("CUST_NM_L").ToString()
                    .lblCustNmMNew.TextValue = dr.Item("CUST_NM_M").ToString()

                    'スプレッドシートの状態荷主コンボの再生成を行う
                    Call Me._G.setCustCond(dr.Item("CUST_CD_L").ToString())

                    ' 振替先 キープ品列表示制御
                    Dim isVisibleKeepGoods As Boolean = IsBykKeepGoodsCd()
                    .sprDtlNew.ActiveSheet.Columns(LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo).Visible = isVisibleKeepGoods
                End If

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 在庫テーブル照会参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowZaiTablePopup(ByVal frm As LMD010F) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMD100DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMD100C_TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '振替元の在庫テーブル照会参照時
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("WH_CD") = frm.cmbSoko.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
            .Item("GOODS_NM") = frm.txtGoodsNmCust.TextValue
            .Item("LOT_NO") = frm.txtLotNo.TextValue.ToUpper()
            .Item("INKA_STATE_KB") = "50"
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("ZERO_SEARCH_FLG") = "01"

        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMD100")

    End Function

    ''' <summary>
    ''' 在庫テーブル照会Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnZaiTablesPop(ByVal frm As LMD010F) As Boolean

        Dim prm As LMFormData = Me.ShowZaiTablePopup(frm)
        Dim dr As DataRow
        If prm.ReturnFlg = True AndAlso prm.ParamDataSet.Tables(LMControlC.LMD100C_TABLE_NM_OUT).Rows.Count <> 0 Then

            dr = prm.ParamDataSet.Tables(LMControlC.LMD100C_TABLE_NM_OUT).Rows(0)

            With frm

                '振替元の在庫テーブル照会参照時
                .txtGoodsCdCust.TextValue = dr.Item("GOODS_CD_CUST").ToString()
                .txtGoodsNmCust.TextValue = dr.Item("NM_1").ToString()
                .lblGoodsCdNrs.TextValue = dr.Item("GOODS_CD_NRS").ToString()
                .txtLotNo.TextValue = dr.Item("LOT_NO").ToString()
                .txtSerialNo.TextValue = dr.Item("SERIAL_NO").ToString()
                .numIrime.Value = dr.Item("IRIME").ToString
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
                .lblIrimeTanni.TextValue = dr.Item("IRIME_UT_NM").ToString()
                .lblIrimeTanniKB.TextValue = dr.Item("IRIME_UT").ToString()
                .lblKonsuTanni.TextValue = dr.Item("NB_UT_NM").ToString()
                .lblKonsuTanniKB.TextValue = dr.Item("NB_UT").ToString()
                .lblCntTani.TextValue = dr.Item("NB_UT_NM").ToString()
                .lblCntTaniKB.TextValue = dr.Item("NB_UT").ToString()
#Else
                .lblIrimeTanni.KbnValue = dr.Item("IRIME_UT").ToString()
                .lblKonsuTanni.KbnValue = dr.Item("NB_UT").ToString()
                .lblCntTani.KbnValue = dr.Item("NB_UT").ToString()
#End If


                .lblIrisuCnt.Value = dr.Item("PKG_NB").ToString()
                .cmbTaxKbn.SelectedValue = dr.Item("TAX_KB").ToString()

            End With

            '終了処理()
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

            '在庫引当処理をそのまま呼び出し
            Call OpenHikiatePop(frm)

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMD010F, ByVal actionType As LMD010C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            '振替先の商品マスタ参照時
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdLNew.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdMNew.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMD010C.ActionType.ENTER Then
                .Item("GOODS_CD_CUST") = frm.txtGoodsCdCustNew.TextValue
                .Item("GOODS_NM_1") = frm.txtGoodsNmCustNew.TextValue
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            If frm.chkYoukiChange.Checked = True Then
                '容器変更有の場合は振替元と同一の入目単位をセット
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
                .Item("IRIME_UT") = frm.lblIrimeTanniKB.TextValue()
#Else
                .Item("IRIME_UT") = frm.lblIrimeTanni.KbnValue
#End If
            Else
                '容器変更無の場合は振替元と同一の入目、入目単位、個数単位をセット
                .Item("IRIME") = frm.numIrime.Value
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
                .Item("IRIME_UT") = frm.lblIrimeTanniKB.TextValue()
                .Item("NB_UT") = frm.lblKonsuTanniKB.TextValue()
#Else
                .Item("IRIME_UT") = frm.lblIrimeTanni.KbnValue
                .Item("NB_UT") = frm.lblKonsuTanni.KbnValue
#End If

            End If

        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ020")

    End Function

    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMD010F, ByVal actionType As LMD010C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)

            With frm

                '振替先の商品マスタ参照時
                .txtGoodsCdCustNew.TextValue = dr.Item("GOODS_CD_CUST").ToString()
                .txtGoodsNmCustNew.TextValue = dr.Item("GOODS_NM_1").ToString()
                .lblGoodsCdNrsNew.TextValue = dr.Item("GOODS_CD_NRS").ToString()
                .numIrimeNew.Value = dr.Item("STD_IRIME_NB").ToString()
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
                .lblIrimeTanniNew.TextValue = dr.Item("STD_IRIME_UT_NM").ToString()
                .lblKosuTanniNew.TextValue = dr.Item("NB_UT_NM").ToString()
#Else
                .lblIrimeTanniNew.KbnValue = dr.Item("STD_IRIME_UT").ToString()
                .lblKosuTanniNew.KbnValue = dr.Item("NB_UT").ToString()
#End If
                DirectCast(.lblGoodsCdNrsNew, Win.InputMan.LMImTextBox).BackColorDef = Utility.LMGUIUtility.GetReadOnlyBackColor()

                For i As Integer = 0 To .sprDtlNew.Sheets(0).RowCount - 1
                    If Convert.ToDecimal(Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(i, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo))) = 0 Then
                        '値が０の場合、設定
                        .sprDtlNew.SetCellValue(i, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo, .numIrimeNew.TextValue)
                    End If
                Next

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 作業項目マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="value">作業コード</param>
    ''' <param name="type">振替元or振替先の判定文字列</param>
    ''' <remarks></remarks>
    Private Function ShowSagyoPopup(ByVal frm As LMD010F, ByVal value As String, ByVal sagyoCnt As Integer, ByVal type As String) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            If LMD010C.SagyoData.O.ToString().Equals(type) = True Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            Else
                .Item("CUST_CD_L") = frm.txtCustCdLNew.TextValue
            End If
            .Item("SAGYO_CD") = value
            .Item("SAGYO_CNT") = sagyoCnt
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ200")

    End Function

    ''' <summary>
    ''' 作業Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="sagyoStr">後ろ2桁を除いたコントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPop(ByVal frm As LMD010F, ByVal objNm As String, ByVal sagyoStr As String, ByVal actionType As LMD010C.ActionType) As Boolean

        Select Case actionType

            Case LMD010C.ActionType.ENTER

                Return Me.SetReturnSagyoPopEnter(frm, objNm)

            Case LMD010C.ActionType.MASTER

                Return Me.SetReturnSagyoPopOpenMaster(frm, objNm, sagyoStr)

        End Select

        Return False

    End Function

    ''' <summary>
    ''' 作業マスタ参照POP起動(Enter押下時)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPopEnter(ByVal frm As LMD010F, ByVal objNm As String) As Boolean

        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(objNm)
        Dim type As String = txtCtl.Name.Substring(10, 1)

        Dim prm As LMFormData = Me.ShowSagyoPopup(frm, txtCtl.TextValue, 1, type)
        Dim arrCtlNm As ArrayList = Me.GetSagyoCtlNm(objNm)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)

            txtCtl.TextValue = dr.Item("SAGYO_CD").ToString()
            Me._G.GetTextControl(arrCtlNm(1).ToString()).TextValue = dr.Item("SAGYO_RYAK").ToString()
            Me._G.GetTextControl(arrCtlNm(2).ToString()).TextValue = dr.Item("SAGYO_NM").ToString()

            Return True

        Else
            Me._G.GetTextControl(arrCtlNm(1).ToString()).TextValue = String.Empty
            Me._G.GetTextControl(arrCtlNm(2).ToString()).TextValue = String.Empty

        End If

        Return False

    End Function

    ''' <summary>
    ''' 作業マスタ参照POP起動(マスタ参照時)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="sagyoStr">後ろ2桁</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoPopOpenMaster(ByVal frm As LMD010F, ByVal objNm As String, ByVal sagyoStr As String) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim arr As ArrayList = New ArrayList()
        Dim max As Integer = LMD010C.SAGYO_MAX_REC
        Dim type As String = sagyoStr.Substring(0, 1)
        Dim txtNm As String = String.Concat(LMD010C.SAGYO_CD, type)
        Dim ctl As Win.InputMan.LMImTextBox() = New Win.InputMan.LMImTextBox() {}
        Dim chkCtl As Win.InputMan.LMImTextBox = Nothing
        Dim cnt As Integer = 0

        For i As Integer = 1 To max

            '作業コードコントロールの取得
            chkCtl = DirectCast(frm.Controls.Find(String.Concat(txtNm, i.ToString()), True)(0), Win.InputMan.LMImTextBox)

            '値が入っていないものを判定
            If String.IsNullOrEmpty(chkCtl.TextValue) = True Then

                '現在のカウントを設定
                cnt = ctl.Length

                '領域の確保
                ReDim Preserve ctl(cnt)

                'コントロール配列に設定
                ctl(cnt) = chkCtl

            End If

        Next

        '処理結果のカウント
        cnt = ctl.Length
        Dim msg As String = String.Empty
        Select Case type

            Case LMD010C.SagyoData.O.ToString()

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"振替元作業コード", "Transfer original working code", "대체 원(元)작업코드", "中国語"})
                '2017/09/25 修正 李↑

            Case LMD010C.SagyoData.N.ToString()

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"振替先作業コード", "Transfer destination working code", "대체 처(先)작업코드", "中国語"})
                '2017/09/25 修正 李↑

        End Select

        '設定カウントチェック
        If Me._V.IsSagyoPopupChk(cnt, msg) = False Then
            Return False
        End If

        'ナビゲート処理
        Dim prm As LMFormData = Me.ShowSagyoPopup(frm, String.Empty, cnt, type)

        '戻り値がある場合、設定
        If prm.ReturnFlg = True Then

            Dim dt As DataTable = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT)
            Dim rowMax As Integer = dt.Rows.Count - 1

            '戻り値の行数分設定
            For i As Integer = 0 To rowMax
                ctl(i).TextValue = dt.Rows(i).Item("SAGYO_CD").ToString()
                Me._G.GetTextControl(Me.GetSagyoCtlNm(ctl(i).Name)(1).ToString()).TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString()
                Me._G.GetTextControl(Me.GetSagyoCtlNm(ctl(i).Name)(2).ToString()).TextValue = dt.Rows(i).Item("SAGYO_NM").ToString()
            Next

            max = LMD010C.SAGYO_MAX_REC
            For i As Integer = 1 To max

                '作業コードコントロールの取得
                chkCtl = DirectCast(frm.Controls.Find(String.Concat(txtNm, i.ToString()), True)(0), Win.InputMan.LMImTextBox)

                '値が入っていないものを判定
                If String.IsNullOrEmpty(chkCtl.TextValue) = True Then
                    Me._G.GetTextControl(Me.GetSagyoCtlNm(chkCtl.Name)(1).ToString()).TextValue = String.Empty
                    Me._G.GetTextControl(Me.GetSagyoCtlNm(chkCtl.Name)(2).ToString()).TextValue = String.Empty
                End If

            Next

            Return True

        End If

        max = LMD010C.SAGYO_MAX_REC
        For i As Integer = 1 To max

            '作業コードコントロールの取得
            chkCtl = DirectCast(frm.Controls.Find(String.Concat(txtNm, i.ToString()), True)(0), Win.InputMan.LMImTextBox)

            '値が入っていないものを判定
            If String.IsNullOrEmpty(chkCtl.TextValue) = True Then
                Me._G.GetTextControl(Me.GetSagyoCtlNm(chkCtl.Name)(1).ToString()).TextValue = String.Empty
                Me._G.GetTextControl(Me.GetSagyoCtlNm(chkCtl.Name)(2).ToString()).TextValue = String.Empty
            End If

        Next

        Return False

    End Function

    ''' <summary>
    ''' 作業コントロール名のリストを生成
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>コントロール名のリスト</returns>
    ''' <remarks>
    ''' リスト
    ''' ①：隠し(PK)名
    ''' ②：ラベル名
    ''' ③：フラグ名
    ''' ④：隠し(UP_KBN)名
    ''' </remarks>
    Private Function GetSagyoCtlNm(ByVal objNm As String) As ArrayList

        GetSagyoCtlNm = New ArrayList()

        '後ろ2桁を取得
        Dim ctlNm As String = objNm.Substring(objNm.Length - 2, 2)

        '隠し(PK)名を設定
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_PK, ctlNm))

        'ラベル名を設定
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_NM, ctlNm))

        'Insert用の作業名
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_IN_NM, ctlNm))

        '隠し(UP_KBN)名を設定
        GetSagyoCtlNm.Add(String.Concat(LMD010C.SAGYO_UP, ctlNm))

        Return GetSagyoCtlNm

    End Function

    ''' <summary>
    ''' 在庫引当起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="hikiZenFlg">引当時⇒0 全量時⇒1 </param>
    ''' <remarks></remarks>
    Private Function ShowHikiatePopup(ByVal frm As LMD010F, ByVal hikiZenFlg As Integer) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMC040DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            Dim hikiateFlg As String = String.Empty
            Dim shukkaTani As String = "01"

            If hikiZenFlg = 0 Then
                '手動引当
                hikiateFlg = "00"
            Else
                '自動引当
                hikiateFlg = "01"
                '自動引当時は初期状態に戻して再計算
                frm.lblHikiSumiCnt.TextValue = LMConst.FLG.OFF
                frm.lblKosuCnt.TextValue = Convert.ToString(Convert.ToInt32(frm.numKonsu.Value) + Convert.ToInt32(frm.numCnt.Value))
                frm.lblHikiZanCnt.TextValue = Convert.ToString(Convert.ToInt32(frm.lblKosuCnt.TextValue) - Convert.ToInt32(frm.lblHikiSumiCnt.TextValue))

            End If

            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("WH_CD") = frm.cmbSoko.SelectedValue
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            .Item("CUST_NM_L") = frm.lblCustNmL.TextValue
            .Item("CUST_NM_M") = frm.lblCustNmM.TextValue
            .Item("GOODS_CD_NRS") = frm.lblGoodsCdNrs.TextValue
            .Item("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
            .Item("GOODS_NM") = frm.txtGoodsNmCust.TextValue
            .Item("SERIAL_NO") = frm.txtSerialNo.TextValue
            .Item("LOT_NO") = frm.txtLotNo.TextValue.ToUpper()
            .Item("IRIME") = frm.numIrime.Value
            .Item("IRIME_UT") = frm.lblIrimeTanni.TextValue ' LMC040の画面で区分タイトルラベルを利用する際に変更

            '出荷単位は"01":個数引当で固定
            .Item("ALCTD_KB") = shukkaTani
            .Item("NB_UT") = frm.lblKonsuTanni.TextValue    ' LMC040の画面で区分タイトルラベルを利用する際に変更
            .Item("STD_IRIME_UT") = frm.lblIrimeTanni.TextValue ' LMC040の画面で区分タイトルラベルを利用する際に変更
            .Item("PKG_NB") = frm.lblIrisuCnt.TextValue
            .Item("ALCTD_NB") = "0"
            .Item("BACKLOG_NB") = frm.lblHikiZanCnt.TextValue
            .Item("ALCTD_QT") = "0"
            .Item("BACKLOG_QT") = IrimeCalc(frm.lblHikiZanCnt.TextValue, frm)
            .Item("KONSU") = frm.numKonsu.TextValue
            .Item("HASU") = frm.numCnt.TextValue
            .Item("KOSU") = Convert.ToDecimal(frm.lblKosuCnt.TextValue)
            .Item("SURYO") = IrimeCalc(frm.lblKosuCnt.TextValue, frm)

            .Item("HIKIATE_FLG") = hikiateFlg
            .Item("TANINUSI_FLG") = "00"
            'START YANAI No.4
            .Item("SORT_FLG") = "00"
            'END YANAI No.4
            'START YANAI 20111003 一括引当対応
            .Item("OUTKA_PLAN_DATE") = frm.imdFurikaeDate.TextValue
            'END YANAI 20111003 一括引当対応

            'START YANAI 要望番号507
            .Item("OUTKA_S_CNT") = "0"
            'END YANAI 要望番号507

            'START YANAI 要望番号547
            .Item("PGID") = MyBase.GetPGID()
            'END YANAI 要望番号547

        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me.PopFormShow(prm, "LMC040")

    End Function

    ''' <summary>
    ''' 在庫引当の戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="hikiZenFlg">引当時⇒0 全量時⇒1 </param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnHikiatePop(ByVal frm As LMD010F, ByVal hikiZenFlg As Integer) As Boolean

        Dim prm As LMFormData = Me.ShowHikiatePopup(frm, hikiZenFlg)
        '検索成功時共通処理を行う

        If prm.ReturnFlg = True Then
            Dim dt As DataTable = prm.ParamDataSet.Tables(LMControlC.LMC040C_TABLE_NM_OUT)

            If dt.Rows.Count = 0 Then
                Return False
            End If

            Dim dr As DataRow = dt.Rows(0)

            '取得データをSPREADに表示
            Call Me._G.SetSpread(dt, hikiZenFlg)

            If hikiZenFlg = 0 Then
                '引当時かつスプレッド表示後にフラグを立てる
                Me._HikiateFlg = LMConst.FLG.ON
            Else
                '全量時はフラグを戻す
                Me._HikiateFlg = LMConst.FLG.OFF
            End If

            frm.cmbTaxKbn.SelectedValue = dr.Item("TAX_KB").ToString()
            Call Me.CalcHikiSumiCnt(frm, hikiZenFlg)

        End If

        Return False

    End Function

    ''' <summary>
    ''' 棟室マスタ、在庫マスタ照会画面Pop処理（振替先スプレッド）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <remarks></remarks>
    Private Function ShowPopupSpreadSaki(ByVal frm As LMD010F, ByVal objNm As String) As Boolean

        '移動元スプレッド
        Dim sprA As Win.Spread.LMSpread = frm.sprDtlNew
        Dim spr As FarPoint.Win.Spread.SheetView = sprA.ActiveSheet

        With spr

            If 0 < .Rows.Count Then

                Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
                Dim colNo As Integer = cell.Column.Index
                Dim rowNo As Integer = cell.Row.Index
                Dim listCol As Integer() = _
                    New Integer(2) {LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo, _
                                    LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, _
                                    LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo}

                Select Case colNo
                    '棟、室、ZONE
                    Case LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo, _
                                LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, _
                                LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo
                        '棟室マスタ照会画面Pop処理
                        Dim toShitsuPop As LMFormData = Me.ShowToshitsuZonePopup(frm, rowNo, spr, listCol)
                        '当該画面項目に戻り値を設定
                        If toShitsuPop.ReturnFlg = True Then
                            Dim toShitsuDr As DataRow = toShitsuPop.ParamDataSet.Tables(LMZ120C.TABLE_NM_OUT).Rows(0)
                            sprA.SetCellValue(rowNo, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo, toShitsuDr.Item("TOU_NO").ToString())
                            sprA.SetCellValue(rowNo, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo, toShitsuDr.Item("SITU_NO").ToString())
                            sprA.SetCellValue(rowNo, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo, toShitsuDr.Item("ZONE_CD").ToString())
                        End If

                        MyBase.ShowMessage(frm, "G003")

                    Case Else
                        Return Me._LMDConV.SetFocusErrMessage()

                End Select
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟室マスタ照会画面Pop起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Function ShowToshitsuZonePopup(ByVal frm As LMD010F, ByVal rowNo As Integer, _
                                           ByVal spr As FarPoint.Win.Spread.SheetView, ByVal listCol As Integer()) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ120DS()
        Dim dt As DataTable = ds.Tables(LMZ120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'Keyをデータセットに設定
        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("WH_CD") = frm.cmbSoko.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            '.Item("TOU_NO") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, listCol(0)))
            '.Item("SITU_NO") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, listCol(1)))
            '.Item("ZONE_CD") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, listCol(2)))
            'START SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ120")

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

    ''' <summary>
    ''' 入目計算処理
    ''' </summary>
    ''' <param name="ctl">指定されたコントロール</param>
    ''' <param name="frm">フォーム</param>
    ''' <returns>計算結果</returns>
    ''' <remarks></remarks>
    Private Function IrimeCalc(ByVal ctl As String, ByVal frm As LMD010F) As Object

        '計算結果
        Dim returnCalc As Decimal = 0

        '渡された引数をConvert
        Dim ctlDecimal As Decimal = Convert.ToDecimal(ctl)

        '入目を変数にセット
        Dim irime As Decimal = Convert.ToDecimal(frm.numIrime.Value)

        '単純に計算
        returnCalc = ctlDecimal * irime

        '計算結果をReturn
        Return returnCalc

    End Function

    ''' <summary>
    ''' 引当済個数計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub CalcHikiSumiCnt(ByVal frm As LMD010F, ByVal hikiZenFlg As Integer)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        Dim arrFurikae As ArrayList = Nothing
        Dim HikiSumiCnt As Decimal = 0

        arr = Me._LMDConH.GetCheckList(frm.spdDtl.ActiveSheet, LMD010G.sprDetailDef.DEF.ColNo)
        arrFurikae = Me._LMDConH.GetSpredList(frm.spdDtl.ActiveSheet, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo)

        For i As Integer = 0 To arrFurikae.Count - 1

            '引当済個数の算出を行う
            HikiSumiCnt = HikiSumiCnt + Convert.ToDecimal(Convert.ToInt32(arrFurikae(i)))

        Next

        With frm

            If hikiZenFlg = 0 Then

                '全量以外の時
                Dim kosu As Decimal = (Convert.ToDecimal(.numKonsu.Value) * Convert.ToDecimal(.lblIrisuCnt.Value)) + Convert.ToDecimal(.numCnt.Value)

                '個数の計算
                .lblKosuCnt.Value = Convert.ToDecimal(kosu.ToString())

                '引当済個数と引当残個数をセット
                .lblHikiSumiCnt.Value = HikiSumiCnt.ToString()

                Dim calcHikiZanCnt As Decimal = (Convert.ToDecimal(.lblKosuCnt.TextValue) - HikiSumiCnt)

                .lblHikiZanCnt.Value = Convert.ToDecimal(calcHikiZanCnt.ToString())

            ElseIf hikiZenFlg = 1 Then
                '全量の時

                '個数
                .lblKosuCnt.Value = HikiSumiCnt.ToString()

                '梱数・端数
                Dim konsu As Decimal = 0
                Dim hasu As Decimal = 0
                konsu = System.Math.Floor(Convert.ToDecimal(.lblKosuCnt.Value) / Convert.ToDecimal(.lblIrisuCnt.Value))
                hasu = Convert.ToDecimal(.lblKosuCnt.Value) - konsu * Convert.ToDecimal(.lblIrisuCnt.Value)
                .numKonsu.Value = konsu
                .numCnt.Value = hasu

                '引当済個数
                .lblHikiSumiCnt.Value = HikiSumiCnt.ToString()

                '引当残個数
                .lblHikiZanCnt.TextValue = "0"

            End If

        End With

    End Sub

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMD010F, ByVal actionType As LMD010C.ActionType)

        With frm

            If (LMD010C.ActionType.FURIKAEMOTOKAKUTEI).Equals(actionType) = True Then
                '荷主名称（大）（振替元）
                If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                    If String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        .txtCustCdM.TextValue = "00"
                    End If
                    .lblCustNmL.TextValue = GetCachedCust(.txtCustCdL.TextValue, .txtCustCdM.TextValue, "00", "00", LMD010C.CUST_L)
                End If
            End If

            If (LMD010C.ActionType.FURIKAEMOTOKAKUTEI).Equals(actionType) = True Then
                '荷主名称（中）（振替元）
                If String.IsNullOrEmpty(.txtCustCdM.TextValue) = False Then
                    .lblCustNmM.TextValue = GetCachedCust(.txtCustCdL.TextValue, .txtCustCdM.TextValue, "00", "00", LMD010C.CUST_M)
                End If
            End If

            If (LMD010C.ActionType.FURIKAEKAKUTEI).Equals(actionType) = True Then
                '荷主名称（大）（振替先）
                If String.IsNullOrEmpty(.txtCustCdLNew.TextValue) = False Then
                    If String.IsNullOrEmpty(.txtCustCdMNew.TextValue) = True Then
                        .txtCustCdMNew.TextValue = "00"
                    End If
                    .lblCustNmLNew.TextValue = GetCachedCust(.txtCustCdLNew.TextValue, .txtCustCdMNew.TextValue, "00", "00", LMD010C.CUST_L)
                End If
            End If

            If (LMD010C.ActionType.FURIKAEKAKUTEI).Equals(actionType) = True Then
                '荷主名称（中）（振替先）
                If String.IsNullOrEmpty(.txtCustCdMNew.TextValue) = False Then
                    .lblCustNmMNew.TextValue = GetCachedCust(.txtCustCdLNew.TextValue, .txtCustCdMNew.TextValue, "00", "00", LMD010C.CUST_M)
                End If
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String, _
                                   ByVal custMode As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            If (custMode).Equals(LMD010C.CUST_L) = True Then
                Return dr(0).Item("CUST_NM_L").ToString
            ElseIf (custMode).Equals(LMD010C.CUST_M) = True Then
                Return dr(0).Item("CUST_NM_M").ToString
            End If
        End If

        Return String.Empty

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(最新の請求日取得)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="key"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDataSetSeiqData(ByVal frm As LMD010F, ByVal key As String) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'DataSet設定
        Dim rtDs As DataSet = New LMD000DS()
        Dim row As DataRow = rtDs.Tables(LMControlC.LMD000_TABLE_NM_IN).NewRow
        Dim inTbl As DataTable = rtDs.Tables(LMControlC.LMD000_TABLE_NM_IN)

        row(LMControlC.LMD000_COL_NRS_BR_CD) = frm.cmbNrsBrCd.SelectedValue
        row(LMControlC.LMD000_COL_CHK_DATE) = frm.imdFurikaeDate.TextValue

        '2017/09/25 修正 李↓
        row(LMControlC.LMD000_COL_REPLACE_STR1) = lgm.Selector({"保管料・荷役料が既に計算されている", "Storage fees and handling fee has already been calculated", "보관료/하역료가 이미 계산되어있음", "中国語"})
        '2017/09/25 修正 李↑

        Select Case key
            Case "MOTO"
                row(LMControlC.LMD000_COL_GOODS_CD_NRS) = frm.lblGoodsCdNrs.TextValue

                '2017/09/25 修正 李↓
                row(LMControlC.LMD000_COL_REPLACE_STR2) = lgm.Selector({"振替元確定", "Transfer the original deterministic", "대체 원(元)확정", "中国語"})
                '2017/09/25 修正 李↑

            Case "SAKI"
                row(LMControlC.LMD000_COL_GOODS_CD_NRS) = frm.lblGoodsCdNrsNew.TextValue

                '2017/09/25 修正 李↓
                row(LMControlC.LMD000_COL_REPLACE_STR2) = lgm.Selector({"振替確定", "Transfer confirmed", "대체확정", "中国語"})
                '2017/09/25 修正 李↑

        End Select
        inTbl.Rows.Add(row)

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(振替元の最新の請求日取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetMotoSeiqData(ByVal frm As LMD010F, ByVal ds As DataSet)

        'DataSet設定
        Dim rtDs As DataSet = New LMD010DS()
        Dim row As DataRow = ds.Tables(LMD010C.TABLE_NM_KAGAMI_IN).NewRow
        Dim inTbl As DataTable = ds.Tables(LMD010C.TABLE_NM_KAGAMI_IN)

        '商品マスタより、荷主コードを取得
        '---↓
        'Dim drGoods As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = '", frm.lblGoodsCdNrs.TextValue, "'"))

        Dim goodsDs As MGoodsDS = New MGoodsDS
        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
        goodsDr.Item("GOODS_CD_NRS") = frm.lblGoodsCdNrs.TextValue
        goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
        goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
#End If
        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
        Dim drGoods As DataRow() = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
        '---↑


        '荷主マスタより保管料請求先コードを取得
        Dim drCust As DataRow() = Me._LMDConV.SelectCustListDataRow(drGoods(0).Item("CUST_CD_L").ToString(), drGoods(0).Item("CUST_CD_M").ToString(), drGoods(0).Item("CUST_CD_S").ToString(), drGoods(0).Item("CUST_CD_SS").ToString())
        row("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        row("STORAGE_SEIQTO_CD") = drCust(0).Item("HOKAN_SEIQTO_CD")
        row("HANDLING_SEIQTO_CD") = drCust(0).Item("NIYAKU_SEIQTO_CD")
        row("SAGYO_SEIQTO_CD") = drCust(0).Item("SAGYO_SEIQTO_CD")
        inTbl.Rows.Add(row)

    End Sub

    ''' <summary>
    ''' データセット設定(振替先の最新の請求日取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetSakiSeiqData(ByVal frm As LMD010F, ByVal ds As DataSet)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_KAGAMI_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_KAGAMI_IN)

        '商品マスタより、荷主コードを取得
        '---↓
        'Dim drGoods As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = '", frm.lblGoodsCdNrs.TextValue, "'"))

        Dim goodsDs As MGoodsDS = New MGoodsDS
        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
        goodsDr.Item("GOODS_CD_NRS") = frm.lblGoodsCdNrs.TextValue
        goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
        goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
#End If
        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)

        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
        Dim drGoods As DataRow() = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
        '---↑

        '荷主マスタより保管料請求先コードを取得
        Dim drCust As DataRow() = Me._LMDConV.SelectCustListDataRow(drGoods(0).Item("CUST_CD_L").ToString(), drGoods(0).Item("CUST_CD_M").ToString(), drGoods(0).Item("CUST_CD_S").ToString(), drGoods(0).Item("CUST_CD_SS").ToString())
        dr("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        dr("STORAGE_SEIQTO_CD") = drCust(0).Item("HOKAN_SEIQTO_CD")
        dr("HANDLING_SEIQTO_CD") = drCust(0).Item("NIYAKU_SEIQTO_CD")
        dr("SAGYO_SEIQTO_CD") = drCust(0).Item("SAGYO_SEIQTO_CD")
        dr("CHECK_DATE") = frm.imdFurikaeDate.TextValue

        inTbl.Rows.Add(dr)
        '持ちまわっているデータセットにつめなおす
        ds.Tables(LMD010C.TABLE_NM_KAGAMI_IN).ImportRow(inTbl.Rows(0))


    End Sub

    ''' <summary>
    ''' データセット設定(移動日取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetIdoData(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arrMoto As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_IDO_TRS_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_IDO_TRS_IN)

        Dim rowCount As Integer = 0
        Dim zaiRecNo As String = String.Empty
        rowCount = arrMoto.Count - 1

        For i As Integer = 0 To rowCount

            zaiRecNo = Me._LMDConV.GetCellValue(frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo))

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            dr("ZAI_REC_NO") = zaiRecNo
            dr("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            ds.Tables(LMD010C.TABLE_NM_IDO_TRS_IN).ImportRow(inTbl.Rows(0))

        Next

    End Sub

    ''' <summary>
    ''' データセット設定(振替データ作成用のデータ取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetAll(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arrNew As ArrayList, ByVal arrOld As ArrayList)

        '振替確定データ作成データセットクラスを呼び出す
        Call Me.SetDatasetFurikaeSelect(frm, ds, arrOld)

        '振替元在庫データ検索データセットクラスを呼び出す
        Call Me.SetDatasetZaiOldSelect(frm, ds, arrOld)

        '入荷(大)データセットクラスを呼び出す
        Call Me.SetDatasetInkaL(frm, ds, arrNew)

        '入荷(中)データセットクラスを呼び出す
        Call Me.SetDatasetInkaM(frm, ds)

        '入荷(小)データセットクラスを呼び出す
        Call Me.SetDatasetInkaS(frm, ds, arrNew)

        '出荷(大)データセットクラスを呼び出す
        Call Me.SetDatasetOutKaL(frm, ds)

        '出荷(中)データセットクラスを呼び出す
        Call Me.SetDatasetOutKaM(frm, ds, arrOld)

        '出荷(小)データセットクラスを呼び出す
        Call Me.SetDatasetOutKaS(frm, ds, arrOld)

        '振替先作業データセットクラスを呼び出す
        Call Me.SetDatasetSagyoInka(frm, ds)

        '振替元作業データセットクラスを呼び出す
        Call Me.SetDatasetSagyoOutka(frm, ds)

        '振替先在庫データセットクラスを呼び出す
        Call Me.SetDatasetZaikoNew(frm, ds, arrNew)

        '振替元在庫データセットクラスを呼び出す
        Call Me.SetDatasetZaikoOld(frm, ds, arrOld)

        '最新の請求日取得データセットクラスを呼出
        Call SetDataSetSakiSeiqData(frm, ds)

#If True Then       'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加機能
        '振替データセットクラスを呼び出す
        Call Me.SetDataseFurikae(frm, ds, arrNew)
#End If

    End Sub

    ''' <summary>
    ''' データセット設定(振替データ作成用のデータ取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="arr">振替先のスプレッドシートのチェックボックスを格納したArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetFurikaeSelect(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_IN)
        Dim targetRec As String = String.Empty
        Dim highRec As String = String.Empty

        With frm

            '振替元のスプレッドにチェックが入っている在庫レコード番号のMAXを取得する
            For i As Integer = 0 To arr.Count - 1

                '比較対象の変数にとりあえずセット
                targetRec = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo))

                '比較を行い、比較対象の方が高ければhighRecにセットする
                If highRec < targetRec Then
                    highRec = targetRec
                End If

            Next

            '1レコード分のみデータセットに詰める
            For i As Integer = 0 To 0

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
                dr("WH_CD") = .cmbSoko.SelectedValue.ToString()
                dr("ZAI_REC_NO") = highRec
                dr("CUST_CD_L") = .txtCustCdL.TextValue()
                dr("CUST_CD_M") = .txtCustCdM.TextValue()
                dr("SAKI_CUST_CD_L") = .txtCustCdLNew.TextValue()
                dr("SAKI_CUST_CD_M") = .txtCustCdMNew.TextValue()
                'START YANAI 要望番号616
                'dr("MOTO_GOODS_CD_CUST") = .txtGoodsCdCust.TextValue()
                'dr("SAKI_GOODS_CD_CUST") = .txtGoodsCdCustNew.TextValue()
                dr("MOTO_GOODS_CD_CUST") = .lblGoodsCdNrs.TextValue()
                dr("SAKI_GOODS_CD_CUST") = .lblGoodsCdNrsNew.TextValue()
                'END YANAI 要望番号616

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_IN).ImportRow(inTbl.Rows(0))

            Next


        End With

    End Sub

    ''' <summary>
    ''' データセット設定(振替元在庫データ作成用のデータ取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスを格納したArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetZaiOldSelect(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_ZAI_OLD_IN).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_ZAI_OLD_IN)

        With frm
            'スプレッドシートのチェックの分だけ検索条件をセット
            For i As Integer = 0 To arr.Count - 1

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
                dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo))

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_ZAI_OLD_IN).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(入荷(大))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetInkaL(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        Dim dr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_L).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr("INKA_NO_L") = String.Empty
            dr("FURI_NO") = String.Empty
            dr("INKA_TP") = "50"
            Dim youkiChange As String = String.Empty
            If .chkYoukiChange.Checked = True Then
                youkiChange = "20"
            Else
                youkiChange = "30"
            End If
            dr("INKA_KB") = youkiChange
            dr("INKA_STATE_KB") = "50"
            dr("INKA_DATE") = .imdFurikaeDate.TextValue()
            dr("WH_CD") = .cmbSoko.SelectedValue.ToString()
            dr("CUST_CD_L") = .txtCustCdLNew.TextValue()
            dr("CUST_CD_M") = .txtCustCdMNew.TextValue()
            dr("INKA_PLAN_QT") = Me.calcSuryoKosu(frm, 0, arr)
#If False Then '区分タイトルラベル対応 Changed 20151119 INOUE
            dr("INKA_PLAN_QT_UT") = .lblIrimeTanniKB.TextValue()
#Else
            dr("INKA_PLAN_QT_UT") = .lblIrimeTanni.KbnValue
#End If
            dr("INKA_TTL_NB") = Me.calcSuryoKosu(frm, 1, arr)
            dr("BUYER_ORD_NO_L") = String.Empty
            dr("OUTKA_FROM_ORD_NO_L") = .txtDenpNo.TextValue()
            dr("SEIQTO_CD") = String.Empty

            Dim tokiHokan As String = String.Empty

            '当期保管区分「区分マスタコード：H009」（振替元：10、両方：30）
            If .cmbToukiHokanKbn.SelectedValue.Equals("10") Then
                '「振替元」の場合、"00"
                tokiHokan = "00"
            Else
                '「両方」の場合、"01"
                tokiHokan = "01"
            End If

            dr("TOUKI_HOKAN_YN") = tokiHokan
            dr("HOKAN_YN") = String.Empty
            dr("HOKAN_FREE_KIKAN") = String.Empty
            dr("HOKAN_STR_DATE") = .imdFurikaeDate.TextValue()
            dr("NIYAKU_YN") = .cmbNiyakuNew.SelectedValue.ToString()
            dr("TAX_KB") = .cmbTaxKbnNew.SelectedValue.ToString()
            dr("REMARK") = .txtNyukaRemark.TextValue()
            dr("REMARK_OUT") = String.Empty
            dr("CHECKLIST_PRT_DATE") = String.Empty
            dr("CHECKLIST_PRT_USER") = String.Empty
            dr("UKETSUKELIST_PRT_DATE") = String.Empty
            dr("UKETSUKELIST_PRT_USER") = String.Empty
            dr("UKETSUKE_DATE") = String.Empty
            dr("UKETSUKE_USER") = String.Empty
            dr("KEN_DATE") = String.Empty
            dr("KEN_USER") = String.Empty
            dr("INKO_DATE") = String.Empty
            dr("INKO_USER") = String.Empty
            dr("HOUKOKUSYO_PR_DATE") = String.Empty
            dr("HOUKOKUSYO_PR_USER") = String.Empty
            dr("UNCHIN_TP") = "90"
            dr("UNCHIN_KB") = String.Empty

        End With

        ds.Tables(LMD010C.TABLE_NM_INKA_L).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(入荷(中))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetInkaM(ByVal frm As LMD010F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_M).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr("INKA_NO_L") = String.Empty
            dr("INKA_NO_M") = "001"
            dr("GOODS_CD_NRS") = String.Empty
            dr("OUTKA_FROM_ORD_NO_M") = .txtDenpNo.TextValue.ToString()
            dr("BUYER_ORD_NO_M") = String.Empty
            dr("REMARK") = String.Empty
            dr("PRINT_SORT") = "99"

        End With

        ds.Tables(LMD010C.TABLE_NM_INKA_M).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(入荷(小))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスを格納したArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetInkaS(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_INKA_S).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_INKA_S)

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim inkaMDr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_M).Rows(0)

        'キャッシュの値を取得(商品)
        Dim mGoodsDrs As DataRow() = Nothing
        Dim stdWtKgs As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim irisu As Integer = 0

        With frm

            'スプレッドシートのチェックの分だけ検索条件をセット
            For i As Integer = 0 To arr.Count - 1

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
                dr("INKA_NO_L") = String.Empty
                dr("INKA_NO_M") = inkaMDr.Item("INKA_NO_M").ToString()
                dr("INKA_NO_S") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_INKA_NO_S.ColNo))
                dr("ZAI_REC_NO") = String.Empty
                dr("LOT_NO") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_LOT_NO.ColNo)).ToUpper()
                dr("LOCA") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_LOCA.ColNo)).ToUpper()
                dr("TOU_NO") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo))
                dr("SITU_NO") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo))
                dr("ZONE_CD") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo))

                '---↓
                'mGoodsDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", .lblGoodsCdNrsNew.TextValue, "' "))

                Dim goodsDs As MGoodsDS = New MGoodsDS
                Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                goodsDr.Item("GOODS_CD_NRS") = .lblGoodsCdNrsNew.TextValue
                goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
                goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
#End If
                goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                mGoodsDrs = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                '---↑

                If 0 < mGoodsDrs.Length Then

                    '標準重量を取得
                    stdWtKgs = Convert.ToDecimal(mGoodsDrs(0).Item("STD_WT_KGS").ToString())

                    '標準入目
                    stdIrimeNb = Convert.ToDecimal(mGoodsDrs(0).Item("STD_IRIME_NB").ToString())

                    '入数
                    irisu = Convert.ToInt32(mGoodsDrs(0).Item("PKG_NB").ToString())

                End If

                Dim irime As String = String.Empty
                irime = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo))

                Dim konsu As Integer = 0
                Dim hasu As Integer = 0
                Dim furikaeKosu As Integer = 0
                Dim betuWt As Decimal = 0

                '振替個数の取得
                furikaeKosu = Convert.ToInt32(Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)))

                If irisu = 0 = False Then

                    '振替個数から梱数を求める
                    konsu = furikaeKosu \ irisu

                    '振替個数から端数を求める
                    hasu = furikaeKosu Mod irisu

                End If

                dr("IRIME") = irime

                '個別重量の計算
                If Convert.ToDecimal(irime) = 0 = False AndAlso stdWtKgs = 0 = False AndAlso stdIrimeNb = 0 = False Then
                    betuWt = Convert.ToDecimal(irime) * stdWtKgs / stdIrimeNb
                End If

                dr("BETU_WT") = betuWt.ToString()
                dr("KONSU") = konsu
                dr("HASU") = hasu
                dr("SERIAL_NO") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_SERIAL_NO.ColNo))
                dr("GOODS_COND_KB_1") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_1.ColNo))
                dr("GOODS_COND_KB_2") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_2.ColNo))
                dr("GOODS_COND_KB_3") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_GOODS_COND_KB_3.ColNo))
                dr("GOODS_CRT_DATE") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_GOODS_CRT_DATE.ColNo))
                dr("LT_DATE") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_LT_DATE.ColNo))
                dr("SPD_KB") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_SPD_KB.ColNo))
                If .cmbFurikaeKbn.SelectedText.Equals("簿外品") Then
                    dr("OFB_KB") = "02"
                Else
                    dr("OFB_KB") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_OFB_KB.ColNo))
                End If

                dr("DEST_CD") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_DEST_CD.ColNo))
                dr("REMARK") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_REMARK.ColNo)).ToUpper()
                dr("ALLOC_PRIORITY") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_ALLOC_PRIORITY.ColNo))
                dr("REMARK_OUT") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_REMARK_OUT.ColNo)).ToUpper()

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_INKA_S).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(出荷(大))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutKaL(ByVal frm As LMD010F, ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMD010C.TABLE_NM_OUTKA_L).NewRow()
        Dim constZero As String = "00"

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr("OUTKA_NO_L") = String.Empty
            dr("FURI_NO") = String.Empty
            Dim youkiChange As String = String.Empty
            If .chkYoukiChange.Checked = True Then
                youkiChange = "20"
            Else
                youkiChange = "30"
            End If
            dr("OUTKA_KB") = youkiChange
            dr("SYUBETU_KB") = "50"
            dr("OUTKA_STATE_KB") = "60"

            dr("OUTKAHOKOKU_YN") = constZero
            dr("PICK_KB") = "01"
            dr("DENP_NO") = String.Empty
            dr("ARR_KANRYO_INFO") = String.Empty
            dr("WH_CD") = .cmbSoko.SelectedValue.ToString()
            dr("OUTKA_PLAN_DATE") = .imdFurikaeDate.TextValue()
            dr("OUTKO_DATE") = .imdFurikaeDate.TextValue()
            dr("ARR_PLAN_DATE") = .imdFurikaeDate.TextValue()
            dr("ARR_PLAN_TIME") = String.Empty
            dr("HOKOKU_DATE") = String.Empty

            Dim tokiHokan As String = String.Empty
            '当期保管区分が"振替元"、"両方"の場合'1'をセット
            If .cmbToukiHokanKbn.SelectedText.Equals("振替元") OrElse _
            .cmbToukiHokanKbn.SelectedText.Equals("両方") Then
                tokiHokan = "01"
            Else
                tokiHokan = constZero

            End If
            dr("TOUKI_HOKAN_YN") = tokiHokan
            dr("END_DATE") = .imdFurikaeDate.TextValue()
            dr("CUST_CD_L") = .txtCustCdL.TextValue()
            dr("CUST_CD_M") = .txtCustCdM.TextValue()
            dr("SHIP_CD_L") = String.Empty
            dr("SHIP_CD_M") = String.Empty
            dr("DEST_CD") = String.Empty
            dr("DEST_AD_3") = String.Empty
            dr("DEST_TEL") = String.Empty
            dr("NHS_REMARK") = String.Empty
            dr("SP_NHS_KB") = String.Empty
            dr("COA_YN") = String.Empty
            dr("CUST_ORD_NO") = .txtOrderNo.TextValue()
            dr("BUYER_ORD_NO") = String.Empty
            dr("REMARK") = .txtSyukkaRemark.TextValue()
            dr("OUTKA_PKG_NB") = LMConst.FLG.OFF
            dr("DENP_YN") = constZero
            dr("PC_KB") = String.Empty
            dr("NIYAKU_YN") = .cmbNiyaku.SelectedValue.ToString()
            dr("ALL_PRINT_FLAG") = constZero
            dr("NIHUDA_FLAG") = constZero
            dr("NHS_FLAG") = constZero
            dr("DENP_FLAG") = constZero
            dr("COA_FLAG") = constZero
            dr("HOKOKU_FLAG") = constZero
            dr("MATOME_PICK_FLAG") = constZero
            dr("LAST_PRINT_DATE") = String.Empty
            dr("LAST_PRINT_TIME") = String.Empty
            dr("SASZ_USER") = String.Empty
            dr("OUTKO_USER") = String.Empty
            dr("KEN_USER") = String.Empty
            dr("OUTKA_USER") = String.Empty
            dr("HOU_USER") = String.Empty
            dr("ORDER_TYPE") = String.Empty

            '2011/08/22 追加項目
            dr("DEST_KB") = "00"
            dr("DEST_NM") = String.Empty
            dr("DEST_AD_1") = String.Empty
            dr("DEST_AD_2") = String.Empty

        End With

        ds.Tables(LMD010C.TABLE_NM_OUTKA_L).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(出荷(中))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutKaM(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        Dim dr As DataRow = ds.Tables(LMD010C.TABLE_NM_OUTKA_M).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr("OUTKA_NO_L") = String.Empty
            dr("OUTKA_NO_M") = "001"
            dr("EDI_SET_NO") = String.Empty
            dr("COA_YN") = String.Empty
            dr("CUST_ORD_NO_DTL") = .txtOrderNo.TextValue()
            dr("BUYER_ORD_NO_DTL") = String.Empty
            dr("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue()
            dr("RSV_NO") = String.Empty
            dr("LOT_NO") = .txtLotNo.TextValue.ToUpper()
            dr("SERIAL_NO") = .txtSerialNo.TextValue()
            dr("ALCTD_KB") = "01"
            'START YANAI No.71
            'dr("OUTKA_PKG_NB") = .numKonsu.Value
            'dr("OUTKA_HASU") = .numCnt.Value
            dr("OUTKA_PKG_NB") = .lblIrisuCnt.Value
            dr("OUTKA_HASU") = Convert.ToString( _
                                                (Convert.ToDecimal(.numKonsu.Value) * _
                                                 Convert.ToDecimal(.lblIrisuCnt.Value) + _
                                                 Convert.ToDecimal(.numCnt.Value)) _
                                                Mod Convert.ToDecimal(.lblIrisuCnt.Value))
            'END YANAI No.71
            '振替個数の総量を算出
            Dim suryo As Decimal = Me.calcMotoSuryoKosu(frm, 0, arr)
            dr("OUTKA_QT") = suryo
            dr("OUTKA_TTL_NB") = Convert.ToDecimal(.lblKosuCnt.TextValue())
            dr("OUTKA_TTL_QT") = suryo
            dr("ALCTD_NB") = Convert.ToDecimal(.lblHikiSumiCnt.TextValue())
            dr("ALCTD_QT") = suryo
            dr("BACKLOG_NB") = LMConst.FLG.OFF
            dr("BACKLOG_QT") = LMConst.FLG.OFF
            dr("UNSO_ONDO_KB") = String.Empty
            dr("IRIME") = String.Empty
            dr("IRIME_UT") = String.Empty
            dr("OUTKA_M_PKG_NB") = String.Empty
            dr("REMARK") = .txtSyukkaRemark.TextValue()
            dr("SIZE_KB") = String.Empty
            dr("ZAIKO_KB") = String.Empty
            dr("SOURCE_CD") = String.Empty
            dr("YELLOW_CARD") = String.Empty
            dr("PRINT_SORT") = "99"

            '2011/08/22 追加項目
            dr("GOODS_CD_NRS_FROM") = String.Empty

        End With

        ds.Tables(LMD010C.TABLE_NM_OUTKA_M).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(出荷(小))
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスを格納したArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetOutKaS(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_OUTKA_S).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_OUTKA_S)

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outkaMDr As DataRow = ds.Tables(LMD010C.TABLE_NM_OUTKA_M).Rows(0)

        'キャッシュの値を取得(商品)
        Dim mGoodsDrs As DataRow() = Nothing
        Dim stdWtKgs As Decimal = 0
        Dim stdIrimeNb As Decimal = 0
        Dim irisu As Integer = 0

        With frm

            'スプレッドシートのチェックの分だけ検索条件をセット
            For i As Integer = 0 To arr.Count - 1

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
                dr("OUTKA_NO_L") = String.Empty
                dr("OUTKA_NO_M") = outkaMDr.Item("OUTKA_NO_M").ToString()
                dr("OUTKA_NO_S") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_INKA_NO_S.ColNo))
                dr("TOU_NO") = String.Empty
                dr("SITU_NO") = String.Empty
                dr("ZONE_CD") = String.Empty
                dr("LOCA") = String.Empty
                dr("LOT_NO") = String.Empty
                dr("SERIAL_NO") = String.Empty
                dr("OUTKA_TTL_NB") = outkaMDr.Item("OUTKA_TTL_NB").ToString()
                dr("OUTKA_TTL_QT") = outkaMDr.Item("OUTKA_TTL_QT").ToString()
                dr("ZAI_REC_NO") = String.Empty
                dr("INKA_NO_L") = String.Empty
                dr("INKA_NO_M") = String.Empty
                dr("INKA_NO_S") = String.Empty
                dr("ZAI_UPD_FLAG") = "01"
                '再計算が必要(BLCで)
                dr("ALCTD_CAN_NB") = Convert.ToDecimal(Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo)))
                dr("ALCTD_NB") = Convert.ToDecimal(Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo)))
                '再計算が必要(BLCで)
                dr("ALCTD_CAN_QT") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo))
                dr("ALCTD_QT") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo))
                dr("IRIME") = String.Empty

                '---↓
                'mGoodsDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", .lblGoodsCdNrs.TextValue, "' "))

                Dim goodsDs As MGoodsDS = New MGoodsDS
                Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                goodsDr.Item("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/17 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
                goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
#End If
                goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                mGoodsDrs = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                '---↑

                If 0 < mGoodsDrs.Length Then

                    '標準重量を取得
                    stdWtKgs = Convert.ToDecimal(mGoodsDrs(0).Item("STD_WT_KGS").ToString())

                    '標準入目
                    stdIrimeNb = Convert.ToDecimal(mGoodsDrs(0).Item("STD_IRIME_NB").ToString())

                End If

                Dim irime As String = String.Empty
                irime = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_IRIME.ColNo))

                dr("BETU_WT") = Convert.ToDecimal(irime) * stdWtKgs / stdIrimeNb
                dr("COA_FLAG") = "00"
                dr("REMARK") = String.Empty
                dr("SMPL_FLAG") = "00"

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_OUTKA_S).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub

    ''' <summary>
    ''' 作業慮料取込チェック用データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetSagyoInForDateChk(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal motosakiFlg As String) As DataSet

        Dim arrSagyo As ArrayList = New ArrayList()
        Dim arrSagyoNm As ArrayList = New ArrayList()
        Dim inTbl As DataTable = Nothing

        If (LMConst.FLG.OFF).Equals(motosakiFlg) = True Then
            '振替元
            inTbl = ds.Tables(LMD010C.TABLE_NM_SAGYO_OUTKA)

            If String.IsNullOrEmpty(frm.txtSagyoCdO1.TextValue) = False Then
                arrSagyo.Add(frm.txtSagyoCdO1.TextValue)
                arrSagyoNm.Add(frm.lblSagyoInNmO1.TextValue)
            End If

            If String.IsNullOrEmpty(frm.txtSagyoCdO2.TextValue) = False Then
                arrSagyo.Add(frm.txtSagyoCdO2.TextValue)
                arrSagyoNm.Add(frm.lblSagyoInNmO2.TextValue)
            End If

            If String.IsNullOrEmpty(frm.txtSagyoCdO3.TextValue) = False Then
                arrSagyo.Add(frm.txtSagyoCdO3.TextValue)
                arrSagyoNm.Add(frm.lblSagyoInNmO3.TextValue)
            End If
        Else
            '振替先
            inTbl = ds.Tables(LMD010C.TABLE_NM_SAGYO_INKA)

            If String.IsNullOrEmpty(frm.txtSagyoCdN1.TextValue) = False Then
                arrSagyo.Add(frm.txtSagyoCdN1.TextValue)
                arrSagyoNm.Add(frm.lblSagyoInNmN1.TextValue)
            End If

            If String.IsNullOrEmpty(frm.txtSagyoCdN2.TextValue) = False Then
                arrSagyo.Add(frm.txtSagyoCdN2.TextValue)
                arrSagyoNm.Add(frm.lblSagyoInNmN2.TextValue)
            End If

            If String.IsNullOrEmpty(frm.txtSagyoCdN3.TextValue) = False Then
                arrSagyo.Add(frm.txtSagyoCdN3.TextValue)
                arrSagyoNm.Add(frm.lblSagyoInNmN3.TextValue)
            End If

        End If

        With frm

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            Dim dr As DataRow = Nothing

            For i As Integer = 0 To arrSagyo.Count - 1

                dr = inTbl.NewRow()
                dr("SAGYO_CD") = arrSagyo(i).ToString()
                dr("SAGYO_NM") = arrSagyoNm(i).ToString()

                inTbl.Rows.Add(dr)

            Next

        End With

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(振替先作業)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetSagyoInka(ByVal frm As LMD010F, ByVal ds As DataSet)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_SAGYO_INKA).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_SAGYO_INKA)

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim inkaLDr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_L).Rows(0)
        Dim inkaMDr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_M).Rows(0)

        Dim arrSagyo As ArrayList = New ArrayList()
        Dim arrSagyoNm As ArrayList = New ArrayList()

        If String.IsNullOrEmpty(frm.txtSagyoCdN1.TextValue) = False Then
            arrSagyo.Add(frm.txtSagyoCdN1.TextValue)
            arrSagyoNm.Add(frm.lblSagyoInNmN1.TextValue)
        End If

        If String.IsNullOrEmpty(frm.txtSagyoCdN2.TextValue) = False Then
            arrSagyo.Add(frm.txtSagyoCdN2.TextValue)
            arrSagyoNm.Add(frm.lblSagyoInNmN2.TextValue)
        End If

        If String.IsNullOrEmpty(frm.txtSagyoCdN3.TextValue) = False Then
            arrSagyo.Add(frm.txtSagyoCdN3.TextValue)
            arrSagyoNm.Add(frm.lblSagyoInNmN3.TextValue)
        End If


        With frm

            For i As Integer = 0 To arrSagyo.Count - 1

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("SAGYO_COMP") = "01"
                dr("SKYU_CHK") = "00"
                dr("SAGYO_REC_NO") = String.Empty
                dr("SAGYO_SIJI_NO") = String.Empty
                dr("INOUTKA_NO_LM") = String.Empty
                dr("NRS_BR_CD") = inkaLDr.Item("NRS_BR_CD").ToString()
                dr("WH_CD") = inkaLDr.Item("WH_CD").ToString()
                dr("IOZS_KB") = "11"
                dr("SAGYO_CD") = arrSagyo(i).ToString()
                dr("SAGYO_NM") = arrSagyoNm(i).ToString()
                dr("DEST_SAGYO_FLG") = "00"
                dr("CUST_CD_L") = inkaLDr.Item("CUST_CD_L").ToString()
                dr("CUST_CD_M") = inkaLDr.Item("CUST_CD_M").ToString()
                dr("DEST_CD") = String.Empty
                dr("DEST_NM") = String.Empty
                dr("GOODS_CD_NRS") = inkaMDr.Item("GOODS_CD_CUST").ToString()
                dr("GOODS_NM_NRS") = .txtGoodsNmCustNew.TextValue
                dr("LOT_NO") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(0, LMD010G.sprDetailNewDef.SAKI_LOT_NO.ColNo)).ToUpper()
                'キャッシュの値(作業項目M)
                Dim mSagyoDrs As DataRow() = Nothing
                Dim invTani As String = String.Empty
                Dim sagyoUp As String = LMConst.FLG.OFF
                Dim sagyoNb As String = LMConst.FLG.OFF
                Dim taxKbn As String = String.Empty

                'キャッシュの値(作業項目M)
                'START YANAI 要望番号376
                'mSagyoDrs = Me._LMDConV.SelectSagyoListDataRow(inkaLDr.Item("NRS_BR_CD").ToString(), arrSagyo(i).ToString(), inkaLDr.Item("CUST_CD_L").ToString())
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", arrSagyo(i).ToString(), "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", inkaLDr.Item("NRS_BR_CD").ToString(), "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", inkaLDr.Item("CUST_CD_L").ToString(), "' OR CUST_CD_L = 'ZZZZZ')")

                mSagyoDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If 0 < mSagyoDrs.Length Then
                    invTani = mSagyoDrs(0).Item("INV_TANI").ToString()
                    sagyoUp = mSagyoDrs(0).Item("SAGYO_UP").ToString()
                    taxKbn = mSagyoDrs(0).Item("ZEI_KBN").ToString()

                    '請求金額計算区分 = '01'の場合、今回請求数を1とする
                    If "01".Equals(mSagyoDrs(0).Item("KOSU_BAI").ToString()) = True Then
                        sagyoNb = "1"
                    Else
                        '上記以外の場合、入荷総個数を今回請求数とする
                        sagyoNb = inkaLDr.Item("INKA_TTL_NB").ToString()
                    End If

                End If

                dr("INV_TANI") = invTani
                dr("SAGYO_NB") = sagyoNb
                dr("SAGYO_UP") = sagyoUp
                'START YANAI メモ②No.16
                'dr("SAGYO_GK") = (Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNb)).ToString()
                dr("SAGYO_GK") = Convert.ToString(Math.Round((Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNb)), MidpointRounding.AwayFromZero))
                'END YANAI メモ②No.16
                dr("TAX_KB") = taxKbn
                dr("SEIQTO_CD") = String.Empty
                dr("REMARK_ZAI") = String.Empty
                dr("REMARK_SKYU") = String.Empty
                dr("SAGYO_COMP_CD") = MyBase.GetUserID.ToString()
                dr("SAGYO_COMP_DATE") = .imdFurikaeDate.TextValue

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_SAGYO_INKA).ImportRow(inTbl.Rows(0))

            Next


        End With

    End Sub

    ''' <summary>
    ''' データセット設定(振替元作業)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetSagyoOutka(ByVal frm As LMD010F, ByVal ds As DataSet)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_SAGYO_OUTKA).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_SAGYO_OUTKA)

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim outokaLDr As DataRow = ds.Tables(LMD010C.TABLE_NM_OUTKA_L).Rows(0)
        Dim outkaMDr As DataRow = ds.Tables(LMD010C.TABLE_NM_OUTKA_M).Rows(0)

        Dim arrSagyo As ArrayList = New ArrayList()
        Dim arrSagyoNm As ArrayList = New ArrayList()

        If String.IsNullOrEmpty(frm.txtSagyoCdO1.TextValue) = False Then
            arrSagyo.Add(frm.txtSagyoCdO1.TextValue)
            arrSagyoNm.Add(frm.lblSagyoInNmO1.TextValue)
        End If

        If String.IsNullOrEmpty(frm.txtSagyoCdO2.TextValue) = False Then
            arrSagyo.Add(frm.txtSagyoCdO2.TextValue)
            arrSagyoNm.Add(frm.lblSagyoInNmO2.TextValue)
        End If

        If String.IsNullOrEmpty(frm.txtSagyoCdO3.TextValue) = False Then
            arrSagyo.Add(frm.txtSagyoCdO3.TextValue)
            arrSagyoNm.Add(frm.lblSagyoInNmO3.TextValue)
        End If


        With frm

            For i As Integer = 0 To arrSagyo.Count - 1

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("SAGYO_COMP") = "01"
                dr("SKYU_CHK") = "00"
                dr("SAGYO_REC_NO") = String.Empty
                dr("SAGYO_SIJI_NO") = String.Empty
                dr("INOUTKA_NO_LM") = String.Empty
                dr("NRS_BR_CD") = outokaLDr.Item("NRS_BR_CD").ToString()
                dr("WH_CD") = outokaLDr.Item("WH_CD").ToString()
                dr("IOZS_KB") = "21"
                dr("SAGYO_CD") = arrSagyo(i).ToString()
                dr("SAGYO_NM") = arrSagyoNm(i).ToString()
                dr("DEST_SAGYO_FLG") = "00"
                dr("CUST_CD_L") = outokaLDr.Item("CUST_CD_L").ToString()
                dr("CUST_CD_M") = outokaLDr.Item("CUST_CD_M").ToString()
                dr("DEST_CD") = String.Empty
                dr("DEST_NM") = String.Empty
                dr("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                dr("GOODS_NM_NRS") = .txtGoodsNmCust.TextValue
                dr("LOT_NO") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(0, LMD010G.sprDetailDef.MOTO_LOT_NO.ColNo)).ToUpper()

                'キャッシュの値(作業項目M)
                Dim mSagyoDrs As DataRow() = Nothing
                Dim invTani As String = String.Empty
                Dim sagyoUp As String = LMConst.FLG.OFF
                Dim sagyoNb As String = LMConst.FLG.OFF
                Dim taxKbn As String = String.Empty

                'キャッシュの値(作業項目M)
                'START YANAI 要望番号376
                'mSagyoDrs = Me._LMDConV.SelectSagyoListDataRow(inkaLDr.Item("NRS_BR_CD").ToString(), arrSagyo(i).ToString(), outokaLDr.Item("CUST_CD_L").ToString())
                Dim SelectSagyoString As String = String.Empty
                '削除フラグ
                SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
                '作業コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", arrSagyo(i).ToString(), "' ")
                '営業所コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", outokaLDr.Item("NRS_BR_CD").ToString(), "' ")
                '荷主コード
                SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", outokaLDr.Item("CUST_CD_L").ToString(), "' OR CUST_CD_L = 'ZZZZZ')")

                mSagyoDrs = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
                'END YANAI 要望番号376
                If 0 < mSagyoDrs.Length Then
                    invTani = mSagyoDrs(0).Item("INV_TANI").ToString()
                    sagyoUp = mSagyoDrs(0).Item("SAGYO_UP").ToString()
                    taxKbn = mSagyoDrs(0).Item("ZEI_KBN").ToString()

                    '請求金額計算区分 = '01'の場合、今回請求数を1とする
                    If "01".Equals(mSagyoDrs(0).Item("KOSU_BAI").ToString()) = True Then
                        sagyoNb = "1"
                    Else
                        '上記以外の場合、出荷総個数を今回請求数とする
                        sagyoNb = outkaMDr.Item("OUTKA_TTL_NB").ToString()
                    End If

                End If

                dr("INV_TANI") = invTani
                dr("SAGYO_NB") = sagyoNb
                dr("SAGYO_UP") = sagyoUp
                'START YANAI メモ②No.16
                'dr("SAGYO_GK") = (Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNb)).ToString()
                dr("SAGYO_GK") = Convert.ToString(Math.Round((Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(sagyoNb)), MidpointRounding.AwayFromZero))
                'END YANAI メモ②No.16
                dr("TAX_KB") = taxKbn
                dr("SEIQTO_CD") = String.Empty
                dr("REMARK_ZAI") = String.Empty
                dr("REMARK_SKYU") = String.Empty
                dr("SAGYO_COMP_CD") = MyBase.GetUserID.ToString()
                dr("SAGYO_COMP_DATE") = .imdFurikaeDate.TextValue

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_SAGYO_OUTKA).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(振替先在庫)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスを格納したArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetZaikoNew(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_ZAI_NEW).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_ZAI_NEW)

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim inkaLDr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_L).Rows(0)
        Dim inkaMDr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_M).Rows(0)

        With frm

            'スプレッドシートのチェックの分だけ検索条件をセット
            For i As Integer = 0 To arr.Count - 1

                Dim inkaSDr As DataRow = ds.Tables(LMD010C.TABLE_NM_INKA_S).Rows(i)

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_ZAI_REC_NO.ColNo))
                dr("WH_CD") = .cmbSoko.SelectedValue
                dr("TOU_NO") = inkaSDr.Item("TOU_NO").ToString()
                dr("SITU_NO") = inkaSDr.Item("SITU_NO").ToString()
                dr("ZONE_CD") = inkaSDr.Item("ZONE_CD").ToString()
                dr("LOCA") = inkaSDr.Item("LOCA").ToString()
                dr("LOT_NO") = inkaSDr.Item("LOT_NO").ToString()
                dr("CUST_CD_L") = inkaLDr.Item("CUST_CD_L").ToString()
                dr("CUST_CD_M") = inkaLDr.Item("CUST_CD_M").ToString()
                dr("GOODS_CD_NRS") = inkaMDr.Item("GOODS_CD_NRS").ToString()
                'START ADD 2013/09/10 KOBAYASHI WIT対応
                dr("GOODS_KANRI_NO") = String.Empty
                'END   ADD 2013/09/10 KOBAYASHI WIT対応
                dr("INKA_NO_L") = String.Empty
                dr("INKA_NO_M") = inkaSDr.Item("INKA_NO_M").ToString()
                dr("INKA_NO_S") = inkaSDr.Item("INKA_NO_S").ToString()
                dr("ALLOC_PRIORITY") = inkaSDr.Item("ALLOC_PRIORITY").ToString()
                dr("RSV_NO") = String.Empty
                dr("SERIAL_NO") = inkaSDr.Item("SERIAL_NO").ToString()
                dr("HOKAN_YN") = String.Empty
                dr("TAX_KB") = .cmbTaxKbnNew.SelectedValue
                dr("GOODS_COND_KB_1") = inkaSDr.Item("GOODS_COND_KB_1").ToString()
                dr("GOODS_COND_KB_2") = inkaSDr.Item("GOODS_COND_KB_2").ToString()
                dr("GOODS_COND_KB_3") = inkaSDr.Item("GOODS_COND_KB_3").ToString()
                dr("OFB_KB") = inkaSDr.Item("OFB_KB").ToString()
                dr("SPD_KB") = inkaSDr.Item("SPD_KB").ToString()
                dr("REMARK_OUT") = inkaSDr.Item("REMARK_OUT").ToString()
                dr("PORA_ZAI_NB") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo))
                dr("ALCTD_NB") = LMConst.FLG.OFF
                dr("ALLOC_CAN_NB") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo))
                dr("IRIME") = inkaSDr.Item("IRIME").ToString()
                dr("PORA_ZAI_QT") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo))
                dr("ALCTD_QT") = LMConst.FLG.OFF
                dr("ALLOC_CAN_QT") = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo))

                '「初期入荷日を引き継いで在庫を振替える」の判定
                If .chkInkoDateUmu.Checked = True Then
                    dr("INKO_DATE") = DateFormatUtility.DeleteSlash(Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_INKA_DATE.ColNo)))
                Else
                    dr("INKO_DATE") = inkaLDr.Item("INKA_DATE").ToString()
                End If

                dr("INKO_PLAN_DATE") = inkaLDr.Item("INKA_DATE").ToString()
                dr("ZERO_FLAG") = LMConst.FLG.OFF
                dr("LT_DATE") = inkaSDr.Item("LT_DATE").ToString()
                dr("GOODS_CRT_DATE") = inkaSDr.Item("GOODS_CRT_DATE").ToString()
                dr("DEST_CD") = inkaSDr.Item("DEST_CD").ToString()
                dr("REMARK") = inkaSDr.Item("REMARK").ToString()

                Dim bykKeepGoodsCd As String = ""
                If .sprDtlNew.ActiveSheet.Columns(LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo).Visible Then
                    bykKeepGoodsCd = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_BYK_KEEP_GOODS_CD.ColNo))
                End If
                dr("BYK_KEEP_GOODS_CD") = bykKeepGoodsCd

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_ZAI_NEW).ImportRow(inTbl.Rows(0))

            Next


        End With


    End Sub

    ''' <summary>
    ''' データセット設定(振替元在庫)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="arr">スプレッドシートのチェックボックスを格納したArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetZaikoOld(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD010C.TABLE_NM_ZAI_OLD).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD010C.TABLE_NM_ZAI_OLD)

        With frm

            'スプレッドシートのチェックの分だけ検索条件をセット
            For i As Integer = 0 To arr.Count - 1

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_ZAI_REC.ColNo))

                'BLCで再計算項目
                dr("PORA_ZAI_NB") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo))
                dr("ALLOC_CAN_NB") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo))
                dr("PORA_ZAI_QT") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo))
                dr("ALLOC_CAN_QT") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo))

                'BLCで再計算項目
                dr("TAX_KB") = .cmbTaxKbn.SelectedValue

                dr("GUI_SYS_UPD_DATE") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_UPDATE_DATE.ColNo))
                dr("GUI_SYS_UPD_TIME") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_UPDATE_TIME.ColNo))

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD010C.TABLE_NM_ZAI_OLD).ImportRow(inTbl.Rows(0))

            Next


        End With


    End Sub

    ''' <summary>
    ''' 振替先の振替個数、振替数量を計算して返す
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="flg">数量、個数を返すフラグ 0⇒数量を返却　1⇒個数を返却</param>
    ''' <param name="arr">振替元or振替先のチェックボックスリスト</param>
    ''' <remarks></remarks>
    Friend Function calcSuryoKosu(ByVal frm As LMD010F, ByVal flg As Integer, ByVal arr As ArrayList) As Decimal

        '返却する変数
        Dim rtnValue As Decimal = 0

        'スプレッドシートのチェックの分だけ検索条件をセット
        For i As Integer = 0 To arr.Count - 1

            'フラグによって、数量、個数の計算を可変させる
            If flg = 0 Then
                '数量の計算
                rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo)))
            Else
                '個数の計算
                rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprDtlNew.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)))
            End If

        Next

        Return rtnValue

    End Function

    ''' <summary>
    ''' 振替元の振替個数、振替数量を計算して返す
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="flg">数量、個数を返すフラグ 0⇒数量を返却　1⇒個数を返却</param>
    ''' <param name="arr">振替元or振替先のチェックボックスリスト</param>
    ''' <remarks></remarks>
    Friend Function calcMotoSuryoKosu(ByVal frm As LMD010F, ByVal flg As Integer, ByVal arr As ArrayList) As Decimal

        '返却する変数
        Dim rtnValue As Decimal = 0

        'スプレッドシートのチェックの分だけ検索条件をセット
        For i As Integer = 0 To arr.Count - 1

            'フラグによって、数量、個数の計算を可変させる
            If flg = 0 Then
                '数量の計算
                rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_SURYO.ColNo)))
            Else
                '個数の計算
                rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.spdDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo)))
            End If

        Next

        Return rtnValue

    End Function

    ''' <summary>
    ''' 振替伝票用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMD600InDataSet(ByVal frm As LMD010F, Optional ByRef PreintFLG As String = "") As DataSet

        Dim inDs As DSL.LMD600DS = New DSL.LMD600DS
        Dim dt As DataTable = inDs.Tables("LMD600IN")
        Dim dr As DataRow = dt.NewRow()
        With frm

            dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue()
            dr.Item("FURI_NO") = .lblFurikaeNo.TextValue()

#If False Then      'UPD 2018/12/20 依頼番号 : 003818   【LMS】ITWセミEDI_エクセル取込時(千葉並木)

            dr.Item("FURIKAE_KBN") = .cmbFurikaeKbn.SelectedValue()
            Dim youkiChange As String = String.Empty
            If .chkYoukiChange.Checked = True Then
                youkiChange = "20"
            Else
                youkiChange = "30"
            End If
            dr.Item("YOUKI_HENKO") = youkiChange
            dr.Item("OUT_TAX_KB") = .cmbTaxKbn.SelectedValue()

#Else
            If (String.Empty).Equals(PreintFLG) Then

                dr.Item("FURIKAE_KBN") = .cmbFurikaeKbn.SelectedValue()
                Dim youkiChange As String = String.Empty
                If .chkYoukiChange.Checked = True Then
                    youkiChange = "20"
                Else
                    youkiChange = "30"
                End If
                dr.Item("YOUKI_HENKO") = youkiChange
                dr.Item("OUT_TAX_KB") = .cmbTaxKbn.SelectedValue()

                Dim sysdate As String() = MyBase.GetSystemDateTime()
                dr.Item("FURIKAEBI") = sysdate(0)   '--システム日付

            End If

#End If



        End With
        dt.Rows.Add(dr)

        Return inDs

    End Function


#If True Then       'ADD 2019/01/08 依頼番号 : 004100   【LMS】在庫振替の新規画面追加・編集・削除・再印刷機能の追加
    ''' <summary>
    ''' データセット設定(振替データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDataseFurikae(ByVal frm As LMD010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        Dim dr As DataRow = ds.Tables(LMD010C.TABLE_NM_FURIKAE).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr("WH_CD") = .cmbSoko.SelectedValue.ToString()
            dr("FURI_NO") = String.Empty
            dr("FURI_DATE") = .imdFurikaeDate.TextValue()
            dr("FURI_KBN") = .cmbFurikaeKbn.SelectedValue.ToString()
            Dim youkiChange As String = String.Empty
            If .chkYoukiChange.Checked = True Then
                youkiChange = "20"      '変更有
            Else
                youkiChange = "30"      '変更無
            End If
            dr("YOUKI_HENKO_KBN") = youkiChange

            dr("TAX_KBN") = .cmbTaxKbnNew.SelectedValue.ToString()
            dr("HOKAN_ALLOC_KBN") = .cmbToukiHokanKbn.SelectedValue.ToString()
            dr("MOTO_CUST_CD_L") = .txtCustCdL.TextValue()
            dr("MOTO_CUST_CD_M") = .txtCustCdM.TextValue()
            dr("MOTO_ORD_NO") = .txtOrderNo.TextValue()
            dr("MOTO_GOODS_CD_NRS") = .txtGoodsCdCust.TextValue()
            dr("MOTO_SAGYO_CD1") = .txtSagyoCdO1.TextValue()
            dr("MOTO_SAGYO_CD2") = .txtSagyoCdO2.TextValue()
            dr("MOTO_SAGYO_CD3") = .txtSagyoCdO3.TextValue()
            dr("MOTO_REMARK") = .txtSyukkaRemark.TextValue()

            dr("SAKI_CUST_CD_L") = .txtCustCdLNew.TextValue()
            dr("SAKI_CUST_CD_M") = .txtCustCdMNew.TextValue()
            dr("SAKI_GOODS_CD_NRS") = .txtGoodsCdCustNew.TextValue()
            dr("SAKI_SAGYO_CD1") = .txtSagyoCdN1.TextValue()
            dr("SAKI_SAGYO_CD2") = .txtSagyoCdN2.TextValue()
            dr("SAKI_SAGYO_CD3") = .txtSagyoCdN3.TextValue()
            dr("SAKI_REMARK") = .txtNyukaRemark.TextValue()

        End With

        ds.Tables(LMD010C.TABLE_NM_FURIKAE).Rows.Add(dr)

    End Sub
#End If

#End Region

#Region "計算"

    ''' <summary>
    ''' 全行計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function AllCalculation(ByVal frm As LMD010F) As Boolean

        '全行計算
        Dim max As Integer = frm.sprDtlNew.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max

            If Me.Calculation(frm, i) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Calculation(ByVal frm As LMD010F, ByVal rowNo As Integer) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With frm.sprDtlNew.ActiveSheet


            Dim kosu As Decimal = Convert.ToDecimal(Me.FormatNumValue(Me._LMDConV.GetCellValue(.Cells(rowNo, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo))))
            Dim irime As Decimal = Convert.ToDecimal(Me.FormatNumValue(Me._LMDConV.GetCellValue(.Cells(rowNo, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo))))
            Dim value As String = (kosu * irime).ToString()
            Dim msg As String = String.Empty

            '2017/09/25 修正 李↓
            msg = lgm.Selector({"振替個数", "Transfer number", "대체개수", "中国語"})
            '2017/09/25 修正 李↑

            'START YANAI メモNo.88
            '振替個数のチェック
            If Me._V.IsCalcOver(Convert.ToString(kosu), LMD010C.FURI_KOSU_MIN, LMD010C.FURI_KOSU_MAX, msg) = False Then

                Me._LMDConV.SetErrorControl(frm.sprDtlNew, rowNo, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)
                Return False

            End If
            'END YANAI メモNo.88

            '2017/09/25 修正 李↓
            msg = lgm.Selector({"振替数量", "Transfer quantity", "대체수량", "中国語"})
            '2017/09/25 修正 李↑

            '振替数量のチェック
            If Me._V.IsCalcOver(value, LMD010C.FURI_SURYO_MIN, LMD010C.FURI_SURYO_MAX, msg) = False Then

                .Cells(rowNo, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMDConV.SetErrorControl(frm.sprDtlNew, rowNo, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)
                Return False

            End If

            '個数 * 入目を設定
            frm.sprDtlNew.SetCellValue(rowNo, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo, value)

            'START YANAI メモNo.88
            Dim max As Integer = .Rows.Count - 1
            Dim suryo As Decimal = 0
            kosu = 0
            For i As Integer = 0 To max
                'スプレッドの振替個数・数量の合計を求める
                kosu = kosu + Convert.ToDecimal(Me.FormatNumValue(Me._LMDConV.GetCellValue(.Cells(i, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo))))
                suryo = suryo + Convert.ToDecimal(Me.FormatNumValue(Me._LMDConV.GetCellValue(.Cells(i, LMD010G.sprDetailNewDef.SAKI_HURIKAE_SURYO.ColNo))))
            Next

            '2017/09/25 修正 李↓
            msg = lgm.Selector({"振替個数の合計", "The sum of the transfer number", "대체개수의합계", "中国語"})
            '2017/09/25 修正 李↑

            '振替個数合計のチェック
            If Me._V.IsCalcOver(Convert.ToString(kosu), LMD010C.FURI_KOSU_MIN, LMD010C.FURI_KOSU_MAX, msg) = False Then

                For i As Integer = 0 To max
                    Me._LMDConV.SetErrorControl(frm.sprDtlNew, i, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)
                Next
                Return False

            End If

            '2017/09/25 修正 李↓
            msg = lgm.Selector({"振替数量の合計", "The sum of the transfer quantity", "대체수량의합계", "中国語"})
            '2017/09/25 修正 李↑

            '振替数量合計のチェック
            If Me._V.IsCalcOver(Convert.ToString(suryo), LMD010C.FURI_SURYO_MIN, LMD010C.FURI_SURYO_MAX, msg) = False Then

                For i As Integer = 0 To max
                    .Cells(i, LMD010G.sprDetailNewDef.SAKI_IRIME.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._LMDConV.SetErrorControl(frm.sprDtlNew, i, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)
                Next
                Return False

            End If
            'END YANAI メモNo.88

        End With

        Return True

    End Function

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 新規入荷チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrNew">チェック行群</param>
    ''' <param name="ikkatsu">0：行毎、1：一括</param>
    ''' <param name="expDs"></param>
    ''' <returns></returns>
    Friend Function IsTouSituZoneCheck(ByVal frm As LMD010F, ByVal arrNew As ArrayList, ByVal ikkatsu As String, ByVal expDs As DataSet) As Boolean

        With frm

            Dim sakiMax As Integer = arrNew.Count - 1
            Dim checkRow As Integer = 0

            Dim nrsbrcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim custcd As String = .txtCustCdLNew.TextValue

            Dim goodsNRS As String = String.Empty

            Dim tousituDr As DataRow() = Nothing
            Dim soko_kbn As String = String.Empty
            Dim isTasya As Boolean = False

            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim InkaNb As Decimal

            Dim msg As String = String.Empty

            'チェック対象行がない場合は終了
            If 0 > sakiMax Then Return True

            '新規入荷チェックを行うか、荷主明細マスタ(M_CUST_DETAILS)の用途区分（荷主明細）(Y008).新規入荷チェック不要フラグ(A2)で判定
            'DataSet設定
            Dim chkDs As DataSet = New LMZ340DS()
            Dim inTbl As DataTable = chkDs.Tables(LMZ340C.TABLE_NM_IN)
            Dim row As DataRow = inTbl.NewRow

            '最大保管数量取得用
            Dim capaDs As DataSet = New LMZ340DS()
            Dim capaInTbl As DataTable = capaDs.Tables(LMZ340C.TABLE_NM_IN)

            row("NRS_BR_CD") = nrsbrcd
            row("CUST_CD") = custcd

            inTbl.Rows.Add(row)

            chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckFlg", chkDs)

            If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_FLG).Rows.Count > 0 Then
                Dim flgA2 As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_FLG).Rows(0).Item("FLG_A2").ToString

                'フラグが1で設定されている場合、エラーなしで処理終了
                If "1".Equals(flgA2) Then
                    Return True
                End If

            End If

            '★★★属性チェック
            For i As Integer = 0 To sakiMax

                checkRow = Convert.ToInt32(arrNew(i).ToString)

                '商品マスタ
                goodsNRS = .lblGoodsCdNrsNew.TextValue.ToString

                touNo = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_TOU_NO.ColNo)).ToString
                situNo = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_SITU_NO.ColNo)).ToString
                zoneCd = Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_ZONE_CD.ColNo)).ToString
                InkaNb = Convert.ToDecimal(Me._LMDConV.GetCellValue(.sprDtlNew.ActiveSheet.Cells(checkRow, LMD010G.sprDetailNewDef.SAKI_HURIKAE_KOSU.ColNo)))

                '棟室マスタ
                tousituDr = Me._LMDConV.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)

                '棟室マスタが取得できなければエラー、ワーニングとも起こさせない。
                If tousituDr.Length.Equals(0) Then
                    Continue For
                End If

                soko_kbn = tousituDr(0).Item("SOKO_KB").ToString
                isTasya = tousituDr(0).Item("JISYATASYA_KB").ToString.Equals("02")

                '他社の場合はワーニングを出さない
                If isTasya.Equals(False) Then

                    msg = String.Concat(touNo, "-", situNo)
                    If String.IsNullOrEmpty(zoneCd) = False Then
                        msg = String.Concat(msg, "-", zoneCd)
                    End If

                    '属性系チェック
                    'DataSet設定
                    chkDs = New LMZ340DS()
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                    row = inTbl.NewRow

                    row("NRS_BR_CD") = nrsbrcd
                    row("GOODS_CD_NRS") = goodsNRS
                    row("WH_CD") = sokocd
                    row("TOU_NO") = touNo
                    row("SITU_NO") = situNo
                    row("ZONE_CD") = zoneCd

                    inTbl.Rows.Add(row)

                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckAttr", chkDs)

                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows.Count > 0 Then
                        Dim DokuKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("DOKU_KB_ERR").ToString
                        Dim KouathuGasKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("KOUATHUGAS_KB_ERR").ToString
                        Dim YakuzihoKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("YAKUZIHO_KB_ERR").ToString
                        Dim ShoboCdErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("SHOBO_CD_ERR").ToString

                        'フラグが1で設定されている場合、エラー
                        If "1".Equals(DokuKbErr) Then
                            If (MyBase.ShowMessage(frm, "W299", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                        If "1".Equals(KouathuGasKbErr) Then
                            If (MyBase.ShowMessage(frm, "W300", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                        If "1".Equals(YakuzihoKbErr) Then
                            If (MyBase.ShowMessage(frm, "W301", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                        If "1".Equals(ShoboCdErr) Then
                            If (MyBase.ShowMessage(frm, "W302", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                    End If

                    '最大保管数量用にDataSet設定
                    Dim capaRow As DataRow = capaInTbl.NewRow

                    capaRow("NRS_BR_CD") = nrsbrcd
                    capaRow("GOODS_CD_NRS") = goodsNRS
                    capaRow("WH_CD") = sokocd
                    capaRow("TOU_NO") = touNo
                    capaRow("SITU_NO") = situNo
                    capaRow("ZONE_CD") = zoneCd
                    capaRow("INKA_NB") = InkaNb

                    capaInTbl.Rows.Add(capaRow)

                End If

            Next

            '★★★最大保管数量チェック
            If capaDs.Tables(LMZ340C.TABLE_NM_IN).Rows.Count = 0 Then
                Return True
            End If

            '入荷(小)を棟室順に処理
            Dim drSCapa As DataRow() = capaDs.Tables(LMZ340C.TABLE_NM_IN).Select(Nothing, "TOU_NO ASC, SITU_NO ASC")
            Dim maxSCapa As Integer = drSCapa.Length - 1

            '入荷(小)がない場合は終了
            If 0 > maxSCapa Then Return True

            'ブレイクキー
            Dim keyTouNo As String = String.Empty
            Dim keySituNo As String = String.Empty
            Dim keyTouSituSkip As Boolean = False

            '棟室の貯蔵最大数量
            Dim MaxQty As Decimal
            '棟室の現在の在庫の数量
            Dim ZaiQty As Decimal
            '入庫可能商品数量
            Dim chkQty As Decimal
            '移動元商品数量
            Dim motoQty As Decimal

            '棟室マスタ
            For j As Integer = 0 To maxSCapa

                If Not keyTouNo.Equals(drSCapa(j).Item("TOU_NO").ToString()) _
                    Or Not keySituNo.Equals(drSCapa(j).Item("SITU_NO").ToString()) Then

                    '棟室が変わったら、最大保管数量を取得
                    keyTouNo = drSCapa(j).Item("TOU_NO").ToString()
                    keySituNo = drSCapa(j).Item("SITU_NO").ToString()

                    MaxQty = 0
                    ZaiQty = 0
                    chkQty = 0
                    motoQty = 0
                    keyTouSituSkip = False

                    msg = String.Concat(keyTouNo, "-", keySituNo)

                    'DataSet設定
                    chkDs = New LMZ340DS()
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                    row = inTbl.NewRow

                    row("NRS_BR_CD") = nrsbrcd
                    row("WH_CD") = sokocd
                    row("TOU_NO") = keyTouNo
                    row("SITU_NO") = keySituNo

                    inTbl.Rows.Add(row)

                    '貯蔵最大数量検索
                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckCapa", chkDs)

                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows.Count > 0 Then
                        MaxQty = Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows(0).Item("MAX_QTY").ToString)
                        ZaiQty = Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows(0).Item("ZAI_QTY").ToString)
                    End If

                    '貯蔵最大数量 が 0 の場合、チェック対象外とする。
                    If MaxQty <= 0 Then
                        keyTouSituSkip = True
                    Else
                        '移動元に対象の棟室があるか確認
                        For k As Integer = 0 To frm.spdDtl.ActiveSheet.Rows.Count - 1 'motoMax

                            '移動元に対象の棟室がある場合は在庫に加算
                            If keyTouNo.Equals(Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(k, LMD010G.sprDetailDef.MOTO_TOU_NO.ColNo)).ToString) _
                                And keySituNo.Equals(Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(k, LMD010G.sprDetailDef.MOTO_SITU_NO.ColNo)).ToString) Then

                                '移動元商品数量計算
                                'DataSet設定
                                chkDs = New LMZ340DS()
                                inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                                row = inTbl.NewRow

                                row("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                                row("GOODS_CD_NRS") = .lblGoodsCdNrs.TextValue
                                row("INKA_NB") = Me._LMDConV.GetCellValue(.spdDtl.ActiveSheet.Cells(k, LMD010G.sprDetailDef.MOTO_HURIKAE_KOSU.ColNo)).ToString

                                inTbl.Rows.Add(row)

                                '入庫商品数量を計算
                                chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCalcQty", chkDs)

                                If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 Then
                                    '入庫可能商品数量に計算した入庫商品数量をプラス
                                    motoQty = motoQty + Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString)
                                End If
                            End If

                        Next

                    End If

                    '入庫可能商品数量を計算
                    chkQty = MaxQty - ZaiQty + motoQty

                End If

                '貯蔵最大数量 が 0 または 既にエラーとなっている棟室はスキップ
                If keyTouSituSkip = False Then

                    '対象商品数量計算
                    'DataSet設定
                    chkDs = New LMZ340DS()
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                    row = inTbl.NewRow

                    '振替先の対象行
                    row("NRS_BR_CD") = drSCapa(j).Item("NRS_BR_CD").ToString()
                    row("GOODS_CD_NRS") = drSCapa(j).Item("GOODS_CD_NRS").ToString()
                    row("INKA_NB") = drSCapa(j).Item("INKA_NB").ToString()

                    inTbl.Rows.Add(row)

                    '入庫商品数量を計算
                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCalcQty", chkDs)

                    'UPD 2021/10/04 024123 【LMS】危険物管理_第2弾_アラート機能実装_再実装
                    '危険品でない場合処理しない
                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 AndAlso
                        Not Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString).Equals(0) Then

                        If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 Then
                            '入庫可能商品数量から計算した入庫商品数量をマイナス
                            chkQty = chkQty - Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString)
                        End If

                        '入庫商品数量 >入庫可能商品数量 の場合
                        If chkQty < 0 Then
                            If (MyBase.ShowMessage(frm, "W298", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            keyTouSituSkip = True
                        End If

                    End If


                End If

            Next

        End With

        Return True

    End Function

#End Region

#Region "荷主明細 BYKキープ品管理 有無 判定"

    ''' <summary>
    ''' 荷主明細 BYKキープ品管理 有無 判定
    ''' </summary>
    ''' <returns></returns>
    Private Function IsBykKeepGoodsCd() As Boolean

        Dim ret As Boolean = False

        Dim ds As DataSet = New LMD010DS()
        Dim dr As DataRow = ds.Tables("LMD010_CUST_DETAILS_IN").NewRow
        dr.Item("NRS_BR_CD") = _Frm.cmbNrsBrCd.SelectedValue
        dr.Item("CUST_CD_L") = _Frm.txtCustCdLNew.TextValue
        dr.Item("CUST_CD_M") = _Frm.txtCustCdMNew.TextValue
        dr.Item("CUST_CLASS") = "01"
        dr.Item("SUB_KB") = "1Z"
        ds.Tables("LMD010_CUST_DETAILS_IN").Rows.Add(dr)

        ' 荷主明細 BYKキープ品管理 有無 の取得
        ds = MyBase.CallWSA("LMD010BLF", "SelectIsBykKeepGoodsCd", ds)

        If ds.Tables("LMD010_IS_BYK_KEEP_GOODS_CD").Rows.Count > 0 AndAlso
            ds.Tables("LMD010_IS_BYK_KEEP_GOODS_CD").Rows(0).Item("IS_BYK_KEEP_GOODS_CD").ToString() = "1" Then
            ret = True
        End If

        Return ret

    End Function

#End Region ' "荷主明細 BYKキープ品管理 有無 判定"

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        If Me._Frm.FunctionKey.F1ButtonName = "再印刷" Then
            Call SaiPrintINIT(frm)
        Else
            Call SaiPrintRTN(frm)



        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(複写)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        Call Me.EditCopyEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(引当)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey5Press")

        Call Me.OpenHikiatePop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey5Press")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し(全量)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey6Press")

        Call Me.OpenZenryoPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey6Press")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し(戻る)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey8Press")

        Call Me.CancelAction(frm)

        Call Me.cmbFurikaeKbnSelectedIndexChanged(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey8Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(振替元確定、振替確定)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        If frm.lblSituation.RecordStatus.Equals(RecordStatus.INIT) Then
            Me.FurikaeMotoKakuteiEvent(frm)
        Else
            Me.FurikaeKakuteiEvent(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMD010F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMD010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EnterKeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EnterKeyDown")

    End Sub

    ''' <summary>
    ''' 行削除(振替元)ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnMotoDel_Click(ByRef frm As LMD010F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnMotoDel_Click")

        Call Me.DeleteRowsMoto(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnMotoDel_Click")

    End Sub

    ''' <summary>
    ''' 行追加(振替先)ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnSakiAdd_Click(ByRef frm As LMD010F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnSakiAdd_Click")

        Call Me.AddRowsSaki(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnSakiAdd_Click")

    End Sub

    ''' <summary>
    ''' 行削除(振替先)ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnSakiDel_Click(ByRef frm As LMD010F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnSakiDel_Click")

        Call Me.DeleteRowsSaki(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnSakiDel_Click")

    End Sub

    ''' <summary>
    ''' 梱数TextChangeイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numKonsu_LostFocus(ByVal frm As LMD010F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "numKonsu_LostFocus")

        '「引当残個数計算」処理
        Me.CalcHikiSumiCnt(frm, 0)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "numKonsu_LostFocus")

    End Sub

    ''' <summary>
    ''' 端数TextChangeイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numCnt_LostFocus(ByVal frm As LMD010F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "numCnt_LostFocus")

        '「引当残個数計算」処理
        Me.CalcHikiSumiCnt(frm, 0)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "numCnt_LostFocus")

    End Sub

    ''' <summary>
    ''' 営業所変更時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub cmbNrsBrCd_Change(ByRef frm As LMD010F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbNrsBrCd_Change")

        frm.cmbSoko.SelectedValue = LMUserInfoManager.GetWhCd

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbNrsBrCd_Change")

    End Sub

    ''' <summary>
    ''' 振替先スプレッドのチェンジイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprDtlNew_Change(ByVal frm As LMD010F, ByVal e As FarPoint.Win.Spread.ChangeEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDtlNew_Change")

        Call Me.SprChangeAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDtlNew_Change")

    End Sub

    ''' <summary>
    ''' 振替区分　SelectedIndexChangedイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    Friend Sub cmbFurikaeKbnSelectedIndexChanged(ByVal frm As LMD010F, ByVal e As EventArgs)

        If String.IsNullOrEmpty(frm.cmbFurikaeKbn.SelectedValue.ToString) Then
            '振替区分で先頭空白が選択された：初期入荷日引き継ぎをチェックする
            frm.chkInkoDateUmu.Checked = True

        ElseIf "01".Equals(frm.cmbFurikaeKbn.SelectedValue.ToString) Then
            '振替区分で[荷主変更]が選択された：初期入荷日引き継ぎのチェックを外す
            frm.chkInkoDateUmu.Checked = False

        Else
            '振替区分で上記以外が選択された：初期入荷日引き継ぎをチェックする
            frm.chkInkoDateUmu.Checked = True
        End If

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class