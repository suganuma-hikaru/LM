' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMM     : マスタ
'  プログラムID     :  LMM090V : 荷主マスタメンテナンス
'  作  成  者       :  [熊本史子]
' ==========================================================================
Option Strict On
Option Explicit On

Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMM090Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMM090V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM090F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMMControlV

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM090F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM090C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM090C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM090C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM090C.EventShubetsu.FUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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


            Case LMM090C.EventShubetsu.SAKUJO_HUKKATU          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM090C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
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


            Case LMM090C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM090C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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

            Case LMM090C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM090C.EventShubetsu.DOUBLE_CLICK         'ダブルクリック
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
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

            Case LMM090C.EventShubetsu.ENTER          'Enter
                '50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW      '10:閲覧者
                        kengenFlg = True
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        If kengenFlg = True Then
            Return True
        Else
            MyBase.ShowMessage("E016")
        End If

    End Function

    ''' <summary>
    ''' 編集押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsHenshuChk(ByVal clickObj As LMM090C.ClickObject) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "編集"
        Dim msg As String = Me._Frm.FunctionKey.F2ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtCustCdL.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        'データ存在チェックを行う
        If Me._ControlV.IsRecordStatusChk(Me._Frm.lblSituation.RecordStatus) = False Then
            Return False
        End If

        '影響チェック
        If clickObj = LMM090C.ClickObject.CUST_SS Then
            Return True
        Else
            If MyBase.ShowMessage("W132") = MsgBoxResult.Ok Then
                Return True
            Else                
                Me.SetBaseMsg() '基本メッセージの設定
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 複写押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsFukushaChk() As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "複写"
        Dim msg As String = Me._Frm.FunctionKey.F3ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtCustCdL.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        '親荷主存在チェックを行う
        Dim dr As DataRow() = Nothing
        Me._ControlV.SelectCustListDataRow(dr, Me._Frm.txtCustCdL.TextValue, "00", "00", "00")
        If dr.Length = 0 Then
            MyBase.ShowMessage("E372")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 削除/復活押下時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSakujoHukkatuChk() As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim msg As String = "削除・復活"
        Dim msg As String = Me._Frm.FunctionKey.F4ButtonName
        '2016.01.06 UMANO 英語化対応END

        'データ存在チェックを行う
        If Me._ControlV.IsExistDataChk(Me._Frm, Me._Frm.txtCustCdL.TextValue) = False Then
            Return False
        End If

        '他営業所チェック
        If Me._ControlV.IsUserNrsBrCdChk(Me._Frm.cmbBr.SelectedValue.ToString(), msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'スペース除去
        Call Me.TrinmFindRow()

        '単項目チェック
        If Me.IsKensakuSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As String) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'Return Me._ControlV.SetFocusErrMessage()
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM090C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._ControlV.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm
                Case .txtMainCustCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtMainCustCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblMainCustNm}
                    msg = New String() {.lblTitleMainCust.Text}
                Case .txtSampleSagyoCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtSampleSagyoCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSampleSagyoNm}
                    msg = New String() {.lblTitleSampleSagyo.Text}
                Case .txtZipNo.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtZipNo}
                    msg = New String() {.lblTitleZipNo.Text}
                Case .txtCustKyoriCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustKyoriCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustKyoriNm}
                    msg = New String() {.lblTitleCustKyoriCd.Text}
                Case .txtUnchinTarifTonNyuka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTarifTonNyuka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTarifTonNyuka}
                    msg = New String() {.lblTitleUnchinTarifTonNyuka.Text}
                Case .txtUnchinTarifShadateNyuka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTarifShadateNyuka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTarifShadateNyuka}
                    msg = New String() {.lblTitleUnchinTarifShadateNyuka.Text}
                Case .txtWarimashiTarifNyuka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtWarimashiTarifNyuka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblWarimashiTarifNyuka}
                    msg = New String() {.lblTitleWarimashiTarifNyuka.Text}
                Case .txtYokomochiTarifNyuka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtYokomochiTarifNyuka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblYokomochiTarifNyuka}
                    msg = New String() {.lblTitleYokomochiTarifNyuka.Text}
                Case .txtUnchinTarifTonShukka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTarifTonShukka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTarifTonShukka}
                    msg = New String() {.lblTitleUnchinTarifTonShukka.Text}
                Case .txtUnchinTarifShadateShukka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTarifShadateShukka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTarifShadateShukka}
                    msg = New String() {.lblTitleUnchinTarifShadateShukka.Text}
                Case .txtWarimashiTarifShukka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtWarimashiTarifShukka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblWarimashiTarifShukka}
                    msg = New String() {.lblTitleWarimashiTarifShukka.Text}
                Case .txtYokomochiTarifShukka.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtYokomochiTarifShukka}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblYokomochiTarifShukka}
                    msg = New String() {.lblTitleYokomochiTarifShukka.Text}
                Case .txtShiteiUnsoCompCd.Name _
                    , .txtShiteiUnsoShitenCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtShiteiUnsoCompCd, .txtShiteiUnsoShitenCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblShiteiUnsoCompNm}
                    '2016.01.06 UMANO 英語化対応START
                    'msg = New String() {"指定運送会社コード", "指定運送会社支店コード"}
                    msg = New String() {.lblTitleShiteiUnsoComp.Text, .lblTitleShiteiUnsoComp.Text}
                    '2016.01.06 UMANO 英語化対応END
                    'ADD Start 2018/10/25 要望番号001820
                Case .txtInkaOrigCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtInkaOrigCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblInkaOrigNm}
                    msg = New String() {.lblTitleInkaOrigCd.Text}
                    'ADD End   2018/10/25 要望番号001820
                Case .txtSeiqCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtSeiqCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSeiqNm}
                    msg = New String() {.lblTitleSeiqCd.Text}
                Case .txtHokanSeiqCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtHokanSeiqCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblHokanSeiqNm}
                    msg = New String() {.lblTitleHokanSeiqCd.Text}
                Case .txtNiyakuSeiqCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtNiyakuSeiqCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblNiyakuSeiqNm}
                    msg = New String() {.lblTitleNiyakuSeiqCd.Text}
                Case .txtUnchinSeiqCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinSeiqCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinSeiqNm}
                    msg = New String() {.lblTitleUnchinSeiqCd.Text}
                Case .txtSagyoSeiqCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtSagyoSeiqCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSagyoSeiqNm}
                    msg = New String() {.lblTitleSagyoSeiqCd.Text}
                    'START YANAI 要望番号824
                Case .txtTantoCd.Name
                    ctl = New Win.InputMan.LMImTextBox() {.txtTantoCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblTantoNm}
                    msg = New String() {.txtTantoCd.Text}
                    'END YANAI 要望番号824
                Case .txtTcustBpCd.Name
                    ctl = New Win.InputMan.LMImTextBox() { .txtTcustBpCd}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() { .lblTcustBpNm}
                    msg = New String() { .txtTcustBpCd.Text}

            End Select

            Dim focusCtl As Control = Me._Frm.ActiveControl
            Return Me._ControlV.IsFocusChk(actionType, ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    ''' <summary>
    ''' 保存押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSaveChk(ByVal dateStr As String _
                              , ByVal clickObj As LMM090C.ClickObject _
                              , ByVal beforeDate As String) As Boolean

        With Me._Frm

            'スペース除去
            Call Me._ControlV.TrimSpaceTextvalue(.tabCust)

            '要望番号:349 yamanaka 2012.07.31 Start
            'スプレッド内スペース除去
            Select Case clickObj
                '新規登録・複写の場合
                Case LMM090C.ClickObject.INIT
                    Me.TrimSprDtlRow(.sprCustDetailL)
                    Me.TrimSprDtlRow(.sprCustDetailM)
                    Me.TrimSprDtlRow(.sprCustDetailS)
                    Me.TrimSprDtlRow(.sprCustDetailSS)
                    '大タブ編集時
                Case LMM090C.ClickObject.CUST_L
                    Me.TrimSprDtlRow(.sprCustDetailL)
                    '中タブ編集時
                Case LMM090C.ClickObject.CUST_M
                    Me.TrimSprDtlRow(.sprCustDetailM)
                    '小タブ編集時
                Case LMM090C.ClickObject.CUST_S
                    Me.TrimSprDtlRow(.sprCustDetailS)
                    '極小タブ編集時
                Case LMM090C.ClickObject.CUST_SS
                    Me.TrimSprDtlRow(.sprCustDetailSS)
            End Select
            '要望番号:349 yamanaka 2012.07.31 End

            '単項目チェック
            If Me.IsSaveSingleChk(clickObj) = False Then
                Return False
            End If

            'マスタ存在チェック/関連チェック
            If Me.IsSaveExistMst(dateStr, clickObj, beforeDate) = False Then
                Return False
            End If

            '要望番号:349 yamanaka 2012.07.10 Start
            '用途区分の重複チェック
            Select Case clickObj
                '新規登録・複写の場合
                Case LMM090C.ClickObject.INIT
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailL, Me._Frm.tpgCustL) = False Then
                        Return False
                    End If
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailM, Me._Frm.tpgCustM) = False Then
                        Return False
                    End If
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailS, Me._Frm.tpgCustS) = False Then
                        Return False
                    End If
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailSS, Me._Frm.tpgCustSS) = False Then
                        Return False
                    End If
                    '大タブ編集時
                Case LMM090C.ClickObject.CUST_L
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailL, Me._Frm.tpgCustL) = False Then
                        Return False
                    End If
                    '中タブ編集時
                Case LMM090C.ClickObject.CUST_M
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailM, Me._Frm.tpgCustM) = False Then
                        Return False
                    End If
                    '小タブ編集時
                Case LMM090C.ClickObject.CUST_S
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailS, Me._Frm.tpgCustS) = False Then
                        Return False
                    End If
                    '極小タブ編集時
                Case LMM090C.ClickObject.CUST_SS
                    If Me.ExistCustDetail(Me._Frm.sprCustDetailSS, Me._Frm.tpgCustSS) = False Then
                        Return False
                    End If
                Case Else
                    '処理なし
            End Select
            '要望番号:349 yamanaka 2012.07.10 End

        End With

        Return True

    End Function

    ''' <summary>
    ''' 行追加時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsAddRowChk() As Boolean

        '空行チェック
        If Me.IsKuranChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行削除時チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDelRowChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If list.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    ''' 荷主明細用行追加時チェック(共通)
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsDetailAddRowChk(ByVal maxEda As Integer, ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread) As Boolean

        '空行チェック
        If Me.IsDetailKuranChk(spr) = False Then
            Return False
        End If

        '上限チェック
        maxEda = maxEda + 1
        '2016.01.06 UMANO 英語化対応START
        'If Me._ControlV.IsMaxChk(maxEda, 99, "荷主KEY枝番") = False Then
        If Me._ControlV.IsMaxChk(maxEda, 99, LMM090G.sprCustDetailL.EDA_NO.ColName) = False Then
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        Return True

    End Function
    '要望番号:349 yamanaka 2012.07.10 End

    ''' <summary>
    ''' 基本メッセージ設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetBaseMsg()

        Select Case Me._Frm.lblSituation.DispMode
            Case DispMode.INIT

                MyBase.ShowMessage("G007")

            Case DispMode.VIEW

                MyBase.ShowMessage("G013")

            Case DispMode.EDIT

                MyBase.ShowMessage("G003")

        End Select

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 検索行のTrim処理を行う
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrinmFindRow()

        With Me._Frm.sprCust
            Call Me._ControlV.TrimSpaceSprTextvalue(Me._Frm.sprCust, 0)
            'Trimをかける
            Dim custCd As String = Me._ControlV.GetCellValue(.ActiveSheet.Cells(0, LMM090G.sprCustDef.CUST_CD.ColNo)).Replace("-", "")
            '荷主コードの"-"を削除する
            .SetCellValue(0, LMM090G.sprCustDef.CUST_CD.ColNo, custCd)
        End With

    End Sub

    ''' <summary>
    ''' 明細行のTrim処理を行う
    ''' </summary>
    ''' <param name="spr">Spread</param>
    ''' <remarks></remarks>
    Private Sub TrimSprDtlRow(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

        Dim sprRow As Integer = spr.ActiveSheet.RowCount - 1

        'Trimをかける
        For i As Integer = 0 To sprRow
            Call Me._ControlV.TrimSpaceSprTextvalue(spr, i)
        Next

    End Sub
    ''' <summary>
    ''' 検索押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleChk() As Boolean

        With Me._Frm

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprCust)

            '【荷主コード】
            vCell.SetValidateCell(0, LMM090G.sprCustDef.CUST_CD.ColNo)
            vCell.ItemName = LMM090G.sprCustDef.CUST_CD.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 11
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(大)】
            vCell.SetValidateCell(0, LMM090G.sprCustDef.CUST_NM_L.ColNo)
            vCell.ItemName = LMM090G.sprCustDef.CUST_NM_L.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(中)】
            vCell.SetValidateCell(0, LMM090G.sprCustDef.CUST_NM_M.ColNo)
            vCell.ItemName = LMM090G.sprCustDef.CUST_NM_M.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(小)】
            vCell.SetValidateCell(0, LMM090G.sprCustDef.CUST_NM_S.ColNo)
            vCell.ItemName = LMM090G.sprCustDef.CUST_NM_S.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '【荷主名(極小)】
            vCell.SetValidateCell(0, LMM090G.sprCustDef.CUST_NM_SS.ColNo)
            vCell.ItemName = LMM090G.sprCustDef.CUST_NM_SS.ColName
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleChk(ByVal clickObj As LMM090C.ClickObject) As Boolean

        With Me._Frm

            '******************** 編集部 ********************

            '【営業所】
            .cmbBr.ItemName = .lblTitleBr.Text
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '******************** 荷主コード(大)タブ ********************

            Dim tpg As System.Windows.Forms.TabPage = .tpgCustL

            '【荷主コード(大)】
            .txtCustCdL.ItemName = .lblTitleCustL.Text
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustCdL, .tabCust, tpg)
                Return False
            End If

            '【荷主名(大)】
            .txtCustNmL.ItemName = .lblTitleCustL.Text
            .txtCustNmL.IsHissuCheck = True
            .txtCustNmL.IsForbiddenWordsCheck = True
            .txtCustNmL.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtCustNmL) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustNmL, .tabCust, tpg)
                Return False
            End If

            '【本荷主コード(大)】
            .txtMainCustCd.ItemName = .lblTitleMainCust.Text
            .txtMainCustCd.IsForbiddenWordsCheck = True
            .txtMainCustCd.IsFullByteCheck = 5
            .txtMainCustCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtMainCustCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtMainCustCd, .tabCust, tpg)
                Return False
            End If

            '【サンプル作業コード】
            .txtSampleSagyoCd.ItemName = .lblTitleSampleSagyo.Text
            .txtSampleSagyoCd.IsForbiddenWordsCheck = True
            .txtSampleSagyoCd.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSampleSagyoCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtSampleSagyoCd, .tabCust, tpg)
                Return False
            End If

            '【製品セグメント】
            If .cmbProductSegCd.HissuLabelVisible Then
                '外部倉庫用ABP対策として必須マークがある場合のみ
                .cmbProductSegCd.ItemName = .lblTitleProductSeg.Text
                .cmbProductSegCd.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbProductSegCd) = False Then
                    Return False
                End If
            End If


            '******************** 荷主コード(中)タブ ********************

            tpg = .tpgCustM

            '【荷主名(中)】
            .txtCustNmM.ItemName = .lblTitleCustM.Text
            .txtCustNmM.IsForbiddenWordsCheck = True
            .txtCustNmM.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtCustNmM) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustNmM, .tabCust, tpg)
                Return False
            End If

            'START YANAI 要望番号824
            '【担当者】
            .txtTantoCd.ItemName = .lblTitleTanto.Text
            .txtTantoCd.IsHissuCheck = True
            .txtTantoCd.IsForbiddenWordsCheck = True
            .txtTantoCd.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtTantoCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtTantoCd, .tabCust, tpg)
                Return False
            End If
            Dim userDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", .txtTantoCd.TextValue, "'"))
            If 0 = userDr.Length Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"ユーザーマスタ", "担当者コード"})
                MyBase.ShowMessage("E871")
                '2016.01.06 UMANO 英語化対応END
                Call Me._ControlV.SetErrorControl(.txtTantoCd, .tabCust, tpg)
                Return False
            End If

            'END YANAI 要望番号824

            '【郵便番号】
            .txtZipNo.ItemName = .lblTitleZipNo.Text
            .txtZipNo.IsForbiddenWordsCheck = True
            .txtZipNo.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtZipNo) = False Then
                Call Me._ControlV.SetErrorControl(.txtZipNo, .tabCust, tpg)
                Return False
            End If

            '【住所1】
            .txtAdd1.ItemName = .lblTitleAdd1.Text
            .txtAdd1.IsForbiddenWordsCheck = True
            .txtAdd1.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAdd1) = False Then
                Call Me._ControlV.SetErrorControl(.txtAdd1, .tabCust, tpg)
                Return False
            End If

            '【住所2】
            .txtAdd2.ItemName = .lblTitleAdd2.Text
            .txtAdd2.IsForbiddenWordsCheck = True
            .txtAdd2.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAdd2) = False Then
                Call Me._ControlV.SetErrorControl(.txtAdd2, .tabCust, tpg)
                Return False
            End If

            '【住所3】
            .txtAdd3.ItemName = .lblTitleAdd3.Text
            .txtAdd3.IsForbiddenWordsCheck = True
            .txtAdd3.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAdd3) = False Then
                Call Me._ControlV.SetErrorControl(.txtAdd3, .tabCust, tpg)
                Return False
            End If

            '【契約通貨コード】
            .cmbItemCurrCd.ItemName = .lblTitleSeiqCurrCd.Text
            .cmbItemCurrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbItemCurrCd) = False Then
                Call Me._ControlV.SetErrorControl(.cmbItemCurrCd, .tabCust, tpg)
                Return False
            End If

