' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH020H : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMH020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH020G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconH As LMHControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconG As LMHControlG

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet

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

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMH020F = New LMH020F(Me)

        'Hnadler共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMHconH = New LMHControlH(sForm, MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMHconG = New LMHControlG(sForm)

        'Validate共通クラスの設定
        Me._LMHconV = New LMHControlV(Me, sForm, Me._LMHconG)

        'Gamenクラスの設定
        Me._G = New LMH020G(Me, frm, Me._LMHconG)

        'Validateクラスの設定
        Me._V = New LMH020V(Me, frm, Me._LMHconV, Me._LMHconG, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'コントロール個別設定
        Call Me._G.SetControl()

        '値のクリア
        Call Me._G.ClearControl()

        '初期設定
        If Me.SetForm(frm, prmDs) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Call Me.SetGMessage(frm)

        'START YANAI EDIメモNo.43
        ''画面の入力項目の制御
        'Call Me._G.SetControlsStatus()
        'END YANAI EDIメモNo.43

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' ロード処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="prmDs">データセット</param>
    ''' <returns>
    ''' True ：検索成功
    ''' false：検索失敗
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetForm(ByVal frm As LMH020F, ByVal prmDs As DataSet) As Boolean

        Dim rtnResult As Boolean = False

        'データ取得
        Me._Ds = Me._LMHconH.ServerAccess(prmDs, LMH020C.ACTION_ID_SELECT)

        '検索成功
        If Me._Ds Is Nothing = False _
            AndAlso 0 < Me._Ds.Tables(LMH020C.TABLE_NM_L).Rows.Count Then

            rtnResult = True

            'モード・ステータスの設定
            Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey()

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

            '初期表示時の値設定
            Call Me._G.SetInitValue(Me._Ds)

            '画面の入力項目の制御
            Call Me._G.SetControlsStatus()

            'START YANAI EDIメモNo.43
            '中一覧に値を設定
            Call Me._G.SetInkaMData(Me._Ds)

            '中の詳細情報を表示
            Call Me._G.SetInkaMHeaderData(Me._Ds, 0)
            'END YANAI EDIメモNo.43

        End If

        Return rtnResult

    End Function

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShiftEditMode(ByVal frm As LMH020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH020C.ActionType.EDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsEditChk()

        'サーバチェック
        rtnResult = rtnResult AndAlso Me.ActionData(frm, Me._Ds, LMH020C.ACTION_ID_EDIT)

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'ロック制御のためにフリー大を再表示
        'START YANAI EDIメモNo.43
        'Call Me._G.SetFreeLData(Me._Ds)
        Call Me._G.SetFreeLData2(Me._Ds)

        '中情報のロック制御
        Call Me._G.SetIrisuLockControl()

        Dim rowNo As Integer = Me.GetRowNo(frm)

        '中の詳細情報を設定
        Call Me._G.SetInkaMHeaderData(Me._Ds, rowNo)
        'END YANAI EDIメモNo.43

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 中レコード行番号取得
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetRowNo(ByVal frm As LMH020F) As Integer

        Dim kanriNoM As String = frm.lblKanriNoM.TextValue
        Dim max As Integer = frm.sprGoodsDef.ActiveSheet.RowCount - 1
        Dim recNo As String = String.Empty

        If String.IsNullOrEmpty(kanriNoM) = True Then
            Return 0
        End If

        For i As Integer = 0 To max

            If Convert.ToInt32(kanriNoM) = Convert.ToInt32(Me._LMHconV.GetCellValue(frm.sprGoodsDef.ActiveSheet.Cells(i, LMH020G.sprGoodsDef.EDI_CTL_NO_CHU.ColNo))) Then
                recNo = i.ToString()
            End If
        Next

        If String.IsNullOrEmpty(recNo) = True Then
            Return 0
        Else
            Return Convert.ToInt32(recNo)
        End If

    End Function

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMH020F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH020C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMH020C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '項目チェック：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMH020C.ActionType.MASTEROPEN)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMH020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        '参照の場合、Tab移動して終了
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            Call Me._LMHconH.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        'Enterキー判定
        Dim rtnResult As Boolean = eventFlg

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMH020C.ActionType.ENTER)

        '計算処理
        Dim calcChk As Boolean = True
        If rtnResult = True Then

            'メッセージのクリア
            MyBase.ClearMessageAria(frm)

        End If

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMH020C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False OrElse calcChk = False Then

            '計算処理でのエラーの場合、フォーカス移動しない
            If calcChk = False Then
                Exit Sub
            End If

            'ガイダンスメッセージを設定
            Call Me.ShowGMessage(frm)

            'フォーカス移動処理
            Call Me._LMHconH.NextFocusedControl(frm, eventFlg)
            Exit Sub

        End If

        '項目チェック：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMH020C.ActionType.ENTER)

        'フォーカス移動処理
        Call Me._LMHconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SaveEdiItemData(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As Boolean

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH020C.ActionType.SAVE)

        'Mデータ値設定
        rtnResult = rtnResult AndAlso Me.SetMData(frm, Me._Ds)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(Me._Ds)

        '値設定
        rtnResult = rtnResult AndAlso Me.SetData(frm, Me._Ds)

        '計算処理
        rtnResult = rtnResult AndAlso Me.Calculation(frm, LMH020C.ActionType.SAVE, Nothing)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnsoSaveData(frm, Me._Ds)

        '処理終了アクション
        Call Me.EndAction(frm)

        'エラーの場合、処理を終了
        If rtnResult = False Then

            '中詳細が表示されている場合、ロック制御
            If String.IsNullOrEmpty(frm.lblKanriNoM.TextValue) = False Then
                Call Me._G.SetIrisuLockControl()
            End If

            Return False
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        Dim rowNo As Integer = Me.GetRowNo(frm)

        ''値のクリア
        'Call Me._G.ClearControl()

        '画面の入力項目の制御
        Dim lockFlgM As Boolean = True '中情報常にロックフラグ

        Call Me._G.SetControlsStatus(lockFlgM)

        ''値の設定
        Call Me._G.SetInkaMData(Me._Ds)

        '中の詳細情報を設定
        Call Me._G.SetInkaMHeaderData(Me._Ds, rowNo)

        Call Me._G.SetFreeLData2(Me._Ds)

        '処理終了メッセージの表示
        Call Me._LMHconH.SetMessageG002(frm _
                                        , Me._LMHconV.SetRepMsgData(frm.FunctionKey.F11ButtonName) _
                                        , String.Concat("[", frm.lblTitleEdiKanriNo.Text, " = ", frm.lblEdiKanriNo.TextValue, "]"))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        Return True

    End Function

    ''' <summary>
    ''' 行復活
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub RevivalEdiMData(ByVal frm As LMH020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH020C.ActionType.REVIVAL)

        '未選択チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMHconG.GetCheckList(frm.sprGoodsDef.ActiveSheet, LMH020G.sprGoodsDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._LMHconV.IsSelectChk(arr.Count)

        'Trim処理
        rtnResult = rtnResult AndAlso Me._V.TrimMDataSpaceTextValue()

        '中情報を設定
        rtnResult = rtnResult AndAlso Me.SetMData(frm, Me._Ds)

        '計算処理
        rtnResult = rtnResult AndAlso Me.Calculation(frm, LMH020C.ActionType.REVIVAL, arr)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsMDataChk(Me._Ds, LMH020C.ActionType.REVIVAL, arr)

        '行復活時のチェック
        rtnResult = rtnResult AndAlso Me._V.IsRevivalChk(Me._Ds, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '行復活処理
        Me._Ds = Me.SetRevivalData(frm, Me._Ds, arr)

        ''中一覧に値を設定
        'Call Me._G.SetInkaMData(Me._Ds)

        ''中情報のクリア
        'Call Me._G.ClearMControl()

        '処理終了アクション
        Call Me.EndAction(frm)

        'ロック制御
        Call Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' 行削除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DelEdiMData(ByVal frm As LMH020F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMH020C.ActionType.DEL)

        '未選択チェック
        Dim arr As ArrayList = Nothing
        Dim arrNotCheck As ArrayList = Nothing

        If rtnResult = True Then
            arr = Me._LMHconG.GetCheckList(frm.sprGoodsDef.ActiveSheet, LMH020G.sprGoodsDef.DEF.ColNo)
            arrNotCheck = Me._LMHconG.GetNotCheckList(frm.sprGoodsDef.ActiveSheet, LMH020G.sprGoodsDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._LMHconV.IsSelectChk(arr.Count)

        'Trim処理
        rtnResult = rtnResult AndAlso Me._V.TrimMDataSpaceTextValue()

        '中情報を設定
        rtnResult = rtnResult AndAlso Me.SetMData(frm, Me._Ds)

        '計算処理
        rtnResult = rtnResult AndAlso Me.Calculation(frm, LMH020C.ActionType.DEL, arr)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsMDataChk(Me._Ds, LMH020C.ActionType.DEL, arr)

        '行削除時のチェック
        rtnResult = rtnResult AndAlso Me._V.IsDelChk(Me._Ds, arr, arrNotCheck)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '行削除処理
        Me._Ds = Me.SetDelData(frm, Me._Ds, arr)

        '中一覧に値を設定
        'Call Me._G.SetInkaMData(Me._Ds)

        ''中情報のクリア
        'Call Me._G.ClearMControl()

        '処理終了アクション
        Call Me.EndAction(frm)

        'ロック制御
        Call Me._G.SetControlsStatus()

    End Sub

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMH020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        '行がない場合、スルー
        If rowNo < 0 Then
            Exit Sub
        End If

        'スプレッドヘッダー選択の場合、スルー
        If e.ColumnHeader = True Then
            Exit Sub
        End If

        '参照モードの場合、スルー
        If DispMode.VIEW.Equals(frm.lblSituation.DispMode) = True Then
            '中の詳細情報を設定
            Call Me._G.SetInkaMHeaderData(Me._Ds, rowNo)
            Exit Sub
        End If

        '処理開始アクション
        Call Me.StartAction(frm)

        'Trim処理
        Dim rtnResult As Boolean = Me._V.TrimMDataSpaceTextValue()

        '中の詳細情報を設定
        rtnResult = rtnResult AndAlso Me.SetMData(frm, Me._Ds)

        '計算処理
        rtnResult = rtnResult AndAlso Me.Calculation(frm, LMH020C.ActionType.DOUBLECLICK, Nothing)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsMDataChk(Me._Ds, LMH020C.ActionType.DOUBLECLICK, Nothing)

        'Trimした値を反映
        rtnResult = rtnResult AndAlso Me.SetMData(frm, Me._Ds)

        'エラーがある場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        '中一覧に値を設定
        Call Me._G.SetInkaMData(Me._Ds)

        '中の詳細情報を表示
        Call Me._G.SetInkaMHeaderData(Me._Ds, rowNo)

        '中情報のロック制御
        Call Me._G.SetIrisuLockControl()

    End Sub

    ''' <summary>
    ''' 運送手配の値変更時のロック制御
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub TehaiChangeAction(ByVal frm As LMH020F)
        Call Me._G.SetTariffKbnLockControl()
    End Sub

    ''' <summary>
    ''' 運送手配 , タリフ分類の値変更時のロック制御
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub TariffKbnChangeAction(ByVal frm As LMH020F)

        With frm

            '参照の場合、スルー
            If DispMode.VIEW.Equals(.lblSituation.DispMode) = True Then
                Exit Sub
            End If

            Dim tariffKbn As String = .cmbTariffKbn.SelectedValue.ToString()

            '値がない場合、スルー
            If String.IsNullOrEmpty(tariffKbn) = True Then
                Exit Sub
            End If

            'ロック制御
            Call Me._G.SetTariffKbnLockControl()

            .txtTariffCd.TextValue = String.Empty
            .lblTariffNm.TextValue = String.Empty

            'タリフセットマスタから値取得
            Dim drs As DataRow() = Me._LMHconV.SelectTariffSetListDataRow( _
                                                                          .cmbEigyo.SelectedValue.ToString() _
                                                                        , .txtCustCdL.TextValue _
                                                                        , .txtCustCdM.TextValue _
                                                                        , tariffKbn _
                                                                        , String.Empty _
                                                                        )

            '取得できない場合、　スルー
            If drs.Length < 1 Then
                Exit Sub
            End If

            Dim tariffCd As String = String.Empty
            Select Case tariffKbn

                Case LMHControlC.TARIFF_KURUMA

                    tariffCd = drs(0).Item("UNCHIN_TARIFF_CD2").ToString()

                Case LMHControlC.TARIFF_YOKO

                    tariffCd = drs(0).Item("YOKO_TARIFF_CD").ToString()

                Case Else

                    tariffCd = drs(0).Item("UNCHIN_TARIFF_CD1").ToString()

            End Select

            'タリフマスタから値取得
            Dim tariffNmCol As String = String.Empty
            Dim tariffDrs As DataRow() = Nothing
            If LMHControlC.TARIFF_YOKO.Equals(tariffKbn) = True Then
                tariffDrs = Me._LMHconG.SelectYokoTariffListDataRow(.cmbEigyo.SelectedValue.ToString(), tariffCd)
                tariffNmCol = "YOKO_REM"
            Else
                tariffDrs = Me._LMHconG.SelectUnchinTariffListDataRow(tariffCd, , .imdNyukaDate.TextValue)
                tariffNmCol = "UNCHIN_TARIFF_REM"
            End If

            '取得できない場合、　スルー
            If tariffDrs.Length < 1 Then
                Exit Sub
            End If

            'タリフコードの初期値設定
            .txtTariffCd.TextValue = tariffCd
            .lblTariffNm.TextValue = tariffDrs(0).Item(tariffNmCol).ToString()

        End With


    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CloseForm(ByVal frm As LMH020F, ByVal e As FormClosingEventArgs)

        '編集モード以外なら処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = False Then
            Exit Sub
        End If

        'メッセージの表示
        Select Case MyBase.ShowMessage(frm, "W002")

            Case MsgBoxResult.Yes '「はい」押下時

                '保存処理
                If Me.SaveEdiItemData(frm, LMH020C.ActionType.CLOSE) = False Then
                    e.Cancel = True
                End If

            Case MsgBoxResult.Cancel '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "計算"

    ''' <summary>
    ''' 計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Calculation(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType, ByVal arr As ArrayList) As Boolean

        With frm

            '計算処理をするかどうかを判定
            If Me._V.InkaMDataChkFlg(actionType, arr) = False Then
                Return True
            End If

            Dim dr As DataRow = Me._Ds.Tables(LMH020C.TABLE_NM_M).Select(String.Concat(" EDI_CTL_NO_CHU = '", .lblKanriNoM.TextValue, "' "))(0)
            Dim rtnResult As Boolean = Me._V.Calculation(dr)
            .numSumCnt.Value = dr.Item("NB")
            .numSumAnt.Value = dr.Item("SURYO")

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' Enter押下時の計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function FocusCalcAction(ByVal frm As LMH020F, ByVal objNm As String) As Boolean

        With frm

            Select Case objNm
                Case .numKosu.Name

                    If .numKosu.ReadOnly = True Then
                        Return True
                    End If

                Case .numHasu.Name

                    If .numHasu.ReadOnly = True Then
                        Return True
                    End If

                Case .numSumCnt.Name

                    If .numSumCnt.ReadOnly = True Then
                        Return True
                    End If

                Case .numIrime.Name

                    If .numIrime.ReadOnly = True Then
                        Return True
                    End If

                Case Else
                    Return True

            End Select

            '中の詳細情報を設定
            Call Me.SetMData(frm, Me._Ds)

            '計算処理
            Call Me.Calculation(frm, LMH020C.ActionType.ENTER, Nothing)

            Return True

        End With

    End Function

    ''' <summary>
    ''' フォーカス喪失時の計算処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function LostFocusCalcAction(ByVal frm As LMH020F) As Boolean

        With frm

            Dim hasu As Decimal = Convert.ToDecimal(.numHasu.Value)
            Dim konsu As Decimal = Convert.ToDecimal(.numKosu.Value)
            Dim irisu As Decimal = Convert.ToDecimal(.numIrisu.Value)
            Dim irime As Decimal = Convert.ToDecimal(.numIrime.Value)
            Dim suryo As Decimal = Convert.ToDecimal(.numSumAnt.Value)
            Dim temp As Decimal = 0

            '▼▼▼(マイナスデータ)
            '端数範囲チェック
            If Me._V.IsCalcOver(hasu.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_10, .lblTitleHasu.Text) = False Then
                Return False
            End If
            '▲▲▲(マイナスデータ)

            '個数再計算処理
            '梱数 * 入数 + 端数 → 個数にセット
            temp = konsu * irisu + hasu
            If Me._V.IsCalcOver(temp.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_10, .lblTitleSumCnt.Text) = False Then
                Return False
            End If
            .numSumCnt.Value = temp

            '数量再計算処理
            '個数 * 入目　→　数量にセット
            Dim kosu As Decimal = Convert.ToDecimal(.numSumCnt.Value)

            temp = kosu * irime
            If Me._V.IsCalcOver(temp.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_9_3, .lblTitleSumAnt.Text) = False Then
                Return False
            End If
            .numSumAnt.Value = temp
        End With

        Return True

    End Function



#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UnsoSaveData(ByVal frm As LMH020F, ByVal ds As DataSet) As Boolean

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, LMH020C.ACTION_ID_SAVE, rtnDs) = False Then
            Return False
        End If

        '値の設定
        Me._Ds = rtnDs

        Return True

    End Function

    ''' <summary>
    ''' サーバアクセス(チェック有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="rtnDs">戻りDataSet 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ActionData(ByVal frm As LMH020F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = Me._LMHconH.ServerAccess(ds, actionId)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMH020F)

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
    Private Sub EndAction(ByVal frm As LMH020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 行復活処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetRevivalData(ByVal frm As LMH020F, ByVal ds As DataSet, ByVal arr As ArrayList) As DataSet
        Return Me.SetDelRevivalData(frm, ds, arr, LMConst.FLG.OFF)
    End Function

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDelData(ByVal frm As LMH020F, ByVal ds As DataSet, ByVal arr As ArrayList) As DataSet
        Return Me.SetDelRevivalData(frm, ds, arr, LMConst.FLG.ON)
    End Function

    ''' <summary>
    ''' 行削除 / 復活処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="delFlg">削除フラグ</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetDelRevivalData(ByVal frm As LMH020F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal delFlg As String) As DataSet

        Dim dt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
        Dim max As Integer = arr.Count - 1
        Dim delNm As String = String.Empty
        Dim drs As DataRow() = Me._LMHconG.SelectKbnListDataRow(delFlg, LMKbnConst.KBN_S051)
        If 0 < drs.Length Then
            delNm = drs(0).Item("KBN_NM1").ToString()
        End If

        Dim recNo As Integer = 0

        For i As Integer = 0 To max

            'レコード番号を取得
            recNo = Convert.ToInt32(Me._LMHconG.GetCellValue(frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMH020G.sprGoodsDef.RECNO.ColNo)))
            '▼▼▼(EDIﾒﾓNo.61)
            '状態をスプレッドに表示
            frm.sprGoodsDef.SetCellValue(Convert.ToInt32(arr(i)), LMH020G.sprGoodsDef.JYOTAI_NM.ColNo, delNm)
            '▲▲▲(EDIﾒﾓNo.61)

            '値を設定
            dt.Rows(recNo).Item("JYOTAI") = delFlg
            dt.Rows(recNo).Item("JYOTAI_NM") = delNm


        Next

        Return ds

    End Function

#End Region

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMH020F, ByVal objNm As String, ByVal actionType As LMH020C.ActionType) As Boolean

        With frm

            Dim rtnResult As Boolean = False

            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtGoodsCd.Name

                    rtnResult = Me.SetReturnGoodsPop(frm, actionType)

                Case .txtGoodsComment.Name

                    rtnResult = Me.SetReturnRemarkPop(frm, actionType)

                Case .txtUnsoCd.Name, .txtUnsoBrCd.Name

                    rtnResult = Me.SetReturnUnsocoPop(frm, actionType)

                Case .txtTariffCd.Name

                    rtnResult = Me.SetReturnTariffPop(frm, actionType)

                Case .txtShukkaMotoCd.Name

                    rtnResult = Me.SetReturnDestPop(frm, actionType)

            End Select

            '処理終了アクション
            Call Me.EndAction(frm)

            '中レコードのロック制御の切替
            Call Me._G.SetIrisuLockControl()

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 商品Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnGoodsPop(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowGoodsPopup(frm, actionType)

        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ020C.TABLE_NM_OUT).Rows(0)

            '画面 , DataSetに設定
            Call Me._V.SetGoodsMstData(Me._G.GetInkaMDataRow(Me._Ds), dr, True)

            '中の詳細情報を設定
            Call Me.SetMData(frm, Me._Ds)

            '設定後に計算処理
            Call Me.Calculation(frm, LMH020C.ActionType.MASTEROPEN, Nothing)

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 商品マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMH020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("GOODS_CD_CUST") = frm.txtGoodsCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 注意書Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnRemarkPop(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowRemarkPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ270C.TABLE_NM_OUT).Rows(0)
            '要望番号516 START
            If String.IsNullOrEmpty(dr.Item("REMARK").ToString()) = False AndAlso _
               String.IsNullOrEmpty(frm.txtGoodsComment.TextValue.ToString()) = False Then
                frm.txtGoodsComment.TextValue = String.Concat(frm.txtGoodsComment.TextValue, Space(1), dr.Item("REMARK").ToString())
            ElseIf String.IsNullOrEmpty(dr.Item("REMARK").ToString()) = False AndAlso _
               String.IsNullOrEmpty(frm.txtGoodsComment.TextValue.ToString()) = True Then
                frm.txtGoodsComment.TextValue = dr.Item("REMARK").ToString()
            End If
            '要望番号516 END

            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 注意書テーブル参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowRemarkPopup(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ270DS()
        Dim dt As DataTable = ds.Tables(LMZ270C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("USER_CD") = LMUserInfoManager.GetUserID()
            'START SHINOHARA 要望番号513
            If actionType = LMH020C.ActionType.ENTER Then
                .Item("REMARK") = frm.txtGoodsComment.TextValue
            End If
            'END SHINOHARA 要望番号513		
            .Item("SUB_KB") = LMH020C.YOTO_INKA
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ270", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowUnsocoPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtUnsoCd.TextValue = dr.Item("UNSOCO_CD").ToString()
                .txtUnsoBrCd.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
                .lblUnsoNm.TextValue = Me._LMHconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), LMHControlC.ZENKAKU_SPACE)

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
    Private Function ShowUnsocoPopup(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMH020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNSOCO_CD") = frm.txtUnsoCd.TextValue
                .Item("UNSOCO_BR_CD") = frm.txtUnsoBrCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim name As String = String.Empty

        With frm

            If LMHControlC.TARIFF_YOKO.Equals(.cmbTariffKbn.SelectedValue.ToString()) = True Then

                '横持ちタリフPop
                prm = Me.ShowYokoTariffPopup(frm, actionType)
                tblNm = LMZ100C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                name = "YOKO_REM"

            Else

                '運賃タリフPop
                prm = Me.ShowUnchinTariffPopup(frm, actionType)
                tblNm = LMZ230C.TABLE_NM_OUT
                code = "UNCHIN_TARIFF_CD"
                name = "UNCHIN_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                .txtTariffCd.TextValue = dr.Item(code).ToString()
                .lblTariffNm.TextValue = dr.Item(name).ToString()

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMH020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNCHIN_TARIFF_CD") = frm.txtTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("STR_DATE") = frm.imdNyukaDate.TextValue
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMH020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                '.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                '.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("YOKO_TARIFF_CD") = frm.txtTariffCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As Boolean

        With frm

            Dim prm As LMFormData = Me.ShowDestPopup(frm, actionType)
            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                .txtShukkaMotoCd.TextValue = dr.Item("DEST_CD").ToString()
                .lblShukkaMotoNm.TextValue = dr.Item("DEST_NM").ToString()

                Return True

            End If

            Return False

        End With

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMH020F, ByVal actionType As LMH020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            'START SHINOHARA 要望番号513
            If actionType = LMH020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("DEST_CD") = frm.txtShukkaMotoCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMHconH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMH020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMHControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetGMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを設定する
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As LMH020F)

        Dim messageId As String = "G003"

        MyBase.ShowMessage(frm, messageId)

    End Sub

    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Private Function SetMessageC001(ByVal frm As LMH020F, ByVal msg As String, ByVal actionType As LMH020C.ActionType) As Boolean

        '保存でない場合、メッセージを表示しない
        If LMH020C.ActionType.SAVE <> actionType Then
            Return True
        End If

        Return Me._LMHconH.SetMessageC001(frm, msg)

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' 値設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetData(ByVal frm As LMH020F, ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = Me.SetLData(frm, ds)

        rtnResult = rtnResult AndAlso Me.SetMData(frm, ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 大情報を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetLData(ByVal frm As LMH020F, ByVal ds As DataSet) As Boolean

        Dim dr As DataRow = ds.Tables(LMH020C.TABLE_NM_L).Rows(0)
        With frm

            dr.Item("EDI_CTL_NO") = .lblEdiKanriNo.TextValue
            dr.Item("EDI_STATE_KB") = .cmbStatus.SelectedValue
            dr.Item("INKA_CTL_NO_L") = .lblKanriNoL.TextValue
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue
            dr.Item("NRS_WH_CD") = .cmbWh.SelectedValue
            dr.Item("INKA_STATE_KB") = .cmbShinshokuKbn.SelectedValue
            dr.Item("INKA_DATE") = .imdNyukaDate.TextValue
            dr.Item("INKA_TP") = .cmbNyukaType.SelectedValue
            dr.Item("INKA_KB") = .cmbNyukaKbn.SelectedValue
            dr.Item("BUYER_ORD_NO") = .txtBuyerOrdNo.TextValue
            dr.Item("OUTKA_FROM_ORD_NO") = .txtOrderNo.TextValue
            dr.Item("HOKAN_FREE_KIKAN") = .numFree.TextValue
            dr.Item("HOKAN_STR_DATE") = .imdHokanDate.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("TAX_KB") = .cmbTax.SelectedValue
            dr.Item("TOUKI_HOKAN_YN") = .cmbToukiHo.SelectedValue
            dr.Item("HOKAN_YN") = .cmbZenkiHo.SelectedValue
            dr.Item("NIYAKU_YN") = .cmbNiyakuUmu.SelectedValue
            dr.Item("INKA_PLAN_QT") = .numPlanQt.Value
            dr.Item("INKA_PLAN_QT_UT") = .cmbPlanQtUt.SelectedValue
            dr.Item("INKA_TTL_NB") = .numNyukaCnt.TextValue
            dr.Item("NYUBAN_L") = .txtNyubanL.TextValue
            dr.Item("REMARK") = .txtNyukaComment.TextValue
            dr.Item("UNCHIN_TP") = .cmbUnchinTehai.SelectedValue
            dr.Item("UNCHIN_KB") = .cmbTariffKbn.SelectedValue
            dr.Item("SYARYO_KB") = .cmbSharyoKbn.SelectedValue
            dr.Item("UNSO_ONDO_KB") = .cmbOnkan.SelectedValue
            dr.Item("UNCHIN") = .numUnchin.Value
            dr.Item("UNSO_CD") = .txtUnsoCd.TextValue
            dr.Item("UNSO_BR_CD") = .txtUnsoBrCd.TextValue
            dr.Item("YOKO_TARIFF_CD") = .txtTariffCd.TextValue
            dr.Item("TARIFF_REM") = .lblTariffNm.TextValue
            dr.Item("OUTKA_MOTO") = .txtShukkaMotoCd.TextValue
            dr.Item("OUTKA_MOTO_NM") = .lblShukkaMotoNm.TextValue

        End With

        'スプレッドの値を設定
        ds = Me.GetFreeData(frm.sprFreeL, ds, dr, LMH020C.DATA_KB_L)

        Return True

    End Function

    ''' <summary>
    ''' 中情報を設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetMData(ByVal frm As LMH020F, ByVal ds As DataSet) As Boolean

        With frm

            Dim mNo As String = .lblKanriNoM.TextValue

            '行選択していない場合、スルー
            If String.IsNullOrEmpty(mNo) = True Then
                Return True
            End If

            'ロット№は大文字変換
            .txtLot.TextValue = .txtLot.TextValue.ToUpper

            '荷主商品コードのスペース除去
            .txtGoodsCd.TextValue = Trim(.txtGoodsCd.TextValue)

            '▼▼▼
            If Convert.ToInt32(.numIrisu.TextValue) = 1 Then
                .numSumCnt.TextValue = .numHasu.TextValue
            End If
            '▲▲▲

            Dim dr As DataRow = ds.Tables(LMH020C.TABLE_NM_M).Select(String.Concat(" EDI_CTL_NO_CHU = '", mNo, "' "))(0)
            dr.Item("EDI_CTL_NO_CHU") = .lblKanriNoM.TextValue
            dr.Item("CUST_GOODS_CD") = .txtGoodsCd.TextValue
            dr.Item("GOODS_NM") = .lblGoodsNm.TextValue
            dr.Item("NRS_GOODS_CD") = .lblGoodsKey.TextValue
            dr.Item("INKA_PKG_NB") = .numKosu.TextValue
            dr.Item("HASU") = .numHasu.TextValue
            dr.Item("PKG_NB") = .numIrisu.TextValue
            dr.Item("STD_IRIME") = .numStdIrime.TextValue
            dr.Item("IRIME") = .numIrime.TextValue
            dr.Item("SURYO") = .numSumAnt.TextValue
            dr.Item("NB") = .numSumCnt.TextValue
            dr.Item("BETU_WT") = .numTare.TextValue
            dr.Item("ONDO_KB") = .cmbOndo.SelectedValue
            dr.Item("LOT_NO") = .txtLot.TextValue
            dr.Item("OUTKA_FROM_ORD_NO") = .txtOrderNoM.TextValue
            dr.Item("BUYER_ORD_NO") = .txtBuyerOrdNoM.TextValue
            dr.Item("SERIAL_NO") = .txtSerial.TextValue
            dr.Item("REMARK") = .txtGoodsComment.TextValue

            'スプレッドの値を設定
            ds = Me.GetFreeData(frm.sprFreeM, ds, dr, LMH020C.DATA_KB_M)

            Return True

        End With

    End Function

    ''' <summary>
    ''' フリースプレッドの値を取得
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <param name="dataKb">データ区分</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetFreeData(ByVal spr As Win.Spread.LMSpread, ByVal ds As DataSet, ByVal dr As DataRow, ByVal dataKb As String) As DataSet

        With spr.ActiveSheet

            Dim rowCnt As Integer = 0
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max

                dr.Item(Me._LMHconG.GetCellValue(.Cells(i, LMH020G.sprFreeDef.COLNM.ColNo))) = Me._LMHconG.GetCellValue(.Cells(i, LMH020G.sprFreeDef.FREE.ColNo))

            Next


            Return ds

        End With

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMH020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ShiftEditMode(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMH020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMH020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveEdiItemData(frm, LMH020C.ActionType.SAVE)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMH020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMH020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CloseForm(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprGoodsDef_CellDoubleClick(ByRef frm As LMH020F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub LMH020F_KeyDown(ByVal frm As LMH020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 運送手配の値変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbUnchinTehai_SelectedValueChanged(ByVal frm As LMH020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.TehaiChangeAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' タリフ分類の値変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbTariffKbn_SelectedValueChanged(ByVal frm As LMH020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.TariffKbnChangeAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 行復活のクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnRevival_Click(ByVal frm As LMH020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.RevivalEdiMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 行削除のクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnDel_Click(ByVal frm As LMH020F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.DelEdiMData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class