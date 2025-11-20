' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ       : データ抽出
'  プログラムID     :  LMQ010    : データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Microsoft.Office.Interop

''' <summary>
''' LMQ010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMQ010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMQ010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMQ010G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMQconV As LMQControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMQconH As LMQControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMQconG As LMQControlG

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索結果格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectPattern As DataTable

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

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
        Dim frm As LMQ010F = New LMQ010F(Me)

        'ディスプレイモードの設定
        Dim mode As String = String.Empty
        mode = DispMode.VIEW

        'レコードステータスの設定
        Dim status As String = String.Empty
        status = RecordStatus.NOMAL_REC

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'Validateクラスの設定
        Me._V = New LMQ010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMQ010G(Me, frm)

        'ファンクションキーの設定
        Me._G.SetFunctionKey()

        'タブインデックスの設定
        Me._G.SetTabIndex()

        'シチュエーションラベルの設定
        'Me._G.SetSituation(mode, status)

        'コントロール個別設定
        Me._G.SetControl(MyBase.GetPGID())
        Me._G.CreateComboBox()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread()
        Me._G.SetInitValue(frm)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目の制御
        Me._G.SetControlsStatus()

        'フォーカスの設定
        Me._G.SetFoucus(LMQ010C.EventShubetsu.MAIN)

        'Validate共通クラスの設定
        Me._LMQconV = New LMQControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMQconH = New LMQControlH(DirectCast(frm, Form), GetPGID())

        'Gamen共通クラスの設定
        Me._LMQconG = New LMQControlG(DirectCast(frm, Form))

        'frm.showで選択値が初期化されるので、確保
        Dim idx As Integer = frm.cmbConnection.SelectedIndex
        'フォームの表示
        frm.Show()
        frm.cmbConnection.SelectedIndex = idx

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "外部メソッド"

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMQ010C.EventShubetsu, ByVal frm As LMQ010F)

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMQ010C.EventShubetsu.SINKI    '新規

                '処理開始アクション
                Me._LMQconH.StartAction(frm)

                'コントロール個別設定
                Me._G.SetControl(Me.GetPGID())

                'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                'Me._G.InitSpread(eventShubetsu)

                'Me._G.SetInitValue(frm)

                'メッセージの表示
                ShowMessage(frm, "G003")

                'フォーカスの設定
                Me._G.SetFoucus(LMQ010C.EventShubetsu.SINKI)

                '処理終了アクション
                Me._LMQconH.EndAction(frm)

                'モード・ステータスの設定
                Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NEW_REC)

                'ファンクションキーの設定
                Me._G.SetFunctionKey()

                '画面の入力項目の制御
                Me._G.SetControlsStatus()

            Case LMQ010C.EventShubetsu.HENSYU    '編集

                '処理開始アクション
                Me._LMQconH.StartAction(frm)

                '編集時チェック
                If Me._V.IsHenshuCheck() = False Then
                    Me._LMQconH.EndAction(frm) '処理終了アクション
                    Exit Sub
                End If

                'DataSet設定(排他チェック)
                Dim ds As DataSet = New LMQ010DS()
                Call Me.SetDataSetHaitaChk(frm, ds)

                'ログ出力
                MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

                '==========================
                'WSAクラス呼出
                '==========================
                Dim rtnds As DataSet = MyBase.CallWSA("LMQ010BLF", "EditChk", ds)

                'ログ出力
                MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

                'メッセージコードの判定
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm) 'エラーメッセージ表示
                    Me._LMQconH.EndAction(frm) '処理終了アクション
                Else
                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                    Me._G.InitSpread(eventShubetsu)

                    'フォーカスの設定
                    Me._G.SetFoucus(LMQ010C.EventShubetsu.HENSYU)

                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)

                    'モード・ステータスの設定
                    Call Me._G.SetSituation(DispMode.EDIT, RecordStatus.NOMAL_REC)

                    'ファンクションキーの設定
                    Me._G.SetFunctionKey()

                    '画面の入力項目の制御
                    Me._G.SetControlsStatus()

                End If

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMQ010C.EventShubetsu.DELREV    '削除・復活

                '処理開始アクション
                Me._LMQconH.StartAction(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub
                End If

                '確認メッセージの表示
                '2016.01.06 UMANO 英語化対応START
                'If Me._LMQconH.ConfirmMsg(frm, LMQ010C.FNCNM4) = False Then
                If Me._LMQconH.ConfirmMsg(frm, frm.FunctionKey.F4ButtonName) = False Then
                    '2016.01.06 UMANO 英語化対応END
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub

                End If

                '削除・復活処理を行う
                If Me.DelRevData(frm) = False Then
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub
                End If

                'コントロール個別設定
                Me._G.SetControl(Me.GetPGID())

                'フォーカスの設定
                Me._G.SetFoucus(LMQ010C.EventShubetsu.MAIN)

                '処理終了アクション
                Me._LMQconH.EndAction(frm)

                'モード・ステータスの設定
                Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

                'ファンクションキーの設定
                Me._G.SetFunctionKey()

                '画面の入力項目の制御
                Me._G.SetControlsStatus()

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMQ010C.EventShubetsu.KENSAKU  '検索

                'メッセージ表示(編集モードの場合確認メッセージを表示する)
                If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                    If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                        Call Me._LMQconH.EndAction(frm) '終了処理
                        Exit Sub
                    End If
                End If

                '処理開始アクション
                Me._LMQconH.StartAction(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub
                End If

                'シチュエーションラベルの設定
                'mode = DispMode.VIEW
                'status = RecordStatus.NOMAL_REC
                'Me._G.SetSituation(mode, status)

                'コントロール個別設定
                Me._G.SetControl(Me.GetPGID())

                '検索処理を行う
                Me.SelectData(frm, LMQ010C.NEW_MODE)

                'フォーカスの設定
                Me._G.SetFoucus(LMQ010C.EventShubetsu.KENSAKU)

                '処理終了アクション
                Me._LMQconH.EndAction(frm)

                'モード・ステータスの設定
                Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

                'ファンクションキーの設定
                Me._G.SetFunctionKey()

                '画面の入力項目の制御
                Me._G.SetControlsStatus()

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMQ010C.EventShubetsu.EXCEL    'Excel作成

                '処理開始アクション
                Me._LMQconH.StartAction(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub
                End If

                '確認メッセージの表示
                '2016.01.06 UMANO 英語化対応START
                'If Me._LMQconH.ConfirmMsg(frm, LMQ010C.FNCNM10) = False Then
                If Me._LMQconH.ConfirmMsg(frm, frm.FunctionKey.F10ButtonName) = False Then
                    '2016.01.06 UMANO 英語化対応END
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub

                End If

                ''Excel作成処理を行う
                'If Me.ExcelMake(frm) = False Then
                '    '処理終了アクション
                '    me._LMQconH.EndAction(frm)
                '    Exit Sub
                'End If
                'ExcelCreator作成処理を行う
                Dim rtnDs As DataSet = Me.ExcelCreatorMake(frm)
                '検索成功時印刷処理を行う
                If rtnDs IsNot Nothing Then
                    'ExcelCreator呼び出し処理
                    If Me.ExcelPrint(frm, rtnDs) = False Then
                        If MyBase.IsErrorMessageExist = True Then
                            '更新失敗時、返却メッセージを設定
                            MyBase.ShowMessage(frm)
                        End If
                        '処理終了アクション
                        Me._LMQconH.EndAction(frm)
                        Exit Sub

                    End If

                    'DB更新処理
                    Me.SaveExcelData(frm)

                Else
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub

                End If

                '処理終了アクション
                Me._LMQconH.EndAction(frm)

            Case LMQ010C.EventShubetsu.HOZON    '保存

                '処理開始アクション
                Me._LMQconH.StartAction(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub
                End If

                ''確認メッセージの表示
                'If me._LMQconH.ConfirmMsg(frm, LMQ010C.FNCNM11) = False Then
                '    '処理終了アクション
                '    me._LMQconH.EndAction(frm)
                '    Exit Sub
                'End If

                '保存処理を行う
                If Me.SaveData(frm) = False Then
                    '処理終了アクション
                    Me._LMQconH.EndAction(frm)
                    Exit Sub
                End If

                'コントロール個別設定
                Me._G.SetControl(Me.GetPGID())

                'フォーカスの設定
                Me._G.SetFoucus(LMQ010C.EventShubetsu.MAIN)

                '処理終了アクション
                Me._LMQconH.EndAction(frm)

                'モード・ステータスの設定
                Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

                'ファンクションキーの設定
                Me._G.SetFunctionKey()

                '画面の入力項目の制御
                Me._G.SetControlsStatus()

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

                'Case LMQ010C.EventShubetsu.DOUBLE_CLICK 'SQLパターン一覧ダブルクリック

                '    'メッセージ表示(編集モードの場合確認メッセージを表示する)
                '    If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
                '        If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                '            Call Me._LMQconH.EndAction(frm) '終了処理
                '            Exit Sub
                '        End If
                '    End If

                '    Call Me.RowSelection(frm, e.Row)


        End Select

    End Sub

#End Region '外部メソッド

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMQ010F, ByVal reFlg As String)

        '閾値の取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)

        Dim drmc As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0)


        'DataSet設定
        Dim rtDs As DataSet = New LMQ010DS()
        Me.SetDataSetInData(frm, rtDs)

        'SPREAD(表示行)初期化
        frm.sprPattern.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMQconH.CallWSAAction(DirectCast(frm, Form), _
                                                      "LMQ010BLF", "SelectListData", rtDs, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                      , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            Me._SelectPattern = rtnDs.Tables(LMQ010C.TABLE_NM_OUT)
            Me.SuccessSelect(frm, rtnDs, reFlg)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMQ010F, ByVal ds As DataSet, ByVal reFlg As String)

        Dim dt As DataTable = ds.Tables(LMQ010C.TABLE_NM_OUT)

        '画面解除
        MyBase.UnLockedControls(frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread(LMQ010C.EventShubetsu.KENSAKU)

        '取得データをSPREADに表示
        Me._G.SetSpread(dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        If 0 < Convert.ToInt32(Me._CntSelect) Then

            'メッセージエリアの設定
            If LMQ010C.NEW_MODE.Equals(reFlg) = True Then
                MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
            End If

        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetSituation(DispMode.INIT, RecordStatus.INIT)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal frm As LMQ010F) As Boolean

        SaveData = True

        'DataSet設定
        Dim ds As DataSet = New LMQ010DS()
        Call Me.SetDataSetInData_Save(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMQ010BLF", "SaveData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '更新失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            SaveData = False
        Else
            '更新成功時、メッセージ表示
            '2016.01.06 UMANO 英語化対応START
            'Call Me.SuccessUpdate(frm, "保存")
            Call Me.SuccessUpdate(frm, frm.FunctionKey.F11ButtonName)
            '2016.01.06 UMANO 英語化対応END
            SaveData = True
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

    End Function

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function DelRevData(ByVal frm As LMQ010F) As Boolean

        DelRevData = True

        'DataSet設定
        Dim ds As DataSet = New LMQ010DS()
        Call Me.SetDataSetInData_DelRev(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelRevData")

        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMQ010BLF", "DelRevData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist = True Then
            '更新失敗時、返却メッセージを設定
            MyBase.ShowMessage(frm)
            DelRevData = False
        Else
            '更新成功時、メッセージ表示
            '2016.01.06 UMANO 英語化対応START
            'Call Me.SuccessUpdate(frm, "削除・復活")
            Call Me.SuccessUpdate(frm, frm.FunctionKey.F4ButtonName)
            '2016.01.06 UMANO 英語化対応END
            DelRevData = True
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelRevData")

    End Function

    ''' <summary>
    ''' 更新成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">成功メッセージに載せる変換文字</param>
    ''' <remarks></remarks>
    Private Sub SuccessUpdate(ByVal frm As LMQ010F, ByVal msg As String)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {msg, String.Empty})

    End Sub

    ''' <summary>
    ''' Excel作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ExcelMake(ByVal frm As LMQ010F) As Boolean

        ExcelMake = True

        'DataSet設定
        Dim ds As DataSet = New LMQ010DS()
        ds = Me.SetDataSetInData_Excel(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ExcelMake")

        '==== WSAクラス呼出（Excel作成処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMQ010BLF", "ExcelMake", ds)

        '成功時共通処理を行う
        If rtnDs IsNot Nothing Then

            'Excel出力
            Me.ExcelOut(rtnDs, frm)

            'Excel出力時、最終実行日を更新する
            'DataSet設定
            ds = New LMQ010DS()
            Call Me.SetDataSetInData_Save(frm, ds)

            ' #################### 更新処理　####################'

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveExcelData")

            '==== WSAクラス呼出（変更処理） ====
            rtnDs = MyBase.CallWSA("LMQ010BLF", "SaveExcelData", ds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveExcelData")

        End If

        'メッセージを表示()
        MyBase.ShowMessage(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ExcelMake")

    End Function

    ''' <summary>
    ''' Excel出力処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Sub ExcelOut(ByVal ds As DataSet, ByVal frm As LMQ010F)

        'DataSetの値を二次元配列に格納する
        Dim rowMax As Integer = ds.Tables(0).Rows.Count - 1
        Dim colMax As Integer = ds.Tables(0).Columns.Count - 1
        Dim excelData(rowMax + 1, colMax + 1) As String

        'タイトル列を設定
        For i As Integer = 0 To colMax
            excelData(0, i) = ds.Tables(0).Columns(i).ColumnName
        Next

        '値を設定
        For i As Integer = 0 To rowMax
            For j As Integer = 0 To colMax
                excelData(i + 1, j) = ds.Tables(0).Rows(i).Item(j).ToString
            Next
        Next

        'EXCEL起動
        Dim xlsApp As New Excel.Application
        Dim xlsBook As Excel.Workbook = xlsApp.Workbooks.Add()
        Dim xlsSheet As Excel.Worksheet = DirectCast(xlsBook.Worksheets(1), Excel.Worksheet)

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        startCell = DirectCast(xlsSheet.Cells(1, 1), Excel.Range)
        endCell = DirectCast(xlsSheet.Cells(rowMax + 2, colMax + 1), Excel.Range)
        range = xlsSheet.Range(startCell, endCell)
        range.Value = excelData

        '区分マスタ検索処理
        Dim xlsPath As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'E001'")
        Try
            '保存時、上書き確認ポップで「いいえ」「キャンセル」選択時にエラーになるため
            xlsBook.SaveAs(String.Concat(xlsPath(0).Item("KBN_NM1").ToString, frm.txtExcelName.TextValue, xlsPath(0).Item("KBN_NM2").ToString))
        Catch ex As Exception

        End Try

        xlsApp.Quit()

        xlsSheet = Nothing
        xlsBook = Nothing
        xlsApp = Nothing

    End Sub

    ''' <summary>
    ''' ExcelCreator作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ExcelCreatorMake(ByVal frm As LMQ010F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMQ010DS()
        ds = Me.SetDataSetInData_Excel(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ExcelCreatorMake")

        '==== WSAクラス呼出（Excel作成処理） ====
        'UPD 2021/11/16 DBリード指定
        'Dim rtnDs As DataSet = MyBase.CallWSA("LMQ010BLF", "ExcelMake", ds)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMQ010BLF", "ExcelMake", ds, True)

        '成功時共通処理を行う
        If rtnDs IsNot Nothing Then

        Else
            'メッセージを表示()
            MyBase.ShowMessage(frm)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ExcelCreatorMake")

        Return rtnDs

    End Function

    ''' <summary>
    ''' ExcelCreator作成後、M_SQLの更新処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SaveExcelData(ByVal frm As LMQ010F)

        'DataSet設定
        Dim ds As DataSet = New LMQ010DS()
        Call Me.SetDataSetInData_Excel(frm, ds)

        'Excel出力時、最終実行日を更新する
        'DataSet設定
        ds = New LMQ010DS()
        Call Me.SetDataSetInData_Save(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveExcelData")

        '==== WSAクラス呼出（変更処理） ====
        ds = MyBase.CallWSA("LMQ010BLF", "SaveExcelData", ds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveExcelData")

        'メッセージを表示()
        MyBase.ShowMessage(frm)

    End Sub

    ''' <summary>
    ''' ExcelCreator呼び出し処理
    ''' </summary>
    ''' <param name="ds">出力データ</param>
    ''' <remarks></remarks>
    Private Function ExcelPrint(ByVal frm As LMQ010F, ByVal ds As DataSet) As Boolean

        Dim resultFlg As Boolean = True
        'Dim excelDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E001' AND ", _
        '                                                                                                  "KBN_CD ='", frm.cmbSyubetu.SelectedValue, "'"))

        'INPUTパラメータチェック
        'If Me._LMQconH.CheckParameter(ds, excelDr(0).Item("KBN_NM1").ToString, LMQControlC.ChkObject.ALL_OBJECT) = False Then
        '    Exit Function
        'End If
        If Me._LMQconH.CheckParameter(ds, frm.txtExcelTitleName.TextValue, LMQControlC.ChkObject.ALL_OBJECT) = False Then
            Exit Function
        End If

#If True Then    'UPD 2019/02/12 依頼番号 : 004542   【LMS】LMQ617船積確認書_荷主コードで判別できるように修正
        Dim sFIleNMPlus As String = String.Empty
        If frm.txtPatternID.TextValue.Equals("LMQ616") _
            Or frm.txtPatternID.TextValue.Equals("LMQ617") Then
            If ds.Tables("LMQ000OUT").Rows.Count > 0 Then
                sFIleNMPlus = ds.Tables("LMQ000OUT").Rows(0).Item("CUST_KBN").ToString
            End If
        End If

#End If
        'Excel出力
        'resultFlg = Me._LMQconH.ExcelPrint(ds, excelDr(0).Item("KBN_NM2").ToString, excelDr(0).Item("KBN_NM1").ToString)
#If False Then      'UPD 2019/02/12 依頼番号 : 004542   【LMS】LMQ617船積確認書_荷主コードで判別できるように修正
        resultFlg = Me._LMQconH.ExcelPrint(ds, frm.txtExcelName.TextValue, frm.txtExcelTitleName.TextValue)
#Else
        resultFlg = Me._LMQconH.ExcelPrint(ds, frm.txtExcelName.TextValue, String.Concat(frm.txtExcelTitleName.TextValue + sFIleNMPlus))
#End If


        Return resultFlg

    End Function

    ''' <summary>
    ''' Spreadダブルクリック検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal frm As LMQ010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '検索行が選択された場合は処理終了
        If e.Row = 0 Then
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMQconH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(frm, e.Row)

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMQ010F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
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

        Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMQ010F, ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMQ010C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._LMQconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMQconV.GetCellValue(frm.sprPattern.ActiveSheet.Cells(rowNo, LMQ010G.sprPattern.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetSituation(, recstatus)

        'ファンクションキーの設定
        Me._G.SetFunctionKey()

        '選択行の詳細を表示
        Me._G.SetDetails(rowNo, Me._SelectPattern)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        '画面の入力項目の制御(オールロック)
        Me._G.SetLockControl()


    End Sub

#End Region '内部メソッド

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMQ010F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMQ010C.TABLE_NM_IN).NewRow()

        'ユーザーの営業所コード格納
        dr("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()

        '検索条件　入力部（スプレッド）
        With frm.sprPattern.ActiveSheet
            dr("PATTERN_ID") = Me._LMQconG.GetCellValue(.Cells(0, LMQ010G.sprPattern.PATTERN_ID.ColNo))
            dr("EX_TYPE_KB") = Me._LMQconG.GetCellValue(.Cells(0, LMQ010G.sprPattern.EX_TYPE_NM.ColNo))
            dr("EX_CONTENTS") = Me._LMQconG.GetCellValue(.Cells(0, LMQ010G.sprPattern.EX_CONTENTS.ColNo))
            dr("FILE_NM") = Me._LMQconG.GetCellValue(.Cells(0, LMQ010G.sprPattern.FILE_NM.ColNo))
            dr("FILE_TITLE_NM") = Me._LMQconG.GetCellValue(.Cells(0, LMQ010G.sprPattern.FILE_TITLE_NM.ColNo))
            dr("SYS_DEL_FLG") = Me._LMQconG.GetCellValue(.Cells(0, LMQ010G.sprPattern.SYS_DEL_FLG_NM.ColNo))
        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMQ010C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 保存時、利用するデータセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_Save(ByVal frm As LMQ010F, ByRef rtDs As DataSet)

        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows.Add(rtDs.Tables(LMQ010C.TABLE_NM_OUT).NewRow())

        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("PATTERN_ID") = frm.txtPatternID.TextValue
        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("EX_TYPE_KB") = frm.cmbSyubetu.SelectedValue
        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("EX_CONTENTS") = frm.txtTyusyutu.TextValue
        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("EX_SQL") = frm.txtSql.TextValue
        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("FILE_NM") = frm.txtExcelName.TextValue
        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("FILE_TITLE_NM") = frm.txtExcelTitleName.TextValue

        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
        rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("RECORD_STATUS") = frm.lblSituation.RecordStatus

        If Me._SelectPattern Is Nothing = False Then
            Dim setRows() As DataRow = Me._SelectPattern.Select(String.Concat("PATTERN_ID = '", frm.txtPatternID.TextValue, "'"))
            If 0 < setRows.Length Then
                rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("SYS_UPD_DATE") = setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_UPD_DATE).ToString
                rtDs.Tables(LMQ010C.TABLE_NM_OUT).Rows(0)("SYS_UPD_TIME") = setRows(0)(LMQ010C.DsPatternColumnIndex.SYS_UPD_TIME).ToString
            End If
        End If

    End Sub

    ''' <summary>
    ''' Excel作成時、利用するデータセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData_Excel(ByVal frm As LMQ010F, ByVal rtDs As DataSet) As DataSet

        rtDs.Tables(LMQ010C.TABLE_NM_EXCEL).Rows.Add(rtDs.Tables(LMQ010C.TABLE_NM_EXCEL).NewRow())
        Dim setSql As String = frm.txtSql.TextValue

        With frm.sprParam.Sheets(0)
            Dim maxCol As Integer = .ColumnCount - 1
            For i As Integer = 0 To maxCol
                setSql = setSql.Replace(String.Concat("@", frm.sprParam.Sheets(0).ColumnHeader.Columns(i).Label, "@"), Me._LMQconG.GetCellValue(.Cells(0, i)))
            Next
        End With

        rtDs.Tables(LMQ010C.TABLE_NM_EXCEL).Rows(0)("SQL") = setSql
        '接続先コンボより該当の営業所を抽出
        Dim br As String = String.Empty
        Dim tempBr As String = String.Empty
        Dim brs As String = String.Empty
        brs = frm.cmbConnection.SelectedValue.ToString()
        For i As Integer = 0 To (brs.Length - brs.Replace(",", "").Length)
            tempBr = brs.Split(CChar(","))(i)
            If i = 0 Then br = tempBr
            'ログインユーザと一致なら文句なしで採用
            If tempBr.Equals(LM.Base.LMUserInfoManager.GetNrsBrCd()) = True Then
                br = tempBr
                Exit For
            End If
            'ログインユーザと一致していなかったら、選択されたところの１件目
        Next
        rtDs.Tables(LMQ010C.TABLE_NM_EXCEL).Rows(0)("NRS_BR_CD") = br

        Return rtDs

    End Function

    ''' <summary>
    ''' データセット設定(編集時排他)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk(ByVal frm As LMQ010F, ByVal rtDs As DataSet)

        Dim dt As DataTable = rtDs.Tables(LMQ010C.TABLE_NM_OUT)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr.Item("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
            dr.Item("PATTERN_ID") = .txtPatternID.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue

        End With

        dt.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(削除、復活処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_DelRev(ByVal frm As LMQ010F, ByVal rtDs As DataSet)

        Dim dt As DataTable = rtDs.Tables(LMQ010C.TABLE_NM_OUT)
        Dim dr As DataRow = dt.NewRow()

        With frm

            dr.Item("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
            dr.Item("PATTERN_ID") = .txtPatternID.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue

            '削除/復活の切り替えを行う
            Dim delflg As String = String.Empty
            Select Case .lblSituation.RecordStatus
                Case RecordStatus.NOMAL_REC
                    delflg = LMConst.FLG.ON
                Case RecordStatus.DELETE_REC
                    delflg = LMConst.FLG.OFF
            End Select
            dr.Item("SYS_DEL_FLG") = delflg

        End With

        dt.Rows.Add(dr)

    End Sub

#End Region 'DataSet設定

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShiftInsertStatus")

        '「新規」処理
        Me.ActionControl(LMQ010C.EventShubetsu.SINKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShiftInsertStatus")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ShiftUpdateStatus")

        '「編集」処理
        Me.ActionControl(LMQ010C.EventShubetsu.HENSYU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ShiftUpdateStatus")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "DelRevEvent")

        '「削除・復活」処理
        Me.ActionControl(LMQ010C.EventShubetsu.DELREV, frm)

        Logger.EndLog(MyBase.GetType.Name, "DelRevEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '「検索」処理
        Me.ActionControl(LMQ010C.EventShubetsu.KENSAKU, frm)

        Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "SelectExcelEvent")

        '「Excel作成」処理
        Me.ActionControl(LMQ010C.EventShubetsu.EXCEL, frm)

        Logger.EndLog(MyBase.GetType.Name, "SelectExcelEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '「保存」処理
        Me.ActionControl(LMQ010C.EventShubetsu.HOZON, frm)

        Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMQ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        '終了処理  
        frm.Close()

        Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMQ010F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByVal frm As LMQ010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        '「ダブルクリック」処理
        Me.SprCellSelect(frm, e)

        Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMQ010F, ByVal e As FormClosingEventArgs) As Boolean

        Logger.StartLog(MyBase.GetType.Name, "CloseFormEvent")

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
            Logger.EndLog(MyBase.GetType.Name, "CloseFormEvent")
            Exit Function
        End If

        'メッセージの表示
        Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "W002")
        If rtnResult = MsgBoxResult.Yes Then

            '項目チェック
            If Me._V.IsSingleCheck(LMQ010C.EventShubetsu.HOZON) = False Then
                e.Cancel = True
                Logger.EndLog(MyBase.GetType.Name, "CloseFormEvent")
                Exit Function
            End If

            '保存処理
            If Me.SaveData(frm) = False Then
                e.Cancel = True
            End If

        ElseIf rtnResult = MsgBoxResult.Cancel Then '「キャンセル」押下時
            e.Cancel = True

        End If

        Logger.EndLog(MyBase.GetType.Name, "CloseFormEvent")

    End Function

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMQ010F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class