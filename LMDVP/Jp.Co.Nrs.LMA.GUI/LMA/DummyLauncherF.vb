Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Base
Imports FarPoint.Win.Spread.Model
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' DummyLauncherFクラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' </histry>
Public Class DummyLauncherF

    Private _handler As DummyLauncherH

    Sub New()

        ' この呼び出しは、Windows フォーム デザイナで必要です。
        InitializeComponent()

        ' InitializeComponent() 呼び出しの後で初期化を追加します。

        Me._handler = New DummyLauncherH

    End Sub

    ''' <summary>
    ''' デバッグプロセスのシャットダウンを行う
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub DummyLauncherF_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed

        Me._handler.ShutDownLauncherProcesses()

    End Sub

    ''' <summary>
    ''' 読み込みボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub LoadButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles LoadButton.Click

        Me._handler.ClassLoad(Me)

    End Sub

    ''' <summary>
    ''' DLLリストダブルクリック時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AssemblyListBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles AssemblyListBox.DoubleClick

        Me._handler.ClassLoad(Me)

    End Sub

    ''' <summary>
    ''' DLLリストでキー押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub AssemblyListBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles AssemblyListBox.KeyDown

        If e.KeyCode = Keys.Enter Then
            Me._handler.ClassLoad(Me)
        End If

    End Sub

    ''' <summary>
    ''' フォームリストでキー押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FormListBox_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles ClassListBox.KeyDown
        If e.KeyCode = Keys.Enter Then
            Me._handler.Execute(Me)
        End If
    End Sub

    ''' <summary>
    ''' フォームリストでダブルクリック時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub FormListBox_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClassListBox.DoubleClick

        Me._handler.Execute(Me)

    End Sub

    ''' <summary>
    ''' 実行ボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnExecute_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExecute.Click

        Me._handler.Execute(Me)

    End Sub

    ''' <summary>
    ''' 閉じるボタン押下時のイベント
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        Me.Close()

    End Sub

    ''' <summary>
    ''' マスタデータ取得ボタン
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnGetMaster_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGetMaster.Click

        Me._handler.btnGetMasterClick(Me)

    End Sub

    ''' <summary>
    ''' ハンドラクラスが選択されなおしたとき
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub ClassListBox_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ClassListBox.SelectedIndexChanged

        Me._handler.ViewParam(Me)

    End Sub

    ''' <summary>
    ''' 下キーを押されたときに行を増やす
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub paramView_PreviewKeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles pramView.PreviewKeyDown

        If e.KeyCode = Keys.Down Then

            With Me.pramView.ActiveSheet

                If .ActiveRow.Index = .RowCount - 1 Then

                    .AddRows(.RowCount, 1)

                End If

                'セルに設定するスタイルの取得
                Dim cellStyle As FarPoint.Win.Spread.StyleInfo = LMSpreadUtility.GetTextCell(Me.pramView, InputControl.ALL_MIX, 0, False)

                Dim styleModel As DefaultSheetStyleModel = _
                    DirectCast(.Models.Style, DefaultSheetStyleModel)

                '値設定
                For i As Integer = 0 To .ColumnCount - 1

                    'セルスタイル設定
                    styleModel.SetDirectInfo(0, i, cellStyle)

                Next

            End With

        End If

    End Sub

    ''' <summary>
    ''' スプレッドからのタブ移動
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub paramView_Advance(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.AdvanceEventArgs) Handles pramView.Advance
        Dim iRow As Integer = pramView.ActiveSheet.ActiveRowIndex
        Dim iCol As Integer = pramView.ActiveSheet.ActiveColumnIndex
        If iRow = pramView.ActiveSheet.RowCount - 1 And iCol = pramView.ActiveSheet.ColumnCount - 1 Then

            e.Cancel = True

            Me.btnExecute.Focus()

        End If
    End Sub

    ''' <summary>
    ''' 認証ボタンクリック
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        Call _handler.btnLoginClick(Me)

    End Sub

    ''' <summary>
    ''' メッセージ言語選択時
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    ''' <remarks></remarks>
    Private Sub optJp_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optJp.CheckedChanged, optEn.CheckedChanged, optKo.CheckedChanged, optCN.CheckedChanged

        Select Case True
            Case optJp.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_JP)
            Case optEn.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_EN)
            Case optKo.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_KO)
            Case optCN.Checked = True
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_CN)
            Case Else
                MessageManager.MessageLanguage = CStr(MessageType.MessageType_JP)
        End Select

    End Sub

    Private Sub txtUserID_Validated(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserID.Validated

        Me.LmErrorProvider.SetError(Me.txtUserID, "")

    End Sub

    Private Sub txtUserID_Validating(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtUserID.Validating


        '存在チェック
        If "".Equals(Me.txtUserID.Text.Trim) Then

            e.Cancel = True
            Me.txtUserID.Focus()
            Me.LmErrorProvider.SetError(Me.txtUserID, "入力してください。")
        End If

    End Sub

End Class
