' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF020    : 運送編集
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMF020BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF020BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF020DAC = New LMF020DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' データセットテーブル名(INテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMF020IN"

    ''' <summary>
    ''' F_UNSO_Lテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "F_UNSO_L"

    ''' <summary>
    ''' F_UNSO_Mテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_M As String = "F_UNSO_M"

    ''' <summary>
    ''' F_UNCHINテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNCHIN As String = "F_UNCHIN_TRS"

    ''' <summary>
    ''' データセットテーブル名(G_HEDテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    ''' <summary>
    ''' UNCHIN_INFOテーブル
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_INFO As String = "UNCHIN_INFO"

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' データセットテーブル名(G_HED_CHKテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End

    ''' <summary>
    ''' 運送(大)のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_L As String = "SelectUnsoLInitData"

    ''' <summary>
    ''' 運送(中)のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_M As String = "SelectUnsoMData"

    ''' <summary>
    ''' 運賃のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_UNCHIN As String = "SelectUnchinData"

    ''' <summary>
    ''' (請求)料金情報のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_INFO As String = "SelectUnchinInfoData"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)料金情報のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_SHIHARAI As String = "SelectShiharaiInfoData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 運送(大)新規のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_NEW As String = "SelectNewUnsoLData"

    ''' <summary>
    ''' 排他チェック用のデータ抽出アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_HAITA As String = "SelectHaitaData"

    ''' <summary>
    ''' 運送(大)の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_L As String = "InsertUnsoLData"

    ''' <summary>
    ''' 運送(中)の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_M As String = "InsertUnsoMData"

    ''' <summary>
    ''' (請求)運賃の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_UNCHIN As String = "InsertUnchinData"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SHIHARAI As String = "InsertShiharaiData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 運送(大)の更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_L As String = "UpdateUnsoLData"

    ''' <summary>
    ''' (請求)運賃の更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_UNCHIN As String = "UpdateUnchinData"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_SHIHARAI As String = "UpdateShiharaiData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 支払運賃の更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDATE_PAY_UNCHIN As String = "UpdatePayUnchinData"

    '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
    ''' <summary>
    ''' 届先マスタ更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_DEST_M As String = "UpdateDestMasterData"
    '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start

    '2018/05/14 001545 荷卸電話番号を出荷L(届先電話番号)へ
    ''' <summary>
    ''' 出荷データL更新登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_UPDAT_OUTKA_L As String = "UpdateOutkaLData"

    ''' <summary>
    ''' 運送(大)の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_L As String = "DeleteUnsoLData"

    ''' <summary>
    ''' 運送(中)の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_M As String = "DeleteUnsoMData"

    ''' <summary>
    ''' (請求)運賃の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_UNCHIN As String = "DeleteUnchinData"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_SHIHARAI As String = "DeleteShiharaiData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 支払運賃の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_PAYUNCHIN As String = "DeletePayUnchinData"

    ''' <summary>
    ''' 前ゼロ
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MAEZERO As String = "000"

    ''' <summary>
    ''' 元データ区分(入荷)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const MOTO_DATA_NYUKA As String = "10"

    ''' <summary>
    ''' 運賃計算締め基準(出荷日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CALC_SHUKKA As String = "01"

    ''' <summary>
    ''' 運賃計算締め基準(納入日)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const CALC_NYUKA As String = "02"

    ''' <summary>
    ''' 経理取込
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TORIKOMI As String = "経理取込"

    '2016.01.06 UMANO 英語化対応START
    ''' <summary>
    ''' 編集
    ''' </summary>
    ''' <remarks></remarks>
    Private Const EDIT As String = "編集(Edit)"

    ''' <summary>
    ''' 保存
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SAVE As String = "保存(Save)"
    '2016.01.06 UMANO 英語化対応END

#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue

    ''' <summary>
    ''' 自動採番送り状区分(O010)
    ''' </summary>
    ''' <remarks></remarks>
    Class AUTO_DENP_KBN

        ''' <summary>
        ''' 名鉄運輸
        ''' </summary>
        ''' <remarks></remarks>
        Public Const MEITETSU_TRANSPORT As String = "01"

        ''' <summary>
        ''' 西濃運輸千葉(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_CHIBA As String = "02"

        '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add start
        ''' <summary>
        ''' トールエクスプレス(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_OSAKA As String = "03"

        ''' <summary>
        ''' トールエクスプレス(群馬)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_GUNMA As String = "04"
        '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

        ''' <summary>
        ''' 西濃運輸土気(標準)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_TOKE As String = "05"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](千葉)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_CHIBA As String = "06"

        ''' <summary>
        ''' JPロジスティクス[元トールエクスプレス](土気)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const TTOLL_EXPRESS_TOKE As String = "07"

        ''' <summary>
        ''' 西濃運輸(袖ヶ浦)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_SODEGAURA As String = "08"

        ''' <summary>
        ''' 西濃運輸(大阪)
        ''' </summary>
        ''' <remarks></remarks>
        Public Const SEINO_TRANSPORTATION_OSAKA As String = "09"

    End Class
#End If

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 初期検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectInitData(ByVal ds As DataSet) As DataSet

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_L)

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_M)

        '料金情報のデータ取得(請求)
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_INFO)

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '料金情報のデータ取得(支払)
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_SHIHARAI)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        Return ds

    End Function

    ''' <summary>
    ''' 新規検索の値取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectNewData(ByVal ds As DataSet) As DataSet

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_NEW)

        Return ds

    End Function

    ''' <summary>
    ''' 排他チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        '運送(大)のデータ取得
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_HAITA)

        '最終請求日チェック
        If MyBase.IsMessageExist() = False Then

            'チェック
            Call Me.ChkUnchinSeiqDate(ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As String

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Dim dt As DataTable = ds.Tables(LMF020BLC.TABLE_NM_G_HED)
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

#End Region

#Region "新規登録"

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

        '採番
        Dim unsoNoL As String = Me.GetUnsoNoL(ds)
        Dim value As String() = New String() {unsoNoL}
        Dim colNm As String() = New String() {"UNSO_NO_L"}

        '運送(大)に採番した値を設定
        ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_L, colNm, value)

        '運送(中)に採番した値を設定
        ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_M, colNm, value, "UNSO_NO_M")

        '要望番号:2408 2015.09.17 追加START
