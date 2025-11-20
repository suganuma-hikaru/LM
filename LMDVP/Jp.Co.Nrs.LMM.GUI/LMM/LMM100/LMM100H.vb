' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM100H : 商品マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李


''' <summary>
''' LMM100ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM100H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM100F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM100V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM100G

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

    ''' <summary>
    ''' 在庫存在フラグ
    ''' </summary>
    ''' <remarks>TRUE:存在する、FALSE;存在しない</remarks>
    Private _ExistZaikoFlg As Boolean = False

    ''' <summary>
    ''' 実予在庫個数
    ''' </summary>
    ''' <remarks></remarks>
    Private _SumPoraZaiNb As Integer = 0  'Add 2019/07/31 要望管理006855

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 円周率
    ''' </summary>
    ''' <remarks></remarks>
    Private _CircleRate As Decimal

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが設定される前にアクセスして例外が発生する問題に対応 20151106 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑


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
        Me._Frm = New LMM100F(Me)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM100", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM100G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMM100V(Me, Me._Frm, Me._ControlV)

        '区分マスタから円周率を取得
        Call Me.GetCircleRate()

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

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

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
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.SHINKI) = False Then
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
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        ' 寄託商品単価 表示制御
        Call Me._G.SetKitakuShohinTankaVisible()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Me._InDs = New LMM100DS()
        Call Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM100BLF", "EditChk", Me._InDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        '在庫存在フラグの初期化
        Me._ExistZaikoFlg = False

        '実予在庫個数の初期化
        Me._SumPoraZaiNb = 0  'Add 2019/07/31 要望管理006855

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then
                If MyBase.ShowMessage(Me._Frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '在庫存在フラグの設定
                    Me._ExistZaikoFlg = True

                    '実予在庫個数の設定
                    Me._SumPoraZaiNb = CInt(rtnds.Tables("LMM100ZAIKO").Rows(0).Item("SUM_PORA_ZAI_NB"))  'Add 2019/07/31 要望管理006855

                    '編集モード切り替え処理
                    Call Me.ChangeEditMode(rtnds)

                Else
                    Call Me._V.SetBaseMsg() '基本メッセージの設定
                    Call Me._ControlH.EndAction(Me._Frm) '終了処理
                End If

            Else
                MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
                Call Me._ControlH.EndAction(Me._Frm) '終了処理
            End If
        Else

            '編集モード切り替え処理
            Call Me.ChangeEditMode(rtnds)

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.FUKUSHA) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '複写ボタン押下時チェック
        If Me._V.IsFukushaChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '枝番の設定
        Me._MaxEdaNo = Me._Frm.sprGoodsDetail.ActiveSheet.Rows.Count - 1
        Me._MaxEdaNoSet = -1

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '商品明細マスタ情報をSpreadに設定
        Call Me._G.SetSpreadDtl(Me._DispDt)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '削除/復活ボタン押下時チェック
        If Me._V.IsSakujoHukkatuChk() = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行メッセージ表示
        If Me.ConfirmMsg(LMM100C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM100BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '---↓
        ''キャッシュ最新化
        'Call Me.GetNewCache()
        '---↑

        '完了メッセージ表示
        Call Me.SetCompleteMessage(LMM100C.EventShubetsu.SAKUJO_HUKKATU)

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' 単価一括変更処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateSameTime()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.TANKA_IKKATU_HENKO) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprGoods.ActiveSheet, LMM100G.sprGoodsDef.DEF.ColNo)

        '単価一括変更ボタン押下時チェック
        If Me._V.IsTankaIkkatuChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'Pop起動処理
        Dim prm As LMFormData = Me.ShowTankaPopupTankaIkkatu(list)
        If prm.ReturnFlg = False Then
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.ShowMessage(Me._Frm, "E815")
            '20151029 tsunehira add End
            'MyBase.ShowMessage(Me._Frm, "E199", New String() {"単価グループコード"})
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行確認
        'Dim msg As String = "単価一括変更"
        Dim msg As String = _Frm.FunctionKey.F5ButtonName
        '20151029 tsunehira add Start
        If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetInDataTankaIkkatu(ds, prm, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateIkkatu")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM100BLF", "UpdateIkkatu", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateIkkatu")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            '---↓
            ''キャッシュ最新化
            'Call Me.GetNewCache()
            '---↑


            'エラーEXCEL出力
            MyBase.ShowMessage(Me._Frm, "E235")
            MyBase.MessageStoreDownload()

        ElseIf MyBase.IsMessageExist = True Then

            'エラーメッセージ表示
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub

        Else

            '---↓
            ''キャッシュ最新化
            'Call Me.GetNewCache()
            '---↑

            'メッセージエリアの設定
            MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})
        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub

    '2015.10.02 他荷主対応START
    ''' <summary>
    ''' 他荷主処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub InsertTaninusi()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.TANINUSI) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprGoods.ActiveSheet, LMM100G.sprGoodsDef.DEF.ColNo)

        '他荷主ボタン押下時チェック
        If Me._V.IsTaninusiChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'Pop起動処理
        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, Me._Frm.lblCustNmL.Name)
        Dim prm As LMFormData = Me.ShowCustPopup(ctl, LMM100C.EventShubetsu.TANINUSI)
        If prm.ReturnFlg = False Then
            'MyBase.ShowMessage(Me._Frm, "E199", New String() {"荷主コード"})
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.ShowMessage(Me._Frm, "E199", New String() {LMM100C.SprGoodsColumnIndex.CUST_CD.ToString})
            '20151029 tsunehira add End
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行確認
        'Dim msg As String = "他荷主"
        '20151029 tsunehira add Start
        '英語化対応
        If MyBase.ShowMessage(Me._Frm, "C015") = MsgBoxResult.Cancel Then
            '20151029 tsunehira add End
            'If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetInDataTaninusi(ds, prm, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "InsertTaninusi")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM100BLF", "InsertTaninusi", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "InsertTaninusi")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            'エラーEXCEL出力
            MyBase.ShowMessage(Me._Frm, "E235")
            MyBase.MessageStoreDownload()

        ElseIf MyBase.IsMessageExist = True Then

            'エラーメッセージ表示
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(Me._Frm, "G061")
        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub
    '2015.10.02 他荷主対応END

    'START YANAI 要望番号372
    ''' <summary>
    ''' 荷主一括変更処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub UpdateNinushi()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.NINUSHI_IKKATU_HENKO) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprGoods.ActiveSheet, LMM100G.sprGoodsDef.DEF.ColNo)

        '荷主一括変更ボタン押下時チェック
        If Me._V.IsNinushiIkkatuChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'Pop起動処理
        Dim prm As LMFormData = Me.ShowCustPopupIkkatu(list)
        If prm.ReturnFlg = False Then
            'MyBase.ShowMessage(Me._Frm, "E199", New String() {"荷主コード"})
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.ShowMessage(Me._Frm, "E199", New String() {LMM100G.sprGoodsDef.CUST_CD.ToString})
            '20151029 tsunehira add End
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'Pop選択値チェック
        If Me._V.IsNinushiPopSelectChk(list, prm) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行確認
        'Dim msg As String = "荷主一括変更"
        Dim msg As String = _Frm.FunctionKey.F6ButtonName
        If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetInDataNinushiIkkatu(ds, prm, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateNinushi")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM100BLF", "UpdateNinushi", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateNinushi")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            '---↓
            ''キャッシュ最新化
            'Call Me.GetNewCache()
            '---↑

            'エラーEXCEL出力
            MyBase.ShowMessage(Me._Frm, "E235")
            MyBase.MessageStoreDownload()

        ElseIf MyBase.IsMessageExist = True Then

            'エラーメッセージ表示
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub

        Else

            '---↓
            ''キャッシュ最新化
            'Call Me.GetNewCache()
            '---↑

            'メッセージエリアの設定
            MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})
        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub
    'END YANAI 要望番号372

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.KENSAKU) = False Then
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
                MyBase.ShowMessage(Me._Frm, "G003")
                Exit Sub
            End If
        End If

        '検索処理を行う
        Call Me.SelectData()

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

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
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMMControlC.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.StartAction()

        'Pop起動処理
        Call Me.ShowPopupControl(objNm, LMM100C.EventShubetsu.MASTEROPEN)

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
    Private Function SaveEvent(ByVal eventShubetu As LMM100C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

#If True Then  'UPD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
        '↓から移動
        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetSave(ds)

#End If

        ds = Me.SetSaveCHK(ds)      'ADD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)

        '単項目/関連チェック
        ''If Me._V.IsSaveChk(MyBase.GetSystemDateTime(0), Me._ExistZaikoFlg) = False Then                  'Del 2019/07/31 要望管理006855
        If Me._V.IsSaveChk(MyBase.GetSystemDateTime(0), Me._ExistZaikoFlg, Me._SumPoraZaiNb) = False Then  'Add 2019/07/31 要望管理006855
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If
#If False Then  'UPD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
        '上に移動
        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetSave(ds)

#End If

        'X-Track用商品明細入力チェック
        If Not Me.IsSaveChkXTrack() Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'ログ出力/
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        Dim ctl As Control() = Nothing
        Dim focus As Control = Nothing
        Dim tab As LMTab = Me._Frm.tabGoodsMst
        Dim tabPg As System.Windows.Forms.TabPage = Me._Frm.tpgGoods
        If Me.SaveCallBLF(ds, ctl, focus, tab) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            If focus IsNot Nothing Then
                Call Me._ControlV.SetErrorControl(ctl, focus, tab, tabPg)
            End If
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '---↓
        ''キャッシュ最新化
        'Call Me.GetNewCache()
        '---↑

        '完了メッセージ表示
        Call Me.SetCompleteMessage(LMM100C.EventShubetsu.HOZON)

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)


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

                If Me.SaveEvent(LMM100C.EventShubetsu.TOJIRU) = False Then

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
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.ADD_ROW) = False Then
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
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.DEL_ROW) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprGoodsDetail.ActiveSheet, LMM100G.sprGoodsDtlDef.DEF.ColNo)

        '単項目/関連チェック
        If Me._V.IsDelRowChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行削除
        Call Me._G.DelateDtl(list)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

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
            If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.ENTER) = False Then
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
            Call Me.ShowPopupControl(objNm, LMM100C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnPrintClick()

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '2017/09/25 修正 李↓
        Dim msg As String = lgm.Selector({"印刷処理", "Printing", "인쇄처리", "中国語"})
        '2017/09/25 修正 李↑

        Dim rtnDs As DataSet = New DataSet

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.PRINT) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsPrintChk = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '印刷処理を行う
        'DataSet設定
        If Me.PrintCommon(rtnDs) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        rtnDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        '==== WSAクラス呼出（印刷処理） ====
        rtnDs = MyBase.CallWSA("LMM100BLF", "PrintData", rtnDs)

        '2011.08.26 検証結果一覧№42対応 START
        'If MyBase.IsMessageExist() = True Then

        '    Me._ControlH.EndAction(Me._Frm)
        '    ''メッセージの表示
        '    Me.ShowMessage(Me._Frm, "G007")
        '    Exit Sub

        'End If

        If IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = True Then

                '処理終了アクション
                Me._ControlH.EndAction(Me._Frm)

                'エラーメッセージの場合
                MyBase.ShowMessage(Me._Frm)

                'メッセージエリアの設定
                Call Me._V.SetBaseMsg()

                '画面解除
                MyBase.UnLockedControls(Me._Frm)

                'Cursorを元に戻す
                Cursor.Current = Cursors.Default()

                Exit Sub
            Else

                '処理終了アクション
                Me._ControlH.EndAction(Me._Frm)

                '帳票のガイダンスメッセージの場合
                MyBase.ShowMessage(Me._Frm)

                'メッセージエリアの設定
                Call Me._V.SetBaseMsg()

                '画面解除
                MyBase.UnLockedControls(Me._Frm)

                'Cursorを元に戻す
                Cursor.Current = Cursors.Default()

                Exit Sub
            End If

        End If
        '2011.08.26 検証結果一覧№42対応 END

        'プレビュー判定 
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

        '2017/09/25 修正 李↓
        '処理終了メッセージの表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {lgm.Selector({"印刷処理", "Printing", "인쇄처리", "中国語"}), ""})
        '2017/09/25 修正 李↑

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        'Call Me._G.SetFoucus(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal frm As LMM100F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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

    ''' <summary>
    ''' 個数単位をラベルに設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetKosuTani()

        Call Me._G.SetKosuTani()

    End Sub

    ''' <summary>
    ''' 危険情報確認処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmKikenGoods() As Boolean

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.KIKEN_KAKUNIN) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprGoods.ActiveSheet, LMM100G.sprGoodsDtlDef.DEF.ColNo)

        '単項目/関連チェック
        If Me._V.IsConfirmKikenChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Function
        End If

        If MyBase.ShowMessage(Me._Frm, "C015") = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Function
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetConfKiken(ds, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ConfirmKikenGoods")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM100BLF", "ConfirmKikenGoods", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ConfirmKikenGoods")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            'エラーEXCEL出力
            MyBase.ShowMessage(Me._Frm, "E235")
            MyBase.MessageStoreDownload()

        ElseIf MyBase.IsMessageExist = True Then

            'エラーメッセージ表示
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Function

        Else

            '完了メッセージ表示
            Call Me.SetCompleteMessage(LMM100C.EventShubetsu.KIKEN_KAKUNIN)

        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

        Return True

    End Function

    ''' <summary>
    ''' 容積一括更新処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdateGoodsVolume() As Boolean

        '処理開始アクション
        Call Me.StartAction()

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.KIKEN_KAKUNIN) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._ControlH.GetCheckList(Me._Frm.sprGoods.ActiveSheet, LMM100G.sprGoodsDtlDef.DEF.ColNo)

        '単項目/関連チェック
        If Me._V.IsConfirmVolumeChk(list) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Function
        End If

        If MyBase.ShowMessage(Me._Frm, "C001", {"容積を一括更新"}) = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Function
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM100DS()
        Call Me.SetDataSetUpdateGoodsVolume(ds, list)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateGoodsVolume")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM100BLF", "UpdateGoodsVolume", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateGoodsVolume")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            'エラーEXCEL出力
            MyBase.ShowMessage(Me._Frm, "E235")
            MyBase.MessageStoreDownload()

        ElseIf MyBase.IsMessageExist = True Then

            'エラーメッセージ表示
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Function

        Else

            '完了メッセージ表示
            Call Me.SetCompleteMessage(LMM100C.EventShubetsu.VOLUME_IKKATU)

        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus(Me._ExistZaikoFlg)

        Return True

    End Function
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
    Private Sub ChangeEditMode(ByVal rtnds As DataSet)

        '最大枝番の設定
        Me._MaxEdaNo = -1
        Dim max As Integer = Me._Frm.sprGoodsDetail.ActiveSheet.Rows.Count - 1
        Dim edaNo As Integer = 0
        For i As Integer = 0 To max
            edaNo = Convert.ToInt32(Me._ControlV.GetCellValue(Me._Frm.sprGoodsDetail.ActiveSheet.Cells(i, LMM100G.sprGoodsDtlDef.EDA_NO.ColNo)))
            If Me._MaxEdaNo < edaNo Then
                Me._MaxEdaNo = edaNo
            End If
        Next
        Me._MaxEdaNoSet = Me._MaxEdaNo

        ' 在庫存在フラグ(荷主コードS・SS 編集可否判定用)
        Dim existCustZaikoFlg As Boolean = False
        If rtnds.Tables(LMM100C.TABLE_NM_CUST_ZAIKO).Rows().Count() > 0 Then
            ' 営業所コード、荷主コードL・M、商品キーが一致で
            ' (実予在庫個数、引当中個数、引当可能個数) のいずれかがゼロでないレコードありの場合
            ' 荷主コードS・SS は編集不可とする
            existCustZaikoFlg = True
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg, existCustZaikoFlg)

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
        Me._InDs = New LMM100DS()
        Call SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(Me._Frm, "LMM100BLF", "SelectListData", Me._InDs, lc, Nothing, mc)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMM100C.TABLE_NM_GOODS).Rows.Count > 0 Then
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
        Me._Frm.sprGoods.CrearSpread()

        '取得データをSPREADに表示
        Dim dt As DataTable = Me._OutDs.Tables(LMM100C.TABLE_NM_GOODS)
        Call Me._G.SetSpread(dt)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})

        'ディスプレイモードとステータス設定 
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprGoods.CrearSpread()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

    End Sub

    ''' <summary>
    ''' ワーニング表示時、強制フラグの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveShowMessage(ByVal msgId As String _
                                     , ByVal ds As DataSet) As Boolean

        Dim dr As DataRow = ds.Tables(LMM100C.TABLE_NM_GOODS).Rows(0)

        'メッセージを表示し、戻り値により処理を分ける
        If MyBase.ShowMessage(Me._Frm) = MsgBoxResult.Ok Then '「はい」を選択
            '強制実行フラグの設定
            Dim flg As String = LMConst.FLG.OFF
            Select Case msgId
                Case "W139", "W253"
                    flg = "1"
                Case "W136", "W254"
                    flg = "2"
                Case "W134"
                    flg = "3"
                Case "W135"
                    flg = "4"
                Case "W157"
                    flg = "5"
            End Select
            dr.Item("WARNING_FLG") = flg
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 保存時BLF呼び出し
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="errorCtl">エラーコントロール</param>
    ''' <param name="focusCtl">フォーカス設定コントロール</param>
    ''' <param name="tab">タブ</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveCallBLF(ByVal ds As DataSet _
                               , ByRef errorCtl As Control() _
                               , ByRef focusCtl As Control _
                               , ByRef tab As LMTab _
                              ) As Boolean

        With Me._Frm

            '==========================
            'WSAクラス呼出
            '==========================
            ds = MyBase.CallWSA("LMM100BLF", "SaveData", ds)
            Dim msgId As String = MyBase.GetMessageID
            Dim warningFlg As Boolean = MyBase.IsWarningMessageExist

            'メッセージ未設定時、処理終了
            If String.IsNullOrEmpty(msgId) Then
                Return True
            End If

            'フォーカス、エラーコントロールの設定
            Select Case msgId
                Case "W139", "W136", "W253", "W254"
                    errorCtl = New Control() {.cmbHokanKbnHokan, .txtTankaGroup}
                    focusCtl = .cmbHokanKbnHokan
                Case "E079", "E374", "W135" '単価マスタに関するチェック
                    errorCtl = New Control() {.txtTankaGroup}
                    focusCtl = .txtTankaGroup
                Case "W157"
                    errorCtl = New Control() {.txtCustCdS, .txtCustCdSS}
                    focusCtl = .txtCustCdS
                Case "W134"
                    If .lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
                        errorCtl = New Control() {.txtCustCdS, .txtCustCdSS, .txtGoodsCd}
                        focusCtl = .txtCustCdS
                    Else
                        errorCtl = New Control() {.txtCustCdL, .txtCustCdM, .txtCustCdS, .txtCustCdSS, .txtGoodsCd}
                        focusCtl = .txtCustCdL
                    End If
                Case Else

                    focusCtl = Nothing
            End Select

            'エラーメッセージ設定時、メッセージ表示後処理終了
            If warningFlg = False Then
                MyBase.ShowMessage(Me._Frm)
                Return False
            End If

            'ワーニング表示、[OK]選択時
            If Me.SaveShowMessage(msgId, ds) = False Then
                If Me.SaveCallBLF(ds, errorCtl, focusCtl, tab) = False Then
                    Return False
                End If
            Else
                'メッセージエリアの設定
                Call Me._V.SetBaseMsg()
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM100C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(rowNo, LMM100G.sprGoodsDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm(Me._ExistZaikoFlg)

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        '商品明細マスタ情報をSpreadに設定
        Call Me.GetGoodsDtlDisplayData()
        Call Me._G.SetSpreadDtl(Me._DispDt)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G013")

    End Sub

    ''' <summary>
    ''' 商品明細マスタSpread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetGoodsDtlDisplayData()

        Dim dt As DataTable = Me._OutDs.Tables(LMM100C.TABLE_NM_GOODS_DTL)

        '表示対象データを取得
        Me._DispDt = Nothing
        Dim filter As String = String.Empty
        filter = String.Concat(filter, "NRS_BR_CD = '", Me._Frm.cmbBr.SelectedValue, "'")
        filter = String.Concat(filter, " AND GOODS_CD_NRS = '", Me._Frm.lblGoodsKey.TextValue, "'")
        Dim orderBy As String = "GOODS_CD_NRS_EDA"
        Dim selectDr As DataRow() = dt.Select(filter, orderBy)
        Dim max As Integer = selectDr.Length - 1
        Dim setDS As DataSet = New LMM100DS()
        Me._DispDt = setDS.Tables(LMM100C.TABLE_NM_GOODS_DTL)

        For i As Integer = 0 To max
            Me._DispDt.ImportRow(selectDr(i))
        Next

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function PrintCommon(ByRef prmDs As DataSet) As Boolean

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim dt As DataTable = Nothing
        Dim dr As DataRow = Nothing

        Dim pgId As String = String.Empty

        'パラメータ設定
        prm.ReturnFlg = False

        Select Case Me._Frm.cmbPrint.SelectedValue.ToString()
            Case LMM100C.PRINT_TEON_JOON _
               , LMM100C.PRINT_TEON _
               , LMM100C.PRINT_JOON

                pgId = "LMM500"
                prmDs = New LMM500DS()
                dt = prmDs.Tables("LMM500IN")

                Dim ondoKbn As String = String.Empty
                Select Case Me._Frm.cmbPrint.SelectedValue.ToString()
                    Case LMM100C.PRINT_TEON_JOON
                        ondoKbn = String.Empty
                    Case LMM100C.PRINT_TEON
                        ondoKbn = "02"
                    Case LMM100C.PRINT_JOON
                        ondoKbn = "01"
                End Select

                With Me._Frm.sprGoods.ActiveSheet

                    dr = dt.NewRow()

                    Dim custCd As String = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.CUST_CD.ColNo))
                    custCd = custCd.PadRight(11, Convert.ToChar(" "))

                    '検索条件を設定
                    dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.BR_NM.ColNo))
                    dr.Item("CUST_CD_L") = custCd.Substring(0, 5).Trim()
                    dr.Item("CUST_CD_M") = custCd.Substring(5, 2).Trim()
                    dr.Item("CUST_CD_S") = custCd.Substring(7, 2).Trim()
                    dr.Item("CUST_CD_SS") = custCd.Substring(9, 2).Trim()
                    dr.Item("GOODS_CD_CUST") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.GOODS_CD.ColNo))
                    dr.Item("GOODS_NM_1") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo))
                    dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.STATUS.ColNo))
                    dr.Item("ONDO_KB") = ondoKbn

                End With

                dt.Rows.Add(dr)

            Case LMM100C.PRINT_ICHIRAN

                pgId = "LMM510"
                prmDs = New LMM510DS()
                dt = prmDs.Tables("LMM510IN")
                dr = dt.NewRow()

                dr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                dr.Item("GOODS_CD_NRS") = Me._Frm.lblGoodsKey.TextValue

                dt.Rows.Add(dr)
        End Select

        prm.ParamDataSet = prmDs

        If 0 < dt.Rows.Count Then
            prm.ReturnFlg = True

            'リターンフラグがFalseの場合
        ElseIf prm.ReturnFlg = False Then
            MyBase.ShowMessage(Me._Frm, "G007")
            Return False
        End If


        Return True

    End Function

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal eventShubetu As LMM100C.EventShubetsu) As Boolean

        Select Case eventShubetu
            Case LMM100C.EventShubetsu.SAKUJO_HUKKATU
                '処理続行メッセージ表示
                Dim msg As String = String.Empty
                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.DELETE_REC
                        If MyBase.ShowMessage(Me._Frm, "C013") = MsgBoxResult.Cancel Then
                            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
                            Exit Function
                        End If
                    Case RecordStatus.NOMAL_REC
                        If MyBase.ShowMessage(Me._Frm, "C014") = MsgBoxResult.Cancel Then
                            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
                            Exit Function
                        End If
                End Select

            Case LMM100C.EventShubetsu.HOZON
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
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMM100C.EventShubetsu)

        With Me._Frm

            Dim shoriMsg As String = String.Empty

            Select Case eventShubetu
                Case LMM100C.EventShubetsu.SAKUJO_HUKKATU
                    MyBase.ShowMessage(Me._Frm, "G002", New String() {_Frm.FunctionKey.F4ButtonName, String.Concat("[", .lblTitleGoodsCd.TextValue, Me._Frm.txtGoodsCd.TextValue, "]")})
                Case LMM100C.EventShubetsu.HOZON
                    MyBase.ShowMessage(Me._Frm, "G002", New String() {_Frm.FunctionKey.F11ButtonName, String.Concat("[", .lblTitleGoodsCd.TextValue, Me._Frm.txtGoodsCd.TextValue, "]")})
                Case LMM100C.EventShubetsu.KIKEN_KAKUNIN
                    MyBase.ShowMessage(Me._Frm, "G002", New String() {_Frm.FunctionKey.F8ButtonName, ""})
                Case LMM100C.EventShubetsu.VOLUME_IKKATU
                    MyBase.ShowMessage(Me._Frm, "G002", New String() {"容積一括更新", ""})
            End Select
        End With

    End Sub

    ''' <summary>
    ''' 容積計算
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalcVolume()
        With _Frm

            If CDec(.numWidth.Value) > 0 _
                AndAlso CDec(.numHeight.Value) > 0 _
                AndAlso CDec(.numDepth.Value) > 0 Then
                '円柱の場合、実容積に円柱の容積を設定
                If .lblCylFlg.TextValue = "1" Then
                    .numActualVolume.Value = (CDec(.numWidth.Value) * 0.5) * (CDec(.numDepth.Value) * 0.5) * Me._CircleRate * CDec(.numHeight.Value)
                    .numOccupyVolume.Value = CDec(.numWidth.Value) * CDec(.numHeight.Value) * CDec(.numDepth.Value)
                Else
                    .numActualVolume.Value = CDec(.numWidth.Value) * CDec(.numHeight.Value) * CDec(.numDepth.Value)
                    .numOccupyVolume.Value = CDec(.numWidth.Value) * CDec(.numHeight.Value) * CDec(.numDepth.Value)
                End If
            Else
                .numActualVolume.Value = 0
                .numOccupyVolume.Value = 0
            End If
        End With
    End Sub
    ''' <summary>
    ''' 容積計算(一括)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CalcVolumeBulk()
        With _Frm

            If CDec(.numWidthBulk.Value) > 0 _
                AndAlso CDec(.numHeightBulk.Value) > 0 _
                AndAlso CDec(.numDepthBulk.Value) > 0 Then
                '荷姿の検索条件有りかつ円柱の場合、実容積に円柱の容積を設定
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.sprGoods.ActiveSheet.Cells(0, LMM100G.sprGoodsDef.NISUGATA_CD.ColNo))) = False AndAlso _
                   .lblCylFlg.TextValue = "1" Then
                    .numActualVolumeBulk.Value = (CDec(.numWidthBulk.Value) * 0.5) * (CDec(.numDepthBulk.Value) * 0.5) * Me._CircleRate * CDec(.numHeightBulk.Value)
                    .numOccupyVolumeBulk.Value = CDec(.numWidthBulk.Value) * CDec(.numHeightBulk.Value) * CDec(.numDepthBulk.Value)
                Else
                    .numActualVolumeBulk.Value = CDec(.numWidthBulk.Value) * CDec(.numHeightBulk.Value) * CDec(.numDepthBulk.Value)
                    .numOccupyVolumeBulk.Value = CDec(.numWidthBulk.Value) * CDec(.numHeightBulk.Value) * CDec(.numDepthBulk.Value)
                End If
            Else
                .numActualVolumeBulk.Value = 0
                .numOccupyVolumeBulk.Value = 0
            End If
        End With
    End Sub

    '---↓
    '''' <summary>
    '''' キャッシュ最新化処理
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub GetNewCache()

    '    'キャッシュ最新化
    '    MyBase.LMCacheMasterData(LMConst.CacheTBL.GOODS)
    '    'MyBase.LMCacheMasterData(LMConst.CacheTBL.GOODS_DETAILS)

    'End Sub
    '---↑

    ''' <summary>
    ''' X-Track用商品明細入力チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>X-Track用項目以外はチェックしない</remarks>
    Private Function IsSaveChkXTrack() As Boolean

        Dim spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread = Me._Frm.sprGoodsDetail

        With spr.ActiveSheet

            Dim errIdx As Integer = -1

            For i As Integer = 0 To .Rows.Count - 1
                Dim yotoKbn = Me._ControlV.GetCellValue(.Cells(i, LMM100C.SprGoodsDetailColumnIndex.YOTO_KBN))
                Dim setteiValue = Me._ControlV.GetCellValue(.Cells(i, LMM100C.SprGoodsDetailColumnIndex.SETTEI_VALUE))

                Select Case yotoKbn
                    Case "76"
                        'X-Track取込み(SKU)

                        '未入力はエラー
                        If String.IsNullOrEmpty(setteiValue) Then
                            MyBase.SetMessage("E001", New String() {"X-Track取込み(SKU)"})
                            errIdx = i
                            Exit For
                        End If

                        'X-Track側に未登録ならばエラー
                        Dim ds As DataSet = New LMM100DS()
                        Dim dt As DataTable = ds.Tables("LMM100XTRACK_CHK")
                        Dim dr As DataRow = dt.NewRow()
                        dr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                        dr.Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
                        dr.Item("SKU") = setteiValue
                        dt.Rows.Add(dr)

                        ds = MyBase.CallWSA("LMM100BLF", "XTrackChk_SKU", ds)

                        If MyBase.IsMessageExist() Then
                            errIdx = i
                            Exit For
                        End If

                    Case "77"
                        'X-Track取込み(実入り状態)

                        '未入力はエラー
                        If String.IsNullOrEmpty(setteiValue) Then
                            MyBase.SetMessage("E001", New String() {"X-Track取込み(実入り状態)"})
                            errIdx = i
                            Exit For
                        End If

                        '特定の文字列以外はエラー
                        If (Not "実入り".Equals(setteiValue)) AndAlso (Not "空".Equals(setteiValue)) Then
                            MyBase.SetMessage("E01U", New String() {"X-Track取込み(実入り状態)は、""実入り""か""空""を入力して下さい。"})
                            errIdx = i
                            Exit For
                        End If

                    Case "78"
                        'X-Track取込み(原産国)

                        '未入力はエラー
                        If String.IsNullOrEmpty(setteiValue) Then
                            MyBase.SetMessage("E001", New String() {"X-Track取込み(原産国)"})
                            errIdx = i
                            Exit For
                        End If

                        'X-Track側に未登録ならばエラー
                        Dim ds As DataSet = New LMM100DS()
                        Dim dt As DataTable = ds.Tables("LMM100XTRACK_CHK")
                        Dim dr As DataRow = dt.NewRow()
                        dr.Item("GENSAN_NM") = setteiValue
                        dt.Rows.Add(dr)

                        ds = MyBase.CallWSA("LMM100BLF", "XTrackChk_Gensan", ds)

                        If MyBase.IsMessageExist() Then
                            errIdx = i
                            Exit For
                        End If
                End Select
            Next

            'エラーがあった場合
            If errIdx <> -1 Then
                MyBase.ShowMessage(Me._Frm)
                'エラーコントロール設定
                Call Me._ControlV.SetErrorControl(spr, New Integer() {errIdx}, New Integer() {LMM100C.SprGoodsDetailColumnIndex.SETTEI_VALUE}, Me._Frm.tabGoodsMst, Me._Frm.tpgGoodsDetail)
                Return False
            End If

        End With

        Return True

    End Function

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal eventshubetsu As LMM100C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name _
                   , .txtCustCdM.Name _
                   , .txtCustCdS.Name _
                   , .txtCustCdSS.Name _
                    '荷主マスタ参照POP起動
                    Call Me.SetReturnCustPop(objNm, eventshubetsu)

                Case .txtNisonin.Name
                    '届け先マスタ参照POP起動
                    Call Me.SetReturnDestPop(objNm, eventshubetsu)

                Case .txtNyukaSagyoKbn1.Name _
                   , .txtNyukaSagyoKbn2.Name _
                   , .txtNyukaSagyoKbn3.Name _
                   , .txtNyukaSagyoKbn4.Name _
                   , .txtNyukaSagyoKbn5.Name _
                   , .txtShukkaSagyoKbn1.Name _
                   , .txtShukkaSagyoKbn2.Name _
                   , .txtShukkaSagyoKbn3.Name _
                   , .txtShukkaSagyoKbn4.Name _
                   , .txtShukkaSagyoKbn5.Name _
                   , .txtKonpoSagyoCd.Name _
                   , .txtHasuSagyoKbn1.Name _
                   , .txtHasuSagyoKbn2.Name _
                   , .txtHasuSagyoKbn3.Name
                    '作業項目マスタ参照POP起動
                    Call Me.SetReturnSagyoKmkPop(objNm, eventshubetsu)

                Case .txtTankaGroup.Name
                    '単価マスタ参照POP起動
                    Call Me.SetReturnTankaPop(objNm, eventshubetsu)

                Case .txtShobo.Name
                    '消防マスタ参照POP起動
                    Call Me.SetReturnShoboPop(objNm, eventshubetsu)

                Case .txtUn.Name _
                   , .txtPg.Name
                    'UNマスタ参照POP起動
                    Call Me.SetReturnUNPop(String.Empty, eventshubetsu)

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
    Private Function SetReturnCustPop(ByVal objNm As String, ByVal eventshubetsu As LMM100C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .txtCustCdS.TextValue = dr.Item("CUST_CD_S").ToString()
                .txtCustCdSS.TextValue = dr.Item("CUST_CD_SS").ToString()

                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()
                .lblCustNmS.TextValue = dr.Item("CUST_NM_S").ToString()
                .lblCustNmSS.TextValue = dr.Item("CUST_NM_SS").ToString()

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
                ' 外装のコンボボックス表示
                Me._G.CreateOuterPackageComboBox()
#End If
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
    Private Function ShowCustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM100C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM100C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = Me._Frm.txtCustCdM.TextValue
                .Item("CUST_CD_S") = Me._Frm.txtCustCdS.TextValue
                .Item("CUST_CD_SS") = Me._Frm.txtCustCdSS.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索

#If True Then '商品マスタから荷主マスタ参照へ遷移した場合に、ダブルクリックで荷主を選択すると荷主CD(S,SS)が消える問題に対応 Added 20151110  INOUE 
            .Item("DC_ROW_SELECT_MODE") = LMZ260C.DobleClickRowSelectMode.DISABLED_CLEAR_CUST_CD
#End If

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    'START YANAI 要望番号372
    ''' <summary>
    ''' 荷主一括更新時の荷主マスタ参照POP起動
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ShowCustPopupIkkatu(ByVal list As ArrayList) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim row As Integer = Convert.ToInt32(list(0).ToString)
        Dim brCd As String = Me._ControlV.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
        Dim custCdL As String = Me._ControlV.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
        Dim custCdM As String = Me._ControlV.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))

        With dr
            .Item("NRS_BR_CD") = brCd
            .Item("CUST_CD_L") = custCdL
            .Item("CUST_CD_M") = custCdM
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索

            .Item("DC_ROW_SELECT_MODE") = LMZ260C.DobleClickRowSelectMode.DISABLED_CLEAR_CUST_CD    ' 設定する荷主コードのクリアをしない
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function
    'END YANAI 要望番号372

