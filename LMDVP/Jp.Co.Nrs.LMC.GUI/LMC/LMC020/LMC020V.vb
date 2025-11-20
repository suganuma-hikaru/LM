' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC020V : 出荷データ編集
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.Win.GUI
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMC020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMC020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC020F

    ''' <summary>
    ''' このValidateクラスが紐付くハンドラクラスクラス
    ''' </summary>
    ''' <remarks></remarks>

    Private _H As LMC020H

    ''' <summary>
    ''' GAMEN共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMCControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMCControlV

    ''' <summary>
    ''' Handler共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Hcon As LMCControlH

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass
        Me._H = DirectCast(Me.MyHandler, LMC020H)

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._Gcon = New LMCControlG(handlerClass, DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._Vcon = New LMCControlV(handlerClass, DirectCast(frm, Form))

        'Handler共通クラスの設定
        Me._Hcon = New LMCControlH(DirectCast(frm, Form))

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
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal ds As DataSet, ByVal nowDate As String) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim arr As ArrayList = Nothing

        'スペース除去
        Call Me.TrimSpaceTextValue()

        'マイナス０を変換
        Call Me.ZeroTextValue()

        '【単項目チェック】
        With Me._Frm

            If .cmbPRINT.ReadOnly = False AndAlso LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '印刷種別
                '2017/09/25 修正 李↓
                .cmbPRINT.ItemName() = lgm.Selector({"印刷種別", "Printing type", "인쇄종별", "中国語"})
                '2017/09/25 修正 李↑

                .cmbPRINT.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPRINT) = False Then
                    Return False
                End If
            End If

            If .cmbJikkou.ReadOnly = False AndAlso LMC020C.EventShubetsu.JIKKOU.Equals(eventShubetsu) = True Then
                '実行種別
                '2017/09/25 修正 李↓
                .cmbJikkou.ItemName() = lgm.Selector({"実行種別", "Execution type", "실행종별", "中国語"})
                '2017/09/25 修正 李↑

                .cmbJikkou.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbJikkou) = False Then
                    Return False
                End If

                '出荷管理番号
                '2017/09/25 修正 李↓
                .lblSyukkaLNo.ItemName() = lgm.Selector({"出荷管理番号", "O/D management number", "출하관리번호", "中国語"})
                '2017/09/25 修正 李↑

                .lblSyukkaLNo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.lblSyukkaLNo) = False Then
                    Return False
                End If

            End If
            If ("01").Equals(.cmbPRINT.SelectedValue.ToString) = True Then
                If .numPrtCnt_From.ReadOnly = False AndAlso LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                    '印刷範囲From
                    If String.IsNullOrEmpty(.numPrtCnt_From.TextValue) = False Then
                        If Me.IsHaniCheck(Convert.ToDecimal(.numPrtCnt_From.Value), 1, 99) = False Then
                            MyBase.ShowMessage("E686")
                            Me._Vcon.SetErrorControl(.numPrtCnt_From)
                            Return False
                        End If
                    End If
                End If
                If .numPrtCnt_To.ReadOnly = False AndAlso LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                    '印刷範囲To
                    If String.IsNullOrEmpty(.numPrtCnt_To.TextValue) = False Then
                        If Me.IsHaniCheck(Convert.ToDecimal(.numPrtCnt_To.Value), 1, 99) = False Then
                            MyBase.ShowMessage("E686")
                            Me._Vcon.SetErrorControl(.numPrtCnt_To)
                            Return False
                        End If
                    End If
                End If
                If .numPrtCnt_From.ReadOnly = False AndAlso .numPrtCnt_To.ReadOnly = False AndAlso LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                    '印刷範囲 From + To 大小チェック
                    If .numPrtCnt_From.TextValue > .numPrtCnt_To.TextValue Then
                        MyBase.ShowMessage("E505", New String() {"印刷範囲 To", "印刷範囲 From"})
                        .numPrtCnt_From.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        .numPrtCnt_To.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numPrtCnt_From)
                        Return False
                    End If
                End If
            Else
                If .numPrtCnt.ReadOnly = False AndAlso LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                    '部数
                    If String.IsNullOrEmpty(.numPrtCnt.TextValue) = False Then
                        If Me.IsHaniCheck(Convert.ToDecimal(.numPrtCnt.Value), 1, 99) = False Then
                            '2015.10.21 tusnehira add
                            '英語化対応
                            MyBase.ShowMessage("E686")
                            'MyBase.ShowMessage("E014", New String() {"部数", "1", "99"})
                            Me._Vcon.SetErrorControl(.numPrtCnt)
                            Return False
                        End If
                    End If
                End If
            End If

            '出荷大項目
            If .cmbSoko.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.INS_M_ZERO.Equals(eventShubetsu) = True) Then
                '倉庫
                .cmbSoko.ItemName() = .lblSoko.TextValue
                .cmbSoko.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbSoko) = False Then
                    Return False
                End If
            End If

            If .imdSyukkaDate.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '出庫日
                If String.IsNullOrEmpty(.imdSyukkaDate.TextValue) = True Then
                    .imdSyukkaDate.TextValue = .imdSyukkaYoteiDate.TextValue
                End If
            End If

            If .imdSyukkaYoteiDate.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '出荷予定日
                .imdSyukkaYoteiDate.ItemName() = .lblSyukkaYoteiDate.TextValue
                .imdSyukkaYoteiDate.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.imdSyukkaYoteiDate) = False Then
                    Return False
                End If
            End If

            If .txtNisyuTyumonNo.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'オーダー番号
                .txtNisyuTyumonNo.ItemName() = .lblNisyuTyumonNo.TextValue
                .txtNisyuTyumonNo.IsForbiddenWordsCheck() = True
                .txtNisyuTyumonNo.IsByteCheck() = 30
                If MyBase.IsValidateCheck(.txtNisyuTyumonNo) = False Then
                    Return False
                End If
            End If

            If .txtKainusiTyumonNo.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '注文番号
                .txtKainusiTyumonNo.ItemName() = .lblKainusiTyumonNo.TextValue
                .txtKainusiTyumonNo.IsForbiddenWordsCheck() = True
                .txtKainusiTyumonNo.IsByteCheck() = 30
                If MyBase.IsValidateCheck(.txtKainusiTyumonNo) = False Then
                    Return False
                End If
            End If

            If .numKonpoKosu.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '出荷梱包個数
                If Me.IsHaniCheck(Convert.ToDecimal(.numKonpoKosu.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                    'メッセージの表示
                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.ShowMessage("E684", New String() {LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                    'MyBase.ShowMessage("E014", New String() {"出荷(大)の出荷梱包個数", LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                    Me._Vcon.SetErrorControl(.numKonpoKosu)
                    Return False
                End If
            End If

            If .txtUriCd.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '売上先コード
                .txtUriCd.ItemName() = .lblUriCd.TextValue
                .txtUriCd.IsForbiddenWordsCheck() = True
                .txtUriCd.IsByteCheck() = 15
                If MyBase.IsValidateCheck(.txtUriCd) = False Then
                    Return False
                End If
                If Me.IsDestExistChk(.txtUriCd.TextValue, LMC020C.URICD) = False Then
                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.ShowMessage("E698", New String() {.txtUriCd.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"届先マスタ", .txtUriCd.TextValue})
                    Me._Vcon.SetErrorControl(.txtUriCd)
                    Return False
                End If
            ElseIf .txtUriCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtUriCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtUriCd.ItemName() = .lblUriCd.TextValue
                .txtUriCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtUriCd) = False Then
                    Return False
                End If
            End If

            'START YANAI 要望番号982
            If .txtOkuriNo.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '送り状番号
                .txtOkuriNo.ItemName() = .lblOkuriNo.TextValue
                .txtOkuriNo.IsForbiddenWordsCheck() = True
                .txtOkuriNo.IsByteCheck() = 20
                If MyBase.IsValidateCheck(.txtOkuriNo) = False Then
                    Return False
                End If
            End If
            'END YANAI 要望番号982

            If .cmbTodokesaki.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '届先区分

                '2017/09/25 修正 李↓
                .cmbTodokesaki.ItemName() = lgm.Selector({"届先区分", "Delivery Address category", "송달처구분", "中国語"})
                '2017/09/25 修正 李↑

                .cmbTodokesaki.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbTodokesaki) = False Then
                    Return False
                End If
            End If

            If .txtTodokesakiCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False AndAlso _
                ("00").Equals(.cmbTodokesaki.SelectedValue) = True Then
                '届先コード
                .txtTodokesakiCd.ItemName() = .lblTodokesakiCD.TextValue
                .txtTodokesakiCd.IsHissuCheck() = True
                .txtTodokesakiCd.IsForbiddenWordsCheck() = True
                .txtTodokesakiCd.IsByteCheck() = 15
                If MyBase.IsValidateCheck(.txtTodokesakiCd) = False Then
                    Return False
                End If
                If Me.IsDestExistChk(.txtTodokesakiCd.TextValue, LMC020C.TODOKECD) = False Then
                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.ShowMessage("E698", New String() {.txtTodokesakiCd.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"届先マスタ", .txtTodokesakiCd.TextValue})
                    Me._Vcon.SetErrorControl(.txtTodokesakiCd)
                    Return False
                End If
            ElseIf .txtTodokesakiCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.TODOKESAKI.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.txtTodokesakiCd.TextValue) = False AndAlso Me.IsDestExistChk(.txtTodokesakiCd.TextValue, LMC020C.TODOKECD) = True Then
                    '2015.10.22 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E704", New String() {.txtTodokesakiCd.TextValue})
                    'MyBase.ShowMessage("E160", New String() {"届先マスタ", .txtTodokesakiCd.TextValue})
                    Me._Vcon.SetErrorControl(.txtTodokesakiCd)
                    Return False
                End If
            ElseIf .txtTodokesakiCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtTodokesakiCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '届先コード
                .txtTodokesakiCd.ItemName() = .lblTodokesakiCD.TextValue
                .txtTodokesakiCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtTodokesakiCd) = False Then
                    Return False
                End If
            End If

            If .txtTodokesakiNm.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '届先名

                '2017/09/25 修正 李↓
                .txtTodokesakiNm.ItemName() = lgm.Selector({"届先名", "Delivery Address Name", "송달처명", "中国語"})
                '2017/09/25 修正 李↑

                .txtTodokesakiNm.IsForbiddenWordsCheck() = True
                .txtTodokesakiNm.IsByteCheck() = 80
                If MyBase.IsValidateCheck(.txtTodokesakiNm) = False Then
                    Return False
                End If
            End If

            If .txtTodokeAdderss1.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '届先住所1

                '2017/09/25 修正 李↓
                .txtTodokeAdderss1.ItemName() = lgm.Selector({"届先住所１", "Delivery Address 1", "송달처주소1", "中国語"})
                '2017/09/25 修正 李↑

                .txtTodokeAdderss1.IsForbiddenWordsCheck() = True
                .txtTodokeAdderss1.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtTodokeAdderss1) = False Then
                    Return False
                End If
            End If

            If .txtTodokeAdderss2.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '届先住所2

                '2017/09/25 修正 李↓
                .txtTodokeAdderss2.ItemName() = lgm.Selector({"届先住所２", "Delivery Address 2", "송달처주소2", "中国語"})
                '2017/09/25 修正 李↑

                .txtTodokeAdderss2.IsForbiddenWordsCheck() = True
                .txtTodokeAdderss2.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtTodokeAdderss2) = False Then
                    Return False
                End If
            End If

            If .txtTodokeAdderss3.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '届先住所3

                '2017/09/25 修正 李↓
                .txtTodokeAdderss3.ItemName() = lgm.Selector({"届先住所３", "Delivery Address 3", "송달처주소3", "中国語"})
                '2017/09/25 修正 李↑

                .txtTodokeAdderss3.IsForbiddenWordsCheck() = True
                '(2012.12.11)要望番号1585 40byte→60byte -- START --
                '.txtTodokeAdderss3.IsByteCheck() = 40
                '2019/11/26 要望管理009400 rep
                '.txtTodokeAdderss3.IsByteCheck() = 60
                .txtTodokeAdderss3.IsByteCheck() = 80
                '(2012.12.11)要望番号1585 40byte→60byte --  END  --
                If MyBase.IsValidateCheck(.txtTodokeAdderss3) = False Then
                    Return False
                End If
            End If

            If .txtTodokeTel.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '届先電話番号
                .txtTodokeTel.ItemName() = .lblTodokeTel.TextValue
                .txtTodokeTel.IsForbiddenWordsCheck() = True
                .txtTodokeTel.IsByteCheck() = 20
                If MyBase.IsValidateCheck(.txtTodokeTel) = False Then
                    Return False
                End If
            End If

            If .txtNouhinTeki.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '納品書摘要
                .txtNouhinTeki.ItemName() = .lblNouhinTeki.TextValue
                .txtNouhinTeki.IsForbiddenWordsCheck() = True
                .txtNouhinTeki.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtNouhinTeki) = False Then
                    Return False
                End If
            End If

            If .txtSyukkaRemark.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '出荷時注意事項
                .txtSyukkaRemark.ItemName() = .lblSyukkaRemark.TextValue
                .txtSyukkaRemark.IsForbiddenWordsCheck() = True
                .txtSyukkaRemark.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSyukkaRemark) = False Then
                    Return False
                End If
            ElseIf .txtSyukkaRemark.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSyukkaRemark").Equals(Me._Frm.ActiveControl.Name) = True Then
                '出荷時注意事項
                .txtSyukkaRemark.ItemName() = .lblSyukkaRemark.TextValue
                .txtSyukkaRemark.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSyukkaRemark) = False Then
                    Return False
                End If
            End If

            If .txtOrderType.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'オーダータイプ
                .txtOrderType.ItemName() = .lblOrderType.TextValue
                .txtOrderType.IsForbiddenWordsCheck() = True
                .txtOrderType.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtOrderType) = False Then
                    Return False
                End If
                .txtOrderType.IsHissuCheck() = True
            End If

            If .txtHaisoRemark.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '配送時注意事項
                .txtHaisoRemark.ItemName() = .lblHaisoRemark.TextValue
                .txtHaisoRemark.IsForbiddenWordsCheck() = True
                .txtHaisoRemark.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtHaisoRemark) = False Then
                    Return False
                End If
            ElseIf .txtHaisoRemark.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtHaisoRemark").Equals(Me._Frm.ActiveControl.Name) = True Then
                '配送時注意事項
                .txtHaisoRemark.ItemName() = .lblHaisoRemark.TextValue
                .txtHaisoRemark.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtHaisoRemark) = False Then
                    Return False
                End If
            End If

            '商品別情報
            If .txtSerchGoodsCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                (("txtSerchGoodsCd").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchGoodsNm").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchLot").Equals(Me._Frm.ActiveControl.Name) = True) Then
                '商品コード（検索条件）
                .txtSerchGoodsCd.ItemName() = .lblSerchGoodsCd.TextValue
                .txtSerchGoodsCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSerchGoodsCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSerchGoodsNm.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                (("txtSerchGoodsCd").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchGoodsNm").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchLot").Equals(Me._Frm.ActiveControl.Name) = True) Then

                '商品名（検索条件）
                .txtSerchGoodsNm.ItemName() = .lblSerchGoodsCd.TextValue
                .txtSerchGoodsNm.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSerchGoodsNm) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSerchLot.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                (("txtSerchGoodsCd").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchGoodsNm").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchLot").Equals(Me._Frm.ActiveControl.Name) = True) Then
                'ロット№（検索条件）
                .txtSerchLot.ItemName() = .lblSerchLot.TextValue
                .txtSerchLot.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSerchLot) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            '商品別情報
            If .txtSerchGoodsCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True AndAlso _
                (("txtSerchGoodsCd").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchGoodsNm").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchLot").Equals(Me._Frm.ActiveControl.Name) = True) Then
                '商品コード（検索条件）
                .txtSerchGoodsCd.ItemName() = .lblSerchGoodsCd.TextValue
                .txtSerchGoodsCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSerchGoodsCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSerchGoodsNm.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True AndAlso _
                (("txtSerchGoodsCd").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchGoodsNm").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchLot").Equals(Me._Frm.ActiveControl.Name) = True) Then

                '商品名（検索条件）
                .txtSerchGoodsNm.ItemName() = .lblSerchGoodsCd.TextValue
                .txtSerchGoodsNm.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSerchGoodsNm) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSerchLot.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True AndAlso _
                (("txtSerchGoodsCd").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchGoodsNm").Equals(Me._Frm.ActiveControl.Name) = True OrElse _
                 ("txtSerchLot").Equals(Me._Frm.ActiveControl.Name) = True) Then
                'ロット№（検索条件）
                .txtSerchLot.ItemName() = .lblSerchLot.TextValue
                .txtSerchLot.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSerchLot) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If (LMC020C.EventShubetsu.HENKO.Equals(eventShubetsu) = True) Then
                '印順
                .numPrintSortHenko.ItemName() = .lblPrintSortHenko.TextValue
                .numPrintSortHenko.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.numPrintSortHenko) = False Then
                    .tabMiddle.SelectTab(.tabGoods)
                    Return False
                End If
            End If

            If .txtUnsoCompanyCd.ReadOnly = False AndAlso _
               LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
              ("txtUnsoCompanyCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '運送会社コード

                '2017/09/25 修正 李↓
                .txtUnsoCompanyCd.ItemName() = lgm.Selector({"運送会社コード", "TPTN company code", "운송회사코드", "中国語"})
                '2017/09/25 修正 李↑

                .txtUnsoCompanyCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtUnsoCompanyCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtUnsoSitenCd.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtUnsoSitenCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '運送会社支店コード

                '2017/09/25 修正 李↓
                .txtUnsoSitenCd.ItemName() = lgm.Selector({"運送会社支店コード", "TPTN company Branch code", "운송회사지점코드", "中国語"})
                '2017/09/25 修正 李↑

                .txtUnsoSitenCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtUnsoSitenCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtUnthinTariffCd.ReadOnly = False AndAlso _
               LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
              ("txtUnthinTariffCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '運賃タリフ
                .txtUnthinTariffCd.ItemName() = .lblUnthinTariff.TextValue
                .txtUnthinTariffCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtUnthinTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtExtcTariffCd.ReadOnly = False AndAlso _
               LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
              ("txtExtcTariffCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '割増タリフ
                .txtExtcTariffCd.ItemName() = .lblExtcTariff.TextValue
                .txtExtcTariffCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtExtcTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If .txtPayUnthinTariffCd.ReadOnly = False AndAlso _
               LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
               ("txtPayUnthinTariffCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '支払運賃タリフ
                .txtPayUnthinTariffCd.ItemName() = .lblPayUnthinTariff.TextValue
                .txtPayUnthinTariffCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtPayUnthinTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtPayExtcTariffCd.ReadOnly = False AndAlso _
               LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
              ("txtPayExtcTariffCd").Equals(Me._Frm.ActiveControl.Name) = True Then
                '支払割増タリフ
                .txtPayExtcTariffCd.ItemName() = .lblPayExtcTariff.TextValue
                .txtPayExtcTariffCd.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtPayExtcTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            'START YANAI 要望番号1233 運行・運送画面にて輸送営業所が空白のデータが表示されない
            If .cmbYusoBrCd.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '輸送営業所
                .cmbYusoBrCd.ItemName() = .lblYusoBr.TextValue
                .cmbYusoBrCd.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbYusoBrCd) = False Then
                    Return False
                End If
            End If
            'END YANAI 要望番号1233 運行・運送画面にて輸送営業所が空白のデータが表示されない

            If .sprSyukkaM.Enabled = True Then
                'スプレッド
                arr = Nothing
                arr = Me.getCheckList(.sprSyukkaM)
                If 0 = arr.Count AndAlso _
                   (LMC020C.EventShubetsu.DEL_M.Equals(eventShubetsu) = True OrElse _
                     LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True OrElse _
                     LMC020C.EventShubetsu.HENKO.Equals(eventShubetsu) = True) Then
                    MyBase.ShowMessage("E009")
                    .tabMiddle.SelectTab(.tabGoods)
                    Return False
                ElseIf 1 < arr.Count AndAlso _
                   (LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                    MyBase.ShowMessage("E008")
                    .tabMiddle.SelectTab(.tabGoods)
                    Return False
                    'ElseIf .sprSyukkaM.ActiveSheet.Rows.Count = arr.Count AndAlso _
                    '    (LMC020C.EventShubetsu.DEL_M.Equals(eventShubetsu) = True) Then
                    '    MyBase.ShowMessage("E280")
                    '    .tabMiddle.SelectTab(.tabGoods)
                    '    Return False
                ElseIf .sprSyukkaM.ActiveSheet.Rows.Count = 0 AndAlso _
                    (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                    MyBase.ShowMessage("E295")
                    .tabMiddle.SelectTab(.tabGoods)
                    Return False
                End If
            End If

            If .numPrintSort.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '印順
                .numPrintSort.ItemName() = .lblPrintSort.TextValue
                .numPrintSort.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.numPrintSort) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtOrderNo.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                'オーダー番号
                .txtOrderNo.ItemName() = .lblOrderNo.TextValue
                .txtOrderNo.IsForbiddenWordsCheck() = True
                .txtOrderNo.IsByteCheck() = 30
                If MyBase.IsValidateCheck(.txtOrderNo) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtRsvNo.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '予約番号
                .txtRsvNo.ItemName() = .lblTitleRsvNo.TextValue
                .txtRsvNo.IsForbiddenWordsCheck() = True
                .txtRsvNo.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtRsvNo) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSerialNo.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                'シリアル№
                .txtSerialNo.ItemName() = .lblSerialNo.TextValue
                .txtSerialNo.IsForbiddenWordsCheck() = True
                .txtSerialNo.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtSerialNo) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtCyumonNo.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '注文番号
                .txtCyumonNo.ItemName() = .lblCyumonNo.TextValue
                .txtCyumonNo.IsForbiddenWordsCheck() = True
                .txtCyumonNo.IsByteCheck() = 30
                If MyBase.IsValidateCheck(.txtCyumonNo) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .numIrime.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '入目
                .numIrime.ItemName() = .lblIrime.TextValue
                .numIrime.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.numIrime) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If String.IsNullOrEmpty(.numIrime.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numIrime.Value), Convert.ToDecimal(LMC020C.IRIME_NUM), Convert.ToDecimal(LMC020C.IRIME_MAX)) = False Then
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E014", New String() {.lblIrime.TextValue, LMC020C.IRIME_NUM, Convert.ToDecimal(LMC020C.IRIME_MAX).ToString("#,##0")})
                        'MyBase.ShowMessage("E014", New String() {"入目", LMC020C.IRIME_NUM, Convert.ToDecimal(LMC020C.IRIME_MAX).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numIrime)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .numPkgCnt.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '梱包個数
                If Me.IsHaniCheck(Convert.ToDecimal(.numPkgCnt.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                    'メッセージの表示
                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.ShowMessage("E685", New String() {LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                    'MyBase.ShowMessage("E014", New String() {"出荷(中)の梱包個数", LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                    Me._Vcon.SetErrorControl(.numPkgCnt)
                    Return False
                End If
            End If

            If .numKonsu.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '梱数
                .numKonsu.ItemName() = .lblKonsu.TextValue
                If String.IsNullOrEmpty(.numKonsu.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numKonsu.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                        '英語化対応
                        '20151021 tsunehira add
                        MyBase.ShowMessage("E014", New String() {.lblKonsu.TextValue, LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                        'MyBase.ShowMessage("E014", New String() {"梱数", LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numKonsu)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .numHasu.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '端数
                .numHasu.ItemName() = .lblHasu.TextValue
                If String.IsNullOrEmpty(.numHasu.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numHasu.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                        MyBase.ShowMessage("E014", New String() {.lblHasu.TextValue, LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                        'MyBase.ShowMessage("E014", New String() {"端数", LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.NB_MAX_10).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numHasu)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '個数
                .numSouKosu.ItemName() = .lblKosu.TextValue
                If String.IsNullOrEmpty(.numSouKosu.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numSouKosu.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E167", New String() {.lblKosu.TextValue})
                        'MyBase.ShowMessage("E167", New String() {"個数"})
                        .numHasu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numKonsu)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If
            If (LMC020C.EventShubetsu.HIKIATE.Equals(eventShubetsu)) = True Then
                '個数
                .numSouKosu.ItemName() = .lblKosu.TextValue
                'START YANAI 20110913 小分け対応
                ''START YANAI 20110906 サンプル対応
                ''If String.IsNullOrEmpty(.numSouKosu.TextValue) = True OrElse _
                ''    0 = Convert.ToDecimal(.numSouKosu.Value) Then
                'If (String.IsNullOrEmpty(.numSouKosu.TextValue) = True OrElse _
                '    0 = Convert.ToDecimal(.numSouKosu.Value)) AndAlso _
                '    .optSample.Checked = False Then
                ''END YANAI 20110906 サンプル対応
                If (String.IsNullOrEmpty(.numSouKosu.TextValue) = True OrElse
                    0 = Convert.ToDecimal(.numSouKosu.Value)) AndAlso
                    (.optKowake.Checked = False AndAlso
                     .optSample.Checked = False) Then
                    'END YANAI 20110913 小分け対応
                    MyBase.ShowMessage("E311")
                    .numHasu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numKonsu)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                If .cmbSyukkaSyubetu.ReadOnly() AndAlso .cmbSyukkaSyubetu.SelectedValue.ToString() = "60" Then
                    MyBase.ShowMessage("E438", New String() {"分納出荷", "いいえ（手動引当）"})
                    Return False
                End If
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                (("01").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("02").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("03").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("14").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("08").Equals(.cmbPRINT.SelectedValue) = True) Then
                '引当済個数
                Dim outMRow2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                Dim outMMax As Integer = outMRow2.Length - 1
                Dim outMRow As DataRow = Nothing
                For i As Integer = 0 To outMMax
                    outMRow = outMRow2(i)
                    'START YANAI 20110913 小分け対応
                    'If "0" = outMRow.Item("ALCTD_NB").ToString Then
                    '    MyBase.ShowMessage("E173")
                    '    Me._Vcon.SetErrorControl(.numHikiateKosuSumi)
                    '    .tabMiddle.SelectTab(.tabGoods)
                    '    Return False
                    'End If

                    If ((.optKowake.Checked = False AndAlso .optSample.Checked = False) AndAlso "0" = outMRow.Item("ALCTD_NB").ToString) OrElse _
                        ((.optKowake.Checked = True OrElse .optSample.Checked = True) AndAlso "0.000" = outMRow.Item("ALCTD_QT").ToString) Then

                        '2012.11.28 要望番号1553 修正START
                        If (Convert.ToDecimal(outMRow.Item("IRIME").ToString) > Convert.ToDecimal(outMRow.Item("ALCTD_QT").ToString) AndAlso _
                          ("0" = outMRow.Item("OUTKA_TTL_NB").ToString) AndAlso ("0" = outMRow.Item("ALCTD_NB").ToString)) Then

                        Else
                            MyBase.ShowMessage("E173")
                            Me._Vcon.SetErrorControl(.numHikiateKosuSumi)
                            'START YANAI 要望番号495
                            '.tabMiddle.SelectTab(.tabGoods)
                            .tabMiddle.SelectTab(.tabUnso)
                            'END YANAI 要望番号495
                            Return False
                        End If
                        '2012.11.28 要望番号1553 修正END

                    End If

                    'END YANAI 20110913 小分け対応

                Next
            ElseIf (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                    String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                    (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                If String.IsNullOrEmpty(.numHikiateKosuSumi.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numHikiateKosuSumi.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then
                        'MyBase.ShowMessage("E014", New String() {"引当済個数", LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                        MyBase.ShowMessage("E014", New String() {.lblTitleHikiateKosuSumi.TextValue + .lblKosu.TextValue, LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numHikiateKosuSumi)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                (("01").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("02").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("03").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("14").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("08").Equals(.cmbPRINT.SelectedValue) = True) Then
                '引当残個数
                Dim outMRow2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                Dim outMMax As Integer = outMRow2.Length - 1
                Dim outMRow As DataRow = Nothing
                For i As Integer = 0 To outMMax
                    outMRow = outMRow2(i)
                    If 0 <> Convert.ToDecimal(outMRow.Item("BACKLOG_NB").ToString) Then
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E707")
                        'MyBase.ShowMessage("E172", New String() {"引当残個数"})
                        Me._Vcon.SetErrorControl(.numHikiateKosuZan)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                Next
            ElseIf (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                    String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                    (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                If String.IsNullOrEmpty(.numHikiateKosuZan.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numHikiateKosuZan.Value), LMC020C.KOSU_MIN_NUM, LMC020C.KOSU_MAX_NUM) = False Then

                        '英語化対応
                        '20151021 tsunehira add
                        MyBase.ShowMessage("E682", New String() {LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                        'MyBase.ShowMessage("E014", New String() {"引当残個数", LMC020C.KOSU_MIN, Convert.ToDecimal(LMC020C.KOSU_MAX).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numHikiateKosuZan)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .numSouSuryo.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '数量
                .numSouSuryo.ItemName() = .lblTitleSouSuryo.TextValue
                If String.IsNullOrEmpty(.numSouSuryo.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numSouSuryo.Value), LMC020C.QT_MIN_NUM, Convert.ToDecimal(LMC020C.QT_MAX_NUM)) = False Then
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E167", New String() {.lblTitleSouSuryo.TextValue})
                        'MyBase.ShowMessage("E167", New String() {"数量"})
                        Me._Vcon.SetErrorControl(.numSouSuryo)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                (("01").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("02").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("03").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("14").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("08").Equals(.cmbPRINT.SelectedValue) = True) Then
                '引当済数量
                Dim outMRow2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                Dim outMMax As Integer = outMRow2.Length - 1
                Dim outMRow As DataRow = Nothing
                For i As Integer = 0 To outMMax
                    outMRow = outMRow2(i)
                    If "0" = outMRow.Item("ALCTD_QT").ToString Then
                        MyBase.ShowMessage("E173")
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                Next

            ElseIf (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                    String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                    (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                If String.IsNullOrEmpty(.numHikiateSuryoSumi.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numHikiateSuryoSumi.Value), LMC020C.SURYO_MIN_NUM, Convert.ToDecimal(LMC020C.SURYO_MAX_NUM)) = False Then
                        MyBase.ShowMessage("E014", New String() {.lblTitleHikiateSuryoZan.TextValue + .lblTitleSouSuryo.TextValue, LMC020C.SURYO_MIN, Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                        'MyBase.ShowMessage("E014", New String() {"引当済数量", LMC020C.SURYO_MIN, Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numHikiateSuryoSumi)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                (("01").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("02").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("03").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("14").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("08").Equals(.cmbPRINT.SelectedValue) = True) Then
                '引当残数量
                '引当済数量
                Dim outMRow2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                Dim outMMax As Integer = outMRow2.Length - 1
                Dim outMRow As DataRow = Nothing
                For i As Integer = 0 To outMMax
                    outMRow = outMRow2(i)
                    If 0 <> Convert.ToDecimal(outMRow.Item("BACKLOG_QT").ToString) Then
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E708")
                        'MyBase.ShowMessage("E172", New String() {"引当残数量"})
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                Next
            ElseIf (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                    String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                    (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                    LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                If String.IsNullOrEmpty(.numHikiateSuryoZan.TextValue) = False Then
                    If Me.IsHaniCheck(Convert.ToDecimal(.numHikiateSuryoZan.Value), LMC020C.SURYO_MIN_NUM, Convert.ToDecimal(LMC020C.SURYO_MAX_NUM)) = False Then
                        '英語化対応
                        '20151021 tsunehira add
                        MyBase.ShowMessage("E683", New String() {LMC020C.SURYO_MIN, Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                        'MyBase.ShowMessage("E014", New String() {"引当残数量", LMC020C.SURYO_MIN, Convert.ToDecimal(LMC020C.SURYO_MAX).ToString("#,##0")})
                        Me._Vcon.SetErrorControl(.numHikiateSuryoZan)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .txtGoodsRemark.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '商品別注意事項
                .txtGoodsRemark.ItemName() = .lblGoodsRemark.TextValue
                .txtGoodsRemark.IsForbiddenWordsCheck() = True
                .txtGoodsRemark.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtGoodsRemark) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtGoodsRemark.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                    (LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True) AndAlso _
                    ("txtGoodsRemark").Equals(Me._Frm.ActiveControl.Name) = True Then
                '商品別注意事項
                .txtGoodsRemark.ItemName() = .lblGoodsRemark.TextValue
                .txtGoodsRemark.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtGoodsRemark) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            '2013.03.25  要望番号1959 START
            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.CHANGE_KOSU.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.CHANGE_SURYO.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then

                'EDIデータ出荷数変更不可フラグが"1"の場合のみ対象
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                                                                                 "' AND CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND SUB_KB = '56'"))

                If custDetailsDr.Length >= 1 AndAlso custDetailsDr(0).Item("SET_NAIYO").ToString().Equals("1") = True Then

                    ''引当済でかつ出荷個数 <> EDI個数 の場合はエラー
                    'If Convert.ToDecimal(.numHikiateKosuZan.TextValue) = 0 AndAlso _
                    '   Convert.ToDecimal(.lblEdiOutkaTtlNb.Value) <> 0 AndAlso _
                    '   Convert.ToDecimal(.numSouKosu.Value) <> Convert.ToDecimal(.lblEdiOutkaTtlNb.Value) Then
                    '    MyBase.ShowMessage("E121", New String() {"個数変更"})
                    '    .numSouKosu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    '    Me._Vcon.SetErrorControl(.numSouKosu)
                    '    .tabMiddle.SelectTab(.tabUnso)
                    '    Return False
                    'End If

                    ''引当済でかつ出荷数量 <> EDI数量 の場合はエラー
                    'If Convert.ToDecimal(.numHikiateSuryoZan.TextValue) = 0 AndAlso _
                    '   Convert.ToDecimal(.lblEdiOutkaTtlQt.Value) <> 0 AndAlso _
                    '   Convert.ToDecimal(.numSouSuryo.Value) <> Convert.ToDecimal(.lblEdiOutkaTtlQt.Value) Then
                    '    MyBase.ShowMessage("E121", New String() {"数量変更"})
                    '    .numSouSuryo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    '    Me._Vcon.SetErrorControl(.numSouSuryo)
                    '    .tabMiddle.SelectTab(.tabUnso)
                    '    Return False
                    'End If

                    '出荷個数 <> EDI個数 の場合はエラー(予定入力済でもチェックする)
                    If Convert.ToDecimal(.lblEdiOutkaTtlNb.Value) <> 0 AndAlso _
                       Convert.ToDecimal(.numSouKosu.Value) <> Convert.ToDecimal(.lblEdiOutkaTtlNb.Value) Then
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E700")
                        'MyBase.ShowMessage("E121", New String() {"個数変更"})
                        .numSouKosu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numSouKosu)
                        .tabMiddle.SelectTab(.tabUnso)
                        Return False
                    End If

                    '出荷数量 <> EDI数量 の場合はエラー(予定入力済でもチェックする)
                    If Convert.ToDecimal(.lblEdiOutkaTtlQt.Value) <> 0 AndAlso _
                       Convert.ToDecimal(.numSouSuryo.Value) <> Convert.ToDecimal(.lblEdiOutkaTtlQt.Value) Then
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E701")
                        'MyBase.ShowMessage("E121", New String() {"数量変更"})
                        .numSouSuryo.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numSouSuryo)
                        .tabMiddle.SelectTab(.tabUnso)
                        Return False
                    End If

                End If

            End If
            '2013.03.25  要望番号1959 END

            If .txtSagyoM1.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '作業（中）1
                .lblSagyoM1.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoM1.ItemName() = lgm.Selector({"作業コード(中)１", "Work Code(M)1", "작업코드(中)1", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM1.IsForbiddenWordsCheck() = True
                .txtSagyoM1.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoM1) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoM1.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_M01) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoM1)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoM1.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoM1.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoM1.TextValue) = False AndAlso _
                   (.txtSagyoM1.TextValue = .txtSagyoM2.TextValue OrElse _
                    .txtSagyoM1.TextValue = .txtSagyoM3.TextValue OrElse _
                    .txtSagyoM1.TextValue = .txtSagyoM4.TextValue OrElse _
                    .txtSagyoM1.TextValue = .txtSagyoM5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoM1)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoM1.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoM1").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoM1.ItemName() = lgm.Selector({"作業コード(中)１", "Work Code(M)1", "작업코드(中)1", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM1.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoM1) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoM2.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '作業（中）２
                .lblSagyoM2.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoM2.ItemName() = lgm.Selector({"作業コード(中)２", "Work Code(M)2", "작업코드(中)2", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM2.IsForbiddenWordsCheck() = True
                .txtSagyoM2.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoM2) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoM2.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_M02) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoM2)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoM2.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoM2.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoM2.TextValue) = False AndAlso _
                   (.txtSagyoM2.TextValue = .txtSagyoM3.TextValue OrElse _
                    .txtSagyoM2.TextValue = .txtSagyoM4.TextValue OrElse _
                    .txtSagyoM2.TextValue = .txtSagyoM5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoM2)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoM2.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoM2").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoM2.ItemName() = lgm.Selector({"作業コード(中)２", "Work Code(M)2", "작업코드(中)2", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM2.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoM2) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoM3.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '作業（中）３
                .lblSagyoM3.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .lblSagyoM3.ItemName() = lgm.Selector({"作業コード(中)３", "Work Code(M)3", "작업코드(中)3", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM3.IsForbiddenWordsCheck() = True
                .txtSagyoM3.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoM3) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoM3.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_M03) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoM3)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoM3.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoM3.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoM3.TextValue) = False AndAlso _
                   (.txtSagyoM3.TextValue = .txtSagyoM4.TextValue OrElse _
                    .txtSagyoM3.TextValue = .txtSagyoM5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoM3)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoM3.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoM3").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .lblSagyoM3.ItemName() = lgm.Selector({"作業コード(中)３", "Work Code(M)3", "작업코드(中)3", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM3.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoM3) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoM4.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '作業（中）４
                .lblSagyoM4.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .lblSagyoM4.ItemName() = lgm.Selector({"作業コード(中)４", "Work Code(M)4", "작업코드(中)4", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM4.IsForbiddenWordsCheck() = True
                .txtSagyoM4.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoM4) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoM4.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_M04) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoM4)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoM4.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoM4.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoM4.TextValue) = False AndAlso _
                   (.txtSagyoM4.TextValue = .txtSagyoM5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoM4)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoM4.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoM4").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .lblSagyoM4.ItemName() = lgm.Selector({"作業コード(中)４", "Work Code(M)4", "작업코드(中)4", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM4.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoM4) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoM5.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '作業（中）５
                .lblSagyoM5.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoM5.ItemName() = lgm.Selector({"作業コード(中)５", "Work Code(M)5", "작업코드(中)5", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM5.IsForbiddenWordsCheck() = True
                .txtSagyoM5.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoM5) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoM5.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_M05) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoM5)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoM5.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoM5.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoM5.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoM5").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoM5.ItemName() = lgm.Selector({"作業コード(中)５", "Work Code(M)5", "작업코드(中)5", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoM5.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoM5) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtDestSagyoM1.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '届先作業１
                .lblDestSagyoM1.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtDestSagyoM1.ItemName() = lgm.Selector({"届先作業コード１", "Delivery Work Code 1", "송달처 작업코드1", "中国語"})
                '2017/09/25 修正 李↑

                .txtDestSagyoM1.IsForbiddenWordsCheck() = True
                .txtDestSagyoM1.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtDestSagyoM1) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtDestSagyoM1.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_DESTM01) = False Then
                    Me._Vcon.SetErrorControl(.txtDestSagyoM1)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtDestSagyoM1.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtDestSagyoM1.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtDestSagyoM1.TextValue) = False AndAlso _
                   (.txtDestSagyoM1.TextValue = .txtDestSagyoM2.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtDestSagyoM1)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtDestSagyoM1.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtDestSagyoM1").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtDestSagyoM1.ItemName() = lgm.Selector({"届先作業コード１", "Delivery Work Code 1", "송달처 작업코드1", "中国語"})
                '2017/09/25 修正 李↑

                .txtDestSagyoM1.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtDestSagyoM1) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtDestSagyoM2.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '届先作業２
                .lblDestSagyoM2.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtDestSagyoM2.ItemName() = lgm.Selector({"届先作業コード２", "Delivery Work Code 2", "송달처 작업코드2", "中国語"})
                '2017/09/25 修正 李↑

                .txtDestSagyoM2.IsForbiddenWordsCheck() = True
                .txtDestSagyoM2.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtDestSagyoM2) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtDestSagyoM2.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_DESTM02) = False Then
                    Me._Vcon.SetErrorControl(.txtDestSagyoM2)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtDestSagyoM1.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtDestSagyoM2.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtDestSagyoM2.ReadOnly = False AndAlso String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtDestSagyoM2").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtDestSagyoM2.ItemName() = lgm.Selector({"届先作業コード２", "Delivery Work Code 2", "송달처 작업코드2", "中国語"})
                '2017/09/25 修正 李↑

                .txtDestSagyoM2.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtDestSagyoM2) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            'その他手配情報
            If .cmbTehaiKbn.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '運送手配
                .cmbTehaiKbn.ItemName() = .lblUnsomotoKbn.TextValue
                .cmbTehaiKbn.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbTehaiKbn) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .cmbTariffKbun.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'タリフ分類区分
                .cmbTariffKbun.ItemName() = .lblUnsoutehaiKbun.TextValue
                .cmbTariffKbun.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbTariffKbun) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .cmbSyaryoKbn.ReadOnly = False AndAlso _
                ("20").Equals(.cmbTariffKbun.SelectedValue) = True AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '車輌
                .cmbSyaryoKbn.ItemName() = .lblSyaryoKbn.TextValue
                .cmbSyaryoKbn.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbSyaryoKbn) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .cmbMotoCyakuKbn.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '元着払い
                '先方手配時は必須ではない。
                .cmbMotoCyakuKbn.ItemName() = .lblMotoCyakuKbn.TextValue
                .cmbMotoCyakuKbn.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbMotoCyakuKbn) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtUnsoCompanyCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '運送会社コード

                '2017/09/25 修正 李↓
                .txtUnsoCompanyCd.ItemName() = lgm.Selector({"運送会社コード", "Transport company code", "운송회사코드", "中国語"})
                '2017/09/25 修正 李↑

                If ("10").Equals(.cmbTehaiKbn.SelectedValue) = True Then
                    .txtUnsoCompanyCd.IsHissuCheck() = True
                End If
                .txtUnsoCompanyCd.IsForbiddenWordsCheck() = True
                .txtUnsoCompanyCd.IsByteCheck() = 5
                If MyBase.IsValidateCheck(.txtUnsoCompanyCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                If ("02").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                    ("08").Equals(.cmbPRINT.SelectedValue) = True Then

                    '2017/09/25 修正 李↓
                    .txtUnsoCompanyCd.ItemName() = lgm.Selector({"運送会社コード", "Transport company code", "운송회사코드", "中国語"})
                    '2017/09/25 修正 李↑

                    .txtUnsoCompanyCd.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.txtUnsoCompanyCd) = False Then
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .txtUnsoSitenCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '運送会社支店コード

                '2017/09/25 修正 李↓
                .txtUnsoSitenCd.ItemName() = lgm.Selector({"運送会社支店コード", "Transport company branch code", "운송회사지점코드", "中国語"})
                '2017/09/25 修正 李↑

                If ("10").Equals(.cmbTehaiKbn.SelectedValue) = True Then
                    .txtUnsoSitenCd.IsHissuCheck() = True
                End If
                .txtUnsoSitenCd.IsForbiddenWordsCheck() = True
                .txtUnsoSitenCd.IsByteCheck() = 3
                If MyBase.IsValidateCheck(.txtUnsoSitenCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                If ("02").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                    ("08").Equals(.cmbPRINT.SelectedValue) = True Then

                    '2017/09/25 修正 李↓
                    .txtUnsoSitenCd.ItemName() = lgm.Selector({"運送会社支店コード", "Transport company branch code", "운송회사지점코드", "中国語"})
                    '2017/09/25 修正 李↑

                    .txtUnsoSitenCd.IsHissuCheck() = True
                    If MyBase.IsValidateCheck(.txtUnsoSitenCd) = False Then
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .txtUnthinTariffCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '運送タリフ
                .txtUnthinTariffCd.ItemName() = .lblUnthinTariff.TextValue
                .txtUnthinTariffCd.IsForbiddenWordsCheck() = True
                .txtUnthinTariffCd.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtUnthinTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                If ("40").Equals(.cmbTariffKbun.SelectedValue) = True Then
                    If Me.IsYokoTariffExistChk(.txtUnthinTariffCd.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtUnthinTariffCd)
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E691", New String() {.txtUnthinTariffCd.TextValue})
                        'MyBase.ShowMessage("E079", New String() {"横持ちタリフマスタ", .txtUnthinTariffCd.TextValue})
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                Else
                    Dim custdr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                             "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND " _
                                             , "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND " _
                                             , "CUST_CD_S = '00' AND " _
                                             , "CUST_CD_SS = '00'"))

                    '運賃計算締め基準の値によって、チェック対象の日付を変更
                    Dim checkDate As String = String.Empty
                    If 0 <> custdr.Length Then
                        If ("01").Equals(custdr(0).Item("UNTIN_CALCULATION_KB")) = True Then
                            checkDate = .imdSyukkaYoteiDate.TextValue
                        Else
                            checkDate = .imdNounyuYoteiDate.TextValue
                        End If
                    End If

                    If Me.IsUnchinTariffExistChk(.txtUnthinTariffCd.TextValue, checkDate) = False Then
                        Me._Vcon.SetErrorControl(.txtUnthinTariffCd)
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E690", New String() {.txtUnthinTariffCd.TextValue})
                        'MyBase.ShowMessage("E079", New String() {"運賃タリフマスタ", .txtUnthinTariffCd.TextValue})
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If .cmbUnsoKazeiKbn.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '運送課税区分
                .cmbUnsoKazeiKbn.ItemName() = .lblTitleUnsoKazeiKbn.TextValue
                .cmbUnsoKazeiKbn.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbUnsoKazeiKbn) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtExtcTariffCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '割増タリフ
                .txtExtcTariffCd.ItemName() = .lblExtcTariff.TextValue
                .txtExtcTariffCd.IsForbiddenWordsCheck() = True
                .txtExtcTariffCd.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtExtcTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsWariTariffExistChk(.txtExtcTariffCd.TextValue) = False Then
                    Me._Vcon.SetErrorControl(.txtExtcTariffCd)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E693", New String() {.txtExtcTariffCd.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"割増タリフマスタ", .txtExtcTariffCd.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If .txtPayUnthinTariffCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '支払運送タリフ
                .txtPayUnthinTariffCd.ItemName() = .lblPayUnthinTariff.TextValue
                .txtPayUnthinTariffCd.IsForbiddenWordsCheck() = True
                .txtPayUnthinTariffCd.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtPayUnthinTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                If ("40").Equals(.cmbTariffKbun.SelectedValue) = True Then
                    If Me.IsShiYokoTariffExistChk(.txtPayUnthinTariffCd.TextValue) = False Then
                        Me._Vcon.SetErrorControl(.txtPayUnthinTariffCd)
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E696", New String() {.txtPayUnthinTariffCd.TextValue})
                        'MyBase.ShowMessage("E079", New String() {"支払横持ちタリフマスタ", .txtPayUnthinTariffCd.TextValue})
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                Else

                    'チェック対象の日付は出荷日とする
                    Dim checkDate As String = .imdSyukkaYoteiDate.TextValue

                    If Me.IsShiharaiTariffExistChk(.txtPayUnthinTariffCd.TextValue, checkDate) = False Then
                        Me._Vcon.SetErrorControl(.txtPayUnthinTariffCd)
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E695", New String() {.txtPayUnthinTariffCd.TextValue})
                        'MyBase.ShowMessage("E079", New String() {"支払運賃タリフマスタ", .txtPayUnthinTariffCd.TextValue})
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If .txtPayExtcTariffCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '支払割増タリフ
                .txtPayExtcTariffCd.ItemName() = .lblPayExtcTariff.TextValue
                .txtPayExtcTariffCd.IsForbiddenWordsCheck() = True
                .txtPayExtcTariffCd.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtPayExtcTariffCd) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsShiharaiWariTariffExistChk(.txtPayExtcTariffCd.TextValue) = False Then
                    Me._Vcon.SetErrorControl(.txtPayExtcTariffCd)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E697", New String() {.txtPayExtcTariffCd.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"支払割増タリフマスタ", .txtPayExtcTariffCd.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。


            '2014/01/22 輸出情報追加 START

            '輸出情報
            If .txtShipNm.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then

                '本船名
                .txtShipNm.ItemName() = .lblShipNm.TextValue
                .txtShipNm.IsForbiddenWordsCheck() = True
                .txtShipNm.IsByteCheck() = 50
                If MyBase.IsValidateCheck(.txtShipNm) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                '仕向地
                .txtDestination.ItemName() = .lblDestination.TextValue
                .txtDestination.IsForbiddenWordsCheck() = True
                .txtDestination.IsByteCheck() = 50
                If MyBase.IsValidateCheck(.txtDestination) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                'booking no.
                .txtBookingNo.ItemName() = .lblBookingNo.TextValue
                .txtBookingNo.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtBookingNo) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                'voyage no.
                .txtVoyageNo.ItemName() = .lblVoyageNo.TextValue
                .txtVoyageNo.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtVoyageNo) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                'shipperコード
                .txtShipperCd.ItemName() = .lblShipper.TextValue
                .txtShipperCd.IsForbiddenWordsCheck() = True
                .txtShipperCd.IsByteCheck() = 15
                If MyBase.IsValidateCheck(.txtShipperCd) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                '収納時間
                .txtStorageTestTime.ItemName() = .lblStorageTestTime.TextValue
                .txtStorageTestTime.IsForbiddenWordsCheck() = True
                .txtStorageTestTime.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtStorageTestTime) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                'コンテナ番号
                .txtContainerNo.ItemName() = .lblContainerNo.TextValue
                .txtContainerNo.IsForbiddenWordsCheck() = True
                .txtContainerNo.IsFullByteCheck() = 11
                If MyBase.IsValidateCheck(.txtContainerNo) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

                'コンテナ名
                .txtContainerNm.ItemName() = .lblContainerNm.TextValue
                .txtContainerNm.IsForbiddenWordsCheck() = True
                .txtContainerNm.IsByteCheck() = 20
                If MyBase.IsValidateCheck(.txtContainerNm) = False Then
                    .tabTop.SelectTab(.tabOutInfo)
                    Return False
                End If

            End If

            '2014/01/22 輸出情報追加 END

            If .txtSagyoL1.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '作業（大）1
                .lblSagyoL1.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoL1.ItemName() = lgm.Selector({"作業コード(大)１", "Work Code (L) 1", "작업코드(大)1", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL1.IsForbiddenWordsCheck() = True
                .txtSagyoL1.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoL1) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoL1.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_L01) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoL1)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoL1.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoL1.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoL1.TextValue) = False AndAlso _
                   (.txtSagyoL1.TextValue = .txtSagyoL2.TextValue OrElse _
                    .txtSagyoL1.TextValue = .txtSagyoL3.TextValue OrElse _
                    .txtSagyoL1.TextValue = .txtSagyoL4.TextValue OrElse _
                    .txtSagyoL1.TextValue = .txtSagyoL5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoL1)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoL1.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoL1").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoL1.ItemName() = lgm.Selector({"作業コード(大)１", "Work Code (L) 1", "작업코드(大)1", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL1.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoL1) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoL2.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '作業（大）２
                .lblSagyoL2.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoL2.ItemName() = lgm.Selector({"作業コード(大)２", "Work Code (L) 2", "작업코드(大)2", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL2.IsForbiddenWordsCheck() = True
                .txtSagyoL2.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoL2) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoL2.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_L02) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoL2)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoL2.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoL2.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoL2.TextValue) = False AndAlso _
                   (.txtSagyoL2.TextValue = .txtSagyoL3.TextValue OrElse _
                    .txtSagyoL2.TextValue = .txtSagyoL4.TextValue OrElse _
                    .txtSagyoL2.TextValue = .txtSagyoL5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoL2)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoL2.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoL2").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoL2.ItemName() = lgm.Selector({"作業コード(大)２", "Work Code (L) 2", "작업코드(大)2", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL2.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoL2) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoL3.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '作業（大）３
                .lblSagyoL3.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoL3.ItemName() = lgm.Selector({"作業コード(大)３", "Work Code (L) 3", "작업코드(大)3", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL3.IsForbiddenWordsCheck() = True
                .txtSagyoL3.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoL3) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoL3.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_L03) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoL3)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoL3.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoL3.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoL3.TextValue) = False AndAlso _
                   (.txtSagyoL3.TextValue = .txtSagyoL4.TextValue OrElse _
                    .txtSagyoL3.TextValue = .txtSagyoL5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoL3)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoL3.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoL3").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoL3.ItemName() = lgm.Selector({"作業コード(大)３", "Work Code (L) 3", "작업코드(大)3", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL3.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoL3) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoL4.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '作業（大）４
                .lblSagyoL4.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoL4.ItemName() = lgm.Selector({"作業コード(大)４", "Work Code (L) 4", "작업코드(大)4", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL4.IsForbiddenWordsCheck() = True
                .txtSagyoL4.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoL4) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoL4.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_L04) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoL4)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoL4.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoL4.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If

                '作業重複チェック
                If String.IsNullOrEmpty(.txtSagyoL4.TextValue) = False AndAlso _
                   (.txtSagyoL4.TextValue = .txtSagyoL5.TextValue) Then
                    Me._Vcon.SetErrorControl(.txtSagyoL4)
                    MyBase.ShowMessage("E131")
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoL4.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoL4").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoL4.ItemName() = lgm.Selector({"作業コード(大)４", "Work Code (L) 4", "작업코드(大)4", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL4.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoL4) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtSagyoL5.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '作業（大）５
                .lblSagyoL5.TextValue = String.Empty

                '2017/09/25 修正 李↓
                .txtSagyoL5.ItemName() = lgm.Selector({"作業コード(大)５", "Work Code (L) 5", "작업코드(大)5", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL5.IsForbiddenWordsCheck() = True
                .txtSagyoL5.IsFullByteCheck() = 5
                If MyBase.IsValidateCheck(.txtSagyoL5) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
                If Me.IsSagyoExistChk(Convert.ToString(.cmbEigyosyo.SelectedValue), .txtSagyoL5.TextValue, .txtCust_Cd_L.TextValue, LMC020C.SAGYO_L05) = False Then
                    Me._Vcon.SetErrorControl(.txtSagyoL5)
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E694", New String() {.txtSagyoL5.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"作業マスタ", .txtSagyoL5.TextValue})
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            ElseIf .txtSagyoL5.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoL5").Equals(Me._Frm.ActiveControl.Name) = True Then

                '2017/09/25 修正 李↓
                .txtSagyoL5.ItemName() = lgm.Selector({"作業コード(大)５", "Work Code (L) 5", "작업코드(大)5", "中国語"})
                '2017/09/25 修正 李↑

                .txtSagyoL5.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoL5) = False Then
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .sprDtl.Enabled = True AndAlso _
                (LMC020C.EventShubetsu.DEL_S.Equals(eventShubetsu) = True) Then
                'スプレッド
                arr = Nothing
                arr = Me.getCheckList(.sprDtl)
                If 0 = arr.Count Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
            End If

            '要望番号:1683 yamanaka 2013.03.04 START
            Dim msg As String = String.Empty

            If LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True _
               OrElse LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True _
               OrElse LMC020C.EventShubetsu.UNSO.Equals(eventShubetsu) = True Then
                '英語化対応
                '20151022 tsunehira add
                Select Case eventShubetsu
                    Case LMC020C.EventShubetsu.HENSHU
                        If String.IsNullOrEmpty(.lblShiharaiGroupNo.TextValue) = False OrElse String.IsNullOrEmpty(.lblSeiqGroupNo.TextValue) = False Then
                            MyBase.ShowMessage("E709")
                            Return False
                        End If
                    Case LMC020C.EventShubetsu.DEL
                        If String.IsNullOrEmpty(.lblShiharaiGroupNo.TextValue) = False OrElse String.IsNullOrEmpty(.lblSeiqGroupNo.TextValue) = False Then
                            MyBase.ShowMessage("E710")
                            Return False
                        End If
                    Case LMC020C.EventShubetsu.UNSO
                        If String.IsNullOrEmpty(.lblShiharaiGroupNo.TextValue) = False OrElse String.IsNullOrEmpty(.lblSeiqGroupNo.TextValue) = False Then
                            MyBase.ShowMessage("E711")
                            Return False
                        End If
                End Select

                'Select Case eventShubetsu
                '    Case LMC020C.EventShubetsu.HENSHU
                '        msg = "編集"
                '    Case LMC020C.EventShubetsu.DEL
                '        msg = "削除"
                '    Case LMC020C.EventShubetsu.UNSO
                '        msg = "運送修正"
                'End Select

                'If String.IsNullOrEmpty(.lblShiharaiGroupNo.TextValue) = False OrElse String.IsNullOrEmpty(.lblSeiqGroupNo.TextValue) = False Then
                '    MyBase.ShowMessage("E232", New String() {"まとめ指示", msg})
                '    Return False
                'End If

            End If
            '要望番号:1683 yamanaka 2013.03.04 END

            '2015.07.08 協立化学　シッピングマーク対応 追加START

            'CASE_NO_FROM + CASE_NO_TO　大小チェック
            If Convert.ToDecimal(.numCaseNoFrom.Value) <> 0 AndAlso Convert.ToDecimal(.numCaseNoTo.Value) <> 0 AndAlso _
               Convert.ToDecimal(.numCaseNoFrom.Value) > Convert.ToDecimal(.numCaseNoTo.Value) Then
                MyBase.ShowMessage("E240", New String() {"CaseNo(FROM)", Convert.ToString(.numCaseNoTo.Value)})
                .numCaseNoFrom.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me._Vcon.SetErrorControl(.numCaseNoFrom)
                .tabMiddle.SelectTab(.TabPage2)
                Return False
            End If

            If .txtMarkInfo1.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報１
                .txtMarkInfo1.ItemName() = .lblMarkInfo1.Text
                .txtMarkInfo1.IsForbiddenWordsCheck() = True
                .txtMarkInfo1.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo1) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo2.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報２
                .txtMarkInfo2.ItemName() = .lblMarkInfo2.Text
                .txtMarkInfo2.IsForbiddenWordsCheck() = True
                .txtMarkInfo2.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo2) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo3.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報３
                .txtMarkInfo3.ItemName() = .lblMarkInfo3.Text
                .txtMarkInfo3.IsForbiddenWordsCheck() = True
                .txtMarkInfo3.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo3) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo4.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報４
                .txtMarkInfo4.ItemName() = .lblMarkInfo4.Text
                .txtMarkInfo4.IsForbiddenWordsCheck() = True
                .txtMarkInfo4.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo4) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo5.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報５
                .txtMarkInfo5.ItemName() = .lblMarkInfo5.Text
                .txtMarkInfo5.IsForbiddenWordsCheck() = True
                .txtMarkInfo5.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo5) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo6.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報６
                .txtMarkInfo6.ItemName() = .lblMarkInfo6.Text
                .txtMarkInfo6.IsForbiddenWordsCheck() = True
                .txtMarkInfo6.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo6) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo7.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報７
                .txtMarkInfo7.ItemName() = .lblMarkInfo7.Text
                .txtMarkInfo7.IsForbiddenWordsCheck() = True
                .txtMarkInfo7.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo7) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo8.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報８
                .txtMarkInfo8.ItemName() = .lblMarkInfo8.Text
                .txtMarkInfo8.IsForbiddenWordsCheck() = True
                .txtMarkInfo8.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo8) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo9.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報９
                .txtMarkInfo9.ItemName() = .lblMarkInfo9.Text
                .txtMarkInfo9.IsForbiddenWordsCheck() = True
                .txtMarkInfo9.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo9) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If .txtMarkInfo10.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'マーク情報１０
                .txtMarkInfo10.ItemName() = .lblMarkInfo10.Text
                .txtMarkInfo10.IsForbiddenWordsCheck() = True
                .txtMarkInfo10.IsByteCheck() = 200
                If MyBase.IsValidateCheck(.txtMarkInfo10) = False Then
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                (.cmbPRINT.SelectedValue.Equals("14") = True OrElse .cmbPRINT.SelectedValue.Equals("15") = True) Then
                Dim outMarkRow As DataRow() = ds.Tables(LMC020C.TABLE_NM_MARK_HED).Select("SYS_DEL_FLG = '0'")
                'Dim outMarkMax As Integer = outMarkRow.Length - 1
                'For i As Integer = 0 To outMarkMax
                If outMarkRow.Length = 0 Then
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E720")
                    'MyBase.ShowMessage("E340", New String() {"マーク情報", msg})
                    .tabMiddle.SelectTab(.TabPage2)
                    Return False
                End If

                'Next

            End If

            '2015.07.08 協立化学　シッピングマーク対応 追加END

            'タブレット対応
            '作業L1
            If .txtSagyoRemarkL1.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkL1.ItemName() = lgm.Selector({"作業備考(大)１", "Work Remark (L) 1", "작업 비고(大)1", "中国語"})
                .txtSagyoRemarkL1.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkL1.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkL1) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkL1.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL1").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkL1.ItemName() = lgm.Selector({"作業備考(大)１", "Work Remark (L) 1", "작업 비고(大)1", "中国語"})
                .txtSagyoRemarkL1.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkL1) = False Then
                    Return False
                End If
            End If
            '作業L2
            If .txtSagyoRemarkL2.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkL2.ItemName() = lgm.Selector({"作業備考(大)２", "Work Remark (L) 2", "작업 비고(大)2", "中国語"})
                .txtSagyoRemarkL2.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkL2.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkL2) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkL2.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL2").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkL2.ItemName() = lgm.Selector({"作業備考(大)２", "Work Remark (L) 2", "작업 비고(大)2", "中国語"})
                .txtSagyoRemarkL2.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkL2) = False Then
                    Return False
                End If
            End If
            '作業L3
            If .txtSagyoRemarkL3.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkL3.ItemName() = lgm.Selector({"作業備考(大)３", "Work Remark (L) 3", "작업 비고(大)3", "中国語"})
                .txtSagyoRemarkL3.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkL3.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkL3) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkL3.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL3").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkL3.ItemName() = lgm.Selector({"作業備考(大)３", "Work Remark (L) 3", "작업 비고(大)3", "中国語"})
                .txtSagyoRemarkL3.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkL3) = False Then
                    Return False
                End If
            End If
            '作業L4
            If .txtSagyoRemarkL4.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkL4.ItemName() = lgm.Selector({"作業備考(大)４", "Work Remark (L) 4", "작업 비고(大)4", "中国語"})
                .txtSagyoRemarkL4.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkL4.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkL4) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkL4.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL4").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkL4.ItemName() = lgm.Selector({"作業備考(大)４", "Work Remark (L) 4", "작업 비고(大)4", "中国語"})
                .txtSagyoRemarkL4.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkL4) = False Then
                    Return False
                End If
            End If
            '作業L5
            If .txtSagyoRemarkL5.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkL5.ItemName() = lgm.Selector({"作業備考(大)５", "Work Remark (L) 5", "작업 비고(大)5", "中国語"})
                .txtSagyoRemarkL5.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkL5.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkL5) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkL5.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL5").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkL5.ItemName() = lgm.Selector({"作業備考(大)５", "Work Remark (L) 5", "작업 비고(大)5", "中国語"})
                .txtSagyoRemarkL5.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkL5) = False Then
                    Return False
                End If
            End If

            '作業M1
            If .txtSagyoRemarkM1.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkM1.ItemName() = lgm.Selector({"作業備考(中)１", "Work Remark (M) 1", "작업 비고(中)1", "中国語"})
                .txtSagyoRemarkM1.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkM1.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkM1) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkM1.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL1").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkM1.ItemName() = lgm.Selector({"作業備考(中)１", "Work Remark (M) 1", "작업 비고(中)1", "中国語"})
                .txtSagyoRemarkM1.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkM1) = False Then
                    Return False
                End If
            End If
            '作業M2
            If .txtSagyoRemarkM2.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkM2.ItemName() = lgm.Selector({"作業備考(中)２", "Work Remark (M) 2", "작업 비고(中)2", "中国語"})
                .txtSagyoRemarkM2.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkM2.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkM2) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkM2.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL2").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkM2.ItemName() = lgm.Selector({"作業備考(中)２", "Work Remark (M) 2", "작업 비고(中)2", "中国語"})
                .txtSagyoRemarkM2.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkM2) = False Then
                    Return False
                End If
            End If
            '作業M3
            If .txtSagyoRemarkM3.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkM3.ItemName() = lgm.Selector({"作業備考(中)３", "Work Remark (M) 3", "작업 비고(中)3", "中国語"})
                .txtSagyoRemarkM3.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkM3.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkM3) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkM3.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL3").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkM3.ItemName() = lgm.Selector({"作業備考(中)３", "Work Remark (M) 3", "작업 비고(中)3", "中国語"})
                .txtSagyoRemarkM3.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkM3) = False Then
                    Return False
                End If
            End If
            '作業M4
            If .txtSagyoRemarkM4.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkM4.ItemName() = lgm.Selector({"作業備考(中)４", "Work Remark (M) 4", "작업 비고(中)4", "中国語"})
                .txtSagyoRemarkM4.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkM4.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkM4) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkM4.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL4").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkM4.ItemName() = lgm.Selector({"作業備考(中)４", "Work Remark (M) 4", "작업 비고(中)4", "中国語"})
                .txtSagyoRemarkM4.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkM4) = False Then
                    Return False
                End If
            End If
            '作業M5
            If .txtSagyoRemarkM5.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSagyoRemarkM5.ItemName() = lgm.Selector({"作業備考(中)５", "Work Remark (M) 5", "작업 비고(中)5", "中国語"})
                .txtSagyoRemarkM5.IsForbiddenWordsCheck() = True
                .txtSagyoRemarkM5.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtSagyoRemarkM5) = False Then
                    Return False
                End If
            ElseIf .txtSagyoRemarkM5.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSagyoRemarkL5").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSagyoRemarkM5.ItemName() = lgm.Selector({"作業備考(中)５", "Work Remark (M) 5", "작업 비고(中)5", "中国語"})
                .txtSagyoRemarkM5.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSagyoRemarkM5) = False Then
                    Return False
                End If
            End If
            '届先作業M1
            If .txtDestSagyoRemarkM1.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtDestSagyoRemarkM1.ItemName() = lgm.Selector({"届先作業備考１", "Delivery Work Remark 1", "송달처 작업 비고1", "中国語"})
                .txtDestSagyoRemarkM1.IsForbiddenWordsCheck() = True
                .txtDestSagyoRemarkM1.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtDestSagyoRemarkM1) = False Then
                    Return False
                End If
            ElseIf .txtDestSagyoRemarkM1.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtDestSagyoRemarkM1").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtDestSagyoRemarkM1.ItemName() = lgm.Selector({"届先作業備考１", "Delivery Work Remark 1", "송달처 작업 비고1", "中国語"})
                .txtDestSagyoRemarkM1.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtDestSagyoRemarkM1) = False Then
                    Return False
                End If
            End If
            '届先作業M2
            If .txtDestSagyoRemarkM2.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtDestSagyoRemarkM2.ItemName() = lgm.Selector({"届先作業備考２", "Delivery Work Remark 2", "송달처 작업 비고2", "中国語"})
                .txtDestSagyoRemarkM2.IsForbiddenWordsCheck() = True
                .txtDestSagyoRemarkM2.IsByteCheck() = 100
                If MyBase.IsValidateCheck(.txtDestSagyoRemarkM2) = False Then
                    Return False
                End If
            ElseIf .txtDestSagyoRemarkM2.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtDestSagyoRemarkM2").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtDestSagyoRemarkM2.ItemName() = lgm.Selector({"届先作業備考２", "Delivery Work Remark 2", "송달처 작업 비고2", "中国語"})
                .txtDestSagyoRemarkM2.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtDestSagyoRemarkM2) = False Then
                    Return False
                End If
            End If

            '現場注意事項
            If .txtSijiRemark.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                .txtSijiRemark.ItemName() = .lblSijiRemark.TextValue
                .txtSijiRemark.IsForbiddenWordsCheck() = True
                .txtSijiRemark.IsByteCheck = 200
                If MyBase.IsValidateCheck(.txtSijiRemark) = False Then
                    Return False
                End If
            ElseIf .txtSijiRemark.ReadOnly = False AndAlso _
                LMC020C.EventShubetsu.MASTER.Equals(eventShubetsu) = True AndAlso _
                ("txtSijiRemark").Equals(Me._Frm.ActiveControl.Name) = True Then
                .txtSijiRemark.ItemName() = .lblSijiRemark.TextValue
                .txtSijiRemark.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtSijiRemark) = False Then
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 単項目入力チェック（ワーニング）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleWorningCheck(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal ds As DataSet, ByVal nowDate As String) As Boolean

        Dim arr As ArrayList = Nothing


        '【単項目チェック】
        With Me._Frm

            If .imdSyukkaYoteiDate.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '出荷予定日
                .imdSyukkaYoteiDate.ItemName() = .lblSyukkaYoteiDate.TextValue
                .imdSyukkaYoteiDate.IsHissuCheck() = True

                If (.imdSyukkaYoteiDate.TextValue) < nowDate Then
                    If MyBase.ShowMessage("W116") = MsgBoxResult.Cancel Then
                        Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                        Return False
                    End If
                End If
            End If

            If .cmbNiyaku.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                '荷役料が"無"の場合、ワーニング
                If ("00").Equals(.cmbNiyaku.SelectedValue) = True Then
                    If MyBase.ShowMessage("W284") = MsgBoxResult.Cancel Then
                        Me._Vcon.SetErrorControl(.cmbNiyaku)
                        Return False
                    End If
                End If
            End If

            If .txtOrderType.ReadOnly = False AndAlso LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                'オーダータイプ
                .txtOrderType.ItemName() = .lblOrderType.TextValue
                .txtOrderType.IsForbiddenWordsCheck() = True
                .txtOrderType.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtOrderType) = False Then
                    If MyBase.ShowMessage("W110") = MsgBoxResult.Cancel Then
                        Me._Vcon.SetErrorControl(.txtOrderType)
                        Return False
                    End If
                End If
            End If

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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal ds As DataSet) As Boolean

        Dim value As Decimal = 0
        Dim dr As DataRow() = Nothing
        'START YANAI 20110913 小分け対応
        Dim outMDr As DataRow() = Nothing
        Dim outSDr As DataRow() = Nothing
        Dim arr As ArrayList = Nothing
        'END YANAI 20110913 小分け対応
        Dim max2 As Integer = 0

        '【関連項目チェック】
        With Me._Frm

            '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック処理を削除
            'If LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
            '    LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True OrElse _
            '    LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True OrElse _
            '    LMC020C.EventShubetsu.FUKUSHA.Equals(eventShubetsu) = True Then
            '    If Convert.ToString(.cmbEigyosyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString() Then
            '        '営業所＋自営業所
            '        Dim msg As String = String.Empty
            '        If LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True Then
            '            msg = "編集"
            '        ElseIf LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True Then
            '            msg = "削除"
            '        ElseIf LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
            '            msg = "完了取消"
            '        ElseIf LMC020C.EventShubetsu.FUKUSHA.Equals(eventShubetsu) = True Then
            '            msg = "複写"
            '        End If

            '        MyBase.ShowMessage("E178", New String() {msg})
            '        Me._Vcon.SetErrorControl(.cmbEigyosyo)
            '        Return False
            '    End If
            'End If




            '要望番号:0919（報告済みの場合も、完了取消が出来るように変更) 2013/01/31 本明 Start
            If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
                'ステータスが報告済の時
                If ("90").Equals(.cmbSagyoSintyoku.SelectedValue) Then
                    '出庫のEDI荷主で実績報告する荷主は完了取消できてはいけない
                    Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                        "CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                        "CUST_CD_M = '", .txtCust_Cd_M.TextValue, "' AND ", _
                                                                                                        "INOUT_KB = '0' AND ", _
                                                                                                        "FLAG_01 <> '0'") _
                                                                                                       )
                    '出庫のEDI荷主で実績報告する荷主の場合エラー
                    If drs.Length >= 1 Then
                        Dim msg As String = String.Empty
                        MyBase.ShowMessage("E533", New String() {msg})
                        Return False
                    End If
                End If

            End If
            '要望番号:0919（報告済みの場合も、完了取消が出来るように変更) 2013/01/31 本明 End

            '2017/06/21 作業データ確定のものがあったら、出荷完了取消できないようにするチェックStart
            If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
                For i As Integer = 0 To ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows.Count - 1
                    If ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows(i).Item("SKYU_CHK").ToString = "01" Then
                        Dim msg As String = "作業確定済みのデータがある(作業管理番号：" & ds.Tables(LMC020C.TABLE_NM_SAGYO).Rows(i).Item("SAGYO_REC_NO").ToString & ")"
                        MyBase.ShowMessage("E237", New String() {msg})
                        Return False
                    End If
                Next
            End If
            '2017/06/21 作業データ確定のものがあったら、出荷完了取消できないようにするチェックEnd

            'START YANAI 要望番号573
            If LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True OrElse _
               LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then

                '追加開始 --- 2014.09.22 kikuchi
                '特定荷主の場合、エラーチェックスキップ(FFEM)
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), _
                                                                                                         "' AND CUST_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("CUST_CD_L").ToString(), _
                                                                                                         "' AND SUB_KB = '82'"))
                If custDetailsDr.Length = 0 Then

                    'EDIで作成されたデータの場合は、削除、完了取消できてはいけない
                    'START YANAI 20110913 小分け対応
                    'Dim outMdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                    '                                                                                 "EDI_FLG = '1' AND ", _
                    '                                                                                 "SYS_DEL_FLG = '0'"))
                    outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                    "(JISSEKI_FLAG = '1' OR JISSEKI_FLAG = '2') AND ", _
                                                                                    "EDI_FLG = '1' AND ", _
                                                                                    "SYS_DEL_FLG = '0'"))
                    'END YANAI 20110913 小分け対応
                    If 0 < outMDr.Length Then
                        Dim msg As String = String.Empty
                        If LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True Then
                            MyBase.ShowMessage("E426", New String() {.FunctionKey.F4ButtonName})
                            'msg = "削除"
                        ElseIf LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
                            MyBase.ShowMessage("E426", New String() {.FunctionKey.F6ButtonName})
                            'msg = "完了取消"
                        End If

                        'MyBase.ShowMessage("E426", New String() {msg})
                        Return False
                    End If
                End If

            End If
            'END YANAI 要望番号573

            If (.imdSyukkaYoteiDate.ReadOnly = False OrElse .imdSyukkaDate.ReadOnly = False) AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .imdSyukkaYoteiDate.TextValue < .imdSyukkaDate.TextValue Then
                    '出庫日＋出荷予定日
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E166", New String() {.lblSyukkaYoteiDate.TextValue, .lblSyukkaDate.TextValue})
                    'MyBase.ShowMessage("E166", New String() {"出荷予定日", "出庫日"})
                    .imdSyukkaYoteiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.imdSyukkaDate)
                    Return False
                Else
                    .imdSyukkaYoteiDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    .imdSyukkaDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            If (.imdSyukkaYoteiDate.ReadOnly = False) AndAlso _
                String.IsNullOrEmpty(.imdHokanEndDate.TextValue) = False AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                If .imdSyukkaYoteiDate.TextValue < .imdHokanEndDate.TextValue Then
                    '出荷予定日 + 保管料終算日
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E166", New String() {.lblSyukkaYoteiDate.TextValue, .lblHokanEndDate.TextValue})
                    'MyBase.ShowMessage("E166", New String() {"出荷予定日", "保管料終算日"})
                    If .imdHokanEndDate.ReadOnly = False Then
                        .imdHokanEndDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    End If
                    Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                    Return False
                Else
                    .imdSyukkaYoteiDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            'START YANAI 要望番号837
            'If (.imdNounyuYoteiDate.ReadOnly = False OrElse .cmbTariffKbun.ReadOnly = False) AndAlso _
            '    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
            '    ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
            If (.imdNounyuYoteiDate.ReadOnly = False OrElse .cmbTariffKbun.ReadOnly = False) AndAlso _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False AndAlso _
                ("01").Equals(.cmbTodokesaki.SelectedValue) = False Then
                'END YANAI 要望番号837
                If ("20").Equals(.cmbTariffKbun.SelectedValue) = False AndAlso _
                    String.IsNullOrEmpty(.imdNounyuYoteiDate.TextValue) Then
                    '納入予定日＋タリフ分類
                    '20151021 tsunehira add
                    MyBase.ShowMessage("E001", New String() {_Frm.lblNounyuYoteiDate.TextValue})
                    Me._Vcon.SetErrorControl(.imdNounyuYoteiDate)
                    Return False
                Else
                    .imdNounyuYoteiDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                End If
            End If

            '納入予定日に値が入力されている時のみチェックを行う。
            If String.IsNullOrEmpty(.imdNounyuYoteiDate.TextValue) = False Then

