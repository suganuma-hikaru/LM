' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求
'  プログラムID     :  LMI040H : 請求データ編集[デュポン用]
'  作  成  者       :  [HISHI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports System.Text
Imports System.IO
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI040F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI040G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConV As LMIControlV

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMI040DS

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
    ''' キャンセルフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _CancelFlg As Boolean

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
        Dim frm As LMI040F = New LMI040F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI040G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI040V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.SHOKI)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        'シチュエーションラベルの設定
        Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()
        Call Me._G.SetInitValue()

        '数値コントロールの書式設定
        Call Me._G.SetNumberControl()

        'コントロールの日付書式設定
        Call Me._G.SetDateControl()

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI040C.EventShubetsu, ByVal frm As LMI040F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI040C.EventShubetsu.SINKI    '新規

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NEW_REC)

                '新規処理
                'START YANAI 要望番号830
                'Call Me._G.ClearControl()
                Call Me._G.ClearControlSinki()
                'END YANAI 要望番号830
                Call Me._G.ClearSpreadControl()
                Call Me._G.SetEigyo()

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.SINKI)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()

            Case LMI040C.EventShubetsu.HENSHU    '編集

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NOMAL_REC)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.HENSHU)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()

            Case LMI040C.EventShubetsu.FUKUSHA    '複写

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.COPY_REC)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.FUKUSHA)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()

            Case LMI040C.EventShubetsu.DEL    '削除

                Me._ChkList = Me._V.GetCheckList()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'START YANAI 要望番号830
                '確認メッセージ
                If MyBase.ShowMessage(Me._Frm, "W006") = MsgBoxResult.Cancel Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If
                'END YANAI 要望番号830

                '削除処理
                rtnDs = Me.DeleteData(frm, Me._ChkList)

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.HOZON)

                    'コントロールの入力制御
                    Call Me._G.SetControlsStatus()
                    Exit Sub
                End If

                '画面項目のクリア
                Call Me._G.ClearControl()
                Call Me._G.ClearSpreadControl()

                MyBase.ShowMessage(frm, "G002", New String() {"削除処理", String.Empty})

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.DEL)

            Case LMI040C.EventShubetsu.KENSAKU    '検索

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
                If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
                    If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                        Call Me._LMIConH.EndAction(frm) '終了処理

                        'ファンクションキーの設定
                        Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.HOZON)

                        'コントロールの入力制御
                        Call Me._G.SetControlsStatus()
                        MyBase.ShowMessage(frm, "G003")
                        Exit Sub
                    Else      'OK押下
                        Call Me._G.ClearControl()
                        Call Me._G.ClearSpreadControl()
                    End If
                End If

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

                '新規処理
                Call Me._G.ClearControl()
                Call Me._G.ClearSpreadControl()

                '検索処理
                rtnDs = Me.KensakuData(frm)

                'Spread表示処理
                Me.SetSpread(frm, rtnDs)

                'START YANAI 要望番号830
                '各金額の合計を設定
                Call Me._G.SumKingaku()
                'END YANAI 要望番号830

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.KENSAKU)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()

            Case LMI040C.EventShubetsu.HOZON    '保存

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '請求計算処理
                Call Me._G.SEIKYUKEISAN()

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '保存処理
                rtnDs = Me.HozonData(frm)

                '合算ワーニングでキャンセル押下は終了
                If Me._CancelFlg = True Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.HOZON)

                    'コントロールの入力制御
                    Call Me._G.SetControlsStatus()
                    Exit Sub
                End If

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.HOZON)

                    'コントロールの入力制御
                    Call Me._G.SetControlsStatus()
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


                '画面項目のクリア
                Call Me._G.ClearControl()
                Call Me._G.ClearSpreadControl()

                MyBase.ShowMessage(frm, "G002", New String() {"保存処理", String.Empty})

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.HOZON)

                'コントロールの入力制御
                Call Me._G.SetControlsStatus()

            Case LMI040C.EventShubetsu.PRINT    '印刷

                Me._ChkList = Me._V.GetCheckList()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '印刷処理
                Call Me.PrintData(frm)

                MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", String.Empty})

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI040C.EventShubetsu.FILEMAKE    'ファイル作成

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '検索処理
                rtnDs = Me.CsvKensakuData(frm)

                'CSV出力処理
                Call Me.MakeCsvMain(frm, rtnDs)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI040C.EventShubetsu.SEIKYUKEISAN '請求計算処理

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._ChkList) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    Exit Sub
                End If

                '請求計算処理
                Call Me._G.SEIKYUKEISAN()

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI040F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        '「新規」処理
        Me.ActionControl(LMI040C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        '「編集」処理
        Me.ActionControl(LMI040C.EventShubetsu.HENSHU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        '「複写」処理
        Me.ActionControl(LMI040C.EventShubetsu.FUKUSHA, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        '「削除」処理
        Me.ActionControl(LMI040C.EventShubetsu.DEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        '「検索」処理
        Me.ActionControl(LMI040C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        '「保存」処理
        Me.ActionControl(LMI040C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI040F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI040F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' ファイル作成押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnFile_Click(ByRef frm As LMI040F)

        Logger.StartLog(Me.GetType.Name, "btnFile_Click")

        '「ファイル作成」処理
        Me.ActionControl(LMI040C.EventShubetsu.FILEMAKE, frm)

        Logger.EndLog(Me.GetType.Name, "btnFile_Click")

    End Sub

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI040F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LMI040C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPrint_Click")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetailLeaveCell(ByVal frm As LMI040F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetailLeaveCell")

        '選択処理
        Call Me.SprFindLeaveCell(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetailLeaveCell")

    End Sub

    ''' <summary>
    ''' 課税金額のLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub numKazei_Leave(ByVal frm As LMI040F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "numKazei_Leave")

        '「請求金額計算」処理
        Me.ActionControl(LMI040C.EventShubetsu.SEIKYUKEISAN, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "numKazei_Leave")

    End Sub

    ''' <summary>
    ''' 非課税金額のLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub numHiKazei_Leave(ByVal frm As LMI040F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "numKazei_Leave")

        '「請求金額計算」処理
        Me.ActionControl(LMI040C.EventShubetsu.SEIKYUKEISAN, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "numKazei_Leave")

    End Sub

    'START YANAI 要望番号830
    ''' <summary>
    ''' 印刷種別変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbPrint_SelectedValueChanged(ByVal frm As LMI040F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ReCreatePrintTypeControl(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 印刷種別1変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbPrint_Type1_SelectedValueChanged(ByVal frm As LMI040F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ReCreatePrintType1Control(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'END YANAI 要望番号830

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function KensakuData(ByVal frm As LMI040F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI040DS()

        'InDataSetの場合
        Call Me.SetInDataKensaku(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KensakuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI040BLF", _
                                                         "SelectListData", _
                                                         rtDs, _
                                                         Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                         -1, _
                                                         False)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KensakuData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 検索結果をSpreadに表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SetSpread(ByVal frm As LMI040F, ByVal ds As DataSet)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(ds)

        'メッセージエリアの設定
        'START YANAI 要望番号830
        'MyBase.ShowMessage(frm, "G016", New String() {Convert.ToString(frm.sprDetail.ActiveSheet.Rows.Count)})
        If frm.sprDetail.ActiveSheet.Rows.Count - 1 = 0 Then
            MyBase.ShowMessage(frm, "G001")
        Else
            MyBase.ShowMessage(frm, "G016", New String() {Convert.ToString(frm.sprDetail.ActiveSheet.Rows.Count - 1)})
        End If
        'END YANAI 要望番号830

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal frm As LMI040F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.NewRow
        'START YANAI 要望番号830
        'If rowNo < 0 Then
        '    Exit Sub
        'End If
        If rowNo <= 0 Then
            Exit Sub
        End If
        'END YANAI 要望番号830

        '同じ行の場合、スルー
        If e.Row = rowNo AndAlso _
            (e.Row = 0 AndAlso _
             String.IsNullOrEmpty(frm.imdTuki.TextValue) = False) Then
            Exit Sub
        End If

        Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMI040F, ByVal rowNo As Integer)

        Dim recstatus As String = String.Empty

        '権限チェック
        If Me._V.IsAuthorityChk(LMI040C.EventShubetsu.DOUBLECLICK) = False Then
            MyBase.ShowMessage(Me._Frm, "E016")
            Exit Sub
        End If

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMIConG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMI040G.sprDetailDef.SYSDELFLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetSituation(DispMode.VIEW, recstatus)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMI040C.EventShubetsu.DOUBLECLICK)

        'クリア処理
        Call Me._G.ClearControl()

        'START YANAI 要望番号830
        '各金額の合計を設定
        Call Me._G.SumKingaku()
        'END YANAI 要望番号830

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(frm, rowNo)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function HozonData(ByVal frm As LMI040F) As DataSet

        Dim gassanFlg As Boolean = False '合算処理判定用フラグ
        Me._CancelFlg = False

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI040DS()

        'InDataSetの場合
        Call Me.SetInDataHozon(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HozonData")

        If frm.lblSituation.RecordStatus = RecordStatus.COPY_REC OrElse _
            frm.lblSituation.RecordStatus = RecordStatus.NEW_REC Then
            '複写時は合算処理があるため、処理分岐

            '==========================
            'WSAクラス呼出
            '==========================
            Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                             "LMI040BLF", _
                                                             "SelectCopyData", _
                                                             rtDs, _
                                                             Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                             (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                             -1, _
                                                             True)

            'エラー時はメッセージを表示して終了
            If MyBase.IsMessageExist() = True Then
                Select Case MyBase.ShowMessage(frm)
                    Case MsgBoxResult.Ok '「OK」押下時
                        '処理続行
                        gassanFlg = True
                    Case MsgBoxResult.Cancel '「キャンセル」押下時
                        Me._CancelFlg = True
                        Return rtnDs

                End Select

            End If

        End If

        '更新処理
        Dim saveMode As String = String.Empty
        If gassanFlg = True Then
            saveMode = "GassanSaveAction"
        ElseIf frm.lblSituation.RecordStatus = RecordStatus.NEW_REC _
        OrElse frm.lblSituation.RecordStatus = RecordStatus.COPY_REC Then
            saveMode = "InsertSaveAction"
        Else
            saveMode = "UpdateSaveAction"
        End If

        '==== WSAクラス呼出（変更処理） ====
        rtDs = Me.ServerAccess(rtDs, saveMode)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HozonData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal frm As LMI040F, ByVal arr As ArrayList) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim rtDs As DataSet = New LMI040DS()

        'InDataSetの場合
        rtDs = Me.SetInDataDelete(frm, arr)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==== WSAクラス呼出（変更処理） ====
        rtDs = Me.ServerAccess(rtDs, "DeleteSaveAction")

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMI040F)

        'DataSet設定
        Dim rtnDs As DataSet = New LMI040DS()

        'InDataSetの場合
        rtnDs = Me.SetInDataPrint(frm, Me._ChkList)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtnDs.Tables(LMI040C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMI040BLF", "PrintData", rtnDs)

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
    ''' CSV出力データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function CsvKensakuData(ByVal frm As LMI040F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI040DS()

        'InDataSetの場合
        Call Me.SetInDataKensaku(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "CsvKensakuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI040BLF", _
                                                         "SelectCsvListData", _
                                                         rtDs, _
                                                         -1, _
                                                         -1, _
                                                         False)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return rtnDs
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "CsvKensakuData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' CSV作成メイン
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsvMain(ByVal frm As LMI040F, ByVal ds As DataSet)

        'If ("00").Equals(frm.cmbFile.SelectedValue) = True Then

        '    If ds.Tables(LMI040C.TABLE_NM_CSV_UNCHIN).Rows.Count = 0 Then
        '        '取得件数が0件の場合は終了
        '        MyBase.ShowMessage(frm, "G001")
        '        Exit Sub
        '    End If

        '    '運賃データ送信ファイル作成
        '    Call Me.MakeCsvUnchin(frm, ds)
        'ElseIf ("01").Equals(frm.cmbFile.SelectedValue) = True Then

        '    If ds.Tables(LMI040C.TABLE_NM_CSV_GL).Rows.Count = 0 Then
        '        '取得件数が0件の場合は終了
        '        MyBase.ShowMessage(frm, "G001")
        '        Exit Sub
        '    End If

        '    '請求データ送信ファイル作成
        '    Call Me.MakeCsvGL(frm, ds)
        'ElseIf ("02").Equals(frm.cmbFile.SelectedValue) = True Then

        '    If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count = 0 AndAlso _
        '       ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows.Count = 0 Then
        '        '取得件数が0件の場合は終了
        '        MyBase.ShowMessage(frm, "G001")
        '        Exit Sub
        '    End If

        '    'FPDEデータファイル作成
        '    Call Me.MakeCsvFPDE(frm, ds)
        'End If
        If ("00").Equals(frm.cmbFile.SelectedValue) = True Then

            If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count = 0 AndAlso
               ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows.Count = 0 Then
                '取得件数が0件の場合は終了
                MyBase.ShowMessage(frm, "G001")
                Exit Sub
            End If

            'FPDEデータファイル作成 BD00942
#If False Then  'UPD 2021/05/20 020903   【LMS】ケマーズ・アクサルタの特別荷主機能の編集
            Call Me.MakeCsvFPDE(frm, ds)
#Else
            '2ファイルにわけて出力
#If False Then   'AADD 2021/05/19 アクサルタ

            Dim setDs As DataSet = ds.Copy()

            'FRB_CD <> 'BD00942' のとき

            If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count - 1 > 0 Then
                Dim drHIKAZEI As DataRow() = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Select("FRB_CD <> 'BD00942'")
                'Dim sortDtRpt As DataTable = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Clone

                Dim dt As DataTable = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI)
                dt.Clear()
                'ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Clear()
                'Dim dt As DataTable = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI)


                Dim cnt As Integer = drHIKAZEI.Length - 1
                Dim dr As DataRow = Nothing
                For i As Integer = 0 To cnt
                    dr = drHIKAZEI(i)
                    'dt.Rows.Add(dr)
                    dt.ImportRow(dr)
                Next

                For Each row As DataRow In drHIKAZEI
                    dt.Rows.Add(row)
                    'ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).ImportRow(row)
                    'ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).ImportRow(row)
                Next

                'ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).ImportRow(drHIKAZE())

            End If

            If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows.Count - 1 > 0 Then
                'Dim drKAZEI As DataRow = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Select(String.Concat(" FRB_CD = '", sFRB_CD, "'"))
                Dim drKAZEI As DataRow() = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Select("FRB_CD <> 'BD00942'")
                ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Clear()

                For Each row As DataRow In drKAZEI
                    ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).ImportRow(row)
                Next
                'ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).ImportRow(drKAZEI)


            End If
