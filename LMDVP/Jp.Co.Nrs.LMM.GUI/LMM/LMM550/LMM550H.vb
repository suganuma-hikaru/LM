' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM550H : 下払いタリフマスタメンテ
'  作  成  者       :  matsumoto
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Microsoft.Office.Interop
Imports System.IO
Imports AdvanceSoftware.ExcelCreator        'ExcelCreator(本体)
Imports AdvanceSoftware.ExcelCreator.Xlsx   'ExcelCreator(2007以降)

''' <summary>
''' LMM550ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM550H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM550F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM550V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM550G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMMControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

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
    '''運賃タリフ情報(距離刻み/運賃)格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _UnchinDs As DataTable

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
    ''' カウントの保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _BeCnt As Integer = 0

    ''' <summary>
    '''EXCEL取込用
    ''' </summary>
    ''' <remarks></remarks>
    Private DataSetString As String()
    Private DataTableString As String()

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
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Me._Frm = New LMM550F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM550", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM550G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMM550V(Me, Me._Frm, Me._ControlV)

        'サーバ日付の設定
        Me._SysDate = MyBase.GetSystemDateTime(0)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub NewDataEvent(ByVal frm As LMM550F)

        '保存用データセットインスタンス作成
        Me._Ds = New LMM550DS()

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '利用スプレッドの初期表示はTypeAにする
        Me.SetSpreadType(frm, LMM550C.TABLE_TP_KBN_00)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMM550F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk() = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Me._Ds = New LMM550DS()

        Dim NrsBrcd As String = frm.cmbNrsBrCd.SelectedValue.ToString
        Dim TariffCd As String = frm.txtShiharaiTariffCd.TextValue
        Dim sort As String = String.Empty

        Select Case frm.cmbTableTp.SelectedValue.ToString()

            Case LMM550C.TABLE_TP_KBN_00, LMM550C.TABLE_TP_KBN_02, LMM550C.TABLE_TP_KBN_03, _
                 LMM550C.TABLE_TP_KBN_04, LMM550C.TABLE_TP_KBN_05, LMM550C.TABLE_TP_KBN_07

                sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,WT_LV ASC,CAR_TP ASC,SHIHARAI_TARIFF_CD_EDA ASC"

            Case LMM550C.TABLE_TP_KBN_01, LMM550C.TABLE_TP_KBN_06

                sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,CAR_TP ASC,WT_LV ASC,SHIHARAI_TARIFF_CD_EDA ASC"

            Case LMM550C.TABLE_TP_KBN_08, LMM550C.TABLE_TP_KBN_09

                sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,ORIG_JIS_CD ASC,DEST_JIS_CD ASC,SHIHARAI_TARIFF_CD_EDA ASC"

        End Select

        Dim drs As DataRow() = Me._UnchinDs.Select(String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                                                 , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                                                 ), sort)
        Dim cnt As Integer = drs.Length - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To cnt
            dr = drs(i)
            Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr)
        Next

        Call SetDataSetHaitaChk()

        '==========================
        'WSAクラス呼出()
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM550BLF", "HaitaData", Me._Ds)

        'データセットの内容保持
        _Ds = rtnds

        'MAX支払タリフコードを隠し項目に保持
        Dim max As Integer = _Ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).Rows.Count
        If (0).Equals(max) = False Then
            frm.lblMaxEda.TextValue = _Ds.Tables(LMM550C.TABLE_NM_SHIHARAI_TARRIF_MAXEDA).Rows(0).Item("SHIHARAI_TARIFF_MAXEDA").ToString()
        End If

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then

            'メッセージエリアの設定
            MyBase.ShowMessage(frm)

            '終了処理
            Call Me._ControlH.EndAction(frm)

            '画面全ロックの解除
            MyBase.UnLockedControls(frm)

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            '終了処理
            Call Me._ControlH.EndAction(frm)

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            '画面の入力項目/ファンクションキーの制御
            Call Me._G.UnLockedForm()

            '運賃タリフスプレッドのロック制御
            Call Me.SetLockControl(LMM550C.EventShubetsu.HENSHU)

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 複写処理 
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CopyDataEvent(ByVal frm As LMM550F)

        '保存用データセットインスタンス作成
        Me._Ds = New LMM550DS()

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.HUKUSHA) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '複写ボタン押下時チェック
        If Me._V.IsFukushaChk() = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.COPY_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '現在の列数のクリア
        _BeCnt = 0

        '運賃タリフSpreadのロック制御
        Call Me.SetLockControl(LMM550C.EventShubetsu.HUKUSHA)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM550F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.SAKUJO) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '削除/復活ボタン押下時チェック
        If Me._V.IsSakujoHukkatuChk() = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '処理続行メッセージ表示
        Dim msg As String = String.Empty
        Select Case Me._Frm.lblSituation.RecordStatus
            Case RecordStatus.DELETE_REC
                msg = "復活"
            Case RecordStatus.NOMAL_REC
                msg = "削除"
        End Select
        If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM550DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM550BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SHIHARAI_TARIFF)

        'メッセージ用コード格納
        Dim UnchinCd As String = frm.txtShiharaiTariffCd.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty) _
                                              , String.Concat("[", frm.lblShiharaiTariffCd.Text, " = ", UnchinCd, "]")})

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' Excel取込処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub InputExcelEvent(ByVal frm As LMM550F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.EXCELINPUT) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理　
            Exit Sub
        End If

        Dim lock As Boolean = True
        Dim unLock As Boolean = False

        Dim ofd As OpenFileDialog = New OpenFileDialog()

        ofd.FileName = LMM550C.DEF_FILENM
        ofd.InitialDirectory = LMM550C.DEF_DRIVE
        ofd.Filter = LMM550C.FILETYPE
        ofd.FilterIndex = LMM550C.ALL_FILE
        ofd.Title = LMM550C.DLGTITLE
        ofd.RestoreDirectory = True

        ofd.CheckFileExists = True
        ofd.CheckPathExists = True

        'ダイアログを表示する
        If ofd.ShowDialog() = DialogResult.OK Then
            Console.WriteLine(ofd.FileName)

            'DataSet設定
            Me._Ds = New LMM550DS()

            '取込んだExcelからDataSetへ設定
            If Me.SetExcelFromDatasetTo(Me._Ds, ofd.FileName, frm) = False Then
                Call Me._ControlH.EndAction(frm) '終了処理
                Exit Sub
            End If

            '取込データのチェック
            If Me.SetExcelData(Me._Frm, Me._Ds, LMM550C.EventShubetsu.EXCELINPUT) = 0 Then

                'PG固定値を設定
                Me._Ds = ExcelDataPGSet(Me._Ds)

                '現在の列数のクリア
                _BeCnt = 0

                '取込データを画面に表示
                Call Me._G.SetExcelUnchinData(Me._Ds, LMM550C.EventShubetsu.EXCELINPUT)

                'モード・ステータスの設定
                Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

                'ファンクションキーの制御
                Call Me._G.SetFunctionKey()

                '編集部の項目のロック解除
                Call Me._ControlG.SetLockControl(Me._Frm, unLock)

                '常にロック項目ロック制御
                Call Me._ControlG.LockComb(Me._Frm.cmbNrsBrCd, lock)

                '運賃タリフスプレッドのロック制御
                Call Me.SetLockControl(LMM550C.EventShubetsu.EXCELINPUT)

                'メッセージエリアの設定
                Call Me._V.SetBaseMsg()

            Else
                'メッセージ蓄積の有無を判定
                If MyBase.IsMessageStoreExist() = True Then
                    'EXCEL起動 
                    MyBase.MessageStoreDownload(True)
                    MyBase.ShowMessage(frm, "E235")
                End If

                'モード・ステータスの設定
                Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

            End If

            '終了処理
            Call Me._ControlH.EndAction(Me._Frm)

            'フォーカスの設定
            Call Me._G.SetFoucus()

        Else
            'キャンセルを押されたときなど選択されなかったとき押下前の状態に戻す
            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

            'メッセージエリアの設定
            Call Me._V.SetBaseMsg()

            'フォーカスの設定
            Call Me._G.SetFoucus()

        End If

    End Sub

    ''' <summary>
    ''' Excelからデータセットへ設定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>    
    Private Function SetExcelFromDatasetTo(ByVal ds As DataSet, ByVal filename As String, ByVal frm As LMM550F) As Boolean

        Dim inTbl As DataTable = ds.Tables(LMM550C.TABLE_NM_KYORI)
        Dim inRow As DataRow
        Dim brCd As String = Base.LMUserInfoManager.GetNrsBrCd

        Try

            '---------- ExcelCreatorでExcelファイルを読み込み開始 -------
            ' ExcelCreator インスタンス生成
            Dim components As System.ComponentModel.Container = New System.ComponentModel.Container()
            Dim excelCreator As AdvanceSoftware.ExcelCreator.Xlsx.XlsxCreator = New XlsxCreator(components)

            ' Excel ファイル (インポートファイル) を読み取り専用でオープンします。
            excelCreator.ReadBook(filename)

            ' 該当のシートをアクティブ化
            excelCreator.ActiveSheet = 1

            ' データが設定されたセルの最大行と最大列の交点座標を取得します。
            Dim maxData As System.Drawing.Point = excelCreator.GetMaxData(AdvanceSoftware.ExcelCreator.MaxEndPoint.MaxPoint)

            'Excelデータの読み込み（データ設定された行を全て読み込む）
            For row As Integer = 2 To maxData.Y + 1
                If excelCreator.Cell("A" & row).Value Is Nothing _
                    OrElse IsDBNull(excelCreator.Cell("A" & row).Value) = True _
                    OrElse excelCreator.Cell("A" & row).Value.ToString.Length = 0 Then
                    Exit For
                End If
                ' データテーブルにExcelのデータを設定
                inRow = inTbl.NewRow()
                inRow.Item("NRS_BR_CD") = CStr(excelCreator.Cell("A" & row).Value)
                inRow.Item("SHIHARAI_TARIFF_CD") = CStr(excelCreator.Cell("B" & row).Value)
                inRow.Item("SHIHARAI_TARIFF_CD_EDA") = CStr(excelCreator.Cell("C" & row).Value)
                inRow.Item("STR_DATE") = CStr(excelCreator.Cell("D" & row).Value)
                inRow.Item("SHIHARAI_TARIFF_REM") = CStr(excelCreator.Cell("E" & row).Value)
                inRow.Item("DATA_TP") = CStr(excelCreator.Cell("F" & row).Value)
                inRow.Item("TABLE_TP") = CStr(excelCreator.Cell("G" & row).Value)
                inRow.Item("CAR_TP") = CStr(excelCreator.Cell("H" & row).Value)
                inRow.Item("CAR_TP_S_NM") = CStr(excelCreator.Cell("I" & row).Value)
                inRow.Item("CAR_TP_T_NM") = CStr(excelCreator.Cell("J" & row).Value)
                inRow.Item("WT_LV") = CStr(excelCreator.Cell("K" & row).Value)
                inRow.Item("KYORI_1") = CStr(excelCreator.Cell("L" & row).Value)
                inRow.Item("KYORI_2") = CStr(excelCreator.Cell("M" & row).Value)
                inRow.Item("KYORI_3") = CStr(excelCreator.Cell("N" & row).Value)
                inRow.Item("KYORI_4") = CStr(excelCreator.Cell("O" & row).Value)
                inRow.Item("KYORI_5") = CStr(excelCreator.Cell("P" & row).Value)
                inRow.Item("KYORI_6") = CStr(excelCreator.Cell("Q" & row).Value)
                inRow.Item("KYORI_7") = CStr(excelCreator.Cell("R" & row).Value)
                inRow.Item("KYORI_8") = CStr(excelCreator.Cell("S" & row).Value)
                inRow.Item("KYORI_9") = CStr(excelCreator.Cell("T" & row).Value)
                inRow.Item("KYORI_10") = CStr(excelCreator.Cell("U" & row).Value)
                inRow.Item("KYORI_11") = CStr(excelCreator.Cell("V" & row).Value)
                inRow.Item("KYORI_12") = CStr(excelCreator.Cell("W" & row).Value)
                inRow.Item("KYORI_13") = CStr(excelCreator.Cell("X" & row).Value)
                inRow.Item("KYORI_14") = CStr(excelCreator.Cell("Y" & row).Value)
                inRow.Item("KYORI_15") = CStr(excelCreator.Cell("Z" & row).Value)
                inRow.Item("KYORI_16") = CStr(excelCreator.Cell("AA" & row).Value)
                inRow.Item("KYORI_17") = CStr(excelCreator.Cell("AB" & row).Value)
                inRow.Item("KYORI_18") = CStr(excelCreator.Cell("AC" & row).Value)
                inRow.Item("KYORI_19") = CStr(excelCreator.Cell("AD" & row).Value)
                inRow.Item("KYORI_20") = CStr(excelCreator.Cell("AE" & row).Value)
                inRow.Item("KYORI_21") = CStr(excelCreator.Cell("AF" & row).Value)
                inRow.Item("KYORI_22") = CStr(excelCreator.Cell("AG" & row).Value)
                inRow.Item("KYORI_23") = CStr(excelCreator.Cell("AH" & row).Value)
                inRow.Item("KYORI_24") = CStr(excelCreator.Cell("AI" & row).Value)
                inRow.Item("KYORI_25") = CStr(excelCreator.Cell("AJ" & row).Value)
                inRow.Item("KYORI_26") = CStr(excelCreator.Cell("AK" & row).Value)
                inRow.Item("KYORI_27") = CStr(excelCreator.Cell("AL" & row).Value)
                inRow.Item("KYORI_28") = CStr(excelCreator.Cell("AM" & row).Value)
                inRow.Item("KYORI_29") = CStr(excelCreator.Cell("AN" & row).Value)
                inRow.Item("KYORI_30") = CStr(excelCreator.Cell("AO" & row).Value)
                inRow.Item("KYORI_31") = CStr(excelCreator.Cell("AP" & row).Value)
                inRow.Item("KYORI_32") = CStr(excelCreator.Cell("AQ" & row).Value)
                inRow.Item("KYORI_33") = CStr(excelCreator.Cell("AR" & row).Value)
                inRow.Item("KYORI_34") = CStr(excelCreator.Cell("AS" & row).Value)
                inRow.Item("KYORI_35") = CStr(excelCreator.Cell("AT" & row).Value)
                inRow.Item("KYORI_36") = CStr(excelCreator.Cell("AU" & row).Value)
                inRow.Item("KYORI_37") = CStr(excelCreator.Cell("AV" & row).Value)
                inRow.Item("KYORI_38") = CStr(excelCreator.Cell("AW" & row).Value)
                inRow.Item("KYORI_39") = CStr(excelCreator.Cell("AX" & row).Value)
                inRow.Item("KYORI_40") = CStr(excelCreator.Cell("AY" & row).Value)
                inRow.Item("KYORI_41") = CStr(excelCreator.Cell("AZ" & row).Value)
                inRow.Item("KYORI_42") = CStr(excelCreator.Cell("BA" & row).Value)
                inRow.Item("KYORI_43") = CStr(excelCreator.Cell("BB" & row).Value)
                inRow.Item("KYORI_44") = CStr(excelCreator.Cell("BC" & row).Value)
                inRow.Item("KYORI_45") = CStr(excelCreator.Cell("BD" & row).Value)
                inRow.Item("KYORI_46") = CStr(excelCreator.Cell("BE" & row).Value)
                inRow.Item("KYORI_47") = CStr(excelCreator.Cell("BF" & row).Value)
                inRow.Item("KYORI_48") = CStr(excelCreator.Cell("BG" & row).Value)
                inRow.Item("KYORI_49") = CStr(excelCreator.Cell("BH" & row).Value)
                inRow.Item("KYORI_50") = CStr(excelCreator.Cell("BI" & row).Value)
                inRow.Item("KYORI_51") = CStr(excelCreator.Cell("BJ" & row).Value)
                inRow.Item("KYORI_52") = CStr(excelCreator.Cell("BK" & row).Value)
                inRow.Item("KYORI_53") = CStr(excelCreator.Cell("BL" & row).Value)
                inRow.Item("KYORI_54") = CStr(excelCreator.Cell("BM" & row).Value)
                inRow.Item("KYORI_55") = CStr(excelCreator.Cell("BN" & row).Value)
                inRow.Item("KYORI_56") = CStr(excelCreator.Cell("BO" & row).Value)
                inRow.Item("KYORI_57") = CStr(excelCreator.Cell("BP" & row).Value)
                inRow.Item("KYORI_58") = CStr(excelCreator.Cell("BQ" & row).Value)
                inRow.Item("KYORI_59") = CStr(excelCreator.Cell("BR" & row).Value)
                inRow.Item("KYORI_60") = CStr(excelCreator.Cell("BS" & row).Value)
                inRow.Item("KYORI_61") = CStr(excelCreator.Cell("BT" & row).Value)
                inRow.Item("KYORI_62") = CStr(excelCreator.Cell("BU" & row).Value)
                inRow.Item("KYORI_63") = CStr(excelCreator.Cell("BV" & row).Value)
                inRow.Item("KYORI_64") = CStr(excelCreator.Cell("BW" & row).Value)
                inRow.Item("KYORI_65") = CStr(excelCreator.Cell("BX" & row).Value)
                inRow.Item("KYORI_66") = CStr(excelCreator.Cell("BY" & row).Value)
                inRow.Item("KYORI_67") = CStr(excelCreator.Cell("BZ" & row).Value)
                inRow.Item("KYORI_68") = CStr(excelCreator.Cell("CA" & row).Value)
                inRow.Item("KYORI_69") = CStr(excelCreator.Cell("CB" & row).Value)
                inRow.Item("KYORI_70") = CStr(excelCreator.Cell("CC" & row).Value)
                inRow.Item("CITY_EXTC_A") = CStr(excelCreator.Cell("CD" & row).Value)
                inRow.Item("CITY_EXTC_B") = CStr(excelCreator.Cell("CE" & row).Value)
                inRow.Item("WINT_EXTC_A") = CStr(excelCreator.Cell("CF" & row).Value)
                inRow.Item("WINT_EXTC_B") = CStr(excelCreator.Cell("CG" & row).Value)
                inRow.Item("RELY_EXTC") = CStr(excelCreator.Cell("CH" & row).Value)
                inRow.Item("INSU") = CStr(excelCreator.Cell("CI" & row).Value)
                'inRow.Item("FRRY_EXTC_PART") = CStr(excelCreator.Cell("CJ" & row).Value)
                inRow.Item("SHIHARAI_TARIFF_CD2") = CStr(excelCreator.Cell("CJ" & row).Value)
                inRow.Item("SHIHARAI_TARIFF_REM2") = CStr(excelCreator.Cell("CK" & row).Value)
                inRow.Item("SYS_ENT_DATE") = CStr(excelCreator.Cell("CL" & row).Value)
                inRow.Item("SYS_ENT_TIME") = CStr(excelCreator.Cell("CM" & row).Value)
                inRow.Item("SYS_ENT_PGID") = CStr(excelCreator.Cell("CN" & row).Value)
                inRow.Item("SYS_ENT_USER") = CStr(excelCreator.Cell("CO" & row).Value)
                inRow.Item("SYS_UPD_DATE") = CStr(excelCreator.Cell("CP" & row).Value)
                inRow.Item("SYS_UPD_TIME") = CStr(excelCreator.Cell("CQ" & row).Value)
                inRow.Item("SYS_UPD_PGID") = CStr(excelCreator.Cell("CR" & row).Value)
                inRow.Item("SYS_UPD_USER") = CStr(excelCreator.Cell("CS" & row).Value)
                inRow.Item("SYS_DEL_FLG") = CStr(excelCreator.Cell("CT" & row).Value)
                'inRow.Item("UPD_FLG") = CStr(excelCreator.Cell("CV" & row).Value)
                'inRow.Item("RECORD_NO") = CStr(excelCreator.Cell("CW" & row).Value)
                'inRow.Item("USER_BR_CD") = CStr(excelCreator.Cell("CX" & row).Value)
                inTbl.Rows.Add(inRow)
            Next
            ' ブックをクリアします。
            excelCreator.CloseBook(True)
            '---------- ExcelCreatorでExcelファイルを読み込み終了 -------

            Return True

        Catch ex As Exception
            MyBase.ShowMessage(frm, "S001", New String() {"エクセルファイルの読み込み"})
            Return False
        End Try

        Return True

    End Function

    ''' <summary>
    ''' Excel取込データチェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <param name="eventShubetsu">イベント</param>
    ''' <remarks></remarks>
    Private Function SetExcelData(ByVal frm As LMM550F, ByVal ds As DataSet, ByVal eventShubetsu As LMM550C.EventShubetsu) As Integer

        '処理開始アクション
        Call Me._ControlH.SetEnterEvent(frm)

        Dim dt As DataTable = ds.Tables(LMM550C.TABLE_NM_KYORI)
        Dim dr As DataRow = Nothing
        Dim rowMax As Integer = dt.Rows.Count - 1
        Dim colMax As Integer = dt.Columns.Count - 1
        Dim InData(rowMax + 1, colMax + 1) As String
        Dim rowNo As Integer = 0
        Dim ErrCnt As Integer = 0
        Dim InCnt As Integer = 0
        Dim rtnResult As Boolean = False
        Dim rtnDataToCnt As Integer = 0
        Dim ChkEndFlg1 As String = LMConst.FLG.OFF
        Dim ChkEndFlg2 As String = LMConst.FLG.OFF
        Dim ChkEndFlg3 As String = LMConst.FLG.OFF        

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '対象行存在チェック(ファイルにデータが１行もない場合エラー)
        For i As Integer = 0 To rowMax
            For j As Integer = 0 To colMax
                InData(i, j) = ds.Tables(LMM550C.TABLE_NM_KYORI).Rows(i).Item(j).ToString
                If String.IsNullOrEmpty(InData(i, j)) = False Then
                    InCnt = InCnt + 1
                End If
            Next
        Next
        If InCnt < 1 Then
            rtnResult = Me.SetExcelInputErrData("E079", New String() {"取込ファイル", "データ"}, rowNo)
            ErrCnt = ErrCnt + 1
            Return ErrCnt
            Exit Function
        End If
        If (rowMax + 1) > 1000 Then
            rtnResult = Me.SetExcelInputErrData("E935", New String() {"1000"}, rowNo)
            ErrCnt = ErrCnt + 1
            Return ErrCnt
            Exit Function
        End If

        With Me._Frm

            For i As Integer = 0 To rowMax

                dr = dt.Rows(i)

                'データセットの行番号を設定
                rowNo = Convert.ToInt32(i + 1)

                '距離刻み存在チェック
                If LMConst.FLG.OFF.Equals(ChkEndFlg1) = True Then
                    rtnDataToCnt = Me.ChkExistDataTp(frm, dr, rowNo)
                    If rtnDataToCnt > 0 Then
                        ChkEndFlg1 = LMConst.FLG.ON   'このチェックは終了
                    End If
                End If

                '-----単項目チェック
                '【営業所】
                '必須チェック
                If Me.ChkHissu(frm, dr("NRS_BR_CD").ToString, rowNo, .lblNrsBrCd.Text) = False Then
                    ErrCnt = ErrCnt + 1
                End If
                '存在チェック
                If Me._V.IsExcelExistCheck(frm, dr, .lblNrsBrCd.Text) = False Then
                    rtnResult = Me.SetExcelInputErrData("E079", New String() {frm.lblNrsBrCd.Text & "マスタ", .lblNrsBrCd.Text & "コード"}, rowNo)
                    ErrCnt = ErrCnt + 1
                End If

                '【タリフコード】
                'バイト数チェック
                If Me.ChkByte(frm, dr("SHIHARAI_TARIFF_CD").ToString, rowNo, .lblShiharaiTariffCd.Text, 10) = False Then
                    ErrCnt = ErrCnt + 1
                End If

                '【適用開始日】
                'バイト数チェック
                If Me.ChkByte(frm, dr("STR_DATE").ToString, rowNo, .lblStrDate.Text, 8) = False Then
                    ErrCnt = ErrCnt + 1
                End If

                '【備考】
                'バイト数チェック
                If Me.ChkByte(frm, dr("SHIHARAI_TARIFF_REM").ToString, rowNo, .lblShiharaiTariffRem.Text, 50) = False Then
                    ErrCnt = ErrCnt + 1
                End If

                '【２次タリフコード】
                'バイト数チェック
                If Me.ChkByte(frm, dr("SHIHARAI_TARIFF_CD2").ToString, rowNo, .lblShiharaiTariffCd2.Text, 10) = False Then
                    ErrCnt = ErrCnt + 1
                End If

                '【データタイプ】
                '必須チェック
                If Me.ChkHissu(frm, dr("DATA_TP").ToString, rowNo, .lblDataTp.Text) = False Then
                    ErrCnt = ErrCnt + 1
                End If
                '禁止文字チェック
                If Me._V.IsExcelForbiddenWordsCheck(frm, dr, frm.lblDataTp.Text) = False Then
                    rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(frm.lblDataTp.Text, "に禁止文字が含まれているデータ")}, rowNo)
                    ErrCnt = ErrCnt + 1
                Else
                    '存在チェック(禁止文字チェックがエラーでなければチェックする)
                    If Me._V.IsExcelExistCheck(frm, dr, .lblDataTp.Text) = False Then
                        rtnResult = Me.SetExcelInputErrData("E079", New String() {"区分マスタ", .lblDataTp.Text}, rowNo)
                        ErrCnt = ErrCnt + 1
                    End If
                End If

                '【テーブルタイプ】
                '必須チェック
                If Me.ChkHissu(frm, dr("TABLE_TP").ToString, rowNo, .lblTableTp.Text) = False Then
                    ErrCnt = ErrCnt + 1
                End If
                '禁止文字チェック
                If Me._V.IsExcelForbiddenWordsCheck(frm, dr, .lblTableTp.Text) = False Then
                    rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(.lblTableTp.Text, "に禁止文字が含まれているデータ")}, rowNo)
                    ErrCnt = ErrCnt + 1
                Else
                    '存在チェック(禁止文字チェックがエラーでなければチェックする)
                    If Me._V.IsExcelExistCheck(frm, dr, .lblTableTp.Text) = False Then
                        rtnResult = Me.SetExcelInputErrData("E079", New String() {"区分マスタ", .lblTableTp.Text}, rowNo)
                        ErrCnt = ErrCnt + 1
                    End If
                End If

                '【車種】
                '必須チェック
                If Me.ChkHissu(frm, dr("CAR_TP").ToString, rowNo, LMM550C.CAR_TP) = False Then
                    ErrCnt = ErrCnt + 1
                End If
                '禁止文字チェック
                If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.CAR_TP) = False Then
                    rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.CAR_TP, "に禁止文字が含まれているデータ")}, rowNo)
                    ErrCnt = ErrCnt + 1
                Else
                    '存在チェック(禁止文字チェックがエラーでなければチェックする)
                    If Me._V.IsExcelExistCheck(frm, dr, LMM550C.CAR_TP) = False Then
                        rtnResult = Me.SetExcelInputErrData("E079", New String() {"区分マスタ", LMM550C.CAR_TP}, rowNo)
                        ErrCnt = ErrCnt + 1
                    End If
                End If

                '【重量】
                If String.IsNullOrEmpty(dr("WT_LV").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.WT_LV) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.WT_LV, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("WT_LV").ToString, rowNo, LMM550C.WT_LV, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("WT_LV")), rowNo, LMM550C.WT_LV, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '【距離1～70】
                For t As Integer = 1 To 70
                    If String.IsNullOrEmpty(dr("KYORI_" & t).ToString) = False Then '取込データがNULLならチェックなし
                        '禁止文字チェック
                        If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.KYORI, t) = False Then
                            rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat((LMM550C.KYORI & t), "に禁止文字が含まれているデータ")}, rowNo)
                            ErrCnt = ErrCnt + 1
                        Else
                            'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                            If Me.ChkByte(frm, dr("KYORI_" & t).ToString.Replace("."c, ""), rowNo, (LMM550C.KYORI & t), 12) = False Then
                                ErrCnt = ErrCnt + 1
                            End If
                            '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                            If Me.ChkRange(frm, Convert.ToDecimal(dr("KYORI_" & t)), rowNo, (LMM550C.KYORI & t), Convert.ToDecimal(0.0), Convert.ToDecimal(999999999.999)) = False Then
                                ErrCnt = ErrCnt + 1
                            End If
                        End If
                    End If
                Next

                '【都市割増Ａ】
                If String.IsNullOrEmpty(dr("CITY_EXTC_A").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.CITY_EXTC_A) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.CITY_EXTC_A, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("CITY_EXTC_A").ToString, rowNo, LMM550C.CITY_EXTC_A, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("CITY_EXTC_A")), rowNo, LMM550C.CITY_EXTC_A, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '【都市割増Ｂ】
                If String.IsNullOrEmpty(dr("CITY_EXTC_B").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.CITY_EXTC_B) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.CITY_EXTC_B, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("CITY_EXTC_B").ToString, rowNo, LMM550C.CITY_EXTC_B, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("CITY_EXTC_B")), rowNo, LMM550C.CITY_EXTC_B, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '【冬期割増Ａ】
                If String.IsNullOrEmpty(dr("WINT_EXTC_A").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック            
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.WINT_EXTC_A) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.WINT_EXTC_A, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("WINT_EXTC_A").ToString, rowNo, LMM550C.WINT_EXTC_A, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("WINT_EXTC_A")), rowNo, LMM550C.WINT_EXTC_A, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '【冬期割増Ｂ】
                If String.IsNullOrEmpty(dr("WINT_EXTC_B").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.WINT_EXTC_B) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.WINT_EXTC_B, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("WINT_EXTC_B").ToString, rowNo, LMM550C.WINT_EXTC_B, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("WINT_EXTC_B")), rowNo, LMM550C.WINT_EXTC_B, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '【中継料】
                If String.IsNullOrEmpty(dr("RELY_EXTC").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.RELY_EXTC) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.RELY_EXTC, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("RELY_EXTC").ToString, rowNo, LMM550C.RELY_EXTC, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("RELY_EXTC")), rowNo, LMM550C.RELY_EXTC, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '【保険料】
                If String.IsNullOrEmpty(dr("INSU").ToString) = False Then '取込データがNULLならチェックなし
                    '禁止文字チェック
                    If Me._V.IsExcelForbiddenWordsCheck(frm, dr, LMM550C.INSU) = False Then
                        rtnResult = Me.SetExcelInputErrData("E260", New String() {String.Concat(LMM550C.INSU, "に禁止文字が含まれているデータ")}, rowNo)
                        ErrCnt = ErrCnt + 1
                    Else
                        'バイト数チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkByte(frm, dr("INSU").ToString, rowNo, LMM550C.INSU, 9) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                        '範囲チェック(禁止文字チェックがエラーでなければチェックする)
                        If Me.ChkRange(frm, Convert.ToDecimal(dr("INSU")), rowNo, LMM550C.INSU, Convert.ToDecimal(0), Convert.ToDecimal(999999999)) = False Then
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '-----関連チェック
                '整合性チェック(データタイプ・車種・重量)
                If String.IsNullOrEmpty(dr("WT_LV").ToString) = False Then '取込データがNULLならチェックなし
                    If LMM550C.DATA_TP_KBN_00.Equals(dr("DATA_TP").ToString) = True Then
                        If (LMM550C.DATA_TP_KBN_00.Equals(dr("CAR_TP").ToString) = True _
                          AndAlso Convert.ToDecimal(0).Equals(Convert.ToDecimal(dr("WT_LV").ToString))) = True Then
                            'ＯＫ
                        Else
                            Dim msg1 As String = "データタイプが00（距離刻み）に対して（車種が00かつ重量が0）でないデータ"
                            rtnResult = Me.SetExcelInputErrData("E028", New String() {msg1, .FunctionKey.F7ButtonName}, rowNo)
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '整合性チェック(テーブルタイプ・重量)
                If String.IsNullOrEmpty(dr("WT_LV").ToString) = False Then '取込データがNULLならチェックなし
                    If (LMM550C.TABLE_TP_KBN_01.Equals(dr("TABLE_TP").ToString) = True _
                     OrElse LMM550C.TABLE_TP_KBN_06.Equals(dr("TABLE_TP").ToString)) = True Then
                        If Convert.ToDecimal(0).Equals(Convert.ToDecimal(dr("WT_LV").ToString)) = False Then
                            Dim msg2 As String = "テーブルタイプが01（車種・距離）または06（宅急便サイズ）に対して（重量が0）でないデータ"
                            rtnResult = Me.SetExcelInputErrData("E028", New String() {msg2, .FunctionKey.F7ButtonName}, rowNo)
                            ErrCnt = ErrCnt + 1
                        End If
                    End If
                End If

                '整合性チェック(テーブルタイプ・車種)
                If LMM550C.TABLE_TP_KBN_00.Equals(dr("TABLE_TP").ToString) = True _
                 OrElse LMM550C.TABLE_TP_KBN_02.Equals(dr("TABLE_TP").ToString) = True _
                 OrElse LMM550C.TABLE_TP_KBN_03.Equals(dr("TABLE_TP").ToString) = True _
                 OrElse LMM550C.TABLE_TP_KBN_04.Equals(dr("TABLE_TP").ToString) = True _
                 OrElse LMM550C.TABLE_TP_KBN_05.Equals(dr("TABLE_TP").ToString) = True _
                 OrElse LMM550C.TABLE_TP_KBN_07.Equals(dr("TABLE_TP").ToString) = True Then
                    If LMM550C.TABLE_TP_KBN_00.Equals(dr("CAR_TP").ToString) = False Then
                        Dim msg3 As String = "テーブルタイプが00（重量・距離）または02（個数・距離）または03（重量・距離）または 04（数量・距離）または05（個数・県コード）または07（重量・県（重量建））に対して（車種が00）でないデータ"
                        rtnResult = Me.SetExcelInputErrData("E028", New String() {msg3, .FunctionKey.F7ButtonName}, rowNo)
                        ErrCnt = ErrCnt + 1
                    End If
                End If

                '混在チェック(営業所・適用開始日・データタイプ)
                '前行のと対象行を比較し、営業所・適用開始日・データタイプのいずれかが異なる場合はエラー
                If LMConst.FLG.OFF.Equals(ChkEndFlg2) = True Then
                    If i <> rowMax Then   '最終行の場合、次の行がないのでチェックしない
                        If dt.Rows(i)("NRS_BR_CD").ToString <> dt.Rows(i + 1)("NRS_BR_CD").ToString _
                          OrElse dt.Rows(i)("SHIHARAI_TARIFF_CD").ToString <> dt.Rows(i + 1)("SHIHARAI_TARIFF_CD").ToString _
                          OrElse dt.Rows(i)("STR_DATE").ToString <> dt.Rows(i + 1)("STR_DATE").ToString Then
                            rtnResult = Me.SetExcelInputErrData("E028", New String() {"営業所・支払タリフコード・適用開始日の組合せが同一でないデータ", .FunctionKey.F7ButtonName}, rowNo)
                            ErrCnt = ErrCnt + 1
                            ChkEndFlg2 = LMConst.FLG.ON   'このチェックは終了
                        End If
                    End If
                End If

                '混在チェック(データタイプ)
                'チェック対象の場合
                If LMConst.FLG.OFF.Equals(ChkEndFlg3) = True Then
                    'データタイプ='00'はチェック対象外
                    If LMM550C.DATA_TP_KBN_00.Equals(dt.Rows(i)("DATA_TP").ToString) = False Then
                        '前行のと対象行を比較し、データタイプが異なる場合はエラー
                        If i <> rowMax Then   '最終行の場合、次の行がないのでチェックしない
                            If dt.Rows(i)("DATA_TP").ToString <> dt.Rows(i + 1)("DATA_TP").ToString Then
                                rtnResult = Me.SetExcelInputErrData("E375", New String() {"データタイプが00（距離刻み）に対して、データタイプ（運賃）が複数混在している", .FunctionKey.F7ButtonName}, rowNo)
                                ErrCnt = ErrCnt + 1
                                ChkEndFlg3 = LMConst.FLG.ON   'このチェックは終了
                            End If
                        End If
                    End If
                End If

            Next

            ''距離刻み存在チェック(ファイルにデータタイプ="00"(距離刻み)のデータが１行もない場合エラー)
            If rtnDataToCnt = 0 Then
                rtnResult = Me.SetExcelInputErrData("E079", New String() {"取込ファイル", "データタイプが00（距離刻み）のデータ"}, rowNo)
                ErrCnt = ErrCnt + 1
            End If

        End With

        Return ErrCnt

    End Function

    ''' <summary>
    ''' Excel取込データ固定値の設定処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function ExcelDataPGSet(ByVal ds As DataSet) As DataSet

        'DataSet設定
        Dim dt As DataTable = ds.Tables(LMM550C.TABLE_NM_KYORI)
        Dim dr As DataRow = Nothing
        Dim sort As String = String.Empty
        Dim sql As String = String.Empty

        sort = "NRS_BR_CD ASC,DATA_TP ASC,TABLE_TP ASC,CAR_TP ASC"
        sql = String.Concat("NRS_BR_CD = '", dt.Rows(0)("NRS_BR_CD"), "'")
        Dim dr2() As DataRow = dt.Select(sql, sort)
        Dim lngcnt As Integer = dr2.Length - 1

        For i As Integer = 0 To lngcnt
            Dim Unchin As String = 0.ToString()
            dr = dr2(i)

            '支払タリフコード枝番："000"から連番で設定
            Dim unchinCdEda As String = _ControlG.SetMaeZeroData(Convert.ToString(i), 3)
            dr("SHIHARAI_TARIFF_CD_EDA") = unchinCdEda

            '削除フラグ："0"固定
            dr("SYS_DEL_FLG") = LMM550C.FLG.OFF

            '重量～1件あたりの航送料(数値項目)：NULLの場合0固定
            If String.IsNullOrEmpty(dr("WT_LV").ToString) = True Then
                dr("WT_LV") = LMM550C.FLG.OFF
            End If
            For t As Integer = 1 To 70
                If String.IsNullOrEmpty(dr("KYORI_" & t).ToString) = True Then
                    dr("KYORI_" & t) = Unchin
                End If
            Next
            If String.IsNullOrEmpty(dr("CITY_EXTC_A").ToString) = True Then
                dr("CITY_EXTC_A") = Unchin
            End If
            If String.IsNullOrEmpty(dr("CITY_EXTC_B").ToString) = True Then
                dr("CITY_EXTC_B") = Unchin
            End If
            If String.IsNullOrEmpty(dr("WINT_EXTC_A").ToString) = True Then
                dr("WINT_EXTC_A") = Unchin
            End If
            If String.IsNullOrEmpty(dr("WINT_EXTC_B").ToString) = True Then
                dr("WINT_EXTC_B") = Unchin
            End If
            If String.IsNullOrEmpty(dr("RELY_EXTC").ToString) = True Then
                dr("RELY_EXTC") = Unchin
            End If
            If String.IsNullOrEmpty(dr("INSU").ToString) = True Then
                dr("INSU") = Unchin
            End If
            '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
            'If String.IsNullOrEmpty(dr("FRRY_EXTC_10KG").ToString) = True Then
            '    dr("FRRY_EXTC_10KG") = Unchin
            'End If
            If String.IsNullOrEmpty(dr("FRRY_EXTC_PART").ToString) = True Then
                dr("FRRY_EXTC_PART") = Unchin
            End If

            '支払タリフコード枝番の保持
            If String.IsNullOrEmpty(unchinCdEda) = False Then
                Me._Frm.lblMaxEda.TextValue = unchinCdEda
            End If

        Next

        Return ds

    End Function


    ''' <summary>
    ''' Excel出力処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub OutputExcelEvent(ByVal frm As LMM550F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.EXCELOUTPUT) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理　
            Exit Sub
        End If

        'Excel出力データの作成を行う
        Dim rtnDs As DataSet = Me.ExcelDataMake(frm)

        'Excel出力処理を行う
        If rtnDs IsNot Nothing Then

            Me.ExcelOut(rtnDs, frm)

        Else
            '処理終了アクション
            Call Me._ControlH.EndAction(frm)
            Exit Sub
        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(frm)

        'メッセージ用コード格納
        Dim UnchinCd As String = frm.txtShiharaiTariffCd.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F8ButtonName.Replace("　", String.Empty) _
                                              , String.Concat("[", frm.lblShiharaiTariffCd.Text, " = ", UnchinCd, "]")})

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' Excel出力データ作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ExcelDataMake(ByVal frm As LMM550F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMM550DS()
        Call Me.SetDataSetExcelInData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ExcelDataMake")

        '==== WSAクラス呼出（Excel出力データ作成処理） ====
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM550BLF", "ExcelDataMake", ds)

        '成功時共通処理を行う
        If rtnDs IsNot Nothing Then

        Else
            'メッセージを表示()
            MyBase.ShowMessage(frm, "G001")

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ExcelDataMake")

        Return rtnDs

    End Function

    ''' <summary>
    ''' Excel出力処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Sub ExcelOut(ByVal ds As DataSet, ByVal frm As LMM550F)

        '★★★エクセルデータ出力時、FRRY_EXTC_PART項目を出力されないようにするための修正ポイント
        Dim copyDs As DataSet = ds.Copy()
        copyDs.Tables(LMM550C.TABLE_NM_KYORI).Columns.Remove("FRRY_EXTC_PART")

        'DataSetの値を二次元配列に格納する
        Dim rowMax As Integer = copyDs.Tables(LMM550C.TABLE_NM_KYORI).Rows.Count - 1
        Dim colMax As Integer = copyDs.Tables(LMM550C.TABLE_NM_KYORI).Columns.Count - 4
        Dim excelData(rowMax + 1, colMax + 1) As String

        'タイトル列を設定
        For i As Integer = 0 To colMax
            excelData(0, i) = copyDs.Tables(LMM550C.TABLE_NM_KYORI).Columns(i).ColumnName
        Next

        '値を設定
        For i As Integer = 0 To rowMax
            For j As Integer = 0 To colMax
                excelData(i + 1, j) = copyDs.Tables(LMM550C.TABLE_NM_KYORI).Rows(i).Item(j).ToString
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

        xlsApp.Quit()

        xlsSheet = Nothing
        xlsBook = Nothing
        xlsApp = Nothing

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM550F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理　
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._ControlH.EndAction(frm) '終了処理　
            Exit Sub
        End If

        '編集部クリアフラグ
        Dim clearFlg As Integer = 0

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(frm) '終了処理
                'メッセージ設定
                MyBase.ShowMessage(Me._Frm, "G003")
                Exit Sub
            Else      'OK押下
                clearFlg = 1
            End If
        End If

        '検索処理を行う
        Call Me.SelectData(frm, clearFlg)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMM550F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave3(ByVal frm As LMM550F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        ''編集モード以外の場合、処理終了
        'If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
        '    Exit Sub
        'End If

        'Dim rowNo As Integer = e.NewRow
        'If rowNo < 1 Then
        '    Exit Sub
        'End If

        ''同じ行の場合、スルー
        'If e.Row = rowNo Then
        '    Exit Sub
        'End If

        'Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal frm As LMM550F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(frm)  '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(frm, e.Row)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMM550F, ByVal rowNo As Integer)

        Dim NrsBrcd As String = String.Empty
        Dim TariffCd As String = String.Empty
        Dim DataTp As String = String.Empty
        Dim StrDate As String = String.Empty
        Dim TableTp As String = String.Empty

        NrsBrcd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef.NRS_BR_CD.ColNo).Text()
        TariffCd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD.ColNo).Text()
        DataTp = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef.DATA_TP.ColNo).Text()
        StrDate = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef.STR_DATE.ColNo).Text()
        TableTp = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef.TABLE_TP.ColNo).Text()

        Dim dDate As Date = Date.Parse(StrDate)
        StrDate = dDate.ToString("yyyyMMdd")

        Dim dt2 As DataTable = Me._UnchinDs

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.DCLICK) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        '選択された計算種別による利用スプレッドの決定
        Me.SetSpreadType(frm, TableTp)

        'SPREAD(明細)初期化
        frm.sprDetail2.CrearSpread(2)
        frm.sprDetail3.CrearSpread(2)

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread2(dt2, LMM550C.EventShubetsu.DCLICK, NrsBrcd, TariffCd, DataTp, StrDate, TableTp)

        '選択行の列数を保持
        If String.IsNullOrEmpty(Me._Frm.lblDefAddCnt.TextValue) = False Then
            _BeCnt = CType(Me._Frm.lblDefAddCnt.TextValue, Integer)
        End If

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
    Private Sub MasterShowEvent(ByVal frm As LMM550F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMM550C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMMControlC.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMM550C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me._ControlH.EndAction(frm)

        '運賃タリフスプレッドのロック制御
        Call Me.SetLockControl(LMM550C.EventShubetsu.MASTEROPEN)

        'メッセージ設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveUnchinTariffItemData(ByVal frm As LMM550F, ByVal eventShubetsu As LMM550C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM550C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Return False
        End If

        ''項目チェック
        If Me._V.IsSaveInputChk(MyBase.GetSystemDateTime(0)) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        'Dim ds As DataSet = New LMM550DS()        
        Call Me.SetDatasetUnchinTariffColItemData(frm, Me._Ds, LMM550C.EventShubetsu.HOZON)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveUnchinTariffItemData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM550BLF", "InsertData", Me._Ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM550BLF", "UpdateData", Me._Ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._ControlH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveUnchinTariffItemData")

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.SHIHARAI_TARIFF)

        ''処理結果メッセージ表示
        Dim UnchinCd As String = frm.txtShiharaiTariffCd.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                              , String.Concat("[", frm.lblShiharaiTariffCd.Text, " = ", UnchinCd, "]")})

        '終了処理
        Call Me._ControlH.EndAction(frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集以外の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveUnchinTariffItemData(Me._Frm, LMM550C.EventShubetsu.TOJIRU) = False Then

                    e.Cancel = True

                End If


            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select


    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'Enterキー判定
            Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
            Dim rtnResult As Boolean = eventFlg

            'カーソル位置の設定
            Dim objNm As String = frm.ActiveControl.Name()

            '権限チェック
            rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM550C.EventShubetsu.ENTER)

            'カーソル位置チェック
            rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMMControlC.ENTER)

            'エラーの場合、終了
            If rtnResult = False Then
                'フォーカス移動処理
                Call Me._ControlH.NextFocusedControl(frm, eventFlg)
                Exit Sub
            End If

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(frm, objNm, LMM550C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

            'メッセージ設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(frm, eventFlg)

        End With

    End Sub

    ''' <summary>
    ''' 行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RowAdd(ByVal frm As LMM550F)

        Dim rtnResult As Boolean = False

        '選択された計算種別による利用スプレッドの決定
        Me.SetSpreadType(frm, frm.cmbTableTp.SelectedValue.ToString())

        'データテーブル(UNCHIN_KYORI)に行追加
        rtnResult = Me.RowAddUnchinDt(frm)

        If rtnResult = True Then

            Dim NrsBrcd As String = String.Empty
            Dim TableTp As String = String.Empty
            NrsBrcd = frm.cmbNrsBrCd.SelectedValue.ToString
            TableTp = frm.cmbTableTp.SelectedValue.ToString

            '運賃タリフ(距離刻み/運賃)情報表示
            Call Me._G.SetSpread2(Me._Ds.Tables(LMM550C.TABLE_NM_KYORI), LMM550C.EventShubetsu.INS_T, NrsBrcd, , , , TableTp)

            '計算種別をロック
            Call Me._ControlG.LockComb(Me._Frm.cmbTableTp, True)

            '運賃タリフスプレッドのロック制御
            Call Me.SetLockControl(LMM550C.EventShubetsu.INS_T)
        End If

        '処理終了アクション
        Call Me._ControlH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' データテーブル(UNCHIN_KYORI)に行追加
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function RowAddUnchinDt(ByVal frm As LMM550F) As Boolean

        '現在の運賃Spread情報(距離刻み/運賃)をDataSetに設定
        Call Me.SetDatasetUnchinTariffColItemData(frm, Me._Ds, LMM550C.EventShubetsu.INS_T)

        Dim dt As DataTable = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI)

        '処理開始アクション
        Call Me._ControlH.SetEnterEvent(frm)

        '行追加時チェック
        If Me._V.IsRowCheck(LMM550C.EventShubetsu.INS_T, frm) = False Then
            '処理終了アクション
            Call Me._ControlH.EndAction(frm)
            Return False
        End If

        '支払タリフコード枝番の採番
        If Me._G.SetUnchinCdEdaDataSet(Me._Ds, LMM550C.EventShubetsu.INS_T) = False Then
            '処理終了アクション
            Call Me._ControlH.EndAction(frm)
            Return False
        End If

        Dim max As Integer = dt.Columns.Count - 1
        Dim dr As DataRow = dt.NewRow()
        For i As Integer = 0 To max
            dr.Item(i) = String.Empty
        Next

        With Me._Frm

            Dim Unchin As String = 0.ToString()

            dr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            dr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCd.TextValue
            dr.Item("STR_DATE") = .imdStrDate.TextValue
            dr.Item("SHIHARAI_TARIFF_REM") = .txtShiharaiTariffRem.TextValue
            dr.Item("DATA_TP") = .cmbDataTp.SelectedValue
            dr.Item("TABLE_TP") = .cmbTableTp.SelectedValue
            dr.Item("CAR_TP") = String.Empty
            dr.Item("WT_LV") = Unchin
            dr.Item("KYORI_1") = Unchin
            dr.Item("KYORI_2") = Unchin
            dr.Item("KYORI_3") = Unchin
            dr.Item("KYORI_4") = Unchin
            dr.Item("KYORI_5") = Unchin
            dr.Item("KYORI_6") = Unchin
            dr.Item("KYORI_7") = Unchin
            dr.Item("KYORI_8") = Unchin
            dr.Item("KYORI_9") = Unchin
            dr.Item("KYORI_10") = Unchin
            dr.Item("KYORI_11") = Unchin
            dr.Item("KYORI_12") = Unchin
            dr.Item("KYORI_13") = Unchin
            dr.Item("KYORI_14") = Unchin
            dr.Item("KYORI_15") = Unchin
            dr.Item("KYORI_16") = Unchin
            dr.Item("KYORI_17") = Unchin
            dr.Item("KYORI_18") = Unchin
            dr.Item("KYORI_19") = Unchin
            dr.Item("KYORI_20") = Unchin
            dr.Item("KYORI_21") = Unchin
            dr.Item("KYORI_22") = Unchin
            dr.Item("KYORI_23") = Unchin
            dr.Item("KYORI_24") = Unchin
            dr.Item("KYORI_25") = Unchin
            dr.Item("KYORI_26") = Unchin
            dr.Item("KYORI_27") = Unchin
            dr.Item("KYORI_28") = Unchin
            dr.Item("KYORI_29") = Unchin
            dr.Item("KYORI_30") = Unchin
            dr.Item("KYORI_31") = Unchin
            dr.Item("KYORI_32") = Unchin
            dr.Item("KYORI_33") = Unchin
            dr.Item("KYORI_34") = Unchin
            dr.Item("KYORI_35") = Unchin
            dr.Item("KYORI_36") = Unchin
            dr.Item("KYORI_37") = Unchin
            dr.Item("KYORI_38") = Unchin
            dr.Item("KYORI_39") = Unchin
            dr.Item("KYORI_40") = Unchin
            dr.Item("KYORI_41") = Unchin
            dr.Item("KYORI_42") = Unchin
            dr.Item("KYORI_43") = Unchin
            dr.Item("KYORI_44") = Unchin
            dr.Item("KYORI_45") = Unchin
            dr.Item("KYORI_46") = Unchin
            dr.Item("KYORI_47") = Unchin
            dr.Item("KYORI_48") = Unchin
            dr.Item("KYORI_49") = Unchin
            dr.Item("KYORI_50") = Unchin
            dr.Item("KYORI_51") = Unchin
            dr.Item("KYORI_52") = Unchin
            dr.Item("KYORI_53") = Unchin
            dr.Item("KYORI_54") = Unchin
            dr.Item("KYORI_55") = Unchin
            dr.Item("KYORI_56") = Unchin
            dr.Item("KYORI_57") = Unchin
            dr.Item("KYORI_58") = Unchin
            dr.Item("KYORI_59") = Unchin
            dr.Item("KYORI_60") = Unchin
            dr.Item("KYORI_61") = Unchin
            dr.Item("KYORI_62") = Unchin
            dr.Item("KYORI_63") = Unchin
            dr.Item("KYORI_64") = Unchin
            dr.Item("KYORI_65") = Unchin
            dr.Item("KYORI_66") = Unchin
            dr.Item("KYORI_67") = Unchin
            dr.Item("KYORI_68") = Unchin
            dr.Item("KYORI_69") = Unchin
            dr.Item("KYORI_70") = Unchin
            dr.Item("CITY_EXTC_A") = Unchin
            dr.Item("CITY_EXTC_B") = Unchin
            dr.Item("WINT_EXTC_A") = Unchin
            dr.Item("WINT_EXTC_B") = Unchin
            dr.Item("RELY_EXTC") = Unchin
            dr.Item("INSU") = Unchin
            '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
            'dr.Item("FRRY_EXTC_10KG") = Unchin
            dr.Item("FRRY_EXTC_PART") = Unchin
            dr.Item("SHIHARAI_TARIFF_CD2") = .txtShiharaiTariffCd2.TextValue
            dr.Item("SHIHARAI_TARIFF_CD_EDA") = .lblMaxEda.TextValue
            dr.Item("ORIG_KEN_N") = String.Empty
            dr.Item("ORIG_CITY_N") = String.Empty
            dr.Item("ORIG_JIS_CD") = String.Empty
            dr.Item("DEST_KEN_N") = String.Empty
            dr.Item("DEST_CITY_N") = String.Empty
            dr.Item("DEST_JIS_CD") = String.Empty
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF
            dr.Item("UPD_FLG") = LMConst.FLG.OFF
            '営業所またぎ処理のため画面値より営業所コード取得
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()


        End With

        '行追加
        dt.Rows.Add(dr)

        Return True

    End Function

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteUnchinData(ByVal frm As LMM550F)

        'TypeBの場合
        If LMM550C.SpreadType.B.Equals(_G._SpreadType) Then
            Me.DeleteUnchinData_B(frm)
            Return
        End If

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        ''並び替え
        'Call Me._G.sprDetailSortColumnCommand()

        '項目チェック
        If Me._V.IsRowCheck(LMM550C.EventShubetsu.DEL_T, frm) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '現在の運賃Spread情報(距離刻み/運賃)をDataSetに設定
        Call Me.SetDatasetUnchinTariffColItemData(frm, Me._Ds, LMM550C.EventShubetsu.DEL_T)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        arr = Me._ControlH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM550G.sprDetailDef2.DEF.ColNo)

        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        Dim sort As String = String.Empty
        Dim sql As String = String.Empty
        Dim sql2 As String = String.Empty
        Dim NrsBrcd As String = Me._Frm.cmbNrsBrCd.SelectedValue.ToString
        Dim TariffCd As String = Me._Frm.txtShiharaiTariffCd.TextValue.ToString
        Dim StrDate As String = Me._Frm.imdStrDate.TextValue.ToString
        Dim TableTp As String = frm.cmbTableTp.SelectedValue.ToString
        Dim DataTp As String = String.Empty
        If String.IsNullOrEmpty(Me._Frm.lblExcelDt.TextValue.Trim) = True Then
            DataTp = Me._Frm.cmbDataTp.SelectedValue.ToString
        Else
            DataTp = Me._Frm.lblExcelDt.TextValue.ToString
        End If

        '正常データを抽出
        sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                      , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                      , "AND STR_DATE = '", StrDate, "' " _
                                      , "AND SYS_DEL_FLG = '", LMM550C.FLG.OFF, "' " _
                                      , "AND (DATA_TP = '", DataTp, "' " _
                                      , "OR  DATA_TP = '", LMM550C.TABLE_TP_KBN_00, "') " _
                                      )
        '削除データを抽出
        sql2 = String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                      , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                      , "AND STR_DATE = '", StrDate, "' " _
                                      , "AND SYS_DEL_FLG = '", LMM550C.FLG.ON, "' " _
                                      , "AND (DATA_TP = '", DataTp, "' " _
                                      , "OR  DATA_TP = '", LMM550C.TABLE_TP_KBN_00, "') " _
                                      )

        Dim setDrs As DataRow() = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).Select(sql)
        Dim setDrs2 As DataRow() = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).Select(sql2)

        Dim spr As SheetView = frm.sprDetail2.ActiveSheet
        Dim tariffCdEda As String = String.Empty
        For i As Integer = max To 0 Step -1

            ''行番号を設定
            'recNo = Me.GetRecNo(spr, arr(i).ToString(), LMM550G.sprDetailDef2.RECNO.ColNo)

            ''運賃タリフのレコード削除
            'Call Me.DeleteRowData(setDrs(recNo - 1))

            '枝番を取得
            tariffCdEda = Me._ControlV.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo))

            '運賃タリフのレコード削除
            For Each drItem As DataRow In setDrs
                If tariffCdEda.Equals(drItem.Item("SHIHARAI_TARIFF_CD_EDA").ToString()) = True Then
                    Call Me.DeleteRowData(drItem)
                    Exit For
                End If
            Next

        Next

        Dim ds As DataSet = New LMM550DS()
        '元々削除データだったものをIMPORT
        Dim cnt2 As Integer = setDrs2.Length - 1
        Dim dr2 As DataRow = Nothing
        For i As Integer = 0 To cnt2
            dr2 = setDrs2(i)
            ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr2)
        Next

        '新たに削除されたデータも含めて全ての行をIMPORT
        Dim cnt As Integer = setDrs.Length - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To cnt
            dr = setDrs(i)
            ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr)
        Next

        '運賃タリフ(距離刻み/運賃)情報表示
        Call Me._G.SetSpread2(ds.Tables(LMM550C.TABLE_NM_KYORI), LMM550C.EventShubetsu.DEL_T, NrsBrcd, , , , TableTp)

        '運賃タリフスプレッドのロック制御
        Call Me.SetLockControl(LMM550C.EventShubetsu.DEL_T)

        '処理終了アクション
        Call Me._ControlH.EndAction(frm)  '終了処理

    End Sub

    ''' <summary>
    ''' 行削除(TypeB)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteUnchinData_B(ByVal frm As LMM550F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '項目チェック
        If Me._V.IsRowCheck(LMM550C.EventShubetsu.DEL_T, frm) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '現在の運賃Spread情報(距離刻み/運賃)をDataSetに設定
        Call Me.SetDatasetUnchinTariffColItemData(frm, Me._Ds, LMM550C.EventShubetsu.DEL_T)

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        arr = Me._ControlH.GetCheckList(frm.sprDetail3.ActiveSheet, LMM550G.sprDetailDef3.DEF.ColNo)

        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        Dim sort As String = String.Empty
        Dim sql As String = String.Empty
        Dim sql2 As String = String.Empty
        Dim NrsBrcd As String = Me._Frm.cmbNrsBrCd.SelectedValue.ToString
        Dim TariffCd As String = Me._Frm.txtShiharaiTariffCd.TextValue.ToString
        Dim StrDate As String = Me._Frm.imdStrDate.TextValue.ToString
        Dim TableTp As String = frm.cmbTableTp.SelectedValue.ToString
        Dim DataTp As String = String.Empty
        If String.IsNullOrEmpty(Me._Frm.lblExcelDt.TextValue.Trim) = True Then
            DataTp = Me._Frm.cmbDataTp.SelectedValue.ToString
        Else
            DataTp = Me._Frm.lblExcelDt.TextValue.ToString
        End If

        '正常データを抽出
        sql = String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                      , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                      , "AND STR_DATE = '", StrDate, "' " _
                                      , "AND SYS_DEL_FLG = '", LMM550C.FLG.OFF, "' " _
                                      , "AND (DATA_TP = '", DataTp, "' " _
                                      , "OR  DATA_TP = '", LMM550C.TABLE_TP_KBN_00, "') "
                                      )
        '削除データを抽出
        sql2 = String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                      , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                      , "AND STR_DATE = '", StrDate, "' " _
                                      , "AND SYS_DEL_FLG = '", LMM550C.FLG.ON, "' " _
                                      , "AND (DATA_TP = '", DataTp, "' " _
                                      , "OR  DATA_TP = '", LMM550C.TABLE_TP_KBN_00, "') "
                                      )

        Dim setDrs As DataRow() = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).Select(sql)
        Dim setDrs2 As DataRow() = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).Select(sql2)

        Dim spr As SheetView = frm.sprDetail3.ActiveSheet
        Dim tariffCdEda As String = String.Empty
        For i As Integer = max To 0 Step -1

            '枝番を取得
            tariffCdEda = Me._ControlV.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo))

            '運賃タリフのレコード削除
            For Each drItem As DataRow In setDrs
                If tariffCdEda.Equals(drItem.Item("SHIHARAI_TARIFF_CD_EDA").ToString()) = True Then
                    Call Me.DeleteRowData(drItem)
                    Exit For
                End If
            Next

        Next

        Dim ds As DataSet = New LMM550DS()
        '元々削除データだったものをIMPORT
        Dim cnt2 As Integer = setDrs2.Length - 1
        Dim dr2 As DataRow = Nothing
        For i As Integer = 0 To cnt2
            dr2 = setDrs2(i)
            ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr2)
        Next

        '新たに削除されたデータも含めて全ての行をIMPORT
        Dim cnt As Integer = setDrs.Length - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To cnt
            dr = setDrs(i)
            ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr)
        Next

        '運賃タリフ(距離刻み/運賃)情報表示
        Call Me._G.SetSpread2(ds.Tables(LMM550C.TABLE_NM_KYORI), LMM550C.EventShubetsu.DEL_T, NrsBrcd, , , , TableTp)

        '運賃タリフスプレッドのロック制御
        Call Me.SetLockControl(LMM550C.EventShubetsu.DEL_T)

        '処理終了アクション
        Call Me._ControlH.EndAction(frm)  '終了処理

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM550F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM550DS()
        Call Me.SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(frm, "LMM550BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Call Me.SuccessSelect(frm, rtnDs)

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(frm)

        'ステータスの設定
        If clearflg <> 1 Then
        Else
            '画面の入力項目/ファンクションキーの制御
            Call Me._G.UnLockedForm()
            If rtnDs Is Nothing = False Then
                '現在の列数のクリア
                _BeCnt = 0
            End If
        End If

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMM550F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM550C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM550C.TABLE_NM_KYORI)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread(2)
        frm.sprDetail3.CrearSpread(2)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '取得データ(UNCHIN_TARIFF)をPrivate変数に保持
        Call Me.SetDataSetUnchinTariffData(dt2)

        '取得件数設定
        Me._CntSelect = dt.Rows.Count.ToString()

        '0件でないとき
        If Me._CntSelect.Equals(LMConst.FLG.OFF) = False Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Me._CntSelect})
        End If


        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' データセット設定(運賃タリフ情報(距離刻み/運賃)格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetUnchinTariffData(ByVal dt As DataTable)

        Me._UnchinDs = dt

    End Sub

    ''' <summary>
    ''' 運賃タリフスプレッド(距離刻み/運賃)のロック制御
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal actionType As LMM550C.EventShubetsu)

        Call Me._G.ChangeLockControl1(actionType)

    End Sub

    ''' <summary>
    ''' レコード番号を取得
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="value">行数</param>
    ''' <param name="colNo">列番号</param>
    ''' <returns>レコード番号</returns>
    ''' <remarks></remarks>
    Private Function GetRecNo(ByVal spr As SheetView, ByVal value As String, ByVal colNo As Integer) As Integer

        Return Convert.ToInt32(Me._ControlV.GetCellValue(spr.Cells(Convert.ToInt32(value), colNo)))

    End Function

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowData(ByVal dr As DataRow)

        If LMConst.FLG.ON.Equals(dr.Item("UPD_FLG").ToString()) = True Then

            '削除フラグをON
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON

        Else

            '行自体を削除
            dr.Delete()

        End If

    End Sub

    ''' <summary>
    ''' 距離刻み存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="dr">このハンドラクラスに紐づくデータロウ</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkExistDataTp(ByVal frm As LMM550F, ByVal dr As DataRow, ByVal rowNo As Integer) As Integer

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False
        Dim cnt As Integer = 0

        With Me._Frm

            'データタイプ="00"(距離刻み)のカウントを取る
            If (dr("DATA_TP").ToString).Equals(LMM550C.DATA_TP_KBN_00) = True Then
                cnt = cnt + 1
            End If

        End With

        Return cnt

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ChkData">チェック対象項目</param>
    ''' <param name="rowNo">取込データの行番号</param>
    ''' <param name="ChkCell">チェック対象項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkHissu(ByVal frm As LMM550F, ByVal ChkData As String, ByVal rowNo As Integer, ByVal ChkCell As String) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            If String.IsNullOrEmpty(ChkData) = True Then
                Return Me.SetExcelInputErrData("E260", New String() {String.Concat(ChkCell, "が空のデータ")}, rowNo)
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' バイト数チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ChkData">チェック対象項目</param>
    ''' <param name="rowNo">取込データの行番号</param>
    ''' <param name="ChkCell">チェック対象項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkByte(ByVal frm As LMM550F, ByVal ChkData As String, ByVal rowNo As Integer, ByVal ChkCell As String, ByVal ByteCnt As Integer) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            If ChkData.Length > ByteCnt Then
                '備考は全角文字なのでバイト数を２倍にしてメッセージを表示
                If ChkCell.Equals(.lblShiharaiTariffRem.Text) = True Then
                    ByteCnt = ByteCnt * 2
                End If
                Return Me.SetExcelInputErrData("E260", New String() {String.Concat(ChkCell, "が" & ByteCnt & "バイトを超えているデータ")}, rowNo)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 範囲チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ChkData">チェック対象項目</param>
    ''' <param name="rowNo">取込データの行番号</param>
    ''' <param name="ChkCell">チェック対象項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkRange(ByVal frm As LMM550F, ByVal ChkData As Decimal, ByVal rowNo As Integer, ByVal ChkCell As String, ByVal ByteCntF As Decimal, ByVal ByteCntT As Decimal) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            If ChkData < ByteCntF OrElse ChkData > ByteCntT Then
                Return Me.SetExcelInputErrData("E260", New String() {String.Concat(ChkCell, "が" & ByteCntF & "～" & ByteCntT & "の範囲外のデータ")}, rowNo)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' Excel取込時のメッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetExcelInputErrData(ByVal id As String, ByVal msg As String(), ByVal rowNo As Integer) As Boolean

        MyBase.SetMessageStore(LMMControlC.GUIDANCE_KBN, id, msg, rowNo.ToString())
        Return False

    End Function

    ''' <summary>
    ''' 計算種別による利用スプレッドの決定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="tableTp"></param>
    Private Sub SetSpreadType(ByVal frm As LMM550F, ByVal tableTp As String)

        Select Case tableTp
            Case LMM550C.TABLE_TP_KBN_08, LMM550C.TABLE_TP_KBN_09
                'TypeB
                _G._SpreadType = LMM550C.SpreadType.B
                _G._SpreadTypeSub = tableTp
                _V._SpreadType = LMM550C.SpreadType.B
                _V._SpreadTypeSub = tableTp
                frm.sprDetail2.Visible = False
                frm.sprDetail3.Visible = True
            Case Else
                'TypeA
                _G._SpreadType = LMM550C.SpreadType.A
                _G._SpreadTypeSub = ""
                _V._SpreadType = LMM550C.SpreadType.A
                _V._SpreadTypeSub = ""
                frm.sprDetail2.Visible = True
                frm.sprDetail3.Visible = False
        End Select

    End Sub

