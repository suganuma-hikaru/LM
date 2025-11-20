' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求
'  プログラムID     :  LMG500    : 保管料・荷役料請求明細印刷
'  作  成  者       :  [SAGAWA]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMG500BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMG500BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG500DAC = New LMG500DAC()

    'START YANAI 要望番号581
    ''' <summary>
    ''' PGID LMG020
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMG020 As String = "LMG020"

    ''' <summary>
    ''' PGID LMG030
    ''' </summary>
    ''' <remarks></remarks>
    Private _LMG030 As String = "LMG030"
    'END YANAI 要望番号581

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
        Dim tableNmIn As String = "LMG500IN"
        Dim tableNmOut As String = "LMG500OUT"
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

        'START YANAI 要望番号581
        Dim minusChkFlg As Boolean = True
        'END YANAI 要望番号581

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

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
            setDtRpt = setDs.Tables(tableNmRpt)

            'START YANAI 要望番号581
            'マイナスチェックを行う
            If Me.IsMinusChk(setDtOut, dt.Rows(i)) = False Then
                minusChkFlg = False
                If (Me._LMG020).Equals(dt.Rows(i).Item("FROM_PGID").ToString) = True Then
                    Continue For
                ElseIf (Me._LMG030).Equals(dt.Rows(i).Item("FROM_PGID").ToString) = True Then
                    Return ds
                Else
                    Return ds
                End If
            End If
            'END YANAI 要望番号581

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

        'START YANAI 要望番号581
        If minusChkFlg = False = True Then
            Return ds
        End If
        'END YANAI 要望番号581

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
        For Each dr As DataRow In ds.Tables(tableNmRpt).Rows
            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If
            '印刷処理実行
            Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
            prtDs = New DataSet
            '指定したレポートIDのデータを抽出する。
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables(tableNmOut), _
                                ds.Tables(LMConst.RD), _
                                String.Empty, _
                                dt.Rows(0).Item("PREVIEW_FLG").ToString(), _
                                prtNb)

        Next

        Return ds

    End Function

    ''' <summary>
    ''' データセットの編集を行う。
    ''' </summary>
    ''' <param name="rptId"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditPrintDataSet(ByVal rptId As String, ByVal ds As DataSet) As DataSet

        '編集データテーブル取得
        Dim outTable As DataTable = ds.Tables("LMG500OUT")

        'OUTデータ数取得
        Dim max As Integer = outTable.Rows.Count - 1

        Select Case rptId

            Case "LMG500", "LMG502"

                'OUTデータを1行ずつ編集
                For i As Integer = 0 To max

                    '一期首残
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE1").ToString()) = True Then
                        outTable.Rows(i).Item("KISYZAN_NB1") = String.Empty
                    End If

                    '二期首残
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE2").ToString()) = True Then
                        outTable.Rows(i).Item("KISYZAN_NB2") = String.Empty
                    End If

                    '三期首残
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE3").ToString()) = True Then
                        outTable.Rows(i).Item("KISYZAN_NB3") = String.Empty
                    End If

                    '一期保管料単価編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE1").ToString) = True OrElse _
                       outTable.Rows(i).Item("STORAGE1").ToString = outTable.Rows(i).Item("STORAGE2").ToString() Then
                        outTable.Rows(i).Item("STORAGE1") = String.Empty
                    End If

                    '二期保管料単価編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE2").ToString) = True OrElse _
                       outTable.Rows(i).Item("STORAGE2").ToString = outTable.Rows(i).Item("STORAGE3").ToString() Then
                        outTable.Rows(i).Item("STORAGE2") = String.Empty
                    End If

                    '三期保管料単価編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE3").ToString) = True Then
                        outTable.Rows(i).Item("STORAGE3") = String.Empty
                    End If

                    '一期保管料あり積数編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE1").ToString()) = False Then
                        If String.IsNullOrEmpty(outTable.Rows(i).Item("STORAGE1").ToString()) = True Then
                            outTable.Rows(i).Item("SEKI_ARI_NB2") = Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB1").ToString()) _
                                                                  + Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB2").ToString())
                            outTable.Rows(i).Item("SEKI_ARI_NB1") = String.Empty
                        End If
                    End If

                    '二期保管料あり積数編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE2").ToString()) = False Then
                        If String.IsNullOrEmpty(outTable.Rows(i).Item("STORAGE2").ToString()) = True Then
                            outTable.Rows(i).Item("SEKI_ARI_NB3") = Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB2").ToString()) _
                                                                  + Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB3").ToString())
                            outTable.Rows(i).Item("SEKI_ARI_NB2") = String.Empty
                        End If
                    End If

                    '三期保管料あり積数編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE3").ToString()) = True Then
                        outTable.Rows(i).Item("SEKI_ARI_NB3") = String.Empty
                    End If

                Next

            Case "LMG501"

                'OUTデータを1行ずつ編集
                For i As Integer = 0 To max

                    '一期保管料単価編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE1").ToString) = True OrElse _
                       outTable.Rows(i).Item("STORAGE1").ToString = outTable.Rows(i).Item("STORAGE2").ToString() Then
                        outTable.Rows(i).Item("STORAGE1") = String.Empty
                    End If

                    '二期保管料単価編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE2").ToString) = True OrElse _
                       outTable.Rows(i).Item("STORAGE2").ToString = outTable.Rows(i).Item("STORAGE3").ToString() Then
                        outTable.Rows(i).Item("STORAGE2") = String.Empty
                    End If

                    '三期保管料単価編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE3").ToString) = True Then
                        outTable.Rows(i).Item("STORAGE3") = String.Empty
                    End If

                    '一期保管料あり積数編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE1").ToString()) = False Then
                        If String.IsNullOrEmpty(outTable.Rows(i).Item("STORAGE1").ToString()) = True Then
                            outTable.Rows(i).Item("SEKI_ARI_NB2") = Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB1").ToString()) _
                                                                  + Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB2").ToString())
                            outTable.Rows(i).Item("SEKI_ARI_NB1") = String.Empty
                        End If
                    End If

                    '二期保管料あり積数編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE2").ToString()) = False Then
                        If String.IsNullOrEmpty(outTable.Rows(i).Item("STORAGE2").ToString()) = True Then
                            outTable.Rows(i).Item("SEKI_ARI_NB3") = Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB2").ToString()) _
                                                                  + Convert.ToDecimal(outTable.Rows(i).Item("SEKI_ARI_NB3").ToString())
                            outTable.Rows(i).Item("SEKI_ARI_NB2") = String.Empty
                        End If
                    End If

                    '三期保管料あり積数編集
                    If String.IsNullOrEmpty(outTable.Rows(i).Item("KURIKOSI_DATE3").ToString()) = True Then
                        outTable.Rows(i).Item("SEKI_ARI_NB3") = String.Empty
                    End If

                Next

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
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectJobNo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectJobNo", ds)

        ''メッセージコードの設定
        'Dim count As Integer = MyBase.GetResultCount()
        'If count < 1 Then
        '    '0件の場合
        '    MyBase.SetMessage("G021")
        'End If

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

    'START YANAI 要望番号581
    ''' <summary>
    ''' マイナス値チェック
    ''' </summary>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Friend Function IsMinusChk(ByVal setDtOut As DataTable, ByVal setDtIn As DataRow) As Boolean

        'マイナスチェック
        Dim max As Integer = setDtOut.Rows.Count - 1
        For i As Integer = 0 To max
            If Convert.ToDecimal(setDtOut.Rows(i).Item("KISYZAN_NB1").ToString) < 0 OrElse _
               Convert.ToDecimal(setDtOut.Rows(i).Item("KISYZAN_NB2").ToString) < 0 OrElse _
               Convert.ToDecimal(setDtOut.Rows(i).Item("KISYZAN_NB3").ToString) < 0 OrElse _
               Convert.ToDecimal(setDtOut.Rows(i).Item("MATUZAN_NB").ToString) < 0 OrElse _
               Convert.ToDecimal(setDtOut.Rows(i).Item("SEKI_ARI_NB1").ToString) < 0 OrElse _
               Convert.ToDecimal(setDtOut.Rows(i).Item("SEKI_ARI_NB2").ToString) < 0 OrElse _
               Convert.ToDecimal(setDtOut.Rows(i).Item("SEKI_ARI_NB3").ToString) < 0 Then

                If (Me._LMG020).Equals(setDtIn.Item("FROM_PGID").ToString) = True Then
                    MyBase.SetMessageStore("00", "E428", New String() {"マイナス値のデータが存在する", "印刷", String.Concat("JOB番号 = [", setDtOut.Rows(i).Item("JOB_NO").ToString, "]")}, setDtIn.Item("SPREAD_GYO_CNT").ToString)
                ElseIf (Me._LMG030).Equals(setDtIn.Item("FROM_PGID").ToString) = True Then
                    MyBase.SetMessage("E428", New String() {"マイナス値のデータが存在する", "印刷", String.Concat("JOB番号 = [", setDtOut.Rows(i).Item("JOB_NO").ToString, "]")})
                Else
                    MyBase.SetMessage("E428", New String() {"マイナス値のデータが存在する", "印刷", String.Concat("JOB番号 = [", setDtOut.Rows(i).Item("JOB_NO").ToString, "]")})
                End If

                Return False

            End If

        Next

        Return True

    End Function
    'END YANAI 要望番号581

#End Region

#End Region

End Class
