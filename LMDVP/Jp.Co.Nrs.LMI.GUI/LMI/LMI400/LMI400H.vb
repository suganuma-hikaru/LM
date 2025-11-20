' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI400H : セット品マスタメンテ
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI400ハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI400H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' フォームを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI400F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI400V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI400G

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 共通Handlerクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlH As LMIControlH

    ''' <summary>
    '''検索件数格納用フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _CntSelect As String

    ''' <summary>
    '''検索結果格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _OutDs As DataSet

    ''' <summary>
    ''' PopUp画面表示フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PopupSkipFlg As Boolean

    ''' <summary>
    ''' チェックリスト格納フィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ChkList As ArrayList

    ''' <summary>
    ''' 印刷種別フラグ
    ''' </summary>
    ''' <remarks></remarks>
    Private _PrintFlg As String

    ''' <summary>
    '''表示用データテーブル格納
    ''' </summary>
    ''' <remarks></remarks>
    Private _DispDt As DataTable

    ''' <summary>
    '''表示用データテーブル格納(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Private _DitailDispDt As DataTable

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <param name="prm">パラメータ</param>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれる</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'フォームの作成
        Me._Frm = New LMI400F(Me)

        'Gamen共通クラスの設定
        Me._ControlG = New LMIControlG(Me._Frm)

        'Validate共通クラスの設定
        Me._ControlV = New LMIControlV(Me, DirectCast(Me._Frm, Form), Me._ControlG)

        'Handler共通クラスの設定
        Me._ControlH = New LMIControlH(MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI400V(Me, Me._Frm, Me._ControlV, Me._ControlG)

        'Gamenクラスの設定
        Me._G = New LMI400G(Me, Me._Frm, Me._ControlG, Me._V)

        'フォームの初期化
        MyBase.InitControl(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        ''コントロール個別設定
        'Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'スプレッドの設定(Gクラスのｽﾌﾟﾚｯﾄﾞの初期設定を呼ぶ)
        Call Me._G.InitDetailSpread()

        'メッセージの表示
        Call MyBase.ShowMessage(Me._Frm, "G007")

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'フォームの表示
        Me._Frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 新規処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ShinkiEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.SHINKI) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NEW_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '画面項目クリア処理/画面初期値設定
        Call Me._G.SetControl()

        'メッセージの表示
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 編集処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub EditDataEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.HENSHU) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        'DataSet設定(排他チェック)
        Dim rtnds As DataSet = Me.SetDataSetHaitaChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Call MyBase.CallWSA("LMI400BLF", "EditChk", rtnds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Sub
        End If

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.EDIT, RecordStatus.NOMAL_REC)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '明細部へ値の設定
        Call Me._G.SetSprDetail(Me._DitailDispDt)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 削除/復活処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub DeleteDataEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行格納処理
        Me._ChkList = Me._V.GetCheckList(LMI400C.EventShubetsu.SAKUJO_HUKKATU)

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI400C.EventShubetsu.SAKUJO_HUKKATU, Me._ChkList) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '処理続行メッセージ表示
        If Me.ConfirmMsg(LMI400C.EventShubetsu.SAKUJO_HUKKATU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'DataSet設定
        Dim rtnDs As DataSet = Me.SetDatasetDelData()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteData")

        '更新処理
        rtnDs = MyBase.CallWSA("LMI400BLF", "DeleteData", rtnDs)

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteData")

        'メッセージ表示
        Call Me.SetCompleteMessage(LMI400C.EventShubetsu.SAKUJO_HUKKATU)

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        '画面項目全クリア
        Call Me._G.ClearControl()

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

    End Sub

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SelectListEvent()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI400C.EventShubetsu.KENSAKU) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = True Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm) '終了処理　
                MyBase.ShowMessage(Me._Frm, "G003")
                Exit Sub
            End If
        End If

        'DataSet設定
        Dim rtnDs As DataSet = Me.SetDataSetInData()


        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectListData")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "SelectListData", rtnDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectListData")

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        If rtnDs IsNot Nothing _
        AndAlso rtnDs.Tables(LMI400C.TABLE_NM_OUT).Rows.Count > 0 Then
            '検索成功時共通処理を行う
            Call Me.SuccessSelect(rtnDs)

        ElseIf rtnDs IsNot Nothing Then
            '取得件数が0件の場合
            Call Me.FailureSelect(rtnDs)
        End If

    End Sub

    ''' <summary>
    ''' 保存処理
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SaveEvent() As Boolean

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI400C.EventShubetsu.HOZON) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        Dim rtnds As DataSet
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        '存在チェック(商品)
        rtnds = Me.SetDataSetGoodsChk()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "ExistChk")

        '==========================
        'WSAクラス呼出
        '==========================
        Call MyBase.CallWSA(blf, "ExistChk", rtnds)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "ExistChk")

        'メッセージコードの判定
        If MyBase.IsMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
            Call Me._ControlH.EndAction(Me._Frm) '終了処理
            Exit Function
        End If

        'DataSetクリア
        rtnds.Clear()

        '更新の場合のみ排他チェック
        If Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) _
           OrElse Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.DELETE_REC) Then

            '排他チェック
            rtnds = Me.SetDataSetHaitaChk()

            'ログ出力
            MyBase.Logger.StartLog(MyBase.GetType.Name, "EditChk")

            '==========================
            'WSAクラス呼出
            '==========================
            Call MyBase.CallWSA(blf, "EditChk", rtnds)

            'ログ出力
            MyBase.Logger.EndLog(MyBase.GetType.Name, "EditChk")

            'メッセージコードの判定
            If MyBase.IsMessageExist() = True Then
                MyBase.ShowMessage(Me._Frm) 'エラーメッセージ表示
                Call Me._ControlH.EndAction(Me._Frm) '終了処理
                Exit Function
            End If

            'DataSetクリア
            rtnds.Clear()

        End If

        'DataSet設定
        rtnds = Me.SetDataSetSave()

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveData")

        '保存処理
        If Me._Frm.lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
            '編集登録
            rtnds = MyBase.CallWSA(blf, "DelInsData", rtnds)
        Else
            '新規登録
            rtnds = MyBase.CallWSA(blf, "InsertData", rtnds)
        End If

        'メッセージコードの判定
        If MyBase.IsErrorMessageExist() = True Then
            MyBase.ShowMessage(Me._Frm)
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Return False
        End If

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveData")

        '終了処理　
        Call Me._ControlH.EndAction(Me._Frm)

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '画面項目全クリア
        Call Me._G.ClearControl()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージ表示
        Call Me.SetCompleteMessage(LMI400C.EventShubetsu.HOZON)

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub CloseFormEvent(ByVal e As FormClosingEventArgs)

        'ディスプレイモードが編集の場合処理終了
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) = False Then
            Exit Sub
        End If

        'メッセージ表示
        Select Case MyBase.ShowMessage(Me._Frm, "W002")

            Case MsgBoxResult.Yes  '「はい」押下時

                If Me.SaveEvent() = False Then

                    e.Cancel = True

                End If

            Case MsgBoxResult.Cancel                  '「キャンセル」押下時

                e.Cancel = True

        End Select

    End Sub

    ''' <summary>
    '''  Spreadダブルクリック処理
    ''' </summary>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub SprCellSelect(ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        'メッセージ表示(編集モードの場合確認メッセージを表示する)        '
        If Me._Frm.lblSituation.DispMode.Equals(DispMode.EDIT) Then
            If MyBase.ShowMessage(Me._Frm, "C002") <> MsgBoxResult.Ok Then
                Call Me._ControlH.EndAction(Me._Frm)  '終了処理
                Exit Sub
            End If
        End If

        Call Me.RowSelection(e.Row)

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeaveイベント
    ''' </summary>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Private Sub SprFindLeaveCell(ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        '編集モードの場合、処理終了
        If DispMode.EDIT.Equals(Me._Frm.lblSituation.DispMode) = True Then
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

        Call Me.RowSelection(rowNo)

    End Sub

    ''' <summary>
    ''' 行追加処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowAdd()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.ROW_ADD) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI400C.EventShubetsu.ROW_ADD) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        '商品マスタ参照
        Dim ds As DataSet = Me.ShowGoodsPopup()


        If ds.Tables("LMZ020OUT").Rows.Count = 0 Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            MyBase.ShowMessage(Me._Frm, "G003")
            Exit Sub
        End If

        '行追加処理を行う
        Call Me._G.AddRow(ds)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' 行削除処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub RowDel()

        '処理開始アクション
        Me._ControlH.StartAction(Me._Frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.ROW_DEL) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行格納処理
        Me._ChkList = Me._V.GetCheckList(LMI400C.EventShubetsu.ROW_DEL)

        '単項目/関連チェック
        If Me._V.IsInputChk(LMI400C.EventShubetsu.ROW_DEL, Me._ChkList) = False Then
            Call Me._ControlH.EndAction(Me._Frm) '終了処理　
            Exit Sub
        End If

        'チェック行削除
        Call Me._G.DelateDtl(Me._ChkList)

        '終了処理
        Call Me._ControlH.EndAction(Me._Frm)

        'メッセージエリアの設定
        Call Me._V.SetBaseMsg()

    End Sub

