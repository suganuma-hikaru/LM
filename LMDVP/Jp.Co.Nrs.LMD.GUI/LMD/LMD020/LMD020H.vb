' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫サブシステム
'  プログラムID     :  LMD020H : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李
Imports GrapeCity.Win.Editors
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMD020ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMD020H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMD020V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMD020G

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConV As LMDControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConH As LMDControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDConG As LMDControlG

    ''' <summary>
    '''検索条件格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDs As DataSet

    ''' <summary>
    '''在庫テーブル結果格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _FindDt As DataTable

    ''' <summary>
    '''在庫テーブル結果格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkCount As Integer

    Private _PrmLMD020 As LMFormData

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private _Frm As LMD020F
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' 荷主(大)コードデータ保持
    ''' </summary>
    ''' <remarks></remarks>
    Private _CustCdL As String

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

        '荷主(大)コード保持エリアの初期化
        Me._CustCdL = String.Empty

        'フォームの作成
        Dim frm As LMD020F = New LMD020F(Me)

        'Validate共通クラスの設定
        Me._LMDConV = New LMDControlV(Me, DirectCast(frm, Form))

        'Hnadler共通クラスの設定
        Me._LMDConH = New LMDControlH(DirectCast(frm, Form), MyBase.GetPGID())

        'Gamen共通クラスの設定
        Me._LMDConG = New LMDControlG(Me, DirectCast(frm, Form))

        'Validateクラスの設定
        Me._V = New LMD020V(Me, frm)

        'Gamenクラスの設定
        Me._G = New LMD020G(Me, frm)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        '2015.10.15 英語化対応START
        'タイトルテキスト・フォント設定の切り替え
        Call MyBase.TitleSwitching(frm)
        '2015.10.15 英語化対応END

        '営業所,倉庫コンボ関連設定
        MyBase.CreateSokoCombData(frm.cmbNrsBrCd, frm.cmbSoko)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True
        'Enter押下イベント設定
        Call Me._LMDConG.SetEnterEvent(frm)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl()

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitSpread()

        'スプレッドの検索行に初期値を設定する
        Call Me._G.SetInitValue(frm)

        'メッセージの表示
        MyBase.ShowMessage(frm, "G007")

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD020C.ActionType.MAIN)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD020C.ActionType.MAIN)

        '行削除、行追加ボタンの個別設定
        Call Me._G.SetControlBtnCtrl()

        'コンボボックスの初期値設定
        Call Me._G.SetcmbValue()

        '条件付きコントロール制御
        If Not LMD020C.NRS_BR_CD_TOKE.Equals(frm.cmbNrsBrCd.SelectedValue.ToString) Then
            '土気以外なら強制出庫は不可
            frm.optKyoseiShuko.Enabled = False
        End If

        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        _Frm = frm
        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region '初期処理

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Function CloseFormEvent(ByVal frm As LMD020F, ByVal e As FormClosingEventArgs) As Boolean

        '移動先にチェックが入っている場合、ワーニング
        If 0 < Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo).Count Then

            'メッセージ表示
            Select Case MyBase.ShowMessage(frm, "W002")
                Case MsgBoxResult.Yes  '「はい」押下時

                    If Me.SaveIdoData(frm) = True Then
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

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectListEvent(ByVal frm As LMD020F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMD020C.ActionType.KENSAKU) = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '項目チェック
        If Me._V.IsKensakuInputChk = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '検索処理を行う
        Call Me.SelectData(frm)

        'キャッシュから名称取得
        Call SetCachedName(frm)

        'フォーカスの設定
        Call Me._G.SetFoucus(LMD020C.ActionType.KENSAKU)

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SaveIdoData(ByVal frm As LMD020F) As Boolean

        '強制出庫の保存処理は特殊なので別メソッドで処理を行う
        If frm.optKyoseiShuko.Checked Then
            Return SaveIdoDataKyoseiShuko(frm)
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD020C.ActionType.HOZON)

        '移動先のチェックボックス一覧
        Dim arrSaki As ArrayList = Nothing

        '移動元のチェックボックス一覧
        Dim arrMoto As ArrayList = Nothing

        'スプレッドチェック処理
        Call Me.chkProcessEndSprSelect(frm)

        '移動先のスプレッドチェック状態を取得
        arrSaki = Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)

        '移動元のスプレッドチェック状態を取得
        arrMoto = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

        '入力チェック、関連チェック
        rtnResult = rtnResult AndAlso Me._V.IsSaveInputChk(frm, arrSaki, arrMoto)

        '保管料取込日チェック
        rtnResult = rtnResult AndAlso Me.IsHokanryoChk(frm, arrMoto)

        '棟 + 室 + ZONE（置き場情報）温度管理チェック
#If False Then  'UPD 2020/06/29 013543   【LMS】在庫移動画面_複数移動機能エラー対応(高度化全般)
        rtnResult = rtnResult AndAlso Me.IsOndoCheck(frm, arrMoto, LMConst.FLG.OFF)

#Else
        rtnResult = rtnResult AndAlso Me.IsOndoCheck(frm, arrMoto, arrSaki, LMConst.FLG.OFF)
#End If

        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        '申請外の商品保管ルール情報取得
        Dim expDs As DataSet = New LMD020DS()
        Dim inRow As DataRow = expDs.Tables("LMD020IN").NewRow
        inRow.Item("NRS_BR_CD") = _Frm.cmbNrsBrCd.SelectedValue
        inRow.Item("WH_CD") = _Frm.cmbSoko.SelectedValue
        inRow.Item("CUST_CD_L") = _Frm.txtCustCdL.TextValue
        inRow.Item("IDO_DATE") = _Frm.imdIdoubi.TextValue
        expDs.Tables("LMD020IN").Rows.Add(inRow)
        expDs = MyBase.CallWSA("LMD020BLF", "getTouSituExp", expDs)
#If False Then  'UPD 2020/06/29 013543   【LMS】在庫移動画面_複数移動機能エラー対応(高度化全般
        rtnResult = rtnResult AndAlso Me.IsDangerousGoodsCheck(frm, arrMoto, LMConst.FLG.OFF, expDs)

#Else
        '依頼番号:013987 棟室マスタ、ZONEマスタチェック処理改修
        'rtnResult = rtnResult AndAlso Me.IsDangerousGoodsCheck(frm, arrSaki, LMConst.FLG.OFF, expDs)
        '新規入荷チェック
        rtnResult = rtnResult AndAlso Me.IsTouSituZoneCheck(frm, arrSaki, LMConst.FLG.OFF, expDs)
