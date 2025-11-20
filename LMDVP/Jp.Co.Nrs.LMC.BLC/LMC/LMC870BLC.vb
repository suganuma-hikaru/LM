' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷
'  プログラムID     :  LMC870BLC : 運送保険
'  作  成  者       :  inoue
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC870BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC870BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC870DAC = New LMC870DAC()


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
        Dim tableNmIn As String = "LMC870IN"
        Dim tableNmOut As String = "LMC870OUT"
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
            ' ''If LMConst.FLG.ON.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) _
            ' ''    OrElse LMConst.FLG.OFF.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) Then
            ' ''    PRINT_FLGを設定しているのはLMC020のみ
            ' ''    inTbl.Merge(dt)
            ' ''Else
            ' ''    inTbl.ImportRow(dt.Rows(i))
            ' ''End If
            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            'Upd 2018.08.16 Start -->
            Dim count As Integer = MyBase.GetResultCount()
            If count < 1 Then
                '0件の場合
                If dt.Rows(0).Item("PRT_PTN").ToString.Equals("21") = True Then
                    '運送保険選択処理時
                    '検索から処理時
                    MyBase.SetMessageStore("00", "E078", New String() {String.Concat("運送保険申込書 ", dt.Rows(0).Item("OUTKA_NO_L").ToString)})

                    ''編集画面で使用　　検索画面で複数選択時おかしくなるのでだめ
                    'MyBase.SetMessage("E024")

                End If

                'Return ds
                Continue For
            End If

            'メッセージの判定
            'If MyBase.IsMessageExist() = True Then
            '    Return ds
            'End If

            '<--Upd 2018.08.16 END

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

#If True Then   'ADD 2021/12/09 同一出荷管理番号で複数Mの時のチェック削除
            ''    'For i As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1


            ''Next
            Dim outkano_m As String = String.Empty
            Dim err_m As String = String.Empty
            Dim wh_nm As String = String.Empty
            Dim brad_1 As String = String.Empty
            Dim err_flg As Boolean = False

            Dim dsEdit As DataSet = setDs.Copy()
            dsEdit.Tables(tableNmOut).Clear()

            Dim editOut As DataTable = dsEdit.Tables(tableNmOut)

            'Dim editRow As DataRow
            Dim setRowNo As Integer = 0

            'For Each editRow As DataRow In setDs.Tables(tableNmOut).Rows
            'For y As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1

            'editRow = setDs.Tables(tableNmOut).Rows(i)

            For x As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1
                If outkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("OUTKA_NO_M").ToString) = False Then
                    'If outkano_m.Equals("001") Then
                    If outkano_m.Equals("") = False Then
                        If err_flg = False Then
                            'OUTKA_Mブレーク時（発送元、発送元住所一致時）
                            editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))

                        End If
                    End If

                    setRowNo = x

                    outkano_m = setDs.Tables(tableNmOut).Rows(x).Item("OUTKA_NO_M").ToString
                    wh_nm = setDs.Tables(tableNmOut).Rows(x).Item("WH_NM").ToString
                    brad_1 = setDs.Tables(tableNmOut).Rows(x).Item("BRAD_1").ToString

                    err_flg = False
                    err_m = String.Empty
                End If


                If outkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("OUTKA_NO_M").ToString) Then
                    If (wh_nm.Equals(setDs.Tables(tableNmOut).Rows(x).Item("WH_NM").ToString) = False OrElse
                        brad_1.Equals(setDs.Tables(tableNmOut).Rows(x).Item("BRAD_1").ToString) = False) AndAlso
                        err_m.Equals(outkano_m) = False Then

                        'OUTKA_Mが同じで発送元、発送元住所が違う時
                        err_flg = True

                        Dim msg As String = String.Concat("運送保険申込書 ", dt.Rows(0).Item("OUTKA_NO_L").ToString, "-", outkano_m)

                        MyBase.SetMessageStore("00", "E02L", New String() {msg})

                        err_m = outkano_m
                        'Return ds

                    Else
                        'err_flg = True


                    End If
                Else
                    'OUTKA_Mブレーク時
                    If err_flg = False Then
                        '（発送元、発送元住所一致時）

                        editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
                    End If

                End If
                '=================
            Next
            'Next

            If err_flg = False Then
                editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
            End If

            If dsEdit.Tables(tableNmOut).Rows.Count = 0 Then
                Return ds
            End If

            setDs = dsEdit.Copy()

#End If
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

            'LMC020のときは1回だけ
            ''If LMConst.FLG.ON.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) _
            ''    OrElse LMConst.FLG.OFF.Equals(dt.Rows(0).Item("PRINT_FLG").ToString) Then
            ''    Exit For
            ''End If
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMC870OUT"), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                  dr.Item("PTN_ID").ToString(), _
                                  dr.Item("PTN_CD").ToString(), _
                                  dr.Item("RPT_ID").ToString(), _
                                  prtDs.Tables("LMC870OUT"), _
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
        Return ds
    End Function

    ''' <summary>
    ''' 出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'Upd 2018.08.16 Start -->
        'メッセージコードの設定
        'Dim count As Integer = MyBase.GetResultCount()
        'If count < 1 Then
        '    '0件の場合
        '    MyBase.SetMessage("G021")
        'End If

        '<--Upd 2018.08.16 END

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
