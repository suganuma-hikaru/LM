' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : 特定荷主機能
'  プログラムID     :  LMI220H : 定期検査管理
'  作  成  者       :  [金]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
Imports Microsoft.Office.Interop

''' <summary>
''' LMI220ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI220H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI220V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI220G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConV As LMIControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' ParameterDS格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

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
        Me._Prm = prm
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI220F = New LMI220F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(frm)

        'Validate共通クラスの設定
        Me._LMIConV = New LMIControlV(Me, sFrom, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI220G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI220V(Me, frm, Me._LMIConV)

        'フォームの初期化
        MyBase.InitControl(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'Enter押下イベントの設定
        Call Me._LMIConV.SetEnterEvent(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        '編集部の項目をクリア
        Call Me._G.ClearControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

        '初期値設定
        Call Me._G.SetInitSpreadValue()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMI220C.EventShubetsu.SHOKI)

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

#Region "ActionControl"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMI220C.EventShubetsu, ByVal frm As LMI220F, Optional ByVal rowNo As Integer = -1)

        'ディスプレイモード、レコードステータス保存域
        Dim mode As String = String.Empty
        Dim status As String = String.Empty

        ' ********** 処理前の共通処理 **********

        '現在コントロールからフォーカスを外す
        frm.sprDetail.Focus()

        '処理開始アクション
        Call Me._LMIConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            Call Me._LMIConH.EndAction(frm)
            Exit Sub
        End If

        '入力チェック
        If Me._V.IsInputCheck(frm, eventShubetsu) = False Then
            Call Me._LMIConH.EndAction(frm)
            Exit Sub
        End If

        ' **************************************

        ' ************* メイン処理 *************

        Select Case eventShubetsu

            Case LMI220C.EventShubetsu.SHINKI
                Call Me.NewDataEvent(frm)

            Case LMI220C.EventShubetsu.HENSHU
                Call Me.EditDataEvent(frm)

            Case LMI220C.EventShubetsu.SAKUJO_HUKKATSU
                Call Me.DelReviveDataEvent(frm)

            Case LMI220C.EventShubetsu.KENSAKU
                Call Me.SelectListEvent(frm)

            Case LMI220C.EventShubetsu.HOZON
                Call Me.SaveData(frm)

            Case LMI220C.EventShubetsu.DOUBLECLICK
                Call Me.RowSelection(frm, rowNo)

            Case LMI220C.EventShubetsu.TORIKOMI_KOSHIN
                Call Me.ImportSaveData(frm)

        End Select

    End Sub

#End Region

#Region "NewDataEvent"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMI220F)

        'DataSet設定(MAXユーザーコード枝番のクリア)
        If Me._Ds Is Nothing Then
            Me._Ds = New LMI220DS()
        End If
        Me._Ds.Clear()

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面入力項目のクリア
        Call Me._G.ClearControl()

        '営業所コード設定
        frm.cmbNrsBrCd.SelectedValue = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMI220C.EventShubetsu.SHINKI)

    End Sub

#End Region

#Region "EditDataEvent"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMI220F)

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMIConH.EndAction(frm)
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMI220C.EventShubetsu.HENSHU) = False Then
            Call Me._LMIConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = Me.SetDataSetInData(frm, frm.sprDetail.ActiveSheet.ActiveRowIndex)
        Me._Ds = New LMI220DS()

        '==========================
        'WSAクラス呼出()
        '==========================
        ds = MyBase.CallWSA("LMI220BLF", "HaitaData", ds)

        'データセットの内容保持
        Me._Ds = ds

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._LMIConH.EndAction(frm)

            Exit Sub

        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G003")

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMI220C.EventShubetsu.HENSHU)

    End Sub

#End Region

