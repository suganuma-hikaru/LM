' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ260G : 荷主マスタ照会（大・中）
'  作  成  者       :  平山
' ==========================================================================
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Utility.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.Spread

''' <summary>
''' LMZ260Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ260G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ260F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ260F)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region 'Constructor

#Region "Method"

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 180, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_NM_M, "荷主名(中)", 180, True)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_NM_S, "荷主名(小)", 180, True)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_NM_SS, "荷主名(極小)", 180, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 90, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 90, True)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_CD_S, "荷主コード" & vbCrLf & "(小)", 90, True)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CUST_CD_SS, "荷主コード" & vbCrLf & "(極小)", 90, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)
        'START YANAI 要望番号558
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.CLOSE_KB, "締日区分", 80, False)
        'END YANAI 要望番号558
        'START YANAI 要望番号836
        Public Shared UNCHIN_TARIFF_CD1 As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.UNCHIN_TARIFF_CD1, "運賃タリフコード（屯キロ建）", 0, False)
        Public Shared UNCHIN_TARIFF_REM1 As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.UNCHIN_TARIFF_REM1, "運賃タリフコード備考（屯キロ建）", 0, False)
        Public Shared UNCHIN_TARIFF_CD2 As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.UNCHIN_TARIFF_CD2, "運賃タリフコード（車建）", 0, False)
        Public Shared UNCHIN_TARIFF_REM2 As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.UNCHIN_TARIFF_REM2, "運賃タリフコード備考（車建）", 0, False)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.EXTC_TARIFF_CD, "割増タリフコード", 0, False)
        Public Shared EXTC_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.EXTC_TARIFF_REM, "割増タリフ備考", 0, False)
        'END YANAI 要望番号836
        '要望番号:1839 terakawa 2013.02.08 Start
        Public Shared PICK_LIST_KB As SpreadColProperty = New SpreadColProperty(LMZ260C.SprColumnIndex.PICK_LIST_KB, "ピッキングリスト区分", 0, False)
        '要望番号:1839 terakawa 2013.02.08 End

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            'START YANAI 要望番号558
            '.sprDetail.ActiveSheet.ColumnCount = 10
            'START YANAI 要望番号836
            '.sprDetail.ActiveSheet.ColumnCount = 11
            '要望番号:1839 terakawa 2013.02.08 Start
            '.sprDetail.ActiveSheet.ColumnCount = 17
            .sprDetail.ActiveSheet.ColumnCount = 18
            '要望番号:1839 terakawa 2013.02.08 End
            'END YANAI 要望番号836
            'END YANAI 要望番号558

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ260G.sprDetailDef.DEF.ColNo + 1

            '列設定
            Dim lock As Boolean() = Me.SetLockControl(drow)
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock(LMZ260C.LOCK.M)))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_NM_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock(LMZ260C.LOCK.S)))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_NM_SS.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock(LMZ260C.LOCK.SS)))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock(LMZ260C.LOCK.M)))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock(LMZ260C.LOCK.S)))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CUST_CD_SS.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock(LMZ260C.LOCK.SS)))
            'START YANAI 要望番号558
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.CLOSE_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            'END YANAI 要望番号558
            'START YANAI 要望番号836
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_REM1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_REM2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.EXTC_TARIFF_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.EXTC_TARIFF_REM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            'END YANAI 要望番号836
            .sprDetail.SetCellStyle(0, LMZ260G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

            '表示・非表示設定
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CUST_NM_M.ColNo).Visible = Not lock(LMZ260C.LOCK.M)
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CUST_NM_S.ColNo).Visible = Not lock(LMZ260C.LOCK.S)
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CUST_NM_SS.ColNo).Visible = Not lock(LMZ260C.LOCK.SS)
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CUST_CD_M.ColNo).Visible = Not lock(LMZ260C.LOCK.M)
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CUST_CD_S.ColNo).Visible = Not lock(LMZ260C.LOCK.S)
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CUST_CD_SS.ColNo).Visible = Not lock(LMZ260C.LOCK.SS)
            'START YANAI 要望番号558
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.CLOSE_KB.ColNo).Visible = True
            'END YANAI 要望番号558
            'START YANAI 要望番号836
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.UNCHIN_TARIFF_REM1.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.UNCHIN_TARIFF_REM2.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.EXTC_TARIFF_REM.ColNo).Visible = False
            'END YANAI 要望番号836
            '要望番号:1839 terakawa 2013.02.08 Start
            .sprDetail.ActiveSheet.Columns(LMZ260G.sprDetailDef.PICK_LIST_KB.ColNo).Visible = False
            '要望番号:1839 terakawa 2013.02.08 End

        End With

        '初期値設定
        Call Me.SetInitValue(drow)

    End Sub

    ''' <summary>
    ''' 小、極小のロック制御
    ''' </summary>
    ''' <param name="drow">DataRow</param>
    ''' <returns>
    ''' Boolean配列
    ''' 1番目：中のロック制御
    ''' 2番目：小のロック制御
    ''' 3番目：極小のロック制御
    ''' </returns>
    ''' <remarks></remarks>
    Private Function SetLockControl(ByVal drow As DataRow) As Boolean()

        Select Case drow.Item("HYOJI_KBN").ToString()

            Case LMZControlC.HYOJI_M

                '初期値設定
                drow.Item("CUST_CD_M") = LMZ260C.OYA_CUST
                drow.Item("CUST_NM_M") = String.Empty
                drow.Item("CUST_CD_S") = LMZ260C.OYA_CUST
                drow.Item("CUST_NM_S") = String.Empty
                drow.Item("CUST_CD_SS") = LMZ260C.OYA_CUST
                drow.Item("CUST_NM_SS") = String.Empty

                '中、小、極小をロック
                Return New Boolean() {True, True, True}

            Case LMZControlC.HYOJI_S

                '初期値設定
                drow.Item("CUST_CD_S") = LMZ260C.OYA_CUST
                drow.Item("CUST_NM_S") = String.Empty
                drow.Item("CUST_CD_SS") = LMZ260C.OYA_CUST
                drow.Item("CUST_NM_SS") = String.Empty

                '小、極小をロック
                Return New Boolean() {False, True, True}

            Case LMZControlC.HYOJI_SS

                '初期値設定
                drow.Item("CUST_CD_SS") = LMZ260C.OYA_CUST
                drow.Item("CUST_NM_SS") = String.Empty

                '極小をロック
                Return New Boolean() {False, False, True}

        End Select

        '中、小、極小を活性化
        Return New Boolean() {False, False, False}

    End Function

    ''' <summary>
    ''' 営業所コンボボックス初期値とスプレッド初期値を設定します
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        _Frm.cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD")

        With _Frm.sprDetail.ActiveSheet

            .Cells(0, LMZ260G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMZ260G.sprDetailDef.CUST_NM_L.ColNo).Value = drow("CUST_NM_L")
            .Cells(0, LMZ260G.sprDetailDef.CUST_NM_M.ColNo).Value = drow("CUST_NM_M")
            .Cells(0, LMZ260G.sprDetailDef.CUST_NM_S.ColNo).Value = drow("CUST_NM_S")
            .Cells(0, LMZ260G.sprDetailDef.CUST_NM_SS.ColNo).Value = drow("CUST_NM_SS")
            .Cells(0, LMZ260G.sprDetailDef.CUST_CD_L.ColNo).Value = drow("CUST_CD_L")
            .Cells(0, LMZ260G.sprDetailDef.CUST_CD_M.ColNo).Value = drow("CUST_CD_M")
            .Cells(0, LMZ260G.sprDetailDef.CUST_CD_S.ColNo).Value = drow("CUST_CD_S")
            .Cells(0, LMZ260G.sprDetailDef.CUST_CD_SS.ColNo).Value = drow("CUST_CD_SS")
            'START YANAI 要望番号558
            .Cells(0, LMZ260G.sprDetailDef.CLOSE_KB.ColNo).Value = String.Empty
            'END YANAI 要望番号558
            'START YANAI 要望番号836
            .Cells(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo).Value = String.Empty
            .Cells(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_REM1.ColNo).Value = String.Empty
            .Cells(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo).Value = String.Empty
            .Cells(0, LMZ260G.sprDetailDef.UNCHIN_TARIFF_REM2.ColNo).Value = String.Empty
            .Cells(0, LMZ260G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Value = String.Empty
            .Cells(0, LMZ260G.sprDetailDef.EXTC_TARIFF_REM.ColNo).Value = String.Empty
            'END YANAI 要望番号836
            '要望番号:1839 terakawa 2013.02.08 Start
            .Cells(0, LMZ260G.sprDetailDef.PICK_LIST_KB.ColNo).Value = String.Empty
            '要望番号:1839 terakawa 2013.02.08 End
            .Cells(0, LMZ260G.sprDetailDef.ROW_INDEX.ColNo).Value = "0"

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()
            '行数設定
            'Dim tbl As DataTable = dtOut.Tables(LMZ260C.TABLE_NM_OUT)
            Dim lngcnt As Integer = dt.Rows.Count
            If lngcnt = 0 Then
                .ResumeLayout(True)
                Exit Sub
            End If

            'キーボード操作でチェックボックスＯＮ
            .KeyboardCheckBoxOn = True

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, sLabel)                            '荷主大名
                .SetCellStyle(i, sprDetailDef.CUST_NM_M.ColNo, sLabel)                            '荷主中名
                .SetCellStyle(i, sprDetailDef.CUST_NM_S.ColNo, sLabel)                            '荷主小名
                .SetCellStyle(i, sprDetailDef.CUST_NM_SS.ColNo, sLabel)                            '荷主極小名
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)                            '荷主大コード
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)                            '荷主中コード
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, sLabel)                            '荷主小コード
                .SetCellStyle(i, sprDetailDef.CUST_CD_SS.ColNo, sLabel)                            '荷主極小コード
                'START YANAI 要望番号558
                .SetCellStyle(i, sprDetailDef.CLOSE_KB.ColNo, sLabel)                             '締日区分
                'END YANAI 要望番号558
                'START YANAI 要望番号836
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_REM1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_REM2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.EXTC_TARIFF_REM.ColNo, sLabel)
                'END YANAI 要望番号836
                '要望番号:1839 terakawa 2013.02.08 Start
                .SetCellStyle(i, sprDetailDef.PICK_LIST_KB.ColNo, sLabel)
                '要望番号:1839 terakawa 2013.02.08 End
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)                            '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, dRow.Item("CUST_NM_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_M.ColNo, dRow.Item("CUST_NM_M").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_S.ColNo, dRow.Item("CUST_NM_S").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_SS.ColNo, dRow.Item("CUST_NM_SS").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dRow.Item("CUST_CD_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dRow.Item("CUST_CD_M").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dRow.Item("CUST_CD_S").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_SS.ColNo, dRow.Item("CUST_CD_SS").ToString)
                'START YANAI 要望番号558
                .SetCellValue(i, sprDetailDef.CLOSE_KB.ColNo, dRow.Item("CLOSE_KB_NM").ToString)
                'END YANAI 要望番号558
                'START YANAI 要望番号836
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, dRow.Item("UNCHIN_TARIFF_CD1").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_REM1.ColNo, dRow.Item("UNCHIN_TARIFF_REM1").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, dRow.Item("UNCHIN_TARIFF_CD2").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_REM2.ColNo, dRow.Item("UNCHIN_TARIFF_REM2").ToString)
                .SetCellValue(i, sprDetailDef.EXTC_TARIFF_CD.ColNo, dRow.Item("EXTC_TARIFF_CD").ToString)
                .SetCellValue(i, sprDetailDef.EXTC_TARIFF_REM.ColNo, dRow.Item("EXTC_TARIFF_REM").ToString)
                '要望番号:1839 terakawa 2013.02.08 Start
                .SetCellValue(i, sprDetailDef.PICK_LIST_KB.ColNo, dRow.Item("PICK_LIST_KB").ToString)
                '要望番号:1839 terakawa 2013.02.08 End
                'END YANAI 要望番号836
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
