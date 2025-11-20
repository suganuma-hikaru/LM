' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD040H : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base   '2017/09/25 追加 李
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports Microsoft.Office.Interop 'ADD 2019/8/27 依頼番号:007116,007119

''' <summary>
''' LMD040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Formフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD040F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD040G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConH As LMDControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConG As LMDControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    ''' チェックボックスをクリック行保存フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CheckRow As Integer

    ''' <summary>
    ''' 検索種別保存フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _SelectTp As String

    '画面間データを取得する
    Dim prmDs As DataSet

    'BLF名
    Private _BLF As String = "LMD040BLF"

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 現在庫検索種別フラグ（ラジオボタン）
    ''' </summary>
    ''' <remarks>ラジオボタンの値を保存</remarks>
    Private _OptTp As Integer

    '2017/09/25 修正 李↓
    ''2016.02.05 追加START
    ' ''' <summary>
    ' ''' 選択した言語を格納するフィールド
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    ''2016.02.05 追加END
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

        'フォームの作成
        Dim frm As LMD040F = New LMD040F(Me)
        Me._Frm = frm

        'Validateクラスの設定
        Me._V = New LMD040V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMD040G(Me, frm)

        Me._CheckRow = -1

        frm.KeyPreview = True

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'Validate共通クラスの設定
        Me._LMDConV = New LMDControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMDConH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMDConG = New LMDControlG(Me, DirectCast(frm, Form))

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbEigyo, frm.cmbSoko)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD040C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コンボボックスの初期値設定
        Call Me._G.SetcmbValue()

        '数値項目の書式設定
        Call Me._G.SetNumberControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Me._G.SetInitValue(frm)

        'コントロール個別設定
        Dim rootPgId As String = MyBase.RootPGID()
        If rootPgId IsNot Nothing AndAlso rootPgId.Equals("LMC020") = True Then
            '出荷編集画面からの遷移なら、編集中の荷主が初期設定される。（営業所・倉庫も）
            Call Me._G.SetControlLMC020(prmDs, frm)

            '検索処理
            Me.SelectListEvent(frm, LMConst.FLG.OFF, Integer.MinValue)
        Else
            Call Me._G.SetControl(Me.GetPGID(), frm)
        End If

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' プリント押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintBtnDown(ByVal frm As LMD040F)

        Dim tabFlg As String = String.Empty

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限・項目チェック
        If Me.IsCheckCall(frm, LMD040C.EventShubetsu.PRINT) = False Then

            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
            Exit Sub
        End If

        '印刷処理　　　
        Dim ds As DataSet = Nothing
        Dim tabKbn As String = String.Empty

        '帳票INパラに画面の検索条件を設定するので検索時のチェック
        With frm

            Dim printKbn As String = .cmbPrint.SelectedValue.ToString()
            Select Case printKbn

                Case LMD040C.PRINT_ZAIKO_RIREKI_LOT _
                    , LMD040C.PRINT_ZAIKO_RIREKI_GOODS

                    tabKbn = LMD040C.TAB_ZAIK
                    If .tabRireki.SelectedTab.Equals(.tabInOutHistoryByInka) = True Then
                        tabKbn = LMD040C.TAB_INKA
                    End If

                    '項目チェック
                    If Me.IsCheckCall(frm, LMD040C.EventShubetsu.KENSAKU, tabKbn) = False Then
                        '処理終了アクション
                        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
                        Exit Sub
                    End If

            End Select

            'コンボボックス選択値
            Select Case .cmbPrint.SelectedValue.ToString()

                Case LMD040C.PRINT_ZAIKO_RIREKI_LOT         '在庫履歴帳票（ロット別）
                    'LMD540呼び出し処理
                    ds = Me.PrintLotRireki(frm, tabKbn)

                Case LMD040C.PRINT_ZAIKO_RIREKI_GOODS       '在庫履歴帳票（商品別）
                    'LMD541呼び出し処理
                    ds = Me.PrintGoodsRireki(frm, tabKbn)

                Case LMD040C.PRINT_OKIBA_ZAIKOICHIRAN       '置場別・在庫一覧表
                    'LMD500呼び出し処理
                    ds = Me.PrintOkibaZai(frm)

                Case LMD040C.PRINT_GOODS_ZAIKOICHIRAN       '商品別・在庫一覧表
                    'LMD520呼び出し処理
                    ds = Me.PrintGoodsZai(frm)

                Case LMD040C.PRINT_TANAOROSI_ICHIRAN        '棚卸し一覧表
                    'LMD510呼び出し処理
                    ds = Me.PrintTanaOroshi(frm)

                Case LMD040C.PRINT_TANAOROSI_ICHIRAN_SYANAI '棚卸し一覧表(社内)
                    'LMD515呼び出し処理
                    ds = Me.PrintTanaOroshiSyanai(frm)

                Case LMD040C.PRINT_TANAOROSI_ICHIRAN_CFS    '棚卸し一覧表(CFS業務用)　'#(2012.07.24)コンソリ業務対応
                    'LMD519呼び出し処理
                    ds = Me.PrintTanaOroshiCFS(frm)

                Case LMD040C.PRINT_YOTEI_TANAOROSI_ICHIRAN  '予定棚卸し一覧表 '#(2012.11.06)千葉対応
                    'LMD610呼び出し処理
                    ds = Me.PrintYoteiTanaOroshi(frm)

                Case LMD040C.PRINT_SHOBO_ZAIKOICHIRAN  '消防類別・在庫一覧表       '2015.03.24
                    'LMD640呼び出し処理
                    ds = Me.PrintShoboZai(frm)
            End Select

        End With

        If IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '判定 
        Dim prevDt As DataTable = ds.Tables(LMConst.RD)
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
        '2015.10.22 tusnehira add
        '英語化対応
        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G002", New String() {_Frm.btnPrint.TextValue, ""})
        'MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

    End Sub

    'ADD START 2019/8/27 依頼番号:007116,007119
    ''' <summary>
    ''' 実行押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ExecutionBtnDown(ByVal frm As LMD040F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '単項目チェック（フォーム）
        If Me._V.IsInputCheck(LMD040C.EventShubetsu.EXECUTION, "", Me._G) Then

            'データセットに画面の値の設定
            Dim LMD040DS As LMD040DS = New LMD040DS
            Dim row As LMD040DS.LMD040INRow = LMD040DS.LMD040IN.NewLMD040INRow
            row.NRS_BR_CD = frm.cmbEigyo.SelectedValue.ToString
            row.WH_CD = frm.cmbSoko.SelectedValue.ToString
            LMD040DS.LMD040IN.AddLMD040INRow(row)

            'コンボボックス選択値
            Select Case frm.cmbExecution.SelectedValue.ToString()

                Case LMD040C.EXECUTION_EMPTY_RACK_REF  '空棚参照

                    Me.ExecutionEmptyRack(frm, LMD040DS)

                Case LMD040C.EXECUTION_ZAIKO_DIFF_LIST '在庫差異リスト

                    Me.ExecutionZaikoDiff(frm, LMD040DS)

                Case LMD040C.EXECUTION_EMPTY_RACK_LIST '空棚リスト

                    Me.ExecutionMakeEmptyRackList(frm, LMD040DS)

            End Select

        End If

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

    End Sub
    'ADD END 2019/8/27 依頼番号:007116,007119

    ''' <summary>
    ''' 入出荷編集処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeInOutka(ByVal frm As LMD040F, ByVal row As Integer, Optional ByVal flg As String = "")

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限・項目チェック
        If Me.IsCheckCall(frm, LMD040C.EventShubetsu.HENSHU, flg) = False Then

            '終了処理アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
            Exit Sub
        End If

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        '入出荷判定処理
        Call Me.changeInOutkaPopup(frm, row, flg)

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMD040F, ByVal key As String, ByVal Row As Integer)

        '開始処理
        Call Me._LMDConH.StartAction(frm)

        '検索処理を行う
        Dim selectFlg As Boolean = Me.SelectData(frm, key, Row)

        'キャッシュから名称取得
        Call Me.SetCachedName(frm)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        '検索成功の場合
        If selectFlg = True Then
            Call Me._G.AddRemoveTabPage()
        End If

    End Sub

    ''' <summary>
    ''' マスタ参照処理(F10マスタ参照)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMD040F)

        'マスタ参照処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.MasterShowEvent(frm, LMD040C.EventShubetsu.MASTER)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMD040F, ByVal EventShubetsu As LMD040C.EventShubetsu)

        Dim tabFlg As String = String.Empty

        '開始処理
        Call Me._LMDConH.StartAction(frm)

        '権限・項目チェック
        If Me.IsCheckCall(frm, LMD040C.EventShubetsu.MASTER) = False Then

            '終了処理
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        If String.IsNullOrEmpty(objNm) = True Then
            '荷主コード(大・中・小・極小）以外の場合はメッセージ表示
            MyBase.ShowMessage(frm, "G005")
        End If

        Select Case objNm

            Case frm.txtCust_Cd_L.Name, frm.txtCust_Cd_M.Name _
            , frm.txtCust_Cd_S.Name, frm.txtCust_Cd_SS.Name

                'コードが全て空だった場合は名称をクリアする
                If String.IsNullOrEmpty(frm.txtCust_Cd_L.TextValue) = True _
                And String.IsNullOrEmpty(frm.txtCust_Cd_M.TextValue) = True _
                And String.IsNullOrEmpty(frm.txtCust_Cd_S.TextValue) = True _
                And String.IsNullOrEmpty(frm.txtCust_Cd_SS.TextValue) = True Then
                    frm.txtCust_Nm.TextValue = String.Empty
                End If

                '荷主コード
                Call Me.ShowCustPopup(frm, objNm, prm, EventShubetsu)

            Case Else
                '荷主コード(大・中・小・極小）以外の場合はメッセージ表示
                MyBase.ShowMessage(frm, "G005")

        End Select
        '画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

    End Sub

    ''' <summary>
    ''' 初期荷主変更処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeNinushiEvent(ByVal frm As LMD040F)

        '権限チェック
        If Me._V.IsAuthorityChk(LMD040C.EventShubetsu.SHOKININUSHI) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'ポップアップ呼び出し処理
        Call Me.ChangeCustPopup(frm, prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            With prm.ParamDataSet.Tables(LMZ010C.TABLE_NM_OUT).Rows(0)
                frm.txtCust_Cd_L.TextValue = .Item("CUST_CD_L").ToString    '荷主コード（大）
                frm.txtCust_Cd_M.TextValue = .Item("CUST_CD_M").ToString    '荷主コード（中）
                frm.txtCust_Cd_S.TextValue = String.Empty                   '荷主コード（小）
                frm.txtCust_Cd_SS.TextValue = String.Empty                  '荷主コード（極小）
                frm.txtCust_Nm.TextValue = .Item("CUST_NM_L").ToString      '荷主名
            End With
        End If


        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())


    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMD040F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 履歴スプレッドダブルクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DoubleClickRireki(ByVal frm As LMD040F, ByVal e As Integer, ByVal flg As String)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '2016.02.09 英語化対応修正START
        Dim MAEZAN As String = "前残"
        Dim INYU As String = "移入"
        Dim ISYUTU As String = "移出"

        Dim kbn027Dr() As DataRow = Nothing
        Dim kbn031Dr() As DataRow = Nothing
        Dim kbn030Dr() As DataRow = Nothing
        '2017/09/25 修正 李↓
        If lgm.MessageLanguage.Equals(LMConst.FLG.OFF) = False Then

            kbn027Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K027", _
                                                                                                             "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                             "' AND SYS_DEL_FLG = '0'"))

            If kbn027Dr.Length > 0 Then
                MAEZAN = kbn027Dr(0).Item("KBN_NM1").ToString()
            End If

            kbn031Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K031", _
                                                                                                             "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                             "' AND SYS_DEL_FLG = '0'"))

            If kbn031Dr.Length > 0 Then
                INYU = kbn031Dr(0).Item("KBN_NM1").ToString()
            End If

            kbn030Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K030", _
                                                                                                             "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                             "' AND SYS_DEL_FLG = '0'"))
            '2017/09/25 修正 李↑

            If kbn030Dr.Length > 0 Then
                ISYUTU = kbn030Dr(0).Item("KBN_NM1").ToString()
            End If
        End If
        '2016.02.09 英語化対応修正END

        'Dim flg As String = "1"

        Dim SHUBETSU As String = String.Empty

        If "1".Equals(flg) = True Then
            '入荷ごとTab
            With frm.sprNyusyukkaN.ActiveSheet
                SHUBETSU = Me._LMDConV.GetCellValue(.Cells(e, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo))
            End With
        Else
            '在庫ごとTab
            With frm.sprNyusyukkaZ.ActiveSheet
                SHUBETSU = Me._LMDConV.GetCellValue(.Cells(e, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo))
            End With
        End If

        'データ種別が"前残"、"移入"、"移出"の場合、処理スキップ
        Select Case SHUBETSU
            Case MAEZAN, INYU, ISYUTU
                Exit Sub
        End Select

        Call Me.ChangeInOutka(frm, e, flg)

    End Sub

    'ADD START 2023/12/25 039659【LMS・EDI・ハンディ】FFEM熊本工場 LMS新規導入に伴う新規構築
    ''' <summary>
    ''' 実行ボタン表示判定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    Friend Function IsExecutionVisible(ByRef frm As LMD040F) As Boolean

        '富士フイルム 和光純薬 大分工場、足柄工場、熊本工場の場合、かつ
        '区分マスタに登録されている倉庫コードに一致する場合のみ表示
        If (("96".Equals(frm.cmbEigyo.SelectedValue.ToString) OrElse "98".Equals(frm.cmbEigyo.SelectedValue.ToString) OrElse
             "F2".Equals(frm.cmbEigyo.SelectedValue.ToString) OrElse "F3".Equals(frm.cmbEigyo.SelectedValue.ToString)) AndAlso
            (MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F030'",
                                                                         " AND KBN_NM4 = '", frm.cmbEigyo.SelectedValue.ToString, "'",
                                                                         " AND KBN_NM5 = '", frm.cmbSoko.SelectedValue.ToString, "'",
                                                                         " AND KBN_NM7 = '1'",
                                                                         " AND SYS_DEL_FLG = '0'")).Count > 0)) Then
            Return True
        End If

        Return False

    End Function
    'ADD END 2023/12/25 039659【LMS・EDI・ハンディ】FFEM熊本工場 LMS新規導入に伴う新規構築

