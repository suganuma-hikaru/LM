' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD     : 在庫
'  プログラムID     :  LMD070V : 在庫帳票印刷
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMD070Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD070V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD070F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconV As LMDControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMDconG As LMDControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD070F, ByVal v As LMDControlV, ByVal g As LMDControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMDconV = v

        Me._LMDconG = g

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
    Friend Function IsInputCheck(ByVal flg As String) As Boolean

        With Me._Frm

            '印刷種別の取得
            Dim Print As String = .cmbPrint.SelectedValue.ToString
            Dim PrintSub As String = .cmbPrintSub.SelectedValue.ToString

            'スペース除去
            Call Me.TrimSpaceTextValue()

            '単項目チェック()
            If Me.IsPrintKyoutuCheck(flg, Print, PrintSub) = False Then
                Return False
            End If

            Select Case Print
                Case LMD070C.PRINT_NITI
                    '日次出荷別在庫リストの場合
                    If Me.IsPrintNitiCheck() = False Then

                        Return False

                    End If

                Case LMD070C.PRINT_ZAIKO, LMD070C.PRINT_ZAIKO_SHOUMEI, LMD070C.PRINT_ZAIKO_SHOUMEI_S_SS, LMD070C.PRINT_SYOUBOU_BUNRUI
                    'Case LMD070C.PRINT_ZAIKO, LMD070C.PRINT_ZAIKO_SHOUMEI

                    '在庫表、在庫証明書、在庫証明書(小･極小毎)の場合
                    If Me.IsPrintZaikoCheck(Print) = False Then
                        Return False
                    End If

                Case LMD070C.PRINT_NYUSHUKA, LMD070C.PRINT_FUDOU

                    '入出荷履歴表、不動在庫の場合
                    If Me.IsPrintSetDateCheck(Print) = False Then

                        Return False

                    End If

                Case LMD070C.PRINT_ZAIKO_UKEHARAI

                    '日付必須From,toチェック
                    If Me.IsPrintZaikoCheck(Print, PrintSub) = False Then
                        Return False
                    End If
            End Select



            '関連チェック(共通)
            If Me.IsPrintSaveCheck(Print, flg) = False Then
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
    Private Function IsPrintKyoutuCheck(ByVal flg As String, ByVal Print As String, ByVal PrintSub As String) As Boolean

        With Me._Frm


            '**********単項目チェック(印刷種別共通)

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            If Print.Equals(LMD070C.PRINT_ZAIKO_UKEHARAI) Then
                '印刷種別サブ
                .cmbPrintSub.ItemName = "印刷種別サブ"
                .cmbPrintSub.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbPrintSub) = False Then
                    Return False
                End If
            End If

            '営業所
            .cmbEigyo.ItemName = "営業所"
            .cmbEigyo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '印刷種別が在庫整合性リスト(08)の場合はチェックをしない
            'If LMD070C.PRINT_ZAIKO_SEIGOUSEI_JITU.Equals(Print) = False AndAlso _
            'LMD070C.PRINT_ZAIKO_SEIGOUSEI_SHUKA.Equals(Print) = False AndAlso _
            'LMD070C.PRINT_ZAIKO_SEIGOUSEI_HIKI.Equals(Print) = False Then

            If Print.Equals(LMD070C.PRINT_ZAIKO_UKEHARAI) = False Then
                If Print.Equals(LMD070C.PRINT_SYOUBOU_BUNRUI_ALL) = False Then
                    '荷主(大)コード
                    .txtCustCdL.ItemName = "荷主(大)コード"
                    .txtCustCdL.IsHissuCheck = True
                    .txtCustCdL.IsForbiddenWordsCheck = True
                    .txtCustCdL.IsByteCheck = 5
                    If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                        Return False
                    End If

                    '荷主(中)コード
                    .txtCustCdM.ItemName = "荷主(中)コード"
                    .txtCustCdM.IsHissuCheck = True
                    .txtCustCdM.IsForbiddenWordsCheck = True
                    .txtCustCdM.IsByteCheck = 2
                    If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                        Return False
                    End If
                End If
            End If

            '荷主(小)コード
            .txtCustCdS.ItemName = "荷主(小)コード"
            .txtCustCdS.IsForbiddenWordsCheck = True
            .txtCustCdS.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdS) = False Then
                Return False
            End If

            '荷主(極小)コード
            .txtCustCdSs.ItemName = "荷主(極小)コード"
            .txtCustCdSs.IsForbiddenWordsCheck = True
            .txtCustCdSs.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdSs) = False Then
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 単項目チェック(日次出荷別在庫リスト)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintNitiCheck() As Boolean

        With Me._Frm

            '印刷種別"日次出荷別在庫リスト"
            Dim errorFlg As Boolean = False
            '出荷日
            .imdSyukkaDate.ItemName = "出荷日"
            .imdSyukkaDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdSyukkaDate) = False Then
                Return False
            End If
            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdSyukkaDate, .imdSyukkaDate.ItemName) = errorFlg Then
                Return errorFlg
            End If

            Return True
        End With
    End Function

    ''' <summary>
    ''' 単項目チェック(在庫表、在庫証明書、在庫受払表、入出荷履歴表)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintZaikoCheck(ByVal Print As String, Optional ByVal Printsub As String = "") As Boolean

        With Me._Frm

            Dim errorFlg As Boolean = False
            '印刷種別"在庫表"、"在庫証明書"、"入出荷履歴表"
            '印刷範囲(From)
            .imdPrintDateS.ItemName = "印刷範囲(From)"
            .imdPrintDateS.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdPrintDateS) = False Then
                Return False
            End If
            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdPrintDateS, .imdPrintDateS.ItemName) = errorFlg Then
                Return errorFlg
            End If

            '印刷種別が入出荷履歴表、不動在庫リストの場合チェックをする
            If LMD070C.PRINT_FUDOU.Equals(Print) OrElse _
            LMD070C.PRINT_NYUSHUKA.Equals(Print) OrElse _
            LMD070C.PRINT_ZAIKO_UKEHARAI.Equals(Print) Then

                Select Case Printsub

                    Case LMD070C.PRINTSUB_ZAIKO_NORMAL, _
                        LMD070C.PRINTSUB_ZAIKO_NONINUSHI, _
                        LMD070C.PRINTSUB_ZAIKO_SOKO, _
                        LMD070C.PRINTSUB_ZAIKO_OKIBA_NORMAL, _
                        LMD070C.PRINTSUB_ZAIKO_OKIBA_NONINUSHI, _
                        LMD070C.PRINTSUB_ZAIKO_OKIBA_KIKEN, _
                        String.Empty

                        '印刷種別:入出荷履歴表、不動在庫リスト
                        '印刷範囲(To)
                        .imdPrintDateE.ItemName = "印刷範囲(To)"
                        .imdPrintDateE.IsHissuCheck = True
                        If MyBase.IsValidateCheck(.imdPrintDateE) = False Then
                            Return False
                        End If
                        'フルバイトチェック
                        If Me.IsInputDateFullByteChk(.imdPrintDateE, .imdPrintDateE.ItemName) = errorFlg Then
                            Return errorFlg
                        End If

                End Select

            End If

            'START YANAI 要望番号1057 在庫証明書出力順変更
            '出荷順
            'UPD Start 2022/10/21 033003 消防分類別在庫重量表は出力順のチェック不要
            'If Print.Equals(LMD070C.PRINT_ZAIKO_UKEHARAI) = False Then
            If Print.Equals(LMD070C.PRINT_ZAIKO_UKEHARAI) = False And Print.Equals(LMD070C.PRINT_SYOUBOU_BUNRUI) = False Then
                'UPD End   2022/10/21 033003
                .cmbSort.ItemName = .lblSort.TextValue
                .cmbSort.IsHissuCheck = True
                If MyBase.IsValidateCheck(.cmbSort) = False Then
                    Return False
                End If
            End If
            'END YANAI 要望番号1057 在庫証明書出力順変更

        End With
        Return True
    End Function

    ''' <summary>
    ''' 単項目チェック(入出荷履歴表、不動在庫)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSetDateCheck(ByVal Print As String) As Boolean

        With Me._Frm

            'セット入力チェック(両方とも入力いなかったらエラー
            If String.IsNullOrEmpty(.imdPrintDateE.TextValue) = True AndAlso _
             String.IsNullOrEmpty(.imdPrintDateS.TextValue) = True Then

                Call Me._LMDconV.SetErrorControl(.imdPrintDateS)
                Call Me._LMDconV.SetErrorControl(.imdPrintDateE)
                Return Me._LMDconV.SetErrMessage("E270", New String() {"印刷範囲(From)", "印刷範囲(To)"})

            End If

        End With
        Return True
    End Function

    ''' <summary>
    ''' 印刷時の関連チェック(共通)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSaveCheck(ByVal Print As String, ByVal flg As String) As Boolean
        Dim drs As DataRow() = Nothing

        With Me._Frm

            Dim brcd As String = .cmbEigyo.SelectedValue.ToString
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue
            Dim CustCdS As String = .txtCustCdS.TextValue
            Dim CustCdSS As String = .txtCustCdSs.TextValue

            'セット入力チェック
            If Me.IsCustLMSetChk(CustCdL, CustCdM) = False Then
                Return False
            End If

            '(2012.12.14)要望番号1671対応の一環として -- START --
            'セット入力チェック(荷主コード(小)・荷主コード(極小))
            If Me.IsCustSSsSetChk(CustCdS, CustCdSS) = False Then
                Return False
            End If
            '(2012.12.14)要望番号1671対応の一環として --  END  --

            '存在チェック(荷主マスタ)
            If String.IsNullOrEmpty(CustCdL) = False AndAlso _
            String.IsNullOrEmpty(CustCdM) = False Then
                drs = Me._LMDconV.SelectCustListDataRow(CustCdL, CustCdM, CustCdS, CustCdSS)

                Dim count As Integer = drs.Count

                If count < 1 Then
                    Call Me._LMDconV.SetErrorControl(.txtCustCdL)
                    Call Me._LMDconV.SetErrorControl(.txtCustCdM)

                    '(2012.12.13)要望番号1671 在庫証明書条件追加 -- START --
                    If CustCdS.ToString.Trim.Equals("") = False Then
                        Call Me._LMDconV.SetErrorControl(.txtCustCdS)

                        If CustCdSS.ToString.Trim.Equals("") = False Then
                            Call Me._LMDconV.SetErrorControl(.txtCustCdSs)
                            Return Me._LMDconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM, "-", CustCdS, "-", CustCdSS)})

                        End If
                        Return Me._LMDconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM, "-", CustCdS)})

                    End If
                    '(2012.12.13)要望番号1671 在庫証明書条件追加 --  END  --

                    Return Me._LMDconV.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM)})

                End If

                'マスタ情報を設定
                Call Me.SetCustData(.txtCustCdL, .lblCustNmL, drs(0).Item("CUST_CD_L").ToString(), drs(0).Item("CUST_NM_L").ToString())
                Call Me.SetCustData(.txtCustCdM, .lblCustNmM, drs(0).Item("CUST_CD_M").ToString(), drs(0).Item("CUST_NM_M").ToString())
                Call Me.SetCustData(.txtCustCdS, .lblCustNmS, drs(0).Item("CUST_CD_S").ToString(), drs(0).Item("CUST_NM_S").ToString())
                Call Me.SetCustData(.txtCustCdSs, .lblCustNmSs, drs(0).Item("CUST_CD_SS").ToString(), drs(0).Item("CUST_NM_SS").ToString())

            End If

            '印刷範囲の大小チェック
            'エラーフラグ
            Dim errorFlg As Boolean = False

            '印刷範囲Fromの値
            Dim PrintFrom As String = .imdPrintDateS.TextValue

            '印刷範囲Toの値
            Dim PrintTo As String = .imdPrintDateE.TextValue

            '(印刷日付From、印刷日付To)がない場合、スルー
            If String.IsNullOrEmpty(PrintFrom) = False _
                 AndAlso String.IsNullOrEmpty(PrintTo) = False Then
                '印刷日付の大小チェック
                If Me.IsLargeSmallChk(PrintTo, PrintFrom, False) = False Then
                    .imdPrintDateS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .imdPrintDateE.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._LMDconV.SetErrorControl(.imdPrintDateE)
                    Return Me._LMDconV.SetErrMessage("E039", New String() {"印刷範囲(To)", "印刷範囲(From)"})
                End If

            End If

            '印刷種別によって関連チェック項目変更
            Select Case Print

                Case LMD070C.PRINT_ZAIKO
                    '在庫表
                    '関連チェック
                    If Me.ZaikoHyou(PrintFrom, flg) = False Then
                        Return False
                    End If

                Case LMD070C.PRINT_ZAIKO_SHOUMEI
                    '在庫証明書
                    If Me.ZaikoShoumei(PrintFrom, flg) = False Then
                        Return False
                    End If

                Case LMD070C.PRINT_ZAIKO_SEIGOUSEI_JITU, LMD070C.PRINT_ZAIKO_SEIGOUSEI_SHUKA, LMD070C.PRINT_ZAIKO_SEIGOUSEI_HIKI
                    '在庫整合性チェック
                    '印刷範囲(From)、月末在庫の大小チェック
                    If Me.GetumatuZaiko(PrintFrom) = False Then
                        Return False
                    End If

                    '(2012.12.13)要望番号1671 在庫証明書条件追加 -- START --
                Case LMD070C.PRINT_ZAIKO_SHOUMEI_S_SS
                    '在庫証明書
                    If Me.ZaikoShoumei_S_SS(PrintFrom, flg) = False Then
                        Return False
                    End If
                    '(2012.12.13)要望番号1671 在庫証明書条件追加 --  END  --

            End Select

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷主のセット入力チェック
    ''' </summary>
    ''' <param name="custCdL">荷主(大)コード</param>
    ''' <param name="custCdM">荷主(中)コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustLMSetChk(ByVal custCdL As String, ByVal custCdM As String) As Boolean

        '両方とも空の場合、スルー
        If String.IsNullOrEmpty(custCdL) = True _
            AndAlso String.IsNullOrEmpty(custCdM) = True _
            Then
            Return True
        End If

        '両方とも値がある場合、スルー
        If String.IsNullOrEmpty(custCdL) = False _
            AndAlso String.IsNullOrEmpty(custCdM) = False _
            Then
            Return True
        End If

        '片方の場合、エラー
        Me._Frm.txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
        Me._LMDconV.SetErrorControl(Me._Frm.txtCustCdL)
        Return Me._LMDconV.SetErrMessage("E017", New String() {"荷主(大)コード", "荷主(中)コード"})

    End Function

    '(2012.12.14)要望番号1671対応の一環として -- START --
    ''' <summary>
    ''' 荷主のセット入力チェック
    ''' </summary>
    ''' <param name="custCdS">荷主(小)コード</param>
    ''' <param name="custCdSs">荷主(極小)コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustSSsSetChk(ByVal custCdS As String, ByVal custCdSs As String) As Boolean

        '荷主(極小)コードのみ入力の場合はエラー
        If String.IsNullOrEmpty(custCdS) = True _
            And String.IsNullOrEmpty(custCdSs) = False Then

            '片方の場合、エラー
            Me._Frm.txtCustCdS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._LMDconV.SetErrorControl(Me._Frm.txtCustCdS)
            Return Me._LMDconV.SetErrMessage("E001", New String() {"荷主(極小)コードを入力している場合、荷主(小)コード"})

        End If

        Return True

    End Function
    '(2012.12.14)要望番号1671対応の一環として --  END  --

    ''' <summary>
    ''' 荷主マスタ情報反映
    ''' </summary>
    ''' <param name="ctl">テキストコントロール</param>
    ''' <param name="lblCtl">ラベルコントロール</param>
    ''' <param name="cd">コード</param>
    ''' <param name="nm">名称</param>
    ''' <remarks></remarks>
    Private Sub SetCustData(ByVal ctl As Win.InputMan.LMImTextBox, ByVal lblCtl As Win.InputMan.LMImTextBox, ByVal cd As String, ByVal nm As String)

        'テキストに値がない場合、ラベルをクリア
        If String.IsNullOrEmpty(ctl.TextValue) = True Then
            lblCtl.TextValue = String.Empty
            Exit Sub
        End If

        'マスタ情報を反映
        ctl.TextValue = cd
        lblCtl.TextValue = nm

    End Sub

    ''' <summary>
    ''' 在庫表の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ZaikoHyou(ByVal PrintFrom As String, ByVal flg As String) As Boolean

        With Me._Frm


            '印刷
            If LMD070C.PRINT_FLG.Equals(flg) = True Then
                '印刷範囲(From)、月末在庫の大小チェック
                If Me.GetumatuZaiko(PrintFrom) = False Then
                    Return False

                End If
            End If

        End With
        Return True
    End Function

    ''' <summary>
    ''' 在庫証明書の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ZaikoShoumei(ByVal PrintFrom As String, ByVal flg As String) As Boolean
        With Me._Frm

            '営業所、荷主コード(大)、荷主コード(中)の処理対象外チェック
            Dim NrsBrCd As String = .cmbEigyo.SelectedValue.ToString
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue

            '営業所コード、荷主コード(大)、荷主コード(中)を連結
            Dim NrsCust As String = String.Concat(NrsBrCd, CustCdL, CustCdM)


            'Z017から対象となるものを取得
            Dim dr As DataRow() = Nothing
            Dim Kbn As String = String.Empty
            dr = Me.SelectKbnListDataRow(LMKbnConst.KBN_Z017)

            If 0 < dr.Length Then
                For i As Integer = 0 To dr.Length - 1

                    '整合性チェック
                    '対象の荷主と一致していたらエラー
                    If NrsCust.Equals(dr(i).Item("KBN_NM1").ToString) = True Then

                        Call Me._LMDconV.SetErrorControl(.txtCustCdL)
                        Call Me._LMDconV.SetErrorControl(.txtCustCdM)

                        Return Me._LMDconV.SetErrMessage("E209", New String() {""})
                        Return False

                    End If
                Next
            End If

            '印刷範囲(From)、月末在庫大小チェック
            If Me.GetumatuZaiko(PrintFrom) = False Then
                Return False

            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 在庫証明書(小･極小)の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ZaikoShoumei_S_SS(ByVal PrintFrom As String, ByVal flg As String) As Boolean
        With Me._Frm

            '営業所、荷主コード(大)、荷主コード(中)の処理対象外チェック
            Dim NrsBrCd As String = .cmbEigyo.SelectedValue.ToString
            Dim CustCdL As String = .txtCustCdL.TextValue
            Dim CustCdM As String = .txtCustCdM.TextValue
            Dim CustCdS As String = .txtCustCdS.TextValue
            Dim CustCdSS As String = .txtCustCdSs.TextValue

            '営業所コード、荷主コード(大)、荷主コード(中)、荷主コード(小)、荷主コード(極小)を連結
            Dim NrsCust As String = String.Concat(NrsBrCd, CustCdL, CustCdM, CustCdS, CustCdSS)

            'Z017から対象となるものを取得
            Dim dr As DataRow() = Nothing
            Dim Kbn As String = String.Empty
            dr = Me.SelectKbnListDataRow(LMKbnConst.KBN_Z017)

            If 0 < dr.Length Then
                For i As Integer = 0 To dr.Length - 1

                    '整合性チェック
                    '対象の荷主と一致していたらエラー
                    If NrsCust.Equals(dr(i).Item("KBN_NM1").ToString) = True Then

                        Call Me._LMDconV.SetErrorControl(.txtCustCdL)
                        Call Me._LMDconV.SetErrorControl(.txtCustCdM)

                        If CustCdS.ToString.Trim.Equals("") = False Then
                            Call Me._LMDconV.SetErrorControl(.txtCustCdS)

                            If CustCdSS.ToString.Trim.Equals("") = False Then
                                Call Me._LMDconV.SetErrorControl(.txtCustCdSs)
                            End If

                        End If

                        Return Me._LMDconV.SetErrMessage("E209", New String() {""})
                        Return False

                    End If
                Next
            End If

            '印刷範囲(From)、月末在庫大小チェック
            If Me.GetumatuZaiko(PrintFrom) = False Then
                Return False

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷範囲(From)、月末在庫の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function GetumatuZaiko(ByVal printFrom As String) As Boolean

        With Me._Frm

            '印刷範囲(From)、月末在庫の大小チェック
            Dim getudata As String = .cmbDataInsDate.SelectedValue.ToString()

            '初期在庫の場合、チェックを行わない。
            If LMDControlC.FLG_ON.Equals(getudata) = True Then
                Return True
            End If

            '(印刷日付From、印刷日付To)がない場合、スルー
            If String.IsNullOrEmpty(getudata) = True _
                AndAlso String.IsNullOrEmpty(printFrom) = True Then
                Return True
            End If

            '印刷日付の大小チェック
            If Me.IsLargeSmallChk(printFrom, getudata, False) = False Then

                .imdPrintDateS.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                .cmbDataInsDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMDconV.SetErrorControl(.imdPrintDateS)
                Return Me._LMDconV.SetErrMessage("E039", New String() {"印刷範囲(From)", "月末在庫"})

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' INPUTパラメータチェック(Nullのﾁｪｯｸ)
    ''' </summary>
    ''' <param name="prmDs">データセット</param>
    ''' <param name="repStr">エラー時の置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function CheckParameter(ByVal prmDs As DataSet, ByVal repStr As String, ByVal chkObject As LMQControlC.ChkObject) As Boolean

        'NULLチェック
        If prmDs Is Nothing = True Then
            'MyBase.SetMessage("S001", New String() {repStr})
            'Return False
            Return Me._LMDconV.SetErrMessage("S001", New String() {repStr})
        End If

        'RECORD数チェック
        If prmDs.Tables(LMQControlC.TABLE_NM_OUT).Rows.Count < 1 Then
            'MyBase.SetMessage("E024")
            'Return False
            Return Me._LMDconV.SetErrMessage("E024")
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

    ''' <summary>
    ''' 日付のフルバイトチェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="str">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputDateFullByteChk(ByVal ctl As Win.InputMan.LMImDate, ByVal str As String) As Boolean

        If ctl.IsDateFullByteCheck = False Then

            Return Me._LMDconV.SetErrMessage("E038", New String() {str, "8"})

        End If

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

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtCustCdS.TextValue = .txtCustCdS.TextValue.Trim()
            .txtCustCdSs.TextValue = .txtCustCdSs.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMD070C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMD070C.EventShubetsu.MASTEROPEN          'マスタ参照
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


            Case LMD070C.EventShubetsu.PRINT        '印刷
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



            Case LMD070C.EventShubetsu.TOJIRU           '閉じる
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMD070C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm


            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name, .txtCustCdS.Name, .txtCustCdSs.Name

                    Dim custNm As String = .lblTitleCust.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNmL, .lblCustNmM, .lblCustNmS, .lblCustNmSs}
                    msg = New String() {"荷主コード(大)", "荷主コード(中)", "荷主コード(小)", "荷主コード(極小)"}

            End Select

        End With

        Return Me._LMDconV.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

    End Function

#End Region

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
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKbnListDataRow(ByVal groupCd As String _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(groupCd))

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal groupCd As String _
                                     ) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")


        '区分グループコード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        Return SelectKbnString

    End Function

#End Region

#End Region 'Method

End Class
