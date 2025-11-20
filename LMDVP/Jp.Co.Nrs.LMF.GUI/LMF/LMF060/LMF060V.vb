' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF060V : 運賃試算
'  作  成  者       :　菱刈
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF060Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF060V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF060F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMFControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMFControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF060F, ByVal v As LMFControlV, ByVal g As LMFControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "Main"

    ''' <summary>
    ''' 距離取得用の入力チェックメソッドです。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputKyoriCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック(距離取得)
        Dim rtnResult As Boolean = Me.IsKyoriSingleCheck()

        '関連チェック(距離取得)
        rtnResult = rtnResult AndAlso Me.IsKyoriSaveCheck()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運賃取得用の入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputUnchinCheck(ByVal data As String) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック(運賃取得用)
        Dim rtnResult As Boolean = Me.IsUnchinSingleCheck()

        '関連チェック(運賃取得)
        rtnResult = rtnResult AndAlso Me.IsUnchinSaveCheck(data)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 印刷用の入力チェックメソッドです。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <param name="nowDate">システム日付</param>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputPrintCheck(ByVal nowDate As String) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック(印刷用)
        Dim rtnResult As Boolean = Me.IsPrintSingleCheck()

        '関連チェック(印刷用)
        rtnResult = rtnResult AndAlso Me.IsPrintSaveCheck(nowDate)

        Return rtnResult

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        With Me._Frm

            .txtOrigJis.TextValue = .txtOrigJis.TextValue.Trim()
            .txtTodokedeJisCd.TextValue = .txtTodokedeJisCd.TextValue.Trim()
            .txtKyoriteiCd.TextValue = .txtKyoriteiCd.TextValue.Trim
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtKyoriteiCd.TextValue = .txtKyoriteiCd.TextValue.Trim()
            .txtTariffCd.TextValue = .txtTariffCd.TextValue.Trim()
            .txtWarimashiCd.TextValue = .txtWarimashiCd.TextValue.Trim()

        End With

    End Sub

#End Region

#Region "単項目チェック"

    ''' <summary>
    ''' 単項目チェック(距離取得)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKyoriSingleCheck() As Boolean

        With Me._Frm

            '**********単項目チェック
            '発地JISコード
            .txtOrigJis.ItemName = "発地JISコード"
            .txtOrigJis.IsHissuCheck = True
            .txtOrigJis.IsForbiddenWordsCheck = True
            .txtOrigJis.IsByteCheck = 7
            .txtOrigJis.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtOrigJis) = False Then
                Return False
            End If

            '届先JISコード
            .txtTodokedeJisCd.ItemName = "届先JISコード"
            .txtTodokedeJisCd.IsHissuCheck = True
            .txtTodokedeJisCd.IsForbiddenWordsCheck = True
            .txtTodokedeJisCd.IsByteCheck = 7
            .txtTodokedeJisCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtTodokedeJisCd) = False Then
                Return False
            End If

            '距離程コード
            .txtKyoriteiCd.ItemName = "距離程コード"
            .txtKyoriteiCd.IsHissuCheck = True
            .txtKyoriteiCd.IsForbiddenWordsCheck = True
            .txtKyoriteiCd.IsFullByteCheck = 3
            .txtKyoriteiCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtKyoriteiCd) = False Then
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック(運賃取得)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinSingleCheck() As Boolean

        With Me._Frm

            '**********単項目チェック(運賃取得)

            'タリフ分類
            .cmbUnso.ItemName = "タリフ分類"
            .cmbUnso.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbUnso) = False Then
                Return False
            End If

            'タリフコード
            .txtTariffCd.ItemName = "タリフコード"
            .txtTariffCd.IsHissuCheck = True
            .txtTariffCd.IsForbiddenWordsCheck = True
            .txtTariffCd.IsByteCheck = 10
            .txtTariffCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtTariffCd) = False Then
                Return False
            End If


            '割増タリフコード
            .txtWarimashiCd.ItemName = "割増タリフコード"
            .txtWarimashiCd.IsForbiddenWordsCheck = True
            .txtWarimashiCd.IsByteCheck = 10
            .txtWarimashiCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtWarimashiCd) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック(印刷種別)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSingleCheck() As Boolean

        With Me._Frm

            '**********単項目チェック(印刷種別)

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            '荷主(大)コード
            '2016.01.06 UMANO 英語化対応START
            '.txtCustCdL.ItemName = "荷主(大)コード"
            .txtCustCdL.ItemName = String.Concat(_Frm.lblTitleCustCd.Text(), LMFControlC.L_NM, LMFControlC.CD)
            '2016.01.06 UMANO 英語化対応END
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主(中)コード
            '2016.01.06 UMANO 英語化対応START
            '.txtCustCdM.ItemName = "荷主(中)コード"
            .txtCustCdM.ItemName = String.Concat(_Frm.lblTitleCustCd.Text(), LMFControlC.M_NM, LMFControlC.CD)
            '2016.01.06 UMANO 英語化対応END
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            .txtCustCdM.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            '距離程コード
            '2016.01.06 UMANO 英語化対応START
            '.txtKyoriteiCd.ItemName = "距離程コード"
            .txtKyoriteiCd.ItemName = _Frm.lblTitleKyoritei.Text()
            '2016.01.06 UMANO 英語化対応END
            .txtKyoriteiCd.IsHissuCheck = True
            .txtKyoriteiCd.IsForbiddenWordsCheck = True
            .txtKyoriteiCd.IsFullByteCheck = 3
            .txtKyoriteiCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtKyoriteiCd) = False Then
                Return False
            End If
            If 0 = Convert.ToDecimal(Me._Gcon.FormatNumValue(.numKyori.TextValue)) = True Then
                Me._Vcon.SetErrorControl(.numKyori)
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E182", New String() {"距離", "0"})
                MyBase.ShowMessage("E182", New String() {_Frm.lblTitleKyori.Text(), "0"})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

            'タリフコード
            .txtTariffCd.ItemName = "タリフコード"
            .txtTariffCd.IsHissuCheck = True
            .txtTariffCd.IsForbiddenWordsCheck = True
            .txtTariffCd.IsByteCheck = 10
            .txtTariffCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtTariffCd) = False Then
                Return False
            End If

            '割増タリフコード
            .txtWarimashiCd.ItemName = "割増タリフコード"
            .txtWarimashiCd.IsForbiddenWordsCheck = True
            .txtWarimashiCd.IsByteCheck = 10
            .txtWarimashiCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtWarimashiCd) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region

