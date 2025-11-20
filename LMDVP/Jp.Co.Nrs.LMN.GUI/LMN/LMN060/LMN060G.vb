' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN060G   : 拠点別在庫一覧
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMN060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMN060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMN060F

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付ける。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMN060F)

        '親クラスのコンストラクタを呼ぶ。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付る。
        MyBase.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付ける。
        MyBase.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "Method"

#Region "FunctionKey"

    ''' <summary>
    ''' ファンクションキーの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFunctionKey()

        Dim always As Boolean = True

        With Me._Frm.FunctionKey

            'ファンクションキータイプの指定
            .SetFKeysType(LMImFunctionKey.FkeyTypes.LIST)

            .Enabled = True

            'ファンクションキー個別設定
            .F1ButtonName = String.Empty
            .F2ButtonName = String.Empty
            .F3ButtonName = String.Empty
            .F4ButtonName = String.Empty
            .F5ButtonName = "在庫日数算出"
            .F6ButtonName = String.Empty
            .F7ButtonName = String.Empty
            .F8ButtonName = String.Empty
            .F9ButtonName = "検 索"
            .F10ButtonName = String.Empty
            .F11ButtonName = String.Empty
            .F12ButtonName = "閉じる"

            'ファンクションキーの制御
            .F1ButtonEnabled = False
            .F2ButtonEnabled = False
            .F3ButtonEnabled = False
            .F4ButtonEnabled = False
            .F5ButtonEnabled = always
            .F6ButtonEnabled = False
            .F7ButtonEnabled = False
            .F8ButtonEnabled = False
            .F9ButtonEnabled = always
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = always

        End With

    End Sub

#End Region

#Region "Form"

#Region "設定・制御"

    ''' <summary>
    ''' タブインデックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetTabIndex()

        With Me._Frm

            .grpSearch.TabIndex = LMN060C.CtlTabIndex.GRP_SEARCH
            .cmbCustCd.TabIndex = LMN060C.CtlTabIndex.NINUSHI
            .chkKeppinOnly.TabIndex = LMN060C.CtlTabIndex.KEPPIN_ONLY
            .chkDispSoko.TabIndex = LMN060C.CtlTabIndex.DISP_SOKO
            .sprDetail.TabIndex = LMN060C.CtlTabIndex.SPR_DETAIL

        End With

    End Sub

    ''' <summary>
    ''' 画面コントロールの個別設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetControl(ByVal ds As DataSet)

        '初期値設定
        Call Me.ClearControl(ds)

        'コンボボックスの設定
        Call Me.CreateComboBox()

    End Sub

    ''' <summary>
    ''' コンボボックスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        '区分マスタ検索処理（荷主コンボ設定用）
        Dim cd As String = String.Empty
        Dim item As String = String.Empty
        Dim sort As String = "KBN_CD"
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = '", LMKbnConst.KBN_S032, "' AND SYS_DEL_FLG = '0'"), sort)

        Dim max As Integer = getDr.Length - 1
        For i As Integer = 0 To max

            item = getDr(i).Item("KBN_NM3").ToString()
            cd = getDr(i).Item("KBN_NM1").ToString()

            Me._Frm.cmbCustCd.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        Next

    End Sub

    ''' <summary>
    ''' ロックを解除する
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub UnLockedForm()

        Call Me.SetFunctionKey()

    End Sub

    ''' <summary>
    ''' フォーカスの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetFoucus()

        Me._Frm.grpSearch.Focus()

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' コントロール値のクリア
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub ClearControl(ByVal ds As DataSet)

        If ds Is Nothing = True Then
            Exit Sub
        End If

        Dim dtParam As DataTable = ds.Tables(LMN060C.TABLE_NM_IN)

        With Me._Frm

            If dtParam IsNot Nothing _
            AndAlso dtParam.Rows.Count > 0 _
            AndAlso String.IsNullOrEmpty(dtParam.Rows(0).Item("SCM_CUST_CD").ToString()) = False Then

                .cmbCustCd.SelectedValue = dtParam.Rows(0).Item("SCM_CUST_CD").ToString
            Else
                .cmbCustCd.SelectedValue = LMN060C.INIT_NINUSHI
            End If
            .chkKeppinOnly.Checked = False
            .chkDispSoko.Checked = True

        End With

    End Sub

#End Region

