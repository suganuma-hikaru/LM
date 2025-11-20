' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ021H : 商品マスタ（在庫）照会
'  作  成  者       :  Annen
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMZ021ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMZ021H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMZ021V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMZ021G
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
    ''' 選択した言語を格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LangFlg As String


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
        Dim frm As LMZ021F = New LMZ021F(Me)

        Dim popLL As LMFormPopLL = DirectCast(frm, LMFormPopLL)

        'Validate共通クラスの設定
        Me._LMZConV = New LMZControlV(popLL, Me)

        'Hnadler共通クラスの設定
        Me._LMZConH = New LMZControlH(popLL, MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMZConG = New LMZControlG(frm)

        'Validateクラスの設定
        Me._V = New LMZ021V(Me, frm, Me._LMZConV)

        'Gamenクラスの設定
        Me._G = New LMZ021G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '選んだ言語の取得
        _LangFlg = MessageManager.MessageLanguage

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
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ021C.TABLE_NM_IN).Rows(0)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread(prmdRow)

        '↓ データ取得の必要があればここにコーディングする。

        '検索処理(キャッシュ)
        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")
        '==========================
        'WSAクラス呼出
        '==========================
        Me._S.OutDs = Me._LMZConH.LoadCallWSAAction(Me._S)

        Dim outTbl As DataTable = Me._S.OutDs.Tables(LMZ021C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        If count = 1 AndAlso prm.SkipFlg <> True Then
            '初期検索結果＝1件 かつ 画面表示フラグがTrue以外の場合画面を開かない
            prm.ParamDataSet = Me._S.OutDs
            'リターンフラグを立てる
            prm.ReturnFlg = True
            LMFormNavigate.Revoke(Me)
            Exit Sub
        End If

        '荷主コードの取得
        Dim CustCdL As String = prmdRow("CUST_CD_L").ToString()
        Dim CustCdM As String = prmdRow("CUST_CD_M").ToString()

        '画面ヘッダー部の設定(荷主キャッシュ)
        Dim headerRow As DataRow() = Me._LMZConG.SelectCustListDataRow(CustCdL, CustCdM)

        '荷主キャッシュから取得件数が0件のとき処理終了
        If headerRow.Length < 1 Then
            '2015.11.02 tsunehira add Start
            '英語化対応
            Select Case _LangFlg
                Case LMZ021C.MESSEGE_LANGUAGE_JAPANESE
                    Call Me._LMZConV.SetMstErrMessage("荷主マスタ", "荷主コード")
                Case LMZ021C.MESSEGE_LANGUAGE_ENGLISH
                    Call Me._LMZConV.SetMstErrMessage("Custmer Master", "Custmer Code")
            End Select
            '2015.11.02 tsunehira add End
            'Call Me._LMZConV.SetMstErrMessage("荷主マスタ", "荷主コード")
            'ロック
            Call Me._G.LockControl(True)

        Else
            'ヘッダー部にデータをセット
            Call Me._G.CustHeaderDataSet(headerRow(0))
            '初期処理ロードメッセージチェック
            If Me._LMZConH.LoadMsgChk(frm, prmdRow, count) = True Then
                outTbl = Me._LMZConH.SetLoadData(Me._S, frm, outTbl, LMZ021C.TABLE_NM_OUT)
                '取得データをSPREADに表示
                Call Me._G.SetSpread(outTbl)
            End If
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
    Private Sub CloseForm(ByVal frm As LMZ021F)

        Call Me._LMZConH.CloseForm(Me._S, LMZ021C.TABLE_NM_OUT)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMZ021F)

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

        Dim outTbl As DataTable = rtnDs.Tables(LMZ021C.TABLE_NM_OUT)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMZConH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            'セッションクラスのOUTテーブルに設定
            Call Me._LMZConH.SetOutds(Me._S, outTbl, count, LMZ021C.TABLE_NM_OUT)

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
    Private Function SelectList(ByVal frm As LMZ021F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMZ021DS()
        Call Me.SetDatasetGoodsInData(frm, ds)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Dim prmdRow As DataRow = Me._S.PrmDs.Tables(LMZ021C.TABLE_NM_IN).Rows(0)

        Dim rtnDs As DataSet = Nothing

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMZConH.CallWSAAction(frm, frm.sprDetail, ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMZ021F, ByVal e As Integer)

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, e)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMZ021F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMZ021G.sprDetailDef.DEF.ColNo
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
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMZ021F, ByVal rowIdx As Integer)

        If 0 < rowIdx Then

            'データテーブルから選択行を抽出
            Dim rowI As Integer = Convert.ToInt32(frm.sprDetail.ActiveSheet.Cells(rowIdx, LMZ021G.sprDetailDef.ROW_INDEX.ColNo).Value.ToString())

            Call Me._LMZConH.SetRtnParam(Me._S, New LMZ021DS(), LMZ021C.TABLE_NM_OUT, rowI)

        End If

    End Sub

#Region "DataSet設定"


    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetGoodsInData(ByVal frm As LMZ021F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMZ021C.TABLE_NM_IN).NewRow

        drow("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue
        drow("CUST_CD_L") = frm.lblCustCdL.TextValue
        drow("CUST_CD_M") = frm.lblCustCdM.TextValue

        With frm.sprDetail.ActiveSheet

            drow("GOODS_NM_1") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.GOODS_NM_1.ColNo)).Replace("[", "[[]").Replace("%", "[%]").Replace("_", "[_]") '要望番号:1823（ロットＮｏの検索条件に%を含んだ場合、置換される値がおかしい）対応　 2013/02/05 本明
            drow("GOODS_CD_CUST") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.GOODS_CD_CUST.ColNo))
            drow("LOT_NO") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.LOT_NO.ColNo))
            If .Cells(0, LMZ021G.sprDetailDef.ALLOC_CAN_NB.ColNo).Value Is Nothing Then
                drow("ALLOC_CAN_NB") = String.Empty
            Else
                drow("ALLOC_CAN_NB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.ALLOC_CAN_NB.ColNo))
            End If
            If IsNumeric(Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.STD_IRIME_NB.ColNo))) = False _
                 OrElse Convert.ToDouble(Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.STD_IRIME_NB.ColNo))) = 0 Then
                drow("IRIME") = String.Empty
            Else
                drow("IRIME") = Format(Convert.ToDouble(Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.STD_IRIME_NB.ColNo))), "0.000")
            End If
            drow("IRIME_UT") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.STD_IRIME_UT.ColNo))
            drow("NB_UT") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.NB_UT_NM.ColNo))
            drow("SEARCH_KEY_1") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.SEARCH_KEY_1.ColNo))
            drow("SEARCH_KEY_2") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.SEARCH_KEY_2.ColNo))
            drow("ONDO_KB") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.ONDO_KB_NM.ColNo))
            drow("SHOBO_CD") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.SHOBO_CD.ColNo))
            drow("CUST_NM_S") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.CUST_NM_S.ColNo))
            drow("CUST_NM_SS") = Me._LMZConV.GetCellValue(.Cells(0, LMZ021G.sprDetailDef.CUST_NM_SS.ColNo))

        End With

        ds.Tables(LMZ021C.TABLE_NM_IN).Rows.Add(drow)


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
    Friend Sub FunctionKey9Press(ByRef frm As LMZ021F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey11Press(ByRef frm As LMZ021F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByRef frm As LMZ021F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()


    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMZ021F, ByVal e As FormClosingEventArgs)

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
    Friend Sub sprCellDoubleClick(ByRef frm As LMZ021F, ByVal e As Integer)

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