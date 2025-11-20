' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC          : 
'  プログラムID     :  LMCControlG  : LMC編集画面 共通処理
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports GrapeCity.Win.Editors.Fields
Imports GrapeCity.Win.Editors

''' <summary>
''' LMCControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/03/01 SUZUKI
''' </histry>
Public Class LMCControlG
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

#End Region

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByRef handlerClass As LMBaseGUIHandler, ByRef frm As Form)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '呼び出し元のハンドラクラスをこのフォームに紐付けます。
        MyBase.MyHandler = handlerClass

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

    End Sub

#End Region

#Region "Method"

    ''' <summary>
    ''' マスタコンボボックス作成
    ''' </summary>
    ''' <param name="cmb">コンボボックスコントロール</param>
    ''' <param name="cacheTBL">cacheテーブル名</param>
    ''' <param name="cd_NM">Value項目名</param>
    ''' <param name="item_NM">Display項目名</param>
    ''' <param name="sort">ソート項目名</param>
    ''' <remarks>営業所コンボ、倉庫コンボなのに使う</remarks>
    Friend Sub CreateComboBox(ByRef cmb As LMImCombo, ByVal cacheTBL As String, _
                              ByVal cd_NM As String, ByVal item_NM As String, ByVal sort As String)

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        cmb.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        'マスタ検索処理
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(cacheTBL).Select("SYS_DEL_FLG = '0'", sort)

        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            cd = getDr(i).Item(cd_NM).ToString()
            item = getDr(i).Item(item_NM).ToString()

            cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

    ''' <summary>
    ''' 区分マスタコンボボックス作成
    ''' </summary>
    ''' <param name="cmb">コンボボックスコントロール</param>
    ''' <param name="KbnGCD">区分グループコード</param>
    ''' <remarks></remarks>
    Friend Sub CreateKBNComboBox(ByRef cmb As LMImCombo, ByVal KbnGCD As String)

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        cmb.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        'マスタ検索処理
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select("KBN_GROUP_CD = '" + KbnGCD + "'")

        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            cd = getDr(i).Item("KBN_CD").ToString()
            item = getDr(i).Item("KBN_NM1").ToString()

            cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType = True Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType = True Then

            'チェックボックスの場合、Booleanの値をStringに変換
            If aCell.Text.Equals("True") = True Then
                GetCellValue = LMConst.FLG.ON
            ElseIf aCell.Text.Equals("False") = True Then
                GetCellValue = LMConst.FLG.OFF
            Else
                GetCellValue = aCell.Text
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType = True Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType = True Then

            '日付の場合、Value値を yyyyMMdd に変換して返却
            If aCell.Value Is Nothing = False AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = Convert.ToDateTime(aCell.Value).ToString("yyyyMMdd")
            End If

        Else
            'テキストの場合、Trimした値を返却
            GetCellValue = aCell.Text.Trim()

        End If

        Return GetCellValue

    End Function

#Region "ユーティリティ"

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Friend Function FormatNumValue(ByVal value As String) As String

        If String.IsNullOrEmpty(value) = True Then
            value = 0.ToString()
        End If

        Return value

    End Function

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMMdd")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM/dd")
        ctl.Holiday = True

    End Sub

#End Region

#End Region

End Class
