' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF040H : 運賃検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF040ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMF040H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMF040V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF040G

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
    ''' システム日付
    ''' </summary>
    ''' <remarks></remarks>
    Private _NowDate As String = MyBase.GetSystemDateTime(0)

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    'START YANAI 要望番号561
    ''' <summary>
    ''' 検索結果のデータセットを保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _KensakuDs As DataSet
    'END YANAI 要望番号561

    '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START
    ''' <summary>
    ''' アクサルタ同送処理フラグ
    ''' </summary>
    ''' <remarks>現在のデータがアクサルタ同送用で検索されたものであればTrue そうでなければFalseが設定される</remarks>
    Private _IsAxaltaDousouProcess As Boolean
    '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START

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
        Dim frm As LMF040F = New LMF040F(Me)

        'Gamen共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMFconG = New LMFControlG(sForm)

        'Validate共通クラスの設定
        Me._LMFconV = New LMFControlV(Me, sForm, Me._LMFconG)

        'Hnadler共通クラスの設定
        Me._LMFconH = New LMFControlH(sForm, MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMF040V(Me, frm, Me._LMFconV, Me._LMFconG)

        'Gamenクラスの設定
        Me._G = New LMF040G(Me, frm, Me._LMFconG)

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

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        Call Me._G.SetInitValue()

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

    'START YANAI 要望番号582
    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub PrintAciton(ByVal frm As LMF040F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.PRINT)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'inputDataSet作成
        Dim prm As LMFormData = New LMFormData()
        Dim prmDs As DataSet = Me.SetDataSetLMG060InData(frm)
        prm.ParamDataSet = prmDs

        '画面遷移
        LMFormNavigate.NextFormNavigate(Me, "LMG060", prm)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    'END YANAI 要望番号582

    'START YANAI 要望番号561
    ''' <summary>
    ''' 連続入力
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub LoopEditAciton(ByVal frm As LMF040F)


        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.LOOPEDIT)

        '連続入力チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        'スプレッドの選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsLoopEditChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()
        arr = Me.IsLoopEditChk(frm, ds, arr)

        'Nothingの場合、ワーニングで「いいえ」選択
        If arr Is Nothing = True Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        If rtnResult = True Then

            Dim max As Integer = arr.Count - 1
            Dim rowNo As Integer = 0
            Dim unsoNo As String = String.Empty
            Dim unsoNoM As String = String.Empty
            Dim rtnDs As DataSet = Nothing
            Dim prmDs As DataSet = Nothing
            Dim prmDt As DataTable = Nothing
            Dim dr() As DataRow = Nothing
            'パラメータクラス生成
            Dim prm As LMFormData = New LMFormData()

            ds = Nothing
            prmDs = New LMF050DS()
            prmDt = prmDs.Tables(LMF050C.TABLE_NM_IN)

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                '送信するデータセットに検索条件を設定
                ds = Me.SetUnchinPkData(frm, rowNo)
                unsoNo = ds.Tables(LMF040C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString()
                unsoNoM = ds.Tables(LMF040C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_M").ToString()

                ''==========================
                ''WSAクラス呼出
                ''==========================

                ''強制実行フラグの設定
                'MyBase.SetForceOparation(False)

                ''閾値の設定(0より大きければ何でもよい)
                'MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

                'rtnDs = MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMControlC.BLF), LMF010C.ACTION_ID_SELECT, ds)

                ''エラー判定
                'If MyBase.IsMessageExist() = True Then

                '    'メッセージ設定
                '    Call Me._LMFconV.SetMstErrMessage("運賃テーブル", Me._LMFconG.EditConcatData(unsoNo, unsoNoM))

                '    '処理終了アクション
                '    Call Me.EndAction(frm)
                '    Exit Sub

                'End If

                'パラメータ設定
                With frm.sprDetail.ActiveSheet
                    'START YANAI 要望番号643
                    'dr = Me._KensakuDs.Tables(LMF040C.TABLE_NM_OUT).Select(String.Concat("UNSO_NO_L = '", Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO.ColNo)).ToString, "' AND ", _
                    '                                                                     "INOUTKA_NO_L = '", Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.KANRI_NO.ColNo)).ToString, "'"))
                    dr = Me._KensakuDs.Tables(LMF040C.TABLE_NM_OUT).Select(String.Concat("UNSO_NO_L = '", Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO.ColNo)).ToString, "' AND ", _
                                                                                         "UNSO_NO_M = '", Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO_EDA.ColNo)).ToString, "' AND ", _
                                                                                         "INOUTKA_NO_L = '", Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.KANRI_NO.ColNo)).ToString, "'"))
                    'END YANAI 要望番号643
                End With
                If 0 < dr.Length Then
                    prmDt.ImportRow(dr(0))
                    prmDt.Rows(0).Item("RENZOKU_FLG") = "01"
                End If

            Next

            Call Me.OpenUnchinEditGamen(prmDs, RecordStatus.NOMAL_REC)

        Else

            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub
    'END YANAI 要望番号561

    ''' <summary>
    ''' 確定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FixDataAciton(ByVal frm As LMF040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.FIX)

        '確定チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsFixChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()
        arr = Me.IsFixSelectChk(frm, ds, arr)

        'Nothingの場合、ワーニングで「いいえ」選択
        If arr Is Nothing = True Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'START KIM 2012/11/21 要望番号：1400
        If rtnResult = False Then
            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If

            '処理終了アクション
            Call Me.EndAction(frm)

        End If
        'END   KIM 2012/11/21 要望番号：1400

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr, LMF040C.ActionType.FIX)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMF040C.ActionType.FIX)

        '一括更新終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F5ButtonName))

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 確定解除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub FixCancellDataAciton(ByVal frm As LMF040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.FIX_CANCELL)

        '確定チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsFixCancellChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()
        arr = Me.IsFixCancellSelectChk(frm, ds, arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'START KIM 2012/11/21 要望番号：1400
        If rtnResult = False Then
            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If

            '処理終了アクション
            Call Me.EndAction(frm)

        End If
        'END   KIM 2012/11/21 要望番号：1400

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr, LMF040C.ActionType.FIX_CANCELL)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMF040C.ActionType.FIX_CANCELL)

        '一括更新終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F6ButtonName))

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' まとめ指示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub GroupingDataAction(ByVal frm As LMF040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.GROUP)

        'まとめ指示チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsGroupChk(arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()

        'START KIM 2012/11/21 要望番号：1400
        arr = Me.IsGroupSelectChk(frm, ds, arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        If rtnResult = False Then
            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If

            '処理終了アクション
            Call Me.EndAction(frm)

        End If
        'END   KIM 2012/11/21 要望番号：1400

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr, LMF040C.ActionType.GROUP)

        'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
        'データセットに対して入力チェック
        rtnResult = rtnResult AndAlso Me.CheckGroupDataSet(frm, ds)
        'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMF040C.ActionType.GROUP)

        '更新終了アクション
        If rtnResult = True Then
            Me._LMFconH.SetMessageG002(frm, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F7ButtonName), String.Empty)
        End If

        'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
        'メッセージコードの判定
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.FunctionKey.F7ButtonName))
        'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' まとめ解除
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub GroupingCancellDataAction(ByVal frm As LMF040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.GROUP_CANCELL)

        'まとめ解除チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsGroupCancellChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()
        arr = Me.IsGroupCancellSelectChk(frm, ds, arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr, LMF040C.ActionType.GROUP_CANCELL)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMF040C.ActionType.GROUP_CANCELL)

        '更新成功の場合、メッセージ表示
        If rtnResult = True Then
            MyBase.ShowMessage(frm, "G040")
        Else

            'START YANAI 要望番号1392 まとめ解除時のメッセージが変
            ''EXCEL起動 
            'MyBase.MessageStoreDownload(True)
            'MyBase.ShowMessage(frm, "E235")
            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If
            'END YANAI 要望番号1392 まとめ解除時のメッセージが変

        End If

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMF040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.KENSAKU)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsInputCheck()

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
        Dim mc As Integer = Convert.ToInt32(Convert.ToDouble( _
                             Me.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'") _
                             (0).Item("VALUE1").ToString))

        MyBase.SetMaxResultCount(mc)


        '検索条件の設定
        Dim ds As DataSet = Me.SetConditionDataSet(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)
        'DBリードオンリー対応
        'Dim rtnDs As DataSet = MyBase.CallWSA(blf, Me.GetSelectActionId(frm), ds)
        Dim rtnDs As DataSet = MyBase.CallWSA(blf, Me.GetSelectActionId(frm), ds, True)


        '通常検索の場合
        Dim count As String = String.Empty
        If String.IsNullOrEmpty(frm.cmbGroup.SelectedValue.ToString()) = True Then
            count = MyBase.GetResultCount.ToString()
        Else
            count = rtnDs.Tables(LMF040C.TABLE_NM_OUT).Rows.Count.ToString()
        End If

        '検索処理
        rtnDs = Me.SelectListData(frm, ds, rtnDs, blf, count)
        If rtnDs Is Nothing = True Then
            Exit Sub
        End If

        'START YANAI 要望番号561
        '検索結果を保持
        Me._KensakuDs = rtnDs.Copy
        'END YANAI 要望番号561

        '値の再設定
        If Me._G.SetSpread(rtnDs) = False Then
            Me._LMFconV.SetErrMessage("E117", New String() {frm.lblTitleSokei.Text, LMFControlC.MAX_KETA})
        End If

        '(2012.11.08)要望番号1577 -- START --
        Dim dt As DataTable = rtnDs.Tables(LMF040C.TABLE_NM_OUT)

        'Spread(フォント色指定)
        Call Me._G.SetSpreadColor(dt)
        '(2012.11.08)要望番号1577 --  END  --

        '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 START
        '纏め候補条件がアクサルタ同送用なら_IsAxaltaDousouProcessにTrueを、
        'そうでなければFlaseを設定する
        _IsAxaltaDousouProcess = frm.cmbGroup.SelectedValue.ToString = LMF040C.ORDER_BY_AXALTA
        '2017/10/10 Annen アクサルタ 運賃按分計算の自動化対応 END

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMF040F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.MASTEROPEN)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF040C.ActionType.MASTEROPEN)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.ShowPopupControl(frm, objNm, LMF040C.ActionType.MASTEROPEN)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Enterキー判定
        Dim eventFlg As Boolean = (e.KeyCode = Keys.Enter)
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        rtnResult = rtnResult AndAlso Me._V.IsAuthority(LMF040C.ActionType.ENTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMF040C.ActionType.ENTER)

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
        Call Me.ShowPopupControl(frm, objNm, LMF040C.ActionType.ENTER)

        '処理終了アクション
        Call Me.EndAction(frm)

        'フォーカス移動処理
        Call Me._LMFconH.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SaveUnchinItemData(ByVal frm As LMF040F)

        '処理開始アクション
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.SAVE)

        '一括変更チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        rtnResult = rtnResult AndAlso Me._V.IsSaveChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Sub

        End If

        '選択レコードのチェック
        Dim ds As DataSet = New LMF040DS()
        arr = Me.IsSaveSelectChk(frm, ds, arr)

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        'START KIM 2012/11/21 要望番号：1400
        If rtnResult = False Then
            If MyBase.IsMessageStoreExist() = True Then
                'EXCEL起動 
                MyBase.MessageStoreDownload(True)
                MyBase.ShowMessage(frm, "E235")
            End If

            '処理終了アクション
            Call Me.EndAction(frm)

        End If
        'END   KIM 2012/11/21 要望番号：1400

        'データセット設定
        rtnResult = rtnResult AndAlso Me.SetSaveDataSet(frm, ds, arr, LMF040C.ActionType.SAVE)

        '保存処理
        rtnResult = rtnResult AndAlso Me.UnchinSaveData(frm, ds, LMF040C.ActionType.SAVE)

        '一括更新終了アクション
        Me._LMFconH.IkkatuEndAction(frm, rtnResult, Me._LMFconV.SetRepMsgData(frm.btnHenko.Text))

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        'START YANAI 要望番号1287 タリフ変更を行い、再検索せずに再計算ボタンを押下すると、タリフ変更前のタリフコードで計算してしまう
        ''一覧の更新
        'Me._G.SetUpdSpread(frm, arr)
        '一覧の更新
        rtnResult = rtnResult AndAlso Me._G.SetUpdSpread(frm, arr)
        'END YANAI 要望番号1287 タリフ変更を行い、再検索せずに再計算ボタンを押下すると、タリフ変更前のタリフコードで計算してしまう
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

        '処理終了アクション
        Call Me.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMF040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Dim rowNo As Integer = e.Row

        If rowNo > 0 Then

            '処理開始アクション
            Call Me.StartAction(frm)

            If Me._V.IsAuthority(LMF040C.ActionType.DOUBLECLICK) = False Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            '送信するデータセットに検索条件を設定
            Dim ds As DataSet = Me.SetUnchinPkData(frm, rowNo)
            Dim unsoNo As String = ds.Tables(LMF040C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_L").ToString()
            Dim unsoNoM As String = ds.Tables(LMF040C.TABLE_NM_IN).Rows(0).Item("UNSO_NO_M").ToString()

            '==========================
            'WSAクラス呼出
            '==========================

            '強制実行フラグの設定
            MyBase.SetForceOparation(False)

            '閾値の設定(0より大きければ何でもよい)
            MyBase.SetLimitCount(Me._LMFconG.GetLimitData())

            'DBリードオンリー対応
            'Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMControlC.BLF), LMF010C.ACTION_ID_SELECT, ds)
            Dim rtnDs As DataSet = MyBase.CallWSA(String.Concat(MyBase.GetPGID(), LMControlC.BLF), LMF010C.ACTION_ID_SELECT, ds, True)

            'エラー判定
            If MyBase.IsMessageExist() = True Then

                'メッセージ設定
                '2016.01.06 UMANO 英語化対応START
                'Call Me._LMFconV.SetMstErrMessage("運賃テーブル", Me._LMFconG.EditConcatData(unsoNo, unsoNoM))
                Call Me._LMFconV.SetMstErrMessage("運賃テーブル(Freight Table)", Me._LMFconG.EditConcatData(unsoNo, unsoNoM))
                '2016.01.06 UMANO 英語化対応END

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Sub

            End If

            'パラメータ設定
            Dim prmDs As DataSet = New LMF050DS()
            Dim prmDt As DataTable = prmDs.Tables(LMF050C.TABLE_NM_IN)
            prmDt.ImportRow(rtnDs.Tables(LMF040C.TABLE_NM_OUT).Rows(0))

            Call Me.OpenUnchinEditGamen(prmDs, RecordStatus.NOMAL_REC)

            '処理終了アクション
            Call Me.EndAction(frm)

        End If

    End Sub

    ''' <summary>
    ''' まとめ候補条件 変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub GroupChangeAction(ByVal frm As LMF040F)

        '検索キー変更
        Call Me._G.SelectGroupOpt()

    End Sub

    ''' <summary>
    ''' 変更対象項目 変更イベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub HenkoChangeAction(ByVal frm As LMF040F)

        'ロック制御
        Call Me._G.LockHenkoChangeControl()

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
    Private Function ShowPopupControl(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As Boolean

        With frm
            'SHINOHARA 要望番号513 カッコ内actionTypeを追加
            '処理開始アクション
            Call Me.StartAction(frm)

            Select Case objNm

                Case .txtTariffCd.Name

                    Call Me.SetReturnTariffPop(frm, objNm, actionType)

                Case .txtExtcCd.Name

                    Call Me.SetReturnExtcPop(frm, actionType)

                Case .txtDriverCd.Name

                    Call Me.SetReturnDriverPop(frm, actionType)

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Call Me.SetReturnCustPop(frm, objNm, actionType)

                Case .txtShuseiL.Name, .txtShuseiM.Name, .txtShuseiS.Name, .txtShuseiSS.Name

                    Call Me.SetShuseiReturnPop(frm, objNm, actionType)

                    'START KURIHARA 要望番号:928
                Case .txtDestCd.Name

                    Call Me.SetReturnDestPop(frm, actionType)
                    'END KURIHARA 要望番号:928

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="tariffKbn">タリフ分類</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnTariffPop(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType, Optional ByVal tariffKbn As String = "") As Boolean

        Dim prm As LMFormData = Nothing
        Dim tblNm As String = String.Empty
        Dim code As String = String.Empty
        Dim name As String = String.Empty

        With frm

            If String.IsNullOrEmpty(tariffKbn) Then

                tariffKbn = .cmbTariffKbn.SelectedValue.ToString()

            End If

            If LMFControlC.TARIFF_YOKO.Equals(tariffKbn) = True Then

                '横持ちタリフPop
                prm = Me.ShowYokoTariffPopup(frm, objNm, actionType)
                tblNm = LMZ100C.TABLE_NM_OUT
                code = "YOKO_TARIFF_CD"
                name = "YOKO_REM"

            Else

                '運賃タリフPop
                prm = Me.ShowUnchinTariffPopup(frm, objNm, actionType)
                tblNm = LMZ230C.TABLE_NM_OUT
                code = "UNCHIN_TARIFF_CD"
                name = "UNCHIN_TARIFF_REM"

            End If

            If prm.ReturnFlg = True Then

                Dim dr As DataRow = prm.ParamDataSet.Tables(tblNm).Rows(0)

                Select Case objNm

                    Case .txtTariffCd.Name

                        .txtTariffCd.TextValue = dr.Item(code).ToString()
                        .lblTariffNm.TextValue = dr.Item(name).ToString()

                    Case Else

                        .txtShuseiL.TextValue = dr.Item(code).ToString()

                End Select

                Return True

            End If

        End With

        Return False

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowUnchinTariffPopup(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ230DS()
        Dim dt As DataTable = ds.Tables(LMZ230C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        Dim brCd As String = String.Empty
        Dim tariff As String = String.Empty
        Dim csFlg As String = String.Empty

        Select Case objNm

            Case frm.txtTariffCd.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                tariff = frm.txtTariffCd.TextValue
                csFlg = LMConst.FLG.ON

            Case Else

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'brCd = LMUserInfoManager.GetNrsBrCd()
                brCd = frm.cmbEigyo.SelectedValue.ToString()
                tariff = frm.txtShuseiL.TextValue
                csFlg = LMConst.FLG.OFF

        End Select

        With dr
            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If actionType = LMF040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("UNCHIN_TARIFF_CD") = tariff
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("STR_DATE") = Me._NowDate
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ230", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowYokoTariffPopup(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ100DS()
        Dim dt As DataTable = ds.Tables(LMZ100C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        Dim brCd As String = String.Empty
        Dim yoko As String = String.Empty
        Dim csFlg As String = String.Empty

        Select Case objNm

            Case frm.txtTariffCd.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                yoko = frm.txtTariffCd.TextValue
                csFlg = LMConst.FLG.ON

            Case Else

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'brCd = LMUserInfoManager.GetNrsBrCd()
                brCd = frm.cmbEigyo.SelectedValue.ToString()
                yoko = frm.txtShuseiL.TextValue
                csFlg = LMConst.FLG.OFF

        End Select

        With dr
            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If actionType = LMF040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("YOKO_TARIFF_CD") = yoko
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csFlg
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ100", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 割増タリフPopの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtExtcCd.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()
                .lblExtcNm.TextValue = dr.Item("EXTC_TARIFF_REM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    'START YANAI 要望番号996
    ''' <summary>
    ''' 割増タリフPopの戻り値を設定(一括変更部)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnExtcPop2(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowExtcPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ240C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShuseiL.TextValue = dr.Item("EXTC_TARIFF_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function
    'END YANAI 要望番号996

    ''' <summary>
    ''' 割増タリフマスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowExtcPopup(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ240DS()
        Dim dt As DataTable = ds.Tables(LMZ240C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            'START SHINOHARA 要望番号513
            If actionType = LMF040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("EXTC_TARIFF_CD") = frm.txtExtcCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ240", "", Me._PopupSkipFlg)

    End Function

    'START s.kobayashi 要望番号2816
    ''' <summary>
    ''' 距離Popの戻り値を設定(一括変更部)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnKyoriPop(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowKyoriPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ080C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShuseiL.TextValue = dr.Item("KYORI_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 距離マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowKyoriPopup(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ080DS()
        Dim dt As DataTable = ds.Tables(LMZ080C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            If actionType = LMF040C.ActionType.ENTER Then
                .Item("KYORI_CD") = frm.txtShuseiL.TextValue
            End If
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ080", "", Me._PopupSkipFlg)

    End Function
    'End s.kobayashi 要望番号2816

    ''' <summary>
    ''' 乗務員Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    '''  <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnDriverPop(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowDriverPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ160C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtDriverCd.TextValue = dr.Item("DRIVER_CD").ToString()
                .lblDriverNm.TextValue = dr.Item("DRIVER_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 乗務員マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDriverPopup(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ160DS()
        Dim dt As DataTable = ds.Tables(LMZ160C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            'START SHINOHARA 要望番号513
            If actionType = LMF040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("DRIVER_CD") = frm.txtDriverCd.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ160", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 荷主Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnCustPop(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowCustPopup(frm, objNm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)

            With frm

                Select Case objNm

                    Case .txtCustCdL.Name, .txtCustCdM.Name

                        .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                        .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                        .lblCustNm.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString(), dr.Item("CUST_NM_M").ToString())

                    Case Else

                        .txtShuseiL.TextValue = dr.Item("CUST_CD_L").ToString()
                        .txtShuseiM.TextValue = dr.Item("CUST_CD_M").ToString()
                        .txtShuseiS.TextValue = dr.Item("CUST_CD_S").ToString()
                        .txtShuseiSS.TextValue = dr.Item("CUST_CD_SS").ToString()

                End Select

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim brCd As String = String.Empty
        Dim custL As String = String.Empty
        Dim custM As String = String.Empty
        Dim custS As String = String.Empty
        Dim custSS As String = String.Empty
        Dim csCd As String = String.Empty

        Select Case objNm

            Case frm.txtCustCdL.Name, frm.txtCustCdM.Name

                brCd = frm.cmbEigyo.SelectedValue.ToString()
                custL = frm.txtCustCdL.TextValue
                custM = frm.txtCustCdM.TextValue
                csCd = LMConst.FLG.ON

            Case Else

                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'brCd = LMUserInfoManager.GetNrsBrCd()
                brCd = frm.cmbEigyo.SelectedValue.ToString()
                custL = frm.txtShuseiL.TextValue
                custM = frm.txtShuseiM.TextValue
                custS = frm.txtShuseiS.TextValue
                custSS = frm.txtShuseiSS.TextValue
                csCd = LMConst.FLG.OFF

        End Select

        With dr

            .Item("NRS_BR_CD") = brCd
            'START SHINOHARA 要望番号513
            If actionType = LMF040C.ActionType.ENTER Then
                'END SHINOHARA 要望番号513
                .Item("CUST_CD_L") = custL
                .Item("CUST_CD_M") = custM
                .Item("CUST_CD_S") = custS
                .Item("CUST_CD_SS") = custSS
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("SEARCH_CS_FLG") = csCd
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)

        End With

        '行追加
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ260", "", Me._PopupSkipFlg)

    End Function

    ''' <summary>
    ''' 請求先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetReturnSeiqtoPop(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowSeiqtoPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtShuseiL.TextValue = dr.Item("SEIQTO_CD").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 請求先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowSeiqtoPopup(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ220DS()
        Dim dt As DataTable = ds.Tables(LMZ220C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString()
            'START SHINOHARA 要望番号513
            If actionType = LMF040C.ActionType.ENTER Then
                .Item("SEIQTO_CD") = frm.txtShuseiL.TextValue
                'START SHINOHARA 要望番号513
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ220", "", Me._PopupSkipFlg)

    End Function

    'START KURIHARA 要望番号:928
    ''' <summary>
    ''' 届先Popの戻り値を設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    '''  <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetReturnDestPop(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim prm As LMFormData = Me.ShowDestPopup(frm, actionType)
        If prm.ReturnFlg = True Then
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)

            With frm

                .txtDestCd.TextValue = dr.Item("DEST_CD").ToString()
                .lblDestNm.TextValue = dr.Item("DEST_NM").ToString()

            End With

            Return True

        End If

        Return False

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMF040F, ByVal actionType As LMF040C.ActionType) As LMFormData

        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With dr
            .Item("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue

            If actionType = LMF040C.ActionType.ENTER Then
                .Item("DEST_CD") = frm.txtDestCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("RELATION_SHOW_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)

        'Pop起動
        Return Me._LMFconH.FormShow(ds, "LMZ210", "", Me._PopupSkipFlg)

    End Function
    'END KURIHARA 要望番号:928

    ''' <summary>
    ''' 修正項目のPop設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置名</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <remarks></remarks>
    Private Function SetShuseiReturnPop(ByVal frm As LMF040F, ByVal objNm As String, ByVal actionType As LMF040C.ActionType) As Boolean

        SetShuseiReturnPop = False

        Select Case frm.cmbShusei.SelectedValue.ToString()

            Case LMF040C.SHUSEI_SEIQTO

                SetShuseiReturnPop = Me.SetReturnSeiqtoPop(frm, actionType)

            Case LMF040C.SHUSEI_TARIFF

                SetShuseiReturnPop = Me.SetReturnTariffPop(frm, objNm, actionType, LMFControlC.TARIFF_KONSAI)

            Case LMF040C.SHUSEI_YOKO

                SetShuseiReturnPop = Me.SetReturnTariffPop(frm, objNm, actionType, LMFControlC.TARIFF_YOKO)

            Case LMF040C.SHUSEI_CUST

                SetShuseiReturnPop = Me.SetReturnCustPop(frm, objNm, actionType)

                'START YANAI 要望番号996
            Case LMF040C.SHUSEI_ETARIFF

                SetShuseiReturnPop = Me.SetReturnExtcPop2(frm, actionType)
                'END YANAI 要望番号996

                'START s.kobayashi 要望番号2186
            Case LMF040C.SHUSEI_KYORI

                SetShuseiReturnPop = Me.SetReturnKyoriPop(frm, actionType)
                'END kobayashi 要望番号2186

        End Select

        Return SetShuseiReturnPop

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索部データ)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function SetConditionDataSet(ByVal frm As LMF040F) As DataSet

        Dim ds As DataSet = New LMF040DS()
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応
        Dim custDetailsDr() As DataRow = Nothing
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応

        'ヘッダ項目
        With frm

            dr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
            dr.Item("DATE_KBN") = .cmbDateKb.SelectedValue.ToString()
            dr.Item("DATE_FROM") = .imdFrom.TextValue
            dr.Item("DATE_TO") = .imdTo.TextValue
            dr.Item("TARIFF_BUNRUI_KB") = .cmbTariffKbn.SelectedValue.ToString()
            dr.Item("SEIQ_TARIFF_CD") = .txtTariffCd.TextValue
            dr.Item("SEIQ_ETARIFF_CD") = .txtExtcCd.TextValue
            dr.Item("DRIVER_CD") = .txtDriverCd.TextValue
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue
            dr.Item("UNCHIN_KBN") = Me.SetUnchinKbh(frm)
            dr.Item("GROUP_KB") = Me.SetGroupChk(frm)
            dr.Item("KAKUTEI_KB") = Me.SetKakutei(frm)
            dr.Item("MOTO_KB") = Me.SetMotoData(frm)
            dr.Item("ORDER_BY") = .cmbGroup.SelectedValue.ToString()
            'START KURIHARA 要望番号:928
            dr.Item("DEST_CD") = .txtDestCd.TextValue
            'END KURIHARA 要望番号:928
            'START YANAI 20120622 DIC運賃まとめ及び再計算対応
            '要望番号:1253 terakawa 2012.07.13 Start
            'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
            '                                                                                                            "SUB_KB = '33'"))
            custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue.ToString(), "' AND ", _
                                                                                                            "CUST_CD = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                            "SUB_KB = '33'"))
            '要望番号:1253 terakawa 2012.07.13 End
            If custDetailsDr.Length > 0 Then
                dr.Item("MATOME_KB") = custDetailsDr(0).Item("SET_NAIYO").ToString
            Else
                dr.Item("MATOME_KB") = String.Empty
            End If
            'END YANAI 20120622 DIC運賃まとめ及び再計算対応
        End With

        'スプレッド項目
        With frm.sprDetail.ActiveSheet

            dr.Item("CUST_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.CUST_NM.ColNo))
            dr.Item("SEIQTO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.SEIQTO_CD.ColNo))
            dr.Item("SEIQTO_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.SEIQTO_NM.ColNo))
            dr.Item("DEST_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.DEST_NM.ColNo))
            dr.Item("UNSO_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSO_NM.ColNo))
            dr.Item("UNSOCO_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSOCO_NM.ColNo))
            dr.Item("TAX_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.ZEI_KBN.ColNo))
            dr.Item("SEIQ_GROUP_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.GROUP.ColNo))
            dr.Item("SEIQ_GROUP_NO_M") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.GROUP_M.ColNo))
            dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.REMARK.ColNo))
            dr.Item("INOUTKA_NO_L") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.KANRI_NO.ColNo))
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSO_NO.ColNo))
            dr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSO_NO_EDA.ColNo))
            dr.Item("TRIP_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.TRIP_NO.ColNo))
            dr.Item("SYUKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.SHUKA_RELY_POINT.ColNo))
            dr.Item("HAIKA_TYUKEI_NM") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.HAIKA_RELY_POINT.ColNo))
            dr.Item("TRIP_NO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.TRIP_NO_SHUKA.ColNo))
            dr.Item("TRIP_NO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.TRIP_NO_CHUKEI.ColNo))
            dr.Item("TRIP_NO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.TRIP_NO_HAIKA.ColNo))
            dr.Item("UNSOCO_SYUKA") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSOCO_SHUKA.ColNo))
            dr.Item("UNSOCO_TYUKEI") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSOCO_CHUKEI.ColNo))
            dr.Item("UNSOCO_HAIKA") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSOCO_HAIKA.ColNo))
            'START KURIHARA 要望番号:928
            'dr.Item("DEST_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.DEST_CD.ColNo))
            'END KURIHARA 要望番号:928
            dr.Item("DEST_JIS_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))
            dr.Item("UNSO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSO_CD.ColNo))
            dr.Item("UNSO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSO_BR_CD.ColNo))
            dr.Item("UNSOCO_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSOCO_CD.ColNo))
            dr.Item("UNSOCO_BR_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.UNSOCO_BR_CD.ColNo))
            'START YANAI 20120622 DIC運賃まとめ及び再計算対応
            dr.Item("CUST_REF_NO") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.CUST_REF_NO.ColNo))
            dr.Item("ZBUKA_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.ZBUKA_CD.ColNo))
            dr.Item("ABUKA_CD") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.ABUKA_CD.ColNo))
            'END YANAI 20120622 DIC運賃まとめ及び再計算対応
            '2013.02.28 / Notes1897開始
            dr.Item("BIN_KB") = Me._LMFconG.GetCellValue(.Cells(0, LMF040G.sprDetailDef.BIN_NM.ColNo))
            '2013.02.28 / Notes1897終了
        End With

        '行追加
        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 運賃PK設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinPkData(ByVal frm As LMF040F, ByVal rowNo As Integer) As DataSet

        Dim ds As DataSet = New LMF040DS()
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        With frm.sprDetail.ActiveSheet

            dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO.ColNo))
            dr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO_EDA.ColNo))

        End With

        dt.Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' 更新情報設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function SetSaveDataSet(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList, ByVal actionType As LMF040C.ActionType) As Boolean

        Dim max As Integer = arr.Count - 1
        Dim spr As Win.Spread.LMSpread = frm.sprDetail
        Dim rowNo As Integer = 0

        '設定内容
        Dim fixFlg As String = String.Empty
        Dim item As String = String.Empty
        Dim cdL As String = String.Empty
        Dim cdM As String = String.Empty
        Dim cdS As String = String.Empty
        Dim cdSS As String = String.Empty
        Dim calcKbn As String = frm.lblCalcKbn.TextValue

        With frm

            'イベントによる切り分け
            Select Case actionType

                Case LMF040C.ActionType.FIX

                    item = LMF040C.FIX_ACTION
                    fixFlg = LMFControlC.FLG_ON

                Case LMF040C.ActionType.FIX_CANCELL

                    item = LMF040C.FIX_CANCELL_ACTION
                    fixFlg = LMFControlC.FLG_OFF

                Case LMF040C.ActionType.GROUP

                    item = LMF040C.GROUP_ACTION

                Case LMF040C.ActionType.GROUP_CANCELL

                    item = LMF040C.GROUP_CANCELL_ACTION

                Case LMF040C.ActionType.SAVE

                    item = .cmbShusei.SelectedValue.ToString()
                    cdL = .txtShuseiL.TextValue
                    cdM = .txtShuseiM.TextValue
                    cdS = .txtShuseiS.TextValue
                    cdSS = .txtShuseiSS.TextValue

            End Select

        End With

        '前ゼロ用
        Dim ketasu As Integer = Me._LMFconG.GetLimitData(LMF040C.IKKATU_LMF040, LMKbnConst.KBN_I004).ToString().Length
        Dim keta As String = Me._LMFconG.GetZeroData(ketasu)
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_UNCHIN)
        Dim dr As DataRow = Nothing

        'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
        frm.lblSysUpdDate.TextValue = MyBase.GetSystemDateTime(0)
        frm.lblSysUpdTime.TextValue = MyBase.GetSystemDateTime(1)
        'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

        'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
        Dim custDetailsDr() As DataRow = Nothing
        'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

        With spr.ActiveSheet

            For i As Integer = 0 To max

                'インスタンス生成
                dr = dt.NewRow()

                'スプレッドの行番号
                rowNo = Convert.ToInt32(arr(i))

                'スプレッドの値を設定
                dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO.ColNo))
                dr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO_EDA.ColNo))
                dr.Item("SEIQ_GROUP_NO") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.GROUP.ColNo))
                dr.Item("SEIQ_GROUP_NO_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.GROUP_M.ColNo))
                dr.Item("SEIQ_TARIFF_BUNRUI_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_KBN.ColNo))
                dr.Item("OUTKA_PLAN_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)))
                dr.Item("ARR_PLAN_DATE") = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)))
                dr.Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MOTO_DATA_KBN.ColNo))
                dr.Item("UNTIN_CALCULATION_KB") = Me.SetCalcKbn(spr, rowNo, actionType, item, calcKbn)
                dr.Item("REMARK") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.REMARK.ColNo))
                dr.Item("VCLE_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.VCLE_KB.ColNo))
                dr.Item("UNSO_ONDO_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNSO_ONDO_KB.ColNo))
                dr.Item("SIZE_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SIZE_KB.ColNo))
                dr.Item("CUST_CD_L") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_M.ColNo))
                dr.Item("DEST_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo))
                dr.Item("DEST_JIS") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo))
                dr.Item("SEIQTO_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo))
                dr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SYS_UPD_TIME.ColNo))

                '確定フラグの設定
                dr.Item("SEIQ_FIXED_FLAG") = fixFlg

                '一括変更の値設定
                dr.Item("ITEM_DATA") = item
                dr.Item("CD_L") = cdL
                dr.Item("CD_M") = cdM
                dr.Item("CD_S") = cdS
                dr.Item("CD_SS") = cdSS

                'チェック用
                dr.Item("ROW_NO") = Me._LMFconG.SetMaeZeroData(rowNo.ToString(), ketasu, keta)

                'START YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)
                dr.Item("NEW_SYS_UPD_DATE") = frm.lblSysUpdDate.TextValue
                dr.Item("NEW_SYS_UPD_TIME") = frm.lblSysUpdTime.TextValue
                dr.Item("SYS_UPD_FLG") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.SYS_UPD_FLG.ColNo))
                'END YANAI 20120622 DIC運賃まとめ及び再計算対応(一括変更の排他制御)

                'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
                dr.Item("CHECK_FLG") = String.Empty
                dr.Item("MATOME_KB") = String.Empty
                If (LMF040C.ActionType.GROUP).Equals(actionType) = True Then
                    '要望番号:1253 terakawa 2012.07.13 Start
                    'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ", _
                    '                                                                                                "SUB_KB = '33'"))
                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ", _
                                                                                                                    "CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ", _
                                                                                                                     "SUB_KB = '33'"))
                    '要望番号:1253 terakawa 2012.07.13 End
                    If custDetailsDr.Length > 0 Then
                        dr.Item("MATOME_KB") = custDetailsDr(0).Item("SET_NAIYO").ToString
                    End If
                End If
                dr.Item("BIN_KB") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.BIN_KB.ColNo))
                dr.Item("ZBUKA_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ZBUKA_CD.ColNo))
                dr.Item("ABUKA_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.ABUKA_CD.ColNo))
                dr.Item("MINASHI_DEST_CD") = Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.MINASHI_DEST_CD.ColNo))
                'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
#If True Then   'ADD 2020/07/20 013381   【LMS】日産物流の場合運賃請求明細にまとめた行を表示する
                dr.Item("MATOME_REMARK_UPNG_FLG") = String.Empty
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", dr.Item("NRS_BR_CD").ToString, "' AND ",
                                                                                                                    "CUST_CD = '", dr.Item("CUST_CD_L").ToString, "' AND ",
                                                                                                                     "SUB_KB = 'A0'"))
                If custDetailsDr.Length > 0 Then
                    If ("1").Equals(custDetailsDr(0).Item("SET_NAIYO").ToString) Then
                        dr.Item("MATOME_REMARK_UPNG_FLG") = LMConst.FLG.ON
                    End If
                End If

#End If
                '行追加
                dt.Rows.Add(dr)

            Next

        End With

        Return True

    End Function

    'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
    ''' <summary>
    ''' まとめ処理チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True</returns>
    ''' <remarks></remarks>
    Private Function CheckGroupDataSet(ByVal frm As LMF040F, ByVal ds As DataSet) As Boolean

        Dim orderBy As String = frm.lblOrderBy.TextValue
        If (LMF040C.ORDER_BY_CUSTTRIP).Equals(orderBy) = True OrElse _
            (LMF040C.ORDER_BY_DEST).Equals(orderBy) = True OrElse _
            (LMF040C.ORDER_BY_DESTJIS).Equals(orderBy) = True Then
            Return True
        End If

        Dim max As Integer = ds.Tables(LMF040C.TABLE_NM_UNCHIN).Rows.Count - 1
        If max = -1 Then
            Return False
        End If

        Dim rowNo As Integer = 0
        Dim chkFlg As Boolean = True
        Dim max2 As Integer = 0
        Dim sort As String = "UNSO_NO_L , UNSO_NO_M"

        Dim custDetailsDr() As DataRow = Nothing
        Dim dr() As DataRow = Nothing
        Dim matomeKb As String = String.Empty
        Dim nrsBrCd As String = String.Empty
        Dim motoDataKb As String = String.Empty
        Dim outkaPlanDate As String = String.Empty
        Dim arrPlanDate As String = String.Empty
        Dim destJisCd As String = String.Empty
        Dim binKb As String = String.Empty
        Dim zBukaCd As String = String.Empty
        Dim aBukaCd As String = String.Empty
        Dim aBukaCd5 As String = String.Empty
        Dim destCd As String = String.Empty
        Dim minashiDestCd As String = String.Empty
        Dim custCdL As String = String.Empty

        With ds.Tables(LMF040C.TABLE_NM_UNCHIN)

            For i As Integer = 0 To max
                rowNo = i

                If String.IsNullOrEmpty(.Rows(i).Item("CHECK_FLG").ToString) = False Then
                    Continue For
                End If

                matomeKb = .Rows(i).Item("MATOME_KB").ToString
                nrsBrCd = .Rows(i).Item("NRS_BR_CD").ToString
                motoDataKb = .Rows(i).Item("MOTO_DATA_KB").ToString
                outkaPlanDate = .Rows(i).Item("OUTKA_PLAN_DATE").ToString
                arrPlanDate = .Rows(i).Item("ARR_PLAN_DATE").ToString
                destJisCd = .Rows(i).Item("DEST_JIS").ToString
                binKb = .Rows(i).Item("BIN_KB").ToString
                zBukaCd = .Rows(i).Item("ZBUKA_CD").ToString
                aBukaCd = .Rows(i).Item("ABUKA_CD").ToString
                aBukaCd5 = Mid(.Rows(i).Item("ABUKA_CD").ToString, 1, 5)
                destCd = .Rows(i).Item("DEST_CD").ToString
                minashiDestCd = .Rows(i).Item("MINASHI_DEST_CD").ToString
                custCdL = .Rows(i).Item("CUST_CD_L").ToString

                If (LMF040C.DIC_MATOME_01).Equals(matomeKb) = True Then
                    '①千葉のまとめ対象荷主の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "ABUKA_CD <> '' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ",
                                               "DEST_JIS = '", destJisCd, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                ElseIf (LMF040C.NRS_BR_CD_10).Equals(nrsBrCd) = True Then
                    '②千葉のまとめ対象荷主以外の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                ElseIf (LMF040C.DIC_MATOME_02).Equals(matomeKb) = True Then
                    '③群馬のまとめ対象荷主の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "OUTKA_PLAN_DATE = '", outkaPlanDate, "' AND ",
                                               "BIN_KB = '", binKb, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                    '(要望番号2129) 2013.12.20 修正START
                ElseIf (LMF040C.DIC_MATOME_05).Equals(matomeKb) = True Then
                    '③群馬BPのまとめ対象荷主の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                    '(要望番号2129) 2013.12.20 修正END
                ElseIf ((LMF040C.NRS_BR_CD_30).Equals(nrsBrCd)) And ("30001".Equals(custCdL) Or "30002".Equals(custCdL) Or "30010".Equals(custCdL)) Then
                    '⑧春日部の場合（元々は営業所=55の処理）
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ",
                                               "ABUKA_CD = '", aBukaCd, "' AND ",
                                               "BIN_KB = '", binKb, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                ElseIf (LMF040C.NRS_BR_CD_30).Equals(nrsBrCd) = True Then
                    '④群馬のまとめ対象荷主以外の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "OUTKA_PLAN_DATE = '", outkaPlanDate, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                ElseIf (LMF040C.DIC_MATOME_03).Equals(matomeKb) = True Then
                    '⑤埼玉のまとめ対象荷主の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ",
                                               "ZBUKA_CD = '", zBukaCd, "' AND ",
                                               "SUBSTRING(ABUKA_CD,1,5) = '", aBukaCd5, "' AND ",
                                               "BIN_KB = '", binKb, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                ElseIf (LMF040C.DIC_MATOME_04).Equals(matomeKb) = True Then
                    '⑥埼玉のまとめ対象荷主の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ",
                                               "SUBSTRING(ABUKA_CD,1,5) = '", aBukaCd5, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                ElseIf (LMF040C.NRS_BR_CD_50).Equals(nrsBrCd) = True Then
                    '⑦埼玉のまとめ対象荷主以外の場合
                    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ",
                                               "MATOME_KB = '", matomeKb, "' AND ",
                                               "NRS_BR_CD = '", nrsBrCd, "' AND ",
                                               "MOTO_DATA_KB = '", motoDataKb, "' AND ",
                                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ",
                                               "(DEST_CD = '", destCd, "' OR ",
                                               "MINASHI_DEST_CD = '", minashiDestCd, "')"),
                                               sort)
                    'ElseIf (LMF040C.NRS_BR_CD_55).Equals(nrsBrCd) = True Then
                    '    '⑧春日部の場合
                    '    dr = .Select(String.Concat("CHECK_FLG = '", String.Empty, "' AND ", _
                    '                               "MATOME_KB = '", matomeKb, "' AND ", _
                    '                               "NRS_BR_CD = '", nrsBrCd, "' AND ", _
                    '                               "MOTO_DATA_KB = '", motoDataKb, "' AND ", _
                    '                               "ARR_PLAN_DATE = '", arrPlanDate, "' AND ", _
                    '                               "ABUKA_CD = '", aBukaCd, "' AND ", _
                    '                               "BIN_KB = '", binKb, "' AND ", _
                    '                               "(DEST_CD = '", destCd, "' OR ", _
                    '                               "MINASHI_DEST_CD = '", minashiDestCd, "')"), _
                    '                               sort)
                Else
                    '以外の場合
                    .Rows(i).Item("CHECK_FLG") = LMConst.FLG.OFF
                    Continue For
                End If

                max2 = dr.Length - 1
                If max2 <= 0 Then
                    '自データのみの場合は、まとめ対象が存在しないということ（①の場合、ABUKA_CDが空だったら１件もヒットしない）
                    .Rows(i).Item("CHECK_FLG") = LMConst.FLG.OFF
                    Continue For
                End If

                For j As Integer = 0 To max2
                    'まとめ対象が存在する場合は、チェックフラグをオンにする
                    dr(j).Item("CHECK_FLG") = LMConst.FLG.ON

                    'まとめ番号の設定
                    dr(j).Item("SEIQ_GROUP_NO") = dr(0).Item("UNSO_NO_L").ToString
                    dr(j).Item("SEIQ_GROUP_NO_M") = dr(0).Item("UNSO_NO_M").ToString
                Next

            Next

            'まとめ対象外レコードをエラーとする。
            dr = .Select(String.Concat("CHECK_FLG = '", LMConst.FLG.OFF, "'"), _
                                       "ROW_NO")
            max = dr.Length - 1
            For i As Integer = 0 To max
                'エラーメッセージをEXCEL出力
                Me._LMFconH.SetErrMessageStore("E239", Convert.ToInt32(dr(i).Item("ROW_NO").ToString))
            Next
            For i As Integer = max To 0 Step -1
                'エラーレコードを削除
                dr(i).Delete()
            Next
            If max > -1 Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' まとめ条件の日付を取得
    ''' </summary>
    ''' <param name="recordNo">レコード番号</param>
    ''' <returns>運送日</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoDate(ByVal ds As DataSet, ByVal recordNo As Integer) As String

        GetUnsoDate = String.Empty

        With ds.Tables(LMF040C.TABLE_NM_UNCHIN).Rows(recordNo)

            '締め日基準が入荷の場合
            If LMFControlC.CALC_NYUKA.Equals(.Item("UNTIN_CALCULATION_KB").ToString) = True Then

                GetUnsoDate = DateFormatUtility.DeleteSlash(.Item("ARR_PLAN_DATE").ToString)

            Else

                GetUnsoDate = DateFormatUtility.DeleteSlash(.Item("OUTKA_PLAN_DATE").ToString)

            End If

        End With

        Return GetUnsoDate

    End Function

    ''' <summary>
    ''' まとめチェック時の必須チェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsiHissuChk(ByVal value As String, ByVal rowNo As Integer) As Boolean

        If String.IsNullOrEmpty(value) = True Then
            Me._LMFconH.SetErrMessageStore("E001", rowNo)
            Return False
        End If

        Return True

    End Function
    'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない

    ''' <summary>
    ''' 締め日区分の設定
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="shuseiKbn">修正区分</param>
    ''' <param name="calcKbn">ヘッダ項目の締め日区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetCalcKbn(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal actionType As LMF040C.ActionType, ByVal shuseiKbn As String, ByVal calcKbn As String) As String

        SetCalcKbn = String.Empty

        With spr.ActiveSheet

            '一括変更以外 または 荷主を更新しない場合、スプレッドの値
            If LMF040C.ActionType.SAVE <> actionType _
                AndAlso LMF040C.SHUSEI_CUST.Equals(shuseiKbn) = False _
                Then
                Return Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo))
            End If

            '隠し項目の値を設定
            Return calcKbn

        End With

    End Function

    'START YANAI 要望番号582
    ''' <summary>
    ''' データセット設定(LMG060引数格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetLMG060InData(ByVal frm As LMF040F) As DataSet

        Dim ds As DataSet = New LMG060DS()
        Dim dr As DataRow = ds.Tables(LMControlC.LMG060C_TABLE_NM_IN).NewRow()

        dr.Item("F_DATE") = frm.imdFrom.TextValue
        dr.Item("T_DATE") = frm.imdTo.TextValue
        dr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
        dr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue

        ds.Tables(LMControlC.LMG060C_TABLE_NM_IN).Rows.Add(dr)
        Return ds

    End Function
    'END YANAI 要望番号582

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub StartAction(ByVal frm As LMF040F)

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
    Private Sub EndAction(ByVal frm As LMF040F)

        '画面解除
        MyBase.UnLockedControls(frm)

        'メッセージ設定
        Call Me.ShowGMessage(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 運賃区分の設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinKbh(ByVal frm As LMF040F) As String

        SetUnchinKbh = String.Empty

        With frm

            '車建て選択時
            If .optShaDate.Checked = True Then
                SetUnchinKbh = LMConst.FLG.OFF
            End If

            'トンキロ選択時
            If .optTonKiro.Checked = True Then
                SetUnchinKbh = LMConst.FLG.ON
            End If

        End With

        Return SetUnchinKbh

    End Function

    ''' <summary>
    ''' まとめ有無の条件設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SetGroupChk(ByVal frm As LMF040F) As String

        SetGroupChk = String.Empty

        With frm

            '未を選択した場合
            If .optGroupMi.Checked = True Then

                SetGroupChk = LMConst.FLG.OFF

            End If

            '済を選択した場合
            If .optGroupSumi.Checked = True Then

                SetGroupChk = LMConst.FLG.ON

            End If

        End With

        Return SetGroupChk

    End Function

    ''' <summary>
    ''' 確定有無の条件設定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SetKakutei(ByVal frm As LMF040F) As String

        SetKakutei = String.Empty

        With frm

            '未確定検索
            If .optRevMi.Checked = True Then
                SetKakutei = LMFControlC.FLG_OFF
            End If

            '確定済検索
            If .optRevKaku.Checked = True Then
                SetKakutei = LMFControlC.FLG_ON
            End If

        End With

        Return SetKakutei

    End Function

    ''' <summary>
    ''' 元データ区分の検索条件
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SetMotoData(ByVal frm As LMF040F) As String

        SetMotoData = String.Empty

        With frm

            Select Case True

                Case .optIn.Checked

                    SetMotoData = LMFControlC.MOTO_DATA_NYUKA

                Case .optOut.Checked

                    SetMotoData = LMFControlC.MOTO_DATA_SHUKKA

                Case .optUnso.Checked

                    SetMotoData = LMFControlC.MOTO_DATA_UNSO

            End Select

        End With

        Return SetMotoData

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionTyp">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UnchinSaveData(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal actionTyp As LMF040C.ActionType) As Boolean

        Dim msg As String = String.Empty
        Dim actionId As String = String.Empty
        Select Case actionTyp

            Case LMF040C.ActionType.FIX

                actionId = LMF040C.ACTION_ID_FIX
                msg = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F5ButtonName)

            Case LMF040C.ActionType.FIX_CANCELL

                actionId = LMF040C.ACTION_ID_FIX_CANCELL
                msg = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F6ButtonName)

            Case LMF040C.ActionType.GROUP

                'START YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
                'actionId = LMF040C.ACTION_ID_GROUP
                If (LMF040C.ORDER_BY_DIC).Equals(frm.lblOrderBy.TextValue) = False Then
                    'DIC以外
                    actionId = LMF040C.ACTION_ID_GROUP
                Else
                    actionId = LMF040C.ACTION_ID_GROUP_DIC
                End If
                'END YANAI 要望番号1206 運賃まとめ画面で全チェックまとめが出来ない
                msg = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F7ButtonName)

            Case LMF040C.ActionType.GROUP_CANCELL

                actionId = LMF040C.ACTION_ID_GROUP_CANCELL
                msg = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F8ButtonName)

            Case LMF040C.ActionType.SAVE

                actionId = LMF040C.ACTION_ID_SAVE
                msg = frm.btnHenko.Text

        End Select

        '確認メッセージ表示
        If Me._LMFconH.SetMessageC001(frm, msg) = False Then
            Return False
        End If

        'サーバアクセス
        Dim rtnDs As DataSet = Nothing

        'エラーがある場合、終了
        If Me.ActionData(frm, ds, actionId, rtnDs) = False Then
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
    Private Function ActionData(ByVal frm As LMF040F _
                                , ByVal ds As DataSet _
                                , ByVal actionId As String _
                                , Optional ByRef rtnDs As DataSet = Nothing _
                                ) As Boolean

        'サーバアクセス
        rtnDs = Me._LMFconH.ServerAccess(ds, actionId)

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
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="rtnDs">DataSet</param>
    ''' <param name="blf">BLF</param>
    ''' <param name="count">件数</param>
    ''' <returns>DataSet　ワーニングで「いいえ」を選択した場合、Nothing</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal frm As LMF040F _
                                    , ByVal ds As DataSet _
                                    , ByVal rtnDs As DataSet _
                                    , ByVal blf As String _
                                    , ByVal count As String _
                                    ) As DataSet

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            'Warningの場合
            If MyBase.IsWarningMessageExist() = True Then

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '通常検索の場合、2回目のサーバアクセス
                    If String.IsNullOrEmpty(frm.cmbGroup.SelectedValue.ToString()) = True Then

                        '強制実行フラグの設定
                        MyBase.SetForceOparation(True)

                        'WSA呼出し
                        'DBリードオンリー対応
                        'rtnDs = MyBase.CallWSA(blf, LMF040C.ACTION_ID_SELECT, ds)
                        rtnDs = MyBase.CallWSA(blf, LMF040C.ACTION_ID_SELECT, ds, True)

                    Else

                        'まとめ検索の場合、既にデータを持っている

                    End If

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

    ''' <summary>
    ''' 検索処理のアクションIDを取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>アクションID</returns>
    ''' <remarks></remarks>
    Private Function GetSelectActionId(ByVal frm As LMF040F) As String

        GetSelectActionId = String.Empty

        If String.IsNullOrEmpty(frm.cmbGroup.SelectedValue.ToString()) = True Then
            GetSelectActionId = LMF040C.ACTION_ID_SELECT
        Else
            GetSelectActionId = LMF040C.ACTION_ID_SELECT_GROUP
        End If

        Return GetSelectActionId

    End Function

