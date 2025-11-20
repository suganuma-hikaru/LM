' ==========================================================================
'  システム名       :  LM
'  サブシステム名   :  LMC       : 出荷管理
'  プログラムID     :  LMC531    : 出荷指示書控え印刷
'  作  成  者       :  大貫和正
' ==========================================================================
Imports Jp.Co.Nrs.LM.DAC
Imports Jp.Co.Nrs.LM.Utility
Imports Jp.Co.Nrs.LM.Const

''' <summary>
''' LMC531BLCクラス
''' </summary>
''' <remarks></remarks>
Public Class LMC531BLC
    Inherits Jp.Co.Nrs.LM.Base.BLC.LMBaseBLC

#Region "Field"

    ''' <summary>
    ''' 使用するDACクラスの生成
    ''' </summary>
    ''' <remarks></remarks>
    Private _Dac As LMC531DAC = New LMC531DAC()


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

        '部数の設定がない場合は初期値0を設定
        Dim prtNb As Integer = 0
        If String.IsNullOrEmpty(ds.Tables("LMC531IN").Rows(0).Item("PRT_NB").ToString()) = False Then
            prtNb = Convert.ToInt32(ds.Tables("LMC531IN").Rows(0).Item("PRT_NB"))
        End If

        'IN条件0件チェック
        If ds.Tables("LMC531IN").Rows.Count = 0 Then
            '0件の場合、エラーメッセージを設定しリターン
            MyBase.SetMessage("G039")
            Return ds
        End If

        '棟別出力フラグを取得
        Dim touNoFlg As String = ds.Tables("LMC531IN").Rows(0).Item("TOU_BETU_FLG").ToString()

        '使用帳票ID取得
        ds = Me.SelectMPrt(ds)

        '棟別に出力の場合、存在する棟番号を取得
        If touNoFlg = "1" Then
            ds = Me.SelectTouNo(ds)
        End If

        'メッセージの判定
        If MyBase.IsMessageExist() = True Then
            Return ds
        End If

        '検索結果取得
        ds = Me.SelectPrintData(ds)

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility
        'レポートID分繰り返す
        Dim prtDs As DataSet
        '帳票種別用
        Dim rptId As String = String.Empty

        Dim prtmax As Integer = 0
        Dim prtDsMax As Integer = 0
        Dim prtCkMax As Integer = 0
        Dim tableName As String = "LMC531OUT"

        For Each dr As DataRow In ds.Tables("M_RPT").Rows

            'レポートIDが空の場合は処理を飛ばす
            If String.IsNullOrEmpty(dr.Item("RPT_ID").ToString()) = True Then
                Continue For
            End If

            prtDs = New DataSet

            'レポートID取得
            rptId = dr.Item("RPT_ID").ToString()

            'DataSet Table名変換

            '空まはたNULLの場合は処理を飛ばす
            If String.IsNullOrEmpty(rptId) = False Then
                '指定したレポートIDのデータを抽出する。
                prtDs = comPrt.CallDataSet(ds.Tables(tableName), dr.Item("RPT_ID").ToString())

                'データセットの編集(出力用テーブルに抽出データを設定)
                prtDs = Me.EditPrintDataSet(rptId, prtDs)

                '棟別に出力の場合、各出力レコードの印刷対象棟フラグを編集
                If touNoFlg = "1" Then

                    '棟毎に印刷対象棟フラグを編集しCSV出力
                    Dim touNum As Integer = ds.Tables("TOU_NO_LIST").Rows.Count - 1
                    For i As Integer = 0 To touNum

                        comPrt = New LMReportDesignerUtility

                        '印刷対象棟フラグを編集
                        prtDs = Me.EditTouNoDataSet(ds.Tables("TOU_NO_LIST").Rows(i).Item("TOU_NO").ToString, rptId, prtDs, tableName)

                        prtDsMax = prtDs.Tables(tableName).Rows.Count - 1

                        'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True Then
                        If (rptId).Equals("LMC527") = True _
                            OrElse (rptId).Equals("LMC528") = True _
                            OrElse (rptId).Equals("LMC886") = True _
                            OrElse (rptId).Equals("LMC529") = True _
                            OrElse (rptId).Equals("LMC539") = True _
                            OrElse (rptId).Equals("LMC861") = True Then
                            prtmax = 1
                        Else
                            prtmax = 0
                        End If

                        For j As Integer = 0 To prtmax
                            If j = 0 Then
                                For k As Integer = 0 To prtDsMax
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "01"
                                Next

                                '帳票CSV出力
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)

                            ElseIf j = 1 Then
                                For k As Integer = 0 To prtDsMax
                                    prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"
                                Next

                                prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())

                                For l As Integer = 1 To prtCkMax
                                    '帳票CSV出力
                                    Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                                Next

                            End If
                        Next

                    Next

                Else '棟別出力を行わない場合(そのまま出力)

                    comPrt = New LMReportDesignerUtility
                    prtDsMax = prtDs.Tables(tableName).Rows.Count - 1

                    'If (rptId).Equals("LMC527") = True OrElse (rptId).Equals("LMC528") = True OrElse (rptId).Equals("LMC529") = True OrElse (rptId).Equals("LMC539") = True Then
                    If (rptId).Equals("LMC527") = True _
                        OrElse (rptId).Equals("LMC528") = True _
                        OrElse (rptId).Equals("LMC886") = True _
                        OrElse (rptId).Equals("LMC529") = True _
                        OrElse (rptId).Equals("LMC539") = True _
                        OrElse (rptId).Equals("LMC861") = True Then
                        prtmax = 2
                    Else
                        prtmax = 0
                    End If
                    For j As Integer = 0 To prtmax
                        If j = 0 Then
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "01"
                            Next

                            '帳票CSV出力
                            Call Me.CSV_OUT(ds, dr, prtDs, prtNb, tableName)

                        ElseIf j = 1 Then
                            For k As Integer = 0 To prtDsMax
                                prtDs.Tables(tableName).Rows(k).Item("RPT_FLG") = "03"
                            Next

                            prtCkMax = Convert.ToInt32(ds.Tables("SET_NAIYO_CHKLIST").Rows(0).Item("SET_NAIYO").ToString())

                            For l As Integer = 1 To prtCkMax
                                '帳票CSV出力
                                Call Me.TOU_CSV_OUT(ds, dr, prtDs, prtNb, tableName)
                            Next
                        End If
                    Next

                End If

            End If

        Next

        Return ds

    End Function


    ''' <summary>
    ''' 印刷対象棟フラグの編集を行う。
    ''' </summary>
    ''' <param name="touNo"></param>
    ''' <param name="ds"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function EditTouNoDataSet(ByVal touNo As String, ByVal rptId As String, ByVal ds As DataSet, ByVal outTableNm As String) As DataSet

        '印刷対象棟フラグ設定(対象棟:0 その他棟:1)
        Dim count As Integer = ds.Tables(outTableNm).Rows.Count - 1
        For i As Integer = 0 To count
            If ds.Tables(outTableNm).Rows(i).Item("TOU_NO").ToString = touNo Then
                '対象棟
                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "0"
            Else
                'その他棟
                ds.Tables(outTableNm).Rows(i).Item("TOU_BETU_FLG") = "1"
            End If
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
            '追記
            Case "LMC522"

                'LOT_NO
                Dim lot As String = String.Empty
                '探したい文字列
                Dim NO As String = "NO."
                '文字列検出用
                Dim numYN As Integer = 0
                '左4桁格納用
                Dim left As String = String.Empty
                '中央3桁格納用
                Dim center As String = String.Empty
                '右3桁格納用
                Dim right As String = String.Empty
                Dim editLotNo As String = String.Empty
                'スペース
                Dim space As String = " "
                'データテーブル取得
                Dim outDt As DataTable = ds.Tables("LMC531OUT")
                Dim max As Integer = outDt.Rows.Count - 1

                '抽出データ明細行の梱数チェック()
                For i As Integer = 0 To max
                    '初期化
                    left = String.Empty
                    center = String.Empty
                    right = String.Empty
                    editLotNo = String.Empty
                    lot = ((outDt.Rows(i).Item("LOT_NO").ToString())).ToUpper
                    numYN = lot.IndexOf(NO) '「NO.」を探す。あればゼロ以上になる。
                    If numYN = -1 Then
                        'ゼロ(NO.なし)なので加工。
                        If lot.Length >= 10 Then
                            left = lot.Substring(0, 4)
                            center = lot.Substring(4, 3)
                            right = Mid(lot, 8)
                            editLotNo = String.Concat(left, space, center, space, right)
                        Else
                            editLotNo = lot
                        End If

                        'データテーブルに戻す。
                        outDt.Rows(i).Item("LOT_NO") = editLotNo
                    Else
                    End If
                Next
                Return ds
                '追記終わり
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
    '''　棟番号取得処理
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet（プロキシ）</returns>
    ''' <remarks></remarks>
    Private Function SelectTouNo(ByVal ds As DataSet) As DataSet

        ds = MyBase.CallDAC(Me._Dac, "SelectTouNo", ds)

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

    ''' <summary>
    ''' 棟別の帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function TOU_CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        ''TODO 開発元の回答により対応
        ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
        ''1秒（1000ミリ秒）待機する
        'System.Threading.Thread.Sleep(1000)
        ''★★★ END

        '棟別の帳票CSV出力
        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                              dr.Item("PTN_ID").ToString(), _
                              dr.Item("PTN_CD").ToString(), _
                              dr.Item("RPT_ID").ToString(), _
                              prtDs.Tables(tableName), _
                              ds.Tables(LMConst.RD), _
                              String.Empty, _
                              String.Empty, _
                              prtNb)

        Return ds

    End Function

    ''' <summary>
    ''' 帳票CSV出力
    ''' </summary>
    ''' <param name="ds">DataSet</param>
    ''' <returns>DataSet</returns>
    ''' <remarks></remarks>
    Private Function CSV_OUT(ByVal ds As DataSet, ByVal dr As DataRow, ByVal prtDs As DataSet, ByVal prtNb As Integer, ByVal tableName As String) As DataSet

        '印刷処理実行
        Dim comPrt As LMReportDesignerUtility = New LMReportDesignerUtility

        comPrt = New LMReportDesignerUtility

        ''TODO 開発元の回答により対応
        ''★★★ 2011/10/04 SBS)佐川 スプール時間が同一になるのを回避するための暫定措置 START
        ''1秒（1000ミリ秒）待機する
        'System.Threading.Thread.Sleep(1000)
        ''★★★ END

        comPrt.StartPrint(Me, dr.Item("NRS_BR_CD").ToString(), _
                      dr.Item("PTN_ID").ToString(), _
                      dr.Item("PTN_CD").ToString(), _
                      dr.Item("RPT_ID").ToString(), _
                      prtDs.Tables(tableName), _
                      ds.Tables(LMConst.RD), _
                      String.Empty, _
                      String.Empty, _
                      prtNb)

        Return ds

    End Function

#End Region

#End Region

End Class
