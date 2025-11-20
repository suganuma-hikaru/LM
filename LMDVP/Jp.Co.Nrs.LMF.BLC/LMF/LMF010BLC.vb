' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送サブ
'  プログラムID     :  LMF010    : 運送検索
'  作  成  者       :  [ito]
' ==========================================================================
Imports Jp.Co.Nrs.LM.Const
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMF010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF010DAC = New LMF010DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 運送(大)排他チェックアクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_HAITA As String = "SelectHaitaData"

    ''' <summary>
    ''' データセットテーブル名(ITEMテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ITEM As String = "ITEM"

    ''' <summary>
    ''' データセットテーブル名(ERRテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_ERR As String = "ERR"

    ''' <summary>
    ''' データセットテーブル名(UNSO_Lテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNSO_L As String = "UNSO_L"

    ''' <summary>
    ''' データセットテーブル名(UNCHINテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_UNCHIN As String = "UNCHIN"

    ''' <summary>
    ''' データセットテーブル名(G_HEDテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED As String = "G_HED"

    '要望番号:1045 terakawa 2013.03.28 Start
    ''' <summary>
    ''' データセットテーブル名(G_HED_CHKテーブル)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_G_HED_CHK As String = "G_HED_CHK"
    '要望番号:1045 terakawa 2013.03.28 End

    ''' <summary>
    ''' 修正項目(運行番号)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const SHUSEI_TRIP As String = "01"

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

    ''' <summary>
    ''' 一括変更
    ''' </summary>
    ''' <remarks></remarks>
    Private Const IKKATU As String = "一括変更"

    ''' <summary>
    ''' ガイダンス区分(00)
    ''' </summary>
    ''' <remarks></remarks>
    Private Const GUIDANCE_KBN As String = "00"

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の新規登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_INSERT_SHIHARAI As String = "InsertShiharaiData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の削除登録アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_DELETE_SHIHARAI As String = "DeleteShiharaiData"
    'END UMANO 要望番号1302 支払運賃に伴う修正。

#End Region

#Region "検索処理"

    ''' <summary>
    ''' 運送(大)検索対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf count > MyBase.GetLimitCount() Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 運送(大)検索対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運賃レコードを取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUnchinData(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    'START UMANO 要望番号1369 運行紐付け対応
    ''' <summary>
    ''' 運送会社修正に伴う支払運賃タリフ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UnsoLShiharaiEdit(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    'END UMANO 要望番号1369 運行紐付け対応

    'START UMANO 要望番号1369 運行紐付け対応
    ''' <summary>
    ''' 運行レコードの支払運賃タリフ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectUncodataTariff(ByVal ds As DataSet) As DataSet

        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    'END UMANO 要望番号1369 運行紐付け対応

    ''' <summary>
    ''' 最終請求日を取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>請求日</returns>
    ''' <remarks>取得できない場合は"00000000"を返却</remarks>
    Private Function SelectGheaderData(ByVal ds As DataSet) As String

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

        Dim dt As DataTable = ds.Tables(LMF010BLC.TABLE_NM_G_HED)
        If dt.Rows.Count < 1 Then

            Return "00000000"

        End If

        Return dt.Rows(0).Item("SKYU_DATE").ToString()

    End Function

    ''' <summary>
    ''' 排他チェック検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkHaitaData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF010BLC.TABLE_NM_UNSO_L)
        Dim max As Integer = dt.Rows.Count - 1

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF010BLC.TABLE_NM_UNSO_L)

        For i As Integer = 0 To max

            selectDt.Clear()
            selectDt.ImportRow(dt.Rows(i))

            '排他チェック
            If Me.SelectHaitaData(selectDs) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' コンボ用のデータ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SelectCombData(ByVal ds As DataSet) As DataSet
        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
    End Function

    ''' <summary>
    ''' 対象運行データキャンセルチェックアクション
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function ChkCancelData(ByVal ds As DataSet) As DataSet

        Dim dt As DataTable = ds.Tables(LMF010BLC.TABLE_NM_ITEM)
        Dim max As Integer = dt.Rows.Count - 1

        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF010BLC.TABLE_NM_ITEM)

        For i As Integer = 0 To max

            selectDt.Clear()
            selectDt.ImportRow(dt.Rows(i))

            'キャンセルチェック
            If Me.SelectCancelData(selectDs) = False Then
                Return ds
            End If

        Next

        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SaveAction(ByVal ds As DataSet) As DataSet

        Return Me.SetErrData(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 解除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function RemovedAction(ByVal ds As DataSet) As DataSet

        Return Me.SetErrData(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 運行レコード更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function UpdateUnsoLLData(ByVal ds As DataSet) As DataSet

        '運行レコードのシステム項目を更新
        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    'START UMANO 要望番号1302 支払運賃に伴う修正。
    ''' <summary>
    ''' (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SetShiharaiData(ByVal ds As DataSet) As DataSet

        'Delete
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, LMF010BLC.ACTION_ID_DELETE_SHIHARAI)

        'Insert
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, LMF010BLC.ACTION_ID_INSERT_SHIHARAI)

        Return ds

    End Function
    'END UMANO 要望番号1302 支払運賃に伴う修正。

    'START KIM 要望番号1485 支払い関連修正。
    ''' <summary>
    ''' (支払)運賃の更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateShiharaiData(ByVal ds As DataSet) As DataSet

        '(支払)運賃の更新処理
        Return Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function
    'END KIM 要望番号1485 支払い関連修正。

#End Region

#Region "チェック"

    ''' <summary>
    ''' 最終請求日のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>最終請求日</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = ds.Tables(LMF010BLC.TABLE_NM_UNSO_L).Rows(0)
        Dim inDate As String = dr.Item("ARR_PLAN_DATE").ToString()
        Dim outDate As String = dr.Item("OUTKA_PLAN_DATE").ToString()

        '納入日、出荷日の両方に値がない場合、スルー
        If String.IsNullOrEmpty(inDate) = True _
            AndAlso String.IsNullOrEmpty(outDate) Then
            Return ds
        End If

        '運賃情報取得
        ds = Me.SelectUnchinData(ds)

        '元データ区分
        Dim moto As String = dr.Item("MOTO_DATA_KB").ToString()

        Dim rowNo As String = dr.Item("ROW_NO").ToString()
        Dim selectDs As DataSet = ds.Copy
        Dim selectDt As DataTable = selectDs.Tables(LMF010BLC.TABLE_NM_UNCHIN)

        Dim dt As DataTable = ds.Tables(LMF010BLC.TABLE_NM_UNCHIN)
        Dim max As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To max

            '請求日を取得用の情報を設定
            selectDt.Clear()
            selectDt.ImportRow(dt.Rows(i))

            '締め処理済の場合、スルー
            If Me.ChkSeiqDate(ds, inDate, outDate, dt.Rows(i).Item("UNTIN_CALCULATION_KB").ToString(), Me.SelectGheaderData(selectDs), moto, rowNo) = False Then
                Return ds
            End If

        Next

        Return ds

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
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkSeiqDate(ByVal ds As DataSet _
                                     , ByVal inDate As String _
                                     , ByVal outDate As String _
                                     , ByVal calcKbn As String _
                                     , ByVal chkDate As String _
                                     , ByVal moto As String _
                                     , ByVal rowNo As String _
                                     ) As Boolean

        Select Case moto

            Case LMF010BLC.MOTO_DATA_NYUKA

                '元データ = 入荷は納入日とチェック
                Return Me.ChkDate(ds, inDate, chkDate, rowNo)

            Case Else

                Select Case calcKbn

                    Case LMF010BLC.CALC_SHUKKA

                        '運賃計算締め基準によるチェック(出荷日)
                        Return Me.ChkDate(ds, outDate, chkDate, rowNo)

                    Case LMF010BLC.CALC_NYUKA

                        '運賃計算締め基準によるチェック(納入日)
                        Return Me.ChkDate(ds, inDate, chkDate, rowNo)

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
    ''' <param name="rowNo">スプレッドの行番号</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ChkDate(ByVal ds As DataSet, ByVal value1 As String, ByVal value2 As String, ByVal rowNo As String) As Boolean

        '比較対象1に値がない場合、スルー
        If String.IsNullOrEmpty(value1) = True Then
            Return True
        End If

        'すでに締め処理が締め処理が終了している場合、エラー
        If value1 <= value2 Then

            '要望番号:1045 terakawa 2013.03.28 Start
            '新黒存在チェック用データセット作成
            Dim dr As DataRow = ds.Tables(LMF010BLC.TABLE_NM_G_HED_CHK).NewRow
            dr.Item("NRS_BR_CD") = ds.Tables(LMF010BLC.TABLE_NM_UNSO_L).Rows(0).Item("NRS_BR_CD")
            dr.Item("SEIQ_TARIFF_BUNRUI_KB") = ds.Tables(LMF010BLC.TABLE_NM_UNCHIN).Rows(0).Item("SEIQ_TARIFF_BUNRUI_KB")
            dr.Item("SEIQTO_CD") = ds.Tables(LMF010BLC.TABLE_NM_UNCHIN).Rows(0).Item("SEIQTO_CD")
            dr.Item("SKYU_DATE") = value1

            ds.Tables(LMF010BLC.TABLE_NM_G_HED_CHK).Rows.Add(dr)

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
            'MyBase.SetMessageStore(LMF010BLC.GUIDANCE_KBN, "E232", New String() {LMF010BLC.TORIKOMI, LMF010BLC.IKKATU}, rowNo)
            MyBase.SetMessageStore(LMF010BLC.GUIDANCE_KBN, "E886", , rowNo)
            '2016.01.06 UMANO 英語化対応END
            Call Me.SetErrData(ds)
            Return False

        End If

        Return True

    End Function

    ''' <summary>
    ''' 運送(特大)テーブル存在チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectLLCountData(ByVal ds As DataSet) As DataSet

        '修正項目が運行番号でない場合、スルー
        Dim dr As DataRow = ds.Tables(LMF010BLC.TABLE_NM_ITEM).Rows(0)
        If LMF010BLC.SHUSEI_TRIP.Equals(dr.Item("ITEM_DATA").ToString()) = False Then
            Return ds
        End If

        Dim tripNo As String = dr.Item("TRIP_NO").ToString()

        ds = Me.DacAccess(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)
        If MyBase.GetResultCount() < 1 Then
            'START UMANO 要望番号1369 一括変更時運行が紐づいている場合はエラー
            '2016.01.06 UMANO 英語化対応START
            'MyBase.SetMessageStore(LMF010BLC.GUIDANCE_KBN, "E079", New String() {"運送（特大）テーブル", tripNo})
            MyBase.SetMessageStore(LMF010BLC.GUIDANCE_KBN, "E887", New String() {tripNo})
            'MyBase.SetMessage("E079", New String() {"運送（特大）テーブル", tripNo})
            '2016.01.06 UMANO 英語化対応START
            'END UMANO 要望番号1369 一括変更時運行が紐づいている場合はエラー
        End If

        Return ds

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudgeStore(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageStoreExist(Convert.ToInt32(ds.Tables(LMF010BLC.TABLE_NM_UNSO_L).Rows(0).Item("ROW_NO").ToString()))

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

    ''' <summary>
    ''' 排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectHaitaData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' キャンセルチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function SelectCancelData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, System.Reflection.MethodBase.GetCurrentMethod.Name)

    End Function

    ''' <summary>
    ''' 更新処理をして正しく更新されたかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetErrData(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        '更新処理
        If Me.ServerChkJudgeStore(ds, actionId) = False Then

            'エラー行の設定
            ds = Me.SetErrData(ds)

        End If

        Return ds

    End Function

    ''' <summary>
    ''' エラー行番号を設定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function SetErrData(ByVal ds As DataSet) As DataSet

        'エラー行の設定
        Dim dt As DataTable = ds.Tables(LMF010BLC.TABLE_NM_ERR)
        Dim dr As DataRow = dt.NewRow()
        dr.Item("ROW_NO") = ds.Tables(LMF010BLC.TABLE_NM_UNSO_L).Rows(0).Item("ROW_NO").ToString()
        dt.Rows.Add(dr)
        Return ds

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

#End Region

End Class
