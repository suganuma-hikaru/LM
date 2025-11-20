' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMF       : 運送
'  プログラムID     :  LMF690BLC : 運送保険
'  作  成  者       :  inoue
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMF690BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMF690BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"
    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMF690DAC = New LMF690DAC()


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
        Dim tableNmIn As String = "LMF690IN"
        Dim tableNmOut As String = "LMF690OUT"
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


            inTbl = setDs.Tables(tableNmIn)

            inTbl.ImportRow(dt.Rows(i))

            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs)

            'Upd 2018.08.16 Start -->
            Dim count As Integer = MyBase.GetResultCount()
            If count < 1 Then
                '0件の場合                
                '検索から処理時
                MyBase.SetMessageStore("00", "E078", New String() {String.Concat("運送保険申込書 ", inTbl.Rows(0).Item("UNSO_NO_L").ToString)})

                'TESTで設定 編集画面(LMF020)からだてIsMessageStoreExistメッセージ蓄積有無の判定がなぜかできない
                ''MyBase.SetMessage("E078", New String() {String.Concat("運送保険申込書 ", inTbl.Rows(0).Item("UNSO_NO_L").ToString)})
                '一括印刷ですべてエラーになるのでやめる
                Continue For
            End If

            'メッセージの判定
            'If MyBase.IsMessageExist() = True Then
            '    Return ds
            'End If

            '<--Upd 2018.08.16 END

            '検索結果取得
            setDs = Me.SelectPrintData(setDs)




            ''Next
            Dim outkano_m As String = String.Empty

            Dim inkano_m As String = String.Empty
            Dim err_m As String = String.Empty
            Dim wh_nm As String = String.Empty
            Dim brad_1 As String = String.Empty
            Dim dest_nm As String = String.Empty
            Dim dest_add As String = String.Empty
            Dim err_flg As Boolean = False

            Dim dsEdit As DataSet = setDs.Copy()
            dsEdit.Tables(tableNmOut).Clear()

            Dim editOut As DataTable = dsEdit.Tables(tableNmOut)

            Dim setRowNo As Integer = 0

            For x As Integer = 0 To setDs.Tables(tableNmOut).Rows.Count - 1
                If setDs.Tables(tableNmOut).Rows(x).Item("MOTO_DATA_KB").Equals("出荷") Then

                    If outkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_M").ToString) = False Then
                        'If outkano_m.Equals("001") Then
                        If outkano_m.Equals("") = False Then
                            If err_flg = False Then
                                'OUTKA_Mブレーク時（発送元、発送元住所一致時）
                                editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))

                            End If
                        End If

                        setRowNo = x

                        outkano_m = setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_M").ToString
                        wh_nm = setDs.Tables(tableNmOut).Rows(x).Item("WH_NM").ToString
                        brad_1 = setDs.Tables(tableNmOut).Rows(x).Item("BRAD_1").ToString

                        err_flg = False
                        err_m = String.Empty
                    End If


                    If outkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_M").ToString) Then
                        If (wh_nm.Equals(setDs.Tables(tableNmOut).Rows(x).Item("WH_NM").ToString) = False OrElse
                            brad_1.Equals(setDs.Tables(tableNmOut).Rows(x).Item("BRAD_1").ToString) = False) AndAlso
                            err_m.Equals(outkano_m) = False Then

                            'OUTKA_Mが同じで発送元、発送元住所が違う時
                            err_flg = True

                            Dim msg As String = String.Concat("出荷管理番号 ", dt.Rows(0).Item("KANRI_NO_L").ToString, "-", outkano_m)

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

                ElseIf setDs.Tables(tableNmOut).Rows(x).Item("MOTO_DATA_KB").Equals("入荷") Then


                    If inkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_M").ToString) = False Then
                        If inkano_m.Equals("") = False Then
                            'INKA_Mブレーク時
                            If err_flg = False Then
                                '（納入先、納入先住所一致時）
                                editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
                            End If

                        End If

                        setRowNo = x

                        inkano_m = setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_M").ToString
                        dest_nm = setDs.Tables(tableNmOut).Rows(x).Item("DEST_NM").ToString
                        dest_add = setDs.Tables(tableNmOut).Rows(x).Item("DEST_ADD").ToString

                        err_flg = False
                        err_m = String.Empty
                    End If


                    If inkano_m.Equals(setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_M").ToString) Then
                        If (dest_nm.Equals(setDs.Tables(tableNmOut).Rows(x).Item("DEST_NM").ToString) = False OrElse
                        dest_add.Equals(setDs.Tables(tableNmOut).Rows(x).Item("DEST_ADD").ToString) = False) AndAlso
                        err_m.Equals(inkano_m) = False Then

                            'INKA_Mが同じで納入先、納入先住所が違う時
                            err_flg = True

                            Dim msg As String = String.Concat("入荷管理番号 ", setDs.Tables(tableNmOut).Rows(x).Item("KANRI_NO_L").ToString, "-", inkano_m)

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


                ElseIf setDs.Tables(tableNmOut).Rows(x).Item("MOTO_DATA_KB").Equals("運送") Then

                    '処理なし

                End If
            Next
            'Next
            If inTbl.Rows(0).Item("MOTO_DATA_KB").Equals("運送") = False Then
                If err_flg = False Then
                    editOut.ImportRow(setDs.Tables(tableNmOut).Rows(setRowNo))
                End If

                If dsEdit.Tables(tableNmOut).Rows.Count = 0 Then
                    Continue For
                    'Return ds
                End If

                setDs = dsEdit.Copy()

            End If

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
            prtDs = comPrt.CallDataSet(ds.Tables("LMF690OUT"), dr.Item("RPT_ID").ToString())

            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item("RPT_ID").ToString(), prtDs)

            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(),
                                  dr.Item("PTN_ID").ToString(),
                                  dr.Item("PTN_CD").ToString(),
                                  dr.Item("RPT_ID").ToString(),
                                  prtDs.Tables("LMF690OUT"),
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

