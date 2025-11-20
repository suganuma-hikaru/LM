' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMI601BLC : 運賃請求明細
'  作  成  者       :  小林信
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI601BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI601BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI601DAC = New LMI601DAC()


#End Region

#Region "Method"

#Region "印刷"

    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet


        '元のデータ
        Dim tableNmIn As String = "LMI601IN"
        Dim tableNmOut As String = "LMI601OUT"
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMI601OUT"), dr.Item("RPT_ID").ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                dr.Item("PTN_ID").ToString(), _
                                dr.Item("PTN_CD").ToString(), _
                                dr.Item("RPT_ID").ToString(), _
                                prtDs.Tables("LMI601OUT"), _
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
        '要望管理1963 S.Kobayashi まとめ番号順ではないため、前行比較をやめる
        Dim tempDs As DataSet = New Jp.Co.Nrs.LM.DSL.LMI601DS
        Dim tempDt As DataTable = tempDs.Tables("LMI601OUT")
        Dim tempDr As DataRow
        Dim preMatomeNo As String = String.Empty
        Dim preUnsoNoL As String = String.Empty
        Dim whereStr As String = String.Empty
        '明細単位で出力しているので、同一のまとめ番号・運送番号Lの場合は金額を０にする。
        For i As Integer = 0 To ds.Tables("LMI601OUT").Rows.Count - 1
            If i <> 0 Then
                whereStr = String.Empty
                whereStr = String.Concat("(SEIQ_GROUP_NO <> '' AND SEIQ_GROUP_NO = '", ds.Tables("LMI601OUT").Rows(i).Item("SEIQ_GROUP_NO").ToString() _
                                         , "') OR UNSO_NO_L ='", ds.Tables("LMI601OUT").Rows(i).Item("UNSO_NO_L").ToString(), "'")
                If (String.IsNullOrEmpty(preMatomeNo) = False _
                    AndAlso preMatomeNo.Equals(ds.Tables("LMI601OUT").Rows(i).Item("SEIQ_GROUP_NO").ToString()) = True) _
                    OrElse (preUnsoNoL.Equals(ds.Tables("LMI601OUT").Rows(i).Item("UNSO_NO_L").ToString()) = True) _
                    OrElse (tempDt.Select(whereStr).Count <> 0) Then
                    '金額を０に設定
                    ds.Tables("LMI601OUT").Rows(i).Item("DECI_UNCHIN") = "0"
                    ds.Tables("LMI601OUT").Rows(i).Item("DECI_CITY_EXTC") = "0"
                    ds.Tables("LMI601OUT").Rows(i).Item("DECI_WINT_EXTC") = "0"
                    ds.Tables("LMI601OUT").Rows(i).Item("DECI_RELY_EXTC") = "0"
                    ds.Tables("LMI601OUT").Rows(i).Item("DECI_TOLL") = "0"
                    ds.Tables("LMI601OUT").Rows(i).Item("DECI_INSU") = "0"
                Else
                    tempDr = tempDt.NewRow
                    tempDr("SEIQ_GROUP_NO") = ds.Tables("LMI601OUT").Rows(i).Item("SEIQ_GROUP_NO").ToString()
                    tempDr("UNSO_NO_L") = ds.Tables("LMI601OUT").Rows(i).Item("UNSO_NO_L").ToString()
                    tempDt.Rows.Add(tempDr)
                End If
            Else
                tempDr = tempDt.NewRow
                tempDr("SEIQ_GROUP_NO") = ds.Tables("LMI601OUT").Rows(i).Item("SEIQ_GROUP_NO").ToString()
                tempDr("UNSO_NO_L") = ds.Tables("LMI601OUT").Rows(i).Item("UNSO_NO_L").ToString()
                tempDt.Rows.Add(tempDr)
            End If
            preMatomeNo = ds.Tables("LMI601OUT").Rows(i).Item("SEIQ_GROUP_NO").ToString()
            preUnsoNoL = ds.Tables("LMI601OUT").Rows(i).Item("UNSO_NO_L").ToString()

        Next



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

        Return MyBase.CallDAC(Me._Dac, "SelectPrintData", ds)

    End Function

#End Region

#End Region

End Class
