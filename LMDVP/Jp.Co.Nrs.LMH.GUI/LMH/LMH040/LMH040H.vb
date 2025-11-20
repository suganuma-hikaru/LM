' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH040H : EDI出荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ' 入力チェックで使用するValidateクラスを格納するフィールド
    Private _V As LMH040V

    ' Gamenクラスを格納するフィールド
    Private _G As LMH040G

    ' Validate共通クラスを格納するフィールド
    Private _LMHconV As LMHControlV

    ' Handler共通クラスを格納するフィールド
    Private _LMHconH As LMHControlH

    ' G共通クラスを格納するフィールド
    Private _LMHconG As LMHControlG

    '値の保持
    Private _Ds As DataSet

    'クリック行番号の保持
    Private _RowNum As Integer

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
        'Dim prmDs As DataSet = prm.ParamDataSet

        '保存用データセット処理
        Me._Ds = prm.ParamDataSet
        Me._Ds.Tables(LMH040C.TABLE_NM_IN_FIX).ImportRow(Me._Ds.Tables(LMH040C.TABLE_NM_IN).Rows(0))

        'フォームの作成
        Dim frm As LMH040F = New LMH040F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMHconG = New LMHControlG(sFrom)

        'Validate共通クラスの設定
        Me._LMHconV = New LMHControlV(Me, sFrom, Me._LMHconG)

        'Hnadler共通クラスの設定
        Me._LMHconH = New LMHControlH(sFrom, MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMH040G(Me, frm, Me._LMHconG, Me._LMHconV)

        'Validateクラスの設定
        Me._V = New LMH040V(Me, frm, Me._LMHconV, Me._G, Me._LMHconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.SetSpread(Me._Ds)

        '初期値設定(スプレッド)
        'Me._G.SetInitValue()

        'ロード処理
        If Me.SetForm(frm) = False Then
            Call Me._G.SetControlsStatus(RecordStatus.NOMAL_REC, LMH040C.MODE_DEFAULT)
            frm.FunctionKey.F2ButtonEnabled = False '編集ボタン無効
            MyBase.ShowMessage(frm, "G001")
        Else
            'メッセージの表示
            Call Me.SetInitMessage(frm)
        End If
        'START YANAI EDIメモNo.43
        Call Me._G.EventLockControlM(True)
        'END YANAI EDIメモNo.43

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' ロード処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>
    ''' True ：検索成功
    ''' false：検索失敗
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetForm(ByVal frm As LMH040F) As Boolean

        Dim rtnResult As Boolean = False
        Dim status As String = String.Empty

        '初期検索処理
        Me._Ds = Me._LMHconH.ServerAccess(Me._Ds, LMH040C.ACTION_ID_SEARCH)

        '検索成功
        If Me._Ds Is Nothing = False _
            AndAlso 0 < Me._Ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows.Count Then

            rtnResult = True

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()

            'ステータス設定
            Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

            '初期表示時の値設定
            Call Me._G.SetHeaderData(Me._Ds)
            Call Me._G.SetSpread(Me._Ds)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(RecordStatus.NOMAL_REC, LMH040C.MODE_DEFAULT)

            'メッセージ表示
            Call Me.SetInitMessage(frm)

            'イベント追加
            AddHandler frm.cmbUnsoTehaiKB.SelectedValueChanged, AddressOf changeTariffJyouho

            'START EDIメモNo.43
            'クリック行番号格納
            Me._RowNum = 0

            '選択データを中データ部に出力
            Call Me._G.SetDetailData(Me._Ds)

            '自由項目（中）スプレッド設定
            Call Me._G.SetFreeMSpread(Me._Ds)
            'END EDIメモNo.43

        End If

        Return rtnResult

    End Function

#End Region '初期処理

#Region "外部メソッド"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMH040F)

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH040C.ActionType.EDIT)

        '入力チェック（ステータスチェック）
        rtnResult = rtnResult AndAlso Me._V.IsStaChk()

        '入力チェック（取込日チェック）
        rtnResult = rtnResult AndAlso Me.IsTorikomiDateChk(frm)

        '排他チェック（EDI出荷データ（大）のみ）
        Me._Ds = Me._LMHconH.ServerAccess(Me._Ds, LMH040C.ACTION_ID_EDIT_HAITA) 'サーバアクセス

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            rtnResult = False
        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus(RecordStatus.NOMAL_REC, LMH040C.MODE_EDIT, Me._Ds)

        'ロック制御用に値の再設定
        'Call Me._G.SetSpread(Me._Ds)

        'START EDIメモNo.43
        Call Me._G.EventLockControlM(False)

        'START EDI
        Call Me._G.SetFreeMSpread(Me._Ds)
        'END EDI

        'ロック制御を行う
        Call Me._G.SetGooDsLock(False)
        Call Me._G.IrisuLockControl()
        Call Me._G.OutkaLockControl()
        Call Me._G.UnsoLockControl()
        'END EDIメモNo.43

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' セルダブルクリック処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMH040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'スプレッドヘッダー選択の場合、スルー
        If e.ColumnHeader = True Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        If (String.Empty.Equals(frm.txtEDICtlNOChu.TextValue.Trim) = False OrElse _
           Me._RowNum = Not Nothing) AndAlso frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then

            '格納対象データの入力チェック
            If Me._V.IsMDatasetInputCheck() = False Then
                '処理終了アクション
                Call Me._LMHconH.EndAction(frm)
                Exit Sub
            End If

            '現在編集中のデータをデータセットに格納
            If Me.SaveEditDataAction(frm, Me._Ds, Me._RowNum) = False Then
                '処理終了アクション
                Call Me._LMHconH.EndAction(frm)
                Exit Sub
            End If

        End If

        '基本メッセージ表示
        Call Me.SetInitMessage(frm)

        'クリック行番号格納
        Me._RowNum = frm.sprGoodsDef.ActiveSheet.ActiveRowIndex()

        '選択データを中データ部に出力
        Call Me._G.SetDetailData(Me._Ds)

        '自由項目（中）スプレッド設定
        Call Me._G.SetFreeMSpread(Me._Ds)

        'ロック制御
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then   '（編集モード）

            ''クリックした中データの「状態」が"通常"以外の場合、ロック制御
            'With frm.sprGoodsDef.ActiveSheet
            '    If .Cells(.ActiveRowIndex, LMH040C.SprMainColumnIndex.DEL).Value.ToString().Equals(LMH040C.DEL_KB_OK_NM) = False Then
            '        Call Me._G.EventLockControlM(True)
            '        Exit Sub
            '    Else
            '        Call Me._G.EventLockControlM(False)
            '    End If
            'End With

            Call Me._G.EventLockControlM(False)

            'ロック制御を行う
            Call Me._G.SetGooDsLock(False)
            Call Me._G.IrisuLockControl()
            Call Me._G.OutkaLockControl()

        Else  '（参照モード）

            Call Me._G.EventLockControlM(True)

        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' データ検索処理（初期表示・再描画）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMH040F)

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH040C.ActionType.KENSAKU)

        '入力チェックはなし

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMHconH.EndAction(frm)
            Exit Sub
        End If

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(Me._LMHconG.GetLimitData())

        '検索条件の設定なし（INデータ固定）

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMH040C.ACTION_ID_SEARCH, Me._Ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'メッセージエリアの設定(0件エラー)
            MyBase.ShowMessage(frm)
            Exit Sub

        Else

            '初期メッセージ表示
            Call Me.SetInitMessage(frm)

            '値の再設定
            Call Me._G.SetSpread(Me._Ds)

        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SaveAction(ByVal frm As LMH040F) As Boolean

        If (String.Empty.Equals(frm.txtEDICtlNOChu.TextValue.Trim) = False OrElse _
           Me._RowNum = Not Nothing) AndAlso frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then

            '編集中のデータをデータセットに格納
            If Me.SaveEditDataAction(frm, Me._Ds, Me._RowNum) = False Then
                Return False
            End If

        End If

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH040C.ActionType.SAVE)

        '▼▼▼要望番号:466
        '中情報の合計値チェック
        rtnResult = rtnResult AndAlso Me._V.IsMDataSumChk(Me._Ds)
        '▲▲▲要望番号:466

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsEditInputCheck()

        '入力チェック（ステータスチェック）
        rtnResult = rtnResult AndAlso Me._V.IsStaChk()

        '取込日チェック
        rtnResult = rtnResult AndAlso Me.IsTorikomiDateChk(frm)

        '確認メッセージ表示
        'rtnResult = rtnResult AndAlso Me._LMHconH.SetMessageC001(frm, Me._LMHconV.SetRepMsgData(frm.FunctionKey.F11ButtonName))

        '保存処理
        If rtnResult = True Then

            '（大）データをデータセットに設定
            Me._Ds = Me._G.SetHeaderDataSet(Me._Ds)
            Me._Ds = Me._G.SaveEditLSprDate(Me._Ds)

            'サーバアクセス
            Me._Ds = Me._LMHconH.ServerAccess(Me._Ds, LMH040C.ACTION_ID_SAVE)

            'エラーがある場合、メッセージ設定
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(frm)
                rtnResult = False
            End If

        End If

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

        'エラーの場合、処理を終了
        If rtnResult = False Then
            Return False
        End If

        '画面を参照モードにする
        Call Me._G.SetControlsStatus(RecordStatus.NOMAL_REC, LMH040C.MODE_REF)

        '処理成功メッセージ表示
        Call Me._LMHconH.SetMessageG002(frm, "保存処理", "")

        'START YANAI EDIメモNo.55
        Return True
        'END YANAI EDIメモNo.55

    End Function

    ''' <summary>
    ''' 届先登録
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SaveDestMaster(ByVal frm As LMH040F) As Boolean

        '権限
        If Me._V.IsAuthority(LMH040C.ActionType.SINKI) = False Then
            Return False
        End If

        '入力チェック
        If Me._V.IsShinkiInputCheck() = False Then
            Return False
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        '届先マスタメンテ起動
        Call SetReturnDestMstmen(frm, prm)

        Return True

    End Function

    ''' <summary>
    ''' ボタンクリックイベントコントロール
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub BtnClickControl(ByVal frm As LMH040F, ByVal objNm As String)

        Dim ptn As LMH040C.ActionType = Nothing

        '処理開始アクション
        Call Me._LMHconH.StartAction(frm)

        Select Case objNm

            Case frm.btnDestSinki.Name
                '新規
                'Call Me.SaveDestMaster(frm)
                If Me.SaveDestMaster(frm) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G003")
                End If
            Case frm.btnRowDelM.Name
                '行削除
                'Call Me.RowDelAction(frm)
                If Me.RowDelAction(frm) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G003")
                End If
            Case frm.btnRowReM.Name
                '行復活
                'Call Me.RowReAction(frm)
                If Me.RowReAction(frm) = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G003")
                End If

        End Select

        '処理終了アクション
        Call Me._LMHconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMH040F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH040C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMH040C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '項目チェック：１件時表示あり
        Me._PopupSkipFlg = True

        '▼▼▼(EDIﾒﾓNo.67)
        If Me.ShowPopupControl(frm, objNm, LMH040C.ActionType.MASTEROPEN) = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

        End If
        '▲▲▲(EDIﾒﾓNo.67)

        '▼▼▼(EDIﾒﾓNo.67)
        '入数が１以下の場合、梱数をロック
        If Convert.ToInt32(frm.numPkgNB.TextValue.Trim()) <= 1 Then
            frm.numOutkaPkgNB.ReadOnly = True
        End If
        '▲▲▲(EDIﾒﾓNo.67)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMH040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)

        '参照の場合、Tab移動して終了
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            Call Me._LMHconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'Enterキー判定
        Dim rtnResult As Boolean = eventFlg

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMH040C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMH040C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMHconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        '項目チェック：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMH040C.ActionType.ENTER)

        'フォーカス移動処理
        Call Me._LMHconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMH040F, ByVal e As FormClosingEventArgs)

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
            Exit Sub
        End If

        If Not frm.FunctionKey.F11ButtonEnabled Then
            ' 保存ボタンが無効の場合
            ' 処理終了
            Exit Sub
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                '保存処理
                If Me.SaveAction(frm) = False Then
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#End Region '外部メソッド

    ''' <summary>
    ''' 計算処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function CalcActionControl(ByRef frm As LMH040F) As Boolean

        If frm.optCnt.Checked = True Then
            If Me._G.SetCalData() = False Then
                Return False
            End If
        Else

            If Me._V.IsInputMConnectionChk(True) = False Then
                Return False
            End If

            If Me._G.SetCalDataQT() = False Then
                Return False
            End If
        End If

        Return True

    End Function

    Private Sub CalcActionControlOptChange()

        Call Me._G.SetCalDataOptChange()

    End Sub

    ''' <summary>
    ''' 編集中のデータをデータセットに格納
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEditDataAction(ByVal frm As LMH040F, ByRef ds As DataSet, ByVal rownum As Integer) As Boolean

        '入力チェック
        If Me._V.IsMDatasetInputCheck() = False Then
            Return False
        End If

        If Me.CalcActionControl(frm) = False Then
            Return False
        End If

        '編集中のデータをデータセットに格納
        If String.IsNullOrEmpty(frm.txtEDICtlNOChu.TextValue()) = False Then
            ds = Me._G.SaveEditData(ds, rownum)
            ds = Me._G.SaveEditMSprDate(ds, rownum)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 取込日付チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>サーバー接続なのでここで処理</remarks>
    Friend Function IsTorikomiDateChk(ByVal frm As LMH040F) As Boolean

        ' ====== TODO : 復活可能性あり =====

        ''日付取得
        'Me._Ds = Me._LMHconH.ServerAccess(Me._Ds, LMH040C.ACTION_ID_TORIKOMI_CHECK) 'サーバアクセス

        ''0件の場合、処理スキップ
        'If Me._Ds.Tables(LMH040C.TABLE_NM_TORIKOMI_DATE).Rows.Count() < 1 Then
        '    Return True
        'End If

        'Dim tempDate As String = frm.imdOutkoDate.TextValue.Replace("/", "")
        'Dim dr As DataRow = Me._Ds.Tables(LMH040C.TABLE_NM_TORIKOMI_DATE).Rows(0)

        ''保管料取込日チェック
        'If tempDate < dr.Item("HOKAN_SKYU_DATE").ToString Then
        '    MyBase.ShowMessage(frm, "E285")
        '    Return False
        'End If

        ''荷役料取込日チェック
        'If tempDate < dr.Item("NIYAKU_SKYU_DATE").ToString Then
        '    MyBase.ShowMessage(frm, "E285")
        '    Return False
        'End If

        ''作業料取込日チェック
        'If tempDate < dr.Item("SAGYO_SKYU_DATE").ToString Then
        '    MyBase.ShowMessage(frm, "E285")
        '    Return False
        'End If

        ''運賃取込
        'If (dr.Item("UNTIN_CALCULATION_KB").ToString.Equals("01")) Then
        '    tempDate = frm.imdOutkaPlanDate.TextValue.Replace("/", "")
        'ElseIf (dr.Item("UNTIN_CALCULATION_KB").ToString.Equals("02")) Then
        '    tempDate = frm.imdArrPlanDate.TextValue.Replace("/", "")
        'End If

        'If frm.cmbUnsoTehaiKB.SelectedValue.ToString.Equals(LMH040C.UNSO_TEHAI_KB_MIXED) OrElse _
        '   frm.cmbUnsoTehaiKB.SelectedValue.ToString.Equals(LMH040C.UNSO_TEHAI_KB_CAR) OrElse _
        '   frm.cmbUnsoTehaiKB.SelectedValue.ToString.Equals(LMH040C.UNSO_TEHAI_KB_SPECIAL) Then

        '    If tempDate < dr.Item("UNCHIN_SKYU_DATE").ToString Then
        '        MyBase.ShowMessage(frm, "E285")
        '        Return False
        '    End If

        'ElseIf frm.cmbUnsoTehaiKB.SelectedValue.ToString.Equals(LMH040C.UNSO_TEHAI_KB_YOKO) Then

        '    If tempDate < dr.Item("YOKOMOCHI_SKYU_DATE").ToString Then
        '        MyBase.ShowMessage(frm, "E285")
        '        Return False
        '    End If

        'End If

        Return True

    End Function

