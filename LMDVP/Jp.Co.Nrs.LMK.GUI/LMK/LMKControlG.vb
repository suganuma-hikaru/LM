' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMK          : 請求サブシステム
'  プログラムID     :  LMKControlG  : 請求共通処理
'  作  成  者       :  [熊本史子]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMKControl画面クラス
''' </summary>
''' <remarks></remarks>
Public Class LMKControlG
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
    Friend Sub New(ByVal frm As Form)

        '親クラスのコンストラクタを呼びます。
        MyBase.new()

        '入力チェックをするフォームをこのクラスに紐付けます。
        Me.MyForm = frm

        Me._Frm = frm

    End Sub

#End Region

#Region "メソッド"

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

        If value.Equals(LMKControlC.YN_FLG_YES) Then
            ctl.SetBinaryValue(LMConst.FLG.ON)
        ElseIf value.Equals(LMKControlC.YN_FLG_NO) Then
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

    ''' <summary>
    ''' 2つの値を連結して設定
    ''' </summary>
    ''' <param name="value1">値1</param>
    ''' <param name="value2">値2</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Friend Function EditConcatData(ByVal value1 As String, ByVal value2 As String, ByVal str As String) As String

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
    ''' 荷主名称設定
    ''' </summary>
    ''' <remarks></remarks>
    Friend Function GetCustNm(ByVal custcdl As String, _
                           ByVal custcdm As String, Optional ByVal custcds As String = "", _
                           Optional ByVal custcdss As String = "") As ArrayList

        Dim cust As ArrayList = New ArrayList
        Dim custNmwher As String = String.Empty

        '荷主コード（大）
        If String.IsNullOrEmpty(custcdl) = False Then

            '荷主コード（中）
            If String.IsNullOrEmpty(custcdm) = True Then

                custcdm = "00"
            End If
            '荷主コード（小）
            If String.IsNullOrEmpty(custcds) = True Then

                custcds = "00"
            End If

            '荷主コード（極小）
            If String.IsNullOrEmpty(custcdss) = True Then

                custcdss = "00"
            End If

        Else
            '荷主コード（大）が未入力の場合、空返却する
            Return cust

        End If

        Dim custDr As DataRow() = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custcdl, custcdm, custcds, custcdss))

        If custDr.Length = 0 Then
            'キャッシュより取得できない場合、空返却する
            Return cust
        End If

        '荷主名称設定
        cust.Add(String.Concat(custDr(0).Item("CUST_NM_L").ToString(), " ", _
                             custDr(0).Item("CUST_NM_M").ToString()))
        cust.Add(String.Concat(custDr(0).Item("CUST_NM_S").ToString(), " ", _
                             custDr(0).Item("CUST_NM_SS").ToString()))
        cust.Add(custDr(0).Item("CUST_CD_L").ToString())
        cust.Add(custDr(0).Item("CUST_CD_M").ToString())
        cust.Add(custDr(0).Item("CUST_CD_S").ToString())
        cust.Add(custDr(0).Item("CUST_CD_SS").ToString())
        cust.Add(custDr(0).Item("CUST_NM_L").ToString())
        cust.Add(custDr(0).Item("CUST_NM_M").ToString())
        cust.Add(custDr(0).Item("CUST_NM_S").ToString())
        cust.Add(custDr(0).Item("CUST_NM_SS").ToString())


        Return cust

    End Function

    ''' <summary>
    ''' 請求先名称の取得
    ''' </summary>
    ''' <param name="sekySaki"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Friend Function GetSeqNm(ByVal sekySaki As String) As ArrayList

        Dim sekysakiCdNm As ArrayList = New ArrayList

        '請求先名称の設定
        If String.IsNullOrEmpty(sekySaki) = False Then

            Dim Seiqdr As DataRow() = Nothing

            Seiqdr = MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(String.Concat("SEIQTO_CD = ", " '", sekySaki, "' "))

            If Seiqdr.Count < 1 = False Then
                sekysakiCdNm.Add(Seiqdr(0).Item("SEIQTO_NM").ToString())
                sekysakiCdNm.Add(Seiqdr(0).Item("SEIQTO_CD").ToString())
                sekysakiCdNm.Add(Seiqdr(0).Item("KOUZA_KB").ToString())
                sekysakiCdNm.Add(Seiqdr(0).Item("KOUZA_KB_NM").ToString())
                sekysakiCdNm.Add(Seiqdr(0).Item("TOTAL_NR").ToString())
                sekysakiCdNm.Add(Seiqdr(0).Item("TOTAL_NG").ToString())
            End If
        End If

        Return sekysakiCdNm

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '荷主コード（大）
        SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        If String.IsNullOrEmpty(custMCd) = False Then

            '荷主コード（中）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        End If

        If String.IsNullOrEmpty(custSCd) = False Then

            '荷主コード（小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_S = ", " '", custSCd, "' ")

        End If

        If String.IsNullOrEmpty(custSSCd) = False Then

            '荷主コード（極小）
            SelectCustString = String.Concat(SelectCustString, " AND ", "CUST_CD_SS = ", " '", custSSCd, "' ")

        End If

        Return SelectCustString

    End Function

    ''' <summary>
    ''' 年月日付の設定
    ''' </summary>
    ''' <param name="strDate">加算対象日付</param>
    ''' <param name="addCnt">加算月</param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Friend Function SetControlDate(ByVal strDate As String, ByVal addCnt As Integer) As String


        Dim strYear As String = String.Empty
        Dim strMonth As String = String.Empty

        'システム日付を引数より加算
        strDate = Convert.ToString(DateSerial(Convert.ToInt32(strDate.Substring(0, 4)), _
                                                Convert.ToInt32(strDate.Substring(4, 2)) + addCnt, _
                                                Convert.ToInt32(strDate.Substring(6, 2))))
        '年月の編集
        strYear = Convert.ToString(strDate).Substring(0, 4)
        strMonth = Convert.ToString(strDate).Substring(5, 2)


        Return String.Concat(strYear, strMonth)

    End Function

#End Region

End Class
