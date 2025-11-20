' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMZ       : 共通
'  プログラムID     :  LMZ380G   : 物産アニマルヘルス在庫選択
'  作  成  者       :  HORI
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
Imports GrapeCity.Win.Editors

''' <summary>
''' LMZ380Gamenクラス
''' </summary>
''' <remarks>画面構築を行う</remarks>
''' <histry>
''' 
''' </histry>
Public Class LMZ380G
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As LMZ380F

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
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As LMZ380F)

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

            .cmbWhType.SelectedValue = drow("WH_TYPE").ToString()

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
            Me.SetLockControl(.cmbWhType, lock)
            .sprDetail.Enabled = Not lock

        End With

    End Sub

    ''' <summary>
    ''' コンボボックスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub CreateComboBox()

        With Me._Frm

            Me.CreateComboBox(.cmbWhType, LMConst.CacheTBL.KBN, New String() {"KBN_NM1"}, New String() {"REM"}, " KBN_GROUP_CD = 'B047'", "KBN_CD")

        End With

    End Sub

    ''' <summary>
    ''' マスタコンボボックス作成
    ''' </summary>
    ''' <param name="cmb">コンボボックスコントロール</param>
    ''' <param name="cacheTbl">cacheテーブル名</param>
    ''' <param name="cdNm">項目名</param>
    ''' <param name="itemNm">Display項目名</param>
    ''' <param name="sql">検索条件</param>
    ''' <param name="sort">ソート</param>
    ''' <remarks>symbolには必ず2個設定してください。</remarks>
    Friend Sub CreateComboBox(ByVal cmb As LMImCombo _
                              , ByVal cacheTbl As String _
                              , ByVal cdNm As String() _
                              , ByVal itemNm As String() _
                              , ByVal sql As String _
                              , ByVal sort As String
                              )

        'リストのクリア
        cmb.Items.Clear()

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        '空行追加
        Call Me.ComboBoxItemAdd(cmb, cd, item)

        'マスタ検索処理
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select(sql, sort)

        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max

            cd = Me.SetCombData(drs(i), cdNm)
            item = Me.SetCombData(drs(i), itemNm)

            'アイテム追加
            Call Me.ComboBoxItemAdd(cmb, cd, item)

        Next

    End Sub

    ''' <summary>
    ''' コンボに設定する文字を作成
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="str">文字型配列</param>
    ''' <returns>設定文字</returns>
    ''' <remarks></remarks>
    Private Function SetCombData(ByVal dr As DataRow _
                                 , ByVal str As String()
                                 ) As String

        SetCombData = String.Empty
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            SetCombData = String.Concat(SetCombData, dr.Item(str(i)).ToString())
        Next

        Return SetCombData

    End Function

    ''' <summary>
    ''' コンボに行を追加　
    ''' </summary>
    ''' <param name="cmb">コントロール</param>
    ''' <param name="cd">Value値</param>
    ''' <param name="item">Text値</param>
    ''' <remarks></remarks>
    Friend Sub ComboBoxItemAdd(ByVal cmb As LMImCombo, ByVal cd As String, ByVal item As String)
        cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))
    End Sub

#End Region

