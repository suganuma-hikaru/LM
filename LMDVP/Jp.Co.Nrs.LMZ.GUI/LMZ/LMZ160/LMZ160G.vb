' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ160F :　乗務員マスタ照会
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
''' LMZ160Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ160G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ160F

    ''' <summary>
    ''' 画面クラスを格納するフィールド
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMZConG As LMZControlG

#End Region 'Field

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="handlerClass">このクラスを生成したハンドラクラス</param>
    ''' <param name="frm">このクラスが設定するコントロールを持つフォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ160F, ByVal g As LMZControlG)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        Me.MyHandler = handlerClass

        '画面構築をするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

        Me._LMZConG = g

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared DRIVER_CD As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.DRIVER_CD, "コード", 60, True)                              '乗務員コード
        Public Shared DRIVER_NM As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.DRIVER_NM, "乗務員名", 280, True)                           '乗務員氏名
        Public Shared AVAL_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.AVAL_YN, "使用可能フラグ", 80, False)                         '使用可能フラグ
        Public Shared LCAR_LICENSE_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.LCAR_LICENSE_YN, "大型免許の有無", 80, False)         '大型免許の有無
        Public Shared TRAILER_LICENSE_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.TRAILER_LICENSE_YN, "けん引免許の有無", 80, False) 'けん引免許の有無
        Public Shared OTSU1_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.OTSU1_YN, "危険物乙1類の有無", 80, False)                    '危険物乙1類の有無
        Public Shared OTSU2_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.OTSU2_YN, "危険物乙2類の有無", 80, False)                    '危険物乙2類の有無
        Public Shared OTSU3_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.OTSU3_YN, "危険物乙3類の有無", 80, False)                    '危険物乙3類の有無
        Public Shared OTSU4_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.OTSU4_YN, "危険物乙4類の有無", 80, False)                    '危険物乙4類の有無
        Public Shared OTSU5_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.OTSU5_YN, "危険物乙5類の有無", 80, False)                    '危険物乙5類の有無
        Public Shared OTSU6_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.OTSU6_YN, "危険物乙6類の有無", 80, False)                    '危険物乙6類の有無
        Public Shared HICOMPGAS_YN As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.HICOMPGAS_YN, "高圧ガス移動監視者の有無", 80, False)     '高圧ガス移動監視者の有無
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ160C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDriver.CrearSpread()

            '列数設定
            .sprDriver.Sheets(0).ColumnCount = 14

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDriver.SetColProperty(New sprDetailDef)
            .sprDriver.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列設定
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.DRIVER_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDriver, InputControl.HAN_NUM_ALPHA, 5, False))      '乗務員コード
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.DRIVER_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDriver, InputControl.ALL_MIX, 20, False))           '乗務員氏名
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.AVAL_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.LCAR_LICENSE_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.TRAILER_LICENSE_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.OTSU1_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.OTSU2_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.OTSU3_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.OTSU4_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.OTSU5_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.OTSU6_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.HICOMPGAS_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver, CellHorizontalAlignment.Left))
            .sprDriver.SetCellStyle(0, LMZ160G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDriver))

        End With

        Call Me.SetInitValue(drow)

    End Sub

    ''' <summary>
    ''' 営業所コンボボックス初期値とスプレッド初期値を設定します
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        _Frm.cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD")

        With _Frm.sprDriver.ActiveSheet

            .Cells(0, LMZ160G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.DRIVER_CD.ColNo).Value = drow("DRIVER_CD")
            .Cells(0, LMZ160G.sprDetailDef.DRIVER_NM.ColNo).Value = drow("DRIVER_NM")
            .Cells(0, LMZ160G.sprDetailDef.AVAL_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.LCAR_LICENSE_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.TRAILER_LICENSE_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.OTSU1_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.OTSU2_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.OTSU3_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.OTSU4_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.OTSU5_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.OTSU6_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.HICOMPGAS_YN.ColNo).Value = String.Empty
            .Cells(0, LMZ160G.sprDetailDef.ROW_INDEX.ColNo).Value = String.Empty

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDriver
        Dim dtOut As DataSet = New DataSet()

        With spr

            'SPREAD(表示行)初期化
            .CrearSpread()

            .SuspendLayout()
            '行数設定
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
                .SetCellStyle(i, sprDetailDef.DRIVER_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DRIVER_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.AVAL_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LCAR_LICENSE_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TRAILER_LICENSE_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OTSU1_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OTSU2_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OTSU3_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OTSU4_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OTSU5_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.OTSU6_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HICOMPGAS_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)                            '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.DRIVER_CD.ColNo, dRow.Item("DRIVER_CD").ToString())
                .SetCellValue(i, sprDetailDef.DRIVER_NM.ColNo, dRow.Item("DRIVER_NM").ToString())
                .SetCellValue(i, sprDetailDef.AVAL_YN.ColNo, dRow.Item("AVAL_YN").ToString())
                .SetCellValue(i, sprDetailDef.LCAR_LICENSE_YN.ColNo, dRow.Item("LCAR_LICENSE_YN").ToString())
                .SetCellValue(i, sprDetailDef.TRAILER_LICENSE_YN.ColNo, dRow.Item("TRAILER_LICENSE_YN").ToString())
                .SetCellValue(i, sprDetailDef.OTSU1_YN.ColNo, dRow.Item("OTSU1_YN").ToString())
                .SetCellValue(i, sprDetailDef.OTSU2_YN.ColNo, dRow.Item("OTSU2_YN").ToString())
                .SetCellValue(i, sprDetailDef.OTSU3_YN.ColNo, dRow.Item("OTSU3_YN").ToString())
                .SetCellValue(i, sprDetailDef.OTSU4_YN.ColNo, dRow.Item("OTSU4_YN").ToString())
                .SetCellValue(i, sprDetailDef.OTSU5_YN.ColNo, dRow.Item("OTSU5_YN").ToString())
                .SetCellValue(i, sprDetailDef.OTSU6_YN.ColNo, dRow.Item("OTSU6_YN").ToString())
                .SetCellValue(i, sprDetailDef.HICOMPGAS_YN.ColNo, dRow.Item("HICOMPGAS_YN").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
