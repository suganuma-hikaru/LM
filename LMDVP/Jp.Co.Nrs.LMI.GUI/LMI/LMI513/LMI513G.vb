' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI513G : JNC_運賃照合作成
'  作  成  者       :  daikoku
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LMI513Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI513G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI513F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI513F)

        '親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付ける。
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
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim lock As Boolean = False

        Dim empty As String = String.Empty

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = empty
            .F2ButtonName = empty
            .F3ButtonName = empty
            .F4ButtonName = empty
            .F5ButtonName = "処理"
            .F6ButtonName = empty
            .F7ButtonName = empty
            .F8ButtonName = empty
            .F9ButtonName = empty
            .F10ButtonName = empty
            .F11ButtonName = empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = always
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = lock
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = lock
            .F11ButtonEnabled = lock
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

            .grpSakusei.TabIndex = LMI513C.CtlTabIndex.CMB_EIGYO
            .cmbEigyo.TabIndex = LMI513C.CtlTabIndex.CMB_EIGYO
            .txtFilePath.TabIndex = LMI513C.CtlTabIndex.TXT_FILE_PATH
            .imdSyoriDate.TabIndex = LMI513C.CtlTabIndex.SYORI_DATE

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal data As String)

        With Me._Frm

            '自営業所の設定
            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '取込ファイルパス
            .txtFilePath.TextValue = LMI513C.FILE_PATH

            '処理月
            Dim syoriDate As Date = New Date(Date.Today.Year, Date.Today.Month, 1)
            syoriDate = syoriDate.AddDays(-1)

            .imdSyoriDate.TextValue = syoriDate.ToString("yyyyMMdd")

        End With

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .imdSyoriDate.Focus()
        End With
    End Sub

    ''' <summary>
    ''' 日付を表示するコントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetDateControl()

        With Me._Frm

            Call Me.SetDateFormat(.imdSyoriDate)

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">書式設定を行うコントロール</param>
    ''' <remarks></remarks>
    Private Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMM")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

    End Sub
#End Region

#End Region


#End Region

End Class
