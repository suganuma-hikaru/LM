' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG080H : 状況詳細
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Win.Base      '2021/06/28 
Imports Jp.Co.Nrs.LM.Utility    '2021/06/28 

''' <summary>
''' LMG080ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMG080H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMG080V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMG080G

    ''' <summary>
    ''' パラメータのNFFormDataをクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FormPrm As LMFormData

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
        prmDs = prm.ParamDataSet

        Dim ds As DataSet = prmDs

        'フォームの作成
        Dim frm As LMG080F = New LMG080F(Me)

        '画面共通クラスの設定
        Me._LMGConG = New LMGControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMGConV = New LMGControlV(Me, DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._LMGConH = New LMGControlH(DirectCast(frm, Form), MyBase.GetPGID(), Me._LMGConV, Me._LMGConG)

        'Validateクラスの設定
        Me._V = New LMG080V(Me, frm, Me._LMGConV)

        'Gamenクラスの設定
        Me._G = New LMG080G(Me, frm, Me._LMGConG)

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
        Call Me._G.SetControl(Me.GetPGID(), MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの初期値設定
        Me._G.SetInitValue(frm)

        'EnterKey判定用
        frm.KeyPreview = True

        '2011/08/08 菱刈 検証結果一覧No3 スタート
        '既存のコメント可 LMG080に遷移したときは検索をするように設定
        'If ds Is Nothing = False Then
        '    '呼出元より取得データがあるか判定（初期検索判定）
        '    Dim dt As DataTable = ds.Tables(LMG080C.TABLE_NM_IN)
        '    Dim dr As DataRow = dt.Rows(0)

        '    '初期検索判定フラグ
        '    Dim Flg As String = LMConst.FLG.OFF

        '    'データテーブルにデータが存在する場合設定
        '    If dt.Rows.Count <> 0 Then
        '        Flg = dr.Item("DEFAULT_SEARCH_FLG").ToString()
        '    End If
        '    '初期検索フラグの判定
        '    If LMConst.FLG.ON.Equals(Flg) = True Then

        '検索処理
        Call Me.SelectData(frm)

        '    Else
        ''メッセージの表示
        'Me.ShowMessage(frm, "G007")

        '    End If
        'End If
        '既存のコメント可 LMG080に遷移したときは検索をするように設定
        '2011/08/08 菱刈 検証結果一覧No3 エンド
        'フォーカスの設定
        Call Me._G.SetFoucus()

        '2011/08/08 菱刈 検証結果一覧NO2 スタート
        'フォームの表示
        frm.Show()
        '2011/08/08 菱刈 検証結果一覧NO2 エンド
        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 予約取消
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub CancelReservation(ByVal frm As LMG080F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG080C.EventShubetsu.YOYAKU_CANCEL) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '予約取消データ設定
        Call SetUpdateData(frm)

        '排他処理
        MyBase.CallWSA("LMG080BLF", "CheckHaita", prmDs)

        'エラーの判定
        If MyBase.IsMessageExist() = False Then

            '更新処理
            MyBase.CallWSA("LMG080BLF", "CancelUpDate", prmDs)

            'エラーの判定
            If MyBase.IsMessageExist() = False Then

                '検索処理
                Call Me.SelectData(frm)

                'メッセージの表示
                MyBase.ShowMessage(frm, "G037")

            Else
                MyBase.ShowMessage(frm)
            End If
        Else
            MyBase.ShowMessage(frm)
        End If

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 処理結果詳細
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub ProcessingResults(ByVal frm As LMG080F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG080C.EventShubetsu.SHORISHOUSAI) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        'データセット設定
        Call Me.setProcessResults(frm)

        '保管荷役計算管理ワーク詳細取得処理
        Dim ds As DataSet = New DataSet()
        ds = MyBase.CallWSA("LMG080BLF", "ProcessResults", prmDs)

        'ＣＳＶ出力判定
        If MyBase.IsMessageStoreExist = False Then

            'メッセージの設定
            MyBase.ShowMessage(frm, "G041")

        Else

            'ＣＳＶ出力処理
            MyBase.MessageStoreDownload()

        End If

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 強制実行
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub Forcing(ByVal frm As LMG080F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG080C.EventShubetsu.KYOUSEI) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '強制実行用データ設定
        Call Me.setExecute(frm)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '排他・エラーチェック・強制実行処理
        Dim ds As DataSet = New DataSet()
        ds = MyBase.CallWSA("LMG080BLF", "CheckExecute", prmDs)

        If MyBase.IsMessageStoreExist = True Then

            'ＣＳＶ出力処理
            MyBase.MessageStoreDownload()

            'メッセージの表示
            MyBase.ShowMessage(frm, "E235")


        Else

            'メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {"強制実行", ""})

        End If

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' Spread検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMG080F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG080C.EventShubetsu.KENSAKU) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '検索処理
        Call Me.SelectData(frm)

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 強制削除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ForceDelete(ByVal frm As LMG080F)

        '開始処理
        Call Me._LMGConH.StartAction(frm)

        '権限・入力チェック
        If IsCheckCall(frm, LMG080C.EventShubetsu.KYOUSEI_DEL) = False Then
            Exit Sub
        End If

        '全画面ロック
        Call MyBase.LockedControls(frm)

        '強制実行・削除用データ設定
        Call Me.setExecute(frm)

        'メッセージ情報を初期化する
        MyBase.ClearMessageStoreData()

        '排他・エラーチェック・強制実行処理
        Dim ds As DataSet = New DataSet()
        ds = MyBase.CallWSA("LMG080BLF", "ForceDelete", prmDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

             'メッセージエリアの設定
            MyBase.ShowMessage(frm)

        Else

            'メッセージの表示
            MyBase.ShowMessage(frm, "G002", New String() {"強制削除", ""})

        End If

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

        '終了処理
        Call Me._LMGConH.EndAction(frm)

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMG080F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "内部処理"

    ''' <summary>
    ''' 検索処理呼出
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMG080F)

        'データセット設定
        Call Me.setSelectData(frm)

        '検索処理
        Call Me.getSelectData(frm)

    End Sub

    ''' <summary>
    ''' 初期検索・検索処理・再描画
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub getSelectData(ByVal frm As LMG080F)

        'WSA呼出し
        Dim rtnDs As DataSet = New DataSet
        rtnDs = Me._LMGConH.CallWSAAction(DirectCast(frm, Form), _
                                         "LMG080BLF", "SelectListData", prmDs _
                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & _
                                         MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then
            'データテーブルの取得
            Dim dtCount As String = rtnDs.Tables(LMG080C.TABLE_NM_OUT).Rows.Count.ToString()
            If "0".Equals(dtCount) = False Then

                'スプレッドデータをクリアする
                frm.sprDetail.CrearSpread()

                '取得データをスプレッドに反映
                Call Me._G.SetSpread(rtnDs)

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G008", New String() {dtCount})
            Else
                'スプレッドデータをクリアする
                frm.sprDetail.CrearSpread()

                'メッセージの表示
                'MyBase.ShowMessage(frm, "G001")
                '  MyBase.ShowMessage(frm)

            End If
        End If

        '全画面ロック解除
        Call MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 入力・権限チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="SHUBETSU"></param>
    ''' <remarks></remarks>
    Private Function IsCheckCall(ByVal frm As LMG080F, ByVal SHUBETSU As LMG080C.EventShubetsu) As Boolean

        'フォームの背景色を初期化する
        Me._G.SetBackColor(frm)

        '背景色クリア
        Me._LMGConG.SetBackColor(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(SHUBETSU) = False Then
            Return False
        End If

        '検索処理の場合、単項目チェック・スプレッド項目チェックを行う
        If LMG080C.EventShubetsu.KENSAKU.Equals(SHUBETSU) = True Then
            If Me._V.IsInputCheck() = False Then
                Return False
            End If
            If Me._V.IsSpreadInputChk() = False Then
                Return False
            End If
        End If

        'イベントの場合、関連項目チェックを行う
        If LMG080C.EventShubetsu.CLOSE.Equals(SHUBETSU) = False Then
            If Me._V.isRelationCheck(SHUBETSU) = False Then
                Return False
            End If
        End If

        '強制削除の場合、５分以上経過しているかチェック
        If LMG080C.EventShubetsu.KYOUSEI_DEL.Equals(SHUBETSU) = True Then
            If Me._V.SysUpdDateTimeCheck(MyBase.GetSystemDateTime(0), MyBase.GetSystemDateTime(1)) = False Then
                Return False
            End If
        End If

        Return True

    End Function

#End Region

#Region "DataSet"

    ''' <summary>
    ''' 検索用データ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub setSelectData(ByVal frm As LMG080F)

        Dim Shorimi As String = String.Empty
        Dim ShoriZumi As String = String.Empty
        Dim ShoriChu As String = String.Empty
        Dim ShoriCancel As String = String.Empty

        Me.prmDs = New LMG080DS()

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim datatable As DataTable = prmDs.Tables(LMG080C.TABLE_NM_SELECTIN)
        Dim dr As DataRow = datatable.NewRow()

        'フォーム入力データ取得
        With frm
            If .chkShoriMi.Checked = True Then
                Shorimi = "00"
            End If
            If .chkShoriZumi.Checked = True Then
                ShoriZumi = "01"
            End If
            If .chkShoriChu.Checked = True Then
                ShoriChu = "02"
            End If
            If .chkTorikeshi.Checked = True Then
                ShoriCancel = "03"
            End If
            dr.Item("JIKKOU_MODE") = .cmbseqflg.SelectedValue
            dr.Item("BATCH_JOKEN") = .cmbBatch.SelectedValue
            dr.Item("JIKKOU_FROM") = .imdInvDateFrom.TextValue
            dr.Item("JIKKOU_TO") = .imdInvDateTo.TextValue
            dr.Item("SHORI_MI") = Shorimi
            dr.Item("SHORI_ZUMI") = ShoriZumi
            dr.Item("SHORI_CHU") = ShoriChu
            dr.Item("SHORI_CANCEL") = ShoriCancel

        End With

        With frm.sprDetail.ActiveSheet
            dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(0, LMG080G.sprDetailDef.NRS_BR_NM.ColNo()))
            dr.Item("USER_NM") = Me._LMGConV.GetCellValue(.Cells(0, LMG080G.sprDetailDef.USER_NM.ColNo()))
            dr.Item("CUST_CD") = Me._LMGConV.GetCellValue(.Cells(0, LMG080G.sprDetailDef.CUST_CD.ColNo()))
            dr.Item("CUST_NM") = Me._LMGConV.GetCellValue(.Cells(0, LMG080G.sprDetailDef.CUST_NM.ColNo())).Trim()
            dr.Item("JOB_NO") = Me._LMGConV.GetCellValue(.Cells(0, LMG080G.sprDetailDef.JOB_NO.ColNo()))
        End With

        'ベトナム対応
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        dr.Item("LANG_FLG") = lgm.MessageLanguage()

        'データの設定
        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' 予約取消用データ
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub SetUpdateData(ByVal frm As LMG080F)
        Me.prmDs = New LMG080DS()

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim datatable As DataTable = prmDs.Tables(LMG080C.TABLE_NM_DEL)
        Dim dr As DataRow = datatable.NewRow()
        Dim def As Integer = Me._V.FindSelectRowOne()

        With frm.sprDetail.ActiveSheet
            dr.Item("SEKY_FLG") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.SEKY_FLG.ColNo))
            dr.Item("BATCH_NO") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.BATCH_NO.ColNo))
            dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.NRS_BR_CD.ColNo))
            dr.Item("OPE_USER_CD") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.USER_ID.ColNo))
            dr.Item("REC_NO") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.REC_NO.ColNo))
            dr.Item("SYS_UPD_DATE") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.SYS_UPD_DATE.ColNo))
            dr.Item("SYS_UPD_TIME") = Me._LMGConV.GetCellValue(.Cells(def, LMG080G.sprDetailDef.SYS_UPD_TIME.ColNo))

        End With

        'データの設定
        datatable.Rows.Add(dr)
    End Sub

    ''' <summary>
    ''' 処理結果詳細データ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub setProcessResults(ByVal frm As LMG080F)
        Me.prmDs = New LMG080DS()

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim datatable As DataTable = prmDs.Tables(LMG080C.TABLE_NM_IN_RESULT)
        Dim def As ArrayList = Me._V.FindSelectRow()
        Dim Max As Integer = def.Count

        With frm.sprDetail.ActiveSheet
            For i As Integer = 0 To Max - 1

                'レコードの作成
                Dim dr As DataRow = datatable.NewRow()
                dr.Item("SEKY_FLG") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.SEKY_FLG.ColNo))
                dr.Item("BATCH_NO") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.BATCH_NO.ColNo))
                dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("OPE_USER_CD") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.USER_ID.ColNo))
                '2011/08/08 菱刈 荷主コード(大),(中),(小),(極小)を検索条件に追加 スタート
                dr.Item("CUST_CD_L") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.CUST_CD_L.ColNo))
                dr.Item("CUST_CD_M") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.CUST_CD_M.ColNo))
                dr.Item("CUST_CD_S") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.CUST_CD_S.ColNo))
                dr.Item("CUST_CD_SS") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.CUST_CD_SS.ColNo))
                '2011/08/08 菱刈 荷主コード(大),(中),(小),(極小)を検索条件に追加 エンド
                dr.Item("ROW_NO") = Convert.ToString(def(i))
                'データの設定
                datatable.Rows.Add(dr)

            Next
        End With

        'データの設定
        prm.ParamDataSet = prmDs

    End Sub

    ''' <summary>
    ''' 強制実行データ設定
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub setExecute(ByVal frm As LMG080F)

        Me.prmDs = New LMG080DS()

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        Dim datatable As DataTable = prmDs.Tables(LMG080C.TABLE_NM_IN_RESULT)
        Dim def As ArrayList = Me._V.FindSelectRow()
        Dim Max As Integer = def.Count
        With frm.sprDetail.ActiveSheet
            For i As Integer = 0 To Max - 1

                'レコードの作成
                Dim dr As DataRow = datatable.NewRow()
                dr.Item("SEKY_FLG") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.SEKY_FLG.ColNo))
                dr.Item("BATCH_NO") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.BATCH_NO.ColNo))
                dr.Item("NRS_BR_CD") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.NRS_BR_CD.ColNo))
                dr.Item("OPE_USER_CD") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.USER_ID.ColNo))
                dr.Item("SYS_UPD_DATE") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.SYS_UPD_DATE.ColNo))
                dr.Item("SYS_UPD_TIME") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.SYS_UPD_TIME.ColNo))
                dr.Item("JOB_NO") = Me._LMGConV.GetCellValue(.Cells(Convert.ToInt32(def(i)), LMG080G.sprDetailDef.JOB_NO.ColNo))
                dr.Item("ROW_NO") = Convert.ToInt32(def(i))

                'データの設定
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
    Friend Sub FunctionKey4Press(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "CancelReservation")

        Call Me.CancelReservation(frm)

        Logger.EndLog(Me.GetType.Name, "CancelReservation")

    End Sub

    ''' <summary>
    ''' F6押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "ProcessingResults")

        Call Me.ProcessingResults(frm)

        Logger.EndLog(Me.GetType.Name, "ProcessingResults")

    End Sub

    ''' <summary>
    ''' F8押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey8Press(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Forcing")

        Call Me.Forcing(frm)

        Logger.EndLog(Me.GetType.Name, "Forcing")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "SelectListEvent")

        '検索処理
        Me.SelectListEvent(frm)

        Logger.EndLog(Me.GetType.Name, "SelectListEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(強制削除処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "ForceDelete")

        '強制削除
        Me.ForceDelete(frm)

        Logger.EndLog(Me.GetType.Name, "ForceDelete")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMG080F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    ''' <summary>
    ''' Enterキー押下処理
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMG080F, ByVal e As System.Windows.Forms.KeyEventArgs)

        If e.KeyCode = Keys.Enter Then

            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

        End If
    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class