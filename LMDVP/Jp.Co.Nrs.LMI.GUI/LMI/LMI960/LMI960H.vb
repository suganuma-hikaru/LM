' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI960H : 出荷データ確認（ハネウェル）
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
Imports Microsoft.Office.Interop

''' <summary>
''' LMI960ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI960H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI960V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI960G

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

        '画面間データを取得する
        Dim prmDs As DataSet = prm.ParamDataSet

        'フォームの作成
        Dim frm As LMI960F = New LMI960F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI960V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMI960G(Me, frm, Me._LMFconG)

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

        'フォームの表示
        frm.Show()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.KENSAKU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.KENSAKU)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

        '表示最大件数の設定
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble(
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)


        '検索条件の設定
        Dim ds As DataSet = New LMI960DS()
        ds = Me.SetConditionDataSet(ds, frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMI960C.ACTION_ID_SELECT, ds)

        '通常検索の場合
        Dim count As String = String.Empty
        count = MyBase.GetResultCount.ToString()

        '検索処理
        rtnDs = Me.SelectListData(frm, ds, rtnDs, blf, count)
        If rtnDs Is Nothing = True Then
            Exit Sub
        End If

        '値の設定
        Me._G.SetSpread(rtnDs)

        ''ADD S 2020/02/07 010901
        ''検索データの部門(倉庫/ISO)を保存
        'frm.ProcessingBumon = frm.cmbBumon.TextValue
        'Select Case frm.ProcessingBumon
        '    Case LMI960C.CmbBumonItems.Soko
        '        frm.WordsNyuShukka = LMI960C.WordsNyuShukka.ForSoko
        '        frm.WordsShukkaTouroku = LMI960C.WordsShukkaTouroku.ForSoko
        '        frm.WordsNyuShukkaTourokuZumi = LMI960C.JuchuStatusName.NyuShukkaTourokuZumi
        '    Case LMI960C.CmbBumonItems.ISO
        '        frm.WordsNyuShukka = LMI960C.WordsNyuShukka.ForISO
        '        frm.WordsShukkaTouroku = LMI960C.WordsShukkaTouroku.ForISO
        '        frm.WordsNyuShukkaTourokuZumi = LMI960C.JuchuStatusName.JuchuTourokuZumi
        'End Select
        '
        'Call Me._G.ChangeCaption()
        ''ADD E 2020/02/07 010901

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 実績作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JissekiSakusei(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.JISSEKI_SAKUSEI)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsTargetValid(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.JISSEKI_SAKUSEI)    'MOD 2019/03/27

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.JISSEKI_SAKUSEI)    'MOD 2019/03/27

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F1ButtonName))

        '一覧の更新
        'DEL 2020/04/22 012106  rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI960C.EventShubetsu.JISSEKI_SAKUSEI)    'MOD 2019/03/27

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    'ADD S 2019/12/12 009741
    ''' <summary>
    ''' 受注作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub JuchuSakusei(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.JUCHU_SAKUSEI)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidJuchuTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.JUCHU_SAKUSEI)

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.JUCHU_SAKUSEI)

        '終了アクション
        Dim msg As String = frm.FunctionKey.F2ButtonName
        If rtnResult Then
            If MyBase.IsMessageStoreExist Then
                MyBase.ShowMessage(frm, "G002", {msg, "一部のデータでエラーが発生しました。詳細はエクセルを参照してください。"})
                MyBase.MessageStoreDownload()
            Else
                MyBase.ShowMessage(frm, "G002", {msg, ""})
            End If
        End If

        '一覧の更新
        'DEL 2020/04/22 012106  rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI960C.EventShubetsu.JUCHU_SAKUSEI)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub
    'ADD E 2019/12/12 009741

    ''' <summary>
    ''' 遅延送信処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub DelayReasonSakusei(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.DELAY_SAKUSEI)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidDelayTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.DELAY_SAKUSEI)

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.DELAY_SAKUSEI)

        '終了アクション
        Dim msg As String = frm.FunctionKey.F3ButtonName
        If rtnResult Then
            If MyBase.IsMessageStoreExist Then
                MyBase.ShowMessage(frm, "G002", {msg, "一部のデータでエラーが発生しました。詳細はエクセルを参照してください。"})
                MyBase.MessageStoreDownload()
            Else
                MyBase.ShowMessage(frm, "G002", {msg, ""})
            End If
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    'ADD S 2020/02/27 010901
    ''' <summary>
    ''' 荷主自動振り分け
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCustAuto(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.UPDATE_CUST_AUTO)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidUpdateCustTarget(frm, arr, LMI960C.EventShubetsu.UPDATE_CUST_AUTO)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.UPDATE_CUST_AUTO)

        '確認メッセージ表示
        Dim msg As String = "【出荷用】荷主自動振分"
        rtnResult = rtnResult AndAlso Me._LMFconH.SetMessageC001(frm, msg)

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, "UpdateCustAuto", "LMI960BLF", rtnDs)

        If rtnResult Then
            MyBase.ShowMessage(frm, "G002", {msg, ""})

            If MyBase.IsMessageStoreExist Then
                MyBase.ShowMessage(frm, "G002", {msg, "一部のデータでエラーが発生しました。詳細はエクセルを参照してください。"})
                MyBase.MessageStoreDownload()

            Else
                Dim dtProcResult As DataTable = rtnDs.Tables("LMI960PROC_RESULT")
                Dim drProcResult As DataRow = rtnDs.Tables("LMI960PROC_RESULT").Rows(0)

                If CInt(drProcResult("SKIP").ToString) > 0 Then

                    '荷主を一意に絞れなかったデータの処理
                    Dim ngCnt As Integer = 0

                    For Each row As DataRow In rtnDs.Tables("LMI960SAKUSEI_TARGET").Rows

                        Dim rowNo As Integer = CInt(row("ROW_NO").ToString)
                        Dim narrowDownList As String = row("NARROW_DOWN_LIST").ToString()

                        If String.IsNullOrEmpty(narrowDownList) Then
                            '自動振分対象だった

                        ElseIf "対象外".Equals(narrowDownList) Then
                            '自動振分対象外だった
                            ngCnt += 1

                        Else
                            '自動振分できなかった
                            '子画面で荷主を選択させる
                            If Me.ChooseCustAuto(frm, rowNo, ds, narrowDownList) Then

                                'データセット設定
                                Dim arrOne As ArrayList = New ArrayList
                                arrOne.Add(rowNo)
                                Dim rtnRet As Boolean = Me.SetJissekiSakuseiTarget(frm, ds, arrOne, LMI960C.EventShubetsu.UPDATE_CUST_MANUAL)

                                'サーバアクセス
                                rtnDs = Nothing
                                rtnRet = rtnRet AndAlso Me.ActionData(frm, ds, "UpdateCustManual", "LMI960BLF", rtnDs)

                            Else
                                ngCnt += 1
                            End If

                        End If

                    Next

                    If ngCnt > 0 Then
                        MyBase.ShowMessage(frm, "G002", {msg, "一部のデータは自動振分できませんでした。個別に荷主手動振分を実行してください。"})
                    End If

                End If

            End If

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 荷主選択
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rowNo"></param>
    ''' <param name="outDs"></param>
    ''' <param name="narrowDownList"></param>
    ''' <returns></returns>
    Private Function ChooseCustAuto(ByVal frm As LMI960F, ByVal rowNo As Integer, ByRef outDs As DataSet, narrowDownList As String) As Boolean

        Dim prm As LMFormData = New LMFormData
        Dim prmDs As DataSet = New LMI963DS
        Dim row As DataRow = prmDs.Tables(LMI963C.TABLE_NM_IN).NewRow
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim skipSetData As Boolean = False

        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        row("CUST_NM_L") = "ハネウェル"
        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        row("HYOJI_KBN") = LMZControlC.HYOJI_S
        row("LOAD_NUMBER") = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo))
        row("ROW_INDEX") = rowNo.ToString
        row("SHUKKA_DATE") = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo))
        row("NONYU_DATE") = GetDatePart(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.NONYU_DATE.ColNo)))
        row("SHUKKA_MOTO") = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SHUKKA_MOTO.ColNo))
        row("NONYU_SAKI") = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.NONYU_SAKI.ColNo))
        row("NARROW_DOWN_LIST") = narrowDownList

        If skipSetData = False Then
            prmDs.Tables(LMI963C.TABLE_NM_IN).Rows.Add(row)
            prm.ParamDataSet = prmDs
            prm.SkipFlg = True  '画面表示フラグ  True:検索結果が1件でも表示する
            skipSetData = True
        End If

        'POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMI963", prm)

        If prm.ReturnFlg = False Then
            Return False
        End If

        '選択された荷主コードをデータセットに追加
        Dim ds260 As DataSet = New LMZ260DS
        outDs.Merge(ds260)
        outDs.Tables("LMZ260OUT").Clear()

        Dim dr As DataRow = outDs.Tables("LMZ260OUT").NewRow()
        dr("CUST_CD_L") = prm.ParamDataSet.Tables(LMI963C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_L").ToString
        dr("CUST_CD_M") = prm.ParamDataSet.Tables(LMI963C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_M").ToString
        outDs.Tables("LMZ260OUT").Rows.Add(dr)

        Return True

    End Function

    ''' <summary>
    ''' 荷主手動振り分け
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpdateCustManual(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.UPDATE_CUST_MANUAL)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '1件のみ選択可能
        rtnResult = rtnResult AndAlso Me._V.IsOneItemSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidUpdateCustTarget(frm, arr, LMI960C.EventShubetsu.UPDATE_CUST_MANUAL)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        Dim ds As DataSet = New LMI960DS()

        '荷主選択
        If Me.ChooseCust(frm, ds) = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.UPDATE_CUST_MANUAL)

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, "UpdateCustManual", "LMI960BLF", rtnDs)

        If rtnResult Then MyBase.ShowMessage(frm, "G002", {"荷主手動振分", ""})

        '一覧の更新
        'DEL 2020/04/22 012106  rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI960C.EventShubetsu.UPDATE_CUST_MANUAL, rtnDs)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 荷主選択
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="outDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChooseCust(ByVal frm As LMI960F, ByRef outDs As DataSet) As Boolean


        Dim prm As LMFormData = New LMFormData
        Dim prmDs As DataSet = New LMZ260DS
        Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        row("CUST_NM_L") = "ハネウェル"
        row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        row("HYOJI_KBN") = LMZControlC.HYOJI_S
        prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = True  '画面表示フラグ  True:検索結果が1件でも表示する

        'POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        If prm.ReturnFlg = False Then
            Return False
        End If

        Dim custCDL As String = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_L").ToString
        Dim custCDM As String = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_M").ToString

        Select Case frm.cmbEigyo.SelectedValue.ToString
            Case LMI960C.NrsBrCd.Chiba  '千葉
                Select Case custCDL
                    Case "00630", "00632", "20630", "70630", "80630"
                        If custCDM = "00" Then
                            '対象荷主である場合
                            outDs.Merge(prm.ParamDataSet)
                            Return True
                        End If
                End Select

            Case LMI960C.NrsBrCd.Forwarding  'フォワーディング
                Select Case custCDL
                    Case "50630"
                        If custCDM = "00" Then
                            '対象荷主である場合
                            outDs.Merge(prm.ParamDataSet)
                            Return True
                        End If
                End Select

        End Select

        '対象荷主でない場合
        MyBase.ShowMessage(frm, "E209", {"選択しなおしてください。"})
        Return False

    End Function

    'ADD E 2020/02/27 010901

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' 出荷登録処理（倉庫）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ShukkaTouroku(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.SHUKKA_TOUROKU)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidNyuShukkaTourokuTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.SHUKKA_TOUROKU)

        Dim msg As String = frm.WordsShukkaTouroku

        If rtnResult Then
            Dim horyuKb As Boolean = False
            Dim changingInoutKb As Boolean = False
            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max
                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.HORYU_KB.ColNo)) = LMI960C.DelKbName.Horyu Then
                    '「保留」の場合
                    horyuKb = True
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)) <> LMI960C.InOutKbName.Mitei AndAlso
                   Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)) <> LMI960C.InOutKbName.Outka Then
                    '入出荷区分が一致しない場合
                    changingInoutKb = True
                End If
            Next

            '確認メッセージ表示
            If Not horyuKb AndAlso Not changingInoutKb Then
                If Me._LMFconH.SetMessageC001(frm, msg) = False Then
                    rtnResult = False
                End If
            Else
                If horyuKb Then
                    If MyBase.ShowMessage(frm, "W295", {msg, ""}) <> MsgBoxResult.Ok Then
                        rtnResult = False
                    End If
                End If

                If rtnResult AndAlso changingInoutKb Then
                    If MyBase.ShowMessage(frm, "C001", {"前回、入荷または輸送登録したデータが含まれています。" & msg, ""}) <> MsgBoxResult.Ok Then
                        rtnResult = False
                    End If
                End If
            End If
        End If

        Dim rtnDs As DataSet = Nothing

        'サーバアクセス
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, LMI960C.ACTION_ID_SHUKKA_TOUROKU, "LMI960BLF", rtnDs)

        If rtnResult Then
            If rtnDs.Tables(LMI960C.TABLE_NM_WARNING_DTL).Rows.Count > 0 Then
                'ワーニングが設定されている場合

                MyBase.ClearMessageStoreData()

                If MyBase.IsMessageStoreExist Then
                    MyBase.ShowMessage(frm, "E235")
                    MyBase.MessageStoreDownload()
                End If

                'ワーニング画面呼出
                Call Me.CallWarning(ds, rtnDs, frm, LMI960C.InOutKb.Outka)

                MyBase.ShowMessage(frm, "G002", {msg, ""})
            Else
                If MyBase.IsMessageStoreExist Then
                    MyBase.ShowMessage(frm, "G002", {msg, "一部のデータでエラーが発生しました。詳細はエクセルを参照してください。"})
                    MyBase.MessageStoreDownload()
                Else
                    MyBase.ShowMessage(frm, "G002", {msg, ""})
                End If
            End If
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 入荷登録処理（倉庫）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub NyukaTouroku(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.NYUKA_TOUROKU)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidNyuShukkaTourokuTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.NYUKA_TOUROKU)

        Dim msg As String = frm.FunctionKey.F11ButtonName

        If rtnResult Then
            Dim horyuKb As Boolean = False
            Dim changingInoutKb As Boolean = False
            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max
                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.HORYU_KB.ColNo)) = LMI960C.DelKbName.Horyu Then
                    '「保留」の場合
                    horyuKb = True
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)) <> LMI960C.InOutKbName.Mitei AndAlso
                   Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)) <> LMI960C.InOutKbName.Inka Then
                    '入出荷区分が一致しない場合
                    changingInoutKb = True
                End If
            Next

            '確認メッセージ表示
            If Not horyuKb AndAlso Not changingInoutKb Then
                If Me._LMFconH.SetMessageC001(frm, msg) = False Then
                    rtnResult = False
                End If
            Else
                If horyuKb Then
                    If MyBase.ShowMessage(frm, "W295", {msg, ""}) <> MsgBoxResult.Ok Then
                        rtnResult = False
                    End If
                End If

                If rtnResult AndAlso changingInoutKb Then
                    If MyBase.ShowMessage(frm, "C001", {"前回、出荷または輸送登録したデータが含まれています。" & msg, ""}) <> MsgBoxResult.Ok Then
                        rtnResult = False
                    End If
                End If
            End If
        End If

        Dim rtnDs As DataSet = Nothing

        'サーバアクセス
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, LMI960C.ACTION_ID_NYUKA_TOUROKU, "LMI960BLF", rtnDs)

        If rtnResult Then
            If rtnDs.Tables(LMI960C.TABLE_NM_WARNING_DTL).Rows.Count > 0 Then
                'ワーニングが設定されている場合

                MyBase.ClearMessageStoreData()

                If MyBase.IsMessageStoreExist Then
                    MyBase.ShowMessage(frm, "G106", {"一部のデータで警告が発生しました。詳細はエクセルを参照してください。"})
                    MyBase.MessageStoreDownload()
                End If

                'ワーニング画面呼出
                Call Me.CallWarning(ds, rtnDs, frm, LMI960C.InOutKb.Inka)

                MyBase.ShowMessage(frm, "G002", {msg, ""})
            Else
                If MyBase.IsMessageStoreExist Then
                    MyBase.ShowMessage(frm, "G002", {msg, "一部のデータで警告が発生しました。詳細はエクセルを参照してください。"})
                    MyBase.MessageStoreDownload()
                Else
                    MyBase.ShowMessage(frm, "G002", {msg, ""})
                End If
            End If
        End If


        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' ワーニング画面呼出処理
    ''' </summary>
    ''' <param name="orgDs">LMI960BLFの引数のデータセット</param>
    ''' <param name="rtnDs">LMI960BLFの戻り値のデータセット</param>
    ''' <param name="frm">フォーム</param>
    ''' <param name="inoutKb">入出荷区分</param>
    ''' <remarks></remarks>
    Private Sub CallWarning(ByVal orgDs As DataSet, ByVal rtnDs As DataSet, ByVal frm As LMI960F, ByVal inoutKb As String)

        'LMI960INの設定
        rtnDs.Tables(LMI960C.TABLE_NM_IN).Merge(orgDs.Tables(LMI960C.TABLE_NM_IN))

        'WARNING_HEDの設定
        Dim dtWarnHed As DataTable = rtnDs.Tables(LMI960C.TABLE_NM_WARNING_HED)
        dtWarnHed.Clear()
        Dim drWarnHed As DataRow = dtWarnHed.NewRow()
        drWarnHed.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        drWarnHed.Item("INOUT_KB") = inoutKb
        dtWarnHed.Rows.Add(drWarnHed)

        Dim prm As LMFormData = New LMFormData
        prm.ParamDataSet = rtnDs

        'ワーニング画面を表示
        LMFormNavigate.NextFormNavigate(Me, "LMI962", prm)

    End Sub

    ''' <summary>
    '''GLIS受注登録処理（ISO）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub GlisJuchuTouroku(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.SHUKKA_TOUROKU)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '1件のみ選択可能
        rtnResult = rtnResult AndAlso Me._V.IsOneItemSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidNyuShukkaTourokuTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'GLIS受注登録処理
        rtnResult = rtnResult AndAlso Me.GlisJuchuTouroku(frm, arr)

        '一覧の更新
        'DEL 2020/04/22 012106  rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI960C.EventShubetsu.SHUKKA_TOUROKU)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' GLIS受注登録処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function GlisJuchuTouroku(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        Dim spr As Win.Spread.LMSpread = frm.sprDetail

        With spr.ActiveSheet

            'スプレッドの行番号
            Dim rowNo As Integer = Convert.ToInt32(arr(0))

            Dim jobNo As String = String.Empty

            Dim shipmentID As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo))
            Dim horyuKb As Boolean = (Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HORYU_KB.ColNo)) = LMI960C.DelKbName.Horyu)

            Dim inDs As GLZ9300DS = New GLZ9300DS
            Dim inDt As GLZ9300DS.GLZ9300IN_BOOK_REF_KEYDataTable = inDs.GLZ9300IN_BOOK_REF_KEY
            Dim inDr As GLZ9300DS.GLZ9300IN_BOOK_REF_KEYRow = inDt.NewGLZ9300IN_BOOK_REF_KEYRow
            inDr.SHIPMENT_ID = shipmentID
            inDt.AddGLZ9300IN_BOOK_REF_KEYRow(inDr)

            Dim outDs As DataSet = New DataSet

            'GLIS受注存在チェック
            If Me.ActionData(frm, inDs, "IsExistBookingForHwl", "LMI960BLF", outDs) = False Then
                'エラーがある場合、終了
                Return False
            End If

            Dim outDt As DataTable = outDs.Tables("GLZ9300OUT_RESULT")
            Dim outDr As DataRow = outDt(0)

            Select Case outDr("PROC_STATUS").ToString
                Case "0"  '存在しない
                    '確認メッセージ表示
                    If horyuKb AndAlso MyBase.ShowMessage(frm, "W295", {frm.WordsShukkaTouroku, ""}) <> MsgBoxResult.Ok Then
                        Return False
                    End If

                    '遷移先画面のデータセット
                    Dim prmDs As New LMI961DS
                    Dim prmDt As LMI961DS.LMI961GAMEN_INDataTable = prmDs.LMI961GAMEN_IN
                    Dim prmDr As LMI961DS.LMI961GAMEN_INRow = prmDt.NewLMI961GAMEN_INRow

                    prmDr.NRS_BR_CD = frm.cmbEigyo.SelectedValue.ToString
                    prmDr.SHIPMENT_ID = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo))
                    prmDr.CRT_DATE = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_CRT_DATE.ColNo)))
                    prmDr.FILE_NAME = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_FILE_NAME.ColNo))
                    prmDr.HED_UPD_DATE = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_UPD_DATE.ColNo))
                    prmDr.HED_UPD_TIME = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_UPD_TIME.ColNo))

                    prmDt.AddLMI961GAMEN_INRow(prmDr)

                    Dim prm As LMFormData = New LMFormData
                    prm.ReturnFlg = False
                    prm.ParamDataSet = prmDs
                    prm.RecStatus = RecordStatus.NOMAL_REC

                    '画面遷移
                    LMFormNavigate.NextFormNavigate(Me, "LMI961", prm)

                    If prm.ReturnFlg = False Then
                        Return False
                    End If

                    'JOB NOの取得
                    jobNo = prm.ParamDataSet.Tables("LMI961GAMEN_OUT").Rows(0).Item("JOB_NO").ToString

                Case "1"  '存在する
                    '確認メッセージ表示
                    If horyuKb Then
                        If MyBase.ShowMessage(frm, "W295", {frm.WordsShukkaTouroku, ""}) <> MsgBoxResult.Ok Then
                            Return False
                        End If
                    Else
                        If Me._LMFconH.SetMessageC001(frm, frm.WordsShukkaTouroku) = False Then
                            Return False
                        End If
                    End If

                    'データセット設定
                    Dim ds As GLZ9300DS = New GLZ9300DS
                    ds.Merge(New LMI960DS)
                    Call Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.SHUKKA_TOUROKU)

                    Dim dt As GLZ9300DS.GLZ9300IN_UPD_HWL_STATUSDataTable = ds.GLZ9300IN_UPD_HWL_STATUS
                    Dim dr As GLZ9300DS.GLZ9300IN_UPD_HWL_STATUSRow = dt.NewGLZ9300IN_UPD_HWL_STATUSRow

                    dr.CRT_DATE = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_CRT_DATE.ColNo)))
                    dr.FILE_NAME = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_FILE_NAME.ColNo)))
                    dr.NRS_BR_CD = frm.cmbEigyo.SelectedValue.ToString()
                    dr.SHINCHOKU_KB_JUCHU = CStr(CInt(LMI960C.ShinchokuKbJuchu.NyuShukkaTourokuZumi))
                    dr.DEL_KB = "0"  '0:正常
                    dr.EDI_SYS_UPD_DATE = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_UPD_DATE.ColNo))
                    dr.EDI_SYS_UPD_TIME = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_UPD_TIME.ColNo))

                    dt.AddGLZ9300IN_UPD_HWL_STATUSRow(dr)

                    outDs = New DataSet

                    'GLIS受注存在チェック
                    If Me.ActionData(frm, ds, "UpdBookingForHwl", "LMI960BLF", outDs) = False Then
                        'エラーがある場合、終了
                        Return False
                    End If

                    outDt = outDs.Tables("GLZ9300OUT_RESULT")
                    outDr = outDt(0)

                    Select Case outDr("PROC_STATUS").ToString
                        Case "0"  'Success
                            MyBase.ShowMessage(frm, "G105", {outDr("MESSAGE").ToString})

                        Case Else
                            'GLISの処理がエラーの場合

                            Dim msg As String = outDr("MESSAGE").ToString

                            If msg.Contains("E00319") Then
                                '対象データは他のユーザによって変更されています。
                                MyBase.ShowMessage(frm, "E011")
                            Else
                                MyBase.ShowMessage(frm, "E01U", {msg})
                            End If

                            Return False

                    End Select

                    'JOB NOの取得
                    jobNo = outDs.Tables("GLZ9300IN_BOOK_UPD_KEY").Rows(0).Item("JOB_NO").ToString

                Case Else
                    'GLISの処理がエラーの場合
                    MyBase.ShowMessage(frm, "E01U", {outDr("MESSAGE").ToString})
                    Return False

            End Select

            'JOB NOの表示
            spr.SetCellValue(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo, jobNo)

        End With

        Return True

    End Function

    ''' <summary>
    '''GLIS受注削除処理（ISO）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub GlisJuchuSakujo(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.DELETE_GLIS_JUCHU)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '1件のみ選択可能
        rtnResult = rtnResult AndAlso Me._V.IsOneItemSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidGLISJuchuSakujoTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As GLZ9300DS = New GLZ9300DS
        ds.Merge(New LMI960DS)
        Call Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.DELETE_GLIS_JUCHU)

        Dim outDs As DataSet = New DataSet

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.DELETE_GLIS_JUCHU, outDs)

        If rtnResult Then

            Dim outDt As DataTable = outDs.Tables("GLZ9300OUT_RESULT")
            Dim outDr As DataRow = outDt(0)

            Select Case outDr("PROC_STATUS").ToString

                Case "0"  'Success
                    MyBase.ShowMessage(frm, "G105", {outDr("MESSAGE").ToString})

                Case Else
                    'GLISの処理がエラーの場合

                    Dim msg As String = outDr("MESSAGE").ToString

                    If msg.Contains("E00319") Then
                        '対象データは他のユーザによって変更されています。
                        MyBase.ShowMessage(frm, "E011")
                    Else
                        MyBase.ShowMessage(frm, "E01U", {msg})
                    End If

            End Select

        End If

        '一覧の更新
        'DEL 2020/04/22 012106  rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI960C.EventShubetsu.DELETE_GLIS_JUCHU)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' StopNote表示
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OutputStopNote(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.KENSAKU)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        Dim max As Integer = arr.Count - 1
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0

        Dim titleData As String() = {"行番号", "Load Number", "HNW SAP Order No", "SKU Number", "Number pieces", "Stop Type", "Location ID", "Stop Note"}

        Dim rowDataList As New List(Of Object())
        Dim groupEndRowList As New List(Of Integer)
        Dim rowIdx As Integer = 0

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            Dim sapOrdNo As String()
            If frm.ProcessingBumon = LMI960C.CmbBumonItems.Soko OrElse frm.ProcessingBumon = LMI960C.CmbBumonItems.ChilledLorry Then
                sapOrdNo = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.CUST_ORD_NO.ColNo)).Split(","c)
            Else
                sapOrdNo = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SAP_ORD_NO.ColNo)).Split(","c)
            End If
            Dim skuNum As String() = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SKU_NUMBER.ColNo)).Split(","c)
            Dim pieces As String() = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.NUMBER_PIECES.ColNo)).Split(","c)

            '出力行の追加(1行目)
            rowIdx += 1
            rowDataList.Add({
                            rowNo,
                            Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo)),
                            sapOrdNo(0),
                            skuNum(0),
                            pieces(0),
                            "P",
                            Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SHUKKA_MOTO_CD.ColNo)),
                            Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.P_STOP_NOTE.ColNo))
                            })

            '出力行の追加(2行目)
            rowIdx += 1
            rowDataList.Add({
                            "",
                            "",
                            If(sapOrdNo.Length > 1, sapOrdNo(1), ""),
                            If(skuNum.Length > 1, skuNum(1), ""),
                            If(pieces.Length > 1, pieces(1), ""),
                            "D",
                            Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.NONYU_SAKI_CD.ColNo)),
                            Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.D_STOP_NOTE.ColNo))
                            })

            'SAP Order NoまたはSKU Numberが3個以上ある場合は3行目以降に出力
            If sapOrdNo.Length >= 3 OrElse skuNum.Length >= 3 Then

                Dim maxIdx As Integer = If(sapOrdNo.Length > skuNum.Length, sapOrdNo.Length, skuNum.Length) - 1

                For j As Integer = 2 To maxIdx
                    '出力行の追加
                    rowIdx += 1
                    rowDataList.Add({
                                    "",
                                    "",
                                    If(sapOrdNo.Length > j, sapOrdNo(j), ""),
                                    If(skuNum.Length > j, skuNum(j), ""),
                                    If(pieces.Length > j, pieces(j), ""),
                                    "",
                                    "",
                                    ""
                                    })
                Next
            End If

            groupEndRowList.Add(rowIdx)

        Next

        'String配列のListから2次元配列に転記
        Dim rowDataArray(rowDataList.Count - 1, titleData.Length - 1) As Object
        rowIdx = -1
        For Each rowData As Object() In rowDataList
            rowIdx += 1
            For colIdx As Integer = 0 To titleData.Length - 1
                rowDataArray(rowIdx, colIdx) = rowData(colIdx)
            Next
        Next

        'Excel出力
        Call Me.OutputExcelForStopNote("StopNote", titleData, rowDataArray, groupEndRowList)

        MyBase.ShowMessage(frm, "G002", {frm.FunctionKey.F8ButtonName, "エクセルを参照してください。"})

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' Excel出力(StopNote用)
    ''' </summary>
    ''' <param name="sheetName">シート名</param>
    ''' <param name="titleData">タイトル行データ</param>
    ''' <param name="bodyData">明細行データ</param>
    ''' <param name="groupEndRowList">セル結合する範囲ごとの最終行のリスト</param>
    Private Sub OutputExcelForStopNote(ByVal sheetName As String, ByVal titleData As String(), ByVal bodyData As Object(,), ByVal groupEndRowList As List(Of Integer))

        Const HeaderRow As Integer = 1
        Const BodyStartRow As Integer = HeaderRow + 1

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        Try
            'EXCEL開始
            xlApp = New Excel.Application
            xlApp.DisplayAlerts = False
            xlBooks = xlApp.Workbooks
            xlBook = xlBooks.Add()

            '作業シート設定
            xlSheets = xlBook.Worksheets
            xlSheet = DirectCast(xlSheets(1), Excel.Worksheet)
            xlSheet.Name = sheetName

            '全セルの表示形式を文字列に設定
            xlSheet.Cells.NumberFormat = "@"

            '表示形式の設定
            xlSheet.Range("A:A").NumberFormat = "General"
            xlSheet.Range("E:E").NumberFormat = "General"

            'ヘッダ(1行)の値の設定
            startCell = DirectCast(xlSheet.Cells(HeaderRow, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(HeaderRow, titleData.Length), Excel.Range)
            range = xlSheet.Range(startCell, endCell)
            range.Value = titleData
            'ヘッダ(1行)の罫線の設定
            range.Borders.Item(Excel.XlBordersIndex.xlEdgeTop).LineStyle = Excel.XlLineStyle.xlContinuous
            range.Borders.Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous

            'ボディの値の設定
            startCell = DirectCast(xlSheet.Cells(BodyStartRow, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(BodyStartRow + bodyData.GetLength(0) - 1, bodyData.GetLength(1)), Excel.Range)
            range = xlSheet.Range(startCell, endCell)
            range.Value = bodyData

            'ボディのセル結合・罫線
            For idx As Integer = 0 To groupEndRowList.Count - 1

                Dim startRow As Integer
                Dim endRow As Integer

                If idx = 0 Then
                    startRow = BodyStartRow
                Else
                    startRow = HeaderRow + groupEndRowList(idx - 1) + 1
                End If
                endRow = HeaderRow + groupEndRowList(idx)

                'A列のセル結合
                startCell = DirectCast(xlSheet.Cells(startRow, 1), Excel.Range)
                endCell = DirectCast(xlSheet.Cells(endRow, 1), Excel.Range)
                range = xlSheet.Range(startCell, endCell)
                range.Merge()

                'B列のセル結合
                startCell = DirectCast(xlSheet.Cells(startRow, 2), Excel.Range)
                endCell = DirectCast(xlSheet.Cells(endRow, 2), Excel.Range)
                range = xlSheet.Range(startCell, endCell)
                range.Merge()

                '罫線
                startCell = DirectCast(xlSheet.Cells(startRow, 1), Excel.Range)
                endCell = DirectCast(xlSheet.Cells(endRow, titleData.Length), Excel.Range)
                range = xlSheet.Range(startCell, endCell)
                range.Borders.Item(Excel.XlBordersIndex.xlEdgeBottom).LineStyle = Excel.XlLineStyle.xlContinuous
                range.Borders.Item(Excel.XlBordersIndex.xlEdgeBottom).Weight = Excel.XlBorderWeight.xlHairline

            Next

            '列幅の調整
            xlSheet.Range("A:G").EntireColumn.AutoFit()

            xlApp.Visible = True

        Finally
            '参照の開放

            If xlSheet IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
            End If

            If xlSheets IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheets)
                xlSheets = Nothing
            End If

            If xlBook IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
            End If

            If xlBooks IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
            End If

            If xlApp IsNot Nothing Then
                xlApp.DisplayAlerts = True
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If

        End Try

    End Sub

    ''' <summary>
    ''' シリンダー取込
    ''' </summary>
    Private Sub ImportCylinder(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.IMPORT_CYLINDER)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '1件のみ選択可能
        rtnResult = rtnResult AndAlso Me._V.IsOneItemSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidTargetOfImportCylinder(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        Dim outFilePath As String = ""
        If Not GetFilePathInteractive(outFilePath) Then
            'ユーザがキャンセルした場合
            Call Me.EndAction(frm)
            Exit Sub
        End If

        Dim outExcelData(,) As Object = Nothing
        If Not RoladCylinderExcel(outFilePath, outExcelData) Then
            'ファイルオープンに失敗した場合
            MyBase.ShowMessage(frm)
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'Excel内のシリアルNoの列番号
        Const SerialColNo As Integer = 9

        If UBound(outExcelData, 2) < SerialColNo Then
            'ExcelにシリアルNoの列が存在しない場合
            MyBase.ShowMessage(frm, "E492", {"取込ファイル", "I列（シリアル）", ""})
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'シリアルNo
        Dim serial As String = ""
        For row As Integer = LBound(outExcelData, 1) To UBound(outExcelData, 1)
            If Not String.IsNullOrWhiteSpace(CStr(outExcelData(row, SerialColNo))) Then
                serial = String.Concat(serial, ",", outExcelData(row, SerialColNo).ToString)
            End If
        Next
        If serial <> "" Then
            serial = serial.Substring(1)
        End If

        If serial = "" Then
            'シリアルNoが空の場合
            MyBase.ShowMessage(frm, "E492", {"取込ファイルのI列（シリアル）", "値", ""})
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.IMPORT_CYLINDER, serial)

        'サーバアクセス
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, "ImportCylinder", "LMI960BLF")

        If rtnResult Then MyBase.ShowMessage(frm, "G002", {frm.FunctionKey.F10ButtonName, ""})

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 運送登録処理（倉庫）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UnsoTouroku(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.UNSO_TOUROKU)

        'チェックボックスの確認
        Dim arr As ArrayList = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidNyuShukkaTourokuTarget(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.SHUKKA_TOUROKU)

        Dim msg As String = frm.btnUnsoTouroku.Text

        If rtnResult Then
            Dim horyuKb As Boolean = False
            Dim changingInoutKb As Boolean = False
            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max
                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.HORYU_KB.ColNo)) = LMI960C.DelKbName.Horyu Then
                    '「保留」の場合
                    horyuKb = True
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)) <> LMI960C.InOutKbName.Mitei AndAlso
                   Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)) <> LMI960C.InOutKbName.Unso Then
                    '入出荷区分が一致しない場合
                    changingInoutKb = True
                End If
            Next

            '確認メッセージ表示
            If Not horyuKb AndAlso Not changingInoutKb Then
                If Me._LMFconH.SetMessageC001(frm, msg) = False Then
                    rtnResult = False
                End If
            Else
                If horyuKb Then
                    If MyBase.ShowMessage(frm, "W295", {msg, ""}) <> MsgBoxResult.Ok Then
                        rtnResult = False
                    End If
                End If

                If rtnResult AndAlso changingInoutKb Then
                    If MyBase.ShowMessage(frm, "C001", {"前回、入荷または出荷登録したデータが含まれています。" & msg, ""}) <> MsgBoxResult.Ok Then
                        rtnResult = False
                    End If
                End If
            End If
        End If

        Dim rtnDs As DataSet = Nothing

        'サーバアクセス
        rtnResult = rtnResult AndAlso Me.ActionData(frm, ds, LMI960C.ACTION_ID_UNSO_TOUROKU, "LMI960BLF", rtnDs)

        If rtnResult Then
            If rtnDs.Tables(LMI960C.TABLE_NM_WARNING_DTL).Rows.Count > 0 Then
                'ワーニングが設定されている場合

                MyBase.ClearMessageStoreData()

                If MyBase.IsMessageStoreExist Then
                    MyBase.ShowMessage(frm, "E235")
                    MyBase.MessageStoreDownload()
                End If

                'ワーニング画面呼出
                Call Me.CallWarning(ds, rtnDs, frm, LMI960C.InOutKb.Unso)

                MyBase.ShowMessage(frm, "G002", {msg, ""})
            Else
                If MyBase.IsMessageStoreExist Then
                    MyBase.ShowMessage(frm, "G002", {msg, "一部のデータでエラーが発生しました。詳細はエクセルを参照してください。"})
                    MyBase.MessageStoreDownload()
                Else
                    MyBase.ShowMessage(frm, "G002", {msg, ""})
                End If
            End If
        End If

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    'ADD START 2019/03/27
    ''' <summary>
    ''' 一括変更処理【出荷日・納入日】
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub IkkatsuChange(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.IKKATSU_CHANGE)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidTargetOfIkattsuChange(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.IKKATSU_CHANGE)

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.IKKATSU_CHANGE)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）")

        '一覧の更新
        'DEL 2020/04/22 012106  rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr, LMI960C.EventShubetsu.IKKATSU_CHANGE)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub
    'ADD END   2019/03/27

    ''' <summary>
    ''' 受注ステータス戻し処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub RollbackJuchuStatus(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.ROLLBACK_JUCHU_STATUS)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidTargetOfRollbackJuchuStatus(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.ROLLBACK_JUCHU_STATUS)

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.ROLLBACK_JUCHU_STATUS)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' JOB NO変更処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ModJobNo(ByVal frm As LMI960F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(LMI960C.EventShubetsu.MOD_JOB_NO)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '1件のみ選択可能
        rtnResult = rtnResult AndAlso Me._V.IsOneItemSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidTargetOfModJobNo(frm, arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, LMI960C.EventShubetsu.MOD_JOB_NO)

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, LMI960C.EventShubetsu.MOD_JOB_NO)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 未処理⇔EDI取消処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub EdiTorikeshi(ByVal frm As LMI960F, ByVal eventShubetsu As LMI960C.EventShubetsu)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMI960C.ActionType.LOOPEDIT)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(eventShubetsu)

        'チェックボックスの確認
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMI960G.sprDetailDef.DEF.ColNo)
        End If

        'バリデーションチェック
        rtnResult = rtnResult AndAlso Me._V.IsTargetSelected(arr)

        '選択レコードのチェック
        rtnResult = rtnResult AndAlso Me.IsValidTargetOfEdiTorikeshi(frm, arr, eventShubetsu)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'データセット設定
        Dim ds As DataSet = New LMI960DS()
        rtnResult = rtnResult AndAlso Me.SetJissekiSakuseiTarget(frm, ds, arr, eventShubetsu)

        'サーバサイド処理
        rtnResult = rtnResult AndAlso Me.CallServerProcess(frm, ds, eventShubetsu)

        '終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）")

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Private Function SetConditionDataSet(ByVal ds As DataSet, ByVal frm As LMI960F) As DataSet

        Dim dt As DataTable = ds.Tables(LMI960C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("BUMON") = .cmbBumon.TextValue
            dr.Item("SHUKKA_DATE_FROM") = .imdOutkaDateFrom.TextValue
            dr.Item("SHUKKA_DATE_TO") = .imdOutkaDateTo.TextValue

            dr.Item("CHK_MISHORI") = .chkMishori.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_JUCHU_OK") = .chkJuchuOK.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_JUCHU_NG") = .chkJuchuNG.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_SHUKKA_TOUROKU_ZUMI") = .chkShukkaTourokuZumi.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_JISSEKI_SAKUSEI_ZUMI") = .chkJissekiSakuseiZumi.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_TORIKESHI") = .chkTorikeshi.GetBinaryValue.Replace("0", "")

            dr.Item("CHK_MITEI") = .chkMitei.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_INKA") = .chkInka.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_OUTKA") = .chkOutka.GetBinaryValue.Replace("0", "")
            dr.Item("CHK_UNSO") = .chkUnso.GetBinaryValue.Replace("0", "")

            With .sprDetail.ActiveSheet

                dr.Item("STATUS_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.REC_STATUS.ColNo)).Trim
                dr.Item("DEL_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.HORYU_KB.ColNo)).Trim
                dr.Item("SHINCHOKU_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.STATUS.ColNo)).Trim
                dr.Item("DELAY_STATUS") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.DELAY_STATUS.ColNo)).Trim
                dr.Item("CYLINDER_SERIAL_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo)).Trim
                dr.Item("GOODS_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.GOODS_CD.ColNo)).Trim
                dr.Item("GOODS_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.GOODS_NM.ColNo)).Trim
                dr.Item("SHIPMENT_ID") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo)).Trim
                dr.Item("SAP_ORD_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.SAP_ORD_NO.ColNo)).Trim
                dr.Item("CUST_ORD_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.CUST_ORD_NO.ColNo)).Trim
                dr.Item("OUTKA_CTL_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)).Trim
                dr.Item("CUST_CD_L") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.CUST_CD_L.ColNo)).Trim  'ADD 2020/02/27 010901
                dr.Item("CUST_CD_M") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.CUST_CD_M.ColNo)).Trim  'ADD 2020/02/27 010901
                dr.Item("SHUKKA_MOTO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.SHUKKA_MOTO_CD.ColNo)).Trim
                dr.Item("SHUKKA_MOTO") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.SHUKKA_MOTO.ColNo)).Trim
                dr.Item("NONYU_SAKI_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.NONYU_SAKI_CD.ColNo)).Trim
                dr.Item("NONYU_SAKI") = Me._LMFconG.GetCellValue(.Cells(0, LMI960G.sprDetailDef.NONYU_SAKI.ColNo)).Trim

            End With

        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 実績作成/一括変更/受注作成/出荷登録の対象情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">選択行番号リスト</param>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="data">
    ''' (オプション) データ
    ''' シリンダー取込の場合、Excelから取り込んだシリアル番号
    ''' </param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetJissekiSakuseiTarget(ByVal frm As LMI960F,
                                             ByVal ds As DataSet,
                                             ByVal arr As ArrayList,
                                             ByVal eventShubetsu As LMI960C.EventShubetsu,
                                             Optional ByVal data As Object = Nothing
                                             ) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0

        Dim dtIn As DataTable = ds.Tables(LMI960C.TABLE_NM_IN)
        dtIn.Clear()
        Dim drIn As DataRow = dtIn.NewRow

        drIn.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        drIn.Item("BUMON") = frm.ProcessingBumon    '2020/02/07 010901
        If eventShubetsu = LMI960C.EventShubetsu.DELAY_SAKUSEI Then
            '遅延送信の場合
            If frm.cmbDelayShubetsu.SelectedValue.ToString = LMI960C.CmbDelayShubetsuItems.Shukka Then
                '出荷遅延の場合
                drIn.Item("BASHO_KB") = LMI960C.CmbBashoKbItems.Tsumikomi
            Else
                '納入遅延の場合
                drIn.Item("BASHO_KB") = LMI960C.CmbBashoKbItems.Nioroshi
            End If
        Else
            drIn.Item("BASHO_KB") = frm.cmbBashoKb.TextValue
        End If
        drIn.Item("ARRIVAL_TIME") = frm.dtpArrivalTime.TextValue
        drIn.Item("DEPARTURE_TIME") = frm.dtpDepartureTime.TextValue
        'ADD START 2019/03/27
        If eventShubetsu = LMI960C.EventShubetsu.IKKATSU_CHANGE Then
            '一括変更【出荷日・納入日】
            drIn.Item("IKKATSU_CHANGE_ITEM") = frm.cmbChangeItem.SelectedValue.ToString()
            drIn.Item("CHANGE_VALUE") = frm.imdChangeDate.TextValue
        End If
        'ADD END   2019/03/27
        If eventShubetsu = LMI960C.EventShubetsu.MOD_JOB_NO Then
            'JOB NO変更
            drIn.Item("CHANGE_VALUE") = frm.txtJobNo.TextValue
        End If
        'ADD S 2019/12/12 009741
        If eventShubetsu = LMI960C.EventShubetsu.JUCHU_SAKUSEI Then
            '受注作成の場合
            If frm.optJuchuYes.Checked Then
                '受注可否Yes
                drIn.Item("SHINCHOKU_KB_JUCHU") = CStr(CInt(LMI960C.ShinchokuKbJuchu.JuchuOK))
                drIn.Item("SHIPMENT_DECLINE_REASON") = ""  'ADD 2020/03/06 011377
            Else
                '受注可否No
                drIn.Item("SHINCHOKU_KB_JUCHU") = CStr(CInt(LMI960C.ShinchokuKbJuchu.JuchuNG))
                drIn.Item("SHIPMENT_DECLINE_REASON") = frm.cmbDeclineReason.SelectedValue.ToString()  'ADD 2020/03/06 011377
            End If
        End If
        'ADD E 2019/12/12 009741
        'ADD S 2020/02/27 010901
        If eventShubetsu = LMI960C.EventShubetsu.SHUKKA_TOUROKU OrElse
           eventShubetsu = LMI960C.EventShubetsu.NYUKA_TOUROKU OrElse
           eventShubetsu = LMI960C.EventShubetsu.UNSO_TOUROKU Then
            '出荷登録/入荷登録の場合
            drIn.Item("SHINCHOKU_KB_JUCHU") = CStr(CInt(LMI960C.ShinchokuKbJuchu.NyuShukkaTourokuZumi))
        End If
        'ADD E 2020/02/27 010901
        'ADD S 2020/02/07 010901
        If eventShubetsu = LMI960C.EventShubetsu.JISSEKI_SAKUSEI AndAlso frm.cmbBashoKb.TextValue = LMI960C.CmbBashoKbItems.Nioroshi Then
            '実績作成(荷下場)の場合
            drIn.Item("SHINCHOKU_KB_JUCHU") = CStr(CInt(LMI960C.ShinchokuKbJuchu.JissekiSakuseiZumi))
        End If
        'ADD E 2020/02/07 010901
        If eventShubetsu = LMI960C.EventShubetsu.DELAY_SAKUSEI Then
            '遅延送信の場合
            drIn.Item("SHIPMENT_DELAY_SHUBETSU") = frm.cmbDelayShubetsu.SelectedValue.ToString()
            drIn.Item("SHIPMENT_DELAY_REASON") = frm.cmbDelayReason.SelectedValue.ToString()
            drIn.Item("SHIPMENT_DELAY_HOSOKU") = frm.cmbDelayHosoku.SelectedText
        End If
        If eventShubetsu = LMI960C.EventShubetsu.EDI_TORIKESHI Then
            '未処理⇒EDI取消の場合
            drIn.Item("SHINCHOKU_KB_JUCHU") = CStr(CInt(LMI960C.ShinchokuKbJuchu.EdiTorikeshi))
        End If
        If eventShubetsu = LMI960C.EventShubetsu.ROLLBACK_EDI_TORIKESHI Then
            'EDI取消⇒未処理の場合
            drIn.Item("SHINCHOKU_KB_JUCHU") = CStr(CInt(LMI960C.ShinchokuKbJuchu.Mishori))
        End If

        dtIn.Rows.Add(drIn)


        Dim dtTarget As DataTable = ds.Tables(LMI960C.TABLE_NM_SAKUSEI_TARGET)
        dtTarget.Clear()
        Dim drTarget As DataRow = Nothing

        With spr.ActiveSheet

            For i As Integer = 0 To max

                'インスタンス生成
                drTarget = dtTarget.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                drTarget.Item("ROW_NO") = rowNo    'ADD 2020/02/27 010901

                drTarget.Item("SHIPMENT_ID") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.LOAD_NUMBER.ColNo))
                drTarget.Item("INOUT_KB") = InOutKbNameToCd(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.INOUT_KB.ColNo)))
                drTarget.Item("CUST_CD_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.CUST_CD_L.ColNo))
                drTarget.Item("CUST_CD_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.CUST_CD_M.ColNo))
                drTarget.Item("OUTKA_CTL_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo))  'ADD 2020/03/06 011377
                drTarget.Item("OUTKA_CTL_NO_DELETED") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO_DELETED.ColNo))
                drTarget.Item("SHUKKA_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo)))
                drTarget.Item("HED_CRT_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_CRT_DATE.ColNo)))    'MOD 2019/12/12 009741
                drTarget.Item("HED_FILE_NAME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_FILE_NAME.ColNo))
                drTarget.Item("HED_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_UPD_DATE.ColNo))
                drTarget.Item("HED_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.HED_UPD_TIME.ColNo))

                'ADD START 2019/03/27
                If eventShubetsu = LMI960C.EventShubetsu.JISSEKI_SAKUSEI AndAlso frm.cmbBashoKb.TextValue = LMI960C.CmbBashoKbItems.Tsumikomi OrElse
                   eventShubetsu = LMI960C.EventShubetsu.IKKATSU_CHANGE AndAlso frm.cmbChangeItem.SelectedValue.ToString() = LMI960C.CmbIkkatsuChangeItems.ShukkaDate OrElse
                   eventShubetsu = LMI960C.EventShubetsu.DELAY_SAKUSEI AndAlso frm.cmbDelayShubetsu.SelectedValue.ToString = LMI960C.CmbDelayShubetsuItems.Shukka Then
                    '実績作成(積込場)/一括変更(出荷日)/遅延送信(出荷遅延)の場合

                    '出荷元
                    drTarget.Item("STP_GYO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.STP1_GYO.ColNo))
                    drTarget.Item("STP_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.STP1_UPD_DATE.ColNo))
                    drTarget.Item("STP_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.STP1_UPD_TIME.ColNo))

                ElseIf eventShubetsu = LMI960C.EventShubetsu.JISSEKI_SAKUSEI AndAlso frm.cmbBashoKb.TextValue = LMI960C.CmbBashoKbItems.Nioroshi OrElse
                       eventShubetsu = LMI960C.EventShubetsu.JISSEKI_SAKUSEI AndAlso frm.cmbBashoKb.TextValue = LMI960C.CmbBashoKbItems.NonyuYotei OrElse
                       eventShubetsu = LMI960C.EventShubetsu.IKKATSU_CHANGE AndAlso frm.cmbChangeItem.SelectedValue.ToString() = LMI960C.CmbIkkatsuChangeItems.NonyuDate OrElse
                       eventShubetsu = LMI960C.EventShubetsu.DELAY_SAKUSEI AndAlso frm.cmbDelayShubetsu.SelectedValue.ToString = LMI960C.CmbDelayShubetsuItems.Nonyu Then
                    '実績作成(納入予定/荷下場)/一括変更(納入日)/遅延送信(納入遅延)の場合

                    '納入先
                    drTarget.Item("STP_GYO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.STP2_GYO.ColNo))
                    drTarget.Item("STP_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.STP2_UPD_DATE.ColNo))
                    drTarget.Item("STP_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.STP2_UPD_TIME.ColNo))

                End If
                'ADD END   2019/03/27

                If eventShubetsu = LMI960C.EventShubetsu.IMPORT_CYLINDER Then
                    'シリンダー取込の場合
                    drTarget.Item("CYLINDER_SERIAL_NO") = DirectCast(data, String)
                Else
                    drTarget.Item("CYLINDER_SERIAL_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo))
                End If

                If eventShubetsu = LMI960C.EventShubetsu.SHUKKA_TOUROKU OrElse
                   eventShubetsu = LMI960C.EventShubetsu.NYUKA_TOUROKU OrElse
                   eventShubetsu = LMI960C.EventShubetsu.UNSO_TOUROKU Then
                    '入出荷登録の場合
                    drTarget.Item("SHORIZUMI_FLG") = LMI962C.ShorizumiFlg.Mishori
                End If

                '行追加
                dtTarget.Rows.Add(drTarget)

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入出荷区分名→コード変換
    ''' </summary>
    ''' <param name="inoutKbName">入出荷区分名</param>
    ''' <returns>入出荷区分コード</returns>
    Private Function InOutKbNameToCd(ByVal inoutKbName As String) As String
        Select Case inoutKbName
            Case LMI960C.InOutKbName.Mitei
                Return LMI960C.InOutKb.Mitei
            Case LMI960C.InOutKbName.Inka
                Return LMI960C.InOutKb.Inka
            Case LMI960C.InOutKbName.Outka
                Return LMI960C.InOutKb.Outka
            Case LMI960C.InOutKbName.Unso
                Return LMI960C.InOutKb.Unso
            Case Else
                Return ""
        End Select
    End Function

#End Region 'DataSet設定

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMI960F)

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
    Private Sub EndAction(ByVal frm As LMI960F)

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
    Private Function CallServerProcess(ByVal frm As LMI960F,
                                       ByVal ds As DataSet,
                                       ByVal eventShubetsu As LMI960C.EventShubetsu,
                                       Optional ByRef outDs As DataSet = Nothing) As Boolean    'MOD 2020/02/07 010901

        Dim msg As String = String.Empty
        Dim actionId As String = String.Empty
        Dim blfName As String = "LMI960BLF"    'MOD 2019/03/27

        Select Case eventShubetsu    'MOD 2019/03/27

            Case LMI960C.EventShubetsu.JISSEKI_SAKUSEI    'MOD 2019/03/27
                '実績作成
                actionId = LMI960C.ACTION_ID_INSERT_SENDOUTEDI
                msg = frm.FunctionKey.F1ButtonName

                'ADD S 2019/12/12 009741
            Case LMI960C.EventShubetsu.JUCHU_SAKUSEI
                '受注作成
                actionId = LMI960C.ACTION_ID_INSERT_SENDOUTEDI_JUCHU
                msg = frm.FunctionKey.F2ButtonName
                'ADD E 2019/12/12 009741

                'MOD START 2019/03/27
            Case LMI960C.EventShubetsu.IKKATSU_CHANGE
                '一括変更
                actionId = LMI960C.ACTION_ID_IKKATSU_CHANGE
                msg = frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）"
                'MOD END   2019/03/27

                'ADD S 2020/02/07 010901
            ''Case LMI960C.EventShubetsu.SHUKKA_TOUROKU
            ''    '出荷登録
            ''    actionId = LMI960C.ACTION_ID_SHUKKA_TOUROKU
            ''    msg = frm.FunctionKey.F6ButtonName

            Case LMI960C.EventShubetsu.DELETE_GLIS_JUCHU
                '受注削除
                actionId = LMI960C.ACTION_ID_DELETE_GLIS_JUCHU
                msg = frm.FunctionKey.F7ButtonName
                'ADD E 2020/02/07 010901

            Case LMI960C.EventShubetsu.DELAY_SAKUSEI
                '遅延送信
                actionId = LMI960C.ACTION_ID_INSERT_SENDOUTEDI_DELAY
                msg = frm.FunctionKey.F3ButtonName

            Case LMI960C.EventShubetsu.ROLLBACK_JUCHU_STATUS
                '受注ステータス戻し処理
                actionId = LMI960C.ACTION_ID_ROLLBACK_JUCHU_STATUS
                msg = frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）"

            Case LMI960C.EventShubetsu.MOD_JOB_NO
                'JOB NO変更
                actionId = LMI960C.ACTION_ID_MOD_JOB_NO
                msg = frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）"

            Case LMI960C.EventShubetsu.EDI_TORIKESHI, LMI960C.EventShubetsu.ROLLBACK_EDI_TORIKESHI
                '未処理⇔EDI取消
                actionId = LMI960C.ACTION_ID_EDI_TORIKESHI
                msg = frm.btnIkkatsuChange.Text & "（" & frm.cmbChangeItem.SelectedText & "）"

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

        If outDs IsNot Nothing Then
            outDs = rtnDs
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
    Private Function ActionData(ByVal frm As LMI960F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , ByVal blfName As String _
                                , Optional ByRef rtnDs As DataSet = Nothing
                                ) As Boolean

        'サーバアクセス
        rtnDs = MyBase.CallWSA(blfName, actionId, ds)

        'エラーがある場合、メッセージ設定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Return False
        End If

        'DEL S 2020/02/27 010901
        ''エラーが保持されている場合、False
        ''If MyBase.IsMessageStoreExist = True Then
        ''    MyBase.ShowMessage(frm, "E235")
        ''    MyBase.MessageStoreDownload()
        ''    Return False
        ''End If
        'DEL E 2020/02/27 010901

        Return True

    End Function

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rtnDs">DataSet</param>
    ''' <param name="blf">BLF</param>
    ''' <param name="count">件数</param>
    ''' <returns>DataSet　ワーニングで「いいえ」を選択した場合、Nothing</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal frm As LMI960F _
                                    , ByVal ds As DataSet _
                                    , ByVal rtnDs As DataSet _
                                    , ByVal blf As String _
                                    , ByVal count As String
                                    ) As DataSet

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, LMI960C.ACTION_ID_SELECT, ds)

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {count})

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '処理終了アクション
                    Call Me.EndAction(frm)

                    Return Nothing

                End If

            Else

                'メッセージエリアの設定(0件エラー)
                MyBase.ShowMessage(frm)

            End If

        Else

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {count})

        End If

        Return rtnDs

    End Function

#End Region 'ユーティリティ

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMI960F)

        'メッセージエリアに値があるか判定
        If String.IsNullOrEmpty(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text) = True Then

            'メッセージ設定
            Call Me.SetInitMessage(frm)

        End If

    End Sub

    ''' <summary>
    ''' 初期メッセージ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetInitMessage(ByVal frm As LMI960F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region 'メッセージ設定

#Region "チェック"

#Region "各処理のチェック"

    ''' <summary>
    ''' 対象データチェック（実績作成）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTargetValid(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            '実行前のあるべき配送ステータス
            Dim validStatus As String = String.Empty
            Select Case frm.cmbBashoKb.TextValue
                Case LMI960C.CmbBashoKbItems.Tsumikomi
                    '場所区分が「積込場」の場合、「未送信」
                    validStatus = LMI960C.StatusName.Misoushin
                Case LMI960C.CmbBashoKbItems.NonyuYotei
                    '場所区分が「納入予定」の場合、「ピック済」
                    validStatus = LMI960C.StatusName.PickZumi
                Case LMI960C.CmbBashoKbItems.Nioroshi
                    '場所区分が「荷下場」の場合、「納入予定」
                    validStatus = LMI960C.StatusName.NonyuYotei
            End Select

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                'ADD S 2019/12/12 009741
                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、実績作成", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If
                'ADD E 2019/12/12 009741

                'ADD S 2020/02/07 010901
                'DEL S 2020/03/06 011377
                ''If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) = LMI960C.JuchuStatusName.JuchuNG Then
                ''    '「受注NG」の場合
                ''    MyBase.ShowMessage(frm, "E428", New String() {"受注NGのデータが選択されている", "、実績作成", ""})
                ''    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                ''    Return False
                ''End If

                ''If frm.ProcessingBumon = LMI960C.CmbBumonItems.Soko _
                ''AndAlso Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.ShukkaTourokuZumi Then
                ''    '受注ステータスが「出荷登録済」でない場合
                ''    MyBase.ShowMessage(frm, "E428", New String() {frm.WordsShukkaTouroku & "済でないデータが選択されている", "、実績作成", ""})
                ''    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                ''    Return False
                ''End If

                ''If frm.ProcessingBumon = LMI960C.CmbBumonItems.ISO _
                ''AndAlso Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.JuchuTourokuZumi Then
                ''    '受注ステータスが「受注登録済」でない場合
                ''    MyBase.ShowMessage(frm, "E428", New String() {frm.WordsShukkaTouroku & "済でないデータが選択されている", "、実績作成", ""})
                ''    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                ''    Return False
                ''End If
                'DEL S 2020/03/06 011377

                'ADD S 2020/03/06 011377
                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.JuchuOK Then
                    '「受注OK」でない場合
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.JuchuOK & "でないデータが選択されている", "、実績作成", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                    Return False
                End If
                'ADD E 2020/03/06 011377

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)) = LMI960C.DELETED Then
                    '入出荷管理番号/JOB NOが「削除済」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {frm.WordsNyuShukka & "データが削除済の", "、実績作成", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                    Return False
                End If
                'ADD E 2020/02/07 010901

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)) <> validStatus Then
                    '実行前のあるべき配送ステータスでない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & validStatus & "でない行が選択されている", "、実績作成", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)    'ADD 2019/12/12 
                    Return False
                End If

#If True Then   'ADD 2019/06/13　依頼番号 : 005566   【LMS】ハネウェルEDI_日付変更改修
                '出荷日＞納入日の場合、エラー
                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo)) > GetDatePart(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.NONYU_DATE.ColNo))) Then
                    MyBase.ShowMessage(frm, "E428", New String() {"出荷日 > 納入日 の", "、実績作成", ""})
                    With .sprDetail.ActiveSheet
                        .SetActiveCell(rowNo, LMI960G.sprDetailDef.NONYU_DATE.ColNo)
                    End With

                    Return False
                End If
#End If
                If .cmbBashoKb.TextValue = LMI960C.CmbBashoKbItems.Tsumikomi Then
                    '出荷日＞システム日付の場合エラー
                    If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo)).CompareTo(Now.ToString("yyyy/MM/dd")) > 0 Then
                        MyBase.ShowMessage(frm, "E428", New String() {"出荷日が未来日の", "、実績作成", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.SHUKKA_DATE.ColNo)
                        Return False
                    End If
                End If

                If .cmbBashoKb.TextValue = LMI960C.CmbBashoKbItems.Nioroshi Then
                    '納入日＞システム日付の場合エラー
                    If GetDatePart(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.NONYU_DATE.ColNo))).CompareTo(Now.ToString("yyyy/MM/dd")) > 0 Then
                        MyBase.ShowMessage(frm, "E428", New String() {"納入日が未来日の", "、実績作成", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.NONYU_DATE.ColNo)
                        Return False
                    End If
                End If

            Next

            Return True

        End With

    End Function

    'ADD S 2019/12/12 009741
    ''' <summary>
    ''' 対象データチェック（受注作成）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidJuchuTarget(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                Dim juchuStatus As String = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo))

                'ADD S 2020/03/06 011377
                If .optJuchuNo.Checked Then
                    '受注可否NOの場合

                    If juchuStatus = LMI960C.JuchuStatusName.NyuShukkaTourokuZumi OrElse juchuStatus = LMI960C.JuchuStatusName.JuchuTourokuZumi Then
                        If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)) = LMI960C.DELETED Then
                            '入出荷/受注登録済であり入出荷管理番号/JOB NOが削除済である場合、処理可能
                            Continue For
                        Else
                            '入出荷/受注登録済であり入出荷管理番号/JOB NOが削除済でない、エラーとする
                            MyBase.ShowMessage(frm, "E428", New String() {frm.WordsNyuShukka & "データが削除済でないデータが選択されている", "、受注可否「No」に", ""})
                            .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                            Return False
                        End If
                    End If
                    'ADD S 2020/03/06 011377

                    If juchuStatus <> LMI960C.JuchuStatusName.Mishori Then  'MOD 2020/03/06 011377
                        '受注前でない場合、エラーとする
                        MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.Mishori & "でない行が選択されている", "、処理", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                        Return False
                    End If

                    'ADD S 2020/03/06 011377
                End If

                If .optJuchuYes.Checked Then
                    '受注可否YESの場合

                    If juchuStatus <> LMI960C.JuchuStatusName.NyuShukkaTourokuZumi _
                    AndAlso juchuStatus <> LMI960C.JuchuStatusName.JuchuTourokuZumi Then
                        '受注ステータスが入出荷/受注登録済でない場合
                        MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & frm.WordsNyuShukkaTourokuZumi & "でないデータが選択されている", "、処理", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                        Return False
                    End If

                    If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)) = LMI960C.DELETED Then
                        '入出荷管理番号/JOB NOが「削除済」の場合
                        MyBase.ShowMessage(frm, "E428", New String() {frm.WordsNyuShukka & "データが削除済の", "、処理", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                        Return False
                    End If

                End If
                'ADD E 2020/03/06 011377
            Next

            Return True

        End With

    End Function
    'ADD E 2019/12/12 009741

    ''' <summary>
    ''' 対象データチェック（遅延送信）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidDelayTarget(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.JuchuOK Then
                    '「受注OK」でない場合
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.JuchuOK & "でないデータが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)) = LMI960C.DELETED Then
                    '入出荷管理番号/JOB NOが「削除済」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {frm.WordsNyuShukka & "データが削除済の", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                    Return False
                End If

                If .cmbDelayShubetsu.SelectedValue.ToString = LMI960C.CmbDelayShubetsuItems.Shukka Then
                    '出荷遅延の場合
                    If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)) <> LMI960C.StatusName.Misoushin Then
                        '配送ステータスが「未送信」でない場合、エラーとする
                        MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & LMI960C.StatusName.Misoushin & "でない行が選択されている", "、処理", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)
                        Return False
                    End If
                Else
                    '納入遅延の場合
                    If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)) <> LMI960C.StatusName.NonyuYotei Then
                        '配送ステータスが「納入予定」でない場合、エラーとする
                        MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & LMI960C.StatusName.NonyuYotei & "でない行が選択されている", "、処理", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)
                        Return False
                    End If
                End If

                If Not String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.DELAY_STATUS.ColNo))) Then
                    '遅延ステータスが空でない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"遅延送信済みの", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.DELAY_STATUS.ColNo)
                    Return False
                End If

            Next

            Return True

        End With

    End Function

    'ADD S 2020/02/27 010901
    ''' <summary>
    ''' 対象データチェック（荷主振り分け）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidUpdateCustTarget(ByVal frm As LMI960F, ByVal arr As ArrayList, ByVal eventShubetsu As LMI960C.EventShubetsu) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.Mishori Then
                    '受注ステータスが未処理でない場合
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.Mishori & "でないデータが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                    Return False
                End If

                If eventShubetsu = LMI960C.EventShubetsu.UPDATE_CUST_AUTO Then
                    '自動振分の場合

                    If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.CUST_CD_L.ColNo)) <> "" _
                    OrElse Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.CUST_CD_M.ColNo)) <> "" Then
                        '荷主振分済の場合
                        MyBase.ShowMessage(frm, "E428", New String() {"荷主振分済であるデータが選択されている", "、荷主自動振分", ""})
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.CUST_CD_L.ColNo)
                        Return False
                    End If

                End If

            Next

            Return True

        End With

    End Function
    'ADD S 2020/02/27 010901

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' 対象データチェック（出荷登録/受注登録/入荷登録/運送登録）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidNyuShukkaTourokuTarget(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                Dim juchuStatus As String = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo))
                If juchuStatus <> LMI960C.JuchuStatusName.Mishori Then 'MOD 2020/03/06 011377
                    '未処理でない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.Mishori & "でない行が選択されている", "、処理", ""}) 'MOD 2020/03/06 011377
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                    Return False
                End If

                'ADD S 2020/02/27 010901
                If (frm.ProcessingBumon = LMI960C.CmbBumonItems.Soko OrElse frm.ProcessingBumon = LMI960C.CmbBumonItems.ChilledLorry) _
                AndAlso (Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.CUST_CD_L.ColNo)) = "" _
                OrElse Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.CUST_CD_M.ColNo)) = "") Then
                    '荷主振分していない場合
                    MyBase.ShowMessage(frm, "E428", New String() {"荷主振分済でないデータが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.CUST_CD_L.ColNo)
                    Return False
                End If
                'ADD E 2020/02/27 010901

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 対象データチェック（GLIS受注削除）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidGLISJuchuSakujoTarget(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", frm.WordsShukkaTouroku & "を行ったデータを選択して下さい。"})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                Dim jobNo As String = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo))
                If jobNo = LMI960C.DELETED Then
                    'JOB NOが削除済の場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"すでに削除済の", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                    Return False
                End If

                Dim juchuStatus As String = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo))
                If juchuStatus <> LMI960C.JuchuStatusName.JuchuTourokuZumi AndAlso
                   juchuStatus <> LMI960C.JuchuStatusName.JuchuOK Then
                    '受注登録済でも受注OKでもない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.JuchuTourokuZumi & "または" & LMI960C.JuchuStatusName.JuchuOK & "でない行が選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                    Return False
                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 対象データチェック（シリンダー取込）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidTargetOfImportCylinder(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)) = LMI960C.DELETED Then
                    '入出荷管理番号/JOB NOが「削除済」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {frm.WordsNyuShukka & "データが削除済の", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)) = LMI960C.StatusName.NioroshiZumi Then
                    '配送ステータスが「荷下ろし済」の場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & LMI960C.StatusName.NioroshiZumi & "の", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)
                    Return False
                End If

                '=== 確認メッセージ ===
                If Not String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo))) Then
                    'シリンダーシリアルNoが空でない場合、上書き確認
                    If MyBase.ShowMessage(frm, "C001", New String() {"すでに取込済みです。再取込（上書き）"}) <> MsgBoxResult.Ok Then
                        .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.CYLINDER_SERIAL_NO.ColNo)
                        Return False
                    End If
                End If

            Next

            Return True

        End With

    End Function

    'ADD S 2020/02/07 010901

    'ADD START 2019/03/27
    ''' <summary>
    ''' 対象データチェック（一括変更【出荷日・納入日】）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidTargetOfIkattsuChange(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            '実行前のあるべき配送ステータス
            Dim validStatus As String = LMI960C.StatusName.Misoushin

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

#If False Then  'ADD 2019/06/13依頼番号 : 005566   【LMS】ハネウェルEDI_日付変更改修
                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)) <> validStatus Then
                    '実行前のあるべき配送ステータスでない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & validStatus & "でない行が選択されている", "、一括変更", ""})
                    Return False
                End If
