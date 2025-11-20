' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF     : 運送
'  プログラムID     :  LMF020V : 運送入力
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMF020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMF020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMF020F

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

    ''' <summary>
    ''' LMF020Gクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _G As LMF020G

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMF020F, ByVal v As LMFControlV, ByVal g As LMF020G, ByVal ctlG As LMFControlG)

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

    ''' <summary>
    ''' 入力チェックメソッドの雛形です。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInputCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        Dim rtnResult As Boolean = Me.IsHeaderChk()
        rtnResult = rtnResult AndAlso Me.IsSprChk()

        'マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsMstExistChk()

        '関連チェック
        rtnResult = rtnResult AndAlso Me.IsInputConnectionChk()
        rtnResult = rtnResult AndAlso Me.IsDateChk()
        rtnResult = rtnResult AndAlso Me.IsSprConnectionChk()

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

            '元着払区分
            .cmbPcKbn.ItemName = .lblTitlePcKbn.Text
            .cmbPcKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbPcKbn) = errorFlg Then
                Return errorFlg
            End If

            '課税区分
            .cmbTax.ItemName = .lblTitleTax.Text
            .cmbTax.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbTax) = errorFlg Then
                Return errorFlg
            End If

            'START YANAI 要望番号1260 必須項目見直し
            ''便区分
            '.cmbBinKbn.ItemName = .lblTitleBinKbn.Text
            '.cmbBinKbn.IsHissuCheck = chkFlg
            'If MyBase.IsValidateCheck(.cmbBinKbn) = errorFlg Then
            '    Return errorFlg
            'End If
            'END YANAI 要望番号1260 必須項目見直し

            'タリフ分類区分
            .cmbTariffKbn.ItemName = .lblTitleTariff.Text
            .cmbTariffKbn.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbTariffKbn) = errorFlg Then
                Return errorFlg
            End If

            '車扱いの場合
            If LMFControlC.TARIFF_KURUMA.Equals(.cmbTariffKbn.SelectedValue.ToString()) = True Then

                '車輌区分
                .cmbSharyoKbn.ItemName = .lblTitleSharyoKbn.Text
                .cmbSharyoKbn.IsHissuCheck = chkFlg
                If MyBase.IsValidateCheck(.cmbSharyoKbn) = errorFlg Then
                    Return errorFlg
                End If

            End If

            '課税区分
            .cmbTax.ItemName = .lblTitleTax.Text
            .cmbTax.IsHissuCheck = chkFlg
            If MyBase.IsValidateCheck(.cmbTax) = errorFlg Then
                Return errorFlg
            End If

            '運送会社コード
            .txtUnsocoCd.ItemName = String.Concat(.lblTitleUnsoco.Text, LMFControlC.CD)
            .txtUnsocoCd.IsHissuCheck = chkFlg
            .txtUnsocoCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsocoCd.IsByteCheck = 5
            .txtUnsocoCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsocoCd) = errorFlg Then
                Return errorFlg
            End If

            '運送会社支店コード
            .txtUnsocoBrCd.ItemName = String.Concat(.lblTitleUnsoco.Text, LMFControlC.BR_CD)
            .txtUnsocoBrCd.IsHissuCheck = chkFlg
            .txtUnsocoBrCd.IsForbiddenWordsCheck = chkFlg
            .txtUnsocoBrCd.IsByteCheck = 3
            .txtUnsocoBrCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtUnsocoBrCd) = errorFlg Then
                Return errorFlg
            End If

            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
            '荷主中コード
            .txtCustCdM.ItemName = "荷主(中)コード"
            .txtCustCdM.IsHissuCheck = chkFlg
            .txtCustCdM.IsForbiddenWordsCheck = chkFlg
            .txtCustCdM.IsByteCheck = 2
            .txtCustCdM.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtCustCdM) = errorFlg Then
                Return errorFlg
            End If
            '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

            '送り状番号
            .txtOkuriNo.ItemName = .lblTitleOkuriNo.Text
            .txtOkuriNo.IsForbiddenWordsCheck = chkFlg
            .txtOkuriNo.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtOkuriNo) = errorFlg Then
                Return errorFlg
            End If

            'オーダー番号
            .txtOrdNo.ItemName = .lblTitletxtOrdNo.Text
            .txtOrdNo.IsForbiddenWordsCheck = chkFlg
            .txtOrdNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtOrdNo) = errorFlg Then
                Return errorFlg
            End If

            '荷送人コード
            .txtShipCd.ItemName = String.Concat(.lblTitleShip.Text, LMFControlC.L_NM, LMFControlC.CD)
            .txtShipCd.IsForbiddenWordsCheck = chkFlg
            .txtShipCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtShipCd) = errorFlg Then
                Return errorFlg
            End If

            '注意番号
            .txtBuyerOrdNo.ItemName = .lblTitleBuyerOrdNo.Text
            .txtBuyerOrdNo.IsForbiddenWordsCheck = chkFlg
            .txtBuyerOrdNo.IsByteCheck = 30
            If MyBase.IsValidateCheck(.txtBuyerOrdNo) = errorFlg Then
                Return errorFlg
            End If

            'タリフコード
            .txtTariffCd.ItemName = String.Concat(.lblTitleTariff.Text, LMFControlC.CD)
            .txtTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtTariffCd.IsByteCheck = 10
            .txtTariffCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtTariffCd) = errorFlg Then
                Return errorFlg
            End If

            '割増タリフコード
            .txtExtcTariffCd.ItemName = String.Concat(.lblTitleExtcTariff.Text, LMFControlC.CD)
            .txtExtcTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtExtcTariffCd.IsByteCheck = 10
            .txtExtcTariffCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtExtcTariffCd) = errorFlg Then
                Return errorFlg
            End If

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            '支払タリフコード
            .txtPayTariffCd.ItemName = String.Concat(.lblTitlePayTariff.Text, LMFControlC.CD)
            .txtPayTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtPayTariffCd.IsByteCheck = 10
            .txtPayTariffCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtPayTariffCd) = errorFlg Then
                Return errorFlg
            End If

            '支払割増タリフコード
            .txtPayExtcTariffCd.ItemName = String.Concat(.lblTitlePayExtcTariff.Text, LMFControlC.CD)
            .txtPayExtcTariffCd.IsForbiddenWordsCheck = chkFlg
            .txtPayExtcTariffCd.IsByteCheck = 10
            .txtPayExtcTariffCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtPayExtcTariffCd) = errorFlg Then
                Return errorFlg
            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            '213.03.28 / 入荷着払い手入力対応 NOTES689 開始

            '2031.03.28 / 入荷着払い手入力対応 NOTES689 終了

            '積込予定日
            If Me._Vcon.IsInputDateFullByteChk(.imdOrigDate, .lblTitleOrigDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '積込予定時刻
            .txtOrigTime.ItemName = .lblTitleOrigTime.Text
            .txtOrigTime.IsForbiddenWordsCheck = chkFlg
            .txtOrigTime.IsByteCheck = 6
            If MyBase.IsValidateCheck(.txtOrigTime) = errorFlg Then
                Return errorFlg
            End If

            '積込コード
            Dim motoUnso As Boolean = LMFControlC.MOTO_DATA_UNSO.Equals(.cmbMotoDataKbn.SelectedValue.ToString())
            .txtOrigCd.ItemName = String.Concat(.lblTitleOrig.Text, LMFControlC.CD)
            .txtOrigCd.IsHissuCheck = motoUnso
            .txtOrigCd.IsForbiddenWordsCheck = chkFlg
            .txtOrigCd.IsByteCheck = 15
           If MyBase.IsValidateCheck(.txtOrigCd) = errorFlg Then
                Return errorFlg
            End If

            '荷降予定日
            If Me._Vcon.IsInputDateFullByteChk(.imdDestDate, .lblTitleDestDate.Text) = errorFlg Then
                Return errorFlg
            End If

            '荷降予定時刻
            .txtDestTime.ItemName = .lblTitleDestTime.Text
            .txtDestTime.IsForbiddenWordsCheck = chkFlg
            .txtDestTime.IsByteCheck = 20              'UPD 2018/06/28 6→20 
            If MyBase.IsValidateCheck(.txtDestTime) = errorFlg Then
                Return errorFlg
            End If

            '荷降予定時刻
            .txtJiDestTime.ItemName = .lblTitleJiDestTime.Text
            .txtJiDestTime.IsForbiddenWordsCheck = chkFlg
            .txtJiDestTime.IsByteCheck = 6
            If MyBase.IsValidateCheck(.txtJiDestTime) = errorFlg Then
                Return errorFlg
            End If

            '荷降コード
            .txtDestCd.ItemName = String.Concat(.lblTitleDest.Text, LMFControlC.CD)
            .txtDestCd.IsHissuCheck = motoUnso
            .txtDestCd.IsForbiddenWordsCheck = chkFlg
            .txtDestCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtDestCd) = errorFlg Then
                Return errorFlg
            End If

            '住所3
            .txtDestAdd3.ItemName = String.Concat(.lblTitleAdd.Text, 3.ToString())
            .txtDestAdd3.IsForbiddenWordsCheck = chkFlg
            '(2012.12.11)要望番号1585 40byte→60byte -- START --
            '.txtDestAdd3.IsByteCheck = 40
            '2019/11/26 要望管理009400 rep
            '.txtDestAdd3.IsByteCheck = 60
            .txtDestAdd3.IsByteCheck = 80
            '(2012.12.11)要望番号1585 40byte→60byte --  END  --
            If MyBase.IsValidateCheck(.txtDestAdd3) = errorFlg Then
                Return errorFlg
            End If

            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            '電話番号
            .txtTel.ItemName = .lblTel.Text
            .txtTel.IsForbiddenWordsCheck = chkFlg
            .txtTel.IsByteCheck = 20
            If MyBase.IsValidateCheck(.txtTel) = errorFlg Then
                Return errorFlg
            End If
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end

            'エリアコード
            .txtAreaCd.ItemName = String.Concat(.lblTitleArea.Text, LMFControlC.CD)
            .txtAreaCd.IsForbiddenWordsCheck = chkFlg
            .txtAreaCd.IsByteCheck = 7
            .txtAreaCd.IsMiddleSpace = chkFlg
            If MyBase.IsValidateCheck(.txtAreaCd) = errorFlg Then
                Return errorFlg
            End If

            '運送コメント
            .txtUnsoComment.ItemName = .lblTitleUnsoComment.Text
            .txtUnsoComment.IsForbiddenWordsCheck = chkFlg
            .txtUnsoComment.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtUnsoComment) = errorFlg Then
                Return errorFlg
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 日付の大小チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDateChk() As Boolean

        With Me._Frm

            Return Me._Vcon.IsDateFromToChk(.imdOrigDate, .imdDestDate, .lblTitleDestDate.Text, .lblTitleOrigDate.Text)

        End With

    End Function

    ''' <summary>
    ''' 明細の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprChk() As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprDetail)

        With vCell

            Dim chkFlg As Boolean = True
            Dim errorFlg As Boolean = False
            Dim spr As Win.Spread.LMSpread = Me._Frm.sprDetail
            Dim max As Integer = spr.ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To max

