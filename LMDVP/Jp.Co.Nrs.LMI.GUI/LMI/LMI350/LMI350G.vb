' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI350G : 保管荷役明細(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Base
Imports GrapeCity.Win.Editors.Fields

''' <summary>
''' LMI350Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI350G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI350F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI350F)

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
            .F5ButtonName = empty
            .F6ButtonName = empty
            .F7ButtonName = LMIControlC.FUNCTION_PRINT
            .F8ButtonName = empty
            .F9ButtonName = empty
            .F10ButtonName = LMIControlC.FUNCTION_POP
            .F11ButtonName = empty
            .F12ButtonName = LMIControlC.FUNCTION_CLOSE

            'ファンクションキーの制御
            .F1ButtonEnabled = lock
            .F2ButtonEnabled = lock
            .F3ButtonEnabled = lock
            .F4ButtonEnabled = lock
            .F5ButtonEnabled = lock
            .F6ButtonEnabled = lock
            .F7ButtonEnabled = always
            .F8ButtonEnabled = lock
            .F9ButtonEnabled = lock
            .F10ButtonEnabled = always
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

            .cmbBr.TabIndex = LMI350C.CtlTabIndex.CmbBr
            .txtCustCdL.TabIndex = LMI350C.CtlTabIndex.CustCdL
            .lblCustNmL.TabIndex = LMI350C.CtlTabIndex.CustNmL
            .txtCustCdM.TabIndex = LMI350C.CtlTabIndex.CustCdM
            .lblCustNmM.TabIndex = LMI350C.CtlTabIndex.CustNmM
            .txtCustCdS.TabIndex = LMI350C.CtlTabIndex.CustCdS
            .lblCustNmS.TabIndex = LMI350C.CtlTabIndex.CustNmS
            .txtSeiqCd.TabIndex = LMI350C.CtlTabIndex.CustCdM
            .lblSeiqNm.TabIndex = LMI350C.CtlTabIndex.CustNmM
            .imdSeiqDate.TabIndex = LMI350C.CtlTabIndex.SeiqDate

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal data As String)

        '編集部の項目をクリア
        Call Me.ClearControl()

        'コントロールの日付書式設定
        Call Me.SetDateFormat()

        '自営業所、請求月設定
        Call Me.SetInput(data)

        '荷主、請求先設定
        Call Me.SetPrmData()

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl()

        With Me._Frm

            .cmbBr.SelectedValue = Nothing
            .txtCustCdL.TextValue = String.Empty
            .lblCustNmL.TextValue = String.Empty
            .txtSeiqCd.TextValue = String.Empty
            .lblSeiqNm.TextValue = String.Empty
            .imdSeiqDate.TextValue = String.Empty

        End With

    End Sub

    ''' <summary>
    '''営業所、当月設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetInput(ByVal data As String)

        With Me._Frm

            '営業所
            .cmbBr.SelectedValue = LMUserInfoManager.GetNrsBrCd()

            '請求月
            .imdSeiqDate.TextValue = String.Concat(Left(data, 6))

            '当月の最終日取得
            '.imdDateTo.TextValue = nextDate.AddMonths(1).AddDays(-1).ToString("yyyyMMdd")

        End With

    End Sub

    ''' <summary>
    '''初期荷主の設定 
    ''' </summary>
    ''' <remarks></remarks> 
    Friend Sub SetPrmData()

        With Me._Frm

            .txtCustCdL.TextValue = "00001"
            .txtCustCdM.TextValue = "00"
            .txtCustCdS.TextValue = "74"
            .txtSeiqCd.TextValue = "3187603"

            '名称を取得し、ラベルに表示を行う
            Dim CustDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue, "' AND " _
                                                                                                            , "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
                                                                                                            , "CUST_CD_M = '", .txtCustCdM.TextValue, "' AND " _
                                                                                                            , "CUST_CD_S = '", .txtCustCdS.TextValue, "'"))
            If 0 < CustDr.Length Then
                .lblCustNmL.TextValue = CustDr(0).Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = CustDr(0).Item("CUST_NM_M").ToString()
                .lblCustNmS.TextValue = CustDr(0).Item("CUST_NM_S").ToString()
            End If

            Dim SeiqDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("NRS_BR_CD = '", .cmbBr.SelectedValue, "' AND " _
                                                                                                              , "SEIQTO_CD = '", .txtSeiqCd.TextValue, "'"))
            If 0 < SeiqDr.Length Then
                .lblSeiqNm.TextValue = SeiqDr(0).Item("SEIQTO_NM").ToString()
            End If

        End With

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetDateFormat()

        Me._Frm.imdSeiqDate.Format = DateFieldsBuilder.BuildFields("yyyyMM")
        Me._Frm.imdSeiqDate.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM")

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm
            .cmbBr.Focus()
        End With

    End Sub

#End Region

#End Region

#End Region

End Class
