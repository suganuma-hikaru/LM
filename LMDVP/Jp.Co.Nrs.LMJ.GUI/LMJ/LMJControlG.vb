' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMJ          : システム管理サブ
'  プログラムID     :  LMJControlG  : LMJ画面 共通処理
'  作  成  者       :  [ito]
' ==========================================================================
Imports GrapeCity.Win.Editors
Imports GrapeCity.Win.Editors.Fields
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.GUI.Win.InputMan

''' <summary>
''' LMJControl画面クラス
''' </summary>
''' <remarks></remarks>
''' <histry>
''' 2011/07/10 ito
''' </histry>
Public Class LMJControlG
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

        Dim cleraData As LMJControlC.CLERA_DATA = LMJControlC.CLERA_DATA.ISNULL
        Dim cmb As Win.InputMan.LMImCombo = Nothing
        Dim kbn As Win.InputMan.LMComboKubun = Nothing
        Dim nrs As Win.InputMan.LMComboNrsBr = Nothing
        Dim sok As Win.InputMan.LMComboSoko = Nothing
        Dim num As Win.InputMan.LMImNumber = Nothing
        Dim imd As Win.InputMan.LMImDate = Nothing

        If TypeOf ctl Is Win.InputMan.LMImCombo = True Then

            cmb = DirectCast(ctl, Win.InputMan.LMImCombo)
            cmb.ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMCOMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboKubun = True Then

            kbn = DirectCast(ctl, Win.InputMan.LMComboKubun)
            kbn.ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMKBN_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboNrsBr = True Then

            nrs = DirectCast(ctl, Win.InputMan.LMComboNrsBr)
            nrs.ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMNRS_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMComboSoko = True Then

            sok = DirectCast(ctl, Win.InputMan.LMComboSoko)
            sok.ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMSOK_COMB

        ElseIf TypeOf ctl Is Win.InputMan.LMImNumber = True Then

            num = DirectCast(ctl, Win.InputMan.LMImNumber)
            num.ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMNUMBER

        ElseIf TypeOf ctl Is Win.InputMan.LMImTextBox = True Then

            DirectCast(ctl, Win.InputMan.LMImTextBox).ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMTEXT

        ElseIf TypeOf ctl Is Win.InputMan.LMImDate = True Then

            imd = DirectCast(ctl, Win.InputMan.LMImDate)
            imd.ReadOnly = lock
            cleraData = LMJControlC.CLERA_DATA.IMDATE

        End If

        'ロックする場合は値をクリア
        If lock = True AndAlso clearFlg = True Then

            Select Case cleraData

                Case LMJControlC.CLERA_DATA.IMTEXT

                    ctl.TextValue = String.Empty

                Case LMJControlC.CLERA_DATA.IMCOMB

                    cmb.SelectedValue = Nothing

                Case LMJControlC.CLERA_DATA.IMKBN_COMB

                    kbn.SelectedValue = Nothing

                Case LMJControlC.CLERA_DATA.IMNRS_COMB

                    nrs.SelectedValue = Nothing

                Case LMJControlC.CLERA_DATA.IMSOK_COMB

                    sok.SelectedValue = Nothing

                Case LMJControlC.CLERA_DATA.IMDATE

                    imd.Value = Nothing

                Case LMJControlC.CLERA_DATA.IMNUMBER

                    num.Value = 0

            End Select

        End If

    End Sub

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

        ctl.Format = DateFieldsBuilder.BuildFields(LMJControlC.DATE_YYYYMMDD)
        ctl.DisplayFormat = DateDisplayFieldsBuilder.BuildFields(LMJControlC.DATE_SLASH_YYYYMMDD)
        If ctl.Holiday <> holidayFlg Then
            ctl.Holiday = holidayFlg
        End If

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
        SelectTcustString = String.Concat(SelectTcustString, " AND ", "DEFAULT_CUST_YN = ", " '", LMJControlC.FLG_ON, "' ")

        Return SelectTcustString

    End Function

#End Region

#End Region

End Class
