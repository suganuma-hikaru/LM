' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ250G : 運送会社マスタ照会
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
''' LMZ250Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ250G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ250F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ250F, ByVal g As LMZControlG)

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UNSOCO_NM As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.UNSOCO_NM, "会社名", 240, True)
        Public Shared UNSOCO_BR_NM As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.UNSOCO_BR_NM, "支店名", 240, True)
        Public Shared MOTOUKE_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.MOTOUKE_KB_NM, "元請", 50, True)
        Public Shared MOTOUKE_KB As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.MOTOUKE_KB, "元請区分", 50, False)
        Public Shared UNSOCO_CD As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.UNSOCO_CD, "会社" & vbCrLf & "コード", 80, True)
        Public Shared UNSOCO_BR_CD As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.UNSOCO_BR_CD, "支店" & vbCrLf & "コード", 80, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ250C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)
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
            .sprDetail.ActiveSheet.ColumnCount = 8

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ250G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.UNSOCO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.UNSOCO_BR_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.MOTOUKE_KB_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.MOTOUKE_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.UNSOCO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 5, False))
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.UNSOCO_BR_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 3, False))
            .sprDetail.SetCellStyle(0, LMZ250G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

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


        Dim kbnNm As String = String.Empty
        Dim kbnCd As String = drow("MOTOUKE_KB").ToString()

        If String.IsNullOrEmpty(kbnCd) = False Then
            Dim kbnRow As DataRow() = Me._LMZConG.SelectKbnListDataRow(LMKbnConst.KBN_M006, kbnCd)
            If kbnRow.Length < 1 Then
                drow("MOTOUKE_KB") = String.Empty
            Else
                kbnNm = kbnRow(0).Item("KBN_NM1").ToString()
            End If
        End If


        With _Frm.sprDetail.ActiveSheet

            .Cells(0, LMZ250G.sprDetailDef.DEF.ColNo).Value = String.Empty
            .Cells(0, LMZ250G.sprDetailDef.UNSOCO_NM.ColNo).Value = drow("UNSOCO_NM")
            .Cells(0, LMZ250G.sprDetailDef.UNSOCO_BR_NM.ColNo).Value = drow("UNSOCO_BR_NM")
            .Cells(0, LMZ250G.sprDetailDef.MOTOUKE_KB_NM.ColNo).Value = kbnNm
            .Cells(0, LMZ250G.sprDetailDef.MOTOUKE_KB.ColNo).Value = drow("MOTOUKE_KB")
            .Cells(0, LMZ250G.sprDetailDef.UNSOCO_CD.ColNo).Value = drow("UNSOCO_CD")
            .Cells(0, LMZ250G.sprDetailDef.UNSOCO_BR_CD.ColNo).Value = drow("UNSOCO_BR_CD")
            .Cells(0, LMZ250G.sprDetailDef.ROW_INDEX.ColNo).Value = String.Empty

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
            'Dim tbl As DataTable = dtOut.Tables(LMZ250C.TABLE_NM_OUT)
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
                .SetCellStyle(i, sprDetailDef.UNSOCO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSOCO_BR_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MOTOUKE_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MOTOUKE_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSOCO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.UNSOCO_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)                            '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.UNSOCO_NM.ColNo, dRow.Item("UNSOCO_NM").ToString())
                .SetCellValue(i, sprDetailDef.UNSOCO_BR_NM.ColNo, dRow.Item("UNSOCO_BR_NM").ToString())
                .SetCellValue(i, sprDetailDef.MOTOUKE_KB_NM.ColNo, dRow.Item("MOTOUKE_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.MOTOUKE_KB.ColNo, dRow.Item("MOTOUKE_KB").ToString())
                .SetCellValue(i, sprDetailDef.UNSOCO_CD.ColNo, dRow.Item("UNSOCO_CD").ToString())
                .SetCellValue(i, sprDetailDef.UNSOCO_BR_CD.ColNo, dRow.Item("UNSOCO_BR_CD").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With

    End Sub


#End Region 'Spread

    '要望対応:1248 terakawa 2013.03.21 Start
#Region "マイ運送会社"

    ''' <summary>
    ''' マイ運送会社オプションボタンを設定します
    ''' </summary>
    ''' <param name="myUnsocoflg"></param>
    ''' <remarks></remarks>
    Friend Sub SetMyUnsocoOptionButton(ByVal frm As LMZ250F, ByVal myUnsocoflg As Boolean)

        If myUnsocoflg = True Then
            'マイ運送会社オプションボタンを有効有効
            frm.optMyUnsoco.Checked = True
        Else
            '全件オプションボタンを有効
            frm.optAll.Checked = True
        End If

    End Sub


#End Region
    '要望対応:1248 terakawa 2013.03.21 End
#End Region

End Class
