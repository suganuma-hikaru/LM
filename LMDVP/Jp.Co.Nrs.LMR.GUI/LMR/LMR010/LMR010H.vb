' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMR010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMR010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMR010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMR010G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMRconV As LMRControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMRconH As LMRControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMRconG As LMRControlG

    ''' <summary>
    '''検索件数格納用フィールド（検索ボタン時）
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    ''' 値の保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _Ds As DataSet = New LMR010DS

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    '''完了種別格納用フィールド（検索ボタン時）
    ''' </summary>
    ''' <remarks></remarks>
    Private _Syubetsu As String
    Private _SyubetsuNM As String
    Private _Eigyosyo As String

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
        Dim frm As LMR010F = New LMR010F(Me)

        'Validate共通クラスの設定
        Me._LMRconV = New LMRControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMRconH = New LMRControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me)

        'Gamen共通クラスの設定
        Me._LMRconG = New LMRControlG(DirectCast(frm, Form))

        'Gamenクラスの設定
        Me._G = New LMR010G(Me, frm, Me._LMRconG)

        'Validateクラスの設定
        Me._V = New LMR010V(Me, frm, Me._LMRconV, Me._LMRconG, Me._G)

        'フォームの初期化
        Call Me.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMR010C.MODE_DEFAULT)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール初期化
        Call Me._G.SetControl(Me.GetPGID())

        'スプレッドの初期化
        Call Me._G.InitSpread()

        If (LMR010C.PGID_LMB010).Equals(MyBase.RootPGID()) = False AndAlso _
            (LMR010C.PGID_LMC010).Equals(MyBase.RootPGID()) = False Then
            'コントロールの初期値設定
            Call Me._G.SetInitControl(frm)
        Else
            frm.CmbEigyo.SelectedValue() = prmDs.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows(0).Item("NRS_BR_CD")
        End If



        'スプレッドの初期値設定
        Me._G.SetInitValue(frm)

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        Me._Ds = prmDs

        If (LMR010C.PGID_LMA020).Equals(MyBase.RootPGID()) = True Then
            Dim newDs As DataSet = New LMR010DS()
            Me._Ds = newDs
        End If

        If 0 < Me._Ds.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows.Count Then
            If (LMConst.FLG.ON).Equals(Me._Ds.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows(0).Item("DEFAULT_SEARCH_FLG")) = True OrElse _
                (LMConst.FLG.OFF).Equals(Me._Ds.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows(0).Item("DEFAULT_SEARCH_FLG")) = True Then

                '進捗区分チェックデータ取得
                Me.StateCheckData(frm)

                '進捗区分チェック
                Dim kanryo As String = Me._V.IsStateKbCheck(Me._Ds, MyBase.RootPGID())
                If ("E194").Equals(kanryo) = True Then
                    '戻り値が空の時はエラー

                    MyBase.SetMessage("E194")
                    Exit Sub
                ElseIf ("E313").Equals(kanryo) = True Then
                    '戻り値が"-1"の時はエラー

                    Dim printNm As String = String.Empty

                    If (LMR010C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
                        printNm = "出荷指図書"
                    End If

                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.SetMessage("E313", New String() {printNm})
                    MyBase.SetMessage("E313")
                    '2016.01.06 UMANO 英語化対応END
                    Exit Sub
                End If

                '残個数チェック
                If ("LMC010").Equals(RootPGID) = True Then
                    Dim dt As DataTable = Me._Ds.Tables(LMR010C.TABLE_NM_STATECHK)
                    Dim outdr As DataRow = Nothing
                    Dim max As Integer = dt.Rows.Count - 1
                    For i As Integer = 0 To max
                        outdr = dt.Rows(i)
                        Dim outNo As String = Me._V.IsZanKosuCheck(outdr)
                        If String.IsNullOrEmpty(outNo) = False Then
                            '戻り値が空以外の時はエラー

                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.SetMessage("E309", New String() {String.Concat("[出荷管理番号=", outNo, "]")})
                            MyBase.SetMessage("E309", New String() {String.Concat(frm.sprKanryo.ActiveSheet.GetColumnLabel(0, LMR010C.sprKanryoColumnIndex.KANRI_NO), outNo, "]")})
                            '2016.01.06 UMANO 英語化対応END
                            Exit Sub
                        End If
                    Next

                End If

                Me._Ds = prmDs
                Dim dr As DataRow = Me._Ds.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows(0)
                'inのデータセットの完了種別設定
                If (LMR010C.PGID_LMB010).Equals(MyBase.RootPGID()) = True Then
                    '入荷の場合
                    Select Case kanryo
                        Case LMR010C.INKA_SINTYOKU_30
                            dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_01
                            frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_01
                        Case LMR010C.INKA_SINTYOKU_40
                            dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_02
                            frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_02
                        Case LMR010C.INKA_SINTYOKU_50
                            dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_03
                            frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_03
                    End Select
                ElseIf (LMR010C.PGID_LMC010).Equals(MyBase.RootPGID()) = True Then
                    '出荷の場合
                    Select Case kanryo
                        Case LMR010C.OUTKA_SINTYOKU_40
                            dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_04
                            frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_04
                        Case LMR010C.OUTKA_SINTYOKU_50
                            dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_05
                            frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_05
                        Case LMR010C.OUTKA_SINTYOKU_60
                            dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_06
                            frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_06
                    End Select
                ElseIf (LMR010C.PGID_LME030).Equals(MyBase.RootPGID()) = True Then
                    dr.Item("KANRYO_SYUBETU") = LMR010C.KANRYO_07
                    frm.cmbKanryo.SelectedValue = LMR010C.KANRYO_07
                End If

                '初期値設定あり
                'パラメータINの値を画面項目に設定
                Me._G.SetInParamValue(frm, prmDs)

                If (LMConst.FLG.ON).Equals(prmDs.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows(0).Item("DEFAULT_SEARCH_FLG")) = True Then
                    '検索処理

                    '検索処理を行う
                    Me.SelectFirstData(frm)

                    '引き渡されたレコード全て完了処理できない場合、エラーセット
                    If frm.sprKanryo.ActiveSheet.Rows.Count < 2 Then
                        MyBase.SetMessage("E401")
                        Exit Sub
                    End If

                End If

            End If
        End If


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
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMR010C.EventShubetsu, ByVal frm As LMR010F)

        '処理開始アクション
        Me._LMRconH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            Call Me._LMRconH.EndAction(frm)
            Exit Sub
        End If

        '単項目チェック
        If Me._V.IsSingleCheck(eventShubetsu, Me._Ds, Me._Syubetsu) = False Then
            Call Me._LMRconH.EndAction(frm)
            Exit Sub
        End If

        If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True Then
            'データセット設定
            Me.SetDataKanryoInData(frm)
            'チェック用データ取得のデータセット設定
            Me.SetDataKanryoCheckInData(frm)

        End If

        If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True Then
            '荷主系情報の処理を行う
            Me.SelectCustData(frm)
        End If

        '関連チェック
        If Me._V.IsKanrenCheck(eventShubetsu, Me._Ds, Me._Syubetsu, Me._Eigyosyo) = False Then
            Call Me._LMRconH.EndAction(frm)
            Exit Sub
        End If

        If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True AndAlso _
            (Me._Syubetsu).Equals("03") = True Then
            '入荷(小)の在庫レコード番号・ロット№チェック
            Me.SelectCheckINKAData(frm)
            If Me._V.IsInkaSChk(eventShubetsu, Me._Ds, Me._Syubetsu) = False Then
                Call Me._LMRconH.EndAction(frm)
                Exit Sub
            End If

            ' 入荷 TSMCシステム個数未達チェック
            If Me._V.IsInkaTsmcQtyChk() = False Then
                Call Me._LMRconH.EndAction(frm)
                Exit Sub
            End If
        End If
        If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True AndAlso _
            (Me._Syubetsu).Equals("06") = True Then
            '在庫データの入庫日チェック
            Me.SelectCheckZAIData(frm)
            If Me._V.IsZaiChk(eventShubetsu, Me._Ds, Me._Syubetsu) = False Then
                Call Me._LMRconH.EndAction(frm)
                Exit Sub
            End If
        End If
        If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True AndAlso _
            (Me._Syubetsu).Equals("06") = True Then
            '在庫データの在庫数チェック
            Me.SelectListOUTKADataKANRYO(frm)
            Me.SelectListZAIDataKANRYO(frm)
            Me.IsZaiNBQT(Me._Ds)

            If Me._V.IsZaiNBQTChk(eventShubetsu, Me._Ds, Me._Syubetsu) = False Then
                Call Me._LMRconH.EndAction(frm)
                Exit Sub
            End If
        End If
        If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True AndAlso _
             (Me._Syubetsu).Equals("06") = True Then
            '入荷データの入荷済チェック
            If Me._V.IsInkaZumiChk() = False Then
                Call Me._LMRconH.EndAction(frm)
                Exit Sub
            End If
        End If

        ' '' ''If (LMR010C.EventShubetsu.KANRYO).Equals(eventShubetsu) = True AndAlso _
        ' '' ''     (Me._Syubetsu).Equals("07") = True Then
        ' '' ''    '入荷データの入荷済チェック
        ' '' ''    If Me._V.IsInkaZumiChk() = False Then
        ' '' ''        Call Me._LMRconH.EndAction(frm)
        ' '' ''        Exit Sub
        ' '' ''    End If
        ' '' ''End If

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'パラメータ設定
        prm.ReturnFlg = False

        ''処理開始アクション
        'Me._LMRconH.StartAction(frm)

        Select Case eventShubetsu

            Case LMR010C.EventShubetsu.TORIKOMI '取込

                '取込は1.5次開発のため、未記載

            Case LMR010C.EventShubetsu.KENSAKU  '検索

                '検索処理を行う
                Me.SelectData(frm)

                'ファンクションキーの設定
                Call Me._G.SetFunctionKey(LMR010C.MODE_DEFAULT)

            Case LMR010C.EventShubetsu.MASTER   'マスタ参照

                Dim ds As DataSet = Me.ShowPopup(frm, eventShubetsu.ToString, prm, eventShubetsu)

                '行数設定
                If prm.ReturnFlg = False Then
                    '戻り件数が0件の場合は終了
                    '処理終了アクション
                    Me._LMRconH.EndAction(frm)
                    Exit Sub
                End If

                Me.SetPopupReturn(frm, eventShubetsu.ToString, prm)

            Case LMR010C.EventShubetsu.ENTER 'Enter押下

                Dim ds As DataSet = Me.ShowPopup(frm, eventShubetsu.ToString, prm, eventShubetsu)
                Me.SetPopupReturn(frm, eventShubetsu.ToString, prm)

            Case LMR010C.EventShubetsu.KANRYO   '完了

                Dim rtDs As DataSet = Nothing   '完了の戻り値をセット

                '確認メッセージ
                Dim rtnResult As MsgBoxResult = MyBase.ShowMessage(frm, "C001", New String() {Me._SyubetsuNM})
                If rtnResult = MsgBoxResult.Ok Then

                    'メッセージ情報を初期化する
                    MyBase.ClearMessageStoreData()

                    If Me._Syubetsu = LMR010C.KANRYO_06 Then
                        ' 出荷完了の場合
                        ' Rapidus次回分納情報取得用IN DataTable の設定
                        SetDtJikaiBunnouIn(Me._Ds)
                    End If

                    '完了処理呼び出し
                    rtDs = Me.SaveData(frm, Me._Ds)

                    'メッセージコードの判定
                    If MyBase.IsMessageStoreExist = True Then

                        MyBase.ShowMessage(frm, "E235")
                        'EXCEL起動()
                        MyBase.MessageStoreDownload()

                    Else

                        '2016.01.06 UMANO 英語化対応START
                        'メッセージの表示（処理件数の表示）
                        'MyBase.ShowMessage(frm, "G002", New String() {Me._SyubetsuNM, String.Concat("更新件数", Convert.ToString(rtDs.Tables("LMR010_SAVECNT").Rows(0).Item("SAVECNT")), "件")})
                        MyBase.ShowMessage(frm, "G002", New String() {Convert.ToString(rtDs.Tables("LMR010_SAVECNT").Rows(0).Item("SAVECNT")), Me._SyubetsuNM})
                        '2016.01.06 UMANO 英語化対応END

                        'ファンクションキーの設定
                        Call Me._G.SetFunctionKey(LMR010C.MODE_KANRYO)

                    End If

                End If

        End Select

        '終了アクション
        Me._LMRconH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMR010F) As Boolean

        Return True

    End Function

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMR010F)

        'DataSet設定
        Dim ds As DataSet = New DataSet

        '画面の入力項目のクリア(編集部)
        Me._G.ClearControl()

        'SPREAD(表示行)初期化
        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet
        'rtnDs = Me.CallWSA("LMR010BLF", "SelectListData", ds)

        'Call Me._G.SetSelectListData(rtnDs)

    End Sub

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMR010F)

        '完了種別の保持
        Me._Syubetsu = Convert.ToString(frm.cmbKanryo.SelectedValue)
        Me._SyubetsuNM = Convert.ToString(frm.cmbKanryo.TextValue)
        Me._Eigyosyo = Convert.ToString(frm.CmbEigyo.SelectedValue)

        '閾値の取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)

        Dim drmc As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                 .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0)

        'DataSet設定
        Dim rtDs As DataSet = New LMR010DS()
        Me.SetDataSelectInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Select Case Convert.ToString(frm.cmbKanryo.SelectedValue)
            Case LMR010C.KANRYO_01, LMR010C.KANRYO_02, LMR010C.KANRYO_03  '入荷
                rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form),
                                                  "LMR010BLF", "SelectListINKAData", rtDs, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                   , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))))
            Case LMR010C.KANRYO_04, LMR010C.KANRYO_05, LMR010C.KANRYO_06  '出荷
                rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form),
                                                  "LMR010BLF", "SelectListOUTKAData", rtDs, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                  , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))), "1")
            Case LMR010C.KANRYO_07  '作業
                rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form),
                                                  "LMR010BLF", "SelectListSagyoSijiData", rtDs, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                  , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))))
        End Select

        '検証結果(メモ)№3対応START--------
        '検索成功時共通処理を行う
        'If rtnDs Is Nothing = False Then
        '    Me._Ds = rtnDs
        '    Me.SuccessSelect(frm, rtnDs)
        'Else
        '    MyBase.ShowMessage(frm)
        'End If

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables("LMR010INOUT").Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Me._Ds = rtnDs
            Me.SuccessSelect(frm, rtnDs)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            'START YANAI 要望番号824
            'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
            Me._G.InitSpread()
            'END YANAI 要望番号824
            Me._LMRconH.FailureSelect(frm)
        End If
        '検証結果(メモ)№3対応END--------

        'キャッシュから名称取得
        Call SetCachedName(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectData")

    End Sub

    ''' <summary>
    ''' 初期検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectFirstData(ByVal frm As LMR010F)

        '完了種別の保持
        Me._Syubetsu = Convert.ToString(frm.cmbKanryo.SelectedValue)
        Me._SyubetsuNM = Convert.ToString(frm.cmbKanryo.TextValue)
        Me._Eigyosyo = Convert.ToString(frm.CmbEigyo.SelectedValue)

        '閾値の取得
        Dim dr As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0)

        Dim drmc As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                 .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0)


        'DataSet設定
        Dim rtDs As DataSet = New LMR010DS()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectFirstData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Select Case Convert.ToString(frm.cmbKanryo.SelectedValue)
            Case LMR010C.KANRYO_01, LMR010C.KANRYO_02, LMR010C.KANRYO_03  '入荷
                rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form),
                                                  "LMR010BLF", "SelectListINKAData", Me._Ds, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                   , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))))
            Case LMR010C.KANRYO_04, LMR010C.KANRYO_05, LMR010C.KANRYO_06  '出荷
                rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form),
                                                  "LMR010BLF", "SelectListOUTKAData", Me._Ds, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                   , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))))
            Case LMR010C.KANRYO_07  '作業
                rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form),
                                                  "LMR010BLF", "SelectListSagyoSijiData", Me._Ds, Convert.ToInt32(Convert.ToDouble(dr.Item("VALUE1"))) _
                                                   , Convert.ToInt32(Convert.ToDouble(drmc.Item("VALUE1"))))
        End Select

        '検証結果(メモ)№3対応START--------
        ''検索成功時共通処理を行う
        'If rtnDs Is Nothing = False Then
        '    Me._Ds = rtnDs
        '    Me.SuccessSelect(frm, rtnDs)
        '    'チェックONにする
        '    Me._G.SpreadCheckOn(frm)
        'Else
        '    MyBase.ShowMessage(frm)
        'End If

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables("LMR010INOUT").Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Me._Ds = rtnDs
            Me.SuccessSelect(frm, rtnDs)
            'チェックONにする
            Me._G.SpreadCheckOn(frm)
        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Me._LMRconH.FailureSelect(frm)
        End If
        '検証結果(メモ)№3対応END--------

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectFirstData")

    End Sub

    ''' <summary>
    ''' 荷主検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectCustData(ByVal frm As LMR010F)

        If (LMR010C.KANRYO_03).Equals(Convert.ToString(frm.cmbKanryo.SelectedValue)) = False Then
            Exit Sub
        End If

        Dim dr As DataRow = Me._Ds.Tables(LMR010C.TABLE_NM_CUST_IN).NewRow()
        Dim outDt As DataTable = Me._Ds.Tables(LMR010C.TABLE_NM_INOUT)

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()
        'チェックリスト取得
        list = Me._LMRconV.GetCheckList(frm.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)
        Dim max As Integer = list.Count - 1
        Dim inkaNo As String = String.Empty

        For i As Integer = 0 To max
            '検索条件　入力部（単項目）
            dr = Me._Ds.Tables(LMR010C.TABLE_NM_CUST_IN).NewRow()
            dr("NRS_BR_CD") = frm.CmbEigyo.SelectedValue
            dr("INKA_NO_L") = Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO))
            '検索条件をデータセットに設定
            Me._Ds.Tables(LMR010C.TABLE_NM_CUST_IN).Rows.Add(dr)

        Next

        'DataSet設定
        Dim rtDs As DataSet = New LMR010DS()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCustData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtDs = MyBase.CallWSA("LMR010BLF", "SelectCustData", Me._Ds)

        '検索成功時共通処理を行う
        If rtDs Is Nothing = False Then
            Me._Ds = rtDs
        Else
            MyBase.ShowMessage(frm)
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCustData")

    End Sub

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSelectInData(ByVal frm As LMR010F, ByVal rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMControlC.LMR010C_TABLE_NM_IN).NewRow()

        '検索条件　入力部（単項目）
        dr("KANRYO_SYUBETU") = frm.cmbKanryo.SelectedValue
        dr("NRS_BR_CD") = frm.CmbEigyo.SelectedValue

        dr("TANTO_USER_CD") = frm.txtTantoCD.TextValue
        dr("CUST_CD") = frm.txtCustCD.TextValue
        If String.IsNullOrEmpty(frm.txtCustCD.TextValue) = True Then
            frm.lblCustNM.TextValue = String.Empty
        End If
        dr("INOUTKA_DATE_FROM") = frm.imdNyukaDate_From.TextValue
        dr("INOUTKA_DATE_TO") = frm.imdNyukaDate_To.TextValue

        '検索条件　入力部（スプレッド）
        With frm.sprKanryo.ActiveSheet
            dr("INOUTKA_NO_L") = Me._LMRconG.GetCellValue(.Cells(0, LMR010G.sprKanryoDef.KANRI_NO_L.ColNo))
            dr("INOUTKA_ORD_NO") = Me._LMRconG.GetCellValue(.Cells(0, LMR010G.sprKanryoDef.ORDER_NO.ColNo))
            dr("TANTO_USER_NM") = Me._LMRconG.GetCellValue(.Cells(0, LMR010G.sprKanryoDef.TANTO_USER.ColNo))
            dr("CUST_NM") = Me._LMRconG.GetCellValue(.Cells(0, LMR010G.sprKanryoDef.CUST_NM.ColNo))
            dr("DEST_NM") = Me._LMRconG.GetCellValue(.Cells(0, LMR010G.sprKanryoDef.DEST_NM.ColNo))
        End With

        '検索条件をデータセットに設定
        rtDs.Tables(LMControlC.LMR010C_TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMR010F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMR010C.TABLE_NM_INOUT)

        '画面解除
        MyBase.UnLockedControls(frm)

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Me._G.InitSpread()

        '取得データをSPREADに表示
        Me._G.SetSpread(frm, dt)

        Me._CntSelect = dt.Rows.Count.ToString()

        If 0 < Convert.ToInt32(Me._CntSelect) Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {Me._CntSelect})
        End If

    End Sub

    ''' <summary>
    ''' データセット設定(完了処理時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataKanryoInData(ByVal frm As LMR010F)

        Dim dt As DataTable = Me._Ds.Tables(LMR010C.TABLE_NM_INOUT)
        Dim dr As DataRow() = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            'フラグ初期化
            dt.Rows(i).Item("SYORI_FLG") = LMConst.FLG.OFF
        Next

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()
        'チェックリスト取得
        list = Me._LMRconV.GetCheckList(frm.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)
        max = list.Count - 1

        Select Case Me._Syubetsu
            Case LMR010C.KANRYO_01  '入荷受付

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("INOUTKA_STATE_KB") = LMR010C.INKA_SINTYOKU_30
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

            Case LMR010C.KANRYO_02  '入荷検品

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("INOUTKA_STATE_KB") = LMR010C.INKA_SINTYOKU_40
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

            Case LMR010C.KANRYO_03  '入庫完了

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("INOUTKA_STATE_KB") = LMR010C.INKA_SINTYOKU_50
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

            Case LMR010C.KANRYO_04  '出庫完了

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("INOUTKA_STATE_KB") = LMR010C.OUTKA_SINTYOKU_40
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

            Case LMR010C.KANRYO_05  '出荷検品

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("INOUTKA_STATE_KB") = LMR010C.OUTKA_SINTYOKU_50
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

            Case LMR010C.KANRYO_06  '出荷完了

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("INOUTKA_STATE_KB") = LMR010C.OUTKA_SINTYOKU_60
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

            Case LMR010C.KANRYO_07  '作業完了

                For i As Integer = 0 To max
                    dr = dt.Select(String.Concat("INOUTKA_NO_L = '", Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO)), "'"))
                    dr(0).Item("SYORI_FLG") = LMConst.FLG.ON
                    dr(0).Item("RECORD_NO") = list(i)
                Next

        End Select

    End Sub

    ''' <summary>
    ''' 在庫数の計算
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsZaiNBQT(ByVal ds As DataSet) As Boolean

        Dim outdt As DataTable = ds.Tables("LMR010_OUTKAS_UPDDATA")
        Dim zaidt As DataTable = ds.Tables("LMR010_ZAI_UPDDATA")
        Dim outmax As Integer = outdt.Rows.Count - 1
        Dim zaimax As Integer = zaidt.Rows.Count - 1
        Dim outdr As DataRow = Nothing
        Dim zaidr As DataRow = Nothing

        '更新後の在庫数を求めるために計算
        For i As Integer = 0 To outmax
            outdr = outdt.Rows(i)
            zaidr = zaidt.Select(String.Concat("ZAI_REC_NO = '", outdr.Item("ZAI_REC_NO").ToString, "'"))(0)
            zaidr.Item("PORA_ZAI_NB") = Convert.ToString(
                                                        Convert.ToInt32(zaidr.Item("PORA_ZAI_NB").ToString()) -
                                                        Convert.ToInt32(outdr.Item("ALCTD_NB").ToString()))

            zaidr.Item("ALCTD_NB") = Convert.ToString(
                                                       Convert.ToInt32(zaidr.Item("ALCTD_NB").ToString()) -
                                                       Convert.ToInt32(outdr.Item("ALCTD_NB").ToString()))

            'START YANAI 20110906 サンプル対応
            'zaidr.Item("PORA_ZAI_QT") = Convert.ToDecimal(zaidr.Item("PORA_ZAI_QT").ToString()) _
            '                            - Convert.ToDecimal(outdr.Item("ALCTD_QT").ToString())

            'zaidr.Item("ALCTD_QT") = Convert.ToDecimal(zaidr.Item("ALCTD_QT").ToString()) _
            '                         - Convert.ToDecimal(outdr.Item("ALCTD_QT").ToString())
            If ("04").Equals(outdr.Item("ALCTD_KB").ToString) = True Then
                'サンプルの場合
                zaidr.Item("PORA_ZAI_QT") = zaidr.Item("PORA_ZAI_QT").ToString()

                zaidr.Item("ALCTD_QT") = zaidr.Item("ALCTD_QT").ToString()
                'START YANAI 20110913 小分け対応
            ElseIf ("03").Equals(outdr.Item("ALCTD_KB").ToString) = True Then
                'START YANAI 要望番号681
                'zaidr.Item("PORA_ZAI_QT") = Convert.ToString( _
                '                                            Convert.ToDecimal(zaidr.Item("PORA_ZAI_QT").ToString()) - _
                '                                            Convert.ToDecimal(zaidr.Item("IRIME").ToString()))

                'zaidr.Item("ALCTD_QT") = Convert.ToString( _
                '                                          Convert.ToDecimal(zaidr.Item("ALCTD_QT").ToString()) - _
                '                                          Convert.ToDecimal(zaidr.Item("IRIME").ToString()))
                zaidr.Item("PORA_ZAI_QT") = Convert.ToString(
                                                            Convert.ToDecimal(zaidr.Item("PORA_ZAI_QT").ToString()) -
                                                            Convert.ToDecimal(outdr.Item("ALCTD_QT").ToString()))

                zaidr.Item("ALCTD_QT") = Convert.ToString(
                                                          Convert.ToDecimal(zaidr.Item("ALCTD_QT").ToString()) -
                                                          Convert.ToDecimal(outdr.Item("ALCTD_QT").ToString()))
                'END YANAI 要望番号681
                'END YANAI 20110913 小分け対応

            Else
                'サンプル・小分け以外の場合
                zaidr.Item("PORA_ZAI_QT") = Convert.ToString(
                                                            Convert.ToDecimal(zaidr.Item("PORA_ZAI_QT").ToString()) -
                                                            Convert.ToDecimal(outdr.Item("ALCTD_QT").ToString()))

                zaidr.Item("ALCTD_QT") = Convert.ToString(
                                                         Convert.ToDecimal(zaidr.Item("ALCTD_QT").ToString()) -
                                                         Convert.ToDecimal(outdr.Item("ALCTD_QT").ToString()))
            End If
            'END YANAI 20110906 サンプル対応

        Next

        Return True

    End Function

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <param name="ds">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SaveData(ByVal frm As LMR010F, ByVal ds As DataSet) As DataSet

        MyBase.Logger.StartLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name)

        Dim rtnDs As DataSet = Nothing
        Select Case Me._Syubetsu
            Case LMR010C.KANRYO_01, LMR010C.KANRYO_02, LMR010C.KANRYO_03    '入荷
                '==== WSAクラス呼出（変更処理） ====
                rtnDs = MyBase.CallWSA("LMR010BLF", "UpdateSaveInkaDataAction", ds)

            Case LMR010C.KANRYO_04, LMR010C.KANRYO_05, LMR010C.KANRYO_06    '出荷
                '==== WSAクラス呼出（変更処理） ====
                rtnDs = MyBase.CallWSA("LMR010BLF", "UpdateSaveOutkaDataAction", ds)

            Case LMR010C.KANRYO_07  '作業
                '==== WSAクラス呼出（変更処理） ====
                rtnDs = MyBase.CallWSA("LMR010BLF", "UpdateSaveSagyoSijiDataAction", ds)

        End Select

        MyBase.Logger.EndLog(Me.GetType.Name, Reflection.MethodBase.GetCurrentMethod.Name)

        Return rtnDs

    End Function

    ''' <summary>
    ''' 入力チェックデータ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectCheckINKAData(ByVal frm As LMR010F)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCheckINKAData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Dim rc As Integer = 0
        rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form), "LMR010BLF", "SelectCheckINKAData", Me._Ds, rc)

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Me._Ds = rtnDs

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCheckINKAData")

    End Sub

    ''' <summary>
    ''' データセット設定(入力チェック時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataKanryoCheckInData(ByVal frm As LMR010F)

        Dim dt As DataTable = Me._Ds.Tables(LMR010C.TABLE_NM_IN_CHECK)
        dt.Clear()
        Dim row As DataRow = Nothing
        Dim br As String = Me._Ds.Tables(LMR010C.TABLE_NM_INOUT).Rows(0).Item("NRS_BR_CD").ToString

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()
        'チェックリスト取得
        list = Me._LMRconV.GetCheckList(frm.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)
        Dim max As Integer = list.Count - 1

        For i As Integer = 0 To max
            row = Me._Ds.Tables(LMR010C.TABLE_NM_IN_CHECK).NewRow
            row("NRS_BR_CD") = br
            row("INOUTKA_NO_L") = Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO))
            Me._Ds.Tables(LMR010C.TABLE_NM_IN_CHECK).Rows.Add(row)
        Next

    End Sub

    ''' <summary>
    ''' 在庫チェックデータ検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectCheckZAIData(ByVal frm As LMR010F)

        'DataSet設定
        Me.SetDataKanryoCheckZaiData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCheckZAIData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Dim rc As Integer = 0
        rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form), "LMR010BLF", "SelectCheckZAIData", Me._Ds, rc)

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Me._Ds = rtnDs

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCheckZAIData")

    End Sub

    ''' <summary>
    ''' データセット設定(入力チェック時)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataKanryoCheckZaiData(ByVal frm As LMR010F)

        Dim dt As DataTable = Me._Ds.Tables(LMR010C.TABLE_NM_IN_CHECK)
        dt.Clear()
        Dim row As DataRow = Nothing
        Dim br As String = Me._Ds.Tables(LMR010C.TABLE_NM_INOUT).Rows(0).Item("NRS_BR_CD").ToString

        'チェックリスト格納変数
        Dim list As ArrayList = New ArrayList()
        'チェックリスト取得
        list = Me._LMRconV.GetCheckList(frm.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)
        Dim max As Integer = list.Count - 1

        For i As Integer = 0 To max
            row = Me._Ds.Tables(LMR010C.TABLE_NM_IN_CHECK).NewRow
            row("NRS_BR_CD") = br
            row("INOUTKA_NO_L") = Me._LMRconG.GetCellValue(frm.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO))
            Me._Ds.Tables(LMR010C.TABLE_NM_IN_CHECK).Rows.Add(row)
        Next

    End Sub

    ''' <summary>
    ''' 初期表示時の進捗区分チェックデータ取得
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub StateCheckData(ByVal frm As LMR010F)

        'DataSet設定
        Dim rtDs As DataSet = New LMR010DS()

        Me.SetDataSelectInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "StateCheckData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Dim rootPGID As String = MyBase.RootPGID()


        If (LMR010C.PGID_LMB010).Equals(rootPGID) = True Then
            '遷移元が入荷一覧
            'WSA呼出し
            rtnDs = MyBase.CallWSA("LMR010BLF", "SelectCheckDataInka", Me._Ds)
        ElseIf (LMR010C.PGID_LMC010).Equals(rootPGID) = True Then
            '遷移元が出荷一覧
            'WSA呼出し
            rtnDs = MyBase.CallWSA("LMR010BLF", "SelectCheckDataOutka", Me._Ds)
        End If

        'チェック対象データ取得成功時、チェック処理を行う
        If rtnDs Is Nothing = False Then

            Me._Ds = rtnDs

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "StateCheckData")

    End Sub

    ''' <summary>
    ''' 完了チェックデータ検索処理（出荷データ）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListOUTKADataKANRYO(ByVal frm As LMR010F)

        'DataSet設定
        Me.SetDataKanryoCheckZaiData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListOUTKADataKANRYO")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Dim rc As Integer = 0
        rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form), "LMR010BLF", "SelectListOUTKADataKANRYO", Me._Ds, rc)

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Me._Ds = rtnDs

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListOUTKADataKANRYO")

    End Sub

    ''' <summary>
    ''' 完了チェックデータ検索処理（在庫データ）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListZAIDataKANRYO(ByVal frm As LMR010F)

        'DataSet設定
        Me.SetDataKanryoCheckZaiData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListZAIDataKANRYO")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Nothing
        Dim rc As Integer = 0
        rtnDs = Me._LMRconH.CallWSAAction(DirectCast(frm, Form), "LMR010BLF", "SelectListZAIDataKANRYO", Me._Ds, rc)

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            Me._Ds = rtnDs

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListZAIDataKANRYO")

    End Sub

    ''' <summary>
    ''' Rapidus次回分納情報取得用IN DataTable の設定
    ''' </summary>
    ''' <param name="ds"></param>
    Private Sub SetDtJikaiBunnouIn(ByVal ds As DataSet)

        ds.Tables(LMR010C.TABLE_NM_JIKAI_BUNNOU_IN).Clear()
        Dim dr As DataRow = ds.Tables(LMR010C.TABLE_NM_JIKAI_BUNNOU_IN).NewRow()
        dr.Item("TEMPLATE_PREFIX") = LMZ390C.TEMPLATE_PREFIX
        dr.Item("TEMPLATE_SUFFIX") = LMZ390C.TEMPLATE_SUFFIX
        ds.Tables(LMR010C.TABLE_NM_JIKAI_BUNNOU_IN).Rows.Add(dr)

    End Sub

