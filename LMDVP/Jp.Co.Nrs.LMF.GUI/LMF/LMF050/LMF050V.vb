' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF050V : 運賃検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL

''' <summary>
''' LMF050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF050F

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF050G

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMFControlV

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF050F, ByVal v As LMFControlV, ByVal g As LMFControlG, ByVal _lmfg As LMF050G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

        Me._Gcon = g

        Me._G = _lmfg

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 保存時の入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheckSave() As Boolean

        '単項目チェック(保存)
        If Me.IsSaveSingleCheck() = False Then
            Return False
        End If
        Return True

    End Function

    ''' <summary>
    ''' 再計算時の入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheckSum(ByVal ds As DataSet) As Boolean

        '単項目チェック(再計算)
        If Me.IsSumSingleCheck() = False Then
            Return False
        End If


        '関連チェック(再計算)
        If Me.IsUnchinSaveCheck(ds) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 確定の入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheckKakutei(ByVal ds As DataSet, ByVal frm As LMF050F) As Boolean

        '関連チェック(確定)
        If Me.IsKakuteiSaveCheck(ds, frm) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 単項目チェック(保存)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck() As Boolean

        With Me._Frm
            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            '運賃備考
            vCell.SetValidateCell(0, LMF050G.sprDetailDef.REMARK.ColNo)
            vCell.ItemName = "運賃備考"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function
    ''' <summary>
    ''' 単項目チェック(再計算)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSumSingleCheck() As Boolean

        With Me._Frm
            '******************** ヘッダ項目の入力チェック ********************

            '運賃タリフコード
            .txtTariffCd.ItemName = "運賃タリフコード"
            .txtTariffCd.IsHissuCheck = True
            .txtTariffCd.IsForbiddenWordsCheck = True
            .txtTariffCd.IsMiddleSpace = True
            .txtTariffCd.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtTariffCd) = False Then
                Return False
            End If

            '割増運賃タリフコード
            .txtWarimashi.ItemName = "割増運賃タリフコード"
            .txtWarimashi.IsForbiddenWordsCheck = True
            .txtWarimashi.IsMiddleSpace = True
            .txtWarimashi.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtWarimashi) = False Then
                Return False
            End If


        End With

        Return True

    End Function

