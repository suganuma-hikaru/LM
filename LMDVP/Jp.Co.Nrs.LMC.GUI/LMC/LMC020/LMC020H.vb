' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC020C : 出荷データ編集
'  作  成  者       :  矢内
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMC020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMC020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMC020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMC020G

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconH As LMCControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMC020DS

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 分析票パス格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _BunsekiArr As ArrayList

    ''' <summary>
    '''削除フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _DelFlg As Boolean

    ''' <summary>
    ''' サーバ日時を格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _SysData(2) As String

    ''' <summary>
    '''FFEM 仕掛品フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _ShikakariHinFlg As String = ""

    ''' <summary>
    ''' 顧客指示変更不可フラグ
    ''' </summary>
    ''' <remarks>False:制限なし　True:出荷(中)の追加・削除を禁止</remarks>
    Private _NoChangeOutkaM As Boolean = False

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

    ''' <summary>
    ''' サーバ日時を取得・設定
    ''' </summary>
    ''' <param name="index">
    ''' 0：サーバ日付
    ''' 1：サーバ時間
    ''' </param>
    ''' <value>サーバ日時用プロパティ</value>
    ''' <returns>truckNmのコントロール</returns>
    ''' <remarks></remarks>
    Private Property SysData(ByVal index As LMC020C.SysData) As String
        Get
            Return _SysData(index)
        End Get
        Set(ByVal value As String)
            _SysData(index) = value
        End Set
    End Property

    ''' <summary>
    ''' 初期検索・保存時の再検索時のフラグ（運送手配等を設定する時にChangeイベントが発生してしまうので、回避用）
    ''' </summary>
    ''' <remarks></remarks>
    Private _SyokiFlg As Boolean

    ''' <summary>
    ''' 画面間パラメータ
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

    'START YANAI 20110913 小分け対応
    ''' <summary>
    ''' 新規作成フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _NewFlg As Boolean = False
    'END YANAI 20110913 小分け対応

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <remarks></remarks>
    Private _prmInitDs As DataSet = Nothing

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _DsCmpr As DataSet = New LMC020DS

    ''' <summary>
    ''' 印刷対象リモート PDF のコピー先ディレクトリ名
    ''' </summary>
    Private _copyToDirectoryName As String = ""

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

        Me._Prm = prm

        'フォームの作成
        Dim frm As LMC020F = New LMC020F(Me)

        'Validateクラスの設定
        Me._V = New LMC020V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMC020G(Me, frm)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMCconG = New LMCControlG(Me, sForm)

        'Hnadler共通クラスの設定
        Me._LMCconH = New LMCControlH(sForm)

        'Validate共通クラスの設定
        Me._LMCconV = New LMCControlV(Me, sForm)

        Me._SyokiFlg = True

        Me._DelFlg = False

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyosyo, frm.cmbSoko)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Dim sysData As String() = MyBase.GetSystemDateTime()
        Me.SysData(LMC020C.SysData.YYYYMMDD) = sysData(0)
        Me.SysData(LMC020C.SysData.HHMMSSsss) = sysData(1)
        Call Me._G.ClearControlL(Convert.ToString(Me.SysData(LMC020C.SysData.YYYYMMDD)))
        Call Me._G.ClearControlM()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpreadM()
        Call Me._G.InitSpreadS()

        '数値コントロールの書式設定
        Call Me._G.SetNumberControl()

        '日付コントロールの書式設定
        Call Me._G.SetNumberControl()

        Dim mode As String = LMC020C.MODE_READONLY
        Dim disp As String = DispMode.VIEW

        Me._Ds = Nothing

        If prm.RecStatus = RecordStatus.NEW_REC Then

            '要望管理1795 s.kobayashi 2013.1.22
            _prmInitDs = prmDs

            '新規モード
            mode = LMC020C.MODE_EDIT
            disp = DispMode.EDIT

            'シチュエーションラベルの設定
            Call Me._G.SetSituation(disp, prm.RecStatus)

            '荷主コードをテキストに設定
            Dim dr As DataRow = prmDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0)
            Call Me._G.SetCustInit(dr)

            '要望番号:1568 terakawa 2013.01.17 Start
            '出荷予定日を上書き設定
            Me._G.SetOutkaPlanDate(dr)
            '要望番号:1568 terakawa 2013.01.17 End

            '要望番号:1793 terakawa 2013.01.21 Start
            'まとめピック区分を上書き設定
            Me._G.SetPickKb(dr)
            '要望番号:1793 terakawa 2013.01.21 End

            '出荷単位によるコントロール制御
            Me._G.SetKosuSuryo()

            '運送タリフセット取得
            Me._G.SetUnsoTariffSet(True)

            '要望番号:1568 terakawa 2013.01.17 Start
            '運送会社取得（画面間データ→荷主マスタ→届先マスタ→運送会社マスタ 経由で取得）
            'Call Me._G.GetUnsoCompany(frm)
            Call Me._G.GetUnsoCompany(frm, dr)
            '要望番号:1568 terakawa 2013.01.17 End

            '運送課税区分取得
            Call Me.SetUnchinUmuInitCd(frm)

            '運送のその他デフォルト設定する項目
            Call Me._G.SetDefaultUnso()

            Me._Ds = New LMC020DS

            '2015.07.08 協立化学　シッピングマーク対応　追加START
            'シッピングマーク(HED)の情報を設定
            Call Me._G.SetMarkHedControl(Me._Ds, -1, LMC020C.EventShubetsu.SINKI)
            '2015.07.08 協立化学　シッピングマーク対応　追加END

            'メッセージの表示
            MyBase.ShowMessage(frm, "G003")

            'START YANAI 20110913 小分け対応
            Me._NewFlg = True
            'END YANAI 20110913 小分け対応

            'タブレット項目の初期値設定
            Me._G.SetWHTablet()

        Else
            '編集モード
            mode = LMC020C.MODE_READONLY
            disp = DispMode.VIEW

            'シチュエーションラベルの設定
            Call Me._G.SetSituation(disp, prm.RecStatus)

            '初期検索処理
            Me._Ds = Me.ServerAccess(prmDs, "SelectInitData")
            Me._DsCmpr = Me._Ds.Copy

            If Me._Ds Is Nothing = False _
                AndAlso 0 < Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows.Count Then
                '検索成功

                'FFEM 仕掛品フラグ 退避
                Me._ShikakariHinFlg = prmDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0).Item("SHIKAKARI_HIN_FLG").ToString()

                '顧客指示変更不可フラグ 退避
                With ""
                    Dim nrsBrCd As String = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("NRS_BR_CD").ToString()
                    Dim custCdL As String = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("CUST_CD_L").ToString()
                    Dim custCdM As String = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("CUST_CD_M").ToString()
                    Dim dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'WC05' AND KBN_NM1 = '", nrsBrCd, "' AND KBN_NM2 = '", custCdL, "' AND KBN_NM3 = '", custCdM, "'"))
                    If dr.Length = 0 Then
                        _NoChangeOutkaM = False
                    Else
                        _NoChangeOutkaM = True
                    End If
                End With

                '初期表示時の値設定
                Me._G.SetInitValue(Me._Ds)

                Dim rootPGID As String = MyBase.RootPGID()
                Dim rowIndex As Integer = 0
                If (LMC020C.PGID_LMD040).Equals(rootPGID) = True Then
                    'LMD040遷移の時は、LMD040で設定された入荷管理番号(中)のデータを表示
                    rowIndex = Me._G.GetOutkaNoM(prmDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0).Item("OUTKA_NO_M").ToString)
                Else
                    rowIndex = 0
                End If

                If rowIndex <> -1 Then
                    'スプレッド選択行の詳細（出荷(中)）を表示
                    Call Me._G.SetOutkaM(rowIndex, Me._Ds)

                    'スプレッド選択行の詳細（作業(中)）を表示()
                    Call Me._G.SetSagyoMControl(rowIndex, Me._Ds)

                    'スプレッド選択行の詳細（出荷(小)）を表示
                    Call Me._G.SetSpread(frm.sprDtl, Me._Ds, LMConst.FLG.OFF, rowIndex)

                    'START YANAI 要望番号853 まとめ処理対応
                    ''引当可能個数・数量のグループ化
                    'Call Me._G.SpreadGroup(Me._Ds)
                    'END YANAI 要望番号853 まとめ処理対応

                End If

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

            Else

                'メッセージの表示
                '2015.10.21 tusnehira add
                '英語化対応
                MyBase.ShowMessage(frm, "E688")
                'MyBase.ShowMessage(frm, "E078", New String() {"出荷データ"})

            End If

        End If
    
        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(mode)

        'ボタンの設定
        Call Me._G.SetBtn(mode, _NoChangeOutkaM)

        '画面の入力項目の制御
        Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                            "SYS_DEL_FLG = '0'"))
        Call Me._G.SetControlsStatus(mode, outDs.Length)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Me._SyokiFlg = False

        ' 印刷対象リモート PDF のコピー先ディレクトリ名 決定
        Call SetCopyToDirectoryName()

        'フォームの表示
        frm.Show()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "内部メソッド"

    ''' <summary>
    ''' 検索以外のイベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal frm As LMC020F)

        ''チェックリスト格納変数
        'Dim list As ArrayList = New ArrayList()

        Select Case eventShubetsu
            Case LMC020C.EventShubetsu.HIKIATE, _
                 LMC020C.EventShubetsu.HOZON, _
                 LMC020C.EventShubetsu.INS_M, _
                 LMC020C.EventShubetsu.COPY_M, _
                 LMC020C.EventShubetsu.DOUBLE_CLICK, _
                 LMC020C.EventShubetsu.LEAVE_CELL

                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, _
                                                             "' AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, _
                                                             "' AND SUB_KB = '80'"))
#If False Then  'UPD 2020/03/25 011614   【LMS】アクタス_引当て時に商品コード先頭7桁のみで引当て変更
                If (eventShubetsu.Equals(LMC020C.EventShubetsu.HIKIATE) OrElse eventShubetsu.Equals(LMC020C.EventShubetsu.HOZON)) AndAlso custDetailsDr.Length > 0 AndAlso Convert.ToInt32(frm.lblEdiOutkaTtlNb.TextValue) > 0 Then

#Else
                If (eventShubetsu.Equals(LMC020C.EventShubetsu.HIKIATE) OrElse eventShubetsu.Equals(LMC020C.EventShubetsu.HOZON)) AndAlso custDetailsDr.Length > 0 AndAlso Convert.ToInt32(frm.lblEdiOutkaTtlNb.TextValue) > 0 _
                  AndAlso LMC020C.AKUTASU_BRCUST.Equals(String.Concat(frm.cmbEigyosyo.SelectedValue, frm.txtCust_Cd_L.TextValue)) = False Then

#End If

                Else
                    '個数・数量を求める
                    If frm.optCnt.Checked = True Then
                        If Me._G.GetKosuSuryo(LMConst.FLG.ON) = False Then
                            Exit Sub
                        End If
                    Else
                        If Me._G.GetKosuSuryo(LMConst.FLG.OFF) = False Then
                            Exit Sub
                        End If
                    End If

                End If

        End Select

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        'ディスプレイモード、レコードステータス保存域
        Dim mode As String = String.Empty
        Dim status As String = String.Empty
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Select Case eventShubetsu

            Case LMC020C.EventShubetsu.SINKI    '新規

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                Dim ds As DataSet = Me.ShowPopup(frm, LMC020C.EventShubetsu.SINKI.ToString(), prm)

                'データセットのクリア
                Dim newDs As DataSet = New LMC020DS
                Me._Ds = newDs

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り件数が0件の場合はエラー
                    'メッセージの表示
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    MyBase.ShowMessage(frm, "E193")
                    Exit Sub
                End If

                'シチュエーションラベルの設定
                mode = DispMode.EDIT
                status = RecordStatus.NEW_REC
                Me._G.SetSituation(mode, status)

                'ファンクションキーの設定
                Me._G.SetFunctionKey(mode)

                'コントロール個別設定
                Me._SyokiFlg = True
                Call Me._G.ClearControlL(Me.SysData(LMC020C.SysData.YYYYMMDD))
                Me._SyokiFlg = False
                Call Me._G.ClearControlM()

                'タブレット使用項目の設定
                Call Me._G.SetWHTablet()

                '戻り値をテキストに設定
                Dim dr As DataRow = ds.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                Call Me._G.SetCust(dr)

                'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                Me._G.InitSpreadM()
                Me._G.InitSpreadS()

                'START YANAI 要望番号1364 出荷運送手配区分が設定されない
                ''運送タリフセット取得
                'Me._G.SetUnsoTariffSet(True)
                Me._SyokiFlg = True
                '運送タリフセット取得
                Me._G.SetUnsoTariffSet(True)
                Me._SyokiFlg = False
                'END YANAI 要望番号1364 出荷運送手配区分が設定されない
                Dim initDr As DataRow = Nothing
                '要望番号:1795 s.kobayashi 2013.01.22 Start
                If _prmInitDs Is Nothing = False Then
                    initDr = _prmInitDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0)
                    '出荷予定日を上書き設定
                    Me._G.SetOutkaPlanDate(initDr)
                    'まとめピック区分を上書き設定
                    Me._G.SetPickKb(initDr)
                    '要望番号:1795 s.kobayashi 2013.01.22 End
                End If
                '運送会社取得（荷主マスタ→届先マスタ→運送会社マスタ 経由で取得）
                Call Me._G.GetUnsoCompany(frm, initDr)

                '運送課税区分取得
                Call Me.SetUnchinUmuInitCd(frm)

                '運送のその他デフォルト設定する項目
                Call Me._G.SetDefaultUnso()

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '画面の入力項目の制御
                Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                    "SYS_DEL_FLG = '0'"))
                Me._G.SetControlsStatus(mode, outDs.Length)

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

                'ボタンの設定
                Call Me._G.SetBtn(mode, _NoChangeOutkaM)

            Case LMC020C.EventShubetsu.HENSHU    '編集
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                Else
                    '請求鑑ヘッダチェック
                    If Me.CheckKagamiData(frm, eventShubetsu, frm.FunctionKey.F2ButtonName) = False Then
                        Exit Sub
                    End If
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                'シチュエーションラベルの設定
                mode = DispMode.EDIT
                status = RecordStatus.NOMAL_REC
                Me._G.SetSituation(mode, status)

                'ファンクションキーの設定
                Me._G.SetFunctionKey(mode, Me._Ds)

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '画面の入力項目の制御
                Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                    "SYS_DEL_FLG = '0'"))

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                'Me._G.SetControlsStatus(mode, outDs.Length)
                Me._G.SetControlsStatus(mode, outDs.Length, Me._Ds)
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End

                '要望対応:1595 yamanaka 2012.11.15 Start
                Dim outDr As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                    "AND SYS_DEL_FLG = '0'"))
                Call Me._G.ChangeGoodsLock(outDr.Count)
                '要望対応:1595 yamanaka 2012.11.15 End

                'ボタンの設定
                Call Me._G.SetBtn(mode, _NoChangeOutkaM)

            Case LMC020C.EventShubetsu.FUKUSHA    '複写

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '要望：2210 振替チェック
                Dim rtnResult As Boolean = Me._V.IsFurikae(Me._Ds)
                If rtnResult = False Then
                    '振替データは複写しない
                    Exit Sub
                End If

                '値のクリア設定
                Call Me._G.CopyControlL()

                '2015.07.21 協立化学　シッピング対応 追加START
                Call Me._G.ClearControlMarkHed()
                '2015.07.21 協立化学　シッピング対応 追加END

                'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                Me._G.InitSpreadM()
                Me._G.InitSpreadS()

                'データセットのクリア
                Me.ClearDataSet()

                'シチュエーションラベルの設定
                mode = DispMode.EDIT
                status = RecordStatus.COPY_REC
                Me._G.SetSituation(mode, status)

                'ファンクションキーの設定
                Me._G.SetFunctionKey(mode)

                'タブレット使用項目の設定
                Call Me._G.SetWHTablet()

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '顧客指示変更不可フラグの初期化
                _NoChangeOutkaM = False

                '画面の入力項目の制御
                Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                      "SYS_DEL_FLG = '0'"))
                Me._G.SetControlsStatus(mode, outDs.Length)

                'ボタンの設定
                Call Me._G.SetBtn(mode, _NoChangeOutkaM)

            Case LMC020C.EventShubetsu.DEL    '削除
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '2012.11.28 nakamura 要望番号612:在庫振替削除 Start
                '振替の場合はLMC020VでW221を表示している為ここではC001は表示しない
                If String.IsNullOrEmpty(frm.lblFurikaeNo.TextValue) = True Then
                    '確認メッセージ
                    '20151021 tunehira add
                    Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C001", New String() {frm.FunctionKey.F4ButtonName})
                    If rtnResult <> MsgBoxResult.Ok Then
                        '処理終了アクション
                        Me._LMCconH.EndAction(frm)
                        Exit Sub

                    End If
                End If

                ''確認メッセージ
                'Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C001", New String() {"削除"})

                'If rtnResult <> MsgBoxResult.Ok Then
                '    '処理終了アクション
                '    Me._LMCconH.EndAction(frm)
                '    Exit Sub

                'End If
                '2012.11.28 nakamura 要望番号612:在庫振替削除 End

                '2012.11.20 nakamura 要望番号612:在庫振替削除 Start
                If String.IsNullOrEmpty(frm.lblFurikaeNo.TextValue) = False Then

                    Dim FuriDelrtnDs As DataSet = Me._G.SetFuriDelDataSet(Me._Ds, eventShubetsu)

                    'SetFuriDelDataSetの結果をMe._DsにCOPY
                    Me._Ds = FuriDelrtnDs.Copy

                    '★★★★★★★★★★★★★★
                    Dim rtnResult As Boolean = True

                    rtnResult = rtnResult AndAlso Me._V.IsDeleteChk(Me._Ds)

                    'エラーの場合、終了
                    If rtnResult = False Then

                        '処理終了アクション
                        Call Me._LMCconH.EndAction(frm)
                        Exit Sub

                    End If
                    '★★★★★★★★★★★★

                    '完了取消データ削除(以下、完了取り消しから流用)
                    '出荷データ(大)のDataSet更新
                    Me._G.SetTK_OUT_L(Me._Ds)

                    '作業のDataSet更新
                    Me._G.SetTK_SAGYO(Me._Ds)

                    '運送のDataSet更新
                    Me._G.SetTK_UNSO(Me._Ds)

                    'イベント種別を一時的に、完了取り消しに設定
                    Dim W_eventShubetu As Integer = LMC020C.EventShubetsu.TORIKESHI
                    '在庫データのDataSetに削除する出荷データ(小)の引当個数分を戻す(完了取消時の戻し)
                    Dim ZairtnDs As DataSet = Me._G.SetZaiRtn(Me._Ds, LMC020C.EventShubetsu.TORIKESHI)

                End If
                '2012.11.20 nakamura 要望番号612:在庫振替削除 End

                Dim rtnDs As DataSet = Nothing
                'START YANAI No.42
                Dim copyDs As DataSet = Me._Ds.Copy
                'END YANAI No.42
                If Me._DelFlg = False Then
                    '在庫データのDataSetに削除する出荷データ(小)の引当個数分を戻す
                    rtnDs = Me._G.SetZaiRtn(Me._Ds, eventShubetsu)

                    '出荷データ(大)のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_OUT_L, Me._Ds)

                    '出荷データ(中)のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_OUT_M, Me._Ds)

                    '出荷データ(小)のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_OUT_S, Me._Ds)

                    '作業のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_SAGYO, Me._Ds)

                    '運送(大)のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_UNSO_L, Me._Ds)
                    '※運送(中)と運賃テーブルに関しては削除フラグを設定しなくても削除される

                    '2015.07.08 協立化学　シッピングマーク対応 追加START
                    'マーク(HED)のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_MARK_HED, Me._Ds)
                    '2015.07.08 協立化学　シッピングマーク対応 追加END

                    '2015.07.21 協立化学　シッピングマーク対応 追加START
                    'マーク(DTL)のDataSetに削除フラグを設定
                    Me._G.SetDelFlg(LMC020C.TABLE_NM_MARK_DTL, Me._Ds)
                    '2015.07.21 協立化学　シッピングマーク対応 追加END

                    Me._DelFlg = True
                End If

                '請求鑑チェック用データセット設定
                Me._G.SetKagamiDataSet(Me._Ds, eventShubetsu)

                '保存処理呼び出し
                rtnDs = Me.DeleteControl(frm, eventShubetsu)

                'エラーがある場合、終了
                If MyBase.IsMessageExist() = True Then

                    'START YANAI No.42
                    Me._Ds = copyDs.Copy
                    Me._DelFlg = False
                    'END YANAI No.42

                    'メッセージ表示
                    MyBase.ShowMessage(frm)

                    '処理終了アクション 
                    Me._LMCconH.EndAction(frm)
                    Exit Sub

                End If

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                'リターンフラグ設定
                Me._Prm.ReturnFlg = True

                '終了処理  
                frm.Close()

            Case LMC020C.EventShubetsu.HIKIATE    '引当

                If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then

                    '処理開始アクション
                    Me._LMCconH.StartAction(frm)

                    If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then
                        '出荷管理番号(中・小）の採番
                        Me._G.SetOutNoDataSet(Me._Ds)

                        '出荷データ(中)をDataSetに更新
                        Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                        '出荷データ(小)をDataSetに更新
                        'START YANAI 20110913 小分け対応
                        'Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                        Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                        'END YANAI 20110913 小分け対応

                        '作業(中レコード)をDataSetに更新
                        Me._G.SetSagyoMDataSet(Me._Ds)

                    End If

                    '引当処理を呼ぶ
                    'START YANAI 要望番号545
                    'Me.ShowHIKIATEG(frm, prm, True, Nothing, False)
                    Me.ShowHIKIATEG(frm, prm, True, Nothing, False, eventShubetsu)
                    'END YANAI 要望番号545

                    'START YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる
                    '出荷管理番号(中・小）の採番
                    Me._G.SetOutNoDataSet(Me._Ds)

                    '出荷データ(小)をDataSetに更新
                    Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                    'END YANAI 要望番号1168 １個以下の小分け引当出荷データを出荷削除した場合、在庫がずれる

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)

                    '要望対応:1595 yamanaka 2012.11.15 Start
                    Dim outDr As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                        "AND SYS_DEL_FLG = '0'"))
                    Call Me._G.ChangeGoodsLock(outDr.Count)
                    '要望対応:1595 yamanaka 2012.11.15 End

                    '倉庫のロック制御
                    Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                        "SYS_DEL_FLG = '0'"))
                    Me._G.SetSoko(outDs.Length)

                End If

            Case LMC020C.EventShubetsu.TORIKESHI    '完了取消
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '確認メッセージ
                '20151021 tsunehira add
                Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C001", New String() {frm.FunctionKey.F6ButtonName})

                If rtnResult <> MsgBoxResult.Ok Then
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub

                End If

                '出荷データ(大)のDataSet更新
                Me._G.SetTK_OUT_L(Me._Ds)

                '作業のDataSet更新
                Me._G.SetTK_SAGYO(Me._Ds)

                '運送のDataSet更新
                Me._G.SetTK_UNSO(Me._Ds)

                '在庫データのDataSetに削除する出荷データ(小)の引当個数分を戻す
                Dim rtnDs As DataSet = Me._G.SetZaiRtn(Me._Ds, eventShubetsu)

                '請求鑑チェック用データセット設定
                Me._G.SetKagamiDataSet(Me._Ds, eventShubetsu)

                Dim dt As DataTable = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L)
                Dim dRow As DataRow = dt.Rows(0)
                rtnDs = Nothing

                '保存処理呼び出し
                rtnDs = Me.DeleteControl(frm, eventShubetsu)

                'エラーがある場合、終了
                If MyBase.IsMessageExist() = True Then

                    'メッセージ表示
                    MyBase.ShowMessage(frm)

                    '処理終了アクション 
                    Me._LMCconH.EndAction(frm)

                    Me._Ds.Tables(LMC020C.TABLE_JIKAI_BUNNOU).Clear()
                    Exit Sub

                End If

                '更新結果の反映
                Me._Ds = rtnDs

                Me._Ds.Tables(LMC020C.TABLE_JIKAI_BUNNOU).Clear()

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                'リターンフラグ設定
                Me._Prm.ReturnFlg = True

                '終了処理  
                frm.Close()

            Case LMC020C.EventShubetsu.UNSO    '運送修正
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                Else
                    '請求鑑ヘッダチェック
                    If Me.CheckKagamiData(frm, eventShubetsu, frm.FunctionKey.F7ButtonName) = False Then
                        Exit Sub
                    End If
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                'シチュエーションラベルの設定
                mode = DispMode.EDIT
                status = RecordStatus.NOMAL_REC
                Me._G.SetSituation(mode, status)

                'ファンクションキーの設定
                Me._G.SetFunctionKey(LMC020C.MODE_UNSO, Me._Ds)

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '画面の入力項目の制御
                Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                    "SYS_DEL_FLG = '0'"))

                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 Start
                'Me._G.SetControlsStatus(LMC020C.MODE_UNSO, outDs.Length)
                Me._G.SetControlsStatus(LMC020C.MODE_UNSO, outDs.Length, Me._Ds)
                '要望番号1369:(支払-運送会社変更時、タリフを再取得し、再計算をおこなう⇒運行が紐付いている場合には、運行の運送会社変更時のみ動作) 2012/08/24 本明 End


                'ボタンの設定
                Call Me._G.SetBtn(LMC020C.MODE_UNSO, _NoChangeOutkaM)

                'リターンフラグ設定
                Me._Prm.ReturnFlg = True

            Case LMC020C.EventShubetsu.SHUSAN    '終算日修正
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                Else
                    '請求鑑ヘッダチェック
                    If Me.CheckKagamiData(frm, eventShubetsu, frm.FunctionKey.F8ButtonName) = False Then
                        Exit Sub
                    End If
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                'シチュエーションラベルの設定
                mode = DispMode.EDIT
                status = RecordStatus.NOMAL_REC
                Me._G.SetSituation(mode, status)

                'ファンクションキーの設定
                Me._G.SetFunctionKey(LMC020C.MODE_SHUSAN, Me._Ds)

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '画面の入力項目の制御
                Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                    "SYS_DEL_FLG = '0'"))
                Me._G.SetControlsStatus(LMC020C.MODE_SHUSAN, outDs.Length)

                'ボタンの設定
                Call Me._G.SetBtn(LMC020C.MODE_SHUSAN, _NoChangeOutkaM)

                'リターンフラグ設定
                Me._Prm.ReturnFlg = True

            Case LMC020C.EventShubetsu.MASTER, _
                 LMC020C.EventShubetsu.MASTER_ENTER 'マスタ参照

                '要望番号1595 yamanaka 2012.11.19 Start
                If frm.ActiveControl.Name.Equals("txtGoodsCdCust") = True _
                OrElse frm.ActiveControl.Name.Equals("lblGoodsNm") = True Then
                    If frm.txtGoodsCdCust.ReadOnly = True OrElse frm.lblGoodsNm.ReadOnly = True Then
                        Exit Sub
                    End If
                End If
                '要望番号1595 yamanaka 2012.11.19 End

                '2014/01/30 輸出情報追加 START
                If frm.ActiveControl.Name.Equals("txtShipperCd") = True Then
                    If frm.txtShipperCd.ReadOnly = True Then
                        Exit Sub
                    End If
                End If
                '2014/01/30 輸出情報追加 END

                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                Dim ds As DataSet = Me.ShowPopup(frm, eventShubetsu.ToString, prm)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り件数が0件の場合は終了
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '要望対応:1595 yamanaka 2012.11.15 Start
                Select Case frm.ActiveControl.Name
                    Case "txtGoodsCdCust", "lblGoodsNm"
                        Me.SetPopupGoodsReturn(frm, eventShubetsu, prm)

                    Case Else
                        Me.SetPopupReturn(frm, eventShubetsu.ToString, prm)

                End Select

                Dim outdt As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                    "AND SYS_DEL_FLG = '0'"))
                Call Me._G.ChangeGoodsLock(outdt.Count)

                'Me.SetPopupReturn(frm, eventShubetsu.ToString, prm)
                '要望対応:1595 yamanaka 2012.11.15 End

            Case LMC020C.EventShubetsu.HOZON    '保存

                

                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                'エラーメッセージ表示欄クリア
                MyBase.ClearMessageAria(frm)

                '2018/12/07 ADD START 要望管理002171
                Dim bErrCalcOutkaPkgNb As Boolean = False
                '出荷梱包個数自動計算用データテーブル初期化
                Call Me.InitCalcOutkaPkgNbIn(frm)
                '2018/12/07 ADD END   要望管理002171

                '出荷大の梱包個数の設定
                If Me._G.OutLkonpoCtl(Me._Ds) = False Then
                    Exit Sub
                End If

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                'データセット設定
                If Me.SaveDataset(frm, Me._Ds) = False Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub

                End If

                '現場指示ステータスを設定
                Me.SetWhhSijiStatus(frm)

                '要望番号:1520 KIM 2012/10/18 START
                If Me.ChkUntinCalDate(frm) = False Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub

                End If
                '要望番号:1520 KIM 2012/10/18 END

                '保管・荷役料最終計算日チェック
                If Me.IsHokanryoChk(frm, frm.FunctionKey.F11ButtonName) = False Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                

                '項目チェック
                If Me._V.IsKanrenCheck2(eventShubetsu, Me._Ds) = False Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                '引当在庫数のチェック処理
                Dim chkFlg As Boolean = Me._V.IsZaikoCheck(Me._Ds)
                If chkFlg = False Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                '2019/12/16 要望管理009513 add start
                '出荷(小)と商品マスタの入目違いで警告
                If Not Me.IrimeCheck(frm, Me._Ds) Then
                    '確認メッセージ
                    Dim rtn As MsgBoxResult = MyBase.ShowMessage(frm, "W150", New String() {"目欠引当", "保存"})
                    If rtn <> MsgBoxResult.Ok Then
                        ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                        Me._G.ClearExportLSet(Me._Ds)

                        Me._LMCconH.EndAction(frm)
                        Exit Sub
                    End If
                End If
                '2019/12/16 要望管理009513 add end

                ' 保存時、分納かつ作業進捗が「予定入力済」のままの場合「検品済」に変更するか否かの確認
                ' (保存後、出荷完了を行い次回分納データの作成を行いたいか否かの確認)
                If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
                    If Not ChkBunnouOutkaStateKb(frm, Me._Ds) Then
                        ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                        Me._G.ClearExportLSet(Me._Ds)

                        Me._LMCconH.EndAction(frm)
                        Exit Sub
                    End If
                End If

                '要望番号:1511 KIM 2012/10/12 START
                '要望番号:1350 terakawa 2012.08.27 Start
                '同一置き場（同一商品・ロット）チェック処理
                'If Me.GoodsLotCheck(frm, Me._Ds) = False Then
                '    Me._LMCconH.EndAction(frm)
                '    Exit Sub
                'End If
                '要望番号:1350 terakawa 2012.08.27 End
                '要望番号:1511 KIM 2012/10/12 END

                ' 温度管理アラートチェック
                Call IsOndoKanriAlertCheck(frm, Me._Ds)

                '出荷梱包個数自動計算の実行確認
                Call Me.ConfirmAutoCalcOutkaPkgNb(frm)  '2018/12/07 ADD 要望管理002171


                Dim rtnDs As DataSet = Nothing

                '確認メッセージ
                'Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C001", New String() {"保存"})
                Dim rtnResult As MsgBoxResult = MsgBoxResult.Ok
                Me._Ds.Merge(New RdPrevInfoDS)
                Me._Ds.Tables(LMConst.RD).Clear()

                '要望番号:1339 yamanaka 2012.09.13 修正Start
                Dim csvFlg As String = Me.DicCsvMakeChk(frm)
                If csvFlg = LMC020C.OUTKA_CANCEL Then
                    Me.DicCsvMake(frm, Me._Ds, csvFlg)
                End If
                '要望番号:1339 yamanaka 2012.09.13 修正End

                ''要望番号:1339 yamanaka 2012.08.28 Start
                'Dim csvFlg As String = Me.DicCsvMakeChk(frm)
                ''要望番号:1339 yamanaka 2012.08.28 End


                If rtnResult = MsgBoxResult.Ok Then
                    'INS START 2023/06/23 035566【LMS】住友ファーマ 端数出荷時出荷オーダー単位の作業コード設定
                    Me.SetHasuOutkaSagyoMDataSet(frm)
                    'INS END   2023/06/23 035566【LMS】住友ファーマ 端数出荷時出荷オーダー単位の作業コード設定

                    ' 分納の場合の出荷M 個数・数量項目再設定
                    If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
                        Dim outkaMPkgNbChanged As Boolean = False
                        SetBunnouNbQtOutkaM(frm, Me._Ds)
                        ' 分納の場合の出荷L 出荷梱包個数 再設定
                        SetBunnouNbQtOutkaL(frm, Me._Ds)
                    End If

                    '保存処理呼び出し
                    rtnDs = Me.SaveControl(frm)

                    '出荷梱包個数自動計算のエラーチェック
                    bErrCalcOutkaPkgNb = IsErrCalcOutkaPkgNb(rtnDs)     '2018/12/07 ADD 要望管理002171
                Else
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    '出荷梱包個数自動計算のフラグクリア(保存処理以外で自動計算されないようにするため)
                    Me._Ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Clear()    '2018/12/07 ADD 要望管理002171

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub

                End If

                '2018/12/07 ADD START 要望管理002171
                '出荷梱包個数自動計算のフラグクリア(保存処理以外で自動計算されないようにするため)
                rtnDs.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Clear()
                Me._Ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Clear()
                '2018/12/07 ADD END   要望管理002171

                'エラーがある場合、終了
                If MyBase.IsMessageExist() = True Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    'メッセージ表示
                    MyBase.ShowMessage(frm)

                    '処理終了アクション 
                    Me._LMCconH.EndAction(frm)
                    Exit Sub

                End If

                'LOG


                '要望番号:1454 yamanaka 2012.09.20 Start
                If csvFlg = LMC020C.OUTKA_SASHIZU Then
                    Me.DicCsvMake(frm, rtnDs, csvFlg)
                End If
                '要望番号:1454 yamanaka 2012.09.20 End

                '更新結果の反映
                Me._Ds = rtnDs

                '出荷チェックリスト用にデータセットを複写
                Dim dsOutboundCheck As DataSet = _Ds.Copy

                'START YANAI 要望番号736
                '分析票処理
                Call Me.BunsekiPrintDataAction(frm)
                'END YANAI 要望番号736

                'プレビュー判定 
                Dim prevDt As DataTable = Me._Ds.Tables(LMConst.RD)
                'プレビューの生成
                Dim prevFrm As New RDViewer()

                If prevDt.Rows.Count > 0 Then

                    'データ設定
                    prevFrm.DataSource = prevDt

                    'プレビュー処理の開始
                    prevFrm.Run()

                    'プレビューフォームの表示
                    prevFrm.Show()

                    'フォーカス設定
                    prevFrm.Focus()

                End If

                '出荷チェックリスト処理
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS) _
                        .Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, "' AND SUB_KB = 'A8' AND SET_NAIYO = '1'"))

                If custDetailsDr.Length > 0 Then
                    dsOutboundCheck = Me.OutboundCheckPrintDataAction(frm, dsOutboundCheck)

                    'プレビュー判定 
                    Dim prevDt2 As DataTable = dsOutboundCheck.Tables(LMConst.RD)
                    'プレビューの生成
                    Dim prevFrm2 As New RDViewer()

                    If prevDt2.Rows.Count > 0 Then

                        'データ設定
                        prevFrm2.DataSource = prevDt2

                        'プレビュー処理の開始
                        prevFrm2.Run()

                        'プレビューフォームの表示
                        prevFrm2.Show()

                        'フォーカス設定
                        prevFrm2.Focus()

                    End If
                End If

                'データセット再設定処理
                Me.datasetSetting(Me._Ds)

                '比較用DSを最新化
                _DsCmpr.Clear()
                _DsCmpr = _Ds.Copy

                '採番した出荷管理番号(大)戻り値を画面に設定
                Dim dt As DataTable = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L)
                Call Me._G.SetOutkaLkanriNo(dt)

                '採番した運送番号戻り値を画面に設定
                dt = Me._Ds.Tables(LMC020C.TABLE_NM_UNSO_L)
                Call Me._G.SetUnsoLkanriNo(dt)


                '2018/12/07 ADD START 要望管理002171
                Dim msg As String = String.Empty
                '出荷梱包個数自動計算がエラーの場合
                If bErrCalcOutkaPkgNb Then
                    msg = " 商品明細マスタの設定に誤りがあるため出荷梱包個数を自動計算できませんでした。"
                End If
                '2018/12/07 ADD END   要望管理002171

                'メッセージの表示   '2018/12/07 要望管理002171 変数msgを追加
                MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), String.Concat("[", frm.lblSyukkaLNoL.Text, " = ", frm.lblSyukkaLNo.TextValue, "]", msg)})

                '処理終了アクション
                Me._LMCconH.EndAction(frm)



                '参照モードにする
                'コントロール個別設定
                Me._SyokiFlg = True
                Call Me._G.ClearControlL(Me.SysData(LMC020C.SysData.YYYYMMDD))
                Me._SyokiFlg = False
                Call Me._G.ClearControlM()

                'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                Me._G.InitSpreadM()
                Me._G.InitSpreadS()

                'シチュエーションラベルの設定
                mode = DispMode.VIEW
                status = RecordStatus.NOMAL_REC
                Me._G.SetSituation(mode, status)

                '初期表示時の値設定
                Me._SyokiFlg = True
                Me._G.SetInitValue(Me._Ds)
                'Me._G.SetInitValue(Me._Ds, activeCol)
                Me._SyokiFlg = False


                'スプレッド選択行の詳細（出荷(中)）を表示()
                Call Me._G.SetOutkaM(0, Me._Ds)

                'スプレッド選択行の詳細（作業(中)）を表示()
                Call Me._G.SetSagyoMControl(0, Me._Ds)

                'スプレッド選択行の詳細（出荷(小)）を表示
                Call Me._G.SetSpread(frm.sprDtl, Me._Ds, LMConst.FLG.OFF, -1)

                'START YANAI 要望番号853 まとめ処理対応
                ''引当可能個数・数量のグループ化
                'Call Me._G.SpreadGroup(Me._Ds)
                'END YANAI 要望番号853 まとめ処理対応

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(mode)

                '顧客指示変更不可フラグ 退避
                With ""
                    Dim nrsBrCd As String = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("NRS_BR_CD").ToString()
                    Dim custCdL As String = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("CUST_CD_L").ToString()
                    Dim custCdM As String = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("CUST_CD_M").ToString()
                    Dim dr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'WC05' AND KBN_NM1 = '", nrsBrCd, "' AND KBN_NM2 = '", custCdL, "' AND KBN_NM3 = '", custCdM, "'"))
                    If dr.Length = 0 Then
                        _NoChangeOutkaM = False
                    Else
                        _NoChangeOutkaM = True
                    End If
                End With

                'ボタンの設定
                Call Me._G.SetBtn(mode, _NoChangeOutkaM)

                '画面の入力項目の制御
                Call Me._G.SetControlsStatus(mode, Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count)

                'LOG
                MyBase.Logger.WriteLog(0, "LMC010H", "ActionControl", "16-☆保存")

                'タブのフォーカス設定
                'Call Me._G.SetTabFoucus()

                'プレビューにフォーカスを設定するため
                If prevDt.Rows.Count > 0 Then
                    'フォーカス設定
                    prevFrm.Focus()
                Else
                    'フォーカスの設定
                    Call Me._G.SetFoucus()
                End If

                'リターンフラグ設定
                Me._Prm.ReturnFlg = True

                'LOG
                MyBase.Logger.WriteLog(0, "LMC010H", "ActionControl", "17-☆保存")

            Case LMC020C.EventShubetsu.PRINT    '印刷

                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsMeitetsuCheck(eventShubetsu, Me._Ds) = False Then            'ADD 2017/08/03 名鉄時チェック 
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '2012.12.14 UTI船積確認書対応START
                If (frm.cmbPRINT.SelectedValue.ToString()).Equals("10") = True Then
                    ''メイン処理(SQLの取得)
                    Call Me.getSqlDataAction(frm)
                Else
                    'メイン処理
                    Call Me.PrintDataAction(frm)
                End If
                '2012.12.14 UTI船積確認書対応END

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

            Case LMC020C.EventShubetsu.TODOKESAKI    '届先
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '届先マスタメンテを呼ぶ
                Me.ShowTodokeMst(frm, prm)

                '届先名称を取得
                Me.GetCachedDest(frm)

                '処理終了アクション
                Me._LMCconH.EndAction(frm)


            Case LMC020C.EventShubetsu.JIKKOU    '実行

                If frm.cmbJikkou.SelectedValue.ToString() = "04" Then
                    ' 「分納出荷」

                    '「編集」処理の実行
                    Me.ActionControl(LMC020C.EventShubetsu.HENSHU, frm)

                    If frm.FunctionKey.F2ButtonEnabled = False Then
                        ' 編集モードに移行した場合

                        ' 【出荷種別】を「分納」とする
                        frm.cmbSyukkaSyubetu.SelectedValue = "60"
                        ' 「分納出荷」の場合の【出荷種別】選択肢固定化
                        frm.cmbSyukkaSyubetu.ReadOnly = True
                    End If

                    Exit Sub
                End If

                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                Select Case frm.cmbJikkou.SelectedValue.ToString
                    Case "01"   '文書管理
                        '文書管理を呼ぶ
                        Me.ShowBunshoKanri(frm, prm)

                    Case "02"   '指示取り消し
                        Me.WHSagyoShiji(frm, prm, LMC930C.PROC_TYPE.CANCEL)

                    Case "03"   '指示
                        Me.WHSagyoShiji(frm, prm, LMC930C.PROC_TYPE.INSTRUCT)

                End Select

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

            Case LMC020C.EventShubetsu.RIREKI    '履歴照会
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '在庫履歴照会を呼ぶ
                Me.ShowZaiRireki(frm, prm)

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

            Case LMC020C.EventShubetsu.INS_M    '追加(中)

                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                If 0 < frm.sprSyukkaM.ActiveSheet.Rows.Count Then
                    '項目チェック
                    If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                           Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                           Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                           Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                        Exit Sub
                    End If

                ElseIf 0 = frm.sprSyukkaM.ActiveSheet.Rows.Count Then
                    '項目チェック
                    If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                           Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                           Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                           Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                        Exit Sub
                    End If

                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then
                    '出荷管理番号(中・小）の採番
                    Me._G.SetOutNoDataSet(Me._Ds)

                    '出荷データ(中)をDataSetに更新
                    Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                    '出荷データ(小)をDataSetに更新
                    'START YANAI 20110913 小分け対応
                    'Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                    Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                    'END YANAI 20110913 小分け対応

                    '作業(中レコード)をDataSetに更新
                    Me._G.SetSagyoMDataSet(Me._Ds)

                End If

                '在庫テーブル照会POP表示
                Dim ds As DataSet = Me.ShowPopup(frm, LMC020C.EventShubetsu.INS_M.ToString(), prm)
                Dim dt As DataTable = ds.Tables(LMControlC.LMD100C_TABLE_NM_OUT)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り値が無い場合は終了

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)

                    Exit Sub
                End If
                'コントロールクリア
                Call Me._G.ClearControlM()

                'スプレッドのクリア
                Me._G.InitSpreadS()

                Dim taninusiFlg As Boolean = False  '他荷主処理のフラグ

                If dt.Rows.Count = 0 Then
                    'dtが設定されていない場合は他荷主処理になる
                    taninusiFlg = True
                End If

                If taninusiFlg = False Then
                    '戻り値をスプレッドに設定
                    Call Me._G.AddOutkaMspread(frm.sprSyukkaM, dt)

                    'スプレッド選択行の詳細（出荷(中)）を表示()
                    Call Me._G.AddOutkaM(dt)

                    '検索条件をクリア
                    Call Me._G.ClearControlSearch()

                End If

                '引当処理を呼ぶ
                'START YANAI 要望番号545
                'Me.ShowHIKIATEG(frm, prm, False, dt, taninusiFlg)
                Me.ShowHIKIATEG(frm, prm, False, dt, taninusiFlg, eventShubetsu)
                'END YANAI 要望番号545

                'スプレッドのスクロール
                Me.SetEndScrollSyukkaM(frm)
                Me.SetEndScrollDtl(frm)

                '出荷管理番号(中・小）の採番
                Me._G.SetOutNoDataSet(Me._Ds)

                '出荷データ(中)をDataSetに更新
                Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                '2015.07.08 協立化学　シッピングマーク対応　追加START
                'シッピングマーク(HED)の情報を設定
                Call Me._G.SetMarkHedControl(Me._Ds, -1, eventShubetsu)
                '2015.07.08 協立化学　シッピングマーク対応　追加END

                '出荷データ(小)をDataSetに更新
                'START YANAI 20110913 小分け対応
                'Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                'END YANAI 20110913 小分け対応

                '作業(中レコード)をDataSetに更新
                Me._G.SetSagyoMDataSet(Me._Ds)

                '要望番号:1731 terakawa 2013.01.15 Start
                If (frm.optKowake.Checked = True OrElse frm.optSample.Checked = True) = True Then
                    'サンプル作業コード設定
                    Me._G.SetSagyoMSample()

                    '出荷(大)梱包個数設定
                    Me._G.SetOutLKonpo()
                End If
                '要望番号:1731 terakawa 2013.01.15 End

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '要望対応:1595 yamanaka 2012.11.15 Start
                Dim outdt As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                    "AND SYS_DEL_FLG = '0'"))
                Call Me._G.ChangeGoodsLock(outdt.Count)
                '要望対応:1595 yamanaka 2012.11.15 End  

                '倉庫のロック制御
                Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                    "SYS_DEL_FLG = '0'"))
                Me._G.SetSoko(outDs.Length)

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

            Case LMC020C.EventShubetsu.COPY_M    '複写(中)
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMCconH.GetCheckList(frm.sprSyukkaM.ActiveSheet, LMC020G.sprSyukkaM.DEFM.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    Me._LMCconH.StartAction(frm)

                    If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then
                        '出荷管理番号(中・小）の採番
                        Me._G.SetOutNoDataSet(Me._Ds)

                        '出荷データ(中)をDataSetに更新
                        Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                        '出荷データ(小)をDataSetに更新
                        'START YANAI 20110913 小分け対応
                        'Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                        Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                        'END YANAI 20110913 小分け対応

                        '作業(中レコード)をDataSetに更新
                        Me._G.SetSagyoMDataSet(Me._Ds)

                    End If

                    '複写処理（出荷(中)）
                    Call Me._G.CopyOutkaM(frm.sprSyukkaM, Me._Ds)

                    'コントロール個別設定
                    Call Me._G.ClearControlM()

                    'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                    Me._G.InitSpreadS()

                    'スプレッドコピー行の詳細（出荷(中)）を表示
                    Call Me._G.SetOutkaM(frm.sprSyukkaM.ActiveSheet.Rows.Count - 1, Me._Ds)

                    '複写処理（作業(中)）
                    Call Me._G.CopySagyoM(frm.sprSyukkaM, Me._Ds)

                    'メッセージの表示
                    MyBase.ShowMessage(frm, "G003")

                    '引当処理を呼ぶ
                    'START YANAI 要望番号545
                    'Me.ShowHIKIATEG(frm, prm, False, Nothing, False)
                    Me.ShowHIKIATEG(frm, prm, False, Nothing, False, eventShubetsu)
                    'END YANAI 要望番号545

                    'スプレッドのスクロール
                    Me.SetEndScrollSyukkaM(frm)
                    Me.SetEndScrollDtl(frm)

                    '処理終了アクション
                    Call Me._LMCconH.EndAction(frm)

                    '要望対応:1595 yamanaka 2012.11.15 Start
                    Dim outdt As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                        "AND SYS_DEL_FLG = '0'"))
                    Call Me._G.ChangeGoodsLock(outdt.Count)
                    '要望対応:1595 yamanaka 2012.11.15 End  

                    '倉庫のロック制御
                    Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                        "SYS_DEL_FLG = '0'"))
                    Me._G.SetSoko(outDs.Length)

                    '画面の入力項目の制御
                    Me._G.SetKosuSuryo()

                End If

            Case LMC020C.EventShubetsu.DEL_M    '削除(中)
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMCconH.GetCheckList(frm.sprSyukkaM.ActiveSheet, LMC020G.sprSyukkaM.DEFM.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    Me._LMCconH.StartAction(frm)

                    If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then
                        '出荷管理番号(中・小）の採番
                        Me._G.SetOutNoDataSet(Me._Ds)

                        '出荷データ(中)をDataSetに更新
                        Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                        '出荷データ(小)をDataSetに更新
                        'START YANAI 20110913 小分け対応
                        'Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                        Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                        'END YANAI 20110913 小分け対応

                        '作業(中レコード)をDataSetに更新
                        Me._G.SetSagyoMDataSet(Me._Ds)

                    End If

                    '削除処理（データセット）
                    Call Me._G.DelSagyoMDataSet(frm.sprSyukkaM, Me._Ds)
                    Call Me._G.DelOutSDataSet(frm.sprSyukkaM, Me._Ds)
                    Call Me._G.DelOutMDataSet(frm.sprSyukkaM, Me._Ds)
                    '2015.07.08 協立化学　シッピング対応 追加START
                    Call Me._G.DelMarkHedDataSet(frm.sprSyukkaM, Me._Ds)
                    '2015.07.08 協立化学　シッピング対応 追加END

                    '2015.07.08 協立化学　シッピング対応 追加START
                    Call Me._G.DelMarkDtlDataSet(frm.sprSyukkaM, Me._Ds)
                    '2015.07.08 協立化学　シッピング対応 追加END

                    '2014.07.08 (黎) WITのPIK_WK連携不要 --ST--
                    ''2014.04.09 (黎) WITのPIK_WK連動削除対応 --ST--
                    ''ピッキングWK(中レベル相当)の削除用データセット設定
                    'Call Me._G.DelPickWkMDataSet(frm.sprSyukkaM, Me._Ds)
                    ''2014.04.09 (黎) WITのPIK_WK連動削除対応 --ED--
                    '2014.07.08 (黎) WITのPIK_WK連携不要 --ED--

                    'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                    Call Me._G.InitSpreadM()
                    Call Me._G.InitSpreadS()

                    'コントロール個別設定
                    Call Me._G.ClearControlM()
                    '2015.07.08 協立化学　シッピング対応 追加START
                    Call Me._G.ClearControlMarkHed()
                    '2015.07.08 協立化学　シッピング対応 追加END

                    '出荷(中)スプレッドの再描画
                    Me._G.SetSpread(frm.sprSyukkaM, Me._Ds, LMConst.FLG.OFF, -1)
                    If frm.sprSyukkaM.ActiveSheet.RowCount <> 0 Then
                        'スプレッド選択行の詳細（出荷(中)）を表示
                        Call Me._G.SetOutkaM(0, Me._Ds)
                        'スプレッド選択行の詳細（作業(中)）を表示()
                        Call Me._G.SetSagyoMControl(0, Me._Ds)
                        'スプレッド選択行の詳細（出荷(小)）を表示
                        Call Me._G.SetSpread(frm.sprDtl, Me._Ds, LMConst.FLG.OFF, 0)

                        '2015.07.08 協立化学　シッピングマーク対応　追加START
                        'シッピングマーク(HED)の情報を設定
                        Call Me._G.SetMarkHedControl(Me._Ds, -1, eventShubetsu)
                        '2015.07.08 協立化学　シッピングマーク対応　追加END

                    End If

                    '引当残・引当済を求める
                    Call Me.SetHikiateKosuSuryo()

                    'メッセージの表示
                    MyBase.ShowMessage(frm, "G003")

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)

                    '要望対応:1595 yamanaka 2012.11.15 Start
                    Dim outdt As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                        "AND SYS_DEL_FLG = '0'"))
                    Call Me._G.ChangeGoodsLock(outdt.Count)
                    '要望対応:1595 yamanaka 2012.11.15 End  

                    '倉庫のロック制御
                    Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                          "SYS_DEL_FLG = '0'"))
                    Me._G.SetSoko(outDs.Length)

                    '画面の入力項目の制御
                    Me._G.SetKosuSuryo()

                End If

            Case LMC020C.EventShubetsu.DEL_S    '削除(小)
                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                'チェックリスト取得
                Dim arr As ArrayList = Nothing
                arr = Me._LMCconH.GetCheckList(frm.sprDtl.ActiveSheet, LMC020G.sprDtl.DEF.ColNo)

                If 0 < arr.Count Then

                    '処理開始アクション
                    Me._LMCconH.StartAction(frm)

                    If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then
                        '出荷管理番号(中・小）の採番
                        Me._G.SetOutNoDataSet(Me._Ds)

                        '出荷データ(中)をDataSetに更新
                        Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                        '出荷データ(小)をDataSetに更新
                        'START YANAI 20110913 小分け対応
                        'Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                        Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                        'END YANAI 20110913 小分け対応

                        '作業(中レコード)をDataSetに更新
                        Me._G.SetSagyoMDataSet(Me._Ds)

                    End If

                    '2014.07.08 (黎) WITのPIK_WK連携不要 --ST--
                    ''2014.04.09 (黎) WITのPIK_WK連動削除対応 --ST--
                    ''ピッキングWK(小レベル相当)の削除用データセット設定
                    'Call Me._G.DelPickWkSDataSet(frm.sprDtl, Me._Ds)
                    ''2014.04.09 (黎) WITのPIK_WK連動削除対応 --ED--
                    ''2014.07.08 (黎) WITのPIK_WK連携不要 --ED--

                    '削除処理（データセット）
                    Call Me._G.DelOutSDataSet(frm.sprDtl, Me._Ds)

                    'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                    Call Me._G.InitSpreadS()

                    '出荷(中)スプレッドの再描画
                    Me._G.SetSpread(frm.sprDtl, Me._Ds, LMConst.FLG.OFF, -1)

                    '引当状況の更新
                    Dim outSdr As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat( _
                                                                               "NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                               "OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "' AND ", _
                                                                               "SYS_DEL_FLG = '0'"))
                    Call Me._G.ChangeHikiate(outSdr.Length)

                    '要望対応:1595 yamanaka 2012.11.15 Start
                    Call Me._G.ChangeGoodsLock(outSdr.Count)
                    '要望対応:1595 yamanaka 2012.11.15 End              

                    '引当残・引当済を求める
                    Call Me.SetHikiateKosuSuryo()

                    'START YANAI 要望番号853 まとめ処理対応
                    ''引当可能個数・数量のグループ化
                    'Call Me._G.SpreadGroup(Me._Ds)
                    'END YANAI 要望番号853 まとめ処理対応

                    'メッセージの表示
                    MyBase.ShowMessage(frm, "G003")

                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)

                    '倉庫のロック制御
                    Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                        "SYS_DEL_FLG = '0'"))
                    Me._G.SetSoko(outDs.Length)

                    '画面の入力項目の制御
                    Me._G.SetKosuSuryo()

                End If

            Case LMC020C.EventShubetsu.HENKO     '一括変更

                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                '印順一括変更
                Me._G.SetPrintSort(Me._Ds)

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                'リターンフラグ設定
                Me._Prm.ReturnFlg = True

            Case LMC020C.EventShubetsu.DOUBLE_CLICK     '出荷(中)ダブルクリック

                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                '項目チェック
                If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(eventShubetsu, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                If String.IsNullOrEmpty(frm.txtGoodsCdCust.TextValue) = False Then

                    '出荷管理番号(中・小）の採番
                    Dim rtnResult As Boolean = Me._G.SetOutNoDataSet(Me._Ds)

                    '出荷データ(中)をDataSetに更新
                    rtnResult = rtnResult AndAlso Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

                    '出荷データ(小)をDataSetに更新
                    'START YANAI 20110913 小分け対応
                    'rtnResult = rtnResult AndAlso Me._G.SetOutSDataSet(Me._Ds, eventShubetsu)
                    rtnResult = rtnResult AndAlso Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)
                    'END YANAI 20110913 小分け対応

                    '作業(中レコード)をDataSetに更新
                    rtnResult = rtnResult AndAlso Me._G.SetSagyoMDataSet(Me._Ds)

                End If

                'コントロール個別設定
                Call Me._G.ClearControlM()

                'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                Me._G.InitSpreadS()

                'スプレッド選択行の詳細（出荷(中)）を表示()
                Dim rowNo As Integer = frm.sprSyukkaM.Sheets(0).ActiveRow.Index
                Call Me._G.SetOutkaM(rowNo, Me._Ds)

                'スプレッド選択行の詳細（作業(中)）を表示()
                Call Me._G.SetSagyoMControl(rowNo, Me._Ds)

                '2015.07.08 協立化学　シッピングマーク対応 追加START
                'コントロール個別設定
                Call Me._G.ClearControlMarkHed()

                'シッピングマーク(HED)の情報を設定
                Call Me._G.SetMarkHedControl(Me._Ds, rowNo, eventShubetsu)
                '2015.07.08 協立化学　シッピングマーク対応 追加END

                'スプレッド選択行の詳細（出荷(小)）を表示
                Call Me._G.SetSpread(frm.sprDtl, Me._Ds, LMConst.FLG.OFF, -1)

                'START YANAI 要望番号853 まとめ処理対応
                ''引当可能個数・数量のグループ化
                'Call Me._G.SpreadGroup(Me._Ds)
                'END YANAI 要望番号853 まとめ処理対応

                'メッセージの表示
                MyBase.ShowMessage(frm, "G003")

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '要望対応:1595 yamanaka 2012.11.15 Start
                Dim outdt As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("OUTKA_NO_M = '", frm.lblSyukkaMNo.TextValue, "'", _
                                                                                                    "AND SYS_DEL_FLG = '0'"))
                Call Me._G.ChangeGoodsLock(outdt.Count)
                '要望対応:1595 yamanaka 2012.11.15 End  

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

            Case LMC020C.EventShubetsu.LEAVE_CELL     '出荷(中)ロストフォーカス

                'コントロール個別設定
                Call Me._G.ClearControlM()

                'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                Me._G.InitSpreadS()

                'スプレッド選択行の詳細（出荷(中)）を表示()
                Call Me._G.SetOutkaM(frm.sprSyukkaM.Sheets(0).ActiveRow.Index, Me._Ds)

                'スプレッド選択行の詳細（作業(中)）を表示()
                Call Me._G.SetSagyoMControl(frm.sprSyukkaM.Sheets(0).ActiveRow.Index, Me._Ds)

                '2015.07.08 協立化学　シッピングマーク対応 追加START
                'コントロール個別設定
                Call Me._G.ClearControlMarkHed()

                'シッピングマーク(HED)の情報を設定
                Call Me._G.SetMarkHedControl(Me._Ds, frm.sprSyukkaM.Sheets(0).ActiveRow.Index, eventShubetsu)
                '2015.07.08 協立化学　シッピングマーク対応 追加END

                ''スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
                'Call Me._G.InitSpreadS()
                'スプレッドの行をクリア
                'frm.sprDtl.CrearSpread()

                'スプレッド選択行の詳細（出荷(小)）を表示
                Call Me._G.SetSpread(frm.sprDtl, Me._Ds, LMConst.FLG.OFF, -1)

                'START YANAI 要望番号853 まとめ処理対応
                ''引当可能個数・数量のグループ化
                'Call Me._G.SpreadGroup(Me._Ds)
                'END YANAI 要望番号853 まとめ処理対応

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                '    '2015.07.08 協立化学　シッピングマーク対応 追加START

            Case LMC020C.EventShubetsu.TAB_LEAVE     'マークタブ　LEAVE

                With frm

                    For i As Integer = 0 To .sprSyukkaM.ActiveSheet.RowCount - 1

                        If .txtOutkaNoM.TextValue.ToString().Equals(Me._LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, LMC020G.sprSyukkaM.KANRI_NO.ColNo))) = True Then

                            If Me._Ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, LMC020G.sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'")).Length = 0 Then

                                If Convert.ToInt32(.numCaseNoFrom.Value()) = 0 AndAlso Convert.ToInt32(.numCaseNoTo.Value()) = 0 AndAlso String.IsNullOrEmpty(.txtMarkInfo1.TextValue) = True _
                                    AndAlso String.IsNullOrEmpty(.txtMarkInfo2.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo3.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo4.TextValue) = True _
                                    AndAlso String.IsNullOrEmpty(.txtMarkInfo5.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo6.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo7.TextValue) = True _
                                    AndAlso String.IsNullOrEmpty(.txtMarkInfo8.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo9.TextValue) = True AndAlso String.IsNullOrEmpty(.txtMarkInfo10.TextValue) = True Then
                                Else
                                    'マーク(HED)をDataSetに更新
                                    Me._G.SetMarkHedDataSet(Me._Ds, i, frm)
                                End If

                            Else
                                '2015.08.07 協立化学　マーク対応 追加START
                                Dim hedRow As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select(String.Concat("OUTKA_NO_M = '", _LMCconG.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, LMC020G.sprSyukkaM.KANRI_NO.ColNo)), "' AND " _
                                                                                  , "SYS_DEL_FLG = '0'"))
                                hedRow(0).Item("CASE_NO_FROM") = .numCaseNoFrom.Value()
                                hedRow(0).Item("CASE_NO_TO") = .numCaseNoTo.Value()
                                '2015.08.07 協立化学　マーク対応 追加END
                                .sprSyukkaM.SetCellValue(i, LMC020G.sprSyukkaM.CASE_NO_FROM.ColNo, .numCaseNoFrom.Value)
                                .sprSyukkaM.SetCellValue(i, LMC020G.sprSyukkaM.CASE_NO_TO.ColNo, .numCaseNoTo.Value)
                            End If
                            'マーク(DTL)をDataSetに更新
                            Me._G.SetMarkDataSet(Me._Ds, i, eventShubetsu)
                        End If

                    Next

                End With
                '2015.07.08 協立化学　シッピングマーク対応 追加END

            Case LMC020C.EventShubetsu.CHANGE_KOSU     '梱数・端数変更時

                '個数・数量を求める
                Call Me.ChangeKosu()

            Case LMC020C.EventShubetsu.CHANGE_IRIME    '入目変更時


            Case LMC020C.EventShubetsu.CHANGE_SURYO     '数量変更時

                '個数・数量を求める
                Call Me.ChangeSuryo()

            Case LMC020C.EventShubetsu.OPT_KOSU      '出荷単位変更（個数）

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

            Case LMC020C.EventShubetsu.OPT_SURYO     '出荷単位変更（数量）

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

            Case LMC020C.EventShubetsu.OPT_KOWAKE    '出荷単位変更（小分け）

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

            Case LMC020C.EventShubetsu.OPT_SAMPLE    '出荷単位変更（サンプル）

                '画面の入力項目の制御
                Me._G.SetKosuSuryo()

            Case LMC020C.EventShubetsu.UNSO_CHANGE  '運送手配変更時

                If Me._SyokiFlg = False Then

                    '画面の入力項目の制御
                    Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                        "SYS_DEL_FLG = '0'"))
                    Call Me._G.SetControlsStatus(LMC020C.MODE_UNSO_CHANGE, outDs.Length)
                    '画面の項目のクリア
                    Call Me._G.ClearControlUnso()

                    '運送タリフセット取得
                    Me._G.SetUnsoTariffSet(False)

                    '運送会社取得（荷主マスタ→届先マスタ→運送会社マスタ 経由で取得）
                    Call Me._G.GetUnsoCompanyChangeDest(frm)

                    '運送課税区分取得
                    Call Me.SetUnchinUmuInitCd(frm)

                    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                    '運送会社コードOLD設定
                    Me._G.SetUnsoCdOld(frm)

                    '運賃タリフセットからタリフコードを設定
                    Call Me._G.GetUnchinTariffSet(frm, False)
                    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

                End If

            Case LMC020C.EventShubetsu.UNSOTARIFF_CHANGE  'タリフ分類区分変更時

                If Me._SyokiFlg = False Then

                    '画面の入力項目の制御
                    Dim outDs As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                        "SYS_DEL_FLG = '0'"))
                    Call Me._G.SetControlsStatus(LMC020C.MODE_UNSO_CHANGE, outDs.Length)

                    '画面の項目のクリア
                    Call Me._G.ClearControlUnso()

                    '運送会社取得（荷主マスタ→届先マスタ→運送会社マスタ 経由で取得）
                    Call Me._G.GetUnsoCompanyChangeDest(frm)

                    '運賃タリフ取得
                    Call Me.SetTariffInitCd(frm)

                    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                    '運送会社コードOLD設定
                    Me._G.SetUnsoCdOld(frm)

                    '運賃タリフセットからタリフコードを設定
                    Call Me._G.GetUnchinTariffSet(frm, True)
                    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

                End If

            Case LMC020C.EventShubetsu.LEAVE_DEST_CD     '届先コード変更時

                'START YANAI 要望番号909
                '届先コードOLD設定
                Me._G.SetDestCdOld(frm)
                'END YANAI 要望番号909

                '運送会社設定
                Me._G.GetUnsoCompanyChangeDest(frm)

                'START YANAI 要望番号745
                '売り上げ先設定
                Me._G.GetUriageChangeDest(frm)
                'END YANAI 要望番号745

                'START YANAI 要望番号880
                Call Me._G.GetTariffSet(frm)
                'END YANAI 要望番号880

                'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                '運送会社コードOLD設定
                Me._G.SetUnsoCdOld(frm)

                '運賃タリフセットからタリフコードを設定
                Call Me._G.GetUnchinTariffSet(frm, False)
                'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

            Case LMC020C.EventShubetsu.TODOKESAKI_CHANGE  '届先区分変更時

                If Me._SyokiFlg = False Then

                    '画面のロック制御
                    Call Me._G.SetControlsStatus(LMC020C.MODE_TODOKESAKI_CHANGE, -1)

                    '画面の項目のクリア
                    Call Me._G.ClearControlTodokesaki(Me._Ds)

                End If

            Case LMC020C.EventShubetsu.PRINT_CHANGE  '印刷種別変更時

                '部数の設定
                Call Me._G.ClearControlPrintBusu()

                Call Me._G.PrintCntSetControl()

            Case LMC020C.EventShubetsu.CHANGE_GOODS   '商品変更処理

                '項目チェック
                If Me._V.IsKanrenCheck(eventShubetsu, Me._Ds) = False Then
                    Exit Sub
                End If

                '処理開始アクション
                Me._LMCconH.StartAction(frm)

                Dim ds As DataSet = Me.ShowPopup(frm, eventShubetsu.ToString, prm)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り件数が0件の場合は終了
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)
                    Exit Sub
                End If

                '処理終了アクション
                Me._LMCconH.EndAction(frm)

                Me.SetPopupReturn(frm, eventShubetsu.ToString, prm)

                'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
            Case LMC020C.EventShubetsu.LEAVE_UNSO_CD     '運送会社コード変更時

                '運送会社コードOLD設定
                Me._G.SetUnsoCdOld(frm)

                '運賃タリフセットからタリフコードを設定
                Call Me._G.GetUnchinTariffSet(frm, False)
                'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

        End Select

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal saveMode As String) As DataSet

        
        '==== WSAクラス呼出（変更処理） ====
        Dim rtnDs As DataSet = Me.ServerAccess(ds, saveMode)
        
        Return rtnDs

    End Function

    ''' <summary>
    ''' 保存処理時のデータセット設定＋チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SaveDataset(ByVal frm As LMC020F, ByVal ds As DataSet) As Boolean

        '出荷管理番号(中・小）の採番
        Dim rtnResult As Boolean = Me._G.SetOutNoDataSet(ds)
     
        '出荷管理番号(大）をDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetOutLDataSet(ds)
     
        '出荷データ(中)をDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetOutMDataSet(ds, LMC020C.EventShubetsu.HOZON)
        
        '出荷データ(小)をDataSetに更新
        'START YANAI 20110913 小分け対応
        'rtnResult = rtnResult AndAlso Me._G.SetOutSDataSet(ds, LMC020C.EventShubetsu.HOZON)
        rtnResult = rtnResult AndAlso Me._G.SetOutSDataSet(ds, LMC020C.EventShubetsu.HOZON, Me._NewFlg)
        'END YANAI 20110913 小分け対応

        '作業(大レコード)をDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetSagyoLDataSet(ds)
        
        '作業(中レコード)をDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetSagyoMDataSet(ds)
        
        '運送(中レコード)をDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetUnsoMDataSet(ds)
        
        '運送(大レコード)をDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetUnsoLDataSet(ds)

        '2014/01/22 輸出情報追加 START
        rtnResult = rtnResult AndAlso Me._G.SetExportLSet(ds)
        '2014/01/22 輸出情報追加 END

        '要望番号:997 terakawa 2012.10.22 Start
        'EDI更新テーブルをDataSetに更新
        rtnResult = rtnResult AndAlso Me._G.SetEdiUpdTblDataSet(ds)
        '要望番号:997 terakawa 2012.10.22 End

        '請求鑑チェック用データセット設定
        rtnResult = rtnResult AndAlso Me._G.SetKagamiDataSet(ds, LMC020C.EventShubetsu.HOZON)

        'ADD 2017/08/29 トール送状先頭取得
        rtnResult = rtnResult AndAlso Me._G.SetTollOkurijyoTblDataSet(ds)

        'タブレット項目を更新
        rtnResult = rtnResult AndAlso Me._G.SetTabletItemData(ds)

        Return rtnResult

    End Function

    'INS START 2023/06/23 035566【LMS】住友ファーマ 端数出荷時出荷オーダー単位の作業コード設定
    ''' <summary>
    ''' 端数出荷の作業(中レコード)の値を設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetHasuOutkaSagyoMDataSet(ByVal frm As LMC020F)

        '荷主マスタの用途区分「端数出荷時 付帯作業自動設定 出荷単位」を取得
        Dim drOutKaL As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)
        Dim custSagyoDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS)
        Dim custSagyoDr As DataRow() = custSagyoDt.Select(
                String.Concat("NRS_BR_CD = '", drOutKaL.Item("NRS_BR_CD").ToString(), "' AND " _
                , "CUST_CD = '", drOutKaL.Item("CUST_CD_L").ToString(), "' AND " _
                , "SUB_KB = 'B3' AND " _
                , "SET_NAIYO = '1'"))

        If custSagyoDr.Length = 0 OrElse custSagyoDr Is Nothing Then

            '端数出荷作業区分が設定されていなければ処理終了
            Exit Sub

        End If
        Dim sagyoCd As String = custSagyoDr(0).Item("SET_NAIYO_2").ToString

        '============================================================================================
        '端数出荷の場合、１出荷１作業とする（端数出荷の２件目以降は削除）
        '============================================================================================
        Dim dtOutkaM As DataTable = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M)
        Dim maxOutkaM As Integer = dtOutkaM.Rows.Count - 1
        Dim dtSagyo As DataTable = Me._Ds.Tables(LMC020C.TABLE_NM_SAGYO)

        Dim drOutKaM As DataRow = Nothing
        Dim hasu As Integer = 0
        Dim cntNotDelOutkaM As Integer = 0
        Dim cntHasuOutkaM As Integer = 0

        For idxOutkaM As Integer = 0 To maxOutkaM

            drOutKaM = dtOutkaM.Rows(idxOutkaM)

            '削除済 を除外
            If drOutKaM.Item("SYS_DEL_FLG").ToString() = LMConst.FLG.ON Then
                Continue For
            End If
            cntNotDelOutkaM = cntNotDelOutkaM + 1

            '端数出荷でない商品 を除外
            If Not (Integer.TryParse(drOutKaM.Item("OUTKA_HASU").ToString(), hasu) AndAlso hasu >= 1) Then
                Continue For
            End If

            Dim hasuSagyoDrs As DataRow() = dtSagyo.Select(
                String.Concat("NRS_BR_CD = '", drOutKaM.Item("NRS_BR_CD").ToString(), "' AND " _
                , "INOUTKA_NO_LM = '", String.Concat(drOutKaM.Item("OUTKA_NO_L").ToString(), drOutKaM.Item("OUTKA_NO_M").ToString()), "' AND " _
                , "SYS_DEL_FLG = '0'"))

            '端数出荷の１件目 を除外
            cntHasuOutkaM = cntHasuOutkaM + 1
            If cntHasuOutkaM <= 1 Then

                '端数出荷の１件目 の作業コードが空の場合は作業コードを設定
                If hasuSagyoDrs.Length <= 0 Then

                    'DBより該当データの取得処理
                    frm.sprSyukkaM.ActiveSheet.ActiveRowIndex = cntNotDelOutkaM - 1
                    Me.ActionControl(LMC020C.EventShubetsu.DOUBLE_CLICK, frm)

                    '作業コード
                    frm.txtSagyoM1.TextValue = sagyoCd

                    '作業コードに紐づく作業名を取得
                    Dim sagyoDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                                   , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                   , "(CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' OR CUST_CD_L = 'ZZZZZ')"))

                    If sagyoDr.Length > 0 Then

                        '作業名をラベルに設定
                        frm.lblSagyoM1.TextValue = sagyoDr(0).Item("SAGYO_RYAK").ToString

                    End If

                    '作業(中レコード)をDataSetに更新
                    Me._G.SetSagyoMDataSet(Me._Ds)

                End If

                Continue For
            End If

            '端数出荷の２件目以降は削除
            For Each hasuSagyoDr As DataRow In hasuSagyoDrs
                If ("0").Equals(hasuSagyoDr.Item("UP_KBN")) = True Then
                    '新規追加の値の場合は、データセットから削除
                    dtSagyo.Rows.Remove(hasuSagyoDr)
                Else
                    '更新の値の場合は、システムフラグを"1"に変更
                    hasuSagyoDr.Item("SYS_DEL_FLG") = LMConst.FLG.ON
                    hasuSagyoDr.Item("UP_KBN") = "2"
                End If
            Next

        Next

    End Sub
    'INS END   2023/06/23 035566【LMS】住友ファーマ 端数出荷時出荷オーダー単位の作業コード設定

    ''' <summary>
    ''' 保存時、分納かつ作業進捗が「予定入力済」のままの場合「検品済」に変更するか否かの確認
    ''' (保存後、出荷完了を行い次回分納データの作成を行いたいか否かの確認)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns>False: 確認「キャンセル」</returns>
    Private Function ChkBunnouOutkaStateKb(ByVal frm As LMC020F, ByVal ds As DataSet) As Boolean

        If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
            ' 分納の場合

            ' 出荷S 件数 0 か否かの判定
            Dim outkaS_Cnt As Integer = 0
            For m As Integer = 0 To ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count() - 1
                Dim drM As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(m)
                If drM.Item("SYS_DEL_FLG").ToString() = LMConst.FLG.ON Then
                    ' 出荷M 削除行は処理対象外
                    Continue For
                End If
                Dim where As String = String.Concat("NRS_BR_CD = '", drM.Item("NRS_BR_CD").ToString(), "'",
                                        "AND OUTKA_NO_L = '", drM.Item("OUTKA_NO_L").ToString(), "'",
                                        "AND OUTKA_NO_M = '", drM.Item("OUTKA_NO_M").ToString(), "'",
                                        "AND SYS_DEL_FLG = '", LMConst.FLG.OFF, "'")
                Dim drS_Arr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(where)
                outkaS_Cnt += drS_Arr.Count()
                If outkaS_Cnt > 0 Then
                    Exit For
                End If
            Next
            If outkaS_Cnt = 0 Then
                ' 出荷S 件数 0 ならば確認不要
                Return True
            End If

            Dim outkaStateKb As String = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_STATE_KB").ToString()
            If Convert.ToDecimal(outkaStateKb) < Convert.ToDecimal(LMC020C.SINTYOKU40) Then
                Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
                Dim replaceMessages() As String = New String() {
                        String.Concat(lgm.Selector({"「", """", """", """"}),
                                      frm.cmbSagyoSintyoku.SelectedText,
                                      lgm.Selector({"」", """", """", """"}))}
                Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C019", replaceMessages)
                If rtnResult = MsgBoxResult.Cancel Then
                    Return False
                End If
                If rtnResult = MsgBoxResult.Yes Then
                    ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_STATE_KB") = LMC020C.SINTYOKU50
                End If
                Return True
            End If

        End If

        Return True

    End Function

    ''' <summary>
    ''' 分納の場合の出荷L 出荷梱包個数 再設定
    ''' </summary>
    ''' <param name="ds"></param>
    Private Sub SetBunnouNbQtOutkaL(ByVal frm As LMC020F, ByVal ds As DataSet)

        If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then

            Dim outkaM_PkgNbTtl As Decimal = 0

            For m As Integer = 0 To ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count() - 1
                Dim drM As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(m)
                If drM.Item("SYS_DEL_FLG").ToString() = LMConst.FLG.ON Then
                    ' 出荷M 削除行は処理対象外
                    Continue For
                End If
                outkaM_PkgNbTtl += Convert.ToDecimal(drM.Item("OUTKA_M_PKG_NB").ToString())
            Next

            If outkaM_PkgNbTtl <> Convert.ToDecimal(ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_PKG_NB").ToString()) Then
                ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_PKG_NB") = outkaM_PkgNbTtl.ToString("0")
            End If

        End If

    End Sub

    ''' <summary>
    ''' 分納の場合の出荷M 個数・数量項目再設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    Private Sub SetBunnouNbQtOutkaM(ByVal frm As LMC020F, ByVal ds As DataSet)

        If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
            ' 分納の場合

            For m As Integer = 0 To ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count() - 1
                Dim drM As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(m)
                If drM.Item("SYS_DEL_FLG").ToString() = LMConst.FLG.ON Then
                    ' 出荷M 削除行は処理対象外
                    Continue For
                End If
                Dim where As String = String.Concat("NRS_BR_CD = '", drM.Item("NRS_BR_CD").ToString(), "'",
                                        "AND OUTKA_NO_L = '", drM.Item("OUTKA_NO_L").ToString(), "'",
                                        "AND OUTKA_NO_M = '", drM.Item("OUTKA_NO_M").ToString(), "'",
                                        "AND SYS_DEL_FLG = '", LMConst.FLG.OFF, "'")
                Dim drS_Arr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(where)
                Dim alctdNbTtl As Decimal = 0
                Dim outkaTtlNb As Decimal
                For s As Integer = 0 To drS_Arr.Count() - 1
                    Dim drS As DataRow = drS_Arr(s)
                    alctdNbTtl += Convert.ToDecimal(drS.Item("ALCTD_NB").ToString())
                Next
                If Convert.ToDecimal(drM.Item("OUTKA_TTL_NB").ToString()) = alctdNbTtl Then
                    ' 出荷M.出荷総個数 = (出荷S.引当済個数) の計 の場合

                    ' 出荷S 0件で保存の場合に本メソッドでゼロクリアすると、
                    ' 次回更新時に本メソッド到達まで再設定されない出荷数量のみ再計算する。
                    outkaTtlNb = alctdNbTtl
                    drM.Item("OUTKA_QT") = outkaTtlNb * Convert.ToDecimal(drM.Item("IRIME").ToString())
                    Continue For
                End If

                ' 出荷M.出荷総個数 = (出荷S.引当済個数) の計 とし、出荷M.出荷総個数 をもとに、個数・数量項目を再設定する。
                outkaTtlNb = alctdNbTtl
                If String.IsNullOrEmpty(drM.Item("OUTKA_PKG_NB").ToString().ToString()) = False OrElse
                Convert.ToDecimal(drM.Item("OUTKA_PKG_NB").ToString()) = 0D Then
                    drM.Item("OUTKA_HASU") = 0D
                Else
                    drM.Item("OUTKA_HASU") = outkaTtlNb Mod Convert.ToDecimal(drM.Item("OUTKA_PKG_NB").ToString())
                End If
                drM.Item("OUTKA_QT") = outkaTtlNb * Convert.ToDecimal(drM.Item("IRIME").ToString())
                drM.Item("OUTKA_TTL_NB") = outkaTtlNb
                drM.Item("OUTKA_TTL_QT") = outkaTtlNb * Convert.ToDecimal(drM.Item("IRIME").ToString())
                drM.Item("ALCTD_NB") = outkaTtlNb
                drM.Item("ALCTD_QT") = outkaTtlNb * Convert.ToDecimal(drM.Item("IRIME").ToString())
                drM.Item("BACKLOG_NB") = 0D.ToString("0")
                drM.Item("BACKLOG_QT") = 0D.ToString("0.000")

                Dim outkaPkgNb As Decimal = Convert.ToDecimal(drM.Item("OUTKA_PKG_NB").ToString())
                If outkaPkgNb = 0D Then
                    drM.Item("OUTKA_M_PKG_NB") = 0D.ToString("0")
                Else
                    Dim calcPkgModNb As Long = 0
                    Dim calcPkgQuoNb As Double = 0

                    If String.IsNullOrEmpty(drM.Item("OUTKA_PKG_NB").ToString().ToString()) = False _
                        AndAlso (drM.Item("OUTKA_PKG_NB").ToString().ToString()).Equals("0") = False Then
                        calcPkgModNb = Convert.ToInt64(outkaTtlNb) Mod Convert.ToInt64(drM.Item("OUTKA_PKG_NB").ToString())
                        calcPkgQuoNb = outkaTtlNb / Convert.ToInt64(drM.Item("OUTKA_PKG_NB").ToString())
                        calcPkgQuoNb = Math.Floor(calcPkgQuoNb)
                    End If
                    If 0 = calcPkgModNb Then
                        drM.Item("OUTKA_M_PKG_NB") = calcPkgQuoNb
                    Else
                        drM.Item("OUTKA_M_PKG_NB") = calcPkgQuoNb + 1
                    End If
                    If Convert.ToInt64(drM.Item("OUTKA_PKG_NB").ToString()) > 999 Then
                        drM.Item("OUTKA_M_PKG_NB") = 1D.ToString("0")
                    End If
                End If
            Next

        End If

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SaveControl(ByVal frm As LMC020F) As DataSet

        
        '更新処理
        Dim saveMode As String = String.Empty
        If frm.lblSituation.RecordStatus = RecordStatus.NEW_REC OrElse _
            frm.lblSituation.RecordStatus = RecordStatus.COPY_REC Then
            saveMode = "InsertSaveAction"
        Else
            saveMode = "UpdateSaveAction"
        End If

        Dim rtnDs As DataSet = Me.SaveData(frm, Me._Ds, saveMode)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 終算日修正・運送修正モードなのかどうか判別
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GamenModeChk(ByVal frm As LMC020F) As Boolean

        Me._LMCconH.EndAction(frm)

        Dim result As Boolean = True

        With frm.FunctionKey

            result = result AndAlso .F1ButtonEnabled.Equals(False)
            result = result AndAlso .F2ButtonEnabled.Equals(True)
            result = result AndAlso .F3ButtonEnabled.Equals(False)
            result = result AndAlso .F4ButtonEnabled.Equals(False)
            result = result AndAlso .F5ButtonEnabled.Equals(False)
            'result = result AndAlso .F6ButtonEnabled.Equals(False)
            result = result AndAlso .F7ButtonEnabled.Equals(False)
            'result = result AndAlso .F8ButtonEnabled.Equals(False)
            result = result AndAlso .F9ButtonEnabled.Equals(False)
            result = result AndAlso .F10ButtonEnabled.Equals(True)
            result = result AndAlso .F11ButtonEnabled.Equals(True)

        End With

        Me._LMCconH.StartAction(frm)

        Return result

    End Function

    Private Function AllFKeyOffChk(ByVal frm As LMC020F) As Boolean

        Dim result As Boolean = True

        With frm.FunctionKey
            result = result AndAlso .F1ButtonEnabled.Equals(False)
            result = result AndAlso .F2ButtonEnabled.Equals(False)
            result = result AndAlso .F3ButtonEnabled.Equals(False)
            result = result AndAlso .F4ButtonEnabled.Equals(False)
            result = result AndAlso .F5ButtonEnabled.Equals(False)
            result = result AndAlso .F6ButtonEnabled.Equals(False)
            result = result AndAlso .F7ButtonEnabled.Equals(False)
            result = result AndAlso .F8ButtonEnabled.Equals(False)
            result = result AndAlso .F9ButtonEnabled.Equals(False)
            result = result AndAlso .F10ButtonEnabled.Equals(False)
            result = result AndAlso .F11ButtonEnabled.Equals(False)
            result = result AndAlso .F12ButtonEnabled.Equals(False)
        End With

        Return result
    End Function
    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function DeleteControl(ByVal frm As LMC020F, ByVal eventShubetsu As LMC020C.EventShubetsu) As DataSet

        '更新処理
        Dim saveMode As String = String.Empty

        If (LMC020C.EventShubetsu.DEL).Equals(eventShubetsu) = True Then
            saveMode = "DeleteAction"
        Else
            saveMode = "DeleteSaveAction"
        End If

        '印刷処理
        Me._Ds.Merge(New RdPrevInfoDS)
        Me._Ds.Tables(LMConst.RD).Clear()

        '2012.11.20 nakamura 要望番号612:在庫振替削除 Start
        If String.IsNullOrEmpty(frm.lblFurikaeNo.ToString()) = True Then

            '要望番号:1339 yamanaka 2012.08.27 Start
            'CSV出力処理
            Me.DicCsvMake(frm, Me._Ds, "16")
            '要望番号:1339 yamanaka 2012.08.27 End
        End If
        '2012.11.20 nakamura 要望番号612:在庫振替削除 End

        Dim rtnDs As DataSet = Me.SaveData(frm, Me._Ds, saveMode)

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

            'フォーカス設定
            prevFrm.Focus()

        End If

        Return rtnDs

    End Function


    ''' <summary>
    ''' マーク情報(行削除)処理
    ''' </summary>
    ''' <param name="dt">データテーブル</param>
    ''' <param name="sql">SQL</param>
    ''' <remarks></remarks>
    Private Sub DeleteTabelData(ByVal dt As DataTable, ByVal sql As String)

        Dim drs As DataRow() = dt.Select(Sql)

        Dim max As Integer = drs.Length - 1

        '削除のみのループ
        For i As Integer = max To 0 Step -1

            '行削除処理
            Call Me.DeleteRowData(drs(i))

        Next

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowData(ByVal dr As DataRow)

        If LMConst.FLG.ON.Equals(dr.Item("UP_KBN").ToString()) = True Then

            '削除フラグをON
            dr.Item("SYS_DEL_FLG") = LMConst.FLG.ON

        Else

            '行自体を削除
            dr.Delete()

        End If

    End Sub

    ''' <summary>
    ''' データセットクリア処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearDataSet()

        '出荷大をクリア
        Me._G.ClearOutL(Me._Ds)

        '出荷中をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Clear()

        '出荷小をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Clear()

        '在庫をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_ZAI).Clear()

        '運送大をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_UNSO_L).Clear()

        '運送中をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_UNSO_M).Clear()

        '作業をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_SAGYO).Clear()

        '出荷管理番号中・小の採番をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_MAX).Clear()

        '2015.07.21 協立化学　シッピング対応 追加START
        'マーク(HED)をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_MARK_HED).Clear()

        'マーク(DTL)をクリア
        Me._Ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Clear()
        '2015.07.21 協立化学　シッピング対応 追加END

        'FFEM入出荷EDIデータ(ヘッダ) のクリア
        Me._Ds.Tables(LMC020C.TABLE_INOUTKAEDI_HED_FJF).Clear()

    End Sub

    ''' <summary>
    ''' 請求鑑検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectKagamiData(ByVal frm As LMC020F, ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal msg As String) As DataSet

        'DataSet設定
        Dim inTbl As DataTable = Me._Ds.Tables(LMC020C.TABLE_NM_KAGAMI_IN)
        inTbl.Clear()
        Dim row As DataRow = inTbl.NewRow()
        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue
        row("MSG") = msg

        Me._Ds.Tables(LMC020C.TABLE_NM_KAGAMI_IN).Rows.Add(row)
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "IsSeiqDateChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Me._Ds = Me.ServerAccess(Me._Ds, "IsSeiqDateChk")

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "IsSeiqDateChk")

        Return Me._Ds

    End Function

    ''' <summary>
    ''' 請求鑑チェック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function CheckKagamiData(ByVal frm As LMC020F, ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal msg As String) As Boolean

        With frm

            '請求鑑検索処理
            Dim rtDs As DataSet = Me.SelectKagamiData(frm, eventShubetsu, msg)
            Dim rtnResult As Boolean = True
            If MyBase.IsMessageExist() = True Then
                rtnResult = False
                MyBase.ShowMessage(frm)
            End If

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 保管・荷役料最終請求日チェック
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function IsHokanryoChk(ByVal frm As LMC020F, ByVal str As String) As Boolean

        'DataSet設定
        Dim chkDs As DataSet = Me.SetDataSetHokanNiyakuData(frm, Me._Ds, str)

        '==========================
        'WSAクラス呼出
        '==========================
        chkDs = MyBase.CallWSA("LMD000BLF", "SelectChkIdoDate", chkDs)

        If MyBase.IsMessageExist() = True Then
            Call Me.ShowMessage(frm)
            If frm.imdSyukkaDate.ReadOnly = False Then
                Me._LMCconV.SetErrorControl(frm.imdSyukkaDate)
            End If
            Return False
        Else
            'START YANAI No.42
            'frm.imdSyukkaYoteiDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
            'END YANAI No.42
        End If

        '出荷（大）の最終請求日が空ではない場合
        Dim endDate As String = frm.imdHokanEndDate.TextValue
        Dim rowCount As Integer = chkDs.Tables(LMControlC.LMD000_TABLE_NM_OUT).Rows.Count - 1
        Dim dt As DataTable = chkDs.Tables(LMControlC.LMD000_TABLE_NM_OUT)

        If String.IsNullOrEmpty(endDate) = False AndAlso endDate.Equals(frm.imdSyukkaDate.TextValue) = False Then

            For i As Integer = 0 To rowCount
                If endDate <= dt.Rows(i).Item("HOKAN_NIYAKU_CALCULATION").ToString() Then
                    MyBase.SetMessage("E375", New String() {dt.Rows(i).Item("REPLACE_STR1").ToString(), dt.Rows(i).Item("REPLACE_STR2").ToString()})
                    Call Me.ShowMessage(frm)
                    If frm.imdHokanEndDate.ReadOnly = False Then
                        Me._LMCconV.SetErrorControl(frm.imdHokanEndDate)
                    End If
                    Return False
                Else
                    'START YANAI No.42
                    'frm.imdHokanEndDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    'END YANAI No.42
                End If
            Next

        End If

        Return True

    End Function

    ''' <summary>
    ''' データセット設定(保管・荷役料最終計算日取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Function SetDataSetHokanNiyakuData(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal str As String) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'データセット作成
        Dim rtDs As DataSet = New LMD000DS()
        Dim inTbl As DataTable = rtDs.Tables(LMControlC.LMD000_TABLE_NM_IN)
        Dim rowCount As Integer = 0

        With frm.sprSyukkaM.ActiveSheet

            rowCount = ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count - 1
            Dim dr As DataRow = Nothing

            Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                               "NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND " _
                                             , "CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' AND " _
                                             , "CUST_CD_M = '", frm.txtCust_Cd_M.TextValue, "' AND " _
                                             , "CUST_CD_S = '00' AND " _
                                             , "CUST_CD_SS = '00'"))

            '運賃計算締め基準の値によって、チェック対象の日付を変更
            Dim checkDate As String = String.Empty
            If 0 <> custDr.Length Then
                If ("01").Equals(custDr(0).Item("UNTIN_CALCULATION_KB")) = True Then
                    checkDate = frm.imdSyukkaYoteiDate.TextValue
                Else
                    checkDate = frm.imdNounyuYoteiDate.TextValue
                End If
            End If

            For i As Integer = 0 To rowCount

                dr = inTbl.NewRow()

                '商品KEYの取得
                dr(LMControlC.LMD000_COL_GOODS_CD_NRS) = ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(i).Item("GOODS_CD_NRS").ToString()
                dr(LMControlC.LMD000_COL_NRS_BR_CD) = frm.cmbEigyosyo.SelectedValue
                dr(LMControlC.LMD000_COL_CHK_DATE) = checkDate
               
                '2017/09/25 修正 李↓
                dr(LMControlC.LMD000_COL_REPLACE_STR1) = lgm.Selector({"保管料・荷役料が既に計算されている", "Storage fees and handling fee has already been calculated", "보관료/하역료가 이미 계산되어있음", "中国語"})
                '2017/09/25 修正 李↑

                dr(LMControlC.LMD000_COL_REPLACE_STR2) = str

                inTbl.Rows.Add(dr)

            Next

        End With

        Return rtDs

    End Function

    '要望番号:1511 KIM 2012/10/12 START
#Region "GoodsLotCheck"

    ''要望番号:1350 terakawa 2012.08.27 Start
    '''' <summary>
    '''' 同一置き場（同一商品・ロット）チェック
    '''' </summary>
    '''' <param name="ds">データセット</param>
    '''' <returns>OK または Close処理の場合:True　Cancelの場合:False</returns>
    '''' <remarks></remarks>
    'Private Function GoodsLotCheck(ByVal frm As LMC020F, ByVal ds As DataSet) As Boolean

    '    Dim whCd As String = frm.cmbSoko.SelectedValue.ToString()
    '    Dim sokoDrs As DataRow() = Me._LMCconV.SelectSokoListDataRow(whCd)
    '    Dim goodsLotCheckYn As String = String.Empty
    '    If 0 < sokoDrs.Length Then
    '        goodsLotCheckYn = sokoDrs(0).Item("GOODSLOT_CHECK_YN").ToString()
    '    End If


    '    '同一置き場に同一商品・ロットがある場合ワーニング
    '    If goodsLotCheckYn = "01" Then
    '        ds = Me.ServerAccess(ds, "ChkGoodsLot")

    '        If ds.Tables(LMC020C.TABLE_NM_WORNING).Rows.Count > 0 Then
    '            Return Me._V.IsWorningChk(ds)
    '        End If
    '    End If

    '    Return True

    'End Function
    '要望番号:1350 terakawa 2012.08.27 End

#End Region '仕様変更により、削除
    '要望番号:1511 KIM 2012/10/12 END

#Region "IrimeCheck"

    ''' <summary>
    ''' 入目違いチェック
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>OK または Close処理の場合:True　Cancelの場合:False</returns>
    ''' <remarks>2019/12/16 要望管理009513 add</remarks>
    Private Function IrimeCheck(ByVal frm As LMC020F, ByVal ds As DataSet) As Boolean

        ds = Me.ServerAccess(ds, "IrimeCheck")

        If ds.Tables("LMC020_WORNING_IRIME").Rows.Count > 0 Then
            Return False
        End If

        Return True

    End Function

#End Region

    '要望番号:1520 KIM 2012/10/18 START
#Region "ChkUntinCalDate"

    ''' <summary>
    ''' 運賃計算締基準チェック
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function ChkUntinCalDate(ByVal frm As LMC020F) As Boolean

        Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                               "NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND " _
                                             , "CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' AND " _
                                             , "CUST_CD_M = '", frm.txtCust_Cd_M.TextValue, "' AND " _
                                             , "CUST_CD_S = '00' AND " _
                                             , "CUST_CD_SS = '00'"))


        If 0 <> custDr.Length Then
            If ("02").Equals(custDr(0).Item("UNTIN_CALCULATION_KB")) = True AndAlso _
               frm.cmbTodokesaki.SelectedValue.Equals("01") = True AndAlso _
               String.IsNullOrEmpty(frm.imdNounyuYoteiDate.TextValue) = True Then
                Call MyBase.ShowMessage(frm, "E524")
                Me._LMCconV.SetErrorControl(frm.imdNounyuYoteiDate)
                Return False
            End If
        End If

        Return True

    End Function

#End Region
    '要望番号:1520 KIM 2012/10/18 END

    ''' <summary>
    ''' 荷主マスタ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function SelectCustData(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMC020DS()

        'ログ出力
        'MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCustData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs = Me.ServerAccess(ds, "SelectCustData")

        'ログ出力
        'MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCustData")

        Return rtDs

    End Function

    'START YANAI 要望番号853 まとめ処理対応
    ''' <summary>
    ''' 在庫データ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function SelectZaiDataMATOME(ByVal frm As LMC020F, ByVal ds As DataSet) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMC020DS()

        'ログ出力
        'MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectZaiDataMATOME")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs = Me.ServerAccess(ds, "SelectZaiDataMATOME")

        'ログ出力
        'MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectZaiDataMATOME")

        Return rtDs

    End Function
    'END YANAI 要望番号853 まとめ処理対応

    ''' <summary>
    ''' 運送課税区分の初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetUnchinUmuInitCd(ByVal frm As LMC020F)

        With frm

            If .cmbUnsoKazeiKbn.ReadOnly = True AndAlso ("10").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                Exit Sub
            End If

            Dim tehaiKbn As String = .cmbTehaiKbn.SelectedValue.ToString()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tehaiKbn) = True OrElse _
                ("10").Equals(tehaiKbn) = False Then
                Exit Sub
            End If

            'START YANAI 要望番号602
            'Dim drs As DataRow() = Me._LMCconV.SelectCustListDataRow( _
            '                                                         .txtCust_Cd_L.TextValue _
            '                                                         , .txtCust_Cd_M.TextValue _
            '                                                        )

            ''取得できた場合、初期値設定
            'If 0 < drs.Length Then
            '    '運送課税区分の初期値設定
            '    .cmbUnsoKazeiKbn.SelectedValue = drs(0).Item("TAX_KB").ToString
            'End If
            '運送課税区分の初期値設定
            .cmbUnsoKazeiKbn.SelectedValue = LMC020C.TAX_KB01
            'END YANAI 要望番号602

        End With

    End Sub

    ''' <summary>
    ''' タリフコードの初期値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetTariffInitCd(ByVal frm As LMC020F)

        With frm

            Dim tariffKbn As String = .cmbTariffKbun.SelectedValue.ToString()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffKbn) = True Then
                Exit Sub
            End If

            'START YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する
            'Dim drs As DataRow() = Me._LMCconV.SelectTariffSetListDataRow( _
            '                                                              .cmbEigyosyo.SelectedValue.ToString() _
            '                                                            , .txtCust_Cd_L.TextValue _
            '                                                            , .txtCust_Cd_M.TextValue _
            '                                                            , String.Empty _
            '                                                            , tariffKbn _
            '                                                            )
            Dim setKb As String = String.Empty
            If String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = True Then
                setKb = "00"
            Else
                setKb = "01"
            End If
            Dim drs As DataRow() = Me._LMCconV.SelectTariffSetListDataRow( _
                                                                          .cmbEigyosyo.SelectedValue.ToString() _
                                                                        , .txtCust_Cd_L.TextValue _
                                                                        , .txtCust_Cd_M.TextValue _
                                                                        , String.Empty _
                                                                        , tariffKbn _
                                                                        , String.Empty _
                                                                        , String.Empty _
                                                                        , String.Empty _
                                                                        , .txtTodokesakiCd.TextValue _
                                                                        , setKb _
                                                                        )
            'END YANAI 要望番号1386 タリフ分類区分を変更した際に誤ったタリフを設定する

            Dim tariffCd As String = String.Empty
            Dim tariffNm As String = String.Empty

            '取得できた場合、初期値設定
            If 0 < drs.Length Then

                Select Case tariffKbn

                    Case "20"

                        tariffCd = drs(0).Item("UNCHIN_TARIFF_CD2").ToString()

                    Case "40"

                        tariffCd = drs(0).Item("YOKO_TARIFF_CD").ToString()

                    Case Else

                        tariffCd = drs(0).Item("UNCHIN_TARIFF_CD1").ToString()

                End Select

                If "40".Equals(tariffKbn) = True Then
                    Dim drsT As DataRow() = Me._LMCconV.SelectYokoTariffListDataRow( _
                                                                                    .cmbEigyosyo.SelectedValue.ToString() _
                                                                                  , tariffCd)
                    If 0 < drsT.Length Then
                        tariffNm = drsT(0).Item("YOKO_REM").ToString()
                    End If
                Else
                    Dim drsT As DataRow() = Me._LMCconV.SelectUnchinTariffListDataRow(tariffCd)
                    If 0 < drsT.Length Then
                        tariffNm = drsT(0).Item("UNCHIN_TARIFF_REM").ToString()
                    End If

                End If

            End If

            'タリフコードの初期値設定
            If String.IsNullOrEmpty(tariffCd) = False Then
                .txtUnthinTariffCd.TextValue = tariffCd
                .lblUnthinTariffNm.TextValue = tariffNm
            End If

        End With

    End Sub
    '要望番号：612 Start nakamura
    ''' <summary>
    ''' 入荷マスタ検索処理 振替データ一括削除対応
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function SelectInkaData(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal eventShubetsu As LMC020C.EventShubetsu) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMC020DS()

        'ログ出力
        'MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCustData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs = Me.ServerAccess(ds, "SelectInkaData")

        'ログ出力
        'MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCustData")

        Return rtDs

    End Function

    '2012.12.14 追加START
    ''' <summary>
    ''' 印刷処理(船積確認書)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub getSqlDataAction(ByVal frm As LMC020F)

        Dim ds As DataSet = Nothing

        ds = Me.setMsqlDataset(frm)

        ds = Me.ServerAccessLmq010(ds, "SelectListData")

        If ds.Tables("LMQ010INOUT").Rows.Count = 0 Then
            'エラー
        Else
            Dim fileNm As String = ds.Tables("LMQ010INOUT").Rows(0).Item("FILE_NM").ToString()
            ds = Me.setCreateSqlDataset(ds, frm)

            'LMQ010に遷移する
            ds = Me.ServerAccessLmq010(ds, "ExcelMake")
            '成功時共通処理を行う
            If ds IsNot Nothing Then
                'ExcelCreator呼び出し処理
                If Me.ExcelPrint(frm, ds, fileNm) = False Then
                    If MyBase.IsErrorMessageExist = True Then
                        '更新失敗時、返却メッセージを設定
                        MyBase.ShowMessage(frm)
                    End If
                    '処理終了アクション
                    Me._LMCconH.EndAction(frm)

                End If
                'Excel出力

            End If

        End If



    End Sub

    ''' <summary>
    ''' LMQ617データセット設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function setMsqlDataset(ByVal frm As LMC020F) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim setDs As DataSet = New LMQ010DS
        Dim setDt As DataTable = setDs.Tables("LMQ010IN")
        Dim setDr As DataRow = Nothing

        setDr = setDt.NewRow()

        setDr.Item("PATTERN_ID") = "LMQ617"
        setDr.Item("EX_TYPE_KB") = "AK"

        '2017/09/25 修正 李↓
        setDr.Item("EX_CONTENTS") = lgm.Selector({"船積確認書", "Shipping confirmation", "선적확인서", "中国語"})
        '2017/09/25 修正 李↑

        setDr.Item("EX_SQL") = String.Empty
        setDr.Item("FILE_NM") = String.Empty
        setDr.Item("FILE_TITLE_NM") = String.Empty
        setDr.Item("SYS_DEL_FLG") = "0"
        setDr.Item("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()

        setDt.Rows.Add(setDr)

        Return setDs

    End Function

    ''' <summary>
    ''' LMQ617データセット設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function setCreateSqlDataset(ByVal setDs As DataSet, ByVal frm As LMC020F) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim setDt As DataTable = setDs.Tables("LMQ010_EXCEL")
        Dim setDr As DataRow = Nothing
        Dim setSql As String = String.Empty

        setDr = setDt.NewRow()

        setSql = setDs.Tables("LMQ010INOUT").Rows(0).Item("EX_SQL").ToString()

        For i As Integer = 0 To 2
            If i = 0 Then

                '2017/09/25 修正 李↓
                setSql = setSql.Replace(String.Concat("@", lgm.Selector({"営業所コード", "Office Code", "영업소코드", "中国語"}), "@"), frm.cmbEigyosyo.SelectedValue.ToString())
                '2017/09/25 修正 李↑

            ElseIf i = 1 Then

                '2017/09/25 修正 李↓
                setSql = setSql.Replace(String.Concat("@", lgm.Selector({"荷主大コード", "Custmer Code (L)", "하주대코드", "中国語"}), "@"), frm.txtCust_Cd_L.TextValue.ToString())
                '2017/09/25 修正 李↑

            ElseIf i = 2 Then
          
                '2017/09/25 修正 李↓
                setSql = setSql.Replace(String.Concat("@", lgm.Selector({"出荷管理番号", "Shipment control number", "출하관리번호", "中国語"}), "@"), frm.lblSyukkaLNo.TextValue.ToString())
                '2017/09/25 修正 李↑

            End If

        Next

        setDr.Item("SQL") = setSql

        setDr.Item("NRS_BR_CD") = setDs.Tables("LMQ010INOUT").Rows(0).Item("NRS_BR_CD").ToString()

        setDt.Rows.Add(setDr)

        Return setDs

    End Function

    ''' <summary>
    ''' ExcelCreator呼び出し処理
    ''' </summary>
    ''' <param name="ds">出力データ</param>
    ''' <remarks></remarks>
    Private Function ExcelPrint(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal fileNm As String) As Boolean

        Dim LMQconH As New LMQControlH(DirectCast(frm, Form), GetPGID())

        Dim resultFlg As Boolean = True

        'INPUTパラメータチェック
        If LMQconH.CheckParameter(ds, fileNm, LMQControlC.ChkObject.ALL_OBJECT) = False Then
            Exit Function
        End If

        'Excel出力
        resultFlg = LMQconH.ExcelPrint(ds, fileNm, fileNm)

        'メッセージ
        '英語化対応
        '20151022 tsunehira add
        MyBase.ShowMessage(frm, "G002", New String() {frm.btnPRINT.TextValue, ""})
        'MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})

        Return resultFlg

    End Function

    '2012.12.14 追加END

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintDataAction(ByVal frm As LMC020F)

        Dim dr As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)
        'Dim unsodr As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows(0)
        Dim cMsg As String = String.Empty

        'START YANAI 20120122 立会書印刷対応
        'If "07".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
        If "07".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True OrElse
           "09".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
            'END YANAI 20120122 立会書印刷対応

            'START YANAI 要望番号394
            'If "01".Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString()) = False Then
            '    Exit Sub
            'End If
            'END YANAI 要望番号394
            'START YANAI 20120122 立会書印刷対応
            ''確認メッセージ：棟別に出力するか否か。
            'Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C004")
            Dim msgId As String = String.Empty
            If "07".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
                msgId = "C004"
            ElseIf "09".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
                msgId = "C009"
            End If

            '確認メッセージ：棟別に出力するか否か。
            Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, msgId)
            'END YANAI 20120122 立会書印刷対応

            If rtnResult.Equals(MsgBoxResult.Yes) = True Then
                dr.Item("TOU_KANRI_YN") = "01"
            ElseIf rtnResult.Equals(MsgBoxResult.No) = True Then
                dr.Item("TOU_KANRI_YN") = "00"
            Else
                Exit Sub
            End If

        End If

        Me._BunsekiArr = New ArrayList()
        If "05".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
            If Me._ShikakariHinFlg = "1" Then
                'FFEM 仕掛品の場合は分析票の印刷を行わない
            Else
                '分析票マスタ(キャッシュの再取得)
                MyBase.LMCacheMasterData(LMConst.CacheTBL.COA)

                '分析表のマスタ存在チェック
                Me._BunsekiArr = Me._V.IsPrintCOACheck(frm, Me._Ds, Me._BunsekiArr)
                If Me._BunsekiArr.Count = 0 Then
                    Exit Sub
                ElseIf ("ALLNOCHECK").Equals(Me._BunsekiArr(0)) = True Then
                    Exit Sub
                Else
                    '処理続行
                End If
            End If
        End If

        If "08".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
            If Me._ShikakariHinFlg = "1" Then
                'FFEM 仕掛品の場合は分析票の印刷を行わない
            Else
                '分析票マスタ(キャッシュの再取得)
                MyBase.LMCacheMasterData(LMConst.CacheTBL.COA)

                Me._BunsekiArr = Me._V.IsPrintCOACheck(frm, Me._Ds, Me._BunsekiArr)
            End If
            'START YANAI 要望番号741
            ''分析表のマスタ存在チェック
            'If Me._BunsekiArr.Count = 0 Then
            '    'チェック行有りで全行エラーの場合は処理終了
            '    Exit Sub
            'ElseIf ("ALLNOCHECK").Equals(Me._BunsekiArr(0)) = True Then
            '    '全行チェックなしの場合は処理続行
            '    dr.Item("COA_FLAG") = "00"
            'Else
            '    dr.Item("COA_FLAG") = "01"
            'End If
            'END YANAI 要望番号741

            Dim nrsBrCd As String = Me._Ds.Tables("LMC020_OUTKA_L").Rows(0)("NRS_BR_CD").ToString()
            Dim custCdL As String = Me._Ds.Tables("LMC020_OUTKA_L").Rows(0)("CUST_CD_L").ToString()
            Dim nihudaYn As String = Me._Ds.Tables("LMC020_UNSO_L").Rows(0)("NIHUDA_YN").ToString()
            Dim outkaPkgNb As String = Me._Ds.Tables("LMC020_OUTKA_L").Rows(0)("OUTKA_PKG_NB").ToString()

            '荷札
            If nrsBrCd.Equals("40") = True AndAlso custCdL.Equals("00237") = True Then
                '運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                    dr.Item("NIHUDA_FLAG") = "00"
                ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                    dr.Item("NIHUDA_FLAG") = "01"
                ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                    dr.Item("NIHUDA_FLAG") = "00"
                ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                    dr.Item("NIHUDA_FLAG") = "00"
                Else
                    '基本的に上記のどれかに引っかかるので、elseにくることはない。
                    dr.Item("NIHUDA_FLAG") = "00"
                End If
            Else
                '運送会社マスタの荷札有無フラグが"01"(有)の場合は、荷札印刷
                If nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = True Then
                    dr.Item("NIHUDA_FLAG") = "00"
                ElseIf nihudaYn.Equals("01") = True AndAlso outkaPkgNb.Equals("0") = False Then
                    dr.Item("NIHUDA_FLAG") = "01"
                ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = True Then
                    dr.Item("NIHUDA_FLAG") = "00"
                ElseIf nihudaYn.Equals("00") = True AndAlso outkaPkgNb.Equals("0") = False Then
                    dr.Item("NIHUDA_FLAG") = "00"
                Else
                    '基本的に上記のどれかに引っかかるので、elseにくることはない。
                    dr.Item("NIHUDA_FLAG") = "00"
                End If
            End If

        End If

        If "08".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
            '運送会社荷主別送り状情報マスタ存在チェック
            If Me._V.IsUnsoCustRptExistChk(frm.txtUnsoCompanyCd.TextValue, frm.txtUnsoSitenCd.TextValue, Convert.ToString(frm.cmbMotoCyakuKbn.SelectedValue)) = False Then
                dr.Item("DENP_FLAG") = "00"
            Else
                dr.Item("DENP_FLAG") = "01"
            End If
        End If

        dr.Item("PRINT_KB") = frm.cmbPRINT.SelectedValue.ToString()
        'START YANAI 20120122 立会書印刷対応
        If ("09").Equals(frm.cmbPRINT.SelectedValue.ToString) = True Then
            '立会書の場合
            dr.Item("PRINT_KB") = "11"
        End If
        'END YANAI 20120122 立会書印刷対応

        '船積確認書対応 yamanaka 2012.12.03 Start
        If ("10").Equals(frm.cmbPRINT.SelectedValue.ToString) = True Then
            dr.Item("PRINT_KB") = "12"
        End If
        '船積確認書対応 yamanaka 2012.12.03 End

#If True Then       'ADD 2018/07/18 依頼番号 : 001540   【LMS】運送保険申込書
        If ("21").Equals(frm.cmbPRINT.SelectedValue.ToString) = True Then
            dr.Item("PRINT_KB") = "21"
        End If

#End If

        'ADD 2017/07/20 名鉄対応 start
        Dim kbnDr() As DataRow = Nothing
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ",
                                                                           "KBN_NM1 = '", frm.cmbEigyosyo.SelectedValue.ToString(), "' AND ",
                                                                           "KBN_NM2 = '", frm.txtUnsoCompanyCd.TextValue, "' AND ",
                                                                            "KBN_NM3 = '", frm.txtUnsoSitenCd.TextValue, "'"))
        If kbnDr.Length = 0 Then
            dr.Item("MEITETSU_FLG") = "0"
        Else
            dr.Item("MEITETSU_FLG") = "1"
        End If
        'ADD 2017/07/20 名鉄対応 End


        dr.Item("OUTKA_STATE_KB") = Me.GetStageData(frm)
        '荷札の場合FromToを設定
        If ("01").Equals(frm.cmbPRINT.SelectedValue.ToString) = True Then
            dr.Item("PRT_NB") = Convert.ToInt32(frm.numPrtCnt_To.Value) - Convert.ToInt32(frm.numPrtCnt_From.Value) + 1
            dr.Item("PRT_NB_From") = frm.numPrtCnt_From.Value
            dr.Item("PRT_NB_To") = frm.numPrtCnt_To.Value
        Else
            dr.Item("PRT_NB") = frm.numPrtCnt.Value
            dr.Item("PRT_NB_From") = "1"
            dr.Item("PRT_NB_To") = "1"
        End If
        dr.Item("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
        dr.Item("CUST_CD_L") = frm.txtCust_Cd_L.TextValue()
        dr.Item("SASZ_USER") = LMUserInfoManager.GetUserID()
        'START YANAI 20120122 立会書印刷対応
        dr.Item("TACHIAI_FLG") = frm.lblTachiai.TextValue
        'END YANAI 20120122 立会書印刷対応
        Dim actionId As String = "UpdatePrintData"

        '自営業でない場合、印刷のみ
        If LMUserInfoManager.GetNrsBrCd().Equals(frm.cmbEigyosyo.SelectedValue.ToString()) = False Then
            actionId = "DoPrint"
        End If

        '出荷データ(中)をDataSetに更新
        Me._G.SetOutMDataSetPrint(frm.sprSyukkaM, Me._Ds)

        '印刷処理
        Me._Ds.Merge(New RdPrevInfoDS)
        Me._Ds.Tables(LMConst.RD).Clear()
        Me._Ds = Me.ServerAccess(Me._Ds, actionId)
        'START YANAI 要望番号741
        ''分析票と一括印刷以外スルー
        'If "05".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True OrElse _
        '   ("08".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True AndAlso _
        '   "01".Equals(dr.Item("COA_FLAG").ToString()) = True) Then
        '分析票と以外スルー
        If "05".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
            If Me._ShikakariHinFlg = "1" Then
                'FFEM 仕掛品の場合は分析票の印刷を行わない
            Else
                'END YANAI 要望番号741

                '分析票の共通部品呼び出し
                Dim strPass() As String
                strPass = DirectCast(Me._BunsekiArr.ToArray(GetType(String)), String())

                'START YANAI 要望番号735
                'If MyBase.PDFDirectPrint(strPass) = False Then

                '    MyBase.ShowMessage(frm, "E235")
                'End If
                strPass = DirectCast(Me._BunsekiArr.ToArray(GetType(String)), String())
                ' リモートの 分析票 PDF ファイルのローカルへのコピー 
                strPass = CopyRemotePdf(frm, strPass, Integer.Parse(frm.cmbPRINT.SelectedValue.ToString()))
                If MyBase.PDFDirectPrint(strPass) = False Then
                    cMsg = LMConst.FLG.ON
                Else
                    ' 分析票 ログ出力
                    Me.WritePrintLog(frm, strPass, Integer.Parse(frm.cmbPRINT.SelectedValue.ToString()))
                End If
                'END YANAI 要望番号735

                '分析表の場合、ここで処理完了
                If "05".Equals(frm.cmbPRINT.SelectedValue.ToString()) = True Then
                    If LMConst.FLG.ON.Equals(cMsg) Then
                        MyBase.ShowMessage(frm, "E800", New String() {frm.lblSyukkaLNo.TextValue})
                    Else
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage(frm, "G002", New String() {frm.btnPRINT.TextValue, ""})
                        'MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})
                    End If
                    '出荷大の値設定(この時点で印刷フラグ等が更新されているので、画面上に設定）
                    Me._G.SetOutLControl(Me._Ds)
                    Exit Sub
                End If
            End If
        End If

        '出荷大の値設定(この時点で印刷フラグ等が更新されているので、画面上に設定）
        Me._G.SetOutLControl(Me._Ds)

        '要望番号:1339 yamanaka 2012.08.27 Start
        If frm.cmbPRINT.SelectedValue.ToString().Equals("07") = True Then
            Me.DicCsvMake(frm, _Ds, "05")
        End If
        '要望番号:1339 yamanaka 2012.08.27 End

        'エラー帳票出力の判定
        Call Me.ShowStorePrintData(frm)  'ADD 2022/01/25 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報


        'プレビュー判定 
        Dim prevDt As DataTable = Me._Ds.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成
            Dim prevFrm As New RDViewer()

            'データ設定
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始
            prevFrm.Run()

            'プレビューフォームの表示
            prevFrm.Show()

            'フォーカス設定
            prevFrm.Focus()

        Else

            'エラーがある場合、エラーメッセージを設定
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Exit Sub
            End If

        End If

        '英語化対応
        '20151022 tsunehira add
        MyBase.ShowMessage(frm, "G002", New String() {frm.btnPRINT.TextValue, ""})
        'MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})

    End Sub

#If True Then    'ADD 2022/01/25 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報

    ''' <summary>
    ''' エラー帳票出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMC020F) As Boolean

        If MyBase.IsMessageStoreExist() = True Then

            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            'MyBase.ShowMessage(frm, "E235")

            Return False

        End If

        Return True

    End Function
