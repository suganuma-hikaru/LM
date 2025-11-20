' ==========================================================================
'  システム名       :  GTO
'  サブシステム名   :  GTA     : メニュー
'  プログラムID     :  LMA010G : ログイン
'  作  成  者       :  [iwamoto]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMA010Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMA010G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMA010F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMA010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "Form"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .txtUserId.TabIndex = LMA010C.CtlTabIndex.USERID
            .txtPassword.TabIndex = LMA010C.CtlTabIndex.PASSWORD
            .txtRePassword.TabIndex = LMA010C.CtlTabIndex.REPASSWORD
            .btnOk.TabIndex = LMA010C.CtlTabIndex.OK
            .btnCancel.TabIndex = LMA010C.CtlTabIndex.CANCEL
            .grpMessageType.TabIndex = LMA010C.CtlTabIndex.GRP_MESSAGE_TYPE
            .optJp.TabIndex = LMA010C.CtlTabIndex.OPT_JP
            .optEn.TabIndex = LMA010C.CtlTabIndex.OPT_EN
            .optKo.TabIndex = LMA010C.CtlTabIndex.OPT_KO
            .optCn.TabIndex = LMA010C.CtlTabIndex.OPT_CN

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal appVersion As String, ByVal formPattern As String)

        'システム名タイトルの設定
        With _Frm

            'コンフィグデータより取得
            .lblSystemTitle.Text = ConfigManager.GetConfigValue(ConfigManager.SYSTEM_NAME)
            .lblVersion.Text = "Version : " & appVersion

            '画面項目の制御
            '①入力項目タイトル
            .lblTitleUserId.Text = "User ID"
            .lblTitlePassword.Text = "Password"
            .lblTitleRePassword.Text = "Re-type password"
            .btnOk.Text = "Login"

            '②表示・非表示
            .lblTitleUserId.Visible = True
            .txtUserId.Visible = True
            .lblTitlePassword.Visible = True
            .txtPassword.Visible = True
            .lblTitleRePassword.Visible = False
            .txtRePassword.Visible = False

            '画面パターンによって、画面コントロールの制御
            If formPattern = LMA010C.FORM_PATTERN_CHANGE_PWD Then

                .lblTitlePassword.Text = "New Password"
                .btnOk.Text = "OK"

                .lblTitleUserId.Visible = False
                .txtUserId.Visible = False

                .lblTitleRePassword.Visible = True
                .txtRePassword.Visible = True
                .lblTitleLang.Visible = False
                .optEn.Visible = False
                .optJp.Visible = False
                .optKo.Visible = False
                .optCn.Visible = False
                .grpMessageType.Visible = False

            End If

            '初期値の設定
            .optJp.Checked = True

            '2017/09/25 修正(韓国語対応) 李↓
            '-----------------------------------------------------------------------------------
            ' 多言語対応　業務プログラムの対応が完了するまで韓国語、中国語は操作不可

            '.optKo.Enabled = False

            .optCn.Enabled = False
            '-----------------------------------------------------------------------------------
            '2017/09/25 修正 李↑

        End With

    End Sub

#End Region 'From

#End Region 'Method

End Class
