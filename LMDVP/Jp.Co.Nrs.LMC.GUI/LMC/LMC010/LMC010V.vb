' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷管理
'  プログラムID     :  LMC010V : 出荷データ一覧
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.Win.Base

''' <summary>
''' LMC010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMC010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC010F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMCControlV

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC010F, ByVal v As LMCControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm
        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = v

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "入力チェック（検索）"

    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck() As Boolean

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

        Call Me.TrimSpaceTextValue()

        With Me._Frm

            '【単項目チェック】

            '******************** ヘッダ項目の入力チェック ********************

            '営業所
            '20151030 tsunehira add Start
            '英語化対応
            .cmbEigyo.ItemName() = .LmTitleLabel1.TextValue
            '20151030 tsunehira add End
            '.cmbEigyo.ItemName() = "営業所"
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '荷主コード
            '2017/09/25 修正 李↓
            .txtCustCD.ItemName() = lgm.Selector({"荷主コード", "Custmer Code", "하주코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCD.IsForbiddenWordsCheck() = True
            .txtCustCD.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtCustCD) = False Then
                Return False
            End If

            '担当者コード
            '2017/09/25 修正 李↓
            .txtPicCD.ItemName() = String.Concat(LMC010G.sprDetailDef.TANTO_USER.ColName, lgm.Selector({"コード", "Code", "코드", "中国語"}))
            '2017/09/25 修正 李↑

            .txtPicCD.IsForbiddenWordsCheck() = True
            .txtPicCD.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtPicCD) = False Then
                Return False
            End If

            '出荷検索From
            If .imdSearchDate_From.IsDateFullByteCheck(8) = False Then

                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E626")
                '2015.10.29 tusnehira add End
                'MyBase.ShowMessage("E038", New String() {"出荷検索From", "8"})
                Return False
            End If

            '出荷検索日To
            If .imdSearchDate_To.IsDateFullByteCheck(8) = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E627")
                '2015.10.29 tusnehira add End
                'MyBase.ShowMessage("E038", New String() {"出荷検索To", "8"})
                Return False
            End If

            '印刷検索From
            If .imdPrintDate_From.IsDateFullByteCheck(8) = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E628")
                '2015.10.29 tusnehira add End
                'MyBase.ShowMessage("E038", New String() {"印刷検索From", "8"})
                Return False
            End If

            '印刷検索日To
            If .imdPrintDate_To.IsDateFullByteCheck(8) = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E629")
                '2015.10.29 tusnehira add End
                'MyBase.ShowMessage("E038", New String() {"印刷検索To", "8"})
                Return False
            End If

            '出荷管理番号（大）（ダイレクト検索）
            .txtOutkaNoL.ItemName() = .lblOutkaNoL.Name
            .txtOutkaNoL.IsForbiddenWordsCheck() = True
            .txtOutkaNoL.IsByteCheck() = 9
            If MyBase.IsValidateCheck(.txtOutkaNoL) = False Then
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'オーダー番号
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.CUST_ORD_NO.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.CUST_ORD_NO.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "オーダー番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.CUST_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.CUST_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "荷主名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'START YANAI 要望番号748
            '荷主コード（小）
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.CUST_CD_S.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.CUST_CD_S.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "小ＣＤ"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'END YANAI 要望番号748

            '届先名
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.DEST_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.DEST_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "届先名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '商品（中1）
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.GOODS_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.GOODS_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "商品（中1）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先住所
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.DEST_AD.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.DEST_AD.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "届先住所"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '送状番号
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.DENP_NO.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.DENP_NO.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "送状番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.UNSOCO_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.UNSOCO_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "運送会社名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add start
            '注文番号
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.BUYER_ORD_NO.ColNo)
            vCell.ItemName() = LMC010G.sprDetailDef.BUYER_ORD_NO.ColName
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            '2018/04/20 001547 【LMS】出荷データ検索画面_注文番号項目追加(千葉BC物管２_石井) Annen add end

            '出荷管理番号（大）
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.OUTKA_NO_L.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "出荷管理番号（大）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）
            'ロット№
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.LOT_NO_S.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.LOT_NO_S.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "ロット№"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'シリアル№
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.SERIAL_NO_S.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.SERIAL_NO_S.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "シリアル№"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷検索にロット、シリアル）

            '配送時注意事項　'要望番号1856対応　2013/02/21　本明
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.REMARK_UNSO.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.REMARK_UNSO.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "配送時注意事項"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作成者
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.SYS_ENT_USER.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.SYS_ENT_USER.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "作成者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '最終更新者
            vCell.SetValidateCell(0, LMC010G.sprDetailDef.SYS_UPD_USER.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = LMC010G.sprDetailDef.SYS_UPD_USER.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "最終更新者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索時入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuKanrenCheck() As Boolean

        With Me._Frm

            '出荷検索日From + 出荷検索日To

            '出荷検索日From + 出荷検索日To相関チェック
            If Me._Vcon.IsRelationChk(.imdSearchDate_From.TextValue, .imdSearchDate_To.TextValue) = False Then
                '
                If String.IsNullOrEmpty(.cmbSearchDate.SelectedValue.ToString()) = True Then

                    '英語化対応
                    '20151021 tsunehira add
                    Me.ShowMessage("E662")
                    'Me.ShowMessage("E199", New String() {"出荷検索日区分"})

                    Me.SetErrorControl(.cmbSearchDate)
                    Return False

                End If

            End If

            '出荷検索日Fromより出荷検索日Toが過去日の場合エラー
            'いずれも設定済である場合のみチェック
            If Me._Vcon.IsFromToChk(.imdSearchDate_From.TextValue, .imdSearchDate_To.TextValue) = False Then

                '出荷検索日Fromより出荷検索日Toが過去日の場合エラー
                '20151029 tsunehira add Start
                '英語化対応
                Me.ShowMessage("E631")
                'Me.ShowMessage("E039", New String() {"出荷検索日To", "出荷検索日From"})

                .imdSearchDate_To.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me.SetErrorControl(.imdSearchDate_From)
                Return False

            End If

            'START YANAI 要望番号917
            ''印刷状況 + 印刷種別相関チェック
            'If Me._Vcon.IsRelationChk(.cmbPrintStatus.SelectedValue.ToString(), .cmbPrintSyubetu.SelectedValue.ToString()) = False Then
            '    Me.ShowMessage("E017", New String() {"印刷状況", "印刷種別"})
            '    .cmbPrintSyubetu.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
            '    Me.SetErrorControl(.cmbPrintStatus)
            '    Return False
            'End If
            'END YANAI 要望番号917

            '印刷検索日From + 印刷検索日To

            '印刷検索日Fromより印刷検索日Toが過去日の場合エラー
            'いずれも設定済である場合のみチェック
            If Me._Vcon.IsFromToChk(.imdPrintDate_From.TextValue, .imdPrintDate_To.TextValue) = False Then

                '印刷検索日Fromより印刷検索日Toが過去日の場合エラー
                '20151029 tsunehira add Start
                '英語化対応
                Me.ShowMessage("E632")
                'Me.ShowMessage("E039", New String() {"印刷検索日To", "印刷検索日From"})
                '20151029 tsunehira add Start
                .imdPrintDate_To.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me.SetErrorControl(.imdPrintDate_From)
                Return False

            End If

        End With

        Return True

    End Function

#End Region '入力チェック（検索）

#Region "入力チェック（新規）"

    ''' <summary>
    ''' 新規時、入力チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSinkiInputCheck(ByVal key As String) As Boolean

        If String.IsNullOrEmpty(key) = True Then

            '荷主が選択されなかった場合、エラー
            MyBase.ShowMessage("E193")    'エラーメッセージ
            Return False

        End If

        Return True

    End Function

    '要望番号:1568 terakawa 2013.01.17 Start
    ''' <summary>
    ''' 新規時、単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsShinkiSingleCheck() As Boolean
        With Me._Frm

            '【単項目チェック】

            '出荷予定日
            If .imdShinkiOutkaDate.IsDateFullByteCheck(8) = False Then
                '20151029 tsunehira add Start
                '英語化対応
                MyBase.ShowMessage("E630")
                'MyBase.ShowMessage("E038", New String() {"出荷予定日", "8"})
            End If

        End With

        Return True

    End Function
    '要望番号:1568 terakawa 2013.01.17 End

#End Region '入力チェック（新規）

#Region "入力チェック（印刷）"

    ''' <summary>
    ''' 印刷イベントの入力チェック
    ''' </summary>
    ''' <param name="printShubetsu">印刷種別</param>
    ''' <param name="arr">リスト</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPrintInputCheck(ByVal printShubetsu As LMC010C.PrintShubetsu, ByVal arr As ArrayList, Optional EventShubetsu As LMC010C.EventShubetsu = LMC010C.EventShubetsu.PRINT) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '【単項目チェック】

            If EventShubetsu = LMC010C.EventShubetsu.JIKKOU Then

                '選択チェック
                If Me.IsSelectDataChk(arr) = False Then
                    Return False
                End If

            Else

                '印刷種別
                '2017/09/25 修正 李↓
                .cmbPrint.ItemName() = lgm.Selector({"印刷種別", "Printing type", "인쇄종별", "中国語"})
                '2017/09/25 修正 李↑

                .cmbPrint.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbPrint) = False Then
                    Return False
                End If

                '選択チェック
                If Me.IsSelectDataChk(arr) = False Then
                    Return False
                End If

                'START YANAI 要望番号497
                ''振替管理番号チェック
                'If Me.IsFurikaeCheck(printShubetsu, LMC010C.SprColumnIndex.FURI_NO, arr) = False Then
                '    Return False
                'End If
                'END YANAI 要望番号497

                'START YANAI 要望番号1156 埼玉浮間合成修正依頼
                ''纏め送状印刷チェック
                'Dim msg As String = Me.PrintDenpGRPCheck(printShubetsu, arr)
                'If String.IsNullOrEmpty(msg) = False Then

                '    MyBase.ShowMessage("E144", New String() {msg})
                '    Return False

                'End If
                'END YANAI 要望番号1156 埼玉浮間合成修正依頼

                'START YANAI 要望番号1184 まとめ送状・荷札時、荷主明細存在チェック
                '纏め送状・荷札印刷チェック
                If Me.IsMatomeCustDetailCheck(printShubetsu, arr) = False Then
                    Return False
                End If
                'END YANAI 要望番号1184 まとめ送状・荷札時、荷主明細存在チェック

                '出荷報告印刷チェック
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                If Me.IsKonzaiCheck(printShubetsu, LMC010C.SprColumnIndex.CUST_CD_L, arr) = False Then
#Else
                If Me.IsKonzaiCheck(printShubetsu, LMC010G.sprDetailDef.CUST_CD_L.ColNo, arr) = False Then
#End If
                    MyBase.ShowMessage("E115")
                    'MyBase.ShowMessage("E115", New String() {"荷主"})
                    Return False
                End If

            'START YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう
            '未引当チェック
            If Me.IsAlctdNbChk(printShubetsu, arr) = False Then
                Return False
            End If
            'END YANAI 要望番号1158 一覧画面　引当前の出荷で納品書の印刷が出来てしまう

            End If

        End With

        Return True

    End Function

    'START YANAI 要望番号800
    ''' <summary>
    ''' 印刷イベントの入力チェック(荷札の場合のみ)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPrintNihudaInputCheck(ByVal ds As DataSet) As Boolean

        With Me._Frm

            '【単項目チェック】
            If 0 < ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows.Count Then
                If ("0").Equals(ds.Tables(LMC010C.TABLE_NM_IN_PRINT).Rows(0).Item("OUTKA_PKG_NB").ToString) = True Then
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E633")
                    'MyBase.ShowMessage("E175", New String() {"梱包数が0のため、荷札"})
                    Return False
                    '20151029 tsunehira add End
                End If
            End If

        End With

        Return True

    End Function
    'END YANAI 要望番号800

#End Region '入力チェック（印刷）

#Region "入力チェック（変更）"

    ''' <summary>
    ''' 変更時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsHenkoInputCheck(ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Call Me.TrimSpaceTextValue()

        With Me._Frm

            '【単項目チェック】

            '運送会社コード
            '2017/09/25 修正 李↓
            .txtTrnCD.ItemName() = lgm.Selector({"運送会社コード", "TPTN company code", "운송회사코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtTrnCD.IsHissuCheck() = True
            .txtTrnCD.IsForbiddenWordsCheck() = True
            .txtTrnCD.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtTrnCD) = False Then
                Return False
            End If

            '運送会社支店コード
            '2017/09/25 修正 李↓
            .txtTrnBrCD.ItemName() = lgm.Selector({"運送会社支店コード", "TPTN company branch code", "운송회사지점코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtTrnBrCD.IsHissuCheck() = True
            .txtTrnBrCD.IsForbiddenWordsCheck() = True
            .txtTrnBrCD.IsByteCheck() = 3
            If MyBase.IsValidateCheck(.txtTrnBrCD) = False Then
                Return False
            End If

            '【関連チェック】

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '上限チェック
            If Me.IsSelectLimitChk(arr.Count, LMC010C.IKKATU_HENKO) = False Then
                Return False
            End If

            '混在チェック（営業所）
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                        Dim eigyo As String = .sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(0)), LMC010C.SprColumnIndex.NRS_BR_CD).Value.ToString()
#Else
            Dim eigyo As String = .sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(0)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Value.ToString()

#End If
            If eigyo.Equals(LMUserInfoManager.GetNrsBrCd) = False Then

                '20151021 tsunehira add
                MyBase.ShowMessage("E661")
                'MyBase.ShowMessage("E178", New String() {"一括変更処理"})
                Return False
            End If

            '運送会社コード + 運送会社支店コード
            Dim trnCd As String = .txtTrnCD.TextValue()
            Dim trnBrCd As String = .txtTrnBrCD.TextValue()
            Dim whereStr As String = "SYS_DEL_FLG = '0'"
            whereStr = whereStr & " AND NRS_BR_CD = '"
            '修正 2015.05.20 営業所またぎ処理のため画面値より営業所コード取得
            'whereStr = whereStr & LMUserInfoManager.GetNrsBrCd() & "'"
            whereStr = whereStr & .cmbEigyo.SelectedValue.ToString() & "'"
            whereStr = whereStr & " AND UNSOCO_CD = '" & trnCd & "'"
            whereStr = whereStr & " AND UNSOCO_BR_CD = '" & trnBrCd & "'"

            '運送会社マスタCheck
            If Me._Vcon.IsExistMst(LMConst.CacheTBL.UNSOCO, whereStr) = False Then
                '20151020 tsunehira add
                Me.ShowMessage("E658", New String() {trnCd & trnBrCd})
                'Me.ShowMessage("E079", New String() {"運送会社マスタ", trnCd & trnBrCd})
                .txtTrnBrCD.BackColorDef = Utility.LMGUIUtility.GetAttentionBackColor()
                Me.SetErrorControl(.txtTrnCD)
                Return False
            End If

        End With

        Return True

    End Function

#End Region '入力チェック（変更）

#Region "入力チェック（実行）"

    ''' <summary>
    ''' 実行時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouInputCheck(ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '【単項目チェック】

            '実行種別
            '2017/09/25 修正 李↓
            .cmbJikkou.ItemName() = lgm.Selector({"実行種別", "Execution type", "실행종별", "中国語"})
            '2017/09/25 修正 李↑

            .cmbJikkou.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbJikkou) = False Then
                Return False
            End If

            '社内入荷データ作成 terakawa Start
            '社内入荷データ作成先
            If ("09").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then

                '2017/09/25 修正 李↓
                .cmbIntEdi.ItemName() = lgm.Selector({"社内入荷データ作成先", "create in-houseI/D data", "사내 입하데이터 작성처", "中国語"})
                '2017/09/25 修正 李↑

                .cmbIntEdi.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbIntEdi) = False Then
                    Return False
                End If
            End If
            '社内入荷データ作成 terakawa End

            '【関連チェック】

            'ハネウェル対応 yamanaka 2013.02.14 Start
            '選択チェック
            'If ("02").Equals(Me._Frm.cmbJikkou.SelectedValue.ToString()) = False Then
            '    If Me.IsSelectDataChk(arr) = False Then
            '        Return False
            '    End If
            'End If
            Select Case Me._Frm.cmbJikkou.SelectedValue.ToString()
                '2015.06.22 協立化学　作業料対応START
                '2017.09.20 EDI届先変更追加対応START
                Case "02", "12", "13", "21", "23"
                    'Case "02", "12", "13"
                    '2017.09.20 EDI届先変更追加対応END
                    '2015.06.22 協立化学　作業料対応END
                    If Me.IsSelectDataChk(arr) = False Then
                        Return False
                    End If
            End Select

            'If Me.IsSelectDataChk(arr) = False Then
            '    Return False
            'End If
            'ハネウェル対応 yamanaka 2013.02.14 End

            '上限チェック
            If Me.IsSelectLimitChk(arr.Count, LMC010C.IKKATU_JIKKO) = False Then
                Return False
            End If

            'START YANAI 要望番号773　報告書Excel対応
            '進捗区分チェック
            If IsStateKbChk2(arr) = False Then
                Return False
            End If
            'END YANAI 要望番号773　報告書Excel対応

            '社内入荷データ作成 terakawa Start
            '複数荷主選択チェック
            If ("09").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                If Me.IsSelectCustChk(arr) = False Then
                    Return False
                End If
            End If
            '社内入荷データ作成 terakawa End

            '2014.04.21 CALT対応 黎 --ST--
            Select Case Me._Frm.cmbJikkou.SelectedValue.ToString()
                Case "12"

                    Dim brCd As String = String.Empty
                    Dim whCd As String = String.Empty
                    Dim max As Integer = arr.Count - 1
                    Dim kbnDr As DataRow() = Nothing
                    Dim inkaStateKb As String = String.Empty

                    '明細
                    For i As Integer = 0 To max

                        '倉庫チェック
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                        brCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD))
                        whCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.WH_CD))
#Else
                        brCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                        whCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.WH_CD.ColNo))
#End If

                        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S102'", "AND KBN_NM1 = '", brCd, "' AND KBN_NM2 = '", whCd, "'"))

                        If kbnDr.Length = 0 Then
                            '20151029 tsunehira add Start
                            '英語化対応
                            MyBase.ShowMessage("E635")
                            'MyBase.ShowMessage("E336", New String() {"倉庫コードが区分マスタに登録", "入荷予定データ作成"})
                            Return False
                        End If

                    Next
            End Select
            '2014.04.21 CALT対応 黎 --ED--

            If ("01").Equals(.cmbJikkou.SelectedValue) = True Then
                ' 一括引当

                ' 分納出荷チェック
                Dim SyubetuKb As String
                For i As Integer = 0 To arr.Count() - 1
                    SyubetuKb = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYUBETU_KB.ColNo))
                    If SyubetuKb = "60" Then
                        MyBase.ShowMessage("E028",
                        New String() {
                            lgm.Selector({String.Concat("(", arr(i), "行目) 分納出荷"), String.Concat("Partial shipment", " (line ", arr(i), ")"), "", ""}),
                            .cmbJikkou.SelectedText})
                        Return False
                    End If

                Next
            End If

        End With

        Return True

    End Function


    Friend Function IsHikiModeCheck(ByVal arr As ArrayList) As String

        With Me._Frm


            Dim max As Integer = arr.Count - 1

            '明細
            For i As Integer = 0 To max

                '倉庫チェック
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                Dim nrsBrCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD))
                Dim custCdL As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_L))
#Else
                Dim nrsBrCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                Dim custCdL As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
#End If

                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
                                                                                                   "' AND CUST_CD = '", custCdL, _
                                                                                                   "' AND SUB_KB = '84'"))
                '届先コードのプラントチェック
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                Dim destCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.DEST_CD))
#Else
                Dim destCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.DEST_CD.ColNo))
#End If

                Dim kbnDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'F019'", "AND KBN_NM1 = '", destCd, "' AND SYS_DEL_FLG = '0'"))

                If custDetailsDr.Length > 0 AndAlso kbnDr.Length = 0 Then
                    Return LMConst.FLG.ON

                End If



            Next

            Return LMConst.FLG.OFF
        End With


    End Function

    ''' <summary>
    ''' 一括引当時のイエローカード同時印刷チェック
    ''' </summary>
    ''' <param name="arr"></param>
    ''' <returns></returns>
    Friend Function IsYCardModeCheck(ByVal arr As ArrayList) As String

        With Me._Frm
            For i As Integer = 0 To arr.Count - 1
                '荷主明細の「一括引き当て：イエローカード印刷フラグ」をチェック
                Dim nrsBrCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                Dim custCdL As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
                Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd,
                                                                                                   "' AND CUST_CD = '", custCdL,
                                                                                                   "' AND SUB_KB = 'A3'"))

                If custDetailsDr.Length > 0 Then
                    Return LMConst.FLG.ON
                End If
            Next
        End With

        Return LMConst.FLG.OFF

    End Function

    Friend Function IsShijiPrtModeCheck(ByVal arr As ArrayList, intCnt As Integer) As String

        With Me._Frm


            Dim max As Integer = arr.Count - 1

            For i As Integer = 0 To max
                If IsNumeric(arr(i)) = True AndAlso Convert.ToInt32(arr(i)) = intCnt Then
                    '倉庫チェック
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    Dim nrsBrCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD))
                    Dim custCdL As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_L))
