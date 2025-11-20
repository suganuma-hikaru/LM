' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH     : EDI
'  プログラムID     :  LMH020V : EDI入荷データ編集
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMH020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMH020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMH020F

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
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMH020G


#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMH020F, ByVal v As LMHControlV, ByVal gCon As LMHControlG, ByVal g As LMH020G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = gCon

        Me._G = g

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck(ByVal ds As DataSet) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()
        rtnResult = rtnResult AndAlso Me.IsSprChk(Me._Frm.sprFreeL)
        rtnResult = rtnResult AndAlso Me.IsSprChk(Me._Frm.sprFreeM)

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '中情報の全行チェック
        rtnResult = rtnResult AndAlso Me.IsMDataAllChk(ds)

        '▼▼▼要望番号:466
        '中情報の合計値チェック
        rtnResult = rtnResult AndAlso Me.IsMDataSumChk(ds)
        '▲▲▲要望番号:466

        'ワーニングチェック
        rtnResult = rtnResult AndAlso Me.IsWarningChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' ヘッダの単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHeaderChk() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '入荷日
            .imdNyukaDate.ItemName = .lblTitleNyukaDate.Text
            .imdNyukaDate.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.imdNyukaDate) = errorFlg Then
                Return errorFlg
            End If
            If Me._Vcon.IsInputDateFullByteChk(.imdNyukaDate, .lblTitleNyukaDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '注文番号
            .txtBuyerOrdNo.ItemName = .lblTitleBuyerOrdNo.Text
            .txtBuyerOrdNo.IsForbiddenWordsCheck = chkFlg
            .txtBuyerOrdNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtBuyerOrdNo) = errorFlg Then
                Return errorFlg
            End If

            'オーダー番号
            .txtOrderNo.ItemName = .lblTitleOrderNo.Text
            .txtOrderNo.IsForbiddenWordsCheck = chkFlg
            .txtOrderNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtOrderNo) = errorFlg Then
                Return errorFlg
            End If

            '保管料起算日
            If Me._Vcon.IsInputDateFullByteChk(.imdHokanDate, .lblTitleHokanDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '備考大（社外）
            .txtNyubanL.ItemName = .lblTitleNyubanL.Text
            .txtNyubanL.IsForbiddenWordsCheck = chkFlg
            .txtNyubanL.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtNyubanL) = errorFlg Then
                Return errorFlg
            End If

            '備考大（社内）
            .txtNyukaComment.ItemName = .lblTitleNyukaComment.Text
            .txtNyukaComment.IsForbiddenWordsCheck = chkFlg
            .txtNyukaComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtNyukaComment) = errorFlg Then
                Return errorFlg
            End If

            '中の単項目チェック
            If Me.IsInputMChk() = errorFlg Then
                Return errorFlg
            End If

            '運送会社コード
            .txtUnsoCd.ItemName = String.Concat(.lblTitleUnso.Text, "コード")
            .txtUnsoCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsoCd.IsByteCheck = 5
            .txtUnsoCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsoCd) = errorFlg Then
                .tabMiddle.SelectedTab = .tabUnso
                Return errorFlg
            End If

            '運送会社支店コード
            .txtUnsoBrCd.ItemName = String.Concat(.lblTitleUnso.Text, "支店コード")
            .txtUnsoBrCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsoBrCd.IsByteCheck = 3
            .txtUnsoBrCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsoBrCd) = errorFlg Then
                .tabMiddle.SelectedTab = .tabUnso
                Return errorFlg
            End If

            '運送タリフ
            .txtTariffCd.ItemName = String.Concat(.lblTitleTariff.Text, "コード")
            .txtTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtTariffCd.IsByteCheck = 10
            .txtTariffCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtTariffCd) = errorFlg Then
                .tabMiddle.SelectedTab = .tabUnso
                Return errorFlg
            End If

            '出荷元
            .txtShukkaMotoCd.ItemName = String.Concat(.lblTitleShukkaMotoCd.Text, "コード")
            .txtShukkaMotoCd.IsForbiddenWordsCheck = chkFlg
            .txtShukkaMotoCd.IsByteCheck = 15
            .txtShukkaMotoCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtShukkaMotoCd) = errorFlg Then
                .tabMiddle.SelectedTab = .tabUnso
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' フリースプレッドの単項目チェック
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprChk(ByVal spr As Win.Spread.LMSpread) As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(spr)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

                'ナンバーセルの場合、スルー
                If TypeOf spr.ActiveSheet.Cells(i, LMH020G.sprFreeDef.FREE.ColNo).Editor Is FarPoint.Win.Spread.CellType.NumberCellType Then
                    Continue For
                End If

                '入力
                .SetValidateCell(i, LMH020G.sprFreeDef.FREE.ColNo)
                .ItemName = Me._Gcon.GetCellValue(spr.ActiveSheet.Cells(i, LMH020G.sprFreeDef.TITLE.ColNo))
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = Me.GetByteData(Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(i, LMH020G.sprFreeDef.COLNM.ColNo)))
                If MyBase.IsValidateCheck(vCell) = errorFlg Then

                    If Me._Frm.sprFreeL.Name.Equals(spr.Name) = True Then
                        Me._Frm.tabMiddle.SelectTab(Me._Frm.tabFreeL)
                    End If

                    Return errorFlg

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

        '運送会社マスタ存在チェック
        Dim rtnResult As Boolean = Me.IsUnsocoExistChk()

        'タリフの存在チェック
        rtnResult = rtnResult AndAlso Me.IsTariffExistChk()

        ''タリフセットの存在チェック
        'rtnResult = rtnResult AndAlso Me.IsTariffSetExistChk()

        '届先の存在チェック
        rtnResult = rtnResult AndAlso Me.IsDestExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk() As Boolean

        With Me._Frm

            Dim unsoCd As String = .txtUnsoCd.TextValue
            Dim unsoBrCd As String = .txtUnsoBrCd.TextValue

            '片方に値がある場合、エラー
            If (String.IsNullOrEmpty(unsoCd) = True _
                AndAlso String.IsNullOrEmpty(unsoBrCd) = False) _
                OrElse _
                (String.IsNullOrEmpty(unsoCd) = False _
                AndAlso String.IsNullOrEmpty(unsoBrCd) = True) _
                Then
                .txtUnsoBrCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.txtUnsoCd, .tabMiddle, .tabUnso)
                Dim lbl As String = .lblTitleUnso.Text
                Return Me._Vcon.SetErrMessage("E017", New String() {String.Concat(lbl, "コード"), String.Concat(lbl, "支店コード")})
            End If

            '値がない場合、スルー
            If String.IsNullOrEmpty(unsoCd) = True Then
                Return True
            End If
            If String.IsNullOrEmpty(unsoBrCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectUnsocoListDataRow(drs, unsoCd, unsoBrCd) = False Then
                .txtUnsoBrCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.txtUnsoCd, .tabMiddle, .tabUnso)
                Return False
            End If

            '名称を設定
            .lblUnsoNm.TextValue = Me._Gcon.EditConcatData(drs(0).Item("UNSOCO_NM").ToString(), drs(0).Item("UNSOCO_BR_NM").ToString(), LMHControlC.ZENKAKU_SPACE)
            .txtUnsoCd.TextValue = drs(0).Item("UNSOCO_CD").ToString()
            .txtUnsoBrCd.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString()
            Return True

        End With

    End Function

    ''' <summary>
    ''' タリフの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffExistChk() As Boolean

        With Me._Frm

            '値が無い場合、スルー
            Dim tariffCd As String = .txtTariffCd.TextValue
            If String.IsNullOrEmpty(tariffCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            Dim name As String = String.Empty
            Dim nameCd As String = String.Empty
            Dim tariffKbn As String = .cmbTariffKbn.SelectedValue.ToString()
            Dim rtnResult As Boolean = False
            If LMHControlC.TARIFF_YOKO.Equals(tariffKbn) = True Then

                rtnResult = Me._Vcon.SelectYokoTariffListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), tariffCd)
                name = "YOKO_REM"
                nameCd = "YOKO_TARIFF_CD"
            Else

                rtnResult = Me._Vcon.SelectUnchinTariffListDataRow(drs, tariffCd, String.Empty, .imdNyukaDate.TextValue)
                name = "UNCHIN_TARIFF_REM"
                nameCd = "UNCHIN_TARIFF_CD"
            End If

            'エラーの場合、終了
            If rtnResult = False Then
                Me._Vcon.SetErrorControl(.txtTariffCd, .tabMiddle, .tabUnso)
                Return rtnResult
            End If

            '名称を設定
            .lblTariffNm.TextValue = drs(0).Item(name).ToString()
            .txtTariffCd.TextValue = drs(0).Item(nameCd).ToString()
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

            Dim drs As DataRow() = Nothing
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCdL.TextValue

            Dim destCd As String = .txtShukkaMotoCd.TextValue
            If String.IsNullOrEmpty(destCd) = False Then

                '出荷元のチェック
                If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, destCd) = False Then
                    Me._Vcon.SetErrorControl(.txtShukkaMotoCd, .tabMiddle, .tabUnso)
                    Return False
                End If

                '名称を設定
                .lblShukkaMotoNm.TextValue = drs(0).Item("DEST_NM").ToString()
                .txtShukkaMotoCd.TextValue = drs(0).Item("DEST_CD").ToString()
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 中情報の全行チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>エラーの場合、対象レコードを画面に表示</remarks>
    Private Function IsMDataAllChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(dr.Item("JYOTAI").ToString()) = True Then
                Continue For
            End If

            '商品のマスタ存在チェック(保存時、入目は商品マスタ値を反映しない)
            If Me.IsGoodsExistChk(ds, dr, skipIrimeFlg:=True) = False Then
                Return False
            End If

            '計算処理 + チェック
            If Me.Calculation(dr) = False Then

                '中番を設定して詳細表示
                Me._Frm.lblKanriNoM.TextValue = dr.Item("EDI_CTL_NO_CHU").ToString()
                Me._G.SetInkaMHeaderData(ds, -1)

                'タブページの切替
                Me._Frm.tabMiddle.SelectedTab = Me._Frm.tabGoods

                Return False

            End If

        Next

        Return True

    End Function

    '▼▼▼要望番号:466
    ''' <summary>
    ''' 中情報の合計値チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function IsMDataSumChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1
        Dim sumNb As Decimal = 0
        Dim sumQt As Decimal = 0
        Dim irisu As Decimal = Convert.ToDecimal(Me._Frm.numIrisu.TextValue)

        For i As Integer = 0 To max

            dr = dt.Rows(i)

            '削除データの場合、スルー
            If LMConst.FLG.ON.Equals(dr.Item("JYOTAI").ToString()) = True Then
                Continue For
            End If

            sumNb = sumNb + Convert.ToDecimal(dr.Item("NB"))
            sumQt = sumQt + Convert.ToDecimal(dr.Item("SURYO"))

        Next

        '範囲チェック(個数)
        If Me.IsCalcOver(sumNb.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_10, "中データ個数の合計") = False Then

            If 1 < irisu Then
                Call Me._Vcon.SetErrorControl(Me._Frm.numKosu)
            Else
                Call Me._Vcon.SetErrorControl(Me._Frm.numHasu)
            End If

            Return False

        End If

        '上限チェック(数量)
        If Me.IsCalcOver(sumQt.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_9_3, "中データ数量の合計") = False Then

            If 1 < irisu Then
                Call Me._Vcon.SetErrorControl(Me._Frm.numKosu)
            Else
                Call Me._Vcon.SetErrorControl(Me._Frm.numHasu)
            End If

            Return False

        End If

        Return True

    End Function
    '▲▲▲要望番号:466

    ''' <summary>
    ''' 商品マスタの存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="chkFlg">チェックを行うかの判定フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGoodsExistChk(ByVal ds As DataSet, ByVal chkFlg As Boolean, Optional ByVal skipIrimeFlg As Boolean = False) As Boolean

        'チェックを行うかを判定
        If chkFlg = False Then
            Return True
        End If

        Return Me.IsGoodsExistChk(ds, Me._G.GetInkaMDataRow(ds), skipIrimeFlg)

    End Function

    ''' <summary>
    ''' 商品マスタの存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="dr">DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsGoodsExistChk(ByVal ds As DataSet, ByVal dr As DataRow, Optional ByVal skipIrimeFlg As Boolean = False) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim cd As String = dr.Item("CUST_GOODS_CD").ToString()
            Dim key As String = dr.Item("NRS_GOODS_CD").ToString()
            Dim mNo As String = dr.Item("EDI_CTL_NO_CHU").ToString()
            Dim custCdL As String = .txtCustCdL.TextValue.ToString().Trim()
            Dim custCdM As String = .txtCustCdM.TextValue.ToString().Trim()

            '商品コードがない場合、スルー
            If String.IsNullOrEmpty(cd) = True Then
                Call Me.SetGoodsMstClearData(dr)
                Return True
            End If

            '商品コードのチェック
            If Me._Vcon.SelectGoodsListDataRow(drs, brCd, custCdL, custCdM, key, cd) = False Then

                '中番を設定して詳細表示
                .lblKanriNoM.TextValue = mNo
                Me._G.SetInkaMHeaderData(ds, -1)
                Me._Vcon.SetErrorControl(.txtGoodsCd, .tabMiddle, .tabGoods)
                Return False

            End If

            '商品Keyに値がない 且つ 複数レコードある場合、エラー
            If String.IsNullOrEmpty(key) = True _
                AndAlso 1 < drs.Length _
                Then

                '中番を設定して詳細表示
                .lblKanriNoM.TextValue = mNo
                Me._G.SetInkaMHeaderData(ds, -1)
                Me._Vcon.SetErrorControl(.txtGoodsCd, .tabMiddle, .tabGoods)
                Return Me._Vcon.SetErrMessage("E206", New String() {"商品コード", "商品KEY"})

            End If

            '値設定
            Call Me.SetGoodsMstData(dr, drs(0), .lblKanriNoM.TextValue.Equals(mNo), skipIrimeFlg)

        End With

        Return True

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffSetExistChk() As Boolean

        With Me._Frm

            'タリフコードがない場合、スルー
            Dim tariffCd As String = .txtTariffCd.TextValue
            If String.IsNullOrEmpty(tariffCd) = True Then
                Return True
            End If

            Dim tariffKbn As String = .cmbTariffKbn.SelectedValue.ToString()

            Dim drs As DataRow() = Me._Vcon.SelectTariffSetListDataRow( _
                                                                          .cmbEigyo.SelectedValue.ToString() _
                                                                        , .txtCustCdL.TextValue _
                                                                        , .txtCustCdM.TextValue _
                                                                        , tariffKbn _
                                                                        , tariffCd _
                                                                        )

            '取得できない場合、エラー
            If drs.Length < 1 Then
                .txtTariffCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.cmbTariffKbn, .tabMiddle, .tabUnso)
                Return Me._Vcon.SetErrMessage("E157")
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 端数 , 入数の関連チェック
    ''' </summary>
    ''' <param name="irisu">入数</param>
    ''' <param name="konsu">梱数</param>
    ''' <param name="hasu">端数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHasuIrisuChk(ByVal irisu As Decimal, ByVal konsu As Decimal, ByVal hasu As Decimal) As Boolean

        With Me._Frm

            '梱数がゼロの以外の場合、チェックを行う
            If 0 <> konsu Then

                '大小チェック
                If Me.IsLargeSmallChk(irisu, hasu, True) = False Then
                    Me._Vcon.SetErrorControl(.numHasu, .tabMiddle, .tabGoods)
                    Return Me._Vcon.SetErrMessage("E218")
                End If

            End If

            Return True

        End With

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
    ''' ワーニングチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsWarningChk() As Boolean

        Dim rtnResult As Boolean = Me.IsTariffHissuChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' タリフコードの関連必須チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffHissuChk() As Boolean

        With Me._Frm

            '日陸手配以外、スルー
            If LMH020C.TEHAI_NRS.Equals(.cmbUnchinTehai.SelectedValue.ToString()) = False Then
                Return True
            End If

            Select Case .cmbTariffKbn.SelectedValue.ToString()

                Case String.Empty, LMHControlC.TARIFF_ROSEN
                Case Else

                    '空値,路線以外、タリフ必須(ワーニング)
                    If String.IsNullOrEmpty(.txtTariffCd.TextValue) = True Then
                        If MyBase.ShowMessage("W139", New String() {String.Concat(.lblTitleTariff.Text, "コード")}) <> MsgBoxResult.Ok Then
                            Me._Vcon.SetErrorControl(.txtTariffCd, .tabMiddle, .tabUnso)
                            Return False
                        End If
                    End If

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 編集時のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditChk() As Boolean
        Return Me.IsStatusChk()
    End Function

    ''' <summary>
    ''' 中情報のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMDataChk(ByVal ds As DataSet, ByVal actionType As LMH020C.ActionType, ByVal arr As ArrayList) As Boolean

        '行選択していない場合、スルー
        Dim mNo As String = Me._Frm.lblKanriNoM.TextValue
        If String.IsNullOrEmpty(mNo) = True Then
            Return True
        End If

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsInputMChk()

        'フリー項目のチェック
        rtnResult = rtnResult AndAlso Me.IsSprChk(Me._Frm.sprFreeM)

        Dim skipIrimeFlg As Boolean = False

        If actionType = LMH020C.ActionType.DOUBLECLICK = True Then
            skipIrimeFlg = True
        End If

        '商品のマスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsGoodsExistChk(ds, Me.InkaMDataChkFlg(actionType, arr), skipIrimeFlg)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 中の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputMChk() As Boolean

        With Me._Frm

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False

            '商品コード
            .txtGoodsCd.ItemName = String.Concat(.lblTitleGoods.Text, "コード")
            .txtGoodsCd.IsForbiddenWordsCheck = chkFlg
            .txtGoodsCd.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtGoodsCd) = errorFlg Then
                .tabMiddle.SelectedTab = .tabGoods
                Return errorFlg
            End If

            'オーダー番号
            .txtOrderNoM.ItemName = .lblTitleOrderNoM.Text
            .txtOrderNoM.IsForbiddenWordsCheck = chkFlg
            .txtOrderNoM.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtOrderNoM) = errorFlg Then
                .tabMiddle.SelectedTab = .tabGoods
                Return errorFlg
            End If

            '注文番号
            .txtBuyerOrdNoM.ItemName = .lblTitleBuyerOrdNoM.Text
            .txtBuyerOrdNoM.IsForbiddenWordsCheck = chkFlg
            .txtBuyerOrdNoM.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtBuyerOrdNoM) = errorFlg Then
                .tabMiddle.SelectedTab = .tabGoods
                Return errorFlg
            End If

            'ロット番号
            .txtLot.ItemName = .lblTitleLot.Text
            .txtLot.IsForbiddenWordsCheck = chkFlg
            .txtLot.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtLot) = errorFlg Then
                .tabMiddle.SelectedTab = .tabGoods
                Return errorFlg
            End If

            '商品別コメント
            .txtGoodsComment.ItemName = .lblTitleGoodsComment.Text
            .txtGoodsComment.IsForbiddenWordsCheck = chkFlg
            .txtGoodsComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtGoodsComment) = errorFlg Then
                .tabMiddle.SelectedTab = .tabGoods
                Return errorFlg
            End If

            'シリアル
            .txtSerial.ItemName = .lblTitleSerial.Text
            .txtSerial.IsForbiddenWordsCheck = chkFlg
            .txtSerial.IsByteCheck = 40
            If MyBase.IsValidateCheck(.txtSerial) = errorFlg Then
                .tabMiddle.SelectedTab = .tabGoods
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 行復活時のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRevivalChk(ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        '削除区分チェック
        Dim rtnResult As Boolean = Me.IsDelKbnChk(ds, arr)

        '行削除未チェック
        rtnResult = rtnResult AndAlso Me.IsDelMisumiChk(ds, arr, False, "通常レコード")

        Return rtnResult

    End Function

    ''' <summary>
    ''' 行削除時のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDelChk(ByVal ds As DataSet, ByVal arr As ArrayList, ByVal arrNot As ArrayList) As Boolean

        '削除区分チェック
        Dim rtnResult As Boolean = Me.IsDelKbnChk(ds, arr)

        '行削除済チェック
        rtnResult = rtnResult AndAlso Me.IsDelMisumiChk(ds, arr, True, "削除済レコード")

        '正常データ件数チェック
        rtnResult = rtnResult AndAlso Me.IsSeijoDataCountChk(ds, arr, arrNot)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 行削除未済チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <param name="chkFlg">チェックフラグ　True：削除済はエラー　False：削除未はエラー</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDelMisumiChk(ByVal ds As DataSet, ByVal arr As ArrayList, ByVal chkFlg As Boolean, ByVal msg As String) As Boolean

        Dim dt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
        Dim max As Integer = arr.Count - 1
        Dim recNo As Integer = 0
        For i As Integer = 0 To max

            'レコード番号を取得
            recNo = Convert.ToInt32(Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMH020G.sprGoodsDef.RECNO.ColNo)))

            If LMConst.FLG.ON.Equals(dt.Rows(recNo).Item("JYOTAI").ToString()) = chkFlg Then
                Return Me._Vcon.SetErrMessage("E286", New String() {msg})
            End If

        Next

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
        If LMH020C.DEL_KBN_NOMAL.Equals(ds.Tables(LMH020C.TABLE_NM_L).Rows(0).Item("DEL_KB").ToString()) = False Then
            Return True
        End If

        Dim dt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
        Dim max As Integer = arrNot.Count - 1
        Dim nomalCount As Integer = 0
        Dim recNo As Integer = 0
        Dim jyotai As String = String.Empty

        For i As Integer = 0 To max

            'レコード番号を取得
            recNo = Convert.ToInt32(Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arrNot(i)), LMH020G.sprGoodsDef.RECNO.ColNo)))
            '状態を取得
            jyotai = Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arrNot(i)), LMH020G.sprGoodsDef.JYOTAI_NM.ColNo))

            '正常データ件数をカウント
            If LMH020C.DEL_KBN_NOMAL.Equals(dt.Rows(recNo).Item("DEL_KB").ToString()) = True AndAlso jyotai.Equals("正常") Then
                nomalCount = nomalCount + 1
            End If

        Next

        If nomalCount = 0 Then
            Return Me._Vcon.SetErrMessage("E412")
        End If

        Return True

    End Function

    ''' <summary>
    ''' 削除区分のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDelKbnChk(ByVal ds As DataSet, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '大情報の削除区分が通常以外、スルー
            If LMH020C.DEL_KBN_NOMAL.Equals(ds.Tables(LMH020C.TABLE_NM_L).Rows(0).Item("DEL_KB").ToString()) = False Then
                Return True
            End If

            Dim dt As DataTable = ds.Tables(LMH020C.TABLE_NM_M)
            Dim max As Integer = arr.Count - 1
            Dim recNo As Integer = 0
            For i As Integer = 0 To max

                'レコード番号を取得
                recNo = Convert.ToInt32(Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMH020G.sprGoodsDef.RECNO.ColNo)))

                Select Case dt.Rows(recNo).Item("DEL_KB").ToString()
                    Case LMH020C.DEL_KBN_CANCELL, LMH020C.DEL_KBN_RESERVE

                        Return Me._Vcon.SetErrMessage("E321")

                End Select

            Next

            Return True

        End With

    End Function

    ''' <summary>
    ''' ステータスチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsStatusChk() As Boolean

        If LMH020C.STATUS_SEIJO.Equals(Me._Frm.cmbStatus.SelectedValue.ToString()) = False _
           AndAlso LMH020C.STATUS_HORYU.Equals(Me._Frm.cmbStatus.SelectedValue.ToString()) = False Then

            Return Me._Vcon.SetErrMessage("E298")
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMH020C.ActionType) As Boolean

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

                Case .txtGoodsCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtGoodsCd}
                    lblCtl = New Control() {.txtGoodsCd, .lblGoodsNm, .lblGoodsKey, .lblTitleKonsuTani, .lblTitleHasuTani, .numIrisu, .numStdIrime, .lblStdIrimeTani, .lblTitleIrimeTani, .lblSumAntTani, .lblSumCntTani, .numTare, .cmbOndo}
                    msg = New String() {String.Concat(.lblTitleGoods.Text, "コード")}

                Case .txtGoodsComment.Name

                    '注意書PopはEnterでは起動しない
                    If LMH020C.ActionType.ENTER = actionType Then
                        Return False
                    End If

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtGoodsComment}
                    msg = New String() {.lblTitleGoodsComment.Text}

                Case .txtUnsoCd.Name, .txtUnsoBrCd.Name

                    Dim unsoNm As String = .lblTitleUnso.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsoCd, .txtUnsoBrCd}
                    lblCtl = New Control() {.lblUnsoNm}
                    msg = New String() {String.Concat(unsoNm, "コード"), String.Concat(unsoNm, "支店コード")}

                Case .txtTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTariffCd}
                    lblCtl = New Control() {.lblTariffNm}
                    msg = New String() {String.Concat(.lblTitleTariff.Text, "コード")}

                Case .txtShukkaMotoCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtShukkaMotoCd}
                    lblCtl = New Control() {.lblShukkaMotoNm}
                    msg = New String() {String.Concat(.lblTitleShukkaMotoCd.Text, "コード")}

            End Select

        End With

        'フォーカス位置チェック
        Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

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

        '▼▼▼(マイナスデータ)
        'Return Me._Vcon.SetErrMessage("E117", New String() {msg, maxData})
        Return Me._Vcon.SetErrMessage("E014", New String() {msg, "0", maxData})
        '▲▲▲(マイナスデータ)

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthority(ByVal actionType As LMH020C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMH020C.ActionType.EDIT

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

            Case LMH020C.ActionType.MASTEROPEN

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

            Case LMH020C.ActionType.SAVE

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

            Case LMH020C.ActionType.CLOSE

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT
                        kengenFlg = True
                End Select

            Case LMH020C.ActionType.DEL

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

            Case LMH020C.ActionType.REVIVAL

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

            Case LMH020C.ActionType.DOUBLECLICK

                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW
                        kengenFlg = True
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
    ''' 可変セルのチェックバイトを取得
    ''' </summary>
    ''' <param name="value">列名</param>
    ''' <returns>バイト数</returns>
    ''' <remarks></remarks>
    Private Function GetByteData(ByVal value As String) As Integer

        GetByteData = 0

        '後ろ2桁で判定
        If Convert.ToInt32(value.Substring(value.Length - 2, 2)) < 21 Then
            GetByteData = 100
        Else
            GetByteData = 200
        End If

        Return GetByteData

    End Function

    ''' <summary>
    ''' 計算処理をするかどうかの判定
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True：計算をする　False：計算をしない</returns>
    ''' <remarks></remarks>
    Friend Function InkaMDataChkFlg(ByVal actionType As LMH020C.ActionType, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '選択していない場合、スルー
            Dim mNo As String = .lblKanriNoM.TextValue
            If String.IsNullOrEmpty(mNo) = True Then
                Return False
            End If

            '選択しているレコードが削除データ
            If LMConst.FLG.ON.Equals(.lblJotai.TextValue) = True Then
                Return False
            End If

            '行削除の場合
            Select Case actionType

                Case LMH020C.ActionType.DEL

                    '画面に表示されているレコードを選択している場合
                    Return Me.SelectGuiDataChk(mNo, arr)

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 画面に表示している情報を選択しているかを判定
    ''' </summary>
    ''' <param name="mNo">EDI管理番号(中)</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>False：選択　True：未選択</returns>
    ''' <remarks></remarks>
    Private Function SelectGuiDataChk(ByVal mNo As String, ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count - 1
        For index As Integer = 0 To max

            If mNo.Equals(Me._Gcon.GetCellValue(Me._Frm.sprGoodsDef.ActiveSheet.Cells(Convert.ToInt32(arr(index)), LMH020G.sprGoodsDef.EDI_CTL_NO_CHU.ColNo))) = True Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 計算処理
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function Calculation(ByVal dr As DataRow) As Boolean

        With Me._Frm

            Dim kosu As Decimal = 0
            Dim irisu As Decimal = Convert.ToDecimal(Me._Gcon.FormatNumValue(dr.Item("PKG_NB").ToString()))
            Dim konsu As Decimal = Convert.ToDecimal(Me._Gcon.FormatNumValue(dr.Item("INKA_PKG_NB").ToString()))
            Dim hasu As Decimal = Convert.ToDecimal(Me._Gcon.FormatNumValue(dr.Item("HASU").ToString()))


            '端数 , 入数の関連チェック
            If Me.IsHasuIrisuChk(irisu, konsu, hasu) = False Then
                Return False
            End If

            If 1 < irisu Then

                '入数が1より大きい場合、梱数 * 入数 + 端数
                kosu = konsu * irisu + hasu

            Else

                '入数が1以下の場合、[画面] 個数
                kosu = Convert.ToDecimal(Me._Gcon.FormatNumValue(dr.Item("NB").ToString()))

            End If

            '▼▼▼(マイナスデータ)
            '範囲チェック(端数)
            If Me.IsCalcOver(hasu.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_10, .lblTitleHasu.Text) = False Then

                Call Me._Vcon.SetErrorControl(.numHasu)

                Return False
            End If
            '▲▲▲(マイナスデータ)

            '範囲チェック(個数)
            If Me.IsCalcOver(kosu.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_10, .lblTitleSumCnt.Text) = False Then
                '▼▼▼(マイナスデータ)
                'dr.Item("NB") = LMHControlC.MAX_10
                '▲▲▲(マイナスデータ)
                If 1 < irisu Then
                    Call Me._Vcon.SetErrorControl(.numKosu)
                Else
                    Call Me._Vcon.SetErrorControl(.numHasu)
                End If

                Return False
            End If

            '正常の場合、値設定
            dr.Item("NB") = kosu

            '個数 * 入目
            Dim suryo As Decimal = kosu * Convert.ToDecimal(Me._Gcon.FormatNumValue(dr.Item("IRIME").ToString()))

            '上限チェック(数量)
            If Me.IsCalcOver(suryo.ToString(), LMHControlC.MIN_0, LMHControlC.MAX_9_3, .lblTitleSumAnt.Text) = False Then
                '▼▼▼(マイナスデータ)
                'dr.Item("SURYO") = LMHControlC.MAX_9_3
                '▲▲▲(マイナスデータ)

                If 1 < irisu Then
                    Call Me._Vcon.SetErrorControl(.numKosu)
                Else
                    Call Me._Vcon.SetErrorControl(.numHasu)
                End If

                Return False
            End If

            '正常の場合、値設定
            dr.Item("SURYO") = suryo

            Return True

        End With

    End Function

    ''' <summary>
    ''' 商品Mの値を中情報に設定
    ''' </summary>
    ''' <param name="inkaMDr">DataRow</param>
    ''' <param name="goodsDr">DataRow</param>
    ''' <remarks></remarks>
    Friend Sub SetGoodsMstData(ByVal inkaMDr As DataRow, ByVal goodsDr As DataRow, ByVal setFlg As Boolean, Optional ByVal skipIrimeFlg As Boolean = False)

        With Me._Frm

            Dim goodsCd As String = goodsDr.Item("GOODS_CD_CUST").ToString()
            Dim goodsNm As String = goodsDr.Item("GOODS_NM_1").ToString()
            Dim goodsKey As String = goodsDr.Item("GOODS_CD_NRS").ToString()
            Dim nbUtNm As String = goodsDr.Item("NB_UT").ToString()
            Dim pkgUtNm As String = goodsDr.Item("PKG_UT").ToString()
            Dim pkgNb As String = goodsDr.Item("PKG_NB").ToString()
            Dim stdIrimeNb As String = goodsDr.Item("STD_IRIME_NB").ToString()
            Dim stdIrimeUtNm As String = goodsDr.Item("STD_IRIME_UT").ToString()
            Dim wt As String = goodsDr.Item("STD_WT_KGS").ToString()
            Dim ondo As String = goodsDr.Item("ONDO_KB").ToString()

            If setFlg = True Then

                .txtGoodsCd.TextValue = goodsCd
                .lblGoodsNm.TextValue = goodsNm
                .lblGoodsKey.TextValue = goodsKey
                .lblTitleKonsuTani.TextValue = nbUtNm
                .lblTitleHasuTani.TextValue = pkgUtNm
                .numIrisu.Value = pkgNb
                .numStdIrime.Value = stdIrimeNb
                .lblStdIrimeTani.TextValue = stdIrimeUtNm
                .lblTitleIrimeTani.TextValue = stdIrimeUtNm
                .lblSumAntTani.TextValue = stdIrimeUtNm
                .lblSumCntTani.TextValue = pkgUtNm
                .numTare.Value = wt
                .cmbOndo.SelectedValue = ondo

                '入目設定無しフラグがTRUEの場合は入目はマスタ値を設定しない（保存時は入目はマスタ反映しない）
                If skipIrimeFlg = False Then
                    .numIrime.Value = stdIrimeNb
                End If

                '入数が１以下の場合、梱数を0にする、端数に個数を入れる。
                If Convert.ToInt32(pkgNb) <= 1 Then
                    .numHasu.TextValue = .numSumCnt.TextValue
                    .numKosu.TextValue = "0"
                End If

            End If

            inkaMDr.Item("CUST_GOODS_CD") = goodsCd
            inkaMDr.Item("GOODS_NM") = goodsNm
            inkaMDr.Item("NRS_GOODS_CD") = goodsKey
            inkaMDr.Item("PKG_NB") = pkgNb
            inkaMDr.Item("STD_IRIME") = stdIrimeNb
            inkaMDr.Item("NB_UT") = goodsDr.Item("NB_UT").ToString()
            inkaMDr.Item("NB_UT_NM") = nbUtNm
            inkaMDr.Item("PKG_UT") = goodsDr.Item("PKG_UT").ToString()
            inkaMDr.Item("PKG_UT_NM") = pkgUtNm
            Dim irimeUt As String = goodsDr.Item("STD_IRIME_UT").ToString()
            inkaMDr.Item("IRIME_UT") = irimeUt
            inkaMDr.Item("IRIME_UT_NM") = stdIrimeUtNm
            inkaMDr.Item("STD_IRIME_UT") = irimeUt
            inkaMDr.Item("STD_IRIME_UT_NM") = stdIrimeUtNm
            inkaMDr.Item("BETU_WT") = wt
            inkaMDr.Item("ONDO_KB") = ondo

            '入数が１以下の場合、梱数を0にする
            If Convert.ToInt32(pkgNb) <= 1 Then
                inkaMDr.Item("INKA_PKG_NB") = "0"
            End If

        End With

    End Sub

    ''' <summary>
    ''' 商品Mの値をクリア
    ''' </summary>
    ''' <param name="inkaMDr">DataRow</param>
    ''' <remarks></remarks>
    Private Sub SetGoodsMstClearData(ByVal inkaMDr As DataRow)

        With inkaMDr

            .Item("CUST_GOODS_CD") = String.Empty
            .Item("GOODS_NM") = String.Empty
            .Item("NRS_GOODS_CD") = String.Empty
            .Item("PKG_NB") = 0
            .Item("STD_IRIME") = 0
            .Item("NB_UT") = String.Empty
            .Item("NB_UT_NM") = String.Empty
            .Item("PKG_UT") = String.Empty
            .Item("PKG_UT_NM") = String.Empty
            .Item("STD_IRIME_UT") = String.Empty
            .Item("STD_IRIME_UT_NM") = String.Empty
            .Item("BETU_WT") = 0
            .Item("ONDO_KB") = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'フリー項目(大)のスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprFreeL)

        'フリー項目(中)のスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprFreeM)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtBuyerOrdNo.TextValue = .txtBuyerOrdNo.TextValue.Trim()
            .txtOrderNo.TextValue = .txtOrderNo.TextValue.Trim()
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtNyubanL.TextValue = .txtNyubanL.TextValue.Trim()
            .txtNyukaComment.TextValue = .txtNyukaComment.TextValue.Trim()
            .txtUnsoCd.TextValue = .txtUnsoCd.TextValue.Trim()
            .txtUnsoBrCd.TextValue = .txtUnsoBrCd.TextValue.Trim()
            .txtTariffCd.TextValue = .txtTariffCd.TextValue.Trim()
            .txtShukkaMotoCd.TextValue = .txtShukkaMotoCd.TextValue.Trim()
      
        End With

        '中情報のスペース除去
        Call Me.TrimMDataSpaceTextValue()

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function TrimMDataSpaceTextValue() As Boolean

        With Me._Frm

            .txtGoodsCd.TextValue = .txtGoodsCd.TextValue.Trim()
            .txtLot.TextValue = .txtLot.TextValue.Trim()
            .txtOrderNoM.TextValue = .txtOrderNoM.TextValue.Trim()
            .txtBuyerOrdNoM.TextValue = .txtBuyerOrdNoM.TextValue.Trim()
            .txtSerial.TextValue = .txtSerial.TextValue.Trim()
            .txtGoodsComment.TextValue = .txtGoodsComment.TextValue.Trim()

        End With

        Return True

    End Function

#End Region 'Method

End Class