#Region "DelReviveDataEvent"

    ''' <summary>
    ''' 削除・復活処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DelReviveDataEvent(ByVal frm As LMI220F)

        Dim tltStr As String = String.Empty
        Dim actionStr As String = String.Empty
        If frm.lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) Then
            tltStr = "削除"
            actionStr = "DeleteData"
        Else
            tltStr = "復活"
            actionStr = "ReviveData"
        End If

        '確認メッセージ
        Select Case MyBase.ShowMessage(frm, "C001", New String() {tltStr})
            Case MsgBoxResult.Cancel  '「キャンセル」押下時
                Call Me._LMIConH.EndAction(frm)
                'メッセージ表示
                MyBase.ShowMessage(frm, "G013")
                Exit Sub
        End Select

        'DataSet設定
        Dim ds As DataSet = Me.SetDataSetInDataHozon(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelReviveData")

        '削除・復活処理
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI220BLF", actionStr, ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMIConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelReviveData")

        '処理結果メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {tltStr, String.Concat("[", frm.lblSerialNo.Text, " = ", frm.txtSerialNo.TextValue, "]")})

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        '編集部クリア
        Call Me._G.ClearEditControl()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

    End Sub

#End Region

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMI220F)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMIConH.EndAction(frm) '終了処理
                Call Me.SetGMessage(frm)
                Exit Sub
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMI220C.EventShubetsu.KENSAKU)

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMI220F, ByVal rowNo As Integer)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMIConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMI220G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.OFF) Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        Call Me._LMIConH.EndAction(frm) '終了処理

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal frm As LMI220F) As Boolean

        'DataSet設定
        Dim ds As DataSet = Me.SetDataSetInDataHozon(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet = New LMI220DS()

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC
                '新規登録処理
                'データ存在チェック
                If Me.IsDataExistChk(frm) = False Then
                    MyBase.ShowMessage(frm, "E010")
                    Call Me._LMIConH.EndAction(frm)  '終了処理
                    Return False
                End If
                rtnDs = MyBase.CallWSA("LMI220BLF", "InsertData", ds)

            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMI220BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMIConH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '処理結果メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblSerialNo.Text, " = ", frm.txtSerialNo.TextValue, "]")})

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        '編集部クリア
        Call Me._G.ClearEditControl()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMI220C.EventShubetsu.SHOKI)

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal frm As LMI220F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveData(frm) = False Then
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMI220C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMIControlC.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMIConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        '処理終了アクション
        Call Me._LMIConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMIConH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 取込更新処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ImportSaveData(ByVal frm As LMI220F) As Boolean

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        Dim fileNm As String = String.Empty
        fileNm = Me.GetFileName()
        If String.IsNullOrEmpty(fileNm) = True Then
            '終了処理
            Call Me._LMIConH.EndAction(frm)
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = Me.SetDataSetImportFile(fileNm)

#If False Then  'UPD 2021/06/28 021552 【LMS】LMS荷主特定機能】ハネウェル様扱いにおける管理データに関して(千葉BC佐久間さん)
        If Me.IsImportSaveChk(ds, frm) = False Then
            MyBase.MessageStoreDownload()
            '終了処理
            Call Me._LMIConH.EndAction(frm)
            Return False
        End If
#Else
        '更新後にエクセル出力するか
        Dim IsImportSaveChkFLG As Boolean = False

        If Me.IsImportSaveChk(ds, frm) = False Then
            'エラーがあっても更新可能かチェック
            'in件数
            Dim inRow As DataRow() = ds.Tables(LMI220C.TABLE_NM_IN).Select()
            'in更新可能件数
            Dim inOKRow As DataRow() = ds.Tables(LMI220C.TABLE_NM_IN).Select("UP_OK_FLG = '1'")

            'If inRow.Length = inOKRow.Length Then
            If inOKRow.Length > 0 Then
                '更新可能があれば更新する
                IsImportSaveChkFLG = True   '更新後に出力すする

            Else
                MyBase.MessageStoreDownload()
                '終了処理
                Call Me._LMIConH.EndAction(frm)
                Return False

            End If

        End If
