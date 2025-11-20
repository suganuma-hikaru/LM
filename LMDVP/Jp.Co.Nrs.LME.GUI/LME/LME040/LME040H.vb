' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME040  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LME040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LME040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LME040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LME040G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEConG As LMEControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEConH As LMEControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMEconV As LMEControlV

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LME040DS

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 閉じる押下時の保存エラーフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _closeErrFlg As Boolean

    'START YANAI 要望番号1090 指摘修正
    ''' <summary>
    ''' 新規・編集フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _editFlg As Boolean
    'END YANAI 要望番号1090 指摘修正

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
        Dim frm As LME040F = New LME040F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMEConG = New LMEControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMEconV = New LMEControlV(Me, sForm)

        'Hnadler共通クラスの設定
        Me._LMEConH = New LMEControlH(DirectCast(frm, Form))

        'Gamenクラスの設定
        Me._G = New LME040G(Me, frm)

        'Validateクラスの設定
        Me._V = New LME040V(Me, frm, Me._LMEconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0), prmDs)

        Dim mode As String = String.Empty
        Dim disp As String = String.Empty

        Me._Ds = Nothing

        If prm.RecStatus = RecordStatus.NEW_REC Then
            '新規モード
            mode = LME040C.MODE_SINKI
            disp = DispMode.EDIT

            'シチュエーションラベルの設定
            Call Me._G.SetSituation(disp, prm.RecStatus)

            Me._Ds = New LME040DS

            'メッセージの表示
            Call MyBase.ShowMessage(frm, "G006")

            'START YANAI 要望番号1090 指摘修正
            '新規・編集フラグの設定
            Me._editFlg = False
            '新規・編集フラグの設定

        Else
            '編集モード
            mode = LME040C.MODE_VIEW
            disp = DispMode.VIEW

            'シチュエーションラベルの設定
            Call Me._G.SetSituation(disp, prm.RecStatus)

            '初期検索
            Call Me.SelectData(frm, prmDs)

            'START YANAI 要望番号1090 指摘修正
            '新規・編集フラグの設定
            Me._editFlg = True
            'END YANAI 要望番号1090 指摘修正

        End If

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(mode)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(mode)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LME040C.EventShubetsu, ByVal frm As LME040F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        Dim disp As String = String.Empty

        '画面色リセット用
        Me._LMEConH.ColorReset(frm)

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LME040C.EventShubetsu.HENSHU    '編集

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSagyoSkyuStatusCheck(_Ds) = False Then
                    Exit Sub
                End If

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus(LME040C.MODE_EDIT)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LME040C.MODE_EDIT)

                disp = DispMode.EDIT

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(disp, RecordStatus.NOMAL_REC)

            Case LME040C.EventShubetsu.FUKUSHA    '複写

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '複写対象外項目のクリア
                Call Me._G.ClearControlFukusha()

                'DataSetのクリア
                Me._Ds = New LME040DS

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus(LME040C.MODE_SINKI)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LME040C.MODE_EDIT)

                disp = DispMode.EDIT

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(disp, RecordStatus.COPY_REC)

                'START YANAI 要望番号1090 指摘修正
                '新規・編集フラグの設定
                Me._editFlg = False
                'END YANAI 要望番号1090 指摘修正

            Case LME040C.EventShubetsu.MASTER, _
                 LME040C.EventShubetsu.MASTERENTER 'マスタ参照

                '参照モード時は終了
                If (DispMode.VIEW).Equals(frm.lblSituation.DispMode) = True Then
                    Exit Sub
                End If

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                'POP表示
                Dim ds As DataSet = Me.ShowPopup(frm, eventShubetsu, prm)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り件数が0件の場合は終了
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                'POP戻り値を画面に設定
                Me.SetPopupReturn(frm, eventShubetsu, prm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME040C.EventShubetsu.HOZON     '保存

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Me._closeErrFlg = True
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '現在表示している値をDataSetに設定
                Call Me.SetInOutSagyo(frm)

                '作業レコードの中で全部に影響ある値をDataSetに設定
                Call Me.SetInOutSagyoAll(frm)

                '作業指示書部分をDataSetに設定
                Call Me.SetInOutSagyoSiji(frm)

                '入力チェック２(DataSetに設定された値に対してするチェックはこのタイミングで行う)
                If Me._V.IsSingleCheckHozon(eventShubetsu, Me._Ds) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Me._closeErrFlg = True
                    Exit Sub
                End If

                '保存処理
                Call Me.HozonData(frm, eventShubetsu)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus(LME040C.MODE_VIEW)

                disp = DispMode.VIEW
                'シチュエーションラベルの設定
                Call Me._G.SetSituation(disp, RecordStatus.NOMAL_REC)

            Case LME040C.EventShubetsu.ROWADD    '行追加

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '現在表示している値をDataSetに設定
                Call Me.SetInOutSagyo(frm)

                '在庫テーブル照会POP表示
                Dim ds As DataSet = Me.ShowRowAdd(frm, eventShubetsu, prm)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り値が無い場合は終了

                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                Dim rtnFlg As Boolean = True

                '引当処理を呼ぶ
                rtnFlg = Me.ShowHIKIATEG(frm, prm, ds)

                If rtnFlg = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                'スプレッドのスクロール
                Me.SetEndScrollDtl(frm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus(LME040C.MODE_EDIT)

                '最終行行目を表示
                frm.sprDetails.ActiveSheet.ActiveRowIndex = frm.sprDetails.ActiveSheet.Rows.Count - 1
                '作業情報・在庫情報の表示
                Call Me._G.SetMeisaiData(Me._Ds)

            Case LME040C.EventShubetsu.ROWDEL    '行削除

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '現在表示している値をDataSetに設定
                Call Me.SetInOutSagyo(frm)

                '削除処理（データセット）
                Call Me.SetInOutSagyoDel(frm)

                '削除処理（SPREAD）
                Call Me.RowDelData(frm)

                '作業情報・在庫情報のクリア
                Call Me._G.ClearMeisaiData()

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                If frm.sprDetails.ActiveSheet.Rows.Count = 0 Then
                    '画面の入力項目の制御
                    Call Me._G.SetControlsStatus(LME040C.MODE_SINKI)
                Else
                    '1行目を表示
                    frm.sprDetails.ActiveSheet.ActiveRowIndex = 0
                    '作業情報・在庫情報の表示
                    Call Me._G.SetMeisaiData(Me._Ds)
                End If

            Case LME040C.EventShubetsu.PRINT    '印刷

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '印刷処理
                Call Me.PrintData(frm, eventShubetsu)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME040C.EventShubetsu.LEAVECELL    'スプレッドのロストフォーカス

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '現在表示している値をDataSetに設定
                Call Me.SetInOutSagyo(frm)

                '作業情報・在庫情報のクリア
                Call Me._G.ClearMeisaiData()

                '作業情報・在庫情報の表示
                Call Me._G.SetMeisaiData(Me._Ds)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME040C.EventShubetsu.DELETE   '削除処理

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSagyoSkyuStatusCheck(_Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                Call Me.DeleteData(frm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                '画面を閉じる
                frm.Close()

            Case LME040C.EventShubetsu.JIKKOU   '実行ボタン押下処理

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False OrElse _
                   Me._V.IsSingleWarningCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)
                Select Case frm.cmbJikkou.SelectedIndex
                    Case 1
                        '文書管理を呼ぶ
                        Me.ShowBunshoKanri(frm, prm)
                    Case 2
                        Me.WHSagyoShiji(frm, prm, LME800C.PROC_TYPE.CANCEL)
                    Case 3
                        Me.WHSagyoShiji(frm, prm, LME800C.PROC_TYPE.INSTRUCT)
                End Select

                '処理終了アクション
                Me._LMEConH.EndAction(frm)



        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LME040F, ByVal e As FormClosingEventArgs) As Boolean

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
            Exit Function
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                Me._closeErrFlg = False

                '保存処理
                Me.ActionControl(LME040C.EventShubetsu.HOZON, frm)

                If Me._closeErrFlg = True Then
                    '保存処理時にエラーがあった場合
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LME040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        Me.ActionControl(LME040C.EventShubetsu.HENSHU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LME040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        Me.ActionControl(LME040C.EventShubetsu.FUKUSHA, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LME040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        Me.ActionControl(LME040C.EventShubetsu.DELETE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LME040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        Select Case objNm
            Case "txtSagyo1", "txtSagyo2", "txtSagyo3", "txtSagyo4", "txtSagyo5"
                '作業コード
            Case "txtRemark1", "txtRemark2", "txtRemark3"
                '注意事項
            Case Else
                'ポップ対象外のテキストの場合
                MyBase.ShowMessage(frm, "G005")
                Exit Sub
        End Select

        Dim txtCtl As Win.InputMan.LMImTextBox = Nothing
        txtCtl = Me._G.GetTextControl(frm.ActiveControl.Name)
        If txtCtl.ReadOnly = True Then
            'アクティブコントロールがロック時は終了
            Exit Sub
        End If

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LME040C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LME040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Me.ActionControl(LME040C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LME040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LME040F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' 行追加押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnRowAdd_Click(ByRef frm As LME040F)

        Logger.StartLog(Me.GetType.Name, "btnRowAdd_Click")

        '「行削除」処理
        Me.ActionControl(LME040C.EventShubetsu.ROWADD, frm)

        Logger.EndLog(Me.GetType.Name, "btnRowAdd_Click")

    End Sub

    ''' <summary>
    ''' 行削除押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnRowDel_Click(ByRef frm As LME040F)

        Logger.StartLog(Me.GetType.Name, "btnRowDel_Click")

        '「行削除」処理
        Me.ActionControl(LME040C.EventShubetsu.ROWDEL, frm)

        Logger.EndLog(Me.GetType.Name, "btnRowDel_Click")

    End Sub

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LME040F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LME040C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPrint_Click")

    End Sub

    ''' <summary>
    ''' スプレッドロフトフォーカス
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sprDetails_LeaveCell(ByVal frm As LME040F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetails_LeaveCell")

        '違う行が選択された場合

        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then

            'DBより該当データの取得処理
            frm.sprDetails.ActiveSheet.ActiveRowIndex = e.NewRow
            Me.ActionControl(LME040C.EventShubetsu.LEAVECELL, frm)

        Else
            'DBより該当データの取得処理
            frm.sprDetails.ActiveSheet.ActiveRowIndex = e.NewRow
            Me.ActionControl(LME040C.EventShubetsu.LEAVECELL, frm)

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetails_LeaveCell")

    End Sub

    ''' <summary>
    ''' マスタ参照可能コントロールでEnter押下
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub masterEnter(ByRef frm As LME040F)

        Logger.StartLog(Me.GetType.Name, "masterEnter")

        Dim txtCtl As Win.InputMan.LMImTextBox = Nothing
        txtCtl = Me._G.GetTextControl(frm.ActiveControl.Name)
        If txtCtl.ReadOnly = True Then
            'アクティブコントロールがロック時は終了
            Exit Sub
        End If

        '「マスタ参照」処理
        Me.ActionControl(LME040C.EventShubetsu.MASTERENTER, frm)

        Logger.EndLog(Me.GetType.Name, "masterEnter")

    End Sub

    ''' <summary>
    ''' 実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnJIKKOU_Click(ByRef frm As LME040F)

        Logger.StartLog(Me.GetType.Name, "btnJIKKOU_Click")

        Me.ActionControl(LME040C.EventShubetsu.JIKKOU, frm)

        Logger.EndLog(Me.GetType.Name, "btnJIKKOU_Click")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LME040F, ByVal ds As DataSet)

        'スプレッドの行をクリア
        frm.sprDetails.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectData")

        '==========================
        'WSAクラス呼出
        '==========================
        Me._Ds = MyBase.CallWSA("LME040BLF", "SelectData", ds)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '取得した値を表示
        If 0 < Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows.Count Then
            '作業指示書部分の設定
            Call Me._G.SetSagyoSiji(Me._Ds)
        End If
        If 0 < Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Rows.Count Then
            'Spreadに設定
            Call Me._G.SetSpread(Me._Ds)
        End If

        '1行目を表示
        frm.sprDetails.ActiveSheet.ActiveRowIndex = 0
        '作業情報・在庫情報の表示
        Call Me._G.SetMeisaiData(Me._Ds)

        If 0 < frm.sprDetails.ActiveSheet.Rows.Count Then
            'メッセージの表示
            MyBase.ShowMessage(frm, "G003")
        Else
            'メッセージの表示
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage(frm, "E078", New String() {"作業指示書データ"})
            MyBase.ShowMessage(frm, "E078", New String() {frm.lblTitleSagyoSijiNo.Text()})
            '2016.01.06 UMANO 英語化対応END
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectData")

    End Sub

    ''' <summary>
    ''' 在庫引当起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowHIKIATEG(ByVal frm As LME040F, _
                                  ByVal prm As LMFormData, _
                                  ByVal ds As DataSet) As Boolean

        Dim rtnDs As DataSet = Nothing

        rtnDs = Me.ShowHIKIATE(frm, prm, ds)

        '行数設定
        If prm.ReturnFlg = False Then
            '戻り件数が0件の場合はエラー
            Return False
        End If

        '作業情報・在庫情報のクリア
        Call Me._G.ClearMeisaiData()

        Dim outdt As DataTable = rtnDs.Tables(LME050C.TABLE_NM_OUT)
        Dim max As Integer = outdt.Rows.Count - 1

        For i As Integer = 0 To max
            'DataSetに追加
            Call Me.AddInOutSagyo(frm, outdt(i))
        Next

        'Spreadに設定
        Call Me._G.SetSpread(Me._Ds)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G003")

        Return True

    End Function

    ''' <summary>
    ''' 在庫引当起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowHIKIATE(ByVal frm As LME040F, _
                                 ByVal prm As LMFormData, _
                                 ByVal ds As DataSet) As DataSet

        Dim prmDs As DataSet = New LME050DS
        Dim row As DataRow = prmDs.Tables(LME050C.TABLE_NM_IN).NewRow

        With ds.Tables(LMControlC.LMD100C_TABLE_NM_OUT).Rows(0)

            row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            row("WH_CD") = frm.txtSokoCd.TextValue
            row("CUST_CD_L") = frm.txtCustCdL.TextValue
            row("CUST_CD_M") = frm.txtCustCdM.TextValue
            row("CUST_NM_L") = frm.lblCustNmL.TextValue
            row("CUST_NM_M") = frm.lblCustNmM.TextValue
            row("GOODS_CD_NRS") = .Item("GOODS_CD_NRS").ToString
            row("GOODS_CD_CUST") = .Item("GOODS_CD_CUST").ToString
            row("GOODS_NM") = .Item("GOODS_NM_1").ToString
            row("SERIAL_NO") = .Item("SERIAL_NO").ToString
            row("RSV_NO") = .Item("RSV_NO").ToString
            row("LOT_NO") = .Item("LOT_NO").ToString
            If String.IsNullOrEmpty(.Item("IRIME").ToString) = False Then
                row("IRIME") = .Item("IRIME").ToString
            Else
                row("IRIME") = "0"
            End If
            row("IRIME_UT") = .Item("IRIME_UT").ToString
            row("NB_UT") = .Item("NB_UT").ToString
            row("STD_IRIME_UT") = .Item("STD_IRIME_UT").ToString
            If String.IsNullOrEmpty(.Item("PKG_NB").ToString) = False Then
                row("PKG_NB") = .Item("PKG_NB").ToString
            Else
                row("PKG_NB") = "0"
            End If

            If String.IsNullOrEmpty(row("LOT_NO").ToString) = False Then
                'ロット№が空の場合のみ設定する
                row("REMARK") = .Item("REMARK").ToString
            End If

            row("REMARK_OUT") = .Item("REMARK_OUT").ToString
            row("HIKIATE_ALERT_YN") = .Item("HIKIATE_ALERT_YN").ToString
            row("TAX_KB") = .Item("TAX_KB").ToString
            row("DEST_NM") = .Item("DEST_NM").ToString

            prmDs.Tables(LME050C.TABLE_NM_IN).Rows.Add(row)

        End With

        prm.ParamDataSet = prmDs

        '在庫引当呼出
        LMFormNavigate.NextFormNavigate(Me, "LME050", prm)

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowDelData(ByVal frm As LME040F)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        arr = Me._V.GetCheckList(LME040G.sprDetailsDef.DEF.ColNo, frm.sprDetails)
        Dim max As Integer = arr.Count - 1

        For i As Integer = max To 0 Step -1

            '選択された行を物理削除
            frm.sprDetails.ActiveSheet.Rows(Convert.ToInt32(arr(i))).Remove()

        Next

    End Sub

    ''' <summary>
    ''' 一覧のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetEndScrollDtl(ByVal frm As LME040F)

        Call Me.SetEndScroll(frm.sprDetails, True)

    End Sub

    ''' <summary>
    ''' 一覧のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="setFlg">アクティブセルを設定するかのフラグ</param>
    ''' <remarks></remarks>
    Private Sub SetEndScroll(ByVal spr As Win.Spread.LMSpread, ByVal setFlg As Boolean)

        With spr

            Dim maxRow As Integer = .ActiveSheet.Rows.Count - 1
            If maxRow < 0 Then
                Exit Sub
            End If

            spr.SetViewportTopRow(0, maxRow)

            If setFlg = True Then

                spr.ActiveSheet.SetActiveCell(maxRow, 0)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub HozonData(ByVal frm As LME040F, ByVal eventShubetsu As LME040C.EventShubetsu)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HozonData")

        '==========================
        'WSAクラス呼出
        '==========================
        Me._Ds = MyBase.CallWSA("LME040BLF", "HozonData", Me._Ds)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {"保存処理", ""})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName, ""})
        '2016.01.06 UMANO 英語化対応END

        'DataSetのUPD_FLGを更新
        Call Me.SetInOutSagyoUpdFlg()

        '作業情報・在庫情報のクリア
        Call Me._G.ClearMeisaiData()

        '作業指示書部分の設定
        Call Me._G.SetSagyoSiji(Me._Ds)

        'Spreadに設定
        Call Me._G.SetSpread(Me._Ds)

        '1行目を表示
        frm.sprDetails.ActiveSheet.ActiveRowIndex = 0
        '作業情報・在庫情報の表示
        Call Me._G.SetMeisaiData(Me._Ds)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LME040C.MODE_VIEW)

        '印刷処理
        Call Me.PrintData(frm, eventShubetsu)

        'START YANAI 要望番号1090 指摘修正
        '新規・編集フラグの設定
        Me._editFlg = True
        'END YANAI 要望番号1090 指摘修正

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HozonData")

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LME040F, ByVal eventShubetsu As LME040C.EventShubetsu)

        'DataSet設定
        Dim rtnDs As DataSet = New LME040DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtnDs, eventShubetsu)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtnDs.Tables(LME040C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LME040BLF", "PrintData", rtnDs)

        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowNonPopUpMessage(frm)
            Exit Sub
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

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal frm As LME040F)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName, ""})

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '現在表示している値をDataSetに設定
        Call Me.SetInOutSagyo(frm)

        '作業レコードの中で全部に影響ある値をDataSetに設定
        Call Me.SetInOutSagyoAll(frm)

        '作業指示書部分をDataSetに設定
        Call Me.SetInOutSagyoSiji(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Me._Ds = MyBase.CallWSA("LME040BLF", "DeleteData", Me._Ds)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(行追加時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInOutSagyoSiji(ByVal frm As LME040F)

        Dim inDr As DataRow = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).NewRow()

        If Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows.Count = 0 Then
            '新規
            inDr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).NewRow()
            inDr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            inDr("SAGYO_SIJI_NO") = String.Empty
            inDr("WH_CD") = frm.txtSokoCd.TextValue
            inDr("REMARK_1") = frm.txtRemark1.TextValue
            inDr("REMARK_2") = frm.txtRemark2.TextValue
            inDr("REMARK_3") = frm.txtRemark3.TextValue
            inDr("WH_TAB_STATUS") = frm.cmbWHSagyoSintyoku.SelectedValue
            inDr("SAGYO_SIJI_STATUS") = LME040C.SAGYO_SIJI_STATUS_IMCPMPLETE
            inDr("SAGYO_SIJI_DATE") = frm.imdSagyoDate.TextValue
            'データセットに設定
            Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows.Add(inDr)

        Else
            '更新
            Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("REMARK_1") = frm.txtRemark1.TextValue
            Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("REMARK_2") = frm.txtRemark2.TextValue
            Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("REMARK_3") = frm.txtRemark3.TextValue
            If "01".Equals(frm.cmbWHSagyoSintyoku.SelectedValue) Then
                Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("WH_TAB_STATUS") = "02"
            Else
                Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("WH_TAB_STATUS") = frm.cmbWHSagyoSintyoku.SelectedValue
            End If
            inDr("SAGYO_SIJI_STATUS") = LME040C.SAGYO_SIJI_STATUS_IMCPMPLETE
            inDr("SAGYO_SIJI_DATE") = frm.imdSagyoDate.TextValue

        End If

    End Sub

    ''' <summary>
    ''' データセット設定(行追加時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub AddInOutSagyo(ByVal frm As LME040F, ByVal dr As DataRow)

        Dim keyNo As String = Convert.ToString(Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Rows.Count)
        Dim inDr As DataRow = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).NewRow()
        inDr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).NewRow()

        inDr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        inDr("SAGYO_REC_NO") = String.Empty
        inDr("SAGYO_COMP") = "00"
        inDr("SKYU_CHK") = "00"
        inDr("SAGYO_SIJI_NO") = frm.txtSagyoSijiNo.TextValue
        inDr("INOUTKA_NO_LM") = String.Concat(dr.Item("INKA_NO_L").ToString, dr.Item("INKA_NO_M").ToString)
        inDr("WH_CD") = frm.txtSokoCd.TextValue
        inDr("IOZS_KB") = frm.cmbIozsKb.SelectedValue
        inDr("SAGYO_CD") = String.Empty
        inDr("SAGYO_NM") = String.Empty
        inDr("CUST_CD_L") = frm.txtCustCdL.TextValue
        inDr("CUST_CD_M") = frm.txtCustCdM.TextValue
        inDr("DEST_CD") = dr.Item("DEST_CD_P").ToString

        '---↓
        'Dim destDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' AND ", _
        '                                                                                                  "DEST_CD = '", inDr("DEST_CD").ToString, "'"))
        If String.IsNullOrEmpty(inDr("DEST_CD").ToString) = False Then
            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            destMstDr.Item("DEST_CD") = inDr("DEST_CD").ToString
            destMstDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            Dim destDr() As DataRow = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If destDr.Length > 0 Then
                inDr("DEST_NM") = destDr(0).Item("DEST_NM").ToString
            Else
                inDr("DEST_NM") = String.Empty
            End If
        Else
            inDr("DEST_NM") = String.Empty
        End If

        inDr("GOODS_CD_NRS") = dr.Item("GOODS_CD_NRS").ToString
        inDr("GOODS_NM_NRS") = dr.Item("NM_1").ToString
        inDr("LOT_NO") = dr.Item("LOT_NO").ToString
        inDr("INV_TANI") = String.Empty
        inDr("SAGYO_NB") = dr.Item("HIKI_KOSU").ToString
        inDr("SAGYO_UP") = String.Empty
        inDr("SAGYO_GK") = String.Empty
        inDr("TAX_KB") = dr.Item("TAX_KB").ToString

        If String.IsNullOrEmpty(frm.txtSagyoSeiqtoCd.TextValue) = False Then
            inDr("SEIQTO_CD") = frm.txtSagyoSeiqtoCd.TextValue
        Else
            inDr("SEIQTO_CD") = frm.txtOyaSeiqtoCd.TextValue
        End If

        inDr("REMARK_ZAI") = String.Empty
        inDr("REMARK_SKYU") = String.Empty
        inDr("SAGYO_COMP_CD") = String.Empty
        inDr("SAGYO_COMP_DATE") = frm.imdSagyoDate.TextValue
        inDr("DEST_SAGYO_FLG") = "00"
        inDr("ZAI_REC_NO") = dr.Item("ZAI_REC_NO").ToString
        inDr("PORA_ZAI_NB") = dr.Item("ALLOC_CAN_NB").ToString
        inDr("PORA_ZAI_QT") = dr.Item("ALLOC_CAN_QT").ToString
        inDr("GOODS_CD_CUST") = dr.Item("GOODS_CD_CUST").ToString
        inDr("TOU_NO") = dr.Item("TOU_NO").ToString
        inDr("SITU_NO") = dr.Item("SITU_NO").ToString
        inDr("ZONE_CD") = dr.Item("ZONE_CD").ToString
        inDr("LOCA") = dr.Item("LOCA").ToString
        inDr("IRIME") = dr.Item("IRIME").ToString
        inDr("LT_DATE") = dr.Item("LT_DATE").ToString
        inDr("GOODS_COND_KB_1_NM") = dr.Item("GOODS_COND_NM_1").ToString
        inDr("GOODS_COND_KB_2_NM") = dr.Item("GOODS_COND_NM_2").ToString
        inDr("GOODS_COND_KB_3_NM") = dr.Item("GOODS_COND_NM_3").ToString
        inDr("OFB_KB_NM") = dr.Item("OFB_KB_NM").ToString
        inDr("SPD_KB_NM") = dr.Item("SPD_KB_NM").ToString
        inDr("SERIAL_NO") = dr.Item("SERIAL_NO").ToString
        inDr("GOODS_CRT_DATE") = dr.Item("GOODS_CRT_DATE").ToString
        inDr("DEST_CD_P") = dr.Item("DEST_CD_P").ToString
        inDr("ALLOC_PRIORITY_NM") = dr.Item("ALLOC_PRIORITY_NM").ToString
        inDr("INKO_DATE") = dr.Item("INKO_DATE").ToString
        inDr("INKO_PLAN_DATE") = dr.Item("INKO_PLAN_DATE").ToString
        inDr("PORA_ZAI_NB_ZAI") = dr.Item("ALLOC_CAN_NB").ToString
        inDr("PORA_ZAI_QT_ZAI") = dr.Item("ALLOC_CAN_QT").ToString
        inDr("UPD_FLG") = "1"
        inDr("KEY_NO") = String.Concat(inDr("ZAI_REC_NO"), keyNo)

        'データセットに設定
        Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Rows.Add(inDr)

        frm.txtKeyNo.TextValue = inDr("KEY_NO").ToString

    End Sub

    ''' <summary>
    ''' データセット設定(現在表示している値を設定)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInOutSagyo(ByVal frm As LME040F)

        Dim dr() As DataRow = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select(String.Concat("KEY_NO = '", frm.txtKeyNo.TextValue, "' AND ", _
                                                                                               "(UPD_FLG = '0' OR UPD_FLG = '1')"))
        Dim dr2() As DataRow = Nothing
        Dim inDr As DataRow = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).NewRow()
        Dim sagyoDr() As DataRow = Nothing
        Dim destDr() As DataRow = Nothing

        Dim maxSagyo As Integer = 4
        Dim sagyoCd As String = String.Empty
        Dim sagyoNm As String = String.Empty
        Dim invTani As String = String.Empty
        Dim sagyoUp As String = String.Empty
        Dim kobuBai As String = String.Empty
        Dim sagyoRmk As String = String.Empty

        Dim max As Integer = 0

        For i As Integer = 0 To maxSagyo
            Select Case i
                Case 0
                    sagyoCd = frm.txtSagyo1.TextValue
                    sagyoRmk = frm.txtSagyoRemark1.TextValue
                Case 1
                    sagyoCd = frm.txtSagyo2.TextValue
                    sagyoRmk = frm.txtSagyoRemark2.TextValue
                Case 2
                    sagyoCd = frm.txtSagyo3.TextValue
                    sagyoRmk = frm.txtSagyoRemark3.TextValue
                Case 3
                    sagyoCd = frm.txtSagyo4.TextValue
                    sagyoRmk = frm.txtSagyoRemark4.TextValue
                Case 4
                    sagyoCd = frm.txtSagyo5.TextValue
                    sagyoRmk = frm.txtSagyoRemark5.TextValue
            End Select
            If String.IsNullOrEmpty(sagyoCd) = True Then
                Continue For
            End If

            '作業マスタから取得
            sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' AND ", _
                                                                                               "SAGYO_CD = '", sagyoCd, "'"))
            If sagyoDr.Length > 0 Then
                ''START YANAI 要望番号1090 指摘修正 → 2019/11/25 「作業名」に戻す 要望番号007243 
                sagyoNm = sagyoDr(0).Item("SAGYO_NM").ToString
                ''sagyoNm = sagyoDr(0).Item("SAGYO_RYAK").ToString
                ''END YANAI 要望番号1090 指摘修正
                invTani = sagyoDr(0).Item("INV_TANI").ToString
                sagyoUp = sagyoDr(0).Item("SAGYO_UP").ToString
                kobuBai = sagyoDr(0).Item("KOSU_BAI").ToString
            Else
                sagyoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("CUST_CD_L = 'ZZZZZ' AND ", _
                                                                                                   "SAGYO_CD = '", sagyoCd, "'"))
                If sagyoDr.Length > 0 Then
                    ''START YANAI 要望番号1090 指摘修正 → 2019/11/25 「作業名」に戻す 要望番号007243 
                    sagyoNm = sagyoDr(0).Item("SAGYO_NM").ToString
                    ''sagyoNm = sagyoDr(0).Item("SAGYO_RYAK").ToString
                    ''END YANAI 要望番号1090 指摘修正
                    invTani = sagyoDr(0).Item("INV_TANI").ToString
                    sagyoUp = sagyoDr(0).Item("SAGYO_UP").ToString
                    kobuBai = sagyoDr(0).Item("KOSU_BAI").ToString
                Else
                    sagyoNm = String.Empty
                    invTani = String.Empty
                    sagyoUp = "0"
                    kobuBai = String.Empty
                End If
            End If

            dr2 = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select(String.Concat("KEY_NO = '", frm.txtKeyNo.TextValue, "' AND ", _
                                                                                   "(UPD_FLG = '0' OR UPD_FLG = '1') AND ", _
                                                                                   "SAGYO_CD = '", sagyoCd, "'"))
            If dr2.Length = 0 Then
                '追加
                inDr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).NewRow()
                inDr("NRS_BR_CD") = dr(0).Item("NRS_BR_CD").ToString
                inDr("SAGYO_REC_NO") = String.Empty
                inDr("SAGYO_COMP") = "00"
                inDr("SKYU_CHK") = "00"
                inDr("SAGYO_SIJI_NO") = frm.txtSagyoSijiNo.TextValue
                inDr("INOUTKA_NO_LM") = dr(0).Item("INOUTKA_NO_LM").ToString
                inDr("WH_CD") = frm.txtSokoCd.TextValue
                inDr("IOZS_KB") = frm.cmbIozsKb.SelectedValue
                inDr("SAGYO_CD") = sagyoCd
                inDr("SAGYO_NM") = sagyoNm
                inDr("CUST_CD_L") = frm.txtCustCdL.TextValue
                inDr("CUST_CD_M") = frm.txtCustCdM.TextValue
                inDr("DEST_CD") = dr(0).Item("DEST_CD").ToString
                inDr("REMARK_SIJI") = sagyoRmk

                '---↓
                'destDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' AND ", _
                '                                                                                 "DEST_CD = '", inDr("DEST_CD").ToString, "'"))

                If String.IsNullOrEmpty(inDr("DEST_CD").ToString) = False Then
                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                    destMstDr.Item("DEST_CD") = inDr("DEST_CD").ToString
                    destMstDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    destDr = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                    '---↑

                    If destDr.Length > 0 Then
                        inDr("DEST_NM") = destDr(0).Item("DEST_NM").ToString
                    Else
                        inDr("DEST_NM") = String.Empty
                    End If
                Else
                    inDr("DEST_NM") = String.Empty
                End If

                inDr("GOODS_CD_NRS") = dr(0).Item("GOODS_CD_NRS").ToString
                inDr("GOODS_NM_NRS") = dr(0).Item("GOODS_NM_NRS").ToString
                inDr("LOT_NO") = dr(0).Item("LOT_NO").ToString
                inDr("INV_TANI") = invTani
                inDr("SAGYO_NB") = dr(0).Item("SAGYO_NB").ToString
                inDr("SAGYO_UP") = sagyoUp
                inDr("SAGYO_GK") = Convert.ToString(Math.Round((Convert.ToDecimal(sagyoUp) * Convert.ToDecimal(inDr("SAGYO_NB"))), MidpointRounding.AwayFromZero))
                inDr("TAX_KB") = dr(0).Item("TAX_KB").ToString

                If String.IsNullOrEmpty(frm.txtSagyoSeiqtoCd.TextValue) = False Then
                    inDr("SEIQTO_CD") = frm.txtSagyoSeiqtoCd.TextValue
                Else
                    inDr("SEIQTO_CD") = frm.txtOyaSeiqtoCd.TextValue
                End If

                inDr("REMARK_ZAI") = String.Empty
                inDr("REMARK_SKYU") = String.Empty
                inDr("SAGYO_COMP_CD") = String.Empty
                inDr("SAGYO_COMP_DATE") = frm.imdSagyoDate.TextValue
                inDr("DEST_SAGYO_FLG") = "00"
                inDr("ZAI_REC_NO") = dr(0).Item("ZAI_REC_NO").ToString
                inDr("PORA_ZAI_NB") = dr(0).Item("PORA_ZAI_NB").ToString
                inDr("PORA_ZAI_QT") = dr(0).Item("PORA_ZAI_QT").ToString
                inDr("GOODS_CD_CUST") = dr(0).Item("GOODS_CD_CUST").ToString
                inDr("TOU_NO") = dr(0).Item("TOU_NO").ToString
                inDr("SITU_NO") = dr(0).Item("SITU_NO").ToString
                inDr("ZONE_CD") = dr(0).Item("ZONE_CD").ToString
                inDr("LOCA") = dr(0).Item("LOCA").ToString
                inDr("IRIME") = dr(0).Item("IRIME").ToString
                inDr("LT_DATE") = dr(0).Item("LT_DATE").ToString
                inDr("GOODS_COND_KB_1_NM") = dr(0).Item("GOODS_COND_KB_1_NM").ToString
                inDr("GOODS_COND_KB_2_NM") = dr(0).Item("GOODS_COND_KB_2_NM").ToString
                inDr("GOODS_COND_KB_3_NM") = dr(0).Item("GOODS_COND_KB_3_NM").ToString
                inDr("OFB_KB_NM") = dr(0).Item("OFB_KB_NM").ToString
                inDr("SPD_KB_NM") = dr(0).Item("SPD_KB_NM").ToString
                inDr("SERIAL_NO") = dr(0).Item("SERIAL_NO").ToString
                inDr("GOODS_CRT_DATE") = dr(0).Item("GOODS_CRT_DATE").ToString
                inDr("DEST_CD_P") = dr(0).Item("DEST_CD_P").ToString
                inDr("ALLOC_PRIORITY_NM") = dr(0).Item("ALLOC_PRIORITY_NM").ToString
                inDr("INKO_DATE") = dr(0).Item("INKO_DATE").ToString
                inDr("INKO_PLAN_DATE") = dr(0).Item("INKO_PLAN_DATE").ToString
                inDr("PORA_ZAI_NB_ZAI") = dr(0).Item("PORA_ZAI_NB_ZAI").ToString
                inDr("PORA_ZAI_QT_ZAI") = dr(0).Item("PORA_ZAI_QT_ZAI").ToString
                inDr("UPD_FLG") = "1"
                inDr("KEY_NO") = dr(0).Item("KEY_NO").ToString

                'データセットに設定
                Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Rows.Add(inDr)
            ElseIf dr2.Length = 1 Then

                dr2(0).Item("REMARK_SIJI") = sagyoRmk

            End If

        Next

        '削除
        dr2 = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select(String.Concat("KEY_NO = '", frm.txtKeyNo.TextValue, "' AND ", _
                                                                               "(UPD_FLG = '0' OR UPD_FLG = '1') AND ", _
                                                                               "(SAGYO_CD <> '", frm.txtSagyo1.TextValue, "' AND ", _
                                                                               " SAGYO_CD <> '", frm.txtSagyo2.TextValue, "' AND ", _
                                                                               " SAGYO_CD <> '", frm.txtSagyo3.TextValue, "' AND ", _
                                                                               " SAGYO_CD <> '", frm.txtSagyo4.TextValue, "' AND ", _
                                                                               " SAGYO_CD <> '", frm.txtSagyo5.TextValue, "' AND ", _
                                                                               " SAGYO_CD <> '", String.Empty, "')" _
                                                                               ))
        max = dr2.Length - 1
        For i As Integer = 0 To max
            If ("0").Equals(dr2(i).Item("UPD_FLG").ToString) = True Then
                dr2(i).Item("UPD_FLG") = "2"
            Else
                dr2(i).Item("UPD_FLG") = "-1"
            End If
        Next

    End Sub

    ''' <summary>
    ''' データセット設定(作業レコードの中で全部に影響ある値をDataSetに設定)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInOutSagyoAll(ByVal frm As LME040F)

        Dim dr() As DataRow = Nothing
        Dim max As Integer = 0


        '新規レコードの場合
        dr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select("UPD_FLG = '1'")
        max = dr.Length - 1
        For i As Integer = 0 To max
            dr(i).Item("SAGYO_COMP_DATE") = frm.imdSagyoDate.TextValue
        Next

        '編集レコードの場合
        Dim inDr As DataRow = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).NewRow()
        Dim columnMax As Integer = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Columns.Count - 1
        dr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select("UPD_FLG = '0'")
        max = dr.Length - 1
        For i As Integer = 0 To max

            '作業日が異なる場合は、DataSetの更新
            If (frm.imdSagyoDate.TextValue).Equals(dr(i).Item("SAGYO_COMP_DATE").ToString) = False Then

                '既存レコードを新しいDataRowにコピーし、作業日だけ変更し、テーブルに新規追加
                inDr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).NewRow()
                For col As Integer = 0 To columnMax
                    inDr.Item(col) = dr(i).Item(col)
                Next
                inDr.Item("SAGYO_REC_NO") = String.Empty
                inDr.Item("SAGYO_COMP_DATE") = frm.imdSagyoDate.TextValue
                inDr.Item("UPD_FLG") = "1"
                'データセットに設定
                Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Rows.Add(inDr)

                '既存レコードはUPD_FLG="2"(削除)に更新する
                dr(i).Item("UPD_FLG") = "2"

            End If
        Next

    End Sub

    ''' <summary>
    ''' データセット設定(DataSetのUPD_FLGを更新)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetInOutSagyoUpdFlg()

        Dim dr() As DataRow = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select("UPD_FLG = '1'")
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max
            '作業レコード
            dr(i).Item("UPD_FLG") = "0"
        Next

    End Sub

    ''' <summary>
    ''' データセット設定(行削除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInOutSagyoDel(ByVal frm As LME040F)

        Dim chkList As ArrayList = Me._V.GetCheckList(LME040G.sprDetailsDef.DEF.ColNo, frm.sprDetails)
        Dim max As Integer = chkList.Count() - 1
        Dim dr() As DataRow = Nothing
        Dim drMax As Integer = 0

        For i As Integer = 0 To max
            dr = Me._Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYO).Select(String.Concat("KEY_NO = '", Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(Convert.ToInt32(chkList(i)), LME040G.sprDetailsDef.KEYNO.ColNo)), "' AND ", _
                                                                                  "(UPD_FLG = '0' OR UPD_FLG = '1')"))
            drMax = dr.Length - 1
            For j As Integer = 0 To drMax
                If ("0").Equals(dr(j).Item("UPD_FLG").ToString) = True Then
                    dr(j).Item("UPD_FLG") = "2"
                Else
                    dr(j).Item("UPD_FLG") = "-1"
                End If
            Next
        Next

    End Sub

    ''' <summary>
    ''' データセット設定(印刷時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LME040F, ByRef rtDs As DataSet, ByVal eventShubetsu As LME040C.EventShubetsu)

        Dim dr As DataRow = rtDs.Tables(LME040C.TABLE_NM_IN).NewRow()
        dr = rtDs.Tables(LME040C.TABLE_NM_IN).NewRow()

        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        dr("SAGYO_SIJI_NO") = frm.txtSagyoSijiNo.TextValue
        dr("PRINT_KB") = frm.cmbPrint.SelectedValue
        'START YANAI 要望番号1090 指摘修正
        If (LME040C.EventShubetsu.HOZON).Equals(eventShubetsu) = True Then
            dr("PRINT_KB") = "01"
        End If
        'END YANAI 要望番号1090 指摘修正

        'START YANAI 要望番号1090 指摘修正
        'If (LME040C.EventShubetsu.PRINT).Equals(eventShubetsu) = True Then
        '    '印刷ボタンからの印刷の場合
        '    dr("SAIHAKKO_FLG") = "1"
        'Else
        '    '保存時の印刷の場合
        '    dr("SAIHAKKO_FLG") = "0"
        'End If
        If Me._editFlg = True Then
            '2回目以降の印刷時
            dr("SAIHAKKO_FLG") = "1"
        Else
            '1回目の印刷時
            dr("SAIHAKKO_FLG") = "0"
        End If
        'END YANAI 要望番号1090 指摘修正

        'データセットに設定
        rtDs.Tables(LME040C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' POP起動(行追加の場合)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowRowAdd(ByVal frm As LME040F, ByVal eventShubetsu As LME040C.EventShubetsu, ByVal prm As LMFormData) As DataSet

        Select Case eventShubetsu

            Case LME040C.EventShubetsu.ROWADD         '行追加

                Dim prmDs As DataSet = New LMD100DS()
                Dim row As DataRow = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).NewRow

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
                row("WH_CD") = frm.txtSokoCd.TextValue
                row("CUST_CD_L") = frm.txtCustCdL.TextValue
                row("CUST_CD_M") = frm.txtCustCdM.TextValue
                row("INKA_STATE_KB") = "00"
                row("DEST_GOODS_FLG") = LMConst.FLG.OFF
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("ZERO_SEARCH_FLG") = "01"
                prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows.Add(row)

                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMD100", prm)

        End Select

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LME040F, ByVal eventShubetsu As LME040C.EventShubetsu, ByVal prm As LMFormData) As DataSet

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        Dim value As String = String.Empty
        Dim txtCtl As Win.InputMan.LMImTextBox = Nothing

        Select Case eventShubetsu

            Case LME040C.EventShubetsu.MASTER, _
                 LME040C.EventShubetsu.MASTERENTER 'マスタ参照

                Select Case objNm
                    Case "txtSagyo1", "txtSagyo2", "txtSagyo3", "txtSagyo4", "txtSagyo5"    '作業
                        Dim sagyoCnt As Integer = 0
                        Dim maxsagyoCnt As String = String.Empty
                        Dim msg As String = String.Empty
                        maxsagyoCnt = "5"
                        'value値の設定
                        txtCtl = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        If String.IsNullOrEmpty(frm.txtSagyo1.TextValue) = True Then
                            frm.lblSagyo1.TextValue = String.Empty
                            sagyoCnt = sagyoCnt + 1
                        End If
                        If String.IsNullOrEmpty(frm.txtSagyo2.TextValue) = True Then
                            frm.lblSagyo2.TextValue = String.Empty
                            sagyoCnt = sagyoCnt + 1
                        End If
                        If String.IsNullOrEmpty(frm.txtSagyo3.TextValue) = True Then
                            frm.lblSagyo3.TextValue = String.Empty
                            sagyoCnt = sagyoCnt + 1
                        End If
                        If String.IsNullOrEmpty(frm.txtSagyo4.TextValue) = True Then
                            frm.lblSagyo4.TextValue = String.Empty
                            sagyoCnt = sagyoCnt + 1
                        End If
                        If String.IsNullOrEmpty(frm.txtSagyo5.TextValue) = True Then
                            frm.lblSagyo5.TextValue = String.Empty
                            sagyoCnt = sagyoCnt + 1
                        End If
                        msg = "作業"

                        'START YANAI 要望番号1090 指摘修正
                        'If 0 = sagyoCnt Then
                        '    MyBase.ShowMessage(frm, "E242", New String() {msg, maxsagyoCnt})
                        '    prm.ReturnFlg = False
                        '    Return prm.ParamDataSet
                        'End If
                        'END YANAI 要望番号1090 指摘修正

                        '作業項目マスタ照会
                        Dim prmDs As DataSet = New LMZ200DS()
                        Dim row As DataRow = prmDs.Tables(LMZ200C.TABLE_NM_IN).NewRow

                        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                        row("CUST_CD_L") = frm.txtCustCdL.TextValue
                        row("SAGYO_CNT") = sagyoCnt
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                        If (eventShubetsu).Equals(LME040C.EventShubetsu.MASTERENTER) = True Then
                            'Enter押下時のみ、作業コードを設定
                            row("SAGYO_CD") = value
                        End If

                        prmDs.Tables(LMZ200C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ200", prm)

                    Case "txtRemark1", "txtRemark2", "txtRemark3"   '注意事項

                        Dim prmDs As DataSet = New LMZ270DS()
                        Dim row As DataRow = prmDs.Tables(LMZ270C.TABLE_NM_IN).NewRow

                        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                        row("USER_CD") = LMUserInfoManager.GetUserID()
                        row("SUB_KB") = "06"
                        row("REMARK") = String.Empty
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ270C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ270", prm)

                End Select

        End Select

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' POPからの戻り値を設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetPopupReturn(ByVal frm As LME040F, ByVal eventShubetsu As LME040C.EventShubetsu, ByVal prm As LMFormData)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()
        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)

        '戻り値を画面に設定
        Dim prmDs As DataSet = prm.ParamDataSet
        Dim dt As DataTable = Nothing
        Dim max As Integer = 0

        Select Case objNm
            Case "txtSagyo1", "txtSagyo2", "txtSagyo3", "txtSagyo4", "txtSagyo5"    '作業

                '戻り値を画面に設定
                dt = prmDs.Tables(LMZ200C.TABLE_NM_OUT)
                max = dt.Rows.Count - 1

                If -1 = max Then
                    Exit Sub
                End If

                '1件選択時は選択されたコントロールに設定
                If max = 0 Then
                    Select Case objNm
                        Case "txtSagyo1"
                            frm.txtSagyo1.TextValue = dt.Rows(0).Item("SAGYO_CD").ToString
                            frm.lblSagyo1.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                            frm.txtSagyoRemark1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                        Case "txtSagyo2"
                            frm.txtSagyo2.TextValue = dt.Rows(0).Item("SAGYO_CD").ToString
                            frm.lblSagyo2.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                            frm.txtSagyoRemark2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                        Case "txtSagyo3"
                            frm.txtSagyo3.TextValue = dt.Rows(0).Item("SAGYO_CD").ToString
                            frm.lblSagyo3.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                            frm.txtSagyoRemark3.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                        Case "txtSagyo4"
                            frm.txtSagyo4.TextValue = dt.Rows(0).Item("SAGYO_CD").ToString
                            frm.lblSagyo4.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                            frm.txtSagyoRemark4.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                        Case "txtSagyo5"
                            frm.txtSagyo5.TextValue = dt.Rows(0).Item("SAGYO_CD").ToString
                            frm.lblSagyo5.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                            frm.txtSagyoRemark5.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                    End Select
                    Exit Sub
                End If

                '複数件選択時
                For i As Integer = 0 To max
                    If String.IsNullOrEmpty(Trim(frm.txtSagyo1.TextValue)) = True Then
                        frm.txtSagyo1.TextValue = dt.Rows(i).Item("SAGYO_CD").ToString
                        frm.lblSagyo1.TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString
                        frm.txtSagyoRemark1.TextValue = dt.Rows(i).Item("WH_SAGYO_REMARK").ToString
                        Continue For
                    End If
                    If String.IsNullOrEmpty(Trim(frm.txtSagyo2.TextValue)) = True Then
                        frm.txtSagyo2.TextValue = dt.Rows(i).Item("SAGYO_CD").ToString
                        frm.lblSagyo2.TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString
                        frm.txtSagyoRemark2.TextValue = dt.Rows(i).Item("WH_SAGYO_REMARK").ToString
                        Continue For
                    End If
                    If String.IsNullOrEmpty(Trim(frm.txtSagyo3.TextValue)) = True Then
                        frm.txtSagyo3.TextValue = dt.Rows(i).Item("SAGYO_CD").ToString
                        frm.lblSagyo3.TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString
                        frm.txtSagyoRemark3.TextValue = dt.Rows(i).Item("WH_SAGYO_REMARK").ToString
                        Continue For
                    End If
                    If String.IsNullOrEmpty(Trim(frm.txtSagyo4.TextValue)) = True Then
                        frm.txtSagyo4.TextValue = dt.Rows(i).Item("SAGYO_CD").ToString
                        frm.lblSagyo4.TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString
                        frm.txtSagyoRemark4.TextValue = dt.Rows(i).Item("WH_SAGYO_REMARK").ToString
                        Continue For
                    End If
                    If String.IsNullOrEmpty(Trim(frm.txtSagyo5.TextValue)) = True Then
                        frm.txtSagyo5.TextValue = dt.Rows(i).Item("SAGYO_CD").ToString
                        frm.lblSagyo5.TextValue = dt.Rows(i).Item("SAGYO_RYAK").ToString
                        frm.txtSagyoRemark5.TextValue = dt.Rows(i).Item("WH_SAGYO_REMARK").ToString
                        Continue For
                    End If
                Next

            Case "txtRemark1", "txtRemark2", "txtRemark3"   '注意事項
                '戻り値を画面に設定
                dt = prmDs.Tables(LMZ270C.TABLE_NM_OUT)
                max = dt.Rows.Count - 1

                If -1 = max Then
                    Exit Sub
                End If

                '戻り値を設定
                txtCtl.TextValue = dt.Rows(0).Item("REMARK").ToString()

        End Select

    End Sub


    ''' <summary>
    ''' LMS文書管理(LMU010)起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowBunshoKanri(ByVal frm As LME040F, ByVal prm As LMFormData)

        ''LMS文書管理(LMU010)画面の処理表示用データ設定
        Dim prmDs As DataSet = New LMU010DS
        Dim row As DataRow = prmDs.Tables(LMControlC.LMU010C_TABLE_NM_IN).NewRow
        row("ENT_SYSID_KBN") = "06"
        row("KEY_TYPE_KBN") = "49"
        row("KEY_NO") = frm.txtSagyoSijiNo.TextValue

        prmDs.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'LMS文書管理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMU010", prm)

    End Sub

#End Region 'PopUp

#Region "その他処理"

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpread) As ArrayList

        Return Me._LMEconV.SprSelectList2(defNo, sprDetail)

    End Function

#End Region

#Region "タブレット対応"
    ''' <summary>
    ''' 現場作業指示取消
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub WHSagyoShiji(ByVal frm As LME040F, ByVal prm As LMFormData, ByVal procType As String)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        Dim ds As DataSet = New LME800DS

        Dim tabStatus As String = String.Empty
        Select Case procType
            Case LME800C.PROC_TYPE.INSTRUCT
                tabStatus = LME800C.WH_TAB_SIJI_STATUS.INSTRUCTED
            Case LME800C.PROC_TYPE.CANCEL
                tabStatus = LME800C.WH_TAB_SIJI_STATUS.NOT_INSTRUCTED
            Case LME800C.PROC_TYPE.DELETE
                tabStatus = LME800C.WH_TAB_SIJI_STATUS.NOT_INSTRUCTED
        End Select

        '検品済の場合データセットに登録
        Dim dRow As DataRow = ds.Tables(LME800C.TABLE_NM.LME800IN).NewRow
        dRow.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        dRow.Item("SAGYO_SIJI_NO") = frm.txtSagyoSijiNo.TextValue
        dRow.Item("ROW_NO") = String.Empty
        dRow.Item("PGID") = MyBase.GetPGID
        dRow.Item("SYS_UPD_DATE") = _Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("SYS_UPD_DATE").ToString
        dRow.Item("SYS_UPD_TIME") = _Ds.Tables(LME040C.TABLE_NM_INOUT_SAGYOSIJI).Rows(0).Item("SYS_UPD_TIME").ToString

        dRow.Item("WH_TAB_STATUS_KB") = tabStatus
        dRow.Item("PROC_TYPE") = procType

        ds.Tables(LME800C.TABLE_NM.LME800IN).Rows.Add(dRow)

        '処理呼出
        prm.ParamDataSet = ds

        LMFormNavigate.NextFormNavigate(Me, "LME800", prm)

        If MyBase.IsMessageExist Then

            MyBase.ShowMessage(frm)
        Else
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        End If

        '処理終了アクション
        Call Me._LMEConH.EndAction(frm)

    End Sub

#End Region
#End Region 'Method

End Class
