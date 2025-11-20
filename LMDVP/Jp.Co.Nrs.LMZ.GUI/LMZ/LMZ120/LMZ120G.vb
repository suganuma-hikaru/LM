' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ120G : 棟室ゾーン照会
'  作  成  者       :  平山
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
''' LMZ120Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ120G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ120F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ120F, ByVal g As LMZControlG)

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

#Region "Form"


#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared TOU_NO As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.TOU_NO, "棟", 30, True)
        Public Shared SITU_NO As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.SITU_NO, "室", 30, True)
        Public Shared TOU_SITU_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.TOU_SITU_NM, "棟室名", 180, True)
        Public Shared HOZEI_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.HOZEI_KB_NM, "保税(棟室)", 100, True)
        Public Shared HOZEI_KB As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.HOZEI_KB, "保税(棟室)区分", 50, False)
        Public Shared SHOBO_YN As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.SHOBO_YN, "消防", 55, True)
        Public Shared SHOBO_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.SHOBO_YN_NM, "消防区分", 55, False)
        Public Shared ZONE_CD As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_CD, "ZONE", 60, True)
        Public Shared ZONE_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_NM, "ZONE名", 100, True)
        Public Shared ZONE_HOZEI_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_HOZEI_KB_NM, "保税(ZONE)", 100, True)
        Public Shared ZONE_HOZEI_KB As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_HOZEI_KB, "保税(ZONE)区分", 50, False)
        Public Shared ZONE_ONDO_CTL_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_ONDO_CTL_KB_NM, "温度管理" & vbCrLf & "(ZONE)", 70, True)
        Public Shared ZONE_ONDO_CTL_KB As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_ONDO_CTL_KB, "温度管理(ZONE)区分", 50, False)
        Public Shared ZONE_ONDO_CTL_FLG_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_ONDO_CTL_FLG_NM, "温度管理中" & vbCrLf & "(ZONE)", 100, True)
        Public Shared ZONE_ONDO_CTL_FLG As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_ONDO_CTL_FLG, "温度管理中フラグ(ZONE)区分", 40, False)
        Public Shared ZONE_ONDO As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ZONE_ONDO, "設定温度(℃)" & vbCrLf & "(ZONE)", 100, True)
        Public Shared YAKUJI_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.YAKUJI_YN_NM, "薬事", 45, True)
        Public Shared YAKUJI_YN As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.YAKUJI_YN, "薬事区分", 45, False)
        Public Shared DOKU_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.DOKU_YN_NM, "毒劇", 45, True)
        Public Shared DOKU_YN As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.DOKU_YN, "毒劇区分", 45, False)
        Public Shared GASS_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.GASS_YN_NM, "ガス", 45, True)
        Public Shared GASS_YN As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.GASS_YN, "ガス区分", 45, False)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ120C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

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
            .sprDetail.ActiveSheet.ColumnCount = 24

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ120G.sprDetailDef.DEF.ColNo + 1

            Dim umuKbn As StyleInfo = Me._LMZConG.StyleInfoCustCond(.sprDetail)
            Dim flgKbn As StyleInfo = Me._LMZConG.StyleInfoFlg(.sprDetail)


            '列設定
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.TOU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'START YANAI 要望番号705
            '.sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 1, False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.SITU_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'END YANAI 要望番号705
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.TOU_SITU_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.HOZEI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "H001", False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.HOZEI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.SHOBO_YN.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.SHOBO_YN_NM.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            'START YANAI 要望番号705
            '.sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 1, False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA, 2, False))
            'END YANAI 要望番号705
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_HOZEI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "H001", False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_HOZEI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "O002", False))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_FLG_NM.ColNo, flgKbn)
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_FLG.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ZONE_ONDO.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0, 99, True))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.YAKUJI_YN_NM.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.YAKUJI_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.DOKU_YN_NM.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.DOKU_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.GASS_YN_NM.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.GASS_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ120G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

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

            .SetCellValue(0, LMZ120G.sprDetailDef.TOU_NO.ColNo, drow("TOU_NO").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.SITU_NO.ColNo, drow("SITU_NO").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.TOU_SITU_NM.ColNo, drow("TOU_SITU_NM").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.HOZEI_KB_NM.ColNo, drow("HOZEI_KB").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.HOZEI_KB.ColNo, drow("HOZEI_KB").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.SHOBO_YN.ColNo, drow("SHOBO_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.SHOBO_YN_NM.ColNo, drow("SHOBO_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_CD.ColNo, drow("ZONE_CD").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_NM.ColNo, drow("ZONE_NM").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_HOZEI_KB_NM.ColNo, drow("ZONE_HOZEI_KB").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_HOZEI_KB.ColNo, drow("ZONE_HOZEI_KB").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_KB_NM.ColNo, drow("ZONE_ONDO_CTL_KB").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_KB.ColNo, drow("ZONE_ONDO_CTL_KB").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_FLG_NM.ColNo, drow("ZONE_ONDO_CTL_FLG").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_ONDO_CTL_FLG.ColNo, drow("ZONE_ONDO_CTL_FLG").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.ZONE_ONDO.ColNo, String.Empty)
            .SetCellValue(0, LMZ120G.sprDetailDef.YAKUJI_YN_NM.ColNo, drow("YAKUJI_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.YAKUJI_YN.ColNo, drow("YAKUJI_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.DOKU_YN_NM.ColNo, drow("DOKU_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.DOKU_YN.ColNo, drow("DOKU_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.GASS_YN_NM.ColNo, drow("GASS_YN").ToString())
            .SetCellValue(0, LMZ120G.sprDetailDef.GASS_YN.ColNo, drow("GASS_YN").ToString())

        End With

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()
            .cmbSoko.SelectedValue = drow("WH_CD").ToString()

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

            Dim dRow As DataRow = Nothing
            Dim cd As String = String.Empty


            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.TOU_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SITU_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TOU_SITU_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HOZEI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HOZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SHOBO_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SHOBO_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_HOZEI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_HOZEI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_ONDO_CTL_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_ONDO_CTL_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_ONDO_CTL_FLG_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_ONDO_CTL_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZONE_ONDO.ColNo, rLabel)
                .SetCellStyle(i, sprDetailDef.YAKUJI_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.YAKUJI_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DOKU_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.DOKU_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GASS_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GASS_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.TOU_NO.ColNo, dRow.Item("TOU_NO").ToString())
                .SetCellValue(i, sprDetailDef.SITU_NO.ColNo, dRow.Item("SITU_NO").ToString())
                .SetCellValue(i, sprDetailDef.TOU_SITU_NM.ColNo, dRow.Item("TOU_SITU_NM").ToString())
                .SetCellValue(i, sprDetailDef.HOZEI_KB_NM.ColNo, dRow.Item("HOZEI_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.HOZEI_KB.ColNo, dRow.Item("HOZEI_KB").ToString())

                cd = dRow.Item("SHOBO_YN").ToString()
                .SetCellValue(i, sprDetailDef.SHOBO_YN_NM.ColNo, cd)
                .SetCellValue(i, sprDetailDef.SHOBO_YN.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))

                .SetCellValue(i, sprDetailDef.ZONE_CD.ColNo, dRow.Item("ZONE_CD").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_NM.ColNo, dRow.Item("ZONE_NM").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_HOZEI_KB_NM.ColNo, dRow.Item("ZONE_HOZEI_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_HOZEI_KB.ColNo, dRow.Item("ZONE_HOZEI_KB").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_ONDO_CTL_KB_NM.ColNo, dRow.Item("ZONE_ONDO_CTL_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.ZONE_ONDO_CTL_KB.ColNo, dRow.Item("ZONE_ONDO_CTL_KB").ToString())

                cd = dRow.Item("ZONE_ONDO_CTL_FLG").ToString()
                .SetCellValue(i, sprDetailDef.ZONE_ONDO_CTL_FLG.ColNo, cd)
                .SetCellValue(i, sprDetailDef.ZONE_ONDO_CTL_FLG_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))

                .SetCellValue(i, sprDetailDef.ZONE_ONDO.ColNo, dRow.Item("ZONE_ONDO").ToString())


                cd = dRow.Item("YAKUJI_YN").ToString()
                .SetCellValue(i, sprDetailDef.YAKUJI_YN.ColNo, cd)
                .SetCellValue(i, sprDetailDef.YAKUJI_YN_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))

                cd = dRow.Item("DOKU_YN").ToString()
                .SetCellValue(i, sprDetailDef.DOKU_YN.ColNo, cd)
                .SetCellValue(i, sprDetailDef.DOKU_YN_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))

                cd = dRow.Item("GASS_YN").ToString()
                .SetCellValue(i, sprDetailDef.GASS_YN.ColNo, cd)
                .SetCellValue(i, sprDetailDef.GASS_YN_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))

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