#End If


        '要番2539 20160610 tsunehira add start
        '画面のサイズの値を新規登録のために取り込む
        If String.IsNullOrEmpty(Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI220G.sprDetailDef.SIZE_NM.ColNo))) = False Then
            For Each dr As DataRow In ds.Tables("LMI220IN").Rows
                dr.Item("SIZE") = Me._LMIConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMI220G.sprDetailDef.SIZE_NM.ColNo))
            Next
        Else
            MyBase.ShowMessage(frm, "E199", New String() {frm.lblSize.Text})
            Call Me._LMIConH.EndAction(frm)
            Return False
        End If
        '要番2539 20160610 tsunehira add end


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ImportData")

        '==========================
        'WSAクラス呼出
        '==========================
        '更新処理
        Dim rtnDs As DataSet = New LMI220DS()
        rtnDs = MyBase.CallWSA("LMI220BLF", "ImportData", ds)

        'EXCEL起動()
        If MyBase.IsMessageStoreExist = True Then
            MyBase.MessageStoreDownload()
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ImportData")

        '処理結果メッセージ表示
#If False Then  'UPD 2021/06/29 021552 【LMS】LMS荷主特定機能】ハネウェル様扱いにおける管理データに関して
         MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F7ButtonName.Replace("　", String.Empty), String.Concat("[", ds.Tables(LMI220C.TABLE_NM_IN).Rows.Count().ToString(), "件]")})

#Else
        'in更新可能件数
        Dim inOKRow2 As DataRow() = ds.Tables(LMI220C.TABLE_NM_IN).Select("UP_OK_FLG = '1'")

        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F7ButtonName.Replace("　", String.Empty), String.Concat("[", inOKRow2.Length().ToString(), "件]")})

#End If

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        '編集部クリア
        Call Me._G.ClearEditControl()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMI220C.EventShubetsu.SHOKI)

        Return True

    End Function

#Region "IsImportSaveChk"

    ''' <summary>
    ''' 入力チェック（取込更新）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Private Function IsImportSaveChk(ByVal ds As DataSet, ByVal frm As LMI220F) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False
        Dim rowCnt As Integer = 2
        Dim lastTestDate As Date = Nothing
        Dim nextTestDate As Date = Nothing
        Dim matsuDate As Date = Nothing
        Dim inRow As DataRow() = ds.Tables(LMI220C.TABLE_NM_IN).Select()

        For Each dr As DataRow In ds.Tables(LMI220C.TABLE_NM_IN).Rows
            lastTestDate = Nothing
            nextTestDate = Nothing
            matsuDate = Nothing
            Dim UP_OK_FLG As String = "1".ToString      '1:更新OK

            'シリンダ番号
            If String.IsNullOrEmpty(dr("SERIAL_NO").ToString()) = True Then
                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E473", New String() {"シリンダ番号", ""}, rowCnt.ToString())
                UP_OK_FLG = "0".ToString    '更新不可
            End If

#If True Then   'ADD 2019/10/30 006785【LMS】ハネウェル定期検査管理_次回定検日を製造20年以上経過のものは2年単位更新
            'シリアル番号で定期検査チェック（製造日取得）

            Dim rtnDs As DataSet = New LMI220DS()
            Dim rtnDsDr As DataRow = rtnDs.Tables(LMI220C.TABLE_NM_IN).NewRow()
            rtnDsDr("NRS_BR_CD") = dr("NRS_BR_CD").ToString()
            rtnDsDr("SERIAL_NO") = dr("SERIAL_NO").ToString()
            rtnDsDr("SYS_DEL_FLG") = LMConst.FLG.OFF

            'DSに対象１件設定
            rtnDs.Tables(LMI220C.TABLE_NM_IN).Rows.Add(rtnDsDr)

            rtnDs = Me.IsDataI_HON_TEIKEN(frm, rtnDs)

