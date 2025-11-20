' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ330G : UNマスタ照会
'  作  成  者       :  asatsuma
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
''' LMZ330Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ330G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ330F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ330F)

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared UN_NO As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.UN_NO, "UN", 50, True)
        Public Shared PG_KBN As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.PG_KBN, "PG", 50, True)
        Public Shared IMDG_CLASS As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.IMDG_CLASS, "クラス(正)", 90, True)
        Public Shared IMDG_CLASS1 As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.IMDG_CLASS1, "クラス(副)", 90, True)
        Public Shared IMDG_CLASS2 As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.IMDG_CLASS2, "クラス(副)", 90, True)
        Public Shared MP_FLG_NM As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.MP_FLG_NM, "海洋汚染物質", 100, True)
        Public Shared MP_FLG As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.MP_FLG, "海洋汚染物質区分", 100, False)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ330C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

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
            .sprDetail.ActiveSheet.ColumnCount = LMZ330C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)

            '列固定位置を設定します。(ex.運送会社名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ330G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.UN_NO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 4, False))
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.PG_KBN.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 5, False))
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.IMDG_CLASS.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 3, False))
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.IMDG_CLASS1.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 10, False))
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.IMDG_CLASS2.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 3, False))
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.MP_FLG_NM.ColNo, Me.SetComboMp())
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.MP_FLG.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_HANKAKU, 1, False))
            .sprDetail.SetCellStyle(0, LMZ330G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

        End With

        Call Me.SetInitValue(drow)

    End Sub

    ''' <summary>
    ''' 営業所コンボボックス初期値とスプレッド初期値を設定します
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)

        With _Frm.sprDetail

            .SetCellValue(0, LMZ330G.sprDetailDef.UN_NO.ColNo, drow("UN_NO").ToString())
            .SetCellValue(0, LMZ330G.sprDetailDef.PG_KBN.ColNo, drow("PG_KBN").ToString())

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
                .SetCellStyle(i, sprDetailDef.UN_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.PG_KBN.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IMDG_CLASS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IMDG_CLASS1.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.IMDG_CLASS2.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MP_FLG_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.MP_FLG.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.UN_NO.ColNo, dRow.Item("UN_NO").ToString())
                .SetCellValue(i, sprDetailDef.PG_KBN.ColNo, dRow.Item("PG_KBN").ToString())
                .SetCellValue(i, sprDetailDef.IMDG_CLASS.ColNo, dRow.Item("IMDG_CLASS").ToString())
                .SetCellValue(i, sprDetailDef.IMDG_CLASS1.ColNo, dRow.Item("IMDG_CLASS1").ToString())
                .SetCellValue(i, sprDetailDef.IMDG_CLASS2.ColNo, dRow.Item("IMDG_CLASS2").ToString())
                .SetCellValue(i, sprDetailDef.MP_FLG_NM.ColNo, dRow.Item("MP_FLG_NM").ToString())
                .SetCellValue(i, sprDetailDef.MP_FLG.ColNo, dRow.Item("MP_FLG").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With

    End Sub

    ''' <summary>
    ''' 海洋汚染物質コンボボックス設定
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetComboMp() As StyleInfo

        Dim sort As String = "KBN_CD"
        Dim getDt As DataTable = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN)
        getDt.Rows.Clear()
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = 'G013' AND SYS_DEL_FLG = '0'", sort)

        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max
            getDt.ImportRow(getDr(i))
        Next

        Dim cmb As StyleInfo = LMSpreadUtility.GetComboCell(Me._Frm.sprDetail, New DataView(getDt), "KBN_NM2", "KBN_NM1", False)

        Return cmb

    End Function


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