#End Region

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        '*****表示列*****
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMN060C.SprColumnIndex.CHECK, " ", 20, True)
        Public Shared ITEM_CD As SpreadColProperty = New SpreadColProperty(LMN060C.SprColumnIndex.SHOHIN_CD, "商品ｺｰﾄﾞ", 150, True)
        Public Shared ITEM_NM As SpreadColProperty = New SpreadColProperty(LMN060C.SprColumnIndex.SHOHIN_NM, "商品名称", 440, True)
        Public Shared NUM_TITLE As SpreadColProperty = New SpreadColProperty(LMN060C.SprColumnIndex.SPACE, "　", 150, True)
        '*****隠し列*****
        Public Shared SOKO_CD As SpreadColProperty = New SpreadColProperty(LMN060C.SprColumnIndex.SOKO_CD, "　", 0, False)
        Public Shared BR_CD As SpreadColProperty = New SpreadColProperty(LMN060C.SprColumnIndex.BR_CD, "　", 0, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <param name="ds">列ヘッダ取得結果格納DS</param>
    ''' <param name="firstFlg">初期検索時にLMConst.FLG.ONを設定</param>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal ds As DataSet, Optional ByVal firstFlg As String = LMConst.FLG.OFF)

        With Me._Frm.sprDetail

            'スプレッドの行をクリア
            .CrearSpread()

            '列数設定
            .ActiveSheet.ColumnCount = 6

            'スプレッドの列設定
            .SetColProperty(New LMN060G.sprDetailDef())

            '列固定位置を設定する。
            .ActiveSheet.FrozenColumnCount = LMN060G.sprDetailDef.NUM_TITLE.ColNo + 1

            '列設定用変数
            Dim spr As LMSpreadSearch = Me._Frm.sprDetail
            Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)

            '列設定
            '*****表示列*****
            .SetCellStyle(0, LMN060G.sprDetailDef.DEF.ColNo, lbl)
            .SetCellStyle(0, LMN060G.sprDetailDef.ITEM_CD.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.HAN_NUM_ALPHA, 20, False))
            .SetCellStyle(0, LMN060G.sprDetailDef.ITEM_NM.ColNo, LMSpreadUtility.GetTextCell(spr, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)
            .SetCellStyle(0, LMN060G.sprDetailDef.NUM_TITLE.ColNo, lbl)
            '*****隠し列*****
            .SetCellStyle(0, LMN060G.sprDetailDef.SOKO_CD.ColNo, lbl)
            .SetCellStyle(0, LMN060G.sprDetailDef.BR_CD.ColNo, lbl)

            '初期設定時は処理を抜ける
            If firstFlg.Equals(LMConst.FLG.ON) Then
                Exit Sub
            End If

            Dim dt As DataTable = ds.Tables(LMN060C.TABLE_NM_OUT_HDR)
            If ds Is Nothing = False _
                AndAlso dt.Rows.Count <> 0 _
                AndAlso String.IsNullOrEmpty(dt.Rows(0).Item("SOKO_CD").ToString()) = False Then

                '重複している倉庫コードを削除する
                Dim max As Integer = dt.Rows.Count - 1
                Dim sokoCd As String = String.Empty
                For i As Integer = max To 0 Step -1
                    If sokoCd.Equals(dt.Rows(i).Item("SOKO_CD").ToString()) Then
                        dt.Rows.Remove(dt.Rows(i))
                        max = max - 1
                    Else
                        sokoCd = dt.Rows(i).Item("SOKO_CD").ToString()
                    End If
                Next

                '検索結果を元に列追加
                .ActiveSheet.AddColumns(6, max + 1)

                '設定に関する変数定義
                Dim colIndex As Integer = 0
                Dim font As System.Drawing.Font = New System.Drawing.Font("ＭＳ ゴシック", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(128, Byte))

                For i As Integer = 0 To max

                    colIndex = LMN060C.SprColumnIndex.BR_CD + i + 1

                    '列ヘッダのテキスト設定
                    '*****表示列*****
                    .ActiveSheet.ColumnHeader.Cells(0, colIndex).Text = dt.Rows(i).Item("SOKO_NM").ToString
                    .ActiveSheet.Columns(colIndex, colIndex).Width = 100
                    .ActiveSheet.ColumnHeader.Cells(0, colIndex).Font = font
                    .SetCellStyle(0, colIndex, lbl)

                Next

            End If

        End With

    End Sub

    ''' <summary>
    ''' 初期値を設定します
    ''' </summary>
    ''' <param name="frm"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal frm As LMN060F)

        With frm.sprDetail.ActiveSheet

            Dim max As Integer = Me._Frm.sprDetail.ActiveSheet.ColumnCount - 1

            For i As Integer = 0 To max
                .Cells(0, i).Value = String.Empty
            Next

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal ds As DataSet)

        '検索結果格納テーブル定義
        Dim dtHed As DataTable = ds.Tables(LMN060C.TABLE_NM_OUT_HDR)
        Dim dtOut As DataTable = ds.Tables(LMN060C.TABLE_NM_OUT)

        '欠品・欠品危惧チェックボックスのチェックがあるときテーブル編集を行う
        If Me._Frm.chkKeppinOnly.GetBinaryValue().Equals(LMN060C.FLG.ON) Then
            Call Me.KeppinFlgOn(dtOut)
        End If

        Dim dr As DataRow() = dtOut.Select("1=1", "CUST_GOODS_CD,SOKO_CD")
        Dim shohinCd_OLD As String = String.Empty
        Dim shohinCd_NEW As String = String.Empty
        Dim spr As LMSpreadSearch = Me._Frm.sprDetail

        With spr

            If dr.Length > 0 Then

                '在庫日数算出日時設定
                Dim nissuDate As String = DateFormatUtility.EditSlash(dr(0).Item("NISSU_DATE").ToString) 'スラッシュ編集
                Dim strNissuTime As String = dr(0).Item("NISSU_TIME").ToString
                Dim nissuTime As String = String.Empty
                If strNissuTime.Length >= 6 Then
                    nissuTime = String.Concat(strNissuTime.Substring(0, 2), ":", strNissuTime.Substring(2, 2), ":", strNissuTime.Substring(4, 2))
                End If
                Me._Frm.lblZaikoNissuSanshutsuDate.Text = Me.EditConcatData(nissuDate, nissuTime, " ")

                .SuspendLayout()

                '列設定用変数
                Dim check As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
                Dim lbl As StyleInfo = LMSpreadUtility.GetLabelCell(spr)
                Dim num As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 99999999999, True, , , ",")

                Dim max As Integer = dr.Length - 1
                Dim maxCol As Integer = Me._Frm.sprDetail.ActiveSheet.ColumnCount - 1
                Dim rowHeadCnt As Integer = 0
                Dim setCol As Integer = 6
                Dim rowNum As Integer = 0
                For i As Integer = 0 To max

                    shohinCd_NEW = dr(i).Item("CUST_GOODS_CD").ToString()

                    If shohinCd_NEW.Equals(shohinCd_OLD) = False Then

                        If String.IsNullOrEmpty(shohinCd_OLD) = False _
                        AndAlso maxCol.Equals(setCol - 1) = False Then
                            For j As Integer = setCol To maxCol
                                '在庫情報全てに'0'を設定
                                .SetCellValue(rowNum, j, "0")
                                .SetCellValue(rowNum + 1, j, "0")
                                .SetCellValue(rowNum + 2, j, "0")
                            Next
                        End If

                        '値設定列初期化
                        setCol = 6

                        '三行追加
                        rowNum = .ActiveSheet.Rows.Count
                        .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, 3)

                        '行の連結
                        .ActiveSheet.AddSpanCell(rowNum, LMN060G.sprDetailDef.DEF.ColNo, 3, 1)
                        .ActiveSheet.AddSpanCell(rowNum, LMN060G.sprDetailDef.ITEM_CD.ColNo, 3, 1)
                        .ActiveSheet.AddSpanCell(rowNum, LMN060G.sprDetailDef.ITEM_NM.ColNo, 3, 1)
                        .ActiveSheet.AddSpanCell(rowNum, LMN060G.sprDetailDef.SOKO_CD.ColNo, 3, 1)
                        .ActiveSheet.AddSpanCell(rowNum, LMN060G.sprDetailDef.BR_CD.ColNo, 3, 1)

                        'セルスタイル設定
                        .SetCellStyle(rowNum, LMN060G.sprDetailDef.DEF.ColNo, check)
                        .SetCellStyle(rowNum, LMN060G.sprDetailDef.ITEM_CD.ColNo, lbl)
                        .SetCellStyle(rowNum, LMN060G.sprDetailDef.ITEM_NM.ColNo, lbl)
                        .SetCellStyle(rowNum, LMN060G.sprDetailDef.NUM_TITLE.ColNo, lbl)
                        .SetCellStyle(rowNum + 1, LMN060G.sprDetailDef.NUM_TITLE.ColNo, lbl)
                        .SetCellStyle(rowNum + 2, LMN060G.sprDetailDef.NUM_TITLE.ColNo, lbl)

                        For k As Integer = 6 To maxCol
                            .SetCellStyle(rowNum, k, num)
                            .SetCellStyle(rowNum + 1, k, num)
                            .SetCellStyle(rowNum + 2, k, num)
                        Next

                        'セル値設定
                        .SetCellValue(rowNum, LMN060G.sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                        .SetCellValue(rowNum, LMN060G.sprDetailDef.ITEM_CD.ColNo, shohinCd_NEW)
                        .SetCellValue(rowNum, LMN060G.sprDetailDef.ITEM_NM.ColNo, dr(i).Item("GOODS_NM").ToString())
                        .SetCellValue(rowNum, LMN060G.sprDetailDef.NUM_TITLE.ColNo, "現在在庫数量")
                        .SetCellValue(rowNum + 1, LMN060G.sprDetailDef.NUM_TITLE.ColNo, "近出荷量での在庫日数")
                        .SetCellValue(rowNum + 2, LMN060G.sprDetailDef.NUM_TITLE.ColNo, "未出荷オーダー数")
                        .SetCellValue(rowNum, LMN060G.sprDetailDef.SOKO_CD.ColNo, dr(i).Item("SOKO_CD").ToString())
                        .SetCellValue(rowNum, LMN060G.sprDetailDef.BR_CD.ColNo, dr(i).Item("BR_CD").ToString())

                    End If

                    For j As Integer = setCol To maxCol

                        If dr(i).Item("SOKO_CD").Equals(dtHed.Rows(j - 6).Item("SOKO_CD")) Then

                            '次回設定用に変数に+1する
                            setCol = setCol + 1

                            '在庫情報に検索結果を設定
                            .SetCellValue(rowNum, j, dr(i).Item("ZAIKO_NB").ToString())
                            .SetCellValue(rowNum + 1, j, dr(i).Item("ZAIKO_NISSU").ToString())
                            .SetCellValue(rowNum + 2, j, dr(i).Item("PLAN_OUTKA_NB").ToString())
                            Exit For
                        Else

                            '次回設定用に変数に+1する
                            setCol = setCol + 1

                            '在庫情報全てに'0'を設定
                            .SetCellValue(rowNum, j, "0")
                            .SetCellValue(rowNum + 1, j, "0")
                            .SetCellValue(rowNum + 2, j, "0")
                        End If
                    Next

                    If i = max _
                    AndAlso maxCol.Equals(setCol - 1) = False Then
                        For j As Integer = setCol To maxCol
                            '在庫情報全てに'0'を設定
                            .SetCellValue(rowNum, j, "0")
                            .SetCellValue(rowNum + 1, j, "0")
                            .SetCellValue(rowNum + 2, j, "0")
                        Next
                    End If

                    shohinCd_OLD = dr(i).Item("CUST_GOODS_CD").ToString()

                Next

                .ResumeLayout(True)

            End If

        End With

    End Sub

#Region "内部メソッド"

    ''' <summary>
    ''' 欠品フラグON時の検索結果編集
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Sub KeppinFlgOn(ByVal dt As DataTable)

        '①各商品コードについて、現在在庫数量が0の倉庫が一つ以上存在する場合、表示する
        Dim array As ArrayList = Me.ZaikoSuZero(dt)

        '②各商品コードについて、近出荷量での在庫日数が14日以下の倉庫が一つ以上存在する場合、表示する
        array = Me.ZaikoNissuUnder14(dt, array)

        '表示対象の商品コードのみを含むデータテーブル作成
        Call Me.HyojiOutDate(dt, array)

    End Sub

    ''' <summary>
    ''' 欠品フラグON時の検索結果編集(①現在在庫数量が0の倉庫が存在する商品コードを保持)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Function ZaikoSuZero(ByVal dt As DataTable) As ArrayList

        '********************↓↓ 現在在庫数量が0の倉庫が1つも存在しない商品コードを抽出↓↓ ********************

        '商品コードごとに検索結果を並び替える
        Dim dr As DataRow() = dt.Select("1=1", "CUST_GOODS_CD")

        'ループ内で使用する変数定義
        Dim hyojiFlg As Boolean = False
        Dim shohinCd_OLD As String = String.Empty
        Dim shohinCd_NEW As String = String.Empty
        Dim array As ArrayList = New ArrayList()

        Dim max As Integer = dr.Length - 1
        For i As Integer = 0 To max

            shohinCd_NEW = dr(i).Item("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(shohinCd_OLD) = False _
            AndAlso shohinCd_OLD.Equals(shohinCd_NEW) = False Then

                '表示対象の商品コードを保持
                If hyojiFlg = True Then
                    array.Add(shohinCd_OLD)
                End If

                '表示フラグの初期化
                hyojiFlg = False
            End If

            If dr(i).Item("ZAIKO_NB").ToString().Equals("0") Then
                '在庫数が0の倉庫があれば表示
                hyojiFlg = True
            End If

            If i = max Then
                '表示対象の商品コードを保持
                If hyojiFlg = True Then
                    array.Add(shohinCd_NEW)
                End If
            Else
                shohinCd_OLD = dr(i).Item("CUST_GOODS_CD").ToString()
            End If
        Next

        Return array

    End Function

    ''' <summary>
    ''' 欠品フラグON時の検索結果編集(②近出荷量での在庫日数が14日以下の倉庫が存在しない商品コードを保持)
    ''' </summary>
    ''' <param name="dt"></param>
    ''' <remarks></remarks>
    Private Function ZaikoNissuUnder14(ByVal dt As DataTable, ByVal array As ArrayList) As ArrayList

        '********************↓↓ 近出荷量での在庫日数が14日以下の倉庫が存在しない商品コードを抽出↓↓ ********************

        '商品コードごとに検索結果を並び替える
        Dim dr As DataRow() = dt.Select("1=1", "CUST_GOODS_CD")

        'ループ内で使用する変数定義
        Dim hyojiFlg As Boolean = False
        Dim shohinCd_OLD As String = String.Empty
        Dim shohinCd_NEW As String = String.Empty

        Dim max As Integer = dr.Length - 1
        For i As Integer = 0 To max

            shohinCd_NEW = dr(i).Item("CUST_GOODS_CD").ToString()

            If String.IsNullOrEmpty(shohinCd_OLD) = False _
            AndAlso shohinCd_OLD.Equals(shohinCd_NEW) = False Then
                '表示対象の商品コードを保持
                If hyojiFlg = True Then
                    array.Add(shohinCd_OLD)
                End If

                '表示フラグの初期化
                hyojiFlg = False
            End If

            If Convert.ToInt32(dr(i).Item("ZAIKO_NISSU").ToString()) <= 14 Then
                '近出荷量での在庫日数が14日以下の倉庫が一つ以上存在する場合、表示する
                hyojiFlg = True
            End If

            If i = max Then
                '表示対象の商品コードを保持
                If hyojiFlg = True Then
                    array.Add(shohinCd_NEW)
                End If
            Else
                shohinCd_OLD = dr(i).Item("CUST_GOODS_CD").ToString()
            End If
        Next

        Return array

    End Function

    ''' <summary>
    ''' 欠品フラグON時の検索結果編集(対象商品コードのみでDT作成)
    ''' </summary>
    ''' <param name="array"></param>
    ''' <remarks></remarks>
    Private Sub HyojiOutDate(ByVal dt As DataTable, ByVal array As ArrayList)

        '表示レコードのみを含むテーブル
        Dim CopyDt As DataTable = dt.Copy()
        dt.Rows.Clear()

        Dim outGoodsCd As String = String.Empty
        Dim max As Integer = CopyDt.Rows.Count - 1
        Dim arrayMax As Integer = array.Count - 1
        For i As Integer = 0 To max
            outGoodsCd = CopyDt.Rows(i).Item("CUST_GOODS_CD").ToString()

            For j As Integer = 0 To arrayMax
                If array(j).ToString().Equals(outGoodsCd) Then
                    '表示対象のレコードを格納
                    dt.ImportRow(CopyDt.Rows(i))
                    Exit For
                End If
            Next

        Next

    End Sub

#End Region

#End Region

#Region "部品化検討中"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Private Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Call Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim lblArr As ArrayList = New ArrayList()

        'エディット系コントロールのロック
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            'テキストボックスの場合、ラベル項目であったら処理対象外とする
            If TypeOf arrCtl Is Win.InputMan.LMImTextBox = True Then

                If DirectCast(arrCtl, Win.InputMan.LMImTextBox).Name.Substring(0, 3).Equals("lbl") = True Then
                    lblArr.Add(arrCtl)
                End If

            End If

            'ロック処理/ロック解除処理を行う
            arrCtl.ReadOnlyStatus = lockFlg
        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            lblCtl.ReadOnlyStatus = True
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr
            'ロック処理/ロック解除処理を行う
            Call Me.LockButton(arrCtl, lockFlg)
        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Call Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Call Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(ボタン)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockButton(ByVal ctl As Win.LMButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックする(チェックボックス)
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCheckBox(ByVal ctl As Win.LMCheckBox, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.EnableStatus = enabledFlg

    End Sub

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Private Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

    ''' <summary>
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Private Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = " - ") As String

        EditConcatData = value1
        If String.IsNullOrEmpty(EditConcatData) = True Then

            EditConcatData = value2

        Else

            If String.IsNullOrEmpty(value2) = False Then

                EditConcatData = String.Concat(EditConcatData, str, value2)

            End If

        End If

        Return EditConcatData

    End Function

#End Region

#End Region

End Class

