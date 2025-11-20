' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運賃サブシステム
'  プログラムID     :  LMF070V : 運賃試算比較
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF070Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF070V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF070F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF070F, ByVal v As LMFControlV, ByVal g As LMFControlG)

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

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal data As String) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsSaveSingleCheck() = False Then
            Return False
        End If

        '単項目チェック(Spread部)
        If Me.IsSpreadInputChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSaveCheck(data) = False Then
            Return False
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

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprUnchin)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtOldTariffCd.TextValue = .txtOldTariffCd.TextValue.Trim()
            .txtOldETariffCd.TextValue = .txtOldETariffCd.TextValue.Trim()
            .txtUnsoCd.TextValue = .txtUnsoCd.TextValue.Trim()
            .txtUnsoBrCd.TextValue = .txtUnsoBrCd.TextValue.Trim()
            .txtSeiqtoCd.TextValue = .txtSeiqtoCd.TextValue.Trim()
            .txtNewTariffCd.TextValue = .txtNewTariffCd.TextValue.Trim()
            .txtNewETariffCd.TextValue = .txtNewETariffCd.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm
            '**********編集部のチェック

            '出荷日From
            .imdOutakaDate_From.ItemName = "出荷日From"
            .imdOutakaDate_From.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdOutakaDate_From) = False Then
                Return False
            End If

            '出荷日To
            .imdOutakaDate_To.ItemName = "出荷日To"
            .imdOutakaDate_To.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdOutakaDate_To) = False Then
                Return False
            End If

            '荷主コード(大)
            .txtCustCdL.ItemName = "荷主コード(大)"
            .txtCustCdL.IsHissuCheck = True
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsFullByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = "荷主コード(中)"
            .txtCustCdM.IsHissuCheck = True
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsFullByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            '運賃タリフコード
            .txtOldTariffCd.ItemName = "運賃タリフコード"
            .txtOldTariffCd.IsForbiddenWordsCheck = True
            .txtOldTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtOldTariffCd) = False Then
                Return False
            End If

            '割増運賃タリフコード
            .txtOldETariffCd.ItemName = "割増運賃タリフコード"
            .txtOldETariffCd.IsForbiddenWordsCheck = True
            .txtOldETariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtOldETariffCd) = False Then
                Return False
            End If

            '運送会社コード
            .txtUnsoCd.ItemName = "運送会社コード"
            .txtUnsoCd.IsForbiddenWordsCheck = True
            .txtUnsoCd.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtUnsoCd) = False Then
                Return False
            End If

            '運送会社支社コード
            .txtUnsoBrCd.ItemName = "運送会社支社コード"
            .txtUnsoBrCd.IsForbiddenWordsCheck = True
            .txtUnsoBrCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtUnsoBrCd) = False Then
                Return False
            End If

            '新運賃タリフコード
            .txtNewTariffCd.ItemName = "新運賃タリフコード"
            .txtNewTariffCd.IsForbiddenWordsCheck = True
            .txtNewTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtNewTariffCd) = False Then
                Return False
            End If

            '新割増運賃タリフコード
            .txtNewETariffCd.ItemName = "新割増運賃タリフコード"
            .txtNewETariffCd.IsForbiddenWordsCheck = True
            .txtNewETariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtNewETariffCd) = False Then
                Return False
            End If

            '請求先コード
            .txtSeiqtoCd.ItemName = "請求先コード"
            .txtSeiqtoCd.IsForbiddenWordsCheck = True
            .txtSeiqtoCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtSeiqtoCd) = False Then
                Return False
            End If

            '距離程コード
            .txtKyoriteiCd.ItemName = "距離程コード"
            .txtKyoriteiCd.IsForbiddenWordsCheck = True
            .txtKyoriteiCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtKyoriteiCd) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッドの項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSpreadInputChk() As Boolean

        With Me._Frm
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprUnchin)

            '届先名
            vCell.SetValidateCell(0, LMF070G.sprUnchinDef.DEST_NM.ColNo)
            vCell.ItemName = "届先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先住所
            vCell.SetValidateCell(0, LMF070G.sprUnchinDef.DEST_JIS_NM.ColNo)
            vCell.ItemName = "届先住所"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社
            vCell.SetValidateCell(0, LMF070G.sprUnchinDef.UNSOCO_CD.ColNo)
            vCell.ItemName = "運送会社"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal nowDate As String) As Boolean

        '日付の大小チェック
        Dim rtnResult As Boolean = Me.IsDateChk()

        '新タリフ , 割増タリフの関連チェック
        rtnResult = rtnResult AndAlso Me.IsNewTariffChk()

        'タリフマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsTariffExistChk(nowDate)

        '割増タリフの存在チェック
        rtnResult = rtnResult AndAlso Me.IsExtcExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 日付の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDateChk() As Boolean

        With Me._Frm

            '出荷日To<出荷日From時、エラー
            If Me.IsLargeSmallChk(.imdOutakaDate_To.TextValue, .imdOutakaDate_From.TextValue, False) = False Then
                .imdOutakaDate_From.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.imdOutakaDate_To)
                Return Me._Vcon.SetErrMessage("E039", New String() {"出荷日To", "出荷日From"})
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 新タリフ、割増タリフの関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsNewTariffChk() As Boolean

        With Me._Frm

            '新割増タリフコードに値がない場合、スルー
            If String.IsNullOrEmpty(.txtNewETariffCd.TextValue) = True Then
                Return True
            End If

            '運賃タリフが未入力のときエラー
            If String.IsNullOrEmpty(.txtNewTariffCd.TextValue) = True Then

                .txtNewETariffCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.txtNewTariffCd)

                Return Me._Vcon.SetErrMessage("E339", New String() {"新割増タリフ", "新運賃タリフ"})

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' タリフマスタ存在チェック
    ''' </summary>
    ''' <param name="nowDate">システム日付</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffExistChk(ByVal nowDate As String) As Boolean

        With Me._Frm

            '新タリフコードに値がない場合、スルー
            Dim tariff As String = .txtNewTariffCd.TextValue
            If String.IsNullOrEmpty(tariff) = True Then
                Return True
            End If

            '運賃タリフの存在チェック
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectUnchinTariffListDataRow(drs, tariff, , nowDate) = False Then

                Call Me._Vcon.SetErrorControl(.txtNewTariffCd)
                Return False

            End If

            '名称の設定
            .lblNewTariffNm.TextValue = drs(0).Item("UNCHIN_TARIFF_REM").ToString()

        End With

        Return True

    End Function

    ''' <summary>
    ''' 割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExtcExistChk() As Boolean

        With Me._Frm

            '営業所に値がない場合、スルー
            Dim nrsBrCd As String = .cmbNrsBrCd.SelectedValue.ToString
            If String.IsNullOrEmpty(nrsBrCd) = True Then
                Return True
            End If

            '新割増タリフコードに値がない場合、スルー
            Dim warimashiCd As String = .txtNewETariffCd.TextValue
            If String.IsNullOrEmpty(warimashiCd) = True Then
                Return True
            End If

            '存在チェック(割増タリフマスタ)
            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectExtcUnchinListDataRow(drs, nrsBrCd, warimashiCd, "0000000") = False Then

                Call Me._Vcon.SetErrorControl(.txtNewETariffCd)
                Return False

            End If

            '名称の設定
            .lblNewETariffNm.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

        End With

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMF070F) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

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
        Return Me.IsLargeSmallChk(Convert.ToDecimal(Me._Gcon.FormatNumValue(large)), Convert.ToDecimal(Me._Gcon.FormatNumValue(small)), equalFlg)

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

    ''' <summary>
    ''' オーバーフローチェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="minData">最小値</param>
    ''' <param name="maxData">最大値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCalcOver(ByVal value As String, ByVal minData As String, ByVal maxData As String, ByVal msg As String) As Boolean

        '上限チェック
        If Me._Vcon.IsCalcOver(value, minData, maxData) = False Then
            Return Me.SetCalcOverErrMessage(maxData, msg)
        End If

        Return True

    End Function

    ''' <summary>
    ''' オーバーフローのエラーメッセージ
    ''' </summary>
    ''' <param name="maxData">最大値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetCalcOverErrMessage(ByVal maxData As String, ByVal msg As String) As Boolean

        Return Me._Vcon.SetErrMessage("E037")

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF070C.EventShubetsu) As Boolean

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

                Case .txtUnsoCd.Name, .txtUnsoBrCd.Name

                    Dim unsoNm0 As String = .lblUnsoNm.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsoCd, .txtUnsoBrCd}
                    lblCtl = New Control() {.lblUnsoNm}
                    msg = New String() {String.Concat(unsoNm0, LMFControlC.CD), String.Concat(unsoNm0, LMFControlC.BR_CD)}


                Case .txtCustCdL.Name, .txtCustCdM.Name

                    Dim custNm As String = .lblCustNm.Text
                    lblCtl = New Control() {.lblCustNm}
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtCustCdL, .txtCustCdM}
                    msg = New String() {String.Concat(custNm, LMFControlC.L_NM, LMFControlC.CD) _
                                        , String.Concat(custNm, LMFControlC.M_NM, LMFControlC.CD)}

                Case .txtOldTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtOldTariffCd}
                    lblCtl = New Control() {.lblOldTariffNm}
                    msg = New String() {.lblOldTariffNm.Text}

                Case .txtOldETariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtOldETariffCd}
                    lblCtl = New Control() {.lblOldETariffNm}
                    msg = New String() {.lblOldETariffNm.Text}

                Case .txtSeiqtoCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtSeiqtoCd}
                    lblCtl = New Control() {.lblSeiqtoNm}
                    msg = New String() {.lblSeiqtoNm.Text}

                Case .txtNewTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtNewTariffCd}
                    lblCtl = New Control() {.lblNewTariffNm}
                    msg = New String() {.lblNewTariffNm.Text}

                Case .txtNewETariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtNewETariffCd}
                    lblCtl = New Control() {.lblNewETariffNm}
                    msg = New String() {.lblNewETariffNm.Text}

                Case .txtKyoriteiCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtKyoriteiCd}
                    msg = New String() {String.Concat(.lblTitleKyoritei.Text)}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal eventShubetsu As LMF070C.EventShubetsu) As Boolean


        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMF070C.EventShubetsu.KENSAKU           '`
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

            Case LMF070C.EventShubetsu.MASTEROPEN          '編集
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



            Case LMF070C.EventShubetsu.CLOSE           '閉じる
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

End Class
