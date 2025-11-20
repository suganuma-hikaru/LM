' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMN       : ＳＣＭ
'  プログラムID     :  LMN010BLF : 出荷データ一覧
'  作  成  者       :  [佐川央]
' ==========================================================================
Imports System.Transactions
Imports Jp.Co.Nrs.Com.Base
Imports Jp.Co.Nrs.LM.BLC
Imports Jp.Co.Nrs.LM.DSL
Imports Jp.Co.Nrs.LM.Utility

''' <summary>
''' LMN010BLFクラス
''' </summary>
''' <remarks></remarks>
Public Class LMN010BLF
    Inherits Jp.Co.Nrs.LM.Base.BLF.LMBaseBLF

#Region "Field"

    ''' <summary>
    ''' 使用するBLCクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Blc As LMN010BLC = New LMN010BLC()

#End Region

#Region "Method"

#Region "検索処理"

    ''' <summary>
    ''' 出荷EDIデータ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectListData(ByVal ds As DataSet) As DataSet

        '強制実行フラグにより処理判定
        If MyBase.GetForceOparation() = False Then

            'データ件数取得
            ds = MyBase.CallBLC(Me._Blc, "SelectData", ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

        End If

        '検索結果取得
        ds = MyBase.CallBLC(Me._Blc, "SelectListData", ds)

        '検索条件のステータスが「倉庫指示済」、「実績報告済」の場合
        Dim StatusKbn As String = ds.Tables("LMN010IN").Rows(0).Item("HED_STATUS_KBN").ToString
        If StatusKbn = "02" Or StatusKbn = "03" Then

            '***********************  LMS側出荷日、納入日を取得  *************************

            'データテーブル名取得
            Dim outNm As String = "LMN010OUT"
            Dim lmsDate As String = "LMS_DATE"
            Dim brNm As String = "BR_CD_LIST"
            '検索結果データテーブル
            Dim outDt As DataTable = ds.Tables(outNm)
            '取得結果格納用データセット
            Dim inDs As DataSet = New LMN010DS()
            Dim inDt As DataTable = inDs.Tables(outNm)

            '検索結果データを営業所毎に分割し処理
            Dim brDt As DataTable = ds.Tables(brNm)
            Dim brNum As Integer = brDt.Rows.Count
            For i As Integer = 0 To brNum - 1

                '格納用データセットの初期化
                inDs.Clear()

                '営業所コードを一行設定
                inDs.Tables(brNm).ImportRow(brDt.Rows(i))

                '処理対象営業所コード取得
                Dim brCd As String = brDt.Rows(i).Item("BR_CD").ToString()
                '抽出用フィルタ設定(処理対象営業所データを抽出)
                Dim filter As String = String.Concat("BR_CD = '", brCd, "'")
                '検索結果から処理対象営業所データを抽出
                Dim selectDr As DataRow() = outDt.Select(filter)
                '抽出データ数取得
                Dim selectDrNum As Integer = selectDr.Length

                '抽出データが１件以上存在する場合処理をする
                If selectDrNum > 0 Then

                    '抽出したデータを設定
                    For j As Integer = 0 To selectDrNum - 1
                        inDt.ImportRow(selectDr(j))
                    Next

                    'LMS側出荷日、納入日を取得
                    inDs = MyBase.CallBLC(Me._Blc, "KensakuGetLMSDate", inDs)

                    '取得したLMS側出荷日、納入日を検索結果データテーブルに設定
                    '検索結果に対応するLMS側日付を一行ずつ設定
                    Dim lmsTbl As DataTable = inDs.Tables(lmsDate)
                    Dim outDtNum As Integer = outDt.Rows.Count
                    For k As Integer = 0 To outDtNum - 1
                        '設定用キー項目取得
                        Dim crtDate As String = outDt.Rows(k).Item("CRT_DATE").ToString()
                        Dim fileName As String = outDt.Rows(k).Item("FILE_NAME").ToString()
                        Dim recNo As String = outDt.Rows(k).Item("REC_NO").ToString()
                        'LMS日付設定用データ抽出用フィルタ
                        Dim lmsfilter As String = String.Concat("CRT_DATE = '", crtDate, "' AND FILE_NAME = '", fileName, "' AND REC_NO = '", recNo, "'")
                        'LMS日付設定データ取得
                        Dim dr As DataRow() = lmsTbl.Select(lmsfilter)
                        '1件取得した場合
                        If dr.Length > 0 Then
                            'LMS日付設定
                            outDt.Rows(k).Item("LMS_OUTKA_DATE") = dr(0).Item("LMS_OUTKA_DATE").ToString()
                            outDt.Rows(k).Item("LMS_ARR_DATE") = dr(0).Item("LMS_ARR_DATE").ToString()
                        End If
                    Next

                End If

            Next

        End If

        '検索結果取得
        Return ds

    End Function

#End Region

#Region "設定処理"

    ''' <summary>
    ''' 設定処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SetteiData(ByVal ds As DataSet) As DataSet

        '更新対象テーブル名
        Dim tableNm As String = "LMN010IN"

        '******* 新規登録データ / 更新データを作成 ********

        '新規登録データ/更新データ 格納用
        Dim insDs As DataSet = New LMN010DS()
        Dim insDt As DataTable = insDs.Tables(tableNm)
        '新規登録用データ抽出
        Dim updDt As DataTable = ds.Tables(tableNm)
        Dim insdr As DataRow() = updDt.Select("INSERT_FLG = '1'")

        '******* 新規登録 / 更新処理を行う ***************

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            Dim max As Integer = insdr.Length - 1
            Dim num As New NumberMasterUtility
            For i As Integer = 0 To max
                '新規登録用データ設定
                'SCM管理番号Lを採番する
                insdr(i).Item("SCM_CTL_NO_L") = num.GetAutoCode(NumberMasterUtility.NumberKbn.SCM_CTL_NO_L, Me)
                insDt.ImportRow(insdr(i))
                '更新用データから新規登録対象データを削除
                updDt.Rows.Remove(insdr(i))
            Next

            '処理件数用
            Dim ResultCount As Integer = 0
            Dim insResultCount As Integer = 0
            Dim updResultCount As Integer = 0

            If insDt.Rows.Count <> 0 Then
                '新規登録処理を行う
                insResultCount = SetteiInsertData(insDs)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return insDs
                End If

            End If

            If updDt.Rows.Count <> 0 Then
                '更新処理を行う
                updResultCount = SetteiUpdateData(ds)
                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If
            End If

            '処理件数取得
            ResultCount = insResultCount + updResultCount

            MyBase.SetMessage("G002", New String() {"設定処理", String.Concat(ResultCount.ToString(), "件")})

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="ds">DataSet(登録用)</param>
    ''' <remarks></remarks>
    Private Function SetteiInsertData(ByVal ds As DataSet) As Integer

        '処理件数用
        Dim updResultCount As Integer

        '存在チェック(N_OUTKASCM_L)
        ds = MyBase.CallBLC(Me._Blc, "CheckExistN_OUTKASCM_L", ds)

        '排他チェック(N_OUTKASCM_HED_BP)
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaN_OUTKASCM_HED_BP", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then

            'データの新規登録(N_OUTKASCM_L)
            ds = MyBase.CallBLC(Me._Blc, "SetteiInsertN_OUTKASCM_L", ds)

            'データの抽出(N_OUTKASCM_DTL_BP)
            ds = MyBase.CallBLC(Me._Blc, "SetteiSelectN_OUTKASCM_DTL_BP", ds)

            'データの新規登録(N_OUTKASCM_M)
            ds = MyBase.CallBLC(Me._Blc, "SetteiInsertN_OUTKASCM_M", ds)

            'データの更新(N_OUTKASCM_HED_BP)
            ds = MyBase.CallBLC(Me._Blc, "SetteiUpdateN_OUTKASCM_HED_BP", ds)
            '処理件数取得(ヘッダ単位)
            updResultCount = GetResultCount()

            'データの更新(N_OUTKASCM_DTL_BP)
            ds = MyBase.CallBLC(Me._Blc, "SetteiUpdateN_OUTKASCM_DTL_BP", ds)

        End If

        Return updResultCount

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds">DataSet(更新用)</param>
    ''' <remarks></remarks>
    Private Function SetteiUpdateData(ByVal ds As DataSet) As Integer

        Dim insResultCount As Integer

        '排他チェック(N_OUTKA_SCM_L)
        ds = MyBase.CallBLC(Me._Blc, "CheckHaitaN_OUTKASCM_L", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() = False Then
            'データの更新(N_OUTKA_SCM_L)
            ds = MyBase.CallBLC(Me._Blc, "SetteiUpdateN_OUTKASCM_L", ds)
            '処理件数取得(ヘッダ単位)
            insResultCount = GetResultCount()
        End If

        Return insResultCount

    End Function

#End Region

#Region "送信指示"

    ''' <summary>
    ''' 送信指示データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SoushinShijiSelect(ByVal ds As DataSet) As DataSet

        '送信指示データ取得(N_OUTKASCM_HED_BP)
        ds = MyBase.CallBLC(Me._Blc, "SoushinShijiSelectN_OUTKASCM_HED_BP", ds)

        '送信指示データ取得(N_OUTKASCM_DTL_BP)
        ds = MyBase.CallBLC(Me._Blc, "SoushinShijiSelectN_OTUKASCM_DTL_BP", ds)

        '営業所リスト毎に処理
        Dim dt As DataTable = ds.Tables("BR_CD_LIST")
        Dim brNum As Integer = dt.Rows.Count - 1

        '処理件数取得用
        Dim resultCount As Integer = 0

        For i As Integer = 0 To brNum
            '初期化処理
            Dim lmh800Ds As LMH800DS = Nothing

            'フィルタ用営業所コード取得
            Dim BrCd As String = dt.Rows(i)(0).ToString

            'データ格納用データセット
            Dim insDs As DataSet = New LMN010DS()

            LMH800DS = New LMH800DS
            Dim lmh800row As LMH800DS.LMH800CUST_INFORow = LMH800DS.LMH800CUST_INFO.NewLMH800CUST_INFORow
            lmh800row.SCM_CUST_CD = dt.Rows(i)("SCM_CUST_CD").ToString
            lmh800row.BR_CD = BrCd
            lmh800Ds.LMH800CUST_INFO.AddLMH800CUST_INFORow(lmh800row)

            '営業所毎ヘッダデータ
            Dim writedHedRow As DataRow() = ds.Tables("N_OUTKASCM_HED_BP").Select(String.Concat("BR_CD = ", BrCd))
            Dim maxHed As Integer = writedHedRow.Length - 1
            For j As Integer = 0 To maxHed
                insDs.Tables("N_OUTKASCM_HED_BP").ImportRow(writedHedRow(j))
                LMH800DS = Me.CreateSettingHedData(writedHedRow(j), LMH800DS)
            Next

            '排他チェック(N_OUTKASCM_L)
            Dim checkRow As DataRow() = ds.Tables("LMN010IN").Select(String.Concat("BR_CD = ", BrCd))
            Dim checkNum As Integer = checkRow.Length
            For l As Integer = 0 To checkNum - 1
                insDs.Tables("LMN010IN").ImportRow(checkRow(l))
            Next
            insDs = MyBase.CallBLC(Me._Blc, "SoushinShijiCheckHaitaN_OUTKASCM_L", insDs)
            'メッセージの判定(エラー有り)
            If MyBase.GetResultCount() = 0 Then
                '処理をせず次へ
                Continue For
            End If

            '営業所毎明細データ
            Dim writeDtlRow As DataRow() = ds.Tables("N_OUTKASCM_DTL_BP").Select(String.Concat("BR_CD = ", BrCd))
            Dim maxDtl As Integer = writeDtlRow.Length - 1
            For k As Integer = 0 To maxDtl
                insDs.Tables("N_OUTKASCM_DTL_BP").ImportRow(writeDtlRow(k))
                lmh800Ds = Me.CreateSettingDtlData(writeDtlRow(k), lmh800Ds)
            Next

            'トランザクション開始
            Using scope As TransactionScope = MyBase.BeginTransaction()

                '**********バッチ(LMH800)起動**************
                '*** 営業所毎のSCMH,SCMDを渡す(insDs)
                lmh800Ds = DirectCast(MyBase.CallBLC(New LMH800BLC, "ExecuteSoushinShiji", lmh800Ds), LMH800DS)
                If "00".Equals(lmh800Ds.RESULT(0).RESULT) = False Then
                    Me.SetMessage(lmh800Ds.RESULT(0).ERROR_CD, New String() {"出荷指示データ送信"})
                    'Commitせずに次へ
                    Continue For

                End If

                '出荷EDIデータLのステータスを倉庫指示済にアップデート
                insDs = MyBase.CallBLC(Me._Blc, "SoushinShijiUpdateN_OUTKASCM_L", insDs)
                resultCount = resultCount + MyBase.GetResultCount()

                'トランザクション終了
                MyBase.CommitTransaction(scope)
            End Using

        Next

        'エラーメッセージが存在しない場合、処理件数メッセージを設定
        If IsMessageExist() = False Then
            Me.SetMessage("G002", New String() {"指示送信", String.Concat(resultCount.ToString(), "件")})
        End If

        Return ds

    End Function

    ''' <summary>
    ''' LMH800DSのヘッダ部設定
    ''' </summary>
    ''' <param name="lmn010dr"></param>
    ''' <param name="lmh800Ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSettingHedData(ByVal lmn010dr As DataRow, ByVal lmh800Ds As LMH800DS) As LMH800DS

        Dim lmh800dr As LMH800DS.N_OUTKASCM_HED_BPRow = lmh800Ds.N_OUTKASCM_HED_BP.NewN_OUTKASCM_HED_BPRow
        'Key
        lmh800dr.CRT_DATE = lmn010dr.Item("CRT_DATE").ToString()
        lmh800dr.FILE_NAME = lmn010dr.Item("FILE_NAME").ToString()
        lmh800dr.REC_NO = lmn010dr.Item("REC_NO").ToString()

        lmh800dr.DATA_KB = lmn010dr.Item("DATA_KB").ToString()
        lmh800dr.KITAKU_CD = lmn010dr.Item("KITAKU_CD").ToString()
        lmh800dr.OUTKA_SOKO_CD = lmn010dr.Item("OUTKA_SOKO_CD").ToString()
        lmh800dr.ORDER_TYPE = lmn010dr.Item("ORDER_TYPE").ToString()
        lmh800dr.OUTKA_PLAN_DATE = lmn010dr.Item("OUTKA_PLAN_DATE").ToString()
        lmh800dr.CUST_ORD_NO = lmn010dr.Item("CUST_ORD_NO").ToString()
        lmh800dr.DEST_CD = lmn010dr.Item("DEST_CD").ToString()
        lmh800dr.DEST_JIS_CD = lmn010dr.Item("DEST_JIS_CD").ToString()
        lmh800dr.DEST_NM1 = lmn010dr.Item("DEST_NM1").ToString()
        lmh800dr.DEST_NM2 = lmn010dr.Item("DEST_NM2").ToString()
        lmh800dr.DEST_AD1 = lmn010dr.Item("DEST_AD1").ToString()
        lmh800dr.DEST_AD2 = lmn010dr.Item("DEST_AD2").ToString()
        lmh800dr.DEST_TEL = lmn010dr.Item("DEST_TEL").ToString()
        lmh800dr.DEST_ZIP = lmn010dr.Item("DEST_ZIP").ToString()
        lmh800dr.ARR_PLAN_DATE = lmn010dr.Item("ARR_PLAN_DATE").ToString().Substring(2)
        lmh800dr.ARR_PLAN_TIME = lmn010dr.Item("ARR_PLAN_TIME").ToString()
        lmh800dr.HT_DATE = lmn010dr.Item("HT_DATE").ToString()
        lmh800dr.HT_TIME = lmn010dr.Item("HT_TIME").ToString()
        lmh800dr.HT_CAR_NO = lmn010dr.Item("HT_CAR_NO").ToString()
        lmh800dr.HT_DRIVER = lmn010dr.Item("HT_DRIVER").ToString()
        lmh800dr.HT_UNSO_CO = lmn010dr.Item("HT_UNSO_CO").ToString()
        lmh800dr.MOSIOKURI_KB = lmn010dr.Item("MOSIOKURI_KB").ToString()
        lmh800dr.BUMON_CD = lmn010dr.Item("BUMON_CD").ToString()
        lmh800dr.JIGYOBU_CD = lmn010dr.Item("JIGYOBU_CD").ToString()
        lmh800dr.TOKUI_CD = lmn010dr.Item("TOKUI_CD").ToString()
        lmh800dr.TOKUI_NM = lmn010dr.Item("TOKUI_NM").ToString()
        lmh800dr.BUYER_ORD_NO = lmn010dr.Item("BUYER_ORD_NO").ToString()
        lmh800dr.HACHU_NO = lmn010dr.Item("HACHU_NO").ToString()
        lmh800dr.DENPYO_NO = lmn010dr.Item("DENPYO_NO").ToString()
        lmh800dr.TENPO_CD = lmn010dr.Item("TENPO_CD").ToString()
        lmh800dr.CHOKUSO_KB = lmn010dr.Item("CHOKUSO_KB").ToString()
        lmh800dr.SEIKYU_KB = lmn010dr.Item("SEIKYU_KB").ToString()
        lmh800dr.HACHU_DATE = lmn010dr.Item("HACHU_DATE").ToString()
        lmh800dr.CHUMON_NM = lmn010dr.Item("CHUMON_NM").ToString()
        lmh800dr.HOL_KB = lmn010dr.Item("HOL_KB").ToString()
        lmh800dr.BIKO_HED1 = lmn010dr.Item("BIKO_HED1").ToString()
        lmh800dr.BIKO_HED2 = lmn010dr.Item("BIKO_HED2").ToString()
        lmh800dr.HAKO_NM = lmn010dr.Item("HAKO_NM").ToString()
        lmh800dr.SIIRESAKI_CD = lmn010dr.Item("SIIRESAKI_CD").ToString()
        lmh800dr.KR_TOKUI_CD = lmn010dr.Item("KR_TOKUI_CD").ToString()
        lmh800dr.KEIHI_KB = lmn010dr.Item("KEIHI_KB").ToString()
        lmh800dr.JUCHU_KB = lmn010dr.Item("JUCHU_KB").ToString()
        lmh800dr.DEST_NM = lmn010dr.Item("DEST_NM").ToString()
        lmh800dr.KIGYO_NM = lmn010dr.Item("KIGYO_NM").ToString()
        lmh800dr.FILLER_1 = lmn010dr.Item("FILLER_1").ToString()
        lmh800dr.SCM_CTL_NO_L = lmn010dr.Item("SCM_CTL_NO_L").ToString()

        lmh800Ds.N_OUTKASCM_HED_BP.AddN_OUTKASCM_HED_BPRow(lmh800dr)

        Return lmh800Ds
    End Function


    ''' <summary>
    ''' LMH800DSの明細部設定
    ''' </summary>
    ''' <param name="lmn010dr"></param>
    ''' <param name="lmh800Ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSettingDtlData(ByVal lmn010dr As DataRow, ByVal lmh800Ds As LMH800DS) As LMH800DS
        Dim lmh800dr As LMH800DS.N_OUTKASCM_DTL_BPRow = lmh800Ds.N_OUTKASCM_DTL_BP.NewN_OUTKASCM_DTL_BPRow
        'Key
        lmh800dr.CRT_DATE = lmn010dr.Item("CRT_DATE").ToString()
        lmh800dr.FILE_NAME = lmn010dr.Item("FILE_NAME").ToString()
        lmh800dr.REC_NO = lmn010dr.Item("REC_NO").ToString()
        lmh800dr.GYO = lmn010dr.Item("GYO").ToString()

        lmh800dr.DATA_KB = lmn010dr.Item("DATA_KB").ToString()
        lmh800dr.KITAKU_CD = lmn010dr.Item("KITAKU_CD").ToString()
        lmh800dr.OUTKA_SOKO_CD = lmn010dr.Item("OUTKA_SOKO_CD").ToString()
        lmh800dr.ORDER_TYPE = lmn010dr.Item("ORDER_TYPE").ToString()
        lmh800dr.OUTKA_PLAN_DATE = lmn010dr.Item("OUTKA_PLAN_DATE").ToString()
        lmh800dr.CUST_ORD_NO = lmn010dr.Item("CUST_ORD_NO").ToString()
        lmh800dr.ROW_NO = lmn010dr.Item("ROW_NO").ToString()
        lmh800dr.ROW_TYPE = lmn010dr.Item("ROW_TYPE").ToString()
        lmh800dr.GOODS_CD = lmn010dr.Item("GOODS_CD").ToString()
        lmh800dr.GOODS_NM = lmn010dr.Item("GOODS_NM").ToString()
        lmh800dr.PKG_NB = lmn010dr.Item("PKG_NB").ToString()
        lmh800dr.LOT_NO = lmn010dr.Item("LOT_NO").ToString()
        lmh800dr.OUTKA_PKG_NB = lmn010dr.Item("OUTKA_PKG_NB").ToString()
        lmh800dr.OUTKA_NB = lmn010dr.Item("OUTKA_NB").ToString()
        lmh800dr.TOTAL_WT = lmn010dr.Item("TOTAL_WT").ToString()
        lmh800dr.TOTAL_QT = lmn010dr.Item("TOTAL_QT").ToString()
        lmh800dr.LOT_FLAG = lmn010dr.Item("LOT_FLAG").ToString()
        lmh800dr.BIKO_HED1 = lmn010dr.Item("BIKO_HED1").ToString()
        lmh800dr.BIKO_HED2 = lmn010dr.Item("BIKO_HED2").ToString()
        lmh800dr.BIKO_DTL = lmn010dr.Item("BIKO_DTL").ToString()
        lmh800dr.BUYER_GOODS_CD = lmn010dr.Item("BUYER_GOODS_CD").ToString()
        lmh800dr.TENPO_TANKA = lmn010dr.Item("TENPO_TANKA").ToString()
        lmh800dr.TENPO_KINGAKU = lmn010dr.Item("TENPO_KINGAKU").ToString()
        lmh800dr.JAN_CD = lmn010dr.Item("JAN_CD").ToString()
        lmh800dr.TENPO_BAIKA = lmn010dr.Item("TENPO_BAIKA").ToString()
        lmh800dr.FILLER_1 = lmn010dr.Item("FILLER_1").ToString()
        lmh800dr.SCM_CTL_NO_L = lmn010dr.Item("SCM_CTL_NO_L").ToString()
        lmh800dr.SCM_CTL_NO_M = lmn010dr.Item("SCM_CTL_NO_M").ToString()

        lmh800Ds.N_OUTKASCM_DTL_BP.AddN_OUTKASCM_DTL_BPRow(lmh800dr)

        Return lmh800Ds

    End Function
#End Region

#Region "実績取込"

    ''' <summary>
    ''' 実績取込データ取得
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiTorikomi(ByVal ds As DataSet) As DataSet

        '実績取込データ取得(N_SEND_BP)
        ds = MyBase.CallBLC(Me._Blc, "JissekiTorikomiSelectN_SEND_BP", ds)

        'メッセージの判定(実績取込データが0件の場合処理終了)
        If ds.Tables("N_SEND_BP").Rows.Count = 0 Then
            MyBase.SetMessage("G001")
            Return ds
        End If

        '取込データに紐づくSCM管理番号を取得(N_OUTKASCM_DTL_BPより)
        ds = MyBase.CallBLC(Me._Blc, "JissekiTorikomiSelectN_OUTKASCM_DTL_BP", ds)

        '実績取込データ(N_SEND_BP)にSCM管理番号を設定
        '実績取込データ
        Dim sendTbl As DataTable = ds.Tables("N_SEND_BP")
        'SCM管理番号
        Dim scmNoTbl As DataTable = ds.Tables("N_OUTKASCM_DTL_BP")

        '実績取込データ一行ずつSCM管理番号を取得
        Dim sendNum As Integer = sendTbl.Rows.Count - 1
        For i As Integer = 0 To sendNum

            '抽出条件フィルタ設定
            Dim filter As String = String.Empty
            With sendTbl.Rows(i)
                filter = String.Concat(" CRT_DATE = '", .Item("CRT_DATE"), "' AND FILE_NAME = '", .Item("FILE_NAME"), "' AND REC_NO = '", .Item("REC_NO"), "' AND GYO = '", .Item("GYO"), "'")
            End With
            'SCM管理番号を取得し実績取込データ(N_SEND_BP)に設定
            Dim scmNo As DataRow() = scmNoTbl.Select(filter)
            sendTbl.Rows(i).Item("SCM_CTL_NO_L") = scmNo(0).Item("SCM_CTL_NO_L").ToString()
            sendTbl.Rows(i).Item("SCM_CTL_NO_M") = scmNo(0).Item("SCM_CTL_NO_M").ToString()

        Next

        '存在チェック(N_SEND_BP)
        ds = MyBase.CallBLC(Me._Blc, "CheckExistN_SEND_BP", ds)

        'メッセージの判定
        If MyBase.IsErrorMessageExist() Then
            MyBase.SetMessage("E010")
            '処理終了
            Return ds

        End If

        'トランザクション開始
        Using scope As TransactionScope = MyBase.BeginTransaction()

            '実績取込データ取込(N_SEND_BP)
            ds = MyBase.CallBLC(Me._Blc, "JissekiTorikomiInsertN_SEND_BP", ds)

            '実績取込データ更新処理を行う
            Call Me.UpdateJissekiTorikomiData(ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'トランザクション終了
            MyBase.CommitTransaction(scope)
        End Using

        Return ds

    End Function

    ''' <summary>
    ''' 更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub UpdateJissekiTorikomiData(ByVal ds As DataSet)

        'データの更新(N_OUTKASCM_DTL_BP)
        ds = MyBase.CallBLC(Me._Blc, "JissekiTorikomiUpdateN_OUTKASCM_DTL_BP", ds)

        'データの更新(N_OUTKASCM_M)
        ds = MyBase.CallBLC(Me._Blc, "JissekiTorikomiUpdateN_OUTKASCM_M", ds)

    End Sub

#End Region

#Region "実績送信"

#Region "出荷、納入日チェック"

    ''' <summary>
    ''' 出荷、納入日チェック
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CheckJissekiSoushin(ByVal ds As DataSet) As DataSet

        '実績送信データ取得(N_SEND_BP)
        ds = MyBase.CallBLC(Me._Blc, "JissekiSoushinSelectN_SEND_BP", ds)

        'メッセージの判定(実績送信データが0件の場合処理終了)
        If ds.Tables("N_SEND_BP").Rows.Count = 0 Then
            MyBase.SetMessage("G001")
            Return ds
        End If

        'データ格納用データセット
        Dim inDs As DataSet = New LMN010DS()

        'リストの営業所毎に処理
        Dim brName As String = "BR_CD_LIST"
        Dim dt As DataTable = ds.Tables(brName)
        Dim brNum As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To brNum

            '格納用データセットの初期化
            inDs.Clear()

            '営業所コードを一行設定
            inDs.Tables(brName).ImportRow(dt.Rows(i))

            '営業所毎実績送信データ作成
            Dim sendName As String = "N_SEND_BP"
            Dim sendDt As DataTable = ds.Tables(sendName)
            Dim inDt As DataTable = inDs.Tables(sendName)
            'フィルタ設定
            Dim filter As String = String.Empty
            filter = String.Concat("NRS_BR_CD = '", dt.Rows(i).Item("BR_CD").ToString(), "'")
            'データ抽出
            Dim sendDr As DataRow() = sendDt.Select(filter)

            '抽出データが１件以上存在する場合処理をする
            If sendDr.Length > 0 Then

                '抽出したデータを設定
                Dim sendDrNum As Integer = sendDr.Length - 1
                For j As Integer = 0 To sendDrNum
                    inDt.ImportRow(sendDr(j))
                Next

                'SCM側出荷日、納入日を取得
                inDs = MyBase.CallBLC(Me._Blc, "JissekiSoushinCheckSCMDate", inDs)

                'LMS側出荷日、納入日を取得
                inDs = MyBase.CallBLC(Me._Blc, "JissekiSoushinCheckLMSDate", inDs)

                'SCM日付とLMS日付の比較
                '日付データテーブル名
                Dim scmName As String = "SCM_DATE"
                Dim lmsName As String = "LMS_DATE"
                Dim scmDateTbl As DataTable = inDs.Tables(scmName)
                Dim lmsDateTbl As DataTable = inDs.Tables(lmsName)
                Dim scmNum As Integer = scmDateTbl.Rows.Count
                '一行ずつ比較（SCM側日付を一行ずつ）
                For k As Integer = 0 To scmNum - 1
                    '比較用キー項目取得
                    Dim crtDate As String = scmDateTbl.Rows(k).Item("CRT_DATE").ToString()
                    Dim fileName As String = scmDateTbl.Rows(k).Item("FILE_NAME").ToString()
                    Dim recNo As String = scmDateTbl.Rows(k).Item("REC_NO").ToString()
                    Dim gyo As String = scmDateTbl.Rows(k).Item("GYO").ToString()
                    '比較対象条件設定
                    Dim filterDate As String = String.Empty
                    filterDate = String.Concat("CRT_DATE = '", crtDate, "' AND FILE_NAME = '", fileName, "' AND REC_NO = '", recNo, "' AND GYO = '", gyo, "'")
                    '比較対象LMS側日付取得
                    Dim lmsDataDr As DataRow() = lmsDateTbl.Select(filterDate)
                    '出荷日、納入日の取得
                    Dim scmOutkaDate As String = scmDateTbl.Rows(k).Item("SCM_OUTKA_DATE").ToString()
                    Dim scmArrDate As String = scmDateTbl.Rows(k).Item("SCM_ARR_DATE").ToString()
                    Dim lmsOutkaDate As String = lmsDataDr(0).Item("LMS_OUTKA_DATE").ToString()
                    Dim lmsArrDate As String = lmsDataDr(0).Item("LMS_ARR_DATE").ToString()

                    '出荷日、納入日を比較
                    If (scmOutkaDate <> lmsOutkaDate) Or (scmArrDate <> lmsArrDate) Then
                        '出荷日または納入日が一致しない場合
                        MyBase.SetMessage("W107")
                        Exit For
                    End If

                Next

            End If

        Next

        Return ds

    End Function

#End Region

#Region "実績送信処理"

    ''' <summary>
    ''' 実績送信
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function JissekiSoushin(ByVal ds As DataSet) As DataSet

        Dim tableNm As String = "N_SEND_BP"
        Dim insDt As DataTable = ds.Tables(tableNm)
        Dim lmn800Ds As LMN800DS = Nothing

        '実績送信データ取得(N_SEND_BP)
        ds = MyBase.CallBLC(Me._Blc, "JissekiSoushinSelectN_SEND_BP", ds)

        If insDt.Rows.Count <> 0 Then
            lmn800Ds = New LMN800DS
            Dim lmn800row As LMN800DS.LMN800CUST_INFORow = lmn800Ds.LMN800CUST_INFO.NewLMN800CUST_INFORow
            lmn800row.SCM_CUST_CD = insDt.Rows(0)("SCM_CUST_CD").ToString
            lmn800Ds.LMN800CUST_INFO.AddLMN800CUST_INFORow(lmn800row)

            lmn800Ds = Me.CreateSettingSendData(ds, lmn800Ds)

            'TODO(後でコメントはずす)
            ''トランザクション開始
            'Using scope As TransactionScope = MyBase.BeginTransaction()

            'LMN800の呼び出し
            lmn800Ds = DirectCast(MyBase.CallBLC(New LMN800BLC, "ExecuteSoushinShiji", lmn800Ds), LMN800DS)
            If "00".Equals(lmn800Ds.RESULT(0).RESULT) = False Then
                Me.SetMessage(lmn800Ds.RESULT(0).ERROR_CD, New String() {"実績報告データ送信"})
                Return ds
            End If

            '実績取込データ更新処理を行う
            Call Me.UpdateJissekiSoushinData(ds)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            'TODO(後でコメントはずす)
            ''トランザクション終了
            'MyBase.CommitTransaction(scope)
            'End Using

        End If

        Return ds

    End Function

    ''' <summary>
    ''' LMH800DSのヘッダ部設定
    ''' </summary>
    ''' <param name="lmn010ds"></param>
    ''' <param name="lmn800Ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function CreateSettingSendData(ByVal lmn010ds As DataSet, ByVal lmn800Ds As LMN800DS) As LMN800DS

        Dim dataTable As DataTable = lmn010ds.Tables("N_SEND_BP")

        '設定
        For Each dataRow As DataRow In dataTable.Rows

            Dim lmh800dr As LMN800DS.N_SEND_BPRow = lmn800Ds.N_SEND_BP.NewN_SEND_BPRow
            'Key
            lmh800dr.KITAKU_CD = dataRow.Item("KITAKU_CD").ToString()
            lmh800dr.SOKO_CD = dataRow.Item("SOKO_CD").ToString()
            lmh800dr.ORDER_TYPE = dataRow.Item("ORDER_TYPE").ToString()
            lmh800dr.INOUT_DATE = dataRow.Item("INOUT_DATE").ToString()
            lmh800dr.FILLER_1 = dataRow.Item("FILLER_1").ToString()
            lmh800dr.CUST_ORD_NO = dataRow.Item("CUST_ORD_NO").ToString()
            lmh800dr.DEST_CD = dataRow.Item("DEST_CD").ToString()
            lmh800dr.MEI_NO = dataRow.Item("MEI_NO").ToString()
            lmh800dr.GOODS_CD = dataRow.Item("GOODS_CD").ToString()
            lmh800dr.SEIZO_DATE = dataRow.Item("SEIZO_DATE").ToString()
            lmh800dr.LOT_NO = dataRow.Item("LOT_NO").ToString()
            lmh800dr.SOKO_NO = dataRow.Item("SOKO_NO").ToString()
            lmh800dr.LOCA = dataRow.Item("LOCA").ToString()
            lmh800dr.INOUT_PKG_NB = dataRow.Item("INOUT_PKG_NB").ToString()
            lmh800dr.BUMON_CD = dataRow.Item("BUMON_CD").ToString()
            lmh800dr.JIGYOBU_CD = dataRow.Item("JIGYOBU_CD").ToString()
            lmh800dr.TOKUI_CD = dataRow.Item("TOKUI_CD").ToString()
            lmh800dr.OKURIJO_NO = dataRow.Item("OKURIJO_NO").ToString()
            lmh800dr.FILLER_2 = dataRow.Item("FILLER_2").ToString()

            lmn800Ds.N_SEND_BP.AddN_SEND_BPRow(lmh800dr)

        Next

        Return lmn800Ds

    End Function


    ''' <summary>
    ''' 実績送信更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <remarks></remarks>
    Private Sub UpdateJissekiSoushinData(ByVal ds As DataSet)

        'データの更新(N_SEND_BP)
        ds = MyBase.CallBLC(Me._Blc, "JissekiSoushinUpdateN_SEND_BP", ds)

        'データの更新(N_OUTKASCM_DTL_BP)
        ds = MyBase.CallBLC(Me._Blc, "JissekiSoushinUpdateN_OUTKASCM_DTL_BP", ds)

        'データの更新(N_OUTKASCM_M)
        ds = MyBase.CallBLC(Me._Blc, "JissekiSoushinUpdateN_OUTKASCM_M", ds)

        'データの更新(N_OUTKASCM_L)
        ds = MyBase.CallBLC(Me._Blc, "JissekiSoushinUpdateN_OUTKASCM_L", ds)

        'データ格納用データセット
        Dim inDs As DataSet = New LMN010DS()

        'リストの営業所毎に処理
        Dim brName As String = "BR_CD_LIST"
        Dim sendName As String = "N_SEND_BP"
        Dim dt As DataTable = ds.Tables(brName)
        Dim brNum As Integer = dt.Rows.Count - 1
        For i As Integer = 0 To brNum

            '格納用データセットの初期化
            inDs.Clear()

            'フィルタ用営業所コード取得
            Dim BrCd As String = dt.Rows(i).Item("BR_CD").ToString()

            '営業所毎EDI送信データ
            Dim sendRow As DataRow() = ds.Tables(sendName).Select(String.Concat("NRS_BR_CD = ", BrCd))

            If sendRow.Length > 0 Then
                Dim maxSend As Integer = sendRow.Length - 1
                For j As Integer = 0 To maxSend
                    inDs.Tables(sendName).ImportRow(sendRow(j))
                Next

                '営業所コードを一行設定
                inDs.Tables(brName).ImportRow(dt.Rows(i))

                'リストの営業所毎にデータの更新(H_OUTKAEDI_DTL_BP)
                inDs = MyBase.CallBLC(Me._Blc, "JissekiSoushinUpdateH_OUTKAEDI_DTL_BP", inDs)

                'リストの営業所毎にデータの更新(H_SENDEDI_BP)
                inDs = MyBase.CallBLC(Me._Blc, "JissekiSoushinUpdateH_SENDEDI_BP", inDs)

            End If

        Next

    End Sub

#End Region

#End Region

#Region "欠品状態更新"

    ''' <summary>
    ''' 欠品状態更新処理
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function UpdKeppinJoutai(ByVal ds As DataSet) As DataSet

        Dim inTableName As String = "LMN010IN"
        Dim dt As DataTable = ds.Tables(inTableName)
        Dim dtNum As Integer = dt.Rows.Count
        'IN情報のレコード1行毎に欠品フラグを取得
        For i As Integer = 0 To dtNum - 1

            '欠品判定用フラグ(TRUE:欠品有り FALSE:欠品無し (初期値：FALSE))
            Dim KeppinFlg As Boolean = False

            'IN情報取得
            Dim inDs As DataSet = New LMN010DS
            Dim inDt As DataTable = inDs.Tables(inTableName)
            inDt.ImportRow(dt.Rows(i))

            '明細データより荷主商品コードを取得
            inDs = MyBase.CallBLC(Me._Blc, "UpdKeppinJoutaiSelectN_OUTKASCM_M", inDs)

            '明細データ件数取得
            Dim meiTableName As String = "N_OUTKASCM_M"
            Dim meiNum As Integer = inDs.Tables(meiTableName).Rows.Count
            '明細データ一件毎に欠品数を取得
            For j As Integer = 0 To meiNum - 1

                '欠品チェックデータセット作成
                Dim lmn810inName As String = "LMN810IN"
                Dim lmn810ds As DataSet = New LMN810DS

                '営業所接続情報設定
                Dim brName As String = "BR_CD_LIST"
                'フィルタ用営業所コード取得
                Dim BrCd As String = dt.Rows(i).Item("BR_CD").ToString()
                '営業所接続情報抽出
                Dim brDr As DataRow() = ds.Tables(brName).Select(String.Concat("BR_CD = '", BrCd, "'"))
                lmn810ds.Tables(brName).ImportRow(brDr(0))

                '欠品チェックパラメータ設定
                Dim lmn810dr As DataRow = lmn810ds.Tables(lmn810inName).NewRow()
                lmn810dr.Item("SCM_CUST_CD") = dt.Rows(i).Item("SCM_CUST_CD").ToString()
                lmn810dr.Item("LMS_CUST_CD") = brDr(0).Item("LMS_CUST_CD").ToString()
                lmn810dr.Item("BR_CD") = dt.Rows(i).Item("BR_CD").ToString()
                lmn810dr.Item("SOKO_CD") = dt.Rows(i).Item("SOKO_CD").ToString()
                lmn810dr.Item("GOODS_CD_CUST") = inDs.Tables(meiTableName).Rows(j).Item("CUST_GOODS_CD").ToString()
                lmn810dr.Item("OUTKA_DATE") = dt.Rows(i).Item("OUTKA_DATE").ToString()
                lmn810ds.Tables(lmn810inName).Rows.Add(lmn810dr)

                'LMN810呼び出し
                lmn810ds = MyBase.CallBLC(New LMN810BLC, "ChkKeppin", lmn810ds)

                '欠品チェック
                Dim outName As String = "LMN810OUT"
                Dim lmn810out As DataTable = lmn810ds.Tables(outName)
                '欠品の場合、欠品フラグを設定
                If Convert.ToInt32(lmn810out.Rows(0).Item("KEPPIN_NB")) > 0 Then
                    KeppinFlg = True
                    Exit For
                End If

            Next

            'リターンデータセットに取得した値を設定
            Dim rtnName As String = "LMN010OUT"
            Dim dr As DataRow = ds.Tables(rtnName).NewRow()

            dr.Item("SCM_CTL_NO_L") = ds.Tables(inTableName).Rows(i).Item("SCM_CTL_NO_L").ToString()
            If KeppinFlg Then
                dr.Item("KEPPIN_FLG") = "欠"
            Else
                dr.Item("KEPPIN_FLG") = "-"
            End If

            ds.Tables(rtnName).Rows.Add(dr)

        Next

        Return ds

    End Function

#End Region

#Region "Spreadダブルクリック"

    Private Function CheckNotExistData(ByVal ds As DataSet) As DataSet

        '新規取込フラグ判定
        If ds.Tables("LMN010IN").Rows(0).Item("INSERT_FLG").ToString() = "0" Then
            '新規取込フラグが0の場合はN_OUTKASCM_Lに対し存在チェック
            '存在チェック(N_OUTKASCM_L)
            ds = MyBase.CallBLC(Me._Blc, "CheckNotExistN_OUTKASCM_L", ds)
        Else
            '新規取込フラグが1の場合はN_OTUKASCM_HED_BOに対し存在チェック
            '存在チェック(N_OUTKASCM_HED_BP)
            ds = MyBase.CallBLC(Me._Blc, "CheckNotExistN_OUTKASCM_HED_BP", ds)
        End If

        Return ds

    End Function

#End Region

#End Region

End Class
