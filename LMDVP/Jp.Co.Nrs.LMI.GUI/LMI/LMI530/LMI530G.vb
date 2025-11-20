' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530G : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI530Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI530G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI530F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI530F)

        ' 親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        ' 呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        ' 画面構築をするフォームをこのクラスに紐付ける。
        MyBase.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal nrsBrCd As String)

        Dim always As Boolean = True
        Dim lock As Boolean = False

        Dim empty As String = String.Empty

        With Me._Frm.FunctionKey

            ' ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            ' ファンクションキー個別設定
            .F1ButtonName = empty
            .F2ButtonName = empty
            .F3ButtonName = empty
            .F4ButtonName = empty
            .F5ButtonName = empty
            .F6ButtonName = empty
            .F7ButtonName = empty
            .F8ButtonName = empty
            .F9ButtonName = empty
            .F10ButtonName = empty
            .F11ButtonName = LMIControlC.FUNCTION_HOZON
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            ' ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            If nrsBrCd = LMI530C.ENABLE_NRS_BR_CD Then
                .F11ButtonEnabled = always
            Else
                .F11ButtonEnabled = lock
            End If
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .cmbSelectKb.TabIndex = LMI530C.CtlTabIndex.SELECT_KB

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl()

        ' コントロール値のクリア
        Call Me.ClearControl()

    End Sub

#Region "コントロールの初期設定"
    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbSelectKb.SelectedValue = ""

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal ediCustIndex As Integer, Optional ByVal itemsAdd As Boolean = True)

        With Me._Frm

            ' cmbSelectKb の値の設定
            If .cmbSelectKb.Enabled Then
                If itemsAdd Then
                    .cmbSelectKb.Items.Add(LMI530C.cmbSelectKbItems.CSV)
                    .cmbSelectKb.Items.Add(LMI530C.cmbSelectKbItems.Excel)
                End If
                If ediCustIndex = LMI530C.ediCustIndex.Excel Then
                    .cmbSelectKb.SelectedIndex = 1
                Else
                    .cmbSelectKb.SelectedIndex = 0
                End If
            Else
                If itemsAdd Then
                    .cmbSelectKb.Items.Add("")
                End If
                .cmbSelectKb.SelectedIndex = 0
            End If

        End With

    End Sub

    ''' <summary>
    ''' デザイナ上で設定できない(実行時戻ってしまう)画面幅関連の縮小方向の調整
    ''' </summary>
    Friend Sub AdjustFormaAndControls()

        ' 画面幅縮小値
        Dim shrinkWidth As Integer = 234

        Dim i As Integer
        Dim functionKeyName As String
        Dim functionKeyRightMargin As Integer
        Dim functionKeyHideCnt As Integer = 0

        With Me._Frm
            ' ファンクションキー右端マージンの退避(画面幅縮小前)
            functionKeyRightMargin = .Width - (.FunctionKey.Left + .FunctionKey.Width)

            ' 画面幅縮小(初期表示位置は縮小幅分中央に寄せ直す)
            .Left += CType(shrinkWidth / 2, Integer)
            .Width -= shrinkWidth

            ' デザイン段階で表示の8ファンクションキー(F5～F12)中、非表示分の幅の縮小
            For i = 4 To 9
                functionKeyHideCnt += 1
                functionKeyName = ""
                If i = 4 Then functionKeyName = "F5"
                If i = 5 Then functionKeyName = "F6"
                If i = 6 Then functionKeyName = "F7"
                If i = 7 Then functionKeyName = "F8"
                If i = 8 Then functionKeyName = "F9"
                If i = 9 Then functionKeyName = "Pause"
                If i = 10 Then functionKeyName = "F11"
                If i = 11 Then functionKeyName = "F12"
                If .FunctionKey.FunctionKeyButtons(i).FunctionKey.ToString() = functionKeyName Then
                    .FunctionKey.FunctionKeyButtons(i).Visible = False
                End If
            Next
            .FunctionKey.Width -= CType((.FunctionKey.Width / 8) * functionKeyHideCnt, Integer)
            .FunctionKey.Left = .Width - .FunctionKey.Width - functionKeyRightMargin

            For Each ctlInFrm As Control In .Controls
                If ctlInFrm.Name = "pnlStatusAria" Then
                    ' ステータスバー幅縮小
                    ctlInFrm.Width -= shrinkWidth
                    For Each ctlInSts As Control In ctlInFrm.Controls
                        If ctlInSts.Name = "StatusStrip" Then
                            ' ステータスバー内表示項目幅集計
                            Dim shrinkCount As Integer
                            Dim totalWidth As Integer = 0
                            shrinkCount = 0
                            For i = 0 To DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items.Count
                                If _
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDbNm" OrElse
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusUserNm" OrElse
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDateTime" Then
                                    shrinkCount += 1
                                    totalWidth += DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Width
                                End If
                                If shrinkCount >= 3 Then
                                    Exit For
                                End If
                            Next
                            ' ステータスバー内表示項目幅の縮小(元のステータスバー内表示項目幅の割合での画面幅縮小値按分)
                            shrinkCount = 0
                            For i = 0 To DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items.Count
                                If _
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDbNm" OrElse
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusUserNm" OrElse
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Name = "lblStatusDateTime" Then
                                    shrinkCount += 1
                                    DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Width -=
                                        CType(Math.Truncate(shrinkWidth * DirectCast(ctlInSts, System.Windows.Forms.ToolStrip).Items(i).Width / totalWidth), Integer)
                                End If
                                If shrinkCount >= 3 Then
                                    Exit For
                                End If
                            Next
                        End If
                        Exit For
                    Next
                    Exit For
                End If
            Next

        End With

    End Sub

#End Region


    ''' <summary>
    '''画面の入力項目/ファンクションキーの制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm(ByVal nrsBrCd As String)

        Call Me.SetFunctionKey(nrsBrCd)

        With Me._Frm
            If nrsBrCd = LMI530C.ENABLE_NRS_BR_CD Then
                .cmbSelectKb.Enabled = True
            Else
                .cmbSelectKb.Enabled = False
            End If
        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .cmbSelectKb.Focus()
        End With
    End Sub

#End Region

#End Region

#End Region

End Class
