' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH040C : EDI出荷データ編集
'  作  成  者       :  Kim
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMH040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH040F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMHControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMHControlG

    ''' <summary>
    ''' LMH040Gクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH040G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH040F, ByVal v As LMHControlV, ByVal g As LMH040G, ByVal ctlG As LMHControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = ctlG

        Me._G = g

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "コントロールメソッド"

    ''' <summary>
    ''' 入力チェック（保存）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsEditInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsLHeaderChk()
        rtnResult = rtnResult AndAlso Me.IsSprChk(Me._Frm.sprFreeInputsL)

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputLConnectionChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 格納データチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>編集データをEDI出荷（中）に格納する時の入力チェック</remarks>
    Friend Function IsMDatasetInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '数字コントロールの値検討
        Call Me.CheckNumberValue(Me._Frm.numOutkaHasu)
        Call Me.CheckNumberValue(Me._Frm.numOutkaPkgNB)
        Call Me.CheckNumberValue(Me._Frm.numOutkaTtlNB)
        Call Me.CheckNumberValue(Me._Frm.numOutkaTtlQT)

        '単項目チェック
        '千葉アクタス対応（NRS商品CDが取込時空になる）
        '商品マスタの存在チェックを行わない
        Dim whereStr As String = String.Empty
        whereStr = "NRS_BR_CD = '"
        whereStr = whereStr & _Frm.cmbEigyo.SelectedValue.ToString & "'"
        whereStr = whereStr & " AND CUST_CD = '" & String.Concat(_Frm.txtCustCDL.TextValue.ToString) & "'"
        whereStr = whereStr & " AND SUB_KB = '80' "

        Dim custdtlDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(whereStr)
        Dim rtnResult As Boolean
        If custdtlDr.Length > 0 Then
            rtnResult = Me.IsMHeaderChk(False)
            else
            rtnResult = Me.IsMHeaderChk(True)
        End If

        rtnResult = rtnResult AndAlso Me.IsSprChk(Me._Frm.sprFreeInputsM)

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputMConnectionChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 入力チェック（新規）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsShinkiInputCheck() As Boolean 

        With Me._Frm
            '届先コード入力欄、スペース除去
            .txtDestCD.TextValue = .txtDestCD.TextValue.Trim()
        End With

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsShinkiChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 新規ボタン押下時の入力チェック(届先)
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsShinkiChk() As Boolean

        With Me._Frm

            '届先コード
            .txtDestCD.ItemName = "届先コード"
            .txtDestCD.IsForbiddenWordsCheck = True
            .txtDestCD.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtDestCD) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入力チェック（行削除）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsRowDelInputCheck(ByVal ds As DataSet, ByVal arr As ArrayList, ByVal arrNot As ArrayList) As Boolean

        Dim cnt As Integer = Me.FindSelectedRow()

        '未選択チェック
        If cnt = -1 Then
            MyBase.ShowMessage("E009")
            Return False
            '    '全選択チェック
            'ElseIf cnt = -2 Then
            '    MyBase.ShowMessage("E280")
            '    Return False
        End If

        '混在チェック（正常状態以外のデータが存在する場合、エラー）
        If Me.IsDelKbChk(LMH040C.DEL_KB_NG_NM) = False Then
            Return False
        End If

        '正常データ件数チェック
        If Me.IsSeijoDataCountChk(ds, arr, arrNot) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入力チェック（行復活）
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsRowReInputCheck() As Boolean

        '未選択チェック
        If Me.FindSelectedRow() = -1 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        '混在チェック（削除状態以外のデータが存在する場合、エラー）
        If Me.IsDelKbChk(LMH040C.DEL_KB_OK_NM) = False Then
            Return False
        End If

        Return True

    End Function

#End Region 'コントロールメソッド

