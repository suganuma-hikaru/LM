' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC050V : 出荷帳票印刷
'  作  成  者       :  kishi
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread

Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMC050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC050F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconV As LMCControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMCconG As LMCControlG



#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC050F, ByVal v As LMCControlV, ByVal g As LMCControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMCconV = v

        Me._LMCconG = g

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

        With Me._Frm

            'スペース除去
            Call Me.TrimSpaceTextValue()

            '単項目チェック(共通)
            If Me.IsPrintKyoutuCheck() = False Then
                Return False
            End If

            Dim Shubetu As String = .cmbPrint.SelectedValue.ToString()

            '(2012.02.28) 出荷報告書(月次)追加 --- START ---
            Select Case Shubetu
                Case LMC050C.PRINT01, LMC050C.PRINT03
                    '単項目チェック(チェックリスト)、出荷報告書(月次)
                    If Me.IsPrintListCheck() = False Then
                        Return False
                    End If

                Case LMC050C.PRINT02
                    '単項目チェック(日別出荷報告書)
                    If Me.IsPrintDayCheck() = False Then
                        Return False
                    End If

                    '2013.07.31 追加START
                Case LMC050C.PRINT06
                    '単項目チェック(BYK出荷報告作成)
                    If Me.IsOutkaHoukokuCheck() = False Then
                        Return False
                    End If
                    '2013.07.31 追加END

            End Select
            '(2012.02.28) 出荷報告書(月次)追加 ---  END  ---

            '関連チェック()
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
    Private Function IsPrintKyoutuCheck() As Boolean

        With Me._Frm

            '**********単項目チェック(印刷種別共通)

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
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

            Return True

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック(チェックリスト)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintListCheck() As Boolean

        With Me._Frm
            Dim errorFlg As Boolean = False
            '出荷日
            .imdSyukkaDate.ItemName = "出荷日"
            .imdSyukkaDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdSyukkaDate) = False Then
                Return False
            End If
            If Me.IsInputDateFullByteChk(.imdSyukkaDate, .imdSyukkaDate.ItemName) = errorFlg Then
                Return errorFlg
            End If

            'データ登録日
            .imdDataInsDate.ItemName = "データ登録日"
            If Me.IsInputDateFullByteChk(.imdDataInsDate, .imdDataInsDate.ItemName) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック(日別出荷報告書)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintDayCheck() As Boolean

        With Me._Frm
            Dim errorFlg As Boolean = False

            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            '荷主(小)コード
            .txtCustCD_S.ItemName = "荷主(小)コード"
            .txtCustCD_S.IsForbiddenWordsCheck = True
            .txtCustCD_S.IsByteCheck = 2
            .txtCustCD_S.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCD_S) = False Then
                Return False
            End If
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

            '印刷日付From
            .imdPrintDate_S.ItemName = "印刷日付From"
            .imdPrintDate_S.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdPrintDate_S) = False Then
                Return False
            End If
            If Me.IsInputDateFullByteChk(.imdPrintDate_S, .imdPrintDate_S.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '印刷日付To
            .imdPrintDate_E.ItemName = "印刷日付To"
            .imdPrintDate_E.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdPrintDate_E) = False Then
                Return False
            End If
            If Me.IsInputDateFullByteChk(.imdPrintDate_E, .imdPrintDate_E.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '商品指定
            .txtGoodsCd.ItemName = "商品指定"
            .txtGoodsCd.IsForbiddenWordsCheck = True
            .txtGoodsCd.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCd) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    '2013.07.31 追加START
    ''' <summary>
    ''' 単項目チェック(BYK出荷報告作成)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOutkaHoukokuCheck() As Boolean

        Dim kbnDr() As DataRow = Nothing

        With Me._Frm
            Dim errorFlg As Boolean = False
            '出荷日
            .imdSyukkaDate.ItemName = "出荷日"
            .imdSyukkaDate.IsHissuCheck = True
            If Me.IsInputDateFullByteChk(.imdSyukkaDate, .imdSyukkaDate.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '報告作成対象荷主チェック
            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'H024' AND ", _
                                                                               "KBN_NM1 = '", .cmbEigyo.SelectedValue, "' AND ", _
                                                                               "KBN_NM2 = '", .txtCustCD_L.TextValue, "' AND ", _
                                                                               "KBN_NM3 = '", .txtCustCD_M.TextValue, "'"))
            If kbnDr.Length = 0 Then
                '区分マスタに設定されていない荷主の場合はエラー
                Me._LMCconV.SetErrMessage("E209", New String() {String.Concat(.txtCustCD_L.TextValue, " - ", .txtCustCD_M.TextValue)})
                Return False
            End If

        End With

        Return True

    End Function
    '2013.07.31 追加END

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me._LMCconV.SetErrMessage("E038", New String() {str, "8"})

        End If

        Return True

    End Function

    ''' <summary>
    ''' 印刷時の関連チェック()
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
            
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            Dim Hantei As String = .cmbPrint.SelectedValue.ToString

            '印刷種別が日別出荷報告書だった時は、画面の値を設定する。
            If LMC050C.PRINT02.Equals(Hantei) = True Then
                If .txtCustCD_S.TextValue.Trim.Equals("") = False Then
                    CustCdS = .txtCustCD_S.TextValue()
                End If
            End If
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

            '存在チェック(荷主マスタ)
            If String.IsNullOrEmpty(CustCdL) = False AndAlso _
            String.IsNullOrEmpty(CustCdM) = False Then
                drs = Me.SelectCustListDataRow(CustCdL, CustCdM, CustCdS, CustCdSS)

                Dim count As Integer = drs.Count

                If count < 1 Then
                    Call Me._LMCconV.SetErrorControl(.txtCustCD_L)
                    Call Me._LMCconV.SetErrorControl(.txtCustCD_M)

                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                    If LMC050C.PRINT02.Equals(Hantei) = True Then
                        '日別出荷報告書時
                        Call Me._LMCconV.SetErrorControl(.txtCustCD_S)
                    End If

                    If .txtCustCD_S.TextValue.Trim.Equals("") = False Then
                        Return Me._LMCconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM, "-", CustCdS)})
                    Else
                        Return Me._LMCconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM)})
                    End If
                    'Return Me._LMCconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM)})
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                    Return False

                End If

            End If

            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            'Dim Hantei As String = .cmbPrint.SelectedValue.ToString
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

            '印刷種別が日別出荷報告書だったときに大小チェックをします
            If LMC050C.PRINT02.Equals(Hantei) = True Then
                If Me.IsPrintDaySaveCheck() = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷時の関連チェック(日別出荷報告書)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintDaySaveCheck() As Boolean

        With Me._Frm

            'エラーフラグ
            Dim errorFlg As Boolean = False
            '印刷日付Fromの値
            Dim juryouFrom As String = .imdPrintDate_S.TextValue
            '印刷日付Toの値
            Dim juryouTo As String = .imdPrintDate_E.TextValue
           
            '印刷日付From
            .imdPrintDate_S.ItemName = "印刷日付From"
            '印刷日付To
            .imdPrintDate_E.ItemName = "印刷日付To"
            '運行予定日の大小チェック
          
            '(印刷日付From、印刷日付To)がない場合、スルー
            If String.IsNullOrEmpty(juryouFrom) = True _
                 AndAlso String.IsNullOrEmpty(juryouTo) = True Then
                Return True
            End If

            '印刷日付の大小チェック
            If Me.IsLargeSmallChk(juryouTo, juryouFrom, False) = False Then
                .imdPrintDate_S.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMCconV.SetErrorControl(.imdPrintDate_E)
                Return Me._LMCconV.SetErrMessage("E166", New String() {"印刷日付To", "印刷日付From"})
            End If

            '上限チェック
            Dim limitDate As String = Convert.ToDateTime(DateFormatUtility.EditSlash(juryouFrom)).AddMonths(1).ToString("yyyyMMdd")
            If limitDate < juryouTo Then
                .imdPrintDate_S.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMCconV.SetErrorControl(.imdPrintDate_E)
                Return Me._LMCconV.SetErrMessage("E014", New String() {"印刷日付To" _
                                                                       , DateFormatUtility.EditSlash(juryouFrom) _
                                                                       , DateFormatUtility.EditSlash(limitDate) _
                                                                       })
            End If

        End With

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
        Return Me.IsLargeSmallChk(Convert.ToDecimal(Me._LMCconG.FormatNumValue(large)), Convert.ToDecimal(Me._LMCconG.FormatNumValue(small)), equalFlg)

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
            .txtGoodsCd.TextValue = .txtGoodsCd.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMC050C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMC050C.EventShubetsu.MASTEROPEN          'マスタ参照
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


            Case LMC050C.EventShubetsu.PRINT        '印刷
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



            Case LMC050C.EventShubetsu.TOJIRU           '閉じる
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMC050C.EventShubetsu) As Boolean

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
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            Dim ctl3 As Win.InputMan.LMImTextBox = Nothing
            Dim lbl3 As Win.InputMan.LMImTextBox = Nothing
            Dim msg3 As String = String.Empty
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --
            Dim msg1 As String = String.Empty
            Dim msg2 As String = String.Empty
            Dim nullChk As Boolean = True
            Dim setFlg As Boolean = False

            Select Case objNm

                Case .txtCustCD_L.Name, .txtCustCD_M.Name, .txtCustCD_S.Name

                    ctl1 = .txtCustCD_L
                    msg1 = "荷主(大)コード"
                    lbl1 = .lblCustNM_L
                    ctl2 = .txtCustCD_M
                    msg2 = "荷主(中)コード"
                    lbl2 = .lblCustNM_M
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                    ctl3 = .txtCustCD_S
                    lbl3 = .lblCustNM_S
                    msg3 = "荷主(小)コード"
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                    '荷主(大)、(中)に値が入っていない場合ラベルをクリア
                    If String.IsNullOrEmpty(ctl1.TextValue) = True Then
                        lbl1.TextValue = String.Empty
                    End If

                    If String.IsNullOrEmpty(ctl2.TextValue) = True Then
                        lbl2.TextValue = String.Empty
                    End If

                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                    If String.IsNullOrEmpty(ctl3.TextValue) = True Then
                        lbl3.TextValue = String.Empty
                    End If
                    '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                    setFlg = True

                Case .txtGoodsCd.Name

                    ctl1 = .txtGoodsCd
                    msg1 = "商品指定"
                    lbl1 = .lblGoodsNm

                    '商品コードに値が入力されていない場合ラベルをクリア
                    If String.IsNullOrEmpty(ctl1.TextValue) = True Then
                        lbl1.TextValue = String.Empty
                    End If
            End Select

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            Dim ctlChk3 As Boolean = ctl3 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If .cmbPrint.SelectedValue.ToString.Trim().Equals(LMC050C.PRINT02) = True Then
                '日別出荷報告書
                If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                    OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) _
                    OrElse (ctlChk3 = False AndAlso ctl3.ReadOnly = True) Then

                    Select Case actionType
                        Case LMC050C.EventShubetsu.MASTEROPEN
                            Return Me._LMCconV.SetFocusErrMessage()

                        Case LMC050C.EventShubetsu.ENTER
                            'Enterの場合はメッセージは設定しない
                            Return False

                    End Select
                End If
            Else
                '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

                '通常帳票
                If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                    OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                    Select Case actionType
                        Case LMC050C.EventShubetsu.MASTEROPEN
                            Return Me._LMCconV.SetFocusErrMessage()

                        Case LMC050C.EventShubetsu.ENTER
                            'Enterの場合はメッセージは設定しない
                            Return False

                    End Select
                End If
            End If

            'Enterイベントの場合、空の場合、表示しない
            Select Case actionType

                Case LMC050C.EventShubetsu.ENTER

                    If setFlg = True Then

                        '両方に値がない場合、スルー
                        If String.IsNullOrEmpty(ctl1.TextValue) = True _
                            AndAlso String.IsNullOrEmpty(ctl2.TextValue) = True _
                            AndAlso String.IsNullOrEmpty(ctl3.TextValue) = True Then
                            lbl1.TextValue = String.Empty
                            lbl2.TextValue = String.Empty
                            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
                            lbl3.TextValue = String.Empty
                            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --
                            Return False
                        End If

                    Else

                        'ctl1に値がない 且つ (ctl2がNothing または ctl2に値がない)場合、スルー
                        If nullChk = True _
                            AndAlso String.IsNullOrEmpty(ctl1.TextValue) = True _
                            AndAlso (ctlChk = True OrElse String.IsNullOrEmpty(ctl2.TextValue) = True) _
                            AndAlso (ctlChk3 = True OrElse String.IsNullOrEmpty(ctl3.TextValue) = True) _
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
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 -- START --
            If ctlChk3 = False Then
                ctl3.ItemName = msg1
                ctl3.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl3) = False Then
                    Return False
                End If
            End If
            '(2013.01.20)埼玉BP対応 荷主(小)検索条件追加 --  END  --

            Return True

        End With

    End Function
#End Region

#Region "キャッシュテーブル"
    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd))

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '荷主コード（大）
        SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        If String.IsNullOrEmpty(custSCd) = False Then

            '荷主コード（小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_S = ", " '", custSCd, "' ")

        End If

        If String.IsNullOrEmpty(custSSCd) = False Then

            '荷主コード（極小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_SS = ", " '", custSSCd, "' ")

        End If

        Return SelectCustString

    End Function

#End Region
#End Region 'Method

End Class
