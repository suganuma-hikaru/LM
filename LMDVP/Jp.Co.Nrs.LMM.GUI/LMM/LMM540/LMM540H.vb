' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM540H : 棟マスタメンテナンス
'  作  成  者       :  [narita]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM540ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM540H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM540F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM540V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM540G

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
    '''消防情報格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShouboDs As DataTable


    ''' <summary>
    ''' 棟チェックマスタ格納用フィールド
    ''' </summary>
    Private _TouChk As DataTable

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

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
        Me._Frm = New LMM540F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM540", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM540G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMM540V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(Me._Frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(Me._Frm.cmbNrsBrCd, Me._Frm.cmbWare)

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
    Private Sub NewDataEvent(ByVal frm As LMM540F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '画面の入力項目値設定
        Call Me._G.SetItemIniValue(Me._Frm)

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
    Private Sub EditDataEvent(ByVal frm As LMM540F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk() = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '画面の入力項目値設定
        Call Me._G.SetItemIniValue(Me._Frm)

        'DataSet設定(排他チェック)
        Me._Ds = New LMM540DS()
        Call SetDataSetHaitaChk()

        '==========================
        'WSAクラス呼出()
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM540BLF", "HaitaData", Me._Ds)

        'データセットの内容保持
        _Ds = rtnds

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

            ''商品明細マスタ情報表示設定
            'Call Me._G.SetSpreadDtl(Me._DispDt)

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
    Private Sub CopyDataEvent(ByVal frm As LMM540F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.HUKUSHA) = False Then
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

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent(ByVal frm As LMM540F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.SAKUJO) = False Then
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
        '2016.01.06 UMANO 英語化対応START
        Dim str As String() = Split(Me._Frm.FunctionKey.F4ButtonName, "･")
        '2016.01.06 UMANO 英語化対応END
        Select Case Me._Frm.lblSituation.RecordStatus
            Case RecordStatus.DELETE_REC
                '2016.01.06 UMANO 英語化対応START
                msg = str(1)
            Case RecordStatus.NOMAL_REC
                'msg = "削除"
                msg = str(0)
                '2016.01.06 UMANO 英語化対応END
        End Select
        If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Call Me._V.SetBaseMsg() 'メッセージエリアの設定
            Exit Sub
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM540DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM540BLF", "DeleteData", ds)

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

        'メッセージ用コード格納
        Dim Ware As String = frm.cmbWare.SelectedValue.ToString
        Dim TouNo As String = frm.txtTouNo.TextValue
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty) _
                                              , String.Concat("[", frm.lblWare.Text, " = ", Ware, "]" _
                                                                    , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM540C.SprColumnIndex.TOU_NO), " = ", TouNo, "]"
                                                                    )})


        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMM540F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.KENSAKU) = False Then
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
    Private Sub SprCellLeave(ByVal frm As LMM540F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM540F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

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
    Private Sub RowSelection(ByVal frm As LMM540F, ByVal rowNo As Integer)

        Dim sokocd As String = String.Empty
        Dim touno As String = String.Empty
        Dim situno As String = String.Empty

        sokocd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM540G.sprDetailDef.WH_CD.ColNo).Text()
        touno = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM540G.sprDetailDef.TOU_NO.ColNo).Text()

        Dim dt2 As DataTable = Me._ShouboDs

        Dim dtTouChk As DataTable = Me._TouChk

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.DCLICK) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM540G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        '編集部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)

        'SPREAD(明細)初期化
        frm.sprDetail2.CrearSpread()

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread2(dt2, sokocd, touno, situno)

        '棟チェックマスタスプレッド(3種)
        frm.sprDetail4.CrearSpread()
        frm.sprDetail5.CrearSpread()
        frm.sprDetail6.CrearSpread()

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread4(dtTouChk, sokocd, touno, situno)
        Call Me._G.SetSpread5(dtTouChk, sokocd, touno, situno)
        Call Me._G.SetSpread6(dtTouChk, sokocd, touno, situno)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        MyBase.ShowMessage(frm, "G013")

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveTouItemData(ByVal frm As LMM540F, ByVal eventShubetsu As LMM540C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM540C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Return False
        End If

        ''項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._ControlH.EndAction(frm) '終了処理
            Return False
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM540DS()
        Call Me.SetDatasetTouItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveTouItemData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM540BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM540BLF", "UpdateData", ds)
        End Select

        '配下に反映処理
        rtnDs = MyBase.CallWSA("LMM540BLF", "HaikaData", ds)


        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._ControlH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveTouItemData")

        '終了処理
        Call Me._ControlH.EndAction(frm)


        '処理結果メッセージ表示
        Dim Ware As String = frm.cmbWare.SelectedValue.ToString
        Dim TouNo As String = frm.txtTouNo.TextValue.ToUpper()
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", frm.lblWare.Text, " = ", Ware, "]" _
                                                                            , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM540C.SprColumnIndex.TOU_NO), " = ", TouNo, "]"
                                                                           )})

        '2016.01.06 UMANO 英語化対応END

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

        'ディスプレイモードが編集の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveTouItemData(Me._Frm, LMM540C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'Enterキー判定
            Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
            Dim rtnResult As Boolean = eventFlg

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM540C.EventShubetsu.ENTER)

            ''カーソル位置チェック
            'rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM540C.EventShubetsu.ENTER)

            'エラーの場合、終了
            If rtnResult = False Then
                'フォーカス移動処理
                Call Me._ControlH.NextFocusedControl(frm, eventFlg)
                Exit Sub
            End If

            '荷主検索後、1件のみだったらポップアップ画面を表示しない
            Me._PopupSkipFlg = False
            Select Case objNm
                Case frm.txtFctMgr.Name
                    'ユーザーキャッシュから名称取得
                    Call Me.SetReturnFctMgr()

            End Select

            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

            'メッセージ設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(frm, eventFlg)

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM540F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM540DS()
        Call Me.SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(frm, "LMM540BLF", "SelectListData", _FindDs, lc, Nothing, mc)

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
    Private Sub SuccessSelect(ByVal frm As LMM540F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM540C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM540C.TABLE_NM_TOU_SHOBO)

        Dim dt4 As DataTable = ds.Tables(LMM540C.TABLE_NM_TOU_CHK)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread()

        frm.sprDetail4.CrearSpread()
        frm.sprDetail5.CrearSpread()
        frm.sprDetail6.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '取得データ(TOU_SHOBO)をPrivate変数に保持
        Call Me.SetDataSetTouData(dt2)

        '取得データ(M_TOU_CHK)をPrivate変数に保持
        Call Me.SetDataSetTouChkData(dt4)

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
    ''' データセット設定(棟消防情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTouData(ByVal dt As DataTable)

        Me._ShouboDs = dt

    End Sub


    ''' <summary>
    ''' データセット設定(棟チェックマスタ格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTouChkData(ByVal dt As DataTable)

        Me._TouChk = dt

    End Sub


#End Region

#End Region 'イベント定義(一覧)

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMM540C.EventShubetsu, ByVal frm As LMM540F)

        'ディスプレイモード、レコードステータス保存域
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim targetSpr As Spread.LMSpread
        Dim tagetDefColNo As Integer
        Dim tagetUpdFlgColNo As Integer
        Dim tagetSysDelFlgColNo As Integer

        Select Case eventShubetsu

            Case LMM540C.EventShubetsu.INS_T    '行追加

                '処理開始アクション
                _ControlH.StartAction(frm)

                '消防マスタ照会POP表示
                Dim ds As DataSet = Me.ShowPopup(frm, LMM540C.EventShubetsu.INS_T.ToString(), prm)
                Dim dt As DataTable = ds.Tables(LMZ280C.TABLE_NM_OUT)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り値が無い場合は終了
                    'メッセージの表示
                    ShowMessage(frm, "G003")
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Exit Sub
                Else
                    '項目チェック
                    If Me._V.IsRowCheck(eventShubetsu, ds, frm) = False Then
                        '処理終了アクション
                        _ControlH.EndAction(frm)
                        Exit Sub
                    End If
                End If

                '戻り値を棟マスタ消防スプレッドに設定
                Call Me._G.AddSetSpread2(dt)

                '処理終了アクション
                _ControlH.EndAction(frm)

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus()

                'メッセージの表示
                ShowMessage(frm, "G003")

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()


            Case LMM540C.EventShubetsu.DEL_T    '行削除

                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, Nothing, frm) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM540G.sprDetailDef2.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _ControlH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTouShobo(frm.sprDetail2)

                    '棟マスタ消防スプレッドの再描画
                    Me._G.ReSetSpread(frm.sprDetail2)

                    '処理終了アクション
                    _ControlH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus()

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If


            Case LMM540C.EventShubetsu.INS_DOKU,
                 LMM540C.EventShubetsu.INS_KOUATHUGAS,
                 LMM540C.EventShubetsu.INS_YAKUZIHO     '行追加

                Select Case eventShubetsu
                    Case LMM540C.EventShubetsu.INS_DOKU
                        targetSpr = frm.sprDetail4
                        tagetDefColNo = LMM540G.sprDetailDef4.DEF.ColNo
                    Case LMM540C.EventShubetsu.INS_KOUATHUGAS
                        targetSpr = frm.sprDetail5
                        tagetDefColNo = LMM540G.sprDetailDef5.DEF.ColNo
                    Case LMM540C.EventShubetsu.INS_YAKUZIHO
                        targetSpr = frm.sprDetail6
                        tagetDefColNo = LMM540G.sprDetailDef6.DEF.ColNo
                End Select

                '処理開始アクション
                _ControlH.StartAction(frm)

                '項目チェック
                If Me._V.IsTouChkRowCheck(eventShubetsu, frm, targetSpr, tagetDefColNo) = False Then
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Exit Sub
                End If

                '棟チェックマスタスプレッドを設定
                Select Case eventShubetsu
                    Case LMM540C.EventShubetsu.INS_DOKU
                        Call Me._G.AddSetSpread4()
                    Case LMM540C.EventShubetsu.INS_KOUATHUGAS
                        Call Me._G.AddSetSpread5()
                    Case LMM540C.EventShubetsu.INS_YAKUZIHO
                        Call Me._G.AddSetSpread6()
                End Select

                '処理終了アクション
                _ControlH.EndAction(frm)

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus()

                'メッセージの表示
                ShowMessage(frm, "G003")

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()

            Case LMM540C.EventShubetsu.DEL_DOKU,
                 LMM540C.EventShubetsu.DEL_KOUATHUGAS,
                 LMM540C.EventShubetsu.DEL_YAKUZIHO     '行削除

                Select Case eventShubetsu
                    Case LMM540C.EventShubetsu.DEL_DOKU
                        targetSpr = frm.sprDetail4
                        tagetDefColNo = LMM540G.sprDetailDef4.DEF.ColNo
                        tagetUpdFlgColNo = LMM540G.sprDetailDef4.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo
                    Case LMM540C.EventShubetsu.DEL_KOUATHUGAS
                        targetSpr = frm.sprDetail5
                        tagetDefColNo = LMM540G.sprDetailDef5.DEF.ColNo
                        tagetUpdFlgColNo = LMM540G.sprDetailDef5.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo
                    Case LMM540C.EventShubetsu.DEL_YAKUZIHO
                        targetSpr = frm.sprDetail6
                        tagetDefColNo = LMM540G.sprDetailDef6.DEF.ColNo
                        tagetUpdFlgColNo = LMM540G.sprDetailDef6.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo
                End Select

                '項目チェック
                If Me._V.IsTouChkRowCheck(eventShubetsu, frm, targetSpr, tagetDefColNo) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(targetSpr.ActiveSheet, tagetDefColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _ControlH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTouChk(targetSpr, tagetDefColNo, tagetUpdFlgColNo, tagetSysDelFlgColNo)

                    '棟チェックマスタスプレッドの再描画
                    Me._G.ReSetTouChkSpread(targetSpr)

                    '処理終了アクション
                    _ControlH.EndAction(frm)

                    '画面全ロックの解除
                    Me.UnLockedControls(frm)

                    'ファンクションキーの設定
                    Call Me._G.SetFunctionKey()

                    'フォーカスの設定
                    Call Me._G.SetFoucus()

                    'メッセージの表示
                    ShowMessage(frm, "G003")

                    'カーソルを元に戻す
                    Cursor.Current = Cursors.Default()

                End If

            Case LMM540C.EventShubetsu.MASTER     'マスタ参照
                '******************「マスタ参照」******************'

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                Select Case objNm

               
                    Case Else
                        'ポップ対象外のテキストの場合
                        MyBase.ShowMessage(frm, "G005")

                        'カーソルを元に戻す
                        Cursor.Current = Cursors.Default()

                End Select

            Case LMM540C.EventShubetsu.IKKATU_TOUROKU
                '******************「一括登録」******************'

                '権限チェック
                If Me._V.IsAuthorityChk(eventShubetsu) = False Then
                    Call Me._ControlH.EndAction(frm)  '終了処理
                    Exit Sub
                End If

                '処理開始アクション
                _ControlH.StartAction(frm)

                'キャッシュを最新化する
                MyBase.LMCacheMasterData(LMConst.CacheTBL.CUST)

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(frm.sprDetail.ActiveSheet, LMM540G.sprDetailDef3.DEF.ColNo)

                Dim ds As DataSet = New LMM540DS()

                'チェックされた行がなかった場合、エラーを出力し中断とする。
                If arr.Count = 0 Then
                    '対象行を選択してください。
                    MyBase.ShowMessage(frm, "E009")
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Return

                End If


                'データセット設定中に他社情報が存在した、または重複データが存在した場合はEXCELでエラーを出力し、
                '処理を中断する。
                If MyBase.IsMessageStoreExist Then

                    'EXCEL起動 
                    MyBase.MessageStoreDownload(True)
                    MyBase.ShowMessage(frm, "E235")

                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Return

                End If



                '処理終了アクション
                _ControlH.EndAction(frm)

                'モード・ステータスの設定
                Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

                '画面の入力項目/ファンクションキーの制御
                Call Me._G.UnLockedForm()

                '画面全ロックの解除
                Me.UnLockedControls(frm)

                ''ファンクションキーの設定
                'Call Me._G.SetFunctionKey()

                'フォーカスの設定
                Call Me._G.SetFoucus()

                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()



        End Select

    End Sub

    ''' <summary>
    ''' 保安監督者名取得
    ''' </summary>
    ''' <returns></returns>
    Private Function SetReturnFctMgr() As Boolean

        With Me._Frm

            Dim userDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtFctMgr.TextValue, "'"))

            .lblFctMgrNm.TextValue = String.Empty
            If 0 < userDr.Length Then
                .lblFctMgrNm.TextValue = userDr(0).Item("USER_NM").ToString
            End If

        End With

        Return True

    End Function

#Region "PopUp"

    ''' <summary>
    ''' POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMM540F, ByVal objNM As String, ByVal prm As LMFormData) As DataSet

        Dim value As String = String.Empty

        Select Case objNM

            Case LMM540C.EventShubetsu.INS_T.ToString()         '行追加

                Dim prmDs As DataSet = New LMZ280DS()
                Dim row As DataRow = prmDs.Tables(LMZ280C.TABLE_NM_IN).NewRow
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("SELECT_PLURAL_FLG") = LMConst.FLG.ON   '複数選択可
                prmDs.Tables(LMZ280C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ280", prm)

                '2017/10/26 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            Case LMM540C.EventShubetsu.INS_EXP_T.ToString()         '行追加

                Dim prmDs As DataSet = New LMZ260DS()
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = Convert.ToString(frm.cmbNrsBrCd.SelectedValue)
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_M
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

                '2017/10/26 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        End Select

        Return prm.ParamDataSet

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData()

        Dim dr As DataRow = Me._FindDs.Tables(LMM540C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprDetail.ActiveSheet

            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.NRS_BR_NM.ColNo))
            dr.Item("WH_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.SOKO.ColNo))
            dr.Item("TOU_NO") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.TOU_NO.ColNo))
            dr.Item("TOU_NM") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.TOU_NM.ColNo))
            dr.Item("SOKO_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.SOKO_KB_NM.ColNo))
            dr.Item("HOZEI_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.HOZEI_KB_NM.ColNo))
            dr.Item("HOKAN_KANO_M3") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.HOKAN_KANO_M3.ColNo))
            dr.Item("HOKAN_KANO_KG") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.HOKAN_KANO_KG.ColNo))

            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM540G.sprDetailDef.NRS_BR_NM.ColNo))

            Me._FindDs.Tables(LMM540C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim dr As DataRow = Me._Ds.Tables(LMM540C.TABLE_NM_IN).NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            'スキーマ名取得用
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Me._Ds.Tables(LMM540C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub


    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetTouItemData(ByVal frm As LMM540F, ByVal ds As DataSet)

        With frm

            Dim dsSum As DataSet = New LMM540DS
            Dim dt1 As DataTable = dsSum.Tables(LMM540C.TABLE_NM_IN)
            Dim dr1 As DataRow = dt1.NewRow()

            dr1.Item("WH_CD") = .cmbWare.SelectedValue
            dr1.Item("TOU_NO") = .txtTouNo.TextValue.ToUpper()
            dr1.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            dt1.Rows.Add(dr1)

            Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(frm, "LMM540BLF", "SelectListDataSum", dsSum, Integer.MaxValue)

            Dim drSum As DataRow = rtnDs.Tables(LMM540C.TABLE_NM_SUM).Rows(0)

            '編集部の値（棟情報）をデータセットに設定
            Dim dr As DataRow = ds.Tables(LMM540C.TABLE_NM_IN).NewRow()

            dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue.ToUpper()
            dr.Item("TOU_NM") = .txtTouNm.TextValue.Trim
            dr.Item("SOKO_KB") = .cmbSokoKbn.SelectedValue
            dr.Item("HOZEI_KB") = .cmbHozeiKbn.SelectedValue
            dr.Item("CHOZO_MAX_QTY") = .numChozoMaxQty.Value

            Dim baisu As Decimal = 0

            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim v As String = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540G.sprDetailDef2.BAISU.ColNo))
                If IsNumeric(v) Then
                    baisu += Convert.ToDecimal(v)
                End If
            Next
            dr.Item("CHOZO_MAX_BAISU") = baisu.ToString

            dr.Item("HOKAN_KANO_M3") = If(String.IsNullOrEmpty(drSum.Item("HOKAN_KANO_M3").ToString), "0", drSum.Item("HOKAN_KANO_M3"))
            dr.Item("HOKAN_KANO_KG") = If(String.IsNullOrEmpty(drSum.Item("HOKAN_KANO_KG").ToString), "0", drSum.Item("HOKAN_KANO_KG"))
            dr.Item("ONDO_CTL_KB") = .cmbOndoKbn.SelectedValue
            dr.Item("AREA") = .numArea.Value
            dr.Item("FCT_MGR") = .txtFctMgr.TextValue.Trim
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr.Item("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim

            'スキーマ名取得用
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            ds.Tables(LMM540C.TABLE_NM_IN).Rows.Add(dr)


            '棟消防Spread情報をデータセットに設定
            sprMax = .sprDetail2.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr2 As DataRow = ds.Tables(LMM540C.TABLE_NM_TOU_SHOBO).NewRow()

                dr2.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr2.Item("WH_CD") = .cmbWare.SelectedValue
                dr2.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr2.Item("SHOBO_CD") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540G.sprDetailDef2.SHOBO_CD.ColNo))
                dr2.Item("WH_KYOKA_DATE") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540G.sprDetailDef2.WH_KYOKA_DATE.ColNo))
                dr2.Item("BAISU") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540G.sprDetailDef2.BAISU.ColNo))
                dr2.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540G.sprDetailDef2.UPD_FLG.ColNo))
                dr2.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM540G.sprDetailDef2.SYS_DEL_FLG_T.ColNo))

                dr2.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM540C.TABLE_NM_TOU_SHOBO).Rows.Add(dr2)

            Next


            '棟チェックマスタSpread情報をデータセットに設定
            '毒劇情報
            sprMax = .sprDetail4.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr4 As DataRow = ds.Tables(LMM540C.TABLE_NM_TOU_CHK).NewRow()

                dr4.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr4.Item("WH_CD") = .cmbWare.SelectedValue
                dr4.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr4.Item("KBN_GROUP_CD") = LMM540C.M_Z_KBN_DOKUGEKI
                dr4.Item("KBN_CD") = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM540G.sprDetailDef4.DOKU_KB.ColNo))
                dr4.Item("KBN_NM1") = .sprDetail4.ActiveSheet.Cells(i, LMM540G.sprDetailDef4.DOKU_KB.ColNo).Text
                dr4.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM540G.sprDetailDef4.UPD_FLG.ColNo))
                dr4.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM540G.sprDetailDef4.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr4.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM540C.TABLE_NM_TOU_CHK).Rows.Add(dr4)

            Next

            '毒劇区分を設定
            Dim DokuKbn As String = String.Empty    'ADD 2021/10/06
            Dim id As String = String.Empty
            Dim tempId As String = String.Empty
            For i As Integer = 0 To sprMax

                id = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM540G.sprDetailDef4.DOKU_KB.ColNo))

                '毒劇区分を 特定毒物 > 毒物 > 劇物 > なしで設定
                If String.IsNullOrEmpty(id) = False Then

                    If LMM540C.M_Z_KBN_DOKUGEKI_TOKU.Equals(id) Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_TOKU
                        Exit For '最上位なので、以降確認不要

                    ElseIf LMM540C.M_Z_KBN_DOKUGEKI_DOKU.Equals(id) _
                        And tempId.Equals(LMM540C.M_Z_KBN_DOKUGEKI_TOKU) = False Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_DOKU

                    ElseIf LMM540C.M_Z_KBN_DOKUGEKI_GEKI.Equals(id) _
                        And tempId.Equals(LMM540C.M_Z_KBN_DOKUGEKI_TOKU) = False _
                        And tempId.Equals(LMM540C.M_Z_KBN_DOKUGEKI_DOKU) = False Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_GEKI

                    ElseIf LMM540C.M_Z_KBN_DOKUGEKI_NASI.Equals(id) _
                        And String.IsNullOrEmpty(tempId) Then

                        tempId = LMM540C.M_Z_KBN_DOKUGEKI_NASI

                    End If

                End If

            Next
            DokuKbn = tempId

            '毒劇情報 配下情報へ反映にチェックなしの時、毒区分の更新はしない
            If .chkDoku.Checked.Equals(False) Then
                DokuKbn = "XX"  '更新なし
            End If

            ds.Tables(LMM540C.TABLE_NM_IN).Rows(0).Item("DOKU_KB") = DokuKbn.ToString


            ' 高圧ガス情報
            sprMax = .sprDetail5.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr5 As DataRow = ds.Tables(LMM540C.TABLE_NM_TOU_CHK).NewRow()

                dr5.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr5.Item("WH_CD") = .cmbWare.SelectedValue
                dr5.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr5.Item("KBN_GROUP_CD") = LMM540C.M_Z_KBN_KOUATHUGAS
                dr5.Item("KBN_CD") = _ControlG.GetCellValue(.sprDetail5.ActiveSheet.Cells(i, LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo))
                dr5.Item("KBN_NM1") = .sprDetail5.ActiveSheet.Cells(i, LMM540G.sprDetailDef5.KOUATHUGAS_KB.ColNo).Text
                dr5.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail5.ActiveSheet.Cells(i, LMM540G.sprDetailDef5.UPD_FLG.ColNo))
                dr5.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail5.ActiveSheet.Cells(i, LMM540G.sprDetailDef5.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr5.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM540C.TABLE_NM_TOU_CHK).Rows.Add(dr5)

            Next

            '薬機法情報
            sprMax = .sprDetail6.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr6 As DataRow = ds.Tables(LMM540C.TABLE_NM_TOU_CHK).NewRow()

                dr6.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr6.Item("WH_CD") = .cmbWare.SelectedValue
                dr6.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr6.Item("KBN_GROUP_CD") = LMM540C.M_Z_KBN_YAKUZIHO
                dr6.Item("KBN_CD") = _ControlG.GetCellValue(.sprDetail6.ActiveSheet.Cells(i, LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo))
                dr6.Item("KBN_NM1") = .sprDetail6.ActiveSheet.Cells(i, LMM540G.sprDetailDef6.YAKUZIHO_KB.ColNo).Text
                dr6.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail6.ActiveSheet.Cells(i, LMM540G.sprDetailDef6.UPD_FLG.ColNo))
                dr6.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail6.ActiveSheet.Cells(i, LMM540G.sprDetailDef6.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr6.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM540C.TABLE_NM_TOU_CHK).Rows.Add(dr6)

            Next

            Dim dr7 As DataRow = ds.Tables(LMM540C.TABLE_NM_IN_HAIKA_CHECK).NewRow()

            dr7.Item("SHOBO_CHK") = If(.chkShobo.Checked, "1", "0")
            dr7.Item("DOKU_CHK") = If(.chkDoku.Checked, "1", "0")
            dr7.Item("KOUATHUGAS_CHK") = If(.chkKouathugas.Checked, "1", "0")
            dr7.Item("YAKKIHO_CHK") = If(.chkYakkiho.Checked, "1", "0")


            ds.Tables(LMM540C.TABLE_NM_IN_HAIKA_CHECK).Rows.Add(dr7)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetTouExpItemData(ByVal frm As LMM540F, ByVal ds As DataSet, ByVal CheckListIndexs As ArrayList)

        With frm

            '編集部の値（棟情報）をデータセットに設定
            Dim dr As DataRow = ds.Tables(LMM540C.TABLE_NM_IN).NewRow()

            dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue.ToUpper()
            dr.Item("TOU_NM") = .txtTouNm.TextValue.Trim
            dr.Item("SOKO_KB") = .cmbSokoKbn.SelectedValue
            dr.Item("HOZEI_KB") = .cmbHozeiKbn.SelectedValue
            dr.Item("CHOZO_MAX_QTY") = .numChozoMaxQty.Value
            dr.Item("CHOZO_MAX_BAISU") = .numChozoMaxBaisu.Value
            dr.Item("ONDO_CTL_KB") = .cmbOndoKbn.SelectedValue
            dr.Item("AREA") = .numArea.Value
            dr.Item("FCT_MGR") = .txtFctMgr.TextValue.Trim
            dr.Item("FCT_MGR_NM") = .lblFctMgrNm.TextValue.Trim
            dr.Item("HOKAN_KANO_M3") = .numHokanKanoM3.Value
            dr.Item("HOKAN_KANO_KG") = .numHokanKanoKg.Value
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr.Item("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()


            ds.Tables(LMM540C.TABLE_NM_IN).Rows.Add(dr)



        End With

    End Sub

    ''' <summary>
    ''' データセット設定(削除復活処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM540C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

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

            ds.Tables(LMM540C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから荷主存在確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function IsExistCachedCust(ByVal nrsBrCd As String,
                                       ByVal custCdL As String) As Boolean

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat(
                                                                          "NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        Return 0 < dr.Length

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し(複写)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CopyDataEvent")

        Me.CopyDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CopyDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し(削除)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除処理
        Me.DeleteDataEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub


    ''' <summary>
    ''' F10押下時処理呼び出し(初期荷主変更処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        '一件時表示有り
        Me._PopupSkipFlg = True

        '「マスタ参照」処理
        Call Me.ActionControl(LMM540C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub


    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveTouItemData")

        Me.SaveTouItemData(frm, LMM540C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveTouItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM540F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM540F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_SHOBO_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_SHOBO_Click")

        '「行追加」処理
        Me.ActionControl(LMM540C.EventShubetsu.INS_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_SHOBO_Click")

    End Sub

    '2017/10/24 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 行追加（申請外の商品保管ルール） 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_EXP_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_EXP_Click")

        '「行追加」処理
        Me.ActionControl(LMM540C.EventShubetsu.INS_EXP_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_EXP_Click")

    End Sub
    '2017/10/24 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 毒劇情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_DOKU_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_DOKU_Click")

        '「行追加」処理
        Me.ActionControl(LMM540C.EventShubetsu.INS_DOKU, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_DOKU_Click")

    End Sub

    ''' <summary>
    ''' 高圧ガス情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_KOUATHUGAS_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_KOUATHUGAS_Click")

        '「行追加」処理
        Me.ActionControl(LMM540C.EventShubetsu.INS_KOUATHUGAS, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_KOUATHUGAS_Click")

    End Sub

    ''' <summary>
    ''' 薬事法情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_YAKUZIHO_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_YAKUZIHO_Click")

        '「行追加」処理
        Me.ActionControl(LMM540C.EventShubetsu.INS_YAKUZIHO, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_YAKUZIHO_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_SHOBO_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_SHOBO_Click")

        '「行削除」処理
        Me.ActionControl(LMM540C.EventShubetsu.DEL_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_SHOBO_Click")

    End Sub

    '2017/10/26 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 行削除（申請外の商品保管ルール） 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_EXP_SHOBO_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_EXP_SHOBO_Click")

        '「行削除」処理
        Me.ActionControl(LMM540C.EventShubetsu.DEL_EXP_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_EXP_SHOBO_Click")

    End Sub

    ''' <summary>
    ''' 一括登録　押下処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnIkkatuTouroku_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnIkkatuTouroku_Click")

        '「一括登録」処理
        Me.ActionControl(LMM540C.EventShubetsu.IKKATU_TOUROKU, frm)

        Logger.StartLog(Me.GetType.Name, "btnIkkatuTouroku_Click")

    End Sub
    '2017/10/26 棟マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 毒劇情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_DOKU_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_DOKU_Click")

        '「行削除」処理
        Me.ActionControl(LMM540C.EventShubetsu.DEL_DOKU, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_DOKU_Click")

    End Sub

    ''' <summary>
    ''' 高圧ガス情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_KOUATHUGAS_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_KOUATHUGAS_Click")

        '「行削除」処理
        Me.ActionControl(LMM540C.EventShubetsu.DEL_KOUATHUGAS, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_KOUATHUGAS_Click")

    End Sub

    ''' <summary>
    ''' 薬事法情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_YAKUZIHO_Click(ByRef frm As LMM540F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_YAKUZIHO_Click")

        '「行削除」処理
        Me.ActionControl(LMM540C.EventShubetsu.DEL_YAKUZIHO, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_YAKUZIHO_Click")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM540F_KeyDown(ByVal frm As LMM540F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM540F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM540F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM540F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class