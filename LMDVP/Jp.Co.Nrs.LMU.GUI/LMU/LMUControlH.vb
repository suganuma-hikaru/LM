' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB          : 
'  プログラムID     :  LMMControlH  : LMM編集画面 共通処理
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports System
Imports System.Reflection
Imports FarPoint.Win.Spread

''' <summary>
''' LMMControlハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2010/03/01 金
''' </histry>
Public Class LMUControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 共通クラス(V)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

    ''' <summary>
    ''' 共通クラス(G)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMMControlG

    ''' <summary>
    ''' PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _Pgid As String

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pgid As String, ByVal v As LMMControlV, ByVal g As LMMControlG)

        MyBase.SetPGID(pgid)
        Me._ControlV = v
        Me._ControlG = g

    End Sub


    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByVal frm As Form, ByVal v As LMMControlV, ByVal g As LMMControlG)

        Me._ControlV = v
        Me._ControlG = g

    End Sub
#End Region

#Region "Method"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As LMForm)

        '画面全ロック
        MyBase.LockedControls(frm)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Win.Interface.ILMForm))


    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As LMForm)

        '画面解除
        MyBase.UnLockedControls(frm)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub FailureSelect(ByVal frm As LMForm)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' WSAクラス呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="blf">BLFファイル名</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As LMForm _
                                  , ByVal blf As String _
                                  , ByVal actionId As String _
                                  , ByVal rtDs As DataSet _
                                  , ByVal rc As Integer _
                                  , Optional ByRef cnt As String = Nothing _
                                  , Optional ByVal mc As Integer = -1 _
                                  ) As DataSet

        '強制実行フラグの設定
        MyBase.SetForceOparation(False)

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)


        Dim rtnDs As DataSet = MyBase.CallWSA(blf, actionId, rtDs)
        cnt = MyBase.GetResultCount.ToString()
        'メッセージ判定
        If MyBase.IsMessageExist() = True Then

            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, actionId, rtDs)

                    '検索成功時
                    Return rtnDs

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Call Me.FailureSelect(frm)

                    Return Nothing

                End If

            Else      'Errorの場合

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時、共通処理を行う
                Call Me.FailureSelect(frm)

                Return rtnDs

            End If
        Else
            '検索成功時
            Return rtnDs

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' スプレッド明細行のチェックリスト(RowIndex)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim arr As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._ControlV.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    arr.Add(i)
                End If
            Next

            Return arr

        End With

    End Function

    ''' <summary>
    ''' フォームに検索した結果(Text)を取得
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="objNm">コントロール名</param>
    ''' <returns>LMImTextBox</returns>
    ''' <remarks></remarks>
    Friend Function GetTextControl(ByVal frm As Form, ByVal objNm As String) As Win.InputMan.LMImTextBox
        Return DirectCast(frm.Controls.Find(objNm, True)(0), Win.InputMan.LMImTextBox)
    End Function

    ''' <summary>
    ''' 別PG起動処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="id">画面ID</param>
    ''' <param name="recType">レコードタイプ 初期値 = ""</param>
    ''' <param name="skipFlg">画面表示フラグ 初期値 = False</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Friend Function FormShow(ByVal ds As DataSet, ByVal id As String _
                             , Optional ByVal recType As String = "" _
                             , Optional ByVal skipFlg As Boolean = False) As LMFormData

        'パラメータ設定
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.RecStatus = recType
        prm.SkipFlg = skipFlg

        '画面起動
        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

#Region "ENTERイベント"

    ''' <summary>
    ''' 次コントロールにフォーカス移動
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="eventFlg">Enterボタンの場合、True</param>
    ''' <remarks></remarks>
    Friend Sub NextFocusedControl(ByVal frm As Form _
                                  , ByVal eventFlg As Boolean)

        'Enter以外の場合、処理終了
        If eventFlg = False Then
            Exit Sub
        End If

        'Spread上にコントロールがある場合、処理終了
        Dim sprFocusFlg As Boolean = False
        'フォーム内のSpreadを取得
        Dim arr As ArrayList = New ArrayList()
        arr = New ArrayList()
        Me._ControlG.GetTarget(Of Win.Spread.LMSpread)(arr, frm)
        For Each spr As Win.Spread.LMSpread In arr
            If frm.ActiveControl.Equals(spr) Then
                sprFocusFlg = True
            End If
        Next

        '次コントロールにフォーカス移動
        If sprFocusFlg = False Then
            frm.SelectNextControl(frm.ActiveControl, True, True, True, True)
        End If

    End Sub

    ''' <summary>
    ''' Enter押下イベントの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEnterEvent(ByVal frm As LMForm)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ENTER時にセルを右移動させる
        Call Me.SetSpreadEnterEvent(frm)

    End Sub

    ''' <summary>
    ''' Spread上でのEnter押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSpreadEnterEvent(ByVal frm As LMForm)

        'フォーム内のSpreadを取得
        Dim arr As ArrayList = New ArrayList()
        arr = New ArrayList()
        Me._ControlG.GetTarget(Of Win.Spread.LMSpread)(arr, frm)
        Dim im As New FarPoint.Win.Spread.InputMap

        For Each spr As Win.Spread.LMSpread In arr

            ' 非編集セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

            '編集中セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

        Next

    End Sub

#End Region

    ' ''' <summary>
    ' ''' 閾値取得処理
    ' ''' </summary>
    ' ''' <remarks></remarks>
    'Friend Function GetLimitCount(Optional ByVal kbnCd As String = LMMControlC.LIMIT_COUNT_KENSAKU_GAMEN) As Integer

    '    GetLimitCount = 0

    '    Dim filter As String = String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S054, "'")
    '    filter = String.Concat(filter, " AND KBN_CD = '", kbnCd, "'")
    '    filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
    '    Dim limitData As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
    '    Dim limitCount As Decimal = Convert.ToDecimal(limitData(0).Item("VALUE1").ToString)

    '    GetLimitCount = Convert.ToInt16(limitCount)

    'End Function

#End Region

End Class