#If True Then   'ADD 2020/02/10 010831【LMS】鑑検編集で「取込」実施時にシステムエラー
            '【課税区分】
            .cmbKazeiKbn.ItemName = .lblTitleKazeiKbn.Text
            .cmbKazeiKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbKazeiKbn) = False Then
                Call Me._ControlV.SetErrorControl(.cmbKazeiKbn, .tabCust, tpg)
                Return False
            End If
#End If
            '【デフォルト倉庫】
            .cmbSoko.ItemName = .lblTitleSoko.Text
            .cmbSoko.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSoko) = False Then
                Call Me._ControlV.SetErrorControl(.cmbSoko, .tabCust, tpg)
                Return False
            End If
            '【荷主別距離程】
            .txtCustKyoriCd.ItemName = .lblTitleCustKyoriCd.Text
            .txtCustKyoriCd.IsHissuCheck = True
            .txtCustKyoriCd.IsForbiddenWordsCheck = True
            .txtCustKyoriCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtCustKyoriCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustKyoriCd, .tabCust, tpg)
                Return False
            End If
#If True Then       'ADD 2019/07/10 002520
            '適用
            .txtTekiyo.ItemName = .TitleltxtTekiyo.Text()
            .txtTekiyo.IsForbiddenWordsCheck = True
            .txtTekiyo.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtTekiyo) = False Then
                Return False
            End If