#Region "ボタンクリック"

    ''' <summary>
    ''' 届先マスタメンテの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetReturnDestMstmen(ByVal frm As LMH040F, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMM040DS()
        Dim dRow As DataRow = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).NewRow
        With dRow
            .Item("MODE_FLG") = LMConst.FLG.ON    '新規（'1'）
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCDL.TextValue
            .Item("DEST_CD") = frm.txtDestCD.TextValue
            '要望番号:1217 terakawa 2012.06.28 Start
            .Item("DEST_NM") = frm.lblDestNM.TextValue
            .Item("EDI_CD") = frm.txtEDIDestCD.TextValue
            .Item("ZIP") = frm.txtDestZip.TextValue
            .Item("AD_1") = frm.txtDestAd1.TextValue
            .Item("AD_2") = frm.txtDestAd2.TextValue
            .Item("AD_3") = frm.txtDestAd3.TextValue

            '指定納品書の有り無しフラグが無("00")の場合は指定納品書区分の添付無し("02")に変換
            Dim spNhs As String = frm.cmbSpNhsKB.SelectedValue.ToString()
            If spNhs.Equals("00") = True Then
                spNhs = "02"
            End If
            .Item("SP_NHS_KB") = spNhs
            .Item("COA_YN") = frm.cmbCoaYN.SelectedValue
            .Item("DELI_ATT") = frm.txtUnsoAtt.TextValue
            .Item("TEL") = frm.txtDestTel.TextValue
            .Item("FAX") = frm.txtDestFax.TextValue
            .Item("JIS") = frm.txtDestJisCD.TextValue
            .Item("PICK_KB") = frm.cmbPickKB.SelectedValue
            .Item("REMARK") = frm.txtRemark.TextValue
            '要望番号:1217 terakawa 2012.06.28 End

        End With
        prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows.Add(dRow)
        prm.ParamDataSet = prmDs

        '在庫引当呼出
        LMFormNavigate.NextFormNavigate(Me, "LMM040", prm)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables("LMM040INOUT").Rows(0)

            With frm

                .txtDestCD.TextValue = dr.Item("DEST_CD").ToString()
                .lblDestNM.TextValue = dr.Item("DEST_NM").ToString()
                .txtDestJisCD.TextValue = dr.Item("JIS").ToString()
                .txtDestZip.TextValue = dr.Item("ZIP").ToString()
                .txtDestFax.TextValue = dr.Item("FAX").ToString()
                .txtDestTel.TextValue = dr.Item("TEL").ToString()
                .txtEDIDestCD.TextValue = dr.Item("EDI_CD").ToString()
                .txtDestAd1.TextValue = dr.Item("AD_1").ToString()
                .txtDestAd2.TextValue = dr.Item("AD_2").ToString()
                .txtDestAd3.TextValue = dr.Item("AD_3").ToString()

                Dim spNhsKb As String = dr.Item("SP_NHS_KB").ToString()
                If spNhsKb.Equals("02") = True Then
                    spNhsKb = "00"
                End If
                .cmbSpNhsKB.SelectedValue = spNhsKb

                .cmbCoaYN.SelectedValue = dr.Item("COA_YN").ToString()
                '配送時注意事項は空のとき上書き
                If String.IsNullOrEmpty(.txtUnsoAtt.TextValue) = True Then
                    .txtUnsoAtt.TextValue = dr.Item("DELI_ATT").ToString()
                End If
                '運送会社も空のときに上書き
                If String.IsNullOrEmpty(.txtUnsoCD.TextValue) = True Then
                    .txtUnsoCD.TextValue = dr.Item("SP_UNSO_CD").ToString()
                    .txtUnsoBrCD.TextValue = dr.Item("SP_UNSO_BR_CD").ToString()
                End If

                '要望番号:1217 terakawa 2012.06.28 Start
                '出荷時注意事項は空のとき上書き
                If String.IsNullOrEmpty(.txtRemark.TextValue) = True Then
                    .txtRemark.TextValue = dr.Item("REMARK").ToString()
                End If
                'ピッキング区分は空のときに上書き
                If String.IsNullOrEmpty(.cmbPickKB.TextValue) = True Then
                    .cmbPickKB.SelectedValue = dr.Item("PICK_KB").ToString()
                End If
                '要望番号:1217 terakawa 2012.06.28 End

                '運送会社名セット
                Call Me._G.SetUnsoNm()
                'タリフコード設定
                Call Me._G.SetTariffView()

                ' ==== LMM040OUTに以下の項目が存在しない ====
                .txtDestAd4.TextValue = String.Empty
                .txtDestAd5.TextValue = String.Empty
                'START YANAI EDIメモNo.54
                '.lblDestEmail.TextValue = String.Empty
                .txtDestEmail.TextValue = String.Empty
                'END YANAI EDIメモNo.54

            End With

        End If

    End Sub

    ''' <summary>
    ''' 届先マスタメンテ起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDestMstmen(ByVal frm As LMH040F) As LMFormData

        Dim ds As DataSet = New LMM040DS()
        Dim dt As DataTable = ds.Tables(LMControlC.LMM040C_TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow
        With dr
            .Item("MODE_FLG") = LMConst.FLG.ON    '新規（'1'）
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCDL.TextValue
            .Item("DEST_CD") = frm.txtDestCD.TextValue
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMM040")

    End Function

    ''' <summary>
    ''' 行削除アクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function RowDelAction(ByVal frm As LMH040F) As Boolean

        '権限
        If Me._V.IsAuthority(LMH040C.ActionType.ROWDEL) = False Then
            Return False
        End If

        Dim arr As ArrayList = Me._LMHconG.GetCheckList(frm.sprGoodsDef.ActiveSheet, LMH020G.sprGoodsDef.DEF.ColNo)
        Dim arrNotCheck As ArrayList = Me._LMHconG.GetNotCheckList(frm.sprGoodsDef.ActiveSheet, LMH020G.sprGoodsDef.DEF.ColNo)

        '入力チェック
        If Me._V.IsRowDelInputCheck(Me._Ds, arr, arrNotCheck) = False Then
            Return False
        End If

        '行削除処理
        Call Me._G.SetDelKbData(Me._Ds, LMH040C.DEL_KB_NG, LMH040C.DEL_KB_NG_NM)

        Return True

    End Function

    ''' <summary>
    ''' 行復活アクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function RowReAction(ByVal frm As LMH040F) As Boolean

        '権限
        If Me._V.IsAuthority(LMH040C.ActionType.ROWRE) = False Then
            Return False
        End If

        '入力チェック
        If Me._V.IsRowReInputCheck() = False Then
            Return False
        End If

        '行復活処理
        Call Me._G.SetDelKbData(Me._Ds, LMH040C.DEL_KB_OK, LMH040C.DEL_KB_OK_NM)

        Return True

    End Function

#End Region 'ボタンクリック

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMH040F, ByVal objNm As String, ByVal actionType As LMH040C.ActionType) As Boolean

        With frm

            Dim rtnResult As Boolean = False

            '処理開始アクション
            Call Me._LMHconH.StartAction(frm)

            Select Case objNm

                Case .txtShipCDL.Name, .txtDestCD.Name

                    rtnResult = Me.SetReturnDestPop(frm, objNm, actionType)

                Case .txtUnsoCD.Name, .txtUnsoBrCD.Name

                    rtnResult = Me.SetReturnUnsocoPop(frm, actionType)

                Case .txtUntinTariffCD.Name

                    rtnResult = Me.SetReturnTariffPop(frm, actionType)

                Case .txtExtcTariff.Name

                    rtnResult = Me.SetReturnExtcPop(frm, actionType)

                Case .txtRemark.Name, .txtUnsoAtt.Name, .txtGoodsRemark.Name

                    rtnResult = Me.SetReturnRemarkPop(frm, objNm)

                Case .txtCustGoodsCD.Name

                    rtnResult = Me.SetReturnGoodsPop(frm, actionType)

            End Select

            '処理終了アクション
            Call Me._LMHconH.EndAction(frm)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowUnsocoPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtUnsoCD.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsoBrCD.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsoNM.TextValue = Me._LMHconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))

                Dim unsoNM As String = dr.Item("UNSOCO_NM").ToString()
                Dim unsoBrNM As String = dr.Item("UNSOCO_BR_NM").ToString()

                'データセットに名称設定
                Call Me._G.SetDataSetUnsoNm(unsoNM, unsoBrNM)

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPopup(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMH040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNSOCO_CD") = frm.txtUnsoCD.TextValue.Trim()
                .Item("UNSOCO_BR_CD") = frm.txtUnsoBrCD.TextValue.Trim()
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim name As String = String.Empty

        With frm

            If LMHControlC.TARIFF_YOKO.Equals(.cmbUnsoTehaiKB.SelectedValue.ToString()) Then
                '横持ちタリフPop
                prm = Me.ShowYokoTariffPopup(frm, actionType)
                tblNm = LMZ100C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                name = "YOKO_REM"
            Else
                '運賃タリフPop
                prm = Me.ShowUnchinTariffPopup(frm, actionType)
                tblNm = LMZ230C.TABLE_NM_OUT
                code = "UNCHIN_TARIFF_CD"
                name = "UNCHIN_TARIFF_REM"
            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                .txtUntinTariffCD.TextValue = dr.Item(code).ToString()
                .lblUntinTariffREM.TextValue = dr.Item(name).ToString()

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            '.Item("CUST_CD_L") = frm.txtCustCDL.TextValue.Trim()
            '.Item("CUST_CD_M") = frm.txtCustCDM.TextValue.Trim()
            'START SHINOHARA 要望番号513
            If actionType = LMH040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513	
                .Item("UNCHIN_TARIFF_CD") = frm.txtUntinTariffCD.TextValue.Trim()
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("STR_DATE") = Me._G.GetStrDate()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            '.Item("CUST_CD_L") = frm.txtCustCDL.TextValue.Trim()
            '.Item("CUST_CD_M") = frm.txtCustCDM.TextValue.Trim()
            If actionType = LMH040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("YOKO_TARIFF_CD") = frm.txtUntinTariffCD.TextValue.Trim()
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtExtcTariff.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowExtcPopup(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            '.Item("CUST_CD_L") = frm.txtCustCDL.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCDM.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMH040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513	
                .Item("EXTC_TARIFF_CD") = frm.txtExtcTariff.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMH040F, ByVal objNm As String, ByVal actionType As LMH040C.ActionType) As Boolean

        With frm

            Dim ctl As Win.InputMan.LMImTextBox = Me._LMHconH.GetTextControl(frm, objNm)
            Dim prm As LMFormData = Me.ShowDestPopup(frm, ctl, actionType)
            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                ctl.TextValue = dr.Item("DEST_CD").ToString()
                Dim destNm As String = dr.Item("DEST_NM").ToString()

                Select Case ctl.Name

                    Case .txtShipCDL.Name

                        .lblShipNM.TextValue = destNm

                    Case .txtDestCD.Name

                        .lblDestNM.TextValue = destNm
                        .txtDestJisCD.TextValue = dr.Item("JIS").ToString()
                        .txtDestZip.TextValue = dr.Item("ZIP").ToString()
                        .txtDestFax.TextValue = dr.Item("FAX").ToString()
                        .txtDestTel.TextValue = dr.Item("TEL").ToString()
                        .txtEDIDestCD.TextValue = dr.Item("EDI_CD").ToString()
                        .txtDestAd1.TextValue = dr.Item("AD_1").ToString()
                        .txtDestAd2.TextValue = dr.Item("AD_2").ToString()
                        .txtDestAd3.TextValue = dr.Item("AD_3").ToString()
                        .txtUnsoCD.TextValue = dr.Item("SP_UNSO_CD").ToString()
                        .txtUnsoBrCD.TextValue = dr.Item("SP_UNSO_BR_CD").ToString()
                        .lblUnsoNM.TextValue = String.Empty

                        '運送会社名セット
                        Call Me._G.SetUnsoNm()

                        ' ===== LMZ210OUTに AD_4, AD_5 存在しない =======
                        .txtDestAd4.TextValue = String.Empty
                        .txtDestAd5.TextValue = String.Empty
                        'START YANAI EDIメモNo.54
                        '.lblDestEmail.TextValue = String.Empty
                        .txtDestEmail.TextValue = String.Empty
                        'END YANAI EDIメモNo.54

                        'タリフコード設定
                        Call Me._G.SetTariffView()

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
    Private Function ShowDestPopup(ByVal frm As LMH040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal actionType As LMH040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCDL.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMH040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("DEST_CD") = ctl.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 注意書Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnRemarkPop(ByVal frm As LMH040F, ByVal objNm As String) As Boolean

        Dim prm As LMFormData = Me.ShowRemarkPopup(frm, objNm)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ270C.TABLE_NM_OUT).Rows(0)

            With frm
                Dim ctl As Win.InputMan.LMImTextBox = Me._LMHconH.GetTextControl(frm, objNm)

                '要望番号516 START
                'ctl.TextValue = dr.Item("REMARK").ToString()
                If String.IsNullOrEmpty(dr.Item("REMARK").ToString()) = False AndAlso _
                   String.IsNullOrEmpty(ctl.TextValue.ToString()) = False Then
                    ctl.TextValue = String.Concat(ctl.TextValue.ToString(), Space(1), dr.Item("REMARK").ToString())
                ElseIf String.IsNullOrEmpty(dr.Item("REMARK").ToString()) = False AndAlso _
                   String.IsNullOrEmpty(ctl.TextValue.ToString()) = True Then
                    ctl.TextValue = dr.Item("REMARK").ToString()
                End If
                '要望番号516 END

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 注意書マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowRemarkPopup(ByVal frm As LMH040F, ByVal objNm As String) As LMFormData

        Dim ds As DataSet = New LMZ270DS()
        Dim dt As DataTable = ds.Tables(LMZ270C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("USER_CD") = LMUserInfoManager.GetUserID()
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SUB_KB") = "02"   '用途区分(Y005) 02:出荷管理
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ270", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, actionType)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)
            Call Me._G.SetGoodsDetailData(dr)

            '設定後、計算処理を行う
            If Me.CalcActionControl(frm) = False Then
                Me._G.IrisuLockControl()
                Return False
            End If

            'ロック制御を行う
            Me._G.IrisuLockControl()

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMH040F, ByVal actionType As LMH040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCDL.TextValue.Trim()
            .Item("CUST_CD_M") = frm.txtCustCDM.TextValue.Trim()
            'START SHINOHARA 要望番号513
            If actionType = LMH040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("GOODS_CD_CUST") = frm.txtCustGoodsCD.TextValue.Trim()
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("EDI_INOUT_KB") = "0" '出荷
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMH040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey2Press")

        Call Me.ShiftEditMode(frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し（マスタ参照）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMH040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey11Press")

        Call Me.SaveAction(frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMH040F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '  ========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMH040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCellDoubleClick")

        'ダブルクリックアクション処理
        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCellDoubleClick")

    End Sub

    ''' <summary>
    ''' ボタンクリック時のイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    ''' <remarks></remarks>
    Friend Sub btnClick(ByRef frm As LMH040F, ByVal sender As System.Object)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnClick")

        'ボタンクリックアクション処理
        Call Me.BtnClickControl(frm, CType(sender, System.Windows.Forms.Button).Name)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnClick")

    End Sub

    ''' <summary>
    ''' keyDownイベント（Enterイベント用）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub keyDown(ByRef frm As LMH040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.KeyCode = Keys.Enter Then
            Call Me.EnterAction(frm, e)
        End If

    End Sub

    ''' <summary>
    ''' 計算処理（FocusLeave）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub calcAction(ByRef frm As LMH040F, ByVal e As System.EventArgs)

        '計算処理
        Call Me.CalcActionControl(frm)

    End Sub

    ''' <summary>
    ''' 運送情報のロック制御
    ''' </summary>
    ''' <remarks>「運送手配」コンボボックスの値が変わる時</remarks>
    Friend Sub changeUnsoJyouho()

        Call Me._G.UnsoLockControl()

    End Sub

    ''' <summary>
    ''' タリフ分類区分による表示制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub changeTariffJyouho(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Call Me._G.SetTariffView()

    End Sub

    ''' <summary>
    ''' 出荷単位によるロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub optCheckedChanged()

        Call Me._G.OutkaLockControl()
        Call Me.CalcActionControlOptChange()

    End Sub

    ''' <summary>
    ''' 商品コードコントロールからFocusが外された時の処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub leaveActionGoodsCd()

        Call Me._G.SetGoodsData()

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMH040F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMH040F)
        MyBase.ShowMessage(frm, "G003")
    End Sub

#End Region

#Region "ユーティリティ"

#End Region

#End Region 'Method

End Class