#End If

            '再検査日
            If String.IsNullOrEmpty(dr("LAST_TEST_DATE").ToString()) = True Then
                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E473", New String() {"再検査日", ""}, rowCnt.ToString())
                UP_OK_FLG = "0".ToString    '更新不可
            Else
                If IsDate(Me._G.getSlashStr(dr("LAST_TEST_DATE").ToString())) = False Then
                    MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E541", New String() {"再検査日", dr("LAST_TEST_DATE").ToString()}, rowCnt.ToString())
                    UP_OK_FLG = "0".ToString    '更新不可
                Else
                    lastTestDate = Convert.ToDateTime(Me._G.getSlashStr(dr("LAST_TEST_DATE").ToString()))
                End If
            End If


            '次回定検日
            If String.IsNullOrEmpty(dr("NEXT_TEST_DATE").ToString()) = True Then
                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E473", New String() {"次回定検日", ""}, rowCnt.ToString())
                UP_OK_FLG = "0".ToString    '更新不可
            Else
                If IsDate(Me._G.getSlashStr(dr("NEXT_TEST_DATE").ToString())) = False Then
                    MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E541", New String() {"次回定検日", dr("NEXT_TEST_DATE").ToString()}, rowCnt.ToString())
                    UP_OK_FLG = "0".ToString    '更新不可
                Else
                    nextTestDate = Convert.ToDateTime(Me._G.getSlashStr(dr("NEXT_TEST_DATE").ToString()))
                End If
            End If

            '【 関連チェック 】
            If Not nextTestDate.Equals(Nothing) AndAlso Not lastTestDate.Equals(Nothing) Then
