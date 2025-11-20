' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃
'  プログラムID     :  LMF010H : 運行・運送情報
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMF010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF010G

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

    '2014.07.01 追加START
    ''' <summary>
    ''' 実行時の更新条件格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _JikkouDs As DataSet
    '2014.07.01 追加END

    'ADD 2017/02/27 Start

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    'ADD 2017/02/27 End
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
        Dim frm As LMF010F = New LMF010F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMF010V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMF010G(Me, frm, Me._LMFconG)

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

        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        dt.Rows.Add(dr)

        'コントロール個別設定
        Call Me._G.SetControl(Me._LMFconH.ServerAccess(ds, LMF010C.ACTION_ID_SELECT_CMB))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        '初期値設定(スプレッド)
        Me._G.SetInitValue()

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

    '(2013.01.17)要望番号1617 -- START --
    ''' <summary>
    ''' 出荷編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EditOutkaData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック(※要修正)
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.OUTKA_EDIT)

        'チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        'i'rtnResult = rtnResult AndAlso Me._V.IsEditOutkaChk(arr, LMF010C.ActionType.OUTKA_EDIT)
        rtnResult = rtnResult AndAlso Me._V.IsEditOutkaChk(arr, LMF010C.ActionType.OUTKA_EDIT, Me._G)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        'inputDataSet作成
        Dim prmDs As DataSet = Me.SetDataSetLMC020InData(frm)
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = prmDs
        prm.RecStatus = RecordStatus.NOMAL_REC

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LMC020", prm)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    '(2013.01.17)要望番号1617 --  END  --

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    ''' <summary>
    ''' 運送複写処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub CopyUnsoData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.UNSO_COPY)

        'チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsCopyUnsoChk(arr, LMF010C.ActionType.UNSO_COPY)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '送信するデータセットに検索条件を設定
        Dim ds As DataSet = Me.SetUnsoPkData(frm, Convert.ToInt32(arr(0)))
        Dim unsoNo As String = ds.Tables(LMF010C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString()

        '==========================
        'WSAクラス呼出
        '==========================

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定(0より大きければ何でもよい)
        MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

        Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMControlC.BLF), LMF010C.ACTION_ID_SELECT, ds)

        'エラー判定
        If MyBase.IsMessageExist() = True Then

            'メッセージ設定
            Call Me._LMFconV.SetMstErrMessage("運送(大)テーブル", unsoNo)

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'パラメータ設定
        Dim prmDs As DataSet = New LMF020DS()
        Dim prmDt As DataTable = prmDs.Tables(LMF020C.TABLE_NM_IN)

        '積込予定日を画面の値で上書きする
        rtnDs.Tables(LMF010C.TABLE_NM_OUT).Rows(0).Item("OUTKA_PLAN_DATE") = frm.imdOrigDate.TextValue
        '荷降予定日を画面の値で上書きする
        rtnDs.Tables(LMF010C.TABLE_NM_OUT).Rows(0).Item("ARR_PLAN_DATE") = frm.imdDestDate.TextValue

        prmDt.ImportRow(rtnDs.Tables(LMF010C.TABLE_NM_OUT).Rows(0))

        Call Me.OpenUnsoEditGamen(prmDs, RecordStatus.COPY_REC)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    ''' <summary>
    ''' 運行編集を新規表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub NewTripData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.UNCO_NEW)

        'チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        '(2012.09.06)START UMANO 要望番号1410 
        'rtnResult = rtnResult AndAlso Me._V.IsNewTripChk(arr)
        'i'rtnResult = rtnResult AndAlso Me._V.IsNewTripChk(arr, True, False)
        rtnResult = rtnResult AndAlso Me._V.IsNewTripChk(arr, Me._G, True, False)
        '(2012.09.06)START UMANO 要望番号1410 

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        Dim rowNo As Integer = 0
        Dim max As Integer = arr.Count - 1
        For i As Integer = 0 To max
            '行番号の取得
            rowNo = Convert.ToInt32(arr(i))

            'START UMANO 要望番号1369 運行紐付け対応
            '支払確定データチェック
            rtnResult = rtnResult AndAlso Me.IsClickDataCheck(frm.sprUnsoUnkou.ActiveSheet, rowNo)
            'END UMANO 要望番号1369 運行紐付け対応
        Next
        'END UMANO 要望番号1302 支払運賃に伴う修正。


        '運行編集表示処理
        rtnResult = rtnResult AndAlso Me.SetNewTripData(frm, arr)

        '2012.08.23 群馬対応 追加START
        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

        End If
        '2012.08.23 群馬対応 追加END

        'チェックを外す
        Call Me.SetDefOff(frm, rtnResult)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 運行編集処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EditTripData(ByVal frm As LMF010F, ByVal actionType As LMF010C.ActionType)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        'START YANAI 20120615 外部権限の変更(春日部対応)
        'Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.UNCO_EDIT)
        Dim rtnResult As Boolean = Me._V.IsAuthority(actionType)
        'END YANAI 20120615 外部権限の変更(春日部対応)

        '選択チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If

        'チェック
        'i'rtnResult = rtnResult AndAlso Me._V.IsEditTripChk(arr, actionType)
        rtnResult = rtnResult AndAlso Me._V.IsEditTripChk(arr, actionType, Me._G)

        '運行画面表示処理
        rtnResult = rtnResult AndAlso Me.SetEditTripData(frm, arr)

        'チェックを外す
        Call Me.SetDefOff(frm, rtnResult)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 運送新規処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub NewUnsoData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.UNSO_NEW)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '荷主Pop起動
        '要望番号1569 terakawa 2012.11.12 Start
        'Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        '要望番号1569 terakawa 2012.11.12 End
        Dim prm As LMFormData = Me.ShowCustPopup(frm, LMF010C.ActionType.UNSO_NEW)
        If Me._V.IsCustPopChk(prm) = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        'パラメータ設定
        Dim ds As DataSet = New LMF020DS()
        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_IN)
        dt.ImportRow(prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0))
        Dim dr As DataRow = dt.Rows(0)
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        dr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()

        '編集画面起動
        Call Me.OpenUnsoEditGamen(ds, RecordStatus.NEW_REC)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="sendFlg">データ送信フラグ(送信後再検索時：True)</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMF010F, Optional ByVal sendFlg As Boolean = False)

        '処理開始アクション
        If Not sendFlg Then
            Call Me.StartAction(frm)
        End If

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.KENSAKU)

        '入力チェック
        'i'rtnResult = rtnResult AndAlso Me._V.IsInputCheck()
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(Me._G)

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

        '画面の値を退避
        Dim shelterDs As DataSet = Me.GetGetShelterData(frm)

        '検索条件の設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, LMF010C.ACTION_ID_SELECT, ds)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, LMF010C.ACTION_ID_SELECT, ds)

                    '取得し値のマージ
                    shelterDs = Me.SetrtnDs(shelterDs, rtnDs)

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '処理終了アクション
                    Call Me.EndAction(frm)
                    Exit Sub

                End If

            Else

                'メッセージエリアの設定(0件エラー)
                MyBase.ShowMessage(frm)

                '次処理にて保持している値のみ表示

            End If

        Else

            If sendFlg Then
                shelterDs = rtnDs
            Else
                '取得し値のマージ
                shelterDs = Me.SetrtnDs(shelterDs, rtnDs)

            End If

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G008", New String() {MyBase.GetResultCount.ToString()})

        End If

        '値の再設定
        Call Me._G.SetSpread(shelterDs)

        'START YANAI 要望番号737 運送検索画面：全体が見えるようにする
        'スプレッドの列の表示・非表示設定
        Call Me._G.SetSpreadVisible()
        'END YANAI 要望番号737 運送検索画面：全体が見えるようにする

        '(2013.01.16)要望番号1773 選択行計算処理 -- START --
        Call Me.SetSumSelect(frm)
        '(2013.01.16)要望番号1773 選択行計算処理 --  END  --

        '(2013.03.12)要望番号1857 ハイライト処理追加 -- START --
        Me._G.SetSpreadColor()
        '(2013.03.12)要望番号1857 ハイライト処理追加 --  END  --

        '処理終了アクション
        If Not sendFlg Then
            Call Me.EndAction(frm)
        End If

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    '2022.08.22 追加START
    ''' <summary>
    ''' データ送信処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DataSend(ByVal frm As LMF010F)

        Try
            '処理開始アクション
            Call Me.StartAction(frm)

            '選択行リスト
            Dim selRowArr As ArrayList = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)

            '同じ運行情報に紐づく運送を全て選択する
            selRowArr = SetChkSameUnsoLLData(frm, selRowArr)

            'エラーチェック
            Dim rtnResult As Boolean = True
            rtnResult = rtnResult AndAlso Me._V.IsDataSendChk(selRowArr, Me._G)

            'エラーの場合、終了
            If rtnResult = False Then
                Exit Sub
            End If

            'DataSet設定
            Dim ds As DataSet = SetLMZ360InData(frm, selRowArr)

            '==========================
            ' WSAクラス呼出
            '==========================
            Dim rtnDs As DataSet = MyBase.CallWSA("LMZ360BLF", "DataAdd", ds)

            'メッセージ判定
            If MyBase.IsMessageStoreExist() Then

                'ＣＳＶ出力処理
                MyBase.MessageStoreDownload()

            Else

                ''送信フラグの値更新
                'SetSoshinFlg(frm, rtnDs)

                '画面制御のため再検索
                SelectListData(frm, True)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G002", New String() {"データ送信", String.Empty})

            End If

        Catch ex As Exception

            MyBase.Logger.WriteErrorLog(
                MyBase.GetType.Name _
                , Reflection.MethodBase.GetCurrentMethod().Name _
                , ex.Message _
                , ex)

            '予期せぬエラー
            MyBase.ShowMessage(frm, "S002")

        Finally

            '処理終了アクション
            Call Me.EndAction(frm)

            'フォーカスの設定
            Call Me._G.SetFoucus()

        End Try

    End Sub
    '2022.08.22 追加END

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- STRAT ---
    ''' <summary>
    ''' 車載受注渡し処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SyasaiListData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.SYASAI_WATASHI)

        '車載受注渡しチェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsSyasaiJutyuWatashiChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF010DS()

        'データセット設定
        Call Me.SearchSasaiDataSet(frm, ds, arr)

        '車載受注渡し処理
        Call Me.SyasaiSaveData(frm, ds)

        '車載受注渡し終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, LMFControlC.FUNCTION_SYASAIU & "処理")

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF010F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF010C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF010C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMF010C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF010C.ActionType.ENTER)

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
        Call Me.ShowPopupControl(frm, objNm, LMF010C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 一括更新処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SaveUnsoLItemData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.SAVE)

        '一括変更チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsSaveChk(arr)

        'START UMANO 要望番号1369 運行紐付け対応
        'イベント種別：「登録」かつ　修正項目：「運行番号」の場合のみ
        '(納入予定日・運送会社)相関チェック
        If frm.optEventY.Checked = True AndAlso frm.cmbShuSei.SelectedValue.ToString().Equals(LMF010C.SHUSEI_TRIP) = True Then
            '(2012.09.06)START UMANO 要望番号1410 
            'rtnResult = rtnResult AndAlso Me._V.IsNewTripChk(arr, False)
            'i'rtnResult = rtnResult AndAlso Me._V.IsNewTripChk(arr, False, False)
            rtnResult = rtnResult AndAlso Me._V.IsNewTripChk(arr, Me._G, False, False)
            '(2012.09.06)END UMANO 要望番号1410

            '運行データキャンセル可否チェック
            rtnResult = rtnResult AndAlso Me.SetTripStatusData(frm, arr, frm.lblTitleTripNo.Text)

        Else

            '運行データキャンセル可否チェック
            rtnResult = rtnResult AndAlso Me.SetTripStatusData(frm, arr)

        End If
        'END UMANO 要望番号1369 運行紐付け対応

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF010DS()
        arr = Me.IsSaveSelectChk(frm, ds, arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnsoSaveData(frm, ds)

        '2012.08.23 群馬対応 追加START
        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

        Else
            Call Me._LMFconH.SetMessageG002(frm, frm.btnHenko.Text, String.Empty)

        End If

        '2012.08.23 群馬対応 追加END

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    '2012.06.22 要望番号1189 追加START
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub UnsoPrintData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.BTN_UNSO_PRINT)

        '印刷エラーチェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            'i'arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, LMF010G.sprUnsoUnkouDef.DEF.ColNo)
            arr = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsPrintChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF010DS()
        '2012.08.23 群馬対応 追加START
        '運行指示書の場合のみ一覧行の運行番号存在チェックを行う
        '2014.07.01 修正START