#End If
        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If

        'コード値をキャッシュより再取得
        Me.SetDataSetInCd(frm)

        '在庫移動データ作成のデータ取得
        'DataSet設定
        Dim rtnDs As DataSet = New LMD020DS()

        '登録、更新のデータセットを行うクラスを呼び出す
        Call Me.SetDatasetAll(frm, rtnDs, arrSaki, arrMoto)

        '要望番号:1350 terakawa 2012.08.27 Start
        '同一置き場（同一商品・ロット）チェック
        rtnResult = Me.IsGoodsLotCheck(frm, rtnDs)
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Function
        End If
        '要望番号:1350 terakawa 2012.08.29 End

        '==========================
        'WSAクラス呼出
        '========================== 
        rtnDs = MyBase.CallWSA("LMD020BLF", "InsertSaveAction", rtnDs)

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(frm)
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())  '終了処理
            Return False
        End If

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'スプレッドチェック処理
        Call Me.chkProcessEndSprSelect(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(LMD020C.ActionType.HOZON)

        Dim drIdo As DataRow = rtnDs.Tables(LMD020C.TABLE_NM_IDO)(0)

        '処理終了メッセージの表示
        '2015.10.22 tusnehira add
        '英語化対応
        MyBase.ShowMessage(frm, "G076", New String() {String.Concat(drIdo.Item("REC_NO").ToString())})
        'MyBase.ShowMessage(frm, "G002", New String() {"在庫移動", String.Concat("[", "レコード番号", " = ", drIdo.Item("REC_NO").ToString(), "]")})

        'スプレッドチェッククリア処理
        Call Me.chkClearSakiSpr(frm)

        Me._PrmLMD020 = New LMFormData
        Me._PrmLMD020.ReturnFlg = True

        'start 要望管理009859
        '印刷処理（自動倉庫置場変更一覧）
        If LMD020C.JIYURAN_AUTO_IDO.Equals(frm.cmbJiyuran.SelectedValue) Then
            '事由欄=自動倉庫移動のみ
            Call Me.Print1(frm, rtnDs)
        End If
        'end 要望管理009859

        Return True

    End Function

    ''' <summary>
    ''' 保存処理（強制出庫用）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns></returns>
    ''' <remarks>通常の保存処理とは完全に別物なので隔離</remarks>
    Private Function SaveIdoDataKyoseiShuko(ByVal frm As LMD020F) As Boolean

        Dim arrMoto As ArrayList = Nothing

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        Try
            '権限チェック
            If Not Me._V.IsAuthorityChk(LMD020C.ActionType.HOZON) Then
                Return False
            End If

            '移動元のスプレッドチェック状態を取得
            arrMoto = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

            '自動倉庫以外は除外する
            With frm.sprMoveBefor.ActiveSheet
                For i As Integer = arrMoto.Count - 1 To 0 Step -1
                    Dim rowNo As Integer = Convert.ToInt32(arrMoto(i))
                    Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(
                                                                        String.Concat(
                                                                        "KBN_GROUP_CD = 'O014' ",
                                                                        "AND KBN_NM1 = '", Me._LMDConV.GetCellValue(.Cells(rowNo, LMD020G.sprMoveBefor.NRS_BR_CD.ColNo)).ToString(), "' ",
                                                                        "AND KBN_NM2 = '", Me._LMDConV.GetCellValue(.Cells(rowNo, LMD020G.sprMoveBefor.WH_CD.ColNo)).ToString(), "' ",
                                                                        "AND KBN_NM3 = '", Me._LMDConV.GetCellValue(.Cells(rowNo, LMD020G.sprMoveBefor.TOU_NO.ColNo)).ToString(), "' ",
                                                                        "AND KBN_NM4 = '", Me._LMDConV.GetCellValue(.Cells(rowNo, LMD020G.sprMoveBefor.SITU_NO.ColNo)).ToString(), "' "))
                    If getDr.Length = 0 Then
                        arrMoto.RemoveAt(i)
                    End If
                Next
            End With

            '入力チェック、関連チェック（強制出庫用）
            If Not Me._V.IsSaveInputChkKyoseiShuko(frm, arrMoto) Then
                Return False
            End If

            '自動倉庫にチェックが入っていなければエラー
            If arrMoto.Count = 0 Then
                Me._LMDConV.SetErrMessage("E02C")
                Return False
            End If

            '処理続行確認
            If MyBase.ShowMessage(frm, "W303") <> MsgBoxResult.Ok Then
                Return False
            End If

            '出庫依頼処理
            Dim ds As DataSet = New LMD020DS()
            For i As Integer = 0 To arrMoto.Count - 1
                Dim dr As DataRow = ds.Tables("LMD020IN_REG").NewRow()
                dr("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
                dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo)).ToString()
                dr("WH_CD") = frm.cmbSoko.SelectedValue.ToString()
                dr("CUST_CD_L") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.CUST_CD_L.ColNo)).ToString()
                dr("TOU_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.TOU_NO.ColNo)).ToString()
                dr("SITU_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.SITU_NO.ColNo)).ToString()
                dr("ZONE_CD") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZONE_CD.ColNo)).ToString()
                dr("LOT_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.LOT_NO.ColNo)).ToString()
                dr("GOODS_NM") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_NM.ColNo)).ToString()
                dr("PALLET_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.LOCA.ColNo)).ToString()
                ds.Tables("LMD020IN_REG").Rows.Add(dr)
            Next
            ds = MyBase.CallWSA("LMD020BLF", "ShukoIrai", ds)

            'メッセージコードの判定
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(frm)
                Return False
            End If

        Finally
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
        End Try

        '印刷処理（強制出庫在庫一覧）
        Call Me.Print2(frm, arrMoto)

        Return True

    End Function

    ''' <summary>
    ''' 移動元スプレッド選択、解除処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectIdoMotoEvent(ByVal frm As LMD020F)

        '移動元選択、解除、行追加共通処理を行う
        Call Me.chkSprSelect(frm, LMD020C.ActionType.CHK)

        '行追加・行削除ボタンの制御を行う
        Call Me._G.SetControlBtnCtrl()

    End Sub

    ''' <summary>
    ''' 行追加処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprAddRowEvent(ByVal frm As LMD020F)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD020C.ActionType.COLADD)

        '平行移動時は問答無用で処理を抜ける
        If frm.optHeikouIdo.Checked = True Then
            Exit Sub
        End If

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._V.IsSelectInsChk(arr.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '移動元選択、解除、行追加共通処理を行う
        Call Me.chkSprSelect(frm, LMD020C.ActionType.COLADD)

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub DeleteRowsSaki(ByVal frm As LMD020F)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD020C.ActionType.COLDEL)

        '平行移動時は問答無用で処理を抜ける
        If frm.optHeikouIdo.Checked = True Then
            Exit Sub
        End If

        'チェックリスト取得
        Dim arr As ArrayList = Nothing
        If rtnResult = True Then
            arr = Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMDConV.IsSelectChk(arr.Count)

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        For i As Integer = arr.Count - 1 To 0 Step -1

            '選択された行を物理削除
            frm.sprMoveAfter.ActiveSheet.Rows(Convert.ToInt32(arr(i))).Remove()

        Next

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

    End Sub

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SprAllChangeEvent(ByVal frm As LMD020F)

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD020C.ActionType.ALLCHANGE)

        '入力チェック、関連チェック
        rtnResult = rtnResult AndAlso Me._V.IsAllChangeInputChk()

        'チェックリスト取得
        Dim arrSaki As ArrayList = Nothing
        Dim arrMoto As ArrayList = Nothing
        If rtnResult = True Then
            arrSaki = Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)
            arrMoto = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)
        End If

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMDConV.IsSelectChk(arrSaki.Count)

        '棟 + 室 + ZONE（置き場情報）温度管理チェック
        rtnResult = rtnResult AndAlso Me.IsOndoCheck(frm, arrMoto, arrSaki, LMConst.FLG.ON)

        '未選択チェック
        rtnResult = rtnResult AndAlso Me._LMDConV.IsSelectChk(arrSaki.Count)

        '棟 + 室 + ZONE（置き場情報）温度管理チェック
        rtnResult = rtnResult AndAlso Me.IsOndoCheck(frm, arrMoto, arrSaki, LMConst.FLG.ON)

        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
        '申請外の商品保管ルール情報取得
        Dim expDs As DataSet = New LMD020DS()
        Dim inRow As DataRow = expDs.Tables("LMD020IN").NewRow
        inRow.Item("NRS_BR_CD") = _Frm.cmbNrsBrCd.SelectedValue
        inRow.Item("WH_CD") = _Frm.cmbSoko.SelectedValue
        inRow.Item("CUST_CD_L") = _Frm.txtCustCdL.TextValue
        inRow.Item("IDO_DATE") = _Frm.imdIdoubi.TextValue
        expDs.Tables("LMD020IN").Rows.Add(inRow)
        expDs = MyBase.CallWSA("LMD020BLF", "getTouSituExp", expDs)
        '依頼番号:013987 棟室マスタ、ZONEマスタチェック処理改修
        'rtnResult = rtnResult AndAlso Me.IsDangerousGoodsCheck(frm, arr, LMConst.FLG.ON, expDs)
        '新規入荷チェック
        rtnResult = rtnResult AndAlso Me.IsTouSituZoneCheck(frm, arrSaki, LMConst.FLG.ON, expDs)
        '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

        'エラーがある場合、スルー
        If rtnResult = False Then
            '処理終了アクション
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '一括変更処理を行う
        Call Me.SelectAllChange(frm)

        '処理終了メッセージの表示
        '2015.10.22 tusnehira add
        '英語化対応
        MyBase.ShowMessage(frm, "G077")
        'MyBase.ShowMessage(frm, "G044", New String() {"一括"})

        '処理終了アクション
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理

    End Sub

    ''' <summary>
    ''' 在庫履歴照会処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub ZaiHistoryShow(ByVal frm As LMD020F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD020C.ActionType.ZAIKORIREKI)

        '入力チェック
        rtnResult = rtnResult AndAlso Me._V.IsZaikoDataChk()

        '選択行有無チェック
        If rtnResult = False Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage()) '終了処理
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '在庫履歴照会POP呼出
        Me.ShowZaiHistoryPop(frm)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

    End Sub

    ''' <summary>
    ''' マスタ参照処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub OpenMasterPop(ByVal frm As LMD020F)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = Me._V.IsAuthorityChk(LMD020C.ActionType.MASTER)

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD020C.ActionType.MASTER)

        'エラーの場合、終了
        If rtnResult = False Then
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        'ポップアップ起動コントロール：１件時表示あり
        Me._PopupSkipFlg = True
        'SHINOHARA 要望番号513 ActionType追加
        Me.ShowPopupControl(frm, objNm, LMD020C.ActionType.MASTER)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'スプレッドチェック処理
        Call Me.chkProcessEndSprSelect(frm)

    End Sub

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Call Me.EnterAction(frm, e.KeyCode = Keys.Enter)

    End Sub

    ''' <summary>
    ''' スプレッド移動処理(左スクロール)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub btnSprMoveLeftClickAction(ByVal frm As LMD020F)

        With frm

            Dim point As System.Drawing.Point
            Dim newPoint As Integer = 0

            '移動先スプレッドシートのLocationを取得
            point = .sprMoveAfter.Location

            '現在値より左にずらす
            newPoint = point.X - 30

            '現在値が左端を越える場合は処理しない
            If newPoint < 0 Then
                Exit Sub
            End If

            '移動先スプレッドのLocationにセット
            point.X = newPoint

            '移動先スプレッドのプロパティに再セット
            .sprMoveAfter.Location = point


            Dim sizeMoto As System.Drawing.Size

            Dim newSizeMoto As Integer = 0

            '移動元スプレッドシートのサイズを取得
            sizeMoto = .sprMoveBefor.Size()

            '現在のサイズより縮める
            newSizeMoto = sizeMoto.Width - 30

            '移動元スプレッドのWidthにサイズをセット
            sizeMoto.Width = newSizeMoto

            '移動元スプレッドのプロパティに再セット
            .sprMoveBefor.Size = sizeMoto


            Dim sizeSaki As System.Drawing.Size

            Dim newSizeSaki As Integer = 0

            '移動先スプレッドシートのサイズを取得
            sizeSaki = .sprMoveAfter.Size()

            '現在のサイズより伸ばす
            newSizeSaki = sizeSaki.Width()

            '移動先スプレッドのWidthにサイズをセット
            sizeSaki.Width = newSizeSaki + 30

            '移動先スプレッドのプロパティに再セット
            .sprMoveAfter.Size = sizeSaki

        End With

        MyBase.ShowMessage(frm, "G007")

    End Sub

    ''' <summary>
    ''' スプレッド移動処理(右スクロール)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub btnSprMoveRightClickAction(ByVal frm As LMD020F)

        With frm

            Dim point As System.Drawing.Point
            Dim newPoint As Integer = 0

            '移動先スプレッドシートのLocationを取得
            point = .sprMoveAfter.Location

            '現在値より右にずらす
            newPoint = point.X + 30

            '現在値がフォームの右端を越える場合は処理しない
            If newPoint > 1250 Then
                Exit Sub
            End If

            '移動先スプレッドのLocationにセット
            point.X = newPoint

            '移動先スプレッドのプロパティに再セット
            .sprMoveAfter.Location = point

            Dim sizeSaki As System.Drawing.Size

            Dim newSizeSaki As Integer = 0

            '移動先スプレッドシートのサイズを取得
            sizeSaki = .sprMoveAfter.Size()

            '現在のサイズより伸ばす
            newSizeSaki = sizeSaki.Width - 30

            '移動先スプレッドのWidthにサイズをセット
            sizeSaki.Width = newSizeSaki

            '移動先スプレッドのプロパティに再セット
            .sprMoveAfter.Size = sizeSaki


            Dim sizeMoto As System.Drawing.Size

            Dim newSizeMoto As Integer = 0

            '移動元スプレッドシートのサイズを取得
            sizeMoto = .sprMoveBefor.Size()

            '現在のサイズより伸ばす
            newSizeMoto = sizeMoto.Width()

            '移動元スプレッドのWidthにサイズをセット
            sizeMoto.Width = newSizeMoto + 30

            '移動元スプレッドのプロパティに再セット
            .sprMoveBefor.Size = sizeMoto

        End With

        MyBase.ShowMessage(frm, "G007")

    End Sub

    ''' <summary>
    ''' 荷主(大)コードのフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub txtCustCdLLeaveAction(ByVal frm As LMD020F)

        Dim nowCustCdL As String = frm.txtCustCdL.TextValue

        '前の荷主コード(大)と今の荷主コード(大)の値が変更ない場合、何もしない
        If Me._CustCdL.Equals(nowCustCdL) = True Then
            Return
        End If

        _CustCdL = frm.txtCustCdL.TextValue

        If frm.txtCustCdL.TextValue.Length = 5 Then
            '状態荷主コンボボックスの設定
            If Me.Create_cmbGoodsCondKb3(frm) = True Then
                frm.cmbGoodsCondKb3.ReadOnly = False
            Else
                '状態荷主マスタが存在しないためコンボボックスをクリア
                frm.cmbGoodsCondKb3.Items.Clear()
                frm.cmbGoodsCondKb3.ReadOnly = True
            End If
        Else
            '状態荷主コンボボックスをクリア
            frm.cmbGoodsCondKb3.Items.Clear()
            frm.cmbGoodsCondKb3.ReadOnly = True
        End If

    End Sub

    ''' <summary>
    ''' 状態荷主コンボボックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Function Create_cmbGoodsCondKb3(ByVal frm As LMD020F) As Boolean

        '状態荷主マスタ検索処理（状態荷主コンボ設定用）
        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim sort As String = "JOTAI_CD"
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUSTCOND).Select( _
                                                                                String.Concat( _
                                                                                "NRS_BR_CD = '", frm.cmbNrsBrCd.SelectedValue, "' ", _
                                                                                "AND CUST_CD_L = '", frm.txtCustCdL.TextValue, "' ", _
                                                                                "AND SYS_DEL_FLG = '0'"), _
                                                                                sort)

        If getDr.Length <= 0 Then
            Return False
        End If

        Dim max As Integer = getDr.Length - 1
        frm.cmbGoodsCondKb3.Items.Clear()
        frm.cmbGoodsCondKb3.Items.Add(New ListItem(New SubItem() {New SubItem(""), New SubItem("")}))
        For i As Integer = 0 To max

            item = getDr(i).Item("JOTAI_NM").ToString()
            cd = getDr(i).Item("JOTAI_CD").ToString()

            frm.cmbGoodsCondKb3.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "内部メソッド"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SelectData(ByVal frm As LMD020F)

        'DataSet設定
        Me._FindDs = New LMD020DS()
        Call Me.SetDataSetInData(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = Me._LMDConH.CallWSAAction(DirectCast(frm, Form), _
                                                 "LMD020BLF", "SelectListData", _FindDs _
                                         , Convert.ToInt32(Convert.ToDouble(MyBase.GetLMCachedDataTable _
                                         (LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'S054' AND KBN_CD = '02'")(0).Item("VALUE1"))) _
                                         , Convert.ToInt32(Convert.ToDouble( _
                                         MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN) _
                                             .Select("KBN_GROUP_CD = 'M011' AND KBN_NM1 = '" & MyBase.GetPGID & "'")(0).Item("VALUE1"))))

        'SPREAD(表示行)初期化
        frm.sprMoveBefor.CrearSpread()
        frm.sprMoveAfter.CrearSpread()

        '検索成功時共通処理を行う
        If rtnDs Is Nothing = False Then

            'スプレッドにデータをセットをする
            Call Me.SuccessSelect(frm, rtnDs)

            'ラジオボタンの解除を行う
            Call Me._G.SetControlOptCtrl(False)

        Else

            Exit Sub

        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        '行削除、行追加ボタンの個別設定
        Call Me._G.SetControlBtnCtrl()

        '手動で行ロックを行う
        For i As Integer = 0 To frm.sprMoveAfter.ActiveSheet.Rows.Count - 1

            For j As Integer = 0 To frm.sprMoveAfter.ActiveSheet.ColumnCount - 1
                frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
                frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = True
            Next

        Next

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''自営業所判断による手動で行ロック
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then

        '    For i As Integer = 0 To frm.sprMoveBefor.ActiveSheet.Rows.Count - 1
        '        frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020C.SprColumnIndexMoveBefor.DEF).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
        '        frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020C.SprColumnIndexMoveBefor.DEF).Locked = True
        '    Next
        'End If

        'ファンクションキーの制御
        Call Me._G.SetFunctionKey(LMD020C.ActionType.KENSAKU)

    End Sub

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">検索結果格納データセット</param>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal frm As LMD020F, ByVal ds As DataSet)

        Dim dt As DataTable = ds.Tables(LMD020C.TABLE_NM_OUT)

        If dt.Rows.Count = 0 Then
            Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
            Exit Sub
        End If

        '取得データをSPREADに表示
        Call Me._G.SetSpread(dt)

        'メッセージエリアの設定
        MyBase.ShowMessage(frm, "G008", New String() {dt.Rows.Count.ToString()})

        '検索結果を保持
        Me._FindDt = dt

    End Sub

    ''' <summary>
    ''' 在庫履歴照会呼出
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowZaiHistoryPop(ByVal frm As LMD020F)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ZaiHistoryShow")

        Call Me.SetZaikoRirekiPop(frm)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ZaiHistoryShow")

    End Sub

    ''' <summary>
    ''' 複数移動時の前処理ボタンイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub preActionIdo(ByVal frm As LMD020F)

        If frm.optKyoseiShuko.Checked = True Then
            '移動先のスプレッドを非表示
            frm.sprMoveAfter.Visible = False
        Else
            '移動先のスプレッドを表示
            frm.sprMoveAfter.Visible = True
        End If

        If Me._FindDt Is Nothing Then
            Exit Sub
        End If

        If frm.optHeikouIdo.Checked = True Then
            '移動先のスプレッドを再表示
            Me._G.SetSpreadAfter(Me._FindDt)

        ElseIf frm.optFukusuIdo.Checked = True Then
            '移動先のスプレッドをクリア
            frm.sprMoveAfter.CrearSpread()

        ElseIf frm.optKyoseiShuko.Checked = True Then
            '移動先のスプレッドをクリア
            frm.sprMoveAfter.CrearSpread()
        End If

    End Sub

    ''' <summary>
    ''' 移動元選択、解除、行追加共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub chkSprSelect(ByVal frm As LMD020F, ByVal actionType As Integer)

        'チェックリスト取得
        Dim arrCheckList As ArrayList = Nothing
        Dim arrAllList As ArrayList = Nothing
        Dim arrsakiList As ArrayList = Nothing
        Dim arrsakiChkList As ArrayList = Nothing
        Dim chkStr As String = String.Empty
        Dim chkSakiStr As String = String.Empty
        Dim dr As DataRow = Nothing
        Dim rowNo As Integer = 0
        Dim sakiRowNo As Integer = 0

        '移動元のチェックリストを取得
        arrCheckList = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

        '移動元のチェック行のリスト(全量)を取得
        arrAllList = Me._LMDConH.GetSpredList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

        '移動先のチェックリストを取得
        arrsakiList = Me._LMDConH.GetSpredDelFirstList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)

        '移動先の全てチェックされていないかつ、複数移動の場合は移動先一覧をクリアする
        If arrCheckList.Count = 0 And frm.optFukusuIdo.Checked = True Then

            'スプレッドシートが存在しない場合はスプレッド行のチェッククリアを行い終了
            If arrsakiList.Count = 0 Then
                Call Me.chkClearSpr(frm)
                Exit Sub
            End If

            'メッセージ表示
            Select Case MyBase.ShowMessage(frm, "C006")
                Case MsgBoxResult.Ok   '「はい」押下時
                    '移動先のスプレッドをクリア
                    frm.sprMoveAfter.CrearSpread()

                Case MsgBoxResult.Cancel   '「キャンセル」押下時
                    frm.sprMoveBefor.SetCellValue(_ChkCount, LMD020G.sprMoveBefor.DEF.ColNo, "True")
                    Exit Sub
            End Select

        End If

        'スプレッドシート全量に対してチェックを行う
        For i As Integer = 1 To arrAllList.Count - 1

            chkStr = arrAllList(i).ToString()

            If LMConst.FLG.ON.Equals(chkStr) = True Then
#If False Then  'DEL 2020/06/29 013543   【LMS】在庫移動画面_複数移動機能エラー対応(高度化全般)
                '対象行のロック解除を行う
                frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, False))
#End If
                '複数移動の場合のみ実施
                If frm.optFukusuIdo.Checked = True Then

                    'チェックボックス押下時のチェック
                    If Me._V.inkoDateFutureChk(frm, i, actionType, Convert.ToString(MyBase.GetSystemDateTime(0))) = False OrElse _
                        Me._V.hikiateChk(frm, i, actionType) = False Then
                        '終了処理
                        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
                        'スプレットチェック処理
                        Call Me.chkProcessEndSprSelect(frm)
                        Exit Sub
                    End If

                    '格納しておいたDataTableを使用してスプレッドにセットをする
                    dr = Me._FindDt(i - 1)

                    '移動元に該当するレコードを移動先にセット
                    Call Me._G.SetSpreadSakiRowAdd(dr)

                    'チェック状態を保持しておく
                    Me._ChkCount = i

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {Me._FindDt.Rows.Count.ToString()}) '検証結果(メモ)№115対応(2011.09.12)

                ElseIf frm.optHeikouIdo.Checked = True AndAlso arrsakiList.Count > 0 Then

                    '行番号の取得
                    rowNo = Convert.ToInt32(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.ROW_NO.ColNo)).ToString())

                    '入力チェック
                    If Me._V.inkoDateFutureChk(frm, rowNo, actionType, Convert.ToString(MyBase.GetSystemDateTime(0))) = False Then

                        '終了処理
                        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())
                        'スプレットチェック処理
                        Call Me.chkProcessEndSprSelect(frm)
                        Exit Sub

                    End If

                    '移動元に行番号に該当するレコードにチェックをつける
                    arrsakiList = Me.chkMotoToSaki(frm, arrsakiList, rowNo)

                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G008", New String() {Me._FindDt.Rows.Count.ToString()}) '検証結果(メモ)№115対応(2011.09.12)

                End If

            Else

                '複数移動の場合のみ実施
                If frm.optFukusuIdo.Checked = True Then

                    '対象行のロック処理を行う
                    frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, True))

                End If

            End If

        Next

        Dim chkFlg As Boolean = False

        If frm.optHeikouIdo.Checked = True AndAlso arrsakiList.Count > 0 Then

            '移動元のチェックリストを取得
#If False Then　　'DEL 2020/04/07 
            arrCheckList = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)
#End If

            '移動先のチェックリストを取得
            arrsakiChkList = Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)

            '移動元と移動先でチェックの数に相違がある場合、移動元に合わせる
            If arrCheckList.Count <> arrsakiChkList.Count Then

                For i As Integer = 0 To arrsakiChkList.Count - 1

                    sakiRowNo = Convert.ToInt32(Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrsakiChkList(i)), LMD020G.sprMoveAfter.ROW_NO.ColNo)).ToString())

                    For j As Integer = 0 To arrCheckList.Count - 1

                        rowNo = Convert.ToInt32(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrCheckList(j)), LMD020G.sprMoveBefor.ROW_NO.ColNo)).ToString())

                        If sakiRowNo = rowNo Then
                            chkFlg = True
                        End If

                        If chkFlg = True Then
                            Exit For
                        End If

                    Next

                    If chkFlg = False Then
                        Dim chk As String = Convert.ToInt32(arrsakiChkList(i)).ToString()
                        Dim newChkValue As Integer = Convert.ToInt32(chk) - 1
                        arrsakiList(newChkValue) = LMConst.FLG.OFF
                    End If

                    chkFlg = False

                Next

            End If

            For i As Integer = 1 To arrsakiList.Count

                chkSakiStr = arrsakiList(i - 1).ToString()

                If LMConst.FLG.ON.Equals(chkSakiStr) = False Then

                    'チェックを外す
                    frm.sprMoveAfter.SetCellValue(i, LMD020G.sprMoveAfter.DEF_R.ColNo, "False")
                    '行ロックを行う
                    For j As Integer = 0 To frm.sprMoveAfter.ActiveSheet.ColumnCount - 1
                        frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
                        frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = True
                    Next

                End If

            Next

        End If

        '全てチェックされてない場合はロック解除
        If arrCheckList.Count = 0 Then

            For i As Integer = 1 To arrAllList.Count - 1
                frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, False))
            Next

            'ラジオボタンの解除を行う
            Call Me._G.SetControlOptCtrl(False)

        Else

            'ラジオボタンの解除を行う
            Call Me._G.SetControlOptCtrl(True)

        End If

    End Sub

    ''' <summary>
    ''' 平行移動時のチェック処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Function chkMotoToSaki(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal rowNo As Integer) As ArrayList

        Dim sakiRowNo As Integer = 0
        Dim chkStr As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim chk As Boolean = True

        '自営業所ではないならスキップ
        If frm.sprMoveBefor.Sheets(0).Cells(1, LMD020C.SprColumnIndexMoveBefor.DEF).Locked = True Then
            Return arrSaki
        End If

        For i As Integer = 1 To arrSaki.Count
            chkStr = arrSaki(i - 1).ToString()
            sakiRowNo = Convert.ToInt32(Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(i, LMD020G.sprMoveAfter.ROW_NO.ColNo)).ToString())
            custCdL = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(i, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo)).ToString()
            custCdM = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(i, LMD020G.sprMoveAfter.CUST_CD_M_R.ColNo)).ToString()

            If rowNo = sakiRowNo Then

                '移動元に該当するレコードにチェックをつける
                frm.sprMoveAfter.SetCellValue(i, LMD020G.sprMoveAfter.DEF_R.ColNo, "True")

                'arrayListの更新を行う
                arrSaki(i - 1) = LMConst.FLG.ON

                '行ロックの解除を行う
                For j As Integer = 0 To frm.sprMoveAfter.ActiveSheet.ColumnCount - 1

                    '届先名は常にロックなので、解除は行わない
                    If LMD020C.SprColumnIndexMoveAfter.DEST_NM_R <> j Then
                        frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = False
                        frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetSystemInputBackColor()
                    End If

                    If LMD020C.SprColumnIndexMoveAfter.OFB_KB_R = j Then
                        'スプレッド行に該当する倉替処理区分より、簿外品使用可否を取得
                        chk = Me._G.chkBogaiSpr(custCdL, custCdM)
                        If chk = True Then
                            '簿外品が使用可能ならロック解除
                            frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = False
                            frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetSystemInputBackColor()
                        Else
                            '簿外品使用不可ならロック
                            frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = True
                            frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()

                        End If
                    End If

                Next

            End If

        Next

        Return arrSaki

    End Function

    ''' <summary>
    ''' 処理終了後のスプレッドチェック処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub chkProcessEndSprSelect(ByVal frm As LMD020F)

        'チェックリスト取得
        Dim arrCheckList As ArrayList = Nothing
        Dim arrAllList As ArrayList = Nothing
        Dim arrsakiList As ArrayList = Nothing
        Dim chkStr As String = String.Empty
        Dim chkSakiStr As String = String.Empty
        Dim rowNo As Integer = 0

        '移動元のチェックリストを取得
        arrCheckList = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

        '移動元のチェック行のリスト(全量)を取得
        arrAllList = Me._LMDConH.GetSpredList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

        '移動先のチェックリストを取得
        arrsakiList = Me._LMDConH.GetSpredDelFirstList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)

        '平行移動時に変な動きをするので、フォーカスINを解除する
        If frm.optHeikouIdo.Checked = True Then

            With frm.sprMoveAfter.ActiveSheet

                Dim aCell As Cell = .ActiveCell

                If 0 = aCell.Column.Index Then

                    aCell.Editor.CancelEditing()

                End If

            End With

        End If

        'スプレッドシート全量に対してチェックを行う
        For i As Integer = 1 To arrAllList.Count - 1

            chkStr = arrAllList(i).ToString()

            If LMConst.FLG.ON.Equals(chkStr) = True AndAlso arrsakiList.Count > 0 Then

                '対象行のロック解除を行う
                frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, False))

                If frm.optHeikouIdo.Checked = True Then

                    '移動元の行番号取得
                    rowNo = Convert.ToInt32(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.ROW_NO.ColNo)).ToString())

                    '行番号に該当するレコードにチェックをつける
                    arrsakiList = Me.chkMotoToSaki(frm, arrsakiList, rowNo)

                End If

            Else

                '平行移動の場合のみ実施
                If frm.optHeikouIdo.Checked = True AndAlso arrsakiList.Count > 0 Then
                    arrsakiList(i - 1) = LMConst.FLG.OFF

                Else
                    '対象行のロック処理を行う
                    frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, True))

                End If

            End If

        Next

        '平行移動時、なおかつチェックリスト存在
        If frm.optHeikouIdo.Checked = True AndAlso arrsakiList.Count > 0 Then

            For i As Integer = 1 To arrsakiList.Count - 1

                chkSakiStr = arrsakiList(i - 1).ToString()

                If LMConst.FLG.ON.Equals(chkSakiStr) = False Then

                    'チェックを外す
                    frm.sprMoveAfter.SetCellValue(i, LMD020G.sprMoveAfter.DEF_R.ColNo, "False")
                    '行ロックを行う
                    For j As Integer = 0 To frm.sprMoveAfter.ActiveSheet.ColumnCount - 1
                        frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
                        frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = True
                    Next

                End If

            Next

        End If

        '全てチェックされてない場合はロック解除
        If arrCheckList.Count = 0 Then

            For i As Integer = 1 To arrAllList.Count - 1
                frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, False))
            Next

        End If


    End Sub

    ''' <summary>
    ''' 移動元スプレッドチェックロック解除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub chkClearSpr(ByVal frm As LMD020F)

        'チェックリスト取得
        Dim arrAllList As ArrayList = Nothing

        '移動元のチェック行のリスト(全量)を取得
        arrAllList = Me._LMDConH.GetSpredList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)


        'スプレッドシート全量に対してロック解除を行う
        For i As Integer = 1 To arrAllList.Count - 1

            '対象行のロック解除を行う
            frm.sprMoveBefor.SetCellStyle(i, LMD020G.sprMoveBefor.DEF.ColNo, LMSpreadUtility.GetCheckBoxCell(frm.sprMoveBefor, False))

        Next

    End Sub

    ''' <summary>
    ''' 移動先スプレッドチェックロック解除処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub chkClearSakiSpr(ByVal frm As LMD020F)

        'チェックリスト取得
        Dim arrAllList As ArrayList = Nothing

        '移動元のチェック行のリスト(全量)を取得
        arrAllList = Me._LMDConH.GetSpredList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)


        'スプレッドシート全量に対してロック処理を行う
        For i As Integer = 1 To arrAllList.Count - 1

            'チェックを外す
            frm.sprMoveAfter.SetCellValue(i, LMD020G.sprMoveAfter.DEF_R.ColNo, "False")
            '行ロックを行う
            For j As Integer = 0 To frm.sprMoveAfter.ActiveSheet.ColumnCount - 1
                frm.sprMoveAfter.ActiveSheet.Cells(i, j).BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
                frm.sprMoveAfter.ActiveSheet.Cells(i, j).Locked = True
            Next

        Next

    End Sub

    ''' <summary>
    ''' 一括変更処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub SelectAllChange(ByVal frm As LMD020F)

        Dim arrCheckList As ArrayList = Nothing
        Dim chkStr As String = String.Empty
        Dim dr As DataRow = Nothing

        Dim touNo As String = String.Empty
        Dim situNo As String = String.Empty
        Dim zoneCode As String = String.Empty
        Dim loca As String = String.Empty
        Dim goodsCondKb1 As String = String.Empty
        Dim goodsCondKb2 As String = String.Empty
        Dim goodsCondKb3 As String = String.Empty
        Dim spdKb As String = String.Empty
        Dim ofbKb As String = String.Empty
        Dim ltDate As String = String.Empty
        Dim goodsCrtDate As String = String.Empty
        Dim allocPriority As String = String.Empty
        Dim destCd As String = String.Empty
        Dim rsvNo As String = String.Empty
        Dim remarkOut As String = String.Empty
        Dim remark As String = String.Empty

        'コード値をキャッシュより再取得
        Me.SetDataSetInCdAllChange(frm)

        touNo = frm.txtTouNo.TextValue
        situNo = frm.txtSituNo.TextValue
        zoneCode = frm.txtZoneCd.TextValue
        loca = frm.txtLocation.TextValue
        goodsCondKb1 = frm.cmbGoodsCondKb1.SelectedValue.ToString
        goodsCondKb2 = frm.cmbGoodsCondKb2.SelectedValue.ToString
        goodsCondKb3 = frm.cmbGoodsCondKb3.SelectedValue.ToString
        spdKb = frm.cmbSpdKb.SelectedValue.ToString
        ofbKb = frm.cmbOfbKb.SelectedValue.ToString
        ltDate = frm.imdLtDate.TextValue
        goodsCrtDate = frm.imdGoodsCrtDate.TextValue
        allocPriority = frm.cmdAllocPriority.SelectedValue.ToString
        destCd = frm.txtDestCd.TextValue
        rsvNo = frm.txtRsvNo.TextValue
        remarkOut = frm.txtRemarkOut.TextValue
        remark = frm.txtRemark.TextValue

        arrCheckList = Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)

        'スプレッドシートに対して更新を行う
        For i As Integer = 0 To arrCheckList.Count - 1

            With frm.sprMoveAfter
                '値が入力されている項目についてスプレッドシートを更新する
                '棟NO
                If String.IsNullOrEmpty(touNo) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo, touNo)
                End If
                '室NO
                If String.IsNullOrEmpty(situNo) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo, situNo)
                End If
                'ZONEコード
                If String.IsNullOrEmpty(zoneCode) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, zoneCode)
                End If
                'ロケーション
                If String.IsNullOrEmpty(loca) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.LOCA_R.ColNo, loca)
                End If
                '状態中身
                If String.IsNullOrEmpty(goodsCondKb1) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo, goodsCondKb1)
                End If
                '状態外装    
                If String.IsNullOrEmpty(goodsCondKb2) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo, goodsCondKb2)
                End If
                '状態荷主    
                If String.IsNullOrEmpty(goodsCondKb3) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo, goodsCondKb3)
                End If
                '保留品      
                If String.IsNullOrEmpty(spdKb) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.SPD_KB_R.ColNo, spdKb)
                End If
                '簿外品      
                If String.IsNullOrEmpty(ofbKb) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.OFB_KB_R.ColNo, ofbKb)
                End If
                '賞味期限    
                If String.IsNullOrEmpty(ltDate) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.LT_DATE_R.ColNo, DateFormatUtility.EditSlash(ltDate))
                End If
                '製造日      
                If String.IsNullOrEmpty(goodsCrtDate) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo, DateFormatUtility.EditSlash(goodsCrtDate))
                End If
                '割当優先    
                If String.IsNullOrEmpty(allocPriority) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo, allocPriority)
                End If
                '届先コード  
                If String.IsNullOrEmpty(destCd) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.DEST_CD_R.ColNo, destCd)
                End If
                '予約番号    
                If String.IsNullOrEmpty(rsvNo) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.RSV_NO_R.ColNo, rsvNo)
                End If
                '備考小(社外)
                If String.IsNullOrEmpty(remarkOut) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo, remarkOut)
                End If
                '備考小(社内)
                If String.IsNullOrEmpty(remark) = False Then
                    .SetCellValue(Convert.ToInt32(arrCheckList(i)), LMD020G.sprMoveAfter.REMARK_R.ColNo, remark)
                End If

            End With

        Next

    End Sub

    ''' <summary>
    ''' コード値をキャッシュより再取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetInCdAllChange(ByVal frm As LMD020F) As Boolean

        '棟・室・ゾーン再設定
        Dim rtnResult As Boolean = Me.SetTouCdAllChange(frm)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 棟・室・ゾーン再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTouCdAllChange(ByVal frm As LMD020F) As Boolean

        With frm
            Dim touDr As DataRow() = Nothing
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty

            touDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(String.Concat("WH_CD = '", Convert.ToString(.cmbSoko.SelectedValue), "' AND ", _
                                                                                                     "TOU_NO = '", .txtTouNo.TextValue, "' AND ", _
                                                                                                     "SITU_NO = '", .txtSituNo.TextValue, "' AND ", _
                                                                                                     "ZONE_CD = '", .txtZoneCd.TextValue, "'"))

            If 0 < touDr.Length Then
                .txtTouNo.TextValue = touDr(0).Item("TOU_NO").ToString()
                .txtSituNo.TextValue = touDr(0).Item("SITU_NO").ToString()
                .txtZoneCd.TextValue = touDr(0).Item("ZONE_CD").ToString()
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' コード値をキャッシュより再取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetDataSetInCd(ByVal frm As LMD020F) As Boolean

        '移動先のチェックボックス一覧
        Dim arrSaki As ArrayList = Nothing

        '移動先のスプレッドチェック状態を取得
        arrSaki = Me._LMDConH.GetCheckList(frm.sprMoveAfter.ActiveSheet, LMD020G.sprMoveAfter.DEF_R.ColNo)

        '棟・室・ゾーン再設定
        Dim rtnResult As Boolean = Me.SetTouCd(frm, arrSaki)

        '届先コード再設定
        rtnResult = rtnResult AndAlso Me.SetDestCd(frm, arrSaki)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 棟・室・ゾーン再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetTouCd(ByVal frm As LMD020F, ByVal arrSaki As ArrayList) As Boolean

        With frm
            Dim touDr As DataRow() = Nothing
            Dim max As Integer = arrSaki.Count - 1
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty

            For i As Integer = 0 To max
                touNo = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo))
                situNo = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo))
                zoneCd = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo))

                If String.IsNullOrEmpty(touNo) = False AndAlso _
                    String.IsNullOrEmpty(situNo) = False AndAlso _
                    String.IsNullOrEmpty(zoneCd) = False Then
                    touDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TOU_SITU_ZONE).Select(String.Concat("WH_CD = '", Convert.ToString(.cmbSoko.SelectedValue), "' AND ", _
                                                                                                             "TOU_NO = '", touNo, "' AND ", _
                                                                                                             "SITU_NO = '", situNo, "' AND ", _
                                                                                                             "ZONE_CD = '", zoneCd, "'"))

                    If 0 < touDr.Length Then
                        .sprMoveAfter.SetCellValue(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo, touDr(0).Item("TOU_NO").ToString())
                        .sprMoveAfter.SetCellValue(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo, touDr(0).Item("SITU_NO").ToString())
                        .sprMoveAfter.SetCellValue(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, touDr(0).Item("ZONE_CD").ToString())
                    End If

                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 届先コード再設定
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetDestCd(ByVal frm As LMD020F, ByVal arrSaki As ArrayList) As Boolean

        With frm
            Dim destDr As DataRow() = Nothing
            Dim max As Integer = arrSaki.Count - 1
            Dim destCd As String = String.Empty
            Dim custCd As String = String.Empty

            For i As Integer = 0 To max
                destCd = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.DEST_CD_R.ColNo))
                custCd = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo))
                If String.IsNullOrEmpty(destCd) = False Then
                    '---↓
                    'destDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", custCd, "' AND ", _
                    '                                                                                 "DEST_CD = '", destCd, "' AND ", _
                    '                                                                                 "SYS_DEL_FLG = '0'"))

                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("CUST_CD_L") = custCd
                    destMstDr.Item("DEST_CD") = destCd
                    destMstDr.Item("SYS_DEL_FLG") = "0"
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    destDr = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                    '---↑

                    If 0 < destDr.Length Then
                        .sprMoveAfter.SetCellValue(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.DEST_CD_R.ColNo, destDr(0).Item("DEST_CD").ToString())
                    End If

                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドスクロールの同期処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <param name="flg">どちらのスプレッドが動いたかのフラグ：ON⇒移動元 OFF⇒移動先</param>
    ''' <remarks></remarks>
    Friend Sub sprTopUnderWithChange(ByVal frm As LMD020F, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs, ByVal flg As String)

        If LMConst.FLG.ON.Equals(flg) = True Then
            '移動元のスクロール位置を移動先のスクロール位置に反映
            frm.sprMoveAfter.SetViewportTopRow(0, e.NewTop)
        Else
            '移動先のスクロール位置を移動元のスクロール位置に反映
            frm.sprMoveBefor.SetViewportTopRow(0, e.NewTop)

        End If

    End Sub

    ''' <summary>
    ''' 保管・荷役料最終請求日チェック
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function IsHokanryoChk(ByVal frm As LMD020F, ByVal arrMoto As ArrayList) As Boolean

        'DataSet設定
        Dim chkDs As DataSet = Me.SetDataSetHokanNiyakuData(frm, arrMoto)

        '==========================
        'WSAクラス呼出
        '==========================
        chkDs = MyBase.CallWSA("LMD000BLF", "SelectChkIdoDate", chkDs)

        If MyBase.IsMessageExist() = True Then
            Call Me.ShowMessage(frm)
            Return False
        End If

        Return True

    End Function

    '要望番号:1350 terakawa 2012.08.27 Start
    ''' <summary>
    ''' 同一置き場（同一商品・ロット）チェック
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns>OK または Close処理の場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Private Function IsGoodsLotCheck(ByVal frm As LMD020F, ByVal ds As DataSet) As Boolean

        '要望番号:1511 KIM 2012/10/12 START

        'Dim whCd As String = frm.cmbSoko.SelectedValue.ToString()
        'Dim sokoDrs As DataRow() = Me._LMDConV.SelectSokoListDataRow(whCd)
        'Dim goodsLotCheckYn As String = String.Empty
        'Dim serverChkFlg As Boolean = False
        'If 0 < sokoDrs.Length Then
        '    goodsLotCheckYn = sokoDrs(0).Item("GOODSLOT_CHECK_YN").ToString()
        'End If


        ''同一置き場に同一商品・ロットがある場合ワーニング
        'If goodsLotCheckYn = "01" Then

        '    '重複チェック（画面側）
        '    '画面側チェックの場合、フラグはFalse
        '    If Me._V.IsGoodsLotChk(ds, serverChkFlg) = False Then
        '        Return False
        '    End If

        '    '重複チェック（サーバー側）
        '    '==========================
        '    'WSAクラス呼出
        '    '==========================
        '    ds = MyBase.CallWSA("LMD020BLF", "ChkGoodsLot", ds)

        '    If ds.Tables(LMD020C.TABLE_NM_WORNING).Rows.Count > 0 Then
        '        'サーバー側チェックの場合、フラグはTrue
        '        serverChkFlg = True
        '        Return Me._V.IsWorningChk(ds, serverChkFlg)
        '    End If
        'End If

        '荷主明細マスタを参照し、チェック有無を確認する
        Dim custCd As String = frm.txtCustCdL.TextValue()
        Dim custDetailDrs As DataRow() = Me._LMDConV.SelectCustDetailsListDataRow(custCd)
        Dim goodsLotCheckYn As String = String.Empty
        Dim chkFlg As Boolean = False
        Dim serverChkFlg As Boolean = False
        If 0 < custDetailDrs.Length Then

            For i As Integer = 0 To custDetailDrs.Length - 1
                If custDetailDrs(i).Item("SUB_KB").ToString().Equals("41") = True Then
                    chkFlg = True
                    Exit For
                End If
            Next

        End If

        If chkFlg = True Then
            '同一置き場に同一商品・ロットがある場合ワーニング
            '重複チェック（画面側）
            '画面側チェックの場合、フラグはFalse
            If Me._V.IsGoodsLotChk(ds, serverChkFlg) = False Then
                Return False
            End If

            '重複チェック（サーバー側）
            '==========================
            'WSAクラス呼出
            '==========================
            ds = MyBase.CallWSA("LMD020BLF", "ChkGoodsLot", ds)

            If ds.Tables(LMD020C.TABLE_NM_WORNING).Rows.Count > 0 Then
                'サーバー側チェックの場合、フラグはTrue
                serverChkFlg = True
                Return Me._V.IsWorningChk(ds, serverChkFlg)
            End If
        End If

        '要望番号:1511 KIM 2012/10/12 END

        Return True

    End Function
    '要望番号:1350 terakawa 2012.08.27 End

    ''' <summary>
    ''' 移動元スプレッドドラッグアクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub sprBeforeDragAction(ByVal frm As LMD020F)

        'ドラッグしたセル範囲取得
        Dim sel As FarPoint.Win.Spread.Model.CellRange = frm.sprMoveBefor.Sheets(0).GetSelection(0)

        '選択範囲がない場合、イベントスキップ
        If sel Is Nothing Then
            Exit Sub
        End If

        If sel.Column = -1 AndAlso sel.Row = -1 Then
            Exit Sub
        End If

        'レコードが存在しない、もしくは自営業所ではないならスキップ
        If frm.sprMoveBefor.Sheets(0).RowCount = 1 OrElse frm.sprMoveBefor.Sheets(0).Cells(1, LMD020C.SprColumnIndexMoveBefor.DEF).Locked = True Then
            Exit Sub
        End If

        Dim colCnt As Integer = sel.ColumnCount()
        Dim rowCnt As Integer = sel.RowCount()
        Dim va As String = String.Empty

        '範囲内セルのチェックボックス設定
        If sel.Column <= LMD020C.SprColumnIndexMoveBefor.DEF AndAlso (0 < colCnt OrElse 0 < rowCnt) Then

            '編集スタート行設定
            Dim start As Integer = 1
            If 0 < sel.Row Then
                start = sel.Row
            End If

            If frm.optHeikouIdo.Checked = True Then
                '平行移動の場合

                For i As Integer = start To sel.Row + rowCnt - 1
                    va = frm.sprMoveBefor.Sheets(0).Cells(i, LMD020C.SprColumnIndexMoveBefor.DEF).Value.ToString()
                    If va.Equals("True") = True Then
                        frm.sprMoveBefor.SetCellValue(i, LMD020C.SprColumnIndexMoveBefor.DEF, LMConst.FLG.OFF)
                    Else
                        frm.sprMoveBefor.SetCellValue(i, LMD020C.SprColumnIndexMoveBefor.DEF, "True")
                    End If
                Next

                '編集完了
                frm.sprMoveBefor.StopCellEditing()

                'ドラッグ範囲外のセルActiveCellに設定（部品によるドラッグアクションキャンセル効果）
                frm.sprMoveBefor.Sheets(0).SetActiveCell(0, 0)

                'ActiveCellを編集スタートセルに設定（カーソルずれを防止）
                frm.sprMoveBefor.Sheets(0).SetActiveCell(start, 1)

                '移動先スプレッドに編集結果を反映
                Call Me.spdMoveBefor_Change(frm)

            ElseIf frm.optFukusuIdo.Checked = True Then
                '複数移動の場合、セル編集スキップ

                frm.sprMoveBefor.CancelCellEditing()
                frm.sprMoveBefor.Sheets(0).SetActiveCell(0, 0)
                frm.sprMoveBefor.Sheets(0).SetActiveCell(start, 1)

            End If

        End If
    End Sub

    ''' <summary>
    ''' 移動先スプレッドドラッグアクション
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Private Sub sprAfterDragAction(ByVal frm As LMD020F)

        'ドラッグしたセル範囲取得
        Dim sel As FarPoint.Win.Spread.Model.CellRange = frm.sprMoveBefor.Sheets(0).GetSelection(0)

        '選択範囲がない場合、イベントスキップ
        If sel Is Nothing Then
            Exit Sub
        End If

        If sel.Column = -1 AndAlso sel.Row = -1 Then
            Exit Sub
        End If

        Dim colCnt As Integer = sel.ColumnCount()
        Dim rowCnt As Integer = sel.RowCount()
        Dim va As String = String.Empty

        '範囲内セルのチェックボックス設定
        If sel.Column <= LMD020C.SprColumnIndexMoveBefor.DEF AndAlso (0 < colCnt OrElse 0 < rowCnt) Then

            '編集スタート行設定
            Dim start As Integer = 1
            If 0 < sel.Row Then
                start = sel.Row
            End If

            frm.sprMoveBefor.CancelCellEditing()
            frm.sprMoveBefor.Sheets(0).SetActiveCell(0, 0)
            frm.sprMoveBefor.Sheets(0).SetActiveCell(start, 1)

        End If

    End Sub

    ''' <summary>
    ''' ガイダンスメッセージを取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetGMessage() As String
        Return "G007"
    End Function

    ''' <summary>
    ''' 印刷処理（自動倉庫置場変更一覧）
    ''' </summary>
    ''' <returns>要望管理009859</returns>
    Private Function Print1(ByVal frm As LMD020F, ByVal rtDs As DataSet) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD660DS()
        For Each idoRow As DataRow In rtDs.Tables(LMD020C.TABLE_NM_IDO).Rows
            Dim dr As DataRow = inDs.Tables("LMD660IN").NewRow()
            dr("NRS_BR_CD") = idoRow.Item("NRS_BR_CD")
            dr("REC_NO") = idoRow.Item("REC_NO")
            dr("IDO_DATE") = idoRow.Item("IDO_DATE")
            inDs.Tables("LMD660IN").Rows.Add(dr)
        Next

        'プレビューDataTableの設定
        inDs.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        '印刷処理
        inDs = MyBase.CallWSA("LMD020BLF", "DoLMD660Print", inDs)

        '印刷プレビュー
        Dim prevDt As DataTable = inDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then
            Dim prevFrm As RDViewer = New RDViewer()
            prevFrm.DataSource = prevDt
            prevFrm.Run()
            prevFrm.Show()
            prevFrm.Focus()
        End If

        Return inDs

    End Function

    ''' <summary>
    ''' 印刷処理（強制出庫在庫一覧）
    ''' </summary>
    ''' <returns>要望管理017415</returns>
    Private Function Print2(ByVal frm As LMD020F, ByVal arr As ArrayList) As DataSet

        'データセット作成
        Dim inDs As DataSet = New LMD670DS()
        For i As Integer = 0 To arr.Count - 1
            Dim dr As DataRow = inDs.Tables("LMD670IN").NewRow()
            dr("NRS_BR_CD") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD020G.sprMoveBefor.NRS_BR_CD.ColNo)).ToString()
            dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo)).ToString()
            dr("IDO_DATE") = frm.imdIdoubi.TextValue
            inDs.Tables("LMD670IN").Rows.Add(dr)
        Next

        'プレビューDataTableの設定
        inDs.Tables.Add(New RdPrevInfoDS.PREV_INFODataTable())

        '印刷処理
        inDs = MyBase.CallWSA("LMD020BLF", "DoLMD670Print", inDs)

        '印刷プレビュー
        Dim prevDt As DataTable = inDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then
            Dim prevFrm As RDViewer = New RDViewer()
            prevFrm.DataSource = prevDt
            prevFrm.Run()
            prevFrm.Show()
            prevFrm.Focus()
        End If

        Return inDs

    End Function