#Region "削除追加登録"

    ''' <summary>
    ''' 削除処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DelUnsoHokenData(ByVal ds As DataSet) As DataSet

        '削除追加処理
        Call Me.DelInsData(ds)

        Return ds

    End Function

    ''' <summary>
    ''' 削除追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsUnsoHokenData(ByVal ds As DataSet) As DataSet

        '削除追加処理
        Call Me.DelInsData(ds)

        Return ds

    End Function


    ''' <summary>
    ''' 新規追加登録
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function DelInsData(ByVal ds As DataSet) As Boolean

        Dim rtnResult As Boolean = False

        '削除
        rtnResult = Me.DeletelUnsoHokenData(ds)

        rtnResult = rtnResult AndAlso Me.InsertUnsoHokenData(ds)

        Return rtnResult

    End Function

    ''' <summary>
    ''' 削除登録処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>SQL_FROM_OUTKA
    Private Function UpdatUnsoHoken(ByVal ds As DataSet) As DataSet

        '運送保険登録
        Dim rtnResult As Boolean = Me.DelInsData(ds)

        'Return rtnResult

        Return ds

    End Function


    ''' <summary>
    ''' 運送保険レコードの更新処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DeletelUnsoHokenData(ByVal ds As DataSet) As Boolean


        '削除
        Return Me.ServerChkJudge(ds, "DeleteUnsoHoken")

    End Function

    ''' <summary>
    ''' 運送保険レコードの追加処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function InsertUnsoHokenData(ByVal ds As DataSet) As Boolean


        '新規登録
        Return Me.ServerChkJudge(ds, "InsrtUnsoHoken")

    End Function
#End Region

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge(ByVal ds As DataSet, ByVal actionId As String) As Boolean

        'DACアクセス
        ds = Me.DacAccess(ds, actionId)


        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function

    ''' <summary>
    ''' DACでメッセージが設定されているかを判定
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>True:エラーなし,OK False:エラーあり</returns>
    ''' <remarks></remarks>
    Private Function ServerChkJudge690(ByVal ds As DataSet, ByVal actionId As String) As Boolean
        'DACアクセス
        ds = Me.DacAccess(ds, actionId)


        'DACアクセス
        ds = Me.DacAccess(ds, actionId)

        'エラーがあるかを判定
        Return Not MyBase.IsMessageExist()

    End Function
#End Region

#Region "ユーティリティ"

    ''' <summary>
    ''' DACクラスアクセス
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <param name="actionId">アクションID</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function DacAccess(ByVal ds As DataSet, ByVal actionId As String) As DataSet

        Return MyBase.CallDAC(Me._Dac, actionId, ds)

    End Function
#End Region

End Class
