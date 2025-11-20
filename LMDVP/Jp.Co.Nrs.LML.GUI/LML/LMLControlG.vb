' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LML          : データ管理サブ
'  プログラムID     :  LMLControlG  : LML画面 共通処理
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan
Imports FarPoint.Win.Spread
Imports GrapeCity.Win.Editors

''' <summary>
''' LMLControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' </histry>
Public Class LMLControlG
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

        Dim cleraData As LMLControlC.CLERA_DATA = LMLControlC.CLERA_DATA.ISNULL
        Dim cmb As Win.InputMan.LMImCombo = Nothing
        Dim kbn As Win.InputMan.LMComboKubun = Nothing
        Dim nrs As Win.InputMan.LMComboNrsBr = Nothing
        Dim sok As Win.InputMan.LMComboSoko = Nothing
        Dim num As Win.InputMan.LMImNumber = Nothing
        Dim imd As Win.InputMan.LMImDate = Nothing

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            cmb = DirectCast(ctl, Win.InputMan.LMImCombo)
            cmb.ReadOnly = lock
            cleraData = LMLControlC.CLERA_DATA.IMCOMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            kbn = DirectCast(ctl, Win.InputMan.LMComboKubun)
            kbn.ReadOnly = lock
            cleraData = LMLControlC.CLERA_DATA.IMKBN_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            nrs = DirectCast(ctl, Win.InputMan.LMComboNrsBr)
            nrs.ReadOnly = lock
            cleraData = LMLControlC.CLERA_DATA.IMNRS_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            sok = DirectCast(ctl, Win.InputMan.LMComboSoko)

            num = DirectCast(ctl, Win.InputMan.LMImNumber)
            num.ReadOnly = lock
            cleraData = LMLControlC.CLERA_DATA.IMNUMBER

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = lock
            cleraData = LMLControlC.CLERA_DATA.IMTEXT

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            imd = DirectCast(ctl, Win.InputMan.LMImDate)
            imd.ReadOnly = lock
            cleraData = LMLControlC.CLERA_DATA.IMDATE

        End If

        'ロックする場合は値をクリア
        If lock = True AndAlso clearFlg = True Then

            Select Case cleraData

                Case LMLControlC.CLERA_DATA.IMTEXT

                    ctl.TextValue = String.Empty

                Case LMLControlC.CLERA_DATA.IMCOMB

                    cmb.SelectedValue = Nothing

                Case LMLControlC.CLERA_DATA.IMKBN_COMB

                    kbn.SelectedValue = Nothing

                Case LMLControlC.CLERA_DATA.IMNRS_COMB

                    nrs.SelectedValue = Nothing

                Case LMLControlC.CLERA_DATA.IMSOK_COMB

                    sok.SelectedValue = Nothing

                Case LMLControlC.CLERA_DATA.IMDATE

                    imd.Value = Nothing

                Case LMLControlC.CLERA_DATA.IMNUMBER

                    num.Value = 0

            End Select

        End If

    End Sub

    ''' <summary>
    ''' マスタコンボボックス作成
    ''' </summary>I
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

    ''' <summary>
    ''' 日付コントロールの書式設定
    ''' </summary>
    ''' <param name="ctl">コントロール</param>
    ''' <param name="holidayFlg">休日マスタ反映フラグ 初期値 = True</param>
    ''' <remarks></remarks>
    Friend Sub SetDateFormat(ByVal ctl As LMImDate, Optional ByVal holidayFlg As Boolean = True)

        ctl.Format = DateFieldsBuilder.BuildFields(LMLControlC.DATE_YYYYMMDD)
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields(LMLControlC.DATE_SLASH_YYYYMMDD)
        ctl.Holiday = holidayFlg

    End Sub

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

#End Region

#Region "ユーティリティ"

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
#End Region

#End Region

#Region "キャッシュから値取得"

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <param name="kbnNm1">区分名1</param>
    ''' <param name="kbnNm2">区分名2</param>
    ''' <param name="kbnNm3">区分名3</param>
    ''' <param name="kbnNm4">区分名4</param>
    ''' <param name="kbnNm5">区分名5</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectKbnListDataRow(Optional ByVal kbnCd As String = "" _
                                         , Optional ByVal groupCd As String = "" _
                                         , Optional ByVal kbnNm1 As String = "" _
                                         , Optional ByVal kbnNm2 As String = "" _
                                         , Optional ByVal kbnNm3 As String = "" _
                                         , Optional ByVal kbnNm4 As String = "" _
                                         , Optional ByVal kbnNm5 As String = "" _
                                         ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.KBN).Select(Me.SelectKbnString(kbnCd, groupCd, kbnNm1, kbnNm2, kbnNm3, kbnNm4, kbnNm5), "KBN_CD")

    End Function

    ''' <summary>
    ''' 区分マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="kbnCd">区分コード</param>
    ''' <param name="groupCd">区分グループコード</param>
    ''' <param name="kbnNm1">区分名1</param>
    ''' <param name="kbnNm2">区分名2</param>
    ''' <param name="kbnNm3">区分名3</param>
    ''' <param name="kbnNm4">区分名4</param>
    ''' <param name="kbnNm5">区分名5</param>
    ''' <returns>検索条件</returns>
    ''' <remarks></remarks>
    Private Function SelectKbnString(ByVal kbnCd As String _
                                     , ByVal groupCd As String _
                                     , ByVal kbnNm1 As String _
                                     , ByVal kbnNm2 As String _
                                     , ByVal kbnNm3 As String _
                                     , ByVal kbnNm4 As String _
                                     , ByVal kbnNm5 As String _
                                     ) As String

        SelectKbnString = String.Empty

        '削除フラグ
        SelectKbnString = String.Concat(SelectKbnString, " SYS_DEL_FLG = '0' ")

        '区分コード
        If String.IsNullOrEmpty(kbnCd) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_CD = ", " '", kbnCd, "' ")

        End If

        '区分グループコード
        If String.IsNullOrEmpty(groupCd) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_GROUP_CD = ", " '", groupCd, "' ")

        End If

        '区分名1
        If String.IsNullOrEmpty(kbnNm1) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_NM1 = ", " '", kbnNm1, "' ")

        End If

        '区分名2
        If String.IsNullOrEmpty(kbnNm2) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_NM2 = ", " '", kbnNm2, "' ")

        End If

        '区分名3
        If String.IsNullOrEmpty(kbnNm3) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_NM3 = ", " '", kbnNm3, "' ")

        End If

        '区分名4
        If String.IsNullOrEmpty(kbnNm4) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_NM4 = ", " '", kbnNm4, "' ")

        End If

        '区分名5
        If String.IsNullOrEmpty(kbnNm5) = False Then

            SelectKbnString = String.Concat(SelectKbnString, " AND ", "KBN_NM5 = ", " '", kbnNm5, "' ")

        End If

        Return SelectKbnString

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRow(ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(custLCd, custMCd, custSCd, custSSCd), "CUST_CD_L,CUST_CD_M,CUST_CD_S,CUST_CD_SS")

    End Function

    ''' <summary>
    ''' 荷主マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="custMCd">荷主(中)コード</param>
    ''' <param name="custSCd">荷主(小)コード</param>
    ''' <param name="custSSCd">荷主(極小)コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectCustListDataRowByNrsBrCd(ByVal nrsBrCd As String _
                                          , ByVal custLCd As String _
                                          , Optional ByVal custMCd As String = "" _
                                          , Optional ByVal custSCd As String = "" _
                                          , Optional ByVal custSSCd As String = "" _
                                          ) As DataRow()

        'キャッシュテーブルからデータ抽出
        Return MyBase.GetLMCachedDataTable(LMConst.CacheTBL.CUST).Select(Me.SelectCustString(nrsBrCd, custLCd, custMCd, custSCd, custSSCd))

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

    'START YANAI 20120120 請求データ作成対応
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
    'END YANAI 20120120 請求データ作成対応

    'START KIM 20121016 特定荷主対応（ハネウェル）
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
    'END KIM 20121016 特定荷主対応（ハネウェル）

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
    ''' 届先マスタ(キャッシュテーブル)からデータを抽出する条件を取得
    ''' </summary>
    ''' <param name="custLCd">荷主(大)コード</param>
    ''' <param name="destCd">届先コード</param>
    ''' <returns>データロウ配列</returns>
    ''' <remarks></remarks>
    Friend Function SelectDestListDataRow(ByVal custLCd As String, ByVal destCd As String) As DataRow()

        'キャッシュテーブルからデータ抽出
        Dim destMstDs As MDestDS = New MDestDS
        Dim destMstDr As DataRow = destMstDs.Tables(LMConst.CacheTBL.DEST).NewRow()
        destMstDr.Item("CUST_CD_L") = custLCd
        destMstDr.Item("DEST_CD") = destCd
        destMstDr.Item("SYS_DEL_FLG") = "0"
        destMstDs.Tables(LMConst.CacheTBL.DEST).Rows.Add(destMstDr)
        Dim rtnDs As DataSet = MyBase.GetDestMasterData(destMstDs)
        Return rtnDs.Tables(LMConst.CacheTBL.DEST).Select

    End Function

#End Region

End Class
