' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMB       : 入荷
'  プログラムID     :  LMB560    : 運送保険申込書
'  作  成  者       :  [daikoku]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMB560BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMB560BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMB560DAC = New LMB560DAC()

#End Region

#Region "Method"

#Region "印刷処理"


    ''' <summary>
    ''' 印刷
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = "LMB560IN"
        Dim tableNmOut As String = "LMB560OUT"
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
            inTbl = setDs.Tables(tableNmIn)   '????なぜ必要なの


            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            Dim count As Integer = MyBase.GetResultCount()
            If count < 1 Then
                '0件の場合
                ''If dt.Rows(0).Item("PRT_PTN").ToString.Equals("21") = True Then
                '運送保険選択処理時
                '検索から処理時
                MyBase.SetMessageStore("00", "E078", New String() {String.Concat("運送保険申込書 ", inTbl.Rows(0).Item("INKA_NO_L").ToString)})

                ''編集画面で使用　　検索画面で複数選択時おかしくなるのでだめ
                'MyBase.SetMessage("E024")

                '               '次の入荷管理番号
                Continue For
            End If

            'メッセージの判定
            'If MyBase.IsMessageExist() = True Then
            '    Return dsinka
            'End If

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)

            '同一入荷管理番号で複数Mの時のチェック削除

            Dim inkano_m As String = String.Empty
            Dim err_m As String = String.Empty
            Dim dest_nm As String = String.Empty
            Dim dest_add As String = String.Empty
            Dim err_flg As Boolean = False
            Dim inkano_m_FLG As Boolean = False

            Dim dsEdit As DataSet = setDs.Copy()
            dsEdit.Tables(tableNmOut).Clear()

            Dim editOut As DataTable = dsEdit.Tables(tableNmOut)

            Dim setRowNo As Integer = 0

            For x As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1
                If inkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("INKA_NO_M").ToString) = False Then
                    If inkano_m.Equals("") = False Then
                        'INKA_Mブレーク時
                        If err_flg = False Then
                            '（納入先、納入先住所一致時）
                            editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
                        End If

                    End If

                    setRowNo = x

                    inkano_m = setDs.Tables(tableNmOut).Rows(x).Item("INKA_NO_M").ToString
                    dest_nm = setDs.Tables(tableNmOut).Rows(x).Item("DEST_NM").ToString
                    dest_add = setDs.Tables(tableNmOut).Rows(x).Item("DEST_ADD").ToString

                    err_flg = False
                    err_m = String.Empty
                End If


                If inkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("INKA_NO_M").ToString) Then
                    If (dest_nm.Equals(setDs.Tables(tableNmOut).Rows(x).Item("DEST_NM").ToString) = False OrElse
                        dest_add.Equals(setDs.Tables(tableNmOut).Rows(x).Item("DEST_ADD").ToString) = False) AndAlso
                        err_m.Equals(inkano_m) = False Then

                        'INKA_Mが同じで納入先、納入先住所が違う時
                        err_flg = True

                        Dim msg As String = String.Concat("運送保険申込書 ", setDs.Tables(tableNmOut).Rows(x).Item("INKA_NO_L").ToString, "-", inkano_m)

                        MyBase.SetMessageStore("00", "E02M", New String() {msg})

                        err_m = inkano_m

                    End If
                Else
                    'INKA_Mブレーク時
                    If err_flg = False Then
                        '（納入先、納入先住所一致時）
                        editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
                        err_flg = True
                    End If
                End If

            Next


            If err_flg = False Then
                editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
            End If

            If dsEdit.Tables(tableNmOut).Rows.Count = 0 Then
                'Return ds
                Continue For
            End If

            setDs = dsEdit.Copy()

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
            If keyBrCd <> sortDtRpt.Rows(l).Item("NRS_BR_CD").ToString() OrElse
               keyPtnId <> sortDtRpt.Rows(l).Item("PTN_ID").ToString() OrElse
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
            prtDs = comPrt.CallDataSet(ds.Tables("LMB560OUT"), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                                  dr.Item("PTN_ID").ToString(),
                                  dr.Item("PTN_CD").ToString(),
                                  dr.Item("RPT_ID").ToString(),
                                  prtDs.Tables("LMB560OUT"),
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
