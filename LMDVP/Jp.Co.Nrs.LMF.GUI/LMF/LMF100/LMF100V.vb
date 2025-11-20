' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送サブシステム
'  プログラムID     :  LMF100V : 帳票印刷指示
'  作  成  者       :  篠原
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF100Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMF100V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF100F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconV As LMFControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMFconG As LMFControlG
#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF100F, ByVal v As LMFControlV, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        Me._LMFconV = v

        Me._LMFconG = g

    End Sub

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        With Me._Frm

            '印刷種別の取得
            Dim Print As String = .cmbPrint.SelectedValue.ToString


            'スペース除去
            Call Me.TrimSpaceTextValue()

            '単項目チェック()
            If Me.IsPrintCheck(Print) = False Then
                Return False
            End If
            '関連チェック(共通)
            If Me.IsPrintSaveCheck() = False Then
                Return False
            End If

        End With

        Return True

    End Function
    ''' <summary>
    ''' 単項目チェック(共通)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintCheck(ByVal Print As String) As Boolean

        With Me._Frm


            '**********単項目チェック(印刷種別共通)

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '営業所
            .cmbBr.ItemName = "営業所"
            .cmbBr.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '荷主(大)コード
            .txtCustCdL.ItemName = "荷主(大)コード"
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主(中)コード
            .txtCustCdM.ItemName = "荷主(中)コード"
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            Dim errorFlg As Boolean = False

            '日付From
            .imdOutkaDateFrom.ItemName = "日付From"
            .imdOutkaDateFrom.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdOutkaDateFrom) = False Then
                Return False
            End If
            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdOutkaDateFrom, .imdOutkaDateFrom.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '日付To
            .imdOutkaDateTo.ItemName = "日付To"
            .imdOutkaDateTo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdOutkaDateTo) = False Then
                Return False
            End If

            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdOutkaDateTo, .imdOutkaDateTo.ItemName) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷時の関連チェック()
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSaveCheck() As Boolean

        With Me._Frm

            '日付の
            If Me.IsPrintDateChk() = False Then
                Return False
            End If

            '荷主の存在チェック
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue
            Dim drc As DataRow() = Nothing

            If String.IsNullOrEmpty(CustCdL) = False Then
                'END YANAI 要望番号592

                'キャッシュの荷主マスタ
                'drc = Me.SelectCustListDataRow(CustCdL, CustCdM, "")
                '20161003 要番2622 tsunehira add
                drc = Me._LMFconG.SelectCustListDataRow(.cmbBr.SelectedValue.ToString(), CustCdL, CustCdM)

                '0件以上の場合
                If drc.Count <= 0 Then
                    'エラーメッセージ
                    Me.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM)})
                    Me._LMFconV.SetErrorControl(.txtCustCdL)
                    Me._LMFconV.SetErrorControl(.txtCustCdM)
                    Return False
                End If
            End If
        End With

        Return True

    End Function

    Private Function IsPrintDateChk() As Boolean

        With Me._Frm

            '日付の大小チェック
            Dim dateFrom As String = .imdOutkaDateFrom.TextValue
            Dim dateTo As String = .imdOutkaDateTo.TextValue
            '(日付From、日付To)がない場合、スルー
            If String.IsNullOrEmpty(dateFrom) = True _
                AndAlso String.IsNullOrEmpty(dateTo) = True Then
                Return True
            End If

            '日付の大小チェック
            If Me.IsLargeSmallChk(dateTo, dateFrom, False) = False Then
                .imdOutkaDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMFconV.SetErrorControl(.imdOutkaDateTo)
                Return Me.SetErrMessage("E039", New String() {"日付To", "日付From"})
                Return False
            End If

            '上限チェック
            Dim limitDate As String = Convert.ToDateTime(DateFormatUtility.EditSlash(dateFrom)).AddMonths(1).ToString("yyyyMMdd")
            If limitDate < dateTo Then
                .imdOutkaDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMFconV.SetErrorControl(.imdOutkaDateTo)
                Return Me.SetErrMessage("E014", New String() {"印刷日付To" _
                                                              , DateFormatUtility.EditSlash(dateFrom) _
                                                              , DateFormatUtility.EditSlash(limitDate) _
                                                              })

            End If
            Return True

        End With

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
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
        End With

    End Sub