#Region "Spread"

    ''' <summary>
    ''' スプレッド列定義体
    ''' </summary>
    ''' <remarks>SpreadColProperty型のフィールドのみで定義すること</remarks>
    Public Class sprDetailDef

        'スプレッド(タイトル列)の設定
        Public Shared DEF As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.DEF, " ", 20, True)

        Public Shared GOODS_CD As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.GOODS_CD, "商品CD", 90, True)
        Public Shared GOODS_NM As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.GOODS_NM, "商品名", 200, True)
        Public Shared LOT_NO As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.LOT_NO, "LOT", 160, True)
        Public Shared GOODS_RANK_NM As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.GOODS_RANK_NM, "商品ランク", 120, True)
        Public Shared NB As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.NB, "個数", 90, True)
        Public Shared LT_DATE As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.LT_DATE, "使用期限", 90, True)
        Public Shared NRS_BR_CD As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.NRS_BR_CD, "(営業所コード)", 100, False)
        Public Shared ZAI_REC_NO As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.ZAI_REC_NO, "(在庫レコード番号)", 100, False)
        Public Shared GOODS_CD_NRS As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.GOODS_CD_NRS, "(商品KEY)", 100, False)
        Public Shared GOODS_RANK As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.GOODS_RANK, "(商品ランク)", 100, False)
        Public Shared ROW_INDEX As SpreadColProperty = New SpreadColProperty(LMZ380C.SprColumnIndex.ROW_INDEX, "(行番号)", 100, False)

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
            .sprDetail.Sheets(0).ColumnCount = LMZ380C.SprColumnIndex.LAST

            'スプレッドの列設定（列名、列幅、列の表示・非表示）
            .sprDetail.SetColProperty(New sprDetailDef, False)

            '列固定位置を設定します。(ex.商品名で固定)
            .sprDetail.Sheets(0).FrozenColumnCount = sprDetailDef.GOODS_NM.ColNo + 1

            '列設定
            Dim sLbl As StyleInfo = LMSpreadUtility.GetLabelCell(.sprDetail)
            Dim sTxt As StyleInfo = LMSpreadUtility.GetTextCell(.sprDetail, InputControl.ALL_MIX, 100, False)
            Dim sCustCond As StyleInfo = LMSpreadUtility.GetComboCellMaster(.sprDetail _
                , LMConst.CacheTBL.CUSTCOND _
                , "JOTAI_CD" _
                , "JOTAI_NM" _
                , False _
                , New String() {"NRS_BR_CD", "CUST_CD_L"} _
                , New String() {drow("NRS_BR_CD").ToString(), "00294"} _
                , LMConst.JoinCondition.AND_WORD
                )

            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.GOODS_CD.ColNo, sTxt)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.GOODS_NM.ColNo, sTxt)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.LOT_NO.ColNo, sTxt)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.GOODS_RANK_NM.ColNo, sCustCond)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.NB.ColNo, sLbl)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.LT_DATE.ColNo, sLbl)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.NRS_BR_CD.ColNo, sLbl)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.ZAI_REC_NO.ColNo, sLbl)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.GOODS_CD_NRS.ColNo, sLbl)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.GOODS_RANK.ColNo, sLbl)
            .sprDetail.SetCellStyle(0, LMZ380G.sprDetailDef.ROW_INDEX.ColNo, sLbl)

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

            .SetCellValue(0, LMZ380G.sprDetailDef.GOODS_CD.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.GOODS_NM.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.LOT_NO.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.GOODS_RANK_NM.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.NB.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.LT_DATE.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.NRS_BR_CD.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.ZAI_REC_NO.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.GOODS_CD_NRS.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.GOODS_RANK.ColNo, String.Empty)
            .SetCellValue(0, LMZ380G.sprDetailDef.ROW_INDEX.ColNo, String.Empty)

        End With

        With _Frm

            .cmbNrsBrCd.SelectedValue = drow("NRS_BR_CD").ToString()
            .cmbWhType.SelectedValue = drow("WH_TYPE").ToString()

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
            Dim sNum As StyleInfo = LMSpreadUtility.GetNumberCell(spr, 0, 999999999, True, 0, , ",")

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
                .SetCellStyle(i, sprDetailDef.GOODS_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.LOT_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_RANK_NM.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NB.ColNo, sNum)
                .SetCellStyle(i, sprDetailDef.LT_DATE.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.NRS_BR_CD.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.ZAI_REC_NO.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_CD_NRS.ColNo, sLabel)
                .SetCellStyle(i, sprDetailDef.GOODS_RANK.ColNo, sLabel)

                'セルに値を設定
                .SetCellValue(i, sprDetailDef.DEF.ColNo, LMConst.FLG.OFF)
                .SetCellValue(i, sprDetailDef.GOODS_CD.ColNo, dRow.Item("GOODS_CD").ToString)
                .SetCellValue(i, sprDetailDef.GOODS_NM.ColNo, dRow.Item("GOODS_NM").ToString)
                .SetCellValue(i, sprDetailDef.LOT_NO.ColNo, dRow.Item("LOT_NO").ToString)
                .SetCellValue(i, sprDetailDef.GOODS_RANK_NM.ColNo, dRow.Item("GOODS_RANK_NM").ToString)
                .SetCellValue(i, sprDetailDef.NB.ColNo, dRow.Item("NB").ToString)
                .SetCellValue(i, sprDetailDef.LT_DATE.ColNo, DateFormatUtility.EditSlash(dRow.Item("LT_DATE").ToString()))
                .SetCellValue(i, sprDetailDef.NRS_BR_CD.ColNo, dRow.Item("NRS_BR_CD").ToString)
                .SetCellValue(i, sprDetailDef.ZAI_REC_NO.ColNo, dRow.Item("ZAI_REC_NO").ToString)
                .SetCellValue(i, sprDetailDef.GOODS_CD_NRS.ColNo, dRow.Item("GOODS_CD_NRS").ToString)
                .SetCellValue(i, sprDetailDef.GOODS_RANK.ColNo, dRow.Item("GOODS_RANK").ToString)
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
