' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC500    : 納品書印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const
'2015.09.11 エクシングQR対応追加START
Imports System.Security.Cryptography
Imports System.IO
Imports System.Text
'2015.09.11 エクシングQR対応追加END

''' <summary>
''' LMC500BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC500BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC500DAC = New LMC500DAC()
    Private _501Dac As LMC501DAC = New LMC501DAC()
    Private _502Dac As LMC502DAC = New LMC502DAC()
    Private _787Dac As LMC787DAC = New LMC787DAC()
    Private _880Dac As LMC880DAC = New LMC880DAC()
    Private _891Dac As LMC891DAC = New LMC891DAC()

#End Region

#Region "Method"

#Region "印刷処理"
    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        'START YANAI 要望番号1478 一括印刷が遅い
        Dim beforeSecond As String = String.Empty
        Dim nowSecond As String = String.Empty
        'END YANAI 要望番号1478 一括印刷が遅い

        '元のデータ
        Dim tableNmIn As String = "LMC500IN"
        Dim tableNmOut As String = "LMC500OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        'Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        '部数の設定がない場合は初期値0を設定
        Dim prtNb As Integer = 0
        If String.IsNullOrEmpty(dt.Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(dt.Rows(0).Item("PRT_NB"))
        End If

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)
            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '(2012.03.03) 専用OUTを使用した場合は入れ替えを行う LMC513対応 -- START --
            If setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC513") = True Then
                tableNmOut = "LMC513OUT"
            End If
            '(2012.11.30) UTI LMC630用 --- START ---
            If setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC630") = True Then
                tableNmOut = "LMC630OUT"
            End If
            '(2012.11.30) UTI LMC630用 ---  END  ---
#If True Then   'ADD 2020/05/01 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            If setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC545") = True Then
                tableNmOut = "LMC545OUT"
            End If

#End If

            If setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC891") = True OrElse
               setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC892") = True Then
                tableNmOut = "LMC891OUT"
            End If

            If setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC899") = True Then
                tableNmOut = "LMC899OUT"
            End If

            If setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString().Equals("LMC900") = True Then
                tableNmOut = "LMC900OUT"
            End If


            Dim dtOut As DataTable = ds.Tables(tableNmOut)

            '(2012.03.03) 専用OUTを使用した場合は入れ替えを行う LMC513対応 --  END  --

            '2015.09.11 エクシングQR対応追加START
            'データセットの編集(出力用テーブルに抽出データを設定)
            If tableNmOut.Equals("LMC500OUT") Then
                setDs = Me.EditQrDataSet(setDs)
            End If
            '2015.09.11 エクシングQR対応追加END

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
            'OUT
            For j As Integer = 0 To setDtOut.Rows.Count - 1
                dtOut.ImportRow(setDtOut.Rows(j))
            Next

            'RPT(重複分を含めワーク用RPTテーブルに追加)
            For k As Integer = 0 To setDtRpt.Rows.Count - 1
                workDtRpt.ImportRow(setDtRpt.Rows(k))
            Next

        Next

        'ワーク用RPTワークテーブルの重複を除外する
        Dim view As DataView = New DataView(workDtRpt)
        workDtRpt = view.ToTable(True, "NRS_BR_CD", "PTN_ID", "PTN_CD", "RPT_ID")

        'ソート実行
        Dim rptDr As DataRow() = workDtRpt.Select(Nothing, "NRS_BR_CD ASC, PTN_ID ASC, RPT_ID ASC, PTN_CD ASC")

        'ソート実行後データ格納データセット作成
        Dim sortDtRpt As DataTable = dtRpt.Clone

        'ソート済みデータ格納
        For Each row As DataRow In rptDr
            sortDtRpt.ImportRow(row)
        Next

        'キーブレイク用(NRS_BR_CD,PTN_ID,RPT_ID)
        Dim keyBrCd As String = String.Empty
        Dim keyPtnId As String = String.Empty
        Dim keyRptId As String = String.Empty

        '重複分を除外したワーク用RPTテーブルを帳票出力に使用するDSにセットする)
        For l As Integer = 0 To sortDtRpt.Rows.Count - 1
            '営業所コード、パターンID、レポートIDが一致するレコードは除外する
            If keyBrCd <> sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString() OrElse _
               keyPtnId <> sortDtRpt.Rows(l).Item("PTN_ID").ToString() OrElse _
               keyRptId <> sortDtRpt.Rows(l).Item("RPT_ID").ToString() Then

                '営業所コード、パターンID、レポートIDのいずれかが一致しないレコードは格納する
                dtRpt.ImportRow(sortDtRpt.Rows(l))
                'キー更新
                keyBrCd = sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString()
                keyPtnId = sortDtRpt.Rows(l).Item("PTN_ID").ToString()
                keyRptId = sortDtRpt.Rows(l).Item("RPT_ID").ToString()

            End If

        Next

        '(2012.11.30) LMC630対応の一環として追加 ---
        Dim rptId As String = String.Empty
        Dim prtmax As Integer = 0
        Dim prtDsMax As Integer = 0
        Dim tableName As String = "LMC500OUT"
        '(2012.11.30) LMC630対応の一環として追加 ---

        'レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables(tableNmRpt).Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            '(2012.11.30) LMC630対応の一環として追加 ---
            'レポートID取得
            rptId = dr.Item("RPT_ID").ToString()
            Select Case rptId
                Case "LMC513"
                    tableName = "LMC513OUT"
                Case "LMC630"
                    tableName = "LMC630OUT"
#If True Then   'ADD 2020/05/01 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
                Case "LMC545"
                    tableName = "LMC545OUT"
