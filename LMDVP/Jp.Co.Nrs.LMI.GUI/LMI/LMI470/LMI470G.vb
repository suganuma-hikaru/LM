' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特殊荷主機能
'  プログラムID     :  LMI470  : 日本合成　物流費送信
'  作  成  者       :  [DAIKOKU]
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
''' LMI470Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMI470G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI470F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI470F)

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

            .grpOutputTani.TabIndex = LMI470C.CtlTabIndex.GRPOUTPUTTANI
            .cmbEigyo.TabIndex = LMI470C.CtlTabIndex.CMBEIGYO
            .txtSeikyuCd.TabIndex = LMI470C.CtlTabIndex.TXTCUSTCDL
            .grpOutputTaisyo.TabIndex = LMI470C.CtlTabIndex.GRPOUTPUTTAISYO
            .imdOutkaDateFrom.TabIndex = LMI470C.CtlTabIndex.OUTKAPLANDATEFROM
            .imdOutkaDateTo.TabIndex = LMI470C.CtlTabIndex.OUTKAPLANDATETO

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

            '請求期間の設定(システム日付の前月１日から末日をセット)
            Dim tmpdateFrom As Date = Date.Parse(Format(Convert.ToDecimal(String.Concat(Left(sysDate, 6), "01")), "0000/00/00"))
            .imdOutkaDateFrom.TextValue = Convert.ToString(DateAdd("M", -1, tmpdateFrom).ToString("yyyyMMdd"))

            '前月末日
            Dim tmpdateto As Date = Date.Parse(Format(Convert.ToDecimal(String.Concat(Left(sysDate, 6), "01")), "0000/00/00"))
            .imdOutkaDateTo.TextValue = Convert.ToString(DateAdd("d", -1, tmpdateto).ToString("yyyyMMdd"))

            '初期請求先を設定
            Select Case Convert.ToString(.cmbEigyo.SelectedValue)

                Case LMI470C.NRS_60
                    .txtSeikyuCd.TextValue = LMI470C.DEF_SEIKYU_CD_L_60

                Case Else


            End Select


            '名称を取得し、ラベルに表示を行う
            Dim SeiqDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, "' AND " _
                                                                                                   , "SEIQTO_CD = '", .txtSeikyuCd.TextValue, "'"))
            If 0 < SeiqDr.Length Then
                .lblSeikyuNm.TextValue = SeiqDr(0).Item("SEIQTO_NM").ToString()
            End If

        End With




    End Sub

    ''' <summary>
    ''' コントロールの入力制御
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControlsStatus()

        Me._Frm.cmbEigyo.Enabled = False
        Me._Frm.txtSeikyuCd.Enabled = False
        Me._Frm.imdOutkaDateFrom.Enabled = True
        Me._Frm.imdOutkaDateTo.Enabled = True
        Me._Frm.numKingaku.Enabled = True

    End Sub

    ''' <summary>
    ''' フォーカスの設定(項目チェック・保存エラー時を除く)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        With Me._Frm

            .numKingaku.Focus()

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
