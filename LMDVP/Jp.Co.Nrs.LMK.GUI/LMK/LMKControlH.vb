' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK         : 請求サブシステム
'  プログラムID     :  LMKControlH : 請求サブシステム編集画面 共通処理
'  作  成  者       :  [笈川]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.DSL
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Base.GUI

''' <summary>
''' LMDControlハンドラクラス
''' </summary>
''' <remarks></remarks>
Public Class LMKControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 共通クラス(V)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMKControlV

    ''' <summary>
    ''' 共通クラス(G)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMKControlG

    ''' <summary>
    ''' PGID
    ''' </summary>
    ''' <remarks></remarks>
    Private _Pgid As String

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByRef frm As Form, ByVal pgid As String, ByVal v As LMKControlV, ByVal g As LMKControlG)

        Me.SetPGID(pgid)
        Me._ControlV = v
        Me._ControlG = g

    End Sub

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub New(ByVal pgid As String)

        Me.SetPGID(pgid)

    End Sub

#End Region

    ''' <summary>
    ''' 確認メッセージの表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>OKの場合:True　Cancelの場合:False</returns>
    ''' <remarks></remarks>
    Friend Function SetMessageC001(ByVal frm As Form, ByVal msg As String) As Boolean

        '確認メッセージ表示
        If MyBase.ShowMessage(frm, "C001", New String() {msg}) = MsgBoxResult.Cancel Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 処理終了メッセージを表示
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <remarks></remarks>
    Friend Sub SetMessageG002(ByVal frm As Form, ByVal msg1 As String, ByVal msg2 As String)

        MyBase.ShowMessage(frm, "G002", New String() {msg1, msg2})

    End Sub

    ''' <summary>
    ''' 処理開始アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub StartAction(ByVal frm As Form)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor()

        'メッセージのクリア
        MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form)

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
    ''' <param name="BLF">BLFファイル名</param>
    ''' <param name="actionId">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <param name="mc">ワーニング件数の閾値</param>
    ''' <param name="rc">表示最大件数の閾値</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <returns2>取得エラー時=Nothing。取得成功時=rtnDSを設定。取得0件の時もrtnDSを設定しているのは、呼び元画面にてSpreadクリアの判定に使用するため。</returns2>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As Form _
                                  , ByVal blf As String _
                                  , ByVal actionId As String _
                                  , ByVal rtDs As DataSet _
                                  , ByVal rc As Integer _
                                  , Optional ByVal mc As Integer = -1 _
                                  ) As DataSet

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)

        Dim rtnDs As DataSet = MyBase.CallWSA(blf, actionId, rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist() = True Then
            If MyBase.IsWarningMessageExist() = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MyBase.ShowMessage(frm) = MsgBoxResult.Ok Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(blf, actionId, rtDs)

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                    '検索成功時
                    Return rtnDs

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Call Me.FailureSelect(frm)
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

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If Me._ControlV.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' スプレッド明細行の全量(対象のColum)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetSpredList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max

                list.Add(Me._ControlV.GetCellValue(.Cells(i, defNo)))

            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' 別PG起動処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="id">画面ID</param>
    ''' <param name="recType">レコードタイプ 初期値 = ""</param>
    ''' <returns>パラメータクラス</returns>
    ''' <remarks></remarks>
    Friend Function FormShow(ByVal ds As DataSet, ByVal id As String, Optional ByVal recType As String = "") As LMFormData

        'パラメータ設定
        Dim prm As LMFormData = New LMFormData()
        prm.ParamDataSet = ds
        prm.RecStatus = recType

        '画面起動
        LMFormNavigate.NextFormNavigate(Me, id, prm)

        Return prm

    End Function

    ''' <summary>
    ''' 勘定科目コード設定
    ''' </summary>
    ''' <param name="busyoCd">部署コード</param>
    ''' <param name="keiriKb">経理科目コード区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetKanjoKmkCd(ByVal busyoCd As String, ByVal keiriKb As String) As String

        Dim kanjoKmkCd As String = Me.GetKanjoKmkCd(busyoCd, keiriKb, LMKControlC.GetKanjoKmkInfo.KANJO_KMK_CD)
        Dim keiriBumonCd As String = Me.GetKanjoKmkCd(busyoCd, keiriKb, LMKControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD)

        Return Me._ControlG.EditConcatData(keiriBumonCd, kanjoKmkCd, ".")

    End Function

    ''' <summary>
    ''' 勘定科目コード取得
    ''' </summary>
    ''' <param name="busyoCd">部署コード</param>
    ''' <param name="keiriKb">経理科目コード区分</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetKanjoKmkCd(ByVal busyoCd As String, ByVal keiriKb As String, ByVal rtnInfo As LMKControlC.GetKanjoKmkInfo) As String

        Dim rtnString As String = String.Empty

        If String.IsNullOrEmpty(busyoCd) _
        OrElse String.IsNullOrEmpty(keiriKb) Then
            Return rtnString
        End If

        Dim filter As String = String.Empty
        '区分マスタを検索し、取得結果が0件の場合、エラー
        filter = String.Empty
        filter = String.Concat(filter, "KBN_GROUP_CD = '", LMKbnConst.KBN_B006, "'")
        filter = String.Concat(filter, " AND KBN_NM4 = '", busyoCd, "'")
        filter = String.Concat(filter, " AND KBN_NM1 = '", keiriKb, "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")

        Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)

        If kbnDr.Length = 0 Then
            Return rtnString
        End If

        Select Case rtnInfo
            Case LMKControlC.GetKanjoKmkInfo.KANJO_KMK_CD
                '勘定科目コードを返却
                rtnString = kbnDr(0).Item("KBN_NM3").ToString()
            Case LMKControlC.GetKanjoKmkInfo.KEIRI_BUMON_CD
                '経理部門コードを返却
                rtnString = kbnDr(0).Item("KBN_NM6").ToString()
        End Select

        Return rtnString

    End Function

    ''' <summary>
    ''' 四捨五入処理
    ''' </summary>
    ''' <param name="amt">四捨五入する値</param>
    ''' <param name="pos">四捨五入する際の小数点以下有効桁数(設定例…整数のみ有効の場合：0、小数点第1位まで有効の場合：1、など)</param>
    ''' <returns>四捨五入した結果</returns>
    ''' <remarks></remarks>
    Friend Function ToHalfAdjust(ByVal amt As Decimal, ByVal pos As Integer) As Decimal

        ' 後続の(整数で計算⇒小数に戻す)処理で使用するため、10の乗数を取得
        Dim dCoef As Decimal = Convert.ToDecimal(System.Math.Pow(10, pos))

        Dim i As Decimal = 0

        If amt > 0 Then
            i = Convert.ToDecimal(System.Math.Floor((amt * dCoef) + 0.5) / dCoef)
        Else
            i = Convert.ToDecimal(System.Math.Ceiling((amt * dCoef) - 0.5) / dCoef)
        End If

        Return i

    End Function

End Class