#End Region '内部メソッド

#Region "PopUp"

    ''' <summary>
    ''' POP起動
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function ShowPopup(ByVal frm As LMR010F, ByVal objNM As String, ByVal prm As LMFormData, ByVal eventshubetsu As LMR010C.EventShubetsu) As DataSet

        Dim value As String = String.Empty

        Select Case frm.ActiveControl.Name

            Case "txtCustCD"  '荷主コード

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                Dim brCd As String = frm.CmbEigyo.SelectedValue().ToString()
                row("NRS_BR_CD") = brCd
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                'START SHINOHARA 要望番号513
                If eventshubetsu = LMR010C.EventShubetsu.ENTER Then
                    row("CUST_CD_L") = frm.txtCustCD.TextValue
                End If
                'END SHINOHARA 要望番号513	
                row("HYOJI_KBN") = LMZControlC.HYOJI_M   '検証結果(メモ)№77対応(2011.09.12)
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs

                If String.IsNullOrEmpty(frm.txtCustCD.TextValue) = True Then
                    frm.lblCustNM.TextValue = String.Empty
                End If

                prm.SkipFlg = Me._PopupSkipFlg

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

                'メッセージの表示
                Me.ShowMessage(frm, "G007")

            Case Else
                'ポップ対象外のテキストの場合
                MyBase.ShowMessage(frm, "G005")

        End Select

        Return prm.ParamDataSet

    End Function

    ''' <summary>
    ''' POPからの戻り値を設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetPopupReturn(ByVal frm As LMR010F, ByVal objNM As String, ByVal prm As LMFormData)

        '戻り値を画面に設定
        Dim prmDs As DataSet = prm.ParamDataSet
        Dim dt As DataTable = Nothing
        Dim max As Integer = 0
        Dim dRow As DataRow = Nothing

        Select Case frm.ActiveControl.Name

            Case "txtCustCD"   '荷主コード
                dt = prmDs.Tables(LMZ260C.TABLE_NM_OUT)
                dRow = dt.Rows(0)

                frm.txtCustCD.TextValue = dRow.Item("CUST_CD_L").ToString()
                frm.lblCustNM.TextValue = dRow.Item("CUST_NM_L").ToString()

        End Select

    End Sub

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMR010F)

        With frm

            '荷主名称
            If String.IsNullOrEmpty(.txtCustCD.TextValue) = False Then
                .lblCustNM.TextValue = GetCachedCust(.txtCustCD.TextValue, "00", "00", "00")
            End If

            '担当者名称
            If String.IsNullOrEmpty(.txtTantoCD.TextValue) = False Then
                .lblTantoNM.TextValue = GetCachedUser(.txtTantoCD.TextValue)
            End If

        End With

    End Sub

    ''' <summary>
    ''' 荷主キャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedCust(ByVal custCdL As String, _
                                   ByVal custCdM As String, _
                                   ByVal custCdS As String, _
                                   ByVal custCdSS As String) As String

        Dim dr As DataRow() = Nothing

        '荷主名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                           "CUST_CD_L = '", custCdL, "' AND " _
                                                                         , "CUST_CD_M = '", custCdM, "' AND " _
                                                                         , "CUST_CD_S = '", custCdS, "' AND " _
                                                                         , "CUST_CD_SS = '", custCdSS, "' AND " _
                                                                         , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("CUST_NM_L").ToString
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' ユーザーキャッシュから名称取得
    ''' </summary>
    ''' <remarks></remarks>
    Private Function GetCachedUser(ByVal userCd As String) As String

        Dim dr As DataRow() = Nothing

        'ユーザー名称
        dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat( _
                                                                            "USER_CD = '", userCd, "' AND " _
                                                                          , "SYS_DEL_FLG = '0'"))
        If 0 < dr.Length Then
            Return dr(0).Item("USER_NM").ToString
        End If

        Return String.Empty

    End Function

