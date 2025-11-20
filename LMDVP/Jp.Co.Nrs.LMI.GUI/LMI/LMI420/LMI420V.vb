' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI420V : 運賃比較
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread

''' <summary>
''' LMI420Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI420V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI420F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMIControlV

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI420F, ByVal v As LMIControlV, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

        'Gamen共通クラスの設定
        Me._ControlG = g

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI420C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI420C.EventShubetsu.GETFILE       'データ取得
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT      '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP   '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER   '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT     '50:外部
                        kengenFlg = False
                End Select


            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If
        Return False


    End Function

    ''' <summary>
    ''' 単項目/関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsInputChk(ByVal eventShubetsu As LMI420C.EventShubetsu, Optional ByVal arr As ArrayList = Nothing) As Boolean

        '単項目/関連チェック
        If Me.IsSingleChk(eventShubetsu, arr) = False Then
            Return False
        End If

        Return True

    End Function

#Region "内部メソッド"

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSingleChk(ByVal eventShubetsu As LMI420C.EventShubetsu, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            Dim sCell As LMValidatableCells = New LMValidatableCells(.sprSearch)

            Select Case eventShubetsu

                Case LMI420C.EventShubetsu.GETFILE 'ファイル

                    '【営業所】
                    .cmbNrsBr.ItemName = "営業所"
                    .cmbNrsBr.IsHissuCheck = True
                    If MyBase.IsValidateCheck(.cmbNrsBr) = False Then
                        Me._ControlV.SetErrorControl(.cmbNrsBr)
                        Return False
                    End If

                    '【荷主コード(大)】
                    '.txtCustCdL.ItemName = "荷主(大)"
                    '.txtCustCdL.IsHissuCheck = True
                    '.txtCustCdL.IsForbiddenWordsCheck = True
                    '.txtCustCdL.IsFullByteCheck = 5
                    '.txtCustCdL.IsMiddleSpace = True
                    'If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    '    Me._ControlV.SetErrorControl(.txtCustCdL)
                    '    Return False
                    'End If

                    If String.Empty.Equals(.txtCustCdL.TextValue) Then
                        MyBase.ShowMessage("E019", New String() {"区分マスタ(C032)に対象荷主CD"})
                        Return False

                    End If

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI420C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
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
                    lblCtl = New Control() {.lblCustNmL}
                    msg = New String() {String.Concat(custNm, "コード(大)")}

            End Select

            Return Me._ControlV.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    '''クリア処理を行う
    ''' </summary>
    ''' <param name="ctl">クリア対象項目</param>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ctl As Win.InputMan.LMImTextBox(), _
                             ByVal clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl())

        'クリアコントロール未設定の場合、処理終了
        If clearCtl Is Nothing Then
            Exit Sub
        End If

        Dim clearMax As Integer = clearCtl.Length - 1

        'エディット系コントロールのクリア処理を行う
        For index As Integer = 0 To clearMax

            'コントロール別にクリア処理を行う
            If TypeOf clearCtl(index) Is Win.InputMan.LMImCombo = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImCombo).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMComboKubun = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMComboKubun).SelectedValue = String.Empty

            ElseIf TypeOf clearCtl(index) Is Win.InputMan.LMImNumber = True Then

                DirectCast(clearCtl(index), Win.InputMan.LMImNumber).Value = 0

            Else

                clearCtl(index).TextValue = String.Empty

            End If

        Next

    End Sub

#End Region


#End Region

End Class
