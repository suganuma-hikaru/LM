' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB          : 
'  プログラムID     :  LMMControlG  : LMB編集画面 共通処理
'  作  成  者       :  [金ヘスル]
' ==========================================================================
Imports GrapeCity.Win.Editors.Fields
Imports FarPoint.Win.Spread
Imports FarPoint.Win.Spread.CellType
Imports Jp.Co.Nrs.Com.Const
Imports Jp.Co.Nrs.LM.Base
Imports Jp.Co.Nrs.LM.GUI.Win
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.Base.GUI
Imports Jp.Co.Nrs.LM.GUI.Win.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMMControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/03/01 SUZUKI
''' </histry>
Public Class LMMControlG
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal frm As LMFormSxga)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

    End Sub

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

#Region "画面制御"

    ''' <summary>
    '''クリア処理を行う
    ''' </summary>
    ''' <param name="ctl">クリア対象項目</param>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal ctl As Control)

        Dim arr As ArrayList = New ArrayList()
        Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim max As Integer = arr.Count - 1
        Dim arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl = Nothing

        'エディット系コントロールのクリア処理を行う
        For index As Integer = 0 To max

            arrCtl = DirectCast(arr(index), Nrs.Win.GUI.Win.Interface.IEditableControl)

            'コントロール別にクリア処理を行う
            If TypeOf arrCtl Is Win.InputMan.LMImCombo = True Then

                DirectCast(arrCtl, Win.InputMan.LMImCombo).SelectedValue = String.Empty

            ElseIf TypeOf arrCtl Is Win.InputMan.LMComboKubun = True Then

                DirectCast(arrCtl, Win.InputMan.LMComboKubun).SelectedValue = String.Empty

            ElseIf TypeOf arrCtl Is Win.InputMan.LMImNumber = True Then

                DirectCast(arrCtl, Win.InputMan.LMImNumber).Value = 0

            Else

                arrCtl.TextValue = String.Empty

            End If

        Next

        'チェックボックスのクリア処理を行う
        arr = New ArrayList()
        Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        max = arr.Count - 1
        Dim chk As Win.LMCheckBox = Nothing
        For index As Integer = 0 To max

            chk = DirectCast(arr(index), Win.LMCheckBox)

            chk.SetBinaryValue(0.ToString())

        Next

    End Sub

    ''' <summary>
    ''' マスタコンボボックス作成
    ''' </summary>
    ''' <param name="cmb">コンボボックスコントロール</param>
    ''' <param name="cacheTbl">cacheテーブル名</param>
    ''' <param name="cdNm">項目名</param>
    ''' <param name="itemNm">Display項目名</param>
    ''' <param name="sort">ソート項目名</param>
    ''' <remarks>営業所コンボ、倉庫コンボなのに使う</remarks>
    Friend Sub CreateComboBox(ByVal cmb As LMImCombo _
                              , ByVal cacheTbl As String _
                              , ByVal cdNm As String _
                              , ByVal itemNm As String _
                              , ByVal sort As String _
                              )

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        cmb.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        'マスタ検索処理
        Dim getDr As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select("SYS_DEL_FLG = '0'", sort)

        Dim max As Integer = getDr.Count - 1
        For i As Integer = 0 To max

            cd = getDr(i).Item(cdNm).ToString()
            item = getDr(i).Item(itemNm).ToString()

            cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl"></param>
    ''' <param name="dateFormat">設定する日付フォーマット</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate, Optional ByVal dateFormat As LMMControlC.DATE_FORMAT = LMMControlC.DATE_FORMAT.YYYY_MM_DD)

        Dim format As String = String.Empty
        Dim dispFormat As String = String.Empty

        Select Case dateFormat
            Case LMMControlC.DATE_FORMAT.YYYY_MM_DD
                '年月日編集
                format = "yyyyMMdd"
                dispFormat = "yyyy/MM/dd"
            Case LMMControlC.DATE_FORMAT.YYYY_MM
                '年月編集
                format = "yyyyMM"
                dispFormat = "yyyy/MM"
            Case LMMControlC.DATE_FORMAT.MM_DD
                '月日編集
                format = "MMdd"
                dispFormat = "MM/dd"
        End Select

        ctl.Format = DateFieldsBuilder.BuildFields(format)
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields(dispFormat)
        ctl.Holiday = True

    End Sub

    ''' <summary>
    ''' 前ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="ketasu">設定桁数</param>
    ''' <returns>前ゼロつき設定値</returns>
    ''' <remarks></remarks>
    Friend Function SetMaeZeroData(ByVal value As String, ByVal ketasu As Integer) As String

        SetMaeZeroData = String.Concat(Me.GetZeroData(ketasu), value)

        Return SetMaeZeroData.Substring(SetMaeZeroData.Length - ketasu, ketasu)

    End Function

    ''' <summary>
    ''' 前ゼロするときの数を取得
    ''' </summary>
    ''' <param name="ketasu">設定桁数</param>
    ''' <returns>ゼロデータ</returns>
    ''' <remarks></remarks>
    Private Function GetZeroData(ByVal ketasu As Integer) As String

        GetZeroData = String.Empty
        Dim max As Integer = ketasu - 1
        For index As Integer = 0 To max
            GetZeroData = String.Concat(GetZeroData, LMConst.FLG.OFF)
        Next

        Return GetZeroData

    End Function

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

#Region "ロック制御"

    ''' <summary>
    ''' ロック処理/ロック解除処理を行う
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, Optional ByVal lockFlg As Boolean = False)

        Dim arr As ArrayList = New ArrayList()
        Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
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
            Me.LockEditControl(arrCtl, lockFlg)

        Next

        'ラベル項目をロック
        For Each lblCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In lblArr
            Me.LockEditControl(lblCtl, True)
        Next

        'ボタンのロック制御
        arr = New ArrayList()
        Me.GetTarget(Of Win.LMButton)(arr, ctl)
        For Each arrCtl As Win.LMButton In arr

            'ロック処理/ロック解除処理を行う
            Me.LockButton(arrCtl, lockFlg)

        Next

        'オプションボタンのロック制御
        arr = New ArrayList()
        Me.GetTarget(Of Win.LMOptionButton)(arr, ctl)
        For Each arrCtl As Win.LMOptionButton In arr

            'ロック処理/ロック解除処理を行う
            Me.LockOptionButton(arrCtl, lockFlg)

        Next

        'チェックボックスのロック制御
        arr = New ArrayList()
        Me.GetTarget(Of Win.LMCheckBox)(arr, ctl)
        For Each arrCtl As Win.LMCheckBox In arr

            'ロック処理/ロック解除処理を行う
            Me.LockCheckBox(arrCtl, lockFlg)

        Next

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockText(ByVal ctl As Win.InputMan.LMImTextBox, ByVal lockFlg As Boolean)
        Me.LockEditControl(ctl, lockFlg)
    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockComb(ByVal ctl As Win.InputMan.LMImCombo, ByVal lockFlg As Boolean)
        Me.LockEditControl(ctl, lockFlg)
    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockCombKbn(ByVal ctl As Win.InputMan.LMComboKubun, ByVal lockFlg As Boolean)
        Me.LockEditControl(ctl, lockFlg)
    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockNumber(ByVal ctl As Win.InputMan.LMImNumber, ByVal lockFlg As Boolean)
        Me.LockEditControl(ctl, lockFlg)
    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockDate(ByVal ctl As Win.InputMan.LMImDate, ByVal lockFlg As Boolean)
        Me.LockEditControl(ctl, lockFlg)
    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockMask(ByVal ctl As Win.InputMan.LMImMasked, ByVal lockFlg As Boolean)
        Me.LockEditControl(ctl, lockFlg)
    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockOptionButton(ByVal ctl As Win.LMOptionButton, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
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

    ''' <summary>
    ''' 引数のコントロールをロックします。
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
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockFunctionKey(ByVal ctl As Win.InputMan.LMImFunctionKey, ByVal lockFlg As Boolean)

        Dim enabledFlg As Boolean

        If lockFlg = True Then
            enabledFlg = False
        Else
            enabledFlg = True
        End If

        ctl.Enabled = enabledFlg

    End Sub

    ''' <summary>
    ''' 引数のコントロールをロックします。
    ''' </summary>
    ''' <param name="ctl">設定対象コントロール</param>
    ''' <param name="lockFlg">ロック処理を行う場合True</param>
    ''' <remarks></remarks>
    Friend Sub LockEditControl(ByVal ctl As Nrs.Win.GUI.Win.Interface.IEditableControl, ByVal lockFlg As Boolean)
        ctl.ReadOnlyStatus = lockFlg
    End Sub

#End Region

#End Region

#Region "スプレッド"

    ''' <summary>
    '''Spread検索行のクリア処理を行う
    ''' </summary>
    ''' <param name="spr">検索用Spread</param>
    ''' <remarks></remarks>
    Friend Sub ClearControl(ByVal spr As LMSpreadSearch)

        With spr
            Dim colMax As Integer = .ActiveSheet.Columns.Count - 1
            For i As Integer = 0 To colMax
                .SetCellValue(0, i, String.Empty)
            Next
        End With

    End Sub