#Region "関連チェック"

    ''' <summary>
    ''' 再計算時の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinSaveCheck(ByVal ds As DataSet) As Boolean

        Dim drs As DataRow() = Nothing

        With Me._Frm

            Dim tariffKb As String = .cmbTariffKbn.SelectedValue.ToString
            Dim tariffcd As String = .txtTariffCd.TextValue
            Dim unsodate As String = String.Empty
            Dim tariffCdEda As String = .txtWarimashi.TextValue.ToString
            Dim tariffbun As String = .cmbTariffKbn.TextValue
            Dim nrsbrCd As String = .cmbEigyo.SelectedValue.ToString
            Dim shatdate As String = String.Empty


            '運賃締め基準の取得
            Dim dt As DataTable = ds.Tables(LMF050C.TABLE_NM_OUT)
            Dim dr As DataRow = dt.Rows(0)
            Dim sime As String = dr.Item("UNTIN_CALCULATION_KB").ToString

            '運賃締め基準が01の場合出荷日を設定
            If LMFControlC.CALC_SHUKKA.Equals(sime) = True Then


                unsodate = .imdShukka.TextValue

            Else
                '01以外のときは納入日を設定
                unsodate = .imdArr.TextValue

            End If

            'タリフ分類<>横持ちの場合
            If LMFControlC.TARIFF_YOKO <> (tariffKb) = True Then


                'タリフコード、運送日が入力されているとき
                If String.IsNullOrEmpty(tariffcd) = False AndAlso _
                String.IsNullOrEmpty(unsodate) = False Then


                    '存在チェック(タリフマスタ)
                    If Me._Vcon.SelectUnchinTariffListDataRow(drs, tariffcd, , unsodate) = False Then

                        Call Me._Vcon.SetErrorControl(.txtTariffCd)
                        Return False

                    End If

                    '名称の設定
                    .lblTariffNm.TextValue = drs(0).Item("UNCHIN_TARIFF_REM").ToString()

                End If
            End If

            'タリフ分類=横持ちの場合
            If LMFControlC.TARIFF_YOKO.Equals(tariffKb) = True Then

                '営業所コード、タリフコードが入力されているとき
                If String.IsNullOrEmpty(nrsbrCd) = False AndAlso _
                String.IsNullOrEmpty(tariffcd) = False Then

                    '存在チェック(横持ちタリフマスタ)
                    If Me._Vcon.SelectYokoTariffListDataRow(drs, nrsbrCd, tariffcd) = False Then

                        Call Me._Vcon.SetErrorControl(.txtTariffCd)

                        Return False

                    Else
                        '存在チェックがで取得できた場合
                        shatdate = drs(0).Item("CALC_KB").ToString

                        '名称の設定
                        .lblTariffNm.TextValue = drs(0).Item("YOKO_REM").ToString()

                    End If
                End If
            End If

            Dim warimashiCd As String = .txtWarimashi.TextValue.ToString
            
            '営業所、割増タリフコードが入力されているとき
            If String.IsNullOrEmpty(nrsbrCd) = False AndAlso _
            String.IsNullOrEmpty(warimashiCd) = False Then

                '存在チェック(割増タリフマスタ
                If Me._Vcon.SelectExtcUnchinListDataRow(drs, nrsbrCd, warimashiCd) = False Then
                    Call Me._Vcon.SetErrorControl(.txtWarimashi)
                    Return False
                End If

                '名称を設定
                .lblWarimashi.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

            End If

            Dim shashu As String = .cmbShashu.SelectedValue.ToString()
            '整合性チェック
            'タリフ分類区分が40かつ車建てが02の場合
            If LMFControlC.TARIFF_YOKO.Equals(tariffKb) = True AndAlso _
            LMFControlC.CALC_KBN_CAR.Equals(shatdate) = True Then

                '車種に値がない場合
                If String.IsNullOrEmpty(shashu) = True Then
                    .cmbShashu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtTariffCd)

                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E187", New String() {"車建て", "車輌区分"})
                    MyBase.ShowMessage("E835")
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If

            End If

            'タリフ分類区分が40かつ車建てが02の場合
            If LMFControlC.TARIFF_YOKO.Equals(tariffKb) = True AndAlso _
            LMFControlC.CALC_KBN_CAR.Equals(shatdate) = False Then

                '車種に値がある場合
                If String.IsNullOrEmpty(shashu) = False Then
                    .cmbShashu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtTariffCd)
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E211", New String() {"車建て以外", "車輌区分"})
                    MyBase.ShowMessage("E836")
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 再計算時の整合性チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function Seigou(ByVal frm As LMF050F, ByVal ds As DataSet) As Boolean

        Dim tariffbunruiM As String = String.Empty
        Dim tariffbunruiH As String = String.Empty
        Dim i As Integer = frm.sprDetail.ActiveSheet.RowCount - 1

        tariffbunruiH = frm.cmbTariffKbn.SelectedValue.ToString

        tariffbunruiM = Me._Gcon.GetCellValue(frm.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TARIF_BUN.ColNo)).ToString()

        If LMFControlC.TARIFF_YOKO <> (tariffbunruiM) = True AndAlso _
           LMFControlC.TARIFF_YOKO.Equals(tariffbunruiH) = True Then
            Me._Vcon.SetErrorControl(Me._Frm.cmbTariffKbn)
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E286", New String() {"横持ち"})
            MyBase.ShowMessage("E859")
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        If LMFControlC.TARIFF_YOKO.Equals(tariffbunruiM) = True AndAlso _
          LMFControlC.TARIFF_YOKO <> (tariffbunruiH) = True Then
            Me._Vcon.SetErrorControl(Me._Frm.cmbTariffKbn)
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E286", New String() {"横持ち以外"})
            MyBase.ShowMessage("E860")
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If


        Return True


    End Function

    ''' <summary>
    ''' 確定の関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKakuteiSaveCheck(ByVal ds As DataSet, ByVal frm As LMF050F) As Boolean


        With Me._Frm

            '関連チェック(運賃が0円の場合)
            Dim unchinzero As Decimal = 0
            Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail)
            Dim unchin As Decimal = 0
            Dim dt As DataTable = ds.Tables(LMF050C.TABLE_NM_OUT)
            Dim max As Integer = dt.Rows.Count - 1

            Dim dr As DataRow = dt.Rows(0)


            '親レコード判定
            Dim siqtol As String = String.Empty
            Dim siqtom As String = String.Empty
            Dim unsol As String = String.Empty
            Dim unsom As String = String.Empty

            Dim maxs As Integer = .sprDetail.ActiveSheet.RowCount - 1

            'START YANAI 要望番号974 運賃が0円で、他の割増運賃関係が0円以外の場合エラーとする
            Dim deciUnchin As Decimal = 0
            Dim decicityExtc As Decimal = 0
            Dim deciwintExtc As Decimal = 0
            Dim decirelyExtc As Decimal = 0
            Dim decitoll As Decimal = 0
            Dim deciinsu As Decimal = 0

            For i As Integer = 0 To maxs
                deciUnchin = Convert.ToDecimal(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.UNCHIN.ColNo)).ToString())
                decicityExtc = Convert.ToDecimal(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TOSHI.ColNo)).ToString())
                deciwintExtc = Convert.ToDecimal(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TOKI.ColNo)).ToString())
                decirelyExtc = Convert.ToDecimal(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TYUKEI.ColNo)).ToString())
                decitoll = Convert.ToDecimal(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.KOSO.ColNo)).ToString())
                deciinsu = Convert.ToDecimal(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.SONOTA.ColNo)).ToString())

                '確定運賃が0円で、割り増しのいずれかが一つでも0円以外がある場合はエラー
                If deciUnchin = 0 AndAlso _
                    (decicityExtc <> 0 OrElse _
                     deciwintExtc <> 0 OrElse _
                     decirelyExtc <> 0 OrElse _
                     decitoll <> 0 OrElse _
                     deciinsu <> 0) Then
                    MyBase.ShowMessage("E260", New String() {"運賃が０円、割増運賃が０円でないレコード"})
                    Return False
                End If
            Next
            '1641
            'Dim seiqUnchin As Decimal = 0
            'Dim seiqcityExtc As Decimal = 0
            'Dim seiqwintExtc As Decimal = 0
            'Dim seiqrelyExtc As Decimal = 0
            'Dim seiqtoll As Decimal = 0
            'Dim seiqinsu As Decimal = 0

            'For i As Integer = 0 To max
            '    seiqUnchin = Convert.ToDecimal(dt.Rows(i).Item("SEIQ_UNCHIN").ToString)
            '    seiqcityExtc = Convert.ToDecimal(dt.Rows(i).Item("SEIQ_CITY_EXTC").ToString)
            '    seiqwintExtc = Convert.ToDecimal(dt.Rows(i).Item("SEIQ_WINT_EXTC").ToString)
            '    seiqrelyExtc = Convert.ToDecimal(dt.Rows(i).Item("SEIQ_RELY_EXTC").ToString)
            '    seiqtoll = Convert.ToDecimal(dt.Rows(i).Item("SEIQ_TOLL").ToString)
            '    seiqinsu = Convert.ToDecimal(dt.Rows(i).Item("SEIQ_INSU").ToString)

            '    '請求運賃が0円で、割り増しのいずれかが一つでも0円以外がある場合はエラー
            '    If seiqUnchin = 0 AndAlso _
            '        (seiqcityExtc <> 0 OrElse _
            '         seiqwintExtc <> 0 OrElse _
            '         seiqrelyExtc <> 0 OrElse _
            '         seiqtoll <> 0 OrElse _
            '         seiqinsu <> 0) Then
            '        MyBase.ShowMessage("E260", New String() {"運賃が０円、割増運賃が０円でないレコード"})
            '        Return False
            '    End If
            'Next
            ''END YANAI 要望番号974 運賃が0円で、他の割増運賃関係が0円以外の場合エラーとする

            For i As Integer = 0 To maxs

                '請求グループ番号をdrから取得
                siqtol = Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.GROUP.ColNo)).ToString()

                '請求グループ番号Mをdrから取得
                siqtom = Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.GROUP_UNSO.ColNo)).ToString()

                '運送番号Lをdrから取得
                unsol = Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.UNSO_NO.ColNo)).ToString()

                '運送番号Mをdrから取得
                unsom = Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.UNSO_NO_EDA.ColNo)).ToString()

                If String.IsNullOrEmpty(siqtol) = False AndAlso _
                String.IsNullOrEmpty(siqtom) = False Then

                    If unsol.Equals(siqtol) = True AndAlso _
                    unsom.Equals(siqtom) = True Then

                        unchin = Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.UNCHIN.ColNo)).ToString())) _
                         + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TOSHI.ColNo)).ToString())) _
                         + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TOKI.ColNo)).ToString())) _
                         + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TYUKEI.ColNo)).ToString())) _
                         + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.KOSO.ColNo)).ToString())) _
                         + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.SONOTA.ColNo)).ToString()))

                        '運賃が0の場合
                        If unchin = unchinzero Then

                            '2016.01.06 UMANO 英語化対応START
                            'If MyBase.ShowMessage("W144", New String() {"運賃"}) = MsgBoxResult.Ok Then
                            If MyBase.ShowMessage("W144", New String() {LMF050G.sprDetailDef.UNCHIN.ColName}) = MsgBoxResult.Ok Then
                                '2016.01.06 UMANO 英語化対応END
                                Return True
                            Else
                                Return False
                            End If

                        End If

                    End If

                Else

                    unchin = Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.UNCHIN.ColNo)).ToString())) _
                          + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TOSHI.ColNo)).ToString())) _
                          + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TOKI.ColNo)).ToString())) _
                          + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.TYUKEI.ColNo)).ToString())) _
                          + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.KOSO.ColNo)).ToString())) _
                          + Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.SONOTA.ColNo)).ToString()))

                    '運賃が0の場合
                    If unchin = unchinzero Then

                        '2016.01.06 UMANO 英語化対応START
                        'If MyBase.ShowMessage("W144", New String() {"運賃"}) = MsgBoxResult.Ok Then
                        If MyBase.ShowMessage("W144", New String() {LMF050G.sprDetailDef.UNCHIN.ColName}) = MsgBoxResult.Ok Then
                            '2016.01.06 UMANO 英語化対応END
                            Return True
                        Else
                            Return False
                        End If

                    End If

                End If

            Next

            '出荷日、納入日がブランクまたは０の時
            Dim outDate As String = String.Empty
            Dim arrDate As String = String.Empty
            Dim zero As String = "0"

            '出荷日、納入日をdrから取得
            outDate = dr.Item("OUTKA_PLAN_DATE").ToString
            arrDate = dr.Item("ARR_PLAN_DATE").ToString

            '出荷日がブランク、または「0」である場合はエラー

            If String.IsNullOrEmpty(outDate) = True OrElse _
                 zero.Equals(outDate) = True Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E292", New String() {"出荷日"})
                MyBase.ShowMessage("E292", New String() {.lblTitleShukka.Text()})
                '2016.01.06 UMANO 英語化対応END
                Return False

            End If


            '納入日がブランク、または「0」である場合はエラー
            If String.IsNullOrEmpty(arrDate) = True OrElse _
            zero.Equals(arrDate) = True Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E292", New String() {"納品日"})
                MyBase.ShowMessage("E292", New String() {.lblTitleArr.Text()})
                '2016.01.06 UMANO 英語化対応END
                Return False

            End If

            'START YANAI 要望番号446
            Dim seiqtoCd As String = String.Empty
            For i As Integer = 0 To maxs
                '請求グループ番号Mをdrから取得
                seiqtoCd = Me._Gcon.GetCellValue(.sprDetail.ActiveSheet.Cells(i, LMF050G.sprDetailDef.SEIQTO_CD.ColNo)).ToString()
                If String.IsNullOrEmpty(seiqtoCd) = True Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E425", New String() {"請求先コード", "運賃検索"})
                    MyBase.ShowMessage("E425")
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            Next
            'END YANAI 要望番号446

        End With

        Return True

    End Function


