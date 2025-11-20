' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM040V : 届先マスタメンテナンス
'  作  成  者       :  [金へスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMM040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMM040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMM040F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMMControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMMControlG


    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが初期化される前にアクセスしてされる問題の仮対応 20151109 INOUE
    '    Private _LangFlg As String
    '#Else
    '    Private _LangFlg As String = Jp.Co.Nrs.Win.Base.MessageManager.MessageLanguage
    '#End If
    '2017/09/25 修正 李↑


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMM040F, ByVal v As LMMControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

        'Gamen共通クラスの設定
        Me._Gcon = New LMMControlG(handlerClass, DirectCast(frm, Form))


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
    Friend Function IsSaveInputChk() As Boolean

        With Me._Frm

            'スペース除去
            Call Me._Vcon.TrimSpaceTextvalue(Me._Frm)
            Call Me._Vcon.TrimSpaceSprTextvalue(.sprDetail2 _
                                                    , .sprDetail2.ActiveSheet.Rows.Count - 1 _
                                                    , LMM040G.sprDetailDef2.REMARK.ColNo)

            '単項目チェック(編集部)
            Dim rtnResult As Boolean = Me.IsSaveSingleCheck()

            '単項目チェック(届先明細Spread)
            rtnResult = rtnResult AndAlso Me.IsDestDetailChk()

            'マスタ存在チェック
            rtnResult = rtnResult AndAlso Me.IsMstExistChk()

            '関連チェック
            rtnResult = rtnResult AndAlso Me.IsSaveCheck(Me._Frm)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 行追加/行削除 入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRowCheck(ByVal eventShubetsu As LMM040C.EventShubetsu, ByVal frm As LMM040F) As Boolean
        Dim arr As ArrayList = Nothing

        Select Case eventShubetsu
            Case LMM040C.EventShubetsu.INS_T    '行追加

                Return Me.IsSprNullChk("E219")

            Case LMM040C.EventShubetsu.DEL_T    '行削除

                With Me._Frm
                    '選択ﾁｪｯｸ
                    arr = Nothing
                    arr = Me.getCheckList(.sprDetail2)
                    If 0 = arr.Count Then
                        .sprDetail2.Focus()
                        MyBase.ShowMessage("E009")
                        Return False
                    End If

                End With

                Return True

        End Select


    End Function

    ''' <summary>
    ''' 明細の空行チェック
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprNullChk(ByVal id As String) As Boolean

        With Me._Frm.sprDetail2.ActiveSheet

            Dim sprMax As Integer = .Rows.Count - 1
            For i As Integer = 0 To sprMax

                '既にある行の用途区分・設定値・備考のすべての項目が空白の場合はエラー
                If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SUB_KB))) = True _
                    AndAlso String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SET_NAIYO))) = True _
                    AndAlso String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.REMARK))) = True _
                    Then

                    .Cells(i, LMM040C.SprColumnIndex2.SET_NAIYO).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    .Cells(i, LMM040C.SprColumnIndex2.REMARK).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(Me._Frm.sprDetail2, i, LMM040C.SprColumnIndex2.SUB_KB)

                    MyBase.ShowMessage(id)
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    '(2012.12.11)要望番号1677対応 --- START ---
    ''' <summary>
    ''' タリフセット入力チェック
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>要望番号1677対応</remarks>
    Private Function IsTariffSetChk(ByVal id As String, ByVal frm As LMM040F) As Boolean

        'タリフセット入力チェック
        '運賃タリフ(トンキロ建、車建、割増、横持)のいずれかが入力されていて、タリフ分類区分が未選択ならば、エラーとする。
        If (frm.txtUnchinTariffCd1.TextValue.Equals("") = False _
            Or frm.txtUnchinTariffCd2.TextValue.Equals("") = False _
            Or frm.txtExtcTariffCd.TextValue.Equals("") = False _
            Or frm.txtYokoTariffCd.TextValue.Equals("") = False _
            ) And frm.cmbTariffBunruiKbn.TextValue.Equals("") = True Then
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.ShowMessage("E810")
            '20151029 tsunehira add End
            'MyBase.ShowMessage(id, New String() {"運賃タリフ(トンキロ建、車建、割増、横持)のいずれかが空でない場合、タリフ分類区分"})
            Return False

        End If

        Return True

    End Function
    '(2012.12.11)要望番号1677対応 ---  END  ---

    '''' <summary>
    '''' 保存時のtrim
    '''' </summary>
    '''' <remarks></remarks>
    'Private Sub TrimSpaceTextValue()

    '    With Me._Frm

    '        .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
    '        .txtDestCd.TextValue = .txtDestCd.TextValue.Trim()
    '        .txtEDICd.TextValue = .txtEDICd.TextValue.Trim()
    '        .txtDestNm.TextValue = .txtDestNm.TextValue.Trim()
    '        .txtZip.TextValue = .txtZip.TextValue.Trim()
    '        .txtAd1.TextValue = .txtAd1.TextValue.Trim()
    '        .txtAd2.TextValue = .txtAd2.TextValue.Trim()
    '        .txtAd3.TextValue = .txtAd3.TextValue.Trim()
    '        .txtCustDestCd.TextValue = .txtCustDestCd.TextValue.Trim()
    '        .txtTel.TextValue = .txtTel.TextValue.Trim()
    '        .txtJIS.TextValue = .txtJIS.TextValue.Trim()
    '        .txtFax.TextValue = .txtFax.TextValue.Trim()
    '        .numKyori.TextValue = .numKyori.TextValue.Trim()
    '        .txtSpUnsoCd.TextValue = .txtSpUnsoCd.TextValue.Trim()
    '        .txtSpUnsoBrCd.TextValue = .txtSpUnsoBrCd.TextValue.Trim()
    '        .txtCargoTimeLimit.TextValue = .txtCargoTimeLimit.TextValue.Trim()
    '        .txtDeliAtt.TextValue = .txtDeliAtt.TextValue.Trim()
    '        .txtSalesCd.TextValue = .txtSalesCd.TextValue.Trim()
    '        .txtUriageCd.TextValue = .txtUriageCd.TextValue.Trim()
    '        .txtUnchinSeiqtoCd.TextValue = .txtUnchinSeiqtoCd.TextValue.Trim()
    '        .txtUnchinTariffCd1.TextValue = .txtUnchinTariffCd1.TextValue.Trim()
    '        .txtUnchinTariffCd2.TextValue = .txtUnchinTariffCd2.TextValue.Trim()
    '        .txtExtcTariffCd.TextValue = .txtExtcTariffCd.TextValue.Trim()
    '        .txtYokoTariffCd.TextValue = .txtYokoTariffCd.TextValue.Trim()

    '    End With

    'End Sub

    ''' <summary>
    ''' 単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim msg As String = String.Empty

            '**********編集部のチェック
            '営業所
            .cmbNrsBrCd.ItemName = .lblEigyosyo.Text
            .cmbNrsBrCd.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード（大）
            .txtCustCdL.ItemName = .lblCustL.Text
            .txtCustCdL.IsHissuCheck = chkFlg
            .txtCustCdL.IsForbiddenWordsCheck = chkFlg
            .txtCustCdL.IsFullByteCheck = 5
            .txtCustCdL.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtCustCdL) = errorFlg Then
                Return errorFlg
            End If

            '届先コード
            .txtDestCd.ItemName = .lblDestCd.Text
            .txtDestCd.IsHissuCheck = chkFlg
            .txtDestCd.IsForbiddenWordsCheck = chkFlg
            .txtDestCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtDestCd) = errorFlg Then
                Return errorFlg
            End If

            'EDI届先コード
            .txtEDICd.ItemName = .lblEDICd.Text
            .txtEDICd.IsForbiddenWordsCheck = chkFlg
            .txtEDICd.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtEDICd) = errorFlg Then
                Return errorFlg
            End If

            '届先名
            .txtDestNm.ItemName = .lblDestNm.Text
            .txtDestNm.IsHissuCheck = chkFlg
            .txtDestNm.IsForbiddenWordsCheck = chkFlg
            .txtDestNm.IsByteCheck = 80
            If MyBase.IsValidateCheck(.txtDestNm) = errorFlg Then
                Return errorFlg
            End If
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtDestNm.TextValue.Contains("<") OrElse .txtDestNm.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtDestNm)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '要望番号:1330 terakawa 2012.08.09 Start
            '届先カナ名
            .txtKanaNm.ItemName = .lblKanaNm.Text
            .txtKanaNm.IsForbiddenWordsCheck = chkFlg
            .txtKanaNm.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtKanaNm) = errorFlg Then
                Return errorFlg
            End If
            '要望番号:1330 terakawa 2012.08.09 End
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtKanaNm.TextValue.Contains("<") OrElse .txtKanaNm.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtKanaNm)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '郵便番号
            .txtZip.ItemName = .lblZip.Text
            .txtZip.IsForbiddenWordsCheck = chkFlg
            .txtZip.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtZip) = errorFlg Then
                Return errorFlg
            End If

            '住所1
            .txtAd1.ItemName = .lblAd1.Text
            .txtAd1.IsForbiddenWordsCheck = chkFlg
            .txtAd1.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd1) = errorFlg Then
                Return errorFlg
            End If
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtAd1.TextValue.Contains("<") OrElse .txtAd1.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtAd1)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '住所2
            .txtAd2.ItemName = .lblAd2.Text
            .txtAd2.IsForbiddenWordsCheck = chkFlg
            .txtAd2.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtAd2) = errorFlg Then
                Return errorFlg
            End If
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtAd2.TextValue.Contains("<") OrElse .txtAd2.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtAd2)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '住所3
            .txtAd3.ItemName = .lblAd3.Text
            .txtAd3.IsForbiddenWordsCheck = chkFlg
            '(2012.12.11)要望番号1585 40byte→60byte --- START ---
            '.txtAd3.IsByteCheck = 40
            '2019/11/26 要望管理009400 rep
            .txtAd3.IsByteCheck = 60
            .txtAd3.IsByteCheck = 80
            '(2012.12.11)要望番号1585 40byte→60byte --- START ---
            If MyBase.IsValidateCheck(.txtAd3) = errorFlg Then
                Return errorFlg
            End If
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtAd3.TextValue.Contains("<") OrElse .txtAd3.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtAd3)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '顧客運賃纏めコード
            .txtCustDestCd.ItemName = .lblDicDestCd.Text
            .txtCustDestCd.IsForbiddenWordsCheck = chkFlg
            .txtCustDestCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtCustDestCd) = errorFlg Then
                Return errorFlg
            End If

            '電話番号
            .txtTel.ItemName = .lblTel.Text
            .txtTel.IsForbiddenWordsCheck = chkFlg
            .txtTel.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtTel) = errorFlg Then
                Return errorFlg
            End If

            'JISコード
            .txtJIS.ItemName = .lblJIS.Text
            .txtJIS.IsForbiddenWordsCheck = chkFlg
            .txtJIS.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtJIS) = errorFlg Then
                Return errorFlg
            End If

            If String.IsNullOrEmpty(.txtJIS.TextValue) = True Then
                If MyBase.ShowMessage("W128", New String() {.lblJIS.Text}) <> MsgBoxResult.Ok Then
                    Me.SetErrorControl(.txtJIS)
                    Return errorFlg
                End If
            End If

            'ファックス番号
            .txtFax.ItemName = .lblFax.Text
            .txtFax.IsForbiddenWordsCheck = chkFlg
            .txtFax.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtFax) = errorFlg Then
                Return errorFlg
            End If

            '分析表添付区分
            .cmbCoaYn.ItemName = .lblCoaYn.Text
            .cmbCoaYn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbCoaYn) = errorFlg Then
                Return errorFlg
            End If

            '指定運送会社コード
            '2017/09/25 修正 李↓
            msg = lgm.Selector({"コード", "Code", "코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtSpUnsoCd.ItemName = String.Concat(.grpUnso.Text, msg)
            .txtSpUnsoCd.IsForbiddenWordsCheck = chkFlg
            .txtSpUnsoCd.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtSpUnsoCd) = errorFlg Then
                Return errorFlg
            End If

            '指定運送会社支店コード
            '2017/09/25 修正 李↓
            msg = lgm.Selector({"支店コード", "Branch Code", "지점코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtSpUnsoBrCd.ItemName = String.Concat(.grpUnso.Text, msg)
            .txtSpUnsoBrCd.IsForbiddenWordsCheck = chkFlg
            .txtSpUnsoBrCd.IsByteCheck = 3
            If MyBase.IsValidateCheck(.txtSpUnsoBrCd) = errorFlg Then
                Return errorFlg
            End If

            '荷卸時間制限
            .txtCargoTimeLimit.ItemName = .lblCargoTimeLimit.Text
            .txtCargoTimeLimit.IsForbiddenWordsCheck = chkFlg
            .txtCargoTimeLimit.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtCargoTimeLimit) = errorFlg Then
                Return errorFlg
            End If
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtCargoTimeLimit.TextValue.Contains("<") OrElse .txtCargoTimeLimit.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtCargoTimeLimit)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '大型車輛
            .cmbLargeCarYn.ItemName = .lblLargeCarYn.Text
            .cmbLargeCarYn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbLargeCarYn) = errorFlg Then
                Return errorFlg
            End If

            '配送時注意事項
            .txtDeliAtt.ItemName = .lblDeliAtt.Text
            .txtDeliAtt.IsForbiddenWordsCheck = chkFlg
            .txtDeliAtt.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtDeliAtt) = errorFlg Then
                Return errorFlg
            End If
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtDeliAtt.TextValue.Contains("<") OrElse .txtDeliAtt.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtDeliAtt)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            'START YANAI 要望番号881
            '備考
            .txtRemark.ItemName = .lblRemark.Text
            .txtRemark.IsForbiddenWordsCheck = chkFlg
            .txtRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtRemark) = errorFlg Then
                Return errorFlg
            End If
            'END YANAI 要望番号881
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtRemark.TextValue.Contains("<") OrElse .txtRemark.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtRemark)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '要望番号:1424② yamanaka 2012.09.20 Start
            '支払用住所
            .txtShiharaiAd.ItemName = .lblShiharaiAd.Text
            .txtShiharaiAd.IsForbiddenWordsCheck = chkFlg
            .txtShiharaiAd.IsByteCheck = 120
            If MyBase.IsValidateCheck(.txtShiharaiAd) = errorFlg Then
                Return errorFlg
            End If
            '要望番号:1424② yamanaka 2012.09.20 End
            'INS Start 2023/06/27 半角「<」「>」の入力は禁止する
            If .txtShiharaiAd.TextValue.Contains("<") OrElse .txtShiharaiAd.TextValue.Contains(">") Then
                MyBase.ShowMessage("E003", New String() {"「<」「>」"})
                Me.SetErrorControl(.txtShiharaiAd)
                Return errorFlg
            End If
            'INS End   2023/06/27 半角「<」「>」の入力は禁止する

            '納品書荷主名義コード
            .txtSalesCd.ItemName = .lblSales.Text
            .txtSalesCd.IsForbiddenWordsCheck = chkFlg
            .txtSalesCd.IsFullByteCheck = 5
            .txtSalesCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtSalesCd) = errorFlg Then
                Return errorFlg
            End If

            '売上先コード
            .txtUriageCd.ItemName = .lblUriage.Text
            .txtUriageCd.IsForbiddenWordsCheck = chkFlg
            .txtUriageCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtUriageCd) = errorFlg Then
                Return errorFlg
            End If

            '運賃請求先コード
            .txtUnchinSeiqtoCd.ItemName = .lblUnchinSeiqto.Text
            .txtUnchinSeiqtoCd.IsForbiddenWordsCheck = chkFlg
            .txtUnchinSeiqtoCd.IsByteCheck = 7
            If MyBase.IsValidateCheck(.txtUnchinSeiqtoCd) = errorFlg Then
                Return errorFlg
            End If

            '運賃タリフコード（屯キロ建）
            .txtUnchinTariffCd1.ItemName = .lblUnchinTariff1.Text
            .txtUnchinTariffCd1.IsForbiddenWordsCheck = chkFlg
            .txtUnchinTariffCd1.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTariffCd1) = errorFlg Then
                Return errorFlg
            End If

            '運賃タリフコード（車建）
            .txtUnchinTariffCd2.ItemName = .lblUnchinTariff2.Text
            .txtUnchinTariffCd2.IsForbiddenWordsCheck = chkFlg
            .txtUnchinTariffCd2.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtUnchinTariffCd2) = errorFlg Then
                Return errorFlg
            End If

            '割増運賃タリフコード
            .txtExtcTariffCd.ItemName = .lblExtcTariff.Text
            .txtExtcTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtExtcTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtExtcTariffCd) = errorFlg Then
                Return errorFlg
            End If

            '横持ち運賃タリフコード
            .txtYokoTariffCd.ItemName = .lblYokoTariff.Text
            .txtYokoTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtYokoTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtYokoTariffCd) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 届先明細Spreadの単項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsDestDetailChk() As Boolean

        '**********届先明細スプレッドのチェック
        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail2)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As LMSpread = Me._Frm.sprDetail2
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                '設定値
                .SetValidateCell(i, LMM040G.sprDetailDef2.SET_NAIYO.ColNo)
                .ItemName = LMM040G.sprDetailDef2.SET_NAIYO.ColName
                .IsForbiddenWordsCheck = chkFlg
                '2015.01.20 修正 UMANO バイト数変更(100⇒1000) 
                '.IsByteCheck = 100
                .IsByteCheck = 1000
                '2015.01.20 修正 UMANO
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '備考
                .SetValidateCell(i, LMM040G.sprDetailDef2.REMARK.ColNo)
                .ItemName = LMM040G.sprDetailDef2.REMARK.ColName
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                Dim kbnDr As DataRow() = Nothing
                Dim val As String = "1.000"
                Dim strSubKbn As String = _Vcon.GetCellValue(Me._Frm.sprDetail2.ActiveSheet.Cells(i, LMM040G.sprDetailDef2.SUB_KB.ColNo))

                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "Y006", "' AND KBN_CD = '", strSubKbn, "' AND VALUE1 = '", val, "'"))
                If kbnDr.Length = 0 Then

                    For j As Integer = 0 To max
                        If i <> j AndAlso strSubKbn.Equals(_Vcon.GetCellValue(Me._Frm.sprDetail2.ActiveSheet.Cells(j, LMM040G.sprDetailDef2.SUB_KB.ColNo))) Then
                            MyBase.ShowMessage("E008")
                            Return errorFlg
                        End If
                    Next
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        '荷主マスタ存在チェック（荷主(大)コード、納品書荷主名義コード）
        Dim rtnResult As Boolean = Me.IsCustExistChk()

        '届先マスタ重複チェック（EDI届先コード）
        rtnResult = rtnResult AndAlso Me.IsDestDoubleChk()

        '届先マスタ存在チェック（顧客運賃纏めコード、売上先コード）
        rtnResult = rtnResult AndAlso Me.IsDestExistChk()

        'JISマスタ存在チェック（JISコード）
        rtnResult = rtnResult AndAlso Me.IsJisExistChk()

        '運送会社マスタ存在チェック（運送会社コード）
        rtnResult = rtnResult AndAlso Me.IsUnsocoExistChk()

        '請求先マスタの存在チェック（運賃請求先コード）
        rtnResult = rtnResult AndAlso Me.IsSeiqtoExistChk()

        '運賃タリフマスタの存在チェック（運賃タリフコード（屯キロ建/車建））
        rtnResult = rtnResult AndAlso Me.IsUnchinTariffExistChk()

        '割増運賃タリフマスタの存在チェック（割増運賃タリフコード）
        rtnResult = rtnResult AndAlso Me.IsExtcTariffExistChk()

        '横持ちタリフマスタの存在チェック（横持ちタリフコード）
        rtnResult = rtnResult AndAlso Me.IsYokoTariffExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsCustExistChk() As Boolean

        With Me._Frm

            '①「荷主(大)コード、納品書荷主名義コード」をチェック
            Dim custCdL As String = String.Empty

            For i As Integer = 0 To 1
                If i = 0 Then
                    custCdL = .txtCustCdL.TextValue
                ElseIf i = 1 Then
                    custCdL = .txtSalesCd.TextValue
                End If

                If String.IsNullOrEmpty(custCdL) = True Then
                    '次項目のチェックへ
                Else
                    '荷主マスタ存在チェック
                    Dim drs As DataRow() = Nothing
                    If Me._Vcon.SelectCustListDataRow(drs _
                                                          , custCdL _
                                                          , LMMControlC.FLG_OFF _
                                                          , LMMControlC.FLG_OFF _
                                                          , LMMControlC.FLG_OFF _
                                                          , LMMControlC.CustMsgType.CUST_L _
                                                          ) = False Then

                        If i = 0 Then
                            .txtCustCdL.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtCustCdL)
                            .lblCustNmL.TextValue = String.Empty
                        ElseIf i = 1 Then
                            .txtSalesCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtSalesCd)
                            .lblSalesNm.TextValue = String.Empty
                        End If
                        Return False
                    End If

                    'マスタの値を設定
                    If i = 0 Then
                        .txtCustCdL.TextValue = drs(0).Item("CUST_CD_L").ToString()
                        .lblCustNmL.TextValue = drs(0).Item("CUST_NM_L").ToString()
                    ElseIf i = 1 Then
                        .txtSalesCd.TextValue = drs(0).Item("CUST_CD_L").ToString()
                        .lblSalesNm.TextValue = drs(0).Item("CUST_NM_L").ToString()
                    End If

                End If
            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 届先マスタ重複チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestDoubleChk() As Boolean

        With Me._Frm

            '②「EDI届先コード」チェック
            Dim custl As String = .txtCustCdL.TextValue
            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim destCd As String = .txtDestCd.TextValue
            Dim ediCd As String = .txtEDICd.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(ediCd) = True Then
                Return True
            End If

            '届先マスタ重複チェック
            Dim drs As DataRow() = Nothing
            drs = Me._Vcon.SelectEdiDestListDataRow(brcd, custl, destCd, ediCd)
            If drs.Length > 0 Then
                'MyBase.ShowMessage("E205", New String() {ediCd, "届先"})
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E811", New String() {ediCd})
                '20151029 tsunehira add End

                Call Me._Vcon.SetErrorControl(.txtEDICd)
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk() As Boolean

        With Me._Frm

            '③「顧客運賃纏めコード、売上先コード」をチェック
            Dim destCd As String = String.Empty
            Dim custl As String = .txtCustCdL.TextValue
            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString

            For i As Integer = 0 To 1
                If i = 0 Then
                    destCd = .txtCustDestCd.TextValue
                ElseIf i = 1 Then
                    destCd = .txtUriageCd.TextValue
                End If

                '1）値がない場合、スルー
                If String.IsNullOrEmpty(destCd) = True Then
                    '次項目のチェックへ
                    '2）ﾚｺｰﾄﾞｽﾃｰﾀｽ ＝ 新規or複写で、画面の届先コード＝画面の売上先コードの場合チェックは行わない。
                ElseIf i = 1 AndAlso _
                      (.lblSituation.RecordStatus = RecordStatus.COPY_REC OrElse _
                       .lblSituation.RecordStatus = RecordStatus.NEW_REC) AndAlso _
                       .txtDestCd.TextValue = .txtUriageCd.TextValue Then
                    '次項目のチェックへ
                Else
                    '届先マスタ存在チェック
                    Dim drs As DataRow() = Nothing
                    drs = Me._Vcon.SelectDestListDataRow(brcd, custl, destCd)
                    If drs.Length < 1 Then
                        '20151029 tsunehira add Start
                        '英語化対応
                        MyBase.ShowMessage("E827", New String() {destCd})
                        '20151029 tsunehira add End
                        'MyBase.ShowMessage("E079", New String() {"届先マスタ", destCd})
                        If i = 0 Then
                            .txtCustDestCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtCustDestCd)
                        ElseIf i = 1 Then
                            .txtUriageCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtUriageCd)
                            .lblUriageNm.TextValue = String.Empty
                        End If
                        Return False
                    End If

                    'マスタの値を設定
                    If i = 1 Then
                        .txtUriageCd.TextValue = drs(0).Item("DEST_CD").ToString()
                        .lblUriageNm.TextValue = drs(0).Item("DEST_NM").ToString()
                    End If

                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' JISマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsJisExistChk() As Boolean

        With Me._Frm

            '④「JISコード」チェック
            Dim jisCd As String = .txtJIS.TextValue
            Dim zipCd As String = .txtZip.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(jisCd) = True OrElse _
               String.IsNullOrEmpty(zipCd) = False Then
                Return True
            End If

            'JISマスタ存在チェック
            Dim drs As DataRow() = Nothing
            drs = Me._Vcon.SelectJisListDataRow(jisCd)
            If drs.Length < 1 Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E827", New String() {jisCd})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E079", New String() {"JISマスタ", jisCd})
                Call Me._Vcon.SetErrorControl(.txtJIS)
                Return False
            End If

            'マスタの値を設定
            .txtJIS.TextValue = drs(0).Item("JIS_CD").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk() As Boolean

        With Me._Frm

            '存在チェック前にセット入力チェック
            If Me.IsUnsocoSetChk(.txtSpUnsoCd, .txtSpUnsoBrCd) = False Then
                Return False
            End If

            '⑤「運送会社コード」チェック
            Dim custl As String = .txtCustCdL.TextValue
            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim spunsoCd As String = .txtSpUnsoCd.TextValue
            Dim spunsobrCd As String = .txtSpUnsoBrCd.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(spunsoCd) = True _
                AndAlso String.IsNullOrEmpty(spunsobrCd) = True Then
                Return True
            End If

            '運送会社マスタ存在チェック
            Dim drs As DataRow() = Nothing
            drs = Me._Vcon.SelectUnsocoListDataRow(brcd, spunsoCd, spunsobrCd)
            If drs.Length < 1 Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E828", New String() {String.Concat(spunsoCd, " - ", spunsobrCd)})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E079", New String() {"運送会社マスタ", String.Concat(spunsoCd, " - ", spunsobrCd)})
                Call Me._Vcon.SetErrorControl(.txtSpUnsoCd)
                .lblSpUnsoNm.TextValue = String.Empty
                Return False
            End If

            '複数ある場合、エラー
            If 1 < drs.Length Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E812", New String() {.grpUnso.Text.ToString, .grpUnso.Text.ToString})
                'MyBase.ShowMessage("E206", New String() {String.Concat(.grpUnso.Text, "コード"), String.Concat(.grpUnso.Text, "支店コード")})
                '20151029 tsunehira add End
                Call Me._Vcon.SetErrorControl(.txtSpUnsoCd)
                .lblSpUnsoNm.TextValue = String.Empty
                Return False
            End If

            'マスタの値を設定
            .txtSpUnsoCd.TextValue = drs(0).Item("UNSOCO_CD").ToString()
            .txtSpUnsoBrCd.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString()
            .lblSpUnsoNm.TextValue = Me._Gcon.EditConcatData(drs(0).Item("UNSOCO_NM").ToString(), drs(0).Item("UNSOCO_BR_NM").ToString(), Space(1))

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運送会社のセット入力チェック
    ''' </summary>
    ''' <param name="compCd">運送会社コード</param>
    ''' <param name="sitenCd">運送会社支店コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoSetChk(ByVal compCd As Win.InputMan.LMImTextBox _
                                          , ByVal sitenCd As Win.InputMan.LMImTextBox) As Boolean


        With Me._Frm

            Dim unsocoCd As String = compCd.TextValue
            Dim unsocoBrCd As String = sitenCd.TextValue

            '両方に値がない場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = True _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = True Then
                Return True
            End If

            '両方に値がある場合、スルー
            If String.IsNullOrEmpty(unsocoCd) = False _
                AndAlso String.IsNullOrEmpty(unsocoBrCd) = False Then
                Return True
            End If

            '片方に値がある場合、エラー
            Dim errorControl As Control() = New Control() {compCd, sitenCd}
            Call Me._Vcon.SetErrorControl(errorControl, compCd)
            '20151029 tsunehira add Start
            '英語化対応
            MyBase.ShowMessage("E812", New String() {.grpUnso.Text.ToString, .grpUnso.Text.ToString})
            'MyBase.ShowMessage("E206", New String() {String.Concat(.grpUnso.Text, "コード"), String.Concat(.grpUnso.Text, "支店コード")})
            '20151029 tsunehira add End
            'Return Me._Vcon.SetErrMessage("E017", New String() {String.Concat(.grpUnso.Text, "コード"), String.Concat(.grpUnso.Text, "支店コード")})

        End With

    End Function

    ''' <summary>
    ''' 請求先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSeiqtoExistChk() As Boolean

        With Me._Frm

            '⑥「運賃請求先コード」チェック
            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim UnchinSeiqtoCd As String = .txtUnchinSeiqtoCd.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(UnchinSeiqtoCd) = True Then
                Return True
            End If

            '運送会社マスタ存在チェック
            Dim drs As DataRow() = Nothing
            drs = Me._Vcon.SelectSeiqtoListDataRow(brcd, UnchinSeiqtoCd)
            If drs.Length < 1 Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E830", New String() {UnchinSeiqtoCd})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E079", New String() {"請求先マスタ", UnchinSeiqtoCd})
                Call Me._Vcon.SetErrorControl(.txtUnchinSeiqtoCd)
                .lblUnchinSeiqtoNm.TextValue = String.Empty
                Return False
            End If

            'マスタの値を設定
            .txtUnchinSeiqtoCd.TextValue = drs(0).Item("SEIQTO_CD").ToString()
            .lblUnchinSeiqtoNm.TextValue = Me._Gcon.EditConcatData(drs(0).Item("SEIQTO_NM").ToString(), drs(0).Item("SEIQTO_BUSYO_NM").ToString(), Space(1))

            Return True

        End With

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinTariffExistChk() As Boolean

        With Me._Frm

            '⑦「運賃タリフコード（屯キロ建/車建）」をチェック
            Dim unchintariffCd As String = String.Empty
            Dim tableType As String = String.Empty

            For i As Integer = 0 To 1
                If i = 0 Then
                    unchintariffCd = .txtUnchinTariffCd1.TextValue
                ElseIf i = 1 Then
                    unchintariffCd = .txtUnchinTariffCd2.TextValue
                    tableType = LMMControlC.FLG_ON
                End If

                '値がない場合、スルー
                If String.IsNullOrEmpty(unchintariffCd) = True Then
                    '次項目のチェックへ
                Else
                    '2019/07/22 依頼番号:006796 add start
                    'まずはテーブルタイプ条件なしで存在を確認する
                    Dim drsDummy As DataRow() = Nothing
                    drsDummy = Me._Vcon.SelectUnchinTariffListDataRow(unchintariffCd, , , "**")
                    If drsDummy.Length < 1 Then
                        MyBase.ShowMessage("E079", New String() {"運賃タリフマスタ", unchintariffCd})
                        If i = 0 Then
                            .txtUnchinTariffCd1.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtUnchinTariffCd1)
                            .lblUnchinTariffRem1.TextValue = String.Empty
                        ElseIf i = 1 Then
                            .txtUnchinTariffCd2.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtUnchinTariffCd2)
                            .lblUnchinTariffRem2.TextValue = String.Empty
                        End If
                        Return False
                    End If
                    '2019/07/22 依頼番号:006796 add end

                    '運賃タリフマスタの存在チェック
                    Dim drs As DataRow() = Nothing
                    drs = Me._Vcon.SelectUnchinTariffListDataRow(unchintariffCd, , , tableType)
                    If drs.Length < 1 Then
                        If i = 0 Then
                            '20151029 tsunehira add Start
                            '英語化対応
                            MyBase.ShowMessage("E813")
                            '20151029 tsunehira add End
                            'MyBase.ShowMessage("E187", New String() {"重量・距離建て", "車建て以外のタリフコード"})
                            .txtUnchinTariffCd1.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtUnchinTariffCd1)
                            .lblUnchinTariffRem1.TextValue = String.Empty
                        ElseIf i = 1 Then
                            '20151029 tsunehira add Start
                            '英語化対応
                            MyBase.ShowMessage("E814")
                            '20151029 tsunehira add End
                            'MyBase.ShowMessage("E187", New String() {"車建て", "車建のタリフコード"})
                            .txtUnchinTariffCd2.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Call Me._Vcon.SetErrorControl(.txtUnchinTariffCd2)
                            .lblUnchinTariffRem2.TextValue = String.Empty
                        End If
                        Return False
                    End If

                    'マスタの値を設定
                    If i = 0 Then
                        .txtUnchinTariffCd1.TextValue = drs(0).Item("UNCHIN_TARIFF_CD").ToString()
                        .lblUnchinTariffRem1.TextValue = drs(0).Item("UNCHIN_TARIFF_REM").ToString()
                    ElseIf i = 1 Then
                        .txtUnchinTariffCd2.TextValue = drs(0).Item("UNCHIN_TARIFF_CD").ToString()
                        .lblUnchinTariffRem2.TextValue = drs(0).Item("UNCHIN_TARIFF_REM").ToString()
                    End If

                End If

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' 割増運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExtcTariffExistChk() As Boolean

        With Me._Frm

            '⑧「割増運賃タリフコード」チェック
            Dim extcTariffCd As String = .txtExtcTariffCd.TextValue
            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString

            '値がない場合、スルー
            If String.IsNullOrEmpty(extcTariffCd) = True Then
                Return True
            End If

            '割増運賃タリフマスタの存在チェック
            Dim drs As DataRow() = Nothing
            drs = Me._Vcon.SelectExtcUnchinListDataRow(brcd, extcTariffCd)
            If drs.Length < 1 Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E831", New String() {extcTariffCd})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E079", New String() {"割増運賃タリフマスタ", extcTariffCd})
                Call Me._Vcon.SetErrorControl(.txtExtcTariffCd)
                .lblExtcTariffRem.TextValue = String.Empty
                Return False
            End If

            'マスタの値を設定
            .txtExtcTariffCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString()
            .lblExtcTariffRem.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoTariffExistChk() As Boolean

        With Me._Frm

            '⑨「横持ちタリフコード」チェック
            Dim yokoTariffCd As String = .txtYokoTariffCd.TextValue
            Dim brcd As String = .cmbNrsBrCd.SelectedValue.ToString

            '値がない場合、スルー
            If String.IsNullOrEmpty(yokoTariffCd) = True Then
                Return True
            End If

            '横持ちタリフマスタの存在チェック
            Dim drs As DataRow() = Nothing
            drs = Me._Vcon.SelectYokoTariffListDataRow(brcd, yokoTariffCd)
            If drs.Length < 1 Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E832", New String() {yokoTariffCd})
                '20151029 tsunehira add End
                'MyBase.ShowMessage("E079", New String() {"横持ちタリフヘッダ", yokoTariffCd})
                Call Me._Vcon.SetErrorControl(.txtYokoTariffCd)
                .lblYokoTariffRem.TextValue = String.Empty
                Return False
            End If

            'マスタの値を設定
            .txtYokoTariffCd.TextValue = drs(0).Item("YOKO_TARIFF_CD").ToString()
            .lblYokoTariffRem.TextValue = drs(0).Item("YOKO_REM").ToString()

            Return True

        End With

    End Function

    ''' <summary>
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveCheck(ByVal frm As LMM040F) As Boolean

        '(2012.12.11)要望番号1677対応 --- START ---
        Dim rtnReutl As Boolean = True

        'タリフセット入力チェック
        rtnReutl = Me.IsTariffSetChk("E001", frm)
        If rtnReutl = False Then
            frm.cmbTariffBunruiKbn.Focus()
            Return rtnReutl
        End If

        '明細の空行チェック
        rtnReutl = Me.IsSprNullChk("E392")

        '(2012.12.11)要望番号1677対応 --- END ---

        '届先閲覧制限設定チェック
        rtnReutl = Me.IsAgentReadingAuthorityChk("E001")

        If rtnReutl = False Then
            frm.sprDetail2.Focus()
        End If

        Return rtnReutl

    End Function

    ''' <summary>
    ''' レコードステータスチェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRecordStatusChk(ByVal frm As LMM040F) As Boolean

        If frm.lblSituation.RecordStatus = RecordStatus.DELETE_REC Then
            MyBase.ShowMessage("E035")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMM040F, ByVal eventShubetsu As LMM040C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbNrsBrCd.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMM040C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMM040C.EventShubetsu.HUKUSHA
        '            msg = "複写"

        '        Case LMM040C.EventShubetsu.SAKUJO
        '            msg = "削除・復活"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

    ''' <summary>
    ''' スプレッドでチェックの付いたRowIndexを取得
    ''' </summary>
    ''' <returns>リスト</returns>
    ''' <remarks>チェックのある行のRowIndexをリストに入れて戻す</remarks>
    Friend Function SprSelectCount() As ArrayList

        Dim defNo As Integer = LMM040G.sprDetailDef.DEF.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            Dim list As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If _Vcon.GetCellValue(.Cells(i, defNo)).Equals(LMConst.FLG.ON) = True Then
                    '選択されたRowIndexを設定
                    list.Add(i)
                End If
            Next

            Return list

        End With

    End Function

    ''' <summary>
    ''' 検索押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        'Trimチェック
        '検索
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0, Me._Frm.sprDetail.ActiveSheet.Columns.Count - 1)

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 検索押下時スプレッド単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '営業所コード
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.NRS_BR_NM.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.NRS_BR_NM.ColName
            'vCell.IsHissuCheck = chkFlg '2011.08.24 残作業一覧一覧№34対応
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '荷主コード
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.CUST_CD_L.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.CUST_CD_L.ColName
            vCell.IsByteCheck = 5
            vCell.IsForbiddenWordsCheck = chkFlg
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '荷主名
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.CUST_NM_L.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.CUST_NM_L.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '届先コード
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.DEST_CD.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.DEST_CD.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 15
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '届先名
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.DEST_NM.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.DEST_NM.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '住所
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.AD.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.AD.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

            '電話番号
            vCell.SetValidateCell(0, LMM040G.sprDetailDef.TEL.ColNo)
            vCell.ItemName = LMM040G.sprDetailDef.TEL.ColName
            vCell.IsForbiddenWordsCheck = chkFlg
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function


    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMM040C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMM040C.EventShubetsu.SHINKI           '新規
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.HENSHU          '編集
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.HUKUSHA          '複写
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.SAKUJO          '削除・復活
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

                'START ADD 2013/09/10 KOBAYASHI WIT対応
            Case LMM040C.EventShubetsu.INSATSU      '印刷
                '50:外部の場合エラー
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
                'END   ADD 2013/09/10 KOBAYASHI WIT対応

            Case LMM040C.EventShubetsu.KENSAKU         '検索
                '50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.MASTEROPEN          'マスタ参照
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.HOZON           '保存
                '10:閲覧者、20:入力者（一般）、50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMM040C.EventShubetsu.DCLICK         'ダブルクリック
                '50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMM040C.EventShubetsu.ENTER          'Enter
                '50:外部の場合エラー
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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

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


    ''' <summary>
    ''' 印刷時チェック
    ''' </summary>
    ''' <param name="list">一覧データ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintChk(ByVal list As ArrayList) As Boolean

        ' 対象件数
        Dim max As Integer = list.Count
        Dim row As Integer = 0

        ' 判定項目位置取得
        Dim defNo As Integer = LMM040G.sprDetailDef.DEF.ColNo
        Dim delKbn As Integer = LMM040G.sprDetailDef.SYS_DEL_FLG.ColNo

        With Me._Frm.sprDetail.ActiveSheet

            ' 選択チェック
            Dim selectFlg As Boolean = False
            Dim deleteFlg As Boolean = False

            For idx As Integer = 1 To max
                Dim rowIndex As Integer = CInt(list(idx - 1))
                If _Vcon.GetCellValue(.Cells(rowIndex, defNo)).Equals(LMConst.FLG.ON) = True Then
                    If _Vcon.GetCellValue(.Cells(rowIndex, delKbn)).Equals(LMConst.FLG.ON) = True Then
                        ' 削除データの選択
                        deleteFlg = True
                    Else
                        ' 通常データの選択
                        selectFlg = True
                    End If
                    Exit For
                End If
            Next

            ' 一覧未選択の場合はエラー
            If deleteFlg = True Then
                MyBase.ShowMessage("E257")
                Return False
            End If

            ' 一覧未選択の場合はエラー
            If selectFlg = False Then
                MyBase.ShowMessage("E009")
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As String) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            '検証結果(メモ)№120対応(2011.09.14)
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMM040C.EventShubetsu.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim clearCtl As Nrs.Win.GUI.Win.Interface.IEditableControl() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name

                    Dim custNm As String = .lblCustL.Text
                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCdL}
                    msg = New String() {String.Concat(custNm, LMMControlC.L_NM, LMMControlC.CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblCustNmL}

                Case .txtZip.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtZip}
                    msg = New String() {.lblZip.Text}

                Case .txtCustDestCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCustDestCd}
                    msg = New String() {.lblDicDestCd.Text}

                Case .txtJIS.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtJIS}
                    msg = New String() {.lblJIS.Text}

                Case .txtSpUnsoCd.Name, .txtSpUnsoBrCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtSpUnsoCd, .txtSpUnsoBrCd}
                    msg = New String() {String.Concat(.grpUnso.Text, LMMControlC.CD) _
                                        , String.Concat(.grpUnso.Text, LMMControlC.BR_CD)}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSpUnsoNm}

                Case .txtSalesCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtSalesCd}
                    msg = New String() {.lblSales.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblSalesNm}

                Case .txtUriageCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUriageCd}
                    msg = New String() {.lblUriage.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUriageNm}

                Case .txtUnchinSeiqtoCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinSeiqtoCd}
                    msg = New String() {.lblUnchinSeiqto.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinSeiqtoNm}

                Case .txtUnchinTariffCd1.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTariffCd1}
                    msg = New String() {.lblUnchinTariff1.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTariffRem1}

                Case .txtUnchinTariffCd2.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUnchinTariffCd2}
                    msg = New String() {.lblUnchinTariff2.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblUnchinTariffRem2}

                Case .txtExtcTariffCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtExtcTariffCd}
                    msg = New String() {.lblExtcTariff.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblExtcTariffRem}

                Case .txtYokoTariffCd.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtYokoTariffCd}
                    msg = New String() {.lblYokoTariff.Text}
                    clearCtl = New Nrs.Win.GUI.Win.Interface.IEditableControl() {.lblYokoTariffRem}

            End Select

            Dim focusCtl As Control = Me._Frm.ActiveControl
            Return Me._Vcon.IsFocusChk(actionType, ctl, msg, focusCtl, clearCtl)

        End With

    End Function

    '2017/09/25 修正 李↓ -- Jp.Co.Nrs.LM.Utility参照追加による修正
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList(ByVal sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread) As ArrayList
        '2017/09/25 修正 李↑

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprDetail2").Equals(sprDetail.Name) = True Then
            defNo = LMM040C.SprColumnIndex2.DEF
        End If

        '選択された行の行番号を取得
        Return _Vcon.SprSelectList(defNo, sprDetail)

    End Function

#Region "部品化検討中"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    '''' <summary>
    '''' セルから値を取得
    '''' </summary>
    '''' <param name="aCell">セル</param>
    '''' <returns>取得した値</returns>
    '''' <remarks></remarks>
    'Friend Function GetCellValue(ByVal aCell As Cell) As String

    '    GetCellValue = String.Empty

    '    If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

    '        'コンボボックスの場合、Value値を返却
    '        If aCell.Value Is Nothing = False Then
    '            GetCellValue = aCell.Value.ToString()
    '        End If

    '    ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

    '        'チェックボックスの場合、Booleanの値をStringに変換
    '        If aCell.Text.Equals("True") = True Then
    '            GetCellValue = LMConst.FLG.ON
    '        ElseIf aCell.Text.Equals("False") = True Then
    '            GetCellValue = LMConst.FLG.OFF
    '        Else
    '            GetCellValue = aCell.Text
    '        End If

    '    ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

    '        'ナンバーの場合、Value値を返却
    '        If aCell.Value Is Nothing = False Then
    '            GetCellValue = aCell.Value.ToString()
    '        Else
    '            GetCellValue = 0.ToString()
    '        End If

    '    Else

    '        'テキストの場合、Trimした値を返却
    '        GetCellValue = aCell.Text.Trim()

    '    End If

    '    Return GetCellValue

    'End Function

#End Region


    ''' <summary>
    ''' 代理店の届先閲覧制限設定チェック
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsAgentReadingAuthorityChk(ByVal id As String) As Boolean

        With Me._Frm.sprDetail2.ActiveSheet


            Dim kbnDr As DataRow() = Nothing

            kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", "D032", "' AND KBN_CD = '", "01", "' AND KBN_NM1 = '", "1", "'"))
            If kbnDr.Length = 0 Then
                Return True
            End If

            Dim sprMax As Integer = .Rows.Count - 1
            For i As Integer = 0 To sprMax
                If ("15".Equals(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SUB_KB))) _
                    OrElse "16".Equals(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SUB_KB))) _
                    OrElse "17".Equals(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SUB_KB))) _
                    OrElse "18".Equals(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SUB_KB)))) _
                    AndAlso String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(i, LMM040C.SprColumnIndex2.SET_NAIYO))) Then

                    .Cells(i, LMM040C.SprColumnIndex2.SET_NAIYO).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(Me._Frm.sprDetail2, i, LMM040C.SprColumnIndex2.SUB_KB)

                    MyBase.ShowMessage(id, New String() {.Columns(LMM040C.SprColumnIndex2.SET_NAIYO).Label})
                    Return False

                End If

            Next

        End With

        Return True

    End Function


#End Region 'Method

End Class
