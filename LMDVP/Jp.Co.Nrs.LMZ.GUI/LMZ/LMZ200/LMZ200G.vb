' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ200G : 商品マスタ照会
'  作  成  者       :  kishi
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
''' LMZ200Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ200G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ200F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ200F, ByVal g As LMZControlG)

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

    ''' <summary>
    ''' 画面ヘッダー部値設定
    ''' </summary>
    ''' <param name="drow">荷主キャッシュから取得したデータロウ配列</param>
    ''' <remarks></remarks>
    Friend Sub CustHeaderDataSet(ByVal drow As DataRow)

        With _Frm

            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()
            .lblCustNmL.TextValue = drow("CUST_NM_L").ToString()

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SAGYO_NM As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.SAGYO_NM, "作業項目名", 300, True)
        Public Shared SAGYO_RYAK As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.SAGYO_RYAK, "略称", 100, True)
        Public Shared INV_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.INV_YN_NM, "請求", 40, True)
        Public Shared INV_YN As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.INV_YN, "請求区分", 20, False)
        Public Shared FLWP_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.FLWP_YN_NM, "進捗", 40, True)
        Public Shared FLWP_YN As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.FLWP_YN, "進捗区分", 20, False)
        Public Shared INV_TANI_NM As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.INV_TANI_NM, "請求単位", 80, True)
        Public Shared INV_TANI As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.INV_TANI, "請求単位区分", 20, False)
        Public Shared SAGYO_UP As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.SAGYO_UP, "単価", 120, True)
        Public Shared ZEI_KBN_NM As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.ZEI_KBN_NM, "課税区分", 80, True)
        Public Shared ZEI_KBN As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.ZEI_KBN, "課税区分(区分値)", 20, False)
        Public Shared SAGYO_REMARK As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.SAGYO_REMARK, "備考", 150, True)
        Public Shared SAGYO_CD As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.SAGYO_CD, "作業コード", 90, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)
        Public Shared WH_SAGYO_YN_NM As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.WH_SAGYO_YN_NM, "現場作業", 60, True)
        Public Shared WH_SAGYO_YN As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.WH_SAGYO_YN, "現場作業有無(区分値)", 20, False)
        Public Shared WH_SAGYO_REMARK As SpreadColProperty = New SpreadColProperty(LMZ200C.SprColumnIndex.WH_SAGYO_REMARK, "現場作業備考", 150, True)

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
            .sprDetail.Sheets(0).ColumnCount = 18

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ200G.sprDetailDef.DEF.ColNo + 1


            Dim umuKbn As StyleInfo = Me._LMZConG.StyleInfoCustCond(.sprDetail)

            '列設定
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.SAGYO_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 60, False))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.SAGYO_RYAK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 6, False))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.INV_YN_NM.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.INV_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.FLWP_YN_NM.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.FLWP_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.INV_TANI_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S027", False))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.INV_TANI.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.SAGYO_UP.ColNo, LMSpreadUtility.GetNumberCell(.sprDetail, 0.0, 999999999.999, True))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.ZEI_KBN_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "Z001", False))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.ZEI_KBN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.SAGYO_REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.SAGYO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 5, False))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.WH_SAGYO_YN_NM.ColNo, umuKbn)
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.WH_SAGYO_YN.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ200G.sprDetailDef.WH_SAGYO_REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))

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

            .SetCellValue(0, LMZ200G.sprDetailDef.SAGYO_NM.ColNo, drow("SAGYO_NM").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.SAGYO_RYAK.ColNo, drow("SAGYO_RYAK").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.INV_YN_NM.ColNo, drow("INV_YN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.INV_YN.ColNo, drow("INV_YN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.FLWP_YN_NM.ColNo, drow("FLWP_YN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.FLWP_YN.ColNo, drow("FLWP_YN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.INV_TANI_NM.ColNo, drow("INV_TANI").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.INV_TANI.ColNo, drow("INV_TANI").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.SAGYO_UP.ColNo, String.Empty)
            .SetCellValue(0, LMZ200G.sprDetailDef.ZEI_KBN_NM.ColNo, drow("ZEI_KBN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.ZEI_KBN.ColNo, drow("ZEI_KBN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.SAGYO_REMARK.ColNo, drow("SAGYO_REMARK").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.SAGYO_CD.ColNo, drow("SAGYO_CD").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.WH_SAGYO_YN_NM.ColNo, drow("WH_SAGYO_YN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.WH_SAGYO_YN.ColNo, drow("WH_SAGYO_YN").ToString())
            .SetCellValue(0, LMZ200G.sprDetailDef.WH_SAGYO_REMARK.ColNo, drow("WH_SAGYO_REMARK").ToString())


        End With

        With Me._Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()
            .lblCustCdL.TextValue = drow("CUST_CD_L").ToString()

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable)

        Dim spr As LMSpreadSearch = Me._Frm.sprDetail
        Dim dtOut As DataSet = New DataSet()
        Dim lock As Boolean = True

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

            .ActiveSheet.AddRows(.ActiveSheet.Rows.Count, lngcnt)

            'セルに設定するスタイルの取得
            Dim sDEF As StyleInfo = LMSpreadUtility.GetCheckBoxCell(spr, False)
            Dim sLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Left)
            Dim rLabel As StyleInfo = LMSpreadUtility.GetLabelCell(spr, CellHorizontalAlignment.Right)
            Dim num12 As StyleInfo = Me.StyleInfoNum12dec3(spr, Lock)
            Dim dRow As DataRow = Nothing
            Dim cd As String = String.Empty

            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定
                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.SAGYO_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_RYAK.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INV_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INV_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FLWP_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.FLWP_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INV_TANI_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.INV_TANI.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_UP.ColNo, num12)
                .SetCellStyle(i, sprDetailDef.ZEI_KBN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZEI_KBN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SAGYO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号
                .SetCellStyle(i, sprDetailDef.WH_SAGYO_YN_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.WH_SAGYO_YN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.WH_SAGYO_REMARK.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.SAGYO_NM.ColNo, dRow.Item("SAGYO_NM").ToString)
                .SetCellValue(i, sprDetailDef.SAGYO_RYAK.ColNo, dRow.Item("SAGYO_RYAK").ToString)


                cd = dRow.Item("INV_YN").ToString()
                .SetCellValue(i, sprDetailDef.INV_YN.ColNo, cd)
                .SetCellValue(i, sprDetailDef.INV_YN_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))

                cd = dRow.Item("FLWP_YN").ToString()
                .SetCellValue(i, sprDetailDef.FLWP_YN.ColNo, cd)
                .SetCellValue(i, sprDetailDef.FLWP_YN_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))


                .SetCellValue(i, sprDetailDef.INV_TANI_NM.ColNo, dRow.Item("INV_TANI_NM").ToString)
                .SetCellValue(i, sprDetailDef.INV_TANI.ColNo, dRow.Item("INV_TANI").ToString)
                .SetCellValue(i, sprDetailDef.SAGYO_UP.ColNo, dRow.Item("SAGYO_UP").ToString)
                .SetCellValue(i, sprDetailDef.ZEI_KBN_NM.ColNo, dRow.Item("ZEI_KBN_NM").ToString)
                .SetCellValue(i, sprDetailDef.ZEI_KBN.ColNo, dRow.Item("ZEI_KBN").ToString)
                .SetCellValue(i, sprDetailDef.SAGYO_REMARK.ColNo, dRow.Item("SAGYO_REMARK").ToString)
                .SetCellValue(i, sprDetailDef.SAGYO_CD.ColNo, dRow.Item("SAGYO_CD").ToString)
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))


                cd = dRow.Item("WH_SAGYO_YN").ToString()
                .SetCellValue(i, sprDetailDef.WH_SAGYO_YN.ColNo, cd)
                .SetCellValue(i, sprDetailDef.WH_SAGYO_YN_NM.ColNo, Me._LMZConG.SpreadMaruBatsu(LMZControlC.KBNCD1, cd))
                .SetCellValue(i, sprDetailDef.WH_SAGYO_REMARK.ColNo, dRow.Item("WH_SAGYO_REMARK").ToString)

            Next

            .ResumeLayout(True)

        End With


    End Sub

#End Region 'Spread

#Region "部品"

    ''' <summary>
    ''' セルのプロパティを設定(Number 整数12桁　少数3桁)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="lock">ロックフラグ　Trueの場合ロック</param>
    ''' <returns>セルタイプ</returns>
    ''' <remarks></remarks>
    Private Function StyleInfoNum12dec3(ByVal spr As LMSpread, ByVal lock As Boolean) As StyleInfo

        Return LMSpreadUtility.GetNumberCell(spr, 0, 999999999999.999, lock, 3, , ",")

    End Function
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