#Region "再計算"

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 再計算処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function Silica(ByVal frm As LMF040F) As Boolean

        '処理開始
        Call Me.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthority(LMF040C.ActionType.SAIKEISAN)

        '再計算入力チェック
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMFconG.GetCheckList(frm.sprDetail.ActiveSheet, LMF040G.sprDetailDef.DEF.ColNo)
        End If
        'スプレッドの選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsSilicaChk(arr)

        'エラーの場合、終了
        If rtnResult = False Then

            '処理終了アクション
            Call Me.EndAction(frm)
            Exit Function

        End If

        Dim ds As DataSet = New LMF040DS()
        '選択レコードのチェック
        arr = Me.IsSilicaChk(frm, ds, arr)

        '2017/10/11 Annen アクサルタ 運賃按分計算の自動化対応 START
        If _IsAxaltaDousouProcess Then
            If MyBase.ShowMessage(frm, "C001", {"アクサルタの運賃按分計算"}).Equals(MsgBoxResult.Cancel) Then

                'メッセージのポップアップでキャンセルを押下したとき

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Function

            End If
        End If

        If Not _IsAxaltaDousouProcess Then

            'アクサルタ同送用処理以外

            'Nothingの場合、ワーニングで「いいえ」選択
            If arr Is Nothing = True Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Function

            End If

        Else

            'アクサルタ同送用

            If Not CheckAxaltaProcess(frm, arr) Then

                '処理終了アクション
                Call Me.EndAction(frm)
                Exit Function

            End If
        End If
        '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 END

        '選択している行数が残っているかを判定
        rtnResult = rtnResult AndAlso Me._LMFconH.ChkSelectData(arr)

        If rtnResult = True Then

            Dim max As Integer = arr.Count - 1
            Dim prm As LMFormData = New LMFormData()
            Dim prmDs As DataSet = New LMF800DS()
            Dim prmDt As DataTable = prmDs.Tables("UNCHIN_CALC_IN")
            Dim prmDr As DataRow = prmDt.NewRow()
            '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 START
            Dim prmDrMaxWeight As DataRow = prmDt.NewRow()
            Dim maxWeight As Decimal = 0
            Dim summaryWeight As Decimal = 0
            '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 END
            Dim unchinTariffDr() As DataRow = Nothing
            Dim rtnDs As DataSet = Nothing
            Dim outTbl As DataTable = Nothing
            Dim outDr As DataRow = Nothing
            Dim errDr As DataRow = Nothing
            Dim inDr As DataRow = Nothing

            Dim tariffKbn As String = String.Empty
            Dim tariffNm As String = String.Empty
            Dim wt As String = String.Empty
            Dim kyori As String = String.Empty
            Dim kosu As String = String.Empty
            Dim tariffCd As String = String.Empty
            Dim extcCd As String = String.Empty
            Dim nisugataKbn As String = String.Empty
            Dim nisugataNm As String = String.Empty
            Dim shashu As String = String.Empty
            Dim shashuNm As String = String.Empty
            Dim kiken As String = String.Empty
            Dim kikenNm As String = String.Empty
            Dim deciKyori As String = String.Empty
            Dim sime As String = String.Empty
            Dim startDate As String = String.Empty
            Dim destJisCd As String = String.Empty
            Dim unsoDate As String = String.Empty

            Dim rowNo As Integer = 0

            ds = New LMF040DS()

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))

                tariffKbn = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_KBN.ColNo)).ToString
                tariffNm = Me.GetKbnData(tariffKbn, LMKbnConst.KBN_T015)
                wt = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.JURYO.ColNo)).ToString
                kyori = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.KYORI.ColNo)).ToString
                kosu = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.DECI_NG_NB.ColNo)).ToString
                tariffCd = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo)).ToString
                extcCd = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo)).ToString
                nisugataKbn = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_PKG_UT.ColNo)).ToString
                nisugataNm = Me.GetKbnData(nisugataKbn, LMKbnConst.KBN_N001)
                shashu = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_SYARYO_KB.ColNo)).ToString
                shashuNm = Me.GetKbnData(shashu, LMKbnConst.KBN_S012)
                kiken = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_DANGER_KB.ColNo)).ToString
                kikenNm = Me.GetKbnData(kiken, LMKbnConst.KBN_K008)
                deciKyori = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.KYORI.ColNo)).ToString

                If (LMFControlC.TARIFF_KONSAI).Equals(tariffKbn) = True OrElse _
                    (LMFControlC.TARIFF_KURUMA).Equals(tariffKbn) = True OrElse _
                    (LMFControlC.TARIFF_TOKUBIN).Equals(tariffKbn) = True Then
                    sime = String.Empty
                    startDate = String.Empty

                    sime = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString

                    '締め基準が01の場合
                    If LMFControlC.CALC_SHUKKA.Equals(sime) = True Then
                        '出荷日の設定
                        startDate = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString.Replace("/", String.Empty)
                    Else
                        '納入日の設定
                        startDate = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)).ToString.Replace("/", String.Empty)
                    End If

                    destJisCd = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo)).ToString
                    unchinTariffDr = Me._LMFconG.SelectUnchinTariffListDataRow(tariffCd, String.Empty, startDate)
                    If 0 < unchinTariffDr.Length Then
                        If (LMFControlC.TABTP_KOKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) OrElse _
                            (LMFControlC.TABTP_TAKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) OrElse _
                            (LMFControlC.TABTP_JYUKEN).Equals(unchinTariffDr(0).Item("TABLE_TP").ToString()) Then
                            If String.IsNullOrEmpty(destJisCd) = False Then
                                deciKyori = Mid(destJisCd, 1, 2)
                            Else
                                deciKyori = "0"
                            End If
                        End If
                    End If
                End If

                '運送日は締め日基準で切り替える
                unsoDate = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString.Replace("/", String.Empty)
                If LMFControlC.CALC_SHUKKA.Equals(Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString) = False Then
                    unsoDate = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)).ToString.Replace("/", String.Empty)
                End If


                'データテーブルのクリア
                prm = New LMFormData()
                prmDs = New LMF800DS()
                prmDt = prmDs.Tables("UNCHIN_CALC_IN")
                prmDr = prmDt.NewRow()

                With prmDr

                    '運賃計算プログラムのINパラメータ記入
                    .Item("ACTION_FLG") = LMFControlC.FLG_OFF
                    .Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo)).ToString
                    .Item("CUST_CD_L") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo)).ToString
                    .Item("CUST_CD_M") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_M.ColNo)).ToString
                    .Item("DEST_CD") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.DEST_CD.ColNo)).ToString
                    .Item("DEST_JIS") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.DEST_JIS_CD.ColNo)).ToString

                    'Todo)
                    '2018/02/06 Annen
                    'タリフが取得できないことがあるバグ対応で、原因としてタリフ存在チェックがyyyy/MM/dd型で取得した値を
                    'タリフマスタのキャッシュ、適用開始日（yyyyMMdd型）と比較していた事象が発生したが、
                    '以下の項目も日付を取得した際にyyyyMMdd型に変換していないため、何かしらの影響が出る可能性がある。
                    '現状では保留とすることになった
                    '( 001066 LMS-バグ対応】_運賃再計算が行えない(アクサルタ) 対応時記載)
                    .Item("ARR_PLAN_DATE") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)).ToString

                    .Item("UNSO_PKG_NB") = kosu
                    .Item("NB_UT") = nisugataKbn
                    .Item("UNSO_WT") = wt
                    .Item("UNSO_ONDO_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNSO_ONDO_KB.ColNo)).ToString
                    .Item("TARIFF_BUNRUI_KB") = tariffKbn
                    .Item("VCLE_KB") = shashu
                    .Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.MOTO_DATA_KBN.ColNo)).ToString
                    .Item("SEIQ_TARIFF_CD") = tariffCd
                    .Item("SEIQ_ETARIFF_CD") = extcCd
                    .Item("UNSO_TTL_QT") = "0" 'LMF050でも、0以外ありえないので、固定で0を設定
                    .Item("SIZE_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SIZE_KB.ColNo)).ToString
                    .Item("UNSO_DATE") = unsoDate
                    .Item("CARGO_KB") = String.Empty
                    .Item("CAR_TP") = "00"
                    .Item("WT_LV") = "0"
                    .Item("KYORI") = deciKyori
                    .Item("DANGER_KB") = kiken
                    .Item("GOODS_CD_NRS") = String.Empty


                    'パラムに検索条件の追加
                    prmDt.Rows.Add(prmDr)
                    prm.ParamDataSet = prmDs

                    '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 START
                    If Convert.ToDecimal(wt) > maxWeight Then
                        '選択行で最も重い重量のINIパラメータを保持する
                        prmDrMaxWeight.ItemArray = prmDr.ItemArray
                        '最大重量を更新する
                        maxWeight = Convert.ToDecimal(wt)
                    End If
                    '選択行の重量合計を算出する
                    summaryWeight += Convert.ToDecimal(wt)
                    '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 END

                    '運賃計算プログラムの呼び出し
                    LMFormNavigate.NextFormNavigate(Me, "LMF800", prm)

                    '計算結果をOutのテーブルに設定
                    rtnDs = prm.ParamDataSet
                    outTbl = rtnDs.Tables("UNCHIN_CALC_OUT")

                    'LMF800RESULTからエラーメッセージを取得する
                    errDr = rtnDs.Tables("LMF800RESULT").Rows(0)

                    Select Case errDr.Item("STATUS").ToString()

                        Case "00"

                            'インスタンス生成
                            inDr = ds.Tables(LMF040C.TABLE_NM_UNCHIN).NewRow()

                            outDr = outTbl.Rows(0)

                            '運賃の設定
                            inDr.Item("DECI_CITY_EXTC") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("CITY_EXTC").ToString()))
                            inDr.Item("DECI_WINT_EXTC") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("WINT_EXTC").ToString()))
                            inDr.Item("DECI_RELY_EXTC") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("RELY_EXTC").ToString()))
                            inDr.Item("DECI_TOLL") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("TOLL").ToString()))
                            inDr.Item("DECI_INSU") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(outDr.Item("INSU").ToString()))
                            inDr.Item("DECI_UNCHIN") = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.FormatNumValue(outDr.Item("UNCHIN").ToString())))

                            inDr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo)).ToString
                            inDr.Item("UNSO_NO_L") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO.ColNo)).ToString
                            inDr.Item("UNSO_NO_M") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNSO_NO_EDA.ColNo)).ToString
                            inDr.Item("OUTKA_PLAN_DATE") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString.Replace("/", String.Empty)
                            inDr.Item("ARR_PLAN_DATE") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)).ToString.Replace("/", String.Empty)
                            inDr.Item("MOTO_DATA_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.MOTO_DATA_KBN.ColNo)).ToString
                            inDr.Item("UNTIN_CALCULATION_KB") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString
                            inDr.Item("SEIQTO_CD") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo)).ToString
                            inDr.Item("SYS_UPD_DATE") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SYS_UPD_DATE.ColNo)).ToString
                            inDr.Item("SYS_UPD_TIME") = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SYS_UPD_TIME.ColNo)).ToString
                            inDr.Item("ROW_NO") = rowNo
                            '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 START
                            If _IsAxaltaDousouProcess Then
                                '文字列で重量をソート出来るようにフォーマットを合わせる
                                Dim tmpWt As String = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.JURYO.ColNo)).ToString
                                tmpWt = Convert.ToDecimal(Me._LMFconG.FormatNumValue(tmpWt)).ToString("000000000000.000")
                                inDr.Item("SEIQ_WT") = tmpWt
                            End If
                            '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 END
                            ds.Tables(LMF040C.TABLE_NM_UNCHIN).Rows.Add(inDr)

                        Case Else

                            '異常系(返却値からメッセージを設定)
                            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, errDr.Item("ERROR_CD").ToString, New String() {errDr.Item("YOBI1").ToString}, rowNo.ToString())

                    End Select

                End With

            Next

            '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 START
            If _IsAxaltaDousouProcess Then
                If IsMessageStoreExist() Then
                    'EXCEL起動 
                    MyBase.MessageStoreDownload(True)
                    MyBase.ShowMessage(frm, "E235")
                    'ロック解除
                    Call Me.EndAction(frm)
                    Return False
                Else
                    'Memo)
                    'アクサルタ同送用の処理の場合、
                    '画面で選択した対象行の重量合計を求め、それを最大重量の行で重量に置き換え再度計算を行い、
                    '再計算された運賃を重量の重い順から運賃を引いていき、最低賃金の50を満たないようになったら
                    '残った運賃+50を設定するようにする。
                    '尚、最低賃金の50は全ての対象行に渡って分配されるようにする。
                    '方式としては運賃設定データテーブルの内容をコピーし、その運賃を加工して
                    '元のテーブルの内容と置き換えるように作成している。

                    '重量の合計で運賃計算を行う
                    prm = New LMFormData()
                    prmDs = New LMF800DS()
                    prmDt = prmDs.Tables("UNCHIN_CALC_IN")
                    prmDr = prmDt.NewRow()
                    prmDrMaxWeight.Item("UNSO_WT") = Convert.ToString(summaryWeight)
                    prmDr.ItemArray() = prmDrMaxWeight.ItemArray
                    prmDs.Tables("UNCHIN_CALC_IN").Rows.Add(prmDr)
                    prm.ParamDataSet = prmDs

                    '運賃計算プログラムの呼び出し
                    LMFormNavigate.NextFormNavigate(Me, "LMF800", prm)
                    Dim tnpRtnDs As DataSet = prm.ParamDataSet
                    Dim tmpOutTbl As DataTable = tnpRtnDs.Tables("UNCHIN_CALC_OUT")

                    '合計した重量で運賃を取得する
                    Dim unchinOfSummaryWeight As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(tmpOutTbl.Rows(0).Item("UNCHIN").ToString()))

                    '運賃設定データテーブルの内容をコピーし重量でソートする
                    Dim tmpTable As DataTable = ds.Tables(LMF040C.TABLE_NM_UNCHIN).Copy
                    Dim tmpRows() As DataRow = tmpTable.Select("", "SEIQ_WT desc")
                    '運賃設定データテーブルの内容をクリアする
                    ds.Tables(LMF040C.TABLE_NM_UNCHIN).Clear()

                    '対象データ数分、重量合計から求めた運賃より50円を引いておく
                    unchinOfSummaryWeight -= Convert.ToDecimal(tmpTable.Rows.Count * 50)

                    For Each tmpDataRow As DataRow In tmpRows
                        Dim targetUnchin As Decimal = Convert.ToDecimal(Me._LMFconG.FormatNumValue(tmpDataRow.Item("DECI_UNCHIN").ToString()))

                        'ソートのため取得した重量を削除しておく
                        tmpDataRow("SEIQ_WT") = String.Empty

                        If targetUnchin <= unchinOfSummaryWeight Then
                            '重量合計から求めた運賃の方が高い（以上の）場合、重量合計から求めた運賃から対象データの運賃 - 50（先に引いた最低賃金分）を引く
                            '元の運賃は変更しない
                            unchinOfSummaryWeight -= (targetUnchin - 50)
                        Else
                            '重量合計から求めた運賃の方が低い場合、対象運賃は残った運賃+50を設定し、
                            '重量合計から求めた運賃を0にする。
                            tmpDataRow.Item("DECI_UNCHIN") = unchinOfSummaryWeight + 50
                            unchinOfSummaryWeight = 0
                        End If

                        '運賃設定データテーブルにコピーで更新した行を追加する
                        ds.Tables(LMF040C.TABLE_NM_UNCHIN).ImportRow(tmpDataRow)

                    Next
                End If
            End If
            '2017/10/12 Annen アクサルタ 運賃按分計算の自動化対応 END

            If ds IsNot Nothing Then
                '運賃データ更新処理
                Call Me.UpdUnchinData(ds, frm)
            End If

        End If

        If MyBase.IsMessageStoreExist() = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            'ロック解除
            Call Me.EndAction(frm)
            Return False
        End If

        'ロック解除
        Call Me.EndAction(frm)

    End Function

    ''' <summary>
    ''' 区分名1を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>区分名1</returns>
    ''' <remarks></remarks>
    Private Function GetKbnData(ByVal kbnCd As String, ByVal groupCd As String) As String

        GetKbnData = String.Empty
        Dim drs As DataRow() = Me._LMFconG.SelectKbnListDataRow(kbnCd, groupCd)
        If 0 < drs.Length Then
            GetKbnData = drs(0).Item("KBN_NM1").ToString()
        End If

        Return GetKbnData

    End Function

    ''' <summary>
    ''' 運賃更新処理(再計算時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub UpdUnchinData(ByVal ds As DataSet, ByVal frm As LMF040F)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "UpdUnchinData")

        '==========================
        'WSAクラス呼出
        '==========================
        '2017/10/13 Annen アクサルタ 運賃按分計算の自動化対応 START
        If _IsAxaltaDousouProcess Then
            'アクサルタ同送処理の場合はアクサルタ同送用運賃更新処理を呼び出す
            ds = MyBase.CallWSA("LMF040BLF", "UpdUnchinDataAxaltaDousou", ds)
        Else
            ds = MyBase.CallWSA("LMF040BLF", "UpdUnchinData", ds)
        End If
        'ds = MyBase.CallWSA("LMF040BLF", "UpdUnchinData", ds)
        '2017/10/13 Annen アクサルタ 運賃按分計算の自動化対応 END

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        '2016.01.06 UMANO 英語化対応START
        'MyBase.ShowMessage(frm, "G002", New String() {"再計算処理", ""})
        MyBase.ShowMessage(frm, "G002", New String() {frm.FunctionKey.F11ButtonName, ""})
        '2016.01.06 UMANO 英語化対応END

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "UpdUnchinData")

    End Sub
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

#End Region

#End Region

#Region "メッセージ設定"

    ''' <summary>
    ''' ガイダンスメッセージ表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowGMessage(ByVal frm As LMF040F)

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
    Private Sub SetInitMessage(ByVal frm As LMF040F)
        MyBase.ShowMessage(frm, "G007")
    End Sub

#End Region

#Region "別PG起動処理"

    ''' <summary>
    ''' 運賃編集画面を起動
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="recType">レコードタイプ</param>
    ''' <remarks></remarks>
    Private Sub OpenUnchinEditGamen(ByVal ds As DataSet, ByVal recType As String)

        '画面起動
        Call Me._LMFconH.FormShow(ds, "LMF050", recType)

    End Sub

