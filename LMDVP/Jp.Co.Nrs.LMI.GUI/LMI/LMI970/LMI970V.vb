' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI970V : 運賃データ入力・確認（千葉日産物流）
'  作  成  者       :  Minagawa
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI970Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI970V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI970F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI970F, ByVal v As LMFControlV, ByVal g As LMFControlG)

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
    Friend Function IsInputCheck(ByVal eventShubetsu As LMI970C.EventShubetsu) As Boolean

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue(LMI970C.ActionType.KENSAKU)

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk(eventShubetsu)

        Return rtnResult

    End Function

    ''' <summary>
    ''' ヘッダ項目の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk(ByVal eventShubetsu As LMI970C.EventShubetsu) As Boolean

        With Me._Frm

            '営業所
            Dim rtnResult As Boolean = Me.IsInputted(Me._Frm.cmbEigyo, Me._Frm.lblTitleEigyo.Text)

            'ADD START 2019/05/30 要望管理006030
            If eventShubetsu = LMI970C.EventShubetsu.UPDATE_TANKA Then
                '単価更新の場合

                rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.imdTargetYM, "対象年月")
                rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.numTanka, "単価")

                '単価更新の場合、以降の項目チェックは不要
                Return rtnResult
            End If
            'ADD END   2019/05/30 要望管理006030

            '荷主コード
            rtnResult = rtnResult AndAlso Me.IsValidCustCd()

            'EDI取込日
            rtnResult = rtnResult AndAlso Me.IsValidTorikomiDate()

            '検索日
            rtnResult = rtnResult AndAlso Me.IsValidSearchDate()

            If eventShubetsu = LMI970C.EventShubetsu.PRINT Then
                '印刷の場合
                rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.cmbPrint, "印刷種別")
            End If

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 荷主コードの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidCustCd() As Boolean

        With Me._Frm

            '荷主コード(大)
            .txtCustCdL.ItemName = .lblTitleCust.Text
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' EDI取込日のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidTorikomiDate() As Boolean

        With Me._Frm

            'EDI取込日From
            If .imdTorikomiDateFrom.IsDateFullByteCheck() = False Then
                Return False
            End If

            'EDI取込日To
            If .imdTorikomiDateTo.IsDateFullByteCheck() = False Then
                Return False
            End If


            If Not String.IsNullOrEmpty(.imdTorikomiDateFrom.TextValue) _
            AndAlso Not String.IsNullOrEmpty(.imdTorikomiDateTo.TextValue) Then

                '大小関係チェック
                If .imdTorikomiDateFrom.TextValue > .imdTorikomiDateTo.TextValue Then

                    MyBase.ShowMessage("E039", New String() {.lblTitleTorikomiDate.Text & "To", .lblTitleTorikomiDate.Text & "From"})
                    Return False

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索日のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsValidSearchDate() As Boolean

        With Me._Frm

            If Not String.IsNullOrEmpty(.cmbSearchDateKb.TextValue) Then

                '検索日From
                If .imdSearchDateFrom.IsDateFullByteCheck() = False Then
                    Return False
                End If

                '検索日To
                If .imdSearchDateTo.IsDateFullByteCheck() = False Then
                    Return False
                End If

                If Not String.IsNullOrEmpty(.imdSearchDateFrom.TextValue) _
                AndAlso Not String.IsNullOrEmpty(.imdSearchDateTo.TextValue) Then

                    '大小関係チェック
                    If .imdSearchDateFrom.TextValue > .imdSearchDateTo.TextValue Then

                        MyBase.ShowMessage("E039", New String() {.cmbSearchDateKb.TextValue & "To", .cmbSearchDateKb.TextValue & "From"})
                        Return False

                    End If

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI970C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMI970C.ActionType.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL}
                    lblCtl = New Control() {.lblCustNm}
                    msg = New String() {String.Concat(custNm, LMFControlC.CD), String.Concat(custNm, LMFControlC.BR_CD)}

            End Select

            'フォーカス位置チェック
            Dim rtnResult As Boolean = Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

            '営業所必須
            rtnResult = rtnResult AndAlso Me.IsInputted(Me._Frm.cmbEigyo, Me._Frm.lblTitleEigyo.Text)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="ctl">LMImComboコントロール</param>
    ''' <param name="itmName">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInputted(ByVal ctl As Win.InputMan.LMImCombo, ByVal itmName As String) As Boolean

        ctl.ItemName = itmName
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="ctl">LMComboKubunコントロール</param>
    ''' <param name="itmName">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInputted(ByVal ctl As Win.InputMan.LMComboKubun, ByVal itmName As String) As Boolean

        ctl.ItemName = itmName
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    'ADD START 2019/05/30 要望管理006030
    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="ctl">LMImDateコントロール</param>
    ''' <param name="itmName">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInputted(ByVal ctl As Win.InputMan.LMImDate, ByVal itmName As String) As Boolean

        ctl.ItemName = itmName
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    ''' <summary>
    ''' 必須チェック
    ''' </summary>
    ''' <param name="ctl">LMImNumberコントロール</param>
    ''' <param name="itmName">項目名</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInputted(ByVal ctl As Win.InputMan.LMImNumber, ByVal itmName As String) As Boolean

        ctl.ItemName = itmName
        ctl.IsHissuCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function
    'ADD END   2019/05/30 要望管理006030

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMI970C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMI970C.ActionType.PRINT

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

            Case LMI970C.ActionType.LOOPEDIT

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

            Case LMI970C.ActionType.KENSAKU

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

            Case LMI970C.ActionType.ENTER

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

            Case LMI970C.ActionType.MASTEROPEN

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

            Case LMI970C.ActionType.DOUBLECLICK


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

            Case LMI970C.ActionType.SAVE

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

            Case LMI970C.ActionType.CLOSE

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

            Case LMI970C.ActionType.BACKUP
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

    ''' <summary>
    ''' 処理対象チェック
    ''' </summary>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsTargetSelected(ByVal arr As ArrayList) As Boolean

        'スペース除去
        Call Me.TrimHeaderSpaceTextValue(LMI970C.ActionType.KENSAKU)

        '未選択チェック
        Dim cnt As Integer = arr.Count
        Dim rtnResult As Boolean = Me._Vcon.IsSelectChk(cnt)

        Return rtnResult

    End Function

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue(ByVal actionType As LMI970C.ActionType)

        With Me._Frm

            Select Case actionType

                Case LMI970C.ActionType.KENSAKU

                    .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()

            End Select

        End With

    End Sub

#End Region 'Method

End Class