#If True Then   'ADD 2019/10/29　006785   【LMS】ハネウェル定期検査管理_次回定検日を製造20年以上経過のものは2年単位更新
                '製造日から20年以上経過か？
                If rtnDs.Tables(LMI220C.TABLE_NM_OUT).Rows.Count <> 1 Then
                    '2019/11/29 要望管理009552 del (レコードがない＝新規登録として経過日数のチェック不要)
                    'MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E079", New String() {String.Concat("シリンダ定検データ", ""), String.Concat(" (シリンダ番号:", dr("SERIAL_NO").ToString())}, rowCnt.ToString())

                Else
                    Dim prod_DATE As Date = Convert.ToDateTime(Me._G.getSlashStr(rtnDs.Tables(LMI220C.TABLE_NM_OUT).Rows(0).Item("PROD_DATE").ToString))

                    '2019/12/02 要望管理009226 rep start
                    'Dim prod_DATE20 As Date = prod_DATE.AddYears(20)
                    '
                    'Dim calcDate1 As Date = Nothing
                    'Dim calcDate2 As Date = Nothing
                    '
                    ''If prod_DATE20 <= Convert.ToDateTime(Me._G.getSlashStr(dr("NEXT_TEST_DATE").ToString())) Then
                    'If prod_DATE20 <= Convert.ToDateTime(Me._G.getSlashStr(dr("LAST_TEST_DATE").ToString())) Then
                    '
                    '    '製造日からLAST_TEST_DATEが20年以上経過か
                    '    '次回定検日に再検査日の2年後の値を設定
                    '    calcDate1 = lastTestDate.AddYears(2)
                    '    calcDate2 = lastTestDate.AddYears(2).AddDays(-1)
                    '    If calcDate1.Month.Equals(calcDate2.Month) = False Then
                    '        matsuDate = calcDate2
                    '    Else
                    '        matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(calcDate1.Year.ToString().PadLeft(4, CChar("0")), calcDate1.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)
                    '    End If
                    '
                    '    If matsuDate.Equals(nextTestDate) = False Then
                    '        '次回定検日には再検査日の２年後の月末日を設定してください。[%1]
                    '        MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E567", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), ")")}, rowCnt.ToString())
                    '
                    '    End If
                    '
                    'Else
                    '    '製造日から20年以上経過でない
                    '    '次回定検日に再検査日の5年後の値を設定
                    '    calcDate1 = lastTestDate.AddYears(5)
                    '    calcDate2 = lastTestDate.AddYears(5).AddDays(-1)
                    '    If calcDate1.Month.Equals(calcDate2.Month) = False Then
                    '        matsuDate = calcDate2
                    '    Else
                    '        matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(calcDate1.Year.ToString().PadLeft(4, CChar("0")), calcDate1.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)
                    '    End If
                    '
                    '    If matsuDate.Equals(nextTestDate) = False Then
                    '        '次回定検日には再検査日の５年後の月末日を設定してください。[%1]
                    '        MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E540", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), ")")}, rowCnt.ToString())
                    '
                    '    End If
                    '
                    'End If

                    If prod_DATE.AddYears(18) <= Convert.ToDateTime(Me._G.getSlashStr(dr("LAST_TEST_DATE").ToString())) Then
                        '製造日からLAST_TEST_DATEが18年以上
                        '→次回検定日が[再検査日の2年後の前月末日]でなければエラー
                        Dim lastTestDate2 As Date = lastTestDate.AddYears(2)
                        matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(lastTestDate2.Year.ToString().PadLeft(4, CChar("0")), lastTestDate2.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)

                        If matsuDate.Equals(nextTestDate) = False Then
                            If ("1").Equals(UP_OK_FLG) = True Then
                                '次回定検日には再検査日の２年後の月末日を設定してください。[%1]          更新OK
                                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E567", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), " **更新しました 確認してください**", ")")}, rowCnt.ToString())

                            Else
                                '次回定検日には再検査日の２年後の月末日を設定してください。[%1]
                                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E567", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), ")")}, rowCnt.ToString())

                            End If
                        End If

                    ElseIf prod_DATE.AddYears(15) <= Convert.ToDateTime(Me._G.getSlashStr(dr("LAST_TEST_DATE").ToString())) Then
                        '製造日からLAST_TEST_DATEが15年以上,18年未満
                        '→次回検定日が[製造日の20年後の前月末日]でなければエラー
                        Dim prodDate20 As Date = prod_DATE.AddYears(20)
                        matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(prodDate20.Year.ToString().PadLeft(4, CChar("0")), prodDate20.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)

                        If matsuDate.Equals(nextTestDate) = False Then
                            If ("1").Equals(UP_OK_FLG) = True Then
                                '次回定検日には製造日の２０年後の月末日を設定してください。[%1]       更新OK
                                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E01R", New String() {String.Concat("(製造日:", prod_DATE.ToString("yyyyMMdd"), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), " **更新しました 確認してください**", ")")}, rowCnt.ToString())

                            Else
                                '次回定検日には製造日の２０年後の月末日を設定してください。[%1]
                                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E01R", New String() {String.Concat("(製造日:", prod_DATE.ToString("yyyyMMdd"), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), ")")}, rowCnt.ToString())

                            End If
                        End If

                    Else
                        '製造日からLAST_TEST_DATEが15年未満
                        '→次回検定日が[再検査日の5年後の前月末日]でなければエラー
                        Dim lastTestDate5 As Date = lastTestDate.AddYears(5)
                        matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(lastTestDate5.Year.ToString().PadLeft(4, CChar("0")), lastTestDate5.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)

                        If matsuDate.Equals(nextTestDate) = False Then
                            If ("1").Equals(UP_OK_FLG) = True Then
                                '次回定検日には再検査日の５年後の月末日を設定してください。[%1]  更新OK
                                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E540", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), " **更新しました 確認してください**", ")")}, rowCnt.ToString())

                            Else
                                '次回定検日には再検査日の５年後の月末日を設定してください。[%1]
                                MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E540", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), ")")}, rowCnt.ToString())

                            End If
                        End If
                    End If
                    '2019/12/02 要望管理009226 rep end

                End If

#Else

                '次回定検日に再検査日の5年後の値を設定
                Dim calcDate1 As Date = lastTestDate.AddYears(5)
                Dim calcDate2 As Date = lastTestDate.AddYears(5).AddDays(-1)
                If calcDate1.Month.Equals(calcDate2.Month) = False Then
                    matsuDate = calcDate2
                Else
                    matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(calcDate1.Year.ToString().PadLeft(4, CChar("0")), calcDate1.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)
                End If

                If matsuDate.Equals(nextTestDate) = False Then
                    '次回定検日には再検査日の５年後の月末日を設定してください。[%1]
                    MyBase.SetMessageStore(LMI220C.GUIDANCE_KBN, "E540", New String() {String.Concat("(再検査日:", dr("LAST_TEST_DATE").ToString(), " 次回定検日:", dr("NEXT_TEST_DATE").ToString(), ")")}, rowCnt.ToString())

                End If