#Else
                    Dim nrsBrCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                    Dim custCdL As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
#End If

                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
                                                                                                        "' AND CUST_CD = '", custCdL, _
                                                                                                        "' AND SUB_KB = '89'"))
                    If Not custDetailsDr Is Nothing AndAlso custDetailsDr.Count > 0 Then
                        '届先コードのプラントチェック
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                        Dim destCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.DEST_CD))
#Else
                        Dim destCd As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.DEST_CD.ColNo))
#End If
                        Dim strDESTCD_List As String = custDetailsDr(0).Item("SET_NAIYO").ToString & "," & custDetailsDr(0).Item("SET_NAIYO_2").ToString

                        If custDetailsDr.Length > 0 AndAlso strDESTCD_List.Length > 0 AndAlso strDESTCD_List.Contains(destCd).Equals(True) Then
                            Return LMConst.FLG.OFF

                        End If
                    End If

                End If
            Next






            Return LMConst.FLG.ON
        End With


    End Function

    '顧客WEB出荷登録バッチ対応 t.kido Start
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 顧客WEB出荷登録
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsWebOutkaSingleCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            '【単項目チェック】

            '営業所
            .cmbEigyo.ItemName() = .LmTitleLabel1.TextValue
            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

            '倉庫
            .cmbSoko.ItemName() = .LmTitleLabel2.TextValue
            .cmbSoko.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbSoko) = False Then
                Return False
            End If

            '荷主コード
            .txtCustCD.ItemName() = lgm.Selector({"荷主コード", "Custmer Code", "하주 코드", "中国語"})
            .txtCustCD.IsForbiddenWordsCheck() = True
            .txtCustCD.IsHissuCheck() = True
            .txtCustCD.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtCustCD) = False Then
                Return False
            End If

            '【関連チェック】

            '荷主コード存在チェック
            Dim custDr() As DataRow = Nothing
            custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                              "NRS_BR_CD = '", .cmbEigyo.SelectedValue.ToString(), "' AND " _
                                                                             , "CUST_CD_L = '", .txtCustCD.TextValue(), "' AND " _
                                                                             , "CUST_CD_M = '00' AND " _
                                                                             , "CUST_CD_S = '00' AND " _
                                                                             , "CUST_CD_SS = '00' AND " _
                                                                             , "SYS_DEL_FLG = '0'"))
            If 0 = custDr.Length Then
                MyBase.ShowMessage("E773")
                .txtCustCD.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtCustCD.Focus()
                Return False
            End If

        End With

        Return True

    End Function
    '顧客WEB出荷登録バッチ対応 t.kido End

    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 名変入荷作成
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsMeihenInkaSingleCheck() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            '【単項目チェック】

            '荷主コード（大）（振替先）
            .txtShinkiCustCdL.ItemName() = lgm.Selector({"振替先の荷主コード（大）", "Custmer Code", "하주 코드", "中国語"})
            .txtShinkiCustCdL.IsForbiddenWordsCheck() = True
            .txtShinkiCustCdL.IsHissuCheck() = True
            .txtShinkiCustCdL.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtShinkiCustCdL) = False Then
                Return False
            End If

            '荷主コード（中）（振替先）
            .txtShinkiCustCdM.ItemName() = lgm.Selector({"振替先の荷主コード（中）", "Custmer Code", "하주 코드", "中国語"})
            .txtShinkiCustCdM.IsForbiddenWordsCheck() = True
            .txtShinkiCustCdM.IsHissuCheck() = True
            .txtShinkiCustCdM.IsByteCheck() = 2
            If MyBase.IsValidateCheck(.txtShinkiCustCdM) = False Then
                Return False
            End If

            '【関連チェック】

            '荷主コード存在チェック
            Dim custDr() As DataRow = Nothing
            custDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(String.Concat( _
                                                                              "NRS_BR_CD = '", .cmbEigyo.SelectedValue.ToString(), "' AND " _
                                                                             , "CUST_CD_L = '", .txtShinkiCustCdL.TextValue(), "' AND " _
                                                                             , "CUST_CD_M = '", .txtShinkiCustCdM.TextValue(), "' AND " _
                                                                             , "CUST_CD_S = '00' AND " _
                                                                             , "CUST_CD_SS = '00' AND " _
                                                                             , "SYS_DEL_FLG = '0'"))
            If 0 = custDr.Length Then
                MyBase.ShowMessage("E773")
                .txtShinkiCustCdL.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                .txtShinkiCustCdL.Focus()
                .txtShinkiCustCdM.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 現場作業指示
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsWHSagyoshijiSingleCheck(ByVal arr As ArrayList) As Boolean

        'Dim max As Integer = arr.Count() - 1

        'For i As Integer = 0 To max
        '    With Me._Frm.sprDetail.Sheets(0)
        '        '検品済み以外の場合エラー
        '        If Not (LMC010C.SINTYOKU50).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = True Then
        '            Me.ShowMessage("E991", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
        '            MyBase.se("E991", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
        '            Return False
        '        End If

        '    End With
        'Next

        Return True
    End Function

