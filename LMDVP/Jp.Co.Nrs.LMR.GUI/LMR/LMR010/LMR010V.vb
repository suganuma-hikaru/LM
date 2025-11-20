' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMR010Validateクラス
''' </summary>
''' <remarks>入力チェックを行う</remarks>
''' <histry>
''' </histry>
Public Class LMR010V
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIValidate

#Region "Field"

    ''' <summary>
    ''' このValidateクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMR010F

    ''' <summary>
    ''' G共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMRconG As LMRControlG

    ''' <summary>
    ''' Validate共通クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMRconV As LMRControlV

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">このクラスがチェックするコントロールを持つフォームクラス</param>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMR010F, ByVal v As LMRControlV, ByVal gCon As LMRControlG, ByVal g As LMR010G)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        'Gamen共通クラスの設定
        Me._LMRconG = New LMRControlG(DirectCast(frm, Form))

        'Validate共通クラスの設定
        Me._LMRconV = New LMRControlV(handlerClass, DirectCast(frm, Form))

    End Sub

#End Region 'Constructor

#Region "Method"

    ''' <summary>
    ''' 単項目入力チェック。
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsSingleCheck(ByVal eventShubetsu As LMR010C.EventShubetsu, ByVal ds As DataSet, ByVal Syubetsu As String) As Boolean

        Dim arr As ArrayList = Nothing

        'スペース除去
        Call Me.TrimSpaceTextValue()

        '【単項目チェック】
        With Me._Frm

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '完了種別
                .cmbKanryo.ItemName() = .lblKanryo.TextValue
                .cmbKanryo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.cmbKanryo) = False Then
                    Me._LMRconV.SetErrorControl(.cmbKanryo)
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LMR010C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '営業所
                .CmbEigyo.ItemName() = .lblEigyo.TextValue
                .CmbEigyo.IsHissuCheck() = True
                If MyBase.IsValidateCheck(.CmbEigyo) = False Then
                    Me._LMRconV.SetErrorControl(.CmbEigyo)
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '担当者コード
                .txtTantoCD.ItemName() = .lblTanto.TextValue
                .txtTantoCD.IsForbiddenWordsCheck() = True
                .txtTantoCD.IsByteCheck() = 5
                If MyBase.IsValidateCheck(.txtTantoCD) = False Then
                    Me._LMRconV.SetErrorControl(.txtTantoCD)
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True OrElse _
                LMR010C.EventShubetsu.MASTER.Equals(eventShubetsu) = True Then
                '荷主コード
                .txtCustCD.ItemName() = .lblCust.TextValue
                .txtCustCD.IsForbiddenWordsCheck() = True
                If MyBase.IsValidateCheck(.txtCustCD) = False Then
                    Me._LMRconV.SetErrorControl(.txtCustCD)
                    Return False
                End If
            End If
            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '荷主コード
                .txtCustCD.ItemName() = .lblCust.TextValue
                .txtCustCD.IsByteCheck() = 5
                If MyBase.IsValidateCheck(.txtCustCD) = False Then
                    Me._LMRconV.SetErrorControl(.txtCustCD)
                    Return False
                End If
            End If

            '******************** スプレッド項目の入力チェック ********************
            Dim vCell As LMValidatableCells = New LMValidatableCells(.sprKanryo)

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '管理番号(大)
                vCell.SetValidateCell(0, LMR010G.sprKanryoDef.KANRI_NO_L.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "管理番号(大)"
                vCell.ItemName() = LMR010G.sprKanryoDef.KANRI_NO_L.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 9
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                'オーダー番号
                vCell.SetValidateCell(0, LMR010G.sprKanryoDef.ORDER_NO.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "オーダー番号"
                vCell.ItemName() = LMR010G.sprKanryoDef.ORDER_NO.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 30
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '担当者
                vCell.SetValidateCell(0, LMR010G.sprKanryoDef.TANTO_USER.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "担当者"
                vCell.ItemName() = LMR010G.sprKanryoDef.TANTO_USER.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 20
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '荷主名
                vCell.SetValidateCell(0, LMR010G.sprKanryoDef.CUST_NM.ColNo)
                '2016.01.06 UMANO 英語化対応START
                vCell.ItemName() = "荷主名"
                vCell.ItemName() = LMR010G.sprKanryoDef.CUST_NM.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 122
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                '届先名
                vCell.SetValidateCell(0, LMR010G.sprKanryoDef.DEST_NM.ColNo)
                '2016.01.06 UMANO 英語化対応START
                'vCell.ItemName() = "届先名"
                vCell.ItemName() = LMR010G.sprKanryoDef.DEST_NM.ColName
                '2016.01.06 UMANO 英語化対応END
                vCell.IsForbiddenWordsCheck() = True
                vCell.IsByteCheck() = 80
                If MyBase.IsValidateCheck(vCell) = False Then
                    Return False
                End If
            End If

            If LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True Then
                'スプレッド
                Dim list As ArrayList = New ArrayList()
                list = Me._LMRconV.GetCheckList(.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)
                If 0 = list.Count Then
                    .sprKanryo.Focus()
                    MyBase.ShowMessage("E009")
                    Return False
                End If

            End If

        End With

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
    Friend Function IsKanrenCheck(ByVal eventShubetsu As LMR010C.EventShubetsu, _
                                  ByVal ds As DataSet, _
                                  ByVal Syubetsu As String, _
                                  ByVal Eigyosyo As String) As Boolean

        Dim dr As DataRow = Nothing
        Dim max As Integer = 0

        '【関連項目チェック】
        With Me._Frm

            'If (LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True AndAlso _
            '    Convert.ToString(.CmbEigyo.SelectedValue) <> LMUserInfoManager.GetNrsBrCd().ToString()) OrElse _
            '    (LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True AndAlso _
            '     Eigyosyo <> LMUserInfoManager.GetNrsBrCd().ToString()) Then

            If (LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True AndAlso _
                 Eigyosyo <> LMUserInfoManager.GetNrsBrCd().ToString()) Then
                '営業所＋自営業所
                Dim msgstr As String = String.Empty
                Select Case Syubetsu
                    Case "01"   '入荷受付
                        msgstr = "入荷受付"
                    Case "02"   '入荷検品
                        msgstr = "入荷検品"
                    Case "03"   '入庫完了
                        msgstr = "入庫完了"
                    Case "04"   '出庫完了
                        msgstr = "出庫完了"
                    Case "05"   '出庫検品
                        msgstr = "出庫検品"
                    Case "06"   '出荷完了
                        msgstr = "出荷完了"
                End Select

                '2016.01.06 UMANO 英語化対応START
                msgstr = .cmbKanryo.SelectedText()
                '2016.01.06 UMANO 英語化対応END

                MyBase.ShowMessage("E178", New String() {msgstr})
                'If (LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu)) = True Then
                '    Me._LMRconV.SetErrorControl(.CmbEigyo)
                'End If
                Return False
            End If

            If LMR010C.EventShubetsu.KENSAKU.Equals(eventShubetsu) = True Then
                If String.IsNullOrEmpty(.imdNyukaDate_To.TextValue) = False AndAlso _
                    String.IsNullOrEmpty(.imdNyukaDate_To.TextValue) = False Then
                    '入荷日From + 入荷日To
                    If .imdNyukaDate_To.TextValue < .imdNyukaDate_From.TextValue Then
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E039", New String() {"入出荷日To", "入出荷日From"})
                        MyBase.ShowMessage("E039", New String() {String.Concat(.lblNyukaDate.Text(), "To"), String.Concat(.lblNyukaDate.Text(), "From")})
                        '2016.01.06 UMANO 英語化対応END
                        Me._LMRconV.SetErrorControl(.imdNyukaDate_From)
                        Me._LMRconV.SetErrorControl(.imdNyukaDate_To)
                        Return False
                    End If
                End If
            End If

            If LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True AndAlso _
                ("03").Equals(Syubetsu) = True Then
                '棟・室・ゾーン未入力チェック
                max = ds.Tables("LMR010INOUT").Rows.Count - 1
                For i As Integer = 0 To max
                    dr = ds.Tables("LMR010INOUT").Rows(i)
                    'START YANAI 要望番号433
                    'If (LMConst.FLG.ON).Equals(dr.Item("SYORI_FLG")) = True AndAlso _
                    '    ("01").Equals(dr.Item("LOC_MANAGER_YN")) = True AndAlso _
                    '    ("0").Equals(dr.Item("OKIBA_COUNT").ToString) = False Then
                    If (LMConst.FLG.ON).Equals(dr.Item("SYORI_FLG")) = True AndAlso _
                        ((("01").Equals(dr.Item("LOC_MANAGER_YN")) = True AndAlso _
                          ("0").Equals(dr.Item("OKIBA_COUNT").ToString) = False) OrElse _
                         (("02").Equals(dr.Item("LOC_MANAGER_YN")) = True) AndAlso _
                          ("0").Equals(dr.Item("OKIBA_COUNT2").ToString) = False) Then
                        'END YANAI 要望番号433
                        '2016.01.06 UMANO 英語化対応START
                        'MyBase.ShowMessage("E329", New String() {String.Concat("入荷管理番号=", dr.Item("INOUTKA_NO_L").ToString, "]")})
                        MyBase.ShowMessage("E329", New String() {String.Concat(.sprKanryo.ActiveSheet.GetColumnLabel(0, LMR010C.sprKanryoColumnIndex.KANRI_NO), dr.Item("INOUTKA_NO_L").ToString, "]")})
                        '2016.01.06 UMANO 英語化対応END
                        Return False
                    End If
                Next
            End If

            If LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True AndAlso _
                ("04").Equals(Syubetsu) = True Then
                '荷札フラグ
                max = ds.Tables("LMR010INOUT").Rows.Count - 1
                For i As Integer = 0 To max
                    dr = ds.Tables("LMR010INOUT").Rows(i)
                    If (LMConst.FLG.ON).Equals(dr.Item("SYORI_FLG")) = True AndAlso _
                        ("00").Equals(dr.Item("NIHUDA_FLAG").ToString) = True Then
                        '2016.01.06 UMANO 英語化対応START
                        'If MyBase.ShowMessage("W119", New String() {"荷札", String.Concat("[出荷管理番号=", dr.Item("INOUTKA_NO_L").ToString, "]")}) = MsgBoxResult.Cancel Then
                        If MyBase.ShowMessage("W119", New String() {dr.Item("INOUTKA_NO_L").ToString}) = MsgBoxResult.Cancel Then
                            '2016.01.06 UMANO 英語化対応END
                            dr.Item("SYORI_FLG") = LMConst.FLG.OFF
                        End If
                    End If
                Next
            End If

            If LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True AndAlso _
                ("03").Equals(Syubetsu) = True Then
                '入荷日 + 保管料最終計算日
                'チェックリスト格納変数
                Dim list As ArrayList = New ArrayList()
                'チェックリスト取得
                list = Me._LMRconV.GetCheckList(.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)
                max = list.Count - 1
                Dim inkaDate As String = String.Empty
                Dim inkaNoL As String = String.Empty
                Dim custRow() As DataRow = Nothing
                Dim custMax As Integer = 0

                'START YANAI メモNo.92
                Dim outDr() As DataRow = Nothing
                'END YANAI メモNo.92

                For i As Integer = 0 To max

                    inkaNoL = Me._LMRconG.GetCellValue(.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.KANRI_NO))

                    'START YANAI メモNo.92
                    outDr = ds.Tables(LMR010C.TABLE_NM_INOUT).Select(String.Concat("INOUTKA_NO_L = '", inkaNoL, "'"))
                    If 0 < outDr.Length Then
                        If String.IsNullOrEmpty(outDr(0).Item("HOKAN_STR_DATE").ToString()) = False Then
                            '起算日が設定されていたら、チェック対象外
                            Continue For
                        End If
                    End If
                    'END YANAI メモNo.92

                    inkaDate = Me._LMRconG.GetCellValue(.sprKanryo.ActiveSheet.Cells(Convert.ToInt32(list(i)), LMR010C.sprKanryoColumnIndex.PLAN_DATE)).Replace("/", "")

                    custRow = ds.Tables(LMR010C.TABLE_NM_CUST).Select(String.Concat("INKA_NO_L = '", inkaNoL, "'"))
                    custMax = custRow.Length - 1

                    For j As Integer = 0 To custMax
                        If custRow(j).Item("HOKAN_NIYAKU_CALCULATION").ToString >= inkaDate Then
                            '2016.01.06 UMANO 英語化対応START
                            'MyBase.ShowMessage("E409", New String() {"保管・荷役料最終計算日", "入荷日", "入荷完了", String.Concat("[入荷管理番号 = ", inkaNoL, "]")})
                            MyBase.ShowMessage("E409", New String() {String.Concat("[入荷管理番号 = ", inkaNoL, "]")})
                            '2016.01.06 UMANO 英語化対応END
                            Return False
                        End If
                    Next
                Next
            End If

            If LMR010C.EventShubetsu.KANRYO.Equals(eventShubetsu) = True AndAlso _
               LMR010C.KANRYO_03.Equals(Syubetsu) = True Then

                Dim checkedIdxList As ArrayList _
                    = Me._LMRconV.GetCheckList(.sprKanryo.ActiveSheet, LMR010C.sprKanryoColumnIndex.DEF)

                For Each index As Integer _
                    In Me._LMRconV.GetCheckList(.sprKanryo.ActiveSheet _
                                               , LMR010C.sprKanryoColumnIndex.DEF)
                    Dim inkaNOL As String = _
                        Me._LMRconG.GetCellValue(.sprKanryo.ActiveSheet.Cells(index _
                                                                            , LMR010C.sprKanryoColumnIndex.KANRI_NO))

                    Dim inOutRow As DataRow _
                        = ds.Tables(LMR010C.TABLE_NM_INOUT).AsEnumerable() _
                            .Where(Function(r) inkaNOL.Equals(r.Item("INOUTKA_NO_L"))).FirstOrDefault

                    If (inOutRow IsNot Nothing) Then

                        If LMR010C.WH_KENPIN_WK_STATUS_INKA _
                                .INSPECTED.Equals(inOutRow.Item("WH_KENPIN_WK_STATUS")) = False Then

                            If (String.IsNullOrEmpty(inkaNOL) = False) Then
                                For Each zaiRow As DataRow In ds.Tables(LMR010C.TABLE_NM_ZAI).AsEnumerable() _
                                     .Where(Function(row) inkaNOL.Equals(row.Item("INOUTKA_NO_L")))

                                    ' 検品済の確認は、対象となる在庫に対して行う。
                                    If (LMConst.FLG.ON.Equals(zaiRow.Item("CHK_KENPIN_WK_ON"))) Then

                                        MyBase.ShowMessage("E942", New String() {String.Format("({0})", inkaNOL)})

                                        Return False

                                    End If
                                Next
                            End If
                        End If
                    End If
                Next
            End If

        End With

        Return True

    End Function

    ''' <summary>
    ''' 入荷(小)データチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsInkaSChk(ByVal eventShubetsu As LMR010C.EventShubetsu, ByVal ds As DataSet, ByVal Syubetsu As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMR010_OUT_CHECK")
        If dt.Rows.Count = 0 Then
            Return True
        End If

        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow = Nothing
        For i As Integer = 0 To max
            dr = dt.Rows(i)

            '在庫レコードチェック(在庫レコードが空の場合)
            If String.IsNullOrEmpty(dr.Item("ZAI_REC_NO").ToString) = True Then
                MyBase.ShowMessage("E111", New String() {String.Concat("[入荷管理番号=", dr.Item("INKA_NO_L").ToString, "]")})
                Return False
            End If

            'ロット管理レベルが"00"(なし)の場合、スルー
            If ("00").Equals(dr.Item("LOT_CTL_KB").ToString) = True Then
                Continue For
            End If

            '必須チェック(ロット№が空の場合)
            If String.IsNullOrEmpty(dr.Item("LOT_NO").ToString) = True Then
                MyBase.ShowMessage("E113", New String() {String.Concat("[入荷管理番号=", dr.Item("INKA_NO_L").ToString, "]")})
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 入荷 TSMCシステム個数未達チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function IsInkaTsmcQtyChk() As Boolean

        Dim chkList As ArrayList = Me.getCheckList()
        Dim selectRow As Integer = 0
        Dim sTsmcQtySumi As String
        Dim sTsmcQty As String

        For i As Integer = 0 To chkList.Count() - 1

            With Me._Frm.sprKanryo.ActiveSheet

                selectRow = Convert.ToInt32(chkList(i))
                sTsmcQtySumi = .Cells(selectRow, LMR010G.sprKanryoDef.TSMC_QTY_SUMI.ColNo).Value().ToString()
                sTsmcQty = .Cells(selectRow, LMR010G.sprKanryoDef.TSMC_QTY.ColNo).Value().ToString()

                If sTsmcQtySumi = "0" AndAlso sTsmcQty = "0" Then
                Else
                    If Convert.ToDecimal(sTsmcQtySumi) < Convert.ToDecimal(sTsmcQty) Then
                        Dim ret As MsgBoxResult =
                            Me.ShowMessage("W140", New String() {String.Concat(selectRow.ToString(), "行目は"), "検品済数が受信データの個数より少ない状態での完了指示"})
                        If ret = MsgBoxResult.Cancel Then
                            Return False
                        End If
                    End If
                End If

            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫データチェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsZaiChk(ByVal eventShubetsu As LMR010C.EventShubetsu, ByVal ds As DataSet, ByVal Syubetsu As String) As Boolean

        Dim dt As DataTable = ds.Tables("LMR010_OUT_CHECK")
        Dim max As Integer = dt.Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim outdr As DataRow = Nothing

        For i As Integer = 0 To max
            dr = dt.Rows(i)
            '入荷完了チェック（入庫日が空の場合）
            If String.IsNullOrEmpty(dr.Item("INKO_DATE").ToString) = True Then
                'START YANAI 要望番号409
                'MyBase.ShowMessage("E179", New String() {String.Concat("[出荷管理番号=", dr.Item("INOUTKA_NO_L").ToString, "]")})
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E389", New String() {String.Concat("[出荷管理番号=", dr.Item("INOUTKA_NO_L").ToString, "]")})
                MyBase.ShowMessage("E389", New String() {dr.Item("INOUTKA_NO_L").ToString})
                '2016.01.06 UMANO 英語化対応END
                'END YANAI 要望番号409
                Return False
            End If
            '大小チェック(在庫データの入庫日 + 出荷データ(大)の出荷日)
            outdr = ds.Tables("LMR010INOUT").Select(String.Concat("INOUTKA_NO_L = '", dr.Item("INOUTKA_NO_L").ToString, "'"))(0)
            If outdr.Item("INOUTKA_DATE").ToString < dr.Item("INKO_DATE").ToString Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E179", New String() {String.Concat("[出荷管理番号=", dr.Item("INOUTKA_NO_L").ToString, "]")})
                MyBase.ShowMessage("E179", New String() {dr.Item("INOUTKA_NO_L").ToString})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 在庫数チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsZaiNBQTChk(ByVal eventShubetsu As LMR010C.EventShubetsu, ByVal ds As DataSet, ByVal Syubetsu As String) As Boolean

        Dim zaidt As DataTable = ds.Tables("LMR010_ZAI_UPDDATA")
        Dim zaimax As Integer = zaidt.Rows.Count - 1
        Dim zaidr As DataRow = Nothing

        Dim outdt As DataTable = ds.Tables("LMR010_OUTKAS_UPDDATA")
        Dim outdr As DataRow = Nothing

        '更新後の在庫数を求めるために計算
        For i As Integer = 0 To zaimax
            zaidr = zaidt.Rows(i)
            outdr = outdt.Select(String.Concat("ZAI_REC_NO = '", zaidr.Item("ZAI_REC_NO").ToString, "'"))(0)

            If Convert.ToInt32(zaidr.Item("PORA_ZAI_NB").ToString()) < 0 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E381", New String() {"実予在庫個数", String.Concat("入出荷管理番号 = [", outdr.Item("OUTKA_NO_L").ToString, "]")})
                MyBase.ShowMessage("E894", New String() {outdr.Item("OUTKA_NO_L").ToString})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

            If Convert.ToDecimal(zaidr.Item("PORA_ZAI_QT").ToString()) < 0 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E381", New String() {"実予在庫数量", String.Concat("入出荷管理番号 = [", outdr.Item("OUTKA_NO_L").ToString, "]")})
                MyBase.ShowMessage("E895", New String() {outdr.Item("OUTKA_NO_L").ToString})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

            If Convert.ToInt32(zaidr.Item("ALCTD_NB").ToString()) < 0 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E381", New String() {"引当中梱数", String.Concat("入出荷管理番号 = [", outdr.Item("OUTKA_NO_L").ToString, "]")})
                MyBase.ShowMessage("E896", New String() {outdr.Item("OUTKA_NO_L").ToString})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

            If Convert.ToDecimal(zaidr.Item("ALCTD_QT").ToString()) < 0 Then
                '2016.01.06 UMANO 英語化対応START
                'MyBase.ShowMessage("E381", New String() {"引当中数量", String.Concat("入出荷管理番号 = [", outdr.Item("OUTKA_NO_L").ToString, "]")})
                MyBase.ShowMessage("E897", New String() {outdr.Item("OUTKA_NO_L").ToString})
                '2016.01.06 UMANO 英語化対応END
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 入力チェック（進捗区分のチェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsStateKbCheck(ByVal ds As DataSet, ByVal rootPgid As String) As String

        '【一覧チェック】
        Dim dt As DataTable = ds.Tables(LMR010C.TABLE_NM_STATECHK)
        Dim dr As DataRow = Nothing
        Dim max As Integer = dt.Rows.Count - 1

        Dim stateKbnBefore As String = String.Empty
        Dim stateKbnAfter As String = String.Empty

        With dt
            If ("LMB010").Equals(rootPgid) = True Then
                '入荷
                For i As Integer = 0 To max
                    dr = dt.Rows(i)
                    stateKbnAfter = String.Empty


                    '予定・受付票印刷→入庫済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.INKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_KENPIN_YN").ToString) = False AndAlso _
                        LMR010C.INKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_UKE_PRT_YN").ToString) = False AndAlso _
                        (LMR010C.INKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True OrElse _
                        LMR010C.INKA_SINTYOKU_20.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True) Then
                        '進捗区分が"40"以外
                        '入荷検品制御有無が"0"
                        '進捗区分が"30"以外
                        '入荷受付票印刷制御有無が"0"
                        '進捗区分が"10"または"20"
                        stateKbnAfter = LMR010C.INKA_SINTYOKU_50
                    End If

                    '受付済→入庫済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.INKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_KENPIN_YN").ToString) = False AndAlso _
                        LMR010C.INKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '進捗区分が"40"以外
                        '入荷検品制御有無が"0"
                        '進捗区分が"30"
                        stateKbnAfter = LMR010C.INKA_SINTYOKU_50
                    End If

                    '検品済→入庫済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.INKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '進捗区分が"40"
                        stateKbnAfter = LMR010C.INKA_SINTYOKU_50
                    End If

                    '予定・受付票印刷→検品済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_KENPIN_YN").ToString) = True AndAlso _
                        LMR010C.INKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_UKE_PRT_YN").ToString) = False AndAlso _
                        (LMR010C.INKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True OrElse _
                        LMR010C.INKA_SINTYOKU_20.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True) Then
                        '入荷検品制御有無が"1"
                        '進捗区分が"30"以外
                        '入荷受付票印刷制御有無が"0"
                        '進捗区分が"10"または"20"
                        stateKbnAfter = LMR010C.INKA_SINTYOKU_40
                    End If

                    '受付済→検品済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_KENPIN_YN").ToString) = True AndAlso _
                        LMR010C.INKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '入荷検品制御有無が"1"
                        '進捗区分が"30"
                        stateKbnAfter = LMR010C.INKA_SINTYOKU_40
                    End If

                    '予定・受付票印刷→受付済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("INKA_UKE_PRT_YN").ToString) = True AndAlso _
                        (LMR010C.INKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True OrElse _
                        LMR010C.INKA_SINTYOKU_20.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True) Then
                        '入荷受付票印刷制御有無が"1"
                        '進捗区分が"10"または"20"
                        stateKbnAfter = LMR010C.INKA_SINTYOKU_30
                    End If

                    If String.IsNullOrEmpty(stateKbnBefore) = False Then
                        If stateKbnBefore.Equals(stateKbnAfter) = False Then
                            '一つ前のデータの進捗区分と異なる場合はエラー
                            Return "E194"
                            Exit Function
                        End If
                    End If
                    stateKbnBefore = stateKbnAfter

                Next i

            ElseIf ("LMC010").Equals(rootPgid) = True Then
                '出荷
                For i As Integer = 0 To max
                    dr = dt.Rows(i)
                    stateKbnAfter = String.Empty

                    '予定入力済→出荷済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_50.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_KENPIN_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTOKA_KANRYO_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '進捗区分が"50"以外
                        '出荷検品制御有無が"0"
                        '進捗区分が"40"以外
                        '出庫完了制御有無が"0"
                        '進捗区分が"30"以外
                        '出荷指図書印刷制御有無が"0"
                        '進捗区分が"10"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_60
                    End If

                    '指図書印刷→出荷済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_50.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_KENPIN_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTOKA_KANRYO_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '進捗区分が"50"以外
                        '出荷検品制御有無が"0"
                        '進捗区分が"40"以外
                        '出庫完了制御有無が"0"
                        '進捗区分が"30"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_60
                    End If

                    '出庫済→出荷済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_50.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_KENPIN_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '進捗区分が"50"以外
                        '出荷検品制御有無が"0"
                        '進捗区分が"40"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_60
                    End If

                    '検品済→出荷済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_50.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '進捗区分が"50"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_60
                    End If

                    '予定入力済→検品済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_KENPIN_YN").ToString) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTOKA_KANRYO_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '出荷検品制御有無が"1"
                        '進捗区分が"40"以外
                        '出庫完了制御有無が"0"
                        '進捗区分が"30"以外
                        '出荷指図書印刷制御有無が"0"
                        '進捗区分が"10"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_50
                    End If

                    '指図書印刷→検品済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_KENPIN_YN").ToString) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTOKA_KANRYO_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '出荷検品制御有無が"1"
                        '進捗区分が"40"以外
                        '出庫完了制御有無が"0"
                        '進捗区分が"30"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_50
                    End If

                    '出庫済→検品済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_KENPIN_YN").ToString) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_40.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '出荷検品制御有無が"1"
                        '進捗区分が"40"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_50
                    End If

                    '予定入力済→出庫済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTOKA_KANRYO_YN").ToString) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = False AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString) = False AndAlso _
                        LMR010C.OUTKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '出庫完了制御有無が"1"
                        '進捗区分が"30"以外
                        '出荷指図書印刷制御有無が"0"
                        '進捗区分が"10"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_40
                    End If

                    '指図書印刷→出庫済
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTOKA_KANRYO_YN").ToString) = True AndAlso _
                        LMR010C.OUTKA_SINTYOKU_30.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '出庫完了制御有無が"0"
                        '進捗区分が"30"
                        stateKbnAfter = LMR010C.OUTKA_SINTYOKU_40
                    End If

                    '予定入力済で、出荷指図書が印刷がされていない時のエラー判定はここでする
                    If String.IsNullOrEmpty(stateKbnAfter) = True AndAlso _
                        (LMR010C.FLG_ON).Equals(dr.Item("OUTKA_SASHIZU_PRT_YN").ToString) AndAlso _
                        LMR010C.OUTKA_SINTYOKU_10.Equals(dt.Rows(i).Item("STATE_KB").ToString) = True Then
                        '出荷指図書印刷制御有無が"1"
                        '進捗区分が"10"
                        Return "E313"
                        Exit Function
                    End If

                    If String.IsNullOrEmpty(stateKbnBefore) = False Then
                        If stateKbnBefore.Equals(stateKbnAfter) = False Then
                            '一つ前のデータの進捗区分と異なる場合はエラー
                            Return "E194"
                            Exit Function
                        End If
                    End If
                    stateKbnBefore = stateKbnAfter

                Next i

            End If

        End With

        Return stateKbnAfter

    End Function

    ''' <summary>
    ''' 入力チェック（残個数のチェック）
    ''' </summary>
    ''' <returns>
    ''' True ：入力エラーなし
    ''' false：入力エラーあり
    ''' </returns>
    ''' <remarks>チェックを行うプロパティをTrueに設定し、チェック用メソッドを呼び出します。</remarks>
    Friend Function IsZanKosuCheck(ByVal dr As DataRow) As String

        '【チェック】
        If 0 < Convert.ToInt32(Me.FormatNumValue(dr.Item("BACKLOG_NB").ToString)) Then
            Return dr.Item("INOUTKA_NO_L").ToString
            Exit Function
        End If

        Return String.Empty

    End Function

    ''' <summary>
    ''' 入荷進捗区分チェック
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks>出荷確定時に入荷データが入荷完了済みかチェック</remarks>
    Friend Function IsInkaZumiChk() As Boolean

        'チェックされた行番号取得
        Dim chkList As ArrayList = New ArrayList()
        chkList = Me.getCheckList()
        Dim max As Integer = chkList.Count() - 1
        Dim selectRow As Integer = 0
        Dim inkaStateKb As String = String.Empty
        Dim outkaNoL As String = String.Empty
        'START YANAI 要望番号932
        Dim sCnt As String = String.Empty
        'END YANAI 要望番号932


        For i As Integer = 0 To max

            With Me._Frm.sprKanryo.ActiveSheet

                selectRow = Convert.ToInt32(chkList(i))
                inkaStateKb = .Cells(selectRow, LMR010G.sprKanryoDef.CHK_INKA_STATE_KB.ColNo).Value().ToString()
                outkaNoL = .Cells(selectRow, LMR010G.sprKanryoDef.KANRI_NO_L.ColNo).Value().ToString()
                'START YANAI 要望番号932
                sCnt = .Cells(selectRow, LMR010G.sprKanryoDef.SCNT.ColNo).Value().ToString()
                'END YANAI 要望番号932

                'START YANAI 要望番号394
                'START YANAI 要望番号932
                'If String.IsNullOrEmpty(inkaStateKb) = True Then
                '    MyBase.ShowMessage("E114", New String() {String.Concat("出荷管理番号 = [", outkaNoL, "]")})
                '    Return False
                'End If
                If ("0").Equals(sCnt) = True Then
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E114", New String() {String.Concat("出荷管理番号 = [", outkaNoL, "]")})
                    MyBase.ShowMessage("E114", New String() {String.Concat(.GetColumnLabel(0, LMR010G.sprKanryoDef.KANRI_NO_L.ColNo), "= [", outkaNoL, "]")})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If
                'END YANAI 要望番号932
                'END YANAI 要望番号394

                If LMR010C.INKA_SINTYOKU_50 <= inkaStateKb Then
                Else
                    '2016.01.06 UMANO 英語化対応START
                    'MyBase.ShowMessage("E389", New String() {String.Concat("出荷管理番号 = [", outkaNoL, "]")})
                    MyBase.ShowMessage("E389", New String() {String.Concat(.GetColumnLabel(0, LMR010G.sprKanryoDef.KANRI_NO_L.ColNo), "= [", outkaNoL, "]")})
                    '2016.01.06 UMANO 英語化対応END
                    Return False
                End If

            End With

        Next

        Return True

    End Function

    ''' <summary>
    ''' 権限チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsAuthorityChk(ByVal eventShubetsu As LMR010C.EventShubetsu) As Boolean

        'ログインユーザーの権限
        Dim kengen As String = LMUserInfoManager.GetAuthoLv
        Dim kengenFlg As Boolean = True

        Select Case eventShubetsu

            Case LMR010C.EventShubetsu.TORIKOMI     '取込
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

            Case LMR010C.EventShubetsu.KENSAKU      '検索
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

            Case LMR010C.EventShubetsu.MASTER       'マスタ参照
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

            Case LMR010C.EventShubetsu.KANRYO       '完了
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

            Case LMR010C.EventShubetsu.CLOSE           '閉じる
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
    Friend Sub TrimSpaceTextValue()

        With Me._Frm
            '各項目のTrim処理
            .txtTantoCD.TextValue = Trim(.txtTantoCD.TextValue)
            .txtCustCD.TextValue = Trim(.txtCustCD.TextValue)

            'スプレッドのスペース除去
            Call Me._LMRconV.TrimSpaceSprTextvalue(Me._Frm.sprKanryo, 0)

        End With

    End Sub

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

#Region "選択行取得"
    ''' <summary>
    ''' 選択された行の行番号をArrayListに格納
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function getCheckList() As ArrayList

        'チェックボックスセルのカラム№取得
        Dim defNo As Integer = LMR010C.sprKanryoColumnIndex.DEF

        Return Me._LMRconV.SprSelectList(defNo, Me._Frm.sprKanryo)

    End Function

#End Region

#End Region 'Method

End Class