#If False Then  ' 西濃自動送り状番号対応 20160701 chnaged inoue

        Dim autoDenpNo As String = String.Empty
        Dim intMeitestuNo As Integer = 0
        If String.IsNullOrEmpty(ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0).Item("AUTO_DENP_KBN").ToString()) = False Then
            autoDenpNo = Me.GetMeiTetsuDenpNoL(ds)
            'チェックデジットの組込み
            intMeitestuNo = Convert.ToInt32(autoDenpNo) Mod 7
            autoDenpNo = String.Concat(autoDenpNo, Convert.ToString(intMeitestuNo))
            value = New String() {autoDenpNo}
            colNm = New String() {"AUTO_DENP_NO"}
            '運送(大)に自動送状番号を採番した値を設定
            ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_L, colNm, value)

        End If
#Else
        ' AUTO_DENP_KBNが変更された場合、呼び出し元でoutLDenpNoはクリアされる。
        ' AUTO_DENP_KBN変更時、変更前のoutLDenpNoが設定されていることは考慮しない。
        Dim autoDenpNo As String = String.Empty
        Dim autoDenpKbn As String = ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0).Item("AUTO_DENP_KBN").ToString()

        If String.IsNullOrEmpty(autoDenpKbn) = False Then
            '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd end
            autoDenpNo = Me.GetAutoDenpNo(autoDenpKbn, ds)
            'autoDenpNo = Me.GetAutoDenpNo(autoDenpKbn)
            '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen upd end
            value = New String() {autoDenpNo}
            colNm = New String() {"AUTO_DENP_NO"}

            '運送(大)に自動送状番号を採番した値を設定
            ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_L, colNm, value)
        End If

#End If
        '要望番号:2408 2015.09.17 追加END

        '新規登録
        Call Me.InsertData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertData(ByVal ds As DataSet) As Boolean

        '運送(大)の新規登録
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_INSERT_L)

        '運送(中)の更新処理
        rtnResult = rtnResult AndAlso Me.SetUnsoMData(ds)

        '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_INSERT_DEST_M)
        '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end

        Return rtnResult

    End Function

#End Region

