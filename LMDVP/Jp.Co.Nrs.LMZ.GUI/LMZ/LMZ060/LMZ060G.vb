' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ060 : 郵便番号マスタ照会
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
''' LMZ060Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ060G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ060F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ060F, ByVal g As LMZControlG)

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared ZIP_NO As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.ZIP_NO, "郵便番号", 80, True)    '郵便番号
        Public Shared KEN_N As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.KEN_N, "都道府県名", 100, True)       '都道府県名
        Public Shared CITY_N As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.CITY_N, "市区町村名", 210, True)     '市区町村名
        Public Shared TOWN_N As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.TOWN_N, "町域名", 220, True)         '町域名
        Public Shared JIS_CD As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.JIS_CD, "JISコード", 84, True)   'JISコード
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ060C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

    End Class

    ''' <summary>
    ''' スプレッドの初期設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub InitSpread(ByVal drow As DataRow)

        With Me._Frm

            'スプレッドの行をクリア
            .sprZip.CrearSpread()

            '列数設定
            .sprZip.Sheets(0).ColumnCount = 7

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprZip.SetColProperty(New sprDetailDef)
            .sprZip.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprZip.ActiveSheet.FrozenColumnCount = LMZ060G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprZip.SetCellStyle(0, LMZ060G.sprDetailDef.ZIP_NO.ColNo, LMSpreadUtility.GetTextCell(.sprZip, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprZip.SetCellStyle(0, LMZ060G.sprDetailDef.KEN_N.ColNo, Me.SetComboKen())
            .sprZip.SetCellStyle(0, LMZ060G.sprDetailDef.CITY_N.ColNo, LMSpreadUtility.GetTextCell(.sprZip, InputControl.ALL_MIX, 20, False))
            .sprZip.SetCellStyle(0, LMZ060G.sprDetailDef.TOWN_N.ColNo, LMSpreadUtility.GetTextCell(.sprZip, InputControl.ALL_MIX, 100, False))
            .sprZip.SetCellStyle(0, LMZ060G.sprDetailDef.JIS_CD.ColNo, LMSpreadUtility.GetTextCell(.sprZip, InputControl.HAN_NUM_ALPHA, 7, False))
            .sprZip.SetCellStyle(0, LMZ060G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprZip))

        End With

        Call Me.SetInitValue(drow)

    End Sub

    ''' <summary>
    ''' スプレッド初期値を設定します
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        With _Frm.sprZip

            .SetCellValue(0, LMZ060G.sprDetailDef.ZIP_NO.ColNo, drow("ZIP_NO").ToString())
            .SetCellValue(0, LMZ060G.sprDetailDef.KEN_N.ColNo, drow("KEN_N").ToString())
            .SetCellValue(0, LMZ060G.sprDetailDef.CITY_N.ColNo, drow("CITY_N").ToString())
            .SetCellValue(0, LMZ060G.sprDetailDef.TOWN_N.ColNo, drow("TOWN_N").ToString())
            .SetCellValue(0, LMZ060G.sprDetailDef.JIS_CD.ColNo, drow("JIS_CD").ToString())

        End With

    End Sub

    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprZip
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
                .SetCellStyle(i, sprDetailDef.ZIP_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KEN_N.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CITY_N.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TOWN_N.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.JIS_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.ZIP_NO.ColNo, dRow.Item("ZIP_NO").ToString())
                .SetCellValue(i, sprDetailDef.KEN_N.ColNo, dRow.Item("KEN_N").ToString())
                .SetCellValue(i, sprDetailDef.CITY_N.ColNo, dRow.Item("CITY_N").ToString())
                .SetCellValue(i, sprDetailDef.TOWN_N.ColNo, dRow.Item("TOWN_N").ToString())
                .SetCellValue(i, sprDetailDef.JIS_CD.ColNo, dRow.Item("JIS_CD").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' スプレッド都道府県コンボボックス設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetComboKen() As StyleInfo

        Dim sort As String = "KEN_CD"

        Dim getDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KEN)
        getDt.Rows.Clear()

        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KEN).Select("SYS_DEL_FLG = '0'", sort)

        Dim item As String = String.Empty
        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            item = getDr(i).Item("KEN_NM").ToString()
            getDt.ImportRow(getDr(i))

        Next

        Dim cmb As StyleInfo = LMSpreadUtility.GetComboCell(Me._Frm.sprZip, New DataView(getDt), "KEN_NM", "KEN_NM", False)

        Return cmb

    End Function


#End Region 'Spread

#End Region

End Class
