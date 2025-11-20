' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB010    : 入荷データ検索
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Microsoft.Office.Interop

''' <summary>
''' LMB010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler


#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB010G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconV As LMBControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconH As LMBControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconG As LMBControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

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

#End Region 'Field

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMB010F = New LMB010F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbSoko)

        'Validateクラスの設定
        Me._V = New LMB010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMB010G(Me, frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMB010C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetPGID(), frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue(frm)

        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'Validate共通クラスの設定
        Me._LMBconV = New LMBControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMBconH = New LMBControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMBconG = New LMBControlG(DirectCast(frm, Form))

    End Sub

#End Region '初期処理

#Region "外部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMB010F)

        '処理開始アクション
        Call Me._LMBconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMB010C.EventShubetsu.KENSAKU) = False Then
            Call Me._LMBconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuSingleCheck(Me._G) = False Then
            Call Me._LMBconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '関連チェック
        If Me._V.IsKensakuKanrenCheck = False Then
            Call Me._LMBconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        'キャッシュから名称取得
        Call SetCachedName(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '処理終了アクション
        Call Me._LMBconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMB010C.EventShubetsu, ByVal frm As LMB010F)

        If LMB010C.EventShubetsu.DOUBLE_CLICK <> eventShubetsu Then

            '処理開始アクション
            Call Me._LMBconH.StartAction(frm)

        End If

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
          Call Me._LMBconH.EndAction(frm)
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'パラメータ設定
        prm.ReturnFlg = False
     
        Select Case eventShubetsu

            Case LMB010C.EventShubetsu.SINKI

                '「新規」処理の場合、荷主マスタ参照画面を開く
                'START YANAI 要望番号481
                'Call Me.ShowCustPopup(frm, LMB010C.EventShubetsu.SINKI.ToString(), prm)
                Call Me.ShowCustPopup(frm, LMB010C.EventShubetsu.SINKI.ToString(), prm, LMB010C.EventShubetsu.SINKI)
                'END YANAI 要望番号481

                'START YANAI メモ②No.28
                MyBase.ShowMessage(frm, "G007")
                'EMD YANAI メモ②No.28

            Case LMB010C.EventShubetsu.DEF_CUST
                '初回荷主変更の場合

                '初回荷主変更Popup呼出
                'START YANAI 要望番号481
                'Call Me.ShowCustPopup(frm, frm.FunctionKey.F9ButtonName, prm)
                Call Me.ShowCustPopup(frm, frm.FunctionKey.F9ButtonName, prm, LMB010C.EventShubetsu.DEF_CUST)
                'END YANAI 要望番号481

                'START YANAI メモ②No.28
                MyBase.ShowMessage(frm, "G007")
                'EMD YANAI メモ②No.28

                'START YANAI メモ②No.28
            Case LMB010C.EventShubetsu.JIKKOU
                '実行ボタン押下時

                Dim arr As ArrayList = Nothing
                arr = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB010C.SprColumnIndex.DEF)

                'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
                'チェック処理
                If Me._V.IsJikkoSingleCheck(arr) = False  Then
                    Call Me._LMBconH.EndAction(frm) '終了処理
                    Exit Sub
                End If
                'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

                'START YANAI 20120121 作業一括処理対応
                If ("01").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'END YANAI 20120121 作業一括処理対応

                    'チェック処理
                    If Me._V.IsJikkouSingleCheck(arr, Me._G) = False OrElse
                        Me._V.IsJikkouKanrenCheck(arr, Me._G) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    'EDIデータ取得
                    '検索処理を行う
                    Dim ds As DataSet = Me.SelectEdiData(frm, arr)

                    'チェック処理2
                    If Me._V.IsJikkouKanrenCheck2(ds, arr, Me._G) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '実行処理
                    Call Me.JikkoShori(frm, arr)
                    'EMD YANAI メモ②No.28

                    'START YANAI 20120121 作業一括処理対応

                ElseIf ("02").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '作業一括削除

                    'チェック処理
                    If Me._V.IsSagyoSingleCheck(arr) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '作業一括削除処理
                    Call Me.DelSagyoShori(frm, arr)

                ElseIf ("03").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '作業一括作成
                    'チェック処理
                    If Me._V.IsSagyoSingleCheck(arr) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '作業一括作成処理
                    Call Me.MakeSagyoShori(frm, arr)

                    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
                ElseIf ("04").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '出荷データ作成
                    'チェック処理
                    If Me._V.IsOutkaSingleCheck(arr) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '出荷データ作成処理
                    Call Me.MakeOutkaShori(frm, arr)
                    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

                    '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 Start
                ElseIf ("05").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '出荷データ作成
                    'チェック処理
                    If Me._V.IsUtiSingleCheck(arr, Me._G) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '出荷データ作成処理
                    Call Me.MakeOutkaShori(frm, arr, "1")
                    '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 End

                ElseIf ("06").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'UTI報告済み取消対応 s.kobayashi 2013.1.29 Start
                    'チェック処理
                    If Me._V.IsUtiTorikeshiSingleCheck(arr, Me._G) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '入荷報告済取り消し処理
                    Call Me.InkaHokokuCancel(frm, arr, "1")

                ElseIf ("07").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'WIT対応 入荷データ一括取込対応 kasama

                    'チェック処理
                    If Me._V.IsInkaDataIkkatuTorikomiSingleCheck(arr, Me._G) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '入荷データ一括取込処理
                    Call Me.InkaIkkatuTorikomiShori(frm, arr)

                ElseIf ("08").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '入荷予定データ作成

                    'チェック処理
                    If Me._V.IsInkaDataYoteiSingleCheck(arr, Me._G) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '入荷予定データ作成処理
                    Call Me.InkaYoteiiSakuseiShori(frm, arr)

                    '顧客WEB入荷登録バッチ対応 t.kido Start
                ElseIf ("09").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '顧客WEB入荷登録

                    'チェック処理
                    If Me._V.IsWebInkaSingleCheck() = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    '顧客WEB入荷登録処理
                    Call Me.WebInkaTorokuShori(frm)
                    '顧客WEB入荷登録バッチ対応 t.kido End

                ElseIf ("10").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    '現場作業指示

                    ''チェック処理
                    'If Me._V.IsInkaDataYoteiSingleCheck(arr, Me._G) = False Then
                    '    Call Me._LMBconH.EndAction(frm) '終了処理
                    '    Exit Sub
                    'End If

                    Call Me.WHSagyoSiji(frm, arr)

                ElseIf ("11").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    'X-Track入荷登録

                    'チェック処理
                    If Me._V.IsWebInkaSingleCheck() = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    'X-Track入荷登録処理
                    Call Me.XTrackInkaTorokuShori(frm)

                ElseIf ("12").Equals(frm.cmbJikkou.SelectedValue) = True Then
                    ' RFIDラベルデータ作成

                    ' チェック処理
                    If Me._V.IsMakeRfidLavelDataSingleCheck(arr) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                    ' RFIDラベルデータ作成処理処理
                    Call Me.MakeRfidLavelData(frm, arr)

                Else
                    'チェック処理
                    If Me._V.IsSingleCheck(arr) = False Then
                        Call Me._LMBconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If

                End If
                'END YANAI 20120121 作業一括処理対応

                'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
            Case LMB010C.EventShubetsu.PRINT
                '印刷ボタン押下時

                Dim arr As ArrayList = Nothing
                arr = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB010C.SprColumnIndex.DEF)

                'チェック処理
                If Me._V.IsPrintSingleCheck(arr) = False OrElse _
                    Me._V.IsPrintKanrenCheck(arr) = False Then
                    Call Me._LMBconH.EndAction(frm) '終了処理
                    Exit Sub
                End If

                '追加開始 --- 2014.09.16 kikuchi
                '入荷(小)存在チェック処理
                If frm.cmbPrint.SelectedValue.Equals("02") AndAlso _
                   Me._V.IsInkaSCheck(arr, Me._G) = False Then 'チェックリストのみ対象
                    Call Me._LMBconH.EndAction(frm) '終了処理
                    Exit Sub
                End If
                '追加終了 --- 

                '印刷処理
                'UPD 2017/06/27 アクサルタGHSラベル対応
                If frm.cmbPrint.SelectedValue.Equals("06") = False Then
                    'GHSラベル以外
                    Call Me.PrintAction(frm, arr)

                Else
                    'GHSラベルのとき
                    Call Me.PrintGHSAction(frm, arr)
                End If

                'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

        End Select

        'START YANAI メモ②No.28
        'MyBase.ShowMessage(frm, "G007")
        'EMD YANAI メモ②No.28

        '終了アクション
        Call Me._LMBconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 完了処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub KanryouDataAction(ByVal frm As LMB010F)

        '処理開始アクション
        Call Me._LMBconH.StartAction(frm)

        '権限チェック
        Dim rtnResut As Boolean = Me._V.IsAuthorityChk(LMB010C.EventShubetsu.KANRYO)

        'チェックリスト格納変数
        Dim arr As ArrayList = Nothing
        If rtnResut = True Then

            'チェックリスト取得
            arr = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB010C.SprColumnIndex.DEF)

            '未選択チェック
            rtnResut = rtnResut AndAlso Me._LMBconV.IsSelectChk(arr.Count)

            'エラーの場合、終了
            If rtnResut = False Then

                '終了アクション
                Call Me._LMBconH.EndAction(frm)
                Exit Sub

            End If

            '項目チェック
            If Me._V.IsKanryoSingleCheck(arr, Me._G) = False Then
                Call Me._LMBconH.EndAction(frm) '終了処理
                Exit Sub
            End If

        End If

        'パラメータのインスタンス生成
        Dim prm As LMFormData = New LMFormData()

        prm.ParamDataSet = Me.SetDataSetLMR010InData(frm, arr)

        '完了取込画面に遷移
        LMFormNavigate.NextFormNavigate(Me, "LMR010", prm)

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
        Else
            MyBase.ShowMessage(frm, "G007")
        End If

        '終了アクション
        Call Me._LMBconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMB010F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMB010C.EventShubetsu.MASTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB010C.EventShubetsu.MASTER)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        'START YANAI 要望番号481
        'Call Me.ShowPopupControl(frm, objNm)
        Call Me.ShowPopupControl(frm, objNm, LMB010C.EventShubetsu.MASTER)
        'END YANAI 要望番号481

        MyBase.ShowMessage(frm, "G007")

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMB010C.EventShubetsu.MASTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMB010C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        'START YANAI 要望番号481
        'Call Me.ShowPopupControl(frm, objNm)
        Call Me.ShowPopupControl(frm, objNm, LMB010C.EventShubetsu.ENTER)
        'END YANAI 要望番号481

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub

    'START YANAI 要望番号481
    '''' <summary>
    '''' ポップアップ起動コントロール
    '''' </summary>
    '''' <param name="frm">フォーム</param>
    '''' <param name="objNm">フォーカスコントロール名</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function ShowPopupControl(ByVal frm As LMB010F, ByVal objNm As String) As Boolean
    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMB010F, ByVal objNm As String, ByVal eventShubetsu As LMB010C.EventShubetsu) As Boolean
        'END YANAI 要望番号481

        '終了アクション
        Call Me._LMBconH.StartAction(frm)

        Select Case objNm
            Case frm.txtCustCD.Name
                'START YANAI 要望番号481
                'Call Me.ShowCustPopup(frm, objNm, New LMFormData())
                Call Me.ShowCustPopup(frm, objNm, New LMFormData(), eventShubetsu)
                'END YANAI 要望番号481

        End Select

        '終了アクション
        Call Me._LMBconH.EndAction(frm)

        MyBase.ShowMessage(frm, "G007")

        Return True

    End Function

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As LMB010F, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMB010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        If rowNo > 0 Then

            '処理開始アクション
            Call Me._LMBconH.StartAction(frm)

            '権限チェック
            If Me._V.IsAuthorityChk(LMB010C.EventShubetsu.DOUBLE_CLICK) = False Then
                Call Me._LMBconH.EndAction(frm)
                Exit Sub
            End If

            'ダブルクリックの場合、検索行をクリックしたのがどうかをチェックする
            If Me.DoubleClickChk(frm) = False Then
                Call Me._LMBconH.EndAction(frm)
                Exit Sub
            End If

            With frm.sprDetail.ActiveSheet

                'inputDataSet作成
                Dim prmDs As DataSet = Me.SetDataSetLMB020InData(frm, rowNo)
                Dim prm As LMFormData = New LMFormData()
                prm.ParamDataSet = prmDs
                prm.RecStatus = RecordStatus.NOMAL_REC

                '画面遷移
                LMFormNavigate.NextFormNavigate(Me, "LMB020", prm)

            End With

            '処理終了アクション
            Call Me._LMBconH.EndAction(frm)

        End If

    End Sub

#End Region '外部メソッド

#Region "内部メソッド"

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMB010F)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

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
        Dim rtDs As DataSet = New LMB010DS()
        Call Me.SetDataSetInData(frm, rtDs)

        ''SPREAD(表示行)初期化
        'frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        'DBリードオンリー設定 ADD 2021/11/01
        Dim rtnDs As DataSet = Me._LMBconH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMB010BLF", "SelectListData", rtDs _
                                                         , lc, mc _
                                                            , "1")

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMB010F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMB010C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '検索条件の荷主名クリア
        Call Me._G.ClearCustNM()

        Dim max As Integer = dt.Rows.Count

        If 0 < max Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {max.ToString()})

        End If

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DoubleClickChk(ByVal frm As LMB010F) As Boolean

        'クリックした行が検索行の場合
        If frm.sprDetail.Sheets(0).ActiveRow.Index() = 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 完了(F5)処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function KanryoChk(ByVal frm As LMB010F) As Boolean

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()

        'チェックリスト取得
        list = Me._LMBconH.GetCheckList(frm.sprDetail.ActiveSheet, LMB010C.SprColumnIndex.DEF)

        '未選択チェック
        If Me._LMBconV.IsSelectChk(list.Count()) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 完了画面のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMR010InData(ByVal frm As LMB010F, ByVal arr As ArrayList) As DataSet

        Dim ds As DataSet = New LMR010DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMR010C_TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
                .Item("INOUTKA_NO_L") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
                .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    'START YANAI メモ②No.28
    ''' <summary>
    ''' EDIデータ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectEdiData(ByVal frm As LMB010F, ByVal arr As ArrayList) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMB010DS()
        rtDs = Me.SetDataSetEdiInData(frm, rtDs, arr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListEdiData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMB010BLF", "SelectListEdiData", rtDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListEdiData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' EDIデータ検索処理のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetEdiInData(ByVal frm As LMB010F, ByVal ds As DataSet, ByVal arr As ArrayList) As DataSet

        Dim dt As DataTable = ds.Tables(LMB010C.TABLE_NM_IN_CHK)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
                .Item("INKA_NO_L") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
                .Item("OUTKA_FROM_ORD_NO_L") = Me._LMBconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo))

            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JikkoShori(ByVal frm As LMB010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1

        Dim sysData As String() = MyBase.GetSystemDateTime()
        Dim systemData As String = sysData(0)
        Dim systemTime As String = sysData(1)
        Dim inkaNoL As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim fileNm As String = String.Empty
        Dim plantCd As String = String.Empty
        Dim opeIn As String = "OPE_IN_"
        Dim manual As String = "_MANUAL"
        Dim underBar As String = "_"
        'START YANAI 20120319 EDI対応
        'Dim custCdL_00295 As String = "00295"
        'Dim custCdL_00331 As String = "00331"
        'Dim custCdM_00 As String = "00"
        'Dim custCdM_02 As String = "02"
        'Dim plantCd_WL49 As String = "WL49"
        'Dim plantCd_JP74 As String = "JP74"
        'Dim plantCd_WW34 As String = "WW34"
        'END YANAI 20120319 EDI対応
        Dim recNo As String = "00001"
        Dim recordStatus As String = "MANUAL"
        'START YANAI 20120319 EDI対応
        Dim nrsBrCd As String = String.Empty
        Dim custCdS As String = String.Empty
        Dim kbnDr() As DataRow = Nothing
        'END YANAI 20120319 EDI対応

        'IN情報を設定
        For i As Integer = 0 To max

            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            custCdL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_L.ColNo))
            custCdM = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_M.ColNo))
            'START YANAI 20120319 EDI対応
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
            custCdS = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_S.ColNo))
            'END YANAI 20120319 EDI対応


            dr = ds.Tables(LMB010C.TABLE_NM_IN_EDI).NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM

                '受信ファイル名作成
                ''START YANAI 20120319 EDI対応
                'If (custCdL_00295).Equals(custCdL) = True Then
                '    plantCd = plantCd_WL49
                'ElseIf (custCdL_00331).Equals(custCdL) = True Then
                '    If (custCdM_00).Equals(custCdM) = True Then
                '        plantCd = plantCd_JP74
                '    ElseIf (custCdM_02).Equals(custCdM) = True Then
                '        plantCd = plantCd_WW34
                '    End If
                'End If
                plantCd = String.Empty
                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'D005' AND ", _
                                                                                               "KBN_NM4 = '", nrsBrCd, "' AND ", _
                                                                                               "KBN_NM5 = '", custCdL, "' AND ", _
                                                                                               "KBN_NM6 = '", custCdM, "' AND ", _
                                                                                               "KBN_NM7 = '", custCdS, "'" _
                                                                                               ))
                If 0 < kbnDr.Length Then
                    plantCd = kbnDr(0).Item("KBN_NM1").ToString
                End If
                'END YANAI 20120319 EDI対応
                fileNm = String.Concat(opeIn, _
                                       plantCd, _
                                       manual, _
                                       inkaNoL, _
                                       underBar, _
                                       Mid(systemTime, 1, 2) _
                                       )
                .Item("FILE_NAME") = fileNm
                .Item("REC_NO") = recNo
                .Item("PLANT_CD") = plantCd
                .Item("RECORD_STATUS") = recordStatus
                .Item("SYS_ENT_DATE") = systemData
                .Item("SYS_ENT_TIME") = systemTime

            End With
            ds.Tables(LMB010C.TABLE_NM_IN_EDI).Rows.Add(dr)

        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "InsertDataEDI", ds)

        If ds Is Nothing Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '20151020 tsunehira add
        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G061")
        'MyBase.ShowMessage(frm, "G002", New String() {"EDI入荷データ作成", String.Empty})
        
    End Sub

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 作業一括作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MakeSagyoShori(ByVal frm As LMB010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaNoL As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim nrsBrCd As String = String.Empty

        'IN情報を設定
        For i As Integer = 0 To max

            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            custCdL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_L.ColNo))
            custCdM = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_M.ColNo))
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))

            dr = ds.Tables(LMB010C.TABLE_NM_IN_SAGYO).NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
                .Item("ROW_NO") = arr(i)

            End With
            ds.Tables(LMB010C.TABLE_NM_IN_SAGYO).Rows.Add(dr)

        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "InsertSagyo", ds)

        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '20151020 tsunehira add
        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G063")
        'MyBase.ShowMessage(frm, "G002", New String() {"作業データ一括作成", String.Empty})
    End Sub

    ''' <summary>
    ''' 作業一括削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DelSagyoShori(ByVal frm As LMB010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaNoL As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim nrsBrCd As String = String.Empty

        'IN情報を設定
        For i As Integer = 0 To max

            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            custCdL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_L.ColNo))
            custCdM = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_M.ColNo))
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))

            dr = ds.Tables(LMB010C.TABLE_NM_IN_SAGYO).NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
                .Item("ROW_NO") = arr(i)

            End With
            ds.Tables(LMB010C.TABLE_NM_IN_SAGYO).Rows.Add(dr)

        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "DeleteSagyo", ds)

        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '20151020 tsunehira add
        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G064")
        'MyBase.ShowMessage(frm, "G002", New String() {"作業データ一括削除", String.Empty})
    End Sub
    'END YANAI 20120121 作業一括処理対応

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 出荷データ作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MakeOutkaShori(ByVal frm As LMB010F, ByVal arr As ArrayList, Optional ByVal jikkoFlg As String = "0")

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaNoL As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim nrsBrCd As String = String.Empty
        Dim outkaDate As String = MyBase.GetSystemDateTime(0)
        Dim sUser As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LM.Base.LMUserInfoManager.GetUserID().ToString(), "'"))
        If 0 < sUser.Length Then
            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(outkaDate), "0000/00/00"))
            Select Case sUser(0).Item("OUTKA_DATE_INIT").ToString
                Case LMControlC.LMC020C_OUTKA_DATE_INIT_01
                    outkaDate = DateAdd("d", -1, tmpdate).ToString("yyyyMMdd")
                Case LMControlC.LMC020C_OUTKA_DATE_INIT_02

                Case LMControlC.LMC020C_OUTKA_DATE_INIT_03
                    outkaDate = DateAdd("d", 1, tmpdate).ToString("yyyyMMdd")
            End Select
        End If

        'IN情報を設定
        For i As Integer = 0 To max
            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            custCdL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_L.ColNo))
            custCdM = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_M.ColNo))
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
            dr = ds.Tables(LMB010C.TABLE_NM_IN_OUTKA).NewRow()
            With dr
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
                .Item("OUTKA_DATE_INIT") = outkaDate
                .Item("ROW_NO") = arr(i)
                '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 Start
                .Item("JIKKO_FLG") = jikkoFlg
                '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 End
                'UTI追加修正 yamanaka 2012.12.21 Start
                .Item("OLD_INKA_STATE_KB") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_STATE_KB.ColNo))
                .Item("INKA_STATE_KB") = LMB010C.INKASTATEKB_90
                .Item("SYS_UPD_DATE") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                .Item("SYS_UPD_TIME") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_TIME.ColNo))
                'UTI追加修正 yamanaka 2012.12.21 End

            End With
            ds.Tables(LMB010C.TABLE_NM_IN_OUTKA).Rows.Add(dr)
        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "MakeOutkaData", ds)

        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        '20151020 tsunehira add
        MyBase.ShowMessage(frm, "G065")
        'MyBase.ShowMessage(frm, "G002", New String() {"出荷データ作成", String.Empty})

    End Sub
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'START 要望番号1784 s.kobayashi 
    ''' <summary>
    ''' 入荷報告取消処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub InkaHokokuCancel(ByVal frm As LMB010F, ByVal arr As ArrayList, Optional ByVal jikkoFlg As String = "0")

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaNoL As String = String.Empty
        Dim nrsBrCd As String = String.Empty

        'IN情報を設定
        For i As Integer = 0 To max
            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
            dr = ds.Tables(LMB010C.TABLE_NM_IN_INKAL).NewRow()
            With dr
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("ROW_NO") = arr(i)
                .Item("INKA_STATE_KB") = LMB010C.INKASTATEKB_50
                .Item("SYS_UPD_DATE") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                .Item("SYS_UPD_TIME") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_TIME.ColNo))

            End With
            ds.Tables(LMB010C.TABLE_NM_IN_INKAL).Rows.Add(dr)
        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "InkaHokokuCancel", ds)

        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        '20151020 tsunehira add
        MyBase.ShowMessage(frm, "G068")
        'MyBase.ShowMessage(frm, "G035", New String() {"UTI報告済取消", String.Empty})

    End Sub
    'END 要望番号1784 s.kobayashi 

    'WIT対応 入荷データ一括取込対応 kasama Start
    ''' <summary>
    ''' 入荷データ一括取込処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub InkaIkkatuTorikomiShori(ByVal frm As LMB010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaNoL As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty
        Dim nrsBrCd As String = String.Empty

        Dim whereStr As String = String.Empty
        Dim custdtlDr As DataRow()

        'IN情報を設定
        For i As Integer = 0 To max

            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            custCdL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_L.ColNo))
            custCdM = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_M.ColNo))
            sysUpdDate = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
            sysUpdTime = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_TIME.ColNo))
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))

            dr = ds.Tables(LMB010C.TABLE_NM_IN_INKAL).NewRow()
            With dr

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("INKA_STATE_KB") = LMB010C.INKASTATEKB_40
                .Item("CUST_CD_L") = custCdL
                .Item("CUST_CD_M") = custCdM
                .Item("SYS_UPD_DATE") = sysUpdDate
                .Item("SYS_UPD_TIME") = sysUpdTime
                .Item("ROW_NO") = arr(i)

                '2014.06.06 FFEM対応 追加START
                whereStr = "NRS_BR_CD = '"
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'whereStr = whereStr & LMUserInfoManager.GetNrsBrCd() & "'"
                whereStr = whereStr & nrsBrCd & "'"
                whereStr = whereStr & " AND CUST_CD = '" & String.Concat(custCdL, custCdM) & "'"

                custdtlDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(whereStr)
                If 0 < custdtlDr.Length Then
                    .Item("SET_NAIYO") = custdtlDr(0).Item("SET_NAIYO").ToString
                Else
                    .Item("SET_NAIYO") = "00"
                End If
                '2014.06.06 FFEM対応 追加END

            End With
            ds.Tables(LMB010C.TABLE_NM_IN_INKAL).Rows.Add(dr)

        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "InkaIkkatuTorikomi", ds)

        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        '20151020 tsunehira add
        MyBase.ShowMessage(frm, "G066")
        'MyBase.ShowMessage(frm, "G002", New String() {"入荷データ一括取込", String.Empty})

    End Sub
    'WIT対応 入荷データ一括取込対応 kasama End

    'CALT連携対応 入荷予定データ作成対応 Ri Start
    ''' <summary>
    ''' 入荷予定データ作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub InkaYoteiiSakuseiShori(ByVal frm As LMB010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaSts As String = String.Empty
        Dim inkaNoL As String = String.Empty
        Dim inkaStsKbn As String = String.Empty
        Dim inkaDate As String = String.Empty
        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty
        Dim nrsBrCd As String = String.Empty

        'IN情報を設定
        For i As Integer = 0 To max

            inkaSts = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_STATE_KB.ColNo))

            If inkaSts.Equals(LMB010C.INKASTATEKB_10) = False Then

                '事務所コード
                'INFOMMANAGERより取得

                '入荷管理番号
                inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
                '入荷進捗区分
                inkaStsKbn = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_STATE_KB.ColNo))
                '入荷日
                inkaDate = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_DATE.ColNo))
                '更新日
                sysUpdDate = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                '更新時
                sysUpdTime = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_TIME.ColNo))
                '営業所コード
                nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))

                dr = ds.Tables(LMB010C.TABLE_NM_IN_INKA_PLAN_SEND).NewRow()
                With dr

                    '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                    '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                    .Item("NRS_BR_CD") = nrsBrCd
                    .Item("INKA_NO_L") = inkaNoL
                    .Item("INKA_STATE_KB") = inkaStsKbn
                    .Item("INKA_DATE") = inkaDate
                    .Item("ROW_NO") = arr(i).ToString()
                    .Item("SYS_UPD_DATE") = sysUpdDate
                    .Item("SYS_UPD_TIME") = sysUpdTime

                End With
                ds.Tables(LMB010C.TABLE_NM_IN_INKA_PLAN_SEND).Rows.Add(dr)

            Else
                
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.SetMessageStore("00", "E785", New String() {arr(i).ToString()})
                'MyBase.SetMessageStore("00", "E454", New String() {"予定入力済", "入荷予定データ作成"}, arr(i).ToString())
                '2015.10.29 tusnehira add End
            End If

        Next

        '実行時WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "InkaYoteiiSakusei", ds)

        'エラーはストレージ形式になるかも(件数が膨大ならいちいち止めるのはよくない？)
        If MyBase.IsMessageStoreExist() = True Then
            '処理終了メッセージの表示
            Me.ShowStorePrintData(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        '20151020 tsunehira add
        MyBase.ShowMessage(frm, "G067")
        'MyBase.ShowMessage(frm, "G002", New String() {"入荷予定データ作成", String.Empty})

    End Sub
    'CALT連携対応 入荷予定データ作成対応 Ri End

    '顧客WEB入荷登録バッチ対応 t.kido Start
    ''' <summary>
    ''' 顧客WEB入荷登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub WebInkaTorokuShori(ByVal frm As LMB010F)

        Dim process As System.Diagnostics.Process = Nothing
        Dim exePath As String = String.Empty
        Dim arguments As String = String.Empty
        Dim kbnDr() As DataRow = Nothing

        Try
            '区分マスタから入荷バッチ実行EXEのパスを取得
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S074' AND ", _
                                                                                           "KBN_CD = '09'"))
            If 0 < kbnDr.Length Then
                exePath = kbnDr(0).Item("KBN_NM2").ToString
            End If

            '引数の設定　営業所コード、倉庫コード、荷主コード
            'arguments = String.Format("{0} {1} {2} {3}", "LBB100", frm.cmbEigyo.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), frm.txtCustCD.TextValue())
            arguments = String.Format("{0} {1} {2} {3} {4}", "LBB100", frm.cmbEigyo.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), frm.txtCustCD.TextValue(), LMUserInfoManager.GetUserID())

            '入荷バッチアプリケーションの起動
            process = System.Diagnostics.Process.Start(exePath, arguments)

            '終了するまで待機する
            process.WaitForExit()

            '終了コードを取得する
            Dim ExitCode As Integer = process.ExitCode

            'プロセスの破棄
            process.Close()
            process.Dispose()

            '処理終了メッセージの表示
            Select Case ExitCode
                Case 0
                    '正常終了
                    MyBase.ShowMessage(frm, "G093")
                Case 1
                    '正常終了(該当データなし)
                    MyBase.ShowMessage(frm, "G001")
                Case 4
                    'エラーデータあり
                    MyBase.ShowMessage(frm, "E235")
                Case 9
                    '予期せぬエラー
                    MyBase.ShowMessage(frm, "S002")
            End Select
        Catch ex As Exception

            MyBase.Logger.WriteErrorLog(MyBase.GetType.Name _
                          , Reflection.MethodBase.GetCurrentMethod().Name _
                          , ex.Message _
                          , ex)

            '予期せぬエラー
            MyBase.ShowMessage(frm, "S002")
        End Try
    End Sub
    '顧客WEB入荷登録バッチ対応 t.kido End

    ''' <summary>
    ''' 現場作業指示処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub WHSagyoSiji(ByVal frm As LMB010F, ByVal arr As ArrayList)

        'パラメータのインスタンス生成
        Dim ds As DataSet = New LMB810DS()
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim inkaSts As String = String.Empty
        Dim inkaNoL As String = String.Empty
        Dim inkaStsKbn As String = String.Empty
        Dim inkaDate As String = String.Empty
        Dim sysUpdDate As String = String.Empty
        Dim sysUpdTime As String = String.Empty
        Dim nrsBrCd As String = String.Empty
        Dim whTabWorkStatusKb As String = String.Empty

        If arr.Count = 0 Then
            MyBase.ShowMessage(frm, "E009")
            Call Me._LMBconH.EndAction(frm)
            Exit Sub
        End If

        'IN情報を設定
        For i As Integer = 0 To max

            '入荷管理番号
            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))
            '入荷進捗区分
            inkaStsKbn = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_STATE_KB.ColNo))
            '更新日
            sysUpdDate = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
            '更新時
            sysUpdTime = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_TIME.ColNo))
            '営業所コード
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
            '現場進捗区分
            whTabWorkStatusKb = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.WH_TAB_WORK_STATUS_KB.ColNo))

            dr = ds.Tables("LMB810IN").NewRow()
            With dr

                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("INKA_STATE_KB") = inkaStsKbn
                .Item("WH_TAB_WORK_STATUS_KB") = whTabWorkStatusKb
                .Item("ROW_NO") = arr(i).ToString()
                .Item("SYS_UPD_DATE") = sysUpdDate
                .Item("SYS_UPD_TIME") = sysUpdTime
                .Item("WH_TAB_STATUS_KB") = LMB810C.WH_TAB_SIJI_STATUS.INSTRUCTED
                .Item("PROC_TYPE") = LMB810C.PROC_TYPE.INSTRUCT

            End With
            ds.Tables("LMB810IN").Rows.Add(dr)

        Next

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'パラメータ設定
        prm.ReturnFlg = False


        prm.ParamDataSet = ds
        LMFormNavigate.NextFormNavigate(Me, "LMB810", prm)


        'エラーはストレージ形式になるかも(件数が膨大ならいちいち止めるのはよくない？)
        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm)
        Else
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})
        End If

    End Sub

    ''' <summary>
    ''' X-Track入荷登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub XTrackInkaTorokuShori(ByVal frm As LMB010F)

        Dim process As System.Diagnostics.Process = Nothing
        Dim exePath As String = String.Empty
        Dim arguments As String = String.Empty
        Dim kbnDr() As DataRow = Nothing

        Try
            '区分マスタから入荷バッチ実行EXEのパスを取得
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S074' AND ",
                                                                                           "KBN_CD = '11'"))
            If 0 < kbnDr.Length Then
                exePath = kbnDr(0).Item("KBN_NM2").ToString
            End If

            '引数の設定　営業所コード、倉庫コード、荷主コード
            arguments = String.Format("{0} {1} {2} {3} {4}", "LBB200", frm.cmbEigyo.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), frm.txtCustCD.TextValue(), LMUserInfoManager.GetUserID())

            '入荷バッチアプリケーションの起動
            process = System.Diagnostics.Process.Start(exePath, arguments)

            '終了するまで待機する
            process.WaitForExit()

            '終了コードを取得する
            Dim ExitCode As Integer = process.ExitCode

            'プロセスの破棄
            process.Close()
            process.Dispose()

            '処理終了メッセージの表示
            Select Case ExitCode
                Case 0
                    '正常終了
                    MyBase.ShowMessage(frm, "G002", New String() {"X-Track入荷データの入荷登録", String.Empty})
                Case 1
                    '正常終了(該当データなし)
                    MyBase.ShowMessage(frm, "G001")
                Case 4
                    'エラーデータあり
                    MyBase.ShowMessage(frm, "E235")
                Case 9
                    '予期せぬエラー
                    MyBase.ShowMessage(frm, "S002")
            End Select

        Catch ex As Exception
            MyBase.Logger.WriteErrorLog(MyBase.GetType.Name _
                          , Reflection.MethodBase.GetCurrentMethod().Name _
                          , ex.Message _
                          , ex)

            '予期せぬエラー
            MyBase.ShowMessage(frm, "S002")
        End Try

    End Sub

    ''' <summary>
    ''' RFIDラベルデータ作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="arr"></param>
    ''' <param name="jikkoFlg"></param>
    Private Sub MakeRfidLavelData(ByVal frm As LMB010F, ByVal arr As ArrayList, Optional ByVal jikkoFlg As String = "0")

        Dim excelExtension As String = ".xls"
        Dim excelFileFormat As Excel.XlFileFormat = Excel.XlFileFormat.xlWorkbookNormal
        'Dim excelExtension As String = ".xlsx"
        'Dim excelFileFormat As Excel.XlFileFormat = Excel.XlFileFormat.xlWorkbookDefault
        Dim excelSheetName As String = "Export Data"

        Dim ds As DataSet = New LMB010DS()
        Dim drIn As DataRow = Nothing

        Dim max As Integer = arr.Count - 1
        Dim inkaNoL As String = ""
        Dim nrsBrCd As String = ""

        For i As Integer = 0 To max
            ' 営業所コード
            nrsBrCd = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
            ' 入荷管理番号
            inkaNoL = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))

            drIn = ds.Tables(LMB010C.TABLE_NM_IN_PRINT_RFID).NewRow()
            With drIn
                .Item("NRS_BR_CD") = nrsBrCd
                .Item("INKA_NO_L") = inkaNoL
                .Item("ROW_NO") = arr(i).ToString()
            End With

            ds.Tables(LMB010C.TABLE_NM_IN_PRINT_RFID).Rows.Add(drIn)
        Next

        ' WSAクラス呼び出し
        ds = MyBase.CallWSA("LMB010BLF", "SelectRfidLavelData", ds)

        If MyBase.IsMessageStoreExist() Then
            If ds.Tables(LMB010C.TABLE_NM_OUT_PRINT_RFID).Rows.Count() = 0 Then
                ' メッセージ蓄積あり、かつ対象データ 0件の場合、
                ' エラーありのメッセージの表示および蓄積内容の Excel表示を行い、以降の処理は行わない。
                Me.ShowStorePrintData(frm)
                Return
            End If
        End If


        Dim sysData As String() = MyBase.GetSystemDateTime()
        Dim filePath(2) As String
        Dim fileName(2) As String
        Dim fileAddFlg As Boolean = False

        ' 保存先のファイルのパス・ファイル名
        Dim kbnDr() As DataRow =
            MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(
                String.Concat("KBN_GROUP_CD = 'E052' AND", "(KBN_CD = '01' OR KBN_CD = '02' OR KBN_CD = '03')"))
        If kbnDr.Length() < 3 Then
            MyBase.ShowMessage(frm, "E326", New String() {"RFIDラベル印刷データの保存先とファイル名", "区分マスタ(E052)"})
            Return
        End If
        For i As Integer = 0 To 2
            filePath(i) = kbnDr(i).Item("KBN_NM1").ToString()
            fileName(i) = String.Concat(kbnDr(i).Item("KBN_NM2").ToString(), "_", sysData(0), "_", sysData(1), excelExtension)
            If System.IO.File.Exists(String.Concat(filePath(i), fileName(i))) Then
                ' ミリ秒まで含むファイル名のため重複は考えにくいが万一存在の場合は削除する。
                System.IO.File.Delete(String.Concat(filePath(i), fileName(i)))
            End If
        Next

        Dim inkaNoLSet As New HashSet(Of String)

        Dim drExcel As DataRow()
        Dim lvl1Ut() As String = New String() {"DRU", "BOX", "CY, PAL"}
        Dim excelData(0, 0) As String

        Dim rowMax As Integer
        Dim colMax As Integer

        For i As Integer = 0 To 2
            Dim where As New Text.StringBuilder()
            Dim utArr As String() = lvl1Ut(i).Split(","c)
            For Each ut As String In utArr
                where.Append(String.Concat(If(where.Length() = 0, "", " OR "), "LVL1_UT = '", ut.Trim(), "'"))
            Next
            drExcel = ds.Tables(LMB010C.TABLE_NM_OUT_PRINT_RFID).Select(where.ToString(), "INKA_NO_L, INKA_NO_M")
            If drExcel.Count() = 0 Then
                Continue For
            End If

            ' 二次元配列の行側要素数(の元となる貼付先 Excel 行数)導出
            ' →ヘッダ分 1行 + DataSet項目「TSMCシステム個数」の値の合計
            ' および対象データのあった入荷管理番号(大) の抽出
            rowMax = 1
            For j = 0 To drExcel.Count() - 1
                rowMax += Convert.ToInt32(drExcel(j).Item("TSMC_QTY").ToString())

                If Not inkaNoLSet.Contains(drExcel(j).Item("INKA_NO_L").ToString()) Then
                    inkaNoLSet.Add(drExcel(j).Item("INKA_NO_L").ToString())
                End If
            Next

            ' 二次元配列の列側要素数(の元となる貼付先 Excel 列数)決定およびヘッダ文字列の格納
            Select Case lvl1Ut(i)
                Case "DRU"
                    colMax = 13
                    ReDim excelData(rowMax - 1, colMax - 1)
                    excelData(0, 0) = "Material No."
                    excelData(0, 1) = "Expiration Date"
                    excelData(0, 2) = "Company ID"
                    excelData(0, 3) = "Raw Lot ID"
                    excelData(0, 4) = "Sequence ID"
                    excelData(0, 5) = "Vendor Material No."
                    excelData(0, 6) = "Hazardous"
                    excelData(0, 7) = "Ship To"
                    excelData(0, 8) = "3rd layer PPN"
                    excelData(0, 9) = "Packaging ID."
                    excelData(0, 10) = "Vender Code"
                    excelData(0, 11) = "DUNS No"
                    excelData(0, 12) = "Sample"

                Case "BOX"
                    colMax = 14
                    ReDim excelData(rowMax - 1, colMax - 1)
                    excelData(0, 0) = "Material No."
                    excelData(0, 1) = "Expiration Date"
                    excelData(0, 2) = "Company ID"
                    excelData(0, 3) = "Raw Lot ID"
                    excelData(0, 4) = "Hazardous"
                    excelData(0, 5) = "Serial No."
                    excelData(0, 6) = "2nd layer PPN"
                    excelData(0, 7) = "Box Serial No."
                    excelData(0, 8) = "Ship To"
                    excelData(0, 9) = "3rd layer PPN"
                    excelData(0, 10) = "Packaging ID."
                    excelData(0, 11) = "Vender Code"
                    excelData(0, 12) = "DUNS No"
                    excelData(0, 13) = "Sample"

                Case "CY, PAL"
                    colMax = 12
                    ReDim excelData(rowMax - 1, colMax - 1)
                    excelData(0, 0) = "Material No."
                    excelData(0, 1) = "Expiration Date"
                    excelData(0, 2) = "Company ID"
                    excelData(0, 3) = "Cylinder No."
                    excelData(0, 4) = "Lot ID"
                    excelData(0, 5) = "Vendor Material No."
                    excelData(0, 6) = "Hazardous"
                    excelData(0, 7) = "Ship To"
                    excelData(0, 8) = "Packaging ID."
                    excelData(0, 9) = "Vender Code"
                    excelData(0, 10) = "DUNS No"
                    excelData(0, 11) = "Sample"

            End Select

            Dim sequenceId As Integer = 0
            Dim serialNo As Integer = 0
            Dim boxSerialNo As Integer = 0
            Dim packagingIdSeq As Integer = 0

            ' 二次元配列への DataSet値の格納
            Dim rowIdx As Integer = 0
            For j = 0 To drExcel.Count() - 1
                For k = 1 To Convert.ToInt32(drExcel(j).Item("TSMC_QTY").ToString())
                    ' 1 DataRow につき、DataSet項目「TSMCシステム個数」分繰り返す。
                    rowIdx += 1
                    Select Case lvl1Ut(i)
                        Case "DRU"
                            sequenceId += 1
                            excelData(rowIdx, 0) = drExcel(j).Item("MATERIAL_NO").ToString()
                            excelData(rowIdx, 1) = drExcel(j).Item("EXPIRATION_DATE").ToString()
                            excelData(rowIdx, 2) = drExcel(j).Item("COMPANY_ID").ToString()
                            excelData(rowIdx, 3) = drExcel(j).Item("RAW_LOT_ID").ToString()
                            excelData(rowIdx, 4) = sequenceId.ToString().PadLeft(5, "0"c)
                            excelData(rowIdx, 5) = drExcel(j).Item("VENDOR_MATERIAL_NO").ToString()
                            excelData(rowIdx, 6) = drExcel(j).Item("HAZARDOUS").ToString()
                            excelData(rowIdx, 7) = drExcel(j).Item("SHIP_TO").ToString()
                            excelData(rowIdx, 8) = drExcel(j).Item("THIRD_LAYER_PPN").ToString()
                            excelData(rowIdx, 9) = drExcel(j).Item("PACKAGING_ID").ToString()
                            excelData(rowIdx, 10) = drExcel(j).Item("VENDER_CODE").ToString()
                            excelData(rowIdx, 11) = drExcel(j).Item("DUNS_NO").ToString()
                            excelData(rowIdx, 12) = drExcel(j).Item("SAMPLE").ToString()

                        Case "BOX"
                            serialNo += 1
                            boxSerialNo += 1
                            If boxSerialNo > 99999 Then
                                boxSerialNo = 1
                            End If
                            excelData(rowIdx, 0) = drExcel(j).Item("MATERIAL_NO").ToString()
                            excelData(rowIdx, 1) = drExcel(j).Item("EXPIRATION_DATE").ToString()
                            excelData(rowIdx, 2) = drExcel(j).Item("COMPANY_ID").ToString()
                            excelData(rowIdx, 3) = drExcel(j).Item("RAW_LOT_ID").ToString()
                            excelData(rowIdx, 4) = drExcel(j).Item("HAZARDOUS").ToString()
                            excelData(rowIdx, 5) = serialNo.ToString().PadLeft(5, "0"c)
                            excelData(rowIdx, 6) = drExcel(j).Item("SECOND_LAYER_PPN").ToString()
                            excelData(rowIdx, 7) = boxSerialNo.ToString().PadLeft(5, "0"c)
                            excelData(rowIdx, 8) = drExcel(j).Item("SHIP_TO").ToString()
                            excelData(rowIdx, 9) = drExcel(j).Item("THIRD_LAYER_PPN").ToString()
                            excelData(rowIdx, 10) = drExcel(j).Item("PACKAGING_ID").ToString()
                            excelData(rowIdx, 11) = drExcel(j).Item("VENDER_CODE").ToString()
                            excelData(rowIdx, 12) = drExcel(j).Item("DUNS_NO").ToString()
                            excelData(rowIdx, 13) = drExcel(j).Item("SAMPLE").ToString()

                        Case "CY, PAL"
                            packagingIdSeq += 1
                            If packagingIdSeq > 999 Then
                                packagingIdSeq = 1
                            End If
                            excelData(rowIdx, 0) = drExcel(j).Item("MATERIAL_NO").ToString()
                            excelData(rowIdx, 1) = drExcel(j).Item("EXPIRATION_DATE").ToString()
                            excelData(rowIdx, 2) = drExcel(j).Item("COMPANY_ID").ToString()
                            excelData(rowIdx, 3) = drExcel(j).Item("CYLINDER_NO").ToString()
                            excelData(rowIdx, 4) = drExcel(j).Item("LOT_ID").ToString()
                            excelData(rowIdx, 5) = drExcel(j).Item("VENDOR_MATERIAL_NO").ToString()
                            excelData(rowIdx, 6) = drExcel(j).Item("HAZARDOUS").ToString()
                            excelData(rowIdx, 7) = drExcel(j).Item("SHIP_TO").ToString()
                            excelData(rowIdx, 8) = String.Concat(drExcel(j).Item("PACKAGING_ID").ToString(), packagingIdSeq.ToString().PadLeft(3, "0"c))
                            excelData(rowIdx, 9) = drExcel(j).Item("VENDER_CODE").ToString()
                            excelData(rowIdx, 10) = drExcel(j).Item("DUNS_NO").ToString()
                            excelData(rowIdx, 11) = drExcel(j).Item("SAMPLE").ToString()

                    End Select
                Next
            Next

            ' EXCEL起動
            Dim xlsApp As New Excel.Application
            Dim xlsBook As Excel.Workbook = Nothing
            Dim xlsSheets As Excel.Sheets = Nothing
            Dim xlsSheet As Excel.Worksheet = Nothing

            Dim startCell As Excel.Range = Nothing
            Dim endCell As Excel.Range = Nothing
            Dim range As Excel.Range = Nothing
            Dim rowCnt As Integer = 0


            ' 新規作成
            xlsBook = xlsApp.Workbooks.Add()
            xlsSheet = DirectCast(xlsBook.Worksheets(1), Excel.Worksheet)
            startCell = DirectCast(xlsSheet.Cells(1, 1), Excel.Range)
            endCell = DirectCast(xlsSheet.Cells(rowMax, colMax), Excel.Range)

            xlsSheet.Name = excelSheetName

            range = xlsSheet.Range(startCell, endCell)
            range.Value = excelData
            range.Columns.AutoFit()

            ' 保存時問合せダイアログの非表示化
            xlsApp.DisplayAlerts = False

            Try
                ' 保存時、上書き確認ポップで「いいえ」「キャンセル」選択時のエラー回避

                ' 新規作成
                System.IO.Directory.CreateDirectory(filePath(i))
                xlsBook.SaveAs(String.Concat(filePath(i), fileName(i)), excelFileFormat)

            Catch ex As Exception

            End Try

            ' 非表示化した保存時問合せダイアログの復元
            xlsApp.DisplayAlerts = True

            xlsSheets = Nothing
            xlsSheet = Nothing
            xlsBook.Close(False)    ' Excelを閉じる
            xlsBook = Nothing
            xlsApp.Quit()
            xlsApp = Nothing

        Next

        If MyBase.IsMessageStoreExist() Then
            ' メッセージ蓄積ありの場合
            ' 蓄積内容の Excel表示および、処理済み件数とエラーの両方ありのメッセージ表示
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E534", New String() {inkaNoLSet.Count().ToString(), frm.cmbJikkou.SelectedText})
        Else
            ' 処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, ""})
        End If

    End Sub









    ''' <summary>
    ''' エラー帳票出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMB010F) As Boolean

        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            Return False

        End If

        Return True

    End Function
    'EMD YANAI メモ②No.28

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintAction(ByVal frm As LMB010F, ByVal arr As ArrayList)

        '印刷処理

        'DataSet設定
        Dim rtnDs As DataSet = New LMB010DS()

        'InDataSetの場合
        rtnDs = Me.SetDataSetPrintInData(frm, arr)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtnDs.Tables(LMB010C.TABLE_NM_IN_INKAL).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintAction")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMB010BLF", "PrintAction", rtnDs)

        End If

        'プレビュー判定 
        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If 0 < prevDt.Rows.Count Then

            'プレビューの生成
            Dim prevFrm As RDViewer = New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            'フォーカス設定
            prevFrm.Focus()

        End If

        '処理終了メッセージの表示
        '20151020 tsunehira add
        MyBase.ShowMessage(frm, "G062")
        'MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
        End If

    End Sub
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

    'START 入荷検索画面、アクサルタGHSラベル印刷対応
    ''' <summary>
    ''' 印刷処理  
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintGHSAction(ByVal frm As LMB010F, ByVal arr As ArrayList)

        '印刷処理

        'DataSet設定
        Dim rtnDs As DataSet = New LMB010DS()

        'InDataSetの場合
        rtnDs = Me.SetDataSetPrintInData(frm, arr)

        'LABE_TYPEを退避
        Dim getLABE_TYPE As String = rtnDs.Tables(LMB010C.TABLE_NM_IN_INKAL).Rows(0).Item("LABEL_TYPE").ToString.Trim

        'サーバに渡すレコードが存在する場合
        If 0 < rtnDs.Tables(LMB010C.TABLE_NM_IN_INKAL).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintGHSAction")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMB010BLF", "PrintGHSAction", rtnDs)

        End If

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'パラメータ設定
        prm.ReturnFlg = False

        'GHSラベルCSV出力処理呼出 
        ' LMB800IN にLABEL_TYPE設定
        Dim dr As DataRow = rtnDs.Tables("LMB800IN").NewRow()
        dr.Item("LABEL_TYPE") = getLABE_TYPE.ToString
        rtnDs.Tables("LMB800IN").Rows.Add(dr)

        prm.ParamDataSet = rtnDs
        LMFormNavigate.NextFormNavigate(Me, "LMB800", prm)

        '未取得データ取得（PDF名、PDF番号）
        Dim outDr As DataRow() = rtnDs.Tables("LMB800OUT").Select("PDF_NO = '' OR PDF_NM = '' OR FOLDER = ''", "INKA_NO")
        Dim max As Integer = outDr.Length - 1

        If max > -1 Then
            Dim strMsg As String = String.Empty

            For i As Integer = 0 To max

                strMsg = String.Concat(Mid(outDr(i).Item("INKA_NO").ToString.Trim, 1, 9), "-", outDr(i).Item("GOODS_CD_CUST").ToString.Trim)

                MyBase.SetMessageStore("00", "E223", New String() {"商品マスタにPDFファイル名またはラベル種類の設定がないため印刷"}, "0", "入荷番号-商品コード", strMsg)

            Next

        End If

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

        Else
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbPrint.SelectedText, String.Empty})

        End If

    End Sub
    'END 入荷検索画面、アクサルタGHSラベル印刷対応

