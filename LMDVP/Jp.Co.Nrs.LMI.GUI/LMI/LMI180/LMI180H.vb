' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI180  : NRC出荷／回収情報入力
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI180ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI180H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI180V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI180G

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
        Dim frm As LMI180F = New LMI180F(Me)

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
        Me._G = New LMI180G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI180V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI180C.EventShubetsu, ByVal frm As LMI180F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI180C.EventShubetsu.TORIKOMI    '取込

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    'OrElse _
                    'Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                '取込処理
                Call Me.TorikomiData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI180C.EventShubetsu.HOZON    '保存

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                '入力チェック(サーバー側)
                If Me.HozonCheckMain(frm) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                '保存処理
                Call Me.HozonMain(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

                '要望番号:1942（保存後、Spreadをクリア) 2013/03/14 START
                frm.sprDetails.CrearSpread()
                '要望番号:1942（保存後、Spreadをクリア) 2013/03/14 END

            Case LMI180C.EventShubetsu.EXCEL     'Excel出力

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                'NRC出荷／回収情報Excel作成処理呼び出し
                Call Me.ShowExcelLMI890(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI180C.EventShubetsu.ROWADD     '行追加

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                'スプレッドの行追加
                Call Me._G.RowAddSpread()

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI180C.EventShubetsu.ROWDEL     '行削除

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                '削除行以外をデータセットに保存
                Dim ds As DataSet = SetInDataRowDel(frm)

                'スプレッドに設定
                Call Me._G.SetSpread(frm.sprDetails, ds, True)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

            Case LMI180C.EventShubetsu.CHANGEOPTBUTTOM     'オプションボタン変更

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus()

                'コントロール値のクリア
                Call Me._G.ClearControl()

            Case LMI180C.EventShubetsu.CHANGEOUTKANOL     '出荷管理番号Enter押下時

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm, "G006")
                    Exit Sub
                End If

                '出荷データ取得処理
                Call Me.ChangeOutkaNo(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm, "G006")

                '要望番号:1917 yamanaka 2013.03.06 Start
            Case LMI180C.EventShubetsu.FILESELECT         '選択ボタン押下時押下時

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                Call Me.GetFileName(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                '要望番号:1917 yamanaka 2013.03.06 End
        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI180F) As Boolean

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
    Friend Sub FunctionKey1Press(ByVal frm As LMI180F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Me.ActionControl(LMI180C.EventShubetsu.TORIKOMI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI180F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Me.ActionControl(LMI180C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI180F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI180F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' Excel出力ボタン押下イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnExcelClick(ByRef frm As LMI180F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnExcelClick")

        Me.ActionControl(LMI180C.EventShubetsu.EXCEL, frm)

        Logger.EndLog(Me.GetType.Name, "btnExcelClick")

    End Sub

    ''' <summary>
    ''' 行追加ボタン押下イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnRowAddClick(ByRef frm As LMI180F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnRowAddClick")

        Me.ActionControl(LMI180C.EventShubetsu.ROWADD, frm)

        Logger.EndLog(Me.GetType.Name, "btnRowAddClick")

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnRowDelClick(ByRef frm As LMI180F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnRowDelClick")

        Me.ActionControl(LMI180C.EventShubetsu.ROWDEL, frm)

        Logger.EndLog(Me.GetType.Name, "btnRowDelClick")

    End Sub

    ''' <summary>
    ''' 出荷管理番号変更イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub outkaNoChange(ByRef frm As LMI180F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "outkaNoChange")

        Me.ActionControl(LMI180C.EventShubetsu.CHANGEOUTKANOL, frm)

        Logger.EndLog(Me.GetType.Name, "outkaNoChange")

    End Sub

    ''' <summary>
    ''' ラジオボタンチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub optButtomChange(ByRef frm As LMI180F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "optButtomChange")

        Me.ActionControl(LMI180C.EventShubetsu.CHANGEOPTBUTTOM, frm)

        Logger.EndLog(Me.GetType.Name, "optButtomChange")

    End Sub

    '要望番号:1917 yamanaka 2013.03.06 Start
    ''' <summary>
    ''' 選択ボタン押下イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnSelectClick(ByRef frm As LMI180F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnRowAddClick")

        Me.ActionControl(LMI180C.EventShubetsu.FILESELECT, frm)

        Logger.EndLog(Me.GetType.Name, "btnRowAddClick")

    End Sub
    '要望番号:1917 yamanaka 2013.03.06 End
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub TorikomiData(ByVal frm As LMI180F)

        '(2013.03.07)要望番号1933 ファイル存在チェック -- START --
        'ファイル存在チェック
        Dim filePath_Name As String = frm.txtPath.TextValue.ToString.Trim
        If Me._V.IsFileExist(filePath_Name) = False Then
            Exit Sub
        End If
        '(2013.03.07)要望番号1933 ファイル存在チェック --  END  --

        'ファイルの取込
        Dim rtDs As DataSet = Me._G.GetFileDataCsv(frm)

        If rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Count = 0 Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "E469", New String() {"取込対象のデータ"})
            '処理終了アクション
            Me._LMIConH.EndAction(frm, "G006")
            Exit Sub
        End If

        '入力チェック
        rtDs = Me._V.IsSingleCsvCheck(rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikomiData")

        '==========================
        'WSAクラス呼出
        '==========================
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMI180BLF", "TorikomiData", rtDs)

        '処理終了アクション
        Me._LMIConH.EndAction(frm, "G006")

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '取込データを一覧に表示
        Call Me._G.SetSpread(frm.sprDetails, rtDs, False)

        '入力チェックと状態の更新
        Call Me._G.SetSpreadJotai(frm.sprDetails)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"取込処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiData")

    End Sub

    ''' <summary>
    ''' 保存時の入力チェック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function HozonCheckMain(ByVal frm As LMI180F) As Boolean

        Dim rtnDs As DataSet = Nothing

        If frm.optShukka.Checked = True Then
            '出荷の場合

            '保存時の入力チェック処理(出荷の場合)
            rtnDs = Me.ShukkaCheckData(frm)

            '取得データが存在する場合はエラー
            If rtnDs.Tables(LMI180C.TABLE_NM_OUT).Rows.Count > 0 Then
                MyBase.ShowMessage(frm, "E503", New String() {String.Concat("シリアル№=", rtnDs.Tables(LMI180C.TABLE_NM_OUT).Rows(0).Item("SERIAL_NO").ToString)})
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
    Private Function ShukkaCheckData(ByVal frm As LMI180F) As DataSet

        'データセット設定
        Dim ds As DataSet = SetInDataShukka(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShukkaCheckData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI180BLF", "ShukkaCheckData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShukkaCheckData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub HozonMain(ByVal frm As LMI180F)

        If frm.optShukka.Checked = True Then
            '出荷の場合

            '保存処理(出荷の場合)
            Call Me.ShukkaData(frm)

        ElseIf frm.optKaishu.Checked = True Then
            '回収の場合

            '状態区分再取得
            Call Me.JotaiData(frm)

            '保存処理(回収の場合)
            Call Me.KaishuData(frm)

        ElseIf frm.optTorikeshi.Checked = True Then
            '取消の場合

            '保存処理(取消の場合)
            Call Me.TorikeshiData(frm)

        End If

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

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"保存処理", ""})

    End Sub

    ''' <summary>
    ''' 保存処理(出荷の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ShukkaData(ByVal frm As LMI180F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI180DS()

        'データセット設定
        Dim ds As DataSet = SetInDataShukka(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShukkaData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI180BLF", "ShukkaData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShukkaData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 保存時の状態区分再取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub JotaiData(ByVal frm As LMI180F)

        '画面の値をデータセットに設定
        Dim rtDs As DataSet = Me.SetInDataJotai(frm)

        '入力チェック
        rtDs = Me._V.IsSingleCsvCheck(rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "JotaiData")

        '==========================
        'WSAクラス呼出
        '==========================
        '==== WSAクラス呼出 ====
        rtDs = MyBase.CallWSA("LMI180BLF", "TorikomiData", rtDs)

        '処理終了アクション
        Me._LMIConH.EndAction(frm, "G006")

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '覧に表示
        Call Me._G.SetSpread(frm.sprDetails, rtDs, True)

        '入力チェックと状態の更新
        Call Me._G.SetSpreadJotai(frm.sprDetails)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "JotaiData")

    End Sub

    ''' <summary>
    ''' 保存処理(回収の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function KaishuData(ByVal frm As LMI180F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI180DS()

        'データセット設定
        Dim ds As DataSet = SetInDataKaishu(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "KaishuData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI180BLF", "KaishuData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "KaishuData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 保存処理(取消の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function TorikeshiData(ByVal frm As LMI180F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI180DS()

        'データセット設定
        Dim ds As DataSet = SetInDataTorikeshi(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikeshiData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI180BLF", "TorikeshiData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikeshiData")

        Return rtDs

    End Function

    ''' <summary>
    ''' 出荷管理番号変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeOutkaNo(ByVal frm As LMI180F)

        Dim rtDs As DataSet = Nothing

        '出荷データ取得処理
        rtDs = Me.SelectOutkaData(frm)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '出荷データの設定
        Call Me._G.SetOutkaData(rtDs)

    End Sub

    ''' <summary>
    ''' 出荷データ取得処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectOutkaData(ByVal frm As LMI180F) As DataSet

        Dim ds As DataSet = Nothing

        'InDataSetの場合
        ds = Me.SetInDataOutkaNoChange(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectOutkaData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI180BLF", "SelectOutkaData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectOutkaData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' Excel作成(LMI890)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowExcelLMI890(ByVal frm As LMI180F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'DataSet設定
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        prmDs = New LMI890DS
        row = prmDs.Tables(LMI890C.TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        row("HOKOKU_DATE_FROM") = frm.imdHokokuDateFrom.TextValue
        row("HOKOKU_DATE_TO") = frm.imdHokokuDateTo.TextValue

        prmDs.Tables(LMI890C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'Excel作成処理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMI890", prm)

        If prm.ReturnFlg = False Then
            'メッセージエリアの設定
            MyBase.SetMessage("E501")
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub

    '要望番号:1917 yamanaka 2013.03.06 Start
    ''' <summary>
    ''' ファイル名取得処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub GetFileName(ByVal frm As LMI180F)

        Dim OpenFileDialog1 As New OpenFileDialog()

        '取込ファイルの設定
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F004' AND ", _
                                                                                                        "KBN_NM1 = '", frm.cmbEigyo.SelectedValue, "'"))
        If 0 < kbnDr.Length Then

            ' 初期表示するディレクトリを設定する
            OpenFileDialog1.InitialDirectory = frm.txtPath.TextValue

            ' ファイルのフィルタを設定する
            OpenFileDialog1.Filter = String.Concat("テキスト ファイル(*.txt)|", kbnDr(0).Item("KBN_NM3").ToString())

            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                frm.txtPath.TextValue = OpenFileDialog1.FileName
            End If

        End If

    End Sub
    '要望番号:1917 yamanaka 2013.03.06 End

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(出荷管理番号変更時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataOutkaNoChange(ByVal frm As LMI180F) As DataSet

        Dim rtDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

        dr = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("OUTKA_NO_L") = frm.txtOutkaNoL.TextValue

        'データセットに設定
        rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)
        
        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存(出荷)の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataShukka(ByVal frm As LMI180F) As DataSet

        Dim rtDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        If max = -1 Then
            '一覧に値が設定されていない場合
            dr = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

            dr("NRC_REC_NO") = String.Empty
            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("OUTKA_NO_L") = frm.txtOutkaNoL.TextValue
            dr("EDA_NO") = String.Empty
            dr("TOROKU_KB") = String.Empty
            dr("SERIAL_NO") = String.Empty
            dr("SERIAL_NO_FROM") = frm.txtSerialNoFrom.TextValue
            dr("SERIAL_NO_TO") = frm.txtSerialNoTo.TextValue
            dr("HOKOKU_DATE") = String.Empty

            'データセットに設定
            rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)

            Return rtDs
        End If

        For i As Integer = 0 To max
            dr = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

            dr("NRC_REC_NO") = String.Empty
            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("OUTKA_NO_L") = frm.txtOutkaNoL.TextValue
            dr("EDA_NO") = String.Empty
            dr("TOROKU_KB") = String.Empty
            dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo)).ToString
            dr("SERIAL_NO_FROM") = frm.txtSerialNoFrom.TextValue
            dr("SERIAL_NO_TO") = frm.txtSerialNoTo.TextValue
            dr("HOKOKU_DATE") = String.Empty

            'データセットに設定
            rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)
        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存時の状態区分再取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataJotai(ByVal frm As LMI180F) As DataSet

        Dim rtDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        For i As Integer = 0 To max
            dr = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo)).ToString
            dr("NRC_REC_NO") = String.Empty
            dr("OUTKA_NO_L") = String.Empty
            dr("EDA_NO") = String.Empty
            dr("TOROKU_KB") = String.Empty
            dr("HOKOKU_DATE") = String.Empty

            'データセットに設定
            rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)
        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存(回収)の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataKaishu(ByVal frm As LMI180F) As DataSet

        Dim rtDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        For i As Integer = 0 To max
            dr = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

            dr("NRC_REC_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.NRCRECNO.ColNo)).ToString
            dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
            dr("OUTKA_NO_L") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.OUTKANOL.ColNo)).ToString
            dr("EDA_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.EDANO.ColNo)).ToString
            dr("TOROKU_KB") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.TOROKUKB.ColNo)).ToString
            dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo)).ToString
            dr("HOKOKU_DATE") = frm.imdKaishuDate.TextValue

            'データセットに設定
            rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)
        Next

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(保存(取消)の場合)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataTorikeshi(ByVal frm As LMI180F) As DataSet

        Dim rtDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

        '一覧に値が設定されていない場合
        dr = rtDs.Tables(LMI180C.TABLE_NM_IN).NewRow()

        dr("NRC_REC_NO") = String.Empty
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("OUTKA_NO_L") = frm.txtOutkaNoL.TextValue
        dr("EDA_NO") = String.Empty
        dr("TOROKU_KB") = String.Empty
        dr("SERIAL_NO") = String.Empty
        dr("SERIAL_NO_FROM") = frm.txtSerialNoFrom.TextValue
        dr("SERIAL_NO_TO") = frm.txtSerialNoTo.TextValue
        dr("HOKOKU_DATE") = String.Empty

        'データセットに設定
        rtDs.Tables(LMI180C.TABLE_NM_IN).Rows.Add(dr)

        Return rtDs
        
    End Function

    ''' <summary>
    ''' データセット設定(行削除時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetInDataRowDel(ByVal frm As LMI180F) As DataSet

        Dim rtDs As DataSet = New LMI180DS()
        Dim dr As DataRow = rtDs.Tables(LMI180C.TABLE_NM_OUT).NewRow()
        Dim max As Integer = frm.sprDetails.ActiveSheet.Rows.Count - 1

        For i As Integer = 0 To max
            If Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.DEF.ColNo)).Equals(LMConst.FLG.ON) = False Then
                dr = rtDs.Tables(LMI180C.TABLE_NM_OUT).NewRow()

                dr("SERIAL_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.SERIALNO.ColNo)).ToString
                dr("NRC_REC_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.NRCRECNO.ColNo)).ToString
                dr("OUTKA_NO_L") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.OUTKANOL.ColNo)).ToString
                dr("EDA_NO") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.EDANO.ColNo)).ToString
                dr("TOROKU_KB") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.TOROKUKB.ColNo)).ToString
                dr("HOKOKU_DATE") = Me._LMIconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(i, LMI180G.sprDetails.HOKOKUDATE.ColNo)).ToString

                'データセットに設定
                rtDs.Tables(LMI180C.TABLE_NM_OUT).Rows.Add(dr)
            End If
        Next

        Return rtDs

    End Function

#End Region

#End Region 'Method

End Class
