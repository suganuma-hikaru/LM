' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMG       : 請求サブシステム
'  プログラムID     :  LMG540BLC : デュポン運賃請求明細書(通常・簿外)
'  作  成  者       :  [篠原]
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

Public Class LMG540BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMG540DAC = New LMG540DAC()


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
        Dim tableNmIn As String = "LMG540IN"
        Dim tableNmOut As String = "LMG540OUT"
        Dim tableNmRpt As String = "M_RPT"
        Dim dt As DataTable = ds.Tables(tableNmIn)
        Dim dtOut As DataTable = ds.Tables(tableNmOut)

        '別インスタンス
        Dim setDs As DataSet = ds.Copy()
        Dim inTbl As DataTable = setDs.Tables(tableNmIn)
        Dim max As Integer = 0
        Dim startIndex As Integer = 0
        Dim setDtOut As DataTable
        Dim setDtRpt As DataTable

        'ワーク用RPTテーブル
        Dim prtType As String = String.Empty

        Dim cntTemp As Integer = 0
        Dim allFlag As Boolean = False
        Dim firstFlag As Boolean = False

        'IN条件0件チェック
        If ds.Tables(tableNmIn).Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        Select Case inTbl.Rows(0).Item("PRT_TYPE").ToString()

            Case "01"
                '全部の場合
                startIndex = 0
                max = 1
                allFlag = True
            Case "02"
                '簿品の場合
                startIndex = 0
                max = 0

            Case "03"
                '簿外品のみの場合
                startIndex = 1
                max = 1

        End Select

        For i As Integer = startIndex To max

            '値のクリア
            setDs.Clear()
            ds.Tables(tableNmOut).Clear()
            '初期化
            dt = ds.Tables(tableNmIn)
            dtOut = ds.Tables(tableNmOut)
            setDs = ds.Copy()

            'ワーク用RPTテーブル
            prtType = String.Empty


            '条件の設定

            Select Case i
                Case 0
                    '１周目は簿品
                    prtType = "01"
                Case 1
                    '２周目は簿外品
                    prtType = "02"
            End Select
            ''条件の設定
            'inTbl.ImportRow(dt.Rows(0))
            setDs.Tables(tableNmIn).Rows(0).Item("PRT_TYPE") = prtType

            If allFlag = True AndAlso i = 0 Then
                '全部の場合かつ１回目(簿品)の検索の場合
                firstFlag = True
            End If
            '使用帳票ID取得
            setDs = Me.SelectMPrt(setDs, firstFlag, cntTemp)


            'メッセージの判定
            If MyBase.IsMessageExist() = True Then
                Return ds
            End If

            If firstFlag = True AndAlso cntTemp = 0 Then
                Continue For
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

                'レポートID分繰り返す
                Dim prtDs As DataSet
                For Each dr As DataRow In setDtRpt.Rows
                    'レポートIDが空の場合は処理を飛ばす
                    If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                        Continue For
                    End If
                    '印刷処理実行
                    Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
                    prtDs = New DataSet
                    '指定したレポートIDのデータを抽出する。
                    prtDs = comPrt.CallDataSet(ds.Tables(tableNmOut), dr.Item("RPT_ID").ToString())
                    '帳票CSV出力
                    comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                                        dr.Item("PTN_ID").ToString(), _
                                        dr.Item("PTN_CD").ToString(), _
                                        dr.Item("RPT_ID").ToString(), _
                                        prtDs.Tables(tableNmOut), _
                                        ds.Tables(LMConst.RD), _
                                        dt.Rows(0).Item("PREVIEW_FLG").ToString())

                Next

        Next


        Return ds

    End Function

    ''' <summary>
    '''　出力対象帳票パターン取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectMPrt(ByVal ds As DataSet, ByVal flg As Boolean, ByRef cnt As Integer) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectMPrt", ds)

        'メッセージコードの設定
        Dim count As Integer = MyBase.GetResultCount()

        cnt = count

        If count < 1 Then
            '0件の場合
            If flg = False Then
                MyBase.SetMessage("G021")
            End If
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