#End Region

#Region "チェック"

#Region "各処理のチェック"

    ''' <summary>
    ''' 一括変更チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSelectChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        With frm

            Dim max As Integer = arr.Count - 1
            Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
            Dim dr As DataRow = Nothing
            Dim rtnResult As Boolean = True
            Dim msg As String = Me._LMFconV.SetRepMsgData(.btnHenko.Text)
            Dim spr As FarPoint.Win.Spread.SheetView = .sprDetail.ActiveSheet
            Dim rowNo As Integer = 0
            Dim cdL As String = .txtShuseiL.TextValue
            Dim cdM As String = .txtShuseiM.TextValue
            Dim shusei As String = .cmbShusei.SelectedValue.ToString()

            For i As Integer = 0 To max

                'スプレッドの行番号を設定
                rowNo = Convert.ToInt32(arr(i))

                '自営業チェック
                rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

                '荷主の同値チェック
                rtnResult = rtnResult And Me.IsCustDotiChk(frm, shusei, cdL, cdM, rowNo)

                'タリフマスタの存在チェック
                rtnResult = rtnResult And Me.IsUnchinExistChk(frm, rowNo)

                'START YANAI 要望番号996
                '割増タリフマスタの存在チェック
                rtnResult = rtnResult And Me.IsETariffExistChk(frm, rowNo)
                'END YANAI 要望番号996

                '確定済チェック（2012/01/25撤去）
                'rtnResult = rtnResult And Me.IsKakuteiChk(frm, rowNo, True, LMFControlC.KAKUTEI_ZUMI)

                'エラーがある場合、DataTableに設定
                Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

            Next

            'エラーになったものを削除
            arr = Me._LMFconV.SetErrDt(dt, arr)

            Return arr

        End With

    End Function

    'START YANAI 要望番号561
    ''' <summary>
    ''' 連続入力チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト　ワーニングで「いいえ」を選択した場合、Nothing</returns>
    ''' <remarks></remarks>
    Private Function IsLoopEditChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim msg As String = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F4ButtonName)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0
        Dim unchinChk As Boolean = True

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        arr = Me._LMFconV.SetErrDt(dt, arr)

        '全てエラーの場合、終了
        If arr.Count < 1 Then
            Return arr
        End If

        'ワーニング表示
        If unchinChk = False _
            AndAlso Me._LMFconV.IsWarningChk(MyBase.ShowMessage(frm, "W144")) = False Then
            'AndAlso Me._LMFconV.IsWarningChk(MyBase.ShowMessage(frm, "W144", New String() {LMFControlC.UNCHIN})) = False Then
            '2016.01.06 UMANO 英語化対応END
            Return Nothing
        End If

        Return arr

    End Function
    'END YANAI 要望番号561

    ''' <summary>
    ''' 確定チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト　ワーニングで「いいえ」を選択した場合、Nothing</returns>
    ''' <remarks></remarks>
    Private Function IsFixSelectChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim msg As String = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F5ButtonName)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0
        Dim unchinChk As Boolean = True

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

            '確定済チェック
            rtnResult = rtnResult And Me.IsKakuteiChk(frm, rowNo, True, LMFControlC.KAKUTEI_ZUMI)

            '出荷日の必須チェック
            rtnResult = rtnResult And Me.IsDateHissuChk(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)), LMFControlC.SHUKKABI, rowNo)

            '納入日の必須チェック
            rtnResult = rtnResult And Me.IsDateHissuChk(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)), LMFControlC.NONYUBI, rowNo)

            'START YANAI 要望番号446
            '請求先コード必須チェック
            rtnResult = rtnResult And Me.IsSeiqtoCdHissuChk(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQTO_CD.ColNo)), LMFControlC.SEIQTO_CD, rowNo)
            'END YANAI 要望番号446

            'START YANAI 要望番号974
            '運賃・割り増し運賃0円チェック
            rtnResult = rtnResult And Me.IsUnchinWariZeroChk(frm, "運賃が０円、割増運賃が０円でないレコード", rowNo)
            'END YANAI 要望番号974

            '運賃タリフマスタの承認チェック
            rtnResult = rtnResult And Me.IsApprovalChk(frm, rowNo)

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

            '運賃チェック
            unchinChk = unchinChk AndAlso Me.IsUnchinZeroChk(frm, rowNo, rtnResult)

        Next

        'エラーになったものを削除
        arr = Me._LMFconV.SetErrDt(dt, arr)

        '全てエラーの場合、終了
        If arr.Count < 1 Then
            Return arr
        End If

        'ワーニング表示
        If unchinChk = False _
            AndAlso Me._LMFconV.IsWarningChk(MyBase.ShowMessage(frm, "W144")) = False Then
            'AndAlso Me._LMFconV.IsWarningChk(MyBase.ShowMessage(frm, "W144", New String() {LMFControlC.UNCHIN})) = False Then
            '2016.01.06 UMANO 英語化対応END
            Return Nothing
        End If

        Return arr

    End Function

    ''' <summary>
    ''' 確定解除チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト</returns>
    ''' <remarks></remarks>
    Private Function IsFixCancellSelectChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim msg As String = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F5ButtonName)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

            '確定未チェック
            rtnResult = rtnResult And Me.IsKakuteiChk(frm, rowNo, False, LMFControlC.MI_KAKUTEI)

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        arr = Me._LMFconV.SetErrDt(dt, arr)

        Return arr

    End Function

    'START KIM 2012/11/21 要望番号：1400
    ''' <summary>
    ''' まとめ解除チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト</returns>
    ''' <remarks></remarks>
    Private Function IsGroupSelectChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim msg As String = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F7ButtonName)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

            'まとめ済チェック
            rtnResult = Me.IsZumiGroupChk(frm, rowNo)

            '確定済チェック
            rtnResult = rtnResult And Me.IsKakuteiChk(frm, rowNo, True, LMFControlC.KAKUTEI_ZUMI)

            '出荷日必須チェック
            rtnResult = rtnResult And Me.IsDateHissuChk(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040C.SprColumnIndex.SHUKKA)), "出荷日", rowNo)

            '納入日必須チェック
            rtnResult = rtnResult And Me.IsDateHissuChk(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040C.SprColumnIndex.NONYU)), "納入日", rowNo)

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        arr = Me._LMFconV.SetErrDt(dt, arr)

        Return arr

    End Function
    'END   KIM 2012/11/21 要望番号：1400

    ''' <summary>
    ''' まとめ解除チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト</returns>
    ''' <remarks></remarks>
    Private Function IsGroupCancellSelectChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim msg As String = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F5ButtonName)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

            'まとめ未チェック
            rtnResult = rtnResult And Me.IsMiGroupChk(frm, rowNo)

            '確定済チェック
            rtnResult = rtnResult And Me.IsKakuteiChk(frm, rowNo, True, LMFControlC.KAKUTEI_ZUMI)

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        arr = Me._LMFconV.SetErrDt(dt, arr)

        Return arr

    End Function

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' 再計算チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>リスト　ワーニングで「いいえ」を選択した場合、Nothing</returns>
    ''' <remarks></remarks>
    Private Function IsSilicaChk(ByVal frm As LMF040F, ByVal ds As DataSet, ByVal arr As ArrayList) As ArrayList

        Dim max As Integer = arr.Count - 1
        Dim dt As DataTable = ds.Tables(LMF040C.TABLE_NM_ERR)
        Dim dr As DataRow = Nothing
        Dim rtnResult As Boolean = True
        Dim msg As String = Me._LMFconV.SetRepMsgData(frm.FunctionKey.F11ButtonName)
        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rowNo As Integer = 0
        Dim unchinChk As Boolean = True
        Dim drs As DataRow() = Nothing

        Dim nrsbrcd As String = String.Empty
        Dim tariffKbn As String = String.Empty
        Dim tariffCd As String = String.Empty
        Dim extcCd As String = String.Empty
        Dim sime As String = String.Empty
        Dim startDate As String = String.Empty

        For i As Integer = 0 To max

            'スプレッドの行番号を設定
            rowNo = Convert.ToInt32(arr(i))

            '自営業チェック
            rtnResult = Me.IsMyNrsChk(spr, rowNo, msg)

            nrsbrcd = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo)).ToString
            tariffKbn = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_KBN.ColNo)).ToString
            tariffCd = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo)).ToString
            extcCd = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo)).ToString
            sime = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString

            '締め基準が01の場合
            If LMFControlC.CALC_SHUKKA.Equals(sime) = True Then
                '出荷日の設定
                startDate = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString.Replace("/", String.Empty)
            Else
                '納入日の設定
                startDate = Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)).ToString.Replace("/", String.Empty)
            End If


            If (LMFControlC.TARIFF_YOKO).Equals(tariffKbn) = False Then
                '横持以外の場合

                'タリフコード、運送日が入力されているとき
                If String.IsNullOrEmpty(tariffCd) = False AndAlso _
                    String.IsNullOrEmpty(startDate) = False AndAlso _
                    rtnResult = True Then

                    '存在チェック(タリフマスタ)
                    If Me._LMFconV.SelectUnchinTariffListDataRow(drs, tariffCd, , startDate) = False Then
                        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E079", New String() {"運賃タリフマスタ", tariffCd}, rowNo.ToString())

                        rtnResult = False
                    End If
                End If

            Else
                '横持の場合

                '営業所コード、タリフコードが入力されているとき
                If String.IsNullOrEmpty(nrsbrcd) = False AndAlso _
                    String.IsNullOrEmpty(tariffCd) = False AndAlso _
                    rtnResult = True Then

                    '存在チェック(横持ちタリフマスタ)
                    If Me._LMFconV.SelectYokoTariffListDataRow(drs, nrsbrcd, tariffCd) = False Then
                        MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E079", New String() {"横持ちタリフマスタ", tariffCd}, rowNo.ToString())
                        rtnResult = False
                    End If
                End If
            End If


            '営業所、割増タリフコードが入力されているとき
            If String.IsNullOrEmpty(nrsbrcd) = False AndAlso _
                String.IsNullOrEmpty(extcCd) = False Then

                '存在チェック(割増タリフマスタ)
                If Me._LMFconV.SelectExtcUnchinListDataRow(drs, nrsbrcd, extcCd) = False Then
                    rtnResult = False
                End If

            End If

            'エラーがある場合、DataTableに設定
            Call Me._LMFconV.SetErrDt(dt, dr, rtnResult, i)

        Next

        'エラーになったものを削除
        arr = Me._LMFconV.SetErrDt(dt, arr)

        '全てエラーの場合、終了
        If arr.Count < 1 Then
            Return arr
        End If

        'ワーニング表示
        If unchinChk = False _
            AndAlso Me._LMFconV.IsWarningChk(MyBase.ShowMessage(frm, "W144")) = False Then
            'AndAlso Me._LMFconV.IsWarningChk(MyBase.ShowMessage(frm, "W144", New String() {LMFControlC.UNCHIN})) = False Then
            '2016.01.06 UMANO 英語化対応END
            Return Nothing
        End If

        Return arr

    End Function
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    '2017/10/11 Annen アクサルタ 運賃按分計算の自動化対応 START
    ''' <summary>
    ''' 再計算チェック（アクサルタ同送用）
    ''' </summary>
    ''' <param name="frm">自画面</param>
    ''' <param name="arr">詳細スプレッドの選択行インデックスを格納した配列</param>
    ''' <returns>チェックOK = True、チェックNG = False</returns>
    ''' <remarks></remarks>
    Private Function CheckAxaltaProcess(ByVal frm As LMF040F, ByVal arr As ArrayList) As Boolean
        Dim max As Integer = arr.Count - 1
        Dim basicRowNo As Integer = Convert.ToInt32(arr(0))
        Dim rowNo As Integer = 0
        Dim spread As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        Dim rtnResult As Boolean = True

        If max.Equals(0) Then

            '１行のみ選択した場合

            '単行チェックを行う
            Call AxaltaSingleRowChek(spread, basicRowNo)

        Else

            '複数行選択した場合

            '比較基本行を0行目とするため、比較対象の行は1行目から行う。
            For spreadRowCounter As Integer = 1 To max

                '初回ループの場合は基本行の単行チェックを行う
                If spreadRowCounter.Equals(1) Then
                    Call AxaltaSingleRowChek(spread, basicRowNo)
                End If

                '比較対象行を取得する
                rowNo = Convert.ToInt32(arr(spreadRowCounter))

                '比較対象行の単行チェックを行う
                AxaltaSingleRowChek(spread, rowNo)

                '基本行と比較対象行の相関チェックを行う
                MultiRowCorrelationCheck(spread, basicRowNo, rowNo)
            Next

        End If

        If MyBase.IsMessageStoreExist() Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
            rtnResult = False
        End If

        Return rtnResult

    End Function

    ''' <summary>
    ''' アクサルタ同送処理時の単行チェック
    ''' </summary>
    ''' <param name="Spread">画面の明細スプレッド</param>
    ''' <param name="rowIndex">対象行Index</param>
    ''' <remarks></remarks>
    Private Sub AxaltaSingleRowChek(ByVal Spread As FarPoint.Win.Spread.SheetView, ByVal rowIndex As Integer)
        Dim nrsbrcd As String = String.Empty            '営業所
        Dim sime As String = String.Empty               '締め日基準
        Dim startDate As String = String.Empty          '運送日
        Dim tariffKbn As String = String.Empty          'タリフ区分
        Dim tariffCd As String = String.Empty           'タリフNo
        Dim extcCd As String = String.Empty             '割増タリフマスタ
        Dim drs As DataRow() = Nothing                  'ダミー行
        Dim rtnResult As Boolean = True                 'True： エラーなし　False：エラー 

        '絞め日基準チェック
        sime = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString
        If String.IsNullOrEmpty(sime) Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"締め日基準が設定されていない", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

        '運送日チェック
        If LMFControlC.CALC_SHUKKA.Equals(sime) Then
            '締め基準が01の場合
            '出荷日を設定
            startDate = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString()
        Else
            '納品日を設定
            startDate = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.NONYU.ColNo)).ToString()
        End If

        '2018/02/06 Annen 001066 LMS-バグ対応】_運賃再計算が行えない(アクサルタ) 対応 add start
        'Memo)
        '画面から取得した適用開始日が"yyyy/MM/dd"型なのに対し、比較しているタリフマスタのキャッシュ、
        '項目「適用開始日」が"yyyyMMdd"型なので正しく取得出来ていない。
        'そのため、画面から取得した適用開始日を"yyyyMMdd"型に変換して正しく比較を行えるように対応した。
        startDate = DateFormatUtility.DeleteSlash(startDate)
        '2018/02/06 Annen 001066 LMS-バグ対応】_運賃再計算が行えない(アクサルタ) 対応 add end

        If rtnResult AndAlso String.IsNullOrEmpty(startDate) Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"出荷日、または納品日が設定されていない", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

        'タリフ存在チェック
        nrsbrcd = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.NRS_BR_CD.ColNo)).ToString
        tariffKbn = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.TARIFF_KBN.ColNo)).ToString
        tariffCd = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.TARIFF_CD.ColNo)).ToString

        If Not tariffKbn.Equals(LMFControlC.TARIFF_YOKO) Then
            '横持でない場合
            If Not String.IsNullOrEmpty(tariffCd) AndAlso _
                rtnResult = True Then
                '存在チェック(タリフマスタ)
                If Not Me._LMFconV.SelectUnchinTariffListDataRow(drs, tariffCd, , startDate) Then
                    MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E079", New String() {"運賃タリフマスタ", tariffCd}, rowIndex.ToString())
                    rtnResult = False
                End If
            End If
        Else
            '横持の場合
            If Not String.IsNullOrEmpty(tariffCd) AndAlso _
                rtnResult = True Then
                '存在チェック(横持ちタリフマスタ)
                If Not Me._LMFconV.SelectYokoTariffListDataRow(drs, nrsbrcd, tariffCd) Then
                    MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E079", New String() {"横持ちタリフマスタ", tariffCd}, rowIndex.ToString())
                    rtnResult = False
                End If
            End If
        End If

        '存在チェック(割増タリフマスタ)
        extcCd = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.EXTC_TARIFF_CD.ColNo)).ToString
        If Not String.IsNullOrEmpty(extcCd) Then
            If Me._LMFconV.SelectExtcUnchinListDataRow(drs, nrsbrcd, extcCd) = False Then
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E079", New String() {"割増タリフマスタ", tariffCd}, rowIndex.ToString())
                rtnResult = False
            End If
        End If

        '重量が0の場合、エラー
        If Convert.ToDecimal(Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.JURYO.ColNo))).Equals(0) Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"重量が0", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

        '届先コードが設定されていなければエラー
        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.DEST_CD.ColNo)).ToString()) Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"届先が設定されていない", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

        '既に纏められていたらエラー
        If Not String.IsNullOrEmpty(Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.GROUP.ColNo)).ToString()) Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"既に纏められた情報の", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

        '既に確定されていたらエラー
        If Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.KAKUTEI_FLG.ColNo)).ToString().Equals("01") Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"既に確定された情報の", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

    End Sub

    ''' <summary>
    ''' アクサルタ同送処理時の基本行と比較対象行の相関チェック
    ''' </summary>
    ''' <param name="Spread">画面の明細スプレッド</param>
    ''' <param name="BasicRowIndex">基本行</param>
    ''' <param name="rowIndex">比較対象行</param>
    ''' <returns>正常： True エラー：False </returns>
    ''' <remarks></remarks>
    Private Function MultiRowCorrelationCheck(ByVal Spread As FarPoint.Win.Spread.SheetView, ByVal BasicRowIndex As Integer, ByVal rowIndex As Integer) As Boolean
        Dim sime As String = String.Empty               '比較対象行の締め日基準
        Dim basicSime As String = String.Empty          '基本行の締め日基準
        Dim rtnResult As Boolean = True                 'True： エラーなし　False：エラー 
        '基本行の絞め日基準を取得
        basicSime = Me._LMFconG.GetCellValue(Spread.Cells(BasicRowIndex, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString
        '比較対象行の絞め日基準を取得
        sime = Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo)).ToString

        '絞め日基準チェック
        If Not basicSime.Equals(sime) Then
            rtnResult = False
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"締め日基準が異なる", "処理が継続"}, rowIndex.ToString())
        End If

        '運送日チェック
        If LMFControlC.CALC_SHUKKA.Equals(sime) Then
            '締め基準が01の場合

            '出荷日が異なっていたらエラー
            If rtnResult AndAlso Not Me._LMFconG.GetCellValue(Spread.Cells(BasicRowIndex, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString(). _
                Equals(Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.SHUKKA.ColNo)).ToString()) Then
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"出荷日が異なる", "処理が継続"}, rowIndex.ToString())
                rtnResult = False
            End If

        Else

            '納品日が異なっていたらエラー
            If rtnResult AndAlso Not Me._LMFconG.GetCellValue(Spread.Cells(BasicRowIndex, LMF040G.sprDetailDef.NONYU.ColNo)).ToString(). _
                Equals(Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.NONYU.ColNo)).ToString()) Then
                MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"納品日が異なる", "処理が継続"}, rowIndex.ToString())
                rtnResult = False
            End If

        End If

        '届先コードが異なっていたらエラー
        If Not Me._LMFconG.GetCellValue(Spread.Cells(BasicRowIndex, LMF040G.sprDetailDef.DEST_CD.ColNo)).ToString(). _
            Equals(Me._LMFconG.GetCellValue(Spread.Cells(rowIndex, LMF040G.sprDetailDef.DEST_CD.ColNo)).ToString()) Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E375", New String() {"届先が異なる", "処理が継続"}, rowIndex.ToString())
            rtnResult = False
        End If

        Return rtnResult

    End Function


    '2017/10/11 Annen アクサルタ 運賃按分計算の自動化対応 END

#End Region

#Region "チェックMethod"

    ''' <summary>
    ''' 自営業チェック
    ''' </summary>
    ''' <param name="spr">シート</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMyNrsChk(ByVal spr As FarPoint.Win.Spread.SheetView, ByVal rowNo As Integer, ByVal msg As String) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''自営業でない場合、エラー
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))) = False Then
        '    Return Me.SetIkkatuErrData("E178", New String() {Me._LMFconV.SetRepMsgData(msg)}, rowNo)
        'End If

        Return True

    End Function

    ''' <summary>
    ''' 荷主コードの同値チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="shusei">変更対象</param>
    ''' <param name="cdL">(大)コード</param>
    ''' <param name="cdM">(中)コード</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustDotiChk(ByVal frm As LMF040F, ByVal shusei As String, ByVal cdL As String, ByVal cdM As String, ByVal rowNo As Integer) As Boolean

        With frm.sprDetail.ActiveSheet

            '変更対象が荷主でない場合、スルー
            If LMF040C.SHUSEI_CUST.Equals(shusei) = False Then
                Return True
            End If

            '荷主(大)コードが違う場合、エラー
            If cdL.Equals(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_L.ColNo))) = False Then
                'START YANAI 要望番号498
                'Return Me.SetIkkatuErrData("E227", LMF040G.sprDetailDef.CUST_CD.ColName, rowNo)
                Return Me.SetIkkatuErrData("E227", New String() {LMF040C.CUST_CD}, rowNo)
                'END YANAI 要望番号498
            End If

            '荷主(中)コードが違う場合、エラー
            If cdM.Equals(Me._LMFconG.GetCellValue(.Cells(rowNo, LMF040G.sprDetailDef.CUST_CD_M.ColNo))) = False Then
                'START YANAI 要望番号498
                'Return Me.SetIkkatuErrData("E227", LMF040G.sprDetailDef.CUST_CD.ColName, rowNo)
                Return Me.SetIkkatuErrData("E227", New String() {LMF040C.CUST_CD}, rowNo)
                'END YANAI 要望番号498
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinExistChk(ByVal frm As LMF040F, ByVal rowNo As Integer) As Boolean

        With frm

            '一括変更がタリフコードでない場合、スルー
            If LMF040C.SHUSEI_TARIFF.Equals(.cmbShusei.SelectedValue.ToString()) = False Then
                Return True
            End If

            '適用開始日の設定
            Dim chkDate As String = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.SHUKKA.ColNo)))
            If LMFControlC.CALC_NYUKA.Equals(Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.UNTIN_CALCULATION_KB.ColNo))) = True Then
                chkDate = DateFormatUtility.DeleteSlash(Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NONYU.ColNo)))
            End If

            '取得できない場合、エラー
            Dim tariffCd As String = .txtShuseiL.TextValue
            Dim drs As DataRow() = Me._LMFconG.SelectUnchinTariffListDataRow(tariffCd, String.Empty, chkDate)
            If drs.Length < 1 Then
                Return Me.SetIkkatuErrData("E079", New String() {"運賃タリフマスタ", tariffCd}, rowNo)
            End If

            Return True

        End With

    End Function

    'START YANAI 要望番号996
    ''' <summary>
    ''' 割増タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsETariffExistChk(ByVal frm As LMF040F, ByVal rowNo As Integer) As Boolean

        With frm

            '一括変更が割増タリフコードでない場合、スルー
            If LMF040C.SHUSEI_ETARIFF.Equals(.cmbShusei.SelectedValue.ToString()) = False Then
                Return True
            End If

            '適用開始日の設定
            Dim nrsbrcd As String = Me._LMFconG.GetCellValue(.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))

            '取得できない場合、エラー
            Dim tariffCd As String = .txtShuseiL.TextValue
            Dim drs As DataRow() = Me._LMFconG.SelectExtcUnchinListDataRow(nrsbrcd, tariffCd, String.Empty)
            If drs.Length < 1 Then
                Return Me.SetIkkatuErrData("E079", New String() {"割増運賃タリフマスタ", tariffCd}, rowNo)
            End If

            Return True

        End With

    End Function
    'END YANAI 要望番号996

    ''' <summary>
    ''' 確定済、未チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <param name="flg">フラグ　True：確定済みの場合、エラー　False：確定未の場合、エラー</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKakuteiChk(ByVal frm As LMF040F, ByVal rowNo As Integer, ByVal flg As Boolean, ByVal msg As String) As Boolean

        If LMFControlC.FLG_ON.Equals(Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.KAKUTEI_FLG.ColNo))) = flg Then
            Return Me.SetIkkatuErrData("E237", New String() {msg}, rowNo)
        End If

        Return True

    End Function

    ''' <summary>
    ''' 運賃のゼロ円チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="rtnResult">エラーチェック結果</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>エラー行はワーニングチェックをしない</remarks>
    Private Function IsUnchinZeroChk(ByVal frm As LMF040F, ByVal rowNo As Integer, ByVal rtnResult As Boolean) As Boolean

        'エラー行の場合、スルー
        If rtnResult = False Then
            Return True
        End If

        '運賃がゼロの場合、False
        If 0 = Convert.ToDecimal(Me._LMFconG.FormatNumValue(Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.CHK_UNCHIN.ColNo)))) Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' まとめ未チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMiGroupChk(ByVal frm As LMF040F, ByVal rowNo As Integer) As Boolean

        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.GROUP.ColNo))) = True Then
            Return Me.SetIkkatuErrData("E238", rowNo)
        End If

        Return True

    End Function

    'START KIM 2012/11/21 要望番号：1400
    ''' <summary>
    ''' まとめ済チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsZumiGroupChk(ByVal frm As LMF040F, ByVal rowNo As Integer) As Boolean

        'まとめ済チェック
        If String.IsNullOrEmpty(Me._LMFconG.GetCellValue(frm.sprDetail.ActiveSheet.Cells(rowNo, LMF040G.sprDetailDef.GROUP.ColNo))) = False Then
            MyBase.SetMessageStore(LMFControlC.GUIDANCE_KBN, "E237", New String() {LMFControlC.MATOME_ZUMI}, rowNo.ToString())
            Return False
        End If

        Return True

    End Function
    'END   KIM 2012/11/21 要望番号：1400

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
    ''' 納入日 or 出荷日の必須チェック(E292)
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDateHissuChk(ByVal value As String, ByVal msg As String, ByVal rowNo As Integer) As Boolean

        '値が空の場合、エラー
        If String.IsNullOrEmpty(value) = True Then
            Return Me.SetIkkatuErrData("E292", New String() {msg}, rowNo)
        End If

        Return True

    End Function

    'START YANAI 要望番号446
    ''' <summary>
    ''' 請求先コードの必須チェック(E292)
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSeiqtoCdHissuChk(ByVal value As String, ByVal msg As String, ByVal rowNo As Integer) As Boolean

        '値が空の場合、エラー
        If String.IsNullOrEmpty(value) = True Then
            Return Me.SetIkkatuErrData("E292", New String() {msg}, rowNo)
        End If

        Return True

    End Function
    'END YANAI 要望番号446

    'START YANAI 要望番号974
    ''' <summary>
    ''' 運賃・割り増し運賃0円チェック(E260)
    ''' </summary>
    ''' <param name="msg">置換文字</param>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUnchinWariZeroChk(ByVal frm As LMF040F, ByVal msg As String, ByVal rowNo As Integer) As Boolean

        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet
        '要望管理
        'Dim seiqUnchin As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_UNCHIN.ColNo)))
        'Dim seiqcityExtc As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_CITY_EXTC.ColNo)))
        'Dim seiqwintExtc As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_WINT_EXTC.ColNo)))
        'Dim seiqrelyExtc As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_RELY_EXTC.ColNo)))
        'Dim seiqtoll As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_TOLL.ColNo)))
        'Dim seiqinsu As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.SEIQ_INSU.ColNo)))
        Dim deciUnchin As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.DECI_UNCHIN.ColNo)))
        Dim decicityExtc As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.DECI_CITY_EXTC.ColNo)))
        Dim deciwintExtc As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.DECI_WINT_EXTC.ColNo)))
        Dim decirelyExtc As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.DECI_RELY_EXTC.ColNo)))
        Dim decitoll As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.DECI_TOLL.ColNo)))
        Dim deciinsu As Decimal = Convert.ToDecimal(Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.DECI_INSU.ColNo)))

        ''請求運賃が0円で、割り増しのいずれかが一つでも0円以外がある場合はエラー
        'If seiqUnchin = 0 AndAlso _
        '    (seiqcityExtc <> 0 OrElse _
        '     seiqwintExtc <> 0 OrElse _
        '     seiqrelyExtc <> 0 OrElse _
        '     seiqtoll <> 0 OrElse _
        '     seiqinsu <> 0) Then
        '    Return Me.SetIkkatuErrData("E260", New String() {msg}, rowNo)
        'End If

        '確定運賃が0円で、割り増しのいずれかが一つでも0円以外がある場合はエラー
        If deciUnchin = 0 AndAlso _
            (decicityExtc <> 0 OrElse _
             deciwintExtc <> 0 OrElse _
             decirelyExtc <> 0 OrElse _
             decitoll <> 0 OrElse _
             deciinsu <> 0) Then
            Return Me.SetIkkatuErrData("E260", New String() {msg}, rowNo)
        End If

        Return True

    End Function
    'END YANAI 要望番号974

    ''' <summary>
    ''' 運賃タリフマスタの承認チェック
    ''' </summary>
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsApprovalChk(ByVal frm As LMF040F, ByVal rowNo As Integer) As Boolean

        Dim spr As FarPoint.Win.Spread.SheetView = frm.sprDetail.ActiveSheet

        Dim chkDs As DataSet = New LMF040DS
        Dim chkDt As DataTable = chkDs.Tables("CHK_APPROVAL")
        Dim dr As DataRow = chkDt.NewRow()
        dr.Item("NRS_BR_CD") = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.NRS_BR_CD.ColNo))
        dr.Item("UNCHIN_TARIFF_CD") = Me._LMFconG.GetCellValue(spr.Cells(rowNo, LMF040G.sprDetailDef.TARIFF_CD.ColNo))
        chkDt.Rows.Add(dr)

        'データの取得
        chkDs = MyBase.CallWSA("LMF040BLF", "SelectChkApproval", chkDs)

        If MyBase.IsMessageExist() = True Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm)
            Return False
        End If

        '取得できたデータはエラー
        If chkDs.Tables("CHK_APPROVAL").Rows.Count > 0 Then
            Return Me.SetIkkatuErrData("E999", New String() {"承認されていない運賃タリフがあります。運賃タリフマスタ"}, rowNo)
        End If

        Return True

    End Function

#End Region

#End Region

#End Region

#Region "イベント振分け"

    'START YANAI 要望番号582
    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.PrintAciton(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'END YANAI 要望番号582

    'START YANAI 要望番号561
    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.LoopEditAciton(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'END YANAI 要望番号561

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.FixDataAciton(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.FixCancellDataAciton(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.GroupingDataAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.GroupingCancellDataAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    'START YANAI 20120622 DIC運賃まとめ及び再計算対応
    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.Silica(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub
    'END YANAI 20120622 DIC運賃まとめ及び再計算対応

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_CellDoubleClick(ByVal frm As LMF040F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.RowSelection(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' まとめ候補値変更時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbGroup_SelectedValueChanged(ByVal frm As LMF040F, ByVal e As System.EventArgs)

        Call Me.GroupChangeAction(frm)

    End Sub

    ''' <summary>
    ''' 変更項目の値変更時に発生するイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbShusei_SelectedValueChanged(ByVal frm As LMF040F, ByVal e As System.EventArgs)

        Call Me.HenkoChangeAction(frm)

    End Sub

    ''' <summary>
    ''' ボタンクリックイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub btnHenko_Click(ByVal frm As LMF040F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.SaveUnchinItemData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    ''' <summary>
    ''' フォームのキーダウンイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub LMF040F_KeyDown(ByVal frm As LMF040F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class