#If True Then   'ADD 2018/10/09 依頼番号 : 002190   【LMS】出荷編集画面_出荷予定日＞納入日でも出荷登録不可とする(千葉角田)◎玉野・大極Team◎ 
                If (.imdNounyuYoteiDate.ReadOnly = False OrElse .imdSyukkaYoteiDate.ReadOnly = False) AndAlso
                    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True Then
                    If .imdNounyuYoteiDate.TextValue < .imdSyukkaYoteiDate.TextValue Then
                        '納入予定日＋出荷予定日
                        MyBase.ShowMessage("E166", New String() { .lblNounyuYoteiDate.TextValue, .lblSyukkaYoteiDate.TextValue})
                        'MyBase.ShowMessage("E166", New String() {"納入予定日", "出荷予定日"})
                        .imdSyukkaYoteiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.imdNounyuYoteiDate)
                        Return False

                    End If
                End If

#End If

                'START YANAI 要望番号837
                'If (.imdNounyuYoteiDate.ReadOnly = False OrElse .cmbTariffKbun.ReadOnly = False OrElse .imdSyukkaYoteiDate.ReadOnly = False) AndAlso _
                '     LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                '     ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                If (.imdNounyuYoteiDate.ReadOnly = False OrElse .cmbTariffKbun.ReadOnly = False OrElse .imdSyukkaYoteiDate.ReadOnly = False) AndAlso
                     LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso
                     ("20").Equals(.cmbTehaiKbn.SelectedValue) = False AndAlso
                     ("01").Equals(.cmbTodokesaki.SelectedValue) = False Then
                    'END YANAI 要望番号837
                    If ("20").Equals(.cmbTariffKbun.SelectedValue) = False AndAlso
                        .imdNounyuYoteiDate.TextValue < .imdSyukkaYoteiDate.TextValue Then
                        '納入予定日＋タリフ分類＋出荷予定日
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E166", New String() { .lblNounyuYoteiDate.TextValue, .lblSyukkaYoteiDate.TextValue})
                        'MyBase.ShowMessage("E166", New String() {"納入予定日", "出荷予定日"})
                        .imdNounyuYoteiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                        Return False
                    Else
                        .imdSyukkaYoteiDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                        .imdNounyuYoteiDate.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor
                    End If
                End If
            End If

            If LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) Then
                If (.cmbSyukkaSyubetu.ReadOnly() = False) AndAlso
                    .cmbSyukkaSyubetu.SelectedValue.ToString() = "60" AndAlso .cmbJikkou.SelectedValue.ToString() <> "04" Then
                    MyBase.ShowMessage("E336", New String() {"「分納出荷」の「実行」からの「保存」", String.Concat(.lblSyukkaSyubetu.Text, "に", .cmbSyukkaSyubetu.TextValue, "は選択")})
                    .cmbSyukkaSyubetu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.cmbSyukkaSyubetu)
                    Return False
                End If
            End If

            '★★★保管・荷役料最終計算日チェック
            If LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
               LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True OrElse _
               LMC020C.EventShubetsu.SHUSAN.Equals(eventShubetsu) = True Then

                Dim str As String = String.Empty
                Select Case eventShubetsu
                    Case LMC020C.EventShubetsu.HENSHU
                        str = _Frm.FunctionKey.F2ButtonName
                    Case LMC020C.EventShubetsu.DEL
                        str = _Frm.FunctionKey.F4ButtonName
                    Case LMC020C.EventShubetsu.SHUSAN
                        str = _Frm.FunctionKey.F8ButtonName
                End Select

                If Me._H.IsHokanryoChk(Me._Frm, str) = False Then
                    Return False
                End If
            End If

            If .numKonsu.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '梱数＋端数＋入数
                If 0 <> Convert.ToDecimal(.numKonsu.Value) AndAlso _
                    Convert.ToDecimal(.numIrisu.Value) <= Convert.ToDecimal(.numHasu.Value) Then
                    MyBase.ShowMessage("E218")
                    .numIrisu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .numHasu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numHasu)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If (.numSouSuryo.ReadOnly = False OrElse .numIrime.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                If .optAmt.Checked = True Then
                    '数量＋入目
                    value = Convert.ToDecimal _
                        (Convert.ToDecimal(.numSouSuryo.Value) * 1000) Mod _
                        (Convert.ToDecimal(.numIrime.Value) * 1000)
                    If 0 <> value Then
                        MyBase.ShowMessage("E170", New String() {String.Empty})
                        .numIrime.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numSouSuryo)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If (.numSouSuryo.ReadOnly = False OrElse .numIrime.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                If .optKowake.Checked = True OrElse .optSample.Checked = True Then
                    '数量＋入目
                    If Convert.ToDecimal(.numIrime.Value) < Convert.ToDecimal(.numSouSuryo.Value) Then
                        MyBase.ShowMessage("E191")
                        .numIrime.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numSouSuryo)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '個数＋引当済個数
                If Convert.ToDecimal(.numSouKosu.Value) < Convert.ToDecimal(.numHikiateKosuSumi.Value) Then
                    MyBase.ShowMessage("E169")
                    .numHikiateKosuSumi.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    .numHasu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numKonsu)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '数量＋引当済数量
                If Convert.ToDecimal(.numSouSuryo.Value) < Convert.ToDecimal(.numHikiateSuryoSumi.Value) Then
                    MyBase.ShowMessage("E169")
                    .numHikiateSuryoSumi.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numSouSuryo)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabGoods)
                    .tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If .txtUnsoCompanyCd.ReadOnly = False AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '運送会社＋運送会社支店コード
                If Me.IsUnsoExistChk(.txtUnsoCompanyCd.TextValue, .txtUnsoSitenCd.TextValue) = False Then
                    '20151030 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E834", New String() {String.Concat(.txtUnsoCompanyCd.TextValue, "-", .txtUnsoSitenCd.TextValue)})
                    '20151030 tsunehira add End
                    'MyBase.ShowMessage("E079", New String() {"運送会社マスタ", String.Concat(.txtUnsoCompanyCd.TextValue, "-", .txtUnsoSitenCd.TextValue)})
                    .txtUnsoSitenCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtUnsoCompanyCd)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If (.cmbTehaiKbn.ReadOnly = False OrElse .txtUnsoCompanyCd.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) AndAlso _
                ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                '運送手配＋運送会社＋運送会社支店コード
                If ("10").Equals(.cmbTehaiKbn.SelectedValue) AndAlso _
                    (String.IsNullOrEmpty(.txtUnsoCompanyCd.TextValue) = True OrElse _
                     String.IsNullOrEmpty(.txtUnsoSitenCd.TextValue) = True) Then
                    MyBase.ShowMessage("E001", New String() {_Frm.lblUnsoCompany.TextValue})
                    .txtUnsoSitenCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtUnsoCompanyCd)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            If (.cmbTariffKbun.ReadOnly = False OrElse .imdSyukkaYoteiDate.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                If (("10").Equals(.cmbTariffKbun.SelectedValue) OrElse _
                        ("20").Equals(.cmbTariffKbun.SelectedValue) OrElse _
                        ("30").Equals(.cmbTariffKbun.SelectedValue)) Then
                    'タリフ分類・出荷日、タリフ適用開始日
                    Dim tariffDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(String.Concat("UNCHIN_TARIFF_CD = '", .txtUnthinTariffCd.TextValue, "' AND ", _
                                                                                                                                 "STR_DATE > '", .imdSyukkaYoteiDate.TextValue, "'"))
                    If 0 <> tariffDr.Length Then
                        '2015.10.22 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E706")
                        'MyBase.ShowMessage("E166", New String() {.lblSyukkaYoteiDate.TextValue, "運賃タリフの適用開始日"})
                        Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                        Return False
                    End If
                End If
            End If

            '2014/01/22 輸出情報追加 START
            If (.txtShipperCd.ReadOnly = False) AndAlso _
               (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then

                Dim strDestCd As String = .txtShipperCd.TextValue
                If String.IsNullOrEmpty(strDestCd) = False Then

                    '輸出者コードの存在チェック
                    Dim destMstDs As MDestDS = New MDestDS
                    Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
                    destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
                    destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
                    destMstDr.Item("DEST_CD") = strDestCd
                    destMstDr.Item("SYS_DEL_FLG") = "0"
                    destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
                    Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
                    Dim drstDrs As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select

                    If drstDrs Is Nothing OrElse drstDrs.Length = 0 Then
                        '英語化対応
                        '20151021 tsunehira add
                        MyBase.ShowMessage("E698", New String() {.txtShipperCd.TextValue})
                        'MyBase.ShowMessage("E079", New String() {"届先マスタ", .txtShipperCd.TextValue})
                        Me._Vcon.SetErrorControl(.txtShipperCd)
                        Return False
                    End If

                End If

            End If
            '2014/01/22 輸出情報追加 END

            'START YANAI 要望番号1036
            'If (.cmbTakkyuSize.ReadOnly = False OrElse .txtUnthinTariffCd.ReadOnly = False) AndAlso _
            '    (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
            '    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
            '    LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
            '    LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
            If (.cmbTakkyuSize.ReadOnly = False OrElse .txtUnthinTariffCd.ReadOnly = False) AndAlso _
                ((LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True AndAlso .sprSyukkaM.ActiveSheet.Rows.Count > 0) OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                (LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True AndAlso .sprSyukkaM.ActiveSheet.Rows.Count > 0) OrElse _
                (LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True AndAlso .sprSyukkaM.ActiveSheet.Rows.Count > 0)) Then
                'END YANAI 要望番号1036
                '宅急便サイズ
                Dim tariffDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(String.Concat("UNCHIN_TARIFF_CD = '", .txtUnthinTariffCd.TextValue, "' AND ", _
                                                                                                                             "STR_DATE <= '", .imdSyukkaYoteiDate.TextValue, "'"))
                If 0 <> tariffDr.Length Then
                    '現在表示している画面の値に対して行う
                    If ("06").Equals(tariffDr(0).Item("TABLE_TP").ToString) = True AndAlso String.IsNullOrEmpty(.cmbTakkyuSize.TextValue) = True Then
                        MyBase.ShowMessage("E001", New String() {_Frm.lblTitleTakkyuSize.TextValue})
                        Me._Vcon.SetErrorControl(.cmbTakkyuSize)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                    If ("06").Equals(tariffDr(0).Item("TABLE_TP").ToString) = False AndAlso String.IsNullOrEmpty(.cmbTakkyuSize.TextValue) = False Then
                        '2015.10.22 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E702")
                        'MyBase.ShowMessage("E123", New String() {"選択された運送タリフの場合", "宅急便サイズ"})
                        Me._Vcon.SetErrorControl(.cmbTakkyuSize)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If
            If (.cmbTakkyuSize.ReadOnly = False OrElse .txtUnthinTariffCd.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '宅急便サイズ
                Dim tariffDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(String.Concat("UNCHIN_TARIFF_CD = '", .txtUnthinTariffCd.TextValue, "' AND ", _
                                                                                                                             "STR_DATE <= '", .imdSyukkaYoteiDate.TextValue, "'"))
                If 0 <> tariffDr.Length Then
                    '入力の仕方によっては出来てしまうため、以下でデータセット全部に対してチェックを行う。
                    Dim outMRow2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                    Dim outMRow As DataRow = Nothing
                    Dim max As Integer = outMRow2.Length - 1
                    For i As Integer = 0 To max
                        outMRow = outMRow2(i)
                        If (outMRow.Item("OUTKA_NO_M").ToString).Equals(.lblSyukkaMNo.TextValue) = True Then
                            Continue For
                        End If
                        If ("06").Equals(tariffDr(0).Item("TABLE_TP").ToString) = True AndAlso String.IsNullOrEmpty(outMRow.Item("SIZE_KB").ToString) = True Then
                            MyBase.ShowMessage("E681", New String() {outMRow.Item("OUTKA_NO_M").ToString})
                            Me._Vcon.SetErrorControl(.cmbTakkyuSize)
                            'START YANAI 要望番号495
                            '.tabMiddle.SelectTab(.tabGoods)
                            .tabMiddle.SelectTab(.tabUnso)
                            'END YANAI 要望番号495
                            Return False
                        End If
                        If ("06").Equals(tariffDr(0).Item("TABLE_TP").ToString) = False AndAlso String.IsNullOrEmpty(outMRow.Item("SIZE_KB").ToString) = False Then
                            '2015.10.22 tusnehira add
                            '英語化対応
                            MyBase.ShowMessage("E703", New String() {outMRow.Item("OUTKA_NO_M").ToString})
                            'MyBase.ShowMessage("E123", New String() {String.Concat("[出荷管理番号(中)=", outMRow.Item("OUTKA_NO_M"), "]選択された運送タリフの場合"), "宅急便サイズ"})
                            Me._Vcon.SetErrorControl(.cmbTakkyuSize)
                            'START YANAI 要望番号495
                            '.tabMiddle.SelectTab(.tabGoods)
                            .tabMiddle.SelectTab(.tabUnso)
                            'END YANAI 要望番号495
                            Return False
                        End If
                    Next
                End If
            End If

            If (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) Then
                '進捗区分
                If ("60").Equals(.cmbSagyoSintyoku.SelectedValue) OrElse _
                    ("90").Equals(.cmbSagyoSintyoku.SelectedValue) Then
                    '要望番号:612 Nakamura START 振替一括削除時はエラー回避
                    If String.IsNullOrEmpty(.lblFurikaeNo.TextValue) = True Then
                        MyBase.ShowMessage("E241")
                        Return False
                    End If
                    '要望番号:612 Nakamura END 振替一括削除時はエラー回避
                End If
            End If

            'START YANAI 要望番号497
            'If (LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) OrElse _
            '     (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True) OrElse _
            '     (LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True) OrElse _
            '     (LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True) Then
            If (LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) OrElse
                 (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True) OrElse
                 (LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True) Then
                'END YANAI 要望番号497
                '振替番号
                If String.IsNullOrEmpty(.lblFurikaeNo.TextValue) = False Then
                    '振替番号
                    If (LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) Then
                        '削除押下時
                        '要望番号:612 Nakamura START エラー→ワーニング
                        If String.IsNullOrEmpty(.lblFurikaeNo.TextValue) = False Then
                            '振替管理番号がある場合
                            If MyBase.ShowMessage("W221") = MsgBoxResult.Ok Then
                                Return True
                            Else
                                Return False
                            End If
                        End If
                        ''MyBase.ShowMessage("E162")
                        '要望番号:612 Nakamura END
                    ElseIf (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True) Then
                        '編集押下時
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E399", New String() { .FunctionKey.F2ButtonName, String.Empty})
                        'MyBase.ShowMessage("E399", New String() {"編集", String.Empty})
                    ElseIf (LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True) Then
                        '完了取消押下時
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E399", New String() { .FunctionKey.F6ButtonName, String.Empty})
                        'MyBase.ShowMessage("E399", New String() {"完了取消", String.Empty})
                        'START YANAI 要望番号497
                        'ElseIf (LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True) Then
                        '    '印刷押下時
                        '    MyBase.ShowMessage("E399", New String() {"印刷", String.Empty})
                        'END YANAI 要望番号497
                    End If
                    Return False
                End If
            End If

            If LMC020C.EventShubetsu.TORIKESHI.Equals(eventShubetsu) = True Then
                ' 完了取消時の次回分納チェック
                Dim msgParts As String() = Nothing
                Dim msgCd As String = TorikeshiKanrenCheck(ds, msgParts)
                If msgCd <> "" Then
                    Me.ShowMessage(msgCd, msgParts)
                    Return False
                End If
            End If

            If (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.UNSO.Equals(eventShubetsu) = True) Then
                '運賃データ
                Dim unchinMax As Integer = ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows.Count - 1
                For i As Integer = 0 To unchinMax
                    If ("01").Equals(ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows(i).Item("SEIQ_FIXED_FLAG")) = True Then
                        MyBase.ShowMessage("E126", New String() {String.Empty})
                        Return False
                    End If
                Next
            End If

            'START UMANO 要望番号1302 支払運賃に伴う修正。
            If (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.UNSO.Equals(eventShubetsu) = True) Then
                '支払運賃データ
                Dim shiharaiMax As Integer = ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows.Count - 1
                For i As Integer = 0 To shiharaiMax
                    If ("01").Equals(ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows(i).Item("SHIHARAI_FIXED_FLAG")) = True Then
                        MyBase.ShowMessage("E497", New String() {String.Empty})
                        Return False
                    End If
                Next
            End If
            'END UMANO 要望番号1302 支払運賃に伴う修正。

            'START YANAI No.7
            'If LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True Then
            If (LMC020C.EventShubetsu.HENSHU.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) Then
                'END YANAI No.7
                '作業データ
                Dim sagyoDr As DataRow() = ds.Tables(LMC020C.TABLE_NM_SAGYO).Select("SYS_DEL_FLG = '0'")
                Dim sagyoMax As Integer = sagyoDr.Length - 1
                For i As Integer = 0 To sagyoMax
                    If ("01").Equals(sagyoDr(i).Item("SKYU_CHK")) = True Then
                        MyBase.ShowMessage("E127")
                        Return False
                    End If
                Next
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True Then
                '進捗区分＋印刷
                'START YANAI 要望番号497
                'If ("06").Equals(.cmbPRINT.SelectedValue) = True AndAlso _
                '    (("60").Equals(.cmbSagyoSintyoku.SelectedValue) = False AndAlso _
                '     ("90").Equals(.cmbSagyoSintyoku.SelectedValue) = False) Then
                '    '出荷報告の時。出荷済、報告済以外はエラー
                '    MyBase.ShowMessage("E175", New String() {"出荷報告"})
                '    Me._Vcon.SetErrorControl(.cmbPRINT)
                '    Return False
                'End If
                'END YANAI 要望番号497

                If ("05").Equals(.cmbPRINT.SelectedValue) = True AndAlso _
                    (("10").Equals(.cmbSagyoSintyoku.SelectedValue) = True) Then
                    '分析票の時。出荷済、報告済はエラー
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E175", New String() {.chkCoa.TextValue})
                    'MyBase.ShowMessage("E175", New String() {"分析票"})
                    Me._Vcon.SetErrorControl(.cmbPRINT)
                    Return False
                End If

                'START YANAI 20120122 立会書印刷対応
                'If ("09").Equals(.cmbPRINT.SelectedValue) = True AndAlso _
                '    ("10").Equals(.cmbSagyoSintyoku.SelectedValue) = True Then
                '    '取消連絡の時。出荷予定はエラー
                '    MyBase.ShowMessage("E175", New String() {"取消連絡"})
                '    Me._Vcon.SetErrorControl(.cmbPRINT)
                '    Return False
                'End If
                'END YANAI 20120122 立会書印刷対応

            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                ("01").Equals(.cmbPRINT.SelectedValue) = True Then
                '荷札の時、運送会社マスタの荷札有無フラグをチェック
                Dim unchinMax As Integer = ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows.Count
                If 0 < unchinMax Then
                    If ("00").Equals(ds.Tables(LMC020C.TABLE_NM_UNSO_L).Rows(0).Item("NIHUDA_YN").ToString) = True Then
                        MyBase.ShowMessage("E411")
                        Me._Vcon.SetErrorControl(.cmbPRINT)
                        Return False
                    End If
                End If
            End If

            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                ("02").Equals(.cmbPRINT.SelectedValue) = True Then
                '送り状の時、運送会社荷主別送り状情報マスタ存在チェック
                If Me.IsUnsoCustRptExistChk(.txtUnsoCompanyCd.TextValue, .txtUnsoSitenCd.TextValue, Convert.ToString(.cmbMotoCyakuKbn.SelectedValue)) = False Then
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E689", New String() {String.Concat(.txtUnsoCompanyCd.TextValue, "-", .txtUnsoSitenCd.TextValue)})
                    'MyBase.ShowMessage("E079", New String() {"運送会社荷主別送り状情報マスタ", String.Concat(.txtUnsoCompanyCd.TextValue, "-", .txtUnsoSitenCd.TextValue)})
                    .txtUnsoSitenCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.txtUnsoCompanyCd)
                    'START YANAI 要望番号495
                    '.tabMiddle.SelectTab(.tabUnso)
                    'END YANAI 要望番号495
                    Return False
                End If
            End If

            Dim sprSmax As Integer = .sprDtl.ActiveSheet.Rows.Count - 1
            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = False Then
                '参照モード以外の場合
                For i As Integer = 0 To sprSmax
                    'スプレッド(小)すべてを対象にするチェック
                    If (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                        LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                        LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                        LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) AndAlso _
                        (("60").Equals(.cmbSagyoSintyoku.SelectedValue) = False AndAlso _
                        ("90").Equals(.cmbSagyoSintyoku.SelectedValue) = False) Then
                        '出荷予定日＋入荷日(小)
                        If .imdSyukkaYoteiDate.ReadOnly = False AndAlso _
                            .imdSyukkaYoteiDate.TextValue < Me._Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.INKO_DATE)).Replace("/", "") Then
                            '2015.10.22 tusnehira add
                            '英語化対応
                            MyBase.ShowMessage("E705", New String() {(i + 1).ToString})
                            'MyBase.ShowMessage("E166", New String() {"出荷予定日", String.Concat(i + 1, "行目の入荷日(小)")})
                            .sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.INKO_DATE).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                            Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                            Return False
                        End If

                        '引当中個数＋引当可能個数（更新前の保存値）
                        If Convert.ToDecimal(_Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALLOC_CAN_NB_HOZON))) + _
                            Convert.ToDecimal(_Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALCTD_NB_HOZON))) _
                            < Convert.ToDecimal(_Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALCTD_NB))) Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E718")
                            'Me._Vcon.SetErrMessage("E283", New String() {"引当可能個数", "引当中個数"})
                            Return False
                        End If

                        '実予在庫数
                        If Convert.ToDecimal(_Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.PORA_ZAI_NB))) < 0 Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E717", New String() {i.ToString})
                            'MyBase.ShowMessage("E283", New String() {"実予在庫数", String.Concat(i, "行目")})
                            .sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.PORA_ZAI_NB).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                            Return False
                        End If

                        '引当中個数
                        If Convert.ToDecimal(_Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALCTD_NB))) < 0 Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E716", New String() {i.ToString})
                            'MyBase.ShowMessage("E283", New String() {"引当中個数", String.Concat(i, "行目")})
                            .sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALCTD_NB).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                            Return False
                        End If

                        '引当可能個数
                        If Convert.ToDecimal(_Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALCTD_CAN_NB))) < 0 Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E715", New String() {i.ToString})
                            'MyBase.ShowMessage("E283", New String() {"引当可能個数", String.Concat(i, "行目")})
                            .sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ALCTD_CAN_NB).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                            Return False
                        End If

                        '要望番号:1866（小分けの在庫がずれる）対応　 2013/03/15 本明Start

                        ''START YANAI 要望番号692
                        ''同一出荷内小分けチェック
                        'If Me._Frm.optKowake.Checked = True Then
                        '    outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                        '                                                                    "OUTKA_NO_M <> '", Me._Frm.lblSyukkaMNo.TextValue, "' AND ", _
                        '                                                                    "ZAI_REC_NO = '", _Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ZAI_REC_NO)), "' AND ", _
                        '                                                                    "SYS_DEL_FLG = '0'"))
                        '    max2 = outMDr.Length - 1
                        '    For j As Integer = 0 To max2
                        '        outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                        '                                                                        "OUTKA_NO_M = '", outMDr(j).Item("OUTKA_NO_M").ToString, "' AND ", _
                        '                                                                        "ALCTD_KB = '03' AND ", _
                        '                                                                        "SYS_DEL_FLG = '0'"))
                        '        MyBase.ShowMessage("E435")
                        '        Return False
                        '    Next
                        'End If
                        ''END YANAI 要望番号692

                        '同一出荷内小分けチェック
                        If Convert.ToDouble(Me._Frm.numIrime.Value) > Convert.ToDouble(Me._Frm.numSouSuryo.Value) Then    '小分けの条件をオプションボタンの値でなく入り目>数量で判断する（全量出荷の場合があるので）

                            '削除＆追加もNGとする
                            outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                            "OUTKA_NO_M <> '", Me._Frm.lblSyukkaMNo.TextValue, "' AND ", _
                                                                                            "ZAI_REC_NO = '", _Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(i, LMC020C.sprDtlMColumnIndex.ZAI_REC_NO)), "'"))   '"SYS_DEL_FLG = '0'"の条件を削除
                            '不要なループを除く
                            If outMDr.Length > 0 Then
                                '2019/12/03 要望管理009191 add start
                                '入目と数量によってメッセージを決定する
                                Dim msg As String = String.Empty
                                Dim irime As Double = Convert.ToDouble(outMDr(0).Item("IRIME").ToString)
                                Dim qt As Double = Convert.ToDouble(outMDr(0).Item("OUTKA_TTL_QT").ToString)
                                If irime > qt Then
                                    '同一の在庫から一度に複数回の小分けをする事はできません
                                    msg = "E435"
                                Else
                                    '同一の在庫から一度に個数と小分けをする事はできません。
                                    msg = "E01S"
                                End If
                                '2019/12/03 要望管理009191 add end

                                outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                "OUTKA_NO_M = '", outMDr(0).Item("OUTKA_NO_M").ToString, "' AND ", _
                                                                                                "ALCTD_KB = '03' AND ", _
                                                                                                "SYS_DEL_FLG = '0'"))
                                '2019/12/03 要望管理009191 add start
                                'MyBase.ShowMessage("E435")
                                MyBase.ShowMessage(msg)
                                '2019/12/03 要望管理009191 add end
                                Return False
                            End If
                        End If
                        '要望番号:1866（小分けの在庫がずれる）対応　 2013/03/15 本明End


                    End If
                Next
            End If

            If (LMC020C.EventShubetsu.RIREKI.Equals(eventShubetsu) = True) Then
                '商品選択チェック
                If String.IsNullOrEmpty(.txtGoodsCdCust.TextValue) = True Then
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E199", New String() {.lblGoodsCdCust.TextValue})
                    'MyBase.ShowMessage("E199", New String() {"商品"})
                    Return False
                End If
            End If

            If .cmbSyaryoKbn.ReadOnly = False AndAlso _
                ("40").Equals(.cmbTariffKbun.SelectedValue) = True AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'タリフ分類区分 + 車輌 + 横持ちタリフ

                dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(String.Concat("YOKO_TARIFF_CD = '", .txtUnthinTariffCd.TextValue, "'"))
                If 0 < dr.Length Then

                    '車建ての場合、
                    If LMC020C.CALC_KB_KURUMA.Equals(dr(0).Item("CALC_KB").ToString()) = True Then

                        '車輌区分がない場合、エラー
                        '20151030 tsunehira add Start
                        '英語化対応
                        If Me.IsYokoSharyoChk("E835", "", "", True) = False Then
                            '20151030 tsunehira add End
                            'If Me.IsYokoSharyoChk("E187", "車建て", "車輌区分", True) = False Then
                            Return False
                        End If

                    Else

                        '車輌区分がある場合、エラー
                        '20151030 tsunehira add Start
                        '英語化対応
                        If Me.IsYokoSharyoChk("E836", "", "", False) = False Then
                            '20151030 tsunehira add End
                            'If Me.IsYokoSharyoChk("E211", "車建て以外", "車輌区分", False) = False Then
                            Return False
                        End If

                    End If

                End If

            End If

            'START YANAI メモ②No.20
            If (LMC020C.EventShubetsu.DEL_M.Equals(eventShubetsu) = True) Then
                '要望番号:1253 terakawa 2012.07.13 Start
                'Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND SUB_KB = '07'"))
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                                                                                 "' AND CUST_CD = '", .txtCust_Cd_L.TextValue, _
                                                                                                                                 "' AND SUB_KB = '07'"))
                '要望番号:1253 terakawa 2012.07.13 End
                If 0 < custDetailsDr.Length Then
                    If (LMConst.FLG.ON).Equals(custDetailsDr(0).Item("SET_NAIYO")) = True Then
                        '荷主明細マスタに該当荷主のデータがある場合のみチェックを行う

                        'チェックリスト取得
                        'START YANAI 20110913 小分け対応
                        'Dim arr As ArrayList = Nothing
                        'END YANAI 20110913 小分け対応
                        arr = Me._Hcon.GetCheckList(.sprSyukkaM.ActiveSheet, LMC020G.sprSyukkaM.DEFM.ColNo)
                        Dim sprMmax As Integer = arr.Count - 1
                        For i As Integer = 0 To sprMmax
                            'スプレッド(中)すべてを対象にするチェック
                            'EDI出荷にデータがあるかどうか取得
                            'START YANAI 20110913 小分け対応
                            'Dim outMdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                            '                "OUTKA_NO_L = '", .lblSyukkaLNo.TextValue, "' AND ", _
                            '                "OUTKA_NO_M = '", _Gcon.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC020C.sprSyukkaMColumnIndex.KANRI_NO)), "' AND ", _
                            '                "SYS_DEL_FLG = '0'"))
                            outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                            "OUTKA_NO_L = '", .lblSyukkaLNo.TextValue, "' AND ", _
                                            "OUTKA_NO_M = '", _Gcon.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC020C.sprSyukkaMColumnIndex.KANRI_NO)), "' AND ", _
                                            "SYS_DEL_FLG = '0'"))
                            'END YANAI 20110913 小分け対応
                            If 0 < outMDr.Length Then
                                If ("1").Equals(outMDr(0).Item("EDI_FLG").ToString()) = True Then
                                    MyBase.ShowMessage("E121", New String() {.FunctionKey.F4ButtonName})
                                    Return False
                                End If
                            End If
                        Next
                    End If
                End If
            End If
            'END YANAI メモ②No.20

            'START YANAI 20110913 小分け対応
            If (LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) Then
                outMDr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("OUTKA_NO_L2 <> '' AND SYS_DEL_FLG = '0'")
                '1件でもOUTKA_NO_L2が設定されている場合はエラー
                If 0 < outMDr.Length Then
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E414", New String() {.FunctionKey.F4ButtonName})
                    'MyBase.ShowMessage("E414", New String() {"削除"})
                    Return False
                End If
            End If

            If (LMC020C.EventShubetsu.DEL_M.Equals(eventShubetsu) = True) Then
                'チェックリスト取得
                arr = Me._Hcon.GetCheckList(.sprSyukkaM.ActiveSheet, LMC020G.sprSyukkaM.DEFM.ColNo)
                Dim sprMmax As Integer = arr.Count - 1
                For i As Integer = 0 To sprMmax
                    'スプレッド(中)すべてを対象にするチェック
                    outSDr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                    "OUTKA_NO_L = '", .lblSyukkaLNo.TextValue, "' AND ", _
                                    "OUTKA_NO_M = '", _Gcon.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC020C.sprSyukkaMColumnIndex.KANRI_NO)), "' AND ", _
                                    "OUTKA_NO_L2 <> '' AND ", _
                                    "SYS_DEL_FLG = '0'"))
                    If 0 < outSDr.Length Then
                        '1件でもOUTKA_NO_L2が設定されている場合はエラー
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E414", New String() {.btnROW_DEL_M.TextValue})
                        'MyBase.ShowMessage("E414", New String() {"行削除"})
                        Return False
                    End If
                Next
            End If

            If (LMC020C.EventShubetsu.DEL_S.Equals(eventShubetsu) = True) Then
                'チェックリスト取得
                arr = Me._Hcon.GetCheckList(.sprDtl.ActiveSheet, LMC020G.sprDtl.DEF.ColNo)
                sprSmax = arr.Count - 1
                For i As Integer = 0 To sprSmax
                    'スプレッド(小)すべてを対象にするチェック
                    outSDr = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                    "OUTKA_NO_L = '", .lblSyukkaLNo.TextValue, "' AND ", _
                                    "OUTKA_NO_M = '", .lblSyukkaMNo.TextValue, "' AND ", _
                                    "OUTKA_NO_S = '", _Gcon.GetCellValue(.sprDtl.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC020C.sprDtlMColumnIndex.SHO_NO)), "' AND ", _
                                    "OUTKA_NO_L2 <> '' AND ", _
                                    "SYS_DEL_FLG = '0'"))
                    If 0 < outSDr.Length Then
                        '1件でもOUTKA_NO_L2が設定されている場合はエラー
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E414", New String() {.btnROW_DEL_M.TextValue})
                        'MyBase.ShowMessage("E414", New String() {"行削除"})
                        Return False
                    End If
                Next
            End If

            'END YANAI 20110913 小分け対応

            'START YANAI 20120122 立会書印刷対応
            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
                (("03").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("08").Equals(.cmbPRINT.SelectedValue) = True OrElse _
                 ("09").Equals(.cmbPRINT.SelectedValue) = True) Then
                '印刷OKフラグの初期化
                .lblTachiai.TextValue = "00"

                '立会書の時、荷主明細マスタ存在チェック
                If Me.IsCustDetailsExistChk("13") = False Then
                    If ("09").Equals(.cmbPRINT.SelectedValue) = True Then
                        '印刷NGの場合
                        '2015.10.21 tusnehira add
                        '英語化対応
                        MyBase.ShowMessage("E692", New String() {String.Concat(.txtCust_Cd_L.TextValue)})
                        'MyBase.ShowMessage("E079", New String() {"荷主明細マスタ", String.Concat(.txtCust_Cd_L.TextValue)})
                        Return False
                    End If
                Else
                    '印刷OKフラグをオンにする
                    .lblTachiai.TextValue = "01"
                End If
            End If
            'END YANAI 20120122 立会書印刷対応

            'START YANAI 要望番号681
            If (DispMode.VIEW).Equals(.lblSituation.DispMode) = False AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '全量出荷小分け時の出荷単位チェック
                If ("01").Equals(.lblTaniKowake.TextValue) = True AndAlso _
                    .optCnt.Checked = False Then
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E754")
                    'MyBase.ShowMessage("E438", New String() {"小分け全量出荷", "個数"})
                    .tabMiddle.SelectTab(.tabUnso)
                    Return False
                End If
            End If
            'END YANAI 要望番号681

            If LMC020C.EventShubetsu.CHANGE_GOODS.Equals(eventShubetsu) = True Then
                'まだ選択されていな商品詳細の場合、チェック
                If String.IsNullOrEmpty(.lblSyukkaMNo.TextValue) = True Then
                    MyBase.ShowMessage("E441")
                    .tabMiddle.SelectTab(.tabUnso)
                    Return False
                End If

                'TODO:引当数のチェック　面倒なので後回し
                Dim where As String = String.Empty
                where = String.Concat("NRS_BR_CD ='", _Frm.cmbEigyosyo.SelectedValue, "' AND " _
                                             , "OUTKA_NO_L = '", _Frm.lblSyukkaLNo.TextValue, "' AND " _
                                             , "OUTKA_NO_M = '", _Frm.lblSyukkaMNo.TextValue, "'")

                If ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(where).Length <> 0 OrElse _
                   _Frm.sprDtl.ActiveSheet.RowCount <> 0 Then
                    MyBase.ShowMessage("E442")
                    .tabMiddle.SelectTab(.tabUnso)
                    Return False
                End If
            End If

            'START KIM 要望番号1504 2012/10/09
            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
               ("07").Equals(.cmbPRINT.SelectedValue) = True Then

                '2013.02.18 アグリマート対応 START
                Dim nrsBrCd As String = .cmbEigyosyo.SelectedValue.ToString()
                Dim custCdL As String = .txtCust_Cd_L.TextValue.ToString()
                Dim custCdM As String = .txtCust_Cd_M.TextValue.ToString()

                Dim custDtlDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
                                                                                                 "' AND CUST_CD = '", String.Concat(custCdL, custCdM), _
                                                                                                 "' AND SUB_KB = '67'"))
                If 0 = custDtlDr.Length Then

                    Dim outMRow2 As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
                    Dim outMMax As Integer = outMRow2.Length - 1
                    Dim outMRow As DataRow = Nothing

                    For i As Integer = 0 To outMMax

                        outMRow = outMRow2(i)
                        '20160114 要望番号:2488 tsunehira add start
                        Select Case outMRow.Item("ALCTD_KB").ToString
                            Case "03", "04" '03:小分けの場合 04:ｻﾝﾌﾟﾙ出荷の場合
                                If (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = False AndAlso _
                                    ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = False) OrElse _
                                   (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = True AndAlso _
                                    ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = True) Then '要望番号:1504 terakawa 条件追加 2012.10.19 

                                    MyBase.ShowMessage("E309", New String() {""})
                                    .cmbPRINT.Focus()
                                    Return False
                                End If

                            Case Else '01:個数引当 02:数量引当 
                                If (("0").Equals(outMRow.Item("OUTKA_TTL_NB").ToString) = False AndAlso
                                    ("0").Equals(outMRow.Item("BACKLOG_NB").ToString) = False) OrElse
                                   (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = False AndAlso
                                    ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = False) OrElse
                                   (("0").Equals(outMRow.Item("OUTKA_TTL_NB").ToString) = True AndAlso
                                    ("0").Equals(outMRow.Item("BACKLOG_NB").ToString) = True) OrElse
                                   (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = True AndAlso
                                    ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = True) Then '要望番号:1504 terakawa 条件追加 2012.10.19 

                                    Dim isMihikiateError As Boolean
                                    If (("0").Equals(outMRow.Item("OUTKA_TTL_NB").ToString) = False AndAlso
                                        ("0").Equals(outMRow.Item("BACKLOG_NB").ToString) = False) OrElse
                                       (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = False AndAlso
                                        ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = False) Then
                                        ' 2025/10/03 分納出荷を考慮した条件変更 1/3
                                        ' 以下のどちらかの場合は従来どおり未引当エラーとする。
                                        '   ・出荷総個数(OUTKA_TTL_NB) と 引当残個数(BACKLOG_NB) がどちらもゼロでない
                                        '   ・出荷総数量(OUTKA_TTL_QT) と 引当残数量(BACKLOG_QT) がどちらもゼロでない
                                        isMihikiateError = True
                                    Else
                                        ' 以下のどちらかの場合
                                        '   ・出荷総個数(OUTKA_TTL_NB) と 引当残個数(BACKLOG_NB) がどちらもゼロである
                                        '   ・出荷総数量(OUTKA_TTL_QT) と 引当残数量(BACKLOG_QT) がどちらもゼロである
                                        If .cmbSyukkaSyubetu.SelectedValue.ToString() = "60" AndAlso
                                            Convert.ToDecimal(LMC020C.SINTYOKU50) <= Convert.ToDecimal(.cmbSagyoSintyoku.SelectedValue.ToString()) Then
                                            ' 2025/10/03 分納出荷を考慮した条件変更 2/3
                                            ' 出荷種別が“分納”かつ作業進捗を“検品済”以降であれば、
                                            ' 次回分納出荷対象となる正常データのためエラーとしない。
                                            isMihikiateError = False
                                        Else
                                            ' 2025/10/03 分納出荷を考慮した条件変更 3/3
                                            ' 出荷種別が“分納”以外であれば、従来どおり未引当エラーとする。
                                            isMihikiateError = True
                                        End If
                                    End If
                                    If isMihikiateError Then
                                        MyBase.ShowMessage("E309", New String() {""})
                                        .cmbPRINT.Focus()
                                        Return False
                                    End If
                                End If

                        End Select
                        '20160114 要望番号:2488 tsunehira add end

                        'If (("0").Equals(outMRow.Item("OUTKA_TTL_NB").ToString) = False AndAlso _
                        '    ("0").Equals(outMRow.Item("BACKLOG_NB").ToString) = False) OrElse _
                        '   (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = False AndAlso _
                        '    ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = False) OrElse _
                        '   (("0").Equals(outMRow.Item("OUTKA_TTL_NB").ToString) = True AndAlso _
                        '    ("0").Equals(outMRow.Item("BACKLOG_NB").ToString) = True) OrElse _
                        '   (("0.000").Equals(outMRow.Item("OUTKA_TTL_QT").ToString) = True AndAlso _
                        '    ("0.000").Equals(outMRow.Item("BACKLOG_QT").ToString) = True) Then '要望番号:1504 terakawa 条件追加 2012.10.19 

                        '    MyBase.ShowMessage("E309", New String() {""})
                        '    .cmbPRINT.Focus()
                        '    Return False
                        'End If
                    Next

                End If
                '2013.02.18 アグリマート対応 END

            End If

            'END KIM 要望番号1504 2012/10/09

            '船積確認書対応 yamanaka 2012.12.03 Start
            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
               ("10").Equals(.cmbPRINT.SelectedValue) = True Then

                Dim custDetailsDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, _
                                                                                                                                 "' AND CUST_CD = '", .lblCustCdL.TextValue, _
                                                                                                                                 "' AND SUB_KB = '50'"))
                If custDetailsDr.Length = 0 Then
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E719", New String() {.lblCustCdL.TextValue})
                    'MyBase.ShowMessage("E336", New String() {String.Concat(.lblCustCdL.TextValue, "は対象荷主"), "処理"})
                    Return False
                End If

            End If
            '船積確認書対応 yamanaka 2012.12.03 End

            '印刷種別(梱包明細) + ｹｰｽﾏｰｸ情報チェック
            If LMC020C.EventShubetsu.PRINT.Equals(eventShubetsu) = True AndAlso _
               (("14").Equals(.cmbPRINT.SelectedValue) = True OrElse ("15").Equals(.cmbPRINT.SelectedValue) = True) Then

                'エラー判定フラグ
                Dim rtnFlg As Boolean = False
                '①出荷管理番号中の先頭を取得
                Dim outMRow As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0' AND OUTKA_NO_M = MIN(OUTKA_NO_M)")
                Dim outkaNoM As String = outMRow(0).Item("OUTKA_NO_M").ToString()

                '②対象ケースマーク情報を取得
                Dim csDtlRow As DataRow() = ds.Tables(LMC020C.TABLE_NM_MARK_DTL).Select(String.Concat("SYS_DEL_FLG = '0' AND OUTKA_NO_M = '", outkaNoM, "'"))

                'チェック(データテーブル内に対象データが存在し、Rowが1行以上ある。かつRow全行が空ではない)
                If csDtlRow IsNot Nothing Then
                    For Each row As DataRow In csDtlRow
                        If String.IsNullOrEmpty(row.Item("REMARK_INFO").ToString()) = False Then
                            rtnFlg = True
                            Exit For
                        End If
                    Next
                End If

                'エラー判定
                If rtnFlg = False Then
                    '2015.10.21 tusnehira add
                    '英語化対応
                    MyBase.ShowMessage("E687", New String() {outkaNoM.ToString})
                    'MyBase.ShowMessage("E028", New String() {String.Concat("出荷管理番号「", outkaNoM, "」のｹｰｽﾏｰｸ情報が入力されていない"), "梱包明細の印刷"})
                    Return False
                End If

            End If

            '実行時チェック
            If LMC020C.EventShubetsu.JIKKOU.Equals(eventShubetsu) = True Then

                Select Case .cmbJikkou.SelectedValue.ToString

                    Case "01"   '文書管理

                    Case "02"   '現場作業指示取消
                        If Not "01".Equals(.cmbWHSagyoSintyoku.SelectedValue) AndAlso _
                            Not "02".Equals(.cmbWHSagyoSintyoku.SelectedValue) Then
                            MyBase.ShowMessage("E01A")
                            Return False
                        End If

                    Case "03"   '現場作業指示
                        'ステータスチェック(検品済,完了済み以外エラー)
                        If Not ("50".Equals(ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_STATE_KB").ToString) OrElse
                            "60".Equals(ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_STATE_KB").ToString)) Then
                            MyBase.ShowMessage("E991", New String() {ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_NO_L").ToString})
                            Return False
                        End If

                        '現場作業指示ステータスチェック(指示済みエラー)
                        If LMC020C.WH_TAB_STATUS_01.Equals(ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("WH_TAB_STATUS").ToString) Then
                            MyBase.ShowMessage("E01D", New String() {ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_NO_L").ToString})
                            Return False
                        End If
                        '現場作業対象チェック
                        If Not (.chkTablet.Checked) Then
                            MyBase.ShowMessage("E00I", New String() {ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_NO_L").ToString})
                            Return False
                        End If

                        '引当未済チェック
                        Dim outkaNoM As String = String.Empty
                        For Each drChk As DataRow In ds.Tables(LMC020C.TABLE_NM_OUT_M).Rows
                            If LMConst.FLG.OFF.Equals(drChk.Item("SYS_DEL_FLG").ToString) AndAlso _
                                Integer.Parse(drChk.Item("BACKLOG_NB").ToString) > 0 Then
                                MyBase.ShowMessage("E114", New String() {ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("OUTKA_NO_L").ToString})
                                Return False
                            End If
                        Next

                End Select

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック（ワーニング）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenWorningCheck(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal ds As DataSet) As Boolean

        Dim value As Decimal = 0
        Dim dr As DataRow() = Nothing
        Dim custDetailsDr() As DataRow = Nothing

        '【関連項目チェック】
        With Me._Frm

            '納入予定日に値が入力されている時のみチェックを行う。
            If String.IsNullOrEmpty(.imdNounyuYoteiDate.TextValue) = False Then

                'START YANAI 要望番号837
                'If (.imdNounyuYoteiDate.ReadOnly = False OrElse .imdSyukkaYoteiDate.ReadOnly = False) AndAlso _
                '     LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                '     ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                If (.imdNounyuYoteiDate.ReadOnly = False OrElse .imdSyukkaYoteiDate.ReadOnly = False) AndAlso _
                     LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                     ("20").Equals(.cmbTehaiKbn.SelectedValue) = False AndAlso _
                     ("01").Equals(.cmbTodokesaki.SelectedValue) = False Then
                    'END YANAI 要望番号837
                    Dim sakiduke As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S042'"))
                    If .imdNounyuYoteiDate.TextValue > Convert.ToString(DateAdd("d", Convert.ToDecimal(sakiduke(0).Item("KBN_NM1").ToString), Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(.imdSyukkaYoteiDate.TextValue)).ToString("yyyyMMdd")) Then
                        '納入予定日＋（出荷予定日に先付け納入日許容範囲を加算した値）
                        If MyBase.ShowMessage("W118") = MsgBoxResult.Cancel Then
                            .imdNounyuYoteiDate.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                            Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                            Return False
                        End If
                    End If
                End If

                'START YANAI 要望番号837
                'If (.imdNounyuYoteiDate.ReadOnly = False) AndAlso _
                '    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                '    ("20").Equals(.cmbTehaiKbn.SelectedValue) = False Then
                If (.imdNounyuYoteiDate.ReadOnly = False) AndAlso _
                    LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True AndAlso _
                    ("20").Equals(.cmbTehaiKbn.SelectedValue) = False AndAlso _
                    ("01").Equals(.cmbTodokesaki.SelectedValue) = False Then
                    'END YANAI 要望番号837
                    Dim holi As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.HOL).Select(String.Concat(String.Concat("HOL = '", .imdNounyuYoteiDate.Value, "'")))
                    If 0 < holi.Length OrElse _
                       1 = Weekday(Date.Parse(Format(Convert.ToDecimal(.imdNounyuYoteiDate.TextValue), "0000/00/00"))) Then
                        '納入予定日＋曜日（休日はエラー）
                        If MyBase.ShowMessage("W108") = MsgBoxResult.Cancel Then
                            Me._Vcon.SetErrorControl(.imdNounyuYoteiDate)
                            Return False
                        End If
                    End If
                End If

            End If

            If (.cmbUnsoOndo.ReadOnly = False OrElse .cmbBinKbn.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.INS_M.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.DOUBLE_CLICK.Equals(eventShubetsu) = True OrElse _
                 LMC020C.EventShubetsu.COPY_M.Equals(eventShubetsu) = True) Then
                '温度・便区分
                'START YANAI 要望番号429
                'If (("10").Equals(.cmbUnsoOndo.SelectedValue) OrElse ("20").Equals(.cmbUnsoOndo.SelectedValue)) AndAlso _
                '    (("91").Equals(.cmbBinKbn.SelectedValue) OrElse ("92").Equals(.cmbBinKbn.SelectedValue)) Then
                If (("10").Equals(.cmbUnsoOndo.SelectedValue) = True OrElse ("20").Equals(.cmbUnsoOndo.SelectedValue) = True) AndAlso _
                    (("91").Equals(.cmbBinKbn.SelectedValue) = False AndAlso ("92").Equals(.cmbBinKbn.SelectedValue) = False) Then
                    'END YANAI 要望番号429
                    If MyBase.ShowMessage("W117") = MsgBoxResult.Cancel Then
                        .cmbBinKbn.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.cmbUnsoOndo)
                        'START YANAI 要望番号495
                        '.tabMiddle.SelectTab(.tabGoods)
                        .tabMiddle.SelectTab(.tabUnso)
                        'END YANAI 要望番号495
                        Return False
                    End If
                End If
            End If

            If (LMC020C.EventShubetsu.DEL.Equals(eventShubetsu) = True) Then
                Dim sprMmax As Integer = .sprSyukkaM.ActiveSheet.Rows.Count - 1
                For i As Integer = 0 To sprMmax
                    'スプレッド(中)すべてを対象にするチェック
                    '引当状況
                    Dim outSdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                    "OUTKA_NO_L = '", .lblSyukkaLNo.TextValue, "' AND ", _
                                    "OUTKA_NO_M = '", _Gcon.GetCellValue(.sprSyukkaM.ActiveSheet.Cells(i, LMC020C.sprSyukkaMColumnIndex.KANRI_NO)), "' AND ", _
                                    "SYS_DEL_FLG = '0'"))
                    If outSdr.Length = 0 Then
                        MyBase.ShowMessage("W113")
                        Exit For
                    End If
                Next
            End If

            'START YANAI 要望番号1370 三井化学専用　出荷登録時メッセージ表示
            If (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                If String.IsNullOrEmpty(.txtOrderType.TextValue) = True Then
                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                    "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                                    "SUB_KB = '43'"))

                    dr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.MESSAGE).Select("MESSAGE_ID = 'W216'")
                    If 0 < custDetailsDr.Length AndAlso 0 < dr.Length Then
                        Dim messageStr As String = dr(0).Item("MESSAGE_STRING").ToString.Replace("[%9]", vbCrLf)
                        If MessageBox.Show(messageStr, "出荷入力", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) = DialogResult.No Then
                            Return False
                        End If
                    End If
                End If
            End If
            'END YANAI 要望番号1370 三井化学専用　出荷登録時メッセージ表示


            '■↓ここから下はワーニングポップを表示するが、処理を中断するようなものではない。
            Dim unsoDr() As DataRow = Nothing
            'START YANAI EDIメモNo.52
            'If (.numKonpoKosu.ReadOnly = False OrElse .cmbTodokesaki.ReadOnly = False) AndAlso _
            '    (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
            ''届先区分・出荷梱包個数
            'If ("02").Equals(.cmbTodokesaki.SelectedValue) = True Then
            If (.numKonpoKosu.ReadOnly = False) AndAlso _
                (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                'END YANAI EDIメモNo.52
                '荷主明細キャッシュから値を取得
                '要望番号:1253 terakawa 2012.07.13 Start
                'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND ", _
                '                                                                                                "SUB_KB = '04'"))
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                "CUST_CD = '", .txtCust_Cd_L.TextValue, "' AND ", _
                                                                                                                "SUB_KB = '04'"))
                '要望番号:1253 terakawa 2012.07.13 End
                If 0 < custDetailsDr.Length Then
                    '運送会社キャッシュから値を取得
                    If (Convert.ToDecimal(.numKonpoKosu.TextValue) = 1) Then
                        unsoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat("UNSOCO_CD = '", (custDetailsDr(0).Item("SET_NAIYO").ToString).Substring(0, 3), "' AND " _
                                                                                                             , "UNSOCO_BR_CD = '", (custDetailsDr(0).Item("SET_NAIYO").ToString).Substring(3, 3), "'"))
                    ElseIf (2 <= Convert.ToDecimal(.numKonpoKosu.TextValue)) Then
                        unsoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(String.Concat("UNSOCO_CD = '", (custDetailsDr(0).Item("SET_NAIYO_2").ToString).Substring(0, 3), "' AND " _
                                                                                                             , "UNSOCO_BR_CD = '", (custDetailsDr(0).Item("SET_NAIYO_2").ToString).Substring(3, 3), "'"))
                    End If
                    If unsoDr Is Nothing = False Then
                        If 0 < unsoDr.Length AndAlso _
                            ((.txtUnsoCompanyCd.TextValue).Equals(unsoDr(0).Item("UNSOCO_CD").ToString) = False OrElse _
                             (.txtUnsoSitenCd.TextValue).Equals(unsoDr(0).Item("UNSOCO_BR_CD").ToString) = False) Then
                            If MyBase.ShowMessage("W111", New String() {String.Concat(unsoDr(0).Item("UNSOCO_NM").ToString, " ", unsoDr(0).Item("UNSOCO_BR_NM").ToString)}) = MsgBoxResult.Ok Then
                                .txtUnsoCompanyCd.TextValue = unsoDr(0).Item("UNSOCO_CD").ToString
                                .txtUnsoSitenCd.TextValue = unsoDr(0).Item("UNSOCO_BR_CD").ToString
                                .lblUnsoCompanyNm.TextValue = unsoDr(0).Item("UNSOCO_NM").ToString
                                .lblUnsoSitenNm.TextValue = unsoDr(0).Item("UNSOCO_BR_NM").ToString
                            End If
                        End If
                    End If
                End If
            End If
            'START YANAI EDIメモNo.52
            'End If
            'END YANAI EDIメモNo.52
        End With

        Return True

    End Function


    ''' <summary>
    ''' 名鉄印刷入力チェック（ワーニング）。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsMeitetsuCheck(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal ds As DataSet) As Boolean

        Dim value As Decimal = 0
        Dim dr As DataRow() = Nothing
        Dim custDetailsDr() As DataRow = Nothing

        '【関連項目チェック】
        With Me._Frm
            '荷札・送状時チェック
            If "01".Equals(.cmbPRINT.SelectedValue.ToString) = True OrElse _
               "02".Equals(.cmbPRINT.SelectedValue.ToString) = True Then

                '名鉄対象か
                Dim kbnDr() As DataRow = Nothing
                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'C004' AND ", _
                                                                                   "KBN_NM1 = '", .cmbEigyosyo.SelectedValue.ToString(), "' AND ", _
                                                                                   "KBN_NM2 = '", .txtUnsoCompanyCd.TextValue, "' AND ", _
                                                                                    "KBN_NM3 = '", .txtUnsoSitenCd.TextValue, "'"))
                If kbnDr.Length = 1 Then
                    'お問合せ番号
                    If String.IsNullOrEmpty(.lblAutoDenpNo.TextValue) = True Then
                        MyBase.ShowMessage("E454", New String() {"お問い合わせ番号が空欄", "出力", "お問い合わせ番号の取得を行ってください。"})
                        Return False
                    End If

                End If
            End If

        End With

        Return True

    End Function


    ''' <summary>
    ''' 横持ち車輌チェック
    ''' </summary>
    ''' <param name="id">メッセージID</param>
    ''' <param name="msg1">置換文字1</param>
    ''' <param name="msg2">置換文字2</param>
    ''' <param name="hissuFlg">必須フラグ　True:ない場合、エラー　False:ある場合、エラー</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoSharyoChk(ByVal id As String, ByVal msg1 As String, ByVal msg2 As String, ByVal hissuFlg As Boolean) As Boolean

        '値がある(ない)場合、エラー
        If String.IsNullOrEmpty(Me._Frm.cmbSyaryoKbn.SelectedValue.ToString()) = hissuFlg Then

            Me._Frm.txtUnthinTariffCd.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()

            'START YANAI 要望番号495
            'Me._Vcon.SetErrorControl(Me._Frm.cmbSyaryoKbn, Me._Frm.tabMiddle, Me._Frm.tabUnso)
            Me._Vcon.SetErrorControl(Me._Frm.cmbSyaryoKbn)
            'END YANAI 要望番号495
            MyBase.ShowMessage(id, New String() {msg1, msg2})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 関連項目入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck2(ByVal eventShubetsu As LMC020C.EventShubetsu, ByVal ds As DataSet) As Boolean

        '【データセットに値をセット後の関連項目チェック】
        With Me._Frm

            If (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                Dim zaiDr As DataRow() = ds.Tables(LMC020C.TABLE_NM_ZAI).Select("SYS_DEL_FLG = '0'")
                Dim max As Integer = zaiDr.LENGTH - 1
                For i As Integer = 0 To max
                    '①出荷予定日 + 入荷予定日
                    If .imdSyukkaYoteiDate.TextValue < zaiDr(i).Item("INKA_DATE").ToString Then
                        Dim outSdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                            "ZAI_REC_NO = '", zaiDr(i).Item("ZAI_REC_NO").ToString, "' AND ", _
                                                            "SYS_DEL_FLG = '0'"))
                        If 0 < outSdr.Length Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E723", New String() {outSdr(0).Item("OUTKA_NO_M").ToString, outSdr(0).Item("OUTKA_NO_S").ToString})
                            'MyBase.ShowMessage("E398", New String() {"出荷予定日", _
                            '                                         "入荷予定日", _
                            '                                         String.Concat("[出荷管理番号(中)=", outSdr(0).Item("OUTKA_NO_M"), "・出荷管理番号(小)=", outSdr(0).Item("OUTKA_NO_S"), "]")})
                            Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                            Return False
                        End If
                    End If

                    '②出荷予定日 + 移動日
                    If .imdSyukkaYoteiDate.TextValue < zaiDr(i).Item("IDO_DATE").ToString Then
                        Dim outSdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                            "ZAI_REC_NO = '", zaiDr(i).Item("ZAI_REC_NO").ToString, "' AND ", _
                                                            "SYS_DEL_FLG = '0'"))

                        If 0 < outSdr.Length Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E723", New String() {outSdr(0).Item("OUTKA_NO_M").ToString, outSdr(0).Item("OUTKA_NO_S").ToString})
                            'MyBase.ShowMessage("E398", New String() {"出荷予定日", _
                            '                                         "移動日", _
                            '                                         String.Concat("[出荷管理番号(中)=", outSdr(0).Item("OUTKA_NO_M"), "・出荷管理番号(小)=", outSdr(0).Item("OUTKA_NO_S"), "]")})
                            Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                            Return False
                        End If
                    End If

                    '③保管料起算日 + 保管料終算日
                    If String.IsNullOrEmpty(.imdHokanEndDate.TextValue) = False AndAlso _
                        String.IsNullOrEmpty(zaiDr(i).Item("HOKAN_STR_DATE").ToString) = False Then
                        If .imdHokanEndDate.TextValue < zaiDr(i).Item("HOKAN_STR_DATE").ToString Then
                            Dim outSdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                "ZAI_REC_NO = '", zaiDr(i).Item("ZAI_REC_NO").ToString, "' AND ", _
                                                                "SYS_DEL_FLG = '0'"))
                            If 0 < outSdr.Length Then
                                '英語化対応
                                '20151022 tsunehira add
                                MyBase.ShowMessage("E753", New String() {outSdr(0).Item("OUTKA_NO_M").ToString, outSdr(0).Item("OUTKA_NO_S").ToString})
                                'MyBase.ShowMessage("E402", New String() {"保管料起算日", _
                                '                                         "保管料終算日", _
                                '                                         String.Concat("[出荷管理番号(中)=", outSdr(0).Item("OUTKA_NO_M"), "・出荷管理番号(小)=", outSdr(0).Item("OUTKA_NO_S"), "]")})
                                Me._Vcon.SetErrorControl(.imdHokanEndDate)
                                Return False
                            End If
                        End If
                    End If


                    '③保管料起算日 + 出荷予定日
                    If String.IsNullOrEmpty(.imdSyukkaYoteiDate.TextValue) = False AndAlso _
                        String.IsNullOrEmpty(zaiDr(i).Item("HOKAN_STR_DATE").ToString) = False Then
                        If .imdSyukkaYoteiDate.TextValue < zaiDr(i).Item("HOKAN_STR_DATE").ToString Then
                            Dim outSdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                "ZAI_REC_NO = '", zaiDr(i).Item("ZAI_REC_NO").ToString, "' AND ", _
                                                                "SYS_DEL_FLG = '0'"))

                            If 0 < outSdr.Length Then
                                '英語化対応
                                '20151022 tsunehira add
                                MyBase.ShowMessage("E752", New String() {outSdr(0).Item("OUTKA_NO_M").ToString, outSdr(0).Item("OUTKA_NO_S").ToString})
                                'MyBase.ShowMessage("E402", New String() {"保管料起算日", _
                                '                                         "出荷予定日", _
                                '                                         String.Concat("[出荷管理番号(中)=", outSdr(0).Item("OUTKA_NO_M"), "・出荷管理番号(小)=", outSdr(0).Item("OUTKA_NO_S"), "]")})
                                Me._Vcon.SetErrorControl(.imdSyukkaYoteiDate)
                                Return False
                            End If
                        End If
                    End If

                Next
            End If

            If (LMC020C.EventShubetsu.HOZON.Equals(eventShubetsu) = True) Then
                '届先コードの差異チェック
                Dim zaiDr() As DataRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                        "(DEST_CD_P <> '", .txtTodokesakiCd.TextValue, "' AND ", _
                                                        " DEST_CD_P <> '') AND ", _
                                                        "SYS_DEL_FLG = '0'"))
                If 0 < zaiDr.Length Then
                    '異なるデータが存在した場合は、出荷Sを検索して、エラーとなっている出荷情報を取得する
                    Dim outSdr() As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                        "ZAI_REC_NO = '", zaiDr(0).Item("ZAI_REC_NO").ToString, "' AND ", _
                                                        "SYS_DEL_FLG = '0'"))
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E722", New String() {outSdr(0).Item("OUTKA_NO_M").ToString, outSdr(0).Item("OUTKA_NO_S").ToString})
                    'MyBase.ShowMessage("E393", New String() {String.Concat("[出荷管理番号(中)=", outSdr(0).Item("OUTKA_NO_M"), "・出荷管理番号(小)=", outSdr(0).Item("OUTKA_NO_S"), "]")})
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 引当在庫数チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsZaikoCheck(ByVal ds As DataSet) As Boolean

        Dim zaiRow As DataRow() = ds.Tables(LMC020C.TABLE_NM_ZAI).Select("SYS_DEL_FLG = '0'")
        Dim zaiMax As Integer = zaiRow.Length - 1
        Dim zaiRowMax As Integer = 0
        Dim hikiNB As Decimal = 0
        For i As Integer = 0 To zaiMax
            zaiRow = ds.Tables(LMC020C.TABLE_NM_ZAI).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyosyo.SelectedValue, "' AND ", _
                                                                          "ZAI_REC_NO = '", ds.Tables(LMC020C.TABLE_NM_ZAI).Rows(i).Item("ZAI_REC_NO"), "' AND ", _
                                                                          "SYS_DEL_FLG = '0'"))
            zaiRowMax = zaiRow.Count - 1
            hikiNB = 0
            '在庫レコード番号単位で引当中個数を全部加算し、実予在庫個数より多かったらエラー
            For j As Integer = 0 To zaiRowMax
                hikiNB = hikiNB + Convert.ToDecimal(zaiRow(j).Item("ALCTD_NB"))
            Next
            'START YANAI 要望番号809
            'If Convert.ToDecimal(zaiRow(0).Item("PORA_ZAI_NB")) < hikiNB Then
            '    Me._Vcon.SetErrMessage("E163", New String() {"実予在庫個数", "引当個数"})
            '    Return False
            'End If
            If 0 <= zaiRowMax Then
                If Convert.ToDecimal(zaiRow(0).Item("PORA_ZAI_NB")) < hikiNB Then

                    '20151029 tsunehira add Start
                    '英語化対応
                    Me._Vcon.SetErrMessage("E802")
                    '2015.10.29 tusnehira add End
                    'Me._Vcon.SetErrMessage("E163", New String() {"実予在庫個数", "引当個数"})
                    Return False
                End If
            End If
            'END YANAI 要望番号809
        Next

        Return True

    End Function
    '2012/12/06 要望番号612対応
    ''' <summary>
    ''' 削除処理チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsDeleteChk(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = True

        '引当済みチェック
        rtnResult = rtnResult AndAlso Me.IsHikiateChk(ds)

        '在庫移動チェック
        rtnResult = rtnResult AndAlso Me.IsIdoTrsChk(ds)

        Return rtnResult

    End Function
    ''' <summary>
    ''' 引当済みチェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsHikiateChk(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_FURIDEL)
        Dim dr() As DataRow = dt.Select()
        Dim max As Integer = dr.Length - 1
        For i As Integer = 0 To max

            '2017/09/25 修正 李↓
            '引当済みの場合、エラー
            If dr(i).Item("HIKIATE").ToString().Equals(lgm.Selector({LMC020C.HIKIATE_ARI, LMC020C.HIKIATE_ARI_ENG, LMC020C.HIKIATE_NASI_KR, "中国語"})) = True Then
                Return Me._Vcon.SetErrMessage("E139")
            End If
            '2017/09/25 修正 李↑

            'If LMC020C.HIKIATE_ARI.Equals(dr(i).Item("HIKIATE").ToString()) = True Then
            'Return Me._Vcon.SetErrMessage("E139")
            'End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫移動チェック（削除時）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsIdoTrsChk(ByVal ds As DataSet) As Boolean

        Dim dt As DataTable = ds.Tables(LMC020C.TABLE_NM_FURIDEL)
        Dim dr() As DataRow = dt.Select()
        Dim max As Integer = dr.Length - 1

        For i As Integer = 0 To max

            '在庫移動がある場合、エラー
            If 0 < Convert.ToInt32(Me._Gcon.FormatNumValue(dr(i).Item("ZAI_REC_CNT").ToString())) Then
                Return Me._Vcon.SetErrMessage("E148")
            End If
        Next

        Return True

    End Function
    '2012/12/06 要望番号612対応


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
    ''' 電話番号用チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsNumericHaifunCheck(ByVal value As String) As Boolean

        Dim okValue As String = "0123456789-"
        Dim max As Integer = value.Length
        Dim wordPoint As Integer = 0

        For i As Integer = 1 To max
            wordPoint = okValue.IndexOf(Mid(value, i, 1))
            If -1 = wordPoint Then
                Return False
            End If
        Next

        Return True

    End Function

    ''' <summary>
    ''' 届先マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsDestExistChk(ByVal value As String, ByVal fieldNm As String) As Boolean

        With Me._Frm

            If LMC020C.URICD.Equals(fieldNm) = True Then
                .lblUriNm.TextValue = String.Empty
            ElseIf LMC020C.TODOKECD.Equals(fieldNm) = True Then
                .txtTodokesakiNm.TextValue = String.Empty
            End If

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '届先コードの存在チェック
            'START YANAI メモ②No.23
            'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("DEST_CD = '", value, "'"))
            '---↓
            'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(String.Concat("CUST_CD_L = '", .txtCust_Cd_L.TextValue, "' AND ", _
            '                                                                                               "DEST_CD = '", value, "'"))

            Dim destMstDs As MDestDS = New MDestDS
            Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
#If True Then       'ADD 2018/11/27 依頼番号 : 003383   【LMS】調査_出荷登録時、届先名が違うものが表示される

            destMstDr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue
#End If
            destMstDr.Item("CUST_CD_L") = .txtCust_Cd_L.TextValue
            destMstDr.Item("DEST_CD") = value
            destMstDr.Item("SYS_DEL_FLG") = "0"  '要望番号1604 2012/11/16 本明追加
            destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
            Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
            Dim drs As DataRow() = rtnDs.Tables(LMConst.CacheTBL.DEST).Select
            '---↑

            'END YANAI メモ②No.23
            If drs.Length < 1 Then
                Return False
            End If

            If LMC020C.URICD.Equals(fieldNm) = True Then
                .txtUriCd.TextValue = drs(0).Item("DEST_CD").ToString()
                .lblUriNm.TextValue = drs(0).Item("DEST_NM").ToString()
            ElseIf LMC020C.TODOKECD.Equals(fieldNm) = True Then
                .txtTodokesakiCd.TextValue = drs(0).Item("DEST_CD").ToString()
                'START YANAI 要望番号909
                .txtTodokesakiCdOld.TextValue = drs(0).Item("DEST_CD").ToString()
                'END YANAI 要望番号909
                .txtTodokesakiNm.TextValue = drs(0).Item("DEST_NM").ToString()
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 作業項目マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSagyoExistChk(ByVal nrsBrCd As String, ByVal sagyoCd As String, ByVal custCd As String, ByVal fieldNm As String) As Boolean

        With Me._Frm

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(sagyoCd) = True Then
                Return True
            End If

            '作業コードの存在チェック
            'START YANAI 要望番号376
            'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
            '                                                                                               , "SAGYO_CD = '", sagyoCd, "' AND " _
            '                                                                                               , "CUST_CD_L = '", custCd, "'"))
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SAGYO).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND " _
                                                                                                           , "SAGYO_CD = '", sagyoCd, "' AND " _
                                                                                                           , "(CUST_CD_L = '", custCd, "' OR CUST_CD_L = 'ZZZZZ')"))
            'END YANAI 要望番号376
            If drs.Length < 1 Then
                Return False
            End If

            Select Case fieldNm

                Case LMC020C.SAGYO_L01
                    .txtSagyoL1.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoL1.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_L02
                    .txtSagyoL2.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoL2.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_L03
                    .txtSagyoL3.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoL3.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_L04
                    .txtSagyoL4.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoL4.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_L05
                    .txtSagyoL5.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoL5.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_M01
                    .txtSagyoM1.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoM1.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_M02
                    .txtSagyoM2.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoM2.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_M03
                    .txtSagyoM3.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoM3.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_M04
                    .txtSagyoM4.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoM4.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_M05
                    .txtSagyoM5.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblSagyoM5.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_DESTM01
                    .txtDestSagyoM1.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblDestSagyoM1.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

                Case LMC020C.SAGYO_DESTM02
                    .txtDestSagyoM2.TextValue = drs(0).Item("SAGYO_CD").ToString()
                    .lblDestSagyoM2.TextValue = drs(0).Item("SAGYO_RYAK").ToString()

            End Select


            Return True

        End With

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnchinTariffExistChk(ByVal value As String, ByVal checkDate As String) As Boolean

        With Me._Frm

            .lblUnthinTariffNm.TextValue = String.Empty

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '運賃タリフの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(String.Concat("UNCHIN_TARIFF_CD = '", value, "' AND ", _
                                                                                                                    "STR_DATE <= '", checkDate, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtUnthinTariffCd.TextValue = drs(0).Item("UNCHIN_TARIFF_CD").ToString
            .lblUnthinTariffNm.TextValue = drs(0).Item("UNCHIN_TARIFF_REM").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsYokoTariffExistChk(ByVal value As String) As Boolean

        With Me._Frm

            .lblUnthinTariffNm.TextValue = String.Empty

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '横持ちタリフの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(String.Concat("YOKO_TARIFF_CD = '", value, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtUnthinTariffCd.TextValue = drs(0).Item("YOKO_TARIFF_CD").ToString
            .lblUnthinTariffNm.TextValue = drs(0).Item("YOKO_REM").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 支払運賃タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiTariffExistChk(ByVal value As String, ByVal checkDate As String) As Boolean

        With Me._Frm

            .lblPayUnthinTariffNm.TextValue = String.Empty

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '支払運賃タリフの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAI_TARIFF).Select(String.Concat("SHIHARAI_TARIFF_CD = '", value, "' AND ", _
                                                                                                                    "STR_DATE <= '", checkDate, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtPayUnthinTariffCd.TextValue = drs(0).Item("SHIHARAI_TARIFF_CD").ToString
            .lblPayUnthinTariffNm.TextValue = drs(0).Item("SHIHARAI_TARIFF_REM").ToString

            Return True

        End With

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払横持ちタリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiYokoTariffExistChk(ByVal value As String) As Boolean

        With Me._Frm

            .lblPayUnthinTariffNm.TextValue = String.Empty

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '支払横持ちタリフの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD_SHIHARAI).Select(String.Concat("YOKO_TARIFF_CD = '", value, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtPayUnthinTariffCd.TextValue = drs(0).Item("YOKO_TARIFF_CD").ToString
            .lblPayUnthinTariffNm.TextValue = drs(0).Item("YOKO_REM").ToString

            Return True

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsWariTariffExistChk(ByVal value As String) As Boolean

        With Me._Frm

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '割増タリフの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EXTC_UNCHIN).Select(String.Concat("EXTC_TARIFF_CD = '", value, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtExtcTariffCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString

            Return True

        End With

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' 支払割増タリフマスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsShiharaiWariTariffExistChk(ByVal value As String) As Boolean

        With Me._Frm

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return True
            End If

            '支払割増タリフの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EXTC_SHIHARAI).Select(String.Concat("EXTC_TARIFF_CD = '", value, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtPayExtcTariffCd.TextValue = drs(0).Item("EXTC_TARIFF_CD").ToString

            Return True

        End With

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 運送会社マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsUnsoExistChk(ByVal value As String, ByVal value2 As String) As Boolean

        With Me._Frm

            .lblUnsoCompanyNm.TextValue = String.Empty

            '未入力の場合はTrueを戻す
            If String.IsNullOrEmpty(value) = True AndAlso _
                String.IsNullOrEmpty(value2) = True Then
                Return True
            End If

            '運送会社の存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select( _
                                    String.Concat("UNSOCO_CD = '", value, "' AND " _
                                    , "UNSOCO_BR_CD = '", value2, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            .txtUnsoCompanyCd.TextValue = drs(0).Item("UNSOCO_CD").ToString
            .txtUnsoSitenCd.TextValue = drs(0).Item("UNSOCO_BR_CD").ToString
            .lblUnsoCompanyNm.TextValue = drs(0).Item("UNSOCO_NM").ToString
            .lblUnsoSitenNm.TextValue = drs(0).Item("UNSOCO_BR_NM").ToString
            .lblUnsoTareYn.TextValue = drs(0).Item("TARE_YN").ToString

            Return True

        End With

    End Function

    ''' <summary>
    ''' 分析表出力時、存在チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="ds">データセット</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsPrintCOACheck(ByVal frm As LMC020F, ByVal ds As DataSet, ByVal _BunsekiArr As ArrayList) As ArrayList

        Dim drL As DataRow = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0)

        '分析表存在チェック
        Dim whereStr As String = String.Empty
        Dim nrsBrCd As String = String.Empty
        Dim goodsCdNrs As String = String.Empty
        Dim lotNo As String = String.Empty
        Dim destCd As String = String.Empty
        'START YANAI 要望番号376
        'Dim custCdL As String = String.Empty
        'END YANAI 要望番号376
        Dim custNm As String = String.Empty
        Dim coaYn As String = String.Empty
        Dim noCheckCnt As Integer = 0
        'ADD START 2018/11/14 要望番号001939
        Dim whereStrInkaDate As String = String.Empty
        Dim inkaDate As String = String.Empty
        'ADD END   2018/11/14 要望番号001939

        Dim dr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select("SYS_DEL_FLG = '0'")
        Dim max As Integer = dr.Length - 1
        Dim drM As DataRow = Nothing

        'START YANAI 要望番号735
        Dim allnocheckFlg As Boolean = True
        'END YANAI 要望番号735

        'START YANAI 要望番号735
        'For i As Integer = 0 To max

        '    drM = dr(i)

        '    whereStr = " SYS_DEL_FLG = '0' "
        '    nrsBrCd = drM.Item("NRS_BR_CD").ToString()
        '    goodsCdNrs = drM.Item("GOODS_CD_NRS").ToString()
        '    lotNo = drM.Item("LOT_NO").ToString()
        '    destCd = drL.Item("DEST_CD").ToString()
        '    'START YANAI 要望番号376
        '    'custCdL = drL.Item("CUST_CD_L").ToString()
        '    'END YANAI 要望番号376
        '    custNm = drL.Item("CUST_NM_L").ToString()
        '    coaYn = drM.Item("COA_YN").ToString()

        '    '出荷(中)の分析票区分が"有り"の場合にチェック
        '    If ("01").Equals(coaYn) = True Then

        '        whereStr = String.Concat(whereStr, " AND NRS_BR_CD = '", nrsBrCd, "'")

        '        If String.IsNullOrEmpty(goodsCdNrs) = False Then
        '            whereStr = String.Concat(whereStr, " AND GOODS_CD_NRS = '", goodsCdNrs, "'")
        '        End If

        '        If String.IsNullOrEmpty(lotNo) = False Then
        '            whereStr = String.Concat(whereStr, " AND LOT_NO = '", lotNo, "'")
        '        End If

        '        If String.IsNullOrEmpty(destCd) = False Then
        '            'START YANAI 要望番号376
        '            'whereStr = String.Concat(whereStr, " AND DEST_CD = '", destCd, "'")
        '            whereStr = String.Concat(whereStr, " AND (DEST_CD = '", destCd, "' OR DEST_CD = 'ZZZZZZZZZZZZZZZ') ")
        '        Else
        '            whereStr = String.Concat(whereStr, " AND DEST_CD = 'ZZZZZZZZZZZZZZZ'")
        '            'END YANAI 要望番号376
        '        End If

        '        'START YANAI 要望番号376
        '        'If String.IsNullOrEmpty(custCdL) = False Then
        '        '    whereStr = String.Concat(whereStr, " AND CUST_CD_L = '", custCdL, "'")
        '        'End If
        '        'END YANAI 要望番号376

        '        '存在チェック
        '        Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(whereStr)
        '        If drBunsekiMst.Length() = 0 Then
        '            '存在エラー時
        '            MyBase.ShowMessage("E079", New String() {"分析票マスタ", String.Concat(custNm, "の分析票")})

        '        Else

        '            '分析票のパスを取得
        '            If String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_LINK").ToString()) = False OrElse String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_NAME").ToString()) = False Then
        '                _BunsekiArr.Add(String.Concat(drBunsekiMst(0).Item("COA_LINK").ToString(), "\", drBunsekiMst(0).Item("COA_NAME").ToString()))
        '            Else
        '                MyBase.ShowMessage("E079", New String() {"分析票マスタ", String.Concat(custNm, "の分析票")})
        '            End If

        '        End If
        '    Else
        '        noCheckCnt = noCheckCnt + 1

        '    End If

        'Next
        '分析票印刷枚数を計算
        Dim bunsekiCnt As Integer = 0
        Dim outMdr As DataRow() = Nothing
        Dim outSdr As DataRow() = ds.Tables(LMC020C.TABLE_NM_OUT_S).Select("SYS_DEL_FLG = '0'", "LOT_NO,OUTKA_NO_M")
        Dim outSmax As Integer = outSdr.Length - 1
        Dim goodsNRS As String = String.Empty
        Dim outNoM As String = String.Empty
        For i As Integer = 0 To outSmax
            '商品キーを取得
            goodsNRS = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("OUTKA_NO_M = '", outSdr(i).Item("OUTKA_NO_M").ToString, "' AND ", _
                                                                              "SYS_DEL_FLG = '0'"))(0).Item("GOODS_CD_NRS").ToString

            '同じ商品キーのレコードを取得
            outMdr = ds.Tables(LMC020C.TABLE_NM_OUT_M).Select(String.Concat("GOODS_CD_NRS = '", goodsNRS, "' AND ", _
                                                                            "SYS_DEL_FLG = '0'"), _
                                                                  "OUTKA_NO_M")

            If ((goodsCdNrs).Equals(outMdr(0).Item("GOODS_CD_NRS").ToString()) = False OrElse _
                (lotNo).Equals(outSdr(i).Item("LOT_NO").ToString) = False) AndAlso _
                ("01").Equals(outMdr(0).Item("COA_YN").ToString) Then
                '同じ商品キーのレコードの中で、一番若い出荷管理番号(中)の時だけ
                nrsBrCd = outMdr(0).Item("NRS_BR_CD").ToString()
                goodsCdNrs = outMdr(0).Item("GOODS_CD_NRS").ToString()
                lotNo = outSdr(i).Item("LOT_NO").ToString()

                '特定荷主（FFEM）の場合、売上先から、分析表を出力
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("NRS_BR_CD").ToString(), _
                                                                                                         "' AND CUST_CD = '", ds.Tables("LMC020_OUTKA_L").Rows(0).Item("CUST_CD_L").ToString(), _
                                                                                                         "' AND SUB_KB = '83'"))

                If custDetailsDr.Length > 0 Then
                    '売上先（特殊）
                    destCd = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("SHIP_CD_L").ToString()
                    '売上先コードが存在しない場合は届け先コードをセット
                    If String.IsNullOrEmpty(destCd) = True Then
                        '納品先（標準）
                        destCd = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("DEST_CD").ToString()
                    End If

                Else
                    '納品先（標準）
                    destCd = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("DEST_CD").ToString()
                End If
                'destCd = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("DEST_CD").ToString()

                '入荷日
                inkaDate = outSdr(i).Item("INKA_DATE").ToString()   'ADD 2018/11/14 要望番号001939

                custNm = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("CUST_NM_L").ToString()
                outNoM = outSdr(i).Item("OUTKA_NO_M").ToString

                '条件式の生成
                whereStr = " SYS_DEL_FLG = '0' "
                whereStr = String.Concat(whereStr, " AND NRS_BR_CD = '", nrsBrCd, "'")

                If String.IsNullOrEmpty(goodsCdNrs) = False Then
                    whereStr = String.Concat(whereStr, " AND GOODS_CD_NRS = '", goodsCdNrs, "'")
                End If

                If String.IsNullOrEmpty(lotNo) = False Then
                    whereStr = String.Concat(whereStr, " AND LOT_NO = '", lotNo, "'")
                End If

                If String.IsNullOrEmpty(destCd) = False Then
                    whereStr = String.Concat(whereStr, " AND (DEST_CD = '", destCd, "' OR DEST_CD = 'ZZZZZZZZZZZZZZZ') ")
                Else
                    whereStr = String.Concat(whereStr, " AND DEST_CD = 'ZZZZZZZZZZZZZZZ'")
                End If

                'ADD START 2018/11/14 要望番号001939
                If String.IsNullOrEmpty(inkaDate) = False Then
                    whereStrInkaDate = String.Concat(" AND INKA_DATE = '", inkaDate, "' ")
                End If
                'ADD END   2018/11/14 要望番号001939

                '存在チェック
                'MOD START 2018/11/14 要望番号001939
                ''Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(whereStr)

                '検索1回目:条件に入荷日を含む
                Dim drBunsekiMst As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(String.Concat(whereStr, whereStrInkaDate))
                '該当データなしの場合
                If drBunsekiMst.Length() = 0 Then
                    '検索2回目:入荷日なし(汎用)
                    whereStrInkaDate = " AND INKA_DATE_VERS_FLG = '1' "
                    drBunsekiMst = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.COA).Select(String.Concat(whereStr, whereStrInkaDate))
                End If
                'MOD END   2018/11/14 要望番号001939

                If drBunsekiMst.Length() = 0 Then
                    '存在エラー時
                    '英語化対応
                    '20151021 tsunehira add
                    MyBase.ShowMessage("E699", New String() {custNm.ToString})
                    'MyBase.ShowMessage("E079", New String() {"分析票マスタ", String.Concat(custNm, "の分析票")})

                Else

                    '分析票のパスを取得
                    If String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_LINK").ToString()) = False OrElse String.IsNullOrEmpty(drBunsekiMst(0).Item("COA_NAME").ToString()) = False Then
                        _BunsekiArr.Add(String.Concat(drBunsekiMst(0).Item("COA_LINK").ToString(), "\", drBunsekiMst(0).Item("COA_NAME").ToString()))
                        'START YANAI 要望番号735
                        allnocheckFlg = False
                        'END YANAI 要望番号735
                    Else
                        '英語化対応
                        '20151021 tsunehira add
                        MyBase.ShowMessage("E699", New String() {custNm.ToString})
                        'MyBase.ShowMessage("E079", New String() {"分析票マスタ", String.Concat(custNm, "の分析票")})
                    End If

                End If
            Else
                noCheckCnt = noCheckCnt + 1

            End If

        Next
        'END YANAI 要望番号735

        '中レコード全行チェック対象外の場合
        'START YANAI 要望番号735
        'If max = noCheckCnt - 1 Then
        If allnocheckFlg = True Then
            'END YANAI 要望番号735
            _BunsekiArr.Add("ALLNOCHECK")
            MyBase.ShowMessage("E156")
        End If

        Return _BunsekiArr

    End Function

    ''' <summary>
    ''' 運送会社荷主別送り状情報マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsUnsoCustRptExistChk(ByVal value As String, ByVal value2 As String, ByVal value3 As String) As Boolean

        With Me._Frm

            '未入力の場合はFalseを戻す
            If String.IsNullOrEmpty(value) = True AndAlso _
                String.IsNullOrEmpty(value2) = True Then
                Return False
            End If

            '運送会社荷主別送り状情報の存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSO_CUST_RPT).Select( _
                                    String.Concat("UNSOCO_CD = '", value, "' AND " _
                                    , "UNSOCO_BR_CD = '", value2, "' AND " _
                                    , "MOTO_TYAKU_KB = '", value3, "' AND " _
                                    , "PTN_ID = '11'"))
            If drs.Length < 1 Then
                Return False
            End If

            Return True

        End With

    End Function

    'START YANAI 20120122 立会書印刷対応
    ''' <summary>
    ''' 運送会社荷主別送り状情報マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCustDetailsExistChk(ByVal value As String) As Boolean

        With Me._Frm

            '未入力の場合はFalseを戻す
            If String.IsNullOrEmpty(value) = True Then
                Return False
            End If

            '荷主明細マスタの存在チェック
            '要望番号:1253 terakawa 2012.07.13 Start
            'Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat( _
            '                                                                                                        "CUST_CD LIKE '", .txtCust_Cd_L.TextValue, "%' AND ", _
            '                                                                                                        "SUB_KB = '", value, "'") _
            '                                                                                                       )
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyosyo.SelectedValue, "' AND ", _
                                                                                                                    "CUST_CD LIKE '", .txtCust_Cd_L.TextValue, "%' AND ", _
                                                                                                                    "SUB_KB = '", value, "'") _
                                                                                                                   )
            '要望番号:1253 terakawa 2012.07.13 End
            If drs.Length < 1 Then
                Return False
            End If

            Return True

        End With

    End Function
    'END YANAI 20120122 立会書印刷対応

    'START YANAI 要望番号909
    ''' <summary>
    ''' 届け先戻り値を設定するか判定チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsReturnDestChk() As Boolean

        With Me._Frm

            'START YANAI 要望番号1063
            'If String.IsNullOrEmpty(.txtHaisoRemark.TextValue) = False OrElse _
            '    String.IsNullOrEmpty(.cmbTariffKbun.SelectedValue.ToString()) = False OrElse _
            '    String.IsNullOrEmpty(.txtUnthinTariffCd.TextValue) = False OrElse _
            '    String.IsNullOrEmpty(.txtExtcTariffCd.TextValue) = False Then
            '    'いずれかに値が設定されている場合
            '    If MyBase.ShowMessage("W203", New String() {"届先情報・タリフ情報", "上書き"}) = MsgBoxResult.Ok Then
            '        Return True
            '    Else
            '        Return False
            '    End If
            'End If
            If String.IsNullOrEmpty(.txtHaisoRemark.TextValue) = False Then
                'いずれかに値が設定されている場合
                '英語化対応
                '20151022 tsunehira add
                'If MyBase.ShowMessage("W203", New String() {"届先情報", "上書き"}) = MsgBoxResult.Ok Then
                If MyBase.ShowMessage("W244") = MsgBoxResult.Ok Then
                    Return True
                Else
                    Return False
                End If
            End If
            'END YANAI 要望番号1063

            Return True

        End With

    End Function
    'END YANAI 要望番号909

    '要望番号:1350 terakawa 2012.08.24 Start
    ''' <summary>
    ''' 同一置場での同一商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:OK False:キャンセル</returns>
    ''' <remarks></remarks>
    Friend Function IsWorningChk(ByVal ds As DataSet) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'ワーニングテーブルに情報があった場合、ワーニング出力する
        Dim worningCount As Integer = ds.Tables(LMC020C.TABLE_NM_WORNING).Rows.Count
        If worningCount > 0 Then
            Dim strWorning As String = String.Empty
            Dim wGoodsCdCust As String = String.Empty
            Dim wOkiba As String = String.Empty
            Dim wLotNo As String = String.Empty
            For k As Integer = 0 To worningCount - 1
                strWorning = String.Concat(strWorning, vbCrLf)
                wGoodsCdCust = ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("GOODS_CD_CUST").ToString()
                wOkiba = String.Concat(ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("TOU_NO").ToString(), "-", _
                       ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("SITU_NO").ToString(), "-", _
                       ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("ZONE_CD").ToString())
                If String.IsNullOrEmpty(ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("LOCA").ToString()) = False Then
                    wOkiba = String.Concat(wOkiba, "-", ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("LOCA").ToString())
                End If
                wLotNo = ds.Tables(LMC020C.TABLE_NM_WORNING).Rows(k).Item("LOT_NO").ToString()

                '2017/09/25 修正 李↓
                strWorning = String.Concat(strWorning, lgm.Selector({"商品=", "Goods=", "상품=", "中国語"}), wGoodsCdCust, lgm.Selector({"、置場=", "、Place=", ", 하치장=", "中国語"}), wOkiba, "、LOT=", wLotNo)
                '2017/09/25 修正 李↑

            Next

            'ワーニングテーブルの中身を削除
            ds.Tables(LMC020C.TABLE_NM_WORNING).Clear()
            '英語化対応
            '20151022 tsunehira add
            If MyBase.ShowMessage("W242", New String() {vbCrLf.ToString, String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                Return False
            End If

            'If MyBase.ShowMessage("W215", New String() {String.Concat(vbCrLf, "同じ場所に保管されています。") _
            '                                            , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
            '    Return False
            'End If
        End If

        Return True
    End Function
    '要望番号:1350 terakawa 2012.08.24 End

    '要望番号:997 terakawa 2012.10.22 Start
    ''' <summary>
    ''' 出荷(中)削除チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:OK False:キャンセル</returns>
    ''' <remarks></remarks>
    Friend Function IsOutkaMDeleteChk(ByVal ds As DataSet) As Boolean

        Dim ChkDt As DataTable = ds.Tables(LMC020C.TABLE_NM_OUT_M)
        Dim max As Integer = ChkDt.Rows.Count - 1
        Dim sysDelFlg As String = String.Empty

        For i As Integer = 0 To max
            sysDelFlg = ChkDt.Rows(i).Item("SYS_DEL_FLG").ToString

            If sysDelFlg = "1" Then
                'ワーニング結果
                If MyBase.ShowMessage("W220") <> MsgBoxResult.Ok Then
                    Return False
                End If
                Return True
            End If
        Next
        Return True
    End Function
    '要望番号:997 terakawa 2012.10.22 End

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMC020C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMC020C.EventShubetsu.SINKI        '新規
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

            Case LMC020C.EventShubetsu.HENSHU       '編集
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

            Case LMC020C.EventShubetsu.FUKUSHA      '複写
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

            Case LMC020C.EventShubetsu.DEL          '削除
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

            Case LMC020C.EventShubetsu.HIKIATE      '引当
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

            Case LMC020C.EventShubetsu.TORIKESHI    '完了取消
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

            Case LMC020C.EventShubetsu.UNSO         '運送修正
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

            Case LMC020C.EventShubetsu.SHUSAN       '終算日修正
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

            Case LMC020C.EventShubetsu.MASTER       'マスタ参照
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

            Case LMC020C.EventShubetsu.HOZON        '保存
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

            Case LMC020C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMC020C.EventShubetsu.PRINT        '印刷
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

            Case LMC020C.EventShubetsu.TODOKESAKI   '届先
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

            Case LMC020C.EventShubetsu.INS_M        '追加（中）
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

            Case LMC020C.EventShubetsu.DEL_M       '削除（中）
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

            Case LMC020C.EventShubetsu.COPY_M       '複写（中）
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

            Case LMC020C.EventShubetsu.RIREKI       '履歴照会
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

            Case LMC020C.EventShubetsu.DEL_S        '削除（小）
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

            Case Else
                'すべての権限許可
                kengenFlg = True

        End Select

        Return kengenFlg

    End Function

    ''' <summary>
    ''' 完了取消時の次回分納チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <param name="msgParts"></param>
    ''' <returns></returns>
    Private Function TorikeshiKanrenCheck(ByVal ds As DataSet, ByRef msgParts As String()) As String

        Dim msgCd As String = ""

        ' 登録済みの次回分納データを削除するか否か
        Dim isDeleteJikaiBunnou As Boolean = False

        ds.Tables(LMC020C.TABLE_JIKAI_BUNNOU).Clear()

        With Me._Frm

            ' Rapidus次回分納情報取得

            Dim nrsBrCd As String = .cmbEigyosyo.SelectedValue().ToString()
            Dim outkaNoL As String = .lblSyukkaLNo.TextValue()

            ' Rapidus次回分納情報取得(戻り値1行: 出荷に紐づく EDI出荷単位)
            Dim dsZ390 As DataSet = Me._H.SelectJikaiBunnouInfo(nrsBrCd, outkaNoL)

            For i As Integer = 0 To dsZ390.Tables("LMZ390OUT").Rows.Count() - 1

                If dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_UMU").ToString() = LMConst.FLG.OFF Then
                    ' 次回分納なしならば以降は行わない。
                    Continue For
                End If

                ' 次回分納あり
                Dim jikaiBunnouOutkaCtlNo As String = dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_OUTKA_CTL_NO").ToString()
                Dim jikaiBunnouEdiCtlNo As String = dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_EDI_CTL_NO").ToString()

                Dim jissekiFlag As String = dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_JISSEKI_FLAG").ToString()
                Select Case jissekiFlag
                    Case ""
                        ' 次回分納の出荷指示EDIデータは存在するが
                        ' 同時に登録されたはずの EDI出荷 が存在しない場合(想定外)
                        msgCd = "E454"
                        msgParts = New String() {
                        "次回分納の出荷指示EDIデータのみ存在するデータ不整合状態", "実行",
                        String.Concat("(EDI管理番号:", jikaiBunnouEdiCtlNo, " ", ")", " ", "システム管理者に連絡してください。")}
                        Exit For

                    Case "0"
                        ' 次回分納の EDI出荷L 実績処理フラグが“未出力”の場合
                        If jikaiBunnouOutkaCtlNo.PadRight(8, " "c).Substring(1, 8) = New String("0"c, 8) Then
                            ' 次回分納が出荷未登録の場合

                            ' 登録済みの次回分納データを削除する
                            isDeleteJikaiBunnou = True

                        Else
                            ' 次回分納が出荷登録済の場合
                            If dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_SYS_DEL_FLG").ToString() = LMConst.FLG.ON Then
                                ' 次回分納出荷が削除済の場合

                                ' 登録済みの次回分納データを削除する
                                isDeleteJikaiBunnou = True

                            Else
                                ' 次回分納出荷が非削除の場合
                                msgCd = "E454"
                                msgParts = New String() {
                                "次回分納の出荷が登録済み", "完了取消",
                                String.Concat("出荷管理番号:", jikaiBunnouOutkaCtlNo, " ",
                                              "の出荷を、完了済みの場合は完了取消後に削除してから再度完了取消してください。")}
                                Exit For
                            End If
                        End If

                    Case "1"
                        ' ※E533「実績送信ありの荷主のため、報告済みデータは完了取消できません。」のチェックが先に行われるため、
                        ' 　実質的には下記チェックに該当することはない。

                        ' 次回分納の EDI出荷L 実績処理フラグが“出力済”の場合
                        msgCd = "E454"
                        msgParts = New String() {
                        "次回分納の出荷が登録済み", "完了取消",
                        String.Concat("EDI管理番号: ", jikaiBunnouEdiCtlNo, " ", "の「実績作成済⇒実績未」を実行してから再度完了取消してください。")}
                        Exit For

                    Case "2"
                        ' ※E533「実績送信ありの荷主のため、報告済みデータは完了取消できません。」のチェックが先に行われるため、
                        ' 　実質的には下記チェックに該当することはない。

                        ' 次回分納の EDI出荷L 実績処理フラグが“送信済”の場合
                        msgCd = "E454"
                        msgParts = New String() {
                        "次回分納の出荷が登録済み", "完了取消",
                        String.Concat("EDI管理番号: ", jikaiBunnouEdiCtlNo, " ", "の「実績送信済⇒実績未」を実行してから再度完了取消してください。")}
                        Exit For

                End Select
            Next

            If msgCd = "" AndAlso isDeleteJikaiBunnou Then
                ' 登録済みの次回分納データを削除する場合

                ' 次回分納情報の更新用テーブルへの設定
                For i As Integer = 0 To dsZ390.Tables("LMZ390OUT").Rows.Count() - 1
                    Dim dr As DataRow = ds.Tables(LMC020C.TABLE_JIKAI_BUNNOU).NewRow()

                    dr.Item("NRS_BR_CD") = .cmbEigyosyo.SelectedValue().ToString()
                    dr.Item("EDI_CTL_NO") = dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_EDI_CTL_NO")
                    dr.Item("CRT_DATE") = dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_CRT_DATE")
                    dr.Item("FILE_NAME") = dsZ390.Tables("LMZ390OUT").Rows(i).Item("JIKAI_BUNNOU_FILE_NAME")
                    dr.Item("EDI_L_SYS_UPD_DATE") = dsZ390.Tables("LMZ390OUT").Rows(i).Item("EDI_L_SYS_UPD_DATE")
                    dr.Item("EDI_L_SYS_UPD_TIME") = dsZ390.Tables("LMZ390OUT").Rows(i).Item("EDI_L_SYS_UPD_TIME")

                    ds.Tables(LMC020C.TABLE_JIKAI_BUNNOU).Rows.Add(dr)
                Next
            End If

        End With

        Return msgCd

    End Function

    '2017/09/25 修正 李↓ --- Jp.Co.Nrs.LM.Utility 参照したため
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList(ByVal sprDetail As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread) As ArrayList
        '2017/09/25 修正 李↑

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = 0
        If ("sprSyukkaM").Equals(sprDetail.Name) = True Then
            defNo = LMC020C.sprSyukkaMColumnIndex.DEFM
        Else
            defNo = LMC020C.sprDtlMColumnIndex.DEF
        End If

        '選択された行の行番号を取得
        Return _Vcon.SprSelectList(defNo, sprDetail)

    End Function

    ''' <summary>
    ''' 振替データチェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFurikae(ByVal ds As DataSet) As Boolean

        '種別取得
        Dim OUTkaTp As String = ds.Tables(LMC020C.TABLE_NM_OUT_L).Rows(0).Item("SYUBETU_KB").ToString()
        Dim msg As String = _Frm.FunctionKey.F3ButtonName.ToString

        If OUTkaTp = "50" Then

            Return Me._Vcon.SetErrMessage("E796", New String() {msg})
        End If

        Return True
    End Function

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理
            .txtNisyuTyumonNo.TextValue = Trim(.txtNisyuTyumonNo.TextValue)
            .txtKainusiTyumonNo.TextValue = Trim(.txtKainusiTyumonNo.TextValue)
            .txtUriCd.TextValue = Trim(.txtUriCd.TextValue)
            .txtOkuriNo.TextValue = Trim(.txtOkuriNo.TextValue)
            .txtTodokesakiCd.TextValue = Trim(.txtTodokesakiCd.TextValue)
            'START YANAI 要望番号909
            .txtTodokesakiCdOld.TextValue = Trim(.txtTodokesakiCdOld.TextValue)
            'END YANAI 要望番号909
            .txtTodokesakiNm.TextValue = Trim(.txtTodokesakiNm.TextValue)
            .txtTodokeTel.TextValue = Trim(.txtTodokeTel.TextValue)
            .txtNouhinTeki.TextValue = Trim(.txtNouhinTeki.TextValue)
            .txtSyukkaRemark.TextValue = Trim(.txtSyukkaRemark.TextValue)
            .txtOrderType.TextValue = Trim(.txtOrderType.TextValue)
            .txtTodokeAdderss1.TextValue = Trim(.txtTodokeAdderss1.TextValue)
            .txtTodokeAdderss2.TextValue = Trim(.txtTodokeAdderss2.TextValue)
            .txtTodokeAdderss3.TextValue = Trim(.txtTodokeAdderss3.TextValue)
            .txtHaisoRemark.TextValue = Trim(.txtHaisoRemark.TextValue)
            .txtOrderNo.TextValue = Trim(.txtOrderNo.TextValue)
            .txtRsvNo.TextValue = Trim(.txtRsvNo.TextValue)
            .txtSerialNo.TextValue = Trim(.txtSerialNo.TextValue)
            .txtCyumonNo.TextValue = Trim(.txtCyumonNo.TextValue)
            .txtGoodsRemark.TextValue = Trim(.txtGoodsRemark.TextValue)
            .txtSagyoM1.TextValue = Trim(.txtSagyoM1.TextValue)
            .txtSagyoM2.TextValue = Trim(.txtSagyoM2.TextValue)
            .txtSagyoM3.TextValue = Trim(.txtSagyoM3.TextValue)
            .txtSagyoM4.TextValue = Trim(.txtSagyoM4.TextValue)
            .txtSagyoM5.TextValue = Trim(.txtSagyoM5.TextValue)
            .txtDestSagyoM1.TextValue = Trim(.txtDestSagyoM1.TextValue)
            .txtDestSagyoM2.TextValue = Trim(.txtDestSagyoM2.TextValue)
            .txtUnsoCompanyCd.TextValue = Trim(.txtUnsoCompanyCd.TextValue)
            .txtUnsoSitenCd.TextValue = Trim(.txtUnsoSitenCd.TextValue)
            .txtUnthinTariffCd.TextValue = Trim(.txtUnthinTariffCd.TextValue)
            .txtExtcTariffCd.TextValue = Trim(.txtExtcTariffCd.TextValue)
            .txtSagyoL1.TextValue = Trim(.txtSagyoL1.TextValue)
            .txtSagyoL2.TextValue = Trim(.txtSagyoL2.TextValue)
            .txtSagyoL3.TextValue = Trim(.txtSagyoL3.TextValue)
            .txtSagyoL4.TextValue = Trim(.txtSagyoL4.TextValue)
            .txtSagyoL5.TextValue = Trim(.txtSagyoL5.TextValue)
        End With

    End Sub

    ''' <summary>
    ''' マイナス０を変換
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub ZeroTextValue()

        With Me._Frm
            '各項目のTrim処理
            .numPrtCnt.Value = System.Math.Abs(Convert.ToDecimal(.numPrtCnt.Value))
            .numPrtCnt_From.Value = System.Math.Abs(Convert.ToDecimal(.numPrtCnt_From.Value))
            .numPrtCnt_To.Value = System.Math.Abs(Convert.ToDecimal(.numPrtCnt_To.Value))
            .numKonpoKosu.Value = System.Math.Abs(Convert.ToDecimal(.numKonpoKosu.Value))
            .numSyukkaSouKosu.Value = System.Math.Abs(Convert.ToDecimal(.numSyukkaSouKosu.Value))
            .numPrintSort.Value = System.Math.Abs(Convert.ToDecimal(.numPrintSort.Value))
            .numIrime.Value = System.Math.Abs(Convert.ToDecimal(.numIrime.Value))
            .numPkgCnt.Value = System.Math.Abs(Convert.ToDecimal(.numPkgCnt.Value))
            .numKonsu.Value = System.Math.Abs(Convert.ToDecimal(.numKonsu.Value))
            .numHasu.Value = System.Math.Abs(Convert.ToDecimal(.numHasu.Value))
            .numIrisu.Value = System.Math.Abs(Convert.ToDecimal(.numIrisu.Value))
            .numSouKosu.Value = System.Math.Abs(Convert.ToDecimal(.numSouKosu.Value))
            .numHikiateKosuSumi.Value = System.Math.Abs(Convert.ToDecimal(.numHikiateKosuSumi.Value))
            .numHikiateKosuZan.Value = System.Math.Abs(Convert.ToDecimal(.numHikiateKosuZan.Value))
            .numSouSuryo.Value = System.Math.Abs(Convert.ToDecimal(.numSouSuryo.Value))
            .numHikiateSuryoSumi.Value = System.Math.Abs(Convert.ToDecimal(.numHikiateSuryoSumi.Value))
            .numHikiateSuryoZan.Value = System.Math.Abs(Convert.ToDecimal(.numHikiateSuryoZan.Value))
            .numJuryo.Value = System.Math.Abs(Convert.ToDecimal(.numJuryo.Value))
            .numKyori.Value = System.Math.Abs(Convert.ToDecimal(.numKyori.Value))

        End With

    End Sub

#End Region 'Method

End Class