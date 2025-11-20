' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB010    : 入荷データ検索
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
''' LMB010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMB010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMB010F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMBControlV

    '2017/09/25 修正 李↓
    '    ''' <summary>
    '    ''' 選択した言語を格納するフィールド
    '    ''' </summary>
    '    ''' <remarks></remarks>
    '#If False Then '_LangFlgが設定される前にアクセスして例外が発生する問題に対応 20151106 INOUE
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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMB010F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm
        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMBControlV(handlerClass, DirectCast(frm, LMFormSxga))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 検索時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKensakuSingleCheck(ByVal g As LMB010G) As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '2017/09/25 修正 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 修正 李↑

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

            '2017/09/25 修正 李↓
            '荷主コード
            .txtCustCD.ItemName() = lgm.Selector({"荷主コード", "Custmer Code", "하주 코드", "中国語"})
            '2017/09/25 修正 李↑

            .txtCustCD.IsForbiddenWordsCheck() = True
            .txtCustCD.IsByteCheck() = 5
            If MyBase.IsValidateCheck(.txtCustCD) = False Then
                Return False
            End If

            '入荷日From
            If .imdNyukaDate_From.IsDateFullByteCheck(8) = False Then
                MyBase.ShowMessage("E613")
                Return False
            End If

            '入荷日To
            If .imdNyukaDate_To.IsDateFullByteCheck(8) = False Then
                        MyBase.ShowMessage("E614")
                        Return False
            End If

            '入荷管理番号（大）（ダイレクト検索）
            .txtInkaNoL.ItemName() = .lblInkaNoL.Name
            .txtInkaNoL.IsForbiddenWordsCheck() = True
            .txtInkaNoL.IsByteCheck() = 9
            If MyBase.IsValidateCheck(.txtInkaNoL) = False Then
                Return False
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprDetail)

            'オーダー番号
            vCell.SetValidateCell(0, g.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "オーダー番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主名
            vCell.SetValidateCell(0, g.sprDetailDef.CUST_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.CUST_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "荷主名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'START YANAI 要望番号748
            '荷主コード（小）
            vCell.SetValidateCell(0, g.sprDetailDef.CUST_CD_S.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.CUST_CD_S.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "小ＣＤ"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'END YANAI 要望番号748

            '商品名
            vCell.SetValidateCell(0, g.sprDetailDef.GOODS_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.GOODS_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "商品名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'コメント
            vCell.SetValidateCell(0, g.sprDetailDef.REMARK.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.REMARK.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "備考大（社内）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '出荷元
            vCell.SetValidateCell(0, g.sprDetailDef.DEST_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.DEST_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "出荷元"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '運送会社名
            vCell.SetValidateCell(0, g.sprDetailDef.UNSOCO_NM.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.UNSOCO_NM.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "運送会社名"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 122
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '入荷管理番号（大）
            vCell.SetValidateCell(0, g.sprDetailDef.INKA_NO_L.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.INKA_NO_L.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "入荷管理番号（大）"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 9
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '注文番号
            vCell.SetValidateCell(0, g.sprDetailDef.BUYER_ORD_NO_L.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.BUYER_ORD_NO_L.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "注文番号"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 30
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'START YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）
            'ロット№
            vCell.SetValidateCell(0, g.sprDetailDef.LOT_NO.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.LOT_NO.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "ロット№"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'シリアル№
            vCell.SetValidateCell(0, g.sprDetailDef.SERIAL_NO.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.SERIAL_NO.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "シリアル№"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If
            'END YANAI 要望番号1286 入荷・出荷画面の機能追加（入荷検索にロット、シリアル）

            '担当者
            vCell.SetValidateCell(0, g.sprDetailDef.TANTO_USER.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.TANTO_USER.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "担当者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '作成者
            vCell.SetValidateCell(0, g.sprDetailDef.SYS_ENT_USER.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.SYS_ENT_USER.ColName
            '20151030 tsunehira add End
            'vCell.ItemName() = "作成者"
            vCell.IsForbiddenWordsCheck() = True
            vCell.IsByteCheck() = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '最終更新者
            vCell.SetValidateCell(0, g.sprDetailDef.SYS_UPD_USER.ColNo)
            '20151030 tsunehira add Start
            '英語化対応
            vCell.ItemName() = g.sprDetailDef.SYS_UPD_USER.ColName
            '20151030 tsunehira add End
            vCell.ItemName() = "最終更新者"
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

            '入荷日From + 入荷日To

            '  入荷日Fromより入荷日Toが過去日の場合エラー()
            '  いずれも設定済 である場合のみチェック

            If .imdNyukaDate_From.TextValue.Equals(String.Empty) = False _
                And .imdNyukaDate_To.TextValue.Equals(String.Empty) = False Then

                If .imdNyukaDate_To.TextValue < .imdNyukaDate_From.TextValue Then

                    '入荷日Fromより入荷日Toが過去日の場合エラー
                    'Me.ShowMessage("E039", New String() {"入荷日To", "入荷日From"})
                    Me.ShowMessage("E615")
                    .imdNyukaDate_From.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdNyukaDate_To.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .imdNyukaDate_From.Focus()
                    Return False

                End If

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 完了時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKanryoSingleCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            Dim max As Integer = arr.Count - 1
            Dim spr As Win.Spread.LMSpread = .sprDetail
            Dim stage As String = String.Empty
            Dim recNo As Integer = 0

            For i As Integer = 0 To max

                recNo = Convert.ToInt32(arr(i))

                Select Case Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(recNo, g.sprDetailDef.INKA_STATE_KB.ColNo))

                    Case LMB010C.INKASTATEKB_10
                        
                        '英語化対応
                        '20151021 tsunehira add
                        MyBase.ShowMessage("E649")
                        'MyBase.ShowMessage("E378", New String() {"予定"})
                        Return False

                    Case LMB010C.INKASTATEKB_50, LMB010C.INKASTATEKB_90

                        '2017/09/25 修正 李↓

                        MyBase.ShowMessage("E400", New String() {Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(recNo, g.sprDetailDef.STATUS_NM.ColNo)), String.Concat(
                                           lgm.Selector({"[入荷管理番号", "[Stock control number = ", "[입하관리번호 = ", "中国語"}),
                                           Me._Vcon.GetCellValue(spr.ActiveSheet.Cells(recNo, g.sprDetailDef.INKA_NO_L.ColNo)), "]")})

                        '2017/09/25 修正 李↑

                End Select

            Next

        End With

        Return True

    End Function

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

    'START YANAI メモ②No.28
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouSingleCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '【単項目チェック】

            ''実行種別
            '.cmbJikkou.ItemName() = "実行種別"
            '.cmbJikkou.IsHissuCheck() = True
            'If MyBase.IsValidateCheck(.cmbJikkou) = False Then
            '    Return False
            'End If

            '【関連チェック】

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '上限チェック
            If Me.IsSelectLimitChk(arr.Count, LMB010C.IKKATU_JIKKO) = False Then
                Return False
            End If

            'START YANAI メモ②No.28
            '進捗区分チェック
            Dim max As Integer = arr.Count - 1
            Dim stateKb As String = String.Empty
            Dim stateNm As String = String.Empty
            For i As Integer = 0 To max
                stateKb = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.INKA_STATE_KB.ColNo))
                'START YANAI 要望番号420
                'If ("50").Equals(stateKb) = True OrElse _
                '    ("90").Equals(stateKb) = True Then
                If ("50").Equals(stateKb) = False AndAlso _
                    ("90").Equals(stateKb) = False Then
                    'END YANAI 要望番号420
                    stateNm = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.STATUS_NM.ColNo))
                    'START YANAI 要望番号420

                    '2017/09/25 修正 李↓
                    MyBase.ShowMessage("E423", New String() {stateNm, String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))})
                    '2017/09/25 修正 李↑

                    'END YANAI 要望番号420
                    Return False
                End If
            Next
            'END YANAI メモ②No.28

        End With

        Return True

    End Function

    ''' <summary>
    ''' 実行時入力チェック（関連目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouKanrenCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim max As Integer = arr.Count - 1

        Dim kbn() As DataRow = Nothing
        Dim ediCust() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim whCd As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim orderNoL As String = String.Empty
        Dim orderNoM As String = String.Empty
        Dim inkaTpCd As String = String.Empty

        With Me._Frm

            '【関連項目チェック】
            For i As Integer = 0 To max

                nrsBrCd = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.NRS_BR_CD.ColNo))
                whCd = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.WH_CD.ColNo))
                custCdL = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.CUST_CD_L.ColNo))
                custCdM = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.CUST_CD_M.ColNo))
                orderNoL = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo))
                orderNoM = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.OUTKA_FROM_ORD_NO_M.ColNo))
                inkaTpCd = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.INKA_TP_CD.ColNo))

                '荷主チェック（整合性チェック）
                'デュポン荷主の取得
                kbn = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'D005' AND ", _
                                                                                             "KBN_NM4 = '", nrsBrCd, "' AND ", _
                                                                                             "KBN_NM5 = '", custCdL, "' AND ", _
                                                                                             "KBN_NM6 = '", custCdM, "'" _
                                                                                            ))
                If kbn.Length = 0 Then

                    '2017/09/25 修正 李↓
                    MyBase.ShowMessage("E209", New String() {String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))})
                    '2017/09/25 修正 李↑

                    Return False
                End If

                '荷主チェック（EDI荷主チェック）
                'EDI荷主の取得
                ediCust = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EDI_CUST).Select(String.Concat("INOUT_KB = '1' AND ", _
                                                                                                      "NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                                      "WH_CD = '", whCd, "' AND ", _
                                                                                                      "CUST_CD_L = '", custCdL, "' AND ", _
                                                                                                      "CUST_CD_M = '", custCdM, "'" _
                                                                                                     ))
                If ediCust.Length = 0 Then

                    '2017/09/25 修正 李↓
                    MyBase.ShowMessage("E415", New String() {String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))})
                    '2017/09/25 修正 李↑

                End If

                'オーダー番号(大)未入力チェック
                If String.IsNullOrEmpty(orderNoL) = True Then
                    '20151020 tsunehira add
                    MyBase.ShowMessage("E650", New String() {String.Concat(Convert.ToInt32(arr(i)))})
                    'MyBase.ShowMessage("E416", New String() {"入荷(大)のオーダー番号", String.Concat(Convert.ToInt32(arr(i)), "行目")})

                End If

                'オーダー番号(大)整合性チェック
                '20151020 tsunehira add
                If MyBase.ShowMessage("W236", New String() {arr(i).ToString}) = MsgBoxResult.Cancel Then
                    Return False
                End If
                'If MyBase.ShowMessage("W179", New String() {String.Concat("入荷(大)のオーダー番号(", Convert.ToInt32(arr(i)), "行目)"), "10"}) = MsgBoxResult.Cancel Then
                '    Return False
                'End If

                '入荷種別チェック
                If ("10").Equals(inkaTpCd) = False Then

                    '2017/09/25 修正 李↓
                    MyBase.ShowMessage("E417", New String() {String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))})
                    '2017/09/25 修正 李↑

                End If

                'オーダー番号(中)未入力チェック
                If String.IsNullOrEmpty(orderNoM) = True Then
                    '20151020 tsunehira add
                    MyBase.ShowMessage("E651", New String() {String.Concat(Convert.ToInt32(arr(i)))})
                    'MyBase.ShowMessage("E416", New String() {"入荷(中)のオーダー番号", String.Concat(Convert.ToInt32(arr(i)), "行目")})


                End If

                'オーダー番号(中)桁数チェック
                If orderNoM.Length <> 6 Then
                    '20151020 tsunehira add
                    MyBase.ShowMessage("E652", New String() {String.Concat(Convert.ToInt32(arr(i)))})
                    'MyBase.ShowMessage("E418", New String() {"入荷(中)のオーダー番号", "6", String.Concat(Convert.ToInt32(arr(i)), "行目")})
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 実行時入力チェック（関連目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkouKanrenCheck2(ByVal ds As DataSet, ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim max As Integer = arr.Count - 1

        Dim inkaEdi() As DataRow = Nothing
        Dim nrsBrCd As String = String.Empty
        Dim inkaNoL As String = String.Empty
        Dim orderNoL As String = String.Empty

        Dim ediL1cnt As Integer = ds.Tables(LMB010C.TABLE_NM_OUT_EDI_L1).Rows.Count
        Dim ediL2cnt As Integer = ds.Tables(LMB010C.TABLE_NM_OUT_EDI_L2).Rows.Count
        Dim inkaScnt As Integer = ds.Tables(LMB010C.TABLE_NM_OUT_INKAS).Rows.Count

        With Me._Frm

            '【関連項目チェック】
            For i As Integer = 0 To max

                nrsBrCd = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.NRS_BR_CD.ColNo))
                inkaNoL = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.INKA_NO_L.ColNo))
                orderNoL = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.OUTKA_FROM_ORD_NO_L.ColNo))

                If 0 < ediL1cnt Then
                    'EDI入荷データ存在チェック
                    'EDIデータの取得
                    inkaEdi = ds.Tables(LMB010C.TABLE_NM_OUT_EDI_L1).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                          "INKA_NO_L = '", inkaNoL, "'" _
                                                                                         ))
                    If 0 < inkaEdi.Length Then

                        '2017/09/25 修正 李↓
                        MyBase.ShowMessage("E419", New String() {String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))})
                        '2017/09/25 修正 李↑

                    End If
                End If

                If 0 < ediL2cnt AndAlso _
                    String.IsNullOrEmpty(orderNoL) = False Then
                    'オーダー番号EDI入荷データ存在チェック
                    'EDIデータの取得
                    inkaEdi = ds.Tables(LMB010C.TABLE_NM_OUT_EDI_L2).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                          "OUTKA_FROM_ORD_NO_L = '", orderNoL, "'" _
                                                                                          ))
                    If 0 < inkaEdi.Length Then

                        '2017/09/25 修正 李↓
                        If MyBase.ShowMessage("W180", New String() {String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))}) = MsgBoxResult.Cancel Then
                            Return False
                        End If
                        '2017/09/25 修正 李↑

                    End If
                End If

                If 0 < inkaScnt Then
                    '簿外品区分チェック
                    inkaEdi = ds.Tables(LMB010C.TABLE_NM_OUT_INKAS).Select(String.Concat("NRS_BR_CD = '", nrsBrCd, "' AND ", _
                                                                                         "INKA_NO_L = '", inkaNoL, "' AND ", _
                                                                                         "OFB_KB <> '01'" _
                                                                                        ))
                    If 0 < inkaEdi.Length Then

                        '2017/09/25 修正 李↓
                        MyBase.ShowMessage("E420", New String() {String.Concat(Convert.ToInt32(arr(i)), lgm.Selector({"行目", "Lines", "줄째", "中国語"}))})
                        '2017/09/25 修正 李↑

                    End If
                End If

            Next

        End With

        Return True

    End Function

    'EMD YANAI メモ②No.28

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 作業一括削除・作成時
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSagyoSingleCheck(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '【単項目チェック】

            ''実行種別
            '.cmbJikkou.ItemName() = "実行種別"
            '.cmbJikkou.IsHissuCheck() = True
            'If MyBase.IsValidateCheck(.cmbJikkou) = False Then
            '    Return False
            'End If

            '【関連チェック】

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    'START YANAI 20120121 作業一括処理対応
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal arr As ArrayList) As Boolean

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

        End With

        Return True

    End Function
    'END YANAI 20120121 作業一括処理対応

    'START YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 作業一括削除・作成時
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsOutkaSingleCheck(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '【関連チェック】

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 Start
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 作成時(東レUTIのみ)
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsUtiSingleCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        With Me._Frm

            '【関連チェック】
            Dim custCd As String = String.Empty
            Dim custList As New ArrayList
            Dim destNm1 As String = String.Empty
            Dim destNm2 As String = String.Empty
            Dim max As Integer = arr.Count - 1
            Dim custDetailsDr As DataRow() = Nothing

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '荷主明細チェック
            For i As Integer = 0 To max
                custCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.CUST_CD_L.ColNo))
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, _
                                                                                                                "' AND CUST_CD = '", custCd, _
                                                                                                                "' AND SUB_KB = '50'"))
                '20151020 tsunehira add
                If custDetailsDr.Length = 0 Then
                    MyBase.ShowMessage("E618", New String() {custCd})
                End If


                custList.Add(custCd)
            Next

            'UTI修正 2012.12.11 yamanaka Start
            '同一出荷元チェック
            'If max > 0 Then
            '    destNm1 = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(0)), LMB010G.sprDetailDef.DEST_NM.ColNo))

            '    For i As Integer = 1 To max
            '        destNm2 = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMB010G.sprDetailDef.DEST_NM.ColNo))


            '        If custList(0).ToString().Equals(custList(i).ToString()) = False Then
            '            MyBase.ShowMessage("E375", New String() {"荷主コードが違う", "処理"})
            '            Return False
            '        End If

            '        If destNm1.Equals(destNm2) = False Then
            '            MyBase.ShowMessage("E375", New String() {"出荷元が違う", "処理"})
            '            Return False
            '        End If

            '    Next

            'End If
            'UTI修正 2012.12.11 yamanaka End

        End With

        Return True

    End Function
    '出荷データ作成(東レUTI専用)対応 yamanaka 2012.11.28 End

    '入荷報告取消(東レUTI専用)対応 s.kobayashi 
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 取り消し時(東レUTIのみ)
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsUtiTorikeshiSingleCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        With Me._Frm

            '【関連チェック】
            Dim custCd As String = String.Empty
            Dim custList As New ArrayList
            Dim destNm1 As String = String.Empty
            Dim destNm2 As String = String.Empty
            Dim max As Integer = arr.Count - 1
            Dim custDetailsDr As DataRow() = Nothing
            Dim inkaStateKb As String = String.Empty

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '荷主明細チェック
            For i As Integer = 0 To max
                custCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.CUST_CD_L.ColNo))
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, _
                                                                                                                "' AND CUST_CD = '", custCd, _
                                                                                                                "' AND SUB_KB = '50'"))

                '20151020 tsunehira add
                MyBase.ShowMessage("E618", New String() {custCd})
                'MyBase.ShowMessage("E336", New String() {String.Concat(custCd, "は対象荷主"), "処理"})


                custList.Add(custCd)

                'しんちょくくぶん
                inkaStateKb = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.INKA_STATE_KB.ColNo))

                If LMB010C.INKASTATEKB_90.Equals(inkaStateKb) = False Then

                    '20151020 tsunehira add
                    MyBase.ShowMessage("E620")
                    'MyBase.ShowMessage("E530", New String() {"報告済", "報告済取消"})
                    Return False
                End If

            Next

        End With

        Return True

    End Function
    '入荷報告取消(東レUTI専用)対応 s.kobayashi 

    'WIT対応 入荷データ一括取込対応 kasama Start
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 入荷データ一括取込時
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInkaDataIkkatuTorikomiSingleCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        With Me._Frm

            '【関連チェック】
            Dim custCd As String = String.Empty
            Dim custList As New ArrayList
            Dim destNm1 As String = String.Empty
            Dim destNm2 As String = String.Empty
            Dim max As Integer = arr.Count - 1
            Dim custDetailsDr As DataRow() = Nothing
            Dim inkaStateKb As String = String.Empty

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '荷主明細チェック
            For i As Integer = 0 To max
                custCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.CUST_CD_L.ColNo))
                custDetailsDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", Me._Frm.cmbEigyo.SelectedValue, _
                                                                                                                "' AND CUST_CD = '", custCd, _
                                                                                                                "' AND SUB_KB = '64'"))

                If custDetailsDr.Length = 0 Then
                    '20151020 tsunehira add
                    MyBase.ShowMessage("E618", New String() {custCd})
                    'MyBase.ShowMessage("E336", New String() {String.Concat(custCd, "は対象荷主"), "処理"})
                    Return False
                End If
                custList.Add(custCd)

                '進捗区分チェック
                inkaStateKb = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.INKA_STATE_KB.ColNo))

                If LMB010C.INKASTATEKB_50.Equals(inkaStateKb) OrElse LMB010C.INKASTATEKB_90.Equals(inkaStateKb) Then

                    Dim stateNm As String = Me._Vcon.GetCellValue(.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.STATUS_NM.ColNo))
                    '20151029 tsunehira add Start
                    '英語化対応
                    MyBase.ShowMessage("E786")
                    '2015.10.29 tusnehira add End
                    Return False
                End If

            Next

        End With

        Return True

    End Function
    'WIT対応 入荷データ一括取込対応 kasama End

    'CALT対応 入荷データ一括取込対応 ri --ST--
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 入荷予定データ作成時
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsInkaDataYoteiSingleCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

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

            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

            '【関連チェック】
            Dim brCd As String = String.Empty
            Dim whCd As String = String.Empty
            Dim max As Integer = arr.Count - 1
            Dim kbnDr As DataRow() = Nothing
            Dim inkaStateKb As String = String.Empty

            '明細
            For i As Integer = 0 To max

                '倉庫チェック
                brCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.NRS_BR_CD.ColNo))
                whCd = Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.WH_CD.ColNo))

                kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'S102'", "AND KBN_NM1 = '", brCd, "' AND KBN_NM2 = '", whCd, "'"))

                '20151020 tsunehira add
                If kbnDr.Length = 0 Then
                    MyBase.ShowMessage("E617")
                    'MyBase.ShowMessage("E336", New String() {"倉庫コードが区分マスタに登録", "入荷予定データ作成"})
                End If

            Next

        End With

        Return True

    End Function
    'CALT対応 入荷データ一括取込対応 ri --ED--

    '顧客WEB入荷登録バッチ対応 t.kido Start
    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 顧客WEB入荷登録
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsWebInkaSingleCheck() As Boolean

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
    '顧客WEB入荷登録バッチ対応 t.kido End

    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） 作業一括削除・作成時
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsJikkoSingleCheck(ByVal arr As ArrayList) As Boolean

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

        End With

        Return True

    End Function
    'END YANAI 要望番号1286 入荷・出荷画面の機能追加（出荷データ作成）

    'START YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする
    ''' <summary>
    ''' 印刷時入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPrintSingleCheck(ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            '【単項目チェック】

            '印刷種別
            '2017/09/25 修正 李↓
            .cmbJikkou.ItemName() = lgm.Selector({"印刷種別", "Printing type", "인쇄종별", "中国語"})
            '2017/09/25 修正 李↑

            .cmbPrint.IsHissuCheck() = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If


            '選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

        End With

        Return True

    End Function
    'END YANAI 要望番号840 入荷検索画面、入荷報告書を印刷可能とする

    ''' <summary>
    ''' 実行時入力チェック（単項目チェック） RFIDラベルデータ作成時
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsMakeRfidLavelDataSingleCheck(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            ' 選択チェック
            If Me.IsSelectDataChk(arr) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' ラベル種類入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsPrintKanrenCheck(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '【関連項目チェック】
            'ADD 2017/08/04 GHSラベル時ラベル種類未設定はエラー
            If .cmbPrint.SelectedValue.Equals("06") = True Then
                'GHSラベル未選択時はエラー
                If String.IsNullOrEmpty(.cmbLabelTYpe.TextValue) = True Then
                    MyBase.ShowMessage("E199", New String() {"ラベル種類"})
                    .cmbLabelTYpe.BackColorDef() = LMGUIUtility.GetAttentionBackColor()
                    .cmbLabelTYpe.Focus()

                    Return False
                End If
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
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMB010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMB010C.EventShubetsu.SINKI           '追加
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

            Case LMB010C.EventShubetsu.KANRYO          '完了
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

            Case LMB010C.EventShubetsu.KENSAKU        '検索
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

            Case LMB010C.EventShubetsu.MASTER         'マスタ参照
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

            Case LMB010C.EventShubetsu.DEF_CUST       '初期荷主
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

            Case LMB010C.EventShubetsu.CLOSE           '閉じる
                'すべての権限許可
                kengenFlg = True

            Case LMB010C.EventShubetsu.DOUBLE_CLICK    'ダブルクリック
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

                'START YANAI メモ②No.28
            Case LMB010C.EventShubetsu.JIKKOU           '実行
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
                'START END メモ②No.28

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
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm">フォーカス位置コントロール名</param>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMB010C.EventShubetsu) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'フォーカス位置がない場合、スルー
        If String.IsNullOrEmpty(objNm) = True Then
            'ポップ対象外の場合
            'マスタ参照の場合、エラーメッセージ設定
            If actionType.Equals(LMB010C.EventShubetsu.MASTER) = True Then
                Call Me.SetFocusErrMessage(False)
            End If
            Return False
        End If

        '判定するコントロール設定先変数
        Dim ctl As Win.InputMan.LMImTextBox() = Nothing
        Dim msg As String() = Nothing
        Dim rtnFlg As Boolean = False

        With Me._Frm

            Select Case objNm

                Case .txtCustCD.Name

                    ctl = New Win.InputMan.LMImTextBox() {.txtCustCD}

                    '2017/09/25 修正 李↓
                    msg = New String() {lgm.Selector({"荷主コード", "Custmer Code", "하주 코드", "中国語"})}
                    '2017/09/25 修正 李↑

                    '2015.11.02 tsunehira add End
                    'msg = New String() {"荷主コード"}

                    rtnFlg = Me.IsFocusChk(actionType.ToString(), ctl, msg)
                    If rtnFlg = False Then
                        .lblCustNM.TextValue = String.Empty
                    End If

                Case Else
                    'ポップ対象外の場合
                    'マスタ参照の場合、エラーメッセージ設定
                    If actionType.Equals(LMB010C.EventShubetsu.MASTER) = True Then
                        Call Me.SetFocusErrMessage(False)
                    End If

                    rtnFlg = False

            End Select

            Return rtnFlg

        End With

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="actionType">アクションタイプ</param>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="msg">置換文字配列</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFocusChk(ByVal actionType As String _
                                   , ByVal ctl As Win.InputMan.LMImTextBox() _
                                   , ByVal msg As String() _
                                   ) As Boolean

        'メインのコントロールでの判定のみ
        'フォーカス位置が参照可能チェック
        Dim max As Integer = -1

        '何も無い場合、エラー
        If ctl Is Nothing = False Then
            max = ctl.Length - 1
        End If

        Dim rtnResult As Boolean = Me.IsFoucsReadOnlyChk(ctl, max)

        Select Case actionType

            Case "MASTER"

                rtnResult = Me.SetFocusErrMessage(rtnResult)

            Case "ENTER"

                rtnResult = rtnResult AndAlso Me.IsFoucsValueChk(ctl, max)

        End Select

        '禁止文字チェック
        rtnResult = rtnResult AndAlso Me.IsFoucsForbiddenWordsChk(ctl, msg, max)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 値が入っているかを判定
    ''' </summary>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsValueChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal max As Integer) As Boolean

        '全てに値がない場合、スルー
        For i As Integer = 0 To max
            If Me.IsFoucsValueChk(ctl(i)) = True Then
                Return True
            End If
        Next

        Return False

    End Function

    ''' <summary>
    ''' 値が入っているかを判定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsValueChk(ByVal ctl As Win.InputMan.LMImTextBox) As Boolean

        If ctl Is Nothing = True Then
            Return True
        End If

        If String.IsNullOrEmpty(ctl.TextValue) = True Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置エラーのメッセージ設定
    ''' </summary>
    ''' <param name="chk">判定フラグ</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetFocusErrMessage(ByVal chk As Boolean) As Boolean

        If chk = False Then
            Return Me._Vcon.SetFocusErrMessage()
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置が参照可能判定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks>メインのコントロールでの判定</remarks>
    Private Function IsFoucsReadOnlyChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal max As Integer) As Boolean

        '何も無い場合、エラー
        If ctl Is Nothing = True Then
            Return False
        End If

        For i As Integer = 0 To max

            'ロックされている場合、エラー
            If ctl(i).ReadOnly = True Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置の禁止文字チェック
    ''' </summary>
    ''' <param name="ctl">コントロール配列</param>
    ''' <param name="msg">置換文字配列</param>
    ''' <param name="max">ループ変数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsForbiddenWordsChk(ByVal ctl As Win.InputMan.LMImTextBox(), ByVal msg As String(), ByVal max As Integer) As Boolean

        '全てのコントロールの値に対して禁止文字チェック
        For i As Integer = 0 To max

            If Me.IsFoucsForbiddenWordsChk(ctl(i), msg(i)) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置の禁止文字チェック
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFoucsForbiddenWordsChk(ByVal ctl As Win.InputMan.LMImTextBox, ByVal msg As String) As Boolean

        'コントロールが無い場合、スルー
        If ctl Is Nothing = True Then
            Return True
        End If

        '禁止文字チェック
        ctl.ItemName = msg
        ctl.IsForbiddenWordsCheck = True
        Return MyBase.IsValidateCheck(ctl)

    End Function

    'START YANAI メモ②No.28
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
    'EMD YANAI メモ②No.28

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'スプレッドのスペース除去
        Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprDetail, 0)

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCD.TextValue = Me._Frm.txtCustCD.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' 入荷(小)存在チェック
    ''' </summary>
    ''' <param name="arr">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaSCheck(ByVal arr As ArrayList, ByVal g As LMB010G) As Boolean

        With Me._Frm

            Dim max As Integer = arr.Count - 1
            Dim spr As Win.Spread.LMSpread = .sprDetail

            '荷主明細チェック
            For i As Integer = 0 To max
                If Me._Vcon.GetCellValue(Me._Frm.sprDetail.ActiveSheet.Cells(Convert.ToInt32(arr(i)), g.sprDetailDef.RECCNTS.ColNo)).Equals("0") Then
                    Me._Vcon.SetErrMessage("E787")
                    'Me._Vcon.SetErrMessage("E525", New String() {"選択されたデータのうち、いずれかの入荷(小)"})
                    Return False
                End If
            Next

        End With

        Return True

    End Function

#End Region 'Method

End Class