#End Region

#Region "PopUp"

    'START YANAI 要望番号481
    '''' <summary>
    '''' 荷主マスタ参照POP起動
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Private Sub ShowCustPopup(ByVal frm As LMB010F, ByVal objNM As String, ByVal prm As LMFormData)
    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowCustPopup(ByVal frm As LMB010F, ByVal objNM As String, ByVal prm As LMFormData, ByVal eventShubetsu As LMB010C.EventShubetsu)
        'END YANAI 要望番号481

        Select Case objNM

            Case LMB010C.EventShubetsu.SINKI.ToString()         '新規

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow()
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("CUST_CD_L") = frm.txtCustCD.TextValue
                row("HYOJI_KBN") = LMZControlC.HYOJI_S

                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case frm.txtCustCD.Name                                    '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue().ToString()
                'START YANAI 要望番号481
                'row("CUST_CD_L") = frm.txtCustCD.TextValue
                If (LMB010C.EventShubetsu.ENTER).Equals(eventShubetsu) = True Then
                    row("CUST_CD_L") = frm.txtCustCD.TextValue
                End If
                'END YANAI 要望番号481
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg                          '2011/08/17 野島 POPUP表示対応
                row("HYOJI_KBN") = LMZControlC.HYOJI_S

                If String.IsNullOrEmpty(frm.txtCustCD.TextValue) = True Then
                    frm.lblCustNM.TextValue = String.Empty
                End If

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case frm.FunctionKey.F9ButtonName                                  '初期荷主変更

                Dim prmDs As DataSet = New LMZ010DS
                Dim row As DataRow = prmDs.Tables(LMZ010C.TABLE_NM_IN).NewRow()
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ010C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ010", prm)

                '戻り処理
                If prm.ReturnFlg = True Then
                    With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.lblCustNM.TextValue = .Item("CUST_NM_L").ToString    '荷主名
                    End With
                End If

        End Select

        '戻り処理
        If prm.ReturnFlg = True Then
            Select Case objNM

                Case LMB010C.EventShubetsu.SINKI.ToString()
                    '新規

                    'inputDataSet作成
                    Dim prmDs As DataSet = Me.SetDataSetLMB020InData_SINKI(frm, prm.ParamDataSet)
                    prm.ParamDataSet = prmDs
                    prm.RecStatus = RecordStatus.NEW_REC

                    '画面遷移
                    LMFormNavigate.NextFormNavigate(Me, "LMB020", prm)

                Case frm.txtCustCD.Name
                    'マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCD.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                        frm.lblCustNM.TextValue = .Item("CUST_NM_L").ToString    '荷主名
                        'デフォルト倉庫コード設定 yamanaka 2013.02.26 Start
                        frm.cmbSoko.SelectedValue = .Item("DEFAULT_SOKO_CD").ToString
                        'デフォルト倉庫コード設定 yamanaka 2013.02.26 End

                    End With

            End Select

        End If

    End Sub

    ''' <summary>
    ''' 担当者別荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectTCustListDataRow(ByVal userId As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST).Select(Me.SelectTcustString(userId))

    End Function

    ''' <summary>
    ''' 担当者別荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectTcustString(ByVal userId As String) As String

        SelectTcustString = String.Empty

        '削除フラグ
        SelectTcustString = String.Concat(SelectTcustString, " SYS_DEL_FLG = '0' ")

        'ユーザコード
        SelectTcustString = String.Concat(SelectTcustString, " AND ", "USER_CD = ", " '", userId, "' ")

        '初期荷主該当フラグ(ON)
        SelectTcustString = String.Concat(SelectTcustString, " AND ", "DEFAULT_CUST_YN = ", " '", LMFControlC.FLG_ON, "' ")

        Return SelectTcustString

    End Function

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMB010F)

        With frm

            '荷主名称
            If String.IsNullOrEmpty(frm.txtCustCD.TextValue) = False Then
                .lblCustNM.TextValue = GetCachedCust(.cmbEigyo.SelectedValue.ToString, .txtCustCD.TextValue, "00", "00", "00")
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal nrsBrCd As String, _
                                  ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                          "NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("CUST_NM_L").ToString
        End If

        Return String.Empty

    End Function