#End If

            End If

            Dim rowNo As Integer = rowCnt - 2
            inRow(rowNo).Item("UP_OK_FLG") = UP_OK_FLG.ToString

            rowCnt = rowCnt + 1
        Next

        Return Not MyBase.IsMessageStoreExist

    End Function

#End Region


#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    Private Function IsDataExistChk(ByVal frm As LMI220F) As Boolean

        'DataSet設定
        Dim inDs As DataSet = Me.SetDataSetInDataHozon(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI220BLF", "IsDataExistChk", inDs)

        If rtnDs.Tables(LMI220C.TABLE_NM_OUT).Rows.Count = 0 Then
            Return True
        End If

        Return False

    End Function

#If True Then   'ADD 2019/10/30

    Private Function IsDataI_HON_TEIKEN(ByVal frm As LMI220F, ByVal ds As DataSet) As DataSet

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI220BLF", "IsDataExistChk", ds)

        'If rtnDs.Tables(LMI220C.TABLE_NM_OUT).Rows.Count = 1 Then
        '    Return True
        'End If

        Return rtnDs

    End Function


#End If

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMI220F)

        '編集部クリア
        Call Me._G.ClearEditControl()

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))

        MyBase.SetLimitCount(lc)

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1")))

        MyBase.SetMaxResultCount(mc)

        'DataSet設定
        Me._FindDs = Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(DirectCast(frm, Form), "LMI220BLF", "SelectListData", Me._FindDs, lc, mc)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI220C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(frm, rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(frm, rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMI220F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMI220C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(ds)

        '検索件数
        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        If (LMConst.FLG.OFF).Equals(Me._CntSelect) = True Then
            MyBase.ShowMessage(frm, "G001")
        Else
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        '0件でないとき
        If Me._CntSelect.Equals(LMConst.FLG.OFF) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMI220F, ByVal ds As DataSet)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G001")

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.SetFormControlsStatus()

    End Sub

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI220F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMI220F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

#End Region

#Region "DataSet設定"

#Region "SetDataSetInData"

    ''' <summary>
    ''' データセット設定(Spread)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMI220F, Optional ByVal rowNo As Integer = 0) As DataSet

        Dim ds As DataSet = New LMI220DS()
        Dim dr As DataRow = ds.Tables(LMI220C.TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            '(取消)修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            dr("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd().ToString()
            'dr("NRS_BR_CD") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.NRS_BR_CD.ColNo))
            dr("SERIAL_NO") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.SERIAL_NO.ColNo))
            dr("SIZE") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.SIZE_NM.ColNo))
            dr("PROD_DATE") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.PROD_DATE.ColNo))
            dr("LAST_TEST_DATE") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.LAST_TEST_DATE.ColNo))
            dr("NEXT_TEST_DATE") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.NEXT_TEST_DATE.ColNo))
            dr("SYS_UPD_DATE") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.SYS_UPD_DATE.ColNo))
            dr("SYS_UPD_TIME") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.SYS_UPD_TIME.ColNo))
            dr("SYS_DEL_FLG") = Me._LMIConV.GetCellValue(.Cells(rowNo, LMI220G.sprDetailDef.SYS_DEL_NM.ColNo))

            ds.Tables(LMI220C.TABLE_NM_IN).Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