#End Region

#Region "届け先マスタ"

    ''' <summary>
    ''' 届け先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal objNm As String, ByVal eventshubetsu As LMM100C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                .txtNisonin.TextValue = dr.Item("DEST_CD").ToString()
                .lblNisonin.TextValue = dr.Item("DEST_NM").ToString()
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
    Private Function ShowDestPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM100C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM100C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = Me._Frm.txtNisonin.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.OFF

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "作業項目マスタ"

    ''' <summary>
    ''' 作業項目マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSagyoKmkPop(ByVal objNm As String, ByVal eventshubetsu As LMM100C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowSagyoKmkPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                ctl.TextValue = dr.Item("SAGYO_CD").ToString()
                If objNm.Equals(.txtKonpoSagyoCd.Name) Then
                    .lblKonpoSagyoCd.TextValue = dr.Item("SAGYO_NM").ToString()
                End If
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 作業項目マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowSagyoKmkPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM100C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ200DS()
        Dim dt As DataTable = ds.Tables(LMZ200C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM100C.EventShubetsu.ENTER Then
                .Item("SAGYO_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SAGYO_CNT") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ200", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "単価マスタ"

    ''' <summary>
    ''' 単価マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTankaPop(ByVal objNm As String, ByVal eventshubetsu As LMM100C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowTankaPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ040C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                ctl.TextValue = dr.Item("UP_GP_CD_1").ToString()
                .lblTekiyoStartDate.TextValue = dr.Item("STR_DATE").ToString()
                .numHokanTujo.Value = dr.Item("STORAGE_1").ToString()
                .cmbHokanTujo.SelectedValue = dr.Item("STORAGE_KB1").ToString()
                .numHokanTeion.Value = dr.Item("STORAGE_2").ToString()
                .cmbHokanTeion.SelectedValue = dr.Item("STORAGE_KB2").ToString()
                .numNiyakuNyuko.Value = dr.Item("HANDLING_IN").ToString()
                .cmbNiyakuNyuko.SelectedValue = dr.Item("HANDLING_IN_KB").ToString()
                .numNiyakuShukko.Value = dr.Item("HANDLING_OUT").ToString()
                .cmbNiyakuShukko.SelectedValue = dr.Item("HANDLING_OUT_KB").ToString()
                .numNiyakuMinHosho.Value = dr.Item("MINI_TEKI_IN_AMO").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 単価マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowTankaPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM100C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ040DS()
        Dim dt As DataTable = ds.Tables(LMZ040C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = Me._Frm.txtCustCdM.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM100C.EventShubetsu.ENTER Then
                .Item("UP_GP_CD_1") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("FUTURE_STR_DATE_FLG") = LMConst.FLG.OFF   '未来のデータ選択不能

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ040", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 単価マスタ参照POP起動(単価一括変更)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ShowTankaPopupTankaIkkatu(ByVal list As ArrayList) As LMFormData

        Dim ds As DataSet = New LMZ040DS()
        Dim setRow As Integer = Convert.ToInt32(list(0).ToString)
        Dim dt As DataTable = ds.Tables(LMZ040C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With Me._Frm.sprGoods.ActiveSheet
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(setRow, LMM100G.sprGoodsDef.BR_CD.ColNo))
            dr.Item("CUST_CD_L") = Me._ControlV.GetCellValue(.Cells(setRow, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
            dr.Item("CUST_CD_M") = Me._ControlV.GetCellValue(.Cells(setRow, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))
            dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ040", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "消防マスタ"

    ''' <summary>
    ''' 消防マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnShoboPop(ByVal objNm As String, ByVal eventshubetsu As LMM100C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowShoboPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ280C.TABLE_NM_OUT).Rows(0)
            Dim shoboInfo As String = String.Empty
            shoboInfo = String.Concat(shoboInfo, dr.Item("RUI_NM").ToString())
            shoboInfo = String.Concat(shoboInfo, " ", dr.Item("HINMEI").ToString())
            shoboInfo = String.Concat(shoboInfo, " ", dr.Item("SEISITSU").ToString())
            shoboInfo = String.Concat(shoboInfo, " ", dr.Item("SYU_NM").ToString())
            With Me._Frm
                ctl.TextValue = dr.Item("SHOBO_CD").ToString()
                .lblShobo.TextValue = shoboInfo
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 消防マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowShoboPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM100C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ280DS()
        Dim dt As DataTable = ds.Tables(LMZ280C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM100C.EventShubetsu.ENTER Then
                .Item("SHOBO_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            Select Case Me._Frm.cmbShobokiken.SelectedValue.ToString()
                Case LMM100C.SHOBO_KIKEN_KIKEN
                    .Item("FREE_WHERE") = "RUI <> '09'"
                Case LMM100C.SHOBO_KIKEN_SHITEIKANEN
                    .Item("FREE_WHERE") = "RUI = '09'"
                Case LMM100C.SHOBO_KIKEN_HIGAITO
                    .Item("FREE_WHERE") = "1 = 2"
                Case Else
                    .Item("FREE_WHERE") = String.Empty
            End Select

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ280", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "UNマスタ"
    ''' <summary>
    ''' UNマスタPopの戻り値を設定
    ''' </summary>
    ''' <returns></returns>
    Friend Function SetReturnUNPop(ByVal CntChkOnlyFlg As String,
                                   ByVal eventshubetsu As LMM100C.EventShubetsu,
                                   Optional ByRef CntChkResult As Integer = 0) As Boolean

        Dim prm As LMFormData = Me.ShowUNPopup(CntChkOnlyFlg, eventshubetsu)

        If prm.ReturnFlg = True Then

            'マスタ存在チェックの場合
            If CntChkOnlyFlg = "1" Then

                CntChkResult = prm.ParamDataSet.Tables(LMZ330C.TABLE_NM_OUT).Rows.Count

                '0件の場合、そのまま終了
                If CntChkResult = 0 Then Return True

            End If

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ330C.TABLE_NM_OUT).Rows(0)

            With Me._Frm
                .txtUn.TextValue = dr.Item("UN_NO").ToString()
                .txtPg.TextValue = dr.Item("PG_KBN").ToString()
                .lblClass1.TextValue = dr.Item("IMDG_CLASS").ToString()
                .lblClass2.TextValue = dr.Item("IMDG_CLASS1").ToString()
                .lblClass3.TextValue = dr.Item("IMDG_CLASS2").ToString()
                .lblKaiyouosen.TextValue = dr.Item("MP_FLG_NM").ToString()
                .txtKaiyouosen.TextValue = dr.Item("MP_FLG").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' UNマスタ参照POP起動
    ''' </summary>
    ''' <returns></returns>
    Private Function ShowUNPopup(ByVal CntChkOnlyFlg As String,
                                 ByVal eventshubetsu As LMM100C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ330DS()
        Dim dt As DataTable = ds.Tables(LMZ330C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue.ToString()

            If eventshubetsu = LMM100C.EventShubetsu.ENTER Then
                .Item("UN_NO") = Me._Frm.txtUn.TextValue.ToString()
                .Item("PG_KBN") = Me._Frm.txtPg.TextValue.ToString()
            End If

            .Item("CNT_CHK_ONLY_FLG") = CntChkOnlyFlg

            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ330", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dr As DataRow = Me._InDs.Tables(LMM100C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprGoods.ActiveSheet

            Dim custCd As String = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.CUST_CD.ColNo))
            custCd = custCd.PadRight(11, Convert.ToChar(" "))

            '検索条件を設定
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.BR_NM.ColNo))
            dr.Item("AVAL_YN") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.AVAL_YN_NM.ColNo))     'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能
            dr.Item("CUST_CD_L") = custCd.Substring(0, 5).Trim()
            dr.Item("CUST_CD_M") = custCd.Substring(5, 2).Trim()
            dr.Item("CUST_CD_S") = custCd.Substring(7, 2).Trim()
            dr.Item("CUST_CD_SS") = custCd.Substring(9, 2).Trim()
            dr.Item("CUST_NM_L") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.CUST_NM_L.ColNo))
            dr.Item("GOODS_CD_CUST") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.GOODS_CD.ColNo))
            '(2013.01.11)要望番号1700 -- START --
            'dr.Item("GOODS_NM_1") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo))
            'dr.Item("GOODS_NM_1") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo)).Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
            dr.Item("GOODS_NM_1") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo)).Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")  '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
            '(2013.01.11)要望番号1700 --  END  --
            dr.Item("STD_IRIME_UT") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.IRIME_TANI_CD.ColNo))
            dr.Item("PKG_UT") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.NISUGATA_CD.ColNo))
            dr.Item("ONDO_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.ONDO_KANRI_NM.ColNo))
            dr.Item("UP_GP_CD_1") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColNo))
            dr.Item("DOKU_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.DOKUGEKI_NM.ColNo))
            dr.Item("SHOBO_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.SHOBO_CD.ColNo))
            dr.Item("SEIQTO_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.SEIQT_CD.ColNo))
            dr.Item("SEIQTO_NM") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.SEIQT_COMP_NM.ColNo))
            dr.Item("SEIQTO_BUSYO_NM") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.SEIQT_BUSHO_NM.ColNo))
            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.STATUS.ColNo))
            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.BR_CD.ColNo))
            dr.Item("KIKEN_USER_NM") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.KIKEN_USER_NM.ColNo))
            'ADD START 2018/11/09 要望番号002599
            If .Cells(0, LMM100G.sprGoodsDef.ACTUAL_VOLUME.ColNo).Value Is Nothing = False _
                AndAlso String.IsNullOrEmpty(.Cells(0, LMM100G.sprGoodsDef.ACTUAL_VOLUME.ColNo).Value.ToString()) = False Then
                dr.Item("ACTUAL_VOLUME") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.ACTUAL_VOLUME.ColNo))
            End If
            If .Cells(0, LMM100G.sprGoodsDef.OCCUPY_VOLUME.ColNo).Value Is Nothing = False _
                AndAlso String.IsNullOrEmpty(.Cells(0, LMM100G.sprGoodsDef.OCCUPY_VOLUME.ColNo).Value.ToString()) = False Then
                dr.Item("OCCUPY_VOLUME") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.OCCUPY_VOLUME.ColNo))
            End If
            'ADD END   2018/11/09 要望番号002599
            dr.Item("SEARCH_KEY_2") = Me._ControlV.GetCellValue(.Cells(0, LMM100G.sprGoodsDef.CUST_CATEGORY_2.ColNo))       'ADD 2019/06/21 006318
            Me._InDs.Tables(LMM100C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim dr As DataRow = Me._InDs.Tables(LMM100C.TABLE_NM_IN).NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("GOODS_CD_NRS") = .lblGoodsKey.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim

            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbBr.SelectedValue.ToString()

            '荷主コードS・SS 編集可否判定のための在庫データ取得用
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue()
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue()

            Me._InDs.Tables(LMM100C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(削除復活処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM100C.TABLE_NM_GOODS).NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("GOODS_CD_NRS") = .lblGoodsKey.TextValue
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

            ds.Tables(LMM100C.TABLE_NM_GOODS).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(荷主一括変更処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInDataNinushiIkkatu(ByVal ds As DataSet _
                                            , ByVal prm As LMFormData _
                                            , ByVal list As ArrayList)

        Dim dt As DataTable = ds.Tables(LMM100C.TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = list.Count - 1
        Dim rtnDr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
        Dim custS As String = rtnDr.Item("CUST_CD_S").ToString()
        Dim custSS As String = rtnDr.Item("CUST_CD_SS").ToString()
        Dim row As Integer = 0

        row = Convert.ToInt32(list(0).ToString)
        Dim custCdL As String = Me._ControlV.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
        Dim custCdM As String = Me._ControlV.GetCellValue(Me._Frm.sprGoods.ActiveSheet.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))

        For i As Integer = 0 To max

            With Me._Frm.sprGoods.ActiveSheet

                dr = dt.NewRow()
                row = Convert.ToInt32(list(i).ToString)

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd
                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("GOODS_CD_NRS") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_KEY.ColNo))
                dr.Item("CUST_CD_S") = custS
                dr.Item("CUST_CD_SS") = custSS
                dr.Item("GOODS_CD_CUST") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_CD.ColNo))
                dr.Item("RECORD_NO") = row
                '更新時共通項目設定
                dr.Item("SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_TIME.ColNo))

                '荷主コードS・SS 編集可否判定のための在庫データ取得用
                dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("CUST_CD_L") = custCdL
                dr.Item("CUST_CD_M") = custCdM

                dt.Rows.Add(dr)

            End With

        Next

    End Sub

    'START YANAI 要望番号372
    ''' <summary>
    ''' データセット設定(単価一括変更)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInDataTankaIkkatu(ByVal ds As DataSet _
                                            , ByVal prm As LMFormData _
                                            , ByVal list As ArrayList)

        Dim dt As DataTable = ds.Tables(LMM100C.TABLE_NM_GOODS)
        Dim dr As DataRow = Nothing
        Dim max As Integer = list.Count - 1
        Dim rtnDr As DataRow = prm.ParamDataSet.Tables(LMZ040C.TABLE_NM_OUT).Rows(0)
        Dim upGpCd1 As String = rtnDr.Item("UP_GP_CD_1").ToString()
        Dim row As Integer = 0

        For i As Integer = 0 To max

            With Me._Frm.sprGoods.ActiveSheet

                dr = dt.NewRow()
                row = Convert.ToInt32(list(i).ToString)

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd
                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("GOODS_CD_NRS") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_KEY.ColNo))
                dr.Item("CUST_CD_L") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CD_M.ColNo))
                dr.Item("UP_GP_CD_1") = upGpCd1
                dr.Item("ONDO_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_KBN.ColNo))
                dr.Item("GOODS_CD_CUST") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_CD.ColNo))
                dr.Item("RECORD_NO") = row
                '更新時共通項目設定
                dr.Item("SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_TIME.ColNo))

                dt.Rows.Add(dr)

            End With

        Next

    End Sub
    'END YANAI 要望番号372

    '2015.10.02 他荷主対応START
    ''' <summary>
    ''' データセット設定(他荷主)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInDataTaninusi(ByVal ds As DataSet _
                                            , ByVal prm As LMFormData _
                                            , ByVal list As ArrayList)

        Dim dt As DataTable = ds.Tables(LMM100C.TABLE_NM_GOODS)
        Dim dr As DataRow = Nothing
        Dim max As Integer = list.Count - 1
        Dim rtnDr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
        Dim row As Integer = 0

        For i As Integer = 0 To max

            With Me._Frm.sprGoods.ActiveSheet

                dr = dt.NewRow()
                row = Convert.ToInt32(list(i).ToString)

                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("GOODS_CD_NRS") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_KEY.ColNo))
                dr.Item("CUST_CD_L") = rtnDr.Item("CUST_CD_L").ToString()
                dr.Item("CUST_CD_M") = rtnDr.Item("CUST_CD_M").ToString()
                dr.Item("CUST_CD_S") = rtnDr.Item("CUST_CD_S").ToString()
                dr.Item("CUST_CD_SS") = rtnDr.Item("CUST_CD_SS").ToString()
                dr.Item("GOODS_CD_CUST") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_CD.ColNo))

                dr.Item("SEARCH_KEY_1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CATEGORY_1.ColNo))
                dr.Item("SEARCH_KEY_2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_CATEGORY_2.ColNo))
                dr.Item("CUST_COST_CD1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_KANJO_KMK_CD_1.ColNo))
                dr.Item("CUST_COST_CD2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CUST_KANJO_KMK_CD_2.ColNo))
                dr.Item("JAN_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BAR_CD.ColNo))
                dr.Item("GOODS_NM_1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_NM_1.ColNo))
                dr.Item("GOODS_NM_2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_NM_2.ColNo))
                dr.Item("GOODS_NM_3") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_NM_3.ColNo))
                dr.Item("UP_GP_CD_1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.TANKA_GROUP_CD.ColNo))
                dr.Item("SHOBO_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHOBO_CD.ColNo))
                dr.Item("KIKEN_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KIKENHIN_KBN.ColNo))
                dr.Item("KOUATHUGAS_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KOUATHUGAS_KB.ColNo))
                dr.Item("YAKUZIHO_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.YAKUZIHO_KB.ColNo))
                dr.Item("SHOBOKIKEN_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHOBOKIKEN_KB.ColNo))
                dr.Item("UN") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UN.ColNo))
                dr.Item("PG_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.PG.ColNo))
                dr.Item("CLASS_1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CLASS_1.ColNo))
                dr.Item("CLASS_2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CLASS_2.ColNo))
                dr.Item("CLASS_3") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CLASS_3.ColNo))
                dr.Item("KAIYOUOSEN_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KAIYOUOSEN_KB.ColNo))
                dr.Item("KAIYOUOSEN_KB_NM") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KAIYOUOSEN_KB_NM.ColNo))
                dr.Item("SIZE_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SIZE_KBN.ColNo))
                dr.Item("CHEM_MTRL_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KAGAKUBUSITU_KBN.ColNo))
                dr.Item("DOKU_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.DOKUGEKI_KBN.ColNo))
                dr.Item("GAS_KANRI_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GUS_KANRI_KBN.ColNo))
                dr.Item("ONDO_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_KBN.ColNo))
                dr.Item("UNSO_ONDO_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KBN_UNSO.ColNo))
                dr.Item("ONDO_MX") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_MAX.ColNo))
                dr.Item("ONDO_MM") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_MIN.ColNo))
                dr.Item("ONDO_STR_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_START_HOKAN.ColNo))
                dr.Item("ONDO_END_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_END_HOKAN.ColNo))
                dr.Item("ONDO_UNSO_STR_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_START_UNSO.ColNo))
                dr.Item("ONDO_UNSO_END_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ONDO_KANRI_END_UNSO.ColNo))
                dr.Item("KYOKAI_GOODS_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SOKO_KYOKAI_HIN_KBN.ColNo))
                dr.Item("ALCTD_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HIKIATE_TANI_KBN.ColNo))
                dr.Item("NB_UT") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KOSU_TANI_KBN.ColNo))
                dr.Item("PKG_NB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.IRI_SU.ColNo))
                dr.Item("PKG_UT") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HOSO_TANI.ColNo))
                dr.Item("PLT_PER_PKG_UT") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.PALETTO_HOSOKOSU.ColNo))
                dr.Item("INNER_PKG_NB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.INNER_PKG_NB.ColNo))
                dr.Item("STD_IRIME_NB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HYOJUN_IRIME.ColNo))
                dr.Item("STD_IRIME_UT") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HYOJUN_IRIME_TANI.ColNo))
                dr.Item("STD_WT_KGS") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HYOJUN_JURYO.ColNo))
                dr.Item("HIZYU") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HIZYU.ColNo))
                dr.Item("STD_CBM") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HYOJYUN_YOSEKI.ColNo))
                dr.Item("INKA_KAKO_SAGYO_KB_1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_1.ColNo))
                dr.Item("INKA_KAKO_SAGYO_KB_2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_2.ColNo))
                dr.Item("INKA_KAKO_SAGYO_KB_3") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_3.ColNo))
                dr.Item("INKA_KAKO_SAGYO_KB_4") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_4.ColNo))
                dr.Item("INKA_KAKO_SAGYO_KB_5") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NYUKAJI_SAGYO_KBN_5.ColNo))
                dr.Item("OUTKA_KAKO_SAGYO_KB_1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_1.ColNo))
                dr.Item("OUTKA_KAKO_SAGYO_KB_2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_2.ColNo))
                dr.Item("OUTKA_KAKO_SAGYO_KB_3") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_3.ColNo))
                dr.Item("OUTKA_KAKO_SAGYO_KB_4") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_4.ColNo))
                dr.Item("OUTKA_KAKO_SAGYO_KB_5") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_SAGYO_KBN_5.ColNo))
                dr.Item("PKG_SAGYO") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KONPO_SAGYO_CD.ColNo))
                dr.Item("TARE_YN") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HUTAI_KASAN_FLG.ColNo))
                dr.Item("SP_NHS_YN") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SITEI_NOHINSHO_KBN.ColNo))
                dr.Item("COA_YN") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BUNSEKI_HYO_KBN.ColNo))
                dr.Item("LOT_CTL_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.LOT_KANRI_LEVEL_CD.ColNo))
                dr.Item("LT_DATE_CTL_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHOMIKIGEN_KANRI_CD.ColNo))
                dr.Item("CRT_DATE_CTL_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SEIZOBI_KANRI_CD.ColNo))
                dr.Item("DEF_SPD_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KITEI_HORYUHIN_KBN.ColNo))
                dr.Item("KITAKU_AM_UT_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KITAKU_KAKAKU_TANI_KBN.ColNo))
                dr.Item("KITAKU_GOODS_UP") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.KITAKU_SHOHIN_TANKA.ColNo))
                dr.Item("ORDER_KB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HACCHUTEN_KBN.ColNo))
                dr.Item("ORDER_NB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HACCHU_SURYO.ColNo))
                dr.Item("SHIP_CD_L") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NISONIN_CD_L.ColNo))
                dr.Item("SKYU_MEI_YN") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HIKIATE_CHUIHIN_FLG.ColNo))
                dr.Item("HIKIATE_ALERT_YN") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HIKIATE_CHUIHIN_FLG.ColNo))
                dr.Item("OUTKA_ATT") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHUKKAJI_CHUIJIKO.ColNo))
                dr.Item("PRINT_NB") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.NIHUDA_INSATU_SU.ColNo))
                dr.Item("CONSUME_PERIOD_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SHOHIKIGEN_KINSHIBI.ColNo))
                dr.Item("OCR_GOODS_CD_CUST") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_CUST.ColNo))
                dr.Item("OCR_GOODS_CD_NM1") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_NM1.ColNo))
                dr.Item("OCR_GOODS_CD_NM2") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_NM2.ColNo))
                dr.Item("OCR_GOODS_CD_STD_IRIME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.OCR_GOODS_CD_STD_IRIME.ColNo))
                dr.Item("RECORD_NO") = row
                dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.SYS_DEL_FLG.ColNo))

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
                dr.Item(LMM100C.LMM100OUT_CNAME.OUTER_PKG) = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.OUTER_PACKEGE.ColNo))
