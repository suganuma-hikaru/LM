' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME020H : 作業料明細編集
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LME020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LME020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LME020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LME020G

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
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 画面間パラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' 共通のデータセット
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

    ''' <summary>
    ''' 共通のデータセット
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsAll As DataSet

    ''' <summary>
    ''' データセットカウント
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsCnt As Integer = 0

    ''' <summary>
    ''' 連続入力フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _RenzokuFlg As String

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

        Me._Prm = prm

        Me._Ds = prmDs
        Me._DsAll = prmDs

        'フォームの作成
        Dim frm As LME020F = New LME020F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMEConG = New LMEControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMEconV = New LMEControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMEConH = New LMEControlH(DirectCast(frm, Form))

        'Gamenクラスの設定
        Me._G = New LME020G(Me, frm)

        'Validateクラスの設定
        Me._V = New LME020V(Me, frm, Me._LMEconV)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '画面項目のクリア
        Call Me._G.ClearControl()

        'INのDATASETに設定されている値を画面に設定
        Call Me._G.SetControlEdit(Me._Ds)

        '連続入力フラグ設定
        Me._RenzokuFlg = Me._Ds.Tables(LME020C.TABLE_NM_IN).Rows(0).Item("RENZOKU_FLG").ToString

        Dim errFlg As Boolean = False

        If prm.RecStatus = RecordStatus.NEW_REC Then
            '新規モード
            'シチュエーションラベルの設定
            Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NEW_REC)

        ElseIf prm.RecStatus = RecordStatus.NOMAL_REC Then
            '編集モード
            'シチュエーションラベルの設定
            Call Me._G.SetSituation(DispMode.VIEW, RecordStatus.NOMAL_REC)

        ElseIf prm.RecStatus = RecordStatus.COPY_REC Then
            'コピーモード
            'シチュエーションラベルの設定
            Call Me._G.SetSituation(DispMode.VIEW, RecordStatus.COPY_REC)

        End If

        If prm.RecStatus = RecordStatus.NOMAL_REC OrElse _
            prm.RecStatus = RecordStatus.COPY_REC Then
            '検索処理
            Me._Ds = Me.SelectControl(frm, prmDs)

            If Me._Ds Is Nothing = False _
                AndAlso 0 < Me._Ds.Tables(LME020C.TABLE_NM_INOUT).Rows.Count Then
                '検索成功時、値を画面に設定
                Call Me._G.SetControlServerData(Me._Ds)

            Else
                '検索失敗時、メッセージの表示
                errFlg = True
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage(frm, "E078", New String() {"作業レコード"})
                MyBase.ShowMessage(frm, "E851")
                '2016.01.06 UMANO 英語化対応END
            End If
        End If

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(errFlg, Me._RenzokuFlg)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'コントロール個別設定
        Call Me._G.SetControlFromMain()

        '数値コントロール書式設定
        Call Me._G.SetNumberControl()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

        If prm.RecStatus = RecordStatus.COPY_REC AndAlso _
            errFlg = False Then
            'コピーモードの時は、「複写」ボタン押下時の処理を行う。
            Me.ActionControl(LME020C.EventShubetsu.COPY, frm)
        End If


    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LME020C.EventShubetsu, ByVal frm As LME020F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If
        '(2012.12.17)要望番号1695 計算タイミングを変更 -- START --
        'START YANAI 要望番号875
        'If (LME020C.EventShubetsu.HOZON).Equals(eventShubetsu) = True Then
        '    '処理開始アクション
        '    Me._LMEConH.StartAction(frm)

        '    '作業金額の計算処理
        '    If Me._G.SAGYOKINGAKU(frm) = False Then
        '        '処理終了アクション
        '        Me._LMEConH.EndAction(frm)
        '        Exit Sub
        '    End If

        '    '処理終了アクション
        '    Me._LMEConH.EndAction(frm)
        'End If
        'END YANAI 要望番号875
        '(2012.12.17)要望番号1695 計算タイミングを変更 --  END  --

        '(2012.12.17)同じ処理が連続しているので、片方をコメントします。
        ''処理終了アクション
        'Me._LMEConH.EndAction(frm)

        '処理終了アクション
        Me._LMEConH.EndAction(frm)

        '入力チェック
        If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
           Me._V.IsKanrenCheck(eventShubetsu) = False Then
            '処理終了アクション
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LME020C.EventShubetsu.SINKI     '新規

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '作業ポップアップ表示
                Dim rtnFlg As Boolean = Me.SinkiShowPopup(frm, LME020C.EventShubetsu.SINKI, prm)

                If rtnFlg = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                'データセットのクリア
                Dim newDs As DataSet = New LME020DS
                Me._Ds = newDs

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NEW_REC)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(False, Me._RenzokuFlg)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                'コントロール個別設定
                Call Me._G.SetControl()

            Case LME020C.EventShubetsu.HENSHU     '編集

                '作業料取込チェック
                rtnDs = Me.ChkSeiqDateSagyo(frm, Me._Ds)

                'チェックでエラー時はメッセージを表示
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NOMAL_REC)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(False, Me._RenzokuFlg)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                'コントロール個別設定
                Call Me._G.SetControl()

            Case LME020C.EventShubetsu.COPY     'コピー

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                'データセットのクリア
                Dim newDs As DataSet = New LME020DS
                Me._Ds = newDs

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.COPY_REC)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(False, Me._RenzokuFlg)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                'コントロール個別設定
                Call Me._G.SetControl()

                'コントロールのコピー対象外項目のクリア処理
                Call Me._G.CopyClearControl()

            Case LME020C.EventShubetsu.DEL    '削除

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                'データセット設定処理
                Dim ds As DataSet = Me._G.SetDataSet(frm, Me._Ds)

                '2016.01.06 UMANO 英語化対応START
                'If MyBase.ShowMessage(frm, "C001", New String() {"削除"}) = MsgBoxResult.Ok Then
                If MyBase.ShowMessage(frm, "C001", New String() {frm.FunctionKey.F4ButtonName}) = MsgBoxResult.Ok Then
                    '2016.01.06 UMANO 英語化対応END
                    '削除処理
                    ds = Me.DeleteControl(frm, ds)
                Else
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                'コントロール個別設定
                Call Me._G.SetControl()

                If ("01").Equals(Me._RenzokuFlg) = False Then
                    '終了処理  
                    frm.Close()
                Else
                    '連続フラグがオンの場合は次のデータを表示
                    If ("01").Equals(Me._RenzokuFlg) = True Then
                        'スキップ処理
                        Call Me.skipData(frm)
                    End If
                End If

            Case LME020C.EventShubetsu.SKIP     'スキップ

                'スキップ処理
                Call Me.skipData(frm)

            Case LME020C.EventShubetsu.MASTER    'マスタ参照

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                'コントロールロックチェック
                Dim chkFlg As Boolean = Me._V.ShowPopupControlChk(frm, objNm)

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                'ポップアップの表示
                Call Me.ShowPopup(frm, objNm, prm, chkFlg)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME020C.EventShubetsu.HOZON    '保存

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                'データセット設定処理
                Dim ds As DataSet = Me._G.SetDataSet(frm, Me._Ds)

                '保存処理
                ds = Me.SaveControl(frm, ds)

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                'シチュエーションラベルの設定
                Call Me._G.SetSituation(DispMode.VIEW, RecordStatus.NOMAL_REC)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(False, Me._RenzokuFlg)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

                'コントロール個別設定
                Call Me._G.SetControl()

                '再描画
                Call Me._G.SetControlServerData(ds)

                'メッセージの表示
                MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblTitleSagyoRecNo.Text, " = ", frm.txtSagyoRecNo.TextValue, "]")})

                '連続フラグがオンの場合は次のデータを表示
                If ("01").Equals(Me._RenzokuFlg) = True Then
                    'スキップ処理
                    Call Me.skipData(frm)
                End If

                'START YANAI 要望番号875
            Case LME020C.EventShubetsu.SAGYOKINGAKU    '作業金額計算

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '作業金額の計算処理
                If Me._G.SAGYOKINGAKU(frm) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                '処理終了アクション
                Me._LMEConH.EndAction(frm)
                'END YANAI 要望番号875

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LME020F) As Boolean

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
    Friend Sub FunctionKey1Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        '「新規」処理
        Me.ActionControl(LME020C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        '「編集」処理
        Me.ActionControl(LME020C.EventShubetsu.HENSHU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        '「複写」処理
        Me.ActionControl(LME020C.EventShubetsu.COPY, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        '「削除」処理
        Me.ActionControl(LME020C.EventShubetsu.DEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey7Press")

        '「スキップ」処理
        Me.ActionControl(LME020C.EventShubetsu.SKIP, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey7Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LME020C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        '「保存」処理
        Me.ActionControl(LME020C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LME020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LME020F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 新規押下時ポップアップ表示
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Function SinkiShowPopup(ByVal frm As LME020F, ByVal eventShubetsu As LME020C.EventShubetsu, ByRef prm As LMFormData) As Boolean

        Dim prmDs As DataSet = New LMZ200DS
        Dim row As DataRow = prmDs.Tables(LMZ200C.TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        row("CUST_CD_L") = frm.txtCustCdL.TextValue
        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        row("SAGYO_CNT") = "1"
        prmDs.Tables(LMZ200C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ200", prm)

        '戻り処理
        If prm.ReturnFlg = True Then

            '画面項目のクリア
            Call Me._G.ClearControl()

            'PopUpから取得したデータをコントロールにセット
            With prm.ParamDataSet.Tables(LMZ200C.TABLE_NM_OUT).Rows(0)
                frm.txtSagyoCd.TextValue = .Item("SAGYO_CD").ToString()       '作業コード
                frm.txtSagyoNm.TextValue = .Item("SAGYO_NM").ToString()      '作業名
                frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                frm.txtCustCdM.TextValue = "00"

                '荷主名の取得
                '20160621 tsunehira 要番2491 add start
                Dim custDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                  "CUST_CD_L = '", frm.txtCustCdL.TextValue, "' AND ", _
                                                                                                                  "CUST_CD_M = '", frm.txtCustCdM.TextValue, "'"))
                '20160621 tsunehira 要番2491 add end
                'Dim custDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' AND ", _
                '                                                                                                  "CUST_CD_M = '", frm.txtCustCdM.TextValue, "'"))
                If 0 < custDr.Length Then
                    frm.txtCustNm.TextValue = custDr(0).Item("CUST_NM_L").ToString()      '荷主名（大）
                    'START YANAI 要望番号875
                    frm.txtSeiqtoCd.TextValue = custDr(0).Item("SAGYO_SEIQTO_CD").ToString()      '請求先コード
                    '請求先名の取得
                    Dim seiqtoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("SEIQTO_CD = '", frm.txtSeiqtoCd.TextValue, "'"))
                    If 0 < seiqtoDr.Length Then
                        frm.txtSeiqtoNm.TextValue = seiqtoDr(0).Item("SEIQTO_NM").ToString()      '請求先名
                    End If
                    'END YANAI 要望番号875
                End If
                'START YANAI 要望番号875
                If String.IsNullOrEmpty(.Item("SAGYO_UP").ToString()) = False Then
                    frm.numSagyoUp.Value = .Item("SAGYO_UP").ToString() '請求単価
                End If
                'END YANAI 要望番号875

            End With

            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LME020F, ByVal objNM As String, ByRef prm As LMFormData, ByVal chkFlg As Boolean)

        If chkFlg = False Then
            Exit Sub
        End If

        'オブジェクト名による分岐
        Select Case objNM

            Case "txtCustCdL", "txtCustCdM" '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtCustCdL.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_S
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                frm.txtCustNm.TextValue = String.Empty

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

                '戻り処理
                If prm.ReturnFlg = True Then
                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                        frm.txtCustCdM.TextValue = "00"      '荷主コード（中）
                        frm.txtCustNm.TextValue = .Item("CUST_NM_L").ToString()      '荷主名（大）
                    End With
                End If

            Case "txtDestCd", "txtDestNm" '届先マスタ参照

                Dim prmDs As DataSet = New LMZ210DS
                Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                row("CUST_CD_L") = frm.txtCustCdL.TextValue
                If Me._PopupSkipFlg = False Then
                    row("DEST_CD") = frm.txtDestCd.TextValue
                    row("DEST_NM") = frm.txtDestNm.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)

                '戻り処理
                If prm.ReturnFlg = True Then
                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                        frm.txtDestCd.TextValue = .Item("DEST_CD").ToString()      '届先コード
                        frm.txtDestNm.TextValue = .Item("DEST_NM").ToString()      '届先名
                    End With
                End If

            Case "txtGoodsCdCust", "txtGoodsNm" '商品マスタ参照

                Dim prmDs As DataSet = New LMZ020DS
                Dim row As DataRow = prmDs.Tables(LMZ020C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                row("CUST_CD_L") = frm.txtCustCdL.TextValue
                row("CUST_CD_M") = frm.txtCustCdM.TextValue
                If Me._PopupSkipFlg = False Then
                    row("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
                    row("GOODS_NM_1") = frm.txtGoodsNm.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                prmDs.Tables(LMZ020C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ020", prm)

                '戻り処理
                If prm.ReturnFlg = True Then
                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
                        frm.txtGoodsCdCust.TextValue = .Item("GOODS_CD_CUST").ToString()      '商品コード
                        frm.txtGoodsNm.TextValue = .Item("GOODS_NM_1").ToString()      '商品名
                        frm.txtGoodsCdKey.TextValue = .Item("GOODS_CD_NRS").ToString()      '商品KEY
                        frm.txtGoodsCdCustHide.TextValue = .Item("GOODS_CD_CUST").ToString()      '商品コード(隠し)
                    End With
                End If

            Case "txtSeiqtoCd"  '請求先マスタ参照

                Dim prmDs As DataSet = New LMZ220DS
                Dim row As DataRow = prmDs.Tables(LMZ220C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
                If Me._PopupSkipFlg = False Then
                    row("SEIQTO_CD") = frm.txtSeiqtoCd.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                prmDs.Tables(LMZ220C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                frm.txtSeiqtoNm.TextValue = String.Empty

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

                '戻り処理
                If prm.ReturnFlg = True Then
                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
                        frm.txtSeiqtoCd.TextValue = .Item("SEIQTO_CD").ToString()      '請求先コード
                        frm.txtSeiqtoNm.TextValue = .Item("SEIQTO_NM").ToString()      '請求先名
                    End With
                End If

        End Select

    End Sub

    ''' <summary>
    ''' データ検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SelectControl(ByVal frm As LME020F, ByVal ds As DataSet) As DataSet

        'データ検索処理
        '==== WSAクラス呼出（データ検索処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME020BLF", "SelectData", ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 作業料取込チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ChkSeiqDateSagyo(ByVal frm As LME020F, ByVal ds As DataSet) As DataSet

        'データ検索処理
        '==== WSAクラス呼出（作業料取込チェック） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME020BLF", "ChkSeiqDateSagyo", ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SaveControl(ByVal frm As LME020F, ByVal ds As DataSet) As DataSet

        '更新処理
        Dim saveMode As String = String.Empty
        If frm.lblSituation.RecordStatus = RecordStatus.NEW_REC OrElse _
            frm.lblSituation.RecordStatus = RecordStatus.COPY_REC Then
            saveMode = "InsertSaveAction"
        Else
            saveMode = "UpdateSaveAction"
        End If

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME020BLF", saveMode, ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function DeleteControl(ByVal frm As LME020F, ByVal ds As DataSet) As DataSet

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LME020BLF", "DeleteSaveAction", ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' スキップ処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub skipData(ByVal frm As LME020F)

        Dim cnt As Integer = Me._DsAll.Tables(LME020C.TABLE_NM_IN).Rows.Count - 1
        If Me._DsCnt = cnt Then
            '終了処理  
            frm.Close()
            Exit Sub
        End If
        Me._DsCnt = Me._DsCnt + 1

        Dim jikkouDs As DataSet = New LME020DS
        Dim setDs As DataSet = jikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LME020C.TABLE_NM_IN)
        Dim setDt As DataTable = jikkouDs.Tables(LME020C.TABLE_NM_IN)
        Dim dr As DataRow = setDs.Tables(LME020C.TABLE_NM_IN).NewRow()

        inTbl.Clear()
        dr("NRS_BR_CD") = Me._DsAll.Tables(LME020C.TABLE_NM_IN).Rows(Me._DsCnt).Item("NRS_BR_CD").ToString
        dr("SAGYO_REC_NO") = Me._DsAll.Tables(LME020C.TABLE_NM_IN).Rows(Me._DsCnt).Item("SAGYO_REC_NO").ToString
        inTbl.Rows.Add(dr)

        Dim errFlg As Boolean = False

        '処理開始アクション
        Me._LMEConH.StartAction(frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'シチュエーションラベルの設定
        Call Me._G.SetSituation(DispMode.VIEW, RecordStatus.NOMAL_REC)

        '検索処理
        Me._Ds = Me.SelectControl(frm, setDs)

        If Me._Ds Is Nothing = False _
            AndAlso 0 < Me._Ds.Tables(LME020C.TABLE_NM_INOUT).Rows.Count Then
            '検索成功時、値を画面に設定
            Call Me._G.SetControlServerData(Me._Ds)

        Else
            '検索失敗時、メッセージの表示
            errFlg = True
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage(frm, "E078", New String() {"作業レコード"})
            MyBase.ShowMessage(frm, "E851")
            '2016.01.06 UMANO 英語化対応END
        End If

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(errFlg, Me._RenzokuFlg)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '処理終了アクション
        Me._LMEConH.EndAction(frm)

        'コントロール個別設定
        Call Me._G.SetControl()

        'コントロール個別設定
        Call Me._G.SetControlFromMain()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

        MyBase.ClearMessageAria(frm)

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LME020F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LME020C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue

        '検索条件をデータセットに設定
        rtDs.Tables(LME020C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

#End Region 'Method

End Class
