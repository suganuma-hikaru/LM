' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ120H : 棟・室ゾーン照会
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMZ120ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ120H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ120V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ120G
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
        Dim frm As LMZ120F = New LMZ120F(Me)

        Dim popL As LMFormPopL = DirectCast(frm, LMFormPopL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ120V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ120G(Me, frm, Me._LMZConG)

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
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ120C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '検索処理(キャッシュ)
        Dim ds As DataSet = New LMZ120DS()
        ds.Tables(LMZ120C.TABLE_NM_IN).ImportRow(prmdRow)
        Me._S.OutDs = Me.SelectTousituZoneOutListData(frm, ds)

        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ120C.TABLE_NM_OUT)
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
            outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ120C.TABLE_NM_OUT)
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
    Private Sub CloseForm(ByVal frm As LMZ120F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ120C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ120F)

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

        Dim outTbl As DataTable = rtnDs.Tables(LMZ120C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ120C.TABLE_NM_OUT)

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
    Private Function SelectList(ByVal frm As LMZ120F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ120DS()
        Call Me.SetDatasetGoodsInData(frm, ds)

        'キャッシュテーブルからデータ抽出
        Return Me.SelectTousituZoneOutListData(frm, ds)

    End Function

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ120F, ByVal e As Integer)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ120F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ120G.sprDetailDef.DEF.ColNo
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
    ''' 商品マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMZ120DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectTousituZoneOutListData(ByVal frm As LMZ120F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMZ120C.TABLE_NM_IN)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMZControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '倉庫コード
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("WH_CD").ToString(), LMZControlC.ConditionPattern.equal, "WH_CD"))

        '棟
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("TOU_NO").ToString(), LMZControlC.ConditionPattern.pre, "TOU_NO"))

        '室
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("SITU_NO").ToString(), LMZControlC.ConditionPattern.pre, "SITU_NO"))

        '棟室名
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("TOU_SITU_NM").ToString(), LMZControlC.ConditionPattern.all, "TOU_SITU_NM"))

        '保税(棟室)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("HOZEI_KB").ToString(), LMZControlC.ConditionPattern.equal, "HOZEI_KB"))

        '消防
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("SHOBO_YN").ToString(), LMZControlC.ConditionPattern.equal, "SHOBO_YN"))

        'ZONE
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("ZONE_CD").ToString(), LMZControlC.ConditionPattern.pre, "ZONE_CD"))

        'ZONE名
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("ZONE_NM").ToString(), LMZControlC.ConditionPattern.all, "ZONE_NM"))

        '保税(ZONE)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("ZONE_HOZEI_KB").ToString(), LMZControlC.ConditionPattern.equal, "ZONE_HOZEI_KB"))

        '温度管理(ZONE)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("ZONE_ONDO_CTL_KB").ToString(), LMZControlC.ConditionPattern.equal, "ZONE_ONDO_CTL_KB"))

        '温度管理中(ZONE)
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("ZONE_ONDO_CTL_FLG").ToString(), LMZControlC.ConditionPattern.equal, "ZONE_ONDO_CTL_FLG"))

        '薬事
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("YAKUJI_YN").ToString(), LMZControlC.ConditionPattern.equal, "YAKUJI_YN"))

        '毒劇
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("DOKU_YN").ToString(), LMZControlC.ConditionPattern.equal, "DOKU_YN"))

        'ガス
        andstr.Append(Me._LMZConH.SetWhereData(andstr, drow("GASS_YN").ToString(), LMZControlC.ConditionPattern.equal, "GASS_YN"))

        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "TOU_NO,SITU_NO"

        Return Me._LMZConH.SelectListData(ds, LMZ120C.TABLE_NM_OUT, LMConst.CacheTBL.TOU_SITU_ZONE, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ120F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ120G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ120DS(), LMZ120C.TABLE_NM_OUT, rowI)

        End If

    End Sub

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetGoodsInData(ByVal frm As LMZ120F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ120C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("WH_CD") = frm.cmbSoko.SelectedValue

        With frm.sprDetail.ActiveSheet

            drow("TOU_NO") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.TOU_NO.ColNo))
            drow("SITU_NO") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.SITU_NO.ColNo))
            drow("TOU_SITU_NM") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.TOU_SITU_NM.ColNo))
            drow("HOZEI_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.HOZEI_KB_NM.ColNo))
            drow("SHOBO_YN") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.SHOBO_YN.ColNo))
            drow("ZONE_CD") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.ZONE_CD.ColNo))
            drow("ZONE_NM") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.ZONE_NM.ColNo))
            drow("ZONE_HOZEI_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.ZONE_HOZEI_KB_NM.ColNo))
            drow("ZONE_ONDO_CTL_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_KB_NM.ColNo))
            drow("ZONE_ONDO_CTL_FLG") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_FLG_NM.ColNo))
            drow("YAKUJI_YN") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.YAKUJI_YN_NM.ColNo))
            drow("DOKU_YN") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.DOKU_YN_NM.ColNo))
            drow("GASS_YN") = Me._LMZConV.GetCellValue(.Cells(0, LMZ120G.sprDetailDef.GASS_YN_NM.ColNo))

        End With

        ds.Tables(LMZ120C.TABLE_NM_IN).Rows.Add(drow)

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
    Friend Sub FunctionKey9Press(ByRef frm As LMZ120F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMZ120F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMZ120F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMZ120F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ120F, ByVal e As Integer)

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