#End Region '入力チェック（実行）

#Region "入力チェック（完了）"

    ''' <summary>
    ''' 完了時入力チェック（入力チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanryoInputCheck(ByVal G As LMC010G, ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '【単項目チェック】

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '上限チェック
            If Me.IsSelectLimitChk(arr.Count, LMC010C.IKKATU_KANRYO) = False Then
                Return False
            End If

            '【残数チェック】
            If Me.IsZanDataChk(G, arr) = False Then
                Return False
            End If

            '進捗区分チェック
            If Me.IsStateKbChk(arr) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region '入力チェック（完了）

#Region "入力チェック（マスタ参照）"

    ''' <summary>
    ''' マスタ参照時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsRefMstInputCheck() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Call Me.TrimSpaceTextValue()

        With Me._Frm

            '【単項目チェック】

            '2017/09/25 修正 李↓
            .cmbEigyo.ItemName() = lgm.Selector({"営業所", "Office", "오피스", "中国語"})
            '2017/09/25 修正 李↑

            .cmbEigyo.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbEigyo) = False Then
                Return False
            End If

        End With

        Return True

    End Function

#End Region '入力チェック（マスタ参照）

#If True Then
#Region "運送会社帳票印刷 入力チェック"

    Friend Function IsTrapoPrintStart(ByVal arr As ArrayList) As Boolean

        With _Frm
            .cmbTrapoPrint.IsHissuCheck = True
            .cmbTrapoPrint.ItemName = .grpTrapoPrint.Text

            If MyBase.IsValidateCheck(.cmbTrapoPrint) = False Then
                Return False
            End If

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

        End With


        Return True

    End Function


