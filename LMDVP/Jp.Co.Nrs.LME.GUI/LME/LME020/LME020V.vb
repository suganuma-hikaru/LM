' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME020V : 作業料明細編集
'  作  成  者       :  [YANAI]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base

''' <summary>
''' LME020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LME020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME020F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMEControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME020F, ByVal v As LMEControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LME020C.EventShubetsu) As Boolean

        '【単項目チェック】
        With Me._Frm

            'スペース除去
            Call Me.TrimSpaceTextValue()

            'マイナス０を変換
            Call Me.ZeroTextValue()


            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .cmbIozsKb.ReadOnly = False Then
                '入出在その他区分
                .cmbIozsKb.ItemName() = .lblTitleIozsKb.TextValue
                .cmbIozsKb.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbIozsKb) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtSagyoNm.ReadOnly = False Then
                '作業名
                .txtSagyoNm.ItemName() = "作業名"
                .txtSagyoNm.IsHissuCheck() = True
                .txtSagyoNm.IsForbiddenWordsCheck() = True
                .txtSagyoNm.IsByteCheck() = 60
                If MyBase.IsValidateCheck(.txtSagyoNm) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtCustCdL.ReadOnly = False Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsHissuCheck() = True
                .txtCustCdL.IsForbiddenWordsCheck() = True
                .txtCustCdL.IsByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                .txtCustCdL.ReadOnly = False Then
                '荷主コード(大)
                .txtCustCdL.ItemName() = "荷主コード(大)"
                .txtCustCdL.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtCustCdM.ReadOnly = False Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsHissuCheck() = True
                .txtCustCdM.IsForbiddenWordsCheck() = True
                .txtCustCdM.IsByteCheck() = 2
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                .txtCustCdM.ReadOnly = False Then
                '荷主コード(中)
                .txtCustCdM.ItemName() = "荷主コード(中)"
                .txtCustCdM.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtDestCd.ReadOnly = False Then
                '届先コード
                .txtDestCd.ItemName() = "届先コード"
                .txtDestCd.IsForbiddenWordsCheck() = True
                .txtDestCd.IsByteCheck() = 15
                If MyBase.IsValidateCheck(.txtDestCd) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtDestNm.ReadOnly = False Then
                '届先名
                .txtDestNm.ItemName() = "届先名"
                .txtDestNm.IsForbiddenWordsCheck() = True
                .txtDestNm.IsByteCheck() = 80
                If MyBase.IsValidateCheck(.txtDestNm) = False Then
                    Return False
                End If
            End If

            'START YANAI 要望番号875
            'If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
            '    .cmbDestSagyoFlg.ReadOnly = False Then
            '    '届先作業有無
            '    .cmbDestSagyoFlg.ItemName() = .lblTitleDestSagyoFlg.TextValue
            '    .cmbDestSagyoFlg.IsHissuCheck() = True
            '    If MyBase.IsValidateCheck(.cmbDestSagyoFlg) = False Then
            '        Return False
            '    End If
            'End If
            'END YANAI 要望番号875

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtGoodsCdCust.ReadOnly = False Then
                '商品コード
                .txtGoodsCdCust.ItemName() = "商品コード"
                If LME020C.MOTO_DATA_KBN_10.Equals(.cmbIozsKb.SelectedValue) OrElse LME020C.MOTO_DATA_KBN_20.Equals(.cmbIozsKb.SelectedValue) Then
                    .txtGoodsCdCust.IsHissuCheck() = False
                Else
                    .txtGoodsCdCust.IsHissuCheck() = True
                End If
                .txtGoodsCdCust.IsForbiddenWordsCheck() = True
                .txtGoodsCdCust.IsByteCheck() = 20
                If MyBase.IsValidateCheck(.txtGoodsCdCust) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtGoodsNm.ReadOnly = False Then
                '商品名
                .txtGoodsNm.ItemName() = "商品名"
                If LME020C.MOTO_DATA_KBN_10.Equals(.cmbIozsKb.SelectedValue) OrElse LME020C.MOTO_DATA_KBN_20.Equals(.cmbIozsKb.SelectedValue) Then
                    .txtGoodsNm.IsHissuCheck() = False
                Else
                    .txtGoodsNm.IsHissuCheck() = True
                End If
                .txtGoodsNm.IsForbiddenWordsCheck() = True
                .txtGoodsNm.IsByteCheck() = 60
                If MyBase.IsValidateCheck(.txtGoodsNm) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtLotNo.ReadOnly = False Then
                'ロット№
                .txtLotNo.ItemName() = .lblTitleLotNo.TextValue
                .txtLotNo.IsForbiddenWordsCheck() = True
                .txtLotNo.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtLotNo) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtSeiqtoCd.ReadOnly = False Then
                '請求先コード
                .txtSeiqtoCd.ItemName() = "請求先コード"
                .txtSeiqtoCd.IsHissuCheck() = True
                .txtSeiqtoCd.IsForbiddenWordsCheck() = True
                .txtSeiqtoCd.IsByteCheck() = 7
                If MyBase.IsValidateCheck(.txtSeiqtoCd) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                .txtSeiqtoCd.ReadOnly = False Then
                '請求先コード
                .txtSeiqtoCd.ItemName() = "請求先コード"
                .txtSeiqtoCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSeiqtoCd) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .cmbTaxKb.ReadOnly = False Then
                '課税区分
                .cmbTaxKb.ItemName() = .lblTitleTaxKb.TextValue
                .cmbTaxKb.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbTaxKb) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtRemarkZai.ReadOnly = False Then
                '在庫用備考
                .txtRemarkZai.ItemName() = .lblTitleRemarkZai.TextValue
                .txtRemarkZai.IsForbiddenWordsCheck() = True
                .txtRemarkZai.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtRemarkZai) = False Then
                    Return False
                End If
            End If

            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .txtRemarkSkyu.ReadOnly = False Then
                '在庫用備考
                .txtRemarkSkyu.ItemName() = .lblTitleRemarkSkyu.TextValue
                .txtRemarkSkyu.IsForbiddenWordsCheck() = True
                .txtRemarkSkyu.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtRemarkSkyu) = False Then
                    Return False
                End If
            End If

            '要望番号2038 追加START 2013.10.11
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                .numSagyoNb.ReadOnly = False Then
                '請求数
                .numSagyoNb.ItemName() = .lblTitleSagyoNb.TextValue
                If Me.IsZeroChk(Convert.ToDecimal(.numSagyoNb.Value.ToString())) = False Then
                    Me._Vcon.SetErrMessage("E233", New String() {.numSagyoNb.ItemName()})
                    Return False
                End If

            End If
            '要望番号2038 追加END 2013.10.11

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LME020C.EventShubetsu) As Boolean

        Dim dr() As DataRow = Nothing
        Dim eventNm As String = String.Empty

        '【関連項目チェック】
        With Me._Frm

            'セット入力チェック
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .txtCustCdL.ReadOnly = False AndAlso _
                    .txtCustCdM.ReadOnly = False Then
                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True OrElse _
                        String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        '2016.01.06 UMANO 英語化対応START
                        '荷主コード(大) + 荷主コード(中)
                        'MyBase.ShowMessage("E017", New String() {"荷主コード(大)", "荷主コード(中)"})
                        MyBase.ShowMessage("E766")
                        '2016.01.06 UMANO 英語化対応END
                        .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.txtCustCdL)
                        Return False
                    Else
                        .txtCustCdL.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                        .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            '荷主マスタ存在チェック
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .txtCustCdL.ReadOnly = False AndAlso _
                    .txtCustCdM.ReadOnly = False Then
                    '荷主コード(大) + 荷主コード(中)
                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat("CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                                                                                                 "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))
                    If 0 = dr.Length Then
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"荷主マスタ", String.Concat(.txtCustCdL.TextValue, "-", .txtCustCdM.TextValue)})
                        MyBase.ShowMessage("E773")
                        '2016.01.06 UMANO 英語化対応END
                        .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.txtCustCdL)
                        Return False
                    Else
                        .txtCustCdL.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                        .txtCustCdM.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            '作業金額範囲チェック
            Dim sagyoGk As Decimal = 0
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .numSagyoNb.ReadOnly = False AndAlso _
                    .numSagyoUp.ReadOnly = False AndAlso _
                    .numSagyoGk.ReadOnly = False Then
                    '請求数 + 請求単価 + 作業金額
                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("SAGYO_CD = '", .txtSagyoCd.TextValue, "'"))
                    If 0 < dr.Length Then
                        If dr(0).Item("KOSU_BAI").ToString = "01" Then
                            sagyoGk = Me.ToRound(Convert.ToDecimal(.numSagyoGk.Value), 0)
                        Else
                            sagyoGk = Me.ToRound(Convert.ToDecimal(.numSagyoNb.Value) * Convert.ToDecimal(.numSagyoUp.Value), 0)
                        End If

                        '作業金額の範囲チェック
                        '要望番号:1695 terakawa 2012.12.18 Start
                        'If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME020C.MIN_0), Convert.ToDecimal(LME020C.MAX_10.Replace(",", ""))) = False Then
                        '    MyBase.ShowMessage("E375", New String() {String.Concat("作業金額が", LME020C.MIN_0, "～", LME020C.MAX_10, "の範囲外の"), "保存"})
                        If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME020C.MIN_0), Convert.ToDecimal(LME020C.MAX_9_3.Replace(",", ""))) = False Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E375", New String() {String.Concat("作業金額が", LME020C.MIN_0, "～", LME020C.MAX_9_3, "の範囲外の"), "保存"})
                            MyBase.ShowMessage("E852", New String() {String.Concat(LME020C.MIN_0, "～", LME020C.MAX_9_3, "の範囲外の"), .FunctionKey.F11ButtonName})
                            '2016.01.06 UMANO 英語化対応END
                            '要望番号:1695 terakawa 2012.12.18 End
                            .numSagyoUp.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            .numSagyoGk.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Me._Vcon.SetErrorControl(.numSagyoNb)
                            Return False
                        Else
                            .numSagyoNb.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                            .numSagyoUp.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                            .numSagyoGk.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                        End If
                    End If
                End If
            End If

            '入出在その他区分チェック
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .cmbIozsKb.ReadOnly = False  Then
                    If (LME020C.MOTO_DATA_KBN_10).Equals(.cmbIozsKb.SelectedValue) = True OrElse _
                        (LME020C.MOTO_DATA_KBN_11).Equals(.cmbIozsKb.SelectedValue) = True OrElse _
                        (LME020C.MOTO_DATA_KBN_12).Equals(.cmbIozsKb.SelectedValue) = True OrElse _
                        (LME020C.MOTO_DATA_KBN_20).Equals(.cmbIozsKb.SelectedValue) = True OrElse _
                        (LME020C.MOTO_DATA_KBN_21).Equals(.cmbIozsKb.SelectedValue) = True OrElse _
                        (LME020C.MOTO_DATA_KBN_22).Equals(.cmbIozsKb.SelectedValue) = True Then
                        '入出在その他区分 + レコードステータス
                        MyBase.ShowMessage("E450")
                        Me._Vcon.SetErrorControl(.cmbIozsKb)
                        Return False
                    Else
                        .cmbIozsKb.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            '届先マスタ存在チェック
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .txtDestCd.ReadOnly = False AndAlso _
                    String.IsNullOrEmpty(.txtDestCd.TextValue) = False Then
                    '届先コード + 荷主コード(大)
                    '---↓
                    'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("DEST_CD = '", .txtDestCd.TextValue, "' AND ", _
                    '                                                                             "CUST_CD_L = '", .txtCustCdL.TextValue, "'"))

                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("DEST_CD") = .txtDestCd.TextValue
                    destMstDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                    destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    dr = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
                    '---↑

                    If 0 = dr.Length Then
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"届先マスタ", .txtDestCd.TextValue})
                        MyBase.ShowMessage("E698", New String() {.txtDestCd.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Me._Vcon.SetErrorControl(.txtDestCd)
                        Return False
                    Else
                        .txtDestCd.TextValue = dr(0).Item("DEST_CD").ToString
                        .txtDestCd.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            '商品マスタ存在チェック
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .txtGoodsCdCust.ReadOnly = False Then
                    '商品キー + 商品コード + 荷主コード(大) + 荷主コード(中)
                    If String.IsNullOrEmpty(.txtGoodsCdKey.TextValue) = False Then
                        '画面隠し項目の商品キーが空以外の場合
                        '---↓
                        'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = '", .txtGoodsCdKey.TextValue, "' AND ", _
                        '                                                                              "GOODS_CD_CUST = '", .txtGoodsCdCust.TextValue, "' AND ", _
                        '                                                                              "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                        '                                                                              "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))

                        Dim goodsDs As MGoodsDS = New MGoodsDS
                        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                        goodsDr.Item("GOODS_CD_NRS") = .txtGoodsCdKey.TextValue
                        goodsDr.Item("GOODS_CD_CUST") = .txtGoodsCdCust.TextValue
                        goodsDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                        goodsDr.Item("CUST_CD_M") = .txtCustCdM.TextValue
                        goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/18 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
                        goodsDr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
#End If
                        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                        dr = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                        '---↑

                        If 0 = dr.Length Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E451", New String() {"商品マスタ", .txtGoodsCdCust.TextValue, "商品ポップアップ"})
                            MyBase.ShowMessage("E769", New String() {.txtGoodsCdCust.TextValue})
                            '2016.01.06 UMANO 英語化対応END
                            Me._Vcon.SetErrorControl(.txtGoodsCdCust)
                            Return False
                        Else
                            .txtGoodsCdCust.TextValue = dr(0).Item("GOODS_CD_CUST").ToString
                            .txtGoodsCdCust.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                        End If

                    Else
                        '画面隠し項目の商品キーが空の場合
                        '---↓
                        'dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_CUST = '", .txtGoodsCdCust.TextValue, "' AND ", _
                        '                                                                              "CUST_CD_L = '", .txtCustCdL.TextValue, "' AND ", _
                        '                                                                              "CUST_CD_M = '", .txtCustCdM.TextValue, "'"))

                        Dim goodsDs As MGoodsDS = New MGoodsDS
                        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
                        goodsDr.Item("GOODS_CD_CUST") = .txtGoodsCdCust.TextValue
                        goodsDr.Item("CUST_CD_L") = .txtCustCdL.TextValue
                        goodsDr.Item("CUST_CD_M") = .txtCustCdM.TextValue
#If True Then   'ADD 2023/01/18 035090   【LMS】住友ファーマ　②その他機能でも使用している①と同ソース修正
                        goodsDr.Item("NRS_BR_CD") = .cmbEigyo.SelectedValue.ToString()
#End If
                        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
                        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
                        dr = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
                        '---↑

                        If 0 = dr.Length Then
                            '0件の場合はNG
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E079", New String() {"商品マスタ", .txtGoodsCdCust.TextValue})
                            MyBase.ShowMessage("E769", New String() {.txtGoodsCdCust.TextValue})
                            '2016.01.06 UMANO 英語化対応END
                            Me._Vcon.SetErrorControl(.txtGoodsCdCust)
                            Return False
                        ElseIf 1 = dr.Length Then
                            '1件の場合はOK
                            .txtGoodsCdKey.TextValue = dr(0).Item("GOODS_CD_NRS").ToString
                            .txtGoodsCdCust.TextValue = dr(0).Item("GOODS_CD_CUST").ToString
                            .txtGoodsCdCust.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                        ElseIf 2 <= dr.Length Then
                            '2件以上の場合はNG
                            MyBase.ShowMessage("E452")
                            Me._Vcon.SetErrorControl(.txtGoodsCdCust)
                            Return False
                        End If
                    End If
                End If
            End If

            '請求先マスタ存在チェック
            If LME020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .txtSeiqtoCd.ReadOnly = False Then
                    '請求先コード
                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("SEIQTO_CD = '", .txtSeiqtoCd.TextValue, "'"))
                    If 0 = dr.Length Then
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E079", New String() {"請求先マスタ", .txtSeiqtoCd.TextValue})
                        MyBase.ShowMessage("E830", New String() {.txtSeiqtoCd.TextValue})
                        '2016.01.06 UMANO 英語化対応END
                        Me._Vcon.SetErrorControl(.txtSeiqtoCd)
                        Return False
                    Else
                        .txtSeiqtoCd.TextValue = dr(0).Item("SEIQTO_CD").ToString
                        .txtSeiqtoCd.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            '請求確認済みチェック
            If LME020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
                LME020C.EventShubetsu.DEL.Equals(eventShubetsu) = True Then
                If ("01").Equals(.cmbSkyuChk.SelectedValue) = True Then
                    If LME020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True Then
                        '2016.01.06 UMANO 英語化対応START
                        'eventNm = "編集"
                        eventNm = .FunctionKey.F2ButtonName()
                    Else
                        'eventNm = "削除"
                        eventNm = .FunctionKey.F4ButtonName()
                        '2016.01.06 UMANO 英語化対応END
                    End If
                    MyBase.ShowMessage("E453", New String() {eventNm})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    'START YANAI 要望番号875
    ''' <summary>
    ''' 作業金額チェック（エラー）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSagyoKingakuCheck(ByVal sagyoGk As Decimal) As Boolean

        Dim dr() As DataRow = Nothing
        Dim eventNm As String = String.Empty

        '【関連項目チェック】
        With Me._Frm

            '作業金額の範囲チェック
            '要望番号:1695 terakawa 2012.12.18 Start
            'If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME020C.MIN_0), Convert.ToDecimal(LME020C.MAX_10.Replace(",", ""))) = False Then
            '    MyBase.ShowMessage("E471", New String() {String.Concat("請求数 × 請求単価"), String.Concat(LME020C.MIN_0, "～", LME020C.MAX_10, "の範囲外")})
            If Me.IsHaniCheck(sagyoGk, Convert.ToDecimal(LME020C.MIN_0), Convert.ToDecimal(LME020C.MAX_9_3.Replace(",", ""))) = False Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E471", New String() {String.Concat("請求数 × 請求単価"), String.Concat(LME020C.MIN_0, "～", LME020C.MAX_9_3, "の範囲外")})
                MyBase.ShowMessage("E471", New String() {String.Concat(.lblTitleSagyoNb.Text(), "×", .lblTitleSagyoUp.Text()), String.Concat(LME020C.MIN_0, "～", LME020C.MAX_9_3)})
                '2016.01.06 UMANO 英語化対応END
                '要望番号:1695 terakawa 2012.12.18 End
                .numSagyoUp.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                .numSagyoGk.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.numSagyoNb)
                Return False
            Else
                .numSagyoNb.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                .numSagyoUp.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                .numSagyoGk.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
            End If

        End With

        Return True

    End Function
    'END YANAI 要望番号875

    ''' <summary>
    ''' マスタ参照時のコントロールロックチェック
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks>参照Popupが開くコントロールのロックチェック</remarks>
    Friend Function ShowPopupControlChk(ByVal frm As LME020F, ByVal objNM As String) As Boolean

        'オブジェクト名による分岐
        Select Case objNM

            Case "txtCustCdL" '荷主コード(大)

                If frm.txtCustCdL.ReadOnly = True Then
                    Return False
                Else
                    Return True
                End If

            Case "txtCustCdM" '荷主コード(中)

                If frm.txtCustCdM.ReadOnly = True Then
                    Return False
                Else
                    Return True
                End If

            Case "txtDestCd", "txtDestNm" '届先コード、届先名

                If frm.txtDestCd.ReadOnly = True Then
                    Return False
                Else
                    Return True
                End If

            Case "txtGoodsCdCust", "txtGoodsNm" '商品コード、商品名

                If frm.txtGoodsCdCust.ReadOnly = True Then
                    Return False
                Else
                    Return True
                End If

            Case "txtSeiqtoCd"  '請求先コード

                If frm.txtSeiqtoCd.ReadOnly = True Then
                    Return False
                Else
                    Return True
                End If

        End Select

        Return False

    End Function

    ''' <summary>
    ''' 範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHaniCheck(ByVal value As Decimal, ByVal minData As Decimal, ByVal maxData As Decimal) As Boolean

        If value < minData OrElse _
            maxData < value Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LME020C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LME020C.EventShubetsu.SINKI        '新規
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.HENSHU       '編集
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.COPY       '複写
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.DEL       '削除
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.SKIP       'スキップ
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.MASTER       'マスタ参照
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.HOZON       '保存
                '10:閲覧者、50:外部の場合エラー
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

            Case LME020C.EventShubetsu.CLOSE       '閉じる
                'すべての権限許可
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
                        kengenFlg = True
                End Select

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理
            .txtSagyoNm.TextValue = Trim(.txtSagyoNm.TextValue)
            .txtCustCdL.TextValue = Trim(.txtCustCdL.TextValue)
            .txtCustCdM.TextValue = Trim(.txtCustCdM.TextValue)
            .txtDestCd.TextValue = Trim(.txtDestCd.TextValue)
            .txtDestNm.TextValue = Trim(.txtDestNm.TextValue)
            .txtGoodsCdCust.TextValue = Trim(.txtGoodsCdCust.TextValue)
            .txtGoodsNm.TextValue = Trim(.txtGoodsNm.TextValue)
            .txtLotNo.TextValue = Trim(.txtLotNo.TextValue)
            .txtSeiqtoCd.TextValue = Trim(.txtSeiqtoCd.TextValue)
            .txtRemarkZai.TextValue = Trim(.txtRemarkZai.TextValue)
            .txtRemarkSkyu.TextValue = Trim(.txtRemarkSkyu.TextValue)
        End With

    End Sub

    ''' <summary>
    ''' マイナス０を変換
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ZeroTextValue()

        With Me._Frm
            '各項目のTrim処理
            .numSagyoNb.Value = System.Math.Abs(Convert.ToDecimal(.numSagyoNb.Value))
            .numSagyoUp.Value = System.Math.Abs(Convert.ToDecimal(.numSagyoUp.Value))
            .numSagyoGk.Value = System.Math.Abs(Convert.ToDecimal(.numSagyoGk.Value))
        End With

    End Sub

    '要望番号2038 追加START 2013.1011
    ''' <summary>
    ''' ゼロチェック
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsZeroChk(ByVal value As Decimal) As Boolean

        If 0 = value Then

            Return False

        End If

        Return True

    End Function
    '要望番号2038 追加END 2013.1011

#Region "四捨五入"
    ''' <summary>
    ''' 四捨五入
    ''' </summary>
    ''' <param name="decValue"></param>
    ''' <param name="iDigits"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ToRound(ByVal decValue As Decimal, ByVal iDigits As Integer) As Decimal

        Dim dCoef As Double = System.Math.Pow(10, iDigits)

        If decValue > 0 Then
            Return Convert.ToDecimal(Math.Floor((decValue * dCoef) + 0.5) / dCoef)
        Else
            Return Convert.ToDecimal(Math.Ceiling((decValue * dCoef) - 0.5) / dCoef)
        End If
    End Function
#End Region

#End Region 'Method

End Class
