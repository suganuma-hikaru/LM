' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LML     : 協力会社管理
'  プログラムID     :  LML010V : 協力会社処理
'  作  成  者       :  [大極]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LML010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LML010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LML010F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LML010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal EVENTSHUBETSU As LML010C.EventShubetsu) As Boolean

        '権限の設定
        Dim _Kengen As String = LMUserInfoManager.GetAuthoLv
        Dim _Sentaku As LML010C.EventShubetsu = LML010C.EventShubetsu.SENTAKU

        If _Kengen.Equals(LMConst.AuthoKBN.AGENT) Then                '外部
            If EVENTSHUBETSU.Equals(_Sentaku) Then                    '選択処理
                MyBase.ShowMessage("E016")
                Return False
            End If
        End If

        Return True

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

        With Me._Frm

            '処理内容
            .CmbShori.ItemName = "荷主コード"
            .CmbShori.IsHissuCheck = True
            If MyBase.IsValidateCheck(.CmbShori) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region 'Method

End Class