#End Region 'イベント定義(一覧)

#Region "印刷"

    ''' <summary>
    ''' 在庫履歴帳票（ＬＯＴ別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="tabKbn">選択タブ</param>
    ''' <remarks></remarks>
    Private Function PrintLotRireki(ByVal frm As LMD040F, ByVal tabKbn As String) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD540DS()

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        'INパラメータの設定
        inDs = Me.SetInkaZaikoData(frm, inDs, tabKbn)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD540Print", inDs)

        Return inDs

    End Function

    ''' <summary>
    ''' 在庫履歴帳票（商品別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="tabKbn">選択タブ</param>
    ''' <remarks></remarks>
    Private Function PrintGoodsRireki(ByVal frm As LMD040F, ByVal tabKbn As String) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD541DS()

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        'INパラメータの設定
        inDs = Me.SetInkaZaikoData(frm, inDs, tabKbn)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD541Print", inDs)

        Return inDs

    End Function

    ''' <summary>
    ''' 置場別・在庫一覧表
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintOkibaZai(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD500DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_OKIBA_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD500Print", inDs)

        Return inDs

    End Function

    ''' <summary>
    ''' 商品別・在庫一覧表
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintGoodsZai(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD520DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_GOODS_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD520Print", inDs)

        Return inDs

    End Function

    ''' <summary>
    ''' 棚卸し一覧表
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintTanaOroshi(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD510DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_TANAOROSI_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD510Print", inDs)

        Return inDs

    End Function

    ''' <summary>
    ''' 棚卸し一覧表(社内)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintTanaOroshiSyanai(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD515DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_TANAOROSI_SYANAI_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD515Print", inDs)

        Return inDs

    End Function

    '#(2012.07.24) コンソリ業務対応 --- START ---
    ''' <summary>
    ''' 棚卸し一覧表(CFS業務用)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintTanaOroshiCFS(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD519DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_TANAOROSI_CFS_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD519Print", inDs)

        Return inDs

    End Function
    '#(2012.07.24) コンソリ業務対応 --- 　END  ---

    '#(2012.11.07) 千葉対応 --- START ---
    ''' <summary>
    ''' 棚卸し一覧表
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintYoteiTanaOroshi(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD610DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_YOTEI_TANAOROSI_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD610Print", inDs)

        Return inDs

    End Function

    '#(2012.11.07)  千葉対応 --- END ---

    '追加開始 --- 2015.03.24
    ''' <summary>
    ''' 消防類別・在庫一覧表
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function PrintShoboZai(ByVal frm As LMD040F) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD640DS()
        inDs = Me.SetDataSetPrtInData(frm, inDs, LMD040C.TABLE_NM_SHOBO_ZAI_PRINT)

        'プレビューDataTableの設定
        inDs = Me.SetRptDt(inDs)

        '印刷処理
        inDs = MyBase.CallWSA(Me._BLF, "DoLMD640Print", inDs)

        Return inDs

    End Function
    '追加終了 --- 2015.03.24


    ''' <summary>
    ''' 帳票プログラムに渡すIN情報を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tabKbn">タブ区分</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetInkaZaikoData(ByVal frm As LMD040F, ByVal ds As DataSet, ByVal tabKbn As String) As DataSet

        'INパラメータの設定
        If LMD040C.TAB_INKA.Equals(tabKbn) = True Then
            Call Me.SelectInkaOutkaData(frm)
        Else
            Call Me.SelectOutkaData(frm, LMD040C.TAB_HOKA, 0)
        End If

        Dim setDt As DataTable = Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Copy
        ds.Tables.Add(setDt)

        Return ds

    End Function

    ''' <summary>
    ''' プレビューDataTableを設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetRptDt(ByVal ds As DataSet) As DataSet

        ds.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())
        Return ds

    End Function

    ''' <summary>
    ''' 一覧表用データセット作成
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="inDr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetIchiranData(ByVal frm As LMD040F, ByVal inDr As DataRow, ByVal num As Integer) As DataRow

        'スプレッドの内容取得
        With frm.sprGenzaiko.ActiveSheet
            inDr.Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.NRS_BR_CD.ColNo))
            inDr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.ZAI_REC_NO.ColNo))
        End With

        Return inDr

    End Function

    ''' <summary>
    ''' 印刷用データセット設定（一覧以外）
    ''' 
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SetDataSetPrtInData(ByVal frm As LMD040F, ByVal ds As DataSet, ByVal dsNm As String) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Me._G.SetControlLock(frm, False)

        Dim flg As String = String.Empty
        Dim InkaStateKb As String = String.Empty

        Dim datatable As DataTable = ds.Tables(dsNm)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力内容取得
        With frm
            'パラメータ生成
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("WH_CD") = .cmbSoko.SelectedValue
            dr.Item("INKO_PLAN_DATE_FROM") = .imdNyukaFrom.TextValue
            dr.Item("INKO_PLAN_DATE_TO") = .imdNyukaTo.TextValue
            dr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            dr.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
            dr.Item("CUST_CD_S") = .txtCust_Cd_S.TextValue
            dr.Item("CUST_CD_SS") = .txtCust_Cd_SS.TextValue

            '★2013.02.25 / Notes1890対応開始
            dr.Item("TOU_NO") = .txtTouNo.TextValue
            dr.Item("SITU_NO") = .txtSituNo.TextValue
            dr.Item("ZONE_CD") = .txtZoneCd.TextValue
            dr.Item("LOCA") = .txtLocation.TextValue
            '★2013.02.25 / Notes1890対応終了

            If .chkZeroZaiko.Checked = True Then
                flg = LMConst.FLG.ON
            Else
                flg = LMConst.FLG.OFF
            End If

            If .optYotei.Checked = True Then
                InkaStateKb = "01"              '【予】
            ElseIf .optJikkou.Checked = True Then
                InkaStateKb = "02"              '【実】
            ElseIf .optAll.Checked = True Then
                InkaStateKb = "03"              '【全】
            End If

            dr.Item("INKA_STATE_KB") = InkaStateKb

        End With
        dr.Item("ZERO_FLG") = flg
        dr.Item("INKA_STATE_KB") = InkaStateKb

        '2016.02.05 追加START
        Select Case dsNm

            Case LMD040C.TABLE_NM_TANAOROSI_PRINT, LMD040C.TABLE_NM_TANAOROSI_SYANAI_PRINT, _
                 LMD040C.TABLE_NM_YOTEI_TANAOROSI_PRINT, LMD040C.TABLE_NM_OKIBA_PRINT, _
                 LMD040C.TABLE_NM_GOODS_PRINT, LMD040C.TABLE_NM_TANAOROSI_CFS_PRINT, _
                 LMD040C.TABLE_NM_SHOBO_ZAI_PRINT
                '2017/09/25 修正 李↓
                dr.Item("LANG_FLG") = lgm.MessageLanguage
                '2017/09/25 修正 李↑

            Case Else

        End Select
        '2016.02.05 追加END

        'スプレッドの内容取得
        With frm.sprGenzaiko.ActiveSheet
            '(2012.03.06) Notes №786 条件に"-"有り無し関係なく抽出させる --- START --- 
            'dr.Item("OKIBA") = Me._LMDConV.GetCellValue(.Cells(0, LMD040G.sprGenzaikoDef.OKIBA.ColNo))
            dr.Item("OKIBA") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.OKIBA.ColNo)).Replace("-", String.Empty)
            '(2012.03.06) Notes №786 条件に"-"有り無し関係なく抽出させる ---  END  ---
            dr.Item("GOODS_CD_CUST") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_CD_CUST.ColNo))
            dr.Item("GOODS_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_NM.ColNo))
            dr.Item("SEARCH_KEY_1") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_CATEGORY_1.ColNo))
            dr.Item("LOT_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.LOT_NO.ColNo))
            dr.Item("REMARK") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.REMARK.ColNo))
            dr.Item("SERIAL_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SERIAL_NO.ColNo))
            dr.Item("GOODS_COND_KB_1") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_1.ColNo))
            dr.Item("GOODS_COND_KB_2") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_2.ColNo))
            dr.Item("GOODS_COND_KB_3") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_3.ColNo))
            dr.Item("OFB_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.OFB_NM.ColNo))
            dr.Item("SPD_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SPD_NM.ColNo))
            dr.Item("CUST_COST_CD1") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo))
            dr.Item("CUST_COST_CD2") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo))
            dr.Item("SEARCH_KEY_2") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_CATEGORY_2.ColNo))
            dr.Item("CUST_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_NM.ColNo))
            '(2012.03.06) Notes №786 条件に"-"有り無し関係なく抽出させる --- START --- 
            'dr.Item("INKA_NO") = Me._LMDConV.GetCellValue(.Cells(0, LMD040G.sprGenzaikoDef.INKA_NO.ColNo))
            dr.Item("INKA_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.INKA_NO.ColNo)).Replace("-", String.Empty)
            '(2012.03.06) Notes №786 条件に"-"有り無し関係なく抽出させる ---  END  ---
            dr.Item("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_CD_NRS.ColNo))
            dr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ZAI_REC_NO.ColNo))
            dr.Item("ALLOC_PRIORITY") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.WARIATE_NM.ColNo))
            dr.Item("DEST_CD_P") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.DEST_CD_NM.ColNo))
            dr.Item("SHOBO_CD") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SYOUBOU_CD.ColNo))
            dr.Item("SHOBO_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SYOUBOU_NM.ColNo))
            dr.Item("TAX_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ZEI_KB_NM.ColNo))
            dr.Item("DOKU_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.DOKUGEKI_NM.ColNo))
            dr.Item("ONDO_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ONDO_NM.ColNo))
            'START YANAI 要望番号647
            dr.Item("IRIME_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.NB_UT.ColNo))
            dr.Item("NB_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ZAI_UT.ColNo))
            dr.Item("STD_IRIME_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.JITU_UT.ColNo))
            dr.Item("PKG_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.UT.ColNo))
            'END YANAI 要望番号647

        End With

        'データの設定
        datatable.Rows.Add(dr)

        Return ds

    End Function

#End Region '印刷

#Region "内部処理"

    ''' <summary>
    '''  入荷・出荷判定処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="row"></param>
    ''' <param name="flg"></param>
    ''' <remarks></remarks>
    Private Sub changeInOutkaPopup(ByVal frm As LMD040F, ByVal row As Integer, Optional ByVal flg As String = "")

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        '2016.02.09 英語化対応　修正START
        Dim NYUKA As String = "入荷"
        Dim SHUKKA As String = "出荷"
        Dim HURIIN As String = "振入"
        Dim HURIOUT As String = "振出"
        Dim SHUBETSU As String = String.Empty

        Dim kbn028Dr() As DataRow = Nothing
        '2017/09/25 修正 李↓
        kbn028Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K028", _
                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                         "' AND SYS_DEL_FLG = '0'"))

        If kbn028Dr.Length > 0 Then
            NYUKA = kbn028Dr(0).Item("KBN_NM1").ToString()
        End If

        Dim kbn034Dr() As DataRow = Nothing
        kbn034Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K034", _
                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                         "' AND SYS_DEL_FLG = '0'"))

        If kbn034Dr.Length > 0 Then
            SHUKKA = kbn034Dr(0).Item("KBN_NM1").ToString()
        End If

        Dim kbn029Dr() As DataRow = Nothing
        kbn029Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K029", _
                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                         "' AND SYS_DEL_FLG = '0'"))

        If kbn029Dr.Length > 0 Then
            HURIIN = kbn029Dr(0).Item("KBN_NM1").ToString()
        End If

        Dim kbn035Dr() As DataRow = Nothing
        kbn035Dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "K035", _
                                                                                                         "' AND SUBSTRING(KBN_CD,2,1) = '", lgm.MessageLanguage, _
                                                                                                         "' AND SYS_DEL_FLG = '0'"))
        '2017/09/25 修正 李↑

        If kbn035Dr.Length > 0 Then
            HURIOUT = kbn035Dr(0).Item("KBN_NM1").ToString()
        End If
        '2016.02.09 英語化対応　修正END


        Dim chkList As ArrayList = New ArrayList()
        Dim max As Integer = 0
        Dim num As Integer = 0
        If String.IsNullOrEmpty(flg) = True Then
            Select Case frm.tabRireki.SelectedTab.Name
                Case frm.tabInOutHistoryByInka.Name
                    chkList = Me._V.getCheckListInka()
                    max = chkList.Count() - 1
                    chkList = Me._LMDConH.GetCheckList(frm.sprNyusyukkaN.ActiveSheet, LMD040G.sprNyusyukkaZDef.DEF.ColNo)
                    With frm.sprNyusyukkaN.ActiveSheet
                        For i As Integer = 0 To max
                            num = Convert.ToInt32(chkList(i))
                            SHUBETSU = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo))
                        Next
                        'START YANAI 要望番号593
                        flg = "1"
                        'END YANAI 要望番号593
                    End With

                Case frm.tabInOutHistoryByOutka.Name
                    chkList = Me._V.getCheckListOutka()
                    max = chkList.Count() - 1
                    chkList = Me._LMDConH.GetCheckList(frm.sprNyusyukkaZ.ActiveSheet, LMD040G.sprNyusyukkaZDef.DEF.ColNo)
                    'START YANAI 要望番号593
                    'With frm.sprNyusyukkaN.ActiveSheet
                    With frm.sprNyusyukkaZ.ActiveSheet
                        'END YANAI 要望番号593
                        For i As Integer = 0 To max
                            num = Convert.ToInt32(chkList(i))
                            SHUBETSU = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo))
                        Next
                        'START YANAI 要望番号593
                        flg = "2"
                        'END YANAI 要望番号593
                    End With
            End Select
        Else
            Select Case frm.tabRireki.SelectedTab.Name
                Case frm.tabInOutHistoryByInka.Name
                    With frm.sprNyusyukkaN.ActiveSheet
                        SHUBETSU = Me._LMDConV.GetCellValue(.Cells(row, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo))
                    End With
                Case frm.tabInOutHistoryByOutka.Name
                    With frm.sprNyusyukkaZ.ActiveSheet
                        SHUBETSU = Me._LMDConV.GetCellValue(.Cells(row, LMD040G.sprNyusyukkaZDef.SYUBETU_Z.ColNo))
                    End With
            End Select

        End If
        Select Case SHUBETSU

            Case NYUKA, HURIIN
                Call Me.ChangeInkaPopup(frm, prm, flg, row)

            Case SHUKKA, HURIOUT
                Call Me.ChangeOutkaPopup(frm, prm, flg, row)

            Case Else
                Exit Sub
        End Select
    End Sub

    ''' <summary>
    ''' 入荷データ編集(LMB020)参照
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeInkaPopup(ByVal frm As LMD040F, ByRef prm As LMFormData, Optional ByVal flg As String = "", Optional ByVal row As Integer = Integer.MaxValue)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim chkList2 As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count() - 1
        Dim num As Integer = 0
        Dim num2 As Integer = 0
        Dim prmDs As DataSet = New LMB020DS
        Dim dr As DataRow = prmDs.Tables(LMControlC.LMB020C_TABLE_NM_IN).NewRow()
        chkList2 = Me._LMDConH.GetCheckList(frm.sprGenzaiko.ActiveSheet, _G.sprGenzaikoDef.DEF.ColNo)

        Select Case flg
            'START YANAI 要望番号593
            'Case "1", ""   '入荷ごとTab
            Case "1"   '入荷ごとTab
                'END YANAI 要望番号593
                With frm.sprNyusyukkaN.ActiveSheet
                    If (Integer.MaxValue).Equals(row) = False Then
                        num = frm.sprNyusyukkaN.ActiveSheet.ActiveRowIndex
                    Else
                        chkList = Me._LMDConH.GetCheckList(frm.sprNyusyukkaN.ActiveSheet, LMD040G.sprNyusyukkaZDef.DEF.ColNo)
                        num = Convert.ToInt32(chkList(0))
                    End If
                    num2 = Convert.ToInt32(chkList2(0))

                    dr.Item("INKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo))
                    dr.Item("INKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo))
                    prm.SkipFlg = False
                End With
            Case "2"        '在庫ごとTab
                With frm.sprNyusyukkaZ.ActiveSheet
                    If (Integer.MaxValue).Equals(row) = False Then
                        num = frm.sprNyusyukkaZ.ActiveSheet.ActiveRowIndex
                    Else
                        chkList = Me._LMDConH.GetCheckList(frm.sprNyusyukkaZ.ActiveSheet, LMD040G.sprNyusyukkaZDef.DEF.ColNo)
                        num = Convert.ToInt32(chkList(0))
                    End If
                    num2 = Convert.ToInt32(chkList2(0))

                    dr.Item("INKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo))
                    dr.Item("INKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo))
                    prm.SkipFlg = True
                End With

        End Select

        dr.Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.NRS_BR_CD.ColNo))
        dr.Item("CUST_CD_L") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.CUST_CD_L.ColNo))
        dr.Item("CUST_CD_M") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.CUST_CD_M.ColNo))
        dr.Item("CUST_NM") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.CUST_NM.ColNo))

        prmDs.Tables(LMControlC.LMB020C_TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        '入荷データ編集(LMB020)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMB020", prm)

    End Sub

    ''' <summary>
    ''' 出荷データ編集(LMC020)参照
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeOutkaPopup(ByVal frm As LMD040F, ByRef prm As LMFormData, Optional ByVal flg As String = "", Optional ByVal row As Integer = Integer.MaxValue)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim chkList2 As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count() - 1
        Dim num As Integer = 0
        Dim num2 As Integer = 0
        Dim prmDs As DataSet = New LMC020DS

        Dim dr As DataRow = prmDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).NewRow()
        chkList2 = Me._LMDConH.GetCheckList(frm.sprGenzaiko.ActiveSheet, _G.sprGenzaikoDef.DEF.ColNo)

        Select Case flg
            'START YANAI 要望番号593
            'Case "1", ""   '入荷ごとTab
            Case "1"   '入荷ごとTab
                'END YANAI 要望番号593

                With frm.sprNyusyukkaN.ActiveSheet
                    If (Integer.MaxValue).Equals(row) = False Then
                        num = frm.sprNyusyukkaN.ActiveSheet.ActiveRowIndex
                    Else
                        chkList = Me._LMDConH.GetCheckList(frm.sprNyusyukkaN.ActiveSheet, LMD040G.sprNyusyukkaZDef.DEF.ColNo)
                        num = Convert.ToInt32(chkList(0))
                    End If
                    num2 = Convert.ToInt32(chkList2(0))

                    dr.Item("OUTKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo))
                    dr.Item("OUTKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo))
                    prm.SkipFlg = False
                End With

            Case "2"        '在庫ごとTab

                With frm.sprNyusyukkaZ.ActiveSheet
                    If (Integer.MaxValue).Equals(row) = False Then
                        num = frm.sprNyusyukkaZ.ActiveSheet.ActiveRowIndex
                    Else
                        chkList = Me._LMDConH.GetCheckList(frm.sprNyusyukkaZ.ActiveSheet, LMD040G.sprNyusyukkaZDef.DEF.ColNo)
                        num = Convert.ToInt32(chkList(0))
                    End If
                    num2 = Convert.ToInt32(chkList2(0))

                    dr.Item("OUTKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo))
                    dr.Item("OUTKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo))
                    prm.SkipFlg = True
                End With

        End Select

        dr.Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.NRS_BR_CD.ColNo))
        dr.Item("WH_CD") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.NRS_CR_CD.ColNo))
        dr.Item("CUST_CD_L") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.CUST_CD_L.ColNo))
        dr.Item("CUST_CD_M") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.CUST_CD_M.ColNo))
        dr.Item("CUST_NM") = Me._LMDConV.GetCellValue(frm.sprGenzaiko.ActiveSheet.Cells(num2, _G.sprGenzaikoDef.CUST_NM.ColNo))

        prmDs.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs

        '出荷データ編集(LMC020)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMC020", prm)


    End Sub

    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowCustPopup(ByVal frm As LMD040F, ByVal objNM As String, ByRef prm As LMFormData, ByVal EventShubetsu As LMD040C.EventShubetsu)

        Dim prmDs As DataSet = New LMZ260DS()
        Dim custNmL As String = String.Empty
        Dim custNmM As String = String.Empty
        Dim custNmS As String = String.Empty
        Dim custNmSS As String = String.Empty
        'パラメータ生成
        Dim dr As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow()
        dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        'START SHINOHARA 要望番号513
        If EventShubetsu = LMD040C.EventShubetsu.ENTER Then
            dr.Item("CUST_CD_L") = frm.txtCust_Cd_L.TextValue
            dr.Item("CUST_CD_M") = frm.txtCust_Cd_M.TextValue
            dr.Item("CUST_CD_S") = frm.txtCust_Cd_S.TextValue
            dr.Item("CUST_CD_SS") = frm.txtCust_Cd_SS.TextValue
        End If
        'END SHINOHARA 要望番号513
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        'dr.Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '荷主マスタ照会(LMZ260)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            'PopUpから取得したデータをコントロールにセット
            frm.txtCust_Cd_L.TextValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_L").ToString()    '荷主コード大
            frm.txtCust_Cd_M.TextValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_M").ToString()    '荷主コード中
            frm.txtCust_Cd_S.TextValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_S").ToString()    '荷主コード小
            frm.txtCust_Cd_SS.TextValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_SS").ToString()    '荷主コード極小
            custNmL = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_L").ToString()
            custNmM = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_M").ToString()
            custNmS = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_S").ToString()
            custNmSS = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_SS").ToString()
            frm.txtCust_Nm.TextValue = String.Concat(custNmL, "　", custNmM, "　", custNmS, "　", custNmSS)
        End If

    End Sub

    ''' <summary>
    ''' 初期荷主変更(LMZ010)参照
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ChangeCustPopup(ByVal frm As LMD040F, ByRef prm As LMFormData)

        Dim prmDs As DataSet = New LMZ010DS

        Dim dr As DataRow = prmDs.Tables(LMZ010C.TABLE_NM_IN).NewRow()
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        prmDs.Tables(LMZ010C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs

        '初期荷主変更(LMZ010)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ010", prm)

    End Sub

    ''' <summary>
    ''' チェック（権限・項目）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMD040F, ByVal EVENTSHUBETSU As LMD040C.EventShubetsu, Optional ByVal tabFlg As String = "") As Boolean

        '権限チェック
        If Me._V.IsAuthorityChk(EVENTSHUBETSU) = False Then
            Return False
        End If

        '単項目チェック（フォーム）
        If Me._V.IsInputCheck(EVENTSHUBETSU, tabFlg, Me._G) = False Then
            Return False
        End If

        '関連項目チェック
        If Me._V.isRelationCheck(frm, EVENTSHUBETSU, tabFlg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 再描画処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ReSelectData(ByVal frm As LMD040F)

        'フラグの設定
        Dim tabFlg As String = String.Empty
        Dim keyFlg As String = LMConst.FLG.ON
        Dim reFlg As Boolean = True
        Dim row As Integer = -1
        Dim zaiRecNo As String = Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Rows(0).Item("ZAI_REC_NO").ToString()

        With frm

            '現在庫Tab再描画
            tabFlg = LMD040C.TAB_SONOTA
            Call Me.SelectSprData(Me._SelectTp, frm, tabFlg, reFlg)

            '検索前と同じデータにチェック
            Dim max As Integer = .sprGenzaiko.Sheets(0).RowCount - 1
            For i As Integer = 0 To max

                If zaiRecNo.Equals(.sprGenzaiko.Sheets(0).Cells(i, LMD040C.SprColumnIndex.ZAI_REC_NO).Value.ToString()) = True Then
                    row = i
                    .sprGenzaiko.Sheets(0).Cells(i, LMD040C.SprColumnIndex.DEF).Value = True
                    Exit For
                End If

            Next

            '現在表示画面再描画
            Select Case Me._Frm.tabRireki.SelectedTab.Name

                Case Me._Frm.tabInOutHistoryByInka.Name              '入出荷（入荷ごと）タブ選択時

                    tabFlg = LMD040C.TAB_INKA
                    'DBリードオンリー設定 ADD 2021/11/05
                    'Call Me.SelectSprData("SelectListDataInka", frm, tabFlg)
                    Call Me.SelectSprData("SelectListDataInka", frm, tabFlg, , "1")

                Case Me._Frm.tabInOutHistoryByOutka.Name              '入出荷（在庫ごと）タブ選択時

                    tabFlg = LMD040C.TAB_ZAIK
                    Call Me.SelectSprData("SelectListDataZaiko", frm, tabFlg)

            End Select


        End With

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal frm As LMD040F, ByVal key As String, ByVal Row As Integer) As Boolean

        'フラグの設定
        Dim tabFlg As String = String.Empty
        Dim keyFlg As String = LMConst.FLG.ON

        'DataSet設定
        prmDs = New LMD040DS()

        Dim rtnResult As Boolean = True

        With frm
            Select Case Me._Frm.tabRireki.SelectedTab.Name
                Case Me._Frm.tabGenZaiko.Name              '現在庫タブ選択時

                    '行保存値クリア
                    Me._CheckRow = -1

                    'INデータセット作成
                    Call Me.SetDataSetInData(frm)

                    '検索条件格納
                    Me._FindDs = Me.prmDs.Copy()
                    Me._SelectTp = String.Empty

                    tabFlg = LMD040C.TAB_SONOTA
                    '権限・項目チェック
                    If Me.IsCheckCall(frm, LMD040C.EventShubetsu.KENSAKU, tabFlg) = False Then
                        Return False
                    End If

                    Select Case True
                        '商品の場合
                        Case .optGoods.Checked                 '商品の場合

                            Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS
                            rtnResult = Me.SelectSprData("SelectListDataGoods", frm, tabFlg)
                            Me._SelectTp = "SelectListDataGoods"

                        Case .optGoodLotIrime.Checked          '商品・ロット・入目の場合

                            Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS_LOT
                            rtnResult = Me.SelectSprData("SelectListDataGoodsLot", frm, tabFlg)
                            Me._SelectTp = "SelectListDataGoodsLot"

                        Case .optGoodLotOkiba.Checked          '商品・ロット・置場の場合

                            Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS_OKIBA
                            rtnResult = Me.SelectSprData("SelectListDataOkibaLot", frm, tabFlg)
                            Me._SelectTp = "SelectListDataOkibaLot"

                        Case .optOkiba.Checked                 '商品・ロット・入目・置場の場合

                            Me._OptTp = LMD040C.KensakuTp.KENSAKU_OKIBA
                            rtnResult = Me.SelectSprData("SelectListDataOkiba", frm, tabFlg)
                            Me._SelectTp = "SelectListDataOkiba"

                        Case .optSyousai.Checked               '詳細の場合

                            Me._OptTp = LMD040C.KensakuTp.KENSAKU_DETAIL
                            rtnResult = Me.SelectSprData("SelectListDataAll", frm, tabFlg)
                            Me._SelectTp = "SelectListDataAll"

                    End Select

                    '入出荷（在庫ごと）タブロック制御
                    Select Case True
                        '詳細で検索した場合のみ、在庫ごとタブ選択可能
                        Case .optSyousai.Checked
                            .sprNyusyukkaZ.Locked = True
                        Case Else
                            .sprNyusyukkaZ.Locked = True
                    End Select

                Case Me._Frm.tabInOutHistoryByInka.Name              '入出荷（入荷ごと）タブ選択時
                    tabFlg = LMD040C.TAB_INKA
                    '権限・項目チェック
                    If Me.IsCheckCall(frm, LMD040C.EventShubetsu.KENSAKU, tabFlg) = False Then
                        Return False
                    End If

                    Call Me.SelectInkaOutkaData(frm)
                    'DBリードオンリー設定 ADD 2021/11/05
                    'Call Me.SelectSprData("SelectListDataInka", frm, tabFlg)
                    Call Me.SelectSprData("SelectListDataInka", frm, tabFlg, , "1")
                Case Me._Frm.tabInOutHistoryByOutka.Name              '入出荷（在庫ごと）タブ選択時
                    If keyFlg.Equals(key) = False Then
                        tabFlg = LMD040C.TAB_ZAIK
                    Else
                        tabFlg = LMD040C.TAB_HOKA
                    End If
                    '権限・項目チェック
                    If Me.IsCheckCall(frm, LMD040C.EventShubetsu.KENSAKU, tabFlg) = False Then
                        Return False
                    End If

                    Call Me.SelectOutkaData(frm, tabFlg, Row)
            End Select
        End With

        Return rtnResult

    End Function

    ''' <summary>
    ''' 検索成功時共通処理（現在庫）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Function SuccessSelect(ByVal frm As LMD040F, ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMD040C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        '現在庫スプレッド表示・非表示判定
        Me._G.ChengedSpreadCol()

        Return True

    End Function

    ''' <summary>
    ''' 検索成功時共通処理（入出荷・入荷）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelectInk(ByVal frm As LMD040F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMD040C.TABLE_NM_RIREKI)

        Call Me._G.ChangedSpreadNCol()

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'Me._G.SetControlLock(frm, True)

        'ヘッダデータをコントロールに設定
        Call Me._G.SetTabControl(frm.tabInOutHistoryByInka.Name, Me._CheckRow)

        '取得データをSPREADに表示
        Call Me._G.SetSpreadZaiko(dt, frm.sprNyusyukkaN, True)

        Dim cnt As Integer = dt.Rows.Count()

        'メッセージエリアの設定
        If cnt = 0 Then
            MyBase.ShowMessage(frm, "G001")
        Else
            MyBase.ShowMessage(frm, "G008", New String() {dt.Rows.Count.ToString()})
        End If

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（入出荷・在庫）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelectOutk(ByVal frm As LMD040F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMD040C.TABLE_NM_RIREKI_ZAI)
        Dim dr As DataRow = ds.Tables(LMD040C.TABLE_NM_GENZAIKO).Rows(0)

        Call Me._G.ChangedSpreadZCol()

        '画面解除
        Call MyBase.UnLockedControls(frm)

        'Me._G.SetControlLock(frm, True)

        'ヘッダデータをコントロールに設定
        Call Me._G.SetTabControl(frm.tabInOutHistoryByOutka.Name, Me._CheckRow)

        '取得データをSPREADに表示
        Call Me._G.SetSpreadZaiko(dt, frm.sprNyusyukkaZ)

        Dim cnt As Integer = dt.Rows.Count()

        'メッセージエリアの設定
        If cnt = 0 Then
            MyBase.ShowMessage(frm, "G001")
        Else
            MyBase.ShowMessage(frm, "G008", New String() {dt.Rows.Count.ToString()})
        End If

    End Sub

    ''' <summary>
    ''' 取得処理呼出
    ''' </summary>
    ''' <param name="SelectListData"></param>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function SelectSprData(ByVal SelectListData As String, ByVal frm As LMD040F, ByVal flg As String, Optional ByVal reFlg As Boolean = False _
                                   , Optional ByVal readDBFLG As String = "") As Boolean
        Dim Genzaiko As String = "1"
        Dim Nyuka As String = "2"
        Dim Shukka As String = "3"
        Dim ShukkaDouble As String = "4"
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, SelectListData)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = New DataSet
        If reFlg = True Then
            rtnDs = Me._LMDConH.CallWSAAction(DirectCast(frm, Form), "LMD040BLF", SelectListData, Me._FindDs _
                                        , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                        (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                       , Convert.ToInt32(Convert.ToDouble(
                                         MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))), readDBFLG)
        Else
            rtnDs = Me._LMDConH.CallWSAAction(DirectCast(frm, Form), "LMD040BLF", SelectListData, prmDs _
                                        , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                        (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                      , Convert.ToInt32(Convert.ToDouble(
                                         MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))), readDBFLG)
        End If

        '検索成功時共通処理を行う
        Dim rtnResult As Boolean = True
        If rtnDs Is Nothing = False Then
            Select Case flg
                Case Genzaiko
                    'START YANAI 要望番号617
                    '合計個数の初期化
                    Me._Frm.numSumKosu.Value = 0
                    'END YANAI 要望番号617

                    'SPREAD(表示行)初期化
                    frm.sprGenzaiko.CrearSpread()
                    rtnResult = Me.SuccessSelect(frm, rtnDs)
                Case Nyuka
                    'SPREAD(表示行)初期化
                    frm.sprNyusyukkaN.CrearSpread()
                    Call Me.SuccessSelectInk(frm, rtnDs)
                Case Shukka, ShukkaDouble
                    'SPREAD(表示行)初期化
                    frm.sprNyusyukkaZ.CrearSpread()
                    Call Me.SuccessSelectOutk(frm, rtnDs)
            End Select

            If 0 < rtnDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Rows.Count Then

                prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Clear()
                Dim dr As DataRow = prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).NewRow()
                prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Rows.Add(dr)
                For i As Integer = 0 To prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Columns.Count - 1
                    prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Rows(0).Item(i) = rtnDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Rows(0).Item(i)
                Next

            End If

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, SelectListData)

        Return rtnResult

    End Function

    ''' <summary>
    ''' タブチェンジ検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ChangeTabSelectEvent(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.TabControlEventArgs)

        '開始処理
        Call Me._LMDConH.StartAction(frm)

        With frm
            Select Case e.TabPage.Name
                Case .tabInOutHistoryByInka.Name, .tabInOutHistoryByOutka.Name

                    Call Me._G.ClearTabControl(e.TabPage.Name)
                    Call Me.SelectData(frm, LMConst.FLG.OFF, Integer.MinValue)

            End Select
        End With

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

    End Sub

    ''' <summary>
    ''' Enter押下時マスタ参照判定処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Private Sub EnterkeyControl(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        Dim MasterFlg As Boolean = False

        If e.KeyCode = Keys.Enter Then

            With frm

                Select Case controlNm

                    Case .txtCust_Cd_L.Name, .txtCust_Cd_M.Name, .txtCust_Cd_S.Name, .txtCust_Cd_SS.Name
                        If String.IsNullOrEmpty(frm.txtCust_Cd_L.TextValue) = False _
                        OrElse String.IsNullOrEmpty(frm.txtCust_Cd_M.TextValue) = False _
                        OrElse String.IsNullOrEmpty(frm.txtCust_Cd_S.TextValue) = False _
                        OrElse String.IsNullOrEmpty(frm.txtCust_Cd_SS.TextValue) = False Then

                            MasterFlg = True
                        Else
                            frm.txtCust_Nm.TextValue = String.Empty
                        End If

                End Select

                If MasterFlg = True Then

                    'Pop起動処理：１件時表示なし
                    Me._PopupSkipFlg = False
                    Me.MasterShowEvent(frm, LMD040C.EventShubetsu.ENTER)
                Else

                    frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
                End If


            End With

        End If

    End Sub

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMD040F)

        With frm
            Dim custCdM As String = String.Empty
            Dim custCdS As String = String.Empty
            Dim custCdSS As String = String.Empty

            '荷主名称
            If String.IsNullOrEmpty(.txtCust_Cd_L.TextValue) = False Then
                If String.IsNullOrEmpty(.txtCust_Cd_M.TextValue) = True Then
                    custCdM = "00"
                Else
                    custCdM = .txtCust_Cd_M.TextValue
                End If
                If String.IsNullOrEmpty(.txtCust_Cd_S.TextValue) = True Then
                    custCdS = "00"
                Else
                    custCdS = .txtCust_Cd_S.TextValue
                End If
                If String.IsNullOrEmpty(.txtCust_Cd_SS.TextValue) = True Then
                    custCdSS = "00"
                Else
                    custCdSS = .txtCust_Cd_SS.TextValue
                End If
                .txtCust_Nm.TextValue = GetCachedCust(.txtCust_Cd_L.TextValue, custCdM, custCdS, custCdSS)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>

    Private Function GetCachedCust(ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                            "NRS_BR_CD = '", _Frm.cmbEigyo.SelectedValue.ToString, "' AND " _
                                                                         , "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return String.Concat(dr(0).Item("CUST_NM_L").ToString, "　", dr(0).Item("CUST_NM_M").ToString, "　", dr(0).Item("CUST_NM_S").ToString, "　", dr(0).Item("CUST_NM_SS").ToString)
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G007"
    End Function

    'ADD START 2019/8/27 依頼番号:007116,007119
    ''' <summary>
    ''' 空棚の情報をメッセージ欄に表示
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub ExecutionEmptyRack(ByVal frm As LMD040F, ByVal ds As DataSet)

        '空棚の合計を取得
        ds = MyBase.CallWSA(Me._BLF, "ExecutionEmptyRack", ds)

        '一般棚総数
        Dim normalTotalNum As Integer = 0
        '毒劇棚総数
        Dim dokuTotalNum As Integer = 0
        '一般空棚数
        Dim normalEmptyNum As Integer = 0
        '毒劇空棚数
        Dim dokuEmptyNum As Integer = 0

        For Each row As DataRow In ds.Tables("EMPTY_RACK").Rows

            If "".Equals(row.Item("DOKU_KB").ToString) OrElse "01".Equals(row.Item("DOKU_KB").ToString) Then
                '一般

                normalTotalNum += 1

                '空棚の判定
                If Integer.Parse(row.Item("ZAI_NB").ToString) = 0 Then
                    normalEmptyNum += 1
                End If

            Else
                '毒劇

                dokuTotalNum += 1

                '空棚の判定
                If Integer.Parse(row.Item("ZAI_NB").ToString) = 0 Then
                    dokuEmptyNum += 1
                End If

            End If

        Next

        'メッセージ欄に表示
        frm.SetMsgAriaText("空棚：一般品（" & normalEmptyNum & "／" & normalTotalNum & "棚）　毒劇（" & dokuEmptyNum & "／" & dokuTotalNum & "棚）")

    End Sub

    ''' <summary>
    ''' 在庫差異のリストをExcelファイルに出力
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub ExecutionZaikoDiff(ByVal frm As LMD040F, ByVal ds As DataSet)

        'LMSでの在庫数量、EDIでの在庫数量を取得
        ds = MyBase.CallWSA(Me._BLF, "ExecutionZaikoDiff", ds)

        'ファイルパス設定
        Dim filePath As String = LMD040C.ZAIKO_DIFF_PATH
        Dim fileName As String = LMD040C.ZAIKO_DIFF_NAME

        'ファイル名に日時を付与(置換)
        Dim dateTime As String() = MyBase.GetSystemDateTime()
        fileName = fileName.Replace("YYYYMMDD", dateTime(0))
        fileName = fileName.Replace("hhmmss", dateTime(1).Substring(0, 6))

        '既存ファイルを削除
        If System.IO.File.Exists(filePath & fileName) = True Then
            System.IO.File.Delete(filePath & fileName)
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Try
            'EXCEL開始
            xlApp = New Excel.Application
            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()

            '作業シート設定
            xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)
            xlSheet.Name = "在庫差異"

            'ヘッダ(2行)の値の設定
            xlSheet.Cells(1, 1) = "商品コード"
            xlSheet.Cells(1, 2) = "商品名"
            xlSheet.Cells(1, 3) = "ロット"
            xlSheet.Cells(1, 4) = "LMS在庫"
            xlSheet.Cells(1, 8) = "SAP在庫"
            xlSheet.Cells(1, 12) = "差異"
            xlSheet.Cells(2, 4) = "計"
            xlSheet.Cells(2, 5) = "合格品"
            xlSheet.Cells(2, 6) = "検査中"
            xlSheet.Cells(2, 7) = "不合格"
            xlSheet.Cells(2, 8) = "計"
            xlSheet.Cells(2, 9) = "合格品"
            xlSheet.Cells(2, 10) = "検査中"
            xlSheet.Cells(2, 11) = "不合格"
            xlSheet.Cells(2, 12) = "計"
            xlSheet.Cells(2, 13) = "合格品"
            xlSheet.Cells(2, 14) = "検査中"
            xlSheet.Cells(2, 15) = "不合格"

            '表示書式の設定
            xlSheet.Range("A:C").NumberFormat = "@"

            'ボディの値の設定
            Dim rowIndex As Integer = 3
            For Each row As DataRow In ds.Tables("ZAIKO_DIFF").Rows

                '各値の計算
                Dim LMSTotal As Double = _
                    Double.Parse(row.Item("LMS_PRODUCT_QT").ToString) + _
                    Double.Parse(row.Item("LMS_INSPECT_QT").ToString) + _
                    Double.Parse(row.Item("LMS_DEFECT_QT").ToString)
                Dim SAPTotal As Double = _
                    Double.Parse(row.Item("SAP_PRODUCT_NB").ToString) + _
                    Double.Parse(row.Item("SAP_INSPECT_NB").ToString) + _
                    Double.Parse(row.Item("SAP_DEFECT_NB").ToString)
                Dim diffProduct As Double = Double.Parse(row.Item("LMS_PRODUCT_QT").ToString) - Double.Parse(row.Item("SAP_PRODUCT_NB").ToString)
                Dim diffInspect As Double = Double.Parse(row.Item("LMS_INSPECT_QT").ToString) - Double.Parse(row.Item("SAP_INSPECT_NB").ToString)
                Dim diffDefect As Double = Double.Parse(row.Item("LMS_DEFECT_QT").ToString) - Double.Parse(row.Item("SAP_DEFECT_NB").ToString)
                Dim diffTotal As Double = diffProduct + diffInspect + diffDefect

                'LMS在庫、SAP在庫の両方が計=0の行は出力対象外
                If LMSTotal = 0 AndAlso SAPTotal = 0 Then
                    Continue For
                End If

                xlSheet.Cells(rowIndex, 1) = row.Item("MATNR").ToString                        '商品コード
                xlSheet.Cells(rowIndex, 2) = row.Item("HINMOKU_TXT").ToString                  '商品名
                xlSheet.Cells(rowIndex, 3) = row.Item("CHARG").ToString                        'ロット
                xlSheet.Cells(rowIndex, 4) = LMSTotal.ToString                                 'LMS在庫_計
                xlSheet.Cells(rowIndex, 5) = Me.ZeroObjToEmptyStr(row.Item("LMS_PRODUCT_QT"))  'LMS在庫_合格品
                xlSheet.Cells(rowIndex, 6) = Me.ZeroObjToEmptyStr(row.Item("LMS_INSPECT_QT"))  'LMS在庫_検査中
                xlSheet.Cells(rowIndex, 7) = Me.ZeroObjToEmptyStr(row.Item("LMS_DEFECT_QT"))   'LMS在庫_不合格
                xlSheet.Cells(rowIndex, 8) = SAPTotal.ToString                                 'SAP在庫_計
                xlSheet.Cells(rowIndex, 9) = Me.ZeroObjToEmptyStr(row.Item("SAP_PRODUCT_NB"))  'SAP在庫_合格品
                xlSheet.Cells(rowIndex, 10) = Me.ZeroObjToEmptyStr(row.Item("SAP_INSPECT_NB")) 'SAP在庫_検査中
                xlSheet.Cells(rowIndex, 11) = Me.ZeroObjToEmptyStr(row.Item("SAP_DEFECT_NB"))  'SAP在庫_不合格
                xlSheet.Cells(rowIndex, 12) = Me.ZeroObjToEmptyStr(diffTotal)                  '差異_計
                xlSheet.Cells(rowIndex, 13) = Me.ZeroObjToEmptyStr(diffProduct)                '差異_合格品
                xlSheet.Cells(rowIndex, 14) = Me.ZeroObjToEmptyStr(diffInspect)                '差異_検査中
                xlSheet.Cells(rowIndex, 15) = Me.ZeroObjToEmptyStr(diffDefect)                 '差異_不合格

                rowIndex += 1
            Next

            'セルの結合
            xlSheet.Range("A1:A2").Merge()
            xlSheet.Range("B1:B2").Merge()
            xlSheet.Range("C1:C2").Merge()
            xlSheet.Range("D1:G1").Merge()
            xlSheet.Range("H1:K1").Merge()
            xlSheet.Range("L1:O1").Merge()

            'ヘッダ行の背景色
            xlSheet.Range("A1:O2").Interior.Color = RGB(191, 191, 191)

            '揃え
            xlSheet.Range("A1:O2").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter
            xlSheet.Range("A1:C2").VerticalAlignment = Excel.XlVAlign.xlVAlignBottom

            '太字
            xlSheet.Range("D:D").Font.Bold = True
            xlSheet.Range("H:H").Font.Bold = True
            xlSheet.Range("L:L").Font.Bold = True

            '罫線を点線に変更
            xlSheet.Range("A:O").Borders.Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range("A:O").Borders.Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range("A:O").Borders.Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range("A:O").Borders.Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range("A:O").Borders.Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range("A:O").Borders.Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlDot

            '列幅の調整
            xlSheet.Range("A:C").EntireColumn.AutoFit()

            '保存時の確認ダイアログを非表示に設定
            xlApp.DisplayAlerts = False

            'ディレクトリ作成
            System.IO.Directory.CreateDirectory(filePath)

            'ファイル保存
            xlBook.SaveAs(filePath & fileName)

            '保存時の確認ダイアログを表示に設定
            xlApp.DisplayAlerts = True

        Finally
            'EXCEL終了

            If xlSheet IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
            End If

            If xlBook IsNot Nothing Then
                xlBook.Close(False)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
            End If

            If xlBooks IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
            End If

            If xlApp IsNot Nothing Then
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If

        End Try

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub

    ''' <summary>
    ''' 空棚リストExcelの作成
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub ExecutionMakeEmptyRackList(ByVal frm As LMD040F, ByVal ds As DataSet)

        '空棚の合計を取得
        ds = MyBase.CallWSA(Me._BLF, "ExecutionEmptyRack", ds)

        'ファイルパス設定
        Dim filePath As String = LMD040C.EMPTY_RACK_PATH
        Dim fileName As String = LMD040C.EMPTY_RACK_NAME

        'ファイル名に日時を付与(置換)
        Dim dateTime As String() = MyBase.GetSystemDateTime()
        fileName = fileName.Replace("YYYYMMDD", dateTime(0))
        fileName = fileName.Replace("hhmmss", dateTime(1).Substring(0, 6))

        '既存ファイルを削除
        If System.IO.File.Exists(filePath & fileName) = True Then
            System.IO.File.Delete(filePath & fileName)
        End If

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Try
            'EXCEL開始
            xlApp = New Excel.Application
            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()

            '作業シート設定
            xlSheet = DirectCast(xlBook.Worksheets(1), Excel.Worksheet)
            xlSheet.Name = "空棚"

            'ヘッダ(1行)の値の設定
            xlSheet.Cells(1, 1) = "ロケーション"

            'ボディの値の設定
            Dim rowIndex As Integer = 2
            For Each row As DataRow In ds.Tables("EMPTY_RACK").Rows

                '空棚の判定
                If Integer.Parse(row.Item("ZAI_NB").ToString) = 0 Then

                    xlSheet.Cells(rowIndex, 1) = String.Format("{0}-{1}-{2}", row.Item("TOU_NO").ToString, row.Item("SITU_NO").ToString, row.Item("ZONE_CD").ToString)
                    rowIndex += 1

                End If

            Next

            'ヘッダ行の背景色
            xlSheet.Range("A1").Interior.Color = RGB(191, 191, 191)

            '揃え
            xlSheet.Range("A1").HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter


            '罫線を点線に変更
            Dim lineRange As String = "A1:A" & (rowIndex - 1).ToString
            xlSheet.Range(lineRange).Borders.Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range(lineRange).Borders.Item(Excel.XlBordersIndex.xlEdgeLeft).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range(lineRange).Borders.Item(Excel.XlBordersIndex.xlEdgeRight).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range(lineRange).Borders.Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range(lineRange).Borders.Item(Excel.XlBordersIndex.xlInsideHorizontal).LineStyle = Excel.XlLineStyle.xlDot
            xlSheet.Range(lineRange).Borders.Item(Excel.XlBordersIndex.xlInsideVertical).LineStyle = Excel.XlLineStyle.xlDot

            '列幅の調整
            xlSheet.Range("A:A").EntireColumn.AutoFit()

            '表示書式の設定
            xlSheet.Range("A:A").NumberFormat = "@"

            '保存時の確認ダイアログを非表示に設定
            xlApp.DisplayAlerts = False

            'ディレクトリ作成
            System.IO.Directory.CreateDirectory(filePath)

            'ファイル保存
            xlBook.SaveAs(filePath & fileName)

            '保存時の確認ダイアログを表示に設定
            xlApp.DisplayAlerts = True

        Finally
            'EXCEL終了

            If xlSheet IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
            End If

            If xlBook IsNot Nothing Then
                xlBook.Close(False)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
            End If

            If xlBooks IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
            End If

            If xlApp IsNot Nothing Then
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If

        End Try

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"Excel出力処理", ""})

    End Sub


    ''' <summary>
    ''' 引数が0の場合は空文字列、引数が0以外の場合はそのまま返す
    ''' </summary>
    ''' <param name="obj"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ZeroObjToEmptyStr(ByVal obj As Object) As String
        If "0".Equals(obj.ToString) Then
            Return ""
        Else
            Return obj.ToString
        End If
    End Function
    'ADD END 2019/8/27 依頼番号:007116,007119

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' 現在庫タブ選択時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMD040F)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Me._G.SetControlLock(frm, False)

        Dim flg As String = String.Empty
        Dim InkaStateKb As String = String.Empty

        Me.prmDs.Tables(LMControlC.LMD040C_TABLE_NM_IN).Clear()

        Dim datatable As DataTable = Me.prmDs.Tables(LMControlC.LMD040C_TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力内容取得
        With frm

            '検索種別
            Select Case True
                Case .optGoods.Checked                 '商品の場合
                    Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS
                Case .optGoodLotIrime.Checked          '商品・ロット・入目の場合
                    Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS_LOT
                Case .optGoodLotOkiba.Checked          '商品・ロット・置場の場合
                    Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS_OKIBA
                Case .optOkiba.Checked                 '置場の場合
                    Me._OptTp = LMD040C.KensakuTp.KENSAKU_OKIBA
                Case .optSyousai.Checked               '詳細の場合
                    Me._OptTp = LMD040C.KensakuTp.KENSAKU_DETAIL
            End Select

            dr.Item("OPT_TP") = Me._OptTp

            'パラメータ生成
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("WH_CD") = .cmbSoko.SelectedValue
            dr.Item("INKO_PLAN_DATE_FROM") = .imdNyukaFrom.TextValue
            dr.Item("INKO_PLAN_DATE_TO") = .imdNyukaTo.TextValue
            dr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            dr.Item("CUST_CD_M") = .txtCust_Cd_M.TextValue
            dr.Item("CUST_CD_S") = .txtCust_Cd_S.TextValue
            dr.Item("CUST_CD_SS") = .txtCust_Cd_SS.TextValue

            If .chkZeroZaiko.Checked = True Then
                flg = LMConst.FLG.ON
            Else
                flg = LMConst.FLG.OFF
            End If
            If .optYotei.Checked = True Then
                InkaStateKb = "01"
            ElseIf .optJikkou.Checked = True Then
                InkaStateKb = "02"
            ElseIf .optAll.Checked = True Then
                InkaStateKb = "03"
            End If

            '(2013.02.14)要望番号1843 置場抽出条件追加 -- START --
            dr.Item("TOU_NO") = .txtTouNo.TextValue
            dr.Item("SITU_NO") = .txtSituNo.TextValue
            dr.Item("ZONE_CD") = .txtZoneCd.TextValue
            dr.Item("LOCA") = .txtLocation.TextValue
            '(2013.02.14)要望番号1843 置場抽出条件追加 --  END  --

        End With
        dr.Item("ZERO_FLG") = flg
        dr.Item("INKA_STATE_KB") = InkaStateKb

        'スプレッドの内容取得
        With frm.sprGenzaiko.ActiveSheet

            'スプレッド検索行クリア
            If Me._OptTp.Equals(LMD040C.KensakuTp.KENSAKU_DETAIL) = False Then

                '詳細以外の場合
                .Cells(0, _G.sprGenzaikoDef.REMARK.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.SERIAL_NO.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_1.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_2.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_3.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.OFB_NM.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.SPD_NM.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.INKA_NO.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.GOODS_CD_NRS.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.ZAI_REC_NO.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.WARIATE_NM.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.DEST_CD_NM.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.ZEI_KB_NM.ColNo).Text = String.Empty
                .Cells(0, _G.sprGenzaikoDef.IRIME.ColNo).Text = String.Empty        'ADD 2018/10/30 依頼番号 : 002779   【LMS】在庫履歴照会_入り目検索できるように変更

            End If

            Select Case Me._OptTp

                Case LMD040C.KensakuTp.KENSAKU_GOODS         '商品の場合
                    .Cells(0, _G.sprGenzaikoDef.LOT_NO.ColNo).Text = String.Empty
                    .Cells(0, _G.sprGenzaikoDef.OKIBA.ColNo).Text = String.Empty

                Case LMD040C.KensakuTp.KENSAKU_GOODS_LOT     '商品・ロット・入目の場合
                    .Cells(0, _G.sprGenzaikoDef.OKIBA.ColNo).Text = String.Empty

            End Select

            '共通表示項目
            dr.Item("GOODS_CD_CUST") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_CD_CUST.ColNo))
            dr.Item("GOODS_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_NM.ColNo))
            dr.Item("SEARCH_KEY_1") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_CATEGORY_1.ColNo))
            dr.Item("CUST_COST_CD1") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_KANJYO_CD_1.ColNo))
            dr.Item("CUST_COST_CD2") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_KANJYO_CD_2.ColNo))
            dr.Item("SEARCH_KEY_2") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_CATEGORY_2.ColNo))
            dr.Item("CUST_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.CUST_NM.ColNo))
            dr.Item("SHOBO_CD") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SYOUBOU_CD.ColNo))
            dr.Item("SHOBO_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SYOUBOU_NM.ColNo))
            dr.Item("DOKU_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.DOKUGEKI_NM.ColNo))
            dr.Item("ONDO_NM") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ONDO_NM.ColNo))

            '変動項目
            dr.Item("LOT_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.LOT_NO.ColNo))
            dr.Item("OKIBA") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.OKIBA.ColNo)).Replace("-", String.Empty)
            dr.Item("REMARK") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.REMARK.ColNo))
            '要望番号:1702  terakawa 2012.12.19 Start
            dr.Item("REMARK_OUT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.REMARK_OUT.ColNo))
            '要望番号:1702  terakawa 2012.12.19 End
            dr.Item("SERIAL_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SERIAL_NO.ColNo))
            dr.Item("GOODS_COND_KB_1") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_1.ColNo))
            dr.Item("GOODS_COND_KB_2") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_2.ColNo))
            dr.Item("GOODS_COND_KB_3") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_COND_NM_3.ColNo))
            dr.Item("OFB_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.OFB_NM.ColNo))
            dr.Item("SPD_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.SPD_NM.ColNo))
            dr.Item("INKA_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.INKA_NO.ColNo)).Replace("-", String.Empty)
            dr.Item("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.GOODS_CD_NRS.ColNo))
            dr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ZAI_REC_NO.ColNo))
            dr.Item("ALLOC_PRIORITY") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.WARIATE_NM.ColNo))
            dr.Item("DEST_CD_P") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.DEST_CD_NM.ColNo))
            dr.Item("TAX_KB") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ZEI_KB_NM.ColNo))
            'START YANAI 要望番号647
            dr.Item("IRIME_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.NB_UT.ColNo))
            dr.Item("NB_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.ZAI_UT.ColNo))
            dr.Item("STD_IRIME_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.JITU_UT.ColNo))
            dr.Item("PKG_UT") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.UT.ColNo))
            'END YANAI 要望番号647
            dr.Item("IRIME") = Me._LMDConV.GetCellValue(.Cells(0, _G.sprGenzaikoDef.IRIME.ColNo))       'ADD 2018/10/30 依頼番号 : 002779   【LMS】在庫履歴照会_入り目検索できるように変更
        End With

        '2017/09/25 修正 李↓
        dr.Item("LANG_FLG") = lgm.MessageLanguage
        '2017/09/25 修正 李↑

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 入出荷（入荷ごと）タブ選択時検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectInkaOutkaData(ByVal frm As LMD040F)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Me._G.SetControlLock(frm, False)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count() - 1
        Dim flg As String = String.Empty
        Dim num As Integer = 0

        Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Clear()

        Dim datatable As DataTable = Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO)
        Dim dr As DataRow = datatable.NewRow()
        'フォーム入力内容取得
        With frm

            If .chkSyukkaTorikesi.Checked = True Then
                flg = LMConst.FLG.ON
            Else
                flg = LMConst.FLG.OFF
            End If
            dr.Item("INKO_PLAN_DATE_FROM") = .imdHyoujiFrom.TextValue
            dr.Item("INKO_PLAN_DATE_TO") = .imdHyoujiTo.TextValue
            dr.Item("OUTKA_DEL_FLG") = flg
            dr.Item("OPT_TP") = Me._OptTp
            If Me._OptTp = LMD040C.KensakuTp.KENSAKU_DETAIL OrElse Me._OptTp = LMD040C.KensakuTp.KENSAKU_OKIBA _
               OrElse Me._OptTp = LMD040C.KensakuTp.KENSAKU_GOODS_LOT Then
                dr.Item("GUI_IRIME") = .lblIrimeN.Value '
            End If


        End With
        chkList = Me._LMDConH.GetCheckList(frm.sprGenzaiko.ActiveSheet, _G.sprGenzaikoDef.DEF.ColNo)
        For i As Integer = 0 To max
            num = Convert.ToInt32(chkList(i))
            'スプレッドの内容取得
            With frm.sprGenzaiko.ActiveSheet
                dr.Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.NRS_BR_CD.ColNo))
                dr.Item("WH_CD") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.NRS_CR_CD.ColNo))
                dr.Item("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.GOODS_CD_NRS.ColNo))
                dr.Item("CD_NRS_TO") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.CD_NRS_TO.ColNo))
                dr.Item("INKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.INKA_NO_L.ColNo))
                dr.Item("INKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.INKA_NO_M.ColNo))
                dr.Item("INKA_NO_S") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.INKA_NO_S.ColNo))
                dr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.ZAI_REC_NO.ColNo))

                dr.Item("LOT_NO") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.LOT_NO.ColNo))
                dr.Item("OKIBA") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.OKIBA.ColNo))

                If String.IsNullOrEmpty(dr.Item("GUI_IRIME").ToString()) = True Then
                    dr.Item("GUI_IRIME") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.IRIME.ColNo))
                End If
            End With
        Next

        'TabFlg
        Dim kazuKbn As String = String.Empty
        Dim tabData As String = String.Empty
        Select Case frm.tabRireki.SelectedTab.Name
            Case frm.tabInOutHistoryByInka.Name              '入出荷履歴（入荷ごと）タブ選択時

                tabData = LMD040C.TAB_FLG_INKA

                If frm.optKosu.Checked = True Then
                    kazuKbn = LMD040C.KAZU_KOSU
                Else
                    kazuKbn = LMD040C.KAZU_SURYO
                End If

            Case frm.tabInOutHistoryByOutka.Name             '入出荷履歴（在庫ごと）タブ選択時

                tabData = LMD040C.TAB_FLG_ZAIK

                If frm.optKosuZaiko.Checked = True Then
                    kazuKbn = LMD040C.KAZU_KOSU
                Else
                    kazuKbn = LMD040C.KAZU_SURYO
                End If

        End Select

        Dim ptnId As String = String.Empty
        Select Case frm.cmbPrint.SelectedValue.ToString()

            Case LMD040C.PRINT_ZAIKO_RIREKI_LOT

                ptnId = LMD040C.PTN_ID_LOT

            Case LMD040C.PRINT_ZAIKO_RIREKI_GOODS

                ptnId = LMD040C.PTN_ID_GOODS

        End Select

        dr.Item("PTN_ID") = ptnId
        dr.Item("KAZU_KB") = kazuKbn
        dr.Item("TAB_FLG") = tabData

        '2017/09/25 修正 李↓
        dr.Item("LANG_FLG") = lgm.MessageLanguage
        '2017/09/25 修正 李↑

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 在庫ごとの検索条件
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="tabFlg">タブ区分</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub SetOutkaData(ByVal frm As LMD040F, ByVal tabFlg As String, ByVal rowNo As Integer)

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Me._G.SetControlLock(frm, False)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim flg As String = String.Empty
        Dim num As Integer = 0

        Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO).Clear()

        Dim datatable As DataTable = Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力内容取得
        With frm

            If .chkSyukkaTorikesi.Checked = True Then
                flg = LMConst.FLG.ON
            Else
                flg = LMConst.FLG.OFF
            End If
            dr.Item("INKO_PLAN_DATE_FROM") = .imdHyoujiFrom.TextValue
            dr.Item("INKO_PLAN_DATE_TO") = .imdHyoujiTo.TextValue
            dr.Item("OUTKA_DEL_FLG") = flg
            dr.Item("GUI_IRIME") = .lblIrimeZ.Value
        End With
        chkList = Me._LMDConH.GetCheckList(frm.sprGenzaiko.ActiveSheet, _G.sprGenzaikoDef.DEF.ColNo)
        num = Convert.ToInt32(chkList(0))
        Dim sprGenzaiko As Win.Spread.LMSpreadSearch = frm.sprGenzaiko
        Dim sprNyushukkaZ As Win.Spread.LMSpread = frm.sprNyusyukkaZ

        'スプレッドの内容取得
        With sprGenzaiko.ActiveSheet
            dr.Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.NRS_BR_CD.ColNo))
            dr.Item("WH_CD") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.NRS_CR_CD.ColNo))
            dr.Item("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.GOODS_CD_NRS.ColNo))
            dr.Item("CD_NRS_TO") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.CD_NRS_TO.ColNo))
        End With

        '(2012.11.22)要望番号1587 在庫レコード番号は現在庫タブの値を取得する --- START ---
        With sprGenzaiko.ActiveSheet
            dr.Item("INKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.INKA_NO_L.ColNo))
            dr.Item("INKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.INKA_NO_M.ColNo))
            dr.Item("INKA_NO_S") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.INKA_NO_S.ColNo))
            dr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(num, _G.sprGenzaikoDef.ZAI_REC_NO.ColNo))
        End With

        'If LMD040C.TAB_HOKA.Equals(tabFlg) = False Then
        '    With sprGenzaiko.ActiveSheet
        '        dr.Item("INKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprGenzaikoDef.INKA_NO_L.ColNo))
        '        dr.Item("INKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprGenzaikoDef.INKA_NO_M.ColNo))
        '        dr.Item("INKA_NO_S") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprGenzaikoDef.INKA_NO_S.ColNo))
        '        dr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(num, LMD040G.sprGenzaikoDef.ZAI_REC_NO.ColNo))
        '    End With
        'Else
        '    With sprNyushukkaZ.ActiveSheet()
        '        dr.Item("INKA_NO_L") = Me._LMDConV.GetCellValue(.Cells(rowNo, LMD040G.sprNyusyukkaZDef.INKA_NO_L_Z.ColNo))
        '        dr.Item("INKA_NO_M") = Me._LMDConV.GetCellValue(.Cells(rowNo, LMD040G.sprNyusyukkaZDef.INKA_NO_M_Z.ColNo))
        '        dr.Item("INKA_NO_S") = Me._LMDConV.GetCellValue(.Cells(rowNo, LMD040G.sprNyusyukkaZDef.INKA_NO_S_Z.ColNo))
        '        dr.Item("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(rowNo, LMD040G.sprNyusyukkaZDef.ZAI_REC_NO_Z.ColNo))
        '    End With
        'End If
        '(2012.11.22)要望番号1587 在庫レコード番号は現在庫タブの値を取得する ---  END  ---

        'TabFlg
        Dim kazuKbn As String = String.Empty
        Dim tabData As String = String.Empty
        Select Case frm.tabRireki.SelectedTab.Name
            Case frm.tabInOutHistoryByInka.Name              '入出荷履歴（入荷ごと）タブ選択時

                tabData = LMD040C.TAB_FLG_INKA

                If frm.optKosu.Checked = True Then
                    kazuKbn = LMD040C.KAZU_KOSU
                Else
                    kazuKbn = LMD040C.KAZU_SURYO
                End If

            Case frm.tabInOutHistoryByOutka.Name             '入出荷履歴（在庫ごと）タブ選択時

                tabData = LMD040C.TAB_FLG_ZAIK

                If frm.optKosuZaiko.Checked = True Then
                    kazuKbn = LMD040C.KAZU_KOSU
                Else
                    kazuKbn = LMD040C.KAZU_SURYO
                End If

        End Select

        Dim ptnId As String = String.Empty
        Select Case frm.cmbPrint.SelectedValue.ToString()

            Case LMD040C.PRINT_ZAIKO_RIREKI_LOT

                ptnId = LMD040C.PTN_ID_LOT

            Case LMD040C.PRINT_ZAIKO_RIREKI_GOODS

                ptnId = LMD040C.PTN_ID_GOODS

        End Select

        dr.Item("PTN_ID") = ptnId
        dr.Item("KAZU_KB") = kazuKbn
        dr.Item("TAB_FLG") = tabData

        '2017/09/25 修正 李↓
        dr.Item("LANG_FLG") = lgm.MessageLanguage
        '2017/09/25 修正 李↑

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 入出荷（在庫ごと）タブ選択時検索処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectOutkaData(ByVal frm As LMD040F, ByVal tabFlg As String, ByVal rowNo As Integer)

        '検索条件の設定
        Call Me.SetOutkaData(frm, tabFlg, rowNo)

        Dim sprNyushukkaZ As Win.Spread.LMSpread = frm.sprNyusyukkaZ
        Dim datatable As DataTable = Me.prmDs.Tables(LMD040C.TABLE_NM_GENZAIKO)

        If String.IsNullOrEmpty(datatable.Rows(0).Item("ZAI_REC_NO").ToString()) = False Then
            frm.sprNyusyukkaZ.CrearSpread()
            Call Me.SelectSprData("SelectListDataZaiko", frm, tabFlg)
        Else

            Dim cnt As Integer = sprNyushukkaZ.ActiveSheet.Rows.Count
            'メッセージエリアの設定
            If cnt > 0 Then
                MyBase.ShowMessage(frm)
            Else
                MyBase.ShowMessage(frm, "G008", New String() {sprNyushukkaZ.ActiveSheet.Rows.Count.ToString()})
            End If

        End If

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F5押下時処理呼び出し（入出荷編集）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeInOutka")

        Call Me.ChangeInOutka(frm, Integer.MaxValue)

        Logger.EndLog(Me.GetType.Name, "ChangeInOutka")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm, LMConst.FLG.OFF, Integer.MinValue)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し（マスタ参照）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "MasterShowEvent")

        Me.OpenMasterPop(frm)

        Logger.EndLog(Me.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(初期荷主変更)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeNinushiEvent")

        Me.ChangeNinushiEvent(frm)

        Logger.EndLog(Me.GetType.Name, "ChangeNinushiEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(閉じる)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMD040F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Tabチェンジのイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub Changetab(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.TabControlEventArgs)

        Logger.StartLog(Me.GetType.Name, "Changetab")

        Me._G.SetFunctionKey(e.TabPage.Name)

        Me.ChangeTabSelectEvent(frm, e)

        Logger.EndLog(Me.GetType.Name, "Changetab")

    End Sub

    ''' <summary>
    ''' 入出荷履歴（入荷ごと）スプレッドダブルクリック時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub NyukaDoubleClick(ByRef frm As LMD040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeInOutka")

        'デフォルト処理をキャンセルします。
        If e.Row > -1 AndAlso e.ColumnHeader = False Then
            e.Cancel = True
        End If

        If e.Row.Equals(-1) = False AndAlso e.ColumnHeader = False Then
            Call Me.DoubleClickRireki(frm, e.Row, "1")
        End If

        Logger.EndLog(Me.GetType.Name, "ChangeInOutka")


    End Sub

    ''' <summary>
    ''' 入出荷履歴（在庫ごと）スプレッドダブルクリック時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub ZaikoDoubleClick(ByRef frm As LMD040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeInOutka")

        'デフォルト処理をキャンセルします。
        If e.Row > -1 AndAlso e.ColumnHeader = False Then
            e.Cancel = True
        End If

        If e.Row.Equals(-1) = False AndAlso e.ColumnHeader = False Then
            Call Me.DoubleClickRireki(frm, e.Row, "2")
        End If

        Logger.EndLog(Me.GetType.Name, "ChangeInOutka")

    End Sub

    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <param name="controlNm">コントロール名称</param>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMD040F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        Me.EnterkeyControl(frm, e, controlNm)

    End Sub

    ''' <summary>
    ''' 印刷ボタン押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub PrintBtnDown(ByRef frm As LMD040F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "Print")

        Call Me.PrintBtnDown(frm)

        Logger.StartLog(Me.GetType.Name, "Print")

    End Sub

    'ADD START 2019/8/27 依頼番号:007116,007119
    ''' <summary>
    ''' 実行ボタン押下時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub ExecutionBtnDown(ByRef frm As LMD040F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "Execution")

        Call Me.ExecutionBtnDown(frm)

        Logger.StartLog(Me.GetType.Name, "Execution")

    End Sub
    'ADD END 2019/8/27 依頼番号:007116,007119

    ''' <summary>
    ''' 現在庫スプレッドチェックボックスクリックイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub SetSprCheckBox(ByRef frm As LMD040F, ByVal e As FarPoint.Win.Spread.EditorNotifyEventArgs)

        If Me._CheckRow < 1 OrElse frm.sprGenzaiko.Sheets(0).RowCount - 1 < Me._CheckRow Then
            Me._CheckRow = e.Row
            Exit Sub
        End If

        If Me._CheckRow = e.Row Then
            Exit Sub
        End If

        frm.sprGenzaiko.SetCellValue(Me._CheckRow, _G.sprGenzaikoDef.DEF.ColNo, LMConst.FLG.OFF)
        Me._CheckRow = e.Row

    End Sub

    ''' <summary>
    ''' 現在庫スプレッドDragイベントキャンセル
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub sprDragCancelAction(ByVal frm As LMD040F)

        'ドラッグしたセル範囲取得
        Dim sel As FarPoint.Win.Spread.Model.CellRange = frm.sprGenzaiko.Sheets(0).GetSelection(0)

        '選択範囲がない場合、イベントスキップ
        If sel Is Nothing Then
            Exit Sub
        End If

        If sel.Column = -1 AndAlso sel.Row = -1 Then
            Exit Sub
        End If

        Dim colCnt As Integer = sel.ColumnCount()
        Dim rowCnt As Integer = sel.RowCount()
        Dim va As String = String.Empty

        '範囲内セルのチェックボックス設定
        If sel.Column <= LMD020C.SprColumnIndexMoveBefor.DEF AndAlso (0 < colCnt OrElse 0 < rowCnt) Then

            '編集スタート行設定
            Dim start As Integer = 1
            If 0 < sel.Row Then
                start = sel.Row
            End If

            frm.sprGenzaiko.CancelCellEditing()
            frm.sprGenzaiko.Sheets(0).SetActiveCell(0, 0)
            frm.sprGenzaiko.Sheets(0).SetActiveCell(start, 1)

        End If

    End Sub

    ''' <summary>
    ''' 個数ラジオボタンチェック時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub optKosu_CheckedChanged(ByVal frm As LMD040F, ByVal e As System.EventArgs)

        If frm.optKosu.Checked = True Then
            frm.chkNaigaiKosu.Enabled = True
        Else
            frm.chkNaigaiKosu.Enabled = False
        End If

    End Sub


    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class