#If False Then  'UPD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能
          If frm.cmbPrintKb.SelectedValue.Equals("05") = True OrElse _
            frm.cmbPrintKb.SelectedValue.Equals("07") = True OrElse _
             frm.cmbPrintKb.SelectedValue.Equals("11") = True Then

#Else
        If frm.cmbPrintKb.SelectedValue.Equals("05") = True OrElse
            frm.cmbPrintKb.SelectedValue.Equals("07") = True OrElse
            frm.cmbPrintKb.SelectedValue.Equals("14") = True OrElse
            frm.cmbPrintKb.SelectedValue.Equals("15") = True OrElse      'ADD 2022/01/24 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
            frm.cmbPrintKb.SelectedValue.Equals("16") = True OrElse
            frm.cmbPrintKb.SelectedValue.Equals("17") = True OrElse
             frm.cmbPrintKb.SelectedValue.Equals("11") = True Then

#End If
            '2014.07.01 修正END
            arr = Me.IsSprDataChk(frm, ds, arr)
        End If
        '2012.08.23 群馬対応 追加ND

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'データセット設定
        '2014.07.01 修正START
        If frm.cmbPrintKb.SelectedValue.Equals("07") = False AndAlso _
           frm.cmbPrintKb.SelectedValue.Equals("08") = False Then
            ds = Me.SetUnsoPrintData(frm, arr)
        Else
            'メッセージコードの判定
            If MyBase.IsMessageStoreExist = True Then

                MyBase.ShowMessage(frm, "E235")
                'EXCEL起動()
                MyBase.MessageStoreDownload()
            Else
                'パラメータクラス生成
                Dim prm As LMFormData = New LMFormData()

                If frm.cmbPrintKb.SelectedValue.Equals("07") = True Then
                    'オカケンメイトCSV作成(運送データ)実行処理
                    Call Me.OkakenCsvMake(frm, arr, prm)
                ElseIf frm.cmbPrintKb.SelectedValue.Equals("08") = True Then
                    '名鉄CSV作成(運送データ)実行処理
                    Call Me.MeitetuCsvMake(frm, arr, prm)
                End If

            End If
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If
        '2014.07.01 修正END
        Dim rtnDs As DataSet = New LMF010DS()

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        Dim blf As String = String.Empty

        'サーバに渡すレコードが存在する場合、印刷処理
        If 0 < ds.Tables(LMF010C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, LMF010C.ACTION_ID_PRINT_ONLY)

#If True Then   'UPD 2018/11/21 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能

            If frm.cmbPrintKb.SelectedValue.Equals("14") = True Then
                '一括印刷時
                Dim setDs As DataSet = Nothing
                Dim wktDs As DataSet = Nothing
                Dim setRptDt As DataTable = Nothing
                Dim wkRptDt As DataTable = Nothing
                Dim cnt As Integer = 0

                setRptDt = rtnDs.Tables(LMConst.RD)
                setRptDt.Clear()

                Dim dt As DataTable = ds.Tables("LMF010IN").Copy

                Dim inMax As Integer = ds.Tables("LMF010IN").Rows.Count - 1

                For i As Integer = 0 To inMax
                    ds.Tables("LMF010IN").Clear()

                    ds.Tables("LMF010IN").ImportRow(dt.Rows(i))


                    '==========================
                    'WSAクラス呼出
                    '==========================
                    blf = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
                    rtnDs = MyBase.CallWSA(blf, LMF010C.ACTION_ID_PRINT_ONLY, ds)

                    wkRptDt = rtnDs.Tables(LMConst.RD)

                    'プレビュー情報を設定
                    If wkRptDt Is Nothing = False Then
                        cnt = wkRptDt.Rows.Count - 1
                        For j As Integer = 0 To cnt
                            setRptDt.ImportRow(wkRptDt.Rows(j))
                        Next
                    End If
                Next


                'プレビュー処理
                Me._LMFconH.ShowPreviewData(rtnDs)

            Else

                '==========================
                'WSAクラス呼出
                '==========================
                blf = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
                rtnDs = MyBase.CallWSA(blf, LMF010C.ACTION_ID_PRINT_ONLY, ds)

                'プレビュー処理
                Me._LMFconH.ShowPreviewData(rtnDs)

            End If
        End If
#Else
            '==========================
            'WSAクラス呼出
            '==========================
            blf = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
            rtnDs = MyBase.CallWSA(blf, LMF010C.ACTION_ID_PRINT_ONLY, ds)

            'プレビュー処理
            Me._LMFconH.ShowPreviewData(rtnDs)

        End If
