' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ190G : 請求項目マスタ照会
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
''' LMZ190Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ190G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ190F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ190F, ByVal g As LMZControlG)

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

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.DEF, " ", 20, True)
        '2014.09.12 追加START 多通貨対応
        Public Shared COUNTRY_CD As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.COUNTRY_CD, "国" & vbCrLf & "コード", 50, True)
        '2014.09.12 追加END 多通貨対応
        Public Shared GROUP_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.GROUP_KB_NM, "グループ" & vbCrLf & "コード", 90, True)
        Public Shared GROUP_KB As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.GROUP_KB, "グループコード区分", 50, False)
        Public Shared SEIQKMK_NM As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.SEIQKMK_NM, "請求項目名", 230, True)
        Public Shared TAX_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.TAX_KB_NM, "税区分", 60, True)
        Public Shared TAX_KB As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.TAX_KB, "税区分(区分値)", 50, False)
        Public Shared REMARK As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.REMARK, "備考", 200, True)
        Public Shared SEIQKMK_CD As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.SEIQKMK_CD, "項目" & vbCrLf & "コード", 70, True)
        Public Shared SEIQKMK_CD_S As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.SEIQKMK_CD_S, "項目" & vbCrLf & "CD小", 50, True)
        Public Shared KEIRI_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.KEIRI_KB_NM, "経理科目" & vbCrLf & "コード", 140, True)
        Public Shared KEIRI_KB As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.KEIRI_KB, "経理科目コード区分", 50, False)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ190C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

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
            '2014.09.12 修正START 多通貨対応
            .sprDetail.ActiveSheet.ColumnCount = 13
            '2014.09.12 修正END 多通貨対応

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ190G.sprDetailDef.DEF.ColNo + 1

            '区分名３を取得
            Dim zeikbn As StyleInfo = Me._LMZConG.StyleInfoTax(.sprDetail)

            '列設定
            '2014.09.12 追加START 多通貨対応
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.COUNTRY_CD.ColNo, Me.SetComboCountry())
            '2014.09.12 追加END 多通貨対応
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.GROUP_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S024", False))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.GROUP_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.SEIQKMK_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.TAX_KB_NM.ColNo, zeikbn)
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.TAX_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.REMARK.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.SEIQKMK_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 2, False))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.SEIQKMK_CD_S.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.KEIRI_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "K016", False))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.KEIRI_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ190G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

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

            '2014.09.12 追加START 多通貨対応
            .SetCellValue(0, LMZ190G.sprDetailDef.COUNTRY_CD.ColNo, drow("COUNTRY_CD").ToString())
            '2014.09.12 追加END 多通貨対応
            .SetCellValue(0, LMZ190G.sprDetailDef.GROUP_KB_NM.ColNo, drow("GROUP_KB").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.GROUP_KB.ColNo, drow("GROUP_KB").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.SEIQKMK_NM.ColNo, drow("SEIQKMK_NM").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.TAX_KB_NM.ColNo, drow("TAX_KB").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.TAX_KB.ColNo, drow("TAX_KB").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.REMARK.ColNo, drow("REMARK").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.SEIQKMK_CD_S.ColNo, drow("SEIQKMK_CD_S").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.KEIRI_KB_NM.ColNo, drow("KEIRI_KB").ToString())
            .SetCellValue(0, LMZ190G.sprDetailDef.KEIRI_KB.ColNo, drow("KEIRI_KB").ToString())

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
                '2014.09.12 追加START 多通貨対応
                .SetCellStyle(i, sprDetailDef.COUNTRY_CD.ColNo, sLabel)
                '2014.09.12 追加END 多通貨対応
                .SetCellStyle(i, sprDetailDef.GROUP_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GROUP_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEIQKMK_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TAX_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.TAX_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.REMARK.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEIQKMK_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEIQKMK_CD_S.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KEIRI_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KEIRI_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                '2014.09.12 追加START 多通貨対応
                .SetCellValue(i, sprDetailDef.COUNTRY_CD.ColNo, dRow.Item("COUNTRY_CD").ToString())
                '2014.09.12 追加END 多通貨対応
                .SetCellValue(i, sprDetailDef.GROUP_KB_NM.ColNo, dRow.Item("GROUP_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.GROUP_KB.ColNo, dRow.Item("GROUP_KB").ToString())
                .SetCellValue(i, sprDetailDef.SEIQKMK_NM.ColNo, dRow.Item("SEIQKMK_NM").ToString())
                .SetCellValue(i, sprDetailDef.TAX_KB_NM.ColNo, dRow.Item("TAX_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.TAX_KB.ColNo, dRow.Item("TAX_KB").ToString())
                .SetCellValue(i, sprDetailDef.REMARK.ColNo, dRow.Item("REMARK").ToString())
                .SetCellValue(i, sprDetailDef.SEIQKMK_CD.ColNo, dRow.Item("SEIQKMK_CD").ToString())
                .SetCellValue(i, sprDetailDef.SEIQKMK_CD_S.ColNo, dRow.Item("SEIQKMK_CD_S").ToString())
                .SetCellValue(i, sprDetailDef.KEIRI_KB_NM.ColNo, dRow.Item("KEIRI_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.KEIRI_KB.ColNo, dRow.Item("KEIRI_KB").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With

    End Sub

    '2014.09.12 追加START 多通貨対応
    ''' <summary>
    ''' スプレッド国コードコンボボックス設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetComboCountry() As StyleInfo

        Dim sort As String = "KBN_NM7"
        Dim getDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN)
        getDt.Rows.Clear()
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(String.Concat("KBN_GROUP_CD = 'Z001' AND SYS_DEL_FLG = '0'"), sort)
        Dim item As String = String.Empty
        Dim preitem As String = String.Empty
        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            item = getDr(i).Item("KBN_NM7").ToString()
            If String.IsNullOrEmpty(preitem) = True OrElse item.Equals(preitem) = False Then
                getDr(i).Item("KBN_NM7") = item
                getDt.ImportRow(getDr(i))
            End If
            preItem = getDr(i).Item("KBN_NM7").ToString()
        Next

        Dim cmb As StyleInfo = LMSpreadUtility.GetComboCell(Me._Frm.sprDetail, New DataView(getDt), "KBN_NM7", "KBN_NM7", False)

        Return cmb

    End Function
    '2014.09.12 追加END 多通貨対応

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