#Region "SetDataSetInDataHozon"

    ''' <summary>
    ''' データセット設定(編集部データ)(保存)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInDataHozon(ByVal frm As LMI220F) As DataSet

        Dim ds As DataSet = New LMI220DS()
        Dim dr As DataRow = ds.Tables(LMI220C.TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("SERIAL_NO") = .txtSerialNo.TextValue.Trim()
            dr("SIZE") = .cmbSize.SelectedValue
            dr("PROD_DATE") = DateFormatUtility.DeleteSlash(.imdProdDate.TextValue.Trim)
            dr("LAST_TEST_DATE") = DateFormatUtility.DeleteSlash(.imdLastTestDate.TextValue.Trim)
            dr("NEXT_TEST_DATE") = DateFormatUtility.DeleteSlash(.txtNextTestDate.TextValue.Trim)
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If
            dr("SYS_DEL_FLG") = delflg

            ds.Tables(LMI220C.TABLE_NM_IN).Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

#Region "SetDataSetImportFile"

    ''' <summary>
    ''' データセット設定(取込データ)(取込更新)
    ''' </summary>
    ''' <param name="fileNm">とりこみとりこみふぁいるめい</param>
    ''' <remarks></remarks>
    Private Function SetDataSetImportFile(ByVal fileNm As String) As DataSet

        Dim ds As DataSet = New LMI220DS

        Dim dr As DataRow
        Dim fileString As String = String.Empty
        Dim gyoCount As Integer = 0
        Dim stock As String = String.Empty
        Dim stockFlag As Boolean = False
        Dim kugiri As String = String.Empty

        Dim rowNoMin As Integer = 2   '行の開始数
        Dim colNoMax As Integer = 5  '列の最大数
        Dim rowNoKey As Integer = 1        'Cashに登録されるまで、とりあえず１列目を設定

        '-----------------------------------------------------------------------------------------------
        ' EXCELファイル用
        '-----------------------------------------------------------------------------------------------
        Dim xlApp As Excel.Application = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing
        Dim xlCell As Excel.Range = Nothing

        xlApp = New Excel.Application()

        xlBooks = xlApp.Workbooks


        'EDI取込HEDの数だけループ
        ' EXCEL OPEN
        xlBook = xlBooks.Open(fileNm)

        xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)                 'とりあえず１番目のシートを設定

        xlApp.Visible = False

        '最大行を取得(rowNoKey列の最終入力行を取得)
        Dim rowNoMax As Integer = 0
        xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

        rowNoMax = xlApp.ActiveCell.Row

        '２次元配列に取得する
        Dim arrData(,) As Object
        arrData = DirectCast(xlSheet.Range(xlSheet.Cells(1, 1), xlSheet.Cells(rowNoMax, colNoMax)).Value, Object(,))

        '２次元→DSにセットする
        For j As Integer = rowNoMin To rowNoMax

            'データセットに登録
            If arrData(j, rowNoKey) Is Nothing Then

                Continue For
            Else
                If String.IsNullOrEmpty(arrData(j, rowNoKey).ToString) Then
                    Continue For
                End If
            End If

            gyoCount += 1
            dr = ds.Tables(LMI220C.TABLE_NM_IN).NewRow()
            dr("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd

            'DSに格納
            For k As Integer = 1 To colNoMax
                If arrData(j, k) Is Nothing Then Continue For
                Select Case k
                    Case LMI220C.COL_SERIAL_NO
                        dr("SERIAL_NO") = arrData(j, k).ToString
                    Case LMI220C.COL_LAST_TEST_DATE
                        dr("PROD_DATE") = arrData(j, k).ToString
                        dr("LAST_TEST_DATE") = arrData(j, k).ToString '20160613 tsunehira add 要番2539 自動取込対応
                    Case LMI220C.COL_NEXT_TEST_DATE
                        dr("NEXT_TEST_DATE") = arrData(j, k).ToString
                    Case Else
                        If k > LMI220C.COL_NEXT_TEST_DATE Then Exit For
                End Select
            Next

            'DSにAdd
            ds.Tables(LMI220C.TABLE_NM_IN).Rows.Add(dr)
        Next

        '行カウントをリセット
        gyoCount = 0

        'EXCEL CLOSE
        If xlCell IsNot Nothing Then
            System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlCell)
            xlCell = Nothing
        End If

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
        xlSheet = Nothing

        xlBook.Close(False) 'Excelを閉じる
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
        xlBook = Nothing

        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
        xlBooks = Nothing

        xlApp.DisplayAlerts = False
        xlApp.Quit()
        System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
        xlApp = Nothing

        Return ds

    End Function

#End Region

#End Region

    '要望番号:1970 s.kobayashi 2013.03.25 Start
    ''' <summary>
    ''' ファイル名取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetFileName() As String
        Dim fileName As String = String.Empty
        Dim OpenFileDialog1 As New OpenFileDialog()

        '取込ファイルの設定
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F008' AND ", _
                                                                                                        "KBN_CD = '00'"))
        If 0 < kbnDr.Length Then

            ' 初期表示するディレクトリを設定する
            OpenFileDialog1.InitialDirectory = kbnDr(0).Item("KBN_NM1").ToString().TrimEnd(CChar("\"))

            ' ファイルのフィルタを設定する
            OpenFileDialog1.Filter = String.Concat("Excel ファイル(*.xls)|", "")

            If OpenFileDialog1.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                fileName = OpenFileDialog1.FileName
            End If

        End If

        Return fileName

    End Function
    '要望番号:1970 s.kobayashi 2013.03.25 End

#End Region

#Region "イベント振分け"

#Region "FunctionKey1Press"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Call Me.ActionControl(LMI220C.EventShubetsu.SHINKI, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

#End Region

#Region "FunctionKey2Press"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        Call Me.ActionControl(LMI220C.EventShubetsu.HENSHU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

#End Region

#Region "FunctionKey4Press"

    ''' <summary>
    ''' F4押下時処理呼び出し(削除・復活)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey4Press")

        Call Me.ActionControl(LMI220C.EventShubetsu.SAKUJO_HUKKATSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey4Press")

    End Sub

#End Region

#Region "FunctionKey7Press"

    ''' <summary>
    ''' F7押下時処理呼び出し(取込更新)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey7Press")

        Call Me.ActionControl(LMI220C.EventShubetsu.TORIKOMI_KOSHIN, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey7Press")

    End Sub

#End Region

#Region "FunctionKey9Press"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Call Me.ActionControl(LMI220C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

#End Region

#Region "FunctionKey11Press"

    ''' <summary>
    ''' F11押下時処理呼び出し(保存)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Call Me.ActionControl(LMI220C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

#End Region

#Region "FunctionKey12Press"

    ''' <summary>
    ''' F12押下時処理呼び出し(終了)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

#End Region

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

#Region "ClosingForm"

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI220F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

#End Region

#Region "sprCellDoubleClick"

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMI220F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprCellDoubleClick")

        Call Me.ActionControl(LMI220C.EventShubetsu.DOUBLECLICK, frm, e.Row)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprCellDoubleClick")

    End Sub

#End Region

#Region "LMI220F_KeyDown"

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI220F_KeyDown(ByVal frm As LMI220F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMI220F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMI220F_KeyDown")

    End Sub

#End Region

    ''' <summary>
    ''' 再検査日のロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub imdLastTestDate_Leave(ByVal frm As LMI220F, ByVal e As System.EventArgs)

        If String.IsNullOrEmpty(frm.imdLastTestDate.TextValue) = True Then
            frm.txtNextTestDate.TextValue = String.Empty
            Exit Sub
        End If

        '次回定検日に再検査日の5年後の値を設定
        'Start2013/3/25 S.kobayashi  要望管理1967
        Dim calcDate1 As Date = Convert.ToDateTime(Me._G.getSlashStr(frm.imdLastTestDate.TextValue)).AddYears(5)
        Dim calcDate2 As Date = Convert.ToDateTime(Me._G.getSlashStr(frm.imdLastTestDate.TextValue)).AddYears(5).AddDays(-1)
        Dim matsuDate As Date = Nothing
        If calcDate1.Month.Equals(calcDate2.Month) = False Then
            matsuDate = calcDate2
        Else
            matsuDate = Convert.ToDateTime(Me._G.getSlashStr(String.Concat(calcDate1.Year.ToString().PadLeft(4, CChar("0")), calcDate1.Month.ToString().PadLeft(2, CChar("0")), "01"))).AddDays(-1)
        End If

        'frm.txtNextTestDate.TextValue = Convert.ToDateTime(Me._G.getSlashStr(frm.imdLastTestDate.TextValue)).AddYears(5).AddDays(-1).ToString("yyyy/MM/dd")
        frm.txtNextTestDate.TextValue = matsuDate.ToString("yyyy/MM/dd")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class