#End Region
#End If



#Region "内部メソッド"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMC010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMC010C.EventShubetsu.SINKI           '新規
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

            Case LMC010C.EventShubetsu.KENSAKU         '検索
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

            Case LMC010C.EventShubetsu.MASTER        '取込
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

            Case LMC010C.EventShubetsu.DEF_CUST       '初期荷主
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

            Case LMC010C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMC010C.EventShubetsu.HENKO         '変更
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

            Case LMC010C.EventShubetsu.PRINT         '印刷
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

            Case LMC010C.EventShubetsu.JIKKOU        '実行
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

            Case LMC010C.EventShubetsu.KANRYO        '完了
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

            Case LMC010C.EventShubetsu.DOUBLE_CLICK        '完了
                '10:閲覧者の場合エラー
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
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                Dim defNo As Integer = LMC010C.SprColumnIndex.DEF
#Else
        Dim defNo As Integer = LMC010G.sprDetailDef.DEF.ColNo
#End If

        Return Me._Vcon.SprSelectList(defNo, Me._Frm.sprDetail)

    End Function

    ''' <summary>
    ''' 選択チェック（+チェックリストセット）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectDataChk(ByVal arr As ArrayList) As Boolean

        '選択チェック
        If Me._Vcon.IsSelectChk(arr.Count) = False Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 残数チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsZanDataChk(ByVal G As LMC010G, ByVal arr As ArrayList) As Boolean

        '残数が0以外ならエラー
        Dim max As Integer = arr.Count() - 1
        Dim nb As String = String.Empty
        Dim qt As String = String.Empty

        For i As Integer = 0 To max
            With Me._Frm.sprDetail.Sheets(0)
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                nb = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.BACKLOG_NB))
                qt = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.BACKLOG_QT))
