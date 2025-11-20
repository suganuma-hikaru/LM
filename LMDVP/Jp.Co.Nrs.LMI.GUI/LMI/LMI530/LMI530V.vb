' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI530V : セミEDI環境切り替え(丸和物産)
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI530Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMI530V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI530F
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI530F, ByVal v As LMIControlV, ByVal g As LMIControlG)

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
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk() As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

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

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        '単項目チェック
        If Me.IsSingleChk() = False Then
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

            ' 現在の取込対象
            .cmbSelectKb.ItemName = .lblTitleSelectKb.Text
            .cmbSelectKb.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSelectKb) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

End Class
