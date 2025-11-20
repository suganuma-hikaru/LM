' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM040H : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports System.IO
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base
Imports Microsoft.Office.Interop
Imports Microsoft.VisualBasic.FileIO

''' <summary>
''' LMM040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM040G

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConV As LMMControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConH As LMMControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMMConG As LMMControlG

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
    '''届先明細情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _DestDetailsDs As DataTable

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

    ''' <summary>
    '''サーバ日付用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _SysDate As String

    ''' <summary>
    ''' 画面遷移元判定フラグ
    ''' </summary>
    ''' <remarks>0:メニュー画面より遷移、1:メニュー画面以外から遷移時</remarks>
    Private _modeFlg As String = LMConst.FLG.OFF

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

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
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMM040F = New LMM040F(Me)

        'Gamen共通クラスの設定
        Dim sFrom As Form = DirectCast(frm, Form)
        Me._LMMConG = New LMMControlG(frm)

        'Validate共通クラスの設定
        Me._LMMConV = New LMMControlV(Me, sFrom, Me._LMMConG)

        'Hnadler共通クラスの設定
        Me._LMMConH = New LMMControlH(MyBase.GetPGID(), Me._LMMConV, Me._LMMConG)

        'Gamenクラスの設定
        Me._G = New LMM040G(Me, frm, Me._LMMConG)

        'Validateクラスの設定
        Me._V = New LMM040V(Me, frm, Me._LMMConV)

        'サーバ日付の設定
        Me._SysDate = MyBase.GetSystemDateTime(0)

        'フォームの初期化
        MyBase.InitControl(frm)

        '20151026 tsunehira add
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '20151026 tsunehira end

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベントの設定
        'Call Me._LMMConH.SetEnterEvent(frm)
        '2011/08/11 福田 共通動作(右セル移動不可) エンド

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        '初期設定
        Call Me.SetForm(frm, prmDs, LMControlC.LMM040C_TABLE_NM_IN)
        If Me._modeFlg.Equals(LMConst.FLG.ON) Then
            Exit Sub
        End If

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.MAIN)

        'フォームの表示
        If Me._modeFlg.Equals(LMConst.FLG.ON) Then
            'メニュー以外はモーダルで開く
            frm.ShowDialog()
        Else
            'メニューはモーダルレスで開く
            frm.Show()
        End If

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' ロード処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="prmDs">データセット</param>
    ''' <param name="tblNm">DataTable名</param>
    ''' <remarks></remarks>
    Private Sub SetForm(ByVal frm As LMM040F, ByVal prmDs As DataSet, ByVal tblNm As String)

        'メニュー画面から遷移時
        If prmDs Is Nothing Then
            Exit Sub
        End If
        Me._modeFlg = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("MODE_FLG").ToString()
        If Me._modeFlg.Equals(LMConst.FLG.ON) = False Then
            Exit Sub
        End If

        '要望番号:1217 yamanaka 2012.06.29 Start
        'Dim nrsBrCd As String = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()
        'Dim custCdL As String = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString()
        'Dim destCd As String = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("DEST_CD").ToString()
        '要望番号:1217 yamanaka 2012.06.29 End

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        'モードフラグを画面に保持
        frm.lblModeFlg.TextValue = Me._modeFlg

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '画面入力項目のクリア
        Call Me._G.ClearControl(frm)

        'モードフラグを画面に保持
        frm.lblModeFlg.TextValue = Me._modeFlg

        '要望番号:1217 yamanaka 2012.06.29 Start
        '引継ぎ項目の設定
        Call Me.SetPrmData(frm, prmDs)

        '引継ぎ項目の設定（営業所コード・荷主コード（大）・届先コード）
        'If String.IsNullOrEmpty(nrsBrCd) = False Then
        '    frm.cmbNrsBrCd.SelectedValue = nrsBrCd
        'End If
        'If String.IsNullOrEmpty(custCdL) = False Then
        '    frm.txtCustCdL.TextValue = custCdL
        '    'Pop起動処理
        '    Call Me.ShowPopupControl(frm, frm.txtCustCdL.Name, LMM040C.EventShubetsu.MAIN)
        'End If
        'If String.IsNullOrEmpty(destCd) = False Then
        '    frm.txtDestCd.TextValue = destCd
        'End If
        '要望番号:1217 yamanaka 2012.06.29 End

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'コンボボックスの値の設定
        Me._G.SetcmbValue()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.SHINKI)

        'フォームの表示
        'メニュー以外はモーダルで開く
        frm.ShowDialog()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM040F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.SHINKI) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(MAXユーザーコード枝番のクリア)
        If Me._Ds Is Nothing Then
            Me._Ds = New LMM040DS()
        End If
        Me._Ds.Clear()

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面全ロックの解除
        Me.UnLockedControls(frm)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '画面入力項目のクリア
        Call Me._G.ClearControl(frm)

        'コンボボックスの値の設定
        Me._G.SetcmbValue()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G003")

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.SHINKI)

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM040F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtDestCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'レコードステータスチェック
        If Me._V.IsRecordStatusChk(frm) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM040C.EventShubetsu.HENSHU) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim ds As DataSet = New LMM040DS()
        Call SetDatasetDestItemData(frm, ds)
        Me._Ds = New LMM040DS()

        '==========================
        'WSAクラス呼出
        '==========================
        ds = MyBase.CallWSA("LMM040BLF", "HaitaData", ds)

        'データセットの内容保持
        _Ds = ds

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            '画面全ロックの解除
            MyBase.UnLockedControls(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            '終了処理
            Call Me._LMMConH.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            '画面全ロックの解除
            Me.UnLockedControls(frm)

            '画面の入力項目/ファンクションキーの制御
            Call Me._G.UnLockedForm()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.HENSHU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM040F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtDestCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM040C.EventShubetsu.HUKUSHA) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'DataSet設定(MAXユーザーコード枝番のクリア)
        If Me._Ds Is Nothing Then
            Me._Ds = New LMM040DS()
        End If
        Me._Ds.Clear()

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'MAX届先コード枝番をデータセットに設定
        Dim ds As DataSet = New LMM040DS()
        Dim row As DataRow = ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).NewRow
        Dim maxRowCnt As Integer = frm.sprDetail2.ActiveSheet.Rows.Count - 1
        If -1 < maxRowCnt Then
            row("DEST_MAXCD_EDA") = Me._LMMConV.GetCellValue(frm.sprDetail2.ActiveSheet.Cells(maxRowCnt, LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo))
            ds.Tables(LMM040C.TABLE_NM_DEST_DETAILS_MAXEDA).Rows.Add(row)
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.HUKUSHA)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージエリアの設定
        Me.ShowMessage(frm, "G003")

    End Sub

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM040F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'データ存在チェック
        If Me._LMMConV.IsExistDataChk(frm, frm.txtDestCd.TextValue) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '他営業所選択チェック
        If Me._V.IsUserNrsBrCdChk(frm, LMM040C.EventShubetsu.SAKUJO) = False Then
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '削除フラグチェック
        If frm.lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) Then
            Select Case MyBase.ShowMessage(frm, "C013")
                'Select Case MyBase.ShowMessage(frm, "C001", New String() {"削除"})
                Case MsgBoxResult.Cancel '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        Else
            Select Case MyBase.ShowMessage(frm, "C014")
                'Select Case MyBase.ShowMessage(frm, "C001", New String() {"復活"})
                Case MsgBoxResult.Cancel  '「キャンセル」押下時
                    Call Me._LMMConH.EndAction(frm)
                    'メッセージ表示
                    MyBase.ShowMessage(frm, "G013")
                    Exit Sub
            End Select
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM040DS()
        Call Me.SetDatasetDelData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM040BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'キャッシュ最新化
        '---↓
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.DEST)
        '---↑

        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNCHIN_TARIFF_SET)
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.DEST_DETAILS)

        'メッセージ用請求先コード格納
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblDestCd.Text, " = ", frm.txtDestCd.TextValue, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 一括登録処理
    ''' </summary>
    ''' <param name="frm"></param>
    Private Sub ImportEvent(ByVal frm As LMM040F)

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        Dim setDs As DataSet = New LMM040DS

        Dim tbl As DataTable = setDs.Tables("LMM040_IMPORT")
        tbl.Clear()

        Dim localPath As String = String.Empty

        Dim openFileDialog As New OpenFileDialog()

        openFileDialog.InitialDirectory = "c:\"
        openFileDialog.Filter = "Excel ファイル (*.xl*)|*.xl*"
        openFileDialog.FilterIndex = 1
        openFileDialog.Multiselect = False
        openFileDialog.RestoreDirectory = True
        openFileDialog.Title = "ファイルを選択してください..."

        If openFileDialog.ShowDialog() = DialogResult.OK Then
            'データ取込の開始行番号
            Const ROW_START As Integer = 2

            Dim xlApp As Excel.Application = New Excel.Application()
            Dim xlBooks As Excel.Workbooks = xlApp.Workbooks
            Dim xlBook As Excel.Workbook = Nothing
            Dim xlSheets As Excel.Sheets = Nothing
            Dim xlSheet As Excel.Worksheet = Nothing
            Dim xlCells As Excel.Range = Nothing
            Dim xlCell As Excel.Range = Nothing

            'ファイル名を取得
            localPath = openFileDialog.FileNames(0)

            '取込処理
            Try
                xlBook = xlBooks.Open(localPath)
                xlSheets = xlBook.Sheets
                xlSheet = CType(xlSheets(1), Excel.Worksheet)
                xlCells = xlSheet.Cells

                'セル値を取得してDataTableにセットする
                Dim y As Integer = ROW_START
                Dim rowNum As Integer = 0

                Do
                    '荷主コード(取得)
                    xlCell = CType(xlCells(y, 1), Excel.Range)
                    Dim custCdL As String = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    '終了判定
                    If String.IsNullOrEmpty(custCdL) Then
                        Exit Do
                    End If

                    Dim drow As DataRow = setDs.Tables("LMM040_IMPORT").NewRow
                    rowNum += 1

                    '荷主コード(セット)
                    drow("CUST_CD_L") = custCdL

                    '届先コード
                    xlCell = CType(xlCells(y, 2), Excel.Range)
                    drow("DEST_CD") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    '届先名
                    xlCell = CType(xlCells(y, 3), Excel.Range)
                    drow("DEST_NM") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    '住所1
                    xlCell = CType(xlCells(y, 4), Excel.Range)
                    drow("AD_1") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    '住所2
                    drow("AD_2") = String.Empty

                    '住所3
                    xlCell = CType(xlCells(y, 5), Excel.Range)
                    drow("AD_3") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    '支払用住所
                    xlCell = CType(xlCells(y, 6), Excel.Range)
                    drow("SHIHARAI_AD") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    '郵便番号
                    xlCell = CType(xlCells(y, 7), Excel.Range)
                    drow("ZIP") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    'JISコード
                    drow("JIS") = String.Empty

                    '電話番号
                    xlCell = CType(xlCells(y, 8), Excel.Range)
                    drow("TEL") = If(xlCell.Value Is Nothing, String.Empty, xlCell.Value.ToString)
                    If xlCell IsNot Nothing Then
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                        xlCell = Nothing
                    End If

                    drow("ROW_NO") = rowNum.ToString()
                    drow("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                    drow("LOCALPATH") = localPath.Trim()

                    tbl.Rows.Add(drow)

                    y += 1
                Loop

            Catch ex As Exception
                '例外がスローされたら処理強制終了
                MyBase.ShowMessage(frm, "E547", New String() {"Excel操作"})

                '処理終了アクション
                Call Me._LMMConH.EndAction(frm)
                Exit Sub

            Finally
                'オブジェクトの解放
                If xlCell IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCell)
                    xlCell = Nothing
                End If
                If xlCells IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlCells)
                    xlCells = Nothing
                End If
                If xlSheet IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheet)
                    xlSheet = Nothing
                End If
                If xlSheets IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlSheets)
                    xlSheets = Nothing
                End If
                If xlBook IsNot Nothing Then
                    xlBook.Close(False)
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBook)
                    xlBook = Nothing
                End If
                If xlBooks IsNot Nothing Then
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlBooks)
                    xlBooks = Nothing
                End If
                If xlApp IsNot Nothing Then
                    xlApp.DisplayAlerts = False
                    xlApp.Quit()
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp)
                    xlApp = Nothing
                End If
            End Try
        End If

        '取込ファイルにレコードがない
        If tbl.Rows.Count = 0 Then
            'メッセージの表示
            MyBase.ShowMessage(frm, "E656")

            '処理終了アクション
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'WSAクラス呼び出し
        setDs = MyBase.CallWSA("LMM040BLF", "Import", setDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            'メッセージの表示
            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()

            '処理終了アクション
            Call Me._LMMConH.EndAction(frm)
            Exit Sub
        End If

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {"一括登録", String.Empty})

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

    End Sub

    'START ADD 2013/09/10 KOBAYASHI WIT対応
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintDataEvent(ByVal frm As LMM040F)

        Dim rtnDs As DataSet = New DataSet

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.INSATSU) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'チェックの付いたSpreadのRowIndexを取得
        Dim list As ArrayList = Me._LMMConH.GetCheckList(frm.sprDetail.ActiveSheet, LMM040G.sprDetailDef.DEF.ColNo)

        '項目チェック
        If Me._V.IsPrintChk(list) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '印刷処理を行う
        'DataSet設定
        Me.PrintCommon(frm, rtnDs, list)

        rtnDs.Merge(New RdPrevInfoDS)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintData")

        '==== WSAクラス呼出（印刷処理） ====
        rtnDs = MyBase.CallWSA("LMM040BLF", "PrintData", rtnDs)

        If IsMessageExist() = True Then

            '処理終了アクション
            Me._LMMConH.EndAction(frm)

            'エラーメッセージの場合
            MyBase.ShowMessage(frm)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G013", New String() {})

            '画面解除
            MyBase.UnLockedControls(frm)

            'Cursorを元に戻す
            Cursor.Current = Cursors.Default()

            Exit Sub

        End If

        'プレビュー判定 
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

        '処理終了メッセージの表示
        '20151029 tsunehira add Start
        '英語化対応
        MyBase.ShowMessage(frm, "G062")
        '20151029 tsunehira add End
        'MyBase.ShowMessage(frm, "G002", New String() {"印刷処理", ""})

        '終了処理
        Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

    End Sub
    'END   ADD 2013/09/10 KOBAYASHI WIT対応

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM040F)


        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.KENSAKU) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Call Me.SetGMessage(frm)
                Exit Sub
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.KENSAKU)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM040F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal frm As LMM040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim nrsbrcd As String = String.Empty
        Dim custcd As String = String.Empty
        Dim destcd As String = String.Empty

        nrsbrcd = frm.sprDetail.ActiveSheet.Cells(e.Row, LMM040G.sprDetailDef.NRS_BR_CD.ColNo).Text()
        custcd = frm.sprDetail.ActiveSheet.Cells(e.Row, LMM040G.sprDetailDef.CUST_CD_L.ColNo).Text()
        destcd = frm.sprDetail.ActiveSheet.Cells(e.Row, LMM040G.sprDetailDef.DEST_CD.ColNo).Text()

        Dim dt2 As DataTable = Me._DestDetailsDs

        ''メッセージ表示(編集モードの場合確認メッセージを表示する)
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMMConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(e.Row, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo)).Equals(LMConst.FLG.OFF) Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(e.Row)

        'SPREAD(明細)初期化
        frm.sprDetail2.CrearSpread()

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread2(dt2, nrsbrcd, custcd, destcd)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")
    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM040F, ByVal rowNo As Integer)

        Dim nrsbrcd As String = String.Empty
        Dim custcd As String = String.Empty
        Dim destcd As String = String.Empty

        nrsbrcd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM040G.sprDetailDef.NRS_BR_CD.ColNo).Text()
        custcd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM040G.sprDetailDef.CUST_CD_L.ColNo).Text()
        destcd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM040G.sprDetailDef.DEST_CD.ColNo).Text()

        Dim dt2 As DataTable = Me._DestDetailsDs

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.DCLICK) = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._LMMConV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'ステータス設定
        Me._G.SetModeAndStatus(, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'SPREAD(明細)初期化
        frm.sprDetail2.CrearSpread()

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread2(dt2, nrsbrcd, custcd, destcd)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMM040F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM040C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMMControlC.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM040C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveDestItemData(ByVal frm As LMM040F, ByVal eventShubetsu As LMM040C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._LMMConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM040C.EventShubetsu.HOZON) = False Then
            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False
        End If

        ''項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._LMMConH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM040DS()
        Call Me.SetDatasetDestItemData(frm, ds)

        '設定内容の保持
        _PrmDs = ds

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdateInsertData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet = New LMM040DS()

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM040BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM040BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then

            Dim errCtl As Control = Nothing
            Dim msgId As String = MyBase.GetMessageID()

            Select Case MyBase.GetMessageID()

                '2011.09.08 検証結果_導入時要望№1対応 START
                'Case "E079"

                ''①郵便番号マスタの存在チェック
                'Dim Zip As String = frm.txtZip.TextValue
                'MyBase.SetMessage("E079", New String() {"郵便番号マスタ", Zip})
                ''郵便番号マスタの存在チェックエラーの場合、住所1～3/JISコードをクリア
                'Call Me.ClearZipJisData(frm, rtnDs)
                'errCtl = frm.txtZip

                ''2011.09.08 検証結果_導入時要望№1対応 END

                ''2011.08.25 検証結果一覧№4対応 START
                'Case "E206"

                '    '②郵便番号マスタの存在チェック(該当データがN件ある場合)
                '    Dim Zip As String = frm.txtZip.TextValue
                '    MyBase.SetMessage("E206", New String() {frm.lblZip.Text, frm.lblJIS.Text})

                '    '郵便番号マスタの存在チェックエラーの場合、住所1～3/JISコードをクリア
                '    Call Me.ClearZipJisData(frm, rtnDs)
                '    errCtl = frm.txtZip
                ''2011.08.25 検証結果一覧№4対応 END
                Case "E010"

                    '③重複チェックエラー
                    errCtl = frm.txtCustCdL
                    frm.txtDestCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

            End Select

            'フォーカス設定ありの場合、
            If errCtl Is Nothing = False Then
                Call Me._LMMConV.SetErrorControl(errCtl)
            Else
                frm.ActiveControl.Focus()
            End If
            MyBase.ShowMessage(frm)

            Call Me._LMMConH.EndAction(frm)  '終了処理
            Return False

        Else
            '①JISコードのワーニング
            If MyBase.GetMessageID().Equals("W129") = True Then
                Call Me._LMMConV.SetErrorControl(frm.txtJIS)
                'メッセージの表示
                Select Case MyBase.ShowMessage(frm, "W129", New String() {frm.lblJIS.Text, frm.lblZip.Text})
                    Case MsgBoxResult.Yes '「はい」押下時
                        '郵便番号マスタから取得したJISコードで更新
                        _PrmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("MODE_FLG") = LMMControlC.FLG_ON
                        _PrmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("JIS") = rtnDs.Tables(LMM040C.TABLE_NM_OUT).Rows(0)("JIS").ToString()
                        Select Case frm.lblSituation.RecordStatus
                            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                                '新規登録処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "InsertData", _PrmDs)
                            Case Else
                                '更新処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "UpdateData", _PrmDs)
                        End Select
                    Case MsgBoxResult.No '「いいえ」押下時
                        '画面のJISコードで更新
                        _PrmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("MODE_FLG") = LMMControlC.FLG_ON
                        Select Case frm.lblSituation.RecordStatus
                            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                                '新規登録処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "InsertData", _PrmDs)
                            Case Else
                                '更新処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "UpdateData", _PrmDs)
                        End Select
                    Case MsgBoxResult.Cancel '「キャンセル」押下時
                        Call Me._LMMConV.SetErrorControl(frm.txtJIS)
                        Call Me._LMMConH.EndAction(frm)  '終了処理
                        Return False
                End Select
                '②運賃タリフセットマスタ物理削除のワーニング
            ElseIf MyBase.GetMessageID().Equals("W155") = True Then
                Call Me._LMMConV.SetErrorControl(frm.cmbTariffBunruiKbn)
                'メッセージの表示
                'Select MyBase.ShowMessage(frm, "W155", New String() {"運賃タリフセットマスタ情報"})
                '20151029 tsunehira add Start
                '英語化対応
                Select Case MyBase.ShowMessage(frm, "W252")
                    Case MsgBoxResult.Ok '「OK」押下時
                        '運賃タリフセットマスタの物理削除フラグを"1"に設定
                        _PrmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("DELETE_FLG") = LMMControlC.FLG_ON
                        _PrmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("MODE_FLG") = LMMControlC.FLG_ON
                        Select Case frm.lblSituation.RecordStatus
                            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                                '新規登録処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "InsertData", _PrmDs)
                            Case Else
                                '更新処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "UpdateData", _PrmDs)
                        End Select
                    Case MsgBoxResult.Cancel '「キャンセル」押下時
                        Call Me._LMMConV.SetErrorControl(frm.cmbTariffBunruiKbn)
                        Call Me._LMMConH.EndAction(frm)  '終了処理
                        Return False
                End Select
                '③マスタに存在しない郵便番号に対するJISコードのワーニング
            ElseIf MyBase.GetMessageID().Equals("W176") = True Then
                Call Me._LMMConV.SetErrorControl(frm.txtZip)
                'メッセージの表示
                Select Case MyBase.ShowMessage(frm, "W176", New String() {frm.lblZip.Text, frm.lblJIS.Text})
                    Case MsgBoxResult.Ok '「OK」押下時
                        _PrmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("MODE_FLG") = LMMControlC.FLG_ON
                        Select Case frm.lblSituation.RecordStatus
                            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                                '新規登録処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "InsertData", _PrmDs)
                            Case Else
                                '更新処理
                                rtnDs = MyBase.CallWSA("LMM040BLF", "UpdateData", _PrmDs)
                        End Select
                    Case MsgBoxResult.Cancel '「キャンセル」押下時
                        Call Me._LMMConV.SetErrorControl(frm.cmbTariffBunruiKbn)
                        Call Me._LMMConH.EndAction(frm)  '終了処理
                        Return False
                End Select
            End If
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdateInsertData")

        'キャッシュ最新化
        '---↓
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.DEST)
        '---↑

        MyBase.LMCacheMasterData(LMConst.CacheTBL.UNCHIN_TARIFF_SET)
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.DEST_DETAILS)

        '処理結果メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblDestCd.Text, " = ", frm.txtDestCd.TextValue, "]")})

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus(LMM040C.EventShubetsu.MAIN)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        If Me._modeFlg.Equals(LMConst.FLG.ON) Then
            '返却パラメータへDS設定
            'Me._S.FormPrm.ParamDataSet = rtnDs
            'Dim prm As LMFormData = New LMFormData()
            Me._Prm.ParamDataSet = rtnDs
            Me._Prm.ReturnFlg = True
            frm.Close()
        End If

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal frm As LMM040F, ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveDestItemData(frm, LMM040C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM040C.EventShubetsu.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMMControlC.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            Call Me._LMMConH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMM040C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me._LMMConH.EndAction(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'フォーカス移動処理
        Call Me._LMMConH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM040F)

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
        Me._FindDs = New LMM040DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm, "LMM040BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._LMMConH.CallWSAAction(frm _
        '                                                , "LMM040BLF" _
        '                                                , "SelectListData" _
        '                                                , Me._FindDs _
        '                                                , Me._LMMConH.GetLimitCount() _
        '                                                , , _
        '                                                  Convert.ToInt32(Convert.ToDouble( _
        '                                                  MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                                  .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._LMMConH.EndAction(frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMM040C.TABLE_NM_OUT).Rows.Count > 0 Then
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
    Private Sub SuccessSelect(ByVal frm As LMM040F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM040C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM040C.TABLE_NM_OUT2)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '取得データ(DEST_DETAILS)をPrivate変数に保持
        Call Me.SetDataSetDestDetailsData(dt2)

        Me._CntSelect = dt.Rows.Count.ToString()

        'メッセージエリアの設定
        'If (LMConst.FLG.OFF).Equals(Me._CntSelect) = True Then
        '    MyBase.ShowMessage(frm, "G001")
        'Else
        '    MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        'End If

        '0件でないとき
        If Me._CntSelect.Equals(LMConst.FLG.OFF) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '初期モード判定フラグの初期化
        frm.lblModeFlg.TextValue = "0"

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal frm As LMM040F, ByVal ds As DataSet)

        'SPREAD(表示行)初期化
        frm.sprDetail.CrearSpread()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 画面の郵便番号に該当する項目のクリア
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub ClearZipJisData(ByVal frm As LMM040F, ByVal ds As DataSet)

        With frm
            '郵便番号が存在チェックエラーの場合、関連する項目をクリア
            .txtAd1.TextValue = String.Empty
            .txtAd2.TextValue = String.Empty
            .txtAd3.TextValue = String.Empty
            .txtJIS.TextValue = String.Empty
        End With

    End Sub

    ''' <summary>
    ''' 画面の郵便番号に該当する項目の設定（郵便番号のみ入力し保存した場合）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetZipData(ByVal frm As LMM040F, ByVal ds As DataSet)

        With frm
            '郵便番号に該当する住所1,2を設定
            .txtAd1.TextValue = ds.Tables(LMM040C.TABLE_NM_OUT).Rows(0)("AD_1").ToString()
            .txtAd2.TextValue = ds.Tables(LMM040C.TABLE_NM_OUT).Rows(0)("AD_2").ToString()
        End With

    End Sub

    'START ADD 2013/09/10 KOBAYASHI WIT対応
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub PrintCommon(ByVal frm As LMM040F _
                          , ByRef prmDs As DataSet _
                          , ByVal list As ArrayList)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim dt As DataTable = Nothing
        Dim dr As DataRow = Nothing

        Dim pgId As String = "LMM520"

        'パラメータ設定
        prm.ReturnFlg = False

        prmDs = New LMM520DS()
        dt = prmDs.Tables("LMM520IN")

        Dim max As Integer = list.Count - 1
        Dim row As Integer = 0

        For idx As Integer = 0 To max
            With frm.sprDetail.ActiveSheet

                '選択行だけRowを設定する。
                dr = dt.NewRow()
                row = Convert.ToInt32(list(idx).ToString)

                dr.Item("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(row, LMM040G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("CUST_CD_L") = Me._LMMConV.GetCellValue(.Cells(row, LMM040G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("DEST_CD") = Me._LMMConV.GetCellValue(.Cells(row, LMM040G.sprDetailDef.DEST_CD.ColNo))

                dt.Rows.Add(dr)

            End With
        Next

        prm.ParamDataSet = prmDs

    End Sub
    'END   ADD 2013/09/10 KOBAYASHI WIT対応

    '要望番号:1217 yamanaka 2012.06.29 Start
#Region "引き継ぎ項目の設定"

    ''' <summary>
    ''' 引き継ぎ項目の設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="prmDs">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetPrmData(ByVal frm As LMM040F, ByVal prmDs As DataSet)
        Dim prmString As String = String.Empty
        Dim dr() As DataRow = Nothing

        '営業所コード
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("NRS_BR_CD").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.cmbNrsBrCd.SelectedValue = prmString
        End If

        '荷主コード
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("CUST_CD_L").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtCustCdL.TextValue = prmString
            'Pop起動処理
            Call Me.ShowPopupControl(frm, frm.txtCustCdL.Name, LMM040C.EventShubetsu.MAIN)
        End If

        '届先コード
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("DEST_CD").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtDestCd.TextValue = prmString
        End If

        '届先名
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("DEST_NM").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtDestNm.TextValue = prmString
        End If

        'EDI届先コード
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("EDI_CD").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtEDICd.TextValue = prmString
        End If

        '郵便番号
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("ZIP").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtZip.TextValue = prmString
        End If

        '住所１
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("AD_1").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtAd1.TextValue = prmString
        End If

        '住所２
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("AD_2").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtAd2.TextValue = prmString
        End If

        '住所３
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("AD_3").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtAd3.TextValue = prmString
        End If

        '指定納品書添付区分
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("SP_NHS_KB").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.cmbSpNhsKb.SelectedValue = prmString
        End If

        '分析表票添付区分
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("COA_YN").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.cmbCoaYn.SelectedValue = prmString
        End If

        '配送時注意事項
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("DELI_ATT").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtDeliAtt.TextValue = prmString
        End If

        '電話番号
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("TEL").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtTel.TextValue = prmString
        End If

        'FAX番号
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("FAX").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtFax.TextValue = prmString
        End If

        'JISコード
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("JIS").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtJIS.TextValue = prmString
        End If

        'ピッキングリスト区分
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("PICK_KB").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.cmbPick.SelectedValue = prmString
        End If

        '備考
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("REMARK").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtRemark.TextValue = prmString
        End If

        '売上先コード
        prmString = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows(0).Item("URIAGE_CD").ToString()
        If String.IsNullOrEmpty(prmString) = False Then
            frm.txtUriageCd.TextValue = prmString
            '売上先名称取得
            '---↓
            'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' AND DEST_CD = '", frm.txtUriageCd.TextValue, "'"))

            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            destMstDr.Item("DEST_CD") = frm.txtUriageCd.TextValue
            destMstDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            dr = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If 0 < dr.Length Then
                frm.lblUriageNm.TextValue = dr(0).Item("DEST_NM").ToString()
            End If
        End If

    End Sub

#End Region
    '要望番号:1217 yamanaka 2012.06.29 End

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMM040F)

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
    Private Sub SetGMessage(ByVal frm As LMM040F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMM040C.EventShubetsu, ByVal frm As LMM040F)

        'ディスプレイモード、レコードステータス保存域
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        'パラメータクラス生成
        'Dim prm As LMFormData = New LMFormData()

        Select Case eventShubetsu

            Case LMM040C.EventShubetsu.INS_T    '行追加

                '処理開始アクション
                _LMMConH.StartAction(frm)

                'START YANAI 要望番号570
                ''届先コード枝番の採番
                'If Me._G.SetDestCdEdaDataSet(Me._Ds, eventShubetsu) = False Then
                '    '処理終了アクション
                '    _LMMConH.EndAction(frm)
                '    Exit Sub
                'End If
                'END YANAI 要望番号570

                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, frm) = False Then
                    '処理終了アクション
                    _LMMConH.EndAction(frm)
                    Exit Sub
                End If

                'START YANAI 要望番号570
                '届先コード枝番の採番
                If Me._G.SetDestCdEdaDataSet(Me._Ds, eventShubetsu) = False Then
                    '処理終了アクション
                    _LMMConH.EndAction(frm)
                    Exit Sub
                End If
                'END YANAI 要望番号570

                '空行をデフォルト届先明細スプレッドに設定
                Call Me._G.AddSetSpread2()

                'メッセージの表示
                ShowMessage(frm, "G003")

                '処理終了アクション
                _LMMConH.EndAction(frm)

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus(LMM040C.EventShubetsu.INS_T)

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMM040C.EventShubetsu.DEL_T    '行削除

                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, frm) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMMConH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM040G.sprDetailDef2.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _LMMConH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelDestDetails(frm.sprDetail2)

                    '届先明細スプレッドの再描画
                    Me._G.ReSetSpread(frm.sprDetail2)

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    '処理終了アクション
                    _LMMConH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus(LMM040C.EventShubetsu.DEL_T)

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If

        End Select

    End Sub

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMMConH.StartAction(frm)

            ''背景色の設定
            'Call Me.SetBackColor(frm)

            Select Case objNm

                Case .txtCustCdL.Name, .txtSalesCd.Name

                    Call Me.SetReturnCustPop(frm, objNm, eventshubetsu)

                Case .txtZip.Name

                    Call Me.SetReturnZipPop(frm, objNm, eventshubetsu)

                Case .txtCustDestCd.Name, .txtUriageCd.Name

                    Call Me.SetReturnDestPop(frm, objNm, eventshubetsu)

                Case .txtJIS.Name

                    Call Me.SetReturnJisPop(frm, objNm, eventshubetsu)

                Case .txtSpUnsoCd.Name, .txtSpUnsoBrCd.Name

                    Call Me.SetReturnUnsoPop(frm, eventshubetsu)

                Case .txtUnchinSeiqtoCd.Name

                    Call Me.SetReturnUnchinSeiToPop(frm, objNm, eventshubetsu)

                Case .txtUnchinTariffCd1.Name, .txtUnchinTariffCd2.Name

                    Call Me.SetReturnUnchinTariffPop(frm, objNm, eventshubetsu)

                Case .txtExtcTariffCd.Name

                    Call Me.SetReturnExtcTariffPop(frm, objNm, eventshubetsu)

                Case .txtYokoTariffCd.Name

                    Call Me.SetReturnYokoTariffPop(frm, objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowCustPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                ctl.TextValue = dr.Item("CUST_CD_L").ToString()

                Select Case objNm
                    Case frm.txtCustCdL.Name
                        .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                    Case frm.txtSalesCd.Name
                        .lblSalesNm.TextValue = dr.Item("CUST_NM_L").ToString()
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            'START YANAI 要望番号1314 出荷編集→届け先新規を押下した場合に表示される荷主POP不要
            If (LMM040C.EventShubetsu.ENTER).Equals(eventshubetsu) = True OrElse _
                (LMM040C.EventShubetsu.MAIN).Equals(eventshubetsu) = True Then
                .Item("CUST_CD_L") = ctl.TextValue
            End If
            'END YANAI 要望番号1314 出荷編集→届け先新規を押下した場合に表示される荷主POP不要
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_M   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 郵便番号Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnZipPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowZipPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then

            'PopUpから取得したデータをコントロールにセット
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ060C.TABLE_NM_OUT).Rows(0)

            '住所1(都道府県+市区町村名)
            Dim add1 As String = String.Concat(dr.Item("KEN_N").ToString(), dr.Item("CITY_N").ToString())

            With frm

                ctl.TextValue = dr.Item("ZIP_NO").ToString()         '郵便番号

                '値が空のところにそれぞれ設定
                If String.IsNullOrEmpty(.txtAd1.TextValue) = True Then
                    .txtAd1.TextValue = add1
                End If
                If String.IsNullOrEmpty(.txtAd2.TextValue) = True Then
                    .txtAd2.TextValue = dr.Item("TOWN_N").ToString    '住所2(町域名)
                End If
                If String.IsNullOrEmpty(.txtJIS.TextValue) = True Then
                    .txtJIS.TextValue = dr.Item("JIS_CD").ToString    'JISコード
                End If

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 郵便番号マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowZipPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ060DS()
        Dim dt As DataTable = ds.Tables(LMZ060C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                .Item("ZIP_NO") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ060", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowDestPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)

            With frm
                ctl.TextValue = dr.Item("DEST_CD").ToString()

                Select Case objNm
                    Case frm.txtUriageCd.Name
                        .lblUriageNm.TextValue = dr.Item("DEST_NM").ToString()
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                .Item("DEST_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.OFF
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' JISPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnJisPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowJisPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)
            ctl.TextValue = dr.Item("JIS_CD").ToString()
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' JISマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowJisPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ070DS()
        Dim dt As DataTable = ds.Tables(LMZ070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                .Item("JIS_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ070", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsoPop(ByVal frm As LMM040F, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowUnsoPopup(frm, eventshubetsu)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
            Dim item As String = String.Empty

            With frm

                .txtSpUnsoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtSpUnsoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblSpUnsoNm.TextValue = Me._LMMConG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowUnsoPopup(ByVal frm As LMM040F, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            Dim spUnsoCd As String = frm.txtSpUnsoCd.TextValue
            Dim spUnsoBrCd As String = frm.txtSpUnsoBrCd.TextValue

            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                .Item("UNSOCO_CD") = spUnsoCd
                .Item("UNSOCO_BR_CD") = spUnsoBrCd
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnchinSeiToPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowUnchinSeiToPopup(frm, ctl, eventshubetsu)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
            Dim item As String = String.Empty

            With frm

                ctl.TextValue = dr.Item("SEIQTO_CD").ToString()
                .lblUnchinSeiqtoNm.TextValue = Me._LMMConG.EditConcatData(dr.Item("SEIQTO_NM").ToString(), dr.Item("SEIQTO_BUSYO_NM").ToString(), Space(1))

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinSeiToPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運賃タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnchinTariffPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowUnchinTariffPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ230C.TABLE_NM_OUT).Rows(0)

            With frm

                ctl.TextValue = dr.Item("UNCHIN_TARIFF_CD").ToString()

                Select Case objNm
                    Case frm.txtUnchinTariffCd1.Name
                        .lblUnchinTariffRem1.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                    Case frm.txtUnchinTariffCd2.Name
                        .lblUnchinTariffRem2.TextValue = dr.Item("UNCHIN_TARIFF_REM").ToString()
                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            Dim CustCdL As String = frm.txtCustCdL.TextValue
            .Item("NRS_BR_CD") = brCd
            '.Item("CUST_CD_L") = CustCdL
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                .Item("UNCHIN_TARIFF_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("STR_DATE") = Me._SysDate
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増運賃タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnExtcTariffPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowExtcTariffPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm
                ctl.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblExtcTariffRem.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 割増運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowExtcTariffPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            Dim CustCdL As String = frm.txtCustCdL.TextValue
            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                '.Item("CUST_CD_L") = CustCdL
                .Item("EXTC_TARIFF_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnYokoTariffPop(ByVal frm As LMM040F, ByVal objNm As String, ByVal eventshubetsu As LMM040C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._LMMConH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowYokoTariffPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ100C.TABLE_NM_OUT).Rows(0)

            With frm
                ctl.TextValue = dr.Item("YOKO_TARIFF_CD").ToString()
                .lblYokoTariffRem.TextValue = dr.Item("YOKO_REM").ToString()
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMM040F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM040C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            Dim CustCdL As String = frm.txtCustCdL.TextValue
            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If eventshubetsu = LMM040C.EventShubetsu.ENTER Then
                '.Item("CUST_CD_L") = CustCdL
                .Item("YOKO_TARIFF_CD") = ctl.TextValue
            End If
            'END SHINOHARA 要望番号513	
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.OFF

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMMConH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMM040F)

        Dim dr As DataRow = Me._FindDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).NewRow()

        With frm.sprDetail.ActiveSheet

            dr("SYS_DEL_FLG") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr("NRS_BR_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.NRS_BR_NM.ColNo))
            dr("CUST_CD_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.CUST_CD_L.ColNo))
            dr("CUST_NM_L") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.CUST_NM_L.ColNo))
            dr("DEST_CD") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.DEST_CD.ColNo))
            dr("DEST_NM") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.DEST_NM.ColNo))
            dr("AD_1") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.AD.ColNo))
            dr("TEL") = Me._LMMConV.GetCellValue(.Cells(0, LMM040G.sprDetailDef.TEL.ColNo))

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            Me._FindDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDestItemData(ByVal frm As LMM040F, ByVal ds As DataSet)

        With frm

            '編集部の値（届先情報）をデータセットに設定
            Dim dr As DataRow = ds.Tables(LMControlC.LMM040C_TABLE_NM_IN).NewRow()

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim
            dr("DEST_CD") = .txtDestCd.TextValue.Trim
            dr("EDI_CD") = .txtEDICd.TextValue.Trim
            dr("DEST_NM") = .txtDestNm.TextValue.Trim
            '要望番号:1330 terakawa 2012.08.09 Start
            dr("KANA_NM") = .txtKanaNm.TextValue.Trim
            '要望番号:1330 terakawa 2012.08.09 End
            dr("ZIP") = .txtZip.TextValue.Trim
            dr("AD_1") = .txtAd1.TextValue.Trim
            dr("AD_2") = .txtAd2.TextValue.Trim
            dr("AD_3") = .txtAd3.TextValue.Trim
            dr("CUST_DEST_CD") = .txtCustDestCd.TextValue.Trim
            dr("SALES_CD") = .txtSalesCd.TextValue.Trim
            dr("SP_NHS_KB") = .cmbSpNhsKb.SelectedValue
            dr("COA_YN") = .cmbCoaYn.SelectedValue
            dr("SP_UNSO_CD") = .txtSpUnsoCd.TextValue.Trim
            dr("SP_UNSO_BR_CD") = .txtSpUnsoBrCd.TextValue.Trim
            dr("DELI_ATT") = .txtDeliAtt.TextValue.Trim
            'START YANAI 要望番号881
            dr("REMARK") = .txtRemark.TextValue.Trim
            'END YANAI 要望番号881
            '要望番号:1424② yamanaka 2012.09.20 Start
            dr("SHIHARAI_AD") = .txtShiharaiAd.TextValue.Trim
            '要望番号:1424② yamanaka 2012.09.20 End
            dr("CARGO_TIME_LIMIT") = .txtCargoTimeLimit.TextValue.Trim
            dr("LARGE_CAR_YN") = .cmbLargeCarYn.SelectedValue
            dr("TEL") = .txtTel.TextValue.Trim
            dr("FAX") = .txtFax.TextValue.Trim
            dr("UNCHIN_SEIQTO_CD") = .txtUnchinSeiqtoCd.TextValue.Trim
            dr("JIS") = .txtJIS.TextValue.Trim
            dr("KYORI") = .numKyori.TextValue.Trim
            dr("PICK_KB") = .cmbPick.SelectedValue
            dr("BIN_KB") = .cmbBin.SelectedValue
            dr("MOTO_CHAKU_KB") = .cmbMotoChakuKbn.SelectedValue
            dr("URIAGE_CD") = .txtUriageCd.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim
            'ﾚｺｰﾄﾞｽﾃｰﾀｽが"新規" or "複写"の場合、荷主コード(中)="00"/セット区分="01"固定
            If frm.lblSituation.RecordStatus.Equals(RecordStatus.NEW_REC) OrElse _
             frm.lblSituation.RecordStatus.Equals(RecordStatus.COPY_REC) Then
                dr("CUST_CD_M") = LMMControlC.FLG_OFF
                dr("SET_KB") = LMMControlC.FLG_ON
            Else
                dr("CUST_CD_M") = .lblCustCdM.TextValue.Trim
                dr("SET_KB") = .lblSetKbn.TextValue.Trim
            End If
            'ﾚｺｰﾄﾞｽﾃｰﾀｽが"正常"で、セットマスタコードが空の場合、運賃タリフセットマスタコード="0000"固定
            If frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
                If String.IsNullOrEmpty(.lblSetMstCd.TextValue.Trim) = True Then
                    '(2012.06.22)要望番号1178 セットマスタコードを3桁⇒4桁
                    'dr("SET_MST_CD") = "000"
                    dr("SET_MST_CD") = "0000"
                Else
                    dr("SET_MST_CD") = .lblSetMstCd.TextValue.Trim
                End If

            End If
            dr("TARIFF_BUNRUI_KB") = .cmbTariffBunruiKbn.SelectedValue
            dr("UNCHIN_TARIFF_CD1") = .txtUnchinTariffCd1.TextValue.Trim
            dr("UNCHIN_TARIFF_CD2") = .txtUnchinTariffCd2.TextValue.Trim
            dr("EXTC_TARIFF_CD") = .txtExtcTariffCd.TextValue.Trim
            dr("YOKO_TARIFF_CD") = .txtYokoTariffCd.TextValue.Trim
            dr("SYS_UPD_DATE_T") = .lblSysUpdDateT.TextValue.Trim
            dr("SYS_UPD_TIME_T") = .lblSysUpdTimeT.TextValue.Trim
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            ds.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows.Add(dr)

            '届先明細Spread情報をデータセットに設定
            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr2 As DataRow = ds.Tables(LMM040C.TABLE_NM_OUT2).NewRow()

                dr2("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr2("CUST_CD_L") = .txtCustCdL.TextValue.Trim
                dr2("DEST_CD") = .txtDestCd.TextValue.Trim
                dr2("DEST_CD_EDA") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.DEST_CD_EDA.ColNo))
                dr2("SUB_KB") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.SUB_KB.ColNo))
                dr2("SET_NAIYO") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.SET_NAIYO.ColNo))
                dr2("REMARK") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.REMARK.ColNo))
                dr2("UPD_FLG") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.UPD_FLG.ColNo))
                dr2("SYS_DEL_FLG") = _LMMConG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.SYS_DEL_FLG_T.ColNo))
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr2("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                dr2("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM040C.TABLE_NM_OUT2).Rows.Add(dr2)

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(削除・復活用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal frm As LMM040F, ByVal ds As DataSet)
        Dim dr As DataRow = ds.Tables(LMControlC.LMM040C_TABLE_NM_IN).NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue.Trim
            dr("CUST_CD_M") = .lblCustCdM.TextValue.Trim
            dr("DEST_CD") = .txtDestCd.TextValue.Trim
            dr("SET_KB") = .lblSetMstCd.TextValue.Trim
            dr("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

            Dim delflg As String = String.Empty
            If .lblSituation.RecordStatus.Equals(LMConst.FLG.OFF) = True Then
                delflg = LMConst.FLG.ON
            Else
                delflg = LMConst.FLG.OFF
            End If

            dr("SYS_DEL_FLG") = delflg

        End With

        ds.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(届先明細情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetDestDetailsData(ByVal dt As DataTable)

        Me._DestDetailsDs = dt

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.NewDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    Friend Sub FunctionKey5Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ImportEvent")

        '一括登録
        Me.ImportEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ImportEvent")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "PrintDataEvent")

        '印刷処理
        Me.PrintDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "PrintDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        Me.MasterShowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveDestItemData")

        Me.SaveDestItemData(frm, LMM040C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveDestItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM040F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_TCUST_Click(ByRef frm As LMM040F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

        '「行追加」処理
        Me.ActionControl(LMM040C.EventShubetsu.INS_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_TCUST_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_TCUST_Click(ByRef frm As LMM040F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

        '「行削除」処理
        Me.ActionControl(LMM040C.EventShubetsu.DEL_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_TCUST_Click")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM040F_KeyDown(ByVal frm As LMM040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM040F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM040F_KeyDown")

    End Sub
    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM040F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class