#Else
                nb = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.BACKLOG_NB.ColNo))
                qt = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.BACKLOG_QT.ColNo))
#End If

                If "0".Equals(nb) = False OrElse "0.000".Equals(qt) = False Then
                    '英語化対応
                    '20151021 tsunehira add
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    MyBase.ShowMessage("E668", New String() {String.Concat(String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L))))})
#Else
                    MyBase.ShowMessage("E668", New String() {String.Concat(String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))))})
#End If

                    'MyBase.ShowMessage("E391", New String() {String.Concat(String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]"))})

                    Return False
                End If

            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 選択上限チェック
    ''' </summary>
    ''' <param name="count">選択件数</param>
    ''' <param name="kbn">区分コード</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSelectLimitChk(ByVal count As Integer, ByVal kbn As String) As Boolean

        Dim limitData As Integer = Me.GetLimitData(kbn)

        '選択した件数が多い場合、エラー
        If limitData < count Then
            MyBase.ShowMessage("E168", New String() {count.ToString(), limitData.ToString()})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 閾値の取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="kbnGroup">区分グループコード　初期値 = "I004"</param>
    ''' <returns>閾値</returns>
    ''' <remarks></remarks>
    Private Function GetLimitData(ByVal kbnCd As String, Optional ByVal kbnGroup As String = LMKbnConst.KBN_I004) As Integer

        GetLimitData = 0

        Dim drs As DataRow() = Me.SelectKbnListDataRow(kbnCd, kbnGroup)
        If 0 < drs.Length Then
            GetLimitData = Convert.ToInt32(Convert.ToDouble(drs(0).Item("VALUE1")))
        End If

        Return GetLimitData

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnListDataRow(ByVal kbnCd As String _
                                         , ByVal groupCd As String _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(kbnCd, groupCd))

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal kbnCd As String _
                                     , ByVal groupCd As String _
                                     ) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")

        '区分コード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_CD = ", " '", kbnCd, "' ")

        '区分グループコード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        Return SelectKbnString

    End Function

    'START YANAI 要望番号1184 まとめ送状・荷札時、荷主明細存在チェック
    ''' <summary>
    ''' 荷主明細マスタチェック(纏め送状・荷札)
    ''' </summary>
    ''' <param name="printShubetsu">印刷種別</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsMatomeCustDetailCheck(ByVal printShubetsu As LMC010C.PrintShubetsu, ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '纏め送状・荷札以外、スルー
        If LMC010C.PrintShubetsu.DENP_GRP <> printShubetsu AndAlso _
            LMC010C.PrintShubetsu.NIHUDA_GRP <> printShubetsu Then
            Return True
        End If

        With Me._Frm.sprDetail.ActiveSheet

            '初期値格納変数
            Dim custCdL As String = String.Empty
            '要望番号:1253 terakawa 2012.07.13 Start
            Dim nrsBrCd As String = String.Empty
            '要望番号:1253 terakawa 2012.07.13 End
            Dim max As Integer = arr.Count - 1

            For i As Integer = 0 To max
                custCdL = .Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo).Text
                '要望番号:1253 terakawa 2012.07.13 Start
                nrsBrCd = .Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo).Text
                'If Me.SelectCustDetailListDataRow("34", "00", custCdL, String.Empty, String.Empty, String.Empty, "01", String.Empty, String.Empty) = False Then
                If Me.SelectCustDetailListDataRow(nrsBrCd, "34", "00", custCdL, String.Empty, String.Empty, String.Empty, "01", String.Empty, String.Empty) = False Then
                    '要望番号:1253 terakawa 2012.07.13 End

                    '2017/09/25 修正 李↓
                    MyBase.ShowMessage("E209", New String() {String.Concat(lgm.Selector({"荷主コード=", "Custmer Code=", "하주코드=", "中国語"}), custCdL)})
                    '2017/09/25 修正 李↑

                    Return False
                End If
            Next

        End With

        Return True

    End Function
    'END YANAI 要望番号1184 まとめ送状・荷札時、荷主明細存在チェック

    ''' <summary>
    ''' 混在チェック
    ''' </summary>
    ''' <param name="printShubetsu">印刷種別</param>
    ''' <param name="colNo">対象項目のカラム№</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKonzaiCheck(ByVal printShubetsu As LMC010C.PrintShubetsu, ByVal colNo As Integer, ByVal arr As ArrayList) As Boolean

        '出荷報告印刷以外、スルー
        If LMC010C.PrintShubetsu.HOKOKU <> printShubetsu Then
            Return True
        End If

        With Me._Frm.sprDetail.ActiveSheet

            '初期値格納変数
            Dim first As String = .Cells(Convert.ToInt32(arr(0)), colNo).Text()
            Dim max As Integer = arr.Count - 1

            For i As Integer = 0 To max

                If first.Equals(.Cells(Convert.ToInt32(arr(i)), colNo).Text()) = False Then
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 未引当チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAlctdNbChk(ByVal printShubetsu As LMC010C.PrintShubetsu, ByVal arr As ArrayList) As Boolean

        '納品書・一括・出荷指示印刷以外、スルー
        If LMC010C.PrintShubetsu.NHS <> printShubetsu AndAlso _
            LMC010C.PrintShubetsu.ALL_PRINT <> printShubetsu AndAlso _
             LMC010C.PrintShubetsu.TACHIAI <> printShubetsu Then
            Return True
        End If

        '最小引当数が0ならエラー
        Dim max As Integer = arr.Count() - 1
        Dim nb As String = String.Empty
        Dim qt As String = String.Empty

        '2013.02.18 アグリマート対応 START
        Dim nrsBrCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        '2013.02.18 アグリマート対応 END

        For i As Integer = 0 To max
            With Me._Frm.sprDetail.Sheets(0)
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                nb = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.MIN_ALCTD_NB))
                qt = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.MIN_ALCTD_QT))