#Region "更新登録"

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveAction(ByVal ds As DataSet) As DataSet

        '運送(中)番号の再設定
        ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_M, New String() {}, New String() {}, "UNSO_NO_M")

        '要望番号:2408 2015.09.17 追加START
        Dim value As String()
        Dim colNm As String()
        Dim autoDenpNo As String = String.Empty

#If False Then  ' 西濃自動送り状番号対応 20160701 chnaged inoue
        Dim intMeitestuNo As Integer = 0
        If String.IsNullOrEmpty(ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0).Item("AUTO_DENP_KBN").ToString()) = False AndAlso String.IsNullOrEmpty(ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0).Item("AUTO_DENP_NO").ToString()) = True Then
            autoDenpNo = Me.GetMeiTetsuDenpNoL(ds)
            'チェックデジットの組込み
            intMeitestuNo = Convert.ToInt32(autoDenpNo) Mod 7
            autoDenpNo = String.Concat(autoDenpNo, Convert.ToString(intMeitestuNo))
            value = New String() {autoDenpNo}
            colNm = New String() {"AUTO_DENP_NO"}
            '運送(大)に自動送状番号を採番した値を設定
            ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_L, colNm, value)

        End If
#Else
        Dim unsoLRow As DataRow = ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0)
        Dim autoDenpKbn As String = unsoLRow.Item("AUTO_DENP_KBN").ToString()

        ' AUTO_DENP_KBNが変更された場合、呼び出し元でoutLDenpNoはクリアされる。
        ' AUTO_DENP_KBN変更時、変更前のoutLDenpNoが設定されていることは考慮しない。
        If String.IsNullOrEmpty(autoDenpKbn) = False AndAlso _
           String.IsNullOrEmpty(unsoLRow.Item("AUTO_DENP_NO").ToString()) = True Then

            autoDenpNo = Me.GetAutoDenpNo(autoDenpKbn, ds)

            value = New String() {autoDenpNo}
            colNm = New String() {"AUTO_DENP_NO"}

            '運送(大)に自動送状番号を採番した値を設定
            ds = Me.SetValueData(ds, LMF020BLC.TABLE_NM_UNSO_L, colNm, value)
        End If
#End If

        '要望番号:2408 2015.09.17 追加END

        '更新処理
        Call Me.UpdateData(ds)

        '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add start
        Call Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_INSERT_DEST_M)
        '2018/04/19 001545 【LMS】運送情報入力画面に電話番号項目を追加(千葉BC物管２_石井) Annen add end

        '運送Lの更新をしない
        'ADD 2018/05/14 荷卸電話番号を出荷L(届先電話番号)へ
        'Call Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_UPDAT_OUTKA_L)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As Boolean

        '運送(大)の更新処理
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_UPDATE_L)

        '運送(中)の更新処理
        rtnResult = rtnResult AndAlso Me.SetUnsoMData(ds)

        'START UMANO 要望番号1369 支払運賃に伴う修正。
        If String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO_SYUKA").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO_TYUKEI").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO_HAIKA").ToString()) = True Then

            '支払運賃の更新処理
            rtnResult = rtnResult AndAlso Me.SetPayUnchinData(ds)
        End If
        'END UMANO 要望番号1369 支払運賃に伴う修正。

        Return rtnResult

    End Function

#End Region

#Region "削除登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeleteAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Call Me.DeleteData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DeleteData(ByVal ds As DataSet) As Boolean

        '締め日チェック
        Dim rtnResult As Boolean = Me.ChkUnchinSeiqDate(ds)

        '入荷(大)の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_L)

        '運送(中)の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_M)

        '(請求)運賃の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_UNCHIN)

        'START UMANO 要望番号1302 支払運賃に伴う修正。
        '(支払)運賃の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_SHIHARAI)
        'END UMANO 要望番号1302 支払運賃に伴う修正。

        '支払運賃の物理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_PAYUNCHIN)

        Return rtnResult

    End Function

#End Region