#End If
                Case "LMC891"
                    tableName = "LMC891OUT"

                Case "LMC892"
                    tableName = "LMC891OUT"

                Case "LMC899"
                    tableName = "LMC899OUT"

                Case "LMC900"
                    tableName = "LMC900OUT"

            End Select
            '(2012.11.30) LMC630対応の一環として追加 ---

            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            Select Case rptId
                Case "LMC899"
                    prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs, tableNmOut, ds.Tables("LMC500IN"))

                Case Else
                    prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs, tableNmOut)

            End Select

            'START YANAI 要望番号1478 一括印刷が遅い
            ''TODO 開発元の回答により対応
            ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
            ''1秒（1000ミリ秒）待機する
            'System.Threading.Thread.Sleep(1000)
            ''★★★ END
            'nowSecond = Mid(MyBase.GetSystemTime, 5, 2) '現在の時間の秒を設定

            'If (nowSecond).Equals(beforeSecond) = True Then
            '    '秒が同じ値の場合
            '    '1000ミリ秒 - 現在のミリ秒、待機する
            '    System.Threading.Thread.Sleep(1000 - Convert.ToInt32(Mid(MyBase.GetSystemTime, 7, 3)))
            'Else
            '    '1秒（1000ミリ秒）待機する
            '    System.Threading.Thread.Sleep(1000)
            'End If
            'beforeSecond = nowSecond
            'END YANAI 要望番号1478 一括印刷が遅い


            '(2012.11.30) LMC630対応の一環として追加＆コメント ---
            '行数取得
            prtDsMax = prtDs.Tables(tableName).Rows.Count - 1

            'LMC630 納品書(ﾕｰﾃｨｱｲ用)はCOUNT変更
            If (rptId).Equals("LMC630") = True Then
                '(2012.12.12)LMC630の帳票種別が6種類→4種類に変更
                prtmax = 4
            End If

            If (rptId).Equals("LMC891") = True OrElse (rptId).Equals("LMC892") = True Then
                prtmax = 1
            End If

            For RPT_TYPE As Integer = 0 To prtmax
                'RPT_FLG設定
                If (rptId).Equals("LMC630") = True Then
                    For DS_CNT As Integer = 0 To prtDsMax
                        prtDs.Tables(tableName).Rows(DS_CNT).Item("RPT_FLG") = RPT_TYPE.ToString
                    Next
                End If

                If (rptId).Equals("LMC891") = True OrElse (rptId).Equals("LMC892") = True Then
                    For DS_CNT As Integer = 0 To prtDsMax
                        prtDs.Tables(tableName).Rows(DS_CNT).Item("RPT_FLG") = RPT_TYPE.ToString
                    Next
                End If

                '帳票CSV出力
                Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)
            Next

            ''帳票CSV出力
            'comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
            '                      dr.Item("PTN_ID").ToString(), _
            '                      dr.Item("PTN_CD").ToString(), _
            '                      dr.Item("RPT_ID").ToString(), _
            '                      prtDs.Tables(tableNmOut), _
            '                      ds.Tables(LMConst.RD), _
            '                      String.Empty, _
            '                      String.Empty, _
            '                      prtNb)

            '(2012.11.30) LMC630対応の一環として追加＆コメント ---

            '後処理（特殊な更新処理等）
            If (rptId).Equals("LMC900") = True Then
                '物産アニマルヘルス（納品案内書）
                '出荷実績作成にて出力内容が必要となるので出荷データSに書き込んでおく
                prtDs = MyBase.CallDAC(Me._502Dac, "UpdateOutkaS_Clear_LMC900", prtDs)
                prtDs = MyBase.CallDAC(Me._502Dac, "UpdateOutkaS_LMC900", prtDs)
            End If

        Next

        Return ds

    End Function

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        'CSV出力
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      dr.Item("RPT_ID").ToString(), _
                      prtDs.Tables(tableName), _
                      ds.Tables(LMConst.RD), _
                      String.Empty, _
                      String.Empty, _
                      prtNb)

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <param name="DataSetNM"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet, ByVal DataSetNM As String, Optional ByVal dtIn As DataTable = Nothing) As DataSet
        'Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        '横浜荷主インターコンチネンタル（00125）の場合は置場順にソートする
        '営業所コード取得
        Dim brCd As String = ds.Tables(DataSetNM).Rows(0).Item("NRS_BR_CD").ToString()
        'Dim brCd As String = ds.Tables("LMC500OUT").Rows(0).Item("NRS_BR_CD").ToString()

        '荷主コード取得
        Dim custCd As String = ds.Tables(DataSetNM).Rows(0).Item("CUST_CD_L").ToString()
        'Dim custCd As String = ds.Tables("LMC500OUT").Rows(0).Item("CUST_CD_L").ToString()

        If brCd = "40" AndAlso custCd = "00125" Then

            'ソート対象データテーブル取得
            Dim outDt As DataTable = ds.Tables(DataSetNM)
            'Dim outDt As DataTable = ds.Tables("LMC500OUT")
            'ソート条件設定
            Dim sort As String = "TOU_NO ASC, SITU_NO ASC, ZONE_CD ASC, LOCA ASC"
            'ソート実行
            Dim dr As DataRow() = outDt.Select(Nothing, sort)
            'ソート実行後データ格納データセット作成
            Dim rtnDs As DataSet = ds.Clone
            'ソート済みデータ格納
            For Each row As DataRow In dr
                rtnDs.Tables(DataSetNM).ImportRow(row)
                'rtnDs.Tables("LMC500OUT").ImportRow(row)
            Next

            Return rtnDs

        End If

        Select Case rptId

            'LMC501の場合は営業所コード、出荷管理番号L、荷主商品コード、ロット、入目の昇順にソートする
            Case "LMC501"
                'ソート対象データテーブル取得
                Dim outDt As DataTable = ds.Tables(DataSetNM)
                'Dim outDt As DataTable = ds.Tables("LMC500OUT")
                'ソート条件設定
                Dim sort As String = "NRS_BR_CD ASC, OUTKA_NO_L ASC, GOODS_CD_CUST ASC, LOT_NO ASC, IRIME ASC"
                'ソート実行
                Dim dr As DataRow() = outDt.Select(Nothing, sort)
                'ソート実行後データ格納データセット作成
                Dim rtnDs As DataSet = ds.Clone
                'ソート済みデータ格納
                For Each row As DataRow In dr
                    rtnDs.Tables(DataSetNM).ImportRow(row)
                    'rtnDs.Tables("LMC500OUT").ImportRow(row)
                Next

                Return rtnDs

                '追記
                'ロット番号へLOT_NOが10バイト以上(LOT_NO>=10)のときに、半角スペースを4-3-3の間隔で入れる(例：1234 567 890)

            Case "LMC503"

                'LOT_NO
                Dim lot As String = String.Empty
                '探したい文字列
                Dim NO As String = "NO."
                '文字列検出用
                Dim numYN As Integer = 0
                '左4桁格納用
                Dim left As String = String.Empty
                '中央3桁格納用
                Dim center As String = String.Empty
                '右3桁格納用
                Dim right As String = String.Empty
                Dim editLotNo As String = String.Empty
                'スペース
                Dim space As String = "  "
                'データテーブル取得
                Dim outDt As DataTable = ds.Tables(DataSetNM)
                'Dim outDt As DataTable = ds.Tables("LMC500OUT")
                Dim max As Integer = outDt.Rows.Count - 1

                '抽出データ明細行の梱数チェック()
                For i As Integer = 0 To max
                    '初期化
                    left = String.Empty
                    center = String.Empty
                    right = String.Empty
                    editLotNo = String.Empty
                    lot = ((outDt.Rows(i).Item("LOT_NO").ToString())).ToUpper
                    numYN = lot.IndexOf(NO) '「NO.」を探す。あればゼロ以上になる。
                    If numYN = -1 Then
                        'ゼロ(NO.なし)かつ10バイト以上なので加工。
                        If lot.Length >= 10 Then
                            left = lot.Substring(0, 4)
                            center = lot.Substring(4, 3)
                            right = Mid(lot, 8)
                            editLotNo = String.Concat(left, space, center, space, right)
                        Else
                            editLotNo = lot
                        End If
                        'データテーブルに戻す。
                        outDt.Rows(i).Item("LOT_NO") = editLotNo
                    Else
                    End If
                Next
                Return ds

                '(2013.10.31)千葉系納品書(ｼﾘｱﾙ№なし:LMC738)追加 -- START --
                '(2012.10.19)要望番号1289 千葉系納品書編集 -- START --
                '(2016.07.12)千葉標準+バーコード(LMC819) 追加
            Case "LMC621", "LMC628", "LMC629", "LMC631", "LMC633", "LMC634", "LMC635", _
                 "LMC636", "LMC637", "LMC638", "LMC639", "LMC732", "LMC733", "LMC734", "LMC736", "LMC738", "LMC819", "LMC885"
                '(2013.10.31)千葉系納品書(ｼﾘｱﾙ№なし:LMC738)追加 -- END --

                '同一条件に該当するデータのまとめ処理
                Dim outDt As DataTable = ds.Tables(DataSetNM)  'データ取得
#If True Then   'ADD 2018/08/22 依頼番号 : 001829   【LMS】納品書_商品2重に印刷される
                '大文字小文字半角全角を区別するようにする
                outDt.CaseSensitive = True

#End If
                Dim cloneDt As DataTable = outDt.Clone
                '
                Dim wkKONSU As Decimal = 0
                Dim wkHASU As Decimal = 0
                Dim wkALCTD_QT As Decimal = 0
                Dim wkZAN_KONSU As Decimal = 0
                Dim wkALCTD_NB As Decimal = 0
                Dim wkZAN_HASU As Decimal = 0
                Dim wkEdit As String = String.Empty
                Dim wkEdit2 As String = String.Empty
                Dim wkKey As New ArrayList

                '検索条件の作成
                For Each row As DataRow In outDt.Rows
