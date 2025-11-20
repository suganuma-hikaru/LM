' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF          : 運送サブ
'  プログラムID     :  LMFControlG  : LMF画面 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports GrapeCity.Win.Editors.Fields
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.Win.GUI.Win.Interface
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports GrapeCity.Win.Editors

''' <summary>
''' LMFControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/05/09 ito
''' </histry>
Public Class LMFControlG
    Inherits Jp.Co.Nrs.LM.Base.GUI.LMBaseGUIGamen

#Region "Constructor"

    ''' <summary>
    ''' コンストラクタ
    ''' </summary>
    ''' <param name="frm">フォームクラス</param>
    ''' <remarks>フォームラスとハンドラクラスをこのクラスに紐付けます。編集不可</remarks>
    Public Sub New(ByVal frm As Form)

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
    ''' <param name="lock">ロックフラグ　True:ロック　False:ロック解除</param>
    ''' <param name="clearFlg">クリアフラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Public Sub SetLockInputMan(ByVal ctl As Win.Interface.ILMEditableControl _
                               , ByVal lock As Boolean _
                               , Optional ByVal clearFlg As Boolean = True _
                               )

        Dim cleraData As LMFControlC.CLERA_DATA = LMFControlC.CLERA_DATA.ISNULL
        Dim cmb As Win.InputMan.LMImCombo = Nothing
        Dim kbn As Win.InputMan.LMComboKubun = Nothing
        Dim nrs As Win.InputMan.LMComboNrsBr = Nothing
        Dim sok As Win.InputMan.LMComboSoko = Nothing
        Dim num As Win.InputMan.LMImNumber = Nothing
        Dim imd As Win.InputMan.LMImDate = Nothing

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            cmb = DirectCast(ctl, Win.InputMan.LMImCombo)
            cmb.ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMCOMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            kbn = DirectCast(ctl, Win.InputMan.LMComboKubun)
            kbn.ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMKBN_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            nrs = DirectCast(ctl, Win.InputMan.LMComboNrsBr)
            nrs.ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMNRS_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            sok = DirectCast(ctl, Win.InputMan.LMComboSoko)
            sok.ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMSOK_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            num = DirectCast(ctl, Win.InputMan.LMImNumber)
            num.ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMNUMBER

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMTEXT

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            imd = DirectCast(ctl, Win.InputMan.LMImDate)
            imd.ReadOnly = lock
            cleraData = LMFControlC.CLERA_DATA.IMDATE

        End If

        'ロックする場合は値をクリア
        If lock = True AndAlso clearFlg = True Then

            Select Case cleraData

                Case LMFControlC.CLERA_DATA.IMTEXT

                    ctl.TextValue = String.Empty

                Case LMFControlC.CLERA_DATA.IMCOMB

                    cmb.SelectedValue = Nothing

                Case LMFControlC.CLERA_DATA.IMKBN_COMB

                    kbn.SelectedValue = Nothing

                Case LMFControlC.CLERA_DATA.IMNRS_COMB

                    nrs.SelectedValue = Nothing

                Case LMFControlC.CLERA_DATA.IMSOK_COMB

                    sok.SelectedValue = Nothing

                Case LMFControlC.CLERA_DATA.IMDATE

                    imd.Value = Nothing

                Case LMFControlC.CLERA_DATA.IMNUMBER

                    num.Value = 0

            End Select

        End If

    End Sub

    ''' <summary>
    ''' ロック切り替え処理
    ''' </summary>
    ''' <param name="ctl">コントロール(InputMan以外)</param>
    ''' <param name="lock">ロックフラグ　True:ロック　False:ロック解除</param>
    ''' <remarks></remarks>
    Friend Sub SetLockControl(ByVal ctl As Control, ByVal lock As Boolean)

        ctl.Enabled = Not lock

    End Sub

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="holidayFlg">休日マスタ反映フラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate, Optional ByVal holidayFlg As Boolean = True)

        ctl.Format = DateFieldsBuilder.BuildFields(LMFControlC.DATE_YYYYMMDD)
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields(LMFControlC.DATE_SLASH_YYYYMMDD)
        ctl.Holiday = holidayFlg

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
    ''' <remarks></remarks>
    Friend Sub CreateComboBox(ByVal cmb As LMImCombo _
                              , ByVal cacheTbl As String _
                              , ByVal cdNm As String() _
                              , ByVal itemNm As String() _
                              , ByVal sql As String _
                              , ByVal sort As String _
                              )

        'リストのクリア
        cmb.Items.Clear()

        Dim cd As String = String.Empty
        Dim item As String = String.Empty

        '空行追加
        cmb.Items.Add(New ListItem(New SubItem() {New SubItem(cd), New SubItem(item)}))

        'マスタ検索処理
        Dim drs As DataRow() = MyBase.GetLMCachedDataTable(cacheTbl).Select(sql, sort)

        Dim max As Integer = drs.Length - 1
        For i As Integer = 0 To max

            cd = Me.SetCombData(drs(i), cdNm)
            item = Me.SetCombData(drs(i), itemNm)

            'アイテム追加
            cmb.Items.Add(New ListItem(New SubItem() {New SubItem(item), New SubItem(cd)}))

        Next

    End Sub

    ''' <summary>
    ''' コンボに設定する文字を作成
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <param name="str">文字型配列</param>
    ''' <returns>設定文字</returns>
    ''' <remarks></remarks>
    Private Function SetCombData(ByVal dr As DataRow, ByVal str As String()) As String

        SetCombData = String.Empty
        Dim max As Integer = str.Length - 1
        For i As Integer = 0 To max
            SetCombData = String.Concat(SetCombData, dr.Item(str(i)).ToString())
        Next

        Return SetCombData

    End Function

    ''' <summary>
    ''' セルをロック
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <param name="rowNo">行番号</param>
    ''' <param name="colNo">列番号</param>
    ''' <remarks></remarks>
    Friend Sub SetLockCell(ByVal spr As Win.Spread.LMSpread, ByVal rowNo As Integer, ByVal colNo As Integer)

        With spr.ActiveSheet

            Dim aCell As FarPoint.Win.Spread.Cell = .Cells(rowNo, colNo)
            aCell.Locked = True
            aCell.BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()

        End With

    End Sub

    ''' <summary>
    ''' 背景色クリア処理(Enter)
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="eventFlg">イベントフラグ</param>
    ''' <remarks></remarks>
    Private Sub ClearBackColorDef(ByVal ctl As Control, ByVal eventFlg As Boolean)

        If eventFlg = False Then
            Exit Sub
        End If

        '背景色クリア
        Call Me.ClearBackColorDef(ctl)

    End Sub

    ''' <summary>
    ''' 背景色クリア処理
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <remarks></remarks>
    Private Sub ClearBackColorDef(ByVal ctl As Control)

        '編集コントロール
        Dim arr As ArrayList = New ArrayList()
        Me.GetTarget(Of IEditableControl)(arr, ctl)
        For Each arrCtl As IEditableControl In arr

            '背景色赤でない場合、スルー
            If arrCtl.BackColorDef <> Utility.LMGUIUtility.GetAttentionBackColor() Then
                Continue For
            End If

            'ロックの場合、ロックの背景色
            If arrCtl.ReadOnlyStatus = True OrElse arrCtl.EnableStatus = False Then
                arrCtl.BackColorDef = Utility.LMGUIUtility.GetReadOnlyBackColor()
            Else
                arrCtl.BackColorDef = Utility.LMGUIUtility.GetSystemInputBackColor()
            End If

        Next

        'スプレッド
        arr = New ArrayList()
        Me.GetTarget(Of Win.Spread.LMSpread)(arr, ctl)
        For Each spr As Win.Spread.LMSpread In arr
            Call Me.ClearBackColorDef(spr)
        Next

    End Sub

    ''' <summary>
    ''' 背景色クリア処理(スプレッド)
    ''' </summary>
    ''' <param name="spr">スプレッド</param>
    ''' <remarks></remarks>
    Private Sub ClearBackColorDef(ByVal spr As Win.Spread.LMSpread)

        With spr.ActiveSheet

            Dim rowMax As Integer = .Rows.Count - 1
            Dim colMax As Integer = .Columns.Count - 1
            For i As Integer = 0 To rowMax

                For j As Integer = 0 To colMax

                    Dim aCell As Cell = .Cells(i, j)

                    '背景色赤でない場合、スルー
                    If aCell.BackColor <> Utility.LMGUIUtility.GetAttentionBackColor() Then
                        Continue For
                    End If

                    'ロックセルの場合、ロックの背景色
                    If aCell.Locked = True Then
                        aCell.BackColor = Utility.LMGUIUtility.GetReadOnlyBackColor()
                    Else
                        aCell.BackColor = Utility.LMGUIUtility.GetSystemInputBackColor()
                    End If

                Next

            Next

        End With

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
                Me.GetTarget(Of T)(arr, targetControl)
            Next
        End If

    End Sub

#End Region

#Region "スプレッド"

    ''' <summary>
    ''' セルから値を取得
    ''' </summary>
    ''' <param name="aCell">セル</param>
    ''' <returns>取得した値</returns>
    ''' <remarks></remarks>
    Public Function GetCellValue(ByVal aCell As Cell) As String

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
    Public Function GetCheckList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

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
    ''' <param name="kbnGroup">区分グループコード　初期値 = "S054"</param>
    ''' <returns>閾値</returns>
    ''' <remarks></remarks>
    Public Function GetLimitData(Optional ByVal kbnCd As String = LMFControlC.LIMIT_SELECT, Optional ByVal kbnGroup As String = LMKbnConst.KBN_S054) As Integer

        GetLimitData = 0

        Dim drs As DataRow() = Me.SelectKbnListDataRow(kbnCd, kbnGroup)
        If 0 < drs.Length Then
            GetLimitData = Convert.ToInt32(Convert.ToDouble(drs(0).Item("VALUE1")))
        End If

        Return GetLimitData

    End Function

    ''' <summary>
    ''' NULLの場合、ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <remarks></remarks>
    Public Function FormatNumValue(ByVal value As String) As String

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
    ''' <param name="str"></param>
    ''' <returns>編集後の値</returns>
    ''' <remarks></remarks>
    Public Function EditConcatData(ByVal value1 As String, ByVal value2 As String, Optional ByVal str As String = " - ") As String

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
    ''' 前ゼロを設定
    ''' </summary>
    ''' <param name="value">値</param>
    ''' <param name="ketasu">設定桁数</param>
    ''' <param name="keta">前ゼロの文字</param>
    ''' <returns>前ゼロつき設定値</returns>
    ''' <remarks></remarks>
    Public Function SetMaeZeroData(ByVal value As String, ByVal ketasu As Integer, Optional ByVal keta As String = "") As String

        If String.IsNullOrEmpty(keta) = True Then
            keta = Me.GetZeroData(ketasu)
        End If

        SetMaeZeroData = String.Concat(keta, value)

        Return SetMaeZeroData.Substring(SetMaeZeroData.Length - ketasu, ketasu)

    End Function

    ''' <summary>
    ''' 前ゼロするときの数を取得
    ''' </summary>
    ''' <param name="ketasu">設定桁数</param>
    ''' <returns>ゼロデータ</returns>
    ''' <remarks></remarks>
    Public Function GetZeroData(ByVal ketasu As Integer) As String

        GetZeroData = String.Empty
        Dim max As Integer = ketasu - 1
        For index As Integer = 0 To max
            GetZeroData = String.Concat(GetZeroData, LMConst.FLG.OFF)
        Next

        Return GetZeroData

    End Function

