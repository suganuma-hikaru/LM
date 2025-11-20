' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMH          : EDI
'  プログラムID     :  LMHControlG  : LMH画面 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports GrapeCity.Win.Editors.Fields
Imports FarPoint.Win.Spread
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports GrapeCity.Win.Editors

''' <summary>
''' LMHControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/05/09 ito
''' </histry>
Public Class LMHControlG
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
    ''' <param name="lock">ロックフラグ　True:ロック　False:ロック解除</param>
    ''' <param name="clearFlg">クリアフラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Friend Sub SetLockInputMan(ByVal ctl As Win.Interface.ILMEditableControl _
                               , ByVal lock As Boolean _
                               , Optional ByVal clearFlg As Boolean = True _
                               )

        Dim cleraData As LMHControlC.CLERA_DATA = LMHControlC.CLERA_DATA.ISNULL
        Dim cmb As Win.InputMan.LMImCombo = Nothing
        Dim kbn As Win.InputMan.LMComboKubun = Nothing
        Dim nrs As Win.InputMan.LMComboNrsBr = Nothing
        Dim sok As Win.InputMan.LMComboSoko = Nothing
        Dim num As Win.InputMan.LMImNumber = Nothing
        Dim imd As Win.InputMan.LMImDate = Nothing

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            cmb = DirectCast(ctl, Win.InputMan.LMImCombo)
            cmb.ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMCOMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            kbn = DirectCast(ctl, Win.InputMan.LMComboKubun)
            kbn.ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMKBN_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            nrs = DirectCast(ctl, Win.InputMan.LMComboNrsBr)
            nrs.ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMNRS_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            sok = DirectCast(ctl, Win.InputMan.LMComboSoko)
            sok.ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMSOK_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            num = DirectCast(ctl, Win.InputMan.LMImNumber)
            num.ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMNUMBER

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMTEXT

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            imd = DirectCast(ctl, Win.InputMan.LMImDate)
            imd.ReadOnly = lock
            cleraData = LMHControlC.CLERA_DATA.IMDATE

        End If

        'ロックする場合は値をクリア
        If lock = True AndAlso clearFlg = True Then

            Select Case cleraData

                Case LMHControlC.CLERA_DATA.IMTEXT

                    ctl.TextValue = String.Empty

                Case LMHControlC.CLERA_DATA.IMCOMB

                    cmb.SelectedValue = Nothing

                Case LMHControlC.CLERA_DATA.IMKBN_COMB

                    kbn.SelectedValue = Nothing

                Case LMHControlC.CLERA_DATA.IMNRS_COMB

                    nrs.SelectedValue = Nothing

                Case LMHControlC.CLERA_DATA.IMSOK_COMB

                    sok.SelectedValue = Nothing

                Case LMHControlC.CLERA_DATA.IMDATE

                    imd.Value = Nothing

                Case LMHControlC.CLERA_DATA.IMNUMBER

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

        ctl.Format = DateFieldsBuilder.BuildFields(LMHControlC.DATE_YYYYMMDD)
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields(LMHControlC.DATE_SLASH_YYYYMMDD)
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

    ''' <summary>
    ''' スプレッド明細行のチェックリスト(RowIndex)未選択行取得
    ''' </summary>
    ''' <param name="activeSheet">スプレッドシート</param>
    ''' <param name="defNo">レコードチェックボックスのカラム№</param>
    ''' <returns>チェックリスト</returns>
    ''' <remarks></remarks>
    Friend Function GetNotCheckList(ByVal activeSheet As SheetView, ByVal defNo As Integer) As ArrayList

        'チェック件数取得
        With activeSheet

            Dim arr As ArrayList = New ArrayList()
            Dim max As Integer = .Rows.Count - 1

            For i As Integer = 0 To max
                If LMConst.FLG.OFF.Equals(Me.GetCellValue(.Cells(i, defNo))) = True Then
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
    Friend Function GetLimitData(Optional ByVal kbnCd As String = LMHControlC.LIMIT_SELECT) As Integer

        GetLimitData = 0

        Dim drs As DataRow() = Me.SelectKbnListDataRow(kbnCd, LMKbnConst.KBN_S054)
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

#End Region

