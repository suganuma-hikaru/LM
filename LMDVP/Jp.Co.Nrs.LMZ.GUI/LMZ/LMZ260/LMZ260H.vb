' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ260H : 荷主マスタ照会（大・中）
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMZ260ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ260H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ260V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ260G

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
        Dim frm As LMZ260F = New LMZ260F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ260V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ260G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'ファンクションキーの設定
        'START YANAI 要望番号604
        'Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
        '                                 , LMZControlC.F11Pattern.ptn2)
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn4 _
                                         , LMZControlC.F11Pattern.ptn2)
        'END YANAI 要望番号604

        'タブインデックスの設定
        Call Me._LMZConG.SetTabIndex(frm.sprDetail)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ260C.TABLE_NM_IN).Rows(0)

        'スプレッド・営業所コンボボックスの初期設定
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

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
            Dim ds As DataSet = New LMZ260DS()
            ds.Tables(LMZ260C.TABLE_NM_IN).ImportRow(prmdRow)
            Me._S.OutDs = Me.SelectCustOutListData(frm, ds)

        End If

        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ260C.TABLE_NM_OUT)
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
            outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ260C.TABLE_NM_OUT)
            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)
        End If

        '↑ データ取得の必要があればここにコーディングする。

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォームの表示
        frm.ShowDialog()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        ' Call Me._LMZConG.SetFoucus(frm.sprDetail)
        Call Me.SetFoucus(frm)

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(ByVal frm As LMZ260F)

        'frm.sprDetail.Sheets(0).SetActiveCell(0, LMZ260C.SprColumnIndex.CUST_CD_L)
        frm.sprDetail.Sheets(0).SetActiveCell(0, LMZ260C.SprColumnIndex.DEF)

    End Sub


    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMZ260F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ260C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ260F)

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

        Dim outTbl As DataTable = rtnDs.Tables(LMZ260C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ260C.TABLE_NM_OUT)
            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})

        End If

        '終了処理
        Call Me._LMZConH.EndAction(frm)

        'ファンクションキーの設定
        'START YANAI 要望番号604
        'Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn1 _
        '                                 , LMZControlC.F11Pattern.ptn2)
        Call Me._LMZConG.SetFunctionKey(frm, LMZControlC.F10Pattern.ptn4 _
                                         , LMZControlC.F11Pattern.ptn2)
        'END YANAI 要望番号604

        Call Me.SetFoucus(frm)

    End Sub

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ260F, ByVal e As Integer)


#If False Then '商品マスタから荷主マスタ参照へ遷移した場合に、ダブルクリックで荷主を選択すると荷主CD(S,SS)が消える問題に対応 Changed 20151110  INOUE 
        '返却パラメータ設定
        'START YANAI 要望番号604
        'Call Me.SelectionRowToFrm(frm, e)
        Call Me.SelectionRowToFrm(frm, e, False)
        'END YANAI 要望番号604
#Else
        Dim rowLMZ260IN As DataRow = Me._S.PrmDs.Tables(LMZ260C.TABLE_NM_IN).Rows(0)

        Dim mode As Object = rowLMZ260IN.Item("DC_ROW_SELECT_MODE")

        '荷主コードのクリア処理を無効化するか確認
        Dim disabledClearCustSAndSS As Boolean _
            = Not (mode Is Nothing) AndAlso Convert.ToInt16(mode) = LMZ260C.DobleClickRowSelectMode.DISABLED_CLEAR_CUST_CD

        Call Me.SelectionRowToFrm(frm, e, disabledClearCustSAndSS)
