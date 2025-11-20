' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LME     : 作業
'  プログラムID     :  LME050  : 作業個数引当
'  作  成  者       :  矢内
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LME050Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LME050V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LME050F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LME050F, ByVal v As LMEControlV)

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
    ''' 入力チェック（単項目チェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LME050C.EventShubetsu) As Boolean

        With Me._Frm

            Call Me.valueTrim()

            Select Case eventShubetsu

                Case LME050C.EventShubetsu.KENSAKU, LME050C.EventShubetsu.SENTAKU

                    '入目
                    If Me.IsHaniCheck(Convert.ToDecimal(Me._Frm.numIrime.Value), Convert.ToDecimal(LME050C.IRIME_MIN_NUM), Convert.ToDecimal(LME050C.IRIME_MAX_NUM)) = False Then
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E014", New String() {"入目", LME050C.IRIME_MIN_NUM, LME050C.IRIME_MAX_NUM})
                        MyBase.ShowMessage("E014", New String() {.lblIrime.Text(), LME050C.IRIME_MIN_NUM, LME050C.IRIME_MAX_NUM})
                        '2016.01.06 UMANO 英語化対応END
                        Me._Vcon.SetErrorControl(Me._Frm.numIrime)
                        Return False
                    End If

            End Select

            Dim max As Integer = .sprZaiko.ActiveSheet.Rows.Count - 1

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                For i As Integer = 1 To max

                    If Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.DEF.ColNo)).Equals(LMConst.FLG.ON) = True Then
                        '営業所
                        If (.cmbEigyo.SelectedValue).Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.NRS_BR_CD.ColNo))) = False Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E221", New String() {"営業所"})
                            MyBase.ShowMessage("E221", New String() {.lblEigyo.Text()})
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If

                        '倉庫
                        If (.cmbSoko.SelectedValue).Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.WH_CD.ColNo))) = False Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E221", New String() {"倉庫"})
                            MyBase.ShowMessage("E221", New String() {.lblSoko.Text()})
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If
                    End If

                Next i
            End If

            If LME050C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                'シリアル№
                .txtSerialNO.TextValue = Trim(.txtSerialNO.TextValue)
                .txtSerialNO.ItemName() = .lblSerialNO.Text
                .txtSerialNO.IsForbiddenWordsCheck() = True
                .txtSerialNO.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtSerialNO) = False Then
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '予約番号
                .txtRsvNO.TextValue = Trim(.txtRsvNO.TextValue)
                .txtRsvNO.ItemName() = .lblReserveNO.Text
                .txtRsvNO.IsForbiddenWordsCheck() = True
                .txtRsvNO.IsByteCheck() = 10
                If MyBase.IsValidateCheck(.txtRsvNO) = False Then
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                'ロット番号
                .txtLotNO.TextValue = Trim(.txtLotNO.TextValue)
                .txtLotNO.ItemName() = .lblLotNO.Text
                .txtLotNO.IsForbiddenWordsCheck() = True
                .txtLotNO.IsByteCheck() = 40
                If MyBase.IsValidateCheck(.txtLotNO) = False Then
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '梱数
                .numSyukkaKosu.TextValue = Trim(.numSyukkaKosu.TextValue)
                .numSyukkaKosu.ItemName() = .lblSyukkaKosu.Text
                If MyBase.IsValidateCheck(.numSyukkaKosu) = False Then
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '端数
                .numSyukkaHasu.TextValue = Trim(.numSyukkaHasu.TextValue)
                .numSyukkaHasu.ItemName() = .lblSyukkaHasu.Text
                If MyBase.IsValidateCheck(.numSyukkaHasu) = False Then
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '作業数量
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

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
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
    Private Function IsSprChk(ByVal eventShubetsu As LME050C.EventShubetsu) As Boolean

        '検索処理以外はスルー
        If LME050C.EventShubetsu.KENSAKU <> eventShubetsu Then
            Return True
        End If

        Dim chkFlg As Boolean = True
        Dim errorFlg As Boolean = False

        Dim vCell As LMValidatableCells = New LMValidatableCells(Me._Frm.sprZaiko)

        '棟
        vCell.SetValidateCell(0, LME050G.sprZaiko.TOU_NO.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.TOU_NO.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 2
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '室
        vCell.SetValidateCell(0, LME050G.sprZaiko.SHITSU_NO.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.SHITSU_NO.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 2
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        'ZONE
        vCell.SetValidateCell(0, LME050G.sprZaiko.ZONE_CD.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.ZONE_CD.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 2
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        'START YANAI 要望番号1090 指摘修正
        'ロケーション
        vCell.SetValidateCell(0, LME050G.sprZaiko.LOCA.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.LOCA.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 10
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If
        'END YANAI 要望番号1090 指摘修正

        '備考小（社内）
        vCell.SetValidateCell(0, LME050G.sprZaiko.REMARK.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.REMARK.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 100
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '備考小（社外）
        vCell.SetValidateCell(0, LME050G.sprZaiko.REMARK_OUT.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.REMARK_OUT.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 15
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        '届先名
        vCell.SetValidateCell(0, LME050G.sprZaiko.DEST_NM.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.DEST_NM.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 80
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If

        'START YANAI 要望番号1090 指摘修正
        '在庫レコード番号
        vCell.SetValidateCell(0, LME050G.sprZaiko.ZAI_REC_NO.ColNo)
        vCell.ItemName = Me.SetRepMsgData(LME050G.sprZaiko.ZAI_REC_NO.ColName)
        vCell.IsForbiddenWordsCheck() = chkFlg
        vCell.IsByteCheck = 15
        If MyBase.IsValidateCheck(vCell) = errorFlg Then
            Return errorFlg
        End If
        'END YANAI 要望番号1090 指摘修正

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
    Friend Function IsKanrenCheck(Optional ByVal eventShubetsu As LME050C.EventShubetsu = LME050C.EventShubetsu.SYOKI) As Boolean

        With Me._Frm

            If (LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True OrElse _
                LME050C.EventShubetsu.CAL_SURYO.Equals(eventShubetsu) = True)  Then
                '作業数量 + 入目
                Dim sKosu As Decimal = 0
                sKosu = Convert.ToDecimal(.numSyukkaSouAmt.Value) _
                            Mod (Convert.ToDecimal(.numIrime.Value))
                If sKosu <> 0 Then
                    .numSyukkaSouAmt.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    Me._Vcon.SetErrorControl(.numIrime)
                    MyBase.ShowMessage("E489", New String() {String.Empty})
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '梱数 + 端数
                If ("0").Equals(.numSyukkaKosu.TextValue) = True AndAlso ("0").Equals(.numSyukkaHasu.TextValue) = True Then
                    If Me._Frm.numSyukkaHasu.ReadOnly = False Then
                        .numSyukkaHasu.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                    End If
                    Me._Vcon.SetErrorControl(.numSyukkaKosu)
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E270", New String() {"梱数", "端数"})
                    MyBase.ShowMessage("E270", New String() {.lblSyukkaKosu.Text(), .lblSyukkaHasu.Text()})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '作業個数 + 引当個数合計
                If Convert.ToDecimal(.numSyukkaSouCnt.Value) <> Convert.ToDecimal(.numHikiCntSum.Value) Then
                    '2016.01.06 UMANO 英語化対応START
                    'If MyBase.ShowMessage("W121", New String() {"作業個数", "引当個数合計"}) = MsgBoxResult.Cancel Then
                    If MyBase.ShowMessage("W121", New String() {.lblSyukkaSouCnt.Text(), .lblHikiCntSum.Text()}) = MsgBoxResult.Cancel Then
                        '2016.01.06 UMANO 英語化対応END
                        .numSyukkaSouCnt.BackColor = Utility.LMGUIUtility.GetAttentionBackColor()
                        Me._Vcon.SetErrorControl(.numHikiCntSum)
                        Return False
                    End If
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '作業数量 + 引当数量合計
                If Convert.ToDecimal(.numSyukkaSouAmt.Value) <> Convert.ToDecimal(.numHikiAmtSum.Value) Then
                    '2016.01.06 UMANO 英語化対応START
                    'If MyBase.ShowMessage("W121", New String() {"作業数量", "引当数量合計"}) = MsgBoxResult.Cancel Then
                    If MyBase.ShowMessage("W121", New String() {.lblSyukkaSouAmtl.Text(), .lblHikiAmtSum.Text()}) = MsgBoxResult.Cancel Then
                        '2016.01.06 UMANO 英語化対応END
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

            For i As Integer = 1 To max

                If Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.DEF.ColNo)).Equals(LMConst.FLG.ON) = True Then

                    '入荷完了チェック
                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '入庫日
                        If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.INKO_DATE_ZAI.ColNo))) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'If MyBase.ShowMessage("W200", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W251", New String() {Convert.ToString(i)}) = MsgBoxResult.Cancel Then
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        End If
                    End If

                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '引当可能個数 + 引当個数
                        If Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.HIKI_KANO_CNT.ColNo))) _
                                < Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.HIKI_CNT.ColNo))) Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E184", New String() {String.Concat(i, "行目")})
                            MyBase.ShowMessage("E757", New String() {Convert.ToString(i)})
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If
                    End If

                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '引当数量 + 入目 
                        If ("00").Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.SMPL_FLG.ColNo))) = True AndAlso _
                                Convert.ToDecimal(Me._Vcon.GetCellValue(Me._Frm.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.HIKI_AMT.ColNo))) >= Convert.ToDecimal(Me._Vcon.GetCellValue(Me._Frm.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.IRIME.ColNo))) Then

                            If Convert.ToDecimal(Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.HIKI_AMT.ColNo)))) * 1000 _
                                Mod Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.IRIME.ColNo))) * 1000 <> 0 Then
                                '2016.01.06 UMANO 英語化対応START
                                'MyBase.ShowMessage("E171", New String() {String.Concat(i, "行目")})
                                MyBase.ShowMessage("E171", New String() {Convert.ToString(i)})
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        End If
                    End If

                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '保留品区分
                        If (spdOLT(0).Item("KBN_NM1").ToString()).Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.SPD_KBN_S.ColNo))) = True OrElse _
                            (spdIS(0).Item("KBN_NM1").ToString()).Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.SPD_KBN_S.ColNo))) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E188", New String() {String.Concat(i, "行目")})
                            MyBase.ShowMessage("E762", New String() {Convert.ToString(i)})
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If

                        If (spdSyukka(0).Item("KBN_NM1").ToString()).Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.SPD_KBN_S.ColNo))) = False Then
                            '2016.01.06 UMANO 英語化対応START
                            'If MyBase.ShowMessage("W125", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W248", New String() {Convert.ToString(i)}) = MsgBoxResult.Cancel Then
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        End If
                    End If

                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '商品状態区分1 + 商品状態区分2
                        If String.IsNullOrEmpty(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.NAKAMI.ColNo))) = False OrElse _
                            String.IsNullOrEmpty(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.GAIKAN.ColNo))) = False Then
                            '2016.01.06 UMANO 英語化対応START
                            'If MyBase.ShowMessage("W123", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W246", New String() {Convert.ToString(i)}) = MsgBoxResult.Cancel Then
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        End If
                    End If

                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '割当優先区分
                        If (aloRes(0).Item("KBN_NM1").ToString()).Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.ALLOC_PRIORITY.ColNo))) = True Then
                            '2016.01.06 UMANO 英語化対応START
                            'If MyBase.ShowMessage("W126", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W249", New String() {Convert.ToString(i)}) = MsgBoxResult.Cancel Then
                                '2016.01.06 UMANO 英語化対応END
                                Return False
                            End If
                        End If
                    End If

                    If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                        '予約番号
                        If String.IsNullOrEmpty(.txtRsvNO.TextValue) = False AndAlso _
                            .txtRsvNO.TextValue.Equals(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.YOYAKU_NO.ColNo))) = False Then
                            '2016.01.06 UMANO 英語化対応START
                            'If MyBase.ShowMessage("W127", New String() {String.Concat(i, "行目")}) = MsgBoxResult.Cancel Then
                            If MyBase.ShowMessage("W250", New String() {Convert.ToString(i)}) = MsgBoxResult.Cancel Then
                                '2016.01.06 UMANO 英語化対応END
                                Me._Vcon.SetErrorControl(.txtRsvNO)
                                Return False
                            End If
                        End If
                    End If

                    kosu = kosu + Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.HIKI_CNT.ColNo)))
                    suryo = suryo + Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(i, LME050G.sprZaiko.HIKI_AMT.ColNo)))

                End If

            Next i

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '引当残個数 + 引当個数の合計
                If Convert.ToDecimal(.numHikiZanCnt.Value) < kosu Then
                    Me._Vcon.SetErrorControl(.numHikiZanCnt)
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E185", New String() {"引当残個数"})
                    MyBase.ShowMessage("E185", New String() {.lblHikiSumiCnt.Text()})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
            End If

            If LME050C.EventShubetsu.SENTAKU.Equals(eventShubetsu) = True Then
                '引当残数量 + 引当数量の合計
                If Convert.ToDecimal(.numHikiZanAmt.Value) < suryo Then
                    Me._Vcon.SetErrorControl(.numHikiZanAmt)
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E185", New String() {"引当残数量"})
                    MyBase.ShowMessage("E185", New String() {.lblHikiSumiAmt.Text()})
                    '2016.01.06 UMANO 英語化対応END
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

    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LME050C.SprZaikoColumnIndex.DEF

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
        If Me.IsHaniCheck(Convert.ToDecimal(Me._Frm.numIrime.Value), Convert.ToDecimal(LME050C.IRIME_MIN_NUM), Convert.ToDecimal(LME050C.IRIME_MAX_NUM)) = False Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E014", New String() {"入目", LME050C.IRIME_MIN_NUM, LME050C.IRIME_MAX_NUM})
            MyBase.ShowMessage(Me._Frm.lblIrime.Text(), New String() {"入目", LME050C.IRIME_MIN_NUM, LME050C.IRIME_MAX_NUM})
            '2016.01.06 UMANO 英語化対応END
            Me._Vcon.SetErrorControl(Me._Frm.numIrime)
            Return False
        End If

        Return True

    End Function

    ''' <summary>
    ''' 作業個数、梱数、作業数量の範囲チェック
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function CalSumCheck(ByVal kosu As Decimal, ByVal souCnt As Decimal, ByVal souAmt As Decimal) As Boolean

        '作業個数
        If kosu < 0 OrElse 9999999999 < kosu Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"作業個数", "0", "9999999999"})
            MyBase.ShowMessage("E222", New String() {Me._Frm.lblSyukkaSouCnt.Text(), "0", "9999999999"})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        '梱数
        If souCnt < 0 OrElse 9999999999 < souCnt Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"梱数", "0", "9999999999"})
            MyBase.ShowMessage("E222", New String() {Me._Frm.lblSyukkaKosu.Text(), "0", "9999999999"})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        '作業数量
        If souAmt < 0 OrElse 999999999.999 < souAmt Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"作業数量", "0", "999999999.999"})
            MyBase.ShowMessage("E222", New String() {Me._Frm.lblSyukkaSouAmtl.Text(), "0", "999999999.999"})
            '2016.01.06 UMANO 英語化対応END
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
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"引当個数", "0", "9999999999"})
            MyBase.ShowMessage("E222", New String() {LME050G.sprZaiko.HIKI_CNT.ColName, "0", "9999999999"})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        '引当個数
        If 0 <> amari Then
            MyBase.ShowMessage("E171", New String() {String.Empty})
            Return False
        End If

        '引当数量
        If suryo < 0 OrElse 999999999.999 < suryo Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"引当数量", "0", "999999999.999"})
            MyBase.ShowMessage("E222", New String() {LME050G.sprZaiko.HIKI_AMT.ColName, "0", "999999999.999"})
            '2016.01.06 UMANO 英語化対応END
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
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"引当個数合計", "0", "9999999999"})
            MyBase.ShowMessage("E222", New String() {Me._Frm.lblHikiCntSum.Text(), "0", "9999999999"})
            '2016.01.06 UMANO 英語化対応END
            Return False
        End If

        '引当数量合計
        If suryo < 0 OrElse 999999999.999 < suryo Then
            '2016.01.06 UMANO 英語化対応START
            'MyBase.ShowMessage("E222", New String() {"引当数量合計", "0", "999999999.999"})
            MyBase.ShowMessage("E222", New String() {Me._Frm.lblHikiAmtSum.Text(), "0", "999999999.999"})
            '2016.01.06 UMANO 英語化対応END
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

            '作業個数・数量が0の時は対象外
            If Convert.ToDecimal(.numSyukkaSouCnt.Value) = 0 AndAlso Convert.ToDecimal(.numSyukkaSouAmt.Value) = 0 Then
                Return False
            End If

            'チェックリスト格納変数
            Dim list As ArrayList = New ArrayList()
            list = Me.getCheckList()
            Dim max As Integer = list.Count() - 1

            For i As Integer = 0 To max
                If (Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LME050G.sprZaiko.HIKI_CNT.ColNo)).ToString())) <> 0 OrElse _
                    (Convert.ToDecimal(Me._Vcon.GetCellValue(.sprZaiko.ActiveSheet.Cells(Convert.ToInt32(list(i)), LME050G.sprZaiko.HIKI_AMT.ColNo)).ToString())) <> 0 Then
                    Return False
                End If
            Next

            Return True

        End With

    End Function

    Friend Function IsAuthorityChk(ByVal eventShubetsu As LME050C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LME050C.EventShubetsu.KENSAKU      '検索
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

            Case LME050C.EventShubetsu.SENTAKU      '選択
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

            Case LME050C.EventShubetsu.CLOSE           '閉じる
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
