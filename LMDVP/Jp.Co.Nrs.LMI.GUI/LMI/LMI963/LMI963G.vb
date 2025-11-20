' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI963G : 荷主自動振分画面(手動)（ハネウェル）
'  作  成  者       :  
' ==========================================================================
Option Strict On
Option Explicit On

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
''' LMI963Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
Public Class LMI963G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMI963F

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMI963F)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 210, True)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_NM_M, "荷主名(中)", 180, True)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_NM_S, "荷主名(小)", 180, True)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_NM_SS, "荷主名(極小)", 180, True)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_CD_L, "荷主コード" & vbCrLf & "(大)", 90, True)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_CD_M, "荷主コード" & vbCrLf & "(中)", 90, True)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_CD_S, "荷主コード" & vbCrLf & "(小)", 90, True)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CUST_CD_SS, "荷主コード" & vbCrLf & "(極小)", 90, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)
        Public Shared CLOSE_KB As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.CLOSE_KB, "締日区分", 80, False)
        Public Shared UNCHIN_TARIFF_CD1 As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.UNCHIN_TARIFF_CD1, "運賃タリフコード（屯キロ建）", 0, False)
        Public Shared UNCHIN_TARIFF_REM1 As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.UNCHIN_TARIFF_REM1, "運賃タリフコード備考（屯キロ建）", 0, False)
        Public Shared UNCHIN_TARIFF_CD2 As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.UNCHIN_TARIFF_CD2, "運賃タリフコード（車建）", 0, False)
        Public Shared UNCHIN_TARIFF_REM2 As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.UNCHIN_TARIFF_REM2, "運賃タリフコード備考（車建）", 0, False)
        Public Shared EXTC_TARIFF_CD As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.EXTC_TARIFF_CD, "割増タリフコード", 0, False)
        Public Shared EXTC_TARIFF_REM As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.EXTC_TARIFF_REM, "割増タリフ備考", 0, False)
        Public Shared PICK_LIST_KB As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.PICK_LIST_KB, "ピッキングリスト区分", 0, False)
        Public Shared WH_CD As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.WH_CD, "倉庫区分", 0, False)
        Public Shared LOAD_NUMBER As SpreadColProperty = New SpreadColProperty(LMI963C.SprColumnIndex.LOAD_NUMBER, "LOAD_NUMBER", 100, True)

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
            .sprDetail.ActiveSheet.ColumnCount = 20

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMI963G.sprDetailDef.DEF.ColNo + 1

            '列設定
            Dim lock As Boolean() = Me.SetLockControl(drow)
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock(LMI963C.LOCK.M)))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_NM_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock(LMI963C.LOCK.S)))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_NM_SS.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, lock(LMI963C.LOCK.SS)))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock(LMI963C.LOCK.M)))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock(LMI963C.LOCK.S)))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CUST_CD_SS.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, lock(LMI963C.LOCK.SS)))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.CLOSE_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.UNCHIN_TARIFF_REM1.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.UNCHIN_TARIFF_REM2.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.EXTC_TARIFF_CD.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.EXTC_TARIFF_REM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMI963G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

            '表示・非表示設定
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CUST_NM_M.ColNo).Visible = Not lock(LMI963C.LOCK.M)
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CUST_NM_S.ColNo).Visible = Not lock(LMI963C.LOCK.S)
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CUST_NM_SS.ColNo).Visible = Not lock(LMI963C.LOCK.SS)
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CUST_CD_M.ColNo).Visible = Not lock(LMI963C.LOCK.M)
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CUST_CD_S.ColNo).Visible = Not lock(LMI963C.LOCK.S)
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CUST_CD_SS.ColNo).Visible = Not lock(LMI963C.LOCK.SS)
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.CLOSE_KB.ColNo).Visible = True
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.UNCHIN_TARIFF_CD1.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.UNCHIN_TARIFF_REM1.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.UNCHIN_TARIFF_CD2.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.UNCHIN_TARIFF_REM2.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.EXTC_TARIFF_CD.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.EXTC_TARIFF_REM.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.PICK_LIST_KB.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.WH_CD.ColNo).Visible = False
            .sprDetail.ActiveSheet.Columns(LMI963G.sprDetailDef.LOAD_NUMBER.ColNo).Visible = False

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
                drow.Item("CUST_CD_M") = LMI963C.OYA_CUST
                drow.Item("CUST_NM_M") = String.Empty
                drow.Item("CUST_CD_S") = LMI963C.OYA_CUST
                drow.Item("CUST_NM_S") = String.Empty
                drow.Item("CUST_CD_SS") = LMI963C.OYA_CUST
                drow.Item("CUST_NM_SS") = String.Empty

                '中、小、極小をロック
                Return New Boolean() {True, True, True}

            Case LMZControlC.HYOJI_S

                '初期値設定
                drow.Item("CUST_CD_S") = LMI963C.OYA_CUST
                drow.Item("CUST_NM_S") = String.Empty
                drow.Item("CUST_CD_SS") = LMI963C.OYA_CUST
                drow.Item("CUST_NM_SS") = String.Empty

                '小、極小をロック
                Return New Boolean() {False, True, True}

            Case LMZControlC.HYOJI_SS

                '初期値設定
                drow.Item("CUST_CD_SS") = LMI963C.OYA_CUST
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
        _Frm.txtCmdGyo.TextValue = drow("ROW_INDEX").ToString
        _Frm.txtLoadNumber.TextValue = drow("LOAD_NUMBER").ToString
        _Frm.txtOutkaDate.TextValue = drow("SHUKKA_DATE").ToString
        _Frm.txtNonyuDate.TextValue = drow("NONYU_DATE").ToString
        _Frm.txtOutkaNm.TextValue = drow("SHUKKA_MOTO").ToString
        _Frm.txtNonyuNm.TextValue = drow("NONYU_SAKI").ToString

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpread = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()
            '行数設定
            'Dim tbl As DataTable = dtOut.Tables(LMI963C.TABLE_NM_OUT)
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
            For i As Integer = 0 To lngcnt - 1

                dRow = dt.Rows(i)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, sLabel)                            '荷主大名
                .SetCellStyle(i, sprDetailDef.CUST_NM_M.ColNo, sLabel)                            '荷主中名
                .SetCellStyle(i, sprDetailDef.CUST_NM_S.ColNo, sLabel)                            '荷主小名
                .SetCellStyle(i, sprDetailDef.CUST_NM_SS.ColNo, sLabel)                           '荷主極小名
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)                            '荷主大コード
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)                            '荷主中コード
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, sLabel)                            '荷主小コード
                .SetCellStyle(i, sprDetailDef.CUST_CD_SS.ColNo, sLabel)                            '荷主極小コード
                .SetCellStyle(i, sprDetailDef.CLOSE_KB.ColNo, sLabel)                             '締日区分
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_REM1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNCHIN_TARIFF_REM2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.EXTC_TARIFF_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.EXTC_TARIFF_REM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PICK_LIST_KB.ColNo, sLabel)
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
                .SetCellValue(i, sprDetailDef.CLOSE_KB.ColNo, dRow.Item("CLOSE_KB_NM").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_CD1.ColNo, dRow.Item("UNCHIN_TARIFF_CD1").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_REM1.ColNo, dRow.Item("UNCHIN_TARIFF_REM1").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_CD2.ColNo, dRow.Item("UNCHIN_TARIFF_CD2").ToString)
                .SetCellValue(i, sprDetailDef.UNCHIN_TARIFF_REM2.ColNo, dRow.Item("UNCHIN_TARIFF_REM2").ToString)
                .SetCellValue(i, sprDetailDef.EXTC_TARIFF_CD.ColNo, dRow.Item("EXTC_TARIFF_CD").ToString)
                .SetCellValue(i, sprDetailDef.EXTC_TARIFF_REM.ColNo, dRow.Item("EXTC_TARIFF_REM").ToString)
                .SetCellValue(i, sprDetailDef.PICK_LIST_KB.ColNo, dRow.Item("PICK_LIST_KB").ToString)
                '.SetCellValue(i, sprDetailDef.WH_CD.ColNo, dRow.Item("WH_CD").ToString)
                '.SetCellValue(i, sprDetailDef.LOAD_NUMBER.ColNo, dRow.Item("LOAD_NUMBER").ToString)
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