#End If


        '2012.08.23 群馬対応 追加START
        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()
        Else
            Call Me._LMFconH.SetMessageG002(frm, frm.btnPrint.Text, String.Empty)

        End If
        ''印刷終了アクション
        'Me._LMFconH.IkkatuEndAction(frm, rtnResult, frm.btnPrint.Text)
        '2012.08.23 群馬対応 追加END

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    '2012.06.22 要望番号1189 追加END

    'UnsocoPrintData 追加START
    ''' <summary>
    ''' 運送会社帳票印刷 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub TrapoPrintData(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.BTN_UNSOCO_PRINT)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        '印刷エラーチェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            _ChkList = Me._LMFconG.GetCheckList(frm.sprUnsoUnkou.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)
        End If
        'rtnResult = rtnResult AndAlso Me._V.IsPrintChk(_ChkList)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        If LMF010C.TrapoPrintSelectValues.CREATE_MEITESU_CSV_WITHOUT_GROUPING _
        .Equals(frm.cmbTrapoPrint.SelectedValue) = True Then

            '実行処理
            Call Me.MeitetuPrintData(frm, Me._ChkList, prm, False)

        End If


        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    '2017/02/27 追加END

    '2012.08.03 群馬対応 追加START
    ''' <summary>
    ''' 印刷処理(検索条件で印刷)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks>配車表(群馬)</remarks>
    Private Sub UnsoPrintData_581(ByVal frm As LMF010F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF010C.ActionType.BTN_UNSO_PRINT)

        '印刷エラーチェック
        'i'rtnResult = rtnResult AndAlso Me._V.IsInputCheck()
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck(Me._G)

        'エラーの場合、終了
        If rtnResult = False Then
            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub
        End If

        '検索条件の設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)
        Dim rtnDs As DataSet = New LMF010DS()

        rtnDs.Merge(New RdPrevInfoDS)
        rtnDs.Tables(LMConst.RD).Clear()

        Dim blf As String = String.Empty

        'サーバに渡すレコードが存在する場合、印刷処理
        If 0 < ds.Tables(LMF010C.TABLE_NM_IN).Rows.Count Then

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, LMF010C.ACTION_ID_PRINT_ONLY)

            '==========================
            'WSAクラス呼出
            '==========================
            blf = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
            rtnDs = MyBase.CallWSA(blf, LMF010C.ACTION_ID_PRINT_ONLY, ds)

            'プレビュー処理
            Me._LMFconH.ShowPreviewData(rtnDs)

        End If

        '印刷終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, frm.btnPrint.Text)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    '2012.08.03 群馬対応 追加END

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMF010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        If rowNo > 0 Then

            '処理開始アクション
            Call Me.StartAction(frm)

            If Me._V.IsAuthority(LMF010C.ActionType.DOUBLECLICK) = False Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            'ダブルクリック時エラーチェック
            Dim rtnResult As Boolean = True
            'i'rtnResult = Me._V.IsDoubleClickChk(rowNo)
            rtnResult = Me._V.IsDoubleClickChk(rowNo, Me._G)

            'エラーの場合、終了
            If rtnResult = False Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            '送信するデータセットに検索条件を設定
            Dim ds As DataSet = Me.SetUnsoPkData(frm, rowNo)
            Dim unsoNo As String = ds.Tables(LMF010C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString()

            '==========================
            'WSAクラス呼出
            '==========================

            '強制実行フラグの設定
            MyBase.SetForceOparation(False)

            '閾値の設定(0より大きければ何でもよい)
            MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

            Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMControlC.BLF), LMF010C.ACTION_ID_SELECT, ds)

            'エラー判定
            If MyBase.IsMessageExist() = True Then

                'メッセージ設定
                Call Me._LMFconV.SetMstErrMessage("運送(大)テーブル", unsoNo)

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            'パラメータ設定
            Dim prmDs As DataSet = New LMF020DS()
            Dim prmDt As DataTable = prmDs.Tables(LMF020C.TABLE_NM_IN)
            prmDt.ImportRow(rtnDs.Tables(LMF010C.TABLE_NM_OUT).Rows(0))

            Call Me.OpenUnsoEditGamen(prmDs, RecordStatus.NOMAL_REC)

            '処理終了アクション
            Call Me.EndAction(frm)

        End If

    End Sub

    ''' <summary>
    ''' イベントオプションボタンによるロック制御
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EventOptionClick(ByVal frm As LMF010F)

        'コンボ生成
        Call Me._G.CreateComboBox()

        'ロック制御
        Call Me._G.EventLockControl()

    End Sub

    ''' <summary>
    ''' 修正項目変更によるロック制御
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShuseiLockControl(ByVal frm As LMF010F)

        'ロック制御
        Call Me._G.ShuseiLockControl()

    End Sub

    '(2013.01.17)要望番号1617 -- START --
    ''' <summary>
    ''' データセット設定(LMC020引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMC020InData(ByVal frm As LMF010F) As DataSet

        'DataSet設定
        Dim ds As DataSet = New LMC020DS()
        Dim dr As DataRow = ds.Tables(LMControlC.LMC020C_TABLE_NM_IN).NewRow()
        Dim max As Integer = frm.sprUnsoUnkou.ActiveSheet.RowCount - 1
        Dim rowno As Integer = 0

        With frm.sprUnsoUnkou.ActiveSheet

            '該当する行を取得
            For i As Integer = 0 To max
                'i'If Me._LMFconG.GetCellValue(.Cells(i, LMF010G.sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then
                If Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then
                    rowno = i
                    Exit For
                End If
            Next

            '2022.08.22 修正START
            'dr.Item("OUTKA_NO_L") = Me._LMFconV.GetCellValue(.Cells(rowno, LMF010C.SprColumnIndex.INOUTKA_NO_L)) '入出荷管理番号
            'dr.Item("NRS_BR_CD") = Me._LMFconV.GetCellValue(.Cells(rowno, LMF010C.SprColumnIndex.NRS_BR_CD))    '営業所コード
            dr.Item("OUTKA_NO_L") = Me._LMFconV.GetCellValue(.Cells(rowno, _G.sprUnsoUnkouDef.INOUTKA_NO_L.ColNo)) '入出荷管理番号
            dr.Item("NRS_BR_CD") = Me._LMFconV.GetCellValue(.Cells(rowno, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))    '営業所コード
            '2022.08.22 修正END

        End With

        ds.Tables(LMControlC.LMC020C_TABLE_NM_IN).Rows.Add(dr)

        Return ds

    End Function
    '(2013.01.17)要望番号1617 --  END  --

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
    Private Function ShowPopupControl(ByVal frm As LMF010F, ByVal objNm As String, ByVal actionType As LMF010C.ActionType) As Boolean

        With frm

            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtUnsocoCd0.Name _
                   , .txtUnsocoBrCd0.Name _
                   , .txtUnsocoCd1.Name _
                   , .txtUnsocoBrCd1.Name _
                   , .txtUnsocoCd2.Name _
                   , .txtUnsocoBrCd2.Name

                    Call Me.SetReturnUnsocoPop(frm, objNm, actionType)

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Call Me.SetReturnCustPop(frm, actionType)

                Case .txtTripNo.Name

                    Call Me.SetReturnTripPop(frm)

                Case .txtCntUserCd.Name

                    Call Me.SetReturnUser(frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMF010F, ByVal actinType As LMF010C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, actinType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNm.TextValue = Me._LMFconG.EditConcatData(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)
            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actinType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMF010F, ByVal actinType As LMF010C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim csFlg As String = LMConst.FLG.ON

            Select Case actinType

                Case LMF010C.ActionType.UNSO_NEW

                    '運送新規の場合、初期荷主の値を設定
                    '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                    'brCd = LMUserInfoManager.GetNrsBrCd()
                    brCd = frm.cmbEigyo.SelectedValue.ToString()
                    csFlg = LMConst.FLG.OFF

            End Select

            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            '要望番号1569 terakawa 2012.11.12 Start
            ' If actinType = LMF010C.ActionType.ENTER Then
            If actinType = LMF010C.ActionType.ENTER _
              OrElse actinType = LMF010C.ActionType.UNSO_NEW Then
                '要望番号1569 terakawa 2012.11.12 End
                'END SHINOHARA 要望番号513
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運送会社Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUnsocoPop(ByVal frm As LMF010F, ByVal objNm As String, ByVal actinType As LMF010C.ActionType) As Boolean

        Dim ctlNm As String = Me.GetUnsocoCdCtlNm(objNm)
        Dim unsoArr As ArrayList = Me.GetUnsocoCtlNmArr(ctlNm)
        Dim cdCtl As Win.InputMan.LMImTextBox = Me._LMFconH.GetTextControl(frm, unsoArr(0).ToString())
        Dim brCtl As Win.InputMan.LMImTextBox = Me._LMFconH.GetTextControl(frm, unsoArr(1).ToString())
        Dim prm As LMFormData = Me.ShowUnsocoPopup(frm, cdCtl, brCtl, ctlNm, actinType)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ250C.TABLE_NM_OUT).Rows(0)

            cdCtl.TextValue = dr.Item("UNSOCO_CD").ToString()
            brCtl.TextValue = dr.Item("UNSOCO_BR_CD").ToString()
            Me._LMFconH.GetTextControl(frm, unsoArr(2).ToString()).TextValue _
            = Me._LMFconG.EditConcatData(dr.Item("UNSOCO_NM").ToString(), dr.Item("UNSOCO_BR_NM").ToString(), LMFControlC.ZENKAKU_SPACE)

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運送会社マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="cdCtl">コードコントロール</param>
    ''' <param name="brCtl">支店コードコントロール</param>
    ''' <param name="lastCtlNm">後ろ1桁</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function ShowUnsocoPopup(ByVal frm As LMF010F _
                                     , ByVal cdCtl As Win.InputMan.LMImTextBox _
                                     , ByVal brCtl As Win.InputMan.LMImTextBox _
                                     , ByVal lastCtlNm As String _
                                     , ByVal actinType As LMF010C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ250DS()
        Dim dt As DataTable = ds.Tables(LMZ250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            Dim brCd As String = frm.cmbEigyo.SelectedValue.ToString()
            Dim csFlg As String = LMConst.FLG.ON

            Select Case Convert.ToInt32(lastCtlNm)

                Case LMF010C.UNSOTYPE.UNSOTYPE_0

                    '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                    'brCd = LMUserInfoManager.GetNrsBrCd()
                    brCd = frm.cmbEigyo.SelectedValue.ToString()
                    csFlg = LMConst.FLG.OFF

            End Select

            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If actinType = LMF010C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNSOCO_CD") = cdCtl.TextValue
                .Item("UNSOCO_BR_CD") = brCtl.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ250", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 運行検索Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTripPop(ByVal frm As LMF010F) As Boolean

        Dim prm As LMFormData = Me.ShowTripPopup(frm)
        If prm.ReturnFlg = True Then

            Dim dr As DataRow = prm.ParamDataSet.Tables(LMF210C.TABLE_NM_OUT).Rows(0)
            frm.txtTripNo.TextValue = dr.Item("TRIP_NO").ToString()
            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 運行検索POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function ShowTripPopup(ByVal frm As LMF010F) As LMFormData

        Dim ds As DataSet = New LMF210DS()
        Dim dt As DataTable = ds.Tables(LMF210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            .Item("TRIP_NO") = frm.txtTripNo.TextValue

        End With

        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMF210", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' ユーザマスタ検索の戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnUser(ByVal frm As LMF010F, ByVal actionType As LMF010C.ActionType) As Boolean

        With frm

            'Enter以外、スルー
            If LMF010C.ActionType.ENTER <> actionType Then
                Return True
            End If

            'キャッシュから取得
            Dim ctl As Win.InputMan.LMImTextBox = .txtCntUserCd
            Dim drs As DataRow() = Me._LMFconG.SelectUserListDataRow(ctl.TextValue)
            If drs.Length < 1 Then

                Return False

            End If

            ctl.TextValue = drs(0).Item("USER_CD").ToString()
            .lblCntUserNm.TextValue = drs(0).Item("USER_NM").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' ログインユーザマスタのWH_CD戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SeUserWH(ByVal frm As LMF010F) As String

        With frm

            'キャッシュから取得
            Dim drs As DataRow() = Me._LMFconG.SelectUserListDataRow(MyBase.GetUserID())
            If drs.Length < 1 Then

                Return String.Empty

            End If

            Return drs(0).Item("WH_CD").ToString()

        End With

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF010F)

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
    Private Sub EndAction(ByVal frm As LMF010F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 運送テーブルのPK文字列を取得
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>文字列</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoPkSelectSeq(ByVal dr As DataRow) As String
        Return String.Concat("UNSO_NO_L = '", dr.Item("UNSO_NO_L").ToString(), "' ")
    End Function

    ''' <summary>
    ''' 運送会社コントロール名のリストを生成
    ''' </summary>
    ''' <param name="ctlNm">後ろ1桁</param>
    ''' <returns>コントロール名のリスト</returns>
    ''' <remarks>
    ''' リスト
    ''' ①：コード
    ''' ②：支店コード
    ''' ③：会社名
    ''' </remarks>
    Private Function GetUnsocoCtlNmArr(ByVal ctlNm As String) As ArrayList

        GetUnsocoCtlNmArr = New ArrayList()

        'コード名を設定
        GetUnsocoCtlNmArr.Add(String.Concat(LMF010C.UNSOCO_CD, ctlNm))

        '支店コード名を設定
        GetUnsocoCtlNmArr.Add(String.Concat(LMF010C.UNSOCO_BR, ctlNm))

        'ラベル名を設定
        GetUnsocoCtlNmArr.Add(String.Concat(LMF010C.UNSOCO_NM, ctlNm))

        Return GetUnsocoCtlNmArr

    End Function

    ''' <summary>
    ''' 運送会社コントロール名のリストを生成
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>コントロール名のリスト</returns>
    ''' <remarks>
    ''' リスト
    ''' ①：コード
    ''' ②：支店コード
    ''' ③：会社名
    ''' </remarks>
    Private Function GetUnsocoCtlNm(ByVal objNm As String) As ArrayList

        GetUnsocoCtlNm = New ArrayList()

        '後ろ1桁を取得
        Dim ctlNm As String = Me.GetUnsocoCdCtlNm(objNm)

        Return Me.GetUnsocoCtlNmArr(ctlNm)

    End Function

    ''' <summary>
    ''' 後ろ1桁を取得
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>後ろ1桁</returns>
    ''' <remarks></remarks>
    Private Function GetUnsocoCdCtlNm(ByVal objNm As String) As String
        Return objNm.Substring(objNm.Length - 1, 1)
    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UnsoSaveData(ByVal frm As LMF010F, ByVal ds As DataSet) As Boolean

        '確認メッセージ表示
        If Me._LMFconH.SetMessageC001(frm, frm.btnHenko.Text) = False Then
            Return False
        End If

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF010C.ACTION_ID_SAVE
        If frm.optEventN.Checked = True Then
            actionId = LMF010C.ACTION_ID_REMOVED
        End If

        'エラーがある場合、終了
        Dim rtnResult As Boolean = Me.ActionData(frm, ds, actionId, rtnDs)

        If rtnResult = False Then

            'エラー行のマージ
            ds = Me.ErrDataMerge(ds, rtnDs)
            Call Me.SetServerErrControl(frm)

        End If

        'チェックボックスをはずす
        Call Me._G.SetDefOff(ds)

        Return rtnResult

    End Function

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- START ---
    ''' <summary>
    ''' 車載受注受渡し処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <remarks></remarks>
    Private Sub SyasaiSaveData(ByVal frm As LMF010F, ByVal ds As DataSet)

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing
        Dim actionId As String = LMF010C.ACTION_SYASAI_EATASI

        'エラーがある場合、終了
        Dim rtnResult As Boolean = Me.ActionData(frm, ds, actionId, rtnDs)

        If rtnResult = False Then

            'エラー行のマージ
            ds = Me.ErrDataMerge(ds, rtnDs)
            Call Me.SetServerErrControl(frm)

        End If

        'チェックボックスをはずす
        Call Me._G.SetDefOff(ds)

    End Sub
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

    ''' <summary>
    ''' サーバアクセス(チェック有)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <param name="rtnDs">戻りDataSet 初期値 = Nothing</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ActionData(ByVal frm As LMF010F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        '(2012.09.06)START UMANO 要望番号1410
        'サブ内共通を使用するとHクラスだとエラーEXCELが存在しないので直接呼出
        ''サーバアクセス
        'rtnDs = Me._LMFconH.ServerAccess(ds, actionId)
        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        rtnDs = MyBase.CallWSA(blf, actionId, ds)
        '(2012.09.06)END UMANO 要望番号1410

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

    ''' <summary>
    ''' 運行新規のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetNewTripData(ByVal frm As LMF010F, ByVal arr As ArrayList) As Boolean

        'パラメータ設定
        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_UNSO_L)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        For i As Integer = 0 To max

            dr = dt.NewRow()
            recNo = Convert.ToInt32(arr(i))
            'i'dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, LMF010G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            'i'dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, LMF010G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
            'i'dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, LMF010G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo))
            'i'dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, LMF010G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo))
            dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
            dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo))
            dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo))
            dt.Rows.Add(dr)

        Next

        'エラーがある場合、終了
        Dim rtnDs As DataSet = Nothing
        If Me.ActionData(frm, ds, LMF010C.ACTION_ID_HAITA_DATA, rtnDs) = False Then
            Return False
        End If

        'パラメータ設定
        Dim prmDs As DataSet = New LMF030DS()
        Dim prmDt As DataTable = prmDs.Tables(LMF030C.TABLE_NM_IN)

        '1行目には自営業を設定
        Dim prmDr As DataRow = prmDt.NewRow()
        '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
        prmDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
        'prmDr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
        prmDt.Rows.Add(prmDr)
        max = dt.Rows.Count - 1
        For i As Integer = 0 To max
            prmDt.ImportRow(dt.Rows(i))
        Next

        '運行編集を新規モードで表示
        Call Me.OpenUncoEditGamen(prmDs, RecordStatus.NEW_REC)

        Return True

    End Function

    ''' <summary>
    ''' 運行編集のパラメータ設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetEditTripData(ByVal frm As LMF010F, ByVal arr As ArrayList) As Boolean

        With frm.sprUnsoUnkou.ActiveSheet

            Dim rowNo As Integer = Convert.ToInt32(arr(0))
            'i'Dim brCd As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            'i'Dim tripNo As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO.ColNo))
            'i'Dim tripNoHaika As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))
            'i'Dim tripNoTyukei As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))
            'i'Dim tripNoShuka As String = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))
            Dim brCd As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            Dim tripNo As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))
            Dim tripNoHaika As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))
            Dim tripNoTyukei As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))
            Dim tripNoShuka As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))

            Dim prmDs As DataSet = Nothing
            Dim chkArr As ArrayList = New ArrayList()

            'i'Select Case Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))
            Select Case Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))

                Case LMFControlC.FLG_ON

                    '配送区分で切り替える
                    Select Case frm.cmbHaiso.SelectedValue.ToString()

                        Case LMFControlC.HAISO_SHUKA

                            '運行編集を編集モードで表示(運行番号 集荷)
                            If String.IsNullOrEmpty(Me.SetEditTripData(brCd, tripNoShuka)) = False Then
                                chkArr.Add(tripNoShuka)
                            End If

                        Case LMFControlC.HAISO_THUKEI

                            '運行編集を編集モードで表示(運行番号 中継)
                            If String.IsNullOrEmpty(Me.SetEditTripData(brCd, tripNoTyukei)) = False Then
                                chkArr.Add(tripNoTyukei)
                            End If

                        Case LMFControlC.HAISO_HAIKA

                            '運行編集を編集モードで表示(運行番号 配荷)
                            If String.IsNullOrEmpty(Me.SetEditTripData(brCd, tripNoHaika)) = False Then
                                chkArr.Add(tripNoHaika)
                            End If

                    End Select

                Case LMFControlC.FLG_OFF

                    '運行編集を編集モードで表示(運行番号 集荷)
                    If String.IsNullOrEmpty(Me.SetEditTripData(brCd, tripNo)) = False Then
                        chkArr.Add(tripNo)
                    End If

            End Select

            Dim msg As String = Me.SetTripDataChkMsg(chkArr)
            If String.IsNullOrEmpty(msg) = False Then
                Return Me._LMFconV.SetMstErrMessage("運送(特大)テーブル", msg)
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運行編集のパラメータ設定
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tripNo">運行番号</param>
    ''' <returns>レコード存在チェックでエラーの場合、TripNo　その他：空文字</returns>
    ''' <remarks></remarks>
    Private Function SetEditTripData(ByVal brCd As String, ByVal tripNo As String) As String

        '値がない場合、スルー
        If String.IsNullOrEmpty(tripNo) = True Then
            Return String.Empty
        End If

        'レコード存在チェック
        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_ITEM)
        Dim dr As DataRow = dt.NewRow()
        dr.Item("NRS_BR_CD") = brCd
        dr.Item("TRIP_NO") = tripNo
        dr.Item("ITEM_DATA") = LMF010C.SHUSEI_TRIP
        dt.Rows.Add(dr)

        '検索処理
        Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(MyBase.GetPGID, LMControlC.BLF), LMF010C.ACTION_ID_SELECT_TRIP, ds)
        If MyBase.IsMessageExist() = True Then
            Return tripNo
        End If

        'パラメータ設定
        Dim prmDs As DataSet = New LMF030DS()
        Dim prmDt As DataTable = prmDs.Tables(LMF030C.TABLE_NM_IN)
        prmDt.ImportRow(dt.Rows(0))

        '運行編集を表示
        Call Me.OpenUncoEditGamen(prmDs, RecordStatus.NOMAL_REC)
        Return String.Empty

    End Function

    ''' <summary>
    ''' 運行編集時のエラーメッセージ設定
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>置換文字</returns>
    ''' <remarks></remarks>
    Private Function SetTripDataChkMsg(ByVal arr As ArrayList) As String

        Dim max As Integer = arr.Count - 1

        If max < 0 Then
            Return String.Empty
        End If

        SetTripDataChkMsg = arr(0).ToString()
        For i As Integer = 1 To max
            SetTripDataChkMsg = String.Concat(SetTripDataChkMsg, " , ", arr(i).ToString())
        Next

        Return SetTripDataChkMsg

    End Function

    ''' <summary>
    ''' エラー行のマージ
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rtnDs">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ErrDataMerge(ByVal ds As DataSet, ByVal rtnDs As DataSet) As DataSet

        Dim errDt As DataTable = ds.Tables(LMF010C.TABLE_NM_ERR)
        Dim rtnDt As DataTable = rtnDs.Tables(LMF010C.TABLE_NM_ERR)
        Dim max As Integer = rtnDt.Rows.Count - 1
        For i As Integer = 0 To max
            errDt.ImportRow(rtnDt.Rows(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 一括変更処理のエラーコントロール設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetServerErrControl(ByVal frm As LMF010F)

        Dim msg As String = frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text
        If String.IsNullOrEmpty(msg) = True Then
            Exit Sub
        End If

        If "E079".Equals(frm.Controls.Find(LMFControlC.MES_AREA, True)(0).Text.Substring(0, 4)) = True Then
            Me._LMFconV.SetErrorControl(frm.txtTripNo)
        End If

    End Sub

    ''' <summary>
    ''' チェックボックスのチェックを外す
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rtnResult">チェック結果</param>
    ''' <remarks></remarks>
    Private Sub SetDefOff(ByVal frm As LMF010F, ByVal rtnResult As Boolean)

        'エラーがある場合、スルー
        If rtnResult = False Then
            Exit Sub
        End If

        'チェックボックスをはずす
        Call Me._G.SetDefOff(Nothing)

    End Sub

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF010F)

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
    Private Sub SetInitMessage(ByVal frm As LMF010F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索部データ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetConditionDataSet(ByVal frm As LMF010F) As DataSet

        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("YUSO_BR_CD") = .cmbBetsuEigyo.SelectedValue.ToString()
            dr.Item("UNSO_CD") = .txtUnsocoCd1.TextValue
            dr.Item("UNSO_BR_CD") = .txtUnsocoBrCd1.TextValue
            dr.Item("UNSOCO_CD") = .txtUnsocoCd2.TextValue
            dr.Item("UNSOCO_BR_CD") = .txtUnsocoBrCd2.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("JSHA_KB") = .cmbJshaKb.SelectedValue.ToString()
            dr.Item("DATE_KBN") = .cmbDateKb.SelectedValue.ToString()
            dr.Item("DATE_FROM") = .imdTripDateFrom.TextValue
            dr.Item("DATE_TO") = .imdTripDateTo.TextValue
            dr.Item("SYS_ENT_USER") = .txtCntUserCd.TextValue
            dr.Item("UNCO_ARI_NASHI") = Me.GetUmuData(.optTripY, .optTripN)
            dr.Item("TYUKEI_HAISO_FLG") = Me.GetUmuData(.optChukeiY, .optChukeiN)
            dr.Item("PRINT_KB") = frm.cmbPrintKb.SelectedValue.ToString()   '2012.08.03 追加

        End With

        'スプレッド項目
        With frm.sprUnsoUnkou.ActiveSheet
            'i'
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
            dr.Item("BIN_KB") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.BIN_KB.ColNo))
            dr.Item("TARIFF_BUNRUI_KB") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.BUNRUI.ColNo))
            dr.Item("UNSOCO_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSOCO_2.ColNo))
            dr.Item("CUST_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.CUST_NM.ColNo))
            dr.Item("CUST_REF_NO") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.CUST_REF_NO.ColNo))
            dr.Item("ORIG_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.ORIG_NM.ColNo))
            dr.Item("DEST_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.DEST_NM.ColNo))
            dr.Item("DEST_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.DEST_NM.ColNo))
            dr.Item("DEST_ADD") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.DEST_AD.ColNo))
            dr.Item("DEST_ADD2") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.DEST_ADD2.ColNo))
            dr.Item("AREA_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.AREA.ColNo))
            dr.Item("KANRI_NO_L") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.INOUTKA_NO_L.ColNo))
            dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))
            dr.Item("DRIVER_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.DRIVER.ColNo))
            dr.Item("CAR_TP_KB") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.VCLE_KB.ColNo))
            dr.Item("CAR_NO") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.CAR_NO.ColNo))
            dr.Item("UNSO_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSOCO_1.ColNo))
            dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSO_REM.ColNo))
            dr.Item("SEIQ_GROUP_NO") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.GROUP_NO.ColNo))
            dr.Item("UNSO_ONDO_KB") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSO_ONDO_KB.ColNo))
            dr.Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo))
            dr.Item("SYUKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo))
            dr.Item("HAIKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.HAIKA_RELY_POINT.ColNo))
            dr.Item("TRIP_NO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))
            dr.Item("TRIP_NO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))
            dr.Item("TRIP_NO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))
            dr.Item("UNSOCO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSOCO_SHUKA.ColNo))
            dr.Item("UNSOCO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSOCO_CHUKEI.ColNo))
            dr.Item("UNSOCO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.UNSOCO_HAIKA.ColNo))
            dr.Item("SYS_ENT_USER_NM") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.CNT_USER.ColNo))
            '要望番号2140 追加START 2013.12.25
            dr.Item("ALCTD_STS") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.ALCTD_STS.ColNo))
            '要望番号2140 追加END 2013.12.25
            dr.Item("DENP_NO") = Me._LMFconG.GetCellValue(.Cells(0, _G.sprUnsoUnkouDef.DENP_NO.ColNo))
        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 有無取得
    ''' </summary>
    ''' <param name="optYes">有のコントロール</param>
    ''' <param name="optNo">無のコントロール</param>
    ''' <returns>有無取得</returns>
    ''' <remarks></remarks>
    Private Function GetUmuData(ByVal optYes As Win.LMOptionButton, ByVal optNo As Win.LMOptionButton) As String

        GetUmuData = String.Empty

        '有にチェック
        If LMConst.FLG.ON.Equals(Me._LMFconH.GetOptData(optYes)) = True Then

            GetUmuData = LMFControlC.FLG_ON

        End If

        '無にチェック
        If LMConst.FLG.ON.Equals(Me._LMFconH.GetOptData(optNo)) = True Then

            GetUmuData = LMFControlC.FLG_OFF

        End If

        Return GetUmuData

    End Function

    ''' <summary>
    ''' 検索前の退避処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function GetGetShelterData(ByVal frm As LMF010F) As DataSet

        '退避するDataSetのインスタンス生成
        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_OUT)
        Dim dr As DataRow = Nothing

        With frm.sprUnsoUnkou.ActiveSheet

            '明細の行数分、ループ
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 1 To max

                'チェックが無い場合、スルー
                'i'If LMConst.FLG.OFF.Equals(Me._LMFconG.GetCellValue(.Cells(i, LMF010G.sprUnsoUnkouDef.DEF.ColNo))) = True Then
                If LMConst.FLG.OFF.Equals(Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DEF.ColNo))) = True Then
                    Continue For
                End If

                'DataRowのインスタンス生成
                dr = dt.NewRow()

                dr.Item("C_SELECT") = LMConst.FLG.ON
                'i'
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
                dr.Item("BIN") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.BIN_KB.ColNo))
                dr.Item("BIN_UNSO_LL") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.BIN_KB_UNSO_LL.ColNo))   'ADD 2018/12/19 要望番号000880
                dr.Item("BUNRUI") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.BUNRUI.ColNo))
                dr.Item("UNSOCO_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_CD_2.ColNo))
                dr.Item("UNSOCO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_BR_CD_2.ColNo))
                dr.Item("UNSOCO_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_NM_2.ColNo))
                dr.Item("CUST_REF_NO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.CUST_REF_NO.ColNo))
                dr.Item("ORIG_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.ORIG_CD.ColNo))
                dr.Item("ORIG_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.ORIG_NM.ColNo))
                dr.Item("DEST_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DEST_CD.ColNo))
                dr.Item("DEST_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DEST_NM.ColNo))
                dr.Item("DEST_ADD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DEST_AD.ColNo))
                dr.Item("AREA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.AREA.ColNo))
                dr.Item("UNSO_PKG_NB") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSO_NB.ColNo))
                dr.Item("UNSO_WT") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.WT.ColNo))
                dr.Item("SHOMI_JURYO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SHOMI_WT.ColNo))
                dr.Item("KANRI_NO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.INOUTKA_NO_L.ColNo))
                dr.Item("OUTKA_PLAN_DATE") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.OUTKA_PLAN_DATE.ColNo))
                dr.Item("ARR_PLAN_DATE") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo))
                dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))
                dr.Item("DRIVER_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DRIVER.ColNo))
                dr.Item("TRIP_DATE") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.TRIP_DATE.ColNo))
                dr.Item("CAR_TP") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.VCLE_KB.ColNo))
                dr.Item("CAR_NO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.CAR_NO.ColNo))
                dr.Item("UNSO_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo))
                dr.Item("UNSO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo))
                dr.Item("UNSO_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_NM_1.ColNo))
                dr.Item("UNSO_BR_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_BR_NM_1.ColNo))
                dr.Item("CUST_NM_L") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.CUST_NM_L.ColNo))
                dr.Item("CUST_NM_M") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.CUST_NM_M.ColNo))
                dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSO_REM.ColNo))
                dr.Item("UNCHIN") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNCHIN.ColNo))
                dr.Item("KYORI") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.KYORI.ColNo))
                dr.Item("GROUP_NO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.GROUP_NO.ColNo))
                dr.Item("UNSO_ONDO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSO_ONDO_KB.ColNo))
                dr.Item("MOTO_DATA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo))
                dr.Item("SYUKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo))
                dr.Item("HAIKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.HAIKA_RELY_POINT.ColNo))
                dr.Item("TRIP_NO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))
                dr.Item("TRIP_NO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))
                dr.Item("TRIP_NO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))
                dr.Item("UNSOCO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_NM_SHUKA.ColNo))
                dr.Item("UNSOCO_BR_SYUKA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_BR_NM_SHUKA.ColNo))
                dr.Item("UNSOCO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_NM_CHUKEI.ColNo))
                dr.Item("UNSOCO_BR_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_BR_NM_CHUKEI.ColNo))
                dr.Item("UNSOCO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_NM_HAIKA.ColNo))
                dr.Item("UNSOCO_BR_HAIKA") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSOCO_BR_NM_HAIKA.ColNo))
                dr.Item("TYUKEI_HAISO_FLG") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))
                dr.Item("UNSO_TEHAI_KB") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNSO_TEHAI_KB.ColNo))
                '要望番号2140 追加START 2013.12.25
                dr.Item("ALCTD_STS") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.ALCTD_STS.ColNo))
                '要望番号2140 追加END 2013.12.25
                dr.Item("SYS_ENT_NM") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.CNT_USER.ColNo))
                dr.Item("SYS_ENT_DATE") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.CNT_DATE.ColNo))
                dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
                dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo))

                '(2013.01.17)追加漏れと判断により 大貫 -- START --
                dr.Item("SHIHARAI_UNCHIN") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo))
                dr.Item("SHIHARAI_FIXED_FLAG") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo))
                '(2013.01.17)追加漏れと判断により 大貫 --  END  --

                dr.Item("DENP_NO") = Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DENP_NO.ColNo))

                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理で取得した値のマージ
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="rtnDs">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function SetrtnDs(ByVal ds As DataSet, ByVal rtnDs As DataSet) As DataSet

        '画面DataSetの件数が少ない場合
        If ds.Tables(LMF010C.TABLE_NM_OUT).Rows.Count < rtnDs.Tables(LMF010C.TABLE_NM_OUT).Rows.Count Then
            ds = Me.SetrtnDsMainGuiDs(ds, rtnDs)
        Else
            ds = Me.SetrtnDsMainDacDs(ds, rtnDs)
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理で取得した値のマージ(主体を画面DataSet)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="rtnDs">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function SetrtnDsMainGuiDs(ByVal ds As DataSet, ByVal rtnDs As DataSet) As DataSet

        Dim rtnDt As DataTable = rtnDs.Tables(LMF010C.TABLE_NM_OUT)
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_OUT)
        Dim drs As DataRow() = Nothing
        Dim max As Integer = 0
        Dim cnt As Integer = dt.Rows.Count - 1

        '戻り値から既に設定されている行を削除
        For i As Integer = 0 To cnt
            drs = rtnDt.Select(Me.GetUnsoPkSelectSeq(dt.Rows(i)))
            max = drs.Length - 1
            For j As Integer = 0 To max
                drs(j).Delete()
            Next

        Next

        '戻り値の反映
        max = rtnDt.Rows.Count - 1
        For i As Integer = 0 To max
            dt.ImportRow(rtnDt.Rows(i))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' 検索処理で取得した値のマージ(主体を戻りDataSet)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="rtnDs">データセット</param>
    ''' <returns>データセット</returns>
    ''' <remarks></remarks>
    Private Function SetrtnDsMainDacDs(ByVal ds As DataSet, ByVal rtnDs As DataSet) As DataSet

        Dim rtnDt As DataTable = rtnDs.Tables(LMF010C.TABLE_NM_OUT)
        Dim max As Integer = rtnDt.Rows.Count - 1
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_OUT)
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To max

            '取得したデータがもともとあるかを判定
            dr = rtnDt.Rows(i)
            If dt.Select(Me.GetUnsoPkSelectSeq(dr)).Length = 0 Then

                dt.ImportRow(dr)

            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)PK設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoPkData(ByVal frm As LMF010F, ByVal rowNo As Integer) As DataSet

        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With frm.sprUnsoUnkou.ActiveSheet

            'i'dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            'i'dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
            dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))

        End With

        dt.Rows.Add(dr)

        Return ds

    End Function

    '2012.06.22 要望番号1189 追加START
    ''' <summary>
    ''' 印刷時のデータセット(運送(大)PK)設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">行番号(arrayList)</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoPrintData(ByVal frm As LMF010F, ByVal arr As ArrayList) As DataSet

        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_IN)
        Dim rowNo As Integer = 0
        Dim dr As DataRow
        For i As Integer = 0 To arr.Count - 1
            rowNo = Convert.ToInt32(arr(i))

            dr = dt.NewRow()
            With frm.sprUnsoUnkou.ActiveSheet

                'i'dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
                'i'dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
                'i'dr.Item("PRINT_KB") = frm.cmbPrintKb.SelectedValue.ToString()
                ' ''2012.08.23 追加START 群馬対応 運行指示書追加
                'i'dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO.ColNo))
                ' ''2012.08.23 追加END 群馬対応 運行指示書追加
                dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
                dr.Item("PRINT_KB") = frm.cmbPrintKb.SelectedValue.ToString()
                '2012.08.23 追加START 群馬対応 運行指示書追加
                dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))
                '2012.08.23 追加END 群馬対応 運行指示書追加
                '追加開始 2015.05.13 要望番号:2295
                dr.Item("UNSO_PKG_NB") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NB.ColNo))
                '追加終了 2015.05.13 要望番号:2295

#If True Then    'ADD 2019/06/10 005795【LMS】運送メニュー日陸便の場合、一括印刷で荷札印刷しない

                dr.Item("UNSO_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo))
                dr.Item("UNSO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo))

                'キャッシュ(運送会社M)から値取得
                Dim drs As DataRow() = Me._LMFconG.SelectUnsocoListDataRow(dr.Item("NRS_BR_CD").ToString.Trim, _
                                                                          dr.Item("UNSO_CD").ToString.Trim, _
                                                                          dr.Item("UNSO_BR_CD").ToString.Trim _
                                                                          )

                If drs.Length > 0 Then
                    dr.Item("NIHUDA_FLAG") = drs(0).Item("NIHUDA_YN").ToString()
                Else
                    dr.Item("NIHUDA_FLAG") = "00"
                End If

#End If
#If True Then   'ADD 2021/01/07 026832   【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
                dr.Item("KANRI_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.INOUTKA_NO_L.ColNo))        '管理番号
                dr.Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo))    '元ﾃﾞｰﾀ"
#End If
            End With

            dt.Rows.Add(dr)

        Next

        Return ds

    End Function
    '2012.06.22 要望番号1189 追加END

    ''' <summary>
    ''' 一括変更時の更新情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetSaveDataSet(ByVal frm As LMF010F, ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        'Itemに値設定
        Dim itemDt As DataTable = ds.Tables(LMF010C.TABLE_NM_ITEM)
        Dim itemDr As DataRow = itemDt.NewRow()
        With frm

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'itemDr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            itemDr.Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            itemDr.Item("TRIP_NO") = .txtTripNo.TextValue
            itemDr.Item("BIN_KB") = .cmbBinKb.SelectedValue
            itemDr.Item("UNSO_CD") = .txtUnsocoCd0.TextValue
            itemDr.Item("UNSO_BR_CD") = .txtUnsocoBrCd0.TextValue
            itemDr.Item("SYUKA_TYUKEI_CD") = .cmbChukeiFrom.SelectedValue
            itemDr.Item("HAIKA_TYUKEI_CD") = .cmbChukeiTo.SelectedValue
            itemDr.Item("ITEM_DATA") = .cmbShuSei.SelectedValue

        End With

        itemDt.Rows.Add(itemDr)

        'UNSO_Lの情報を設定
        Dim unsoDt As DataTable = ds.Tables(LMF010C.TABLE_NM_UNSO_L)
        Dim unsoDr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0

        With frm.sprUnsoUnkou.ActiveSheet

            For i As Integer = 0 To max

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                unsoDr = unsoDt.NewRow()
                'i'
                unsoDr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
                unsoDr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
                unsoDr.Item("TYUKEI_HAISO_FLG") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))
                unsoDr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo))
                unsoDr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo))
                unsoDr.Item("ROW_NO") = rowNo
                unsoDr.Item("ARR_PLAN_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo)))
                unsoDr.Item("OUTKA_PLAN_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.OUTKA_PLAN_DATE.ColNo)))
                unsoDr.Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.MOTO_DATA.ColNo))

                'START UMANO 要望番号1369 運行紐付け対応
                unsoDr.Item("UNSO_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo))
                unsoDr.Item("UNSO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo))
                'END UMANO 要望番号1369 運行紐付け対応

#If True Then ' 西濃自動送り状番号対応 20160705 added inoue
                Dim newAutoDenpKbn As String = Me.GetAutoDenpKbnFromUnsoco(TryCast(itemDr.Item("NRS_BR_CD"), String) _
                                                                         , TryCast(itemDr.Item("UNSO_CD"), String) _
                                                                         , TryCast(itemDr.Item("UNSO_BR_CD"), String))

                Dim autoDenpNo As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.AUTO_DENP_NO.ColNo))
                Dim autoDenpKbn As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.AUTO_DENP_KBN.ColNo))
                If (String.IsNullOrEmpty(newAutoDenpKbn) OrElse _
                    newAutoDenpKbn.Equals(autoDenpKbn) = False) Then

                    ' 自動送り状番号区分未設定 or 変更
                    unsoDr.Item("AUTO_DENP_NO") = String.Empty
                Else

                    unsoDr.Item("AUTO_DENP_NO") = autoDenpNo
                End If

                unsoDr.Item("AUTO_DENP_KBN") = newAutoDenpKbn
#End If

#If True Then   'ADD 2019/08/05 005193   【LMS】運行運送情報画面_デフォルト便から変更すると距離が0に置換され、支払運賃が0になるバグ(群馬柿沼)T13_07→対応不要？ T14_11　T15-06 
                Dim sWH_CD As String = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.WH_CD.ColNo))
                If String.Empty.Equals(sWH_CD) Then
                    sWH_CD = SeUserWH(frm)
                End If
                unsoDr.Item("WH_CD") = sWH_CD

#End If
                unsoDt.Rows.Add(unsoDr)

            Next

        End With

        With frm

            '区分マスタより取得
            Dim kbnDetailDr() As DataRow = Nothing
            kbnDetailDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'T029' AND ", _
                                                                                               "KBN_NM1 = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                                               "KBN_NM2 = '", .txtUnsocoCd0.TextValue, "' AND ", _
                                                                                               "KBN_NM3 = '", .txtUnsocoBrCd0.TextValue, "'"))

            If kbnDetailDr.Count > 0 Then
                Dim row As DataRow = ds.Tables(LMF010C.TABLE_NM_OKURIJYO_WK).NewRow
                row("OKURIJYO_HEAD") = kbnDetailDr(0).Item("KBN_NM4")

                ds.Tables(LMF010C.TABLE_NM_OKURIJYO_WK).Rows.Add(row)
            End If

        End With

        Return True

    End Function



    ''' <summary>
    ''' 自動送り状番号区分取得(M_UNSOCO)
    ''' </summary>
    ''' <param name="nrsBrCd"></param>
    ''' <param name="unsocoCd"></param>
    ''' <param name="unsoBrCd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetAutoDenpKbnFromUnsoco(ByVal nrsBrCd As String _
                                            , ByVal unsocoCd As String _
                                            , ByVal unsoBrCd As String) As String

        Dim autoDenpKbn As String = String.Empty

        If (String.IsNullOrEmpty(nrsBrCd) = False AndAlso _
            String.IsNullOrEmpty(unsocoCd) = False AndAlso _
            String.IsNullOrEmpty(unsoBrCd) = False) Then

            Dim unsocoRow As DataRow = _
                MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select() _
                    .Where(Function(row) nrsBrCd.Equals(row.Item("NRS_BR_CD")) AndAlso _
                                         unsocoCd.Equals(row.Item("UNSOCO_CD")) AndAlso _
                                         unsoBrCd.Equals(row.Item("UNSOCO_BR_CD"))).FirstOrDefault()

            If (unsocoRow IsNot Nothing) Then
                If (LMConst.FLG.ON.Equals(unsocoRow.Item("AUTO_DENP_NO_FLG"))) Then
                    autoDenpKbn = TryCast(unsocoRow.Item("AUTO_DENP_NO_KBN"), String)
                End If
            End If

        End If

        Return autoDenpKbn

    End Function



    '(2012.08.13)要望番号1341 車載受注渡し対応 -- STRAT --
    ''' <summary>
    ''' 車載受注渡し更新情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <remarks></remarks>
    Private Sub SearchSasaiDataSet(ByVal frm As LMF010F, ByVal ds As DataSet, ByVal arr As ArrayList)

        '車載受注受渡しの情報を設定
        Dim syasaiDt As DataTable = ds.Tables(LMF010C.TABLE_NM_IN)
        Dim syasaiDr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim rowNo As Integer = 0

        With frm.sprUnsoUnkou.ActiveSheet

            For i As Integer = 0 To max

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                syasaiDr = syasaiDt.NewRow()
                'i'
                syasaiDr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
                syasaiDr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
                syasaiDt.Rows.Add(syasaiDr)

            Next

        End With

    End Sub
    '(2012.08.13)要望番号1341 車載受注渡し対応 --  END  --

    '(2013.01.16)要望番号1773 選択行計算処理 -- START --
    ''' <summary>
    ''' 選択されたスプレッド列の合計値を設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSumSelect(ByVal frm As LMF010F)

        Dim SumWt As Decimal = 0
        Dim SumUntin As Long = 0
        Dim SumShiharai As Long = 0
        Dim max As Integer = frm.sprUnsoUnkou.ActiveSheet.RowCount - 1

        'SHINODA ADD 2017/4/25 要望管理2696対応 Start
        If (LMConst.AuthoKBN.AGENT).Equals(LMUserInfoManager.GetAuthoLv()) = False Then
            With frm.sprUnsoUnkou.ActiveSheet

                For i As Integer = 0 To max
                    'チェックのある行のみ合計する
                    'i'If Me._LMFconG.GetCellValue(.Cells(i, LMF010G.sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then
                    'i'    SumWt = SumWt + Convert.ToDecimal(Me._LMFconG.GetCellValue(.Cells(i, LMF010G.sprUnsoUnkouDef.WT.ColNo)))                '重量
                    'i'    SumUntin = SumUntin + CLng(Me._LMFconG.GetCellValue(.Cells(i, LMF010G.sprUnsoUnkouDef.UNCHIN.ColNo)))                   '運賃
                    'i'    SumShiharai = SumShiharai + CLng(Me._LMFconG.GetCellValue(.Cells(i, LMF010G.sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo)))    '支払運賃
                    'i'End If
                    If Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.DEF.ColNo)).ToString.Equals("1") = True Then
                        SumWt = SumWt + Convert.ToDecimal(Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.WT.ColNo)))                '重量
                        SumUntin = SumUntin + CLng(Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.UNCHIN.ColNo)))                   '運賃
                        SumShiharai = SumShiharai + CLng(Me._LMFconG.GetCellValue(.Cells(i, _G.sprUnsoUnkouDef.SHIHARAI_UNCHIN.ColNo)))    '支払運賃
                    End If
                Next

            End With
        End If
        'SHINODA ADD 2017/4/25 要望管理2696対応 End

        '計算値を設定
        frm.numWt.Value = SumWt
        frm.numUntin.Value = SumUntin
        frm.numShiharaiUntin.Value = SumShiharai

    End Sub
    '(2013.01.16)要望番号1773 選択行計算処理 --  END  --

    '2014.07.01 追加START
    ''' <summary>
    ''' オカケンメイトCSV作成処理(運送データ用)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OkakenCsvMake(ByVal frm As LMF010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMF850DS
        Me._JikkouDs = New LMF010DS()
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMF010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMF010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMF010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty

        'システム日付の取得
        Dim sysdate As String() = MyBase.GetSystemDateTime()

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ", _
                                                                                       "KBN_CD = '01'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMFconV.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMFconV.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C005' AND ", _
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ", _
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("UNSO_NO_L") = Me._LMFconV.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = sysdate(0)
            dr("SYS_TIME") = sysdate(1)
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '検索時WSAクラス呼び出し
        prmDs = MyBase.CallWSA("LMF010BLF", "SelectOkakenCsv", Me._JikkouDs)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '岡山貨物CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMF850", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbPrintKb.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMFconH.EndAction(frm)

    End Sub
    '2014.07.01 追加END

    '2014.07.09 追加START
    ''' <summary>
    ''' 名鉄CSV作成処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MeitetuCsvMake(ByVal frm As LMF010F, ByVal arr As ArrayList, ByVal prm As LMFormData)

        Dim prmDs As DataSet = New LMF860DS
        Me._JikkouDs = New LMF010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMF010C.TABLE_NM_IN_CSV)
        Dim dr As DataRow = setDs.Tables(LMF010C.TABLE_NM_IN_CSV).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMF010C.TABLE_NM_IN_CSV)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty

        'システム日付の取得
        Dim sysdate As String() = MyBase.GetSystemDateTime()

        '保存先のCSVファイルのパス・ファイル名
        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C003' AND ", _
                                                                                       "KBN_CD = '00'"))
        Dim filePath As String = kbnDr(0).Item("KBN_NM1").ToString
        Dim fileName As String = kbnDr(0).Item("KBN_NM2").ToString

        For i As Integer = 0 To max

            '別インスタンスのデータロウを空にする
            inTbl.Clear()

            nrsBrCd = Me._LMFconV.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMFconV.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ", _
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ", _
                                                                                           "KBN_NM2 = '", unsoCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            dr("NRS_BR_CD") = nrsBrCd
            dr("UNSO_NO_L") = Me._LMFconV.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
            dr("ROW_NO") = Convert.ToInt32(arr(i))
            dr("FILEPATH") = filePath
            dr("FILENAME") = fileName
            dr("SYS_DATE") = sysdate(0)
            dr("SYS_TIME") = sysdate(1)
            inTbl.Rows.Add(dr)

            '持ちまわっているデータセットにつめなおす
            setDt.ImportRow(inTbl.Rows(0))

        Next

        If setDt.Rows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        prmDs = MyBase.CallWSA("LMF010BLF", "SelectMeitetuCsv", Me._JikkouDs)

        If MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E430")
            Exit Sub
        End If

        '名鉄CSV出力処理呼出
        prm.ParamDataSet = prmDs
        LMFormNavigate.NextFormNavigate(Me, "LMF860", prm)

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbPrintKb.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMFconH.EndAction(frm)

    End Sub
    '2014.07.09 追加END

    ''' <summary>
    ''' 名鉄帳票印刷(荷札,送状同時印刷)
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="arr"></param>
    ''' <param name="prm"></param>
    ''' <param name="isGrouping"></param>
    ''' <remarks></remarks>
    Private Sub MeitetuPrintData(ByVal frm As LMF010F, ByVal arr As ArrayList, ByVal prm As LMFormData, ByVal isGrouping As Boolean)
        Dim prmDs As DataSet = New LMC800DS
        Me._JikkouDs = New LMF010DS
        Dim setDs As DataSet = Me._JikkouDs.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMF010C.TABLE_NM_IN_UPDATE)
        Dim dr As DataRow = setDs.Tables(LMF010C.TABLE_NM_IN_UPDATE).NewRow()
        Dim setDt As DataTable = Me._JikkouDs.Tables(LMF010C.TABLE_NM_IN_UPDATE)
        Dim max As Integer = arr.Count - 1
        Dim kbnDr() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim unsoCd As String = String.Empty
        Dim shitenCd As String = String.Empty

        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprUnsoUnkou.ActiveSheet

        Dim targetRows As New ArrayList()

        For i As Integer = 0 To max
            nrsBrCd = Me._LMFconG.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))
            unsoCd = Me._LMFconG.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo))
            shitenCd = Me._LMFconG.GetCellValue(spr.Cells(Convert.ToInt32(arr(i)), _G.sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo))

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ", _
                                                                                           "KBN_NM1 = '", nrsBrCd, "' AND ", _
                                                                                           "KBN_NM2 = '", unsoCd, "' AND ", _
                                                                                            "KBN_NM3 = '", shitenCd, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない運送会社の場合はスルー
                Continue For
            End If

            ' 対象の運送会社のみ設定
            targetRows.Add(arr(i))

        Next

        If targetRows.Count = 0 Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E070")
            Exit Sub
        End If


        Me.SetDataSetInMeitetuPrintData(frm, Me._JikkouDs, targetRows)
        Me.SetDataSetInData_UPDATE(frm, Me._JikkouDs, targetRows)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '検索時WSAクラス呼び出し
        ' まとめなし
        prmDs = MyBase.CallWSA("LMF010BLF", "PrintMeitetuReport", Me._JikkouDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")
            'EXCEL起動()
            MyBase.MessageStoreDownload()

            Exit Sub
        ElseIf MyBase.IsMessageExist() = True Then
            '処理終了メッセージの表示
            MyBase.ShowMessage(frm, "E070")

            'EXCEL起動()
            MyBase.MessageStoreDownload()

            Exit Sub
        End If

        ''エラー帳票出力の判定
        'Call Me.ShowStorePrintData(frm)

        'プレビュー判定 
        Dim prevDt As DataTable = prmDs.Tables(LMConst.RD)
        If prevDt IsNot Nothing AndAlso prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        '処理終了メッセージの表示
        MyBase.ShowMessage(frm, "G002", New String() {frm.cmbTrapoPrint.SelectedText, String.Empty})

        '処理終了アクション
        Call Me._LMFconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 印刷時、利用する更新用INデータセット(名鉄用)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInMeitetuPrintData(ByVal frm As LMF010F, ByRef rtDs As DataSet, ByVal arr As ArrayList)

        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim rtDt As DataTable = rtDs.Tables(LMF010C.TABLE_NM_IN_PRINT)
        Dim row As DataRow = Nothing
        Dim spr As Win.Spread.LMSpread = frm.sprUnsoUnkou
        Dim rowNo As Integer = 0
        Dim flg As Boolean = True
        For i As Integer = 0 To arr.Count - 1

            With spr.ActiveSheet

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(arr(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                row = rtDt.NewRow()

                row.Item("USER_BR_CD") = userNrCd
                row.Item("NRS_BR_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo).Value()
                '運送番号を出荷管理番号に設定
                row.Item("OUTKA_NO_L") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo).Value()
                row.Item("OUTKA_STATE_KB") = ""
                row.Item("PICK_KB") = ""
                row.Item("OUTOKA_KANRYO_YN") = ""
                row.Item("OUTKA_KENPIN_YN") = ""
                row.Item("SYS_UPD_DATE") = .Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo).Value()
                row.Item("SYS_UPD_TIME") = .Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo).Value()
                row.Item("OUTKA_SASHIZU_PRT_YN") = ""
                row.Item("S_COUNT") = ""
                row.Item("PRINT_KB") = ""
                row.Item("ROW_NO") = rowNo
                row.Item("CUST_CD_L") = ""
                row.Item("CUST_CD_M") = ""
                row.Item("NIHUDA_YN") = ""
                row.Item("SASZ_USER") = ""
                row.Item("OUTKA_PKG_NB") = ""
                row.Item("TACHIAI_FLG") = ""
                row.Item("NHS_FLAG") = ""
                row.Item("OUTKA_PLAN_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Convert.ToString(.Cells(rowNo, _G.sprUnsoUnkouDef.OUTKA_PLAN_DATE.ColNo).Value())) '出荷予定日
                row.Item("ARR_PLAN_DATE") = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Convert.ToString(.Cells(rowNo, _G.sprUnsoUnkouDef.ARR_PLAN_DATE.ColNo).Value())) '納入予定日
                row.Item("UNSO_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo).Value()
                row.Item("UNSO_BR_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo).Value()
                row.Item("DEST_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.DEST_CD.ColNo).Value()
                row.Item("CUST_DEST_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.DEST_CD.ColNo).Value()
                row.Item("ROW_COUNT") = arr.Count

                rtDt.Rows.Add(row)

            End With

        Next


    End Sub


    ''' <summary>
    ''' 変更時、利用するデータセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_UPDATE(ByVal frm As LMF010F, ByRef rtDs As DataSet, ByVal arr As ArrayList)

        Dim max As Integer = arr.Count - 1
        'Dim userNrCd As String = LM.Base.LMUserInfoManager.GetNrsBrCd()
        Dim userNrCd As String = frm.cmbEigyo.SelectedValue.ToString()
        Dim row As DataRow = rtDs.Tables(LMF010C.TABLE_NM_IN_UPDATE).NewRow
        Dim rowNo As Integer = 0
        Dim rtDt As DataTable = rtDs.Tables(LMF010C.TABLE_NM_IN_UPDATE)
        Dim flg As Boolean = True
        ''Dim unsoCd As String = frm.txtTrnCD.TextValue
        ''Dim unsoCdBr As String = frm.txtTrnBrCD.TextValue

        Dim spr As Win.Spread.LMSpread = frm.sprUnsoUnkou

        With spr.ActiveSheet

            For i As Integer = 0 To max

                '変換ミスはサーバに渡さない
                flg = Integer.TryParse(arr(i).ToString(), rowNo)
                If flg = False Then
                    Continue For
                End If

                row = rtDt.NewRow()

                row.Item("NRS_BR_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo).Value()
                '運送番号を出荷管理番号に設定
                row.Item("OUTKA_NO_L") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo).Value()
                row.Item("UNSO_NO_L") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo).Value()
                row.Item("UNSOCO_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_CD_1.ColNo).Value()
                row.Item("UNSOCO_BR_CD") = .Cells(rowNo, _G.sprUnsoUnkouDef.UNSOCO_BR_CD_1.ColNo).Value()
                row.Item("SYS_UPD_DATE") = .Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo).Value()
                row.Item("SYS_UPD_TIME") = .Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo).Value()
                row.Item("UNSO_SYS_UPD_DATE") = .Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo).Value()
                row.Item("UNSO_SYS_UPD_TIME") = .Cells(rowNo, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo).Value()
                row.Item("RECORD_NO") = rowNo

                row.Item("WH_CD") = ""

                row.Item("AUTO_DENP_KBN") = ""
                row.Item("AUTO_DENP_NO") = ""

                rtDt.Rows.Add(row)

            Next

        End With

    End Sub

    '2017/02/27  名鉄対応 add end

    '2022.08.22 追加START
    ''' <summary>
    ''' 同じ運行情報に紐づく運送を全て選択する
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetChkSameUnsoLLData(ByVal frm As LMF010F, ByVal arr As ArrayList) As ArrayList

        Dim arrNew As ArrayList
        Dim rowIdx As Integer = 0
        Dim tripNo As String

        If arr.Count < 1 Then
            Return arr
        End If

        With frm.sprUnsoUnkou

            For selIdx As Integer = 0 To arr.Count - 1

                'Index取得
                If Integer.TryParse(arr(selIdx).ToString(), rowIdx) = False Then
                    Continue For
                End If

                '運行番号
                tripNo = Me._LMFconV.GetCellValue(.ActiveSheet.Cells(rowIdx, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))
                If String.IsNullOrEmpty(tripNo) Then
                    Continue For
                End If

                For idx As Integer = 0 To .ActiveSheet.RowCount - 1

                    '運行番号が一致する行を特定
                    If tripNo = Me._LMFconG.GetCellValue(.ActiveSheet.Cells(idx, _G.sprUnsoUnkouDef.TRIP_NO.ColNo)) Then
                        '行を選択
                        .SetCellValue(idx, _G.sprUnsoUnkouDef.DEF.ColNo, True)
                    End If

                Next

            Next

            '選択行（Index）取得
            arrNew = Me._LMFconG.GetCheckList(.ActiveSheet, _G.sprUnsoUnkouDef.DEF.ColNo)

        End With

        Return arrNew

    End Function
    '2022.08.22 追加END

    '2022.08.22 追加START
    ''' <summary>
    ''' データセット設定(LMZ360引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetLMZ360InData(ByVal frm As LMF010F, ByVal arr As ArrayList) As DataSet

        'API情報取得
        Dim apiInfo As DataRow = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
            .Select("KBN_GROUP_CD = 'U041' AND KBN_CD = '00'")(0)

        Dim apiUrl As String = apiInfo("KBN_NM1").ToString              'APIサーバーURL
        Dim apiKeyHeader As String = apiInfo("KBN_NM2").ToString        'APIキーヘッダ
        Dim apiKey As String = apiInfo("KBN_NM3").ToString              'APIキー

        Dim language As String 'ユーザー言語区分
        Select Case Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
            Case CStr(MessageType.MessageType_JP)
                '日本の場合
                language = "ja"
            Case Else
                language = "en"
        End Select

        'DataSet設定
        Dim ds As DataSet = New LMZ360DS()
        Dim dt As DataTable = ds.Tables(LMZ360C.TABLE_NM_IN)
        Dim dr As DataRow
        Dim rowIdx As Integer = 0

        With frm.sprUnsoUnkou.ActiveSheet
            For idx As Integer = 0 To arr.Count - 1

                'Index取得
                If Integer.TryParse(arr(idx).ToString(), rowIdx) = False Then
                    Continue For
                End If

                dr = dt.NewRow()

                dr.Item("API_SERVER_URL") = apiUrl              'APIサーバーURL
                dr.Item("API_KEY_HEADER") = apiKeyHeader        'APIキーヘッダ
                dr.Item("API_KEY") = apiKey                     'APIキー
                dr.Item("API_LANGUAGE") = language              'ユーザー言語区分

                dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()                                                               '営業所コード
                dr.Item("UNSO_NO_L") = Me._LMFconV.GetCellValue(.Cells(rowIdx, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))                 '運送番号L
                dr.Item("TRIP_NO") = Me._LMFconV.GetCellValue(.Cells(rowIdx, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))                     '運行番号
                dr.Item("GUI_SYS_UPD_DATE") = Me._LMFconV.GetCellValue(.Cells(rowIdx, _G.sprUnsoUnkouDef.SYS_UPD_DATE.ColNo))       '更新日付
                dr.Item("GUI_SYS_UPD_TIME") = Me._LMFconV.GetCellValue(.Cells(rowIdx, _G.sprUnsoUnkouDef.SYS_UPD_TIME.ColNo))       '更新時間

                dr.Item("KBN_LANG") = language                  'ユーザー言語区分

                dt.Rows.Add(dr)

            Next
        End With

        Return ds

    End Function
    '2022.08.22 追加END

    '2022.08.22 追加START
    ''' <summary>
    ''' 送信フラグ設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSoshinFlg(ByVal frm As LMF010F, ByVal ds As DataSet)

        '送信フラグ
        Dim soshinFlg As String = "01" '済
        Dim soshinFlgNm As String = Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                .Select($"KBN_GROUP_CD = 'U009' AND KBN_CD = '{soshinFlg}'") _
                (0).Item("KBN_NM8").ToString

        Dim max As Integer = frm.sprUnsoUnkou.ActiveSheet.RowCount - 1
        Dim sel_L_Tbl As DataTable = ds.Tables(LMZ360C.TABLE_NM_ABHB910_UNSO_L)

        With frm.sprUnsoUnkou
            For idx As Integer = 0 To max

                '運送番号Lが一致する行を特定
                Dim unsoNoL As String = Me._LMFconG.GetCellValue(.ActiveSheet.Cells(idx, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo))
                Dim rows As DataRow() = sel_L_Tbl.Select($"UNSO_NO_L = '{unsoNoL}'")
                If rows.Length = 0 Then
                    Continue For
                End If

                '送信フラグ設定
                .SetCellValue(idx, _G.sprUnsoUnkouDef.DEF.ColNo, True)
                .SetCellValue(idx, _G.sprUnsoUnkouDef.PF_SOSHIN.ColNo, soshinFlg)
                .SetCellValue(idx, _G.sprUnsoUnkouDef.PF_SOSHIN_NM.ColNo, soshinFlgNm)

            Next
        End With

    End Sub
    '2022.08.22 追加END

#End Region

#Region "別PG起動処理"

    ''' <summary>
    ''' 運送編集画面を起動
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="recType">レコードタイプ</param>
    ''' <remarks></remarks>
    Private Sub OpenUnsoEditGamen(ByVal ds As DataSet, ByVal recType As String)

        '画面起動
        Call Me._LMFconH.FormShow(ds, "LMF020", recType)

    End Sub

    ''' <summary>
    ''' 運行編集画面を起動
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="recType">レコードタイプ</param>
    ''' <remarks></remarks>
    Private Sub OpenUncoEditGamen(ByVal ds As DataSet, ByVal recType As String)

        '画面起動
        Call Me._LMFconH.FormShow(ds, "LMF030", recType)

    End Sub

#End Region

#Region "チェック"

    ''' <summary>
    ''' 一括変更帳票出力するチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>ArrayList</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSelectChk(ByVal frm As LMF010F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim shuseiKbn As String = frm.cmbShuSei.SelectedValue.ToString()

        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprUnsoUnkou.ActiveSheet
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(frm, spr, rowNo)

            '運送手配区分チェック
            rtnResult = rtnResult And Me.IsTehaiChk(frm, spr, rowNo)

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            '支払確定データチェック
            rtnResult = rtnResult AndAlso Me.IsClickDataCheck(spr, rowNo)
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            '登録の場合
            If frm.optEventY.Checked = True Then

                '中継済みチェック
                rtnResult = rtnResult And Me.IsTyuKeiChk(spr, shuseiKbn, rowNo)

                '運行番号設定済みチェック
                rtnResult = rtnResult And Me.IsTripNoChk(spr, rowNo)

            Else

                '運行番号未設定チェック
                rtnResult = rtnResult And Me.IsTripMiChk(spr, shuseiKbn, rowNo)

                '中継地未設定チェック
                rtnResult = rtnResult And Me.IsTyukeiMiChk(spr, shuseiKbn, rowNo)

            End If

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        Return Me._LMFconV.SetErrDt(dt, arr)

    End Function

    'START UMANO 2012.08.24 群馬対応
    ''' <summary>
    ''' 印刷帳票チェック(現状は運行指示書のみ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>ArrayList</returns>
    ''' <remarks></remarks>
    Private Function IsSprDataChk(ByVal frm As LMF010F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True

        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprUnsoUnkou.ActiveSheet
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            ''自営業チェック
            'rtnResult = Me.IsMyNrsChk(frm, spr, rowNo)

            '2014.07.01 修正START
            '運送データチェック
            If frm.cmbPrintKb.SelectedValue.Equals("07") = True OrElse
               frm.cmbPrintKb.SelectedValue.Equals("11") = True Then
                rtnResult = Me.IsUnsoDataChk(spr, rowNo)
#If True Then   'ADD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能
            ElseIf frm.cmbPrintKb.SelectedValue.Equals("14") = True Then
                '一括印刷時（元データ運送のみ）
                rtnResult = Me.IsMotoDataPrintChk(spr, rowNo)
#End If
#If True Then   'ADD 2022/01/24 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
            ElseIf frm.cmbPrintKb.SelectedValue.Equals("15") = True Then
                '運送保険申込時印刷時（元データ運送のみ）
                rtnResult = Me.IsMotoDataPrintChkUnsoHoken(spr, rowNo)

#End If
            ElseIf frm.cmbPrintKb.SelectedValue.Equals("16") = True Then
                '運送チェックリスト印刷時（元データ運送のみ）
                rtnResult = Me.IsMotoDataPrintChk(spr, rowNo, "運送チェックリスト印刷")

            ElseIf frm.cmbPrintKb.SelectedValue.Equals("17") = True Then
                '立合書（運送）印刷時（元データ運送のみ）
                rtnResult = Me.IsMotoDataPrintChk(spr, rowNo, "立合書（運送）印刷")

            Else
                '運行番号未設定チェック
                rtnResult = Me.IsTripMiPrintChk(spr, rowNo)
                End If
                '2014.07.01 修正END

                'エラーがある場合、DataTableに設定
                Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        Return Me._LMFconV.SetErrDt(dt, arr)

    End Function
    'END UMANO 2012.08.24 群馬対応

    ''' <summary>
    ''' 自営業チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMyNrsChk(ByVal frm As LMF010F, ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '自営業でない場合、エラー
        'i'If LMUserInfoManager.GetNrsBrCd().Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))) = False Then
        'i'    Return Me.SetIkkatuErrData("E178", New String() {Me._LMFconV.SetRepMsgData(frm.btnHenko.Text)}, rowNo)
        'i'End If

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.NRS_BR_CD.ColNo))) = False Then
        '    Return Me.SetIkkatuErrData("E178", New String() {Me._LMFconV.SetRepMsgData(frm.btnHenko.Text)}, rowNo)
        'End If

        Return True

    End Function

    ''' <summary>
    ''' 運送手配区分チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTehaiChk(ByVal frm As LMF010F, ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '日陸手配でない場合、エラー
        'i'If LMFControlC.TEHAI_NRS.Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.UNSO_TEHAI_KB.ColNo))) = False Then
        'i'    Return Me.SetIkkatuErrData("E336", New String() {LMFControlC.NRS_TEHAI, Me._LMFconV.SetRepMsgData(frm.btnHenko.Text)}, rowNo)
        'i'End If
        If LMFControlC.TEHAI_NRS.Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_TEHAI_KB.ColNo))) = False Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me.SetIkkatuErrData("E336", New String() {LMFControlC.NRS_TEHAI, Me._LMFconV.SetRepMsgData(frm.btnHenko.Text)}, rowNo)
            Return Me.SetIkkatuErrData("E884", New String() {Me._LMFconV.SetRepMsgData(frm.btnHenko.Text)}, rowNo)
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function

    ''' <summary>
    ''' 中継済みチェック
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="shuseiKbn">修正区分</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTyuKeiChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal shuseiKbn As String, ByVal rowNo As Integer) As Boolean

        '修正項目が運行番号でない場合、スルー
        If LMF010C.SHUSEI_TRIP.Equals(shuseiKbn) = False Then
            Return True
        End If

        'すでに中継配送済みの場合、エラー
        'i'If LMFControlC.FLG_ON.Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then
        'i'    Return Me.SetIkkatuErrData("E226", rowNo)
        'i'End If
        If LMFControlC.FLG_ON.Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TYUKEI_FLG.ColNo))) = True Then
            Return Me.SetIkkatuErrData("E226", rowNo)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 運行番号設定済みチェック
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTripNoChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '運行番号に値がある場合、エラー
        'i'If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO.ColNo))) = False Then
        'i'    Return Me.SetIkkatuErrData("E230", rowNo)
        'i'End If
        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))) = False Then
            Return Me.SetIkkatuErrData("E230", rowNo)
        End If

        Return True

    End Function

    '2012.08.23 群馬対応 追加START
    ''' <summary>
    ''' 運行番号未設定チェック(運行指示書印刷時)
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTripMiPrintChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '運行番号に値がない場合、エラー
        'i'If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.TRIP_NO.ColNo))) = True Then
        'i'    Return Me.SetIkkatuErrData("E320", New String() {LMF010C.TRIP_MI, "印刷"}, rowNo)
        'i'End If
        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))) = True Then
            Return Me.SetIkkatuErrData("E320", New String() {LMF010C.TRIP_MI, "印刷"}, rowNo)
        End If

        Return True

    End Function
    '2012.08.23 群馬対応 追加END

#If True Then   'ADD 2018/11/20 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能
    ''' <summary>
    ''' 元データ区分チェック(運送のみ)
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="title">処理名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMotoDataPrintChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer, Optional ByVal title As String = "一括印刷") As Boolean

        '元データ区分が運送以外は、エラー([%1]の為、[%2]できません。)
        ' "40"じゃなく"運送" なんのため？？

        If ("運送").Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo))) = False Then
            Return Me.SetIkkatuErrData("E320", New String() {"元データが運送以外", title}, rowNo)
        End If

        Return True

    End Function

#End If

#If True Then   'ADD 2022/01/24 026832 【LMS】運送保険料システム化_実装_運送保険申込書対応_運行・運送情報
    ''' <summary>
    ''' 元データ区分チェック(運送のみ)
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMotoDataPrintChkUnsoHoken(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '元データ区分が運送以外は、エラー([%1]の為、[%2]できません。)

        If ("運送").Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo))) = False Then
            Return Me.SetIkkatuErrData("E320", New String() {"元データが運送以外", "運送保険申込書印刷"}, rowNo)
        End If

        Return True

    End Function
#End If

    '2014.07.01 追加START
    ''' <summary>
    ''' 運送データチェック(オカケンメイトCSV作成実行時)　※元データ区分が運送(MOTO_DATA_KB = "40")
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsoDataChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '元データ区分が運送(MOTO_DATA_KB = "40")でない場合はエラー
        If Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.MOTO_DATA_KB.ColNo)).Equals(LMF010C.MOTO_DATA_KB_UNSO) = False Then
            Return Me.SetIkkatuErrData("E428", New String() {"入荷または出荷で作成されている", "出力", String.Concat("運送番号：", Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.UNSO_NO_L.ColNo)))}, rowNo)
        End If

        Return True

    End Function
    '2014.07.01 追加END

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 選択行データチェック
    ''' </summary>
    ''' <param name="rowNo"></param>''' 
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsClickDataCheck(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer) As Boolean

        '支払運賃が確定済の場合はエラー
        'i'If LMFControlC.FLG_ON.Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo))) = True Then
        'i'    Return Me.SetIkkatuErrData("E497", New String() {"編集できません。"}, rowNo)
        'i'End If
        If LMFControlC.FLG_ON.Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.SHIHARAI_FIXED_FLAG.ColNo))) = True Then
            Return Me.SetIkkatuErrData("E497", New String() {"編集できません。"}, rowNo)
        End If

        Return True

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 運行番号未設定チェック
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="shuseiKbn">修正区分</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTripMiChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal shuseiKbn As String, ByVal rowNo As Integer) As Boolean

        '運行番号でない場合、スルー
        If LMF010C.SHUSEI_TRIP.Equals(shuseiKbn) = False Then
            Return True
        End If

        '全ての運行番号に値がない場合、エラー
        'i'
        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))) = True _
           AndAlso String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO_SHUKA.ColNo))) = True _
           AndAlso String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO_CHUKEI.ColNo))) = True _
           AndAlso String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.TRIP_NO_HAIKA.ColNo))) = True _
           Then
            Return Me.SetIkkatuErrData("E237", New String() {LMF010C.TRIP_MI}, rowNo)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 中継地未設定チェック
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="shuseiKbn">修正区分</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTyukeiMiChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal shuseiKbn As String, ByVal rowNo As Integer) As Boolean

        '中継配送でない場合、スルー
        If LMF010C.SHUSEI_CHUKEI.Equals(shuseiKbn) = False Then
            Return True
        End If

        '集荷中継地に値がない場合、エラー
        'i'If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF010G.sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo))) = True Then
        'i'    Return Me.SetIkkatuErrData("E237", New String() {LMF010C.TYUKEI_MI}, rowNo)
        'i'End If
        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(spr.Cells(rowNo, _G.sprUnsoUnkouDef.SHUKA_RELY_POINT.ColNo))) = True Then
            Return Me.SetIkkatuErrData("E237", New String() {LMF010C.TYUKEI_MI}, rowNo)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 一括更新時のメッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetIkkatuErrData(ByVal id As String, ByVal rowNo As Integer) As Boolean

        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, id, , rowNo.ToString())
        Return False

    End Function

    ''' <summary>
    ''' 一括更新時のメッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetIkkatuErrData(ByVal id As String, ByVal msg As String(), ByVal rowNo As Integer) As Boolean

        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, id, msg, rowNo.ToString())
        Return False

    End Function

    ''' <summary>
    ''' 運行データキャンセル可否チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTripStatusData(ByVal frm As LMF010F, ByVal arr As ArrayList, Optional ByVal tripNo As String = Nothing) As Boolean

        'パラメータ設定
        Dim ds As DataSet = New LMF010DS()
        Dim dt As DataTable = ds.Tables(LMF010C.TABLE_NM_ITEM)
        Dim dr As DataRow = Nothing
        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0

        If tripNo Is Nothing Then

            For i As Integer = 0 To max

                dr = dt.NewRow()
                recNo = Convert.ToInt32(arr(i))
                dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(frm.sprUnsoUnkou.ActiveSheet.Cells(recNo, _G.sprUnsoUnkouDef.TRIP_NO.ColNo))
                dt.Rows.Add(dr)

            Next

        Else

            dr = dt.NewRow()
            dr.Item("TRIP_NO") = tripNo
            dt.Rows.Add(dr)

        End If



        'エラーがある場合、終了
        Dim rtnDs As DataSet = Nothing
        If Me.ActionData(frm, ds, LMF010C.ACTION_ID_CANCEL_DATA, rtnDs) = False Then
            Return False
        End If

        Return True

    End Function

