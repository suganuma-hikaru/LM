' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC     : 出荷
'  プログラムID     :  LMC040C : 在庫引当
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC040Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMC040V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMC040F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Gcon As LMCControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _Vcon As LMCControlV

    'START KIM 要望番号1479 一括引当時、引当画面の速度改善

    ''' <summary>
    ''' 格納変数（残個数）
    ''' </summary>
    ''' <remarks></remarks>
    Friend _numHikiZanCnt As Decimal = 0

    ''' <summary>
    ''' 格納変数（残数量）
    ''' </summary>
    ''' <remarks></remarks>
    Friend _numHikiZanAmt As Decimal = 0

    'END KIM 要望番号1479 一括引当時、引当画面の速度改善

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMC040F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Validate共通クラスの設定
        Me._Vcon = New LMCControlV(handlerClass, DirectCast(frm, Form))

        'Gamen共通クラスの設定
        Me._Gcon = New LMCControlG(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(Optional ByVal eventShubetsu As LMC040C.EventShubetsu = LMC040C.EventShubetsu.SYOKI _
                                  , Optional ByVal eigyo As String = "00" _
                                  , Optional ByVal soko As String = "00") As Boolean

        With Me._Frm

            Call Me.valueTrim()

            Select Case eventShubetsu

                Case LMC040C.EventShubetsu.KENSAKU, LMC040C.EventShubetsu.SENTAKU

                    '入目
                    If Me.IsHaniCheck(Convert.ToDecimal(Me._Frm.numIrime.Value), Convert.ToDecimal(LMC040C.IRIME_MIN_NUM), Convert.ToDecimal(LMC040C.IRIME_MAX_NUM)) = False Then
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E014", New String() {_Frm.lblIrime.TextValue, LMC040C.IRIME_MIN_NUM, LMC040C.IRIME_MAX_NUM})
                        'MyBase.ShowMessage("E014", New String() {"入目", LMC040C.IRIME_MIN_NUM, LMC040C.IRIME_MAX_NUM})
                        Me._Vcon.SetErrorControl(Me._Frm.numIrime)
                        Return False
                    End If

            End Select

            Dim max As Integer = .sprZaiko.ActiveSheet.Rows.Count - 1

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                For i As Integer = 1 To max

                    If Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.DEF.ColNo)).Equals(LMConst.FLG.ON) = True Then
                        '営業所
                        If eigyo.Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.NRS_BR_CD.ColNo))) = False Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E221", New String() {.lblEigyo.TextValue})
                            'MyBase.ShowMessage("E221", New String() {"営業所"})
                            Return False
                        End If

                        '倉庫
                        If soko.Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.WH_CD.ColNo))) = False Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E221", New String() {.lblEigyo.TextValue})
                            'MyBase.ShowMessage("E221", New String() {"倉庫"})
                            Return False
                        End If
                    End If

                Next i
            End If

            If LMC040C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                'シリアル№
                .txtSerialNO.TextValue = Trim(.txtSerialNO.TextValue)
                .txtSerialNO.ItemName() = .lblSerialNO.Text
                .txtSerialNO.IsForbiddenWordsCheck() = True
                .txtSerialNO.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtSerialNO) = False Then
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '予約番号
                .txtRsvNO.TextValue = Trim(.txtRsvNO.TextValue)
                .txtRsvNO.ItemName() = .lblReserveNO.Text
                .txtRsvNO.IsForbiddenWordsCheck() = True
                .txtRsvNO.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtRsvNO) = False Then
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                'ロット番号
                .txtLotNO.TextValue = Trim(.txtLotNO.TextValue)
                .txtLotNO.ItemName() = .lblLotNO.Text
                .txtLotNO.IsForbiddenWordsCheck() = True
                .txtLotNO.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtLotNO) = False Then
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '梱数
                .numSyukkaKosu.TextValue = Trim(.numSyukkaKosu.TextValue)
                .numSyukkaKosu.ItemName() = .lblSyukkaKosu.Text
                If MyBase.IsValidateCheck(.numSyukkaKosu) = False Then
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷端数
                .numSyukkaHasu.TextValue = Trim(.numSyukkaHasu.TextValue)
                .numSyukkaHasu.ItemName() = .lblSyukkaHasu.Text
                If MyBase.IsValidateCheck(.numSyukkaHasu) = False Then
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷数量
                .numSyukkaSouAmt.TextValue = Trim(.numSyukkaSouAmt.TextValue)
                .numSyukkaSouAmt.ItemName() = .lblSyukkaSouAmtl.Text
                If MyBase.IsValidateCheck(.numSyukkaSouAmt) = False Then
                    Return False
                End If
            End If

            '【一覧チェック】
            If Me.IsSprChk(eventShubetsu) = False Then
                Return False
            End If

            'チェックリスト格納変数
            Dim list As ArrayList = New ArrayList()

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '未選択チェック
                'チェックリスト取得
                list = Me.getCheckList()

                '未選択チェック
                If _Vcon.IsSelectChk(list.Count()) = False Then
                    MyBase.ShowMessage("E009")
                    Return False
                End If
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' スプレッド(検索行)単項目チェック
    ''' </summary>
    ''' <param name="eventShubetsu">イベント種別</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsSprChk(ByVal eventShubetsu As LMC040C.EventShubetsu) As Boolean

        '検索処理以外はスルー
        If LMC040C.EventShubetsu.KENSAKU <> eventShubetsu Then
            Return True
        End If

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprZaiko)

        '棟
        vCell.SetValidateCell(0, LMC040G.sprZaiko.TOU_NO.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LMC040G.sprZaiko.TOU_NO.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 2
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '室
        vCell.SetValidateCell(0, LMC040G.sprZaiko.SHITSU_NO.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LMC040G.sprZaiko.SHITSU_NO.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        'START YANAI 要望番号705
        'vCell.IsByteCheck = 1
        vCell.IsByteCheck = 2
        'END YANAI 要望番号705
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        'ZONE
        vCell.SetValidateCell(0, LMC040G.sprZaiko.ZONE_CD.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LMC040G.sprZaiko.ZONE_CD.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        'START YANAI 要望番号705
        'vCell.IsByteCheck = 1
        vCell.IsByteCheck = 2
        'END YANAI 要望番号705
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '備考小（社内）
        vCell.SetValidateCell(0, LMC040G.sprZaiko.REMARK.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LMC040G.sprZaiko.REMARK.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 100
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '備考小（社外）
        vCell.SetValidateCell(0, LMC040G.sprZaiko.REMARK_OUT.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LMC040G.sprZaiko.REMARK_OUT.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 15
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '届先名
        vCell.SetValidateCell(0, LMC040G.sprZaiko.DEST_NM.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LMC040G.sprZaiko.DEST_NM.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 80
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        Return True

    End Function

    ''' <summary>
    ''' vbCrLf,"　"を空文字に変換
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <returns>値</returns>
    ''' <remarks></remarks>
    Private Function SetRepMsgData(ByVal value As String) As String

        value = value.Replace(vbCrLf, String.Empty)
        value = value.Replace("　", String.Empty)
        Return value

    End Function

    ''' <summary>
    ''' 入力チェック（関連チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsKanrenCheck(Optional ByVal eventShubetsu As LMC040C.EventShubetsu = LMC040C.EventShubetsu.SYOKI) As Boolean

        With Me._Frm

            If (LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True OrElse _
                LMC040C.EventShubetsu.CAL_SURYO.Equals(eventShubetsu) = True) AndAlso _
                (.optKowake.Checked = False AndAlso .optSample.Checked = False) Then
                '出荷数量 + 入目
                Dim sKosu As Decimal = 0
                sKosu = Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                            Mod (Convert.ToDecimal(.numIrime.Value))
                If sKosu <> 0 Then
                    .numSyukkaSouAmt.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numIrime)
                    MyBase.ShowMessage("E170", New String() {String.Empty})
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.TANINUSI.Equals(eventShubetsu) = True Then
                '振替対象商品マスタ存在チェック
                If Me.IsFuriGoodsffExistChk(.lblGoodsNRS.TextValue) = False Then
                    .lblGoodsNM.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.lblGoodsCD)
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E755", New String() {.lblGoodsCD.TextValue})
                    'MyBase.ShowMessage("E079", New String() {"振替対象商品マスタ", .lblGoodsCD.TextValue})
                    Return False
                End If
            End If

            Dim likeFlg As Boolean = False

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷単位 + 梱数 + 出荷端数
                If .optCnt.Checked = True Then

                    Dim custDetailsDr() As DataRow = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, _
                                                 "' AND CUST_CD = '", .lblCustCD_L.TextValue, _
                                                 "' AND SUB_KB = '80'"))

                    If ("0").Equals(.numSyukkaKosu.Value.ToString()) = True AndAlso ("0").Equals(.numSyukkaHasu.Value.ToString()) = True Then

                        If custDetailsDr.Length = 0 AndAlso String.IsNullOrEmpty(.lblGoodsNM.TextValue) = False Then

                            If Me._Frm.numSyukkaHasu.ReadOnly = False Then
                                .numSyukkaHasu.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                            End If
                            Me._Vcon.SetErrorControl(.numSyukkaKosu)
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E759")
                            'MyBase.ShowMessage("E187", New String() {"出荷単位が個数", "梱数または出荷端数"})
                            Return False

                        Else
                            likeFlg = True

                        End If

                    ElseIf custDetailsDr.Length = 0 AndAlso String.IsNullOrEmpty(.lblGoodsNM.TextValue) = False Then

                    Else
                        likeFlg = True

                    End If
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷単位（数量） + 出荷数量
                If .optAmt.Checked = True Then
                    If (LMC040C.PLUS_ZERO).Equals(.numSyukkaSouAmt.Value.ToString()) = True OrElse _
                       (LMC040C.MINUS_ZERO).Equals(.numSyukkaSouAmt.Value.ToString()) = True Then
                        Me._Vcon.SetErrorControl(.numSyukkaSouAmt)
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E761")
                        'MyBase.ShowMessage("E187", New String() {"出荷単位が数量", "出荷数量"})
                        Return False
                    End If
                End If
            End If

            '要望管理番号2523 20160420 tsunehira add start
            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷単位（小分け） + 引き当て数が複数
                Dim ChkList As ArrayList = New ArrayList()
                ChkList = Me.getCheckList
                If .optKowake.Checked = True AndAlso (ChkList.Count() - 1) >= 1 Then
                    MyBase.ShowMessage("E428", New String() {"小分けは複数行選択している", "選択を完了", "１行だけ選んでください"})
                    Return False
                End If
            End If
            '要望管理番号2523 20160420 tsunehira add start

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷単位（小分け） + 出荷数量
                If .optKowake.Checked = True Then
                    If (LMC040C.PLUS_ZERO).Equals(.numSyukkaSouAmt.Value.ToString()) = True OrElse _
                       (LMC040C.MINUS_ZERO).Equals(.numSyukkaSouAmt.Value.ToString()) = True Then
                        Me._Vcon.SetErrorControl(.numSyukkaSouAmt)
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E760")
                        'MyBase.ShowMessage("E187", New String() {"出荷単位が小分け", "出荷数量"})
                        Return False
                    End If
                End If
            End If

            'If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
            '    '出荷単位（サンプル） + 出荷数量
            '    If .optSample.Checked = True Then
            '        If (LMC040C.PLUS_ZERO).Equals(.numSyukkaSouAmt.Value.ToString()) = True OrElse _
            '           (LMC040C.MINUS_ZERO).Equals(.numSyukkaSouAmt.Value.ToString()) = True Then
            '            Me._Vcon.SetErrorControl(.numSyukkaSouAmt)
            '            MyBase.ShowMessage("E187", New String() {"出荷単位がサンプル", "出荷数量"})
            '            Return False
            '        End If
            '    End If
            'End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷単位（小分け・サンプル） + 出荷数量 + 入目
                If .optKowake.Checked = True OrElse .optSample.Checked = True Then
                    '(2012.12.14)要望番号1680 入目<出荷数量 → 入目≦出荷数量に変更 -- START --
                    If Convert.ToDecimal(.numIrime.Value) <= Convert.ToDecimal(.numSyukkaSouAmt.Value) Then
                        'If Convert.ToDecimal(.numIrime.Value) < Convert.ToDecimal(.numSyukkaSouAmt.Value) Then
                        '(2012.12.14)要望番号1680 入目<出荷数量 → 入目≦出荷数量に変更 -- END  --
                        .numIrime.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numSyukkaSouAmt)
                        MyBase.ShowMessage("E191")
                        Return False
                    End If
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷個数 + 引当個数合計
                'START YANAI 20110913 小分け対応
                'If Convert.ToDecimal(.numSyukkaSouCnt.Value) <> Convert.ToDecimal(.numHikiCntSum.Value) Then
                If Convert.ToDecimal(.numSyukkaSouCnt.Value) <> Convert.ToDecimal(.numHikiCntSum.Value) AndAlso _
                    .optKowake.Checked = False Then
                    'END YANAI 20110913 小分け対応
                    '英語化対応
                    '20151022 tsunehira add
                    'If MyBase.ShowMessage("W121", New String() {"出荷個数", "引当個数合計"}) = MsgBoxResult.Cancel Then
                    If MyBase.ShowMessage("W121", New String() {_Frm.lblSyukkaSouCnt.TextValue, _Frm.lblHikiCntSum.TextValue}) = MsgBoxResult.Cancel Then
                        .numSyukkaSouCnt.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numHikiCntSum)
                        Return False
                    End If
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '出荷数量 + 引当数量合計
                If Convert.ToDecimal(.numSyukkaSouAmt.Value) <> Convert.ToDecimal(.numHikiAmtSum.Value) Then
                    If .optKowake.Checked = False Then
                        '英語化対応
                        '20151022 tsunehira add
                        'If MyBase.ShowMessage("W121", New String() {"出荷数量", "引当数量合計"}) = MsgBoxResult.Cancel Then
                        If MyBase.ShowMessage("W121", New String() {_Frm.lblSyukkaSouAmtl.TextValue, _Frm.lblHikiAmtSum.TextValue}) = MsgBoxResult.Cancel Then
                            .numSyukkaSouAmt.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                            Me._Vcon.SetErrorControl(.numHikiAmtSum)
                            Return False
                        End If
                    Else
                        '英語化対応
                        '20151022 tsunehira add
                        MyBase.ShowMessage("E456", New String() {_Frm.lblSyukkaSouAmtl.TextValue, _Frm.lblHikiAmtSum.TextValue})
                        'MyBase.ShowMessage("E456", New String() {"出荷数量", "引当数量合計"})
                        .numSyukkaSouAmt.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numHikiAmtSum)
                        Return False
                    End If

                End If
            End If

            '【一覧チェック】
            Dim max As Integer = .sprZaiko.ActiveSheet.Rows.Count - 1
            Dim kosu As Decimal = 0
            Dim suryo As Decimal = 0

            Dim spdOLT As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'H003' AND KBN_CD = '05' ")
            Dim spdIS As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'H003' AND KBN_CD = '06' ")
            Dim spdSyukka As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'H003' AND KBN_CD = '01' ")
            Dim aloRes As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'W001' AND KBN_CD = '20' ")

            '2019/12/25 要望管理006868 add start
            Dim bohinNm As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'B002' AND KBN_CD = '01' ")
            Dim wZaiRem As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, _
                                                                                                        "' AND CUST_CD = '", .lblCustCD_L.TextValue, _
                                                                                                        "' AND SUB_KB = '9Q'"))
            Dim wBogai As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST_DETAILS).Select(String.Concat("NRS_BR_CD = '", .cmbEigyo.SelectedValue, _
                                                                                                        "' AND CUST_CD = '", .lblCustCD_L.TextValue, _
                                                                                                        "' AND SUB_KB = '9R'"))
            Dim dispW246 As Boolean = False
            '2019/12/25 要望管理006868 add end

            Dim preGoodsCdNrs As String = String.Empty

            For i As Integer = 1 To max

                If Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.DEF.ColNo)).Equals(LMConst.FLG.ON) = True Then

                    'START YANAI 要望番号776
                    '入荷完了チェック
                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '入庫日
                        If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.INKO_DATE_ZAI.ColNo))) = True Then
                            '英語化対応
                            '20151022 tsunehira add
                            'If MyBase.ShowMessage("W200", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then

                            If MyBase.ShowMessage("W251", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                Return False
                            End If
                        End If
                    End If
                    'END YANAI 要望番号776

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '引当可能個数 + 引当個数 + 出荷単位
                        If .optCnt.Checked = True Then
                            If Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_KANO_CNT.ColNo))) _
                                < Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_CNT.ColNo))) Then
                                '英語化対応
                                '20151022 tsunehira add
                                MyBase.ShowMessage("E757", New String() {i.ToString})
                                'MyBase.ShowMessage("E184", New String() {String.Concat(i, "行目")})
                                Return False
                            End If
                        End If
                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '引当可能数量 + 引当数量 + 出荷単位
                        If .optAmt.Checked = True Then
                            If Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_KANO_AMT.ColNo))) _
                                < Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo))) Then
                                '英語化対応
                                '20151022 tsunehira add
                                MyBase.ShowMessage("E758", New String() {i.ToString})
                                'MyBase.ShowMessage("E186", New String() {String.Concat(i, "行目")})
                                Return False
                            End If
                        End If
                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '引当数量 + 入目 + 出荷単位
                        If .optCnt.Checked = True OrElse .optAmt.Checked = True Then
                            If ("00").Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.SMPL_FLG.ColNo))) = True AndAlso _
                                Convert.ToDecimal(Me._Gcon.GetCellValue(Me._Frm.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo))) >= Convert.ToDecimal(Me._Gcon.GetCellValue(Me._Frm.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.IRIME.ColNo))) Then
                                '小分け在庫以外
                                If Convert.ToDecimal(Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo)))) * 1000 _
                                    Mod Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.IRIME.ColNo))) * 1000 <> 0 Then
                                    '英語化対応
                                    '20151022 tsunehira add
                                    MyBase.ShowMessage("E171", New String() {i.ToString})
                                    'MyBase.ShowMessage("E171", New String() {String.Concat(i, "行目")})
                                    Return False
                                End If

                                If Convert.ToDecimal(Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo)))) < _
                                    Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.IRIME.ColNo))) Then
                                    MyBase.ShowMessage("E455", New String() {String.Concat(i, "行目")})
                                    Return False
                                End If

                            Else
                                'If Convert.ToDecimal(Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo)))) * 1000 _
                                '    Mod Convert.ToDecimal(.numIrime.Value) * 1000 <> 0 Then
                                '    MyBase.ShowMessage("E171", New String() {String.Concat(i, "行目")})
                                '    Return False
                                'End If
                                If Convert.ToDecimal(Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo)))) < _
                                    Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.IRIME.ColNo))) Then
                                    MyBase.ShowMessage("E455", New String() {String.Concat(i, "行目")})
                                    Return False
                                End If

                            End If
                        End If

                    End If

                    If likeFlg = True Then

                        If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                            '商品キー
                            If String.IsNullOrEmpty(preGoodsCdNrs) = False AndAlso preGoodsCdNrs.Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.GOODS_CD_NRS.ColNo))) = False Then
                                '英語化対応
                                '20151022 tsunehira add
                                MyBase.ShowMessage("E765", New String() {i.ToString})
                                'MyBase.ShowMessage("E428", New String() {"選択された荷主商品が違う", "選択", String.Concat(i, "行目")})
                                Return False
                            Else
                                preGoodsCdNrs = Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.GOODS_CD_NRS.ColNo))
                            End If
                        End If

                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '保留品区分
                        If (spdOLT(0).Item("KBN_NM1").ToString()).Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.SPD_KBN_S.ColNo))) = True OrElse _
                            (spdIS(0).Item("KBN_NM1").ToString()).Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.SPD_KBN_S.ColNo))) = True Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E762", New String() {i.ToString})
                            'MyBase.ShowMessage("E188", New String() {String.Concat(i, "行目")})
                            Return False
                        End If

                        If (spdSyukka(0).Item("KBN_NM1").ToString()).Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.SPD_KBN_S.ColNo))) = False Then
                            '2015.10.22 tusnehira add
                            '英語化対応
                            'If MyBase.ShowMessage("W125", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W248", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                Return False
                            End If
                        End If
                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '商品状態区分1 + 商品状態区分2
                        If String.IsNullOrEmpty(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.NAKAMI.ColNo))) = False OrElse _
                            String.IsNullOrEmpty(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.GAIKAN.ColNo))) = False Then
                            '2019/12/25 要望管理006868 add start
                            dispW246 = True
                            '2019/12/25 要望管理006868 add end
                            '英語化対応
                            '20151022 tsunehira add
                            'If MyBase.ShowMessage("W123", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W246", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                Return False
                            End If
                        End If
                    End If

                    '2019/12/25 要望管理006868 add start
                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        If (wBogai.Length > 0) AndAlso (wBogai(0).Item("SET_NAIYO").ToString().Equals("1")) AndAlso (Not dispW246) Then
                            '簿外品区分によるチェック(簿品以外は警告)
                            If bohinNm(0).Item("KBN_NM1").ToString().Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.OFB_KBN.ColNo))) = False Then
                                dispW246 = True
                                If MyBase.ShowMessage("W246", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                    Return False
                                End If
                            End If
                        End If
                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        If (wZaiRem.Length > 0) AndAlso (wZaiRem(0).Item("SET_NAIYO").ToString().Equals("1")) Then
                            '備考小(社内,社外)によるチェック
                            Dim remark As String = Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.REMARK.ColNo))
                            If String.IsNullOrEmpty(remark) Then
                                remark = Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.REMARK_OUT.ColNo))
                            End If
                            If Not String.IsNullOrEmpty(remark) Then
                                If MyBase.ShowMessage("W294", New String() {remark, i.ToString}) = MsgBoxResult.Cancel Then
                                    Return False
                                End If
                            End If
                        End If
                    End If
                    '2019/12/25 要望管理006868 add end

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        'START YANAI 20111003 一括引当対応
                        'If String.IsNullOrEmpty(.lblConsDate.TextValue) = False AndAlso _
                        '   String.IsNullOrEmpty(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.OUTKA_PLAN_DATE.ColNo))) = False Then
                        'Dim eDate As String = Convert.ToString(DateAdd("d", Convert.ToDecimal(.lblConsDate.TextValue), _
                        '                     Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.OUTKA_PLAN_DATE.ColNo))))
                        If String.IsNullOrEmpty(.lblConsDate.TextValue) = False AndAlso _
                           String.IsNullOrEmpty(.txtOutkaPlanDate.TextValue) = False Then

                            '2016.01.18 英語化不具合対応 修正START
                            '出荷日 + 消費期限事前禁止日 を求める
                            'Dim eDate As String = Convert.ToString(DateAdd("d", Convert.ToDecimal(.lblConsDate.TextValue), _
                            '                     Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(.txtOutkaPlanDate.TextValue)))
                            'END YANAI 20111003 一括引当対応

                            Dim format As String = "yyyy/MM/dd"
                            Dim eDate As Date = DateAdd("d", Convert.ToDecimal(.lblConsDate.TextValue), Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(.txtOutkaPlanDate.TextValue))
                            Dim str As String = eDate.ToString(format)
                            str = String.Concat(Left(str, 4), Mid(str, 6, 2), Mid(str, 9, 2))

                            '出荷日 + 消費期限事前禁止日 + 入庫予定日
                            'If eDate _
                            '   < Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.INKO_DATE.ColNo))) Then
                            If str _
                               < Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.INKO_DATE.ColNo))) Then
                                '英語化対応
                                '20151022 tsunehira add
                                MyBase.ShowMessage("E763", New String() {i.ToString})
                                'MyBase.ShowMessage("E190", New String() {String.Concat(i, "行目")})
                                Return False
                            End If

                            '賞味有効期限 + 出荷日 + 消費期限事前禁止日
                            'START YANAI 要望番号727
                            'If String.IsNullOrEmpty(Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.LT_DATE.ColNo)))) = False AndAlso _
                            '    Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.LT_DATE.ColNo))) _
                            '        < eDate Then
                            'If String.IsNullOrEmpty(Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.LT_DATE.ColNo)))) = False AndAlso _
                            '    Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.LT_DATE.ColNo))).Replace("/", "") _
                            '        < eDate Then
                            If String.IsNullOrEmpty(Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.LT_DATE.ColNo)))) = False AndAlso _
                                Convert.ToString(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.LT_DATE.ColNo))).Replace("/", "") _
                                    < str Then

                                'END YANAI 要望番号727
                                '英語化対応
                                '20151022 tsunehira add
                                'If MyBase.ShowMessage("W124", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                                If MyBase.ShowMessage("W247", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                    Return False
                                End If
                            End If
                            '2016.01.18 英語化不具合対応 修正END
                        End If
                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '割当優先区分
                        If (aloRes(0).Item("KBN_NM1").ToString()).Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.ALLOC_PRIORITY.ColNo))) = True Then
                            '英語化対応
                            '20151022 tsunehira add
                            'If MyBase.ShowMessage("W126", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W249", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                Return False
                            End If
                        End If
                    End If

                    If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '予約番号
                        If String.IsNullOrEmpty(.txtRsvNO.TextValue) = False AndAlso _
                            .txtRsvNO.TextValue.Equals(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.YOYAKU_NO.ColNo))) = False Then
                            '英語化対応
                            '20151022 tsunehira add
                            'If MyBase.ShowMessage("W127", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W250", New String() {i.ToString}) = MsgBoxResult.Cancel Then
                                Me._Vcon.SetErrorControl(.txtRsvNO)
                                Return False
                            End If
                        End If
                    End If

                    kosu = kosu + Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_CNT.ColNo)))
                    suryo = suryo + Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.HIKI_AMT.ColNo)))

                End If
                '(2013.03..11)要望番号1229 小分け、サンプル時は入荷完了された商品にみ引当可 -- START --
                If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                    '出荷単位（小分け）/ (サンプル)
                    If .optKowake.Checked = True Or .optSample.Checked = True Then
                        If Convert.ToDouble(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LMC040G.sprZaiko.INKA_STATE_KB.ColNo)).ToString) < 50 Then
                            '英語化対応
                            '20151022 tsunehira add
                            MyBase.ShowMessage("E764")
                            'MyBase.ShowMessage("E375", New String() {"入荷完了していない", "「小分け」「サンプル」出荷は"})
                            Return False
                        End If
                    End If
                End If
                '(2013.03..11)要望番号1229 小分け、サンプル時は入荷完了された商品にみ引当可 --  END  --

            Next i

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '引当残個数 + 引当個数の合計
                If Convert.ToDecimal(.numHikiZanCnt.Value) < kosu Then
                    Me._Vcon.SetErrorControl(.numHikiZanCnt)
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E185", New String() {.lblHikiZanCnt.TextValue})
                    'MyBase.ShowMessage("E185", New String() {"引当残個数"})
                    Return False
                End If
            End If

            If LMC040C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '引当残数量 + 引当数量の合計
                If Convert.ToDecimal(.numHikiZanAmt.Value) < suryo Then
                    Me._Vcon.SetErrorControl(.numHikiZanAmt)
                    '英語化対応
                    '20151022 tsunehira add
                    MyBase.ShowMessage("E185", New String() {.lblHikiZanAmt.TextValue})
                    'MyBase.ShowMessage("E185", New String() {"引当残数量"})
                    Return False
                End If
            End If

        End With

        Return True

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

    'START YANAI 20110914 一括引当対応
    '''' <summary>
    '''' 入力チェック（自動引当時のチェック）
    '''' </summary>
    '''' <returns>
    '''' True ：入力エラーなし
    '''' false：入力エラーあり
    '''' </returns>
    '''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    'Friend Function IsAutoCheck(ByVal rDs As DataSet, ByVal prmDs As DataSet) As Boolean
    ''' <summary>
    ''' 入力チェック（自動引当時のチェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsAutoCheck(ByVal rDs As DataSet, ByVal prmDs As DataSet) As Boolean
        'END YANAI 20110914 一括引当対応

        '【一覧チェック】
        Dim max As Integer = rDs.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Count - 1
        Dim kanoKosu As Decimal = 0
        Dim kanoSuryo As Decimal = 0
        'SHINODA
        Dim strZaiRecNo As ArrayList = New ArrayList()
        'Dim delRows As DataTable = New DataTable()
        'SHINODA

        'SHINODA
        Dim Outka_Plan_Date As String = String.Empty
        If prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows.Count > 0 Then
            Outka_Plan_Date = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN).Rows(0).Item("OUTKA_PLAN_DATE").ToString()
        End If
        'SHINODA

        '(2013.03.21)要望番号1229 小分け、サンプル時は入荷完了された商品のみ引当可 -- START --
        'With rDs.Tables(LMC040C.TABLE_NM_OUTZAI)
        '    'START YANAI 20110914 一括引当対応
        '    Dim indt As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)
        '    Dim indr() As DataRow = Nothing
        '    'END YANAI 20110914 一括引当対応

        '    For i As Integer = 0 To max

        '        kanoKosu = kanoKosu + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_NB"))
        '        kanoSuryo = kanoSuryo + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_QT"))

        '        'START YANAI 20110914 一括引当対応
        '        indr = indt.Select(String.Concat("ZAI_REC_NO = '", .Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
        '        If 0 < indr.Length Then
        '            kanoKosu = kanoKosu - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_NB").ToString())
        '            kanoSuryo = kanoSuryo - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_QT").ToString())
        '        End If
        '        'END YANAI 20110914 一括引当対応

        '    Next i

        'End With

        Dim indt2 As DataTable = prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
        If ("03").Equals(indt2.Rows(0).Item("ALCTD_KB").ToString()) = True Or _
             ("04").Equals(indt2.Rows(0).Item("ALCTD_KB").ToString()) = True Then

            With rDs.Tables(LMC040C.TABLE_NM_OUTZAI)

                Dim indt As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)
                Dim indr() As DataRow = Nothing

                For i As Integer = 0 To max

                    If Convert.ToDouble(.Rows(i).Item("INKA_STATE_KB")) < 50 Then
                        '次の行へ
                        Continue For

                    Else
                        kanoKosu = kanoKosu + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_NB"))
                        kanoSuryo = kanoSuryo + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_QT"))

                        indr = indt.Select(String.Concat("ZAI_REC_NO = '", .Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                        If 0 < indr.Length Then
                            kanoKosu = kanoKosu - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_NB").ToString())
                            kanoSuryo = kanoSuryo - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_QT").ToString())
                        End If

                    End If

                Next i

            End With

        Else

            With rDs.Tables(LMC040C.TABLE_NM_OUTZAI)
                'START YANAI 20110914 一括引当対応
                Dim indt As DataTable = prmDs.Tables(LMC040C.TABLE_NM_IN2)
                Dim indr() As DataRow = Nothing
                'END YANAI 20110914 一括引当対応

                For i As Integer = 0 To max
                    'ADD 2020/09/16 015408   【LMS】群馬_出荷不可は引当できないようにする
                    If ("03").Equals(.Rows(i).Item("SPD_KB").ToString.Trim) = True Then
                        '次の行へ
                        Continue For

                    Else

                        'SHINODA
                        If Outka_Plan_Date.Equals(String.Empty) = False Then
                            '出荷日 + 消費期限事前禁止日 を求める
                            Dim eDate As String = Convert.ToString(DateAdd("d", Convert.ToDecimal(.Rows(i).Item("CONSUME_PERIOD_DATE")),
                                             Jp.Co.Nrs.Com.Utility.DateFormatUtility.EditSlash(Outka_Plan_Date)))

                            eDate = String.Concat(Left(eDate, 4), Mid(eDate, 6, 2), Mid(eDate, 9, 2))
                            '有効期限チェック
                            If String.IsNullOrEmpty(.Rows(i).Item("LT_DATE").ToString()) = False AndAlso
                          .Rows(i).Item("LT_DATE").ToString() < eDate Then
                                'delRows.Add(.Rows(i))
                                strZaiRecNo.Add(i)
                            Else
                                kanoKosu = kanoKosu + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_NB"))
                                kanoSuryo = kanoSuryo + Convert.ToDecimal(.Rows(i).Item("ALLOC_CAN_QT"))
                            End If
                        End If
                        'SHINODA

                        'START YANAI 20110914 一括引当対応
                        indr = indt.Select(String.Concat("ZAI_REC_NO = '", .Rows(i).Item("ZAI_REC_NO").ToString(), "'"))
                        If 0 < indr.Length Then
                            kanoKosu = kanoKosu - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_NB").ToString())
                            kanoSuryo = kanoSuryo - Convert.ToDecimal(indr(0).Item("ALLOC_CAN_QT").ToString())
                        End If
                        'END YANAI 20110914 一括引当対応

                    End If   'ADD 2020/09/16 015408   【LMS】群馬_出荷不可は引当できないようにする
                Next i

            End With

        End If
        '(2013.03.21)要望番号1229 小分け、サンプル時は入荷完了された商品のみ引当可 --  END --

        'START KIM 要望番号1479 一括引当時、引当画面の速度改善
        If Me._Frm Is Nothing Then

            With prmDs.Tables(LMControlC.LMC040C_TABLE_NM_IN)
                '引当残個数 + 引当可能個数の合計

                If kanoKosu < Me._numHikiZanCnt Then
                    'MyBase.ShowMessage("E192")
                    Return False
                End If

                '引当残数量 + 引当可能数量の合計
                If kanoSuryo < Me._numHikiZanAmt Then
                    'MyBase.ShowMessage("E192")
                    Return False
                End If
            End With
        Else
            'END KIM 要望番号1479 一括引当時、引当画面の速度改善

            With Me._Frm

                '引当残個数 + 引当可能個数の合計
                If kanoKosu < Convert.ToDecimal(.numHikiZanCnt.Value) Then
                    Me._Vcon.SetErrorControl(.numHikiZanCnt)
                    MyBase.ShowMessage("E192")
                    Return False
                End If

                '引当残数量 + 引当可能数量の合計
                If kanoSuryo < Convert.ToDecimal(.numHikiZanAmt.Value) Then
                    Me._Vcon.SetErrorControl(.numHikiZanAmt)
                    MyBase.ShowMessage("E192")
                    Return False
                End If

            End With

            'START KIM 要望番号1479 一括引当時、引当画面の速度改善
        End If
        'END KIM 要望番号1479 一括引当時、引当画面の速度改善

        'SHINODA
        Dim delcnt As Integer = 0
        For z As Integer = 0 To strZaiRecNo.Count - 1 Step 1
            'Dim delRow As DataRow = rDs.Tables(LMC040C.TABLE_NM_OUTZAI).Rows(Convert.ToInt32(strZaiRecNo.Item(z)))
            'rDs.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.Remove(delRow)
            rDs.Tables(LMC040C.TABLE_NM_OUTZAI).Rows.RemoveAt(Convert.ToInt32(strZaiRecNo.Item(z)) - delcnt)
            delcnt = delcnt + 1
        Next
        'SHINODA

        Return True

    End Function

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMC040G.sprZaiko.DEF.ColNo

        '選択された行の行番号を取得
        Return _Vcon.SprSelectList(defNo, _Frm.sprZaiko)

    End Function

    ''' <summary>
    ''' 入目の範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IrimeCheck() As Boolean

        '入目
        If Me.IsHaniCheck(Convert.ToDecimal(Me._Frm.numIrime.Value), Convert.ToDecimal(LMC040C.IRIME_MIN_NUM), Convert.ToDecimal(LMC040C.IRIME_MAX_NUM)) = False Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E014", New String() {_Frm.lblIrime.TextValue, LMC040C.IRIME_MIN_NUM, LMC040C.IRIME_MAX_NUM})
            'MyBase.ShowMessage("E014", New String() {"入目", LMC040C.IRIME_MIN_NUM, LMC040C.IRIME_MAX_NUM})
            Me._Vcon.SetErrorControl(Me._Frm.numIrime)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 出荷個数、梱数、出荷数量の範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function CalSumCheck(ByVal kosu As Decimal, ByVal souCnt As Decimal, ByVal souAmt As Decimal) As Boolean

        '出荷個数
        If kosu < 0 OrElse 9999999999 < kosu Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {_Frm.lblSyukkaSouCnt.TextValue, "0", "9999999999"})
            'MyBase.ShowMessage("E222", New String() {"出荷個数", "0", "9999999999"})
            Return False
        End If

        '梱数
        If souCnt < 0 OrElse 9999999999 < souCnt Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {_Frm.lblSyukkaKosu.TextValue, "0", "9999999999"})
            'MyBase.ShowMessage("E222", New String() {"梱数", "0", "9999999999"})
            Return False
        End If

        '出荷数量
        If souAmt < 0 OrElse 999999999.999 < souAmt Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {_Frm.lblSyukkaSouAmtl.TextValue, "0", "999999999.999"})
            'MyBase.ShowMessage("E222", New String() {"出荷数量", "0", "999999999.999"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 引当個数、引当数量の範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function CalHikiCheck(ByVal kosu As Decimal, ByVal suryo As Decimal, ByVal amari As Decimal) As Boolean

        '引当個数
        If kosu < 0 OrElse 9999999999 < kosu Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {LMC040G.sprZaiko.HIKI_CNT.ColName, "0", "9999999999"})
            'MyBase.ShowMessage("E222", New String() {"引当個数", "0", "9999999999"})
            Return False
        End If

        '引当個数
        If 0 <> amari Then
            MyBase.ShowMessage("E171", New String() {String.Empty})
            Return False
        End If

        '引当数量
        If suryo < 0 OrElse 999999999.999 < suryo Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {LMC040G.sprZaiko.HIKI_AMT.ColName, "0", "999999999.999"})
            'MyBase.ShowMessage("E222", New String() {"引当数量", "0", "999999999.999"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 引当個数合計、引当数量合計の範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function CalHikiGokeiCheck(ByVal kosu As Decimal, ByVal suryo As Decimal) As Boolean

        '引当個数合計
        If kosu < 0 OrElse 9999999999 < kosu Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {_Frm.lblHikiCntSum.TextValue, "0", "9999999999"})
            'MyBase.ShowMessage("E222", New String() {"引当個数合計", "0", "9999999999"})
            Return False
        End If

        '引当数量合計
        If suryo < 0 OrElse 999999999.999 < suryo Then
            '英語化対応
            '20151022 tsunehira add
            MyBase.ShowMessage("E222", New String() {_Frm.lblHikiAmtSum.TextValue, "0", "999999999.999"})
            'MyBase.ShowMessage("E222", New String() {"引当数量合計", "0", "999999999.999"})
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 振替対象商品マスタ存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function IsFuriGoodsffExistChk(ByVal value As String) As Boolean

        With Me._Frm

            '振替対象商品マスタの存在チェック
            Dim drs As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.FURI_GOODS).Select(String.Concat("CD_NRS = '", value, "'"))
            If drs.Length < 1 Then
                Return False
            End If

            Return True

        End With

    End Function

    ''' <summary>
    ''' 引当個数・数量ALLゼロチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAllZeroChk() As Boolean

        With Me._Frm

            'START YANAI 20110906 サンプル対応
            ''サンプル選択時は対象外
            'If .optSample.Checked = True Then
            '    Return False
            'End If
            'END YANAI 20110906 サンプル対応

            '出荷個数・数量が0の時は対象外
            If Convert.ToDecimal(.numSyukkaSouCnt.Value) = 0 AndAlso Convert.ToDecimal(.numSyukkaSouAmt.Value) = 0 Then
                Return False
            End If

            'チェックリスト格納変数
            Dim list As ArrayList = New ArrayList()
            list = Me.getCheckList()
            Dim max As Integer = list.Count() - 1

            For i As Integer = 0 To max
                If (Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMC040G.sprZaiko.HIKI_CNT.ColNo)).ToString())) <> 0 OrElse _
                    (Convert.ToDecimal(Me._Gcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMC040G.sprZaiko.HIKI_AMT.ColNo)).ToString())) <> 0 Then
                    Return False
                End If
            Next

            Return True

        End With

    End Function

    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMC040C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMC040C.EventShubetsu.TANINUSI     '他荷主
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

            Case LMC040C.EventShubetsu.KENSAKU      '検索
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

            Case LMC040C.EventShubetsu.SENTAKU      '選択
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

            Case LMC040C.EventShubetsu.CLOSE           '閉じる
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
    ''' 項目のTrim処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub valueTrim()

        With Me._Frm
            '各項目のTrim処理
            .txtSerialNO.TextValue = Trim(.txtSerialNO.TextValue)
            .txtRsvNO.TextValue = Trim(.txtRsvNO.TextValue)
            .txtLotNO.TextValue = Trim(.txtLotNO.TextValue)
            .numIrime.TextValue = Trim(.numIrime.TextValue)
            .numSyukkaKosu.TextValue = Trim(.numSyukkaKosu.TextValue)
            .numSyukkaHasu.TextValue = Trim(.numSyukkaHasu.TextValue)
            .numSyukkaSouAmt.TextValue = Trim(.numSyukkaSouAmt.TextValue)

            'スプレッドのスペース除去
            Call Me._Vcon.TrimSpaceSprTextvalue(Me._Frm.sprZaiko, 0)

        End With

    End Sub

#End Region 'Method

End Class
