' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ     : ｼｽﾃﾑ管理
'  プログラムID     :  LMJ010V : 請求在庫・実在庫差異分リスト作成
'  作  成  者       :  Shinohara
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMJ010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMJ010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMJ010F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMJControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMJ010F, ByVal v As LMJControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsConditionChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '作成種別
            .cmbPrint.ItemName = .lblTitleShubetsu.Text
            .cmbPrint.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbPrint) = errorFlg Then
                Return errorFlg
            End If

            '処理内容
            .cmbShori.ItemName = .lblTitleShori.Text
            .cmbShori.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbShori) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(大)
            Dim chkSonota As Boolean = LMJ010C.SHORI_SONOTA.Equals(.cmbShori.SelectedValue.ToString())
            .txtCustCdL.ItemName = String.Concat(.lblTitleCust.Text, "コード(大)")
            .txtCustCdL.IsHissuCheck = chkSonota
            .txtCustCdL.IsForbiddenWordsCheck = chkFlg
            If chkSonota = True Then
                .txtCustCdL.IsFullByteCheck = 5
            End If
            .txtCustCdL.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtCustCdL) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = String.Concat(.lblTitleCust.Text, "コード(中)")
            .txtCustCdM.IsHissuCheck = chkSonota
            .txtCustCdM.IsForbiddenWordsCheck = chkFlg
            If chkSonota = True Then
                .txtCustCdM.IsFullByteCheck = 2
            End If
            If MyBase.IsValidateCheck(.txtCustCdM) = errorFlg Then
                Return errorFlg
            End If

            '請求日付(Date型)
            .imdSeiqDate.ItemName = .lblTitleSeiqDate.Text
            .imdSeiqDate.IsHissuCheck = chkSonota
            If MyBase.IsValidateCheck(.imdSeiqDate) = errorFlg Then
                Return errorFlg
            End If
            If Me._Vcon.IsInputDateFullByteChk(.imdSeiqDate, .lblTitleSeiqDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '請求日付(Comb型)
            .cmbSeiqComb.ItemName = .lblTitleSeiqDate.Text
            .cmbSeiqComb.IsHissuCheck = Not chkSonota
            If MyBase.IsValidateCheck(.cmbSeiqComb) = errorFlg Then
                Return errorFlg
            End If

            '月末在庫
            .cmbZaiko.ItemName = .lblTitleZaiko.Text
            .cmbZaiko.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbZaiko) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsConditionChk() As Boolean

        'デフォルトプリンタの必須チェック
        Dim rtnResult As Boolean = Me.IsDefPrintChk()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '日付の大小チェック
        rtnResult = rtnResult AndAlso Me.IsDateFromToChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' デフォルトプリンタの必須チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDefPrintChk() As Boolean

        'デフォルトプリンタ1に値がないがない場合、エラー
        If String.IsNullOrEmpty(LMUserInfoManager.GetDefPrt1()) = True Then
            Return Me._Vcon.SetErrMessage("E019", New String() {"ログインユーザーのデフォルトプリンタ1"})
        End If

        Return True

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean
        Return Me.IsCustExist()
    End Function

    ''' <summary>
    ''' 荷主マスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExist() As Boolean

        With Me._Frm

            '処理内容 = 指定された荷主のみチェックするの場合、スルー
            If LMJ010C.SHORI_SONOTA.Equals(.cmbShori.SelectedValue.ToString()) = True Then
                Return True
            End If

            '(大)コードがない場合、スルー
            Dim custL As String = .txtCustCdL.TextValue
            If String.IsNullOrEmpty(custL) = True Then
                Return True
            End If

            '(中)コードがない場合、スルー
            Dim custM As String = .txtCustCdM.TextValue
            If String.IsNullOrEmpty(custM) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCustListDataRow(drs, custL, custM, LMJControlC.FLG_OFF, LMJControlC.FLG_OFF, LMJControlC.CustMsgType.CUST_M) = False Then
                .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Return False
            End If

            '名称の設定
            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 日付の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDateFromToChk() As Boolean

        With Me._Frm

            Dim zaikoDate As String = .cmbZaiko.SelectedValue.ToString

            '値がない場合、スルー
            If String.IsNullOrEmpty(zaikoDate) = True Then
                Return True
            End If

            '荷主指定以外、スルー
            If LMJ010C.SHORI_SONOTA.Equals(.cmbShori.SelectedValue.ToString()) = False Then
                Return True
            End If

            '報告日 < 月末在庫の場合、エラー
            If .imdSeiqDate.TextValue < zaikoDate Then

                .cmbZaiko.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.imdSeiqDate)
                Return Me._Vcon.SetErrMessage("E182", New String() {"請求日付", "月末在庫"})

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMJ010C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNmL, .lblCustNmM}
                    msg = New String() {String.Concat(custNm, "コード(大)"), String.Concat(custNm, "コード(中)")}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMJ010C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMJ010C.ActionType.CREATE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMJ010C.ActionType.ZAIKO

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMJ010C.ActionType.MASTEROPEN

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMJ010C.ActionType.ENTER

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = False
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMJ010C.ActionType.CLOSE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()

        End With

    End Sub

#End Region 'Method

End Class
