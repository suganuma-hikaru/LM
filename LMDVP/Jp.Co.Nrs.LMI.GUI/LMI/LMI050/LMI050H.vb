' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI050  : EDI月末在庫実績送信ﾃﾞｰﾀ作成
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMI050ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI050H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI050V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI050G

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMIControlG

    ''' <summary>
    ''' H共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConH As LMIControlH

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

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
        Dim frm As LMI050F = New LMI050F(Me)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '画面共通クラスの設定
        Dim sForm As Form = DirectCast(frm, Form)
        Me._LMIConG = New LMIControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMIconV = New LMIControlV(Me, sForm, Me._LMIConG)

        'Hnadler共通クラスの設定
        Me._LMIConH = New LMIControlH(MyBase.GetPGID())

        'Gamenクラスの設定
        Me._G = New LMI050G(Me, frm, Me._LMIConG)

        'Validateクラスの設定
        Me._V = New LMI050V(Me, frm, Me._LMIconV, Me._G)

        'フォームの初期化
        Call MyBase.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey()

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'メッセージの表示
        Call MyBase.ShowMessage(frm, "G006")

        '画面の入力項目の制御
        Call Me._G.SetControlsStatus()

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
    Friend Sub ActionControl(ByVal eventShubetsu As LMI050C.EventShubetsu, ByVal frm As LMI050F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()
        Dim rtnDs As DataSet = Nothing

        '権限チェック
        If _V.IsAuthorityChk(eventShubetsu) = False Then
            MyBase.ShowMessage(frm, "E016")
            Exit Sub
        End If

        Select Case eventShubetsu

            Case LMI050C.EventShubetsu.JIKKO    '実行

                '処理開始アクション
                Me._LMIConH.StartAction(frm)

                '入力チェック
                If Me._V.IsSingleCheck(eventShubetsu) = False OrElse _
                   Me._V.IsKanrenCheck(eventShubetsu) = False Then
                    '処理終了アクション
                    Me._LMIConH.EndAction(frm)
                    Exit Sub
                End If

                '実行処理
                Call Me.JikkoMain(frm)

                '処理終了アクション
                Me._LMIConH.EndAction(frm)

            Case LMI050C.EventShubetsu.CHANGECUST     '荷主コンボ変更

                '荷主単位の画面コントロールの個別設定
                Call Me._G.SetControlCust(MyBase.GetSystemDateTime(0))

        End Select

    End Sub

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI050F) As Boolean

        Return True

    End Function

#End Region 'イベント定義(一覧)

