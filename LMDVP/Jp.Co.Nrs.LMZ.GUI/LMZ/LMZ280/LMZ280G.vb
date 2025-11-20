' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ     : 共通
'  プログラムID     :  LMZ280G : JISマスタ検索
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
''' LMZ280Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ280G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ280F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ280F)

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


#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定

        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.DEF, " ", 20, True)
        Public Shared SHOBO_CD As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.SHOBO_CD, "消防コード", 80, True)
        Public Shared RUI_NM As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.RUI_NM, "類", 90, True)
        Public Shared RUI As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.RUI, "類区分", 50, False)
        Public Shared HINMEI As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.HINMEI, "品名", 140, True)
        Public Shared SEISITSU As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.SEISITSU, "性質", 140, True)
        Public Shared KIKEN_TOKYU_NM As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.KIKEN_TOKYU_NM, "等級", 80, True)
        Public Shared KIKEN_TOKYU As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.KIKEN_TOKYU, "等級区分", 50, False)
        Public Shared SYU_NM As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.SYU_NM, "種別", 80, True)
        Public Shared SYU As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.SYU, "種別区分", 50, False)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ280C.SprColumnIndex.ROW_INDEX, "行番号", 10, False)

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
            .sprDetail.ActiveSheet.ColumnCount = 11

            '2015.10.15 英語化対応START
            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            '.sprDetail.SetColProperty(New sprDetailDef)
            .sprDetail.SetColProperty(New sprDetailDef, False)
            '2015.10.15 英語化対応END

            '列固定位置を設定します。(ex.荷主名で固定)
            .sprDetail.ActiveSheet.FrozenColumnCount = LMZ280G.sprDetailDef.DEF.ColNo + 1

            '列設定
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.SHOBO_CD.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.HAN_NUMBER, 3, False))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.RUI_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S004", False))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.RUI.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.HINMEI.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.SEISITSU.ColNo, LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 40, False))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.KIKEN_TOKYU_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S002", False))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.KIKEN_TOKYU.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.SYU_NM.ColNo, LMSpreadUtility.GetComboCellKbn(.sprDetail, "S022", False))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.SYU.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))
            .sprDetail.SetCellStyle(0, LMZ280G.sprDetailDef.ROW_INDEX.ColNo, LMSpreadUtility.GetLabelCell(.sprDetail))

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

            .SetCellValue(0, LMZ280G.sprDetailDef.SHOBO_CD.ColNo, drow("SHOBO_CD").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.RUI_NM.ColNo, drow("RUI").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.RUI.ColNo, drow("RUI").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.HINMEI.ColNo, drow("HINMEI").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.SEISITSU.ColNo, drow("SEISITSU").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.KIKEN_TOKYU_NM.ColNo, drow("KIKEN_TOKYU").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.KIKEN_TOKYU.ColNo, drow("KIKEN_TOKYU").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.SYU_NM.ColNo, drow("SYU").ToString())
            .SetCellValue(0, LMZ280G.sprDetailDef.SYU.ColNo, drow("SYU").ToString())

        End With

    End Sub


    ''' <summary>
    ''' スプレッドにデータを設定(明細)
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetSpread(ByVal dt As DataTable, ByVal selectPluralFlg As String)

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

            '複数選択の場合、.KeyboardCheckBoxOnを無効にする(LMSpreadSearchの仕様でTrueだと複数選択不可のため)
            If LMConst.FLG.ON.Equals(selectPluralFlg) Then
                .KeyboardCheckBoxOn = False
            Else
                'キーボード操作でチェックボックスＯＮ
                .KeyboardCheckBoxOn = True
            End If

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
                .SetCellStyle(i, sprDetailDef.SHOBO_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.RUI_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.RUI.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.HINMEI.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SEISITSU.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KIKEN_TOKYU_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.KIKEN_TOKYU.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYU_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.SYU.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ROW_INDEX.ColNo, sLabel)     '行番号

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.SHOBO_CD.ColNo, dRow.Item("SHOBO_CD").ToString())
                .SetCellValue(i, sprDetailDef.RUI_NM.ColNo, dRow.Item("RUI_NM").ToString())
                .SetCellValue(i, sprDetailDef.RUI.ColNo, dRow.Item("RUI").ToString())
                .SetCellValue(i, sprDetailDef.HINMEI.ColNo, dRow.Item("HINMEI").ToString())
                .SetCellValue(i, sprDetailDef.SEISITSU.ColNo, dRow.Item("SEISITSU").ToString())
                .SetCellValue(i, sprDetailDef.KIKEN_TOKYU_NM.ColNo, dRow.Item("KIKEN_TOKYU_NM").ToString())
                .SetCellValue(i, sprDetailDef.KIKEN_TOKYU.ColNo, dRow.Item("KIKEN_TOKYU").ToString())
                .SetCellValue(i, sprDetailDef.SYU_NM.ColNo, dRow.Item("SYU_NM").ToString())
                .SetCellValue(i, sprDetailDef.SYU.ColNo, dRow.Item("SYU").ToString())
                .SetCellValue(i, sprDetailDef.ROW_INDEX.ColNo, Convert.ToString(i - 1))

            Next

            .ResumeLayout(True)

        End With

    End Sub

#End Region 'Spread

#Region "部品"

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
