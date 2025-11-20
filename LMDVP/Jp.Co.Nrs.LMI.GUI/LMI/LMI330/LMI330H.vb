' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI330H : 納品データ選択&編集
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI330ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI330H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI330F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI330V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI330G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

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
    ''' 印刷種別フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintFlg As String

    ''' <summary>
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

    ''' <summary>
    '''表示用データテーブル格納(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DitailDispDt As DataTable

    ''' <summary>
    '''行削除データ格納テーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private _RowDelDs As DataSet = New LMI330DS

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Me._Frm = New LMI330F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH(MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI330V(Me, Me._Frm, Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMI330G(Me, Me._Frm, Me._ControlG, Me._V)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitDetailSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' セット品表示処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetHinEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.SETHIN) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                MyBase.ShowMessage(Me._Frm, "G003")
                Exit Sub
            End If
        End If

        '画面IDの取得
        Dim formId As String = LMI330C.FORM_ID

        '画面遷移用空パラメータ作成
        Dim prm As LMFormData = New LMFormData()

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, formId, prm)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDataEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim rtnds As DataSet = Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Call MyBase.CallWSA("LMI330BLF", "EditChk", rtnds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'データセットクリア
        Me._RowDelDs.Clear()

        '明細部へ値の設定
        Call Me._G.SetSprDetail(Me._DitailDispDt)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI330C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                MyBase.ShowMessage(Me._Frm, "G003")
                Exit Sub
            End If
        End If

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        'DataSet設定
        Dim rtnDs As DataSet = Me.SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = Me._ControlH.CallWSAAction(DirectCast(Me._Frm, Form), blf, "SelectListData", rtnDs, lc, mc)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI330C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)

        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)

        End If

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent()

        '背景色クリア
        Me._ControlG.SetBackColor(Me._Frm)

        'カーソル位置の設定
        Dim objNm As String = Me._Frm.ActiveControl.Name()

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMI330C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        '処理開始アクション：１件時表示あり
        Me._PopupSkipFlg = True
        Me._ControlH.StartAction(Me._Frm)

        'Pop起動処理
        Call Me.ShowPopupControl(objNm, LMI330C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._ControlH.EndAction(Me._Frm) '終了処理　

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent() As Boolean

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI330C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '排他チェック
        Dim rtnds As DataSet
        rtnds = Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Call MyBase.CallWSA("LMI330BLF", "EditChk", rtnds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Function
        End If

        'DataSetクリア
        rtnds.Clear()

        'DataSet設定
        rtnds = Me.SetDataSetSave()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '保存処理
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)
        rtnds = MyBase.CallWSA(blf, "UpdateData", rtnds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '画面項目全クリア
        Call Me._G.ClearControl()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G015", New String() {""})

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveEvent() = False Then

                    e.Cancel = True

                End If

            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnPrintClick()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.PRINT) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行格納処理
        Me._ChkList = Me._V.GetCheckList(LMI330C.EventShubetsu.PRINT)

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI330C.EventShubetsu.PRINT, Me._ChkList) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'データセット
        Dim rtnDs As DataSet = Me.SetPrintData()
        rtnDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DoPrint")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)
        rtnDs = MyBase.CallWSA(blf, "DoPrint", rtnDs)


        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)

        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        '終了メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {"印刷", ""})

    End Sub

    ''' <summary>
    '''  Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm)  '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(e.Row)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            'カーソル位置チェック
            If Me._V.IsFocusChk(objNm, LMI330C.EventShubetsu.ENTER) = False Then
                Call Me._ControlH.NextFocusedControl(Me._Frm, True) 'フォーカス移動処理を行う
                Exit Sub
            End If

            '処理開始アクション
            Me._ControlH.StartAction(Me._Frm)

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(objNm, LMI330C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(Me._Frm, True)

        End With

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(Me._Frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.NewRow
        If rowNo < 1 Then
            Exit Sub
        End If

        '同じ行の場合、スルー
        If e.Row = rowNo Then
            Exit Sub
        End If

        Call Me.RowSelection(rowNo)

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowDel()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.ROW_DEL) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行格納処理
        Me._ChkList = Me._V.GetCheckList(LMI330C.EventShubetsu.ROW_DEL)

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI330C.EventShubetsu.ROW_DEL, Me._ChkList) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '削除データ格納
        Me._RowDelDs = Me.SetDataRowDel()

        'チェック行削除
        Call Me._G.DelateDtl(Me._ChkList)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 作成処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub BtnExcelClick()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.EXCEL) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI330C.EventShubetsu.EXCEL) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'InDataSet設定
        Dim prmDs As DataSet = Me.SetLMI940InDataSet()
        prm.ParamDataSet = prmDs

        'Excel作成処理呼出
        LMFormNavigate.NextFormNavigate(Me, LMI330C.FORM_EXCEL_ID, prm)

        'エラー判断フラグ
        Dim errFlg As String = prm.ParamDataSet.Tables(LMI330C.TABLE_NM_EXCEL_IN).Rows(0).Item("ERR_FLG").ToString()

        If prm.ReturnFlg = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理

            '要望対応:1853 yamanaka 2013.02.14 Start
            Select Case errFlg
                Case "1"
                    MyBase.ShowMessage(Me._Frm, "E296", New String() {"フォルダ"})
                Case "2"
                    MyBase.ShowMessage(Me._Frm, "E492", New String() {"フォルダ", "ファイル", ""})
                Case "3"
                    MyBase.ShowMessage(Me._Frm, "E454", New String() {"ファイルが複数", "作成", ""})
                Case "4"
                    MyBase.ShowMessage(Me._Frm, "E454", New String() {"読み取り専用", "作成", ""})
            End Select
            '要望対応:1853 yamanaka 2013.02.14 End

            Exit Sub
        End If

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        '終了メッセージ表示
        MyBase.ShowMessage(Me._Frm, "G002", New String() {"Excel出力処理", ""})

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me.GetSprSearchDisplayData()
        Call Me._G.SetSprSearch(Me._DispDt)

        '取得件数設定
        Me._CntSelect = Me._DispDt.Rows.Count.ToString()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G008", New String() {Me._CntSelect})


    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprSearch.CrearSpread()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '画面項目全クリア
        Call Me._G.ClearControl()

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI330C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprSearch.ActiveSheet.Cells(rowNo, LMI330G.sprSearchDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '明細部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)
        Call Me.GetSprDetailDisplayData(rowNo)
        Call Me._G.SetSprDetail(Me._DitailDispDt)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' Spread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSprSearchDisplayData()

        With Me._Frm

            'DataTableの初期化
            Me._DispDt = New DataTable
            Dim ediCtlNo As String = String.Empty
            Dim dt As DataTable = Me._OutDs.Tables(LMI330C.TABLE_NM_OUT)
            Dim selectDr As DataRow() = dt.Select()
            Dim setDs As DataSet = New LMI330DS()
            Me._DispDt = setDs.Tables(LMI330C.TABLE_NM_OUT)

            For i As Integer = 0 To selectDr.Length - 1

                If selectDr(i).Item("EDI_CTL_NO").ToString().Equals(ediCtlNo) = False Then

                    Me._DispDt.ImportRow(selectDr(i))
                    ediCtlNo = selectDr(i).Item("EDI_CTL_NO").ToString()

                End If

            Next

        End With

    End Sub

    ''' <summary>
    ''' 明細Spread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSprDetailDisplayData(ByVal rowNo As Integer)

        With Me._Frm

            'DataTableの初期化
            Me._DitailDispDt = New DataTable
            Dim filter As String = String.Empty
            Dim orderBy As String = String.Empty
            Dim selectDr As DataRow() = Nothing
            Dim setDs As DataSet = New LMI330DS()
            Me._DitailDispDt = setDs.Tables(LMI330C.TABLE_NM_OUT)

            '取得条件設定
            filter = String.Concat(filter, "NRS_BR_CD = '", .cmbNrsBr.SelectedValue, "'" _
                                         , " AND EDI_CTL_NO = '", Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(rowNo, LMI330G.sprSearchDef.EDI_CTL_NO.ColNo)), "'")

            '並び順設定
            orderBy = "EDI_CTL_NO_CHU"

            'データ取得
            Dim dt As DataTable = Me._OutDs.Tables(LMI330C.TABLE_NM_OUT)
            selectDr = dt.Select(filter, orderBy)

            For i As Integer = 0 To selectDr.Length - 1
                Me._DitailDispDt.ImportRow(selectDr(i))
            Next

        End With

    End Sub

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal objNm As String, ByVal eventshubetsu As LMI330C.EventShubetsu) As Boolean

        With Me._Frm

            Select Case objNm
                Case .txtCustCdL.Name

                    '荷主マスタ参照POP起動
                    Call Me.SetReturnCustPop(objNm, eventshubetsu)

                Case .txtDestCd.Name
                    '届け先マスタ参照POP起動
                    Call Me.SetReturnDestPop(objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

#Region "荷主マスタ"

    ''' <summary>
    ''' 荷主マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal objNm As String, ByVal eventshubetsu As LMI330C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With Me._Frm

                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMI330C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbNrsBr.SelectedValue.ToString()
            If eventshubetsu = LMI330C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF 'キャッシュ検索
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_M
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "届け先マスタ"

    ''' <summary>
    ''' 届け先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal objNm As String, ByVal eventshubetsu As LMI330C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(Me._Frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
            With Me._Frm
                .txtDestCd.TextValue = dr.Item("DEST_CD").ToString()
                .lblDestNm.TextValue = dr.Item("DEST_NM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届け先マスタ参照POP起動
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMI330C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = Me._Frm.cmbNrsBr.SelectedValue.ToString()
            .Item("CUST_CD_L") = Me._Frm.txtCustCdL.TextValue
            If eventshubetsu = LMI330C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = Me._Frm.txtDestCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.OFF

        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetInData() As DataSet

        Dim ds As DataSet = New LMI330DS
        Dim dt As DataTable = ds.Tables(LMI330C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("WH_CD") = .cmbSoko.SelectedValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("F_DATE") = .imdOutkaPlanDate_From.TextValue
            dr.Item("T_DATE") = .imdOutkaPlanDate_To.TextValue
            dr.Item("DELIVERY_NO") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI330G.sprSearchDef.DELIVERY_NO.ColNo))
            dr.Item("DEST_CD") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI330G.sprSearchDef.DEST_CD.ColNo))
            dr.Item("DEST_NM") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI330G.sprSearchDef.DEST_NM.ColNo))

            ds.Tables(LMI330C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetHaitaChk() As DataSet

        Dim ds As DataSet = New LMI330DS
        Dim dt As DataTable = ds.Tables(LMI330C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("EDI_CTL_NO") = .lblEdiCtlNo.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue

            ds.Tables(LMI330C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(行削除)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataRowDel() As DataSet

        Dim ds As DataSet = New LMI330DS
        Dim dt As DataTable = ds.Tables(LMI330C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = Me._ChkList.Count - 1
        Dim arr As Integer = 0

        With Me._Frm

            For i As Integer = 0 To max

                arr = Convert.ToInt32(Me._ChkList(i))
                dr.Item("NRS_BR_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("CUST_CD_L") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("EDI_CTL_NO") = .lblEdiCtlNo.TextValue
                dr.Item("DELIVERY_NO") = .txtDeliveryNo.TextValue
                dr.Item("ARR_PLAN_DATE") = .imdArrPlanDate.TextValue
                dr.Item("DEST_CD") = .txtDestCd.TextValue
                dr.Item("DEST_NM") = .lblDestNm.TextValue
                dr.Item("EDI_CTL_NO_CHU") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.EDI_CTL_NO_CHU.ColNo))
                dr.Item("CUST_GOODS_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.GOODS_CD_CUST.ColNo))
                dr.Item("LOT_NO") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.LOT_NO.ColNo))
                dr.Item("GOODS_NM") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.GOODS_NM.ColNo))
                dr.Item("BUYER_ORD_NO") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.BUYER_ORD_NO.ColNo))
                dr.Item("OUTKA_TTL_NB") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(arr, LMI330G.sprDetailDef.OUTKA_TTL_NB.ColNo))
                dr.Item("SYS_DEL_FLG") = "1"

                ds.Tables(LMI330C.TABLE_NM_IN).Rows.Add(dr)

            Next

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetSave() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI330DS
            Dim dt As DataTable = ds.Tables(LMI330C.TABLE_NM_IN)
            Dim dr As DataRow = Nothing
            Dim max As Integer = .sprDetail.ActiveSheet.RowCount - 1

            For i As Integer = 0 To max

                dr = dt.NewRow()
                dr.Item("NRS_BR_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("CUST_CD_L") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("EDI_CTL_NO") = .lblEdiCtlNo.TextValue
                dr.Item("DELIVERY_NO") = .txtDeliveryNo.TextValue
                dr.Item("ARR_PLAN_DATE") = .imdArrPlanDate.TextValue
                dr.Item("DEST_CD") = .txtDestCd.TextValue
                dr.Item("DEST_NM") = .lblDestNm.TextValue
                dr.Item("EDI_CTL_NO_CHU") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.EDI_CTL_NO_CHU.ColNo))
                dr.Item("CUST_GOODS_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.GOODS_CD_CUST.ColNo))
                dr.Item("LOT_NO") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.LOT_NO.ColNo))
                dr.Item("GOODS_NM") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.GOODS_NM.ColNo))
                dr.Item("BUYER_ORD_NO") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.BUYER_ORD_NO.ColNo))
                dr.Item("OUTKA_TTL_NB") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI330G.sprDetailDef.OUTKA_TTL_NB.ColNo))
                dr.Item("SYS_DEL_FLG") = "0"

                ds.Tables(LMI330C.TABLE_NM_IN).Rows.Add(dr)
            Next

            If Me._RowDelDs.Tables("LMI330IN").Rows.Count > 0 Then
                For j As Integer = 0 To Me._RowDelDs.Tables("LMI330IN").Rows.Count - 1

                    dr = dt.NewRow()
                    dr.Item("NRS_BR_CD") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("NRS_BR_CD")
                    dr.Item("CUST_CD_L") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("CUST_CD_L")
                    dr.Item("EDI_CTL_NO") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("EDI_CTL_NO")
                    dr.Item("DELIVERY_NO") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("DELIVERY_NO")
                    dr.Item("ARR_PLAN_DATE") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("ARR_PLAN_DATE")
                    dr.Item("DEST_CD") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("DEST_CD")
                    dr.Item("DEST_NM") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("DEST_NM")
                    dr.Item("EDI_CTL_NO_CHU") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("EDI_CTL_NO_CHU")
                    dr.Item("CUST_GOODS_CD") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("CUST_GOODS_CD")
                    dr.Item("LOT_NO") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("LOT_NO")
                    dr.Item("GOODS_NM") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("GOODS_NM")
                    dr.Item("BUYER_ORD_NO") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("BUYER_ORD_NO")
                    dr.Item("OUTKA_TTL_NB") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("OUTKA_TTL_NB")
                    dr.Item("SYS_DEL_FLG") = Me._RowDelDs.Tables("LMI330IN").Rows(j).Item("SYS_DEL_FLG")

                    ds.Tables(LMI330C.TABLE_NM_IN).Rows.Add(dr)

                Next
            End If

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(印刷処理)
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function SetPrintData() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMH584DS
            Dim dt As DataTable = ds.Tables(LMI330C.TABLE_NM_RPT_IN)
            Dim dr As DataRow = Nothing
            Dim arr As Integer = 0
            Dim max As Integer = Me._ChkList.Count - 1

            For i As Integer = 0 To max

                arr = Convert.ToInt32(Me._ChkList(i))
                dr = dt.NewRow
                dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.sprSearch.ActiveSheet.Cells(arr, LMI330G.sprSearchDef.NRS_BR_CD.ColNo))
                dr.Item("CUST_CD_L") = Me._ControlV.GetCellValue(.sprSearch.ActiveSheet.Cells(arr, LMI330G.sprSearchDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = "00"
                dr.Item("EDI_CTL_NO") = Me._ControlV.GetCellValue(.sprSearch.ActiveSheet.Cells(arr, LMI330G.sprSearchDef.EDI_CTL_NO.ColNo))
                dr.Item("PRTFLG") = "2"
                dr.Item("INOUT_KB") = "0"
                dt.Rows.Add(dr)

            Next

            Return ds

        End With

    End Function

    ''' <summary>
    ''' Excel出力処理用データセット作成
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetLMI940InDataSet() As DataSet

        Dim prmDs As DataSet = New LMI940DS()
        Dim row As DataRow = prmDs.Tables(LMI330C.TABLE_NM_EXCEL_IN).NewRow

        With Me._Frm

            row("NRS_BR_CD") = .cmbNrsBr.SelectedValue.ToString()
            row("CUST_CD_L") = .txtCustCdL.TextValue.ToString()
            row("CUST_CD_M") = "00"
            row("GOODS_CD_POSITION") = .txtGoodsCdPosition.TextValue.ToString()
            prmDs.Tables(LMI330C.TABLE_NM_EXCEL_IN).Rows.Add(row)

        End With

        Return prmDs

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(セット品)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SetHinEvent")

        Me.SetHinEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SetHinEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI330F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMI330F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI330FKeyDown(ByVal frm As LMI330F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMI330F_KeyDown")

        Call Me.EnterAction(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMI330F_KeyDown")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprSearchLeaveCell(ByVal frm As LMI330F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetailLeaveCell")

        Call Me.SprFindLeaveCell(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetailLeaveCell")

    End Sub

    ''' <summary>
    ''' Printボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub PrintClick(ByVal frm As LMI330F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintClick")

        '印刷処理
        Call Me.BtnPrintClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintClick")

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DelClick(ByVal frm As LMI330F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelClick")

        '行削除処理
        Call Me.RowDel()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelClick")

    End Sub

    ''' <summary>
    ''' 作成ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub ExcelClick(ByVal frm As LMI330F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ExcelClick")

        '作成処理
        Call Me.BtnExcelClick()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ExcelClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class