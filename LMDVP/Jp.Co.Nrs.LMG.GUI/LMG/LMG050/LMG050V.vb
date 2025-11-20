' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG     : 請求サブシステム
'  プログラムID     :  LMG050V : 請求処理 請求書作成
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMG050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
Public Class LMG050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMG050F

    ''' <summary>
    ''' 入力チェックで使用するValidateクラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _ControlV As LMGControlV

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMG050F, ByVal v As LMGControlV)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        MyBase.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._ControlV = v

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <param name="eventShubetsu">権限チェックを行うイベント</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMG050C.EventShubetsu) As Boolean

        Dim kengen As String = LMUserInfoManager.GetAuthoLv

        Select Case eventShubetsu
            Case LMG050C.EventShubetsu.EDIT _
                , LMG050C.EventShubetsu.DELETE _
                , LMG050C.EventShubetsu.KAKUTEI _
                , LMG050C.EventShubetsu.IMPORT _
                , LMG050C.EventShubetsu.INITIALIZE _
                , LMG050C.EventShubetsu.KEIRITAISHOGAI _
                , LMG050C.EventShubetsu.KEIRIMODOSHI _
                , LMG050C.EventShubetsu.MSTSANSHO _
                , LMG050C.EventShubetsu.SAVE _
                , LMG050C.EventShubetsu.PRINT                           '編集、削除、確定、取込、初期化、経理対象外、経理戻し、マスタ参照、保存、SAP出力、SAP取消、印刷

                If kengen.Equals(LMConst.AuthoKBN.VIEW) = True _
                OrElse kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then  '10:閲覧者、50:外部の場合エラー
                    '2011/08/04 菱刈 検証結果一覧 No3 スタート
                    'OrElse kengen.Equals(LMConst.AuthoKBN.EDIT) = True　　'20:入力者(一般)はエラーにしません。
                    '2011/08/04 菱刈 検証結果一覧 No3 スタート
                    MyBase.ShowMessage("E016")
                    Return False
                End If

            Case LMG050C.EventShubetsu.ENTER  'エンター押下

                If kengen.Equals(LMConst.AuthoKBN.VIEW) = True _
                OrElse kengen.Equals(LMConst.AuthoKBN.AGENT) = True Then  '10:閲覧者、50:外部の場合ポップアップ起動無し
                    '2011/08/04 菱刈 検証結果一覧 No3 スタート
                    'OrElse kengen.Equals(LMConst.AuthoKBN.EDIT) = True _  20:入力者(一般)はPOP起動あり
                    '2011/08/04 菱刈 検証結果一覧 No3 エンド

                    Return False
                End If

            Case LMG050C.EventShubetsu.SAPOUT   'SAP出力

                Dim userDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(String.Concat("USER_CD = '", LMUserInfoManager.GetUserID, "'"))
                If userDr.Length = 0 Then
                    'ユーザ未登録はエラー（ログインしている限り有り得ないはず）
                    MyBase.ShowMessage("E016")
                    Return False
                Else
                    'SAP連携実行権限がなければエラー
                    If Not "01".Equals(userDr(0).Item("SAP_LINK_AUTHO").ToString) Then
                        MyBase.ShowMessage("E016")
                        Return False
                    End If
                End If

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 編集/削除/ステージアップ時共通チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsCommonChk(ByVal msg As String) As Boolean

        msg = String.Concat(msg)

        '管轄営業所チェック
        If Me.BrCdChk(msg) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' フォーカス位置チェック
    ''' </summary>
    ''' <param name="objNm"></param>
    ''' <param name="eventShubetsu"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsFocusChk(ByVal objNm As String, ByVal eventShubetsu As LMG050C.EventShubetsu) As Boolean

        'フォーカス位置がない場合、スルー
        If objNm Is Nothing = True Then
            Return False
        End If

        With Me._Frm

            Select Case objNm
                Case .txtSeikyuCd.Name

                    'ロック判定
                    If .txtSeikyuCd.ReadOnly = True Then

                        'ロックされている場合処理終了
                        Return False

                    End If

                    '禁止文字チェックを行う
                    If String.IsNullOrEmpty(.txtSeikyuCd.TextValue) = False Then
                        .txtSeikyuCd.ItemName = .lblTitleSeikyuCd.Text
                        .txtSeikyuCd.IsForbiddenWordsCheck = True
                        If MyBase.IsValidateCheck(.txtSeikyuCd) = False Then
                            Return False
                        End If
                    End If

                Case .sprSeikyuM.Name

                    Return True

                Case Else

                    Select Case eventShubetsu
                        Case LMG050C.EventShubetsu.MSTSANSHO
                            MyBase.ShowMessage("G005")
                    End Select
                    Return False

            End Select

            Return True

        End With

    End Function

    ''' <summary>
    ''' 保存押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsSaveChk() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsSaveSingleChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsSaveRelationChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    '''取込押下時入力チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsImportChk() As Boolean

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsImportSingleChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsImportRelationChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行追加時チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし False：入力エラーあり
    ''' </returns>
    ''' <remarks></remarks>
    Friend Function IsAddRowChk() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************

            '請求先コード
            'セグメントの初期値取得のため入力が必要
            .txtSeikyuCd.ItemName = .lblTitleSeikyuCd.Text
            .txtSeikyuCd.IsHissuCheck = True
            .txtSeikyuCd.IsForbiddenWordsCheck = True
            .txtSeikyuCd.IsByteCheck = 7
            .txtSeikyuCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtSeikyuCd) = False Then
                Return False
            End If

        End With

        '上限チェック
        If Me.IsMaxEdabanChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 行削除時チェック
    ''' </summary>
    ''' <param name="list">選択行格納配列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDelRowChk(ByVal list As ArrayList) As Boolean

        '必須選択チェック
        If List.Count = 0 Then
            MyBase.ShowMessage("E009")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' SAP出力押下時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSapOutChk() As Boolean

        ' 単項目チェック
        If Me.IsSapOutSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' SAP取消押下時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsSapCancelChk() As Boolean

        ' 単項目チェック
        If Me.IsSapCancelSingleChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 印刷押下時チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsPrintChk() As Boolean

        '単項目チェック
        If Me.IsPrintSingleChk() = False Then
            Return False
        End If

        '関連チェック
        If Me.IsPrintRelationChk() = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' オーバーフローチェック
    ''' </summary>
    ''' <param name="calc">計算値</param>
    ''' <param name="errorNm">エラー項目名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsOverFlowChk(ByVal calc As String _
                                  , ByVal errorNm As String) As Boolean

        '★ ADD START 2011/09/06 SUGA
        Dim calcLng As Long = CLng(calc.Replace("-", ""))
        '★ ADD E N D 2011/09/06 SUGA

        '★ UPD START 2011/09/06 SUGA
        'Dim caliInt As Integer = calc.Replace("-", "").Length()
        Dim caliInt As Integer = calcLng.ToString().Length()
        '★ UPD E N D 2011/09/06 SUGA

        If 10 < caliInt Then
            MyBase.ShowMessage("E117", New String() {errorNm, "9,999,999,999"})

            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' オーバーフローチェック(9桁)
    ''' </summary>
    ''' <param name="calc">計算値</param>
    ''' <param name="errorNm">エラー項目名称</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsOverFlowChk9(ByVal calc As String _
                                  , ByVal errorNm As String) As Boolean

        '★ ADD START 2011/09/06 SUGA
        Dim calcLng As Long = CLng(calc.Replace("-", ""))
        '★ ADD E N D 2011/09/06 SUGA

        '★ UPD START 2011/09/06 SUGA
        'Dim caliInt As Integer = calc.Replace("-", "").Length()
        Dim caliInt As Integer = calcLng.ToString().Length()
        '★ UPD E N D 2011/09/06 SUGA

        If 9 < caliInt Then
            MyBase.ShowMessage("E117", New String() {errorNm, "999,999,999"})

            Return False
        End If

        Return True

    End Function

