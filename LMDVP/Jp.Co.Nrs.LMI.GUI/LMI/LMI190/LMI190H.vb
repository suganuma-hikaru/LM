' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI190  : ハネウェル管理
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Microsoft.Office.Interop

''' <summary>
''' LMI190ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI190H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI190V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI190G

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
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMI190DS

    ''' <summary>
    '''編集モード
    ''' </summary>
    ''' <remarks></remarks>
    Private _EditModeFlg As Boolean

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

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
        Dim frm As LMI190F = New LMI190F(Me)

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
        Me._G = New LMI190G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI190V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMI190G.MODE_SHOKI)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定 & 初期値設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        '数値コントロールの書式設定
        Call Me._G.SetNumberControl()

        'シリンダタイプコンボの作成
        Call Me.GetCylinder(frm)

        '在庫場所コンボの作成
        Call Me.GetToFromNm(frm)

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI190C.EventShubetsu, ByVal frm As LMI190F)

        'パラメータクラス生成
        Dim rtnDs As DataSet = Nothing
        Dim errDs As DataSet = Nothing
        Dim errHashTable As Hashtable = New Hashtable

        '保存処理なら編集中の値を確定する
        If eventShubetsu.Equals(LMI190C.EventShubetsu.HOZON) = True Then
            'Call Me.SetShipNm(frm, frm.sprDetails.ActiveSheet.ActiveRowIndex)
        End If

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        If eventShubetsu.Equals(LMI190C.EventShubetsu.MASTEROPEN) = False Then
            '処理開始アクション
            Me._LMIConH.StartAction(frm)
        End If

        '入力チェック
        If Me._V.IsSingleCheck(eventShubetsu) = False OrElse
           Me._V.IsKanrenCheck(eventShubetsu) = False Then
            '処理終了アクション
            Me._LMIConH.EndAction(frm, "G006")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI190C.EventShubetsu.GETDATA    'データ取得

                'データ取得処理
                Call Me.GetDataMain(frm)

                'Case LMI190C.EventShubetsu.HENSHU    '編集(F2)

                '    'Spreadを編集モードに変更
                '    Call Me._G.SetModeStatus(LMI190G.MODE_EDIT)

                '    'Functionキー設定
                '    Call Me._G.SetFunctionKey(LMI190G.MODE_EDIT)

                '----------猪熊-----------------------------------------------------------
                'Case LMI190C.EventShubetsu.SUZUSHO     '鈴商(F3)

                '    Call Me.ShowLMI200(frm)

            Case LMI190C.EventShubetsu.IMPORT_N40CD     'N40コード取込(F3)

                Call Me.ImportN40Code(frm)


                'Case LMI190C.EventShubetsu.HENKHAKU    '返却(F4)
            Case LMI190C.EventShubetsu.HENKYAKUSHUKKA   '返却／出荷(F4)

                Call Me.ShowLMI210_RETURN_OUTKA(frm)

                'Case LMI190C.EventShubetsu.SHUKKA       '出荷(F5)

                'Call Me.ShowLMI210_OUTKA(frm)

                'Case LMI190C.EventShubetsu.GETLOG      '取得ログ(F6)
            Case LMI190C.EventShubetsu.GETLOG      '取得ログ(F5)

                Call Me.ShowLMI240(frm)
                '----------猪熊-----------------------------------------------------------

            Case LMI190C.EventShubetsu.HAIKI,
                 LMI190C.EventShubetsu.HAIKIKAIJO    '廃棄,廃棄解除

                Dim kaijoFlg As Boolean = True
                If eventShubetsu.Equals(LMI190C.EventShubetsu.HAIKI) = True Then
                    kaijoFlg = False
                End If

                'チェックリスト取得
                Me._ChkList = Me._V.GetCheckList()

                '関連チェック
                If Me.IsHaikiKanrenCheck(frm, kaijoFlg) = False Then

                    'メッセージコードの判定
                    If MyBase.IsMessageStoreExist = True Then
                        MyBase.ShowMessage(frm, "E235")
                        'EXCEL起動()
                        MyBase.MessageStoreDownload()

                        '処理終了アクション
                        Me._LMIConH.EndAction(frm, "G006")

                        Exit Sub
                    End If

                End If

                'メイン処理
                Call Me.HaikiMain(frm, kaijoFlg)

            Case LMI190C.EventShubetsu.KENSAKU    '検索(F9)

                '検索処理
                Call Me.KensakuMain(frm)

            Case LMI190C.EventShubetsu.TEIKIKENSAKANRI    '定期検査管理(F8)

                Call Me.ShowLMI220(frm)

            Case LMI190C.EventShubetsu.MASTEROPEN    'マスタ参照(F10)

                'Call Me.ShowLMI220(frm)
                Call Me.OpenMasterPop(frm)

            Case LMI190C.EventShubetsu.HOZON    '保存(F11)

                '---------猪熊
                'If Me._V.IsHenkoSingleCheck() = False Then
                'Call Me._LMEconH.EndAction(frm) '終了処理
                'Exit Sub
                'End If
                '---------猪熊

                '保存処理
                'Call Me.IkkatsuHenko(frm, errHashtable, errDs)
                Call Me.HozonMain(frm)

            Case LMI190C.EventShubetsu.EXCEL     'Excel出力

                'NRC出荷／回収情報Excel作成処理呼び出し
                Call Me.ShowExcelLMI900(frm)

        End Select

        If eventShubetsu.Equals(LMI190C.EventShubetsu.MASTEROPEN) = False Then
            '処理終了アクション
            Me._LMIConH.EndAction(frm, "G006")
        End If

    End Sub

    ''' <summary>
    ''' 「閉じる」処理時の保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function HozonActionForClose(ByVal frm As LMI190F) As Boolean

        Dim rtnFlg As Boolean = True
        Dim eventShubetsu As LMI190C.EventShubetsu = LMI190C.EventShubetsu.HOZON
        Dim errDs As DataSet = Nothing
        Dim errHashTable As Hashtable = New Hashtable

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Return False
        End If

        '処理開始アクション
        Me._LMIConH.StartAction(frm)

        '入力チェック
        If Me._V.IsSingleCheck(eventShubetsu) = False OrElse
           Me._V.IsKanrenCheck(eventShubetsu) = False Then
            '処理終了アクション
            Me._LMIConH.EndAction(frm, "G006")
            Return False
        End If

        '保存処理
        rtnFlg = Me.HozonMain(frm)

        '処理終了アクション
        Me._LMIConH.EndAction(frm, "G006")

        Return rtnFlg

    End Function

    ''' <summary>
    ''' オプションボタン変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeOptionBtn(ByVal frm As LMI190F)

        'If LMI190G.MODE_EDIT.Equals(frm.txtMode.TextValue) = True Then
        '    '編集モードの場合

        '    '編集モード解除 & モード情報再格納
        '    frm.txtMode.TextValue = Me.GetModeFlg(frm)
        'Else
        '    '編集モード以外の場合

        'コントロール値のクリア
        Call Me._G.ClearControl()
        'End If

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMI190G.MODE_SHOKI)

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMI190F, ByVal e As FormClosingEventArgs)

        ''編集モード以外なら処理終了
        'If LMI190G.MODE_EDIT.Equals(frm.txtMode.TextValue) = False Then
        '    Exit Sub
        'End If

        ''メッセージの表示
        'Select Case MyBase.ShowMessage(frm, "W002")

        '    Case MsgBoxResult.Yes '「はい」押下時

        '        '保存処理を行う
        '        If Me.HozonActionForClose(frm) = False Then
        '            e.Cancel = True
        '            Exit Sub
        '        End If

        '        '処理終了
        '        Exit Sub

        '    Case MsgBoxResult.Cancel '「キャンセル」押下時
        '        '処理キャンセル
        '        e.Cancel = True

        'End Select

    End Sub

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Me.ActionControl(LMI190C.EventShubetsu.GETDATA, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        Me.ActionControl(LMI190C.EventShubetsu.HENSHU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        Me.ActionControl(LMI190C.EventShubetsu.IMPORT_N40CD, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        'Me.ActionControl(LMI190C.EventShubetsu.HENKHAKU, frm)
        Me.ActionControl(LMI190C.EventShubetsu.HENKYAKUSHUKKA, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub
    '----------------------------------------------------------------------------------------

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey5Press")

        'Me.ActionControl(LMI190C.EventShubetsu.SHUKKA, frm)
        Me.ActionControl(LMI190C.EventShubetsu.GETLOG, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey5Press")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey6Press")

        'Me.ActionControl(LMI190C.EventShubetsu.GETLOG, frm)
        Me.ActionControl(LMI190C.EventShubetsu.HAIKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey6Press")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey7Press")

        'Me.ActionControl(LMI190C.EventShubetsu.HAIKI, frm)
        Me.ActionControl(LMI190C.EventShubetsu.HAIKIKAIJO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey7Press")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey8Press")

        'Me.ActionControl(LMI190C.EventShubetsu.HAIKIKAIJO, frm)
        Me.ActionControl(LMI190C.EventShubetsu.TEIKIKENSAKANRI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey8Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Me.ActionControl(LMI190C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(TODO)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'Me.ActionControl(LMI190C.EventShubetsu.TEIKIKENSAKANRI, frm)
        Me.ActionControl(LMI190C.EventShubetsu.MASTEROPEN, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Me.ActionControl(LMI190C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' Excel出力処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnExcelClick(ByVal frm As LMI190F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnExcelClick")

        Me.ActionControl(LMI190C.EventShubetsu.EXCEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnExcelClick")

    End Sub

    ''' <summary>
    ''' 一括変更ボタン
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub btnAllChangeClick(ByVal frm As LMI190F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnAllChangeClick")

        Me.ActionControl(LMI190C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnAllChangeClick")

    End Sub


    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMI190F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' ラジオボタンチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub optButtomChange(ByRef frm As LMI190F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "optButtomChange")

        Call Me.ChangeOptionBtn(frm)

        Logger.EndLog(Me.GetType.Name, "optButtomChange")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' データ取得処理 (F1)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub GetDataMain(ByVal frm As LMI190F)

        'データセット設定
        Dim ds As DataSet = Me.SetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "GetDataMain")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "GetDataMain", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "GetDataMain")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'シリンダタイプコンボの作成
        Call Me.GetCylinder(frm)

        '在庫場所コンボの作成
        Call Me.GetToFromNm(frm)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"データ取得処理", ""})

    End Sub

    ''' <summary>
    ''' シリンダタイプコンボの値を取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub GetCylinder(ByVal frm As LMI190F)

        'データセット設定
        Dim ds As DataSet = SetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCylinder")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "SelectCylinder", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCylinder")

        'シリンダタイプコンボの作成
        Me._G.CreateCylinderCombo(rtnDs)

    End Sub

    ''' <summary>
    ''' 在庫場所コンボの値を取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub GetToFromNm(ByVal frm As LMI190F)

        'データセット設定
        Dim ds As DataSet = SetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectToFromNm")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "SelectToFromNm", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectToFromNm")

        '在庫場所コンボの作成
        Me._G.CreateToFromNmCombo(rtnDs)

    End Sub

    ''' <summary>
    ''' 画面モード取得
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetModeFlg(ByVal frm As LMI190F) As String

        Dim modeFlg As String = String.Empty

        If frm.optZaiko.Checked = True Then
            modeFlg = LMI190G.MODE_ZAI
        ElseIf frm.optRireki.Checked = True Then
            modeFlg = LMI190G.MODE_RIREKI
        ElseIf frm.optHaiki.Checked = True Then
            modeFlg = LMI190G.MODE_HAIKI
        End If

        Return modeFlg

    End Function

    ''' <summary>
    ''' 検索処理 (F9)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KensakuMain(ByVal frm As LMI190F)

        '一覧クリア
        frm.sprDetails.CrearSpread()

        'データセット設定
        Dim ds As DataSet = Me.SetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KensakuMain")

        '閾値の設定
        MyBase.SetLimitCount(Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                      (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))))

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble(
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)

        '検索データ取得
        'Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form) _
        '                                               , "LMI190BLF", blfNm, ds _
        '                                               , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
        '                                               , Convert.ToInt32(Convert.ToDouble( _
        '                                                 MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                                 .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        '------------------------------------------------------------------------------------------------------------
        '==========================
        'WSAクラス呼出
        '==========================
        Dim blfNm As String = String.Empty
        If frm.optZaiko.Checked = True Then
            '在庫検索の場合
            blfNm = "SelectKensakuCountZaiko"
        ElseIf frm.optRireki.Checked = True Then
            '履歴検索の場合
            blfNm = "SelectKensakuCountRireki"
        ElseIf frm.optHaiki.Checked = True Then
            '廃棄済検索の場合
            blfNm = "SelectKensakuCountHaiki"
        End If

        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", blfNm, ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    '==========================
                    'WSAクラス呼出
                    '==========================
                    Dim blfNm2 As String = String.Empty
                    If frm.optZaiko.Checked = True Then
                        '在庫検索の場合
                        blfNm2 = "SelectKensakuZaikoData"
                    ElseIf frm.optRireki.Checked = True Then
                        '履歴検索の場合
                        blfNm2 = "SelectKensakuRirekiData"
                    ElseIf frm.optHaiki.Checked = True Then
                        '廃棄済検索の場合
                        blfNm2 = "SelectKensakuHaikiData"
                    End If

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA("LMI190BLF", blfNm2, ds)

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                    ''取得し値のマージ
                    'shelterDs = Me.SetrtnDs(shelterDs, rtnDs)

                    ''メッセージエリアの設定
                    'MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '画面解除
                    MyBase.UnLockedControls(frm)

                    ''処理終了アクション
                    'Call Me.EndAction(frm)
                    Exit Sub

                End If

            Else

                'メッセージエリアの設定(0件エラー)
                MyBase.ShowMessage(frm)

                '次処理にて保持している値のみ表示

            End If

        End If
        '------------------------------------------------------------------------------------------------------------

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KensakuMain")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        ''エラーがある場合、メッセージ表示
        'If MyBase.IsMessageExist() = True Then
        '    MyBase.ShowMessage(frm)
        '    Exit Sub
        'End If

        Me._Ds = rtnDs

        '検索件数が0件の場合、メッセージ設定
        If Me._Ds.Tables(LMI190C.TABLE_NM_OUT).Rows.Count = 0 Then
            MyBase.ShowMessage(frm, "G001")
            Call Me._G.SetFunctionKey(LMI190G.MODE_SHOKI)
            Exit Sub
        End If


        'スプレッドに検索結果を設定
        If frm.optZaiko.Checked = True Then
            '在庫検索の場合
            'Call Me._G.SetSpreadAll(frm.sprDetails, rtnDs, MyBase.GetSystemDateTime(0))

            Call Me._G.SetSpreadAll(frm.sprDetails, rtnDs, MyBase.GetSystemDateTime(0), LMI190G.MODE_ZAI)
            Call Me._G.SetSprChange(LMI190G.MODE_ZAI)
        ElseIf frm.optRireki.Checked = True Then
            '履歴検索の場合
            'Call Me._G.SetSpreadAll(frm.sprDetails, rtnDs, MyBase.GetSystemDateTime(0))

            Call Me._G.SetSpreadAll(frm.sprDetails, rtnDs, MyBase.GetSystemDateTime(0), LMI190G.MODE_RIREKI)
            Call Me._G.SetSprChange(LMI190G.MODE_RIREKI)
        Else
            '廃棄済検索の場合
            Call Me._G.SetSpreadAll(frm.sprDetails, rtnDs, MyBase.GetSystemDateTime(0), LMI190G.MODE_HAIKI)
            Call Me._G.SetSprChange(LMI190G.MODE_HAIKI)
        End If

        'ファンクションキー設定
        Call Me._G.SetFunctionKey(Me.GetModeFlg(frm))

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})

    End Sub

    ''' <summary>
    ''' 廃棄・廃棄解除処理 (F7,F8)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="kaijoFlg">true:廃棄解除処理  false:廃棄処理</param>
    ''' <remarks></remarks>
    Private Sub HaikiMain(ByVal frm As LMI190F, ByVal kaijoFlg As Boolean)

        'INデータセット設定
        Dim ds As DataSet = Me.SetSpreadInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HaikiMain")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blfNm As String = "UpdateHaikiData"
        Dim setStr As String = "廃棄"
        Dim titleStr As String = "廃棄処理"

        If kaijoFlg = True Then
            '廃棄解除処理
            blfNm = "UpdateHaikiKaijoData"
            setStr = String.Empty
            titleStr = "廃棄解除処理"
        End If

        '処理実行
        'Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), "LMI190BLF", blfNm, ds)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", blfNm, ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HaikiMain")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '画面再描画
        Dim rowNo As Integer = 0
        For i As Integer = 0 To Me._ChkList.Count - 1
            rowNo = Convert.ToInt32(Me._ChkList(i))
            frm.sprDetails.SetCellValue(rowNo, LMI190G.sprDetailsAll.HAIKIYN.ColNo, setStr)
        Next

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {titleStr, ""})

    End Sub

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function HozonMain(ByVal frm As LMI190F) As Boolean

        '続行確認
        Dim rtn As MsgBoxResult

        If frm.cmbEditList.SelectedValue.ToString.Equals(LMI190C.EDIT_SELECT_SHIPCD) Then
            If frm.txtShipCd.TextValue.Trim().Equals(String.Empty) = True Then
                rtn = Me.ShowMessage(frm, "C011", New String() {"一括変更"})
            Else
                rtn = Me.ShowMessage(frm, "C001", New String() {"一括変更"})
            End If
        Else
            If frm.txtShipNm.TextValue.Trim().Equals(String.Empty) = True Then
                rtn = Me.ShowMessage(frm, "C011", New String() {"一括変更"})
            Else
                rtn = Me.ShowMessage(frm, "C001", New String() {"一括変更"})
            End If
        End If

        If rtn = MsgBoxResult.Ok Then
            ''エラーをExcelに出力
            'If errDs.Tables("LMI190_GUIERROR").Rows.Count <> 0 Then
            '    Call Me.ExcelErrorSet(errDs)
            'End If
            'メッセージコードの判定
            If MyBase.IsMessageStoreExist = True Then
                MyBase.ShowMessage(frm, "G007")
                'EXCEL起動()
                MyBase.MessageStoreDownload()
                Exit Function
            End If

        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Function
        End If

        'INデータセット設定
        'Dim ds As DataSet = Me.SetEditSpreadInData(frm)
        Dim ds As DataSet = Me.SetDataHenkoKey(frm)
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HozonMain")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blfNm As String = "UpdateHozonData"

        '処理実行
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", blfNm, ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HozonMain")

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Return False
        End If

        'エラーがある場合、メッセージ表示
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        '画面再描画
        'Dim rowNo As Integer = 0
        'Dim setStr As String = String.Empty
        'For i As Integer = 0 To Me._ChkList.Count - 1
        '    rowNo = Convert.ToInt32(Me._ChkList(i))
        '    setStr = frm.sprDetails.ActiveSheet.Cells(rowNo, LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo).Text
        '    frm.sprDetails.SetCellValue(rowNo, LMI190G.sprDetailsRireki.SHIPCDL.ColNo, setStr)
        'Next

        '編集モードを解除する
        'Call Me._G.SetModeStatus(LMI190G.MODE_RIREKI)
        Call Me._G.SetFunctionKey(LMI190G.MODE_RIREKI)

        'メッセージエリアの設定
        'MyBase.ShowMessage(frm, "G002", New String() {"保存処理", ""})
        Call Me.SuccessHenko(frm)

        Return True

    End Function

    ''' <summary>
    ''' Excel作成(LMI900)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowExcelLMI900(ByVal frm As LMI190F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'InDataSet設定
        Dim prmDs As DataSet = Me.SetLMI900InDataSet(frm)
        prm.ParamDataSet = prmDs

        'Excel作成処理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMI900", prm)

        If prm.ReturnFlg = False Then
            'メッセージエリアの設定
            MyBase.SetMessage("E501")
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub
    ''' <summary>
    ''' 一括変更処理（未使用）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub IkkatsuHenko(ByVal frm As LMI190F, ByVal errHashtable As Hashtable, ByVal errDs As DataSet)

        '続行確認
        Dim rtn As MsgBoxResult

        rtn = Me.ShowMessage(frm, "C001", New String() {"一括変更"})

        If rtn = MsgBoxResult.Ok Then
            ''エラーをExcelに出力
            'If errDs.Tables("LMI190_GUIERROR").Rows.Count <> 0 Then
            '    Call Me.ExcelErrorSet(errDs)
            'End If
            'メッセージコードの判定
            If MyBase.IsMessageStoreExist = True Then
                MyBase.ShowMessage(frm, "G007")
                'EXCEL起動()
                MyBase.MessageStoreDownload()
                Exit Sub
            End If

        ElseIf rtn = MsgBoxResult.Cancel Then
            Me.ShowMessage(frm, "G007")
            Exit Sub
        End If

        'DataSet設定
        Dim rtDs As DataSet = New LMI190DS()
        Call Me.SetDataHenkoKey(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "Henko")
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMI190BLF", "UpdateHozonData", rtDs)

        'メッセージ情報を初期化する
        'MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            Call Me.OutputExcel(frm)
            Call Me._LMIConH.EndAction(frm)
            Exit Sub
        Else
            Call Me.SuccessHenko(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "Henko")

        Call Me._LMIConH.EndAction(frm)

    End Sub

    '---------猪熊-------------------------------------------------------
    '''' <summary>
    '''' 鈴商 (F3)：ハネウェル管理（鈴木商会）画面を開く (LMI200)
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Private Sub ShowLMI200(ByVal frm As LMI190F)

    '    'パラメータクラス生成
    '    Dim prm As LMFormData = New LMFormData()

    '    'ハネウェル管理（鈴木商会）画面を開く
    '    LMFormNavigate.NextFormNavigate(Me, "LMI200", prm)

    'End Sub

    ''' <summary>
    ''' 返却／出荷 (F4)：シリンダ在庫画面を開く (LMI210)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowLMI210_RETURN_OUTKA(ByVal frm As LMI190F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'DataSet設定
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        'TODO：パラメータ確定後、完成
        'prmDs = New LMI210DS
        'row = prmDs.Tables(LMI210C.TABLE_NM_IN).NewRow
        'row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString

        'prmDs.Tables(LMI210C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'シリンダ在庫画面を開く
        LMFormNavigate.NextFormNavigate(Me, "LMI210", prm)

    End Sub

    '''' <summary>
    '''' 出荷 (F5)：シリンダ在庫画面を開く (LMI210)
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Private Sub ShowLMI210_OUTKA(ByVal frm As LMI190F)

    '    'パラメータクラス生成
    '    Dim prm As LMFormData = New LMFormData()

    '    'DataSet設定
    '    Dim prmDs As DataSet = Nothing
    '    Dim row As DataRow = Nothing

    '    'TODO：パラメータ確定後、完成
    '    'prmDs = New LMI210DS
    '    'row = prmDs.Tables(LMI210C.TABLE_NM_IN).NewRow
    '    'row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString

    '    'prmDs.Tables(LMI210C.TABLE_NM_IN).Rows.Add(row)
    '    prm.ParamDataSet = prmDs

    '    'シリンダ在庫画面を開く
    '    LMFormNavigate.NextFormNavigate(Me, "LMI210", prm)

    'End Sub
    '---------------------------------------------------------------------------------
    ''' <summary>
    ''' 取得ログ (F5)：データ取得ログ画面を開く (LMI240)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowLMI240(ByVal frm As LMI190F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'シリンダ在庫画面を開く
        LMFormNavigate.NextFormNavigate(Me, "LMI240", prm)

    End Sub

    ''' <summary>
    ''' 定期検査管理 (F10)：定期検査管理画面を開く (LMI220)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowLMI220(ByVal frm As LMI190F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'シリンダ在庫画面を開く
        LMFormNavigate.NextFormNavigate(Me, "LMI220", prm)

    End Sub

    ''' <summary>
    ''' 一覧ダブルクリック：履歴表示画面を開く (LMI230)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowLMI230(ByVal frm As LMI190F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'DataSet設定
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        'TODO：パラメータ確定後、完成
        'prmDs = New LMI230DS
        'row = prmDs.Tables(LMI230C.TABLE_NM_IN).NewRow
        'row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString

        'prmDs.Tables(LMI230C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        '履歴表示画面を開く
        LMFormNavigate.NextFormNavigate(Me, "LMI230", prm)

    End Sub

    Friend Sub cmbEditList_SelectedValueChanged(ByVal frm As LMI190F)

        '値のクリア
        Call Me.ClearControl(frm)

        'コントロールの設定
        Call Me._G.SetControl(frm)

    End Sub

    '''' <summary>
    '''' キャッシュから「荷送人名」を取得してスプレッドに設定する
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <param name="rowNo"></param>
    '''' <remarks></remarks>
    'Friend Sub SetShipNm(ByVal frm As LMI190F, ByVal rowNo As Integer)

    '    Dim destNm As String = String.Empty
    '    Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString()
    '    Dim destCd As String = frm.sprDetails.ActiveSheet.Cells(rowNo, LMI190G.sprDetailsRireki.SHIPCDL_EDIT.ColNo).Text
    '    Dim custCd As String = frm.sprDetails.ActiveSheet.Cells(rowNo, LMI190G.sprDetailsRireki.CUSTCDL.ColNo).Text

    '    '荷送人名称を取得
    '    '-------
    '    'Dim destDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select( _
    '    '                          String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND CUST_CD_L = '", custCd, "' AND DEST_CD = '", destCd, "' "))
    '    Dim destMstDs As MDestDS = New MDestDS
    '    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
    '    destMstDr.Item("NRS_BR_CD") = nrsBrCd
    '    destMstDr.Item("CUST_CD_L") = custCd
    '    destMstDr.Item("DEST_CD") = destCd
    '    destMstDr.Item("SYS_DEL_FLG") = "0"
    '    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
    '    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
    '    Dim destDr As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select

    '    '-------
    '    If 0 < destDr.Length Then
    '        destNm = destDr(0).Item("DEST_NM").ToString()
    '    End If

    '    'スプレッドに名称を設定
    '    frm.sprDetails.SetCellValue(rowNo, LMI190G.sprDetailsRireki.SHIPNML.ColNo, destNm)

    'End Sub

    ''' <summary>
    ''' 廃棄・廃棄解除処理時の関連チェック
    ''' </summary>
    ''' <param name="kaijoFlg">True：廃棄解除 False：廃棄</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHaikiKanrenCheck(ByVal frm As LMI190F, ByVal kaijoFlg As Boolean) As Boolean

        Dim rtnFlg As Boolean = True

        If Me._ChkList.Count = 0 Then
            MyBase.ShowMessage(frm, "E009")
            Return False
        End If

        '廃棄区分チェック
        Dim colNo As Integer = LMI190C.SprColumnIndexSPRALL.HAIKIYN

        With frm.sprDetails.ActiveSheet

            Dim max As Integer = Me._ChkList.Count - 1
            Dim rowNo As Integer = 0
            Dim str1 As String = String.Empty
            Dim str2 As String = String.Empty

            If kaijoFlg = True Then
                str1 = "廃棄されていない"
                str2 = "廃棄解除"
            Else
                str1 = "廃棄済"
                str2 = "廃棄"
            End If

            For i As Integer = 0 To max
                rowNo = Convert.ToInt32(Me._ChkList(i))
                If String.IsNullOrEmpty(Me._LMIconV.GetCellValue(.Cells(rowNo, colNo))) = kaijoFlg Then
                    MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E513", New String() {str1, str2, String.Concat((rowNo + 1).ToString(), "行目")}, (rowNo + 1).ToString())
                    rtnFlg = False
                End If
            Next

        End With

        Return rtnFlg

    End Function

#Region "N40コード取込処理"

    ''' <summary>
    ''' N40コード取込
    ''' </summary>
    ''' <returns></returns>
    Private Function ImportN40Code(ByVal frm As LMI190F) As Boolean

        Dim filePath As String = ""
        'Excelファイルパス取得
        If Not GetFilePathInteractive(filePath) Then
            'ユーザがキャンセルした場合
            Return False
        End If

        Dim dsImp As DataSet = New LMI190DS
        Dim listDtNm As List(Of String) = New List(Of String)
        Dim impRowCnt As Integer = 0
        'Excel取込
        If Not RoladN40CodeExcel(frm, filePath, dsImp, listDtNm, impRowCnt) Then
            'ファイルオープンに失敗した場合
            Return False
        End If

        Dim loopCnt As Integer = 0          'ループ回数
        Dim processedCnt As Integer = 0     '処理済み件数

        'Excelデータを取り込んだDataTableごとに処理
        For Each dtNm As String In listDtNm
            loopCnt += 1

            Dim ds As LMI190DS = New LMI190DS
            ds.Tables("LMI190IN_N40_CHG").Merge(dsImp.Tables(dtNm))

            Dim logString As String = $"({loopCnt}/{listDtNm.Count}回目)"
            MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, logString)
            'WSAクラス呼出
            Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "ImportN40Code", ds)
            MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name, logString)

            If MyBase.IsMessageExist Then
                MyBase.ShowMessage(frm)
                Return False
            End If

            '処理済み件数表示
            processedCnt += ds.Tables("LMI190IN_N40_CHG").Rows.Count
            MyBase.ShowMessage(frm, "G057", {$"{frm.FunctionKey.F3ButtonName}の", $"{impRowCnt}件中{processedCnt}件を処理済み"})  '[%1]処理中です。[%2]
            frm.Refresh()
        Next

        MyBase.ShowMessage(frm, "G002", {frm.FunctionKey.F3ButtonName, $"{processedCnt}件を処理しました。"})  '[%1]を完了しました。[%2]

    End Function

    ''' <summary>
    ''' 対話型Excelファイルパス取得
    ''' </summary>
    ''' <param name="outFilePath">ユーザが選択したファイルのパス</param>
    ''' <returns>True:取得成功 / False:取得失敗(ユーザがキャンセル)</returns>
    Private Function GetFilePathInteractive(ByRef outFilePath As String) As Boolean

        Using ofd As New OpenFileDialog
            ofd.Title = "取込ファイルを選択してください"
            ofd.Filter = "Excelファイル (*.xlsx;*.xls)|*.xlsx;*.xls"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() = DialogResult.OK Then
                outFilePath = ofd.FileName
                Return True
            End If
        End Using

        Return False

    End Function

    ''' <summary>
    ''' N40コードExcel読込
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="filePath">読込ファイルパス</param>
    ''' <param name="dsImp">(出力) Excelデータを取り込んだDataSet</param>
    ''' <param name="listDtNm">(出力) Excelデータを取り込んだDataTable名</param>
    ''' <param name="impRowCnt">(出力) 取込件数</param>
    ''' <returns>Ture:読込成功 / False:ファイルオープン失敗</returns>
    Private Function RoladN40CodeExcel(ByVal frm As LMI190F,
                                       ByVal filePath As String,
                                       ByVal dsImp As DataSet,
                                       ByVal listDtNm As List(Of String),
                                       ByRef impRowCnt As Integer
                                       ) As Boolean

        'ファイル存在チェック
        If Not System.IO.File.Exists(filePath) Then
            MyBase.ShowMessage(frm, "E395", {filePath})  'ファイルが見つかりません。[%1]
            Return False
        End If

        Const DetailsStartRow As Integer = 3        '明細の開始行
        Const NumOfColInFileLayout As Integer = 35  'ファイルレイアウト上の列数
        Const ColNoSerial As Integer = 1            'Serial Number列
        Const ColNoBarcode As Integer = 2           'Barcode列

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        Try
            'EXCEL開始
            xlApp = New Excel.Application
            xlApp.Visible = False
            xlApp.DisplayAlerts = False
            xlBooks = xlApp.Workbooks

            Try
                'リンクを更新しない、読み取り専用
                xlBook = xlBooks.Open(filePath, 0, True)
            Catch ex As Exception
                MyBase.ShowMessage(frm, "E547", {"Excelでファイルを開くときにエラーが発生しました。"})  '処理に失敗しました。（[%1]）
                Return False
            End Try

            '作業シート設定
            xlSheets = xlBook.Worksheets
            xlSheet = DirectCast(xlSheets(1), Excel.Worksheet)
            xlSheet.Activate()

            'オートフィルタを解除
            xlSheet.AutoFilterMode = False

            '最終セルを選択
            xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

            If xlApp.ActiveCell.Column < NumOfColInFileLayout Then
                '実際の列数がファイルレイアウトより少ない場合
                MyBase.ShowMessage(frm, "E479", {"正しい取込ファイルか確認してください。"})  '項目数が不足しています。[%1]
                Return False
            End If

            '最終セルの行番号・列番号を取得
            Dim lastCellRow As Integer = xlApp.ActiveCell.Row
            Dim lastCellCol As Integer = 2  '2列目(Barcode)まで取り込む

            '明細の範囲を取得
            startCell = DirectCast(xlSheet.Cells(DetailsStartRow, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(lastCellRow, lastCellCol), Excel.Range)
            range = xlSheet.Range(startCell, endCell)

            '範囲のセル値を取得する
            Dim arrData(,) As Object = DirectCast(range.Value, Object(,))

            Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString
            Dim dtImp As DataTable = Nothing    '取込先DataTable
            Dim tblSeq As Integer = 0           '取込先DataTable連番

            For row As Integer = LBound(arrData, 1) To UBound(arrData, 1)
                If Not String.IsNullOrWhiteSpace(CStr(arrData(row, ColNoSerial))) AndAlso
                   Not String.IsNullOrWhiteSpace(CStr(arrData(row, ColNoBarcode))) Then
                    If impRowCnt Mod LMI190C.N40ImpRowsMax = 0 Then
                        'DataTableを分割する件数分取り込んだとき
                        'DataTableを追加
                        dtImp = dsImp.Tables("LMI190IN_N40_CHG").Clone
                        tblSeq += 1
                        dtImp.TableName = dtImp.TableName & tblSeq
                        dsImp.Tables.Add(dtImp)
                        listDtNm.Add(dtImp.TableName)
                    End If

                    'DataTableにデータを追加
                    Dim drExcelData As DataRow = dtImp.NewRow
                    drExcelData("NRS_BR_CD") = nrsBrCd
                    drExcelData("SERIAL_NO") = CStr(arrData(row, ColNoBarcode))
                    drExcelData("CYLINDER_NO") = CStr(arrData(row, ColNoSerial))
                    dtImp.Rows.Add(drExcelData)
                    impRowCnt += 1
                End If
            Next

            If impRowCnt < 1 Then
                MyBase.ShowMessage(frm, "E656")  '取込対象のデータが存在しません。
                Return False
            End If

        Finally
            '参照の開放

            If xlSheet IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
            End If

            If xlSheets IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheets)
                xlSheets = Nothing
            End If

            If xlBook IsNot Nothing Then
                xlBook.Close(False)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
            End If

            If xlBooks IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
            End If

            If xlApp IsNot Nothing Then
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If

        End Try

        Return True

    End Function

#End Region 'N40コード取込処理

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' INデータセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInData(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

        dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("SERIAL_NO") = frm.txtSerialNo.TextValue
        dr("CYLINDER_TYPE") = frm.cmbCylinderType.SelectedValue
        dr("TOFROM_NM") = frm.cmbToFromNm.SelectedValue
        dr("KEIKA_DATE") = frm.numKeikaDate.TextValue
        dr("KIJUN_DATE") = frm.imdKijunDate.TextValue
        dr("IDO_DATE_FROM") = frm.imdIdoDateFrom.TextValue
        dr("IDO_DATE_TO") = frm.imdIdoDateTo.TextValue

        '2013.08.15 要望対応2095 START
        dr("COOLANT_GOODS_KB") = frm.cmbCoolantGoodsKb.SelectedValue
        '2013.08.15 要望対応2095 END

        '空・実入り区分の設定
        Dim kbnCd As String = String.Empty
        If frm.optKara.Checked = True Then
            kbnCd = "01"
        ElseIf frm.optMiiri.Checked = True Then
            kbnCd = "02"
        Else
            kbnCd = String.Empty
        End If

        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'M016' AND ",
                                                                                                        "KBN_CD = '", kbnCd, "'"))
        dr("EMPTY_KB") = String.Empty
        If 0 < kbnDr.Length Then
            dr("EMPTY_KB") = kbnDr(0).Item("KBN_NM1").ToString
        End If

        '入出在その他区分の設定
        If frm.optInka.Checked = True Then
            dr("IOZS_KB") = "10"
        ElseIf frm.optOutka.Checked = True Then
            dr("IOZS_KB") = "20"
        Else
            dr("IOZS_KB") = String.Empty
        End If

        '遅延金制度開始日の設定
        dr("CHIENSTART_DATE") = frm.imdChienDate.TextValue
        'SYSDATEの設定
        dr("SYSDATE") = MyBase.GetSystemDateTime(0)

        'データセットに設定
        rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

        Return rtDs

    End Function

    ''' <summary>
    ''' INデータセット設定（廃棄・廃棄解除処理時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetSpreadInData(ByVal frm As LMI190F) As DataSet

        Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim rowNo As Integer = 0
        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = Nothing

        With frm.sprDetails.ActiveSheet

            For i As Integer = 0 To Me._ChkList.Count - 1

                dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()
                rowNo = Convert.ToInt32(Me._ChkList(i).ToString())

                dr("NRS_BR_CD") = nrsBrCd
                dr("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.SERIALNO.ColNo))
                dr("IOZS_KB") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.IOZSKBCD.ColNo))
                dr("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.INOUTDATE.ColNo)).Replace("/", "")
                dr("SHIP_CD_L") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.SHIPCDL.ColNo))
                dr("SHIP_NM_L") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.SHIPNML.ColNo))
                dr("INOUTKA_NO_L") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.INOUTKANOL.ColNo))
                dr("INOUTKA_NO_M") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.INOUTKANOM.ColNo))
                dr("INOUTKA_NO_S") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.INOUTKANOS.ColNo))
                dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo))
                dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(rowNo, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo))

                rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

            Next

        End With

        Return rtDs

    End Function

    '''' <summary>
    '''' INデータセット設定（保存処理時）
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Private Function SetEditSpreadInData(ByVal frm As LMI190F) As DataSet

    '    Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString()
    '    Dim rtDs As DataSet = New LMI190DS()
    '    Dim dr As DataRow = Nothing

    '    Dim preCd As String = String.Empty
    '    Dim editCd As String = String.Empty

    '    Me._ChkList = New ArrayList()

    '    With frm.sprDetails.ActiveSheet

    '        For i As Integer = 0 To frm.sprDetails.ActiveSheet.RowCount - 1

    '            preCd = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SHIPCDL.ColNo))
    '            editCd = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SHIPCDL_EDIT.ColNo))

    '            If preCd.Equals(editCd) = True Then
    '                Continue For
    '            End If

    '            dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

    '            dr("NRS_BR_CD") = nrsBrCd
    '            dr("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SERIALNO.ColNo))
    '            dr("IOZS_KB") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.IOZSKBCD.ColNo))
    '            dr("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.INOUTDATE.ColNo)).Replace("/", "")
    '            dr("SHIP_CD_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SHIPCDL_EDIT.ColNo))
    '            dr("SHIP_NM_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SHIPNML.ColNo))
    '            dr("INOUTKA_NO_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.INOUTKANOL.ColNo))
    '            dr("INOUTKA_NO_M") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.INOUTKANOM.ColNo))
    '            dr("INOUTKA_NO_S") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.INOUTKANOS.ColNo))
    '            dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo))
    '            dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(i, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo))

    '            rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

    '            'ArrayListに対象行番号を格納
    '            Me._ChkList.Add(i)

    '        Next

    '    End With

    '    Return rtDs

    'End Function

    ''' <summary>
    ''' Excel出力処理用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMI900InDataSet(ByVal frm As LMI190F) As DataSet

        Dim prmDs As DataSet = New LMI900DS
        Dim row As DataRow = Nothing

        With frm.sprDetails.ActiveSheet

            For i As Integer = 0 To .Rows.Count - 1

                row = prmDs.Tables(LMI900C.TABLE_NM_IN).NewRow

                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                row("EMPTY_KB") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.EMPTYKB))
                row("CYLINDER_TYPE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.CYLINDERTYPE))
                row("TOFROM_NM") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.TOFROMNM))
                row("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.SERIALNO))
                row("ALBAS_CHG") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.YOUKINO))
                row("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.INOUTDATE))
                row("NEXT_TEST_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.NEXTTESTDATE))
                row("KEIKA_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.KEIKADATE1))
                row("IOZS_KB") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.IOZSKB))
                row("SHIP_CD_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.SHIPCDL))
                row("SHIP_NM_L") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.SHIPNML))
                row("BUYER_ORD_NO_DTL") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.BUYERORDNO))
                row("PROD_DATE") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.PROD_DATE))          'ADD 2019/10/29 006786
                row("GOODS_CD_CUST") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.GOODS_CD_CUST))  'ADD 2019/10/31 008262
                row("GOODS_NM") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.GOODS_NM))            'ADD 2019/10/31 008262
                row("SEARCH_KEY_2") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.SEARCH_KEY_2))    'ADD 2019/12/10 009849
                row("REMARK_IN") = Me._LMIconV.GetCellValue(.Cells(i, LMI190C.SprColumnIndexSPRALL.REMARK_IN))

                prmDs.Tables(LMI900C.TABLE_NM_IN).Rows.Add(row)

            Next

        End With

        Return prmDs

    End Function

#End Region

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI190F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMI190C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI190C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '項目チェック：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMI190C.EventShubetsu.MASTEROPEN)

    End Sub

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMI190F, ByVal objNm As String, ByVal actionType As LMI190C.EventShubetsu) As Boolean

        With frm

            Dim rtnResult As Boolean = False

            'スプレッドの場合、後でロック
            Dim sprNm As String = .sprDetails.Name
            If sprNm.Equals(objNm) = False Then

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

            End If

            Select Case objNm

                Case .txtShipCd.Name

                    rtnResult = Me.SetReturnDestPop(frm, objNm, actionType)

            End Select

            '処理終了アクション
            Me._LMIConH.EndAction(frm)

            Return rtnResult

        End With

    End Function


    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMI190F, ByVal objNm As String, ByVal actionType As LMI190C.EventShubetsu) As Boolean

        With frm

            Dim ctl As Win.InputMan.LMImTextBox = Me._LMIConH.GetTextControl(frm, objNm)
            Dim prm As LMFormData = Me.ShowDestPopup(frm, ctl, actionType)
            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                ctl.TextValue = dr.Item("DEST_CD").ToString()
                Dim destNm As String = dr.Item("DEST_NM").ToString()
                Dim jis As String = dr.Item("JIS").ToString()

                Select Case ctl.Name

                    Case .txtShipCd.Name

                        .txtShipNm.TextValue = destNm

                End Select

                Return True

            End If

            Return False

        End With

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMI190F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal actionType As LMI190C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim rowNo As Integer = 0
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            '.Item("CUST_CD_L") = frm.sprDetails.ActiveSheet.Cells(rowNo, LMI190G.sprDetailsRireki.CUSTCDL.ColNo).Text
            If actionType = LMI190C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = ctl.TextValue
            End If
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMIConH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

#End Region

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'Enterキー判定
        Dim rtnResult As Boolean = eventFlg

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMI190C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI190C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMIConH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            MyBase.ShowMessage(frm)

            Exit Sub

        End If

        '項目チェック：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMI190C.EventShubetsu.ENTER)

        'フォーカス移動処理
        Call Me._LMIConH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region 'Method

#Region "エラーEXCEL出力のデータセット設定"

    ''' <summary>
    ''' エラーEXCEL出力データセット設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>

    Private Function ExcelErrorSet(ByRef ds As DataSet) As DataSet

        Dim max As Integer = ds.Tables("LMI190_GUIERROR").Rows.Count() - 1
        Dim dr As DataRow
        Dim prm1 As String = String.Empty
        Dim prm2 As String = String.Empty
        Dim prm3 As String = String.Empty
        Dim prm4 As String = String.Empty
        Dim prm5 As String = String.Empty

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        For i As Integer = 0 To max

            dr = ds.Tables("LMI190_GUIERROR").Rows(i)

            If String.IsNullOrEmpty(dr("PARA1").ToString()) = False Then
                prm1 = dr("PARA1").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA2").ToString()) = False Then
                prm2 = dr("PARA2").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA3").ToString()) = False Then
                prm3 = dr("PARA3").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA4").ToString()) = False Then
                prm4 = dr("PARA4").ToString()
            End If
            If String.IsNullOrEmpty(dr("PARA5").ToString()) = False Then
                prm5 = dr("PARA5").ToString()
            End If
            MyBase.SetMessageStore(dr("GUIDANCE_ID").ToString() _
                     , dr("MESSAGE_ID").ToString() _
                     , New String() {prm1, prm2, prm3, prm4, prm5} _
                     , dr("ROW_NO").ToString() _
                     , dr("KEY_NM").ToString() _
                     , dr("KEY_VALUE").ToString())

        Next

        Return ds

    End Function

#End Region

#Region "一括変更選択行データセット"
    ''' <summary>
    ''' 一括変更選択行データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SetDataHenkoKey(ByVal frm As LMI190F) As DataSet

        Dim chkList As ArrayList = Me._V.GetCheckList()
        Dim max As Integer = chkList.Count()
        Dim dr As DataRow
        Dim selectRow As Integer = 0
        Dim nrsBrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim rtDs As DataSet = New LMI190DS()

        Dim preCd As String = String.Empty
        Dim preNm As String = String.Empty
        Dim edtCd As String = String.Empty
        Dim edtNm As String = String.Empty

        With frm.sprDetails.ActiveSheet

            For i As Integer = 0 To max - 1

                selectRow = Convert.ToInt32(chkList(i))

                If frm.optZaiko.Checked = True Then
                    '在庫検索の場合
                    preCd = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SHIPCDL.ColNo))
                    preNm = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SHIPNML.ColNo))

                    'If frm.txtShipCd.TextValue.Trim().Equals(String.Empty) = True Then
                    '    edtCd = preCd
                    'Else
                    '    edtCd = frm.txtShipCd.TextValue.Trim()
                    'End If
                    'If frm.txtShipNm.TextValue.Trim().Equals(String.Empty) = True Then
                    '    edtNm = preNm
                    'Else
                    '    edtNm = frm.txtShipNm.TextValue.Trim()
                    'End If
                    If frm.cmbEditList.SelectedValue.ToString.Equals(LMI190C.EDIT_SELECT_SHIPCD) Then
                        edtCd = frm.txtShipCd.TextValue.Trim()
                        edtNm = preNm
                    Else
                        edtCd = preCd
                        edtNm = frm.txtShipNm.TextValue.Trim()
                    End If

                    dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

                    dr("NRS_BR_CD") = nrsBrCd
                    dr("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SERIALNO.ColNo))
                    dr("IOZS_KB") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.IOZSKBCD.ColNo))
                    dr("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTDATE.ColNo)).Replace("/", "")
                    dr("SHIP_CD_L") = edtCd
                    dr("SHIP_NM_L") = edtNm
                    dr("INOUTKA_NO_L") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTKANOL.ColNo))
                    dr("INOUTKA_NO_M") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTKANOM.ColNo))
                    dr("INOUTKA_NO_S") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTKANOS.ColNo))
                    dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo))
                    dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo))

                    rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

                    ''ArrayListに対象行番号を格納
                    'Me._ChkList.Add(i)

                Else
                    '履歴検索、廃棄済検索の場合
                    preCd = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SHIPCDL.ColNo))
                    preNm = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SHIPNML.ColNo))

                    'If frm.txtShipCd.TextValue.Trim().Equals(String.Empty) = True Then
                    '    edtCd = preCd
                    'Else
                    '    edtCd = frm.txtShipCd.TextValue.Trim()
                    'End If

                    'If frm.txtShipNm.TextValue.Trim().Equals(String.Empty) = True Then
                    '    edtNm = preNm
                    'Else
                    '    edtNm = frm.txtShipNm.TextValue.Trim()
                    'End If
                    If frm.cmbEditList.SelectedValue.ToString.Equals(LMI190C.EDIT_SELECT_SHIPCD) Then
                        edtCd = frm.txtShipCd.TextValue.Trim()
                        edtNm = preNm
                    Else
                        edtCd = preCd
                        edtNm = frm.txtShipNm.TextValue.Trim()
                    End If

                    dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

                    dr("NRS_BR_CD") = nrsBrCd
                    dr("SERIAL_NO") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SERIALNO.ColNo))
                    dr("IOZS_KB") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.IOZSKBCD.ColNo))
                    dr("INOUT_DATE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTDATE.ColNo)).Replace("/", "")
                    dr("SHIP_CD_L") = edtCd
                    dr("SHIP_NM_L") = edtNm
                    dr("INOUTKA_NO_L") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTKANOL.ColNo))
                    dr("INOUTKA_NO_M") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTKANOM.ColNo))
                    dr("INOUTKA_NO_S") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.INOUTKANOS.ColNo))
                    dr("SYS_UPD_DATE") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SYSUPDDATE.ColNo))
                    dr("SYS_UPD_TIME") = Me._LMIconV.GetCellValue(.Cells(selectRow, LMI190G.sprDetailsAll.SYSUPDTIME.ColNo))

                    rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

                    ''ArrayListに対象行番号を格納
                    'Me._ChkList.Add(i)

                End If

                preCd = String.Empty
                preNm = String.Empty
                edtCd = String.Empty
                edtNm = String.Empty
            Next

        End With

        Return rtDs

    End Function
#End Region

#Region "一括変更成功時"
    ''' <summary>
    ''' 一括変更成功時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SuccessHenko(ByVal frm As LMI190F)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"一括変更", String.Empty})

    End Sub
#End Region

#Region "削除予定？"

    ''' <summary>
    ''' 出荷データ取得処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal frm As LMI190F) As DataSet

        Dim ds As DataSet = Nothing

        'InDataSetの場合
        ds = Me.SetInDataOutkaNoChange(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectOutkaData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "SelectOutkaData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectOutkaData")

        Return rtnDs

    End Function


    ''' <summary>
    ''' 保存処理(出荷の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ShukkaData(ByVal frm As LMI190F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI190DS()

        'データセット設定
        Dim ds As DataSet = SetInDataShukka(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShukkaData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "ShukkaData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShukkaData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 保存処理(回収の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function KaishuData(ByVal frm As LMI190F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI190DS()

        'データセット設定
        Dim ds As DataSet = SetInDataKaishu(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KaishuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "KaishuData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KaishuData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 保存処理(取消の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function TorikeshiData(ByVal frm As LMI190F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI190DS()

        'データセット設定
        Dim ds As DataSet = SetInDataTorikeshi(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikeshiData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "TorikeshiData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikeshiData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 出荷管理番号変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeOutkaNo(ByVal frm As LMI190F)

        Dim rtDs As DataSet = Nothing

        '出荷データ取得処理
        rtDs = Me.SelectOutkaData(frm)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 保存時の入力チェック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function HozonCheckMain(ByVal frm As LMI190F) As Boolean

        Dim rtnDs As DataSet = Nothing

        If frm.optZaiko.Checked = True Then
            '出荷の場合

            '保存時の入力チェック処理(出荷の場合)
            rtnDs = Me.ShukkaCheckData(frm)

            '取得データが存在する場合はエラー
            If rtnDs.Tables(LMI190C.TABLE_NM_OUT).Rows.Count > 0 Then
                MyBase.ShowMessage(frm, "E503", New String() {String.Concat("シリアル№=", rtnDs.Tables(LMI190C.TABLE_NM_OUT).Rows(0).Item("SERIAL_NO").ToString)})
                Return False
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' 保存時の入力チェック処理(出荷の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ShukkaCheckData(ByVal frm As LMI190F) As DataSet

        'データセット設定
        Dim ds As DataSet = SetInDataShukka(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShukkaCheckData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI190BLF", "ShukkaCheckData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShukkaCheckData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' データセット設定(保存(取消)の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataTorikeshi(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

        '一覧に値が設定されていない場合
        dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

        dr("NRC_REC_NO") = String.Empty
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("OUTKA_NO_L") = frm.txtSerialNo.TextValue
        dr("EDA_NO") = String.Empty
        dr("TOROKU_KB") = String.Empty
        dr("SERIAL_NO") = String.Empty
        dr("HOKOKU_DATE") = String.Empty

        'データセットに設定
        rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(行削除時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataRowDel(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_OUT).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        For i As Integer = 0 To max
            If Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetailsAll.DEF.ColNo)).Equals(LMConst.FLG.ON) = False Then
                dr = rtDs.Tables(LMI190C.TABLE_NM_OUT).NewRow()

                dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetailsAll.SERIALNO.ColNo)).ToString

                'データセットに設定
                rtDs.Tables(LMI190C.TABLE_NM_OUT).Rows.Add(dr)
            End If
        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存時の状態区分再取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataJotai(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        For i As Integer = 0 To max
            dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetailsAll.SERIALNO.ColNo)).ToString
            dr("NRC_REC_NO") = String.Empty
            dr("OUTKA_NO_L") = String.Empty
            dr("EDA_NO") = String.Empty
            dr("TOROKU_KB") = String.Empty
            dr("HOKOKU_DATE") = String.Empty

            'データセットに設定
            rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)
        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存(回収)の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataKaishu(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        For i As Integer = 0 To max
            dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

            'dr("NRC_REC_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetails.NRCRECNO.ColNo)).ToString
            'dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            'dr("OUTKA_NO_L") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetails.OUTKANOL.ColNo)).ToString
            'dr("EDA_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetails.EDANO.ColNo)).ToString
            'dr("TOROKU_KB") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetails.TOROKUKB.ColNo)).ToString
            'dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetailsZaiko.SERIALNO.ColNo)).ToString
            'dr("HOKOKU_DATE") = frm.imdKijunDate.TextValue

            'データセットに設定
            rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)
        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(出荷管理番号変更時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataOutkaNoChange(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

        dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("OUTKA_NO_L") = frm.txtSerialNo.TextValue

        'データセットに設定
        rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存(出荷)の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataShukka(ByVal frm As LMI190F) As DataSet

        Dim rtDs As DataSet = New LMI190DS()
        Dim dr As DataRow = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        If max = -1 Then
            '一覧に値が設定されていない場合
            dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

            dr("NRC_REC_NO") = String.Empty
            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("OUTKA_NO_L") = frm.txtSerialNo.TextValue
            dr("EDA_NO") = String.Empty
            dr("TOROKU_KB") = String.Empty
            dr("SERIAL_NO") = String.Empty
            dr("HOKOKU_DATE") = String.Empty

            'データセットに設定
            rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)

            Return rtDs
        End If

        For i As Integer = 0 To max
            dr = rtDs.Tables(LMI190C.TABLE_NM_IN).NewRow()

            dr("NRC_REC_NO") = String.Empty
            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("OUTKA_NO_L") = frm.txtSerialNo.TextValue
            dr("EDA_NO") = String.Empty
            dr("TOROKU_KB") = String.Empty
            dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI190G.sprDetailsAll.SERIALNO.ColNo)).ToString
            dr("HOKOKU_DATE") = String.Empty

            'データセットに設定
            rtDs.Tables(LMI190C.TABLE_NM_IN).Rows.Add(dr)
        Next

        Return rtDs

    End Function

#End Region

#Region "EXCEL出力処理"
    Private Sub OutputExcel(ByVal frm As LMI190F)

        MyBase.ShowMessage(frm, "E235")
        'EXCEL起動()
        MyBase.MessageStoreDownload()

    End Sub

#End Region

#Region "コントロール初期化"
    Private Sub ClearControl(ByVal frm As LMI190F)

        With frm
            .txtShipCd.TextValue = String.Empty
            .txtShipNm.TextValue = String.Empty
        End With

    End Sub
#End Region

End Class
