' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI961H : GLIS見積情報照会（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.GL.DSL

''' <summary>
''' LMI961ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI961H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI961V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI961G

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

    ''' <summary>
    ''' 画面間データ
    ''' </summary>
    ''' <remarks></remarks>
    Private _Prm As LMFormData

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
        Me._Prm = prm

        'リターンフラグの初期値
        Me._Prm.ReturnFlg = False

        'フォームの作成
        Dim frm As LMI961F = New LMI961F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI961V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMI961G(Me, frm, Me._LMFconG)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期値設定
        Call Me._G.SetInitValue()

        'メッセージの表示
        Call Me.SetInitMessage(frm)

        '呼び出し元画面情報を設定
        frm.Owner = Application.OpenForms.Item(String.Concat(MyBase.RootPGID(), "F"))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォームの表示
        frm.ShowDialog()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMI961F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI961C.ActionType.KENSAKU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI961C.EventShubetsu.KENSAKU)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '検索条件の設定
        Dim ds As DataSet = New LMI961DS()
        ds.Merge(Me._Prm.ParamDataSet)
        ds.Merge(New GLZ9300DS)
        ds = Me.SetConditionDataSet(ds, frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMI961C.ACTION_ID_SELECT, ds)

        '検索結果
        If MyBase.IsMessageExist() Then
            MyBase.ShowMessage(frm)
        Else
            With rtnDs.Tables("GLZ9300OUT_RESULT").Rows(0)
                If .Item("PROC_STATUS").ToString = "0" Then
                    '正常メッセージ表示
                    MyBase.ShowMessage(frm, "G105", New String() {.Item("MESSAGE").ToString})
                    '値の設定
                    Me._G.SetSpread(rtnDs)
                Else
                    If .Item("MESSAGE").ToString.Contains("E00319") Then
                        '排他エラー時は、分かりやすいメッセージに置き換え
                        MyBase.ShowMessage(frm, "E01V", New String() {"親画面で", ""})
                    Else
                        'エラーメッセージ表示
                        MyBase.ShowMessage(frm, "E01U", New String() {.Item("MESSAGE").ToString})
                    End If
                End If
            End With
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 受注作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JuchuSakusei(ByVal frm As LMI961F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI961C.ActionType.LOOPEDIT)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI961G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '登録情報の設定
        Dim ds As DataSet = New LMI961DS()
        ds.Merge(Me._Prm.ParamDataSet)
        ds.Merge(New GLZ9300DS)
        ds = Me.SetJuchuSakuseiDataSet(ds, frm, Convert.ToInt32(arr(0)))

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMI961C.ACTION_ID_SHUKKA_TOUROKU, ds)


        '登録結果
        If MyBase.IsMessageExist() Then
            MyBase.ShowMessage(frm)
            '処理終了アクション
            Call Me.EndAction(frm)
        Else
            With rtnDs.Tables("GLZ9300OUT_RESULT").Rows(0)
                If .Item("PROC_STATUS").ToString = "0" Then
                    '正常メッセージ表示
                    MyBase.ShowMessage(frm, "G105", New String() {.Item("MESSAGE").ToString})
                    '処理終了アクション
                    Call Me.EndAction(frm)
                    '成功後処理
                    SuccessEnd(.Item("MESSAGE").ToString)
                Else
                    If .Item("MESSAGE").ToString.Contains("E00319") Then
                        '排他エラー時は、分かりやすいメッセージに置き換え
                        MyBase.ShowMessage(frm, "E01V", New String() {"親画面で", ""})
                    Else
                        'エラーメッセージ表示
                        MyBase.ShowMessage(frm, "E01U", New String() {.Item("MESSAGE").ToString})
                    End If
                    '処理終了アクション
                    Call Me.EndAction(frm)
                End If
            End With
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI961F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索部データ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetConditionDataSet(ByVal ds As DataSet, ByVal frm As LMI961F) As DataSet

        Dim dt As DataTable = ds.Tables(LMI961C.TABLE_NM_KENSAKU_IN)
        Dim dr As DataRow = dt.NewRow()

        'ヘッダ項目
        With frm

            dr.Item("EXP_FLG") = .chkYushutsu.GetBinaryValue
            dr.Item("IMP_FLG") = .chkYunyu.GetBinaryValue
            dr.Item("LOCAL_FLG") = .chkKokunai.GetBinaryValue

            dr.Item("EST_NO") = .txtEstNo.TextValue
            dr.Item("EST_NO_EDA") = .txtEstNoEda.TextValue
            dr.Item("FWD_USER_NM") = .txtFwdUserNm.TextValue
            dr.Item("EST_MAKE_USER_NM") = .txtEstMakeUserNm.TextValue
            dr.Item("GOODS_NM") = .txtGoodsNm.TextValue
            dr.Item("SEARCH_REM") = .txtSearchRem.TextValue

        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 受注作成の対象情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetJuchuSakuseiDataSet(ByVal ds As DataSet, ByVal frm As LMI961F, ByVal rowNo As Integer) As DataSet

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        '仮受注作成元見積データ
        Dim dtIn As DataTable = ds.Tables("GLZ9300IN_PROV_BOOK_INS_KEY")
        Dim drIn As DataRow = dtIn.NewRow

        With spr.ActiveSheet

            drIn.Item("EST_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.EST_NO.ColNo))
            drIn.Item("EST_NO_EDA") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.EST_NO_EDA.ColNo))

            If frm.optYuki.Checked Then
                '行き
                drIn.Item("LEG_TYPE_KBN") = "00001"
                If Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.JOB_OUT_EXP_KBN.ColNo)) = "00001" Then
                    drIn.Item("KARI_EXP_IMP") = "1"
                ElseIf Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.JOB_OUT_IMP_KBN.ColNo)) = "00001" Then
                    drIn.Item("KARI_EXP_IMP") = "2"
                ElseIf Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.JOB_LOC_KBN.ColNo)) = "00001" Then
                    drIn.Item("KARI_EXP_IMP") = "1"
                End If
            End If

            If frm.optKaeri.Checked Then
                '帰り
                drIn.Item("LEG_TYPE_KBN") = "00002"
                If Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.JOB_IN_EXP_KBN.ColNo)) = "00001" Then
                    drIn.Item("KARI_EXP_IMP") = "1"
                ElseIf Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.JOB_IN_IMP_KBN.ColNo)) = "00001" Then
                    drIn.Item("KARI_EXP_IMP") = "2"
                End If
            End If

            drIn.Item("EST_SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.SYS_UPD_DATE.ColNo))
            drIn.Item("EST_SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.SYS_UPD_TIME.ColNo))

            drIn.Item("CONT_SEQ") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.CONT_SEQ.ColNo))
            drIn.Item("CONT_LEG_SEQ") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.CONT_LEG_SEQ.ColNo))
            drIn.Item("CARGO_SEQ") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI961G.sprDetailDef.CARGO_SEQ.ColNo))

        End With

        dtIn.Rows.Add(drIn)

        'EDI更新対象データ
        dtIn = ds.Tables("GLZ9300IN_UPD_HWL_STATUS")
        drIn = dtIn.NewRow

        With ds.Tables(LMI961C.TABLE_NM_GAMEN_IN).Rows(0)

            drIn.Item("CRT_DATE") = .Item("CRT_DATE").ToString()
            drIn.Item("FILE_NAME") = .Item("FILE_NAME").ToString()
            drIn.Item("NRS_BR_CD") = .Item("NRS_BR_CD").ToString()
            drIn.Item("SHINCHOKU_KB_JUCHU") = "4" '出荷登録済
            drIn.Item("DEL_KB") = "0" '正常
            drIn.Item("EDI_SYS_UPD_DATE") = .Item("HED_UPD_DATE").ToString()
            drIn.Item("EDI_SYS_UPD_TIME") = .Item("HED_UPD_TIME").ToString()

        End With

        dtIn.Rows.Add(drIn)

        Return ds

    End Function