#Region "内部メソッド"

    ''' <summary>
    ''' 管轄営業所チェック
    ''' </summary>
    ''' <param name="msg">置換文字列</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function BrCdChk(ByVal msg As String) As Boolean

        '削除 2015.05.20 営業所またぎ処理のため営業所コードチェック削除
        'Dim userBr As String = LMUserInfoManager.GetNrsBrCd
        'If Me._Frm.cmbBr.SelectedValue.Equals(userBr) = False Then
        '    MyBase.ShowMessage("E178", New String() {msg})
        '    Return False
        'End If

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleChk() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************

            '請求先コード
            .txtSeikyuCd.ItemName = .lblTitleSeikyuCd.Text
            .txtSeikyuCd.IsHissuCheck = True
            .txtSeikyuCd.IsForbiddenWordsCheck = True
            '2011/08/04 菱刈 検証結果一覧 No6 スタート
            .txtSeikyuCd.IsByteCheck = 7
            '2011/08/04 菱刈 検証結果一覧 No6 エンド
            .txtSeikyuCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtSeikyuCd) = False Then
                Return False
            End If

            '担当者名
            If String.IsNullOrEmpty(.txtSeikyuTantoNm.TextValue) = False Then
                .txtSeikyuTantoNm.ItemName = .lblTitleSeikyuTantoNm.Text
                .txtSeikyuTantoNm.IsForbiddenWordsCheck = True
                .txtSeikyuTantoNm.IsByteCheck = 20
                If MyBase.IsValidateCheck(.txtSeikyuTantoNm) = False Then
                    Return False
                End If
            End If

            '請求日
            .imdInvDate.ItemName = .lblTitleSeikyuDate.Text
            .imdInvDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdInvDate) = False Then
                Return False
            End If
            If .imdInvDate.IsDateFullByteCheck = False Then
                MyBase.ShowMessage("E038", New String() {.lblTitleSeikyuDate.Text, "8"})
                Return False
            End If

            '請求通貨コード
            .cmbSeiqCurrCd.ItemName() = "請求通貨"
            .cmbSeiqCurrCd.IsHissuCheck() = True
            If String.IsNullOrEmpty(Convert.ToString(.cmbSeiqCurrCd.SelectedValue)) = True Then
                MyBase.ShowMessage("E001", New String() {"請求通貨"})
                Me._ControlV.SetErrorControl(.cmbSeiqCurrCd)
                Return False
            End If

            '変換元通貨コード
            .cmbCurrencyConversion1.ItemName() = "変換元通貨"
            .cmbCurrencyConversion1.IsHissuCheck() = True
            If String.IsNullOrEmpty(Convert.ToString(.cmbCurrencyConversion1.SelectedValue)) = True Then
                MyBase.ShowMessage("E001", New String() {"変換元通貨"})
                Me._ControlV.SetErrorControl(.cmbCurrencyConversion1)
                Return False
            End If

            '変換先通貨コード
            .cmbCurrencyConversion2.ItemName() = "変換先通貨"
            .cmbCurrencyConversion2.IsHissuCheck() = True
            If String.IsNullOrEmpty(Convert.ToString(.cmbCurrencyConversion2.SelectedValue)) = True Then
                MyBase.ShowMessage("E001", New String() {"変換先通貨"})
                Me._ControlV.SetErrorControl(.cmbCurrencyConversion2)
                Return False
            End If

            '備考
            If String.IsNullOrEmpty(.txtRemark.TextValue) = False Then
                .txtRemark.ItemName = .lblTitleBiko.Text
                .txtRemark.IsForbiddenWordsCheck = True
                .txtRemark.IsByteCheck = 100
                If MyBase.IsValidateCheck(.txtRemark) = False Then
                    Return False
                End If
            End If

            Dim CallallK As Decimal = Convert.ToDecimal(.numCalAllK.Value)

            '上限チェック
            '税額

            Dim ZeigakuK As Decimal = Convert.ToDecimal(.numZeigakuK.Value)

            If Me.IsOverFlowChk9(ZeigakuK.ToString(), "税額") = False Then

                Me._ControlV.SetErrorControl(.numZeigakuK)
                Return False
            End If

            Dim ZeigakuU As Decimal = Convert.ToDecimal(.numZeigakuU.Value)

            If Me.IsOverFlowChk9(ZeigakuU.ToString(), "税額") = False Then

                Me._ControlV.SetErrorControl(.numZeigakuU)
                Return False
            End If

            'EX_RATE
            If Convert.ToDecimal(.numExRate.Value) <= 0 Then
                MyBase.ShowMessage("E014", New String() {"EX_RATE", "0", "99,999.999999"})
                Me._ControlV.SetErrorControl(.numExRate)
                Return False
            End If

            '2011/08/04 菱刈 入力チェック追加  全体値引額がマイナス値だったらエラー スタート
            If Convert.ToDecimal(.numNebikiGakuK.Value) < 0 Then
                MyBase.ShowMessage("E014", New String() {"全体値引額", "0", "999,999,999"})
                Me._ControlV.SetErrorControl(.numNebikiGakuK)
                Return False
            End If

            If Convert.ToDecimal(.numNebikiGakuM.Value) < 0 Then
                MyBase.ShowMessage("E014", New String() {"全体値引額", "0", "999,999,999"})
                Me._ControlV.SetErrorControl(.numNebikiGakuM)
                Return False
            End If
            '2011/08/04 菱刈 入力チェック追加  全体値引額がマイナス値だったらエラー エンド

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprSeikyuM)
            Dim max As Integer = .sprSeikyuM.ActiveSheet.Rows.Count - 1
            '2011/08/04 菱刈 明細行が0の場合エラー スタート
            If max = -1 Then
                MyBase.ShowMessage("E028", New String() {"明細件数が0件", "保存"})
                Return False
            End If
            '2011/08/04 菱刈 明細行が0の場合エラー エンド

            '2011/08/05 菱刈 明細行が100件以上の場合はエラー スタート
            If max > 98 Then
                MyBase.ShowMessage("E028", New String() {"明細件数が100件以上", "保存"})
                Return False
            End If
            '2011/08/05 菱刈 明細行が100件以上の場合はエラー エンド
            Dim rowNo As Integer = 0
            Dim countK As Integer = 0
            Dim countM As Integer = 0
            Dim nebikiRtGk As String = String.Empty
            '★ ADD START 2011/09/06 SUGA
            Dim keisanGk As String = String.Empty
            Dim rateNebiki As String = String.Empty
            Dim koteiNebiki As String = String.Empty
            Dim total As Decimal = 0
            '★ ADD E N D 2011/09/06 SUGA

            '外部倉庫用ABP対策
            Dim drABP As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'G203' AND KBN_NM1 = '", LM.Base.LMUserInfoManager.GetNrsBrCd, "'"))

            For i As Integer = 0 To max
                If drABP.Length = 0 Then
                    '真荷主
                    vCell.SetValidateCell(i, LMG050G.sprSeikyuMDef.TCUST_BPCD.ColNo)
                    vCell.ItemName = LMG050G.sprSeikyuMDef.TCUST_BPCD.ColName
                    vCell.IsHissuCheck = True
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '製品セグメント
                    vCell.SetValidateCell(i, LMG050G.sprSeikyuMDef.PRODUCT_SEG_CD.ColNo)
                    vCell.ItemName = LMG050G.sprSeikyuMDef.PRODUCT_SEG_CD.ColName
                    vCell.IsHissuCheck = True
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '地域セグメント(発地)
                    vCell.SetValidateCell(i, LMG050G.sprSeikyuMDef.ORIG_SEG_CD.ColNo)
                    vCell.ItemName = LMG050G.sprSeikyuMDef.ORIG_SEG_CD.ColName
                    vCell.IsHissuCheck = True
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If

                    '地域セグメント(着地)
                    vCell.SetValidateCell(i, LMG050G.sprSeikyuMDef.DEST_SEG_CD.ColNo)
                    vCell.ItemName = LMG050G.sprSeikyuMDef.DEST_SEG_CD.ColName
                    vCell.IsHissuCheck = True
                    If MyBase.IsValidateCheck(vCell) = False Then
                        Return False
                    End If
                End If

                '摘要
                vCell.SetValidateCell(i, LMG050G.sprSeikyuMDef.TEKIYOU.ColNo)
                vCell.ItemName = LMG050G.sprSeikyuMDef.TEKIYOU.ColName
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 60
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                rowNo = i

                '2011/08/04 菱刈 スプレッドの固定値引額がマイナスの場合エラー スタート
                If Convert.ToDecimal(Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))) < 0 Then
                    MyBase.ShowMessage("E014", New String() {"固定値引額", "0", "999,999,999"})
                    Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo)
                    Return False
                End If
                '2011/08/04 菱刈 スプレッドの固定値引額がマイナスの場合エラー エンド

                'オーバーフローチェックを行う
                '★ UPD START 2011/09/06 SUGA
                '2011/08/12 菱刈 オーバーフローチェックコメント化 スタート
                nebikiRtGk = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.RATENEBIKI_GAKU.ColNo))
                If Me.IsOverFlowChk(nebikiRtGk.ToString(), "率値引額") = False Then
                    Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo)
                    Return False
                End If
                '2011/08/12 菱刈 オーバーフローチェックコメント化 エンド
                '★ UPD E N D 2011/09/06 SUGA
                '★ ADD START 2011/09/06 SUGA
                keisanGk = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo))
                rateNebiki = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.RATENEBIKI_GAKU.ColNo))
                koteiNebiki = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))

                total = Convert.ToDecimal(keisanGk) - (Convert.ToDecimal(rateNebiki) + Convert.ToDecimal(koteiNebiki))

                If Me.IsOverFlowChk(total.ToString(), "請求額") = False Then
                    total = 0
                    Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo)
                    Return False
                End If
                '★ ADD START 2011/09/06 SUGA

                '2011/08/05 菱刈 課税分、免税分がスプレッドになく全体値引額が入力されている場合エラー スタート
                '課税区分
                If Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo)) = "01" Then
                    countK = +1
                End If
                ' 免税区分以外の時はエラー
                If Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.KAZEI_KBN.ColNo)) = "02" Then
                    countM = +1
                End If

            Next

            '課税区分、免税区分のカウントが0件の場合で全体値引額が入力されていたらエラー
            '課税区分
            If countK = 0 AndAlso _
                 Convert.ToDecimal(.numNebikiGakuK.Value) <> 0 Then
                MyBase.ShowMessage("E386", New String() {"課税"})
                Me._ControlV.SetErrorControl(.numNebikiGakuK)
                Return False

            End If

            '免税区分
            If countM = 0 AndAlso _
             Convert.ToDecimal(.numNebikiGakuM.Value) <> 0 Then
                MyBase.ShowMessage("E386", New String() {"免税"})
                Me._ControlV.SetErrorControl(.numNebikiGakuM)
                Return False
            End If
            '2011/08/05 菱刈 課税分、免税分がスプレッドになく全体値引額が入力されている場合エラー エンド

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveRelationChk() As Boolean

        '入力不可チェック
        With Me._Frm
            '課税
            If Convert.ToDecimal(.numCalAllK.Value) < 0 Then
                If Convert.ToDecimal(.numNebikiRateK.Value) <> 0 _
                OrElse Convert.ToDecimal(.numNebikiGakuK.Value) <> 0 Then
                    MyBase.ShowMessage("E123", New String() {"計算総額がマイナス", "全体値引率、全体値引額"})
                    If Convert.ToDecimal(.numNebikiRateK.Value) <> 0 Then
                        Me._ControlV.SetErrorControl(.numNebikiRateK)
                        Me._ControlV.SetErrorControl(.numCalAllK)
                    Else
                        Me._ControlV.SetErrorControl(.numNebikiGakuK)
                        Me._ControlV.SetErrorControl(.numCalAllK)
                    End If
                    Return False
                End If
            End If
            '免税
            If Convert.ToDecimal(.numCalAllM.Value) < 0 Then
                If Convert.ToDecimal(.numNebikiRateM.Value) <> 0 _
                OrElse Convert.ToDecimal(.numNebikiGakuM.Value) <> 0 Then
                    MyBase.ShowMessage("E123", New String() {"計算総額がマイナス", "全体値引率、全体値引額"})
                    If Convert.ToDecimal(.numNebikiRateM.Value) <> 0 Then
                        Me._ControlV.SetErrorControl(.numNebikiRateM)
                        Me._ControlV.SetErrorControl(.numCalAllK)
                    Else
                        Me._ControlV.SetErrorControl(.numNebikiGakuM)
                        Me._ControlV.SetErrorControl(.numCalAllK)
                    End If
                    Return False
                End If
            End If

        End With

        With Me._Frm.sprSeikyuM.ActiveSheet

            '******************** Spread項目の入力チェック ********************
            Dim max As Integer = .Rows.Count - 1
            Dim groupKb As String = String.Empty
            Dim sqKmkCd As String = String.Empty
            Dim busyoCd As String = String.Empty
            Dim rtnFlg As Boolean = False
            Dim rowNo As Integer = 0
            For i As Integer = 0 To max

                rowNo = i

                '部署チェックを行う
                groupKb = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.GROUP_CD_KBN.ColNo))
                sqKmkCd = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.SHUBETU_KBN.ColNo))
                busyoCd = Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.BUSYO.ColNo))
                If Me.ChkBusyo(groupKb, sqKmkCd, busyoCd) = False Then
                    MyBase.ShowMessage("E267")
                    Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.BUSYO.ColNo)
                    Return False
                End If

                '入力不可チェック
                If Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo))) < 0 Then
                    If Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo))) <> 0 _
                    OrElse Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo))) <> 0 Then
                        MyBase.ShowMessage("E123", New String() {"計算額がマイナス", "値引率、固定値引額"})
                        If Convert.ToDecimal(Me._ControlV.GetCellValue(.Cells(i, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo))) <> 0 Then
                            Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.NEBIKI_RATE.ColNo)
                            Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo)
                        Else
                            Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KOTEINEBIKI_GAKU.ColNo)
                            Me._ControlV.SetErrorControl(Me._Frm.sprSeikyuM, rowNo, LMG050G.sprSeikyuMDef.KEISAN_GAKU.ColNo)
                        End If
                        Return False
                    End If
                End If
               
            Next

        End With

        '組み合わせ妥当性チェック
        Dim dr As DataRow = Nothing
        Dim ItemCurrSpr As String = String.Empty
        Dim SeiqCurrHed As String = String.Empty
        Dim ItemCurr As String = Me._Frm.cmbCurrencyConversion1.SelectedValue.ToString()
        Dim InvCurr As String = Me._Frm.cmbCurrencyConversion2.SelectedValue.ToString()

        '請求建値を取得
        SeiqCurrHed = Me._Frm.cmbSeiqCurrCd.SelectedValue.ToString()

        For i As Integer = 0 To Me._Frm.sprSeikyuM.ActiveSheet.Rows.Count - 1

            '契約建値を取得
            ItemCurrSpr = Me._ControlV.GetCellValue(Me._Frm.sprSeikyuM.ActiveSheet.Cells(i, LMG050G.sprSeikyuMDef.ITEM_CURR_CD.ColNo))

            If (InvCurr.Equals(SeiqCurrHed) = True And ItemCurr.Equals(ItemCurrSpr) = True) = False _
             And (ItemCurr.Equals(SeiqCurrHed) = True And InvCurr.Equals(ItemCurrSpr) = True) = False _
             And (SeiqCurrHed.Equals(ItemCurrSpr) = True) = False Then

                MyBase.ShowMessage("E725")
                Return False

            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 部署チェックを行う
    ''' </summary>
    ''' <param name="groupKb">グループ区分</param>
    ''' <param name="sqKmkCd">請求項目コード</param>
    ''' <param name="busyoCd">部署コード</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ChkBusyo(ByVal groupKb As String, ByVal sqKmkCd As String, ByVal busyoCd As String) As Boolean

        Dim sqKmkDr As DataRow() = Nothing
        Dim kbnDr As DataRow() = Nothing
        Dim filter As String = String.Empty

        '請求項目マスタを検索し、経理科目コード区分を取得
        filter = String.Empty
        filter = String.Concat(filter, "GROUP_KB = '", groupKb, "'")
        filter = String.Concat(filter, " AND SEIQKMK_CD = '", sqKmkCd, "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")

        sqKmkDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQKMK).Select(filter)
        If sqKmkDr.Length = 0 Then
            Return False
        End If

        '区分マスタを検索し、取得結果が0件の場合、エラー
        filter = String.Empty
        filter = String.Concat(filter, "KBN_GROUP_CD = '", LMKbnConst.KBN_B006, "'")
        filter = String.Concat(filter, " AND KBN_NM4 = '", busyoCd, "'")
        filter = String.Concat(filter, " AND KBN_NM1 = '", sqKmkDr(0).Item("KEIRI_KB"), "'")
        filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")

        kbnDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)

        If kbnDr.Length = 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 取込押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsImportSingleChk() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************

            '請求先コード
            .txtSeikyuCd.ItemName = .lblTitleSeikyuCd.Text
            .txtSeikyuCd.IsHissuCheck = True
            .txtSeikyuCd.IsForbiddenWordsCheck = True
            '2011/08/04 菱刈 検証結果一覧 No6 スタート
            .txtSeikyuCd.IsByteCheck = 7
            '2011/08/04 菱刈 検証結果一覧 No6 エンド
            .txtSeikyuCd.IsMiddleSpace = True
            If MyBase.IsValidateCheck(.txtSeikyuCd) = False Then
                Return False
            End If

            '請求日
            .imdInvDate.ItemName = .lblTitleSeikyuDate.Text
            .imdInvDate.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdInvDate) = False Then
                Return False
            End If
            If .imdInvDate.IsDateFullByteCheck = False Then
                MyBase.ShowMessage("E038", New String() {.lblTitleSeikyuDate.Text, "8"})
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 取込押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsImportRelationChk() As Boolean

        With Me._Frm

            If .chkHokan.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkNiyaku.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkUnchin.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkSagyou.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkYokomochi.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkTemplate.GetBinaryValue().Equals(LMConst.FLG.OFF) Then

                MyBase.ShowMessage("E199", New String() {"取込項目"})
                Me._ControlV.SetErrorControl(.chkHokan)
                Me._ControlV.SetErrorControl(.chkNiyaku)
                Me._ControlV.SetErrorControl(.chkUnchin)
                Me._ControlV.SetErrorControl(.chkSagyou)
                Me._ControlV.SetErrorControl(.chkYokomochi)
                Me._ControlV.SetErrorControl(.chkTemplate)
                Return False

            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のTrim
        Call Me.TrimSpaceHeaderTextValue()

        'スプレッドのTrim
        Call Me.TrimSpaceSprTextValue()

    End Sub

    ''' <summary>
    ''' ヘッダ項目のTrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceHeaderTextValue()

        With Me._Frm

            .txtSeikyuCd.TextValue = .txtSeikyuCd.TextValue.Trim()
            .txtSeikyuTantoNm.TextValue = .txtSeikyuTantoNm.TextValue.Trim()
            .txtRemark.TextValue = .txtRemark.TextValue.Trim()

        End With

    End Sub

    ''' <summary>
    ''' スプレッド部のTrim
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceSprTextValue()

        Dim aCell As Cell = Nothing

        With Me._Frm.sprSeikyuM

            Dim maxCol As Integer = .ActiveSheet.Columns.Count - 1
            Dim maxRow As Integer = .ActiveSheet.Rows.Count - 1
            For i As Integer = 0 To maxCol

                For r As Integer = 0 To maxRow

                    aCell = .ActiveSheet.Cells(r, i)
                    If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True Then

                    Else

                        .SetCellValue(r, i, Me._ControlV.GetCellValue(aCell))
                    End If

                Next

            Next

        End With

    End Sub

    ''' <summary>
    ''' SEQ上限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMaxEdabanChk() As Boolean

        Dim seq As Integer = Convert.ToInt32(Me._Frm.lblMaxEdaban.TextValue)

        If 99 <= seq Then
            MyBase.ShowMessage("E062", New String() {"枝番"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' SAP出力押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSapOutSingleChk() As Boolean

        With Me._Frm

            ' 進捗区分チェック
            If Not .cmbStateKbn.SelectedValue.ToString().Equals(LMG050C.STATE_INSATU_ZUMI) Then
                ' 「印刷済」以外はエラー

                ' エラーメッセージの動的文字列編集
                Dim stateName As String = String.Empty
                For i As Integer = 0 To .cmbStateKbn.Items().Count - 1
                    If .cmbStateKbn.Items(i).SubItems(1).Value.Equals(LMG050C.STATE_INSATU_ZUMI) Then
                        stateName = .cmbStateKbn.Items(i).Text
                        Exit For
                    End If
                Next
                MyBase.ShowMessage("E320",
                    New String() {String.Concat(.lblTitleSincyoku.Text, "が", stateName, "以外"), .btnSapOut.Text})
                Return False
            End If

            ' 請求日チェック
            ' 区分マスタを検索し、SAP出力連携可能日付を取得する。
            Dim filter As String = String.Empty
            filter = String.Empty
            filter = String.Concat(Filter, "KBN_GROUP_CD = '", LMG040C.KBN_SAP_OUT_START_DATE, "'")
            Filter = String.Concat(Filter, " AND KBN_CD = '", "01", "'")
            Filter = String.Concat(Filter, " AND SYS_DEL_FLG = '0'")
            Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            If kbnDr.Length = 0 OrElse
                kbnDr(0).Item("KBN_NM1").ToString().Trim().Length <> 8 Then
                MyBase.ShowMessage("E320",
                    New String() {
                        String.Concat("SAP出力連携可能日付が区分マスタ（区分グループコード='",
                                      LMG040C.KBN_SAP_OUT_START_DATE, "'）に未登録、または不適切な設定値"),
                        String.Concat(.lblTitleSeikyuDate.Text, "の妥当性をチェック")})
                Return False
            End If
            If .imdInvDate.TextValue < kbnDr(0).Item("KBN_NM1").ToString() Then
                Dim sapOutStartDate As String = ""
                ' 請求日 < SAP出力連携可能日付はエラー
                MyBase.ShowMessage(
                    "E320",
                    New String() {
                        String.Concat(.lblTitleSeikyuDate.Text, "が",
                        DateFormatUtility.EditSlash(kbnDr(0).Item("KBN_NM1").ToString()),
                        "より前"),
                        .btnSapOut.Text})
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' SAP取消押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSapCancelSingleChk() As Boolean

        With Me._Frm

            ' 進捗区分チェック
            If Not .cmbStateKbn.SelectedValue.ToString().Equals(LMG050C.STATE_KEIRI_TORIKOMI_ZUMI) Then
                ' 「経理取込済」以外はエラー

                ' エラーメッセージの動的文字列編集
                Dim stateName As String = String.Empty
                For i As Integer = 0 To .cmbStateKbn.Items().Count - 1
                    If .cmbStateKbn.Items(i).SubItems(1).Value.Equals(LMG050C.STATE_KEIRI_TORIKOMI_ZUMI) Then
                        stateName = .cmbStateKbn.Items(i).Text
                        Exit For
                    End If
                Next
                MyBase.ShowMessage("E320",
                    New String() {String.Concat(.lblTitleSincyoku.Text, "が", stateName, "以外"), .btnSapCancel.Text})
                Return False
            End If

            ' 請求日チェック
            ' 区分マスタを検索し、SAP出力連携可能日付を取得する。
            Dim filter As String = String.Empty
            filter = String.Empty
            filter = String.Concat(filter, "KBN_GROUP_CD = '", LMG040C.KBN_SAP_OUT_START_DATE, "'")
            filter = String.Concat(filter, " AND KBN_CD = '", "01", "'")
            filter = String.Concat(filter, " AND SYS_DEL_FLG = '0'")
            Dim kbnDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(filter)
            If kbnDr.Length = 0 OrElse
                kbnDr(0).Item("KBN_NM1").ToString().Trim().Length <> 8 Then
                MyBase.ShowMessage("E320",
                    New String() {
                        String.Concat("SAP出力連携可能日付が区分マスタ（区分グループコード='",
                                      LMG040C.KBN_SAP_OUT_START_DATE, "'）に未登録、または不適切な設定値"),
                        String.Concat(.lblTitleSeikyuDate.Text, "の妥当性をチェック")})
                Return False
            End If
            If .imdInvDate.TextValue < kbnDr(0).Item("KBN_NM1").ToString() Then
                Dim sapOutStartDate As String = ""
                ' 請求日 < SAP出力連携可能日付はエラー
                MyBase.ShowMessage(
                    "E320",
                    New String() {
                        String.Concat(.lblTitleSeikyuDate.Text, "が",
                        DateFormatUtility.EditSlash(kbnDr(0).Item("KBN_NM1").ToString()),
                        "より前"),
                        .btnSapCancel.Text})
                Return False
            End If
        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷押下時単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintSingleChk() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************

            '印刷種別
            .cmbPrint.ItemName = "印刷種別"
            .cmbPrint.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbPrint) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 印刷押下時関連チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsPrintRelationChk() As Boolean

        With Me._Frm

            '******************** ヘッダ項目の入力チェック ********************

            '印刷種別
            If .chkMainAri.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkSubAri.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkKeiHikaeAri.GetBinaryValue().Equals(LMConst.FLG.OFF) _
            AndAlso .chkHikaeAri.GetBinaryValue().Equals(LMConst.FLG.OFF) Then

                MyBase.ShowMessage("E199", New String() {"請求書種別"})
                Me._ControlV.SetErrorControl(.chkMainAri)
                Me._ControlV.SetErrorControl(.chkSubAri)
                Me._ControlV.SetErrorControl(.chkKeiHikaeAri)
                Me._ControlV.SetErrorControl(.chkHikaeAri)
                Return False
            End If

        End With

        Return True

    End Function
    '★ ADD START 2011/09/06 SUGA

    ''' <summary>
    ''' マイナス値チェック
    ''' </summary>
    ''' <param name="strTarget">チェック対象値</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function ChkInputMinus(ByVal strTarget As String, ByVal errorNm As String, ByVal max As String) As Boolean

        Dim decTarget As Decimal = Convert.ToDecimal(strTarget)
        If decTarget < 0 Then
            'メッセージ設定
            MyBase.ShowMessage("E014", New String() {errorNm, "0", max})
            Return False
        End If

        Return True

    End Function
    '★ ADD E N D 2011/09/06 SUGA

#End Region

#End Region

End Class
