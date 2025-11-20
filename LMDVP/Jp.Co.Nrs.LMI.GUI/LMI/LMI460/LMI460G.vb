' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI460  : ローム　請求先コード変更
'  作  成  者       :  [馬野]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI460Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI460G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI460F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI460F)

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

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True
        Dim edit As Boolean = False
        Dim view As Boolean = False
        Dim lock As Boolean = False

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = "実　行"
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = String.Empty
            .F10ButtonName = LMIControlC.FUNCTION_POP
            .F11ButtonName = String.Empty
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
            .F10ButtonEnabled = always
            .F11ButtonEnabled = lock
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region 'FunctionKey

#Region "Mode&Status"

    ''' <summary>
    ''' Dispモードとレコードステータスの設定
    ''' </summary>
    ''' <param name="mode">Dispモード</param>
    ''' <param name="status">レコードステータス</param>
    ''' <remarks></remarks>
    Friend Sub SetModeAndStatus(ByVal mode As String, ByVal status As String)

        With Me._Frm

        End With

    End Sub

#End Region 'Mode&Status

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpOutputTani.TabIndex = LMI460C.CtlTabIndex.GRPOUTPUTTANI
            '.optOneNrsBr.TabIndex = LMI460C.CtlTabIndex.ONEEIGYO
            '.optOneCust.TabIndex = LMI460C.CtlTabIndex.ONECUST
            .cmbEigyo.TabIndex = LMI460C.CtlTabIndex.CMBEIGYO
            .txtCustCdL.TabIndex = LMI460C.CtlTabIndex.TXTCUSTCDL
            .txtCustCdM.TabIndex = LMI460C.CtlTabIndex.TXTCUSTCDM
            .grpOutputTaisyo.TabIndex = LMI460C.CtlTabIndex.GRPOUTPUTTAISYO
            '.optAllJisseki.TabIndex = LMI460C.CtlTabIndex.ALLJISSEKI
            '.optOutkaPlanJisseki.TabIndex = LMI460C.CtlTabIndex.OUTKAPLANJISSEKI
            .imdOutkaDateFrom.TabIndex = LMI460C.CtlTabIndex.OUTKAPLANDATEFROM
            .imdOutkaDateTo.TabIndex = LMI460C.CtlTabIndex.OUTKAPLANDATETO
            '.grpJikko.TabIndex = LMI460C.CtlTabIndex.GRPJIKKO
            '.cmbJikko.TabIndex = LMI460C.CtlTabIndex.CMBJIKKO
            '.btnJikko.TabIndex = LMI460C.CtlTabIndex.BTNJIKKO

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal sysDate As String)

        With Me._Frm
            '自営業所の設定
            Dim brCd As String = LMUserInfoManager.GetNrsBrCd()
            .cmbEigyo.SelectedValue = brCd

            '出荷予定日の設定
            .imdOutkaDateFrom.TextValue = String.Concat(Left(sysDate, 6), "01")

            Dim tmpdate As Date = Date.Parse(Format(Convert.ToDecimal(String.Concat(Left(sysDate, 6), "01")), "0000/00/00"))
            .imdOutkaDateTo.TextValue = Convert.ToString(DateAdd("d", -1, DateAdd("m", 1, tmpdate)).ToString("yyyyMMdd"))

            '初期荷主の設定

            Select Case Convert.ToString(.cmbEigyo.SelectedValue)

                Case LMI460C.NRS_YKO
                    .txtCustCdL.TextValue = LMI460C.DEF_CUST_CD_L_YKO
                    .txtCustCdM.TextValue = LMI460C.DEF_CUST_CD_M_YKO

                Case Else


            End Select


            '名称を取得し、ラベルに表示を行う
            Dim dr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                                                          "NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND " _
                                                                                                        , "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND " _
                                                                                                        , "CUST_CD_M = '", .txtCustCdM.TextValue, "'" _
                                                                                           ))
            If 0 < dr.Length Then
                .lblCustNmL.TextValue = dr(0).Item("CUST_NM_L").ToString()
                .lblCustNmM.TextValue = dr(0).Item("CUST_NM_M").ToString()
            End If

        End With




    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Me._Frm.cmbEigyo.Enabled = False
        Me._Frm.txtCustCdL.Enabled = False
        Me._Frm.txtCustCdM.Enabled = False
        Me._Frm.imdOutkaDateFrom.Enabled = True
        Me._Frm.imdOutkaDateTo.Enabled = True

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .cmbEigyo.Focus()

        End With

    End Sub

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ClearControl()

        With Me._Frm

        End With

    End Sub

#End Region

#Region "Spread"

#End Region

#Region "ユーティリティ"

#Region "プロパティ"


#End Region

#Region "前埋め設定"

    ''' <summary>
    ''' 前埋め設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="value2">前埋めする文字</param>
    ''' <param name="keta">桁数</param>
    ''' <returns>前埋めした値</returns>
    ''' <remarks></remarks>
    Friend Function MaeCoverData(ByVal value As String, _
                                 ByVal value2 As String, _
                                 ByVal keta As Integer) As String

        For i As Integer = value.Length To keta - 1
            value = String.Concat(value2, value)
        Next

        Return value

    End Function

#End Region

#End Region

#End Region

End Class