#End If

    'START YANAI 要望番号736
    ''' <summary>
    ''' 保存時の分析表印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub BunsekiPrintDataAction(ByVal frm As LMC020F)

        Me._BunsekiArr = New ArrayList()
        Dim allnocheckFlg As Boolean = True
        Dim whereStr As String = String.Empty
        Dim nrsBrCd As String = String.Empty
        Dim goodsCdNrs As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim destCd As String = String.Empty
        Dim outkaNoM As String = String.Empty
        Dim messageFlg As Boolean = True
        'ADD START 2018/11/14 要望番号001939
        Dim whereStrInkaDate As String = String.Empty
        Dim inkaDate As String = String.Empty
        'ADD END   2018/11/14 要望番号001939

        Dim outMdr As DataRow() = Nothing
        Dim outSdr As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("SYS_DEL_FLG = '0'", "OUTKA_NO_M,OUTKA_NO_S,LOT_NO")
        Dim outSmax As Integer = outSdr.Length - 1

        Dim printM_GOODS() As String = New String() {}
        Dim printS_LOT() As String = New String() {}
        Dim printCnt As Integer = 0
        Dim printFlg As Boolean = True
        Dim cMsg As String = String.Empty

        '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen add start 
        Dim stockLotNo As New Hashtable
        '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen add end

        '分析票マスタ(キャッシュの再取得)
        '2020/11/21 千葉BC出荷保存性能劣化対応
        Dim nrsBrCD_temp As String = frm.cmbEigyosyo.SelectedValue.ToString()
        Dim custCDL As String = _Ds.Tables("LMC020_OUTKA_L").Rows(0).Item("CUST_CD_L").ToString()

        If Me._ShikakariHinFlg = "1" Then
            'FFEM 仕掛品の場合は分析票の印刷を行わない
            Exit Sub
        End If
