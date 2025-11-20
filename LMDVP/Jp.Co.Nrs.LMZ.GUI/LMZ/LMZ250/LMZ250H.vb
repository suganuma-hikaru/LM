' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ250H : 運送会社マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMZ250ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ250H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ250V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ250G

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
        Dim frm As LMZ250F = New LMZ250F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ250V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ250G(Me, frm, Me._LMZConG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
                                         , LMZControlC.F11Pattern.ptn2)

        'タブインデックスの設定
        Call Me._LMZConG.SetTabIndex(frm.sprDetail)

        'フォーカスの設定
        Call Me._LMZConG.SetFoucus(frm.sprDetail)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ250C.TABLE_NM_IN).Rows(0)

        'スプレッド・営業所コンボボックスの初期設定
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '要望対応:1248 terakawa 2013.03.21 Start
        'マイ運送会社設定
        Call Me.SetMyUnsoco(frm)
        '要望対応:1248 terakawa 2013.03.21 End

        '検索フラグ判定
        If LMConst.FLG.ON.Equals(prmdRow("SEARCH_CS_FLG")) = True Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
            '==========================
            'WSAクラス呼出
            '==========================
            Me._S.OutDs = Me._LMZConH.LoadCallWSAAction(Me._S)

        Else
            '検索処理(キャッシュ)
            Dim ds As DataSet = New LMZ250DS()
            ds.Tables(LMZ250C.TABLE_NM_IN).ImportRow(prmdRow)
            Me._S.OutDs = Me.SelectUnsocoOutListData(frm, ds)

        End If

        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ250C.TABLE_NM_OUT)
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
            outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ250C.TABLE_NM_OUT)
            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)
        End If

        '↑ データ取得の必要があればここにコーディングする。

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
    Private Sub CloseForm(ByVal frm As LMZ250F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ250C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ250F)

        '処理開始アクション
        Call Me._LMZConH.StartAction(frm)

        '入力チェック
        If Me._V.IsInputChk() = False Then

            '終了処理
            Call Me._LMZConH.EndAction(frm)

            Exit Sub

        End If

        '検索
        Dim rtnDs As DataSet = Me.SelectList(frm)

        Dim outTbl As DataTable = rtnDs.Tables(LMZ250C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ250C.TABLE_NM_OUT)
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
    Private Sub RowSelection(ByVal frm As LMZ250F, ByVal e As Integer)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ250F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ250G.sprDetailDef.DEF.ColNo
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

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMZ250F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ250DS()
        Call SetDatasetUnsocoInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ250C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        '検索フラグ判定
        If prmdRow("SEARCH_CS_FLG").Equals(LMConst.FLG.ON) = True Then


            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

            '==========================
            'WSAクラス呼出
            '==========================
            rtnDs = Me._LMZConH.CallWSAAction(frm, frm.sprDetail, ds)

        Else

            'キャッシュテーブルからデータ抽出
            rtnDs = Me.SelectUnsocoOutListData(frm, ds)

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ250DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoOutListData(ByVal frm As LMZ250F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '運送会社名
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNSOCO_NM").ToString(), LMZControlC.ConditionPattern.all, "UNSOCO_NM"))

        '支店名
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNSOCO_BR_NM").ToString(), LMZControlC.ConditionPattern.all, "UNSOCO_BR_NM"))

        '元請区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("MOTOUKE_KB").ToString(), LMZControlC.ConditionPattern.equal, "MOTOUKE_KB"))

        '運送会社コード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNSOCO_CD").ToString(), LMZControlC.ConditionPattern.pre, "UNSOCO_CD"))

        '運送会社支店コード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("UNSOCO_BR_CD").ToString(), LMZControlC.ConditionPattern.pre, "UNSOCO_BR_CD"))

        '要望対応:1248 terakawa 2013.03.21 Start
        If drow("MY_UNSOCO_YN").ToString() = LMZ250C.MY_UNSOCO_YN_TRUE Then
            'マイ運送会社区分
            andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("MY_UNSOCO_YN").ToString(), LMZControlC.ConditionPattern.equal, "MY_UNSOCO_YN"))
        End If
        '要望対応:1248 terakawa 2013.03.21 End

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "UNSOCO_CD,UNSOCO_BR_CD"

        Return Me._LMZConH.SelectListData(ds, LMZ250C.TABLE_NM_OUT, LMConst.CacheTBL.UNSOCO, andstr.ToString(), sort)

    End Function

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からマイ運送会社データ件数を抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ250DS</param>
    ''' <returns>TRUE/FALSE</returns>
    ''' <remarks></remarks>
    Private Function SelectMyUnsocoCount(ByVal frm As LMZ250F, ByVal ds As DataSet) As Boolean

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        'マイ運送会社区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("MY_UNSOCO_YN").ToString(), LMZControlC.ConditionPattern.equal, "MY_UNSOCO_YN"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim locationRows As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(andstr.ToString())

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
    Private Sub SelectionRowToFrm(ByVal frm As LMZ250F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ250G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ250DS(), LMZ250C.TABLE_NM_OUT, rowI)

        End If

    End Sub

    '要望対応:1248 terakawa 2013.03.21 Start
    ''' <summary>
    ''' マイ運送会社設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetMyUnsoco(ByVal frm As LMZ250F)

        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ250C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        Dim getMyUnsocoFlg As Boolean = False

        '以下の項目が指定されていた場合、全件オプションにチェック
        If String.IsNullOrEmpty(prmdRow.Item("UNSOCO_NM").ToString()) = False OrElse _
           String.IsNullOrEmpty(prmdRow.Item("UNSOCO_BR_NM").ToString()) = False OrElse _
           String.IsNullOrEmpty(prmdRow.Item("UNSOCO_CD").ToString()) = False OrElse _
           String.IsNullOrEmpty(prmdRow.Item("UNSOCO_BR_CD").ToString()) = False Then

            '全件オプションにチェック
            _G.SetMyUnsocoOptionButton(frm, False)

            '初期検索用パラメータにマイ運送会社区分をセット
            prmdRow.Item("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_FALSE

            Exit Sub
        Else
            '存在チェックのため、マイ運送会社区分をセット
            prmdRow.Item("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_TRUE


            '検索フラグ判定
            If prmdRow("SEARCH_CS_FLG").Equals(LMConst.FLG.ON) = True Then

                'ログ出力
                MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectMyUnsoco")

                '==========================
                'WSAクラス呼出
                '==========================
                getMyUnsocoFlg = Me._LMZConH.LoadMyWSAAction(Me._S)
            Else
                'キャッシュテーブルからデータ抽出
                getMyUnsocoFlg = Me.SelectMyUnsocoCount(frm, Me._S.PrmDs)
            End If


            If getMyUnsocoFlg = True Then
                'マイ運送会社オプションにチェック
                _G.SetMyUnsocoOptionButton(frm, True)

                '初期検索用パラメータにマイ運送会社区分をセット
                prmdRow.Item("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_TRUE
            Else
                '全件オプションにチェック
                _G.SetMyUnsocoOptionButton(frm, False)

                '初期検索用パラメータにマイ運送会社区分をセット
                prmdRow.Item("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_FALSE
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
    Private Sub SetDatasetUnsocoInData(ByVal frm As LMZ250F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ250C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue

        With frm.sprDetail.ActiveSheet
            drow("UNSOCO_NM") = .Cells(0, LMZ250G.sprDetailDef.UNSOCO_NM.ColNo).Text.Trim()
            drow("UNSOCO_BR_NM") = .Cells(0, LMZ250G.sprDetailDef.UNSOCO_BR_NM.ColNo).Text.Trim()
            drow("MOTOUKE_KB") = .Cells(0, LMZ250G.sprDetailDef.MOTOUKE_KB.ColNo).Text.Trim()
            drow("UNSOCO_CD") = .Cells(0, LMZ250G.sprDetailDef.UNSOCO_CD.ColNo).Text.Trim()
            drow("UNSOCO_BR_CD") = .Cells(0, LMZ250G.sprDetailDef.UNSOCO_BR_CD.ColNo).Text.Trim()

            '要望対応:1248 terakawa 2013.03.21 Start
            If frm.optAll.Checked = True Then
                drow("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_FALSE
            ElseIf frm.optMyUnsoco.Checked = True Then
                drow("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_TRUE
            Else
                drow("MY_UNSOCO_YN") = LMZ250C.MY_UNSOCO_YN_FALSE
            End If
            '要望対応:1248 terakawa 2013.03.21 End

        End With

        ds.Tables(LMZ250C.TABLE_NM_IN).Rows.Add(drow)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMZ250F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMZ250F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMZ250F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByRef frm As LMZ250F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ250F, ByVal e As Integer)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        '選択処理
        Call Me.RowSelection(frm, e)

        '選択行の取得に成功時自フォームを閉じる
        If Me._S.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class