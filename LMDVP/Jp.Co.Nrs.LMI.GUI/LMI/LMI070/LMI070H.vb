' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 請求
'  プログラムID     :  LMI070H : 請求データ作成 [ダウ・ケミカル用]
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI070ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI070H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI070V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI070G

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
        Dim frm As LMI070F = New LMI070F(Me)

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
        Me._G = New LMI070G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI070V(Me, frm, Me._LMIconV, Me._G)

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI070C.EventShubetsu, ByVal frm As LMI070F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI070C.EventShubetsu.MAKE    '作成

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

            Case LMI070C.EventShubetsu.JIKKO    '実行

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                'EXCEL出力データ作成処理呼び出し
                Call Me.ShowExcel(frm, prm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI070C.EventShubetsu.PRINT    '印刷

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

            Case LMI070C.EventShubetsu.MASTER    'マスタ参照

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
    Friend Function CloseForm(ByVal frm As LMI070F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMI070C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI070F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI070F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    '''作成押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnMake_Click(ByRef frm As LMI070F)

        Logger.StartLog(Me.GetType.Name, "btnMake_Click")

        '「作成」処理
        Me.ActionControl(LMI070C.EventShubetsu.MAKE, frm)

        Logger.EndLog(Me.GetType.Name, "btnMake_Click")

    End Sub

    ''' <summary>
    '''実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnJikko_Click(ByRef frm As LMI070F)

        Logger.StartLog(Me.GetType.Name, "btnJikko_Click")

        '「実行」処理
        Me.ActionControl(LMI070C.EventShubetsu.JIKKO, frm)

        Logger.EndLog(Me.GetType.Name, "btnJikko_Click")

    End Sub

    ''' <summary>
    '''印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMI070F)

        Logger.StartLog(Me.GetType.Name, "btnPrint_Click")

        '「印刷」処理
        Me.ActionControl(LMI070C.EventShubetsu.PRINT, frm)

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
    Private Sub MakeData(ByVal frm As LMI070F)

        'DataSet設定
        Dim rtDs As DataSet = New LMI070DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), _
                                                         "LMI070BLF", _
                                                         "MakeData", _
                                                         rtDs, _
                                                         Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))), _
                                                         -1)

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
    Private Sub ShowPopup(ByVal frm As LMI070F, ByVal objNM As String, ByRef prm As LMFormData)

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

    ''' <summary>
    ''' Excel作成
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowExcel(ByVal frm As LMI070F, ByRef prm As LMFormData)

        'DataSet設定
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        'If ("01").Equals(frm.cmbJikko.SelectedValue) = True OrElse _
        '    ("02").Equals(frm.cmbJikko.SelectedValue) = True Then
        '    prmDs = New LMI800DS
        '    row = prmDs.Tables(LMI800C.TABLE_NM_IN).NewRow
        '    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        '    row("CUST_CD_L") = frm.txtCustCdL.TextValue
        '    row("CUST_CD_M") = frm.txtCustCdM.TextValue

        '    row("DATE_FROM") = frm.imdDateFrom.TextValue
        '    row("DATE_TO") = frm.imdDateTo.TextValue

        '    row("JIKKO_KB") = frm.cmbJikko.SelectedValue

        '    prmDs.Tables(LMI800C.TABLE_NM_IN).Rows.Add(row)
        '    prm.ParamDataSet = prmDs

        '    '保管料・荷役料呼出
        '    LMFormNavigate.NextFormNavigate(Me, "LMI800", prm)

        '    If prm.ReturnFlg = True Then
        '        'メッセージエリアの設定
        '        MyBase.ShowMessage(frm, "G002", New String() {"実行処理", ""})
        '    Else
        '        'メッセージエリアの設定
        '        MyBase.ShowMessage(frm, "E462")
        '        Exit Sub
        '    End If

        'End If

        'If ("01").Equals(frm.cmbJikko.SelectedValue) = True OrElse _
        '    ("03").Equals(frm.cmbJikko.SelectedValue) = True Then
        '    prmDs = New LMI810DS
        '    row = prmDs.Tables(LMI810C.TABLE_NM_IN).NewRow
        '    row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        '    row("CUST_CD_L") = frm.txtCustCdL.TextValue
        '    row("CUST_CD_M") = frm.txtCustCdM.TextValue

        '    row("DATE_FROM") = frm.imdDateFrom.TextValue
        '    row("DATE_TO") = frm.imdDateTo.TextValue

        '    row("JIKKO_KB") = frm.cmbJikko.SelectedValue

        '    prmDs.Tables(LMI810C.TABLE_NM_IN).Rows.Add(row)
        '    prm.ParamDataSet = prmDs

        '    '運賃呼出
        '    LMFormNavigate.NextFormNavigate(Me, "LMI810", prm)

        '    If prm.ReturnFlg = True Then
        '        'メッセージエリアの設定
        '        MyBase.ShowMessage(frm, "G002", New String() {"実行処理", ""})
        '    Else
        '        'メッセージエリアの設定
        '        MyBase.ShowMessage(frm, "E462")
        '        Exit Sub
        '    End If
        'End If

        If ("04").Equals(frm.cmbJikko.SelectedValue) = True OrElse _
            ("05").Equals(frm.cmbJikko.SelectedValue) = True OrElse _
            ("06").Equals(frm.cmbJikko.SelectedValue) = True OrElse _
            ("07").Equals(frm.cmbJikko.SelectedValue) = True Then
            prmDs = New LMI820DS
            row = prmDs.Tables(LMI820C.TABLE_NM_IN).NewRow
            row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            row("CUST_CD_L") = frm.txtCustCdL.TextValue
            row("CUST_CD_M") = frm.txtCustCdM.TextValue

            row("DATE_FROM") = frm.imdDateFrom.TextValue
            row("DATE_TO") = frm.imdDateTo.TextValue

            row("JIKKO_KB") = frm.cmbJikko.SelectedValue

            prmDs.Tables(LMI820C.TABLE_NM_IN).Rows.Add(row)
            prm.ParamDataSet = prmDs

            '運賃呼出
            LMFormNavigate.NextFormNavigate(Me, "LMI820", prm)

            If prm.ReturnFlg = True Then
                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G002", New String() {"実行処理", ""})
            Else
                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "E465")
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintData(ByVal frm As LMI070F)

        'DataSet設定
        Dim rtnDs As DataSet = New LMI070DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtnDs)

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        'サーバに渡すレコードが存在する場合、更新処理
        If 0 < rtnDs.Tables(LMI070C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMI070BLF", "PrintData", rtnDs)

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
    Private Sub SetInData(ByVal frm As LMI070F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI070C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        dr("DATE_FROM") = frm.imdDateFrom.TextValue
        dr("DATE_TO") = frm.imdDateTo.TextValue

        dr("MAKE_KB") = frm.cmbMake.SelectedValue
        dr("JIKKO_KB") = frm.cmbJikko.SelectedValue
        dr("PRINT_KB1") = frm.cmbPrint1.SelectedValue
        dr("PRINT_KB2") = frm.cmbPrint2.SelectedValue

        '検索条件をデータセットに設定
        rtDs.Tables(LMI070C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

#End Region 'Method

End Class
