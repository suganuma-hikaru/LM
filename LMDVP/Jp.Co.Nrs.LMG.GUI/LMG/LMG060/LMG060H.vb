' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG060H : 請求印刷指示
'  作  成  者       :  [菱刈]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMG060ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG060G

    ''' <summary>
    ''' パラメータ格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrmDs As DataSet

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGconV As LMGControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGconH As LMGControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGconG As LMGControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

#End Region

#Region "Method"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        '画面間データを取得する
        Me._PrmDs = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMG060F = New LMG060F(Me)

        'Validateクラスの設定
        Me._LMGconV = New LMGControlV(Me, DirectCast(frm, Form))

        'Gクラスの設定
        Me._LMGconG = New LMGControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMGconH = New LMGControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMGconV, Me._LMGconG)

        'Validateクラスの設定
        Me._V = New LMG060V(Me, frm, Me._LMGconV, Me._LMGconG, Me._LMGconH)

        'Gamenクラスの設定
        Me._G = New LMG060G(Me, frm)

        'フォームの初期化
        MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面の入力項目/ファンクションキーの制御
        Call Me._G.UnLockedForm()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'START YANAI 要望番号582
        '運賃検索から遷移時のみ初期値設定
        Call Me._G.SetControlPrm(Me._PrmDs, MyBase.RootPGID())
        'END YANAI 要望番号582

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#Region "イベント定義(一覧)"

#Region "印刷区分値変更"

    Private Sub Print(ByVal frm As LMG060F)

        '処理開始アクション
        Call Me._LMGconH.StartAction(frm)

        '画面全ロック
        MyBase.LockedControls(frm)

        '終了メッセージ表示
        MyBase.ShowMessage(frm, "G007")

        '終了処理
        Call Me.EndAction(frm)

        'ロック制御
        Call Me._G.Locktairff(frm)

    End Sub

#End Region

#Region "マスタ参照"
    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMG060F)


        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()



        '権限チェック()
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMG060C.EventShubetsu.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMG060C.EventShubetsu.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then

            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMG060C.EventShubetsu.MASTEROPEN)

        '終了メッセージ設定
        MyBase.ShowMessage(frm, "G007")


        '処理終了アクション
        Call Me.EndAction(frm)
        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, True)



    End Sub
    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMG060F, ByVal e As System.Windows.Forms.KeyEventArgs)


        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg


        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthorityChk(LMG060C.EventShubetsu.MASTEROPEN)

        ''カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMG060C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me.NextFocusedControl(frm, eventFlg)

            Exit Sub
        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMG060C.EventShubetsu.ENTER)

        '終了メッセージ設定
        MyBase.ShowMessage(frm, "G007")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub
#Region "Pop"
    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMG060F, ByVal objNm As String, ByVal actionType As LMG060C.EventShubetsu) As Boolean

        With frm

            '処理開始アクション
            Call Me._LMGconH.StartAction(frm)
            '画面全ロック
            MyBase.LockedControls(frm)

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name


                    '荷主(大)Lコード
                    Call Me.CustPop(frm, actionType)

                Case .txtSeiqCd.Name


                    '請求先コード
                    Call Me.SetReturnSeqtoPop(frm, actionType)
            End Select


        End With

        Return True
    End Function


#End Region

#End Region

