' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI560H : TSMC請求データ計算
'  作  成  者       :  [HORI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMI560ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
Public Class LMI560H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI560V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI560G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConV As LMIControlV

    '''' <summary>
    '''' Handler共通クラスを格納するフィールド
    '''' </summary>
    '''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    '画面間データを取得する
    Dim prmDs As DataSet

    ''' <summary>
    ''' デリゲート（非同期実行用）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Delegate Function WriteStringAsyncDelegate(ByVal ds As DataSet) As Integer

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
        Dim frm As LMI560F = New LMI560F(Me)

        '画面共通クラスの設定
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMI560V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMI560G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._LMIconV = New LMIControlV(Me, DirectCast(frm, Form), Me._LMIconG)

        'ハンドラー共通クラスの設定
        Me._LMIConH = New LMIControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'EnterKey制御
        frm.KeyPreview = True

        'フォームの初期化
        Call Me.InitControl(frm)

        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID(), MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'フォームの初期値設定
        Me._G.SetInitValue(frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

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
    ''' 前回計算取消処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub CancelCalc(ByVal frm As LMI560F)

        'スタート処理
        Call Me._LMIConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMI560C.EventShubetsu.ZENKEISANTORI) = False Then
            '終了処理
            Call Me._LMIConH.EndAction(frm)

            Exit Sub
        End If

        '実行確認
        Select Case MyBase.ShowMessage(frm, "C001", New String() {"前回計算分のデータ取消"})
            Case MsgBoxResult.Cancel
                '終了処理
                Call Me._LMIConH.EndAction(frm)

                Exit Sub
        End Select

        '全画面ロック
        MyBase.LockedControls(frm)

        '取消用データセット設定
        Call Me.SetCancelCalc(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        MyBase.Logger.StartLog(MyBase.GetType.Name, "CancelCalc")

        prmDs = MyBase.CallWSA("LMI560BLF", "CancelCalc", prmDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(frm)
        Else
            ShowMessage(frm, "G002", New String() {"前回計算取消処理", ""})
        End If

        '画面ロック解除
        MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.CUST)

    End Sub

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub Execute(ByVal frm As LMI560F)

        'スタートアクション
        Call Me._LMIConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMI560C.EventShubetsu.JIKKOU) = False Then
            '終了処理
            Call Me._LMIConH.EndAction(frm)

            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '検索用データセット設定
        Call Me.SetSearchData(frm)

        '実行用データセット設定
        Call Me.SetExecute(frm)

        ' メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '==========================
        'WSAクラス呼出
        '==========================
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SeikyuCalc")

        prmDs = MyBase.CallWSA("LMI560BLF", "SeikyuCalc", prmDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then
            MyBase.ShowMessage(frm, "E235")
            ' Excel 起動 
            Call MyBase.MessageStoreDownload()
        Else
            If MyBase.IsErrorMessageExist() = True Then
                MyBase.ShowMessage(frm)
            Else
                ShowMessage(frm, "G002", New String() {"計算処理", ""})
            End If
        End If

        '画面ロック解除
        MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMIConH.EndAction(frm)

        'キャッシュ最新化
        MyBase.LMCacheMasterData(LMConst.CacheTBL.CUST)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMI560F)

        'スタートアクション
        Call Me._LMIConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMI560C.EventShubetsu.KENSAKU) = False Then
            '終了処理
            Call Me._LMIConH.EndAction(frm)

            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        'スプレッドのクリア
        frm.sprDetail.CrearSpread()

        '検索用データセット設定
        Call Me.SetSearchData(frm)

        '==========================
        'WSAクラス呼出
        '==========================
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        Dim rtnDs As DataSet = Me._LMIConH.CallWSAAction(
            DirectCast(frm, Form),
            "LMI560BLF",
            "SelectListData",
            prmDs,
            Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))),
            1000
            )

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then
            Dim dt As DataTable = rtnDs.Tables(LMI560C.TABLE_NM_OUT)
            If dt.Rows.Count > 0 Then
                '取得データをスプレッドに反映
                Call Me._G.SetSelectListData(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G016", New String() {dt.Rows.Count.ToString()})
            End If
        End If

        '画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMIConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMI560F, ByVal eventShubetsu As LMI560C.EventShubetsu)

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.MasterShowEvent(frm, eventShubetsu)

        '処理終了アクション
        Call Me._LMIConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMI560F, ByVal eventShubetsu As LMI560C.EventShubetsu)

        'スタートアクション
        Call Me._LMIConH.StartAction(frm)

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()
        If String.IsNullOrEmpty(objNm) Then
            MyBase.ShowMessage(frm, "G005")
        End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Select Case objNm
            Case frm.txtSeqtoCd.Name
                If String.IsNullOrEmpty(frm.txtSeqtoCd.TextValue) Then
                    frm.lblSeqtoNm.TextValue = String.Empty
                End If

                Call Me.ShowSeiqtoPopup(frm, objNm, prm, eventShubetsu)

            Case Else
                MyBase.ShowMessage(frm, "G005")

                '画面ロック解除
                Call MyBase.UnLockedControls(frm)

                Exit Sub
        End Select

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        '画面ロック解除
        Call MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI560F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "内部処理"

    ''' <summary>
    ''' バッチ切離処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub SetDelegate(ByVal ds As DataSet)

        Dim dlgt As New WriteStringAsyncDelegate _
            (AddressOf CallCalcBatch)

        Dim ar As IAsyncResult = dlgt.BeginInvoke(ds _
                                                  , Nothing _
                                                  , dlgt)

    End Sub

    ''' <summary>
    ''' LMG800呼び出し
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CallCalcBatch(ByVal ds As DataSet) As Integer

        '登録処理（保管荷役計算管理ワークヘッダ）
        MyBase.CallWSA("LMI560BLF", "CalcBatch", ds)

        Return 0

    End Function

    ''' <summary>
    ''' 請求先マスタ照会(LMZ220)参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowSeiqtoPopup(ByVal frm As LMI560F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMI560C.EventShubetsu)

        'パラメータ生成
        Dim prmDs As DataSet = New LMZ220DS()
        Dim dr As DataRow = prmDs.Tables(LMZ220C.TABLE_NM_IN).NewRow()

        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
        If LMI560C.EventShubetsu.ENTER.Equals(eventShubetsu) Then
            dr.Item("SEIQTO_CD") = frm.txtSeqtoCd.TextValue
        End If
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        prmDs.Tables(LMZ220C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '請求先マスタ照会(LMZ220)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ220", prm)

        '戻り処理
        If prm.ReturnFlg Then
            'PopUpから取得したデータをコントロールにセット
            frm.txtSeqtoCd.TextValue = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0).Item("SEIQTO_CD").ToString()
            frm.lblSeqtoNm.TextValue = prm.ParamDataSet.Tables(LMZ220C.TABLE_NM_OUT).Rows(0).Item("SEIQTO_NM").ToString()
        End If

    End Sub

    ''' <summary>
    ''' チェック（権限・項目）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMI560F, ByVal SHUBETSU As LMI560C.EventShubetsu) As Boolean

        'フォームの背景色を初期化する
        Me._G.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(SHUBETSU) = False Then
            Return False
        End If

        '単項目チェック
        If Me._V.IsInputCheck(SHUBETSU) = False Then
            Return False
        End If

        '実行・前回計算取消イベントの場合
        Select Case SHUBETSU
            Case LMI560C.EventShubetsu.JIKKOU, LMI560C.EventShubetsu.ZENKEISANTORI
                '関連項目チェック
                If Me._V.isRelationCheck(SHUBETSU) = False Then
                    Return False
                End If
        End Select

        Return True

    End Function

    ''' <summary>
    ''' Enter押下時マスタ参照判定処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <param name="controlNm"></param>
    ''' <remarks></remarks>
    Private Sub EnterkeyControl(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String, ByVal eventShubetsu As LMI560C.EventShubetsu)

        If Keys.Enter.Equals(e.KeyCode) Then
            Dim MasterFlg As Boolean = False

            Select Case controlNm
                Case frm.txtSeqtoCd.Name
                    If Not String.IsNullOrEmpty(frm.txtSeqtoCd.TextValue) Then
                        MasterFlg = True
                    Else
                        frm.lblSeqtoNm.TextValue = String.Empty
                    End If
            End Select

            'マスタ検索フラグ　がTRUEの場合検索処理を行う
            If MasterFlg Then
                Me._PopupSkipFlg = False
                Me.MasterShowEvent(frm, eventShubetsu)

                '処理終了アクション
                Call Me._LMIConH.EndAction(frm)
            Else
                'EnterKeyによるタブ遷移
                frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
            End If
        End If

    End Sub

#End Region '内部処理

#Region "DataSet"

    ''' <summary>
    ''' 取消用データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetCancelCalc(ByVal frm As LMI560F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'スプレッド選択行の取得（リスト）
        Dim chkList As ArrayList = Me._V.getCheckList()

        'データテーブルの宣言
        Dim datatable As DataTable = Me.prmDs.Tables(LMI560C.TABLE_NM_IN_DEL)
        datatable.Clear()

        Dim spr As SheetView = frm.sprDetail.ActiveSheet
        For i As Integer = 0 To chkList.Count() - 1
            Dim num As Integer = Convert.ToInt32(chkList(i))
            Dim dr As DataRow = datatable.NewRow()

            '営業所コード
            dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue

            '請求先コード
            dr.Item("SEIQTO_CD") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.SEIQTO_CD.ColNo))

            '最終請求日
            dr.Item("LAST_DATE") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.LAST_DATE_ORG.ColNo))

            '最終JOB番号
            dr.Item("LAST_JOB_NO") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.LAST_JOB_NO.ColNo))

            '前回請求日
            dr.Item("BEFORE_DATE") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.BEFORE_DATE.ColNo))

            '前回JOB番号
            dr.Item("BEFORE_JOB_NO") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.BEFORE_JOB_NO.ColNo))

            '前々回請求日
            dr.Item("OLD_DATE") = ""

            '前々回JOB番号
            dr.Item("OLD_JOB_NO") = ""

            '前回請求日(予備)
            Dim yobiDate As String = String.Concat(Left(Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.LAST_DATE_ORG.ColNo)).ToString, 6), "01")
            yobiDate = Me._V.GetSimeDate(Me._V.GetAddDate(yobiDate, "m", -1), Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.CLOSE_KB.ColNo)).ToString)
            dr.Item("BEFORE_DATE_YOBI") = yobiDate

            '行番号
            dr.Item("LINE_NO") = chkList(i)

            datatable.Rows.Add(dr)
        Next

        '前々回情報を取得してデータセットに反映
        Dim copyDs As DataSet = Me.prmDs.Copy()
        copyDs = MyBase.CallWSA("LMI560BLF", "SelectOldInfo", copyDs)
        If copyDs.Tables(LMI560C.TABLE_NM_IN_DEL).Rows.Count > 0 Then
            Me.prmDs = Nothing
            Me.prmDs = copyDs.Copy()
        End If

        prm.ParamDataSet = prmDs

    End Sub

    ''' <summary>
    ''' 実行用データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetExecute(ByVal frm As LMI560F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'スプレッド選択行の取得（リスト）
        Dim chkList As ArrayList = Me._V.getCheckList()

        'データテーブルの宣言
        Dim datatable As DataTable = Me.prmDs.Tables(LMI560C.TABLE_NM_IN_CALC)
        datatable.Clear()

        Dim spr As SheetView = frm.sprDetail.ActiveSheet
        For i As Integer = 0 To chkList.Count() - 1
            Dim num As Integer = Convert.ToInt32(chkList(i))
            Dim dr As DataRow = datatable.NewRow()

            '営業所コード
            dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue

            '請求先コード
            dr.Item("SEIQTO_CD") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.SEIQTO_CD.ColNo))

            '請求期間FROM
            dr.Item("INV_DATE_FROM") = Me._V.GetAddDate(Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.LAST_DATE_ORG.ColNo)), "d", +1)

            '請求期間TO
            dr.Item("INV_DATE_TO") = Me._V.GetSimeDate(frm.imdInvDate.TextValue, Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.CLOSE_KB.ColNo)))

            'JOB番号【後で設定する】
            dr.Item("JOB_NO") = String.Empty

            'JOB番号(加工前)【後で設定する】
            dr.Item("JOB_NO_ORG") = String.Empty

            '行番号
            dr.Item("LINE_NO") = chkList(i)

            '最終請求日
            dr.Item("LAST_DATE") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.LAST_DATE_ORG.ColNo))

            '最終JOB番号
            dr.Item("LAST_JOB_NO") = Me._LMIConV.GetCellValue(spr.Cells(num, LMI560G.sprDetailDef.LAST_JOB_NO.ColNo))

            datatable.Rows.Add(dr)
        Next

        prm.ParamDataSet = prmDs

    End Sub

    ''' <summary>
    ''' 検索用データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchData(ByVal frm As LMI560F)

        'データテーブル
        If IsNothing(Me.prmDs) Then Me.prmDs = New LMI560DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMI560C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        datatable.Clear()

        With frm
            '営業所コード
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue

            '請求先コード
            dr.Item("SEIQTO_CD") = .txtSeqtoCd.TextValue

            '請求月
            dr.Item("INV_DATE") = Left(.imdInvDate.TextValue, 6)

            With .sprDetail.ActiveSheet
                '請求先名
                dr.Item("SEIQTO_NM") = Me._V.GetCellValue(.Cells(0, LMI560G.sprDetailDef.SEIQTO_NM.ColNo)).Trim()
            End With
        End With

        datatable.Rows.Add(dr)

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "UndotheLastCalclation")

        Call Me.CancelCalc(frm)

        Logger.EndLog(Me.GetType.Name, "UndotheLastCalclation")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Execute")

        Call Me.Execute(frm)

        Logger.EndLog(Me.GetType.Name, "Execute")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "MasterShowEvent")

        Me.OpenMasterPop(frm, LMI560C.EventShubetsu.MASTER)

        Logger.EndLog(Me.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI560F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '''========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <param name="controlNm">コントロール名称</param>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMI560F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        If e.KeyCode = Keys.Enter Then
            Me.EnterkeyControl(frm, e, controlNm, LMI560C.EventShubetsu.ENTER)
        End If

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class