#Region "確定フラグチェック"

    ''' <summary>
    ''' 確定済フラグチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function kakutei(ByVal ds As DataSet, ByVal frm As LMF050F) As Boolean

        Dim dr As DataRow = Nothing
        Dim kakuteiFlg As String = String.Empty
        Dim flgOn As String = "01"
        Dim dt As DataTable = ds.Tables(LMF050C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)

            '確定フラグをdrから取得
            kakuteiFlg = dr.Item("SEIQ_FIXED_FLAG").ToString

            '確定フラグが「01」の場合エラーメッセージ
            If flgOn.Equals(kakuteiFlg) = True Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E237", New String() {"確定済"})
                MyBase.ShowMessage("E237", New String() {frm.FunctionKey.F5ButtonName})
                '2016.01.06 UMANO 英語化対応END

                Return False

            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 未確定フラグチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function mikakutei(ByVal ds As DataSet, ByVal frm As LMF050F) As Boolean

        Dim dr As DataRow = Nothing
        Dim kakuteiFlg As String = String.Empty
        Dim flgOff As String = "00"
        Dim dt As DataTable = ds.Tables(LMF050C.TABLE_NM_OUT)
        Dim max As Integer = dt.Rows.Count - 1

        For i As Integer = 0 To max
            dr = dt.Rows(i)

            '確定フラグをdrから取得
            kakuteiFlg = dr.Item("SEIQ_FIXED_FLAG").ToString

            '確定フラグが「00」の場合エラーメッセージ
            If flgOff.Equals(kakuteiFlg) = True Then

                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E237", New String() {"未確定"})
                MyBase.ShowMessage("E863")
                '2016.01.06 UMANO 英語化対応END

                Return False

            End If

        Next

        Return True

    End Function

    'START YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない
    ''' <summary>
    ''' 重量マイナスチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSeiqwtMinusChk(ByVal wt As Decimal, ByVal sumWt As Decimal) As Boolean

        If wt - sumWt <= 0 Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E495", New String() {"親の請求適用重量"})
            MyBase.ShowMessage("E495")
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        Return True

    End Function
    'END YANAI 要望番号1267 運賃重量を変更しても運送重量が変わらない