#Region "関連チェック"

    ''' <summary>
    ''' 距離取得時の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKyoriSaveCheck() As Boolean

        '荷主マスタの存在チェック
        Return Me.IsCustExistChk()

    End Function

    ''' <summary>
    ''' 運賃取得時の関連チェック
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinSaveCheck(ByVal nowDate As String) As Boolean

        'タリフ分類 , 車輌区分のチェック
        Dim rtnResult As Boolean = Me.IsTariffKbn()

        'タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsTariffExistChk(nowDate)

        '割増タリフのマスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsExtcExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' タリフ分類 , 車輌区分の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffKbn() As Boolean

        With Me._Frm

            'タリフ分類区分＝車扱いのとき、"車輌区分"必須チェック
            If .cmbUnso.SelectedValue.ToString().Equals(LMFControlC.TARIFF_KURUMA) _
            AndAlso String.IsNullOrEmpty(.cmbSyasyu.SelectedValue.ToString()) = True Then

                .cmbSyasyu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.cmbUnso)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E187", New String() {"車扱い", "車輌区分"})
                Return Me._Vcon.SetErrMessage("E858")
                '2016.01.06 UMANO 英語化対応END

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 印刷時の関連チェック
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSaveCheck(ByVal nowDate As String) As Boolean

        '荷主マスタの存在チェック
        Dim rtnResult As Boolean = Me.IsCustExistChk()

        'タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsTariffExistChk(nowDate)

        '割増タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsExtcExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk() As Boolean

        With Me._Frm

            '大コードに値がない場合、スルー
            Dim custCdL As String = .txtCustCdL.TextValue
            If String.IsNullOrEmpty(custCdL) = True Then
                Return True
            End If

            '中コードに値がない場合、スルー
            Dim custCdM As String = .txtCustCdM.TextValue
            If String.IsNullOrEmpty(custCdL) = True Then
                Return True
            End If

            '存在チェック(荷主マスタ)
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCustListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), custCdL, custCdM, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF, LMFControlC.CustMsgType.CUST_M) = False Then

                .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.txtCustCdL)

                Return False
            End If

            '名称の設定
            .lblCustNm.TextValue = Me._Gcon.EditConcatData(drs(0).Item("CUST_NM_L").ToString(), drs(0).Item("CUST_NM_M").ToString(), LMFControlC.ZENKAKU_SPACE)
            Return True

        End With

    End Function

    ''' <summary>
    ''' タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffExistChk(ByVal nowDate As String) As Boolean

        With Me._Frm

            'タリフコードに値がない場合、スルー
            Dim tariff As String = .txtTariffCd.TextValue
            If String.IsNullOrEmpty(tariff) = True Then
                Return True
            End If

            Dim kyori As String = .numKyori.TextValue
            Dim unsodate As String = .imdUnsoDate.TextValue
            Dim selectDate As String = unsodate
            Dim tariffbun As String = .cmbUnso.SelectedValue.ToString()

            '運送日が入力されていない場合
            If String.IsNullOrEmpty(unsodate) = True Then
                selectDate = nowDate
            End If

            'タリフ分類区分が "横持ち" の時
            Dim drs As DataRow() = Nothing
            Dim colNm As String = String.Empty
            Dim colCd As String = String.Empty
            If tariffbun.Equals(LMFControlC.TARIFF_YOKO) = True Then
                colNm = "YOKO_REM"
                colCd = "YOKO_TARIFF_CD"
                If Me._Vcon.SelectYokoTariffListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), tariff) = False Then
                    Call Me._Vcon.SetErrorControl(.txtTariffCd)
                    Return False
                End If

            Else

                'タリフ分類区分が40以外の時
                '存在チェック(タリフマスタ)
                colNm = "UNCHIN_TARIFF_REM"
                colCd = "UNCHIN_TARIFF_CD"
                If Me._Vcon.SelectUnchinTariffListDataRow(drs, tariff, , selectDate) = False Then

                    '運送日に値がある場合
                    If String.IsNullOrEmpty(unsodate) = False Then
                        Call Me._Vcon.SetErrorControl(.imdUnsoDate)
                    End If
                    Call Me._Vcon.SetErrorControl(.txtTariffCd)
                    'Return Me._Vcon.SetErrMessage("G001")　'2011.10.11 検証結果_その他(メモ)№47対応
                    Return False
                    'START YANAI 運賃試算（個数・県コード）緊急対応
                Else
                    'START YANAI 要望番号702
                    'If ("05").Equals(drs(0).Item("TABLE_TP").ToString) = True Then
                    '    'テーブルタイプが「個数・県コード」の場合はエラーとする
                    '    Me._Vcon.SetErrMessage("E437")
                    '    Return False
                    'End If
                    If ("02").Equals(drs(0).Item("TABLE_TP").ToString) = True OrElse _
                        ("05").Equals(drs(0).Item("TABLE_TP").ToString) = True OrElse _
                        ("06").Equals(drs(0).Item("TABLE_TP").ToString) = True Then
                        'テーブルタイプが個数・距離(02)、個数・県コード(05)、宅急便サイズ・県コード(06)の場合はエラーとする
                        Me._Vcon.SetErrMessage("E437")
                        Return False
                    End If
                    'END YANAI 要望番号702
                    'END YANAI 運賃試算（個数・県コード）緊急対応
                End If

            End If

            '名称の設定
            .lblTariffNm.TextValue = drs(0).Item(colNm).ToString()

            'コードの設定
            .txtTariffCd.TextValue = drs(0).Item(colCd).ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExtcExistChk() As Boolean

        With Me._Frm

            '割増コードに値がない場合、スルー
            Dim warimashiCd As String = .txtWarimashiCd.TextValue
            If String.IsNullOrEmpty(warimashiCd) = True Then
                Return True
            End If

            Dim todo As String = .txtTodokedeJisCd.TextValue
            If String.IsNullOrEmpty(todo) = True Then
                todo = "0000000"
            End If

            '存在チェック(割増タリフマスタ)
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectExtcUnchinListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), warimashiCd, todo) = False Then

                If "0000000".Equals(todo) = False Then
                    .txtTodokedeJisCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                End If
                Call Me._Vcon.SetErrorControl(.txtWarimashiCd)
                'Return Me._Vcon.SetErrMessage("G001") 　'2011.10.11 検証結果_その他(メモ)№47対応
                Return False
            End If

            '名称の設定
            .lblWarimashiNm.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

            'コードの再取得
            .txtWarimashiCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString()

            Return True

        End With

    End Function