#Else
                nb = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.MIN_ALCTD_NB.ColNo))
                qt = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.MIN_ALCTD_QT.ColNo))
#End If

                '2013.02.18 アグリマート対応 START
                If LMC010C.PrintShubetsu.NHS = printShubetsu Then
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    nrsBrCd = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD))
                    custCdL = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_L))
                    custCdM = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_M))
#Else
                    nrsBrCd = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo))
                    custCdL = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo))
                    custCdM = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_M.ColNo))
#End If

                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, _
                                                                                                     "' AND CUST_CD = '", String.Concat(custCdL, custCdM), _
                                                                                                     "' AND SUB_KB = '67'"))
                    If 0 < custDetailsDr.Length Then
                        Continue For
                    End If
                End If
                '2013.02.18 アグリマート対応 END

                If ("0").Equals(nb) = True AndAlso ("0.000").Equals(qt) = True Then
                    Dim SyubetuKb As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SYUBETU_KB.ColNo))
                    Dim outkaStateKb As String = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))
                    Dim isMihikiateError As Boolean = True
                    If SyubetuKb = "60" AndAlso Convert.ToDecimal(LMC020C.SINTYOKU50) <= Convert.ToDecimal(outkaStateKb) Then
                        ' 2025/10/03 分納出荷を考慮した条件変更(出荷指示書)
                        ' 出荷種別が“分納”かつ作業進捗を“検品済”以降であれば、次回分納出荷対象となる正常データのためエラーとしない。
                        isMihikiateError = False
                    End If
                    If isMihikiateError Then
                        '英語化対応
                        '20151021 tsunehira add
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                                        MyBase.ShowMessage("E676", New String() {String.Concat(String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L))))})
#Else
                        MyBase.ShowMessage("E676", New String() {String.Concat(String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo))))})

#End If
                        'MyBase.ShowMessage("E491", New String() {String.Concat(String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]"))})
                        Return False
                    End If
                End If

            End With
        Next

        Return True

    End Function

    ''' <summary>
    ''' 振替管理番号チェック
    ''' </summary>
    ''' <param name="printShubetsu">印刷種別</param>
    ''' <param name="colNo">対象項目のカラム№</param>
    ''' <param name="arr">リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFurikaeCheck(ByVal printShubetsu As LMC010C.PrintShubetsu, ByVal colNo As Integer, ByVal arr As ArrayList) As Boolean

        With Me._Frm.sprDetail.ActiveSheet

            Dim max As Integer = arr.Count - 1

            For i As Integer = 0 To max

                '振替管理番号が設定されていたらエラー
                If String.IsNullOrEmpty(.Cells(Convert.ToInt32(arr(i)), colNo).Text()) = False Then
                    '英語化対応
                    '20151021 tsunehira add
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    MyBase.ShowMessage("E669", New String() {String.Concat(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L).Text())})
#Else
                    MyBase.ShowMessage("E669", New String() {String.Concat(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo).Text())})
