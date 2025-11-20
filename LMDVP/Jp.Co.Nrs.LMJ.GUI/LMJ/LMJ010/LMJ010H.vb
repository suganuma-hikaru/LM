' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ010H : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  Shinohara
' ==========================================================================
Imports System.Text
Imports System.IO
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMJ010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMJ010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMJ010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMJ010G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconV As LMJControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconG As LMJControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMJconH As LMJControlH

    ''' <summary>
    ''' 前回値保持変数
    ''' </summary>
    ''' <remarks></remarks>
    Private _PreInputData As String

    ''' <summary>
    ''' サーバ日付
    ''' </summary>
    ''' <remarks></remarks>
    Private _SysDate As String

    ''' <summary>
    ''' ロードフラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _LoadFlg As String = LMConst.FLG.OFF

    ''' <summary>
    ''' 初期荷主の月末在庫情報を保持
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
        Dim frm As LMJ010F = New LMJ010F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMJconG = New LMJControlG(sForm)

        'Validate共通クラスの設定
        Me._LMJconV = New LMJControlV(Me, sForm, Me._LMJconG)

        'Hnadler共通クラスの設定
        Me._LMJconH = New LMJControlH(sForm, MyBase.GetPGID(), Me)

        'Validateクラスの設定
        Me._V = New LMJ010V(Me, frm, Me._LMJconV)

        'Gamenクラスの設定
        Me._G = New LMJ010G(Me, frm, Me._LMJconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        '初期値設定
        Me._G.SetInitValue()

        'ロードフラグをON
        Me._LoadFlg = LMConst.FLG.ON

        '月末在庫コンボ生成
        Me._Ds = Me.SetSelectData(frm)

        '請求日付の初期設定
        Call Me._G.SetInitValue(Me._SysDate)

        'メッセージの表示
        Call Me.SetInitMessage(frm)
        
        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' ファイル作成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub CreatePrintData(ByVal frm As LMJ010F)

        '保持している値を更新
        Call Me.UpdatePreInputData(frm, frm.ActiveControl.Name)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMJ010C.ActionType.CREATE)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

        '確認メッセージ表示
        rtnResult = rtnResult AndAlso Me._LMJconH.SetMessageC001(frm, Me._LMJconV.SetRepMsgData(frm.FunctionKey.F7ButtonName))

        'ファイル作成処理
        rtnResult = rtnResult AndAlso Me.CreatePrintDataAction(frm)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMJ010F)

        'カーソル位置の設定
        Dim objNm As String = frm.ActiveControl.Name()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMJ010C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMJ010C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMJ010C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMJ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMJ010C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMJ010C.ActionType.ENTER)

        'エラーの場合、終了
        If rtnResult = False Then

            'フォーカス移動処理
            Call Me._LMJconH.NextFocusedControl(frm, eventFlg)

            'メッセージ設定
            Call Me.ShowGMessage(frm)

            Exit Sub

        End If

        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Call Me.ShowPopupControl(frm, objNm, LMJ010C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMJconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 処理内容の値変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeShoriComb(ByVal frm As LMJ010F)

        '初期表示フラグで処理続行判定
        If LMConst.FLG.OFF.Equals(Me._LoadFlg) = True Then
            Exit Sub
        End If

        Call Me._G.SetControlsStatus(Me._Ds, Me._SysDate)

    End Sub

    ''' <summary>
    ''' 請求日付コンボの値変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ChangeSeiqComb(ByVal frm As LMJ010F)
        frm.cmbZaiko.SelectedValue = frm.cmbSeiqComb.SelectedValue
    End Sub

    ''' <summary>
    ''' 荷主コードのロスとフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Sub LeaveCustData(ByVal frm As LMJ010F, ByVal actionType As LMJ010C.ActionType)

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

                Case LMJ010C.ActionType.CUSTL_LEAVE

                    chkCd = custL

                Case LMJ010C.ActionType.CUSTM_LEAVE

                    chkCd = custM

            End Select

            '前回の値と同じ場合、スルー
            If Me._PreInputData.Equals(chkCd) = True Then
                Exit Sub
            End If

            'マスタに存在しない場合、スルー
            Dim drs As DataRow() = Me._LMJconG.SelectCustListDataRow(custL, custM, , LMJControlC.FLG_OFF)
            If drs.Length < 1 Then
                Exit Sub
            End If

            '名称の設定
            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()

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
    Private Function ShowPopupControl(ByVal frm As LMJ010F, ByVal objNm As String, ByVal actionType As LMJ010C.ActionType) As Boolean

        Dim rtnResult As Boolean = False

        '処理開始アクション
        Call Me.StartAction(frm)

        With frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

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
    Private Function SetReturnCustPop(ByVal frm As LMJ010F, ByVal actionType As LMJ010C.ActionType, ByVal objNm As String) As Boolean

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

                If LMJ010C.ActionType.MASTEROPEN = actionType Then

                    '保持している値を更新
                    Call Me.UpdatePreInputData(frm, objNm)

                End If

            End With

            'マスタ参照の場合
            If LMJ010C.ActionType.MASTEROPEN = actionType Then

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
    Private Function ShowCustPopup(ByVal frm As LMJ010F, ByVal actionType As LMJ010C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMJ010C.ActionType.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMJconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMJ010F)

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
    Private Sub EndAction(ByVal frm As LMJ010F)

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
    Private Function SetZaikoDateControl(ByVal frm As LMJ010F) As DataSet

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
            If Me._LMJconG.SelectCustListDataRow(lCd, mCd).Length < 1 Then
                Return Nothing
            End If

            '月末在庫コンボ生成
            Return Me.SetSelectData(frm)

        End With

    End Function

    ''' <summary>
    ''' 月末在庫コンボのデータ取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectGetuData(ByVal frm As LMJ010F) As DataSet

        With frm

            Dim ds As DataSet = New LMJ010DS()
            Dim dt As DataTable = ds.Tables(LMJ010C.TABLE_NM_GETU_IN)
            Dim dr As DataRow = dt.NewRow()
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dt.Rows.Add(dr)

            Return Me._LMJconH.ServerAccess(ds, LMJ010C.ACTION_ID_GET_GETUDATA)

        End With

    End Function

    ''' <summary>
    ''' 保持している値を更新(荷主コード)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <remarks></remarks>
    Private Sub UpdatePreInputData(ByVal frm As LMJ010F, ByVal objNm As String)

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
    Private Function CreatePrintDataAction(ByVal frm As LMJ010F) As Boolean

        With frm

            Dim ds As DataSet = New LMJ010DS()
            Dim dt As DataTable = ds.Tables(LMJ010C.TABLE_NM_IN)
            Dim dr As DataRow = dt.NewRow()
            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("SEIKYU_DATE") = Me.GetSeiqDate(frm)
            dr.Item("RIREKI_DATE") = .cmbZaiko.SelectedValue.ToString()
            dr.Item("CLOSE_KB") = .cmbShori.SelectedValue.ToString()
            dr.Item("SERIAL_FLG") = Me.GetSerialFlg(frm)
            dt.Rows.Add(dr)
            ds.Tables.Add(New LMJ800DS.LMJ800OUTDataTable())

            'サーバアクセス
            Dim rtnDs As DataSet = Me._LMJconH.ServerAccess(ds, LMJ010C.ACTION_ID_GET_CREATE_DATA)

            'エラーがある場合、エラーメッセージを設定
            Dim errDt As DataTable = rtnDs.Tables(LMJ010C.TABLE_NM_ERR)
            If 0 < errDt.Rows.Count Then

                Dim errDr As DataRow = errDt.Rows(0)

                Select Case errDr.Item("CHK").ToString()

                    Case LMJ010C.ERR_01

                        frm.imdSeiqDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

                    Case LMJ010C.ERR_02

                        frm.cmbZaiko.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

                End Select

                '荷主が入力可能の場合
                If LMJ010C.SHORI_SONOTA.Equals(frm.cmbShori.SelectedValue.ToString()) = True Then
                    frm.txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._LMJconV.SetErrorControl(frm.txtCustCdL)
                End If
                Return Me._LMJconV.SetErrMessage(errDr.Item("ID").ToString(), New String() {errDr.Item("MSG").ToString()})

            End If

            'ファイル出力
           Return Me.OutFileData(frm, rtnDs)

        End With

    End Function

    ''' <summary>
    ''' サーバ日付を保持
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SetSysDate(ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMJ010C.TABLE_NM_SYS_DATETIME)
        Me._SysDate = dt.Rows(dt.Rows.Count - 1).Item("SYS_DATE").ToString()

    End Sub

    ''' <summary>
    ''' シリアルの値取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>有にチェックしている場合、"1"　無にチェックしている場合、"0"</returns>
    ''' <remarks></remarks>
    Private Function GetSerialFlg(ByVal frm As LMJ010F) As String

        If frm.optSerialAri.Checked = True Then
            GetSerialFlg = LMConst.FLG.ON
        Else
            GetSerialFlg = LMConst.FLG.OFF
        End If

        Return GetSerialFlg

    End Function

    ''' <summary>
    ''' 請求日付の取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>請求日付</returns>
    ''' <remarks></remarks>
    Private Function GetSeiqDate(ByVal frm As LMJ010F) As String

        With frm

            If LMJ010C.SHORI_SONOTA.Equals(.cmbShori.SelectedValue.ToString()) = True Then

                GetSeiqDate = .imdSeiqDate.TextValue

            Else

                GetSeiqDate = .cmbSeiqComb.SelectedValue.ToString()

            End If

            Return GetSeiqDate

        End With

    End Function

    ''' <summary>
    ''' 月末在庫コンボ生成処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetSelectData(ByVal frm As LMJ010F) As DataSet

        'コンボデータ取得
        Dim rtnDs As DataSet = Me.SelectGetuData(frm)

        'サーバ日付を保持
        Call Me.SetSysDate(rtnDs)

        'コンボ生成
        Me._G.SetZaikoDateControl(rtnDs)

        Return rtnDs

    End Function

    ''' <summary>
    ''' ファイル出力処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Function OutFileData(ByVal frm As LMJ010F, ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMJ010C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        'データが存在しない場合、スルー
        If max < 0 Then
            MyBase.ShowMessage(frm, "G001")
            Return False
        End If

        Dim setData As StringBuilder = New StringBuilder()

        'タイトル行の設定
        'setData.Append("荷主名,荷主コード,請求日,商品KEY,商品コード(荷主),荷主コード,商品名,ロット番号,シリアル番号,請求在庫個数,,実在庫個数")
        setData.Append("荷主名,荷主コード,請求日,商品KEY,商品コード(荷主),荷主コード,商品名,ロット番号,シリアル番号,請求在庫個数,請求在庫数量,実在庫個数,数量不一致,実在庫数量")

        Dim formatStr As String = "#,##0"
        Dim formatDec As String = "#,##0.000"
        For i As Integer = 0 To max

            '改行の設定
            setData.Append(vbNewLine)

            With dt.Rows(i)

                setData.Append(Me.SetFileData(.Item("CUST_NM").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("CUST_CD").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(DateFormatUtility.EditSlash(.Item("OUTPUT_DATE").ToString())))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("GOODS_CD_NRS").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("GOODS_CD_CUST").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("CUST_CD_DTL").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("GOODS_NM").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("LOT_NO").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("SERIAL_NO").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(Me.SetCommaEditData(.Item("HIKAKU_ZAI_NB").ToString(), formatStr)))
                setData.Append(",")
                setData.Append(Me.SetFileData(Me.SetCommaEditData(.Item("HIKAKU_ZAI_QT").ToString(), formatDec)))
                setData.Append(",")
                setData.Append(Me.SetFileData(Me.SetCommaEditData(.Item("ZAI_NB").ToString(), formatStr)))
                setData.Append(",")
                setData.Append(Me.SetFileData(.Item("UNMATCH").ToString()))
                setData.Append(",")
                setData.Append(Me.SetFileData(Me.SetCommaEditData(.Item("ZAI_QT").ToString(), formatDec)))

            End With

        Next

        '保存先のCSVファイルのパス
        'Dim csvPath As String = String.Concat(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory), "\AAA.csv")
        Dim csvPath As String = String.Concat(System.Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) _
                                                                              , "\", GetPGID().ToString, "_", GetSystemDateTime(0).ToString _
                                                                              , GetSystemDateTime(1).ToString, ".csv")

        'CSVファイルに書き込むときに使うEncoding
        Dim enc As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift_JIS")

        '開く
        Dim sr As StreamWriter = New StreamWriter(csvPath, False, enc)

        '値の設定
        sr.Write(setData.ToString())

        'ファイルを閉じる
        sr.Close()

        '保存したファイルを表示
        System.Diagnostics.Process.Start(csvPath)

        '完了メッセージを表示
        Me._LMJconH.SetMessageG002(frm, frm.cmbPrint.SelectedText, String.Empty)

        Return True

    End Function

    ''' <summary>
    ''' CSVファイル設定前処理
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetFileData(ByVal value As String) As String

        If value.IndexOf(ControlChars.Quote) > -1 _
            OrElse value.IndexOf(","c) > -1 _
            OrElse value.IndexOf(ControlChars.Cr) > -1 _
            OrElse value.IndexOf(ControlChars.Lf) > -1 _
            OrElse value.StartsWith(" ") = True _
            OrElse value.StartsWith(ControlChars.Tab) = True _
            OrElse value.EndsWith(" ") = True _
            OrElse value.EndsWith(ControlChars.Tab) = True _
            Then

            If value.IndexOf(ControlChars.Quote) > -1 Then
                '"を""とする
                value = value.Replace("""", """""")
            End If
            value = """" + value + """"

        End If

        Return value

    End Function

    ''' <summary>
    ''' カンマ編集
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="formatStr">値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCommaEditData(ByVal value As String, ByVal formatStr As String) As String

        Dim aDec As Decimal = New Decimal()

        Dim flg As Boolean = Decimal.TryParse(value, aDec)
        If flg = False Then
            Return value
        End If

        Return aDec.ToString(formatStr)

    End Function

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMJ010F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMJControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMJ010F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMJ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.CreatePrintData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMJ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMJ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' 処理内容変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbShori_SelectedValueChanged(ByVal frm As LMJ010F, ByVal e As System.EventArgs)
        Call Me.ChangeShoriComb(frm)
    End Sub

    ''' <summary>
    ''' 請求日付コンボ変更時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub cmbSeiqComb_SelectedValueChanged(ByVal frm As LMJ010F, ByVal e As System.EventArgs)
        Call Me.ChangeSeiqComb(frm)
    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMJ010F_KeyDown(ByVal frm As LMJ010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub txtCustCdL_Enter(ByVal frm As LMJ010F, ByVal e As System.EventArgs)
        Call Me.UpdatePreInputData(frm.txtCustCdL.TextValue)
    End Sub

    ''' <summary>
    ''' 荷主コード(大)のフォーカスアウトしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdL_Leave(ByVal frm As LMJ010F, ByVal e As System.EventArgs)
        Call Me.LeaveCustData(frm, LMJ010C.ActionType.CUSTL_LEAVE)
    End Sub

    ''' <summary>
    ''' 荷主コード(中)のフォーカスインしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdM_Enter(ByVal frm As LMJ010F, ByVal e As System.EventArgs)
        Call Me.UpdatePreInputData(frm.txtCustCdM.TextValue)
    End Sub

    ''' <summary>
    ''' 荷主コード(中)のフォーカスアウトしたときに発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub txtCustCdM_Leave(ByVal frm As LMJ010F, ByVal e As System.EventArgs)
        Call Me.LeaveCustData(frm, LMJ010C.ActionType.CUSTM_LEAVE)
    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region

End Class