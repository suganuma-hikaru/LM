' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI960V : 出荷データ確認（ハネウェル）
'  作  成  者       :  Minagawa
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread


''' <summary>
''' LMI960Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI960V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI960F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMFControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI960F, ByVal v As LMFControlV, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = g

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal eventShubetsu As LMI960C.EventShubetsu) As Boolean

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'ヘッダ項目のチェック
        Dim rtnResult As Boolean = Me.IsHeaderChk(eventShubetsu)

        Return rtnResult

    End Function

    ''' <summary>
    ''' ヘッダ項目のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk(ByVal eventShubetsu As LMI960C.EventShubetsu) As Boolean

        With Me._Frm

            '営業所
            Dim rtnResult As Boolean = Me.IsInputted(Me._Frm.cmbEigyo, Me._Frm.lblTitleEigyo.Text)

            If rtnResult Then
                Select Case Me._Frm.cmbEigyo.SelectedValue.ToString
                    Case LMI960C.NrsBrCd.Chiba, LMI960C.NrsBrCd.Forwarding
                    Case Else
                        MyBase.ShowMessage("E208", {"選択された" & Me._Frm.lblTitleEigyo.Text & "は"})
                        rtnResult = False
                End Select
            End If

            '部門
            rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.cmbBumon, Me._Frm.lblTitleBumon.Text)

            '出荷日
            rtnResult = rtnResult AndAlso Me.IsValidOutkaDate()

            '実績作成の場合
            If eventShubetsu = LMI960C.EventShubetsu.JISSEKI_SAKUSEI Then

                '場所区分
                rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.cmbBashoKb, Me._Frm.lblTitleBashoKb.Text)

                '到着時刻
                rtnResult = rtnResult AndAlso IsValidTime(.dtpArrivalTime, .lblTitleArrivalTime)

                If .cmbBashoKb.TextValue <> LMI960C.CmbBashoKbItems.NonyuYotei Then
                    '出発時刻
                    rtnResult = rtnResult AndAlso IsValidTime(.dtpDepartureTime, .lblTitleDepartureTime)

                    If rtnResult Then
                        '到着時刻・出発時刻の大小関係チェック
                        If .dtpArrivalTime.TextValue > .dtpDepartureTime.TextValue Then
                            MyBase.ShowMessage("E271", New String() { .lblTitleDepartureTime.Text, .lblTitleArrivalTime.Text & "以降に"})
                            rtnResult = False
                        End If
                    End If
                End If

            End If

            'ADD START 2019/03/27
            '一括変更【出荷日・納入日】の場合
            If eventShubetsu = LMI960C.EventShubetsu.IKKATSU_CHANGE Then

                '一括変更項目
                rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.cmbChangeItem, Me._Frm.pnlIkkatsuChange.Text)

                '一括変更値【出荷日・納入日】
                rtnResult = rtnResult AndAlso Me.IsValidChangedValue()

            End If
            'ADD END   2019/03/27

            'ADD S 2020/03/06 011377
            '受注返答の場合
            If eventShubetsu = LMI960C.EventShubetsu.JUCHU_SAKUSEI Then
                If .optJuchuNo.Checked Then
                    rtnResult = rtnResult AndAlso Me.IsInputted(.cmbDeclineReason, "受注可否Noの理由")
                End If
            End If
            'ADD E 2020/03/06 011377

            '遅延送信の場合
            If eventShubetsu = LMI960C.EventShubetsu.DELAY_SAKUSEI Then
                rtnResult = rtnResult AndAlso Me.IsInputted(.cmbDelayShubetsu, "遅延種別")
                rtnResult = rtnResult AndAlso Me.IsInputted(.cmbDelayReason, "遅延理由")
                rtnResult = rtnResult AndAlso Me.IsInputted(.cmbDelayHosoku, "遅延補足")
            End If

            '受注ステータス戻し処理の場合
            If eventShubetsu = LMI960C.EventShubetsu.ROLLBACK_JUCHU_STATUS Then
                If rtnResult AndAlso (Me._Frm.ProcessingBumon <> LMI960C.CmbBumonItems.Soko AndAlso
                    Me._Frm.ProcessingBumon <> LMI960C.CmbBumonItems.ChilledLorry) Then
                    '「倉庫/ISO/Chilled Lorry」が(倉庫 または Chilled Lorry)でない場合
                    MyBase.ShowMessage("E428", New String() {"倉庫ではなく Chilled Lorry でもない", "、処理", ""})
                    rtnResult = False
                End If
            End If

            'JOB NO変更の場合
            If eventShubetsu = LMI960C.EventShubetsu.MOD_JOB_NO Then
                If rtnResult AndAlso (Me._Frm.ProcessingBumon <> LMI960C.CmbBumonItems.ISO) Then
                    '「倉庫/ISO」がISOでない場合
                    MyBase.ShowMessage("E428", New String() {"ISOではない", "、処理", ""})
                    rtnResult = False
                End If

                '一括変更値【JOB NO】
                rtnResult = rtnResult AndAlso Me.IsValidChangedValue()
            End If

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="itmName">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputted(ByVal ctl As Win.InputMan.LMImCombo, ByVal itmName As String) As Boolean

        ctl.ItemName = itmName
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 出荷日のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidOutkaDate() As Boolean

        With Me._Frm

            '出荷日From
            If .imdOutkaDateFrom.IsDateFullByteCheck() = False Then
                Return False
            End If

            '出荷日To
            If .imdOutkaDateTo.IsDateFullByteCheck() = False Then
                Return False
            End If

            '出荷日From/Toのいずれかは必須
            If String.IsNullOrEmpty(.imdOutkaDateFrom.TextValue) AndAlso String.IsNullOrEmpty(.imdOutkaDateTo.TextValue) Then
                .imdOutkaDateFrom.Focus()  'ADD 2020/02/07 010901
                MyBase.ShowMessage("E270", New String() {.lblTitleOutkaDate.Text & "From", .lblTitleOutkaDate.Text & "To"})
                Return False
            End If

            '出荷日From≦Toであること
            If Not String.IsNullOrEmpty(.imdOutkaDateFrom.TextValue) AndAlso Not String.IsNullOrEmpty(.imdOutkaDateTo.TextValue) Then
                If .imdOutkaDateFrom.TextValue > .imdOutkaDateTo.TextValue Then
                    .imdOutkaDateFrom.Focus()  'ADD 2020/02/07 010901
                    MyBase.ShowMessage("E039", New String() {.lblTitleOutkaDate.Text & "To", .lblTitleOutkaDate.Text & "From"})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 時刻チェック
    ''' </summary>
    ''' <param name="ctrl"></param>
    ''' <param name="lblTitle"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsValidTime(ByVal ctrl As Win.InputMan.LMImTime, ByVal lblTitle As Win.LMTitleLabel) As Boolean

        ctrl.ItemName = lblTitle.Text
        ctrl.IsHissuCheck = True
        If MyBase.IsValidateCheck(ctrl) = False Then
            Return False
        End If

        If ctrl.TextValue.Length <> 4 Then
            MyBase.ShowMessage("E038", New String() {lblTitle.Text, "4"})
            Return False
        End If

        Return True

    End Function

    'ADD START 2019/03/27
    ''' <summary>
    ''' 一括変更値チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsValidChangedValue() As Boolean

        Select Case _Frm.cmbChangeItem.SelectedValue.ToString()
            '一括変更項目による分岐

            Case LMI960C.CmbIkkatsuChangeItems.ShukkaDate, LMI960C.CmbIkkatsuChangeItems.NonyuDate
                '出荷日/納入日の場合

                '日付チェック
                Dim ctrl As Win.InputMan.LMImDate = _Frm.imdChangeDate
                ctrl.ItemName = _Frm.cmbChangeItem.SelectedText
                ctrl.IsHissuCheck = True
                Return MyBase.IsValidateCheck(ctrl)

            Case LMI960C.CmbIkkatsuChangeItems.JobNo
                'JOB NOの場合

                'JOB NOチェック
                Dim ctrl As Win.InputMan.LMImTextBox = _Frm.txtJobNo
                ctrl.ItemName = _Frm.cmbChangeItem.SelectedText
                ctrl.IsHissuCheck = True
                ctrl.IsFullByteCheck = 10
                Return MyBase.IsValidateCheck(ctrl)

        End Select

        Return False

    End Function
    'ADD END   2019/03/27

    ''' <summary>
    ''' 処理対象チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTargetSelected(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        Return rtnResult

    End Function

    'ADD S 2020/02/07 010901
    ''' <summary>
    ''' 単一選択チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsOneItemSelected(ByVal arr As ArrayList) As Boolean

        If arr.Count <> 1 Then
            MyBase.ShowMessage("E008")  'MOD 2020/02/07 010901
            Return False
        End If

        Return True

    End Function
    'ADD E 2020/02/07 010901

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

        End With

    End Sub

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMI960C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMI960C.ActionType.PRINT

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.LOOPEDIT

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.KENSAKU

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.ENTER

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.MASTEROPEN

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.DOUBLECLICK


                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.SAVE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI960C.ActionType.CLOSE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select

            Case LMI960C.ActionType.BACKUP
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

#End Region 'Method

End Class
