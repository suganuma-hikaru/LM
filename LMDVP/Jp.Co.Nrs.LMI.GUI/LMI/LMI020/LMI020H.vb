' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI    : データ管理サブ
'  プログラムID     :  LMI020 : デュポン在庫
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI020G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconH As LMIControlH

    ''' <summary>
    ''' 前回値保持変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _PreInputData As String

    ''' <summary>
    ''' 月末在庫コンボのデータ保持用(プラント)
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
        Dim frm As LMI020F = New LMI020F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIconG = New LMIControlG(sForm)

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIconG)

        'Hnadler共通クラスの設定
        Me._LMIconH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI020G(Me, frm, Me._LMIconG)

        'Validateクラスの設定
        Me._V = New LMI020V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'プラントの月末在庫を取得
        Dim ds As DataSet = New LMI020DS()
        Dim dt As DataTable = ds.Tables(LMI020C.TABLE_NM_GETU_IN)
        Dim dr As DataRow = dt.NewRow()

        Me._Ds = Me._LMIconH.ServerAccess(Me.SetLoadSelectData(), LMI020C.ACTION_ID_SELECT_LOAD_DATA)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'メッセージの表示
        Call Me.SetInitMessage(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

    ''' <summary>
    ''' 初期ロード時のサーバアクセス条件
    ''' </summary>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetLoadSelectData() As DataSet

        Dim ds As DataSet = New LMI020DS()
        Dim dt As DataTable = ds.Tables(LMI020C.TABLE_NM_GETU_IN)
        Dim dr As DataRow = Nothing
        Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
        Dim drs As DataRow() = Me._LMIconG.SelectKbnListDataRow(, LMKbnConst.KBN_D004, , brCd)
        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = brCd
            dr.Item("CUST_CD_L") = drs(i).Item("KBN_NM5").ToString()
            dr.Item("CUST_CD_M") = drs(i).Item("KBN_NM6").ToString()

            dt.Rows.Add(dr)

        Next

        Return ds

    End Function

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' ファイル作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub CreatePrintData(ByVal frm As LMI020F)

        '保持している値を更新
        Call Me.UpdatePreInputData(frm, frm.ActiveControl.Name)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI020C.ActionType.CREATE)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        'ファイル作成処理
        rtnResult = rtnResult AndAlso Me.CreatePrintDataAction(frm)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス位置設定
        If rtnResult = True Then
            Call Me._G.SetFoucus()
        End If

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI020F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI020C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI020C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMI020C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMI020C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMI020C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMIconH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            Call Me.ShowGMessage(frm)

            Exit Sub

        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMI020C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMIconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 作成種別変更時の処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintChangeAction(ByVal frm As LMI020F)

        'SFTP選択時
        If LMI020C.PRINT_SFTP.Equals(frm.cmbPrint.SelectedValue.ToString()) = True Then

            'プラント荷主の設定
            Call Me._G.SetPlantCust()

            '月末在庫コンボ生成
            Dim sql As String = String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' " _
                                              , " AND CUST_CD_M = '", frm.txtCustCdM.TextValue, "' ")
            Me._G.SetZaikoDateControl(Me._Ds.Tables(LMI020C.TABLE_NM_GETU_OUT).Select(sql, "RIREKI_DATE desc"))

        End If

        Call Me._G.SetLockControl()

        'START YANAI 要望番号769
        'プラントコードの設定
        Call Me._G.SetPlantCD()
        'END YANAI 要望番号769

    End Sub

    ''' <summary>
    ''' プラントコンボ値変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PlantChangeAction(ByVal frm As LMI020F)

        'プラント荷主の設定
        Call Me._G.SetPlantCust()

        '月末在庫コンボ生成
        Dim sql As String = String.Concat("CUST_CD_L = '", frm.txtCustCdL.TextValue, "' " _
                                          , " AND CUST_CD_M = '", frm.txtCustCdM.TextValue, "' ")
        Me._G.SetZaikoDateControl(Me._Ds.Tables(LMI020C.TABLE_NM_GETU_OUT).Select(sql, "RIREKI_DATE desc"))

        '初期表示以外は報告日の設定をしない
        If LMConst.FLG.ON.Equals(frm.lblLoadFlg.TextValue) = False Then
            Exit Sub
        End If

        'システム日付を画面に設定
        If Me._Ds Is Nothing = False Then
            frm.imdHokokuDate.TextValue = Me._Ds.Tables(LMI020C.TABLE_NM_SYS_DATETIME).Rows(0).Item("SYS_DATE").ToString()
        End If

    End Sub

    ''' <summary>
    ''' 荷主コードのロスとフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Sub LeaveCustData(ByVal frm As LMI020F, ByVal actionType As LMI020C.ActionType)

        With frm

            '大コードに値がない場合、スルー
            Dim custL As String = .txtCustCdL.TextValue
            If String.IsNullOrEmpty(custL) = True Then
                Exit Sub
            End If

            '中コードに値がない場合、スルー
            Dim custM As String = .txtCustCdM.TextValue
            If String.IsNullOrEmpty(custM) = True Then
                Exit Sub
            End If

            Dim chkCd As String = String.Empty
            Select Case actionType

                Case LMI020C.ActionType.CUSTL_LEAVE

                    chkCd = custL

                Case LMI020C.ActionType.CUSTM_LEAVE

                    chkCd = custM

            End Select

            '前回の値と同じ場合、スルー
            If Me._PreInputData.Equals(chkCd) = True Then
                Exit Sub
            End If

            'マスタに存在しない場合、スルー
            Dim drs As DataRow() = Me._LMIconG.SelectCustListDataRow(custL, custM, , LMIControlC.FLG_OFF)
            If drs.Length < 1 Then
                Exit Sub
            End If

            '名称の設定
            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()

            Dim sNm As String = String.Empty
            Dim sCd As String = .txtCustCdS.TextValue
            If String.IsNullOrEmpty(sCd) = False Then

                Dim max As Integer = drs.Length - 1
                For i As Integer = 0 To max

                    If sCd.Equals(drs(i).Item("CUST_CD_S").ToString()) = True Then
                        sNm = drs(i).Item("CUST_NM_S").ToString()
                        Continue For
                    End If

                Next

                .lblCustNmS.TextValue = sNm

            End If

            '荷主(小)一覧に値を設定
            Call Me._G.SetCustSDtl(drs)

            '全ロック
            Call Me.StartAction(frm)

            '月末在庫コンボ生成
            Call Me.SetZaikoDateControl(frm)

            'ロック解除
            Call Me.EndAction(frm)

        End With

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "PopUp"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMI020F, ByVal objNm As String, ByVal actionType As LMI020C.ActionType) As Boolean

        Dim rtnResult As Boolean = False

        '処理開始アクション
        Call Me.StartAction(frm)

        With frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtCustCdS.Name

                    rtnResult = Me.SetReturnCustPop(frm, actionType, objNm)

            End Select

        End With

        '処理終了アクション
        Call Me.EndAction(frm)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMI020F, ByVal actionType As LMI020C.ActionType, ByVal objNm As String) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                Dim lCd As String = dr.Item("CUST_CD_L").ToString()
                Dim mCd As String = dr.Item("CUST_CD_M").ToString()
                .txtCustCdL.TextValue = lCd
                .txtCustCdM.TextValue = mCd
                .lblCustNmL.TextValue = dr.Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr.Item("CUST_NM_M").ToString()

                Dim sCd As String = String.Empty
                Dim sNm As String = String.Empty
                If LMI020C.PRINT_NITHIJI.Equals(frm.cmbPrint.SelectedValue.ToString()) = False Then
                    sCd = dr.Item("CUST_CD_S").ToString()
                    sNm = dr.Item("CUST_NM_S").ToString()
                End If

                .txtCustCdS.TextValue = sCd
                .lblCustNmS.TextValue = sNm

                If LMI020C.ActionType.MASTEROPEN = actionType Then

                    '保持している値を更新
                    Call Me.UpdatePreInputData(frm, objNm)

                End If

                '荷主小明細に値を設定
                Call Me._G.SetCustSDtl(Me._LMIconG.SelectCustListDataRow(lCd, mCd, , LMIControlC.FLG_OFF))

            End With

            'マスタ参照の場合
            If LMI020C.ActionType.MASTEROPEN = actionType Then

                '月末在庫コンボ生成
                Call Me.SetZaikoDateControl(frm)

            End If

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMI020F, ByVal actionType As LMI020C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMI020C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                .Item("CUST_CD_S") = frm.txtCustCdS.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

            '検証結果(メモ)№77対応(2011.09.12)
            Select Case frm.cmbPrint.SelectedValue.ToString()

                Case LMI020C.PRINT_NITHIJI

                    .Item("HYOJI_KBN") = LMZControlC.HYOJI_S '荷主(中)まで表示

                Case LMI020C.PRINT_ZAIKO

                    .Item("HYOJI_KBN") = LMZControlC.HYOJI_SS '荷主(小)まで表示

                Case LMI020C.PRINT_SFTP
                    'マスタ参照なし
            End Select


        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMIconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMI020F)

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
    Private Sub EndAction(ByVal frm As LMI020F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 月末在庫コンボ生成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetZaikoDateControl(ByVal frm As LMI020F) As DataSet

        With frm

            '荷主コード(大)がない場合、スルー
            Dim lCd As String = .txtCustCdL.TextValue
            If String.IsNullOrEmpty(lCd) = True Then
                Return Nothing
            End If

            '荷主コード(中)がない場合、スルー
            Dim mCd As String = .txtCustCdM.TextValue
            If String.IsNullOrEmpty(mCd) = True Then
                Return Nothing
            End If

            '存在しないデータの場合、スルー
            If Me._LMIconG.SelectCustListDataRow(lCd, mCd).Length < 1 Then
                Return Nothing
            End If

            Dim ds As DataSet = New LMI020DS()
            Dim dt As DataTable = ds.Tables(LMI020C.TABLE_NM_GETU_IN)
            Dim dr As DataRow = dt.NewRow()
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = lCd
            dr.Item("CUST_CD_M") = mCd
            dt.Rows.Add(dr)

            'コンボ生成
            Dim rtnDs As DataSet = Me._LMIconH.ServerAccess(ds, LMI020C.ACTION_ID_GET_GETUDATA)
            Me._G.SetZaikoDateControl(rtnDs.Tables(LMI020C.TABLE_NM_GETU_OUT).Select(String.Empty, "RIREKI_DATE desc"))

            Return rtnDs

        End With

    End Function

    ''' <summary>
    ''' 保持している値を更新(荷主コード)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <remarks></remarks>
    Private Sub UpdatePreInputData(ByVal frm As LMI020F, ByVal objNm As String)

        With frm

            Select Case objNm


                Case .txtCustCdL.Name

                    Call Me.UpdatePreInputData(.txtCustCdL.TextValue)

                Case .txtCustCdM.Name

                    Call Me.UpdatePreInputData(.txtCustCdM.TextValue)

            End Select

        End With

    End Sub

    ''' <summary>
    ''' 前回値の設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Private Sub UpdatePreInputData(ByVal value As String)
        Me._PreInputData = value
    End Sub

    ''' <summary>
    ''' ファイル作成プログラム起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CreatePrintDataAction(ByVal frm As LMI020F) As Boolean

        With frm

            Dim pgId As String = String.Empty
            Select Case .cmbPrint.SelectedValue.ToString()

                Case LMI020C.PRINT_NITHIJI

                    pgId = "LMI500"

                Case LMI020C.PRINT_ZAIKO

                    pgId = "LMI501"

                Case LMI020C.PRINT_SFTP

                    pgId = "LMI502"

            End Select

            '別プログラムを起動
            Dim ds As DataSet = New LMI020DS()
            Dim dt As DataTable = ds.Tables(LMI020C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("CUST_CD_S") = .txtCustCdS.TextValue
            dr.Item("PLANT_CD") = .cmbPlantCd.SelectedText
            dr.Item("REPORT_DATE") = .imdHokokuDate.TextValue
            dr.Item("RIREKI_DATE") = .cmbZaiRirekiDate.SelectedValue.ToString()
            'START YANAI 要望番号953
            If (LMI020C.PRINT_SFTP).Equals(.cmbPrint.SelectedValue.ToString()) = True Then
                dr.Item("OFB_KB") = "01"
            End If
            'END YANAI 要望番号953
            dt.Rows.Add(dr)
            Me._LMIconH.FormShow(ds, pgId)

            'エラーがある場合、エラーメッセージを設定
            If MyBase.IsMessageExist() = True Then

                Dim rtnResult As Boolean = True

                If "E341".Equals(MyBase.GetMessageID()) = True Then
                    frm.txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    frm.cmbZaiRirekiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._LMIconV.SetErrorControl(frm.txtCustCdL)
                    rtnResult = False
                End If

                MyBase.ShowMessage(frm)
                Return rtnResult
            End If

            '完了メッセージを表示
            Me._LMIconH.SetMessageG002(frm, .cmbPrint.SelectedText, String.Empty)
            Return True

        End With

    End Function

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI020F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(Me.GetMsgAreaText(frm)) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMI020F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

    ''' <summary>
    ''' メッセージエリアの値を取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetMsgAreaText(ByVal frm As LMI020F) As String
        Return frm.Controls.Find(LMIControlC.MES_AREA, True)(0).Text
    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CreatePrintData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI020F_KeyDown(ByVal frm As LMI020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 荷主コード(大)のフォーカスインしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdL_Enter(ByVal frm As LMI020F, ByVal e As System.EventArgs)
        Call Me.UpdatePreInputData(frm.txtCustCdL.TextValue)
    End Sub

    ''' <summary>
    ''' 荷主コード(大)のフォーカスアウトしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdL_Leave(ByVal frm As LMI020F, ByVal e As System.EventArgs)
        Call Me.LeaveCustData(frm, LMI020C.ActionType.CUSTL_LEAVE)
    End Sub

    ''' <summary>
    ''' 荷主コード(中)のフォーカスインしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdM_Enter(ByVal frm As LMI020F, ByVal e As System.EventArgs)
        Call Me.UpdatePreInputData(frm.txtCustCdM.TextValue)
    End Sub

    ''' <summary>
    ''' 荷主コード(中)のフォーカスアウトしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdM_Leave(ByVal frm As LMI020F, ByVal e As System.EventArgs)
        Call Me.LeaveCustData(frm, LMI020C.ActionType.CUSTM_LEAVE)
    End Sub

    ''' <summary>
    ''' 作成種別の値変更時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbPrint_SelectedValueChanged(ByVal frm As LMI020F, ByVal e As System.EventArgs)
        Call Me.PrintChangeAction(frm)
    End Sub

    ''' <summary>
    ''' プラントコードの値変更時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbPlantCd_SelectedValueChanged(ByVal frm As LMI020F, ByVal e As System.EventArgs)
        Call Me.PlantChangeAction(frm)
    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class