#End If
                    'MyBase.ShowMessage("E399", New String() {"印刷", String.Concat("[出荷管理番号=", .Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L).Text(), "]")})

                    Return False
                End If

            Next

        End With

        Return True

    End Function

    'START YANAI 要望番号1156 埼玉浮間合成修正依頼
    '''' <summary>
    '''' 纏め送状印刷時の入力チェック
    '''' </summary>
    '''' <param name="printShubetsu">印刷種別</param>
    '''' <param name="arr">リスト</param>
    '''' <returns></returns>
    '''' <remarks>String.Empty:エラーなし,OK  左記以外:エラーあり</remarks>
    'Private Function PrintDenpGRPCheck(ByVal printShubetsu As LMC010C.PrintShubetsu, ByVal arr As ArrayList) As String

    '    '纏め送状印刷以外、スルー
    '    If LMC010C.PrintShubetsu.DENP_GRP <> printShubetsu Then
    '        Return String.Empty
    '    End If

    '    '纏め送状関連チェック
    '    With Me._Frm.sprDetail.ActiveSheet

    '        '初期値格納変数
    '        Dim rowNo As Integer = Convert.ToInt32(arr(0))
    '        Dim outkaPlanDate As String = .Cells(Convert.ToInt32(rowNo), LMC010C.SprColumnIndex.OUTKA_PLAN_DATE).Text()
    '        Dim arrPlanDate As String = .Cells(Convert.ToInt32(rowNo), LMC010C.SprColumnIndex.ARR_PLAN_DATE).Text()
    '        Dim custCdL As String = .Cells(Convert.ToInt32(rowNo), LMC010C.SprColumnIndex.CUST_CD_L).Text()
    '        Dim destCd As String = .Cells(Convert.ToInt32(rowNo), LMC010C.SprColumnIndex.DEST_CD).Text()
    '        Dim unsoCd As String = .Cells(Convert.ToInt32(rowNo), LMC010C.SprColumnIndex.UNSO_CD).Text()

    '        Dim max As Integer = arr.Count - 1

    '        For i As Integer = 0 To max

    '            rowNo = Convert.ToInt32(arr(i))
    '            If outkaPlanDate.Equals(.Cells(rowNo, LMC010C.SprColumnIndex.OUTKA_PLAN_DATE).Text()) = False Then
    '                Return "出荷日"
    '            End If

    '            If arrPlanDate.Equals(.Cells(rowNo, LMC010C.SprColumnIndex.ARR_PLAN_DATE).Text()) = False Then
    '                Return "納入日"
    '            End If

    '            If custCdL.Equals(.Cells(rowNo, LMC010C.SprColumnIndex.CUST_CD_L).Text()) = False Then
    '                Return "荷主"
    '            End If

    '            If destCd.Equals(.Cells(rowNo, LMC010C.SprColumnIndex.DEST_CD).Text()) = False Then
    '                Return "届先"
    '            End If

    '            If unsoCd.Equals(.Cells(rowNo, LMC010C.SprColumnIndex.UNSO_CD).Text()) = False Then
    '                Return "運送会社"
    '            End If

    '        Next

    '    End With

    '    Return String.Empty

    'End Function
    'END YANAI 要望番号1156 埼玉浮間合成修正依頼

    ''' <summary>
    ''' 進捗区分チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsStateKbChk(ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count() - 1

        For i As Integer = 0 To max
            With Me._Frm.sprDetail.Sheets(0)
                '出荷済
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                                If (LMC010C.SINTYOKU60).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = True Then
                    '英語化対応
                    '20151021 tsunehira add

                    Me.ShowMessage("E671", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
#Else
                If (LMC010C.SINTYOKU60).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))) = True Then
                    '英語化対応
                    '20151021 tsunehira add

                    Me.ShowMessage("E671", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
#End If
                    '                 Me.ShowMessage("E400", New String() {"出荷済", String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                    Return False
                End If

                '報告済
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                If (LMC010C.SINTYOKU90).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = True Then
#Else
                If (LMC010C.SINTYOKU90).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))) = True Then
#End If
                    '英語化対応
                    '20151021 tsunehira add
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    Me.ShowMessage("E672", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
#Else
                    Me.ShowMessage("E672", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
#End If
                    'Me.ShowMessage("E400", New String() {"報告済", String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                    Return False
                End If

            End With
        Next

        Return True

    End Function

    'START YANAI 要望番号773　報告書Excel対応
    ''' <summary>
    ''' 進捗区分チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsStateKbChk2(ByVal arr As ArrayList) As Boolean

        Dim max As Integer = arr.Count() - 1
        'START YANAI 20120322 特別梱包個数計算
        Dim custDetailsDr() As DataRow = Nothing
        Dim flg As String = String.Empty
        'END YANAI 20120322 特別梱包個数計算

        For i As Integer = 0 To max
            With Me._Frm.sprDetail.Sheets(0)
                '出荷済以外の場合エラー
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                If ("05").Equals(Me._Frm.cmbJikkou.SelectedValue) = True AndAlso _
                    (LMC010C.SINTYOKU60).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = False Then
#Else
                If ("05").Equals(Me._Frm.cmbJikkou.SelectedValue) = True AndAlso _
                    (LMC010C.SINTYOKU60).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))) = False Then
#End If
                    '英語化対応
                    '20151021 tsunehira add
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    Me.ShowMessage("E673", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
#Else
                    Me.ShowMessage("E673", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
#End If
                    'Me.ShowMessage("E443", New String() {"出荷済", String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                    Return False
                End If

                'START YANAI 20120322 特別梱包個数計算
                '出荷済・報告済みの場合エラー
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    '出荷済
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                    If (LMC010C.SINTYOKU60).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = True Then
#Else
                    If (LMC010C.SINTYOKU60).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))) = True Then
#End If
                        '英語化対応
                        '20151021 tsunehira add
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                        Me.ShowMessage("E637", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
#Else
                        Me.ShowMessage("E637", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
#End If
                        Return False
                    End If

                    '報告済
#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                If (LMC010C.SINTYOKU90).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = True Then
                    '英語化対応
                    '20151021 tsunehira add
                    Me.ShowMessage("E638", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
                    Return False
                End If

#Else
                    If (LMC010C.SINTYOKU90).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))) = True Then
                        '英語化対応
                        '20151021 tsunehira add
                        Me.ShowMessage("E638", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
                        Return False
                    End If

#End If
                End If

#If False Then      'UPD 2019/08/07 006921   【LMS】バグ_出荷データ検索でスプレッド移動すると完了処理が行えない(YCC竹下)T15-14 
                                '振替管理番号が設定されていたらエラー
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.FURI_NO))) = False Then
                        '英語化対応
                        '20151021 tsunehira add
                        Me.ShowMessage("E670", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
                        'Me.ShowMessage("E399", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})

                        Return False
                    End If
                End If

                '運賃確定済チェック
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    flg = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.SEIQ_FIXED_FLAG))
                    If String.IsNullOrEmpty(flg) = False AndAlso flg.Equals("00") = False Then
                        '20141021 tsunehira add
                        Me.ShowMessage("E660", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
                        'Me.ShowMessage("E126", New String() {String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                        Return False
                    End If
                End If

                '自営業所チェック
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    '削除 2015.05.20 営業所またぎ処理のため営業所チェックを削除
                    'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD))) = False Then
                    '    Me.ShowMessage("E178", New String() {Me._Frm.cmbJikkou.SelectedText})
                    '    Return False
                    'End If
                End If

                '荷主明細マスタチェック
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    '要望番号:1253 terakawa 2012.07.13 Start
                    'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_L)), "' AND ", _
                    '                                                                                                "SUB_KB = '22' AND SET_NAIYO = '01'"))
                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD)), "' AND ", _
                                                                                                                    "CUST_CD = '", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_L)), "' AND ", _
                                                                                                                    "SUB_KB = '22' AND SET_NAIYO = '01'"))
                    '要望番号:1253 terakawa 2012.07.13 End

                    If custDetailsDr.Length = 0 Then
                        '20151030 tsunehira add Start
                        '英語化対応
                        Me.ShowMessage("E833", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
                        '20151030 tsunehira add End
                        'Me.ShowMessage("E209", New String() {String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                        Return False
                    End If
                End If
                'END YANAI 20120322 特別梱包個数計算

                '社内入荷データ作成 terakawa Start
                '予定入力が含まれていた場合、エラー
                If ("09").Equals(Me._Frm.cmbJikkou.SelectedValue) = True AndAlso _
                    (LMC010C.SINTYOKU10).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_STATE_KB))) = True Then
                    '英語化対応
                    '20151021 tsunehira add
                    Me.ShowMessage("E639", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)))})
                    Return False
                End If
                '社内入荷データ作成 terakawa End

#Else
                '振替管理番号が設定されていたらエラー
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.FURI_NO.ColNo))) = False Then
                        '英語化対応
                        '20151021 tsunehira add
                        Me.ShowMessage("E670", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
                        'Me.ShowMessage("E399", New String() {Me._Frm.cmbJikkou.SelectedText, String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})

                        Return False
                    End If
                End If

                '運賃確定済チェック
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    flg = Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.SEIQ_FIXED_FLAG.ColNo))
                    If String.IsNullOrEmpty(flg) = False AndAlso flg.Equals("00") = False Then
                        '20141021 tsunehira add
                        Me.ShowMessage("E660", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
                        'Me.ShowMessage("E126", New String() {String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                        Return False
                    End If
                End If

                '自営業所チェック
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    '削除 2015.05.20 営業所またぎ処理のため営業所チェックを削除
                    'If LMUserInfoManager.GetNrsBrCd().Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.NRS_BR_CD))) = False Then
                    '    Me.ShowMessage("E178", New String() {Me._Frm.cmbJikkou.SelectedText})
                    '    Return False
                    'End If
                End If

                '荷主明細マスタチェック
                If ("07").Equals(Me._Frm.cmbJikkou.SelectedValue) = True Then
                    '要望番号:1253 terakawa 2012.07.13 Start
                    'custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("CUST_CD = '", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.CUST_CD_L)), "' AND ", _
                    '                                                                                                "SUB_KB = '22' AND SET_NAIYO = '01'"))
                    custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.NRS_BR_CD.ColNo)), "' AND ", _
                                                                                                                    "CUST_CD = '", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo)), "' AND ", _
                                                                                                                    "SUB_KB = '22' AND SET_NAIYO = '01'"))
                    '要望番号:1253 terakawa 2012.07.13 End

                    If custDetailsDr.Length = 0 Then
                        '20151030 tsunehira add Start
                        '英語化対応
                        Me.ShowMessage("E833", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
                        '20151030 tsunehira add End
                        'Me.ShowMessage("E209", New String() {String.Concat("[出荷管理番号=", Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010C.SprColumnIndex.OUTKA_NO_L)), "]")})
                        Return False
                    End If
                End If
                'END YANAI 20120322 特別梱包個数計算

                '社内入荷データ作成 terakawa Start
                '予定入力が含まれていた場合、エラー
                If ("09").Equals(Me._Frm.cmbJikkou.SelectedValue) = True AndAlso _
                    (LMC010C.SINTYOKU10).Equals(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_STATE_KB.ColNo))) = True Then
                    '英語化対応
                    '20151021 tsunehira add
                    Me.ShowMessage("E639", New String() {String.Concat(Me._Vcon.GetCellValue(.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.OUTKA_NO_L.ColNo)))})
                    Return False
                End If
                '社内入荷データ作成 terakawa End

#End If

            End With
        Next

        Return True

    End Function
    'END YANAI 要望番号773　報告書Excel対応

    'START YANAI 要望番号1184 まとめ送状・荷札時、荷主明細存在チェック
    ''' <summary>
    ''' 荷主明細マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustDetailListDataRow(ByVal nrsBrCd As String, _
                                                ByVal subKb As String, _
                                                ByVal custClass As String, _
                                                ByVal custCdL As String, _
                                                Optional ByVal custCdM As String = "", _
                                                Optional ByVal custCdS As String = "", _
                                                Optional ByVal custCdSS As String = "", _
                                                Optional ByVal setNaiyo As String = "", _
                                                Optional ByVal setNaiyo2 As String = "", _
                                                Optional ByVal setNaiyo3 As String = "") As Boolean


        Dim custCd As String = String.Empty
        If ("00").Equals(custClass) = True Then
            custCd = custCdL
        ElseIf ("01").Equals(custClass) = True Then
            custCd = String.Concat(custCdL, custCdM)
        ElseIf ("02").Equals(custClass) = True Then
            custCd = String.Concat(custCdL, custCdM, custCdS)
        ElseIf ("03").Equals(custClass) = True Then
            custCd = String.Concat(custCdL, custCdM, custCdS, custCdSS)
        End If

        Dim sql As String = String.Empty

        '要望番号:1253 terakawa 2012.07.13 Start
        'sql = String.Concat("SUB_KB = '", subKb, "' AND ", _
        '                    "CUST_CD = '", custCd, "'")
        sql = String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                            "SUB_KB = '", subKb, "' AND ", _
                            "CUST_CD = '", custCd, "'")
        '要望番号:1253 terakawa 2012.07.13 End

        If String.IsNullOrEmpty(setNaiyo) = False Then
            sql = String.Concat(sql, " AND SET_NAIYO ='", setNaiyo, "'")
        End If
        If String.IsNullOrEmpty(setNaiyo2) = False Then
            sql = String.Concat(sql, " AND SET_NAIYO_2 ='", setNaiyo2, "'")
        End If
        If String.IsNullOrEmpty(setNaiyo3) = False Then
            sql = String.Concat(sql, " AND SET_NAIYO_3 ='", setNaiyo3, "'")
        End If

        'キャッシュテーブルからデータ抽出
        Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sql)
        If custDetailsDr.Length = 0 Then
            Return False
        End If

        Return True

    End Function
    'END YANAI 要望番号1184 まとめ送状・荷札時、荷主明細存在チェック

    '社内入荷データ作成 terakawa Start
    ''' <summary>
    ''' 複数荷主選択チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectCustChk(ByVal arr As ArrayList) As Boolean

        Dim custCdL As String = String.Empty
        Dim custCdLChk As String = String.Empty
        Dim max As Integer = arr.Count - 2

        For i As Integer = 0 To max
            '選択チェック
            custCdL = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMC010G.sprDetailDef.CUST_CD_L.ColNo)).ToString()
            custCdLChk = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i + 1)), LMC010G.sprDetailDef.CUST_CD_L.ColNo)).ToString()
            If custCdL.Equals(custCdLChk) = False Then
                '英語化対応
                '20151021 tsunehira add
                MyBase.ShowMessage("E679")
                'MyBase.ShowMessage("E527", New String() {"荷主", "社内入荷データ作成"})
                Return False
            End If
        Next
        Return True

    End Function
    '社内入荷データ作成 terakawa End

#End Region '内部メソッド

#Region "ユーティリティ"

    ''' <summary>
    ''' エラー項目の背景色とフォーカスを設定する
    ''' </summary>
    ''' <param name="ctl">エラーコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetErrorControl(ByVal ctl As Control)

        Dim errorColor As System.Drawing.Color = Utility.LMGUIUtility.GetAttentionBackColor()

        If TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).BackColorDef = errorColor

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).BackColorDef = errorColor


        End If

        ctl.Focus()
        ctl.Select()

    End Sub

#End Region

#Region "Trim"

    ''' <summary>
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理
            .txtCustCD.TextValue = Trim(.txtCustCD.TextValue)
            .txtPicCD.TextValue = Trim(.txtPicCD.TextValue)
            .txtTrnCD.TextValue = Trim(.txtTrnCD.TextValue)

            'スプレッドのスペース除去
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

        End With

    End Sub