#End Region 'PopUp

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMB010F, ByVal rtDs As DataSet)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dr As DataRow = rtDs.Tables(LMB010C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("INKA_STATE_KB1") = frm.chkStaYotei.GetBinaryValue.ToString().Replace("0", "")
        dr("INKA_STATE_KB2") = frm.chkStaPrint.GetBinaryValue.ToString().Replace("0", "")
        dr("INKA_STATE_KB3") = frm.chkStaUketsuke.GetBinaryValue.ToString().Replace("0", "")
        dr("INKA_STATE_KB4") = frm.chkStaKenpin.GetBinaryValue.ToString().Replace("0", "")
        dr("INKA_STATE_KB5") = frm.chkStaNyuka.GetBinaryValue.ToString().Replace("0", "")
        dr("INKA_STATE_KB6") = frm.chkStaHoukoku.GetBinaryValue.ToString().Replace("0", "")
        dr("CUST_CD_L") = frm.txtCustCD.TextValue
        dr("UNCHIN_TP1") = frm.chkTranUnso.GetBinaryValue.ToString().Replace("0", "")
        dr("UNCHIN_TP2") = frm.chkTranYoko.GetBinaryValue.ToString().Replace("0", "")
        dr("UNCHIN_TP3") = frm.chkTranALL.GetBinaryValue.ToString().Replace("0", "")
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("WH_CD") = frm.cmbSoko.SelectedValue
        dr("INKA_DATE_FROM") = frm.imdNyukaDate_From.TextValue
        dr("INKA_DATE_TO") = frm.imdNyukaDate_To.TextValue
        dr("INKA_NO_L_DIRECT") = frm.txtInkaNoL.TextValue
        If frm.chkSelectByNrsB.Checked = True Then
            dr("TANTO_USER_FLG") = LMConst.FLG.ON
        Else
            dr("TANTO_USER_FLG") = LMConst.FLG.OFF
        End If
        dr("USER_ID") = LMUserInfoManager.GetUserID
        '2017/09/25 修正 李↓
        dr("LANG_FLG") = lgm.MessageLanguage()
        '2017/09/25 修正 李↑


        '検索条件　入力部（スプレッド）
        With frm.sprDetail.ActiveSheet

            dr("OUTKA_FROM_ORD_NO_L") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo))
            dr("CUST_NM") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.CUST_NM.ColNo))
            'START YANAI 要望番号748
            dr("CUST_CD_S") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.CUST_CD_S.ColNo))
            'END YANAI 要望番号748
            '(2013.01.11)要望番号1700 -- START --
            'dr("GOODS_NM") = Me._LMBconV.GetCellValue(.Cells(0, LMB010G.sprDetailDef.GOODS_NM.ColNo))
            'dr("GOODS_NM") = Me._LMBconV.GetCellValue(.Cells(0, LMB010G.sprDetailDef.GOODS_NM.ColNo)).Replace("%", "[%]").Replace("_", "[_]").Replace("[", "[[]")
            dr("GOODS_NM") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.GOODS_NM.ColNo)).Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]")   '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
            '(2013.01.11)要望番号1700 --  END  --
            dr("REMARK") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.REMARK.ColNo))
            dr("DEST_NM") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.DEST_NM.ColNo))
            dr("UNCHIN_KB") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.UNCHIN_KB.ColNo))
            dr("UNSOCO_NM") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.UNSOCO_NM.ColNo))
            dr("WEB_INKA_NO_L") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.WEB_INKA_NO_L.ColNo))
            dr("INKA_NO_L") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.INKA_NO_L.ColNo))
            dr("BUYER_ORD_NO_L") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.BUYER_ORD_NO_L.ColNo))
            dr("INKA_TP") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.INKA_TP.ColNo))
            dr("INKA_KB") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.INKA_KB.ColNo))
            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            dr("LOT_NO") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.LOT_NO.ColNo))
            dr("SERIAL_NO") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.SERIAL_NO.ColNo))
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            dr("TANTO_USER") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.TANTO_USER.ColNo))
            dr("SYS_ENT_USER") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.SYS_ENT_USER.ColNo))
            dr("SYS_UPD_USER") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.SYS_UPD_USER.ColNo))

            dr("WH_WORK_STATUS") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.WH_WORK_STATUS_NM.ColNo))

            dr("WH_TAB_STATUS") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.WH_TAB_STATUS_NM.ColNo))
            dr("WH_TAB_WORK_STATUS") = Me._LMBconV.GetCellValue(.Cells(0, _G.sprDetailDef.WH_TAB_WORK_STATUS_NM.ColNo))

            '区分マスタ参照項目値判定
            If dr("UNCHIN_KB").ToString().Length() < 2 Then
                dr("UNCHIN_KB") = String.Empty
            End If
            If dr("INKA_TP").ToString().Length() < 2 Then
                dr("INKA_TP") = String.Empty
            End If
            If dr("INKA_KB").ToString().Length() < 2 Then
                dr("INKA_KB") = String.Empty
            End If


        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMB010C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(LMB020引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB020InData(ByVal frm As LMB010F, ByVal rowno As Integer) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMB020DS()
        Dim dr As DataRow = ds.Tables(LMControlC.LMB020C_TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            '要望管理番号 2506 tsunehira add start
            'dr.Item("INKA_NO_L") = Me._LMBconV.GetCellValue(.Cells(rowno, LMB010C.SprColumnIndex.INKA_NO_L)) '管理番号
            'dr.Item("NRS_BR_CD") = Me._LMBconV.GetCellValue(.Cells(rowno, LMB010C.SprColumnIndex.NRS_BR_CD)) '営業所コード
            'dr.Item("CUST_CD_L") = Me._LMBconV.GetCellValue(.Cells(rowno, LMB010C.SprColumnIndex.CUST_CD_L)) '荷主コード（大）
            'dr.Item("CUST_CD_M") = Me._LMBconV.GetCellValue(.Cells(rowno, LMB010C.SprColumnIndex.CUST_CD_M)) '荷主コード（中）
            'dr.Item("CUST_NM") = Me._LMBconV.GetCellValue(.Cells(rowno, LMB010C.SprColumnIndex.CUST_NM))     '荷主名

            dr.Item("INKA_NO_L") = Me._LMBconV.GetCellValue(.Cells(rowno, _G.sprDetailDef.INKA_NO_L.ColNo))
            dr.Item("NRS_BR_CD") = Me._LMBconV.GetCellValue(.Cells(rowno, _G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("CUST_CD_L") = Me._LMBconV.GetCellValue(.Cells(rowno, _G.sprDetailDef.CUST_CD_L.ColNo))
            dr.Item("CUST_CD_M") = Me._LMBconV.GetCellValue(.Cells(rowno, _G.sprDetailDef.CUST_CD_M.ColNo))
            dr.Item("CUST_NM") = Me._LMBconV.GetCellValue(.Cells(rowno, _G.sprDetailDef.CUST_NM.ColNo))
            '要望管理番号 2506 tsunehira add end
        End With

        ds.Tables(LMControlC.LMB020C_TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(LMB020引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMB020InData_SINKI(ByVal frm As LMB010F, ByVal prmDs As DataSet) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMB020DS()
        Dim dr As DataRow = ds.Tables(LMControlC.LMB020C_TABLE_NM_IN).NewRow()

        With prmDs.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue     '営業所コード  
            dr.Item("CUST_CD_L") = .Item("CUST_CD_L").ToString    '荷主コード（大）
            dr.Item("CUST_CD_M") = .Item("CUST_CD_M").ToString    '荷主コード（中）
            dr.Item("CUST_NM") = String.Concat(.Item("CUST_NM_L").ToString, .Item("CUST_NM_M").ToString)    '荷主名
        End With

        ds.Tables(LMControlC.LMB020C_TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' データセット設定(LMB020引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetPrintInData(ByVal frm As LMB010F, ByVal arr As ArrayList) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMB010DS()
        Dim dr As DataRow = ds.Tables(LMB010C.TABLE_NM_IN_INKAL).NewRow()
        Dim max As Integer = arr.Count - 1

        Dim sCnt As Decimal = 0
        Dim inkaStateKb As String = String.Empty
        Dim newInkaStateKb As String = String.Empty
        Dim sokoDr() As DataRow = Nothing

        'IN情報を設定
        For i As Integer = 0 To max

            dr = ds.Tables(LMB010C.TABLE_NM_IN_INKAL).NewRow()
            With dr

                sokoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("WH_CD = '", Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.WH_CD.ColNo)), "'"))

                .Item("NRS_BR_CD") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo))
                .Item("INKA_NO_L") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_NO_L.ColNo))

                inkaStateKb = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.INKA_STATE_KB.ColNo))
                If String.IsNullOrEmpty(inkaStateKb) = True Then
                    inkaStateKb = "00"
                End If
                sCnt = Convert.ToDecimal(Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.RECCNTS.ColNo)))
                If sCnt = 0 Then
                    newInkaStateKb = "10"
                ElseIf ("00").Equals(sokoDr(0).Item("INKA_UKE_PRT_YN").ToString) = False Then
                    newInkaStateKb = "20"
                ElseIf ("00").Equals(sokoDr(0).Item("INKA_KENPIN_YN").ToString) = False Then
                    newInkaStateKb = "30"
                ElseIf ("00").Equals(sokoDr(0).Item("INKA_KENPIN_YN").ToString) = True Then
                    newInkaStateKb = "40"
                End If
                If Convert.ToDecimal(newInkaStateKb) > Convert.ToDecimal(inkaStateKb) Then
                    .Item("INKA_STATE_KB") = newInkaStateKb
                Else
                    .Item("INKA_STATE_KB") = inkaStateKb
                End If
                '入荷報告書の場合はINKA_STATE_KBの更新はしないため、現在の値を設定する
