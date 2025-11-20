' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI250H : シリンダ番号チェック
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.ReportDesigner

''' <summary>
''' LMI250ハンドラクラス
''' </summary>
''' <remarks>
''' </remarks>
''' <histry>
''' </histry>
Public Class LMI250H
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIHandler

#Region "Field"

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _V As LMI250V

    ''' <summary>
    ''' Gamenクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI250G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConV As LMIControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConH As LMIControlH

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConG As LMIControlG

#End Region

#Region "Method"

#Region "初期処理"

    ''' <summary>
    ''' ハンドラクラスの初期処理メソッド
    ''' </summary>
    ''' <remarks>画面遷移部品よりこのメソッドが呼ばれます。</remarks>
    Public Sub Main(ByVal prm As LMFormData)

        'カーソルを砂時計にする
        Cursor.Current = Cursors.WaitCursor

        'フォームの作成
        Dim frm As LMI250F = New LMI250F(Me)

        'Validateクラスの設定
        Me._ConV = New LMIControlV(Me, DirectCast(frm, Form), Me._ConG)

        'Gクラスの設定
        Me._ConG = New LMIControlG(DirectCast(frm, Form))

        'ハンドラー共通クラスの設定
        Me._ConH = New LMIControlH(MyBase.GetPGID())

        'Validateクラスの設定
        Me._V = New LMI250V(Me, frm, Me._ConV, Me._ConG)

        'Gamenクラスの設定
        Me._G = New LMI250G(Me, frm, Me._ConG)

        'フォームの初期化
        Call Me.InitControl(frm)

        'ファンクションキーの設定
        Call Me._G.SetFunctionKey(False)

        'タブインデックスの設定
        Call Me._G.SetTabIndex()

        'コントロールの入力制御
        Call Me._G.SetControlsStatus()

        '初期値設定(荷主コード)
        Call Me._G.SetControlPrm()

        'コントロール個別設定
        Call Me._G.SetControl(MyBase.GetSystemDateTime(0))

        'フォーカスの設定
        Call Me._G.SetFoucus()

        'メッセージの表示
        Me.ShowMessage(frm, "G007")

        'フォームの表示
        frm.Show()

        'カーソルを元に戻す
        Cursor.Current = Cursors.Default

    End Sub

#End Region

#Region "イベント定義(一覧)"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <remarks></remarks>
    Private Function PrintShutu(ByVal frm As LMI250F) As Boolean

        '処理開始アクション
        Call Me._ConH.StartAction(frm)

        '権限チェック
        If Me._V.IsAuthorityChk(LMI250C.EventShubetsu.PRINT) = False Then
            '処理終了アクション
            Call Me._ConH.EndAction(frm)
            Return False
        End If

        '入力チェック
        If Me._V.IsInputCheck(LMI250C.EventShubetsu.PRINT) = False Then
            '処理終了アクション
            Call Me._ConH.EndAction(frm)
            Return False
        End If

        'データセット
        Dim rtnDs As DataSet = Me.SetDataSet(frm)

        'ログ出力
        MyBase.Logger.StartLog(MyBase.GetType.Name, System.Reflection.MethodBase.GetCurrentMethod.Name)

        '==========================
        'WSAクラス呼出
        '==========================
        Dim blf As String = String.Concat(MyBase.GetPGID(), LMIControlC.BLF)

        rtnDs = MyBase.CallWSA(blf, "DoPrint", rtnDs)

        'メッセージ判定
        If IsMessageExist() = True Then

            'エラーメッセージ判定
            If MyBase.IsErrorMessageExist() = False Then

                '印刷処理でエラーメッセージあったらメッセージを表示してG007を設定
                MyBase.ShowMessage(frm)
                '処理終了アクション
                Call Me._ConH.EndAction(frm)

                Return False

            End If
        End If

        Dim prevDt As DataTable = rtnDs.Tables(LMConst.RD)
        If prevDt.Rows.Count > 0 Then

            'プレビューの生成 
            Dim prevFrm As New RDViewer()

            'データ設定 
            prevFrm.DataSource = prevDt

            'プレビュー処理の開始 
            prevFrm.Run()

            'プレビューフォームの表示 
            prevFrm.Show()

        End If

        '処理終了アクション
        Call Me._ConH.EndAction(frm)

        '終了メッセージ表示
        MyBase.SetMessage("G002", New String() {"印刷", ""})

        MyBase.ShowMessage(frm)

        Return True

    End Function

    ''' <summary>
    ''' 画面の終了
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Function CloseForm(ByVal frm As LMI250F) As Boolean

        Return True

    End Function

#End Region

#Region "イベント振分け"

    ''' <summary>
    ''' F7押下時処理呼び出し(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey7Press(ByRef frm As LMI250F, ByVal e As System.Windows.Forms.KeyEventArgs)

        Logger.StartLog(Me.GetType.Name, "Print")

        Call Me.PrintShutu(frm)

        Logger.EndLog(Me.GetType.Name, "Print")

    End Sub

    ''' <summary>
    ''' F12押下時処理呼び出し(終了処理)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ファンクションキーイベント</param>
    ''' <remarks></remarks>
    Friend Sub FunctionKey12Press(ByRef frm As LMI250F, ByVal e As System.Windows.Forms.KeyEventArgs)

        '終了処理  
        frm.Close()

    End Sub

    ''' <summary>
    ''' フォームが閉じる前に発生するイベント
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <param name="e">ウィンドウクローズイベント</param>
    ''' <remarks>F12キーイベント、「閉じる」ボタン押下イベントと共有</remarks>
    Friend Sub ClosingForm(ByRef frm As LMI250F, ByVal e As FormClosingEventArgs)

        Logger.StartLog(Me.GetType.Name, "ClosingForm")

        Logger.EndLog(Me.GetType.Name, "ClosingForm")

    End Sub

    '========================  ↓↓↓ その他のイベント  ↓↓↓ ========================

    '========================  ↑↑↑ その他のイベント  ↑↑↑ ========================

#End Region

#Region "データセット設定"

    ''' <summary>
    ''' データセット設定(印刷)
    ''' </summary>
    ''' <param name="frm">このハンドラクラスに紐づくフォーム</param>
    ''' <remarks></remarks>
    Private Function SetDataSet(ByVal frm As LMI250F) As DataSet

        Dim ds As DataSet = New LMI610DS
        Dim dt As DataTable = ds.Tables(LMI250C.TABLE_NM_IN)
        Dim dr As DataRow = dt.NewRow()

        With frm

            'データセットに格納
            dr("NRS_BR_CD") = .cmbEigyo.SelectedValue
            'dr("CUST_CD_L") = .txtCustCd.TextValue
            dr("F_DATE") = .imdPrintDate_From.TextValue
            dr("T_DATE") = .imdPrintDate_To.TextValue
            dt.Rows.Add(dr)

            Return ds

        End With

    End Function

#End Region

#End Region

End Class