#If True Then       'ADD 2018/12/18 依頼番号 : 003845   【LMS】運送・運行画面から印刷をかけると出荷(中)が分かれて印刷

                    '出荷管理番号(中)
                    If IsDBNull(row.Item("OUTKA_NO_L")) = False Then
                        wkEdit += String.Concat("OUTKA_NO_L = '", row.Item("OUTKA_NO_L").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("OUTKA_NO_L IS NULL", " AND ")
                    End If

#End If
                    '--(2012.11.01)要望番号1560 -- START --
                    '出荷管理番号(中)
                    If IsDBNull(row.Item("OUTKA_NO_M")) = False Then
                        wkEdit += String.Concat("OUTKA_NO_M = '", row.Item("OUTKA_NO_M").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("OUTKA_NO_M IS NULL", " AND ")
                    End If
                    '--(2012.11.01)要望番号1560 -- END --

                    '商品コード
                    If IsDBNull(row.Item("GOODS_CD_CUST")) = False Then
                        wkEdit += String.Concat("GOODS_CD_CUST = '", row.Item("GOODS_CD_CUST").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("GOODS_CD_CUST IS NULL", " AND ")
                    End If

                    'ロット№
                    If IsDBNull(row.Item("LOT_NO")) = False Then
                        wkEdit += String.Concat("LOT_NO = '", row.Item("LOT_NO").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("LOT_NO IS NULL", " AND ")
                    End If

                    '入目
                    If IsDBNull(row.Item("IRIME")) = False Then
                        wkEdit += String.Concat("IRIME = '", row.Item("IRIME").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat(" IS NULL", " AND ")
                    End If

                    '(2013.02.18)要望番号1865 まとめ条件に棟・室・ゾーンを追加 -- START --
                    '棟
                    If IsDBNull(row.Item("TOU_NO")) = False Then
                        wkEdit += String.Concat("TOU_NO = '", row.Item("TOU_NO").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("TOU_NO IS NULL", " AND ")
                    End If

                    '室
                    If IsDBNull(row.Item("SITU_NO")) = False Then
                        wkEdit += String.Concat("SITU_NO = '", row.Item("SITU_NO").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("SITU_NO IS NULL", " AND ")
                    End If

                    'ZONE
                    If IsDBNull(row.Item("ZONE_CD")) = False Then
                        wkEdit += String.Concat("ZONE_CD = '", row.Item("ZONE_CD").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("ZONE_CD IS NULL", " AND ")
                    End If
                    '(2013.02.18)要望番号1865 まとめ条件に棟・室・ゾーンを追加 --  END  --

                    'ロケーション
                    If IsDBNull(row.Item("LOCA")) = False Then
                        wkEdit += String.Concat("LOCA = '", row.Item("LOCA").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("LOCA IS NULL", " AND ")
                    End If

                    '商品状態区分１
                    If IsDBNull(row.Item("GOODS_COND_CD_1")) = False Then
                        wkEdit += String.Concat("GOODS_COND_CD_1 = '", row.Item("GOODS_COND_CD_1").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("GOODS_COND_CD_1 IS NULL", " AND ")
                    End If

                    '商品状態区分２
                    If IsDBNull(row.Item("GOODS_COND_CD")) = False Then
                        wkEdit += String.Concat("GOODS_COND_CD = '", row.Item("GOODS_COND_CD").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("GOODS_COND_CD IS NULL", " AND ")
                    End If

                    '商品状態区分３
                    If IsDBNull(row.Item("GOODS_COND_CD_3")) = False Then
                        wkEdit += String.Concat("GOODS_COND_CD_3 = '", row.Item("GOODS_COND_CD_3").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("GOODS_COND_CD_3 IS NULL", " AND ")
                    End If

                    'シリアルナンバー
                    If IsDBNull(row.Item("SERIAL_NO")) = False Then
                        wkEdit += String.Concat("SERIAL_NO = '", row.Item("SERIAL_NO").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("SERIAL_NO IS NULL", " AND ")
                    End If

                    '備考（社外）
                    If IsDBNull(row.Item("REMARK_OUT")) = False Then
                        wkEdit += String.Concat("REMARK_OUT = '", row.Item("REMARK_OUT").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("REMARK_OUT IS NULL", " AND ")
                    End If

                    '備考（社内）
                    If IsDBNull(row.Item("REMARK_ZAI")) = False Then
                        wkEdit += String.Concat("REMARK_ZAI = '", row.Item("REMARK_ZAI").ToString, "' AND ")
                    Else
                        wkEdit += String.Concat("REMARK_ZAI IS NULL", " AND ")
                    End If

                    '賞味期限
                    If IsDBNull(row.Item("LT_DATE")) = False Then
                        wkEdit += String.Concat("LT_DATE = '", row.Item("LT_DATE").ToString, "'")
                    Else
                        wkEdit += String.Concat("LT_DATE IS NULL", "")
                    End If

                    '(2016.7.13) LMC629対応 -- START --
                    '製造日 
                    If (rptId.Equals("LMC629") OrElse rptId.Equals("LMC885")) Then

                        ' JT物流(LMC629)固有表示項目(20160713現在)
                        If IsDBNull(row.Item("GOODS_CRT_DATE")) = False Then
                            wkEdit += String.Concat(" AND GOODS_CRT_DATE = '", row.Item("GOODS_CRT_DATE").ToString, "'")
                        Else
                            wkEdit += String.Concat(" AND GOODS_CRT_DATE IS NULL", "")
                        End If

                    End If
                    '(2016.7.13) LMC629対応 -- END --

                    '重複不可
                    If wkKey.Contains(wkEdit) = False Then
                        wkKey.Add(wkEdit)
                    End If

                    wkEdit = String.Empty
                Next

                'まとめ処理
                For i As Integer = 0 To wkKey.Count - 1
                    '検索条件による同一データの抽出
                    Dim wkDataRow() As DataRow = outDt.Select(wkKey(i).ToString)

                    '20121026 修正 --Start--
                    '入荷管理区分の値による制御
                    'If outDt.Rows(i).Item("SET_NAIYO").ToString <> "1" Then

                    '    '①入荷管理区分<>"1"の場合は、まとめ処理を実行。 ----------------------------
                    '    '１件を超える場合にまとめる 
                    '    If wkDataRow.Length > 1 Then
                    '        wkKONSU = 0
                    '        wkALCTD_QT = 0
                    '        wkZAN_KONSU = 0
                    '        wkALCTD_NB = 0

                    '        For Each tmpRow As DataRow In wkDataRow
                    '            wkKONSU += Convert.ToDecimal(tmpRow.Item("KONSU"))
                    '            wkALCTD_QT += Convert.ToDecimal(tmpRow.Item("ALCTD_QT"))
                    '            wkZAN_KONSU += Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                    '            wkALCTD_NB += Convert.ToDecimal(tmpRow.Item("ALCTD_NB"))
                    '        Next
                    '        wkDataRow(0).Item("KONSU") = wkKONSU
                    '        wkDataRow(0).Item("ALCTD_QT") = wkALCTD_QT
                    '        wkDataRow(0).Item("ZAN_KONSU") = wkZAN_KONSU
                    '        wkDataRow(0).Item("ALCTD_NB") = wkALCTD_NB
                    '        wkDataRow(0).Item("ZAN_HASU") = CLng(wkDataRow(0).Item("ALCTD_NB")) Mod CLng(wkDataRow(0).Item("PKG_NB"))
                    '    End If
                    '    cloneDt.ImportRow(wkDataRow(0))

                    'Else
                    '    '②入荷管理区分="1"の場合は、まとめ処理SKIP。件数分設定する。 ---------------
                    '    For Cut As Integer = 0 To wkDataRow.Length - 1
                    '        cloneDt.ImportRow(wkDataRow(Cut))
                    '    Next

                    'End If

                    '(2012.11.08)要望番号1572 千葉系納品書編集 -- START --
                    'If outDt.Rows(i).Item("SET_NAIYO").ToString = "1" Then

                    '    If Mid(outDt.Rows(i).Item("SYS_UPD_PGID").ToString, 1, 4) = "IKOU" Then

                    '        '①入荷管理区分="1"、更新プログラムID="IKOU"の場合は、残個数の最小値を取得 ----------------------------
                    '        If wkDataRow.Length > 1 Then
                    '            wkKONSU = 0
                    '            wkALCTD_QT = 0
                    '            wkZAN_KONSU = 0
                    '            wkALCTD_NB = 0

                    '            For Each tmpRow As DataRow In wkDataRow
                    '                wkKONSU += Convert.ToDecimal(tmpRow.Item("KONSU"))
                    '                wkALCTD_QT += Convert.ToDecimal(tmpRow.Item("ALCTD_QT"))
                    '                If wkZAN_KONSU = 0 Then
                    '                    wkZAN_KONSU = Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                    '                Else
                    '                    If wkZAN_KONSU < Convert.ToDecimal(tmpRow.Item("ZAN_KONSU")) Then
                    '                        wkZAN_KONSU = Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                    '                    End If
                    '                End If
                    '                wkALCTD_NB += Convert.ToDecimal(tmpRow.Item("ALCTD_NB"))
                    '            Next
                    '            wkDataRow(0).Item("KONSU") = wkKONSU
                    '            wkDataRow(0).Item("ALCTD_QT") = wkALCTD_QT
                    '            wkDataRow(0).Item("ZAN_KONSU") = wkZAN_KONSU
                    '            wkDataRow(0).Item("ALCTD_NB") = wkALCTD_NB
                    '            wkDataRow(0).Item("ZAN_HASU") = CLng(wkDataRow(0).Item("ALCTD_NB")) Mod CLng(wkDataRow(0).Item("PKG_NB"))
                    '        End If
                    '        cloneDt.ImportRow(wkDataRow(0))

                    '    Else
                    '        '②入荷管理区分="1"、更新プログラムID<>"IKOU"の場合は、まとめ処理を実行。 ----------------------------
                    '        '１件を超える場合にまとめる 
                    '        If wkDataRow.Length > 1 Then
                    '            wkKONSU = 0
                    '            wkALCTD_QT = 0
                    '            wkZAN_KONSU = 0
                    '            wkALCTD_NB = 0

                    '            For Each tmpRow As DataRow In wkDataRow
                    '                wkKONSU += Convert.ToDecimal(tmpRow.Item("KONSU"))
                    '                wkALCTD_QT += Convert.ToDecimal(tmpRow.Item("ALCTD_QT"))
                    '                wkZAN_KONSU += Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                    '                wkALCTD_NB += Convert.ToDecimal(tmpRow.Item("ALCTD_NB"))
                    '            Next
                    '            wkDataRow(0).Item("KONSU") = wkKONSU
                    '            wkDataRow(0).Item("ALCTD_QT") = wkALCTD_QT
                    '            wkDataRow(0).Item("ZAN_KONSU") = wkZAN_KONSU
                    '            wkDataRow(0).Item("ALCTD_NB") = wkALCTD_NB
                    '            wkDataRow(0).Item("ZAN_HASU") = CLng(wkDataRow(0).Item("ALCTD_NB")) Mod CLng(wkDataRow(0).Item("PKG_NB"))
                    '        End If
                    '        cloneDt.ImportRow(wkDataRow(0))

                    '    End If

                    'Else
                    '    '③入荷管理区分<>"1"の場合は、まとめ処理SKIP。件数分設定する。 ---------------
                    '    For Cut As Integer = 0 To wkDataRow.Length - 1
                    '        cloneDt.ImportRow(wkDataRow(Cut))
                    '    Next

                    'End If

                    If outDt.Rows(i).Item("SET_NAIYO").ToString = "1" Then

                        '①入荷管理区分="1"の場合は、残個数の最小値を取得 ----------------------------
                        If wkDataRow.Length > 1 Then
                            wkKONSU = 0
                            wkHASU = 0
                            wkALCTD_QT = 0
                            wkZAN_KONSU = 0
                            wkALCTD_NB = 0
                            wkZAN_HASU = 0

                            For Each tmpRow As DataRow In wkDataRow
                                wkKONSU += Math.Floor(Convert.ToDecimal(tmpRow.Item("KONSU")))
                                '要望管理1805 2013/01/28 s.kobayashi
                                wkHASU += Convert.ToDecimal(tmpRow.Item("HASU"))
                                wkALCTD_QT += Convert.ToDecimal(tmpRow.Item("ALCTD_QT"))
                                If wkZAN_KONSU = 0 Then
                                    wkZAN_KONSU = Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                                    wkZAN_HASU = Convert.ToDecimal(tmpRow.Item("ZAN_HASU"))
                                Else
                                    If Convert.ToDecimal(tmpRow.Item("ZAN_KONSU")) < wkZAN_KONSU Then
                                        wkZAN_KONSU = Convert.ToDecimal(tmpRow.Item("ZAN_KONSU"))
                                        wkZAN_HASU = Convert.ToDecimal(tmpRow.Item("ZAN_HASU"))
                                    End If
                                End If
                                wkALCTD_NB += Convert.ToDecimal(tmpRow.Item("ALCTD_NB"))
                            Next
                            '要望管理1805 2013/01/28 s.kobayashi
                            wkDataRow(0).Item("HASU") = wkHASU Mod Convert.ToDecimal(wkDataRow(0).Item("PKG_NB"))
                            wkDataRow(0).Item("KONSU") = wkKONSU + Math.Floor(wkHASU / Convert.ToDecimal(wkDataRow(0).Item("PKG_NB")))
                            wkDataRow(0).Item("ALCTD_QT") = wkALCTD_QT
                            wkDataRow(0).Item("ZAN_KONSU") = wkZAN_KONSU
                            wkDataRow(0).Item("ALCTD_NB") = wkALCTD_NB
                            'wkDataRow(0).Item("ZAN_HASU") = CLng(wkDataRow(0).Item("ALCTD_NB")) Mod CLng(wkDataRow(0).Item("PKG_NB"))
                            wkDataRow(0).Item("ZAN_HASU") = wkZAN_HASU
                        End If
                        cloneDt.ImportRow(wkDataRow(0))

                    Else
                        '②入荷管理区分<>"1"の場合は、まとめ処理SKIP。件数分設定する。 ---------------
                        For Cut As Integer = 0 To wkDataRow.Length - 1
                            cloneDt.ImportRow(wkDataRow(Cut))
                        Next

                    End If
                    '(2012.11.08)要望番号1572 千葉系納品書編集 --  END  --

                    '20121026 修正 --End--

                Next

                '元データクリア
                outDt.Clear()

                '集約データを元データへ
                For Each cloneRow As DataRow In cloneDt.Rows
                    Dim wkNewRow As DataRow = ds.Tables(DataSetNM).NewRow
                    '-00-
                    wkNewRow("RPT_ID") = cloneRow("RPT_ID")
                    wkNewRow("NRS_BR_CD") = cloneRow("NRS_BR_CD")
                    wkNewRow("PRINT_SORT") = cloneRow("PRINT_SORT")
                    wkNewRow("DEST_CD") = cloneRow("DEST_CD")
                    wkNewRow("DEST_NM") = cloneRow("DEST_NM")
                    wkNewRow("DEST_AD_1") = cloneRow("DEST_AD_1")
                    wkNewRow("DEST_AD_2") = cloneRow("DEST_AD_2")
                    wkNewRow("DEST_AD_3") = cloneRow("DEST_AD_3")
                    wkNewRow("DEST_TEL") = cloneRow("DEST_TEL")
                    wkNewRow("OUTKA_PLAN_DATE") = cloneRow("OUTKA_PLAN_DATE")
                    '-01-
                    wkNewRow("CUST_ORD_NO") = cloneRow("CUST_ORD_NO")
                    wkNewRow("BUYER_ORD_NO") = cloneRow("BUYER_ORD_NO")
                    wkNewRow("UNSOCO_NM") = cloneRow("UNSOCO_NM")
                    wkNewRow("ARR_PLAN_DATE") = cloneRow("ARR_PLAN_DATE")
                    wkNewRow("ARR_PLAN_TIME") = cloneRow("ARR_PLAN_TIME")
                    wkNewRow("OUTKA_NO_L") = cloneRow("OUTKA_NO_L")
                    wkNewRow("URIG_NM") = cloneRow("URIG_NM")
                    wkNewRow("CUST_NM_L") = cloneRow("CUST_NM_L")
                    wkNewRow("CUST_AD_1") = cloneRow("CUST_AD_1")
                    wkNewRow("CUST_AD_2") = cloneRow("CUST_AD_2")
                    '-02-
                    wkNewRow("CUST_TEL") = cloneRow("CUST_TEL")
                    wkNewRow("NRS_BR_NM") = cloneRow("NRS_BR_NM")
                    wkNewRow("NRS_BR_AD1") = cloneRow("NRS_BR_AD1")
                    wkNewRow("NRS_BR_AD2") = cloneRow("NRS_BR_AD2")
                    wkNewRow("NRS_BR_TEL") = cloneRow("NRS_BR_TEL")
                    wkNewRow("NRS_BR_FAX") = cloneRow("NRS_BR_FAX")
                    wkNewRow("PIC") = cloneRow("PIC")
                    wkNewRow("OUTKA_NO_M") = cloneRow("OUTKA_NO_M")
                    wkNewRow("OUTKA_NO_S") = cloneRow("OUTKA_NO_S")
                    wkNewRow("INKA_DATE") = cloneRow("INKA_DATE")
                    '-03-
                    wkNewRow("REMARK_OUT") = cloneRow("REMARK_OUT")
                    wkNewRow("GOODS_NM_1") = cloneRow("GOODS_NM_1")
                    wkNewRow("LOT_NO") = cloneRow("LOT_NO")
                    wkNewRow("SERIAL_NO") = cloneRow("SERIAL_NO")
                    wkNewRow("IRIME") = cloneRow("IRIME")
                    wkNewRow("PKG_NB") = cloneRow("PKG_NB")
                    wkNewRow("KONSU") = cloneRow("KONSU")
                    wkNewRow("HASU") = cloneRow("HASU")
                    wkNewRow("ALCTD_NB") = cloneRow("ALCTD_NB")
                    wkNewRow("PKG_UT") = cloneRow("PKG_UT")
                    '-04-
                    wkNewRow("PKG_UT_NM") = cloneRow("PKG_UT_NM")
                    wkNewRow("ALCTD_QT") = cloneRow("ALCTD_QT")
                    wkNewRow("IRIME_UT") = cloneRow("IRIME_UT")
                    wkNewRow("WT") = cloneRow("WT")
                    wkNewRow("UNSO_WT") = cloneRow("UNSO_WT")
                    wkNewRow("TOU_NO") = cloneRow("TOU_NO")
                    wkNewRow("SITU_NO") = cloneRow("SITU_NO")
                    wkNewRow("ZONE_CD") = cloneRow("ZONE_CD")
                    wkNewRow("LOCA") = cloneRow("LOCA")
                    wkNewRow("ZAN_KONSU") = cloneRow("ZAN_KONSU")
                    '-05-
                    wkNewRow("ZAN_HASU") = cloneRow("ZAN_HASU")
                    wkNewRow("GOODS_CD_CUST") = cloneRow("GOODS_CD_CUST")
                    wkNewRow("REMARK_M") = cloneRow("REMARK_M")
                    wkNewRow("CUST_NM_S") = cloneRow("CUST_NM_S")
                    wkNewRow("CUST_ORD_NO_DTL") = cloneRow("CUST_ORD_NO_DTL")
                    wkNewRow("OUTKA_REMARK") = cloneRow("OUTKA_REMARK")
                    wkNewRow("HAISOU_REMARK") = cloneRow("HAISOU_REMARK")
                    wkNewRow("NHS_REMARK") = cloneRow("NHS_REMARK")
                    wkNewRow("KYORI") = cloneRow("KYORI")
                    wkNewRow("OUTKA_PKG_NB_L") = cloneRow("OUTKA_PKG_NB_L")
                    '-06-
                    wkNewRow("SOKO_NM") = cloneRow("SOKO_NM")
                    wkNewRow("BUSHO_NM") = cloneRow("BUSHO_NM")
                    wkNewRow("NRS_BR_NM_NICHI") = cloneRow("NRS_BR_NM_NICHI")
                    wkNewRow("ALCTD_CAN_QT") = cloneRow("ALCTD_CAN_QT")
                    wkNewRow("ALCTD_KB") = cloneRow("ALCTD_KB")
                    wkNewRow("PTN_FLAG") = cloneRow("PTN_FLAG")
                    wkNewRow("CUST_CD_L") = cloneRow("CUST_CD_L")
                    wkNewRow("ORDER_TYPE") = cloneRow("ORDER_TYPE")
                    wkNewRow("TITLE_SMPL") = cloneRow("TITLE_SMPL")
                    wkNewRow("LT_DATE") = cloneRow("LT_DATE")
                    '-07-
                    wkNewRow("ALCTD_CAN_NB") = cloneRow("ALCTD_CAN_NB")
                    wkNewRow("BUYER_ORD_NO_DTL") = cloneRow("BUYER_ORD_NO_DTL")
                    wkNewRow("GOODS_COND_CD") = cloneRow("GOODS_COND_CD")
                    wkNewRow("REMARK_ZAI") = cloneRow("REMARK_ZAI")
                    wkNewRow("BACKLOG_QT") = cloneRow("BACKLOG_QT")
                    wkNewRow("DOKU_NM") = cloneRow("DOKU_NM")
                    wkNewRow("ATSUKAISYA_NM") = cloneRow("ATSUKAISYA_NM")
                    wkNewRow("SAGYO_MEI_REC_NO_1") = cloneRow("SAGYO_MEI_REC_NO_1")
                    wkNewRow("SAGYO_MEI_CD_1") = cloneRow("SAGYO_MEI_CD_1")
                    wkNewRow("SAGYO_MEI_NM_1") = cloneRow("SAGYO_MEI_NM_1")
                    '-08-
                    wkNewRow("SAGYO_MEI_REC_NO_2") = cloneRow("SAGYO_MEI_REC_NO_2")
                    wkNewRow("SAGYO_MEI_CD_2") = cloneRow("SAGYO_MEI_CD_2")
                    wkNewRow("SAGYO_MEI_NM_2") = cloneRow("SAGYO_MEI_NM_2")
                    wkNewRow("SAGYO_MEI_REC_NO_3") = cloneRow("SAGYO_MEI_REC_NO_3")
                    wkNewRow("SAGYO_MEI_CD_3") = cloneRow("SAGYO_MEI_CD_3")
                    wkNewRow("SAGYO_MEI_NM_3") = cloneRow("SAGYO_MEI_NM_3")
                    wkNewRow("SAGYO_MEI_REC_NO_4") = cloneRow("SAGYO_MEI_REC_NO_4")
                    wkNewRow("SAGYO_MEI_CD_4") = cloneRow("SAGYO_MEI_CD_4")
                    wkNewRow("SAGYO_MEI_NM_4") = cloneRow("SAGYO_MEI_NM_4")
                    wkNewRow("SAGYO_MEI_REC_NO_5") = cloneRow("SAGYO_MEI_REC_NO_5")
                    '-09-
                    wkNewRow("SAGYO_MEI_CD_5") = cloneRow("SAGYO_MEI_CD_5")
                    wkNewRow("SAGYO_MEI_NM_5") = cloneRow("SAGYO_MEI_NM_5")
                    wkNewRow("MCD_SET_NAIYO") = cloneRow("MCD_SET_NAIYO")
                    wkNewRow("MCD_SET_NAIYO_2") = cloneRow("MCD_SET_NAIYO_2")
                    wkNewRow("MCD_SET_NAIYO_3") = cloneRow("MCD_SET_NAIYO_3")
                    wkNewRow("CUST_NM_S_H") = cloneRow("CUST_NM_S_H")
                    wkNewRow("TOU_SITU_NM") = cloneRow("TOU_SITU_NM")
                    wkNewRow("JISYATASYA_KB") = cloneRow("JISYATASYA_KB")
                    wkNewRow("GOODS_COND_NM") = cloneRow("GOODS_COND_NM")
                    wkNewRow("PC_KB_NM") = cloneRow("PC_KB_NM")
                    '-10-
                    wkNewRow("TORI_KB") = cloneRow("TORI_KB")
                    wkNewRow("SHIP_CD_L") = cloneRow("SHIP_CD_L")
                    wkNewRow("SET_NAIYO_4") = cloneRow("SET_NAIYO_4")
                    wkNewRow("SET_NAIYO_5") = cloneRow("SET_NAIYO_5")
                    wkNewRow("SET_NAIYO_6") = cloneRow("SET_NAIYO_6")
                    wkNewRow("GOODS_NM_2") = cloneRow("GOODS_NM_2")
                    wkNewRow("KICHO_KB") = cloneRow("KICHO_KB")
                    wkNewRow("SEI_YURAI_KB") = cloneRow("SEI_YURAI_KB")
                    wkNewRow("REMARK_UPPER") = cloneRow("REMARK_UPPER")
                    wkNewRow("REMARK_LOWER") = cloneRow("REMARK_LOWER")
                    '-11-
                    wkNewRow("GOODS_SYUBETU") = cloneRow("GOODS_SYUBETU")
                    wkNewRow("YUKOU_KIGEN") = cloneRow("YUKOU_KIGEN")
                    wkNewRow("GOODS_NM_3") = cloneRow("GOODS_NM_3")
                    wkNewRow("EDISHIP_CD") = cloneRow("EDISHIP_CD")
                    wkNewRow("EDIDEST_CD") = cloneRow("EDIDEST_CD")
                    wkNewRow("SET_NAIYO_FROM1") = cloneRow("SET_NAIYO_FROM1")
                    wkNewRow("SET_NAIYO_FROM2") = cloneRow("SET_NAIYO_FROM2")
                    wkNewRow("SET_NAIYO_FROM3") = cloneRow("SET_NAIYO_FROM3")
                    wkNewRow("CUST_AD_3") = cloneRow("CUST_AD_3")
                    wkNewRow("DEST_REMARK") = cloneRow("DEST_REMARK")
                    '-12-
                    wkNewRow("DEST_SALES_CD") = cloneRow("DEST_SALES_CD")
                    wkNewRow("DEST_SALES_NM") = cloneRow("DEST_SALES_NM")
                    wkNewRow("DEST_SALES_AD_1") = cloneRow("DEST_SALES_AD_1")
                    wkNewRow("DEST_SALES_AD_2") = cloneRow("DEST_SALES_AD_2")
                    wkNewRow("DEST_SALES_AD_3") = cloneRow("DEST_SALES_AD_3")
                    wkNewRow("DEST_SALES_TEL") = cloneRow("DEST_SALES_TEL")
                    wkNewRow("CUST_NM_M") = cloneRow("CUST_NM_M")
                    wkNewRow("DENPYO_NM") = cloneRow("DENPYO_NM")
                    wkNewRow("SAGYO_RYAK_1") = cloneRow("SAGYO_RYAK_1")
                    wkNewRow("SAGYO_RYAK_2") = cloneRow("SAGYO_RYAK_2")
                    '-13-
                    wkNewRow("SAGYO_RYAK_3") = cloneRow("SAGYO_RYAK_3")
                    wkNewRow("SAGYO_RYAK_4") = cloneRow("SAGYO_RYAK_4")
                    wkNewRow("SAGYO_RYAK_5") = cloneRow("SAGYO_RYAK_5")
                    wkNewRow("DEST_SAGYO_RYAK_1") = cloneRow("DEST_SAGYO_RYAK_1")
                    wkNewRow("DEST_SAGYO_RYAK_2") = cloneRow("DEST_SAGYO_RYAK_2")
                    wkNewRow("SZ01_YUSO") = cloneRow("SZ01_YUSO")
                    wkNewRow("SZ01_UNSO") = cloneRow("SZ01_UNSO")
                    wkNewRow("GOODS_COND_CD_1") = cloneRow("GOODS_COND_CD_1")
                    wkNewRow("GOODS_COND_CD_3") = cloneRow("GOODS_COND_CD_3")
                    wkNewRow("SET_NAIYO") = cloneRow("SET_NAIYO")
                    '-14-
                    wkNewRow("SYS_UPD_PGID") = cloneRow("SYS_UPD_PGID")
                    '要望番号:1607 terakawa 2012.11.16 Start
                    wkNewRow("SHOBO_KBN_NM") = cloneRow("SHOBO_KBN_NM")
                    wkNewRow("HINMEI") = cloneRow("HINMEI")
                    '要望番号:1607 terakawa 2012.11.16 End
                    '(2012.11.21)要望番号:1615 容器重量追加 -- START --
                    wkNewRow("PKG_WT") = cloneRow("PKG_WT")
                    '(2012.11.21)要望番号:1615 容器重量追加 --  END  --
                    '(2012.11.28) LMC637対応 -- START --
                    wkNewRow("CUST_CD_M") = cloneRow("CUST_CD_M")
                    wkNewRow("CUST_CD_S") = cloneRow("CUST_CD_S")
                    wkNewRow("CUST_COST_CD2") = cloneRow("CUST_COST_CD2")
                    '(2012.11.28) LMC637対応 --  END  --

                    '(2012.12.17)LMC731対応START--
                    wkNewRow("SEARCH_KEY_2") = cloneRow("SEARCH_KEY_2")
                    '(2012.12.17)LMC731対応  END --

                    '(2016.7.13) LMC629対応 -- START --
                    wkNewRow("GOODS_CRT_DATE") = cloneRow("GOODS_CRT_DATE")
                    '(2016.7.13) LMC629対応 -- END --

                    ds.Tables(DataSetNM).Rows.Add(wkNewRow)
                Next

                Return ds

                '(2012.10.19)要望番号1289 千葉系納品書編集 --  END  --


            Case "LMC622"                '端数の分割
                Dim outDt As DataTable = ds.Tables(DataSetNM)
                '分割実行後データ格納データセット作成
                Dim rtnDs As DataSet = ds.Clone
                'ソート済みデータ格納
                Dim rowIdxKonsu As Integer = 0
                Dim rowIdxHasu As Integer = 0
                Dim ngOfKonsu As Integer = 0
                Dim ngOfHasu As Integer = 0
                Dim ng As Integer = 0

                Dim konsu As Integer = 0
                Dim hasu As Integer = 0
                For Each row As DataRow In outDt.Select()

                    konsu = Convert.ToInt32(System.Math.Floor(Convert.ToDecimal(row.Item("konsu"))))
                    hasu = Convert.ToInt32(row.Item("HASU").ToString())
                    '端数項目があれば分割対象
                    If (konsu <> 0 And hasu <> 0) = True Then
                        '先に入れる
                        rowIdxKonsu = rtnDs.Tables(DataSetNM).Rows.Count
                        rtnDs.Tables(DataSetNM).ImportRow(row)
                        rowIdxHasu = rtnDs.Tables(DataSetNM).Rows.Count
                        rtnDs.Tables(DataSetNM).ImportRow(row)
                        '値の変更
                        ngOfKonsu = Convert.ToInt32(rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ALCTD_NB")) - Convert.ToInt32(rtnDs.Tables(DataSetNM).Rows(rowIdxHasu).Item("HASU"))
                        ngOfHasu = Convert.ToInt32(rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("HASU"))
                        ng = Convert.ToInt32(rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ALCTD_NB"))
                        '個数(ALCTD_NB)
                        rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ALCTD_NB") = ngOfKonsu.ToString()
                        rtnDs.Tables(DataSetNM).Rows(rowIdxHasu).Item("ALCTD_NB") = ngOfHasu.ToString()
                        '梱数(KONSU)
                        rtnDs.Tables(DataSetNM).Rows(rowIdxHasu).Item("KONSU") = "0"
                        '端数(HASU)
                        rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("HASU") = "0"
                        '数量(ALCTD_QT)
                        rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ALCTD_QT") = (Convert.ToDecimal(rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ALCTD_QT")) * ngOfKonsu / ng).ToString()
                        rtnDs.Tables(DataSetNM).Rows(rowIdxHasu).Item("ALCTD_QT") = (Convert.ToDecimal(rtnDs.Tables(DataSetNM).Rows(rowIdxHasu).Item("ALCTD_QT")) * ngOfHasu / ng).ToString()
                        '残梱数(ZAN_KONSU)
                        rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ZAN_KONSU") = "-----"
                        '残個数(ZAN_HASU)
                        rtnDs.Tables(DataSetNM).Rows(rowIdxKonsu).Item("ZAN_HASU") = "0"
                    Else
                        '分割対象外の場合はそのまま加える
                        rtnDs.Tables(DataSetNM).ImportRow(row)
                    End If
                Next

                Return rtnDs

            Case "LMC899"
                'TSMC
                Dim outDt As DataTable = ds.Tables(DataSetNM)

                With "摘要欄(明細部)の加工"

                    ' 各明細行の営業所コード、出荷管理番号L・M に紐付く TSMC在庫データ・シリンダーNo. の取得
                    ' 取得は営業所コード、出荷管理番号L・M 単位に 1回行う。同単位に複数件存在の可能性あり。
                    Dim zaiTsmcDt As DataTable = Nothing
                    Dim setDs As DataSet = New DSL.LMC500DS()
                    Dim where As New StringBuilder()
                    Dim cylinderNo As New StringBuilder()

                    For Each outDr As DataRow In outDt.Rows()
                        where.Clear()
                        where.Append(String.Concat("    NRS_BR_CD  = '", outDr.Item("NRS_BR_CD").ToString(), "' "))
                        where.Append(String.Concat("AND OUTKA_NO_L = '", outDr.Item("OUTKA_NO_L").ToString(), "' "))
                        where.Append(String.Concat("AND OUTKA_NO_M = '", outDr.Item("OUTKA_NO_M").ToString(), "' "))
                        If IsNothing(zaiTsmcDt) OrElse zaiTsmcDt.Select(where.ToString(), "").Count() = 0 Then
                            setDs.Tables(DataSetNM).Clear()
                            setDs.Tables(DataSetNM).ImportRow(outDr)
                            setDs = MyBase.CallDAC(Me._Dac, "SelectZaiTsmcCylinderNo", setDs)
                            If setDs.Tables("D_ZAI_TSMC").Rows().Count() > 0 Then
                                If IsNothing(zaiTsmcDt) Then
                                    zaiTsmcDt = setDs.Tables("D_ZAI_TSMC").Copy()
                                Else
                                    zaiTsmcDt.Merge(setDs.Tables("D_ZAI_TSMC"))
                                End If
                            End If
                        End If
                    Next

                    If Not IsNothing(zaiTsmcDt) Then
                        ' 各明細行の営業所コード、出荷管理番号L・M に紐付く TSMC在庫データ・シリンダーNo. が存在する場合、
                        ' ① C_OUTKA_S.SERIAL_NO に一致する TSMC在庫データ・シリンダーNo. が存在する場合、
                        ' 　そのシリンダーNo.を当該明細行の摘要欄に設定する。
                        ' ② C_OUTKA_S.SERIAL_NO が設定されていない、または一致する TSMC在庫データ・シリンダーNo. が存在しない場合、
                        ' 　シリンダーNo. 順に先頭から当該明細行の摘要欄に結合する。
                        For Each outDr As DataRow In outDt.Rows()
                            For tryCnt As Integer = If(outDr.Item("SERIAL_NO").ToString().Length() > 0, 1, 2) To 2
                                If True Then
                                    where.Clear()
                                    where.Append(String.Concat("    NRS_BR_CD  =  '", outDr.Item("NRS_BR_CD").ToString(), "' "))
                                    where.Append(String.Concat("AND OUTKA_NO_L =  '", outDr.Item("OUTKA_NO_L").ToString(), "' "))
                                    where.Append(String.Concat("AND OUTKA_NO_M =  '", outDr.Item("OUTKA_NO_M").ToString(), "' "))
                                End If
                                If tryCnt = 1 Then
                                    where.Append(String.Concat("AND CYLINDER_NO =  '", outDr.Item("SERIAL_NO").ToString(), "' "))
                                End If
                                If True Then
                                    where.Append(String.Concat("AND USE_FLG    <> '1' "))
                                End If
                                If tryCnt = 1 AndAlso zaiTsmcDt.Select(where.ToString()).Length() > 0 Then
                                    Exit For
                                End If
                            Next
                            cylinderNo.Clear()
                            Dim appendCnt As Integer = 0
                            For Each zaiTsmcDr As DataRow In zaiTsmcDt.Select(where.ToString(), "CYLINDER_NO")
                                appendCnt += 1
                                If appendCnt > Convert.ToInt32(outDr.Item("ALCTD_NB").ToString()) Then
                                    ' 明細の個数を超えない範囲での出力(対象シリンダーNo. 文字列の結合)とする。
                                    Exit For
                                End If
                                Dim appendStr As String = String.Concat(If(cylinderNo.Length() = 0, "", " "), zaiTsmcDr.Item("CYLINDER_NO").ToString())
                                cylinderNo.Append(appendStr)
                                ' 結合に使用した シリンダーNo. の行は“使用済”としてフラグを立て、次明細以降の抽出対象外とする。
                                zaiTsmcDr.Item("USE_FLG") = "1"
                            Next
                            If cylinderNo.Length() > 0 Then
                                outDr.Item("BUYER_ORD_NO_DTL") = String.Concat(cylinderNo.ToString(), " ", outDr.Item("BUYER_ORD_NO_DTL").ToString())
                            End If
                        Next
                    End If

                End With

                With "摘要欄(フッター部)の加工"

                    Dim listOutkaAtt As New List(Of String)
                    Dim dokuNm As String = String.Empty
                    Dim haisoRemarkMst As String = String.Empty

                    For Each row As DataRow In outDt.Select()
                        '項目 HAISOU_REMARK_MST は結合値なので分割する
                        '　1バイト目:  商品.毒劇区分が無しなら0, それ以外は1
                        '　2バイト以降:商品明細 TSMC 出荷時注意事項納品書記載対象 に該当の場合の 商品マスタ 出荷時注意事項
                        Dim dokuKb As String = Left(row.Item("HAISOU_REMARK_MST").ToString, 1)
                        Dim outkaAtt As String = Mid(row.Item("HAISOU_REMARK_MST").ToString, 2).TrimEnd()

                        '出荷時注意事項が被っていなければリストに追加
                        If Not String.IsNullOrEmpty(outkaAtt) Then
                            If Not listOutkaAtt.Contains(outkaAtt) Then
                                listOutkaAtt.Add(outkaAtt)
                            End If
                        End If

                        '毒劇物であれば毒劇物譲渡書の出力値をセット
                        If "1".Equals(dokuKb) AndAlso String.IsNullOrEmpty(dokuNm) Then
                            dokuNm = "兼　毒劇物譲渡書"
                        End If
                    Next

                    '出荷時注意事項と毒劇物譲渡書を連結して適用欄の出力値とする
                    If listOutkaAtt.Count > 0 Then
                        For i As Integer = 0 To listOutkaAtt.Count - 1
                            haisoRemarkMst = String.Concat(haisoRemarkMst, "、", listOutkaAtt(i))
                        Next
                        haisoRemarkMst = Mid(haisoRemarkMst, 2)
                        If Not String.IsNullOrEmpty(dokuNm) Then
                            haisoRemarkMst = String.Concat(haisoRemarkMst, "　")
                        End If
                    End If
                    haisoRemarkMst = String.Concat(haisoRemarkMst, dokuNm)

                    '全レコードの適用欄に書き戻し
                    For Each row As DataRow In outDt.Select()
                        row.Item("HAISOU_REMARK_MST") = haisoRemarkMst
                    Next

                End With

                With "仮納品書判定フラグ設定"

                    If Not (dtIn Is Nothing) AndAlso dtIn.Rows().Count() > 0 Then
                        For Each row As DataRow In outDt.Select()
                            If dtIn.Rows(0).Item("KARI_FLG").ToString() = "1" Then
                                row.Item("KARI_FLG") = "1"
                            Else
                                row.Item("KARI_FLG") = "0"
                            End If
                        Next
                    End If

                End With

            Case "LMC900"
                '物産アニマルヘルス（納品案内書）

                Const DIVIDE_LEN As Integer = 60    '請求先名(上段)の最大バイト数
                Const GYO_NO_MAX As Integer = 6     '1伝票の明細数

                Dim outDt As DataTable = ds.Tables(DataSetNM)

                Dim denpNo As String = String.Empty
                Dim gyoNo As Integer = GYO_NO_MAX
                Dim befTorihikiKbn As String = String.Empty

                For Each row As DataRow In outDt.Select()

                    With "請求先名の分割"

                        '請求先名(上段)ではみ出す分を請求先名(下段)にセット
                        Dim seiqtoNm As String = row.Item("A_SEIQTO_NM_1").ToString()
                        If LenB(seiqtoNm) > DIVIDE_LEN Then
                            Dim seiqtoNm1 As String = LeftB(seiqtoNm, DIVIDE_LEN)
                            Dim seiqtoNm2 As String = seiqtoNm.Substring(seiqtoNm1.Length, seiqtoNm.Length - seiqtoNm1.Length)
                            row.Item("A_SEIQTO_NM_1") = seiqtoNm1
                            row.Item("A_SEIQTO_NM_2") = seiqtoNm2
                        End If

                    End With

                    With "採番"

                        Dim torihikiKbn As String = row.Item("A_TORIHIKI_KBN").ToString()
                        If Not torihikiKbn.Equals(befTorihikiKbn) Then
                            '案内書_取引区分が変わったら採番をリセット
                            denpNo = New NumberMasterUtility().GetAutoCode(NumberMasterUtility.NumberKbn.BAH_DENP_NO, Me, row.Item("NRS_BR_CD").ToString())
                            gyoNo = 1
                            befTorihikiKbn = torihikiKbn
                        ElseIf gyoNo = GYO_NO_MAX Then
                            '1伝票の明細数に到達したら採番をリセット
                            denpNo = New NumberMasterUtility().GetAutoCode(NumberMasterUtility.NumberKbn.BAH_DENP_NO, Me, row.Item("NRS_BR_CD").ToString())
                            gyoNo = 1
                        Else
                            '明細数をカウントアップ
                            gyoNo += 1
                        End If

                        row.Item("DENP_NO") = denpNo
                        row.Item("SEIRI_NO") = denpNo
                        row.Item("GYO_NO") = gyoNo.ToString()

                    End With

                Next

            Case Else
                '追記終了
        End Select

        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    '''　棟番号取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTouNo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTouNo", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet
        '2015/1/23
        'LMC520DACが肥大化したため521に分岐する処理を追加
        '
        '
        '
        Dim rptId As String = ds.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString

        Select Case rptId

            Case "LMC757"
                Return MyBase.CallDAC(Me._501Dac, "SelectPrintData", ds)

            Case "LMC787"
                Return MyBase.CallDAC(Me._787Dac, "SelectPrintData", ds)
                '20160120 ゴードー tsunehira add start
            Case "LMC815"
                Return MyBase.CallDAC(Me._501Dac, "SelectPrintData", ds)
                '20160120 ゴードー tsunehira add end

#If True Then 'ADD 20220/04/30 007999   【LMS】セミEDI_ジョンソンエンドジョンソンJ&J_土気自動倉庫預かり_入荷・出荷新規
            Case "LMC545"
                'Return MyBase.CallDAC(Me._501Dac, "SelectPrintData", ds)
                '全オーダーNo取得
                ds = MyBase.CallDAC(Me._501Dac, "SELECT_OUT_ORD_NO", ds)

                If ds.Tables("OUT_ORDER").Rows.Count > 0 Then

                    '別インスタンス
                    Dim setDs As DataSet = ds.Copy()

                    For i As Integer = 0 To ds.Tables("OUT_ORDER").Rows.Count - 1
                        ds.Tables("LMC500IN").Clear()
                        Dim dr As DataRow = ds.Tables("LMC500IN").NewRow()

                        dr("NRS_BR_CD") = setDs.Tables("LMC500IN").Rows(0).Item("NRS_BR_CD").ToString()
                        dr("KANRI_NO_L") = setDs.Tables("LMC500IN").Rows(0).Item("KANRI_NO_L").ToString()
                        dr("PRT_NB") = setDs.Tables("LMC500IN").Rows(0).Item("PRT_NB").ToString()
                        dr("PTN_FLAG") = setDs.Tables("LMC500IN").Rows(0).Item("PTN_FLAG").ToString
                        dr("SAIHAKKO_FLG") = setDs.Tables("LMC500IN").Rows(0).Item("SAIHAKKO_FLG").ToString()
                        dr("OUTKA_NO_L") = setDs.Tables("LMC500IN").Rows(0).Item("OUTKA_NO_L").ToString()
                        dr("ORDER_NO") = ds.Tables("OUT_ORDER").Rows(i).Item("CHKCUST_ORD_NO").ToString()

                        ds.Tables("LMC500IN").Rows.Add(dr)

                        ds = MyBase.CallDAC(Me._501Dac, "SelectPrintData", ds)

                        setDs.Tables("LMC545OUT").Merge(ds.Tables("LMC545OUT"))
                    Next

                    ''ds = setDs.Copy
                    ds.Tables("LMC545OUT").Clear()
                    ds.Tables("LMC545OUT").Merge(setDs.Tables("LMC545OUT"))
                    Return ds
                End If
#End If
            Case "LMC880"
                Return MyBase.CallDAC(Me._880Dac, "SelectPrintData", ds)

            Case "LMC891"
                Return MyBase.CallDAC(Me._891Dac, "SelectPrintData", ds)

            Case "LMC892"
                Return MyBase.CallDAC(Me._891Dac, "SelectPrintData", ds)

            Case "LMC900"
                Return MyBase.CallDAC(Me._502Dac, "SelectPrintData_LMC900", ds)

            Case Else
                Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

        End Select


    End Function

    '2015.09.11 エクシングQR対応追加START
    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditQrDataSet(ByVal ds As DataSet) As DataSet

        Dim preNrsBrCd As String = String.Empty
        Dim unsoNoL As String = String.Empty
        Dim preInput As String = String.Empty
        Dim preQrCode As String = String.Empty

        Dim keyVal As String = String.Empty

        If String.IsNullOrEmpty(ds.Tables("LMC500OUT").Rows(0).Item("CUST_NM_S").ToString()) = True Then
            Return ds
        ElseIf String.IsNullOrEmpty(ds.Tables("LMC500OUT").Rows(0).Item("CUST_NM_S").ToString()) = False AndAlso _
               String.IsNullOrEmpty(ds.Tables("LMC500OUT").Rows(0).Item("CUST_NM_S_H").ToString()) = True Then
            ' 一覧情報を取得する
            ds = Me.CallDAC(_Dac, "SearchKeyCode", ds)
            '区分マスタに０件の場合はメッセージをセット
            If MyBase.GetResultCount = 0 Then
                Return ds
            Else
                keyVal = ds.Tables("KEY_CODE").Rows(0).Item("KEY_CODE").ToString()
            End If
            '201510.20 QRコード不具合対応START
        Else
            Return ds
            '201510.20 QRコード不具合対応END
        End If

        For j As Integer = 0 To ds.Tables("LMC500OUT").Rows.Count - 1

            '201510.20 QRコード不具合対応START
            If ds.Tables("LMC500OUT").Rows(j).Item("CUST_NM_S").ToString().Length <> 9 Then
                Return ds
            End If
            '201510.20 QRコード不具合対応END

            Dim strInput As String = String.Concat(ds.Tables("LMC500OUT").Rows(j).Item("NRS_BR_CD").ToString(), ds.Tables("LMC500OUT").Rows(j).Item("CUST_NM_S").ToString())

            If String.IsNullOrEmpty(preInput) AndAlso strInput.Equals(preInput) = False Then
                ds.Tables("LMC500OUT").Rows(j).Item("QR_CODE") = Me.encrypt(strInput, keyVal)
            Else
                ds.Tables("LMC500OUT").Rows(j).Item("QR_CODE") = preQrCode
            End If

            preNrsBrCd = ds.Tables("LMC500OUT").Rows(j).Item("NRS_BR_CD").ToString()
            unsoNoL = ds.Tables("LMC500OUT").Rows(j).Item("CUST_NM_S").ToString()
            preInput = String.Concat(preNrsBrCd, unsoNoL)
            preQrCode = ds.Tables("LMC500OUT").Rows(j).Item("QR_CODE").ToString()

        Next

        Return ds

    End Function


#Region "暗号化ロジック"

    ''' <summary>
    ''' 暗号化ロジック
    ''' </summary>
    ''' <param name="strInput">string</param>
    ''' <returns>string</returns>
    ''' <remarks></remarks>
    Private Function encrypt(ByVal strInput As String, ByRef keyVal As String) As String

        Dim key() As Byte = Encoding.ASCII.GetBytes(keyVal.Substring(0, 16))
        Dim iv() As Byte = Encoding.ASCII.GetBytes(keyVal.Substring(0, 8))
        Dim des As New TripleDESCryptoServiceProvider
        des.Key = key
        des.IV = iv
        Dim transform As ICryptoTransform = des.CreateEncryptor
        Dim destination As Byte()
        Using ms As System.IO.MemoryStream = New System.IO.MemoryStream
            Using cs As CryptoStream = New CryptoStream(ms, transform, CryptoStreamMode.Write)
                Try
                    Dim source As Byte() = Encoding.Unicode.GetBytes(strInput)
                    cs.Write(source, 0, source.Length)
                    cs.FlushFinalBlock()
                    destination = ms.ToArray()
                Catch ex As Exception
                    Throw
                Finally
                    cs.Close()
                    ms.Close()
                End Try
            End Using
        End Using

        Return System.Convert.ToBase64String(destination)

    End Function

#End Region
    '2015.09.11 エクシングQR対応追加END

#Region "文字列操作"

    ''' <summary>
    ''' 文字列長（Shift_JIS 換算のバイト数）を求める
    ''' </summary>
    ''' <param name="targetString">対象文字列</param>
    ''' <returns>対象文字列のバイト数</returns>
    ''' <remarks></remarks>
    Private Function LenB(ByVal targetString As String) As Integer

        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(targetString)

    End Function

    ''' <summary>Left関数のバイト版。文字数をバイト数で指定して文字列を切捨て。</summary>
    ''' <param name="str">対象の文字列</param>
    ''' <param name="Length">切り抜く文字列のバイト数</param>
    ''' <returns>切捨てられた文字列</returns>
    ''' <remarks>最後の１バイトが全角文字の半分になる場合、その１バイトは無視される。</remarks>
    Public Function LeftB(ByVal str As String, Optional ByVal Length As Integer = 0) As String

        If str = "" Then
            Return ""
        End If

        'Lengthが0か、バイト数をオーバーする場合は全バイトが指定されたものとみなす。
        Dim RestLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(str)

        If Length = 0 OrElse Length > RestLength Then
            Length = RestLength
        End If

        '切捨て
        Dim SJIS As System.Text.Encoding = System.Text.Encoding.GetEncoding("Shift-JIS")
        Dim B() As Byte = CType(Array.CreateInstance(GetType(Byte), Length), Byte())

        Array.Copy(SJIS.GetBytes(str), 0, B, 0, Length)

        Dim st1 As String = SJIS.GetString(B)

        '切捨てた結果、最後の１バイトが全角文字の半分だった場合、その半分は切り捨てる。
        Dim ResultLength As Integer = System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(st1)

        If Length = ResultLength - 1 Then
            Return st1.Substring(0, st1.Length - 1)
        Else
            Return st1
        End If

    End Function

#End Region

#End Region

#End Region

End Class