#End Region

    'START KIM 特定荷主対応 2012/9/20
#Region "入力チェック（ハネウェルCSV引当）"

    ''' <summary>
    ''' フォルダ存在チェック
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFolderExist(ByVal filePath As String) As Boolean

        If System.IO.Directory.Exists(filePath) = False Then

            'フォルダが存在しない場合、エラーにする
            '英語化対応
            '20151021 tsunehira add
            Me.ShowMessage("E663")
            'Me.ShowMessage("E296", New String() {"C:\LMUSER"})
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' ファイル存在チェック
    ''' </summary>
    ''' <param name="filePath"></param>
    ''' <param name="fileName"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFileExist(ByVal filePath As String, ByVal fileName As String) As Boolean

        If System.IO.File.Exists(String.Concat(filePath, fileName)) = False Then

            'ファイルが存在しない場合、エラーにする
            '英語化対応
            '20151021 tsunehira add
            Me.ShowMessage("E674")
            'Me.ShowMessage("E469", New String() {"取込対象のファイル"})
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 必須チェック（CSV項目)
    ''' </summary>
    ''' <param name="strText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCSVHissuCheck(ByVal strText As String) As Boolean

        Me._Frm.txtFile.TextValue = strText
        Me._Frm.txtFile.IsHissuCheck() = True
        If MyBase.IsValidateCheck(Me._Frm.txtFile) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 禁止文字チェック（CSV項目)
    ''' </summary>
    ''' <param name="strText"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCSVForbiddenWordsCheck(ByVal strText As String) As Boolean

        Me._Frm.txtFile.TextValue = strText
        Me._Frm.txtFile.IsForbiddenWordsCheck() = True
        If MyBase.IsValidateCheck(Me._Frm.txtFile) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フルバイトチェック（CSV項目)
    ''' </summary>
    ''' <param name="strText"></param>
    ''' <param name="cnt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCSVFullByteCheck(ByVal strText As String, ByVal cnt As Integer) As Boolean

        Me._Frm.txtFile.TextValue = strText
        Me._Frm.txtFile.IsFullByteCheck() = cnt
        If MyBase.IsValidateCheck(Me._Frm.txtFile) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' バイトチェック（CSV項目)
    ''' </summary>
    ''' <param name="strText"></param>
    ''' <param name="cnt"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCSVByteCheck(ByVal strText As String, ByVal cnt As Integer) As Boolean

        Me._Frm.txtFile.TextValue = strText
        Me._Frm.txtFile.IsByteCheck() = cnt
        If MyBase.IsValidateCheck(Me._Frm.txtFile) = False Then
            Return False
        End If

        Return True

    End Function

#End Region
    'END KIM 特定荷主対応 2012/9/20

#End Region 'Method

End Class