#End Region

#End Region

#Region "他営業所チェック"

    ''' <summary>
    ''' 他営業所チェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUserNrsBrCdChk(ByVal frm As LMF050F, ByVal eventShubetsu As LMF050C.EventShubetsu) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''ユーザーのログイン営業所と異なる場合エラー
        'If frm.cmbEigyo.SelectedValue.Equals(LMUserInfoManager.GetNrsBrCd) = False Then
        '    Dim msg As String = String.Empty

        '    Select Case eventShubetsu

        '        Case LMF050C.EventShubetsu.HENSHU
        '            msg = "編集"

        '        Case LMF050C.EventShubetsu.KAKUTEI
        '            msg = "確定"

        '        Case LMF050C.EventShubetsu.KAKUTEIKAIJO
        '            msg = "確定解除"

        '    End Select

        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False

        'End If

        Return True

    End Function

#End Region

#Region "権限チェック"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMF050C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMF050C.EventShubetsu.HENSHU          '編集
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

            Case LMF050C.EventShubetsu.MASTEROPEN          'マスタ参照
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

            Case LMF050C.EventShubetsu.SAVE           '保存
                '10閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMF050C.EventShubetsu.UNCHINGET           '再計算
                '10閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMF050C.EventShubetsu.KAKUTEI          '確定
                '10閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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


            Case LMF050C.EventShubetsu.KAKUTEIKAIJO          '確定解除
                '10閲覧者、20:入力者（一般）、25:入力者（上級）、50:外部の場合エラー
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

            Case LMF050C.EventShubetsu.TOJIRU           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF050C.EventShubetsu) As Boolean

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


                Case .txtTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTariffCd}
                    lblCtl = New Control() {.lblTariffNm}
                    msg = New String() {String.Concat(.lblTitleTariff.Text, LMFControlC.CD)}

                Case .txtWarimashi.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtWarimashi}
                    lblCtl = New Control() {.lblWarimashi}
                    msg = New String() {String.Concat(.lblTitleWarimashi.Text, LMFControlC.CD)}

            End Select

            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

#End Region

#End Region 'Method

End Class