#Region "POPUP"

    ''' <summary>
    ''' ポップアップ起動コントロール
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <remarks></remarks>
    Private Function ShowPopupControl(ByVal frm As LMD020F, ByVal objNm As String, ByVal actionType As LMD020C.ActionType) As Boolean

        With frm

            Select Case objNm

                Case .sprMoveBefor.Name
                    'メッセージをセットして終了
                    Return Me._LMDConV.SetFocusErrMessage()
                Case .sprMoveAfter.Name
                    '移動先スプレッドのマスタ参照を行う
                    Return Me.ShowPopupSpreadAfter(frm, objNm, actionType)
                Case .txtCustCdL.Name, .txtCustCdM.Name
                    'ヘッダの荷主マスタ参照時
                    '荷主マスタ照会画面をPOP呼出&戻り値設定
                    Call Me.SetCustPop(frm, actionType)
                Case .txtTouNo.Name, .txtSituNo.Name, .txtZoneCd.Name
                    'ヘッダの棟室マスタ参照時
                    '棟室マスタ照会画面をPOP呼出&戻り値設定
                    Call Me.SetTouSituPop(frm, actionType)
                Case .txtDestCd.Name
                    'ヘッダの届先マスタ参照時
                    '届先マスタ照会画面をPOP呼出&戻り値設定
                    Call Me.SetDestPop(frm, actionType)

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟室マスタ、届先マスタ照会画面Pop処理（移動先スプレッド）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカスコントロール名</param>
    ''' <remarks></remarks>
    Private Function ShowPopupSpreadAfter(ByVal frm As LMD020F, ByVal objNm As String, ByVal actionType As LMD020C.ActionType) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '移動先スプレッド
        Dim sprA As Win.Spread.LMSpread = frm.sprMoveAfter
        Dim spr As FarPoint.Win.Spread.SheetView = sprA.ActiveSheet
        Dim msg As String = String.Empty

        With spr

            If 0 < .Rows.Count Then

                Dim cell As FarPoint.Win.Spread.Cell = .ActiveCell
                Dim colNo As Integer = cell.Column.Index
                Dim rowNo As Integer = cell.Row.Index
                Dim listCol As Integer() = _
                    New Integer(2) {LMD020G.sprMoveAfter.TOU_NO_R.ColNo, _
                                    LMD020G.sprMoveAfter.SITU_NO_R.ColNo, _
                                    LMD020G.sprMoveAfter.ZONE_CD_R.ColNo}


                If frm.optHeikouIdo.Checked = True Then

                    '平行移動時はチェックついていないとマスタ参照はNGとする
                    If rowNo = 0 OrElse .Cells(rowNo, LMD020G.sprMoveAfter.DEF_R.ColNo).Value.Equals(LMConst.FLG.OFF) OrElse .Cells(rowNo, LMD020G.sprMoveAfter.DEF_R.ColNo).Value.Equals("False") Then
                        Me._LMDConV.SetFocusErrMessage()
                        Return False
                    End If

                Else

                    '複数移動は検索行以外全量OKとする
                    If rowNo = 0 Then
                        Me._LMDConV.SetFocusErrMessage()
                        Return False
                    End If


                End If

                Select Case colNo
                    '棟、室、ZONE
                    Case LMD020G.sprMoveAfter.TOU_NO_R.ColNo, _
                                LMD020G.sprMoveAfter.SITU_NO_R.ColNo, _
                                LMD020G.sprMoveAfter.ZONE_CD_R.ColNo

                        '棟室マスタ照会画面Pop処理
                        Dim toShitsuPop As LMFormData = Me.ShowToshitsuZonePopup(frm, rowNo, spr, listCol, actionType)
                        '当該画面項目に戻り値を設定
                        If toShitsuPop.ReturnFlg = True Then
                            Dim toShitsuDr As DataRow = toShitsuPop.ParamDataSet.Tables(LMZ120C.TABLE_NM_OUT).Rows(0)
                            sprA.SetCellValue(rowNo, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, toShitsuDr.Item("TOU_NO").ToString())
                            sprA.SetCellValue(rowNo, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, toShitsuDr.Item("SITU_NO").ToString())
                            sprA.SetCellValue(rowNo, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, toShitsuDr.Item("ZONE_CD").ToString())
                        End If

                        '届先名
                    Case LMD020G.sprMoveAfter.DEST_CD_R.ColNo

                        '2017/09/25 修正 李↓
                        msg = lgm.Selector({"届先コード", "Destination code", "송달처코드", "中国語"})
                        '2017/09/25 修正 李↑

                        '禁止文字チェック
                        If Me._V.IsSprForbiddenWordsChk(rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, msg, 15) = False Then
                            Return False
                        End If

                        '届先コード無しでマスタ参照をした場合、届先名をクリアする
                        If String.IsNullOrEmpty(sprA.ActiveSheet.Cells(rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo).Text) = True Then
                            sprA.ActiveSheet.Cells(rowNo, LMD020G.sprMoveAfter.DEST_NM_R.ColNo).Text = String.Empty
                        End If

                        Dim destPop As LMFormData = Me.ShowDestPopup(frm, rowNo, spr, actionType)
                        '当該画面項目に戻り値を設定
                        If destPop.ReturnFlg = True Then
                            Dim destDr As DataRow = destPop.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
                            sprA.SetCellValue(rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, destDr.Item("DEST_CD").ToString())
                            sprA.SetCellValue(rowNo, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, destDr.Item("DEST_NM").ToString())
                        End If

                    Case Else
                        Return Me._LMDConV.SetFocusErrMessage()

                End Select

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 棟室マスタ照会画面Pop起動(移動先スプレッド)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Function ShowToshitsuZonePopup(ByVal frm As LMD020F, ByVal rowNo As Integer, _
                                           ByVal spr As FarPoint.Win.Spread.SheetView, ByVal listCol As Integer(), _
                                           ByVal actionType As LMD020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ120DS()
        Dim dt As DataTable = ds.Tables(LMZ120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'Keyをデータセットに設定
        With dr
            .Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo))
            .Item("WH_CD") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, LMD020G.sprMoveAfter.WH_CD_R.ColNo))
            'START SHINOHARA 要望番号513
            If actionType = LMD020C.ActionType.ENTER Then
                .Item("TOU_NO") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, listCol(0)))
                .Item("SITU_NO") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, listCol(1)))
                .Item("ZONE_CD") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, listCol(2)))
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ120")

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動(移動先スプレッド)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopup(ByVal frm As LMD020F, ByVal rowNo As Integer, _
                                   ByVal spr As FarPoint.Win.Spread.SheetView, _
                                   ByVal actionType As LMD020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'Keyをデータセットに設定
        With dr
            .Item("NRS_BR_CD") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo))
            .Item("CUST_CD_L") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo))
            'START SHINOHARA 要望番号513
            If actionType = LMD020C.ActionType.ENTER Then
                .Item("DEST_CD") = Me._LMDConV.GetCellValue(spr.Cells(rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo))
            End If
            'END SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With
        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ210")

    End Function

    ''' <summary>
    ''' 荷主マスタ照会画面Pop処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetCustPop(ByVal frm As LMD020F, ByVal actionType As LMD020C.ActionType) As Boolean

        '荷主マスタ参照POP起動
        Dim prm As LMFormData = Me.ShowCustPopup(frm, actionType)
        '戻り値の設定
        If prm.ReturnFlg = True Then
            'LMZ260Cデータセット取得
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ260C.TABLE_NM_OUT).Rows(0)
            '当画面項目へセット
            With frm
                .txtCustCdL.TextValue = dr.Item("CUST_CD_L").ToString()
                .txtCustCdM.TextValue = dr.Item("CUST_CD_M").ToString()
                .lblCustNM.TextValue = String.Concat(dr.Item("CUST_NM_L").ToString(), "　", dr.Item("CUST_NM_M").ToString())
            End With
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主マスタ参照POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowCustPopup(ByVal frm As LMD020F, ByVal actionType As LMD020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ260DS()
        Dim dt As DataTable = ds.Tables(LMZ260C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        'KeyをLMZ260Cへデータセット
        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString
            'START SHINOHARA 要望番号513
            If actionType = LMD020C.ActionType.ENTER Then
                .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
                .Item("CUST_CD_M") = frm.txtCustCdM.TextValue
            End If
            'START SHINOHARA 要望番号513
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            .Item("HYOJI_KBN") = LMZControlC.HYOJI_S
        End With

        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ260")

    End Function

    ''' <summary>
    ''' 棟室マスタ照会画面Pop処理(置場)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetTouSituPop(ByVal frm As LMD020F, ByVal actionType As LMD020C.ActionType) As Boolean

        '荷主マスタ参照POP起動
        Dim prm As LMFormData = Me.ShowTouSituPopup(frm, actionType)
        '戻り値の設定
        If prm.ReturnFlg = True Then
            'LMZ260Cデータセット取得
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ120C.TABLE_NM_OUT).Rows(0)
            '当画面項目へセット
            With frm
                .txtTouNo.TextValue = dr.Item("TOU_NO").ToString()
                .txtSituNo.TextValue = dr.Item("SITU_NO").ToString()
                .txtZoneCd.TextValue = dr.Item("ZONE_CD").ToString()
            End With
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 棟室マスタ参照POP起動(置場)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowTouSituPopup(ByVal frm As LMD020F, ByVal actionType As LMD020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ120DS()
        Dim dt As DataTable = ds.Tables(LMZ120C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        'Keyをデータセット
        With dr
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            '.Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
            'START YANAI 要望番号705
            .Item("WH_CD") = frm.cmbSoko.SelectedValue
            'END YANAI 要望番号705
            If actionType = LMD020C.ActionType.ENTER Then
                'START YANAI 要望番号705
                '.Item("WH_CD") = frm.cmbSoko.SelectedValue
                'END YANAI 要望番号705
                .Item("TOU_NO") = frm.txtTouNo.TextValue
                .Item("SITU_NO") = frm.txtSituNo.TextValue
                .Item("ZONE_CD") = frm.txtZoneCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ120")

    End Function

    ''' <summary>
    ''' 届先マスタ照会画面Pop処理(届先コード)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:選択有 False:選択無</returns>
    ''' <remarks></remarks>
    Private Function SetDestPop(ByVal frm As LMD020F, ByVal actionType As LMD020C.ActionType) As Boolean

        '届先マスタ参照POP起動
        Dim prm As LMFormData = Me.ShowDestPopupIkt(frm, actionType)
        '戻り値の設定
        If prm.ReturnFlg = True Then
            'LMZ210Cデータセット取得
            Dim dr As DataRow = prm.ParamDataSet.Tables(LMZ210C.TABLE_NM_OUT).Rows(0)
            '当画面項目へセット
            With frm
                .txtDestCd.TextValue = dr.Item("DEST_CD").ToString()
            End With
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 届先マスタ参照POP起動(一括変更項目)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowDestPopupIkt(ByVal frm As LMD020F, ByVal actionType As LMD020C.ActionType) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Dim ds As DataSet = New LMZ210DS()
        Dim dt As DataTable = ds.Tables(LMZ210C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        'Keyをデータセットに設定
        With dr
            .Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString
            .Item("CUST_CD_L") = frm.txtCustCdL.TextValue
            If actionType = LMD020C.ActionType.ENTER Then
                .Item("DEST_CD") = frm.txtDestCd.TextValue
            End If
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
        End With

        dt.Rows.Add(dr)
        prm.ParamDataSet = ds
        prm.SkipFlg = Me._PopupSkipFlg

        'Pop起動
        Return Me.PopFormShow(prm, "LMZ210")

    End Function

    ''' <summary>
    ''' 在庫履歴画面Pop処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:再描画 False:再描画無し</returns>
    ''' <remarks></remarks>
    Private Function SetZaikoRirekiPop(ByVal frm As LMD020F) As Boolean

        '荷主マスタ参照POP起動
        Dim prm As LMFormData = Me.ShowZaikoRirekiPopup(frm)

        Return False

    End Function

    ''' <summary>
    ''' 在庫履歴POP起動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function ShowZaikoRirekiPopup(ByVal frm As LMD020F) As LMFormData

        Dim prm As LMFormData = New LMFormData()
        Me._FindDs = New LMD030DS()

        'DataSet設定
        Call Me.SetDataSetInZaiData(frm)

        prm.ParamDataSet = Me._FindDs

        'Pop起動
        Return Me.PopFormShow(prm, "LMD030")

    End Function

    ''' <summary>
    ''' Pop起動処理
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <param name="id">画面ID</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Private Function PopFormShow(ByVal prm As LMFormData, ByVal id As String) As LMFormData

        LMFormNavigate.NextFormNavigate(Me, id, prm)
        Return prm

    End Function

    ''' <summary>
    ''' キャッシュから名称取得（全項目）
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Sub SetCachedName(ByVal frm As LMD020F)

        With frm

            '荷主名称（大）（振替元）
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                If String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                    .txtCustCdM.TextValue = "00"
                End If
                .lblCustNM.TextValue = GetCachedCust(.txtCustCdL.TextValue, .txtCustCdM.TextValue, "00", "00")
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
            Return String.Concat(dr(0).Item("CUST_NM_L").ToString, "　", dr(0).Item("CUST_NM_M").ToString)
        Else
        End If

        Return String.Empty

    End Function

#End Region 'POPUP

#Region "Enter処理"

    ''' <summary>
    ''' Enter処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">処理を行う場合 True</param>
    ''' <remarks></remarks>
    Private Sub EnterAction(ByVal frm As LMD020F, ByVal eventFlg As Boolean)

        'カーソル位置の設定
        Dim objNm As String = frm.FocusedControlName()

        '権限チェック
        Dim rtnResult As Boolean = eventFlg

        'カーソル位置チェック
        rtnResult = rtnResult AndAlso Me._V.IsFocusChk(objNm, LMD020C.ActionType.ENTER)

        'Popを表示するかを判定
        rtnResult = rtnResult AndAlso Me.ChkOpenEnterAction(frm, objNm)

        'エラーの場合、終了
        If rtnResult = False Then
            'フォーカス移動処理
            If frm.sprMoveAfter.Name.Equals(objNm) OrElse frm.sprMoveBefor.Name.Equals(objNm) Then Exit Sub
            Call Me.NextFocusedControl(frm, eventFlg)
            Exit Sub
        End If

        '処理開始アクション
        Call Me._LMDConH.StartAction(frm)

        '項目チェック：Pop起動処理：１件時表示なし
        Me._PopupSkipFlg = False
        Me.ShowPopupControl(frm, objNm, LMD020C.ActionType.ENTER)

        '終了処理
        Call Me._LMDConH.EndAction(frm, Me.GetGMessage())

        'フォーカス移動処理
        If frm.sprMoveAfter.Name.Equals(objNm) OrElse frm.sprMoveBefor.Name.Equals(objNm) Then Exit Sub
        Call Me.NextFocusedControl(frm, eventFlg)

    End Sub

    ''' <summary>
    ''' Enter処理の特殊フォーカス移動
    ''' </summary>
    ''' <param name="eventFlg">Enterの場合、True</param>
    ''' <remarks></remarks>
    Private Sub NextFocusedControl(ByVal frm As LMD020F, ByVal eventFlg As Boolean)

        'Enter以外の場合、スルー
        If eventFlg = False Then
            Exit Sub
        End If

        frm.SelectNextControl(frm.ActiveControl, True, True, True, True)

    End Sub

    ''' <summary>
    ''' Enter処理時にPopを表示するかを判定
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <returns>True:参照する False:参照しない</returns>
    ''' <remarks></remarks>
    Private Function ChkOpenEnterAction(ByVal frm As LMD020F, ByVal objNm As String) As Boolean

        With frm

            Select Case objNm

                Case .sprMoveAfter.Name

                    Return False

                Case .sprMoveBefor.Name

                    Return False

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        Return False
                    End If

                    Return True

                Case .txtTouNo.Name, .txtSituNo.Name, .txtZoneCd.Name

                    If String.IsNullOrEmpty(.txtTouNo.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtSituNo.TextValue) = True _
                        AndAlso String.IsNullOrEmpty(.txtZoneCd.TextValue) = True Then
                        Return False
                    End If

                    Return True

                Case .txtDestCd.Name

                    If String.IsNullOrEmpty(.txtDestCd.TextValue) = True Then
                        Return False
                    End If

                    Return True

            End Select

            Return Not String.IsNullOrEmpty(DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox).TextValue)

        End With
    End Function