#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山) 
        If (nrsBrCD_temp = "10" AndAlso custCDL = "00135") OrElse (nrsBrCD_temp = "93" AndAlso custCDL = "00135") OrElse
            (nrsBrCD_temp = "96" AndAlso custCDL = "00135") OrElse (nrsBrCD_temp = "98" AndAlso custCDL = "00135") Then

#Else
        If (nrsBrCD_temp = "10" AndAlso custCDL = "00135") OrElse (nrsBrCD_temp = "93" AndAlso custCDL = "00135") OrElse
            (nrsBrCD_temp = "40" AndAlso custCDL = "00555") OrElse (nrsBrCD_temp = "60" AndAlso custCDL = "00135") OrElse
            (nrsBrCD_temp = "96" AndAlso custCDL = "00135") OrElse (nrsBrCD_temp = "98" AndAlso custCDL = "00135") OrElse (nrsBrCD_temp = "F1" AndAlso custCDL = "00135") OrElse
            (nrsBrCD_temp = "F2" AndAlso custCDL = "00135") OrElse (nrsBrCD_temp = "F3" AndAlso custCDL = "00135") Then 'ADD 2022/10/19 033380   【LMS】FFEM足柄工場LMS導入 F2追加, 2023/11/28 039659 F3 追加
#End If
            'FFEM関連だけキャッシュ取得
            MyBase.LMCacheMasterData(LMConst.CacheTBL.COA)
        End If

        For i As Integer = 0 To outSmax
            '出荷(中)を取得
            outMdr = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("OUTKA_NO_M = '", outSdr(i).Item("OUTKA_NO_M").ToString, "' AND ", _
                                                                                "SYS_DEL_FLG = '0'"))

            '分析表区分の判定
            If ("00").Equals(outMdr(0).Item("COA_YN").ToString) = True Then
                Continue For
            End If

            printFlg = True
            For j As Integer = 0 To printCnt - 1
                '商品、小ロット
                If (printM_GOODS(j)).Equals(outMdr(0).Item("GOODS_CD_NRS").ToString) = True AndAlso _
                    (printS_LOT(j)).Equals(outSdr(i).Item("LOT_NO").ToString()) = True Then
                    printFlg = False
                Else
                    printFlg = True
                End If
                If printFlg = False Then
                    Exit For
                End If
            Next
            If printFlg = False Then
                Continue For
            End If

            '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd start 
            '出荷（小）に同じロットナンバーのものがあった場合、最初のものしか出力しない処理
            Dim targetLotNo As String = outSdr(i).Item("LOT_NO").ToString
            If stockLotNo.ContainsKey(targetLotNo) = True Then
                Continue For
            Else
                stockLotNo.Add(targetLotNo, String.Empty)
            End If
            '000878 20180105【LMS】出荷_同商品・ロット違いを一括で分析票添付(群馬村松) Annen upd end 

            If (outkaNoM).Equals(outSdr(i).Item("OUTKA_NO_M").ToString) = False Then
                outkaNoM = outSdr(i).Item("OUTKA_NO_M").ToString
                messageFlg = True
            End If

            nrsBrCd = outMdr(0).Item("NRS_BR_CD").ToString()
            goodsCdNrs = outMdr(0).Item("GOODS_CD_NRS").ToString()
            lotNo = outSdr(i).Item("LOT_NO").ToString()

            '2014.09.25 ADD START
            Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("PRT_NB") = 0
            '2014.09.25 ADD END
            '2018/11/09 ADD START 要望番号002864
            Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("PRT_NB_From") = "1"
            Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("PRT_NB_To") = "1"
            '2018/11/09 ADD END   要望番号002864

            '特定荷主（FFEM）の場合、売上先から、分析表を出力
            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", _Ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), _
                                                                                                     "' AND CUST_CD = '", _Ds.Tables("LMC020_OUTKA_L").Rows(0).Item("CUST_CD_L").ToString(), _
                                                                                                     "' AND SUB_KB = '83'"))

            If custDetailsDr.Length > 0 Then
                '売上先（特殊）
                destCd = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("SHIP_CD_L").ToString()

                '特定荷主（FFEM）の場合　強制的に帳票区分を分析票"05"にする
                Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("PRINT_KB") = "05"

                '売上先コードが空の場合は届け先コードをセット
                If String.IsNullOrEmpty(destCd) = True Then
                    '納品先（標準）
                    destCd = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("DEST_CD").ToString()
                End If

            Else
                '納品先（標準）
                destCd = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("DEST_CD").ToString()
            End If

            '入荷日
            inkaDate = outSdr(i).Item("INKA_DATE").ToString()   'ADD 2018/11/14 要望番号001939

            '条件式の生成
            whereStr = " SYS_DEL_FLG = '0' "
            whereStr = String.Concat(whereStr, " AND NRS_BR_CD = '", nrsBrCd, "'")

            If String.IsNullOrEmpty(goodsCdNrs) = False Then
                whereStr = String.Concat(whereStr, " AND GOODS_CD_NRS = '", goodsCdNrs, "'")
            End If

            If String.IsNullOrEmpty(lotNo) = False Then
                whereStr = String.Concat(whereStr, " AND LOT_NO = '", lotNo, "'")
            End If

            If String.IsNullOrEmpty(destCd) = False Then
                whereStr = String.Concat(whereStr, " AND (DEST_CD = '", destCd, "' OR DEST_CD = 'ZZZZZZZZZZZZZZZ') ")
            Else
                whereStr = String.Concat(whereStr, " AND DEST_CD = 'ZZZZZZZZZZZZZZZ'")
            End If

            'ADD START 2018/11/14 要望番号001939
            If String.IsNullOrEmpty(inkaDate) = False Then
                whereStrInkaDate = String.Concat(" AND INKA_DATE = '", inkaDate, "' ")
            End If
            'ADD END   2018/11/14 要望番号001939

            '存在チェック
            'MOD START 2018/11/14 要望番号001939
            ''Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(whereStr)

            '検索1回目:条件に入荷日を含む
            Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(String.Concat(whereStr, whereStrInkaDate))
            '該当データなしの場合
            If drBunsekiMst.Length() = 0 Then
                '検索2回目:入荷日なし(汎用)
                whereStrInkaDate = " AND INKA_DATE_VERS_FLG = '1' "
                drBunsekiMst = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(String.Concat(whereStr, whereStrInkaDate))
            End If
            'MOD END   2018/11/14 要望番号001939

            If drBunsekiMst.Length() > 0 Then
                '分析票のパスを取得
                If String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_LINK").ToString()) = False OrElse String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_NAME").ToString()) = False Then
                    If messageFlg = True Then
                        Select Case MyBase.ShowMessage(frm, "W193", New String() {outMdr(0).Item("OUTKA_NO_M").ToString(), outMdr(0).Item("GOODS_CD_CUST").ToString()})

                            Case MsgBoxResult.Ok '「OK」押下時

                                '分析票の共通部品呼び出し
                                _BunsekiArr.Add(String.Concat(drBunsekiMst(0).Item("COA_LINK").ToString(), "\", drBunsekiMst(0).Item("COA_NAME").ToString()))
                                allnocheckFlg = False
                                messageFlg = False
                                ReDim Preserve printM_GOODS(printCnt)
                                ReDim Preserve printS_LOT(printCnt)

                                printM_GOODS(printCnt) = outMdr(0).Item("GOODS_CD_NRS").ToString
                                printS_LOT(printCnt) = outSdr(i).Item("LOT_NO").ToString()
                                printCnt = printCnt + 1

                        End Select
                    Else
                        '分析票の共通部品呼び出し
                        _BunsekiArr.Add(String.Concat(drBunsekiMst(0).Item("COA_LINK").ToString(), "\", drBunsekiMst(0).Item("COA_NAME").ToString()))
                    End If

                Else
                    '分析表のパスが空の場合
                    Continue For
                    cMsg = LMConst.FLG.ON
                End If

            Else
                '存在しない場合
                Continue For
                cMsg = LMConst.FLG.ON

            End If

        Next



        '中レコード全行チェック対象外の場合
        If allnocheckFlg = True Then
            _BunsekiArr.Add("ALLNOCHECK")
        End If


        If ("ALLNOCHECK").Equals(Me._BunsekiArr(0)) = True Then
            Exit Sub
        End If


        Dim strPass() As String
        strPass = DirectCast(Me._BunsekiArr.ToArray(GetType(String)), String())
        ' リモートの 分析票 PDF ファイルのローカルへのコピー 
        If strPass.Length <> 0 Then
            strPass = CopyRemotePdf(frm, strPass, 5)
        End If
        If MyBase.PDFDirectPrint(strPass) = False Then
            cMsg = LMConst.FLG.ON
        Else
            If strPass.Length <> 0 Then
                ' 分析票 ログ出力
                Me.WritePrintLog(frm, strPass, 5)
            End If
        End If

        '2014/09/22本来出力するべき（中の分析表が必須なのに、分析表がないとき）
        If LMConst.FLG.ON.Equals(cMsg) Then
            Me.ShowMessage(frm, "C012")
        Else


            Dim actionId As String = "UpdatePrintData"
            '印刷処理
            'Me._Ds.Merge(New RdPrevInfoDS)
            'Me._Ds.Tables(LMConst.RD).Clear()
            Me._Ds = Me.ServerAccess(Me._Ds, actionId)

        End If


    End Sub
    'END YANAI 要望番号736

    ''' <summary>
    ''' 保存時の出荷チェックリスト印刷処理
    ''' </summary>
    ''' <param name="frm"></param>
    Private Function OutboundCheckPrintDataAction(ByVal frm As LMC020F, ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)

        dr.Item("PRINT_KB") = "24"
        dr.Item("PRT_NB") = 0
        dr.Item("PRT_NB_From") = "1"
        dr.Item("PRT_NB_To") = "1"

        '印刷処理
        Return Me.ServerAccess(ds, "UpdatePrintData")

    End Function

    ''' <summary>
    ''' 運賃更新フラグを設定(確認メッセージ有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>更新する場合:"1"　更新しない場合:"0"</returns>
    ''' <remarks></remarks>
    Private Function PrintGMessage(ByVal frm As LMC020F) As String

        '出荷指図印刷以外スルー
        If "07".Equals(frm.cmbPRINT.SelectedValue.ToString()) = False Then
            Return LMConst.FLG.OFF
        End If

        Select Case frm.cmbSagyoSintyoku.SelectedValue.ToString()

            Case LMC020C.SINTYOKU60, LMC020C.SINTYOKU90
            Case Else

                Return LMConst.FLG.OFF

        End Select


        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C005") = MsgBoxResult.Cancel Then
            Return LMConst.FLG.OFF
        End If

        Return LMConst.FLG.ON

    End Function

    ''' <summary>
    ''' 進捗区分の設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetStageData(ByVal frm As LMC020F) As String

        GetStageData = String.Empty

        Dim printKbn As String = frm.cmbPRINT.SelectedValue.ToString()
        Dim oldStateKb As String = Convert.ToString(frm.cmbSagyoSintyoku.SelectedValue)
        Dim newStateKb As String = String.Empty

        'START YANAI 要望番号497
        ''出荷報告の場合
        'If LMC020C.PRINT_HOKOKU.Equals(printKbn) = True Then
        '    Return LMC020C.SINTYOKU90
        'End If
        'END YANAI 要望番号497

        '小レコードがない場合
        Dim outSdr As DataRow() = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("SYS_DEL_FLG = '0'")
        If outSdr.Length < 1 Then
            'START YANAI 要望番号565
            'If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU10 Then
            If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb <= LMC020C.SINTYOKU10 Then
                'END YANAI 要望番号565
                newStateKb = LMC020C.SINTYOKU10
            End If
        End If

        '出荷指図書の場合
        If LMC020C.PRINT_SASHIZUSHO.Equals(printKbn) = True Then
            If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU30 Then
                newStateKb = LMC020C.SINTYOKU30
            End If
        End If

        Dim dr As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)

        'START YANAI No.29
        '出荷指図書印刷制御 = 使用
        If "01".Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString()) = True Then
            If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU10 Then
                newStateKb = LMC020C.SINTYOKU10
            End If
        End If
        'END YANAI No.29

        'まとめ無し
        If "01".Equals(dr.Item("PICK_KB").ToString()) = True Then
            If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU30 Then
                newStateKb = LMC020C.SINTYOKU30
            End If
        End If

        '出庫完了 = 使用
        If "01".Equals(dr.Item("OUTOKA_KANRYO_YN").ToString()) = True Then
            If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU30 Then
                newStateKb = LMC020C.SINTYOKU30
            End If
        End If

        '出荷検品 = 使用
        If "01".Equals(dr.Item("OUTKA_KENPIN_YN").ToString()) = True Then
            If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU40 Then
                newStateKb = LMC020C.SINTYOKU40
            End If
        End If

        If String.IsNullOrEmpty(newStateKb) = True AndAlso oldStateKb < LMC020C.SINTYOKU50 Then
            newStateKb = LMC020C.SINTYOKU50
        End If

        If String.IsNullOrEmpty(newStateKb) = True Then
            newStateKb = oldStateKb
        End If

        Return newStateKb

    End Function

    '要望番号:1339 yamanaka 2012.08.27 Start
    ''' <summary>
    ''' 日立物流出荷音声データCSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DicCsvMake(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal rptFlg As String)

        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, _
                                                                                                                 "' AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, _
                                                                                                                 "' AND SUB_KB = '39'"))
        'LOG
        MyBase.Logger.WriteLog(0, "LMC010H", "DicCsvMake", "1-☆☆保存")

        If 0 < custDetailsDr.Length Then
            Dim setDs As DataSet = New LMC830DS
            Dim setDt As DataTable = setDs.Tables("LMC830IN")
            Dim setDr As DataRow = Nothing
            Dim sysDtTm As String() = MyBase.GetSystemDateTime()
            Dim max As Integer = ds.Tables("LMC020_OUTKA_M").Rows.Count - 1

            'パラメータクラス生成
            Dim prm As LMFormData = New LMFormData()

            'パラメータ設定
            prm.ReturnFlg = False

            '要望番号:1339 yamanaka 2012.09.13 Start
            For i As Integer = 0 To max
                setDr = setDt.NewRow()

                setDr.Item("NRS_BR_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()
                setDr.Item("OUTKA_NO_L") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString()
                setDr.Item("WH_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("WH_CD").ToString()

                '要望番号:1454 yamanaka 2012.09.20 Start
                If Me._NewFlg = True Then
                    setDr.Item("TOU_HAN_FLG") = "00"
                Else
                    setDr.Item("TOU_HAN_FLG") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("TOU_KANRI_YN").ToString()
                End If
                '要望番号:1454 yamanaka 2012.09.20 End

                setDr.Item("SYS_DATE") = sysDtTm(0)
                setDr.Item("SYS_TIME") = Mid(sysDtTm(1), 1, 6)
                setDr.Item("COMPNAME") = Environment.MachineName
                setDr.Item("RPT_FLG") = rptFlg

                If rptFlg = LMC020C.OUTKA_SASHIZU Then
                    setDt.Rows.Add(setDr)
                    Exit For
                Else
                    If ds.Tables("LMC020_OUTKA_M").Rows(i).Item("SYS_DEL_FLG").Equals("1") = True Then
                        setDr.Item("OUTKA_NO_M") = ds.Tables("LMC020_OUTKA_M").Rows(i).Item("OUTKA_NO_M").ToString()
                    Else
                        Continue For
                    End If
                End If
                setDt.Rows.Add(setDr)
            Next

            'setDr.Item("NRS_BR_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString()
            'setDr.Item("OUTKA_NO_L") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("OUTKA_NO_L").ToString()
            'setDr.Item("WH_CD") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("WH_CD").ToString()
            'setDr.Item("TOU_HAN_FLG") = ds.Tables("LMC020_OUTKA_L").Rows(0).Item("TOU_KANRI_YN").ToString()
            'setDr.Item("SYS_DATE") = sysDtTm(0)
            'setDr.Item("SYS_TIME") = Mid(sysDtTm(1), 1, 6)
            'setDr.Item("COMPNAME") = Environment.MachineName
            'setDr.Item("RPT_FLG") = rptFlg
            '要望番号:1339 yamanaka 2012.09.13 End

            'LOG
            MyBase.Logger.WriteLog(0, "LMC010H", "DicCsvMake", "2-☆☆保存")

            'CSV出力処理呼出
            prm.ParamDataSet = setDs
            LMFormNavigate.NextFormNavigate(Me, "LMC830", prm)

            'LOG
            MyBase.Logger.WriteLog(0, "LMC010H", "DicCsvMake", "3-☆☆保存")

        End If

    End Sub

    ''' <summary>
    ''' 日立物流出荷音声データCSV作成判定処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function DicCsvMakeChk(ByVal frm As LMC020F) As String

        Dim dr As DataRow = Me._Ds.Tables("LMC020_OUTKA_L").Rows(0)

        '######## 倉庫マスタ（SOKO_MST）の出荷指図書印刷（CHECKP_11）に"1"が設定されていて、
        '######## 中レコードが削除された場合、出荷取消連絡票（中削除）(LMC600)の印刷
        Dim drm As DataRow() = Nothing
        drm = Me._Ds.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString(), "' AND SYS_DEL_FLG = 1 "))
        If 0 < drm.Length Then
            Return "17"
        End If

        '######## 倉庫マスタ（SOKO_MST）の出荷指図書印刷（CHECKP_11）に"1"が設定されていて、
        '######## 出荷データ（大）のピッキングリスト区分（PICK_KB)が"01"の場合、出荷指図書(LMC520)の印刷処理を行う。
        drm = Me._Ds.Tables("LMC020_OUTKA_M").Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString(), "' AND SYS_DEL_FLG = 0 "))
        Dim max As Integer = drm.Length - 1
        Dim chkFlg As Boolean = True
        For i As Integer = 0 To max
            If ("0").Equals(drm(i).Item("BACKLOG_NB").ToString) = False OrElse _
                ("0.000").Equals(drm(i).Item("BACKLOG_QT").ToString) = False Then
                chkFlg = False
                Exit For
            End If
        Next
        If (dr.Item("OUTKA_STATE_KB_OLD").ToString().Equals(dr.Item("OUTKA_STATE_KB").ToString()) = False AndAlso _
            chkFlg = True) Then
            Return "05"
        End If

        Return String.Empty

    End Function
    '要望番号:1339 yamanaka 2012.08.27 End

    '2018/12/07 ADD START 要望管理002171
    ''' <summary>
    ''' 出荷梱包個数自動計算用データテーブル初期化
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub InitCalcOutkaPkgNbIn(ByVal frm As LMC020F)

        Me._Ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Clear()

        Dim drCalcPkgNbIn As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).NewRow()

        '自動計算フラグ初期化
        drCalcPkgNbIn("AUTO_CALC_FLG") = "0"

        'YCCサクラの場合
        If frm.cmbEigyosyo.SelectedValue.ToString() = "40" AndAlso frm.txtCust_Cd_L.TextValue = "00237" Then
            '荷主明細マスタ取得
            Dim drCustDtl9N() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select( _
                                            String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue.ToString(), "'", _
                                                          " AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, "'", _
                                                          " AND SUB_KB = '9N'"))

            If drCustDtl9N.Length > 0 Then
                '自動計算フラグ設定
                drCalcPkgNbIn("AUTO_CALC_FLG") = drCustDtl9N(0).Item("SET_NAIYO").ToString()
            End If
        End If

        '画面入力値使用フラグ初期化
        drCalcPkgNbIn("USE_GAMEN_VALUE_FLG") = LMC020C.USE_GAMEN_VALUE_FALSE

        Me._Ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Rows.Add(drCalcPkgNbIn)

    End Sub

    ''' <summary>
    ''' 温度管理アラートチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり(本チェックは常に「True:エラーなし」を返す)</returns>
    ''' <remarks></remarks>
    Private Function IsOndoKanriAlertCheck(ByVal frm As LMC020F, ByVal ds As DataSet) As Boolean

        With frm

            Dim nrsBrCd As String = .cmbEigyosyo.SelectedValue.ToString
            Dim outkaDate As String = .imdSyukkaYoteiDate.TextValue

            ' 出荷(中)
            Dim dtM As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_M)
            Dim maxM As Integer = dtM.Rows.Count - 1

            ' 入荷(中) がない場合は終了
            If 0 > maxM Then Return True

            Dim goodsCdNrs As String = String.Empty
            Dim chkedGoodsCdNrsSet As New HashSet(Of String)

            Dim chkDs As DataSet = New LMZ370DS()

            For i As Integer = 0 To maxM

                ' 行削除されているデータはチェックしない。
                If LMConst.FLG.ON.Equals(dtM.Rows(i).Item("SYS_DEL_FLG").ToString()) Then
                    Continue For
                End If

                ' チェック済みの商品の再度のチェックは行わない。
                goodsCdNrs = dtM.Rows(i).Item("GOODS_CD_NRS").ToString()
                If chkedGoodsCdNrsSet.Contains(goodsCdNrs) Then
                    Continue For
                End If
                chkedGoodsCdNrsSet.Add(goodsCdNrs)

                ' 温度管理アラートチェック
                ' DataSet設定
                Dim row As DataRow = chkDs.Tables(LMZ370C.TABLE_NM_IN).NewRow()
                Dim inTbl As DataTable = chkDs.Tables(LMZ370C.TABLE_NM_IN)

                row("NRS_BR_CD") = nrsBrCd
                row("INOUTKA_DATE") = outkaDate
                row("GOODS_CD_NRS") = goodsCdNrs

                inTbl.Rows.Add(row)
            Next

            chkDs = MyBase.CallWSA("LMZ370BLF", "SelectGoodsAndDetails", chkDs)

            ' 温度管理アラートチェック対象商品がない場合は終了
            If chkDs.Tables(LMZ370C.TABLE_NM_OUT).Rows.Count = 0 Then Return True

            ' 警告対象商品が 1件以上存在する場合
            ' 商品名を付与した警告メッセージの表示
            Dim goodsNmMsg As New Text.StringBuilder
            For Each dr As DataRow In chkDs.Tables(LMZ370C.TABLE_NM_OUT).Rows()
                goodsNmMsg.Append(String.Concat(vbCrLf, dr.Item("GOODS_NM").ToString()))
            Next
            Call MyBase.ShowMessage(frm, "W314", New String() {goodsNmMsg.ToString()})

        End With

        Return True

    End Function

    ''' <summary>
    ''' 出荷梱包個数自動計算の実行確認
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ConfirmAutoCalcOutkaPkgNb(ByVal frm As LMC020F)

        '自動計算フラグ＝TRUE、かつ、画面入力値使用フラグ＝FALSEの場合
        If Me._Ds.Tables("LMC020_CALC_OUTKA_PKG_NB_IN").Rows(0).Item("AUTO_CALC_FLG").ToString() = "1" _
        AndAlso Me._Ds.Tables("LMC020_CALC_OUTKA_PKG_NB_IN").Rows(0).Item("USE_GAMEN_VALUE_FLG").ToString() = LMC020C.USE_GAMEN_VALUE_FALSE Then

            '確認メッセージ
            Dim rtn As MsgBoxResult = MyBase.ShowMessage(frm, "C001", New String() {"出荷(大)の出荷梱包個数を自動計算"})
            If rtn <> MsgBoxResult.Ok Then
                '[キャンセル]押下の場合、画面入力値使用フラグをTRUEに変更する
                Me._Ds.Tables("LMC020_CALC_OUTKA_PKG_NB_IN").Rows(0).Item("USE_GAMEN_VALUE_FLG") = LMC020C.USE_GAMEN_VALUE_TRUE
            End If
        End If

    End Sub

    ''' <summary>
    ''' 出荷梱包個数自動計算のエラーチェック
    ''' </summary>
    ''' <returns>True:エラーあり / False:エラーなし</returns>
    ''' <remarks></remarks>
    Private Function IsErrCalcOutkaPkgNb(ds As DataSet) As Boolean

        Dim bErrFlg As Boolean = False

        '自動計算フラグ＝TRUE、かつ、画面入力値使用フラグ＝FALSEの場合
        If Me._Ds.Tables("LMC020_CALC_OUTKA_PKG_NB_IN").Rows(0).Item("AUTO_CALC_FLG").ToString() = "1" _
        AndAlso Me._Ds.Tables("LMC020_CALC_OUTKA_PKG_NB_IN").Rows(0).Item("USE_GAMEN_VALUE_FLG").ToString() = LMC020C.USE_GAMEN_VALUE_FALSE Then

            '計算結果レコードがない場合
            If ds.Tables("LMC020_CALC_OUTKA_PKG_NB_SAKURA").Rows.Count < 1 Then
                bErrFlg = True
            End If

        End If

        Return bErrFlg

    End Function
    '2018/12/07 ADD END   要望管理002171

    ''' <summary>
    ''' 印刷対象リモート PDF のコピー先ディレクトリ名 決定
    ''' </summary>
    Private Sub SetCopyToDirectoryName()

        Me._copyToDirectoryName = String.Concat(SetCopyToParentDirectoryName(), "\", "PDF_COPY")

    End Sub

    ''' <summary>
    ''' 印刷対象リモート PDF のコピー先の親ディレクトリ名 決定
    ''' </summary>
    Private Function SetCopyToParentDirectoryName() As String

        ' 仮初期値の設定
        Dim copyToParentDirectoryName As String = "C:\LMUSER"

        ' 区分マスタよりの分析票ローカルパスの取得
        Dim filter As String = "KBN_GROUP_CD = 'B009' AND KBN_CD = '00'"
        If MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter).Count = 0 Then
            ' 区分マスタが存在しない場合は仮初期値のままとする。
            Return copyToParentDirectoryName
        End If
        Dim coaDir As String =
            (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)(0).Item("KBN_NM1")).ToString()
        If coaDir.Substring(coaDir.Length - 2, 2) = ":\" Then
            ' 分析票ローカルパスがドライブルートの場合は仮初期値のままとする。
            Return copyToParentDirectoryName
        End If
        ' 分析票ローカルパスがドライブルートでない場合
        ' 分析票ローカルパスの親ディレクトリをコピー先の親ディレクトリ名とする。
        Dim di As New System.IO.DirectoryInfo(coaDir)
        copyToParentDirectoryName = di.Parent.FullName
        If copyToParentDirectoryName.Substring(copyToParentDirectoryName.Length - 2, 2) = ":\" Then
            ' ただし、分析票ローカルパスの親ディレクトリがドライブルートの場合は
            ' 分析票ローカルパスそのものをコピー先の親ディレクトリ名とする。
            copyToParentDirectoryName = coaDir
        End If

        Return copyToParentDirectoryName

    End Function

    ''' <summary>
    ''' リモートの 分析票 PDF ファイルのローカルへのコピー 
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="src">リモートファイルパスの配列</param>
    ''' <param name="printKbn">印刷帳票の種類</param>
    ''' <returns>コピー先ローカルファイルパスの配列</returns>
    Private Function CopyRemotePdf(ByVal frm As LMC020F, ByVal src() As String, ByVal printKbn As Integer) As String()

        Dim i As Integer

        ' 戻り値（コピー先ローカルファイルパスの配列）初期化
        ' 初期値：リモートファイルパスそのもの
        Dim dst() As String
        ReDim dst(src.Length() - 1)
        For i = 0 To dst.Length() - 1
            dst(i) = src(i)
        Next

        If Me._copyToDirectoryName.Length > 0 Then
            If printKbn = 5 Then
                ' 分析票 コピー前 ログ出力
                Me.WritePrintLog(frm, src, printKbn + 100)

                ' コピー先ローカルファイルパスの編集
                ' （リモートファイルパスの配列数分の繰り返し処理）
                For i = 0 To src.Length() - 1
                    ' 繰り返し処理の現対象配列要素より手前に同一ファイル名がある場合のカウント
                    Dim dupCnt As Integer = 0
                    Dim dupExists As Boolean = False
                    For j As Integer = 0 To i - 1
                        If System.IO.Path.GetFileName(src(i)).Equals(System.IO.Path.GetFileName(src(j))) Then
                            dupCnt += 1
                            dupExists = True
                        End If
                    Next
                    ' 繰り返し処理の現対象配列要素より後に同一ファイル名があるか否かの判定
                    If Not dupExists Then
                        For k As Integer = i + 1 To src.Length() - 1
                            If System.IO.Path.GetFileName(src(i)).Equals(System.IO.Path.GetFileName(src(k))) Then
                                dupExists = True
                                Exit For
                            End If
                        Next
                    End If
                    ' コピー先ローカルファイルパスの編集
                    ' リモートファイルパスの配列内に同一ファイル名が存在する場合、
                    ' ローカルファイル名の拡張子（例: ".pdf"）の手前に "_" と連番文字列を付加する。
                    ' （リモートファイルパスの配列内に同一ファイル名が存在しなければリモートファイル名そのまま）
                    dst(i) = String.Concat(
                                Me._copyToDirectoryName, "\",
                                System.IO.Path.GetFileNameWithoutExtension(src(i)),
                                IIf(dupExists, String.Concat("_", (dupCnt + 1).ToString()), ""),
                                System.IO.Path.GetExtension(src(i)))
                Next
                Dim pathForLog() As String
                ReDim pathForLog(dst.Length() - 1)
                For i = 0 To dst.Length() - 1
                    Try
                        If Not System.IO.Directory.Exists(Me._copyToDirectoryName) Then
                            Call System.IO.Directory.CreateDirectory(Me._copyToDirectoryName)
                        End If
                        System.IO.File.Copy(src(i), dst(i), True)
                        pathForLog(i) = dst(i)
                    Catch ex As Exception
                        pathForLog(i) = String.Concat(dst(i), " ", "コピー失敗（詳細は次行以降）", vbCrLf, ex.ToString())
                        dst(i) = String.Concat(
                                        Me._copyToDirectoryName, "\",
                                        "コピーに失敗した場合に意図的に印刷を失敗させるための実在しないファイル名", "_",
                                        DateTime.Now.ToString("yyyyMMddHHmmssfff"), "_",
                                        System.IO.Path.ChangeExtension(System.IO.Path.GetRandomFileName(), ".pdf"))
                    End Try
                Next

                ' 分析票 コピー後 ログ出力
                Me.WritePrintLog(frm, pathForLog, printKbn + 200)
            End If
        End If

        Return dst

    End Function

    ''' <summary>
    ''' ログ出力
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub WritePrintLog(ByVal frm As LMC020F, ByVal fileName() As String, ByVal printKbn As Integer)

        Dim setDs As DataSet = New LMC020DS
        Dim setDt As DataTable = setDs.Tables("LMC020IN_WRITE_LOG")
        Dim setDr As DataRow = setDt.NewRow()
        For i As Integer = 0 To fileName.Length - 1
            setDr = setDt.NewRow()
            setDr.Item("PATH") = fileName(i).ToString
            setDr.Item("PRINT_KBN") = printKbn.ToString
            setDt.Rows.Add(setDr)
        Next
        Dim rtnDs As DataSet = MyBase.CallWSA("LMC020BLF", "WritePrintLog", setDs)

    End Sub

    ''' <summary>
    ''' 完了取消時の次回分納チェック用 Rapidus次回分納情報取得
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="outkaNoL"></param>
    ''' <returns></returns>
    Friend Function SelectJikaiBunnouInfo(ByVal nrsBrCd As String, ByVal outkaNoL As String) As DataSet

        Dim ds As DataSet = New LMZ390DS
        Dim dr As DataRow = ds.Tables("LMZ390IN").NewRow

        dr.Item("NRS_BR_CD") = nrsBrCd
        dr.Item("OUTKA_CTL_NO") = outkaNoL
        dr.Item("EDI_CTL_NO") = ""
        dr.Item("TEMPLATE_PREFIX") = LMZ390C.TEMPLATE_PREFIX
        ds.Tables("LMZ390IN").Rows.Add(dr)

        ' Rapidus次回分納情報取得
        ds = MyBase.CallWSA("LMZ390BLF", "SelectJikaiBunnouInfo", ds)

        Return ds

    End Function

#End Region '内部メソッド

#Region "外部メソッド"

    ''' <summary>
    ''' サーバアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ServerAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Dim rtnDs As DataSet = MyBase.CallWSA("LMC020BLF", actionId, ds)

        Dim sysDt As DataTable = rtnDs.Tables(LMC020C.TABLE_NM_SYS_DATETIME)
        If 0 < sysDt.Rows.Count Then

            Me.SysData(LMC020C.SysData.YYYYMMDD) = sysDt.Rows(0).Item("SYS_DATE").ToString()
            Me.SysData(LMC020C.SysData.HHMMSSsss) = sysDt.Rows(0).Item("SYS_TIME").ToString()

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' サーバアクセス(LMQ010)
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ServerAccessLmq010(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        '閾値の設定
        MyBase.SetLimitCount(1000)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(1000)
        Dim rtnDs As DataSet = MyBase.CallWSA("LMQ010BLF", actionId, ds)

        Return rtnDs

    End Function

#End Region '外部メソッド

#Region "PopUp"

    ''' <summary>
    ''' POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMC020F, ByVal objNM As String, ByVal prm As LMFormData) As DataSet

        Dim value As String = String.Empty

        Select Case objNM

            Case LMC020C.EventShubetsu.SINKI.ToString()         '新規

                Dim prmDs As DataSet = New LMZ260DS()
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                If _prmInitDs Is Nothing = False Then
                    Dim dr As DataRow = _prmInitDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows(0)
                    Call Me._G.SetCustInit(dr)
                End If
                row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                row("CUST_CD_S") = "00"
                row("CUST_CD_SS") = "00"
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = False

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case LMC020C.EventShubetsu.INS_M.ToString()         '行追加

                Dim prmDs As DataSet = New LMD100DS()
                Dim row As DataRow = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).NewRow
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                row("WH_CD") = frm.cmbSoko.SelectedValue
                row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                row("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
                row("GOODS_NM") = frm.txtSerchGoodsNm.TextValue
                row("LOT_NO") = frm.txtSerchLot.TextValue.ToUpper
                row("IRIME") = String.Empty
                row("IRIME_UT") = String.Empty
                row("NB_UT") = String.Empty
                row("REMARK") = String.Empty
                row("REMARK_OUT") = String.Empty
                row("TAX_KB") = String.Empty
                row("INKA_STATE_KB") = "00"
                row("DEST_GOODS_FLG") = LMConst.FLG.OFF
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("ZERO_SEARCH_FLG") = "00"
                row("DEST_CD") = frm.txtTodokesakiCd.TextValue()
                row("DEST_NM") = Me.GetCachedDest(frm)
                '2014.09.11 追加START アクタス対応
                row("ADD_FLG") = "01"
                '2014.09.11 追加END アクタス対応
                prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows.Add(row)

                '在庫データの情報をセット
                Dim zaiDr() As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_ZAI).Select("SYS_DEL_FLG = '0'")
                Dim max As Integer = zaiDr.Length - 1
                For i As Integer = 0 To max
                    row = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).NewRow
                    row("ZAI_REC_NO") = zaiDr(i).Item("ZAI_REC_NO")
                    row("PORA_ZAI_NB") = zaiDr(i).Item("PORA_ZAI_NB")
                    row("ALCTD_NB") = zaiDr(i).Item("ALCTD_NB_GAMEN")
                    'START YANAI 要望番号853 まとめ処理対応
                    'row("ALLOC_CAN_NB") = zaiDr(i).Item("ALLOC_CAN_NB_GAMEN")
                    row("ALLOC_CAN_NB") = zaiDr(i).Item("ALLOC_CAN_NB")
                    'END YANAI 要望番号853 まとめ処理対応
                    row("PORA_ZAI_QT") = zaiDr(i).Item("PORA_ZAI_QT")
                    row("ALCTD_QT") = zaiDr(i).Item("ALCTD_QT_GAMEN")
                    'START YANAI 要望番号853 まとめ処理対応
                    'row("ALLOC_CAN_QT") = zaiDr(i).Item("ALLOC_CAN_QT_GAMEN")
                    row("ALLOC_CAN_QT") = zaiDr(i).Item("ALLOC_CAN_QT")
                    'END YANAI 要望番号853 まとめ処理対応
                    '2014.09.11 追加END
                    prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).Rows.Add(row)
                Next

                prm.ParamDataSet = prmDs

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMD100", prm)

            Case LMC020C.EventShubetsu.MASTER.ToString()         'マスタ参照

                Select Case frm.ActiveControl.Name

                    Case "txtSerchGoodsCd", "txtSerchGoodsNm", "txtSerchLot", _
                         "txtGoodsCdCust", "lblGoodsNm" '商品コード、商品名、ロット

                        Dim prmDs As DataSet = New LMD100DS()
                        Dim row As DataRow = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).NewRow
                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("WH_CD") = frm.cmbSoko.SelectedValue
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                        'START YANAI 要望番号481
                        'row("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
                        'row("GOODS_NM") = frm.txtSerchGoodsNm.TextValue
                        'row("LOT_NO") = frm.txtSerchLot.TextValue.ToUpper
                        row("GOODS_CD_CUST") = String.Empty
                        row("GOODS_NM") = String.Empty
                        row("LOT_NO") = String.Empty
                        'END YANAI 要望番号481
                        row("IRIME") = String.Empty
                        row("IRIME_UT") = String.Empty
                        row("NB_UT") = String.Empty
                        row("REMARK") = String.Empty
                        row("REMARK_OUT") = String.Empty
                        row("TAX_KB") = String.Empty
                        row("INKA_STATE_KB") = "00"
                        row("DEST_GOODS_FLG") = LMConst.FLG.OFF
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                        row("ZERO_SEARCH_FLG") = "01"
                        row("DEST_CD") = frm.txtTodokesakiCd.TextValue()
                        row("DEST_NM") = Me.GetCachedDest(frm)
                        prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows.Add(row)

                        '在庫データの情報をセット
                        Dim zaiDr() As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_ZAI).Select("SYS_DEL_FLG = '0'")
                        Dim max As Integer = zaiDr.Length - 1
                        For i As Integer = 0 To max
                            row = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).NewRow
                            row("ZAI_REC_NO") = zaiDr(i).Item("ZAI_REC_NO")
                            row("PORA_ZAI_NB") = zaiDr(i).Item("PORA_ZAI_NB")
                            row("ALCTD_NB") = zaiDr(i).Item("ALCTD_NB_GAMEN")
                            row("ALLOC_CAN_NB") = zaiDr(i).Item("ALLOC_CAN_NB_GAMEN")
                            row("PORA_ZAI_QT") = zaiDr(i).Item("PORA_ZAI_QT")
                            row("ALCTD_QT") = zaiDr(i).Item("ALCTD_QT_GAMEN")
                            row("ALLOC_CAN_QT") = zaiDr(i).Item("ALLOC_CAN_QT_GAMEN")
                            prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).Rows.Add(row)
                        Next

                        prm.ParamDataSet = prmDs

                        'POP呼出
                        LMFormNavigate.NextFormNavigate(Me, "LMD100", prm)

                    Case "txtUriCd", "txtTodokesakiCd"  '売上先、届先

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ210DS()
                        Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        'START YANAI 要望番号481
                        'row("DEST_CD") = value
                        'END YANAI 要望番号481
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        If String.IsNullOrEmpty(value) = True Then
                            Select Case frm.ActiveControl.Name
                                Case "txtUriCd"
                                    frm.lblUriNm.TextValue = String.Empty

                                Case "txtTodokesakiCd"
                                    frm.txtTodokesakiNm.TextValue = String.Empty
                                    frm.txtTodokeAdderss1.TextValue = String.Empty
                                    frm.txtTodokeAdderss2.TextValue = String.Empty
                            End Select
                        End If

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)

                    Case "txtUnsoCompanyCd", "txtUnsoSitenCd"   '運送会社・支店コード

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ250DS()
                        Dim row As DataRow = prmDs.Tables(LMZ250C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        'START YANAI 要望番号513
                        'row("UNSOCO_CD") = frm.txtUnsoCompanyCd.TextValue
                        'row("UNSOCO_BR_CD") = frm.txtUnsoSitenCd.TextValue
                        'END YANAI 要望番号513
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        If String.IsNullOrEmpty(value) = True Then
                            frm.lblUnsoCompanyNm.TextValue = String.Empty
                            frm.lblUnsoSitenNm.TextValue = String.Empty
                        End If

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ250", prm)

                    Case "txtUnthinTariffCd"   '運送タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = Nothing
                        Dim row As DataRow = Nothing
                        If ("40").Equals(frm.cmbTariffKbun.SelectedValue) = True Then
                            '横持ち選択時は横持ちタリフ
                            prmDs = New LMZ100DS()
                            row = prmDs.Tables(LMZ100C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            'START YANAI 要望番号513
                            'row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                            'row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                            'row("YOKO_TARIFF_CD") = value
                            'END YANAI 要望番号513
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ100C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ100", prm)

                        Else
                            '横持ち以外の場合は運賃タリフ
                            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                         , "CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' AND " _
                                                                         , "CUST_CD_M = '", frm.txtCust_Cd_M.TextValue, "' AND " _
                                                                         , "CUST_CD_S = '00' AND " _
                                                                         , "CUST_CD_SS = '00'"))

                            '運賃計算締め基準の値によって、チェック対象の日付を変更
                            Dim checkDate As String = String.Empty
                            If 0 <> dr.Length Then
                                If ("01").Equals(dr(0).Item("UNTIN_CALCULATION_KB")) = True Then
                                    checkDate = frm.imdSyukkaYoteiDate.TextValue
                                Else
                                    checkDate = frm.imdNounyuYoteiDate.TextValue
                                End If
                            End If

                            prmDs = New LMZ230DS()
                            row = prmDs.Tables(LMZ230C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            'row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                            'row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                            'START YANAI 要望番号513
                            'row("UNCHIN_TARIFF_CD") = value
                            'row("STR_DATE") = checkDate
                            'END YANAI 要望番号513
                            'START YANAI 要望番号832
                            row("STR_DATE") = checkDate
                            'END YANAI 要望番号832

                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ230C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ230", prm)

                        End If

                    Case "txtExtcTariffCd"   '割増タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ240DS()
                        Dim row As DataRow = prmDs.Tables(LMZ240C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        'START YANAI 要望番号481
                        'row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        'row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                        'END YANAI 要望番号481
                        'START YANAI 要望番号481
                        'row("EXTC_TARIFF_CD") = value
                        'END YANAI 要望番号481
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ240C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ240", prm)

                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                    Case "txtPayUnthinTariffCd"   '支払タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = Nothing
                        Dim row As DataRow = Nothing
                        If ("40").Equals(frm.cmbTariffKbun.SelectedValue) = True Then
                            '横持ち選択時は支払横持ちタリフ
                            prmDs = New LMZ320DS()
                            row = prmDs.Tables(LMZ320C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ320C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblPayUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ320", prm)

                        Else
                            '横持ち以外の場合は支払運賃タリフ

                            'チェック対象の日付は納入予定日とする
                            Dim checkDate As String = frm.imdNounyuYoteiDate.TextValue
                            prmDs = New LMZ290DS()
                            row = prmDs.Tables(LMZ290C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            row("STR_DATE") = checkDate
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ290C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblPayUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ290", prm)

                        End If

                    Case "txtPayExtcTariffCd"   '支払割増タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ300DS()
                        Dim row As DataRow = prmDs.Tables(LMZ300C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ300C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ300", prm)
                        'END UMANO 要望番号1302 支払運賃に伴う修正。

                    Case "txtSyukkaRemark", "txtHaisoRemark", "txtGoodsRemark"  '注意事項

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ270DS()
                        Dim row As DataRow = prmDs.Tables(LMZ270C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("USER_CD") = LMUserInfoManager.GetUserID()
                        row("SUB_KB") = "02"
                        row("REMARK") = String.Empty
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ270C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ270", prm)


                    Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5", _
                         "txtDestSagyoM1", "txtDestSagyoM2", _
                         "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"   '作業コード
                        Dim sagyoCnt As Integer = 0
                        Dim maxsagyoCnt As String = String.Empty
                        Dim msg As String = String.Empty

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        '英語化対応
                        '20151022 tsunehira add
                        'sagyoCnt値の設定
                        Select Case frm.ActiveControl.Name
                            Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5"
                                maxsagyoCnt = "5"
                                sagyoCnt = 5
                                If String.IsNullOrEmpty(frm.txtSagyoM1.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM2.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM3.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM4.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM5.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                'msg = "作業(中)"
                                If 0 = sagyoCnt Then
                                    MyBase.ShowMessage(frm, "E712", New String() {maxsagyoCnt})
                                    prm.ReturnFlg = False
                                    Return prm.ParamDataSet
                                End If
                            Case "txtDestSagyoM1", "txtDestSagyoM2"
                                maxsagyoCnt = "2"
                                sagyoCnt = 2
                                If String.IsNullOrEmpty(frm.txtDestSagyoM1.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtDestSagyoM2.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                'msg = "届先付帯作業"
                                If 0 = sagyoCnt Then
                                    MyBase.ShowMessage(frm, "E713", New String() {maxsagyoCnt})
                                    prm.ReturnFlg = False
                                    Return prm.ParamDataSet
                                End If

                            Case "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"
                                maxsagyoCnt = "5"
                                sagyoCnt = 5
                                If String.IsNullOrEmpty(frm.txtSagyoL1.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL2.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL3.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL4.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL5.TextValue) = False Then
                                    sagyoCnt = sagyoCnt - 1
                                End If
                                'msg = "作業(大)"
                                If 0 = sagyoCnt Then
                                    MyBase.ShowMessage(frm, "E714", New String() {maxsagyoCnt})
                                    prm.ReturnFlg = False
                                    Return prm.ParamDataSet
                                End If

                        End Select

                        'If 0 = sagyoCnt Then
                        '    MyBase.ShowMessage(frm, "E242", New String() {msg, maxsagyoCnt})
                        '    prm.ReturnFlg = False
                        '    Return prm.ParamDataSet
                        'End If

                        '作業項目マスタ照会
                        Dim prmDs As DataSet = New LMZ200DS()
                        Dim row As DataRow = prmDs.Tables(LMZ200C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        'START YANAI 要望番号481
                        'row("SAGYO_CD") = value
                        'END YANAI 要望番号481
                        row("SAGYO_CNT") = sagyoCnt
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ200C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        Select Case frm.ActiveControl.Name
                            Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5"
                                If String.IsNullOrEmpty(frm.txtSagyoM1.TextValue) = True Then
                                    frm.lblSagyoM1.TextValue = String.Empty
                                    frm.txtSagyoRemarkM1.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM2.TextValue) = True Then
                                    frm.lblSagyoM2.TextValue = String.Empty
                                    frm.txtSagyoRemarkM2.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM3.TextValue) = True Then
                                    frm.lblSagyoM3.TextValue = String.Empty
                                    frm.txtSagyoRemarkM3.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM4.TextValue) = True Then
                                    frm.lblSagyoM4.TextValue = String.Empty
                                    frm.txtSagyoRemarkM4.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM5.TextValue) = True Then
                                    frm.lblSagyoM5.TextValue = String.Empty
                                    frm.txtSagyoRemarkM5.TextValue = String.Empty
                                End If
                            Case "txtDestSagyoM1", "txtDestSagyoM2"
                                If String.IsNullOrEmpty(frm.txtDestSagyoM1.TextValue) = True Then
                                    frm.lblDestSagyoM1.TextValue = String.Empty
                                    frm.txtDestSagyoRemarkM1.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtDestSagyoM2.TextValue) = True Then
                                    frm.lblDestSagyoM2.TextValue = String.Empty
                                    frm.txtDestSagyoRemarkM2.TextValue = String.Empty
                                End If
                            Case "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"
                                If String.IsNullOrEmpty(frm.txtSagyoL1.TextValue) = True Then
                                    frm.lblSagyoL1.TextValue = String.Empty
                                    frm.txtSagyoRemarkL1.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL2.TextValue) = True Then
                                    frm.lblSagyoL2.TextValue = String.Empty
                                    frm.txtSagyoRemarkL2.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL3.TextValue) = True Then
                                    frm.lblSagyoL3.TextValue = String.Empty
                                    frm.txtSagyoRemarkL3.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL4.TextValue) = True Then
                                    frm.lblSagyoL4.TextValue = String.Empty
                                    frm.txtSagyoRemarkL4.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL5.TextValue) = True Then
                                    frm.lblSagyoL5.TextValue = String.Empty
                                    frm.txtSagyoRemarkL5.TextValue = String.Empty
                                End If
                        End Select

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ200", prm)

                        '2014/01/23 輸出情報追加 START
                    Case frm.txtShipperCd.Name  '届先マスタ

                        Dim prmDs As DataSet = New LMZ210DS()
                        Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg
                        If String.IsNullOrEmpty(frm.txtShipperCd.TextValue) = True Then
                            frm.lblShipperNm.TextValue = String.Empty
                        End If

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)

                        '2014/01/23 輸出情報追加 END

                    Case Else
                        'ポップ対象外のテキストの場合
                        MyBase.ShowMessage(frm, "G005")

                End Select

            Case LMC020C.EventShubetsu.MASTER_ENTER.ToString()         'マスタEnter処理（作業コードのみ）
                Select Case frm.ActiveControl.Name
                    Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5", _
                         "txtDestSagyoM1", "txtDestSagyoM2", _
                         "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"   '作業コード
                        Dim sagyoCnt As Integer = 1
                        Dim maxsagyoCnt As String = String.Empty
                        Dim msg As String = String.Empty

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        '作業項目マスタ照会
                        Dim prmDs As DataSet = New LMZ200DS()
                        Dim row As DataRow = prmDs.Tables(LMZ200C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        row("SAGYO_CD") = value
                        row("SAGYO_CNT") = sagyoCnt
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ200C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        Select Case frm.ActiveControl.Name
                            Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5"
                                If String.IsNullOrEmpty(frm.txtSagyoM1.TextValue) = True Then
                                    frm.lblSagyoM1.TextValue = String.Empty
                                    frm.txtSagyoRemarkM1.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM2.TextValue) = True Then
                                    frm.lblSagyoM2.TextValue = String.Empty
                                    frm.txtSagyoRemarkM2.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM3.TextValue) = True Then
                                    frm.lblSagyoM3.TextValue = String.Empty
                                    frm.txtSagyoRemarkM3.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM4.TextValue) = True Then
                                    frm.lblSagyoM4.TextValue = String.Empty
                                    frm.txtSagyoRemarkM4.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoM5.TextValue) = True Then
                                    frm.lblSagyoM5.TextValue = String.Empty
                                    frm.txtSagyoRemarkM5.TextValue = String.Empty
                                End If
                            Case "txtDestSagyoM1", "txtDestSagyoM2"
                                If String.IsNullOrEmpty(frm.txtDestSagyoM1.TextValue) = True Then
                                    frm.lblDestSagyoM1.TextValue = String.Empty
                                    frm.txtDestSagyoRemarkM1.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtDestSagyoM2.TextValue) = True Then
                                    frm.lblDestSagyoM2.TextValue = String.Empty
                                    frm.txtDestSagyoRemarkM2.TextValue = String.Empty
                                End If
                            Case "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"
                                If String.IsNullOrEmpty(frm.txtSagyoL1.TextValue) = True Then
                                    frm.lblSagyoL1.TextValue = String.Empty
                                    frm.txtSagyoRemarkL1.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL2.TextValue) = True Then
                                    frm.lblSagyoL2.TextValue = String.Empty
                                    frm.txtSagyoRemarkL2.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL3.TextValue) = True Then
                                    frm.lblSagyoL3.TextValue = String.Empty
                                    frm.txtSagyoRemarkL3.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL4.TextValue) = True Then
                                    frm.lblSagyoL4.TextValue = String.Empty
                                    frm.txtSagyoRemarkL4.TextValue = String.Empty
                                End If
                                If String.IsNullOrEmpty(frm.txtSagyoL5.TextValue) = True Then
                                    frm.lblSagyoL5.TextValue = String.Empty
                                    frm.txtSagyoRemarkL5.TextValue = String.Empty
                                End If
                        End Select

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ200", prm)

                        'START YANAI 要望番号513
                    Case "txtUnsoCompanyCd", "txtUnsoSitenCd"   '運送会社・支店コード

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ250DS()
                        Dim row As DataRow = prmDs.Tables(LMZ250C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("UNSOCO_CD") = frm.txtUnsoCompanyCd.TextValue
                        row("UNSOCO_BR_CD") = frm.txtUnsoSitenCd.TextValue
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        If String.IsNullOrEmpty(value) = True Then
                            frm.lblUnsoCompanyNm.TextValue = String.Empty
                            frm.lblUnsoSitenNm.TextValue = String.Empty
                        End If

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ250", prm)

                    Case "txtUnthinTariffCd"   '運送タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = Nothing
                        Dim row As DataRow = Nothing
                        If ("40").Equals(frm.cmbTariffKbun.SelectedValue) = True Then
                            '横持ち選択時は横持ちタリフ
                            prmDs = New LMZ100DS()
                            row = prmDs.Tables(LMZ100C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            'START YANAI 要望番号481
                            'row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                            'row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                            'END YANAI 要望番号481
                            row("YOKO_TARIFF_CD") = value
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ100C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ100", prm)

                        Else
                            '横持ち以外の場合は運賃タリフ
                            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                         , "CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' AND " _
                                                                         , "CUST_CD_M = '", frm.txtCust_Cd_M.TextValue, "' AND " _
                                                                         , "CUST_CD_S = '00' AND " _
                                                                         , "CUST_CD_SS = '00'"))

                            '運賃計算締め基準の値によって、チェック対象の日付を変更
                            Dim checkDate As String = String.Empty
                            If 0 <> dr.Length Then
                                If ("01").Equals(dr(0).Item("UNTIN_CALCULATION_KB")) = True Then
                                    checkDate = frm.imdSyukkaYoteiDate.TextValue
                                Else
                                    checkDate = frm.imdNounyuYoteiDate.TextValue
                                End If
                            End If

                            prmDs = New LMZ230DS()
                            row = prmDs.Tables(LMZ230C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            'row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                            'row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                            row("UNCHIN_TARIFF_CD") = value
                            row("STR_DATE") = checkDate
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ230C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ230", prm)

                        End If
                        'END YANAI 要望番号513
                        'START YANAI 要望番号481
                    Case "txtExtcTariffCd"   '割増タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ240DS()
                        Dim row As DataRow = prmDs.Tables(LMZ240C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        'row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        'row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                        row("EXTC_TARIFF_CD") = value
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ240C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ240", prm)

                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                    Case "txtPayUnthinTariffCd"   '支払運賃タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = Nothing
                        Dim row As DataRow = Nothing
                        If ("40").Equals(frm.cmbTariffKbun.SelectedValue) = True Then
                            '横持ち選択時は支払横持ちタリフ
                            prmDs = New LMZ320DS()
                            row = prmDs.Tables(LMZ320C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            row("YOKO_TARIFF_CD") = value
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ320C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblPayUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ320", prm)

                        Else
                            '横持ち以外の場合は支払運賃タリフ

                            'チェック対象の日付は納入予定日とする
                            Dim checkDate As String = frm.imdNounyuYoteiDate.TextValue
                            prmDs = New LMZ290DS()
                            row = prmDs.Tables(LMZ290C.TABLE_NM_IN).NewRow

                            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                            'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                            row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                            row("UNSOCO_CD") = frm.txtUnsoCompanyCd.TextValue
                            row("UNSOCO_BR_CD") = frm.txtUnsoSitenCd.TextValue
                            row("SHIHARAI_TARIFF_CD") = value
                            row("STR_DATE") = checkDate
                            row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                            prmDs.Tables(LMZ290C.TABLE_NM_IN).Rows.Add(row)
                            prm.ParamDataSet = prmDs
                            prm.SkipFlg = Me._PopupSkipFlg

                            If String.IsNullOrEmpty(value) = True Then
                                frm.lblPayUnthinTariffNm.TextValue = String.Empty
                            End If

                            'Pop起動
                            LMFormNavigate.NextFormNavigate(Me, "LMZ290", prm)

                        End If

                    Case "txtPayExtcTariffCd"   '割増タリフ

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ300DS()
                        Dim row As DataRow = prmDs.Tables(LMZ300C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("UNSOCO_CD") = frm.txtUnsoCompanyCd.TextValue
                        row("UNSOCO_BR_CD") = frm.txtUnsoSitenCd.TextValue
                        row("EXTC_TARIFF_CD") = value
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ300C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ300", prm)
                        'END UMANO 要望番号1302 支払運賃に伴う修正。

                    Case "txtUriCd", "txtTodokesakiCd"  '売上先、届先

                        'value値の設定
                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        value = txtCtl.TextValue

                        Dim prmDs As DataSet = New LMZ210DS()
                        Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        row("DEST_CD") = value
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        If String.IsNullOrEmpty(value) = True Then
                            Select Case frm.ActiveControl.Name
                                Case "txtUriCd"
                                    frm.lblUriNm.TextValue = String.Empty

                                Case "txtTodokesakiCd"
                                    frm.txtTodokesakiNm.TextValue = String.Empty
                                    frm.txtTodokeAdderss1.TextValue = String.Empty
                                    frm.txtTodokeAdderss2.TextValue = String.Empty
                            End Select
                        End If

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)

                    Case "txtSerchGoodsCd", "txtSerchGoodsNm", "txtSerchLot", _
                         "txtGoodsCdCust", "lblGoodsNm" '商品コード、商品名、ロット

                        Dim prmDs As DataSet = New LMD100DS()
                        Dim row As DataRow = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).NewRow
                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LM.Base.LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("WH_CD") = frm.cmbSoko.SelectedValue
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue

                        '要望対応:1595 yamanaka 2012.11.15 Start
                        If frm.ActiveControl.Name = "txtGoodsCdCust" Then
                            row("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
                        Else
                            row("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
                        End If

                        If frm.ActiveControl.Name = "lblGoodsNm" Then
                            row("GOODS_NM") = frm.lblGoodsNm.TextValue
                        Else
                            row("GOODS_NM") = frm.txtSerchGoodsNm.TextValue
                        End If
                        'row("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
                        'row("GOODS_NM") = frm.txtSerchGoodsNm.TextValue
                        '要望対応:1595 yamanaka 2012.11.15 End

#If True Then   'ADD 2020/06/23 012642   引き当て時に古いロット（入庫日）がわからない
                        '入庫日表示ソート指定有か
                        Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat(
                                                                           "KBN_GROUP_CD = 'Z026' AND " _
                                                                         , "KBN_NM1 = '", frm.cmbEigyosyo.SelectedValue.ToString(), "' AND " _
                                                                         , "KBN_NM2 = '", frm.txtCust_Cd_L.TextValue, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
                        If 0 < dr.Length Then
                            row("INKO_DATE_FLG") = LMConst.FLG.ON
                        Else
                            row("INKO_DATE_FLG") = String.Empty
                        End If
#End If

                        row("LOT_NO") = frm.txtSerchLot.TextValue.ToUpper
                        row("IRIME") = String.Empty
                        row("IRIME_UT") = String.Empty
                        row("NB_UT") = String.Empty
                        row("REMARK") = String.Empty
                        row("REMARK_OUT") = String.Empty
                        row("TAX_KB") = String.Empty
                        row("INKA_STATE_KB") = "00"
                        row("DEST_GOODS_FLG") = LMConst.FLG.OFF
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                        row("ZERO_SEARCH_FLG") = "01"
                        row("DEST_CD") = frm.txtTodokesakiCd.TextValue()
                        row("DEST_NM") = Me.GetCachedDest(frm)
                        prmDs.Tables(LMControlC.LMD100C_TABLE_NM_IN).Rows.Add(row)

                        '在庫データの情報をセット
                        Dim zaiDr() As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_ZAI).Select("SYS_DEL_FLG = '0'")
                        Dim max As Integer = zaiDr.Length - 1
                        For i As Integer = 0 To max
                            row = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).NewRow
                            row("ZAI_REC_NO") = zaiDr(i).Item("ZAI_REC_NO")
                            row("PORA_ZAI_NB") = zaiDr(i).Item("PORA_ZAI_NB")
                            row("ALCTD_NB") = zaiDr(i).Item("ALCTD_NB_GAMEN")
                            row("ALLOC_CAN_NB") = zaiDr(i).Item("ALLOC_CAN_NB_GAMEN")
                            row("PORA_ZAI_QT") = zaiDr(i).Item("PORA_ZAI_QT")
                            row("ALCTD_QT") = zaiDr(i).Item("ALCTD_QT_GAMEN")
                            row("ALLOC_CAN_QT") = zaiDr(i).Item("ALLOC_CAN_QT_GAMEN")
                            prmDs.Tables(LMControlC.LMD100C_TABLE_NM_ZAI).Rows.Add(row)
                        Next

                        prm.ParamDataSet = prmDs

                        'POP呼出
                        LMFormNavigate.NextFormNavigate(Me, "LMD100", prm)
                        'END YANAI 要望番号481

                        '2014/01/23 輸出情報追加 START
                    Case frm.txtShipperCd.Name  '届先マスタ

                        Dim prmDs As DataSet = New LMZ210DS()
                        Dim row As DataRow = prmDs.Tables(LMZ210C.TABLE_NM_IN).NewRow

                        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                        'row("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                        row("DEST_CD") = frm.txtShipperCd.TextValue
                        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

                        prmDs.Tables(LMZ210C.TABLE_NM_IN).Rows.Add(row)
                        prm.ParamDataSet = prmDs
                        prm.SkipFlg = Me._PopupSkipFlg

                        If String.IsNullOrEmpty(frm.txtShipperCd.TextValue) = True Then
                            frm.lblShipperNm.TextValue = String.Empty
                        End If

                        'Pop起動
                        LMFormNavigate.NextFormNavigate(Me, "LMZ210", prm)

                        '2014/01/23 輸出情報追加 END

                End Select

            Case LMC020C.EventShubetsu.CHANGE_GOODS.ToString()

                Dim prmDs As DataSet = New LMZ020DS()
                Dim row As DataRow = prmDs.Tables(LMZ020C.TABLE_NM_IN).NewRow

                With row
                    '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                    '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                    .Item("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
                    .Item("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                    .Item("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
                    .Item("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
                    .Item("IRIME") = frm.numIrime.TextValue
                    .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                End With
                prmDs.Tables(LMZ020C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = True

                LMFormNavigate.NextFormNavigate(Me, "LMZ020", prm)

        End Select

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' POPからの戻り値を設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetPopupReturn(ByVal frm As LMC020F, ByVal objNM As String, ByVal prm As LMFormData)

        '戻り値を画面に設定
        Dim prmDs As DataSet = prm.ParamDataSet
        Dim dt As DataTable = Nothing
        Dim max As Integer = 0
        Dim dRow As DataRow = Nothing

        Select Case objNM
            Case LMC020C.EventShubetsu.CHANGE_GOODS.ToString()
                dt = prmDs.Tables(LMZ020C.TABLE_NM_OUT)
                If Not dt Is Nothing Then
                    If 0 < dt.Rows.Count Then
                        dRow = dt.Rows(0)

                        frm.txtGoodsCdCust.TextValue = dRow.Item("GOODS_CD_CUST").ToString()
                        frm.lblGoodsNm.TextValue = dRow.Item("GOODS_NM_1").ToString()
                        frm.lblGoodsCdNrs.TextValue = dRow.Item("GOODS_CD_NRS").ToString()
                    End If
                End If

            Case Else
                Select Case frm.ActiveControl.Name
                    Case "txtUriCd", "txtTodokesakiCd"  '売上先、届先
                        dt = prmDs.Tables(LMZ210C.TABLE_NM_OUT)
                        dRow = dt.Rows(0)

                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        txtCtl.TextValue = dRow.Item("DEST_CD").ToString()
                        'START YANAI 要望番号909
                        frm.txtTodokesakiCdOld.TextValue = dRow.Item("DEST_CD").ToString()
                        'END YANAI 要望番号909

                        If ("txtUriCd").Equals(frm.ActiveControl.Name) = True Then
                            frm.lblUriNm.TextValue = dRow.Item("DEST_NM").ToString()
                        Else
                            frm.txtTodokesakiNm.TextValue = dRow.Item("DEST_NM").ToString()
                            frm.txtTodokeAdderss1.TextValue = dRow.Item("AD_1").ToString()
                            frm.txtTodokeAdderss2.TextValue = dRow.Item("AD_2").ToString()
                            frm.txtTodokeAdderss3.TextValue = dRow.Item("AD_3").ToString()
                            frm.txtTodokeTel.TextValue = dRow.Item("TEL").ToString()
                            'START YANAI 要望番号909
                            'frm.txtHaisoRemark.TextValue = dRow.Item("DELI_ATT").ToString
                            frm.txtHaisoRemarkNew.TextValue = dRow.Item("DELI_ATT").ToString
                            'END YANAI 要望番号909
                            frm.cmbSiteinouhin.SelectedValue = dRow.Item("SP_NHS_KB").ToString
                            If String.IsNullOrEmpty(dRow.Item("KYORI").ToString) = True Then
                                frm.numKyori.Value = 0
                            Else
                                frm.numKyori.Value = dRow.Item("KYORI").ToString
                            End If

                            frm.cmbBunsakiTmp.SelectedValue = dRow.Item("COA_YN").ToString

                            '運送会社設定
                            Me._G.GetUnsoCompanyChangeDest(frm)

                            '要望番号:1605 yamanaka 2012.11.21 Start
                            'START YANAI 要望番号745
                            If String.IsNullOrEmpty(dRow.Item("URIAGE_CD").ToString()) = False Then
                                frm.txtUriCd.TextValue = dRow.Item("URIAGE_CD").ToString()

                                '---↓
                                'Dim destDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' AND DEST_CD = '", dRow.Item("URIAGE_CD").ToString(), "'"))

                                Dim destMstDs As MDestDS = New MDestDS
                                Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                                destMstDr.Item("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                                destMstDr.Item("DEST_CD") = dRow.Item("URIAGE_CD").ToString()
                                destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
                                destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                                Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                                Dim destDr() As DataRow = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                                '---↑

                                If 0 < destDr.Length Then
                                    frm.lblUriNm.TextValue = destDr(0).Item("DEST_NM").ToString()
                                Else
                                    frm.lblUriNm.TextValue = String.Empty
                                End If

                            Else

                                '要望番号1943 修正START
                                If String.IsNullOrEmpty(frm.txtUriCd.TextValue) = False AndAlso frm.txtUriCd.ReadOnly = False Then
                                    '既にコードが入っている場合、売上先コード・名称の入替は行わない
                                Else
                                    frm.txtUriCd.TextValue = String.Empty
                                    frm.lblUriNm.TextValue = String.Empty
                                End If
                                '要望番号1943 修正END
                            End If
                            'END YANAI 要望番号745
                            '要望番号:1605 yamanaka 2012.11.21 End

                            'START YANAI 要望番号880
                            Call Me._G.GetTariffSet(frm)
                            'END YANAI 要望番号880

                            'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                            '運送会社コードOLD設定
                            Me._G.SetUnsoCdOld(frm)

                            '運賃タリフセットからタリフコードを設定
                            Call Me._G.GetUnchinTariffSet(frm, False)
                            'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

                        End If

                    Case "txtSerchGoodsCd", "txtSerchGoodsNm", "txtSerchLot"
                        dt = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_OUT)
                        If Not dt Is Nothing Then
                            If 0 < dt.Rows.Count Then
                                dRow = dt.Rows(0)

                                frm.txtSerchGoodsCd.TextValue = dRow.Item("GOODS_CD_CUST").ToString()
                                frm.txtSerchGoodsNm.TextValue = dRow.Item("NM_1").ToString()
                                frm.txtSerchLot.TextValue = dRow.Item("LOT_NO").ToString()

                            End If
                        End If

                    Case "txtUnsoCompanyCd", "txtUnsoSitenCd"   '運送会社・支店コード
                        dt = prmDs.Tables(LMZ250C.TABLE_NM_OUT)
                        dRow = dt.Rows(0)

                        frm.txtUnsoCompanyCd.TextValue = dRow.Item("UNSOCO_CD").ToString()
                        frm.txtUnsoSitenCd.TextValue = dRow.Item("UNSOCO_BR_CD").ToString()
                        frm.lblUnsoCompanyNm.TextValue = dRow.Item("UNSOCO_NM").ToString()
                        frm.lblUnsoSitenNm.TextValue = dRow.Item("UNSOCO_BR_NM").ToString()
                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                        If frm.txtPayUnthinTariffCd.TextValue = Nothing Then
                            frm.txtPayUnthinTariffCd.TextValue = dRow.Item("UNCHIN_TARIFF_CD").ToString()
                            frm.lblPayUnthinTariffNm.TextValue = dRow.Item("UNCHIN_TARIFF_REM").ToString()

                        End If
                        If frm.txtPayExtcTariffCd.TextValue = Nothing Then
                            frm.txtPayExtcTariffCd.TextValue = dRow.Item("EXTC_TARIFF_CD").ToString()
                        End If
                        'END UMANO 要望番号1302 支払運賃に伴う修正。

                        'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                        '運送会社コードOLD設定
                        Me._G.SetUnsoCdOld(frm)

                        '運賃タリフセットからタリフコードを設定
                        Call Me._G.GetUnchinTariffSet(frm, True)
                        'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

                    Case "txtUnthinTariffCd"   '運送タリフ

                        If ("40").Equals(frm.cmbTariffKbun.SelectedValue) = True Then
                            '横持ちの場合は横持ちタリフコードをセット
                            dt = prmDs.Tables(LMZ100C.TABLE_NM_OUT)
                            dRow = dt.Rows(0)
                            frm.txtUnthinTariffCd.TextValue = dRow.Item("YOKO_TARIFF_CD").ToString()
                            frm.lblUnthinTariffNm.TextValue = dRow.Item("YOKO_REM").ToString()
                        Else
                            '横持ち以外の場合は運賃タリフコードをセット
                            dt = prmDs.Tables(LMZ230C.TABLE_NM_OUT)
                            dRow = dt.Rows(0)
                            frm.txtUnthinTariffCd.TextValue = dRow.Item("UNCHIN_TARIFF_CD").ToString()
                            frm.lblUnthinTariffNm.TextValue = dRow.Item("UNCHIN_TARIFF_REM").ToString()
                        End If

                    Case "txtExtcTariffCd"   '割増タリフ
                        dt = prmDs.Tables(LMZ240C.TABLE_NM_OUT)
                        dRow = dt.Rows(0)

                        frm.txtExtcTariffCd.TextValue = dRow.Item("EXTC_TARIFF_CD").ToString()

                        'START UMANO 要望番号1302 支払運賃に伴う修正。
                    Case "txtPayUnthinTariffCd"   '支払運賃タリフ

                        If ("40").Equals(frm.cmbTariffKbun.SelectedValue) = True Then
                            '横持ちの場合は支払横持ちタリフコードをセット
                            dt = prmDs.Tables(LMZ320C.TABLE_NM_OUT)
                            dRow = dt.Rows(0)
                            frm.txtPayUnthinTariffCd.TextValue = dRow.Item("YOKO_TARIFF_CD").ToString()
                            frm.lblPayUnthinTariffNm.TextValue = dRow.Item("YOKO_REM").ToString()
                        Else
                            '横持ち以外の場合は支払運賃タリフコードをセット
                            dt = prmDs.Tables(LMZ290C.TABLE_NM_OUT)
                            dRow = dt.Rows(0)
                            frm.txtPayUnthinTariffCd.TextValue = dRow.Item("SHIHARAI_TARIFF_CD").ToString()
                            frm.lblPayUnthinTariffNm.TextValue = dRow.Item("SHIHARAI_TARIFF_REM").ToString()
                        End If

                    Case "txtPayExtcTariffCd"   '支払割増タリフ
                        dt = prmDs.Tables(LMZ300C.TABLE_NM_OUT)
                        dRow = dt.Rows(0)

                        frm.txtPayExtcTariffCd.TextValue = dRow.Item("EXTC_TARIFF_CD").ToString()
                        'END UMANO 要望番号1302 支払運賃に伴う修正。


                    Case "txtSyukkaRemark", "txtHaisoRemark", "txtGoodsRemark"  '注意事項
                        dt = prmDs.Tables(LMZ270C.TABLE_NM_OUT)
                        dRow = dt.Rows(0)

                        Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                        'START YANAI 要望番号516
                        'txtCtl.TextValue = dRow.Item("REMARK").ToString()
                        If String.IsNullOrEmpty(txtCtl.TextValue) = True Then
                            '値が設定されていない場合は、戻り値をそのまま設定
                            txtCtl.TextValue = dRow.Item("REMARK").ToString()
                        Else
                            '値が設定されている場合は、元々の値 & 半角スペース & 戻り値を設定
                            txtCtl.TextValue = String.Concat(txtCtl.TextValue, Space(1), dRow.Item("REMARK").ToString())
                        End If
                        'END YANAI 要望番号516

                    Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5", _
                         "txtDestSagyoM1", "txtDestSagyoM2", _
                         "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"   '作業コード
                        If objNM.Equals(LMC020C.EventShubetsu.MASTER.ToString()) = True Then         'マスタ参照
                            'マスタ参照ボタン押下時

                            '戻り値を画面に設定
                            dt = prmDs.Tables(LMZ200C.TABLE_NM_OUT)
                            max = dt.Rows.Count - 1

                            If -1 = max Then
                                Exit Sub
                            End If

                            If 0 = max Then
                                '1件の時は選択されたコントロールに設定
                                'value値の設定
                                Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                                txtCtl.TextValue = dt.Rows(0).Item("SAGYO_CD").ToString

                                Select Case frm.ActiveControl.Name
                                    Case "txtSagyoM1"
                                        frm.lblSagyoM1.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkM1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoM2"
                                        frm.lblSagyoM2.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkM2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoM3"
                                        frm.lblSagyoM3.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkM3.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoM4"
                                        frm.lblSagyoM4.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkM4.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoM5"
                                        frm.lblSagyoM5.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkM5.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtDestSagyoM1"
                                        frm.lblDestSagyoM1.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtDestSagyoRemarkM1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtDestSagyoM2"
                                        frm.lblDestSagyoM2.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtDestSagyoRemarkM2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoL1"
                                        frm.lblSagyoL1.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkL1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoL2"
                                        frm.lblSagyoL2.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkL2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoL3"
                                        frm.lblSagyoL3.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkL3.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoL4"
                                        frm.lblSagyoL4.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkL4.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                    Case "txtSagyoL5"
                                        frm.lblSagyoL5.TextValue = dt.Rows(0).Item("SAGYO_RYAK").ToString
                                        frm.txtSagyoRemarkL5.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                End Select
                                Exit Sub
                            End If

                            Select Case frm.ActiveControl.Name
                                Case "txtSagyoM1", "txtSagyoM2", "txtSagyoM3", "txtSagyoM4", "txtSagyoM5"
                                    For i As Integer = 0 To max
                                        dRow = dt.Rows(i)

                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoM1.TextValue)) = True Then
                                            frm.txtSagyoM1.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoM1.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkM1.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoM2.TextValue)) = True Then
                                            frm.txtSagyoM2.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoM2.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkM2.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoM3.TextValue)) = True Then
                                            frm.txtSagyoM3.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoM3.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkM3.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoM4.TextValue)) = True Then
                                            frm.txtSagyoM4.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoM4.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkM4.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoM5.TextValue)) = True Then
                                            frm.txtSagyoM5.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoM5.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkM5.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                    Next

                                Case "txtDestSagyoM1", "txtDestSagyoM2"
                                    For i As Integer = 0 To max
                                        dRow = dt.Rows(i)

                                        If String.IsNullOrEmpty(Trim(frm.txtDestSagyoM1.TextValue)) = True Then
                                            frm.txtDestSagyoM1.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblDestSagyoM1.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtDestSagyoRemarkM1.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtDestSagyoM2.TextValue)) = True Then
                                            frm.txtDestSagyoM2.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblDestSagyoM2.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtDestSagyoRemarkM2.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                    Next

                                Case "txtSagyoL1", "txtSagyoL2", "txtSagyoL3", "txtSagyoL4", "txtSagyoL5"
                                    For i As Integer = 0 To max
                                        dRow = dt.Rows(i)

                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoL1.TextValue)) = True Then
                                            frm.txtSagyoL1.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoL1.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkL1.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoL2.TextValue)) = True Then
                                            frm.txtSagyoL2.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoL2.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkL2.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoL3.TextValue)) = True Then
                                            frm.txtSagyoL3.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoL3.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkL3.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoL4.TextValue)) = True Then
                                            frm.txtSagyoL4.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoL4.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkL4.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If
                                        If String.IsNullOrEmpty(Trim(frm.txtSagyoL5.TextValue)) = True Then
                                            frm.txtSagyoL5.TextValue = dRow.Item("SAGYO_CD").ToString
                                            frm.lblSagyoL5.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                            frm.txtSagyoRemarkL5.TextValue = dRow.Item("WH_SAGYO_REMARK").ToString
                                            Continue For
                                        End If

                                    Next



                            End Select

                        Else
                            'Enter押下時

                            '戻り値を画面に設定
                            dt = prmDs.Tables(LMZ200C.TABLE_NM_OUT)
                            dRow = dt.Rows(0)

                            'value値の設定
                            Dim txtCtl As Win.InputMan.LMImTextBox = Me._G.GetTextControl(frm.ActiveControl.Name)
                            txtCtl.TextValue = dRow.Item("SAGYO_CD").ToString

                            Select Case frm.ActiveControl.Name
                                Case "txtSagyoM1"
                                    frm.lblSagyoM1.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkM1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoM2"
                                    frm.lblSagyoM2.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkM2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoM3"
                                    frm.lblSagyoM3.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkM3.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoM4"
                                    frm.lblSagyoM4.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkM4.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoM5"
                                    frm.lblSagyoM5.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkM5.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtDestSagyoM1"
                                    frm.lblDestSagyoM1.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtDestSagyoRemarkM1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtDestSagyoM2"
                                    frm.lblDestSagyoM2.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtDestSagyoRemarkM2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoL1"
                                    frm.lblSagyoL1.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkL1.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoL2"
                                    frm.lblSagyoL2.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkL2.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoL3"
                                    frm.lblSagyoL3.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkL3.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoL4"
                                    frm.lblSagyoL4.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkL4.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                                Case "txtSagyoL5"
                                    frm.lblSagyoL5.TextValue = dRow.Item("SAGYO_RYAK").ToString
                                    frm.txtSagyoRemarkL5.TextValue = dt.Rows(0).Item("WH_SAGYO_REMARK").ToString
                            End Select

                        End If

                        '2014/01/23 輸出情報追加 START
                    Case frm.txtShipperCd.Name  '届先マスタ
                        'PopUpから取得したデータをコントロールにセット
                        With prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                            frm.txtShipperCd.TextValue = .Item("DEST_CD").ToString()
                            frm.lblShipperNm.TextValue = .Item("DEST_NM").ToString()
                        End With
                        '2014/01/23 輸出情報追加 END

                End Select

        End Select


    End Sub

    '要望対応:1595 yamanaka 2012.11.15 Start
    ''' <summary>
    ''' POPからの戻り値を設定(商品変更)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetPopupGoodsReturn(ByVal frm As LMC020F, ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal prm As LMFormData)

        '戻り値を画面に設定
        Dim prmDs As DataSet = prm.ParamDataSet
        Dim dt As DataTable = prmDs.Tables(LMControlC.LMD100C_TABLE_NM_OUT)

        'スプレッドのクリア
        Me._G.InitSpreadS()

        '戻り値をスプレッドに設定(更新)
        Call Me._G.UpOutkaMspread(frm.sprSyukkaM, dt)

        'スプレッド選択行の詳細（出荷(中)）を表示(更新)
        Call Me._G.UpOutkaM(dt, Me._Ds)

        '検索条件をクリア
        Call Me._G.ClearControlSearch()

        '引当処理を呼ぶ
        Me.ShowHIKIATEG(frm, prm, False, dt, False, eventShubetsu)

        '出荷管理番号(中・小）の採番
        Me._G.SetOutNoDataSet(Me._Ds)

        '出荷データ(中)をDataSetに更新
        Me._G.SetOutMDataSet(Me._Ds, eventShubetsu)

        '出荷データ(小)をDataSetに更新
        Me._G.SetOutSDataSet(Me._Ds, eventShubetsu, Me._NewFlg)

        '作業(中レコード)をDataSetに更新
        Me._G.SetSagyoMDataSet(Me._Ds)

    End Sub
    '要望対応:1595 yamanaka 2012.11.15 End

    'START YANAI 要望番号545
    '''' <summary>
    '''' 在庫引当起動
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Private Sub ShowHIKIATEG(ByVal frm As LMC020F, ByVal prm As LMFormData, ByVal msgFlg As Boolean, ByVal dt As DataTable, ByVal taninusiFlg As Boolean)
    ''' <summary>
    ''' 在庫引当起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowHIKIATEG(ByVal frm As LMC020F, _
                             ByVal prm As LMFormData, _
                             ByVal msgFlg As Boolean, _
                             ByVal dt As DataTable, _
                             ByVal taninusiFlg As Boolean, _
                             ByVal eventShubetsu As LMC020C.EventShubetsu)

        'END YANAI 要望番号545

        Dim ds As DataSet = Nothing

        '2014.09.11 追加START
        Dim addRecflg As String = String.Empty

        If dt Is Nothing = True Then
            If eventShubetsu.Equals(LMC020C.EventShubetsu.COPY_M) = True Then
                addRecflg = "01"
            End If
        ElseIf dt.Rows.Count > 0 Then
            addRecflg = dt.Rows(0).Item("ADD_FLG").ToString()
        End If
        '2014.09.11 追加END

        '確認メッセージ
        Dim rtnResult As MsgBoxResult = Nothing
        If taninusiFlg = False Then
            If msgFlg = False Then
                rtnResult = MsgBoxResult.No
                '要望管理番号2523 20160420 tsunehira add start
            ElseIf frm.optKowake.Checked = True Then
                '小分け出荷は強制的に手動引当
                rtnResult = MsgBoxResult.No
                '要望管理番号2523 20160420 tsunehira add end
            Else
                rtnResult = MyBase.ShowMessage(frm, "C003")
            End If

        Else
            '他荷主処理の時は、強制的に手動引当
            rtnResult = MsgBoxResult.No
        End If

        If rtnResult = MsgBoxResult.Yes Then
            '項目チェック
            If Me._V.IsSingleCheck(LMC020C.EventShubetsu.HIKIATE, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                   Me._V.IsKanrenCheck(LMC020C.EventShubetsu.HIKIATE, Me._Ds) = False OrElse _
                   Me._V.IsSingleWorningCheck(LMC020C.EventShubetsu.HIKIATE, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                   Me._V.IsKanrenWorningCheck(LMC020C.EventShubetsu.HIKIATE, Me._Ds) = False Then
                Exit Sub
            End If

            '自動引当
            ds = Me.ShowHIKIATE(frm, LMC020C.MODE_AUTO_HIKIATE, prm, taninusiFlg)

            '行数設定
            If prm.ReturnFlg = False Then
                '戻り件数が0件の場合はエラー
                Exit Sub
            End If

            'START YANAI 要望番号780
            '削除処理のための選択処理
            Call Me._G.SetCheckBox(frm.sprDtl)

            '削除処理（データセット）
            Call Me._G.DelOutSDataSet(frm.sprDtl, Me._Ds)

            'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
            Call Me._G.InitSpreadS()
            'END YANAI 要望番号780

        ElseIf rtnResult = MsgBoxResult.No Then
            '手動引当
            '2014.09.11 修正START
            'ds = Me.ShowHIKIATE(frm, LMC020C.MODE_HIKIATE, prm, taninusiFlg)
            ds = Me.ShowHIKIATE(frm, LMC020C.MODE_HIKIATE, prm, taninusiFlg, addRecflg)
            '2014.09.11 修正END

            '行数設定
            If prm.ReturnFlg = False Then
                '戻り件数が0件の場合はエラー
                Exit Sub
            End If

        Else
            Exit Sub
        End If

        '戻り値を画面に設定
        Dim outdt As DataTable = ds.Tables(LMControlC.LMC040C_TABLE_NM_OUT)

        'START YANAI 要望番号389
        If 0 < outdt.Rows.Count Then
            If String.IsNullOrEmpty(outdt.Rows(0).Item("GOODS_CD_NRS_FROM").ToString) = False Then
                'GOODS_NRS_CD_FROMに値が設定されている場合は、他荷主モード
                taninusiFlg = True
            End If

            Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, _
                                                                         "' AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, _
                                                                         "' AND SUB_KB = '80'"))
            Dim likeFlg As Boolean = False