#End Region

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Public Function SelectCustListDataRow(ByVal brCd As String _
                                          , ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd))
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(brCd, custLCd, custMCd, custSCd, custSSCd))
        '20160928 要番2622 tsunehira add 
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
    Private Function SelectCustString(ByVal brCd As String _
                                     , ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '20160928 要番2622 tsunehira add start
        '営業所コード
        SelectCustString = String.Concat(SelectCustString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")
        '20160928 要番2622 tsunehira add end

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
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByVal brCd As String, ByVal unsoCd As String, Optional ByVal unsoBrCd As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        '        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(unsoCd, unsoBrCd))
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(brCd, unsoCd, unsoBrCd))
        '20160928 要番2622 tsunehira add end

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoString(ByVal brCd As String, ByVal unsoCd As String, Optional ByVal unsoBrCd As String = "") As String

        SelectUnsocoString = String.Empty

        '削除フラグ
        SelectUnsocoString = String.Concat(SelectUnsocoString, " SYS_DEL_FLG = '0' ")

        '20160928 要番2622 tsunehira add start
        '営業所コード
        SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")
        '20160928 要番2622 tsunehira add end

        '運送会社コード
        SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_CD = ", " '", unsoCd, "' ")

        '運送会社支店コード
        If String.IsNullOrEmpty(unsoBrCd) = False Then
            SelectUnsocoString = String.Concat(SelectUnsocoString, " AND ", "UNSOCO_BR_CD = ", " '", unsoBrCd, "' ")
        End If

        Return SelectUnsocoString

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Public Function SelectKbnListDataRow(ByVal kbnCd As String _
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

    ''' <summary>
    ''' 担当者別荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Public Function SelectTCustListDataRow(ByVal userId As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.TCUST).Select(Me.SelectTcustString(userId))

    End Function

    ''' <summary>
    ''' 担当者別荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectTcustString(ByVal userId As String) As String

        SelectTcustString = String.Empty

        '削除フラグ
        SelectTcustString = String.Concat(SelectTcustString, " SYS_DEL_FLG = '0' ")

        'ユーザコード
        SelectTcustString = String.Concat(SelectTcustString, " AND ", "USER_CD = ", " '", userId, "' ")

        '初期荷主該当フラグ(ON)
        SelectTcustString = String.Concat(SelectTcustString, " AND ", "DEFAULT_CUST_YN = ", " '", LMFControlC.FLG_ON, "' ")

        Return SelectTcustString

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Public Function SelectUnchinTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(Me.SelectUnchinTariffString(tariffCd, tariffCdEda, startDate), "STR_DATE Desc")

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              ) As String

        SelectUnchinTariffString = String.Empty

        '運賃タリフコード
        SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, "UNCHIN_TARIFF_CD = ", " '", tariffCd, "' ")

        '運賃タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "UNCHIN_TARIFF_CD_EDA = ", " '", Me.SetMaeZeroData(tariffCdEda, 3), "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "STR_DATE <= ", " '", startDate, "' ")
        End If

        Return SelectUnchinTariffString

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectYokoTariffListDataRow(ByVal brCd As String, ByVal tariffCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD).Select(Me.SelectYokoTariffString(brCd, tariffCd))

    End Function

    ''' <summary>
    ''' 横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectYokoTariffString(ByVal brCd As String, ByVal tariffCd As String) As String

        SelectYokoTariffString = String.Empty

        '削除フラグ
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '横持ちタリフコード
        SelectYokoTariffString = String.Concat(SelectYokoTariffString, " AND ", "YOKO_TARIFF_CD = ", " '", tariffCd, "' ")

        Return SelectYokoTariffString

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Public Function SelectExtcUnchinListDataRow(ByVal brCd As String _
                                                , ByVal extcCd As String _
                                                , ByVal jisCd As String _
                                                ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EXTC_UNCHIN).Select(Me.SelectExtcUnchinString(brCd, extcCd, jisCd))

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectExtcUnchinString(ByVal brCd As String _
                                            , ByVal extcCd As String _
                                            , ByVal jisCd As String _
                                            ) As String

        SelectExtcUnchinString = String.Empty

        '営業所
        SelectExtcUnchinString = String.Concat(SelectExtcUnchinString, " NRS_BR_CD = ", " '", brCd, "' ")

        '割増タリフコード
        SelectExtcUnchinString = String.Concat(SelectExtcUnchinString, " AND ", "EXTC_TARIFF_CD = ", " '", extcCd, "' ")

        Return SelectExtcUnchinString

    End Function

    'START YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等
    ''' <summary>
    ''' 支払タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAI_TARIFF).Select(Me.SelectShiharaiTariffString(tariffCd, tariffCdEda, startDate), "STR_DATE Desc")

    End Function

    ''' <summary>
    ''' 支払タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              ) As String

        SelectShiharaiTariffString = String.Empty

        '支払タリフコード
        SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, "SHIHARAI_TARIFF_CD = ", " '", tariffCd, "' ")

        '支払タリフコード枝番
        If String.IsNullOrEmpty(tariffCdEda) = False Then
            SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " AND ", "SHIHARAI_TARIFF_CD_EDA = ", " '", Me.SetMaeZeroData(tariffCdEda, 3), "' ")
        End If

        '適用開始日
        If String.IsNullOrEmpty(startDate) = False Then
            SelectShiharaiTariffString = String.Concat(SelectShiharaiTariffString, " AND ", "STR_DATE <= ", " '", startDate, "' ")
        End If

        Return SelectShiharaiTariffString

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaiYokoTariffListDataRow(ByVal brCd As String, ByVal tariffCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.YOKO_TARIFF_HD_SHIHARAI).Select(Me.SelectShiharaiYokoTariffString(brCd, tariffCd))

    End Function

    ''' <summary>
    ''' 支払横持ちタリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="tariffCd">横持ちタリフコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaiYokoTariffString(ByVal brCd As String, ByVal tariffCd As String) As String

        SelectShiharaiYokoTariffString = String.Empty

        '営業所コード
        SelectShiharaiYokoTariffString = String.Concat(SelectShiharaiYokoTariffString, "NRS_BR_CD = ", " '", brCd, "' ")

        '横持ちタリフコード
        SelectShiharaiYokoTariffString = String.Concat(SelectShiharaiYokoTariffString, " AND ", "YOKO_TARIFF_CD = ", " '", tariffCd, "' ")

        Return SelectShiharaiYokoTariffString

    End Function

    ''' <summary>
    ''' 支払割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectExtcShiharaiListDataRow(ByVal brCd As String _
                                                , ByVal extcCd As String _
                                                , ByVal jisCd As String _
                                                ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.EXTC_SHIHARAI).Select(Me.SelectExtcShiharaiString(brCd, extcCd, jisCd))

    End Function

    ''' <summary>
    ''' 割増タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="extcCd">割増タリフコード</param>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectExtcShiharaiString(ByVal brCd As String _
                                            , ByVal extcCd As String _
                                            , ByVal jisCd As String _
                                            ) As String

        SelectExtcShiharaiString = String.Empty

        '営業所
        SelectExtcShiharaiString = String.Concat(SelectExtcShiharaiString, " NRS_BR_CD = ", " '", brCd, "' ")

        '割増タリフコード
        SelectExtcShiharaiString = String.Concat(SelectExtcShiharaiString, " AND ", "EXTC_TARIFF_CD = ", " '", extcCd, "' ")

        Return SelectExtcShiharaiString

    End Function

    ''' <summary>
    ''' 支払先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="shiharaitoCd">支払先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectShiharaitoListDataRow(ByVal shiharaitoCd As String _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SHIHARAITO).Select(Me.SelectShiharaitoString(shiharaitoCd))

    End Function

    ''' <summary>
    ''' 支払先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="shiharaitoCd">支払先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectShiharaitoString(ByVal shiharaitoCd As String _
                                             ) As String

        SelectShiharaitoString = String.Empty

        '支払先コード
        SelectShiharaitoString = String.Concat(SelectShiharaitoString, "SHIHARAITO_CD = ", " '", shiharaitoCd, "' ")

        Return SelectShiharaitoString

    End Function
    'END YANAI 要望番号1302 支払運賃に伴う修正。バッチ呼び出し、画面項目追加等

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    ''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    ''' <param name="yokoCd">横持ちタリフコード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTariffSetListDataRow(ByVal brCd As String _
                                               , ByVal custLCd As String _
                                               , ByVal custMCd As String _
                                               , ByVal tariffKbn As String _
                                               , ByVal tariffCd1 As String _
                                               , ByVal tariffCd2 As String _
                                               , ByVal yokoCd As String _
                                               , Optional ByVal setCd As String = "" _
                                               , Optional ByVal destCd As String = "" _
                                               ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(Me.SelectTariffSetString(brCd, custLCd, custMCd, setCd, tariffKbn, tariffCd1, tariffCd2, yokoCd, destCd))

    End Function

    ''' <summary>
    ''' 運賃タリフセットマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="setCd">セットマスタコード</param>
    ''' <param name="tariffKbn">タリフ分類区分</param>
    ''' <param name="tariffCd1">運賃タリフコード（屯キロ建）</param>
    ''' <param name="tariffCd2">運賃タリフコード（車建）</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Private Function SelectTariffSetString(ByVal brCd As String _
                                           , ByVal custLCd As String _
                                           , ByVal custMCd As String _
                                           , ByVal setCd As String _
                                           , ByVal tariffKbn As String _
                                           , ByVal tariffCd1 As String _
                                           , ByVal tariffCd2 As String _
                                           , ByVal yokoCd As String _
                                           , ByVal destCd As String _
                                           ) As String

        SelectTariffSetString = String.Empty

        '営業所コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " NRS_BR_CD = ", " '", brCd, "' ")

        '荷主(大)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '荷主(中)コード
        SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "CUST_CD_M = ", " '", custMCd, "' ")

        'セット区分
        If String.IsNullOrEmpty(setCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "SET_KB = ", " '", setCd, "' ")

        End If

        'タリフコード1
        If String.IsNullOrEmpty(tariffCd1) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD1 = ", " '", tariffCd1, "' ")

        End If

        'タリフコード2
        If String.IsNullOrEmpty(tariffCd2) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "UNCHIN_TARIFF_CD2 = ", " '", tariffCd2, "' ")

        End If

        '横持ちタリフコード
        If String.IsNullOrEmpty(yokoCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "YOKO_TARIFF_CD = ", " '", yokoCd, "' ")

        End If

        '届先コード
        If String.IsNullOrEmpty(destCd) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        End If

        'タリフ分類区分
        If String.IsNullOrEmpty(tariffKbn) = False Then

            SelectTariffSetString = String.Concat(SelectTariffSetString, " AND ", "TARIFF_BUNRUI_KB = ", " '", tariffKbn, "' ")

        End If

        Return SelectTariffSetString

    End Function

    ''' <summary>
    ''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="goodsCd">商品KEY</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectGoodsListDataRow(ByVal brCd As String _
                                              , ByVal goodsCd As String _
                                              , ByVal custCd_l As String _
                                              , ByVal custCd_m As String _
                                              ) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(Me.SelectGoodsString(brCd, goodsCd))

        Dim goodsDs As MGoodsDS = New MGoodsDS
        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
        goodsDr.Item("NRS_BR_CD") = brCd
        goodsDr.Item("CUST_CD_L") = custCd_l
        goodsDr.Item("CUST_CD_M") = custCd_m
        goodsDr.Item("GOODS_CD_NRS") = goodsCd
        goodsDr.Item("SYS_DEL_FLG") = "0"
        goodsDs.Tables(LMConst.CacheTBL.GOODS).Rows.Add(goodsDr)
        Dim rtnDs As DataSet = MyBase.GetGoodsMasterData(goodsDs)
        Return rtnDs.Tables(LMConst.CacheTBL.GOODS).Select
        '---↑

    End Function

    ''' <summary>
    ''' 商品マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="goodsCd">商品KEY</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectGoodsString(ByVal brCd As String _
                                      , ByVal goodsCd As String _
                                      ) As String

        SelectGoodsString = String.Empty

        '削除フラグ
        SelectGoodsString = String.Concat(SelectGoodsString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '商品KEY
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "GOODS_CD_NRS = ", " '", goodsCd, "' ")

        Return SelectGoodsString

    End Function

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

    ''' <summary>
    ''' JISマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectJisListDataRow(ByVal jisCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.JIS).Select(Me.SelectJisString(jisCd))

    End Function

    ''' <summary>
    ''' JISマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="jisCd">JISコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectJisString(ByVal jisCd As String) As String

        SelectJisString = String.Empty

        '削除フラグ
        SelectJisString = String.Concat(SelectJisString, " SYS_DEL_FLG = '0' ")

        'JISコード
        SelectJisString = String.Concat(SelectJisString, " AND ", "JIS_CD = ", " '", jisCd, "' ")

        Return SelectJisString

    End Function

    ''' <summary>
    ''' 乗務員マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="driverCd">乗務員コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDriverListDataRow(ByVal driverCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DRIVER).Select(Me.SelectDriverString(driverCd))

    End Function

    ''' <summary>
    ''' 乗務員マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="driverCd">乗務員コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectDriverString(ByVal driverCd As String) As String

        SelectDriverString = String.Empty

        '削除フラグ
        SelectDriverString = " SYS_DEL_FLG = '0' "

        '乗務員コード
        SelectDriverString = String.Concat(SelectDriverString, " AND DRIVER_CD = ", " '", driverCd, "' ")

        Return SelectDriverString

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSeiqtoListDataRow(ByVal brCd As String, ByVal seqtoCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEIQTO).Select(Me.SelectSeiqtoString(brCd, seqtoCd))

    End Function

    ''' <summary>
    ''' 請求先タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="seqtoCd">請求先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSeiqtoString(ByVal brCd As String, ByVal seqtoCd As String) As String

        SelectSeiqtoString = String.Empty

        '削除フラグ
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '請求先タリフコード
        SelectSeiqtoString = String.Concat(SelectSeiqtoString, " AND ", "SEIQTO_CD = ", " '", seqtoCd, "' ")

        Return SelectSeiqtoString

    End Function

#End Region

#End Region

End Class
