' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ021G : 商品マスタ(在庫)照会
'  作  成  者       :  Annen
' ==========================================================================
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Com.Utility
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.Win.Utility.Spread
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Utility.Spread

''' <summary>
''' LMZ021Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ021G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ021F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ021F)

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

#Region "Form"

    ''' <summary>
    ''' 画面ヘッダー部値設定
    ''' </summary>
    ''' <param name="drow">荷主キャッシュから取得したデータロウ配列</param>
    ''' <remarks></remarks>
    Friend Sub CustHeaderDataSet(ByVal drow As DataRow)

        With _Frm

            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustNmL.TextValue = drow("CUST_NM_L").ToString()
            .lblCustCdM.TextValue = drow("CUST_CD_M").ToString()
            .lblCustNmM.TextValue = drow("CUST_NM_M").ToString()

        End With

    End Sub

    ''' <summary>
    ''' 画面ヘッダー部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            Me.SetLockControl(.lblCustCdL, lock)
            Me.SetLockControl(.lblCustNmL, lock)
            Me.SetLockControl(.lblCustCdM, lock)
            Me.SetLockControl(.lblCustNmM, lock)
            .sprDetail.Enabled = Not lock

        End With

    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared GOODS_NM_1 As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.GOODS_NM_1, "正式名", 300, True)
        Public Shared GOODS_CD_CUST As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.GOODS_CD_CUST, "商品コード", 150, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.LOT_NO, "ロット№", 100, True)
        '2017.09.27 annen AXL出荷登録改善対応 START
        Public Shared ALLOC_CAN_NB As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.ALLOC_CAN_NB, "引当可能個数", 100, True)
        '2017.09.27 annen AXL出荷登録改善対応 END
        Public Shared STD_IRIME_NB As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.STD_IRIME_NB, "標準入目", 100, True)
        Public Shared STD_IRIME_UT_NM As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.STD_IRIME_UT_NM, "入目単位", 100, True)
        'Public Shared STD_IRIME_NM As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.STD_IRIME_NM, "入目", 150, True)
        Public Shared NB_UT_NM As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.NB_UT_NM, "個数単位", 120, True)
        Public Shared NB_UT As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.NB_UT, "個数単位区分値", 70, False)
        Public Shared PKG_NM As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.PKG_NM, "入数", 120, True)
        Public Shared SEARCH_KEY_1 As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.SEARCH_KEY_1, "荷主カテゴリ1", 150, True)
        Public Shared SEARCH_KEY_2 As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.SEARCH_KEY_2, "荷主カテゴリ2", 150, True)
        Public Shared ONDO_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.ONDO_KB_NM, "温度管理", 70, True)
        Public Shared ONDO_KB As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.ONDO_KB, "温度区分値", 50, False)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.SHOBO_CD, "消防", 40, True)
        Public Shared CUST_NM_S As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_NM_S, "荷主名(小)", 150, True)
        Public Shared CUST_NM_SS As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_NM_SS, "荷主名(極小)", 150, True)
        Public Shared STD_IRIME_UT As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.STD_IRIME_UT, "標準入目単位", 150, False)
        Public Shared PKG_NB As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.PKG_NB, "包装個数", 150, False)
        Public Shared PKG_UT As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.PKG_UT, "包装単位", 150, False)
        Public Shared PKG_UT_NM As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.PKG_UT_NM, "包装単位名称", 150, False)
        Public Shared CUST_CD_S As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_CD_S, "荷主コード(小)", 150, False)
        Public Shared CUST_CD_SS As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_CD_SS, "荷主コード(極小)", 150, False)
        Public Shared CUST_CD_L As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_CD_L, "荷主コード(大)", 150, False)
        Public Shared CUST_CD_M As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_CD_M, "荷主コード(中)", 150, False)
        Public Shared CUST_NM_L As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_NM_L, "荷主名(大)", 150, False)
        Public Shared CUST_NM_M As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.CUST_NM_M, "荷主名(中)", 150, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.GOODS_CD_NRS, "商品KEY", 150, False)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ021C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

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
            .sprDetail.Sheets(0).ColumnCount = 29

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)

            '列固定位置を設定します。(ex.商品名で固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.GOODS_NM_1.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.GOODS_NM_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 60, False)) '検証結果_導入時要望 №62対応(2011.09.13)　
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.GOODS_CD_CUST.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 20, False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.LOT_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 40, False))
            '2017.09.27 annen AXL出荷登録改善対応 START
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.ALLOC_CAN_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            '2017.09.27 annen AXL出荷登録改善対応 END
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.STD_IRIME_NB.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 999999.999, False, 3, True, ","))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.STD_IRIME_UT_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.NB_UT_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "K002", False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.NB_UT.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 2, True))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.PKG_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 30, True))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.SEARCH_KEY_1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 25, False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.SEARCH_KEY_2.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX_IME_OFF, 25, False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.ONDO_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "O002", False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.ONDO_KB.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 2, False))             '隠し
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.SHOBO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 3, False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_NM_S.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_NM_SS.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.STD_IRIME_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.PKG_NB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.PKG_UT.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.PKG_UT_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_CD_S.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_CD_SS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_CD_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_CD_M.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_NM_L.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.CUST_NM_M.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.GOODS_CD_NRS.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ021G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

        End With


        Call Me.SetInitValue(drow)


    End Sub


    ''' <summary>
    ''' 画面初期値設定(スプレッド)
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        With Me._Frm.sprDetail

            .SetCellValue(0, LMZ021G.sprDetailDef.GOODS_NM_1.ColNo, drow("GOODS_NM_1").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.GOODS_CD_CUST.ColNo, drow("GOODS_CD_CUST").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.LOT_NO.ColNo, drow("LOT_NO").ToString())
            '2017.09.27 annen AXL出荷登録改善対応 START
            '.SetCellValue(0, LMZ021G.sprDetailDef.ALLOC_CAN_NB.ColNo, drow("ALLOC_CAN_NB").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.ALLOC_CAN_NB.ColNo, String.Empty)
            '2017.09.27 annen AXL出荷登録改善対応 END
            .SetCellValue(0, LMZ021G.sprDetailDef.STD_IRIME_NB.ColNo, drow("IRIME").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.STD_IRIME_UT_NM.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.NB_UT_NM.ColNo, drow("NB_UT").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.NB_UT.ColNo, drow("NB_UT").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.PKG_NM.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.SEARCH_KEY_1.ColNo, drow("SEARCH_KEY_1").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.SEARCH_KEY_2.ColNo, drow("SEARCH_KEY_2").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.ONDO_KB_NM.ColNo, drow("ONDO_KB").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.ONDO_KB.ColNo, drow("ONDO_KB").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.SHOBO_CD.ColNo, drow("SHOBO_CD").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_NM_S.ColNo, drow("CUST_NM_S").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_NM_SS.ColNo, drow("CUST_NM_SS").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.STD_IRIME_UT.ColNo, drow("IRIME_UT").ToString())
            .SetCellValue(0, LMZ021G.sprDetailDef.PKG_NB.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.PKG_UT.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.PKG_UT_NM.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_CD_S.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_CD_SS.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_CD_L.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_CD_M.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_NM_L.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.CUST_NM_M.ColNo, String.Empty)
            .SetCellValue(0, LMZ021G.sprDetailDef.GOODS_CD_NRS.ColNo, String.Empty)


        End With

        With _Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()
            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustCdM.TextValue = drow("CUST_CD_M").ToString()

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
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)

            '数値型を設定（小数点以下を固定表示するため）
            Dim tNumCell As New FarPoint.Win.Spread.CellType.NumberCellType()
            tNumCell.ShowSeparator = True   'セパレータ表示する(おまけ)
            tNumCell.DecimalPlaces = 3      '小数点以下３桁
            tNumCell.FixedPoint = True      '小数点以下を固定表示(必ず0.000と表示する)

            Dim dRow As DataRow = Nothing

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.GOODS_NM_1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_CUST.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ALLOC_CAN_NB.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.STD_IRIME_NB.ColNo, rLabel)
                '数値型を設定（小数点以下を固定表示するため）
                .ActiveSheet.Cells(i, sprDetailDef.STD_IRIME_NB.ColNo).CellType = tNumCell
                .SetCellStyle(i, sprDetailDef.STD_IRIME_UT_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NB_UT_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NB_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PKG_NM.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.SEARCH_KEY_1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEARCH_KEY_2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ONDO_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_NM_S.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_NM_SS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.STD_IRIME_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PKG_NB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PKG_UT.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_S.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_SS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_CD_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_NM_L.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.CUST_NM_M.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.GOODS_NM_1.ColNo, dRow.Item("GOODS_NM_1").ToString)
                .SetCellValue(i, sprDetailDef.GOODS_CD_CUST.ColNo, dRow.Item("GOODS_CD_CUST").ToString)
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString)
                .SetCellValue(i, sprDetailDef.ALLOC_CAN_NB.ColNo, Convert.ToInt32(dRow.Item("ALLOC_CAN_NB")).ToString("#,0"))
                '数値型でセットする
                '.SetCellValue(i, sprDetailDef.STD_IRIME_NB.ColNo, dRow.Item("STD_IRIME_NB").ToString)
                .ActiveSheet.SetValue(i, sprDetailDef.STD_IRIME_NB.ColNo, Convert.ToDouble(dRow.Item("STD_IRIME_NB")), False)
                .SetCellValue(i, sprDetailDef.STD_IRIME_UT_NM.ColNo, dRow.Item("STD_IRIME_UT_NM").ToString)
                .SetCellValue(i, sprDetailDef.NB_UT_NM.ColNo, dRow.Item("NB_UT_NM").ToString)
                .SetCellValue(i, sprDetailDef.NB_UT.ColNo, dRow.Item("NB_UT").ToString)
                .SetCellValue(i, sprDetailDef.PKG_NM.ColNo, String.Concat((dRow.Item("PKG_NB").ToString), " ", (dRow.Item("PKG_UT_NM").ToString)))
                .SetCellValue(i, sprDetailDef.SEARCH_KEY_1.ColNo, dRow.Item("SEARCH_KEY_1").ToString)
                .SetCellValue(i, sprDetailDef.SEARCH_KEY_2.ColNo, dRow.Item("SEARCH_KEY_2").ToString)
                .SetCellValue(i, sprDetailDef.ONDO_KB_NM.ColNo, dRow.Item("ONDO_KB_NM").ToString)
                .SetCellValue(i, sprDetailDef.ONDO_KB.ColNo, dRow.Item("ONDO_KB").ToString)
                .SetCellValue(i, sprDetailDef.SHOBO_CD.ColNo, dRow.Item("SHOBO_CD").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_S.ColNo, dRow.Item("CUST_NM_S").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_SS.ColNo, dRow.Item("CUST_NM_SS").ToString)
                .SetCellValue(i, sprDetailDef.STD_IRIME_UT.ColNo, dRow.Item("STD_IRIME_UT").ToString)
                .SetCellValue(i, sprDetailDef.PKG_NB.ColNo, dRow.Item("PKG_NB").ToString)
                .SetCellValue(i, sprDetailDef.PKG_UT.ColNo, dRow.Item("PKG_UT").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_S.ColNo, dRow.Item("CUST_CD_S").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_SS.ColNo, dRow.Item("CUST_CD_SS").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_L.ColNo, dRow.Item("CUST_CD_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_CD_M.ColNo, dRow.Item("CUST_CD_M").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_L.ColNo, dRow.Item("CUST_NM_L").ToString)
                .SetCellValue(i, sprDetailDef.CUST_NM_M.ColNo, dRow.Item("CUST_NM_M").ToString)
                .SetCellValue(i, sprDetailDef.GOODS_CD_NRS.ColNo, dRow.Item("GOODS_CD_NRS").ToString)
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))
            Next

            .ResumeLayout(True)

        End With


    End Sub

#End Region 'Spread

#Region "部品"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

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

#End Region

#End Region

End Class
