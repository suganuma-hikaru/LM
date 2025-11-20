' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LM     : 在庫サブシステム
'  プログラムID     :  LMD020V : 
'  作  成  者       :  
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Base
Imports Jp.Co.Nrs.LM.Utility '2017/09/25 追加 李

''' <summary>
''' LMD020Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMD020V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMD020F

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMDControlV

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMDControlG

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMD020F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMDControlV(handlerClass, DirectCast(frm, LMFormSxga))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 保存時の項目チェック、関連チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChk(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList) As Boolean

        With Me._Frm

            '検索項目のTrim
            Call Me.TrimSpaceTextValue()

            '保存時の単項目チェック
            If Me.IsSaveSingleCheck(arrSaki) = False Then
                Return False
            End If

            '未選択チェック
            If Me._Vcon.IsSelectChk(arrSaki.Count()) = False Then
                Return False
            End If

            '移動日チェック
            If Me.idoDateChk(frm, arrMoto) = False Then
                Return False
            End If

            '棟室マスタ存在チェック
            If Me.IsExistTouSituChk(arrSaki) = False Then
                Return False
            End If

            '届先マスタ存在チェック
            If Me.IsExistDestChk(arrSaki) = False Then
                Return False
            End If

            '置き場チェック
            If Me.custOkibaChk(frm, arrSaki, arrMoto) = False Then
                Return False
            End If

            '大小チェック
            If Me.IsIdoDaishoCheck(frm, arrSaki, arrMoto) = False Then
                Return False
            End If

            '小分け完了チェック
            If Me.IsCompletedKowake(frm, arrMoto) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存時の項目チェック、関連チェック（強制出庫用）
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSaveInputChkKyoseiShuko(ByVal frm As LMD020F, ByVal arrMoto As ArrayList) As Boolean

        With Me._Frm

            '検索項目のTrim
            Call Me.TrimSpaceTextValue()

            '移動日
            .imdIdoubi.ItemName = "移動日"
            .imdIdoubi.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdIdoubi) = False Then
                Return False
            End If
            If .imdIdoubi.IsDateFullByteCheck = False Then
                Me.SetErrorControl(.imdIdoubi)
                Me._Vcon.SetErrMessage("E038", New String() { .imdIdoubi.ItemName, "8"})
                Return False
            End If

            '移動日チェック
            If Me.idoDateChk(frm, arrMoto) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 検索時の項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsKensakuInputChk() As Boolean

        '検索項目のTrim
        Call Me.TrimSpaceTextValue()

        '単項目チェック
        If Me.IsKensakuSingleCheck() = False Then
            Return False
        End If

        With Me._Frm

            '入荷FromToチェック
            If Me._Vcon.IsFromToChk(.imdNyukaFrom.TextValue, .imdNyukaTo.TextValue) = False Then
                SetErrorControl(.imdNyukaFrom)
                SetErrorControl(.imdNyukaTo)
                '20151029 tsunehira add Start
                '英語化対応
                Return Me._Vcon.SetErrMessage("E615")
                '20151029 tsunehira add End
                'Return Me._Vcon.SetErrMessage("E039", New String() {"入荷日To", "入荷日From"})
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 一括変更時の項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAllChangeInputChk() As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        '関連チェック
        With _Frm

            Dim chkFlg As Boolean = True
            Dim brcd As String = String.Empty
            Dim whcd As String = String.Empty
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim loca As String = String.Empty
            Dim goodsCondKb1 As String = String.Empty
            Dim goodsCondKb2 As String = String.Empty
            Dim goodsCondKb3 As String = String.Empty
            Dim spdKb As String = String.Empty
            Dim ofbKb As String = String.Empty
            Dim ltDate As String = String.Empty
            Dim goodsCrtDate As String = String.Empty
            Dim allocPriority As String = String.Empty
            Dim destCd As String = String.Empty
            Dim rsvNo As String = String.Empty
            Dim remarkOut As String = String.Empty
            Dim remark As String = String.Empty

            brcd = .cmbNrsBrCd.SelectedValue.ToString()
            whcd = .cmbSoko.SelectedValue.ToString()
            touNo = .txtTouNo.TextValue
            situNo = .txtSituNo.TextValue
            zoneCd = .txtZoneCd.TextValue
            loca = .txtLocation.TextValue
            goodsCondKb1 = .cmbGoodsCondKb1.SelectedValue.ToString
            goodsCondKb2 = .cmbGoodsCondKb2.SelectedValue.ToString
            goodsCondKb3 = .cmbGoodsCondKb3.SelectedValue.ToString
            spdKb = .cmbSpdKb.SelectedValue.ToString
            ofbKb = .cmbOfbKb.SelectedValue.ToString
            ltDate = .imdLtDate.TextValue
            goodsCrtDate = .imdGoodsCrtDate.TextValue
            allocPriority = .cmdAllocPriority.SelectedValue.ToString
            destCd = .txtDestCd.TextValue
            rsvNo = .txtRsvNo.TextValue
            remarkOut = .txtRemarkOut.TextValue
            remark = .txtRemark.TextValue

            '単項目チェック
            If Me.IsAllChangeCheck() = False Then
                Return False
            End If

            '全項目未入力の場合はエラー
            If String.IsNullOrEmpty(touNo) = True AndAlso
               String.IsNullOrEmpty(situNo) = True AndAlso _
               String.IsNullOrEmpty(zoneCd) = True AndAlso _
               String.IsNullOrEmpty(loca) = True AndAlso _
               String.IsNullOrEmpty(goodsCondKb1) = True AndAlso _
               String.IsNullOrEmpty(goodsCondKb2) = True AndAlso _
               String.IsNullOrEmpty(goodsCondKb3) = True AndAlso _
               String.IsNullOrEmpty(spdKb) = True AndAlso _
               String.IsNullOrEmpty(ofbKb) = True AndAlso _
               String.IsNullOrEmpty(ltDate) = True AndAlso _
               String.IsNullOrEmpty(goodsCrtDate) = True AndAlso _
               String.IsNullOrEmpty(allocPriority) = True AndAlso _
               String.IsNullOrEmpty(destCd) = True AndAlso _
               String.IsNullOrEmpty(rsvNo) = True AndAlso _
               String.IsNullOrEmpty(remarkOut) = True AndAlso _
               String.IsNullOrEmpty(remark) = True Then

                Me.SetErrorControl(.txtSituNo)
                Me.SetErrorControl(.txtZoneCd)
                Me.SetErrorControl(.txtLocation)
                Me.SetErrorControl(.cmbGoodsCondKb1)
                Me.SetErrorControl(.cmbGoodsCondKb2)
                If .cmbGoodsCondKb3.Items.Count > 0 Then
                    Me.SetErrorControl(.cmbGoodsCondKb3)
                End If
                Me.SetErrorControl(.cmbSpdKb)
                Me.SetErrorControl(.cmbOfbKb)
                Me.SetErrorControl(.imdLtDate)
                Me.SetErrorControl(.imdGoodsCrtDate)
                Me.SetErrorControl(.cmdAllocPriority)
                Me.SetErrorControl(.txtDestCd)
                Me.SetErrorControl(.txtRsvNo)
                Me.SetErrorControl(.txtRemarkOut)
                Me.SetErrorControl(.txtRemark)
                Me.SetErrorControl(.txtTouNo)

                Return Me._Vcon.SetErrMessage("E981")
            End If

            '棟・室・ZONEマスタ存在チェック
            If String.IsNullOrEmpty(touNo) = False AndAlso String.IsNullOrEmpty(situNo) = False AndAlso _
               String.IsNullOrEmpty(zoneCd) = False Then
                If Me._Vcon.SelectToShitsuZoneListDataRow(brcd, whcd, touNo, situNo, zoneCd).Length < 1 Then
                    Me.SetErrorControl(.txtSituNo)
                    Me.SetErrorControl(.txtZoneCd)
                    Me.SetErrorControl(.txtTouNo)

                    '2017/09/25 修正 李↓
                    Dim msg As String = String.Empty
                    msg = lgm.Selector({"棟室ZONEマスタ", "Building room ZONE master", "동(棟)실(室)존(Zone)마스터", "中国語"})
                    '2017/09/25 修正 李↑

                    Return Me._Vcon.SetMstErrMessage(msg, String.Concat(touNo, " - ", situNo, " - ", zoneCd))
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 行削除時の項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsRowDelChk(ByVal arr As ArrayList) As Boolean

        '未選択チェック
        If Me._Vcon.IsSelectChk(arr.Count) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 未選択チェック（行追加時）
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSelectInsChk(ByVal chkCnt As Integer) As Boolean

        'チェック件数が0件
        If chkCnt = 0 Then

            Return Me._Vcon.SetErrMessage("E368")

        End If

        Return True

    End Function

    ''' <summary>
    ''' 在庫履歴時の項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsZaikoDataChk() As Boolean

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMD020C.SprColumnIndexMoveBefor.DEF

        '選択された行の行番号を取得
        Dim ArrChkList As ArrayList = Me._Vcon.SprSelectList(defNo, Me._Frm.sprMoveBefor)

        '未選択チェック
        If Me._Vcon.IsSelectChk(ArrChkList.Count()) = False Then
            Return False
        End If

        '単一選択チェック
        If Me._Vcon.IsSelectOneChk(ArrChkList.Count()) = False Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal actionType As LMD020C.ActionType) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case actionType
            '在庫履歴、検索、マスタ参照、保存、行削除、行追加、一括変更
            Case LMD020C.ActionType.ZAIKORIREKI, _
                        LMD020C.ActionType.KENSAKU, _
                        LMD020C.ActionType.MASTER, _
                        LMD020C.ActionType.HOZON, _
                        LMD020C.ActionType.COLADD, _
                        LMD020C.ActionType.COLDEL, _
                        LMD020C.ActionType.ALLCHANGE
                '10:閲覧者、50:外部の場合エラー
                Select Case kengen
                    Case LMConst.AuthoKBN.VIEW        '10:閲覧者
                        kengenFlg = False
                    Case LMConst.AuthoKBN.EDIT         '20:入力者（一般）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.EDIT_UP    '25:入力者（上級）
                        kengenFlg = True
                    Case LMConst.AuthoKBN.LEADER    '30:管理職
                        kengenFlg = True
                    Case LMConst.AuthoKBN.MANAGER '40:システム管理者
                        kengenFlg = True
                    Case LMConst.AuthoKBN.AGENT      '50:外部
                        kengenFlg = False
                End Select

                '閉じる、左スクロール、右スクロール
            Case LMD020C.ActionType.TOJIRU, _
                        LMD020C.ActionType.LEFTSCRL, _
                        LMD020C.ActionType.RIGHTSCRL
                'すべての権限許可
                kengenFlg = True

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
    Friend Function IsFocusChk(ByVal objNm As String, ByVal actionType As LMD020C.ActionType) As Boolean

        ''フォーカス位置がない場合、スルー
        'If String.IsNullOrEmpty(objNm) = True Then
        '    Return False
        'End If

        Dim ctl1 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl2 As Win.InputMan.LMImTextBox = Nothing
        Dim ctl3 As Win.InputMan.LMImTextBox = Nothing
        Dim msg1 As String = String.Empty
        Dim msg2 As String = String.Empty
        Dim msg3 As String = String.Empty

        With Me._Frm

            Select Case objNm

                Case .txtCustCdL.Name, .txtCustCdM.Name

                    ctl1 = .txtCustCdL
                    msg1 = "荷主(大)コード"
                    ctl2 = .txtCustCdM
                    msg2 = "荷主(中)コード"

                    'コードが空なら名称を消す
                    If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
                    And String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                        .lblCustNM.TextValue = String.Empty
                    End If

                Case .txtSituNo.Name, .txtTouNo.Name, .txtZoneCd.Name
                    ctl1 = .txtTouNo
                    msg1 = "棟番号"
                    ctl2 = .txtSituNo
                    msg2 = "室番号"
                    ctl3 = .txtZoneCd
                    msg3 = "ZONEコード"

                Case .txtDestCd.Name
                    ctl1 = .txtDestCd
                    msg1 = "届先コード"

                Case .sprMoveAfter.Name, .sprMoveBefor.Name

                    Return True

            End Select

            'Nothing判定用
            Dim ctlChk As Boolean = ctl2 Is Nothing

            Dim ctlChk2 As Boolean = ctl3 Is Nothing

            'フォーカス位置が参照可能でない場合、エラー
            If (ctl1 Is Nothing = True OrElse ctl1.ReadOnly = True) _
                OrElse (ctlChk = False AndAlso ctl2.ReadOnly = True) Then

                Select Case actionType

                    Case LMD020C.ActionType.MASTER

                        Return _Vcon.SetFocusErrMessage()

                    Case LMD020C.ActionType.ENTER

                        'Enterの場合はメッセージは設定しない
                        Return False

                End Select


            End If

            '禁止文字チェック(1つ目のコントロール)
            ctl1.ItemName = msg1
            ctl1.IsForbiddenWordsCheck = True
            If MyBase.IsValidateCheck(ctl1) = False Then
                Return False
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk = False Then
                ctl2.ItemName = msg2
                ctl2.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl2) = False Then
                    Return False
                End If
            End If

            '禁止文字チェック(2つ目のコントロール)
            If ctlChk2 = False Then
                ctl3.ItemName = msg3
                ctl3.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(ctl3) = False Then
                    Return False
                End If
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' スプレッド禁止文字チェック
    ''' </summary>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <param name="msg">メッセージ</param>
    ''' <param name="maxByte">最大文字数</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsSprForbiddenWordsChk(ByVal rowNo As Integer, ByVal colNo As Integer, ByVal msg As String, ByVal maxByte As Integer) As Boolean

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprMoveAfter)

        '届先コード
        vCell.SetValidateCell(rowNo, colNo)
        vCell.ItemName = msg
        vCell.IsForbiddenWordsCheck = True
        vCell.IsByteCheck = maxByte
        If MyBase.IsValidateCheck(vCell) = False Then
            Return False
        End If

        Return True

    End Function
    '要望番号:1350 terakawa 2012.08.29 Start
    ''' <summary>
    ''' 同一置場での同一商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsGoodsLotChk(ByVal ds As DataSet, ByVal serverChkFlg As Boolean) As Boolean
        '同一置き場に同一商品・ロットがある場合ワーニング
        Dim drs As DataTable = ds.Tables(LMD020C.TABLE_NM_ZAI_NEW)
        Dim max As Integer = drs.Rows.Count - 1
        Dim worningDt As DataTable = ds.Tables(LMD020C.TABLE_NM_WORNING)
        Dim worningDr As DataRow = Nothing
        Dim worningFlg As Boolean = Nothing

        For i As Integer = 0 To max
            Dim nrsBrCd As String = drs(i)("NRS_BR_CD").ToString()
            Dim custGoodsCd As String = drs(i)("GOODS_CD_CUST").ToString()
            Dim custCdL As String = drs(i)("CUST_CD_L").ToString()
            Dim lotNo As String = drs(i)("LOT_NO").ToString()
            Dim touNo As String = drs(i)("TOU_NO").ToString()
            Dim situNo As String = drs(i)("SITU_NO").ToString()
            Dim zoneCd As String = drs(i)("ZONE_CD").ToString()
            Dim loca As String = drs(i)("LOCA").ToString()

            'ワーニングフラグをリセット
            worningFlg = False

            '要望番号:1511 KIM 2012/10/12 START
            '商品コード違いの重複チェック
            'Dim drGoods As DataRow() = ds.Tables(LMD020C.TABLE_NM_ZAI_NEW).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND CUST_CD_L = '", custCdL, _
            '                                                       "' AND LOT_NO = '", lotNo, "' AND TOU_NO = '", touNo, _
            '                                                        "' AND SITU_NO = '", situNo, "' AND ZONE_CD = '", zoneCd, _
            '                                                        "' AND LOCA = '", loca, "' AND GOODS_CD_CUST <> '", custGoodsCd, "'"))
            'If drGoods.Count >= 1 Then
            '    worningFlg = True
            'End If
            '要望番号:1511 KIM 2012/10/12 END

            'ロット番号違いの重複チェック
            Dim drLot As DataRow() = ds.Tables(LMD020C.TABLE_NM_ZAI_NEW).Select(String.Concat(" NRS_BR_CD = '", nrsBrCd, "' AND CUST_CD_L = '", custCdL, _
                                                                   "' AND LOT_NO <> '", lotNo, "' AND TOU_NO = '", touNo, _
                                                                    "' AND SITU_NO = '", situNo, "' AND ZONE_CD = '", zoneCd, _
                                                                    "' AND LOCA = '", loca, "' AND GOODS_CD_CUST = '", custGoodsCd, "'"))
            If drLot.Count >= 1 Then
                worningFlg = True
            End If


            'ワーニングフラグがTrueの場合、ワーニングテーブルに情報をセット
            If worningFlg = True Then
                worningDr = worningDt.NewRow()
                With worningDr
                    .Item("GOODS_CD_CUST") = custGoodsCd
                    .Item("TOU_NO") = touNo
                    .Item("SITU_NO") = situNo
                    .Item("ZONE_CD") = zoneCd
                    .Item("LOCA") = loca
                    .Item("LOT_NO") = lotNo
                End With
                ds.Tables(LMD020C.TABLE_NM_WORNING).Rows.Add(worningDr)
            End If
        Next

        If ds.Tables(LMD020C.TABLE_NM_WORNING).Rows.Count > 0 Then
            Return IsWorningChk(ds, serverChkFlg)
        End If

        Return True
    End Function
    ''' <summary>
    ''' 同一置場での同一商品・ロット重複チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:OK False:キャンセル</returns>
    ''' <remarks></remarks>
    Friend Function IsWorningChk(ByVal ds As DataSet, ByVal serverChkFlg As Boolean) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        'ワーニングテーブルに情報があった場合、ワーニング出力する
        Dim worningCount As Integer = ds.Tables(LMD020C.TABLE_NM_WORNING).Rows.Count
        If worningCount > 0 Then
            Dim strWorning As String = String.Empty
            Dim wGoodsCdCust As String = String.Empty
            Dim wOkiba As String = String.Empty
            Dim wLotNo As String = String.Empty
            For k As Integer = 0 To worningCount - 1
                strWorning = String.Concat(strWorning, vbCrLf)
                wGoodsCdCust = ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("GOODS_CD_CUST").ToString()
                wOkiba = String.Concat(ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("TOU_NO").ToString(), "-", _
                                       ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("SITU_NO").ToString(), "-", _
                                       ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("ZONE_CD").ToString())
                If String.IsNullOrEmpty(ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("LOCA").ToString()) = False Then
                    wOkiba = String.Concat(wOkiba, "-", ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("LOCA").ToString())
                End If
                wLotNo = ds.Tables(LMD020C.TABLE_NM_WORNING).Rows(k).Item("LOT_NO").ToString()
            
                '2017/09/25 修正 李↓
                strWorning = String.Concat(strWorning, lgm.Selector({"商品=", "Goods=", "상품=", "中国語"}), wGoodsCdCust, lgm.Selector({"、置場=", "、Place=", ", 하치장=", "中国語"}), wOkiba, "、LOT=", wLotNo)
                '2017/09/25 修正 李↑

            Next

            'ワーニングテーブルの中身を削除
            ds.Tables(LMD020C.TABLE_NM_WORNING).Clear()

            If serverChkFlg = True Then
                '2015.10.22 tusnehira add
                '英語化対応
                If MyBase.ShowMessage("W242", New String() {vbCrLf, String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then

                    'If MyBase.ShowMessage("W215", New String() {String.Concat(vbCrLf, "同じ場所に保管されています。") _
                    '                                            , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                    Return False
                End If
            Else
                '2015.10.22 tusnehira add
                '英語化対応
                If MyBase.ShowMessage("W243", New String() {vbCrLf, String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then

                    'If MyBase.ShowMessage("W215", New String() {String.Concat(vbCrLf, "同じ場所で画面上に存在します。") _
                    '                        , String.Concat(vbCrLf, vbCrLf, strWorning)}) <> MsgBoxResult.Ok Then
                    Return False
                End If
            End If
        End If

        Return True
    End Function
    '要望番号:1350 terakawa 2012.08.29 End

#Region "単項目チェック"

    ''' <summary>
    ''' 検索時の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsKensakuSingleCheck() As Boolean

        With Me._Frm

            '営業所
            .cmbNrsBrCd.ItemName = "営業所"
            .cmbNrsBrCd.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbNrsBrCd) = False Then
                Return False
            End If

            '倉庫
            .cmbSoko.ItemName = "倉庫"
            .cmbSoko.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbSoko) = False Then
                Return False
            End If

            '荷主コード(大)
            .txtCustCdL.ItemName = "荷主コード(大)"
            .txtCustCdL.IsForbiddenWordsCheck = True
            .txtCustCdL.IsByteCheck = 5
            If MyBase.IsValidateCheck(.txtCustCdL) = False Then
                Return False
            End If

            '荷主コード(中)
            .txtCustCdM.ItemName = "荷主コード(中)"
            .txtCustCdM.IsForbiddenWordsCheck = True
            .txtCustCdM.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtCustCdM) = False Then
                Return False
            End If

            'コードが空なら名称を消す
            If String.IsNullOrEmpty(.txtCustCdL.TextValue) = True _
            And String.IsNullOrEmpty(.txtCustCdM.TextValue) = True Then
                .lblCustNM.TextValue = String.Empty
            End If

            '入荷日(From)
            .imdNyukaFrom.ItemName = "入荷日(From)"
            If .imdNyukaFrom.IsDateFullByteCheck = False Then
                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E613")
                '20151029 tsunehira add End
                'Me._Vcon.SetErrMessage("E038", New String() {.imdNyukaFrom.ItemName, "8"})
                Return False
            End If

            '入荷日(To)
            .imdNyukaTo.ItemName = "入荷日(To)"
            If .imdNyukaTo.IsDateFullByteCheck = False Then
                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E614")
                '20151029 tsunehira add End
                'Me._Vcon.SetErrMessage("E038", New String() {.imdNyukaTo.ItemName, "8"})
                Return False
            End If

            '移動日
            If .imdIdoubi.IsDateFullByteCheck = False Then

                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E038", New String() {.lblTitleimdIdoubi.TextValue, "8"})
                '20151029 tsunehira add End
                'Me._Vcon.SetErrMessage("E038", New String() {.imdIdoubi.ItemName, "8"})
                Return False
            End If

            '******************** Spread項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprMoveBefor)

            '商品名
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.GOODS_NM.ColNo)
            vCell.ItemName = "商品名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 60
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロット
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.LOT_NO.ColNo)
            vCell.ItemName = "ロット"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'シリアルNo
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.SERIAL_NO.ColNo)
            vCell.ItemName = "シリアル№"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 40
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '棟
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.TOU_NO.ColNo)
            vCell.ItemName = "棟"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 2
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '室
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.SITU_NO.ColNo)
            vCell.ItemName = "室"
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 1
            vCell.IsByteCheck = 2
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ZONE
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.ZONE_CD.ColNo)
            vCell.ItemName = "ZONE"
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            'vCell.IsByteCheck = 1
            vCell.IsByteCheck = 2
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'ロケーション
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.LOCA.ColNo)
            vCell.ItemName = "ロケーション"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主商品コード
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.GOODS_CD_CUST.ColNo)
            vCell.ItemName = "荷主商品コード"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 20
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '荷主カテゴリ2
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.SEARCH_KEY_2.ColNo)
            vCell.ItemName = "荷主カテゴリ2"
            vCell.IsForbiddenWordsCheck = True
            'START YANAI 要望番号1065 荷主カテゴリのバイト変更
            'vCell.IsByteCheck = 20
            vCell.IsByteCheck = 25
            'END YANAI 要望番号1065 荷主カテゴリのバイト変更
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '届先名
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.DEST_NM.ColNo)
            vCell.ItemName = "届先名"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 80
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '予約番号
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.RSV_NO.ColNo)
            vCell.ItemName = "予約番号"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '備考小(社外)
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.REMARK_OUT.ColNo)
            vCell.ItemName = "備考小(社外)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 15
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            '在庫レコードNo.
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo)
            vCell.ItemName = "在庫レコードNo."
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 10
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

            'コメント
            vCell.SetValidateCell(0, LMD020G.sprMoveBefor.REMARK.ColNo)
            vCell.ItemName = "備考小(社内)"
            vCell.IsForbiddenWordsCheck = True
            vCell.IsByteCheck = 100
            If MyBase.IsValidateCheck(vCell) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 保存時の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSaveSingleCheck(ByVal arr As ArrayList) As Boolean

        With Me._Frm

            '事由欄区分
            .cmbJiyuran.ItemName = "事由区分"
            .cmbJiyuran.IsHissuCheck = True
            If MyBase.IsValidateCheck(.cmbJiyuran) = False Then
                Return False
            End If

            '事由欄
            .txtJiyuran.ItemName = "事由欄"
            .txtJiyuran.IsForbiddenWordsCheck = True
            .txtJiyuran.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtJiyuran) = False Then
                Return False
            End If

            '移動日
            .imdIdoubi.ItemName = "移動日"
            .imdIdoubi.IsHissuCheck = True
            If MyBase.IsValidateCheck(.imdIdoubi) = False Then
                Return False
            End If
            If .imdIdoubi.IsDateFullByteCheck = False Then
                Me.SetErrorControl(.imdIdoubi)
                Me._Vcon.SetErrMessage("E038", New String() {.imdIdoubi.ItemName, "8"})
                Return False
            End If

            '******************** Spread項目の入力チェック ********************


            'スプレッドでチェックがついているレコードのみチェックを行う
            Dim max As Integer = arr.Count - 1
            'START YANAI メモ②No.15
            Dim sokoDr() As DataRow = Nothing
            Dim sokoHissu As String = String.Empty
            'END YANAI メモ②No.15
            For i As Integer = 0 To max

                Dim vCell As LMValidatableCells = New LMValidatableCells(.sprMoveAfter)

                'START YANAI メモ②No.15
                sokoDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SOKO).Select(String.Concat("WH_CD = '", Me._Vcon.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.WH_CD_R.ColNo)).ToString(), "'"))
                sokoHissu = String.Empty
                If 0 < sokoDr.Length Then
                    sokoHissu = sokoDr(0).Item("LOC_MANAGER_YN").ToString()
                End If
                'END YANAI メモ②No.15

                '棟
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo)
                vCell.ItemName = "棟番号"
                'START YANAI メモ②No.15
                'START YANAI 要望番号433
                'If ("01").Equals(sokoHissu) = True Then
                If ("01").Equals(sokoHissu) = True OrElse _
                    ("02").Equals(sokoHissu) = True Then
                    'END YANAI 要望番号433
                    vCell.IsHissuCheck = True
                End If
                'END YANAI メモ②No.15
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 2
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '室
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo)
                vCell.ItemName = "室番号"
                'START YANAI メモ②No.15
                'START YANAI 要望番号433
                'If ("01").Equals(sokoHissu) = True Then
                If ("01").Equals(sokoHissu) = True OrElse _
                    ("02").Equals(sokoHissu) = True Then
                    'END YANAI 要望番号433
                    'END YANAI メモ②No.15
                    vCell.IsHissuCheck = True
                End If
                'END YANAI メモ②No.15
                vCell.IsForbiddenWordsCheck = True
                'START YANAI 要望番号705
                'vCell.IsByteCheck = 1
                'START S_KOBA 要望番号705
                'vCell.IsFullByteCheck = 2
                vCell.IsByteCheck = 2
                'END S_KOBA 要望番号705
                'END YANAI 要望番号705
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                'ZONE
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)
                vCell.ItemName = "ZONE"
                'START YANAI メモ②No.15
                'START YANAI 要望番号433
                'If ("01").Equals(sokoHissu) = True Then
                If ("01").Equals(sokoHissu) = True Then
                    'END YANAI 要望番号433
                    'END YANAI メモ②No.15
                    vCell.IsHissuCheck = True
                End If
                'END YANAI メモ②No.15
                vCell.IsForbiddenWordsCheck = True
                'START YANAI 要望番号705
                'vCell.IsByteCheck = 1
                'START S_KOBA 要望番号705
                'vCell.IsFullByteCheck = 2
                vCell.IsByteCheck = 2
                'END S_KOBA 要望番号705
                'END YANAI 要望番号705
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                'ロケーション
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.LOCA_R.ColNo)
                vCell.ItemName = "ロケーション"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '移動個数
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)
                vCell.ItemName = "移動個数"
                vCell.IsForbiddenWordsCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '移動個数の0チェック
                If Me.IsIdoKosuCheck(Convert.ToInt32(arr(i))) = False Then
                    Me._Vcon.SetErrMessage("E304")
                    Return False
                End If

                '保留品
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.SPD_KB_R.ColNo)
                vCell.ItemName = "保留品"
                vCell.IsHissuCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '簿外品
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.OFB_KB_R.ColNo)
                vCell.ItemName = "簿外品"
                vCell.IsHissuCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '賞味期限
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.LT_DATE_R.ColNo)
                vCell.ItemName = "賞味期限"
                vCell.IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '製造日
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.GOODS_CRT_DATE_R.ColNo)
                vCell.ItemName = "製造日"
                vCell.IsFullByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '割当優先
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.ALLOC_PRIORITY_R.ColNo)
                vCell.ItemName = "割当優先"
                vCell.IsHissuCheck = True
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '届先コード
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.DEST_CD_R.ColNo)
                vCell.ItemName = "届先コード"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 15
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '予約番号
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.RSV_NO_R.ColNo)
                vCell.ItemName = "予約番号"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 10
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                '備考小(社外)
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo)
                vCell.ItemName = "備考小(社外)"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 15
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

                'コメント
                vCell.SetValidateCell(Convert.ToInt32(arr(i)), LMD020G.sprMoveAfter.REMARK_R.ColNo)
                vCell.ItemName = "備考小（社内）"
                vCell.IsForbiddenWordsCheck = True
                vCell.IsByteCheck = 100
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 一括変更時の単項目チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsAllChangeCheck() As Boolean

        With Me._Frm

            '置場(棟)
            .txtTouNo.ItemName = "棟番号"
            .txtTouNo.IsForbiddenWordsCheck = True
            .txtTouNo.IsByteCheck = 2
            If MyBase.IsValidateCheck(.txtTouNo) = False Then
                Return False
            End If

            '置場(室)
            .txtSituNo.ItemName = "室番号"
            .txtSituNo.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            '.txtSituNo.IsByteCheck = 1
            '.txtSituNo.IsFullByteCheck = 2
            'START S_KOBA 要望番号705
            .txtSituNo.IsByteCheck = 2
            'END S_KOBA 要望番号705
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(.txtSituNo) = False Then
                Return False
            End If

            '置場(ZONE)
            .txtZoneCd.ItemName = "ZONEコード"
            .txtZoneCd.IsForbiddenWordsCheck = True
            'START YANAI 要望番号705
            '.txtZoneCd.IsByteCheck = 1
            'START S_KOBA 要望番号705
            '.txtZoneCd.IsFullByteCheck = 2
            .txtZoneCd.IsByteCheck = 2
            'END S_KOBA 要望番号705
            'END YANAI 要望番号705
            If MyBase.IsValidateCheck(.txtZoneCd) = False Then
                Return False
            End If

            '置場(ロケーション)
            .txtLocation.ItemName = "ロケーション"
            .txtLocation.IsForbiddenWordsCheck = True
            .txtLocation.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtLocation) = False Then
                Return False
            End If

            '届先コード
            .txtDestCd.ItemName = "届先コード"
            .txtDestCd.IsForbiddenWordsCheck = True
            .txtDestCd.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtDestCd) = False Then
                Return False
            End If

            '予約番号
            .txtRsvNo.ItemName = "予約番号"
            .txtRsvNo.IsForbiddenWordsCheck = True
            .txtRsvNo.IsByteCheck = 10
            If MyBase.IsValidateCheck(.txtRsvNo) = False Then
                Return False
            End If

            '備考小(社外)
            .txtRemarkOut.ItemName = "備考小(社外)"
            .txtRemarkOut.IsForbiddenWordsCheck = True
            .txtRemarkOut.IsByteCheck = 15
            If MyBase.IsValidateCheck(.txtRemarkOut) = False Then
                Return False
            End If

            '備考小(社内)
            .txtRemark.ItemName = "備考小(社内)"
            .txtRemark.IsForbiddenWordsCheck = True
            .txtRemark.IsByteCheck = 100
            If MyBase.IsValidateCheck(.txtRemark) = False Then
                Return False
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 移動個数0チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIdoKosuCheck(ByVal i As Integer) As Boolean

        Dim idoKosu As Integer = 0

        '移動個数の取得
        idoKosu = Convert.ToInt32(Me._Vcon.GetCellValue(Me._Frm.sprMoveAfter.ActiveSheet.Cells(i, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)).ToString())

        '移動個数が0の場合はFalseをReturnする
        If idoKosu = 0 Then
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 置場チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function custOkibaChk(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList) As Boolean

        '基盤となる荷主情報
        Dim goodsCustNrs As String = String.Empty
        Dim custCdL As String = String.Empty
        Dim custCdM As String = String.Empty
        Dim custCdS As String = String.Empty
        Dim custCdSS As String = String.Empty
        Dim drGoods As DataRow() = Nothing
        Dim drCust As DataRow() = Nothing
        Dim hokanSeiqCd As String = String.Empty

        '移動元の荷主情報
        Dim motoTou As String = String.Empty
        Dim motoSitu As String = String.Empty
        Dim motoSeiqCd As String = String.Empty
        Dim motoZone As String = String.Empty
        Dim drMotoTouSitu As DataRow() = Nothing

        '移動先の荷主情報
        Dim sakiTou As String = String.Empty
        Dim sakiSitu As String = String.Empty
        Dim sakiZone As String = String.Empty
        Dim sakiSeiqCd As String = String.Empty
        Dim drSakiTouSitu As DataRow() = Nothing

        '判定結果
        Dim chkMotoString As String = String.Empty
        Dim chkSakiString As String = String.Empty

        Dim sakiMax As Integer = arrSaki.Count - 1
        Dim motoMax As Integer = arrMoto.Count - 1
        For i As Integer = 0 To motoMax

            '商品KEYの取得
            goodsCustNrs = Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString()

            '商品マスタより、荷主コードを取得
            '---↓
            'drGoods = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(String.Concat("GOODS_CD_NRS = ", " '", goodsCustNrs, "' "))

            Dim goodsDs As MGoodsDS = New MGoodsDS
            Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
            goodsDr.Item("GOODS_CD_NRS") = goodsCustNrs
            goodsDr.Item("SYS_DEL_FLG") = "0"    '要望番号1604 2012/11/16 本明追加
#If True Then   'ADD 2023/01/06 035090   【LMS】住友ファーマ　在庫移動ができない
            goodsDr.Item("NRS_BR_CD") = Me._Frm.cmbNrsBrCd.SelectedValue.ToString

#End If
            goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
            Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
            drGoods = rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
            '---↑

            'START KIM 要望番号1517
            If drGoods.Length = 0 Then
                '20151029 tsunehira add Start
                '英語化対応
                Me._Vcon.SetErrMessage("E803")
                '20151029 tsunehira add End
                'Me._Vcon.SetErrMessage("E523", New String() {"商品"})
                Return False
            End If
            'END KIM 要望番号1517

            custCdL = drGoods(0).Item("CUST_CD_L").ToString()
            custCdM = drGoods(0).Item("CUST_CD_M").ToString()
            custCdS = drGoods(0).Item("CUST_CD_S").ToString()
            custCdSS = drGoods(0).Item("CUST_CD_SS").ToString()

            '荷主マスタより保管料請求先コードを取得
            drCust = Me._Vcon.SelectCustListDataRow(custCdL, custCdM, custCdS, custCdSS)
            hokanSeiqCd = drCust(0).Item("HOKAN_SEIQTO_CD").ToString()

            'ゾーンマスタより、移動元の請求先コードを取得
            motoTou = Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.TOU_NO.ColNo)).ToString()
            motoSitu = Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.SITU_NO.ColNo)).ToString()
            motoZone = Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZONE_CD.ColNo)).ToString()
            drMotoTouSitu = Me._Vcon.SelectToShitsuZoneListDataRow(frm.cmbNrsBrCd.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), motoTou, motoSitu, motoZone)

            '取得出来なかった場合は・・・？
            If drMotoTouSitu.Length < 1 Then
                Return True
            End If

            motoSeiqCd = drMotoTouSitu(0).Item("SEIQTO_CD").ToString()

            If hokanSeiqCd.Equals(motoSeiqCd) = True Then
                '移動前置場は荷主専用
                chkMotoString = LMConst.FLG.ON
            ElseIf String.IsNullOrEmpty(motoSeiqCd) = False Then
                '移動前置場は荷主専用でない
                chkMotoString = LMConst.FLG.OFF
            Else
                '移動前置場は他の荷主専用
                chkMotoString = String.Empty
            End If

            For j As Integer = 0 To sakiMax

                'ゾーンマスタより、移動先の請求先コードを取得
                sakiTou = Me._Vcon.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString()
                sakiSitu = Me._Vcon.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString()
                sakiZone = Me._Vcon.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)).ToString()
                drSakiTouSitu = Me._Vcon.SelectToShitsuZoneListDataRow(frm.cmbNrsBrCd.SelectedValue.ToString(), frm.cmbSoko.SelectedValue.ToString(), sakiTou, sakiSitu, sakiZone)

                '取得出来なかった場合は・・・？
                If drSakiTouSitu.Length < 1 Then
                    Return True
                End If

                sakiSeiqCd = drSakiTouSitu(0).Item("SEIQTO_CD").ToString()

                If hokanSeiqCd.Equals(sakiSeiqCd) = True Then
                    '移動先置場は荷主専用
                    chkSakiString = LMConst.FLG.ON
                ElseIf String.IsNullOrEmpty(sakiSeiqCd) = False Then
                    '移動先置場は荷主専用でない
                    chkSakiString = LMConst.FLG.OFF
                Else
                    '移動前置場は他の荷主専用
                    chkSakiString = String.Empty
                End If

                '置場判定
                Select Case chkMotoString

                    Case LMConst.FLG.ON
                        '移動元が荷主専用かつ移動先が荷主専用でないまたは他の荷主専用だった場合エラー
                        If LMConst.FLG.OFF.Equals(chkSakiString) = True OrElse _
                        String.Empty.Equals(chkSakiString) Then
                            Me._Vcon.SetErrMessage("E290")
                            Return False
                        End If

                    Case LMConst.FLG.OFF
                        '移動元が荷主専用でないかつ移動先が荷主専用置場だった場合エラー
                        If LMConst.FLG.ON.Equals(chkSakiString) = True Then
                            Me._Vcon.SetErrMessage("E289")
                            Return False
                        End If

                    Case String.Empty
                        '移動元が他の荷主かつ移動先が荷主専用置場だった場合エラー
                        If LMConst.FLG.ON.Equals(chkSakiString) = True Then
                            Me._Vcon.SetErrMessage("E289")
                            Return False
                        End If

                End Select

                If frm.optHeikouIdo.Checked = True Then
                    Exit For
                End If

            Next

        Next

        Return True

    End Function

    ''' <summary>
    ''' 引当数量、引当中チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function hikiateChk(ByVal frm As LMD020F, ByVal i As Integer, ByVal actionType As Integer) As Boolean

        Dim suryo As Decimal = 0
        Dim irime As Decimal = 0
        Dim konsu As Integer = 0

        '行追加の時はチェックを行わない
        If LMD020C.ActionType.COLADD = actionType Then
            Return True
        End If

        '引当中数量の取得
        suryo = Convert.ToDecimal(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.ALCTD_QT.ColNo)).ToString())

        '入目の取得
        irime = Convert.ToDecimal(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.IRIME.ColNo)).ToString())

        '引当中梱数の取得
        konsu = Convert.ToInt32(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.ALCTD_NB.ColNo)).ToString())

        Dim sentaku As Integer = Convert.ToInt32(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.DEF.ColNo)).ToString())

        '引当数量チェック
        If suryo <> 0 AndAlso suryo < irime AndAlso sentaku <> 0 Then
            Me._Vcon.SetErrMessage("E287")
            frm.sprMoveBefor.SetCellValue(i, LMD020G.sprMoveBefor.DEF.ColNo, "False")
            Return False
        End If

        '引当中チェック
        If konsu <> 0 AndAlso sentaku <> 0 Then
            If MyBase.ShowMessage("W153") = MsgBoxResult.Cancel Then
                frm.sprMoveBefor.SetCellValue(i, LMD020G.sprMoveBefor.DEF.ColNo, "False")
                Return False
            End If
        End If

        Return True

    End Function

    ''' <summary>
    ''' 入荷日・未来日チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function inkoDateFutureChk(ByVal frm As LMD020F, ByVal i As Integer, ByVal actionType As Integer, ByVal sysDate As String) As Boolean

        Dim inkoDate As String = String.Empty

        '行追加の時はチェックを行わない
        If LMD020C.ActionType.COLADD = actionType Then
            Return True
        End If

        '入荷日の取得
        inkoDate = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.INKO_DATE.ColNo)).ToString())

        Dim sentaku As String = Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(i, LMD020G.sprMoveBefor.DEF.ColNo)).ToString()

        '入荷日チェック
        If sentaku.Equals(LMConst.FLG.OFF) = False AndAlso _
            sysDate < inkoDate Then
            '20151029 tsunehira add Start
            '英語化対応
            Me._Vcon.SetErrMessage("E808")
            '20151029 tsunehira add End
            'Me._Vcon.SetErrMessage("E369", New String() {"入荷日"})
            frm.sprMoveBefor.SetCellValue(i, LMD020G.sprMoveBefor.DEF.ColNo, "False")
            Return False
        End If

        Return True

    End Function

#End Region

#Region "マスタ存在チェック"

    ''' <summary>
    ''' 保存時の棟室マスタ存在チェック(移動先スプレッド)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistTouSituChk(ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim spr As Win.Spread.LMSpread = Me._Frm.sprMoveAfter

        With spr.ActiveSheet

            Dim drTouSotu As DataRow() = Nothing

            Dim brcd As String = String.Empty
            Dim whcd As String = String.Empty
            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim max As Integer = arr.Count - 1
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))
                brcd = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo)).ToString()
                whcd = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.WH_CD_R.ColNo)).ToString()
                touNo = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString()
                situNo = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString()
                zoneCd = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)).ToString()

                'START YANAI メモ②No.25
                If String.IsNullOrEmpty(touNo) = True OrElse _
                    String.IsNullOrEmpty(situNo) = True OrElse _
                    String.IsNullOrEmpty(zoneCd) = True Then
                    Continue For
                End If
                'END YANAI メモ②No.25

                drTouSotu = Me._Vcon.SelectToShitsuZoneListDataRow(brcd, whcd, touNo, situNo, zoneCd)

                '棟・室・Zoneマスタ存在チェック
                If drTouSotu.Length < 1 Then
                    .Cells(rowNo, LMD020G.sprMoveAfter.SITU_NO_R.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    .Cells(rowNo, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(spr, rowNo, LMD020G.sprMoveAfter.TOU_NO_R.ColNo)
                   
                    '2017/09/25 修正 李↓
                    Dim msg As String = String.Empty
                    msg = lgm.Selector({"棟室ZONEマスタ", "Building room ZONE master", "동(棟)실(室)존(Zone)마스터", "中国語"})
                    '2017/09/25 修正 李↑

                    Me._Vcon.SetMstErrMessage(msg, String.Concat(touNo, " - ", situNo, " - ", zoneCd))
                    Return False
                End If

                'マスタの値を設定
                spr.SetCellValue(rowNo, LMD020G.sprMoveAfter.TOU_NO_R.ColNo, drTouSotu(0).Item("TOU_NO").ToString())
                spr.SetCellValue(rowNo, LMD020G.sprMoveAfter.SITU_NO_R.ColNo, drTouSotu(0).Item("SITU_NO").ToString())
                spr.SetCellValue(rowNo, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo, drTouSotu(0).Item("ZONE_CD").ToString())

            Next

        End With


        Return True

    End Function

    ''' <summary>
    ''' 保存時の届先マスタ存在チェック(移動先スプレッド)
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsExistDestChk(ByVal arr As ArrayList) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New lmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        Dim spr As Win.Spread.LMSpread = Me._Frm.sprMoveAfter

        With spr.ActiveSheet

            Dim drDest As DataRow() = Nothing
            Dim max As Integer = arr.Count - 1

            Dim brcd As String = String.Empty
            Dim custCdL As String = String.Empty
            Dim destCd As String = String.Empty
            Dim rowNo As Integer = 0

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arr(i))
                brcd = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.NRS_BR_CD_R.ColNo)).ToString()
                custCdL = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.CUST_CD_L_R.ColNo)).ToString()
                destCd = Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo)).ToString()

                If String.IsNullOrEmpty(destCd) = True Then
                    '届先コードが空の場合チェックは行わない
                    Return True
                End If

                drDest = Me._Vcon.SelectDestListDataRow(brcd, custCdL, destCd)

                '届先マスタ存在チェック
                If drDest.Length < 1 Then
                    Me._Vcon.SetErrorControl(spr, rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo)
              
                    '2017/09/25 修正 李↓
                    Dim msg As String = String.Empty
                    msg = lgm.Selector({"届先マスタ", "Shipping master", "송달처마스터", "中国語"})
                    '2017/09/25 修正 李↑

                    Me._Vcon.SetMstErrMessage(msg, destCd)
                    Return False
                End If

                'マスタの値を設定
                spr.SetCellValue(rowNo, LMD020G.sprMoveAfter.DEST_CD_R.ColNo, drDest(0).Item("DEST_CD").ToString())
                spr.SetCellValue(rowNo, LMD020G.sprMoveAfter.DEST_NM_R.ColNo, drDest(0).Item("DEST_NM").ToString())

            Next

        End With


        Return True

    End Function

#End Region

#Region "大小チェック"

    ''' <summary>
    ''' 移動個数大小チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrSaki">移動先リスト</param>
    ''' <param name="arrMoto">移動元リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIdoDaishoCheck(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList) As Boolean

        '複数移動を選択した場合
        If Me._Frm.optFukusuIdo.Checked = True Then
            Return Me.IsIdoZaikoSumChk(frm, arrSaki, arrMoto)
        End If

        '平行移動を選択した場合
        Return Me.IsIdoZaikoChk(frm, arrSaki, arrMoto)

    End Function

    ''' <summary>
    ''' 残数、移動個数(合計)チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrSaki">移動先リスト</param>
    ''' <param name="arrMoto">移動元リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIdoZaikoSumChk(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList) As Boolean

        Dim zansu As Integer = 0
        Dim idoKosu As Integer = 0

        '引当可能個数の取得
        Dim motoMax As Integer = arrMoto.Count - 1
        For i As Integer = 0 To motoMax
            zansu = zansu + Convert.ToInt32(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo)).ToString())

        Next

        '移動個数の合計を取得
        Dim sakiMax As Integer = arrSaki.Count - 1
        For i As Integer = 0 To sakiMax
            idoKosu = idoKosu + Convert.ToInt32(Me._Vcon.GetCellValue(frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(i)), LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)).ToString())
        Next

        '移動個数＞引当可能在庫数の場合エラー
        If idoKosu > zansu = True Then
            frm.sprMoveAfter.ActiveSheet.Cells(Convert.ToInt32(arrSaki(0)), LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo).BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
            Me._Vcon.SetErrMessage("E288")
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 残数、移動個数チェック(ワーニング)
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrSaki">移動先リスト</param>
    ''' <param name="arrMoto">移動元リスト</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsIdoZaikoChk(ByVal frm As LMD020F, ByVal arrSaki As ArrayList, ByVal arrMoto As ArrayList) As Boolean

        '平行移動により同一ループ
        Dim max As Integer = arrSaki.Count - 1
        Dim spr As FarPoint.Win.Spread.SheetView = Me._Frm.sprMoveBefor.ActiveSheet
        Dim sakiSpr As Win.Spread.LMSpread = Me._Frm.sprMoveAfter
        Dim motoValue As Decimal = 0
        Dim sakiValue As Decimal = 0
        Dim chkCnt As Integer = -1
        Dim rowNo As Integer = 0

        With sakiSpr.ActiveSheet

            For i As Integer = 0 To max

                rowNo = Convert.ToInt32(arrSaki(i))

                motoValue = Convert.ToDecimal(Me._Vcon.GetCellValue(spr.Cells(rowNo, LMD020G.sprMoveBefor.ALLOC_CAN_NB.ColNo)))
                sakiValue = Convert.ToDecimal(Me._Vcon.GetCellValue(.Cells(rowNo, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo)))

                '元残数 < 移動個数(初回ワーニング)
                If motoValue < sakiValue Then

                    chkCnt += 1
                    If chkCnt < 1 Then

                        'ワーニング表示
                        If MyBase.ShowMessage("W175") <> MsgBoxResult.Ok Then
                            Return False
                        End If

                    End If

                    '自動調整(残数をそのまま設定)
                    sakiSpr.SetCellValue(rowNo, LMD020G.sprMoveAfter.IDO_KOSU_R.ColNo, motoValue.ToString())

                End If

            Next

        End With

        Return True

    End Function

    ''' <summary>
    ''' 移動日チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function idoDateChk(ByVal frm As LMD020F, ByVal arrMoto As ArrayList) As Boolean

        Dim inkoDate As String = String.Empty
        Dim idoDate As String = frm.imdIdoubi.TextValue
        Dim max As Integer = arrMoto.Count - 1
        For i As Integer = 0 To max

            '入荷日の取得
            inkoDate = Jp.Co.Nrs.Com.Utility.DateFormatUtility.DeleteSlash(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.INKO_DATE.ColNo)).ToString())

            '入荷日FromToチェック
            If Me._Vcon.IsFromToChk(inkoDate, idoDate) = True Then

                Continue For

            End If

            Me.SetErrorControl(frm.imdIdoubi)
            '20151029 tsunehira add Start
            '英語化対応
            Me._Vcon.SetErrMessage("E615")
            '20151029 tsunehira add End
            'Me._Vcon.SetErrMessage("E039", New String() {"移動日", "入荷日"})
            Return False

        Next

        Return True

    End Function

