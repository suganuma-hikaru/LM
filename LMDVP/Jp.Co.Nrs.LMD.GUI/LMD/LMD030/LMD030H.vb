' ==========================================================================
'  システム名       :  LMO
'  サブシステム名   :  LMD     : 在庫管理
'  プログラムID     :  LMD030H : 在庫履歴
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMD030ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD030H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' Formフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD030F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD030V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD030G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconH As LMDControlH

    ''' <summary>
    ''' 画面間データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Dim _PrmDs As DataSet

    ''' <summary>
    ''' 検索結果データを保存するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Dim _OutDs As DataSet

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
        Me._PrmDs = prm.ParamDataSet()

        'フォームの作成
        Dim frm As LMD030F = New LMD030F(Me)
        Me._Frm = frm

        'Validateクラスの設定
        Me._V = New LMD030V(Me, Me._Frm)

        'Gamenクラスの設定
        Me._G = New LMD030G(Me, Me._Frm)

        'Validate共通クラスの設定
        Me._LMDconV = New LMDControlV(Me, DirectCast(Me._Frm, Form))

        'Hnadler共通クラスの設定
        Me._LMDconH = New LMDControlH(DirectCast(Me._Frm, Form), MyBase.GetPGID())

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'フォームの初期化
        Call Me.InitControl(Me._Frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD030C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()
        'Call Me._G.SetInitValue(Me._Frm)

        'コントロール個別設定
        Call Me._G.SetControl(Me.GetPGID(), Me._PrmDs)

        '↓ データ取得の必要があればここにコーディングする。

        '検索結果格納データセット初期化
        Me._OutDs = New DataSet

        '検索処理(初期）
        Call Me.SelectData(LMD030C.SearchShubetsu.FIRST_SEARCH)

        '↑ データ取得の必要があればここにコーディングする。

        'スプレッドのソートロック
        Call Me._G.SetSortLock()

        'メッセージの表示
        Me.ShowMessage(Me._Frm, "G007")

        '画面の入力項目の制御
        Call _G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'optionボタンにイベント追加
        AddHandler Me._Frm.optCntShow.CheckedChanged, AddressOf opt_CheckedChanged
        AddHandler Me._Frm.optAmtShow.CheckedChanged, AddressOf opt_CheckedChanged

    End Sub

#End Region

#Region "外部メソッド"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMD030C.EventShubetsu, ByVal frm As LMD030F)

        '処理開始アクション
        Call Me._LMDconH.StartAction(frm)

        '権限チェック（共通）
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Call Me._LMDconH.EndAction(frm, Me.GetGMessage())
            Exit Sub
        End If

        'イベント種別による分岐
        Select Case eventShubetsu

            Case LMD030C.EventShubetsu.KENSAKU

                '検索処理
                Call Me.SelectData(LMD030C.SearchShubetsu.NEW_SEARCH)

                Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理

            Case LMD030C.EventShubetsu.DEL

                '******************「削除」******************'

                '未選択チェック
                If Me._V.IsSelectDataChk() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '取消可能チェック
                If Me._V.IsTorikesiChk() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '最終請求日 + 移動日チェック
                If Me.IsHokanNiyakuChk() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '後続行チェック
                If Me._V.IsNextRowChk() = False Then
                    Call Me._LMDconH.EndAction(frm, Me.GetGMessage()) '終了処理
                    Exit Sub
                End If

                '削除処理
                Call Me.DeleteData()

                Call Me._LMDconH.EndAction(frm, Me.GetGMessage())

        End Select

    End Sub

#End Region

#Region "内部メソッド"

#Region "検索処理"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal SearchShubetsu As LMD030C.SearchShubetsu)

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        'MyBase.SetLimitCount(LMD030C.LIMITED_COUNT)

        'DataSet設定
        Dim rtDs As DataSet = New LMD030DS()
        Call Me.SetDataSetInData(SearchShubetsu)

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Me._OutDs = Me._LMDconH.CallWSAAction(DirectCast(Me._Frm, Form), "LMD030BLF", "SelectListData", _PrmDs, 0)

        '残個数、残数量の計算を行う
        Call Me.SetColData()

        '検索成功時共通処理を行う
        Dim cnt As Integer = Me._OutDs.Tables(LMD030C.TABLE_NM_OUT).Rows.Count()
        If Me._OutDs.Tables(LMD030C.TABLE_NM_OUT).Rows.Count() > 0 Then
            Call Me.SuccessSelect(SearchShubetsu)
        Else
            '検証結果(メモ)№116対応(2011.09.12)START----
            '再描画する場合は検索結果メッセージを表示しない
            If SearchShubetsu.Equals(LMD030C.SearchShubetsu.RE_SEARCH) = False Then
                MyBase.ShowMessage(Me._Frm, "G001", New String() {cnt.ToString()})
            End If
            '検証結果(メモ)№116対応(2011.09.12)END----
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        'ファンクションキーの設定
        Call Me._G.UnLockedForm()

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal SearchShubetsu As LMD030C.SearchShubetsu)

        '画面解除
        Call MyBase.UnLockedControls(Me._Frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(Me._OutDs)

        'オプションボタンによる表示・非表示切り替え
        Call Me.ControlViewData()

        '再描画する場合は検索結果メッセージを表示しない
        If SearchShubetsu.Equals(LMD030C.SearchShubetsu.RE_SEARCH) = False Then

            Dim cnt As Integer = Me._OutDs.Tables(LMD030C.TABLE_NM_OUT).Rows.Count()

            If cnt = 0 Then
                MyBase.ShowMessage(Me._Frm, "G001", New String() {cnt.ToString()})
            Else
                'メッセージエリアの設定
                MyBase.ShowMessage(Me._Frm, "G016", New String() {cnt.ToString()})
            End If

        End If

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMD030F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' 残個数・残数量 設定処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetColData()

        '計算用変数
        Dim inkaKosu As Double = 0
        Dim inkaSuryo As Double = 0
        Dim outkaKosu As Double = 0
        Dim outkaSuryo As Double = 0
        Dim kosu As Double = 0
        Dim suryo As Double = 0

        Dim max As Integer = Me._OutDs.Tables(LMD030C.TABLE_NM_OUT).Rows.Count() - 1
        With Me._OutDs.Tables(LMD030C.TABLE_NM_OUT)

            For i As Integer = 0 To max

                '取消、予定レコードは計算対象外
                If .Rows(i)("STATE_KB").ToString().Equals("消") = False OrElse _
                   .Rows(i)("STATE_KB").ToString().Equals("予") = False Then

                    inkaKosu = Convert.ToDouble(.Rows(i)("INKA_NB").ToString())
                    inkaSuryo = Convert.ToDouble(.Rows(i)("INKA_QT").ToString())
                    outkaKosu = Convert.ToDouble(.Rows(i)("OUTKA_NB").ToString())
                    outkaSuryo = Convert.ToDouble(.Rows(i)("OUTKA_QT").ToString())

                    '種別が「移入」以外の場合
                    If .Rows(i)("SYUBETU").ToString().Equals("移入") = False Then
                        kosu = kosu + (inkaKosu - outkaKosu)                       '入荷個数-出荷個数
                        suryo = suryo + (inkaSuryo - outkaSuryo)                   '入荷数量-出荷数量
                    Else
                        kosu = inkaKosu                     '入荷個数
                        suryo = inkaSuryo                   '入荷数量
                    End If

                    .Rows(i)("BACKLOG_NB") = kosu      '残個数
                    .Rows(i)("BACKLOG_QT") = suryo      '残数量

                End If
            Next

        End With
    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G007"
    End Function

#End Region

#Region "削除処理"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteData()

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        'DataSet設定
        Dim ds As DataSet = New LMD030DS()
        Call Me.SetDataSetInData_DELETE(ds)

        ' #################### 削除処理　####################'

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '==== WSAクラス呼出（変更処理） ====
        MyBase.CallWSA("LMD030BLF", "DeleteData", ds)

        'メッセージコードの判定
        If MyBase.IsMessageExist = True Then
            MyBase.ShowMessage(Me._Frm)
        Else
            '更新成功時、メッセージ表示
            Call Me.SuccessDelete("削除")
        End If

        Call Me._LMDconH.EndAction(Me._Frm, Me.GetGMessage())

        ' #################### 再描画　####################'
        Call Me.SelectData(LMD030C.SearchShubetsu.RE_SEARCH)


    End Sub

    ''' <summary>
    ''' 削除成功時共通処理
    ''' </summary>
    ''' <param name="msg">成功メッセージに載せる変換文字</param>
    ''' <remarks></remarks>
    Private Sub SuccessDelete(ByVal msg As String)

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G002", New String() {msg, ""})

        '終了処理
        Call Me._LMDconH.EndAction(Me._Frm, Me.GetGMessage())

    End Sub

    ''' <summary>
    ''' 最終請求日 + 移動日チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsHokanNiyakuChk() As Boolean

        Dim ds As DataSet = Me.SetDataSetSeiqData()
        ds = MyBase.CallWSA("LMD000BLF", "SelectChkIdoDate", ds)

        If MyBase.IsMessageExist() = True Then
            '処理終了アクション
            Call Me.ShowMessage(Me._Frm)
            Return False
        End If

        Return True

    End Function

#End Region

#Region "Optionボタン関連"

    ''' <summary>
    ''' optionボタンチェック時、発生するイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub opt_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.ControlViewData()
    End Sub

    ''' <summary>
    ''' optionボタンによるセル表示制御
    ''' </summary>
    ''' <remarks></remarks>
    Public Sub ControlViewData()

        With Me._Frm.sprDetail

            Dim v1 As Boolean = False
            Dim v2 As Boolean = False

            If Me._Frm.optCntShow.Checked() = True Then
                '個数表示
                v1 = True
            ElseIf Me._Frm.optAmtShow.Checked() = True Then
                '数量表示
                v2 = True
            End If

            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.INKA_NB).Visible() = v1
            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.OUTKA_NB).Visible() = v1
            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.BACKLOG_NB).Visible() = v1
            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.PKG_UT).Visible() = v1

            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.INKA_QT).Visible() = v2
            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.OUTKA_QT).Visible() = v2
            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.BACKLOG_QT).Visible() = v2
            .Sheets(0).Columns.Get(LMD030C.SprColumnIndex.STD_IRIME_UT).Visible() = v2

        End With


    End Sub

#End Region

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD030F)

        'フォーカス移動処理
        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

#End Region

#Region "DataSet設定"

    '=== TODO : 遷移先画面完成後、格画面用inputDataSet作成 ==='

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal SearchShubetsu As LMD030C.SearchShubetsu)

        With Me._PrmDs.Tables(LMD030C.TABLE_NM_IN)

            If SearchShubetsu.Equals(LMD030C.SearchShubetsu.FIRST_SEARCH) = False Then
                '検索条件　出荷取消表示有無
                If Me._Frm.chkSyukkaDelShow.Checked() = True Then
                    .Rows(0)("DEL_VIEW_FLG") = "01"
                Else
                    .Rows(0)("DEL_VIEW_FLG") = "00"
                End If
            End If

        End With

    End Sub

    ''' <summary>
    ''' 削除時、利用するデータセット設定
    ''' </summary>
    ''' <param name="rtDs"></param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData_DELETE(ByRef rtDs As DataSet)

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count() - 1
       
        Dim num As Integer = 0
        Dim O_PORA_ZAI_NB As Decimal = 0
        Dim O_ALLOC_CAN_NB As Decimal = 0
        Dim O_PORA_ZAI_QT As Decimal = 0
        Dim O_ALLOC_CAN_QT As Decimal = 0
        Dim N_PORA_ZAI_NB As Decimal = 0
        Dim N_ALLOC_CAN_NB As Decimal = 0
        Dim N_PORA_ZAI_QT As Decimal = 0
        Dim N_ALLOC_CAN_QT As Decimal = 0

        Dim rtDt As DataTable = rtDs.Tables(LMD030C.TABLE_NM_IN_DEL)
        Dim rtDr As DataRow = Nothing

        For i As Integer = 0 To max

            With Me._Frm.sprDetail.ActiveSheet

                num = Convert.ToInt32(chkList(i))

                '梱数・数量計算
                'O_PORA_ZAI_NB = Convert.ToInt32(.Cells(num, LMD030G.sprDetailDef.PORA_ZAI_NB.ColNo).Value()) + Convert.ToInt32(.Cells(num, LMD030G.sprDetailDef.INKA_NB.ColNo).Value())
                'O_ALLOC_CAN_NB = Convert.ToInt32(.Cells(num, LMD030G.sprDetailDef.ALLOC_CAN_NB.ColNo).Value()) + Convert.ToInt32(.Cells(num, LMD030G.sprDetailDef.INKA_NB.ColNo).Value())

                O_PORA_ZAI_NB = Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.INKA_NB.ColNo).Value())
                O_ALLOC_CAN_NB = Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.INKA_NB.ColNo).Value())
                O_PORA_ZAI_QT = O_PORA_ZAI_NB * Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.STD_IRIME_NB.ColNo).Value())
                O_ALLOC_CAN_QT = O_ALLOC_CAN_NB * Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.STD_IRIME_NB.ColNo).Value())

                N_PORA_ZAI_NB = Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.PORA_ZAI_NB.ColNo).Value()) - Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.INKA_NB.ColNo).Value())
                If N_PORA_ZAI_NB = 0 Then
                    N_ALLOC_CAN_NB = 0
                    N_PORA_ZAI_QT = 0
                    N_ALLOC_CAN_QT = 0
                Else
                    N_ALLOC_CAN_NB = Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.ALLOC_CAN_NB.ColNo).Value()) - Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.INKA_NB.ColNo).Value())
                    N_PORA_ZAI_QT = N_PORA_ZAI_NB * Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.STD_IRIME_NB.ColNo).Value())
                    N_ALLOC_CAN_QT = N_ALLOC_CAN_NB * Convert.ToDecimal(.Cells(num, LMD030G.sprDetailDef.STD_IRIME_NB.ColNo).Value())
                End If

                rtDr = rtDt.NewRow()

                'データ行作成
                rtDr.Item("NRS_BR_CD") = Me._Frm.lblNrsBrCd.TextValue()
                rtDr.Item("O_ZAI_REC_NO") = .Cells(num, LMD030G.sprDetailDef.O_ZAI_REC_NO.ColNo).Value()
                rtDr.Item("N_ZAI_REC_NO") = .Cells(num, LMD030G.sprDetailDef.N_ZAI_REC_NO.ColNo).Value()
                rtDr.Item("O_PORA_ZAI_NB") = O_PORA_ZAI_NB
                rtDr.Item("O_ALLOC_CAN_NB") = O_ALLOC_CAN_NB
                rtDr.Item("O_PORA_ZAI_QT") = O_PORA_ZAI_QT
                rtDr.Item("O_ALLOC_CAN_QT") = O_ALLOC_CAN_QT
                rtDr.Item("N_PORA_ZAI_NB") = N_PORA_ZAI_NB
                rtDr.Item("N_ALLOC_CAN_NB") = N_ALLOC_CAN_NB
                rtDr.Item("N_PORA_ZAI_QT") = N_PORA_ZAI_QT
                rtDr.Item("N_ALLOC_CAN_QT") = N_ALLOC_CAN_QT
                rtDr.Item("IDO_SYS_UPD_DATE") = .Cells(num, LMD030G.sprDetailDef.IDO_SYS_UPD_DATE.ColNo).Value()
                rtDr.Item("IDO_SYS_UPD_TIME") = .Cells(num, LMD030G.sprDetailDef.IDO_SYS_UPD_TIME.ColNo).Value()
                rtDr.Item("O_ZAI_SYS_UPD_DATE") = .Cells(num, LMD030G.sprDetailDef.O_ZAI_SYS_UPD_DATE.ColNo).Value()
                rtDr.Item("O_ZAI_SYS_UPD_TIME") = .Cells(num, LMD030G.sprDetailDef.O_ZAI_SYS_UPD_TIME.ColNo).Value()
                rtDr.Item("N_ZAI_SYS_UPD_DATE") = .Cells(num, LMD030G.sprDetailDef.N_ZAI_SYS_UPD_DATE.ColNo).Value()
                rtDr.Item("N_ZAI_SYS_UPD_TIME") = .Cells(num, LMD030G.sprDetailDef.N_ZAI_SYS_UPD_TIME.ColNo).Value()
                rtDr.Item("HOKAN_SEIQTO_CD") = .Cells(num, LMD030G.sprDetailDef.HOKAN_SEIQTO_CD.ColNo).Value()
                rtDr.Item("IDO_DATE") = DateFormatUtility.DeleteSlash(.Cells(num, LMD030G.sprDetailDef.IDO_DATE.ColNo).Text)

                '行追加
                rtDt.Rows.Add(rtDr)

            End With

        Next

    End Sub

    ''' <summary>
    ''' 保管料チェックに利用するデータセット設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetSeiqData() As DataSet

        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count() - 1
        Dim num As Integer = 0
        Dim row As DataRow = Nothing

        Dim rtDs As DataSet = New LMD000DS()
        Dim inTbl As DataTable = rtDs.Tables(LMControlC.LMD000_TABLE_NM_IN)
        Dim dr As DataRow = Nothing

        For i As Integer = 0 To max

            With Me._Frm.sprDetail.ActiveSheet

                dr = inTbl.NewRow
                num = Convert.ToInt32(chkList(i))

                dr(LMControlC.LMD000_COL_GOODS_CD_NRS) = Me._Frm.lblGoodsCDNrs.TextValue
                dr(LMControlC.LMD000_COL_NRS_BR_CD) = Me._Frm.lblNrsBrCd.TextValue
                dr(LMControlC.LMD000_COL_CHK_DATE) = .Cells(num, LMD030G.sprDetailDef.IDO_DATE.ColNo).Text().Replace("/", "")
                dr(LMControlC.LMD000_COL_REPLACE_STR1) = "鑑作成済みの"
                dr(LMControlC.LMD000_COL_REPLACE_STR2) = "削除"

                inTbl.Rows.Add(dr)

            End With

        Next

        Return rtDs

    End Function

#End Region 'DataSet設定

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し（削除）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "DeleteData")

        '削除処理
        Call Me.ActionControl(LMD030C.EventShubetsu.DEL, frm)

        Logger.EndLog(Me.GetType.Name, "DeleteData")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Import JDE")

        ''Import JDE
        'Me.sendPrint(Me._Frm, e)

        Logger.EndLog(Me.GetType.Name, "Import JDE")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectData")

        '検索処理
        Call Me.ActionControl(LMD030C.EventShubetsu.KENSAKU, frm)

        Logger.EndLog(Me.GetType.Name, "SelectData")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        Me._Frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMD030F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMD030F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EnterKeyDown")

        Call Me.EnterAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EnterKeyDown")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class