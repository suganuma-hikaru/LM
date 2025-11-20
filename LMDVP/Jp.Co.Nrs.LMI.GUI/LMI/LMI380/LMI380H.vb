' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI380  : 住化カラー実績報告データ作成
'  作  成  者       :  [馬野]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI380ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI380H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI380V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI380G

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
        Dim frm As LMI380F = New LMI380F(Me)

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
        Me._G = New LMI380G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMI380V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        ''営業所,倉庫コンボ関連設定
        'MyBase.CreateSokoCombData(frm.cmbEigyo, Nothing)

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI380C.EventShubetsu, ByVal frm As LMI380F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI380C.EventShubetsu.JIKKO    '実行

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                'CSV出力データ作成処理呼び出し
                Call Me.ShowCSV(frm, prm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI380C.EventShubetsu.MASTER    'マスタ参照

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
    Friend Function CloseForm(ByVal frm As LMI380F) As Boolean

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
    Friend Sub FunctionKey10Press(ByVal frm As LMI380F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMI380C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI380F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI380F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    '''実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnJikko_Click(ByRef frm As LMI380F)

        Logger.StartLog(Me.GetType.Name, "btnJikko_Click")

        '「実行」処理
        Me.ActionControl(LMI380C.EventShubetsu.JIKKO, frm)

        Logger.EndLog(Me.GetType.Name, "btnJikko_Click")

    End Sub

    ''' <summary>
    ''' オプション選択時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub opt_Check(ByVal frm As LMI380F)

        Call Me._G.SetControlsStatus()

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"


    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMI380F, ByVal objNM As String, ByRef prm As LMFormData)

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
                row("HYOJI_KBN") = LMZControlC.HYOJI_SS
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
    ''' TXT作成
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowCSV(ByVal frm As LMI380F, ByRef prm As LMFormData)

        'DataSet設定
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        prmDs = New LMI930DS
        row = prmDs.Tables(LMI930C.TABLE_NM_IN).NewRow

        If frm.optOneNrsBr.Checked = True Then
            row("OUTPUT_TANI_KBN") = "02"
        ElseIf frm.optOneCust.Checked = True Then
            row("OUTPUT_TANI_KBN") = "03"
        End If
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        row("CUST_CD_L") = frm.txtCustCdL.TextValue
        row("CUST_CD_M") = frm.txtCustCdM.TextValue
        If frm.optAllJisseki.Checked = True Then
            row("OUTPUT_TAISYO_KBN") = "01"
        ElseIf frm.optOutkaPlanJisseki.Checked = True Then
            row("OUTPUT_TAISYO_KBN") = "02"
        End If
        row("OUTKA_PLAN_DATE_FROM") = frm.imdOutkaDateFrom.TextValue
        row("OUTKA_PLAN_DATE_TO") = frm.imdOutkaDateTo.TextValue

        prmDs.Tables(LMI930C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'TXT作成処理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMI930", prm)

        If prm.ReturnFlg = True Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G002", New String() {"実行処理", ""})
        Else
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "E465")
            Exit Sub
        End If

    End Sub


#End Region

#Region "DataSet設定"

#End Region

#End Region 'Method

End Class
