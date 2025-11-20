' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ350G : 真荷主照会
'  作  成  者       :  hori
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
''' LMZ350Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ350G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ350F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ350F)

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

#Region "Form"

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ350C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared BP_NM1 As SpreadColProperty = New SpreadColProperty(LMZ350C.SprColumnIndex.BP_NM1, "名称", 300, True)
        Public Shared BP_CD As SpreadColProperty = New SpreadColProperty(LMZ350C.SprColumnIndex.BP_CD, "コード", 80, True)
        Public Shared COUNTRY_CD As SpreadColProperty = New SpreadColProperty(LMZ350C.SprColumnIndex.COUNTRY_CD, "国", 110, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ350C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow, ByVal dsCombo As DataSet)

        With Me._Frm

            'スプレッドの行をクリア
            .sprDetail.CrearSpread()

            '列数設定
            .sprDetail.ActiveSheet.ColumnCount = LMZ350C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)

            '列固定位置を設定します。(ex.運送会社名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ350G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMZ350G.sprDetailDef.BP_NM1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMZ350G.sprDetailDef.BP_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 10, False))
            .sprDetail.SetCellStyle(0, LMZ350G.sprDetailDef.COUNTRY_CD.ColNo, LMSpreadUtility.GetComboCell(.sprDetail, New DataView(dsCombo.Tables("LMZ350COMBO_COUNTRY")), "COUNTRY_CD", "COUNTRY_ENM", False))
            .sprDetail.SetCellStyle(0, LMZ350G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

            '初期値設定
            .sprDetail.SetCellValue(0, sprDetailDef.COUNTRY_CD.ColNo, drow.Item("COUNTRY_CD").ToString)

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
                .SetCellStyle(i, sprDetailDef.BP_NM1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BP_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.COUNTRY_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.BP_NM1.ColNo, dRow.Item("BP_NM1").ToString())
                .SetCellValue(i, sprDetailDef.BP_CD.ColNo, dRow.Item("BP_CD").ToString())
                .SetCellValue(i, sprDetailDef.COUNTRY_CD.ColNo, dRow.Item("COUNTRY_ENM").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#End Region

End Class