#End Region

#Region "小分け出荷完了チェック"

    ''' <summary>
    '''小分け出荷完了チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsCompletedKowake(ByVal frm As LMD020F, ByVal arrMoto As ArrayList) As Boolean

        Dim max As Integer = arrMoto.Count - 1
        Dim alctdQt As Decimal = 0
        Dim irime As Decimal = 0
        For i As Integer = 0 To max

            '引当中数量の取得
            alctdQt = Convert.ToDecimal(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ALCTD_QT.ColNo)).ToString())
            irime = Convert.ToDecimal(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.IRIME.ColNo)).ToString())
            '入り目との比較チェック
            If alctdQt = 0 OrElse irime <= alctdQt Then

                Continue For

            End If
            '20151029 tsunehira add Start
            '英語化対応
            Me._Vcon.SetErrMessage("E809", New String() {String.Concat(Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo)).ToString())})
            '20151029 tsunehira add End
            'Me._Vcon.SetErrMessage("E543", New String() {String.Concat("在庫レコードNo.：", Me._Vcon.GetCellValue(frm.sprMoveBefor.ActiveSheet.Cells(Convert.ToInt32(arrMoto(i)), LMD020G.sprMoveBefor.ZAI_REC_NO.ColNo)).ToString())})
            Return False

        Next

        Return True

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' スペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimSpaceTextValue()

        'ヘッダ項目のスペース除去
        Call Me.TrimHeaderSpaceTextValue()

        'START YANAI 要望番号548
        ''移動元スプレッドのスペース除去
        'Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprMoveBefor)

        ''移動先スプレッドのスペース除去
        'Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprMoveAfter)
        '移動元スプレッドのスペース除去
        Call Me.TrimSpaceSprTextvalue(Me._Frm.sprMoveBefor)

        '移動先スプレッドのスペース除去
        Call Me.TrimSpaceSprTextvalue(Me._Frm.sprMoveAfter)
        'END YANAI 要望番号548

    End Sub

    ''' <summary>
    ''' ヘッダ項目のスペース除去
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub TrimHeaderSpaceTextValue()

        With Me._Frm

            .txtCustCdL.TextValue = .txtCustCdL.TextValue.Trim()
            .txtCustCdM.TextValue = .txtCustCdM.TextValue.Trim()
            .txtJiyuran.TextValue = .txtJiyuran.TextValue.Trim()
            .txtTouNo.TextValue = .txtTouNo.TextValue.Trim()
            .txtSituNo.TextValue = .txtSituNo.TextValue.Trim()
            .txtZoneCd.TextValue = .txtZoneCd.TextValue.Trim()
            .txtLocation.TextValue = .txtLocation.TextValue.Trim()

        End With

    End Sub

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

        ElseIf TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).BackColorDef = errorColor

        End If

        ctl.Focus()
        ctl.Select()

    End Sub

    'START YANAI 要望番号548
    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread)

        With spr
            Dim rowMax As Integer = .ActiveSheet.Rows.Count - 1

            For i As Integer = 0 To rowMax

                Call Me.TrimSpaceSprTextvalue(spr, i)

            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドの値をTrim
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <remarks></remarks>
    Friend Sub TrimSpaceSprTextvalue(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer)

        With spr

            Dim colMax As Integer = .ActiveSheet.Columns.Count - 1
            Dim aCell As Cell = Nothing

            For i As Integer = 0 To colMax

                aCell = .ActiveSheet.Cells(rowNo, i)

                If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.CheckBoxCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.DateTimeCellType = True _
                    OrElse TypeOf aCell.Editor Is CellType.NumberCellType = True _
                    Then
                    '処理なし
                Else
                    If (LMD020G.sprMoveBefor.LOCA.ColNo).Equals(i) = False AndAlso _
                        (LMD020G.sprMoveBefor.REMARK.ColNo).Equals(i) = False AndAlso _
                        (LMD020G.sprMoveBefor.REMARK_OUT.ColNo).Equals(i) = False AndAlso _
                        (LMD020G.sprMoveAfter.LOCA_R.ColNo).Equals(i) = False AndAlso _
                        (LMD020G.sprMoveAfter.REMARK_R.ColNo).Equals(i) = False AndAlso _
                        (LMD020G.sprMoveAfter.REMARK_OUT_R.ColNo).Equals(i) = False Then
                        'LOCA、備考小(社内)、備考小(社外)以外の時のみTRIMを行う
                        .SetCellValue(rowNo, i, Me._Vcon.GetCellValue(.ActiveSheet.Cells(rowNo, i)))
                    End If
                End If

            Next

        End With

    End Sub
    'END YANAI 要望番号548