#Region "日付関連"
    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me.SetErrMessage("E038", New String() {str, "8"})

        End If

        Return True

    End Function

#Region "大小チェック"

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsLargeSmallChk(ByVal large As String, ByVal small As String, ByVal equalFlg As Boolean) As Boolean

        '値がない場合、スルー
        If String.IsNullOrEmpty(large) = True OrElse _
           String.IsNullOrEmpty(small) = True _
           Then
            Return True
        End If

        '大小比較
        Return Me.IsLargeSmallChk(Convert.ToDecimal(Me.FormatNumValue(large)), Convert.ToDecimal(Me.FormatNumValue(small)), equalFlg)

    End Function

    ''' <summary>
    ''' 大小チェック
    ''' </summary>
    ''' <param name="large">大きい方の値</param>
    ''' <param name="small">小さい方の値</param>
    ''' <param name="equalFlg">イコールがエラーの場合：True　イコールがエラーではないの場合：False</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Overloads Function IsLargeSmallChk(ByVal large As Decimal, ByVal small As Decimal, ByVal equalFlg As Boolean) As Boolean

        '大小比較
        If equalFlg = True Then

            If large <= small Then
                Return False
            End If

        Else

            If large < small Then
                Return False
            End If

        End If

        Return True

    End Function
#End Region
#End Region
#End Region

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMF100C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMF100C.EventShubetsu.MASTEROPEN          'マスタ参照
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


            Case LMF100C.EventShubetsu.PRINT        '印刷
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



            Case LMF100C.EventShubetsu.TOJIRU           '閉じる
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF100C.EventShubetsu) As Boolean

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

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    ctl1 = .txtCustCdL
                    msg1 = "荷主(大)コード"
                    lbl1 = .lblCustNmL
                    ctl2 = .txtCustCdM
                    msg2 = "荷主(中)コード"
                    lbl2 = .lblCustNmM
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

                    Case LMF100C.EventShubetsu.MASTEROPEN

                        Return Me.SetFocusErrMessage()

                    Case LMF100C.EventShubetsu.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select

            End If


            'Enterイベントの場合、空の場合、表示しない
            Select Case actionType

                Case LMF100C.EventShubetsu.ENTER

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
#Region "部品化検討中"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Private Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    ''' <summary>
    ''' フォーカス位置エラーのメッセージ設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function SetFocusErrMessage() As Boolean

        Return Me.SetErrMessage("G005")

    End Function
    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String) As Boolean

        MyBase.ShowMessage(id)
        Return False

    End Function

    ''' <summary>
    ''' メッセージ設定
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Friend Function SetErrMessage(ByVal id As String, ByVal msg As String()) As Boolean

        MyBase.ShowMessage(id, msg)
        Return False

    End Function

#Region "ユーティリティー"
    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function
#End Region
#Region "キャッシュ"

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="seiqCd">請求先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(Optional ByVal custLCd As String = "" _
                                             , Optional ByVal custMCd As String = "" _
                                             , Optional ByVal seiqCd As String = "" _
                                             ) As DataRow()
        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(_Frm.cmbBr.SelectedValue.ToString(), custLCd, custMCd, seiqCd))

    End Function


    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="seiqCd">請求先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal brCd As String _
                                     , ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal seiqCd As String _
                                     ) As String


        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '営業所コード　'20161003 要番2622 tsunehira add
        SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")


        If String.IsNullOrEmpty(custLCd) = False Then
            '荷主コード（大）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")
        End If

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        Return SelectCustString

    End Function
#End Region
#End Region



End Class
