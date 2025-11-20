' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : データ管理サブ
'  プログラムID     :  LMI020V : デュポン在庫
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMI020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMI020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI020F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMIControlV

    ''' <summary>
    ''' LMI020Gクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMI020G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI020F, ByVal v As LMIControlV, ByVal g As LMI020G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._G = g

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

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        'ワーニングチェック
        rtnResult = rtnResult AndAlso Me.IsWarningChk()

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
            .cmbPrint.ItemName = .lblTitlePrint.Text
            .cmbPrint.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbPrint) = errorFlg Then
                Return errorFlg
            End If

            Dim shubetsu As String = .cmbPrint.SelectedValue.ToString()

            'プラントコード
            .cmbPlantCd.ItemName = "プラントコード"
            .cmbPlantCd.IsHissuCheck = LMI020C.PRINT_SFTP.Equals(shubetsu)
            If MyBase.IsValidateCheck(.cmbPlantCd) = errorFlg Then
                Return errorFlg
            End If

            '営業所
            .cmbEigyo.ItemName = .lblTitleEigyo.Text
            .cmbEigyo.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbEigyo) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(大)
            .txtCustCdL.ItemName = String.Concat(.lblTitleCust.Text, "コード(大)")
            .txtCustCdL.IsHissuCheck = LMI020C.PRINT_NITHIJI.Equals(shubetsu)
            .txtCustCdL.IsForbiddenWordsCheck = chkFlg
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtCustCdL) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = String.Concat(.lblTitleCust.Text, "コード(中)")
            .txtCustCdM.IsHissuCheck = LMI020C.PRINT_NITHIJI.Equals(shubetsu)
            .txtCustCdM.IsForbiddenWordsCheck = chkFlg
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード(小)
            .txtCustCdS.ItemName = String.Concat(.lblTitleCust.Text, "コード(小)")
            .txtCustCdS.IsFullByteCheck = 2
            .txtCustCdM.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(.txtCustCdS) = errorFlg Then
                Return errorFlg
            End If

            '報告日
            .imdHokokuDate.ItemName = .lblTitleHokokuDate.Text
            .imdHokokuDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdHokokuDate) = errorFlg Then
                Return errorFlg
            End If
            If Me._Vcon.IsInputDateFullByteChk(.imdHokokuDate, .lblTitleHokokuDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '月末在庫
            .cmbZaiRirekiDate.ItemName = .lblTitleZaiRirekiDate.Text
            .cmbZaiRirekiDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbZaiRirekiDate) = errorFlg Then
                Return errorFlg
            End If

        End With

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
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsConditionChk() As Boolean

        '日付の大小チェック
        Dim rtnResult As Boolean = Me.IsDateFromToChk()

        '荷主コード大中の関連チェック
        rtnResult = rtnResult AndAlso Me.IsCustSetChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主マスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExist() As Boolean

        With Me._Frm

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
            Dim custS As String = .txtCustCdS.TextValue
            If Me._Vcon.SelectCustListDataRow(drs, custL, custM, custS, LMIControlC.FLG_OFF, LMIControlC.CustMsgType.CUST_S) = False Then
                .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

                '(小)コードに値がある場合、反映
                If String.IsNullOrEmpty(custS) = False Then
                    .txtCustCdS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                End If
                Me._Vcon.SetErrorControl(.txtCustCdL)
                Return False
            End If

            '名称の設定
            .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
            .lblCustNmM.TextValue = drs(0).Item("CUST_NM_M").ToString()

            '(小)コードに値がある場合、反映
            If String.IsNullOrEmpty(custS) = False Then
                .lblCustNmS.TextValue = drs(0).Item("CUST_NM_S").ToString()
            End If

            '荷主(小)を含まない情報を取得
            Me._Vcon.SelectCustListDataRow(drs, custL, custM, , LMIControlC.FLG_OFF, LMIControlC.CustMsgType.CUST_S)

            '荷主(小)一覧に反映
            Call Me._G.SetCustSDtl(drs)

            'デュポン荷主チェック
            Return Me.IsDhuponCustExistChk(custL, custM)

        End With

    End Function

    ''' <summary>
    ''' デュポン荷主チェック
    ''' </summary>
    ''' <param name="custLCd">荷主コード(大)</param>
    ''' <param name="custMCd">荷主コード(中)</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDhuponCustExistChk(ByVal custLCd As String, ByVal custMCd As String) As Boolean

        With Me._Frm

            'SFTPの場合、チェックをしない
            If LMI020C.PRINT_SFTP.Equals(.cmbPrint.SelectedValue.ToString()) = True Then
                Return True
            End If

            '取得できない場合、エラー
            Dim sCd As String = .txtCustCdS.TextValue
            Dim drs As DataRow() = Me._Vcon.SelectKbnListDataRow(, LMKbnConst.KBN_D006, .cmbEigyo.SelectedValue.ToString(), .txtCustCdL.TextValue, .txtCustCdM.TextValue, sCd)
            If drs.Length < 1 Then
                .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.txtCustCdL)
                If String.IsNullOrEmpty(sCd) = False Then
                    .txtCustCdS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                End If
                Return Me._Vcon.SetErrMessage("E300")

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 日付の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDateFromToChk() As Boolean

        With Me._Frm

            Dim zetuDate As String = .cmbZaiRirekiDate.SelectedValue.ToString()

            'START YANAI 要望番号410
            ''初期在庫を選択した場合、スルー
            'If LMI020C.END_DATE.Equals(zetuDate) = True Then
            '    Return True
            'End If
            'END YANAI 要望番号410

            '月末在庫 < 報告日の場合、スルー
            If zetuDate < .imdHokokuDate.TextValue Then
                Return True
            End If

            .cmbZaiRirekiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Vcon.SetErrorControl(.imdHokokuDate)
            Return Me._Vcon.SetErrMessage("E182", New String() {.lblTitleHokokuDate.Text, .lblTitleZaiRirekiDate.Text})

        End With

    End Function

    ''' <summary>
    ''' 荷主コード大中の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustSetChk() As Boolean

        With Me._Frm

            Dim custL As String = .txtCustCdL.TextValue
            Dim custM As String = .txtCustCdM.TextValue

            '両方に値がある場合、スルー
            If Me.IsCustSetChk(custL, custM, False) = True Then
                Return True
            End If

            '両方に値がない場合、スルー
            If Me.IsCustSetChk(custL, custM, True) = True Then
                Return Me.IsCustSChk()
            End If

            .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Vcon.SetErrorControl(.txtCustCdL)
            Return Me._Vcon.SetErrMessage("E017", New String() {"荷主コード(大)", "荷主コード(中)"})

        End With

    End Function

    ''' <summary>
    ''' 荷主コード大中の関連チェック
    ''' </summary>
    ''' <param name="custL">荷主コード(大)</param>
    ''' <param name="custM">荷主コード(中)</param>
    ''' <param name="chk">チェックフラグ　True：両方に値がない場合、True　False：両方に値がある場合、True</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsCustSetChk(ByVal custL As String, ByVal custM As String, ByVal chk As Boolean) As Boolean

        '両方に値がある(ない)場合、スルー
        If String.IsNullOrEmpty(custL) = chk _
            AndAlso String.IsNullOrEmpty(custM) = chk Then
            Return True
        End If

        Return False

    End Function

    ''' <summary>
    ''' 荷主コード小のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustSChk() As Boolean

        With Me._Frm

            '小コードが空の場合、スルー
            Dim custS As String = .txtCustCdS.TextValue
            If String.IsNullOrEmpty(custS) = True Then
                Return True
            End If

            '値がある場合、エラー
            .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            .txtCustCdS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Vcon.SetErrorControl(.txtCustCdL)
            Dim msg As String = "荷主コード"
            Return Me._Vcon.SetErrMessage("E224", New String() {String.Concat(msg, "(小)"), String.Concat(msg, "(大)", ",", msg, "(中)")})

        End With

    End Function

    ''' <summary>
    ''' ワーニングチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsWarningChk() As Boolean

        With Me._Frm

            '在庫証明書以外、スルー
            If LMI020C.PRINT_ZAIKO.Equals(.cmbPrint.SelectedValue.ToString()) = False Then
                Return True
            End If

            '荷主コード(大)に値がある場合、スルー
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = False Then
                Return True
            End If

            'ワーニング表示
            If Me._Vcon.IsWarningChk(MyBase.ShowMessage("W156")) = False Then
                .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.txtCustCdL)
                Return False
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMI020C.ActionType) As Boolean

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

                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtCustCdS.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNmL, .lblCustNmM, .lblCustNmS, .lblCustDtl}
                    msg = New String() {String.Concat(custNm, "コード(大)"), String.Concat(custNm, "コード(中)"), String.Concat(custNm, "コード(小)")}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMI020C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMI020C.ActionType.CREATE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI020C.ActionType.MASTEROPEN

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI020C.ActionType.ENTER

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = False
                End Select

            Case LMI020C.ActionType.CLOSE

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
            .txtCustCdS.TextValue = .txtCustCdS.TextValue.Trim()

        End With

    End Sub

#End Region 'Method

End Class
