' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMD          : 
'  プログラムID     :  LMDControlG  : LMD編集画面 共通処理
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
Imports GrapeCity.Win.Editors

''' <summary>
''' LMDControl画面クラス
''' </summary>
''' <remarks></remarks>
Public Class LMDControlG
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Field"

    ''' <summary>
    ''' このクラスが紐付くフォームクラス
    ''' </summary>
    ''' <remarks></remarks>
    Private _Frm As Form

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

        Me._Frm = frm

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

#Region "画面制御"

    ''' <summary>
    ''' Enter押下イベントの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEnterEvent(ByVal spr As Jp.Co.Nrs.LM.GUI.Win.Spread.LMSpread)

        'キーイベントをフォームで受け取る
        Me._Frm.KeyPreview = True

        'ENTER時にセルを右移動させる
        Dim im As New FarPoint.Win.Spread.InputMap

        ' 非編集セルでの[Enter]キーを「次列へ移動」とします
        im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

        '編集中セルでの[Enter]キーを「次列へ移動」とします
        im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
        im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

    End Sub

    ''' <summary>
    ''' YES/NOフラグからチェックボックスを設定
    ''' </summary>
    ''' <param name="ctl">チェックボックス</param>
    ''' <param name="value">YES:01,NO:00</param>
    ''' <remarks></remarks>
    Friend Sub SetCheckBox(ByVal ctl As Win.LMCheckBox, ByVal value As String)

        If String.IsNullOrEmpty(value) Then
            Exit Sub
        End If

        If value.Equals(LMDControlC.YN_FLG_YES) Then
            ctl.SetBinaryValue(LMConst.FLG.ON)
        ElseIf value.Equals(LMDControlC.YN_FLG_NO) Then
            ctl.SetBinaryValue(LMConst.FLG.OFF)
        End If

    End Sub

    ''' <summary>
    ''' タブ移動処理
    ''' </summary>
    ''' <param name="spr">スプレッドコントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetNextControl(ByVal spr As Win.Spread.LMSpread)

        If Me._Frm.ActiveControl.Equals(spr) = False Then
            Me._Frm.SelectNextControl(Me._Frm.ActiveControl, True, True, True, True)
        End If

    End Sub

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
    ''' 画面上のコントロールから指定した型のクラスかその継承クラスを取得する
    ''' </summary>
    ''' <param name="arr">取得したコントロールを格納するArrayList</param>
    ''' <param name="ownControl">コントロール取得元となるコントロール</param>
    ''' <remarks></remarks>
    Friend Sub GetTarget(Of T)(ByVal arr As ArrayList, ByVal ownControl As Control)

        If TypeOf ownControl Is T _
            OrElse ownControl.GetType.IsSubclassOf(GetType(T)) Then

            '指定されたクラスかその継承クラス
            arr.Add(ownControl)

        End If

        If 0 < ownControl.Controls.Count Then
            For Each targetControl As Control In ownControl.Controls
                Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

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
                        ElseIf cell.Locked = True Then
                            cell.BackColor = lockColor
                        End If
                        cell = Nothing
                    Next

                Next

            End With

        Next

    End Sub

#End Region

#End Region

#Region "スプレッド"

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Friend Function GetCellValue(ByVal aCell As Cell) As String

        GetCellValue = String.Empty

        If TypeOf aCell.Editor Is CellType.ComboBoxCellType Then

            'コンボボックスの場合、Value値を返却
            If aCell.Value Is Nothing = False Then
                GetCellValue = aCell.Value.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.CheckBoxCellType Then

            'チェックボックスの場合、0 or 1を返却
            If Me.ChangeBooleanCheckBox(aCell.Text) = True Then
                GetCellValue = LMConst.FLG.ON
            Else
                GetCellValue = LMConst.FLG.OFF
            End If

        ElseIf TypeOf aCell.Editor Is CellType.NumberCellType Then

            'ナンバーの場合、Value値を返却
            If aCell.Value Is Nothing = False _
                AndAlso String.IsNullOrEmpty(aCell.Value.ToString()) = False Then
                GetCellValue = aCell.Value.ToString()
            Else
                GetCellValue = 0.ToString()
            End If

        ElseIf TypeOf aCell.Editor Is CellType.DateTimeCellType Then

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

    ''' <summary>
    ''' チェックボックスの値をString型からBoolean型に変換する
    ''' </summary>
    ''' <param name="value">obj.text(0:チェック無し,1:チェック有り)</param>
    ''' <returns>True:チェック有り,False:チェック無し</returns>
    ''' <remarks></remarks>
    Friend Function ChangeBooleanCheckBox(ByVal value As String) As Boolean

        '"1"の場合Trueを返却
        If (LMConst.FLG.ON.Equals(value) = True) _
            OrElse True.ToString().Equals(value) = True Then
            Return True
        End If

        '"0"の場合Falseを返却
        Return False

    End Function

    ''' <summary>
    ''' スプレッド明細行のチェックリスト(RowIndex)取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetCheckList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim arr As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If LMConst.FLG.ON.Equals(Me.GetCellValue(.Cells(i, defNo))) = True Then
                    '選択されたRowIndexを設定
                    arr.Add(i)
                End If
            Next

            Return arr

        End With

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' 閾値の取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード　初期値 = "02"</param>
    ''' <returns>閾値</returns>
    ''' <remarks></remarks>
    Friend Function GetLimitData(Optional ByVal kbnCd As String = LMDControlC.LIMIT_SELECT) As Integer

        GetLimitData = 0

        Dim drs As DataRow() = Me.SelectKbnListDataRow(kbnCd, LMKbnConst.KBN_S054)
        If 0 < drs.Length Then
            GetLimitData = Convert.ToInt32(Convert.ToDouble(drs(0).Item("VALUE1")))
        End If

        Return GetLimitData

    End Function

#End Region

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKbnListDataRow(ByVal kbnCd As String _
                                         , ByVal groupCd As String _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(kbnCd, groupCd))

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal kbnCd As String _
                                     , ByVal groupCd As String _
                                     ) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")

        '区分コード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_CD = ", " '", kbnCd, "' ")

        '区分グループコード
        SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        Return SelectKbnString

    End Function

#End Region

#Region "Spread Enterイベント"

    ''' <summary>
    ''' Enter押下イベントの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEnterEvent(ByVal frm As Form)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        'ENTER時にセルを右移動させる
        Call Me.SetSpreadEnterEvent(frm)

    End Sub

    ''' <summary>
    ''' Spread上でのEnter押下処理
    ''' </summary>
    ''' <remarks></remarks>
    Private Sub SetSpreadEnterEvent(ByVal frm As Form)

        'フォーム内のSpreadを取得
        Dim arr As ArrayList = New ArrayList()
        arr = New ArrayList()
        Me.GetTarget(Of Win.Spread.LMSpread)(arr, frm)
        Dim im As New FarPoint.Win.Spread.InputMap

        For Each spr As Win.Spread.LMSpread In arr

            ' 非編集セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

            '編集中セルでの[Enter]キーを「次列へ移動」とします
            im = spr.GetInputMap(FarPoint.Win.Spread.InputMapMode.WhenAncestorOfFocused)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.None), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)
            im.Put(New FarPoint.Win.Spread.Keystroke(Keys.Enter, Keys.Shift), FarPoint.Win.Spread.SpreadActions.MoveToNextColumnWrap)

        Next

    End Sub

#End Region 'Spread Enterイベント

End Class
