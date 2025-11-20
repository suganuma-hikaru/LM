' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG030H : 保管料荷役料明細編集
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner
Imports Jp.Co.Nrs.Win.Base      '2021/06/28 
Imports Jp.Co.Nrs.LM.Utility    '2021/06/28 

''' <summary>
''' LMG030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMG030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG030G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGConV As LMGControlV

    '''' <summary>
    '''' Handler共通クラスを格納するフィールド
    '''' </summary>
    '''' <remarks></remarks>
    Private _LMGConH As LMGControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGConG As LMGControlG

    '画面間データを取得する
    Dim prmDs As DataSet

    ''' <summary>
    ''' データを一時的に保管する
    ''' </summary>
    ''' <remarks></remarks>
    Private SekiNb1Bk As Decimal = 0
    Private SekiNb2Bk As Decimal = 0
    Private SekiNb3Bk As Decimal = 0
    Private HokanTnk1Bk As Decimal = 0
    Private HokanTnk2Bk As Decimal = 0
    Private HokanTnk3Bk As Decimal = 0

    ''' <summary>
    ''' 変動保管料フラグ
    ''' </summary>
    ''' <remarks>0:適用しない　1:適用する</remarks>
    Private _varStrageFlg As String = "0"

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
        Dim frm As LMG030F = New LMG030F(Me)

        Dim ds As DataSet = prmDs

        '画面共通クラスの設定
        Me._LMGConG = New LMGControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._LMGConV = New LMGControlV(Me, DirectCast(frm, Form))

        'Validateクラスの設定
        'START YANAI 20111014 一括変更追加
        'Me._V = New LMG030V(Me, frm, Me._LMGConV)
        'ハンドラー共通クラスの設定
        Me._LMGConH = New LMGControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMGConV, Me._LMGConG)

        Me._V = New LMG030V(Me, frm, Me._LMGConV, Me._LMGConH)
        'END YANAI 20111014 一括変更追加

        'Gamenクラスの設定
        Me._G = New LMG030G(Me, frm, Me._LMGConG)

        'START YANAI 20111014 一括変更追加
        ''ハンドラー共通クラスの設定
        'Me._LMGConH = New LMGControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMGConV, Me._LMGConG)
        'END YANAI 20111014 一括変更追加

        'EnterKey判定用
        frm.KeyPreview = True

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'フォームの初期化
        Call Me.InitControl(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID(), prmDs)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        Me._G.SetInitValue(frm)

        '↓ データ取得の必要があればここにコーディングする。

        '==========================
        'WSAクラス呼出
        '==========================
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        Call Me.GetSekyMeisai(frm, ds)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '↑ データ取得の必要があればここにコーディングする。

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(True, _varStrageFlg)

        '画面の入力項目の制御
        Call _G.SetControlsStatus(False)

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        '2011/08/02 菱刈 検証結果一覧 No1 スタート
        'frm.ShowDialog()
        frm.Show()
        '2011/08/02 菱刈 検証結果一覧 No1 エンド
        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListData(ByVal frm As LMG030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'DoubleClick行判定
        If e.Row.Equals(-1) = True _
        Or e.Row.Equals(0) = True Then
            Exit Sub
        End If

        '編集モード判定処理
        If frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._LMGConH.EndAction(frm) '終了処理
                Exit Sub
            End If
        End If

        '権限チェック
        If Me._V.IsAuthorityChk(LMG030C.EventShubetsu.DOUBLECLICK) = False Then
            Call Me._LMGConH.EndAction(frm) '終了処理
            Exit Sub
        End If

        '画面ロック
        MyBase.LockedControls(frm)

        '2011/08/16 菱刈 排他チェックを削除 スタート
        '2011/08/16 菱刈 排他チェックを削除 エンド

        'シチュエーションラベルの設定
        Call Me._G.SetModeAndStatus()

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(True, _varStrageFlg)

        '2011/08/16 菱刈 RowNoの取得 スタート
        Dim Row As Integer = e.Row
        '2011/08/16 菱刈 RowNoの取得 エンド

        '2011/08/16 菱刈 RowNoの取得 スタート
        'START YANAI 20111014 ロック時ReadOnly
        ''選択されたスプレッド行のデータをフォームに設定する。
        'Call Me._G.SetControlSpreadData(Row, Me._LMGConV)
        'END YANAI 20111014 ロック時ReadOnly

        'START YANAI 20111014 ロック時ReadOnly
        ''入力項目の設定
        'Call Me._G.SetControlsStatus(False)
        'END YANAI 20111014 ロック時ReadOnly

        '終了アクション
        Call Me._LMGConH.EndAction(frm)

        '全画面ロック解除
        MyBase.UnLockedControls(frm)

        'START YANAI 20111014 ロック時ReadOnly
        '入力項目の設定
        Call Me._G.SetControlsStatus(False)
        'END YANAI 20111014 ロック時ReadOnly

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub EditDataEvent(ByVal frm As LMG030F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG030C.EventShubetsu.HENSHU) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '排他データ設定
        Call Me.CheckHaita(frm)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '排他チェック処理
        MyBase.CallWSA("LMG030BLF", "CheckHaita", prmDs)

        'エラーの判定
        If MyBase.IsMessageExist() = False Then

            'シチュエーションラベルの設定
            Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

            'START YANAI 20111014 ロック時ReadOnly
            ''入力項目の設定
            'Call Me._G.SetControlsStatus(True)
            'END YANAI 20111014 ロック時ReadOnly

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey(False, _varStrageFlg)

            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G003")

            'START YANAI 20111014 ロック時ReadOnly
            '終了アクション
            Call Me._LMGConH.EndAction(frm)

            '全画面ロック解除
            MyBase.UnLockedControls(frm)

            '入力項目の設定
            Call Me._G.SetControlsStatus(True)
            'END YANAI 20111014 ロック時ReadOnly

        Else
            'メッセージの表示
            MyBase.ShowMessage(frm)

            'START YANAI 20111014 ロック時ReadOnly
            '終了アクション
            Call Me._LMGConH.EndAction(frm)

            '全画面ロック解除
            MyBase.UnLockedControls(frm)
            'END YANAI 20111014 ロック時ReadOnly
        End If

        'START YANAI 20111014 ロック時ReadOnly
        ''終了アクション
        'Call Me._LMGConH.EndAction(frm)

        ''全画面ロック解除
        'MyBase.UnLockedControls(frm)
        'END YANAI 20111014 ロック時ReadOnly

    End Sub

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub Acquisithion(ByVal frm As LMG030F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG030C.EventShubetsu.TORIKOMI) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '2011/08/02 菱刈 検証結果一覧 No4 確認メッセージの追加 スタート
        '確認ポップアップ
        If MyBase.ShowMessage(frm, "W152", New String() {"取込"}) <> MsgBoxResult.Ok Then

            Call Me._LMGConH.EndAction(frm) '終了処理
            MyBase.UnLockedControls(frm)    '全画面ロック解除
            Exit Sub
        End If
        '2011/08/02 菱刈 検証結果一覧 No4 確認メッセージの追加 エンド
        '存在チェック用データセット
        Dim ds As DataSet = SetAcquisithionHaita(frm)

        '存在チェック処理
        MyBase.CallWSA("LMG030BLF", "AcquisithionHaita", ds)

        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)                 'メッセージの表示
            Call Me._LMGConH.EndAction(frm)         '終了アクション
            MyBase.UnLockedControls(frm)            '全画面ロック解除
            Exit Sub
        End If

        'データセット   
        Call Me.setBatchParm(frm)

        'バッチ呼出処理
        MyBase.CallWSA("LMG030BLF", "Acquisithion", prmDs)

        'エラーの判定
        If MyBase.IsMessageExist() = False Then

            '再描画処理
            Call Me.GetSekyMeisai(frm, Me.ReSelect(frm))

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey(True, _varStrageFlg)

            '画面の入力項目の制御
            Call _G.SetControlsStatus(False)

            MyBase.ShowMessage(frm, "G002", New String() {"取込処理", ""})
        Else
            'メッセージの表示
            MyBase.ShowMessage(frm)

        End If

        '終了アクション
        Call Me._LMGConH.EndAction(frm)

        '全画面ロック解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMG030F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG030C.EventShubetsu.KENSAKU) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        'データセット
        Call Me.SetSelectData(frm)

        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet
        rtnDs = Me._LMGConH.CallWSAAction(DirectCast(frm, Form), _
                                         "LMG030BLF", "SelectListData", prmDs _
                                 , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                 (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            'データテーブルの取得
            Dim dtCount As String = rtnDs.Tables(LMG030C.TABLE_NM_OUT).Rows.Count.ToString()
            If "0".Equals(dtCount) = False Then

                'スプレッドデータをクリアする
                frm.sprMeisaiPrt.CrearSpread()

                '取得データをスプレッドに反映
                Call Me._G.SetSelectListData(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G008", New String() {dtCount})
            Else
                'スプレッドデータをクリアする
                frm.sprMeisaiPrt.CrearSpread()

                'メッセージの表示
                MyBase.ShowMessage(frm, "G001")

            End If
        End If

        '編集部クリア
        Call Me._G.ClearFormData(frm)

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        'シチュエーションラベルの設定
        Call Me._G.SetModeAndStatus()

        '入力項目の設定
        Call Me._G.SetControlsStatus(False)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(True, _varStrageFlg)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveItemData(ByVal frm As LMG030F, ByVal controlNm As String) As Boolean

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG030C.EventShubetsu.SAVE) = False Then
            Exit Function
        End If

        '選択フォーム位置チェック（保管料再計算）
        Call Me.FocusCheck(frm, controlNm)

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '2011/08/04 菱刈 保存時の確認メッセージ削除 スタート
        '確認ポップアップ
        'If MyBase.ShowMessage(frm, "W003") <> MsgBoxResult.Ok Then
        '    Call Me._LMGConH.EndAction(frm) '終了処理
        '    MyBase.UnLockedControls(frm)    '全画面ロック解除
        '    Exit Function
        'End If
        '2011/08/04 菱刈 保存時の確認メッセージ削除 エンド

        '排他データ設定
        Call Me.CheckHaita(frm)
        MyBase.Logger.StartLog(MyBase.GetType.Name, "CheckHaita")

        '排他チェック処理
        MyBase.CallWSA("LMG030BLF", "CheckHaita", prmDs)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CheckHaita")

        'エラーの判定
        If MyBase.IsMessageExist() = False Then

            '更新用データ設定
            Call Me.UpDateDataSet(frm)

            '更新処理
            MyBase.CallWSA("LMG030BLF", "UpDateTable", prmDs)
            If MyBase.IsMessageExist() = True Then

                'メッセージの表示
                MyBase.ShowMessage(frm)

                '終了アクション
                Call Me._LMGConH.EndAction(frm)

                '全画面ロック解除
                MyBase.UnLockedControls(frm)

                Return False
            End If

            'シチュエーションラベルの設定
            Call Me._G.SetModeAndStatus(DispMode.VIEW, RecordStatus.NOMAL_REC)

            'ファンクションキーの設定
            Call Me._G.SetFunctionKey(True, _varStrageFlg)

            'START YANAI 20111014 ロック時ReadOnly
            ''画面の入力項目の制御
            'Call _G.SetControlsStatus(False)
            'END YANAI 20111014 ロック時ReadOnly

            'メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {"保存処理", ""})

            '終了アクション
            Call Me._LMGConH.EndAction(frm)

            '全画面ロック解除
            MyBase.UnLockedControls(frm)

            'START YANAI 20111014 ロック時ReadOnly
            '画面の入力項目の制御
            Call _G.SetControlsStatus(False)
            'END YANAI 20111014 ロック時ReadOnly

            Return True
        Else

            'メッセージの表示
            MyBase.ShowMessage(frm)

            '終了アクション
            Call Me._LMGConH.EndAction(frm)

            '全画面ロック解除
            MyBase.UnLockedControls(frm)

            Return False
        End If

    End Function

    ''' <summary>
    ''' 印刷ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub PrintBtnClick(ByVal frm As LMG030F)

        ''印刷処理　TODO
        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG030C.EventShubetsu.PRINT) = False Then
            'エンドアクション
            Me._LMGConH.EndAction(frm)
            Exit Sub
        End If

        'スタートアクション
        Me._LMGConH.StartAction(frm)

        'コントロールロック処理
        MyBase.LockedControls(frm)

        '印刷処理を行う。
        Call Me.Print(frm)

        '全画面ロック解除
        MyBase.UnLockedControls(frm)

        '終了処理
        Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Print(ByVal frm As LMG030F) As DataSet

        With frm
            Dim ds As DataSet = New LMG500DS
            Dim dt As DataTable = ds.Tables("LMG500IN")
            Dim dr As DataRow = dt.NewRow

            dr("NRS_BR_CD") = .lblNrsBrCd.TextValue
            dr("TANTO_USER_FLG") = "0"
            dr("JOB_NO") = .lblJobNo.TextValue
            '2011/08/04 菱刈 プレビュー表示、印刷部数の設定 スタート
            'プレビューにチェックがついていたら1を設定
            Dim flgP As String = String.Empty
            If frm.chkMeisaiPrev.Checked = True Then
                'プレビューを表示する。
                flgP = "1"
            Else
                flgP = ""

            End If
            dr("PREVIEW_FLG") = flgP
            '2011/08/04 菱刈 プレビュー表示、印刷部数の設定 エンド

            'START YANAI 要望番号581
            dr("SPREAD_GYO_CNT") = String.Empty
            dr("FROM_PGID") = MyBase.GetPGID
            'END YANAI 要望番号581

            ds.Tables("LMG500IN").Rows.Add(dr)
            ds.Merge(New RdPrevInfoDS)

            '印刷のBLFへ
            Dim blf As String = String.Concat(MyBase.GetPGID(), LMControlC.BLF)


            Dim rtnDs As DataSet = MyBase.CallWSA(blf, "Print", ds)

            'メッセージ判定
            If IsMessageExist() = True Then

                'エラーメッセージ判定
                If MyBase.IsErrorMessageExist() = False Then


                    ''処理終了アクション
                    '印刷処理でエラーメッセージあったらメッセージを表示してG007を設定
                    '2011/08/02 菱刈 メッセージの変更 スタート
                    'MyBase.ShowMessage(frm)
                    MyBase.ShowMessage(frm, "S001", New String() {"印刷"})
                    '2011/08/02 菱刈 メッセージの変更 エンド
                    Return ds

                End If

                'START YANAI 要望番号581
                MyBase.ShowMessage(frm)
                Return ds
                'END YANAI 要望番号581

            End If

            'プレビュー判定 
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

            '終了メッセージ表示
            MyBase.SetMessage("G002", New String() {"印刷", ""})

            MyBase.ShowMessage(frm)

            Return ds

        End With

    End Function

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 一括変更ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub IkkatuBtnClick(ByVal frm As LMG030F)

        '権限・入力チェック
        If Me.IsCheckCall(frm, LMG030C.EventShubetsu.IKKATU) = False Then
            'エンドアクション
            Me._LMGConH.EndAction(frm)
            Exit Sub
        End If

        'スタートアクション
        Me._LMGConH.StartAction(frm)

        'コントロールロック処理
        MyBase.LockedControls(frm)

        '一括変更処理を行う。
        Call Me.Ikkatu(frm)

        '全画面ロック解除
        MyBase.UnLockedControls(frm)

        '終了処理
        Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub Ikkatu(ByVal frm As LMG030F)

        '確認メッセージ
        If MyBase.ShowMessage(frm, "C001", New String() {"一括変更"}) <> MsgBoxResult.Ok Then
            Exit Sub
        End If

        'INデータセット設定
        Call Me.SetIkkatuDataSet(frm)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '更新処理
        MyBase.CallWSA("LMG030BLF", "IkkatuUpDateTable", prmDs)
        If MyBase.IsMessageStoreExist() = True Then
            'EXCEL起動 
            MyBase.MessageStoreDownload(True)
            MyBase.ShowMessage(frm, "E235")
        Else
            MyBase.ShowMessage(frm, "G002", New String() {"一括変更", ""})

        End If

    End Sub

    'END YANAI 20111014 一括変更追加

    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprCellLeave(ByVal frm As LMG030F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        Dim rowNo As Integer = e.NewRow
        If rowNo < 1 Then
            Exit Sub
        End If

        '同じ行の場合、スルー
        If e.Row = rowNo Then
            Exit Sub
        End If

        Call Me.RowSelection(frm, rowNo)

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal frm As LMG030F, ByVal rowNo As Integer)

      
        '権限チェック
        If Me._V.IsAuthorityChk(LMG030C.EventShubetsu.DOUBLECLICK) = False Then
            Call Me._LMGConH.EndAction(frm) '終了処理
            Exit Sub
        End If

      
        '選択されたスプレッド行のデータをフォームに設定する。
        Call Me._G.SetControlSpreadData(rowNo, Me._LMGConV)

        '入力項目の設定
        Call Me._G.SetControlsStatus(False)

      
    End Sub

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' CellLeaveイベント処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub cmbIkkatuSelected(ByVal frm As LMG030F, ByVal e As System.EventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then
            Exit Sub
        End If

        '入力項目の設定
        Call Me._G.ClearFormIkkatuData(frm)

    End Sub
    'END YANAI 20111014 一括変更追加

#End Region 'イベント定義(一覧)

#Region "内部処理"

    ''' <summary>
    ''' 保管荷役料明細印刷テーブル取得処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSekyMeisai(ByVal frm As LMG030F, ByVal ds As DataSet)

        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMGConH.CallWSAAction(DirectCast(frm, Form), _
                                                 "LMG030BLF", "SelectListData", ds _
                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            '変動保管料フラグの取得
            _varStrageFlg = "0"
            If rtnDs.Tables(LMG030C.TABLE_NM_OUT_VAR_STRAGE).Rows.Count > 0 Then
                _varStrageFlg = rtnDs.Tables(LMG030C.TABLE_NM_OUT_VAR_STRAGE).Rows(0).Item("VAR_STRAGE_FLG").ToString()
            End If

            'データテーブルの取得
            Dim dtCount As String = rtnDs.Tables(LMG030C.TABLE_NM_OUT).Rows.Count.ToString()
            If "0".Equals(dtCount) = False Then

                'スプレッドデータをクリアする
                frm.sprMeisaiPrt.CrearSpread()

                '取得データをスプレッドに反映
                Call Me._G.SetSelectListData(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G008", New String() {dtCount})
            Else
                'スプレッドデータをクリアする
                frm.sprMeisaiPrt.CrearSpread()
                MyBase.ShowMessage(frm, "E078", New String() {"保管荷役料明細印刷テーブル"})
            End If
        End If

        'シチュエーションラベルの設定
        Call Me._G.SetModeAndStatus()

    End Sub

    ''' <summary>
    ''' 入力・権限チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="SHUBETSU"></param>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMG030F, ByVal SHUBETSU As LMG030C.EventShubetsu) As Boolean

        'フォームの背景色を初期化する
        Me._G.SetBackColor(frm)

        '背景色クリア
        Me._LMGConG.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(SHUBETSU) = False Then
            Return False
        End If

        '印刷処理の場合、単項目チェックを行う
        If LMG030C.EventShubetsu.PRINT.Equals(SHUBETSU) = True Then
            If Me._V.IsInputCheck(SHUBETSU) = False Then
                Return False
            End If
        End If

        '検索処理の場合、スプレッド項目チェックを行う
        If LMG030C.EventShubetsu.KENSAKU.Equals(SHUBETSU) = True Then
            If Me._V.IsSpreadInputChk() = False Then
                Return False
            End If
        End If

        '保存処理の場合、編集部項目チェックを行う
        If LMG030C.EventShubetsu.SAVE.Equals(SHUBETSU) = True Then
            If Me._V.IsSaveDataCheck() = False Then
                Return False
            End If
        End If

        '編集・取込イベントの場合、関連項目チェックを行う
        Select Case SHUBETSU
            Case LMG030C.EventShubetsu.HENSHU, LMG030C.EventShubetsu.TORIKOMI
                If Me._V.isRelationCheck(SHUBETSU) = False Then
                    Return False
                End If
        End Select

        'START YANAI 20111014 一括変更追加
        '一括処理の場合、単項目チェックを行う
        If LMG030C.EventShubetsu.IKKATU.Equals(SHUBETSU) = True Then
            If Me._V.IsIkkatuCheck(SHUBETSU) = False Then
                Return False
            End If
        End If
        'END YANAI 20111014 一括変更追加

        Return True

    End Function

    'START YANAI 20111013 保管料自動計算廃止
    '''' <summary>
    '''' FocusInイベント
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Private Sub SetVariableDicision(ByVal frm As LMG030F)

    '    'データの退避を行う
    '    Me.SekiNb1Bk = Convert.ToDecimal(frm.numSekiNb1.Value)
    '    Me.SekiNb2Bk = Convert.ToDecimal(frm.numSekiNb2.Value)
    '    Me.SekiNb3Bk = Convert.ToDecimal(frm.numSekiNb3.Value)

    '    Me.HokanTnk1Bk = Convert.ToDecimal(frm.numHokanTnk1.Value)
    '    Me.HokanTnk2Bk = Convert.ToDecimal(frm.numHokanTnk2.Value)
    '    Me.HokanTnk3Bk = Convert.ToDecimal(frm.numHokanTnk3.Value)

    'End Sub
    'END YANAI 20111013 保管料自動計算廃止

    'START YANAI 20111013 保管料自動計算廃止
    '''' <summary>
    '''' FocusOutイベント
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Private Sub Recalculation(ByVal frm As LMG030F)

    '    Dim HokanAmt As Decimal = 0
    '    Dim SekiNb1 As Decimal = Convert.ToDecimal(frm.numSekiNb1.Value)
    '    Dim SekiNb2 As Decimal = Convert.ToDecimal(frm.numSekiNb2.Value)
    '    Dim SekiNb3 As Decimal = Convert.ToDecimal(frm.numSekiNb3.Value)
    '    Dim HokanTnk1 As Decimal = Convert.ToDecimal(frm.numHokanTnk1.Value)
    '    Dim HokanTnk2 As Decimal = Convert.ToDecimal(frm.numHokanTnk2.Value)
    '    Dim HokanTnk3 As Decimal = Convert.ToDecimal(frm.numHokanTnk3.Value)

    '    '金額差異判定
    '    If Me.SekiNb1Bk.Equals(SekiNb1) = True _
    '    AndAlso Me.SekiNb2Bk.Equals(SekiNb2) = True _
    '    AndAlso Me.SekiNb3Bk.Equals(SekiNb3) = True _
    '    AndAlso Me.HokanTnk1Bk.Equals(HokanTnk1) = True _
    '    AndAlso Me.HokanTnk2Bk.Equals(HokanTnk2) = True _
    '    AndAlso Me.HokanTnk3Bk.Equals(HokanTnk3) = True Then

    '        '差異がない場合、処理を終了
    '        Exit Sub

    '    Else

    '        '差異がある場合、保管料の金額を再計算する
    '        HokanAmt = (SekiNb1 * HokanTnk1) + (SekiNb2 * HokanTnk2) + (SekiNb3 * HokanTnk3)

    '        '保管料を四捨五入し再設定する
    '        frm.lblHokanAmt.Value = Convert.ToString(Me._LMGConH.ToHalfAdjust(HokanAmt, 0))

    '    End If

    'End Sub
    'END YANAI 20111013 保管料自動計算廃止

    ''' <summary>
    ''' 保存処理時再計算チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Private Sub FocusCheck(ByVal frm As LMG030F, ByVal controlNm As String)

        '保管料（積数・単価）にフォーカスが存在する場合
        With frm
            Select Case controlNm
                Case .numHokanTnk1.Name, .numHokanTnk2.Name, .numHokanTnk3.Name _
                , .numSekiNb1.Name, .numSekiNb2.Name, .numSekiNb3.Name

                    'START YANAI 20111013 保管料自動計算廃止
                    ''再計算処理
                    'Recalculation(frm)
                    'END YANAI 20111013 保管料自動計算廃止

            End Select
        End With

    End Sub

    ''' <summary>
    ''' 保存イベント（閉じる）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CloseSaveEvent(ByVal frm As LMG030F, ByVal e As FormClosingEventArgs, ByVal controlNm As String) As Boolean

        'ディスプレイモードが編集の場合
        If DispMode.EDIT.Equals(frm.lblSituation.DispMode) = True Then

            'メッセージ表示
            Select Case MyBase.ShowMessage(frm, "W002")
                Case MsgBoxResult.Yes  '「はい」押下時
                    If Me.SaveItemData(frm, controlNm) = True Then
                        Return True
                    Else '保存失敗時
                        e.Cancel = True
                        Return False
                    End If

                Case MsgBoxResult.No   '「いいえ」押下時
                    Return True
                Case Else                   '「キャンセル」押下時
                    e.Cancel = True
                    Return False
            End Select
        End If
        Return True

    End Function

#End Region

#Region "DataSet"

    ''' <summary>
    ''' 検索用データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetSelectData(ByVal frm As LMG030F)

        'データテーブル
        Me.prmDs = New LMG030DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG030C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm

            dr.Item("NRS_BR_CD") = .lblNrsBrCd.TextValue
            dr.Item("JOB_NO") = .lblJobNo.TextValue

        End With

        'ベトナム対応
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        dr.Item("LANG_FLG") = lgm.MessageLanguage()

        'スプレッド入力データ取得
        With frm.sprMeisaiPrt.ActiveSheet

            dr.Item("GOODS_CD_CUST") = Me._LMGConV.GetCellValue(.Cells(0, LMG030G.sprMeisaiPrtDef.CUST_GOODS_CD.ColNo))
            dr.Item("GOODS_NM_1") = Me._LMGConV.GetCellValue(.Cells(0, LMG030G.sprMeisaiPrtDef.CUST_GOODS_NM.ColNo))
            dr.Item("LOT_NO") = Me._LMGConV.GetCellValue(.Cells(0, LMG030G.sprMeisaiPrtDef.LOT_NO.ColNo))
            dr.Item("SERIAL_NO") = Me._LMGConV.GetCellValue(.Cells(0, LMG030G.sprMeisaiPrtDef.SERIAL_NO.ColNo))
            dr.Item("GOODS_CD_NRS") = Me._LMGConV.GetCellValue(.Cells(0, LMG030G.sprMeisaiPrtDef.NRS_GOODS.ColNo))
            dr.Item("CUST_NM_S_SS") = Me._LMGConV.GetCellValue(.Cells(0, LMG030G.sprMeisaiPrtDef.CUST_NM_S_SS.ColNo))
        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 再描画用データセット
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ReSelect(ByVal frm As LMG030F) As DataSet

        Dim prmDs As DataSet = New LMG030DS()
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim datatable As DataTable = prmDs.Tables(LMG030C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm

            dr.Item("JOB_NO") = .lblJobNo.TextValue
            dr.Item("NRS_BR_CD") = .lblNrsBrCd.TextValue

        End With

        'データの設定
        datatable.Rows.Add(dr)

        Return prmDs

    End Function

    ''' <summary>
    ''' 更新用データ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub UpDateDataSet(ByVal frm As LMG030F)

        'データテーブル
        prmDs = New LMG030DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG030C.TABLE_NM_IN_UPDATE)
        Dim dr As DataRow = datatable.NewRow()
        Dim HandlingAmoTtl As String = String.Empty
        With frm
            '2011/08/02 菱刈 検証結果一覧No6 スタート
            '既存のコメント化
            '荷役量合計金額
            ' HandlingAmoTtl = Convert.ToString(Convert.ToDecimal(.numInNb.TextValue) _
            '+ Convert.ToDecimal(.numOutNb.TextValue))

            dr.Item("NRS_BR_CD") = Trim(.lblNrsBrCd.TextValue.ToString())
            dr.Item("JOB_NO") = Trim(.lblJobNo.TextValue.ToString())
            dr.Item("CTL_NO") = Trim(.lblCtlNo.TextValue.ToString())
            dr.Item("SEKI_ARI_NB1") = Trim(.numSekiNb1.TextValue.ToString())
            dr.Item("SEKI_ARI_NB2") = Trim(.numSekiNb2.TextValue.ToString())
            dr.Item("SEKI_ARI_NB3") = Trim(.numSekiNb3.TextValue.ToString())
            dr.Item("STORAGE1") = Trim(.numHokanTnk1.TextValue.ToString())
            dr.Item("STORAGE2") = Trim(.numHokanTnk2.TextValue.ToString())
            dr.Item("STORAGE3") = Trim(.numHokanTnk3.TextValue.ToString())
            'START YANAI 20111013 保管料自動計算廃止
            'dr.Item("STORAGE_AMO_TTL") = Trim(.lblHokanAmt.TextValue.ToString())
            dr.Item("STRAGE_HENDO_NASHI_AMO_TTL") = Trim(.numHokanAmt.TextValue.ToString())
            'END YANAI 20111013 保管料自動計算廃止
            dr.Item("STORAGE_AMO_TTL") = Trim(.numVarHokanAmt.TextValue.ToString())
            dr.Item("INKO_NB_TTL1") = Trim(.numInNb.TextValue.ToString())
            dr.Item("HANDLING_IN1") = Trim(.numNiyakuInTnk1.TextValue.ToString())
            dr.Item("HANDLING_IN2") = Trim(.numNiyakuInTnk2.TextValue.ToString())
            dr.Item("HANDLING_IN3") = Trim(.numNiyakuInTnk3.TextValue.ToString())
            dr.Item("OUTKO_NB_TTL1") = Trim(.numOutNb.TextValue.ToString())
            dr.Item("HANDLING_OUT1") = Trim(.numNiyakuOutTnk1.TextValue.ToString())
            dr.Item("HANDLING_OUT2") = Trim(.numNiyakuOutTnk2.TextValue.ToString())
            dr.Item("HANDLING_OUT3") = Trim(.numNiyakuOutTnk3.TextValue.ToString())
            '荷役料の合計を計算結果ではなく、画面の値を設定
            ' dr.Item("HANDLING_AMO_TTL") = Trim(HandlingAmoTtl)
            dr.Item("HANDLING_AMO_TTL") = Trim(.numNiyakuAmt.TextValue.ToString())
            '2011/08/02 菱刈 検証結果一覧No6 エンド
            dr.Item("SYS_UPD_DATE_PRT") = Trim(.lblSysUpdDate.TextValue.ToString())
            dr.Item("SYS_UPD_TIME_PRT") = Trim(.lblSysUpdTime.TextValue.ToString())

        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 排他処理（フォーム）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CheckHaita(ByVal frm As LMG030F)

        'データテーブル
        prmDs = New LMG030DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG030C.TABLE_NM_IN_UPDATE)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm

            dr.Item("NRS_BR_CD") = .lblNrsBrCd.TextValue.ToString()
            dr.Item("JOB_NO") = .lblJobNo.TextValue.ToString()
            dr.Item("CTL_NO") = .lblCtlNo.TextValue.ToString()
            dr.Item("SYS_UPD_DATE_PRT") = .lblSysUpdDate.TextValue.ToString()
            dr.Item("SYS_UPD_TIME_PRT") = .lblSysUpdTime.TextValue.ToString()

        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 排他処理（スプレッド）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CheckHaitaspr(ByVal frm As LMG030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'データテーブル
        prmDs = New LMG030DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG030C.TABLE_NM_IN_UPDATE)
        Dim dr As DataRow = datatable.NewRow()
        Dim Row As Integer = e.Row

        'スプレッドデータ取得
        With frm.sprMeisaiPrt.ActiveSheet

            dr.Item("NRS_BR_CD") = frm.lblNrsBrCd.TextValue.ToString()
            dr.Item("JOB_NO") = frm.lblJobNo.TextValue.ToString()
            dr.Item("CTL_NO") = Me._LMGConV.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.CTL_NO.ColNo))
            dr.Item("SYS_UPD_DATE_PRT") = Me._LMGConV.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SYS_UPD_DATE.ColNo))
            dr.Item("SYS_UPD_TIME_PRT") = Me._LMGConV.GetCellValue(.Cells(Row, LMG030G.sprMeisaiPrtDef.SYS_UPD_TIME.ColNo))

        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 取込処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub setBatchParm(ByVal frm As LMG030F)

        'データテーブル
        prmDs = New LMG030DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG030C.TABLE_NM_BATCH)
        Dim dr As DataRow = datatable.NewRow()
        Dim HandlingAmoTtl As String = String.Empty

        'フォーム入力データ取得
        With frm

            dr.Item("IN_JOB_NO") = Trim(.lblJobNo.TextValue.ToString())
            dr.Item("IN_NRS_BR_CD") = Trim(.lblNrsBrCd.TextValue.ToString())
            dr.Item("IN_SEKY_FLG") = "00"
            dr.Item("IN_OPE_USER_CD") = LMUserInfoManager.GetUserID()

        End With

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 請求元在庫存在チェック用
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetAcquisithionHaita(ByVal frm As LMG030F) As DataSet

        Dim ds As DataSet = New LMG030DS

        Dim dt As DataTable = ds.Tables(LMG030C.TABLE_NM_BATCH_HAITA)

        Dim dr As DataRow = dt.NewRow()
        With frm
            dr.Item("NRS_BR_CD") = .lblNrsBrCd.TextValue.ToString()
            dr.Item("JOB_NO") = .lblJobNo.TextValue.ToString()
            dr.Item("INV_DATE_TO") = .lblInvDateTo.TextValue.ToString()
        End With
        dt.Rows.Add(dr)
        Return ds

    End Function

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetIkkatuDataSet(ByVal frm As LMG030F)

        'データテーブル
        prmDs = New LMG030DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG030C.TABLE_NM_IN_IKKATU)
        Dim dr As DataRow = datatable.NewRow()
        Dim arr As ArrayList = Nothing
        arr = Me._LMGConH.GetCheckList(frm.sprMeisaiPrt.ActiveSheet, LMG030C.SprColumnIndex.DEF)
        Dim max As Integer = arr.Count - 1

        'フォーム入力データ取得
        With frm.sprMeisaiPrt.ActiveSheet

            For i As Integer = 0 To max

                dr = datatable.NewRow()

                dr.Item("NRS_BR_CD") = frm.lblNrsBrCd.TextValue.ToString()
                dr.Item("JOB_NO") = frm.lblJobNo.TextValue.ToString()
                dr.Item("CTL_NO") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.CTL_NO.ColNo))
                dr.Item("STORAGE1") = String.Empty
                dr.Item("SEKI_ARI_NB1") = String.Empty
                dr.Item("STORAGE2") = String.Empty
                dr.Item("SEKI_ARI_NB2") = String.Empty
                dr.Item("STORAGE3") = String.Empty
                dr.Item("SEKI_ARI_NB3") = String.Empty
                dr.Item("STORAGE_AMO_TTL") = String.Empty
                If ("01").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '積数1期
                    dr.Item("SEKI_ARI_NB1") = frm.numIkkatu.Value.ToString()
                ElseIf ("02").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '積数2期
                    dr.Item("SEKI_ARI_NB2") = frm.numIkkatu.Value.ToString()
                ElseIf ("03").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '積数3期
                    dr.Item("SEKI_ARI_NB3") = frm.numIkkatu.Value.ToString()
                ElseIf ("04").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '単価1期
                    dr.Item("STORAGE1") = frm.numIkkatu.Value.ToString()
                ElseIf ("05").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '単価2期
                    dr.Item("STORAGE2") = frm.numIkkatu.Value.ToString()
                ElseIf ("06").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '単価3期
                    dr.Item("STORAGE3") = frm.numIkkatu.Value.ToString()
                ElseIf ("07").Equals(frm.cmbIkkatu.SelectedValue) = True Then
                    '保管料
                    dr.Item("STORAGE_AMO_TTL") = frm.numIkkatu.Value.ToString()
                End If
                dr.Item("GOODS_CD_CUST") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.CUST_GOODS_CD.ColNo))
                dr.Item("GOODS_NM") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.CUST_GOODS_NM.ColNo))
                dr.Item("LOT_NO") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.LOT_NO.ColNo))
                dr.Item("SERIAL_NO") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.SERIAL_NO.ColNo))
                dr.Item("INKA_NO_L") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.INKA_NO_L.ColNo))
                dr.Item("ROW_NO") = Convert.ToString(arr(i))
                dr.Item("SYS_UPD_DATE") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMG030G.sprMeisaiPrtDef.SYS_UPD_TIME.ColNo))

                'データの設定
                datatable.Rows.Add(dr)

            Next i

        End With

    End Sub
    'END YANAI 20111014 一括変更追加

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMG030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "EditDataEvent")

        Call Me.EditDataEvent(frm)

        Logger.EndLog(Me.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMG030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Acquisithion")

        '検索処理
        Me.Acquisithion(frm)

        Logger.EndLog(Me.GetType.Name, "Acquisithion")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMG030F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        Logger.StartLog(Me.GetType.Name, "")

        Call Me.SaveItemData(frm, controlNm)

        Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        '終了処理  
        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG030F, ByVal e As FormClosingEventArgs, ByVal controlNm As String)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseSaveEvent(frm, e, controlNm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Enterキー押下処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMG030F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        If e.KeyCode = Keys.Enter Then

            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

        End If
    End Sub

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMG030F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        Logger.StartLog(Me.GetType.Name, "RowSelection")

        'DBより該当データの取得処理
        Call Me.SelectListData(frm, e)

        Logger.EndLog(Me.GetType.Name, "RowSelection")

    End Sub

    'START YANAI 20111013 保管料自動計算廃止
    '''' <summary>
    '''' フォーカスイン時イベント
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Friend Sub SetVariable(ByRef frm As LMG030F)

    '    Me.SetVariableDicision(frm)

    'End Sub
    'END YANAI 20111013 保管料自動計算廃止

    'START YANAI 20111013 保管料自動計算廃止
    '''' <summary>
    '''' フォーカスアウト時イベント
    '''' </summary>
    '''' <param name="frm"></param>
    '''' <remarks></remarks>
    'Friend Sub SetCalcLate(ByRef frm As LMG030F)

    '    '再計算処理を行う
    '    Call Me.Recalculation(frm)

    'End Sub
    'END YANAI 20111013 保管料自動計算廃止

    ''' <summary>
    ''' 印刷ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnPrint_Click(ByRef frm As LMG030F, ByVal e As System.EventArgs)

        Call Me.PrintBtnClick(frm)

    End Sub

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 一括変更ボタンクリック時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub btnIkkatu_Click(ByRef frm As LMG030F, ByVal e As System.EventArgs)

        Call Me.IkkatuBtnClick(frm)

    End Sub
    'END YANAI 20111014 一括変更追加

    ''' <summary>
    ''' セルのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprDetail_LeaveCell(ByVal frm As LMG030F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

        Call Me.SprCellLeave(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetail_LeaveCell")

    End Sub

    'START YANAI 20111014 一括変更追加
    ''' <summary>
    ''' 修正項目コンボのロストフォーカスイベント
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub cmbIkkatu_Selected(ByVal frm As LMG030F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbIkkatu_Selected")

        Call Me.cmbIkkatuSelected(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbIkkatu_Selected")

    End Sub
    'END YANAI 20111014 一括変更追加

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class