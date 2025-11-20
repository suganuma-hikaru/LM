' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG010H : 保管料/荷役料計算
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Win.Base      '2021/06/28 
Imports Jp.Co.Nrs.LM.Utility    '2021/06/28 

''' <summary>
''' LMG010ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
Public Class LMG010H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG010V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG010G

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
        Dim frm As LMG010F = New LMG010F(Me)

        '画面共通クラスの設定
        Me._LMGConG = New LMGControlG(DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMG010V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMG010G(Me, frm, Me._LMGConG)

        'Validateクラスの設定
        Me._LMGConV = New LMGControlV(Me, DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMGConH = New LMGControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMGConV, Me._LMGConG)

        'EnterKey制御
        frm.KeyPreview = True

        'フォームの初期化
        Call Me.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        'SBS高道）初期値の設定（当月－1）　
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
    Private Sub UndotheLastCalclation(ByVal frm As LMG010F)

        '取得用データセット
        Dim rtnDs As DataSet = New DataSet

        'スタート処理
        Call Me._LMGConH.StartAction(frm)

        'チェック処理
        If IsCheckCall(frm, LMG010C.EventShubetsu.ZENKEISANTORI) = False Then
            Exit Sub
        End If

        '前回計算取消処理確認
        Select Case MyBase.ShowMessage(frm, "C001", New String() {"前回計算分のデータ取消"})
            Case MsgBoxResult.Cancel  '「キャンセル」押下時
                Call Me._LMGConH.EndAction(frm)
                MyBase.UnLockedControls(frm)
                Exit Sub
        End Select

        '画面のロック制御
        MyBase.LockedControls(frm)

        '排他用データセット設定
        Call Me.SetCalclation(frm)

        '更新・削除処理（排他）
        MyBase.CallWSA("LMG010BLF", "CheckHaitaMCust", prmDs)

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMGConH.EndAction(frm)  '終了処理
            MyBase.UnLockedControls(frm)
            Exit Sub
        End If

        '更新・削除処理
        rtnDs = MyBase.CallWSA("LMG010BLF", "UpDateDel", prmDs)

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMGConH.EndAction(frm)  '終了処理
            MyBase.UnLockedControls(frm)
            Exit Sub
        End If

        'メッセージの表示
        ShowMessage(frm, "G002", New String() {"前回計算取消処理", ""})

        Call Me._LMGConH.EndAction(frm)  '終了処理

        '画面ロック解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 処理結果詳細呼出処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CallProcessResults(ByVal frm As LMG010F)

        '権限チェック
        If Me._V.IsAuthorityChk(LMG010C.EventShubetsu.JOUKYOUSHOSAI) = False Then
            Exit Sub
        End If
        '2011/08/02 菱刈 処理開始アクション追加 スタート
        'スタート処理
        Call Me._LMGConH.StartAction(frm)
        '2011/08/02 菱刈 処理開始アクション追加 エンド
        '全画面ロック
        Call MyBase.LockedControls(frm)

        '状況詳細画面用DataSet設定
        Call Me.SetProcessResults(frm)

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub Execute(ByVal frm As LMG010F)

        'スタートアクション
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG010C.EventShubetsu.JIKKOU) = False Then
            Exit Sub
        End If

        '単価未承認チェック
        If IsCheckApprovalTanka(frm) = False Then
            Exit Sub
        End If

        '変動保管料チェック
        If IsCheckvar(frm) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '排他チェック用
        Call Me.SetCalclation(frm)

        '実行用データセット設定
        Call Me.SetExecute(frm)

        '登録処理（保管荷役計算管理ワークヘッダ）
        Dim ds As DataSet = MyBase.CallWSA("LMG010BLF", "InsertWorkHead", prmDs)

        'メッセージコードの判定
        If MyBase.IsMessageStoreExist = True Then

            'ＣＳＶ出力処理
            MyBase.MessageStoreDownload()

            ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt START
            '排他処理全件エラーの場合、エラーメッセージを表示し、処理を終了する
            If MyBase.IsErrorMessageExist() = True Then
                MyBase.ShowMessage(frm)

                Call Me._LMGConH.EndAction(frm)  '終了処理
                MyBase.UnLockedControls(frm)
                Exit Sub
            End If
            ' 2011/09/05 SBS)Takamichi Hiata_ErrCnt END
        End If

        If LMG010C.ONLINE.Equals(frm.cmbBatch.SelectedValue) Then
            'オンライン実行の場合
            '計算処理
            Call Me.SetDelegate(ds)

            ShowMessage(frm, "G043")
        Else
            '夜間実行の場合
            ShowMessage(frm, "G049")
        End If

        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMG010F)

        'スタートアクション
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG010C.EventShubetsu.KENSAKU) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        'スプレッドのクリア
        frm.sprDetail.CrearSpread()

        'DataSet設定
        Dim ds As DataSet = New DataSet
        Call Me.SetSearchData(frm)

        'SPREAD(表示行)初期化
        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        rtnDs = Me._LMGConH.CallWSAAction(DirectCast(frm, Form) _
                                           , "LMG010BLF", "SelectListData", prmDs _
                                           , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                             (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                           , Convert.ToInt32(Convert.ToDouble(
                                             MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))


        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            'データテーブルの取得
            Dim dt As DataTable = rtnDs.Tables(LMG010C.TABLE_NM_OUT)
            If "0".Equals(dt.Rows.Count.ToString()) = False Then

                '取得データをスプレッドに反映
                Call Me._G.SetSelectListData(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G016", New String() {dt.Rows.Count.ToString()})

            End If
        End If

        frm.lblCustNm.TextValue = String.Empty

        'フォーム荷主名称の設定
        Call Me._G.SetCustName()

        '画面ロック解除
        Call MyBase.UnLockedControls(frm)
    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMG010F, ByVal eventShubetsu As LMG010C.EventShubetsu)

        'Pop起動処理：１件時表示あり
        Me._PopupSkipFlg = True
        Call Me.MasterShowEvent(frm, eventShubetsu)

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub MasterShowEvent(ByVal frm As LMG010F, ByVal eventShubetsu As LMG010C.EventShubetsu)

        'スタートアクション
        Call Me._LMGConH.StartAction(frm)

        ''権限・項目チェック
        'START SHINOHARA 要望番号513
        'If Me.IsCheckCall(frm, LMG010C.EventShubetsu.MASTER) = False Then
        '    Call Me._LMGConH.EndAction(frm) '終了処理
        '    Exit Sub
        'End If
        'END SHINOHARA 要望番号513		

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '現在フォーカスのあるコントロール名の取得
        Dim objNm As String = frm.FocusedControlName()
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        If String.IsNullOrEmpty(objNm) = True Then
            '荷主コード(大・中）以外の場合はメッセージ表示
            MyBase.ShowMessage(frm, "G005")
        End If

        Select Case objNm
            Case frm.txtCustCdL.Name

                If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = True AndAlso
                 String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = True Then

                    '荷主名称をクリア
                    frm.lblCustNm.TextValue = String.Empty
                    '2011/08/01 菱刈 検証一覧No3 スタート
                End If

                '荷主コード
                Call Me.ShowCustPopup(frm, objNm, prm, eventShubetsu)

                'End If
                '2011/08/01 菱刈 検証一覧No3 エンド

            Case frm.txtCustCdM.Name
                If String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = True AndAlso
                 String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = True Then

                    '荷主名称をクリア
                    frm.lblCustNm.TextValue = String.Empty

                    '2011/08/01 菱刈 検証一覧No3 スタート
                End If
                '荷主コード
                Call Me.ShowCustPopup(frm, objNm, prm, eventShubetsu)

                ' End If
                '2011/08/01 菱刈 検証一覧No3 スタート
            Case Else
                '荷主コード(大・中）以外の場合はメッセージ表示
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
    Friend Function CloseForm(ByVal frm As LMG010F) As Boolean

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
        MyBase.CallWSA("LMG010BLF", "CalcBatch", ds)

        Return 0

    End Function

    ''' <summary>
    ''' 荷主マスタ照会(LMZ260)参照
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="objNM"></param>
    ''' <param name="prm"></param>
    ''' <remarks></remarks>
    Private Sub ShowCustPopup(ByVal frm As LMG010F, ByVal objNM As String, ByRef prm As LMFormData, ByVal eventShubetsu As LMG010C.EventShubetsu)

        Dim prmDs As DataSet = New LMZ260DS()

        'パラメータ生成
        Dim dr As DataRow = prmDs.Tables(LMZ260C.TABLE_NM_IN).NewRow()
        dr.Item("NRS_BR_CD") = frm.cmbBr.SelectedValue.ToString
        'START SHINOHARA 要望番号513
        If eventShubetsu = LMG010C.EventShubetsu.ENTER Then
            dr.Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            dr.Item("CUST_CD_M") = frm.txtCustCdM.TextValue
        End If
        'END SHINOHARA 要望番号513		
        dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        dr.Item("HYOJI_KBN") = LMZControlC.HYOJI_S   '検証結果(メモ)№77対応(2011.09.12)
        prmDs.Tables(LMZ260C.TABLE_NM_IN).Rows.Add(dr)
        prm.ParamDataSet = prmDs
        prm.SkipFlg = Me._PopupSkipFlg

        '荷主マスタ照会(LMZ260)POP呼出
        LMFormNavigate.NextFormNavigate(Me, "LMZ260", prm)

        '戻り処理
        If prm.ReturnFlg = True Then
            'PopUpから取得したデータをコントロールにセット
            frm.txtCustCdL.TextValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_L").ToString()    '荷主コード大
            frm.txtCustCdM.TextValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_CD_M").ToString()    '荷主コード中
            frm.lblCustNm.TextValue = String.Concat(prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_L").ToString() _
            , " ", prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CUST_NM_M").ToString())
            'START YANAI 要望番号558
            frm.cmbSimebi.SelectedValue = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("CLOSE_KB").ToString()    '締日区分
            Dim hokanNiyakuDate As Integer = Convert.ToInt32(prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0).Item("HOKAN_NIYAKU_CALCULATION").ToString())
            frm.imdInvDate.TextValue = Me._LMGConG.SetControlDate(DateAdd("m", 1, (hokanNiyakuDate).ToString("0000/00/00")).ToString("yyyyMMdd"), 0) '請求月
            'END YANAI 要望番号558
        End If

    End Sub

    ''' <summary>
    ''' チェック（権限・項目）
    ''' </summary>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMG010F, ByVal SHUBETSU As LMG010C.EventShubetsu) As Boolean

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
            Case LMG010C.EventShubetsu.JIKKOU, LMG010C.EventShubetsu.ZENKEISANTORI
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
    Private Sub EnterkeyControl(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String, ByVal eventShubetsu As LMG010C.EventShubetsu)

        'マスタ検索フラグ
        Dim MasterFlg As Boolean = False

        If e.KeyCode = Keys.Enter Then
            Select Case controlNm
                Case frm.txtCustCdL.Name, frm.txtCustCdM.Name     'カーソルが荷主コード（大）の場合
                    '荷主コード（大）にデータが入力されていない場合
                    If String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = False OrElse
                    String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = False Then

                        MasterFlg = True

                    Else
                        '2011/08/01 菱刈 検証結果一覧 No3(マスタ参照) スタート
                        '荷主(大)、荷主(中)に両方値が入っていない場合
                        frm.lblCustNm.TextValue = String.Empty

                    End If

                    'Case frm.txtCustCdM.Name      'カーソルが荷主コード（中）の場合
                    '    '荷主コード（中）にデータが入力されていない場合
                    '    If String.IsNullOrEmpty(frm.txtCustCdM.TextValue) = False OrElse _
                    '    String.IsNullOrEmpty(frm.txtCustCdL.TextValue) = False Then

                    '        MasterFlg = True

                    '    End If
                    '2011/08/01 菱刈 検証結果一覧 No3(マスタ参照) エンド
                Case Else                      'カーソルが荷主コード以外の場合

                    'EnterKeyによるタブ遷移
                    frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
                    Exit Sub
            End Select

            'マスタ検索フラグ　がTRUEの場合検索処理を行う
            If MasterFlg = True Then

                'Pop起動処理：１件時表示なし
                Me._PopupSkipFlg = False
                Me.MasterShowEvent(frm, eventShubetsu)

            Else '上記以外の場合タブ遷移

                'EnterKeyによるタブ遷移
                frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
            End If

        End If

    End Sub

    ''' <summary>
    ''' 実行モードオプションボタン変更時処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ChangeCheckBox(ByVal frm As LMG010F)

        Select Case True

            Case frm.optSeikyuC.Checked                       '実行モード＝チェックの場合

                '入出荷未完了チェックボックスを活性化する
                frm.chkMikan.Enabled = True

            Case Else                                         '実行モード≠チェックの場合

                '入出荷未完了チェックボックスを非活性化する
                frm.chkMikan.Enabled = False

                '入出荷未完了チェックボックスのチェックを外す
                frm.chkMikan.Checked = False

        End Select

    End Sub

    ''' <summary>
    ''' 単価未承認チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function IsCheckApprovalTanka(ByVal frm As LMG010F) As Boolean

        'スプレッド選択行の取得（リスト）
        Dim chkList As ArrayList = Me._LMGConH.GetCheckList(frm.sprDetail.ActiveSheet, LMG010G.sprDetailDef.DEF.ColNo)

        'データの設定
        With frm.sprDetail.ActiveSheet
            For i As Integer = 0 To chkList.Count() - 1
                Dim num As Integer = Convert.ToInt32(chkList(i))

                Dim chkDs As DataSet = Me.prmDs.Copy()
                Dim chkDt As DataTable = chkDs.Tables(LMG010C.TABLE_NM_CHK_APPROVAL_TANKA)
                Dim dr As DataRow = chkDt.NewRow()
                dr.Item("ROW_NO") = chkList(i)
                dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("CUST_CD_L") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_M.ColNo))
                chkDt.Rows.Add(dr)

                'データの取得
                chkDs = MyBase.CallWSA("LMG010BLF", "SelectChkApprovalTanka", chkDs)

                If MyBase.IsMessageExist() = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm)
                    Return False
                End If

                '取得できたデータはエラー
                If chkDs.Tables(LMG010C.TABLE_NM_CHK_APPROVAL_TANKA).Rows.Count > 0 Then
                    '最初のエラー行に位置付け
                    Dim row As Integer = Convert.ToInt32(chkDs.Tables(LMG010C.TABLE_NM_CHK_APPROVAL_TANKA).Rows(0).Item("ROW_NO").ToString())
                    Dim col As Integer = LMG010G.sprDetailDef.CUST_CD.ColNo
                    Me._LMGConV.SetErrorControl(frm.sprDetail, row, col)

                    'メッセージの表示
                    Me.ShowMessage(frm, "E999", New String() {"承認されていない単価があります。単価マスタ"})
                    Return False
                End If
            Next
        End With

        Return True

    End Function

    ''' <summary>
    ''' 変動保管料チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Function IsCheckvar(ByVal frm As LMG010F) As Boolean

        '区分マスタから変動保管料適用年月を取得
        Dim varApplyYm As String = "999999"
        Dim kbnDrs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G021' AND KBN_CD = '01'"))
        If kbnDrs.Length > 0 Then
            varApplyYm = kbnDrs(0).Item("KBN_NM1").ToString()
        End If

        'スプレッド選択行の取得（リスト）
        Dim chkList As ArrayList = Me._LMGConH.GetCheckList(frm.sprDetail.ActiveSheet, LMG010G.sprDetailDef.DEF.ColNo)

        'データの設定
        With frm.sprDetail.ActiveSheet
            For i As Integer = 0 To chkList.Count() - 1
                Dim num As Integer = Convert.ToInt32(chkList(i))

                Dim chkDs As DataSet = Me.prmDs.Copy()
                Dim chkDt As DataTable = chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR)

                Dim invDateTo As String = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.INV_DATE.ColNo))
                If Left(invDateTo, 6) < varApplyYm Then
                    Continue For
                End If

                Dim dr As DataRow = chkDt.NewRow()
                dr.Item("ROW_NO") = chkList(i)
                dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("CUST_CD_L") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_M.ColNo))
                dr.Item("CUST_CD_S") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_S.ColNo))
                dr.Item("CUST_CD_SS") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_SS.ColNo))
                chkDt.Rows.Add(dr)

                'データの取得
                chkDs = MyBase.CallWSA("LMG010BLF", "SelectChkVar", chkDs)

                If MyBase.IsMessageExist() = True Then
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm)
                    Return False
                End If

                '取得できたデータのエラーチェック
                If chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR).Rows.Count > 0 Then
                    Dim hasError As Boolean = False

                    'チェック内容別メッセージの表示
                    Do
                        If chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR).Rows.Count >= 2 Then
                            Me.ShowMessage(frm, "E375", New String() {"変動保管料ありの請求先の変動倍率適用経過月数(区分分類コードG022)が複数件登録されている", "実行"})
                            hasError = True
                            Exit Do
                        End If
                        Dim kbnKeikaMonthValue1 As String = chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR).Rows(0).Item("KBN_KEIKA_MONTH_VALUE1").ToString()
                        Dim kbnKeikaMonthValue2 As String = chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR).Rows(0).Item("KBN_KEIKA_MONTH_VALUE2").ToString()
                        If IsNumeric(kbnKeikaMonthValue1) = False OrElse IsNumeric(kbnKeikaMonthValue2) = False Then
                            Me.ShowMessage(frm, "E375", New String() {"変動保管料ありの請求先の変動倍率適用経過月数(区分分類コードG022)が登録されていない", "実行"})
                            hasError = True
                            Exit Do
                        End If
                        If Convert.ToDecimal(kbnKeikaMonthValue1) = 0D OrElse Convert.ToDecimal(kbnKeikaMonthValue1) = 0D Then
                            Me.ShowMessage(frm, "E375", New String() {"変動保管料ありの請求先の変動倍率適用経過月数(区分分類コードG022)の設定値(数値1,2)がゼロの", "実行"})
                            hasError = True
                            Exit Do
                        End If
                        If chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR).Rows(0).Item("SAITEI_HAN_KB").ToString() <> "03" Then
                            Me.ShowMessage(frm, "E02N")
                            hasError = True
                            Exit Do
                        End If
                        Exit Do
                    Loop
                    If hasError Then
                        '最初のエラー行に位置付け
                        Dim row As Integer = Convert.ToInt32(chkDs.Tables(LMG010C.TABLE_NM_CHK_VAR).Rows(0).Item("ROW_NO").ToString())
                        Dim col As Integer = LMG010G.sprDetailDef.CUST_CD.ColNo
                        Me._LMGConV.SetErrorControl(frm.sprDetail, row, col)
                        Return False
                    End If
                End If
            Next
        End With

        Return True

    End Function

#End Region '内部処理

#Region "DataSet"

    ''' <summary>
    ''' 処理詳細情報用DataSet
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetProcessResults(ByVal frm As LMG010F)
        '2011/08/08 菱刈　検証結果一覧No3 スタート
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        'データセット設定をコメント化
        ''データテーブル
        'prmDs = New LMG080DS()
        'Dim datatable As DataTable = Me.prmDs.Tables(LMG080C.TABLE_NM_IN)
        'Dim dr As DataRow = datatable.NewRow()

        'dr.Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON

        ''データの設定
        'datatable.Rows.Add(dr)

        'prm.ParamDataSet = prmDs
        '2011/08/08 菱刈 検証結果一覧No3 エンド
        LMFormNavigate.NextFormNavigate(Me, "LMG080", prm)

        If prm.ReturnFlg = True Then

            Call MyBase.UnLockedControls(frm)

        End If

    End Sub

    ''' <summary>
    ''' 実行用データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetExecute(ByVal frm As LMG010F)

        '請求フラグ
        Dim SEKY_FLG As String = String.Empty

        '入出荷未完了含フラグ
        Dim MIKAN_INC_FLG As String = "0"

        '2011/08/03 菱刈　請求月の取得方法変更 スタート
        '請求月
        ' Dim SEIKYUDATE As String = String.Empty

        'バッチ条件
        Dim BatchKb As String = String.Empty

        '締日区分
        ' Dim CLOSE_KB As String = String.Empty

        With frm
            '請求フラグ判定
            Select Case True
                Case .optSeikyuH.Checked
                    SEKY_FLG = "00"
                Case .optSeikyuC.Checked
                    SEKY_FLG = "01"
                Case .optSeikyuGC.Checked
                    SEKY_FLG = "02"
            End Select

            '入出荷未完了含フラグ
            If .chkMikan.Checked = True Then
                MIKAN_INC_FLG = "1"
            End If

            'バッチ条件
            BatchKb = frm.cmbBatch.SelectedValue.ToString()

        End With

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'スプレッド取得行の設定
        Dim num As Integer = 0

        'スプレッド選択行の取得（リスト）
        Dim chkList As ArrayList = Me._LMGConH.GetCheckList(frm.sprDetail.ActiveSheet, _
                                                            LMG010G.sprDetailDef.DEF.ColNo)
        '選択行の行数取得
        Dim max As Integer = chkList.Count() - 1

        'データテーブルの宣言
        Dim datatable As DataTable = Me.prmDs.Tables(LMG010C.TABLE_NM_EXECUTE)

        With frm.sprDetail.ActiveSheet
            For i As Integer = 0 To max
                num = Convert.ToInt32(chkList(i))
                '締日区分
                'CLOSE_KB = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CLOSE_KB.ColNo))

                ''請求日
                'SEIKYUDATE = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.INV_DATE.ColNo))

                ''今回請求日の設定（末日の場合）
                'If "00".Equals(CLOSE_KB) = True Then
                '    Dim day As String = String.Concat(SEIKYUDATE.Substring(0, 6), "01")
                '    SEIKYUDATE = Date.Parse(Format(CInt(day), "0000/00/00")).AddMonths(1).AddDays(-1).ToString
                '    SEIKYUDATE = SEIKYUDATE.Substring(0, 10).Replace("/", "")
                'Else
                '    SEIKYUDATE = String.Concat(SEIKYUDATE, CLOSE_KB)
                'End If

                num = Convert.ToInt32(chkList(i))
                Dim dr As DataRow = datatable.NewRow()
                dr.Item("SEKY_FLG") = SEKY_FLG
                '2011/08/08 菱刈 バッチ番号はサーバー側で取得しているためコメント化 スタートAS NRS_BR_CD
                'dr.Item("BATCH_NO") = String.Concat(Me.GetSystemDate(), Me.GetSystemTime())
                '2011/08/08 菱刈 バッチ番号はサーバー側で取得しているためコメント化 エンド
                '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
                'dr.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
                dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("OPE_USER_CD") = LMUserInfoManager.GetUserID()
                dr.Item("MIKAN_INC_FLG") = MIKAN_INC_FLG
                dr.Item("SEKY_DATE") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.INV_DATE.ColNo))
                dr.Item("JOB_NO") = " "
                dr.Item("CUST_CD_L") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_M.ColNo))
                dr.Item("CUST_CD_S") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_S.ColNo))
                dr.Item("CUST_CD_SS") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_SS.ColNo))
                dr.Item("EXEC_STATE_KB") = "00"
                dr.Item("EXEC_RESULT_KB") = " "
                dr.Item("NORMAL_END_FLG") = " "
                dr.Item("DISP_ROW_NO") = num.ToString
                dr.Item("HOKAN_NIYAKU_CALCULATION") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.LAST_INV_DATE.ColNo)).Replace("/", "")
                dr.Item("CLOSE_KB") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CLOSE_KB.ColNo))
                dr.Item("INV_DATE_TO") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.INV_DATE.ColNo))
                '2011/08/03 菱刈　請求月の取得方法変更 エンド
                dr.Item("MESSAGE_ID") = " "
                dr.Item("EXEC_TIMING_KB") = BatchKb
                dr.Item("KIWARI_KB") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.KIWARI_KB.ColNo))
                dr.Item("ROW_NO") = chkList(i)

                datatable.Rows.Add(dr)

            Next
        End With

        'データの設定
        prm.ParamDataSet = prmDs

    End Sub

    ''' <summary>
    ''' 検索用データセット設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetSearchData(ByVal frm As LMG010F)

        '担当分のみ表示フラグ
        Dim USER_FLG As String = String.Empty

        '今回請求日
        Dim SEKY_DATE As String = String.Empty

        '今回請求日・年
        Dim year As Integer = Convert.ToInt32(frm.imdInvDate.TextValue.ToString.Substring(0, 4))

        '今回請求日・月
        Dim month As Integer = Convert.ToInt32(frm.imdInvDate.TextValue.ToString.Substring(4, 2))

        'データテーブル
        prmDs = New LMG010DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG010C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm
            dr.Item("NRS_BR_CD") = .cmbBr.SelectedValue
            dr.Item("USER_CD") = LMUserInfoManager.GetUserID

            '今回請求日の設定
            Select Case Convert.ToString(.cmbSimebi.SelectedValue)
                Case "00"
                    SEKY_DATE = Convert.ToString(DateSerial(year, month + 1, 1).AddDays(-1))
                Case Else
                    SEKY_DATE = Convert.ToString(DateSerial(year, month, Convert.ToInt32(.cmbSimebi.SelectedValue)))
            End Select

            SEKY_DATE = SEKY_DATE.Replace("/", "").Substring(0, 8)
            dr.Item("SEKY_DATE") = SEKY_DATE

            '担当分のみ表示フラグ設定
            If .chkSelectByNrsB.Checked = True Then
                USER_FLG = LMConst.FLG.ON
            Else
                USER_FLG = LMConst.FLG.OFF
            End If
            dr.Item("TANTO_USER_FLG") = USER_FLG                                                               '担当分のみ表示フラグ
            dr.Item("CUST_CD_L") = .txtCustCdL.TextValue                                                       '荷主コード（大）
            dr.Item("CUST_CD_M") = .txtCustCdM.TextValue                                                       '荷主コード（中）
            dr.Item("CLOSE_KB") = .cmbSimebi.SelectedValue                                                     '締め日区分

            'スプレッド入力データ取得
            With .sprDetail.ActiveSheet
                dr.Item("CUST_NM") = Me._V.GetCellValue(.Cells(0, LMG010G.sprDetailDef.CUST_NM.ColNo)).Trim()  '荷主名
                dr.Item("KIWARI_KB") = Me._V.GetCellValue(.Cells(0, LMG010G.sprDetailDef.KIWARI_KBN.ColNo))    '期割区分
            End With
        End With

        'ベトナム対応
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        dr.Item("LANG_FLG") = lgm.MessageLanguage()

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定（排他・検索用）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetCalclation(ByVal frm As LMG010F)
        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim num As Integer = 0
        Dim chkList As ArrayList = Me._V.getCheckList()
        Dim max As Integer = chkList.Count() - 1
        prmDs = New LMG010DS()
        Dim datatable As DataTable = Me.prmDs.Tables(LMG010C.TABLE_NM_DEL)
        Dim closeKb As String = String.Empty

        '請求日1ヶ月前
        Dim SEKY_DATE As String = String.Empty

        '請求日1ヶ月前・年
        Dim year As Integer = 0

        '請求日1ヶ月前・月
        Dim month As Integer = 0

        Dim Henshu As String = String.Empty

        chkList = Me._LMGConH.GetCheckList(frm.sprDetail.ActiveSheet, LMG010G.sprDetailDef.DEF.ColNo)
        With frm.sprDetail.ActiveSheet
            For i As Integer = 0 To max
                num = Convert.ToInt32(chkList(i))
                Dim dr As DataRow = datatable.NewRow()
                dr.Item("ROW_NO") = chkList(i)
                dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("JOB_NO") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.JOB_NO.ColNo))
                dr.Item("CUST_CD_L") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_M.ColNo))
                dr.Item("CUST_CD_S") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_S.ColNo))
                dr.Item("CUST_CD_SS") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CUST_CD_SS.ColNo))
                dr.Item("INV_DATE_TO") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.LAST_INV_DATE.ColNo))
                dr.Item("UPD_DATE_M_CUST") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.UPD_DATE_M_CUST.ColNo))
                dr.Item("UPD_TIME_M_CUST") = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.UPD_TIME_M_CUST.ColNo))

                '計算日の取得
                Henshu = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.LAST_INV_DATE.ColNo))

                '計算日のスラッシュ編集をとる
                Henshu = Henshu.Replace("/", "").Substring(0, 8)

                '年の取得
                year = Convert.ToInt32(Henshu.ToString.Substring(0, 4))

                '月の取得
                month = Convert.ToInt32(Henshu.ToString.Substring(4, 2))

                '締日区分の取得
                closeKb = Me._LMGConV.GetCellValue(.Cells(num, LMG010G.sprDetailDef.CLOSE_KB.ColNo))

                '請求日の設定
                Select Case closeKb

                    '末締めのとき
                    Case "00"
                        SEKY_DATE = Convert.ToString(DateSerial(year, month, 1).AddDays(-1))

                        '末締め以外のとき
                    Case Else
                        SEKY_DATE = Convert.ToString(DateSerial(year, month - 1, Convert.ToInt32(closeKb)))

                End Select

                SEKY_DATE = SEKY_DATE.Replace("/", "").Substring(0, 8)

                dr.Item("HOKAN_NIYAKU_CALCULATION_YOBI") = SEKY_DATE

                datatable.Rows.Add(dr)
            Next
        End With

        'データの設定
        prm.ParamDataSet = prmDs

    End Sub

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F4押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "UndotheLastCalclation")

        Call Me.UndotheLastCalclation(frm)

        Logger.EndLog(Me.GetType.Name, "UndotheLastCalclation")

    End Sub

    ''' <summary>
    ''' F7押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Execute")

        Call Me.Execute(frm)

        Logger.EndLog(Me.GetType.Name, "Execute")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SetProcessResults")

        Call Me.CallProcessResults(frm)

        Logger.EndLog(Me.GetType.Name, "SetProcessResults")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub FunctionKey10Press(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "MasterShowEvent")

        Me.OpenMasterPop(frm, LMG010C.EventShubetsu.MASTER)

        Logger.EndLog(Me.GetType.Name, "MasterShowEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG010F, ByVal e As FormClosingEventArgs)

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
    Friend Sub EnterKeyDown(ByRef frm As LMG010F, ByVal e As System.Windows.Forms.KeyEventArgs, ByVal controlNm As String)

        If e.KeyCode = Keys.Enter Then
            Me.EnterkeyControl(frm, e, controlNm, LMG010C.EventShubetsu.ENTER)
        End If

    End Sub

    ''' <summary>
    ''' 実行モードオプションボタン　選択時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub ChangedOpt(ByRef frm As LMG010F, ByVal e As System.EventArgs)

        Me.ChangeCheckBox(frm)

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class