#If False Then  'UPD 2020/03/25 011614   【LMS】アクタス_引当て時に商品コード先頭7桁のみで引当て変更
            If custDetailsDr.Length > 0 AndAlso frm.lblEdiOutkaTtlNb.TextValue <> "0" Then

#Else
            If custDetailsDr.Length > 0 AndAlso frm.lblEdiOutkaTtlNb.TextValue <> "0" _
             AndAlso LMC020C.AKUTASU_BRCUST.Equals(String.Concat(frm.cmbEigyosyo.SelectedValue, frm.txtCust_Cd_L.TextValue)) = False Then

#End If
                '戻り値をスプレッドに設定(更新)
                likeFlg = True
                Call Me._G.UpOutkaMspread(frm.sprSyukkaM, outdt, likeFlg)
            End If

        End If
        'END YANAI 要望番号389

        'START YANAI 要望番号545
        'If taninusiFlg = True Then
        '他荷主を選択の場合
        If taninusiFlg = True AndAlso _
            (eventShubetsu).Equals(LMC020C.EventShubetsu.HIKIATE) = False Then
            '他荷主を選択且つ引当ボタン以外を押下(追加(中)または複写(中)を押下)した場合
            'END YANAI 要望番号545

            '戻り値をスプレッドに設定
            Call Me._G.AddOutkaMspread(frm.sprSyukkaM, outdt)

            'スプレッド選択行の詳細（出荷(中)）を表示()
            Call Me._G.AddOutkaM(outdt)

        End If

        'ロット№を設定
        Call Me._G.SetLotNo(outdt)

        'START YANAI 要望番号1019
        ''出荷データ(中)を詳細の個数・数量部分の更新
        'Call Me._G.SetOutkaMhikiate(outdt)
        '出荷データ(中)を詳細の個数・数量部分の更新
        Call Me._G.SetOutkaMhikiate(outdt, eventShubetsu, taninusiFlg)
        'END YANAI 要望番号1019

        '要望番号1955 端数出荷時作業区分対応
        Call Me._G.SetHasuSagyoCd(outdt, eventShubetsu)

        '引当てたデータを在庫データセットに設定
        'START YANAI 要望番号888
        'Call Me._G.SetZaiDataSet_hikiate(frm.sprDtl, outdt, Me._Ds)
        Call Me._G.SetZaiDataSet_hikiate(frm.sprDtl, outdt, Me._Ds, taninusiFlg)
        'END YANAI 要望番号888

        'スプレッド選択行の詳細（出荷(小)）を表示
        Call Me._G.AddOutkaS(frm.sprDtl, outdt, Me._Ds)

        '出荷中の梱包個数の設定
        Call Me._G.OutMkonpoCtl(Me._Ds)

        '引当状況の更新
        Call Me._G.ChangeHikiate(outdt.Rows.Count)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G003")

        '引当残・引当済を求める
        Call Me.SetHikiateKosuSuryo()

        '個数・数量を求める
        Call Me.ChangeKosu()

        'START YANAI 要望番号853 まとめ処理対応
        ''引当可能個数・数量のグループ化
        'Call Me._G.SpreadGroup(Me._Ds)
        'END YANAI 要望番号853 まとめ処理対応

        '検索条件をクリア
        Call Me._G.ClearControlSearch()

        ''画面の入力項目の制御
        'Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 在庫引当起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowHIKIATE(ByVal frm As LMC020F, ByVal sHikiateFlg As String, ByVal prm As LMFormData, _
                                 ByVal taninusiFlg As Boolean, Optional ByVal addRecFlg As String = "") As DataSet

        Dim prmDs As DataSet = New LMC040DS
        Dim row As DataRow = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).NewRow

        Dim dr As DataRow()

        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue
        row("WH_CD") = frm.cmbSoko.SelectedValue
        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
        row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
        row("CUST_NM_L") = frm.lblCust_Nm_L.TextValue
        row("CUST_NM_M") = frm.lblCust_Nm_M.TextValue

        '2014.08.12 修正START
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
                                                                           "NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, "' AND " _
                                                                         , "CUST_CD = '", frm.txtCust_Cd_L.TextValue, "' AND " _
                                                                         , "SUB_KB = '80'"))