#Region "キャッシュから値取得"

    '2016.02.18 要望番号2491 修正START
    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByVal nrsBrCd As String _
                                          , ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd))
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(nrsBrCd, custLCd, custMCd, custSCd, custSSCd))

    End Function
    '2016.02.18 要望番号2491 修正END

    '2016.02.18 要望番号2491 修正START
    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectCustString(ByVal nrsBrCd As String _
                                     , ByVal custLCd As String _
                                     , ByVal custMCd As String _
                                     , ByVal custSCd As String _
                                     , ByVal custSSCd As String _
                                     ) As String

        SelectCustString = String.Empty

        '削除フラグ
        SelectCustString = String.Concat(SelectCustString, " SYS_DEL_FLG = '0' ")

        '2016.02.18 要望番号2491 修正START
        '営業所コード
        SelectCustString = String.Concat(SelectCustString, " AND ", "NRS_BR_CD = ", " '", nrsBrCd, "' ")
        '2016.02.18 要望番号2491 修正END

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
    '2016.02.18 要望番号2491 修正END

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDestListDataRow(ByVal custLCd As String, ByVal destCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.DEST).Select(Me.SelectDestString(custLCd, destCd))

        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("CUST_CD_L") = custLCd
        destMstDr.Item("DEST_CD") = destCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Return rtnDs.Tables(LMConst.CacheTBL.DEST).Select
        '---↑

    End Function

    ''' <summary>
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectDestString(ByVal custLCd As String, ByVal destCd As String) As String

        SelectDestString = String.Empty

        '削除フラグ
        SelectDestString = String.Concat(SelectDestString, " SYS_DEL_FLG = '0' ")

        '荷主コード（大）
        SelectDestString = String.Concat(SelectDestString, " AND ", "CUST_CD_L = ", " '", custLCd, "' ")

        '届先コード
        SelectDestString = String.Concat(SelectDestString, " AND ", "DEST_CD = ", " '", destCd, "' ")

        Return SelectDestString

    End Function


    ''' <summary>
    ''' ユーザーマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userCd">ユーザーコード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTantouListDataRow(ByVal userCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.USER).Select(Me.SelectTantouString(userCd))

    End Function

    ''' <summary>
    ''' ユーザーマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userCd">ユーザーコード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectTantouString(ByVal userCd As String) As String

        SelectTantouString = String.Empty

        '削除フラグ
        SelectTantouString = String.Concat(SelectTantouString, " SYS_DEL_FLG = '0' ")

        'ユーザーコード
        SelectTantouString = String.Concat(SelectTantouString, " AND ", "USER_CD = ", " '", userCd, "' ")

        Return SelectTantouString

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectUnsocoListDataRow(ByVal unsoCd As String, Optional ByVal unsoBrCd As String = "") As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNSOCO).Select(Me.SelectUnsocoString(unsoCd, unsoBrCd))

    End Function

    ''' <summary>
    ''' 運送会社マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="unsoCd">運送会社コード</param>
    ''' <param name="unsoBrCd">運送会社支店コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnsocoString(ByVal unsoCd As String, Optional ByVal unsoBrCd As String = "") As String

        SelectUnsocoString = String.Empty

        '削除フラグ
        SelectUnsocoString = String.Concat(SelectUnsocoString, " SYS_DEL_FLG = '0' ")

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

    ''' <summary>
    ''' 担当者別荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="userId">ユーザID</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectTCustListDataRow(ByVal userId As String) As DataRow()

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
        SelectTcustString = String.Concat(SelectTcustString, " AND ", "DEFAULT_CUST_YN = ", " '", LMHControlC.FLG_ON, "' ")

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
    Friend Function SelectUnchinTariffListDataRow(ByVal tariffCd As String _
                                                  , Optional ByVal tariffCdEda As String = "" _
                                                  , Optional ByVal startDate As String = "" _
                                                  , Optional ByVal dataTp As String = "" _
                                                  ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF).Select(Me.SelectUnchinTariffString(tariffCd, tariffCdEda, startDate, dataTp), "STR_DATE Desc")

    End Function

    ''' <summary>
    ''' 運賃タリフマスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="tariffCd">運賃タリフコード</param>
    ''' <param name="tariffCdEda">運賃タリフコード枝番</param>
    ''' <param name="startDate">適用開始日</param>
    ''' <param name="dataTp">データタイプ</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinTariffString(ByVal tariffCd As String _
                                              , Optional ByVal tariffCdEda As String = "" _
                                              , Optional ByVal startDate As String = "" _
                                              , Optional ByVal dataTp As String = "" _
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

        'データタイプ
        If String.IsNullOrEmpty(dataTp) = False Then
            'SelectUnchinTariffString = String.Concat(SelectUnchinTariffString, " AND ", "DATA_TP = ", " '", dataTp, "' ")
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
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.UNCHIN_TARIFF_SET).Select(Me.SelectTariffSetString(brCd, custLCd, custMCd, setCd, tariffKbn, tariffCd1, tariffCd2, yokoCd, destCd), "SET_MST_CD")

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
    ''' <param name="goodsKey">商品KEY</param>
    ''' <param name="goodsCd">商品コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectGoodsListDataRow(ByVal brCd As String _
                                              , ByVal goodsKey As String _
                                              , ByVal goodsCd As String _
                                              , ByVal custCdL As String _
                                              , ByVal custCdM As String _
                                              ) As DataRow()

        'キャッシュテーブルからデータ抽出
        '---↓
        'Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.GOODS).Select(Me.SelectGoodsString(brCd, goodsKey, goodsCd, custCdL, custCdM))

        Dim goodsDs As MGoodsDS = New MGoodsDS
        Dim goodsDr As DataRow = goodsDs.Tables(LMConst.CacheTBL.GOODS).NewRow()
        goodsDr.Item("NRS_BR_CD") = brCd
        goodsDr.Item("CUST_CD_L") = custCdL
        goodsDr.Item("CUST_CD_M") = custCdM
        If String.IsNullOrEmpty(goodsKey) = False Then goodsDr.Item("GOODS_CD_NRS") = goodsKey
        If String.IsNullOrEmpty(goodsCd) = False Then goodsDr.Item("GOODS_CD_CUST") = goodsCd
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
    ''' <param name="goodsKey">商品KEY</param>
    ''' <param name="goodsCd">商品コード</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectGoodsString(ByVal brCd As String _
                                      , ByVal goodsKey As String _
                                      , ByVal goodsCd As String _
                                      , ByVal custCdL As String _
                                      , ByVal custCdM As String _
                                      ) As String

        SelectGoodsString = String.Empty

        '削除フラグ
        SelectGoodsString = String.Concat(SelectGoodsString, " SYS_DEL_FLG = '0' ")

        '営業所コード
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主コード(大)
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")

        '荷主コード(中)
        SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "CUST_CD_M = ", " '", custCdM, "' ")

        '商品KEY
        If String.IsNullOrEmpty(goodsKey) = False Then
            SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "GOODS_CD_NRS = ", " '", goodsKey, "' ")
        End If

        '商品コード
        If String.IsNullOrEmpty(goodsCd) = False Then
            SelectGoodsString = String.Concat(SelectGoodsString, " AND ", "GOODS_CD_CUST = ", " '", goodsCd, "' ")
        End If

        Return SelectGoodsString

    End Function

    '取込対応 20120305 Start
    ''' <summary>
    ''' セミEDI情報設定マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="WhCd">倉庫コード</param>
    ''' <param name="custCdL">荷主コードL</param>
    ''' <param name="custCdM">荷主コードM</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectSemiEDIListDatalow(ByVal brCd As String _
                                      , ByVal whCd As String _
                                      , ByVal custCdL As String _
                                      , ByVal custCdM As String _
                                      , ByVal inoutKb As String _
                                      ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.SEMIEDI_INFO_STATE).Select(Me.SelectSemiEDIString(brCd, whCd, custCdL, custCdM, inoutKb))

    End Function

    ''' <summary>
    ''' セミEDI情報設定マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="brCd">営業所コード</param>
    ''' <param name="WhCd">倉庫コード</param>
    ''' <param name="custCdL">荷主コードL</param>
    ''' <param name="custCdM">荷主コードM</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectSemiEDIString(ByVal brCd As String _
                                      , ByVal whCd As String _
                                      , ByVal custCdL As String _
                                      , ByVal custCdM As String _
                                      , ByVal inoutKb As String _
                                      ) As String

        SelectSemiEDIString = String.Empty

        '削除フラグ
        SelectSemiEDIString = String.Concat(SelectSemiEDIString, " SYS_DEL_FLG = '0' ")

        '倉庫コード
        SelectSemiEDIString = String.Concat(SelectSemiEDIString, " AND ", "WH_CD = ", " '", whCd, "' ")

        '営業所コード
        SelectSemiEDIString = String.Concat(SelectSemiEDIString, " AND ", "NRS_BR_CD = ", " '", brCd, "' ")

        '荷主コード(大)
        SelectSemiEDIString = String.Concat(SelectSemiEDIString, " AND ", "CUST_CD_L = ", " '", custCdL, "' ")

        '荷主コード(中)
        SelectSemiEDIString = String.Concat(SelectSemiEDIString, " AND ", "CUST_CD_M = ", " '", custCdM, "' ")

        '荷主コード(中)
        SelectSemiEDIString = String.Concat(SelectSemiEDIString, " AND ", "INOUT_KB = ", " '", inoutKb, "' ")

        Return SelectSemiEDIString

    End Function
    '取込対応 20120305 End

#End Region

#End Region

End Class