#Region "共通更新"

    ''' <summary>
    ''' 運送(中)の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnsoMData(ByVal ds As DataSet) As Boolean

        'Delete & Insert
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_M)

        '更新の場合
        Return rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_INSERT_M)

    End Function

    ''' <summary>
    ''' (請求)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetUnchinData(ByVal ds As DataSet) As DataSet

        '最終請求日チェック
        Dim rtnResult As Boolean = Me.ChkSeiqDate(ds, LMF020BLC.SAVE)

        'Delete
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_UNCHIN)

        'Insert
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_INSERT_UNCHIN)

        Return ds

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal ds As DataSet) As DataSet

        'START UMANO 要望番号1369 支払運賃に伴う修正。
        If String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO_SYUKA").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO_TYUKEI").ToString()) = True AndAlso _
           String.IsNullOrEmpty(ds.Tables(TABLE_NM_UNSO_L).Rows(0).Item("TRIP_NO_HAIKA").ToString()) = True Then

            'Delete
            Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_DELETE_SHIHARAI)

            'Insert
            rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_INSERT_SHIHARAI)

        End If

        Return ds

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    ''' <summary>
    ''' 支払運賃の更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetPayUnchinData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, LMF020BLC.ACTION_ID_UPDATE_PAY_UNCHIN)

    End Function

#End Region

#Region "チェック"

    ''' <summary>
    ''' 運賃情報の締め日チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkUnchinSeiqDate(ByVal ds As DataSet) As Boolean

        'INテーブルに値設定
        ds.Tables(LMF020BLC.TABLE_NM_IN).ImportRow(ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0))

        '運賃のデータ取得
        ds = Me.DacAccess(ds, LMF020BLC.ACTION_ID_SELECT_UNCHIN)

        'チェック
        Return Me.ChkSeiqDate(ds, LMF020BLC.EDIT)


    End Function

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet, ByVal msg As String) As Boolean

        Dim dr As DataRow = ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0)
        Dim inDate As String = dr.Item("ARR_PLAN_DATE").ToString()
        Dim outDate As String = dr.Item("OUTKA_PLAN_DATE").ToString()

        '納入日、出荷日の両方に値がない場合、スルー
        If String.IsNullOrEmpty(inDate) = True _
            AndAlso String.IsNullOrEmpty(outDate) Then
            Return True
        End If

        '元データ区分
        Dim moto As String = dr.Item("MOTO_DATA_KB").ToString()

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF020BLC.TABLE_NM_UNCHIN)

        Dim dt As DataTable = ds.Tables(LMF020BLC.TABLE_NM_UNCHIN)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            '請求日を取得用の情報を設定
            selectDt.Clear()
            selectDt.ImportRow(dt.Rows(i))

            '締め処理済の場合、スルー
            If Me.ChkSeiqDate(ds, inDate, outDate, dt.Rows(i).Item("UNTIN_CALCULATION_KB").ToString(), Me.SelectGheaderData(selectDs), moto, msg) = False Then
                Return False
            End If

        Next

        Return True

    End Function

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="inDate">納入日</param>
    ''' <param name="outDate">出荷日</param>
    ''' <param name="calcKbn">締め基準</param>
    ''' <param name="chkDate">請求日</param>
    ''' <param name="moto">元データ区分</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet _
                                     , ByVal inDate As String _
                                     , ByVal outDate As String _
                                     , ByVal calcKbn As String _
                                     , ByVal chkDate As String _
                                     , ByVal moto As String _
                                     , ByVal msg As String _
                                     ) As Boolean

        Select Case moto

            Case LMF020BLC.MOTO_DATA_NYUKA

                '元データ = 入荷は納入日とチェック
                Return Me.ChkDate(ds, inDate, chkDate, msg)

            Case Else

                Select Case calcKbn

                    Case LMF020BLC.CALC_SHUKKA

                        '運賃計算締め基準によるチェック(出荷日)
                        Return Me.ChkDate(ds, outDate, chkDate, msg)

                    Case LMF020BLC.CALC_NYUKA

                        '運賃計算締め基準によるチェック(納入日)
                        Return Me.ChkDate(ds, inDate, chkDate, msg)

                End Select

        End Select

        Return True

    End Function

    ''' <summary>
    ''' 日付チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="value1">比較対象日</param>
    ''' <param name="value2">最終締め日</param>
    ''' <param name="msg">置換文字</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal ds As DataSet, ByVal value1 As String, ByVal value2 As String, ByVal msg As String) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            '要望番号:1045 terakawa 2013.03.28 Start
            '新黒存在チェック用データセット作成
            Dim dr As DataRow = ds.Tables(LMF020BLC.TABLE_NM_G_HED_CHK).NewRow
            dr.Item("NRS_BR_CD") = ds.Tables(LMF020BLC.TABLE_NM_UNCHIN).Rows(0).Item("NRS_BR_CD")
            dr.Item("SEIQ_TARIFF_BUNRUI_KB") = ds.Tables(LMF020BLC.TABLE_NM_UNCHIN).Rows(0).Item("SEIQ_TARIFF_BUNRUI_KB")
            dr.Item("SEIQTO_CD") = ds.Tables(LMF020BLC.TABLE_NM_UNCHIN).Rows(0).Item("SEIQTO_CD")
            dr.Item("SKYU_DATE") = value1

            ds.Tables(LMF020BLC.TABLE_NM_G_HED_CHK).Rows.Add(dr)

            '新黒存在チェック
            ds = Me.DacAccess(ds, "NewKuroExistChk")
            If MyBase.GetResultCount() >= 1 Then

                '請求期間内チェック
                ds = Me.DacAccess(ds, "InSkyuDateChk")
                If MyBase.GetResultCount() >= 1 Then

                    Return True
                End If

            End If
            '要望番号:1045 terakawa 2013.03.28 End

            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessage("E232", New String() {LMF020BLC.TORIKOMI, msg})
            MyBase.SetMessage("E886", New String() {msg})
            '2016.01.06 UMANO 英語化対応END
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function

    ''' <summary>
    ''' UNSO_NO_Lを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>InkaNoL</returns>
    ''' <remarks></remarks>
    Private Function GetUnsoNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.UNSO_NO_L, Me, ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString())

    End Function

    '要望番号:2408 2015.09.17 追加START
    ''' <summary>
    ''' MEITETSU_DENP_NOを取得
    ''' </summary>
    ''' <returns>MEITETSU_DENP_NO</returns>
    ''' <remarks></remarks>
