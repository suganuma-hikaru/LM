' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI       : 特定荷主機能
'  プログラムID     :  LMI6I0BLC : シリンダ番号エラーリスト
'  作  成  者       :  yamanaka
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI610BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI610BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI610DAC = New LMI610DAC()


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
        Dim tableNmIn As String = "LMI610IN"
        Dim tableNmOut As String = "LMI610OUT"
        Dim tableNmWk As String = "LMI610WK"
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
            setDs = Me.SelectMPrt(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            'シリアル番号チェック
            setDs = Me.SerialCheck(setDs)

            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            '検索結果を詰め替え
            setDtOut = setDs.Tables(tableNmOut)
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMI610OUT"), dr.Item("RPT_ID").ToString())
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables("LMI610OUT"), _
                                ds.Tables(LMConst.RD))

        Next

        Return ds

    End Function

    ''' <summary>
    ''' シリアル番号のチェック
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SerialCheck(ByVal ds As DataSet) As DataSet

        Dim dtOut As DataTable = ds.Tables("LMI610OUT")
        Dim dtWk As DataTable = ds.Tables("LMI610WK")
        Dim max As Integer = dtWk.Rows.Count - 1
        Dim dr As DataRow = Nothing
        Dim str As Integer = 0

        For i As Integer = 0 To max

            dr = dtWk.Rows(i)

            If String.IsNullOrEmpty(dr.Item("SERIAL_NO").ToString()) = True Then '空白チェック
                dr.Item("ERR_MSG") = "エラー　シリンダ番号なし"
                dtOut.ImportRow(dr)
            ElseIf System.Text.RegularExpressions.Regex.IsMatch(dr.Item("SERIAL_NO").ToString(), "^[0-9]+$") = False Then '数値チェック
                dr.Item("ERR_MSG") = "エラー　半角数字以外"
                dtOut.ImportRow(dr)
            ElseIf dr.Item("SERIAL_NO").ToString().Length <> 9 Then '桁数チェック
                dr.Item("ERR_MSG") = "エラー　９桁以外"
                dtOut.ImportRow(dr)
            Else
                str = Me.CheckDigit(dr) 'チェックデジット

                If Mid(dr.Item("SERIAL_NO").ToString(), 9, 1) <> str.ToString() Then
                    dr.Item("ERR_MSG") = String.Concat("エラー　チェックデジット(", str, ")")
                    dtOut.ImportRow(dr)
                End If
            End If
        Next

        Dim count As Integer = dtOut.Rows.Count
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return ds

    End Function

    ''' <summary>
    '''　チェックデジット処理
    ''' </summary>
    ''' <param name="dr">DataRow</param>
    ''' <returns>str(9桁目の整数)</returns>
    ''' <remarks></remarks>
    Private Function CheckDigit(ByVal dr As DataRow) As Integer

        'チェックデジット作業用変数
        Dim wk As Integer = 0
        Dim str As Integer = 0
        Dim evenNum As Integer = 0

        For j As Integer = 1 To 8
            Select Case j
                Case 1, 3, 5, 7
                    If Mid(dr.Item("SERIAL_NO").ToString(), j, 1) <> "0" Then
                        wk = wk + Convert.ToInt32(Mid(dr.Item("SERIAL_NO").ToString(), j, 1))
                    End If
                Case 2, 4, 6, 8
                    If Mid(dr.Item("SERIAL_NO").ToString(), j, 1) <> "0" Then
                        evenNum = Convert.ToInt32(Mid(dr.Item("SERIAL_NO").ToString(), j, 1)) * 2
                        If evenNum > 9 Then
                            evenNum = Convert.ToInt32(Mid(evenNum.ToString(), 1, 1)) + Convert.ToInt32(Mid(evenNum.ToString(), 2, 1))
                            wk = wk + evenNum
                        Else
                            wk = wk + evenNum
                        End If
                    End If
            End Select
        Next

        If wk < 10 Then
            str = 10 - wk
        ElseIf Mid(wk.ToString(), 2, 1) <> "0" Then
            str = 10 - Convert.ToInt32(Mid(wk.ToString(), 2, 1))
        Else
            str = 0
        End If

        Return str

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

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

#End Region

#End Region

End Class