#Region "マスタ存在チェック"

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectUnsocoListDataRow(drs, .txtUnsoCD.TextValue, .txtUnsoBrCD.TextValue) = False Then
                Call Me._Vcon.SetErrorControl(.txtUnsoCD)
                Call Me._Vcon.SetErrorControl(.txtUnsoBrCD)
                .lblUnsoNM.TextValue = String.Empty
                Return False
            End If

            Dim unsoNM As String = drs(0).Item("UNSOCO_NM").ToString
            Dim unsoBrNM As String = drs(0).Item("UNSOCO_BR_NM").ToString

            '名称を設定
            .lblUnsoNM.TextValue = Me._Gcon.EditConcatData(unsoNM, unsoBrNM, Space(1))

            Return True

        End With

    End Function

    ''' <summary>
    ''' 届先マスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk() As Boolean

        With Me._Frm

            Dim dr As DataRow() = Nothing
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCDL.TextValue

            '届先のチェック
            If Me._Vcon.SelectDestListDataRow(dr, brCd, custCdL, .txtDestCD.TextValue) = False Then
                Call Me._Vcon.SetErrorControl(.txtDestCD)
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 売上先の存在チェック（届先マスタ）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUriagesakiExistChk() As Boolean

        With Me._Frm

            Dim dr As DataRow() = Nothing
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCDL.TextValue

            '売上先のチェック
            If Me._Vcon.SelectDestListDataRow(dr, brCd, custCdL, .txtShipCDL.TextValue) = False Then
                Call Me._Vcon.SetErrorControl(.txtShipCDL)
                .lblShipNM.TextValue = String.Empty
                Return False
            End If

            '名称を設定
            .lblShipNM.TextValue = dr(0).Item("DEST_NM").ToString()

        End With

        Return True

    End Function

    ''' <summary>
    ''' タリフの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffExistChk() As Boolean

        With Me._Frm

            '値が無い場合、スルー
            Dim tariffCd As String = .txtUntinTariffCD.TextValue
            If String.IsNullOrEmpty(tariffCd) = True Then
                'Call Me._Vcon.SetErrorControl(.txtUntinTariffCD)
                Return True
            End If

            Dim tariffKbn As String = .cmbUnsoTehaiKB.SelectedValue.ToString()
            Dim dr As DataRow() = Nothing

            'タリフ分類区分による分岐
            Select Case tariffKbn

                Case LMHControlC.TARIFF_YOKO    '横持ちタリフの場合

                    If Me._Vcon.SelectYokoTariffListDataRow(dr, .cmbEigyo.SelectedValue.ToString, tariffCd) = False Then
                        Call Me._Vcon.SetErrorControl(.txtUntinTariffCD)
                        .lblUntinTariffREM.TextValue = String.Empty
                        Return False
                    End If

                    '名称を設定
                    .lblUntinTariffREM.TextValue = dr(0).Item("YOKO_REM").ToString()

                Case Else                          '運賃タリフの場合

                    Dim tmpdate As String = String.Empty

                    '適用日取得
                    tmpdate = Me._G.GetStrDate()

                    If Me._Vcon.SelectUnchinTariffListDataRow(dr, tariffCd, "", tmpdate, "00") = False Then
                        Call Me._Vcon.SetErrorControl(.txtUntinTariffCD)
                        .lblUntinTariffREM.TextValue = String.Empty
                        Return False
                    End If

                    '名称を設定
                    .lblUntinTariffREM.TextValue = dr(0).Item("UNCHIN_TARIFF_REM").ToString()

            End Select

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

            '値がない場合、スルー
            Dim extcCd As String = .txtExtcTariff.TextValue
            If String.IsNullOrEmpty(extcCd) = True Then
                Return True
            End If

            Dim dr As DataRow() = Nothing
            If Me._Vcon.SelectExtcUnchinListDataRow(dr, .cmbEigyo.SelectedValue.ToString(), extcCd) = False Then
                Call Me._Vcon.SetErrorControl(.txtExtcTariff)
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 商品マスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGoodsExistChk() As Boolean

        With Me._Frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim cd As String = .txtCustGoodsCD.TextValue.Trim()
            Dim key As String = .lblNrsGoodsCD.TextValue.Trim()
            Dim custCdL As String = .txtCustCDL.TextValue.Trim()
            Dim custCdM As String = .txtCustCDM.TextValue.Trim()
            Dim dr As DataRow() = Nothing

            '商品コードのチェック
            If Me._Vcon.SelectGoodsListDataRow(dr, brCd, custCdL, custCdM, key, cd) = False Then
                Call Me._Vcon.SetErrorControl(.txtCustGoodsCD)
                Return False
            End If

            '▼▼▼要望番号:614
            '商品Keyに値がない 且つ 複数レコードある場合、エラー
            If String.IsNullOrEmpty(key) = True _
                AndAlso 1 < dr.Length _
                Then

                Me._Vcon.SetErrorControl(.txtCustGoodsCD, .tabMiddle, .tabGoods)
                Return Me._Vcon.SetErrMessage("E206", New String() {"商品コード", "商品KEY"})

            End If

            '名称を設定
            .lblGoodsNM.TextValue = dr(0).Item("GOODS_NM_1").ToString()
            .lblNrsGoodsCD.TextValue = dr(0).Item("GOODS_CD_NRS").ToString()
            '▲▲▲要望番号:614

        End With

        Return True

    End Function

#End Region 'マスタ存在チェック