#End If


            '【運賃タリフコード(トンキロ建) 入荷】
            .txtUnchinTarifTonNyuka.ItemName = .lblTitleUnchinTarifTonNyuka.Text
            .txtUnchinTarifTonNyuka.IsForbiddenWordsCheck = True
            .txtUnchinTarifTonNyuka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTarifTonNyuka) = False Then
                Call Me._ControlV.SetErrorControl(.txtUnchinTarifTonNyuka, .tabCust, tpg)
                Return False
            End If

            '【運賃タリフコード(車建) 入荷】
            .txtUnchinTarifShadateNyuka.ItemName = .lblTitleUnchinTarifShadateNyuka.Text
            .txtUnchinTarifShadateNyuka.IsForbiddenWordsCheck = True
            .txtUnchinTarifShadateNyuka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTarifShadateNyuka) = False Then
                Call Me._ControlV.SetErrorControl(.txtUnchinTarifShadateNyuka, .tabCust, tpg)
                Return False
            End If

            '【割増タリフコード 入荷】
            .txtWarimashiTarifNyuka.ItemName = .lblTitleWarimashiTarifNyuka.Text
            .txtWarimashiTarifNyuka.IsForbiddenWordsCheck = True
            .txtWarimashiTarifNyuka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtWarimashiTarifNyuka) = False Then
                Call Me._ControlV.SetErrorControl(.txtWarimashiTarifNyuka, .tabCust, tpg)
                Return False
            End If

            '【横持ち運賃タリフコード 入荷】
            .txtYokomochiTarifNyuka.ItemName = .lblTitleYokomochiTarifNyuka.Text
            .txtYokomochiTarifNyuka.IsForbiddenWordsCheck = True
            .txtYokomochiTarifNyuka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtYokomochiTarifNyuka) = False Then
                Call Me._ControlV.SetErrorControl(.txtYokomochiTarifNyuka, .tabCust, tpg)
                Return False
            End If

            '【運賃タリフコード(トンキロ建) 出荷】
            .txtUnchinTarifTonShukka.ItemName = .lblTitleUnchinTarifTonShukka.Text
            .txtUnchinTarifTonShukka.IsForbiddenWordsCheck = True
            .txtUnchinTarifTonShukka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTarifTonShukka) = False Then
                Call Me._ControlV.SetErrorControl(.txtUnchinTarifTonShukka, .tabCust, tpg)
                Return False
            End If

            '【運賃タリフコード(車建) 出荷】
            .txtUnchinTarifShadateShukka.ItemName = .lblTitleUnchinTarifShadateShukka.Text
            .txtUnchinTarifShadateShukka.IsForbiddenWordsCheck = True
            .txtUnchinTarifShadateShukka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTarifShadateShukka) = False Then
                Call Me._ControlV.SetErrorControl(.txtUnchinTarifShadateShukka, .tabCust, tpg)
                Return False
            End If

            '【割増タリフコード 出荷】
            .txtWarimashiTarifShukka.ItemName = .lblTitleWarimashiTarifShukka.Text
            .txtWarimashiTarifShukka.IsForbiddenWordsCheck = True
            .txtWarimashiTarifShukka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtWarimashiTarifShukka) = False Then
                Call Me._ControlV.SetErrorControl(.txtWarimashiTarifShukka, .tabCust, tpg)
                Return False
            End If

            '【横持ち運賃タリフコード 出荷】
            .txtYokomochiTarifShukka.ItemName = .lblTitleYokomochiTarifShukka.Text
            .txtYokomochiTarifShukka.IsForbiddenWordsCheck = True
            .txtYokomochiTarifShukka.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtYokomochiTarifShukka) = False Then
                Call Me._ControlV.SetErrorControl(.txtYokomochiTarifShukka, .tabCust, tpg)
                Return False
            End If

            '【指定運送会社コード】
            .txtShiteiUnsoCompCd.ItemName = .lblTitleShiteiUnsoComp.Text
            .txtShiteiUnsoCompCd.IsForbiddenWordsCheck = True
            .txtShiteiUnsoCompCd.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtShiteiUnsoCompCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtShiteiUnsoCompCd, .tabCust, tpg)
                Return False
            End If

            '【指定運送会社支店コード】
            .txtShiteiUnsoShitenCd.ItemName = .lblTitleShiteiUnsoComp.Text
            .txtShiteiUnsoShitenCd.IsForbiddenWordsCheck = True
            .txtShiteiUnsoShitenCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtShiteiUnsoShitenCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtShiteiUnsoShitenCd, .tabCust, tpg)
                Return False
            End If

            'ADD Start 2018/10/25 要望番号001820
            '【入荷出荷元コード】
            .txtInkaOrigCd.ItemName = .lblTitleInkaOrigCd.Text
            .txtInkaOrigCd.IsForbiddenWordsCheck = True
            .txtInkaOrigCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtInkaOrigCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtInkaOrigCd, .tabCust, tpg)
                Return False
            End If
            'ADD End   2018/10/25 要望番号001820

            '【入荷報告区分】
            .cmbNyukaHokoku.ItemName = .lblTitleNyukaHokoku.Text
            .cmbNyukaHokoku.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNyukaHokoku) = False Then
                Call Me._ControlV.SetErrorControl(.cmbNyukaHokoku, .tabCust, tpg)
                Return False
            End If

            '【在庫報告区分】
            .cmbZaikoHokoku.ItemName = .lblTitleZaikoHokoku.Text
            .cmbZaikoHokoku.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbZaikoHokoku) = False Then
                Call Me._ControlV.SetErrorControl(.cmbZaikoHokoku, .tabCust, tpg)
                Return False
            End If

            '【出荷報告区分】
            .cmbShukkaHokoku.ItemName = .lblTitleShukkaHokoku.Text
            .cmbShukkaHokoku.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbShukkaHokoku) = False Then
                Call Me._ControlV.SetErrorControl(.cmbShukkaHokoku, .tabCust, tpg)
                Return False
            End If

            '******************** 荷主コード(小)タブ ********************

            tpg = .tpgCustS

            '【荷主名(小)】
            .txtCustNmS.ItemName = .lblTitleCustS.Text
            .txtCustNmS.IsForbiddenWordsCheck = True
            .txtCustNmS.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtCustNmS) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustNmS, .tabCust, tpg)
                Return False
            End If

            '【荷主別名】
            .txtCustBetuNm.ItemName = .lblTitleCustBetuNm.Text
            .txtCustBetuNm.IsForbiddenWordsCheck = True
            .txtCustBetuNm.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtCustBetuNm) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustBetuNm, .tabCust, tpg)
                Return False
            End If

            '【真荷主コード】
            If .lblTcustBpNm.HissuLabelVisible Then
                '外部倉庫用ABP対策として必須マークがある場合のみ
                .txtTcustBpCd.ItemName = .lblTitleTcustBpCd.Text
                .txtTcustBpCd.IsHissuCheck = True
                .txtTcustBpCd.IsForbiddenWordsCheck = True
                .txtTcustBpCd.IsByteCheck = 10
                If MyBase.IsValidateCheck(.txtTcustBpCd) = False Then
                    Call Me._ControlV.SetErrorControl(.txtTcustBpCd, .tabCust, tpg)
                    Return False
                End If
            End If

            '【主請求先コード】
            .txtSeiqCd.ItemName = .lblTitleSeiqCd.Text
            .txtSeiqCd.IsHissuCheck = True
            .txtSeiqCd.IsForbiddenWordsCheck = True
            .txtSeiqCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtSeiqCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtSeiqCd, .tabCust, tpg)
                Return False
            End If

            '【保管料請求先コード】
            .txtHokanSeiqCd.ItemName = .lblTitleHokanSeiqCd.Text
            .txtHokanSeiqCd.IsHissuCheck = True
            .txtHokanSeiqCd.IsForbiddenWordsCheck = True
            .txtHokanSeiqCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtHokanSeiqCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtHokanSeiqCd, .tabCust, tpg)
                Return False
            End If

            '【荷役料請求先コード】
            .txtNiyakuSeiqCd.ItemName = .lblTitleNiyakuSeiqCd.Text
            .txtNiyakuSeiqCd.IsHissuCheck = True
            .txtNiyakuSeiqCd.IsForbiddenWordsCheck = True
            .txtNiyakuSeiqCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtNiyakuSeiqCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtNiyakuSeiqCd, .tabCust, tpg)
                Return False
            End If

            '【運賃請求先コード】
            .txtUnchinSeiqCd.ItemName = .lblTitleUnchinSeiqCd.Text
            .txtUnchinSeiqCd.IsHissuCheck = True
            .txtUnchinSeiqCd.IsForbiddenWordsCheck = True
            .txtUnchinSeiqCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtUnchinSeiqCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtUnchinSeiqCd, .tabCust, tpg)
                Return False
            End If

            '【作業料請求先コード】
            .txtSagyoSeiqCd.ItemName = .lblTitleSagyoSeiqCd.Text
            .txtSagyoSeiqCd.IsHissuCheck = True
            .txtSagyoSeiqCd.IsForbiddenWordsCheck = True
            .txtSagyoSeiqCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtSagyoSeiqCd) = False Then
                Call Me._ControlV.SetErrorControl(.txtSagyoSeiqCd, .tabCust, tpg)
                Return False
            End If

            '【運賃計算締め基準】
            .cmbUnchinCalc.ItemName = .lblTitleUnchinCalc.Text
            .cmbUnchinCalc.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbUnchinCalc) = False Then
                Call Me._ControlV.SetErrorControl(.cmbUnchinCalc, .tabCust, tpg)
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprCustPrt)
            Dim spr As Spread.LMSpread = .sprCustPrt
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            Dim id As String = String.Empty
            Dim chckId As String = String.Empty
            Dim colId As Integer = LMM090G.sprCustPrtDef.PTN_ID.ColNo
            For i As Integer = 0 To max
                '【帳票種類ID】
                vCell.SetValidateCell(i, colId)
                vCell.ItemName = LMM090G.sprCustPrtDef.PTN_ID.ColName
                vCell.IsHissuCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then
                    Call Me._ControlV.SetErrorControl(spr, i, colId, .tabCust, tpg)
                    Return False
                End If

                '同一チェック(同一の帳票種類IDが使用されている場合エラー)
                id = Me._ControlV.GetCellValue(.sprCustPrt.ActiveSheet.Cells(i, colId))
                If i <> max Then
                    For j As Integer = i + 1 To max
                        chckId = Me._ControlV.GetCellValue(.sprCustPrt.ActiveSheet.Cells(j, colId))
                        If id.Equals(chckId) Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E022", New String() {"帳票種類ID"})
                            MyBase.ShowMessage("E022", New String() {LMM090G.sprCustPrtDef.PTN_ID.ColName})
                            '2016.01.06 UMANO 英語化対応END
                            Call Me._ControlV.SetErrorControl(spr, New Integer() {i, j}, New Integer() {colId, colId}, .tabCust, tpg)
                            Return False
                        End If
                    Next
                End If
            Next



            '要望番号:349 yamanaka 2012.07.25 Start
            '******************** 荷主明細スプレッド(共通)チェック ********************
            Select Case clickObj
                '新規登録・複写時
                Case LMM090C.ClickObject.INIT
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailL, Me._Frm.tpgCustL) = False Then
                        Return False
                    End If
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailM, Me._Frm.tpgCustM) = False Then
                        Return False
                    End If
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailS, Me._Frm.tpgCustS) = False Then
                        Return False
                    End If
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailSS, Me._Frm.tpgCustSS) = False Then
                        Return False
                    End If
                    '大タブ編集時
                Case LMM090C.ClickObject.CUST_L
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailL, Me._Frm.tpgCustL) = False Then
                        Return False
                    End If
                    '中タブ編集時
                Case LMM090C.ClickObject.CUST_M
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailM, Me._Frm.tpgCustM) = False Then
                        Return False
                    End If
                    '小タブ編集時
                Case LMM090C.ClickObject.CUST_S
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailS, Me._Frm.tpgCustS) = False Then
                        Return False
                    End If
                    '極小タブ編集時
                Case LMM090C.ClickObject.CUST_SS
                    If Me.ChkCustDetail(Me._Frm.sprCustDetailSS, Me._Frm.tpgCustSS) = False Then
                        Return False
                    End If
                Case Else
                    '処理なし
            End Select

            '要望番号:349 yamanaka 2012.07.25 End

            '******************** 荷主コード(極小)タブ ********************

            tpg = .tpgCustSS

            '【荷主名(極小)】
            .txtCustNmSS.ItemName = .lblTitleCustSS.Text
            .txtCustNmSS.IsForbiddenWordsCheck = True
            .txtCustNmSS.IsByteCheck = 60
            If MyBase.IsValidateCheck(.txtCustNmSS) = False Then
                Call Me._ControlV.SetErrorControl(.txtCustNmSS, .tabCust, tpg)
                Return False
            End If

            '【主担当者】
            .txtMainTantoNm.ItemName = .lblTitleMainTantoNm.Text
            .txtMainTantoNm.IsForbiddenWordsCheck = True
            .txtMainTantoNm.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtMainTantoNm) = False Then
                Call Me._ControlV.SetErrorControl(.txtMainTantoNm, .tabCust, tpg)
                Return False
            End If

            '【副担当者】
            .txtSubTantoNm.ItemName = .lblTitleSubTantoNm.Text
            .txtSubTantoNm.IsForbiddenWordsCheck = True
            .txtSubTantoNm.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtSubTantoNm) = False Then
                Call Me._ControlV.SetErrorControl(.txtSubTantoNm, .tabCust, tpg)
                Return False
            End If

            '【電話番号】
            .txtTel.ItemName = .lblTitleTel.Text
            .txtTel.IsForbiddenWordsCheck = True
            .txtTel.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtTel) = False Then
                Call Me._ControlV.SetErrorControl(.txtTel, .tabCust, tpg)
                Return False
            End If

            '【FAX】
            .txtFax.ItemName = .lblTitleFax.Text
            .txtFax.IsForbiddenWordsCheck = True
            .txtFax.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtFax) = False Then
                Call Me._ControlV.SetErrorControl(.txtFax, .tabCust, tpg)
                Return False
            End If

            '【Emailアドレス】
            .txtEmail.ItemName = .lblTitleEmail.Text
            .txtEmail.IsForbiddenWordsCheck = True
            .txtEmail.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtEmail) = False Then
                Call Me._ControlV.SetErrorControl(.txtEmail, .tabCust, tpg)
                Return False
            End If

            '【保管・荷役料最終計算日】
            .imdLastCalc.ItemName = .lblTitleLastCalc.Text
            .imdLastCalc.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdLastCalc) = False Then
                Call Me._ControlV.SetErrorControl(.imdLastCalc, .tabCust, tpg)
                Return False
            End If
            If .imdLastCalc.IsDateFullByteCheck = False Then
                MyBase.ShowMessage("E038", New String() {.lblTitleLastCalc.Text, "8"})
                Call Me._ControlV.SetErrorControl(.imdLastCalc, .tabCust, tpg)
                Return False
            End If

            '【保管・荷役料計算有無】
            .cmbCalc.ItemName = .lblTitleCalc.Text
            .cmbCalc.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbCalc) = False Then
                Call Me._ControlV.SetErrorControl(.cmbCalc, .tabCust, tpg)
                Return False
            End If

            '【請求時薄外品取扱区分】
            .cmbSeiqHakugaiHinKbn.ItemName = .lblTitleSeiqHakugaiHinKbn.Text
            .cmbSeiqHakugaiHinKbn.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSeiqHakugaiHinKbn) = False Then
                Call Me._ControlV.SetErrorControl(.cmbSeiqHakugaiHinKbn, .tabCust, tpg)
                Return False
            End If


        End With

        Return True

    End Function

    ''要望番号:349 yamanaka 2012.07.25 Start
    ''' <summary>
    ''' 保存押下時明細スプレッドチェック
    ''' </summary>
    ''' <param name="spr">Spread</param>
    ''' <param name="tpg">TabPage</param>
    ''' <returns>True:エラーなし, False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkCustDetail(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread, ByVal tpg As System.Windows.Forms.TabPage) As Boolean
        Dim valCell As LMValidatableCells = New LMValidatableCells(spr)
        Dim maxRow As Integer = spr.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To maxRow

            '【用途区分】
            valCell.SetValidateCell(i, LMM090C.sprCustDetailColumnIndex.YOTO_KBN)
            valCell.ItemName = LMM090G.sprCustDetailL.YOTO_KBN.ColName
            valCell.IsHissuCheck = True
            If MyBase.IsValidateCheck(valCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                  , i _
                                                  , LMM090C.sprCustDetailColumnIndex.YOTO_KBN _
                                                  , Me._Frm.tabCust _
                                                  , tpg)
                Return False
            End If


            '【設定値】
            valCell.SetValidateCell(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE)
            valCell.ItemName = LMM090G.sprCustDetailL.SETTEI_VALUE.ColName
            valCell.IsForbiddenWordsCheck = True
            valCell.IsByteCheck = 100
            valCell.IsHissuCheck = True

            If MyBase.IsValidateCheck(valCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                  , i _
                                                  , LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE _
                                                  , Me._Frm.tabCust _
                                                  , tpg)
                Return False
            End If

            '【設定値2】
            valCell.SetValidateCell(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2)
            valCell.ItemName = LMM090G.sprCustDetailL.SETTEI_VALUE2.ColName
            valCell.IsForbiddenWordsCheck = True
            valCell.IsByteCheck = 100

            If MyBase.IsValidateCheck(valCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                  , i _
                                                  , LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2 _
                                                  , Me._Frm.tabCust _
                                                  , tpg)
                Return False
            End If

            '【設定値3】
            valCell.SetValidateCell(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3)
            valCell.ItemName = LMM090G.sprCustDetailL.SETTEI_VALUE3.ColName
            valCell.IsForbiddenWordsCheck = True
            valCell.IsByteCheck = 100

            If MyBase.IsValidateCheck(valCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                  , i _
                                                  , LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3 _
                                                  , Me._Frm.tabCust _
                                                  , tpg)
                Return False
            End If

            '【備考】
            valCell.SetValidateCell(i, LMM090C.sprCustDetailColumnIndex.BIKO)
            valCell.ItemName = LMM090G.sprCustDetailL.BIKO.ColName
            valCell.IsForbiddenWordsCheck = True
            valCell.IsByteCheck = 100

            If MyBase.IsValidateCheck(valCell) = False Then
                Call Me._ControlV.SetErrorControl(spr _
                                                  , i _
                                                  , LMM090C.sprCustDetailColumnIndex.BIKO _
                                                  , Me._Frm.tabCust _
                                                  , tpg)
                Return False
            End If
        Next
        Return True
    End Function
    ''要望番号:349 yamanaka 2012.07.25 End

    ''' <summary>
    ''' 過去日不可チェック
    ''' </summary>
    ''' <param name="clickObj">編集時押下ボタン</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkOldDate(ByVal clickObj As LMM090C.ClickObject, ByVal beforeDateStr As String) As Boolean

        With Me._Frm

            If .lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) _
            AndAlso clickObj.Equals(LMM090C.ClickObject.CUST_SS) Then

                '最終JOB番号が設定されている場合のみチェックを行う
                If String.IsNullOrEmpty(.lblLastJob.TextValue) = True Then
                    Return True
                End If

                Dim BeforeDate As Date = Date.ParseExact(beforeDateStr, "yyyyMMdd", Nothing)
                Dim EditDate As Date = Date.ParseExact(.imdLastCalc.TextValue, "yyyyMMdd", Nothing)

                If EditDate < BeforeDate Then
                    '2016.01.06 UMANO 英語化対応START
                    'Me._ControlV.SetErrMessage("E123", New String() {"請求計算が完了しているデータ", "保管・荷役料最終計算日に過去日"})
                    Me._ControlV.SetErrMessage("E872")
                    '2016.01.06 UMANO 英語化対応END
                    Me._ControlV.SetErrorControl(.imdLastCalc, .tabCust, .tpgCustSS)
                    Return False
                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時締日区分関連チェック
    ''' </summary>
    ''' <param name="hokanCloseKbn">保管料請求先の締日区分</param>
    ''' <param name="niyakuCloseKbn">荷役料請求先の締日区分</param>
    ''' <param name="clickObj">編集時押下ボタン</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkCloseKbn(ByVal hokanCloseKbn As String _
                                 , ByVal niyakuCloseKbn As String _
                                 , ByVal clickObj As LMM090C.ClickObject) As Boolean

        With Me._Frm

            '荷主小が活性時のみチェックを行う
            Select Case clickObj
                Case LMM090C.ClickObject.CUST_L _
                , LMM090C.ClickObject.CUST_SS
                    Return True
                Case LMM090C.ClickObject.CUST_M
                    If .lblSituation.RecordStatus.Equals(RecordStatus.NOMAL_REC) Then
                        Return True
                    End If
            End Select

            '保管料請求先締日区分と、荷役料請求先締日区分が異なる場合、ワーニング表示
            If hokanCloseKbn.Equals(niyakuCloseKbn) = False Then

                '2016.01.06 UMANO 英語化対応START
                'If MyBase.ShowMessage("W121", New String() {"保管料請求先の締日", "荷役料請求先の締日"}) = MsgBoxResult.Ok Then
                If MyBase.ShowMessage("W257") = MsgBoxResult.Ok Then
                    '2016.01.06 UMANO 英語化対応END
                    Return True
                Else
                    Me._ControlV.SetErrorControl(New Control() {.txtHokanSeiqCd, .txtNiyakuSeiqCd}, .txtHokanSeiqCd)
                    Me.SetBaseMsg() '基本メッセージの設定
                    Return False
                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    '''行追加時、Spreadに何も入力されていない場合、エラー
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKuranChk() As Boolean

        With Me._Frm.sprCustPrt.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            Dim colMax As Integer = .Columns.Count - 1
            Dim chkFlg As Boolean = False
            For i As Integer = 0 To rowMax
                For j As Integer = LMM090G.sprCustPrtDef.DEF.ColNo + 1 To colMax
                    If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, j))) = False Then
                        chkFlg = True
                        Exit For
                    End If
                Next
                If chkFlg = False Then
                    MyBase.ShowMessage("E219")
                    Return False
                End If

                '初期値設定
                chkFlg = False
            Next
        End With

        Return True

    End Function

    '要望番号:349 yamanaka 2012.07.10 Start
    ''' <summary>
    '''荷主明細行追加時、Spreadに何も入力されていない場合、エラー(共通)
    ''' </summary>
    ''' <param name="spr">Spread</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDetailKuranChk(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread) As Boolean

        With spr.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            For i As Integer = 0 To rowMax

                '用途区分チェック
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.YOTO_KBN))) = False Then
                    Continue For
                End If

                '設定値チェック
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE))) = False Then
                    Continue For
                End If

                '設定値2チェック
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE2))) = False Then
                    Continue For
                End If

                '設定値3チェック
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.SETTEI_VALUE3))) = False Then
                    Continue For
                End If

                '備考チェック
                If String.IsNullOrEmpty(Me._ControlV.GetCellValue(.Cells(i, LMM090C.sprCustDetailColumnIndex.BIKO))) = False Then
                    Continue For
                End If

                MyBase.ShowMessage("E219")
                Return False

            Next
        End With

        Return True

    End Function

    ''' <summary>
    '''重複チェック(共通)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ExistCustDetail(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread, ByVal tpg As System.Windows.Forms.TabPage) As Boolean
        Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
        Dim id As String = String.Empty
        Dim chckId As String = String.Empty
        Dim yotoKbnColmNo As Integer = LMM090C.sprCustDetailColumnIndex.YOTO_KBN

        '重複チェック(同一の用途区分が使用されている場合エラー)
        For i As Integer = 0 To max
            With spr.ActiveSheet

                id = Me._ControlV.GetCellValue(.Cells(i, yotoKbnColmNo))

                If i <> max Then
                    '20150803 tsunehira add
                    'フォーマットの複数登録対応
                    If id = "0A" Then
                        Continue For
                    End If
                    For j As Integer = i + 1 To max
                        chckId = Me._ControlV.GetCellValue(.Cells(j, yotoKbnColmNo))
                        If id.Equals(chckId) = True Then
                            MyBase.ShowMessage("E496", New String() {"用途区分", String.Concat("該当行:" _
                                                                                           , (i + 1).ToString(), "行目," _
                                                                                           , (j + 1).ToString(), "行目")})
                            Call Me._ControlV.SetErrorControl(spr, New Integer() {i, j} _
                                                              , New Integer() {yotoKbnColmNo, yotoKbnColmNo} _
                                                              , Me._Frm.tabCust, tpg)
                            Return False
                        End If
                    Next
                End If

            End With
        Next

        Return True

    End Function
    '要望番号:349 yamanaka 2012.07.10 End

#Region "マスタ存在チェック"

    ''' <summary>
    ''' 保存押下時マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveExistMst(ByVal dateStr As String _
                                    , ByVal clickObj As LMM090C.ClickObject _
                                    , ByVal beforeDate As String) As Boolean

        With Me._Frm

            '******************** 荷主コード(大)タブ ********************

            Dim tpg As System.Windows.Forms.TabPage = .tpgCustL

            '荷主マスタ存在チェック　　　　【本荷主】
            Dim rtnResult As Boolean = Me.IsCustExistChk(.txtMainCustCd, .lblMainCustNm, tpg)

            '作業項目マスタ存在チェック　　【サンプル作業】
            rtnResult = rtnResult AndAlso Me.IsSagyoKmkExistChk(.txtSampleSagyoCd, .lblSampleSagyoNm, tpg)

            '******************** 荷主コード(中)タブ ********************

            tpg = .tpgCustM

            '距離程マスタ存在チェック　　　    【荷主別距離程】
            rtnResult = rtnResult AndAlso Me.IsKyoriExistChk(.txtCustKyoriCd, .lblCustKyoriNm, tpg)

            '運賃タリフマスタの存在チェック    【運賃タリフコード（トンキロ建）入荷】
            rtnResult = rtnResult AndAlso Me.IsUnchinTariffExistChk(.txtUnchinTarifTonNyuka, .lblUnchinTarifTonNyuka, tpg, LMM090C.TableType.TON_KIRO)

            '運賃タリフマスタの存在チェック    【運賃タリフコード（車建）入荷】
            rtnResult = rtnResult AndAlso Me.IsUnchinTariffExistChk(.txtUnchinTarifShadateNyuka, .lblUnchinTarifShadateNyuka, tpg, LMM090C.TableType.SHA_DATE)

            '割増運賃タリフマスタの存在チェック【割増タリフコード入荷】
            rtnResult = rtnResult AndAlso Me.IsExtcTariffExistChk(.txtWarimashiTarifNyuka, .lblWarimashiTarifNyuka, tpg)

            '横持ちタリフマスタの存在チェック  【横持ち運賃タリフコード入荷】
            rtnResult = rtnResult AndAlso Me.IsYokoTariffExistChk(.txtYokomochiTarifNyuka, .lblYokomochiTarifNyuka, tpg)

            '運賃タリフマスタの存在チェック    【運賃タリフコード（トンキロ建）出荷】
            rtnResult = rtnResult AndAlso Me.IsUnchinTariffExistChk(.txtUnchinTarifTonShukka, .lblUnchinTarifTonShukka, tpg, LMM090C.TableType.TON_KIRO)

            '運賃タリフマスタの存在チェック    【運賃タリフコード（車建）出荷】
            rtnResult = rtnResult AndAlso Me.IsUnchinTariffExistChk(.txtUnchinTarifShadateShukka, .lblUnchinTarifShadateShukka, tpg, LMM090C.TableType.SHA_DATE)

            '割増運賃タリフマスタの存在チェック【割増タリフコード出荷】
            rtnResult = rtnResult AndAlso Me.IsExtcTariffExistChk(.txtWarimashiTarifShukka, .lblWarimashiTarifShukka, tpg)

            '横持ちタリフマスタの存在チェック  【横持ち運賃タリフコード出荷】
            rtnResult = rtnResult AndAlso Me.IsYokoTariffExistChk(.txtYokomochiTarifShukka, .lblYokomochiTarifShukka, tpg)

            '運送会社マスタ存在チェック　　　　【指定運送会社コード】
            rtnResult = rtnResult AndAlso Me.IsUnsocoExistChk(.txtShiteiUnsoCompCd, .txtShiteiUnsoShitenCd, .lblShiteiUnsoCompNm, tpg)

            'ADD Start 2018/10/25 要望番号001820
            '届先マスタ存在チェック　　　　　　【入荷出荷元コード】
            rtnResult = rtnResult AndAlso Me.IsDestExistChk(.txtInkaOrigCd, .lblInkaOrigNm, tpg)
            'ADD End   2018/10/25 要望番号001820

            '******************** 荷主コード(小)タブ ********************

            Dim closeKbnSeiqto As String = String.Empty
            Dim closeKbnNiyaku As String = String.Empty

            tpg = .tpgCustS

            '請求先マスタの存在チェック        【主請求先】
            rtnResult = rtnResult AndAlso Me.IsSeiqtoExistChk(.txtSeiqCd, .lblSeiqNm, tpg)

            '請求先マスタの存在チェック        【保管料請求先】
            rtnResult = rtnResult AndAlso Me.IsSeiqtoExistChk(.txtHokanSeiqCd, .lblHokanSeiqNm, tpg, closeKbnSeiqto)

            '請求先マスタの存在チェック        【荷役料請求先】
            rtnResult = rtnResult AndAlso Me.IsSeiqtoExistChk(.txtNiyakuSeiqCd, .lblNiyakuSeiqNm, tpg, closeKbnNiyaku)

            '請求先マスタの存在チェック        【運賃請求先】
            rtnResult = rtnResult AndAlso Me.IsSeiqtoExistChk(.txtUnchinSeiqCd, .lblUnchinSeiqNm, tpg)

            '請求先マスタの存在チェック        【作業料請求先】
            rtnResult = rtnResult AndAlso Me.IsSeiqtoExistChk(.txtSagyoSeiqCd, .lblSagyoSeiqNm, tpg)

            '過去日不可チェック
            rtnResult = rtnResult AndAlso Me.ChkOldDate(clickObj, beforeDate)

            'ワーニングチェックを行う
            rtnResult = rtnResult AndAlso Me.ChkCloseKbn(closeKbnSeiqto, closeKbnNiyaku, clickObj)

            Return rtnResult

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage _
                                        ) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '新規時、新規対象レコードの荷主コードと同値の場合、スルー
            If .lblSituation.RecordStatus.Equals(RecordStatus.NEW_REC) _
            AndAlso .txtCustCdL.TextValue.Equals(checkValue) Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectCustListDataRow(ExistChkDr, checkValue) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("CUST_CD_L").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("CUST_NM_L").ToString()
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 作業項目マスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoKmkExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            'START YANAI 要望番号376
            'If Me._ControlV.SelectSagyoListDataRow(ExistChkDr, checkValue, CustCdL, brCd) = False Then
            Dim SelectSagyoString As String = String.Empty
            '削除フラグ
            SelectSagyoString = String.Concat(SelectSagyoString, " SYS_DEL_FLG = '0' ")
            '作業コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND SAGYO_CD = '", checkValue, "' ")
            '営業所コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND NRS_BR_CD = '", brCd, "' ")
            '荷主コード
            SelectSagyoString = String.Concat(SelectSagyoString, " AND (CUST_CD_L = '", CustCdL, "' OR CUST_CD_L = 'ZZZZZ')")

            Dim sagyoDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(SelectSagyoString)
            If sagyoDr.Length = 0 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E079", New String() {"作業項目マスタ", checkValue})
                MyBase.ShowMessage("E768", New String() {checkValue})
                '2016.01.06 UMANO 英語化対応END
                'END YANAI 要望番号376
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                'START YANAI 要望番号376
                'If ExistChkDr IsNot Nothing Then
                '    ctlCd.TextValue = ExistChkDr(0).Item("SAGYO_CD").ToString()
                '    ctlNm.TextValue = ExistChkDr(0).Item("SAGYO_NM").ToString()
                'End If
                ctlCd.TextValue = sagyoDr(0).Item("SAGYO_CD").ToString()
                ctlNm.TextValue = sagyoDr(0).Item("SAGYO_NM").ToString()
                'END YANAI 要望番号376
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 距離程マスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKyoriExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectKyoriListDataRow(ExistChkDr, brCd, checkValue) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("KYORI_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("KYORI_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    Private Function IsUnchinTariffExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage _
                                        , ByVal tableType As LMM090C.TableType) As Boolean

        With Me._Frm

            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me.SelectUnchinTariffListDataRow(ExistChkDr, checkValue, tableType) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("UNCHIN_TARIFF_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("UNCHIN_TARIFF_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="drs">データロウ配列</param>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tableType">テーブルタイプ</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnchinTariffListDataRow(ByRef drs As DataRow() _
                                                  , ByVal tariffCd As String _
                                                  , ByVal tableType As LMM090C.TableType _
                                                  ) As Boolean

        '2019/07/11 依頼番号:006680 add start
        'まずはテーブルタイプ条件なしで存在を確認する
        Dim drsDummy As DataRow() = Nothing
        drsDummy = Me._ControlV.SelectUnchinTariffListDataRow(tariffCd, "", "", "**")

        If drsDummy.Length < 1 Then
            MyBase.ShowMessage("E079", New String() {"運賃タリフマスタ", tariffCd})
            Return False
        End If
        '2019/07/11 依頼番号:006680 add end

        'テーブルタイプの設定(未設定時 ：<> 01で検索)
        Dim tType As String = String.Empty
        Dim msg1 As String = String.Empty
        Dim msg2 As String = String.Empty
        Select Case tableType
            Case LMM090C.TableType.SHA_DATE
                tType = "01"
                msg1 = "車建"
                msg2 = "車建のタリフコード"

            Case LMM090C.TableType.TON_KIRO
                msg1 = "重量・距離建"
                msg2 = "車建以外のタリフコード"

        End Select

        'キャッシュテーブルからデータ抽出
        drs = Me._ControlV.SelectUnchinTariffListDataRow(tariffCd, "", "", tType)

        '取得できない場合、エラー
        If drs.Length < 1 Then

            '2016.01.06 UMANO 英語化対応START
            Select Case tableType
                Case LMM090C.TableType.SHA_DATE
                    MyBase.ShowMessage("E873")

                Case LMM090C.TableType.TON_KIRO
                    MyBase.ShowMessage("E874")

            End Select
            'MyBase.ShowMessage("E187", New String() {msg1, msg2})
            '2016.01.06 UMANO 英語化対応END

            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 割増運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExtcTariffExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectExtcUnchinListDataRow(ExistChkDr, brCd, checkValue) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("EXTC_TARIFF_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("EXTC_TARIFF_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoTariffExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectYokoTariffListDataRow(ExistChkDr, brCd, checkValue) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("YOKO_TARIFF_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("YOKO_REM").ToString()
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqtoExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage _
                                        , Optional ByRef closeKbn As String = "") As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectSeiqtoListDataRow(ExistChkDr, brCd, checkValue) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("SEIQTO_CD").ToString()
                    ctlNm.TextValue = Me._ControlV.EditConcatData(ExistChkDr(0).Item("SEIQTO_NM").ToString(), ExistChkDr(0).Item("SEIQTO_BUSYO_NM").ToString(), Space(1))
                    closeKbn = ExistChkDr(0).Item("CLOSE_KB").ToString()
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社のセット入力チェック
    ''' </summary>
    ''' <param name="compCd">運送会社コード</param>
    ''' <param name="sitenCd">運送会社支店コード</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoSetChk(ByVal compCd As Win.InputMan.LMImTextBox _
                                          , ByVal sitenCd As Win.InputMan.LMImTextBox _
                                          , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            Dim unsocoCd As String = compCd.TextValue
            Dim unsocoBrCd As String = sitenCd.TextValue

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = True _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = True Then
                Return True
            End If

            '両方に値がある場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = False _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = False Then
                Return True
            End If

            '片方に値がある場合、エラー
            Dim errorControl As Control() = New Control() {compCd, sitenCd}
            Call Me._ControlV.SetErrorControl(errorControl, compCd, .tabCust, tpg)
            '2016.01.06 UMANO 英語化対応START
            'Return Me._ControlV.SetErrMessage("E017", New String() {"指定運送会社コード", "指定運送会社支店コード"})
            Return Me._ControlV.SetErrMessage("E875")
            '2016.01.06 UMANO 英語化対応START

        End With

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <param name="compCd">運送会社コード</param>
    ''' <param name="sitenCd">運送会社支店コード</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk(ByVal compCd As Win.InputMan.LMImTextBox _
                                      , ByVal sitenCd As Win.InputMan.LMImTextBox _
                                      , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                      , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            '存在チェック前にセット入力チェック
            If Me.IsUnsocoSetChk(compCd, sitenCd, tpg) = False Then
                Return False
            End If

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkComp As String = compCd.TextValue
            Dim checkSiten As String = sitenCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkComp) = True _
            OrElse String.IsNullOrEmpty(checkSiten) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectUnsocoListDataRow(ExistChkDr, brCd, checkComp, checkSiten, LMMControlC.UnsoMsgType.UNSO_SHITEN) = False Then
                Dim errorControl As Control() = New Control() {compCd, sitenCd}
                Call Me._ControlV.SetErrorControl(errorControl, compCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    compCd.TextValue = ExistChkDr(0).Item("UNSOCO_CD").ToString()
                    sitenCd.TextValue = ExistChkDr(0).Item("UNSOCO_BR_CD").ToString()
                    ctlNm.TextValue = Me._ControlV.EditConcatData(ExistChkDr(0).Item("UNSOCO_NM").ToString(), ExistChkDr(0).Item("UNSOCO_BR_NM").ToString(), Space(1))
                End If
            End If

            Return True

        End With

    End Function

    'ADD Start 2018/10/25 要望番号001820
    ''' <summary>
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <param name="ctlCd">マスタ存在チェックを行う項目</param>
    ''' <param name="ctlNm">名称設定項目</param>
    ''' <param name="tpg">表示するタブ頁</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk(ByVal ctlCd As Win.InputMan.LMImTextBox _
                                        , ByVal ctlNm As Win.InputMan.LMImTextBox _
                                        , ByVal tpg As System.Windows.Forms.TabPage) As Boolean

        With Me._Frm

            Dim brCd As String = .cmbBr.SelectedValue.ToString()
            Dim checkValue As String = ctlCd.TextValue

            '名称項目を空にする
            ctlNm.TextValue = String.Empty

            '値がない場合、スルー
            If String.IsNullOrEmpty(checkValue) = True Then
                Return True
            End If

            '名称取得用テーブル
            Dim ExistChkDr As DataRow() = Nothing

            'マスタ存在チェックを行う
            If Me._ControlV.SelectDestListDataRow(ExistChkDr, brCd, .txtCustCdL.TextValue, checkValue) = False Then
                Call Me._ControlV.SetErrorControl(ctlCd, .tabCust, tpg)
                Return False
            Else
                If ExistChkDr IsNot Nothing Then
                    ctlCd.TextValue = ExistChkDr(0).Item("DEST_CD").ToString()
                    ctlNm.TextValue = ExistChkDr(0).Item("DEST_NM").ToString()
                End If
            End If

            Return True

        End With

    End Function
    'ADD End   2018/10/25 要望番号001820

#End Region

#End Region

#End Region

End Class
