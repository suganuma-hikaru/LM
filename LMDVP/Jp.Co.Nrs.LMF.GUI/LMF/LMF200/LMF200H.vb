'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF200H : 運行未登録運送検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
'選択処理で使用中
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMF200ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF200H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF200V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF200G

    ''' <summary>
    ''' 最新の検索時取得DS
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    ''' パラメータのNFFormDataをクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FormPrm As LMFormData

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconV As LMFControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconH As LMFControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

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
        Dim frm As LMF200F = New LMF200F(Me)

        '画面間データを取得する
        Me._FormPrm = prm
        Dim prmDs As DataSet = prm.ParamDataSet

        Me._OutDs = prm.ParamDataSet

        'Hnadler共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Validateクラスの設定
        Me._V = New LMF200V(Me, frm, Me._LMFconV)

        'Gamenクラスの設定
        Me._G = New LMF200G(Me, frm, Me._LMFconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()


        ''営業所の設定
        Call Me._G.SetcmbNrsBrCd()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue(prmDs.Tables(LMF200C.TABLE_NM_IN).Rows(0))

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G007")

        'フォーカスの設定
        Call Me._G.SetFoucus()

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
    Friend Function CloseForm(ByVal frm As LMF200F) As Boolean

        If Me._FormPrm.ParamDataSet.Tables(LMF200C.TABLE_NM_OUT) Is Nothing = True _
            OrElse Me._FormPrm.ParamDataSet.Tables(LMF200C.TABLE_NM_OUT).Rows.Count = 0 Then

            'リターンコードの設定
            Me._FormPrm.ReturnFlg = False
        Else

            'リターンコードの設定
            Me._FormPrm.ReturnFlg = True

        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectListEvent(ByVal frm As LMF200F) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '入力チェック
        If Me._V.IsInputCheck() = False Then

            '終了処理
            Call Me.EndAction(frm)

            Return False

        End If

        '検索条件格納
        Dim ds As DataSet = Me.SetDataSetInData(frm)

        '閾値の設定
        MyBase.SetLimitCount(Me._LMFconG.GetLimitData())


        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)



        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================

        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMF200C.ACTION_ID_SELECT, ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, LMF200C.ACTION_ID_SELECT, ds)

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '処理終了アクション
                    Call Me.EndAction(frm)
                    Return False

                End If

            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

            End If

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

        End If

        'LMF030画面にて行追加したレコードを削除
        Call Me.DeleteRowSelectData(rtnDs)

        '値の設定
        Call Me._G.SetSpread(rtnDs)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Function

#Region "LMF030画面にて行追加したレコードを削除"

    ''' <summary>
    ''' すでに選択されているレコードの削除
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function DeleteRowSelectData(ByVal ds As DataSet) As DataSet

        Dim prm As LMFormData = Me._FormPrm
        Dim dsi As DataSet = prm.ParamDataSet
        Me._OutDs = dsi

        'データテーブルLMF200OUTの宣言
        Dim dt As DataTable = ds.Tables(LMF200C.TABLE_NM_OUT)

        'データテーブル、データロウの宣言(F_UNSO_L)
        Dim dtf As DataTable = ds.Tables(LMF200C.F_UNSO_L)
        Dim drf As DataRow = dtf.NewRow()

        '行追加されているレコードをSELECT
        Dim dr As DataRow() = Me._OutDs.Tables(LMF200C.F_UNSO_L).Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = dr.Length - 1

        Dim chkDr As DataRow() = Nothing

        '運送番号と紐付いて行追加されている物を削除
        For i As Integer = 0 To max
            chkDr = dt.Select(Me.SetJobContSql(dr(i)))
            If 0 < chkDr.Length Then
                chkDr(0).Delete()
            End If
        Next

        '検索結果を保持
        Me._OutDs = ds

        Dim count As Integer = dt.Rows.Count()
        'メッセージエリアの設定
        If count > 0 Then

            MyBase.SetMessage("G008", New String() {dt.Rows.Count.ToString})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 削除するレコードを取得するSQL
    ''' </summary>
    ''' <param name="dr">データロウ</param>
    ''' <returns>SQL</returns>
    ''' <remarks></remarks>
    Private Function SetJobContSql(ByVal dr As DataRow) As String

        '条件としてLMF200のUNSO_NO_LとLMF030のUNSO_NOがイコール物を取得する
        Return String.Concat("UNSO_NO_L = '", dr.Item("UNSO_NO_L").ToString(), "' ")

    End Function

#End Region

#Region "選択処理"

    ''' <summary>
    ''' 選択処理（ダブルクリック時）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMF200F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)


        'arrの取得
        Dim arr As ArrayList = New ArrayList()
        arr.Add(e.Row)

        ''同値チェック
        If Me._LMFconV.IsDotiChk(frm.sprDetail, arr, LMF200G.sprDetailDef.NONYUDATE.ColNo, LMF200G.sprDetailDef.UNSO_CD.ColNo, _
                                 LMF200G.sprDetailDef.UNSO_BR_CD.ColNo) = False Then
            Exit Sub
        End If

        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, arr)

    End Sub

    ''' <summary>
    ''' 選択処理（OKボタン押下時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub RowOkSelect(ByVal frm As LMF200F)

        'チェックの付いたSpreadのRowIndexを取得
        Dim defNo As Integer = LMF200G.sprDetailDef.DEF.ColNo

        '共通クラスでメソッドが作成され次第下記項目をコメント化解除
        Dim arr As ArrayList = Me.SprSelectCount(frm.sprDetail, defNo)

        '未選択チェック
        If Me._LMFconV.IsSelectChk(arr.Count) = False Then
            Exit Sub
        End If

        ''同値チェック
        If Me._LMFconV.IsDotiChk(frm.sprDetail, arr, LMF200G.sprDetailDef.NONYUDATE.ColNo, LMF200G.sprDetailDef.UNSO_CD.ColNo, _
                                 LMF200G.sprDetailDef.UNSO_BR_CD.ColNo) = False Then
            Exit Sub
        End If


        '返却パラメータ設定
        Call Me.SelectionRowToFrm(frm, arr)

    End Sub

    ''' <summary>
    ''' 営業所の値変更時イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub NrsBrCdChange(ByVal frm As LMF200F)

        With frm

            '営業所コードの値を設定
            Dim nrsBrCd As String = .cmbEigyo.SelectedValue.ToString()

            '営業所コードがNULLの時は処理終了
            If String.IsNullOrEmpty(nrsBrCd) = True Then
                Exit Sub
            End If

            '運送コードがロックされている時は処理終了
            If .txtUnsocoCd.ReadOnly = True Then
                Exit Sub
            End If

            '(2012.11.09)要望番号1462 運送会社１次の絞り込み不要 -- START --

            .txtUnsocoCd.TextValue = String.Empty
            .txtUnsocoBrCd.TextValue = String.Empty
            .lblUnsocoNm.TextValue = String.Empty

            ''コンボグループコードの設定
            'Dim KbnGroup As String = "N017"

            ''データrowの設定(KBN)
            'Dim dr As DataRow() = Nothing

            ''データrowの設定(UNSOCO)
            'Dim drs As DataRow() = Nothing

            ''格納用の運送会社コード
            'Dim Unsoco As String = .txtUnsocoCd.TextValue

            ''格納用の運送支社コード
            'Dim UnsocoBr As String = .txtUnsocoBrCd.TextValue


            ''キャッシュの区分マスタから値を取得
            'dr = Me._LMFconG.SelectKbnListDataRow(nrsBrCd, KbnGroup)

            'Dim count As Integer = dr.Count
            'If count >= 1 Then

            '    '取得した値を運送会社コードに設定
            '    Unsoco = dr(0).Item("KBN_NM3").ToString()

            '    'テキストの運送コードの設定
            '    .txtUnsocoCd.TextValue = dr(0).Item("KBN_NM3").ToString()

            '    '取得した値を運送支社コードに設定
            '    UnsocoBr = dr(0).Item("KBN_NM4").ToString()

            '    'テキストの運送支社コードに設定
            '    .txtUnsocoBrCd.TextValue = dr(0).Item("KBN_NM4").ToString()

            '    'マスタ参照処理の呼び出し
            '    Call Me.SetReturnUnsocoPop(frm, LMF200C.EventShubetsu.MASTEROPEN)

            'Else
            '    .txtUnsocoCd.TextValue = String.Empty
            '    .txtUnsocoBrCd.TextValue = String.Empty
            '    .lblUnsocoNm.TextValue = String.Empty
            'End If

            '(2012.11.09)要望番号1462 運送会社１次の絞り込み不要 --  END  --

        End With

        '終了処理
        Call Me.EndAction(frm)


    End Sub


#Region "共通クラスでメソッドが作成され次第そちらを使用(画面モードを設定・取得)"

    ''' <summary>
    ''' 画面のモードを設定・取得します
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Property FormPrm() As LMFormData
        Get
            Return _FormPrm
        End Get
        Set(ByVal value As LMFormData)
            _FormPrm = value
        End Set
    End Property
#End Region

#End Region

#Region "マスタ参照"

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF200F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        'カーソル位置チェック
        If Me._V.IsFocusChk(objNm, LMF200C.EventShubetsu.MASTEROPEN) = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF200C.EventShubetsu.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF200F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF200C.EventShubetsu.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            Call Me.ShowGMessage(frm)

            Exit Sub

        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMF200C.EventShubetsu.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

#Region "POP"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMF200F, ByVal objNm As String, ByVal actionType As LMF200C.EventShubetsu) As Boolean

        Dim rtnResult As Boolean = False

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    '運送会社のマスタ参照の呼び出し
                    rtnResult = Me.SetReturnUnsocoPop(frm, actionType)

            End Select

        End With

        Return rtnResult

    End Function

#End Region

#Region "マスタ参照処理"

    ''' <summary>
    ''' 運送会社マスタPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMF200F, ByVal actinType As LMF200C.EventShubetsu) As Boolean

        Dim prm As LMFormData = Me.ShowUnsocoPop(frm, actinType)

        If prm.ReturnFlg = True Then
            'タリフマスタ照会
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)
            With frm
                .txtUnsocoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsocoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsocoNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), Space(1))
            End With
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPop(ByVal frm As LMF200F, ByVal actinType As LMF200C.EventShubetsu) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            'START SHINOHARA 要望番号513
            If actinType = LMF200C.EventShubetsu.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNSOCO_CD") = frm.txtUnsocoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsocoBrCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

#End Region

#End Region

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMF200F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '検索イベントの呼び出し
        Call Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF200F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'マスタ参照処理の呼び出し
        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMF200F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '選択処理()
        Call Me.RowOkSelect(frm)

        If Me.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF200F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMF200F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMF200F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '選択処理
        Call Me.RowSelection(frm, e)

        '選択行の取得に成功時自フォームを閉じる
        If Me.FormPrm.ReturnFlg = True Then
            frm.Close()
        End If

        Logger.EndLog(Me.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub


    ''' <summary>
    ''' 営業所コンボボックス変更時
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub NrsBrCd(ByVal frm As LMF200F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '営業所コンボックス変更処理
        Me.NrsBrCdChange(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub KeyDown(ByVal frm As LMF200F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub


#Region "データセット"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSetInData(ByVal frm As LMF200F) As DataSet

        Dim ds As DataSet = New LMF200DS()
        Dim dt As DataTable = ds.Tables(LMF200C.TABLE_NM_IN)

        'データテーブルの項目をクリア
        dt.Clear()

        Dim dr As DataRow = dt.NewRow()

        With frm

            '検索条件の格納
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr("YUSO_BR_CD") = .cmbBetsuEigyo.SelectedValue
            dr("UNSO_CD") = .txtUnsocoCd.TextValue
            dr("UNSO_BR_CD") = .txtUnsocoBrCd.TextValue
            dr("UNSO_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.UNSOCO_NM.ColNo))
            dr("ARR_PLAN_DATE_FROM") = .imdArrDateFrom.TextValue
            dr("ARR_PLAN_DATE_TO") = .imdArrDateTo.TextValue
            dr("UNSO_NO") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.UNSO_NO.ColNo))
            dr("BIN_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.BINKBN.ColNo))
            dr("TARIFF_BUNRUI_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.UNSO_TEHAI_KBN.ColNo))
            dr("CUST_REF_NO") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.CUST_REF_NO.ColNo))
            dr("ORIG_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.ORIG_NM.ColNo))
            dr("DEST_NO") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.DEST_NM.ColNo))
            dr("AREA_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.ARIA.ColNo))
            dr("KANRI_NO_L") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.KANRI_NO.ColNo))
            dr("CUST_L_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.CUST_NM.ColNo))
            dr("REMARK") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.REMARK.ColNo))
            dr("SEIQ_GROUP_NO") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.GROUP_NO.ColNo))
            dr("UNSO_ONDO_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.ONKAN.ColNo))
            dr("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.MOTO_DATA_KBN.ColNo))
            dr("SYUKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.SHUNI_TI.ColNo))
            dr("HAIKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.HAINI_TI.ColNo))
            dr("TRIP_NO_SYUKA") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.TRIP_NO_SHUKA.ColNo))
            dr("TRIP_NO_TYUKEI") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.TRIP_NO_CHUKEI.ColNo))
            dr("TRIP_NO_HAIKA") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.TRIP_NO_HAIKA.ColNo))
            dr("UNSO_SYUKA_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.UNSOCO_NM_SHUKA.ColNo))
            dr("UNSO_TYUKEI_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.UNSOCO_NM_CHUKEI.ColNo))
            dr("UNSO_HAIKA_NM") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.UNSOCO_NM_HAIKA.ColNo))

#If True Then   'ADD 2020/09/01 032102   【LMS】運行・運送画面の改修要望
            dr("DEST_ADD") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(0, LMF200G.sprDetailDef.DEST_ADD.ColNo))
#End If


            '検索条件の追加
            dt.Rows.Add(dr)

        End With

        Return ds

    End Function

#End Region

#Region "返却パラメータ系"

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <remarks></remarks>
    Private Sub SelectionRowToFrm(ByVal frm As LMF200F, ByVal arr As ArrayList)

        Dim ds As DataSet = New LMF200DS()
        Dim max As Integer = arr.Count - 1
        For i As Integer = 0 To max
            ds = Me.SelectionRowToFrm(frm, ds, Convert.ToInt32(arr(i)))
        Next

        Me._FormPrm.ParamDataSet = ds
        Me._FormPrm.ReturnFlg = True

    End Sub

    ''' <summary>
    ''' 選択行をフォームパラメータに戻す
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowIdx">選択行インデクス</param>
    ''' <remarks></remarks>
    Private Function SelectionRowToFrm(ByVal frm As LMF200F, ByVal ds As DataSet, ByVal rowIdx As Integer) As DataSet

        If 0 < rowIdx Then

            Dim dt As DataTable = ds.Tables(LMF200C.TABLE_NM_OUT)
            With frm.sprDetail.ActiveSheet

                Dim recNo As Integer = Convert.ToInt32(Me._LMFconG.GetCellValue(.Cells(rowIdx, LMF200G.sprDetailDef.ROW_NO.ColNo)))
                dt.ImportRow(Me._OutDs.Tables(LMF200C.TABLE_NM_OUT).Rows(recNo))

            End With

        End If

        Return ds

    End Function

#End Region

#Region "スプレッドでチェックついたRowIndexを取得"

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="defNo"></param>
    ''' <returns></returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount(ByVal spr As LMSpread, ByVal defNo As Integer) As ArrayList


        With spr.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._LMFconG.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

#End Region

#Region "ユーティリティ(終了アクション、処理開始アクション)"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF200F)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(frm)

    End Sub


    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EndAction(ByVal frm As LMF200F)

        '画面解除
        MyBase.UnLockedControls(frm)


        If IsMessageExist() = True Then
            MyBase.ShowMessage(frm)

        Else
            'メッセージ設定
            Call Me.ShowGMessage(frm)

        End If

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF200F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub
    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMF200F)

        Dim messageId As String = "G007"

        MyBase.ShowMessage(frm, messageId)

    End Sub

#End Region

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
