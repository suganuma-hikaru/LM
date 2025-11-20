' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ090G : エリアマスタ照会
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
''' LMZ090Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ090G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ090F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ090F, ByVal g As LMZControlG)

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
    ''' 画面ヘッダー部ロック処理を行う
    ''' </summary>
    ''' <param name="lock">trueはロック処理</param>
    ''' <remarks></remarks>
    Friend Sub LockControl(ByVal lock As Boolean)

        With Me._Frm

            Me.SetLockControl(.cmbNrsBrCd, lock)
            .sprDetail.Enabled = Not lock

        End With

    End Sub

    ''' <summary>
    ''' ファンクションキーのロック処理
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub FunctionKeyLock()

        With Me._Frm.FunctionKey
            .F9ButtonEnabled = False
            .F10ButtonEnabled = False
            .F11ButtonEnabled = False
            .F12ButtonEnabled = True

            '2015.10.15 英語化対応START
            'タイトルテキスト・フォント設定の切り替え
            .TitleSwitching(Me._Frm)
            '2015.10.15 英語化対応END

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
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared AREA_CD As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.AREA_CD, "エリア" & vbCrLf & "コード", 80, True)
        Public Shared BIN_KB_NM As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.BIN_KB_NM, "便区分", 90, True)
        Public Shared BIN_KB As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.BIN_KB, "便区分", 50, False)
        Public Shared AREA_NM As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.AREA_NM, "エリア名", 150, True)
        Public Shared AREA_INFO As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.AREA_INFO, "備考", 390, True)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ090C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

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
            .sprDetail.ActiveSheet.ColumnCount = 7

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ090G.sprDetailDef.DEF.ColNo + 1

            '区分コンボ設定
            Dim flgKbn As StyleInfo = Me._LMZConG.StyleInfoFlg(.sprDetail)

            '列設定
            .sprDetail.SetCellStyle(0, LMZ090G.sprDetailDef.AREA_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUM_ALPHA_U, 7, False))
            .sprDetail.SetCellStyle(0, LMZ090G.sprDetailDef.BIN_KB_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "U001", False))
            .sprDetail.SetCellStyle(0, LMZ090G.sprDetailDef.BIN_KB.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail, CellHorizontalAlignment.Left))
            .sprDetail.SetCellStyle(0, LMZ090G.sprDetailDef.AREA_NM.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 20, False))
            .sprDetail.SetCellStyle(0, LMZ090G.sprDetailDef.AREA_INFO.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False))
            .sprDetail.SetCellStyle(0, LMZ090G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

        End With


        Call Me.SetInitValue(drow)


    End Sub


    ''' <summary>
    ''' 画面初期値設定(スプレッド)
    ''' </summary>
    ''' <param name="drow"></param>
    ''' <remarks></remarks>
    Friend Sub SetInitValue(ByVal drow As DataRow)


        With Me._Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()

        End With


        With Me._Frm.sprDetail

            .SetCellValue(0, LMZ090G.sprDetailDef.AREA_CD.ColNo, drow("AREA_CD").ToString())
            .SetCellValue(0, LMZ090G.sprDetailDef.BIN_KB_NM.ColNo, drow("BIN_KB").ToString())
            .SetCellValue(0, LMZ090G.sprDetailDef.BIN_KB.ColNo, drow("BIN_KB").ToString())
            .SetCellValue(0, LMZ090G.sprDetailDef.AREA_NM.ColNo, drow("AREA_NM").ToString())
            .SetCellValue(0, LMZ090G.sprDetailDef.AREA_INFO.ColNo, drow("AREA_INFO").ToString())

            '初期値を空に設定
            .Sheets(0).Cells(0, sprDetailDef.BIN_KB_NM.ColNo).Value = String.Empty


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
            Dim cd As String = String.Empty


            '値設定
            For i As Integer = 1 To lngcnt

                dRow = dt.Rows(i - 1)

                'セルスタイル設定

                .SetCellStyle(i, sprDetailDef.DEF.ColNo, sDEF)
                .SetCellStyle(i, sprDetailDef.AREA_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BIN_KB_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.BIN_KB.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.AREA_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.AREA_INFO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.AREA_CD.ColNo, dRow.Item("AREA_CD").ToString())
                .SetCellValue(i, sprDetailDef.BIN_KB_NM.ColNo, dRow.Item("BIN_KB_NM").ToString())
                .SetCellValue(i, sprDetailDef.BIN_KB.ColNo, dRow.Item("BIN_KB").ToString())
                .SetCellValue(i, sprDetailDef.AREA_NM.ColNo, dRow.Item("AREA_NM").ToString())
                .SetCellValue(i, sprDetailDef.AREA_INFO.ColNo, dRow.Item("AREA_INFO").ToString())
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