#End Region

#End Region 'イベント定義(一覧)

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMM550F, ByVal objNm As String, ByVal eventshubetsu As LMM550C.EventShubetsu) As Boolean

        With frm

            'スプレッドの場合、後でロック
            If objNm.Equals(.sprDetail3.Name) = False Then

                '処理開始アクション
                Call Me._ControlH.StartAction(frm)

            End If

            Select Case objNm

                Case .sprDetail3.Name

                    Return Me.ShowPopupSpread(frm, objNm, eventshubetsu)

                Case .txtShiharaiTariffCd2.Name

                    Call Me.SetReturnUnchinTariffPop(frm, objNm, eventshubetsu)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' ポップアップ起動スプレッド(TypeB)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="eventshubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupSpread(ByVal frm As LMM550F, ByVal objNm As String, ByVal eventshubetsu As LMM550C.EventShubetsu) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail3

        With spr.ActiveSheet

            If 0 < .Rows.Count Then

                Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
                Dim colNo As Integer = cell.Column.Index
                Dim rowNo As Integer = cell.Row.Index

                Select Case colNo

                    Case LMM550G.sprDetailDef3.ORIG_JIS_CD.ColNo
                        '起点/JISコード

                        'ロック項目はスルー
                        If cell.Locked = True OrElse spr.ActiveSheet.Columns(cell.Column.Index).Locked = True Then
                            Return False
                        End If

                        '処理開始アクション
                        Call Me._ControlH.StartAction(frm)

                        'Enter処理は値がない場合、名称をクリア
                        If LMM550C.EventShubetsu.ENTER.Equals(eventshubetsu) Then
                            If String.IsNullOrEmpty(Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(rowNo, colNo))) = True Then
                                spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.ORIG_KEN_N.ColNo, String.Empty)
                                spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.ORIG_CITY_N.ColNo, String.Empty)
                                Return False
                            End If
                        End If

                        Dim jisCd As String = Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef3.ORIG_JIS_CD.ColNo))
                        Dim toJisPop As LMFormData = Me.ShowJisPopup(frm, rowNo, eventshubetsu, jisCd)
                        If toJisPop.ReturnFlg = True Then
                            Dim toJisDr As DataRow = toJisPop.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)
                            spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.ORIG_KEN_N.ColNo, toJisDr.Item("KEN").ToString())
                            spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.ORIG_CITY_N.ColNo, toJisDr.Item("SHI").ToString())
                            spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.ORIG_JIS_CD.ColNo, toJisDr.Item("JIS_CD").ToString())
                        End If

                    Case LMM550G.sprDetailDef3.DEST_JIS_CD.ColNo
                        '着点/JISコード

                        'ロック項目はスルー
                        If cell.Locked = True OrElse spr.ActiveSheet.Columns(cell.Column.Index).Locked = True Then
                            Return False
                        End If

                        '処理開始アクション
                        Call Me._ControlH.StartAction(frm)

                        'Enter処理は値がない場合、名称をクリア
                        If LMM550C.EventShubetsu.ENTER.Equals(eventshubetsu) Then
                            If String.IsNullOrEmpty(Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(rowNo, colNo))) = True Then
                                spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.DEST_KEN_N.ColNo, String.Empty)
                                spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.DEST_CITY_N.ColNo, String.Empty)
                                Return False
                            End If
                        End If

                        Dim jisCd As String = Me._ControlV.GetCellValue(spr.ActiveSheet.Cells(rowNo, LMM550G.sprDetailDef3.DEST_JIS_CD.ColNo))
                        Dim toJisPop As LMFormData = Me.ShowJisPopup(frm, rowNo, eventshubetsu, jisCd)
                        If toJisPop.ReturnFlg = True Then
                            Dim toJisDr As DataRow = toJisPop.ParamDataSet.Tables(LMZ070C.TABLE_NM_OUT).Rows(0)
                            spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.DEST_KEN_N.ColNo, toJisDr.Item("KEN").ToString())
                            spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.DEST_CITY_N.ColNo, toJisDr.Item("SHI").ToString())
                            spr.SetCellValue(rowNo, LMM550G.sprDetailDef3.DEST_JIS_CD.ColNo, toJisDr.Item("JIS_CD").ToString())
                        End If

                    Case Else

                        If LMM550C.EventShubetsu.ENTER.Equals(eventshubetsu) Then
                            'フォーカス移動処理
                            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
                            Return False
                        End If

                        Return Me._ControlV.SetFocusErrMessage()

                End Select

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 支払運賃タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnchinTariffPop(ByVal frm As LMM550F, ByVal objNm As String, ByVal eventshubetsu As LMM550C.EventShubetsu) As Boolean

        Dim ctl As Win.InputMan.LMImTextBox = Me._ControlH.GetTextControl(frm, objNm)
        Dim prm As LMFormData = Me.ShowUnchinTariffPopup(frm, ctl, eventshubetsu)

        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ290C.TABLE_NM_OUT).Rows(0)

            With frm

                ctl.TextValue = dr.Item("SHIHARAI_TARIFF_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 支払運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMM550F, ByVal ctl As Win.InputMan.LMImTextBox, ByVal eventshubetsu As LMM550C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ290DS()
        Dim dt As DataTable = ds.Tables(LMZ290C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Dim brCd As String = frm.cmbNrsBrCd.SelectedValue.ToString()
            .Item("NRS_BR_CD") = brCd
            If eventshubetsu = LMM550C.EventShubetsu.ENTER Then
                .Item("SHIHARAI_TARIFF_CD") = ctl.TextValue
            End If
            .Item("STR_DATE") = Me._SysDate
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ290", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' JIS参照POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rowNo"></param>
    ''' <param name="eventshubetsu"></param>
    ''' <param name="jisCd"></param>
    ''' <returns></returns>
    Private Function ShowJisPopup(ByVal frm As LMM550F, ByVal rowNo As Integer, ByVal eventshubetsu As LMM550C.EventShubetsu, ByVal jisCd As String) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ070DS()
        Dim dt As DataTable = ds.Tables(LMZ070C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim spr As SheetView = frm.sprDetail.ActiveSheet

        With dr
            If eventshubetsu = LMM550C.EventShubetsu.ENTER Then
                .Item("JIS_CD") = jisCd
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._ControlH.FormShow(ds, "LMZ070", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dr As DataRow = Me._FindDs.Tables(LMM550C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprDetail.ActiveSheet

            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM550G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM550G.sprDetailDef.NRS_BR_NM.ColNo))
            dr.Item("SHIHARAI_TARIFF_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM550G.sprDetailDef.SHIHARAI_TARIFF_CD.ColNo))
            dr.Item("DATA_TP") = Me._ControlV.GetCellValue(.Cells(0, LMM550G.sprDetailDef.DATA_TP_NM.ColNo))
            dr.Item("TABLE_TP") = Me._ControlV.GetCellValue(.Cells(0, LMM550G.sprDetailDef.TABLE_TP_NM.ColNo))

            '営業所またぎ処理のため画面値より営業所コード取得
            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM550G.sprDetailDef.NRS_BR_CD.ColNo))

            Me._FindDs.Tables(LMM550C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim dr As DataRow = Me._Ds.Tables(LMM550C.TABLE_NM_IN).NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCd.TextValue.Trim
            dr.Item("STR_DATE") = .imdStrDate.TextValue.Trim
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            'スキーマ名取得用
            '営業所またぎ処理のため画面値より営業所コード取得
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Me._Ds.Tables(LMM550C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新・列挿入・列削除・行追加・行削除用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUnchinTariffColItemData(ByVal frm As LMM550F, ByVal ds As DataSet, ByVal eventShubetsu As LMM550C.EventShubetsu)

        'TypeBの場合
        If LMM550C.SpreadType.B.Equals(_G._SpreadType) Then
            Me.SetDatasetUnchinTariffColItemData_B(frm, ds, eventShubetsu)
            Return
        End If

        With frm

            Dim dr As DataRow = Nothing
            Dim NrsBrcd As String = frm.cmbNrsBrCd.SelectedValue.ToString
            Dim TariffCd As String = frm.txtShiharaiTariffCd.TextValue
            Dim sort As String = String.Empty

            '列挿入・列削除・行追加・行削除時はソートしない
            If eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.DEL_T) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.INS_T) = True Then
                'ソートしない
            Else
                Select Case frm.cmbTableTp.SelectedValue.ToString()
                    Case LMM550C.TABLE_TP_KBN_00, LMM550C.TABLE_TP_KBN_02, LMM550C.TABLE_TP_KBN_03, _
                         LMM550C.TABLE_TP_KBN_04, LMM550C.TABLE_TP_KBN_05, LMM550C.TABLE_TP_KBN_07

                        sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,WT_LV ASC,CAR_TP ASC,SHIHARAI_TARIFF_CD_EDA ASC"
                    Case LMM550C.TABLE_TP_KBN_01, LMM550C.TABLE_TP_KBN_06

                        sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,CAR_TP ASC,WT_LV ASC,SHIHARAI_TARIFF_CD_EDA ASC"
                End Select
            End If

            '現在のDataSet内の削除データを設定先のDataSetにImportする
            Dim drs As DataRow() = ds.Tables(LMM550C.TABLE_NM_KYORI).Select(String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                                                     , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                                                     , "AND SYS_DEL_FLG = '", LMM550C.FLG.ON, "' " _
                                                                     ), sort)
            'DataSetのクリア
            Me._Ds = New LMM550DS()

            Dim drIn As DataRow = Me._Ds.Tables(LMM550C.TABLE_NM_IN).NewRow()
            If String.IsNullOrEmpty(.lblExcelDt.TextValue.Trim) = True Then
                drIn.Item("DATA_TP") = .cmbDataTp.SelectedValue
            Else
                drIn.Item("DATA_TP") = .lblExcelDt.TextValue.Trim
            End If
            drIn.Item("TABLE_TP") = .cmbTableTp.SelectedValue
            '営業所またぎ処理のため画面値より営業所コード取得
            drIn.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            Me._Ds.Tables(LMM550C.TABLE_NM_IN).Rows.Add(drIn)

            Dim cnt As Integer = drs.Length - 1
            Dim dr2 As DataRow = Nothing
            For i As Integer = 0 To cnt
                dr2 = drs(i)
                Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr2)
            Next

            '現在の運賃タリフSpread(距離刻み/運賃)情報をDataSetに設定
            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1

            For i As Integer = 1 To sprMax

                '編集部の値（運賃タリフ情報）をデータセットに設定
                dr = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).NewRow()

                dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCd.TextValue.Trim
                dr.Item("STR_DATE") = .imdStrDate.TextValue.Trim
                If i = 1 Then
                    dr.Item("DATA_TP") = LMM550C.DATA_TP_KBN_00
                Else                    
                    If String.IsNullOrEmpty(.lblExcelDt.TextValue.Trim) = True Then
                        dr.Item("DATA_TP") = .cmbDataTp.SelectedValue
                    Else
                        dr.Item("DATA_TP") = .lblExcelDt.TextValue.Trim
                    End If
                End If
                dr.Item("TABLE_TP") = .cmbTableTp.SelectedValue
                dr.Item("SHIHARAI_TARIFF_REM") = .txtShiharaiTariffRem.TextValue.Trim
                dr.Item("SHIHARAI_TARIFF_CD2") = .txtShiharaiTariffCd2.TextValue.Trim
                '営業所またぎ処理のため画面値より営業所コード取得
                dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

                '距離刻みの行の場合、固定値を設定
                If i = 1 Then
                    dr.Item("WT_LV") = 0
                    dr.Item("CAR_TP") = LMM550C.DATA_TP_KBN_00
                    dr.Item("CITY_EXTC_A") = 0
                    dr.Item("CITY_EXTC_B") = 0
                    dr.Item("WINT_EXTC_A") = 0
                    dr.Item("WINT_EXTC_B") = 0
                    dr.Item("RELY_EXTC") = 0
                    dr.Item("INSU") = 0
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    'dr.Item("FRRY_EXTC_10KG") = 0
                    dr.Item("FRRY_EXTC_PART") = 0
                    dr.Item("FRRY_EXTC_PART") = 0
                    dr.Item("SHIHARAI_TARIFF_CD_EDA") = "000"
                    '列数の増減によって、列がズレた分を修正
                    Dim ADColCnt As Integer = 0
                    If (LMM550C.EventShubetsu.COLADD).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt + 1
                    End If
                    If (LMM550C.EventShubetsu.COLDEL).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt - 1
                    End If
                    dr.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.UPD_FLG.ColNo + ADColCnt)))
                    dr.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_DATE") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SYS_UPD_DATE.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_TIME") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SYS_UPD_TIME.ColNo + ADColCnt)))
                Else

                    If (LMM550C.TABLE_TP_KBN_00).Equals(.cmbTableTp.SelectedValue) = True _
                    OrElse (LMM550C.TABLE_TP_KBN_03).Equals(.cmbTableTp.SelectedValue) = True _
                    OrElse (LMM550C.TABLE_TP_KBN_07).Equals(.cmbTableTp.SelectedValue) = True Then
                        dr.Item("WT_LV") = Convert.ToDecimal(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.WT_LV.ColNo)))
                    End If

                    If (LMM550C.TABLE_TP_KBN_02).Equals(.cmbTableTp.SelectedValue) = True _
                    OrElse (LMM550C.TABLE_TP_KBN_05).Equals(.cmbTableTp.SelectedValue) = True Then
                        dr.Item("WT_LV") = Convert.ToDecimal(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.NB.ColNo)))
                    End If

                    If (LMM550C.TABLE_TP_KBN_04).Equals(.cmbTableTp.SelectedValue) = True Then
                        dr.Item("WT_LV") = Convert.ToDecimal(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.QT.ColNo)))
                    End If

                    '画面：テーブルタイプが01,06の場合は画面の値(0)を設定
                    If (LMM550C.TABLE_TP_KBN_01).Equals(.cmbTableTp.SelectedValue) = True _
                    OrElse (LMM550C.TABLE_TP_KBN_06).Equals(.cmbTableTp.SelectedValue) = True Then
                        dr.Item("WT_LV") = Convert.ToDecimal(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.WT_LV.ColNo)))
                    End If


                    '画面：車種がロックされている場合(or空白の場合)は、'00'固定
                    If String.IsNullOrEmpty(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.CAR_TP.ColNo))) = True Then
                        dr.Item("CAR_TP") = LMM550C.DATA_TP_KBN_00
                        If (LMM550C.TABLE_TP_KBN_01).Equals(.cmbTableTp.SelectedValue) = True Then
                            dr.Item("CAR_TP") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.CAR_TP.ColNo))
                        End If
                        If (LMM550C.TABLE_TP_KBN_06).Equals(.cmbTableTp.SelectedValue) = True Then
                            dr.Item("CAR_TP") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.T_SIZE.ColNo))
                        End If
                    Else
                        If (LMM550C.TABLE_TP_KBN_01).Equals(.cmbTableTp.SelectedValue) = True Then
                            dr.Item("CAR_TP") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.CAR_TP.ColNo))
                        End If
                        If (LMM550C.TABLE_TP_KBN_06).Equals(.cmbTableTp.SelectedValue) = True Then
                            dr.Item("CAR_TP") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM550G.sprDetailDef2.T_SIZE.ColNo))
                        End If
                    End If

                    '列数の増減によって、列がズレた分を修正
                    Dim ADColCnt As Integer = 0
                    If (LMM550C.EventShubetsu.COLADD).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt + 1
                    End If
                    If (LMM550C.EventShubetsu.COLDEL).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt - 1
                    End If

                    dr.Item("CITY_EXTC_A") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.CITY_EXTC_A.ColNo + ADColCnt)))
                    dr.Item("CITY_EXTC_B") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.CITY_EXTC_B.ColNo + ADColCnt)))
                    dr.Item("WINT_EXTC_A") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.WINT_EXTC_A.ColNo + ADColCnt)))
                    dr.Item("WINT_EXTC_B") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.WINT_EXTC_B.ColNo + ADColCnt)))
                    dr.Item("RELY_EXTC") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.RELY_EXTC.ColNo + ADColCnt)))
                    dr.Item("INSU") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.INSU.ColNo + ADColCnt)))
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    'dr.Item("FRRY_EXTC_10KG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.FRRY_EXTC_10KG.ColNo + ADColCnt)))
                    dr.Item("FRRY_EXTC_PART") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.FRRY_EXTC_PART.ColNo + ADColCnt)))
                    dr.Item("SHIHARAI_TARIFF_CD_EDA") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SHIHARAI_TARIFF_CD_EDA.ColNo + ADColCnt)))
                    dr.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.UPD_FLG.ColNo + ADColCnt)))
                    dr.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SYS_DEL_FLG_T.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_DATE") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SYS_UPD_DATE.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_TIME") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, (LMM550G.sprDetailDef2.SYS_UPD_TIME.ColNo + ADColCnt)))

                End If

                Dim colCnt As Integer = 0
                For s As Integer = LMM550C.SprColumnIndex2.KYORI_1 To LMM550C.SprColumnIndex2.KYORI_70
                    If .sprDetail2.ActiveSheet.Cells(i, s).Column.Visible = True Then
                        colCnt = colCnt + 1
                        dr.Item("KYORI_" & colCnt) = Me._ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, s))
                    End If
                    If String.IsNullOrEmpty(_ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, s))) = True Then
                        dr.Item("KYORI_" & (s - 5)) = CType("0.000", Integer)
                    End If
                Next

                For cStart As Integer = colCnt + 1 To 70
                    dr.Item("KYORI_" & cStart) = CType("0.000", Integer)
                Next

                Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).Rows.Add(dr)

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ/TypeB)(登録・更新・列挿入・列削除・行追加・行削除用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetUnchinTariffColItemData_B(ByVal frm As LMM550F, ByVal ds As DataSet, ByVal eventShubetsu As LMM550C.EventShubetsu)

        With frm

            Dim dr As DataRow = Nothing
            Dim NrsBrcd As String = frm.cmbNrsBrCd.SelectedValue.ToString
            Dim TariffCd As String = frm.txtShiharaiTariffCd.TextValue
            Dim sort As String = String.Empty

            '列挿入・列削除・行追加・行削除時はソートしない
            If eventShubetsu.Equals(LMM550C.EventShubetsu.COLADD) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.COLDEL) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.DEL_T) = True _
                OrElse eventShubetsu.Equals(LMM550C.EventShubetsu.INS_T) = True Then
                'ソートしない
            Else
                sort = "NRS_BR_CD ASC,SHIHARAI_TARIFF_CD ASC,DATA_TP ASC,TABLE_TP ASC,ORIG_JIS_CD ASC,DEST_JIS_CD ASC,SHIHARAI_TARIFF_CD_EDA ASC"
            End If

            '現在のDataSet内の削除データを設定先のDataSetにImportする
            Dim drs As DataRow() = ds.Tables(LMM550C.TABLE_NM_KYORI).Select(String.Concat("NRS_BR_CD = '", NrsBrcd, "' " _
                                                                     , "AND SHIHARAI_TARIFF_CD = '", TariffCd, "' " _
                                                                     , "AND SYS_DEL_FLG = '", LMM550C.FLG.ON, "' "
                                                                     ), sort)
            'DataSetのクリア
            Me._Ds = New LMM550DS()

            Dim drIn As DataRow = Me._Ds.Tables(LMM550C.TABLE_NM_IN).NewRow()
            If String.IsNullOrEmpty(.lblExcelDt.TextValue.Trim) = True Then
                drIn.Item("DATA_TP") = .cmbDataTp.SelectedValue
            Else
                drIn.Item("DATA_TP") = .lblExcelDt.TextValue.Trim
            End If
            drIn.Item("TABLE_TP") = .cmbTableTp.SelectedValue
            '営業所またぎ処理のため画面値より営業所コード取得
            drIn.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            Me._Ds.Tables(LMM550C.TABLE_NM_IN).Rows.Add(drIn)

            Dim cnt As Integer = drs.Length - 1
            Dim dr2 As DataRow = Nothing
            For i As Integer = 0 To cnt
                dr2 = drs(i)
                Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).ImportRow(dr2)
            Next

            '現在の運賃タリフSpread(距離刻み/運賃)情報をDataSetに設定
            Dim sprMax As Integer = .sprDetail3.ActiveSheet.Rows.Count - 1

            For i As Integer = 1 To sprMax

                '編集部の値（運賃タリフ情報）をデータセットに設定
                dr = Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).NewRow()

                dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCd.TextValue.Trim
                dr.Item("STR_DATE") = .imdStrDate.TextValue.Trim
                If i = 1 Then
                    dr.Item("DATA_TP") = LMM550C.DATA_TP_KBN_00
                Else
                    If String.IsNullOrEmpty(.lblExcelDt.TextValue.Trim) = True Then
                        dr.Item("DATA_TP") = .cmbDataTp.SelectedValue
                    Else
                        dr.Item("DATA_TP") = .lblExcelDt.TextValue.Trim
                    End If
                End If
                dr.Item("TABLE_TP") = .cmbTableTp.SelectedValue
                dr.Item("SHIHARAI_TARIFF_REM") = .txtShiharaiTariffRem.TextValue.Trim
                dr.Item("SHIHARAI_TARIFF_CD2") = .txtShiharaiTariffCd2.TextValue.Trim
                '営業所またぎ処理のため画面値より営業所コード取得
                dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

                '距離刻みの行の場合、固定値を設定
                If i = 1 Then
                    dr.Item("ORIG_KEN_N") = String.Empty
                    dr.Item("ORIG_CITY_N") = String.Empty
                    dr.Item("ORIG_JIS_CD") = String.Empty
                    dr.Item("DEST_KEN_N") = String.Empty
                    dr.Item("DEST_CITY_N") = String.Empty
                    dr.Item("DEST_JIS_CD") = String.Empty
                    dr.Item("WT_LV") = 0
                    dr.Item("CAR_TP") = LMM550C.DATA_TP_KBN_00
                    dr.Item("CITY_EXTC_A") = 0
                    dr.Item("CITY_EXTC_B") = 0
                    dr.Item("WINT_EXTC_A") = 0
                    dr.Item("WINT_EXTC_B") = 0
                    dr.Item("RELY_EXTC") = 0
                    dr.Item("INSU") = 0
                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    'dr.Item("FRRY_EXTC_10KG") = 0
                    dr.Item("FRRY_EXTC_PART") = 0
                    dr.Item("FRRY_EXTC_PART") = 0
                    dr.Item("SHIHARAI_TARIFF_CD_EDA") = "000"
                    '列数の増減によって、列がズレた分を修正
                    Dim ADColCnt As Integer = 0
                    If (LMM550C.EventShubetsu.COLADD).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt + 1
                    End If
                    If (LMM550C.EventShubetsu.COLDEL).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt - 1
                    End If
                    dr.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.UPD_FLG.ColNo + ADColCnt)))
                    dr.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_DATE") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SYS_UPD_DATE.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_TIME") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SYS_UPD_TIME.ColNo + ADColCnt)))
                Else

                    dr.Item("ORIG_KEN_N") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM550G.sprDetailDef3.ORIG_KEN_N.ColNo))
                    dr.Item("ORIG_CITY_N") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM550G.sprDetailDef3.ORIG_CITY_N.ColNo))
                    dr.Item("ORIG_JIS_CD") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM550G.sprDetailDef3.ORIG_JIS_CD.ColNo))
                    dr.Item("DEST_KEN_N") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM550G.sprDetailDef3.DEST_KEN_N.ColNo))
                    dr.Item("DEST_CITY_N") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM550G.sprDetailDef3.DEST_CITY_N.ColNo))
                    dr.Item("DEST_JIS_CD") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM550G.sprDetailDef3.DEST_JIS_CD.ColNo))
                    dr.Item("WT_LV") = 0
                    dr.Item("CAR_TP") = LMM550C.DATA_TP_KBN_00
                    dr.Item("CITY_EXTC_A") = 0
                    dr.Item("CITY_EXTC_B") = 0
                    dr.Item("WINT_EXTC_A") = 0
                    dr.Item("WINT_EXTC_B") = 0
                    dr.Item("RELY_EXTC") = 0
                    dr.Item("INSU") = 0

                    '列数の増減によって、列がズレた分を修正
                    Dim ADColCnt As Integer = 0
                    If (LMM550C.EventShubetsu.COLADD).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt + 1
                    End If
                    If (LMM550C.EventShubetsu.COLDEL).Equals(eventShubetsu) = True Then
                        ADColCnt = ADColCnt - 1
                    End If

                    '運賃タリフマスタと同じく"FRRY_EXTC_10KG"を除外する
                    'dr.Item("FRRY_EXTC_10KG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.FRRY_EXTC_10KG.ColNo + ADColCnt)))
                    dr.Item("FRRY_EXTC_PART") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.FRRY_EXTC_PART.ColNo + ADColCnt)))
                    dr.Item("SHIHARAI_TARIFF_CD_EDA") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SHIHARAI_TARIFF_CD_EDA.ColNo + ADColCnt)))
                    dr.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.UPD_FLG.ColNo + ADColCnt)))
                    dr.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SYS_DEL_FLG_T.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_DATE") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SYS_UPD_DATE.ColNo + ADColCnt)))
                    dr.Item("SYS_UPD_TIME") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, (LMM550G.sprDetailDef3.SYS_UPD_TIME.ColNo + ADColCnt)))

                End If

                Dim colCnt As Integer = 0
                For s As Integer = LMM550C.SprColumnIndex3.KYORI_1 To LMM550C.SprColumnIndex3.KYORI_70
                    If .sprDetail3.ActiveSheet.Cells(i, s).Column.Visible = True Then
                        colCnt = colCnt + 1
                        dr.Item("KYORI_" & colCnt) = Me._ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, s))
                    End If
                    If String.IsNullOrEmpty(_ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, s))) = True Then
                        dr.Item("KYORI_" & (s - 6)) = CType("0.000", Integer)
                    End If
                Next

                For cStart As Integer = colCnt + 1 To 70
                    dr.Item("KYORI_" & cStart) = CType("0.000", Integer)
                Next

                Me._Ds.Tables(LMM550C.TABLE_NM_KYORI).Rows.Add(dr)

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(削除復活処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM550C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCd.TextValue.Trim
            dr.Item("STR_DATE") = .imdStrDate.TextValue.Trim
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            '営業所またぎ処理のため画面値より営業所コード取得
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            '削除/復活の切り替えを行う
            Dim delflg As String = String.Empty
            Select Case .lblSituation.RecordStatus
                Case RecordStatus.NOMAL_REC
                    delflg = LMConst.FLG.ON
                Case RecordStatus.DELETE_REC
                    delflg = LMConst.FLG.OFF
            End Select
            dr.Item("SYS_DEL_FLG") = delflg

            ds.Tables(LMM550C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(Excel出力条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetExcelInData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM550C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr.Item("SHIHARAI_TARIFF_CD") = .txtShiharaiTariffCd.TextValue.Trim
            dr.Item("STR_DATE") = .imdStrDate.TextValue.Trim
            '営業所またぎ処理のため画面値より営業所コード取得
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.OFF

            ds.Tables(LMM550C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し(Excel取込処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "InputExcelEvent")

        'Excel取込処理
        Me.InputExcelEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "InputExcelEvent")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し(Excel出力処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "OutputExcelEvent")

        'Excel出力処理
        Me.OutputExcelEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "OutputExcelEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveUnchinTariffItemData")

        Me.SaveUnchinTariffItemData(frm, LMM550C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveUnchinTariffItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM550F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMM550F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_UNCHIN_Click(ByRef frm As LMM550F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_UNCHIN_Click")

        '「行追加」処理
        Call Me.RowAdd(frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_UNCHIN_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_UNCHIN_Click(ByRef frm As LMM550F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_UNCHIN_Click")

        '「行削除」処理
        Call Me.DeleteUnchinData(frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_UNCHIN_Click")

    End Sub

    ''' <summary>
    ''' 列の挿入・削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ColAddDel(ByVal frm As LMM550F, ByVal mode As LM.GUI.Win.Spread.LMSpread.ColMode)

        'TypeBの場合
        If LMM550C.SpreadType.B.Equals(_G._SpreadType) Then
            Me.ColAddDel_B(frm, mode)
            Return
        End If

        Dim EventShubetsu As LMM550C.EventShubetsu = LMM550C.EventShubetsu.MAIN
        Dim Index As Integer = 0
        If String.IsNullOrEmpty(Me._Frm.lblDefAddCnt.TextValue) = True Then
            Me._Frm.lblDefAddCnt.TextValue = 0.ToString
        End If

        'フォーカスの位置を設定
        Index = frm.sprDetail2.ActiveSheet.ActiveCell.Column.Index

        'フォーカスが距離(Km)列の範囲内にある場合のみ処理を行う
        If LMM550C.SprColumnIndex2.KYORI_1 <= Index And Index <= LMM550C.SprColumnIndex2.KYORI_70 Then

            If (CType(1, LMSpread.ColMode)).Equals(mode) = True Then
                _BeCnt = CType(Me._Frm.lblDefAddCnt.TextValue, Integer) + 1
                EventShubetsu = LMM550C.EventShubetsu.COLADD
            End If
            If (CType(-1, LMSpread.ColMode)).Equals(mode) = True Then
                _BeCnt = CType(Me._Frm.lblDefAddCnt.TextValue, Integer) - 1
                EventShubetsu = LMM550C.EventShubetsu.COLDEL
            End If

            '列挿入後の距離刻みの列数が70を超えた場合、もしくは列削除後の距離の列数が1になった場合は処理終了
            If _BeCnt > 70 Then
                _BeCnt = 70   '設定可能な列数に戻す
                Exit Sub
            End If
            If _BeCnt < 1 Then
                _BeCnt = 1   '設定可能な列数に戻す
                Exit Sub
            End If

            '列挿入/列追加
            Dim beforeColumnsCount As Integer = frm.sprDetail2.ActiveSheet.Columns.Count
            frm.sprDetail2.ColumnAddRemove(mode)
            frm.sprDetail2.ActiveSheet.Columns.Count = beforeColumnsCount

            'DataSet設定
            Call Me.SetDatasetUnchinTariffColItemData(frm, Me._Ds, EventShubetsu)

            '列挿入・列削除後の列数の保持
            frm.lblAddCnt.TextValue = _BeCnt.ToString

            Dim NrsBrcd As String = String.Empty
            Dim TableTp As String = String.Empty
            NrsBrcd = frm.cmbNrsBrCd.SelectedValue.ToString
            TableTp = frm.cmbTableTp.SelectedValue.ToString

            '運賃タリフ(距離刻み/運賃)情報表示
            Call Me._G.SetSpread2(Me._Ds.Tables(LMM550C.TABLE_NM_KYORI), EventShubetsu, NrsBrcd, , , , TableTp)

            '運賃タリフスプレッドのロック制御
            Call Me.SetLockControl(EventShubetsu)

            '運賃タリフスプレッドのフォーカス設定
            frm.sprDetail2.ActiveSheet.SetActiveCell(0, LMM550C.SprColumnIndex2.KYORI_1)

        End If

    End Sub

    ''' <summary>
    ''' 列の挿入・削除(TypeB) 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ColAddDel_B(ByVal frm As LMM550F, ByVal mode As LM.GUI.Win.Spread.LMSpread.ColMode)

        Dim EventShubetsu As LMM550C.EventShubetsu = LMM550C.EventShubetsu.MAIN
        Dim Index As Integer = 0
        If String.IsNullOrEmpty(Me._Frm.lblDefAddCnt.TextValue) = True Then
            Me._Frm.lblDefAddCnt.TextValue = 0.ToString
        End If

        'フォーカスの位置を設定
        Index = frm.sprDetail3.ActiveSheet.ActiveCell.Column.Index

        'フォーカスが重量or個数列の範囲内にある場合のみ処理を行う
        If LMM550C.SprColumnIndex3.KYORI_1 <= Index And Index <= LMM550C.SprColumnIndex3.KYORI_70 Then

            If (CType(1, LMSpread.ColMode)).Equals(mode) = True Then
                _BeCnt = CType(Me._Frm.lblDefAddCnt.TextValue, Integer) + 1
                EventShubetsu = LMM550C.EventShubetsu.COLADD
            End If
            If (CType(-1, LMSpread.ColMode)).Equals(mode) = True Then
                _BeCnt = CType(Me._Frm.lblDefAddCnt.TextValue, Integer) - 1
                EventShubetsu = LMM550C.EventShubetsu.COLDEL
            End If

            '列挿入後の距離刻みの列数が70を超えた場合、もしくは列削除後の重量or個数の列数が1になった場合は処理終了
            If _BeCnt > 70 Then
                _BeCnt = 70   '設定可能な列数に戻す
                Exit Sub
            End If
            If _BeCnt < 1 Then
                _BeCnt = 1   '設定可能な列数に戻す
                Exit Sub
            End If

            '列挿入/列追加
            Dim beforeColumnsCount As Integer = frm.sprDetail3.ActiveSheet.Columns.Count
            frm.sprDetail3.ColumnAddRemove(mode)
            frm.sprDetail3.ActiveSheet.Columns.Count = beforeColumnsCount

            'DataSet設定
            Call Me.SetDatasetUnchinTariffColItemData(frm, Me._Ds, EventShubetsu)

            '列挿入・列削除後の列数の保持
            frm.lblAddCnt.TextValue = _BeCnt.ToString

            Dim NrsBrcd As String = String.Empty
            Dim TableTp As String = String.Empty
            NrsBrcd = frm.cmbNrsBrCd.SelectedValue.ToString
            TableTp = frm.cmbTableTp.SelectedValue.ToString

            '運賃タリフ(距離刻み/運賃)情報表示
            Call Me._G.SetSpread2(Me._Ds.Tables(LMM550C.TABLE_NM_KYORI), EventShubetsu, NrsBrcd, , , , TableTp)

            '運賃タリフスプレッドのロック制御
            Call Me.SetLockControl(EventShubetsu)

            '運賃タリフスプレッドのフォーカス設定
            frm.sprDetail3.ActiveSheet.SetActiveCell(0, LMM550C.SprColumnIndex3.KYORI_1)

        End If

    End Sub

    ''' <summary>
    ''' 列のロック・アンロック 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ColLockUnLock(ByVal frm As LMM550F, ByVal mode As Boolean)

        If LMM550C.SpreadType.A.Equals(_G._SpreadType) Then
            'TypeA
            frm.sprDetail2.SpanColumnLock = mode
        Else
            'TypeB
            frm.sprDetail3.SpanColumnLock = mode
        End If

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM550F_KeyDown(ByVal frm As LMM550F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM550F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM550F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM550F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    ''' <summary>
    ''' セルの編集モード終了イベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    Friend Sub sprDetail3_EditModeOff(ByVal frm As LMM550F, ByVal e As EventArgs)

        With Me._Frm

            'Enterキー判定
            Dim eventFlg As Boolean = True
            Dim rtnResult As Boolean = eventFlg

            'カーソル位置の設定
            Dim objNm As String = frm.ActiveControl.Name()

            '権限チェック
            rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM550C.EventShubetsu.ENTER)

            'カーソル位置チェック
            rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMMControlC.ENTER)

            'エラーの場合、終了
            If rtnResult = False Then
                Exit Sub
            End If

            'Pop起動処理：１件時表示なし
            Me._PopupSkipFlg = False
            Call Me.ShowPopupControl(frm, objNm, LMM550C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

            'メッセージ設定
            Call Me._V.SetBaseMsg()

        End With

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class