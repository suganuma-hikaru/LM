' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMQ          : 
'  プログラムID     :  LMQControlH  : LMQ データ抽出Excel作成
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread

''' <summary>
''' LMQControlハンドラクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 
''' </histry>
Public Class LMQControlH
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 共通クラス(V)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMQControlV

    ''' <summary>
    ''' 共通クラス(G)
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMQControlG

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
    ''' <param name="frm"></param>
    ''' <param name="pgid"></param>
    ''' <remarks></remarks>
    Public Sub New(ByVal frm As Form, ByVal pgid As String)

        MyBase.SetPGID(pgid)
        _ControlV = New LMQControlV(New LM.Base.GUI.LMBaseGUIHandler(), frm)
        _ControlG = New LMQControlG(frm)

    End Sub

#End Region 'Constructor

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
        MyBase.ClearMessageAria(DirectCast(frm, Jp.Co.Nrs.LM.GUI.Win.Interface.ILMForm))

    End Sub

    ''' <summary>
    ''' 終了アクション
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Friend Sub EndAction(ByVal frm As Form)

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
    Friend Sub FailureSelect(ByVal frm As Form)

        '画面解除
        MyBase.UnLockedControls(frm)

    End Sub

    ''' <summary>
    ''' 処理続行確認
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="msg">メッセージ置換文字列(処理名)</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ConfirmMsg(ByVal frm As Form, ByVal msg As String) As Boolean

        If MyBase.ShowMessage(frm, "C001", New String() {msg}).Equals(MsgBoxResult.Cancel) = True Then
            Return False
        End If

        Return True

    End Function


    ''' <summary>
    ''' WSAクラス呼出
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="bBLF">BLFファイル名</param>
    ''' <param name="methodName">メソッド名</param>
    ''' <param name="rtDs">データセット</param>
    ''' <returns>WSA呼出後のデータセット</returns>
    ''' <returns2>取得エラー時=Nothing。取得成功時=rtnDSを設定。取得0件の時もrtnDSを設定しているのは、呼び元画面にてSpreadクリアの判定に使用するため。</returns2>
    ''' <remarks></remarks>
    Friend Function CallWSAAction(ByVal frm As Form _
                                , ByVal bBLF As String _
                                , ByVal methodName As String _
                                , ByVal rtDs As DataSet _
                                , ByVal rc As Integer _
                                , Optional ByVal mc As Integer = -1 _
                                ) As DataSet

        '閾値の設定
        MyBase.SetLimitCount(rc)

        '表示最大件数の設定
        MyBase.SetMaxResultCount(mc)


        Dim rtnDs As DataSet = MyBase.CallWSA(bBLF, methodName, rtDs)

        'メッセージ判定
        If MyBase.IsMessageExist = True Then
            If MyBase.IsWarningMessageExist = True Then         'Warningの場合

                'メッセージを表示し、戻り値により処理を分ける
                If MsgBoxResult.Ok.Equals(MyBase.ShowMessage(frm)) = True Then '「はい」を選択

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(True)

                    'WSA呼出し
                    rtnDs = MyBase.CallWSA(bBLF, methodName, rtDs)

                    '強制実行フラグの設定
                    MyBase.SetForceOparation(False)

                    '検索成功時
                    Return rtnDs

                Else    '「いいえ」を選択
                    'メッセージエリアの設定
                    MyBase.ShowMessage(frm, "G007")

                    '検索失敗時、共通処理を行う
                    Me.FailureSelect(frm)
                    Return Nothing

                End If
            Else

                'メッセージエリアの設定
                MyBase.ShowMessage(frm)

                '検索失敗時、共通処理を行う
                Me.FailureSelect(frm)
                Return rtnDs

            End If
        Else
            '検索成功時
            Return rtnDs

        End If

        Return Nothing

    End Function

    ''' <summary>
    ''' INPUTパラメータチェック(Nullのﾁｪｯｸ)
    ''' </summary>
    ''' <param name="prmDs">データセット</param>
    ''' <param name="repStr">エラー時の置換文字</param>
    ''' <param name="chkObject"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function CheckParameter(ByVal prmDs As DataSet, ByVal repStr As String, ByVal chkObject As LMQControlC.ChkObject) As Boolean

        'NULLチェック
        If prmDs Is Nothing = True Then
            MyBase.SetMessage("S001", New String() {repStr})
            Return False
        End If

        'RECORD数チェック
        If prmDs.Tables(LMQControlC.TABLE_NM_OUT).Rows.Count < 1 Then
            MyBase.SetMessage("E024")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' エクセル出力処理
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <param name="pgid"></param>
    ''' <param name="fileNm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function ExcelPrint(ByVal ds As DataSet, ByVal pgid As String, ByVal fileNm As String) As Boolean

        Try
            'ExcelCreatorの生成
            Dim xls As LM.Utility.ExcelCreatorUtility8 = New LM.Utility.ExcelCreatorUtility8

            'Excel出力を開始します。
            If xls.PrintXls(pgid, ds.Tables(LMQControlC.TABLE_NM_OUT), pgid, fileNm) = False Then
                Return False
            End If

        Catch ex As Exception
            ' 例外が発生した時の処理(不適切なファイルを取込んだ場合)
            MyBase.SetMessage("S002")
            Return False
        End Try

        Return True

    End Function

End Class
