' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD080  : 荷主システム在庫数とNRS在庫数との照合
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMD080ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD080H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD080V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD080G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConG As LMDControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConH As LMDControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMD080DS

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
        Dim frm As LMD080F = New LMD080F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMDConG = New LMDControlG(Me, DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMDconV = New LMDControlV(Me, sForm)

        'Hnadler共通クラスの設定
        Me._LMDConH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMD080G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMD080V(Me, frm, Me._LMDconV)

        'データセットの初期化
        Me._Ds = New LMD080DS

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMD080C.EventShubetsu, ByVal frm As LMD080F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMD080C.EventShubetsu.CHECK    'チェック

                '処理開始アクション
                Me._LMDConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                'チェック処理
                Call Me.CheckData(frm)

            Case LMD080C.EventShubetsu.TORIKOMI    '取込

                '処理開始アクション
                Me._LMDConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False  Then
                    '処理終了アクション
                    Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '取込処理
                Call Me.TorikomiData(frm)

            Case LMD080C.EventShubetsu.SHUKEI    '集計

                '処理開始アクション
                Me._LMDConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '集計処理
                Call Me.ShukeiData(frm)

            Case LMD080C.EventShubetsu.SHOGO    '照合

                '処理開始アクション
                Me._LMDConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '照合処理
                Call Me.ShogohData(frm)

            Case LMD080C.EventShubetsu.MASTER    'マスタ参照

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                '処理開始アクション
                Me._LMDConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '荷主コード
                Call Me.ShowPopup(frm, objNm, prm)

                '処理終了アクション
                Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

            Case LMD080C.EventShubetsu.CUSTCDCHENGE    '荷主コード変更

                '荷主在庫レイアウトの設定
                Call Me.ShogohMstData(frm)

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus()

            Case LMD080C.EventShubetsu.LAYOUTCHENGE   '荷主在庫レイアウト変更

                '照合キーの設定
                Call Me._G.SetShogohKey(frm, Me._Ds)

                '画面の入力項目の制御
                Call Me._G.SetControlsStatusLayout()

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMD080F) As Boolean

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
    Friend Sub FunctionKey1Press(ByVal frm As LMD080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        '「チェック」処理
        Me.ActionControl(LMD080C.EventShubetsu.CHECK, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMD080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        '「取込」処理
        Me.ActionControl(LMD080C.EventShubetsu.TORIKOMI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMD080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        '「集計」処理
        Me.ActionControl(LMD080C.EventShubetsu.SHUKEI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMD080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        '「照合」処理
        Me.ActionControl(LMD080C.EventShubetsu.SHOGO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMD080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMD080C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMD080F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMD080F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    '''チェック押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnCheck_Click(ByRef frm As LMD080F)

        Logger.StartLog(Me.GetType.Name, "btnCheck_Click")

        '「チェック」処理
        Me.ActionControl(LMD080C.EventShubetsu.CHECK, frm)

        Logger.EndLog(Me.GetType.Name, "btnCheck_Click")

    End Sub

    ''' <summary>
    '''取込押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnTorikomi_Click(ByRef frm As LMD080F)

        Logger.StartLog(Me.GetType.Name, "btnTorikomi_Click")

        '「取込」処理
        Me.ActionControl(LMD080C.EventShubetsu.TORIKOMI, frm)

        Logger.EndLog(Me.GetType.Name, "btnTorikomi_Click")

    End Sub

    ''' <summary>
    '''集計押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnShukei_Click(ByRef frm As LMD080F)

        Logger.StartLog(Me.GetType.Name, "btnShukei_Click")

        '「取込」処理
        Me.ActionControl(LMD080C.EventShubetsu.SHUKEI, frm)

        Logger.EndLog(Me.GetType.Name, "btnShukei_Click")

    End Sub

    ''' <summary>
    '''照合押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnShogo_Click(ByRef frm As LMD080F)

        Logger.StartLog(Me.GetType.Name, "btnShogo_Click")

        '「照合」処理
        Me.ActionControl(LMD080C.EventShubetsu.SHOGO, frm)

        Logger.EndLog(Me.GetType.Name, "btnShogo_Click")

    End Sub

    ''' <summary>
    ''' 荷主コードの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCd_Leave(ByRef frm As LMD080F)

        Logger.StartLog(Me.GetType.Name, "txtCustCd_Leave")

        'DBより該当データの取得処理
        Me.ActionControl(LMD080C.EventShubetsu.CUSTCDCHENGE, frm)

        Logger.EndLog(Me.GetType.Name, "txtCustCd_Leave")

    End Sub

    ''' <summary>
    ''' 荷主在庫レイアウトの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbLayout_Changed(ByRef frm As LMD080F)

        Logger.StartLog(Me.GetType.Name, "cmbLayout_Changed")

        'DBより該当データの取得処理
        Me.ActionControl(LMD080C.EventShubetsu.LAYOUTCHENGE, frm)

        Logger.EndLog(Me.GetType.Name, "cmbLayout_Changed")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' チェック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub CheckData(ByVal frm As LMD080F)

        'DataSet設定
        Dim rtDs As DataSet = New LMD080DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "CheckData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMDConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMD080BLF", _
                                                         "CheckData", _
                                                         rtDs, _
                                                         -1, _
                                                         -1)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'チェック処理後のコントロールの入力制御
        Call Me._G.SetControlsStatusCheck(rtnDs)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"チェック処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "CheckData")

    End Sub

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub TorikomiData(ByVal frm As LMD080F)

        'DataSet設定
        Dim rtDs As DataSet = New LMD080DS()

        'ファイルの取込
        Dim rtnFlg As Boolean = Me._G.GetFileData(frm, Me._Ds, rtDs)
        If rtnFlg = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        If rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows.Count = 0 Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "E469", New String() {"取込対象のデータ"})
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "TorikomiData")

        '==========================
        'WSAクラス呼出
        '==========================
        '==== WSAクラス呼出（削除処理） ====
        rtDs = MyBase.CallWSA("LMD080BLF", "TorikomiData", rtDs)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '取込処理後のコントロールの入力制御
        Call Me._G.SetControlsStatusTorikomi()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"取込処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "TorikomiData")

    End Sub

    ''' <summary>
    ''' 集計処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShukeiData(ByVal frm As LMD080F)

        'DataSet設定
        Dim rtDs As DataSet = New LMD080DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShukeiData")

        '==========================
        'WSAクラス呼出
        '==========================
        '==== WSAクラス呼出（削除処理） ====
        rtDs = MyBase.CallWSA("LMD080BLF", "ShukeiData", rtDs)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '集計処理後のコントロールの入力制御
        Call Me._G.SetControlsStatusShukei()

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"集計処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShukeiData")

    End Sub

    ''' <summary>
    ''' 照合処理(メイン)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShogohData(ByVal frm As LMD080F)

        'DataSet設定
        Dim rtDs As DataSet = New LMD080DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShogohData")

        '==========================
        'WSAクラス呼出
        '==========================
        '==== WSAクラス呼出（照合処理） ====
        rtDs = MyBase.CallWSA("LMD080BLF", "ShogohData", rtDs)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '照合処理を行う
        Dim rtnFlg As Boolean = Me.SetShogohData(frm, rtDs)
        '要望管理1986 2013/4/3 S.kobayashi
        'If rtnFlg = False Then
        '    '処理終了アクション
        '    Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
        '    Exit Sub
        'End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"照合処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShogohData")

    End Sub

    ''' <summary>
    ''' 照合処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function SetShogohData(ByVal frm As LMD080F, ByVal rtDs As DataSet) As Boolean

        '■照合の流れは
        '①個数が一致するデータを探し、CHECK_FLGをオンにする
        '②LMD810INに荷主在庫数データ、荷主在庫数データサマリの値をとりあえず設定する（CHECK_FLGがオンのデータは対象外)
        '③②で設定したLMD810Nに対して、画面の照合キーの項目で並べ替えをする
        '④照合キーが一致するデータの「個数(荷主)」と「個数(NRS)」をそれぞれ設定する

        Dim rowNo As Integer = frm.cmbLayout.SelectedIndex - 1

        Dim inDs As DataSet = New LMD810DS()
        Dim selectSql As String = String.Empty
        Dim selectDr() As DataRow = Nothing
        '荷主在庫数データの件数
        Dim max As Integer = rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows.Count - 1

        '荷主明細マスタの値を取得
        Dim custDetailsDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                         "CUST_CD = '", frm.txtCustCdL.TextValue, frm.txtCustCdM.TextValue, "' AND ", _
                                                                                                                         "SUB_KB = '36' AND SET_NAIYO = '01'"))

        Dim custDetailsDr2 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyo.SelectedValue, "' AND ", _
                                                                                                                         "CUST_CD = '", frm.txtCustCdL.TextValue, frm.txtCustCdM.TextValue, "' AND ", _
                                                                                                                         "SUB_KB = '36' AND SET_NAIYO = '02'"))

        If custDetailsDr.Length > 0 Then
            'ロット№特殊チェックの場合(8Byteでチェック)
            For i As Integer = 0 To max
                rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("LOT_NO") = Me.GetLotNo(rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("LOT_NO").ToString)
            Next
        End If

        Dim Lot As String = String.Empty
        If custDetailsDr2.Length > 0 Then
            'ロット№特殊チェックの場合(ハイフン除去・空白以降撤去)NRS在庫
            For j As Integer = 0 To max

                rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM).Rows(j).Item("LOT_NO") = Replace(rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM).Rows(j).Item("LOT_NO").ToString, "-", "")
                Lot = rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM).Rows(j).Item("LOT_NO").ToString()
                If Lot.IndexOf(" ") > 1 Then
                    rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM).Rows(j).Item("LOT_NO") = Left(Lot, Lot.IndexOf(" "))
                End If

            Next
        End If

        
        '■①
        '個数が一致するデータを探し、CHECK_FLGをオンにする
        For i As Integer = 0 To max

            '荷主商品コード
            selectSql = String.Concat("GOODS_CD_CUST = '", rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("GOODS_CD_CUST").ToString, "'")
            'ロット№
            If frm.chkLotNo.Checked = True Then
                selectSql = String.Concat(selectSql, " AND LOT_NO = '", rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("LOT_NO").ToString, "'")
            End If
            'シリアル№
            If frm.chkSerialNo.Checked = True Then
                selectSql = String.Concat(selectSql, " AND SERIAL_NO = '", rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("SERIAL_NO").ToString, "'")
            End If
            '入目
            If frm.chkIrime.Checked = True Then
                selectSql = String.Concat(selectSql, " AND IRIME = '", rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("IRIME").ToString, "'")
            End If
            '入目単位
            If frm.chkIrimeUt.Checked = True Then
                selectSql = String.Concat(selectSql, " AND IRIME_UT = '", rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("IRIME_UT").ToString, "'")
            End If
            '個数
            selectSql = String.Concat(selectSql, " AND NB = '", rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("NB").ToString, "'")

            '条件に一致するデータを検索
            selectDr = rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM).Select(selectSql)
            If selectDr.Length > 0 Then
                '個数が同じ場合

                '荷主在庫数データのチェックフラグをオンにする
                rtDs.Tables(LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Rows(i).Item("CHECK_FLG") = LMConst.FLG.ON
                '荷主在庫数データサマリのチェックフラグをオンにする
                selectDr(0).Item("CHECK_FLG") = LMConst.FLG.ON
            End If

        Next

        '■②
        'LMD810INにセット(荷主在庫数データ)
        Call Me.SetInShogohData(frm, inDs, rtDs, LMD080C.TABLE_NM_IN_ZAISHOGOHCUST)
        'LMD810INにセット(荷主在庫数データサマリ)
        Call Me.SetInShogohData(frm, inDs, rtDs, LMD080C.TABLE_NM_IN_ZAISHOGOHCUSTSUM)

        '■③
        Dim prmDs As DataSet = New LMD810DS()
        '荷主商品コード
        selectSql = "CUST_GOODS_CD"
        'ロット№
        If frm.chkLotNo.Checked = True Then
            selectSql = String.Concat(selectSql, ",LOT_NO")
        End If
        'シリアル№
        If frm.chkSerialNo.Checked = True Then
            selectSql = String.Concat(selectSql, ",SERIAL_NO")
        End If
        '入目
        If frm.chkIrime.Checked = True Then
            selectSql = String.Concat(selectSql, ",IRIME")
        End If
        '入目単位
        If frm.chkIrimeUt.Checked = True Then
            selectSql = String.Concat(selectSql, ",IRIME_UT")
        End If
        '個数
        selectSql = String.Concat(selectSql, ",NB")
        '区分
        selectSql = String.Concat(selectSql, ",KBN")

        '並び替えした値をprmDsに設定する
        Dim inDr() As DataRow = inDs.Tables(LMD810C.TABLE_NM_IN).Select(Nothing, selectSql)
        max = inDs.Tables(LMD810C.TABLE_NM_IN).Rows.Count - 1
        For i As Integer = 0 To max
            prmDs.Tables(LMD810C.TABLE_NM_IN).ImportRow(inDr(i))
        Next

        '■④
        max = max - 1
        For i As Integer = 0 To max

            '次のカウントのデータと比較していき、照合キーが一致した場合は個数を設定する

            '照合済みレコードの場合は次のデータへ
            If String.IsNullOrEmpty(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("NRS_NB").ToString) = False AndAlso _
                String.IsNullOrEmpty(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("CUST_NB").ToString) = False Then
                Continue For
            End If

            '区分
            If (prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("KBN").ToString).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("KBN").ToString) = True Then
                Continue For
            End If
            '荷主商品コード
            If (prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("CUST_GOODS_CD").ToString).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("CUST_GOODS_CD").ToString) = False Then
                Continue For
            End If
            'ロット№
            If frm.chkLotNo.Checked = True Then
                If (prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("LOT_NO").ToString).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("LOT_NO").ToString) = False Then
                    Continue For
                End If
            End If
            'シリアル№
            If frm.chkSerialNo.Checked = True Then
                If (prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("SERIAL_NO").ToString).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("SERIAL_NO").ToString) = False Then
                    Continue For
                End If
            End If
            '入目
            If frm.chkIrime.Checked = True Then
                If (prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("IRIME").ToString).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("IRIME").ToString) = False Then
                    Continue For
                End If
            End If
            '入目単位
            If frm.chkIrimeUt.Checked = True Then
                If (prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("IRIME_UT").ToString).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("IRIME_UT").ToString) = False Then
                    Continue For
                End If
            End If

            If (LMConst.FLG.ON).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("CHECK_FLG").ToString) = False AndAlso _
                 (LMConst.FLG.ON).Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("CHECK_FLG").ToString) = True Then
                'CHECK_FLGがオフで、次のレコードがCHECK_FLGオンの場合は、個数まで一致するデータが他にあるということなので、ここでは個数の設定はしない
            Else
                If ("01").Equals(prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("KBN").ToString) = True Then
                    prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("NRS_NB") = prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("NRS_NB").ToString
                    prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("CUST_NB") = prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("CUST_NB").ToString
                Else
                    prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("NRS_NB") = prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("NRS_NB").ToString
                    prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i + 1).Item("CUST_NB") = prmDs.Tables(LMD810C.TABLE_NM_IN).Rows(i).Item("CUST_NB").ToString
                End If
            End If

        Next

        '照合キーの設定
        Dim dr As DataRow = prmDs.Tables(LMD810C.TABLE_NM_IN_FLG).NewRow()
        'ロット№
        If frm.chkLotNo.Checked = True Then
            dr.Item("LOT_NO") = "01"
        End If
        'シリアル№
        If frm.chkSerialNo.Checked = True Then
            dr.Item("SERIAL_NO") = "01"
        End If
        '入目
        If frm.chkIrime.Checked = True Then
            dr.Item("IRIME") = "01"
        End If
        '入目単位
        If frm.chkIrimeUt.Checked = True Then
            dr.Item("IRIME_UT") = "01"
        End If
        'データセットに設定
        prmDs.Tables(LMD810C.TABLE_NM_IN_FLG).Rows.Add(dr)


        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs

        'バッチ呼出
        LMFormNavigate.NextFormNavigate(Me, "LMD810", prm)

        Return prm.ReturnFlg

    End Function

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMD080F, ByVal objNM As String, ByRef prm As LMFormData)

        'オブジェクト名による分岐
        Select Case objNM

            Case "txtCustCdL", "txtCustCdM" '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtCustCdL.TextValue
                    row("CUST_CD_M") = frm.txtCustCdM.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                frm.lblCustNmL.TextValue = String.Empty
                frm.lblCustNmM.TextValue = String.Empty

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case Else 'マスタ参照対象項目以外の場合
                MyBase.ShowMessage(frm, "G005")
                Exit Sub
        End Select

        '戻り処理
        If prm.ReturnFlg = True Then
            Select Case objNM

                Case "txtCustCdL", "txtCustCdM" '荷主マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                        frm.lblCustNmL.TextValue = .Item("CUST_NM_L").ToString()      '荷主名（大）
                        frm.txtCustCdM.TextValue = .Item("CUST_CD_M").ToString()      '荷主コード（中）
                        frm.lblCustNmM.TextValue = .Item("CUST_NM_M").ToString()      '荷主名（中）
                    End With

                    '荷主在庫レイアウトの設定
                    Call Me.ShogohMstData(frm)

                    '画面の入力項目の制御
                    Call Me._G.SetControlsStatus()

            End Select

        End If

        MyBase.ShowMessage(frm, "G006")

    End Sub

    ''' <summary>
    ''' 荷主在庫数データ取込制御マスタ取得処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShogohMstData(ByVal frm As LMD080F)

        If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = True OrElse _
            String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = True Then
            '荷主コードが空の場合は、荷主在庫レイアウトをクリアして終了
            frm.cmbLayout.Items.Clear()
            Exit Sub
        End If

        If (frm.txtCustCdL.TextValue).Equals(frm.txtCustCdLOld.TextValue) = False OrElse _
            (frm.txtCustCdM.TextValue).Equals(frm.txtCustCdMOld.TextValue) = False Then
            '荷主コードが変わった時
        Else
            Exit Sub
        End If

        '荷主在庫レイアウトコンボクリア
        frm.cmbLayout.Items.Clear()

        'DataSet設定
        Dim rtDs As DataSet = New LMD080DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShogohMstData")

        Me._Ds = Nothing

        '==========================
        'WSAクラス呼出
        '==========================
        '==== WSAクラス呼出（照合処理） ====
        Me._Ds = MyBase.CallWSA("LMD080BLF", "ShogohMstData", rtDs)

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '荷主在庫レイアウトの設定
        Call Me._G.SetComboLayout(frm, Me._Ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShogohMstData")

    End Sub

    ''' <summary>
    ''' ロット№加工処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetLotNo(ByVal value As String) As String

        Dim rtnValue As String = value

        '前8桁を取得し、さらに「.」を空に置換する
        rtnValue = Mid(rtnValue, 1, 8)
        rtnValue = rtnValue.Replace(".", String.Empty)

        Return rtnValue

    End Function

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G006"
    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMD080F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMD080C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        dr("JISSHI_DATE") = frm.imdJisshiDate.TextValue

        If Me._Ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows.Count > 0 AndAlso _
            frm.cmbLayout.SelectedIndex >= 0 Then
            dr("EDA_NO") = Me._Ds.Tables(LMD080C.TABLE_NM_OUT_ZAISHOGOH).Rows(frm.cmbLayout.SelectedIndex - 1).Item("EDA_NO").ToString
        End If

        '検索条件をデータセットに設定
        rtDs.Tables(LMD080C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(照合結果)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInShogohData(ByVal frm As LMD080F, ByRef inDs As DataSet, ByVal rtDs As DataSet, ByVal tableNm As String)

        Dim dr As DataRow = inDs.Tables(LMD810C.TABLE_NM_IN).NewRow()
        Dim max As Integer = rtDs.Tables(tableNm).Rows.Count - 1

        For i As Integer = 0 To max
            If frm.chkWriteFlg.Checked = True AndAlso _
                (LMConst.FLG.ON).Equals(rtDs.Tables(tableNm).Rows(i).Item("CHECK_FLG").ToString) = True Then
                '個数が同じ場合はEXCEL出力しない場合はLMD810INに設定しない
                Continue For
            End If

            dr = inDs.Tables(LMD810C.TABLE_NM_IN).NewRow()

            '検索条件　単項目部
            dr("NRS_BR_CD") = rtDs.Tables(tableNm).Rows(i).Item("NRS_BR_CD").ToString
            dr("CUST_CD_L") = rtDs.Tables(tableNm).Rows(i).Item("CUST_CD_L").ToString
            dr("CUST_CD_M") = rtDs.Tables(tableNm).Rows(i).Item("CUST_CD_M").ToString
            dr("CUST_GOODS_CD") = rtDs.Tables(tableNm).Rows(i).Item("GOODS_CD_CUST").ToString
            dr("GOODS_NM") = rtDs.Tables(tableNm).Rows(i).Item("GOODS_NM").ToString
            dr("LOT_NO") = rtDs.Tables(tableNm).Rows(i).Item("LOT_NO").ToString
            dr("SERIAL_NO") = rtDs.Tables(tableNm).Rows(i).Item("SERIAL_NO").ToString
            dr("IRIME") = rtDs.Tables(tableNm).Rows(i).Item("IRIME").ToString
            dr("IRIME_UT") = rtDs.Tables(tableNm).Rows(i).Item("IRIME_UT").ToString
            dr("NB") = rtDs.Tables(tableNm).Rows(i).Item("NB").ToString
            dr("CHECK_FLG") = rtDs.Tables(tableNm).Rows(i).Item("CHECK_FLG").ToString

            If (LMD080C.TABLE_NM_IN_ZAISHOGOHCUST).Equals(tableNm) = False Then
                '荷主在庫数データサマリの場合
                dr("NRS_NB") = rtDs.Tables(tableNm).Rows(i).Item("NB").ToString
                dr("KBN") = "01"
            Else
                '荷主在庫数データの場合
                dr("CUST_NB") = rtDs.Tables(tableNm).Rows(i).Item("NB").ToString
                dr("KBN") = "02"
            End If

            'データセットに設定
            inDs.Tables(LMD810C.TABLE_NM_IN).Rows.Add(dr)
        Next

    End Sub

#End Region

#End Region 'Method

End Class