#Region "マスタPOP"
    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    ''' 
    Private Function CustPop(ByVal frm As LMG060F, ByVal actionType As LMG060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            '荷主マスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()

            End With

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMG060F, ByVal actionType As LMG060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '営業所コードを選択の営業所に変更
            .Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMG060C.EventShubetsu.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

    End Function

    ''' <summary>
    ''' 請求先マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeqtoPop(ByVal frm As LMG060F, ByVal actionType As LMG060C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowSeqtoPop(frm, actionType)

        If prm.ReturnFlg = True Then
            '請求先マスタ
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtSeiqCd.TextValue = dr.Item("SEIQTO_CD").ToString()
                .lblSeiqNm.TextValue = dr.Item("SEIQTO_NM").ToString()
            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowSeqtoPop(ByVal frm As LMG060F, ByVal actionType As LMG060C.EventShubetsu) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMG060C.EventShubetsu.ENTER Then
                .Item("SEIQTO_CD") = frm.txtSeiqCd.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        '行追加
        dt.Rows.Add(dr)

        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ220")

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintShutu(ByVal frm As LMG060F) As Boolean

        '処理開始アクション
        Call Me._LMGconH.StartAction(frm)

        '画面全ロック
        MyBase.LockedControls(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG060C.EventShubetsu.PRINT) = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Return False

        End If

        'データセット
        Dim rtDs As DataSet = Me.SetDataSetInDataHantei(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        'プリント種別の判定
        Dim Print As String = frm.cmbPrint.SelectedValue.ToString
        Dim rtnDs As DataSet = Nothing

        Select Case Print

            Case LMG060C.PRINT_UNCHIN_SEIKYU
                '運賃請求明細書
                rtnDs = MyBase.CallWSA(blf, LMG060C.ACTION_ID_PRINT_SEIKYU, rtDs)

            Case LMG060C.PRINT_UNCHIN_TARIFF

                '運賃請求明細書(タリフ)
                rtnDs = MyBase.CallWSA(blf, LMG060C.ACTION_ID_PRINT_TARIFF, rtDs)

            Case LMG060C.PRINT_UNCHIN_CHECK
                '運賃チェックリスト
                rtnDs = MyBase.CallWSA(blf, LMG060C.ACTION_ID_PRINT_CHECK, rtDs)

            Case LMG060C.PRINT_UNCHIN_INKA
                '運賃請求明細書(入荷)
                rtnDs = MyBase.CallWSA(blf, LMG060C.ACTION_ID_PRINT_INKA, rtDs)

                '(2012.09.25) 追加START 運賃請求明細書(出荷)
            Case LMG060C.PRINT_UNCHIN_OUTKA
                '運賃請求明細書(出荷)
                rtnDs = MyBase.CallWSA(blf, LMG060C.ACTION_ID_PRINT_OUTKA, rtDs)
                '(2012.09.25) 追加END 運賃請求明細書(出荷)

                '(2013.02.18)要望番号1832 運賃請求明細書(連続) -- START --
            Case LMG060C.PRINT_UNCHIN_RENZOKU
                '運賃請求明細書(連続)
                rtnDs = MyBase.CallWSA(blf, LMG060C.ACTION_ID_PRINT_RENZOKU, rtDs)
                '(2013.02.18)要望番号1832 運賃請求明細書(連続) --  END  --

        End Select

        'メッセージコードの判定
        If Me.ShowStorePrintData(frm, Print) = True Then
            '終了メッセージ表示
            MyBase.ShowMessage(frm, "G002", New String() {"印刷", ""})
        End If

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

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        Return True

    End Function

    ''' <summary>
    ''' エラー出力処理
    ''' </summary>
    ''' <returns>出力する場合:False　出力しない場合:True</returns>
    ''' <remarks></remarks>
    Private Function ShowStorePrintData(ByVal frm As LMG060F, ByVal print As String) As Boolean

        If MyBase.IsMessageExist() = True OrElse MyBase.IsMessageStoreExist() = True Then

            If print.Equals(LMG060C.PRINT_UNCHIN_RENZOKU) = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            Else
                'エラーメッセージ表示
                MyBase.ShowMessage(frm)
                MyBase.ShowMessage(frm, "G007")
            End If

            Return False

        End If

        Return True

    End Function

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMG060F)

        '処理開始アクション
        Call Me._LMGconH.StartAction(frm)

        '項目チェック
        If Me._V.IsKensakuSingleCheck = False Then
            Call Me._LMGconH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '検索処理を行う
        Dim rtnDs As DataSet = Me.SelectList(frm)

        Dim outTbl As DataTable = rtnDs.Tables(LMG060C.TABLE_NM_OUT_CUST)
        Dim count As Integer = outTbl.Rows.Count

        '取得件数による処理変更
        If Me._LMGconH.CountRows(frm, frm.sprDetail, outTbl) = True AndAlso 0 < count Then

            '取得データをSPREADに表示
            Call Me._G.SetSpread(outTbl)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {Convert.ToString(count)})

        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

        '処理終了アクション
        Call Me._LMGconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 検索処理(データセット設定)
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectList(ByVal frm As LMG060F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMG060DS()
        Call SetDatasetCustInData(frm, ds)

        Dim rtnDs As DataSet = Nothing

        'キャッシュテーブルからデータ抽出
        rtnDs = Me.SelectCustOutListData(frm, ds)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">LMG060DS</param>
    ''' <returns>抽出された行列データセット</returns>
    ''' <remarks></remarks>
    Private Function SelectCustOutListData(ByVal frm As LMG060F, ByVal ds As DataSet) As DataSet

        'DataSetのIN情報を取得
        Dim inTbl As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_CUST)

        'INTableの条件rowの格納
        Dim drow As DataRow = inTbl.Rows(0)

        Dim strSqlCust As New System.Text.StringBuilder()
        Dim whereStr As String = String.Empty
        Dim andstr As New System.Text.StringBuilder()

        '▼▼キャッシュテーブルへの抽出条件文作成▼▼
        '営業所
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("NRS_BR_CD").ToString(), LMGControlC.ConditionPattern.equal, "NRS_BR_CD"))

        '荷主名(大)
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("CUST_NM_L").ToString(), LMGControlC.ConditionPattern.all, "CUST_NM_L"))

        '荷主名(中)
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("CUST_NM_M").ToString(), LMGControlC.ConditionPattern.all, "CUST_NM_M"))

        '荷主コード(大)
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("CUST_CD_L").ToString(), LMGControlC.ConditionPattern.pre, "CUST_CD_L"))

        '荷主コード(中)
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("CUST_CD_M").ToString(), LMGControlC.ConditionPattern.pre, "CUST_CD_M"))

        '荷主コード(小)
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("CUST_CD_S").ToString(), LMGControlC.ConditionPattern.pre, "CUST_CD_S"))

        '荷主コード(極小)
        andstr.Append(Me._LMGconH.SetWhereData(andstr, drow("CUST_CD_SS").ToString(), LMGControlC.ConditionPattern.pre, "CUST_CD_SS"))

        '全対象荷主は表示しない
        andstr.Append("AND CUST_CD_L <> 'ZZZZZ'")
        '▲▲キャッシュテーブルへの抽出条件文作成▲▲

        'キャッシュテーブルからデータ抽出
        Dim sort As String = "CUST_CD_L,CUST_CD_M,CUST_CD_S,CUST_CD_SS"

        Return Me._LMGconH.SelectListData(ds, LMG060C.TABLE_NM_OUT_CUST, LMConst.CacheTBL.CUST, andstr.ToString(), sort)

    End Function

    ''' <summary>
    ''' データセット設定(一覧部データ)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetCustInData(ByVal frm As LMG060F, ByVal ds As DataSet)

        Dim drow As DataRow = ds.Tables(LMG060C.TABLE_NM_IN_CUST).NewRow

        drow("NRS_BR_CD") = frm.cmbBr.SelectedValue

        With frm.sprDetail.ActiveSheet
            '検索行の値を設定
            drow("CUST_CD_L") = .Cells(0, LMG060G.sprDetailDef.CUST_CD_L.ColNo).Text.Trim()
            drow("CUST_CD_M") = .Cells(0, LMG060G.sprDetailDef.CUST_CD_M.ColNo).Text.Trim()
            drow("CUST_NM_L") = .Cells(0, LMG060G.sprDetailDef.CUST_NM_L.ColNo).Text.Trim()
            drow("CUST_NM_M") = .Cells(0, LMG060G.sprDetailDef.CUST_NM_M.ColNo).Text.Trim()
            drow("CUST_CD_S") = "00"
            drow("CUST_CD_SS") = "00"
            drow("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            drow("HYOJI_KBN") = LMZControlC.HYOJI_M
        End With

        ds.Tables(LMG060C.TABLE_NM_IN_CUST).Rows.Add(drow)

    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し(印刷処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMG060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷処理の呼び出し
        Call Me.PrintShutu(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '(2013.02.18)要望番号1832 複数荷主連続印刷対応 -- START --
    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '検索処理の呼び出し
        Call Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '(2013.02.18)要望番号1832 複数荷主連続印刷対応 --  END  --

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMG060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照
        Me.OpenMasterPop(frm)

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG060F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓========================

    ''' <summary>
    ''' 印刷コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub Print(ByVal frm As LMG060F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '印刷コンボボックス変更
        Me.Print(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMG060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Enterキーイベント
        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#Region "データセット"

    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInDataHantei(ByVal frm As LMG060F) As DataSet

        With frm

            Dim Print As String = .cmbPrint.SelectedValue.ToString
            Dim ds As DataSet = Nothing

            '印刷種別によってデータセット変更
            Select Case Print
                Case LMG060C.PRINT_UNCHIN_SEIKYU

                    '運賃請求明細
                    ds = New LMF510DS

                    Dim dt As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_SEIKYU)

                    'データセット510IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMG060C.TABLE_NM_IN_SEIKYU)

                Case LMG060C.PRINT_UNCHIN_TARIFF

                    '運賃請求明細(タリフ)
                    ds = New LMF520DS

                    Dim dt As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_TARIFF)

                    'データセット520IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMG060C.TABLE_NM_IN_TARIFF)

                Case LMG060C.PRINT_UNCHIN_CHECK

                    '運賃チェックリスト
                    ds = New LMF530DS

                    Dim dt As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_CHECK)

                    'データセット530IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMG060C.TABLE_NM_IN_CHECK)

                Case LMG060C.PRINT_UNCHIN_INKA

                    '運賃請求明細(入荷)
                    ds = New LMF590DS

                    Dim dt As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_INKA)

                    'データセット590IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMG060C.TABLE_NM_IN_INKA)

                    '(2012.09.25) 追加START 運賃請求明細書(出荷)
                Case LMG060C.PRINT_UNCHIN_OUTKA

                    '運賃請求明細(出荷)
                    ds = New LMF630DS

                    Dim dt As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_OUTKA)

                    'データセット630IN
                    ds = Me.SetDataSetInData(frm, ds, dt, LMG060C.TABLE_NM_IN_OUTKA)
                    '(2012.09.25) 追加END 運賃請求明細書(出荷)

                    '(2013.02.19)要望番号1832 運賃請求明細書連続印刷 -- START --
                Case LMG060C.PRINT_UNCHIN_RENZOKU

                    '運賃請求明細(連続)
                    ds = New LMF510DS

                    Dim dt As DataTable = ds.Tables(LMG060C.TABLE_NM_IN_SEIKYU)

                    'データセット510IN
                    ds = Me.SetDataSetInDataRenzoku(frm, ds, dt, LMG060C.TABLE_NM_IN_SEIKYU)
                    '(2013.02.19)要望番号1832 運賃請求明細書連続印刷 --  END  --

            End Select

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMG060F, ByVal ds As DataSet, ByVal dt As DataTable, ByVal id As String) As DataSet

        With frm
            Dim dr As DataRow = dt.NewRow()

            '運賃締め基準の取得
            Dim brCd As String = .cmbBr.SelectedValue.ToString() '20161019 要番2622 tsunehira add
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue
            Dim Seiq As String = .txtSeiqCd.TextValue
            Dim Sime As String = String.Empty

            '荷主が入力されている場合
            'START YANAI 要望番号592
            'If String.IsNullOrEmpty(CustCdL) = False AndAlso _
            'String.IsNullOrEmpty(CustCdM) = False Then
            If String.IsNullOrEmpty(CustCdL) = False Then
                'END YANAI 要望番号592

                '荷主コードでの締め基準の取得
                'Sime = Me.SimeKijun(CustCdL, CustCdM, "", Sime)
                Sime = Me.SimeKijun(brCd, CustCdL, CustCdM, "", Sime) '20161019 要番2622 tsunehira add
            End If

            '請求先コードが入力されている場合、運賃締め基準が取得されている場合は処理をしない
            If String.IsNullOrEmpty(Seiq) = False AndAlso _
            String.IsNullOrEmpty(Sime) = True Then

                '請求先コードでの運賃締め基準の取得
                'Sime = Me.SimeKijun("", "", Seiq, Sime)
                Sime = Me.SimeKijun(brCd, "", "", Seiq, Sime) '20161019 要番2622 tsunehira add

            End If


            ''データセットに格納
            dr("NRS_BR_CD") = .cmbBr.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue
            dr("CUST_CD_M") = .txtCustCdM.TextValue
            Dim Print As String = .cmbPrint.SelectedValue.ToString
            '運賃チェックリストの場合は請求先コードをデータセットしない
            If LMG060C.PRINT_UNCHIN_CHECK.Equals(Print) = False Then


                dr("SEIQ_CD") = .txtSeiqCd.TextValue
            End If
            dr("F_DATE") = .imdOutkaDateFrom.TextValue
            dr("T_DATE") = .imdOutkaDateTo.TextValue
            dr("UNTIN_CALCULATION_KB") = Sime

            '2013.02.28 / Notes1774(請求運賃明細書のみINに設定)
            If .cmbPrint.SelectedValue.Equals(LMG060C.PRINT_UNCHIN_SEIKYU) = True Then
                dr("CLOSE_KB") = .cmbCloseKbNm.SelectedValue
            End If
            '2013.02.28 / Notes1774

            'データセットの追加
            ds.Tables(id).Rows.Add(dr)

        End With
        Return ds
    End Function

    '(2013.02.19)要望番号1835 -- START --
    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInDataRenzoku(ByVal frm As LMG060F, ByVal ds As DataSet, ByVal dt As DataTable, ByVal id As String) As DataSet

        With frm

            Dim dr As DataRow = Nothing

            'チェック行格納処理
            Dim rowNo As ArrayList = Me._LMGconH.GetCheckList(.sprDetail.ActiveSheet, LMG060C.SprColumnIndex.DEF)

            '運賃締め基準の取得
            Dim CustCdL As String = String.Empty
            Dim CustCdM As String = String.Empty
            Dim Sime As String = String.Empty
            Dim Max As Integer = rowNo.Count - 1

            For i As Integer = 0 To Max

                dr = dt.NewRow()

                CustCdL = String.Empty
                CustCdM = String.Empty
                CustCdL = Me._LMGconV.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(rowNo(i)), LMG060C.SprColumnIndex.CUST_CD_L)).ToString.Trim
                CustCdM = Me._LMGconV.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(rowNo(i)), LMG060C.SprColumnIndex.CUST_CD_M)).ToString.Trim

                '荷主コードでの締め基準の取得
                'Sime = Me.SimeKijun(CustCdL, CustCdM, "", Sime)
                Sime = Me.SimeKijun(.cmbBr.SelectedValue.ToString(), CustCdL, CustCdM, "", Sime) '20161019 要番2622 tsunehira add

                'データセットに格納
                dr("NRS_BR_CD") = .cmbBr.SelectedValue
                dr("CUST_CD_L") = CustCdL.ToString
                dr("CUST_CD_M") = CustCdM.ToString
                dr("SEIQ_CD") = String.Empty
                dr("F_DATE") = .imdOutkaDateFrom.TextValue
                dr("T_DATE") = .imdOutkaDateTo.TextValue
                dr("UNTIN_CALCULATION_KB") = Sime
                dr("ROW_NO") = rowNo(i)

                'データセットの追加
                ds.Tables(id).Rows.Add(dr)

            Next

        End With

        Return ds

    End Function
    '(2013.02.19)要望番号1835 --  END  --

    ''' <summary>
    ''' 運賃締め基準の取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SimeKijun(ByVal brCd As String, ByVal CustCdL As String, ByVal CustCdM As String, ByVal Seiq As String, ByVal Sime As String) As String
        '20161019 要番2622 営業所コード追加 tsunehira add

        Dim drC As DataRow() = Nothing
        Dim count As Integer = 0

        'キャッシュの荷主マスターの値の取得
        drC = Me._V.SelectCustListDataRow(brCd, CustCdL, CustCdM, Seiq)

        'データロウのカウントの取得
        count = drC.Count

        'データが1件以上の場合
        If count >= 1 Then

            'データロウの0の運賃締め基準の取得
            Sime = drC(0).Item("UNTIN_CALCULATION_KB").ToString

        End If

        Return Sime

    End Function

#End Region

#Region "ユーティリティ"
    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMG060F)

        '画面解除
        MyBase.UnLockedControls(frm)


        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As Form, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub


#End Region

#End Region

End Class