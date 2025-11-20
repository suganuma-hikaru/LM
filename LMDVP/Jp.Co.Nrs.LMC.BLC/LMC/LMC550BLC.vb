' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC600    : 荷札
'  作  成  者       :  [shinohara]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC550BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC550BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC550DAC = New LMC550DAC()


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
        Dim tableNmIn As String = "LMC550IN"
        Dim tableNmOut As String = "LMC550OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim tableNmKbn As String = "LMC550_KBN" '2012/06/08 Notes1126
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)
        Dim dtKbn As DataTable = ds.Tables(tableNmKbn) '2012/06/08 Notes1126

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = ds.Tables(tableNmIn).Rows.Count - 1
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable
        'Notes1126対応 データセットLMC550_KBN取得用
        Dim setDsKbn As DataSet = ds.Copy()
        '帳票マスタ退避
        Dim tokushuRptDs As DataSet = Nothing

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Clone

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        '該当の営業所に帳票IDが存在するかチェック
        If String.IsNullOrEmpty(dt.Rows(0).Item("TOKUSHU_KBN").ToString()) = False Then
            'M_RPTを取得
            setDs = Me.SelectTokushuMRpt(setDs)
            If MyBase.IsMessageExist() = True Then
                Return setDs
            End If
            '帳票情報を退避
            tokushuRptDs = setDs.Copy
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)
            '要望番号1787 20130128 s.kobayashi
            setDs = Me.ForcedChangeRPT(dt.Rows(i).Item("TOKUSHU_KBN").ToString(), "M_RPT", setDs, tokushuRptDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '@(2012.06.05)纏め荷札対応 --- START ---
            If dt.Rows(i).Item("MATOME_FLG").Equals("1") = True Then

                '纏め条件の取得
                setDs = Me.SelectMatome(setDs)

                'メッセージの判定
                If MyBase.IsMessageExist() = True Then
                    Return ds
                End If

            End If
            '@(2012.06.05)纏め荷札対応 ---  END  ---

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)
            setDs = Me.ForcedChangeRPT(dt.Rows(i).Item("TOKUSHU_KBN").ToString(), "LMC550OUT", setDs, tokushuRptDs)

            'Notes1126対応 Z_KBNに格納されてる印刷件数取得
            setDsKbn = Me.SelectTagMax(setDsKbn)

            '@(2012.06.05) 要望対応1126 纏め荷札対応 --- START ---
            Dim prtNbFrom As Integer = 1
            Dim prtNbTo As Integer = 1
            Dim MaxPrt As Integer = 1
            Dim maxCtl As Boolean = False
            '荷札印刷枚数取得
            If setDsKbn.Tables(tableNmKbn).Rows.Count.ToString.Equals("0") = False Then
                'DBから出力した印刷可能最大枚数
                MaxPrt = Convert.ToInt32(setDsKbn.Tables(tableNmKbn).Rows(i).Item("PRINT_MAX"))
                maxCtl = True
            End If

            '印刷部数判定処理
            If dt.Rows(i).Item("MATOME_FLG").Equals("1") = True Then
                '①纏め荷札時
                If Convert.ToInt32(setDs.Tables(tableNmOut).Rows(i).Item("OUTKA_PKG_NB")) > MaxPrt AndAlso maxCtl = True Then
                    '区分マスタに設定されたMAX枚数より多ければ、設定MAX枚数を上限とする。
                    prtNbTo = MaxPrt
                Else
                    If Convert.ToInt32(setDs.Tables(tableNmOut).Rows(i).Item("OUTKA_PKG_NB")) > Convert.ToInt32(dt.Rows(i).Item("PRT_NB")) Then
                        'にぬしごとのMAXすう　の　わりあて
                        prtNbTo = Convert.ToInt32(dt.Rows(i).Item("PRT_NB"))
                    Else
                        '上記条件外ならば、合計値を設定
                        prtNbTo = Convert.ToInt32(setDs.Tables(tableNmOut).Rows(i).Item("OUTKA_PKG_NB"))

                    End If
                   

                End If

            Else
                '②通常荷札時
                If String.IsNullOrEmpty(dt.Rows(i).Item("PRT_NB").ToString()) = False Then
                    If Convert.ToInt32(dt.Rows(i).Item("PRT_NB")) > MaxPrt AndAlso maxCtl = True Then
                        '区分マスタに設定されたMAX枚数より多ければ、設定MAX枚数を上限とする。
                        prtNbFrom = Convert.ToInt32(dt.Rows(i).Item("PRT_NB_FROM"))
                        prtNbTo = Convert.ToInt32(dt.Rows(i).Item("PRT_NB_TO")) - (Convert.ToInt32(dt.Rows(i).Item("PRT_NB")) - MaxPrt)
                    Else
                        '上記条件外ならば、合計値を設定
                        prtNbFrom = Convert.ToInt32(dt.Rows(i).Item("PRT_NB_FROM"))
                        prtNbTo = Convert.ToInt32(dt.Rows(i).Item("PRT_NB_TO"))
                    End If
                End If

            End If

            'Dim prtNb As Integer = 1
            'If String.IsNullOrEmpty(dt.Rows(i).Item("PRT_NB").ToString()) = False Then
            '    prtNb = Convert.ToInt32(dt.Rows(i).Item("PRT_NB"))
            'End If
            '@(2012.06.05) 要望対応1126 纏め荷札対応 ---  END  ---

            '検索結果を詰め替え、検索結果データを印刷枚数分複写し先頭ページフラグを付与する
            setDtOut = SetTopFlag(setDs.Tables(tableNmOut), prtNbFrom, prtNbTo)
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

            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables("LMC550OUT"), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs, setDs)

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
            ''END YANAI 要望番号1478 一括印刷が遅い


            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC550OUT"), _
                                  ds.Tables(LMConst.RD), _
                                  )

        Next

        Return ds

    End Function


    ''' <summary>
    ''' OUTデータセットのレコードを印刷枚数分複写し先頭ページフラグを付与
    ''' </summary>
    ''' <param name="outDt"></param>
    ''' <param name="prtNbFrom"></param>
    ''' <param name="prtNbTo"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function SetTopFlag(ByVal outDt As DataTable, ByVal prtNbFrom As Integer, ByVal prtNbTo As Integer) As DataTable

        'リターンデータテーブル作成
        Dim rtnDt As DataTable = outDt.Clone

        'ワーク用OUTデータレコードの取得★
        Dim workoutDt As DataTable = outDt
        'ワーク用OUTデータレコードの取得★
        Dim workoutDr As DataRow = outDt.Rows(0)
        '明細件数カウント用★
        Dim maxrow As Integer = workoutDt.Rows.Count - 1

        'ADD 2016/07/14 新荷札対応 何番目対応
        'Dim iCnt As Integer = 0

        'OUTデータレコードの取得
        'Dim outDr As DataRow = outDt.Rows(0)
        Dim outDr As DataRow

        If workoutDr.Item("RPT_ID").ToString = "LMC557" Then

            'For g As Integer = 0 To maxrow

            '    outDr = outDt.Rows(g)

            '    For h As Integer = 1 To prtNb

            For h As Integer = prtNbFrom To prtNbTo

                For g As Integer = 0 To maxrow

                    outDr = outDt.Rows(g)

                    ''先頭ページフラグを付与
                    If h = prtNbFrom And g = 0 Then
                        outDr.Item("TOP_FLAG") = "1"

                    ElseIf g = 0 Then

                        outDr.Item("TOP_FLAG") = "2"

                    Else

                        outDr.Item("TOP_FLAG") = "0"
                    End If

                    '複写
                    rtnDt.ImportRow(outDr)

                Next


            Next


        Else

            'OUTデータレコードを印刷枚数分複写し先頭ページフラグを付与

            outDr = outDt.Rows(0)

            For i As Integer = prtNbFrom To prtNbTo

                '先頭ページフラグを付与
                If i = prtNbFrom Then
                    outDr.Item("TOP_FLAG") = "1"
                Else
                    outDr.Item("TOP_FLAG") = "0"
                End If

                'ADD 2016/07/14 Start 新荷札対応 何番目かをセット
                ''If workoutDr.Item("RPT_ID").ToString = "LMC550" Then
                'iCnt += 1
                outDr.Item("ROW_NO") = i
                ''End If
                'ADD 2016/07/14 End

                '複写
                rtnDt.ImportRow(outDr)

            Next

        End If

        Return rtnDt

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="prtId"></param>
    ''' <param name="dsOut"></param>
    ''' <param name="dsIn"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal prtId As String, ByVal dsOut As DataSet, ByVal dsIn As DataSet) As DataSet

        Dim inDt As DataTable = dsIn.Tables("LMC550IN")     'データ取得
        Dim outDt As DataTable = dsOut.Tables("LMC550OUT")  'データ取得
        Dim max As Integer = outDt.Rows.Count - 1           '抽出データ数取得
        Dim count As Integer = 0                            '項番(初期値0)

        If prtId.ToString().Equals("LMC551") = True And inDt.Rows(0).Item("PTN_FLAG").Equals("2") Then
            '荷札(日医工用) + 詰め合わせ画面から出力の場合のみ処理実行
            For j As Integer = 0 To max
                outDt.Rows(j).Item("GOODS_CD_CUST") = inDt.Rows(0).Item("GOODS_CD_CUST").ToString()
                outDt.Rows(j).Item("GOODS_NM_2") = inDt.Rows(0).Item("GOODS_NM_1").ToString()
                outDt.Rows(j).Item("GOODS_NM_3") = inDt.Rows(0).Item("GOODS_NM_2").ToString()
                outDt.Rows(j).Item("LT_DATE") = inDt.Rows(0).Item("LT_DATE").ToString()
                outDt.Rows(j).Item("LOT_NO") = inDt.Rows(0).Item("LOT_NO").ToString()
                outDt.Rows(j).Item("GOODS_SYUBETU") = inDt.Rows(0).Item("GOODS_SYUBETU").ToString()
            Next
        End If

        Return dsOut

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTokushuMRpt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTokushuMRpt", ds)

        'メッセージコードの設定
        Dim count As Integer = ds.Tables("M_RPT").Rows.Count
        If count < 1 Then
            '0件の場合SelectTokushuMRpt
            MyBase.SetMessage("E320", New String() {"荷札の帳票パターンが未設定", "印刷"})
        End If

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
            '0件の場合SelectMatome
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

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

    '@(2012.06.05) 纏め荷札対応 --- START ---
    ''' <summary>
    ''' 纏め荷札抽出条件取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMatome(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMatome", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    '@(2012.06.05) 纏め荷札対応 ---  END  ---

    '(2012.06.08)  荷札MAX出力枚数対応 --- START ---
    ''' <summary>
    ''' 纏め荷札抽出条件取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTagMax(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTagMax", ds)

        ''メッセージコードの設定
        'Dim count As Integer = MyBase.GetResultCount()
        'If count < 1 Then
        '    '0件の場合
        '    MyBase.SetMessage("G021")
        'End If

        Return ds

    End Function

    '(2012.06.08)  荷札MAX出力枚数対応---  END  ---

    '20130128 
    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="tokushuKbn"></param>
    ''' <param name="tblNm"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function ForcedChangeRPT(ByVal tokushuKbn As String, ByVal tblNm As String, ByVal ds As DataSet, ByVal rptDs As DataSet) As DataSet

        If String.IsNullOrEmpty(tokushuKbn) = True Then
            Return ds
        End If

        If rptDs Is Nothing OrElse rptDs.Tables("M_RPT").Rows.Count = 0 Then
            Return ds
        End If

        Dim rptRow As DataRow = rptDs.Tables("M_RPT").Rows(0)
        For Each row As DataRow In ds.Tables(tblNm).Rows

            If "M_RPT".Equals(tblNm) = True Then
                row("PTN_CD") = rptRow("PTN_CD")
            End If
            row("RPT_ID") = rptRow("RPT_ID")
        Next

        Return ds

    End Function

#End Region

#End Region

End Class