#If True Then   'ADD 20187/11/28 依頼番号 : 003400   【LMS】運送情報画面_印刷順変更機能追加
                '印刷順
                .SetValidateCell(i, LMF020G.sprDetailDef.PRT_ORDER.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.PRT_ORDER.ColName)
                .IsSujiCheck = chkFlg
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

#End If

                '商品KEY
                .SetValidateCell(i, LMF020G.sprDetailDef.GOODS_CD.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.GOODS_CD.ColName)
                .IsForbiddenWordsCheck = chkFlg
                .IsHankakuCheck = chkFlg
                .IsMiddleSpace = chkFlg
                .IsByteCheck = 20
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '商品名
                .SetValidateCell(i, LMF020G.sprDetailDef.GOODS_NM.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.GOODS_NM.ColName)
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 60
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                'ロット№
                .SetValidateCell(i, LMF020G.sprDetailDef.LOT_NO.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.LOT_NO.ColName)
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 40
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '備考
                .SetValidateCell(i, LMF020G.sprDetailDef.REMARK.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.REMARK.ColName)
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '在庫部課
                .SetValidateCell(i, LMF020G.sprDetailDef.ZAI_BUKA.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.ZAI_BUKA.ColName)
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 7
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

                '扱い部課
                .SetValidateCell(i, LMF020G.sprDetailDef.HOKA_BUKA.ColNo)
                .ItemName = Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.HOKA_BUKA.ColName)
                .IsForbiddenWordsCheck = chkFlg
                .IsByteCheck = 7
                If MyBase.IsValidateCheck(vCell) = errorFlg Then
                    Return errorFlg
                End If

            Next

            '0件チェック
            Return Me.IsRowCntChk(max)

        End With

    End Function

    ''' <summary>
    ''' 明細0件チェック
    ''' </summary>
    ''' <param name="rowCnt">行数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsRowCntChk(ByVal rowCnt As Integer) As Boolean

        If rowCnt < 0 Then
            Return Me._Vcon.SetErrMessage("E231")
        End If

        Return True

    End Function

    ''' <summary>
    ''' ヘッダの関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsInputConnectionChk() As Boolean

        Return Me.IsTariffKbnChk()

    End Function

    ''' <summary>
    ''' タリフ分類のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffKbnChk() As Boolean

        With Me._Frm

            '元データ <> 運送の場合、スルー
            If LMFControlC.MOTO_DATA_UNSO.Equals(.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
                Return True
            End If

            'タリフ分類 = 入荷着払いの場合、エラー
            Dim tariff As String = .cmbTariffKbn.SelectedValue.ToString()
            If LMFControlC.TARIFF_INKA.Equals(tariff) = True Then
                Me._Vcon.SetErrorControl(.cmbTariffKbn)
                Return Me._Vcon.SetErrMessage("E286", New String() {.cmbTariffKbn.SelectedText})
            End If

            '横持ち以外、スルー
            If LMFControlC.TARIFF_YOKO.Equals(tariff) = False Then
                Return True
            End If

            'タリフに値がない場合、スルー
            If String.IsNullOrEmpty(.txtTariffCd.TextValue) = True Then
                Return True
            End If

            '計算区分チェック
            Dim nullChk As Boolean = String.IsNullOrEmpty(.cmbSharyoKbn.SelectedValue.ToString())
            Dim calcChk As Boolean = LMFControlC.CALC_KBN_CAR.Equals(.lblCalcKbn.TextValue)

            '車建て 且つ 車輌区分がない場合、エラー
            If calcChk = True _
                AndAlso nullChk = True _
                Then
                .cmbSharyoKbn.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                .txtTariffCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.cmbTariffKbn)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E187", New String() {LMFControlC.SHADATE, .lblTitleSharyoKbn.Text})
                Return Me._Vcon.SetErrMessage("E797")
                '2016.01.06 UMANO 英語化対応END

            End If

            '車建て以外 且つ 車輌区分がある場合、エラー
            If calcChk = False _
                AndAlso nullChk = False _
                Then
                .cmbSharyoKbn.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                .txtTariffCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.cmbTariffKbn)
                '2016.01.06 UMANO 英語化対応START
                'Return Me._Vcon.SetErrMessage("E211", New String() {String.Concat(LMFControlC.SHADATE, LMFControlC.IGAI), .lblTitleSharyoKbn.Text})
                Return Me._Vcon.SetErrMessage("E836")
                '2016.01.06 UMANO 英語化対応END

            End If


            Return True

        End With

    End Function

    ''' <summary>
    ''' マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMstExistChk() As Boolean

        '運送会社マスタの存在チェック
        Dim rtnResult As Boolean = Me.IsUnsocoExistChk()

        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
        '荷主マスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsCustExistChk()
        '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End

        '届先マスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsDestExistChk1()

        'タリフの存在チェック
        rtnResult = rtnResult AndAlso Me.IsTariffExistChk()

        '割増タリフマスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsExtcExistChk()

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '支払タリフの存在チェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiTariffExistChk()

        '支払割増タリフマスタ存在チェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiExtcExistChk()
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        '届先マスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsDestExistChk2()

        'エリアマスタの存在チェック
        rtnResult = rtnResult AndAlso Me.IsAreaExistChk()

        Return rtnResult

    End Function

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsocoExistChk() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectUnsocoListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), .txtUnsocoCd.TextValue, .txtUnsocoBrCd.TextValue) = False Then
                .txtUnsocoBrCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(.txtUnsocoCd)
                Return False
            End If

            'マスタの値を設定
            .txtUnsocoCd.TextValue = drs(0).Item("UNSOCO_CD").ToString()
            .txtUnsocoBrCd.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString()
            Dim unsoNm As String = drs(0).Item("UNSOCO_NM").ToString()
            Dim unsoBrNm As String = drs(0).Item("UNSOCO_BR_NM").ToString()
            .lblUnsocoNm.TextValue = Me._Gcon.EditConcatData(unsoNm, unsoBrNm, Space(1))
            .lblUnsoNm.TextValue = unsoNm
            .lblUnsoBrNm.TextValue = unsoBrNm
            .lblTareYn.TextValue = drs(0).Item("TARE_YN").ToString()

            Return True

        End With

    End Function


    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
    ''' <summary>
    ''' 荷主マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCustExistChk(Optional ByVal bErrDisp As Boolean = True) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectCustListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), .txtCustCdL.TextValue, .txtCustCdM.TextValue, "", "", LMFControlC.CustMsgType.CUST_M) = False Then
                If bErrDisp Then    'エラー表示する場合のみ
                    .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Call Me._Vcon.SetErrorControl(.txtCustCdM)
                End If
                Return False
            End If

            'マスタの値を設定
            .txtCustCdL.TextValue = drs(0).Item("CUST_CD_L").ToString()
            Dim CustNmL As String = drs(0).Item("CUST_NM_L").ToString()

            Dim CustNmM As String = String.Empty
            If String.IsNullOrEmpty(.txtCustCdM.TextValue) = False Then
                '.txtCustCdMが空の場合でもtxtCustCdLでキャッシュからもってくるため
                .txtCustCdM.TextValue = drs(0).Item("CUST_CD_M").ToString()
                CustNmM = drs(0).Item("CUST_NM_M").ToString()
            End If

            .lblCustNm.TextValue = Me._Gcon.EditConcatData(CustNmL, CustNmM, Space(1))
            .lblCustNmM.TextValue = CustNmM '荷主中名称をダミーのラベルに保持する

            Return True

        End With

    End Function
    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End



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

            Dim tariffKbn As String = .cmbTariffKbn.SelectedValue.ToString()
            If Me._G.SelectTariffMst(tariffCd, tariffKbn, Me.GetCalcUlation()) = False Then

                Call Me._Vcon.SetErrorControl(.txtTariffCd)

                If LMFControlC.TARIFF_YOKO.Equals(tariffKbn) = True Then

                    Return Me._Vcon.SetYokoTariffExistErr(tariffCd)

                Else

                    Return Me._Vcon.SetUnchinTariffExistErr(tariffCd)

                End If

            End If

            Return True

        End With

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払タリフの存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiTariffExistChk() As Boolean

        With Me._Frm

            '値が無い場合、スルー
            Dim payTariffCd As String = .txtPayTariffCd.TextValue
            If String.IsNullOrEmpty(payTariffCd) = True Then
                Return True
            End If

            Dim tariffKbn As String = .cmbTariffKbn.SelectedValue.ToString()
            If Me._G.SelectShiharaiTariffMst(payTariffCd, tariffKbn, "02") = False Then

                Call Me._Vcon.SetErrorControl(.txtPayTariffCd)

                If LMFControlC.TARIFF_YOKO.Equals(tariffKbn) = True Then

                    Return Me._Vcon.SetShiharaiYokoTariffExistErr(payTariffCd)

                Else

                    Return Me._Vcon.SetShiharaiTariffExistErr(payTariffCd)

                End If

            End If

            Return True

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' (請求)割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsExtcExistChk() As Boolean

        With Me._Frm

            '値がない場合、スルー
            Dim extcCd As String = .txtExtcTariffCd.TextValue
            If String.IsNullOrEmpty(extcCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectExtcUnchinListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), extcCd) = False Then
                Call Me._Vcon.SetErrorControl(.txtExtcTariffCd)
                Return False
            End If

            'マスタの値を設定
            .txtExtcTariffCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString()
            .lblExtcTariffRem.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

            Return True

        End With

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiExtcExistChk() As Boolean

        With Me._Frm

            '値がない場合、スルー
            Dim payExtcCd As String = .txtPayExtcTariffCd.TextValue
            If String.IsNullOrEmpty(payExtcCd) = True Then
                Return True
            End If

            Dim drs As DataRow() = Nothing
            If Me._Vcon.SelectExtcShiharaiListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), payExtcCd) = False Then
                Call Me._Vcon.SetErrorControl(.txtPayExtcTariffCd)
                Return False
            End If

            'マスタの値を設定
            .txtPayExtcTariffCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString()
            .lblPayExtcTariffRem.TextValue = drs(0).Item("EXTC_TARIFF_REM").ToString()

            Return True

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 届先マスタの存在チェック(1回目)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk1() As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCdL.TextValue

            Dim shipCd As String = .txtShipCd.TextValue
            If String.IsNullOrEmpty(shipCd) = False Then

                '荷送人のチェック
                'START YANAI 要望番号376
                'If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, shipCd) = False Then
                '    Call Me._Vcon.SetErrorControl(.txtShipCd)
                '    Return False
                'End If

                '(2013.03.29)エラーメッセージを設定しないチェックPG呼び出し -- START --
                'If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, shipCd) = False Then
                If Me._Vcon.SelectDestListDataRow_NoMsg(drs, brCd, custCdL, shipCd) = False Then
                    '(2013.03.29)エラーメッセージを設定しないチェックPG呼び出し --  END  --
                    If Me._Vcon.SelectDestListDataRow(drs, brCd, "ZZZZZ", shipCd) = False Then
                        Call Me._Vcon.SetErrorControl(.txtShipCd)
                        Return False
                    End If
                End If
                'END YANAI 要望番号376

                'マスタの値を設定
                .txtShipCd.TextValue = drs(0).Item("DEST_CD").ToString()
                .lblShipNm.TextValue = drs(0).Item("DEST_NM").ToString()

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 届先マスタの存在チェック(2回目)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk2() As Boolean

        With Me._Frm

            Dim brCd As String = .cmbEigyo.SelectedValue.ToString()
            Dim custCdL As String = .txtCustCdL.TextValue

            '積込先の存在チェック
            Dim rtnResult As Boolean = Me.IsOrigExistChk(brCd, custCdL)

            '荷降先の存在チェック
            rtnResult = rtnResult AndAlso Me.IsDestExistChk(brCd, custCdL)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' 積込先の存在チェック
    ''' </summary>
    ''' <param name="brCd">営業所</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsOrigExistChk(ByVal brCd As String, ByVal custCdL As String) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            Dim origCd As String = .txtOrigCd.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(origCd) = True Then
                Return True
            End If

            'START YANAI 要望番号376
            ''積込先のチェック
            'If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, origCd) = False Then
            '    Call Me._Vcon.SetErrorControl(.txtOrigCd)
            '    Return False
            'End If
            '積込先のチェック
            If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, origCd) = False Then
                If Me._Vcon.SelectDestListDataRow(drs, brCd, "ZZZZZ", origCd) = False Then
                    Call Me._Vcon.SetErrorControl(.txtOrigCd)
                    Return False
                End If
            End If
            'END YANAI 要望番号376

            'マスタの値を設定
            .txtOrigCd.TextValue = drs(0).Item("DEST_CD").ToString()
            .lblOrigNm.TextValue = drs(0).Item("DEST_NM").ToString()
            .lblOrigJisCd.TextValue = drs(0).Item("JIS").ToString()

        End With

        Return True

    End Function

    ''' <summary>
    ''' 荷降先の存在チェック
    ''' </summary>
    ''' <param name="brCd">営業所</param>
    ''' <param name="custCdL">荷主コード(大)</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk(ByVal brCd As String, ByVal custCdL As String) As Boolean

        With Me._Frm

            Dim drs As DataRow() = Nothing
            Dim destCd As String = .txtDestCd.TextValue

            '値がない場合、スルー
            If String.IsNullOrEmpty(destCd) = True Then
                Return True
            End If

            '荷降先のチェック
            'START YANAI 要望番号376
            'If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, destCd) = False Then
            '    Call Me._Vcon.SetErrorControl(.txtDestCd)
            '    Return False
            'End If
            '(2013.03.29)エラーメッセージを設定しないチェックPG呼び出し -- START --
            'If Me._Vcon.SelectDestListDataRow(drs, brCd, custCdL, destCd) = False Then
            If Me._Vcon.SelectDestListDataRow_NoMsg(drs, brCd, custCdL, destCd) = False Then
                '(2013.03.29)エラーメッセージを設定しないチェックPG呼び出し --  END  --
                If Me._Vcon.SelectDestListDataRow(drs, brCd, "ZZZZZ", destCd) = False Then
                    Call Me._Vcon.SetErrorControl(.txtDestCd)
                    Return False
                End If
            End If
            'END YANAI 要望番号376

            'マスタの値を設定
            .txtDestCd.TextValue = drs(0).Item("DEST_CD").ToString()
            .lblDestNm.TextValue = drs(0).Item("DEST_NM").ToString()
            'START YANAI 要望番号376
            '.lblDestNm.TextValue = drs(0).Item("JIS").ToString()
            .lblDestJisCd.TextValue = drs(0).Item("JIS").ToString()
            'END YANAI 要望番号376
            .lblZipNo.TextValue = drs(0).Item("ZIP").ToString()
            .lblDestAdd1.TextValue = drs(0).Item("AD_1").ToString()
            .lblDestAdd2.TextValue = drs(0).Item("AD_2").ToString()

        End With

        Return True

    End Function

    ''' <summary>
    ''' エリアマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsAreaExistChk() As Boolean

        With Me._Frm

            '値がない場合、スルー
            Dim areaCd As String = .txtAreaCd.TextValue
            If String.IsNullOrEmpty(areaCd) = True Then
                Return True
            End If

            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            Dim binKb As String = .cmbBinKbn.SelectedValue.ToString
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End

            Dim drs As DataRow() = Nothing
            '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 Start
            'If Me._Vcon.SelectAreaListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), areaCd, .lblDestJisCd.TextValue) = False Then
            If Me._Vcon.SelectAreaListDataRow(drs, .cmbEigyo.SelectedValue.ToString(), areaCd, .lblDestJisCd.TextValue, binKb) = False Then
                '要望番号:1202(エリアマスタの便区分もキーとする) 2012/07/03 本明 End
                Call Me._Vcon.SetErrorControl(.txtAreaCd)
                Return False
            End If

            'マスタの値を設定
            .txtAreaCd.TextValue = drs(0).Item("AREA_CD").ToString()
            .lblAreaNm.TextValue = drs(0).Item("AREA_NM").ToString()

            Return True

        End With

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
    ''' 関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprConnectionChk() As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Dim max As Integer = .Rows.Count - 1
            Dim tbType As String = Me._Frm.lblTakSize.TextValue
            If -1 < max Then

                Dim tani As String = Me._Gcon.GetCellValue(.Cells(0, LMF020G.sprDetailDef.KOSU_TANI.ColNo))

                For i As Integer = 0 To max

                    'START YANAI 要望番号1260 必須項目見直し
                    ''商品KEY、名称の必須チェック
                    'If Me.IsKanrenHissuChk(i) = False Then
                    '    Return False
                    'End If
                    'END YANAI 要望番号1260 必須項目見直し

                    '単位混在チェック
                    If 0 <> i _
                        AndAlso String.IsNullOrEmpty(Me._Frm.txtTariffCd.TextValue) = False _
                        AndAlso LMFControlC.FLG_OFF.Equals(Me._Frm.lblSplitFlg.TextValue) = True _
                        AndAlso LMFControlC.CALC_KBN_NISUGATA.Equals(Me._Frm.lblCalcKbn.TextValue) = True _
                        AndAlso Me.IsTaniMixChk(i, tani) = False _
                        Then
                        Return False
                    End If

                    '端数 , 入数の関連チェック
                    If Me.IsHasuIrisuChk(i) = False Then
                        Return False
                    End If

                    '宅急便のチェック
                    If Me.IsTakSizeChk(tbType, i) = False Then
                        .Cells(i, LMF020G.sprDetailDef.SIZE.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(Me._Frm.txtTariffCd)
                        Return Me.SetTakSizeErr(tbType)
                    End If

                Next

                Return True

            End If

        End With

    End Function

    ''' <summary>
    ''' 単位混在チェック
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="tani">単位</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTaniMixChk(ByVal rowNo As Integer, ByVal tani As String) As Boolean

        Dim spr As Win.Spread.LMSpread = Me._Frm.sprDetail

        With spr.ActiveSheet

            '別の単位がある場合、エラー
            If tani.Equals(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.KOSU_TANI.ColNo))) = False Then
                Me._Frm.txtTariffCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Call Me._Vcon.SetErrorControl(Me._Frm.cmbTariffKbn)
                Return Me._Vcon.SetErrMessage("E140")
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 端数 , 入数の関連チェック
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsHasuIrisuChk(ByVal rowNo As Integer) As Boolean

        Dim spr As Win.Spread.LMSpread = Me._Frm.sprDetail
        With spr.ActiveSheet

            '梱数がゼロの以外の場合、チェックを行う
            If 0 <> Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.UNSO_KOSU.ColNo)))) Then

                '大小チェック
                If Me._Vcon.IsLargeSmallChk(Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.KONPO_KOSU.ColNo)))) _
                                            , Convert.ToDecimal(Me._Gcon.FormatNumValue(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.HASU.ColNo)))) _
                                            , True) = False Then
                    Me._Vcon.SetErrorControl(spr, rowNo, LMF020G.sprDetailDef.HASU.ColNo)
                    Return Me._Vcon.SetErrMessage("E218")
                End If

            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 宅急便サイズの関連チェック
    ''' </summary>
    ''' <param name="tbType">テーブルタイプ</param>
    ''' <param name="rowNo">行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTakSizeChk(ByVal tbType As String, ByVal rowNo As Integer) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            '値がない場合、スルー
            If String.IsNullOrEmpty(tbType) = True Then
                Return True
            End If

            'タリフコードに値がない場合、スルー
            If String.IsNullOrEmpty(Me._Frm.txtTariffCd.TextValue) = True Then
                Return True
            End If

            'True(値がないとエラー)　False(値があるとエラー)
            Dim chkFlg As Boolean = LMF020C.TABLE_TYPE_TAK.Equals(tbType)
            If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.SIZE.ColNo))) = chkFlg Then
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 宅急便サイズのチェックエラーメッセージを表示
    ''' </summary>
    ''' <param name="tbType">テーブルタイプ</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetTakSizeErr(ByVal tbType As String) As Boolean

        '2016.01.06 UMANO 英語化対応START
        'Dim id As String = "E187"
        Dim id As String = "E899"
        'Dim msg As String = LMF020C.TYPE_TAKKYUBIN
        If LMF020C.TABLE_TYPE_TAK.Equals(tbType) = False Then
            'id = "E211"
            id = "E900"
            'msg = String.Concat(msg, LMFControlC.IGAI)
        End If
        'Return Me._Vcon.SetErrMessage(id, New String() {msg, Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.SIZE.ColName)})
        Return Me._Vcon.SetErrMessage(id, New String() {Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.SIZE.ColName)})
        '2016.01.06 UMANO 英語化対応END

    End Function

    ''' <summary>
    ''' ワーニングチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsWarningChk() As Boolean

        With Me._Frm

            'タリフがない場合、ワーニング
            Dim rtnResult As Boolean = Me.IsTariffNullChk()

            '休日チェック
            rtnResult = rtnResult AndAlso Me.IsHoriDateChk(.imdOrigDate, .lblTitleOrigDate.Text)
            rtnResult = rtnResult AndAlso Me.IsHoriDateChk(.imdDestDate, .lblTitleDestDate.Text)

            Return rtnResult

        End With

    End Function

    ''' <summary>
    ''' タリフの必須チェック(ワーニング)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTariffNullChk() As Boolean

        'タリフがない場合、ワーニング
        If String.IsNullOrEmpty(Me._Frm.txtTariffCd.TextValue) = False Then
            Return True
        End If

        'いいえ選択時
        If Me._Vcon.IsWarningChk(MyBase.ShowMessage("W139", New String() {String.Concat(Me._Frm.lblTitleTariff.Text, LMFControlC.CD)})) = False Then
            Call Me._Vcon.SetErrorControl(Me._Frm.txtTariffCd)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 休日チェック
    ''' </summary>
    ''' <param name="ctl">日付コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHoriDateChk(ByVal ctl As Win.InputMan.LMImDate, ByVal msg As String) As Boolean

        '値がない場合、スルー
        Dim value As String = ctl.TextValue
        If String.IsNullOrEmpty(value) = True Then
            Return True
        End If

        Select Case Convert.ToDateTime(DateFormatUtility.EditSlash(value)).DayOfWeek

            Case System.DayOfWeek.Sunday, System.DayOfWeek.Saturday

                'ワーニング表示
                If Me._Vcon.IsWarningChk(Me.SetHolWarning(msg, LMF020C.KYUJITU)) = False Then
                    Call Me._Vcon.SetErrorControl(ctl)
                    Return False
                End If

        End Select

        '休日マスタ検索
        Dim drs As DataRow() = Me._Vcon.SelectHolListDataRow(value)
        If 0 < drs.Length Then
            If Me._Vcon.IsWarningChk(Me.SetHolWarning(msg, LMF020C.SHUKUJITU)) = False Then
                Call Me._Vcon.SetErrorControl(ctl)
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 休日チェックメッセージ
    ''' </summary>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <returns>メッセージ出力の戻り値</returns>
    ''' <remarks></remarks>
    Private Function SetHolWarning(ByVal msg1 As String, ByVal msg2 As String) As Integer
        Return MyBase.ShowMessage("W140", New String() {msg1, msg2})
    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMF020C.ActionType) As Boolean

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMF020C.ActionType.MASTEROPEN) = True Then
                Call Me._Vcon.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim txtCtl As Win.InputMan.LMImTextBox() = Nothing
        Dim lblCtl As Control() = Nothing
        Dim msg As String() = Nothing

        With Me._Frm

            Select Case objNm

                Case .sprDetail.Name

                    Return True

                Case .txtUnsocoCd.Name, .txtUnsocoBrCd.Name

                    Dim unsoNm As String = .lblTitleUnsoco.Text
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtUnsocoCd, .txtUnsocoBrCd}
                    lblCtl = New Control() {.lblUnsocoNm}
                    msg = New String() {String.Concat(unsoNm, LMFControlC.CD), String.Concat(unsoNm, LMFControlC.BR_CD)}


                    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 Start
                Case .txtCustCdM.Name

                    Return True
                    '要望番号:1240(【春日部要望】運送編集画面：荷主中の変更を可能にする) 2012/07/05 本明 End


                Case .txtShipCd.Name

                    Dim shipNm As String = .lblTitleShip.Text
                    lblCtl = New Control() {.lblShipNm}
                    txtCtl = New Win.InputMan.LMImTextBox() {.txtShipCd}
                    msg = New String() {String.Concat(shipNm, LMFControlC.L_NM, LMFControlC.CD)}

                Case .txtTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtTariffCd}
                    lblCtl = New Control() {.lblTariffRem}
                    msg = New String() {String.Concat(.lblTitleTariff.Text, LMFControlC.CD)}

                Case .txtExtcTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtExtcTariffCd}
                    lblCtl = New Control() {.lblExtcTariffRem}
                    msg = New String() {String.Concat(.lblTitleExtcTariff.Text, LMFControlC.CD)}

                    'START UMANO 要望番号1302 支払運賃に伴う修正。
                Case .txtPayTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtPayTariffCd}
                    lblCtl = New Control() {.lblPayTariffRem}
                    msg = New String() {String.Concat(.lblTitlePayTariff.Text, LMFControlC.CD)}

                Case .txtPayExtcTariffCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtPayExtcTariffCd}
                    lblCtl = New Control() {.lblPayExtcTariffRem}
                    msg = New String() {String.Concat(.lblTitlePayExtcTariff.Text, LMFControlC.CD)}
                    'END UMANO 要望番号1302 支払運賃に伴う修正。

                Case .txtOrigCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtOrigCd}
                    lblCtl = New Control() {.lblOrigNm, .lblOrigJisCd}
                    msg = New String() {String.Concat(.lblTitleOrig.Text, LMFControlC.CD)}

                Case .txtDestCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtDestCd}
                    lblCtl = New Control() {.lblDestNm, .lblDestJisCd, .lblZipNo, .lblDestAdd1, .lblDestAdd2}
                    msg = New String() {String.Concat(.lblTitleDest.Text, LMFControlC.CD)}

                Case .txtAreaCd.Name

                    txtCtl = New Win.InputMan.LMImTextBox() {.txtAreaCd}
                    lblCtl = New Control() {.lblAreaNm}
                    msg = New String() {String.Concat(.lblTitleArea.Text, LMFControlC.CD)}

            End Select

            'フォーカス位置チェック
            Return Me._Vcon.IsFocusChk(actionType.ToString(), txtCtl, msg, lblCtl)

        End With

    End Function

    ''' <summary>
    ''' POP UP用入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPopupInputCheck(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal colNo As Integer, ByVal colNm As String) As Boolean

        'チェック用のセル
        Dim vCell As Utility.Spread.LMValidatableCells = New Utility.Spread.LMValidatableCells(spr)

        With vCell

            .SetValidateCell(rowNo, colNo)
            .ItemName = colNm
            .IsForbiddenWordsCheck = True

            Return Me.IsValidateCheck(vCell)

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
    Friend Function IsAuthority(ByVal actionType As LMF020C.ActionType) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv()
        Dim kengenFlg As Boolean = True

        Select Case actionType

            Case LMF020C.ActionType.EDIT, LMF020C.ActionType.UNSO_NEW

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.COPY

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.DELETE

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.DEST_SAVE

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.MASTEROPEN

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.SAVE

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.CLOSE

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

            Case LMF020C.ActionType.PRINT

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.ADD

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

            Case LMF020C.ActionType.DEL

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
                        'START UMANO 20120630 外部権限の変更(春日部対応)
                        'kengenFlg = False
                        kengenFlg = True
                        'END UMANO 20120630 外部権限の変更(春日部対応)
                End Select

        End Select

        Return Me._Vcon.IsAuthorityChk(kengenFlg)

    End Function

    ''' <summary>
    ''' 編集時のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsEditChk(ByVal ds As DataSet) As Boolean

        '自営業のチェック
        Dim msg As String = Me._Vcon.SetRepMsgData(Me._Frm.FunctionKey.F2ButtonName)
        Dim rtnResult As Boolean = Me.IsMyNrsBrChk(msg)

        '運送手配区分チェック
        rtnResult = rtnResult AndAlso Me.IsTehaiChk(msg)

        '請求済みチェック
        rtnResult = rtnResult AndAlso Me.IsKakuteiGroupChk(ds, msg)

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '支払済みチェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiKakuteiChk(ds, msg)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        Return rtnResult

    End Function

    ''' <summary>
    ''' 複写時チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCopyChk() As Boolean

        '自営業チェック
        Dim msg As String = Me._Vcon.SetRepMsgData(Me._Frm.FunctionKey.F3ButtonName)
        Dim rtnResult As Boolean = Me.IsMyNrsBrChk(msg)

        '運送手配区分チェック
        rtnResult = rtnResult AndAlso Me.IsTehaiChk(msg)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 削除処理のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk(ByVal ds As DataSet) As Boolean

        '自営業のチェック
        Dim msg As String = Me._Vcon.SetRepMsgData(Me._Frm.FunctionKey.F4ButtonName)
        Dim rtnResult As Boolean = Me.IsMyNrsBrChk(msg)

        '運送手配区分チェック
        rtnResult = rtnResult AndAlso Me.IsTehaiChk(msg)

        '請求済みチェック
        rtnResult = rtnResult AndAlso Me.IsKakuteiGroupChk(ds, msg)

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '支払済みチェック
        rtnResult = rtnResult AndAlso Me.IsShiharaiKakuteiChk(ds, msg)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        '元データ区分チェック
        rtnResult = rtnResult AndAlso Me.IsMotoDataChk(msg)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 行追加時のチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAddChk() As Boolean

        Dim max As Integer = Me._Frm.sprDetail.ActiveSheet.Rows.Count - 1
        For i As Integer = 0 To max
            'START YANAI 要望番号1260 必須項目見直し
            'If Me.IsKanrenHissuChk(i) = False Then
            '    Return False
            'End If
            'END YANAI 要望番号1260 必須項目見直し
        Next

        Return True

    End Function

    ''' <summary>
    ''' 印刷処理前チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintChk(ByVal ds As DataSet) As Boolean

        '部数チェック
        Dim value As String = Me._Frm.cmbPrint.SelectedValue.ToString()
        Dim rtnResult As Boolean = Me.IsPrintMaeChk(value)

        'START YANAI 要望番号1191 運送：貨物引取書の入力チェック誤り
        '運送事由チェック
        rtnResult = rtnResult AndAlso Me.IsUnsoJiyuChk()
        'END YANAI 要望番号1191 運送：貨物引取書の入力チェック誤り

        Return rtnResult

    End Function

    ''' <summary>
    ''' 印刷時のチェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintMaeChk(ByVal value As String) As Boolean

        With Me._Frm

            '印刷種別の必須チェック
            .cmbPrint.ItemName = LMFControlC.PRINT_KBN
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

            Select Case value

                '印刷(納品書)の場合
                Case LMF020C.PRINT_NOUHIN

                    Dim objData As Object = .numPrtCnt.Value
                    If objData Is Nothing = False Then

                        If Convert.ToInt32(Me._Gcon.FormatNumValue(objData.ToString())) < 1 Then

                            Call Me._Vcon.SetErrorControl(.numPrtCnt)
                            Return Me._Vcon.SetErrMessage("E233", New String() {LMF020C.BUSU})

                        End If

                    End If

                    '印刷(荷札)の場合
                Case LMF020C.PRINT_NIFUDA

                    Dim objDataFrom As Object = .numPrtCnt_From.Value
                    If objDataFrom Is Nothing = False Then

                        If Convert.ToInt32(Me._Gcon.FormatNumValue(objDataFrom.ToString())) < 1 Then

                            Call Me._Vcon.SetErrorControl(.numPrtCnt_From)
                            Return Me._Vcon.SetErrMessage("E233", New String() {LMF020C.BUSU})

                        End If

                    End If

                    Dim objDataTo As Object = .numPrtCnt_To.Value
                    If objDataTo Is Nothing = False Then

                        If Convert.ToInt32(Me._Gcon.FormatNumValue(objDataTo.ToString())) < 1 Then

                            Call Me._Vcon.SetErrorControl(.numPrtCnt_To)
                            Return Me._Vcon.SetErrMessage("E233", New String() {LMF020C.BUSU})

                        End If

                    End If

                    If objDataFrom Is Nothing = False AndAlso objDataTo Is Nothing = False Then

                        If Convert.ToInt32(Me._Gcon.FormatNumValue(objDataFrom.ToString())) > Convert.ToInt32(Me._Gcon.FormatNumValue(objDataTo.ToString())) Then

                            Call Me._Vcon.SetErrorControl(.numPrtCnt_To)
                            Call Me._Vcon.SetErrorControl(.numPrtCnt_From)
                            Return Me._Vcon.SetErrMessage("E505", New String() {LMF020C.PRINT_RANGE_TO, LMF020C.PRINT_RANGE_FROM})

                        End If

                    End If
#If True Then   'ADD 2018/11/21 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能

                    '一括印刷の場合
                Case LMF020C.PRINT_ALL
                    '元データ区分 <> 運送の場合、エラー
                    If LMFControlC.MOTO_DATA_UNSO.Equals(Me._Frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
                        Return Me._Vcon.SetErrMessage("E175", New String() {String.Concat(Me._Frm.lblTitleMotoData.Text, Me._Frm.cmbMotoDataKbn.TextValue)})
                    End If

#End If

#If True Then   'ADD 2018/11/21 依頼番号 : 002743   【LMS】運行運送情報画面_一括印刷機能

                    '運送保険料申込書印刷の場合
                Case LMF020C.PRINT_UNSO_HOKEN
                    '元データ区分 <> 運送の場合、エラー
                    If LMFControlC.MOTO_DATA_UNSO.Equals(Me._Frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
                        Return Me._Vcon.SetErrMessage("E175", New String() {String.Concat(Me._Frm.lblTitleMotoData.Text, Me._Frm.cmbMotoDataKbn.TextValue)})
                    End If

#End If

                    '運送チェックリスト印刷の場合
                Case "08"
                    '元データ区分 <> 運送の場合、エラー
                    If LMFControlC.MOTO_DATA_UNSO.Equals(Me._Frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
                        Return Me._Vcon.SetErrMessage("E175", New String() {String.Concat(Me._Frm.lblTitleMotoData.Text, Me._Frm.cmbMotoDataKbn.TextValue)})
                    End If

                    '立合書（運送）印刷の場合
                Case "09"
                    '元データ区分 <> 運送の場合、エラー
                    If LMFControlC.MOTO_DATA_UNSO.Equals(Me._Frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
                        Return Me._Vcon.SetErrMessage("E175", New String() {String.Concat(Me._Frm.lblTitleMotoData.Text, Me._Frm.cmbMotoDataKbn.TextValue)})
                    End If

                Case Else

                    Return True

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 自営業チェック
    ''' </summary>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMyNrsBrChk(ByVal msg As String) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        ''自営業でない場合、エラー
        'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Frm.cmbEigyo.SelectedValue.ToString()) = False Then
        '    Return Me._Vcon.SetErrMessage("E178", New String() {msg})
        'End If

        Return True

    End Function

    ''' <summary>
    ''' 運送手配区分チェック
    ''' </summary>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsTehaiChk(ByVal msg As String) As Boolean

        '日陸手配でない場合、エラー
        If LMFControlC.TEHAI_NRS.Equals(Me._Frm.cmbTehaiKbn.SelectedValue.ToString()) = False Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me._Vcon.SetErrMessage("E336", New String() {LMFControlC.NRS_TEHAI, msg})
            Return Me._Vcon.SetErrMessage("E884", New String() {msg})
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function

    ''' <summary>
    ''' 確定、まとめ済みチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKakuteiGroupChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        '値が無い場合、スルー
        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_INFO)
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '運賃確定済みの場合、エラー
        If LMConst.FLG.OFF.Equals(dt.Rows(0).Item("FLAG_CNT").ToString()) = False Then
            Return Me._Vcon.SetKakuteiGroupErrMessage(LMFControlC.KAKUTEI, msg)
        End If

        'まとめ指示済みの場合、エラー
        If LMConst.FLG.OFF.Equals(dt.Rows(0).Item("GROPU_CNT").ToString()) = False Then
            Return Me._Vcon.SetKakuteiGroupErrMessage(LMFControlC.GROUP, msg)
        End If

        Return True

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払確定チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>※まとめチェックは現状行わない</remarks>
    Private Function IsShiharaiKakuteiChk(ByVal ds As DataSet, ByVal msg As String) As Boolean

        '値が無い場合、スルー
        Dim dt As DataTable = ds.Tables(LMF020C.TABLE_NM_SHIHARAI)
        If dt.Rows.Count < 1 Then
            Return True
        End If

        '支払運賃確定済みの場合、エラー
        If LMConst.FLG.OFF.Equals(dt.Rows(0).Item("FLAG_CNT").ToString()) = False Then
            Return Me._Vcon.SetKakuteiGroupErrMessage(LMFControlC.KAKUTEI, msg)
        End If

        ''まとめ指示済みの場合、エラー
        'If LMConst.FLG.OFF.Equals(dt.Rows(0).Item("GROPU_CNT").ToString()) = False Then
        '    Return Me._Vcon.SetKakuteiGroupErrMessage(LMFControlC.GROUP, msg)
        'End If

        Return True

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 元データ区分チェック
    ''' </summary>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMotoDataChk(ByVal msg As String) As Boolean

        If LMFControlC.MOTO_DATA_UNSO.Equals(Me._Frm.cmbMotoDataKbn.SelectedValue.ToString()) = False Then
            Return Me._Vcon.SetErrMessage("E028", New String() {String.Concat(Me._Frm.lblTitleMotoData.Text, LMF020C.KAKKO_1, Me._Frm.cmbMotoDataKbn.SelectedText, LMF020C.KAKKO_2), msg})
        End If

        Return True

    End Function

    'START YANAI 要望番号1260 必須項目見直し
    '''' <summary>
    '''' 商品KEY、名称の必須チェック
    '''' </summary>
    '''' <param name="rowNo">行番号</param>
    '''' <returns>True:エラーなし,OK False:エラーあり</returns>
    '''' <remarks></remarks>
    'Private Function IsKanrenHissuChk(ByVal rowNo As Integer) As Boolean

    '    Dim spr As Win.Spread.LMSpread = Me._Frm.sprDetail
    '    With Me._Frm.sprDetail.ActiveSheet

    '        '商品KEY、名称両方に値が無い場合、エラー
    '        If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.GOODS_CD.ColNo))) = True _
    '            AndAlso String.IsNullOrEmpty(Me._Gcon.GetCellValue(.Cells(rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo))) = True Then

    '            .Cells(rowNo, LMF020G.sprDetailDef.GOODS_NM.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
    '            Me._Vcon.SetErrorControl(spr, rowNo, LMF020G.sprDetailDef.GOODS_CD.ColNo)
    '            Return Me._Vcon.SetErrMessage("E270", New String() {Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.GOODS_CD.ColName), Me._Vcon.SetRepMsgData(LMF020G.sprDetailDef.GOODS_NM.ColName)})

    '        End If

    '    End With

    '    Return True

    'End Function
    'END YANAI 要望番号1260 必須項目見直し

    'START YANAI 要望番号1191 運送：貨物引取書の入力チェック誤り
    ''' <summary>
    ''' 運送事由チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsoJiyuChk() As Boolean

        If (LMFControlC.JIYUKB_HAISO).Equals(Me._Frm.cmbUnsoJiyuKbn.SelectedValue) = True AndAlso _
            (LMFControlC.PRINTKB_BUPPIN).Equals(Me._Frm.cmbPrint.SelectedValue) = True Then
            '2016.01.06 UMANO 英語化対応START
            'Return Me._Vcon.SetErrMessage("E028", New String() {"運送事由区分が配送", "物品取引書の印刷"})
            Return Me._Vcon.SetErrMessage("E855")
            '2016.01.06 UMANO 英語化対応END
        End If

        Return True

    End Function
    'END YANAI 要望番号1191 運送：貨物引取書の入力チェック誤り

    ''' <summary>
    ''' オーバーフローのエラーメッセージ
    ''' </summary>
    ''' <param name="maxData">最大値</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>False</returns>
    ''' <remarks></remarks>
    Private Function SetCalcOverErrMessage(ByVal maxData As String, ByVal msg As String) As Boolean

        Return Me._Vcon.SetErrMessage("E117", New String() {msg, maxData})

    End Function

    ''' <summary>
    ''' 運送新規Pop未選択チェック
    ''' </summary>
    ''' <param name="prm">パラメータクラス</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCustPopChk(ByVal prm As LMFormData) As Boolean

        If prm.ReturnFlg = False Then

            Return Me._Vcon.SetErrMessage("E193")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 締め日区分を取得
    ''' </summary>
    ''' <returns>締め日区分</returns>
    ''' <remarks></remarks>
    Friend Function GetCalcUlation() As String

        With Me._Frm

            '値設定
            'Dim custDrs As DataRow() = Me._Gcon.SelectCustListDataRow(.txtCustCdL.TextValue, .txtCustCdM.TextValue, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)
            Dim custDrs As DataRow() = Me._Gcon.SelectCustListDataRow(.cmbEigyo.SelectedValue.ToString, .txtCustCdL.TextValue, .txtCustCdM.TextValue, LMFControlC.FLG_OFF, LMFControlC.FLG_OFF)
            '20160928 要番2622 tsunehira add
            Dim shimeKbn As String = String.Empty
            If 0 < custDrs.Length Then
                shimeKbn = custDrs(0).Item("UNTIN_CALCULATION_KB").ToString()
            End If

            Return shimeKbn

        End With

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtUnsocoCd.TextValue = .txtUnsocoCd.TextValue.Trim()
            .txtUnsocoBrCd.TextValue = .txtUnsocoBrCd.TextValue.Trim()
            .txtOkuriNo.TextValue = .txtOkuriNo.TextValue.Trim()
            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtOrdNo.TextValue = .txtOrdNo.TextValue.Trim()
            .txtShipCd.TextValue = .txtShipCd.TextValue.Trim()
            .txtBuyerOrdNo.TextValue = .txtBuyerOrdNo.TextValue.Trim()
            .txtTariffCd.TextValue = .txtTariffCd.TextValue.Trim()
            .txtExtcTariffCd.TextValue = .txtExtcTariffCd.TextValue.Trim()
            .txtOrigTime.TextValue = .txtOrigTime.TextValue.Trim()
            .txtOrigCd.TextValue = .txtOrigCd.TextValue.Trim()
            .txtDestTime.TextValue = .txtDestTime.TextValue.Trim()
            .txtJiDestTime.TextValue = .txtJiDestTime.TextValue.Trim()
            .txtDestCd.TextValue = .txtDestCd.TextValue.Trim()
            .txtDestAdd3.TextValue = .txtDestAdd3.TextValue.Trim()
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
            .txtTel.TextValue = .txtTel.TextValue.Trim()
            '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end
            .txtAreaCd.TextValue = .txtAreaCd.TextValue.Trim()
            .txtUnsoComment.TextValue = .txtUnsoComment.TextValue.Trim()

        End With

    End Sub

#End Region 'Method

End Class