#End Region 'DataSet設定

    ''' <summary>
    ''' 登録成功後処理
    ''' </summary>
    ''' <param name="message">登録後のGLISのメッセージ</param>
    ''' <remarks></remarks>
    Private Sub SuccessEnd(ByVal message As String)

        '閉じる以外ロック
        Me._G.LockFunctionKeyExceptF12()

        'リターンフラグ設定
        Me._Prm.ReturnFlg = True

        '戻り値のJOB_NOの設定(メッセージから)
        Dim dt As DataTable = Me._Prm.ParamDataSet.Tables(LMI961C.TABLE_NM_GAMEN_OUT)
        Dim dr As DataRow = dt.NewRow
        dr.Item("JOB_NO") = Split(message, "受注番号：")(1)
        dt.Rows.Add(dr)

    End Sub


#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMI961F)

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
    Private Sub EndAction(ByVal frm As LMI961F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' サーバサイド処理の実行
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="eventShubetsu">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function CallServerProcess(ByVal frm As LMI961F, ByVal ds As DataSet, ByVal eventShubetsu As LMI961C.EventShubetsu) As Boolean

        Dim msg As String = String.Empty
        Dim actionId As String = String.Empty
        Dim blfName As String = "LMI961BLF"

        Select Case eventShubetsu
            Case LMI961C.EventShubetsu.SHUKKA_TOUROKU
                '出荷登録
                actionId = LMI961C.ACTION_ID_SHUKKA_TOUROKU
                msg = frm.FunctionKey.F6ButtonName
        End Select

        '確認メッセージ表示
        If Me._LMFconH.SetMessageC001(frm, msg) = False Then
            Return False
        End If

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, blfName, rtnDs) = False Then
            Return False
        End If

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
    Private Function ActionData(ByVal frm As LMI961F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , ByVal blfName As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = MyBase.CallWSA(blfName, actionId, ds)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        'エラーが保持されている場合、False
        If MyBase.IsMessageStoreExist = True Then
            Return False
        End If

        Return True

    End Function

#End Region 'ユーティリティ

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI961F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMIControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMI961F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region 'メッセージ設定

#Region "チェック"

#Region "各処理のチェック"

#End Region '各処理のチェック

#End Region 'チェック

#End Region '内部メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI961F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.JuchuSakusei(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI961F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SelectListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI961F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMI961F_KeyDown(ByVal frm As LMI961F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