#If False Then  'UPD 2018/11/01 依頼番号 : 002747   【LMS】入荷報告印刷_角印つけるつけないの選択機能

                 If ("01").Equals(frm.cmbPrint.SelectedValue) = True Then
                    .Item("INKA_STATE_KB") = inkaStateKb
                End If

#Else
                '入荷報告書（角印）も追加
                If ("01").Equals(frm.cmbPrint.SelectedValue) = True _
                    Or ("07").Equals(frm.cmbPrint.SelectedValue) = True Then
                    .Item("INKA_STATE_KB") = inkaStateKb
                End If

#End If

                .Item("TANTO_USER") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.PIC.ColNo))
                .Item("SYS_UPD_DATE") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_DATE.ColNo))
                .Item("SYS_UPD_TIME") = Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.SYS_UPD_TIME.ColNo))
                .Item("ROW_NO") = arr(i)
                .Item("PRINT_KB") = frm.cmbPrint.SelectedValue

                '修正開始 2014.12.10
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                                 String.Concat("NRS_BR_CD = '", Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.NRS_BR_CD.ColNo)), _
                                                             "' AND CUST_CD = '", Me._LMBconV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprDetailDef.CUST_CD_L.ColNo)), _
                                                             "' AND SUB_KB = '90'"))

                If 0 < custDetailsDr.Length Then
                    .Item("SET_NAIYO") = custDetailsDr(0).Item("SET_NAIYO").ToString()
                Else
                    .Item("SET_NAIYO") = "00"
                End If
                '修正終了

                'ADD 2017/08/04 GHSラベル対応
                If frm.cmbPrint.SelectedValue.Equals("06") = True AndAlso _
                     frm.cmbLabelTYpe.Visible = True Then
                    .Item("LABEL_TYPE") = Convert.ToInt32(frm.cmbLabelTYpe.SelectedValue.ToString.Trim)
                Else
                    .Item("LABEL_TYPE") = String.Empty
                End If
            End With
            ds.Tables(LMB010C.TABLE_NM_IN_INKAL).Rows.Add(dr)

        Next

        Return ds

    End Function
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

