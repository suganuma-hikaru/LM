' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA010V : ログイン
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMA010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMA010F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMA010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal formPattern As String) As Boolean

        With Me._Frm

            If LMA010C.FORM_PATTERN_LOGIN.Equals(formPattern) = True Then

                .txtUserId.ItemName = .lblTitleUserId.Text
                .txtUserId.IsHissuCheck = True
                .txtUserId.IsForbiddenWordsCheck = True
                .txtUserId.IsByteCheck = 5
                .txtUserId.IsMiddleSpace = True
                If Me.IsValidateCheck(.txtUserId) = False Then
                    Return False
                End If

            End If

            .txtPassword.ItemName = .lblTitlePassword.Text
            .txtPassword.IsHissuCheck = True
            .txtPassword.IsForbiddenWordsCheck = True
            .txtPassword.IsByteCheck = 15
            .txtPassword.IsMiddleSpace = True
            If Me.IsValidateCheck(.txtPassword) = False Then
                Return False
            End If

            If LMA010C.FORM_PATTERN_CHANGE_PWD.Equals(formPattern) = True Then

                .txtRePassword.ItemName = .lblTitleRePassword.Text
                .txtRePassword.IsHissuCheck = True
                .txtRePassword.IsForbiddenWordsCheck = True
                .txtRePassword.IsByteCheck = 15
                .txtRePassword.IsMiddleSpace = True
                If Me.IsValidateCheck(.txtRePassword) = False Then
                    Return False
                End If

                '関連チェック
                If .txtPassword.TextValue.Trim().Equals(.txtRePassword.TextValue.Trim()) = False Then
                    MyBase.ShowMessage("E015")
                    .txtPassword.BackColorDef = LMGUIUtility.GetAttentionBackColor
                    .txtRePassword.BackColorDef = LMGUIUtility.GetAttentionBackColor
                    .txtPassword.Focus()
                    Return False
                Else
                    .txtPassword.BackColorDef = Color.White
                    .txtRePassword.BackColorDef = Color.White
                End If

            End If

        End With

        Return True

    End Function

#End Region

End Class
