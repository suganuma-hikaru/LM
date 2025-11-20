' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMS     : マスタ
'  プログラムID     :  LMM540H : 棟マスタメンテナンス
'  作  成  者       :  [narita]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC

''' <summary>
''' LMM540BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMM540BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMM540DAC = New LMM540DAC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 棟マスタ更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectData", ds)

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
    ''' 棟マスタ更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '棟マスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData")

        '棟マスタ消防データ取得
        ds = Me.DacAccess(ds, "SelectListData2")

        '棟チェックマスタデータ取得
        ds = Me.DacAccess(ds, "SelectListData4")

        Return ds

    End Function


#End Region

#Region "設定処理"

    ''' <summary>
    ''' 棟マスタ存在チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckExistTouM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "CheckExistTouM", ds)

        '処理件数による判定
        If MyBase.GetResultCount() > 0 Then
            '1件以上の場合、マスタ存在メッセージを設定する
            MyBase.SetMessage("E010")
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 棟マスタ排他チェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function CheckHaitaTouM(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "SelectTouM", ds)

        Return ds

    End Function
#End Region

#Region "新規登録"

    ''' <summary>
    ''' 棟マスタ新規登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertSaveAction(ByVal ds As DataSet) As DataSet

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

        '棟情報の新規登録
        Dim rtnResult As Boolean = Me.InsertTouData(ds)

        '棟マスタ消防情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouShoboData(ds)

        '棟マスタ消防情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouShoboData(ds)


        '棟チェックマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouChkData(ds)

        '棟チェックマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouChkData(ds)

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

        '更新処理
        Call Me.UpdateData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 更新登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateData(ByVal ds As DataSet) As Boolean

        '棟情報の更新登録
        Dim rtnResult As Boolean = Me.UpdateTouData(ds)

        '棟マスタ消防情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouShoboData(ds)

        '棟マスタ消防情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouShoboData(ds)

        '棟チェックマスタ情報の物理削除
        rtnResult = rtnResult AndAlso Me.DelTouChkData(ds)

        '棟チェックマスタ情報の新規登録
        rtnResult = rtnResult AndAlso Me.InsertTouChkData(ds)

        Return rtnResult

    End Function

#End Region

#Region "更新時保管可能データ取得"

    ''' <summary>
    ''' 保管可能データ取得
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListDataSum(ByVal ds As DataSet) As DataSet

        '保管可能データ取得
        ds = Me.DacAccess(ds, "SelectListDataSum")

        Return ds

    End Function

#End Region

#Region "配下に反映処理"

    ''' <summary>
    ''' 配下に反映処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function HaikaSaveAction(ByVal ds As DataSet) As DataSet

        '更新処理
        Call Me.HaikaData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 配下に反映処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function HaikaData(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = True
        Dim flgUpdTouSitu As Boolean = False
        Dim flgUpdZone As Boolean = False

        '消防
        If ds.Tables("LMM540IN_HAIKA_CHECK").Rows(0).Item("SHOBO_CHK").Equals("1") = True Then
            '棟室マスタ消防情報の物理削除
            rtnResult = rtnResult AndAlso Me.DelTouSituShoboData(ds)

            ds.Tables("LMM540_TOU_SITU_SHOBO").Rows.Clear()
            ds.Tables("LMM540_ZONE_SHOBO").Rows.Clear()

            If rtnResult Then
                '棟室マスタを選択
                ds = Me.DacAccess(ds, "SelectListData5")

                '棟室をキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_TOU_SITU").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_SHOBO").Rows

                        Dim dr3 As DataRow = ds.Tables("LMM540_TOU_SITU_SHOBO").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("SHOBO_CD") = dr2.Item("SHOBO_CD")
                        dr3.Item("WH_KYOKA_DATE") = dr2.Item("WH_KYOKA_DATE")
                        dr3.Item("BAISU") = dr2.Item("BAISU")

                        ds.Tables("LMM540_TOU_SITU_SHOBO").Rows.Add(dr3)
                        flgUpdTouSitu = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertTouSituShoboData(ds)
            End If

            'ゾーンマスタ消防情報の物理削除
            rtnResult = rtnResult AndAlso Me.DelZoneShoboData(ds)

            If rtnResult Then
                'ゾーンマスタを選択
                ds = Me.DacAccess(ds, "SelectListData6")

                'ゾーンをキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_ZONE").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_SHOBO").Rows

                        Dim dr3 As DataRow = ds.Tables("LMM540_ZONE_SHOBO").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("ZONE_CD") = dr1.Item("ZONE_CD")
                        dr3.Item("SHOBO_CD") = dr2.Item("SHOBO_CD")
                        dr3.Item("WH_KYOKA_DATE") = dr2.Item("WH_KYOKA_DATE")
                        dr3.Item("BAISU") = dr2.Item("BAISU")

                        ds.Tables("LMM540_ZONE_SHOBO").Rows.Add(dr3)
                        flgUpdZone = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertZoneShoboData(ds)
            End If
        End If

        '毒劇
        If ds.Tables("LMM540IN_HAIKA_CHECK").Rows(0).Item("DOKU_CHK").Equals("1") = True Then

            '棟室ゾーンチェックマスタ(毒劇)の物理削除
            ds.Tables("LMM540_KBN_CD").Rows.Clear()
            Dim dr0 As DataRow = ds.Tables("LMM540_KBN_CD").NewRow
            dr0.Item("KBN_GROUP_CD") = "G001"
            ds.Tables("LMM540_KBN_CD").Rows.Add(dr0)

            rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

            ds.Tables("LMM540_TOU_SITU_CHK").Rows.Clear()
            ds.Tables("LMM540_ZONE_CHK").Rows.Clear()

            If rtnResult Then
                '棟室マスタを選択
                ds = Me.DacAccess(ds, "SelectListData5")

                '棟室をキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_TOU_SITU").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_CHK").Select("KBN_GROUP_CD = 'G001'")

                        Dim dr3 As DataRow = ds.Tables("LMM540_TOU_SITU_CHK").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("KBN_GROUP_CD") = dr2.Item("KBN_GROUP_CD")
                        dr3.Item("KBN_CD") = dr2.Item("KBN_CD")
                        dr3.Item("KBN_NM1") = dr2.Item("KBN_NM1")

                        ds.Tables("LMM540_TOU_SITU_CHK").Rows.Add(dr3)
                        flgUpdTouSitu = True
                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertTouSituChkData(ds)
            End If

            If rtnResult Then
                'ゾーンマスタを選択
                ds = Me.DacAccess(ds, "SelectListData6")

                'ゾーンをキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_ZONE").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_CHK").Select("KBN_GROUP_CD = 'G001'")

                        Dim dr3 As DataRow = ds.Tables("LMM540_ZONE_CHK").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("ZONE_CD") = dr1.Item("ZONE_CD")
                        dr3.Item("KBN_GROUP_CD") = dr2.Item("KBN_GROUP_CD")
                        dr3.Item("KBN_CD") = dr2.Item("KBN_CD")
                        dr3.Item("KBN_NM1") = dr2.Item("KBN_NM1")

                        ds.Tables("LMM540_ZONE_CHK").Rows.Add(dr3)
                        flgUpdZone = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertZoneChkData(ds)
            End If
        End If

        '高圧ガス
        If ds.Tables("LMM540IN_HAIKA_CHECK").Rows(0).Item("KOUATHUGAS_CHK").Equals("1") = True Then

            '棟室ゾーンチェックマスタ(高圧ガス)の物理削除
            ds.Tables("LMM540_KBN_CD").Rows.Clear()
            Dim dr0 As DataRow = ds.Tables("LMM540_KBN_CD").NewRow
            dr0.Item("KBN_GROUP_CD") = "G012"
            ds.Tables("LMM540_KBN_CD").Rows.Add(dr0)

            rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

            ds.Tables("LMM540_TOU_SITU_CHK").Rows.Clear()
            ds.Tables("LMM540_ZONE_CHK").Rows.Clear()

            If rtnResult Then
                '棟室マスタを選択
                ds = Me.DacAccess(ds, "SelectListData5")

                '棟室をキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_TOU_SITU").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_CHK").Select("KBN_GROUP_CD = 'G012'")

                        Dim dr3 As DataRow = ds.Tables("LMM540_TOU_SITU_CHK").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("KBN_GROUP_CD") = dr2.Item("KBN_GROUP_CD")
                        dr3.Item("KBN_CD") = dr2.Item("KBN_CD")
                        dr3.Item("KBN_NM1") = dr2.Item("KBN_NM1")

                        ds.Tables("LMM540_TOU_SITU_CHK").Rows.Add(dr3)
                        flgUpdTouSitu = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertTouSituChkData(ds)
            End If

            If rtnResult Then
                'ゾーンマスタを選択
                ds = Me.DacAccess(ds, "SelectListData6")

                'ゾーンをキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_ZONE").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_CHK").Select("KBN_GROUP_CD = 'G012'")

                        Dim dr3 As DataRow = ds.Tables("LMM540_ZONE_CHK").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("ZONE_CD") = dr1.Item("ZONE_CD")
                        dr3.Item("KBN_GROUP_CD") = dr2.Item("KBN_GROUP_CD")
                        dr3.Item("KBN_CD") = dr2.Item("KBN_CD")
                        dr3.Item("KBN_NM1") = dr2.Item("KBN_NM1")

                        ds.Tables("LMM540_ZONE_CHK").Rows.Add(dr3)
                        flgUpdZone = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertZoneChkData(ds)
            End If
        End If

        '薬機法
        If ds.Tables("LMM540IN_HAIKA_CHECK").Rows(0).Item("YAKKIHO_CHK").Equals("1") = True Then

            '棟室ゾーンチェックマスタ(薬機法)の物理削除
            ds.Tables("LMM540_KBN_CD").Rows.Clear()
            Dim dr0 As DataRow = ds.Tables("LMM540_KBN_CD").NewRow
            dr0.Item("KBN_GROUP_CD") = "G201"
            ds.Tables("LMM540_KBN_CD").Rows.Add(dr0)

            rtnResult = rtnResult AndAlso Me.DelTouSituZoneChkData(ds)

            ds.Tables("LMM540_TOU_SITU_CHK").Rows.Clear()
            ds.Tables("LMM540_ZONE_CHK").Rows.Clear()

            If rtnResult Then
                '棟室マスタを選択
                ds = Me.DacAccess(ds, "SelectListData5")

                '棟室をキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_TOU_SITU").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_CHK").Select("KBN_GROUP_CD = 'G201'")

                        Dim dr3 As DataRow = ds.Tables("LMM540_TOU_SITU_CHK").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("KBN_GROUP_CD") = dr2.Item("KBN_GROUP_CD")
                        dr3.Item("KBN_CD") = dr2.Item("KBN_CD")
                        dr3.Item("KBN_NM1") = dr2.Item("KBN_NM1")

                        ds.Tables("LMM540_TOU_SITU_CHK").Rows.Add(dr3)
                        flgUpdTouSitu = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertTouSituChkData(ds)
            End If

            If rtnResult Then
                'ゾーンマスタを選択
                ds = Me.DacAccess(ds, "SelectListData6")

                'ゾーンをキーにINSERT(取得行ごとに繰り返し)
                For Each dr1 As DataRow In ds.Tables("LMM540_HAIKA_ZONE").Rows

                    For Each dr2 As DataRow In ds.Tables("LMM540_TOU_CHK").Select("KBN_GROUP_CD = 'G201'")

                        Dim dr3 As DataRow = ds.Tables("LMM540_ZONE_CHK").NewRow
                        dr3.Item("NRS_BR_CD") = dr1.Item("NRS_BR_CD")
                        dr3.Item("WH_CD") = dr1.Item("WH_CD")
                        dr3.Item("TOU_NO") = dr1.Item("TOU_NO")
                        dr3.Item("SITU_NO") = dr1.Item("SITU_NO")
                        dr3.Item("ZONE_CD") = dr1.Item("ZONE_CD")
                        dr3.Item("KBN_GROUP_CD") = dr2.Item("KBN_GROUP_CD")
                        dr3.Item("KBN_CD") = dr2.Item("KBN_CD")
                        dr3.Item("KBN_NM1") = dr2.Item("KBN_NM1")

                        ds.Tables("LMM540_ZONE_CHK").Rows.Add(dr3)
                        flgUpdZone = True

                    Next

                Next

                rtnResult = rtnResult AndAlso Me.InsertZoneChkData(ds)
            End If
        End If

        '棟室マスタの更新者情報を更新
#If False Then  'upd 2021/10/08  024461 【LMS】棟マスタ一括更新_旧毒劇区分設定しな
        
        If flgUpdTouSitu Then
#Else

        If flgUpdTouSitu OrElse
            ds.Tables("LMM540IN_HAIKA_CHECK").Rows(0).Item("DOKU_CHK").Equals("1") = True Then

#End If
            '棟室マスタに関係する更新があった場合のみ
            rtnResult = rtnResult AndAlso Me.UpdateTouSituData(ds)
        End If

        'ZONEマスタの更新者情報を更新
        If flgUpdZone Then
            'ZONEマスタに関係する更新があった場合のみ
            rtnResult = rtnResult AndAlso Me.UpdateZoneData(ds)
        End If

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

        '削除処理
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

        '棟情報の論理削除
        Dim rtnResult As Boolean = Me.ServerChkJudge(ds, "DeleteTouM")

        '棟マスタ消防情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTouShoboM")

        '棟チェックマスタ情報の論理削除
        rtnResult = rtnResult AndAlso Me.ServerChkJudge(ds, "DeleteTouChkM")

        Return rtnResult

    End Function

#End Region

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

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

#Region "共通更新"

    ''' <summary>
    ''' 棟マスタ 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertTouM")

    End Function

    ''' <summary>
    ''' 棟マスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateTouData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateTouM")

    End Function

    ''' <summary>
    ''' 棟室マスタ 配下に反映
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertTouSituShoboM")

    End Function

    ''' <summary>
    ''' ゾーンマスタ 配下に反映
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertZoneShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertZoneShoboM")

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ(棟室） 配下に反映
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouSituChkData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertTouSituChkM")

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ(ゾーン) 配下に反映
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertZoneChkData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "InsertZoneChkM")

    End Function

    ''' <summary>
    ''' 棟マスタ消防情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTouShoboM")

    End Function


    ''' <summary>
    ''' 棟チェックマスタ情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouChkData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DelTouChkM")

    End Function

    ''' <summary>
    ''' 棟室マスタ消防情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouSituShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DeleteTouSituShoboM")

    End Function

    ''' <summary>
    ''' ゾーンマスタ消防情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelZoneShoboData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DeleteZoneShoboM")

    End Function

    ''' <summary>
    ''' 棟室ゾーンチェックマスタ情報 物理削除
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelTouSituZoneChkData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "DeleteTouSituZoneChkM")

    End Function

    ''' <summary>
    ''' 棟マスタ消防情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouShoboData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM540_TOU_SHOBO").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTouShoboM")
        End If

    End Function


    ''' <summary>
    ''' 棟チェックマスタ情報 新規
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function InsertTouChkData(ByVal ds As DataSet) As Boolean

        '新規登録する明細データがない場合、処理終了
        If ds.Tables("LMM540_TOU_CHK").Rows.Count = 0 Then
            Return True
        Else
            Return Me.ServerChkJudge(ds, "InsertTouChkM")
        End If

    End Function

    ''' <summary>
    ''' 棟室マスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateTouSituData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateTouSituM")

    End Function

    ''' <summary>
    ''' ZONEマスタ 更新
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function UpdateZoneData(ByVal ds As DataSet) As Boolean

        Return Me.ServerChkJudge(ds, "UpdateZoneM")

    End Function

#End Region


#End Region

End Class
