' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK     : 支払サブシステム
'  プログラムID     :  LMK060V : 支払印刷指示
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMK060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMK060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMK060F
    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMKconV As LMKControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMKconG As LMKControlG
#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMK060F, ByVal v As LMKControlV, ByVal g As LMKControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        Me._LMKconV = v

        Me._LMKconG = g

    End Sub

#End Region

#Region "入力チェック"

    ''' <summary>
    ''' 入力チェック
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

            '単項目チェック(共通)
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

            '運送会社コード
            .txtUnsocoCd.ItemName = "運送会社コード"
            .txtUnsocoCd.IsForbiddenWordsCheck = True
            .txtUnsocoCd.IsByteCheck = 5
            '支払運賃チェックリストの場合必須入力チェック
            If LMK060C.PRINT_SHIHARAI_CHECK.Equals(Print) = True Then
                .txtUnsocoCd.IsHissuCheck = True
            End If
            If MyBase.IsValidateCheck(.txtUnsocoCd) = False Then
                Return False
            End If

            '運送会社支店コード
            .txtUnsocoBrCd.ItemName = "運送会社支店コード"
            .txtUnsocoBrCd.IsForbiddenWordsCheck = True
            .txtUnsocoBrCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtUnsocoBrCd) = False Then
                Return False
            End If

            '支払運賃明細時のみチェック
            If LMK060C.PRINT_SHIHARAI_MEISAI.Equals(Print) = True Then
                '支払先コード
                .txtShiharaiCd.ItemName = "支払先コード"
                .txtShiharaiCd.IsForbiddenWordsCheck = True
                .txtShiharaiCd.IsByteCheck = 8
                If MyBase.IsValidateCheck(.txtShiharaiCd) = False Then
                    Return False
                End If
            End If

            '日付From
            .imdOutkaDateFrom.ItemName = "日付From"
            .imdOutkaDateFrom.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdOutkaDateFrom) = False Then
                Return False
            End If

            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdOutkaDateFrom, .imdOutkaDateFrom.ItemName) = False Then
                Return False
            End If

            '日付To
            .imdOutkaDateTo.ItemName = "日付To"
            .imdOutkaDateTo.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdOutkaDateTo) = False Then
                Return False
            End If

            'フルバイトチェック
            If Me.IsInputDateFullByteChk(.imdOutkaDateTo, .imdOutkaDateTo.ItemName) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷時の関連チェック(共通)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSaveCheck() As Boolean

        With Me._Frm

            '日付の大小チェック
            If Me.IsPrintDateChk() = False Then
                Return False
            End If

            '運送会社の存在チェック
            Dim UnsocoCd As String = .txtUnsocoCd.TextValue
            Dim UnsocoBrCd As String = .txtUnsocoBrCd.TextValue
            Dim dr As DataRow() = Nothing

            '運送会社が入力されていたら
            If String.IsNullOrEmpty(UnsocoCd) = False Then

                'キャッシュの運送会社マスタ
                dr = Me.SelectUnsocoListDataRow(UnsocoCd, UnsocoBrCd, "")

                '0件以上の場合
                If dr.Count <= 0 Then
                    'エラーメッセージ
                    Me._LMKconV.SetErrMessage("E079", New String() {"運送会社マスタ", String.Concat(UnsocoCd, "-", UnsocoBrCd)})
                    Me._LMKconV.SetErrorControl(.txtUnsocoCd)
                    Me._LMKconV.SetErrorControl(.txtUnsocoBrCd)
                    Return False
                End If

            End If

            '支払先の存在チェック
            If LMK060C.PRINT_SHIHARAI_MEISAI.Equals(.cmbPrint.SelectedValue) = True Then
                Dim ShiharaiCd As String = .txtShiharaiCd.TextValue
                Dim drs As DataRow() = Nothing

                '支払先コードが入力されているとき
                If String.IsNullOrEmpty(ShiharaiCd) = False Then

                    'キャッシュの支払先マスタ
                    drs = Me.SelectShiharaiListDataRow(.cmbBr.SelectedValue.ToString, ShiharaiCd)

                    '0件だった場合
                    If drs.Count <= 0 Then
                        'エラーメッセージ
                        Me._LMKconV.SetErrMessage("E079", New String() {"支払先マスタ", ShiharaiCd})
                        Me._LMKconV.SetErrorControl(.txtShiharaiCd)
                        Return False
                    End If
                End If
            End If

            '支払先が入力されていない時、運送会社コード、運送会社支店コードが設定されていない時はエラー
            If LMK060C.PRINT_SHIHARAI_MEISAI.Equals(.cmbPrint.SelectedValue) = True Then
                If String.IsNullOrEmpty(.txtShiharaiCd.TextValue) = True Then
                    '運送会社コードが入力されているか
                    If String.IsNullOrEmpty(.txtUnsocoCd.TextValue) = True OrElse _
                    String.IsNullOrEmpty(.txtUnsocoBrCd.TextValue) = True Then
                        Me._LMKconV.SetErrMessage("E270", New String() {"運送会社コード", "支払先コード"})
                        Me._LMKconV.SetErrorControl(.txtUnsocoCd)
                        Me._LMKconV.SetErrorControl(.txtUnsocoBrCd)
                        Me._LMKconV.SetErrorControl(.txtShiharaiCd)
                        Return False
                    End If
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷時の日付チェック(共通)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
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
                Me._LMKconV.SetErrorControl(.imdOutkaDateTo)
                Return Me._LMKconV.SetErrMessage("E039", New String() {"日付To", "日付From"})
                Return False
            End If

            '上限チェック
            Dim limitDate As String = Convert.ToDateTime(DateFormatUtility.EditSlash(dateFrom)).AddMonths(1).ToString("yyyyMMdd")
            If limitDate < dateTo Then
                .imdOutkaDateFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._LMKconV.SetErrorControl(.imdOutkaDateTo)
                Return Me._LMKconV.SetErrMessage("E014", New String() {"印刷日付To" _
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

            .txtUnsocoCd.TextValue = .txtUnsocoCd.TextValue.Trim()
            .txtUnsocoBrCd.TextValue = .txtUnsocoBrCd.TextValue.Trim()
            .txtShiharaiCd.TextValue = .txtShiharaiCd.TextValue.Trim()

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

            Return Me._LMKconV.SetErrMessage("E038", New String() {str, "8"})

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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMK060C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMK060C.EventShubetsu.MASTEROPEN          'マスタ参照
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


            Case LMK060C.EventShubetsu.PRINT        '印刷
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



            Case LMK060C.EventShubetsu.TOJIRU           '閉じる
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMK060C.EventShubetsu) As Boolean

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

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    ctl1 = .txtUnsocoCd
                    msg1 = "運送会社コード"
                    lbl1 = .lblUnsocoNm
                    ctl2 = .txtUnsocoBrCd
                    msg2 = "運送会社支店コード"
                    lbl2 = .lblUnsocoBrNm
                    '運送会社、運送会社支店に値が入っていない場合ラベルをクリア
                    If String.IsNullOrEmpty(ctl1.TextValue) = True Then
                        lbl1.TextValue = String.Empty
                    End If

                    If String.IsNullOrEmpty(ctl2.TextValue) = True Then
                        lbl2.TextValue = String.Empty
                    End If

                    setFlg = True

                Case .txtShiharaiCd.Name
                    ctl1 = .txtShiharaiCd
                    lbl1 = .lblShiharaiNm
                    msg1 = "支払先コード"

                    '支払先に値が入っていない場合ラベルをクリア
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

                    Case LMK060C.EventShubetsu.MASTEROPEN

                        Return Me._LMKconV.SetFocusErrMessage

                    Case LMK060C.EventShubetsu.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select

            End If


            'Enterイベントの場合、空の場合、表示しない
            Select Case actionType

                Case LMK060C.EventShubetsu.ENTER

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
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsocoCd">運送会社コード</param>
    ''' <param name="unsocoBrCd">運送会社支店コード</param>
    ''' <param name="shiharaiCd">支払先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(Optional ByVal unsocoCd As String = "" _
                                             , Optional ByVal unsocoBrCd As String = "" _
                                             , Optional ByVal shiharaiCd As String = "" _
                                             ) As DataRow()
        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(unsocoCd, unsocoBrCd, shiharaiCd))

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsocoCd">運送会社コード</param>
    ''' <param name="unsocoBrCd">運送会社支店コード</param>
    ''' <param name="shiharaiCd">支払先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoString(ByVal unsocoCd As String _
                                     , ByVal unsocoBrCd As String _
                                     , ByVal shiharaiCd As String _
                                     ) As String

        SelectUnsocoString = String.Empty

        '削除フラグ
        SelectUnsocoString = String.Concat(SelectUnsocoString, " SYS_DEL_FLG = '0' ")

        If String.IsNullOrEmpty(unsocoCd) = False Then
            '運送会社コード
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_CD = ", " '", unsocoCd, "' ")
        End If

        If String.IsNullOrEmpty(unsocoBrCd) = False Then

            '運送会社支店コード
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_BR_CD = ", " '", unsocoBrCd, "' ")

        End If

        '支払先コード
        If String.IsNullOrEmpty(shiharaiCd) = False Then
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNCHIN_SEIQTO_CD = ", " '", shiharaiCd, "' ")
        End If
        Return SelectUnsocoString

    End Function

    ''' <summary>
    ''' 支払先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="shiharaiCd">支払先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiListDataRow(ByVal brCd As String, ByVal shiharaiCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAITO).Select(Me.SelectShiharaiString(brCd, shiharaiCd))

    End Function

    ''' <summary>
    ''' 支払先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="shiharaiCd">支払先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiString(ByVal brCd As String, ByVal shiharaiCd As String) As String

        SelectShiharaiString = String.Empty

        '営業所コード
        SelectShiharaiString = String.Concat(SelectShiharaiString, "NRS_BR_CD = ", " '", brCd, "' ")

        '支払先タリフコード
        SelectShiharaiString = String.Concat(SelectShiharaiString, " AND ", "SHIHARAITO_CD = ", " '", shiharaiCd, "' ")

        Return SelectShiharaiString

    End Function

#End Region

End Class
