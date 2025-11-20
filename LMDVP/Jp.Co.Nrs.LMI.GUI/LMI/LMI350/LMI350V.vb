' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI350V : 保管荷役明細(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI350Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI350V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI350F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconV As LMIControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIconG As LMIControlG
#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI350F, ByVal v As LMIControlV, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        Me._LMIconV = v

        Me._LMIconG = g

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMI350C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMI350C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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


            Case LMI350C.EventShubetsu.PRINT        '印刷
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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



            Case LMI350C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' 単項目/関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsInputCheck() As Boolean

        '単項目/関連チェック
        If Me.IsSingleChk() = False OrElse Me.IsSaveCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSingleChk() As Boolean

        With Me._Frm


            '**********単項目チェック(印刷種別共通)

            '営業所
            .cmbBr.ItemName = "営業所"
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '荷主(大)コード
            .txtCustCdL.ItemName = "荷主(大)コード"
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主(中)コード
            .txtCustCdM.ItemName = "荷主(中)コード"
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtSeiqCd) = False Then
                Return False
            End If

            '荷主(小)コード
            .txtCustCdS.ItemName = "荷主(小)コード"
            .txtCustCdS.IsHissuCheck = True
            .txtCustCdS.IsForbiddenWordsCheck = True
            .txtCustCdS.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtSeiqCd) = False Then
                Return False
            End If

            '荷主(中)コード
            .txtSeiqCd.ItemName = "請求先コード"
            .txtSeiqCd.IsHissuCheck = True
            .txtSeiqCd.IsForbiddenWordsCheck = True
            .txtSeiqCd.IsFullByteCheck = 7
            If MyBase.IsValidateCheck(.txtSeiqCd) = False Then
                Return False
            End If

            Dim errorFlg As Boolean = False

            '請求年月
            .imdSeiqDate.ItemName = "請求年月"
            .imdSeiqDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdSeiqDate) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck() As Boolean

        With Me._Frm


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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI350C.EventShubetsu) As Boolean

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

                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtCustCdS.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM, .txtCustCdS}
                    lblCtl = New Control() {.lblCustNmL, .lblCustNmM, .lblCustNmS}
                    msg = New String() {String.Concat(custNm, "コード(大)"), String.Concat(custNm, "コード(中)"), String.Concat(custNm, "コード(小)")}

                Case .txtSeiqCd.Name

                    Dim SeiqNm As String = .lblTitleSeiq.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtSeiqCd}
                    lblCtl = New Control() {.lblSeiqNm}
                    msg = New String() {SeiqNm}

            End Select

            Return Me._LMIconV.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

#End Region

End Class