#End If

#If True Then   'ADD 2019/06/13依頼番号 : 005566   【LMS】ハネウェルEDI_日付変更改修
                'ピック済み:          出荷日変更不可
                '荷降ろし済	:出荷日/納入日とも変更不可
                Dim cmbKbn As String = .cmbChangeItem.SelectedValue.ToString    '一括変更項目取得
                Dim status As String = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo))

                If ("01").Equals(cmbKbn) Then
                    '出荷日のとき
                    If status.Equals(LMI960C.StatusName.NioroshiZumi) = True Or
                        status.Equals(LMI960C.StatusName.PickZumi) = True Then

                        MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & status & "の", "、出荷日を一括変更", ""})

                        With .sprDetail.ActiveSheet
                            .SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)
                        End With

                        Return False
                    End If

                ElseIf ("02").Equals(cmbKbn) Then
                    '納入日のとき
                    If status.Equals(LMI960C.StatusName.NioroshiZumi) = True Then

                        MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & status & "の", "、納入日を一括変更", ""})
                        With .sprDetail.ActiveSheet
                            .SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)
                        End With

                        Return False

                    End If
                End If
#End If

            Next

            Return True

        End With

    End Function
    'ADD END   2019/03/27

    ''' <summary>
    ''' 対象データチェック（受注ステータス戻し処理）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidTargetOfRollbackJuchuStatus(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)) = LMI960C.RecStatusName.Cancelled Then
                    'TMC取消が「取消」の場合
                    MyBase.ShowMessage(frm, "E428", New String() {"取消データが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.REC_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.NyuShukkaTourokuZumi AndAlso
                   Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> LMI960C.JuchuStatusName.JuchuOK Then
                    '受注ステータスが入出荷登録済でも受注OKでもない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.NyuShukkaTourokuZumi & "または" & LMI960C.JuchuStatusName.JuchuOK & "でない行が選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)) <> LMI960C.StatusName.Misoushin Then
                    '配送ステータスが「未送信」でない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"配送ステータスが" & LMI960C.StatusName.Misoushin & "でない行が選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.STATUS.ColNo)
                    Return False
                End If

                If Not String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.DELAY_STATUS.ColNo))) Then
                    '遅延ステータスが空でない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"遅延送信済みのデータが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.DELAY_STATUS.ColNo)
                    Return False
                End If

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)) <> LMI960C.DELETED Then
                    '出荷管理番号が削除済でない場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {frm.WordsNyuShukka & "データが削除済でないデータが選択されている", "、処理", ""})
                    .sprDetail.ActiveSheet.SetActiveCell(rowNo, LMI960G.sprDetailDef.INOUTKA_CTL_NO.ColNo)
                    Return False
                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 対象データチェック（JOB NO変更）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidTargetOfModJobNo(ByVal frm As LMI960F, ByVal arr As ArrayList) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) = LMI960C.JuchuStatusName.Mishori Then
                    '受注ステータスが未処理の場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & LMI960C.JuchuStatusName.Mishori & "の", "、処理", ""})
                    Return False
                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 対象データチェック（未処理⇔EDI取消処理）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidTargetOfEdiTorikeshi(ByVal frm As LMI960F, ByVal arr As ArrayList, ByVal eventShubetsu As LMI960C.EventShubetsu) As Boolean

        With frm

            Dim max As Integer = arr.Count - 1
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0

            Dim validStatus As String = ""
            Select Case eventShubetsu
                Case LMI960C.EventShubetsu.EDI_TORIKESHI
                    validStatus = LMI960C.JuchuStatusName.Mishori
                Case LMI960C.EventShubetsu.ROLLBACK_EDI_TORIKESHI
                    validStatus = LMI960C.JuchuStatusName.EdiTorikeshi
            End Select

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                If Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMI960G.sprDetailDef.JUCHU_STATUS.ColNo)) <> validStatus Then
                    '受注ステータスが不適切な場合、エラーとする
                    MyBase.ShowMessage(frm, "E428", New String() {"受注ステータスが" & validStatus & "でない行が選択されている", "、処理", ""})
                    Return False
                End If

            Next

            Return True

        End With

    End Function