#End Region 'DataSet設定

#End Region '内部メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ShiftInsertStatus")

        '「新規」処理
        Call Me.ActionControl(LMB010C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ShiftInsertStatus")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し(完了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ShiftCompleteStatus")

        '「完了」処理
        Call Me.KanryouDataAction(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "ShiftCompleteStatus")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "FunctionKey10Press")

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(Me.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(初期荷主変更処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        '「初期荷主変更」処理
        Call Me.ActionControl(LMB010C.EventShubetsu.DEF_CUST, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByVal frm As LMB010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "RowSelection")

        '「ダブルクリック」処理
        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームでKEYを押下時、発生するイベントです。
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub LMB010F_KeyDown(ByVal frm As LMB010F, ByVal e As System.Windows.Forms.KeyEventArgs)
        Call Me.EnterAction(frm, e)
    End Sub

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 印刷ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByVal frm As LMB010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnPrint_Click")

        Call Me.ActionControl(LMB010C.EventShubetsu.PRINT, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnPrint_Click")

    End Sub
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

    'ADD GHSラベル対応 ラベル種類
    ''' <summary>
    ''' btnPri Select変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbPrint_SelectedValueChanged(ByVal frm As LMB010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbPrint_SelectedValueChanged")

        If frm.cmbPrint.SelectedValue.Equals("06") = True Then
            'GHSラベル時
            frm.cmbLabelTYpe.Visible = True
            frm.cmbLabelTYpe.SelectedValue = "01"

        Else
            'GHSラベル以外
            frm.cmbLabelTYpe.Visible = False

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbPrint_SelectedValueChanged")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class