#If False Then ' 西濃自動送り状番号出力対応 20160701 chagned inoue
    Private Function GetMeiTetsuDenpNoL(ByVal ds As DataSet) As String

        Dim num As New NumberMasterUtility
        Return num.GetAutoCode(NumberMasterUtility.NumberKbn.MEITETSU_DENP_NO, Me, ds.Tables(LMF020BLC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD").ToString())
#Else
    Private Function GetMeiTetsuDenpNoL() As String
        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
            GetAutoCode(NumberMasterUtility.NumberKbn.MEITETSU_DENP_NO, Me)


        If (String.IsNullOrEmpty(newCode) = False) Then

            'チェックデジットの組込み
            Dim checkDigit As Integer = Convert.ToInt32(newCode) Mod 7
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo

#End If
    End Function
    '要望番号:2408 2015.09.17 追加END

#If True Then ' 西濃自動送り状番号出力対応 20160701 added inoue

    '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end
    ''' <summary>
    ''' TOLL_DENP_NOを取得(大阪トール)
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO, Me)

        Dim dt As DataTable = ds.Tables("LMF020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' TOLL_DENP_NOを取得(群馬トール)
    ''' </summary>
    ''' <returns>TOLL_DENP_NO</returns>
    ''' <remarks></remarks>
    Private Function GetGunmaTollDenpNoL(ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.TOLL_DENP_NO_CYD, Me)

        Dim dt As DataTable = ds.Tables("LMF020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function
    '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

    ''' <summary>
    ''' 送り状番号生成(千葉JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetChibaTollDenpNoL(ByVal ds As DataSet) As String

        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_CHI, Me)

        Dim dt As DataTable = ds.Tables("LMF020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 送り状番号生成(土気JPロジスティクス[元トールエクスプレス])
    ''' </summary>
    ''' <returns>送り状番号</returns>
    ''' <remarks></remarks>
    Private Function GetTokeTollDenpNoL(ByVal ds As DataSet) As String

        Dim autoDenpNo As String = String.Empty

        Dim newCode As String = New NumberMasterUtility().
        GetAutoCode(NumberMasterUtility.NumberKbn.JPLOGI_DENP_NO_TOK, Me)

        Dim dt As DataTable = ds.Tables("LMF020_OKURIJYO_WK")
        Dim okurijyoHed As String = String.Empty

        If dt.Rows.Count > 0 Then
            okurijyoHed = dt.Rows(0).Item("OKURIJYO_HEAD").ToString
        End If

        If (String.IsNullOrEmpty(newCode) = False) Then

            Dim checkDigit As Integer

            'チェックデジットの組込み

            If String.Empty.Equals(okurijyoHed) = False Then
                '区分マスタより取得できた時
                newCode = String.Concat(okurijyoHed, newCode)
            End If

            checkDigit = CInt(Convert.ToInt64(newCode) Mod 7)
            autoDenpNo = String.Concat(newCode, Convert.ToString(checkDigit))

        End If

        Return autoDenpNo


    End Function

    ''' <summary>
    ''' 自動採番送り状番号取得
    ''' </summary>
    ''' <param name="autoDenpKbn">自動送り状番号区分</param>
    ''' <param name="ds">自動送り状番号区分</param>
    ''' <returns>自動送り状番号</returns>
    ''' <remarks>
    ''' LMC010BLC,LMC020BLC,LMF020BLCに同メソッドあり
    ''' ToDo:共通ライブラリ化を検討
    ''' </remarks>
    Private Function GetAutoDenpNo(ByVal autoDenpKbn As String, ByVal ds As DataSet) As String


        Dim autoDenpNo As String = String.Empty

        If (String.IsNullOrEmpty(autoDenpKbn)) Then
            ' 自動送り状番号払い出し対象外
            Return autoDenpNo
        End If

        Dim nubmerKbn As NumberMasterUtility.NumberKbn = Nothing

        Select Case autoDenpKbn
            Case AUTO_DENP_KBN.MEITETSU_TRANSPORT
                ' 名鉄運輸送り状番号生成
                autoDenpNo = Me.GetMeiTetsuDenpNoL()

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_CHIBA
                ' 西濃運輸(千葉)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_CHIBA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

                '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end
            Case AUTO_DENP_KBN.TTOLL_EXPRESS_OSAKA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_GUNMA
                ' トールエクスプレス送り状番号生成                
                autoDenpNo = Me.GetGunmaTollDenpNoL(ds)
                '2017/12/14 【EDI】送状･荷札_大阪群馬トール_EDI化対応 Annen add end

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_TOKE
                ' 西濃運輸(千葉)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_TOKE
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_CHIBA
                'JPロジスティクス[元トールエクスプレス](千葉) 送り状番号生成
                autoDenpNo = Me.GetChibaTollDenpNoL(ds)

            Case AUTO_DENP_KBN.TTOLL_EXPRESS_TOKE
                'JPロジスティクス[元トールエクスプレス](土気) 送り状番号生成
                autoDenpNo = Me.GetTokeTollDenpNoL(ds)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_SODEGAURA
                ' 西濃運輸(袖ヶ浦)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_SODEGAURA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case AUTO_DENP_KBN.SEINO_TRANSPORTATION_OSAKA
                ' 西濃運輸(大阪)送り状番号生成
                nubmerKbn = NumberMasterUtility.NumberKbn.AUTO_DENP_NO_SEINO_OSAKA
                autoDenpNo = New NumberMasterUtility().GetAutoCode(nubmerKbn, Me)

            Case Else
                Throw New InvalidOperationException(String.Format("定義されていないAUTO_DENP_KBN=[{0}]が指定されました。", autoDenpKbn))

        End Select


        Return autoDenpNo


    End Function
#End If

    ''' <summary>
    ''' 全ての行にValueの値を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="tblNm">テーブル名</param>
    ''' <param name="colNm">列名</param>
    ''' <param name="value">設定したい値</param>
    ''' <param name="eda">枝番の列名</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetValueData(ByVal ds As DataSet _
                                  , ByVal tblNm As String _
                                  , ByVal colNm As String() _
                                  , ByVal value As String() _
                                  , Optional ByVal eda As String = "" _
                                  ) As DataSet

        Dim dt As DataTable = ds.Tables(tblNm)
        Dim max As Integer = dt.Rows.Count - 1
        Dim count As Integer = value.Length - 1
        Dim cnt As Integer = 0
        Dim setEda As String = String.Empty

        For i As Integer = 0 To max

            For j As Integer = 0 To count

                dt.Rows(i).Item(colNm(j)) = value(j)

            Next

            '枝番の設定
            If String.IsNullOrEmpty(eda) = False Then

                cnt += 1
                setEda = String.Concat(LMF020BLC.MAEZERO, cnt.ToString())
                dt.Rows(i).Item(eda) = setEda.Substring(setEda.Length - 3, 3)

            End If

        Next

        Return ds

    End Function

#End Region

End Class
