' ==========================================================================
'  システム名       :  LMS
'  サブシステム名   :  LMU       : 文書管理
'  プログラムID     :  LMU010H   : 文書管理画面
'  作  成  者       :  NRS)OHNO
' ==========================================================================
Imports System.IO
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.Com.Utility

''' <summary>
''' LMU010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMU010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMU010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMU010G

    ''' <summary>
    ''' キャッシュデータセットを格納するフィールド
    ''' </summary>
    ''' <remarks>現在選択されているデータを保持します。</remarks>
    Private _SessionDS As DataSet

    ''' <summary>
    ''' コンボ作成用データセットを格納するフィールド
    ''' </summary>
    ''' <remarks>現在選択されているデータを保持します。</remarks>
    Private _ComboDS As DataSet

    ''' <summary>
    ''' 画面遷移フラグ
    ''' </summary>
    ''' <remarks>画面遷移時に受け取るActFlgを設定</remarks>
    Private _ActFlg As String

    ' 共通Handlerクラスを格納するフィールド
    Private _LMUConH As LMUControlH

    '検索条件格納用フィールド
    Private _FindDs As DataSet

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
        Dim frm As LMU010F = New LMU010F(Me)

        'Validateクラスの設定
        Me._V = New LMU010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMU010G(Me, frm)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(DispMode.VIEW)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Dim ds As DataSet = New LMU010DS()

        'コンボボックス作成データの取得 2015/1/6 ｱﾙﾍﾞ対応
        ds = Me.CreateComboData(ds)

        Me._ComboDS = ds.Copy

        'コンボボックス生成 2015/1/6 ｱﾙﾍﾞ対応
        Call _G.CreateCombo(ds)

        'INパラメータの設定
        prmDs = Me.InitPrmDataSet(prmDs)

        '画面の入力項目の制御()
        Call _G.SetControlsStatus(DispMode.VIEW, prmDs)

        '画面遷移フラグ設定
        If "GTA020".Equals(MyBase.RootPGID()) = False Then
            Me._ActFlg = LMConst.FLG.ON
        Else
            Me._ActFlg = LMConst.FLG.OFF
        End If

        '遷移元がメニュー画面以外の場合、初期検索制御()
        If Me._ActFlg.Equals(LMConst.FLG.ON) = True Then
            '画面間データをSessionに格納
            Me._SessionDS = prmDs
            '画面間データで検索処理
            Call Me.SelectDataListData(frm, True)
        Else
            'メッセージの表示
            Me.ShowMessage(frm, "G007")
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' INパラメータの設定
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function InitPrmDataSet(ByVal ds As DataSet) As DataSet

        '既にインスタンスがある場合、スルー
        If ds Is Nothing = False Then
            Return ds
        End If

        'インスタンス生成
        ds = New LMU010DS()

        '空行追加
        Dim dt As DataTable = ds.Tables(LMControlC.LMU010C_TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim max As Integer = dt.Columns.Count - 1
        For i As Integer = 0 To max
            dr.Item(i) = String.Empty
        Next
        dt.Rows.Add(dr)

        Return ds

    End Function

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMU010F)

        '初期処理
        Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMU010C.EVENT_EDIT) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '共通チェックを行う
        If Me._V.IsCommonChk(False) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim arrSelectedRcd As New ArrayList

        '関連チェック
        If Me._V.IsSpreadSelectionChk(frm.sprData, _
                                      LMU010G.sprDataDef.DEF.ColNo, _
                                      LMU010G.sprDataDef.FILE_NO.ColNo, _
                                      LMU010G.sprDataDef.SYSTEM.ColNo, _
                                      arrSelectedRcd) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '関連チェック(編集不可チェック)
        If Me._V.IsEditPossibleChk(arrSelectedRcd, "Edit") = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '削除処理の関連チェック(検索条件変更)
        If Me._V.IsConditionChangedCheck(Me._G.KEY_TYPE_USED, Me._G.KEY_NM_USED, _
                                         frm.cmbKeyType.SelectedValue.ToString, frm.txtKeyNo.TextValue, _
                                         frm.cmbKeyType.SelectedText, "Edit") = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '排他チェックを行う
        Dim ds As DataSet = New LMU010DS
        Dim arr As ArrayList = Me.ClickHaitaChk(frm, ds)

        '終了処理
        Me.EndAction(frm)


        'メッセージを表示する
        If MyBase.IsMessageExist() = True Then

            MyBase.ShowMessage(frm)
        Else
            Me.ShowMessage(frm, "G014", New String() {"文書ファイルの情報"})

            'スプレッドクリア
            frm.sprData.CrearSpread()

            'Spread部編集モード処理(ロック制御)
            Call Me._G.SetSpread(Me._SessionDS, Me._ComboDS, DispMode.EDIT, arr)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus(DispMode.EDIT, Me._SessionDS)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey(DispMode.EDIT)
        End If



        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectDataListData(ByVal frm As LMU010F, Optional ByVal firstFlg As Boolean = False)

        '初期処理
        Me.StartAction(frm)

        '共通チェックを行う
        If Me._V.IsCommonChk(True) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMU010DS
        Call Me.SetDatasetDataListCondition(frm, ds)

        'スプレッドクリア
        frm.sprData.CrearSpread()

        '画面解除
        Me.UnLockedControls(frm)

        '検索処理を行う
        Call Me.SelectDataAgain(frm, ds, firstFlg)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SaveDataListData(ByRef frm As LMU010F, ByVal actionType As LMU010C.ActionType) As Boolean

        '初期処理
        Me.StartAction(frm)

        '権限チェック
        'If Me._V.IsAuthorityChk(LMU010C.EVENT_SAVE) = False Then
        '    Me.EndAction(frm) '終了処理
        '    Exit Function
        'End If

        'Spread部入力チェック
        If Me._V.IsSpreadInputChk() = False Then
            Me.EndAction(frm) '終了処理
            Return False
        End If

        '画面全ロック解除
        Me.UnLockedControls(frm)

        If actionType = LMU010C.ActionType.Close OrElse Me.ShowMessageC001(frm, "Save") = True Then

            'ログ出力
            Logger.StartLog(Me.GetType.Name, "SaveDataList")

            Dim rtnDs As New DataSet
            Dim dsTmp As DataSet = Me._SessionDS.Clone
            '格納場所定義
            Dim strDR As String = Me.GetDirectory(frm)

            'strDRの最後の文字が'\'でない場合は'\'を付加する 2014/01/10 追加（本明）　
            'If Right(strDR, 1) <> "\" Then
            '    strDR = String.Concat(strDR, "\")
            'End If

            Dim localPath As String = String.Empty
            Dim fileNM As String = String.Empty     'コピー元ファイル名
            Dim fileNMCopy As String = String.Empty 'コピー先ファイル名

            Dim dt As DataTable = Me._SessionDS.Tables(LMU010C.TABLE_NM_OUT)
            Dim max As Integer = dt.Rows.Count - 1
            Dim recNo As Integer = 0
            Dim sheet As FarPoint.Win.Spread.SheetView = frm.sprData.ActiveSheet
            For i As Integer = 0 To max

                recNo = Convert.ToInt32(Me._G.GetCellValue(sheet.Cells(i, LMU010G.sprDataDef.RECORDNO.ColNo)))

                With dt.Rows(recNo)

                    If .Item("UPD_FLG").ToString.Equals(LMConst.FLG.OFF) = True Then

                        'ファイル名設定
                        localPath = .Item("LOCAL_FULL_PATH").ToString
                        'ファイル名
                        fileNM = localPath.Substring(localPath.LastIndexOf("\") + 1)


                        'コピー先ファイル名設定
                        Dim sysDateTime() As String = GetSystemDateTime()   'システム日時を取得
                        fileNMCopy = String.Concat(sysDateTime(0), sysDateTime(1), "_", fileNM)

                        'ファイル存在チェック
                        If Me._V.IsFileExistChk1(localPath, strDR + "\" + fileNMCopy) = False Then
                            'Cursorを元に戻す
                            Cursor.Current = Cursors.Default
                            Return False
                        End If

                        If MyBase.IsMessageExist() = True Then
                            'Cursorを元に戻す
                            Cursor.Current = Cursors.Default
                            MyBase.ShowMessage(frm)
                            Return False
                        End If

                        If File.Exists(strDR) = False Then
                            Directory.CreateDirectory(strDR)
                        End If
                        File.Copy(localPath, strDR + "\" + fileNMCopy)

                        .Item("ENT_SYSID_KBN") = frm.cmbSystemID.SelectedValue      'SystemID区分
                        .Item("KEY_TYPE_KBN") = frm.cmbKeyType.SelectedValue        'キー種別
                        .Item("KEY_NO") = frm.txtKeyNo.TextValue                    'キー番号
                        .Item("KEY_NO_SEQ") = "0"                                   '0固定
                        .Item("FILE_TYPE_KBN") = Me._G.GetCellValue(sheet.Cells(i, LMU010G.sprDataDef.FILE_TYPE.ColNo))
                        .Item("REMARK") = Me._G.GetCellValue(sheet.Cells(i, LMU010G.sprDataDef.REMARK.ColNo))
                        .Item("FILE_PATH") = strDR
                        .Item("FILE_NM") = fileNMCopy
                        .Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        .Item("SYS_DEL_FLG") = LMConst.FLG.OFF

                    ElseIf .Item("UPD_FLG").ToString.Equals("2") Then

                        '排他チェック
                        rtnDs.Clear()
                        dsTmp.Clear()
                        dsTmp.Tables(LMU010C.TABLE_NM_OUT).ImportRow(dt.Rows(i))
                        rtnDs = Me.CallWSA("LMU010BLF", "chkDataListHaita", dsTmp)

                        If MyBase.IsMessageExist() = True Then
                            Me.EndAction(frm) '終了処理
                            Me.ShowMessage(frm, "E011")
                            Return False

                        End If

                        .Item("FILE_TYPE_KBN") = Me._G.GetCellValue(sheet.Cells(i, LMU010G.sprDataDef.FILE_TYPE.ColNo))
                        .Item("REMARK") = Me._G.GetCellValue(sheet.Cells(i, LMU010G.sprDataDef.REMARK.ColNo))

                    End If

                End With

            Next

            'ログ出力
            Logger.EndLog(Me.GetType.Name, "SaveDataList")

            'DataSet設定
            Dim ds As DataSet = New LMU010DS
            Call Me.SetDatasetDataListCondition(frm, ds)

            'スプレッドクリア
            frm.sprData.CrearSpread()

            '==========================
            'WSAクラス呼出
            '==========================
            rtnDs.Clear()
            rtnDs = Me.CallWSA("LMU010BLF", "SaveDataList", ds)

            '再検索
            Call Me.SelectDataAgain(frm, ds, False, True)

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub CloseForm(ByVal frm As LMU010F, ByVal e As FormClosingEventArgs)

        '編集モード以外なら処理終了
        If frm.lblSituation.DispMode <> DispMode.EDIT Then
            Exit Sub
        End If

        'メッセージの表示
        Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "W002")
        If rtnResult = MsgBoxResult.Yes Then

            '保存処理
            If Me.SaveDataListData(frm, LMU010C.ActionType.Close) = False Then
                e.Cancel = True
            End If

        ElseIf rtnResult = MsgBoxResult.No Then
            '何もしない

        Else
            e.Cancel = True

        End If

    End Sub

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMU010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '権限チェック
        If Me._V.IsAuthorityChk(LMU010C.EVENT_DCLICK) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '編集モードの場合、スルー
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        '列ヘッダダブルクリックの場合、スルー
        If e.ColumnHeader = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.Row
        If rowNo > -1 Then

            'メッセージのクリア
            Me.ClearMessageAria(frm)

            'ログ出力
            Logger.StartLog(Me.GetType.Name, "RowSelection")

            Dim Proc As New System.Diagnostics.Process
            Dim targetPath As String = String.Empty
            Dim fileNM As String = String.Empty
            Dim recNo As Integer = Convert.ToInt32(Me._G.GetCellValue(frm.sprData.ActiveSheet.Cells(rowNo, LMU010G.sprDataDef.RECORDNO.ColNo)))

            With Me._SessionDS.Tables(LMU010C.TABLE_NM_OUT).Rows(recNo)

                If LMU010C.SYSTEM_NVO.Equals(.Item("ENT_SYSID_KBN").ToString) = True AndAlso .Item("UPD_FLG").Equals(LMConst.FLG.OFF) = True Then

                    Dim strTmp As String = .Item("LOCAL_FULL_PATH").ToString
                    targetPath = strTmp.Substring(0, strTmp.LastIndexOf("\") + 1)
                    fileNM = strTmp.Substring(strTmp.LastIndexOf("\") + 1)

                Else
                    targetPath = .Item("FILE_PATH").ToString

                    ''Leaseの場合はファイル名の後ろに'\'をつける
                    'If LMU010C.SYSTEM_LEASE.Equals(.Item("ENT_SYSID_KBN").ToString) = True Then
                    '    targetPath = String.Concat(targetPath, "\")
                    'End If

                    'targetPathの最後の文字が'\'でない場合は'\'を付加する
                    If Right(targetPath, 1) <> "\" Then
                        targetPath = String.Concat(targetPath, "\")
                    End If

                    fileNM = .Item("FILE_NM").ToString

                End If

                'ファイル存在チェック
                If Me._V.IsFileExistChk2(String.Concat(targetPath, fileNM)) = False Then
                    'Cursorを元に戻す
                    Cursor.Current = Cursors.Default
                    Exit Sub
                End If

                Proc.StartInfo.WorkingDirectory = targetPath
                Proc.StartInfo.FileName = fileNM

                'カーソルを砂時計にする
                Cursor.Current = Cursors.WaitCursor

                Proc.Start()
                Proc.Close()

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default

            End With

            'ログ出力
            Logger.EndLog(Me.GetType.Name, "DeleteDataList")

        End If

    End Sub

    ''' <summary>
    ''' Delet処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteData(ByVal frm As LMU010F)

        '処理開始アクション
        Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMU010C.EVENT_DEL) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim arrSelectedRcd As New ArrayList()

        '関連チェック(CheckBox未選択・複数選択チェック)
        If Me._V.IsSpreadSelectionChk(frm.sprData, _
                                      LMU010G.sprDataDef.DEF.ColNo, _
                                      LMU010G.sprDataDef.FILE_NO.ColNo, _
                                      LMU010G.sprDataDef.SYSTEM.ColNo, _
                                      arrSelectedRcd) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim actStr As String = "削除"

        '関連チェック(編集不可チェック)
        If Me._V.IsEditPossibleChk(arrSelectedRcd, actStr) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '削除処理の関連チェック(検索条件変更)
        If Me._V.IsConditionChangedCheck(Me._G.KEY_TYPE_USED, Me._G.KEY_NM_USED, _
                                         frm.cmbKeyType.SelectedValue.ToString, frm.txtKeyNo.TextValue, _
                                         frm.cmbKeyType.SelectedText, actStr) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '確認メッセージの表示
        If Me.ShowMessageC001(frm, actStr) = False Then
            Me.EndAction(frm)
            Exit Sub
        End If

        '削除するデータを設定
        Dim ds As DataSet = New LMU010DS()
        Dim dt As DataTable = ds.Tables(LMU010C.TABLE_NM_OUT)
        Dim dr As DataRow = dt.NewRow()
        Dim hashTbl As Hashtable = DirectCast(arrSelectedRcd.Item(0), Hashtable)
        With dr
            .Item("FILE_NO") = hashTbl.Item("FileNo").ToString()
            .Item("SYS_UPD_DATE") = hashTbl.Item("SysUpdDate").ToString()
            .Item("SYS_UPD_TIME") = hashTbl.Item("SysUpdTime").ToString()
        End With
        dt.Rows.Add(dr)

        '再検索用の値を設定
        Dim inTbl As DataTable = ds.Tables(LMControlC.LMU010C_TABLE_NM_IN)
        Dim indr As DataRow = inTbl.NewRow()
        With indr
            .Item("ENT_SYSID_KBN") = frm.cmbSystemID.SelectedValue.ToString()
            .Item("KEY_TYPE_KBN") = frm.cmbKeyType.SelectedValue.ToString()
            .Item("KEY_NO") = frm.txtKeyNo.TextValue
        End With
        inTbl.Rows.Add(indr)

        'ログ出力
        Logger.StartLog(Me.GetType.Name, "DeleteListData")

        '更新処理
        Dim rtnDs As DataSet = Me.CallWSA("LMU010BLF", "DeleteListData", ds)


        'エラーの場合、メッセージを設定して終了
        If MyBase.IsMessageExist() = True Then
            Me.ShowMessage(frm)
            Me.EndAction(frm)
        End If

        'スプレッドクリア
        frm.sprData.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(rtnDs, Me._ComboDS, DispMode.VIEW)

        '再検索した値の設定
        Me._SessionDS = rtnDs

        'メッセージ表示
        Me.ShowMessage(frm, "G015", New String() {actStr})

        '処理終了アクション
        Me.EndAction(frm)

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' ボタン押下時の排他チェックを行う(Edit,Delete時)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ClickHaitaChk(ByVal frm As LMU010F, ByVal ds As DataSet) As ArrayList

        'DataSet設定
        Call Me.SetDatasetDataListCondition(frm, ds)

        Dim arr As New ArrayList
        Dim dsTmp As DataSet = ds.Clone
        Dim max As Integer = frm.sprData.ActiveSheet.RowCount - 1

        For i As Integer = 0 To max
            If Me._G.GetCellValue(frm.sprData.ActiveSheet.Cells(i, LMU010G.sprDataDef.DEF.ColNo)).Equals("1") Then

                dsTmp.Clear()
                dsTmp.Tables(LMU010C.TABLE_NM_OUT).ImportRow(Me._SessionDS.Tables(LMU010C.TABLE_NM_OUT).Rows(i))

                '排他チェック
                Dim rtnDs As DataSet = Me.CallWSA("LMU010BLF", "chkDataListHaita", dsTmp)

                If MyBase.IsMessageExist() = True Then
                    'Me.EndAction(frm) '終了処理
                    'MyBase.ShowMessage(frm)

                    Return arr
                End If

                Me._SessionDS.Tables(LMU010C.TABLE_NM_OUT).Rows(i).Item("UPD_FLG") = "2"
                arr.Add(i)
                Exit For
            End If
        Next

        Return arr

    End Function

    ''' <summary>
    ''' 格納ディレクトリ取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function GetDirectory(ByRef frm As LMU010F) As String

        Dim KBDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN)   'LMS用フォルダ
        Dim drKbn As DataRow() = KBDt.Select("KBN_GROUP_CD = 'C027' " & _
                                              "AND KBN_CD   = '01'   ")

        '格納場所定義
        Dim strRootDR As String = drKbn(0).Item("KBN_NM1").ToString
        Dim strKeyNoDR As String = frm.txtKeyNo.TextValue.ToString
        Dim strDR As String = strRootDR

        Return strDR

    End Function

    ''' <summary>
    ''' 再検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">データセット</param>
    ''' <param name="firstFlg">初回検索フラグ = False</param>
    ''' <param name="actionFlg">アクションフラグ = action</param>
    ''' <remarks></remarks>
    Private Function SelectDataAgain(ByVal frm As LMU010F _
                                   , ByVal ds As DataSet _
                                   , Optional ByVal firstFlg As Boolean = False _
                                   , Optional ByVal actionFlg As Boolean = False _
                                   ) As Boolean

        '画面の入力項目の制御()
        Call _G.SetControlsStatus(DispMode.VIEW, ds)

        '検索条件保持
        Me._G.KEY_TYPE_USED = frm.cmbKeyType.SelectedValue.ToString
        Me._G.KEY_NM_USED = frm.txtKeyNo.TextValue

        'ログ出力
        Logger.StartLog(Me.GetType.Name, "SelectDataList")

        '強制実行フラグの設定
        Me.SetForceOparation(actionFlg)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me.CallWSA("LMU010BLF", "SelectDataList", ds)

        'メッセージの判定
        If Me.IsMessageExist = True Then
            If Me.IsWarningMessageExist = True Then                'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If firstFlg = True OrElse _
                   firstFlg = False AndAlso Me.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    Me.SetForceOparation(True)
                    'メッセージの初期化
                    Me.ClearMessageData()
                    'WSA呼出し
                    rtnDs = Me.CallWSA("LMU010BLF", "SelectDataList", ds)
                    '取得データをSPREADに表示
                    Call Me._G.SetSpread(rtnDs, Me._ComboDS, DispMode.VIEW)
                    'メッセージエリアの設定
                    Me.ShowMessage(frm, "G006")

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    Me.ShowMessage(frm, "G007")
                End If

            Else    'Warning以外の場合
                'メッセージエリアの設定
                If firstFlg = True Then
                    'Me.ShowMessage(frm, "G039")
                Else
                    Me.ShowMessage(frm)
                End If
            End If
        Else

            '取得データをSPREADに表示
            Call Me._G.SetSpread(rtnDs, Me._ComboDS, DispMode.VIEW)
            'メッセージエリアの設定

            If actionFlg = False Then

                Me.ShowMessage(frm, "G006")

            Else

                Me.ShowMessage(frm, "G015", New String() {"保存"})

            End If

        End If

        Me._SessionDS = rtnDs

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(DispMode.VIEW)

        'ログ出力
        Logger.EndLog(Me.GetType.Name, "SelectDataList")

    End Function

    ''' <summary>
    ''' 開始処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMU010F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        'メッセージエリアのクリア
        MyBase.ClearMessageAria(frm)

        'メッセージのクリア
        MyBase.ClearMessageData()


    End Sub

    ''' <summary>
    ''' 終了処理処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMU010F)

        '画面解除
        Me.UnLockedControls(frm)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索処理時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDataListCondition(ByVal frm As LMU010F, ByVal ds As DataSet)

        Dim dRow As DataRow = ds.Tables(LMControlC.LMU010C_TABLE_NM_IN).NewRow

        '【ヘッダ】
        dRow("ENT_SYSID_KBN") = frm.cmbSystemID.SelectedValue   'cmbSystemID
        dRow("KEY_TYPE_KBN") = frm.cmbKeyType.SelectedValue     'cmbKeyType
        dRow("KEY_NO") = frm.txtKeyNo.TextValue                 'txtKeyNo
        dRow("ACT_FLG") = Me._ActFlg                            '画面遷移フラグ

        ds.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows.Add(dRow)

        If Me._SessionDS Is Nothing = False Then

            For Each dr As DataRow In Me._SessionDS.Tables(LMU010C.TABLE_NM_OUT).Rows
                ds.Tables(LMU010C.TABLE_NM_OUT).ImportRow(dr)
            Next
        End If

        Me._SessionDS = ds

    End Sub

    ''' <summary>
    ''' データセット設定(排他チェック時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="rowNum">選択行番号</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetHaitaChkCondition(ByVal frm As LMU010F, ByVal ds As DataSet, ByVal rowNum As Integer)

        Dim dRow As DataRow = ds.Tables(LMControlC.LMU010C_TABLE_NM_IN).NewRow

        '【ヘッダ】
        With frm
            dRow("KEY_TYPE_KBN") = .cmbKeyType.SelectedValue    'cmbKeyType
            dRow("KEY_NO") = .txtKeyNo.TextValue            'txtKeyNo
        End With

        '【スプレッド検索行】
        With frm.sprData.Sheets(0)
            dRow("DEF") = String.Empty                                                            'DEF
            dRow("FILE_TYPE_KBN") = String.Empty                                                      'FILE_TYPE
            dRow("REMARK") = String.Empty                                                         'REMARK
            dRow("FILE_PATH") = String.Empty                                                      'FILE_PATH
            dRow("SYS_ENT_USER") = .Cells(rowNum, LMU010G.sprDataDef.SYS_ENT_USER.ColNo).Value    'SYS_ENT_USER
            dRow("SYS_ENT_DATE") = .Cells(rowNum, LMU010G.sprDataDef.SYS_ENT_DATE.ColNo).Value    'SYS_ENT_DATE
        End With

        'ds.Tables(REC010C.TABLE_NM_IN).Rows.Add(dRow)

    End Sub

#End Region 'DataSet設定

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="groupCd">区分分類コード</param>
    ''' <returns>区分名６</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnData(ByVal kbn As String, ByVal groupCd As String, ByVal colNm As String) As String

        SelectKbnData = String.Empty

        Dim drows As DataRow() = Me.SelectKbnListDataRow(kbn, groupCd)

        If drows.Length > 0 Then
            '正常時 レートを設定
            SelectKbnData = drows(0).Item(colNm).ToString()
        End If

        Return SelectKbnData

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="groupCd">区分分類コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnListDataRow(ByVal kbn As String _
                                         , ByVal groupCd As String _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectZbnString(kbn, groupCd))

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbn">区分コード</param>
    ''' <param name="groupCd">区分分類コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectZbnString(ByVal kbn As String _
                                     , ByVal groupCd As String _
                                     ) As String

        SelectZbnString = String.Empty

        '削除フラグ
        SelectZbnString = String.Concat(SelectZbnString, " SYS_DEL_FLG = '0' ")

        '区分グループコード
        SelectZbnString = String.Concat(SelectZbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        '区分コード
        If String.IsNullOrEmpty(kbn) = False Then

            SelectZbnString = String.Concat(SelectZbnString, " AND ", "KBN_CD = ", " '", kbn, "' ")

        End If

        Return SelectZbnString

    End Function

#End Region

#End Region '個別メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMU010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        '編集処理
        Call Me.ShiftEditMode(frm)

        Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMU010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectDataListData")

        '検索処理  
        Call Me.SelectDataListData(frm)

        Logger.EndLog(Me.GetType.Name, "SelectDataListData")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMU010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey11Press")

        '保存処理  
        Call Me.SaveDataListData(frm, LMU010C.ActionType.Save)

        Logger.EndLog(Me.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMU010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey12Press")

        '終了処理  
        frm.Close()

        Logger.EndLog(Me.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMU010F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseForm(frm, e)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprLocationCellDoubleClick(ByRef frm As LMU010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        '選択処理
        Call Me.RowSelection(frm, e)

    End Sub

    ''' <summary>
    ''' Addボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAddClick(ByRef frm As LMU010F, ByVal e As System.EventArgs)

        '初期処理
        Me.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMU010C.EVENT_ADD) = False Then
            MyBase.ShowMessage(frm, "E016")
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        '共通チェックを行う
        If Me._V.IsCommonChk(False) = False Then
            Me.EndAction(frm) '終了処理
            Exit Sub
        End If

        With frm

            '関連チェック(検索条件変更)
            If Me._V.IsConditionChangedCheck(Me._G.KEY_TYPE_USED, Me._G.KEY_NM_USED, _
                                             .cmbKeyType.SelectedValue.ToString, .txtKeyNo.TextValue, _
                                             .cmbKeyType.SelectedText, "Add") = False Then
                Me.EndAction(frm) '終了処理
                Exit Sub
            End If

            '画面解除
            Me.UnLockedControls(frm)

            'DataSet設定
            Dim ds As DataSet = New LMU010DS
            Call Me.SetDatasetDataListCondition(frm, ds)

            Dim fileStream As Stream
            Dim openFileDialog As New OpenFileDialog()

            openFileDialog.InitialDirectory = "c:\"
            openFileDialog.Filter = "All files (*.*)|*.*"
            openFileDialog.FilterIndex = 1
            openFileDialog.RestoreDirectory = True
            openFileDialog.Multiselect = True

            If openFileDialog.ShowDialog() = DialogResult.OK Then
                fileStream = openFileDialog.OpenFile()
                If Not (fileStream Is Nothing) Then

                    Dim tmpDs As DataSet = Me._SessionDS
                    Dim rowCnt As Integer = tmpDs.Tables(LMU010C.TABLE_NM_OUT).Rows.Count - 1
                    Dim arr As New ArrayList
                    Dim tmpRw As DataRow = Nothing
                    '格納場所定義
                    Dim strDR As String = Me.GetDirectory(frm)
                    Dim localPath As String = String.Empty
                    Dim fileNM As String = String.Empty

                    'Systemの名称を取得
                    Dim sysNm As String = Me.SelectKbnData(LMU010C.SYSTEM_NVO, "F015", "KBN_NM6")

                    Dim KBDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN)   'ファイルタイプ取得
                    Dim drKbn As DataRow() = KBDt.Select("KBN_GROUP_CD = 'F014' " & _
                                                          "AND KBN_CD   = 'N2'   ")

                    Dim max As Integer = openFileDialog.FileNames.Count - 1
                    For i As Integer = 0 To max

                        'ファイル名設定
                        localPath = openFileDialog.FileNames(i)
                        fileNM = localPath.Substring(localPath.LastIndexOf("\") + 1)

                        If MyBase.IsMessageExist() = True Then
                            'Cursorを元に戻す
                            Cursor.Current = Cursors.Default
                            MyBase.ShowMessage(frm)
                            Exit Sub
                        End If

                        tmpRw = tmpDs.Tables(LMU010C.TABLE_NM_OUT).NewRow

                        tmpRw.Item("ENT_SYSID_KBN") = frm.cmbSystemID.SelectedValue   'cmbSystemID
                        tmpRw.Item("KEY_TYPE_KBN") = .cmbKeyType.SelectedValue
                        tmpRw.Item("KEY_NO") = .txtKeyNo.TextValue
                        tmpRw.Item("KEY_NO_SEQ") = "0"  '0固定
                        tmpRw.Item("FILE_PATH") = strDR + openFileDialog.SafeFileNames(i)
                        tmpRw.Item("LOCAL_FULL_PATH") = openFileDialog.FileNames(i)
                        tmpRw.Item("UPD_FLG") = "0"
                        'tmpRw.Item("FILE_TYPE_KBN") = drKbn(0).Item("KBN_CD").ToString
                        tmpRw.Item("FILE_TYPE_KBN") = ""


                        '.SelectedValue = "07"

                        '2014/01/21　大野追加
                        If Me._V.IsFileNameByteChk(openFileDialog.SafeFileNames(i)) = False Then

                            'Cursorを元に戻す
                            Cursor.Current = Cursors.Default

                            'フォーカスの設定
                            Call Me._G.SetFoucus()

                            Exit Sub

                        End If


                        tmpDs.Tables(LMU010C.TABLE_NM_OUT).Rows.Add(tmpRw)

                        arr.Add(rowCnt + (i + 1))

                    Next

                    'スプレッドクリア
                    .sprData.CrearSpread()

                    Call Me._G.SetSpread(tmpDs, Me._ComboDS, DispMode.EDIT, arr)

                End If



                'モード・ステータスの設定
                Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(DispMode.EDIT)

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus(DispMode.EDIT, Me._SessionDS)

                'メッセージを表示する
                Me.ShowMessage(frm, "G003")

            End If

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default

            'フォーカスの設定
            Call Me._G.SetFoucus()

        End With

    End Sub

    ''' <summary>
    ''' Deleteボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnDeleteClick(ByRef frm As LMU010F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "btnDeleteClick")

        '終了処理  
        Call Me.DeleteData(frm)

        Logger.EndLog(Me.GetType.Name, "btnDeleteClick")

    End Sub

    ''' <summary>
    ''' 処理確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="replaceMsg">処理メッセージ</param>
    ''' <returns>True：処理続行、False：処理中断</returns>
    ''' <remarks></remarks>
    Friend Function ShowMessageC001(ByVal frm As LMU010F, ByVal replaceMsg As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {replaceMsg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function
    ''' <summary>
    ''' LMU010のコンボボックス作成データの取得をします。
    ''' </summary>
    ''' <remarks></remarks>
    ''' 
    Private Function CreateComboData(ByVal ds As DataSet) As DataSet

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMU010BLF", "SelectListData", ds)

        Return ds

    End Function
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class