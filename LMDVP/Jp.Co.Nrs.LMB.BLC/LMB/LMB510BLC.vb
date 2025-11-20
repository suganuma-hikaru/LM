' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷管理
'  プログラムID     :  LMB510    : 入荷チェックリスト印刷
'  作  成  者       :  [kobayashi]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB510BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB510BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB510DAC = New LMB510DAC()

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac514 As LMB514DAC = New LMB514DAC()

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

        '元のデータ
        Dim tableNmIn As String = "LMB510IN"
        Dim tableNmOut As String = "LMB510OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone

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

        
        'レポートID分繰り返す
        Dim prtDs As DataSet
        For Each dr As DataRow In ds.Tables("M_RPT").Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet
            '指定したレポートIDのデータを抽出する
            prtDs = comPrt.CallDataSet(ds.Tables("LMB510OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMB510OUT"), _
                                  ds.Tables(LMConst.RD))
        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="prtId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal prtId As String, ByVal ds As DataSet) As DataSet

        '抽出データテーブル取得
        Dim outDt As DataTable = ds.Tables("LMB510OUT")
        '抽出データ数取得
        Dim max As Integer = outDt.Rows.Count - 1
        'ワーク用変数
        Dim konsu As Integer = 0
        Dim hasu As Integer = 0
        Dim pkg As Integer = 0
        '追記 
        Dim LOT_SP_KB As String = String.Empty
        Dim lot As String = String.Empty 'LOT_NO
        Dim NO As String = "NO."         '探したい文字列
        Dim numYN As Integer = 0         '文字列検出用　ない場合はそのままゼロ
        Dim left As String = String.Empty '左4桁
        Dim center As String = String.Empty '中央3桁
        Dim right As String = String.Empty  '右3桁
        Dim editLotNo As String = String.Empty '右3桁
        Dim space As String = "  "

        '丸数字(例：①)を括弧数字(例：(1))に変換 篠原　START 2012/05/29 Notes933
        Dim strLOT_NO As String = String.Empty
        '丸数字(例：①)を括弧数字(例：(1))に変換 篠原　E N D 2012/05/29 Notes933

        '抽出データ明細行の梱数チェック
        For i As Integer = 0 To max
            '丸数字(例：①)を括弧数字(例：(1))に変換 篠原　START 2012/05/29 Notes933
            strLOT_NO = (outDt.Rows(i).Item("LOT_NO").ToString())
            strLOT_NO = strLOT_NO.Replace("①", "(1)")
            strLOT_NO = strLOT_NO.Replace("②", "(2)")
            strLOT_NO = strLOT_NO.Replace("③", "(3)")
            strLOT_NO = strLOT_NO.Replace("④", "(4)")
            strLOT_NO = strLOT_NO.Replace("⑤", "(5)")
            strLOT_NO = strLOT_NO.Replace("⑥", "(6)")
            strLOT_NO = strLOT_NO.Replace("⑦", "(7)")
            strLOT_NO = strLOT_NO.Replace("⑧", "(8)")
            strLOT_NO = strLOT_NO.Replace("⑨", "(9)")
            strLOT_NO = strLOT_NO.Replace("⑩", "(10)")
            strLOT_NO = strLOT_NO.Replace("⑪", "(11)")
            strLOT_NO = strLOT_NO.Replace("⑫", "(12)")
            strLOT_NO = strLOT_NO.Replace("⑬", "(13)")
            strLOT_NO = strLOT_NO.Replace("⑭", "(14)")
            strLOT_NO = strLOT_NO.Replace("⑮", "(15)")
            strLOT_NO = strLOT_NO.Replace("⑯", "(16)")
            strLOT_NO = strLOT_NO.Replace("⑰", "(17)")
            strLOT_NO = strLOT_NO.Replace("⑱", "(18)")
            strLOT_NO = strLOT_NO.Replace("⑲", "(19)")
            strLOT_NO = strLOT_NO.Replace("⑳", "(20)")
            outDt.Rows(i).Item("LOT_NO") = strLOT_NO
            '丸数字(例：①)を括弧数字(例：(1))に変換 篠原　E N D 2012/05/29 Notes933
            '梱数、端数、包装個数取得
            konsu = Convert.ToInt32(outDt.Rows(i).Item("KONSU").ToString())
            hasu = Convert.ToInt32(outDt.Rows(i).Item("HASU").ToString())
            pkg = Convert.ToInt32(outDt.Rows(i).Item("PKG_NB").ToString())

            '梱数 = 0 かつ 端数 >= 包装個数 の場合は、端数を梱数と端数に分解する
            If konsu = 0 AndAlso hasu >= pkg Then
                '梱数と端数を算出し抽出データテーブルに詰め直す
                outDt.Rows(i).Item("KONSU") = Math.Floor(hasu / pkg).ToString()
                outDt.Rows(i).Item("HASU") = (hasu Mod pkg).ToString()
            End If

            '追記
            'LMB510の場合はLOT_SP_KB　=　"01"で、LOT_NO　に『NO.』(NOドット)　が含まれない場合、
            'かつLOT_NOが10バイト以上(LOT_NO>=10)のときに、
            'ロット番号に半角スペースを4-3-3の間隔で入れる(例：1234 567 890)

            Select Case prtId
                Case "LMB510"
                    LOT_SP_KB = (outDt.Rows(i).Item("LOT_SP_KB").ToString)
                    If LOT_SP_KB = "01" Then
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
                    Else
                    End If

                Case Else

            End Select

            '追記終了

        Next

        Select Case prtId
            Case ""
            Case Else

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
    ''' 印刷データ検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectPrintData(ByVal ds As DataSet) As DataSet

        Select Case ds.Tables("LMB510IN").Rows(0).Item("NRS_BR_CD").ToString
            Case "94"
                MyBase.CallDAC(Me._Dac514, "SelectPrintData", ds)
            Case Else
                MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)
        End Select

        Return ds

    End Function

#End Region

#End Region

End Class
