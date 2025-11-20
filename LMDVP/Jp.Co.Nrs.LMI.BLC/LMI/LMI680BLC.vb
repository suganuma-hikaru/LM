' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI680BLC : 保管・荷役明細書(MT触媒)
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI680BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI680BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI680DAC = New LMI680DAC()


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
        Dim tableNmIn As String = "LMI680IN"
        Dim tableNmOut As String = "LMI680OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dtIn As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)
        Dim dtRpt As DataTable = ds.Tables(tableNmRpt)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = dtIn.Rows.Count - 1
        Dim setDtOut As DataTable = Nothing
        Dim setDtRpt As DataTable = Nothing

        'ワーク用RPTテーブル
        Dim workDtRpt As DataTable = dtRpt.Copy

        'IN条件0件チェック
        If dtIn.Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        For i As Integer = 0 To max

            '値のクリア
            setDs.Clear()

            '条件の設定
            inTbl.ImportRow(dtIn.Rows(i))

            '使用帳票ID取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectMPrt", setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = MyBase.CallDAC(Me._Dac, "SelectPrintData", setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

#If True Then   'ADD 2019/03/28 依頼番号 : 004883   【LMS】特定荷主機能_千葉三井化学(MT触媒)保管料をテンプレートから取得
            '請求テンプレートマスタ の保管料がある場合、レコード追加
            '別インスタンス
            Dim newDs As DataSet = ds.Copy()

            Dim chkROW_COUNT As String = "2"
            Dim setDtOutNew As DataTable = newDs.Tables(tableNmOut)
            setDtOutNew.Clear()

            For j As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1
                If (chkROW_COUNT).Equals(setDs.Tables(tableNmOut).Rows(j).Item("ROW_COUNT").ToString) = True _
                    AndAlso ("0.000").Equals(setDs.Tables(tableNmOut).Rows(j).Item("STORAGE_AMO_TOU").ToString) = False Then

                    setDtOutNew.ImportRow(setDs.Tables(tableNmOut).Rows(j))

                    '内容設定
                    setDtOutNew.Rows(j).Item("ROW_COUNT") = "11"
                    setDtOutNew.Rows(j).Item("STORAGE_AMO_TTL") = setDtOutNew.Rows(j).Item("STORAGE_AMO_TOU").ToString

                    setDtOutNew.Rows(j).Item("MEIGARA") = "棟貸保管料"
                    setDtOutNew.Rows(j).Item("KISYZAN_NB1") = "0"
                    setDtOutNew.Rows(j).Item("HANDLING_IN1") = "0"
                    setDtOutNew.Rows(j).Item("STORAGE1") = "0"
                    setDtOutNew.Rows(j).Item("KISYZAN_NB2") = "0"
                    setDtOutNew.Rows(j).Item("KISYZAN_NB3") = "0"
                    setDtOutNew.Rows(j).Item("MATUZAN_NB") = "0"
                    setDtOutNew.Rows(j).Item("INKO_NB_TTL1") = "0"
                    setDtOutNew.Rows(j).Item("INKO_NB_TTL2") = "0"
                    setDtOutNew.Rows(j).Item("HANDLING_IN_AMO_TTL") = "0"
                    setDtOutNew.Rows(j).Item("OUTKO_NB_TTL1") = "0"
                    setDtOutNew.Rows(j).Item("OUTKO_NB_TTL2") = "0"
                    setDtOutNew.Rows(j).Item("HANDLING_OUT_AMO_TTL") = "0"
                    setDtOutNew.Rows(j).Item("SEKISU") = "0"
                    setDtOutNew.Rows(j).Item("KISYZAN_QT1") = "0"
                    setDtOutNew.Rows(j).Item("KISYZAN_QT2") = "0"
                    setDtOutNew.Rows(j).Item("KISYZAN_QT3") = "0"
                    setDtOutNew.Rows(j).Item("MATUZAN_QT") = "0"
                    setDtOutNew.Rows(j).Item("INKO_NB_TTL_QT") = "0"
                    setDtOutNew.Rows(j).Item("OUTKO_NB_TTL_QT") = "0"

                    chkROW_COUNT = ""
                Else

                End If
                setDtOutNew.ImportRow(setDs.Tables(tableNmOut).Rows(j))
            Next

#End If
            '検索結果を詰め替え
#If False Then  'ADD 2019/03/28 依頼番号 : 004883   【LMS】特定荷主機能_千葉三井化学(MT触媒)保管料をテンプレートから取得
                        setDtOut = setDs.Tables(tableNmOut)

#Else
            setDtOut = newDs.Tables(tableNmOut)

#End If



            setDtRpt = setDs.Tables(tableNmRpt)

            '取得した件数分別インスタンスから帳票出力に使用するDSにセットする
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMI680OUT"), dr.Item("RPT_ID").ToString())
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables("LMI680OUT"), _
                                ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function

#End Region

#End Region

End Class
