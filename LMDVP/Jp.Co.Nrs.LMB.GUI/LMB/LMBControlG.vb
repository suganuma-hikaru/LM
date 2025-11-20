' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB          : 
'  プログラムID     :  LMBControlG  : LMB編集画面 共通処理
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
Imports GrapeCity.Win.Editors

''' <summary>
''' LMBControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/03/01 SUZUKI
''' </histry>
Public Class LMBControlG
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Friend Sub New(ByVal frm As Form)

        '親クラスのコンストラクタを呼びます。
        MyBase.New()

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

    End Sub

#End Region

#Region "Method"

#Region "コントロール"

    ''' <summary>
    ''' ロック切り替え処理
    ''' </summary>
    ''' <param name="ctl">コントロール(InputManのみ)</param>
    ''' <param name="lock">ロックフラグ</param>
    ''' <param name="clearFlg">クリアフラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockInputMan(ByVal ctl As Win.Interface.ILMEditableControl, ByVal lock As Boolean, Optional ByVal clearFlg As Boolean = True)

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            DirectCast(ctl, Win.InputMan.LMImCombo).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            DirectCast(ctl, Win.InputMan.LMComboKubun).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            DirectCast(ctl, Win.InputMan.LMComboNrsBr).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            DirectCast(ctl, Win.InputMan.LMComboSoko).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            DirectCast(ctl, Win.InputMan.LMImNumber).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = lock

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            DirectCast(ctl, Win.InputMan.LMImDate).ReadOnly = lock

        End If

        'ロックする場合は値をクリア
        If lock = True AndAlso clearFlg = True Then
            ctl.TextValue = String.Empty
        End If

    End Sub

    ''' <summary>
    ''' ロック切り替え処理
    ''' </summary>
    ''' <param name="ctl">コントロール(InputMan以外)</param>
    ''' <param name="lock">ロックフラグ</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, ByVal lock As Boolean)

        ctl.Enabled = Not lock

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
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("yyyyMMdd")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("yyyy/MM/dd")
        If ctl.Holiday <> True Then
            ctl.Holiday = True
        End If

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat2(ByVal ctl As LMImDate)

        ctl.Format = DateFieldsBuilder.BuildFields("MMdd")
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields("MM/dd")
        If ctl.Holiday <> True Then
            ctl.Holiday = True
        End If

    End Sub

#End Region

#Region "スプレッド"

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

#End Region

#Region "Spread Enterイベント"

    ''' <summary>
    ''' Enter押下イベントの設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Sub SetEnterEvent(ByVal frm As Form)

        'キーイベントをフォームで受け取る
        frm.KeyPreview = True

        '2011/08/23  福田　共通動作(右セル移動不可) スタート

        'ENTER時にセルを右移動させる
        Call Me.SetSpreadEnterEvent(frm)

        '2011/08/23  福田　共通動作(右セル移動不可) エンド

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
                Call Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

#End Region 'Spread Enterイベント

#End Region

End Class
