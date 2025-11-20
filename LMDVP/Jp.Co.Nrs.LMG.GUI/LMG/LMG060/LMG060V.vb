' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG060V : 請求印刷指示
'  作  成  者       :  [hishikari]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMG060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMG060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG060F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGconV As LMGControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGconG As LMGControlG

    '要望番号1832 -- START --
    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMGconH As LMGControlH
    '要望番号1832 --  END  --

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG060F, ByVal v As LMGControlV, ByVal g As LMGControlG, ByVal h As LMGControlH)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        Me._LMGconV = v

        Me._LMGconG = g

        Me._LMGconH = h

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

            '**********単項目チェック(運賃請求明細書(連続)以外)
            If .cmbPrint.SelectedValue.Equals(LMG060C.PRINT_UNCHIN_RENZOKU) = False Then

                '荷主(大)コード
                .txtCustCdL.ItemName = "荷主(大)コード"

                '運賃チェックリストの場合必須入力チェック
                If LMG060C.PRINT_UNCHIN_CHECK.Equals(Print) = True Then
                    .txtCustCdL.IsHissuCheck = True
                End If
                .txtCustCdL.IsForbiddenWordsCheck = True
                .txtCustCdL.IsFullByteCheck = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If

                '荷主(中)コード
                .txtCustCdM.ItemName = "荷主(中)コード"

                'START YANAI 要望番号592
                ''運賃チェックリストの場合必須入力チェック
                'If LMG060C.PRINT_UNCHIN_CHECK.Equals(Print) = True Then
                '    .txtCustCdM.IsHissuCheck = True
                'End If
                'END YANAI 要望番号592

                .txtCustCdM.IsForbiddenWordsCheck = True
                .txtCustCdM.IsFullByteCheck = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If

                '請求先コード
                .txtSeiqCd.ItemName = "請求先コード"
                .txtSeiqCd.IsForbiddenWordsCheck = True
                '.txtSeiqCd.IsFullByteCheck = 7
                If MyBase.IsValidateCheck(.txtSeiqCd) = False Then
                    Return False
                End If

            End If

            '**********単項目チェック(印刷種別共通)

            '運賃チェックリストの場合はチェックをしない
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

            '**********単項目チェック(運賃請求明細書(連続)のみ)
            If .cmbPrint.SelectedValue.Equals(LMG060C.PRINT_UNCHIN_RENZOKU) = True Then

                Dim arr As ArrayList = Nothing
                arr = Me._LMGconH.GetCheckList(.sprDetail.ActiveSheet, LMG060C.SprColumnIndex.DEF)

                '選択チェック
                If Me.IsSelectDataChk(arr) = False Then
                    Return False
                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk(ByVal arr As ArrayList) As Boolean

        '選択チェック
        If Me._LMGconV.IsSelectChk(arr.Count) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

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

            '荷主が入力されていたら
            'START YANAI 要望番号592
            'If String.IsNullOrEmpty(CustCdL) = False AndAlso _
            'String.IsNullOrEmpty(CustCdM) = False Then
            If String.IsNullOrEmpty(CustCdL) = False Then
                'END YANAI 要望番号592

                'キャッシュの荷主マスタ
                drc = Me.SelectCustListDataRow(.cmbBr.SelectedValue.ToString(), CustCdL, CustCdM, "")

                '0件以上の場合
                If drc.Count <= 0 Then
                    'エラーメッセージ
                    Me.SetErrMessage("E079", New String() {"荷主マスタ", String.Concat(CustCdL, "-", CustCdM)})
                    Me._LMGconV.SetErrorControl(.txtCustCdL)
                    Me._LMGconV.SetErrorControl(.txtCustCdM)
                    Return False
                End If

                '荷主コードでの荷主締め基準チェック
                If Me.SimeKijun(drc) = False Then

                    'エラーメッセージ
                    Me.SetErrMessage("E344", New String() {"荷主コード"})
                    Me._LMGconV.SetErrorControl(.txtCustCdL)
                    Me._LMGconV.SetErrorControl(.txtCustCdM)
                    Return False

                End If


            End If

            Dim SeiqCd As String = .txtSeiqCd.TextValue
            '荷主のキャッシュから締め基準の取得
            '請求先コードを元に荷主マスターの主請求先マスタコード
            Dim dr As DataRow() = Nothing

            '請求先コードが入力されているとき
            If String.IsNullOrEmpty(SeiqCd) = False Then

                'キャッシュの請求先マスター
                drc = Me.SelectSeiqtoListDataRow(.cmbBr.SelectedValue.ToString, SeiqCd)

                '0件だった場合
                If drc.Count <= 0 Then
                    'エラーメッセージ
                    Me.SetErrMessage("E079", New String() {"請求先マスタ", SeiqCd})
                    Me._LMGconV.SetErrorControl(.txtSeiqCd)

                    Return False
                End If

                '荷主マスタのキャッシュから運賃締め基準を取得
                dr = Me.SelectCustListDataRow(.cmbBr.SelectedValue.ToString, "", "", SeiqCd)

                If dr.Count > 1 Then

                    '請求先コードでの荷主締め基準チェック
                    If Me.SimeKijun(dr) = False Then
                        'エラーメッセージ
                        Me.SetErrMessage("E344", New String() {"請求先コード"})
                        Me._LMGconV.SetErrorControl(.txtSeiqCd)
                        Return False
                    End If

                End If

            End If

            'START KIM 2012/11/21 要望番号：1588

            ''請求先が入力されていない時、荷主コード(大)、(中)が設定されていない時はエラー
            ''START YANAI 要望番号592
            ''If String.IsNullOrEmpty(.txtSeiqCd.TextValue) = True Then
            ''    '荷主コードが入力されているか
            ''    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True OrElse _
            ''    String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
            ''        Me.SetErrMessage("E270", New String() {"請求先コード", "荷主コード"})
            ''        Me._LMGconV.SetErrorControl(.txtCustCdL)
            ''        Me._LMGconV.SetErrorControl(.txtCustCdM)
            ''        Me._LMGconV.SetErrorControl(.txtSeiqCd)
            ''        Return False
            ''    End If
            ''End If
            ''(2012.09.25) 追加START 運賃請求明細書(出荷)
            'If LMG060C.PRINT_UNCHIN_SEIKYU.Equals(.cmbPrint.SelectedValue) = True OrElse _
            '                LMG060C.PRINT_UNCHIN_TARIFF.Equals(.cmbPrint.SelectedValue) = True OrElse _
            '                LMG060C.PRINT_UNCHIN_OUTKA.Equals(.cmbPrint.SelectedValue) = True Then
            '    '(2012.09.25) 追加END 運賃請求明細書(出荷)
            '    If String.IsNullOrEmpty(.txtSeiqCd.TextValue) = True Then
            '        '荷主コードが入力されているか
            '        If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True OrElse _
            '        String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
            '            Me.SetErrMessage("E270", New String() {"請求先コード", "荷主コード"})
            '            Me._LMGconV.SetErrorControl(.txtCustCdL)
            '            Me._LMGconV.SetErrorControl(.txtCustCdM)
            '            Me._LMGconV.SetErrorControl(.txtSeiqCd)
            '            Return False
            '        End If
            '    End If
            'End If
            ''END YANAI 要望番号592

            If LMG060C.PRINT_UNCHIN_TARIFF.Equals(.cmbPrint.SelectedValue) = True OrElse _
               LMG060C.PRINT_UNCHIN_OUTKA.Equals(.cmbPrint.SelectedValue) = True Then
                If String.IsNullOrEmpty(.txtSeiqCd.TextValue) = True Then
                    '荷主コードが入力されているか
                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True OrElse _
                    String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        Me.SetErrMessage("E270", New String() {"請求先コード", "荷主コード"})
                        Me._LMGconV.SetErrorControl(.txtCustCdL)
                        Me._LMGconV.SetErrorControl(.txtCustCdM)
                        Me._LMGconV.SetErrorControl(.txtSeiqCd)
                        Return False
                    End If
                End If
            End If

            '運賃請求明細書印刷で,請求先、荷主コード(大)、(中)のいずれも設定されていない場合、ワーニング
            If LMG060C.PRINT_UNCHIN_SEIKYU.Equals(.cmbPrint.SelectedValue) = True Then
                If String.IsNullOrEmpty(.txtSeiqCd.TextValue) = True AndAlso _
                   String.IsNullOrEmpty(.txtCustCdL.TextValue) = True AndAlso _
                   String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                    Dim rtn As MsgBoxResult = MyBase.ShowMessage("W222", New String() {"運賃請求明細書"})
                    If rtn.Equals(MsgBoxResult.Cancel) = True OrElse rtn.Equals(MsgBoxResult.No) = True Then
                        Return False
                    End If
                End If
            End If
            'END   KIM 2012/11/21 要望番号：1588

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
                Me._LMGconV.SetErrorControl(.imdOutkaDateTo)
                Return Me.SetErrMessage("E039", New String() {"日付To", "日付From"})
                Return False
            End If

            '上限チェック
            Dim limitDate As String = Convert.ToDateTime(DateFormatUtility.EditSlash(dateFrom)).AddMonths(1).ToString("yyyyMMdd")
            If limitDate < dateTo Then
                .imdOutkaDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMGconV.SetErrorControl(.imdOutkaDateTo)
                Return Me.SetErrMessage("E014", New String() {"印刷日付To" _
                                                              , DateFormatUtility.EditSlash(dateFrom) _
                                                              , DateFormatUtility.EditSlash(limitDate) _
                                                              })

            End If
            Return True

        End With

    End Function


    ''' <summary>
    ''' 運賃締め基準のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SimeKijun(ByVal dr As DataRow()) As Boolean


        '0レコード目の締め基準を取得
        Dim sime As String = dr(0).Item("UNTIN_CALCULATION_KB").ToString

        Dim count As Integer = dr.Count

        For i As Integer = 0 To count - 1

            '締め基準0レコードの締め基準と一致しているか判定
            If sime.Equals(dr(i).Item("UNTIN_CALCULATION_KB").ToString) = False Then

                Return False
                Exit For
            End If
        Next

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
            .txtSeiqCd.TextValue = .txtSeiqCd.TextValue.Trim()

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

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMG060C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMG060C.EventShubetsu.MASTEROPEN          'マスタ参照
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


            Case LMG060C.EventShubetsu.PRINT        '印刷
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



            Case LMG060C.EventShubetsu.TOJIRU           '閉じる
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

