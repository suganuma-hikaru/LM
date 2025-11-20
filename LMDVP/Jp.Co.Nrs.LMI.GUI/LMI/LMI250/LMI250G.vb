' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI250G : シリンダ番号チェック
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan

''' <summary>
''' LMI250Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI250G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI250F

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ConG As LMIControlG

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI250F, ByVal g As LMIControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._ConG = g
    End Sub

#End Region

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
            .F5ButtonName = String.Empty
            .F6ButtonName = String.Empty
            .F7ButtonName = "印 刷"
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
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
            .F7ButtonEnabled = always
            .F8ButtonEnabled = False
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
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

            .imdPrintDate_From.TabIndex = 1
            .imdPrintDate_To.TabIndex = 2

        End With

    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        With Me._Frm

            '.txtCustCD.ReadOnly = True

        End With

    End Sub

#End Region

#Region "コントロールの初期設定"

    ''' <summary>
    '''営業所、システム日付 - 15日、システム日付 + 15日の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm

            .cmbEigyo.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            'システム日付 - 15日取得
            Dim fromDate As String = DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), Convert.ToInt32(Mid(sysDate, 7, 2))).AddDays(-15).ToString("yyyyMMdd")

            'システム日付 + 15日取得
            Dim toDate As String = DateSerial(Convert.ToInt32(Mid(sysDate, 1, 4)), Convert.ToInt32(Mid(sysDate, 5, 2)), Convert.ToInt32(Mid(sysDate, 7, 2))).AddDays(15).ToString("yyyyMMdd")

            .imdPrintDate_From.TextValue = fromDate
            .imdPrintDate_To.TextValue = toDate

        End With

    End Sub

    ''' <summary>
    '''初期荷主の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetControlPrm()

        With Me._Frm
            '.txtCustCD.TextValue = "00630"

            ''名称を取得し、ラベルに表示を行う
            'Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCD.TextValue _
            '                                                                                            , "' AND CUST_CD_M = '00'"))
            'If 0 < dr.Length Then
            '    .lblCustNM.TextValue = dr(0).Item("CUST_NM_L").ToString()
            'End If

        End With

    End Sub

#End Region

#Region "フォーカス設定"

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .imdPrintDate_From.Focus()
        End With

    End Sub

#End Region

#End Region

#End Region

End Class