#End Region 'PopUp

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(作成処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F5押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey5Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Import JDE")

        ''Import JDE
        'Me.sendPrint(frm, e)

        Logger.EndLog(Me.GetType.Name, "Import JDE")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        'Logger.StartLog(Me.GetType.Name, "")

        'Logger.EndLog(Me.GetType.Name, "")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey9Press")

        '検索処理
        Me.ActionControl(LMR010C.EventShubetsu.KENSAKU, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False
            'START SHINOHARA 要望番号513
            Me.ActionControl(LMR010C.EventShubetsu.ENTER, frm)
            'END SHINOHARA 要望番号513		
        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True
            'START SHINOHARA 要望番号513
            Me.ActionControl(LMR010C.EventShubetsu.MASTER, frm)
            'END SHINOHARA 要望番号513		
        End If


        'マスタ参照処理
        'START SHINOHARA 要望番号513　コメントアウト
        'Me.ActionControl(LMR010C.EventShubetsu.MASTER, frm)
        'END SHINOHARA 要望番号513		

        Logger.EndLog(Me.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "FunctionKey11Press")

        '完了処理
        Me.ActionControl(LMR010C.EventShubetsu.KANRYO, frm)

        Logger.EndLog(Me.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMR010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMR010F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(Me.GetType.Name, "ClosingForm")

        MyBase.Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class