#End Region

#Region "フォーカス位置チェック"
    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMG060C.EventShubetsu) As Boolean

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

                Case .txtSeiqCd.Name
                    ctl1 = .txtSeiqCd
                    lbl1 = .lblSeiqNm
                    msg1 = "請求先コード"

                    '請求先に値が入っていない場合ラベルをクリア
                    If String.IsNullOrEmpty(ctl1.TextValue) = True Then
                        lbl1.TextValue = String.Empty
                    End If



            End Select

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                Select Case actionType

                    Case LMG060C.EventShubetsu.MASTEROPEN

                        Return Me.SetFocusErrMessage()

                    Case LMG060C.EventShubetsu.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select

            End If


            'Enterイベントの場合、空の場合、表示しない
            Select Case actionType

                Case LMG060C.EventShubetsu.ENTER

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
    Friend Function SelectCustListDataRow(ByVal brCd As String _
                                             , Optional ByVal custLCd As String = "" _
                                             , Optional ByVal custMCd As String = "" _
                                             , Optional ByVal seiqCd As String = "" _
                                             ) As DataRow()
        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(brCd, custLCd, custMCd, seiqCd))

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

        '営業所コード　20160930 要番2622 tsunehira add 
        SelectCustString = String.Concat(SelectCustString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")


        If String.IsNullOrEmpty(custLCd) = False Then
            '荷主コード（大）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")
        End If

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        '請求先コード
        If String.IsNullOrEmpty(seiqCd) = False Then
            SelectCustString = String.Concat(SelectCustString, " AND ", "UNCHIN_SEIQTO_CD = ", " '", seiqCd, "' ")
        End If
        Return SelectCustString

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSeiqtoListDataRow(ByVal brCd As String, ByVal seqtoCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(Me.SelectSeiqtoString(brCd, seqtoCd))

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoString(ByVal brCd As String, ByVal seqtoCd As String) As String

        SelectSeiqtoString = String.Empty

        '削除フラグ
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '請求先タリフコード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "SEIQTO_CD = ", " '", seqtoCd, "' ")

        Return SelectSeiqtoString

    End Function
#End Region
#End Region

#Region "検索ボタン押下時チェック"

    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '営業所
            .cmbBr.ItemName() = "営業所"
            .cmbBr.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbBr) = False Then
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '荷主名(大)
            vCell.SetValidateCell(0, LMG060G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName() = "荷主名(大)"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名(中)
            vCell.SetValidateCell(0, LMG060G.sprDetailDef.CUST_NM_M.ColNo)
            vCell.ItemName() = "荷主名(中)"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(大)
            vCell.SetValidateCell(0, LMG060G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName = "荷主コード(大)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 5
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主コード(中)
            vCell.SetValidateCell(0, LMG060G.sprDetailDef.CUST_CD_M.ColNo)
            vCell.ItemName = "荷主コード(中)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

End Class