#End Region

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
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Friend Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = " - ") As String

        EditConcatData = value1
        If String.IsNullOrEmpty(EditConcatData) = True Then

            EditConcatData = value2

        Else

            If String.IsNullOrEmpty(value2) = False Then

                EditConcatData = String.Concat(EditConcatData, str, value2)

            End If

        End If

        Return EditConcatData

    End Function

    ''' <summary>
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Public Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

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
    ''' 背景色の初期化
    ''' </summary>
    ''' <param name="ctl">制御対象項目</param>
    ''' <remarks></remarks>
    Friend Sub SetBackColor(ByVal ctl As Control)

        Dim arr As ArrayList = New ArrayList()
        Me.GetTarget(Of Nrs.Win.GUI.Win.Interface.IEditableControl)(arr, ctl)
        Dim defColor As System.Drawing.Color = Utility.LMGUIUtility.GetSystemInputBackColor
        Dim lockColor As System.Drawing.Color = Utility.LMGUIUtility.GetReadOnlyBackColor

        'エディット系コントロール
        For Each arrCtl As Nrs.Win.GUI.Win.Interface.IEditableControl In arr

            If arrCtl.ReadOnlyStatus = False Then

                arrCtl.BackColorDef = defColor

            Else
                arrCtl.BackColorDef = lockColor

            End If

        Next

        'スプレッド項目
        arr = New ArrayList()
        Me.GetTarget(Of Win.Spread.LMSpread)(arr, ctl)
        Dim rowMax As Integer = 0
        Dim colMax As Integer = 0
        Dim cell As FarPoint.Win.Spread.Cell = Nothing

        For Each spr As Win.Spread.LMSpread In arr

            With spr.ActiveSheet

                rowMax = .Rows.Count - 1
                colMax = .Columns.Count - 1

                For i As Integer = 0 To rowMax

                    For j As Integer = 0 To colMax

                        cell = .Cells(i, j)
                        If cell.Locked = False Then
                            cell.BackColor = defColor
                        End If
                        cell = Nothing
                    Next

                Next

            End With

        Next

    End Sub

#End Region

#Region "キャッシュから値取得"
    ''' <summary>
    ''' ユーザマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUserListDataRow(ByVal userId As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(Me.SelectUserString(userId))

    End Function

    ''' <summary>
    ''' ユーザマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUserString(ByVal userId As String) As String

        SelectUserString = String.Empty

        '削除フラグ
        SelectUserString = String.Concat(SelectUserString, " SYS_DEL_FLG = '0' ")

        'ユーザコード
        SelectUserString = String.Concat(SelectUserString, " AND ", "USER_CD = ", " '", userId, "' ")

        Return SelectUserString

    End Function

#End Region

#End Region

End Class
