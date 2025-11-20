'' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ190H : 請求項目マスタ照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMZ190ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ190H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ190V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ190G
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
        Dim frm As LMZ190F = New LMZ190F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ190V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ190G(Me, frm, Me._LMZConG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

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
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ190C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '検索処理(キャッシュ)
        Dim ds As DataSet = New LMZ190DS()
        ds.Tables(LMZ190C.TABLE_NM_IN).ImportRow(prmdRow)

        '国コード取得用　(営業所マスタ(キャッシュ)抽出)
        ds = Me.SelectNrsBrOutListData(ds)

        Me._S.OutDs = Me.SelectSeiqkmkOutListData(frm, ds)

        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ190C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        If count = 1 AndAlso prm.SkipFlg <> True Then
            '初期検索結果＝1件 かつ 画面表示フラグがTrue以外の場合画面を開かない
            prm.ParamDataSet = Me._S.OutDs
            'リターンフラグを立てる
            prm.ReturnFlg = True
            LMFormNavigate.Revoke(Me)
            Exit Sub
        End If

        '初期処理ロードメッセージチェック
        If Me._LMZConH.LoadMsgChk(frm, prmdRow, count) = True Then
            outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ190C.TABLE_NM_OUT)
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

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"


    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMZ190F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ190C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ190F)

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

        Dim outTbl As DataTable = rtnDs.Tables(LMZ190C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ190C.TABLE_NM_OUT)

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

#End Region 'イベント定義(一覧)

#Region "個別メソッド"


    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMZ190F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ190DS()
        Call Me.SetDatasetSeiqkmkInData(frm, ds)

        'キャッシュテーブルからデータ抽出
        Return Me.SelectSeiqkmkOutListData(frm, ds)

    End Function

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ190F, ByVal e As Integer)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ190F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ190G.sprDetailDef.DEF.ColNo
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
    ''' 営業所マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="ds">LMZ190DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectNrsBrOutListData(ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ190C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼

        '営業所コード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "NRS_BR_CD"

        Return Me._LMZConH.SelectListData(ds, LMZ190C.TABLE_NM_COUNTRY, LMConst.CacheTBL.NRS_BR, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' 請求項目マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ190DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqkmkOutListData(ByVal frm As LMZ190F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ190C.TABLE_NM_IN)

        Dim cntTbl As DataTable = ds.Tables(LMZ190C.TABLE_NM_COUNTRY)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim cntdrow As DataRow = cntTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼

        'グループコード区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("GROUP_KB").ToString(), LMZControlC.ConditionPattern.equal, "GROUP_KB"))

        '請求項目名
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("SEIQKMK_NM").ToString(), LMZControlC.ConditionPattern.all, "SEIQKMK_NM"))

        '税区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("TAX_KB").ToString(), LMZControlC.ConditionPattern.equal, "TAX_KB"))

        '備考
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("REMARK").ToString(), LMZControlC.ConditionPattern.all, "REMARK"))

        '項目コード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("SEIQKMK_CD").ToString(), LMZControlC.ConditionPattern.pre, "SEIQKMK_CD"))

        '経理科目コード区分
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("KEIRI_KB").ToString(), LMZControlC.ConditionPattern.equal, "KEIRI_KB"))

        '2014.09.12 追加START 多通貨対応
        '国コード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, cntdrow("COUNTRY_CD").ToString(), LMZControlC.ConditionPattern.equal, "COUNTRY_CD"))
        '2014.09.12 追加END 多通貨対応

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        '要望番号1808（請求項目マスタPOPの並び順変更）対応　2013/01/31 本明 Start
        'Dim sort As String = "GROUP_KB,SEIQKMK_CD"
        Dim sort As String = "GROUP_KB, SEIQKMK_NM, SEIQKMK_CD"
        '要望番号1808（請求項目マスタPOPの並び順変更）対応　2013/01/31 本明 End

        Return Me._LMZConH.SelectListData(ds, LMZ190C.TABLE_NM_OUT, LMConst.CacheTBL.SEIQKMK, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ190F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ190G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ190DS(), LMZ190C.TABLE_NM_OUT, rowI)

        End If

    End Sub

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetSeiqkmkInData(ByVal frm As LMZ190F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ190C.TABLE_NM_IN).NewRow
        '2014.09.12 追加START 多通貨対応
        Dim cntDrow As DataRow = ds.Tables(LMZ190C.TABLE_NM_COUNTRY).NewRow
        '2014.09.12 追加END 多通貨対応

        With frm.sprDetail.ActiveSheet

            '2014.09.12 追加START 多通貨対応
            cntDrow("COUNTRY_CD") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.COUNTRY_CD.ColNo))
            '2014.09.12 追加END 多通貨対応

            drow("GROUP_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.GROUP_KB_NM.ColNo))
            drow("SEIQKMK_NM") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.SEIQKMK_NM.ColNo))
            drow("TAX_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.TAX_KB_NM.ColNo))
            drow("REMARK") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.REMARK.ColNo))
            drow("SEIQKMK_CD") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.SEIQKMK_CD.ColNo))
            drow("KEIRI_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ190G.sprDetailDef.KEIRI_KB_NM.ColNo))

        End With

        ds.Tables(LMZ190C.TABLE_NM_IN).Rows.Add(drow)

        '2014.09.12 追加START 多通貨対応
        ds.Tables(LMZ190C.TABLE_NM_COUNTRY).Rows.Add(cntDrow)
        '2014.09.12 追加END 多通貨対応

    End Sub


#End Region 'DataSet設定

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMZ190F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMZ190F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMZ190F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMZ190F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ190F, ByVal e As Integer)

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