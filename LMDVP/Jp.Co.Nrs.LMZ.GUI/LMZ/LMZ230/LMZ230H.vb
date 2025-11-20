' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ230H : 横持ちタリフマスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMZ230ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ230H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ230V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ230G

    ''' <summary>
    ''' Sessionクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _S As LMZControlS

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG

    ''' <summary>
    ''' ハンドラクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConH As LMZControlH

    ''' <summary>
    ''' Validateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConV As LMZControlV

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
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

        'セッションクラスのインスタンス生成
        Me._S = New LMZControlS()

        'パラメータオブジェクトを退避
        Me._S.FormPrm = prm

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        '画面間データを取得する
        Me._S.PrmDs = Me._S.FormPrm.ParamDataSet

        'フォームの作成
        Dim frm As LMZ230F = New LMZ230F(Me)

        Dim popL As LMFormpopL = DirectCast(frm, LMFormpopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Gamenクラスの設定
        Me._G = New LMZ230G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMZ230V(Me, frm, Me._LMZConV, Me._LMZConG, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'Enter押下イベント設定
        Call Me._LMZConH.SetEnterEvent(frm)

        'ファンクションキーの設定
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
                                         , LMZControlC.F11Pattern.ptn2)

        'タブインデックスの設定
        Call Me._LMZConG.SetTabIndex(frm.sprDetail)

        'フォーカスの設定
        Call Me._LMZConG.SetFoucus(frm.sprDetail)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ230C.TABLE_NM_IN).Rows(0)

        'スプレッド・営業所コンボボックスの初期設定
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        'START YANAI 要望番号1193 運賃タリフセットGRPのキャッシュを削除し、POP画面はDBからデータを取得する。
        prmdRow("SEARCH_CS_FLG") = LMConst.FLG.ON
        'END YANAI 要望番号1193 運賃タリフセットGRPのキャッシュを削除し、POP画面はDBからデータを取得する。

        '要望対応:1248 terakawa 2013.03.21 Start
        'マイ運送会社設定
        Call Me.SetMyUnchinTariff(frm)
        '要望対応:1248 terakawa 2013.03.21 End

        '検索フラグ判定
        If LMConst.FLG.ON.Equals(prmdRow("SEARCH_CS_FLG")) = True Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
            '==========================
            'WSAクラス呼出
            '==========================
            Me._S.OutDs = Me._LMZConH.LoadCallWSAAction(Me._S)
            If Me.IsErrorMessageExist() = True Then
                MyBase.ShowMessage(frm)
                '画面表示
                Call Me.FrmShow(frm)
                Exit Sub
            End If

            '荷主情報
            Dim custTbl As DataTable = Me._S.OutDs.Tables(LMZ230C.TABLE_NM_CUST)
            If 0 < custTbl.Rows.Count Then
                Call Me._G.CustHeaderDataSet(custTbl.Rows(0))
            End If

        Else
            '検索処理(キャッシュ)
            Dim ds As DataSet = New LMZ230DS()
            ds.Tables(LMZ230C.TABLE_NM_IN).ImportRow(prmdRow)
            '荷主コードの有無
            If String.IsNullOrEmpty(prmdRow("CUST_CD_L").ToString()) = True Then
                Me._S.OutDs = Me.SelectUnchinTariffOutListData(frm, ds)
            Else
                Me._S.OutDs = Me.SelectUnchinTariffSetOutListData(frm, ds)
                '初期検索結果件数が1件以外
                If Me._S.OutDs.Tables(LMZ230C.TABLE_NM_OUT).Rows.Count <> 1 Then
                    '画面ヘッダー部セット
                    If Me._V.HeaderExist() = False Then
                        '画面表示
                        Call Me.FrmShow(frm)
                        Exit Sub
                    End If
                End If
            End If
        End If


        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ230C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        If count = 1 AndAlso prm.SkipFlg <> True Then
            '初期検索結果＝1件 かつ 画面表示フラグがTrue以外の場合画面を開かない
            prm.ParamDataSet = Me._S.OutDs
            'リターンフラグを立てる
            prm.ReturnFlg = True
            LMFormNavigate.Revoke(Me)
            Exit Sub
        End If

        If LMConst.FLG.ON.Equals(prmdRow("SEARCH_CS_FLG")) = True Then
            count = Me._S.Cnt
        End If

        If Me._LMZConH.LoadMsgChk(frm, prmdRow, count) = True Then
            outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ230C.TABLE_NM_OUT)

            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)
        End If

        '↑ データ取得の必要があればここにコーディングする。

        '画面表示
        Call Me.FrmShow(frm)

    End Sub

    ''' <summary>
    ''' フォームを表示する。
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FrmShow(ByVal frm As LMZ230F)

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMZ230F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ230C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ230F)

        '処理開始アクション
        Call Me._LMZConH.StartAction(frm)

        'START YANAI 要望番号1193 運賃タリフセットGRPのキャッシュを削除し、POP画面はDBからデータを取得する。
        Me._S.PrmDs.Tables(LMZ230C.TABLE_NM_IN).Rows(0)("SEARCH_CS_FLG") = LMConst.FLG.ON
        'END YANAI 要望番号1193 運賃タリフセットGRPのキャッシュを削除し、POP画面はDBからデータを取得する。

        '入力チェック
        If Me._V.IsInputChk(Me._S.PrmDs.Tables(LMZ230C.TABLE_NM_IN).Rows(0)("SEARCH_CS_FLG").ToString()) = False Then

            '終了処理
            Call Me._LMZConH.EndAction(frm)

            Exit Sub

        End If

        '検索
        Dim rtnDs As DataSet = Me.SelectList(frm)

        Dim outTbl As DataTable = rtnDs.Tables(LMZ230C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ230C.TABLE_NM_OUT)
            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})

        End If

        '終了処理
        Call Me._LMZConH.EndAction(frm)

        'ファンクションキーの設定
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
                                        , LMZControlC.F11Pattern.ptn2)

        Call Me._LMZConG.SetFoucus(frm.sprDetail)


    End Sub

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ230F, ByVal e As Integer)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ230F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ230G.sprDetailDef.DEF.ColNo
        Dim arr As ArrayList = Me._LMZConV.SprSelectCount(frm.sprDetail, defNo)


        '単一選択チェック
        If Me._LMZConV.IsSelectOneChk(arr.Count) = False Then
            Exit Sub
        End If

        '未選択チェック
        If Me._LMZConV.IsSelectChk(arr.Count) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)))

    End Sub


    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMZ230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm)

        If rtnResult = False Then
            Exit Sub
        End If

        'フォーカス移動処理
        Call Me._LMZConH.NextFocusedControl(frm)

    End Sub

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMZ230F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ230DS()
        Call SetDatasetTariffInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ230C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        'START YANAI 要望番号1193 運賃タリフセットGRPのキャッシュを削除し、POP画面はDBからデータを取得する。
        prmdRow("SEARCH_CS_FLG") = LMConst.FLG.ON
        'END YANAI 要望番号1193 運賃タリフセットGRPのキャッシュを削除し、POP画面はDBからデータを取得する。

        '検索フラグ判定
        If prmdRow("SEARCH_CS_FLG").Equals(LMConst.FLG.ON) = True Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

            '==========================
            'WSAクラス呼出
            '==========================
            rtnDs = Me._LMZConH.CallWSAAction(frm, frm.sprDetail, ds)
            '存在チェック
            If Me.IsErrorMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Return rtnDs
            End If
            '荷主情報
            Dim custTbl As DataTable = rtnDs.Tables(LMZ230C.TABLE_NM_CUST)
            If 0 < custTbl.Rows.Count Then
                Call Me._G.CustHeaderDataSet(custTbl.Rows(0))
            Else
                Call Me._G.CustHeaderClear()
            End If

        Else
            'キャッシュ検索
            '荷主コードの有無チェック
            If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = True Then
                '荷主コードが空なので荷主名をクリアする
                Call Me._G.CustHeaderClear()
                'キャッシュテーブルからデータ抽出
                rtnDs = Me.SelectUnchinTariffOutListData(frm, ds)
            Else
                rtnDs = Me.SelectUnchinTariffSetOutListData(frm, ds)
            End If

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ230DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffOutListData(ByVal frm As LMZ230F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        'タリフコード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNCHIN_TARIFF_CD").ToString(), LMZControlC.ConditionPattern.pre, "UNCHIN_TARIFF_CD"))

        'タリフリマーク
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNCHIN_TARIFF_REM").ToString(), LMZControlC.ConditionPattern.all, "UNCHIN_TARIFF_REM"))

        'タリフタイプ
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("TABLE_TP").ToString(), LMZControlC.ConditionPattern.equal, "TABLE_TP"))

        '基準日
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("STR_DATE").ToString(), LMZControlC.ConditionPattern.less, "STR_DATE"))

        '要望対応:1248 terakawa 2013.03.21 Start
        If drow("MY_UNCHIN_TARIFF_YN").ToString() = LMZ250C.MY_UNSOCO_YN_TRUE Then
            'マイ運賃タリフ区分
            andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("MY_UNCHIN_TARIFF_YN").ToString(), LMZControlC.ConditionPattern.equal, "MY_UNCHIN_TARIFF_YN"))
        End If
        '要望対応:1248 terakawa 2013.03.21 End


        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "TABLE_TP,UNCHIN_TARIFF_CD"

        Return Me._LMZConH.SelectListData(ds, LMZ230C.TABLE_NM_OUT, LMConst.CacheTBL.UNCHIN_TARIFF, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ230DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffSetOutListData(ByVal frm As LMZ230F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        'タリフ区分 = "00"固定
        andstr.Append(Me._LMZConH.SetWhereData(andstr, LMZControlC.KBNCD0, LMZControlC.ConditionPattern.equal, "TARIFF_KBN"))

        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '荷主コード(大)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_L").ToString(), LMZControlC.ConditionPattern.equal, "CUST_CD_L"))

        '荷主コード(中)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_M").ToString(), LMZControlC.ConditionPattern.equal, "CUST_CD_M"))

        'タリフコード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNCHIN_TARIFF_CD").ToString(), LMZControlC.ConditionPattern.pre, "UNCHIN_TARIFF_CD"))

        'タリフリマーク
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNCHIN_TARIFF_REM").ToString(), LMZControlC.ConditionPattern.all, "UNCHIN_TARIFF_REM"))

        'タリフタイプ
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("TABLE_TP").ToString(), LMZControlC.ConditionPattern.equal, "TABLE_TP"))

        '基準日
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("STR_DATE").ToString(), LMZControlC.ConditionPattern.less, "STR_DATE"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "TABLE_TP,UNCHIN_TARIFF_CD"

        'Return Me._LMZConH.SelectListData(ds, LMZ230C.TABLE_NM_OUT, LMConst.CacheTBL.UNCHIN_TARIFF_SET_GRP, andstr.ToString(), sort)
        Return Nothing

    End Function

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からマイ運賃タリフデータ件数を抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ230DS</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    Private Function SelectMyUnchinTariffCount(ByVal frm As LMZ230F, ByVal ds As DataSet) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        'マイ運賃タリフ区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("MY_UNCHIN_TARIFF_YN").ToString(), LMZControlC.ConditionPattern.equal, "MY_UNCHIN_TARIFF_YN"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim locationRows As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(andstr.ToString())

        If locationRows.Length > 0 Then
            Return True
        End If

        Return False

    End Function
    '要望対応:1248 terakawa 2013.03.21 End


    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ230F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ230G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ230DS(), LMZ230C.TABLE_NM_OUT, rowI)

        End If

    End Sub

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' マイ運賃タリフ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMyUnchinTariff(ByVal frm As LMZ230F)

        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ230C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        Dim getMyUnchinTariffFlg As Boolean = False

        '荷主コードが指定されていた場合、全件オプションにチェック
        If String.IsNullOrEmpty(prmdRow.Item("CUST_CD_L").ToString()) = False Then

            '全件オプションにチェック
            _G.SetMyUnchinTariffOptionButton(frm, False)

            'マイ運賃タリフオプションをロック
            _G.LockMyUnchinTariffOptionButton(frm)

            '初期検索用パラメータにマイ運賃タリフ区分をセット
            prmdRow.Item("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_FALSE

            Exit Sub

            '検索条件が指定されていた場合、全件オプションにチェック
        ElseIf String.IsNullOrEmpty(prmdRow.Item("UNCHIN_TARIFF_CD").ToString()) = False OrElse _
               String.IsNullOrEmpty(prmdRow.Item("UNCHIN_TARIFF_REM").ToString()) = False Then

            '全件オプションにチェック
            _G.SetMyUnchinTariffOptionButton(frm, False)

            '初期検索用パラメータにマイ運賃タリフ区分をセット
            prmdRow.Item("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_FALSE

            Exit Sub
        Else
            '存在チェックのため、マイ運賃タリフ区分をセット
            prmdRow.Item("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_TRUE


            '検索フラグ判定
            If prmdRow("SEARCH_CS_FLG").Equals(LMConst.FLG.ON) = True Then

                'ログ出力
                MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectMyUnchinTariff")

                '==========================
                'WSAクラス呼出
                '==========================
                getMyUnchinTariffFlg = Me._LMZConH.LoadMyWSAAction(Me._S)
            Else
                'キャッシュテーブルからデータ抽出
                getMyUnchinTariffFlg = Me.SelectMyUnchinTariffCount(frm, Me._S.PrmDs)
            End If


            If getMyUnchinTariffFlg = True Then
                'マイ運賃タリフオプションにチェック
                _G.SetMyUnchinTariffOptionButton(frm, True)

                '初期検索用パラメータにマイ運賃タリフ区分をセット
                prmdRow.Item("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_TRUE
            Else
                '全件オプションにチェック
                _G.SetMyUnchinTariffOptionButton(frm, False)

                '初期検索用パラメータにマイ運賃タリフ区分をセット
                prmdRow.Item("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_FALSE
            End If

        End If

    End Sub
    '要望対応:1248 terakawa 2013.03.21 End

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetTariffInData(ByVal frm As LMZ230F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ230C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("CUST_CD_L") = frm.txtCustCdL.TextValue
        drow("CUST_CD_M") = frm.txtCustCdM.TextValue
        drow("STR_DATE") = frm.lblStrDate.TextValue

        With frm.sprDetail.ActiveSheet

            drow("UNCHIN_TARIFF_CD") = Me._LMZConV.GetCellValue(.Cells(0, LMZ230G.sprDetailDef.UNCHIN_TARIFF_CD.ColNo))
            drow("UNCHIN_TARIFF_REM") = Me._LMZConV.GetCellValue(.Cells(0, LMZ230G.sprDetailDef.UNCHIN_TARIFF_REM.ColNo))
            drow("TABLE_TP") = Me._LMZConV.GetCellValue(.Cells(0, LMZ230G.sprDetailDef.TABLE_TP_NM.ColNo))

        End With

        '要望対応:1248 terakawa 2013.03.21 Start
        If frm.optAll.Checked = True Then
            drow("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_FALSE
        ElseIf frm.optMyUnchinTariff.Checked = True Then
            drow("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_TRUE
        Else
            drow("MY_UNCHIN_TARIFF_YN") = LMZ230C.MY_UNCHIN_TARIFF_YN_FALSE
        End If
        '要望対応:1248 terakawa 2013.03.21 End

        ds.Tables(LMZ230C.TABLE_NM_IN).Rows.Add(drow)

    End Sub


#End Region 'DataSet設定

#End Region '個別メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMZ230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "selectdata")

        '検索処理
        Call Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "selectdata")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(OK処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMZ230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "OKbutton")

        '選択処理
        Call Me.RowOkSelect(frm)

        '選択行の取得に成功時自フォームを閉じる
        If Me._S.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "OKbutton")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMZ230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "CloseButton")

        '終了処理  
        frm.Close()

        Logger.EndLog(Me.GetType.Name, "CloseButton")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMZ230F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ230F, ByVal e As Integer)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        '選択処理
        Call Me.RowSelection(frm, e)

        '選択行の取得に成功時自フォームを閉じる
        If Me._S.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub


    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMZ230F_KeyDown(ByVal frm As LMZ230F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "LMZ230F_KeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "LMZ230F_KeyDown")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class