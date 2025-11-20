' ========================================================================
'  システム名     : LM
'  サブシステム名 : LMA     : 協力会社管理
'  プログラムID   : LML010G : 協力会社機能
'  作  成  者     : [大極]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LML010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LML010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LML010F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMIConG As LMLControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LML010F, ByVal g As LMLControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMIConG = g
    End Sub

#End Region 'Constructor

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey(ByVal rock As Boolean)

        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F9ButtonName = "実　行"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = False
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = rock
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            'Main
            .CmbCustNm.TabIndex = 1
            .CmbShori.TabIndex = 2

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus(ByVal rock As Boolean)

        With Me._Frm
            .CmbShori.Enabled = rock

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus(Optional ByVal tmpKBN As String = "")

        With Me._Frm
            .CmbCustNm.Focus()

        End With

    End Sub

    ''' <summary>
    ''' 画面名の設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetComboBoxShori(ByVal KbnCd As String)

        With Me._Frm

            'コンボボックスのクリア
            ClearComboBoxShori()

            Dim sql As String = String.Concat("KBN_GROUP_CD = 'L008' ", " AND KBN_NM5 = '", KbnCd, "' ")

            'コンボボックス生成
            Me._LMIConG.CreateComboBox(.CmbShori, LMConst.CacheTBL.KBN, New String() {"KBN_CD"}, New String() {"KBN_NM2"}, sql, "KBN_CD")

            'コンボボックスの設定
            Me.SetControlsStatus(True)

            'ファンクションキーの設定
            Me.SetFunctionKey(True)
        End With

    End Sub

    ''' <summary>
    ''' コンボボックスの画面名をクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearComboBoxShori()
        With Me._Frm

            .CmbShori.SelectedValue() = ""
            .CmbShori.Items.Clear()

        End With
    End Sub

#End Region

#End Region

#End Region

End Class
