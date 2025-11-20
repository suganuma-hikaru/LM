' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMI     : 特定荷主機能
'  プログラムID     :  LMI480  : 古河請求(ディック)
'  作  成  者       :  kido
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMI480BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMI480BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMI480DAC = New LMI480DAC()

#End Region

#Region "Const"

    ''' <summary>
    ''' 抽出区分
    ''' </summary>
    ''' <remarks></remarks>
    Public Const SELECT_KB_01_DICG As String = "01"           'DICG関係請求

    ''' <summary>
    ''' 帳票パターン取得テーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_M_RPT As String = "M_RPT"

    ''' <summary>
    ''' 帳票パターンアクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_MPRT As String = "SelectMPrintPattern"

    ''' <summary>
    ''' 印刷データ取得アクション
    ''' </summary>
    ''' <remarks></remarks>
    Private Const ACTION_ID_SELECT_PRT_DATA As String = "SelectPrintData"

    ''' <summary>
    ''' INテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_IN As String = "LMI480IN"

    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT012 As String = "LMI480OUT_0102"
    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT013 As String = "LMI480OUT_0103"
    ''' <summary>
    ''' OUTテーブル名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const TABLE_NM_OUT014 As String = "LMI480OUT_0104"

    ''' <summary>
    ''' RPTテーブルのカラム名
    ''' </summary>
    ''' <remarks></remarks>
    Private Const NRS_BR_CD As String = "NRS_BR_CD"
    Private Const PTN_ID As String = "PTN_ID"
    Private Const PTN_CD As String = "PTN_CD"
    Private Const RPT_ID As String = "RPT_ID"
#End Region

#Region "Method"

    ''' <summary>
    ''' 検索処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    Private Function SelectData(ByVal ds As DataSet) As DataSet

        Dim selectKb As String = ds.Tables("LMI480IN").Rows(0).Item("SELECT_KB").ToString()

        '抽出区分により処理の振り分け
        Select Case selectKb

            'DICG関係請求
            Case SELECT_KB_01_DICG

                '神奈川配送分横持
                ds = MyBase.CallDAC(Me._Dac, "SelectData0101", ds)

                '神奈川配送分横持(聖亘提出用)
                ds = MyBase.CallDAC(Me._Dac, "SelectData0102", ds)     'UPD 2019/05/27 ③神奈川地区(横浜市･川崎市)固定車：D:LMI595 005720【LMS】特定荷主機能_古河HBFN請求(4/10,22,25日分別してデータ反映)の修正依頼(古河佐藤所長)→自動化

                '神奈川地区固定車
                ds = MyBase.CallDAC(Me._Dac, "SelectData0103", ds)     'UPD 2019/05/27 ②神奈川配送分横持：D:LMI594

                '栃木地区最低保証
                ds = MyBase.CallDAC(Me._Dac, "SelectData0104", ds)     'UPD 2019/05/27 ①栃木地区最低保証：RD:LMI593 

        End Select

        Return ds

    End Function

#End Region

#Region "印刷処理"

    ''' <summary>
    ''' 印刷処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function DoPrint(ByVal ds As DataSet) As DataSet

        '元のデータ
        Dim tableNmIn As String = LMI480BLC.TABLE_NM_IN

        '-+-------------------
        ds.Tables("M_RPT").Clear()
        ds.Tables("PREV_INFO").Clear()
        '-*-------------------

        Dim tableNmOut As String = String.Empty

        Select Case ds.Tables(tableNmIn).Rows(0).Item("PTN_ID").ToString
            Case "C7"
                tableNmOut = LMI480BLC.TABLE_NM_OUT014
            Case "C8"
                tableNmOut = LMI480BLC.TABLE_NM_OUT012
            Case "C9"
                tableNmOut = LMI480BLC.TABLE_NM_OUT013
        End Select

        Dim tableNmRpt As String = LMI480BLC.TABLE_NM_M_RPT

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
            '請求で取得済みを使用
            'setDs = Me.SelectPrintData(setDs)

            'メッセージの判定
            'If MyBase.IsMessageExist() = True Then
            '    Return ds
            'End If

            'M_RPT よりRPT_IDをセット　UPD 2019/05/27

            For x As Integer = 0 To ds.Tables(tableNmOut).Rows.Count - 1
                Dim sRPT_ID As String = setDs.Tables("M_RPT").Rows(0).Item("RPT_ID").ToString.Trim
                ds.Tables(tableNmOut).Rows(x).Item("RPT_ID") = sRPT_ID

                If ("LMI594").Equals(sRPT_ID) Then

                    '料金再計算
                    Dim dTank As Decimal = 0
                    Dim dKingaku As Decimal = 0
                    Dim d3tMade As Decimal = 0
                    Dim dWT As Decimal = 0

                    If CDec(ds.Tables(tableNmOut).Rows(x).Item("SUM_WT")) > 3000 Then
                        dWT = CDec(ds.Tables(tableNmOut).Rows(x).Item("SUM_WT")) - 3000
                        dTank = CDec(ds.Tables(tableNmOut).Rows(x).Item("T3_OVER_CHARGE"))
                        dKingaku = dWT * dTank
                        ds.Tables(tableNmOut).Rows(x).Item("EXCESS_CHARGE") = dKingaku

                        ds.Tables(tableNmOut).Rows(x).Item("TOTAL_FARE") = dKingaku + CDec(ds.Tables(tableNmOut).Rows(x).Item("T3MADE"))
                    Else

                        ds.Tables(tableNmOut).Rows(x).Item("EXCESS_CHARGE") = "0".ToString

                        ds.Tables(tableNmOut).Rows(x).Item("TOTAL_FARE") = CDec(ds.Tables(tableNmOut).Rows(x).Item("T3MADE"))

                    End If

                End If
            Next


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
        workDtRpt = view.ToTable(True, LMI480BLC.NRS_BR_CD, LMI480BLC.PTN_ID, LMI480BLC.PTN_CD, LMI480BLC.RPT_ID)

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
            prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item(LMI480BLC.RPT_ID).ToString())
            '帳票ごとの編集があるなら行う。
            prtDs = Me.EditPrintDataSet(dr.Item(LMI480BLC.RPT_ID).ToString(), prtDs)
            '帳票CSV出力
            comPrt.StartPrint(Me, dr.Item(LMI480BLC.NRS_BR_CD).ToString(), _
                                dr.Item(LMI480BLC.PTN_ID).ToString(), _
                                dr.Item(LMI480BLC.PTN_CD).ToString(), _
                                dr.Item(LMI480BLC.RPT_ID).ToString(), _
                                prtDs.Tables(tableNmOut), _
                                ds.Tables(LMConst.RD))

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


        Select Case rptId
            Case ""
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

        ds = MyBase.CallDAC(Me._Dac, LMI480BLC.ACTION_ID_SELECT_MPRT, ds)

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

        Dim rtnDs As DataSet = MyBase.CallDAC(Me._Dac, LMI480BLC.ACTION_ID_SELECT_PRT_DATA, ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()
        If count < 1 Then
            '0件の場合
            MyBase.SetMessage("G021")
        End If

        Return rtnDs

    End Function

#End Region

End Class