#End Region '各処理のチェック

#End Region 'チェック

#Region "シリンダーExcel取込"

    ''' <summary>
    ''' 対話型Excelファイルパス取得
    ''' </summary>
    ''' <param name="outFilePath">ユーザが選択したファイルのパス</param>
    ''' <returns>True:取得成功 / False:取得失敗(ユーザがキャンセル)</returns>
    Private Function GetFilePathInteractive(ByRef outFilePath As String) As Boolean

        Using ofd As New OpenFileDialog
            ofd.Title = "取込ファイルを選択してください"
            ofd.Filter = "Excelファイル (*.xlsx;*.xls)|*.xlsx;*.xls"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() = DialogResult.OK Then
                outFilePath = ofd.FileName
                Return True
            End If
        End Using

        Return False

    End Function

    ''' <summary>
    ''' シリンダーExcel読込
    ''' </summary>
    ''' <param name="filePath">読込ファイルパス</param>
    ''' <param name="outExcelData">(出力) Excelデータ</param>
    ''' <returns>Ture:読込成功 / False:ファイルオープン失敗</returns>
    Private Function RoladCylinderExcel(ByVal filePath As String, ByRef outExcelData(,) As Object) As Boolean

        'ファイル存在チェック
        If Not System.IO.File.Exists(filePath) Then
            MyBase.SetMessage("E395", {filePath})
            Return False
        End If


        Const HeaderRow As Integer = 1
        Const BodyStartRow As Integer = HeaderRow + 1

        Dim xlApp As Excel.Application = Nothing
        Dim xlBooks As Excel.Workbooks = Nothing
        Dim xlBook As Excel.Workbook = Nothing
        Dim xlSheets As Excel.Sheets = Nothing
        Dim xlSheet As Excel.Worksheet = Nothing

        Dim startCell As Excel.Range = Nothing
        Dim endCell As Excel.Range = Nothing
        Dim range As Excel.Range = Nothing

        Try
            'EXCEL開始
            xlApp = New Excel.Application
            xlApp.Visible = False
            xlApp.DisplayAlerts = False
            xlBooks = xlApp.Workbooks

            Try
                'リンクを更新しない、読み取り専用
                xlBook = xlBooks.Open(filePath, 0, True)
            Catch
                MyBase.SetMessage("E547", {"ファイルを開けません。"})
                Return False
            End Try

            '作業シート設定
            xlSheets = xlBook.Worksheets
            xlSheet = DirectCast(xlSheets(1), Excel.Worksheet)
            xlSheet.Activate()

            '最終セルを選択
            xlSheet.Cells.SpecialCells(Excel.XlCellType.xlCellTypeLastCell).Select()

            '最終セルの行番号・列番号を取得
            Dim lastCellRow As Integer = xlApp.ActiveCell.Row
            Dim lastCellCol As Integer = xlApp.ActiveCell.Column

            '明細の範囲を取得
            startCell = DirectCast(xlSheet.Cells(BodyStartRow, 1), Excel.Range)
            endCell = DirectCast(xlSheet.Cells(lastCellRow, lastCellCol), Excel.Range)
            range = xlSheet.Range(startCell, endCell)

            '範囲のセル値を取得する
            Dim arrData(,) As Object = DirectCast(range.Value, Object(,))
            outExcelData = arrData

        Finally
            '参照の開放

            If xlSheet IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheet)
                xlSheet = Nothing
            End If

            If xlSheets IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlSheets)
                xlSheets = Nothing
            End If

            If xlBook IsNot Nothing Then
                xlBook.Close(False)
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBook)
                xlBook = Nothing
            End If

            If xlBooks IsNot Nothing Then
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlBooks)
                xlBooks = Nothing
            End If

            If xlApp IsNot Nothing Then
                xlApp.Quit()
                System.Runtime.InteropServices.Marshal.FinalReleaseComObject(xlApp)
                xlApp = Nothing
            End If

        End Try

        Return True

    End Function

#End Region 'シリンダーExcel取込

    ''' <summary>
    ''' "yyyy/MM/dd HH:mm:ss" 形式を前提とした文字列の "yyyy/MM/dd" 部を取り出す。
    ''' </summary>
    ''' <param name="dateTimeText">文字列(yyyy/MM/dd HH:mm:ss)</param>
    ''' <returns>取り出した文字列(yyyy/MM/dd)
    ''' 渡された文字列の長さが "yyyy/MM/dd" に満たない場合はそのままの値を返す。</returns>
    Private Function GetDatePart(ByVal dateTimeText As String) As String

        If dateTimeText.Length < "yyyy/MM/dd".Length Then
            Return dateTimeText
        End If

        Return dateTimeText.Substring(0, "yyyy/MM/dd".Length)

    End Function

#End Region '内部メソッド

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.JissekiSakusei(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub


    'ADD S 2019/12/12 009741
    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.JuchuSakusei(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'ADD E 2019/12/12 009741

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.DelayReasonSakusei(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    'ADD S 2020/02/27 010901
    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.UpdateCustAuto(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.UpdateCustManual(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'ADD E 2020/02/27 010901

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        If frm.ProcessingBumon = LMI960C.CmbBumonItems.Soko OrElse frm.ProcessingBumon = LMI960C.CmbBumonItems.ChilledLorry Then
            '倉庫の場合
            'または Chilled Lorry の場合
            Call Me.ShukkaTouroku(frm)
        ElseIf frm.ProcessingBumon = LMI960C.CmbBumonItems.ISO Then
            'ISOの場合
            Call Me.GlisJuchuTouroku(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.GlisJuchuSakujo(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OutputStopNote(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SelectListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ImportCylinder(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.NyukaTouroku(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub LMI960F_KeyDown(ByVal frm As LMI960F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    Friend Sub btnUnsoTouroku_Click(ByVal frm As LMI960F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.UnsoTouroku(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    'ADD START 2019/03/27
    ''' <summary>
    ''' 一括変更ボタン押下時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub btnIkkatsuChange_Click(ByVal frm As LMI960F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Select Case frm.cmbChangeItem.SelectedValue.ToString
            Case LMI960C.CmbIkkatsuChangeItems.ShukkaDate, LMI960C.CmbIkkatsuChangeItems.NonyuDate '出荷日、納入日
                Call Me.IkkatsuChange(frm)

            Case LMI960C.CmbIkkatsuChangeItems.RollbackJuchuStatus '入出荷登録済、受注送信済⇒未処理
                Call Me.RollbackJuchuStatus(frm)

            Case LMI960C.CmbIkkatsuChangeItems.JobNo 'JOB NO
                Call Me.ModJobNo(frm)

            Case LMI960C.CmbIkkatsuChangeItems.EdiTorikeshi '未処理⇒EDI取消
                Call Me.EdiTorikeshi(frm, LMI960C.EventShubetsu.EDI_TORIKESHI)

            Case LMI960C.CmbIkkatsuChangeItems.RollbackEdiTorikeshi 'EDI取消⇒未処理
                Call Me.EdiTorikeshi(frm, LMI960C.EventShubetsu.ROLLBACK_EDI_TORIKESHI)

        End Select

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'ADD END   2019/03/27

    ''' <summary>
    ''' 部門(倉庫/ISO)選択値変更時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbBumon_SelectedIndexChanged(ByVal frm As LMI960F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me._G.ChangeControlsPropertyWhenBumonChanged()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 一括変更項目選択値変更時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbChangeItem_SelectedIndexChanged(ByVal frm As LMI960F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me._G.ChangeControlsPropertyWhenChangeItemChanged()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 場所区分選択値変更時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub cmbBashoKb_SelectedIndexChanged(ByVal frm As LMI960F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me._G.ChangeControlsPropertyWhenBashoKbChanged()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