#End If
                dr.Item("WIDTH") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.WIDTH.ColNo))
                dr.Item("HEIGHT") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.HEIGHT.ColNo))
                dr.Item("DEPTH") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.DEPTH.ColNo))
                dr.Item("ACTUAL_VOLUME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.ACTUAL_VOLUME.ColNo))
                dr.Item("OCCUPY_VOLUME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.OCCUPY_VOLUME.ColNo))
                dr.Item("CYL_FLG") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.CYL_FLG.ColNo))

                dt.Rows.Add(dr)

            End With

        Next

    End Sub
    '2015.10.02 他荷主対応END

    ''' <summary>
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetSave(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM100C.TABLE_NM_GOODS).NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdateDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdateTime.TextValue.Trim
            '登録項目格納
            '******************* 商品タブ *****************************
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("GOODS_CD_NRS") = .lblGoodsKey.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("CUST_CD_S") = .txtCustCdS.TextValue
            dr.Item("CUST_CD_SS") = .txtCustCdSS.TextValue
            dr.Item("GOODS_CD_CUST") = .txtGoodsCd.TextValue
            dr.Item("SEARCH_KEY_1") = .txtCustCategory1.TextValue
            dr.Item("SEARCH_KEY_2") = .txtCustCategory2.TextValue
            dr.Item("CUST_COST_CD1") = .txtCustKanjokamoku1.TextValue
            dr.Item("CUST_COST_CD2") = .txtCustKanjokamoku2.TextValue
            dr.Item("JAN_CD") = .txtBarkodo.TextValue
            dr.Item("GOODS_NM_1") = .txtGoodsNm1.TextValue
            dr.Item("GOODS_NM_2") = .txtGoodsNm2.TextValue
            dr.Item("GOODS_NM_3") = .txtGoodsNm3.TextValue
            dr.Item("UP_GP_CD_1") = .txtTankaGroup.TextValue
            dr.Item("SHOBO_CD") = .txtShobo.TextValue
            dr.Item("KIKEN_KB") = .cmbKikenhin.SelectedValue
            dr.Item("KOUATHUGAS_KB") = .cmbKouathugas.SelectedValue
            dr.Item("YAKUZIHO_KB") = .cmbYakuziho.SelectedValue
            dr.Item("SHOBOKIKEN_KB") = .cmbShobokiken.SelectedValue
            dr.Item("UN") = .txtUn.TextValue
            dr.Item("PG_KB") = .txtPg.TextValue
            'dr.Item("PG_KB") = .cmbPg.SelectedValue
            '非該当は空で登録
            If .txtUn.TextValue <> "-" Then
                dr.Item("CLASS_1") = .lblClass1.TextValue
                dr.Item("CLASS_2") = .lblClass2.TextValue
                dr.Item("CLASS_3") = .lblClass3.TextValue
            Else
                dr.Item("CLASS_1") = String.Empty
                dr.Item("CLASS_2") = String.Empty
                dr.Item("CLASS_3") = String.Empty
            End If
            dr.Item("KAIYOUOSEN_KB") = .txtKaiyouosen.TextValue
            dr.Item("KAIYOUOSEN_KB_NM") = .lblKaiyouosen.TextValue
            dr.Item("SIZE_KB") = .cmbSizeKbn.SelectedValue '検証結果№70対応(2011.09.08)
            dr.Item("CHEM_MTRL_KB") = .cmbKagakuBusitu.SelectedValue
            dr.Item("DOKU_KB") = .cmbDokugeki.SelectedValue
            dr.Item("KOUATHUGAS_KB") = .cmbKouathugas.SelectedValue
            dr.Item("YAKUZIHO_KB") = .cmbYakuziho.SelectedValue
            dr.Item("SHOBOKIKEN_KB") = .cmbShobokiken.SelectedValue
            dr.Item("GAS_KANRI_KB") = .cmbGusKanri.SelectedValue
            dr.Item("ONDO_KB") = .cmbHokanKbnHokan.SelectedValue
            dr.Item("UNSO_ONDO_KB") = .cmbHokanKbnUnso.SelectedValue
            dr.Item("ONDO_MX") = .numOndoKanriMax.Value
            dr.Item("ONDO_MM") = .numOndoKanriMin.Value
            Dim ondoStart As String = .imdOndoKanriStartHokan.TextValue
            Dim ondoEnd As String = .imdOndoKanriEndHokan.TextValue
            If String.IsNullOrEmpty(ondoStart) = False Then
                ondoStart = ondoStart.Substring(4, 4)
            End If
            If String.IsNullOrEmpty(ondoEnd) = False Then
                ondoEnd = ondoEnd.Substring(4, 4)
            End If
            dr.Item("ONDO_STR_DATE") = ondoStart
            dr.Item("ONDO_END_DATE") = ondoEnd
            ondoStart = .imdOndoKanriStartUnso.TextValue
            ondoEnd = .imdOndoKanriEndUnso.TextValue
            If String.IsNullOrEmpty(ondoStart) = False Then
                ondoStart = ondoStart.Substring(4, 4)
            End If
            If String.IsNullOrEmpty(ondoEnd) = False Then
                ondoEnd = ondoEnd.Substring(4, 4)
            End If
            dr.Item("ONDO_UNSO_STR_DATE") = ondoStart
            dr.Item("ONDO_UNSO_END_DATE") = ondoEnd
            dr.Item("KYOKAI_GOODS_KB") = .cmbSokoHinmoku.SelectedValue
            If .optKosu.Checked = True Then
                dr.Item("ALCTD_KB") = LMM100C.HIKIATE_TANI_KOSU
            Else
                dr.Item("ALCTD_KB") = LMM100C.HIKIATE_TANI_SURYO
            End If
            dr.Item("NB_UT") = .cmbKosuTani.SelectedValue
            dr.Item("PKG_NB") = .numIrisu.Value
            dr.Item("PKG_UT") = .cmbHosotani.SelectedValue
            dr.Item("PLT_PER_PKG_UT") = .numPalettoSu.Value
            dr.Item("INNER_PKG_NB") = .numInnerPkgNb.Value
            dr.Item("STD_IRIME_NB") = .numHyojyunIrime.Value
            dr.Item("STD_IRIME_UT") = .cmbHyojyunIrimeTani.SelectedValue
            dr.Item("STD_WT_KGS") = .numHyojyunJyuryo.Value
            dr.Item("HIZYU") = .numHizyu.Value
            dr.Item("STD_CBM") = .numHyojyunYoseki.Value
            dr.Item("INKA_KAKO_SAGYO_KB_1") = .txtNyukaSagyoKbn1.TextValue
            dr.Item("INKA_KAKO_SAGYO_KB_2") = .txtNyukaSagyoKbn2.TextValue
            dr.Item("INKA_KAKO_SAGYO_KB_3") = .txtNyukaSagyoKbn3.TextValue
            dr.Item("INKA_KAKO_SAGYO_KB_4") = .txtNyukaSagyoKbn4.TextValue
            dr.Item("INKA_KAKO_SAGYO_KB_5") = .txtNyukaSagyoKbn5.TextValue
            dr.Item("OUTKA_KAKO_SAGYO_KB_1") = .txtShukkaSagyoKbn1.TextValue
            dr.Item("OUTKA_KAKO_SAGYO_KB_2") = .txtShukkaSagyoKbn2.TextValue
            dr.Item("OUTKA_KAKO_SAGYO_KB_3") = .txtShukkaSagyoKbn3.TextValue
            dr.Item("OUTKA_KAKO_SAGYO_KB_4") = .txtShukkaSagyoKbn4.TextValue
            dr.Item("OUTKA_KAKO_SAGYO_KB_5") = .txtShukkaSagyoKbn5.TextValue
            dr.Item("PKG_SAGYO") = .txtKonpoSagyoCd.TextValue
            dr.Item("TARE_YN") = .cmbHutaiJyuryo.SelectedValue
            dr.Item("SP_NHS_YN") = .cmbSiteiNohinSho.SelectedValue
            dr.Item("COA_YN") = .cmbBunsekiHyo.SelectedValue
            dr.Item("LOT_CTL_KB") = .cmbLotKanriLevel.SelectedValue
            dr.Item("LT_DATE_CTL_KB") = .cmbShomiKigenKanri.SelectedValue
            dr.Item("CRT_DATE_CTL_KB") = .cmbSeizobiKanri.SelectedValue
            dr.Item("DEF_SPD_KB") = .cmbKiteiHoryuhinKbn.SelectedValue
            dr.Item("KITAKU_AM_UT_KB") = .cmbKitakuKakaku.SelectedValue
            dr.Item("KITAKU_GOODS_UP") = .numKitakuShohinTanka.Value
            dr.Item("ORDER_KB") = .cmbHacchuten.SelectedValue
            dr.Item("ORDER_NB") = .numHaccyuSuryo.Value
            dr.Item("SHIP_CD_L") = .txtNisonin.TextValue
            dr.Item("SKYU_MEI_YN") = .cmbSeikyuMeisaisho.SelectedValue
            dr.Item("UNSO_HOKEN_YN") = .cmbUnsoHoken.SelectedValue          'ADD 2018/07/17 依頼番号 001540
            dr.Item("HIKIATE_ALERT_YN") = .cmbHikiateChuiHin.SelectedValue
            dr.Item("OUTKA_ATT") = .txtShukkaChuiJiko.TextValue
            dr.Item("PRINT_NB") = .numNihudaInsatu.Value
            dr.Item("CONSUME_PERIOD_DATE") = .numShohikigenKinshi.Value
            '20150730 常平add
            dr.Item("OCR_GOODS_CD_CUST") = .TxtBoxGoodsCd.TextValue
            dr.Item("OCR_GOODS_CD_NM1") = .TxtBoxGoodsNM1.TextValue
            dr.Item("OCR_GOODS_CD_NM2") = .TxtBoxGoodsNM2.TextValue
            dr.Item("OCR_GOODS_CD_STD_IRIME") = .TxtBoxIrime.TextValue

#If True Then ' 要望番号2471対応 added 2015.12.14 inoue
            dr.Item(LMM100C.LMM100OUT_CNAME.OUTER_PKG) = .cmbOuterPackage.SelectedValue
#End If
            dr.Item("WIDTH") = .numWidth.Value
            dr.Item("DEPTH") = .numDepth.Value
            dr.Item("HEIGHT") = .numHeight.Value
            dr.Item("ACTUAL_VOLUME") = .numActualVolume.Value
            dr.Item("OCCUPY_VOLUME") = .numOccupyVolume.Value
            dr.Item("CYL_FLG") = .lblCylFlg.TextValue

            'チェック時使用項目
            dr.Item("WARNING_FLG") = LMConst.FLG.OFF
            dr.Item("CHK_CUST_CD_S") = .lblCustCdS.TextValue
            dr.Item("CHK_CUST_CD_SS") = .lblCustCdSS.TextValue

            '要望対応1995 端数作業対応
            dr.Item("OUTKA_HASU_SAGYO_KB_1") = .txtHasuSagyoKbn1.TextValue
            dr.Item("OUTKA_HASU_SAGYO_KB_2") = .txtHasuSagyoKbn2.TextValue
            dr.Item("OUTKA_HASU_SAGYO_KB_3") = .txtHasuSagyoKbn3.TextValue

            dr.Item("AVAL_YN") = .cmbAVAL_YN.SelectedValue      'ADD 2019/04/22 依頼番号 : 005252   【LMS】商品マスタの整理機能

            ds.Tables(LMM100C.TABLE_NM_GOODS).Rows.Add(dr)

            '商品明細内容をDataSetに格納する
            Call Me.SetDataSetDtlSave(ds)

        End With

    End Sub

#If True Then   'ADD 2020/02/26 010140【LMS】フィルメ誤出荷類似_EDI取込、出荷登録共に個別荷主機能使用(高度化和地)
    ''' <summary>
    ''' データセット設定(保存時チェック用)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetSaveCHK(ByVal ds As DataSet) As DataSet

        ds.Tables(LMM100C.TABLE_NM_SAVE_CHK).Clear()
        Dim drSave As DataRow = ds.Tables(LMM100C.TABLE_NM_SAVE_CHK).NewRow()

        With Me._Frm
            'M_EDI_CUSTに存在チェック
            Dim EDI_CUST_FLG As String = LMConst.FLG.OFF

            Dim selectStr As String = String.Empty
            selectStr = String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue, "'", _
                                                    " AND CUST_CD_L = '", .txtCustCdL.TextValue, "'", _
                                                    " AND CUST_CD_M = '", .txtCustCdM.TextValue, "'", _
                                                    " AND SYS_DEL_FLG = '0'")

            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(selectStr)

            If drs.Length > 0 Then
                'EDI_CUST設定有
                EDI_CUST_FLG = LMConst.FLG.ON
            End If
            drSave.Item("EDI_CUST_FLG") = EDI_CUST_FLG.ToString

            '荷主詳細に
            '荷主用途区分「同一商品コード入数違い許可」を追加し、「1」が設定されている場合はエラーとしない。
            Dim CUST_DETAILS_9ZFLG As String = LMConst.FLG.OFF

            '荷主詳細  SUB_KB = '9Z'より、同一商品コード入数違い許可取得
            Dim sSql As String = "NRS_BR_CD = '" & .cmbBr.SelectedValue.ToString & "' AND CUST_CD = '" & .txtCustCdL.TextValue.ToString() & .txtCustCdM.TextValue.ToString() & "' " _
                                 & " AND SUB_KB = '9Z' AND SET_NAIYO = '1'"
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sSql)
            If dr.Length > 0 Then
                CUST_DETAILS_9ZFLG = LMConst.FLG.ON
            End If
            drSave.Item("CUST_DETAILS_9ZFLG") = CUST_DETAILS_9ZFLG.ToString

            '入数変更可能か
            Dim IRISU_EDIT_FLG As String = LMConst.FLG.OFF

            If Me._ExistZaikoFlg = False Then
                IRISU_EDIT_FLG = LMConst.FLG.ON
            End If
            drSave.Item("IRISU_EDIT_FLG") = IRISU_EDIT_FLG.ToString

            ds.Tables(LMM100C.TABLE_NM_SAVE_CHK).Rows.Add(drSave)

        End With

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMM100BLF", "GET_GOODSM_CUST", ds)
        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "GET_GOODSM_CUST")

        Return ds

    End Function
#End If
    ''' <summary>
    ''' データセット設定(保存処理(商品明細))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetDtlSave(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM100C.TABLE_NM_GOODS_DTL)
        Dim max As Integer = Me._Frm.sprGoodsDetail.ActiveSheet.Rows.Count - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To max

            With Me._Frm.sprGoodsDetail.ActiveSheet

                dr = dt.NewRow()

                dr.Item("NRS_BR_CD") = Me._Frm.cmbBr.SelectedValue
                dr.Item("GOODS_CD_NRS") = Me._Frm.lblGoodsKey.TextValue

                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM100G.sprGoodsDtlDef.EDA_NO.ColNo))) Then
                    Me._MaxEdaNoSet = Me._MaxEdaNoSet + 1
                    dr.Item("GOODS_CD_NRS_EDA") = Me._MaxEdaNoSet.ToString().PadLeft(2, Convert.ToChar("0"))
                Else
                    dr.Item("GOODS_CD_NRS_EDA") = Me._ControlV.GetCellValue(.Cells(i, LMM100G.sprGoodsDtlDef.EDA_NO.ColNo))
                End If
                dr.Item("SUB_KB") = Me._ControlV.GetCellValue(.Cells(i, LMM100G.sprGoodsDtlDef.YOTO_KBN.ColNo))
                dr.Item("SET_NAIYO") = Me._ControlV.GetCellValue(.Cells(i, LMM100G.sprGoodsDtlDef.SETTEI_VALUE.ColNo))
                dr.Item("REMARK") = Me._ControlV.GetCellValue(.Cells(i, LMM100G.sprGoodsDtlDef.BIKO.ColNo))

                dt.Rows.Add(dr)

            End With
        Next

    End Sub

    ''' <summary>
    ''' データセット設定(危険品情報確認処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetConfKiken(ByVal ds As DataSet _
                                            , ByVal list As ArrayList)

        Dim dt As DataTable = ds.Tables(LMM100C.TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = list.Count - 1
        Dim row As Integer = 0

        For i As Integer = 0 To max

            With Me._Frm.sprGoods.ActiveSheet

                dr = dt.NewRow()
                row = Convert.ToInt32(list(i).ToString)

                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("GOODS_CD_NRS") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_KEY.ColNo))
                dr.Item("RECORD_NO") = row
                dr.Item("SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_TIME.ColNo))
                dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dt.Rows.Add(dr)

            End With

        Next

    End Sub

    ''' <summary>
    ''' データセット設定(容積一括更新処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetUpdateGoodsVolume(ByVal ds As DataSet _
                                            , ByVal list As ArrayList)

        Dim dt As DataTable = ds.Tables(LMM100C.TABLE_NM_GOODS)
        Dim dr As DataRow = Nothing
        Dim max As Integer = list.Count - 1
        Dim row As Integer = 0

        For i As Integer = 0 To max

            With Me._Frm.sprGoods.ActiveSheet

                dr = dt.NewRow()
                row = Convert.ToInt32(list(i).ToString)

                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("GOODS_CD_NRS") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.GOODS_KEY.ColNo))
                dr.Item("RECORD_NO") = row
                dr.Item("SYS_UPD_DATE") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.UPDATE_TIME.ColNo))
                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(row, LMM100G.sprGoodsDef.BR_CD.ColNo))
                dr.Item("WIDTH") = Me._Frm.numWidthBulk.Value
                dr.Item("DEPTH") = Me._Frm.numDepthBulk.Value
                dr.Item("HEIGHT") = Me._Frm.numHeightBulk.Value
                dr.Item("ACTUAL_VOLUME") = Me._Frm.numActualVolumeBulk.Value
                dr.Item("OCCUPY_VOLUME") = Me._Frm.numOccupyVolumeBulk.Value

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
    Friend Sub FunctionKey1Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey5Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateSameTime")

        '単価一括変更
        Me.UpdateSameTime()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateSameTime")

    End Sub

    'START YANAI 要望番号372
    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateNinushi")

        '荷主一括変更
        Me.UpdateNinushi()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateNinushi")

    End Sub
    'END YANAI 要望番号372

    '2015.10.02 他荷主対応START
    ''' <summary>
    ''' F7押下時処理呼び出し(他荷主)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "InsertTaninusi")

        '他荷主
        Me.InsertTaninusi()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "InsertTaninusi")

    End Sub
    '2015.10.02 他荷主対応END
    ''' <summary>
    ''' F8押下時処理呼び出し(危険品確認)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ConfirmKikenGoods")

        '危険品情報確認
        Me.ConfirmKikenGoods()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ConfirmKikenGoods")

    End Sub
    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent(LMM100C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM100F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM100F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Friend Sub LMM100FKeyDown(ByVal frm As LMM100F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM100F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM100F_KeyDown")

    End Sub

    ''' <summary>
    ''' Addボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub AddClick(ByVal frm As LMM100F, ByVal e As System.EventArgs)

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
    Friend Sub DelClick(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelClick")

        '行削除処理
        Call Me.RowDel()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelClick")

    End Sub

    ''' <summary>
    ''' Printボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub PrintClick(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintClick")

        '印刷処理
        Call Me.BtnPrintClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintClick")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprGoodsLeaveCell(ByVal frm As LMM100F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprGoodsLeaveCell")

        Call Me.SprFindLeaveCell(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprGoodsLeaveCell")

    End Sub

    ''' <summary>
    ''' 個数単位値変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbKosuTani_SelectedValueChanged(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbKosuTani_SelectedValueChanged")

        Call Me.SetKosuTani()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbKosuTani_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' 荷姿単位変更時のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub cmbHosotani_SelectedValueChanged(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbHosotani_SelectedValueChanged")

        Call Me._G.SetHutaiJyuryo()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbHosotani_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' 高圧ガス区分変更時のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub cmbKouathugas_SelectedValueChanged(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbKouathugas_SelectedValueChanged")

        Call Me._G.SetGusKanri()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbKouathugas_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' 消防危険品区分変更時のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub cmbShobokiken_SelectedValueChanged(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbShobokiken_SelectedValueChanged")

        Call Me._G.SetKikenhin()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbShobokiken_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' カーソル移動時(幅)のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub numWidth_Leave(ByVal frm As LMM100F, ByVal e As System.EventArgs)
        Call Me.CalcVolume()
    End Sub

    ''' <summary>
    ''' カーソル移動時(高さ)のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub numHeight_Leave(ByVal frm As LMM100F, ByVal e As System.EventArgs)
        Call Me.CalcVolume()
    End Sub

    ''' <summary>
    ''' カーソル移動時(奥行)のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub numDepth_Leave(ByVal frm As LMM100F, ByVal e As System.EventArgs)
        Call Me.CalcVolume()
    End Sub
    ''' <summary>
    ''' カーソル移動時(幅)のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub numWidthBulk_Leave(ByVal frm As LMM100F, ByVal e As System.EventArgs)
        Call Me.CalcVolumeBulk()
    End Sub

    ''' <summary>
    ''' カーソル移動時(高さ)のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub numHeightBulk_Leave(ByVal frm As LMM100F, ByVal e As System.EventArgs)
        Call Me.CalcVolumeBulk()
    End Sub

    ''' <summary>
    ''' カーソル移動時(奥行)のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub numDepthBulk_Leave(ByVal frm As LMM100F, ByVal e As System.EventArgs)
        Call Me.CalcVolumeBulk()
    End Sub
    ''' <summary>
    ''' Updateボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub UpdateClick(ByVal frm As LMM100F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateClick")

        '容積一括更新処理
        Call Me.UpdateGoodsVolume()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

    ''' <summary>
    ''' 円周率の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetCircleRate()

        '区分マスタから円周率を取得
        Dim selectStr As String = "KBN_GROUP_CD = 'E048' AND KBN_CD = '01'"
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(selectStr)

        If 0 < drs.Length Then
            Me._CircleRate = Convert.ToDecimal(drs(0).Item("KBN_NM1"))
        Else
            Me._CircleRate = Convert.ToDecimal("3.14")
        End If

    End Sub

End Class