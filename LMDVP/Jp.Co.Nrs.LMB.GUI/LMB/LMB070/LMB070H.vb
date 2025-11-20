' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB070H : 写真選択
'  作  成  者       :  matsumoto
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMB070ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMB070H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMB070V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMB070G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConG As LMBControlG

    ''' <summary>
    ''' ハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConH As LMBControlH

    ''' <summary>
    ''' Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBConV As LMBControlV

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    ''' <summary>
    ''' パラメータデータセット
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' パラメータデータセット(入庫品名変更前)
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDsSave As DataSet

    ''' <summary>
    ''' 入庫品枠ごとの写真枚数を保持
    ''' </summary>
    Private _PhotoDict As Dictionary(Of String, Integer)

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

        Me._Prm = prm

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet()

        'フォームの作成
        Dim frm As LMB070F = New LMB070F(Me)

        Dim popLL As LMFormPopLL = DirectCast(frm, LMFormPopLL)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(frm)

        'Validateクラスの設定
        Me._V = New LMB070V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMB070G(Me, frm)

        Me._LMBConV = New LMBControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMBConH = New LMBControlH(DirectCast(frm, Form), Me.GetPGID, Me)

        'Gamen共通クラスの設定
        Me._LMBConG = New LMBControlG(DirectCast(frm, Form))

        'Dictionaryの初期化
        Me._PhotoDict = New Dictionary(Of String, Integer)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '抽出条件の設定
        Me._G.SetInitForm(frm, Me._PrmDs)
        frm.imdSatsueiDateFrom.TextValue = MyBase.GetSystemDateTime(0)

        '↓ データ取得の必要があればここにコーディングする。

        '↑ データ取得の必要があればここにコーディングする。

        '明細クリア
        Me._G.RemoveDetailInCtl()

        '前回登録データが存在する場合、前回登録データを表示
        Me.DispLastEntData()

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'DownLoadボタンロック処理
        Call Me._G.LockDownLoadControl()

        'ステータスバーの位置調整
        Me._G.SizesetStatusStrip(frm)

        'リターンコードの設定
        Me._Prm.ReturnFlg = False

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub


#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMB070F)

        '処理開始アクション
        Call Me._LMBConH.StartAction(frm)

        '入力チェック
        If Me._V.IsInputChk(_G) = False Then
            '終了処理
            Call Me._LMBConH.EndAction(frm)
            Exit Sub
        End If

        '検索
        Dim rtnDs As DataSet = Me.SelectList(frm)

        If rtnDs IsNot Nothing Then

            '明細クリア
            Me._G.RemoveDetailInCtl()

            Dim outTbl As DataTable = rtnDs.Tables(LMB070C.TABLE_NM_OUT)
            Dim count As Integer = outTbl.Rows.Count

            '取得件数による処理変更
            If 0 < count Then

                Dim ds As DataSet = New LMB070DS()
                Dim dispTbl As DataTable = ds.Tables(LMB070C.TABLE_NM_OUT)

                '前回登録データが存在する場合、検索結果に前回登録データを追加する
                If Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows.Count > 0 Then

                    '前回登録データをNOでグループ化
                    Dim workDt As DataTable = Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Copy
                    Dim view As DataView = New DataView(workDt)
                    workDt = view.ToTable(True, "NO")

                    'NOでソート
                    Dim sortDr As DataRow() = workDt.Select(Nothing, "NO ASC")
                    For Each row As DataRow In sortDr

                        '検索結果に存在するか確認
                        Dim selRows As DataRow() = outTbl.Select(String.Concat("NO = '", row("NO").ToString(), "'"))
                        If selRows.Length = 0 Then

                            '検索結果に存在しなければ追加
                            Dim addDr As DataRow() = Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Select(String.Concat("NO = '", row("NO").ToString(), "'"), "FILE_PATH ASC")
                            For Each addrow As DataRow In addDr
                                Dim drow As DataRow = dispTbl.NewRow

                                drow("NO") = addrow("NO")
                                drow("SHOHIN_NM") = addrow("SHOHIN_NM")
                                drow("SATSUEI_DATE") = addrow("SATSUEI_DATE")
                                drow("USER_LNM") = addrow("USER_LNM")
                                drow("SYS_UPD_DATE") = addrow("SYS_UPD_DATE")
                                drow("SYS_UPD_TIME") = addrow("SYS_UPD_TIME")
                                drow("FILE_PATH") = addrow("FILE_PATH")

                                '表示用DataTableに前回登録データをセット
                                dispTbl.Rows.Add(drow)
                            Next
                        End If
                    Next

                    '表示用DataSetに前回登録データをコピー
                    '※前回登録データを選択済みとして表示するために必要
                    For Each row As DataRow In Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows
                        ds.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).ImportRow(row)
                    Next
                End If

                '表示用DataTableに検索結果をセット
                For Each row As DataRow In outTbl.Rows
                    dispTbl.ImportRow(row)
                Next

                '取得データを表示
                Me._G.AddDetailInCtl(ds, Me._PhotoDict)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G016", New String() {Convert.ToString(count)})

            Else

                '取得件数が0件で前回登録データが存在する場合、前回登録データを表示
                Me.DispLastEntData()

            End If

        End If

        '終了処理
        Call Me._LMBConH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'DownLoadボタンロック処理
        Call Me._G.LockDownLoadControl()

        frm.pnlDetail.Focus()

    End Sub

    ''' <summary>
    ''' ダウンロード
    ''' </summary>
    ''' <param name="frm"></param>
    Private Sub DownLoadAction(ByVal frm As LMB070F)

        Dim backDir As String = String.Empty
        Dim ds As DataSet = New LMB070DS()

        '処理開始アクション
        Call Me._LMBConH.StartAction(frm)

        '画像選択チェック
        If Me._V.IsSelectedCheck(_G) = False Then
            '終了処理
            Call Me._LMBConH.EndAction(frm)
            Exit Sub
        End If

        'ファイル保存ダイアログ
        Dim sfd As New FolderBrowserDialog
        'ダイアログタイトル
        sfd.Description = "画像ファイルを保存するフォルダを選択してください"
        'ファイル保存ダイアログ[初期ディレクトリ]
        sfd.RootFolder = Environment.SpecialFolder.Desktop

        'ダイアログ展開
        Dim dlogResult As DialogResult = sfd.ShowDialog()

        If dlogResult = DialogResult.OK Then
            'OKなら

            '選択ディレクトリ
            backDir = String.Concat(sfd.SelectedPath, "\")

        End If

        'ダイアログ破棄
        sfd.Dispose()

        If String.IsNullOrEmpty(backDir) Then
            '画面解除
            Call Me._LMBConH.FailureSelect(frm)
            'Cursorを元に戻す
            Cursor.Current = Cursors.Default()
            Exit Sub
        End If

        'データセット設定(選択画像情報)
        Call SetDatasetSelPthotoData(frm, ds)

        '画像コピー
        Dim filePathTo As String
        For Each row As DataRow In ds.Tables(LMB070C.TABLE_NM_OUT_INKA_PHOTO).Rows

            'ADD START 2023/08/18 037916
            filePathTo = String.Concat(backDir, System.IO.Path.GetFileName(row.Item("FILE_PATH").ToString()))
            Using myClient As System.Net.WebClient = New System.Net.WebClient
                myClient.DownloadFile(row.Item("FILE_PATH").ToString(), filePathTo)
            End Using
            'ADD END 2023/08/18 037916
            'DEL START 2023/08/18 037916
            'If System.IO.File.Exists(row.Item("FILE_PATH").ToString()) Then
            '    filePathTo = String.Concat(backDir, System.IO.Path.GetFileName(row.Item("FILE_PATH").ToString()))
            '    System.IO.File.Copy(row.Item("FILE_PATH").ToString(), filePathTo, True)
            'End If
            'DEL END 2023/08/18 037916
        Next

        MyBase.ShowMessage(frm, "G002", New String() {frm.btnDownLoad.Text, ""})

        '終了処理
        Call Me._LMBConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 編集ボタンクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub EditAction(ByVal frm As LMB070F, ByVal sender As Object)
        Dim objButton As Button = CType(sender, Button)

        '処理開始
        Call Me._LMBConH.StartAction(frm)

        '変更前データ退避
        Dim ds As DataSet = New LMB070DS()
        _PrmDsSave = ds
        Dim drow As DataRow = _PrmDsSave.Tables(LMB070C.TABLE_NM_SAVE).NewRow

        With frm

            '編集対象No
            .txtNo.TextValue = objButton.Parent.Tag.ToString()
            Dim idx As Integer = 0

            '対象データのみ編集可能な状態にする+変更前項目を退避する
            For Each ctl As Control In .pnlDetailIn.Controls
                If ctl.Tag.ToString().Equals(.txtNo.TextValue) Then
                    '対象データ
                    For Each ctl2 As Control In .pnlDetailIn.Controls(idx).Controls
                        If ctl2.Name.IndexOf("btnEdit_") >= 0 Then
                            ctl2.Visible = False
                        ElseIf ctl2.Name.IndexOf("btnSave_") >= 0 Then
                            ctl2.Visible = True
                        ElseIf ctl2.Name.IndexOf(LMB070C.CTLNM_SHOHIN_NM) >= 0 Then
                            DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).ReadOnly = False
                            drow("SHOHIN_NM") = DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue
                        ElseIf ctl2.Name.IndexOf(LMB070C.CTLNM_SYS_UPD_DATE) >= 0 Then
                            drow("SYS_UPD_DATE") = DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue
                        ElseIf ctl2.Name.IndexOf(LMB070C.CTLNM_SYS_UPD_TIME) >= 0 Then
                            drow("SYS_UPD_TIME") = DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue
                        End If
                    Next
                Else
                    '対象データ以外
                    For Each ctl3 As Control In .pnlDetailIn.Controls(idx).Controls
                        If ctl3.Name.IndexOf("btnEdit_") >= 0 Then
                            ctl3.Visible = False
                        End If
                    Next
                End If

                idx += 1
            Next

            _PrmDsSave.Tables(LMB070C.TABLE_NM_SAVE).Rows.Add(drow)

            'ファンクションキーの活性非活性を変更する
            .FunctionKey.F9ButtonEnabled = False
            .FunctionKey.F11ButtonEnabled = False

            '終了処理
            Call Me._LMBConH.EndAction(frm, "G003")

            '終了処理共通部品内で単項目のロックが自動で解除されるため終了処理後にロックする
            .imdSatsueiDateFrom.ReadOnly = True
            .imdSatsueiDateTo.ReadOnly = True
            .txtKeyword.ReadOnly = True
            .btnDownLoad.Enabled = False

        End With

    End Sub

    ''' <summary>
    ''' 保存ボタンクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub SaveAction(ByVal frm As LMB070F, ByVal sender As Object)
        Dim objButton As Button = CType(sender, Button)

        '処理開始
        Call Me._LMBConH.StartAction(frm)

        With frm

            'DataSet設定
            Dim ds As DataSet = New LMB070DS()
            Dim drow As DataRow = ds.Tables(LMB070C.TABLE_NM_SAVE).NewRow

            drow("NO") = .txtNo.TextValue

            For Each ctl0 As Control In objButton.Parent.Controls
                If ctl0.Name.IndexOf(LMB070C.CTLNM_SHOHIN_NM) >= 0 Then
                    drow("SHOHIN_NM") = DirectCast(ctl0, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue
                ElseIf ctl0.Name.IndexOf(LMB070C.CTLNM_SYS_UPD_DATE) >= 0 Then
                    drow("SYS_UPD_DATE") = DirectCast(ctl0, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue
                ElseIf ctl0.Name.IndexOf(LMB070C.CTLNM_SYS_UPD_TIME) >= 0 Then
                    drow("SYS_UPD_TIME") = DirectCast(ctl0, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue
                End If
            Next

            If drow("SHOHIN_NM").ToString().Length > 50 Then

                'メッセージ表示
                MyBase.ShowMessage(frm, "E993", New String() {"入庫品名", "50"})

                '終了処理(共通部品内で活性非活性が変わるため)
                Call Me._LMBConH.EndAction(frm)
            Else

                ds.Tables(LMB070C.TABLE_NM_SAVE).Rows.Add(drow)

                '更新
                ds = MyBase.CallWSA("LMB070BLF", "UpdateNpPhoto", ds)

                Dim syohinNm As String = String.Empty
                Dim sysUpdDate As String = String.Empty
                Dim sysUpdTime As String = String.Empty

                'エラーがある場合、終了
                If MyBase.IsErrorMessageExist() = True Then

                    'メッセージ表示
                    MyBase.ShowMessage(frm)

                    syohinNm = _PrmDsSave.Tables(LMB070C.TABLE_NM_SAVE).Rows(0).Item("SHOHIN_NM").ToString()
                    sysUpdDate = _PrmDsSave.Tables(LMB070C.TABLE_NM_SAVE).Rows(0).Item("SYS_UPD_DATE").ToString()
                    sysUpdTime = _PrmDsSave.Tables(LMB070C.TABLE_NM_SAVE).Rows(0).Item("SYS_UPD_TIME").ToString()

                Else

                    MyBase.ShowMessage(frm, "G002", New String() {"保存", ""})

                    syohinNm = ds.Tables(LMB070C.TABLE_NM_SAVE).Rows(0).Item("SHOHIN_NM").ToString()
                    sysUpdDate = ds.Tables(LMB070C.TABLE_NM_SAVE).Rows(0).Item("SYS_UPD_DATE").ToString()
                    sysUpdTime = ds.Tables(LMB070C.TABLE_NM_SAVE).Rows(0).Item("SYS_UPD_TIME").ToString()

                    '保存を行ったデータが前回登録データだった場合、前回登録データに変更後の値を適用する
                    If Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows.Count > 0 Then

                        Dim targetFlg As Boolean = False

                        For Each row As DataRow In Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows

                            If .txtNo.TextValue.Equals(row("NO")) Then
                                '返却用のDs設定
                                row("SHOHIN_NM") = syohinNm
                                row("SYS_UPD_DATE") = sysUpdDate
                                row("SYS_UPD_TIME") = sysUpdTime

                                targetFlg = True
                            End If

                        Next

                        If targetFlg Then

                            '初期表示に使用するDataTableにコピー
                            Call SetDatasetOutInkaData(frm, Me._PrmDs)

                            'リターンコードの設定
                            Me._Prm.ReturnFlg = True

                        End If

                    End If

                End If

                _PrmDsSave.Clear()

                '終了処理(共通部品内で活性非活性が変わるため)
                Call Me._LMBConH.EndAction(frm)

                Dim idx As Integer = 0

                '活性状態を初期状態に戻す
                For Each ctl As Control In .pnlDetailIn.Controls
                    If ctl.Tag.ToString().Equals(.txtNo.TextValue) Then
                        '対象データ
                        For Each ctl2 As Control In .pnlDetailIn.Controls(idx).Controls
                            If ctl2.Name.IndexOf("btnEdit_") >= 0 Then
                                ctl2.Visible = True
                            ElseIf ctl2.Name.IndexOf("btnSave_") >= 0 Then
                                ctl2.Visible = False
                            ElseIf ctl2.Name.IndexOf(LMB070C.CTLNM_SHOHIN_NM) >= 0 Then
                                DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).ReadOnly = True
                                DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue = syohinNm
                            ElseIf ctl2.Name.IndexOf(LMB070C.CTLNM_SYS_UPD_DATE) >= 0 Then
                                DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue = sysUpdDate
                            ElseIf ctl2.Name.IndexOf(LMB070C.CTLNM_SYS_UPD_TIME) >= 0 Then
                                DirectCast(ctl2, Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox).TextValue = sysUpdTime
                            End If
                        Next
                    Else
                        '対象データ以外
                        For Each ctl3 As Control In .pnlDetailIn.Controls(idx).Controls
                            If ctl3.Name.IndexOf("btnEdit_") >= 0 Then
                                ctl3.Visible = True
                            End If
                        Next
                    End If

                    idx += 1
                Next

                '保存時の項目の活性非活性を変更する
                .FunctionKey.F9ButtonEnabled = True
                .FunctionKey.F11ButtonEnabled = True
                .imdSatsueiDateFrom.ReadOnly = False
                .imdSatsueiDateTo.ReadOnly = False
                .txtKeyword.ReadOnly = False
                .btnDownLoad.Enabled = True

                .txtNo.TextValue = String.Empty

            End If

        End With

    End Sub

    ''' <summary>
    ''' 全て選択ボタンクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub PictureAllSelClick(ByVal frm As LMB070F, ByVal sender As Object)
        Dim objButton As Button = CType(sender, Button)

        '画像を全て選択
        For Each ctl As Control In objButton.Parent.Controls
            'DEL START 2023/08/18 037916
            'If ctl.Name.IndexOf(LMB070C.CTLNM_PHOTO) >= 0 Then
            '    ctl.Tag = LMB070C.PHOTO_SEL
            'End If
            'DEL END 2023/08/18 037916
            If ctl.Name.IndexOf(LMB070C.CTLNM_PHOTOLABEL) >= 0 Then
                ctl.Tag = LMB070C.PHOTO_SEL 'ADD 2023/08/18 037916
                ctl.BackColor = Me._G.ColorPhotoSel
            End If
        Next

    End Sub

    ''' <summary>
    ''' 全てを表示ボタンクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub PictureAllDispClick(ByVal frm As LMB070F, ByVal sender As Object)
        Dim objButton As Button = CType(sender, Button)
        Dim photoNo As String = objButton.Parent.Tag.ToString()

        '画像件数を取得
        Dim photoCount As Integer = 0
        If Me._PhotoDict.ContainsKey(photoNo) Then
            photoCount = Me._PhotoDict(photoNo)
        End If

        Dim photoVisible As Boolean
        If objButton.Text.Equals(LMB070C.BTNTITLE_ALLDISP) Then
            '全てを表示
            photoVisible = True
        Else
            '一部を表示
            photoVisible = False
        End If

        '画像の表示非表示切り替え
        '画像10枚まで常に表示、11枚目以降の表示非表示を切り替える
        Dim ctlNmNo As Integer = Integer.Parse(objButton.Parent.Name.Replace(LMB070C.CTLNM_PANELB, ""))
        For i As Integer = 11 To photoCount
            Dim fileNmLbl As String = String.Concat(LMB070C.CTLNM_PHOTOLABEL, ctlNmNo.ToString(), "_", i.ToString())
            Dim ctrlLbl() As Control = frm.Controls.Find(fileNmLbl, True)
            If ctrlLbl.Length > 0 Then
                ctrlLbl(0).Visible = photoVisible
            End If

            Dim fileNmPic As String = String.Concat(LMB070C.CTLNM_PHOTO, ctlNmNo.ToString(), "_", i.ToString())
            Dim ctrlPic() As Control = frm.Controls.Find(fileNmPic, True)
            If ctrlPic.Length > 0 Then
                ctrlPic(0).Visible = photoVisible
            End If
        Next

        'コンテナサイズ変更
        Dim sizePanelX As Integer = objButton.Parent.Size.Width
        Dim sizePanelY As Integer = objButton.Parent.Size.Height
        Dim sizeDetailInX As Integer = frm.pnlDetailIn.Size.Width
        Dim sizeDetailInY As Integer = frm.pnlDetailIn.Size.Height
        Dim sizeSabun As Integer
        If photoVisible Then
            Dim addRowCnt As Integer = CType(Math.Ceiling((photoCount - 10) / 5), Integer)
            sizeSabun = (LMB070C.SIZE_PANEL_ADD_Y * addRowCnt)
        Else
            sizeSabun = LMB070C.SIZE_PANEL_Y - sizePanelY
        End If
        objButton.Parent.Size = New System.Drawing.Size(sizePanelX, (sizePanelY + sizeSabun))
        frm.pnlDetailIn.Size = New System.Drawing.Size(sizeDetailInX, (sizeDetailInY + sizeSabun))

        'ボタン位置移動
        Dim buttonLocationX As Integer = objButton.Location.X
        Dim buttonLocationY As Integer = objButton.Location.Y
        objButton.Location = New System.Drawing.Point(buttonLocationX, (buttonLocationY + sizeSabun))

        'ボタンキャプション切り替え
        If photoVisible Then
            objButton.Text = LMB070C.BTNTITLE_HIDEDISP
        Else
            objButton.Text = LMB070C.BTNTITLE_ALLDISP
        End If

        'ボタン押下コンテナより下のコンテナ位置移動
        Dim max As Integer = Me._PhotoDict.Count
        For i As Integer = (ctlNmNo + 1) To max
            Dim fileNmPanel As String = String.Concat(LMB070C.CTLNM_PANELB, i.ToString())
            Dim ctrlPanel() As Control = frm.Controls.Find(fileNmPanel, True)
            If ctrlPanel.Length > 0 Then
                Dim panelLocationX As Integer = ctrlPanel(0).Location.X
                Dim panelLocationY As Integer = ctrlPanel(0).Location.Y
                ctrlPanel(0).Location = New System.Drawing.Point(panelLocationX, (panelLocationY + sizeSabun))
            End If
        Next

    End Sub

    ''' <summary>
    ''' 画像クリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub PictureClick(ByVal frm As LMB070F, ByVal sender As Object)
        'クリックされた画像(PictureBox)
        Dim objPic As PictureBox = CType(sender, PictureBox)
        'クリックされた画像の裏にあるラベル
        Dim lblNm As String = objPic.Name.Replace(LMB070C.CTLNM_PHOTO, LMB070C.CTLNM_PHOTOLABEL)

        Dim ctrl() As Control = frm.Controls.Find(lblNm, True)
        If ctrl.Length > 0 Then

            '画像選択 or 選択解除
            'UPD START 2023/08/18 037916
            'If objPic.Tag Is Nothing OrElse String.IsNullOrEmpty(objPic.Tag.ToString()) Then
            '    ctrl(0).BackColor = Me._G.ColorPhotoSel
            '    objPic.Tag = LMB070C.PHOTO_SEL
            'Else
            '    ctrl(0).BackColor = Me._G.ColorPhotoNotSel
            '    objPic.Tag = String.Empty
            'End If
            If ctrl(0).Tag Is Nothing OrElse String.IsNullOrEmpty(ctrl(0).Tag.ToString()) Then
                ctrl(0).BackColor = Me._G.ColorPhotoSel
                ctrl(0).Tag = LMB070C.PHOTO_SEL
            Else
                ctrl(0).BackColor = Me._G.ColorPhotoNotSel
                ctrl(0).Tag = String.Empty
            End If
            'UPD END 2023/08/18 037916
        End If

    End Sub

    ''' <summary>
    ''' 画像ダブルクリック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="sender"></param>
    Private Sub PictureDoubleClick(ByVal frm As LMB070F, ByVal sender As Object)

        '画像照会(LMB090)を表示
        Dim objPic As PictureBox = CType(sender, PictureBox)
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = New LMB090DS
        Dim row As DataRow = prmDs.Tables("LMB090IN").NewRow
        'UPD START 2023/08/18 037916
        'row("FILE_PATH") = objPic.ImageLocation
        row("IMAGE") = objPic.Image
        'UPD END 2023/08/18 037916
        prmDs.Tables("LMB090IN").Rows.Add(row)
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMB090", prm)

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 前回登録データのみ表示
    ''' </summary>
    Private Sub DispLastEntData()

        '前回登録データが存在する場合、前回登録データを表示
        If Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows.Count > 0 Then
            Dim ds As DataSet = New LMB070DS()
            Dim dispTbl As DataTable = ds.Tables(LMB070C.TABLE_NM_OUT)

            For Each row As DataRow In Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows
                Dim drow As DataRow = dispTbl.NewRow

                drow("NO") = row("NO")
                drow("SHOHIN_NM") = row("SHOHIN_NM")
                drow("SATSUEI_DATE") = row("SATSUEI_DATE")
                drow("USER_LNM") = row("USER_LNM")
                drow("SYS_UPD_DATE") = row("SYS_UPD_DATE")
                drow("SYS_UPD_TIME") = row("SYS_UPD_TIME")
                drow("FILE_PATH") = row("FILE_PATH")

                dispTbl.Rows.Add(drow)
            Next

            For Each row As DataRow In Me._PrmDs.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows
                ds.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).ImportRow(row)
            Next

            '前回登録データを表示
            Me._G.AddDetailInCtl(ds, Me._PhotoDict)
        End If

    End Sub

    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMB070F) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble(
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))
        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble(
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))
        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Dim ds As DataSet = New LMB070DS()
        Call Me.SetDatasetInData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
        Dim rtnDs As DataSet
        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMBConH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMB070BLF", "SelectListData", ds _
                                                         , lc, mc, "1")

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 選択処理（登録ボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMB070F)

        '処理開始アクション
        Me._LMBConH.StartAction(frm)

        'データセット設定(選択画像情報)
        Call SetDatasetSelPthotoData(frm, Me._PrmDs)

        If Not String.IsNullOrEmpty(Me._PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("DISP_MODE").ToString()) Then
            MyBase.Logger.StartLog(MyBase.GetType.Name, "RowOkSelect")

            '画像選択情報を登録する
            MyBase.CallWSA("LMB070BLF", "UpdateInkaPhoto", Me._PrmDs)

            'メッセージコードの判定
            If MyBase.IsErrorMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Me._LMBConH.EndAction(frm)  '終了処理
                Exit Sub
            End If

            MyBase.Logger.EndLog(MyBase.GetType.Name, "RowOkSelect")

            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), ""})

            '初期表示に使用するDataTableにコピー
            Call SetDatasetDeleteData(frm, Me._PrmDs)

            'リターンコードの設定
            Me._Prm.ReturnFlg = True

            'outのパラメータをセット
            Me._Prm.ParamDataSet = Me._PrmDs

            '処理終了アクション
            Me._LMBConH.EndAction(frm)

            Exit Sub
        End If

        'リターンコードの設定
        Me._Prm.ReturnFlg = True

        'outのパラメータをセット
        Me._Prm.ParamDataSet = Me._PrmDs

        '処理終了アクション
        Me._LMBConH.EndAction(frm)

        '画面を閉じる
        frm.Close()

    End Sub

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(選択画像情報)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetSelPthotoData(ByVal frm As LMB070F, ByVal ds As DataSet)

        Dim max As Integer = Me._PhotoDict.Count
        Dim pnlName As String
        Dim dt As DataTable = ds.Tables(LMB070C.TABLE_NM_OUT_INKA_PHOTO)
        Dim outDr As DataRow = dt.NewRow()
        dt.Clear()

        For i As Integer = 1 To max
            pnlName = String.Concat(LMB070C.CTLNM_PANELB, i.ToString())
            Dim ctrlPanel() As Control = frm.Controls.Find(pnlName, True)
            If ctrlPanel.Length > 0 Then

                Dim ctrlShohinNm() As Control = frm.Controls.Find(String.Concat(LMB070C.CTLNM_SHOHIN_NM, i.ToString()), True)
                Dim shohinNm As String = String.Empty
                If ctrlShohinNm.Length > 0 Then
                    Dim objTxt1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox =
                        CType(ctrlShohinNm(0), Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox)
                    shohinNm = objTxt1.TextValue
                End If

                Dim ctrlSatueiDt() As Control = frm.Controls.Find(String.Concat(LMB070C.CTLNM_SATSUEI_DATE, i.ToString()), True)
                Dim satueiDt As String = String.Empty
                If ctrlSatueiDt.Length > 0 Then
                    Dim objDate1 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate =
                        CType(ctrlSatueiDt(0), Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImDate)
                    satueiDt = objDate1.TextValue
                End If

                Dim ctrlUserNm() As Control = frm.Controls.Find(String.Concat(LMB070C.CTLNM_USER_LNM, i.ToString()), True)
                Dim userNm As String = String.Empty
                If ctrlUserNm.Length > 0 Then
                    Dim objTxt2 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox =
                        CType(ctrlUserNm(0), Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox)
                    userNm = objTxt2.TextValue
                End If

                Dim ctrlSysUpdDate() As Control = frm.Controls.Find(String.Concat(LMB070C.CTLNM_SYS_UPD_DATE, i.ToString()), True)
                Dim sysUpdDate As String = String.Empty
                If ctrlSysUpdDate.Length > 0 Then
                    Dim objTxt3 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox =
                        CType(ctrlSysUpdDate(0), Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox)
                    sysUpdDate = objTxt3.TextValue
                End If

                Dim ctrlSysUpdTime() As Control = frm.Controls.Find(String.Concat(LMB070C.CTLNM_SYS_UPD_TIME, i.ToString()), True)
                Dim sysUpdTime As String = String.Empty
                If ctrlSysUpdTime.Length > 0 Then
                    Dim objTxt4 As Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox =
                        CType(ctrlSysUpdTime(0), Jp.Co.Nrs.LM.GUI.Win.InputMan.LMImTextBox)
                    sysUpdTime = objTxt4.TextValue
                End If

                For Each ctl As Control In ctrlPanel(0).Controls
                    '画像コントロールの場合
                    If ctl.Name.IndexOf(LMB070C.CTLNM_PHOTO) >= 0 Then
                        'DEL START 2023/08/18 037916
                        ''選択画像の場合
                        'If Not ctl.Tag Is Nothing AndAlso Not String.IsNullOrEmpty(ctl.Tag.ToString()) Then
                        'DEL END 2023/08/18 037916
                        'ADD START 2023/08/18 037916
                        '画像コントロールの裏にあるラベル
                        Dim lblNm As String = ctl.Name.Replace(LMB070C.CTLNM_PHOTO, LMB070C.CTLNM_PHOTOLABEL)
                        Dim lblCtrl() As Control = frm.Controls.Find(lblNm, True)
                        If lblCtrl.Length > 0 Then
                            '選択画像の場合
                            If Not lblCtrl(0).Tag Is Nothing AndAlso Not String.IsNullOrEmpty(lblCtrl(0).Tag.ToString()) Then
                                'ADD END 2023/08/18 037916
                                Dim ctlPic As PictureBox = CType(ctl, PictureBox)

                                outDr = dt.NewRow()

                                outDr("NRS_BR_CD") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD")
                                outDr("INKA_NO_L") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("INKA_NO_L")
                                outDr("INKA_NO_M") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("INKA_NO_M")
                                outDr("INKA_NO_S") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("INKA_NO_S")
                                outDr("NO") = ctrlPanel(0).Tag.ToString()
                                outDr("SHOHIN_NM") = shohinNm
                                outDr("SATSUEI_DATE") = satueiDt
                                outDr("USER_LNM") = userNm
                                outDr("SYS_UPD_DATE") = sysUpdDate
                                outDr("SYS_UPD_TIME") = sysUpdTime
                                'UPD START 2023/08/18 037916
                                'outDr("FILE_PATH") = ctlPic.ImageLocation
                                outDr("FILE_PATH") = ctlPic.Tag
                                'UPD END 2023/08/18 037916

                                '設定値をデータセットに設定
                                dt.Rows.Add(outDr)
                            End If
                        End If  'ADD 2023/08/18 037916
                    End If
                Next
            End If
        Next

        Return

    End Sub

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    Private Sub SetDatasetDeleteData(ByVal frm As LMB070F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO)
        Dim outDr As DataRow = dt.NewRow()
        dt.Clear()

        For Each row As DataRow In ds.Tables(LMB070C.TABLE_NM_OUT_INKA_PHOTO).Rows
            outDr = dt.NewRow()

            outDr("NRS_BR_CD") = row("NRS_BR_CD")
            outDr("INKA_NO_L") = row("INKA_NO_L")
            outDr("INKA_NO_M") = row("INKA_NO_M")
            outDr("INKA_NO_S") = row("INKA_NO_S")
            outDr("NO") = row("NO")
            outDr("FILE_PATH") = row("FILE_PATH")
            outDr("SHOHIN_NM") = row("SHOHIN_NM")
            outDr("SATSUEI_DATE") = row("SATSUEI_DATE")
            outDr("USER_LNM") = row("USER_LNM")
            outDr("SYS_UPD_DATE") = row("SYS_UPD_DATE")
            outDr("SYS_UPD_TIME") = row("SYS_UPD_TIME")

            '設定値をデータセットに設定
            dt.Rows.Add(outDr)
        Next

    End Sub

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    Private Sub SetDatasetOutInkaData(ByVal frm As LMB070F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMB070C.TABLE_NM_OUT_INKA_PHOTO)
        Dim outDr As DataRow = dt.NewRow()
        dt.Clear()

        For Each row As DataRow In ds.Tables(LMB070C.TABLE_NM_IN_INKA_PHOTO).Rows
            outDr = dt.NewRow()

            outDr("NRS_BR_CD") = row("NRS_BR_CD")
            outDr("INKA_NO_L") = row("INKA_NO_L")
            outDr("INKA_NO_M") = row("INKA_NO_M")
            outDr("INKA_NO_S") = row("INKA_NO_S")
            outDr("NO") = row("NO")
            outDr("FILE_PATH") = row("FILE_PATH")
            outDr("SHOHIN_NM") = row("SHOHIN_NM")
            outDr("SATSUEI_DATE") = row("SATSUEI_DATE")
            outDr("USER_LNM") = row("USER_LNM")
            outDr("SYS_UPD_DATE") = row("SYS_UPD_DATE")
            outDr("SYS_UPD_TIME") = row("SYS_UPD_TIME")

            '設定値をデータセットに設定
            dt.Rows.Add(outDr)
        Next

    End Sub

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetInData(ByVal frm As LMB070F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMB070C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("NRS_BR_CD")
        drow("INKA_NO_L") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("INKA_NO_L")
        drow("INKA_NO_M") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("INKA_NO_M")
        drow("INKA_NO_S") = _PrmDs.Tables(LMB070C.TABLE_NM_IN).Rows(0).Item("INKA_NO_S")
        drow("SATSUEI_DATE_FROM") = frm.imdSatsueiDateFrom.TextValue
        drow("SATSUEI_DATE_TO") = frm.imdSatsueiDateTo.TextValue
        drow("KEYWORD") = frm.txtKeyword.TextValue.Trim()

        ds.Tables(LMB070C.TABLE_NM_IN).Rows.Add(drow)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMB070F) As Boolean

        Return True

    End Function