#If False Then  'UPD 2020/03/25 011614   【LMS】アクタス_引当て時に商品コード先頭7桁のみで引当て変更
        If dr.Length = 0 Then

#Else
        If dr.Length = 0 _
            OrElse LMC020C.AKUTASU_BRCUST.Equals(String.Concat(frm.cmbEigyosyo.SelectedValue, frm.txtCust_Cd_L.TextValue)) = True Then

#End If

            If String.IsNullOrEmpty(frm.lblGoodsCdNrsFrom.TextValue) = False Then
                row("GOODS_CD_NRS") = frm.lblGoodsCdNrsFrom.TextValue
            Else
                row("GOODS_CD_NRS") = frm.lblGoodsCdNrs.TextValue
            End If

            If taninusiFlg = False Then
                row("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
            Else
                row("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue
            End If
            If taninusiFlg = False Then
                row("GOODS_NM") = frm.lblGoodsNm.TextValue
            Else
                row("GOODS_NM") = frm.txtSerchGoodsNm.TextValue
            End If

        Else
            row("GOODS_CD_NRS") = String.Empty
            If taninusiFlg = False Then
                row("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue.Split(CChar("-"))(0)
            Else
                row("GOODS_CD_CUST") = frm.txtSerchGoodsCd.TextValue.Split(CChar("-"))(0)
            End If
            If taninusiFlg = False Then
                row("GOODS_NM") = String.Empty
            Else
                row("GOODS_NM") = frm.txtSerchGoodsNm.TextValue
            End If
            '2014.09.11 追加START
            row("ADD_FLG") = addRecFlg
            '2014.09.11 追加END

        End If
        '2014.08.12 修正END

        row("SERIAL_NO") = frm.txtSerialNo.TextValue
        row("RSV_NO") = frm.txtRsvNo.TextValue
        If taninusiFlg = False Then
            row("LOT_NO") = frm.txtLotNo.TextValue.ToUpper
        Else
            row("LOT_NO") = frm.txtSerchLot.TextValue.ToUpper
        End If
        row("IRIME") = frm.numIrime.Value
        row("IRIME_UT") = frm.lblIrimeUT.TextValue     'LMC040の画面で区分タイトルラベルを利用する際に変更
        If frm.optCnt.Checked = True Then
            row("ALCTD_KB") = "01"
        ElseIf frm.optAmt.Checked = True Then
            row("ALCTD_KB") = "02"
        ElseIf frm.optKowake.Checked = True Then
            row("ALCTD_KB") = "03"
        ElseIf frm.optSample.Checked = True Then
            row("ALCTD_KB") = "04"
        End If
        row("NB_UT") = frm.lblKonsuUT.TextValue        'LMC040の画面で区分タイトルラベルを利用する際に変更
        row("STD_IRIME_UT") = frm.lblIrimeUT.TextValue 'LMC040の画面で区分タイトルラベルを利用する際に変更
        row("PKG_NB") = frm.numIrisu.Value
        'row("ALCTD_NB") = frm.numHikiateKosuSumi.Value
        'row("BACKLOG_NB") = frm.numHikiateKosuZan.Value
        'row("ALCTD_QT") = frm.numHikiateSuryoSumi.Value
        'row("BACKLOG_QT") = frm.numHikiateSuryoZan.Value
        row("ALCTD_NB") = "0"
        row("BACKLOG_NB") = Convert.ToDecimal(frm.numHikiateKosuZan.Value) + Convert.ToDecimal(frm.numHikiateKosuSumi.Value)
        row("ALCTD_QT") = "0"
        row("BACKLOG_QT") = Convert.ToDecimal(frm.numHikiateSuryoZan.Value) + Convert.ToDecimal(frm.numHikiateSuryoSumi.Value)
        row("KONSU") = frm.numKonsu.Value
        row("HASU") = frm.numHasu.Value
        row("KOSU") = frm.numSouKosu.Value
        row("SURYO") = frm.numSouSuryo.Value

        If String.IsNullOrEmpty(row("LOT_NO").ToString) = False Then
            'ロット№が空の場合のみ、画面の値を設定する
            row("REMARK") = frm.lblRemark.TextValue
        Else
            row("REMARK") = String.Empty
        End If

        row("REMARK_OUT") = frm.lblRemarkOut.TextValue
        row("HIKIATE_ALERT_YN") = frm.lblHikiateAlertYn.TextValue
        row("TAX_KB") = frm.lblTaxKb.TextValue
        row("DEST_NM") = frm.txtTodokesakiNm.TextValue

        row("HIKIATE_FLG") = sHikiateFlg

        If taninusiFlg = True Then
            row("TANINUSI_FLG") = "01"
        Else
            'START YANAI メモ②No.27
            'row("TANINUSI_FLG") = "00"
            If String.IsNullOrEmpty(frm.lblGoodsCdNrsFrom.TextValue) = False Then
                row("TANINUSI_FLG") = "01"
            Else
                row("TANINUSI_FLG") = "00"
            End If
            'END YANAI メモ②No.27
        End If

        'START YANAI No.4
        '要望番号:1592 terakawa 2012.11.16 Start
        'If (LMC020C.MODE_AUTO_HIKIATE).Equals(sHikiateFlg) = True Then
        '要望番号:1592 terakawa 2012.11.16 End
        '要望番号:1253 terakawa 2012.07.13 Start
        'Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", frm.txtCust_Cd_L.TextValue, "' AND SUB_KB = '02'"))
        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, _
                                                                                                                         "' AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, "' AND SUB_KB = '02'"))
        '要望番号:1253 terakawa 2012.07.13 End
        If 0 < custDetailsDr.Length Then
            row("SORT_FLG") = custDetailsDr(0).Item("SET_NAIYO")

        Else
            row("SORT_FLG") = "00"
        End If
        '要望番号:1592 terakawa 2012.11.16 Start
        'Else
        'row("SORT_FLG") = "00"
        'End If
        '要望番号:1592 terakawa 2012.11.16 End
        'END YANAI No.4

        'START YANAI 20111003 一括引当対応
        row("OUTKA_PLAN_DATE") = frm.imdSyukkaYoteiDate.TextValue
        'END YANAI 20111003 一括引当対応

        'START YANAI 要望番号507
        row("OUTKA_S_CNT") = frm.sprDtl.ActiveSheet.Rows.Count
        'END YANAI 要望番号507

        'START YANAI 要望番号547
        row("PGID") = MyBase.GetPGID()
        'END YANAI 要望番号547

        'START 要望番号008131
#If False Then  'UPD 2021/09/06 023522   【LMS】安田倉庫移転_PG改修点洗い出し_改修(営業荻山) 
        If ("96".Equals(frm.cmbEigyosyo.SelectedValue) OrElse "98".Equals(frm.cmbEigyosyo.SelectedValue)) _
                And "00135".Equals(frm.txtCust_Cd_L.TextValue) _
                And LMC020C.MODE_AUTO_HIKIATE.Equals(sHikiateFlg) Then

#Else
        'UPD 2022/10/19 033380   【LMS】FFEM足柄工場LMS導入 F2追加, 2023/11/28 039659 F3 追加
        If ("96".Equals(frm.cmbEigyosyo.SelectedValue) OrElse "98".Equals(frm.cmbEigyosyo.SelectedValue) OrElse "F1".Equals(frm.cmbEigyosyo.SelectedValue) OrElse "F2".Equals(frm.cmbEigyosyo.SelectedValue) OrElse "F3".Equals(frm.cmbEigyosyo.SelectedValue)) _
                And "00135".Equals(frm.txtCust_Cd_L.TextValue) _
                And LMC020C.MODE_AUTO_HIKIATE.Equals(sHikiateFlg) Then

#End If

            'ADD START 要望番号009476
            Dim filter As String = "KBN_GROUP_CD = 'F030'" _
                                     & " AND KBN_NM4 = '" & frm.cmbEigyosyo.SelectedValue.ToString & "'" _
                                     & " AND KBN_NM5 = '" & frm.cmbSoko.SelectedValue.ToString & "'" _
                                     & " AND KBN_NM6 = '1'"

            Dim drsF030 As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            If drsF030.Length > 0 Then
                '在庫ステータス更新対象の倉庫の場合
                'ADD END 要望番号009476
                row("GOODS_COND_KB_3") = "00"
            End If  'ADD 要望番号009476
        End If
        'END 要望番号008131
#If True Then   'ADD 2020/05/22007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
        '引当対象設定
        Dim brCd As String = frm.cmbEigyosyo.SelectedValue.ToString
        Dim custCd As String = frm.txtCust_Cd_L.TextValue.ToString
        Dim JJ_FLG As String = LMConst.FLG.OFF
        Dim JJ_HIKIATE As String = String.Empty
        Dim drjj As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C038' AND KBN_NM1 = '", brCd, "' And KBN_NM2 = '", custCd, "'"))
        If 0 < drjj.Length Then
            JJ_FLG = LMConst.FLG.ON
            JJ_HIKIATE = drjj(0).Item("KBN_NM4").ToString.Trim
        End If
        row("JJ_FLG") = JJ_FLG
        row("JJ_HIKIATE") = JJ_HIKIATE
#End If
        prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows.Add(row)

        '在庫データの情報をセット
        Dim zaiDr() As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_ZAI).Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = zaiDr.Length - 1
        For i As Integer = 0 To max
            row = prmDs.Tables(LMC040C.TABLE_NM_ZAI).NewRow
            row("ZAI_REC_NO") = zaiDr(i).Item("ZAI_REC_NO")
            row("PORA_ZAI_NB") = zaiDr(i).Item("PORA_ZAI_NB")
            row("ALCTD_NB") = zaiDr(i).Item("ALCTD_NB_GAMEN")
            'START YANAI 要望番号853 まとめ処理対応
            'row("ALLOC_CAN_NB") = zaiDr(i).Item("ALLOC_CAN_NB_GAMEN")
            row("ALLOC_CAN_NB") = zaiDr(i).Item("ALLOC_CAN_NB")
            'END YANAI 要望番号853 まとめ処理対応
            row("PORA_ZAI_QT") = zaiDr(i).Item("PORA_ZAI_QT")
            row("ALCTD_QT") = zaiDr(i).Item("ALCTD_QT_GAMEN")
            'START YANAI 要望番号853 まとめ処理対応
            'row("ALLOC_CAN_QT") = zaiDr(i).Item("ALLOC_CAN_QT_GAMEN")
            row("ALLOC_CAN_QT") = zaiDr(i).Item("ALLOC_CAN_QT")
            'END YANAI 要望番号853 まとめ処理対応
            prmDs.Tables(LMC040C.TABLE_NM_ZAI).Rows.Add(row)
        Next

        '20170623 在庫不整合修正Add
        Dim outS() As DataRow = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("SYS_DEL_FLG = '0' AND OUTKA_NO_M = '" & frm.lblSyukkaMNo.TextValue & "'")
        Dim maxOutS As Integer = outS.Length - 1
        For i As Integer = 0 To maxOutS
            row = prmDs.Tables(LMC040C.TABLE_NM_OUT_S).NewRow
            row("NRS_BR_CD") = outS(i).Item("NRS_BR_CD")
            row("OUTKA_NO_L") = outS(i).Item("OUTKA_NO_L")
            row("OUTKA_NO_M") = outS(i).Item("OUTKA_NO_M")
            row("OUTKA_NO_S") = outS(i).Item("OUTKA_NO_S")
            row("TOU_NO") = outS(i).Item("TOU_NO")
            row("SITU_NO") = outS(i).Item("SITU_NO")
            row("ZONE_CD") = outS(i).Item("ZONE_CD")
            row("LOCA") = outS(i).Item("LOCA")
            row("LOT_NO") = outS(i).Item("LOT_NO")
            row("SERIAL_NO") = outS(i).Item("SERIAL_NO")
            row("OUTKA_TTL_NB") = outS(i).Item("OUTKA_TTL_NB")
            row("OUTKA_TTL_QT") = outS(i).Item("OUTKA_TTL_QT")
            row("ZAI_REC_NO") = outS(i).Item("ZAI_REC_NO")
            row("INKA_NO_L") = outS(i).Item("INKA_NO_L")
            row("INKA_NO_M") = outS(i).Item("INKA_NO_M")
            row("INKA_NO_S") = outS(i).Item("INKA_NO_S")
            row("ZAI_UPD_FLAG") = outS(i).Item("ZAI_UPD_FLAG")
            row("ALCTD_CAN_NB") = outS(i).Item("ALCTD_CAN_NB")
            row("ALCTD_NB") = outS(i).Item("ALCTD_NB")
            row("ALCTD_CAN_QT") = outS(i).Item("ALCTD_CAN_QT")
            row("ALCTD_QT") = outS(i).Item("ALCTD_QT")
            row("IRIME") = outS(i).Item("IRIME")
            row("BETU_WT") = outS(i).Item("BETU_WT")
            row("COA_FLAG") = outS(i).Item("COA_FLAG")
            row("REMARK") = outS(i).Item("REMARK")
            row("SMPL_FLAG") = outS(i).Item("SMPL_FLAG")
            row("REC_NO") = outS(i).Item("REC_NO")
            row("ALCTD_NB_GAMEN") = outS(i).Item("ALCTD_NB_GAMEN")
            row("ALCTD_QT_GAMEN") = outS(i).Item("ALCTD_QT_GAMEN")
            row("ALCTD_CAN_NB_GAMEN") = outS(i).Item("ALCTD_CAN_NB_GAMEN")
            row("ALCTD_CAN_QT_GAMEN") = outS(i).Item("ALCTD_CAN_QT_GAMEN")
            row("ALCTD_NB_MATOME") = outS(i).Item("ALCTD_NB_MATOME")
            row("ALCTD_QT_MATOME") = outS(i).Item("ALCTD_QT_MATOME")
            row("ALCTD_CAN_NB_MATOME") = outS(i).Item("ALCTD_CAN_NB_MATOME")
            row("ALCTD_CAN_QT_MATOME") = outS(i).Item("ALCTD_CAN_QT_MATOME")
            row("OUTKA_NO_L2") = outS(i).Item("OUTKA_NO_L2")
            row("N_ZAI_REC_NO") = outS(i).Item("N_ZAI_REC_NO")
            row("O_ZAI_REC_NO") = outS(i).Item("O_ZAI_REC_NO")
            row("SYS_UPD_DATE") = outS(i).Item("SYS_UPD_DATE")
            row("SYS_UPD_TIME") = outS(i).Item("SYS_UPD_TIME")
            row("SYS_DEL_FLG") = outS(i).Item("SYS_DEL_FLG")
            row("UP_KBN") = outS(i).Item("UP_KBN")
            row("KOWAKE_IRIME") = outS(i).Item("KOWAKE_IRIME")
            row("MATOME_FLG") = outS(i).Item("MATOME_FLG")
            row("SMPL_FLG_ZAI") = outS(i).Item("SMPL_FLG_ZAI")
            prmDs.Tables(LMC040C.TABLE_NM_OUT_S).Rows.Add(row)
        Next
        '20170623 在庫不整合修正End

        prm.ParamDataSet = prmDs

        '在庫引当呼出
        LMFormNavigate.NextFormNavigate(Me, "LMC040", prm)

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' 届先マスタメンテ起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowTodokeMst(ByVal frm As LMC020F, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMM040DS
        Dim row As DataRow = prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue
        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
        row("DEST_CD") = frm.txtTodokesakiCd.TextValue

        'START YANAI 要望番号1217 出荷編集・出荷EDI編集と、届け先マスタの連携
        row("DEST_NM") = frm.txtTodokesakiNm.TextValue
        row("EDI_CD") = String.Empty
        row("ZIP") = String.Empty
        row("AD_1") = frm.txtTodokeAdderss1.TextValue
        row("AD_2") = frm.txtTodokeAdderss2.TextValue
        row("AD_3") = frm.txtTodokeAdderss3.TextValue
        row("SP_NHS_KB") = frm.cmbSiteinouhin.SelectedValue
        row("COA_YN") = frm.cmbBunsakiTmp.SelectedValue
        row("DELI_ATT") = frm.txtHaisoRemark.TextValue
        row("TEL") = frm.txtTodokeTel.TextValue
        row("FAX") = String.Empty
        row("JIS") = String.Empty
        row("PICK_KB") = frm.cmbPick.SelectedValue
        row("REMARK") = String.Empty
        row("URIAGE_CD") = frm.txtUriCd.TextValue
        'END YANAI 要望番号1217 出荷編集・出荷EDI編集と、届け先マスタの連携

        row("MODE_FLG") = LMConst.FLG.ON

        prmDs.Tables(LMControlC.LMM040C_TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        '在庫引当呼出
        LMFormNavigate.NextFormNavigate(Me, "LMM040", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            With prm.ParamDataSet.Tables("LMM040INOUT").Rows(0)

                '届先マスタメンテから取得したデータをコントロールにセット
                frm.txtTodokesakiCd.TextValue = .Item("DEST_CD").ToString()
                'START YANAI 要望番号909
                frm.txtTodokesakiCdOld.TextValue = .Item("DEST_CD").ToString()
                'END YANAI 要望番号909
                frm.txtTodokesakiNm.TextValue = .Item("DEST_NM").ToString()
                frm.txtTodokeAdderss1.TextValue = .Item("AD_1").ToString()
                frm.txtTodokeAdderss2.TextValue = .Item("AD_2").ToString()
                frm.txtTodokeAdderss3.TextValue = .Item("AD_3").ToString()
                frm.txtTodokeTel.TextValue = .Item("TEL").ToString()
                'START YANAI 要望番号909
                'frm.txtHaisoRemark.TextValue = .Item("DELI_ATT").ToString
                frm.txtHaisoRemarkNew.TextValue = .Item("DELI_ATT").ToString
                'END YANAI 要望番号909
                frm.cmbSiteinouhin.SelectedValue = .Item("SP_NHS_KB").ToString
                frm.numKyori.TextValue = .Item("KYORI").ToString
                frm.cmbBunsakiTmp.SelectedValue = .Item("COA_YN").ToString

                '要望番号:1605 yamanaka 2012.11.21 Start
                'START YANAI 要望番号745
                If String.IsNullOrEmpty(.Item("URIAGE_CD").ToString()) = False Then
                    frm.txtUriCd.TextValue = .Item("URIAGE_CD").ToString

                    '---↓
                    'Dim destDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", frm.txtCust_Cd_L.TextValue, "' AND DEST_CD = '", .Item("URIAGE_CD").ToString(), "'"))

                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
                    destMstDr.Item("DEST_CD") = .Item("URIAGE_CD").ToString()
                    destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    Dim destDr() As DataRow = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                    '---↑

                    If 0 < destDr.Length Then
                        frm.lblUriNm.TextValue = destDr(0).Item("DEST_NM").ToString()
                    Else
                        frm.lblUriNm.TextValue = String.Empty
                    End If

                Else
                    frm.txtUriCd.TextValue = String.Empty
                    frm.lblUriNm.TextValue = String.Empty

                End If
                'END YANAI 要望番号745
                '要望番号:1605 yamanaka 2012.11.21 End

                'START YANAI 要望番号880
                Call Me._G.GetTariffSet(frm)
                'END YANAI 要望番号880

                'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
                '要望番号:1571 yamanaka 2012.11.21 Start
                'frm.txtUnsoCompanyCd.TextValue = .Item("SP_UNSO_CD").ToString
                'frm.txtUnsoSitenCd.TextValue = .Item("SP_UNSO_BR_CD").ToString
                '要望番号:1571 yamanaka 2012.11.21 End

                '運送会社コードOLD設定
                Me._G.SetUnsoCdOld(frm)

                '運賃タリフセットからタリフコードを設定
                Call Me._G.GetUnchinTariffSet(frm, False)
                'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

            End With

            '運送会社設定
            Me._G.GetUnsoCompanyChangeDest(frm)

        End If

    End Sub

    ''' <summary>
    ''' LMS文書管理(LMU010)起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowBunshoKanri(ByVal frm As LMC020F, ByVal prm As LMFormData)

        ''LMS文書管理(LMU010)画面の処理表示用データ設定
        Dim prmDs As DataSet = New LMU010DS
        Dim row As DataRow = prmDs.Tables(LMControlC.LMU010C_TABLE_NM_IN).NewRow
        row("ENT_SYSID_KBN") = "06"
        row("KEY_TYPE_KBN") = "14"
        row("KEY_NO") = frm.lblSyukkaLNo.TextValue

        prmDs.Tables(LMControlC.LMU010C_TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'LMS文書管理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMU010", prm)

    End Sub

    ''' <summary>
    ''' 在庫履歴照会起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShowZaiRireki(ByVal frm As LMC020F, ByVal prm As LMFormData)

        '在庫履歴照会(LMD040)画面の処理表示用データ設定
        Dim prmDs As DataSet = New LMD040DS
        Dim row As DataRow = prmDs.Tables(LMControlC.LMD040C_TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue
        row("WH_CD") = frm.cmbSoko.SelectedValue
        row("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
        row("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
        row("GOODS_CD_CUST") = frm.txtGoodsCdCust.TextValue
        row("GOODS_NM") = frm.lblGoodsNm.TextValue
        row("LOT_NO") = frm.txtLotNo.TextValue

        prmDs.Tables(LMControlC.LMD040C_TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        '在庫引当呼出
        LMFormNavigate.NextFormNavigate(Me, "LMD040", prm)

    End Sub

    ''' <summary>
    ''' 届け先キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedDest(ByVal frm As LMC020F) As String

        With frm

            Dim dr As DataRow() = Nothing

            '届先名称
            '---↓
            'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat( _
            '                                                                   "NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND " _
            '                                                                 , "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
            '                                                                 , "DEST_CD = '", .txtTodokesakiCd.TextValue, "' AND " _
            '                                                                 , "SYS_DEL_FLG = '0'"))

            If String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = True Then
                Return String.Empty
            End If

            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
            destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
            destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            destMstDr.Item("DEST_CD") = .txtTodokesakiCd.TextValue
            destMstDr.Item("SYS_DEL_FLG") = "0"
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            dr = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            If 0 < dr.Length Then
                Return dr(0).Item("DEST_NM").ToString
            Else
            End If

            Return String.Empty

        End With

    End Function

#End Region 'PopUp

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 引当残・引当済を求める
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetHikiateKosuSuryo() As Boolean

        '引当個数・数量を求める
        If Me._G.GetHikiKosuSuryo(Me._Ds) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 個数・数量を求める（梱数・端数変更時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ChangeKosu() As Boolean

        '個数・数量を求める
        If Me._G.GetKosuSuryo(LMConst.FLG.ON) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 個数・数量を求める（数量変更時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ChangeSuryo() As Boolean

        '個数・数量を求める
        If Me._G.GetKosuSuryo(LMConst.FLG.OFF) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 個数・数量を求める（ボタン押下時）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ChangeKosuSuryo(ByVal frm As LMC020F) As Boolean

        If ("numKonsu").Equals(frm.ActiveControl.Name) = True OrElse _
            ("numHasu").Equals(frm.ActiveControl.Name) = True Then

            '個数・数量を求める
            If Me._G.GetKosuSuryo(LMConst.FLG.ON) = False Then
                Return False
            End If

        ElseIf ("numSouSuryo").Equals(frm.ActiveControl.Name) = True Then

            '個数・数量を求める
            If Me._G.GetKosuSuryo(LMConst.FLG.OFF) = False Then
                Return False
            End If

        End If

        Return True

    End Function
    ''' <summary>
    ''' 出荷(中)単位の運送重量を求める
    ''' </summary>
    ''' <param name="outMdr">対象の出荷（中）データ</param>
    ''' <remarks></remarks>
    Friend Function GetUnsoWTM(ByVal outMdr As DataRow _
                             , ByVal frm As LMC020F _
                             , Optional ByVal irimeM As String = "" _
                             , Optional ByVal goodsCdNrsM As String = "" _
                             , Optional ByVal kosuM As String = "" _
                             , Optional ByVal tareYnM As String = "" _
                             , Optional ByVal stdIrimeNbM As String = "" _
                             , Optional ByVal stdWtKgsM As String = "" _
                             , Optional ByVal suryoM As String = "") As String

        Dim irime As String = String.Empty
        Dim goodsCdNrs As String = String.Empty
        Dim kosu As String = String.Empty
        Dim tareYn As String = String.Empty
        Dim stdIrimeNb As String = String.Empty
        Dim stdWtKgs As String = String.Empty
        '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
        Dim suryo As String = String.Empty
        '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

        If String.IsNullOrEmpty(irimeM) = True Then
            irime = outMdr.Item("IRIME").ToString
            goodsCdNrs = outMdr.Item("GOODS_CD_NRS").ToString
            kosu = outMdr.Item("OUTKA_TTL_NB").ToString
            tareYn = outMdr.Item("TARE_YN").ToString
            stdIrimeNb = outMdr.Item("STD_IRIME_NB").ToString
            stdWtKgs = outMdr.Item("STD_WT_KGS").ToString
            '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
            suryo = (outMdr.Item("OUTKA_TTL_QT").ToString)
            '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

        Else
            irime = irimeM
            goodsCdNrs = goodsCdNrsM
            kosu = kosuM
            tareYn = tareYnM
            stdIrimeNb = stdIrimeNbM
            stdWtKgs = stdWtKgsM
            '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
            suryo = suryoM
            '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

        End If

        Dim goodsWT As Decimal = 0
        If (LMC020C.PLUS_ZERO).Equals(irime) = True OrElse _
           (LMC020C.MINUS_ZERO).Equals(irime) = True Then
            Return Convert.ToString(goodsWT)
        End If

        '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
        '数量＜標準入目ならば、小分けとみなす
        If Convert.ToDecimal(suryo) < Convert.ToDecimal(stdIrimeNb) Then
            '[小分け出荷] 商品１つあたりの重量 = （商品）標準重量 * 数量 / （商品）標準入目
            goodsWT = Convert.ToDecimal(stdWtKgs) * Convert.ToDecimal(suryo) / Convert.ToDecimal(stdIrimeNb)
        Else
            '[通常出荷用] 商品１つあたりの重量 = （商品）標準重量 * 入目 / （商品）標準入目
            goodsWT = Convert.ToDecimal(stdWtKgs) * Convert.ToDecimal(irime) / Convert.ToDecimal(stdIrimeNb)
        End If

        ''商品１つあたりの重量 = （商品）標準重量 * 入目 / （商品）標準入目
        'goodsWT = Convert.ToDecimal(stdWtKgs) * Convert.ToDecimal(irime) / Convert.ToDecimal(stdIrimeNb)

        '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

        '風袋加算処理（商品１つあたりの重量に加算）
        If ("01").Equals(tareYn) = True Then
            Dim futaiDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'N001' AND ", _
                                                                                                              "KBN_CD ='", outMdr.Item("PKG_UT").ToString, "'"))
            goodsWT = goodsWT + Convert.ToDecimal(futaiDr(0).Item("VALUE1").ToString)
        End If

        '(2012.12.06)要望番号1649 小分け時の運送重量対応 --- START ---
        '数量＜標準入目ならば、小分けとみなす
        If Convert.ToDecimal(suryo) < Convert.ToDecimal(stdIrimeNb) Then
            '小分け時は何もしない
        Else
            '商品単位の重量 = 商品１つあたりの重量 * 個数
            goodsWT = goodsWT * Convert.ToDecimal(kosu)
        End If

        ''商品単位の重量 = 商品１つあたりの重量 * 個数
        'goodsWT = goodsWT * Convert.ToDecimal(kosu)

        '(2012.12.06)要望番号1649 小分け時の運送重量対応 ---  END  ---

        Return Convert.ToString(goodsWT)

    End Function

    ''' <summary>
    ''' データセット再設定処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function datasetSetting(ByVal ds As DataSet) As DataSet

        '出荷(大)
        Me.datasetClear(ds, LMC020C.TABLE_NM_OUT_L)

        '出荷(中)
        Me.datasetClear(ds, LMC020C.TABLE_NM_OUT_M)

        '出荷(小)
        Me.datasetClear(ds, LMC020C.TABLE_NM_OUT_S)

        '作業
        Me.datasetClear(ds, LMC020C.TABLE_NM_SAGYO)

        '運送(大)
        Me.datasetClear(ds, LMC020C.TABLE_NM_UNSO_L)

        '運送(中)
        Me.datasetClear(ds, LMC020C.TABLE_NM_UNSO_M)

        '在庫データ
        Me.datasetClear(ds, LMC020C.TABLE_NM_ZAI)
        Me.datasetZaiClear(ds, LMC020C.TABLE_NM_ZAI)

        '2015.07.08 協立化学　シッピング対応 追加START
        'マーク(HED)
        Me.datasetClear(ds, LMC020C.TABLE_NM_MARK_HED)
        '2015.07.08 協立化学　シッピング対応 追加END

        '2015.07.21 協立化学　シッピング対応 追加START
        'マーク(HED)
        Me.datasetClear(ds, LMC020C.TABLE_NM_MARK_DTL)
        '2015.07.21 協立化学　シッピング対応 追加END

        Return ds

    End Function

    ''' <summary>
    ''' データセット再設定処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function datasetClear(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim maxCnt As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            If LMConst.FLG.OFF.Equals(dr.Item("SYS_DEL_FLG")) = True Then
                '削除でないデータは更新区分を検索時のデフォルト値である"1"に設定
                dr.Item("UP_KBN") = "1"

            Else
                ds.Tables(tblNm).Rows.Remove(dr)
                i = i - 1
                maxCnt = maxCnt - 1
            End If

            If i = maxCnt = True Then
                Exit For
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 在庫データセット再設定処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Friend Function datasetZaiClear(ByVal ds As DataSet, ByVal tblNm As String) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            dr.Item("ALCTD_KB_FLG") = "99"
            'START YANAI 要望番号979
            dr.Item("ALCTD_NB_HOZON") = dr.Item("ALCTD_NB")
            dr.Item("ALCTD_QT_HOZON") = dr.Item("ALCTD_QT")
            dr.Item("ALLOC_CAN_NB_HOZON") = dr.Item("ALLOC_CAN_NB")
            dr.Item("ALLOC_CAN_QT_HOZON") = dr.Item("ALLOC_CAN_QT")
            'END YANAI 要望番号979

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 商品情報明細のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetEndScrollSyukkaM(ByVal frm As LMC020F)

        Call Me.SetEndScroll(frm.sprSyukkaM, True)

    End Sub

    ''' <summary>
    ''' 商品情報明細のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetEndScrollDtl(ByVal frm As LMC020F)

        Call Me.SetEndScroll(frm.sprDtl, True)

    End Sub

    ''' <summary>
    ''' 明細のスクロールバーを最終行に設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="setFlg">アクティブセルを設定するかのフラグ</param>
    ''' <remarks></remarks>
    Private Sub SetEndScroll(ByVal spr As Win.Spread.LMSpread, ByVal setFlg As Boolean)

        With spr

            Dim maxRow As Integer = .ActiveSheet.Rows.Count - 1
            If maxRow < 0 Then
                Exit Sub
            End If

            spr.SetViewportTopRow(0, maxRow)

            If setFlg = True Then

                spr.ActiveSheet.SetActiveCell(maxRow, 0)

            End If

        End With

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMC020F, ByVal e As FormClosingEventArgs) As Boolean

        '編集モード以外なら処理終了
        '全てのファンクションキーが無効になっている場合は、更新中でも閉じる。
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False OrElse AllFKeyOffChk(frm) Then
            Exit Function
        End If

        If Not frm.FunctionKey.F11ButtonEnabled Then
            ' 保存ボタンが無効の場合
            ' 処理終了
            Exit Function
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                '画面色リセット用
                Me._LMCconH.ColorReset(frm)

                'START YANAI メモ②No.7
                '個数・数量を求める
                Call Me.ChangeKosu()

                '2018/12/07 ADD START 要望管理002171
                Dim bErrCalcOutkaPkgNb As Boolean = False
                '出荷梱包個数自動計算用データテーブル初期化
                Call Me.InitCalcOutkaPkgNbIn(frm)
                '2018/12/07 ADD END   要望管理002171

                '出荷大の梱包個数の設定
                If Me._G.OutLkonpoCtl(Me._Ds) = False Then
                    Exit Function
                End If
                'END YANAI メモ②No.7

                '項目チェック
                If Me._V.IsSingleCheck(LMC020C.EventShubetsu.HOZON, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenCheck(LMC020C.EventShubetsu.HOZON, Me._Ds) = False OrElse _
                       Me._V.IsSingleWorningCheck(LMC020C.EventShubetsu.HOZON, Me._Ds, Me.SysData(LMC020C.SysData.YYYYMMDD)) = False OrElse _
                       Me._V.IsKanrenWorningCheck(LMC020C.EventShubetsu.HOZON, Me._Ds) = False Then
                    e.Cancel = True
                    Exit Function
                Else
                    ''請求鑑ヘッダチェック
                    'If Me.CheckKagamiData(frm, LMC020C.EventShubetsu.HOZON, "保存") = False Then
                    '    e.Cancel = True
                    '    Exit Function
                    'End If
                End If

                'START YANAI メモ②No.7
                '処理開始アクション
                Me._LMCconH.StartAction(frm)
                'END YANAI メモ②No.7

                'データセット設定
                Call Me.SaveDataset(frm, Me._Ds)

                '現場指示ステータスを設定
                Me.SetWhhSijiStatus(frm)

                '引当在庫数のチェック処理
                Dim chkFlg As Boolean = Me._V.IsZaikoCheck(Me._Ds)
                If chkFlg = False Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    e.Cancel = True
                    Me._LMCconH.EndAction(frm)
                    Exit Function
                End If

                ' 保存時、分納かつ作業進捗が「予定入力済」のままの場合「検品済」に変更するか否かの確認
                ' (保存後、出荷完了を行い次回分納データの作成を行いたいか否かの確認)
                If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
                    If Not ChkBunnouOutkaStateKb(frm, Me._Ds) Then
                        ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                        Me._G.ClearExportLSet(Me._Ds)

                        e.Cancel = True
                        Me._LMCconH.EndAction(frm)
                        Exit Function
                    End If
                End If

                '出荷梱包個数自動計算の実行確認
                Call Me.ConfirmAutoCalcOutkaPkgNb(frm)  '2018/12/07 ADD 要望管理002171

                'START YANAI メモ②No.7
                ''処理開始アクション
                'Me._LMCconH.StartAction(frm)
                'END YANAI メモ②No.7

                ' 分納の場合の出荷M 個数・数量項目再設定
                If frm.cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
                    SetBunnouNbQtOutkaM(frm, Me._Ds)
                    ' 分納の場合の出荷L 出荷梱包個数 再設定
                    SetBunnouNbQtOutkaL(frm, Me._Ds)
                End If

                '印刷処理
                Me._Ds.Merge(New RdPrevInfoDS)
                Me._Ds.Tables(LMConst.RD).Clear()

                '保存処理呼び出し
                Dim rtnDs As DataSet = Me.SaveControl(frm)

                '2018/12/07 ADD START 要望管理002171
                '出荷梱包個数自動計算のエラーチェック
                bErrCalcOutkaPkgNb = IsErrCalcOutkaPkgNb(rtnDs)

                '出荷梱包個数自動計算のフラグクリア(保存処理以外で自動計算されないようにするため)
                Me._Ds.Tables(LMC020C.TABLE_NM_CALC_OUTKA_PKG_NB_IN).Clear()
                '2018/12/07 ADD END   要望管理002171

                'エラーがある場合、終了
                If MyBase.IsMessageExist() = True Then
                    ' 輸出情報 DataTable の初回設定が“新規”の場合の初期化
                    Me._G.ClearExportLSet(Me._Ds)

                    'メッセージ表示
                    MyBase.ShowMessage(frm)

                    '処理終了アクション 
                    Me._LMCconH.EndAction(frm)

                    e.Cancel = True
                    Exit Function

                End If

                '2018/12/07 ADD START 要望管理002171
                If bErrCalcOutkaPkgNb Then
                    '出荷梱包個数自動計算がエラーの場合
                    Dim msg As String = " 商品明細マスタの設定に誤りがあるため出荷梱包個数を自動計算できませんでした。"
                    'メッセージの表示
                    MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName.Replace("　", String.Empty), msg})

                    e.Cancel = True
                    Exit Function
                End If
                '2018/12/07 ADD END   要望管理002171

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey1Press")

        If frm.cmbJikkou.SelectedValue.ToString() = "04" Then
            '「分納出荷」を選択の場合はクリアする。
            '「分納出荷」選択→実行処理 よりの「編集」処理の場合のみ「分納出荷」で固定とするため

            frm.cmbJikkou.SelectedValue = ""
        End If

        '「新規」処理
        Me.ActionControl(LMC020C.EventShubetsu.SINKI, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey2Press")

        If frm.cmbJikkou.SelectedValue.ToString() = "04" Then
            '「分納出荷」を選択の場合はクリアする。
            '「分納出荷」選択→実行処理 よりの「編集」処理の場合のみ「分納出荷」で固定とするため

            frm.cmbJikkou.SelectedValue = ""
        End If

        '「編集」処理
        Me.ActionControl(LMC020C.EventShubetsu.HENSHU, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey3Press")

        If frm.cmbJikkou.SelectedValue.ToString() = "04" Then
            '「分納出荷」を選択の場合はクリアする。
            '「分納出荷」選択→実行処理 よりの「編集」処理の場合のみ「分納出荷」で固定とするため

            frm.cmbJikkou.SelectedValue = ""
        End If

        '「複写」処理
        Me.ActionControl(LMC020C.EventShubetsu.FUKUSHA, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey4Press")

        '「削除」処理
        Me.ActionControl(LMC020C.EventShubetsu.DEL, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey4Press")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey5Press")

        '「引当」処理
        Me.ActionControl(LMC020C.EventShubetsu.HIKIATE, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey5Press")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey6Press")

        '「完了取消」処理
        Me.ActionControl(LMC020C.EventShubetsu.TORIKESHI, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey6Press")


    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey7Press")

        '「運送修正」処理
        Me.ActionControl(LMC020C.EventShubetsu.UNSO, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey7Press")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey8Press")

        '「終算日修正」処理
        Me.ActionControl(LMC020C.EventShubetsu.SHUSAN, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey8Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey9Press")

        '「追加(中)」処理
        Me.ActionControl(LMC020C.EventShubetsu.INS_M, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        '「マスタ参照」処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey11Press")

        '「保存」処理
        Me.ActionControl(LMC020C.EventShubetsu.HOZON, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMC020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' 実行押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnJIKKOU_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnJIKKOU_Click")

        '「実行」処理
        Me.ActionControl(LMC020C.EventShubetsu.JIKKOU, frm)

        Logger.EndLog(Me.GetType.Name, "btnJIKKOU_Click")

    End Sub

    ''' <summary>
    ''' 印刷押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnPRINT_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnPRINT_Click")

        '「印刷」処理
        Me.ActionControl(LMC020C.EventShubetsu.PRINT, frm)

        Logger.EndLog(Me.GetType.Name, "btnPRINT_Click")

    End Sub

    ''' <summary>
    ''' 新規(届先)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnNew_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnNew_Click")

        '「新規(届先)」処理
        Me.ActionControl(LMC020C.EventShubetsu.TODOKESAKI, frm)

        Logger.EndLog(Me.GetType.Name, "btnNew_Click")

    End Sub

    ''' <summary>
    ''' 履歴照会押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnRireki_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnRireki_Click")

        '「新規(届先)」処理
        Me.ActionControl(LMC020C.EventShubetsu.RIREKI, frm)

        Logger.EndLog(Me.GetType.Name, "btnRireki_Click")

    End Sub

    ''' <summary>
    ''' 追加(中)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_INS_M_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnROW_INS_M_Click")

        '「追加(中)」処理
        Me.ActionControl(LMC020C.EventShubetsu.INS_M, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_INS_M_Click")

    End Sub

    ''' <summary>
    ''' 複写(中)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_COPY_M_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnROW_COPY_M_Click")

        '「複写(中)」処理
        Me.ActionControl(LMC020C.EventShubetsu.COPY_M, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_COPY_M_Click")

    End Sub

    ''' <summary>
    ''' 削除(中)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_M_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_M_Click")

        '「削除(中)」処理
        Me.ActionControl(LMC020C.EventShubetsu.DEL_M, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_M_Click")

    End Sub

    ''' <summary>
    ''' 削除(小)押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnROW_DEL_S_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnROW_DEL_S_Click")

        '「削除(小)」処理
        Me.ActionControl(LMC020C.EventShubetsu.DEL_S, frm)

        Logger.EndLog(Me.GetType.Name, "btnROW_DEL_S_Click")

    End Sub

    ''' <summary>
    ''' 一括変更押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnHenko_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnHenko_Click")

        '「一括変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.HENKO, frm)

        Logger.EndLog(Me.GetType.Name, "btnHenko_Click")

    End Sub

    ''' <summary>
    ''' 商品変更ボタン押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub btnChangeGoods_Click(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "btnChangeGoods_Click")

        '「商品変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.CHANGE_GOODS, frm)

        Logger.EndLog(Me.GetType.Name, "btnChangeGoods_Click")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMC020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprSyukkaM_CellDoubleClick(ByRef frm As LMC020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.DOUBLE_CLICK, frm)

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' SPREADのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprSyukkaM_LeaveCell(ByVal frm As LMC020F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprSyukkaM_LeaveCell")

        If e.Row.Equals(e.NewRow) = False Then
            '違う行が選択された場合

            If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then

                'DBより該当データの取得処理
                frm.sprSyukkaM.ActiveSheet.ActiveRowIndex = e.NewRow
                Me.ActionControl(LMC020C.EventShubetsu.LEAVE_CELL, frm)

                'START YANAI 要望番号646
            Else
                'DBより該当データの取得処理
                frm.sprSyukkaM.ActiveSheet.ActiveRowIndex = e.NewRow
                Me.ActionControl(LMC020C.EventShubetsu.DOUBLE_CLICK, frm)
                'END YANAI 要望番号646

            End If

        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprSyukkaM_LeaveCell")

    End Sub

    ' ''' <summary>
    ' ''' SPREADのロストフォーカスイベント(マーク情報)
    ' ''' </summary>
    ' ''' <param name="frm">フォーム</param>
    ' ''' <param name="e">イベント詳細</param>
    ' ''' <remarks></remarks>
    'Friend Sub sprMarkHed_LeaveCell(ByVal frm As LMC020F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

    '    MyBase.Logger.StartLog(MyBase.GetType.Name, "sprMarkHed_LeaveCell")

    '    If e.Row.Equals(e.NewRow) = False Then
    '        '違う行が選択された場合

    '        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then

    '            'DBより該当データの取得処理
    '            frm.sprMarkHed.ActiveSheet.ActiveRowIndex = e.NewRow
    '            Me.ActionControl(LMC020C.EventShubetsu.LEAVE_CELL_MARK_HED, frm)

    '            'START YANAI 要望番号646
    '        Else
    '            'DBより該当データの取得処理
    '            frm.sprMarkHed.ActiveSheet.ActiveRowIndex = e.NewRow
    '            Me.ActionControl(LMC020C.EventShubetsu.DOUBLE_CLICK, frm)
    '            'END YANAI 要望番号646

    '        End If

    '    End If

    '    MyBase.Logger.EndLog(MyBase.GetType.Name, "sprMarkHed_LeaveCell")

    'End Sub


    ''' <summary>
    ''' 梱数・端数の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numKonsu_Leave(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "numKonsu_Leave")

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.CHANGE_KOSU, frm)

        Logger.EndLog(Me.GetType.Name, "numKonsu_Leave")

    End Sub

    ''' <summary>
    ''' 数量の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numSouSuryo_Leave(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "numSuryo_Leave")

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.CHANGE_SURYO, frm)

        Logger.EndLog(Me.GetType.Name, "numSuryo_Leave")

    End Sub

    ''' <summary>
    ''' 入目の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub numIrime_Leave(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "numIrime_Leave")

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.CHANGE_IRIME, frm)

        Logger.EndLog(Me.GetType.Name, "numIrime_Leave")

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optCnt_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optCnt_Selected")

        '「出荷単位変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.OPT_KOSU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optCnt_Selected")

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optAmt_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optAmt_Selected")

        '「出荷単位変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.OPT_SURYO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optAmt_Selected")

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optKowake_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optKowake_Selected")

        '「出荷単位変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.OPT_KOWAKE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optKowake_Selected")

    End Sub

    ''' <summary>
    ''' 出荷単位変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub optSample_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optSample_Selected")

        '「出荷単位変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.OPT_SAMPLE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optSample_Selected")

    End Sub

    ''' <summary>
    ''' 運送手配区分変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbTehaiKbn_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbTehaiKbn_Selected")

        '「運送手配区分変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.UNSO_CHANGE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbTehaiKbn_Selected")

    End Sub

    ''' <summary>
    ''' タリフ分類区分変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbTariffKbun_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbTariffKbun_Selected")

        '「タリフ分類区分変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.UNSOTARIFF_CHANGE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbTariffKbun_Selected")

    End Sub

    ''' <summary>
    ''' 印刷種別変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbPRINT_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbPRINT_Selected")

        '「印刷種別変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.PRINT_CHANGE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbPRINT_Selected")

    End Sub

    ''' <summary>
    ''' 作業コードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub sagyo_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "sagyo_Enter")

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "sagyo_Enter")

    End Sub

    ''' <summary>
    ''' 届先コードの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub txtTodokesakiCd_Leave(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "txtTodokesakiCd_Leave")

        'START YANAI 要望番号909
        ''DBより該当データの取得処理
        'Me.ActionControl(LMC020C.EventShubetsu.LEAVE_DEST_CD, frm)
        If (frm.txtTodokesakiCd.TextValue).Equals(frm.txtTodokesakiCdOld.TextValue) = False Then
            'DBより該当データの取得処理
            Me.ActionControl(LMC020C.EventShubetsu.LEAVE_DEST_CD, frm)
        End If
        'END YANAI 要望番号909

        Logger.EndLog(Me.GetType.Name, "txtTodokesakiCd_Leave")

    End Sub

    ''' <summary>
    ''' 届先区分変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbTodokesaki_Selected(ByVal frm As LMC020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbTodokesaki_Selected")

        '「届先区分変更」処理
        Me.ActionControl(LMC020C.EventShubetsu.TODOKESAKI_CHANGE, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbTodokesaki_Selected")

    End Sub

    'START YANAI 要望番号513
    ''' <summary>
    ''' 運送会社コードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub unsoCd_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "unsoCd_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "unsoCd_Enter")

    End Sub

    ''' <summary>
    ''' 運送タリフコードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub unsoTariff_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "unsoTariff_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "unsoTariff_Enter")

    End Sub
    'END YANAI 要望番号513

    'START YANAI 要望番号481
    ''' <summary>
    ''' 割増タリフコードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub wariTariff_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "wariTariff_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "wariTariff_Enter")

    End Sub

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払運賃タリフコードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub shiharaiTariff_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "shiharaiTariff_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "shiharaiTariff_Enter")

    End Sub
    'END YANAI 要望番号513

    'START YANAI 要望番号481
    ''' <summary>
    ''' 支払割増タリフコードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub shiharaiwariTariff_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "shiharaiwariTariff_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "shiharaiwariTariff_Enter")

    End Sub
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START YANAI 要望番号481
    ''' <summary>
    ''' 届先コードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub destCd_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "wariTariff_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "wariTariff_Enter")

    End Sub

    ''' <summary>
    ''' 検索条件部分のコードテキストでのEnterイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub search_Enter(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "search_Enter")

        'Enterキー押下時イベント：１件時表示なし
        Me._PopupSkipFlg = False

        'DBより該当データの取得処理
        Me.ActionControl(LMC020C.EventShubetsu.MASTER_ENTER, frm)

        Logger.EndLog(Me.GetType.Name, "search_Enter")

    End Sub
    'END YANAI 要望番号481

    'START YANAI 要望番号1425 タリフ設定の機能追加：群馬
    ''' <summary>
    ''' 運送会社コードの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub txtUnsoCompanyCd_Leave(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "txtUnsoCompanyCd_Enter")

        If (frm.txtUnsoCompanyCd.TextValue).Equals(frm.txtUnsoCompanyCdOld.TextValue) = False OrElse _
            (frm.txtUnsoSitenCd.TextValue).Equals(frm.txtUnsoSitenCdOld.TextValue) = False Then
            Me.ActionControl(LMC020C.EventShubetsu.LEAVE_UNSO_CD, frm)
        End If

        '要望番号:2408 2015.09.17 追加START
        '運賃会社情報から自動送状区分を設定
        Call Me._G.GetAutoDenpSet(frm)
        '要望番号:2408 2015.09.17 追加START

        Logger.EndLog(Me.GetType.Name, "txtUnsoCompanyCd_Enter")

    End Sub
    'END YANAI 要望番号1425 タリフ設定の機能追加：群馬

    '追加開始 --- 2014.07.24 kikuchi
    ''' <summary>
    ''' 分析表添付の値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub cmbBunsakiTmp_Selected(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "cmbBunsakiTmp_Enter")

        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", frm.cmbEigyosyo.SelectedValue, _
                                                                                                         "' AND CUST_CD = '", frm.txtCust_Cd_L.TextValue, _
                                                                                                         "' AND SUB_KB = '78'"))

        If 0 < custDetailsDr.Length Then

            Dim LoopNum As Integer = Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows.Count - 1
            Dim i As Integer

            If frm.cmbBunsakiTmp.SelectedValue.ToString().Equals(LMC020C.BUNSEKI_ARI) Then
                'データ更新
                For i = 0 To LoopNum
                    Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(i).Item("COA_YN") = "01"
                Next
                'ラジオボタン描画更新
                Call Me._G.setBunsekiTemp(True)
            ElseIf frm.cmbBunsakiTmp.SelectedValue.ToString().Equals(LMC020C.BUNSEKI_NASI) Then
                'データ更新
                For i = 0 To LoopNum
                    Me._Ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows(i).Item("COA_YN") = "00"
                Next
                'ラジオボタン描画更新
                Call Me._G.setBunsekiTemp(False)
            End If

        End If

        Logger.EndLog(Me.GetType.Name, "cmbBunsakiTmp_Enter")

    End Sub
    '追加開始 --- 2014.07.24 kikuchi

    '2015.07.08 協立化学　シッピング対応 追加END
    ''' <summary>
    ''' マークタブLEAVE処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub TabPage2_Leave(ByRef frm As LMC020F)

        Logger.StartLog(Me.GetType.Name, "TabPage2_Leave")

        '「マークタブLEAVE」処理
        Me.ActionControl(LMC020C.EventShubetsu.TAB_LEAVE, frm)

        Logger.EndLog(Me.GetType.Name, "TabPage2_Leave")

    End Sub

    '2015.07.08 協立化学　シッピング対応 追加END

#Region "タブレット対応"
    ''' <summary>
    ''' 現場作業指示
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub WHSagyoShiji(ByVal frm As LMC020F, ByVal prm As LMFormData, ByVal procType As String)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        Dim ds As DataSet = New LMC930DS

        Dim tabStatus As String = String.Empty
        Select Case procType
            Case LMC930C.PROC_TYPE.INSTRUCT
                tabStatus = LMC930C.WH_TAB_SIJI_STATUS.INSTRUCTED
            Case LMC930C.PROC_TYPE.CANCEL
                tabStatus = LMC930C.WH_TAB_SIJI_STATUS.NOT_INSTRUCTED
            Case LMC930C.PROC_TYPE.DELETE
                tabStatus = LMC930C.WH_TAB_SIJI_STATUS.NOT_INSTRUCTED
        End Select

        '検品済の場合データセットに登録
        Dim dRow As DataRow = ds.Tables(LMC930C.TABLE_NM.LMC930IN).NewRow
        dRow.Item("NRS_BR_CD") = frm.cmbEigyosyo.SelectedValue.ToString()
        dRow.Item("OUTKA_NO_L") = frm.lblSyukkaLNo.TextValue
        dRow.Item("OUTKA_STATE_KB") = _Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_STATE_KB").ToString
        dRow.Item("ROW_NO") = String.Empty
        dRow.Item("PGID") = MyBase.GetPGID
        dRow.Item("SYS_UPD_DATE") = _Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("SYS_UPD_DATE").ToString
        dRow.Item("SYS_UPD_TIME") = _Ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("SYS_UPD_TIME").ToString
        dRow.Item("WH_TAB_STATUS_KB") = tabStatus
        dRow.Item("PROC_TYPE") = procType
        dRow.Item("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
        dRow.Item("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
        ds.Tables(LMC930C.TABLE_NM.LMC930IN).Rows.Add(dRow)

        '処理呼出
        prm.ParamDataSet = ds

        LMFormNavigate.NextFormNavigate(Me, "LMC930", prm)

        If MyBase.IsMessageExist Then

            MyBase.ShowMessage(frm)
        Else
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {frm.cmbJikkou.SelectedText, String.Empty})

        End If

        '処理終了アクション
        Call Me._LMCconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 現場作業指示ステータス更新
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetWhhSijiStatus(ByVal frm As LMC020F)

        If Not ("0".Equals(frm.lblSituation.RecordStatus) AndAlso _
            LMC020C.MODE_EDIT.Equals(frm.lblSituation.DispMode)) Then
            Exit Sub
        End If

        Dim dt As DataTable = _Ds.Tables(LMC020C.TABLE_NM_OUT_L)

        '現場作業指示ステータスの確認
        If LMC020C.WH_TAB_STATUS_00.Equals(dt.Rows(0).Item("WH_TAB_STATUS")) Then
            '未指示の場合は処理なし
            Exit Sub
        End If

        '再指示不要チェックの確認
        If "01".Equals(dt.Rows(0).Item("WH_TAB_NO_SIJI_FLG")) Then
            'チェック有の場合、現場作業指示ステータスを確認
            If LMC020C.WH_TAB_STATUS_02.Equals(dt.Rows(0).Item("WH_TAB_STATUS")) Then
                '現場作業指示ステータスが指示後変更の場合、指示済みに変更
                dt.Rows(0).Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_01
            End If
            '再指示不要チェック有の場合は処理終了
            Exit Sub
        End If

        'データテーブルの比較
        If CompareDataset() = True Then
            dt.Rows(0).Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_02
        End If

        If "00".Equals(dt.Rows(0).Item("WH_TAB_YN")) Then
            dt.Rows(0).Item("WH_TAB_STATUS") = LMC020C.WH_TAB_STATUS_99
        End If

    End Sub


    ''' <summary>
    ''' 変更確認
    ''' </summary>
    ''' <remarks></remarks>
    Private Function CompareDataset() As Boolean

        '出荷L
        Dim dtOutL As DataTable = _Ds.Tables(LMC020C.TABLE_NM_OUT_L)
        Dim dtCmprOutL As DataTable = _DsCmpr.Tables(LMC020C.TABLE_NM_OUT_L)
        '出荷予定日
        If Not dtOutL.Rows(0).Item("OUTKA_PLAN_DATE").Equals(dtCmprOutL.Rows(0).Item("OUTKA_PLAN_DATE")) Then
            Return True
        End If

        '納入予定日
        If Not dtOutL.Rows(0).Item("ARR_PLAN_DATE").Equals(dtCmprOutL.Rows(0).Item("ARR_PLAN_DATE")) Then
            Return True
        End If
        If Not dtOutL.Rows(0).Item("ARR_PLAN_TIME").Equals(dtCmprOutL.Rows(0).Item("ARR_PLAN_TIME")) Then
            Return True
        End If

        'オーダー番号
        If Not dtOutL.Rows(0).Item("CUST_ORD_NO").Equals(dtCmprOutL.Rows(0).Item("CUST_ORD_NO")) Then
            Return True
        End If

        '注文番号
        If Not dtOutL.Rows(0).Item("BUYER_ORD_NO").Equals(dtCmprOutL.Rows(0).Item("BUYER_ORD_NO")) Then
            Return True
        End If

        '届先
        If Not dtOutL.Rows(0).Item("DEST_CD").Equals(dtCmprOutL.Rows(0).Item("DEST_CD")) Then
            Return True
        End If

        ' 以下、届先名、届先住所1、届先住所2 の 3項目は、届先区分により表示元が分かれるが、
        ' DB 登録の際は届先区分に関係なく、
        ' C_OUTKA_L の DEST_NM(※), DEST_AD_1, DEST_AD_2 に設定しているので、
        ' この値同士を比較して変更の有無を判定する。
        ' ※ DataTalbe LMC020_OUTKA_L では DEST_NM2

        ' 届先名
        If Not dtOutL.Rows(0).Item("DEST_NM2").Equals(dtCmprOutL.Rows(0).Item("DEST_NM2")) Then
            Return True
        End If

        ' 届先住所1
        If Not dtOutL.Rows(0).Item("DEST_AD_1").Equals(dtCmprOutL.Rows(0).Item("DEST_AD_1")) Then
            Return True
        End If

        ' 届先住所2
        If Not dtOutL.Rows(0).Item("DEST_AD_2").Equals(dtCmprOutL.Rows(0).Item("DEST_AD_2")) Then
            Return True
        End If

        '届先住所3
        If Not dtOutL.Rows(0).Item("DEST_AD_3").Equals(dtCmprOutL.Rows(0).Item("DEST_AD_3")) Then
            Return True
        End If

        '届先電話番号
        If Not dtOutL.Rows(0).Item("DEST_TEL").Equals(dtCmprOutL.Rows(0).Item("DEST_TEL")) Then
            Return True
        End If

        '出荷時注意事項
        If Not dtOutL.Rows(0).Item("REMARK").Equals(dtCmprOutL.Rows(0).Item("REMARK")) Then
            Return True
        End If

        '現場注意事項
        If Not dtOutL.Rows(0).Item("WH_SIJI_REMARK").Equals(dtCmprOutL.Rows(0).Item("WH_SIJI_REMARK")) Then
            Return True
        End If

        '急ぎフラグ
        If Not dtOutL.Rows(0).Item("URGENT_YN").Equals(dtCmprOutL.Rows(0).Item("URGENT_YN")) Then
            Return True
        End If

        '現場作業フラグ(チェックなし⇒ありのときだけ)
        If Not dtOutL.Rows(0).Item("WH_TAB_YN").Equals(dtCmprOutL.Rows(0).Item("WH_TAB_YN")) AndAlso
             LMC020C.WH_TAB_YN_YES.Equals(dtOutL.Rows(0).Item("WH_TAB_YN")) Then
            Return True
        End If

        '梱包数
        If Not dtOutL.Rows(0).Item("OUTKA_PKG_NB").Equals(dtCmprOutL.Rows(0).Item("OUTKA_PKG_NB")) Then
            Return True
        End If

        '報告フラグ
        If Not dtOutL.Rows(0).Item("WH_TAB_HOKOKU_YN").Equals(dtCmprOutL.Rows(0).Item("WH_TAB_HOKOKU_YN")) Then
            Return True
        End If



        '運送
        Dim dtUnso As DataTable = _Ds.Tables(LMC020C.TABLE_NM_UNSO_L)
        Dim dtCmprUnso As DataTable = _DsCmpr.Tables(LMC020C.TABLE_NM_UNSO_L)
        '運送会社
        If Not dtUnso.Rows(0).Item("UNSO_CD").Equals(dtCmprUnso.Rows(0).Item("UNSO_CD")) Then
            Return True
        End If
        '支店
        If Not dtUnso.Rows(0).Item("UNSO_BR_CD").Equals(dtCmprUnso.Rows(0).Item("UNSO_BR_CD")) Then
            Return True
        End If


        '出荷M
        Dim dtOutM As DataTable = _Ds.Tables(LMC020C.TABLE_NM_OUT_M)
        Dim dtCmprOutM As DataTable = _DsCmpr.Tables(LMC020C.TABLE_NM_OUT_M)
        If dtOutM.Rows.Count <> dtCmprOutM.Rows.Count Then
            Return True
        End If
        For i As Integer = 0 To dtOutM.Rows.Count - 1
            '更新区分チェック
            If LMConst.FLG.ON.Equals(dtOutM.Rows(i).Item("UP_KBN")) Then
                '入目
                If Not dtOutM.Rows(i).Item("IRIME").Equals(dtCmprOutM.Rows(i).Item("IRIME")) Then
                    Return True
                End If
                '入目単位
                If Not dtOutM.Rows(i).Item("IRIME_UT").Equals(dtCmprOutM.Rows(i).Item("IRIME_UT")) Then
                    Return True
                End If
                '梱包個数
                If Not dtOutM.Rows(i).Item("OUTKA_M_PKG_NB").Equals(dtCmprOutM.Rows(i).Item("OUTKA_M_PKG_NB")) Then
                    Return True
                End If
                '個数
                If Not dtOutM.Rows(i).Item("OUTKA_TTL_NB").Equals(dtCmprOutM.Rows(i).Item("OUTKA_TTL_NB")) Then
                    Return True
                End If
                '数量
                If Not dtOutM.Rows(i).Item("OUTKA_TTL_QT").Equals(dtCmprOutM.Rows(i).Item("OUTKA_TTL_QT")) Then
                    Return True
                End If

            End If
        Next

        '出荷S
        Dim dtOutS As DataTable = _Ds.Tables(LMC020C.TABLE_NM_OUT_S)
        Dim dtCmprOutS As DataTable = _DsCmpr.Tables(LMC020C.TABLE_NM_OUT_S)
        If dtOutS.Rows.Count <> dtCmprOutS.Rows.Count Then
            Return True
        End If

        '作業
        Dim dtSagyo As DataTable = _Ds.Tables(LMC020C.TABLE_NM_SAGYO)
        Dim dtCmprSagyo As DataTable = _DsCmpr.Tables(LMC020C.TABLE_NM_SAGYO)
        '件数
        If dtSagyo.Rows.Count <> dtCmprSagyo.Rows.Count Then
            Return True
        End If
        For i As Integer = 0 To dtSagyo.Rows.Count - 1
            '作業レコードNo
            If Not dtSagyo.Rows(i).Item("SAGYO_REC_NO").Equals(dtCmprSagyo.Rows(i).Item("SAGYO_REC_NO")) Then
                Return True
            End If
            '作業CD
            If Not dtSagyo.Rows(i).Item("SAGYO_CD").Equals(dtCmprSagyo.Rows(i).Item("SAGYO_CD")) Then
                Return True
            End If
            '現場作業用備考
            If Not dtSagyo.Rows(i).Item("REMARK_SIJI").Equals(dtCmprSagyo.Rows(i).Item("REMARK_SIJI")) Then
                Return True
            End If
            '削除行(作業L)
            If "2".Equals(dtSagyo(i).Item("UP_KBN").ToString) AndAlso _
               "1".Equals(dtSagyo(i).Item("UP_KBN2").ToString) AndAlso
               LMConst.FLG.ON.Equals(dtSagyo(i).Item("SYS_DEL_FLG").ToString) Then
                Return True
            End If

        Next


        Return False

    End Function
#End Region
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class