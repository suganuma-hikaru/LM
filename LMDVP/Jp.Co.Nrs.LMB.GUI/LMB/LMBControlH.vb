' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB          : 
'  プログラムID     :  LMBControlH  : LMB編集画面 共通処理
'  作  成  者       :  [金へする]
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
''' LMBControlハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2010/03/01 金
''' </histry>
Public Class LMBControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 共通クラス(V)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMBControlV

    ''' <summary>
    ''' 共通クラス(G)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMBControlG

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
    Friend Sub New(ByVal frm As Form, ByVal pgid As String, ByVal h As LM.Base.GUI.LMBaseGUIHandler)

        MyBase.SetPGID(pgid)
        Me._ControlV = New LMBControlV(h, frm)
        Me._ControlG = New LMBControlG(frm)

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

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
    ''' <param name="id">ガイダンスメッセージID</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form, Optional ByVal id As String = "G007")

        '画面解除
        MyBase.UnLockedControls(frm)

        'ガイダンスメッセージを表示
        Call Me.SetGMessage(frm, id)

        'Cursorを元に戻す
        Cursor.Current = Cursors.Default()

    End Sub

    ''' <summary>
    ''' 検索失敗時共通処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub FailureSelect(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <param name="cntSelect">検索結果件数</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ConfirmMsg(ByVal frm As Form, ByVal msg As String, ByVal cntSelect As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            'メッセージエリアの設定
            MyBase.ShowMessage(frm, "G016", New String() {cntSelect})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' WSAクラス呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="blf">BLFファイル名</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <param name="mc">ワーニング件数の閾値</param>
    ''' <param name="rc">表示最大件数の閾値</param>
    ''' <param name="readDBFLG">1 リード指定</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <returns2>取得エラー時=Nothing。取得成功時=rtnDSを設定。取得0件の時もrtnDSを設定しているのは、呼び元画面にてSpreadクリアの判定に使用するため。</returns2>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As Form _
                                  , ByVal blf As String _
                                  , ByVal actionId As String _
                                  , ByVal rtDs As DataSet _
                                  , ByVal rc As Integer _
                                  , Optional ByVal mc As Integer = -1 _
                                  , Optional ByVal readDBFLG As String = "") As DataSet

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)

        Dim rtnDs As DataSet = New DataSet
        If readDBFLG.Equals("1") = True Then
            'DBリードオンリー設定 ADD 2021/11/01
            rtnDs = MyBase.CallWSA(blf, actionId, rtDs, True)
        Else
            rtnDs = MyBase.CallWSA(blf, actionId, rtDs)
        End If

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    If readDBFLG.Equals("1") = True Then
                        'DBリードオンリー設定 
                        rtnDs = MyBase.CallWSA(blf, actionId, rtDs, True)
                    Else
                        rtnDs = MyBase.CallWSA(blf, actionId, rtDs)
                    End If

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                        '検索成功時
                        Return rtnDs

                    Else    '「いいえ」を選択
                        'メッセージエリアの設定
                        MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Call Me.FailureSelect(DirectCast(frm, Form))
                    Return Nothing

                End If

            Else

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
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Friend Function SetMessageC001(ByVal frm As LMB020F, ByVal msg As String) As Boolean

        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    '追加開始 2015.05.13 要望番号:2292
    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Friend Function SetMessageC001(ByVal frm As LMB040F, ByVal msg As String) As Boolean

        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    '追加終了 2015.05.13 要望番号:2292

    ''' <summary>
    ''' ガイダンスメッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="id">メッセージID</param>
    ''' <remarks></remarks>
    Private Sub SetGMessage(ByVal frm As Form, ByVal id As String)

        'メッセージ欄に値がある場合、スルー
        If String.IsNullOrEmpty(frm.Controls.Find("lblMsgAria", True)(0).Text) = False Then
            Exit Sub
        End If

        MyBase.ShowMessage(frm, id)

    End Sub

    '2011/08/25 岸 まとめ検証結果(画面共通)№3対応 追加
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


#End Region 'Method

End Class
