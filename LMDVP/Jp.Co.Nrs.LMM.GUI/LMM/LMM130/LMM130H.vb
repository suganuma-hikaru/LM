' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM130H : 棟室マスタメンテナンス
'  作  成  者       :  [kishi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base.GUI

''' <summary>
''' LMM130ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMM130H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM130F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMM130V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMM130G

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

    Private _TouSituExpDs As DataTable

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ格納用フィールド
    ''' </summary>
    Private _TouSituZoneChk As DataTable

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>    
    Private _Ds As DataSet

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 倉庫自社他社判定用データセット
    ''' </summary>
    Private _dsSokoJT As DataSet = Nothing

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
        Me._Frm = New LMM130F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMMControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMMControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMMControlH("LMM130", Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMM130G(Me, Me._Frm, Me._ControlG)

        'Validateクラスの設定
        Me._V = New LMM130V(Me, Me._Frm, Me._ControlV)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(Me._Frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(Me._Frm.cmbNrsBrCd, Me._Frm.cmbWare)

        '倉庫自社他社判定用データセット設定
        Me.GetSokoJT()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        '2011/08/11 福田 共通動作(右セル移動不可) スタート
        'Enter押下イベントの設定
        'Call Me._ControlH.SetEnterEvent(Me._Frm)
        '2011/08/11 福田 共通動作(右セル移動不可) エンド

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
    Private Sub NewDataEvent(ByVal frm As LMM130F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        '画面の入力項目値設定
        Call Me._G.SetItemIniValue(Me._Frm)
        '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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
    Private Sub EditDataEvent(ByVal frm As LMM130F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '編集ボタン押下時チェック
        If Me._V.IsHenshuChk() = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Exit Sub
        End If

        '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        '画面の入力項目値設定
        Call Me._G.SetItemIniValue(Me._Frm)
        '2017/10/27 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        'DataSet設定(排他チェック)
        Me._Ds = New LMM130DS()
        Call SetDataSetHaitaChk()

        '==========================
        'WSAクラス呼出()
        '==========================
        Dim rtnds As DataSet = MyBase.CallWSA("LMM130BLF", "HaitaData", Me._Ds)

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
    Private Sub CopyDataEvent(ByVal frm As LMM130F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.HUKUSHA) = False Then
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

        ''商品明細マスタ情報をSpreadに設定
        'Call Me._G.SetSpreadDtl(Me._DispDt)

        If Me._Frm.numAreaRentHokanAmo.ReadOnly() Then
            ' 坪貸し保管料 編集権限のない入力者の場合は値をゼロクリアする。
            Me._Frm.numAreaRentHokanAmo.Value = 0
        End If

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
    Private Sub DeleteDataEvent(ByVal frm As LMM130F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.SAKUJO) = False Then
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
                'msg = "復活"
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
        Dim ds As DataSet = New LMM130DS()
        Call Me.SetDatasetDelData(ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理(排他処理)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMM130BLF", "DeleteData", ds)

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
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.TOU_SITU_SHOBO)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TOU_SITU_SHOBO_GRP)

        'メッセージ用コード格納
        Dim Ware As String = frm.cmbWare.SelectedValue.ToString
        Dim TouNo As String = frm.txtTouNo.TextValue
        Dim SituNo As String = frm.txtSituNo.TextValue
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty) _
        '                                      , String.Concat("[", frm.lblWare.Text, " = ", Ware, "]" _
        '                                                            , "[", LMM130C.TOU, " = ", TouNo, "]" _
        '                                                            , "[", LMM130C.SHITU, " = ", SituNo, "]")})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F4ButtonName.Replace("　", String.Empty) _
                                              , String.Concat("[", frm.lblWare.Text, " = ", Ware, "]" _
                                                                    , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.TOU_NO), " = ", TouNo, "]" _
                                                                    , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.SITU_NO), " = ", SituNo, "]")})

        '2016.01.06 UMANO 英語化対応END

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
    Private Sub SelectListEvent(ByVal frm As LMM130F)

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.KENSAKU) = False Then
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
    Private Sub SprCellLeave(ByVal frm As LMM130F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

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
    Private Sub SprCellSelect(ByVal frm As LMM130F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
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
    Private Sub RowSelection(ByVal frm As LMM130F, ByVal rowNo As Integer)

        Dim sokocd As String = String.Empty
        Dim touno As String = String.Empty
        Dim situno As String = String.Empty

        sokocd = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM130G.sprDetailDef.WH_CD.ColNo).Text()
        touno = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM130G.sprDetailDef.TOU_NO.ColNo).Text()
        situno = frm.sprDetail.ActiveSheet.Cells(rowNo, LMM130G.sprDetailDef.SITU_NO.ColNo).Text()

        Dim dt2 As DataTable = Me._ShouboDs
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        Dim dt3 As DataTable = Me._TouSituExpDs
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        Dim dtTouSituZoneChk As DataTable = Me._TouSituZoneChk

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.DCLICK) = False Then
            Call Me._ControlH.EndAction(frm) '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMM130G.sprDetailDef.SYS_DEL_FLG.ColNo))) = True Then
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

        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 

        'SPREAD(明細)(申請外の商品保管ルール)初期化
        frm.sprDetail3.CrearSpread()

        'SPREAD(明細(申請外の商品保管ルール))へデータを移動
        Call Me._G.SetSpread3(dt3, sokocd, touno, situno)

        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        '棟室ゾーンチェックマスタスプレッド(3種)
        frm.sprDetail4.CrearSpread()
        frm.sprDetail5.CrearSpread()
        frm.sprDetail6.CrearSpread()

        'SPREAD(明細)へデータを移動
        Call Me._G.SetSpread4(dtTouSituZoneChk, sokocd, touno, situno)
        Call Me._G.SetSpread5(dtTouSituZoneChk, sokocd, touno, situno)
        Call Me._G.SetSpread6(dtTouSituZoneChk, sokocd, touno, situno)

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
    Private Function SaveTouSituItemData(ByVal frm As LMM130F, ByVal eventShubetsu As LMM130C.EventShubetsu) As Boolean

        '処理開始アクション
        Call Me._ControlH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMM130C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(frm)  '終了処理
            Return False
        End If

        ''項目チェック
        If Me._V.IsSaveInputChk() = False Then
            Call Me._ControlH.EndAction(frm) '終了処理
            Return False
        End If

        'チェックで問題がなく他社倉庫情報が入力不可状態であれば内容をクリアする（編集中はクリア処理を行っていないため）
        If Not Me._G.IsTasyaInfo() Then
            frm.txtTasyaWhNm.TextValue = String.Empty
            frm.txtTasyaZip.TextValue = String.Empty
            frm.txtTasyaAd1.TextValue = String.Empty
            frm.txtTasyaAd2.TextValue = String.Empty
            frm.txtTasyaAd3.TextValue = String.Empty
        End If

        'DataSet設定
        Dim ds As DataSet = New LMM130DS()
        Call Me.SetDatasetTouSituItemData(frm, ds)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveTouSituItemData")

        LMUserInfoManager.GetNrsBrCd()

        '==========================
        'WSAクラス呼出
        '==========================
        'レコードステータスの判定
        Dim rtnDs As DataSet

        Select Case frm.lblSituation.RecordStatus
            Case RecordStatus.NEW_REC, RecordStatus.COPY_REC
                '新規登録処理
                rtnDs = MyBase.CallWSA("LMM130BLF", "InsertData", ds)
            Case Else
                '更新処理(排他処理)
                rtnDs = MyBase.CallWSA("LMM130BLF", "UpdateData", ds)
        End Select

        'メッセージコードの判定
        If Me.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._ControlH.EndAction(frm)  '終了処理
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveTouSituItemData")

        '終了処理
        Call Me._ControlH.EndAction(frm)

        'キャッシュ最新化
        'MyBase.LMCacheMasterData(LMConst.CacheTBL.TOU_SITU_SHOBO)
        MyBase.LMCacheMasterData(LMConst.CacheTBL.TOU_SITU_SHOBO_GRP)

        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        '申請外の商品保管ルール、内部保持データセットの内容を更新する。
        ds.Tables("LMM130IN").Rows.Clear()
        ds.Tables("LMM130IN").Rows.Add(ds.Tables("LMM130IN").NewRow)
        ds = MyBase.CallWSA("LMM130BLF", "SelectListData3", ds)
        Me._TouSituExpDs = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP)
        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        '処理結果メッセージ表示
        Dim Ware As String = frm.cmbWare.SelectedValue.ToString
        Dim TouNo As String = frm.txtTouNo.TextValue.ToUpper()
        Dim SituNo As String = frm.txtSituNo.TextValue.ToUpper()
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
        '                                      , String.Concat("[", frm.lblWare.Text, " = ", Ware, "]" _
        '                                                            , "[", LMM130C.TOU, " = ", TouNo, "]" _
        '                                                            , "[", LMM130C.SHITU, " = ", SituNo, "]")})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty) _
                                                      , String.Concat("[", frm.lblWare.Text, " = ", Ware, "]" _
                                                                            , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.TOU_NO), " = ", TouNo, "]" _
                                                                            , "[", frm.sprDetail.ActiveSheet.GetColumnLabel(0, LMM130C.SprColumnIndex.SITU_NO), " = ", SituNo, "]")})

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

                If Me.SaveTouSituItemData(Me._Frm, LMM130C.EventShubetsu.TOJIRU) = False Then

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
    Private Sub EnterAction(ByVal frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        With Me._Frm

            'Enterキー判定
            Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
            Dim rtnResult As Boolean = eventFlg

            'カーソル位置の設定
            Dim objNm As String = .ActiveControl.Name()

            '権限チェック
            rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMM130C.EventShubetsu.ENTER)

            ''カーソル位置チェック
            'rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMM130C.EventShubetsu.ENTER)

            'エラーの場合、終了
            If rtnResult = False Then
                'フォーカス移動処理
                Call Me._ControlH.NextFocusedControl(frm, eventFlg)
                Exit Sub
            End If

            '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
            '荷主検索後、1件のみだったらポップアップ画面を表示しない
            Me._PopupSkipFlg = False
            Select Case objNm
                Case frm.txtCustCD.Name
                    Dim prm As LMFormData = New LMFormData()
                    prm.SkipFlg = _PopupSkipFlg
                    Dim ds As DataSet = Me.ShowPopup(frm, objNm, prm)
                    Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_OUT)

                    If prm.ReturnFlg = False Then
                        '戻り値が無い場合は終了
                        'メッセージの表示
                        ShowMessage(frm, "G003")
                        '処理終了アクション
                        _ControlH.EndAction(frm)
                        Exit Sub
                    Else
                        '荷主コード（大）と荷主名を設定する
                        With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                            frm.txtCustCD.TextValue = .Item("CUST_CD_L").ToString()       '荷主コード（大）
                            frm.lblCustNM.TextValue = .Item("CUST_NM_L").ToString()       '荷主名
                        End With
                    End If
                Case frm.txtUserCd.Name
                    Call Me.SetReturnUser(frm.txtUserCd, frm.lblUserNm)

                Case frm.txtUserCdSub.Name
                    Call Me.SetReturnUser(frm.txtUserCdSub, frm.lblUserNmSub)

                Case frm.txtFctMgr.Name
                    'ユーザーキャッシュから名称取得
                    Call Me.SetReturnFctMgr()

            End Select
            '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

            ''Pop起動処理
            'Call Me.ShowPopupControl(frm, objNm, LMM010C.EventShubetsu.ENTER)

            '処理終了アクション
            Call Me._ControlH.EndAction(frm)

            'メッセージ設定
            Call Me._V.SetBaseMsg()

            'フォーカス移動処理
            Call Me._ControlH.NextFocusedControl(frm, eventFlg)

        End With

    End Sub

    ''' <summary>
    ''' 倉庫自社他社判定用データセット設定
    ''' </summary>
    Private Sub GetSokoJT()

        _dsSokoJT = New LMM130DS()

        Dim dt As DataTable = _dsSokoJT.Tables("LMM130_SOKO_JT")
        Dim dr As DataRow = dt.NewRow()
        dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
        dt.Rows.Add(dr)
        _dsSokoJT = MyBase.CallWSA("LMM130BLF", "SelectSokoJT", _dsSokoJT)

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMM130F, ByVal clearflg As Integer)

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
        Me._FindDs = New LMM130DS()
        Call Me.SetDataSetInData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(frm, "LMM130BLF", "SelectListData", _FindDs, lc, Nothing, mc)

        'Dim rtnDs As DataSet = Me._ControlH.CallWSAAction(frm, _
        '                                         "LMM130BLF", "SelectListData", _FindDs _
        '                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
        '                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
        '                                  , , Convert.ToInt32(Convert.ToDouble( _
        '                                     MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
        '                                     .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

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
    Private Sub SuccessSelect(ByVal frm As LMM130F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMM130C.TABLE_NM_OUT)
        Dim dt2 As DataTable = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_SHOBO)
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        Dim dt3 As DataTable = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP)
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        Dim dt4 As DataTable = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'SPREAD(表示行・明細)初期化
        frm.sprDetail.CrearSpread()
        frm.sprDetail2.CrearSpread()
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        frm.sprDetail3.CrearSpread()
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        frm.sprDetail4.CrearSpread()
        frm.sprDetail5.CrearSpread()
        frm.sprDetail6.CrearSpread()

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '取得データ(TOU_SITU_SHOBO)をPrivate変数に保持
        Call Me.SetDataSetTouSituData(dt2)
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
        Call Me.SetDataSetTouSituExpData(dt3)
        '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

        '取得データ(M_TOU_SITU_ZONE_CHK)をPrivate変数に保持
        Call Me.SetDataSetTouSituZoneChkData(dt4)

        '取得件数設定
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

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' データセット設定(棟室消防情報格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTouSituData(ByVal dt As DataTable)

        Me._ShouboDs = dt

    End Sub

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' データセット設定(棟室申請外商品保管ルール)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTouSituExpData(ByVal dt As DataTable)

        Me._TouSituExpDs = dt

    End Sub
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' データセット設定(棟室ゾーンチェックマスタ格納)
    ''' </summary>    
    ''' <param name="dt">このハンドラクラスに紐づくデータテーブル</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetTouSituZoneChkData(ByVal dt As DataTable)

        Me._TouSituZoneChk = dt

    End Sub


#End Region

#End Region 'イベント定義(一覧)

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMM130C.EventShubetsu, ByVal frm As LMM130F)

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

            Case LMM130C.EventShubetsu.INS_T    '行追加

                '処理開始アクション
                _ControlH.StartAction(frm)

                '消防マスタ照会POP表示
                Dim ds As DataSet = Me.ShowPopup(frm, LMM130C.EventShubetsu.INS_T.ToString(), prm)
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

                '戻り値を棟室マスタ消防スプレッドに設定
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

                '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            Case LMM130C.EventShubetsu.INS_EXP_T

                '処理開始アクション
                _ControlH.StartAction(frm)

                '荷主マスタ照会POP表示
                Dim ds As DataSet = Me.ShowPopup(frm, LMM130C.EventShubetsu.INS_EXP_T.ToString(), prm)
                Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_OUT)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り値が無い場合は終了
                    'メッセージの表示
                    ShowMessage(frm, "G003")
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Return
                Else
                    '項目チェック
                    If Me._V.IsExpRowCheck(eventShubetsu, frm) = False Then
                        '処理終了アクション
                        _ControlH.EndAction(frm)
                        Return
                    End If
                End If

                '戻り値を申請外の商品保管ルールスプレッドに設定
                Call Me._G.AddSetSpread3(dt)

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

                '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

            Case LMM130C.EventShubetsu.DEL_T    '行削除

                '項目チェック
                If Me._V.IsRowCheck(eventShubetsu, Nothing, frm) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(frm.sprDetail2.ActiveSheet, LMM130G.sprDetailDef2.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _ControlH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTouSituShobo(frm.sprDetail2)

                    '棟室マスタ消防スプレッドの再描画
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

                '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            Case LMM130C.EventShubetsu.DEL_EXP_T    '行削除

                '権限チェック
                If Me._V.IsAuthorityChk(eventShubetsu) = False Then
                    Call Me._ControlH.EndAction(frm)  '終了処理
                    Exit Sub
                End If

                '項目チェック
                If Me._V.IsExpRowCheck(eventShubetsu, frm) = False Then
                    Return
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(frm.sprDetail3.ActiveSheet, LMM130G.sprDetailDef3.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _ControlH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTouSituExp(frm.sprDetail3)

                    '棟室マスタ消防スプレッドの再描画
                    Me._G.ReSetExpSpread(frm.sprDetail3)

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

            Case LMM130C.EventShubetsu.INS_DOKU,
                 LMM130C.EventShubetsu.INS_KOUATHUGAS,
                 LMM130C.EventShubetsu.INS_YAKUZIHO     '行追加

                Select Case eventShubetsu
                    Case LMM130C.EventShubetsu.INS_DOKU
                        targetSpr = frm.sprDetail4
                        tagetDefColNo = LMM130G.sprDetailDef4.DEF.ColNo
                    Case LMM130C.EventShubetsu.INS_KOUATHUGAS
                        targetSpr = frm.sprDetail5
                        tagetDefColNo = LMM130G.sprDetailDef5.DEF.ColNo
                    Case LMM130C.EventShubetsu.INS_YAKUZIHO
                        targetSpr = frm.sprDetail6
                        tagetDefColNo = LMM130G.sprDetailDef6.DEF.ColNo
                End Select

                '処理開始アクション
                _ControlH.StartAction(frm)

                '項目チェック
                If Me._V.IsTouSituZoneChkRowCheck(eventShubetsu, frm, targetSpr, tagetDefColNo) = False Then
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Exit Sub
                End If

                '棟室ゾーンチェックマスタスプレッドを設定
                Select Case eventShubetsu
                    Case LMM130C.EventShubetsu.INS_DOKU
                        Call Me._G.AddSetSpread4()
                    Case LMM130C.EventShubetsu.INS_KOUATHUGAS
                        Call Me._G.AddSetSpread5()
                    Case LMM130C.EventShubetsu.INS_YAKUZIHO
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

            Case LMM130C.EventShubetsu.DEL_DOKU,
                 LMM130C.EventShubetsu.DEL_KOUATHUGAS,
                 LMM130C.EventShubetsu.DEL_YAKUZIHO     '行削除

                Select Case eventShubetsu
                    Case LMM130C.EventShubetsu.DEL_DOKU
                        targetSpr = frm.sprDetail4
                        tagetDefColNo = LMM130G.sprDetailDef4.DEF.ColNo
                        tagetUpdFlgColNo = LMM130G.sprDetailDef4.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo
                    Case LMM130C.EventShubetsu.DEL_KOUATHUGAS
                        targetSpr = frm.sprDetail5
                        tagetDefColNo = LMM130G.sprDetailDef5.DEF.ColNo
                        tagetUpdFlgColNo = LMM130G.sprDetailDef5.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo
                    Case LMM130C.EventShubetsu.DEL_YAKUZIHO
                        targetSpr = frm.sprDetail6
                        tagetDefColNo = LMM130G.sprDetailDef6.DEF.ColNo
                        tagetUpdFlgColNo = LMM130G.sprDetailDef6.UPD_FLG.ColNo
                        tagetSysDelFlgColNo = LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo
                End Select

                '項目チェック
                If Me._V.IsTouSituZoneChkRowCheck(eventShubetsu, frm, targetSpr, tagetDefColNo) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(targetSpr.ActiveSheet, tagetDefColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    _ControlH.StartAction(frm)

                    'スプレッドの削除処理(削除フラグの設定・行の削除)
                    Call Me._G.DelTouSituZoneChk(targetSpr, tagetDefColNo, tagetUpdFlgColNo, tagetSysDelFlgColNo)

                    '棟室ゾーンチェックマスタスプレッドの再描画
                    Me._G.ReSetTouSituZoneChkSpread(targetSpr)

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

            Case LMM130C.EventShubetsu.MASTER     'マスタ参照
                '******************「マスタ参照」******************'

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                Select Case objNm

                    Case frm.txtCustCD.Name

                        '荷主コード
                        Dim ds As DataSet = Me.ShowPopup(frm, objNm, prm)
                        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_OUT)

                        If prm.ReturnFlg = False Then
                            '戻り値が無い場合は終了
                            'メッセージの表示
                            ShowMessage(frm, "G003")
                            '処理終了アクション
                            _ControlH.EndAction(frm)
                            Exit Sub
                        Else
                            '荷主コード（大）と荷主名を設定する
                            With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                                frm.txtCustCD.TextValue = .Item("CUST_CD_L").ToString()       '荷主コード（大）
                                frm.lblCustNM.TextValue = .Item("CUST_NM_L").ToString()       '荷主名
                            End With
                        End If

                    Case Else
                        'ポップ対象外のテキストの場合
                        MyBase.ShowMessage(frm, "G005")

                        'カーソルを元に戻す
                        Cursor.Current = Cursors.Default()

                End Select

            Case LMM130C.EventShubetsu.IKKATU_TOUROKU
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

                '入力チェック
                If Not Me._V.IkkatuTourokuCheck(Me.IsExistCachedCust(Me._Frm.cmbNrsBrCd.SelectedValue.ToString, Me._Frm.txtCustCD.TextValue)) Then
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Return
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._ControlH.GetCheckList(frm.sprDetail.ActiveSheet, LMM130G.sprDetailDef3.DEF.ColNo)

                Dim ds As DataSet = New LMM130DS()

                'チェックされた行がなかった場合、エラーを出力し中断とする。
                If arr.Count = 0 Then
                    '対象行を選択してください。
                    MyBase.ShowMessage(frm, "E009")
                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Return

                End If

                'データセット設定
                Call Me.SetDatasetTouSituExpItemData(frm, ds, arr)

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

                '一括登録処理
                Dim rtnDs As DataSet = MyBase.CallWSA("LMM130BLF", "IkkatuTourokuExpData", ds)

                If MyBase.IsMessageStoreExist Then

                    'EXCEL起動 
                    MyBase.MessageStoreDownload(True)
                    MyBase.ShowMessage(frm, "E235")

                    '処理終了アクション
                    _ControlH.EndAction(frm)
                    Return

                End If

                '申請外の商品保管ルール、内部保持データセットの内容を更新する。
                ds.Tables("LMM130IN").Rows.Clear()
                ds.Tables("LMM130IN").Rows.Add(ds.Tables("LMM130IN").NewRow)
                ds = MyBase.CallWSA("LMM130BLF", "SelectListData3", ds)
                Me._TouSituExpDs = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP)

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

                'メッセージの表示
                MyBase.ShowMessage(frm, "G002", New String() {Me._Frm.btnIkkatuTouroku.Text, ""})
                'カーソルを元に戻す
                Cursor.Current = Cursors.Default()


                '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

            Case LMM130C.EventShubetsu.WARE_CHANGE
                '倉庫の選択肢が変更された際の制御
                Me._G.ChangeWare(_dsSokoJT)

            Case LMM130C.EventShubetsu.JISYA_TASYA_CHANGE
                '自社他社区分の選択肢が変更された際の制御
                Me._G.ChangeJisyaTasya()

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
    Private Function ShowPopup(ByVal frm As LMM130F, ByVal objNM As String, ByVal prm As LMFormData) As DataSet

        Dim value As String = String.Empty

        Select Case objNM

            Case LMM130C.EventShubetsu.INS_T.ToString()         '行追加

                Dim prmDs As DataSet = New LMZ280DS()
                Dim row As DataRow = prmDs.Tables(LMZ280C.TABLE_NM_IN).NewRow
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("SELECT_PLURAL_FLG") = LMConst.FLG.ON   '複数選択可
                prmDs.Tables(LMZ280C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ280", prm)

                '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            Case LMM130C.EventShubetsu.INS_EXP_T.ToString()         '行追加

                Dim prmDs As DataSet = New LMZ260DS()
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = Convert.ToString(frm.cmbNrsBrCd.SelectedValue)
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_M
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case "txtCustCD"

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtCustCD.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row.Item("HYOJI_KBN") = LMZControlC.HYOJI_M
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                If String.IsNullOrEmpty(frm.txtCustCD.TextValue) = True Then
                    frm.lblCustNM.TextValue = String.Empty
                End If

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)




                '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

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

        Dim dr As DataRow = Me._FindDs.Tables(LMM130C.TABLE_NM_IN).NewRow()

        With Me._Frm.sprDetail.ActiveSheet

            dr.Item("SYS_DEL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.SYS_DEL_NM.ColNo))
            dr.Item("NRS_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.NRS_BR_NM.ColNo))
            dr.Item("WH_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.SOKO.ColNo))
            dr.Item("TOU_NO") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.TOU_NO.ColNo))
            dr.Item("SITU_NO") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.SITU_NO.ColNo))
            dr.Item("TOU_SITU_NM") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.TOU_SITU_NM.ColNo))
            dr.Item("HOZEI_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.HOZEI_KB_NM.ColNo))
            dr.Item("ONDO_CTL_KB") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.ONDO_CTL_KB_NM.ColNo))
            dr.Item("ONDO_CTL_FLG") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.ONDO_CTL_FLG_NM.ColNo))

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            'dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("USER_BR_CD") = Me._ControlV.GetCellValue(.Cells(0, LMM130G.sprDetailDef.NRS_BR_NM.ColNo))

            Me._FindDs.Tables(LMM130C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetHaitaChk()

        Dim dr As DataRow = Me._Ds.Tables(LMM130C.TABLE_NM_IN).NewRow()

        With Me._Frm

            '排他処理条件を格納
            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue
            dr.Item("SITU_NO") = .txtSituNo.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()

            Me._Ds.Tables(LMM130C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetTouSituItemData(ByVal frm As LMM130F, ByVal ds As DataSet)

        With frm

            '編集部の値（棟室情報）をデータセットに設定
            Dim dr As DataRow = ds.Tables(LMM130C.TABLE_NM_IN).NewRow()

            dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue.ToUpper()
            dr.Item("SITU_NO") = .txtSituNo.TextValue.ToUpper()
            dr.Item("TOU_SITU_NM") = .txtTouSituNm.TextValue.Trim
            dr.Item("SOKO_KB") = .cmbSokoKbn.SelectedValue
            dr.Item("HOZEI_KB") = .cmbHozeiKbn.SelectedValue
            dr.Item("CHOZO_MAX_QTY") = .numChozoMaxQty.Value
            dr.Item("CHOZO_MAX_BAISU") = .numChozoMaxBaisu.Value
            dr.Item("ONDO_CTL_KB") = .cmbOndoCtlKbn.SelectedValue
            dr.Item("ONDO") = .numOndo.Value
            dr.Item("MAX_ONDO_UP") = .numMaxOndoUp.Value
            dr.Item("MINI_ONDO_DOWN") = .numMiniOndoDown.Value
            dr.Item("ONDO_CTL_FLG") = .cmbOndoCtlFlg.SelectedValue
            dr.Item("HAN") = .txtHan.TextValue.Trim
            dr.Item("CBM") = .numCbm.Value
            dr.Item("AREA") = .numArea.Value
            dr.Item("MX_PLT_QT") = .numMxPltQt.Value
            dr.Item("RACK_YN") = .cmbRackYn.SelectedValue
            dr.Item("FCT_MGR") = .txtFctMgr.TextValue.Trim
            dr.Item("SHOKA_KB") = .cmbShokaKbn.SelectedValue
            '要望番号：674 yamanaka 2012.7.5 Start
            dr.Item("JISYATASYA_KB") = .cmbJisyatasyaKbn.SelectedValue
            '要望番号：674 yamanaka 2012.7.5 End
            dr.Item("DOKU_KB") = .cmbDokuKbn.SelectedValue
            dr.Item("GAS_KANRI_KB") = .cmbGasKanriKbn.SelectedValue
            dr.Item("MAX_CAPA_KG_QTY") = .numMxWt.Value
            dr.Item("USER_CD") = .txtUserCd.TextValue
            dr.Item("USER_CD_SUB") = .txtUserCdSub.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr.Item("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim
            'スキーマ名取得用
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            dr.Item("TASYA_WH_NM") = .txtTasyaWhNm.TextValue
            dr.Item("TASYA_ZIP") = .txtTasyaZip.TextValue
            dr.Item("TASYA_AD_1") = .txtTasyaAd1.TextValue
            dr.Item("TASYA_AD_2") = .txtTasyaAd2.TextValue
            dr.Item("TASYA_AD_3") = .txtTasyaAd3.TextValue
            dr.Item("AREA_RENT_HOKAN_AMO") = .numAreaRentHokanAmo.TextValue

            ds.Tables(LMM130C.TABLE_NM_IN).Rows.Add(dr)

            '棟室消防Spread情報をデータセットに設定
            Dim sprMax As Integer = .sprDetail2.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr2 As DataRow = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_SHOBO).NewRow()

                dr2.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr2.Item("WH_CD") = .cmbWare.SelectedValue
                dr2.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr2.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr2.Item("SHOBO_CD") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM130G.sprDetailDef2.SHOBO_CD.ColNo))
                dr2.Item("WH_KYOKA_DATE") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM130G.sprDetailDef2.WH_KYOKA_DATE.ColNo))
                dr2.Item("BAISU") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM130G.sprDetailDef2.BAISU.ColNo))
                dr2.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM130G.sprDetailDef2.UPD_FLG.ColNo))
                dr2.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail2.ActiveSheet.Cells(i, LMM130G.sprDetailDef2.SYS_DEL_FLG_T.ColNo))

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr2.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
                dr2.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM130C.TABLE_NM_TOU_SITU_SHOBO).Rows.Add(dr2)

            Next

            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
            sprMax = .sprDetail3.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr3 As DataRow = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP).NewRow()

                dr3.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr3.Item("WH_CD") = .cmbWare.SelectedValue
                dr3.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr3.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr3.Item("SERIAL_NO") = String.Empty
                dr3.Item("APL_DATE_FROM") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM130G.sprDetailDef3.APPLICATION_DATE_FROM.ColNo))
                dr3.Item("APL_DATE_TO") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM130G.sprDetailDef3.APPLICATION_DATE_TO.ColNo))
                dr3.Item("CUST_CD_L") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM130G.sprDetailDef3.CUST_CODE.ColNo))
                dr3.Item("CUST_NM") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM130G.sprDetailDef3.CUST_NM.ColNo))
                dr3.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM130G.sprDetailDef3.UPD_FLG.ColNo))
                dr3.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail3.ActiveSheet.Cells(i, LMM130G.sprDetailDef3.SYS_DEL_FLG_T.ColNo))

                ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP).Rows.Add(dr3)

            Next
            '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

            '棟室ゾーンチェックマスタSpread情報をデータセットに設定
            '毒劇情報
            sprMax = .sprDetail4.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr4 As DataRow = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK).NewRow()

                dr4.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr4.Item("WH_CD") = .cmbWare.SelectedValue
                dr4.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr4.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr4.Item("ZONE_CD") = String.Empty
                dr4.Item("KBN_GROUP_CD") = LMM130C.M_Z_KBN_DOKUGEKI
                dr4.Item("KBN_CD") = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM130G.sprDetailDef4.DOKU_KB.ColNo))
                dr4.Item("KBN_NM1") = .sprDetail4.ActiveSheet.Cells(i, LMM130G.sprDetailDef4.DOKU_KB.ColNo).Text
                dr4.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM130G.sprDetailDef4.UPD_FLG.ColNo))
                dr4.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail4.ActiveSheet.Cells(i, LMM130G.sprDetailDef4.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr4.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK).Rows.Add(dr4)

            Next

            ' 高圧ガス情報
            sprMax = .sprDetail5.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr5 As DataRow = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK).NewRow()

                dr5.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr5.Item("WH_CD") = .cmbWare.SelectedValue
                dr5.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr5.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr5.Item("ZONE_CD") = String.Empty
                dr5.Item("KBN_GROUP_CD") = LMM130C.M_Z_KBN_KOUATHUGAS
                dr5.Item("KBN_CD") = _ControlG.GetCellValue(.sprDetail5.ActiveSheet.Cells(i, LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo))
                dr5.Item("KBN_NM1") = .sprDetail5.ActiveSheet.Cells(i, LMM130G.sprDetailDef5.KOUATHUGAS_KB.ColNo).Text
                dr5.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail5.ActiveSheet.Cells(i, LMM130G.sprDetailDef5.UPD_FLG.ColNo))
                dr5.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail5.ActiveSheet.Cells(i, LMM130G.sprDetailDef5.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr5.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK).Rows.Add(dr5)

            Next

            '薬事法情報
            sprMax = .sprDetail6.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To sprMax
                Dim dr6 As DataRow = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK).NewRow()

                dr6.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr6.Item("WH_CD") = .cmbWare.SelectedValue
                dr6.Item("TOU_NO") = .txtTouNo.TextValue.Trim
                dr6.Item("SITU_NO") = .txtSituNo.TextValue.Trim
                dr6.Item("ZONE_CD") = String.Empty
                dr6.Item("KBN_GROUP_CD") = LMM130C.M_Z_KBN_YAKUZIHO
                dr6.Item("KBN_CD") = _ControlG.GetCellValue(.sprDetail6.ActiveSheet.Cells(i, LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo))
                dr6.Item("KBN_NM1") = .sprDetail6.ActiveSheet.Cells(i, LMM130G.sprDetailDef6.YAKUZIHO_KB.ColNo).Text
                dr6.Item("UPD_FLG") = _ControlG.GetCellValue(.sprDetail6.ActiveSheet.Cells(i, LMM130G.sprDetailDef6.UPD_FLG.ColNo))
                dr6.Item("SYS_DEL_FLG") = _ControlG.GetCellValue(.sprDetail6.ActiveSheet.Cells(i, LMM130G.sprDetailDef6.SYS_DEL_FLG_T.ColNo))

                '営業所またぎ処理のため画面値より営業所コード取得
                dr6.Item("USER_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

                ds.Tables(LMM130C.TABLE_NM_TOU_SITU_ZONE_CHK).Rows.Add(dr6)

            Next

        End With

    End Sub

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' データセット設定(編集部データ)(登録・更新用)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetTouSituExpItemData(ByVal frm As LMM130F, ByVal ds As DataSet, ByVal CheckListIndexs As ArrayList)

        With frm

            '編集部の値（棟室情報）をデータセットに設定
            Dim dr As DataRow = ds.Tables(LMM130C.TABLE_NM_IN).NewRow()

            dr.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue.ToUpper()
            dr.Item("SITU_NO") = .txtSituNo.TextValue.ToUpper()
            dr.Item("TOU_SITU_NM") = .txtTouSituNm.TextValue.Trim
            dr.Item("SOKO_KB") = .cmbSokoKbn.SelectedValue
            dr.Item("HOZEI_KB") = .cmbHozeiKbn.SelectedValue
            dr.Item("CHOZO_MAX_QTY") = .numChozoMaxQty.Value
            dr.Item("CHOZO_MAX_BAISU") = .numChozoMaxBaisu.Value
            dr.Item("ONDO_CTL_KB") = .cmbOndoCtlKbn.SelectedValue
            dr.Item("ONDO") = .numOndo.Value
            dr.Item("MAX_ONDO_UP") = .numMaxOndoUp.Value
            dr.Item("MINI_ONDO_DOWN") = .numMiniOndoDown.Value
            dr.Item("ONDO_CTL_FLG") = .cmbOndoCtlFlg.SelectedValue
            dr.Item("HAN") = .txtHan.TextValue.Trim
            dr.Item("CBM") = .numCbm.Value
            dr.Item("AREA") = .numArea.Value
            dr.Item("MX_PLT_QT") = .numMxPltQt.Value
            dr.Item("RACK_YN") = .cmbRackYn.SelectedValue
            dr.Item("FCT_MGR") = .txtFctMgr.TextValue.Trim
            dr.Item("SHOKA_KB") = .cmbShokaKbn.SelectedValue
            dr.Item("JISYATASYA_KB") = .cmbJisyatasyaKbn.SelectedValue
            dr.Item("DOKU_KB") = .cmbDokuKbn.SelectedValue
            dr.Item("GAS_KANRI_KB") = .cmbGasKanriKbn.SelectedValue
            dr.Item("MAX_CAPA_KG_QTY") = .numMxWt.Value
            dr.Item("USER_CD") = .txtUserCd.TextValue
            dr.Item("USER_CD_SUB") = .txtUserCdSub.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue.Trim)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim
            dr.Item("SYS_DEL_FLG") = .lblSysDelFlg.TextValue.Trim
            dr.Item("USER_BR_CD") = .cmbNrsBrCd.SelectedValue.ToString()
            'dr.Item("APL_DATE_FROM") = DateFormatUtility.DeleteSlash(.imdSearchDate_From.TextValue)
            'dr.Item("APL_DATE_TO") = DateFormatUtility.DeleteSlash(.imdSearchDate_To.TextValue)
            'dr.Item("CUST_CD_L") = .txtCustCD.TextValue

            ds.Tables(LMM130C.TABLE_NM_IN).Rows.Add(dr)

            '申請外の商品保管ルールマスタ登録用データテーブル設定
            For i As Integer = 0 To CheckListIndexs.Count - 1
                Dim detailSpreadRowsIndex As Integer = Convert.ToInt32(CheckListIndexs(i))
                '情報が他社の場合はエラー情報をストックする
                If _ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(detailSpreadRowsIndex, LMM130G.sprDetailDef.JISYATASYA_KB.ColNo)).Equals(LMM130C.TASYA) Then
                    '～行が他社の情報のため一括登録が出来ません。
                    MyBase.SetMessageStore("00", "E959", New String() {(detailSpreadRowsIndex).ToString})
                End If
                Dim dr3 As DataRow = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP).NewRow()
                dr3.Item("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
                dr3.Item("WH_CD") = _ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(detailSpreadRowsIndex, LMM130G.sprDetailDef.WH_CD.ColNo))
                dr3.Item("TOU_NO") = _ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(detailSpreadRowsIndex, LMM130G.sprDetailDef.TOU_NO.ColNo))
                dr3.Item("SITU_NO") = _ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(detailSpreadRowsIndex, LMM130G.sprDetailDef.SITU_NO.ColNo))
                dr3.Item("SERIAL_NO") = String.Empty
                dr3.Item("APL_DATE_FROM") = DateFormatUtility.DeleteSlash(.imdSearchDate_From.TextValue)
                dr3.Item("APL_DATE_TO") = DateFormatUtility.DeleteSlash(.imdSearchDate_To.TextValue)
                dr3.Item("CUST_CD_L") = .txtCustCD.TextValue
                dr3.Item("CUST_NM") = .lblCustNM.TextValue
                dr3.Item("UPD_FLG") = BaseConst.FLG.ON
                dr3.Item("SYS_DEL_FLG") = BaseConst.FLG.OFF
                dr3.Item("ROW_NO") = (detailSpreadRowsIndex).ToString
                ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP).Rows.Add(dr3)

            Next

            '画面情報重複チェック
            '登録対象が画面にある時点で重複していた場合はエラー情報をストックし、この時点で一括登録処理がら除外してしまう。
            Dim dt As DataTable = ds.Tables(LMM130C.TABLE_NM_TOU_SITU_EXP)
            Dim max As Integer = dt.Rows.Count - 1
            For i As Integer = 0 To max - 1
                For j As Integer = i + 1 To max
                    'データテーブル内で営業所コード、倉庫、棟、室で重複していた場合はエラー情報をストックする
                    If dt.Rows(i).Item("NRS_BR_CD").Equals(dt.Rows(j).Item("NRS_BR_CD")) Then
                        If dt.Rows(i).Item("WH_CD").Equals(dt.Rows(j).Item("WH_CD")) Then
                            If dt.Rows(i).Item("TOU_NO").Equals(dt.Rows(j).Item("TOU_NO")) Then
                                If dt.Rows(i).Item("SITU_NO").Equals(dt.Rows(j).Item("SITU_NO")) Then
                                    '～行と～行は重複しています。確認してください。
                                    MyBase.SetMessageStore("00", "E960", New String() {dt.Rows(i).Item("ROW_NO").ToString, dt.Rows(j).Item("ROW_NO").ToString})
                                    'MyBase.SetMessageStore("00", "E022", New String() {dt.Rows(i).Item("ROW_NO").ToString & "行と" & dt.Rows(j).Item("ROW_NO").ToString & "行"})
                                End If
                            End If
                        End If
                    End If
                Next
            Next

        End With

    End Sub
    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' データセット設定(削除復活処理)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDatasetDelData(ByVal ds As DataSet)

        Dim dr As DataRow = ds.Tables(LMM130C.TABLE_NM_IN).NewRow()

        With Me._Frm

            dr.Item("WH_CD") = .cmbWare.SelectedValue
            dr.Item("TOU_NO") = .txtTouNo.TextValue
            dr.Item("SITU_NO") = .txtSituNo.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue.Trim

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'dr.Item("USER_BR_CD") = LMUserInfoManager.GetNrsBrCd.ToString()
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

            ds.Tables(LMM130C.TABLE_NM_IN).Rows.Add(dr)

        End With

    End Sub

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
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
    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' ユーザマスタ検索の戻り値を設定
    ''' </summary>
    ''' <param name="txtUserCd">(入力) ユーザコードのテキストボックス</param>
    ''' <param name="lblUserNm">(出力) ユーザ名のテキストボックス</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUser(ByVal txtUserCd As InputMan.LMImTextBox, ByVal lblUserNm As InputMan.LMImTextBox) As Boolean

        'キャッシュから取得
        Dim drs As DataRow() = Me._ControlG.SelectUserListDataRow(txtUserCd.TextValue)
        If drs.Length < 1 Then
            lblUserNm.TextValue = String.Empty
            Return False
        End If

        txtUserCd.TextValue = drs(0).Item("USER_CD").ToString()
        lblUserNm.TextValue = drs(0).Item("USER_NM").ToString()

        Return True

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey2Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey3Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey4Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' F10押下時処理呼び出し(初期荷主変更処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        '一件時表示有り
        Me._PopupSkipFlg = True

        '「マスタ参照」処理
        Call Me.ActionControl(LMM130C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub
    '2017/10/25 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveTouSituItemData")

        Me.SaveTouSituItemData(frm, LMM130C.EventShubetsu.HOZON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveTouSituItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMM130F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMM130F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_SHOBO_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_SHOBO_Click")

        '「行追加」処理
        Me.ActionControl(LMM130C.EventShubetsu.INS_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_SHOBO_Click")

    End Sub

    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 行追加（申請外の商品保管ルール） 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_EXP_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_EXP_Click")

        '「行追加」処理
        Me.ActionControl(LMM130C.EventShubetsu.INS_EXP_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_EXP_Click")

    End Sub
    '2017/10/24 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 毒劇情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_DOKU_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_DOKU_Click")

        '「行追加」処理
        Me.ActionControl(LMM130C.EventShubetsu.INS_DOKU, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_DOKU_Click")

    End Sub

    ''' <summary>
    ''' 高圧ガス情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_KOUATHUGAS_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_KOUATHUGAS_Click")

        '「行追加」処理
        Me.ActionControl(LMM130C.EventShubetsu.INS_KOUATHUGAS, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_KOUATHUGAS_Click")

    End Sub

    ''' <summary>
    ''' 薬事法情報行追加 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_CHK_YAKUZIHO_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_CHK_YAKUZIHO_Click")

        '「行追加」処理
        Me.ActionControl(LMM130C.EventShubetsu.INS_YAKUZIHO, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_CHK_YAKUZIHO_Click")

    End Sub

    ''' <summary>
    ''' 行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_SHOBO_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_SHOBO_Click")

        '「行削除」処理
        Me.ActionControl(LMM130C.EventShubetsu.DEL_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_SHOBO_Click")

    End Sub

    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start 
    ''' <summary>
    ''' 行削除（申請外の商品保管ルール） 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_EXP_SHOBO_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_EXP_SHOBO_Click")

        '「行削除」処理
        Me.ActionControl(LMM130C.EventShubetsu.DEL_EXP_T, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_EXP_SHOBO_Click")

    End Sub

    ''' <summary>
    ''' 一括登録　押下処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnIkkatuTouroku_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnIkkatuTouroku_Click")

        '「一括登録」処理
        Me.ActionControl(LMM130C.EventShubetsu.IKKATU_TOUROKU, frm)

        Logger.StartLog(Me.GetType.Name, "btnIkkatuTouroku_Click")

    End Sub
    '2017/10/26 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end 

    ''' <summary>
    ''' 毒劇情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_DOKU_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_DOKU_Click")

        '「行削除」処理
        Me.ActionControl(LMM130C.EventShubetsu.DEL_DOKU, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_DOKU_Click")

    End Sub

    ''' <summary>
    ''' 高圧ガス情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_KOUATHUGAS_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_KOUATHUGAS_Click")

        '「行削除」処理
        Me.ActionControl(LMM130C.EventShubetsu.DEL_KOUATHUGAS, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_KOUATHUGAS_Click")

    End Sub

    ''' <summary>
    ''' 薬事法情報行削除 押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_CHK_YAKUZIHO_Click(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_CHK_YAKUZIHO_Click")

        '「行削除」処理
        Me.ActionControl(LMM130C.EventShubetsu.DEL_YAKUZIHO, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_CHK_YAKUZIHO_Click")

    End Sub

    ''' <summary>
    ''' 倉庫 選択肢変更
    ''' </summary>
    ''' <param name="frm"></param>
    Friend Sub cmbWare_SelectedValueChanged(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "cmbWare_SelectedValueChanged")

        Me.ActionControl(LMM130C.EventShubetsu.WARE_CHANGE, frm)

        Logger.EndLog(Me.GetType.Name, "cmbWare_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' 自社他社区分 選択肢変更
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    Friend Sub cmbJisyatasyaKbn_SelectedValueChanged(ByRef frm As LMM130F)

        Logger.StartLog(Me.GetType.Name, "cmbJisyatasyaKbn_SelectedValueChanged")

        Me.ActionControl(LMM130C.EventShubetsu.JISYA_TASYA_CHANGE, frm)

        Logger.EndLog(Me.GetType.Name, "cmbJisyatasyaKbn_SelectedValueChanged")

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMM130F_KeyDown(ByVal frm As LMM130F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMM130F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMM130F_KeyDown")

    End Sub

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMM130F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class