#End Region


    ''' <summary>
    ''' 棟 + 室 + ZONE（置き場情報）温度管理チェック 
    ''' </summary>
    ''' <param name="arrMoto"></param>
    ''' <param name="arrSaki"></param>
    ''' <param name="ikkatsu">0：行毎、1：一括</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsOndoCheck(ByVal arrMoto As ArrayList, ByVal arrSaki As ArrayList, ByVal ikkatsu As String) As Boolean

        '2017/09/25 追加 李↓
        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)
        '2017/09/25 追加 李↑

        With Me._Frm

            ''Dim sakiMax As Integer = arrSaki.Count - 1
            Dim motoMax As Integer = arrSaki.Count - 1
            Dim checkRow As Integer = 0

            Dim nrsbrcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim custcd As String = String.Empty
            'Dim subkb As String = "13"
            'Dim sql As String = String.Empty

            Dim custDtlDr As DataRow() = Nothing

            Dim goodsNRS As String = String.Empty
            Dim goodsDr As DataRow() = Nothing

            '前
            Dim ondokbn As String = String.Empty
            Dim ondoStartDate As String = String.Empty
            Dim ondoEndDate As String = String.Empty
            Dim ondoUpper As String = String.Empty
            Dim ondoLower As String = String.Empty

            '後
            Dim tousituDr As DataRow() = Nothing
            Dim zoneDr As DataRow() = Nothing

            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim zoneCd As String = String.Empty
            Dim ondoCtlKbn_TouSitu As String = String.Empty
            Dim ondoCtlKbn_Zone As String = String.Empty
            Dim ondo_TouSitu As String = String.Empty
            Dim ondo_Zone As String = String.Empty

            Dim msg As String = String.Empty

            '倉庫マスタチェック
            Dim sokoDrs As DataRow() = Me._Vcon.SelectSokoListDataRow(.cmbSoko.SelectedValue.ToString())
            If sokoDrs.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", .cmbSoko.SelectedValue.ToString()))
            If sokoDrs(0).Item("LOC_MANAGER_YN").ToString = "00" Then
                Return True
            End If

            For i As Integer = 0 To motoMax

                checkRow = Convert.ToInt32(arrSaki(i).ToString)
                Dim checkLeftRow As Integer = checkRow
                If Not .optHeikouIdo.Checked Then
                    '複数移動の場合
                    checkLeftRow = Convert.ToInt32(arrMoto(0).ToString)
                End If

                ''移動前
                'custcd = Me._Vcon.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkRow, LMD020G.sprMoveBefor.CUST_CD_L.ColNo)).ToString
                ''Dim inakLNo As String = .lblKanriNoL.TextValue.Trim
                'sql = String.Concat("NRS_BR_CD = ", " '", nrsbrcd, "' ", _
                '                                 " AND CUST_CD = ", " '", custcd, "' ", _
                '                                 " AND SUB_KB = ", " '", subkb, "' ")

                ''キャッシュテーブルからデータ抽出
                'custDtlDr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(sql)

                'If custDtlDr.Length < 1 Then Continue For

                'If "1".Equals(custDtlDr(0).Item("SET_NAIYO").ToString) = False Then Continue For

                goodsNRS = Me._Vcon.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkLeftRow, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString
                goodsDr = Me._Vcon.SelectgoodsListDataRow(nrsbrcd, goodsNRS)

                '判定

                '2017/09/25 修正 李↓
                msg = lgm.Selector({"商品マスタ", "Product master", "상품마스터", "中国語"})
                '2017/09/25 修正 李↑

                If goodsDr.Length < 1 Then Return Me._Vcon.SetMstErrMessage(msg, String.Concat(nrsbrcd, " - ", goodsNRS))

                ondokbn = goodsDr(0).Item("ONDO_KB").ToString
                ondoStartDate = goodsDr(0).Item("ONDO_STR_DATE").ToString
                ondoEndDate = goodsDr(0).Item("ONDO_END_DATE").ToString
                ondoUpper = goodsDr(0).Item("ONDO_MX").ToString
                ondoLower = goodsDr(0).Item("ONDO_MM").ToString

                '判定
                If Not "02".Equals(ondokbn) Then Continue For


                Dim idoYYYY As String = Left(.imdIdoubi.TextValue, 4)
                Dim startYYYY As String = idoYYYY
                Dim endYYYY As String = idoYYYY
                If ondoStartDate > ondoEndDate Then endYYYY = (Integer.Parse(idoYYYY) + 1).ToString

                '判定
                If Not (String.Concat(startYYYY, ondoStartDate) <= .imdIdoubi.TextValue _
                        And String.Concat(endYYYY, ondoEndDate) >= .imdIdoubi.TextValue) Then Continue For


                '移動後
                If "0".Equals(ikkatsu) Then
                    touNo = Me._Vcon.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString
                    situNo = Me._Vcon.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString
                    zoneCd = Me._Vcon.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.ZONE_CD_R.ColNo)).ToString
                Else
                    '一括変更
                    touNo = .txtTouNo.TextValue.Trim
                    situNo = .txtSituNo.TextValue.Trim
                    zoneCd = .txtZoneCd.TextValue.Trim
                End If

                '棟室マスタ
                tousituDr = Me._Vcon.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)
                '棟室マスタになければ以降のチェックなし
                If tousituDr.Length.Equals(0) Then
                    Continue For
                End If

                ondoCtlKbn_TouSitu = tousituDr(0).Item("TOU_ONDO_CTL_KB").ToString
                ondo_TouSitu = tousituDr(0).Item("TOU_ONDO").ToString

                'ゾーンマスタ
                zoneDr = Me._Vcon.SelectZoneListDataRow(nrsbrcd, sokocd, touNo, situNo, zoneCd)
                'ゾーンマスタになければ以降のチェックなし
                If zoneDr.Length.Equals(0) Then
                    Continue For
                End If

                ondoCtlKbn_Zone = zoneDr(0).Item("ZONE_ONDO_CTL_KB").ToString
                ondo_Zone = zoneDr(0).Item("ZONE_ONDO").ToString

                '判定①
                If Not "02".Equals(ondoCtlKbn_TouSitu) And Not "02".Equals(ondoCtlKbn_Zone) Then
                    '2015.10.22 tusnehira add
                    '英語化対応
                    msg = String.Concat(touNo, "-", situNo, "-", zoneCd)
                    If MyBase.ShowMessage("W241", New String() {msg}) = MsgBoxResult.Cancel Then Return False

                    'msg = String.Concat("置場　", touNo, "-", situNo, "-", zoneCd)
                    'If MyBase.ShowMessage("W191", New String() {msg, "定温置場"}) = MsgBoxResult.Cancel Then Return False
                End If

                '判定②
                '　ondoLower <= ondo_TouSitu <= ondoUpper　　　　　　　　　8>15 or 15 >12 ◎　　
                '　ondoLower <= ondo_Zone <= ondoUpper                     8>10 or 10 >12 ◎
                If String.IsNullOrEmpty(ondo_Zone) Then
                    'ゾーンなし
                    If ("02".Equals(ondoCtlKbn_TouSitu) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_TouSitu) Or Integer.Parse(ondo_TouSitu) > Integer.Parse(ondoUpper))) Then
                        msg = String.Concat(touNo, "-", situNo)
                        If MyBase.ShowMessage("W240", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                    End If
                Else
                    'ゾーンあり
                    If ("02".Equals(ondoCtlKbn_TouSitu) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_TouSitu) Or Integer.Parse(ondo_TouSitu) > Integer.Parse(ondoUpper))) _
                    Or ("02".Equals(ondoCtlKbn_Zone) And (Integer.Parse(ondoLower) > Integer.Parse(ondo_Zone) Or Integer.Parse(ondo_Zone) > Integer.Parse(ondoUpper))) Then
                        msg = String.Concat(touNo, "-", situNo, "-", zoneCd)
                        If MyBase.ShowMessage("W240", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                    End If
                End If

            Next

        End With

        Return True

    End Function

    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add start
    ''' <summary>
    ''' 危険物倉庫棟室チェック
    ''' </summary>
    ''' <param name="frm">フォーム</param>
    ''' <param name="arrMoto">チェック行群</param>
    ''' <param name="ikkatsu">0：行毎、1：一括</param>
    ''' <param name="expDs"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsDangerousGoodsCheck(ByVal frm As LMD020F, ByVal arrMoto As ArrayList, ByVal ikkatsu As String, ByVal expDs As DataSet) As Boolean

        '多言語対応用ユーティリティ
        Dim lgm As New LmLangMGR(MessageManager.MessageLanguage)

        With Me._Frm

            Dim motoMax As Integer = arrMoto.Count - 1
            Dim checkRow As Integer = 0

            Dim nrsbrcd As String = .cmbNrsBrCd.SelectedValue.ToString
            Dim sokocd As String = .cmbSoko.SelectedValue.ToString
            Dim custcd As String = .txtCustCdL.TextValue

            Dim goodsNRS As String = String.Empty
            Dim goodsDr As DataRow() = Nothing
            Dim kikenkbn As String = String.Empty

            Dim tousituDr As DataRow() = Nothing
            Dim soko_kbn As String = String.Empty
            Dim isTasya As Boolean = False

            Dim touNo As String = String.Empty
            Dim situNo As String = String.Empty
            Dim msg As String = String.Empty

            '危険物チェック結果をワーニングで出すかエラーで出すかの情報を区分テーブルより取得
            Dim selectString As String = String.Empty
            '削除フラグ
            selectString = String.Concat(selectString, " SYS_DEL_FLG = '0' ")
            '区分グループコード
            selectString = String.Concat(selectString, " AND KBN_GROUP_CD = 'S111' ")
            '区分コード
            selectString = String.Concat(selectString, " AND KBN_CD = '0' ")

            Dim targetDataRow() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(selectString)
            Dim IsCheckError As Boolean = Convert.ToInt32(Convert.ToDecimal(targetDataRow(0).Item("VALUE1").ToString)).Equals(Convert.ToInt32(LMD020C.DangerousGoodsCheckErrorOrWarning.Err))

            For i As Integer = 0 To motoMax

                checkRow = Convert.ToInt32(arrMoto(i).ToString)

                '商品マスタ
                goodsNRS = Me._Vcon.GetCellValue(.sprMoveBefor.ActiveSheet.Cells(checkRow, LMD020G.sprMoveBefor.GOODS_CD_NRS.ColNo)).ToString
                goodsDr = Me._Vcon.SelectgoodsListDataRow(nrsbrcd, goodsNRS)

                kikenkbn = goodsDr(0).Item("KIKEN_KB").ToString

                '移動後
                If "0".Equals(ikkatsu) Then
                    touNo = Me._Vcon.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.TOU_NO_R.ColNo)).ToString
                    situNo = Me._Vcon.GetCellValue(.sprMoveAfter.ActiveSheet.Cells(checkRow, LMD020G.sprMoveAfter.SITU_NO_R.ColNo)).ToString
                Else
                    '一括変更
                    touNo = .txtTouNo.TextValue.Trim
                    situNo = .txtSituNo.TextValue.Trim
                End If

                '棟室マスタ
                tousituDr = Me._Vcon.SelectTouSituListDataRow(nrsbrcd, sokocd, touNo, situNo)

                '棟室マスタが取得できなければエラー、ワーニングとも起こさせない。
                If tousituDr.Length.Equals(0) Then
                    Continue For
                End If

                soko_kbn = tousituDr(0).Item("SOKO_KB").ToString
                isTasya = tousituDr(0).Item("JISYATASYA_KB").ToString.Equals("02")

                If IsCheckError Then
                    '危険物チェックをエラーとする場合

                    '危険物チェック
                    Dim isErr As Boolean = False
                    If (kikenkbn.Equals("02") OrElse kikenkbn.Equals("03")) AndAlso soko_kbn = "11" Then
                        If isTasya Then
                            isErr = True
                        ElseIf String.IsNullOrEmpty(touNo) OrElse String.IsNullOrEmpty(situNo) Then
                            isErr = True
                        Else
                            Dim drTouSituExp As DataRow() = expDs.Tables(LMD020C.TABLE_NM_TOU_SITU_EXP).Select("TOU_NO = '" & touNo & "' AND SITU_NO = '" & situNo & "'")
                            If drTouSituExp.Count = 0 Then
                                isErr = True
                            End If
                        End If
                        If isErr Then
                            msg = String.Concat(touNo, "-", situNo)
                            MyBase.ShowMessage("G092", New String() {msg})
                            Return False
                        End If
                    End If

                    '一般品の商品(kikenkbn=01、04)で、倉庫区分(soko_kb=02)が危険物倉庫であればワーニング
                    If (kikenkbn.Equals("01") OrElse kikenkbn.Equals("04")) AndAlso soko_kbn = "02" Then
                        msg = String.Concat(touNo, "-", situNo)
                        MyBase.ShowMessage("G091", New String() {msg})
                        Return False
                    End If
                Else

                    '2012/12/06 他社の場合はワーニングチェックを入れない処理を追加
                    If isTasya.Equals(False) Then

                        '危険物チェックをワーニングとする場合
                        msg = String.Concat(touNo, "-", situNo)

                        '危険品(kikenkbn<>01)で、倉庫区分(soko_kb=11)が普通倉庫であればワーニング
                        If kikenkbn <> "01" AndAlso soko_kbn = "11" Then
                            If MyBase.ShowMessage("W239", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        End If
                        '一般品の商品(kikenkbn=01)で、倉庫区分(soko_kb=02)が危険物倉庫であればワーニング
                        If kikenkbn.Equals("01") AndAlso soko_kbn = "02" Then
                            If MyBase.ShowMessage("W238", New String() {msg}) = MsgBoxResult.Cancel Then Return False
                        End If

                    End If

                End If

            Next

        End With

        Return True
    End Function
    '2017/10/31 棟室マスタ,在庫_危険品温度管理アラート→エラー対応 Annen add end

#End Region 'Method

End Class