#End Region

#Region "内部メソッド"

    ''' <summary>
    ''' 検索成功時共通処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SuccessSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        '取得データをSPREADに表示
        Call Me.GetSprSearchDisplayData()
        Call Me._G.SetSprSearch(Me._DispDt)

        '取得件数設定
        Me._CntSelect = Me._DispDt.Rows.Count.ToString()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージ表示
        Call Me.SetCompleteMessage(LMI400C.EventShubetsu.KENSAKU)


    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理(検索結果が0件の場合))
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub FailureSelect(ByVal ds As DataSet)

        '変数に取得結果を格納
        Me._OutDs = ds

        'SPREAD(表示行)初期化
        Me._Frm.sprDetail.CrearSpread()

        'ディスプレイモードとステータス設定
        Call Me._G.SetModeAndStatus(DispMode.INIT, RecordStatus.INIT)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '画面項目全クリア
        Call Me._G.ClearControl()

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージエリアの設定
        MyBase.ShowMessage(Me._Frm, "G001")

    End Sub

    ''' <summary>
    ''' 行選択処理
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Private Sub RowSelection(ByVal rowNo As Integer)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI400C.EventShubetsu.DOUBLE_CLICK) = False Then
            Call Me._ControlH.EndAction(Me._Frm)  '終了処理
            Exit Sub
        End If

        Dim recstatus As String = String.Empty

        '選択行の削除フラグからレコードステータスを設定
        If LMConst.FLG.OFF.Equals(Me._ControlV.GetCellValue(Me._Frm.sprSearch.ActiveSheet.Cells(rowNo, LMI400G.sprSearchDef.SYS_DEL_FLG.ColNo))) = True Then
            recstatus = LMConst.FLG.OFF
        Else
            recstatus = LMConst.FLG.ON
        End If

        'モード・ステータスの設定
        Call Me._G.SetModeAndStatus(DispMode.VIEW, recstatus)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        '画面の入力項目
        Call Me._G.SetControlsStatus()

        '明細部へデータを移動
        Call Me._G.SetControlSpreadData(rowNo)
        Call Me.GetSprDetailDisplayData(rowNo)
        Call Me._G.SetSprDetail(Me._DitailDispDt)

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default()

        'メッセージ表示
        Call Me._V.SetBaseMsg()

    End Sub

    ''' <summary>
    ''' Spread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSprSearchDisplayData()

        With Me._Frm

            'DataTableの初期化
            Me._DispDt = New DataTable
            Dim oyaCd As String = String.Empty
            Dim dt As DataTable = Me._OutDs.Tables(LMI400C.TABLE_NM_OUT)
            Dim selectDr As DataRow() = dt.Select()
            Dim setDs As DataSet = New LMI400DS()
            Me._DispDt = setDs.Tables(LMI400C.TABLE_NM_OUT)

            For i As Integer = 0 To selectDr.Length - 1

                If selectDr(i).Item("OYA_CD").ToString().Equals(oyaCd) = False Then

                    Me._DispDt.ImportRow(selectDr(i))
                    oyaCd = selectDr(i).Item("OYA_CD").ToString()

                End If

            Next

        End With

    End Sub

    ''' <summary>
    ''' 明細Spread表示用にDataTaleを編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub GetSprDetailDisplayData(ByVal rowNo As Integer)

        With Me._Frm

            'DataTableの初期化
            Me._DitailDispDt = New DataTable
            Dim filter As String = String.Empty
            Dim orderBy As String = String.Empty
            Dim selectDr As DataRow() = Nothing
            Dim setDs As DataSet = New LMI400DS()
            Me._DitailDispDt = setDs.Tables(LMI400C.TABLE_NM_OUT)

            '取得条件設定
            filter = String.Concat(filter, "NRS_BR_CD = '", .cmbNrsBr.SelectedValue, "'" _
                                         , " AND OYA_CD = '", Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(rowNo, LMI400G.sprSearchDef.OYA_CD.ColNo)), "'")

            '並び順設定
            orderBy = "KO_CD"

            'データ取得
            Dim dt As DataTable = Me._OutDs.Tables(LMI400C.TABLE_NM_OUT)
            selectDr = dt.Select(filter, orderBy)

            For i As Integer = 0 To selectDr.Length - 1
                Me._DitailDispDt.ImportRow(selectDr(i))
            Next

        End With

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ConfirmMsg(ByVal eventShubetu As LMI400C.EventShubetsu) As Boolean

        Select Case eventShubetu
            Case LMI400C.EventShubetsu.SAKUJO_HUKKATU
                '処理続行メッセージ表示
                Dim msg As String = String.Empty

                Select Case Me._Frm.lblSituation.RecordStatus
                    Case RecordStatus.DELETE_REC
                        msg = "復活"
                    Case RecordStatus.NOMAL_REC
                        msg = "削除"
                End Select

                If MyBase.ShowMessage(Me._Frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
                    Call Me._V.SetBaseMsg() 'メッセージエリアの設定
                    Exit Function
                End If

            Case LMI400C.EventShubetsu.HOZON
                '確認メッセージ
                If MyBase.ShowMessage(Me._Frm, "W003") = MsgBoxResult.Cancel Then
                    Call Me._V.SetBaseMsg()
                    Return False
                End If

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 処理完了メッセージ
    ''' </summary>
    ''' <param name="eventShubetu">イベント種別</param>
    ''' <remarks></remarks>
    Private Sub SetCompleteMessage(ByVal eventShubetu As LMI400C.EventShubetsu)

        With Me._Frm

            Dim shoriMsg As String = String.Empty
            Dim comMsg As String = String.Empty

            Select Case eventShubetu
                Case LMI400C.EventShubetsu.KENSAKU
                    shoriMsg = "G008"
                    comMsg = Convert.ToString(Me._CntSelect)

                Case LMI400C.EventShubetsu.SAKUJO_HUKKATU
                    shoriMsg = "G010"

                Case LMI400C.EventShubetsu.HOZON
                    shoriMsg = "G015"

            End Select

            If eventShubetu.Equals(LMI400C.EventShubetsu.KENSAKU) = True Then
                MyBase.ShowMessage(Me._Frm, shoriMsg, New String() {comMsg})

            Else
                MyBase.ShowMessage(Me._Frm, shoriMsg, New String() {""})

            End If

        End With

    End Sub

#Region "PopUp"

    ''' <summary>
    ''' 商品マスタマスタ参照POP起動
    ''' </summary>
    ''' <remarks></remarks>
    Private Function ShowGoodsPopup() As DataSet

        Dim ds As DataSet = New LMZ020DS()
        Dim dt As DataTable = ds.Tables(LMZ020C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()
        Dim prm As LMFormData = New LMFormData()

        With dr

            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            .Item("NRS_BR_CD") = LMUserInfoManager.GetNrsBrCd()
            .Item("CUST_CD_L") = "00182"
            .Item("CUST_CD_M") = "00"
            .Item("DEFAULT_SEARCH_FLG") = LMConst.FLG.ON
            dt.Rows.Add(dr)

        End With

        'Pop起動
        prm = Me._ControlH.FormShow(ds, "LMZ020", "", Me._PopupSkipFlg)

        Return prm.ParamDataSet

    End Function

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定(検索処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetInData() As DataSet

        Dim ds As DataSet = New LMI400DS
        Dim dt As DataTable = ds.Tables(LMI400C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With Me._Frm

            dr.Item("NRS_BR_CD") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI400G.sprSearchDef.NRS_BR_NM.ColNo))
            dr.Item("OYA_CD") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI400G.sprSearchDef.OYA_CD.ColNo))
            dr.Item("OYA_NM") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI400G.sprSearchDef.OYA_NM.ColNo))
            dr.Item("SYS_DEL_FLG") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(0, LMI400G.sprSearchDef.STATUS.ColNo))

            ds.Tables(LMI400C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(排他処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetHaitaChk() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI400DS
            Dim dt As DataTable = ds.Tables(LMI400C.TABLE_NM_IN)
            Dim dr As DataRow = Nothing

            dr = dt.NewRow()
            dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
            dr.Item("OYA_CD") = .txtOyaCd.TextValue
            dr.Item("SYS_UPD_DATE") = DateFormatUtility.DeleteSlash(.lblUpdDate.TextValue)
            dr.Item("SYS_UPD_TIME") = .lblUpdTime.TextValue

            ds.Tables(LMI400C.TABLE_NM_IN).Rows.Add(dr)

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(削除・復活処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDatasetDelData() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI400DS
            Dim dt As DataTable = ds.Tables(LMI400C.TABLE_NM_IN)
            Dim dr As DataRow = Nothing
            Dim max As Integer = Me._ChkList.Count - 1
            Dim arr As Integer = 0

            '削除/復活の切り替えを行う
            Dim delflg As String = String.Empty

            Select Case .lblSituation.RecordStatus

                Case RecordStatus.NOMAL_REC
                    delflg = LMConst.FLG.ON

                Case RecordStatus.DELETE_REC
                    delflg = LMConst.FLG.OFF

            End Select

            For i As Integer = 0 To max

                arr = Convert.ToInt32(Me._ChkList(i))
                dr = dt.NewRow()
                dr.Item("NRS_BR_CD") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(arr, LMI400G.sprSearchDef.NRS_BR_CD.ColNo))
                dr.Item("OYA_CD") = Me._ControlG.GetCellValue(.sprSearch.ActiveSheet.Cells(arr, LMI400G.sprSearchDef.OYA_CD.ColNo))
                dr.Item("SYS_DEL_FLG") = delflg

                ds.Tables(LMI400C.TABLE_NM_IN).Rows.Add(dr)

            Next

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(存在チェック)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetGoodsChk() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI400DS
            Dim dt As DataTable = ds.Tables(LMI400C.TABLE_NM_IN)
            Dim dr As DataRow = Nothing
            Dim max As Integer = .sprDetail.ActiveSheet.RowCount - 1

            For i As Integer = 0 To max

                dr = dt.NewRow()
                dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
                dr.Item("KO_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI400G.sprDetailDef.KO_CD.ColNo))

                ds.Tables(LMI400C.TABLE_NM_IN).Rows.Add(dr)

            Next

            Return ds

        End With

    End Function

    ''' <summary>
    ''' データセット設定(保存処理)
    ''' </summary>
    ''' <remarks></remarks>
    Private Function SetDataSetSave() As DataSet

        With Me._Frm

            Dim ds As DataSet = New LMI400DS
            Dim dt As DataTable = ds.Tables(LMI400C.TABLE_NM_IN)
            Dim dr As DataRow = Nothing
            Dim max As Integer = .sprDetail.ActiveSheet.RowCount - 1

            For i As Integer = 0 To max

                dr = dt.NewRow()
                dr.Item("NRS_BR_CD") = .cmbNrsBr.SelectedValue
                dr.Item("OYA_CD") = .txtOyaCd.TextValue
                dr.Item("OYA_NM") = .txtOyaNm.TextValue
                dr.Item("KO_CD") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI400G.sprDetailDef.KO_CD.ColNo))
                dr.Item("SET_KOSU") = Me._ControlG.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMI400G.sprDetailDef.KOSU.ColNo))

                ds.Tables(LMI400C.TABLE_NM_IN).Rows.Add(dr)
            Next

            Return ds

        End With

    End Function

#End Region

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し(新規)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByRef frm As LMI400F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "NewDataEvent")

        Me.ShinkiEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "NewDataEvent")

    End Sub

    ''' <summary>
    ''' F2押下時処理呼び出し(編集)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey2Press(ByRef frm As LMI400F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "EditDataEvent")

        Me.EditDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "EditDataEvent")

    End Sub

    ''' <summary>
    ''' F4押下時処理呼び出し(削除・復活)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey4Press(ByRef frm As LMI400F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DeleteDataEvent")

        '削除・復活処理
        Me.DeleteDataEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DeleteDataEvent")

    End Sub

    ''' <summary>
    ''' F9押下時処理呼び出し(検索処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey9Press(ByRef frm As LMI400F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectCurrencyEvent")

        '検索処理
        Me.SelectListEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectCurrencyEvent")

    End Sub

    ''' <summary>
    ''' F11押下時処理呼び出し(保存処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey11Press(ByRef frm As LMI400F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "SaveEvent")

        '保存処理
        Me.SaveEvent()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "SaveEvent")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI400F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "CloseForm")

        frm.Close()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "CloseForm")

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI400F, ByVal e As FormClosingEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        '終了処理  
        Call Me.CloseFormEvent(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓　========================

    ''' <summary>
    ''' SPREADダブルクリック時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub sprCellDoubleClick(ByRef frm As LMI400F, ByVal e As FarPoint.Win.Spread.CellClickEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "RowSelection")

        Me.SprCellSelect(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "RowSelection")

    End Sub

    ''' <summary>
    ''' 検索SpreadのLeave
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="e">イベント詳細情報</param>
    ''' <remarks></remarks>
    Friend Sub sprSearchLeaveCell(ByVal frm As LMI400F, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "sprDetailLeaveCell")

        Call Me.SprFindLeaveCell(e)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "sprDetailLeaveCell")

    End Sub

    ''' <summary>
    ''' 行追加ボタン押下時のイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub AddClick(ByVal frm As LMI400F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "AddClick")

        '行追加処理
        Call Me.RowAdd()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "AddClick")

    End Sub

    ''' <summary>
    ''' 行削除ボタン押下イベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">イベント詳細</param>
    ''' <remarks></remarks>
    Friend Sub DelClick(ByVal frm As LMI400F, ByVal e As System.EventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "DelClick")

        '行削除処理
        Call Me.RowDel()

        MyBase.Logger.EndLog(MyBase.GetType.Name, "DelClick")

    End Sub

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#End Region

End Class