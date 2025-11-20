' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI360  : ＤＩＣ運賃請求明細書作成
'  作  成  者       :  [篠原]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI360ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI360H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI360V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI360G

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
        Dim frm As LMI360F = New LMI360F(Me)

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
        Me._G = New LMI360G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI360V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI360C.EventShubetsu, ByVal frm As LMI360F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI360C.EventShubetsu.MAKE    '作成

                '処理開始アクション
                Me._LMIconH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '作成処理
                Call Me.MakeData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

                'Case LMI360C.EventShubetsu.JIKKO    '実行

                '    '処理開始アクション
                '    Me._LMIConH.StartAction(frm)

                '    '入力チェック
                '    If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                '       Me._V.IsKanrenCheck(eventShubetsu) = False Then
                '        '処理終了アクション
                '        Me._LMIConH.EndAction(frm)
                '        Exit Sub
                '    End If

                '    'CSV出力データ作成処理呼び出し
                '    Call Me.ShowCSV(frm, prm)

                '    '処理終了アクション
                '    Me._LMIConH.EndAction(frm)

            Case LMI360C.EventShubetsu.PRINT    '印刷

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '印刷処理
                Call Me.PrintData(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI360C.EventShubetsu.MASTER    'マスタ参照

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '荷主コード
                Call Me.ShowPopup(frm, objNm, prm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI360F) As Boolean

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
    Friend Sub FunctionKey1Press(ByVal frm As LMI360F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Me.ActionControl(LMI360C.EventShubetsu.MAKE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI360F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMI360C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI360F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI360F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '''' <summary>
    ''''実行押下時処理呼び出し
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Friend Sub btnJikko_Click(ByRef frm As LMI360F)

    '    Logger.StartLog(Me.GetType.Name, "btnJikko_Click")

    '    '「実行」処理
    '    Me.ActionControl(LMI360C.EventShubetsu.JIKKO, frm)

    '    Logger.EndLog(Me.GetType.Name, "btnJikko_Click")

    'End Sub

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI360F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LMI360C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPrint_Click")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub MakeData(ByVal frm As LMI360F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI360DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI360BLF", "MakeData", rtDs)

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"作成処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MakeData")

    End Sub

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMI360F, ByVal objNM As String, ByRef prm As LMFormData)

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

            End Select

        End If

        MyBase.ShowMessage(frm, "G006")

    End Sub

    '''' <summary>
    '''' CSV作成
    '''' </summary>
    '''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    '''' <remarks></remarks>
    'Private Sub ShowCSV(ByVal frm As LMI360F, ByRef prm As LMFormData)

    '    'DataSet設定
    '    Dim prmDs As DataSet = Nothing
    '    Dim row As DataRow = Nothing

    '    prmDs = New LMI870DS
    '    row = prmDs.Tables(LMI870C.TABLE_NM_IN).NewRow
    '    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
    '    row("DATE_FROM") = frm.imdDateFrom.TextValue
    '    row("DATE_TO") = frm.imdDateTo.TextValue

    '    prmDs.Tables(LMI870C.TABLE_NM_IN).Rows.Add(row)
    '    prm.ParamDataSet = prmDs

    '    'CSV作成処理呼出
    '    LMFormNavigate.NextFormNavigate(Me, "LMI870", prm)

    '    If prm.ReturnFlg = True Then
    '        'メッセージエリアの設定
    '        MyBase.ShowMessage(frm, "G002", New String() {"実行処理", ""})
    '    Else
    '        'メッセージエリアの設定
    '        MyBase.ShowMessage(frm, "E430")
    '        Exit Sub
    '    End If

    'End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMI360F)

        'DataSet設定
        Dim rtnDs As DataSet = New LMI360DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtnDs)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtnDs.Tables(LMI360C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMI360BLF", "PrintData", rtnDs)

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

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMI360F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI360C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        dr("DATE_FROM") = frm.imdDateFrom.TextValue
        dr("DATE_TO") = frm.imdDateTo.TextValue

        'dr("JIKKO_KB") = frm.cmbJikko.SelectedValue
        dr("PRINT_KB") = frm.cmbPrint.SelectedValue
        'dr("PRINT_KB2") = frm.cmbRosen.SelectedValue

        '検索条件をデータセットに設定
        rtDs.Tables(LMI360C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

#End Region 'Method

End Class