#End If

            Call Me.MakeCsvFPDE(frm, ds, "NotBD00942")
            Call Me.MakeCsvFPDE(frm, ds, "BD00942")

#End If
        End If

    End Sub

    '''' <summary>
    '''' CSV作成(運賃データ送信ファイル)
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    'Private Sub MakeCsvUnchin(ByVal frm As LMI040F, ByVal ds As DataSet)

    '    Dim max As Integer = ds.Tables(LMI040C.TABLE_NM_CSV_UNCHIN).Rows.Count - 1

    '    'CSV出力処理
    '    Dim setData As StringBuilder = New StringBuilder()

    '    'ヘッダ部の出力
    '    setData.Append("HR6       ANRS    ANRS001 ADKK    ADKK001                                  ")
    '    setData.Append(vbNewLine)

    '    For i As Integer = 0 To max
    '        With ds.Tables(LMI040C.TABLE_NM_CSV_UNCHIN).Rows(i)
    '            setData.Append("R6  WPX")

    '            If String.IsNullOrEmpty(.Item("CUST_ORD_NO").ToString()) = False AndAlso _
    '                ("WPX").Equals(.Item("CUST_ORD_NO").ToString()) = True Then
    '                setData.Append(.Item("CUST_ORD_NO").ToString())
    '            End If
    '            setData.Append("            ")
    '            setData.Append(Mid(String.Concat(.Item("GOODS_CD_CUST").ToString(), Space(16)), 1, 16))
    '            setData.Append(String.Concat(MaeCoverData(.Item("DECI_UNCHIN").ToString(), "0", 10), "00"))
    '            setData.Append(Space(28))
    '            setData.Append(vbNewLine)
    '        End With
    '    Next

    '    '保存先のCSVファイルのパス
    '    Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C006' AND ", _
    '                                                                                                    "KBN_CD = '", frm.cmbFile.SelectedValue, "'"))
    '    'システム日付の取得
    '    Dim rtnDs As DataSet = MyBase.CallWSA("LMI040BLF", "SetSysDateTime", ds)
    '    Dim sysDt As DataTable = rtnDs.Tables(LMI040C.TABLE_NM_SYS_DATETIME)

    '    Dim csvPath As String = String.Concat(kbnDr(0).Item("KBN_NM2").ToString, _
    '                                          kbnDr(0).Item("KBN_NM3").ToString, _
    '                                          sysDt.Rows(0).Item("SYS_DATE").ToString(), _
    '                                          sysDt.Rows(0).Item("SYS_TIME").ToString(), _
    '                                          ".txt")

    '    'CSVファイルに書き込むときに使うEncoding
    '    Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

    '    'ファイルを開く
    '    System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM2").ToString)
    '    Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

    '    '値の設定
    '    sr.Write(setData.ToString())

    '    'ファイルを閉じる
    '    sr.Close()

    '    '処理終了メッセージの表示
    '    MyBase.ShowMessage(frm, "G002", New String() {"ファイル作成処理", String.Empty})

    'End Sub

    '''' <summary>
    '''' CSV作成(請求データ送信ファイル作成)
    '''' </summary>
    '''' <param name="ds">DataSet</param>
    '''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    'Private Sub MakeCsvGL(ByVal frm As LMI040F, ByVal ds As DataSet)

    '    Dim max As Integer = ds.Tables(LMI040C.TABLE_NM_CSV_GL).Rows.Count - 1

    '    Dim sumAmount As Decimal = 0
    '    '請求金額 + 税額の合計を求める
    '    For i As Integer = 0 To max
    '        sumAmount = sumAmount + _
    '                    Convert.ToDecimal(ds.Tables(LMI040C.TABLE_NM_CSV_GL).Rows(i).Item("AMOUNT").ToString) + _
    '                    Convert.ToDecimal(ds.Tables(LMI040C.TABLE_NM_CSV_GL).Rows(i).Item("VAT_AMOUNT").ToString)
    '    Next

    '    'CSV出力処理
    '    Dim setData As StringBuilder = New StringBuilder()

    '    'ヘッダ部の出力（1行目)
    '    setData.Append("HR7       ANRS    ANRS001 ADKK    ADKK001")
    '    setData.Append(Space(279))
    '    setData.Append(vbNewLine)

    '    'ヘッダ部の出力（2行目)
    '    setData.Append("HEAD      50JP-PC-FPDE  PASS    ETB01")
    '    setData.Append(Space(283))
    '    setData.Append(vbNewLine)

    '    'ヘッダ部の出力（3行目)
    '    setData.Append("TB01HD                          VI21     ")
    '    setData.Append(Mid(frm.imdSeikyu.TextValue, 7, 2))
    '    setData.Append(".")
    '    setData.Append(Mid(frm.imdSeikyu.TextValue, 5, 2))
    '    setData.Append(".")
    '    setData.Append(Mid(frm.imdSeikyu.TextValue, 3, 2))
    '    setData.Append("     ")
    '    setData.Append(Mid(frm.imdSeikyu.TextValue, 7, 2))
    '    setData.Append(".")
    '    setData.Append(Mid(frm.imdSeikyu.TextValue, 5, 2))
    '    setData.Append(".")
    '    setData.Append(Mid(frm.imdSeikyu.TextValue, 3, 2))
    '    setData.Append("        ")
    '    setData.Append("JPY                                                                499900")
    '    setData.Append(Space(177))
    '    setData.Append(vbNewLine)

    '    'ヘッダ部の出力（4行目)
    '    setData.Append("TB01AP    31601419              ")
    '    setData.Append(Right(String.Concat("              ", sumAmount), 14))
    '    setData.Append("                                          14")
    '    setData.Append(Space(230))
    '    setData.Append(vbNewLine)

    '    For i As Integer = 0 To max
    '        With ds.Tables(LMI040C.TABLE_NM_CSV_GL).Rows(i)
    '            setData.Append("TB01GL    404030120             ")
    '            setData.Append(Right(String.Concat("              ", .Item("AMOUNT").ToString), 14))
    '            setData.Append(Right(String.Concat("              ", .Item("VAT_AMOUNT").ToString), 14))
    '            setData.Append("                            14")
    '            setData.Append(Space(43))
    '            If String.IsNullOrEmpty(.Item("SRC_CD").ToString) = False Then
    '                setData.Append(.Item("SRC_CD").ToString)
    '            Else
    '                setData.Append("  ")
    '            End If
    '            setData.Append(Space(148))
    '            If String.IsNullOrEmpty(.Item("FRB_CD").ToString) = False Then
    '                setData.Append(.Item("FRB_CD").ToString)
    '            Else
    '                setData.Append("       ")
    '            End If
    '            setData.Append(Space(30))
    '            setData.Append(vbNewLine)
    '        End With
    '    Next

    '    '保存先のCSVファイルのパス
    '    Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C006' AND ", _
    '                                                                                                    "KBN_CD = '", frm.cmbFile.SelectedValue, "'"))
    '    'システム日付の取得
    '    Dim rtnDs As DataSet = MyBase.CallWSA("LMI040BLF", "SetSysDateTime", ds)
    '    Dim sysDt As DataTable = rtnDs.Tables(LMI040C.TABLE_NM_SYS_DATETIME)

    '    Dim csvPath As String = String.Concat(kbnDr(0).Item("KBN_NM2").ToString, _
    '                                          kbnDr(0).Item("KBN_NM3").ToString, _
    '                                          sysDt.Rows(0).Item("SYS_DATE").ToString(), _
    '                                          sysDt.Rows(0).Item("SYS_TIME").ToString(), _
    '                                          ".txt")

    '    'CSVファイルに書き込むときに使うEncoding
    '    Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

    '    'ファイルを開く
    '    System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM2").ToString)
    '    Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

    '    '値の設定
    '    sr.Write(setData.ToString())

    '    'ファイルを閉じる
    '    sr.Close()

    '    '処理終了メッセージの表示
    '    MyBase.ShowMessage(frm, "G002", New String() {"ファイル作成処理", String.Empty})

    'End Sub

    ''' <summary>
    ''' CSV作成(FPDEデータファイル作成)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks>DACのMakeCsvメソッド呼出</remarks>
    Private Sub MakeCsvFPDE(ByVal frm As LMI040F, ByVal ds As DataSet, Optional ByVal sFRB_CD As String = "")
        '#If True Then   'AADD 2021/05/19 アクサルタ
        '        Dim setDs As DataSet = ds.Copy()
        '        If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count - 1 > 0 Then
        '            ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI) = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Select(String.Concat(" FRB_CD = '", sFRB_CD, "'"))
        '        End If

        '        If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count - 1 > 0 Then


        '        End If
        '#End If

        Dim maxHIKAZEI As Integer = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count - 1
        Dim maxKAZEI As Integer = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows.Count - 1

        'システム日付の取得
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI040BLF", "SetSysDateTime", ds)
        Dim sysDt As DataTable = rtnDs.Tables(LMI040C.TABLE_NM_SYS_DATETIME)

        '20170314 月末日の算出
        Dim year As Integer
        Dim month As Integer

        If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows.Count = 0 Then
            If ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows.Count = 0 Then
                'ALL-NGの場合はシステム日付
                year = Integer.Parse(sysDt.Rows(0).Item("SYS_DATE").ToString().ToString().Substring(0, 4))
                month = Integer.Parse(sysDt.Rows(0).Item("SYS_DATE").ToString().ToString().Substring(4, 2))
            Else
                year = Integer.Parse(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(0).Item("SEKY_YM").ToString().Substring(0, 4))
                month = Integer.Parse(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(0).Item("SEKY_YM").ToString().Substring(4, 2))

            End If
        Else
            year = Integer.Parse(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(0).Item("SEKY_YM").ToString().Substring(0, 4))
            month = Integer.Parse(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(0).Item("SEKY_YM").ToString().Substring(4, 2))

        End If
        'Dim year As Integer = Integer.Parse(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(0).Item("SEKY_YM").ToString().Substring(0, 4))
        'Dim month As Integer = Integer.Parse(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(0).Item("SEKY_YM").ToString().Substring(4, 2))
        Dim days As Integer = DateTime.DaysInMonth(year, month) ' その月の日数
        Dim lastDayOfMonth As String = New DateTime(year, month, days).ToString
        Dim SekyDate As String = lastDayOfMonth.Substring(0, 4) + lastDayOfMonth.Substring(5, 2) + lastDayOfMonth.Substring(8, 2)

        'アクサルタフラグ
        Dim akusarutaFlag As Boolean = False
        If frm.cmbSEIQTO_KBN.SelectedValue.ToString = "01" Then '塗料→アクサルタ
            akusarutaFlag = True
        End If

        'CSV出力処理
        Dim setData As StringBuilder = New StringBuilder()

        '①ヘッダ部の出力
        setData.Append("BH00FPDE        ")
        setData.Append(sysDt.Rows(0).Item("SYS_DATE").ToString())
        setData.Append("4500FPDESBDC")
        setData.Append(vbNewLine)

        '②各レコードごとのヘッダ部分を作成
        Dim recHead As String = String.Empty
        recHead = String.Concat(recHead, "DH00FPDE        ")
        recHead = String.Concat(recHead, sysDt.Rows(0).Item("SYS_DATE").ToString())
        recHead = String.Concat(recHead, SekyDate)
        recHead = String.Concat(recHead, "KB4500")
        recHead = String.Concat(recHead, sysDt.Rows(0).Item("SYS_DATE").ToString())
        recHead = String.Concat(recHead, "  JPY  ")
        recHead = String.Concat(recHead, "                            ")
        recHead = String.Concat(recHead, "DS-NICHIRIKU    ")
        recHead = String.Concat(recHead, "FPDE DATA FOR ")
        recHead = String.Concat(recHead, Mid(frm.imdSeikyu.TextValue, 1, 4))
        recHead = String.Concat(recHead, "/")
        recHead = String.Concat(recHead, Mid(frm.imdSeikyu.TextValue, 5, 2))

        '③247レコードごとに出力するヘッダ部分を作成(非課税)
        Dim hikazeiHead As String = String.Empty
        hikazeiHead = String.Concat(hikazeiHead, "AP00")
        hikazeiHead = String.Concat(hikazeiHead, "FPDE        ")
        hikazeiHead = String.Concat(hikazeiHead, sysDt.Rows(0).Item("SYS_DATE").ToString())
        hikazeiHead = String.Concat(hikazeiHead, "31")
        hikazeiHead = String.Concat(hikazeiHead, "10025067         ")
        hikazeiHead = String.Concat(hikazeiHead, " ")
        hikazeiHead = String.Concat(hikazeiHead, "非課税金額合計１")
        hikazeiHead = String.Concat(hikazeiHead, "非課税金額合計２")
        hikazeiHead = String.Concat(hikazeiHead, SekyDate)
#If False Then  'UPD 2019/01/31 依頼番号 : 003976   【LMS】アクサルタFPDEデータ作成機能_PaymentTermの変更
        hikazeiHead = String.Concat(hikazeiHead, "K015120")
#Else
        hikazeiHead = String.Concat(hikazeiHead, "K013090")
#End If
        hikazeiHead = String.Concat(hikazeiHead, "")
        hikazeiHead = String.Concat(hikazeiHead, "      ")
        hikazeiHead = String.Concat(hikazeiHead, "   ")
        hikazeiHead = String.Concat(hikazeiHead, "      ")
        hikazeiHead = String.Concat(hikazeiHead, "   ")
        hikazeiHead = String.Concat(hikazeiHead, "V1")
        hikazeiHead = String.Concat(hikazeiHead, "          ")
        hikazeiHead = String.Concat(hikazeiHead, " ")
        hikazeiHead = String.Concat(hikazeiHead, "N")

        '⑤247レコードごとに出力するヘッダ部分を作成(課税)
        Dim kazeiHead As String = String.Empty
        kazeiHead = String.Concat(kazeiHead, "AP00")
        kazeiHead = String.Concat(kazeiHead, "FPDE        ")
        kazeiHead = String.Concat(kazeiHead, sysDt.Rows(0).Item("SYS_DATE").ToString())
        kazeiHead = String.Concat(kazeiHead, "31")
        kazeiHead = String.Concat(kazeiHead, "10025067         ")
        kazeiHead = String.Concat(kazeiHead, " ")
        kazeiHead = String.Concat(kazeiHead, "課税金額合計１")
        kazeiHead = String.Concat(kazeiHead, "課税金額合計２")
        kazeiHead = String.Concat(kazeiHead, SekyDate)
#If False Then      'UPD 2019/01/31 依頼番号 : 003976   【LMS】アクサルタFPDEデータ作成機能_PaymentTermの変更
        kazeiHead = String.Concat(kazeiHead, "K015120")
#Else
        kazeiHead = String.Concat(kazeiHead, "K013090")
#End If
        kazeiHead = String.Concat(kazeiHead, "")
        kazeiHead = String.Concat(kazeiHead, "      ")
        kazeiHead = String.Concat(kazeiHead, "   ")
        kazeiHead = String.Concat(kazeiHead, "      ")
        kazeiHead = String.Concat(kazeiHead, "   ")
        If akusarutaFlag = True Then
            '2019/11/25 要望管理009221 rep
            'kazeiHead = String.Concat(kazeiHead, "V3")
#If False Then  'UPD 2021/03/12
            kazeiHead = String.Concat(kazeiHead, "V4")
        Else
            kazeiHead = String.Concat(kazeiHead, "V4")
        End If

#Else
            kazeiHead = String.Concat(kazeiHead, "V4")  'V3 から　V4   UPD 2021/05/20 　020903
        Else
            kazeiHead = String.Concat(kazeiHead, "V3")
        End If

#End If
        kazeiHead = String.Concat(kazeiHead, "          ")
        kazeiHead = String.Concat(kazeiHead, " ")
        kazeiHead = String.Concat(kazeiHead, "N")

        Dim headFlg As Boolean = True '247レコード単位のヘッダ出力タイミングのフラグ
        Dim sumBOND As Decimal = 0
        Dim srcNo As String = String.Empty
        Dim recCnt As Integer = 0
        Dim chkFRB_CD As String = String.Empty
        Dim cntTotal As Integer = 0     ''ADD 2021/05/20  020903 

        For i As Integer = 0 To maxHIKAZEI
#If True Then   'ADD 2021/05/20  020903   【LMS】ケマーズ・アクサルタの特別荷主機能の編集
            chkFRB_CD = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("FRB_CD").ToString
            If sFRB_CD.ToString.Equals("BD00942") = True Then
                If chkFRB_CD.ToString.Equals("BD00942") = False Then
                    Continue For
                End If
            Else
                If chkFRB_CD.ToString.Equals("BD00942") = True Then
                    Continue For
                End If
            End If
            cntTotal = cntTotal + 1
#End If
            If headFlg = True Then
                setData.Replace("非課税金額合計１", MaeCoverData(Convert.ToString(sumBOND), "0", 14))
                setData.Replace("非課税金額合計２", MaeCoverData(Convert.ToString(sumBOND), "0", 14))
                '②各レコードごとのヘッダの出力
                setData.Append(recHead)
                setData.Append(vbNewLine)
                '③247レコードごとに出力するヘッダの出力
                setData.Append(hikazeiHead)
                setData.Append(vbNewLine)
                sumBOND = 0
                headFlg = False
            End If

            setData.Append("GL00")
            setData.Append("FPDE        ")
            setData.Append(sysDt.Rows(0).Item("SYS_DATE").ToString())
            setData.Append("40")
            setData.Append(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("ACCOUNT").ToString)
            setData.Append("         ")

            setData.Append(MaeCoverData(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("BOND").ToString, "0", 14))
            setData.Append(MaeCoverData(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("BOND").ToString, "0", 14))
            setData.Append("00000000000000")
            setData.Append("V1")

            If String.IsNullOrEmpty(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("COST_CENTER").ToString) = True Then
                setData.Append("          ")
            ElseIf Len(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("COST_CENTER").ToString) <> 10 Then
                setData.Append("          ")
            Else
                setData.Append(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("COST_CENTER").ToString)
            End If

            If Len(String.Concat(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("FRB_CD").ToString, "   ")) <> 10 Then
                setData.Append("          ")
            Else
                setData.Append(String.Concat(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("FRB_CD").ToString, "   "))
            End If

            setData.Append("    ")

            srcNo = String.Concat(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("SRC_NO").ToString, "      ")
            If ("0B").Equals(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("DEPART").ToString) = True AndAlso
                (String.Concat("9988", "      ")).Equals(srcNo) = True Then
                srcNo = String.Concat("9941", "      ")
            End If
            If Len(srcNo) <> 10 Then
                srcNo = "          "
            End If
            setData.Append(srcNo)

            setData.Append("              ")
            setData.Append("   ")

            If ("04").Equals(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("SEKY_KMK").ToString) = True AndAlso
                String.IsNullOrEmpty(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("DEST_CTY").ToString) = True Then
                setData.Append(String.Empty)
            ElseIf ("04").Equals(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("SEKY_KMK").ToString) = True AndAlso
                String.IsNullOrEmpty(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("DEST_CTY").ToString) = False Then
                setData.Append(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("DEST_CTY").ToString)
            Else
                setData.Append("JP")
            End If

            setData.Append(vbNewLine)

            sumBOND = sumBOND + Convert.ToDecimal(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_HIKAZEI).Rows(i).Item("BOND").ToString)

            recCnt = recCnt + 1
            If recCnt = 248 Then
                recCnt = 0
                headFlg = True
            End If

        Next
        setData.Replace("非課税金額合計１", MaeCoverData(Convert.ToString(sumBOND), "0", 14))
        setData.Replace("非課税金額合計２", MaeCoverData(Convert.ToString(sumBOND), "0", 14))
        sumBOND = 0

        Dim sumSOUND As Decimal = 0
        Dim sumVATAMOUNT As Decimal = 0

        headFlg = True
        recCnt = 0

        For i As Integer = 0 To maxKAZEI
#If True Then   'ADD 2021/05/20  020903   【LMS】ケマーズ・アクサルタの特別荷主機能の編集
            chkFRB_CD = ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("FRB_CD").ToString
            If sFRB_CD.ToString.Equals("BD00942") = True Then
                If chkFRB_CD.Equals("BD00942") = False Then
                    Continue For
                End If
            Else
                If chkFRB_CD.ToString.Equals("BD00942") = True Then
                    Continue For
                End If
            End If
            cntTotal = cntTotal + 1

#End If
            If headFlg = True Then
                setData.Replace("課税金額合計１", MaeCoverData(Convert.ToString(sumSOUND), "0", 14))
                setData.Replace("課税金額合計２", MaeCoverData(Convert.ToString(sumSOUND), "0", 14))
                '②各レコードごとのヘッダの出力
                setData.Append(recHead)
                setData.Append(vbNewLine)
                '⑤247レコードごとに出力するヘッダの出力
                setData.Append(kazeiHead)
                setData.Append(vbNewLine)
                sumSOUND = 0
                headFlg = False
            End If

            sumVATAMOUNT = sumVATAMOUNT +
                           Convert.ToDecimal(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("VAT_AMOUNT").ToString)

            setData.Append("GL00")
            setData.Append("FPDE        ")
            setData.Append(sysDt.Rows(0).Item("SYS_DATE").ToString())
            setData.Append("40")
            setData.Append(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("ACCOUNT").ToString)
            setData.Append("         ")
            setData.Append(MaeCoverData(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("SOUND").ToString, "0", 14))
            setData.Append(MaeCoverData(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("SOUND").ToString, "0", 14))
            If recCnt = 247 OrElse
                i = maxKAZEI Then
                '248レコード目の時は合計を設定。
                'recCnt = 247となっているが、recCntの加算が各レコードの終わった後なので、247の時が248レコード目
                setData.Append(MaeCoverData(Convert.ToString(sumVATAMOUNT), "0", 14))
                sumVATAMOUNT = 0
            Else
                setData.Append("00000000000000")
            End If
            If akusarutaFlag = True Then
                setData.Append("V4")    'V3 から　V4   UPD 2021/05/20 　020903
            Else
                setData.Append("V3")

            End If
            ''#If False Then  'UPD 2021/03/12
            ''                setData.Append("V4")
            ''            Else
            ''                setData.Append("V4")
            ''            End If

            ''#Else
            ''                setData.Append("V3")
            ''            Else
            ''                setData.Append("V3")
            ''            End If

            ''#End If

            If String.IsNullOrEmpty(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("COST_CENTER").ToString) = True Then
                setData.Append("          ")
            ElseIf Len(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("COST_CENTER").ToString) <> 10 Then
                setData.Append("          ")
            Else
                setData.Append(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("COST_CENTER").ToString)
            End If

            If Len(String.Concat(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("FRB_CD").ToString, "   ")) <> 10 Then
                setData.Append("          ")
            Else
                setData.Append(String.Concat(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("FRB_CD").ToString, "   "))
            End If

            setData.Append("    ")

            srcNo = String.Concat(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("SRC_NO").ToString, "      ")
            If ("0B").Equals(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("DEPART").ToString) = True AndAlso
                (String.Concat("9988", "      ")).Equals(srcNo) = True Then
                srcNo = String.Concat("9941", "      ")
            End If
            If Len(srcNo) <> 10 Then
                srcNo = "          "
            End If
            setData.Append(srcNo)

            setData.Append("              ")
            setData.Append("   ")

            If ("04").Equals(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("SEKY_KMK").ToString) = True AndAlso
                String.IsNullOrEmpty(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("DEST_CTY").ToString) = True Then
                setData.Append("   ")
            ElseIf ("04").Equals(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("SEKY_KMK").ToString) = True AndAlso
                String.IsNullOrEmpty(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("DEST_CTY").ToString) = False Then
                setData.Append(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("DEST_CTY").ToString)
            Else
                setData.Append("JP")
            End If

            setData.Append(vbNewLine)

            sumSOUND = sumSOUND +
                       Convert.ToDecimal(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("VAT_AMOUNT").ToString) +
                       Convert.ToDecimal(ds.Tables(LMI040C.TABLE_NM_CSV_FPDE_KAZEI).Rows(i).Item("SOUND").ToString)

            recCnt = recCnt + 1
            If recCnt = 248 Then
                recCnt = 0
                headFlg = True
            End If

        Next
        setData.Replace("課税金額合計１", MaeCoverData(Convert.ToString(sumSOUND), "0", 14))
        setData.Replace("課税金額合計２", MaeCoverData(Convert.ToString(sumSOUND), "0", 14))
        sumSOUND = 0

        '保存先のCSVファイルのパス
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C006' AND ",
                                                                                                        "KBN_CD = '", frm.cmbFile.SelectedValue, "'"))
#If False Then  'UPD 2021/05/20  020903   【LMS】ケマーズ・アクサルタの特別荷主機能の編集
        Dim csvPath As String = String.Concat(kbnDr(0).Item("KBN_NM2").ToString,
                                        kbnDr(0).Item("KBN_NM3").ToString,
                                        sysDt.Rows(0).Item("SYS_DATE").ToString(),
                                        sysDt.Rows(0).Item("SYS_TIME").ToString(),
                                        ".txt")

#Else

        If cntTotal = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {"ファイル作成処理", String.Empty})

            Exit Sub
        End If

        Dim csvPath As String = String.Empty

        If sFRB_CD.ToString.Equals("BD00942") = True Then
            csvPath = String.Concat(kbnDr(0).Item("KBN_NM2").ToString,
                                        kbnDr(0).Item("KBN_NM3").ToString,
                                        sysDt.Rows(0).Item("SYS_DATE").ToString(),
                                        sysDt.Rows(0).Item("SYS_TIME").ToString(),
                                        "_BD00942.txt")

        Else
            csvPath = String.Concat(kbnDr(0).Item("KBN_NM2").ToString,
                                        kbnDr(0).Item("KBN_NM3").ToString,
                                        sysDt.Rows(0).Item("SYS_DATE").ToString(),
                                        sysDt.Rows(0).Item("SYS_TIME").ToString(),
                                        "_Not_BD00942.txt")

        End If
#End If

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        'ファイルを開く
        System.IO.Directory.CreateDirectory(kbnDr(0).Item("KBN_NM2").ToString)
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {"ファイル作成処理", String.Empty})

    End Sub

    ''' <summary>
    ''' ダブルコーテーション付加
    ''' </summary>
    ''' <param name="val"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetDblQuotation(ByVal val As String) As String

        Return String.Concat("""", val, """")

    End Function

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

    'START YANAI 要望番号830
    ''' <summary>
    ''' 印刷種別変更によるロック制御
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ReCreatePrintTypeControl(ByVal frm As LMI040F)

        Dim kbnDr() As DataRow = Nothing
        Dim max As Integer = 0

        frm.cmbPrintType10.Visible = False
        frm.cmbPrintType11.Visible = False
        frm.cmbPrintType12.Visible = False
        frm.cmbPrintType13.Visible = False
        frm.cmbPrintType10.SelectedValue = Nothing
        frm.cmbPrintType11.SelectedValue = Nothing
        frm.cmbPrintType12.SelectedValue = Nothing
        frm.cmbPrintType13.SelectedValue = Nothing

        If ("02").Equals(frm.cmbPrint.SelectedValue) = True Then
            frm.cmbPrintType11.Visible = True
        ElseIf ("03").Equals(frm.cmbPrint.SelectedValue) = True Then
            frm.cmbPrintType12.Visible = True
        ElseIf ("04").Equals(frm.cmbPrint.SelectedValue) = True Then
            frm.cmbPrintType13.Visible = True
        Else
            frm.cmbPrintType10.Visible = True
        End If

    End Sub

    ''' <summary>
    ''' 印刷種別1変更によるロック制御
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ReCreatePrintType1Control(ByVal frm As LMI040F)

        frm.cmbPrintType20.Visible = False
        frm.cmbPrintType21.Visible = False
        frm.cmbPrintType20.SelectedValue = Nothing
        frm.cmbPrintType21.SelectedValue = Nothing

        If ("02").Equals(frm.cmbPrint.SelectedValue) = True Then
            frm.cmbPrintType21.Visible = True
        ElseIf ("03").Equals(frm.cmbPrint.SelectedValue) = True Then
            frm.cmbPrintType21.Visible = True
        ElseIf ("04").Equals(frm.cmbPrint.SelectedValue) = True Then
            frm.cmbPrintType20.Visible = True
        Else
            frm.cmbPrintType20.Visible = True
        End If

    End Sub
    'END YANAI 要望番号830

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInDataKensaku(ByVal frm As LMI040F, ByRef rtnDs As DataSet)

        Dim dr As DataRow = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("SEKY_YM") = Mid(frm.imdSeikyu.TextValue, 1, 6)
        dr("DEPART") = frm.cmbJigyoubu.SelectedValue
        dr("SEKY_KMK") = frm.cmbSeikyuKoumoku.SelectedValue
        dr("MISK_CD") = frm.cmbMisuku.SelectedValue
        dr("PRINT_KB") = frm.cmbPrint.SelectedValue
        dr("CSV_KB") = frm.cmbFile.SelectedValue

        'START YANAI 要望番号830
        If frm.cmbPrintType10.Visible = True Then
            dr("PRINT_TYPE1") = String.Empty
        ElseIf frm.cmbPrintType11.Visible = True Then
            dr("PRINT_TYPE1") = frm.cmbPrintType11.SelectedValue
        ElseIf frm.cmbPrintType12.Visible = True Then
            dr("PRINT_TYPE1") = frm.cmbPrintType12.SelectedValue
        ElseIf frm.cmbPrintType13.Visible = True Then
            dr("PRINT_TYPE1") = frm.cmbPrintType13.SelectedValue
        End If

        If frm.cmbPrintType20.Visible = True Then
            dr("PRINT_TYPE2") = String.Empty
        ElseIf frm.cmbPrintType21.Visible = True Then
            dr("PRINT_TYPE2") = frm.cmbPrintType21.SelectedValue
        End If

        dr("FRB_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI040G.sprDetailDef.FRBCD.ColNo))
        dr("SRC_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI040G.sprDetailDef.SRCCD.ColNo))
        dr("COST_CENTER") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI040G.sprDetailDef.COST.ColNo))
        dr("DEST_CTY") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI040G.sprDetailDef.DESTCITY.ColNo))
        dr("SYS_ENT_DATE") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI040G.sprDetailDef.SYSENTDATE.ColNo))
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
        dr("USER_BR_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI040G.sprDetailDef.NRSBRCD.ColNo))
        'END YANAI 要望番号830
        dr("MAIN_BR") = Me.GetMainBrCd()
        dr("SEIQTO_KBN") = frm.cmbSEIQTO_KBN.SelectedValue
        '検索条件をデータセットに設定
        rtnDs.Tables(LMI040C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(保存)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInDataHozon(ByVal frm As LMI040F, ByRef rtnDs As DataSet)

        Dim dr As DataRow = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo2.SelectedValue
        dr("SEKY_YM") = Mid(frm.imdTuki.TextValue, 1, 6)
        dr("DEPART") = frm.cmbJigyou.SelectedValue
        dr("SEKY_KMK") = frm.cmbSeikyukoumoku2.SelectedValue
        dr("FRB_CD") = frm.txtFrbcd.TextValue
        dr("SRC_CD") = frm.txtSrc.TextValue
        dr("COST_CENTER") = frm.txtCust.TextValue
        dr("MISK_CD") = frm.cmbMisk.SelectedValue
        dr("DEST_CTY") = frm.txtDestCity.TextValue
        dr("AMOUNT") = frm.numSeikyuKingaku.Value
        dr("SOUND") = frm.numKazei.Value
        dr("BOND") = frm.numHiKazei.Value
        dr("VAT_AMOUNT") = frm.numZeigaku.Value
        dr("JIDO_FLAG") = frm.lblAutoHide.TextValue
        dr("SHUDO_FLAG") = frm.lblShuDouHide.TextValue
        dr("PRINT_KB") = "01"
        dr("CSV_KB") = frm.cmbFile.SelectedValue

        dr("SEKY_YM_OLD") = Mid(frm.imdTukiHide.TextValue, 1, 6)
        dr("DEPART_OLD") = frm.cmbJigyouHide.SelectedValue
        dr("SEKY_KMK_OLD") = frm.cmbSeikyukoumoku2Hide.SelectedValue
        dr("FRB_CD_OLD") = frm.txtFrbcdHide.TextValue
        dr("SRC_CD_OLD") = frm.txtSrcHide.TextValue
        dr("COST_CENTER_OLD") = frm.txtCustHide.TextValue
        dr("MISK_CD_OLD") = frm.cmbMiskHide.SelectedValue
        dr("DEST_CTY_OLD") = frm.txtDestCityHide.TextValue
        dr("MAIN_BR") = Me.GetMainBrCd()

        '検索条件をデータセットに設定
        rtnDs.Tables(LMI040C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SetInDataDelete(ByVal frm As LMI040F, ByVal arr As ArrayList) As DataSet

        Dim rtnDs As DataSet = New LMI040DS()
        Dim dr As DataRow = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()
        Dim inTbl As DataTable = rtnDs.Tables(LMI040C.TABLE_NM_IN)
        Dim max As Integer = arr.Count - 1

        '別インスタンスのデータロウを空にする
        inTbl.Clear()

        For i As Integer = 0 To max

            dr = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()

            dr("NRS_BR_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.NRSBRCD.ColNo))
            dr("SEKY_YM_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.TUKI.ColNo)).Replace("/", "")
            dr("DEPART_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.JIGYOUCD.ColNo))
            dr("SEKY_KMK_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.SEIKYUCD.ColNo))
            dr("FRB_CD_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.FRBCD.ColNo))
            dr("SRC_CD_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.SRCCD.ColNo))
            dr("COST_CENTER_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.COST.ColNo))
            dr("MISK_CD_OLD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.MISKCD.ColNo))
            dr("MAIN_BR") = Me.GetMainBrCd()

            inTbl.Rows.Add(dr)

        Next

        Return rtnDs

    End Function

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SetInDataPrint(ByVal frm As LMI040F, ByVal arr As ArrayList) As DataSet

        Dim rtnDs As DataSet = New LMI040DS()
        Dim dr As DataRow = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()
        Dim inTbl As DataTable = rtnDs.Tables(LMI040C.TABLE_NM_IN)
        Dim max As Integer = arr.Count - 1

        '別インスタンスのデータロウを空にする
        inTbl.Clear()

        Select Case frm.cmbPrint.SelectedValue.ToString()
            Case LMI040C.PRINT_CHECK_LIST

                For i As Integer = 0 To max

                    dr = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()

                    dr("NRS_BR_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.NRSBRCD.ColNo))
                    dr("SEKY_YM") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.TUKI.ColNo)).Replace("/", "")
                    dr("DEPART") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.JIGYOUCD.ColNo))
                    dr("SEKY_KMK") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.SEIKYUCD.ColNo))
                    dr("FRB_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.FRBCD.ColNo))
                    dr("SRC_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.SRCCD.ColNo))
                    dr("COST_CENTER") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.COST.ColNo))
                    dr("MISK_CD") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMI040G.sprDetailDef.MISKCD.ColNo))
                    dr("PRINT_KB") = frm.cmbPrint.SelectedValue

                    'START YANAI 要望番号830
                    If frm.cmbPrintType10.Visible = True Then
                        dr("PRINT_TYPE1") = STRING.EMPTY
                    ElseIf frm.cmbPrintType11.Visible = True Then
                        dr("PRINT_TYPE1") = frm.cmbPrintType11.SelectedValue
                    ElseIf frm.cmbPrintType12.Visible = True Then
                        dr("PRINT_TYPE1") = frm.cmbPrintType12.SelectedValue
                    ElseIf frm.cmbPrintType13.Visible = True Then
                        dr("PRINT_TYPE1") = frm.cmbPrintType13.SelectedValue
                    End If

                    If frm.cmbPrintType20.Visible = True Then
                        dr("PRINT_TYPE2") = String.Empty
                    ElseIf frm.cmbPrintType21.Visible = True Then
                        dr("PRINT_TYPE2") = frm.cmbPrintType21.SelectedValue
                    End If
                    'END YANAI 要望番号830
                    dr("MAIN_BR") = Me.GetMainBrCd()

                    inTbl.Rows.Add(dr)

                Next

            Case LMI040C.PRINT_SEKY_KAGAMI, LMI040C.PRINT_SEKY_SHUKEI, LMI040C.PRINT_SEKY_SHUKEIKEIRI

                dr = rtnDs.Tables(LMI040C.TABLE_NM_IN).NewRow()

                dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                dr("SEKY_YM") = Mid(frm.imdSeikyu.TextValue, 1, 6)
                dr("DEPART") = frm.cmbJigyoubu.SelectedValue
                dr("SEKY_KMK") = frm.cmbSeikyuKoumoku.SelectedValue
                dr("MISK_CD") = frm.cmbMisuku.SelectedValue
                dr("PRINT_KB") = frm.cmbPrint.SelectedValue
                'START YANAI 要望番号830
                If frm.cmbPrintType10.Visible = True Then
                    dr("PRINT_TYPE1") = String.Empty
                ElseIf frm.cmbPrintType11.Visible = True Then
                    dr("PRINT_TYPE1") = frm.cmbPrintType11.SelectedValue
                ElseIf frm.cmbPrintType12.Visible = True Then
                    dr("PRINT_TYPE1") = frm.cmbPrintType12.SelectedValue
                ElseIf frm.cmbPrintType13.Visible = True Then
                    dr("PRINT_TYPE1") = frm.cmbPrintType13.SelectedValue
                End If

                If frm.cmbPrintType20.Visible = True Then
                    dr("PRINT_TYPE2") = String.Empty
                ElseIf frm.cmbPrintType21.Visible = True Then
                    dr("PRINT_TYPE2") = frm.cmbPrintType21.SelectedValue
                End If
                'END YANAI 要望番号830

                dr("SEIQTO_KBN") = frm.cmbSEIQTO_KBN.SelectedValue

                dr("MAIN_BR") = Me.GetMainBrCd()
                inTbl.Rows.Add(dr)

        End Select

        Return rtnDs

    End Function

    ''' <summary>
    ''' デュポン業務主営業所コードの取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMainBrCd() As String

        Dim mainBrRr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'D018' AND KBN_CD ='00'"))

        Return mainBrRr(0).Item("KBN_NM1").ToString()

    End Function

#End Region

#Region "外部メソッド"

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ServerAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Dim rtnDs As DataSet = MyBase.CallWSA("LMI040BLF", actionId, ds)

        Return rtnDs

    End Function

#End Region

#End Region 'Method

End Class