#End Region

#End Region

#Region "イベント振分け"

    '(2013.01.17)要望番号1617 -- START --
    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '出荷編集
        Call Me.EditOutkaData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '(2013.01.17)要望番号1617 --  END  --

    'START YANAI 要望番号1241 運送検索：運送複写機能を追加する
    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '運送複写
        Call Me.CopyUnsoData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'END YANAI 要望番号1241 運送検索：運送複写機能を追加する

    '2022.08.22 追加START
    ''' <summary>
    ''' F4押下時処理呼び出し(データ送信)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'データ送信
        Call Me.DataSend(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '2022.08.22 追加END

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.NewTripData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EditTripData(frm, LMF010C.ActionType.UNCO_EDIT)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.NewUnsoData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 --- STRAT ---
    ''' <summary>
    ''' F8押下時処理呼び出し(車載受注渡し処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '車載受注渡し処理
        Call Me.SyasaiListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '＃(2012.08.13) 要望番号：1341 車載受注渡し対応 ---  END  ---

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '検索処理
        Call Me.SelectListData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey12Press(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByVal frm As LMF010F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '選択処理
        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' イベントオプションボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub optEvent_CheckedChanged(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "optEvent_CheckedChanged")

        Call Me.EventOptionClick(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "optEvent_CheckedChanged")

    End Sub

    ''' <summary>
    ''' 修正項目変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbShuSei_SelectedValueChanged(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.ShuseiLockControl(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMF010F_KeyDown(ByVal frm As LMF010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnHenko_Click(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveUnsoLItemData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' 運行編集ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnUnkoEdit_Click(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EditTripData(frm, LMF010C.ActionType.BTN_UNCO_EDIT)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '2012.06.22 要望番号1189 追加START
    ''' <summary>
    ''' 印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'Call Me.UnsoPrintData(frm)
        If frm.cmbPrintKb.SelectedItem.Text = "配車表(群馬)" Then
            '配車表(群馬)
            Call Me.UnsoPrintData_581(frm)
        Else
            '配車表(群馬)以外
            Call Me.UnsoPrintData(frm)
        End If

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '2012.06.22 要望番号1189 追加END


    '2017/02227 追加 START
    ''' <summary>
    ''' 運送会社帳票印刷 印刷ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnTrapoPrint_Click(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.TrapoPrintData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    '2017/0227 追加 END

    '(2013.01.17)要望番号1774 -- START --
    ''' <summary>
    ''' SPREADクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub chkcell_CheckChanged(ByVal frm As LMF010F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "CellClick")

        '選択行の重量･運賃･支払運賃合算表示処理
        Call Me.SetSumSelect(frm)

        '(2013.03.12)要望番号1857 ハイライト処理追加 -- START --
        Me._G.SetSpreadColor()
        '(2013.03.12)要望番号1857 ハイライト処理追加 --  END  --

        MyBase.Logger.EndLog(Me.GetType.Name, "CellClick")

    End Sub
    '(2013.01.17)要望番号1774 -- START --

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class