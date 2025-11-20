' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMR       : 完了
'  プログラムID     :  LMR010    : 完了取込
'  作  成  者       :  [矢内正之]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMR010BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMR010BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMR010DAC = New LMR010DAC()


#End Region

#Region "Method"

#Region "検索処理"

#Region "入荷検索処理"

    ''' <summary>
    ''' 更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectINKAData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectINKAData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 特定の荷主固有のテーブルが存在するか否かの判定
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function GetTrnTblExits(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "GetTrnTblExits", ds)

    End Function

    ''' <summary>
    ''' 更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListINKAData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListINKAData", ds)

    End Function

    'START YANAI 要望番号653
    ''' <summary>
    ''' 更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListINKAZAIData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListINKAZAIData", ds)

    End Function
    'END YANAI 要望番号653

#End Region

#Region "出荷検索処理"

    ''' <summary>
    ''' 更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectOUTKAData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectOUTKAData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf MyBase.GetLimitCount() < count Then
            ''閾値以上の場合
            'UPD 2019/06/26 006441【LMS】出荷完了できないデータがある
            'MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})

        End If

        Return ds

    End Function

    ''' <summary>
    ''' 更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListOUTKAData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListOUTKAData", ds)

    End Function

    'START YANAI 要望番号653
    ''' <summary>
    ''' 更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListOUTKAZAIData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListOUTKAZAIData", ds)

    End Function
    'END YANAI 要望番号653

    ''' <summary>
    ''' 更新対象データ検索（出荷データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListOUTKADataKANRYO(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListOUTKADataKANRYO", ds)

    End Function

    ''' <summary>
    ''' 更新対象データ検索（在庫データ）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListZAIDataKANRYO(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListZAIDataKANRYO", ds)

    End Function

#End Region

#Region "入荷チェックデータ検索処理"

    ''' <summary>
    ''' チェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckINKAData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectCheckINKAData", ds)

    End Function

#End Region

#Region "在庫チェックデータ検索処理"

    ''' <summary>
    ''' チェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckZAIData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectCheckZAIData", ds)

    End Function

#End Region

#Region "入荷進捗区分チェックデータ検索処理"

    ''' <summary>
    ''' チェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckDataInka(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectCheckDataInka", ds)

    End Function

#End Region

#Region "出荷進捗区分チェックデータ検索処理"

    ''' <summary>
    ''' チェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCheckDataOutka(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectCheckDataOutka", ds)

    End Function

#End Region

#Region "荷主データ検索処理"

    ''' <summary>
    ''' チェック対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectCustData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectCustData", ds)

    End Function

#End Region

#Region "作業検索処理"

    ''' <summary>
    ''' 更新対象データ件数検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectSagyoSijiData(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectSagyoSijiData", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G001")
        ElseIf count > MyBase.GetMaxResultCount() Then
            '表示最大件数を超える場合
            MyBase.SetMessage("E397", New String() {MyBase.GetMaxResultCount.ToString()})
        ElseIf MyBase.GetLimitCount() < count Then
            '閾値以上の場合
            MyBase.SetMessage("W001", New String() {MyBase.GetLimitCount.ToString()})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 更新対象データ検索
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListSagyoSijiData(ByVal ds As DataSet) As DataSet

        Return MyBase.CallDAC(_Dac, "SelectListSagyoSijiData", ds)

    End Function


#End Region

#Region "Rapidus出荷EDIデータテーブル 存在チェック"

    ''' <summary>
    ''' Rapidus出荷EDIデータテーブル 存在チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function CheckOutkaEdiDtlRapiExists(ByVal ds As DataSet) As Boolean

        ds.Tables("LMR010_TBL_EXISTS").Clear()
        Dim drTblExists As DataRow = ds.Tables("LMR010_TBL_EXISTS").NewRow()
        drTblExists.Item("NRS_BR_CD") = ds.Tables("LMR010INOUT").Rows(0).Item("NRS_BR_CD")
        drTblExists.Item("TBL_NM") = "H_OUTKAEDI_DTL_RAPI"
        ds.Tables("LMR010_TBL_EXISTS").Rows.Add(drTblExists)
        ds = Me.GetTrnTblExits(ds)

        Dim drExists As DataRow()
        drExists = ds.Tables("LMR010_TBL_EXISTS").Select("TBL_NM = 'H_OUTKAEDI_DTL_RAPI'")
        If drExists.Count > 0 AndAlso drExists(0).Item("TBL_EXISTS").ToString() = "1" Then
            Return True
        Else
            Return False
        End If

    End Function

#End Region ' "Rapidus出荷EDIデータテーブル 存在チェック"

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 完了処理 更新（入荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveInkaDataAction(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateSaveInkaDataAction", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 完了処理 更新（出荷）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveOutkaDataAction(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateSaveOutkaDataAction", ds)

        '振替処理判定
        Dim max As Integer = ds.Tables("LMR010_FURIKAE").Rows.Count - 1

        If -1 <> max Then
            'LMR010_FURIKAEにデータが入っている場合は、振替処理対象

            For i As Integer = 0 To max

                'データセット設定
                '入荷(大)データセット設定（商品がない方）
                ds = Me.SetInkaLData(ds, i)

                '入荷(中)データセット設定（商品がない方）
                ds = Me.SetInkaMData(ds, i)

                '入荷(小)データセット設定（商品がない方）
                ds = Me.SetInkaSData(ds, i)

                '出荷(大)データセット設定（商品がある方）
                ds = Me.SetOutkaLData(ds, i)

                '出荷(中)データセット設定（商品がある方）
                ds = Me.SetOutkaMData(ds, i)

                '出荷(小)データセット設定（商品がない方）
                ds = Me.SetOutkaSData(ds, i)

                '在庫データセット設定（商品がない方）
                ds = Me.SetZaiSakiData(ds, i)

                'START YANAI 要望番号510
                ''出荷(中)データセット設定（商品がない方）
                'ds = Me.SetOutkaMDataUpd(ds, i)
                'END YANAI 要望番号510

                '出荷(小)データセット設定（商品がある方）
                ds = Me.SetOutkaSDataUpd(ds, i)

                '採番して、データセットに設定
                ds = Me.SetSaibanData(ds)


                'データ更新
                '入荷(大)データ作成（商品がない方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriInkaLData", ds)

                '入荷(中)データ作成（商品がない方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriInkaMData", ds)

                '入荷(小)データ作成（商品がない方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriInkaSData", ds)

                '出荷(大)データ作成（商品がある方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriOutkaLData", ds)

                '出荷(中)データ作成（商品がある方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriOutkaMData", ds)

                'START YANAI 要望番号510
                '出荷(小)データ更新（商品がある方⇒ない方）
                ds = MyBase.CallDAC(Me._Dac, "UpdateSaveFuriOutkaSData", ds)
                'END YANAI 要望番号510

                '出荷(小)データ作成（商品がある方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriOutkaSData", ds)

                '在庫データ作成（商品がない方）
                ds = MyBase.CallDAC(Me._Dac, "InsertSaveFuriSakiZaiData", ds)

                'START YANAI 要望番号510
                ''出荷(中)データ更新（商品がある方）
                'ds = MyBase.CallDAC(Me._Dac, "UpdateSaveFuriOutkaMData", ds)

                ''出荷(小)データ更新（商品がある方）
                'ds = MyBase.CallDAC(Me._Dac, "UpdateSaveFuriOutkaSData", ds)
                'END YANAI 要望番号510

            Next

        End If

        If ("60").Equals(Convert.ToString(ds.Tables("LMR010INOUT").Rows(0).Item("INOUTKA_STATE_KB"))) = True Then
            '出荷完了の場合

            ' Rapidus出荷EDIデータテーブル 存在チェック
            If Me.CheckOutkaEdiDtlRapiExists(ds) Then
                ' 存在する場合

                ' Rapidus次回分納情報取得
                ds = MyBase.CallDAC(Me._Dac, "SelectJikaiBunnouInfo", ds)
                If MyBase.GetResultCount() > 0 Then
                    ' Rapidus次回分納情報ありの場合

                    ' 次回分納情報登録
                    ds = InsertJikaiBunnou(ds)
                End If
            End If
        End If

        Return ds

    End Function


    ''' <summary>
    ''' 完了処理 更新（作業）
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function UpdateSaveSagyoSijiDataAction(ByVal ds As DataSet) As DataSet

        'DACクラス呼出
        ds = MyBase.CallDAC(Me._Dac, "UpdateSaveSagyoSijiDataAction", ds)

        Return ds

    End Function

    ''' <summary>
    ''' 次回分納情報登録
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    Private Function InsertJikaiBunnou(ByVal ds As DataSet) As DataSet

        Dim num As New NumberMasterUtility
        Dim ediCtlNoNew As String

        Dim ediCtlNo As String
        ' Key: 分納元のEDI管理番号, Value: 新規採番した EDI管理番号
        Dim ediCtlNoDict As New Dictionary(Of String, String)

        ' EDI管理番号の採番(分納元のEDI管理番号 単位)
        For Each dr As DataRow In ds.Tables("LMR010_JIKAI_BUNNOU_OUT").Rows
            ediCtlNo = dr.Item("EDI_CTL_NO").ToString()
            If ediCtlNoDict.ContainsKey(ediCtlNo) Then
                Continue For
            End If
            ediCtlNoNew = num.GetAutoCode(NumberMasterUtility.NumberKbn.EDI_OUTKA_NO_L, Me, dr.Item("NRS_BR_CD").ToString())
            ediCtlNoDict.Add(ediCtlNo, ediCtlNoNew)
        Next
        For Each dr As DataRow In ds.Tables("LMR010_JIKAI_BUNNOU_OUT").Rows
            dr.Item("EDI_CTL_NO_NEW") = ediCtlNoDict(dr.Item("EDI_CTL_NO").ToString())
        Next

        ' Rapidus次回分納情報 出荷指示EDIデータ 同一ファイル名件数 取得
        Dim dsCnt As DataSet = ds.Clone()
        For Each dr As DataRow In ds.Tables("LMR010_JIKAI_BUNNOU_OUT").Rows
            dsCnt.Tables("LMR010_JIKAI_BUNNOU_OUT").Clear()
            dsCnt.Tables("LMR010_JIKAI_BUNNOU_OUT").ImportRow(dr)
            dsCnt = MyBase.CallDAC(Me._Dac, "SelectSameCrtDateAndFileNameCnt", dsCnt)
            dr.Item("SAME_CRT_DATE_AND_FILE_NAME_CNT") = MyBase.GetResultCount().ToString()
        Next

        ' 次回分納情報登録

        ' H_OUTKAEDI_DTL_RAPI
        ds = MyBase.CallDAC(Me._Dac, "InsertJikaiBunnouOutkaEdiRapi", ds)

        ' H_OUTKAEDI_L
        ds = MyBase.CallDAC(Me._Dac, "InsertJikaiBunnouOutkaEdiL", ds)

        ' H_OUTKAEDI_M
        ds = MyBase.CallDAC(Me._Dac, "InsertJikaiBunnouOutkaEdiM", ds)

        Return ds

    End Function


#End Region

#Region "DataSet設定処理"

    ''' <summary>
    ''' データセット設定(入荷大)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetInkaLData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_INKA_L_FURIKAE").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_INKA_L_FURIKAE").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("INKA_NO_L") = String.Empty
        dr("FURI_NO") = String.Empty
        dr("INKA_TP") = "50"
        dr("INKA_KB") = "30"
        dr("INKA_STATE_KB") = "50"
        dr("INKA_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("WH_CD") = furiDr("WH_CD").ToString
        dr("CUST_CD_L") = furiDr("CUST_CD_L_OUTKA").ToString
        dr("CUST_CD_M") = furiDr("CUST_CD_M_OUTKA").ToString
        dr("INKA_PLAN_QT") = furiDr("ALCTD_QT").ToString
        dr("INKA_PLAN_QT_UT") = furiDr("STD_IRIME_UT").ToString
        dr("INKA_TTL_NB") = furiDr("ALCTD_NB").ToString
        dr("BUYER_ORD_NO_L") = String.Empty
        dr("OUTKA_FROM_ORD_NO_L") = String.Empty
        dr("SEIQTO_CD") = String.Empty
        dr("TOUKI_HOKAN_YN") = "00"
        dr("HOKAN_YN") = "01"
        dr("HOKAN_FREE_KIKAN") = furiDr("HOKAN_FREE_KIKAN").ToString
        dr("HOKAN_STR_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("NIYAKU_YN") = "00"
        'START YANAI 要望番号589
        'dr("TAX_KB") = "01"
        dr("TAX_KB") = furiDr("TAX_KB").ToString
        'END YANAI 要望番号589
        dr("REMARK") = String.Empty
        dr("REMARK_OUT") = String.Empty
        dr("CHECKLIST_PRT_DATE") = String.Empty
        dr("CHECKLIST_PRT_USER") = String.Empty
        dr("UKETSUKELIST_PRT_DATE") = String.Empty
        dr("UKETSUKELIST_PRT_USER") = String.Empty
        dr("UKETSUKE_DATE") = String.Empty
        dr("UKETSUKE_USER") = String.Empty
        dr("KEN_DATE") = String.Empty
        dr("KEN_USER") = String.Empty
        dr("INKO_DATE") = String.Empty
        dr("INKO_USER") = String.Empty
        dr("HOUKOKUSYO_PR_DATE") = String.Empty
        dr("HOUKOKUSYO_PR_USER") = String.Empty
        dr("UNCHIN_TP") = "00"
        dr("UNCHIN_KB") = String.Empty

        'データセットに設定
        ds.Tables("LMR010_INKA_L_FURIKAE").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(入荷中)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetInkaMData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_INKA_M_FURIKAE").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_INKA_M_FURIKAE").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("INKA_NO_L") = String.Empty
        dr("INKA_NO_M") = "001"
        'START YANAI 要望番号510
        'dr("GOODS_CD_NRS") = furiDr("GOODS_CD_NRS_FROM").ToString
        dr("GOODS_CD_NRS") = furiDr("GOODS_CD_NRS").ToString
        'END YANAI 要望番号510
        dr("OUTKA_FROM_ORD_NO_M") = String.Empty
        dr("BUYER_ORD_NO_M") = String.Empty
        dr("REMARK") = String.Empty
        dr("PRINT_SORT") = "99"

        'データセットに設定
        ds.Tables("LMR010_INKA_M_FURIKAE").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(入荷小)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetInkaSData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_INKA_S_FURIKAE").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_INKA_S_FURIKAE").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("INKA_NO_L") = String.Empty
        dr("INKA_NO_M") = "001"
        dr("INKA_NO_S") = "001"
        dr("ZAI_REC_NO") = String.Empty
        dr("LOT_NO") = furiDr("LOT_NO").ToString
        dr("LOCA") = furiDr("LOCA").ToString
        dr("TOU_NO") = furiDr("TOU_NO").ToString
        dr("SITU_NO") = furiDr("SITU_NO").ToString
        dr("ZONE_CD") = furiDr("ZONE_CD").ToString
        dr("HASU") = Convert.ToString(Convert.ToDecimal(furiDr("ALCTD_NB").ToString) Mod (Convert.ToDecimal(furiDr("PKG_NB").ToString)))
        dr("KONSU") = Convert.ToString((Convert.ToDecimal(furiDr("ALCTD_NB").ToString) - Convert.ToDecimal(dr("HASU").ToString)) / (Convert.ToDecimal(furiDr("PKG_NB").ToString)))
        dr("IRIME") = furiDr("IRIME").ToString
        dr("BETU_WT") = furiDr("BETU_WT").ToString
        dr("SERIAL_NO") = furiDr("SERIAL_NO").ToString
        dr("GOODS_COND_KB_1") = furiDr("GOODS_COND_KB_1").ToString
        dr("GOODS_COND_KB_2") = furiDr("GOODS_COND_KB_2").ToString
        dr("GOODS_COND_KB_3") = furiDr("GOODS_COND_KB_3").ToString
        dr("GOODS_CRT_DATE") = furiDr("GOODS_CRT_DATE").ToString
        dr("LT_DATE") = furiDr("LT_DATE").ToString
        dr("SPD_KB") = furiDr("SPD_KB").ToString
        dr("OFB_KB") = furiDr("OFB_KB").ToString
        dr("DEST_CD") = String.Empty
        dr("REMARK") = furiDr("REMARK").ToString
        dr("ALLOC_PRIORITY") = furiDr("ALLOC_PRIORITY").ToString
        dr("REMARK_OUT") = String.Empty

        'データセットに設定
        ds.Tables("LMR010_INKA_S_FURIKAE").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(出荷大)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetOutkaLData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_OUTKA_L_FURIKAE").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_OUTKA_L_FURIKAE").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("OUTKA_NO_L") = String.Empty
        dr("FURI_NO") = String.Empty
        dr("OUTKA_KB") = "30"
        dr("SYUBETU_KB") = "50"
        dr("OUTKA_STATE_KB") = "60"
        dr("OUTKAHOKOKU_YN") = "00"
        dr("PICK_KB") = "01"
        dr("DENP_NO") = furiDr("DENP_NO").ToString
        dr("ARR_KANRYO_INFO") = furiDr("ARR_KANRYO_INFO").ToString
        dr("WH_CD") = furiDr("WH_CD").ToString
        dr("OUTKA_PLAN_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("OUTKO_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("ARR_PLAN_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("ARR_PLAN_TIME") = furiDr("ARR_PLAN_TIME").ToString
        dr("HOKOKU_DATE") = furiDr("HOKOKU_DATE").ToString
        dr("TOUKI_HOKAN_YN") = "01"
        dr("END_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("CUST_CD_L") = furiDr("CUST_CD_L_ZAI").ToString
        dr("CUST_CD_M") = furiDr("CUST_CD_M_ZAI").ToString
        dr("SHIP_CD_L") = furiDr("SHIP_CD_L").ToString
        dr("SHIP_CD_M") = furiDr("SHIP_CD_M").ToString
        'START YANAI 要望番号506
        'dr("DEST_CD") = furiDr("DEST_CD").ToString
        'dr("DEST_AD_3") = furiDr("DEST_AD_3").ToString
        'dr("DEST_TEL") = furiDr("DEST_TEL").ToString
        dr("DEST_CD") = String.Empty
        dr("DEST_AD_3") = String.Empty
        dr("DEST_TEL") = String.Empty
        'END YANAI 要望番号506
        dr("NHS_REMARK") = furiDr("NHS_REMARK").ToString
        dr("SP_NHS_KB") = furiDr("SP_NHS_KB").ToString
        dr("COA_YN") = furiDr("COA_YN").ToString
        dr("CUST_ORD_NO") = furiDr("CUST_ORD_NO").ToString
        dr("BUYER_ORD_NO") = furiDr("BUYER_ORD_NO").ToString
        dr("REMARK") = furiDr("OUTKA_L_REMARK").ToString
        dr("OUTKA_PKG_NB") = furiDr("OUTKA_PKG_NB").ToString
        dr("DENP_YN") = furiDr("DENP_YN").ToString
        dr("PC_KB") = furiDr("PC_KB").ToString
        dr("NIYAKU_YN") = "00"
        dr("ALL_PRINT_FLAG") = furiDr("ALL_PRINT_FLAG").ToString
        dr("NIHUDA_FLAG") = furiDr("NIHUDA_FLAG").ToString
        dr("NHS_FLAG") = furiDr("NHS_FLAG").ToString
        dr("DENP_FLAG") = furiDr("DENP_FLAG").ToString
        dr("COA_FLAG") = furiDr("COA_FLAG").ToString
        dr("HOKOKU_FLAG") = furiDr("HOKOKU_FLAG").ToString
        dr("MATOME_PICK_FLAG") = furiDr("MATOME_PICK_FLAG").ToString
        dr("LAST_PRINT_DATE") = furiDr("LAST_PRINT_DATE").ToString
        dr("LAST_PRINT_TIME") = furiDr("LAST_PRINT_TIME").ToString
        dr("SASZ_USER") = furiDr("SASZ_USER").ToString
        dr("OUTKO_USER") = furiDr("OUTKO_USER").ToString
        dr("KEN_USER") = furiDr("KEN_USER").ToString
        dr("OUTKA_USER") = furiDr("OUTKA_USER").ToString
        dr("HOU_USER") = furiDr("HOU_USER").ToString
        dr("ORDER_TYPE") = furiDr("ORDER_TYPE").ToString
        'START YANAI 要望番号510
        dr("DEST_KB") = String.Empty
        dr("DEST_NM") = String.Empty
        dr("DEST_AD_1") = String.Empty
        dr("DEST_AD_2") = String.Empty
        'END YANAI 要望番号510

        'データセットに設定
        ds.Tables("LMR010_OUTKA_L_FURIKAE").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(出荷中)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetOutkaMData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_OUTKA_M_FURIKAE").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_OUTKA_M_FURIKAE").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("OUTKA_NO_L") = String.Empty
        dr("OUTKA_NO_M") = "001"
        dr("EDI_SET_NO") = furiDr("EDI_SET_NO").ToString
        dr("COA_YN") = furiDr("COA_YN_M").ToString
        dr("CUST_ORD_NO_DTL") = furiDr("CUST_ORD_NO_DTL").ToString
        dr("BUYER_ORD_NO_DTL") = furiDr("BUYER_ORD_NO_DTL").ToString
        'START YANAI 要望番号510
        'dr("GOODS_CD_NRS") = furiDr("GOODS_CD_NRS").ToString
        dr("GOODS_CD_NRS") = furiDr("GOODS_CD_NRS_FROM").ToString
        'END YANAI 要望番号510
        dr("RSV_NO") = furiDr("RSV_NO").ToString
        dr("LOT_NO") = furiDr("LOT_NO").ToString
        dr("SERIAL_NO") = furiDr("SERIAL_NO").ToString
        dr("ALCTD_KB") = furiDr("ALCTD_KB").ToString
        dr("OUTKA_PKG_NB") = furiDr("OUTKA_PKG_NB_M").ToString
        dr("OUTKA_HASU") = furiDr("ALCTD_NB").ToString
        dr("OUTKA_QT") = furiDr("ALCTD_QT").ToString
        dr("OUTKA_TTL_NB") = furiDr("ALCTD_NB").ToString
        dr("OUTKA_TTL_QT") = furiDr("ALCTD_QT").ToString
        dr("ALCTD_NB") = furiDr("ALCTD_NB").ToString
        dr("ALCTD_QT") = furiDr("ALCTD_QT").ToString
        dr("BACKLOG_NB") = "0"
        dr("BACKLOG_QT") = "0"
        dr("UNSO_ONDO_KB") = furiDr("UNSO_ONDO_KB").ToString
        dr("IRIME") = furiDr("IRIME_M").ToString
        dr("IRIME_UT") = furiDr("IRIME_UT").ToString
        dr("OUTKA_M_PKG_NB") = furiDr("OUTKA_M_PKG_NB").ToString
        dr("REMARK") = furiDr("OUTKA_M_REMARK").ToString
        dr("SIZE_KB") = furiDr("SIZE_KB").ToString
        dr("ZAIKO_KB") = furiDr("ZAIKO_KB").ToString
        dr("SOURCE_CD") = furiDr("SOURCE_CD").ToString
        dr("YELLOW_CARD") = furiDr("YELLOW_CARD").ToString
        dr("PRINT_SORT") = "99"

        'データセットに設定
        ds.Tables("LMR010_OUTKA_M_FURIKAE").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(出荷小)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetOutkaSData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_OUTKA_S_FURIKAE").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_OUTKA_S_FURIKAE").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        'START YANAI 要望番号510
        'dr("OUTKA_NO_L") = String.Empty
        'dr("OUTKA_NO_M") = "001"
        'dr("OUTKA_NO_S") = "001"
        dr("OUTKA_NO_L") = furiDr("OUTKA_NO_L").ToString
        dr("OUTKA_NO_M") = furiDr("OUTKA_NO_M").ToString
        dr("OUTKA_NO_S") = furiDr("OUTKA_NO_S").ToString
        'END YANAI 要望番号510
        dr("TOU_NO") = furiDr("TOU_NO").ToString
        dr("SITU_NO") = furiDr("SITU_NO").ToString
        dr("ZONE_CD") = furiDr("ZONE_CD").ToString
        dr("LOCA") = furiDr("LOCA").ToString
        dr("LOT_NO") = furiDr("LOT_NO").ToString
        dr("SERIAL_NO") = furiDr("SERIAL_NO").ToString
        dr("OUTKA_TTL_NB") = furiDr("OUTKA_TTL_NB").ToString
        dr("OUTKA_TTL_QT") = furiDr("OUTKA_TTL_QT").ToString
        dr("ZAI_REC_NO") = furiDr("ZAI_REC_NO").ToString
        dr("INKA_NO_L") = furiDr("INKA_NO_L").ToString
        dr("INKA_NO_M") = furiDr("INKA_NO_M").ToString
        dr("INKA_NO_S") = furiDr("INKA_NO_S").ToString
        dr("ZAI_UPD_FLAG") = "01"
        dr("ALCTD_CAN_NB") = "0"
        dr("ALCTD_NB") = furiDr("ALCTD_NB").ToString
        dr("ALCTD_CAN_QT") = "0"
        dr("ALCTD_QT") = furiDr("ALCTD_QT").ToString
        dr("IRIME") = furiDr("IRIME").ToString
        dr("BETU_WT") = furiDr("BETU_WT").ToString
        dr("COA_FLAG") = "00"
        dr("REMARK") = furiDr("REMARK").ToString
        dr("SMPL_FLAG") = furiDr("SMPL_FLAG").ToString

        'データセットに設定
        ds.Tables("LMR010_OUTKA_S_FURIKAE").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(振替先の在庫)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetZaiSakiData(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_ZAI_FURIKAE_SAKI").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_ZAI_FURIKAE_SAKI").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("ZAI_REC_NO") = String.Empty
        dr("WH_CD") = furiDr("WH_CD").ToString
        dr("TOU_NO") = furiDr("TOU_NO").ToString
        dr("SITU_NO") = furiDr("SITU_NO").ToString
        dr("ZONE_CD") = furiDr("ZONE_CD").ToString
        dr("LOCA") = furiDr("LOCA").ToString
        dr("LOT_NO") = furiDr("LOT_NO").ToString
        dr("CUST_CD_L") = furiDr("CUST_CD_L_OUTKA").ToString
        dr("CUST_CD_M") = furiDr("CUST_CD_M_OUTKA").ToString
        'START YANAI 要望番号510
        'dr("GOODS_CD_NRS") = furiDr("GOODS_CD_NRS_FROM").ToString
        dr("GOODS_CD_NRS") = furiDr("GOODS_CD_NRS").ToString
        'END YANAI 要望番号510
        dr("INKA_NO_L") = String.Empty
        dr("INKA_NO_M") = "001"
        dr("INKA_NO_S") = "001"
        dr("ALLOC_PRIORITY") = furiDr("ALLOC_PRIORITY").ToString
        dr("RSV_NO") = furiDr("RSV_NO_ZAI").ToString
        dr("SERIAL_NO") = furiDr("SERIAL_NO_ZAI").ToString
        dr("HOKAN_YN") = furiDr("HOKAN_YN").ToString
        'START YANAI 要望番号589
        'dr("TAX_KB") = "01"
        dr("TAX_KB") = furiDr("TAX_KB").ToString
        'END YANAI 要望番号589
        dr("GOODS_COND_KB_1") = furiDr("GOODS_COND_KB_1").ToString
        dr("GOODS_COND_KB_2") = furiDr("GOODS_COND_KB_2").ToString
        dr("GOODS_COND_KB_3") = furiDr("GOODS_COND_KB_3").ToString
        dr("OFB_KB") = "01"
        dr("SPD_KB") = furiDr("SPD_KB").ToString
        dr("REMARK_OUT") = furiDr("REMARK_OUT").ToString
        dr("PORA_ZAI_NB") = "0"
        dr("ALCTD_NB") = "0"
        dr("ALLOC_CAN_NB") = "0"
        dr("IRIME") = furiDr("IRIME").ToString
        dr("PORA_ZAI_QT") = "0"
        dr("ALCTD_QT") = "0"
        dr("ALLOC_CAN_QT") = "0"
        dr("INKO_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("INKO_PLAN_DATE") = furiDr("OUTKA_PLAN_DATE").ToString
        dr("ZERO_FLAG") = "00"
        dr("LT_DATE") = furiDr("LT_DATE").ToString
        dr("GOODS_CRT_DATE") = furiDr("GOODS_CRT_DATE").ToString
        dr("DEST_CD_P") = furiDr("DEST_CD_ZAI").ToString
        dr("REMARK") = furiDr("REMARK").ToString
        dr("SMPL_FLAG") = furiDr("SMPL_FLAG").ToString

        'データセットに設定
        ds.Tables("LMR010_ZAI_FURIKAE_SAKI").Rows.Add(dr)

        Return ds

    End Function

    'START YANAI 要望番号510
    '''' <summary>
    '''' データセット設定(出荷中)
    '''' </summary>
    '''' <param name="ds">データセット</param>
    '''' <remarks></remarks>
    'Private Function SetOutkaMDataUpd(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

    '    ds.Tables("LMR010_OUTKA_M_FURIKAE_SAKI").Clear()
    '    Dim dr As DataRow = ds.Tables("LMR010_OUTKA_M_FURIKAE_SAKI").NewRow()
    '    Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

    '    dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
    '    dr("OUTKA_NO_L") = furiDr("OUTKA_NO_L").ToString
    '    dr("OUTKA_NO_M") = furiDr("OUTKA_NO_M").ToString
    '    dr("GOODS_CD_NRS_FROM") = furiDr("GOODS_CD_NRS_FROM").ToString

    '    'データセットに設定
    '    ds.Tables("LMR010_OUTKA_M_FURIKAE_SAKI").Rows.Add(dr)

    '    Return ds

    'End Function
    'END YANAI 要望番号510

    ''' <summary>
    ''' データセット設定(出荷小)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetOutkaSDataUpd(ByVal ds As DataSet, ByVal cnt As Integer) As DataSet

        ds.Tables("LMR010_OUTKA_S_FURIKAE_SAKI").Clear()
        Dim dr As DataRow = ds.Tables("LMR010_OUTKA_S_FURIKAE_SAKI").NewRow()
        Dim furiDr As DataRow = ds.Tables("LMR010_FURIKAE").Rows(cnt)

        dr("NRS_BR_CD") = furiDr("NRS_BR_CD").ToString
        dr("OUTKA_NO_L") = furiDr("OUTKA_NO_L").ToString
        dr("OUTKA_NO_M") = furiDr("OUTKA_NO_M").ToString
        dr("OUTKA_NO_S") = furiDr("OUTKA_NO_S").ToString
        dr("INKA_NO_M") = "001"
        dr("INKA_NO_S") = "001"
        dr("ZAI_UPD_FLAG") = "01"

        'データセットに設定
        ds.Tables("LMR010_OUTKA_S_FURIKAE_SAKI").Rows.Add(dr)

        Return ds

    End Function

    ''' <summary>
    ''' データセット設定(採番)
    ''' </summary>
    ''' <param name="ds">データセット</param>
    ''' <remarks></remarks>
    Private Function SetSaibanData(ByVal ds As DataSet) As DataSet

        Dim dr As DataRow = Nothing

        '採番
        Dim num As New NumberMasterUtility
        Dim inkaNoL As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.INKA_NO_L, Me, ds.Tables("LMR010_FURIKAE").Rows(0).Item("NRS_BR_CD").ToString())
        Dim outkaNoL As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.OUTKA_NO_L, Me, ds.Tables("LMR010_FURIKAE").Rows(0).Item("NRS_BR_CD").ToString())
        Dim furiNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.FURI_CTL_NO, Me, ds.Tables("LMR010_FURIKAE").Rows(0).Item("NRS_BR_CD").ToString())
        Dim zaiNo As String = num.GetAutoCode(NumberMasterUtility.NumberKbn.ZAI_REC_NO, Me, ds.Tables("LMR010_FURIKAE").Rows(0).Item("NRS_BR_CD").ToString())

        '入荷(大)
        dr = ds.Tables("LMR010_INKA_L_FURIKAE").Rows(0)
        dr("INKA_NO_L") = inkaNoL
        dr("FURI_NO") = furiNo

        '入荷(中)
        dr = ds.Tables("LMR010_INKA_M_FURIKAE").Rows(0)
        dr("INKA_NO_L") = inkaNoL

        '入荷(小)
        dr = ds.Tables("LMR010_INKA_S_FURIKAE").Rows(0)
        dr("INKA_NO_L") = inkaNoL
        dr("ZAI_REC_NO") = zaiNo

        '出荷(大)
        dr = ds.Tables("LMR010_OUTKA_L_FURIKAE").Rows(0)
        dr("OUTKA_NO_L") = outkaNoL
        dr("FURI_NO") = furiNo

        '出荷(中)
        dr = ds.Tables("LMR010_OUTKA_M_FURIKAE").Rows(0)
        dr("OUTKA_NO_L") = outkaNoL

        'START YANAI 要望番号510
        ''出荷(小)
        'dr = ds.Tables("LMR010_OUTKA_S_FURIKAE").Rows(0)
        '出荷(小)（振替先)
        dr = ds.Tables("LMR010_OUTKA_S_FURIKAE_SAKI").Rows(0)
        'END YANAI 要望番号510
        dr("OUTKA_NO_L") = outkaNoL
        'START YANAI 要望番号510
        dr("ZAI_REC_NO") = zaiNo
        dr("INKA_NO_L") = inkaNoL
        'END YANAI 要望番号510

        '在庫（振替先）…振替先とは、在庫がなくて出荷ができなかった方
        dr = ds.Tables("LMR010_ZAI_FURIKAE_SAKI").Rows(0)
        dr("ZAI_REC_NO") = zaiNo
        dr("INKA_NO_L") = inkaNoL

        '出荷(小)（振替元）…出荷画面にて入力したデータを更新する
        dr = ds.Tables("LMR010_OUTKA_S_FURIKAE").Rows(0)
        dr("INKA_NO_L") = inkaNoL
        dr("ZAI_REC_NO") = zaiNo

        Return ds

    End Function

#End Region

#End Region

End Class