#End Region 'DataSet設定

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMB070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey9Press")

        '検索処理
        Call Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(OK処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMB070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        '選択処理
        Call Me.RowOkSelect(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMB070F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMB070F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    ''' <summary>
    ''' Downloadボタン処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub btnDownLoad_Click(ByVal frm As LMB070F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnDownLoad_Click")

        Call Me.DownLoadAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnDownLoad_Click")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' 編集クリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnEditClick(ByRef frm As LMB070F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnEditClick")

        'クリックアクション処理
        Call Me.EditAction(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnEditClick")

    End Sub

    ''' <summary>
    ''' 保存クリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnSaveClick(ByRef frm As LMB070F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnSaveClick")

        'クリックアクション処理
        Call Me.SaveAction(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnSaveClick")

    End Sub

    ''' <summary>
    ''' 全て選択クリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAllSelClick(ByRef frm As LMB070F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnAllSelClick")

        'クリックアクション処理
        Call Me.PictureAllSelClick(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnAllSelClick")

    End Sub

    ''' <summary>
    ''' 全てを表示クリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub btnAllDispClick(ByRef frm As LMB070F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnAllDispClick")

        'クリックアクション処理
        Call Me.PictureAllDispClick(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnAllDispClick")

    End Sub

    ''' <summary>
    ''' 画像クリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub picThumbnailClick(ByRef frm As LMB070F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "picThumbnailClick")

        'クリックアクション処理
        Call Me.PictureClick(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "picThumbnailClick")

    End Sub

    ''' <summary>
    ''' 画像ダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub picThumbnailDoubleClick(ByRef frm As LMB070F, ByVal sender As Object, ByVal e As EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "picThumbnailDoubleClick")

        'ダブルクリックアクション処理
        Call Me.PictureDoubleClick(frm, sender)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "picThumbnailDoubleClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class