#End Region

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMF060C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMF060C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMF060C.EventShubetsu.SISANSET          '試算結果退避
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

            Case LMF060C.EventShubetsu.DEL        '行削除
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

            Case LMF060C.EventShubetsu.KYORIGET        '距離取得
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

            Case LMF060C.EventShubetsu.UNCHINGET        '運賃取得
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

            Case LMF060C.EventShubetsu.PRINT        '印刷
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



            Case LMF060C.EventShubetsu.TOJIRU           '閉じる
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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF060C.EventShubetsu) As Boolean

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


                Case .txtCustCdL.Name, .txtCustCdM.Name


                    Dim custNm As String = .lblTitleCustCd.Text

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    lblCtl = New Control() {.lblCustNm}
                    msg = New String() {String.Concat(custNm, LMFControlC.L_NM, LMFControlC.CD) _
                                         , String.Concat(custNm, LMFControlC.M_NM, LMFControlC.CD)}

                Case .txtTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTariffCd}
                    lblCtl = New Control() {.lblTariffNm}
                    msg = New String() {String.Concat(.lblTitleTariff.Text)}

                Case .txtWarimashiCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtWarimashiCd}
                    lblCtl = New Control() {.lblWarimashiNm}
                    msg = New String() {String.Concat(.lblTitleWarimashi.Text)}

                Case .txtOrigJis.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtOrigJis}
                    lblCtl = New Control() {.lblOrigJisNm}
                    msg = New String() {String.Concat(.lblTitleOrigJis.Text)}


                Case .txtTodokedeJisCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTodokedeJisCd}
                    lblCtl = New Control() {.txtTodokedeJisNm}
                    msg = New String() {String.Concat(.lblTitleTodokedeJis.Text)}

                Case .txtKyoriteiCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtKyoriteiCd}
                    msg = New String() {String.Concat(.lblTitleKyoritei.Text)}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function
#End Region

#End Region 'Method

End Class