#Region "内部メソッド"

    ''' <summary>
    ''' 保存時、ヘッダの単項目チェック（EDI出荷（大））
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsLHeaderChk() As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            '出庫日
            If Me._Vcon.IsInputDateFullByteChk(.imdOutkoDate, .lblOutkoDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '出荷予定日
            .imdOutkaPlanDate.ItemName = "出荷予定日"
            .imdOutkaPlanDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdOutkaPlanDate) = errorFlg OrElse _
               Me._Vcon.IsInputDateFullByteChk(.imdOutkaPlanDate, .lblOutkaPlanDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '納入予定日
            .imdArrPlanDate.ItemName = "納入予定日"
            .imdArrPlanDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdArrPlanDate) = errorFlg OrElse _
               Me._Vcon.IsInputDateFullByteChk(.imdArrPlanDate, .lblArrPlanDate.Text) = errorFlg Then
                Return errorFlg
            End If

            'オーダー番号
            If Me._Vcon.IsKinsiByteChk(.txtCustOrdNO, .lblCustOrdNO.Text, 30) = errorFlg Then
                Return errorFlg
            End If

            '注文番号
            If Me._Vcon.IsKinsiByteChk(.txtBuyerOrdNO, .lblBuyerOrdNO.Text, 30) = errorFlg Then
                Return errorFlg
            End If

            '売上先コード
            If Me._Vcon.IsKinsiByteChk(.txtShipCDL, .lblShipCDL.Text, 15) = errorFlg Then
                Return errorFlg
            End If

            '届先コード
            .txtDestCD.ItemName = "届先コード"
            .txtDestCD.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.txtDestCD) = errorFlg OrElse _
               Me._Vcon.IsKinsiByteChk(.txtDestCD, .lblDestCD.Text, 15) = errorFlg Then
                Return errorFlg
            End If

            '売上先コード（存在チェック）
            With Me._Frm.sprFreeInputsL.Sheets(0)
                For i As Integer = 0 To .RowCount - 1
                    If .Cells(i, LMH040C.SprFreeColumnIndex.COLNM).Value.ToString.Equals("FREE_C30") = True Then

                        If String.IsNullOrEmpty(.Cells(i, LMH040C.SprFreeColumnIndex.INPUT).Value.ToString) = False Then

                            Select Case .Cells(i, LMH040C.SprFreeColumnIndex.INPUT).Value.ToString.Substring(0, 2)
                                Case "01", "02", "03", "04"
                                    'FREE_C30項目の前2桁が01～04の場合はチェックスキップ
                                Case Else
                                    '届先コード存在チェック
                                    If Me.IsDestExistChk() = errorFlg Then
                                        Return errorFlg
                                    End If
                                    '売上先コードが入力されている場合、存在チェック
                                    If String.IsNullOrEmpty(Me._Frm.txtShipCDL.TextValue.Trim()) = False Then
                                        If Me.IsUriagesakiExistChk() = errorFlg Then
                                            Return errorFlg
                                        End If
                                    End If
                            End Select

                        End If
                    End If
                Next
            End With

            '(2012.12.11)要望番号1585 40byte→60byte -- START --
            '届先住所3
            'If Me._Vcon.IsKinsiByteChk(.txtDestAd3, "届先住所3", 40) = errorFlg Then
            '2019/11/26 要望管理009400 rep
            'If Me._Vcon.IsKinsiByteChk(.txtDestAd3, "届先住所3", 60) = errorFlg Then
            If Me._Vcon.IsKinsiByteChk(.txtDestAd3, "届先住所3", 80) = errorFlg Then
                Return errorFlg
            End If
            '(2012.12.11)要望番号1585 40byte→60byte --  END  --

            '届先(EDI)
            If Me._Vcon.IsKinsiByteChk(.txtEDIDestCD, .lblEDIDestCD.Text, 20) = errorFlg Then
                Return errorFlg
            End If

            '届先郵便番号
            If Me._Vcon.IsKinsiByteChk(.txtDestZip, .lblDestZip.Text, 10) = errorFlg OrElse _
               Me._Vcon.IsLiteralChk(.txtDestZip, .txtDestZip.TextValue, .lblDestZip.Text) = errorFlg Then
                Return errorFlg
            End If

            '届先JISコード
            If Me._Vcon.IsKinsiByteChk(.txtDestJisCD, .lblDestJisCD.Text, 7) = errorFlg Then
                Return errorFlg
            End If

            '届先電話番号
            '2012.04.25 要望番号1009 修正START 数値チェックを外す(15byte⇒20byte)
            'If Me._Vcon.IsKinsiByteChk(.txtDestTel, .lblDestTel.Text, 15) = errorFlg OrElse _
            '   Me._Vcon.IsLiteralChk(.txtDestTel, .txtDestTel.TextValue, .lblDestTel.Text) = errorFlg Then
            '    Return errorFlg
            'End If

            If Me._Vcon.IsKinsiByteChk(.txtDestTel, .lblDestTel.Text, 20) = errorFlg Then
                Return errorFlg
            End If

            '2012.04.25 要望番号1009 修正END

            '届先FAX番号
            If Me._Vcon.IsKinsiByteChk(.txtDestFax, .lblDestFax.Text, 15) = errorFlg OrElse _
               Me._Vcon.IsLiteralChk(.txtDestFax, .txtDestFax.TextValue, .lblDestFax.Text) = errorFlg Then
                Return errorFlg
            End If

            '届先Email
            '2012.04.25 要望番号1009 修正START(40byte⇒30byte)
            'If Me._Vcon.IsKinsiByteChk(.txtDestEmail, .lblDestEmail.Text, 40) = errorFlg Then
            If Me._Vcon.IsKinsiByteChk(.txtDestEmail, .lblDestEmail.Text, 30) = errorFlg Then
                Return errorFlg
            End If
            '2012.04.25 要望番号1009 修正END

            '出荷時注意事項
            If Me._Vcon.IsKinsiByteChk(.txtRemark, .lblRemark.Text, 100) = errorFlg Then
                Return errorFlg
            End If

            '配送時注意事項
            If Me._Vcon.IsKinsiByteChk(.txtUnsoAtt, .lblUnsoAtt.Text, 100) = errorFlg Then
                Return errorFlg
            End If

            '運送会社コード
            If Me._Vcon.IsKinsiByteChk(.txtUnsoCD, "運送会社コード", 5) = errorFlg Then
                Return errorFlg
            End If

            '運送会社支店コード
            If Me._Vcon.IsKinsiByteChk(.txtUnsoBrCD, "運送会社支店コード", 3) = errorFlg Then
                Return errorFlg
            End If

            '運賃/横持ちタリフコード
            If Me._Vcon.IsKinsiByteChk(.txtUntinTariffCD, .lblTariff.Text, 10) = errorFlg Then
                Return errorFlg
            End If

            '運賃/横持ちタリフコード（存在チェック）
            If Me.IsTariffExistChk() = errorFlg Then
                Return errorFlg
            End If

            '割増タリフコード
            If Me._Vcon.IsKinsiByteChk(.txtExtcTariff, .lblExtcTariff.Text, 10) = errorFlg Then
                Return errorFlg
            End If

            '割増タリフコード（存在チェック）
            If Me.IsExtcExistChk() = errorFlg Then
                Return errorFlg
            End If

        End With

        Return chkFlg

    End Function

    ''' <summary>
    ''' ヘッダの単項目チェック（EDI出荷（中））
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMHeaderChk(Optional ByVal goodsCheck As Boolean = True) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False
        Dim unsoFlg As Boolean = False '運送時Mタブ固定対応 terakawa 2012.06.15

        With Me._Frm

            '運送時Mタブ固定対応 terakawa 2012.06.15 Start
            'EDI出荷(大)のFREE_C30の値を取得
            Dim freeC30 As String = Me._Vcon.GetCellValue( _
                                    .sprFreeInputsL.ActiveSheet.Cells(Me.GetFreeC30Row(), LMH040G.sprFreeLDef.INPUT.ColNo)).ToString()

            'FREE_C30の頭2文字が"01"の場合、運送データとして扱う
            If Left(freeC30, 2).ToString.Equals(LMH040C.UNSO_DATA) = True Then
                unsoFlg = True
            End If
            '運送時Mタブ固定対応 terakawa 2012.06.15 End


            '商品コード
            .txtCustGoodsCD.ItemName = "商品コード"
            .txtCustGoodsCD.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.txtCustGoodsCD) = errorFlg OrElse _
                Me._Vcon.IsKinsiByteChk(.txtCustGoodsCD, "商品コード", 30) = errorFlg Then
                Return errorFlg
            End If

            '運送時Mタブ固定対応 terakawa 2012.06.15 Start
            '商品コード（存在チェック）
            '運送データの場合は、商品コードの存在チェックを行わない
            '編集時、アクタス等（M_CUST_DETL:SUB_KB=80）は商品CDが複数あるので、チェックを行わない
            If unsoFlg = False And goodsCheck = True Then
                If Me.IsGoodsExistChk() = errorFlg Then
                    Return errorFlg
                End If
            End If
            '運送時Mタブ固定対応 terakawa 2012.06.15 End

            'オーダー番号
            If Me._Vcon.IsKinsiByteChk(.txtCustOrdNoDtl, .lblCustOrdNoDtl.Text, 30) = errorFlg Then
                Return errorFlg
            End If

            '予約番号
            If Me._Vcon.IsKinsiByteChk(.txtRsvNO, .lblRsvNO.Text, 10) = errorFlg Then
                Return errorFlg
            End If

            'シリアル№
            If Me._Vcon.IsKinsiByteChk(.txtSerialNO, .lblTitleSerialNo.Text, 40) = errorFlg Then
                Return errorFlg
            End If

            '注文番号
            If Me._Vcon.IsKinsiByteChk(.txtBuyerOrdNoDtl, .lblBuyerOrdNoDtl.Text, 30) = errorFlg Then
                Return errorFlg
            End If

            '入目
            .numIrime.ItemName = "入目"
            .numIrime.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.numIrime) = errorFlg OrElse _
                Me._Vcon.IsBoundsChk(Convert.ToDouble(.numIrime.Value), LMHControlC.MAX_6_3, LMHControlC.MIN_0, .lblIrime.Text) = errorFlg Then '要望番号:466
                'Me._Vcon.IsBoundsChk(Convert.ToDouble(.numIrime.Value), 999999.999, 0, .lblIrime.Text) = errorFlg Then
                Return errorFlg
            End If

            '出荷単位
            .cmbOutkaKB.ItemName = .lblOutkaKB.Text
            .cmbOutkaKB.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbOutkaKB) = errorFlg Then
                Return errorFlg
            End If

            '梱数
            .numOutkaPkgNB.ItemName = "梱数"
            .numOutkaPkgNB.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.numOutkaPkgNB) = errorFlg OrElse _
                Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaPkgNB.Value), LMHControlC.MAX_10, LMHControlC.MIN_0, .lblOutkaPkgNB.Text) = errorFlg Then '要望番号:466
                'Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaPkgNB.Value), 9999999999, 0, .lblOutkaPkgNB.Text) = errorFlg Then 
                Me._Vcon.SetErrorControl(.numOutkaPkgNB)
                Return errorFlg
            End If

            '端数

            'If Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaHasu.Value), 9999999999, 0, .lblOutkaHasu.Text) = errorFlg Then 
            If Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaHasu.Value), LMHControlC.MAX_10, LMHControlC.MIN_0, .lblOutkaHasu.Text) = errorFlg Then '要望番号:466
                Me._Vcon.SetErrorControl(.numOutkaHasu)
                Return errorFlg
            End If

            '個数
            'If Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaTtlNB.Value), 9999999999, 0, "出荷個数") = errorFlg Then 
            If Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaTtlNB.Value), LMHControlC.MAX_10, LMHControlC.MIN_0, "個数") = errorFlg Then '要望番号:466
                Me._Vcon.SetErrorControl(.numOutkaTtlNB)
                Return errorFlg
            End If

            '数量
            'If Me._Vcon.IsSujiByteChk(.numOutkaTtlQT.Value.ToString, 9, 3, "出荷数量") = errorFlg OrElse _
            If Me._Vcon.IsBoundsChk(Convert.ToDouble(.numOutkaTtlQT.Value), LMHControlC.MAX_9_3, LMHControlC.MIN_0, "数量") = errorFlg Then  '要望番号:466
                Me._Vcon.SetErrorControl(.numOutkaTtlNB)
                Return errorFlg
            End If

            '商品別注意事項
            If Me._Vcon.IsKinsiByteChk(.txtGoodsRemark, .lblGoodsRemark.Text, 100) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return chkFlg

    End Function

    ''' <summary>
    ''' 自由設定項目チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSprChk(ByVal spr As LM.GUI.Win.Spread.LMSpread) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False
        Dim dbColNm As String = String.Empty
        Dim input As String = String.Empty
        Dim title As String = String.Empty
        Dim vCell As LMValidatableCells = New LMValidatableCells(spr)

        With spr.Sheets(0)

            For i As Integer = 0 To .RowCount - 1
                dbColNm = .Cells(i, LMH040C.SprFreeColumnIndex.COLNM).Value.ToString()

                input = Me.NothingConvertString(.Cells(i, LMH040C.SprFreeColumnIndex.INPUT).Value).ToString()
                title = .Cells(i, LMH040C.SprFreeColumnIndex.TITLE).Value.ToString()

                If dbColNm.Substring(0, 6).Equals("FREE_N") Then
                    '数値01～10
                    If String.IsNullOrEmpty(input) = True Then
                        input = "0"
                        .Cells(i, LMH040C.SprFreeColumnIndex.INPUT).Value = input
                    End If

                    '▼▼▼(マイナスデータ)
                    'If Me._Vcon.IsBoundsChk(Convert.ToDouble(input), 999999999.999, 0, title) = errorFlg Then
                    '    Call Me._Vcon.SetErrorControl(spr, i, LMH040C.SprFreeColumnIndex.INPUT)
                    '    Return errorFlg
                    'End If
                    '▲▲▲(マイナスデータ)

                ElseIf Convert.ToInt16(dbColNm.Substring(6, 2)) <= 20 Then
                    '文字列01～20
                    If Me._Vcon.IsKinsiByteChk(vCell, i, LMH040C.SprFreeColumnIndex.INPUT, title, 100) = errorFlg Then
                        Return errorFlg
                    End If

                Else
                    '文字列21～30
                    If Me._Vcon.IsKinsiByteChk(vCell, i, LMH040C.SprFreeColumnIndex.INPUT, title, 200) = errorFlg Then
                        Return errorFlg
                    End If

                End If
            Next

        End With

        Return chkFlg

    End Function

    ''' <summary>
    ''' ヘッダの関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputLConnectionChk() As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            '運送会社コード＋運送会社支店コード
            Dim unsoCd As String = .txtUnsoCD.TextValue
            Dim unsoBrCd As String = .txtUnsoBrCD.TextValue

            If String.IsNullOrEmpty(unsoCd) = False _
                AndAlso String.IsNullOrEmpty(unsoBrCd) = False Then
                '両方に値がある場合、運送会社コード＋運送会社支店コード存在チェック
                If Me.IsUnsocoExistChk() = errorFlg Then
                    Call Me._Vcon.SetErrorControl(.txtUnsoCD)
                    Return Me._Vcon.SetErrMessage("E079", New String() {"運送会社マスタ", .txtUnsoCD.TextValue})
                End If
            ElseIf String.IsNullOrEmpty(unsoCd) = True _
                    AndAlso String.IsNullOrEmpty(unsoBrCd) = True Then
                '両方に値がない場合、スルー
            Else
                If String.IsNullOrEmpty(unsoCd) = True Then
                    Call Me._Vcon.SetErrorControl(.txtUnsoCD)
                Else
                    Call Me._Vcon.SetErrorControl(.txtUnsoBrCD)
                End If
                Return Me._Vcon.SetErrMessage("E017", New String() {"運送会社コード", "運送会社支店コード"})
            End If

            '2011.10.04 廃止START EDIメモ№60

            ''タリフ分類（画面）＋タリフ分類（タリフセット）
            'Dim TariffKb As String = String.Empty
            'Dim TariffCd As String = String.Empty

            'TariffCd = .txtUntinTariffCD.TextValue()

            ''タリフコードが入力されていない場合、or 横持タリフの場合、スルー
            'If String.IsNullOrEmpty(TariffCd) = True OrElse _
            '   LMHControlC.TARIFF_YOKO.Equals(.cmbUnsoTehaiKB.SelectedValue.ToString) = True Then
            '    Return True
            'End If

            ''タリフセットマスタから値取得
            'Dim getdr As DataRow() = Me._G.GetTariffSetDataRow(Me._Frm, "")

            'If getdr.Length < 1 Then
            '    Call Me._Vcon.SetErrorControl(.txtUntinTariffCD)
            '    Call Me._Vcon.SetErrorControl(.cmbUnsoTehaiKB)
            '    MyBase.ShowMessage("E157")
            '    Return errorFlg
            'End If

            'TariffKb = getdr(0).Item("TARIFF_BUNRUI_KB").ToString

            'If TariffKb.Equals(.cmbUnsoTehaiKB.SelectedValue.ToString) = False Then
            '    Call Me._Vcon.SetErrorControl(.txtUntinTariffCD)
            '    MyBase.ShowMessage("E157")
            '    Return errorFlg
            'End If

            '2011.10.04 廃止END EDIメモ№60

        End With

        Return chkFlg

    End Function

    ''' <summary>
    ''' 商品情報部の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Public Function IsInputMConnectionChk(Optional ByVal notFocusFlg As Boolean = False) As Boolean

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        With Me._Frm

            Dim hasu As Decimal = Convert.ToDecimal(.numOutkaHasu.Value)
            Dim konsu As Decimal = Convert.ToDecimal(.numOutkaPkgNB.Value)
            Dim irisu As Decimal = Convert.ToDecimal(.numPkgNB.Value)
            Dim irime As Decimal = Convert.ToDecimal(.numIrime.Value)
            Dim suryo As Decimal = Convert.ToDecimal(.numOutkaTtlQT.Value)

            '出荷単位
            If .optCnt.Checked = True Then  '個数

                If konsu <> 0 AndAlso _
                   Me._Vcon.IsLargeSmallChk(irisu, hasu, True) = errorFlg Then

                    If notFocusFlg = False Then
                        Call Me._Vcon.SetErrorControl(.numOutkaHasu)
                    End If

                    MyBase.ShowMessage("E218")
                    Return errorFlg
                End If

                'If Me._Vcon.IsBoundsChk(konsu * irisu + hasu, 9999999999, 0, "出荷個数") = errorFlg Then
                If Me._Vcon.IsBoundsChk(konsu * irisu + hasu, LMHControlC.MAX_10, LMHControlC.MIN_0, "出荷個数") = errorFlg Then
                    If notFocusFlg = False Then
                        Call Me._Vcon.SetErrorControl(.numOutkaHasu)
                        Call Me._Vcon.SetErrorControl(.numOutkaPkgNB)
                    End If

                    MyBase.ShowMessage("E167", New String() {"出荷個数"})
                    Return errorFlg
                End If

            ElseIf .optAmt.Checked = True Then  '数量

                If suryo / irime > 9999999999 Then

                    If notFocusFlg = False Then
                        Call Me._Vcon.SetErrorControl(.numOutkaTtlQT)
                    End If

                    MyBase.ShowMessage("E167", New String() {"出荷個数"})
                    Return errorFlg
                End If

                If suryo * 1000 Mod irime * 1000 <> 0 Then

                    If notFocusFlg = False Then
                        Call Me._Vcon.SetErrorControl(.numOutkaTtlQT)
                    End If

                    MyBase.ShowMessage("E170", New String() {""})
                    Return errorFlg
                End If

                '▼▼▼(マイナスデータ)
                'If suryo / irime < 1 Then

                '    If notFocusFlg = False Then
                '        Call Me._Vcon.SetErrorControl(.numOutkaTtlQT)
                '    End If

                '    MyBase.ShowMessage("E171", New String() {""})
                '    Return errorFlg
                'End If
                '▲▲▲(マイナスデータ)

            ElseIf .optSample.Checked = True Then 'サンプル
                If irime < suryo Then

                    If notFocusFlg = False Then
                        Call Me._Vcon.SetErrorControl(.numOutkaTtlQT)
                    End If

                    MyBase.ShowMessage("E421")

                    Return errorFlg
                End If

            End If

            Return True

        End With

        Return chkFlg

    End Function


    '▼▼▼要望番号:466
    ''' <summary>
    ''' 中情報の合計値チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function IsMDataSumChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMH040C.TABLE_NM_OUT_M)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Decimal = 0
        Dim sumQt As Decimal = 0
        Dim irisu As Decimal = Convert.ToDecimal(Me._Frm.numPkgNB.TextValue)

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(dr.Item("JYOTAI").ToString()) = True Then
                Continue For
            End If

            sumNb = sumNb + Convert.ToDecimal(dr.Item("OUTKA_TTL_NB"))
            sumQt = sumQt + Convert.ToDecimal(dr.Item("OUTKA_TTL_QT"))

            '範囲チェック(個数)

            '範囲チェック(個数)
            If Me.IsCalcOver(sumNb.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_10, "中データ個数の合計") = False Then

                If 1 < irisu Then
                    Call Me._Vcon.SetErrorControl(Me._Frm.numOutkaPkgNB)
                Else
                    Call Me._Vcon.SetErrorControl(Me._Frm.numOutkaHasu)
                End If

                Return False

            End If

            '上限チェック(数量)
            If Me.IsCalcOver(sumQt.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_9_3, "中データ数量の合計") = False Then

                If 1 < irisu Then
                    Call Me._Vcon.SetErrorControl(Me._Frm.numOutkaPkgNB)
                Else
                    Call Me._Vcon.SetErrorControl(Me._Frm.numOutkaHasu)
                End If

                Return False

            End If

        Next


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

        Return Me._Vcon.SetErrMessage("E014", New String() {msg, "0", maxData})

    End Function

    '▲▲▲要望番号:466

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMH040C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .txtShipCDL.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtShipCDL}
                    lblCtl = New Control() {.lblShipNM}
                    msg = New String() {"売上先コード"}

                Case .txtDestCD.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtDestCD}
                    lblCtl = New Control() {.lblDestNM}
                    msg = New String() {"届先コード"}

                Case .txtUnsoCD.Name, .txtUnsoBrCD.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUnsoCD, .txtUnsoBrCD}
                    lblCtl = New Control() {.lblUnsoNM}
                    msg = New String() {String.Concat(.lblUnso.Text, "コード"), String.Concat(.lblUnso.Text, "支店コード")}

                Case .txtUntinTariffCD.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtUntinTariffCD}
                    lblCtl = New Control() {.lblUntinTariffREM}
                    msg = New String() {String.Concat(.lblTariff.Text, "コード")}

                Case .txtExtcTariff.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtExtcTariff}
                    msg = New String() {String.Concat(.lblExtcTariff.Text, "コード")}

                Case .txtRemark.Name, .txtGoodsRemark.Name, .txtUnsoAtt.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtRemark, .txtGoodsRemark, .txtUnsoAtt}
                    msg = New String() {.lblRemark.Text, .lblGoodsRemark.Text, .lblUnsoAtt.Text}

                Case .txtCustGoodsCD.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCustGoodsCD}
                    lblCtl = New Control() {.lblGoodsNM, .lblNrsGoodsCD}
                    msg = New String() {"商品コード"}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), ctl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    ''' カーソル位置チェック
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="cell">セル</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusSprChk(ByVal spr As Win.Spread.LMSpread, ByVal cell As FarPoint.Win.Spread.Cell) As Boolean

        'ロック項目はスルー
        If cell.Locked = True OrElse spr.ActiveSheet.Columns(cell.Column.Index).Locked = True Then

            Return Me._Vcon.SetFocusErrMessage()

        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMH040C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMH040C.ActionType.EDIT, _
                 LMH040C.ActionType.MASTEROPEN, _
                 LMH040C.ActionType.SAVE, _
                 LMH040C.ActionType.SINKI, _
                 LMH040C.ActionType.ROWRE, _
                 LMH040C.ActionType.ROWDEL, _
                 LMH040C.ActionType.ENTER, _
                 LMH040C.ActionType.ROWSELECT

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

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprGoodsDef)
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprFreeInputsL)
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprFreeInputsM)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustOrdNO.TextValue = .txtCustOrdNO.TextValue.Trim()
            .txtBuyerOrdNO.TextValue = .txtBuyerOrdNO.TextValue.Trim()
            .txtShipCDL.TextValue = .txtShipCDL.TextValue.Trim()
            .txtDestCD.TextValue = .txtDestCD.TextValue.Trim()
            .txtDestAd3.TextValue = .txtDestAd3.TextValue.Trim()
            .txtEDIDestCD.TextValue = .txtEDIDestCD.TextValue.Trim()
            .txtDestZip.TextValue = .txtDestZip.TextValue.Trim()
            .txtDestJisCD.TextValue = .txtDestJisCD.TextValue.Trim()
            .txtDestTel.TextValue = .txtDestTel.TextValue.Trim()
            .txtDestFax.TextValue = .txtDestFax.TextValue.Trim()
            .txtDestEmail.TextValue = .txtDestEmail.TextValue.Trim()
            .txtRemark.TextValue = .txtRemark.TextValue.Trim()
            .txtGoodsRemark.TextValue = .txtGoodsRemark.TextValue.Trim()
            .txtCustGoodsCD.TextValue = .txtCustGoodsCD.TextValue.Trim()
            .txtSerialNO.TextValue = .txtSerialNO.TextValue.Trim()
            .txtCustOrdNoDtl.TextValue = .txtCustOrdNoDtl.TextValue.Trim()
            .txtRsvNO.TextValue = .txtRsvNO.TextValue.Trim()
            .txtBuyerOrdNoDtl.TextValue = .txtBuyerOrdNoDtl.TextValue.Trim()
            .txtUnsoAtt.TextValue = .txtUnsoAtt.TextValue.Trim()
            .txtUnsoCD.TextValue = .txtUnsoCD.TextValue.Trim()
            .txtUnsoBrCD.TextValue = .txtUnsoBrCD.TextValue.Trim()
            .txtUntinTariffCD.TextValue = .txtUntinTariffCD.TextValue.Trim()
            .txtExtcTariff.TextValue = .txtExtcTariff.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 選択行有無判別
    ''' </summary>
    ''' <param name="rowCnt">選択行数（省略可）</param>
    ''' <returns>-1：選択行無し　0：単一行選択中　1：複数行選択中 </returns>
    ''' <remarks></remarks>
    Friend Function FindSelectedRow(Optional ByRef rowCnt As Integer = 0) As Integer

        With Me._Frm.sprGoodsDef.Sheets(0)

            Dim rowIdx As Integer = -1

            For i As Integer = 0 To .RowCount - 1
                If .Cells(i, LMH040G.sprGoodsDef.DEF.ColNo).Value IsNot Nothing AndAlso _
                   .Cells(i, LMH040G.sprGoodsDef.DEF.ColNo).Value.ToString = True.ToString Then

                    rowCnt = rowCnt + 1

                    If rowIdx = 0 Then
                        rowIdx = 1
                    End If
                    If rowIdx <> 1 Then
                        rowIdx = 0
                    End If

                End If
            Next

            'If rowCnt = .RowCount() Then
            '    Return -2
            'End If

            Return rowIdx

        End With

    End Function

    ''' <summary>
    ''' 状態混在チェック（スプレッド）
    ''' </summary>
    ''' <param name="str"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDelKbChk(ByVal str As String) As Boolean

        With Me._Frm.sprGoodsDef.Sheets(0)

            For i As Integer = 0 To .RowCount - 1
                If .Cells(i, LMH040G.sprGoodsDef.DEF.ColNo).Value IsNot Nothing AndAlso _
                   .Cells(i, LMH040G.sprGoodsDef.DEF.ColNo).Value.ToString = True.ToString AndAlso _
                   .Cells(i, LMH040G.sprGoodsDef.DEL.ColNo).Value.ToString.Equals(str) = True Then

                    MyBase.ShowMessage("E286", New String() {String.Concat(str, "レコード")})
                    Return False

                End If
            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' ノーマルデータが0件になる場合エラー
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="arr"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsSeijoDataCountChk(ByVal ds As DataSet, ByVal arr As ArrayList, ByVal arrNot As ArrayList) As Boolean

        '大情報の削除区分が通常以外、スルー
        If LMH040C.STATUS_NOMAL.Equals(ds.Tables(LMH040C.TABLE_NM_OUT_L).Rows(0).Item("EDI_STATE_KB").ToString()) = False Then
            Return True
        End If

        Dim dt As DataTable = ds.Tables(LMH040C.TABLE_NM_OUT_M)
        Dim max As Integer = arrNot.Count - 1
        Dim nomalCount As Integer = 0
        Dim recNo As Integer = 0
        Dim jyotai As String = String.Empty

        For i As Integer = 0 To max

            'レコード番号を取得
            recNo = Convert.ToInt32(Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arrNot(i)), LMH040G.sprGoodsDef.DEF.ColNo)))
            '状態を取得
            jyotai = Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arrNot(i)), LMH040G.sprGoodsDef.DEL.ColNo))

            '正常データ件数をカウント
            'If LMH040C.DEL_KB_OK.Equals(dt.Rows(recNo).Item("DEL_KB").ToString()) = True AndAlso jyotai.Equals(LMH040C.DEL_KB_OK_NM) Then
            If jyotai.Equals(LMH040C.DEL_KB_OK_NM) Then
                nomalCount = nomalCount + 1
            End If

        Next

        If nomalCount = 0 Then
            Return Me._Vcon.SetErrMessage("E412")
        End If

        Return True

    End Function

    ''' <summary>
    ''' ステータスチェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsStaChk() As Boolean

        '2012.03.15 要望番号895 大阪対応START
        'EDIステータスが通常("00"(正常))または("05"(保留))以外の場合、エラー
        If Me._Frm.cmbEDIStateKB.SelectedValue.ToString.Equals("00") = True OrElse _
           Me._Frm.cmbEDIStateKB.SelectedValue.ToString.Equals("05") = True Then

        Else
            MyBase.ShowMessage("E298")
            Return False
        End If
        '2012.03.15 要望番号895 大阪対応END

        Return True

    End Function

    '運送時Mタブ固定対応 terakawa 2012.06.15 Start
    ''' <summary>
    ''' FREE_C30行番号取得
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetFreeC30Row() As Integer
        Dim freeC30Row As Integer = 0
        For i As Integer = 0 To Me._Frm.sprFreeInputsL.ActiveSheet.RowCount - 1
            If Me._Vcon.GetCellValue(Me._Frm.sprFreeInputsL.ActiveSheet _
                                     .Cells(i, LMH040G.sprFreeLDef.DB_COL_NM.ColNo)).ToString() = LMH040C.COLMUN_NM_FREE_C30 Then
                freeC30Row = i
                Exit For
            End If
        Next
        Return freeC30Row
    End Function
    '運送時Mタブ固定対応 terakawa 2012.06.15 End

    ''' <summary>
    ''' 数字コントロールの値設定
    ''' </summary>
    ''' <param name="clt"></param>
    ''' <remarks></remarks>
    Friend Sub CheckNumberValue(ByRef clt As Win.InputMan.LMImNumber)
        If clt.Value Is Nothing Then
            clt.Value = 0
        End If
    End Sub

    ''' <summary>
    ''' Nothing変換（文字列）
    ''' </summary>
    ''' <param name="value"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function NothingConvertString(ByVal value As Object) As Object

        If value Is Nothing Then
            value = String.Empty
        End If

        Return value

    End Function

#End Region '内部メソッド

    ''' <summary>
    ''' 明細0件チェック
    ''' </summary>
    ''' <param name="rowCnt">行数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsRowCntChk(ByVal rowCnt As Integer) As Boolean

        If rowCnt < 0 Then
            Return Me._Vcon.SetErrMessage("E231", New String() {"（中）"})
        End If

        Return True

    End Function

#End Region 'Method

End Class