#End If



    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ260F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ260G.sprDetailDef.DEF.ColNo
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
        'START YANAI 要望番号604
        'Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)))
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)), True)
        'END YANAI 要望番号604

    End Sub

    'START YANAI 要望番号604
    ''' <summary>
    ''' 選択処理（大中設定ボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowLMSelect(ByVal frm As LMZ260F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ260G.sprDetailDef.DEF.ColNo
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
        Call Me.SelectionRowToFrm(frm, Convert.ToInt32(arr(0)), False)

    End Sub
    'END YANAI 要望番号604

#End Region 'イベント定義(一覧)

#Region "個別メソッド"

    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMZ260F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ260DS()
        Call SetDatasetCustInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ260C.TABLE_NM_IN).Rows(0)

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
            rtnDs = Me.SelectCustOutListData(frm, ds)

        End If

        Return rtnDs

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ260DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectCustOutListData(ByVal frm As LMZ260F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))
        
        '荷主名(大)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_NM_L").ToString(), LMZControlC.ConditionPattern.all, "CUST_NM_L"))

        '荷主名(中)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_NM_M").ToString(), LMZControlC.ConditionPattern.all, "CUST_NM_M"))

        '荷主名(小)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_NM_S").ToString(), LMZControlC.ConditionPattern.all, "CUST_NM_S"))

        '荷主名(極小)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_NM_SS").ToString(), LMZControlC.ConditionPattern.all, "CUST_NM_SS"))

        '荷主コード(大)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_L").ToString(), LMZControlC.ConditionPattern.pre, "CUST_CD_L"))
        
        '荷主コード(中)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_M").ToString(), LMZControlC.ConditionPattern.pre, "CUST_CD_M"))

        '荷主コード(小)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_S").ToString(), LMZControlC.ConditionPattern.pre, "CUST_CD_S"))

        '荷主コード(極小)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("CUST_CD_SS").ToString(), LMZControlC.ConditionPattern.pre, "CUST_CD_SS"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "CUST_CD_L,CUST_CD_M,CUST_CD_S,CUST_CD_SS"
     
        Return Me._LMZConH.SelectListData(ds, LMZ260C.TABLE_NM_OUT, LMConst.CacheTBL.CUST, andstr.ToString(), sort)

    End Function

    'START YANAI 要望番号604
    '''' <summary>
    '''' 選択行をフォームパラメータに戻す
    '''' </summary>
    '''' <param name="rowIdx">選択行インデクス</param>
    '''' <remarks></remarks>
    'Private Sub SelectionRowToFrm(ByVal frm As LMZ260F, ByVal rowIdx As Integer)
    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ260F, ByVal rowIdx As Integer, ByVal custRtnFlg As Boolean)
        'END YANAI 要望番号604

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ260G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ260DS(), LMZ260C.TABLE_NM_OUT, rowI)

            If custRtnFlg = False Then
                '戻り値から荷主(小)、荷主(極小)をクリアする
                Me._S.FormPrm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_S") = String.Empty
                Me._S.FormPrm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_SS") = String.Empty
                Me._S.FormPrm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_S") = String.Empty
                Me._S.FormPrm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_SS") = String.Empty
            End If

        End If

    End Sub

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetCustInData(ByVal frm As LMZ260F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ260C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue

        With frm.sprDetail.ActiveSheet
            drow("CUST_CD_L") = .Cells(0, LMZ260G.sprDetailDef.CUST_CD_L.ColNo).Text.Trim()
            drow("CUST_CD_M") = .Cells(0, LMZ260G.sprDetailDef.CUST_CD_M.ColNo).Text.Trim()
            drow("CUST_CD_S") = .Cells(0, LMZ260G.sprDetailDef.CUST_CD_S.ColNo).Text.Trim()
            drow("CUST_CD_SS") = .Cells(0, LMZ260G.sprDetailDef.CUST_CD_SS.ColNo).Text.Trim()
            drow("CUST_NM_L") = .Cells(0, LMZ260G.sprDetailDef.CUST_NM_L.ColNo).Text.Trim()
            drow("CUST_NM_M") = .Cells(0, LMZ260G.sprDetailDef.CUST_NM_M.ColNo).Text.Trim()
            drow("CUST_NM_S") = .Cells(0, LMZ260G.sprDetailDef.CUST_NM_S.ColNo).Text.Trim
            drow("CUST_NM_SS") = .Cells(0, LMZ260G.sprDetailDef.CUST_NM_SS.ColNo).Text.Trim

        End With

        ds.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(drow)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMZ260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "selectdata")

        '検索処理
        Call Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "selectdata")

    End Sub

    'START YANAI 要望番号604
    ''' <summary>
    ''' F10押下時処理呼び出し(大中設定処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMZ260F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "LMSetbutton")

        '選択処理
        Call Me.RowLMSelect(frm)

        '選択行の取得に成功時自フォームを閉じる
        If Me._S.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, "LMSetbutton")

    End Sub
    'END YANAI 要望番号604

    ''' <summary>
    ''' F11押下時処理呼び出し(OK処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMZ260F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMZ260F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByRef frm As LMZ260F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ260F, ByVal e As Integer)

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