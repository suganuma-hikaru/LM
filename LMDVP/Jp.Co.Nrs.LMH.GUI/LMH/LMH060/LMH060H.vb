' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH060  : EDI出荷データ荷主コード設定
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win

''' <summary>
''' LMH060ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMH060H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMH060V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH060G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHConG As LMHControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHConH As LMHControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMHconV As LMHControlV

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
        Dim frm As LMH060F = New LMH060F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMHConG = New LMHControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMHconV = New LMHControlV(Me, DirectCast(frm, Form), Me._LMHConG)

        'Hnadler共通クラスの設定
        Me._LMHConH = New LMHControlH(DirectCast(frm, Form), MyBase.GetPGID)

        'Gamenクラスの設定
        Me._G = New LMH060G(Me, frm)

        'Validateクラスの設定
        Me._V = New LMH060V(Me, frm, Me._LMHconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0), prmDs)

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' イベントコントロール
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub ActionControl(ByVal eventShubetsu As LMH060C.EventShubetsu, ByVal frm As LMH060F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If Me._V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMH060C.EventShubetsu.NINUSHISET    '荷主セット

                '処理開始アクション
                Me._LMHConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMHConH.EndAction(frm)
                    Exit Sub
                End If

                '荷主セット処理
                Call Me.NinushiSetData(frm)

                '処理終了アクション
                Me._LMHConH.EndAction(frm)

            Case LMH060C.EventShubetsu.CANCEL    'キャンセル

                '処理開始アクション
                Me._LMHConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMHConH.EndAction(frm)
                    Exit Sub
                End If

                'キャンセル処理
                Call Me.CancelData(frm)

                '処理終了アクション
                Me._LMHConH.EndAction(frm)

            Case LMH060C.EventShubetsu.KENSAKU    '検索

                '処理開始アクション
                Me._LMHConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMHConH.EndAction(frm)
                    Exit Sub
                End If

                '検索処理
                Call Me.KensakuData(frm)

                '処理終了アクション
                Me._LMHConH.EndAction(frm)

            Case LMH060C.EventShubetsu.HOZON    '登録

                '処理開始アクション
                Me._LMHConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMHConH.EndAction(frm)
                    Exit Sub
                End If

                '登録処理
                Call Me.HozonData(frm)

                '処理終了アクション
                Me._LMHConH.EndAction(frm)

            Case LMH060C.EventShubetsu.MASTER    'マスタ参照

                '処理開始アクション
                Me._LMHConH.StartAction(frm)

                '現在フォーカスのあるコントロール名の取得
                Dim objNm As String = frm.FocusedControlName()

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMHConH.EndAction(frm)
                    Exit Sub
                End If

                'ポップアップ表示処理
                Call Me.ShowPopup(frm, LMH060C.EventShubetsu.MASTER, prm)

                '処理終了アクション
                Me._LMHConH.EndAction(frm)

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMH060F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F2押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByVal frm As LMH060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey2Press")

        Me.ActionControl(LMH060C.EventShubetsu.NINUSHISET, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey2Press")

    End Sub

    ''' <summary>
    ''' F3押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey3Press(ByVal frm As LMH060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey3Press")

        Me.ActionControl(LMH060C.EventShubetsu.CANCEL, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey3Press")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByVal frm As LMH060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey9Press")

        Me.ActionControl(LMH060C.EventShubetsu.KENSAKU, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey9Press")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByVal frm As LMH060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey10Press")

        'イベント判定
        If e.KeyCode = Keys.Enter Then
            'Enterキー押下時イベント：１件時表示なし
            Me._PopupSkipFlg = False

        Else
            'F10押下時イベント：１件時表示あり
            Me._PopupSkipFlg = True

        End If

        Me.ActionControl(LMH060C.EventShubetsu.MASTER, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey10Press")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByVal frm As LMH060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey11Press")

        Me.ActionControl(LMH060C.EventShubetsu.HOZON, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey11Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMH060F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey12Press")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey12Press")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByVal frm As LMH060F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 荷主セット処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub NinushiSetData(ByVal frm As LMH060F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH060DS()

        'InDataSetの場合
        Call Me.SetInHozonData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "NinushiSetData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH060BLF", "UpdateNinushiSet", rtDs)

        'メッセージコードの判定(EXCELにエラーが設定されている場合)
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"荷主セット処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "NinushiSetData")

    End Sub

    ''' <summary>
    ''' キャンセル処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub CancelData(ByVal frm As LMH060F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH060DS()

        'InDataSetの場合
        Call Me.SetInHozonData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "CancelData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH060BLF", "UpdateCancel", rtDs)

        'メッセージコードの判定(EXCELにエラーが設定されている場合)
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"キャンセル処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "CancelData")

    End Sub

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub HozonData(ByVal frm As LMH060F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH060DS()

        'InDataSetの場合
        Call Me.SetInHozonData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "HozonData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMH060BLF", "UpdateHozon", rtDs)

        'メッセージコードの判定(EXCELにエラーが設定されている場合)
        If MyBase.IsMessageStoreExist = True Then

            MyBase.ShowMessage(frm, "E235")

            'EXCEL起動()
            MyBase.MessageStoreDownload()
            Exit Sub
        End If

        'エラー時はメッセージを表示して終了
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Exit Sub
        End If

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G002", New String() {"登録処理", ""})

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "HozonData")

    End Sub

    ''' <summary>
    ''' マスタ参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開く場合のコントロールです。</remarks>
    Private Sub ShowPopup(ByVal frm As LMH060F, ByVal eventShubetsu As LMH060C.EventShubetsu, ByRef prm As LMFormData)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()

        'オブジェクト名による分岐
        Select Case objNm
            Case "txtCustCdL", "txtCustCdM" '荷主マスタ参照

                Dim prmDs As DataSet = New LMZ260DS
                Dim row As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow
                row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
                If Me._PopupSkipFlg = False Then
                    row("CUST_CD_L") = frm.txtCustCdL.TextValue
                    row("CUST_CD_M") = frm.txtCustCdM.TextValue
                End If
                row("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
                row("HYOJI_KBN") = LMZControlC.HYOJI_S
                prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(row)
                prm.ParamDataSet = prmDs
                prm.SkipFlg = Me._PopupSkipFlg

                frm.lblCustNmL.TextValue = String.Empty
                frm.lblCustNmM.TextValue = String.Empty

                'POP呼出
                LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

            Case Else
                'ポップ対象外のテキストの場合
                MyBase.ShowMessage(frm, "G005")
                Exit Sub

        End Select


        '戻り処理
        If prm.ReturnFlg = True Then
            'オブジェクト名による分岐
            Select Case objNm
                Case "txtCustCdL", "txtCustCdM" '荷主マスタ参照

                    'PopUpから取得したデータをコントロールにセット
                    With prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
                        frm.txtCustCdL.TextValue = .Item("CUST_CD_L").ToString()      '荷主コード（大）
                        frm.lblCustNmL.TextValue = .Item("CUST_NM_L").ToString()      '荷主名（大）
                        frm.txtCustCdM.TextValue = .Item("CUST_CD_M").ToString()      '荷主コード（中）
                        frm.lblCustNmM.TextValue = .Item("CUST_NM_M").ToString()      '荷主名（中）
                    End With

            End Select
        End If

        MyBase.ShowMessage(frm, "G006")

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub KensakuData(ByVal frm As LMH060F)

        'DataSet設定
        Dim rtDs As DataSet = New LMH060DS()

        'スプレッドの行をクリア
        frm.sprDetails.CrearSpread()

        'InDataSetの場合
        Call Me.SetInKensakuData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectKensakuData")

        '閾値の設定
        Dim lc As Integer = Convert.ToInt32(Convert.ToDouble( _
                              MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                              .Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1")))
        MyBase.SetLimitCount(lc)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMHConH.CallWSAAction(DirectCast(frm, Form) _
                                                         , "LMH060BLF", "SelectKensakuData", rtDs _
                                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs IsNot Nothing Then
            Call Me.SuccessSelect(frm, rtnDs)
        End If

        If 1 < frm.sprDetails.ActiveSheet.Rows.Count Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G002", New String() {"検索処理", ""})
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectKensakuData")

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理（画面別）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMH060F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMH060C.TABLE_NM_OUT)

        '画面解除
        Call MyBase.UnLockedControls(frm)

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInKensakuData(ByVal frm As LMH060F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMH060C.TABLE_NM_IN).NewRow()

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue '営業所コード
        dr("CUST_CD_LX") = LMH060C.CUST_CD_LX '荷主コード(大)(X固定)
        dr("CUST_CD_MX") = LMH060C.CUST_CD_MX '荷主コード(中)(X固定)
        dr("CUST_CD") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.CUSTCD.ColNo)) '荷主コード
        dr("CUST_NM") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.CUSTNM.ColNo)) '荷主名
        dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.EDICTLNO.ColNo)) 'EDI番号
        dr("OUTKA_PLAN_DATE") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.OUTKAPLANDATE.ColNo)) '出荷予定日
        dr("DEST_CD") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.DESTCD.ColNo)) '届先コード
        dr("DEST_NM") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.DESTNM.ColNo)) '届先名
        dr("ZBUKA_CD") = Me._LMHconV.GetCellValue(frm.sprDetails.ActiveSheet.Cells(0, LMH060G.sprDetailsDef.ZBUKACD.ColNo)) '在庫部課コード

        '検索条件をデータセットに設定
        rtDs.Tables(LMH060C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(荷主セット、キャンセル、登録)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInHozonData(ByVal frm As LMH060F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMH060C.TABLE_NM_IN).NewRow()

        With frm.sprDetails.ActiveSheet
            Dim arr As ArrayList = Nothing
            arr = Me.GetCheckList(LMH060G.sprDetailsDef.DEF.ColNo, frm.sprDetails)
            Dim max As Integer = arr.Count - 1

            Dim intRow As Integer = 0

            For i As Integer = 0 To max
                dr = rtDs.Tables(LMH060C.TABLE_NM_IN).NewRow()

                intRow = Convert.ToInt32(arr(i).ToString)

                dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue '営業所コード
                dr("CUST_CD_L") = frm.txtCustCdL.TextValue  '荷主コード(大)
                dr("CUST_CD_M") = frm.txtCustCdM.TextValue  '荷主コード(中)
                dr("CUST_CD_LX") = LMH060C.CUST_CD_LX '荷主コード(大)(X固定)
                dr("CUST_CD_MX") = LMH060C.CUST_CD_MX '荷主コード(中)(X固定)
                dr("EDI_CTL_NO") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.EDICTLNO.ColNo)) 'EDI番号
                dr("OUTKA_PLAN_DATE") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.OUTKAPLANDATE.ColNo)).Replace("/", String.Empty) '出荷予定日
                dr("DEST_CD") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.DESTCD.ColNo)) '届先コード
                dr("DEST_NM") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.DESTNM.ColNo)) '届先名
                dr("ZBUKA_CD") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.ZBUKACD.ColNo)) '在庫部課コード
                dr("CUST_CD_L_EDIL") = Mid(Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.CUSTCD.ColNo)), 1, 5) '現在荷主コード(大)
                dr("CUST_CD_M_EDIL") = Mid(Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.CUSTCD.ColNo)), 7, 2) '現在荷主コード(中)
                dr("CUST_CD_L_UPD") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.CUSTCDUPD.ColNo)) '対象荷主コード
                dr("RCV_NM_HED") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.RCVNMHED.ColNo)) '更新対象テーブル名
                dr("SYS_UPD_DATE") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.SYSUPDDATE.ColNo)) '更新日付
                dr("SYS_UPD_TIME") = Me._LMHconV.GetCellValue(.Cells(intRow, LMH060G.sprDetailsDef.SYSUPDTIME.ColNo)) '更新時間
                dr("ROW_NO") = intRow


                'データセットに設定
                rtDs.Tables(LMH060C.TABLE_NM_IN).Rows.Add(dr)
            Next

        End With

    End Sub

#End Region

#Region "その他処理"

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal defNo As Integer, ByVal sprDetail As Spread.LMSpreadSearch) As ArrayList

        Return Me._LMHconV.SprSelectList(defNo, sprDetail)

    End Function

#End Region

#End Region 'Method

End Class