#Region "イベント振分け"

    ''' <summary>
    ''' F1押下時処理呼び出し
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey1Press(ByVal frm As LMI050F, ByVal e As System.Windows.Forms.KeyEventArgs)

        MyBase.Logger.StartLog(MyBase.GetType.Name, "FunctionKey1Press")

        Me.ActionControl(LMI050C.EventShubetsu.JIKKO, frm)

        MyBase.Logger.EndLog(MyBase.GetType.Name, "FunctionKey1Press")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByVal frm As LMI050F, ByVal e As System.Windows.Forms.KeyEventArgs)

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
    Friend Sub ClosingForm(ByVal frm As LMI050F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(MyBase.GetType.Name, "ClosingForm")

        Call Me.CloseForm(frm)

        Logger.EndLog(MyBase.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================
    ''' <summary>
    ''' コンボボックスチェンジイベント
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Friend Sub ChangeCustCombo(ByRef frm As LMI050F, ByVal e As System.EventArgs)

        Logger.StartLog(Me.GetType.Name, "ChangeCustCombo")

        Me.ActionControl(LMI050C.EventShubetsu.CHANGECUST, frm)

        Logger.EndLog(Me.GetType.Name, "ChangeCustCombo")

    End Sub
    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region 'イベント振分け

#Region "ユーティリティ"

    ''' <summary>
    ''' 実行処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub JikkoMain(ByVal frm As LMI050F)

        Select Case Convert.ToString(frm.cmbCust.SelectedValue) '区分マスタE029のKBN_CDが設定されている
            Case "01" '篠崎運送

                Dim rtDs As DataSet = Nothing

                '篠崎運送専用の月末在庫テーブルからデータ取得処理
                rtDs = Me.SelectMonthlySnz(frm)

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    Exit Sub
                End If

                '継続処理判定
                Dim ShoriFlg As String = Me.WorningSnz(frm, rtDs)

                Select Case ShoriFlg
                    Case "01"

                    Case "02"
                        '篠崎運送専用の月末在庫テーブル作成処理
                        rtDs = Me.MakeDataSNZ(frm)

                        'エラー時はメッセージを表示して終了
                        If MyBase.IsMessageExist() = True Then
                            MyBase.ShowMessage(frm)
                            Exit Sub
                        End If

                    Case "03"
                        Exit Sub
                End Select

                'CSV出力データ作成処理呼び出し
                Call Me.ShowCsvLMI880(frm)

                'エラー時はメッセージを表示して終了
                If MyBase.IsMessageExist() = True Then
                    MyBase.ShowMessage(frm)
                    Exit Sub
                End If

                'メッセージエリアの設定
                MyBase.ShowMessage(frm, "G002", New String() {"実行処理", ""})

            Case Else

        End Select

    End Sub

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブルからデータ取得処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SelectMonthlySnz(ByVal frm As LMI050F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI050DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "SelectMonthlySnz")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI050BLF", "SelectMonthlySnz", rtDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "SelectMonthlySnz")

        Return rtnDs

    End Function

    ''' <summary>
    ''' 篠崎運送専用の月末在庫テーブル作成処理
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function MakeDataSNZ(ByVal frm As LMI050F) As DataSet

        'DataSet設定
        Dim rtDs As DataSet = New LMI050DS()

        'InDataSetの場合
        Call Me.SetInData(frm, rtDs)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, "MakeDataSNZ")

        '==========================
        'WSAクラス呼出
        '==========================
        Dim rtnDs As DataSet = MyBase.CallWSA("LMI050BLF", "MakeDataSNZ", rtDs)

        'ログ出力
        MyBase.Logger.EndLog(MyBase.GetType.Name, "MakeDataSNZ")

        Return rtDs

    End Function

    ''' <summary>
    ''' 篠崎運送の継続処理判定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function WorningSnz(ByVal frm As LMI050F, ByVal ds As DataSet) As String

        Dim rtnFlg As String = String.Empty

        If ds.Tables(LMI050C.TABLE_NM_OUT_SNZ).Rows.Count = 0 Then
            'データがない場合は必ず作成する
            rtnFlg = "02"
            Return rtnFlg
        End If

        Dim dr() As DataRow = Nothing

        '実績日付で検索(送信済み)
        dr = ds.Tables(LMI050C.TABLE_NM_OUT_SNZ).Select(String.Concat("JISSEKI_DATE = '", frm.imdDate.TextValue, "' AND ", _
                                                                      "SEND_FLG = '1'"))
        If dr.Length > 0 Then
            Select Case MyBase.ShowMessage(frm, "W213", New String() {String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, String.Empty, vbCrLf})
                Case MsgBoxResult.Yes  '「はい」押下時
                    rtnFlg = "01"
                    Return rtnFlg
                Case MsgBoxResult.No   '「いいえ」押下時
                    rtnFlg = "02"
                    Return rtnFlg
                Case Else              '「キャンセル」押下時
                    rtnFlg = "03"
                    Return rtnFlg
            End Select
        End If

        '実績日付で検索(未送信)
        dr = ds.Tables(LMI050C.TABLE_NM_OUT_SNZ).Select(String.Concat("JISSEKI_DATE = '", frm.imdDate.TextValue, "' AND ", _
                                                                      "SEND_FLG = '0'"))
        If dr.Length > 0 Then
            Select Case MyBase.ShowMessage(frm, "W214")
                Case MsgBoxResult.Ok   '「OK」押下時
                    rtnFlg = "02"
                    Return rtnFlg
                Case Else              '「キャンセル」押下時
                    rtnFlg = "03"
                    Return rtnFlg
            End Select
        End If

        Return rtnFlg

    End Function

    ''' <summary>
    ''' CSV作成(LMI880)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub ShowCsvLMI880(ByVal frm As LMI050F)

        'パラメータクラス生成
        Dim prm As LMFormData = New LMFormData()

        'DataSet設定
        Dim prmDs As DataSet = Nothing
        Dim row As DataRow = Nothing

        prmDs = New LMI880DS
        row = prmDs.Tables(LMI880C.TABLE_NM_IN).NewRow
        row("NRS_BR_CD") = frm.cmbEigyo.SelectedValue.ToString
        row("JISSEKI_DATE") = frm.imdDate.TextValue

        prmDs.Tables(LMI880C.TABLE_NM_IN).Rows.Add(row)
        prm.ParamDataSet = prmDs

        'CSV作成処理呼出
        LMFormNavigate.NextFormNavigate(Me, "LMI880", prm)

        If prm.ReturnFlg = False Then
            'メッセージエリアの設定
            MyBase.SetMessage("E501")
            Exit Sub
        End If

    End Sub

#End Region

#Region "DataSet設定"

    ''' <summary>
    ''' データセット設定
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Sub SetInData(ByVal frm As LMI050F, ByRef rtDs As DataSet)

        Dim dr As DataRow = rtDs.Tables(LMI050C.TABLE_NM_IN).NewRow()
        Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'E029' AND ", _
                                                                                                        "KBN_CD = '", frm.cmbCust.SelectedValue, "'"))

        '検索条件　単項目部
        dr("NRS_BR_CD") = frm.cmbEigyo.SelectedValue
        dr("JISSEKI_DATE") = frm.imdDate.TextValue
        If kbnDr.Length > 0 Then
            dr("CUST_CD_L") = kbnDr(0).Item("KBN_NM2").ToString
            dr("CUST_CD_M") = kbnDr(0).Item("KBN_NM3").ToString
        End If

        '検索条件をデータセットに設定
        rtDs.Tables(LMI050C.TABLE_NM_IN).Rows.Add(dr)

    End Sub

#End Region

#End Region 'Method

End Class
