' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME030  : 作業指示書検索
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LME030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LME030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LME030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LME030G

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
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' 実行時の更新条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _JikkouDs As DataSet

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
        Dim frm As LME030F = New LME030F(Me)

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
        Me._G = New LME030G(Me, frm)

        'Validateクラスの設定
        Me._V = New LME030V(Me, frm, Me._LMEconV, Me._G)

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

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

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
    Friend Sub ActionControl(ByVal eventShubetsu As LME030C.EventShubetsu, ByVal frm As LME030F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LME030C.EventShubetsu.SINKI    '新規

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                '「新規」処理の場合、荷主マスタ参照画面を開く(モーダレス)
                Call Me.ShowPopup(frm, LME030C.EventShubetsu.SINKI, prm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME030C.EventShubetsu.KENSAKU    '検索

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                '検索処理
                Call Me.KensakuData(frm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME030C.EventShubetsu.MASTER    'マスタ参照

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                'ポップアップ表示処理
                Call Me.ShowPopup(frm, LME030C.EventShubetsu.MASTER, prm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME030C.EventShubetsu.DOUBLECLICK     'ダブルクリック

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                '編集画面を開く
                Call Me.ShowLME040(frm, prm)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME030C.EventShubetsu.JIKKOU     '実行ボタン押下

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                If Me._V.IsJikkouInputCheck(Me._ChkList) = False Then
                    Call Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                Select Case frm.cmbJikkou.SelectedValue.ToString

                    Case "01"   '現場作業指示

                        '入力チェック
                        If Me._V.IsSingleCheck(eventShubetsu) = False Then
                            '処理終了アクション
                            Me._LMEConH.EndAction(frm)
                            Exit Sub
                        End If

                        Me._ChkList = Me._V.GetCheckList()

                        Call Me.WHSagyoShiji(frm, Me._ChkList, prm)

                End Select

                '処理終了アクション
                Me._LMEConH.EndAction(frm)

            Case LME030C.EventShubetsu.COMPLETE     '完了

                '処理開始アクション
                Me._LMEConH.StartAction(frm)

                Me._ChkList = Me._V.GetCheckList()

                '項目チェック
                If Me._V.IsKanryoSingleCheck(Me._ChkList, Me._G) = False Then
                    '処理終了アクション
                    Me._LMEConH.EndAction(frm)
                    Exit Sub
                End If

                '完了画面を開く
                Call Me.ShowLMR010(frm, prm, Me._ChkList)

                '処理終了アクション
                Me._LMEConH.EndAction(frm)
        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LME030F) As Boolean

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
    Friend Sub FunctionKey1Press(ByVal frm As LME030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Me.ActionControl(LME030C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LME030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Me.ActionControl(LME030C.EventShubetsu.COMPLETE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LME030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Me.ActionControl(LME030C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LME030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LME030C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LME030F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LME030F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' スプレッドロフトフォーカス
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sprDetails_DoubleClick(ByVal frm As LME030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetails_DoubleClick")

        Me.ActionControl(LME030C.EventShubetsu.DOUBLECLICK, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetails_DoubleClick")

    End Sub


    ''' <summary>
    ''' 実行ボタン押下処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnJikkou_Click(ByVal frm As LME030F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnJikkou_Click")

        Me.ActionControl(LME030C.EventShubetsu.JIKKOU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnJikkou_Click")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LME030F, ByVal eventShubetsu As LME030C.EventShubetsu, ByRef prm As LMFormData)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        'イベント種別による分岐
        Select Case eventShubetsu
            Case LME030C.EventShubetsu.SINKI '新規
                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                row("CUST_CD_L") = frm.txtCustCdL.TextValue
                row("CUST_CD_M") = frm.txtCustCdM.TextValue
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                Me._PopupSkipFlg = False '1件表示なし
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case Else
                'オブジェクト名による分岐
                Select Case objNm
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

                    Case Else
                        'ポップ対象外のテキストの場合
                        MyBase.ShowMessage(frm, "G005")
                        Exit Sub

                End Select

        End Select


        '戻り処理
        If prm.ReturnFlg = True Then
            'イベント種別による分岐
            Select Case eventShubetsu
                Case LME030C.EventShubetsu.SINKI '新規
                    'inputDataSet作成
                    Dim prmDs As DataSet = Me.SetDataSetLME040InData(frm, prm.ParamDataSet, LME030C.EventShubetsu.SINKI)
                    prm.ParamDataSet = prmDs
                    prm.RecStatus = RecordStatus.NEW_REC

                    '画面遷移
                    LMFormNavigate.NextFormNavigate(Me, "LME040", prm)

                Case Else
                    'オブジェクト名による分岐
                    Select Case objNm
                        Case "txtCustCdL", "txtCustCdM" '荷主マスタ参照

                            'PopUpから取得したデータをコントロールにセット
                            With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                                frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                                frm.lblCustNmL.TextValue = .Item("CUST_NM_L").ToString()      '荷主名（大）
                                frm.txtCustCdM.TextValue = .Item("CUST_CD_M").ToString()      '荷主コード（中）
                                frm.lblCustNmM.TextValue = .Item("CUST_NM_M").ToString()      '荷主名（中）
                            End With

                    End Select

            End Select

        End If

        MyBase.ShowMessage(frm, "G006")

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KensakuData(ByVal frm As LME030F)

        'DataSet設定
        Dim rtDs As DataSet = New LME030DS()

        'スプレッドの行をクリア
        frm.sprDetails.CrearSpread()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectKensakuData")

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))
        MyBase.SetLimitCount(lc)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LME030BLF", "SelectKensakuData", rtDs)

        If MyBase.IsWarningMessageExist() = True Then         'Warningの場合
            'メッセージを表示し、戻り値により処理を分ける
            If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択
                '処理を続ける
            Else    '「いいえ」を選択
                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G007")
                Exit Sub
            End If
        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        '取得した値を一覧に追加
        If 0 < rtnDs.Tables(LME030C.TABLE_NM_OUT).Rows.Count Then
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        If 0 < frm.sprDetails.ActiveSheet.Rows.Count Then
            'メッセージエリアの設定
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})
            MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F9ButtonName, ""})
            '2016.01.06 UMANO 英語化対応END
        Else
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "E024")
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectKensakuData")

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LME030F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LME030C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

    End Sub

    ''' <summary>
    ''' 編集画面を開く
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>編集画面を開く</remarks>
    Private Sub ShowLME040(ByVal frm As LME030F, ByRef prm As LMFormData)

        'inputDataSet作成
        Dim prmDs As DataSet = Me.SetDataSetLME040InData(frm, prm.ParamDataSet, LME030C.EventShubetsu.DOUBLECLICK)
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LME040", prm)

    End Sub


#Region "タブレット対応"
    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub WHSagyoShiji(ByVal frm As LME030F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        Me._JikkouDs = New LME800DS
        For i As Integer = 0 To arr.Count - 1

            '検品済の場合データセットに登録
            Dim dRow As DataRow = Me._JikkouDs.Tables("LME800IN").NewRow

            dRow.Item("NRS_BR_CD") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.NRSBRCD.ColNo))
            dRow.Item("SAGYO_SIJI_NO") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.SAGYOSIJINO.ColNo))
            dRow.Item("ROW_NO") = Convert.ToInt32(arr(i))
            dRow.Item("PGID") = MyBase.GetPGID
            dRow.Item("SYS_UPD_DATE") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.SYSUPDDATE.ColNo))
            dRow.Item("SYS_UPD_TIME") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.SYSUPDTIME.ColNo))
            dRow.Item("WH_TAB_STATUS_KB") = LME800C.WH_TAB_SIJI_STATUS.INSTRUCTED
            dRow.Item("PROC_TYPE") = LME800C.PROC_TYPE.INSTRUCT

            Me._JikkouDs.Tables("LME800IN").Rows.Add(dRow)

        Next

        '処理呼出
        prm.ParamDataSet = Me._JikkouDs

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

    ''' <summary>
    ''' 編集画面を開く
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>編集画面を開く</remarks>
    Private Sub ShowLMR010(ByVal frm As LME030F, ByRef prm As LMFormData, ByVal arr As ArrayList)

        'inputDataSet作成
        Dim prmDs As DataSet = Me.SetDataSetLMR010InData(frm, arr)
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LMR010", prm)

    End Sub

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LME030F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LME030C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr("CUST_CD_M") = frm.txtCustCdM.TextValue
        dr("DATE_FROM") = frm.imdDateFrom.TextValue
        dr("DATE_TO") = frm.imdDateTo.TextValue
        dr("SAGYO_SIJI_NO") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LME030G.sprDetailsDef.SAGYOSIJINO.ColNo)) '作業指示書番号
        dr("CUST_NM_L") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LME030G.sprDetailsDef.CUSTNM.ColNo)) '荷主名
        dr("GOODS_NM") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LME030G.sprDetailsDef.GOODSNM.ColNo)) '商品名
        dr("SAGYO_NM") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LME030G.sprDetailsDef.SAGYONM.ColNo)) '作業名
        dr("WH_TAB_STATUS") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LME030G.sprDetailsDef.WHTABSTATUSNM.ColNo))  '現場作業指示ステータス
        If frm.chkStaIncomplete.Checked = True Then
            dr("SAGYO_SIJI_STATUS1") = LMConst.FLG.ON
        Else
            dr("SAGYO_SIJI_STATUS1") = LMConst.FLG.OFF
        End If
        If frm.chkStaCompletion.Checked = True Then
            dr("SAGYO_SIJI_STATUS2") = LMConst.FLG.ON
        Else
            dr("SAGYO_SIJI_STATUS2") = LMConst.FLG.OFF
        End If
        '検索条件をデータセットに設定
        rtDs.Tables(LME030C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(LME040引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLME040InData(ByVal frm As LME030F, ByVal prmDs As DataSet, ByVal eventShubetsu As LME030C.EventShubetsu) As DataSet

        Dim ds As DataSet = New LME040DS()
        Dim dr As DataRow = ds.Tables(LME040C.TABLE_NM_IN).NewRow()

        Select Case eventShubetsu

            Case LME030C.EventShubetsu.SINKI '新規

                With prmDs.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                    dr.Item("NRS_BR_CD") = .Item("NRS_BR_CD") '営業所コード
                    dr.Item("CUST_CD_L") = .Item("CUST_CD_L") '荷主コード（大）
                    dr.Item("CUST_CD_M") = .Item("CUST_CD_M") '荷主コード（中）
                End With

            Case LME030C.EventShubetsu.DOUBLECLICK 'ダブルクリック

                With frm.sprDetails.Sheets(0)
                    dr("NRS_BR_CD") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(.ActiveRowIndex, LME030G.sprDetailsDef.NRSBRCD.ColNo)) '営業所コード
                    dr("CUST_CD_L") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(.ActiveRowIndex, LME030G.sprDetailsDef.CUSTCDL.ColNo)) '荷主コード(大)
                    dr("CUST_CD_M") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(.ActiveRowIndex, LME030G.sprDetailsDef.CUSTCDM.ColNo)) '荷主コード(中)
                    dr("SAGYO_SIJI_NO") = Me._LMEconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(.ActiveRowIndex, LME030G.sprDetailsDef.SAGYOSIJINO.ColNo)) '作業指示書番号
                End With

        End Select

        ds.Tables(LME040C.TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function


    ''' <summary>
    ''' 完了画面のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetLMR010InData(ByVal frm As LME030F, ByVal arr As ArrayList) As DataSet

        Dim ds As DataSet = New LMR010DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMR010C_TABLE_NM_IN)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetails
        For i As Integer = 0 To max

            dr = dt.NewRow()
            With dr

                .Item("NRS_BR_CD") = Me._LMEconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.NRSBRCD.ColNo))
                .Item("INOUTKA_NO_L") = Me._LMEconV.GetCellValue(spr.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LME030G.sprDetailsDef.SAGYOSIJINO.ColNo))
                .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            End With
            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

#End Region

#End Region 'Method

End Class
