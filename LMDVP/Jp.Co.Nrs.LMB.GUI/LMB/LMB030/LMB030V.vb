' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB     : 入荷
'  プログラムID     :  LMB030V : 入荷報告書
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB030Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB030V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB030F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMBconV As LMBControlV


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB030F, ByVal v As LMBControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMBconV = v

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
    Friend Function IsInputCheck(Optional ByVal mode As String = LMB030C.MODE_DEFAULT) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()


        '単項目チェック(印刷用)
        If Me.IsPrintSingleCheck() = False Then
            Return False
        End If

        '関連チェック(印刷用)
        If Me.IsPrintSaveCheck() = False Then
            Return False
        End If


        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック(印刷種別)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSingleCheck() As Boolean

        With Me._Frm
            Dim errorFlg As Boolean = False
            '**********単項目チェック(印刷種別)

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '営業所
            .cmbEigyo.ItemName = "営業所"
            .cmbEigyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If


            '荷主(大)コード
            .txtCustCD_L.ItemName = "荷主(大)コード"
            .txtCustCD_L.IsHissuCheck = True
            .txtCustCD_L.IsForbiddenWordsCheck = True
            .txtCustCD_L.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCD_L) = False Then
                Return False
            End If

            '荷主(中)コード
            .txtCustCD_M.ItemName = "荷主(中)コード"
            .txtCustCD_M.IsHissuCheck = True
            .txtCustCD_M.IsForbiddenWordsCheck = True
            .txtCustCD_M.IsByteCheck = 2
            .txtCustCD_M.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCD_M) = False Then
                Return False
            End If

            '入荷日
            .imdNyukaDate.ItemName = "入荷日"
            .imdNyukaDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdNyukaDate) = False Then
                Return False
            End If
            If Me.IsInputDateFullByteChk(.imdNyukaDate, .lblTitleNyukaDate.Text) = errorFlg Then
                Me._LMBconV.SetErrorControl(.imdNyukaDate)
                Return errorFlg
            End If

            'データ登録日
            If Me.IsInputDateFullByteChk(.imdDataInsDate, .lblTitleDataInsDate.Text) = errorFlg Then
                Me._LMBconV.SetErrorControl(.imdDataInsDate)
                Return errorFlg
            End If

            Return True
        End With

    End Function


    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me._LMBconV.SetErrMessage("E038", New String() {str, "8"})

        End If

        Return True

    End Function

    ''' <summary>
    ''' 印刷時の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSaveCheck() As Boolean
        Dim drs As DataRow() = Nothing

        With Me._Frm

            Dim brcd As String = .cmbEigyo.SelectedValue.ToString
            Dim CustCdL As String = .txtCustCD_L.TextValue
            Dim CustCdM As String = .txtCustCD_M.TextValue
            Dim CustCdS As String = "00"
            Dim CustCdSS As String = "00"

            '荷主名ラベルクリア
            .lblCustNM_L.TextValue = String.Empty
            .lblCustNM_M.TextValue = String.Empty

            '存在チェック(荷主マスタ)
            If String.IsNullOrEmpty(CustCdL) = False AndAlso _
            String.IsNullOrEmpty(CustCdM) = False Then
                drs = Me._LMBconV.SelectCustListDataRow(CustCdL, CustCdM, CustCdS, CustCdSS)

                Dim count As Integer = drs.Count

                If count < 1 Then
                    Call Me._LMBconV.SetErrorControl(.txtCustCD_L)
                    Call Me._LMBconV.SetErrorControl(.txtCustCD_M)

                    Return Me._LMBconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM)})

                    Return False
                Else
                    .lblCustNM_L.TextValue = drs(0).Item("CUST_NM_L").ToString()
                    .lblCustNM_M.TextValue = drs(0).Item("CUST_NM_M").ToString()

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCD_L.TextValue = .txtCustCD_L.TextValue.Trim()
            .txtCustCD_M.TextValue = .txtCustCD_M.TextValue.Trim()
            
        End With

    End Sub



    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMB030C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMB030C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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


            Case LMB030C.EventShubetsu.PRINT        '印刷
                '10:閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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



            Case LMB030C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

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

#Region "フォーカス位置チェック"
    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMB030C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm


            Dim ctl1 As Win.InputMan.LMImTextBox = Nothing
            Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
            Dim lbl1 As Win.InputMan.LMImTextBox = Nothing
            Dim lbl2 As Win.InputMan.LMImTextBox = Nothing
            Dim msg1 As String = String.Empty
            Dim msg2 As String = String.Empty
            Dim nullChk As Boolean = True
            Dim setFlg As Boolean = False

            Select Case objNm

                Case .txtCustCD_L.Name, .txtCustCD_M.Name

                    ctl1 = .txtCustCD_L
                    msg1 = "荷主(大)コード"
                    lbl1 = .lblCustNM_L
                    ctl2 = .txtCustCD_M
                    msg2 = "荷主(中)コード"
                    lbl2 = .lblCustNM_M


                    '荷主(大)、(中)に値が入っていない場合ラベルをクリア
                    If String.IsNullOrEmpty(ctl1.TextValue) = True Then
                        lbl1.TextValue = String.Empty
                    End If

                    If String.IsNullOrEmpty(ctl2.TextValue) = True Then
                        lbl2.TextValue = String.Empty
                    End If

                    setFlg = True
            End Select

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                Select Case actionType

                    Case LMB030C.EventShubetsu.MASTEROPEN

                        Return Me._LMBconV.SetFocusErrMessage()


                    Case LMB030C.EventShubetsu.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select

            End If


            'Enterイベントの場合、空の場合、表示しない
            Select Case actionType

                Case LMB030C.EventShubetsu.ENTER

                    If setFlg = True Then

                        '両方に値がない場合、スルー
                        If String.IsNullOrEmpty(ctl1.TextValue) = True _
                            AndAlso String.IsNullOrEmpty(ctl2.TextValue) = True Then
                            lbl1.TextValue = String.Empty
                            lbl2.TextValue = String.Empty
                            Return False
                        End If

                    Else

                        'ctl1に値がない 且つ (ctl2がNothing または ctl2に値がない)場合、スルー
                        If nullChk = True _
                            AndAlso String.IsNullOrEmpty(ctl1.TextValue) = True _
                            AndAlso (ctlChk = True OrElse String.IsNullOrEmpty(ctl2.TextValue) = True) _
                            Then
                            lbl1.TextValue = String.Empty
                            lbl2.TextValue = String.Empty
                            Return False

                        End If

                    End If

            End Select

            '禁止文字チェック(1つ目のコントロール)
            ctl1.ItemName = msg1
            ctl1.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(ctl1) = False Then
                Return False
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk = False Then
                ctl2.ItemName = msg1
                ctl2.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl2) = False Then
                    Return False
                End If
            End If

            Return True

        End With

    End Function


#End Region

#End Region 'Method

End Class