#End Region 'Enter処理

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索条件格納)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInData(ByVal frm As LMD020F)

        Dim datatable As DataTable = Me._FindDs.Tables(LMD020C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()

        With frm

            dr("NRS_BR_CD") = .cmbNrsBrCd.SelectedValue
            dr("WH_CD") = .cmbSoko.SelectedValue
            dr("CUST_CD_L") = .txtCustCdL.TextValue
            dr("CUST_CD_M") = .txtCustCdM.TextValue
            dr("INKO_DATE_FROM") = .imdNyukaFrom.TextValue
            dr("INKO_DATE_TO") = .imdNyukaTo.TextValue
            dr("IDO_DATE") = .imdIdoubi.TextValue
            '"全て"がチェックされている場合
            If .optAll.Checked = True Then
                dr("GOODS_COND_FLG") = LMConst.FLG.OFF
            Else
                dr("GOODS_COND_FLG") = LMConst.FLG.ON
            End If

            If .optHeikouIdo.Checked = True Then
                dr("IDO_SYUBETU") = LMConst.FLG.OFF
            Else
                dr("IDO_SYUBETU") = LMConst.FLG.ON
            End If
#If True Then   'ADD 2021/03/02 強制出庫指定時 018870   【LMS】強制出庫の出庫順を棚卸し一覧表と同じにして欲しい
            If .optKyoseiShuko.Checked = True Then
                dr("KYOSEI_SHUKO") = LMConst.FLG.ON
            Else
                dr("KYOSEI_SHUKO") = LMConst.FLG.OFF
            End If
#End If
        End With

        With frm.sprMoveBefor.ActiveSheet

            Dim rowCount As Integer = 0
            dr("GOODS_NM_1") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.GOODS_NM.ColNo))
            dr("LOT_NO") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.LOT_NO.ColNo)).ToUpper()
            dr("SERIAL_NO") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.SERIAL_NO.ColNo))
            dr("TOU_NO") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.TOU_NO.ColNo))
            dr("SITU_NO") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.SITU_NO.ColNo))
            dr("ZONE_CD") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.ZONE_CD.ColNo))
            'START YANAI 要望番号548
            'dr("LOCA") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.LOCA.ColNo))
            dr("LOCA") = Me._LMDConV.GetCellValueNotTrim(.Cells(rowCount, LMD020G.sprMoveBefor.LOCA.ColNo))
            'END YANAI 要望番号548
            dr("GOODS_CD_CUST") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo))
            dr("GOODS_COND_KB_1") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.GOODS_COND_NM_1.ColNo))
            dr("GOODS_COND_KB_2") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.GOODS_COND_NM_2.ColNo))
            dr("GOODS_COND_KB_3") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.GOODS_COND_NM_3.ColNo))
            dr("SPD_KB") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.SPD_KB_NM.ColNo))
            dr("OFB_KB") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.OFB_KB_NM.ColNo))
            dr("BYK_KEEP_GOODS_CD") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.KEEP_GOODS_NM.ColNo))
            dr("SEARCH_KEY_2") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.SEARCH_KEY_2.ColNo))
            dr("ALLOC_PRIORITY") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.ALLOC_PRIORITY_NM.ColNo))
            dr("DEST_NM") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.DEST_NM.ColNo))
            dr("RSV_NO") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.RSV_NO.ColNo))
            'START YANAI 要望番号548
            'dr("REMARK_OUT") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.REMARK_OUT.ColNo))
            'dr("REMARK") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.REMARK.ColNo))
            dr("REMARK_OUT") = Me._LMDConV.GetCellValueNotTrim(.Cells(rowCount, LMD020G.sprMoveBefor.REMARK_OUT.ColNo))
            dr("REMARK") = Me._LMDConV.GetCellValueNotTrim(.Cells(rowCount, LMD020G.sprMoveBefor.REMARK.ColNo))
            'END YANAI 要望番号548
            dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo))
            'START YANAI 要望番号766
            dr("CUST_CD_S") = Me._LMDConV.GetCellValue(.Cells(rowCount, LMD020G.sprMoveBefor.CUST_CD_S.ColNo))
            'END YANAI 要望番号766

        End With

        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(在庫履歴照会)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetInZaiData(ByVal frm As LMD020F)

        Dim datatable As DataTable = Me._FindDs.Tables(LMD030C.TABLE_NM_IN)
        Dim dr As DataRow = datatable.NewRow()
        Dim arrCheckList As ArrayList = Nothing

        arrCheckList = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

        With frm.sprMoveBefor.ActiveSheet

            Dim rowCount As Integer = 0
            rowCount = arrCheckList.Count - 1

            For i As Integer = 0 To rowCount

                dr("NRS_BR_CD") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.NRS_BR_CD.ColNo))
                dr("WH_CD") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.WH_CD.ColNo))
                dr("CUST_CD_L") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.CUST_CD_L.ColNo))
                dr("CUST_CD_M") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.CUST_CD_M.ColNo))
                dr("CUST_NM") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.CUST_NM_L.ColNo))
                dr("GOODS_CD_CUST") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo))
                dr("GOODS_NM_1") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.GOODS_NM.ColNo))
                dr("LOT_NO") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.LOT_NO.ColNo)).ToUpper()

                Dim irime As Decimal = Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.IRIME.ColNo)))
                Dim zaiqt As Decimal = Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo)))

                If zaiqt < irime Then
                    dr("IRIME") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo))
                Else
                    dr("IRIME") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.IRIME.ColNo))
                End If

                dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo))
                dr("HOKAN_NIYAKU_CALCULATION") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.HOKAN_NIYAKU_CALCULATION.ColNo))
                dr("LAST_OUTKO_DATE") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.OUTKO_DATE.ColNo))
                dr("DEL_VIEW_FLG") = LMD020C.DEL_VIEW_FLG
                dr("VIEW_FLG") = LMD020C.VIEW_FLG
                dr("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrCheckList(rowCount)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo))

            Next

        End With

        datatable.Rows.Add(dr)

    End Sub

    ''' <summary>
    ''' データセット設定(在庫移動データ作成のデータ取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">このハンドラクラスに紐づくデータセット</param>
    ''' <remarks></remarks>
    Private Sub SetDatasetAll(ByVal frm As LMD020F, ByVal ds As DataSet, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList)

        '移動先在庫データセットクラスを呼び出す
        Call Me.SetDataSetZaiNewData(frm, ds, arrSaki, arrMoto)

        '移動元在庫データセットクラスを呼び出す
        Call Me.SetDataSetZaiOldData(frm, ds, arrMoto, arrSaki)

        '在庫移動トランザクションデータセットクラスを呼び出す
        Call Me.SetDataSetIdoTrsData(frm, ds, arrSaki, arrMoto)

        '最新の請求日取得データセットクラスを呼び出す
        Call SetDataSetSeiqData(frm, ds, arrSaki, arrMoto)

    End Sub

    ''' <summary>
    ''' データセット設定(在庫データ登録)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <param name="arrSaki">移動先スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <param name="arrMoto">移動元スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetZaiNewData(ByVal frm As LMD020F, ByVal ds As DataSet, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD020C.TABLE_NM_ZAI_NEW).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD020C.TABLE_NM_ZAI_NEW)

        '移動先の項目
        Dim idoKosu As Decimal = 0
        Dim newPoraZaiQt As String = String.Empty

        '移動元の項目
        Dim poraZaiQt As Decimal = 0
        Dim allocCanNb As Decimal = 0
        Dim irime As Decimal = 0

        With frm.sprMoveAfter.ActiveSheet

            Dim rowCount As Integer = 0
            rowCount = arrSaki.Count - 1

            For i As Integer = 0 To rowCount

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo)).ToString()
                dr("ZAI_REC_NO") = String.Empty

                '平行移動、複数移動によって、セットする値を変更
                If frm.optFukusuIdo.Checked = True Then
                    dr("LOT_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.LOT_NO.ColNo)).ToString().ToUpper()
                    dr("CUST_CD_L") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.CUST_CD_L.ColNo)).ToString()
                    dr("CUST_CD_M") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.CUST_CD_M.ColNo)).ToString()
                    dr("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()
                    dr("INKA_NO_L") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.INKA_NO_L.ColNo)).ToString()
                    dr("INKA_NO_M") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.INKA_NO_M.ColNo)).ToString()
                    dr("INKA_NO_S") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.INKA_NO_S.ColNo)).ToString()
                    dr("SERIAL_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.SERIAL_NO.ColNo)).ToString()
                    dr("HOKAN_YN") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.HOKAN_YN.ColNo)).ToString()
                    dr("TAX_KB") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.TAX_KB.ColNo)).ToString()
                    irime = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.IRIME.ColNo)).ToString())
                    poraZaiQt = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo)).ToString())
                    dr("IRIME") = irime.ToString()
                    dr("INKO_DATE") = DateFormatUtility.DeleteSlash(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.INKO_DATE.ColNo)).ToString())
                    dr("ZERO_FLAG") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.ZERO_FLAG.ColNo)).ToString()
                    dr("SMPL_FLAG") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.SMPL_FLAG.ColNo)).ToString()
                    '要望番号:1350 terakawa 2012.08.29 Start
                    dr("GOODS_CD_CUST") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo)).ToString()
                    '要望番号:1350 terakawa 2012.08.29 End
                    'START ADD 2013/09/10 KOBAYASHI WIT対応
                    dr("GOODS_KANRI_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.GOODS_KANRI_NO.ColNo)).ToString()
                    'END   ADD 2013/09/10 KOBAYASHI WIT対応
                Else
                    dr("LOT_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.LOT_NO.ColNo)).ToString().ToUpper()
                    dr("CUST_CD_L") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.CUST_CD_L.ColNo)).ToString()
                    dr("CUST_CD_M") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.CUST_CD_M.ColNo)).ToString()
                    dr("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()
                    dr("INKA_NO_L") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.INKA_NO_L.ColNo)).ToString()
                    dr("INKA_NO_M") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.INKA_NO_M.ColNo)).ToString()
                    dr("INKA_NO_S") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.INKA_NO_S.ColNo)).ToString()
                    dr("SERIAL_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.SERIAL_NO.ColNo)).ToString()
                    dr("HOKAN_YN") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.HOKAN_YN.ColNo)).ToString()
                    dr("TAX_KB") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.TAX_KB.ColNo)).ToString()
                    irime = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.IRIME.ColNo)).ToString())
                    poraZaiQt = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.PORA_ZAI_QT.ColNo)).ToString())
                    dr("IRIME") = irime.ToString()
                    dr("INKO_DATE") = DateFormatUtility.DeleteSlash(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.INKO_DATE.ColNo)).ToString())
                    dr("ZERO_FLAG") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZERO_FLAG.ColNo)).ToString()
                    dr("SMPL_FLAG") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.SMPL_FLAG.ColNo)).ToString()
                    '要望番号:1350 terakawa 2012.08.29 Start
                    dr("GOODS_CD_CUST") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo)).ToString()
                    '要望番号:1350 terakawa 2012.08.29 End
                    'START ADD 2013/09/10 KOBAYASHI WIT対応
                    dr("GOODS_KANRI_NO") = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_KANRI_NO.ColNo)).ToString()
                    'END   ADD 2013/09/10 KOBAYASHI WIT対応
                End If

                dr("WH_CD") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.WH_CD_R.ColNo)).ToString()
                dr("TOU_NO") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString()
                dr("SITU_NO") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString()
                dr("ZONE_CD") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)).ToString()
                'START YANAI 要望番号548
                'dr("LOCA") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.LOCA_R.ColNo)).ToString().ToUpper()
                dr("LOCA") = Me._LMDConV.GetCellValueNotTrim(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.LOCA_R.ColNo)).ToString().ToUpper()
                'END YANAI 要望番号548
                dr("ALLOC_PRIORITY") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo)).ToString()
                dr("RSV_NO") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.RSV_NO_R.ColNo)).ToString()
                dr("GOODS_COND_KB_1") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo)).ToString()
                dr("GOODS_COND_KB_2") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo)).ToString()
                dr("GOODS_COND_KB_3") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo)).ToString()
                dr("OFB_KB") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.OFB_KB_R.ColNo)).ToString()
                dr("SPD_KB") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SPD_KB_R.ColNo)).ToString()
                dr("BYK_KEEP_GOODS_CD") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.BYK_KEEP_GOODS_CD_R.ColNo)).ToString()
                'START YANAI 要望番号548
                'dr("REMARK_OUT") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo)).ToString().ToUpper()
                dr("REMARK_OUT") = Me._LMDConV.GetCellValueNotTrim(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo)).ToString().ToUpper()
                'END YANAI 要望番号548
                idoKosu = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)).ToString())
                dr("PORA_ZAI_NB") = idoKosu.ToString()
                dr("ALCTD_NB") = LMConst.FLG.OFF
                dr("ALLOC_CAN_NB") = idoKosu.ToString()

                'START YANAI 要望番号1391 在庫移動処理で全量移動をしないときに移動先の数量がおかしい
                'If idoKosu = 0 AndAlso poraZaiQt < irime Then
                '    newPoraZaiQt = poraZaiQt.ToString()
                'Else
                '    newPoraZaiQt = (idoKosu * irime).ToString()
                'End If
                If poraZaiQt < irime Then
                    newPoraZaiQt = poraZaiQt.ToString()
                Else
                    newPoraZaiQt = (idoKosu * irime).ToString()
                End If
                'END YANAI 要望番号1391 在庫移動処理で全量移動をしないときに移動先の数量がおかしい

                '要望番号:1508 KIM 2012/10/12 START
                '対象データが小分けデータの場合
                Dim alctdQt As Decimal = 0
                '2017/04/24 Shinoda Mod 要望管理2686 Start
                'alctdQt = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALCTD_QT.ColNo)).ToString())
                If frm.optFukusuIdo.Checked = True Then
                    alctdQt = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.ALCTD_QT.ColNo)).ToString())
                Else
                    alctdQt = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALCTD_QT.ColNo)).ToString())
                End If
                '2017/04/24 Shinoda Mod 要望管理2686 End

                If 0 < alctdQt AndAlso alctdQt < irime Then
                    newPoraZaiQt = (irime - alctdQt).ToString()
                End If
                '要望番号:1508 KIM 2012/10/12 END

                dr("PORA_ZAI_QT") = newPoraZaiQt
                dr("ALCTD_QT") = LMConst.FLG.OFF
                dr("ALLOC_CAN_QT") = newPoraZaiQt

                dr("INKO_PLAN_DATE") = frm.imdIdoubi.TextValue
                dr("LT_DATE") = DateFormatUtility.DeleteSlash(Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.LT_DATE_R.ColNo)).ToString())
                dr("GOODS_CRT_DATE") = DateFormatUtility.DeleteSlash(Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo)).ToString())
                dr("DEST_CD_P") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.DEST_CD_R.ColNo)).ToString()
                'START YANAI 要望番号548
                'dr("REMARK") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.REMARK_R.ColNo)).ToString().ToUpper()
                dr("REMARK") = Me._LMDConV.GetCellValueNotTrim(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.REMARK_R.ColNo)).ToString().ToUpper()
                'END YANAI 要望番号548

                'start 要望管理009859
                '事由欄=自動倉庫移動で、移動先が自動倉庫ならばロケーション(=パレット)をクリアする
                If LMD020C.JIYURAN_AUTO_IDO.Equals(frm.cmbJiyuran.SelectedValue) Then
                    Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(
                                                                        String.Concat(
                                                                        "KBN_GROUP_CD = 'O014' ",
                                                                        "AND KBN_NM1 = '", dr("NRS_BR_CD").ToString(), "' ",
                                                                        "AND KBN_NM2 = '", dr("WH_CD").ToString(), "' ",
                                                                        "AND KBN_NM3 = '", dr("TOU_NO").ToString(), "' ",
                                                                        "AND KBN_NM4 = '", dr("SITU_NO").ToString(), "' "))
                    If getDr.Length > 0 Then
                        dr("LOCA") = String.Empty
                    End If
                End If
                'end 要望管理009859

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD020C.TABLE_NM_ZAI_NEW).ImportRow(inTbl.Rows(0))

            Next

        End With


    End Sub

    ''' <summary>
    ''' データセット設定(在庫データ更新)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <param name="arrSaki">移動先スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <param name="arrMoto">移動元スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetZaiOldData(ByVal frm As LMD020F, ByVal ds As DataSet, ByVal arrMoto As ArrayList, ByVal arrSaki As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD020C.TABLE_NM_ZAI_OLD).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD020C.TABLE_NM_ZAI_OLD)

        '移動先の項目
        Dim idoKosu As Decimal = 0
        Dim totalIdoKosu As Decimal = 0

        '移動元の項目
        Dim poraZaiNb As Decimal = 0
        Dim allocCanNb As Decimal = 0
        Dim irime As Decimal = 0

        totalIdoKosu = Me.calcIdoKosu(frm, arrSaki)

        With frm.sprMoveBefor.ActiveSheet

            Dim rowCount As Integer = 0
            rowCount = arrMoto.Count - 1

            For i As Integer = 0 To rowCount

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                poraZaiNb = Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.PORA_ZAI_NB.ColNo)).ToString())
                allocCanNb = Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo)).ToString())
                irime = Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.IRIME.ColNo)).ToString())

                If frm.optHeikouIdo.Checked = True Then
                    idoKosu = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)).ToString())
                Else
                    idoKosu = totalIdoKosu
                End If

                dr("NRS_BR_CD") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.NRS_BR_CD.ColNo)).ToString()
                dr("ZAI_REC_NO") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo)).ToString()
                dr("PORA_ZAI_NB") = (poraZaiNb - idoKosu).ToString()
                dr("ALCTD_NB") = (Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALCTD_NB.ColNo)).ToString())).ToString()
                dr("ALLOC_CAN_NB") = (allocCanNb - idoKosu).ToString()
                dr("IRIME") = (Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.IRIME.ColNo)).ToString())).ToString()
                dr("PORA_ZAI_QT") = ((poraZaiNb - idoKosu) * irime).ToString()
                dr("ALLOC_CAN_QT") = ((allocCanNb - idoKosu) * irime).ToString()

                '要望番号:1508 KIM 2012/10/12 START
                '対象データが小分けデータの場合
                Dim alctdQt As Decimal = 0
                alctdQt = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALCTD_QT.ColNo)).ToString())
                If 0 < alctdQt AndAlso alctdQt < irime Then
                    If Convert.ToDecimal(dr("PORA_ZAI_NB")) = 0 Then
                        dr("PORA_ZAI_NB") = LMConst.FLG.ON
                        dr("ALLOC_CAN_NB") = LMConst.FLG.ON
                    End If
                    dr("PORA_ZAI_QT") = alctdQt
                    dr("ALLOC_CAN_QT") = LMConst.FLG.OFF
                End If
                '要望番号:1508 KIM 2012/10/12 END

                '移動先の移動個数に値によってゼロフラグの値を可変
                If (allocCanNb - idoKosu) = 0 Then
                    dr("ZERO_FLAG") = "03"
                Else
                    dr("ZERO_FLAG") = String.Empty
                End If

                dr("GUI_SYS_UPD_DATE") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.SYS_UPD_DATE.ColNo)).ToString()
                dr("GUI_SYS_UPD_TIME") = Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.SYS_UPD_TIME.ColNo)).ToString()

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD020C.TABLE_NM_ZAI_OLD).ImportRow(inTbl.Rows(0))
            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(移動在庫トランザクション登録)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <param name="arrSaki">移動先スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <param name="arrMoto">移動元スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetIdoTrsData(ByVal frm As LMD020F, ByVal ds As DataSet, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim dr As DataRow = setDs.Tables(LMD020C.TABLE_NM_IDO).NewRow()
        Dim inTbl As DataTable = setDs.Tables(LMD020C.TABLE_NM_IDO)

        '既にセットしてある項目を参照する為にインスタンスを生成
        Dim zaiOldDr As DataRow = ds.Tables(LMD020C.TABLE_NM_ZAI_OLD).Rows(0)
        Dim zaiNewDr As DataRow = Nothing

        With frm.sprMoveAfter.ActiveSheet

            Dim rowCount As Integer = 0
            rowCount = arrSaki.Count - 1
            Dim motoRow As Integer = 0

            For i As Integer = 0 To rowCount

                zaiNewDr = ds.Tables(LMD020C.TABLE_NM_ZAI_NEW).Rows(i)

                '平行移動時のみセットするdtを変更
                If frm.optHeikouIdo.Checked = True Then
                    zaiOldDr = ds.Tables(LMD020C.TABLE_NM_ZAI_OLD).Rows(i)
                    motoRow = i
                End If

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo)).ToString()
                dr("REC_NO") = String.Empty
                dr("IDO_DATE") = frm.imdIdoubi.TextValue
                dr("O_ZAI_REC_NO") = zaiOldDr.Item("ZAI_REC_NO").ToString()
                dr("O_PORA_ZAI_NB") = Convert.ToString(Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.PORA_ZAI_NB.ColNo)).ToString()))
                dr("O_ALCTD_NB") = zaiOldDr.Item("ALCTD_NB").ToString()
                dr("O_ALLOC_CAN_NB") = Convert.ToString(Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo)).ToString()))
                dr("O_IRIME") = zaiOldDr.Item("IRIME").ToString()
                dr("N_ZAI_REC_NO") = zaiNewDr.Item("ZAI_REC_NO").ToString()
                dr("N_PORA_ZAI_NB") = zaiNewDr.Item("PORA_ZAI_NB").ToString()
                dr("N_ALCTD_NB") = zaiNewDr.Item("ALCTD_NB").ToString()
                dr("N_ALLOC_CAN_NB") = zaiNewDr.Item("ALLOC_CAN_NB").ToString()
                dr("REMARK_KBN") = frm.cmbJiyuran.SelectedValue
                dr("REMARK") = frm.txtJiyuran.TextValue
                dr("HOKOKU_DATE") = String.Empty
                dr("ZAIK_ZAN_FLG") = "00"
                If LMD020C.JIYURAN_AUTO_IDO.Equals(frm.cmbJiyuran.SelectedValue) Then
                    '事由欄=自動倉庫移動なら無条件に"01"
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.TOU_NO.ColNo)).ToString().Equals _
                    (Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString()) = False Then
                    '棟
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.SITU_NO.ColNo)).ToString().Equals _
                    (Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString()) = False Then
                    '室
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.ZONE_CD.ColNo)).ToString().Equals _
                    (Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)).ToString()) = False Then
                    'ゾーン
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                'START YANAI 要望番号548
                'If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                '    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.LOCA.ColNo)).ToString().Equals _
                '    (Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.LOCA_R.ColNo)).ToString()) = False Then
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValueNotTrim(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.LOCA.ColNo)).ToString().Equals _
                    (Me._LMDConV.GetCellValueNotTrim(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.LOCA_R.ColNo)).ToString()) = False Then
                    'END YANAI 要望番号548
                    'ロケーション
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.GOODS_COND_NM_1.ColNo)).ToString().Equals _
                    (frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_1_R.ColNo).Text.Trim) = False Then
                    '状態中身
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.GOODS_COND_NM_2.ColNo)).ToString().Equals _
                    (frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_2_R.ColNo).Text.Trim) = False Then
                    '状態外装
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.GOODS_COND_NM_3.ColNo)).ToString().Equals _
                    (frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.GOODS_COND_KB_3_R.ColNo).Text.Trim) = False Then
                    '状態荷主
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.SPD_KB_NM.ColNo)).ToString().Equals _
                    (frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SPD_KB_R.ColNo).Text.Trim) = False Then
                    '保留品
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.OFB_KB_NM.ColNo)).ToString().Equals _
                    (frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.OFB_KB_R.ColNo).Text.Trim) = False Then
                    '簿外品
                    dr("ZAIK_ZAN_FLG") = "01"
                End If
                'START YANAI 要望番号548
                'If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                '    Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.REMARK.ColNo)).ToString().Equals _
                '    (Me._LMDConV.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.REMARK_R.ColNo)).ToString()) = False Then
                If ("00").Equals(dr("ZAIK_ZAN_FLG").ToString) AndAlso _
                    Me._LMDConV.GetCellValueNotTrim(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.REMARK.ColNo)).ToString().Equals _
                    (Me._LMDConV.GetCellValueNotTrim(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.REMARK_R.ColNo)).ToString()) = False Then
                    'END YANAI 要望番号548
                    '備考小(社内)
                    dr("ZAIK_ZAN_FLG") = "01"
                End If

                '2013/1/11 要望番号1765 Start
                Dim zaikIrime As String = "0"
                Dim alloc_can_qt As Decimal = Convert.ToDecimal(Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(motoRow)), LMD020G.sprMoveBefor.ALLOC_CAN_QT.ColNo)).ToString())
                If Convert.ToDecimal(zaiOldDr.Item("IRIME").ToString()) < alloc_can_qt Then
                    zaikIrime = zaiOldDr.Item("IRIME").ToString()
                Else
                    zaikIrime = alloc_can_qt.ToString()
                End If
                dr("ZAIK_IRIME") = zaikIrime
                'End

                'start 要望管理009859
                '出庫指示番号,入庫指示番号（事由欄=自動倉庫移動の際は採番値が入るが、それはSQL編集時に行う）
                dr("OUTKO_NO") = String.Empty
                dr("INKO_NO") = String.Empty

                'プリフィックス（出庫(入庫)指示番号の接頭語）
                Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(
                                                                    String.Concat(
                                                                    "KBN_GROUP_CD = 'D003' ",
                                                                    "AND KBN_NM1 = '", dr("NRS_BR_CD").ToString(), "' "))
                If getDr.Length = 0 Then
                    dr("PREFIX") = "0"
                Else
                    dr("PREFIX") = getDr(0).Item("KBN_NM6").ToString()
                End If
                'end 要望管理009859

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD020C.TABLE_NM_IDO).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub

    ''' <summary>
    ''' データセット設定(最新の請求日取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="ds">保存処理時に使用するデータセット</param>
    ''' <param name="arrSaki">移動先スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <remarks></remarks>
    Private Sub SetDataSetSeiqData(ByVal frm As LMD020F, ByVal ds As DataSet, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList)

        '別インスタンスのデータセットを宣言(コピーして)
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(LMD020C.TABLE_NM_SEIQ_HED_IN)

        With frm.sprMoveAfter.ActiveSheet

            Dim rowCount As Integer = 0
            Dim goodsCustNrs As String = String.Empty
            Dim custCdL As String = String.Empty
            Dim custCdM As String = String.Empty
            Dim custCdS As String = String.Empty
            Dim custCdSS As String = String.Empty
            Dim drGoods As DataRow() = Nothing
            Dim drCust As DataRow() = Nothing
            Dim hokanSeiqCd As String = String.Empty

            rowCount = arrMoto.Count - 1

            For i As Integer = 0 To rowCount

                Dim dr As DataRow = setDs.Tables(LMD020C.TABLE_NM_SEIQ_HED_IN).NewRow()

                '商品KEYの取得
                If frm.optHeikouIdo.Checked = True Then
                    goodsCustNrs = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()
                Else
                    goodsCustNrs = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()
                End If

                '商品マスタより、荷主コードを取得
                '---↓
                'drGoods = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", goodsCustNrs, "' "))

                Dim goodsDs As MGoodsDS = New MGoodsDS
                Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                goodsDr.Item("GOODS_CD_NRS") = goodsCustNrs
                goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/11 035090   【LMS】住友ファーマ　在庫移動ができない
                goodsDr.Item("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()

#End If
                goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                '---↑

                custCdL = drGoods(0).Item("CUST_CD_L").ToString()
                custCdM = drGoods(0).Item("CUST_CD_M").ToString()
                custCdS = drGoods(0).Item("CUST_CD_S").ToString()
                custCdSS = drGoods(0).Item("CUST_CD_SS").ToString()

                '荷主マスタより保管料請求先コードを取得
                drCust = Me._LMDConV.SelectCustListDataRow(custCdL, custCdM, custCdS, custCdSS)
                hokanSeiqCd = drCust(0).Item("HOKAN_SEIQTO_CD").ToString()

                '別インスタンスのデータロウを空にする
                inTbl.Clear()

                dr("NRS_BR_CD") = frm.cmbNrsBrCd.SelectedValue.ToString()
                dr("SEIQTO_CD") = hokanSeiqCd
                dr("IDO_DATE") = frm.imdIdoubi.TextValue

                inTbl.Rows.Add(dr)

                '持ちまわっているデータセットにつめなおす
                ds.Tables(LMD020C.TABLE_NM_SEIQ_HED_IN).ImportRow(inTbl.Rows(0))

            Next

        End With

    End Sub

    ''' <summary>
    ''' 引当可能梱数(移動個数)を計算して返す
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Function calcIdoKosu(ByVal frm As LMD020F, ByVal arr As ArrayList) As Decimal

        '返却する変数
        Dim rtnValue As Decimal = 0

        With frm.sprMoveAfter.ActiveSheet

            'スプレッドシートのチェックの分だけ検索条件をセット
            For i As Integer = 0 To arr.Count - 1

                '個数の計算
                rtnValue = rtnValue + Convert.ToDecimal(Me._LMDConV.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)).ToString())

            Next

        End With

        Return rtnValue

    End Function

    ''' <summary>
    ''' データセット設定(保管・荷役料最終計算日取得)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="arrMoto">移動先スプレッドシートのチェック内容を保持しているArrayList</param>
    ''' <remarks></remarks>
    Private Function SetDataSetHokanNiyakuData(ByVal frm As LMD020F, ByVal arrMoto As ArrayList) As DataSet

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '別インスタンスのデータセットを宣言(コピーして)
        Dim rtDs As DataSet = New LMD000DS()
        Dim inTbl As DataTable = rtDs.Tables(LMControlC.LMD000_TABLE_NM_IN)

        Dim goodsCdNrs As String = String.Empty
        Dim rowCount As Integer = 0
        Dim hokanNiyakuDate As String = String.Empty

        With frm.sprMoveAfter.ActiveSheet

            rowCount = arrMoto.Count - 1

            For i As Integer = 0 To rowCount

                Dim dr As DataRow = inTbl.NewRow()

                '商品KEYの取得
                If frm.optHeikouIdo.Checked = True Then
                    goodsCdNrs = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()
                Else
                    goodsCdNrs = Me._LMDConV.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(0)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()
                End If

                dr(LMControlC.LMD000_COL_GOODS_CD_NRS) = goodsCdNrs
                dr(LMControlC.LMD000_COL_NRS_BR_CD) = frm.cmbNrsBrCd.SelectedValue
                dr(LMControlC.LMD000_COL_CHK_DATE) = frm.imdIdoubi.TextValue

                '2017/09/25 修正 李↓
                dr(LMControlC.LMD000_COL_REPLACE_STR1) = lgm.Selector({"保管料・荷役料が既に計算されている", "Storage fees and handling fee has already been calculated", "보관료/하역료가 이미 계산되어있음", "中国語"})
                '2017/09/25 修正 李↑

                dr(LMControlC.LMD000_COL_REPLACE_STR2) = frm.FunctionKey.F11ButtonName


                'dr(LMControlC.LMD000_COL_REPLACE_STR1) = "保管料・荷役料が既に計算されている"
                'dr(LMControlC.LMD000_COL_REPLACE_STR2) = "保存"

                inTbl.Rows.Add(dr)

            Next

        End With

        Return rtDs

    End Function

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 新規入荷チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrSaki">チェック行群</param>
    ''' <param name="ikkatsu">0：行毎、1：一括</param>
    ''' <param name="expDs"></param>
    ''' <returns></returns>
    Friend Function IsTouSituZoneCheck(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal ikkatsu As String, ByVal expDs As DataSet) As Boolean

        With frm

            Dim sakiMax As Integer = arrSaki.Count - 1
            Dim checkRow As Integer = 0
            Dim checkMotoRow As Integer = 0

            Dim nrsbrcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim custcd As String = .txtCustCdL.TextValue

            Dim goodsNRS As String = String.Empty

            Dim tousituDr As DataRow() = Nothing
            Dim soko_kbn As String = String.Empty
            Dim isTasya As Boolean = False

            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim InkaNb As Decimal

            Dim msg As String = String.Empty

            'チェック対象行がない場合は終了
            If 0 > sakiMax Then Return True

            '新規入荷チェックを行うか、荷主明細マスタ(M_CUST_DETAILS)の用途区分（荷主明細）(Y008).新規入荷チェック不要フラグ(A2)で判定
            'DataSet設定
            Dim chkDs As DataSet = New LMZ340DS()
            Dim inTbl As DataTable = chkDs.Tables(LMZ340C.TABLE_NM_IN)
            Dim row As DataRow = inTbl.NewRow

            '最大保管数量取得用
            Dim capaDs As DataSet = New LMZ340DS()
            Dim capaInTbl As DataTable = capaDs.Tables(LMZ340C.TABLE_NM_IN)

            row("NRS_BR_CD") = nrsbrcd
            row("CUST_CD") = custcd

            inTbl.Rows.Add(row)

            chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckFlg", chkDs)

            If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_FLG).Rows.Count > 0 Then
                Dim flgA2 As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_FLG).Rows(0).Item("FLG_A2").ToString

                'フラグが1で設定されている場合、エラーなしで処理終了
                If "1".Equals(flgA2) Then
                    Return True
                End If

            End If

            '移動元のチェックを取得
            Dim arrMoto As ArrayList = Me._LMDConH.GetCheckList(frm.sprMoveBefor.ActiveSheet, LMD020G.sprMoveBefor.DEF.ColNo)

            '★★★属性チェック
            For i As Integer = 0 To sakiMax

                checkRow = Convert.ToInt32(arrSaki(i).ToString)

                If frm.optHeikouIdo.Checked = True Then
                    '平行移動の場合
                    checkMotoRow = checkRow
                Else
                    '複数移動の場合
                    checkMotoRow = Convert.ToInt32(arrMoto(0).ToString)
                End If

                '商品マスタ
                goodsNRS = Me._LMDConV.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString

                '移動後
                If "0".Equals(ikkatsu) Then
                    touNo = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString
                    situNo = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString
                    zoneCd = Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)).ToString
                    InkaNb = Convert.ToDecimal(Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)))
                Else
                    '一括変更
                    touNo = .txtTouNo.TextValue.Trim
                    situNo = .txtSituNo.TextValue.Trim
                    zoneCd = .txtZoneCd.TextValue.Trim
                    InkaNb = 0  '数量チェックは行わない
                End If

                '棟室マスタ
                tousituDr = Me._LMDConV.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)

                '棟室マスタが取得できなければエラー、ワーニングとも起こさせない。
                If tousituDr.Length.Equals(0) Then
                    Continue For
                End If

                soko_kbn = tousituDr(0).Item("SOKO_KB").ToString
                isTasya = tousituDr(0).Item("JISYATASYA_KB").ToString.Equals("02")

                '他社の場合はワーニングを出さない
                If isTasya.Equals(False) Then

                    msg = String.Concat(touNo, "-", situNo)
                    If String.IsNullOrEmpty(zoneCd) = False Then
                        msg = String.Concat(msg, "-", zoneCd)
                    End If

                    '属性系チェック
                    'DataSet設定
                    chkDs = New LMZ340DS()
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                    row = inTbl.NewRow

                    row("NRS_BR_CD") = nrsbrcd
                    row("GOODS_CD_NRS") = goodsNRS
                    row("WH_CD") = sokocd
                    row("TOU_NO") = touNo
                    row("SITU_NO") = situNo
                    row("ZONE_CD") = zoneCd

                    inTbl.Rows.Add(row)

                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckAttr", chkDs)

                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows.Count > 0 Then
                        Dim DokuKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("DOKU_KB_ERR").ToString
                        Dim KouathuGasKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("KOUATHUGAS_KB_ERR").ToString
                        Dim YakuzihoKbErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("YAKUZIHO_KB_ERR").ToString
                        Dim ShoboCdErr As String = chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_ATTR).Rows(0).Item("SHOBO_CD_ERR").ToString

                        'フラグが1で設定されている場合、エラー
                        If "1".Equals(DokuKbErr) Then
                            If (MyBase.ShowMessage(frm, "W299", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                        If "1".Equals(KouathuGasKbErr) Then
                            If (MyBase.ShowMessage(frm, "W300", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                        If "1".Equals(YakuzihoKbErr) Then
                            If (MyBase.ShowMessage(frm, "W301", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                        If "1".Equals(ShoboCdErr) Then
                            If (MyBase.ShowMessage(frm, "W302", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                        End If
                    End If

                    '最大保管数量用にDataSet設定
                    Dim capaRow As DataRow = capaInTbl.NewRow

                    capaRow("NRS_BR_CD") = nrsbrcd
                    capaRow("GOODS_CD_NRS") = goodsNRS
                    capaRow("WH_CD") = sokocd
                    capaRow("TOU_NO") = touNo
                    capaRow("SITU_NO") = situNo
                    capaRow("ZONE_CD") = zoneCd
                    capaRow("INKA_NB") = InkaNb

                    capaInTbl.Rows.Add(capaRow)

                End If

            Next

            '★★★最大保管数量チェック(行毎のみ)
            If "1".Equals(ikkatsu) OrElse capaDs.Tables(LMZ340C.TABLE_NM_IN).Rows.Count = 0 Then
                Return True
            End If

            '移動先を棟室順に処理
            Dim drSCapa As DataRow() = capaDs.Tables(LMZ340C.TABLE_NM_IN).Select(Nothing, "TOU_NO ASC, SITU_NO ASC")
            Dim maxSCapa As Integer = drSCapa.Length - 1

            '移動先がない場合は終了
            If 0 > maxSCapa Then Return True

            'ブレイクキー
            Dim keyTouNo As String = String.Empty
            Dim keySituNo As String = String.Empty
            Dim keyTouSituSkip As Boolean = False

            '棟室の貯蔵最大数量
            Dim MaxQty As Decimal
            '棟室の現在の在庫の数量
            Dim ZaiQty As Decimal
            '入庫可能商品数量
            Dim chkQty As Decimal
            '移動元商品数量
            Dim motoQty As Decimal

            '棟室マスタ
            For j As Integer = 0 To maxSCapa

                If Not keyTouNo.Equals(drSCapa(j).Item("TOU_NO").ToString()) _
                    Or Not keySituNo.Equals(drSCapa(j).Item("SITU_NO").ToString()) Then

                    '棟室が変わったら、最大保管数量を取得
                    keyTouNo = drSCapa(j).Item("TOU_NO").ToString()
                    keySituNo = drSCapa(j).Item("SITU_NO").ToString()

                    MaxQty = 0
                    ZaiQty = 0
                    chkQty = 0
                    motoQty = 0
                    keyTouSituSkip = False

                    msg = String.Concat(keyTouNo, "-", keySituNo)

                    'DataSet設定
                    chkDs = New LMZ340DS()
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                    row = inTbl.NewRow

                    row("NRS_BR_CD") = nrsbrcd
                    row("WH_CD") = sokocd
                    row("TOU_NO") = keyTouNo
                    row("SITU_NO") = keySituNo

                    inTbl.Rows.Add(row)

                    '貯蔵最大数量検索
                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCheckCapa", chkDs)

                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows.Count > 0 Then
                        MaxQty = Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows(0).Item("MAX_QTY").ToString)
                        ZaiQty = Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CHECK_CAPA).Rows(0).Item("ZAI_QTY").ToString)
                    End If

                    '貯蔵最大数量 が 0 の場合、チェック対象外とする。
                    If MaxQty <= 0 Then
                        keyTouSituSkip = True
                    Else
                        '移動元に対象の棟室があるか確認
                        For k As Integer = 0 To arrMoto.Count - 1

                            checkMotoRow = Convert.ToInt32(arrMoto(k).ToString)

                            '移動元に対象の棟室がある場合は在庫に加算
                            If keyTouNo.Equals(Me._LMDConV.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveBefor.TOU_NO.ColNo)).ToString) _
                                And keySituNo.Equals(Me._LMDConV.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveBefor.SITU_NO.ColNo)).ToString) Then

                                '移動元商品数量計算
                                'DataSet設定
                                chkDs = New LMZ340DS()
                                inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                                row = inTbl.NewRow

                                row("NRS_BR_CD") = Me._LMDConV.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveBefor.NRS_BR_CD.ColNo)).ToString
                                row("GOODS_CD_NRS") = Me._LMDConV.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString

                                '移動個数を計算
                                Dim poraZaiNb As Decimal
                                Dim idoKosu As Decimal

                                poraZaiNb = Convert.ToDecimal(Me._LMDConV.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveBefor.PORA_ZAI_NB.ColNo)).ToString())

                                If frm.optHeikouIdo.Checked = True Then
                                    '平行移動の場合
                                    idoKosu = Convert.ToDecimal(Me._LMDConV.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkMotoRow, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)).ToString())
                                Else
                                    '複数移動の場合
                                    idoKosu = Me.calcIdoKosu(frm, arrSaki)  'トータル移動個数
                                End If

                                row("INKA_NB") = (poraZaiNb - idoKosu).ToString()

                                inTbl.Rows.Add(row)

                                '入庫商品数量を計算
                                chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCalcQty", chkDs)

                                If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 Then
                                    '入庫可能商品数量に計算した入庫商品数量をプラス
                                    motoQty = motoQty + Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString)
                                End If
                            End If

                        Next

                    End If

                    '入庫可能商品数量を計算
                    chkQty = MaxQty - ZaiQty + motoQty

                End If

                '貯蔵最大数量 が 0 または 既にエラーとなっている棟室はスキップ
                If keyTouSituSkip = False Then

                    '対象商品数量計算
                    'DataSet設定
                    chkDs = New LMZ340DS()
                    inTbl = chkDs.Tables(LMZ340C.TABLE_NM_IN)
                    row = inTbl.NewRow

                    '入荷(中)の対象行
                    row("NRS_BR_CD") = drSCapa(j).Item("NRS_BR_CD").ToString()
                    row("GOODS_CD_NRS") = drSCapa(j).Item("GOODS_CD_NRS").ToString()
                    row("INKA_NB") = drSCapa(j).Item("INKA_NB").ToString()

                    inTbl.Rows.Add(row)

                    '入庫商品数量を計算
                    chkDs = MyBase.CallWSA("LMZ340BLF", "SelectCalcQty", chkDs)

                    'UPD 2021/10/04 024123 【LMS】危険物管理_第2弾_アラート機能実装_再実装
                    '危険品でない場合処理しない
                    If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 AndAlso
                        Not Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString).Equals(0) Then

                        If chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows.Count > 0 Then
                            '入庫可能商品数量から計算した入庫商品数量をマイナス
                            chkQty = chkQty - Convert.ToDecimal(chkDs.Tables(LMZ340C.TABLE_NM_OUT_CALC_QTY).Rows(0).Item("INK_QTY").ToString)
                        End If

                        '入庫商品数量 >入庫可能商品数量 の場合
                        If chkQty < 0 Then
                            If (MyBase.ShowMessage(frm, "W298", New String() {msg}) <> MsgBoxResult.Ok) Then Return False
                            keyTouSituSkip = True
                        End If

                    End If


                End If

            Next

        End With

        Return True

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F6押下時処理呼び出し（在庫履歴照会）
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey6Press(ByRef frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ZaiHistoryShowEvent")

        '在庫履歴照会
        Me.ZaiHistoryShow(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ZaiHistoryShowEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F10押下時処理呼び出し(マスタ参照)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey10Press(ByRef frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "MasterShowEvent")

        'マスタ参照
        Me.OpenMasterPop(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "MasterShowEvent")


    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveCostItemData")

        Me.SaveIdoData(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveCostItemData")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByRef frm As LMD020F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Me.CloseFormEvent(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub


    '========================  ↓↓↓その他のイベント ↓↓↓========================

    ''' <summary>
    ''' Enter押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub EnterKeyDown(ByRef frm As LMD020F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EnterKeyDown")

        Call Me.EnterAction(frm, e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EnterKeyDown")

    End Sub

    ''' <summary>
    ''' ◀ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnSprMoveLeft_Click(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnSprMoveLeft_Click")

        Call Me.btnSprMoveLeftClickAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnSprMoveLeft_Click")

    End Sub

    ''' <summary>
    ''' ▶ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnSprMoveRight_Click(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnSprMoveRight_Click")

        Call btnSprMoveRightClickAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnSprMoveRight_Click")

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnLineDel_Click(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnLineDel_Click")

        Me.DeleteRowsSaki(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnLineDel_Click")

    End Sub

    ''' <summary>
    ''' 行追加ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnLineAdd_Click(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnLineAdd_Click")

        Me.SprAddRowEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnLineAdd_Click")

    End Sub

    ''' <summary>
    ''' 一括変更ボタン押下時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub btnAllChange_Click(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "btnAllChange_Click")

        Me.SprAllChangeEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "btnAllChange_Click")

    End Sub

    ''' <summary>
    ''' スプレッドの値変更イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Friend Sub spdMoveBefor_Change(ByVal frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "spdMoveBefor_Change")

        '「スプレッド変更」処理
        Me.SelectIdoMotoEvent(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "spdMoveBefor_Change")

    End Sub

    ''' <summary>
    ''' 営業所変更時処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub cmbNrsBrCd_Change(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "cmbNrsBrCd_Change")

        frm.cmbSoko.SelectedValue = LMUserInfoManager.GetWhCd

        MyBase.Logger.EndLog(MyBase.GetType.Name, "cmbNrsBrCd_Change")

    End Sub

    ''' <summary>
    ''' 移動元スプレッド縦スクロールイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprMoveBefor_TopChange(ByVal frm As LMD020F, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprMoveBefor_TopChange")

        Call Me.sprTopUnderWithChange(frm, e, LMConst.FLG.ON)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprMoveBefor_TopChange")

    End Sub

    ''' <summary>
    ''' 移動先スプレッド縦スクロールイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprMoveAfter_TopChange(ByVal frm As LMD020F, ByVal e As FarPoint.Win.Spread.TopChangeEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprMoveAfter_TopChange")

        Call Me.sprTopUnderWithChange(frm, e, LMConst.FLG.OFF)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprMoveAfter_TopChange")

    End Sub

    ''' <summary>
    ''' 複数移動ラジオチェック状態変更時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub optFukusuIdo_CheckedChanged(ByVal frm As LMD020F)
        Call Me.preActionIdo(frm)
    End Sub

    ''' <summary>
    ''' 強制出庫ラジオチェック状態変更時
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub optKyoseiShuko_CheckedChanged(ByVal frm As LMD020F)
        Call Me.preActionIdo(frm)
    End Sub

    ''' <summary>
    ''' 移動元スプレッドドラッグイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub sprMoveBefor_MouseUp(ByVal frm As LMD020F)
        Call Me.sprBeforeDragAction(frm)
    End Sub

    ''' <summary>
    ''' 移動先スプレッドドラッグイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub sprMoveAfter_MouseUp(ByVal frm As LMD020F)
        Call Me.sprAfterDragAction(frm)
    End Sub

    ''' <summary>
    ''' 棟 + 室 + ZONE（置き場情報）温度管理チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrMoto"></param>
    ''' <param name="arrSaki"></param>
    ''' <param name="ikkatsu"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOndoCheck(ByVal frm As LMD020F, ByVal arrMoto As ArrayList, ByVal arrSaki As ArrayList, ByVal ikkatsu As String) As Boolean

        Return Me._V.IsOndoCheck(arrMoto, arrSaki, ikkatsu)

    End Function

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    Private Function IsDangerousGoodsCheck(ByVal frm As LMD020F, ByVal arrMoto As ArrayList, ByVal ikkatsu As String, ByVal expDs As DataSet) As Boolean

        Return Me._V.IsDangerousGoodsCheck(frm, arrMoto, ikkatsu, expDs)

    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

    ''' <summary>
    ''' 荷主(大)コードのフォーカス移動
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub txtCustCdL_Leave(ByRef frm As LMD020F)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "txtCustCdL_Leave")

        Call Me.txtCustCdLLeaveAction(frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "txtCustCdL_Leave")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#End Region 'Method

End Class